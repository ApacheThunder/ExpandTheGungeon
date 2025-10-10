using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class backrooms_flow_03 {
        
        public static DungeonFlow BackRooms_Flow_03 {
            get {
                if (!m_backrooms_flow_03) m_backrooms_flow_03 = m_BackRooms_Flow_03();
                return m_backrooms_flow_03;
            }
        }

        private static DungeonFlow m_backrooms_flow_03;
        
        private static DungeonFlow m_BackRooms_Flow_03() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();
            
            DungeonFlowNode entranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandRoomPrefabs.Expand_BackRooms_Entrance_WarpWing, handlesOwnWarping: false);
            
            DungeonFlowNode exitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.Expand_BackRooms_Exit);

            DungeonFlowNode exitWarpNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsRoomTable, isWarpWingNode: true);

            DungeonFlowNode m_FlowWarpEntranceNode01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpEntranceNode02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpEntranceNode03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpEntranceNode04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpEntranceNode05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, handlesOwnWarping: false);
                        
            DungeonFlowNode m_FlowWarpDestinationNode01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, isWarpWingNode: true, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpDestinationNode02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, isWarpWingNode: true, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpDestinationNode03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, isWarpWingNode: true, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpDestinationNode04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, isWarpWingNode: true, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpDestinationNode05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, isWarpWingNode: true, handlesOwnWarping: false);
            

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
           

            // Chain 1
            DungeonFlowNode m_Node01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node06 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node07 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node08 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node09 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node10 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            DungeonFlowNode m_SubNode01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode06 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode07 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode08 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode09 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode10 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            DungeonFlowNode m_FlowWarpEntranceSubNode01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpEntranceSubNode02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpDestinatioSubNode01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, isWarpWingNode: true, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpDestinatioSubNode02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, isWarpWingNode: true, handlesOwnWarping: false);

            m_CachedFlow.AddNodeToFlow(m_FlowWarpEntranceNode01, entranceNode);

            m_CachedFlow.AddNodeToFlow(m_FlowWarpDestinationNode01, m_FlowWarpEntranceNode01);
            m_CachedFlow.AddNodeToFlow(m_Node01, m_FlowWarpDestinationNode01);
            m_CachedFlow.AddNodeToFlow(m_Node02, m_Node01);
            m_CachedFlow.AddNodeToFlow(m_Node03, m_Node02);
            m_CachedFlow.AddNodeToFlow(m_Node04, m_Node03);
            m_CachedFlow.AddNodeToFlow(m_Node05, m_Node03);
            m_CachedFlow.AddNodeToFlow(m_Node06, m_Node05);
            m_CachedFlow.AddNodeToFlow(m_Node07, m_Node05);
            m_CachedFlow.AddNodeToFlow(m_Node08, m_Node07);
            m_CachedFlow.AddNodeToFlow(m_Node09, m_Node08);
            m_CachedFlow.AddNodeToFlow(m_FlowWarpEntranceSubNode01, m_Node06);
            // Warp Wing Subchain 1
            m_CachedFlow.AddNodeToFlow(m_FlowWarpDestinatioSubNode01, m_FlowWarpEntranceSubNode01);
            m_CachedFlow.AddNodeToFlow(m_SubNode01, m_FlowWarpDestinatioSubNode01);
            m_CachedFlow.AddNodeToFlow(m_SubNode02, m_SubNode01);
            m_CachedFlow.AddNodeToFlow(m_SubNode03, m_SubNode02);
            m_CachedFlow.AddNodeToFlow(m_SubNode04, m_SubNode03);
            m_CachedFlow.AddNodeToFlow(m_FlowWarpEntranceSubNode02, m_Node03);
            m_CachedFlow.AddNodeToFlow(m_SubNode05, m_FlowWarpEntranceSubNode02);
            // Warp Wing SUbchain 2
            m_CachedFlow.AddNodeToFlow(m_FlowWarpDestinatioSubNode02, m_FlowWarpEntranceSubNode02);
            m_CachedFlow.AddNodeToFlow(m_SubNode06, m_FlowWarpDestinatioSubNode02);
            m_CachedFlow.AddNodeToFlow(m_SubNode07, m_SubNode06);
            m_CachedFlow.AddNodeToFlow(m_SubNode08, m_SubNode07);
            m_CachedFlow.AddNodeToFlow(m_SubNode09, m_FlowWarpDestinatioSubNode02);
            m_CachedFlow.AddNodeToFlow(m_SubNode10, m_FlowWarpDestinatioSubNode02);


            // Chain 2
            DungeonFlowNode m_Node11 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node12 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node13 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node14 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node15 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node16 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node17 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node18 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node19 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node20 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            DungeonFlowNode m_SubNode11 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode12 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode13 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode14 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode15 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode16 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode17 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode18 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode19 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode20 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            DungeonFlowNode m_FlowWarpEntranceSubNode03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpDestinatioSubNode03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, isWarpWingNode: true, handlesOwnWarping: false);

            m_CachedFlow.AddNodeToFlow(m_FlowWarpEntranceNode02, entranceNode);
            
            m_CachedFlow.AddNodeToFlow(m_FlowWarpDestinationNode02, m_FlowWarpEntranceNode02);
            m_CachedFlow.AddNodeToFlow(m_Node11, m_FlowWarpDestinationNode02);
            m_CachedFlow.AddNodeToFlow(m_Node12, m_Node11);
            m_CachedFlow.AddNodeToFlow(m_Node13, m_Node12);
            m_CachedFlow.AddNodeToFlow(m_Node14, m_Node13);
            m_CachedFlow.AddNodeToFlow(m_Node15, m_Node14);
            m_CachedFlow.AddNodeToFlow(m_Node16, m_Node15);
            m_CachedFlow.AddNodeToFlow(m_Node17, m_Node15);
            m_CachedFlow.AddNodeToFlow(m_Node18, m_Node17);
            m_CachedFlow.AddNodeToFlow(m_Node19, m_Node18);
            m_CachedFlow.AddNodeToFlow(m_Node20, m_Node19);
            m_CachedFlow.AddNodeToFlow(m_FlowWarpEntranceSubNode03, m_Node17);
            // Warp Wing Subchain 1
            m_CachedFlow.AddNodeToFlow(m_FlowWarpDestinatioSubNode03, m_FlowWarpEntranceSubNode03);
            m_CachedFlow.AddNodeToFlow(m_SubNode11, m_FlowWarpDestinatioSubNode03);
            m_CachedFlow.AddNodeToFlow(m_SubNode12, m_SubNode11);
            m_CachedFlow.AddNodeToFlow(m_SubNode13, m_SubNode12);
            m_CachedFlow.AddNodeToFlow(m_SubNode14, m_SubNode13);
            m_CachedFlow.AddNodeToFlow(m_SubNode15, m_SubNode13);
            m_CachedFlow.AddNodeToFlow(m_SubNode16, m_SubNode15);
            m_CachedFlow.AddNodeToFlow(m_SubNode17, m_SubNode16);
            m_CachedFlow.AddNodeToFlow(m_SubNode18, m_SubNode16);
            m_CachedFlow.AddNodeToFlow(m_SubNode19, m_SubNode18);
            m_CachedFlow.AddNodeToFlow(m_SubNode20, m_SubNode19);




            // Chain 3
            DungeonFlowNode m_Node21 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node22 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node23 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node24 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node25 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node26 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node27 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node28 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node29 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node30 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            DungeonFlowNode m_SubNode21 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode22 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode23 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode24 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode25 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode26 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode27 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode28 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode29 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode30 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            DungeonFlowNode m_FlowWarpEntranceSubNode04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpDestinatioSubNode04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, isWarpWingNode: true, handlesOwnWarping: false);


            m_CachedFlow.AddNodeToFlow(m_FlowWarpEntranceNode03, entranceNode);

            m_CachedFlow.AddNodeToFlow(m_FlowWarpDestinationNode03, m_FlowWarpEntranceNode03);
            m_CachedFlow.AddNodeToFlow(m_Node21, m_FlowWarpDestinationNode03);
            m_CachedFlow.AddNodeToFlow(m_Node22, m_Node21);
            m_CachedFlow.AddNodeToFlow(m_Node23, m_Node22);
            m_CachedFlow.AddNodeToFlow(m_Node24, m_Node23);
            m_CachedFlow.AddNodeToFlow(m_Node25, m_Node24);
            m_CachedFlow.AddNodeToFlow(m_Node26, m_Node25);
            m_CachedFlow.AddNodeToFlow(m_Node27, m_Node25);
            m_CachedFlow.AddNodeToFlow(m_Node28, m_Node27);
            m_CachedFlow.AddNodeToFlow(m_Node29, m_Node27);
            m_CachedFlow.AddNodeToFlow(m_Node30, m_Node29);
            m_CachedFlow.AddNodeToFlow(m_FlowWarpEntranceSubNode04, m_Node30);

            // Warp Wing Subchain 1
            m_CachedFlow.AddNodeToFlow(m_FlowWarpDestinatioSubNode04, m_FlowWarpEntranceSubNode04);
            m_CachedFlow.AddNodeToFlow(m_SubNode21, m_FlowWarpDestinatioSubNode04);
            m_CachedFlow.AddNodeToFlow(m_SubNode22, m_SubNode21);
            m_CachedFlow.AddNodeToFlow(m_SubNode23, m_SubNode22);
            m_CachedFlow.AddNodeToFlow(m_SubNode24, m_SubNode23);
            m_CachedFlow.AddNodeToFlow(m_SubNode25, m_SubNode24);
            m_CachedFlow.AddNodeToFlow(m_SubNode26, m_SubNode25);
            m_CachedFlow.AddNodeToFlow(m_SubNode27, m_SubNode26);
            m_CachedFlow.AddNodeToFlow(m_SubNode28, m_SubNode26);
            m_CachedFlow.AddNodeToFlow(m_SubNode29, m_SubNode28);
            m_CachedFlow.AddNodeToFlow(m_SubNode30, m_SubNode29);

            // Chain 4
            DungeonFlowNode m_Node31 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node32 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node33 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node34 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node35 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node36 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node37 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node38 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node39 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node40 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            DungeonFlowNode m_SubNode31 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode32 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode33 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode34 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode35 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode36 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode37 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode38 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode39 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode40 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            m_CachedFlow.AddNodeToFlow(m_FlowWarpEntranceNode04, entranceNode);

            m_CachedFlow.AddNodeToFlow(m_FlowWarpDestinationNode04, m_FlowWarpEntranceNode04);
            m_CachedFlow.AddNodeToFlow(m_Node31, m_FlowWarpDestinationNode04);
            m_CachedFlow.AddNodeToFlow(m_Node32, m_Node31);
            m_CachedFlow.AddNodeToFlow(m_Node33, m_Node32);
            m_CachedFlow.AddNodeToFlow(m_Node34, m_Node33);
            m_CachedFlow.AddNodeToFlow(m_Node35, m_Node34);
            m_CachedFlow.AddNodeToFlow(m_Node36, m_Node35);
            m_CachedFlow.AddNodeToFlow(m_Node37, m_Node36);
            m_CachedFlow.AddNodeToFlow(m_Node38, m_Node37);
            m_CachedFlow.AddNodeToFlow(m_Node39, m_Node38);
            m_CachedFlow.AddNodeToFlow(m_Node40, m_Node39);

            // Sub Chain 1
            m_CachedFlow.AddNodeToFlow(m_SubNode31, m_Node32);
            m_CachedFlow.AddNodeToFlow(m_SubNode32, m_SubNode31);
            m_CachedFlow.AddNodeToFlow(m_SubNode33, m_SubNode32);
            // Sub Chain 2
            m_CachedFlow.AddNodeToFlow(m_SubNode34, m_Node35);
            m_CachedFlow.AddNodeToFlow(m_SubNode35, m_SubNode34);
            m_CachedFlow.AddNodeToFlow(m_SubNode36, m_SubNode35);
            // Sub Chain 3
            m_CachedFlow.AddNodeToFlow(m_SubNode36, m_Node37);
            m_CachedFlow.AddNodeToFlow(m_SubNode37, m_SubNode36);
            m_CachedFlow.AddNodeToFlow(m_SubNode38, m_SubNode37);
            // Sub Chain 3
            m_CachedFlow.AddNodeToFlow(m_SubNode39, m_Node39);
            m_CachedFlow.AddNodeToFlow(m_SubNode40, m_SubNode39);

            // Chain 5 (final)
            DungeonFlowNode m_Node41 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node42 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node43 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node44 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_Node45 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            
            DungeonFlowNode m_SubNode41 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode42 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode43 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode44 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_SubNode45 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            DungeonFlowNode m_FlowWarpEntranceSubNode05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, handlesOwnWarping: false);
            DungeonFlowNode m_FlowWarpDestinatioSubNode05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsWarpWingTable, isWarpWingNode: true, handlesOwnWarping: false);
                        
            m_CachedFlow.AddNodeToFlow(m_FlowWarpEntranceNode05, entranceNode);

            m_CachedFlow.AddNodeToFlow(m_FlowWarpDestinationNode05, m_FlowWarpEntranceNode05);
            m_CachedFlow.AddNodeToFlow(m_Node41, m_FlowWarpDestinationNode05);
            m_CachedFlow.AddNodeToFlow(m_Node42, m_Node41);
            m_CachedFlow.AddNodeToFlow(m_Node43, m_Node42);
            m_CachedFlow.AddNodeToFlow(m_Node44, m_Node43);
            m_CachedFlow.AddNodeToFlow(m_Node45, m_Node44);
            m_CachedFlow.AddNodeToFlow(m_FlowWarpEntranceSubNode05, m_Node44);

            // Warp Wing Subchain 1
            m_CachedFlow.AddNodeToFlow(m_FlowWarpDestinatioSubNode05, m_FlowWarpEntranceSubNode05);
            m_CachedFlow.AddNodeToFlow(m_SubNode41, m_FlowWarpDestinatioSubNode05);
            m_CachedFlow.AddNodeToFlow(m_SubNode42, m_SubNode41);
            m_CachedFlow.AddNodeToFlow(m_SubNode43, m_SubNode41);
            m_CachedFlow.AddNodeToFlow(m_SubNode44, m_SubNode43);
            m_CachedFlow.AddNodeToFlow(m_SubNode45, m_SubNode43);

            m_CachedFlow.FirstNode = entranceNode;
            
            return m_CachedFlow;
        }
    }
}

