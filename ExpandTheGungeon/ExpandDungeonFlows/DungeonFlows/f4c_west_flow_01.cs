using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class f4c_west_flow_01 {

        public static DungeonFlow F4c_West_Flow_01() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();
            m_CachedFlow.name = "F4c_West_Flow_01";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.CustomRoomTableSecretGlitchFloor;
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { ExpandDungeonFlow.CustomSecretFloorSharedInjectionData };
            m_CachedFlow.Initialize();


            DungeonFlowNode entranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandRoomPrefabs.Expand_West_Entrance);
            DungeonFlowNode exitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.SecretExitRoom);
            DungeonFlowNode fakebossFoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.boss_foyertable);
            DungeonFlowNode fakebossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.FakeBossRoom);
            DungeonFlowNode westBossfoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.boss_foyertable);
            DungeonFlowNode westBossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.Expand_West_WestBrosBossRoom);
            
            DungeonFlowNode WestWinchesterNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            List<PrototypeDungeonRoom> m_WinchesterRoomList = new List<PrototypeDungeonRoom>();

            foreach (WeightedRoom weightedRoom in ExpandPrefabs.winchesterroomtable.includedRooms.elements) {
                if (weightedRoom.room != null) { m_WinchesterRoomList.Add(weightedRoom.room); }
            }

            if (m_WinchesterRoomList.Count > 0) {
                m_WinchesterRoomList = m_WinchesterRoomList.Shuffle();
                WestWinchesterNode.overrideExactRoom = BraveUtility.RandomElement(m_WinchesterRoomList);
            }
            
            DungeonFlowNode WestGunMuncherNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandPrefabs.subshop_muncher_01);

            DungeonFlowNode WestShopNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.shop_room_table);
            DungeonFlowNode WestShopBackRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Expand_West_SecretShopWarp, handlesOwnWarping: false);

            DungeonFlowNode WestChestRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_West_ChestRoom);
            DungeonFlowNode WestChestRoom_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_West_ChestRoom);

            DungeonFlowNode WestSecretRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, ExpandRoomPrefabs.Expand_West_SecretKeyShop);
            DungeonFlowNode WestSecretSpacerRoom_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, ExpandRoomPrefabs.Expand_TinySecretEmpty);
            DungeonFlowNode WestSecretWarp_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.Expand_West_SecretWarp, handlesOwnWarping: false);
            DungeonFlowNode WestSecretHub_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB, ExpandRoomPrefabs.Expand_West_SecretHub, isWarpWingNode: true, handlesOwnWarping: false);
            DungeonFlowNode WestSecretHub_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB, ExpandRoomPrefabs.Expand_West_SecretHub2, isWarpWingNode: true, handlesOwnWarping: false);

            DungeonFlowNode WestRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestTinyCanyonRoomTable);
            DungeonFlowNode WestRoom_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestCanyonRoomTable);
            DungeonFlowNode WestRoom_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestTinyCanyonRoomTable);
            DungeonFlowNode WestRoom_04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestTinyCanyonRoomTable);
            DungeonFlowNode WestRoom_05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestTinyCanyonRoomTable);
            DungeonFlowNode WestRoom_06 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestTinyCanyonRoomTable);
            DungeonFlowNode WestRoom_07 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestTinyCanyonRoomTable);
            DungeonFlowNode WestRoom_08 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestCanyonRoomTable);
            DungeonFlowNode WestRoom_09 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestTinyCanyonRoomTable);
            DungeonFlowNode WestRoom_10 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestCanyonRoomTable);
            DungeonFlowNode WestRoom_11 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestTinyCanyonRoomTable);
            DungeonFlowNode WestRoom_12 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestTinyCanyonRoomTable);

            DungeonFlowNode WestRoom_13 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestInterior1RoomTable);
            DungeonFlowNode WestRoom_14 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestInterior1RoomTable);
            DungeonFlowNode WestRoom_15 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestInterior1RoomTable);
            DungeonFlowNode WestRoom_16 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.WestInterior1RoomTable);

            DungeonFlowNode MinibossRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, overrideTable: ExpandPrefabs.MegaMiniBossRoomTable);
            DungeonFlowNode ChallengeShrineRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.MegaChallengeShrineTable);
            DungeonFlowNode ShrineRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandRoomPrefabs.Expand_West_ShrineRoom);
            ShrineRoom_01.forcedDoorType = DungeonFlowNode.ForcedDoorType.LOCKED;
            DungeonFlowNode SpecialRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.basic_special_rooms_noBlackMarket);

            DungeonFlowNode BlankRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Expand_West_BlankPedestalRoom);
            DungeonFlowNode BlankRoom_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Expand_West_BlankPedestalRoom);

            DungeonFlowNode RatKeyRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Expand_West_RatKeyPedestalRoom);
            DungeonFlowNode RatKeyRoom_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Expand_West_RatKeyPedestalRoom);
            DungeonFlowNode SecretRatKeyRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, ExpandRoomPrefabs.Expand_West_SecretRatKeyPedestalRoom);
            DungeonFlowNode SecretRatKeyRoom_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, ExpandRoomPrefabs.Expand_West_SecretRatKeyPedestalRoom);


            DungeonFlowNode PuzzleRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.PuzzleRoom1);
            DungeonFlowNode PuzzleRoom_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.PuzzleRoom2);
            DungeonFlowNode PuzzleRoom_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.PuzzleRoom3);

            DungeonFlowNode m_SpecialRewardNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.SecretRewardRoom);

            DungeonFlowNode m_SecretBossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.GungeoneerMimicBossRoom);
            DungeonFlowNode m_SecretBossFoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandRoomPrefabs.GungeoneerMimicBossFoyerRoom);
            // DungeonFlowNode m_SecretBossFoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.boss_foyertable);
            DungeonFlowNode m_SecretBossExitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.Expand_West_RatKeyPedestalRoom);
            // DungeonFlowNode m_SecretBossExitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandPrefabs.DraGunExitRoom);
            // DungeonFlowNode m_SecretBossEndTimesNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandPrefabs.DraGunEndTimesRoom, isWarpWingNode: true);
            // DungeonFlowNode m_SecretBossShopNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandPrefabs.BlacksmithShop);

            m_CachedFlow.AddNodeToFlow(entranceNode, null);
            m_CachedFlow.AddNodeToFlow(WestRoom_01, entranceNode);            
            m_CachedFlow.AddNodeToFlow(WestRoom_03, entranceNode);
            m_CachedFlow.AddNodeToFlow(WestRoom_04, entranceNode);
            m_CachedFlow.AddNodeToFlow(WestRoom_05, entranceNode);
            m_CachedFlow.AddNodeToFlow(WestRoom_06, entranceNode);
            m_CachedFlow.AddNodeToFlow(WestRoom_07, entranceNode);
            m_CachedFlow.AddNodeToFlow(WestRoom_09, entranceNode);
            m_CachedFlow.AddNodeToFlow(WestRoom_11, entranceNode);
            m_CachedFlow.AddNodeToFlow(WestRoom_12, entranceNode);
            m_CachedFlow.AddNodeToFlow(WestGunMuncherNode, WestRoom_03);
            m_CachedFlow.AddNodeToFlow(WestWinchesterNode, WestRoom_11);

            m_CachedFlow.AddNodeToFlow(WestRoom_08, WestRoom_07);
            m_CachedFlow.AddNodeToFlow(BlankRoom_01, WestRoom_08);

            m_CachedFlow.AddNodeToFlow(BlankRoom_02, WestRoom_05);

            m_CachedFlow.AddNodeToFlow(WestChestRoom_01, WestRoom_01);
            m_CachedFlow.AddNodeToFlow(WestChestRoom_02, WestRoom_09);

            m_CachedFlow.AddNodeToFlow(WestSecretRoom_01, WestChestRoom_02);
            m_CachedFlow.AddNodeToFlow(WestSecretSpacerRoom_02, WestSecretRoom_01);
            m_CachedFlow.AddNodeToFlow(WestSecretWarp_01, WestSecretSpacerRoom_02);
            m_CachedFlow.AddNodeToFlow(WestSecretHub_01, WestSecretWarp_01);

            m_CachedFlow.AddNodeToFlow(SecretRatKeyRoom_01, WestSecretHub_01);
            m_CachedFlow.AddNodeToFlow(WestRoom_13, WestSecretHub_01);
            m_CachedFlow.AddNodeToFlow(ShrineRoom_01, WestRoom_13);
            m_CachedFlow.AddNodeToFlow(SpecialRoom_01, WestRoom_13);

            m_CachedFlow.AddNodeToFlow(ChallengeShrineRoom_01, WestSecretHub_01);
            m_CachedFlow.AddNodeToFlow(WestRoom_14, ChallengeShrineRoom_01);
            m_CachedFlow.AddNodeToFlow(WestRoom_15, WestRoom_14);
            m_CachedFlow.AddNodeToFlow(SecretRatKeyRoom_02, WestRoom_15);

            m_CachedFlow.AddNodeToFlow(WestRoom_16, WestRoom_14);
            m_CachedFlow.AddNodeToFlow(MinibossRoom_01, WestRoom_16);
            m_CachedFlow.AddNodeToFlow(RatKeyRoom_01, MinibossRoom_01);
            
            m_CachedFlow.AddNodeToFlow(WestShopNode, WestRoom_06);
            m_CachedFlow.AddNodeToFlow(WestShopBackRoom_01, WestShopNode);
            m_CachedFlow.AddNodeToFlow(WestSecretHub_02, WestShopBackRoom_01);
            m_CachedFlow.AddNodeToFlow(PuzzleRoom_01, WestSecretHub_02);
            m_CachedFlow.AddNodeToFlow(PuzzleRoom_02, WestSecretHub_02);
            m_CachedFlow.AddNodeToFlow(PuzzleRoom_03, WestSecretHub_02);
            m_CachedFlow.AddNodeToFlow(m_SpecialRewardNode, WestSecretHub_02);
            // m_CachedFlow.AddNodeToFlow(m_SecretBossShopNode, m_SpecialRewardNode);            
            m_CachedFlow.AddNodeToFlow(m_SecretBossFoyerNode, m_SpecialRewardNode);
            // m_CachedFlow.AddNodeToFlow(m_SecretBossFoyerNode, m_SecretBossShopNode);
            m_CachedFlow.AddNodeToFlow(m_SecretBossNode, m_SecretBossFoyerNode);
            m_CachedFlow.AddNodeToFlow(m_SecretBossExitNode, m_SecretBossNode);
            // m_CachedFlow.AddNodeToFlow(m_SecretBossEndTimesNode, m_SecretBossExitNode);

            m_CachedFlow.AddNodeToFlow(WestRoom_02, WestRoom_12);
            m_CachedFlow.AddNodeToFlow(westBossfoyerNode, WestRoom_02);
            m_CachedFlow.AddNodeToFlow(westBossNode, westBossfoyerNode);
            m_CachedFlow.AddNodeToFlow(exitNode, westBossNode);

            m_CachedFlow.AddNodeToFlow(WestRoom_10, WestRoom_04);
            m_CachedFlow.AddNodeToFlow(fakebossFoyerNode, WestRoom_10);
            m_CachedFlow.AddNodeToFlow(fakebossNode, fakebossFoyerNode);
            m_CachedFlow.AddNodeToFlow(RatKeyRoom_02, fakebossNode);

            m_CachedFlow.FirstNode = entranceNode;

            return m_CachedFlow;
        }
    }
}

