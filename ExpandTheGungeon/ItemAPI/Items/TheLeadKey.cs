using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using System;
using ExpandTheGungeon.ExpandMain;
using Pathfinding;
using tk2dRuntime.TileMap;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.SpriteAPI;

namespace ExpandTheGungeon.ItemAPI {

    public class TheLeadKey : PlayerItem {

        public static int TheLeadKeyPickupID;

        public static GameObject TheLeadKeyObject;

        public static void Init(AssetBundle expandSharedAssets1) {
            TheLeadKeyObject = expandSharedAssets1.LoadAsset<GameObject>("The Lead Key");
            SpriteSerializer.AddSpriteToObject(TheLeadKeyObject, ExpandPrefabs.EXItemCollection, "theleadkey");
            
            TheLeadKey theleadkey = TheLeadKeyObject.AddComponent<TheLeadKey>();
            string shortDesc = "Ancient Dungeons Beyond Space";
            string longDesc = "Takes you to a space that only exists in dreams, spitting you back out into the real world somewhere... else.";
            ItemBuilder.SetupItem(theleadkey, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(theleadkey, ItemBuilder.CooldownType.Damage, 450f);
            theleadkey.quality = ItemQuality.B;
            if (!ExpandSettings.EnableEXItems) { theleadkey.quality = ItemQuality.EXCLUDED; }

            theleadkey.passiveStatModifiers = new StatModifier[] {
                new StatModifier() {
                    statToBoost = PlayerStats.StatType.Curse,
                    amount = 1,
                    modifyType = StatModifier.ModifyMethod.ADDITIVE,
                    isMeatBunBuff = false
                }
            };

            TheLeadKeyPickupID = theleadkey.PickupObjectId;
        }


        public TheLeadKey() {
            m_InUse = false;
            m_IsTeleporting = false;
            m_DebugMode = false;
        }
        
        private Texture2D m_CachedScreenCapture;

        private bool m_InUse;
        private bool m_IsTeleporting;
        private bool m_ScreenCapInProgress;
        private bool m_DebugMode;

        private Vector3 m_cachedRoomPosition;

        private List<PrototypeDungeonRoom> MainRoomlist;
        private List<PrototypeDungeonRoom> RewardRoomList;
        private List<PrototypeDungeonRoom> NPCRoomList;
        private List<PrototypeDungeonRoom> SecretRoomList;
        private List<PrototypeDungeonRoom> ShrineRoomList;
        private List<PrototypeDungeonRoom> ExitElevatorRoomList;


        private bool IsUsableRightNow(PlayerController user) {
            if (!user) { return false; }
            if (user?.CurrentRoom?.area?.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) { return false; }
            if (m_InUse | user.IsInCombat | user.IsInMinecart | user.InExitCell) { return false; }
            if (user.CurrentRoom != null && user.CurrentRoom.IsSealed) { return false; }
            if (GameManager.Instance?.CurrentLevelOverrideState == GameManager.LevelOverrideState.RESOURCEFUL_RAT) { return false; }
            return true;
        }

        public override bool CanBeUsed(PlayerController user) { return (IsUsableRightNow(user) && base.CanBeUsed(user)); }

        protected override void DoEffect(PlayerController user) {
            m_InUse = true;
            GameManager.Instance.StartCoroutine(CorruptionRoomTime(user));
        }
        

        private Texture2D PortalTextureRender() {
            m_ScreenCapInProgress = true;
            Texture2D m_Texture = null;
            if (Pixelator.Instance.slavedCameras != null && Pixelator.Instance.slavedCameras.Count > 0) {                
                m_Texture = ExpandUtility.GenerateTexture2DFromRenderTexture(Pixelator.Instance.slavedCameras[0].activeTexture);
            }
            if (m_Texture) {
                m_ScreenCapInProgress = false;
                return m_Texture;
            } else {
                m_ScreenCapInProgress = false;
                return ExpandAssets.LoadAsset<Texture2D>("EX_GlitchPortalDefaultTexture");
            }
        }

        
        public override void Pickup(PlayerController player) {
            base.Pickup(player);


            if (MainRoomlist == null | RewardRoomList == null | NPCRoomList == null | SecretRoomList == null | ShrineRoomList == null) {
                MainRoomlist = new List<PrototypeDungeonRoom>();
                RewardRoomList = new List<PrototypeDungeonRoom>();
                NPCRoomList = new List<PrototypeDungeonRoom>();
                SecretRoomList = new List<PrototypeDungeonRoom>();
                ShrineRoomList = new List<PrototypeDungeonRoom>();
                ExitElevatorRoomList = new List<PrototypeDungeonRoom>();
                
                foreach (WeightedRoom wRoom in ExpandPrefabs.CustomRoomTable.includedRooms.elements) {
                    if (wRoom.room != null && wRoom.room.overrideRoomVisualType == -1) { MainRoomlist.Add(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.MegaChallengeShrineTable.includedRooms.elements) {
                    if (wRoom.room != null) { MainRoomlist.Add(wRoom.room); }
                }
                
                foreach (PrototypeDungeonRoom room in MainRoomlist) {
                    if (room.category == PrototypeDungeonRoom.RoomCategory.REWARD) { RewardRoomList.Add(room); }
                }
                foreach (PrototypeDungeonRoom room in ExpandPrefabs.BonusChestRooms) { RewardRoomList.Add(room); }
                RewardRoomList.Add(ExpandPrefabs.reward_room);
                RewardRoomList.Add(ExpandRoomPrefabs.Expand_Apache_RickRollChest);
                
                foreach (PrototypeDungeonRoom room in ExpandPrefabs.winchesterrooms) {
                    NPCRoomList.Add(room);
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.shop_room_table.includedRooms.elements) {
                    if (wRoom.room != null) { NPCRoomList.Add(wRoom.room); }
                }
                
                foreach (WeightedRoom wRoom in ExpandPrefabs.SecretRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { SecretRoomList.Add(wRoom.room); }
                }
                SecretRoomList.Add(ExpandPrefabs.ResourcefulRat_SecondSecretRoom_01);

                foreach (WeightedRoom wRoom in ExpandPrefabs.basic_special_rooms.includedRooms.elements) {
                    if (wRoom.room != null) {
                        if (!wRoom.room.name.ToLower().StartsWith("shrine_demonface_room")) { ShrineRoomList.Add(wRoom.room); }
                    }
                }

                ShrineRoomList.Add(ExpandPrefabs.black_market);

                ExitElevatorRoomList.Add(ExpandPrefabs.exit_room_basic);
                ExitElevatorRoomList.Add(ExpandPrefabs.tiny_exit);
            }
        }

        protected override void OnPreDrop(PlayerController player) { base.OnPreDrop(player); }

        public override void Update() { base.Update(); }
        
        public void UnfreezeClearChallenge(PlayerController user) {
            user.ClearAllInputOverrides();
            try {
                if (ChallengeManager.Instance.ActiveChallenges.Count > 0 && user.IsInCombat) { ChallengeManager.Instance.ForceStop(); }
            } catch (Exception) { }
        }

        private void StunEnemiesForTeleport(RoomHandler targetRoom, float StunDuration = 0.5f) {
            if (!targetRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.All)) { return; }
            List<AIActor> activeEnemies = targetRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
            if (activeEnemies == null | activeEnemies.Count <= 0) { return; }
            for (int i = 0; i < activeEnemies.Count; i++) {
                if (activeEnemies[i].IsNormalEnemy && activeEnemies[i].healthHaver && !activeEnemies[i].healthHaver.IsBoss) {
                    Vector2 vector = (!activeEnemies[i].specRigidbody) ? activeEnemies[i].sprite.WorldBottomLeft : activeEnemies[i].specRigidbody.UnitBottomLeft;
                    Vector2 vector2 = (!activeEnemies[i].specRigidbody) ? activeEnemies[i].sprite.WorldTopRight : activeEnemies[i].specRigidbody.UnitTopRight;
                    if (activeEnemies[i] && activeEnemies[i].behaviorSpeculator) { activeEnemies[i].behaviorSpeculator.Stun(StunDuration, false); }
                }
            }
        }

        private void TogglePlayerInput(PlayerController targetPlayer, bool lockState) {
            Minimap.Instance.ToggleMinimap(false, false);
            if (lockState) {
                targetPlayer.ForceStopDodgeRoll();
                targetPlayer.CurrentInputState = PlayerInputState.NoInput;
                targetPlayer.healthHaver.IsVulnerable = false;
            } else {
                targetPlayer.CurrentInputState = PlayerInputState.AllInput;
                targetPlayer.healthHaver.IsVulnerable = true;
            }
        }

        public IEnumerator CorruptionRoomTime(PlayerController user) {
            RoomHandler currentRoom = user.CurrentRoom;
            Dungeon dungeon = GameManager.Instance.Dungeon;
                        
            m_CachedScreenCapture = PortalTextureRender();
            yield return null;
            while (m_ScreenCapInProgress) { yield return null; }

            if (currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { StunEnemiesForTeleport(currentRoom, 1f); }

            TogglePlayerInput(user, true);

            m_cachedRoomPosition = user.transform.position;

            AkSoundEngine.PostEvent("Play_EX_CorruptionRoomTransition_01", gameObject);
            ExpandShaders.Instance.GlitchScreenForDuration(1, 1.4f, 0.1f);

            GameObject TempFXObject = Instantiate(ExpandAssets.LoadAsset<GameObject>("EXLeadKeyGlitchScreenFX"), transform.position, Quaternion.identity);
            TempFXObject.transform.SetParent(dungeon.gameObject.transform);
            ExpandScreenFXController fxController = TempFXObject.GetComponent<ExpandScreenFXController>();
            while (fxController.GlitchAmount < 1) {
                fxController.GlitchAmount += (BraveTime.DeltaTime / 0.5f);
                yield return null;
            }

            bool m_CopyCurrentRoom = false;

            if (!string.IsNullOrEmpty(currentRoom.GetRoomName())) { m_CopyCurrentRoom = (UnityEngine.Random.value < 0.05f); }

            PrototypeDungeonRoom SelectedPrototypeDungeonRoom = null;

            if (m_CopyCurrentRoom) {
                try {
                    SelectedPrototypeDungeonRoom = RoomBuilder.GenerateRoomPrefabFromTexture2D(RoomDebug.DumpRoomAreaToTexture2D(currentRoom));
                } catch (Exception ex) {
                    if (ExpandSettings.debugMode) {
                        ETGModConsole.Log("[ExpandTheGungeon.TheLeadKey] ERROR: Exception occured while building room!", true);
                        Debug.LogException(ex);
                    }
                    AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", gameObject);
                    fxController.GlitchAmount = 0;
                    Destroy(TempFXObject);
                    m_InUse = false;
                    TogglePlayerInput(user, false);
                    ClearCooldowns();
                    yield break;
                }
            } else {
                float RoomSelectionSeed = UnityEngine.Random.value;
                bool GoingToSecretBoss = false;

                if (RoomSelectionSeed <= 0.01f) { GoingToSecretBoss = true; }

                if (!GoingToSecretBoss | ExpandSettings.HasSpawnedSecretBoss) {
                    if (RoomSelectionSeed <= 0.25f) {
                        SelectedPrototypeDungeonRoom = BraveUtility.RandomElement(RewardRoomList);
                    } else if (RoomSelectionSeed <= 0.5f) {
                        List<PrototypeDungeonRoom> m_SpecialRooms = new List<PrototypeDungeonRoom>();

                        m_SpecialRooms.Add(BraveUtility.RandomElement(NPCRoomList));
                        m_SpecialRooms.Add(BraveUtility.RandomElement(SecretRoomList));
                        m_SpecialRooms.Add(BraveUtility.RandomElement(ShrineRoomList));

                        SelectedPrototypeDungeonRoom = BraveUtility.RandomElement(m_SpecialRooms);
                    } else {
                        SelectedPrototypeDungeonRoom = BraveUtility.RandomElement(MainRoomlist);
                    }
                } else {
                    ExpandSettings.HasSpawnedSecretBoss = true;

                    RoomHandler[] SecretBossRoomCluster = null;

                    try {
                        SecretBossRoomCluster = GenerateCorruptedBossRoomCluster();
                    } catch (Exception ex) {
                        ETGModConsole.Log("[ExpandTheGungeon.TheLeadKey] ERROR: Exception occured while building room!", true);
                        if (ExpandSettings.debugMode) { Debug.LogException(ex); }
                        AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", gameObject);
                        fxController.GlitchAmount = 0;
                        Destroy(TempFXObject);
                        m_InUse = false;
                        TogglePlayerInput(user, false);
                        ClearCooldowns();
                        yield break;
                    }
                    yield return null;
                    if (SecretBossRoomCluster == null) {
                        AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", gameObject);
                        while (fxController.GlitchAmount > 0) {
                            fxController.GlitchAmount -= (BraveTime.DeltaTime / 0.5f);
                            yield return null;
                        }
                        Destroy(TempFXObject);
                        m_InUse = false;
                        TogglePlayerInput(user, false);
                        ClearCooldowns();
                        yield break;
                    }
                    
                    ExpandPlaceCorruptTiles.PlaceCorruptTiles(dungeon, SecretBossRoomCluster[0], null, true, true);
                    ExpandPlaceCorruptTiles.PlaceCorruptTiles(dungeon, SecretBossRoomCluster[1], null, true, true);

                    TeleportToRoom(user, SecretBossRoomCluster[0]);
                    
                    while (m_IsTeleporting) { yield return null; }

                    
                    GameObject m_PortalWarpObjectBossCluster = Instantiate(ExpandPrefabs.EX_GlitchPortal, (user.gameObject.transform.position + new Vector3(0.75f, 0)), Quaternion.identity);
                    ExpandGlitchPortalController m_PortalControllerBossCluster = m_PortalWarpObjectBossCluster.GetComponent<ExpandGlitchPortalController>();
                    if (m_CachedScreenCapture) { m_PortalControllerBossCluster.renderer.material.SetTexture("_PortalTex", m_CachedScreenCapture); }
                    m_PortalControllerBossCluster.CachedPosition = m_cachedRoomPosition;
                    m_PortalControllerBossCluster.ParentRoom = SecretBossRoomCluster[0];
                    SecretBossRoomCluster[0].RegisterInteractable(m_PortalControllerBossCluster);

                    while (fxController.GlitchAmount > 0) {
                        fxController.GlitchAmount -= (BraveTime.DeltaTime / 0.5f);
                        yield return null;
                    }

                    TogglePlayerInput(user, false);

                    m_PortalControllerBossCluster.Configured = true;

                    Destroy(TempFXObject);
                    m_InUse = false;
                    if (m_DebugMode) { ClearCooldowns(); }
                    yield break;
                }
            }

            if (SelectedPrototypeDungeonRoom == null) {
                AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", gameObject);
                while (fxController.GlitchAmount > 0) {
                    fxController.GlitchAmount -= (BraveTime.DeltaTime / 0.5f);
                    yield return null;
                }
                Destroy(TempFXObject);
                m_InUse = false;
                TogglePlayerInput(user, false);
                ClearCooldowns();
                yield break;
            }

            if (m_CopyCurrentRoom) { SelectedPrototypeDungeonRoom.overrideRoomVisualType = currentRoom.RoomVisualSubtype; }

            RoomHandler GlitchRoom = null;

            string DungeonName = "NULL";

            List<string> DungeonNames = new List<string>() {
                "castle",
                "sewer",
                "jungle",
                "phobos",
                "gungeon",
                "cathedral",
                "belly",
                "mines",                
                "resourcefulrat",
                "catacombs",
                // "nakatomi",
                "west",
                "forge",
                "bullethell",
            };

            for (int i = 0; i < DungeonNames.Count; i++) {
                if (dungeon.gameObject.name.ToLower().Contains(DungeonNames[i])) {
                    DungeonNames.Remove(DungeonNames[i]);
                    break;
                }
            }
            
            DungeonName = BraveUtility.RandomElement(DungeonNames.Shuffle());
            
            foreach (string Name in DungeonNames) {
                if (SelectedPrototypeDungeonRoom.name.ToLower().Contains(Name)) {
                    DungeonName = Name;
                    break;
                }
            }
            
                        
            if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("castle") && !dungeon.gameObject.name.ToLower().Contains("castle")) {
                DungeonName = "Castle";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("sewer_") && !dungeon.gameObject.name.ToLower().Contains("sewer")) {
                DungeonName = "Sewer";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("expand_jungle") && !dungeon.gameObject.name.ToLower().Contains("jungle")) {
                DungeonName = "Jungle";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("expand_forest") && !dungeon.gameObject.name.ToLower().Contains("jungle")) {
                // DungeonName = "Jungle"; Can't use this tileset without exceptions happening.
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("phobos") && !dungeon.gameObject.name.ToLower().Contains("phobos")) {
                DungeonName = "Phobos";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("gungeon_") && !dungeon.gameObject.name.ToLower().Contains("gungeon")) {
                DungeonName = "Gungeon";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("cathedral_") && !dungeon.gameObject.name.ToLower().Contains("cathedral")) {
                DungeonName = "Cathedral";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("expand_belly") && !dungeon.gameObject.name.ToLower().Contains("belly")) {
                DungeonName = "Belly";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("mine_") && !dungeon.gameObject.name.ToLower().Contains("mines")) {
                DungeonName = "Mines";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("mines_") && !dungeon.gameObject.name.ToLower().Contains("mines")) {
                DungeonName = "Mines";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("hollow_") && !dungeon.gameObject.name.ToLower().Contains("catacombs")) {
                DungeonName = "Catacombs";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("catacomb") && !dungeon.gameObject.name.ToLower().Contains("catacombs")) {
                DungeonName = "Catacombs";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("connector_shortcatacave") && !dungeon.gameObject.name.ToLower().Contains("catacombs")) {
                DungeonName = "Catacombs";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("normal_clobulonparadise") && !dungeon.gameObject.name.ToLower().Contains("catacombs")) {
                DungeonName = "Catacombs";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("normal_cubeworld") && !dungeon.gameObject.name.ToLower().Contains("catacombs")) {
                DungeonName = "Catacombs";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("normal_themummyreturns") && !dungeon.gameObject.name.ToLower().Contains("catacombs")) {
                DungeonName = "Catacombs";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("normal_shelletons") && !dungeon.gameObject.name.ToLower().Contains("catacombs")) {
                DungeonName = "Catacombs";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("normal_skeletonsandcubes") && !dungeon.gameObject.name.ToLower().Contains("catacombs")) {
                DungeonName = "Catacombs";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("normal_blobsandcubeslivingtogether") && !dungeon.gameObject.name.ToLower().Contains("catacombs")) {
                DungeonName = "Catacombs";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("office_") && !dungeon.gameObject.name.ToLower().Contains("nakatomi")) {
                // DungeonName = "Nakatomi"; // There are issues trying to use this tileset so will allow random tileset.
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("expand_west") && !dungeon.gameObject.name.ToLower().Contains("west")) {
                // DungeonName = "West"; // West tileset causes issues so have to disable this as well.
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("forge") && !dungeon.gameObject.name.ToLower().Contains("forge")) {
                DungeonName = "Forge";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("bhell_") && !dungeon.gameObject.name.ToLower().Contains("bullethell")) {
                DungeonName = "BulletHell";
            } else if (SelectedPrototypeDungeonRoom.name.ToLower().Contains("hell_") && !dungeon.gameObject.name.ToLower().Contains("bullethell")) {
                DungeonName = "BulletHell";
            }
            
            Dungeon dungeon2 = DungeonDatabase.GetOrLoadByName("Base_" + DungeonName);
            if (!DungeonName.ToLower().Contains(dungeon.gameObject.name) && !m_CopyCurrentRoom) {
                GlitchRoom = ExpandUtility.AddCustomRuntimeRoomWithTileSet(dungeon2, SelectedPrototypeDungeonRoom, false, false, allowProceduralLightFixtures: (true || m_CopyCurrentRoom));
            } else {
                GlitchRoom = ExpandUtility.AddCustomRuntimeRoom(SelectedPrototypeDungeonRoom, false, false, allowProceduralLightFixtures: (true || m_CopyCurrentRoom));
            }
            dungeon2 = null;
            
                     
            if (GlitchRoom == null) {
                AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", gameObject);
                while (fxController.GlitchAmount > 0) {
                    fxController.GlitchAmount -= (BraveTime.DeltaTime / 0.5f);
                    yield return null;
                }
                Destroy(TempFXObject);
                m_InUse = false;                
                TogglePlayerInput(user, false);
                ClearCooldowns();
                yield break;
            }

            if (!string.IsNullOrEmpty(GlitchRoom.GetRoomName())) {
                GlitchRoom.area.PrototypeRoomName = ("Corrupted " + GlitchRoom.GetRoomName());
            } else {
                GlitchRoom.area.PrototypeRoomName = ("Corrupted Room");
            }
            
            if (m_CopyCurrentRoom) {
                if (ExpandSettings.EnableGlitchFloorScreenShader && !dungeon.IsGlitchDungeon) {
                    GameObject GlitchShaderObject = Instantiate(ExpandAssets.LoadAsset<GameObject>("EXRoomCorruptionFX"), GlitchRoom.area.UnitCenter, Quaternion.identity);
                    ExpandScreenFXController FXController = GlitchShaderObject.GetComponent<ExpandScreenFXController>();
                    FXController.ParentRoom = GlitchRoom;
                    FXController.UseCorruptionAmbience = m_CopyCurrentRoom;
                    GlitchShaderObject.transform.SetParent(dungeon.gameObject.transform);
                }

                GameObject[] Objects = FindObjectsOfType<GameObject>();

                try {
                    foreach (GameObject Object in Objects) {
                        if (Object && Object.transform.parent == currentRoom.hierarchyParent && IsValidObject(Object)) {
                            Vector3 OrigPosition = (Object.transform.position - currentRoom.area.basePosition.ToVector3());
                            Vector3 NewPosition = (OrigPosition + GlitchRoom.area.basePosition.ToVector3());
                            GameObject newObject = Instantiate(Object, NewPosition, Quaternion.identity);
                            newObject.transform.SetParent(GlitchRoom.hierarchyParent);

                            if (newObject.GetComponent<BaseShopController>()) { Destroy(newObject.GetComponent<BaseShopController>()); }
                            if (newObject.GetComponent<PathingTrapController>()) { Destroy(newObject.GetComponent<PathingTrapController>()); }

                            if (newObject.GetComponent<IPlaceConfigurable>() != null) { newObject.GetComponent<IPlaceConfigurable>().ConfigureOnPlacement(GlitchRoom); }

                            if (newObject.GetComponent<TalkDoerLite>()) {
                                newObject.GetComponent<TalkDoerLite>().SpeaksGleepGlorpenese = true;
                            }

                            if (newObject.GetComponent<IPlayerInteractable>() != null) {
                                GlitchRoom.RegisterInteractable(newObject.GetComponent<IPlayerInteractable>());
                            }

                            if (newObject.GetComponent<FlippableCover>()) {
                                ExpandKickableObject kickableObject = newObject.AddComponent<ExpandKickableObject>();
                                newObject.GetComponent<ExpandKickableObject>().ConfigureOnPlacement(GlitchRoom);
                                GlitchRoom.RegisterInteractable(kickableObject);
                            }

                            if (newObject && UnityEngine.Random.value <= 0.4f && !newObject.GetComponent<AIActor>() && !newObject.GetComponent<Chest>()) {
                                if (!string.IsNullOrEmpty(newObject.name) && !newObject.name.ToLower().StartsWith("glitchtile") && !newObject.name.ToLower().StartsWith("ex secret door") && !newObject.name.ToLower().StartsWith("lock") && !newObject.name.ToLower().StartsWith("chest")) {
                                    float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.04f);
                                    float RandomDispFloat = UnityEngine.Random.Range(0.06f, 0.08f);
                                    float RandomDispIntensityFloat = UnityEngine.Random.Range(0.07f, 0.1f);
                                    float RandomColorProbFloat = UnityEngine.Random.Range(0.035f, 0.1f);
                                    float RandomColorIntensityFloat = UnityEngine.Random.Range(0.05f, 0.1f);
                                    ExpandShaders.Instance.BecomeGlitched(newObject, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                }
                            }
                        }                    
                    }
                } catch (Exception ex) {
                    if (ExpandSettings.debugMode) {
                        ETGModConsole.Log("[ExpandTheGungeon.TheLeadKey] ERROR: Exception occured while duplicating objects for new room!", true);
                        Debug.LogException(ex);
                    }
                }

                IntVector2 ChestPosition = ExpandObjectDatabase.GetRandomAvailableCellForChest(dungeon, GlitchRoom, new List<IntVector2>());

                if (ChestPosition != IntVector2.Zero) {
                    GameObject newChest = Instantiate(ExpandPrefabs.SurpriseChestObject, ChestPosition.ToVector3(), Quaternion.identity);
                    ExpandFakeChest fakeChest = newChest.GetComponent<ExpandFakeChest>();
                    fakeChest.ConfigureOnPlacement(GlitchRoom);
                    GlitchRoom.RegisterInteractable(fakeChest);
                }
            }

            if (GlitchRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.SECRET && GlitchRoom.IsSecretRoom) { GlitchRoom.secretRoomManager.OpenDoor(); }
            
            if (m_CopyCurrentRoom) {
                ExpandPlaceCorruptTiles.PlaceCorruptTiles(dungeon, GlitchRoom, null, false, true, true);
            } else {
                ExpandPlaceCorruptTiles.PlaceCorruptTiles(dungeon, GlitchRoom, null, true, true, true);
            }
                        
            TeleportToRoom(user, GlitchRoom, false, m_CopyCurrentRoom);
            
            yield return null;
            while (m_IsTeleporting) { yield return null; }
            
            if (user.transform.position.GetAbsoluteRoom() != null) {
                if (user.CurrentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                    user.CurrentRoom.CompletelyPreventLeaving = true;
                }
            }
            if (GameManager.Instance.CurrentFloor == 1) {
                if (dungeon.data.Entrance != null) { dungeon.data.Entrance.AddProceduralTeleporterToRoom(); }
            }

            GameObject m_PortalWarpObject = Instantiate(ExpandPrefabs.EX_GlitchPortal, (user.gameObject.transform.position + new Vector3(0.75f, 0)), Quaternion.identity);
            ExpandGlitchPortalController m_PortalController = m_PortalWarpObject.GetComponent<ExpandGlitchPortalController>();
            if (m_CachedScreenCapture) { m_PortalController.renderer.material.SetTexture("_PortalTex", m_CachedScreenCapture); }
            m_PortalController.CachedPosition = m_cachedRoomPosition;
            m_PortalController.ParentRoom = GlitchRoom;
            GlitchRoom.RegisterInteractable(m_PortalController);
            
            while (fxController.GlitchAmount > 0) {
                fxController.GlitchAmount -= (BraveTime.DeltaTime / 0.5f);
                yield return null;
            }
            
            TogglePlayerInput(user, false);

            m_PortalController.Configured = true;

            Destroy(TempFXObject);
            
            m_InUse = false;
            if (m_DebugMode) { ClearCooldowns(); }
            yield break;
        }

        private bool IsValidObject(GameObject sourceObject) {
            List<Type> ComponentsToCheck = new List<Type>() {
                typeof(PlayerController),
                typeof(AIActor),
                typeof(ElevatorDepartureController),
                typeof(TeleporterController),
                typeof(BaseShopController),
                typeof(ExpandKickableObject),
                typeof(MineCartController),
                typeof(ExpandSecretDoorPlacable),
                typeof(ExpandElevatorDepartureManager),
                typeof(TrapController)
            };
            foreach (Type type in ComponentsToCheck) {
                if (sourceObject.GetComponent(type)) { return false; }
            }
            return true;
        }

        public void TeleportToRoom(PlayerController targetPlayer, RoomHandler targetRoom, bool isSecondaryPlayer = false, bool isCorruptedRoomCopy = false, Vector2? overridePosition = null) {
           m_IsTeleporting = true;
            bool m_NeedsNewPosition = false;
            Vector2 OldPosition = (targetPlayer.transform.position - targetPlayer.CurrentRoom.area.basePosition.ToVector3());
            IntVector2 OldPositionIntVec2 = (targetPlayer.CenterPosition.ToIntVector2() - targetPlayer.CurrentRoom.area.basePosition);
            Vector2 NewPosition = (OldPosition + targetRoom.area.basePosition.ToVector2());

            if (overridePosition.HasValue) {
                NewPosition = (overridePosition.Value + targetRoom.area.basePosition.ToVector2());
            } else {
                if (isCorruptedRoomCopy && !GameManager.Instance.Dungeon.data.isPlainEmptyCell(OldPositionIntVec2.x + targetRoom.area.basePosition.x, OldPositionIntVec2.y + targetRoom.area.basePosition.y)) {
                    m_NeedsNewPosition = true;
                } else if (!isCorruptedRoomCopy) {
                    m_NeedsNewPosition = true;
                }
                if (m_NeedsNewPosition) {
                    IntVector2? randomAvailableCell = ExpandUtility.GetRandomAvailableCellForPlayer(GameManager.Instance.Dungeon, targetRoom);
                    if (randomAvailableCell.HasValue) {
                        NewPosition = randomAvailableCell.Value.ToVector3();
                    } else {
                        randomAvailableCell = ExpandUtility.GetRandomAvailableCellSmart(targetRoom, new IntVector2(2, 3));                    
                    }
                    if (!randomAvailableCell.HasValue) {
                        m_IsTeleporting = false;
                        return;
                    }
                }
            }

            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER && !isSecondaryPlayer) {
                PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(targetPlayer);
                if (otherPlayer) { TeleportToRoom(otherPlayer, targetRoom, true, true); }
            }
            targetPlayer.DoVibration(Vibration.Time.Normal, Vibration.Strength.Medium);            
            GameManager.Instance.StartCoroutine(HandleTeleportToRoom(targetPlayer, NewPosition));
            targetPlayer.specRigidbody.Velocity = Vector2.zero;
            targetPlayer.knockbackDoer.TriggerTemporaryKnockbackInvulnerability(1f);
            targetRoom.EnsureUpstreamLocksUnlocked();
        }

        private IEnumerator HandleTeleportToRoom(PlayerController targetPlayer, Vector2 targetPoint) {
            CameraController cameraController = GameManager.Instance.MainCameraController;
            Vector2 offsetVector = (cameraController.transform.position - targetPlayer.transform.position);
            offsetVector -= cameraController.GetAimContribution();
            cameraController.SetManualControl(true, false);
            cameraController.OverridePosition = cameraController.transform.position;
            yield return new WaitForSeconds(0.1f);
            targetPlayer.transform.position = targetPoint;
            targetPlayer.specRigidbody.Reinitialize();
            targetPlayer.specRigidbody.RecheckTriggers = true;
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                cameraController.OverridePosition = cameraController.GetIdealCameraPosition();
            } else {
                cameraController.OverridePosition = (targetPoint + offsetVector).ToVector3ZUp(0f);
            }
            targetPlayer.WarpFollowersToPlayer();
            targetPlayer.WarpCompanionsToPlayer(false);
            ExpandCombatRoomManager CombatManager = null;
            if (targetPlayer.transform.position.GetAbsoluteRoom() != null) {
                StunEnemiesForTeleport(targetPlayer.transform.position.GetAbsoluteRoom(), 1.8f);
                GameObject RoomManager = new GameObject("Room Manager") { layer = 0 };
                RoomManager.transform.position = targetPlayer.transform.position;
                RoomManager.transform.parent = targetPlayer.transform.position.GetAbsoluteRoom().hierarchyParent;
                CombatManager = RoomManager.AddComponent<ExpandCombatRoomManager>();
                CombatManager.ParentRoom = targetPlayer.transform.position.GetAbsoluteRoom();
            }
            Pixelator.Instance.MarkOcclusionDirty();
            yield return null;
            if (CombatManager) { CombatManager.Activated = true; }
            cameraController.SetManualControl(false, true);
            yield return new WaitForSeconds(0.15f);
            targetPlayer.DoVibration(Vibration.Time.Normal, Vibration.Strength.Medium);
            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(targetPlayer.specRigidbody, null, false);
            m_IsTeleporting = false;
            yield break;
        }
        
