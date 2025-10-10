using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class backrooms_flow_01 {

        public static DungeonFlow BackRooms_Flow_01 {
            get {
                if (!m_backrooms_flow_01) m_backrooms_flow_01 = m_BackRooms_Flow_01();
                return m_backrooms_flow_01;
            }
        }

        private static DungeonFlow m_backrooms_flow_01;
        
        private static DungeonFlow m_BackRooms_Flow_01() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            DungeonFlowNode entranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, null, ExpandPrefabs.BackRoomsEntranceRoomTable);
            DungeonFlowNode exitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.Expand_BackRooms_Exit);

            DungeonFlowNode exitWarpNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, null, ExpandPrefabs.BackRoomsRoomTable, isWarpWingNode: true);
                        
            DungeonFlowNode BackRoomNode_001 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_002 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_003 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_004 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_005 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_006 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_007 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_008 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_009 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_010 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_011 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_012 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_013 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_014 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_015 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_016 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_017 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_018 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_019 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_020 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_021 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_022 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_023 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_024 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_025 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_026 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_027 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_028 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_029 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_030 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_031 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_032 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_033 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_034 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_035 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_036 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_037 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_038 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_039 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            DungeonFlowNode BackRoomNode_040 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_041 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_042 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_043 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_044 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_045 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_046 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_047 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_048 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_049 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            
            DungeonFlowNode BackRoomNode_050 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_051 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_052 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_053 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_054 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_055 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_056 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_057 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_058 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_059 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            
            DungeonFlowNode BackRoomNode_060 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_061 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_062 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_063 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_064 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_065 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_066 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_067 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_068 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_069 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            
            DungeonFlowNode BackRoomNode_070 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_071 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_072 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_073 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_074 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_075 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_076 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_077 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_078 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_079 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);

            DungeonFlowNode BackRoomNode_080 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_081 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_082 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_083 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_084 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_085 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_086 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_087 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_088 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_089 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);
            DungeonFlowNode BackRoomNode_090 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, null, ExpandPrefabs.BackRoomsRoomTable);


            m_CachedFlow.name = "BackRooms_Flow_01";
            m_CachedFlow.fallbackRoomTable = null;
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>(0);
            // m_CachedFlow.sharedInjectionData.Add(ExpandDungeonFlow.HollowsInjectionData);

            m_CachedFlow.Initialize();
            
            m_CachedFlow.AddNodeToFlow(entranceNode, null);
            m_CachedFlow.AddNodeToFlow(exitWarpNode, entranceNode);
            m_CachedFlow.AddNodeToFlow(exitNode, exitWarpNode);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_001, entranceNode);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_002, entranceNode);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_003, entranceNode);            
            m_CachedFlow.AddNodeToFlow(BackRoomNode_004, entranceNode);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_005, BackRoomNode_004);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_006, BackRoomNode_005);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_007, BackRoomNode_006);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_008, BackRoomNode_007);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_009, BackRoomNode_008);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_010, BackRoomNode_009);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_011, BackRoomNode_010);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_012, BackRoomNode_011);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_013, BackRoomNode_012);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_014, BackRoomNode_013);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_015, BackRoomNode_014);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_012, BackRoomNode_011);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_013, BackRoomNode_012);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_014, BackRoomNode_013);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_015, BackRoomNode_014);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_016, BackRoomNode_015);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_017, BackRoomNode_016);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_018, BackRoomNode_027);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_019, BackRoomNode_028);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_020, BackRoomNode_029);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_021, BackRoomNode_030);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_022, BackRoomNode_031);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_032, BackRoomNode_003);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_033, BackRoomNode_032);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_034, BackRoomNode_033);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_035, BackRoomNode_034);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_036, BackRoomNode_035);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_037, BackRoomNode_036);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_038, BackRoomNode_037);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_039, BackRoomNode_038);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_040, BackRoomNode_039);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_041, BackRoomNode_040);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_042, BackRoomNode_041);
            
            m_CachedFlow.AddNodeToFlow(BackRoomNode_043, BackRoomNode_037);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_044, BackRoomNode_043);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_045, BackRoomNode_044);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_046, BackRoomNode_039);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_047, BackRoomNode_046);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_048, BackRoomNode_047);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_049, BackRoomNode_047);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_050, BackRoomNode_002);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_051, BackRoomNode_050);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_052, BackRoomNode_051);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_053, BackRoomNode_052);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_054, BackRoomNode_053);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_055, BackRoomNode_054);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_056, BackRoomNode_055);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_057, BackRoomNode_056);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_058, BackRoomNode_057);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_059, BackRoomNode_058);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_060, BackRoomNode_059);


            m_CachedFlow.AddNodeToFlow(BackRoomNode_061, BackRoomNode_054);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_062, BackRoomNode_056);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_063, BackRoomNode_056);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_064, BackRoomNode_057);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_065, BackRoomNode_001);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_066, BackRoomNode_065);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_067, BackRoomNode_066);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_068, BackRoomNode_067);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_069, BackRoomNode_068);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_070, BackRoomNode_069);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_071, BackRoomNode_070);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_072, BackRoomNode_071);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_073, BackRoomNode_072);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_074, BackRoomNode_073);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_075, BackRoomNode_074);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_076, BackRoomNode_069);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_077, BackRoomNode_076);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_078, BackRoomNode_003);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_079, BackRoomNode_078);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_080, BackRoomNode_079);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_081, BackRoomNode_080);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_081, BackRoomNode_004);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_082, BackRoomNode_081);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_083, BackRoomNode_082);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_084, BackRoomNode_083);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_085, BackRoomNode_084);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_086, BackRoomNode_082);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_087, BackRoomNode_086);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_088, BackRoomNode_087);
            m_CachedFlow.AddNodeToFlow(BackRoomNode_089, BackRoomNode_088);

            m_CachedFlow.AddNodeToFlow(BackRoomNode_090, BackRoomNode_087);

            m_CachedFlow.FirstNode = entranceNode;
            
            return m_CachedFlow;
        }
    }
}

