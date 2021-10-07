using Dungeonator;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class fruit_loops {        

        public static int LoopRoomCount = 8;
        public static int SingleChainRoomCount = 10;

        public static DungeonFlow Fruit_Loops() {
            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            List<DungeonFlowNode> m_cachedNodes_01 = new List<DungeonFlowNode>();
            List<DungeonFlowNode> m_cachedNodes_02 = new List<DungeonFlowNode>();
            List<DungeonFlowNode> m_cachedNodes_03 = new List<DungeonFlowNode>();
            List<DungeonFlowNode> m_cachedNodes_04 = new List<DungeonFlowNode>();

            DungeonFlowNode m_EntranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandRoomPrefabs.Giant_Elevator_Room);
            DungeonFlowNode m_HubNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);
            DungeonFlowNode m_ShopNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.shop_room_table);
            DungeonFlowNode m_ShopNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.shop_room_table);
            DungeonFlowNode m_ChestRoom_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room, oneWayLoopTarget: true);
            DungeonFlowNode m_ChestRoom_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandPrefabs.gungeon_rewardroom_1);
            DungeonFlowNode m_ChestRoom_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandPrefabs.gungeon_rewardroom_1);
            DungeonFlowNode m_ChestRoom_04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room, oneWayLoopTarget: true);
            DungeonFlowNode m_ExitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandPrefabs.tiny_exit);
            DungeonFlowNode m_BossFoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.boss_foyer);
            DungeonFlowNode m_BossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, overrideTable: ExpandPrefabs.MegaBossRoomTable);
            DungeonFlowNode m_FakeBossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandPrefabs.tutorial_minibossroom);
            DungeonFlowNode m_FakeBossFoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.boss_foyer);

            for (int i = 0; i < LoopRoomCount + 1; i++) {
                m_cachedNodes_01.Add(ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL));
                m_cachedNodes_04.Add(ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL));
            }

            for (int i = 0; i < SingleChainRoomCount + 1; i++) {
                m_cachedNodes_02.Add(ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL));
                m_cachedNodes_03.Add(ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL));
            }

            m_cachedNodes_01[0].roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            m_cachedNodes_01[LoopRoomCount].roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            m_cachedNodes_02[0].roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            m_cachedNodes_02[SingleChainRoomCount].roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            m_cachedNodes_03[0].roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            m_cachedNodes_03[SingleChainRoomCount].roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            m_cachedNodes_04[0].roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            m_cachedNodes_04[LoopRoomCount].roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR;

            bool bossShuffle = BraveUtility.RandomBool();

            m_CachedFlow.name = "Fruit_Loops";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.CustomRoomTable2;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>() { ExpandDungeonFlow.BaseSubTypeRestrictions };
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { ExpandDungeonFlow.BaseSharedInjectionData };
            m_CachedFlow.Initialize();

            // Hub area
            m_CachedFlow.AddNodeToFlow(m_HubNode, null);

            // Big Entrance with first loop.
            m_CachedFlow.AddNodeToFlow(m_EntranceNode, m_HubNode);

            m_CachedFlow.AddNodeToFlow(m_cachedNodes_01[0], m_HubNode);
            for (int i = 1; i < LoopRoomCount + 1; i++) { m_CachedFlow.AddNodeToFlow(m_cachedNodes_01[i], m_cachedNodes_01[i - 1]); }

            m_CachedFlow.AddNodeToFlow(m_ShopNode_01, m_cachedNodes_01[LoopRoomCount]);
            m_CachedFlow.AddNodeToFlow(m_ChestRoom_01, m_ShopNode_01);
            m_CachedFlow.LoopConnectNodes(m_HubNode, m_ChestRoom_01);

            // Maybe boss path. :P
            m_CachedFlow.AddNodeToFlow(m_cachedNodes_02[0], m_HubNode);
            for (int i = 1; i < SingleChainRoomCount + 1; i++) { m_CachedFlow.AddNodeToFlow(m_cachedNodes_02[i], m_cachedNodes_02[i - 1]); }
            m_CachedFlow.AddNodeToFlow(m_ChestRoom_02, m_cachedNodes_02[SingleChainRoomCount]);
            if (bossShuffle) {
                m_CachedFlow.AddNodeToFlow(m_BossFoyerNode, m_ChestRoom_02);
                m_CachedFlow.AddNodeToFlow(m_BossNode, m_BossFoyerNode);
                m_CachedFlow.AddNodeToFlow(m_ExitNode, m_BossNode);
            } else {
                m_CachedFlow.AddNodeToFlow(m_FakeBossFoyerNode, m_ChestRoom_02);
                m_CachedFlow.AddNodeToFlow(m_FakeBossNode, m_FakeBossFoyerNode);
            }

            // Maybe boss path. :P
            m_CachedFlow.AddNodeToFlow(m_cachedNodes_03[0], m_EntranceNode);
            for (int i = 1; i < SingleChainRoomCount + 1; i++) { m_CachedFlow.AddNodeToFlow(m_cachedNodes_03[i], m_cachedNodes_03[i - 1]); }
            m_CachedFlow.AddNodeToFlow(m_ChestRoom_03, m_cachedNodes_03[SingleChainRoomCount]);
            if (bossShuffle) {
                m_CachedFlow.AddNodeToFlow(m_FakeBossFoyerNode, m_ChestRoom_03);
                m_CachedFlow.AddNodeToFlow(m_FakeBossNode, m_FakeBossFoyerNode);
            } else {
                m_CachedFlow.AddNodeToFlow(m_BossFoyerNode, m_ChestRoom_03);
                m_CachedFlow.AddNodeToFlow(m_BossNode, m_BossFoyerNode);
                m_CachedFlow.AddNodeToFlow(m_ExitNode, m_BossNode);
            }

            // Second Loop       
            m_CachedFlow.AddNodeToFlow(m_cachedNodes_04[0], m_EntranceNode);
            for (int i = 1; i < LoopRoomCount + 1; i++) { m_CachedFlow.AddNodeToFlow(m_cachedNodes_04[i], m_cachedNodes_04[i - 1]); }
            m_CachedFlow.AddNodeToFlow(m_ShopNode_02, m_cachedNodes_04[LoopRoomCount]);
            m_CachedFlow.AddNodeToFlow(m_ChestRoom_04, m_ShopNode_02);
            m_CachedFlow.LoopConnectNodes(m_ChestRoom_04, m_EntranceNode);
            m_CachedFlow.FirstNode = m_HubNode;

            return m_CachedFlow;
        }
    }
}