        private RoomHandler[] GenerateCorruptedBossRoomCluster(Action<RoomHandler> postProcessCellData = null, DungeonData.LightGenerationStyle lightStyle = DungeonData.LightGenerationStyle.STANDARD) {
            Dungeon dungeon = GameManager.Instance.Dungeon;

            PrototypeDungeonRoom[] RoomArray = new PrototypeDungeonRoom[] {
                ExpandRoomPrefabs.CreepyGlitchRoom_Entrance,
                ExpandRoomPrefabs.CreepyGlitchRoom
            };

            IntVector2[] basePositions = new IntVector2[] { IntVector2.Zero, new IntVector2(14, 0) };
            
                        
            GameObject tileMapObject = GameObject.Find("TileMap");
            tk2dTileMap m_tilemap = tileMapObject.GetComponent<tk2dTileMap>();

            if (m_tilemap == null) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("ERROR: TileMap object is null! Something seriously went wrong!"); }
                return null;
            }

            TK2DDungeonAssembler assembler = new TK2DDungeonAssembler();
            assembler.Initialize(dungeon.tileIndices);

            if (RoomArray.Length != basePositions.Length) {
                Debug.LogError("Attempting to add a malformed room cluster at runtime!");
                return null;
            }

            RoomHandler[] RoomClusterArray = new RoomHandler[RoomArray.Length];
            int num = 6;
            int num2 = 3;
            IntVector2 intVector = new IntVector2(int.MaxValue, int.MaxValue);
            IntVector2 intVector2 = new IntVector2(int.MinValue, int.MinValue);
            for (int i = 0; i < RoomArray.Length; i++) {
                intVector = IntVector2.Min(intVector, basePositions[i]);
                intVector2 = IntVector2.Max(intVector2, basePositions[i] + new IntVector2(RoomArray[i].Width, RoomArray[i].Height));
            }
            IntVector2 a = intVector2 - intVector;
            IntVector2 b = IntVector2.Min(IntVector2.Zero, -1 * intVector);
            a += b;
            IntVector2 intVector3 = new IntVector2(dungeon.data.Width + num, num);
            int newWidth = dungeon.data.Width + num * 2 + a.x;
            int newHeight = Mathf.Max(dungeon.data.Height, a.y + num * 2);
            CellData[][] array = BraveUtility.MultidimensionalArrayResize(dungeon.data.cellData, dungeon.data.Width, dungeon.data.Height, newWidth, newHeight);
            dungeon.data.cellData = array;
            dungeon.data.ClearCachedCellData();
            for (int j = 0; j < RoomArray.Length; j++) {
                IntVector2 d = new IntVector2(RoomArray[j].Width, RoomArray[j].Height);
                IntVector2 b2 = basePositions[j] + b;
                IntVector2 intVector4 = intVector3 + b2;
                CellArea cellArea = new CellArea(intVector4, d, 0);
                cellArea.prototypeRoom = RoomArray[j];
                RoomHandler SelectedRoomInArray = new RoomHandler(cellArea);
                for (int k = -num; k < d.x + num; k++) {
                    for (int l = -num; l < d.y + num; l++) {
                        IntVector2 p = new IntVector2(k, l) + intVector4;
                        if ((k >= 0 && l >= 0 && k < d.x && l < d.y) || array[p.x][p.y] == null) {
                            CellData cellData = new CellData(p, CellType.WALL);
                            cellData.positionInTilemap = cellData.positionInTilemap - intVector3 + new IntVector2(num2, num2);
                            cellData.parentArea = cellArea;
                            cellData.parentRoom = SelectedRoomInArray;
                            cellData.nearestRoom = SelectedRoomInArray;
                            cellData.distanceFromNearestRoom = 0f;
                            array[p.x][p.y] = cellData;
                        }
                    }
                }
                dungeon.data.rooms.Add(SelectedRoomInArray);
                RoomClusterArray[j] = SelectedRoomInArray;
            }

