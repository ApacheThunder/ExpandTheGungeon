using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandDungeonFlows;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandMain;

namespace ExpandTheGungeon {

    public class ExpandObjectMods : MonoBehaviour {
                
        public static void InitSpecialMods() {
            ExpandStats.randomSeed = Random.value;

            if (!GameManager.Instance | !GameManager.Instance.Dungeon) { return; }
            
            if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                List<AGDEnemyReplacementTier> m_cachedReplacementTiers = GameManager.Instance.EnemyReplacementTiers;
                // Removes special enemies added after the secret floor
                for (int i = 0; i < m_cachedReplacementTiers.Count; i++) {
                    if (m_cachedReplacementTiers[i].Name.ToLower().EndsWith("_forge") | m_cachedReplacementTiers[i].Name.ToLower().EndsWith("_hell")) {
                        m_cachedReplacementTiers.Remove(m_cachedReplacementTiers[i]);
                    }
                }
                // Add some of the new FTA enemies to the old secret floors
                ExpandEnemyReplacements.Init(m_cachedReplacementTiers);
            }

            InitObjectMods(GameManager.Instance.Dungeon);

            ExpandDungeonFlow.isGlitchFlow = false;
        }

        private static void InitObjectMods(Dungeon dungeon) {

            if (!GameManager.Instance | !dungeon) { return; }
            
            if (ExpandStats.EnableJungleRain && dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.JUNGLEGEON) {
                GameObject JungleRainPlacable = new GameObject("ExpandJungleThunderStorm", new System.Type[] { typeof(ExpandThunderStormPlacable) }) { layer = 0 };
                JungleRainPlacable.transform.parent = dungeon.gameObject.transform;
                ExpandThunderStormPlacable ThunderstormPlacable = JungleRainPlacable.GetComponent<ExpandThunderStormPlacable>();
                ThunderstormPlacable.useCustomIntensity = true;
                ThunderstormPlacable.RainIntensity = ExpandStats.JungleRainIntensity;
                ThunderstormPlacable.enableLightning = true;
                ThunderstormPlacable.isSecretFloor = false;
                ThunderstormPlacable.ConfigureOnPlacement(null);
            }

            if (GameManager.Instance.CurrentFloor == 1) { ExpandStats.HasSpawnedSecretBoss = false; }
                        
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

            if (player1) { if (player1.HasPassiveItem(ItemAPI.CorruptedJunk.CorruptedJunkID)) { playerHasCorruptedJunk = true; } }
            if (player2) { if (player2.HasPassiveItem(ItemAPI.CorruptedJunk.CorruptedJunkID)) { playerHasCorruptedJunk = true; } }

            if (ExpandStats.EnableExpandedGlitchFloors && (dungeon.IsGlitchDungeon | ExpandDungeonFlow.isGlitchFlow | playerHasCorruptedJunk)) {
                
                if (!dungeon.IsGlitchDungeon && !ExpandDungeonFlow.isGlitchFlow && playerHasCorruptedJunk) {
                    if (FindObjectsOfType<AIActor>() != null && FindObjectsOfType<AIActor>().Length > 0) {
                        foreach (AIActor enemy in FindObjectsOfType<AIActor>()) {
                            if (!enemy.IsBlackPhantom && !enemy.healthHaver.IsBoss && !string.IsNullOrEmpty(enemy.EnemyGuid) && enemy.optionalPalette == null && (string.IsNullOrEmpty(enemy.OverrideDisplayName) | !enemy.OverrideDisplayName.StartsWith("Corrupted"))) {
                                if (!ExpandLists.DontGlitchMeList.Contains(enemy.EnemyGuid) && enemy.GetAbsoluteParentRoom() != null && !string.IsNullOrEmpty(enemy.GetAbsoluteParentRoom().GetRoomName()) && enemy.GetAbsoluteParentRoom().GetRoomName().ToLower().StartsWith("corrupted")) {
                                    if (Random.value <= 0.6f) {
                                        ExpandShaders.Instance.BecomeGlitched(enemy, 0.04f, 0.07f, 0.05f, 0.07f, 0.05f);
                                        ExpandGlitchedEnemies.GlitchExistingEnemy(enemy);
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
                    dungeon.BossMasteryTokenItemId = ItemAPI.CustomMasterRounds.GtlichFloorMasterRoundID;

                    if (ExpandStats.EnableGlitchFloorScreenShader) {
                        GameObject EXGlitchFloorScreenFX = Instantiate(ExpandPrefabs.EXGlitchFloorScreenFX);
                        EXGlitchFloorScreenFX.transform.SetParent(dungeon.gameObject.transform);
                    }
                    
                    if (FindObjectsOfType<AIActor>() != null && FindObjectsOfType<AIActor>().Length > 0) {
                        foreach (AIActor enemy in FindObjectsOfType<AIActor>()) {
                            float RandomIntervalFloat = Random.Range(0.02f, 0.04f);
                            float RandomDispFloat = Random.Range(0.06f, 0.08f);
                            float RandomDispIntensityFloat = Random.Range(0.07f, 0.1f);
                            float RandomColorProbFloat = Random.Range(0.035f, 0.1f);
                            float RandomColorIntensityFloat = Random.Range(0.05f, 0.1f);

                            if (!enemy.IsBlackPhantom && !enemy.healthHaver.IsBoss && !string.IsNullOrEmpty(enemy.EnemyGuid) && enemy.optionalPalette == null && (string.IsNullOrEmpty(enemy.OverrideDisplayName) | !enemy.OverrideDisplayName.StartsWith("Corrupted"))) {
                                if (!ExpandLists.DontGlitchMeList.Contains(enemy.EnemyGuid)) {
                                    if (Random.value <= 0.6f) {
                                        ExpandShaders.Instance.BecomeGlitched(enemy, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                        ExpandGlitchedEnemies.GlitchExistingEnemy(enemy);
                                    }
                                    if (Random.value <= 0.25f && !ExpandLists.blobsAndCritters.Contains(enemy.EnemyGuid) && enemy.GetComponent<ExpandSpawnGlitchObjectOnDeath>() == null) {
                                        enemy.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>();
                                    }
                                }
                            }
                        }
                    }
                    
                    if (FindObjectsOfType<BraveBehaviour>() != null && FindObjectsOfType<BraveBehaviour>().Length > 0) {
                        foreach (BraveBehaviour targetObject in FindObjectsOfType<BraveBehaviour>()) {
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
                    
                    ExpandPlaceGlitchedEnemies.PlaceRandomEnemies(dungeon, GameManager.Instance.CurrentFloor);
                    // Destroy(m_GlitchEnemyRandomizer);
                    MaybeSetupGlitchEnemyStun(dungeon);
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

