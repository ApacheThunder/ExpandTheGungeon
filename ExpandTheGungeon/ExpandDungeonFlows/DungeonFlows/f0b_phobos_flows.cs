using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class f0b_phobos_flows {

        public static DungeonFlow F0b_Phobos_Flow_01(DungeonFlow m_CachedFlow) {
            
            if (!ExpandDungeonFlow.PhobosInjectionData) {
                ExpandDungeonFlow.PhobosInjectionData = ScriptableObject.CreateInstance<SharedInjectionData>();
                ExpandDungeonFlow.PhobosInjectionData.name = "Phobos Common Injection Data";
                ExpandDungeonFlow.PhobosInjectionData.UseInvalidWeightAsNoInjection = true;
                ExpandDungeonFlow.PhobosInjectionData.PreventInjectionOfFailedPrerequisites = false;
                ExpandDungeonFlow.PhobosInjectionData.IsNPCCell = false;
                ExpandDungeonFlow.PhobosInjectionData.IgnoreUnmetPrerequisiteEntries = false;
                ExpandDungeonFlow.PhobosInjectionData.OnlyOne = false;
                ExpandDungeonFlow.PhobosInjectionData.ChanceToSpawnOne = 0.5f;
                ExpandDungeonFlow.PhobosInjectionData.AttachedInjectionData = new List<SharedInjectionData>(0);
                ExpandDungeonFlow.PhobosInjectionData.InjectionData = new List<ProceduralFlowModifierData>(0);
            }

            
            m_CachedFlow.name = "F0b_Phobos_Flow_01";
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { ExpandDungeonFlow.BaseSharedInjectionData, ExpandDungeonFlow.PhobosInjectionData };

            m_CachedFlow.FirstNode.overrideExactRoom = ExpandPrefabs.big_entrance;

            m_CachedFlow.AllNodes[2].overrideExactRoom = ExpandPrefabs.blobulordroom01;

            return m_CachedFlow;
        }

        public static DungeonFlow F0b_Phobos_Flow_02(DungeonFlow m_CachedFlow) {
            
            m_CachedFlow.name = "F0b_Phobos_Flow_02";
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { ExpandDungeonFlow.BaseSharedInjectionData, ExpandDungeonFlow.PhobosInjectionData };

            m_CachedFlow.FirstNode.overrideExactRoom = ExpandPrefabs.big_entrance;

            m_CachedFlow.AllNodes[2].overrideExactRoom = ExpandPrefabs.blobulordroom01;

            return m_CachedFlow;
        }
    }
}

