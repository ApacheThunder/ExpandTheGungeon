using System;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandPrefab;
using System.Collections;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandWestPuzzleRoomController : BraveBehaviour, IPlaceConfigurable {

        public ExpandWestPuzzleRoomController() { m_SelectedType = PuzzleType.None; }

        private enum PuzzleType {
            None,
            BuildFakeWall,
            HideKeyOnRandomEnemy,
            HideKeyOnTable,
            PlaceChestPuzzle,
            PlaceWinchesterSign,
            PlaceHubSign
        }

        private PuzzleType m_SelectedType;

        private RoomHandler m_ParentRoom;

        private void Start() {
            switch (m_SelectedType) {
                case PuzzleType.BuildFakeWall:
                    HandleBuildFakeWall();
                    break;
                case PuzzleType.HideKeyOnRandomEnemy:
                    HandleHideKeyOnEnemy();
                    break;
                case PuzzleType.HideKeyOnTable:
                    HandleHideKeyOnTable();
                    break;
                case PuzzleType.PlaceChestPuzzle:
                    HandleChestRoomSetup();
                    break;
                case PuzzleType.PlaceWinchesterSign:
                    HandleWinchesterRoomSetup();
                    break;
                case PuzzleType.PlaceHubSign:
                    HandlePlaceHubSign();
                    break;
                case PuzzleType.None:
                    Destroy(gameObject);
                    return;
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
            try {
                DungeonPlaceable ChestPlatform = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Treasure_Dais_Stone_Carpet", ExpandAssets.AssetSource.SharedAuto2);
                GameObject Chest_Rainbow = ExpandAssets.LoadOfficialAsset<GameObject>("Chest_Rainbow", ExpandAssets.AssetSource.SharedAuto1);

                IntVector2 TreasureChestCarpetPosition1 = new IntVector2(8, 29);
                IntVector2 TreasureChestCarpetPosition2 = new IntVector2(8, 56);
                IntVector2 SecretChestPosition1 = new IntVector2(9, 31);
                IntVector2 SecretChestPosition2 = new IntVector2(8, 58);
                GameObject TreasureChestStoneCarpet1 = ChestPlatform.InstantiateObject(m_ParentRoom, TreasureChestCarpetPosition1);
                GameObject TreasureChestStoneCarpet2 = ChestPlatform.InstantiateObject(m_ParentRoom, TreasureChestCarpetPosition2);
                TreasureChestStoneCarpet1.transform.position -= new Vector3(0.55f, 0);
                TreasureChestStoneCarpet2.transform.position -= new Vector3(0.55f, 0);
                TreasureChestStoneCarpet1.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                TreasureChestStoneCarpet2.transform.SetParent(m_ParentRoom.hierarchyParent, true);

                GameObject PlacedNormalWestChestObject = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EX_Chest_West, false, true).InstantiateObject(m_ParentRoom, SecretChestPosition1);
                GameObject PlacedRainbowChestObject = ExpandUtility.GenerateDungeonPlacable(Chest_Rainbow, false, true).InstantiateObject(m_ParentRoom, SecretChestPosition2);
                // PlacedNormalWestChestObject.transform.position += new Vector3(0.5f, 0);
                PlacedRainbowChestObject.transform.position += new Vector3(0.5f, 0);
                TreasureChestStoneCarpet1.transform.position += new Vector3(0.5f, 0);
                TreasureChestStoneCarpet2.transform.position += new Vector3(0.5f, 0);
                PlacedNormalWestChestObject.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedRainbowChestObject.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                
                GenericLootTable BlackChestLootTable = GameManager.Instance.RewardManager.ItemsLootTable;

                Chest PlacedNormalWestComponent = PlacedNormalWestChestObject.GetComponent<Chest>();
                Chest PlacedRainbowChestComponent = PlacedRainbowChestObject.GetComponent<Chest>();
                PlacedNormalWestComponent.lootTable.lootTable = BlackChestLootTable;
                bool LootTableCheck = PlacedNormalWestComponent.lootTable.canDropMultipleItems && PlacedNormalWestComponent.lootTable.overrideItemLootTables != null && PlacedNormalWestComponent.lootTable.overrideItemLootTables.Count > 0;
                if (LootTableCheck) { PlacedNormalWestComponent.lootTable.overrideItemLootTables[0] = BlackChestLootTable; }
                PlacedNormalWestComponent.overrideMimicChance = 0f;
                PlacedNormalWestComponent.ForceUnlock();
                PlacedNormalWestComponent.PreventFuse = true;
                PlacedRainbowChestComponent.ForceUnlock();
                PlacedRainbowChestComponent.PreventFuse = true;
                m_ParentRoom.RegisterInteractable(PlacedNormalWestComponent);
                m_ParentRoom.RegisterInteractable(PlacedRainbowChestComponent);

                Vector3 SpecialLockedDoorPosition = (new Vector3(9, 52.25f) + m_ParentRoom.area.basePosition.ToVector3());
                GameObject SpecialLockedDoor = Instantiate(ExpandObjectDatabase.LockedJailDoor, SpecialLockedDoorPosition, Quaternion.identity);
                SpecialLockedDoor.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                InteractableLock SpecialLockedDoorComponent = SpecialLockedDoor.GetComponentInChildren<InteractableLock>();
                SpecialLockedDoorComponent.lockMode = InteractableLock.InteractableLockMode.RESOURCEFUL_RAT;
                SpecialLockedDoorComponent.JailCellKeyId = 0;
                tk2dBaseSprite RainbowLockSprite = SpecialLockedDoorComponent.GetComponentInChildren<tk2dBaseSprite>();
                if (RainbowLockSprite != null) { ExpandShaders.Instance.ApplyRainbowShader(RainbowLockSprite); }
                
                IntVector2 PuzzleChestPosition1 = new IntVector2(5, 20);
                IntVector2 PuzzleChestPosition2 = new IntVector2(13, 20);
                IntVector2 PuzzleChestPosition3 = new IntVector2(5, 41);
                IntVector2 PuzzleChestPosition4 = new IntVector2(13, 41);
                IntVector2 PuzzleChestPosition5 = new IntVector2(5, 51);
                IntVector2 PuzzleChestPosition6 = new IntVector2(13, 51);
                IntVector2 PuzzleChestCarpetPosition1 = (PuzzleChestPosition1 - new IntVector2(1, 2));
                IntVector2 PuzzleChestCarpetPosition2 = (PuzzleChestPosition2 - new IntVector2(1, 2));
                IntVector2 PuzzleChestCarpetPosition3 = (PuzzleChestPosition3 - new IntVector2(1, 2));
                IntVector2 PuzzleChestCarpetPosition4 = (PuzzleChestPosition4 - new IntVector2(1, 2));
                IntVector2 PuzzleChestCarpetPosition5 = (PuzzleChestPosition5 - new IntVector2(1, 2));
                IntVector2 PuzzleChestCarpetPosition6 = (PuzzleChestPosition6 - new IntVector2(1, 2));

                GameObject PlacedPuzzleWestChest1 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EX_Chest_West, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition1, false, true);
                GameObject PlacedPuzzleWestChest2 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EX_Chest_West, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition2, false, true);
                GameObject PlacedPuzzleWestChest3 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EX_Chest_West, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition3, false, true);
                GameObject PlacedPuzzleWestChest4 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EX_Chest_West, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition4, false, true);
                GameObject PlacedPuzzleWestChest5 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EX_Chest_West, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition5, false, true);
                GameObject PlacedPuzzleWestChest6 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EX_Chest_West, false, true).InstantiateObject(m_ParentRoom, PuzzleChestPosition6, false, true);
                GameObject PuzzleChestStoneCarpet1 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition1);
                GameObject PuzzleChestStoneCarpet2 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition2);
                GameObject PuzzleChestStoneCarpet3 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition3);
                GameObject PuzzleChestStoneCarpet4 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition4);
                GameObject PuzzleChestStoneCarpet5 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition5);
                GameObject PuzzleChestStoneCarpet6 = ChestPlatform.InstantiateObject(m_ParentRoom, PuzzleChestCarpetPosition6);
                PlacedPuzzleWestChest1.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedPuzzleWestChest2.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedPuzzleWestChest3.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedPuzzleWestChest4.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedPuzzleWestChest5.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PlacedPuzzleWestChest6.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet1.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet2.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet3.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet4.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet5.transform.SetParent(m_ParentRoom.hierarchyParent, true);
                PuzzleChestStoneCarpet6.transform.SetParent(m_ParentRoom.hierarchyParent, true);

                Chest PuzzleWestChest1Component = PlacedPuzzleWestChest1.GetComponent<Chest>();
                Chest PuzzleWestChest2Component = PlacedPuzzleWestChest2.GetComponent<Chest>();
                Chest PuzzleWestChest3Component = PlacedPuzzleWestChest3.GetComponent<Chest>();
                Chest PuzzleWestChest4Component = PlacedPuzzleWestChest4.GetComponent<Chest>();
                Chest PuzzleWestChest5Component = PlacedPuzzleWestChest5.GetComponent<Chest>();
                Chest PuzzleWestChest6Component = PlacedPuzzleWestChest6.GetComponent<Chest>();
                PuzzleWestChest1Component.PreventFuse = true;
                PuzzleWestChest1Component.ChestIdentifier = Chest.SpecialChestIdentifier.RAT;
                PuzzleWestChest1Component.IsLocked = true;
                PuzzleWestChest2Component.PreventFuse = true;
                PuzzleWestChest2Component.ChestIdentifier = Chest.SpecialChestIdentifier.RAT;
                PuzzleWestChest2Component.IsLocked = true;
                PuzzleWestChest3Component.PreventFuse = true;
                PuzzleWestChest3Component.IsLocked = true;
                PuzzleWestChest3Component.ChestIdentifier = Chest.SpecialChestIdentifier.RAT;
                PuzzleWestChest4Component.PreventFuse = true;
                PuzzleWestChest4Component.IsLocked = true;
                PuzzleWestChest4Component.ChestIdentifier = Chest.SpecialChestIdentifier.RAT;
                PuzzleWestChest5Component.PreventFuse = true;
                PuzzleWestChest5Component.IsLocked = true;
                PuzzleWestChest5Component.ChestIdentifier = Chest.SpecialChestIdentifier.RAT;
                PuzzleWestChest6Component.PreventFuse = true;
                PuzzleWestChest6Component.IsLocked = true;
                PuzzleWestChest6Component.ChestIdentifier = Chest.SpecialChestIdentifier.RAT;

                if (UnityEngine.Random.value < 0.5f) {
                    PuzzleWestChest1Component.forceContentIds = new List<int> { 68 };
                    PuzzleWestChest2Component.forceContentIds = new List<int> { 727, 727 };
                } else {
                    PuzzleWestChest1Component.forceContentIds = new List<int> { 727, 727 };
                    PuzzleWestChest2Component.forceContentIds = new List<int> { 68 };
                }
                if (UnityEngine.Random.value < 0.5f) {
                    PuzzleWestChest3Component.forceContentIds = new List<int> { 70, 70, 70, 70 };
                    PuzzleWestChest4Component.forceContentIds = new List<int> { 727, 727 };
                } else {
                    PuzzleWestChest3Component.forceContentIds = new List<int> { 727, 727 };
                    PuzzleWestChest4Component.forceContentIds = new List<int> { 70, 70, 70, 70 };
                }
                if (UnityEngine.Random.value < 0.5f) {
                    PuzzleWestChest5Component.forceContentIds = new List<int> { 74 };
                    PuzzleWestChest6Component.forceContentIds = new List<int> { 316 };
                } else {
                    PuzzleWestChest5Component.forceContentIds = new List<int> { 316 };
                    PuzzleWestChest6Component.forceContentIds = new List<int> { 74 };
                }

                PuzzleWestChest1Component.ConfigureOnPlacement(m_ParentRoom);
                PuzzleWestChest2Component.ConfigureOnPlacement(m_ParentRoom);
                PuzzleWestChest3Component.ConfigureOnPlacement(m_ParentRoom);
                PuzzleWestChest4Component.ConfigureOnPlacement(m_ParentRoom);
                PuzzleWestChest5Component.ConfigureOnPlacement(m_ParentRoom);
                PuzzleWestChest6Component.ConfigureOnPlacement(m_ParentRoom);
                m_ParentRoom.RegisterInteractable(PuzzleWestChest1Component);
                m_ParentRoom.RegisterInteractable(PuzzleWestChest2Component);
                m_ParentRoom.RegisterInteractable(PuzzleWestChest3Component);
                m_ParentRoom.RegisterInteractable(PuzzleWestChest4Component);
                m_ParentRoom.RegisterInteractable(PuzzleWestChest5Component);
                m_ParentRoom.RegisterInteractable(PuzzleWestChest6Component);

                Vector3 InfoSignPosition = (new Vector3(6, 8) + m_ParentRoom.area.basePosition.ToVector3());

                GameObject ChestPuzzleInfoSign = Instantiate(ExpandPrefabs.Jungle_BlobLostSign, InfoSignPosition, Quaternion.identity);
                ChestPuzzleInfoSign.name = "Lunk's Minigame Sign";
                ChestPuzzleInfoSign.GetComponent<ExpandNoteDoer>().stringKey = "A minigame Lunk created based on a game he used to play in a land far away.\nGuess the right chest to continue forward.\n If you can guess the correct chest 3 times, the ultimate prize shall be gained!";
                Destroy(ChestPuzzleInfoSign.GetComponent<SpeculativeRigidbody>());
                Destroy(ChestPuzzleInfoSign.GetComponent<MajorBreakable>());
                m_ParentRoom.RegisterInteractable(ChestPuzzleInfoSign.GetComponent<ExpandNoteDoer>());
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    string Message = "[ExpandTheGungeon] Warning: Exception caught in ExpandWestPuzzleRoomController.HandleChestRoomSetup!";
                    ETGModConsole.Log(Message);
                    Debug.Log(Message);
                    Debug.LogException(ex);
                }
            }
        }
        
        // This will be manually called externally from now on since this doen't seem to work as intended during floor generation
        // (aka Winchester NPC likely not placed at the time this code runs so this always places sign at default value)
        public void HandleWinchesterRoomSetup() {
            RoomHandler WinchesterRoom = null;

            foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms) {
                if (!string.IsNullOrEmpty(room.GetRoomName()) && room.GetRoomName().ToLower().StartsWith("winchesterroom")) {
                    WinchesterRoom = room;
                    break;
                }
            }

            if (WinchesterRoom != null) {
                WinchesterRoom.ForcePitfallForFliers = true;
                WinchesterRoom.TargetPitfallRoom = WinchesterRoom;

                IntVector2 WinchesterNotePosition = new IntVector2(3, 3);
                Vector3 NPCOffset = new Vector3(1, 0, 0);

                TalkDoerLite[] m_NPCs = FindObjectsOfType<TalkDoerLite>();

                if (m_NPCs != null && m_NPCs.Length > 0) {
                    foreach (TalkDoerLite npc in m_NPCs) {
                        if (npc.GetAbsoluteParentRoom() == WinchesterRoom) {
                            WinchesterNotePosition = (npc.transform.PositionVector2().ToIntVector2() - WinchesterRoom.area.basePosition - new IntVector2(1, 0));
                            break;
                        }
                    }
                }

                Vector3 InfoSignPosition = (new Vector3(3, 3) + WinchesterRoom.area.basePosition.ToVector3());
                
                if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_001")) {
                    InfoSignPosition = (new Vector3(14, 5) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_002")) {
                    InfoSignPosition = (new Vector3(14, 5) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_003")) {
                    InfoSignPosition = (new Vector3(12, 5) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_004")) {
                    InfoSignPosition = (new Vector3(10, 4) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_005")) {
                    InfoSignPosition = (new Vector3(10, 4) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_ag&d_001")) {
                    InfoSignPosition = (new Vector3(4, 15) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_ag&d_002")) {
                    InfoSignPosition = (new Vector3(23, 5) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_ag&d_003")) {
                    InfoSignPosition = (new Vector3(10, 18) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_ag&d_004")) {
                    InfoSignPosition = (new Vector3(12, 5) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_ag&d_005")) {
                    InfoSignPosition = (new Vector3(6, 11) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_joe_001")) {
                    InfoSignPosition = (new Vector3(6, 2) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_joe_002")) {
                    InfoSignPosition = (new Vector3(6, 2) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_joe_003")) {
                    InfoSignPosition = (new Vector3(5, 11) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_joe_004")) {
                    InfoSignPosition = (new Vector3(7, 2) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                } else if (WinchesterRoom.GetRoomName().ToLower().StartsWith("winchesterroom_joe_005")) {
                    InfoSignPosition = (new Vector3(20, 11) + WinchesterRoom.area.basePosition.ToVector3() - NPCOffset);
                }
                 
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

            if (m_ParentRoom.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.PuzzleRoom1.name.ToLower())) {
                m_SelectedType = PuzzleType.HideKeyOnRandomEnemy;
            } else if (m_ParentRoom.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.PuzzleRoom2.name.ToLower())) {
                m_SelectedType = PuzzleType.HideKeyOnTable;
            } else if (m_ParentRoom.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.PuzzleRoom3.name.ToLower())) {
                m_SelectedType = PuzzleType.BuildFakeWall;
            } else if (m_ParentRoom.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.SecretRewardRoom.name.ToLower())) {
                m_SelectedType = PuzzleType.PlaceChestPuzzle;
            } else if (m_ParentRoom.IsActuallyWildWestEntrance()) {
                m_SelectedType = PuzzleType.PlaceWinchesterSign;
            } else if (m_ParentRoom.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.Expand_West_SecretHub2.name.ToLower())) {
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

