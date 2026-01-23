using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandDungeonFlows;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ItemAPI;
using System.Reflection;

namespace ExpandTheGungeon {

    public class ExpandObjectMods {

        public static void InitSpecialMods() {
            ExpandSettings.randomSeed = Random.value;
                        
            if (!GameManager.Instance | !GameManager.Instance.Dungeon) { return; }
            
            ExpandStaticReferenceManager.PopulateLists();

            InitObjectMods(GameManager.Instance.Dungeon);
            
            ExpandDungeonFlow.isGlitchFlow = false;
            ExpandSettings.SewersIsFuture = false;
        }

        private static void InitObjectMods(Dungeon dungeon) {

            if (!GameManager.Instance | !dungeon) { return; }
            
            if (ExpandSettings.EnableJungleRain && dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.JUNGLEGEON) {
                // GameObject JungleRainPlacable = new GameObject("ExpandJungleThunderStorm", new System.Type[] { typeof(ExpandThunderStormPlacable) }) { layer = 0 };
                GameObject JungleRainPlacable = Object.Instantiate(ExpandAssets.LoadAsset<GameObject>("ExpandJungleThunderStorm"));
                JungleRainPlacable.transform.parent = dungeon.gameObject.transform;
                ExpandThunderStormPlacable ThunderstormPlacable = JungleRainPlacable.GetComponent<ExpandThunderStormPlacable>();
                if (ThunderstormPlacable) {
                    ThunderstormPlacable.RainIntensity = ExpandSettings.JungleRainIntensity;
                    ThunderstormPlacable.ConfigureOnPlacement(null);
                }
            }

            ExpandDungeonMusicAPI.EnteredNewCustomFloor = false;
            try {
                if (GameManager.Instance?.DungeonMusicController){
                    bool SupportsLoopSections = false;
                    bool SupportsCustomMusic = ExpandDungeonMusicAPI.CustomLevelMusic.TryGetValue(dungeon.musicEventName, out SupportsLoopSections);
                    FieldInfo m_CoolDownTimer = typeof(DungeonFloorMusicController).GetField("COOLDOWN_TIMER", BindingFlags.NonPublic | BindingFlags.Instance);
                    FieldInfo m_MusicChangeTimer = typeof(DungeonFloorMusicController).GetField("MUSIC_CHANGE_TIMER", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (SupportsCustomMusic && SupportsLoopSections) {
                        m_CoolDownTimer.SetValue(GameManager.Instance.DungeonMusicController, 34f);
                        m_MusicChangeTimer.SetValue(GameManager.Instance.DungeonMusicController, 50f);
                    } else {
                        m_CoolDownTimer.SetValue(GameManager.Instance.DungeonMusicController, 22.5f);
                        m_MusicChangeTimer.SetValue(GameManager.Instance.DungeonMusicController, 40f);
                    }
                }
            } catch (System.Exception) { }

            if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.BELLYGEON && dungeon.data != null && dungeon.data.rooms != null) {
                foreach (RoomHandler room in dungeon.data.rooms) {
                    if (room != null && room.area != null && room.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) {
                        foreach (AIActor enemy in room.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                            if (enemy && enemy.EnemyGuid == ExpandEnemyDatabase.ParasiteBossGUID) {
                                enemy.AdditionalSafeItemDrops = new List<PickupObject>() { ExpandKeyBulletPickup.OldKeyObject.GetComponent<ExpandKeyBulletPickup>() };
                                break;
                            }
                        }
                        break;
                    }
                }
            }

            if (GameManager.Instance.CurrentFloor == 1) { ExpandSettings.HasSpawnedSecretBoss = false; }
                        
            // Disable victory music for Ser Manuel if not on tutorial floor. (it can cause double music bug if you kill him on other floors)
            if (dungeon.LevelOverrideType != GameManager.LevelOverrideState.TUTORIAL) {
                ExpandPrefabs.SerManuel.GetComponent<HealthHaver>().forcePreventVictoryMusic = true;
            } else {
                ExpandPrefabs.SerManuel.GetComponent<HealthHaver>().forcePreventVictoryMusic = false;
            }

            // Assign pitfall destination to entrance on Floor 1 if in Bossrush mode and special entrance room to Miniboss room path is available.
            // Glitch Chest floors now added for this since they now have elevator entrance rooms.
            if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.BOSSRUSH |
                GameManager.Instance.CurrentGameMode == GameManager.GameMode.SUPERBOSSRUSH | 
                dungeon.IsGlitchDungeon)
            {   
                foreach (RoomHandler specificRoom in dungeon.data.rooms) {
                    if (!string.IsNullOrEmpty(specificRoom.GetRoomName())) {                        
                        if (specificRoom.GetRoomName().ToLower().StartsWith("elevatormaintenance") && (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON | dungeon.IsGlitchDungeon)) {
                            if (dungeon.data.Entrance != null && dungeon.data.Entrance.GetRoomName().ToLower().StartsWith("elevator entrance")) {
                                dungeon.data.Entrance.TargetPitfallRoom = specificRoom;
                                dungeon.data.Entrance.ForcePitfallForFliers = true;
                            }
                        }
                    }
                }
            }

