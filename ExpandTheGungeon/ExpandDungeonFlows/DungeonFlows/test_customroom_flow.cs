using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class test_customroom_flow : ExpandDungeonFlow {
        
        public static DungeonFlow Test_CustomRoom_Flow() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            DungeonFlowNode entranceNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandPrefabs.elevator_entrance);
            DungeonFlowNode exitNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.SecretExitRoom);
            DungeonFlowNode bossfoyerNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.boss_foyertable);
            DungeonFlowNode bossNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.Expand_West_WestBrosBossRoom);

            DungeonFlowNode TestRoomNode_0 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.Expand_Apache_RickRollChest);

            DungeonFlowNode TestRoomNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.Expand_Apache_SurpriseChest);

            DungeonFlowNode TestShopNode = GenerateDefaultNode(m_CachedFlow, ExpandPrefabs.shop02.category, overrideTable: ExpandPrefabs.shop_room_table);
            DungeonFlowNode TestRewardNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.gungeon_rewardroom_1);
            
            DungeonFlowNode TestSecretRoomNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, ExpandRoomPrefabs.Expand_GlitchedSecret);
            DungeonFlowNode testConnectorNode01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.Expand_BootlegRoom);

            DungeonFlowNode TestRoomNode_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.Expand_Apache_RainbowRoom);
            DungeonFlowNode TestRoomNode_03 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.SecretRewardRoom);
            // DungeonFlowNode SecondSecretRoom = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SECRET, ExpandRoomPrefabs.Secret_Expand_logo);

            /*foreach (PrototypeDungeonRoom room in ExpandRoomPrefabs.Expand_Jungle_Rooms) {
                if (room.name == "Expand_Forest_Mixed22") {
                    TestRoomNode_02.overrideExactRoom = room;
                    break;
                }
            }*/

            m_CachedFlow.name = "Test_CustomRoom_Flow";
            m_CachedFlow.fallbackRoomTable = null;
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>(0);
            m_CachedFlow.sharedInjectionData.Add(HollowsInjectionData);

            m_CachedFlow.Initialize();

            m_CachedFlow.AddNodeToFlow(entranceNode, null);            
            m_CachedFlow.AddNodeToFlow(TestRoomNode_01, entranceNode);
            m_CachedFlow.AddNodeToFlow(TestRoomNode_0, entranceNode);
            
            m_CachedFlow.AddNodeToFlow(TestRewardNode, TestRoomNode_01);
            m_CachedFlow.AddNodeToFlow(TestSecretRoomNode, TestRewardNode);

            m_CachedFlow.AddNodeToFlow(TestShopNode, TestRoomNode_01);
            m_CachedFlow.AddNodeToFlow(bossfoyerNode, TestShopNode);
            m_CachedFlow.AddNodeToFlow(bossNode, bossfoyerNode);
            m_CachedFlow.AddNodeToFlow(exitNode, bossNode);
            // m_CachedFlow.AddNodeToFlow(SecondSecretRoom, TestShopNode);
            
            m_CachedFlow.AddNodeToFlow(testConnectorNode01, TestRoomNode_01);
            m_CachedFlow.AddNodeToFlow(TestRoomNode_02, testConnectorNode01);
            m_CachedFlow.AddNodeToFlow(TestRoomNode_03, TestRoomNode_02);
            

            m_CachedFlow.FirstNode = entranceNode;

            return m_CachedFlow;
        }
    }
}

