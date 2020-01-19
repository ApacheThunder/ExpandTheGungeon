using Dungeonator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class secretglitchfloor_flow : ExpandDungeonFlow {
        
        public static DungeonFlow SecretGlitchFloor_Flow() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            DungeonFlowNode m_FakeBossFoyerNode = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.SPECIAL,
                percentChance = 1,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                // overrideExactRoom = ExpandPrefabs.boss_foyer,
                overrideRoomTable = ExpandPrefabs.boss_foyertable,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetIsOneWay = true,
                guidAsString = Guid.NewGuid().ToString()
            };

            m_CachedFlow.name = "SecretGlitchFloor_Flow";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.CustomRoomTableSecretGlitchFloor;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>(0);
            m_CachedFlow.Initialize();


            DungeonFlowNode m_EntranceNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandRoomPrefabs.Giant_Elevator_Room);
            DungeonFlowNode m_ShopNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.shop_room_table);
            DungeonFlowNode m_ChestRoom_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandPrefabs.reward_room);
            DungeonFlowNode m_ChestRoom_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandPrefabs.reward_room);
            DungeonFlowNode m_WinchesterNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);

            List<PrototypeDungeonRoom> m_WinchesterRoomList = new List<PrototypeDungeonRoom>();

            foreach (WeightedRoom weightedRoom in ExpandPrefabs.winchesterroomtable.includedRooms.elements) {
                if (weightedRoom.room != null) { m_WinchesterRoomList.Add(weightedRoom.room); }
            }

            if (m_WinchesterRoomList.Count > 0) {
                m_WinchesterRoomList = m_WinchesterRoomList.Shuffle();
                m_WinchesterNode.overrideExactRoom = BraveUtility.RandomElement(m_WinchesterRoomList);
            }


            DungeonFlowNode m_GunMuncherNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandPrefabs.subshop_muncher_01);


            DungeonFlowNode m_ShopBackRoomNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.ShopBackRoom);
            DungeonFlowNode m_ShopBackRoomExitNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom_Pitfall);

            DungeonFlowNode m_SecretKeyShop = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, Instantiate(ExpandPrefabs.shop_special_key_01));
            DungeonFlowNode m_SecretHubRoom = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, Instantiate(ExpandPrefabs.square_hub));
            m_SecretKeyShop.overrideExactRoom.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            m_SecretHubRoom.overrideExactRoom.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            m_SecretHubRoom.overrideExactRoom.name = "Secret Hub Room";
            m_SecretHubRoom.overrideExactRoom.placedObjects.Clear();
            m_SecretHubRoom.overrideExactRoom.placedObjectPositions.Clear();
            m_SecretHubRoom.overrideExactRoom.roomEvents.Clear();

            DungeonFlowNode m_FirstSecretAreaChallengeShrineNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.MegaChallengeShrineTable);

            DungeonFlowNode m_ExitNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.SecretExitRoom);
            DungeonFlowNode m_BossFoyerNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.boss_foyertable);
            // DungeonFlowNode m_BossNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, BraveUtility.RandomElement(ExpandPrefabs.MegaBossRoomTable.includedRooms.elements).room);
            DungeonFlowNode m_BossNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.GungeoneerMimicBossRoom);
            
            DungeonFlowNode m_FakeBossNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.FakeBossRoom);
            // DungeonFlowNode m_FakeBossFoyerNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.boss_foyer, oneWayLoopTarget: true);

            DungeonFlowNode m_FirstChainNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_FirstChainNode_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_FirstChainNode_03 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_FirstChainShrineNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandPrefabs.letsgetsomeshrines_001);
            // DungeonFlowNode m_FirstChainKeyRoomNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, Instantiate(ExpandRoomPrefabs.Utiliroom));
            // m_FirstChainKeyRoomNode.overrideExactRoom.name = "Special Key Room 1";

            DungeonFlowNode m_FirstSecretChainNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_FirstSecretChainBlankRoomNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, Instantiate(ExpandRoomPrefabs.Utiliroom));
            DungeonFlowNode m_FirstSecretChainBlankRoomNode_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, Instantiate(ExpandRoomPrefabs.Utiliroom));
            DungeonFlowNode m_FirstSecretChainKeyRoomNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, Instantiate(ExpandRoomPrefabs.Utiliroom));
            m_FirstSecretChainBlankRoomNode_01.overrideExactRoom.name = "Blank Room 1";
            m_FirstSecretChainBlankRoomNode_02.overrideExactRoom.name = "Blank Room 2";
            m_FirstSecretChainKeyRoomNode.overrideExactRoom.name = "Special Key Room 2";

            DungeonFlowNode m_FirstSecretChainMiniBossNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.MegaMiniBossRoomTable);
            DungeonFlowNode m_FirstSecretChainWallMimicNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.SpecialWallMimicRoom);

            DungeonFlowNode m_FirstSecretChainSpecialNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.basic_special_rooms_noBlackMarket);
            DungeonFlowNode m_FirstSecretChainHubNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);
            DungeonFlowNode m_FirstSecretSpecialSecretNode1 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom);
            DungeonFlowNode m_FirstSecretSpecialSecretNode2 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom_SpecialPit);
            DungeonFlowNode m_FirstSecretSpecialSecretNode3 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, Instantiate(ExpandRoomPrefabs.Utiliroom));
            DungeonFlowNode m_FirstSecretSpecialSecretNode4 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, Instantiate(ExpandRoomPrefabs.Utiliroom));
            m_FirstSecretSpecialSecretNode3.overrideExactRoom.name = "Tiny Secret Room 1";
            m_FirstSecretSpecialSecretNode4.overrideExactRoom.name = "Tiny Secret Room 2";
            m_FirstSecretSpecialSecretNode3.overrideExactRoom.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            m_FirstSecretSpecialSecretNode4.overrideExactRoom.category = PrototypeDungeonRoom.RoomCategory.SECRET;

            DungeonFlowNode m_FirstSecretChainCombatNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);

            DungeonFlowNode m_SecondChainNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_SecondChainNode_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_SecondChainNode_03 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_SecondChainNode_04 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_SecondChainNode_05 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);            
            DungeonFlowNode m_SecondChainHub = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);

            DungeonFlowNode m_ThirdChainNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_ThirdChainNode_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_ThirdChainNode_03 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_ThirdChainNode_04 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            
            DungeonFlowNode m_FourthChainNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_FourthChainNode_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_FourthChainNode_03 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);

            DungeonFlowNode m_SingleRoomChainNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);

            DungeonFlowNode m_SpecialMaintenanceHubNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB, ExpandRoomPrefabs.SpecialMaintenanceRoom, isWarpWingNode: true);
            DungeonFlowNode m_SpecialMaintenanceEntranceNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, Instantiate(ExpandRoomPrefabs.Utiliroom));
            m_SpecialMaintenanceEntranceNode.overrideExactRoom.name = "Special Entrance";
            DungeonFlowNode m_ThwompTrapRoomNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.ThwompCrossingVerticalNoRain);


            DungeonFlowNode m_SpecialMaintenanceSecretRewardNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.SecretRewardRoom);

            DungeonFlowNode m_PuzzleNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, Instantiate(ExpandPrefabs.gungeon_checkerboard));
            m_PuzzleNode_01.overrideExactRoom.name = "Zelda Puzzle Room 1";
            DungeonFlowNode m_PuzzleNode_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, Instantiate(ExpandPrefabs.gungeon_normal_fightinaroomwithtonsoftraps));
            m_PuzzleNode_02.overrideExactRoom.name = "Zelda Puzzle Room 2";
            // Zelda Puzzle Room 3
            DungeonFlowNode m_PuzzleNode_03 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.PuzzleRoom3);


            DungeonFlowNode m_Temp = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.ThwompCrossingHorizontal);


            DungeonFlowNode m_SecretBossNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.SecretBossRoom);
            DungeonFlowNode m_SecretBossFoyerNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandPrefabs.DragunBossFoyerRoom);
            DungeonFlowNode m_SecretBossExitNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandPrefabs.DraGunExitRoom);
            DungeonFlowNode m_SecretBossEndTimesNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandPrefabs.DraGunEndTimesRoom, isWarpWingNode: true);
            DungeonFlowNode m_SecretBossShopNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandPrefabs.BlacksmithShop);


            // Entrance Node
            m_CachedFlow.AddNodeToFlow(m_EntranceNode, null);

            // First Chain with path to main secret area.
            m_CachedFlow.AddNodeToFlow(m_FirstChainNode_01, m_EntranceNode);
            m_CachedFlow.AddNodeToFlow(m_FirstChainNode_02, m_FirstChainNode_01);
            m_CachedFlow.AddNodeToFlow(m_FirstChainNode_03, m_FirstChainNode_02);
            // m_CachedFlow.AddNodeToFlow(m_FirstChainKeyRoomNode, m_FirstChainNode_02);
            m_CachedFlow.AddNodeToFlow(m_FirstChainShrineNode, m_FirstChainNode_03);
            m_CachedFlow.AddNodeToFlow(m_ChestRoom_01, m_FirstChainNode_03);
            m_CachedFlow.AddNodeToFlow(m_SecretKeyShop, m_ChestRoom_01);
            m_CachedFlow.AddNodeToFlow(m_SecretHubRoom, m_SecretKeyShop);

            m_CachedFlow.AddNodeToFlow(m_FirstSecretAreaChallengeShrineNode, m_SecretHubRoom);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretChainHubNode, m_FirstSecretAreaChallengeShrineNode);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretChainBlankRoomNode_01, m_FirstSecretChainHubNode);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretChainCombatNode, m_FirstSecretChainHubNode);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretChainMiniBossNode, m_FirstSecretChainCombatNode);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretChainKeyRoomNode, m_FirstSecretChainMiniBossNode);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretChainWallMimicNode, m_FirstSecretChainHubNode);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretSpecialSecretNode4, m_FirstSecretChainWallMimicNode);


            m_CachedFlow.AddNodeToFlow(m_FirstSecretChainNode, m_SecretHubRoom);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretChainBlankRoomNode_02, m_FirstSecretChainNode);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretChainSpecialNode, m_FirstSecretChainNode);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretSpecialSecretNode1, m_SecretHubRoom);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretSpecialSecretNode2, m_FirstSecretSpecialSecretNode1);
            m_CachedFlow.AddNodeToFlow(m_FirstSecretSpecialSecretNode3, m_FirstSecretSpecialSecretNode2);

            // Second Chain. Leads to shop and the boss forked from a hub room.
            m_CachedFlow.AddNodeToFlow(m_SecondChainNode_01, m_EntranceNode);
            m_CachedFlow.AddNodeToFlow(m_SecondChainHub, m_SecondChainNode_01);
            m_CachedFlow.AddNodeToFlow(m_SecondChainNode_02, m_SecondChainHub);
            m_CachedFlow.AddNodeToFlow(m_SecondChainNode_04, m_SecondChainHub);
            m_CachedFlow.AddNodeToFlow(m_SecondChainNode_03, m_SecondChainNode_02);
            m_CachedFlow.AddNodeToFlow(m_ShopNode, m_SecondChainNode_03);
            m_CachedFlow.AddNodeToFlow(m_ShopBackRoomNode, m_ShopNode);
            m_CachedFlow.AddNodeToFlow(m_ShopBackRoomExitNode, m_ShopBackRoomNode);

            m_CachedFlow.AddNodeToFlow(m_SecondChainNode_05, m_SecondChainNode_04);            
            m_CachedFlow.AddNodeToFlow(m_ChestRoom_02, m_SecondChainNode_05);
            m_CachedFlow.AddNodeToFlow(m_BossFoyerNode, m_SecondChainNode_05);
            m_CachedFlow.AddNodeToFlow(m_BossNode, m_BossFoyerNode);
            m_CachedFlow.AddNodeToFlow(m_ExitNode, m_BossNode);

            // Third Chain (Dead End)
            m_CachedFlow.AddNodeToFlow(m_ThirdChainNode_01, m_EntranceNode);
            m_CachedFlow.AddNodeToFlow(m_ThirdChainNode_02, m_ThirdChainNode_01);
            m_CachedFlow.AddNodeToFlow(m_ThirdChainNode_03, m_ThirdChainNode_02);
            m_CachedFlow.AddNodeToFlow(m_ThirdChainNode_04, m_ThirdChainNode_03);

            // Fourth Chain (Single chain to fake boss)
            m_CachedFlow.AddNodeToFlow(m_FourthChainNode_01, m_EntranceNode);
            m_CachedFlow.AddNodeToFlow(m_FourthChainNode_02, m_FourthChainNode_01);
            m_CachedFlow.AddNodeToFlow(m_FourthChainNode_03, m_FourthChainNode_02);
            m_CachedFlow.AddNodeToFlow(m_FakeBossFoyerNode, m_FourthChainNode_03);
            m_CachedFlow.AddNodeToFlow(m_FakeBossNode, m_FakeBossFoyerNode);
            m_CachedFlow.LoopConnectNodes(m_FourthChainNode_01, m_FakeBossFoyerNode);

            // Single Room Dead end
            m_CachedFlow.AddNodeToFlow(m_SingleRoomChainNode, m_EntranceNode);
            // Winchester Room
            m_CachedFlow.AddNodeToFlow(m_WinchesterNode, m_EntranceNode);
            // Gun Muncher Room
            m_CachedFlow.AddNodeToFlow(m_GunMuncherNode, m_EntranceNode);

            // Special Maintenance Room Chain            
            m_CachedFlow.AddNodeToFlow(m_SpecialMaintenanceHubNode, m_EntranceNode);
            m_CachedFlow.AddNodeToFlow(m_ThwompTrapRoomNode, m_SpecialMaintenanceHubNode);
            m_CachedFlow.AddNodeToFlow(m_SpecialMaintenanceEntranceNode, m_ThwompTrapRoomNode);

            m_CachedFlow.AddNodeToFlow(m_SpecialMaintenanceSecretRewardNode, m_SpecialMaintenanceHubNode);
            m_CachedFlow.AddNodeToFlow(m_PuzzleNode_01, m_SpecialMaintenanceHubNode);
            m_CachedFlow.AddNodeToFlow(m_PuzzleNode_02, m_PuzzleNode_01);
            m_CachedFlow.AddNodeToFlow(m_PuzzleNode_03, m_SpecialMaintenanceHubNode);

            // Secret Boss Chain
            m_CachedFlow.AddNodeToFlow(m_SecretBossShopNode, m_SpecialMaintenanceSecretRewardNode);
            m_CachedFlow.AddNodeToFlow(m_SecretBossFoyerNode, m_SecretBossShopNode);
            m_CachedFlow.AddNodeToFlow(m_SecretBossNode, m_SecretBossFoyerNode);
            m_CachedFlow.AddNodeToFlow(m_SecretBossExitNode, m_SecretBossNode);
            m_CachedFlow.AddNodeToFlow(m_SecretBossEndTimesNode, m_SecretBossExitNode);

            m_CachedFlow.FirstNode = m_EntranceNode;

            return m_CachedFlow;
        }


        public static IEnumerator InitCustomObjects(float Seed = 0, bool randomBool = false, bool randomBool2 = false) {
            AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
            AssetBundle assetBundle2 = ResourceManager.LoadAssetBundle("shared_auto_002");
            ExpandObjectDatabase objectDatabase = new ExpandObjectDatabase();
            PlayerController PrimaryPlayer = GameManager.Instance.PrimaryPlayer;
            try { Pixelator.Instance.RegisterAdditionalRenderPass(ExpandShaders.GlitchScreenShader); } catch (System.Exception) { }
            // GameManager.Instance.Dungeon.musicEventName = "Play_Mus_Dungeon_Rat_Theme_01";
            // GameManager.Instance.DungeonMusicController.ResetForNewFloor(GameManager.Instance.Dungeon);
            

            if (PrimaryPlayer.HasPickupID(316)) {
		        while (PrimaryPlayer.HasPickupID(316)) {
                    PrimaryPlayer.RemovePassiveItem(316);
                    if (!PrimaryPlayer.HasPickupID(316)) {
                        GameUIRoot.Instance.UpdatePlayerConsumables(PrimaryPlayer.carriedConsumables);
                        break;
                    }
                    yield return null;
                }                
            }

            yield return null;

            GameObject[] GameObjects = FindObjectsOfType<GameObject>();
            if (GameObjects != null) {
                foreach (GameObject obj in GameObjects) {
                    if (!string.IsNullOrEmpty(obj.name)) {
                        if (obj.name.ToLower().StartsWith("arrival") && obj.name.ToLower().EndsWith("(clone)")) {
                            obj.name = "Arrival";
                            obj.transform.name = "Arrival";
                        }
                    }
                }

            }

            try {
                DungeonPlaceable ChestPlatform = assetBundle2.LoadAsset("Treasure_Dais_Stone_Carpet") as DungeonPlaceable;

                GameObject Chest_Black = assetBundle.LoadAsset<GameObject>("Chest_Black");
                GameObject Chest_Rainbow = assetBundle.LoadAsset<GameObject>("Chest_Rainbow");
                GameObject Chest_Rat = assetBundle.LoadAsset<GameObject>("Chest_Rat");

                RoomHandler GiantElevatorEntranceRoom = null;
                RoomHandler SpecialMaintenanceRoom = null;
                RoomHandler SpecialEntrance = null;
                RoomHandler SecretRewardRoom = null;
                RoomHandler SecretBossRoom = null;
                RoomHandler SecretBossFoyerRoom = null;
                RoomHandler PuzzleRoom1 = null;
                RoomHandler PuzzleRoom2 = null;
                RoomHandler PuzzleRoom3 = null;
                RoomHandler SpecialWallMimicRoom = null;
                RoomHandler ShopBackRoom = null;
                RoomHandler TinyPitFallRoom = null;                
                // RoomHandler TinyKeyRoom1 = null;
                RoomHandler TinyKeyRoom2 = null;
                RoomHandler TinyKeyRoom3 = null;
                RoomHandler TinyKeyRoom4 = null;
                RoomHandler TinyBlankRoom1 = null;
                RoomHandler TinyBlankRoom2 = null;

                // GameObject PlacedSecretKeyPedestal1 = null;
                GameObject PlacedSecretKeyPedestal2 = null;
                GameObject PlacedSecretKeyPedestal3 = null;
                GameObject PlacedSecretKeyPedestal4 = null;
                GameObject BlankRewardPedestal1 = null;
                GameObject BlankRewardPedestal2 = null;

                if (PrimaryPlayer.carriedConsumables.ResourcefulRatKeys > 0) {
                    PrimaryPlayer.carriedConsumables.ResourcefulRatKeys = 0;
                    GameUIRoot.Instance.UpdatePlayerConsumables(PrimaryPlayer.carriedConsumables);
                }

                if (FindObjectsOfType<ElevatorArrivalController>() != null) {
                    foreach (ElevatorArrivalController elevatorArrivalController in FindObjectsOfType<ElevatorArrivalController>()) {
                        if (elevatorArrivalController.gameObject.GetComponentsInChildren<tk2dBaseSprite>(true) != null) {
                            foreach (tk2dBaseSprite baseSprite in elevatorArrivalController.gameObject.GetComponentsInChildren<tk2dBaseSprite>(true)) {
                                ExpandShaders.Instance.ApplyGlitchShader(baseSprite);
                            }
                        }
                    }
                }

                if (FindObjectsOfType<ElevatorDepartureController>() != null) {                    
                    foreach (ElevatorDepartureController elevatorDepartureController in FindObjectsOfType<ElevatorDepartureController>()) {
                        elevatorDepartureController.UsesOverrideTargetFloor = true;
                        elevatorDepartureController.OverrideTargetFloor = GlobalDungeonData.ValidTilesets.FORGEGEON;
                    }
                }


                foreach (RoomHandler roomHandler in GameManager.Instance.Dungeon.data.rooms) {
                    if (roomHandler.GetRoomName() != null) {
                        if (roomHandler.GetRoomName().StartsWith("Giant Elevator Room")) { GiantElevatorEntranceRoom = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Special Maintenance Room")) { SpecialMaintenanceRoom = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Special Entrance")) { SpecialEntrance = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Secret Reward Room")) { SecretRewardRoom = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Secret Boss Room")) { SecretBossRoom = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith(ExpandPrefabs.DragunBossFoyerRoom.name)) { SecretBossFoyerRoom = roomHandler; }                        
                        if (roomHandler.GetRoomName().StartsWith("Zelda Puzzle Room 1")) { PuzzleRoom1 = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Zelda Puzzle Room 2")) { PuzzleRoom2 = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Zelda Puzzle Room 3")) { PuzzleRoom3 = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Special WallMimic Room")) { SpecialWallMimicRoom = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Shop Back Room")) { ShopBackRoom = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Utiliroom (Pitfall)")) { TinyPitFallRoom = roomHandler; }
                        // if (roomHandler.GetRoomName().StartsWith("Special Key Room 1")) { TinyKeyRoom1 = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Special Key Room 2")) { TinyKeyRoom2 = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Tiny Secret Room 1")) { TinyKeyRoom3 = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Tiny Secret Room 2")) { TinyKeyRoom4 = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Blank Room 1")) { TinyBlankRoom1 = roomHandler; }
                        if (roomHandler.GetRoomName().StartsWith("Blank Room 2")) { TinyBlankRoom2 = roomHandler; }
                    }
                }

                if (SpecialMaintenanceRoom != null && GiantElevatorEntranceRoom != null) {
                    // ChaosWeatherController.AddRainStormToRoom(GiantElevatorEntranceRoom, new IntVector2(50, 50), 480f, true);                
                    ExpandWeatherController.AddRainStormToFloor("Base_ResourcefulRat", 480f, true);                    

                    GiantElevatorEntranceRoom.TargetPitfallRoom = SpecialMaintenanceRoom;
                    GiantElevatorEntranceRoom.ForcePitfallForFliers = true;
                    ExpandUtility.FloorStamper(SpecialMaintenanceRoom, new IntVector2(8, 8), 14, 13, CellType.FLOOR);

                    if (FindObjectsOfType<NoteDoer>() != null) {
                        foreach (NoteDoer note in FindObjectsOfType<NoteDoer>()) {
                            if (note.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith(SpecialMaintenanceRoom.GetRoomName())) {
                                note.stringKey = "A mini dungeon strung together by Lunk based on previous Dungeons he had encountered.\nFind the keys to gain access to the final puzzle.";
                                note.alreadyLocalized = true;
                                note.name = "Lunk's Dungeon Sign";
                            }
                        }
                    }
                }

                if (SecretRewardRoom != null) {

                    IntVector2 TreasureChestCarpetPosition1 = new IntVector2(8, 29);
                    IntVector2 TreasureChestCarpetPosition2 = new IntVector2(8, 54);
                    IntVector2 SecretChestPosition1 = new IntVector2(8, 31);
                    IntVector2 SecretChestPosition2 = new IntVector2(8, 56);
                    GameObject TreasureChestStoneCarpet1 = ChestPlatform.InstantiateObject(SecretRewardRoom, TreasureChestCarpetPosition1);
                    GameObject TreasureChestStoneCarpet2 = ChestPlatform.InstantiateObject(SecretRewardRoom, TreasureChestCarpetPosition2);
                    TreasureChestStoneCarpet1.transform.position -= new Vector3(0.55f, 0);
                    TreasureChestStoneCarpet2.transform.position -= new Vector3(0.55f, 0);
                    TreasureChestStoneCarpet1.transform.parent = SecretRewardRoom.hierarchyParent;
                    TreasureChestStoneCarpet2.transform.parent = SecretRewardRoom.hierarchyParent;

                    GameObject PlacedBlackChestObject = ExpandUtility.GenerateDungeonPlacable(Chest_Black, false, true).InstantiateObject(SecretRewardRoom, SecretChestPosition1);
                    GameObject PlacedRainbowChestObject = ExpandUtility.GenerateDungeonPlacable(Chest_Rainbow, false, true).InstantiateObject(SecretRewardRoom, SecretChestPosition2);
                    PlacedBlackChestObject.transform.position += new Vector3(0.5f, 0);
                    PlacedRainbowChestObject.transform.position += new Vector3(0.5f, 0);
                    TreasureChestStoneCarpet1.transform.position += new Vector3(0.5f, 0);
                    TreasureChestStoneCarpet2.transform.position += new Vector3(0.5f, 0);
                    PlacedBlackChestObject.transform.parent = SecretRewardRoom.hierarchyParent;
                    PlacedRainbowChestObject.transform.parent = SecretRewardRoom.hierarchyParent;

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
                    SecretRewardRoom.RegisterInteractable(PlacedBlackChestComponent);
                    SecretRewardRoom.RegisterInteractable(PlacedRainbowChestComponent);

                    Vector3 SpecialLockedDoorPosition = new Vector3(9, 52.25f) + SecretRewardRoom.area.basePosition.ToVector3();
                    GameObject SpecialLockedDoor = Instantiate(objectDatabase.LockedJailDoor, SpecialLockedDoorPosition, Quaternion.identity);
                    SpecialLockedDoor.transform.parent = SecretRewardRoom.hierarchyParent;
                    InteractableLock SpecialLockedDoorComponent = SpecialLockedDoor.GetComponentInChildren<InteractableLock>();
                    SpecialLockedDoorComponent.lockMode = InteractableLock.InteractableLockMode.RESOURCEFUL_RAT;
                    SpecialLockedDoorComponent.JailCellKeyId = 0;
                    tk2dBaseSprite RainbowLockSprite = SpecialLockedDoorComponent.GetComponentInChildren<tk2dBaseSprite>();
                    if (RainbowLockSprite != null) { ExpandShaders.Instance.ApplyRainbowShader(RainbowLockSprite); }

                    GameObject PlacedPuzzleKeyPedestal = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RewardPedestalPrefab, false, true).InstantiateObject(SecretRewardRoom, new IntVector2(9, 15), false, true);
                    if (PlacedPuzzleKeyPedestal != null) {
                        if (PlacedPuzzleKeyPedestal.GetComponent<RewardPedestal>() != null) {
                            RewardPedestal PlacedPuzzleKeyPedestalComponent = PlacedPuzzleKeyPedestal.GetComponent<RewardPedestal>();
                            PlacedPuzzleKeyPedestalComponent.SpecificItemId = 727;
                            PlacedPuzzleKeyPedestalComponent.SpawnsTertiarySet = false;
                            PlacedPuzzleKeyPedestalComponent.UsesSpecificItem = true;
                            PlacedPuzzleKeyPedestalComponent.overrideMimicChance = 0f;
                            PlacedPuzzleKeyPedestalComponent.ConfigureOnPlacement(SecretRewardRoom);
                        }
                    }

                    IntVector2 PuzzleChestPosition1 = new IntVector2(4, 19);
                    IntVector2 PuzzleChestPosition2 = new IntVector2(12, 19);
                    IntVector2 PuzzleChestPosition3 = new IntVector2(4, 40);
                    IntVector2 PuzzleChestPosition4 = new IntVector2(12, 40);
                    IntVector2 PuzzleChestPosition5 = new IntVector2(4, 50);
                    IntVector2 PuzzleChestPosition6 = new IntVector2(12, 50);
                    IntVector2 PuzzleChestCarpetPosition1 = PuzzleChestPosition1 - new IntVector2(0, 1);
                    IntVector2 PuzzleChestCarpetPosition2 = PuzzleChestPosition2 - new IntVector2(0, 1);
                    IntVector2 PuzzleChestCarpetPosition3 = PuzzleChestPosition3 - new IntVector2(0, 1);
                    IntVector2 PuzzleChestCarpetPosition4 = PuzzleChestPosition4 - new IntVector2(0, 1);
                    IntVector2 PuzzleChestCarpetPosition5 = PuzzleChestPosition5 - new IntVector2(0, 1);
                    IntVector2 PuzzleChestCarpetPosition6 = PuzzleChestPosition6 - new IntVector2(0, 1);

                    GameObject PlacedPuzzleRatChest1 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(SecretRewardRoom, PuzzleChestPosition1, false, true);
                    GameObject PlacedPuzzleRatChest2 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(SecretRewardRoom, PuzzleChestPosition2, false, true);
                    GameObject PlacedPuzzleRatChest3 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(SecretRewardRoom, PuzzleChestPosition3, false, true);
                    GameObject PlacedPuzzleRatChest4 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(SecretRewardRoom, PuzzleChestPosition4, false, true);
                    GameObject PlacedPuzzleRatChest5 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(SecretRewardRoom, PuzzleChestPosition5, false, true);
                    GameObject PlacedPuzzleRatChest6 = ExpandUtility.GenerateDungeonPlacable(Chest_Rat, false, true).InstantiateObject(SecretRewardRoom, PuzzleChestPosition6, false, true);
                    GameObject PuzzleChestStoneCarpet1 = ChestPlatform.InstantiateObject(SecretRewardRoom, PuzzleChestCarpetPosition1);
                    GameObject PuzzleChestStoneCarpet2 = ChestPlatform.InstantiateObject(SecretRewardRoom, PuzzleChestCarpetPosition2);
                    GameObject PuzzleChestStoneCarpet3 = ChestPlatform.InstantiateObject(SecretRewardRoom, PuzzleChestCarpetPosition3);
                    GameObject PuzzleChestStoneCarpet4 = ChestPlatform.InstantiateObject(SecretRewardRoom, PuzzleChestCarpetPosition4);
                    GameObject PuzzleChestStoneCarpet5 = ChestPlatform.InstantiateObject(SecretRewardRoom, PuzzleChestCarpetPosition5);
                    GameObject PuzzleChestStoneCarpet6 = ChestPlatform.InstantiateObject(SecretRewardRoom, PuzzleChestCarpetPosition6);
                    PlacedPuzzleRatChest1.transform.parent = SecretRewardRoom.hierarchyParent;
                    PlacedPuzzleRatChest2.transform.parent = SecretRewardRoom.hierarchyParent;
                    PlacedPuzzleRatChest3.transform.parent = SecretRewardRoom.hierarchyParent;
                    PlacedPuzzleRatChest4.transform.parent = SecretRewardRoom.hierarchyParent;
                    PlacedPuzzleRatChest5.transform.parent = SecretRewardRoom.hierarchyParent;
                    PlacedPuzzleRatChest6.transform.parent = SecretRewardRoom.hierarchyParent;
                    PuzzleChestStoneCarpet1.transform.parent = SecretRewardRoom.hierarchyParent;
                    PuzzleChestStoneCarpet2.transform.parent = SecretRewardRoom.hierarchyParent;
                    PuzzleChestStoneCarpet3.transform.parent = SecretRewardRoom.hierarchyParent;
                    PuzzleChestStoneCarpet4.transform.parent = SecretRewardRoom.hierarchyParent;
                    PuzzleChestStoneCarpet5.transform.parent = SecretRewardRoom.hierarchyParent;
                    PuzzleChestStoneCarpet6.transform.parent = SecretRewardRoom.hierarchyParent;

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

                    if (Seed < 0.5f) {
                        PuzzleRatChest1Component.forceContentIds = new List<int> { 68 };
                        PuzzleRatChest2Component.forceContentIds = new List<int> { 727, 727 };
                    } else {
                        PuzzleRatChest1Component.forceContentIds = new List<int> { 727, 727 };
                        PuzzleRatChest2Component.forceContentIds = new List<int> { 68 };
                    }
                    if (randomBool) {
                        PuzzleRatChest3Component.forceContentIds = new List<int> { 70, 70, 70, 70 };
                        PuzzleRatChest4Component.forceContentIds = new List<int> { 727, 727 };
                    } else {
                        PuzzleRatChest3Component.forceContentIds = new List<int> { 727, 727 };
                        PuzzleRatChest4Component.forceContentIds = new List<int> { 70, 70, 70, 70 };
                    }
                    if (randomBool2) {
                        PuzzleRatChest5Component.forceContentIds = new List<int> { 74 };
                        PuzzleRatChest6Component.forceContentIds = new List<int> { 316 };
                    } else {
                        PuzzleRatChest5Component.forceContentIds = new List<int> { 316 };
                        PuzzleRatChest6Component.forceContentIds = new List<int> { 74 };
                    }

                    PuzzleRatChest1Component.ConfigureOnPlacement(SecretRewardRoom);
                    PuzzleRatChest2Component.ConfigureOnPlacement(SecretRewardRoom);
                    PuzzleRatChest3Component.ConfigureOnPlacement(SecretRewardRoom);
                    PuzzleRatChest4Component.ConfigureOnPlacement(SecretRewardRoom);
                    PuzzleRatChest5Component.ConfigureOnPlacement(SecretRewardRoom);
                    PuzzleRatChest6Component.ConfigureOnPlacement(SecretRewardRoom);
                    SecretRewardRoom.RegisterInteractable(PuzzleRatChest1Component);
                    SecretRewardRoom.RegisterInteractable(PuzzleRatChest2Component);
                    SecretRewardRoom.RegisterInteractable(PuzzleRatChest3Component);
                    SecretRewardRoom.RegisterInteractable(PuzzleRatChest4Component);
                    SecretRewardRoom.RegisterInteractable(PuzzleRatChest5Component);
                    SecretRewardRoom.RegisterInteractable(PuzzleRatChest6Component);


                    if (FindObjectsOfType<NoteDoer>() != null) {
                        foreach (NoteDoer note in FindObjectsOfType<NoteDoer>()) {
                            if (note.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith(SecretRewardRoom.GetRoomName())) {
                                note.stringKey = "A minigame Lunk created based on a game he used to play in a land far away.\nGuess the right chest to continue forward.\n If you can guess the correct chest 3 times, the ultimate prize shall be gained!";
                                note.alreadyLocalized = true;
                                note.name = "Lunk's Minigame Sign";
                            }
                        }
                    }
                }

                /*if (TinyKeyRoom1 != null) {
                    PlacedSecretKeyPedestal1 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RewardPedestalPrefab, false, true).InstantiateObject(TinyKeyRoom1, IntVector2.One, false, true);
                    PlacedSecretKeyPedestal1.transform.parent = TinyKeyRoom1.hierarchyParent;
                    RewardPedestal PlacedSecretKeyPedestalComponent1 = PlacedSecretKeyPedestal1.GetComponent<RewardPedestal>();
                    PlacedSecretKeyPedestalComponent1.SpecificItemId = 727;
                    PlacedSecretKeyPedestalComponent1.SpawnsTertiarySet = false;
                    PlacedSecretKeyPedestalComponent1.UsesSpecificItem = true;
                    PlacedSecretKeyPedestalComponent1.overrideMimicChance = 0f;
                    PlacedSecretKeyPedestalComponent1.ConfigureOnPlacement(TinyKeyRoom1);
                }*/
                if (TinyKeyRoom2 != null) {
                    PlacedSecretKeyPedestal2 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RewardPedestalPrefab, false, true).InstantiateObject(TinyKeyRoom2, IntVector2.One, false, true);
                    PlacedSecretKeyPedestal2.transform.parent = TinyKeyRoom2.hierarchyParent;
                    RewardPedestal PlacedSecretKeyPedestalComponent2 = PlacedSecretKeyPedestal2.GetComponent<RewardPedestal>();
                    PlacedSecretKeyPedestalComponent2.SpecificItemId = 727;
                    PlacedSecretKeyPedestalComponent2.SpawnsTertiarySet = false;
                    PlacedSecretKeyPedestalComponent2.UsesSpecificItem = true;
                    PlacedSecretKeyPedestalComponent2.overrideMimicChance = 0f;
                    PlacedSecretKeyPedestalComponent2.ConfigureOnPlacement(TinyKeyRoom2);
                }
                if (TinyKeyRoom3 != null) {
                    PlacedSecretKeyPedestal3 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RewardPedestalPrefab, false, true).InstantiateObject(TinyKeyRoom3, IntVector2.One, false, true);
                    PlacedSecretKeyPedestal3.transform.parent = TinyKeyRoom3.hierarchyParent;
                    RewardPedestal PlacedSecretKeyPedestalComponent3 = PlacedSecretKeyPedestal3.GetComponent<RewardPedestal>();
                    PlacedSecretKeyPedestalComponent3.SpecificItemId = 727;
                    PlacedSecretKeyPedestalComponent3.SpawnsTertiarySet = false;
                    PlacedSecretKeyPedestalComponent3.UsesSpecificItem = true;
                    PlacedSecretKeyPedestalComponent3.overrideMimicChance = 0f;
                    PlacedSecretKeyPedestalComponent3.ConfigureOnPlacement(TinyKeyRoom3);
                }
                if (TinyKeyRoom4 != null) {
                    PlacedSecretKeyPedestal4 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RewardPedestalPrefab, false, true).InstantiateObject(TinyKeyRoom4, IntVector2.One, false, true);
                    PlacedSecretKeyPedestal4.transform.parent = TinyKeyRoom4.hierarchyParent;
                    RewardPedestal PlacedSecretKeyPedestalComponent4 = PlacedSecretKeyPedestal4.GetComponent<RewardPedestal>();
                    PlacedSecretKeyPedestalComponent4.SpecificItemId = 727;
                    PlacedSecretKeyPedestalComponent4.SpawnsTertiarySet = false;
                    PlacedSecretKeyPedestalComponent4.UsesSpecificItem = true;
                    PlacedSecretKeyPedestalComponent4.overrideMimicChance = 0f;
                    PlacedSecretKeyPedestalComponent4.ConfigureOnPlacement(TinyKeyRoom4);

                }
                if (TinyBlankRoom1 != null) {
                    BlankRewardPedestal1 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RewardPedestalPrefab, false, true).InstantiateObject(TinyBlankRoom1, IntVector2.One, false, true);
                    BlankRewardPedestal1.transform.parent = TinyBlankRoom1.hierarchyParent;
                    RewardPedestal BlankRewardPedestallComponent1 = BlankRewardPedestal1.GetComponent<RewardPedestal>();
                    BlankRewardPedestallComponent1.SpecificItemId = 224;
                    BlankRewardPedestallComponent1.SpawnsTertiarySet = false;
                    BlankRewardPedestallComponent1.UsesSpecificItem = true;
                    BlankRewardPedestallComponent1.overrideMimicChance = 0f;
                    BlankRewardPedestallComponent1.ConfigureOnPlacement(TinyBlankRoom1);
                }
                if (TinyBlankRoom2 != null) {
                    BlankRewardPedestal2 = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RewardPedestalPrefab, false, true).InstantiateObject(TinyBlankRoom2, IntVector2.One, false, true);
                    BlankRewardPedestal2.transform.parent = TinyBlankRoom2.hierarchyParent;
                    RewardPedestal BlankRewardPedestallComponent2 = BlankRewardPedestal2.GetComponent<RewardPedestal>();
                    BlankRewardPedestallComponent2.SpecificItemId = 224;
                    BlankRewardPedestallComponent2.SpawnsTertiarySet = false;
                    BlankRewardPedestallComponent2.UsesSpecificItem = true;
                    BlankRewardPedestallComponent2.overrideMimicChance = 0f;
                    BlankRewardPedestallComponent2.ConfigureOnPlacement(TinyBlankRoom2);
                }

                if (SpecialWallMimicRoom != null) {
                    IntVector2 WingsItemPosition = new IntVector2(9, 6);
                    Vector2 WingsItemOffset = new Vector2(0.5f, 0.8f);
                    if (Seed <= 0.5f) {
                        WingsItemPosition += new IntVector2(0, 7);
                        WingsItemOffset -= new Vector2(0f, 0.3f);
                    }
                    GameObject PlacedWingsItem = ExpandUtility.GenerateDungeonPlacable(spawnsItem: true, CustomOffset: WingsItemOffset).InstantiateObject(SpecialWallMimicRoom, WingsItemPosition);
                    PlacedWingsItem.transform.parent = SpecialWallMimicRoom.hierarchyParent;
                    WingsItem WingsItemComponent = PlacedWingsItem.GetComponent<WingsItem>();
                    if (WingsItemComponent != null) {
                        SpecialWallMimicRoom.RegisterInteractable(WingsItemComponent);
                        PassiveItem WingsPassive = WingsItemComponent.GetComponentInChildren<PassiveItem>(true);
                        if (WingsPassive) { WingsPassive.GetRidOfMinimapIcon(); }
                    }
                }

                if (ShopBackRoom != null) {
                    IntVector2 MirrorChestPosition = new IntVector2(3, 33);
                    GameObject MirrorChestShrineObject = ExpandPrefabs.CurrsedMirrorPlacable.InstantiateObject(ShopBackRoom, MirrorChestPosition, true);
                    MirrorChestShrineObject.transform.parent = ShopBackRoom.hierarchyParent;
                    MirrorChestShrineObject.AddComponent<ExpandMirrorController>();
                    ExpandMirrorController chaosMirrorControllerComponent = MirrorChestShrineObject.GetComponent<ExpandMirrorController>();
                    chaosMirrorControllerComponent.ConfigureOnPlacement(ShopBackRoom);
                    
                    IntVector2 BellosNotePosition = new IntVector2(3, 9);
                    GameObject PlacedBellosNote = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.PlayerLostRatNote, false, true).InstantiateObject(ShopBackRoom, BellosNotePosition);
                    PlacedBellosNote.transform.parent = ShopBackRoom.hierarchyParent;
                    NoteDoer BellosNoteComponent = PlacedBellosNote.GetComponent<NoteDoer>();

                    if (BellosNoteComponent != null) {
                        if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.PHOBOSGEON) {
                            BellosNoteComponent.stringKey = "Every time the Gungeon shifts I expect to be in the Forge. But it seems I'm destined to always end up in this corrupted world.\nThe fabric of reality seems off here. Even the Gundead dread being here.\n I have stashed my 2 most important items behind an enchanted mirror across a long pit.\n Should keep those vandalizing Gungeoneers who like to shoot up my shop from getting to it.\n One day I'll reach the real Forge and finally meet her....Maybe then I can finally leave this place...";
                        } else {
                            BellosNoteComponent.stringKey = "I have stashed my most important items behind an enchanted mirror across a long pit.\nHopefully it will be enough to keep them safe from the Gungeoneers who like to vandalize my shop with their guns.";
                        }
                        BellosNoteComponent.useAdditionalStrings = false;
                        BellosNoteComponent.alreadyLocalized = true;
                        BellosNoteComponent.name = "Bellos Note";
                        ShopBackRoom.RegisterInteractable(BellosNoteComponent);
                    }
                    if (SpecialEntrance != null && TinyPitFallRoom != null) {
                        SpecialEntrance.AddProceduralTeleporterToRoom();
                        TinyPitFallRoom.ForcePitfallForFliers = true;
                        TinyPitFallRoom.TargetPitfallRoom = SpecialEntrance;
                    }
                }

                if (PuzzleRoom1 != null) {
                    List<AIActor> PuzzleEnemyList = new List<AIActor>();
                    if (FindObjectsOfType<AIActor>() != null) {
                        foreach (AIActor actor in FindObjectsOfType<AIActor>()) {
                            if (actor.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith(PuzzleRoom1.GetRoomName())) {
                                PuzzleEnemyList.Add(actor);
                            }
                        }
                    }

                    if (PuzzleEnemyList.Count > 0) {
                        AIActor selectedActor = PuzzleEnemyList[0];
                        if (randomBool) { selectedActor = PuzzleEnemyList[1]; }
                        ExpandShaders.Instance.ApplyGlitchShader(selectedActor.GetComponentInChildren<tk2dBaseSprite>());
                        selectedActor.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>();
                        selectedActor.CanDropItems = true;
                        selectedActor.AdditionalSimpleItemDrops = new List<PickupObject> { ExpandPrefabs.RatKeyItem };                    
                    }
                }

                if (PuzzleRoom2 != null) {
                    IntVector2 GlitchedTable1Position = new IntVector2(9, 10);
                    IntVector2 GlitchedTable2Position = new IntVector2(9, 8);
                    GameObject GlitchedVerticalTable1 = ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, false, true).InstantiateObject(PuzzleRoom2, GlitchedTable1Position);
                    GameObject GlitchedHorizontalTable1 = ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, false, true).InstantiateObject(PuzzleRoom2, GlitchedTable2Position);

                    GlitchedVerticalTable1.AddComponent<ExpandKickableObject>();
                    GlitchedHorizontalTable1.AddComponent<ExpandKickableObject>();

                    float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                    float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
                    float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.2f);
                    float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
                    float RandomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.22f);

                    if (randomBool) {
                        ExpandKickableObject GlitchedTable1Component = GlitchedVerticalTable1.GetComponent<ExpandKickableObject>();
                        GlitchedTable1Component.SpawnedObject = ExpandPrefabs.RatKeyItem.gameObject;
                        GlitchedTable1Component.willDefinitelyExplode = true;
                        GlitchedTable1Component.spawnObjectOnSelfDestruct = true;
                        ExpandShaders.Instance.ApplyGlitchShader(GlitchedTable1Component.GetComponentInChildren<tk2dBaseSprite>(), true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                    } else {
                        ExpandKickableObject GlitchedTable1Component = GlitchedHorizontalTable1.GetComponent<ExpandKickableObject>();
                        GlitchedTable1Component.SpawnedObject = ExpandPrefabs.RatKeyItem.gameObject;
                        GlitchedTable1Component.willDefinitelyExplode = true;
                        GlitchedTable1Component.spawnObjectOnSelfDestruct = true;
                        ExpandShaders.Instance.ApplyGlitchShader(GlitchedTable1Component.GetComponentInChildren<tk2dBaseSprite>(), true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                    }
                    PuzzleRoom2.RegisterInteractable(GlitchedVerticalTable1.GetComponentInChildren<FlippableCover>());
                    PuzzleRoom2.RegisterInteractable(GlitchedHorizontalTable1.GetComponentInChildren<FlippableCover>());
                    PuzzleRoom2.RegisterInteractable(GlitchedVerticalTable1.GetComponentInChildren<ExpandKickableObject>());
                    PuzzleRoom2.RegisterInteractable(GlitchedHorizontalTable1.GetComponentInChildren<ExpandKickableObject>());
                }

                if (PuzzleRoom3 != null) {
                    IntVector2 SecretKeyPosition = new IntVector2(14, 21);
                    GameObject SecretKeyPedestal = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RewardPedestalPrefab, false, true).InstantiateObject(PuzzleRoom3, SecretKeyPosition, false, true);
                    SecretKeyPedestal.transform.parent = PuzzleRoom3.hierarchyParent;

                    RewardPedestal SecretKeyPedestalComponent = SecretKeyPedestal.GetComponent<RewardPedestal>();
                    SecretKeyPedestalComponent.SpecificItemId = 727;
                    SecretKeyPedestalComponent.SpawnsTertiarySet = false;
                    SecretKeyPedestalComponent.UsesSpecificItem = true;
                    SecretKeyPedestalComponent.overrideMimicChance = 0f;
                    SecretKeyPedestalComponent.ConfigureOnPlacement(PuzzleRoom3);

                    IntVector2 wallPosition = new IntVector2(14, 20);
                    // ChaosUtility.DestroyWallAtPosition(GameManager.Instance.Dungeon, PuzzleRoom3, wallPosition, true);
                    // ChaosUtility.DestroyWallAtPosition(GameManager.Instance.Dungeon, PuzzleRoom3, wallPosition + new IntVector2(1, 0), true);
                    SecretKeyPedestalComponent.gameObject.transform.localScale -= new Vector3(0.7f, 0.7f);
                    // ChaosUtility.GenerateFakeWall(DungeonData.Direction.SOUTH, new IntVector2(14, 20), PuzzleRoom3, updateMinimapOnly: true);
                    ExpandUtility.GenerateFakeWall(DungeonData.Direction.SOUTH, new IntVector2(14, 20), PuzzleRoom3, markAsSecret: true, isGlitched: true);
                }


                if (SecretBossRoom != null) {                    
                    GameObject SpecialRatBoss = DungeonPlaceableUtility.InstantiateDungeonPlaceable(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").gameObject, SecretBossRoom, new IntVector2(17, 28), true, AIActor.AwakenAnimationType.Awaken, true);
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
                        RatBossAIActor.ConfigureOnPlacement(SecretBossRoom);
                    }
                }

                /*DungeonDoorController[] doorObjects = FindObjectsOfType<DungeonDoorController>();

                if (doorObjects != null && doorObjects.Length > 0) {
                    foreach (DungeonDoorController doorObj in doorObjects) {
                        if (doorObj.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith("Giant Elevator Room")) {
                            if (doorObj.doorModules != null && doorObj.doorModules.Length > 0) {
                                FieldInfo field = typeof(DungeonDoorController).GetField("doorClosesAfterEveryOpen", BindingFlags.Instance | BindingFlags.NonPublic);
                                field.SetValue(doorObj, true);
                            }
                        }
                    }
                }*/

            } catch (Exception ex) {
                if (ExpandStats.debugMode) {
                    ETGModConsole.Log("[DEBUG] Warning: Exception caught during object setup phase in secretglithcfloor_flow!");
                }
                Debug.Log("[DEBUG] Warning: Exception caught during object setup phase in secretglithcfloor_flow!");
                Debug.LogException(ex);
            }
            List<AGDEnemyReplacementTier> m_ReplacementTiers = GameManager.Instance.EnemyReplacementTiers;
            if (m_ReplacementTiers != null) {
                ExpandEnemyReplacements.InitReplacementEnemiesAfterSecret(m_ReplacementTiers, GlobalDungeonData.ValidTilesets.FORGEGEON, "_Forge");
                ExpandEnemyReplacements.InitReplacementEnemiesAfterSecret(m_ReplacementTiers, GlobalDungeonData.ValidTilesets.HELLGEON, "_Hell");
            }

            yield return null;
            Destroy(objectDatabase);
            assetBundle = null;
            assetBundle2 = null;
            yield return new WaitForSeconds(1.2f);
            ETGModMainBehaviour.Instance.gameObject.AddComponent<ExpandRatFloorRainController>();
            try { Pixelator.Instance.DeregisterAdditionalRenderPass(ExpandShaders.GlitchScreenShader); } catch (Exception) { }
            yield break;
        }
    }
}

