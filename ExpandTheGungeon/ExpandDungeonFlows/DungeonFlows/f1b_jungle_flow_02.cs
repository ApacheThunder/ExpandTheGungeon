﻿using ExpandTheGungeon.ExpandObjects;
using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class f1b_jungle_flow_02 : ExpandDungeonFlow {

        public static DungeonFlow F1b_Jungle_Flow_02() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            if (!JungleInjectionData) {
                JungleInjectionData = ScriptableObject.CreateInstance<SharedInjectionData>();
                JungleInjectionData.name = "Jungle Common Injection Data";
                JungleInjectionData.UseInvalidWeightAsNoInjection = true;
                JungleInjectionData.PreventInjectionOfFailedPrerequisites = false;
                JungleInjectionData.IsNPCCell = false;
                JungleInjectionData.IgnoreUnmetPrerequisiteEntries = false;
                JungleInjectionData.OnlyOne = false;
                JungleInjectionData.ChanceToSpawnOne = 0.5f;
                JungleInjectionData.AttachedInjectionData = new List<SharedInjectionData>(0);
                JungleInjectionData.InjectionData = new List<ProceduralFlowModifierData>() {
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
                            },
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


            DungeonFlowNode entranceNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, ExpandRoomPrefabs.Expand_Jungle_Entrance);
            DungeonFlowNode exitNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.EXIT, ExpandRoomPrefabs.Expand_Jungle_Exit);
            DungeonFlowNode bossfoyerNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.SPECIAL, overrideTable: ExpandPrefabs.boss_foyertable);
            DungeonFlowNode bossNode = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.BOSS, overrideTable: ExpandPrefabs.bosstable_01_gatlinggull);

            DungeonFlowNode JungleShopNode = GenerateDefaultNode(m_CachedFlow, ExpandPrefabs.shop02.category, overrideTable: ExpandPrefabs.shop_room_table);
            DungeonFlowNode JungleRewardNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.gungeon_rewardroom_1);
            DungeonFlowNode JungleRewardNode_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.gungeon_rewardroom_1);


            DungeonFlowNode JungleRoomNode_01 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB, oneWayLoopTarget: true);
            DungeonFlowNode JungleRoomNode_02 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_04 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_05 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_06 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB, oneWayLoopTarget: true);
            DungeonFlowNode JungleRoomNode_07 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_09 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_10 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_11 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.HUB);
            DungeonFlowNode JungleRoomNode_12 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_13 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_14 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_16 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_17 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);
            DungeonFlowNode JungleRoomNode_18 = GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL);

            m_CachedFlow.name = "F1b_Jungle_Flow_02";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.JungleRoomTable;
            m_CachedFlow.phantomRoomTable = null;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>() { BaseSharedInjectionData, JungleInjectionData };
            m_CachedFlow.Initialize();

            m_CachedFlow.AddNodeToFlow(entranceNode, null);
            
            // First Looping branch
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_16, entranceNode);
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_01, JungleRoomNode_16);
            // Dead End
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_05, JungleRoomNode_01);
            // Start of Loop
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_02, JungleRoomNode_01);
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_04, JungleRoomNode_02);
            m_CachedFlow.AddNodeToFlow(JungleRewardNode_01, JungleRoomNode_04);
            // Connect End of Loop to first in chain
            m_CachedFlow.LoopConnectNodes(JungleRewardNode_01, JungleRoomNode_01);

            // Second Looping branch
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_17, entranceNode);
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_06, JungleRoomNode_17);
            // Dead End
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_10, JungleRoomNode_06);
            // Start of Loop
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_07, JungleRoomNode_06);
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_09, JungleRoomNode_07);
            m_CachedFlow.AddNodeToFlow(JungleRewardNode_02, JungleRoomNode_09);
            // Connect End of Loop to first in chain
            m_CachedFlow.LoopConnectNodes(JungleRewardNode_02, JungleRoomNode_06);

            // Splitting path to Shop or Boss
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_18, entranceNode);
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_11, JungleRoomNode_18);
            // Path To Boss
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_12, JungleRoomNode_11);
            // Path to Shop
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_13, JungleRoomNode_11);
            m_CachedFlow.AddNodeToFlow(JungleShopNode, JungleRoomNode_13);
            // Dead End
            m_CachedFlow.AddNodeToFlow(JungleRoomNode_14, JungleRoomNode_11);


            m_CachedFlow.AddNodeToFlow(bossfoyerNode, JungleRoomNode_12);
            m_CachedFlow.AddNodeToFlow(bossNode, bossfoyerNode);
            m_CachedFlow.AddNodeToFlow(exitNode, bossNode);

            m_CachedFlow.FirstNode = entranceNode;

            return m_CachedFlow;
        }
    }
}

