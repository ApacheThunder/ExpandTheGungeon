using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;
using System;
using ExpandTheGungeon.ExpandMain;
using Pathfinding;
using tk2dRuntime.TileMap;
using ExpandTheGungeon.ExpandComponents;

namespace ExpandTheGungeon.ItemAPI {

    public class TheLeadKey : PlayerItem {

        public static int TheLeadKeyPickupID;

        public static GameObject TheLeadKeyObject;

        public static void Init(AssetBundle expandSharedAssets1) {
            TheLeadKeyObject = expandSharedAssets1.LoadAsset<GameObject>("The Lead Key");
            ItemBuilder.AddSpriteToObject(TheLeadKeyObject, expandSharedAssets1.LoadAsset<Texture2D>("theleadkey"), false, false);

            TheLeadKey theleadkey = TheLeadKeyObject.AddComponent<TheLeadKey>();
            string shortDesc = "Ancient Dungeons Beyond Space";
            string longDesc = "Takes you to a space that only exists in dreams, spitting you back out into the real world somewhere... else.";
            ItemBuilder.SetupItem(theleadkey, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(theleadkey, ItemBuilder.CooldownType.Damage, 450f);
            theleadkey.quality = ItemQuality.B;

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
            TentacleVFX = (GameObject)ResourceCache.Acquire("Global VFX/VFX_Tentacleport");
            m_InUse = false;
            m_IsTeleporting = false;
        }

        private GameObject TentacleVFX;
        

        private bool m_InUse;
        private bool m_IsTeleporting;
        

        private List<PrototypeDungeonRoom> MainRoomlist;
        private List<PrototypeDungeonRoom> RewardRoomList;
        private List<PrototypeDungeonRoom> NPCRoomList;
        private List<PrototypeDungeonRoom> SecretRoomList;
        private List<PrototypeDungeonRoom> ShrineRoomList;
        private List<PrototypeDungeonRoom> ExitElevatorRoomList;


        private bool IsUsableRightNow(PlayerController user) {
			if (!user) { return false; }
            if (user.CurrentRoom != null) {
                if (user.CurrentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) { return false; }
            }
            if (m_InUse | user.IsInCombat | user.IsInMinecart) { return false; }
            if (user.CurrentRoom != null && user.CurrentRoom.IsSealed) { return false; }
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.RESOURCEFUL_RAT) { return false; }
            return true;
        }

        public override bool CanBeUsed(PlayerController user) { return (IsUsableRightNow(user) && base.CanBeUsed(user)); }

        protected override void DoEffect(PlayerController user) {
            m_InUse = true;
            GameManager.Instance.StartCoroutine(CorruptionRoomTime(user));
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
            // m_PickedUp = true;
        }

        protected override void OnPreDrop(PlayerController player) { base.OnPreDrop(player); }

        public override void Update() { base.Update(); }
        
        private GameObject DoTentacleVFX(PlayerController user) {
            if (TentacleVFX) {
                GameObject tentacleObjectInstance = Instantiate(TentacleVFX);
                tentacleObjectInstance.GetComponent<tk2dBaseSprite>().PlaceAtLocalPositionByAnchor(user.specRigidbody.UnitBottomCenter + new Vector2(0f, -1f), tk2dBaseSprite.Anchor.LowerCenter);
                tentacleObjectInstance.transform.position = tentacleObjectInstance.transform.position.Quantize(0.0625f);
                tentacleObjectInstance.GetComponent<tk2dBaseSprite>().UpdateZDepth();
                return tentacleObjectInstance;
            } else {
                return null;
            }
        }
        
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

            TogglePlayerInput(user, true);

