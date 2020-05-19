using ExpandTheGungeon.ExpandUtilities;
using Dungeonator;
using System;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandObjects;

namespace ExpandTheGungeon.ExpandDungeonFlows {
    
    public class ExpandDungeonFlow : FlowDatabase {

        public static bool isGlitchFlow = false;

        public static List<string> GlitchChestFlows = new List<string>() {
            "custom_glitch_flow",
            "custom_glitchchest_flow",
            "custom_glitchchestalt_flow",
            "apache_fucking_around_flow"
        };

        public static DungeonFlow LoadCustomFlow(Func<string, DungeonFlow>orig, string target) {
            string flowName = target;
            if (flowName.Contains("/")) { flowName = target.Substring(target.LastIndexOf("/") + 1); }
            if (flowName.ToLower().EndsWith("secret_doublebeholster_flow")) {
                GlitchChestFlows = GlitchChestFlows.Shuffle();
                if (ExpandStats.randomSeed <= 0.5f) {
                    flowName = BraveUtility.RandomElement(GlitchChestFlows);
                } else {
                    flowName = GlitchChestFlows[UnityEngine.Random.Range(0, GlitchChestFlows.Count)];
                }
            } else if (flowName.ToLower().EndsWith("secret_doublebeholster_flow_orig")) {
                flowName = "secret_doublebeholster_flow";
            }
            if (KnownFlows != null && KnownFlows.Count > 0) {
                foreach (DungeonFlow flow in KnownFlows) {
                    if (flow.name != null && flow.name != string.Empty) {                                                
                        if (flowName.ToLower() == flow.name.ToLower()) {
                            // Allows glitch chest floors to have things like the Old Crest room drop off if on Gungeon tileset, etc.
                            if (GlitchChestFlows.Contains(flow.name.ToLower())) {
                                flow.sharedInjectionData = RetrieveSharedInjectionDataListFromCurrentFloor();
                            }
                            DebugTime.RecordStartTime();
                            DebugTime.Log("AssetBundle.LoadAsset<DungeonFlow>({0})", new object[] { flowName });
                            return flow;
                        }
                    }
                }
            }
            return orig(target);
        }
        
        public static DungeonFlow LoadOfficialFlow(string target) {
            string flowName = target;
            if (flowName.Contains("/")) { flowName = target.Substring(target.LastIndexOf("/") + 1); }
            AssetBundle m_assetBundle_orig = ResourceManager.LoadAssetBundle("flows_base_001");
            DebugTime.RecordStartTime();
            DungeonFlow result = m_assetBundle_orig.LoadAsset<DungeonFlow>(flowName);
            DebugTime.Log("AssetBundle.LoadAsset<DungeonFlow>({0})", new object[] { flowName });
            if (result == null) {
                Debug.Log("ERROR: Requested DungeonFlow not found!\nCheck that you provided correct DungeonFlow name and that it actually exists!");
                m_assetBundle_orig = null;
                return null;
            } else {
                m_assetBundle_orig = null;
                return result;
            }
        }
        
        public static List<DungeonFlow> KnownFlows;
        
        public static DungeonFlow Foyer_Flow;

        // Default stuff to use with custom Flows
        public static SharedInjectionData BaseSharedInjectionData;
        public static SharedInjectionData GungeonInjectionData;
        public static SharedInjectionData SewersInjectionData;
        public static SharedInjectionData HollowsInjectionData;
        public static SharedInjectionData CastleInjectionData;
        public static SharedInjectionData JungleInjectionData;
        public static SharedInjectionData BellyInjectionData;

        public static ProceduralFlowModifierData JunkSecretRoomInjector;
        public static ProceduralFlowModifierData SecretFloorEntranceInjector;
        public static ProceduralFlowModifierData SecretMiniElevatorInjector;
        public static ProceduralFlowModifierData SecretJungleEntranceInjector;
        public static ProceduralFlowModifierData BellySpecialEntranceRoomInjector;
        public static ProceduralFlowModifierData BellySpecialMonsterRoomInjector;

