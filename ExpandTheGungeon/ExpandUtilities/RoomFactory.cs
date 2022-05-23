using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Dungeonator;
using FloorType = Dungeonator.CellVisualData.CellFloorType;
using ExpandTheGungeon.ExpandPrefab;


namespace ExpandTheGungeon.ExpandUtilities {
    
    public class RoomFactory {
        
        public static readonly string dataHeader = "***DATA***";
        private static readonly RoomEventDefinition sealOnEnterWithEnemies = new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM);
        private static readonly RoomEventDefinition unsealOnRoomClear = new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM);
                
        public struct RoomData {
            public string category;
            public string normalSubCategory;
            public string specialSubCategory;
            public string bossSubCategory;
            public Vector2[] enemyPositions;
            public string[] enemyGUIDs;
            public Vector2[] placeablePositions;
            public string[] placeableGUIDs;
            public int[] enemyReinforcementLayers;
            public Vector2[] exitPositions;
            public string[] exitDirections;
            public string[] floors;
            public float weight;
            public bool isSpecialRoom;
            public bool randomizeEnemyPositions, doFloorDecoration, doWallDecoration, doLighting;
            [NonSerialized]
            public PrototypeDungeonRoom room;
        }
        
        public static PrototypeDungeonRoom BuildFromAssetBundle(AssetBundle[] Bundles, string assetPath, bool setRoomCategory = false, bool autoAssignToFloor = false, bool assignDecorationSettings = false) {
            TextAsset m_Asset = ExpandAssets.LoadAsset<TextAsset>(assetPath); ;
            if (m_Asset) {
                RoomData roomData = ExtractRoomDataFromTextAssetBytes(m_Asset);
                return Build(Bundles, ExpandUtility.BytesToTexture(m_Asset.bytes, m_Asset.name), roomData, setRoomCategory, autoAssignToFloor, assignDecorationSettings, roomData.weight);
            } else {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: RoomFactory asset: " + assetPath + " was not found!");
                return null;
            }
        }

        public static PrototypeDungeonRoom Build(AssetBundle[] Bundles, Texture2D texture, RoomData roomData, bool SetRoomCategory, bool AutoAssignToFloor, bool AssignDecorationProperties, float? Weight) {
            try {
                PrototypeDungeonRoom room = CreateRoomFromTexture(texture);
                ApplyRoomData(Bundles, room, roomData, SetRoomCategory, AutoAssignToFloor, AssignDecorationProperties, Weight);
                room.OnBeforeSerialize();
                room.OnAfterDeserialize();
                room.UpdatePrecalculatedData();
                return room;
            } catch (Exception e) {
                Tools.PrintError("Failed to build room!");
                Tools.PrintException(e);
            }
            return CreateEmptyRoom(12, 12);
        }

        public static void ApplyRoomData(AssetBundle[] Bundles, PrototypeDungeonRoom room, RoomData roomData, bool setRoomCategory, bool autoAssignToFloor, bool assignDecorationProperties, float? Weight, bool CellDataWasSerialized = false) {
            // Tools.Print("Building Exits...");
            if (roomData.exitPositions != null) {
                for (int i = 0; i < roomData.exitPositions.Length; i++) {
                    DungeonData.Direction dir = (DungeonData.Direction)Enum.Parse(typeof(DungeonData.Direction), roomData.exitDirections[i].ToUpper());
                    AddExit(room, roomData.exitPositions[i], dir);
                }
            } else {
                AddExit(room, new Vector2(room.Width / 2, room.Height), DungeonData.Direction.NORTH);
                AddExit(room, new Vector2(room.Width / 2, 0), DungeonData.Direction.SOUTH);
                AddExit(room, new Vector2(room.Width, room.Height / 2), DungeonData.Direction.EAST);
                AddExit(room, new Vector2(0, room.Height / 2), DungeonData.Direction.WEST);
            }

            // Tools.Print("Adding Enemies...");
            if (roomData.enemyPositions != null) {
                for (int i = 0; i < roomData.enemyPositions.Length; i++) {
                    AddEnemyToRoom(room, roomData.enemyPositions[i], roomData.enemyGUIDs[i], roomData.enemyReinforcementLayers[i], roomData.randomizeEnemyPositions);
                }
            }

            // Tools.Print("Adding Objects...");
            if (roomData.placeablePositions != null) {
               for (int i = 0; i < roomData.placeablePositions.Length; i++) {
					AddPlaceableToRoom(Bundles, room, roomData.placeablePositions[i], roomData.placeableGUIDs[i]);
				}
            }

            if (setRoomCategory | autoAssignToFloor) {
                // Set categories
                if (!string.IsNullOrEmpty(roomData.category)) { room.category = GetRoomCategory(roomData.category); }
                if (!string.IsNullOrEmpty(roomData.normalSubCategory)) { room.subCategoryNormal = GetRoomNormalSubCategory(roomData.normalSubCategory); }
                if (!string.IsNullOrEmpty(roomData.bossSubCategory)) { room.subCategoryBoss = GetRoomBossSubCategory(roomData.bossSubCategory); }
                if (!string.IsNullOrEmpty(roomData.specialSubCategory)) { room.subCategorySpecial = GetRoomSpecialSubCategory(roomData.specialSubCategory); }
            }
            if (autoAssignToFloor && roomData.floors != null) {
                if (!Weight.HasValue) {
                    if (room.category == PrototypeDungeonRoom.RoomCategory.SECRET) {
                        Weight = 15; // Normal secret rooms have weight of 15.
                    } else {
                        Weight = 1;
                    }
                }
                if (room.category == PrototypeDungeonRoom.RoomCategory.SECRET) {
                    // Secret rooms are generally shared across all floors via a specific room table.
                    // Room Editor doesn't currently set this for secret rooms
                    room.OverrideMusicState = DungeonFloorMusicController.DungeonMusicState.CALM;
                    ExpandPrefabs.SecretRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(room, Weight.Value));
                } else {
                    foreach (string floor in roomData.floors) { AssignRoomToFloorRoomTable(room, GetTileSet(floor), Weight); }
                    /*ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(room, Weight.Value));
                    ExpandPrefabs.CustomRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(room, Weight.Value));
                    ExpandPrefabs.CustomRoomTable2.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(room, Weight.Value));*/
                }
            }

            if (assignDecorationProperties) {
                room.allowFloorDecoration = roomData.doFloorDecoration;
                room.allowWallDecoration = roomData.doWallDecoration;
                room.usesProceduralLighting = roomData.doLighting;
            }

            // Define FloorData in from second Texture object if one exists
            if (!CellDataWasSerialized) {
                Texture2D m_ExtraFloorData = ExpandAssets.LoadAsset<Texture2D>(room.name + "_FloorData");
                if (m_ExtraFloorData) { ApplyExtraFloorCellDataFromTexture2D(room, m_ExtraFloorData); }
            }
        }

        public static void AssignRoomToFloorRoomTable(PrototypeDungeonRoom Room, GlobalDungeonData.ValidTilesets targetTileSet, float? Weight) {
            if (targetTileSet == GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                ExpandPrefabs.CastleRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(Room, Weight.Value));
            } else if (targetTileSet == GlobalDungeonData.ValidTilesets.SEWERGEON) {
                ExpandPrefabs.SewersRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(Room, Weight.Value));
            } else if (targetTileSet == GlobalDungeonData.ValidTilesets.GUNGEON) {
                ExpandPrefabs.Gungeon_RoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(Room, Weight.Value));
            } else if (targetTileSet == GlobalDungeonData.ValidTilesets.CATHEDRALGEON) {
                ExpandPrefabs.AbbeyRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(Room, Weight.Value));
            } else if (targetTileSet == GlobalDungeonData.ValidTilesets.MINEGEON) {
                ExpandPrefabs.MinesRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(Room, Weight.Value));
            } else if (targetTileSet == GlobalDungeonData.ValidTilesets.CATACOMBGEON) {
                ExpandPrefabs.CatacombsRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(Room, Weight.Value));
            } else if (targetTileSet == GlobalDungeonData.ValidTilesets.OFFICEGEON) {
                // R&G floor doesn't use room tables yet. If a room has this set, I'll assign to all floors instead.
                return;
            } else if (targetTileSet == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                ExpandPrefabs.ForgeRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(Room, Weight.Value));
            } else if (targetTileSet == GlobalDungeonData.ValidTilesets.HELLGEON) {
                ExpandPrefabs.BulletHellRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(Room, Weight.Value));
            }
            return;
        }


        public static GlobalDungeonData.ValidTilesets GetTileSet(string val) {
            return (GlobalDungeonData.ValidTilesets)Enum.Parse(typeof(GlobalDungeonData.ValidTilesets), val.ToUpper());
        }
        public static PrototypeDungeonRoom.RoomCategory GetRoomCategory(string val) {
            return (PrototypeDungeonRoom.RoomCategory)Enum.Parse(typeof(PrototypeDungeonRoom.RoomCategory), val.ToUpper());
        }
        public static PrototypeDungeonRoom.RoomNormalSubCategory GetRoomNormalSubCategory(string val) {
            return (PrototypeDungeonRoom.RoomNormalSubCategory)Enum.Parse(typeof(PrototypeDungeonRoom.RoomNormalSubCategory), val.ToUpper());
        }
        public static PrototypeDungeonRoom.RoomBossSubCategory GetRoomBossSubCategory(string val) {
            return (PrototypeDungeonRoom.RoomBossSubCategory)Enum.Parse(typeof(PrototypeDungeonRoom.RoomBossSubCategory), val.ToUpper());
        }
        public static PrototypeDungeonRoom.RoomSpecialSubCategory GetRoomSpecialSubCategory(string val) {
            return (PrototypeDungeonRoom.RoomSpecialSubCategory)Enum.Parse(typeof(PrototypeDungeonRoom.RoomSpecialSubCategory), val.ToUpper());
        }

        public static RoomData ExtractRoomDataFromFile(string path) {
            string data = ExpandAssets.BytesToString(File.ReadAllBytes(path));
            return ExtractRoomData(data);
        }

        public static RoomData ExtractRoomDataFromTextAssetBytes(TextAsset textAsset) {
            return ExtractRoomData(ExpandAssets.BytesToString(textAsset.bytes));
        }
                
        public static RoomData ExtractRoomData(string data) {
            int num = (data.Length - dataHeader.Length - 1);
            for (int i = num; i > 0; i--) {
                string text = data.Substring(i, dataHeader.Length);
                if (text.Equals(dataHeader)) { return JsonUtility.FromJson<RoomData>(data.Substring(i + dataHeader.Length)); }
            }
            ETGModConsole.Log("No room data found!", true);
            return default(RoomData);
        }
        
        public static PrototypeDungeonRoom CreateRoomFromTexture(Texture2D texture) {
            int width = texture.width;
            int height = texture.height;
            PrototypeDungeonRoom room = GetNewPrototypeDungeonRoom(width, height);
            room.FullCellData = new PrototypeDungeonRoomCellData[width * height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    room.FullCellData[x + y * width] = CellDataFromColor(texture.GetPixel(x, y));
                }
            }
            room.name = texture.name;
            return room;
        }

        public static PrototypeDungeonRoomCellData CellDataFromColor(Color32 color) {
            // if (color.Equals(Color.magenta)) return null;

            PrototypeDungeonRoomCellData data = new PrototypeDungeonRoomCellData();
            data.state = TypeFromColor(color);
            data.diagonalWallType = DiagonalWallTypeFromColor(color);
            data.appearance = new PrototypeDungeonRoomCellAppearance() { OverrideFloorType = FloorType.Stone };

            if (color == Color.red) {
                data.appearance.OverrideFloorType = FloorType.Carpet;
                data.appearance.IsPhantomCarpet = true;
            }

            return data;
        }

        public static CellType TypeFromColor(Color color) {
            if (color == Color.black) {
                return CellType.PIT;
            } else if (color == Color.white | color == Color.red) {
                return CellType.FLOOR;
            }
            return CellType.WALL;
        }

        public static DiagonalWallType DiagonalWallTypeFromColor(Color color) {
            if (color == Color.red)
                return DiagonalWallType.NORTHEAST;
            else if (color == Color.green)
                return DiagonalWallType.SOUTHEAST;
            else if (color == Color.blue)
                return DiagonalWallType.SOUTHWEST;
            else if (color == Color.yellow)
                return DiagonalWallType.NORTHWEST;
            else
                return DiagonalWallType.NONE;
        }

        public static PrototypeDungeonRoom CreateEmptyRoom(int width = 12, int height = 12) {
            try {
                Tools.Print("  Create Empty Room...", "5599FF");
                PrototypeDungeonRoom room = GetNewPrototypeDungeonRoom(width, height);
                AddExit(room, new Vector2(width / 2, height), DungeonData.Direction.NORTH);
                AddExit(room, new Vector2(width / 2, 0), DungeonData.Direction.SOUTH);
                AddExit(room, new Vector2(width, height / 2), DungeonData.Direction.EAST);
                AddExit(room, new Vector2(0, height / 2), DungeonData.Direction.WEST);
                
                room.FullCellData = new PrototypeDungeonRoomCellData[width * height];
                for (int x = 0; x < width; x++) {
                    for (int y = 0; y < height; y++) {
                        room.FullCellData[x + y * width] = new PrototypeDungeonRoomCellData() {
                            state = CellType.FLOOR,
                            appearance = new PrototypeDungeonRoomCellAppearance() {
                                OverrideFloorType = FloorType.Stone,
                            },
                        };
                    }
                }

                room.OnBeforeSerialize();
                room.OnAfterDeserialize();
                room.UpdatePrecalculatedData();
                return room;
            } catch (Exception e) {
                Tools.PrintException(e);
                return null;
            }
        }
        
        public static GameObject GetGameObjectFromBundles(AssetBundle[] bundles, string assetPath) {
            foreach (AssetBundle bundle in bundles) {
                if (bundle.LoadAsset<GameObject>(assetPath)) { return bundle.LoadAsset<GameObject>(assetPath); }
            }
            return null;            
        }

        public static DungeonPlaceable GetPlaceableFromBundles(AssetBundle[] bundles, string assetPath) {
            foreach (AssetBundle bundle in bundles) {
                if (bundle.LoadAsset<DungeonPlaceable>(assetPath)) { return bundle.LoadAsset<DungeonPlaceable>(assetPath); }
            }
            return null;
        }
        
        public static void AddEnemyToRoom(PrototypeDungeonRoom room, Vector2 location, string guid, int layer, bool shuffle) {
            DungeonPlaceable placeableContents = ScriptableObject.CreateInstance<DungeonPlaceable>();
            placeableContents.width = 1;
            placeableContents.height = 1;
            placeableContents.respectsEncounterableDifferentiator = true;
            placeableContents.variantTiers = new List<DungeonPlaceableVariant>() {
                new DungeonPlaceableVariant() {
                    percentChance = 1,
                    prerequisites = new DungeonPrerequisite[0],
                    enemyPlaceableGuid = guid,
                    materialRequirements= new DungeonPlaceableRoomMaterialRequirement[0],
                }
            };

            PrototypePlacedObjectData objectData = new PrototypePlacedObjectData() {
                contentsBasePosition = location,
                fieldData = new List<PrototypePlacedObjectFieldData>(),
                instancePrerequisites = new DungeonPrerequisite[0],
                linkedTriggerAreaIDs = new List<int>(),
                placeableContents = placeableContents,
            };

            if (layer > 0) {
                AddObjectDataToReinforcementLayer(room, objectData, layer - 1, location, shuffle);
            } else {
                room.placedObjects.Add(objectData);
                room.placedObjectPositions.Add(location);
            }

            if (!room.roomEvents.Contains(sealOnEnterWithEnemies)) { room.roomEvents.Add(sealOnEnterWithEnemies); }
            if (!room.roomEvents.Contains(unsealOnRoomClear)) { room.roomEvents.Add(unsealOnRoomClear); }
        }

        public static void AddObjectDataToReinforcementLayer(PrototypeDungeonRoom room, PrototypePlacedObjectData objectData, int layer, Vector2 location, bool shuffle) {
            if (room.additionalObjectLayers.Count <= layer) {
                for (int i = room.additionalObjectLayers.Count; i <= layer; i++) {
                    PrototypeRoomObjectLayer newLayer = new PrototypeRoomObjectLayer {
                        layerIsReinforcementLayer = true,
                        placedObjects = new List<PrototypePlacedObjectData>(),
                        placedObjectBasePositions = new List<Vector2>(),
                        shuffle = shuffle
                    };
                    room.additionalObjectLayers.Add(newLayer);
                }
            }
            room.additionalObjectLayers[layer].placedObjects.Add(objectData);
            room.additionalObjectLayers[layer].placedObjectBasePositions.Add(location);
        }

        public static void AddPlaceableToRoom(AssetBundle[] Bundles, PrototypeDungeonRoom room, Vector2 location, string assetPath) {
            try {
               if (GetGameObjectFromBundles(Bundles, assetPath) != null) {
					DungeonPrerequisite[] array = new DungeonPrerequisite[0];
					room.placedObjectPositions.Add(location);
					DungeonPlaceable dungeonPlaceable = ScriptableObject.CreateInstance<DungeonPlaceable>();
					dungeonPlaceable.width = 2;
					dungeonPlaceable.height = 2;
					dungeonPlaceable.respectsEncounterableDifferentiator = true;
					dungeonPlaceable.variantTiers = new List<DungeonPlaceableVariant> {
						new DungeonPlaceableVariant {
							percentChance = 1f,
							nonDatabasePlaceable = GetGameObjectFromBundles(Bundles, assetPath),
							prerequisites = array,
							materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
						}
					};
					room.placedObjects.Add(new PrototypePlacedObjectData {
						contentsBasePosition = location,
						fieldData = new List<PrototypePlacedObjectFieldData>(),
						instancePrerequisites = array,
						linkedTriggerAreaIDs = new List<int>(),
						placeableContents = dungeonPlaceable
					});
                    return;                
                } else if (GetPlaceableFromBundles(Bundles, assetPath) != null) {
                    DungeonPrerequisite[] instancePrerequisites = new DungeonPrerequisite[0];
                    room.placedObjectPositions.Add(location);
                    room.placedObjects.Add(
                        new PrototypePlacedObjectData {
                    	    contentsBasePosition = location,
                    	    fieldData = new List<PrototypePlacedObjectFieldData>(),
                    	    instancePrerequisites = instancePrerequisites,
                    	    linkedTriggerAreaIDs = new List<int>(),
                    	    placeableContents = GetPlaceableFromBundles(Bundles, assetPath)
                        }
                    );
                    return;
                } else {
                    Tools.PrintError("Unable to find asset in asset bundles: " + assetPath, "FF0000");
                }
            } catch (Exception e) {
                Tools.PrintException(e);
            }
        }

        public static void AddExit(PrototypeDungeonRoom room, Vector2 location, DungeonData.Direction direction) {
            if (room.exitData == null) { room.exitData = new PrototypeRoomExitData(); }
            if (room.exitData.exits == null) { room.exitData.exits = new List<PrototypeRoomExit>(); }

            PrototypeRoomExit exit = new PrototypeRoomExit(direction, location);
            exit.exitType = PrototypeRoomExit.ExitType.NO_RESTRICTION;
            Vector2 margin = (direction == DungeonData.Direction.EAST || direction == DungeonData.Direction.WEST) ? new Vector2(0, 1) : new Vector2(1, 0);
            exit.containedCells.Add(location + margin);
            room.exitData.exits.Add(exit);
        }

        public static PrototypeDungeonRoom GetNewPrototypeDungeonRoom(int width = 12, int height = 12) {
            PrototypeDungeonRoom room = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            room.injectionFlags = new RuntimeInjectionFlags();
            room.RoomId = UnityEngine.Random.Range(10000, 1000000);
            room.pits = new List<PrototypeRoomPitEntry>();
            room.placedObjects = new List<PrototypePlacedObjectData>();
            room.placedObjectPositions = new List<Vector2>();
            room.additionalObjectLayers = new List<PrototypeRoomObjectLayer>();
            room.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            room.roomEvents = new List<RoomEventDefinition>();
            room.overriddenTilesets = 0;
            room.paths = new List<SerializedPath>();
            room.prerequisites = new List<DungeonPrerequisite>();
            room.excludedOtherRooms = new List<PrototypeDungeonRoom>();
            room.rectangularFeatures = new List<PrototypeRectangularFeature>();
            room.exitData = new PrototypeRoomExitData();
            room.exitData.exits = new List<PrototypeRoomExit>();
            room.Width = width;
            room.Height = height;

            // Not originally defined as defaults in Kyle's code.
            room.GUID = Guid.NewGuid().ToString();
            room.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            room.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            room.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            room.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            room.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            room.usesProceduralDecoration = true;
            room.usesProceduralLighting = true;
            room.allowWallDecoration = true;
            room.allowFloorDecoration = true;            
            room.PreventMirroring = false;
            room.InvalidInCoop = false;            
            room.preventAddedDecoLayering = false;
            room.precludeAllTilemapDrawing = false;
            room.drawPrecludedCeilingTiles = false;
            room.preventBorders = false;
            room.preventFacewallAO = false;
            room.usesCustomAmbientLight = false;
            room.customAmbientLight = Color.white;
            room.ForceAllowDuplicates = false;
            room.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            room.IsLostWoodsRoom = false;
            room.UseCustomMusic = false;
            room.UseCustomMusicState = false;
            room.CustomMusicEvent = string.Empty;
            room.UseCustomMusicSwitch = false;
            room.CustomMusicSwitch = string.Empty;
            room.overrideRoomVisualTypeForSecretRooms = false;
            room.rewardChestSpawnPosition = new IntVector2(-1, -1);
            room.overrideRoomVisualType = -1;

            return room;
        }


                
        public static void ApplyExtraFloorCellDataFromTexture2D(PrototypeDungeonRoom room, Texture2D sourceTexture) {
            if (sourceTexture == null) {
                ETGModConsole.Log("[ExpandTheGungeon] ApplyExtraFloorCellDataFromTexture2D: WARNING! Requested Texture for extra floor data is null!", ExpandSettings.debugMode);
                ETGModConsole.Log("[ExpandTheGungeon] Room: " + room.name + " will not have any extra floor data!", ExpandSettings.debugMode);
                return;
            }

            int width = room.Width;
            int height = room.Height;
            int ArrayLength = (width * height);

            if (sourceTexture.GetPixels32().Length != ArrayLength) {
                ETGModConsole.Log("[ExpandTheGungeon] ApplyExtraFloorCellDataFromTexture2D: WARNING! Image resolution doesn't match size of room!", ExpandSettings.debugMode);
                ETGModConsole.Log("[ExpandTheGungeon] Room: " + room.name + " will not have any extra floor data!", ExpandSettings.debugMode);
                return;
            }

            Color WhitePixel = new Color32(255, 255, 255, 255); // Normal Floor

            Color RedPixel = new Color32(255, 0, 0, 255); // Fire damage cell

            Color GreenPixel = new Color32(0, 255, 0, 255); // Poison damage cell

            Color BlueHalfGreenPixel = new Color32(0, 127, 255, 255); // Ice Override
            Color HalfBluePixel = new Color32(0, 0, 127, 255); // Water Override
            Color HalfRedPixel = new Color32(0, 0, 127, 255); // Carpet Override
            Color GreenHalfRBPixel = new Color32(127, 255, 127, 255); // Grass Override
            Color HalfWhitePixel = new Color32(127, 127, 127, 255); // Bone Override
            Color OrangePixel = new Color32(255, 127, 0, 255); // Flesh Override
            Color RedHalfGBPixel = new Color32(255, 127, 127, 255); // ThickGoop Override
            
            for (int X = 0; X < width; X++) {
                for (int Y = 0; Y < height; Y++) {
                    int ArrayPosition = (Y * width + X);
                    Color? m_Pixel = sourceTexture.GetPixel(X, Y);
                    PrototypeDungeonRoomCellData cellData = room.FullCellData[ArrayPosition];
                    float DamageToPlayersPerTick = 0;
                    float DamageToEnemiesPerTick = 0;
                    float TickFrequency = 0;
                    bool RespectsFlying = true;
                    bool IsPoison = false;
                    bool isDamageCell = false;
                    CoreDamageTypes DamageCellsType = CoreDamageTypes.None;
                    FloorType floorType = FloorType.Stone;
                    if (cellData != null && m_Pixel.HasValue && cellData.state == CellType.FLOOR) {
                        if (m_Pixel.Value == RedPixel) {
                            floorType = FloorType.Stone;
                            DamageCellsType = CoreDamageTypes.Fire;
                        } else if (m_Pixel.Value == BlueHalfGreenPixel) {
                            floorType = FloorType.Ice;
                        } else if (m_Pixel.Value == HalfBluePixel) {
                            floorType = FloorType.Water;
                        } else if (m_Pixel.Value == HalfRedPixel) {
                            floorType = FloorType.Carpet;
                        } else if (m_Pixel.Value == GreenHalfRBPixel) {
                            floorType = FloorType.Grass;
                        } else if (m_Pixel.Value == HalfWhitePixel) {
                            floorType = FloorType.Bone;
                        } else if (m_Pixel.Value == OrangePixel) {
                            floorType = FloorType.Flesh;
                        } else if (m_Pixel.Value == RedHalfGBPixel) {
                            floorType = FloorType.ThickGoop;
                        }
                        if (DamageCellsType == CoreDamageTypes.Fire) {
                            isDamageCell = true;
                            DamageToPlayersPerTick = 0.5f;
                            TickFrequency = 1;
                        } else if (DamageCellsType == CoreDamageTypes.Poison) {
                            IsPoison = true;
                            isDamageCell = true;
                            DamageToPlayersPerTick = 0.5f;
                            TickFrequency = 1;
                        }
                        ApplyExtraFloorCellData(cellData, DamageCellsType, floorType, DamageToPlayersPerTick, DamageToEnemiesPerTick, TickFrequency, RespectsFlying, isDamageCell, IsPoison);
                    }
                }
            }
        }
        

        // New stuff for extra floor cell types
        public static void ApplyExtraFloorCellData(PrototypeDungeonRoomCellData cellData, CoreDamageTypes DamageType, FloorType FloorType, float DamageToPlayersPerTick = 0, float DamageToEnemiesPerTick = 0, float TickFrequency = 0, bool RespectsFlying = true, bool DoesDamage = false, bool IsPoison = false) {
            cellData.doesDamage = DoesDamage;
            cellData.damageDefinition = new CellDamageDefinition() {
                damageTypes = DamageType,
                damageToPlayersPerTick = DamageToPlayersPerTick,
                damageToEnemiesPerTick = DamageToEnemiesPerTick,
                tickFrequency = TickFrequency,
                respectsFlying = RespectsFlying,
                isPoison = IsPoison
            };
            if (cellData.appearance == null) {
                cellData.appearance = new PrototypeDungeonRoomCellAppearance() {
                    overrideDungeonMaterialIndex = -1,
                    IsPhantomCarpet = false,
                    ForceDisallowGoop = false,
                    globalOverrideIndices = new PrototypeIndexOverrideData() { indices = new List<int>(0) }
                };
            }
            if (DamageType == CoreDamageTypes.Poison) {
                cellData.ForceTileNonDecorated = true;
                // cellData.appearance.OverrideFloorType = FloorType.Stone;
                cellData.damageDefinition.damageTypes = CoreDamageTypes.Poison;
            } else if (DamageType == CoreDamageTypes.Fire) {
                cellData.ForceTileNonDecorated = true;
                // cellData.appearance.OverrideFloorType = FloorType.Stone;
                cellData.damageDefinition.damageTypes = CoreDamageTypes.Fire;
            }
            cellData.appearance.OverrideFloorType = FloorType;
        }

    }
}

