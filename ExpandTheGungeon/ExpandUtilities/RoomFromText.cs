using Dungeonator;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandUtilities {

	public class RoomFromText {

        public static void GenerateDefaultRoomLayout(PrototypeDungeonRoom room, CellType DefaultCellType = CellType.FLOOR) {
            if (room == null) { return; }
            int width = room.Width;
            int height = room.Height;
            FieldInfo privateField = typeof(PrototypeDungeonRoom).GetField("m_cellData", BindingFlags.Instance | BindingFlags.NonPublic);
            PrototypeDungeonRoomCellData[] m_cellData = new PrototypeDungeonRoomCellData[width * height];

            CellType[,] cellData = new CellType[width, height];
            for (int Y = 0; Y < height; Y++) { for (int X = 0; X < width; X++) { cellData[X, Y] = DefaultCellType; } }

            for (int X = 0; X < width; X++) { for (int Y = 0; Y < height; Y++) { m_cellData[Y * width + X] = GenerateDefaultCellData(cellData[X, Y]); } }

            privateField.SetValue(room, m_cellData);
            room.UpdatePrecalculatedData();
        }

        public static void GenerateRoomFromText(PrototypeDungeonRoom room, string fileName) {
            string[] linesFromEmbeddedResource = ResourceExtractor.GetLinesFromEmbeddedResource(fileName);
            int width = room.Width;
            int height = room.Height;

            room.FullCellData = new PrototypeDungeonRoomCellData[width * height];

            CellType[,] cellData = new CellType[width, height];
            for (int Y = 0; Y < height; Y++) {
                string text = linesFromEmbeddedResource[Y];
                for (int X = 0; X < width; X++) {
                    char c = text[X];
                    // Corrects final row being off by one unit (read as first line in text file)
                    if (Y == 0 && X > 0) { c = text[X + 1]; }
                    if (c == '-') {
                        cellData[X, height - Y - 1] = CellType.FLOOR;
                    } else if (c == 'P') {
                        cellData[X, height - Y - 1] = CellType.PIT;
                    } else if (c == 'W') {
                        cellData[X, height - Y - 1] = CellType.WALL;
                    } else {
                        cellData[X, height - Y - 1] = CellType.FLOOR;
                    }
                }
            }

            // Set Final Cell (it was left null due to adjustment to fix final row)
            string text2 = linesFromEmbeddedResource[height - 1];
            char C = text2[width - 1];
            if (C == '-') {
                cellData[width - 1, height - 1] = CellType.FLOOR;
            } else if (C == 'P') {
                cellData[width - 1, height - 1] = CellType.PIT;
            } else if (C == 'W') {
                cellData[width - 1, height - 1] = CellType.WALL;
            } else {
                cellData[width - 1, height - 1] = CellType.FLOOR;
            }

            for (int X = 0; X < width; X++) {
                for (int Y = 0; Y < height; Y++) {
                    room.FullCellData[Y * width + X] = GenerateDefaultCellData(cellData[X, Y]);
                }
            }
                
            room.UpdatePrecalculatedData();
        }
        
        public static PrototypeDungeonRoomCellData GenerateDefaultCellData(CellType cellType, DiagonalWallType diagnalWallType = DiagonalWallType.NONE) {
            PrototypeDungeonRoomCellData m_NewCellData = new PrototypeDungeonRoomCellData(string.Empty, cellType) {
                state = cellType,
                diagonalWallType = diagnalWallType,
                breakable = false,
                str = string.Empty,
                conditionalOnParentExit = false,
                conditionalCellIsPit = false,
                parentExitIndex = 0,
                containsManuallyPlacedLight = false,
                lightPixelsOffsetY = 0,
                lightStampIndex = 0,
                doesDamage = false,
                damageDefinition = new CellDamageDefinition() {
                    damageTypes = CoreDamageTypes.None,
                    damageToPlayersPerTick = 0,
                    damageToEnemiesPerTick = 0,
                    tickFrequency = 0,
                    respectsFlying = false,
                    isPoison = false
                },
                appearance = new PrototypeDungeonRoomCellAppearance() {
                    overrideDungeonMaterialIndex = -1,
                    IsPhantomCarpet = false,
                    ForceDisallowGoop = false,
                    OverrideFloorType = CellVisualData.CellFloorType.Stone,
                    globalOverrideIndices = new PrototypeIndexOverrideData() { indices = new List<int>() },
                },
                ForceTileNonDecorated = false,            
            };
            return m_NewCellData;
        }
        
        public static void AddExitToRoom(PrototypeDungeonRoom room, Vector2 ExitLocation, DungeonData.Direction ExitDirection, PrototypeRoomExit.ExitType ExitType = PrototypeRoomExit.ExitType.NO_RESTRICTION, PrototypeRoomExit.ExitGroup ExitGroup = PrototypeRoomExit.ExitGroup.A, bool ContainsDoor = true, int ExitLength = 3, int exitSize = 2, DungeonPlaceable overrideDoorObject = null) {
            if (room == null) { return; }
            if (room.exitData == null) {
                room.exitData = new PrototypeRoomExitData();
                room.exitData.exits = new List<PrototypeRoomExit>();
            }
            if (room.exitData.exits == null) { room.exitData.exits = new List<PrototypeRoomExit>(); }
            PrototypeRoomExit m_NewExit = new PrototypeRoomExit(ExitDirection, ExitLocation) {
                exitDirection = ExitDirection,
                exitType = ExitType,
                exitGroup = ExitGroup,
                containsDoor = ContainsDoor,
                exitLength = ExitLength,
                containedCells = new List<Vector2>()
            };

            if (ExitDirection == DungeonData.Direction.WEST | ExitDirection == DungeonData.Direction.EAST) {
                if (exitSize > 2) {
                    m_NewExit.containedCells.Add(ExitLocation);
                    m_NewExit.containedCells.Add(ExitLocation + new Vector2(0, 1));
                    for (int i = 2; i < exitSize; i++) {
                        m_NewExit.containedCells.Add(ExitLocation + new Vector2(0, i));
                    }
                } else {
                    m_NewExit.containedCells.Add(ExitLocation);
                    m_NewExit.containedCells.Add(ExitLocation + new Vector2(0, 1));
                }
            } else {
                if (exitSize > 2) {
                    m_NewExit.containedCells.Add(ExitLocation);
                    m_NewExit.containedCells.Add(ExitLocation + new Vector2(1, 0));
                    for (int i = 2; i < exitSize; i++) {
                        m_NewExit.containedCells.Add(ExitLocation + new Vector2(i, 0));
                    }
                } else {
                    m_NewExit.containedCells.Add(ExitLocation);
                    m_NewExit.containedCells.Add(ExitLocation + new Vector2(1, 0));
                }
            }

            if (overrideDoorObject) { m_NewExit.specifiedDoor = overrideDoorObject; }

            room.exitData.exits.Add(m_NewExit);
        }

        public static void AddObjectToRoom(PrototypeDungeonRoom room, Vector2 position, DungeonPlaceable PlacableContents = null, DungeonPlaceableBehaviour NonEnemyBehaviour = null, string EnemyBehaviourGuid = null, float SpwanChance = 1f, int xOffset = 0, int yOffset = 0) {
            if (room == null) { return; }
            if (room.placedObjects == null) { room.placedObjects = new List<PrototypePlacedObjectData>(); }
            if (room.placedObjectPositions == null) { room.placedObjectPositions = new List<Vector2>(); }

            PrototypePlacedObjectData m_NewObjectData = new PrototypePlacedObjectData() {
                placeableContents = null,
                nonenemyBehaviour = null,
                spawnChance = SpwanChance,
                // unspecifiedContents = null,
                enemyBehaviourGuid = string.Empty,
                contentsBasePosition = position,
                layer = 0,
                xMPxOffset = xOffset,
                yMPxOffset = yOffset,
                fieldData = new List<PrototypePlacedObjectFieldData>(0),
                instancePrerequisites = new DungeonPrerequisite[0],
                linkedTriggerAreaIDs = new List<int>(0),
                assignedPathIDx = -1,
                assignedPathStartNode = 0
            };

            if (PlacableContents != null) {
                m_NewObjectData.placeableContents = PlacableContents;
            } else if (NonEnemyBehaviour != null) {
                m_NewObjectData.nonenemyBehaviour = NonEnemyBehaviour;
            } else if (EnemyBehaviourGuid != null) {
                m_NewObjectData.enemyBehaviourGuid = EnemyBehaviourGuid;
            } else {
                // All possible object fields were left null? Do nothing and return if this is the case.
                return;
            }

            room.placedObjects.Add(m_NewObjectData);
            room.placedObjectPositions.Add(position);
            return;
        }

    }
}

