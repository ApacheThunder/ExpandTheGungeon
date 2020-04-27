using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;
using System;
using ExpandTheGungeon.ExpandMain;
using System.Collections.ObjectModel;

namespace ExpandTheGungeon.ItemAPI {

    public class TheLeadKey : PlayerItem {

        public static int TheLeadKeyPickupID;
        
        public static void Init() {
            string name = "The Lead Key";
            string resourcePath = "ExpandTheGungeon/Textures/Items/theleadkey";
            GameObject gameObject = new GameObject();
            TheLeadKey theleadkey = gameObject.AddComponent<TheLeadKey>();
            ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject, true);
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
            if (user.CurrentRoom != null) {
                if (user.CurrentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) { return false; }
            }
            if (m_InUse | user.IsInCombat | user.IsInMinecart) { return false; }
            if (user.CurrentRoom != null && user.CurrentRoom.IsSealed) { return false; }
            // if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.RESOURCEFUL_RAT) { return false; }
            return true;
        }

        public override bool CanBeUsed(PlayerController user) { return (IsUsableRightNow(user) && base.CanBeUsed(user)); }

        protected override void DoEffect(PlayerController user) {
            // AkSoundEngine.PostEvent("Play_BOSS_bulletbros_anger_01", gameObject);
            m_InUse = true;
            GameManager.Instance.StartCoroutine(TentacleTime(user));
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

        public IEnumerator TentacleTime(PlayerController user) {
            RoomHandler currentRoom = user.CurrentRoom;
           
            if (currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { StunEnemiesForTeleport(currentRoom, 4); }
            AkSoundEngine.PostEvent("Play_OBJ_lock_pick_01", gameObject);
            user.ForceStopDodgeRoll();
            user.healthHaver.IsVulnerable = false;
            
            yield return new WaitForSeconds(0.1f);
                        
            Dungeon dungeon = GameManager.Instance.Dungeon;

            // MainRoomlist
            // RewardRoomList
            // NPCRoomList
            // SecretRoomList
            // ShrineRoomList

            PrototypeDungeonRoom SelectedPrototypeDungeonRoom = null;

            float RoomSelectionSeed = UnityEngine.Random.value;

            if (RoomSelectionSeed <= 0.1f) {
                SelectedPrototypeDungeonRoom = BraveUtility.RandomElement(ExitElevatorRoomList);
            } else if (RoomSelectionSeed <= 0.2f) {
                SelectedPrototypeDungeonRoom = BraveUtility.RandomElement(RewardRoomList);
            } else if (RoomSelectionSeed <= 0.45f) {
                List<PrototypeDungeonRoom> m_SpecialRooms = new List<PrototypeDungeonRoom>();

                m_SpecialRooms.Add(BraveUtility.RandomElement(NPCRoomList));
                m_SpecialRooms.Add(BraveUtility.RandomElement(SecretRoomList));
                m_SpecialRooms.Add(BraveUtility.RandomElement(ShrineRoomList));

                SelectedPrototypeDungeonRoom = BraveUtility.RandomElement(m_SpecialRooms);
            } else {
                SelectedPrototypeDungeonRoom = BraveUtility.RandomElement(MainRoomlist);
            }
                        
            if (SelectedPrototypeDungeonRoom == null) {
                /*yield return new WaitForSeconds(0.4f);
                DoTentacleVFX(user);
                yield return new WaitForSeconds(0.45f);
                user.IsVisible = true;
                user.healthHaver.IsVulnerable = true;
                user.ClearAllInputOverrides();*/
                AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", gameObject);
                user.healthHaver.IsVulnerable = true;
                yield break;
            }
            

            /*if (SelectedPrototypeDungeonRoom.category == PrototypeDungeonRoom.RoomCategory.SECRET) {
                SelectedPrototypeDungeonRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            }*/

            RoomHandler GlitchRoom = ExpandUtility.Instance.AddCustomRuntimeRoom(SelectedPrototypeDungeonRoom);

            GlitchRoom.area.PrototypeRoomName = ("Corrupted " + GlitchRoom.GetRoomName());

            ExpandPlaceCorruptTiles corruptedTilePlacer = new ExpandPlaceCorruptTiles();
            corruptedTilePlacer.PlaceCorruptTiles(dungeon, GlitchRoom, null, true, true);

            // Spawn Rainbow chest. This room doesn't spawn NPC it seems.(unless player hasn't unlocked it yet? Not likely. Most would have unlocked this one by now)
            /*if (GlitchRoom.GetRoomName().ToLower().EndsWith("earlymetashopcell")) {
                IntVector2 SpecialChestLocation = new IntVector2(10, 14);
                WeightedGameObject wChestObject = new WeightedGameObject();
                Chest RainbowChest = GameManager.Instance.RewardManager.Rainbow_Chest;
                wChestObject.rawGameObject = RainbowChest.gameObject;
                WeightedGameObjectCollection wChestObjectCollection = new WeightedGameObjectCollection();
                wChestObjectCollection.Add(wChestObject);
                Chest PlacableChest = GlitchRoom.SpawnRoomRewardChest(wChestObjectCollection, (SpecialChestLocation + GlitchRoom.area.basePosition));
            }*/

            
            // user.EscapeRoom(PlayerController.EscapeSealedRoomStyle.TELEPORTER, true, GlitchRoom);
            TeleportToRoom(user, GlitchRoom);
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
            m_InUse = false;
            corruptedTilePlacer = null;
            yield break;
        }

        public void TeleportToRoom(PlayerController targetPlayer, RoomHandler targetRoom) {
            m_IsTeleporting = true;
            // if (targetPlayer.m_isStartingTeleport) { return; }
            // targetPlayer.m_isStartingTeleport = true;
            IntVector2? randomAvailableCell = targetRoom.GetRandomAvailableCell(new IntVector2?(new IntVector2(2, 2)), new CellTypes?(CellTypes.FLOOR), false, null);
            if (!randomAvailableCell.HasValue) {
                m_IsTeleporting = false;
                return;
            }
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(targetPlayer);
                if (otherPlayer) { TeleportToRoom(otherPlayer, targetRoom); }
            }
            // targetPlayer.m_isStartingTeleport = false;
            targetPlayer.DoVibration(Vibration.Time.Normal, Vibration.Strength.Medium);            
            GameManager.Instance.StartCoroutine(HandleTeleportToRoom(targetPlayer, randomAvailableCell.Value.ToCenterVector2()));
            targetPlayer.specRigidbody.Velocity = Vector2.zero;
            targetPlayer.knockbackDoer.TriggerTemporaryKnockbackInvulnerability(1f);
            targetRoom.EnsureUpstreamLocksUnlocked();
            // GameManager.Instance.StartCoroutine(DelayedRoomReset(targetPlayer, targetPlayer.CurrentRoom));
        }

        private IEnumerator HandleTeleportToRoom(PlayerController targetPlayer, Vector2 targetPoint) {
            if (targetPlayer.transform.position.GetAbsoluteRoom() != null) { StunEnemiesForTeleport(targetPlayer.transform.position.GetAbsoluteRoom(), 1f); }
            targetPlayer.healthHaver.IsVulnerable = false;
            CameraController cameraController = GameManager.Instance.MainCameraController;
            Vector2 offsetVector = (cameraController.transform.position - targetPlayer.transform.position);
            offsetVector -= cameraController.GetAimContribution();
            Minimap.Instance.ToggleMinimap(false, false);
            cameraController.SetManualControl(true, false);
            cameraController.OverridePosition = cameraController.transform.position;
            targetPlayer.CurrentInputState = PlayerInputState.NoInput;
            yield return new WaitForSeconds(0.1f);
            DoTentacleVFX(targetPlayer);
            // yield return new WaitForSeconds(0.4f);
            yield return new WaitForSeconds(1);
            targetPlayer.ToggleRenderer(false, "arbitrary teleporter");
            targetPlayer.ToggleGunRenderers(false, "arbitrary teleporter");
            targetPlayer.ToggleHandRenderers(false, "arbitrary teleporter");
            yield return new WaitForSeconds(1);
            Pixelator.Instance.FadeToBlack(0.15f, false, 0f);
            yield return new WaitForSeconds(0.15f);
            // targetPlayer.specRigidbody.Position = new Position(targetPoint);
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
            Pixelator.Instance.FadeToBlack(0.15f, true, 0f);
            yield return null;
            if (CombatManager) { CombatManager.Activated = true; }
            cameraController.SetManualControl(false, true);
            // yield return new WaitForSeconds(0.75f);
            yield return new WaitForSeconds(0.15f);
            DoTentacleVFX(targetPlayer);
            targetPlayer.DoVibration(Vibration.Time.Normal, Vibration.Strength.Medium);
            // yield return new WaitForSeconds(0.25f);
            yield return new WaitForSeconds(1.7f);
            targetPlayer.ToggleRenderer(true, "arbitrary teleporter");
            targetPlayer.ToggleGunRenderers(true, "arbitrary teleporter");
            targetPlayer.ToggleHandRenderers(true, "arbitrary teleporter");
            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(targetPlayer.specRigidbody, null, false);
            targetPlayer.CurrentInputState = PlayerInputState.AllInput;
            targetPlayer.healthHaver.IsVulnerable = true;
            m_IsTeleporting = false;
            yield break;
        }
        
        /*private IEnumerator DelayedRoomReset(PlayerController targetPlayer, RoomHandler targetRoom) {
            if (targetRoom == null | targetPlayer.CurrentRoom == null) { yield break; }
            while (targetPlayer.CurrentRoom == targetRoom) { yield return null; }
            yield return null;
            if (targetRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) || !targetRoom.EverHadEnemies || GameManager.Instance.InTutorial) {
                targetRoom.ResetPredefinedRoomLikeDarkSouls();
            }
            if (!targetRoom.EverHadEnemies) { targetRoom.forceTeleportersActive = true; }
            ReadOnlyCollection<Projectile> allProjectiles = StaticReferenceManager.AllProjectiles;
            for (int i = allProjectiles.Count - 1; i >= 0; i--) {
                if (allProjectiles[i]) { allProjectiles[i].DieInAir(false, true, true, false); }
            }
            yield break;
        }*/

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
                        }
                        return;
                    } else if (ParentRoom.CompletelyPreventLeaving) {
                        ParentRoom.CompletelyPreventLeaving = false;
                        ParentRoom.UnsealRoom();
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

