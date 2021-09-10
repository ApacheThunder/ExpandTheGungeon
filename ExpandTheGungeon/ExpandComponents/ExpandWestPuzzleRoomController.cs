﻿using System;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandObjects;
using System.Collections;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandWestPuzzleRoomController : BraveBehaviour, IPlaceConfigurable {

        public ExpandWestPuzzleRoomController() { }

        private enum PuzzleType {
            None,
            BuildFakeWall,
            HideKeyOnRandomEnemy,
            HideKeyOnTable,
            PlaceWinchesterSign,
            PlaceChestPuzzle,
            SetupRatBoss,
            PlaceHubSign
        }

        private PuzzleType m_SelectedType;

        private RoomHandler m_ParentRoom;

        private void Start() {
            if (m_SelectedType == PuzzleType.BuildFakeWall) {
                HandleBuildFakeWall();
            } else if (m_SelectedType == PuzzleType.HideKeyOnRandomEnemy) {
                HandleHideKeyOnEnemy();
            } else if (m_SelectedType == PuzzleType.HideKeyOnTable) {
                HandleHideKeyOnTable();
            } else if (m_SelectedType == PuzzleType.PlaceWinchesterSign) {
                HandleWinchesterRoomSetup();
            } else if (m_SelectedType == PuzzleType.PlaceChestPuzzle) {
                HandleChestRoomSetup();
            } else if (m_SelectedType == PuzzleType.SetupRatBoss) {
                HandleRatBossSetup();
            } else if (m_SelectedType == PuzzleType.PlaceHubSign) {
                HandlePlaceHubSign();
            }
            Destroy(gameObject);
            return;
        }
                
        private void HandleHideKeyOnEnemy() {
            List<AIActor> PuzzleEnemyList = new List<AIActor>();

            if (m_ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.All)) {
                PuzzleEnemyList = m_ParentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
            }

            if (PuzzleEnemyList.Count > 0) {
                AIActor selectedActor = BraveUtility.RandomElement(PuzzleEnemyList);
                ExpandShaders.Instance.ApplyGlitchShader(selectedActor.GetComponentInChildren<tk2dBaseSprite>());
                selectedActor.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>();
                selectedActor.CanDropItems = true;
                selectedActor.AdditionalSimpleItemDrops = new List<PickupObject> { PickupObjectDatabase.GetById(727) };
            }
        }

        private void HandleHideKeyOnTable() {
            IntVector2 GlitchedTable1Position = new IntVector2(9, 10);
            IntVector2 GlitchedTable2Position = new IntVector2(9, 8);
            GameObject GlitchedVerticalTable1 = ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, false, true).InstantiateObject(m_ParentRoom, GlitchedTable1Position);
            GameObject GlitchedVerticalTable2 = ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, false, true).InstantiateObject(m_ParentRoom, GlitchedTable2Position);
            GlitchedVerticalTable1.transform.SetParent(m_ParentRoom.hierarchyParent, true);
            GlitchedVerticalTable2.transform.SetParent(m_ParentRoom.hierarchyParent, true);

            GlitchedVerticalTable1.AddComponent<ExpandKickableObject>();
            GlitchedVerticalTable2.AddComponent<ExpandKickableObject>();

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.2f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RandomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.22f);

            if (BraveUtility.RandomBool()) {
                ExpandKickableObject GlitchedTable1Component = GlitchedVerticalTable1.GetComponent<ExpandKickableObject>();
                GlitchedTable1Component.SpawnedObject = PickupObjectDatabase.GetById(727).gameObject;
                GlitchedTable1Component.willDefinitelyExplode = true;
                GlitchedTable1Component.spawnObjectOnSelfDestruct = true;
                ExpandShaders.Instance.ApplyGlitchShader(GlitchedTable1Component.GetComponentInChildren<tk2dBaseSprite>(), true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
            } else {
                ExpandKickableObject GlitchedTable1Component = GlitchedVerticalTable2.GetComponent<ExpandKickableObject>();
                GlitchedTable1Component.SpawnedObject = PickupObjectDatabase.GetById(727).gameObject;
                GlitchedTable1Component.willDefinitelyExplode = true;
                GlitchedTable1Component.spawnObjectOnSelfDestruct = true;
                ExpandShaders.Instance.ApplyGlitchShader(GlitchedTable1Component.GetComponentInChildren<tk2dBaseSprite>(), true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
            }
            m_ParentRoom.RegisterInteractable(GlitchedVerticalTable1.GetComponentInChildren<FlippableCover>());
            m_ParentRoom.RegisterInteractable(GlitchedVerticalTable2.GetComponentInChildren<FlippableCover>());
            m_ParentRoom.RegisterInteractable(GlitchedVerticalTable1.GetComponent<ExpandKickableObject>());
            m_ParentRoom.RegisterInteractable(GlitchedVerticalTable2.GetComponent<ExpandKickableObject>());
        }

        private void HandleBuildFakeWall() {
            Vector3 SecretKeyPosition = (new Vector3(14, 21) + m_ParentRoom.area.basePosition.ToVector3());
            GameObject SecretKeyPedestal = Instantiate(ExpandPrefabs.RatKeyRewardPedestal, SecretKeyPosition, Quaternion.identity);
            SecretKeyPedestal.transform.SetParent(m_ParentRoom.hierarchyParent, true);

            IntVector2 wallPosition = new IntVector2(14, 20);
            SecretKeyPedestal.transform.localScale -= new Vector3(0.7f, 0.7f);
            ExpandUtility.GenerateFakeWall(DungeonData.Direction.SOUTH, new IntVector2(14, 20), m_ParentRoom, markAsSecret: true);
        }

        private void HandleRatBossSetup() {
            GameObject SpecialRatBoss = DungeonPlaceableUtility.InstantiateDungeonPlaceable(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").gameObject, m_ParentRoom, new IntVector2(17, 28), true, AIActor.AwakenAnimationType.Awaken, true);
            AIActor RatBossAIActor = SpecialRatBoss.GetComponent<AIActor>();
            if (RatBossAIActor != null) {
                PickupObject.ItemQuality targetQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.C : PickupObject.ItemQuality.D) : PickupObject.ItemQuality.B;
                GenericLootTable lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
                PickupObject item = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
                PickupObject item2 = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
                Destroy(RatBossAIActor.gameObject.GetComponent<ResourcefulRatDeathController>());
                Destroy(RatBossAIActor.gameObject.GetComponent<ResourcefulRatRewardRoomController>());
                RatBossAIActor.State = AIActor.ActorState.Awakening;
                RatBossAIActor.StealthDeath = true;
                RatBossAIActor.healthHaver.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>();
                ExpandSpawnGlitchObjectOnDeath ObjectSpawnerComponent = RatBossAIActor.healthHaver.gameObject.GetComponent<ExpandSpawnGlitchObjectOnDeath>();
                ObjectSpawnerComponent.spawnRatCorpse = true;
                ObjectSpawnerComponent.ratCorpseSpawnsKey = true;
                ObjectSpawnerComponent.parentEnemyWasRat = true;
                if (item && item2) { RatBossAIActor.AdditionalSafeItemDrops = new List<PickupObject> { item, item2 }; }
                RatBossAIActor.healthHaver.enabled = true;
                RatBossAIActor.healthHaver.forcePreventVictoryMusic = true;
                RatBossAIActor.ConfigureOnPlacement(m_ParentRoom);
                RatBossAIActor.specRigidbody.CollideWithOthers = true;
            }
        }

        private void HandleChestRoomSetup() {
            AssetBundle sharedAssets1 = ResourceManager.LoadAssetBundle("shared_auto_001");
            AssetBundle sharedAssets2 = ResourceManager.LoadAssetBundle("shared_auto_002");

            try { 
                DungeonPlaceable ChestPlatform = sharedAssets2.LoadAsset<DungeonPlaceable>("Treasure_Dais_Stone_Carpet");
                GameObject Chest_Black = sharedAssets1.LoadAsset<GameObject>("Chest_Black");
                GameObject Chest_Rainbow = sharedAssets1.LoadAsset<GameObject>("Chest_Rainbow");
                GameObject Chest_Rat = sharedAssets1.LoadAsset<GameObject>("Chest_Rat");

                IntVector2 TreasureChestCarpetPosition1 = new IntVector2(8, 29);
                IntVector2 TreasureChestCarpetPosition2 = new IntVector2(8, 54);
                IntVector2 SecretChestPosition1 = new IntVector2(8, 31);
                IntVector2 SecretChestPosition2 = new IntVector2(8, 56);
                GameObject TreasureChestStoneCarpet1 = ChestPlatform.InstantiateObject(m_ParentRoom, TreasureChestCarpetPosition1);
                GameObject TreasureChestStoneCarpet2 = ChestPlatform.InstantiateObject(m_ParentRoom, TreasureChestCarpetPosition2);
                TreasureChestStoneCarpet1.transform.position -= new Vector3(0.55f, 0);
                TreasureChestStoneCarpet2.transform.position -= new Vector3(0.55f, 0);
                TreasureChestStoneCarpet1.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                TreasureChestStoneCarpet2.transform.SetParent(m_ParentRoom.hierarchyParent, true);

                GameObject PlacedBlackChestObject = ExpandUtility.GenerateDungeonPlacable(Chest_Black, false, true).InstantiateObject(m_ParentRoom, SecretChestPosition1);
                GameObject PlacedRainbowChestObject = ExpandUtility.GenerateDungeonPlacable(Chest_Rainbow, false, true).InstantiateObject(m_ParentRoom, SecretChestPosition2);
                PlacedBlackChestObject.transform.position += new Vector3(0.5f, 0);
                PlacedRainbowChestObject.transform.position += new Vector3(0.5f, 0);
                TreasureChestStoneCarpet1.transform.position += new Vector3(0.5f, 0);
                TreasureChestStoneCarpet2.transform.position += new Vector3(0.5f, 0);
                PlacedBlackChestObject.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedRainbowChestObject.transform.SetParent(m_ParentRoom.hierarchyParent, true);

                tk2dBaseSprite PlacedBlackChestSprite = PlacedBlackChestObject.GetComponentInChildren<tk2dBaseSprite>();

                GenericLootTable BlackChestLootTable = GameManager.Instance.RewardManager.ItemsLootTable;

                Chest PlacedBlackChestComponent = PlacedBlackChestObject.GetComponent<Chest>();
                Chest PlacedRainbowChestComponent = PlacedRainbowChestObject.GetComponent<Chest>();
                PlacedBlackChestComponent.ChestType = Chest.GeneralChestType.ITEM;
                PlacedBlackChestComponent.lootTable.lootTable = BlackChestLootTable;
                bool LootTableCheck = PlacedBlackChestComponent.lootTable.canDropMultipleItems && PlacedBlackChestComponent.lootTable.overrideItemLootTables != null && PlacedBlackChestComponent.lootTable.overrideItemLootTables.Count > 0;
                if (LootTableCheck) { PlacedBlackChestComponent.lootTable.overrideItemLootTables[0] = BlackChestLootTable; }
                PlacedBlackChestComponent.overrideMimicChance = 0f;
                PlacedBlackChestComponent.ForceUnlock();
                PlacedBlackChestComponent.PreventFuse = true;
                PlacedRainbowChestComponent.ForceUnlock();
                PlacedRainbowChestComponent.PreventFuse = true;
                m_ParentRoom.RegisterInteractable(PlacedBlackChestComponent);
                m_ParentRoom.RegisterInteractable(PlacedRainbowChestComponent);

                Vector3 SpecialLockedDoorPosition = (new Vector3(9, 52.25f) + m_ParentRoom.area.basePosition.ToVector3());
                GameObject SpecialLockedDoor = Instantiate(ExpandObjectDatabase.LockedJailDoor, SpecialLockedDoorPosition, Quaternion.identity);
                SpecialLockedDoor.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                InteractableLock SpecialLockedDoorComponent = SpecialLockedDoor.GetComponentInChildren<InteractableLock>();
                SpecialLockedDoorComponent.lockMode = InteractableLock.InteractableLockMode.RESOURCEFUL_RAT;
                SpecialLockedDoorComponent.JailCellKeyId = 0;
                tk2dBaseSprite RainbowLockSprite = SpecialLockedDoorComponent.GetComponentInChildren<tk2dBaseSprite>();
                if (RainbowLockSprite != null) { ExpandShaders.Instance.ApplyRainbowShader(RainbowLockSprite); }
                
                IntVector2 PuzzleChestPosition1 = new IntVector2(4, 19);
                IntVector2 PuzzleChestPosition2 = new IntVector2(12, 19);
                IntVector2 PuzzleChestPosition3 = new IntVector2(4, 40);
                IntVector2 PuzzleChestPosition4 = new IntVector2(12, 40);
                IntVector2 PuzzleChestPosition5 = new IntVector2(4, 50);
                IntVector2 PuzzleChestPosition6 = new IntVector2(12, 50);
                IntVector2 PuzzleChestCarpetPosition1 = (PuzzleChestPosition1 - new IntVector2(0, 1));
                IntVector2 PuzzleChestCarpetPosition2 = (PuzzleChestPosition2 - new IntVector2(0, 1));
                IntVector2 PuzzleChestCarpetPosition3 = (PuzzleChestPosition3 - new IntVector2(0, 1));
                IntVector2 PuzzleChestCarpetPosition4 = (PuzzleChestPosition4 - new IntVector2(0, 1));
                IntVector2 PuzzleChestCarpetPosition5 = (PuzzleChestPosition5 - new IntVector2(0, 1));
                IntVector2 PuzzleChestCarpetPosition6 = (PuzzleChestPosition6 - new IntVector2(0, 1));

                GameObject PlacedPuzzleRatChest1 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition1, false, true);
                GameObject PlacedPuzzleRatChest2 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition2, false, true);
                GameObject PlacedPuzzleRatChest3 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition3, false, true);
                GameObject PlacedPuzzleRatChest4 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition4, false, true);
                GameObject PlacedPuzzleRatChest5 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition5, false, true);
                GameObject PlacedPuzzleRatChest6 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition6, false, true);
                GameObject PuzzleChestStoneCarpet1 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition1);
                GameObject PuzzleChestStoneCarpet2 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition2);
                GameObject PuzzleChestStoneCarpet3 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition3);
                GameObject PuzzleChestStoneCarpet4 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition4);
                GameObject PuzzleChestStoneCarpet5 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition5);
                GameObject PuzzleChestStoneCarpet6 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition6);
                PlacedPuzzleRatChest1.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedPuzzleRatChest2.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedPuzzleRatChest3.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedPuzzleRatChest4.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedPuzzleRatChest5.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedPuzzleRatChest6.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet1.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet2.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet3.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet4.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet5.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet6.transform.SetParent(m_ParentRoom.hierarchyParent, true);

                Chest PuzzleRatChest1Component = PlacedPuzzleRatChest1.GetComponent<Chest>();
                Chest PuzzleRatChest2Component = PlacedPuzzleRatChest2.GetComponent<Chest>();
                Chest PuzzleRatChest3Component = PlacedPuzzleRatChest3.GetComponent<Chest>();
                Chest PuzzleRatChest4Component = PlacedPuzzleRatChest4.GetComponent<Chest>();
                Chest PuzzleRatChest5Component = PlacedPuzzleRatChest5.GetComponent<Chest>();
                Chest PuzzleRatChest6Component = PlacedPuzzleRatChest6.GetComponent<Chest>();
                PuzzleRatChest1Component.PreventFuse = true;
                PuzzleRatChest2Component.PreventFuse = true;
                PuzzleRatChest3Component.PreventFuse = true;
                PuzzleRatChest4Component.PreventFuse = true;
                PuzzleRatChest5Component.PreventFuse = true;
                PuzzleRatChest6Component.PreventFuse = true;
                PuzzleRatChest1Component.overrideMimicChance = 0f;
                PuzzleRatChest2Component.overrideMimicChance = 0f;
                PuzzleRatChest3Component.overrideMimicChance = 0f;
                PuzzleRatChest4Component.overrideMimicChance = 0f;
                PuzzleRatChest5Component.overrideMimicChance = 0f;
                PuzzleRatChest6Component.overrideMimicChance = 0f;

                float Seed = UnityEngine.Random.value;

                if (Seed <= 0.5f) {
                    PuzzleRatChest1Component.forceContentIds = new List<int> { 68 };
                    PuzzleRatChest2Component.forceContentIds = new List<int> { 727, 727 };
                } else {
                    PuzzleRatChest1Component.forceContentIds = new List<int> { 727, 727 };
                    PuzzleRatChest2Component.forceContentIds = new List<int> { 68 };
                }
                if (BraveUtility.RandomBool()) {
                    PuzzleRatChest3Component.forceContentIds = new List<int> { 70, 70, 70, 70 };
                    PuzzleRatChest4Component.forceContentIds = new List<int> { 727, 727 };
                } else {
                    PuzzleRatChest3Component.forceContentIds = new List<int> { 727, 727 };
                    PuzzleRatChest4Component.forceContentIds = new List<int> { 70, 70, 70, 70 };
                }
                if (BraveUtility.RandomBool()) {
                    PuzzleRatChest5Component.forceContentIds = new List<int> { 74 };
                    PuzzleRatChest6Component.forceContentIds = new List<int> { 316 };
                } else {
                    PuzzleRatChest5Component.forceContentIds = new List<int> { 316 };
                    PuzzleRatChest6Component.forceContentIds = new List<int> { 74 };
                }

                PuzzleRatChest1Component.ConfigureOnPlacement(m_ParentRoom);
                PuzzleRatChest2Component.ConfigureOnPlacement(m_ParentRoom);
                PuzzleRatChest3Component.ConfigureOnPlacement(m_ParentRoom);
                PuzzleRatChest4Component.ConfigureOnPlacement(m_ParentRoom);
                PuzzleRatChest5Component.ConfigureOnPlacement(m_ParentRoom);
                PuzzleRatChest6Component.ConfigureOnPlacement(m_ParentRoom);
                m_ParentRoom.RegisterInteractable(PuzzleRatChest1Component);
                m_ParentRoom.RegisterInteractable(PuzzleRatChest2Component);
                m_ParentRoom.RegisterInteractable(PuzzleRatChest3Component);
                m_ParentRoom.RegisterInteractable(PuzzleRatChest4Component);
                m_ParentRoom.RegisterInteractable(PuzzleRatChest5Component);
                m_ParentRoom.RegisterInteractable(PuzzleRatChest6Component);

                Vector3 InfoSignPosition = (new Vector3(6, 4) + m_ParentRoom.area.basePosition.ToVector3());

                GameObject ChestPuzzleInfoSign = Instantiate(ExpandPrefabs.Jungle_BlobLostSign, InfoSignPosition, Quaternion.identity);
                ChestPuzzleInfoSign.name = "Lunk's Minigame Sign";
                ChestPuzzleInfoSign.GetComponent<ExpandNoteDoer>().stringKey = "A minigame Lunk created based on a game he used to play in a land far away.\nGuess the right chest to continue forward.\n If you can guess the correct chest 3 times, the ultimate prize shall be gained!";
                Destroy(ChestPuzzleInfoSign.GetComponent<SpeculativeRigidbody>());
                Destroy(ChestPuzzleInfoSign.GetComponent<MajorBreakable>());
                m_ParentRoom.RegisterInteractable(ChestPuzzleInfoSign.GetComponent<ExpandNoteDoer>());
            } catch (Exception ex) {
                if (ExpandStats.debugMode) {
                    string Message = "[ExpandTheGungeon] Warning: Exception caught in ExpandWestPuzzleRoomController.HandleChestRoomSetup!";
                    ETGModConsole.Log(Message);
                    Debug.Log(Message);
                    Debug.LogException(ex);
                }
            }
            sharedAssets1 = null;
            sharedAssets2 = null;
        }

        private void HandleWinchesterRoomSetup() {
            RoomHandler WinchesterRoom = null;

            foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms) {
                if (!string.IsNullOrEmpty(room.GetRoomName()) && room.GetRoomName().StartsWith("WinchesterRoom")) {
                    WinchesterRoom = room;
                    break;
                }
            }

            if (WinchesterRoom != null) {
                WinchesterRoom.ForcePitfallForFliers = true;
                WinchesterRoom.TargetPitfallRoom = WinchesterRoom;

                IntVector2 WinchesterNotePosition = new IntVector2(3, 3);

                TalkDoerLite[] m_NPCs = FindObjectsOfType<TalkDoerLite>();

                if (m_NPCs != null && m_NPCs.Length > 0) {
                    foreach (TalkDoerLite npc in m_NPCs) {
                        if (npc.gameObject.transform.parent == WinchesterRoom.hierarchyParent) {
                            WinchesterNotePosition = (npc.transform.PositionVector2().ToIntVector2() - WinchesterRoom.area.basePosition - new IntVector2(1, 0));
                            break;
                        }
                    }
                }

                Vector3 InfoSignPosition = (new Vector3(3, 3) + WinchesterRoom.area.basePosition.ToVector3());

                GameObject PlacedWinchesterNote = Instantiate(ExpandPrefabs.Jungle_BlobLostSign, InfoSignPosition, Quaternion.identity);
                PlacedWinchesterNote.name = "Winchester's Sign";
                PlacedWinchesterNote.GetComponent<ExpandNoteDoer>().stringKey = "Notice: Anti-Flight Pits have been installed.\n I know you've been using fancy wings or that jetpack to cheat at my game!\n I'd like to see you try that again! [Winchester].";
                WinchesterRoom.RegisterInteractable(PlacedWinchesterNote.GetComponent<ExpandNoteDoer>());
                PlacedWinchesterNote.transform.SetParent(WinchesterRoom.hierarchyParent, true);
            }

            if (GameManager.Instance.PrimaryPlayer) {
                StartCoroutine(HandleKeyInventoryCheck(GameManager.Instance.PrimaryPlayer));
            }
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER && GameManager.Instance.SecondaryPlayer) {
                StartCoroutine(HandleKeyInventoryCheck(GameManager.Instance.SecondaryPlayer));
            }

        }

        private IEnumerator HandleKeyInventoryCheck(PlayerController player) {
            /*if (Dungeon.IsGenerating | GameManager.Instance.IsLoadingLevel) {
                while (Dungeon.IsGenerating | GameManager.Instance.IsLoadingLevel) { yield return null; }
            }*/

            bool hadKeys = false;
            if (player.HasPickupID(316)) {
                hadKeys = true;
                while (player.HasPickupID(316)) {
                    player.RemovePassiveItem(316);
                    if (!player.HasPickupID(316)) { break; }
                    yield return null;
                }
            }
            if (player.carriedConsumables != null && player.carriedConsumables.ResourcefulRatKeys > 0) {
                hadKeys = true;
                player.carriedConsumables.ResourcefulRatKeys = 0;
            }
            yield return null;
            if (hadKeys) { GameUIRoot.Instance.UpdatePlayerConsumables(player.carriedConsumables); }
            yield break;
        }

        private void HandlePlaceHubSign() {
            Vector3 InfoSignPosition = (new Vector3(3, m_ParentRoom.area.dimensions.y - 2) + m_ParentRoom.area.basePosition.ToVector3());
            
            GameObject PlacedLunkNote = Instantiate(ExpandPrefabs.Jungle_BlobLostSign, InfoSignPosition, Quaternion.identity);
            PlacedLunkNote.name = "Lunk's Dungeon Sign";
            PlacedLunkNote.GetComponent<ExpandNoteDoer>().stringKey = "A mini dungeon strung together by Lunk based on previous Dungeons he had encountered.\nFind the keys to gain access to the final puzzle.";
            m_ParentRoom.RegisterInteractable(PlacedLunkNote.GetComponent<ExpandNoteDoer>());
            PlacedLunkNote.transform.SetParent(m_ParentRoom.hierarchyParent, true);
        }
        

        public void ConfigureOnPlacement(RoomHandler room) {

            if (room == null | string.IsNullOrEmpty(room.GetRoomName())) {
                m_SelectedType = PuzzleType.None;
                return;
            }

            m_ParentRoom = room;

            if (room.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.PuzzleRoom1.name.ToLower())) {
                m_SelectedType = PuzzleType.HideKeyOnRandomEnemy;
            } else if (room.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.PuzzleRoom2.name.ToLower())) {
                m_SelectedType = PuzzleType.HideKeyOnTable;
            } else if (room.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.PuzzleRoom3.name.ToLower())) {
                m_SelectedType = PuzzleType.BuildFakeWall;
            } else if (room.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.SecretRewardRoom.name.ToLower())) {
                m_SelectedType = PuzzleType.PlaceChestPuzzle;
            } else if (room.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.SecretBossRoom.name.ToLower())) {
                m_SelectedType = PuzzleType.SetupRatBoss;
            } else if (room.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.Expand_West_Entrance.name.ToLower())) {
                m_SelectedType = PuzzleType.PlaceWinchesterSign;
            } else if (room.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.Expand_West_SecretHub2.name.ToLower())) {
                m_SelectedType = PuzzleType.PlaceHubSign;
            } else {
                m_SelectedType = PuzzleType.None;
            }
        }

        private void Update() { }
        private void LateUpdate() { }

        protected override void OnDestroy() { base.OnDestroy(); }
    }    
}