            ConnectClusteredRooms(RoomClusterArray[1], RoomClusterArray[0], RoomArray[1], RoomArray[0], 0, 0, 3, 3);
            try { 
                for (int n = 0; n < RoomClusterArray.Length; n++) {
                    try {
                        RoomClusterArray[n].WriteRoomData(dungeon.data);
                    } catch (Exception) {
                        if (ExpandSettings.debugMode) { ETGModConsole.Log("WARNING: Exception caused during WriteRoomData step on room: " + RoomClusterArray[n].GetRoomName()); }
                    } try {
                        dungeon.data.GenerateLightsForRoom(dungeon.decoSettings, RoomClusterArray[n], GameObject.Find("_Lights").transform, lightStyle);
                    } catch (Exception) {
                        if (ExpandSettings.debugMode) { ETGModConsole.Log("WARNING: Exception caused during GeernateLightsForRoom step on room: " + RoomClusterArray[n].GetRoomName()); }
                    }
                    postProcessCellData?.Invoke(RoomClusterArray[n]);
                    if (RoomClusterArray[n].area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.SECRET) {
                        RoomClusterArray[n].BuildSecretRoomCover();
                    }
                }
                GameObject gameObject = (GameObject)Instantiate(BraveResources.Load("RuntimeTileMap", ".prefab"));
                tk2dTileMap component = gameObject.GetComponent<tk2dTileMap>();
                string str = UnityEngine.Random.Range(10000, 99999).ToString();
                gameObject.name = "Corrupted_" + "RuntimeTilemap_" + str;
                component.renderData.name = "Corrupted_" + "RuntimeTilemap_" + str + " Render Data";
                component.Editor__SpriteCollection = dungeon.tileIndices.dungeonCollection;
                
                TK2DDungeonAssembler.RuntimeResizeTileMap(component, a.x + num2 * 2, a.y + num2 * 2, m_tilemap.partitionSizeX, m_tilemap.partitionSizeY);
                for (int num3 = 0; num3 < RoomArray.Length; num3++) {
                    IntVector2 intVector5 = new IntVector2(RoomArray[num3].Width, RoomArray[num3].Height);
                    IntVector2 b3 = basePositions[num3] + b;
                    IntVector2 intVector6 = intVector3 + b3;
                    for (int num4 = -num2; num4 < intVector5.x + num2; num4++) {
                        for (int num5 = -num2; num5 < intVector5.y + num2 + 2; num5++) {
                            try {
                                assembler.BuildTileIndicesForCell(dungeon, component, intVector6.x + num4, intVector6.y + num5);
                            } catch (Exception ex) {
                                if (ExpandSettings.debugMode) {
                                    ETGModConsole.Log("WARNING: Exception caused during BuildTileIndicesForCell step on room: " + RoomArray[num3].name);
                                    Debug.Log("WARNING: Exception caused during BuildTileIndicesForCell step on room: " + RoomArray[num3].name);
                                    Debug.LogException(ex);
                                }
                            }
                        }
                    }
                }
                RenderMeshBuilder.CurrentCellXOffset = intVector3.x - num2;
                RenderMeshBuilder.CurrentCellYOffset = intVector3.y - num2;
                component.ForceBuild();
                RenderMeshBuilder.CurrentCellXOffset = 0;
                RenderMeshBuilder.CurrentCellYOffset = 0;
                component.renderData.transform.position = new Vector3(intVector3.x - num2, intVector3.y - num2, intVector3.y - num2);
                for (int num6 = 0; num6 < RoomClusterArray.Length; num6++) {
                    RoomClusterArray[num6].OverrideTilemap = component;
                    for (int num7 = 0; num7 < RoomClusterArray[num6].area.dimensions.x; num7++) {
                        for (int num8 = 0; num8 < RoomClusterArray[num6].area.dimensions.y + 2; num8++) {
                            IntVector2 intVector7 = RoomClusterArray[num6].area.basePosition + new IntVector2(num7, num8);
                            if (dungeon.data.CheckInBoundsAndValid(intVector7)) {
                                CellData currentCell = dungeon.data[intVector7];
                                TK2DInteriorDecorator.PlaceLightDecorationForCell(dungeon, component, currentCell, intVector7);
                            }
                        }
                    }
                    Pathfinder.Instance.InitializeRegion(dungeon.data, RoomClusterArray[num6].area.basePosition + new IntVector2(-3, -3), RoomClusterArray[num6].area.dimensions + new IntVector2(3, 3));
                    if (!RoomClusterArray[num6].IsSecretRoom) {
                        RoomClusterArray[num6].RevealedOnMap = true;
                        RoomClusterArray[num6].visibility = RoomHandler.VisibilityStatus.VISITED;
                        StartCoroutine(Minimap.Instance.RevealMinimapRoomInternal(RoomClusterArray[num6], true, true, false));
                    }
                    
                    RoomClusterArray[num6].PostGenerationCleanup();
                }

                if (RoomArray.Length == RoomClusterArray.Length) {
                    for (int i = 0; i < RoomArray.Length; i++) {
                        if (RoomArray[i].usesProceduralDecoration && RoomArray[i].allowFloorDecoration) {
                            TK2DInteriorDecorator decorator = new TK2DInteriorDecorator(assembler);
                            decorator.HandleRoomDecoration(RoomClusterArray[i], dungeon, m_tilemap);
                        }
                    }
                }
            } catch (Exception) { }

