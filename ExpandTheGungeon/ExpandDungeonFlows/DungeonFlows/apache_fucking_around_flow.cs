using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class apache_fucking_around_flow {
        
        public static DungeonFlow Apache_Fucking_Around_Flow() {

            DungeonFlowSubtypeRestriction m_SubTypeRestrictions = new DungeonFlowSubtypeRestriction() {
                baseCategoryRestriction = PrototypeDungeonRoom.RoomCategory.NORMAL,
                normalSubcategoryRestriction = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP,
                bossSubcategoryRestriction = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS,
                specialSubcategoryRestriction = PrototypeDungeonRoom.RoomSpecialSubCategory.UNSPECIFIED_SPECIAL,
                secretSubcategoryRestriction = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET,
                maximumRoomsOfSubtype = 1
            };

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            bool SecretRoomSelector = BraveUtility.RandomBool();

            m_CachedFlow.name = "Apache_Fucking_Around_Flow";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.CustomRoomTable2;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>() { m_SubTypeRestrictions };
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>(0);
            
            m_CachedFlow.Initialize();

            DungeonFlowNode m_EntranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandPrefabs.elevator_entrance);
            DungeonFlowNode m_ExitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandPrefabs.tiny_exit);
            DungeonFlowNode m_BossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandPrefabs.doublebeholsterroom01);
            DungeonFlowNode m_BossFoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.boss_foyer);

            // First chain of nodes starting off Entrance
            DungeonFlowNode m_ConnectorNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode m_ConnectorNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode m_ConnectorNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode m_ConnectorNode_04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode m_ShopNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.shop_room_table);
            DungeonFlowNode m_HubNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);
            DungeonFlowNode m_RewardNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room);
            DungeonFlowNode m_RewardNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room);
            DungeonFlowNode m_RewardNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.gungeon_rewardroom_1);
            DungeonFlowNode m_NormalNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_NormalNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);            
            DungeonFlowNode m_NormalNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_NormalNode_04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_NormalNode_05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_NormalNode_06 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_NormalNode_07 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_NormalNode_08 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_NormalNode_09 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_NormalNode_10 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_SecretNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET);
            DungeonFlowNode m_SecretNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET);
            DungeonFlowNode m_LoopTargetNormalNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, oneWayLoopTarget: true);

            // Warp Wing chain
            DungeonFlowNode m_WarpWingConnectorNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Expand_SecretElevatorDestinationRoom, isWarpWingNode: true);
            DungeonFlowNode m_WarpWingConnectorNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode m_WarpWingConnectorNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode m_WarpWingConnectorNode_04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode m_WarpWingNormalNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_WarpWingNormalNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_WarpWingNormalNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode m_WarpWingRewardNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room);
            
            // Assign special elevator entrance room to one of the two guranteed secret rooms on this flow.
            if (SecretRoomSelector) {
                m_SecretNode_01.overrideExactRoom = ExpandRoomPrefabs.Expand_SecretElevatorEntranceRoom;
            } else {
                m_SecretNode_02.overrideExactRoom = ExpandRoomPrefabs.Expand_SecretElevatorEntranceRoom;
            }

            m_CachedFlow.AddNodeToFlow(m_EntranceNode, null);
            m_CachedFlow.AddNodeToFlow(m_ConnectorNode_01, m_EntranceNode);
            m_CachedFlow.AddNodeToFlow(m_ConnectorNode_02, m_ConnectorNode_01);
            m_CachedFlow.AddNodeToFlow(m_ConnectorNode_03, m_ConnectorNode_02);
            // Shop node branching off first connector node
            m_CachedFlow.AddNodeToFlow(m_ShopNode, m_ConnectorNode_01);
            // Huh node. A chest node then normal node leading to boss room/exit with a normal node and loop nodes connected to exit.
            m_CachedFlow.AddNodeToFlow(m_HubNode, m_ConnectorNode_01);
            m_CachedFlow.AddNodeToFlow(m_RewardNode_01, m_HubNode);
            m_CachedFlow.AddNodeToFlow(m_NormalNode_01, m_HubNode);
            m_CachedFlow.AddNodeToFlow(m_BossFoyerNode, m_NormalNode_01);
            m_CachedFlow.AddNodeToFlow(m_BossNode, m_BossFoyerNode);
            m_CachedFlow.AddNodeToFlow(m_ExitNode, m_BossNode);
            m_CachedFlow.AddNodeToFlow(m_NormalNode_02, m_ExitNode);
            m_CachedFlow.AddNodeToFlow(m_LoopTargetNormalNode, m_ExitNode);
            m_CachedFlow.AddNodeToFlow(m_NormalNode_03, m_LoopTargetNormalNode);
            m_CachedFlow.AddNodeToFlow(m_RewardNode_02, m_NormalNode_03);
            // Connect end of this chain back to first node in loop chain.
            m_CachedFlow.LoopConnectNodes(m_RewardNode_02, m_LoopTargetNormalNode);


            // Branch of nodes that leads off hub to mainly normal rooms, a chest and a couple secret rooms.
            m_CachedFlow.AddNodeToFlow(m_NormalNode_04, m_HubNode);
            m_CachedFlow.AddNodeToFlow(m_NormalNode_05, m_NormalNode_04);
            m_CachedFlow.AddNodeToFlow(m_NormalNode_06, m_NormalNode_05);
            m_CachedFlow.AddNodeToFlow(m_NormalNode_07, m_NormalNode_06);
            m_CachedFlow.AddNodeToFlow(m_SecretNode_01, m_NormalNode_06);

            m_CachedFlow.AddNodeToFlow(m_NormalNode_08, m_NormalNode_05);
            m_CachedFlow.AddNodeToFlow(m_NormalNode_09, m_NormalNode_08);
            m_CachedFlow.AddNodeToFlow(m_NormalNode_10, m_NormalNode_09);
            m_CachedFlow.AddNodeToFlow(m_SecretNode_02, m_NormalNode_10);
            m_CachedFlow.AddNodeToFlow(m_RewardNode_03, m_NormalNode_10);

            // Warpwing CHain of nodes with 1 reward room
            m_CachedFlow.AddNodeToFlow(m_WarpWingConnectorNode_01, m_HubNode);
            m_CachedFlow.AddNodeToFlow(m_WarpWingNormalNode_01, m_WarpWingConnectorNode_01);
            m_CachedFlow.AddNodeToFlow(m_WarpWingRewardNode_01, m_WarpWingNormalNode_01);
            m_CachedFlow.AddNodeToFlow(m_WarpWingConnectorNode_02, m_WarpWingConnectorNode_01);
            m_CachedFlow.AddNodeToFlow(m_WarpWingNormalNode_02, m_WarpWingConnectorNode_02);
            m_CachedFlow.AddNodeToFlow(m_WarpWingConnectorNode_03, m_WarpWingNormalNode_02);
            m_CachedFlow.AddNodeToFlow(m_WarpWingConnectorNode_04, m_WarpWingConnectorNode_03);
            m_CachedFlow.AddNodeToFlow(m_WarpWingNormalNode_03, m_WarpWingConnectorNode_04);


            m_CachedFlow.FirstNode = m_EntranceNode;

            return m_CachedFlow;
        }
    }
}

