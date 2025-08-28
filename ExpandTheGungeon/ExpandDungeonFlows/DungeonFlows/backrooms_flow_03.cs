using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class backrooms_flow_03 {
        
        public static DungeonFlow BackRooms_Flow_03() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();
            
            DungeonFlowNode entranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, null, ExpandPrefabs.BackRoomsEntranceRoomTable, handlesOwnWarping: false);
            
            DungeonFlowNode exitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.Expand_BackRooms_Exit);

            DungeonFlowNode exitWarpNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsRoomTable, isWarpWingNode: true);
            
            m_CachedFlow.name = "BackRooms_Flow_03";
            m_CachedFlow.fallbackRoomTable = null;
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>(0);

            m_CachedFlow.Initialize();

            m_CachedFlow.AddNodeToFlow(entranceNode, null);
            m_CachedFlow.AddNodeToFlow(exitWarpNode, entranceNode);
            m_CachedFlow.AddNodeToFlow(exitNode, exitWarpNode);

            DungeonFlowNode m_FlowNode01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable, isWarpWingNode: true, handlesOwnWarping: false);
            DungeonFlowNode m_FlowNode02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode06 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode07 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode08 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode09 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode10 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            m_CachedFlow.AddNodeToFlow(m_FlowNode01, entranceNode);
            m_CachedFlow.AddNodeToFlow(m_FlowNode02, m_FlowNode01);
            m_CachedFlow.AddNodeToFlow(m_FlowNode03, m_FlowNode02);
            m_CachedFlow.AddNodeToFlow(m_FlowNode04, m_FlowNode03);
            m_CachedFlow.AddNodeToFlow(m_FlowNode05, m_FlowNode04);
            m_CachedFlow.AddNodeToFlow(m_FlowNode06, m_FlowNode05);
            m_CachedFlow.AddNodeToFlow(m_FlowNode07, m_FlowNode06);
            m_CachedFlow.AddNodeToFlow(m_FlowNode08, m_FlowNode07);
            m_CachedFlow.AddNodeToFlow(m_FlowNode09, m_FlowNode08);
            m_CachedFlow.AddNodeToFlow(m_FlowNode10, m_FlowNode09);
            

            m_CachedFlow.FirstNode = entranceNode;
            
            return m_CachedFlow;
        }
    }
}