            PlayerController player1 = GameManager.Instance.PrimaryPlayer;
            PlayerController player2 = GameManager.Instance.SecondaryPlayer;

            bool playerHasCorruptedJunk = false;

            if (ExpandPlaceFloorObjects.PlayerHasThirdEye && Pixelator.Instance && Pixelator.Instance.DoOcclusionLayer) { Pixelator.Instance.DoOcclusionLayer = false; }

            if (player1) { if (player1.HasPassiveItem(CorruptedJunk.CorruptedJunkID)) { playerHasCorruptedJunk = true; } }
            if (player2) { if (player2.HasPassiveItem(CorruptedJunk.CorruptedJunkID)) { playerHasCorruptedJunk = true; } }

            if (ExpandSettings.EnableExpandedGlitchFloors && (dungeon.IsGlitchDungeon | ExpandDungeonFlow.isGlitchFlow | playerHasCorruptedJunk)) {
                
                if (!dungeon.IsGlitchDungeon && !ExpandDungeonFlow.isGlitchFlow && playerHasCorruptedJunk) {
                    if (Object.FindObjectsOfType<AIActor>() != null && UnityEngine.Object.FindObjectsOfType<AIActor>().Length > 0) {
                        foreach (AIActor enemy in Object.FindObjectsOfType<AIActor>()) {
                            if (!enemy.IsBlackPhantom && !enemy.healthHaver.IsBoss && !string.IsNullOrEmpty(enemy.EnemyGuid) && enemy.optionalPalette == null && (string.IsNullOrEmpty(enemy.OverrideDisplayName) | !enemy.OverrideDisplayName.StartsWith("Corrupted"))) {
                                if (!ExpandLists.DontGlitchMeList.Contains(enemy.EnemyGuid) && enemy.GetAbsoluteParentRoom() != null && !string.IsNullOrEmpty(enemy.GetAbsoluteParentRoom().GetRoomName()) && enemy.GetAbsoluteParentRoom().GetRoomName().ToLower().StartsWith("corrupted")) {
                                    if (Random.value <= 0.6f) {
                                        ExpandShaders.Instance.BecomeGlitched(enemy, 0.04f, 0.07f, 0.05f, 0.07f, 0.05f);
                                        ExpandEnemyCorruptor.CorruptExistingEnemy(enemy);
                                    }
                                    if (Random.value <= 0.25f && !ExpandLists.blobsAndCritters.Contains(enemy.EnemyGuid) && enemy.GetComponent<ExpandSpawnGlitchObjectOnDeath>() == null) {
                                        enemy.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>();
                                    }
                                }
                            }
                        }
                        MaybeSetupGlitchEnemyStun(dungeon, true);
                    }
                    return;
                }

                if (dungeon.IsGlitchDungeon | ExpandDungeonFlow.isGlitchFlow) {
                    if (ExpandDungeonFlow.isGlitchFlow && !dungeon.IsGlitchDungeon) dungeon.IsGlitchDungeon = true;
                    if (GameManager.HasInstance) ExpandUtility.CheckAndFixNextLevelIndex(GameManager.Instance);

                    dungeon.BossMasteryTokenItemId = CustomMasterRounds.GtlichFloorMasterRoundID;

                    if (ExpandSettings.EnableGlitchFloorScreenShader && !ExpandLists.InvalidGraphicsModes.Contains(SystemInfo.graphicsDeviceType)) {
                        GameObject EXGlitchFloorScreenFX = Object.Instantiate(ExpandAssets.LoadAsset<GameObject>("EXGlitchFloorScreenFX"));
                        EXGlitchFloorScreenFX.transform.SetParent(dungeon.gameObject.transform);
                    }
                    
                    if (Object.FindObjectsOfType<AIActor>() != null && Object.FindObjectsOfType<AIActor>().Length > 0) {
                        foreach (AIActor enemy in Object.FindObjectsOfType<AIActor>()) {
                            float RandomIntervalFloat = Random.Range(0.02f, 0.04f);
                            float RandomDispFloat = Random.Range(0.06f, 0.08f);
                            float RandomDispIntensityFloat = Random.Range(0.07f, 0.1f);
                            float RandomColorProbFloat = Random.Range(0.035f, 0.1f);
                            float RandomColorIntensityFloat = Random.Range(0.05f, 0.1f);

                            if (!enemy.IsBlackPhantom && !enemy.healthHaver.IsBoss && !string.IsNullOrEmpty(enemy.EnemyGuid) && enemy.optionalPalette == null && (string.IsNullOrEmpty(enemy.OverrideDisplayName) | !enemy.OverrideDisplayName.StartsWith("Corrupted"))) {
                                if (!ExpandLists.DontGlitchMeList.Contains(enemy.EnemyGuid)) {
                                    if (Random.value <= 0.6f) {
                                        ExpandShaders.Instance.BecomeGlitched(enemy, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                        ExpandEnemyCorruptor.CorruptExistingEnemy(enemy);
                                    }
                                    if (Random.value <= 0.25f && !ExpandLists.blobsAndCritters.Contains(enemy.EnemyGuid) && enemy.GetComponent<ExpandSpawnGlitchObjectOnDeath>() == null) {
                                        enemy.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>();
                                    }
                                }
                            }
                        }
                    }
                    
                    if (Object.FindObjectsOfType<BraveBehaviour>() != null && Object.FindObjectsOfType<BraveBehaviour>().Length > 0) {
                        foreach (BraveBehaviour targetObject in Object.FindObjectsOfType<BraveBehaviour>()) {
                            if (Random.value <= 0.05f && targetObject.gameObject && !targetObject.gameObject.GetComponent<AIActor>() && !targetObject.gameObject.GetComponent<Chest>()) {
                                if (string.IsNullOrEmpty(targetObject.gameObject.name) | (!targetObject.gameObject.name.ToLower().StartsWith("glitchtile") && !targetObject.gameObject.name.ToLower().StartsWith("ex secret door") && !targetObject.gameObject.name.ToLower().StartsWith("lock") && !targetObject.gameObject.name.ToLower().StartsWith("chest"))) {
                                    float RandomIntervalFloat = Random.Range(0.02f, 0.04f);
                                    float RandomDispFloat = Random.Range(0.06f, 0.08f);
                                    float RandomDispIntensityFloat = Random.Range(0.07f, 0.1f);
                                    float RandomColorProbFloat = Random.Range(0.035f, 0.1f);
                                    float RandomColorIntensityFloat = Random.Range(0.05f, 0.1f);
                                    ExpandShaders.Instance.BecomeGlitched(targetObject, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                }
                            }
                        }
                    }
                    
                    ExpandPlaceCorruptedEnemies.Instance.PlaceRandomEnemies(dungeon, GameManager.Instance.CurrentFloor);
                    ExpandPlaceCorruptedEnemies.DestroyInstance();
                    // Destroy(m_GlitchEnemyRandomizer);
                    MaybeSetupGlitchEnemyStun(dungeon);

                    GameObject[] m_GameObjects = Object.FindObjectsOfType<GameObject>();

                    List<GameObject> m_ExplodyBoyz = new List<GameObject>();

                    if (m_GameObjects != null && m_GameObjects.Length > 0) {
                        foreach (GameObject gameObject in m_GameObjects) {
                            if (!string.IsNullOrEmpty(gameObject.name) && (gameObject.name.ToLower().StartsWith("red barrel")) &&
                                gameObject.transform.childCount > 0 && gameObject.transform.Find("Red Barrel") != null && Random.value <= 0.45f)
                            {
                                m_ExplodyBoyz.Add(gameObject);
                            }
                        }
                        if (m_ExplodyBoyz.Count > 0) {
                            for (int i = 0; i < m_ExplodyBoyz.Count; i++) {
                                Vector2 explodyPosition = (m_ExplodyBoyz[i].transform.PositionVector2() + new Vector2(0.5f, 0.5f));
                                RoomHandler currentRoom = explodyPosition.GetAbsoluteRoom();
                                Object.Destroy(m_ExplodyBoyz[i]);
                                AIActor explodyboy = AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(ExpandEnemyDatabase.ExplodyBoyGUID), explodyPosition, currentRoom, true);
                                float RandomIntervalFloat = Random.Range(0.02f, 0.04f);
                                float RandomDispFloat = Random.Range(0.06f, 0.08f);
                                float RandomDispIntensityFloat = Random.Range(0.07f, 0.1f);
                                float RandomColorProbFloat = Random.Range(0.035f, 0.1f);
                                float RandomColorIntensityFloat = Random.Range(0.05f, 0.1f);
                                ExpandShaders.Instance.BecomeGlitched(explodyboy, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(ExpandDungeonMusicAPI.TempCustomBossMusic)) {
                AIActor[] enemies = Object.FindObjectsOfType<AIActor>();
                if (enemies != null && enemies.Length > 0) {
                    foreach (AIActor enemy in enemies) {
                        if (enemy.healthHaver && enemy.healthHaver.HasHealthBar && enemy.healthHaver.IsBoss && enemy.gameObject.GetComponent<GenericIntroDoer>()) {
                            enemy.gameObject.GetComponent<GenericIntroDoer>().BossMusicEvent = ExpandDungeonMusicAPI.TempCustomBossMusic;
                            ExpandDungeonMusicAPI.TempCustomBossMusic = string.Empty;
                        }
                    }
                }
            }
        }

        private static void MaybeSetupGlitchEnemyStun(Dungeon dungeon, bool forceRun = false) {

            if (!dungeon.IsGlitchDungeon && !forceRun) { return; }

            List<RoomHandler> RoomList = dungeon.data.rooms;
            foreach (RoomHandler room in RoomList) {
                if (!string.IsNullOrEmpty(room.GetRoomName()) && room.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS && room.HasActiveEnemies(RoomHandler.ActiveEnemyType.All)) {
                    foreach (AIActor enemy in room.GetActiveEnemies(RoomHandler.ActiveEnemyType.All)) {
                        if (!string.IsNullOrEmpty(enemy.OverrideDisplayName)) {
                            if (enemy.OverrideDisplayName.ToLower().StartsWith("corrupted")) {
                                enemy.gameObject.AddComponent<ExpandGlitchedEnemyStunManager>();
                            }
                        }
                    }
                }
            }
        }
    }
}