        public static DungeonFlowSubtypeRestriction BaseSubTypeRestrictions = new DungeonFlowSubtypeRestriction() {
            baseCategoryRestriction = PrototypeDungeonRoom.RoomCategory.NORMAL,
            normalSubcategoryRestriction = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP,
            bossSubcategoryRestriction = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS,
            specialSubcategoryRestriction = PrototypeDungeonRoom.RoomSpecialSubCategory.UNSPECIFIED_SPECIAL,
            secretSubcategoryRestriction = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET,
            maximumRoomsOfSubtype = 1
        };

        // Custom Room Table for Keep Shared Injection Data (for Jungle Entrance room injection data)
        public static GenericRoomTable m_KeepJungleEntranceRooms;

        // Generate a DungeonFlowNode with a default configuration
        public static DungeonFlowNode GenerateDefaultNode(DungeonFlow targetflow, PrototypeDungeonRoom.RoomCategory roomType, PrototypeDungeonRoom overrideRoom = null, GenericRoomTable overrideTable = null, bool oneWayLoopTarget = false, bool isWarpWingNode = false, string nodeGUID = null, DungeonFlowNode.NodePriority priority = DungeonFlowNode.NodePriority.MANDATORY, float percentChance = 1) {

            if (string.IsNullOrEmpty(nodeGUID)) { nodeGUID = Guid.NewGuid().ToString(); }

            DungeonFlowNode m_CachedNode = new DungeonFlowNode(targetflow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = roomType,
                percentChance = percentChance,
                priority = priority,
                overrideExactRoom = overrideRoom,
                overrideRoomTable = overrideTable,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = isWarpWingNode,
                handlesOwnWarping = isWarpWingNode,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                guidAsString = nodeGUID,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = oneWayLoopTarget,
                flow = targetflow
            };

            return m_CachedNode;
        }        
        
        
        // Retrieve sharedInjectionData from a specific floor if one is available
        public static List<SharedInjectionData> RetrieveSharedInjectionDataListFromCurrentFloor() {
            Dungeon dungeon = GameManager.Instance.CurrentlyGeneratingDungeonPrefab;
            
            if (dungeon == null) {
                dungeon = GameManager.Instance.Dungeon;
                if (dungeon == null) { return new List<SharedInjectionData>(0); }
                
            }

            if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON | 
                dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.PHOBOSGEON |
                dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FINALGEON |
                dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.HELLGEON |
                dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON |
                dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.RATGEON)
            {
                return new List<SharedInjectionData>(0);
            }

            List<SharedInjectionData> m_CachedInjectionDataList = new List<SharedInjectionData>(0);

            if (dungeon.PatternSettings != null && dungeon.PatternSettings.flows != null && dungeon.PatternSettings.flows.Count > 0) {
                if (dungeon.PatternSettings.flows[0].sharedInjectionData != null && dungeon.PatternSettings.flows[0].sharedInjectionData.Count > 0) {
                    m_CachedInjectionDataList = dungeon.PatternSettings.flows[0].sharedInjectionData;
                }
            }
            
            return m_CachedInjectionDataList;
        }

        public static ProceduralFlowModifierData RickRollSecretRoomInjector;

        public static SharedInjectionData CustomSecretFloorSharedInjectionData;


