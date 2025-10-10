using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class backrooms_flow_02 {
        
        public static DungeonFlow BackRooms_Flow_02 {
            get {
                if (!m_backrooms_flow_02) m_backrooms_flow_02 = m_BackRooms_Flow_02();
                return m_backrooms_flow_02;
            }
        }

        private static DungeonFlow m_backrooms_flow_02;
        
        private static DungeonFlow m_BackRooms_Flow_02() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            DungeonFlowNode entranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, null, ExpandPrefabs.BackRoomsEntranceRoomTable);
            DungeonFlowNode exitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.Expand_BackRooms_Exit);

            DungeonFlowNode exitWarpNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsRoomTable, isWarpWingNode: true);
            
            m_CachedFlow.name = "BackRooms_Flow_02";
            m_CachedFlow.fallbackRoomTable = null;
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>(0);

            m_CachedFlow.Initialize();

            m_CachedFlow.AddNodeToFlow(entranceNode, null);
            m_CachedFlow.AddNodeToFlow(exitWarpNode, entranceNode);
            m_CachedFlow.AddNodeToFlow(exitNode, exitWarpNode);

            DungeonFlowNode m_FlowNode01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
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

            DungeonFlowNode m_FlowSubNode01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode06 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode07 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode08 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode09 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode10 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            m_CachedFlow.AddNodeToFlow(m_FlowSubNode01, m_FlowNode02);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode02, m_FlowSubNode01);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode03, m_FlowSubNode02);

            m_CachedFlow.AddNodeToFlow(m_FlowSubNode04, m_FlowNode04);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode05, m_FlowSubNode04);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode06, m_FlowSubNode06);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode07, m_FlowSubNode07);

            m_CachedFlow.AddNodeToFlow(m_FlowSubNode08, m_FlowSubNode06);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode09, m_FlowSubNode08);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode10, m_FlowSubNode09);


            DungeonFlowNode m_FlowNode11 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode12 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode13 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode14 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode15 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode16 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode17 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode18 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode19 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode20 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            m_CachedFlow.AddNodeToFlow(m_FlowNode11, entranceNode);
            m_CachedFlow.AddNodeToFlow(m_FlowNode12, m_FlowNode11);
            m_CachedFlow.AddNodeToFlow(m_FlowNode13, m_FlowNode12);
            m_CachedFlow.AddNodeToFlow(m_FlowNode14, m_FlowNode13);
            m_CachedFlow.AddNodeToFlow(m_FlowNode15, m_FlowNode14);
            m_CachedFlow.AddNodeToFlow(m_FlowNode16, m_FlowNode15);
            m_CachedFlow.AddNodeToFlow(m_FlowNode17, m_FlowNode16);
            m_CachedFlow.AddNodeToFlow(m_FlowNode18, m_FlowNode17);
            m_CachedFlow.AddNodeToFlow(m_FlowNode19, m_FlowNode18);
            m_CachedFlow.AddNodeToFlow(m_FlowNode20, m_FlowNode19);


            DungeonFlowNode m_FlowSubNode11 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode12 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode13 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode14 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode15 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode16 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode17 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode18 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode19 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode20 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            m_CachedFlow.AddNodeToFlow(m_FlowSubNode11, m_FlowNode13);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode12, m_FlowNode14);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode13, m_FlowNode15);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode14, m_FlowNode16);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode15, m_FlowNode17);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode16, m_FlowNode18);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode17, m_FlowNode19);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode18, m_FlowNode20);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode19, m_FlowNode20);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode20, m_FlowNode20);


            DungeonFlowNode m_FlowNode21 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode22 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode23 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode24 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode25 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode26 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode27 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode28 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode29 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode30 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            m_CachedFlow.AddNodeToFlow(m_FlowNode21, entranceNode);
            m_CachedFlow.AddNodeToFlow(m_FlowNode22, m_FlowNode21);
            m_CachedFlow.AddNodeToFlow(m_FlowNode23, m_FlowNode22);
            m_CachedFlow.AddNodeToFlow(m_FlowNode24, m_FlowNode23);
            m_CachedFlow.AddNodeToFlow(m_FlowNode25, m_FlowNode24);
            m_CachedFlow.AddNodeToFlow(m_FlowNode26, m_FlowNode25);
            m_CachedFlow.AddNodeToFlow(m_FlowNode27, m_FlowNode26);
            m_CachedFlow.AddNodeToFlow(m_FlowNode28, m_FlowNode27);
            m_CachedFlow.AddNodeToFlow(m_FlowNode29, m_FlowNode28);
            m_CachedFlow.AddNodeToFlow(m_FlowNode30, m_FlowNode29);


            DungeonFlowNode m_FlowSubNode21 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode22 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode23 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode24 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode25 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);


            m_CachedFlow.AddNodeToFlow(m_FlowSubNode21, m_FlowNode24);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode22, m_FlowSubNode21);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode23, m_FlowSubNode22);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode24, m_FlowSubNode23);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode25, m_FlowSubNode23);

            DungeonFlowNode m_FlowSubNode26 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode27 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode28 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode29 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode30 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            m_CachedFlow.AddNodeToFlow(m_FlowSubNode26, entranceNode);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode27, entranceNode);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode28, entranceNode);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode29, exitWarpNode);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode30, m_FlowSubNode29);

            
            DungeonFlowNode m_FlowNode31 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode32 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode33 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode34 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode35 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode36 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode37 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode38 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode39 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode40 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode41 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode42 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode43 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode44 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode45 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowNode46 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            m_CachedFlow.AddNodeToFlow(m_FlowNode31, entranceNode);
            m_CachedFlow.AddNodeToFlow(m_FlowNode32, m_FlowNode31);
            m_CachedFlow.AddNodeToFlow(m_FlowNode33, m_FlowNode32);
            m_CachedFlow.AddNodeToFlow(m_FlowNode34, m_FlowNode33);
            m_CachedFlow.AddNodeToFlow(m_FlowNode35, m_FlowNode34);
            m_CachedFlow.AddNodeToFlow(m_FlowNode36, m_FlowNode35);
            m_CachedFlow.AddNodeToFlow(m_FlowNode37, m_FlowNode36);
            m_CachedFlow.AddNodeToFlow(m_FlowNode38, m_FlowNode37);
            m_CachedFlow.AddNodeToFlow(m_FlowNode39, m_FlowNode38);
            m_CachedFlow.AddNodeToFlow(m_FlowNode40, m_FlowNode39);
            m_CachedFlow.AddNodeToFlow(m_FlowNode41, m_FlowNode40);
            m_CachedFlow.AddNodeToFlow(m_FlowNode42, m_FlowNode41);
            m_CachedFlow.AddNodeToFlow(m_FlowNode43, m_FlowNode42);
            m_CachedFlow.AddNodeToFlow(m_FlowNode44, m_FlowNode43);
            m_CachedFlow.AddNodeToFlow(m_FlowNode45, m_FlowNode44);

            DungeonFlowNode m_FlowSubNode31 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode32 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode33 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode34 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode35 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode36 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode37 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode38 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode39 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode m_FlowSubNode40 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            m_CachedFlow.AddNodeToFlow(m_FlowSubNode31, m_FlowNode36);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode32, m_FlowNode37);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode33, m_FlowNode38);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode34, m_FlowNode39);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode35, m_FlowNode40);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode36, m_FlowNode41);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode37, m_FlowNode42);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode38, m_FlowNode43);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode39, m_FlowNode44);
            m_CachedFlow.AddNodeToFlow(m_FlowSubNode40, m_FlowNode45);


            m_CachedFlow.FirstNode = entranceNode;
            
            return m_CachedFlow;
        }
    }
}

