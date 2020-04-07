﻿using Dungeonator;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandUtilities {

	public class RoomBuilder {

        public static string TextureBasePath = "Textures\\RoomLayoutData\\";
        
        public static void GenerateRoomLayoutFromPNG(PrototypeDungeonRoom room, string filePath, PrototypeRoomPitEntry.PitBorderType PitBorderType = PrototypeRoomPitEntry.PitBorderType.FLAT, CoreDamageTypes DamageCellsType = CoreDamageTypes.None) {
            Texture2D m_TextureFromResource = ResourceExtractor.GetTextureFromResource(TextureBasePath + filePath);
            
            float DamageToPlayersPerTick = 0;
            float DamageToEnemiesPerTick = 0;
            float TickFrequency = 0;
            bool RespectsFlying = true;
            bool DamageCellsArePoison = false;

            if (DamageCellsType == CoreDamageTypes.Fire) {
                DamageToPlayersPerTick = 0.5f;
                TickFrequency = 1;
            } else if (DamageCellsType == CoreDamageTypes.Poison) {
                DamageCellsArePoison = true;
                DamageToPlayersPerTick = 0.5f;
                TickFrequency = 1;
            }

            if (m_TextureFromResource == null) {
                ETGModConsole.Log("[ExpandTheGungeon] GenerateRoomFromImage: Error! Requested Texture Resource is Null!");
                return;
            }
                                    
            Color WhitePixel = new Color32(255, 255, 255, 255); // Wall Cell
            Color PinkPixel = new Color32(255, 0, 255, 255); // Diagonal Wall Cell (North East)
            Color YellowPixel = new Color32(255, 255, 0, 255); // Diagonal Wall Cell (North West)
            Color HalfPinkPixel = new Color32(127, 0, 127, 255); // Diagonal Wall Cell (South East)
            Color HalfYellowPixel = new Color32(127, 127, 0, 255); // Diagonal Wall Cell (South West)
            
            Color BluePixel = new Color32(0, 0, 255, 255); // Floor Cell

            Color BlueHalfGreenPixel = new Color32(0, 127, 255, 255); // Floor Cell (Ice Override)
            Color HalfBluePixel = new Color32(0, 0, 127, 255); // Floor Cell (Water Override)
            Color HalfRedPixel = new Color32(0, 0, 127, 255); // Floor Cell (Carpet Override)
            Color GreenHalfRBPixel = new Color32(127, 255, 127, 255); // Floor Cell (Grass Override)
            Color HalfWhitePixel = new Color32(127, 127, 127, 255); // Floor Cell (Bone Override)
            Color OrangePixel = new Color32(255, 127, 0, 255); // Floor Cell (Flesh Override)
            Color RedHalfGBPixel = new Color32(255, 127, 127, 255); // Floor Cell (ThickGoop Override)

            Color GreenPixel = new Color32(0, 255, 0, 255); // Damage Floor Cell

            Color RedPixel = new Color32(255, 0, 0, 255); // Pit Cell

            int width = room.Width;
            int height = room.Height;
            int ArrayLength = (width * height);

            if (m_TextureFromResource.GetPixels32().Length != ArrayLength) {
                ETGModConsole.Log("[ExpandTheGungeon] GenerateRoomFromImage: Error! Image resolution doesn't match size of room!");
                return;
            }

            room.FullCellData = new PrototypeDungeonRoomCellData[ArrayLength];                        
            List<Vector2> m_Pits = new List<Vector2>();
            
            for (int X = 0; X < width; X++) {
                for (int Y = 0; Y < height; Y++) {
                    int ArrayPosition = (Y * width + X);
                    Color? m_Pixel = m_TextureFromResource.GetPixel(X, Y);
                    CellType cellType = CellType.FLOOR;
                    DiagonalWallType diagonalWallType = DiagonalWallType.NONE;
                    CellVisualData.CellFloorType OverrideFloorType = CellVisualData.CellFloorType.Stone;
                    bool isDamageCell = false;
                    bool cellDamagesPlayer = false;
                    if (m_Pixel.HasValue) {
                        if (m_Pixel.Value == WhitePixel | m_Pixel.Value == PinkPixel | 
                            m_Pixel.Value == YellowPixel | m_Pixel.Value == HalfPinkPixel | 
                            m_Pixel.Value == HalfYellowPixel)
                        {
                            cellType = CellType.WALL;
                            if (m_Pixel.Value == PinkPixel) {
                                diagonalWallType = DiagonalWallType.NORTHEAST;
                            } else if (m_Pixel.Value == YellowPixel) {
                                diagonalWallType = DiagonalWallType.NORTHWEST;
                            } else if (m_Pixel.Value == HalfPinkPixel) {
                                diagonalWallType = DiagonalWallType.SOUTHEAST;
                            } else if (m_Pixel.Value == HalfYellowPixel) {
                                diagonalWallType = DiagonalWallType.SOUTHWEST;
                            }
                        } else if (m_Pixel.Value == RedPixel) {
                            cellType = CellType.PIT;
                            m_Pits.Add(new Vector2(X, Y));
                        } else if (m_Pixel.Value == BluePixel | m_Pixel.Value == GreenPixel | 
                            m_Pixel.Value == BlueHalfGreenPixel | m_Pixel.Value == HalfBluePixel |
                            m_Pixel.Value == HalfRedPixel | m_Pixel.Value == GreenHalfRBPixel |
                            m_Pixel.Value == HalfWhitePixel | m_Pixel.Value == OrangePixel |
                            m_Pixel.Value == RedHalfGBPixel)
                        {
                            cellType = CellType.FLOOR;
                            if (m_Pixel.Value == GreenPixel) {
                                isDamageCell = true;
                                if (DamageCellsType == CoreDamageTypes.Ice) {
                                    cellDamagesPlayer = false;
                                } else {
                                    cellDamagesPlayer = true;
                                }
                            } else if (m_Pixel.Value == BlueHalfGreenPixel) {
                                OverrideFloorType = CellVisualData.CellFloorType.Ice;
                            } else if (m_Pixel.Value == HalfBluePixel) {
                                OverrideFloorType = CellVisualData.CellFloorType.Water;
                            } else if (m_Pixel.Value == HalfRedPixel) {
                                OverrideFloorType = CellVisualData.CellFloorType.Carpet;
                            } else if (m_Pixel.Value == GreenHalfRBPixel) {
                                OverrideFloorType = CellVisualData.CellFloorType.Grass;
                            } else if (m_Pixel.Value == HalfWhitePixel) {
                                OverrideFloorType = CellVisualData.CellFloorType.Bone;
                            } else if (m_Pixel.Value == OrangePixel) {
                                OverrideFloorType = CellVisualData.CellFloorType.Flesh;
                            } else if (m_Pixel.Value == RedHalfGBPixel) {
                                OverrideFloorType = CellVisualData.CellFloorType.ThickGoop;
                            }
                        } else {
                            cellType = CellType.FLOOR;
                        }
                    } else {
                        cellType = CellType.FLOOR;
                    }
                    if (DamageCellsType != CoreDamageTypes.None && isDamageCell) {
                        room.FullCellData[ArrayPosition] = GenerateCellData(cellType, diagonalWallType, cellDamagesPlayer, DamageCellsArePoison, DamageCellsType, DamageToPlayersPerTick, DamageToEnemiesPerTick, TickFrequency, RespectsFlying);
                    } else {
                        room.FullCellData[ArrayPosition] = GenerateCellData(cellType, diagonalWallType, OverrideFloorType: OverrideFloorType);
                    }
                }
            }

            if (m_Pits.Count > 0) {
                room.pits = new List<PrototypeRoomPitEntry>() {
                    new PrototypeRoomPitEntry(m_Pits) {
                        containedCells = m_Pits,
                        borderType = PitBorderType
                    }
                };
            }
            room.OnBeforeSerialize();
            room.OnAfterDeserialize();
            room.UpdatePrecalculatedData();
        }

        public static void GenerateBasicRoomLayout(PrototypeDungeonRoom room, CellType DefaultCellType = CellType.FLOOR, PrototypeRoomPitEntry.PitBorderType pitBorderType = PrototypeRoomPitEntry.PitBorderType.FLAT) {
            int width = room.Width;
            int height = room.Height;
            int ArrayLength = (width * height);

            room.FullCellData = new PrototypeDungeonRoomCellData[ArrayLength];
            List<Vector2> m_Pits = new List<Vector2>();

            for (int X = 0; X < width; X++) {
                for (int Y = 0; Y < height; Y++) {
                    int ArrayPosition = (Y * width + X);
                    room.FullCellData[ArrayPosition] = GenerateCellData(DefaultCellType);
                    if (DefaultCellType == CellType.PIT) { m_Pits.Add(new Vector2(X, Y)); }
                }
            }

            if (m_Pits.Count > 0) {
                room.pits = new List<PrototypeRoomPitEntry>() {
                    new PrototypeRoomPitEntry(m_Pits) {
                        containedCells = m_Pits,
                        borderType = pitBorderType
                    }
                };
            }

            room.OnBeforeSerialize();
            room.UpdatePrecalculatedData();
        }
                
        public static PrototypeDungeonRoomCellData GenerateCellData(CellType cellType, DiagonalWallType diagnalWallType = DiagonalWallType.NONE, bool DoesDamage = false, bool IsPoison = false, CoreDamageTypes DamageType = CoreDamageTypes.None, float DamageToPlayersPerTick = 0, float DamageToEnemiesPerTick = 0, float TickFrequency = 0, bool RespectsFlying = true, CellVisualData.CellFloorType OverrideFloorType = CellVisualData.CellFloorType.Stone) {
            PrototypeDungeonRoomCellData m_NewCellData = new PrototypeDungeonRoomCellData(string.Empty, cellType) {
                state = cellType,
                diagonalWallType = diagnalWallType,
                breakable = false,
                str = string.Empty,
                conditionalOnParentExit = false,
                conditionalCellIsPit = false,
                parentExitIndex = -1,
                containsManuallyPlacedLight = false,
                lightPixelsOffsetY = 0,
                lightStampIndex = 0,
                doesDamage = DoesDamage,
                damageDefinition = new CellDamageDefinition() {
                    damageTypes = DamageType,
                    damageToPlayersPerTick = DamageToEnemiesPerTick,
                    damageToEnemiesPerTick = DamageToEnemiesPerTick,
                    tickFrequency = TickFrequency,
                    respectsFlying = RespectsFlying,
                    isPoison = IsPoison
                },
                appearance = new PrototypeDungeonRoomCellAppearance() {
                    overrideDungeonMaterialIndex = -1,
                    IsPhantomCarpet = false,
                    ForceDisallowGoop = false,
                    OverrideFloorType = OverrideFloorType,
                    globalOverrideIndices = new PrototypeIndexOverrideData() { indices = new List<int>(0) }
                },
                ForceTileNonDecorated = false,
                additionalPlacedObjectIndices = new List<int>() { -1 },
                placedObjectRUBELIndex = -1
            };

            if (DamageType == CoreDamageTypes.Poison) {
                m_NewCellData.ForceTileNonDecorated = true;
                m_NewCellData.appearance.OverrideFloorType  = CellVisualData.CellFloorType.Stone;
                m_NewCellData.damageDefinition.damageTypes = CoreDamageTypes.Poison;
            } else if (DamageType == CoreDamageTypes.Fire) {
                m_NewCellData.ForceTileNonDecorated = true;
                m_NewCellData.appearance.OverrideFloorType = CellVisualData.CellFloorType.Stone;
                m_NewCellData.damageDefinition.damageTypes = CoreDamageTypes.Fire;
            }

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

        public static void AddObjectToRoom(PrototypeDungeonRoom room, Vector2 position, DungeonPlaceable PlacableContents = null, DungeonPlaceableBehaviour NonEnemyBehaviour = null, string EnemyBehaviourGuid = null, float SpawnChance = 1f, int xOffset = 0, int yOffset = 0) {
            if (room == null) { return; }
            if (room.placedObjects == null) { room.placedObjects = new List<PrototypePlacedObjectData>(); }
            if (room.placedObjectPositions == null) { room.placedObjectPositions = new List<Vector2>(); }

            PrototypePlacedObjectData m_NewObjectData = new PrototypePlacedObjectData() {
                placeableContents = null,
                nonenemyBehaviour = null,
                spawnChance = SpawnChance,
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

