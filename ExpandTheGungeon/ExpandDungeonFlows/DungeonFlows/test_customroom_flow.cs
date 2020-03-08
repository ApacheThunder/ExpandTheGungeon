using ExpandTheGungeon.ExpandObjects;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class test_customroom_flow : ExpandDungeonFlow {
        
        public static DungeonFlow Test_CustomRoom_Flow() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            DungeonFlowNode entranceNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandPrefabs.tiny_entrance);
            DungeonFlowNode exitNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.SecretExitRoom);
            DungeonFlowNode bossfoyerNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.boss_foyertable);
            DungeonFlowNode bossNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.GungeoneerMimicBossRoom);

            // DungeonFlowNode firstConnectorNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom);
            // DungeonFlowNode lastConnectorNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom);

            DungeonFlowNode testConnectorNode01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.gungeon_rewardroom_1);
            // DungeonFlowNode testConnectorNode02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room);
            DungeonFlowNode testConnectorNode03 = GenerateDefaultNode(m_CachedFlow, ExpandPrefabs.shop02.category, overrideTable: ExpandPrefabs.shop_room_table);
            // DungeonFlowNode testConnectorNode04 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom);
            //DungeonFlowNode testConnectorNode05 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom);
            /*DungeonFlowNode testConnectorNode06 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom);
            DungeonFlowNode testConnectorNode07 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Utiliroom);*/

            DungeonFlowNode TestRoomNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.Expand_Apache_SpikeAndPits);
            DungeonFlowNode TestSecretRoomNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, ExpandRoomPrefabs.Expand_GlitchedSecret);

            m_CachedFlow.name = "Test_CustomRoom_Flow";
            m_CachedFlow.fallbackRoomTable = null;
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>(0);
            m_CachedFlow.Initialize();

            m_CachedFlow.AddNodeToFlow(entranceNode, null);
            // m_CachedFlow.AddNodeToFlow(firstConnectorNode, entranceNode);
            // m_CachedFlow.AddNodeToFlow(TestRoomNode_01, firstConnectorNode);
            m_CachedFlow.AddNodeToFlow(TestRoomNode_01, entranceNode);
            
            m_CachedFlow.AddNodeToFlow(testConnectorNode01, TestRoomNode_01);
            m_CachedFlow.AddNodeToFlow(TestSecretRoomNode_01, testConnectorNode01);

            // m_CachedFlow.AddNodeToFlow(testConnectorNode02, TestRoomNode_01);
            m_CachedFlow.AddNodeToFlow(testConnectorNode03, TestRoomNode_01);
            // m_CachedFlow.AddNodeToFlow(testConnectorNode04, TestRoomNode_01);
            //m_CachedFlow.AddNodeToFlow(testConnectorNode05, TestRoomNode_01);
            /*m_CachedFlow.AddNodeToFlow(testConnectorNode06, TestRoomNode_01);
            m_CachedFlow.AddNodeToFlow(testConnectorNode07, TestRoomNode_01);*/

            // m_CachedFlow.AddNodeToFlow(lastConnectorNode, TestRoomNode_01);
            // m_CachedFlow.AddNodeToFlow(exitNode, lastConnectorNode);
            m_CachedFlow.AddNodeToFlow(bossfoyerNode, TestRoomNode_01);
            m_CachedFlow.AddNodeToFlow(bossNode, bossfoyerNode);
            m_CachedFlow.AddNodeToFlow(exitNode, bossNode);
            m_CachedFlow.FirstNode = entranceNode;

            return m_CachedFlow;
        }
    }
}

