using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class f1b_jungle_flow_01 {
        
        public static DungeonFlow F1b_Jungle_Flow_01() {
            
            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            if (!ExpandDungeonFlow.JungleInjectionData) {
                ExpandDungeonFlow.JungleInjectionData = ScriptableObject.CreateInstance<SharedInjectionData>();
                ExpandDungeonFlow.JungleInjectionData.name = "Jungle Common Injection Data";
                ExpandDungeonFlow.JungleInjectionData.UseInvalidWeightAsNoInjection = true;
                ExpandDungeonFlow.JungleInjectionData.PreventInjectionOfFailedPrerequisites = false;
                ExpandDungeonFlow.JungleInjectionData.IsNPCCell = false;
                ExpandDungeonFlow.JungleInjectionData.IgnoreUnmetPrerequisiteEntries = false;
                ExpandDungeonFlow.JungleInjectionData.OnlyOne = false;
                ExpandDungeonFlow.JungleInjectionData.ChanceToSpawnOne = 0.5f;
                ExpandDungeonFlow.JungleInjectionData.AttachedInjectionData = new List<SharedInjectionData>(0);
                ExpandDungeonFlow.JungleInjectionData.InjectionData = new List<ProceduralFlowModifierData>() {
                    new ProceduralFlowModifierData() {
                        annotation = "Cathedral Crest Room",
                        DEBUG_FORCE_SPAWN = false,
                        OncePerRun = false,
                        placementRules = new List<ProceduralFlowModifierData.FlowModifierPlacementType>() {
                            ProceduralFlowModifierData.FlowModifierPlacementType.END_OF_CHAIN
                        },
                        roomTable = null,
                        exactRoom = ExpandRoomPrefabs.Expand_Jungle_OldCrest,
                        IsWarpWing = false,
                        RequiresMasteryToken = false,
                        chanceToLock = 0.5f,
                        selectionWeight = 1,
                        chanceToSpawn = 1,
                        RequiredValidPlaceable = null,
                        CanBeForcedSecret = true,
                        RandomNodeChildMinDistanceFromEntrance = 0,
                        exactSecondaryRoom = null,
                        framedCombatNodes = 0,
                        prerequisites = new DungeonPrerequisite[] {
                            new DungeonPrerequisite() {
                                prerequisiteType = DungeonPrerequisite.PrerequisiteType.DEMO_MODE,
                                prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.GREATER_THAN,
                                statToCheck = TrackedStats.TIMES_REACHED_GUNGEON,
                                maxToCheck = 0,
                                comparisonValue = 0,
                                encounteredObjectGuid = string.Empty,
                                encounteredRoom = null,
                                requiredNumberOfEncounters = 0,
                                requiredCharacter = PlayableCharacters.Pilot,
                                requireCharacter = false,
                                requiredTileset = 0,
                                requireTileset = false,
                                saveFlagToCheck = 0,
                                requireFlag = false,
                                requireDemoMode = false
                            }
                        }
                    },
                    new ProceduralFlowModifierData() {
                        annotation = "Lost Baby Dragun",
                        DEBUG_FORCE_SPAWN = false,
                        OncePerRun = false,
                        placementRules = new List<ProceduralFlowModifierData.FlowModifierPlacementType>() {
                            ProceduralFlowModifierData.FlowModifierPlacementType.RANDOM_NODE_CHILD
                        },
                        roomTable = null,
                        exactRoom = ExpandRoomPrefabs.Expand_Jungle_SecretDragun,
                        IsWarpWing = false,
                        RequiresMasteryToken = false,
                        chanceToLock = 1,
                        selectionWeight = 1,
                        chanceToSpawn = 1,
                        RequiredValidPlaceable = null,
                        CanBeForcedSecret = true,
                        RandomNodeChildMinDistanceFromEntrance = 0,
                        exactSecondaryRoom = null,
                        framedCombatNodes = 0,
                        prerequisites = new DungeonPrerequisite[] {
                            new DungeonPrerequisite() {
                                prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                                prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.EQUAL_TO,
                                statToCheck = TrackedStats.TIMES_REACHED_GUNGEON,
                                maxToCheck = 0,
                                comparisonValue = 0,
                                encounteredObjectGuid = string.Empty,
                                encounteredRoom = null,
                                requiredNumberOfEncounters = 0,
                                requiredCharacter = PlayableCharacters.Pilot,
                                requireCharacter = false,
                                requiredTileset = GlobalDungeonData.ValidTilesets.JUNGLEGEON,
                                requireTileset = true,
                                saveFlagToCheck = 0,
                                requireFlag = false,
                                requireDemoMode = false
                            }
                        }
                    }
                };
            }
            
            DungeonFlowNode entranceNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandRoomPrefabs.Expand_Jungle_Entrance);
            DungeonFlowNode exitNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.Expand_Jungle_Exit);
            DungeonFlowNode bossfoyerNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.boss_foyertable);
            DungeonFlowNode bossNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, ExpandRoomPrefabs.Expand_Jungle_Boss);
            
            DungeonFlowNode JungleShopNode = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, ExpandPrefabs.shop02.category, overrideTable: ExpandPrefabs.shop_room_table);
            DungeonFlowNode JungleRewardNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room);
            DungeonFlowNode JungleRewardNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room);


            DungeonFlowNode JungleRoomNode_01 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);
            DungeonFlowNode JungleRoomNode_02 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_03 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_04 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_05 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_08 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_09 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_10 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_13 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_14 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_15 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_16 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_17 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_18 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_19 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_20 = ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);



            m_CachedFlow.name = "F1b_Jungle_Flow_01";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.JungleRoomTable;
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { ExpandDungeonFlow.BaseSharedInjectionData, ExpandDungeonFlow.JungleInjectionData };
            m_CachedFlow.Initialize();

            m_CachedFlow.AddNodeToFlow(entranceNode, null);           
            
            // Hub 
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_01, entranceNode);

            // Branch 1
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_02, JungleRoomNode_01);
            // Branch 1 SubBranch 1 (Dead End)
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_03, JungleRoomNode_02);
            
            // Branch 1 SubBranch 2 (Leads to Reward)
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_04, JungleRoomNode_02);
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_05, JungleRoomNode_04);
            m_CachedFlow.AddNodeToFlow(JungleRewardNode_01, JungleRoomNode_04);

            // Branch 2
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_08, JungleRoomNode_01);
            // Sub Branch 1 (Leads to Reward)
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_09, JungleRoomNode_08);
            m_CachedFlow.AddNodeToFlow(JungleRewardNode_02, JungleRoomNode_09);
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_13, JungleRoomNode_09);
            // Sub Branch 2 (Dead End)
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_10, JungleRoomNode_08);
                        
            // Branch 3 (Path to Boss and Shop)
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_14, JungleRoomNode_01);
            // Sub Branch 1 (Leads to Boss)
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_15, JungleRoomNode_14);
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_19, JungleRoomNode_15);
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_18, JungleRoomNode_15);
            // Sub Branch 2 (Leads to Shop)
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_16, JungleRoomNode_14);

            m_CachedFlow.AddNodeToFlow(JungleRoomNode_17, JungleRoomNode_16);
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_20, JungleRoomNode_16);
            m_CachedFlow.AddNodeToFlow(JungleShopNode, JungleRoomNode_17);


            m_CachedFlow.AddNodeToFlow(bossfoyerNode, JungleRoomNode_18);
            m_CachedFlow.AddNodeToFlow(bossNode, bossfoyerNode);
            m_CachedFlow.AddNodeToFlow(exitNode, bossNode);

            m_CachedFlow.FirstNode = entranceNode;

            return m_CachedFlow;
        }
    }
}

