using Dungeonator;
using System;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using System.Reflection;
using MonoMod.RuntimeDetour;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandDungeonFlows;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandCustomDungeonPrefabs : DungeonDatabase {

        public static GameObject GameManagerObject;
                
        public static List<string> customDungeons;

        // public static GameObject Base_Canyon;

        public static GameLevelDefinition CanyonDefinition;
        public static GameLevelDefinition JungleDefinition;

        /*public static Dungeon GetOrLoadByNameHook(string name) {
            Dungeon dungeon = null;
            foreach (string dungeonName in customDungeons) {
                if (dungeonName.ToLower() == name.ToLower()) {
                    if (name.ToLower() == "base_canyon") {
                        dungeon = CanyonDungeon(GetOrLoadByName_Orig("Base_ResourcefulRat"));
                        break;
                    } else if (name.ToLower() == "base_jungle") {
                        dungeon = JungleDungeon(GetOrLoadByName_Orig("Base_ResourcefulRat"));
                        break;
                    }
                }
            }
            if (dungeon) {
                DebugTime.RecordStartTime();
                DebugTime.Log("AssetBundle.LoadAsset<Dungeon>({0})", new object[] { name });
                return dungeon;
            } else {
                return GetOrLoadByName_Orig(name);
            }
        }*/

        public static Dungeon GetOrLoadByNameHook(Func<string, Dungeon>orig, string name) {
            Dungeon dungeon = null;
            foreach (string dungeonName in customDungeons) {
                if (dungeonName.ToLower() == name.ToLower()) {
                    if (name.ToLower() == "base_canyon") {
                        dungeon = CanyonDungeon(GetOrLoadByName_Orig("Base_ResourcefulRat"));
                        break;
                    } else if (name.ToLower() == "base_jungle") {
                        dungeon = JungleDungeon(GetOrLoadByName_Orig("Base_ResourcefulRat"));
                        break;
                    }
                }
            }
            if (dungeon) {
                DebugTime.RecordStartTime();
                DebugTime.Log("AssetBundle.LoadAsset<Dungeon>({0})", new object[] { name });
                return dungeon;
            } else {
                return orig(name);
            }
        }
                
        public static Dungeon GetOrLoadByName_Orig(string name) {
            AssetBundle assetBundle = ResourceManager.LoadAssetBundle("dungeons/" + name.ToLower());
            DebugTime.RecordStartTime();
            Dungeon component = assetBundle.LoadAsset<GameObject>(name).GetComponent<Dungeon>();
            DebugTime.Log("AssetBundle.LoadAsset<Dungeon>({0})", new object[] { name });
            return component;
        }

        public static Hook getOrLoadByName_Hook;
        public static Hook dungeonStartHook;

        public void DungeonStart_Hook(Action<ItemDB>orig, ItemDB self) {
            List<WeightedGameObject> collection;
            if (self.ModLootPerFloor.TryGetValue("ANY", out collection)) {
                GameManager.Instance.Dungeon.baseChestContents.defaultItemDrops.elements.AddRange(collection);
            }
            string dungeonFloorName = GameManager.Instance.Dungeon.DungeonFloorName;
            if (!string.IsNullOrEmpty(dungeonFloorName) && dungeonFloorName.StartsWith("#")) {
                string key = dungeonFloorName.Substring(1, dungeonFloorName.IndexOf('_') - 1);
                if (self.ModLootPerFloor.TryGetValue(key, out collection)) {
                    GameManager.Instance.Dungeon.baseChestContents.defaultItemDrops.elements.AddRange(collection);
                }
            }
        }

        public static void InitCustomDungeons() {

            if (ExpandStats.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonDatabase.GetOrLoadByName Hook..."); }
            getOrLoadByName_Hook = new Hook(
                typeof(DungeonDatabase).GetMethod("GetOrLoadByName", BindingFlags.Static | BindingFlags.Public),
                typeof(ExpandCustomDungeonPrefabs).GetMethod("GetOrLoadByNameHook", BindingFlags.Static | BindingFlags.Public)
            );

            if (ExpandStats.debugMode) { Debug.Log("[ExpandTheGungeon] Installing ItemDB.DungeonStart Hook..."); }
            dungeonStartHook = new Hook(
                typeof(ItemDB).GetMethod("DungeonStart", BindingFlags.Instance | BindingFlags.Public),
                typeof(ExpandCustomDungeonPrefabs).GetMethod("DungeonStart_Hook", BindingFlags.Instance | BindingFlags.Public),
                typeof(ItemDB)
            );

            customDungeons = new List<string>() { "Base_Canyon", "Base_Jungle" };
            
            AssetBundle braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");
            GameManagerObject = braveResources.LoadAsset<GameObject>("_GameManager");

            CanyonDefinition = new GameLevelDefinition() {
                dungeonSceneName = "tt_canyon",
                dungeonPrefabPath = "Base_Canyon",
                priceMultiplier = 2,
                secretDoorHealthMultiplier = 1,
                enemyHealthMultiplier = 2.1f,
                damageCap = 300,
                bossDpsCap = 78,
                flowEntries = new List<DungeonFlowLevelEntry>(0),
                predefinedSeeds = new List<int>(0)
            };

            foreach (GameLevelDefinition levelDefinition in GameManager.Instance.customFloors) {
                if (levelDefinition.dungeonSceneName == "tt_jungle") {
                    JungleDefinition = levelDefinition;
                    break;
                }
            }
            
            if (JungleDefinition != null) {
                JungleDefinition.priceMultiplier = 1.20000005f;
                JungleDefinition.secretDoorHealthMultiplier = 1;
                JungleDefinition.enemyHealthMultiplier = 1.33329999f;
                JungleDefinition.damageCap = 300;
                JungleDefinition.bossDpsCap = 42;
            }

            for(int i = 0; i < GameManagerObject.GetComponent<GameManager>().customFloors.Count; i++) {
                if (GameManagerObject.GetComponent<GameManager>().customFloors[i].dungeonSceneName == "tt_jungle") {
                    GameLevelDefinition levelDefinition = GameManagerObject.GetComponent<GameManager>().customFloors[i];
                    levelDefinition.priceMultiplier = 1.20000005f;
                    levelDefinition.secretDoorHealthMultiplier = 1;
                    levelDefinition.enemyHealthMultiplier = 1.33329999f;
                    levelDefinition.damageCap = 300;
                    levelDefinition.bossDpsCap = 42;
                    break;
                }
            }

            GameManager.Instance.customFloors.Add(CanyonDefinition);
            GameManagerObject.GetComponent<GameManager>().customFloors.Add(CanyonDefinition);

            braveResources = null;
        }

        public static void ReInitFloorDefinitions() {

            bool EntryNotExist = true;
            bool EntryNotExist2 = true;

            if (CanyonDefinition == null) {
                CanyonDefinition = new GameLevelDefinition() {
                    dungeonSceneName = "tt_canyon",
                    dungeonPrefabPath = "Base_Canyon",
                    priceMultiplier = 2,
                    secretDoorHealthMultiplier = 1,
                    enemyHealthMultiplier = 2.1f,
                    damageCap = 300,
                    bossDpsCap = 78,
                    flowEntries = new List<DungeonFlowLevelEntry>(0),
                    predefinedSeeds = new List<int>(0)
                };
            }

            if (JungleDefinition == null) {
                foreach (GameLevelDefinition levelDefinition in GameManager.Instance.customFloors) {
                    if (levelDefinition.dungeonSceneName == "tt_jungle") {
                        JungleDefinition = levelDefinition;
                        break;
                    }
                }

                if (JungleDefinition != null) {
                    JungleDefinition.priceMultiplier = 1.20000005f;
                    JungleDefinition.secretDoorHealthMultiplier = 1;
                    JungleDefinition.enemyHealthMultiplier = 1.33329999f;
                    JungleDefinition.damageCap = 300;
                    JungleDefinition.bossDpsCap = 42;
                }
            }


            if (GameManager.Instance && GameManager.Instance.customFloors != null) {
                foreach (GameLevelDefinition definition in GameManager.Instance.customFloors) {
                    if (definition.dungeonSceneName == "tt_canyon") { EntryNotExist = false; }
                    break;
                }

                for(int i = 0; i < GameManagerObject.GetComponent<GameManager>().customFloors.Count; i++) {
                    if (GameManagerObject.GetComponent<GameManager>().customFloors[i].dungeonSceneName == "tt_jungle") {
                        GameLevelDefinition levelDefinition = GameManagerObject.GetComponent<GameManager>().customFloors[i];
                        levelDefinition.priceMultiplier = 1.20000005f;
                        levelDefinition.secretDoorHealthMultiplier = 1;
                        levelDefinition.enemyHealthMultiplier = 1.33329999f;
                        levelDefinition.damageCap = 300;
                        levelDefinition.bossDpsCap = 42;
                        break;
                    }
                }

                if (EntryNotExist) { GameManager.Instance.customFloors.Add(CanyonDefinition); }
            }

            if (GameManagerObject && GameManagerObject.GetComponent<GameManager>() && GameManagerObject.GetComponent<GameManager>().customFloors != null) {
                foreach (GameLevelDefinition definition in GameManagerObject.GetComponent<GameManager>().customFloors) {
                    if (definition.dungeonSceneName == "tt_canyon") { EntryNotExist2 = false; }
                    break;
                }
                if (EntryNotExist2) { GameManagerObject.GetComponent<GameManager>().customFloors.Add(CanyonDefinition); }
            } else if (GameManagerObject == null) {
                AssetBundle braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");
                GameManagerObject = braveResources.LoadAsset<GameObject>("_GameManager");
                if (GameManagerObject && GameManagerObject.GetComponent<GameManager>() && GameManagerObject.GetComponent<GameManager>().customFloors != null) {
                    GameManagerObject.GetComponent<GameManager>().customFloors.Add(CanyonDefinition);
                }
                braveResources = null;
            }
        }


        public static Dungeon CanyonDungeon(Dungeon dungeon) {
            Dungeon MinesDungeonPrefab = GetOrLoadByName_Orig("Base_Mines");
            Dungeon RatDungeonPrefab = GetOrLoadByName_Orig("Base_ResourcefulRat");
            Dungeon FinalScenarioPilotPrefab = GetOrLoadByName_Orig("FinalScenario_Pilot");
            Dungeon FinalScenarioBulletPrefab = GetOrLoadByName_Orig("FinalScenario_Bullet");
            DungeonMaterial FinalScenario_MainMaterial = UnityEngine.Object.Instantiate(FinalScenarioPilotPrefab.roomMaterialDefinitions[0]);
            FinalScenario_MainMaterial.supportsPits = true;
            FinalScenario_MainMaterial.doPitAO = false;
            // FinalScenario_MainMaterial.pitsAreOneDeep = true;
            FinalScenario_MainMaterial.useLighting = true;
            // FinalScenario_MainMaterial.supportsLavaOrLavalikeSquares = true;
            FinalScenario_MainMaterial.lightPrefabs.elements[0].rawGameObject = MinesDungeonPrefab.roomMaterialDefinitions[0].lightPrefabs.elements[0].rawGameObject;
            FinalScenario_MainMaterial.roomFloorBorderGrid = MinesDungeonPrefab.roomMaterialDefinitions[0].roomFloorBorderGrid;
            FinalScenario_MainMaterial.pitLayoutGrid = MinesDungeonPrefab.roomMaterialDefinitions[0].pitLayoutGrid;
            FinalScenario_MainMaterial.pitBorderFlatGrid = MinesDungeonPrefab.roomMaterialDefinitions[0].pitBorderFlatGrid;

            DungeonTileStampData m_CanyonStampData = ScriptableObject.CreateInstance<DungeonTileStampData>();
            m_CanyonStampData.name = "ENV_CANYON_STAMP_DATA";
            m_CanyonStampData.tileStampWeight = 0;
            m_CanyonStampData.spriteStampWeight = 0;
            m_CanyonStampData.objectStampWeight = 1;
            m_CanyonStampData.stamps = new TileStampData[0];
            m_CanyonStampData.spriteStamps = new SpriteStampData[0];
            m_CanyonStampData.objectStamps = RatDungeonPrefab.stampData.objectStamps;
            m_CanyonStampData.SymmetricFrameChance = 0.25f;
            m_CanyonStampData.SymmetricCompleteChance = 0.6f;

            dungeon.gameObject.name = "Base_Canyon";            
            dungeon.contentSource = ContentSource.CONTENT_UPDATE_03;
            dungeon.DungeonSeed = 0;
            dungeon.DungeonFloorName = "A Corrupted Place.";
            dungeon.DungeonShortName = "A Corrupted Place.";
            dungeon.DungeonFloorLevelTextOverride = "Beneath the Melting Permafrost.";
            dungeon.LevelOverrideType = GameManager.LevelOverrideState.NONE;
            dungeon.debugSettings = new DebugDungeonSettings() {
                RAPID_DEBUG_DUNGEON_ITERATION_SEEKER = false,
                RAPID_DEBUG_DUNGEON_ITERATION = false,
                RAPID_DEBUG_DUNGEON_COUNT = 50,
                GENERATION_VIEWER_MODE = false,
                FULL_MINIMAP_VISIBILITY = false,
                COOP_TEST = false,
                DISABLE_ENEMIES = false,
                DISABLE_LOOPS = false,
                DISABLE_SECRET_ROOM_COVERS = false,
                DISABLE_OUTLINES = false,
                WALLS_ARE_PITS = false
            };
            dungeon.PatternSettings = new SemioticDungeonGenSettings() {
                flows = new List<DungeonFlow>() { ExpandDungeonFlows.secretglitchfloor_flow.SecretGlitchFloor_Flow() },
                mandatoryExtraRooms = new List<ExtraIncludedRoomData>(0),
                optionalExtraRooms = new List<ExtraIncludedRoomData>(0),
                MAX_GENERATION_ATTEMPTS = 250,
                DEBUG_RENDER_CANVASES_SEPARATELY = false
            };            
            dungeon.ForceRegenerationOfCharacters = false;
            dungeon.ActuallyGenerateTilemap = true;
            dungeon.decoSettings = new TilemapDecoSettings {
                standardRoomVisualSubtypes = new WeightedIntCollection {
                    elements = new WeightedInt[] {
                        new WeightedInt() {
                            annotation = "pilot final",
                            value = 0,
                            weight = 1,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "pilot final2",
                            value = 1,
                            weight = 1,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "shop",
                            value = 2,
                            weight = 0,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "pilot final2",
                            value = 3,
                            weight = 1,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "pilot final3",
                            value = 4,
                            weight = 1,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "pilot final4",
                            value = 5,
                            weight = 1,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "pilot final5",
                            value = 6,
                            weight = 1,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        }
                    }
                },
                decalLayerStyle = MinesDungeonPrefab.decoSettings.decalLayerStyle,
                decalSize = MinesDungeonPrefab.decoSettings.decalSize,
                decalSpacing = MinesDungeonPrefab.decoSettings.decalSpacing,
                decalExpansion = MinesDungeonPrefab.decoSettings.decalExpansion,
                patternLayerStyle = MinesDungeonPrefab.decoSettings.patternLayerStyle,
                patternSize = MinesDungeonPrefab.decoSettings.patternSize,
                patternSpacing = MinesDungeonPrefab.decoSettings.patternSpacing,
                patternExpansion = MinesDungeonPrefab.decoSettings.patternExpansion,
                decoPatchFrequency = MinesDungeonPrefab.decoSettings.decoPatchFrequency,
                ambientLightColor = MinesDungeonPrefab.decoSettings.ambientLightColor,
                ambientLightColorTwo = MinesDungeonPrefab.decoSettings.ambientLightColorTwo,
                lowQualityAmbientLightColor = MinesDungeonPrefab.decoSettings.lowQualityAmbientLightColor,
                lowQualityAmbientLightColorTwo = MinesDungeonPrefab.decoSettings.lowQualityAmbientLightColorTwo,
                lowQualityCheapLightVector = MinesDungeonPrefab.decoSettings.lowQualityCheapLightVector,
                UsesAlienFXFloorColor = MinesDungeonPrefab.decoSettings.UsesAlienFXFloorColor,
                AlienFXFloorColor = MinesDungeonPrefab.decoSettings.AlienFXFloorColor,
                generateLights = MinesDungeonPrefab.decoSettings.generateLights,
                lightCullingPercentage = MinesDungeonPrefab.decoSettings.lightCullingPercentage,
                lightOverlapRadius = MinesDungeonPrefab.decoSettings.lightOverlapRadius,
                nearestAllowedLight = MinesDungeonPrefab.decoSettings.nearestAllowedLight,
                minLightExpanseWidth = MinesDungeonPrefab.decoSettings.minLightExpanseWidth,
                lightHeight = MinesDungeonPrefab.decoSettings.lightHeight,
                lightCookies = MinesDungeonPrefab.decoSettings.lightCookies,
                debug_view = false
            };
            dungeon.tileIndices = new TileIndices() {
                tilesetId = GlobalDungeonData.ValidTilesets.PHOBOSGEON,
                dungeonCollection = ExpandUtility.ReplaceDungeonCollection(FinalScenarioPilotPrefab.tileIndices.dungeonCollection, ExpandPrefabs.ENV_Tileset_Canyon_Texture),
                dungeonCollectionSupportsDiagonalWalls = false,
                aoTileIndices = FinalScenarioBulletPrefab.tileIndices.aoTileIndices,
                placeBorders = true,
                placePits = false,
                chestHighWallIndices = new List<TileIndexVariant>() {
                    new TileIndexVariant() {
                        index = 41,
                        likelihood = 0.5f,
                        overrideLayerIndex = 0,
                        overrideIndex = 0
                    }
                },
                decalIndexGrid = null,
                patternIndexGrid = FinalScenarioBulletPrefab.tileIndices.patternIndexGrid,
                globalSecondBorderTiles = new List<int>(0),
                edgeDecorationTiles = null
            };
            dungeon.tileIndices.dungeonCollection.name = "ENV_Canyon_Collection";
            dungeon.roomMaterialDefinitions = new DungeonMaterial[] {
                FinalScenario_MainMaterial,
                FinalScenario_MainMaterial,
                FinalScenario_MainMaterial,
                FinalScenario_MainMaterial,
                FinalScenario_MainMaterial,
                FinalScenario_MainMaterial,
                FinalScenario_MainMaterial
            };
            dungeon.dungeonWingDefinitions = new DungeonWingDefinition[0];
            dungeon.pathGridDefinitions = new List<TileIndexGrid>() { MinesDungeonPrefab.pathGridDefinitions[0] };
            dungeon.dungeonDustups = new DustUpVFX() {
                runDustup = MinesDungeonPrefab.dungeonDustups.runDustup,
                waterDustup = MinesDungeonPrefab.dungeonDustups.waterDustup,
                additionalWaterDustup = MinesDungeonPrefab.dungeonDustups.additionalWaterDustup,
                rollNorthDustup = MinesDungeonPrefab.dungeonDustups.rollNorthDustup,
                rollNorthEastDustup = MinesDungeonPrefab.dungeonDustups.rollNorthEastDustup,
                rollEastDustup = MinesDungeonPrefab.dungeonDustups.rollEastDustup,
                rollSouthEastDustup = MinesDungeonPrefab.dungeonDustups.rollSouthEastDustup,
                rollSouthDustup = MinesDungeonPrefab.dungeonDustups.rollSouthDustup,
                rollSouthWestDustup = MinesDungeonPrefab.dungeonDustups.rollSouthWestDustup,
                rollWestDustup = MinesDungeonPrefab.dungeonDustups.rollWestDustup,
                rollNorthWestDustup = MinesDungeonPrefab.dungeonDustups.rollNorthWestDustup,
                rollLandDustup = MinesDungeonPrefab.dungeonDustups.rollLandDustup
            };
            dungeon.damageTypeEffectMatrix = MinesDungeonPrefab.damageTypeEffectMatrix;
            dungeon.stampData = m_CanyonStampData;
            dungeon.UsesCustomFloorIdea = false;
            dungeon.FloorIdea = new RobotDaveIdea() {
                ValidEasyEnemyPlaceables = new DungeonPlaceable[0],
                ValidHardEnemyPlaceables = new DungeonPlaceable[0],
                UseWallSawblades = false,
                UseRollingLogsVertical = true,
                UseRollingLogsHorizontal = true,
                UseFloorPitTraps = false,
                UseFloorFlameTraps = true,
                UseFloorSpikeTraps = true,
                UseFloorConveyorBelts = true,
                UseCaveIns = true,
                UseAlarmMushrooms = false,
                UseChandeliers = true,
                UseMineCarts = false,
                CanIncludePits = false
            };
            dungeon.PlaceDoors = true;
            dungeon.doorObjects = MinesDungeonPrefab.doorObjects;
            dungeon.oneWayDoorObjects = MinesDungeonPrefab.oneWayDoorObjects;
            dungeon.oneWayDoorPressurePlate = MinesDungeonPrefab.oneWayDoorPressurePlate;
            dungeon.phantomBlockerDoorObjects = MinesDungeonPrefab.phantomBlockerDoorObjects;
            dungeon.UsesWallWarpWingDoors = false;
            dungeon.baseChestContents = MinesDungeonPrefab.baseChestContents;
            dungeon.SecretRoomSimpleTriggersFacewall = new List<GameObject>() { MinesDungeonPrefab.SecretRoomSimpleTriggersFacewall[0] };
            dungeon.SecretRoomSimpleTriggersSidewall = new List<GameObject>() { MinesDungeonPrefab.SecretRoomSimpleTriggersSidewall[0] };
            dungeon.SecretRoomComplexTriggers = new List<ComplexSecretRoomTrigger>(0);
            dungeon.SecretRoomDoorSparkVFX = MinesDungeonPrefab.SecretRoomDoorSparkVFX;
            dungeon.SecretRoomHorizontalPoofVFX = MinesDungeonPrefab.SecretRoomHorizontalPoofVFX;
            dungeon.SecretRoomVerticalPoofVFX = MinesDungeonPrefab.SecretRoomVerticalPoofVFX;
            dungeon.sharedSettingsPrefab = MinesDungeonPrefab.sharedSettingsPrefab;
            dungeon.NormalRatGUID = string.Empty;
            dungeon.BossMasteryTokenItemId = CustomMasterRounds.CanyonMasterRoundID;
            dungeon.UsesOverrideTertiaryBossSets = false;
            dungeon.OverrideTertiaryRewardSets = new List<TertiaryBossRewardSet>(0);
            dungeon.defaultPlayerPrefab = MinesDungeonPrefab.defaultPlayerPrefab;
            dungeon.StripPlayerOnArrival = false;
            dungeon.SuppressEmergencyCrates = false;
            dungeon.SetTutorialFlag = false;
            dungeon.PlayerIsLight = true;
            dungeon.PlayerLightColor = MinesDungeonPrefab.PlayerLightColor;
            dungeon.PlayerLightIntensity = 4;
            dungeon.PlayerLightRadius = 4;
            dungeon.PrefabsToAutoSpawn = new GameObject[0];            
            dungeon.musicEventName = "Play_MUS_Dungeon_Rat_Theme_01";
            
            FinalScenarioPilotPrefab = null;
            RatDungeonPrefab = null;
            MinesDungeonPrefab = null;

            return dungeon;
        }

        public static Dungeon JungleDungeon(Dungeon dungeon) {
            AssetBundle braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");
            Dungeon MinesDungeonPrefab = GetOrLoadByName_Orig("Base_Mines");
            Dungeon GungeonPrefab = GetOrLoadByName_Orig("Base_Gungeon");
            Dungeon SewersPrefab = GetOrLoadByName_Orig("Base_Sewer");
            
            DungeonMaterial Jungle_Woods = ScriptableObject.CreateInstance<DungeonMaterial>();
            Jungle_Woods.wallShards = GungeonPrefab.roomMaterialDefinitions[0].wallShards;
            Jungle_Woods.bigWallShards = GungeonPrefab.roomMaterialDefinitions[0].bigWallShards;
            Jungle_Woods.bigWallShardDamageThreshold = 10;
            Jungle_Woods.fallbackVerticalTileMapEffects = GungeonPrefab.roomMaterialDefinitions[0].fallbackVerticalTileMapEffects;
            Jungle_Woods.fallbackHorizontalTileMapEffects = GungeonPrefab.roomMaterialDefinitions[0].fallbackHorizontalTileMapEffects;
            Jungle_Woods.pitfallVFXPrefab = null;
            Jungle_Woods.UsePitAmbientVFX = false;
            Jungle_Woods.AmbientPitVFX = new List<GameObject>(0);
            Jungle_Woods.PitVFXMinCooldown = 5;
            Jungle_Woods.PitVFXMaxCooldown = 30;
            Jungle_Woods.ChanceToSpawnPitVFXOnCooldown = 1;
            Jungle_Woods.stampFailChance = 0.2f;
            Jungle_Woods.overrideTableTable = null;
            Jungle_Woods.supportsPits = false;
            Jungle_Woods.doPitAO = true;
            Jungle_Woods.pitsAreOneDeep = false;
            Jungle_Woods.supportsDiagonalWalls = false;
            Jungle_Woods.supportsUpholstery = false;
            Jungle_Woods.carpetIsMainFloor = false;

            TileIndexGrid jungle_carpetGrid1 = ExpandUtility.BuildNewTileIndexGrid("Nakatomi_Carpet_Dark_01");            
            jungle_carpetGrid1.topLeftIndices = new TileIndexList() { indices = new List<int>() { 164 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.topIndices = new TileIndexList() { indices = new List<int>() { 165 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.topRightIndices = new TileIndexList() { indices = new List<int>() { 166 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.leftIndices = new TileIndexList() { indices = new List<int>() { 186 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.centerIndices = new TileIndexList() { indices = new List<int>() { 187 }, indexWeights = new List<float>() { 0.1f } };
            jungle_carpetGrid1.rightIndices = new TileIndexList() { indices = new List<int>() { 188 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.bottomLeftIndices = new TileIndexList() { indices = new List<int>() { 208 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.bottomIndices = new TileIndexList() { indices = new List<int>() { 209 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.bottomRightIndices = new TileIndexList() { indices = new List<int>() { 210 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.topLeftNubIndices = new TileIndexList() { indices = new List<int>() { 212 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.topRightNubIndices = new TileIndexList() { indices = new List<int>() { 211 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.bottomLeftNubIndices = new TileIndexList() { indices = new List<int>() { 190 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid1.bottomRightNubIndices = new TileIndexList() { indices = new List<int>() { 189 }, indexWeights = new List<float>() { 1 } };

            /*TileIndexGrid jungle_carpetGrid2 = ExpandUtility.BuildNewTileIndexGrid("Nakatomi_Carpet_Red_01");
            jungle_carpetGrid2.roomTypeRestriction = -1;
            jungle_carpetGrid2.topLeftIndices = new TileIndexList() { indices = new List<int>() { 230 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.topIndices = new TileIndexList() { indices = new List<int>() { 231 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.topRightIndices = new TileIndexList() { indices = new List<int>() { 232 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.leftIndices = new TileIndexList() { indices = new List<int>() { 252 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.centerIndices = new TileIndexList() { indices = new List<int>() { 253 }, indexWeights = new List<float>() { 0.1f } };
            jungle_carpetGrid2.rightIndices = new TileIndexList() { indices = new List<int>() { 254 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.bottomLeftIndices = new TileIndexList() { indices = new List<int>() { 274 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.bottomIndices = new TileIndexList() { indices = new List<int>() { 275 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.bottomRightIndices = new TileIndexList() { indices = new List<int>() { 276 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.topLeftNubIndices = new TileIndexList() { indices = new List<int>() { 278 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.topRightNubIndices = new TileIndexList() { indices = new List<int>() { 277 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.bottomLeftNubIndices = new TileIndexList() { indices = new List<int>() { 256 }, indexWeights = new List<float>() { 1 } };
            jungle_carpetGrid2.bottomRightNubIndices = new TileIndexList() { indices = new List<int>() { 255 }, indexWeights = new List<float>() { 1 } };*/

            Jungle_Woods.supportsChannels = false;
            Jungle_Woods.minChannelPools = 0;
            Jungle_Woods.maxChannelPools = 3;
            Jungle_Woods.channelTenacity = 0.75f;
            Jungle_Woods.supportsLavaOrLavalikeSquares = false;

            TileIndexGrid jungle_lavaGrid1 = ExpandUtility.BuildNewTileIndexGrid("Nakatomi_Carpet_dark_01");
            jungle_lavaGrid1.topLeftIndices = new TileIndexList() { indices = new List<int>() { 318 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.topIndices = new TileIndexList() { indices = new List<int>() { 406 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.topRightIndices = new TileIndexList() { indices = new List<int>() { 340 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.leftIndices = new TileIndexList() { indices = new List<int>() { 428 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.centerIndices = new TileIndexList() { indices = new List<int>() { 296 }, indexWeights = new List<float>() { 0.1f } };
            jungle_lavaGrid1.rightIndices = new TileIndexList() { indices = new List<int>() { 450 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.bottomLeftIndices = new TileIndexList() { indices = new List<int>() { 362 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.bottomIndices = new TileIndexList() { indices = new List<int>() { 472 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.bottomRightIndices = new TileIndexList() { indices = new List<int>() { 384 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.horizontalIndices = new TileIndexList() { indices = new List<int>() { 604 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.verticalIndices = new TileIndexList() { indices = new List<int>() { 582 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.topCapIndices = new TileIndexList() { indices = new List<int>() { 494 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.rightCapIndices = new TileIndexList() { indices = new List<int>() { 538 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.bottomCapIndices = new TileIndexList() { indices = new List<int>() { 560 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.leftCapIndices = new TileIndexList() { indices = new List<int>() { 516 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.allSidesIndices = new TileIndexList() { indices = new List<int>() { 626 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.topLeftNubIndices = new TileIndexList() { indices = new List<int>() { 520 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.topRightNubIndices = new TileIndexList() { indices = new List<int>() { 519 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.bottomLeftNubIndices = new TileIndexList() { indices = new List<int>() { 498 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.bottomRightNubIndices = new TileIndexList() { indices = new List<int>() { 497 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderTopNubLeftIndices = new TileIndexList() { indices = new List<int>() { 408 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderTopNubRightIndices = new TileIndexList() { indices = new List<int>() { 407 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderTopNubBothIndices = new TileIndexList() { indices = new List<int>() { 409 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderRightNubTopIndices = new TileIndexList() { indices = new List<int>() { 452 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderRightNubBottomIndices = new TileIndexList() { indices = new List<int>() { 451 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderRightNubBothIndices = new TileIndexList() { indices = new List<int>() { 453 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderBottomNubLeftIndices = new TileIndexList() { indices = new List<int>() { 473 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderBottomNubRightIndices = new TileIndexList() { indices = new List<int>() { 474 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderBottomNubBothIndices = new TileIndexList() { indices = new List<int>() { 475 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderLeftNubTopIndices = new TileIndexList() { indices = new List<int>() { 429 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderLeftNubBottomIndices = new TileIndexList() { indices = new List<int>() { 430 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.borderLeftNubBothIndices = new TileIndexList() { indices = new List<int>() { 431 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.diagonalNubsTopLeftBottomRight = new TileIndexList() { indices = new List<int>() { 517 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.diagonalNubsTopRightBottomLeft = new TileIndexList() { indices = new List<int>() { 495 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.doubleNubsTop = new TileIndexList() { indices = new List<int>() { 540 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.doubleNubsRight = new TileIndexList() { indices = new List<int>() { 541 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.doubleNubsBottom = new TileIndexList() { indices = new List<int>() { 539 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.doubleNubsLeft = new TileIndexList() { indices = new List<int>() { 542 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.quadNubs = new TileIndexList() { indices = new List<int>() { 518 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.topRightWithNub = new TileIndexList() { indices = new List<int>() { 341 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.topLeftWithNub = new TileIndexList() { indices = new List<int>() { 319 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.bottomRightWithNub = new TileIndexList() { indices = new List<int>() { 385 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.bottomLeftWithNub = new TileIndexList() { indices = new List<int>() { 363 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.diagonalBorderNE = new TileIndexList() { indices = new List<int>() { 342 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.diagonalBorderSE = new TileIndexList() { indices = new List<int>() { 320 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.diagonalBorderSW = new TileIndexList() { indices = new List<int>() { 342 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.diagonalBorderNW = new TileIndexList() { indices = new List<int>() { 320 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.diagonalCeilingNE = new TileIndexList() { indices = new List<int>() { 388 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.diagonalCeilingSE = new TileIndexList() { indices = new List<int>() { 366 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.diagonalCeilingSW = new TileIndexList() { indices = new List<int>() { 367 }, indexWeights = new List<float>() { 1 } };
            jungle_lavaGrid1.diagonalCeilingNW = new TileIndexList() { indices = new List<int>() { 389 }, indexWeights = new List<float>() { 1 } };

            Jungle_Woods.carpetGrids = new TileIndexGrid[] { jungle_carpetGrid1/*, jungle_carpetGrid2*/ };

            Jungle_Woods.lavaGrids = new TileIndexGrid[] { jungle_lavaGrid1 };
            Jungle_Woods.supportsIceSquares = false;
            Jungle_Woods.iceGrids = new TileIndexGrid[0];
            Jungle_Woods.roomFloorBorderGrid = null;

            TileIndexGrid jungle_wood_roomCeilingBorderGrid = ExpandUtility.BuildNewTileIndexGrid("Jungle_CeilingBorder_Inner_01");
            jungle_wood_roomCeilingBorderGrid.topLeftIndices = new TileIndexList() { indices = new List<int>() { 334 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.topIndices = new TileIndexList() { indices = new List<int>() { 335 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.topRightIndices = new TileIndexList() { indices = new List<int>() { 336 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.leftIndices = new TileIndexList() { indices = new List<int>() { 356 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.centerIndices = new TileIndexList() { indices = new List<int>() { 357 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.rightIndices = new TileIndexList() { indices = new List<int>() { 358 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.bottomLeftIndices = new TileIndexList() { indices = new List<int>() { 378 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.bottomIndices = new TileIndexList() { indices = new List<int>() { 379 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.bottomRightIndices = new TileIndexList() { indices = new List<int>() { 380 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.horizontalIndices = new TileIndexList() { indices = new List<int>() { 423 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.verticalIndices = new TileIndexList() { indices = new List<int>() { 424 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.topCapIndices = new TileIndexList() { indices = new List<int>() { 357 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.rightCapIndices = new TileIndexList() { indices = new List<int>() { 357 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.bottomCapIndices = new TileIndexList() { indices = new List<int>() { 357 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.leftCapIndices = new TileIndexList() { indices = new List<int>() { 357 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.allSidesIndices = new TileIndexList() { indices = new List<int>() { 357 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.topLeftNubIndices = new TileIndexList() { indices = new List<int>() { 357 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.topRightNubIndices = new TileIndexList() { indices = new List<int>() { 357 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.bottomLeftNubIndices = new TileIndexList() { indices = new List<int>() { 357 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_roomCeilingBorderGrid.bottomRightNubIndices = new TileIndexList() { indices = new List<int>() { 357 }, indexWeights = new List<float>() { 1 } };
            
            Jungle_Woods.roomCeilingBorderGrid = jungle_wood_roomCeilingBorderGrid;

            TileIndexGrid jungle_wood_pitLayoutGrid = ExpandUtility.BuildNewTileIndexGrid("Belly_PitLayout_01");
            jungle_wood_pitLayoutGrid.topLeftIndices = new TileIndexList() { indices = new List<int>() { 858 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_pitLayoutGrid.topIndices = new TileIndexList() { indices = new List<int>() { 858 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_pitLayoutGrid.topRightIndices = new TileIndexList() { indices = new List<int>() { 858 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_pitLayoutGrid.centerIndices = new TileIndexList() {
                indices = new List<int>() { 748, 770, 792, 814 },
                indexWeights = new List<float>() { 1, 1, 1, 1 }
            };
            jungle_wood_pitLayoutGrid.horizontalIndices = new TileIndexList() { indices = new List<int>() { 858 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_pitLayoutGrid.topCapIndices = new TileIndexList() { indices = new List<int>() { 858 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_pitLayoutGrid.rightCapIndices = new TileIndexList() { indices = new List<int>() { 858 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_pitLayoutGrid.leftCapIndices = new TileIndexList() { indices = new List<int>() { 858 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_pitLayoutGrid.allSidesIndices = new TileIndexList() { indices = new List<int>() { 858 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_pitLayoutGrid.extendedSet = true;
            jungle_wood_pitLayoutGrid.PitInternalSquareOptions = new PitSquarePlacementOptions() {
                PitSquareChance = 0.1f,
                CanBeFlushLeft = true,
                CanBeFlushRight = true,
                CanBeFlushBottom = true
            };

            Jungle_Woods.pitLayoutGrid = jungle_wood_pitLayoutGrid;
            Jungle_Woods.pitBorderRaisedGrid = null;
            Jungle_Woods.additionalPitBorderFlatGrid = null;

            TileIndexGrid jungle_wood_outerCeilingBorderGrid = ExpandUtility.BuildNewTileIndexGrid("Jungle_CeilingBorder_Outer_01");
            jungle_wood_outerCeilingBorderGrid.topLeftIndices = new TileIndexList() { indices = new List<int>() { 268 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_outerCeilingBorderGrid.topIndices = new TileIndexList() {
                indices = new List<int>() { 400, 401, 402 },
                indexWeights = new List<float>() { 1, 1, 1 } };
            jungle_wood_outerCeilingBorderGrid.topRightIndices = new TileIndexList() { indices = new List<int>() { 269 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_outerCeilingBorderGrid.leftIndices = new TileIndexList() {
                indices = new List<int>() { 337, 359, 381 },
                indexWeights = new List<float>() { 1, 1, 1 } };
            jungle_wood_outerCeilingBorderGrid.centerIndices = new TileIndexList() { indices = new List<int>() { -1 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_outerCeilingBorderGrid.rightIndices = new TileIndexList() {
                indices = new List<int>() { 333, 355, 377 },
                indexWeights = new List<float>() { 1, 1, 1 }
            };
            jungle_wood_outerCeilingBorderGrid.bottomLeftIndices = new TileIndexList() { indices = new List<int>() { 290 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_outerCeilingBorderGrid.bottomIndices = new TileIndexList() {
                indices = new List<int>() { 312, 313, 314 },
                indexWeights = new List<float>() { 1, 1, 1 }
            };
            jungle_wood_outerCeilingBorderGrid.bottomRightIndices = new TileIndexList() { indices = new List<int>() { 291 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_outerCeilingBorderGrid.horizontalIndices = new TileIndexList() { indices = new List<int>() { 296 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_outerCeilingBorderGrid.verticalIndices = new TileIndexList() { indices = new List<int>() { 295 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_outerCeilingBorderGrid.topCapIndices = new TileIndexList() { indices = new List<int>() { 294 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_outerCeilingBorderGrid.rightCapIndices = new TileIndexList() { indices = new List<int>() { 318 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_outerCeilingBorderGrid.bottomCapIndices = new TileIndexList() { indices = new List<int>() { 316 }, indexWeights = new List<float>() { 1 } };
            jungle_wood_outerCeilingBorderGrid.leftCapIndices = new TileIndexList() { indices = new List<int>() { 317 }, indexWeights = new List<float>() { 1 } };
            

            Jungle_Woods.pitBorderFlatGrid = jungle_wood_outerCeilingBorderGrid;
            Jungle_Woods.outerCeilingBorderGrid = jungle_wood_outerCeilingBorderGrid;
            Jungle_Woods.floorSquareDensity = 0.05f;
            Jungle_Woods.floorSquares = new TileIndexGrid[0];
            Jungle_Woods.usesFacewallGrids = false;


            TileIndexGrid Jungle_FaceWallIndexGrid_01 = ExpandUtility.BuildNewTileIndexGrid("Nakatomi_Blue_WallLayout_01_DoubleWindows");
            TileIndexGrid Jungle_FaceWallIndexGrid_02 = ExpandUtility.BuildNewTileIndexGrid("Nakatomi_Blue_WallLayout_02_SingleWindow");
            TileIndexGrid Jungle_FaceWallIndexGrid_03 = ExpandUtility.BuildNewTileIndexGrid("Nakatomi_Blue_WallLayout_03_BigSingleWindow");

            Jungle_FaceWallIndexGrid_01.topLeftIndices = new TileIndexList() { indices = new List<int>() { 32 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_01.topIndices = new TileIndexList() { indices = new List<int>() { 33 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_01.topRightIndices = new TileIndexList() { indices = new List<int>() { 31 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_01.bottomLeftIndices = new TileIndexList() { indices = new List<int>() { 54 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_01.bottomIndices = new TileIndexList() { indices = new List<int>() { 55 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_01.bottomRightIndices = new TileIndexList() { indices = new List<int>() { 53 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_02.topLeftIndices = new TileIndexList() { indices = new List<int>() { 32 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_02.topIndices = new TileIndexList() { indices = new List<int>() { 34 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_02.topRightIndices = new TileIndexList() { indices = new List<int>() { 31 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_02.bottomLeftIndices = new TileIndexList() { indices = new List<int>() { 54 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_02.bottomIndices = new TileIndexList() { indices = new List<int>() { 56 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_02.bottomRightIndices = new TileIndexList() { indices = new List<int>() { 53 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_03.topLeftIndices = new TileIndexList() { indices = new List<int>() { 32 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_03.topIndices = new TileIndexList() { indices = new List<int>() { 37 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_03.topRightIndices = new TileIndexList() { indices = new List<int>() { 31 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_03.bottomLeftIndices = new TileIndexList() { indices = new List<int>() { 54 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_03.bottomIndices = new TileIndexList() { indices = new List<int>() { 59 }, indexWeights = new List<float>() { 1 } };
            Jungle_FaceWallIndexGrid_03.bottomRightIndices = new TileIndexList() { indices = new List<int>() { 53 }, indexWeights = new List<float>() { 1 } };
            

            Jungle_Woods.facewallGrids = new FacewallIndexGridDefinition[] {
                new FacewallIndexGridDefinition() {
                    grid = Jungle_FaceWallIndexGrid_01,
                    minWidth = 3,
                    maxWidth = 8,
                    hasIntermediaries = false,
                    minIntermediaryBuffer = 4,
                    maxIntermediaryBuffer = 6,
                    minIntermediaryLength = 1,
                    maxIntermediaryLength = 3,
                    topsMatchBottoms = true,
                    middleSectionSequential = false,
                    canExistInCorners = true,
                    forceEdgesInCorners = true,
                    canAcceptWallDecoration = false,
                    canAcceptFloorDecoration = false,
                    forcedStampMatchingStyle = DungeonTileStampData.IntermediaryMatchingStyle.ANY,
                    canBePlacedInExits = false,
                    chanceToPlaceIfPossible = 0.15f,
                    perTileFailureRate = 0.05f
                },
                new FacewallIndexGridDefinition() {
                    grid = Jungle_FaceWallIndexGrid_02,
                    minWidth = 3,
                    maxWidth = 8,
                    hasIntermediaries = false,
                    minIntermediaryBuffer = 4,
                    maxIntermediaryBuffer = 6,
                    minIntermediaryLength = 1,
                    maxIntermediaryLength = 3,
                    topsMatchBottoms = true,
                    middleSectionSequential = false,
                    canExistInCorners = true,
                    forceEdgesInCorners = true,
                    canAcceptWallDecoration = false,
                    canAcceptFloorDecoration = false,
                    forcedStampMatchingStyle = DungeonTileStampData.IntermediaryMatchingStyle.ANY,
                    canBePlacedInExits = false,
                    chanceToPlaceIfPossible = 0.15f,
                    perTileFailureRate = 0.05f
                },
                new FacewallIndexGridDefinition() {
                    grid = Jungle_FaceWallIndexGrid_03,
                    minWidth = 3,
                    maxWidth = 8,
                    hasIntermediaries = false,
                    minIntermediaryBuffer = 4,
                    maxIntermediaryBuffer = 6,
                    minIntermediaryLength = 1,
                    maxIntermediaryLength = 3,
                    topsMatchBottoms = true,
                    middleSectionSequential = false,
                    canExistInCorners = true,
                    forceEdgesInCorners = true,
                    canAcceptWallDecoration = false,
                    canAcceptFloorDecoration = false,
                    forcedStampMatchingStyle = DungeonTileStampData.IntermediaryMatchingStyle.ANY,
                    canBePlacedInExits = false,
                    chanceToPlaceIfPossible = 0.15f,
                    perTileFailureRate = 0.05f
                },
            };
            Jungle_Woods.usesInternalMaterialTransitions = false;
            Jungle_Woods.usesProceduralMaterialTransitions = false;
            Jungle_Woods.internalMaterialTransitions = new RoomInternalMaterialTransition[0];
            Jungle_Woods.secretRoomWallShardCollections = new List<GameObject>(0);
            Jungle_Woods.overrideStoneFloorType = false;
            Jungle_Woods.overrideFloorType = CellVisualData.CellFloorType.Stone;
            Jungle_Woods.useLighting = true;
            Jungle_Woods.lightPrefabs = new WeightedGameObjectCollection() {
               elements = new List<WeightedGameObject>() {
                   new WeightedGameObject() {
                       rawGameObject = GungeonPrefab.roomMaterialDefinitions[0].lightPrefabs.elements[0].rawGameObject,
                       weight = 1,                       
                       forceDuplicatesPossible = false,
                       pickupId = -1,
                       additionalPrerequisites = new DungeonPrerequisite[0]                       
                   }
               }
            };
            Jungle_Woods.facewallLightStamps = GungeonPrefab.roomMaterialDefinitions[0].facewallLightStamps;
            Jungle_Woods.sidewallLightStamps = GungeonPrefab.roomMaterialDefinitions[0].sidewallLightStamps;
            Jungle_Woods.usesDecalLayer = false;
            Jungle_Woods.decalIndexGrid = null;
            Jungle_Woods.decalLayerStyle = TilemapDecoSettings.DecoStyle.GROW_FROM_WALLS;
            Jungle_Woods.decalSize = 1;
            Jungle_Woods.decalSpacing = 1;
            Jungle_Woods.forceEdgesDiagonal = false;
            Jungle_Woods.exteriorFacadeBorderGrid = null;
            Jungle_Woods.facadeTopGrid = null;
            Jungle_Woods.bridgeGrid = null;


            DungeonMaterial Jungle_Bamboo = ScriptableObject.CreateInstance<DungeonMaterial>();
            Jungle_Bamboo.wallShards = GungeonPrefab.roomMaterialDefinitions[0].wallShards;
            Jungle_Bamboo.bigWallShards = GungeonPrefab.roomMaterialDefinitions[0].bigWallShards;
            Jungle_Bamboo.bigWallShardDamageThreshold = 10;
            Jungle_Bamboo.fallbackVerticalTileMapEffects = GungeonPrefab.roomMaterialDefinitions[0].fallbackVerticalTileMapEffects;
            Jungle_Bamboo.fallbackHorizontalTileMapEffects = GungeonPrefab.roomMaterialDefinitions[0].fallbackHorizontalTileMapEffects;
            Jungle_Bamboo.pitfallVFXPrefab = null;
            Jungle_Bamboo.UsePitAmbientVFX = false;
            Jungle_Bamboo.AmbientPitVFX = new List<GameObject>(0);
            Jungle_Bamboo.PitVFXMinCooldown = 5;
            Jungle_Bamboo.PitVFXMaxCooldown = 30;
            Jungle_Bamboo.ChanceToSpawnPitVFXOnCooldown = 1;
            Jungle_Bamboo.stampFailChance = 0.2f;
            Jungle_Bamboo.overrideTableTable = null;
            Jungle_Bamboo.supportsPits = false;
            Jungle_Bamboo.doPitAO = true;
            Jungle_Bamboo.pitsAreOneDeep = false;
            Jungle_Bamboo.supportsDiagonalWalls = false;
            Jungle_Bamboo.supportsUpholstery = false;
            Jungle_Bamboo.carpetIsMainFloor = false;
            Jungle_Bamboo.carpetGrids = new TileIndexGrid[] { jungle_carpetGrid1/*, jungle_carpetGrid2*/ };
            Jungle_Bamboo.supportsChannels = false;
            Jungle_Bamboo.minChannelPools = 0;
            Jungle_Bamboo.maxChannelPools = 3;
            Jungle_Bamboo.channelTenacity = 0.75f;
            Jungle_Bamboo.supportsLavaOrLavalikeSquares = true;
            Jungle_Bamboo.lavaGrids = new TileIndexGrid[] { jungle_lavaGrid1 };
            Jungle_Bamboo.supportsIceSquares = false;
            Jungle_Bamboo.iceGrids = new TileIndexGrid[0];
            Jungle_Bamboo.roomFloorBorderGrid = null;
            Jungle_Bamboo.roomCeilingBorderGrid = jungle_wood_roomCeilingBorderGrid;                        
            Jungle_Bamboo.pitLayoutGrid = jungle_wood_pitLayoutGrid;

            TileIndexGrid jungle_bamboo_pitBorderFlatGrid = ExpandUtility.BuildNewTileIndexGrid("Jungle_Custom_PitBorder_01");
            jungle_bamboo_pitBorderFlatGrid.topLeftIndices = new TileIndexList() { indices = new List<int>() { 208 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.topIndices = new TileIndexList() { indices = new List<int>() { 187 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.topRightIndices = new TileIndexList() { indices = new List<int>() { 208 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.leftIndices = new TileIndexList() { indices = new List<int>() { 186 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.rightIndices = new TileIndexList() { indices = new List<int>() { 188 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.bottomLeftIndices = new TileIndexList() { indices = new List<int>() { 208 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.bottomIndices = new TileIndexList() { indices = new List<int>() { 209 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.bottomRightIndices = new TileIndexList() { indices = new List<int>() { 210 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.topLeftNubIndices = new TileIndexList() { indices = new List<int>() { 188 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.topRightNubIndices = new TileIndexList() { indices = new List<int>() { 188 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.bottomLeftNubIndices = new TileIndexList() { indices = new List<int>() { 190 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.bottomRightNubIndices = new TileIndexList() { indices = new List<int>() { 189 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.diagonalNubsTopLeftBottomRight = new TileIndexList() { indices = new List<int>() { 210 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.diagonalNubsTopRightBottomLeft = new TileIndexList() { indices = new List<int>() { 208 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.horizontalIndices = new TileIndexList() { indices = new List<int>() { 188 }, indexWeights = new List<float>() { 1 } };
            jungle_bamboo_pitBorderFlatGrid.verticalIndices = new TileIndexList() { indices = new List<int>() { 188 }, indexWeights = new List<float>() { 1 } };
            
            Jungle_Bamboo.pitBorderFlatGrid = jungle_wood_outerCeilingBorderGrid;
            Jungle_Bamboo.pitBorderRaisedGrid = null;
            Jungle_Bamboo.additionalPitBorderFlatGrid = null;
            Jungle_Bamboo.outerCeilingBorderGrid = jungle_wood_outerCeilingBorderGrid;
            Jungle_Bamboo.floorSquareDensity = 0.05f;
            Jungle_Bamboo.floorSquares = new TileIndexGrid[0];
            Jungle_Bamboo.usesFacewallGrids = false;
            Jungle_Bamboo.facewallGrids = new FacewallIndexGridDefinition[] {
                new FacewallIndexGridDefinition() {
                    grid = Jungle_FaceWallIndexGrid_01,
                    minWidth = 3,
                    maxWidth = 8,
                    hasIntermediaries = false,
                    minIntermediaryBuffer = 4,
                    maxIntermediaryBuffer = 6,
                    minIntermediaryLength = 1,
                    maxIntermediaryLength = 3,
                    topsMatchBottoms = true,
                    middleSectionSequential = false,
                    canExistInCorners = true,
                    forceEdgesInCorners = true,
                    canAcceptWallDecoration = false,
                    canAcceptFloorDecoration = false,
                    forcedStampMatchingStyle = DungeonTileStampData.IntermediaryMatchingStyle.ANY,
                    canBePlacedInExits = false,
                    chanceToPlaceIfPossible = 0.15f,
                    perTileFailureRate = 0.05f
                },
                new FacewallIndexGridDefinition() {
                    grid = Jungle_FaceWallIndexGrid_02,
                    minWidth = 3,
                    maxWidth = 8,
                    hasIntermediaries = false,
                    minIntermediaryBuffer = 4,
                    maxIntermediaryBuffer = 6,
                    minIntermediaryLength = 1,
                    maxIntermediaryLength = 3,
                    topsMatchBottoms = true,
                    middleSectionSequential = false,
                    canExistInCorners = true,
                    forceEdgesInCorners = true,
                    canAcceptWallDecoration = false,
                    canAcceptFloorDecoration = false,
                    forcedStampMatchingStyle = DungeonTileStampData.IntermediaryMatchingStyle.ANY,
                    canBePlacedInExits = false,
                    chanceToPlaceIfPossible = 0.15f,
                    perTileFailureRate = 0.05f
                },
                new FacewallIndexGridDefinition() {
                    grid = Jungle_FaceWallIndexGrid_03,
                    minWidth = 3,
                    maxWidth = 8,
                    hasIntermediaries = false,
                    minIntermediaryBuffer = 4,
                    maxIntermediaryBuffer = 6,
                    minIntermediaryLength = 1,
                    maxIntermediaryLength = 3,
                    topsMatchBottoms = true,
                    middleSectionSequential = false,
                    canExistInCorners = true,
                    forceEdgesInCorners = true,
                    canAcceptWallDecoration = false,
                    canAcceptFloorDecoration = false,
                    forcedStampMatchingStyle = DungeonTileStampData.IntermediaryMatchingStyle.ANY,
                    canBePlacedInExits = false,
                    chanceToPlaceIfPossible = 0.15f,
                    perTileFailureRate = 0.05f
                },
            };
            Jungle_Bamboo.usesInternalMaterialTransitions = false;
            Jungle_Bamboo.usesProceduralMaterialTransitions = false;
            Jungle_Bamboo.internalMaterialTransitions = new RoomInternalMaterialTransition[0];
            Jungle_Bamboo.secretRoomWallShardCollections = new List<GameObject>(0);
            Jungle_Bamboo.overrideStoneFloorType = false;
            Jungle_Bamboo.overrideFloorType = CellVisualData.CellFloorType.Stone;
            Jungle_Bamboo.useLighting = true;
            Jungle_Bamboo.lightPrefabs = new WeightedGameObjectCollection() {
               elements = new List<WeightedGameObject>() {
                   new WeightedGameObject() {
                       rawGameObject = GungeonPrefab.roomMaterialDefinitions[0].lightPrefabs.elements[0].rawGameObject,
                       weight = 1,                       
                       forceDuplicatesPossible = false,
                       pickupId = -1,
                       additionalPrerequisites = new DungeonPrerequisite[0]                       
                   }
               }
            };
            Jungle_Bamboo.facewallLightStamps = GungeonPrefab.roomMaterialDefinitions[0].facewallLightStamps;
            Jungle_Bamboo.sidewallLightStamps = GungeonPrefab.roomMaterialDefinitions[0].sidewallLightStamps;
            Jungle_Bamboo.usesDecalLayer = false;
            Jungle_Bamboo.decalIndexGrid = null;
            Jungle_Bamboo.decalLayerStyle = TilemapDecoSettings.DecoStyle.GROW_FROM_WALLS;
            Jungle_Bamboo.decalSize = 1;
            Jungle_Bamboo.decalSpacing = 1;
            Jungle_Bamboo.forceEdgesDiagonal = false;
            Jungle_Bamboo.exteriorFacadeBorderGrid = null;
            Jungle_Bamboo.facadeTopGrid = null;
            Jungle_Bamboo.bridgeGrid = null;



                        
            DungeonTileStampData m_JungleStampData = ScriptableObject.CreateInstance<DungeonTileStampData>();
            m_JungleStampData.name = "ENV_JUNGLE_STAMP_DATA";
            m_JungleStampData.tileStampWeight = 1;
            m_JungleStampData.spriteStampWeight = 0;
            m_JungleStampData.objectStampWeight = 1.5f;
            // m_JungleStampData.objectStampWeight = 1;
            m_JungleStampData.stamps = new TileStampData[0];
            m_JungleStampData.spriteStamps = new SpriteStampData[0];
            m_JungleStampData.objectStamps = GungeonPrefab.stampData.objectStamps;
            m_JungleStampData.SymmetricFrameChance = 0.5f;
            m_JungleStampData.SymmetricCompleteChance = 0.25f;
            // m_JungleStampData.SymmetricFrameChance = 0.1f;
            // m_JungleStampData.SymmetricCompleteChance = 0.1f;

            dungeon.gameObject.name = "Base_Jungle";
            dungeon.contentSource = ContentSource.CONTENT_UPDATE_03;
            dungeon.DungeonSeed = 0;
            dungeon.DungeonFloorName = "Deep in the Jungle";
            dungeon.DungeonShortName = "Deep in the Jungle";
            dungeon.DungeonFloorLevelTextOverride = "An Ancient Place...";
            dungeon.LevelOverrideType = GameManager.LevelOverrideState.NONE;
            dungeon.debugSettings = new DebugDungeonSettings() {
                RAPID_DEBUG_DUNGEON_ITERATION_SEEKER = false,
                RAPID_DEBUG_DUNGEON_ITERATION = false,
                RAPID_DEBUG_DUNGEON_COUNT = 50,
                GENERATION_VIEWER_MODE = false,
                FULL_MINIMAP_VISIBILITY = false,
                COOP_TEST = false,
                DISABLE_ENEMIES = false,
                DISABLE_LOOPS = false,
                DISABLE_SECRET_ROOM_COVERS = false,
                DISABLE_OUTLINES = false,
                WALLS_ARE_PITS = false
            };
            dungeon.PatternSettings = new SemioticDungeonGenSettings() {
                flows = new List<DungeonFlow>() { f1b_jungle_flow_01.F1b_Jungle_Flow_01(), f1b_jungle_flow_02.F1b_Jungle_Flow_02() },
                mandatoryExtraRooms = new List<ExtraIncludedRoomData>(0),
                optionalExtraRooms = new List<ExtraIncludedRoomData>(0),
                MAX_GENERATION_ATTEMPTS = 250,
                DEBUG_RENDER_CANVASES_SEPARATELY = false
            };

            

            dungeon.ForceRegenerationOfCharacters = false;
            dungeon.ActuallyGenerateTilemap = true;
            dungeon.decoSettings = new TilemapDecoSettings {
                standardRoomVisualSubtypes = new WeightedIntCollection {
                    elements = new WeightedInt[] {
                        new WeightedInt() {
                            annotation = "woods",
                            value = 0,
                            weight = 0.5f,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "the boo",
                            value = 1,
                            weight = 0.5f,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "shop",
                            value = 2,
                            weight = 0,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "unused",
                            value = 3,
                            weight = 0,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "unused",
                            value = 4,
                            weight = 0,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "unused",
                            value = 5,
                            weight = 0,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "unused",
                            value = 6,
                            weight = 0,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        },
                        new WeightedInt() {
                            annotation = "unused",
                            value = 7,
                            weight = 0,
                            additionalPrerequisites = new DungeonPrerequisite[0]
                        }
                    }
                },
                decalLayerStyle = TilemapDecoSettings.DecoStyle.NONE,
                decalSize = 3,
                decalSpacing = 1,
                decalExpansion = 0,
                patternLayerStyle = TilemapDecoSettings.DecoStyle.NONE,
                patternSize = 3,
                patternSpacing = 3,
                patternExpansion = 0,
                decoPatchFrequency = 0.01f,
                ambientLightColor = new Color(0.727336f, 0.766108f, 0.785294f, 1),
                ambientLightColorTwo = new Color(0.62549f, 0.664706f, 0.684314f, 1),
                lowQualityAmbientLightColor = new Color(1, 1, 1, 1),
                lowQualityAmbientLightColorTwo = new Color(1, 1, 1, 1),
                lowQualityCheapLightVector = new Vector4(1, 0, -1, 0),
                UsesAlienFXFloorColor = false,
                AlienFXFloorColor = new Color(0, 0, 0, 1),
                generateLights = true,
                lightCullingPercentage = 0.2f,
                lightOverlapRadius = 8,
                nearestAllowedLight = 12,
                minLightExpanseWidth = 2,
                lightHeight = -2,
                lightCookies = new Texture2D[0],
                debug_view = false
            };
            
            dungeon.tileIndices = new TileIndices() {
                tilesetId = GlobalDungeonData.ValidTilesets.JUNGLEGEON,
                dungeonCollection = braveResources.LoadAsset<GameObject>("TallGrassStrip").GetComponent<tk2dTiledSprite>().Collection,
                dungeonCollectionSupportsDiagonalWalls = false,
                aoTileIndices = new AOTileIndices() {
                    AOFloorTileIndex = 0,
                    AOBottomWallBaseTileIndex = 1,
                    AOBottomWallTileRightIndex = 2,
                    AOBottomWallTileLeftIndex = 3,
                    AOBottomWallTileBothIndex = 4,
                    AOTopFacewallRightIndex = 6,
                    AOTopFacewallLeftIndex = 5,
                    AOTopFacewallBothIndex = 7,
                    AOFloorWallLeft = 5,
                    AOFloorWallRight = 6,
                    AOFloorWallBoth = 7,
                    AOFloorPizzaSliceLeft = 8,
                    AOFloorPizzaSliceRight = 9,
                    AOFloorPizzaSliceBoth = 10,
                    AOFloorPizzaSliceLeftWallRight = 11,
                    AOFloorPizzaSliceRightWallLeft = 12,
                    AOFloorWallUpAndLeft = 13,
                    AOFloorWallUpAndRight = 14,
                    AOFloorWallUpAndBoth = 15,
                    AOFloorDiagonalWallNortheast = -1,
                    AOFloorDiagonalWallNortheastLower = -1,
                    AOFloorDiagonalWallNortheastLowerJoint = -1,
                    AOFloorDiagonalWallNorthwest = -1,
                    AOFloorDiagonalWallNorthwestLower = -1,
                    AOFloorDiagonalWallNorthwestLowerJoint = -1,
                    AOBottomWallDiagonalNortheast = -1,
                    AOBottomWallDiagonalNorthwest = -1
                },
                placeBorders = true,
                placePits = false,
                chestHighWallIndices = new List<TileIndexVariant>() {
                    new TileIndexVariant() {
                        index = 41,
                        likelihood = 0.5f,
                        overrideLayerIndex = 0,
                        overrideIndex = 0
                    }
                },
                decalIndexGrid = null,
                patternIndexGrid = null,
                globalSecondBorderTiles = new List<int>(0),
                edgeDecorationTiles = null
            };

            dungeon.roomMaterialDefinitions = new DungeonMaterial[] {
                Jungle_Woods,
                Jungle_Bamboo,
                Jungle_Woods,
                Jungle_Woods,
                Jungle_Woods,
                Jungle_Woods,
                Jungle_Woods,
                Jungle_Woods
            };
            dungeon.dungeonWingDefinitions = new DungeonWingDefinition[0];
            dungeon.pathGridDefinitions = new List<TileIndexGrid>() { MinesDungeonPrefab.pathGridDefinitions[0] };
            dungeon.dungeonDustups = new DustUpVFX() {
                runDustup = GungeonPrefab.dungeonDustups.runDustup,
                waterDustup = GungeonPrefab.dungeonDustups.waterDustup,
                additionalWaterDustup = GungeonPrefab.dungeonDustups.additionalWaterDustup,
                rollNorthDustup = GungeonPrefab.dungeonDustups.rollNorthDustup,
                rollNorthEastDustup = GungeonPrefab.dungeonDustups.rollNorthEastDustup,
                rollEastDustup = GungeonPrefab.dungeonDustups.rollEastDustup,
                rollSouthEastDustup = GungeonPrefab.dungeonDustups.rollSouthEastDustup,
                rollSouthDustup = GungeonPrefab.dungeonDustups.rollSouthDustup,
                rollSouthWestDustup = GungeonPrefab.dungeonDustups.rollSouthWestDustup,
                rollWestDustup = GungeonPrefab.dungeonDustups.rollWestDustup,
                rollNorthWestDustup = GungeonPrefab.dungeonDustups.rollNorthWestDustup,
                rollLandDustup = GungeonPrefab.dungeonDustups.rollLandDustup
            };
            dungeon.damageTypeEffectMatrix = GungeonPrefab.damageTypeEffectMatrix;
            dungeon.stampData = m_JungleStampData;
            dungeon.UsesCustomFloorIdea = false;
            dungeon.FloorIdea = new RobotDaveIdea() {
                ValidEasyEnemyPlaceables = new DungeonPlaceable[0],
                ValidHardEnemyPlaceables = new DungeonPlaceable[0],
                UseWallSawblades = false,
                UseRollingLogsVertical = false,
                UseRollingLogsHorizontal = false,
                UseFloorPitTraps = false,
                UseFloorFlameTraps = false,
                UseFloorSpikeTraps = false,
                UseFloorConveyorBelts = false,
                UseCaveIns = false,
                UseAlarmMushrooms = false,
                UseChandeliers = false,
                UseMineCarts = false,
                CanIncludePits = true
            };
            dungeon.PlaceDoors = true;
            dungeon.doorObjects = ExpandPrefabs.Jungle_Doors;
            dungeon.oneWayDoorObjects = GungeonPrefab.oneWayDoorObjects;
            dungeon.oneWayDoorPressurePlate = GungeonPrefab.oneWayDoorPressurePlate;
            dungeon.phantomBlockerDoorObjects = GungeonPrefab.phantomBlockerDoorObjects;
            dungeon.UsesWallWarpWingDoors = false;
            dungeon.baseChestContents = GungeonPrefab.baseChestContents;
            dungeon.SecretRoomSimpleTriggersFacewall = new List<GameObject>() { SewersPrefab.SecretRoomSimpleTriggersFacewall[0] };
            dungeon.SecretRoomSimpleTriggersSidewall = new List<GameObject>() { SewersPrefab.SecretRoomSimpleTriggersSidewall[0] };
            dungeon.SecretRoomComplexTriggers = new List<ComplexSecretRoomTrigger>(0);
            dungeon.SecretRoomDoorSparkVFX = SewersPrefab.SecretRoomDoorSparkVFX;
            dungeon.SecretRoomHorizontalPoofVFX = SewersPrefab.SecretRoomHorizontalPoofVFX;
            dungeon.SecretRoomVerticalPoofVFX = SewersPrefab.SecretRoomVerticalPoofVFX;
            dungeon.sharedSettingsPrefab = GungeonPrefab.sharedSettingsPrefab;
            dungeon.NormalRatGUID = string.Empty;
            dungeon.BossMasteryTokenItemId = 468;
            dungeon.UsesOverrideTertiaryBossSets = false;
            dungeon.OverrideTertiaryRewardSets = new List<TertiaryBossRewardSet>(0);
            dungeon.defaultPlayerPrefab = GungeonPrefab.defaultPlayerPrefab;
            dungeon.StripPlayerOnArrival = false;
            dungeon.SuppressEmergencyCrates = false;
            dungeon.SetTutorialFlag = false;
            dungeon.PlayerIsLight = false;
            dungeon.PlayerLightColor = new Color(1, 1, 1, 1);
            dungeon.PlayerLightIntensity = 3;
            dungeon.PlayerLightRadius = 5;
            dungeon.PrefabsToAutoSpawn = new GameObject[0];
            dungeon.musicEventName = string.Empty;
            
            braveResources = null;
            MinesDungeonPrefab = null;
            GungeonPrefab = null;

            return dungeon;
        }

    }
}

