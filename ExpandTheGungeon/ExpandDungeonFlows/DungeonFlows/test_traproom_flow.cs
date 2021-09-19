using Dungeonator;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class test_traproom_flow : ExpandDungeonFlow {
        
        public static GenericRoomTable TrapRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();

        public static DungeonFlow Test_TrapRoom_Flow() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            DungeonFlowNode entranceNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandPrefabs.tiny_entrance);
            DungeonFlowNode exitNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.SecretExitRoom);
            DungeonFlowNode firstConnectorNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom);
            DungeonFlowNode lastConnectorNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom);

            DungeonFlowNode TrapRoomNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_03 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_04 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_05 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_06 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_07 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_08 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_09 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_10 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_11 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_12 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_13 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_14 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_15 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_16 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_17 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_18 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_19 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_20 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_21 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_22 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_23 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_24 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode TrapRoomNode_25 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            
            TrapRoomTable.name = "Test Trap Room Table";
            TrapRoomTable.includedRooms = new WeightedRoomCollection();
            TrapRoomTable.includedRooms.elements = new List<WeightedRoom>();
            TrapRoomTable.includedRoomTables = new List<GenericRoomTable>(0);

            foreach (WeightedRoom weightedRoom in ExpandPrefabs.CustomRoomTable.includedRooms.elements) {
                if (weightedRoom.room != null && weightedRoom.room.category == PrototypeDungeonRoom.RoomCategory.NORMAL && 
                    weightedRoom.room.subCategoryNormal == PrototypeDungeonRoom.RoomNormalSubCategory.TRAP)
                {
                    TrapRoomTable.includedRooms.elements.Add(weightedRoom);
                }                
            }

            m_CachedFlow.name = "Test_TrapRoom_Flow";
            m_CachedFlow.fallbackRoomTable = TrapRoomTable;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>(0);
            m_CachedFlow.Initialize();

            m_CachedFlow.AddNodeToFlow(entranceNode, null);
            m_CachedFlow.AddNodeToFlow(firstConnectorNode, entranceNode);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_01, firstConnectorNode);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_02, TrapRoomNode_01);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_03, TrapRoomNode_02);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_04, TrapRoomNode_03);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_05, TrapRoomNode_04);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_06, TrapRoomNode_05);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_07, TrapRoomNode_06);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_08, TrapRoomNode_07);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_09, TrapRoomNode_08);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_10, TrapRoomNode_09);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_11, TrapRoomNode_10);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_12, TrapRoomNode_11);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_13, TrapRoomNode_12);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_14, TrapRoomNode_13);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_15, TrapRoomNode_14);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_16, TrapRoomNode_15);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_17, TrapRoomNode_16);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_18, TrapRoomNode_17);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_19, TrapRoomNode_18);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_20, TrapRoomNode_19);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_21, TrapRoomNode_20);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_22, TrapRoomNode_21);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_23, TrapRoomNode_22);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_24, TrapRoomNode_23);
            m_CachedFlow.AddNodeToFlow(TrapRoomNode_25, TrapRoomNode_24);
            m_CachedFlow.AddNodeToFlow(lastConnectorNode, TrapRoomNode_25);
            m_CachedFlow.AddNodeToFlow(exitNode, lastConnectorNode);
            m_CachedFlow.FirstNode = entranceNode;

            return m_CachedFlow;
        }
    }
}

