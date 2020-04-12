using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Dungeonator;
// using Random = UnityEngine.Random;
using FloorType = Dungeonator.CellVisualData.CellFloorType;

namespace ExpandTheGungeon.ExpandUtilities {

    public static class RoomFactory {

        public static readonly string dataHeader = "***DATA***";

        public static string TextureBasePath = "Textures\\RoomLayoutData\\RoomFactoryRooms\\";
        private static string nameSpace = "ExpandTheGungeon";

        // public static string roomDirectory = Path.Combine(ETGMod.GameFolder, "CustomRoomData");

        public static Dictionary<string, PrototypeDungeonRoom> rooms = new Dictionary<string, PrototypeDungeonRoom>();
        // private static FieldInfo m_cellData = typeof(PrototypeDungeonRoom).GetField("m_cellData", BindingFlags.Instance | BindingFlags.NonPublic);
        private static RoomEventDefinition sealOnEnterWithEnemies = new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM);
        private static RoomEventDefinition unsealOnRoomClear = new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM);

        public struct RoomData {
            public string category, normalSubCategory, specialSubCatergory, bossSubCategory;
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
            [NonSerialized]
            public PrototypeDungeonRoom room;
        }

        /*public static void LoadRoomsFromRoomDirectory() {
            Directory.CreateDirectory(roomDirectory);
            foreach (string f in Directory.GetFiles(roomDirectory)) {
                if (!f.EndsWith(".room")) continue;
                Tools.Log($"Found room: \"{f}\"");
                rooms.Add(f, BuildFromFile(Path.Combine(roomDirectory, f)));
            }
        }

        public static PrototypeDungeonRoom BuildFromFile(string roomPath) {
            var texture = ResourceExtractor.GetTextureFromFile(roomPath, ".room");
            RoomData roomData = ExtractRoomDataFromFile(roomPath);
            return Build(texture, roomData);
        }*/

        public static PrototypeDungeonRoom BuildFromResource(string roomPath) {
            string RoomPath = (TextureBasePath + roomPath);
            Texture2D texture = ResourceExtractor.GetTextureFromResource(RoomPath);
            RoomData roomData = ExtractRoomDataFromResource(RoomPath);
            return Build(texture, roomData);
        }

        public static PrototypeDungeonRoom Build(Texture2D texture, RoomData roomData) {
            try {
                PrototypeDungeonRoom room = CreateRoomFromTexture(texture);
                ApplyRoomData(room, roomData);                
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

        public static void ApplyRoomData(PrototypeDungeonRoom room, RoomData roomData) {
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
                    AddEnemyToRoom(room, roomData.enemyPositions[i], roomData.enemyGUIDs[i], roomData.enemyReinforcementLayers[i]);
                }
            }

            // Tools.Print("Adding Objects...");
            if (roomData.placeablePositions != null) {
                for (int i = 0; i < roomData.placeablePositions.Length; i++) {
                    AddPlaceableToRoom(room, roomData.placeablePositions[i], roomData.placeableGUIDs[i]);
                }
            }

            //Set categories
            /*if (!string.IsNullOrEmpty(roomData.category)) { room.category = Tools.GetEnumValue<PrototypeDungeonRoom.RoomCategory>(roomData.category); }
            if (!string.IsNullOrEmpty(roomData.normalSubCategory)) { room.subCategoryNormal = Tools.GetEnumValue<PrototypeDungeonRoom.RoomNormalSubCategory>(roomData.normalSubCategory); }
            if (!string.IsNullOrEmpty(roomData.bossSubCategory)) { room.subCategoryBoss = Tools.GetEnumValue<PrototypeDungeonRoom.RoomBossSubCategory>(roomData.bossSubCategory); }
            if (!string.IsNullOrEmpty(roomData.specialSubCatergory)) { room.subCategorySpecial = Tools.GetEnumValue<PrototypeDungeonRoom.RoomSpecialSubCategory>(roomData.specialSubCatergory); }*/
        }

        public static RoomData ExtractRoomDataFromFile(string path) {
            string data = ResourceExtractor.BytesToString(File.ReadAllBytes(path));
            return ExtractRoomData(data);
        }

        public static RoomData ExtractRoomDataFromResource(string path) {
            path = path.Replace("/", ".");
            path = path.Replace("\\", ".");
            string data = ResourceExtractor.BytesToString(ResourceExtractor.ExtractEmbeddedResource(($"{nameSpace}." + path)));
            return ExtractRoomData(data);
        }

        public static RoomData ExtractRoomData(string data) {
           if (data.Contains(dataHeader)) {
                string dataContent = data.Substring(data.IndexOf(dataHeader) + dataHeader.Length);
                return JsonUtility.FromJson<RoomData>(dataContent);
            }
            return new RoomData();
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
            if (color.Equals(Color.magenta)) return null;

            PrototypeDungeonRoomCellData data = new PrototypeDungeonRoomCellData();
            data.state = TypeFromColor(color);
            data.diagonalWallType = DiagonalWallTypeFromColor(color);
            data.appearance = new PrototypeDungeonRoomCellAppearance() { OverrideFloorType = FloorType.Stone };
            return data;
        }

        public static CellType TypeFromColor(Color color) {
            if (color == Color.black)
                return CellType.PIT;
            else if (color == Color.white)
                return CellType.FLOOR;
            else
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

        /*public static int GetStyleValue(string dungeonName, string shrineID) {
            if (ShrineFactory.builtShrines != null && ShrineFactory.builtShrines.ContainsKey(shrineID)) {
                CustomShrineData shrineData = ShrineFactory.builtShrines[shrineID]?.GetComponent<CustomShrineData>();
                if (shrineData != null && shrineData.roomStyles != null && shrineData.roomStyles.ContainsKey(dungeonName))
                    return shrineData.roomStyles[dungeonName];
            }
            return -1;
        }*/

        public static GameObject GetPlaceableFromBundles(string assetPath) {
            GameObject asset = null;
            AssetBundle[] assetBundles = new AssetBundle[] {
                ResourceManager.LoadAssetBundle("shared_auto_001"),
                ResourceManager.LoadAssetBundle("shared_auto_002")
            };
            foreach (AssetBundle bundle in assetBundles) {
                asset = bundle.LoadAsset(assetPath) as GameObject;
                if (asset) { break; }
            }
            assetBundles[0] = null;
            assetBundles[1] = null;
            return asset;
        }

        public static void AddEnemyToRoom(PrototypeDungeonRoom room, Vector2 location, string guid, int layer) {
            DungeonPrerequisite[] emptyReqs = new DungeonPrerequisite[0];

            DungeonPlaceable placeableContents = ScriptableObject.CreateInstance<DungeonPlaceable>();
            placeableContents.width = 1;
            placeableContents.height = 1;
            placeableContents.respectsEncounterableDifferentiator = true;
            placeableContents.variantTiers = new List<DungeonPlaceableVariant>() {
                new DungeonPlaceableVariant() {
                    percentChance = 1,
                    prerequisites = emptyReqs,
                    enemyPlaceableGuid = guid,
                    materialRequirements= new DungeonPlaceableRoomMaterialRequirement[0],
                }
            };

            PrototypePlacedObjectData objectData = new PrototypePlacedObjectData() {
                contentsBasePosition = location,
                fieldData = new List<PrototypePlacedObjectFieldData>(),
                instancePrerequisites = emptyReqs,
                linkedTriggerAreaIDs = new List<int>(),
                placeableContents = placeableContents,
            };

            if (layer > 0) {
                AddObjectDataToReinforcementLayer(room, objectData, layer - 1, location);
            } else {
                room.placedObjects.Add(objectData);
                room.placedObjectPositions.Add(location);
            }

            if (!room.roomEvents.Contains(sealOnEnterWithEnemies)) { room.roomEvents.Add(sealOnEnterWithEnemies); }
            if (!room.roomEvents.Contains(unsealOnRoomClear)) { room.roomEvents.Add(unsealOnRoomClear); }
        }

        public static void AddObjectDataToReinforcementLayer(PrototypeDungeonRoom room, PrototypePlacedObjectData objectData, int layer, Vector2 location) {
            if (room.additionalObjectLayers.Count <= layer) {
                for (int i = room.additionalObjectLayers.Count; i <= layer; i++) {
                    PrototypeRoomObjectLayer newLayer = new PrototypeRoomObjectLayer() {
                        layerIsReinforcementLayer = true,
                        placedObjects = new List<PrototypePlacedObjectData>(),
                        placedObjectBasePositions = new List<Vector2>()
                    };
                    room.additionalObjectLayers.Add(newLayer);

                }
            }
            room.additionalObjectLayers[layer].placedObjects.Add(objectData);
            room.additionalObjectLayers[layer].placedObjectBasePositions.Add(location);
        }

        public static void AddPlaceableToRoom(PrototypeDungeonRoom room, Vector2 location, string assetPath) {
            try {
                GameObject asset = GetPlaceableFromBundles(assetPath);
                if (asset) {
                    DungeonPrerequisite[] emptyReqs = new DungeonPrerequisite[0];
                    room.placedObjectPositions.Add(location);
                    room.placedObjects.Add(new PrototypePlacedObjectData() {
                        contentsBasePosition = location,
                        fieldData = new List<PrototypePlacedObjectFieldData>(),
                        instancePrerequisites = emptyReqs,
                        linkedTriggerAreaIDs = new List<int>(),
                        placeableContents = new DungeonPlaceable() {
                            width = 2,
                            height = 2,
                            respectsEncounterableDifferentiator = true,
                            variantTiers = new List<DungeonPlaceableVariant>() {
                                new DungeonPlaceableVariant() {
                                    percentChance = 1,
                                    nonDatabasePlaceable = asset,
                                    prerequisites = emptyReqs,
                                    materialRequirements= new DungeonPlaceableRoomMaterialRequirement[0]
                                }
                            }
                        }
                    });
                    //Tools.Print($"Added {asset.name} to room.");
                } else {
                    Tools.PrintError($"Unable to find asset in asset bundles: {assetPath}");
                }
            } catch (Exception e) {
                Tools.PrintException(e);
            }
        }

        public static void AddExit(PrototypeDungeonRoom room, Vector2 location, DungeonData.Direction direction) {
            if (room.exitData == null)
                room.exitData = new PrototypeRoomExitData();
            if (room.exitData.exits == null)
                room.exitData.exits = new List<PrototypeRoomExit>();

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
            room.paths = new List<SerializedPath>();
            room.prerequisites = new List<DungeonPrerequisite>();
            room.excludedOtherRooms = new List<PrototypeDungeonRoom>();
            room.rectangularFeatures = new List<PrototypeRectangularFeature>();
            room.exitData = new PrototypeRoomExitData();
            room.exitData.exits = new List<PrototypeRoomExit>();
            room.allowWallDecoration = false;
            room.allowFloorDecoration = false;
            room.Width = width;
            room.Height = height;
            return room;
        }

        /*public static void LogExampleRoomData() {
            Vector2[] vectorArray = new Vector2[] {
                new Vector2(4, 4),
                new Vector2(4, 14),
                new Vector2(14, 4),
                new Vector2(14, 14),
            };
            string[] guids = new string[] {
                "01972dee89fc4404a5c408d50007dad5",
                "7b0b1b6d9ce7405b86b75ce648025dd6",
                "ffdc8680bdaa487f8f31995539f74265",
                "01972dee89fc4404a5c408d50007dad5",
            };

            Vector2[] exits = new Vector2[] {
                new Vector2(0, 9),
                new Vector2(9, 0),
                new Vector2(20, 9),
                new Vector2(9, 20),
            };

            string[] dirs = new string[] { "EAST", "SOUTH", "WEST",  "NORTH" };

            RoomData rd = new RoomData() {
                enemyPositions = vectorArray,
                enemyGUIDs = guids,
                exitPositions = exits,
                exitDirections = dirs,

            };
            Tools.Print("Data to JSON: " + JsonUtility.ToJson(rd));
        }

        public static void StraightLine() {
            try {
                Vector2[] enemyPositions = new Vector2[100];
                string[] enemyGuids = new string[100];
                int[] enemyLayers = new int[100];
                for (int i = 0; i < enemyGuids.Length; i++) {
                    List<EnemyDatabaseEntry> db = EnemyDatabase.Instance.Entries;
                    int r = Random.Range(0, db.Count);
                    enemyGuids[i] = db[r].encounterGuid;
                    enemyPositions[i] = new Vector2(i * 2, 10);
                    enemyLayers[i] = 0;
                }

                Vector2[] exits = new Vector2[] { new Vector2(0, 9), new Vector2(200, 9), };

                string[] dirs = new string[] { "WEST", "EAST" };

                RoomData data = new RoomData() {
                    enemyPositions = enemyPositions,
                    enemyGUIDs = enemyGuids,
                    enemyReinforcementLayers = enemyLayers,
                    exitPositions = exits,
                    exitDirections = dirs,
                };
                Tools.Log("Data to JSON: " + JsonUtility.ToJson(data));
            } catch (Exception e) {
                Tools.PrintException(e);
            }
        }*/
    }
}

