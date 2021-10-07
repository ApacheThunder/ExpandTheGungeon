using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class f2b_belly_flow_01 {
        
        public static DungeonFlow F2b_Belly_Flow_01() {
            
            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            if (!ExpandDungeonFlow.BellyInjectionData) {
                ExpandDungeonFlow.BellyInjectionData = ScriptableObject.CreateInstance<SharedInjectionData>();
                ExpandDungeonFlow.BellyInjectionData.name = "Belly Common Injection Data";
                ExpandDungeonFlow.BellyInjectionData.UseInvalidWeightAsNoInjection = true;
                ExpandDungeonFlow.BellyInjectionData.PreventInjectionOfFailedPrerequisites = false;
                ExpandDungeonFlow.BellyInjectionData.IsNPCCell = false;
                ExpandDungeonFlow.BellyInjectionData.IgnoreUnmetPrerequisiteEntries = false;
                ExpandDungeonFlow.BellyInjectionData.OnlyOne = false;
                ExpandDungeonFlow.BellyInjectionData.ChanceToSpawnOne = 0.5f;
                ExpandDungeonFlow.BellyInjectionData.AttachedInjectionData = new List<SharedInjectionData>(0);
                ExpandDungeonFlow.BellyInjectionData.InjectionData = new List<ProceduralFlowModifierData>(0);
            }
            
            DungeonFlowNode entranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandRoomPrefabs.Expand_Belly_Entrance);
            DungeonFlowNode exitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.Expand_Belly_ExitHub);
            DungeonFlowNode exitNode2 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandRoomPrefabs.Expand_Belly_RealExit);
            DungeonFlowNode bossfoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.boss_foyertable);
            DungeonFlowNode bossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.Expand_Belly_BossRoom);
            
            DungeonFlowNode BellyRewardNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.gungeon_rewardroom_1);
            DungeonFlowNode BellyRewardNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.gungeon_rewardroom_1);
            DungeonFlowNode BellyRewardNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.Expand_Belly_Reward);
            DungeonFlowNode BellyShrineNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL, ExpandRoomPrefabs.Expand_Belly_Shrine);

            DungeonFlowNode BellyRoomConnectorNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR);
            DungeonFlowNode BellyRoomConnectorNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);

            DungeonFlowNode BellyRoomNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_06 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_07 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_10 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_11 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_12 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_13 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_14 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_15 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_16 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode BellyRoomNode_17 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);


            m_CachedFlow.name = "F2b_Belly_Flow_01";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.BellyRoomTable;
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>() { ExpandDungeonFlow.BaseSubTypeRestrictions };
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { ExpandDungeonFlow.BaseSharedInjectionData/*, ExpandDungeonFlow.BellyInjectionData*/ };
            m_CachedFlow.Initialize();

            m_CachedFlow.AddNodeToFlow(entranceNode, null);
            
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_01, entranceNode);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_02, BellyRoomNode_01);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_03, BellyRoomNode_02);
            
            m_CachedFlow.AddNodeToFlow(BellyRoomConnectorNode_01, BellyRoomNode_03);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_04, BellyRoomConnectorNode_01);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_05, BellyRoomConnectorNode_01);
            m_CachedFlow.AddNodeToFlow(BellyRewardNode_01, BellyRoomNode_05);

            m_CachedFlow.AddNodeToFlow(BellyRoomNode_06, BellyRoomConnectorNode_01);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_07, BellyRoomNode_06);            
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_10, BellyRoomNode_07);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_11, BellyRoomNode_10);

            m_CachedFlow.AddNodeToFlow(BellyRoomConnectorNode_02, BellyRoomNode_11);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_12, BellyRoomConnectorNode_02);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_13, BellyRoomConnectorNode_02);
            m_CachedFlow.AddNodeToFlow(BellyRewardNode_02, BellyRoomNode_13);

            m_CachedFlow.AddNodeToFlow(BellyRoomNode_14, BellyRoomConnectorNode_02);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_15, BellyRoomConnectorNode_02);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_16, BellyRoomNode_15);
            m_CachedFlow.AddNodeToFlow(BellyRoomNode_17, BellyRoomNode_16);
            
            m_CachedFlow.AddNodeToFlow(bossfoyerNode, BellyRoomNode_17);
            m_CachedFlow.AddNodeToFlow(bossNode, bossfoyerNode);
            m_CachedFlow.AddNodeToFlow(exitNode, bossNode);

            m_CachedFlow.AddNodeToFlow(BellyRewardNode_03, exitNode);
            m_CachedFlow.AddNodeToFlow(BellyShrineNode_01, exitNode);
            m_CachedFlow.AddNodeToFlow(exitNode2, exitNode);

            m_CachedFlow.FirstNode = entranceNode;

            return m_CachedFlow;
        }
    }
}