            if (currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { StunEnemiesForTeleport(currentRoom, 1f); }
            
            AkSoundEngine.PostEvent("Play_EX_CorruptionRoomTransition_01", gameObject);
            ExpandShaders.Instance.GlitchScreenForDuration(1, 1.4f, 0.1f);

            GameObject TempFXObject = new GameObject("EXScreenFXTemp") { };
            TempFXObject.transform.SetParent(dungeon.gameObject.transform);
            TempFXObject.SetActive(false);
            yield return null;
            ExpandGlitchScreenFXController fxController = TempFXObject.AddComponent<ExpandGlitchScreenFXController>();
            fxController.shaderType = ExpandGlitchScreenFXController.ShaderType.Glitch;
            fxController.GlitchAmount = 0;
            yield return null;
            TempFXObject.SetActive(true);
            while (fxController.GlitchAmount < 1) {
                fxController.GlitchAmount += (BraveTime.DeltaTime / 0.5f);
                yield return null;
            }

            bool m_CopyCurrentRoom = (UnityEngine.Random.value < 0.4f);

            PrototypeDungeonRoom SelectedPrototypeDungeonRoom = null;
            ExpandPlaceCorruptTiles corruptedTilePlacer = new ExpandPlaceCorruptTiles();

            if (m_CopyCurrentRoom) {
               SelectedPrototypeDungeonRoom = RoomBuilder.GenerateRoomPrefabFromTexture2D(RoomDebug.DumpRoomAreaToTexture2D(currentRoom));
            } else {
                float RoomSelectionSeed = UnityEngine.Random.value;
                bool GoingToSecretBoss = false;

                if (RoomSelectionSeed <= 0.01f) { GoingToSecretBoss = true; }

                if (!GoingToSecretBoss | ExpandStats.HasSpawnedSecretBoss) {
                    if (RoomSelectionSeed <= 0.05f && GameManager.Instance.CurrentFloor != 6 && GameManager.Instance.CurrentFloor != 5) {
                        SelectedPrototypeDungeonRoom = BraveUtility.RandomElement(ExitElevatorRoomList);
                    } else if (RoomSelectionSeed <= 0.25f) {
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
                    ExpandStats.HasSpawnedSecretBoss = true;

                    RoomHandler[] SecretBossRoomCluster = GenerateCorruptedBossRoomCluster();
                    yield return null;
                    if (SecretBossRoomCluster == null) {
                        AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", gameObject);
                        TogglePlayerInput(user, false);
                        ClearCooldowns();
                        yield break;
                    }
                    
                    corruptedTilePlacer.PlaceCorruptTiles(dungeon, SecretBossRoomCluster[0], null, true, true);
                    corruptedTilePlacer.PlaceCorruptTiles(dungeon, SecretBossRoomCluster[1], null, true, true);

                    TeleportToRoom(user, SecretBossRoomCluster[0]);

                    while (fxController.GlitchAmount > 0) {
                        fxController.GlitchAmount -= (BraveTime.DeltaTime / 0.5f);
                        yield return null;
                    }

                    TogglePlayerInput(user, false);

                    Destroy(TempFXObject);
                    corruptedTilePlacer = null;
                    m_InUse = false;
                    yield break;
                }
            }

            if (SelectedPrototypeDungeonRoom == null) {
                AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", gameObject);
                TogglePlayerInput(user, false);
                ClearCooldowns();
                yield break;
            }

            if (m_CopyCurrentRoom) { SelectedPrototypeDungeonRoom.overrideRoomVisualType = currentRoom.RoomVisualSubtype; }

            RoomHandler GlitchRoom = ExpandUtility.Instance.AddCustomRuntimeRoom(SelectedPrototypeDungeonRoom, addTeleporter: false, allowProceduralLightFixtures: (true || m_CopyCurrentRoom));

            if (GlitchRoom == null) {
                AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", gameObject);
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
                GameObject GlitchShaderObject = Instantiate(ExpandPrefabs.EXGlitchFloorScreenFX, GlitchRoom.area.UnitCenter, Quaternion.identity);
                ExpandGlitchScreenFXController FXController = GlitchShaderObject.GetComponent<ExpandGlitchScreenFXController>();
                FXController.isRoomSpecific = true;
                FXController.ParentRoom = GlitchRoom;
                FXController.UseCorruptionAmbience = m_CopyCurrentRoom;
                GlitchShaderObject.transform.SetParent(dungeon.gameObject.transform);

                GameObject[] Objects = FindObjectsOfType<GameObject>();

                foreach (GameObject Object in Objects) {
                    if (Object && Object.transform.parent == currentRoom.hierarchyParent &&
                        !Object.GetComponent<PlayerController>() && !Object.GetComponent<AIActor>() &&
                        !Object.GetComponent<ElevatorDepartureController>() && !Object.GetComponent<TeleporterController>() &&
                        !Object.GetComponent<BaseShopController>() && !Object.GetComponent<ExpandKickableObject>()
                       )
                    {
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

                IntVector2 ChestPosition = ExpandObjectDatabase.GetRandomAvailableCellForChest(dungeon, GlitchRoom, new List<IntVector2>());

                if (ChestPosition != IntVector2.Zero) {
                    GameObject newChest = Instantiate(ExpandPrefabs.SurpriseChestObject, ChestPosition.ToVector3(), Quaternion.identity);
                    ExpandFakeChest fakeChest = newChest.GetComponent<ExpandFakeChest>();
                    fakeChest.ConfigureOnPlacement(GlitchRoom);
                    GlitchRoom.RegisterInteractable(fakeChest);
                    ExpandKickableObject kickableChest = newChest.AddComponent<ExpandKickableObject>();
                    kickableChest.ConfigureOnPlacement(GlitchRoom);
                    GlitchRoom.RegisterInteractable(kickableChest);
                }
            }

            if (GlitchRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.SECRET && GlitchRoom.IsSecretRoom) { GlitchRoom.secretRoomManager.OpenDoor(); }

            if (m_CopyCurrentRoom) {
                corruptedTilePlacer.PlaceCorruptTiles(dungeon, GlitchRoom, null, false, true, true);
            } else {
                corruptedTilePlacer.PlaceCorruptTiles(dungeon, GlitchRoom, null, true, true, true);
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

            while (fxController.GlitchAmount > 0) {
                fxController.GlitchAmount -= (BraveTime.DeltaTime / 0.5f);
                yield return null;
            }

            TogglePlayerInput(user, false);
            Destroy(TempFXObject);
            corruptedTilePlacer = null;
            
            m_InUse = false;
            yield break;
        }

        public void TeleportToRoom(PlayerController targetPlayer, RoomHandler targetRoom, bool isSecondaryPlayer = false, bool isCorruptedRoomCopy = false) {
            m_IsTeleporting = true;
            bool m_NeedsNewPosition = false;
            Vector3 OldPosition = (targetPlayer.transform.position - targetPlayer.CurrentRoom.area.basePosition.ToVector3());
            IntVector2 OldPositionIntVec2 = (targetPlayer.CenterPosition.ToIntVector2() - targetPlayer.CurrentRoom.area.basePosition);
            Vector3 NewPosition = (OldPosition + targetRoom.area.basePosition.ToVector3());

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
                    randomAvailableCell = ExpandUtility.Instance.GetRandomAvailableCellSmart(targetRoom, new IntVector2(1, 3));                    
                }
                if (!randomAvailableCell.HasValue) {
                    m_IsTeleporting = false;
                    return;
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
            // if (targetPlayer.transform.position.GetAbsoluteRoom() != null) { StunEnemiesForTeleport(targetPlayer.transform.position.GetAbsoluteRoom(), 0.25f); }
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
                if (ExpandStats.debugMode) { ETGModConsole.Log("ERROR: TileMap object is null! Something seriously went wrong!"); }
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
                        if (ExpandStats.debugMode) { ETGModConsole.Log("WARNING: Exception caused during WriteRoomData step on room: " + RoomClusterArray[n].GetRoomName()); }
                    } try {
                        dungeon.data.GenerateLightsForRoom(dungeon.decoSettings, RoomClusterArray[n], GameObject.Find("_Lights").transform, lightStyle);
                    } catch (Exception) {
                        if (ExpandStats.debugMode) { ETGModConsole.Log("WARNING: Exception caused during GeernateLightsForRoom step on room: " + RoomClusterArray[n].GetRoomName()); }
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
                                if (ExpandStats.debugMode) {
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

