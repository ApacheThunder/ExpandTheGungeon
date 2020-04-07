using Dungeonator;
using System;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using System.Reflection;
using MonoMod.RuntimeDetour;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandCustomDungeonPrefabs : DungeonDatabase {

        public static GameObject GameManagerObject;
                
        public static List<string> customDungeons;

        public static GameObject Base_Canyon;

        public static GameLevelDefinition CanyonDefinition;

        public static Dungeon GetOrLoadByNameHook(string name) {
            Dungeon dungeon = null;
            foreach (string dungeonName in customDungeons) {
                if (dungeonName.ToLower() == name.ToLower()) {
                    if (name.ToLower() == "base_canyon") {
                        dungeon = CanyonDungeon(GetOrLoadByName_Orig("Base_ResourcefulRat"));
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

            customDungeons = new List<string>() { "Base_Canyon" };
            
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

            if (GameManager.Instance) {
                foreach (GameLevelDefinition definition in GameManager.Instance.customFloors) {
                    if (definition.dungeonSceneName == "tt_canyon") { EntryNotExist = false; }
                    break;
                }
                if (EntryNotExist) { GameManager.Instance.customFloors.Add(CanyonDefinition); }
            }

            if (GameManagerObject && GameManagerObject.GetComponent<GameManager>()) {
                foreach (GameLevelDefinition definition in GameManagerObject.GetComponent<GameManager>().customFloors) {
                    if (definition.dungeonSceneName == "tt_canyon") { EntryNotExist2 = false; }
                    break;
                }
                if (EntryNotExist2) { GameManagerObject.GetComponent<GameManager>().customFloors.Add(CanyonDefinition); }
            } else if (GameManagerObject == null) {
                AssetBundle braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");
                GameManagerObject = braveResources.LoadAsset<GameObject>("_GameManager");
                GameManagerObject.GetComponent<GameManager>().customFloors.Add(CanyonDefinition);
                braveResources = null;
            }
        }


        public static Dungeon CanyonDungeon(Dungeon dungeon) {
            Dungeon MinesDungeonPrefab = GetOrLoadByName_Orig("Base_Mines");
            Dungeon RatDungeonPrefab = GetOrLoadByName_Orig("Base_ResourcefulRat");
            Dungeon FinalScenarioPilotPrefab = GetOrLoadByName_Orig("FinalScenario_Pilot");
            Dungeon FinalScenarioBulletPrefab = GetOrLoadByName_Orig("FinalScenario_Bullet");


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
                        MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[3],
                        MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[3],
                        MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[2], // shop visual type. Do not remove
                        MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[3],
                        MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[3],
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
                dungeonCollection = ExpandUtility.BuildSpriteCollection(FinalScenarioPilotPrefab.tileIndices.dungeonCollection, ExpandPrefabs.ENV_Tileset_Canyon_Texture),
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
                MinesDungeonPrefab.roomMaterialDefinitions[0],
                MinesDungeonPrefab.roomMaterialDefinitions[1],
                MinesDungeonPrefab.roomMaterialDefinitions[2],
                MinesDungeonPrefab.roomMaterialDefinitions[3],
                MinesDungeonPrefab.roomMaterialDefinitions[4],
                MinesDungeonPrefab.roomMaterialDefinitions[5],
                MinesDungeonPrefab.roomMaterialDefinitions[6],
                MinesDungeonPrefab.roomMaterialDefinitions[7]
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
            dungeon.BossMasteryTokenItemId = 468;
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
    }
}