            DeadlyDeadlyGoopManager.ReinitializeData();
            Minimap.Instance.InitializeMinimap(dungeon.data);
            return RoomClusterArray;
        }

        private void ConnectClusteredRooms(RoomHandler first, RoomHandler second, PrototypeDungeonRoom firstPrototype, PrototypeDungeonRoom secondPrototype, int firstRoomExitIndex, int secondRoomExitIndex, int room1ExitLengthPadding = 3, int room2ExitLengthPadding = 3) {
            if (first.area.instanceUsedExits == null | second.area.exitToLocalDataMap == null |
                second.area.instanceUsedExits == null | first.area.exitToLocalDataMap == null)
            { return; }
            try {
                first.area.instanceUsedExits.Add(firstPrototype.exitData.exits[firstRoomExitIndex]);
                RuntimeRoomExitData runtimeRoomExitData = new RuntimeRoomExitData(firstPrototype.exitData.exits[firstRoomExitIndex]);
                first.area.exitToLocalDataMap.Add(firstPrototype.exitData.exits[firstRoomExitIndex], runtimeRoomExitData);
                second.area.instanceUsedExits.Add(secondPrototype.exitData.exits[secondRoomExitIndex]);
                RuntimeRoomExitData runtimeRoomExitData2 = new RuntimeRoomExitData(secondPrototype.exitData.exits[secondRoomExitIndex]);
                second.area.exitToLocalDataMap.Add(secondPrototype.exitData.exits[secondRoomExitIndex], runtimeRoomExitData2);
                first.connectedRooms.Add(second);
                first.connectedRoomsByExit.Add(firstPrototype.exitData.exits[firstRoomExitIndex], second);
                first.childRooms.Add(second);
                second.connectedRooms.Add(first);
                second.connectedRoomsByExit.Add(secondPrototype.exitData.exits[secondRoomExitIndex], first);
                second.parentRoom = first;
                runtimeRoomExitData.linkedExit = runtimeRoomExitData2;
                runtimeRoomExitData2.linkedExit = runtimeRoomExitData;
                runtimeRoomExitData.additionalExitLength = room1ExitLengthPadding;
                runtimeRoomExitData2.additionalExitLength = room2ExitLengthPadding;
            } catch (Exception) {
                ETGModConsole.Log("WARNING: Exception caused during CoonectClusteredRunTimeRooms method!");
                return;
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }

    public class ExpandCombatRoomManager : BraveBehaviour {

        public ExpandCombatRoomManager() { Activated = false; }

        public bool Activated;
        
        public RoomHandler ParentRoom;

        private void Start() { }

        private void Update() {
            if (Activated) {
                if (ParentRoom != null) {
                    if (ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                        if (!ParentRoom.CompletelyPreventLeaving) {
                            ParentRoom.CompletelyPreventLeaving = true;
                            ParentRoom.SealRoom();
                            AkSoundEngine.PostEvent("Play_OBJ_gate_slam_01", GameManager.Instance.PrimaryPlayer.gameObject);
                        }
                        return;
                    } else if (ParentRoom.CompletelyPreventLeaving) {
                        ParentRoom.CompletelyPreventLeaving = false;
                        ParentRoom.UnsealRoom();
                        AkSoundEngine.PostEvent("Play_OBJ_gate_open_01", GameManager.Instance.PrimaryPlayer.gameObject);
                        return;
                    } else {
                        return;
                    }
                }
            }
        }
                
        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

