using Dungeonator;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;


namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class f1b_future_flows {
        
        public static DungeonFlow F1b_Future_Flow_01 {
            get {
                if (!m_f1b_future_flow_01) m_f1b_future_flow_01 = m_F1b_Future_Flow_01();
                return m_f1b_future_flow_01;
            }
        }

        public static DungeonFlow F1b_Future_Flow_02 {
            get {
                if (!m_f1b_future_flow_02) m_f1b_future_flow_02 = m_F1b_Future_Flow_02();
                return m_f1b_future_flow_02;
            }
        }

        private static DungeonFlow m_f1b_future_flow_01;
        private static DungeonFlow m_f1b_future_flow_02;

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

            DungeonFlowNode EntranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, null, ExpandPrefabs.FutureEntranceRoomTable);
            // DungeonFlowNode EntranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandRoomPrefabs.Expand_Future_EntranceRoom_01);

            DungeonFlowNode ConnectorNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, oneWayLoopTarget: true);
            DungeonFlowNode ConnectorNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            
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
            DungeonFlowNode CombatNode_15 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode CombatNode_16 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            
            DungeonFlowNode HubNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);
            DungeonFlowNode HubNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);

            DungeonFlowNode ShopNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandRoomPrefabs.Expand_Future_ShopRoom);

            DungeonFlowNode RewardNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_Future_RewardRoom);
            DungeonFlowNode RewardNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_Future_RewardRoom);
            DungeonFlowNode RewardNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_Future_RewardRoom);

            DungeonFlowNode BossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.Expand_Future_BossRoom);
            DungeonFlowNode BossFoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, null, ExpandPrefabs.FutureFoyerRoomTable);
            DungeonFlowNode ExitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.Expand_Future_ExitRoom);

            // Entrance
            m_CachedFlow.AddNodeToFlow(EntranceNode, null);
            m_CachedFlow.AddNodeToFlow(CombatNode_01, EntranceNode);
            m_CachedFlow.AddNodeToFlow(ConnectorNode_01, CombatNode_01);
            m_CachedFlow.AddNodeToFlow(HubNode_01, ConnectorNode_01);
            // First Loop
            m_CachedFlow.AddNodeToFlow(CombatNode_02, HubNode_01);
            m_CachedFlow.AddNodeToFlow(CombatNode_03, CombatNode_02);
            m_CachedFlow.AddNodeToFlow(CombatNode_04, CombatNode_03);
            m_CachedFlow.AddNodeToFlow(CombatNode_05, CombatNode_04);
            m_CachedFlow.AddNodeToFlow(CombatNode_06, CombatNode_05);
            m_CachedFlow.AddNodeToFlow(RewardNode_01, CombatNode_06);
            m_CachedFlow.LoopConnectNodes(RewardNode_01, ConnectorNode_01);
            // Dead Ends off first Hub
            m_CachedFlow.AddNodeToFlow(CombatNode_07, HubNode_01);
            m_CachedFlow.AddNodeToFlow(CombatNode_08, HubNode_01);

            // Second Loop
            m_CachedFlow.AddNodeToFlow(CombatNode_09, EntranceNode);
            m_CachedFlow.AddNodeToFlow(ConnectorNode_02, CombatNode_09);
            m_CachedFlow.AddNodeToFlow(HubNode_02, ConnectorNode_02);
            m_CachedFlow.AddNodeToFlow(CombatNode_10, HubNode_02);
            m_CachedFlow.AddNodeToFlow(CombatNode_11, CombatNode_10);
            m_CachedFlow.AddNodeToFlow(CombatNode_12, CombatNode_11);
            m_CachedFlow.AddNodeToFlow(CombatNode_13, CombatNode_12);
            m_CachedFlow.AddNodeToFlow(RewardNode_02, CombatNode_13);
            // m_CachedFlow.LoopConnectNodes(RewardNode_02, ConnectorNode_02);
            // Path to Shop and third reward room
            m_CachedFlow.AddNodeToFlow(CombatNode_14, HubNode_02);
            m_CachedFlow.AddNodeToFlow(ShopNode, CombatNode_14);
            m_CachedFlow.AddNodeToFlow(RewardNode_03, CombatNode_14);
            // Path to Boss
            m_CachedFlow.AddNodeToFlow(CombatNode_15, HubNode_02);
            m_CachedFlow.AddNodeToFlow(CombatNode_16, CombatNode_15);
            m_CachedFlow.AddNodeToFlow(BossFoyerNode, CombatNode_16);
            m_CachedFlow.AddNodeToFlow(BossNode, BossFoyerNode);
            m_CachedFlow.AddNodeToFlow(ExitNode, BossNode);
            



            
            
            m_CachedFlow.FirstNode = EntranceNode;

            return m_CachedFlow;
        }

        private static DungeonFlow m_F1b_Future_Flow_02() {
            
            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();
            m_CachedFlow.name = "F1b_Future_Flow_02";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.FutureRoomTable;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { ExpandDungeonFlow.FutureInjectionData };
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.Initialize();


            DungeonFlowNode EntranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandRoomPrefabs.Expand_Future_EntranceRoom_01, oneWayLoopTarget: true);

            DungeonFlowNode ConnectorNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode ConnectorNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);

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
            DungeonFlowNode HubNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);

            DungeonFlowNode ShopNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, ExpandRoomPrefabs.Expand_Future_ShopRoom);

            DungeonFlowNode RewardNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_Future_RewardRoom);
            DungeonFlowNode RewardNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_Future_RewardRoom);
            DungeonFlowNode RewardNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandRoomPrefabs.Expand_Future_RewardRoom);

            DungeonFlowNode BossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.Expand_Future_BossRoom);
            DungeonFlowNode BossFoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, null, ExpandPrefabs.FutureFoyerRoomTable);
            DungeonFlowNode ExitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.Expand_Future_ExitRoom);


            // Entrance // Start of Big Loop
            m_CachedFlow.AddNodeToFlow(EntranceNode, null);
            m_CachedFlow.AddNodeToFlow(ConnectorNode_01, EntranceNode);
            m_CachedFlow.AddNodeToFlow(CombatNode_01, ConnectorNode_01);
            m_CachedFlow.AddNodeToFlow(CombatNode_02, CombatNode_01);
            m_CachedFlow.AddNodeToFlow(CombatNode_03, CombatNode_02);
            m_CachedFlow.AddNodeToFlow(HubNode_01, CombatNode_03);
            m_CachedFlow.AddNodeToFlow(CombatNode_04, CombatNode_03);
            m_CachedFlow.AddNodeToFlow(CombatNode_05, CombatNode_04);
            m_CachedFlow.AddNodeToFlow(CombatNode_06, CombatNode_05);
            m_CachedFlow.AddNodeToFlow(RewardNode_01, CombatNode_06);
            m_CachedFlow.LoopConnectNodes(RewardNode_01, EntranceNode);

            // First Hub Branch to second Reward
            m_CachedFlow.AddNodeToFlow(CombatNode_07, HubNode_01);
            m_CachedFlow.AddNodeToFlow(CombatNode_08, CombatNode_07);
            m_CachedFlow.AddNodeToFlow(RewardNode_02, CombatNode_08);
            m_CachedFlow.AddNodeToFlow(CombatNode_09, CombatNode_08);

            // Second Hub Branch to Shop/SecondHub
            m_CachedFlow.AddNodeToFlow(CombatNode_10, HubNode_01);
            m_CachedFlow.AddNodeToFlow(ShopNode, CombatNode_10);
            m_CachedFlow.AddNodeToFlow(CombatNode_11, CombatNode_10);
            m_CachedFlow.AddNodeToFlow(CombatNode_12, CombatNode_11);

            // Second Hub that branches to Boss and dead ends
            m_CachedFlow.AddNodeToFlow(HubNode_02, CombatNode_12);
            m_CachedFlow.AddNodeToFlow(CombatNode_13, HubNode_02);
            m_CachedFlow.AddNodeToFlow(CombatNode_14, HubNode_02);
            m_CachedFlow.AddNodeToFlow(ConnectorNode_02, HubNode_02);

            // Path to Boss
            m_CachedFlow.AddNodeToFlow(BossFoyerNode, ConnectorNode_02);
            m_CachedFlow.AddNodeToFlow(BossNode, BossFoyerNode);
            m_CachedFlow.AddNodeToFlow(ExitNode, BossNode);
            m_CachedFlow.AddNodeToFlow(RewardNode_03, ExitNode);


            m_CachedFlow.FirstNode = EntranceNode;

            return m_CachedFlow;
        }
    }
}

