using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class f0b_office_flows {

        public static DungeonFlow F0b_Office_Flow_01(DungeonFlow m_CachedFlow) {
            
            if (!ExpandDungeonFlow.OfficeInjectionData) {
                ExpandDungeonFlow.OfficeInjectionData = ScriptableObject.CreateInstance<SharedInjectionData>();
                ExpandDungeonFlow.OfficeInjectionData.name = "Office Common Injection Data";
                ExpandDungeonFlow.OfficeInjectionData.UseInvalidWeightAsNoInjection = true;
                ExpandDungeonFlow.OfficeInjectionData.PreventInjectionOfFailedPrerequisites = false;
                ExpandDungeonFlow.OfficeInjectionData.IsNPCCell = false;
                ExpandDungeonFlow.OfficeInjectionData.IgnoreUnmetPrerequisiteEntries = false;
                ExpandDungeonFlow.OfficeInjectionData.OnlyOne = false;
                ExpandDungeonFlow.OfficeInjectionData.ChanceToSpawnOne = 0.5f;
                ExpandDungeonFlow.OfficeInjectionData.AttachedInjectionData = new List<SharedInjectionData>(0);
                ExpandDungeonFlow.OfficeInjectionData.InjectionData = new List<ProceduralFlowModifierData>(0);
            }

            
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