        // Initialize KnownFlows array with custom + official flows.
        public static void InitDungeonFlows(bool refreshFlows = false) {

            Dungeon TutorialPrefab = DungeonDatabase.GetOrLoadByName("Base_Tutorial");
            Dungeon CastlePrefab = DungeonDatabase.GetOrLoadByName("Base_Castle");
            Dungeon SewerPrefab = DungeonDatabase.GetOrLoadByName("Base_Sewer");
            Dungeon GungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Gungeon");
            Dungeon CathedralPrefab = DungeonDatabase.GetOrLoadByName("Base_Cathedral");
            Dungeon MinesPrefab = DungeonDatabase.GetOrLoadByName("Base_Mines");
            Dungeon ResourcefulRatPrefab = DungeonDatabase.GetOrLoadByName("Base_ResourcefulRat");
            Dungeon CatacombsPrefab = DungeonDatabase.GetOrLoadByName("Base_Catacombs");
            Dungeon NakatomiPrefab = DungeonDatabase.GetOrLoadByName("Base_Nakatomi");
            Dungeon ForgePrefab = DungeonDatabase.GetOrLoadByName("Base_Forge");
            Dungeon BulletHellPrefab = DungeonDatabase.GetOrLoadByName("Base_BulletHell");

            BaseSharedInjectionData = ExpandPrefabs.sharedAssets2.LoadAsset<SharedInjectionData>("Base Shared Injection Data");
            GungeonInjectionData = GungeonPrefab.PatternSettings.flows[0].sharedInjectionData[1];
            SewersInjectionData = SewerPrefab.PatternSettings.flows[0].sharedInjectionData[1];
            HollowsInjectionData = CatacombsPrefab.PatternSettings.flows[0].sharedInjectionData[1];
            CastleInjectionData = CastlePrefab.PatternSettings.flows[0].sharedInjectionData[0];

            Foyer_Flow = FlowHelpers.DuplicateDungeonFlow(ExpandPrefabs.sharedAssets2.LoadAsset<DungeonFlow>("Foyer Flow"));

            // List<DungeonFlow> m_knownFlows = new List<DungeonFlow>();
            KnownFlows = new List<DungeonFlow>();

            // Build and add custom flows to list.
            BossrushFlows.InitBossrushFlows();

            KnownFlows.Add(custom_glitchchest_flow.Custom_GlitchChest_Flow());
            KnownFlows.Add(test_west_floor_03a_flow.TEST_West_Floor_03a_Flow());
            KnownFlows.Add(demo_stage_flow.DEMO_STAGE_FLOW());
            KnownFlows.Add(complex_flow_test.Complex_Flow_Test());
            KnownFlows.Add(custom_glitch_flow.Custom_Glitch_Flow());
            KnownFlows.Add(really_big_flow.Really_Big_Flow());
            KnownFlows.Add(fruit_loops.Fruit_Loops());
            KnownFlows.Add(custom_glitchchestalt_flow.Custom_GlitchChestAlt_Flow());
            KnownFlows.Add(secretglitchfloor_flow.SecretGlitchFloor_Flow());
            KnownFlows.Add(test_traproom_flow.Test_TrapRoom_Flow());
            KnownFlows.Add(test_customroom_flow.Test_CustomRoom_Flow());
            KnownFlows.Add(apache_fucking_around_flow.Apache_Fucking_Around_Flow());
            KnownFlows.Add(f1b_jungle_flow_01.F1b_Jungle_Flow_01());
            KnownFlows.Add(f1b_jungle_flow_02.F1b_Jungle_Flow_02());
            KnownFlows.Add(f2b_belly_flow_01.F2b_Belly_Flow_01());

            // Fix issues with nodes so that things other then MainMenu can load Foyer flow
            Foyer_Flow.name = "Foyer_Flow";
            Foyer_Flow.AllNodes[1].handlesOwnWarping = true;
            Foyer_Flow.AllNodes[2].handlesOwnWarping = true;
            Foyer_Flow.AllNodes[3].handlesOwnWarping = true;

            KnownFlows.Add(Foyer_Flow);
            KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(LoadOfficialFlow("npcparadise")));
            KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(LoadOfficialFlow("secret_doublebeholster_flow")));
            KnownFlows.Add(BossrushFlows.Bossrush_01_Castle);
            KnownFlows.Add(BossrushFlows.Bossrush_01a_Sewer);
            KnownFlows.Add(BossrushFlows.Bossrush_02_Gungeon);
            KnownFlows.Add(BossrushFlows.Bossrush_02a_Cathedral);
            KnownFlows.Add(BossrushFlows.Bossrush_03_Mines);
            KnownFlows.Add(BossrushFlows.Bossrush_04_Catacombs);
            KnownFlows.Add(BossrushFlows.Bossrush_05_Forge);
            KnownFlows.Add(BossrushFlows.Bossrush_06_BulletHell);
            KnownFlows.Add(BossrushFlows.MiniBossrush_01);

            // Add official flows to list (flows found in Dungeon asset bundles after AG&D)
            for (int i = 0; i < TutorialPrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(TutorialPrefab.PatternSettings.flows[i]));
            }
            for (int i = 0; i < CastlePrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(CastlePrefab.PatternSettings.flows[i]));
            }
            for (int i = 0; i < SewerPrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(SewerPrefab.PatternSettings.flows[i]));
            }
            for (int i = 0; i < GungeonPrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(GungeonPrefab.PatternSettings.flows[i]));
            }
            for (int i = 0; i < CathedralPrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(CathedralPrefab.PatternSettings.flows[i]));
            }
            for (int i = 0; i < MinesPrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(MinesPrefab.PatternSettings.flows[i]));
            }
            for (int i = 0; i < ResourcefulRatPrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(ResourcefulRatPrefab.PatternSettings.flows[i]));
            }
            for (int i = 0; i < CatacombsPrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(CatacombsPrefab.PatternSettings.flows[i]));
            }
            for (int i = 0; i < NakatomiPrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(NakatomiPrefab.PatternSettings.flows[i]));
            }
            for (int i = 0; i < ForgePrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(ForgePrefab.PatternSettings.flows[i]));
            }
            for (int i = 0; i < BulletHellPrefab.PatternSettings.flows.Count; i++) {
                KnownFlows.Add(FlowHelpers.DuplicateDungeonFlow(BulletHellPrefab.PatternSettings.flows[i]));
            }

            // Let's make things look cool and give all boss rush flows my new tiny exit room. :D            
            BossrushFlows.Bossrush_01a_Sewer.AllNodes[2].overrideExactRoom = ExpandPrefabs.tiny_exit;
            BossrushFlows.Bossrush_02_Gungeon.AllNodes[6].overrideExactRoom = ExpandPrefabs.tiny_exit;
            BossrushFlows.Bossrush_02a_Cathedral.AllNodes[2].overrideExactRoom = ExpandPrefabs.oldbulletking_room_01;
            BossrushFlows.Bossrush_02a_Cathedral.AllNodes[3].overrideExactRoom = ExpandPrefabs.tiny_exit;
            BossrushFlows.Bossrush_03_Mines.AllNodes[6].overrideExactRoom = ExpandPrefabs.tiny_exit;
            BossrushFlows.Bossrush_04_Catacombs.AllNodes[6].overrideExactRoom = ExpandPrefabs.tiny_exit;
            // Fix Forge Bossrush so it uses the correct boss foyer room for Dragun.
            // Using the same foyer room for previous floors looks odd so I fixed it. :P
            BossrushFlows.Bossrush_05_Forge.AllNodes[1].overrideExactRoom = ExpandPrefabs.DragunBossFoyerRoom;
            BossrushFlows.Bossrush_05_Forge.AllNodes[3].overrideExactRoom = ExpandPrefabs.tiny_exit;
            

            JunkSecretRoomInjector = new ProceduralFlowModifierData() {
                annotation = "Tiny Secret Room",
                DEBUG_FORCE_SPAWN = false,
                OncePerRun = false,
                placementRules = new List<ProceduralFlowModifierData.FlowModifierPlacementType>() {
                    ProceduralFlowModifierData.FlowModifierPlacementType.RANDOM_NODE_CHILD
                },
                roomTable = null,
                exactRoom = ExpandRoomPrefabs.Expand_TinySecret,
                IsWarpWing = false,
                RequiresMasteryToken = false,
                chanceToLock = 0,
                selectionWeight = 1,
                chanceToSpawn = 1,
                RequiredValidPlaceable = null,
                prerequisites = new DungeonPrerequisite[0],
                CanBeForcedSecret = true,
                RandomNodeChildMinDistanceFromEntrance = 0,
                exactSecondaryRoom = null,
                framedCombatNodes = 0,
            };

            BellySpecialEntranceRoomInjector = new ProceduralFlowModifierData() {
                annotation = "Secret Belly Entrance Room",
                DEBUG_FORCE_SPAWN = false,
                OncePerRun = false,
                placementRules = new List<ProceduralFlowModifierData.FlowModifierPlacementType>() {
                    ProceduralFlowModifierData.FlowModifierPlacementType.END_OF_CHAIN
                },
                roomTable = null,
                exactRoom = ExpandRoomPrefabs.Expand_Gungeon_BellyEntranceRoom,
                IsWarpWing = false,
                RequiresMasteryToken = false,
                chanceToLock = 0,
                selectionWeight = 1,
                chanceToSpawn = 1,
                RequiredValidPlaceable = null,
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.EQUAL_TO,
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        requiredTileset = GlobalDungeonData.ValidTilesets.GUNGEON,
                        requireTileset = true,
                        comparisonValue = 1,
                        encounteredObjectGuid = string.Empty,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        requireDemoMode = false,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requireFlag = false,
                        useSessionStatValue = false,
                        encounteredRoom = null,
                        requiredNumberOfEncounters = -1,
                        saveFlagToCheck = GungeonFlags.TUTORIAL_COMPLETED,
                        statToCheck = TrackedStats.GUNBERS_MUNCHED
                    }
                },
                CanBeForcedSecret = true,
                RandomNodeChildMinDistanceFromEntrance = 0,
                exactSecondaryRoom = null,
                framedCombatNodes = 0,
            };

            BellySpecialMonsterRoomInjector = new ProceduralFlowModifierData() {
                annotation = "Secret Belly Monster Room",
                DEBUG_FORCE_SPAWN = false,
                OncePerRun = false,
                placementRules = new List<ProceduralFlowModifierData.FlowModifierPlacementType>() {
                    ProceduralFlowModifierData.FlowModifierPlacementType.NO_LINKS
                },
                roomTable = null,
                exactRoom = ExpandRoomPrefabs.Expand_Gungeon_HiddenMonsterRoom,
                IsWarpWing = true,
                RequiresMasteryToken = false,
                chanceToLock = 0,
                selectionWeight = 1,
                chanceToSpawn = 1,
                RequiredValidPlaceable = null,
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.EQUAL_TO,
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        requiredTileset = GlobalDungeonData.ValidTilesets.GUNGEON,
                        requireTileset = true,
                        comparisonValue = 1,
                        encounteredObjectGuid = string.Empty,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        requireDemoMode = false,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requireFlag = false,
                        useSessionStatValue = false,
                        encounteredRoom = null,
                        requiredNumberOfEncounters = -1,
                        saveFlagToCheck = GungeonFlags.TUTORIAL_COMPLETED,
                        statToCheck = TrackedStats.GUNBERS_MUNCHED
                    }
                },
                CanBeForcedSecret = true,
                RandomNodeChildMinDistanceFromEntrance = 0,
                exactSecondaryRoom = null,
                framedCombatNodes = 0,
            };
            
            GungeonInjectionData.InjectionData.Add(BellySpecialEntranceRoomInjector);
            GungeonInjectionData.InjectionData.Add(BellySpecialMonsterRoomInjector);

            SewersInjectionData.InjectionData.Add(JunkSecretRoomInjector);


            SecretFloorEntranceInjector = new ProceduralFlowModifierData() {
                annotation = "Secret Floor Entrance Room",
                DEBUG_FORCE_SPAWN = false,
                OncePerRun = false,
                placementRules = new List<ProceduralFlowModifierData.FlowModifierPlacementType>() {
                    ProceduralFlowModifierData.FlowModifierPlacementType.NO_LINKS
                },
                roomTable = null,                
                // exactRoom = SewersInjectionData.InjectionData[0].exactRoom,
                exactRoom = ExpandRoomPrefabs.SecretExitRoom2,
                IsWarpWing = true,
                RequiresMasteryToken = false,
                chanceToLock = 0,
                selectionWeight = 1,
                chanceToSpawn = 1,
                RequiredValidPlaceable = null,
                prerequisites = new DungeonPrerequisite[0],
                CanBeForcedSecret = true,
                RandomNodeChildMinDistanceFromEntrance = 0,
                exactSecondaryRoom = null,
                framedCombatNodes = 0,
            };

            SecretMiniElevatorInjector = new ProceduralFlowModifierData() {
                annotation = "Secret MiniElevator Room with Glitched Rats",
                DEBUG_FORCE_SPAWN = false,
                OncePerRun = false,
                placementRules = new List<ProceduralFlowModifierData.FlowModifierPlacementType>() {
                    ProceduralFlowModifierData.FlowModifierPlacementType.RANDOM_NODE_CHILD
                },
                roomTable = null,
                exactRoom = ExpandRoomPrefabs.SecretRatEntranceRoom,
                IsWarpWing = false,
                RequiresMasteryToken = false,
                chanceToLock = 0.25f,
                selectionWeight = 1,
                chanceToSpawn = 1,
                RequiredValidPlaceable = null,
                prerequisites = new DungeonPrerequisite[0],
                CanBeForcedSecret = true,
                RandomNodeChildMinDistanceFromEntrance = 0,
                exactSecondaryRoom = null,
                framedCombatNodes = 0,
            };


            m_KeepJungleEntranceRooms = ScriptableObject.CreateInstance<GenericRoomTable>();
            m_KeepJungleEntranceRooms.includedRoomTables = new List<GenericRoomTable>(0);
            m_KeepJungleEntranceRooms.includedRooms = new WeightedRoomCollection() {
                elements = new List<WeightedRoom>() {
                    ExpandRoomPrefabs.GenerateWeightedRoom(ExpandRoomPrefabs.Expand_Keep_TreeRoom, Weight: 0.5f),
                    ExpandRoomPrefabs.GenerateWeightedRoom(ExpandRoomPrefabs.Expand_Keep_TreeRoom2)
                }
            };


            SecretJungleEntranceInjector = new ProceduralFlowModifierData() {
                annotation = "Secret Jungle Entrance Room",
                DEBUG_FORCE_SPAWN = false,
                OncePerRun = false,
                placementRules = new List<ProceduralFlowModifierData.FlowModifierPlacementType>() {
                    ProceduralFlowModifierData.FlowModifierPlacementType.RANDOM_NODE_CHILD
                },
                roomTable = m_KeepJungleEntranceRooms,
                exactRoom = null,
                IsWarpWing = false,
                RequiresMasteryToken = false,
                chanceToLock = 0,
                selectionWeight = 1,
                chanceToSpawn = 1,
                RequiredValidPlaceable = null,
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.EQUAL_TO,
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,                        
                        requiredTileset = GlobalDungeonData.ValidTilesets.CASTLEGEON,
                        requireTileset = true,
                        comparisonValue = 1,
                        encounteredObjectGuid = string.Empty,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        requireDemoMode = false,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requireFlag = false,
                        useSessionStatValue = false,
                        encounteredRoom = null,
                        requiredNumberOfEncounters = -1,
                        saveFlagToCheck = GungeonFlags.TUTORIAL_COMPLETED,
                        statToCheck = TrackedStats.GUNBERS_MUNCHED
                    }
                },
                CanBeForcedSecret = true,
                RandomNodeChildMinDistanceFromEntrance = 0,
                exactSecondaryRoom = null,
                framedCombatNodes = 0,
            };

            HollowsInjectionData.InjectionData.Add(SecretFloorEntranceInjector);
            HollowsInjectionData.InjectionData.Add(SecretMiniElevatorInjector);
            CastleInjectionData.InjectionData.Add(SecretJungleEntranceInjector);


            RickRollSecretRoomInjector = new ProceduralFlowModifierData() {
                annotation = "RickRoll Secret Room",
                DEBUG_FORCE_SPAWN = false,
                OncePerRun = false,
                placementRules = new List<ProceduralFlowModifierData.FlowModifierPlacementType>() {
                    ProceduralFlowModifierData.FlowModifierPlacementType.END_OF_CHAIN
                },
                roomTable = null,
                exactRoom = ExpandRoomPrefabs.Expand_RickRollSecret,
                IsWarpWing = false,
                RequiresMasteryToken = false,
                chanceToLock = 0,
                selectionWeight = 1,
                chanceToSpawn = 0.25f,
                RequiredValidPlaceable = null,
                prerequisites = new DungeonPrerequisite[0],
                CanBeForcedSecret = true,
                RandomNodeChildMinDistanceFromEntrance = 1,
                exactSecondaryRoom = null,
                framedCombatNodes = 0
            };

            CustomSecretFloorSharedInjectionData = ScriptableObject.CreateInstance<SharedInjectionData>();

            CustomSecretFloorSharedInjectionData.name = "Rick Roll Secret Room Injection Data";
            CustomSecretFloorSharedInjectionData.InjectionData = new List<ProceduralFlowModifierData>() { RickRollSecretRoomInjector };
            CustomSecretFloorSharedInjectionData.UseInvalidWeightAsNoInjection = true;
            CustomSecretFloorSharedInjectionData.IsNPCCell = false;
            CustomSecretFloorSharedInjectionData.PreventInjectionOfFailedPrerequisites = false;
            CustomSecretFloorSharedInjectionData.OnlyOne = false;
            CustomSecretFloorSharedInjectionData.ChanceToSpawnOne = 1;
            CustomSecretFloorSharedInjectionData.AttachedInjectionData = new List<SharedInjectionData>(0);
            
            ExpandObjectDatabase objectDatabase = new ExpandObjectDatabase();

            // Due to load order, I need to set this up here instead.
            ExpandRoomPrefabs.Expand_Jungle_OldCrest.associatedMinimapIcon = SewersInjectionData.InjectionData[1].exactRoom.associatedMinimapIcon;
            RoomBuilder.AddObjectToRoom(ExpandRoomPrefabs.Expand_Jungle_OldCrest, new Vector2(5, 7), objectDatabase.GodRays);
            RoomBuilder.AddObjectToRoom(ExpandRoomPrefabs.Expand_Jungle_OldCrest, new Vector2(7, 8), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Jungle_ItemStump, useExternalPrefab: true), xOffset: 6);
            // RoomBuilder.AddObjectToRoom(ExpandRoomPrefabs.Expand_Jungle_OldCrest, new Vector2(7, 7), NonEnemyBehaviour: SewersInjectionData.InjectionData[1].exactRoom.additionalObjectLayers[1].placedObjects[0].nonenemyBehaviour, xOffset: 4, yOffset: 8);

            objectDatabase = null;
            TutorialPrefab = null;
            CastlePrefab = null;
            SewerPrefab = null;
            GungeonPrefab = null;
            CathedralPrefab = null;
            MinesPrefab = null;
            ResourcefulRatPrefab = null;
            CatacombsPrefab = null;
            NakatomiPrefab = null;
            ForgePrefab = null;
            BulletHellPrefab = null;
        }
    }
}

