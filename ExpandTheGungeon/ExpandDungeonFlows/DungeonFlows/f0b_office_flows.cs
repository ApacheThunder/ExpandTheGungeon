using Dungeonator;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class f0b_office_flows {

        public static DungeonFlow F0b_Office_Flow_01 {
            get {
                if (!m_f0b_office_flow_01) m_f0b_office_flow_01 = m_F0b_Office_Flow_01();
                return m_f0b_office_flow_01;
            }
        }

        private static DungeonFlow m_f0b_office_flow_01;

        private static DungeonFlow m_F0b_Office_Flow_01() {
            Dungeon CathedralPrefab = DungeonDatabase.GetOrLoadByName("Base_Cathedral");

            DungeonFlow m_CachedFlow = FlowHelpers.DuplicateDungeonFlow(CathedralPrefab.PatternSettings.flows[0]);

            CathedralPrefab = null;

            m_CachedFlow.name = "F0b_Office_Flow_01";
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.AbbeyRoomTableForOffice;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { ExpandDungeonFlow.BaseSharedInjectionData, ExpandDungeonFlow.PhobosInjectionData };

            m_CachedFlow.FirstNode.overrideExactRoom = ExpandPrefabs.elevator_entrance;
            m_CachedFlow.AllNodes[2].overrideExactRoom = ExpandPrefabs.oldbulletking_room_01;

            return m_CachedFlow;
        }
    }
}

