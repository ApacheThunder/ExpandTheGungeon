using Dungeonator;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;


namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class f1b_future_flow_01 {
        
        public static DungeonFlow F1b_Future_Flow_01 {
            get {
                if (!m_f1b_future_flow_01) m_f1b_future_flow_01 = m_F1b_Future_Flow_01();
                return m_f1b_future_flow_01;
            }
        }

        private static DungeonFlow m_f1b_future_flow_01;

        private static DungeonFlow m_F1b_Future_Flow_01() {
            
            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();
            m_CachedFlow.name = "F1b_Future_Flow_01";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.FutureRoomTable;
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() {
                ExpandDungeonFlow.BaseSharedInjectionData,
                ExpandDungeonFlow.FutureInjectionData
            };

            m_CachedFlow.Initialize();

            DungeonFlowNode EntranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandRoomPrefabs.Expand_Future_EntranceRoom);
            DungeonFlowNode BossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.Expand_Future_BossRoom);
            DungeonFlowNode BossFoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, null, ExpandPrefabs.FutureFoyerRoomTable);            
            DungeonFlowNode ExitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.Expand_Future_ExitRoom);

            DungeonFlowNode ConnectorNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode ConnectorNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode ConnectorNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode ConnectorNode_04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode ConnectorNode_05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);

            DungeonFlowNode CombatNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_06 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_07 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_08 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_09 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_10 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_11 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_12 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_13 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_14 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);

            DungeonFlowNode HubNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);

            DungeonFlowNode ShopNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandRoomPrefabs.Expand_Future_ShopRoom);

            DungeonFlowNode RewardNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_Future_RewardRoom, null, true);
            DungeonFlowNode RewardNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_Future_RewardRoom);
            DungeonFlowNode RewardNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_Future_RewardRoom);


            m_CachedFlow.AddNodeToFlow(EntranceNode, null);
            m_CachedFlow.AddNodeToFlow(ConnectorNode_01, CombatNode_05);
            m_CachedFlow.AddNodeToFlow(BossFoyerNode, ConnectorNode_01);
            m_CachedFlow.AddNodeToFlow(BossNode, BossFoyerNode);
            m_CachedFlow.AddNodeToFlow(ExitNode, BossNode);
            m_CachedFlow.AddNodeToFlow(RewardNode_03, ExitNode);
            m_CachedFlow.AddNodeToFlow(CombatNode_01, EntranceNode);
            m_CachedFlow.AddNodeToFlow(RewardNode_01, CombatNode_01);
            m_CachedFlow.AddNodeToFlow(ConnectorNode_02, CombatNode_06);
            m_CachedFlow.AddNodeToFlow(CombatNode_02, ConnectorNode_02);
            m_CachedFlow.AddNodeToFlow(CombatNode_03, HubNode_01);
            m_CachedFlow.AddNodeToFlow(CombatNode_04, CombatNode_03);
            m_CachedFlow.AddNodeToFlow(CombatNode_05, CombatNode_04);
            m_CachedFlow.AddNodeToFlow(CombatNode_06, RewardNode_01);
            m_CachedFlow.AddNodeToFlow(ConnectorNode_03, CombatNode_06);
            m_CachedFlow.AddNodeToFlow(CombatNode_07, ConnectorNode_03);
            m_CachedFlow.AddNodeToFlow(CombatNode_08, HubNode_01);
            m_CachedFlow.AddNodeToFlow(CombatNode_09, CombatNode_02);
            m_CachedFlow.AddNodeToFlow(ConnectorNode_04, CombatNode_08);
            m_CachedFlow.AddNodeToFlow(CombatNode_10, CombatNode_05);
            m_CachedFlow.AddNodeToFlow(CombatNode_11, CombatNode_10);
            m_CachedFlow.AddNodeToFlow(CombatNode_12, CombatNode_11);
            m_CachedFlow.AddNodeToFlow(ConnectorNode_05, CombatNode_12);
            m_CachedFlow.AddNodeToFlow(HubNode_01, CombatNode_09);
            m_CachedFlow.AddNodeToFlow(ShopNode, CombatNode_02);
            m_CachedFlow.AddNodeToFlow(CombatNode_13, CombatNode_04);
            m_CachedFlow.AddNodeToFlow(CombatNode_14, CombatNode_11);
            m_CachedFlow.AddNodeToFlow(RewardNode_02, CombatNode_14);

            m_CachedFlow.LoopConnectNodes(CombatNode_07, RewardNode_01);

            m_CachedFlow.FirstNode = EntranceNode;

            return m_CachedFlow;
        }
    }
}

