using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandDungeonFlows {
        
    public class f0b_phobos_flows {

        public static DungeonFlow F0b_Phobos_Flow_01 {
            get {
                if (!m_f0b_phobos_flow_01) m_f0b_phobos_flow_01 = m_F0b_Phobos_Flow_01();
                return m_f0b_phobos_flow_01;
            }
        }

        public static DungeonFlow F0b_Phobos_Flow_02 {
            get {
                if (!m_f0b_phobos_flow_02) m_f0b_phobos_flow_02 = m_F0b_Phobos_Flow_02();
                return m_f0b_phobos_flow_02;
            }
        }

        private static DungeonFlow m_f0b_phobos_flow_01;
        private static DungeonFlow m_f0b_phobos_flow_02;

        private static DungeonFlow m_F0b_Phobos_Flow_01() {
            Dungeon SewerPrefab = DungeonDatabase.GetOrLoadByName("Base_Sewer");

            DungeonFlow m_CachedFlow = FlowHelpers.DuplicateDungeonFlow(SewerPrefab.PatternSettings.flows[0]);

            SewerPrefab = null;
            
            m_CachedFlow.name = "F0b_Phobos_Flow_01";
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { ExpandDungeonFlow.BaseSharedInjectionData, ExpandDungeonFlow.PhobosInjectionData };

            m_CachedFlow.FirstNode.overrideExactRoom = ExpandPrefabs.big_entrance;

            m_CachedFlow.AllNodes[2].overrideExactRoom = ExpandRoomPrefabs.Expand_Future_BossRoom;

            return m_CachedFlow;
        }

        public static DungeonFlow m_F0b_Phobos_Flow_02() {
            Dungeon SewerPrefab = DungeonDatabase.GetOrLoadByName("Base_Sewer");

            DungeonFlow m_CachedFlow = FlowHelpers.DuplicateDungeonFlow(SewerPrefab.PatternSettings.flows[1]);

            SewerPrefab = null;

            m_CachedFlow.name = "F0b_Phobos_Flow_02";
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { ExpandDungeonFlow.BaseSharedInjectionData, ExpandDungeonFlow.PhobosInjectionData };

            m_CachedFlow.FirstNode.overrideExactRoom = ExpandPrefabs.big_entrance;

            m_CachedFlow.AllNodes[2].overrideExactRoom = ExpandRoomPrefabs.Expand_Future_BossRoom;

            return m_CachedFlow;
        }
    }
}

