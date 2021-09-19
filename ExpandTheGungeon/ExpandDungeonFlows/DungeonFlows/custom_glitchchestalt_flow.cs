﻿using Dungeonator;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class custom_glitchchestalt_flow : ExpandDungeonFlow {
        
        public static int LoopRoomCount = 4;
        public static int SingleChainRoomCount = 3;

        public static DungeonFlow Custom_GlitchChestAlt_Flow() {
            
            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            List<DungeonFlowNode> m_cachedNodes_01 = new List<DungeonFlowNode>();
            List<DungeonFlowNode> m_cachedNodes_02 = new List<DungeonFlowNode>();
            List<DungeonFlowNode> m_cachedNodes_03 = new List<DungeonFlowNode>();
            List<DungeonFlowNode> m_cachedNodes_04 = new List<DungeonFlowNode>();

            DungeonFlowNode m_EntranceNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandPrefabs.big_entrance);
            DungeonFlowNode m_HubNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);
            DungeonFlowNode m_ShopNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.shop_room_table);
            DungeonFlowNode m_ChestRoom_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room, oneWayLoopTarget: true);
            DungeonFlowNode m_ChestRoom_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandPrefabs.gungeon_rewardroom_1);
            DungeonFlowNode m_ChestRoom_03 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandPrefabs.gungeon_rewardroom_1);
            DungeonFlowNode m_ChestRoom_04 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room, oneWayLoopTarget: true);
            DungeonFlowNode m_ExitNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandPrefabs.tiny_exit);
            DungeonFlowNode m_BossFoyerNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.boss_foyer);
            DungeonFlowNode m_BossNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandPrefabs.doublebeholsterroom01);
            DungeonFlowNode m_FakeBossNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandPrefabs.tutorial_minibossroom);
            DungeonFlowNode m_FakeBossFoyerNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.boss_foyer);
            

            for (int i = 0; i < LoopRoomCount + 1; i++) {
                m_cachedNodes_01.Add(GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL));
                m_cachedNodes_04.Add(GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL));
            }

            for (int i = 0; i < SingleChainRoomCount + 1; i++) {
                m_cachedNodes_02.Add(GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL));
                m_cachedNodes_03.Add(GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL));
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

            m_CachedFlow.name = "Custom_GlitchChestAlt_Flow";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.CustomRoomTable2;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>() { BaseSubTypeRestrictions };
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { BaseSharedInjectionData };
            

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
            m_CachedFlow.AddNodeToFlow(m_ChestRoom_04, m_cachedNodes_04[LoopRoomCount]);
            m_CachedFlow.LoopConnectNodes(m_ChestRoom_04, m_EntranceNode);
            m_CachedFlow.FirstNode = m_HubNode;

            return m_CachedFlow;
        }
    }
}

