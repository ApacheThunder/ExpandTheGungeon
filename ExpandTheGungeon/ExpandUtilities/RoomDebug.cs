using Dungeonator;
using System;
using System.IO;
using UnityEngine;

namespace ExpandTheGungeon.ExpandUtilities {

    public class RoomDebug {

        public static void DumpCurrentRoomLayout(PrototypeDungeonRoom overrideRoom = null, RoomHandler generatedRoomHandler = null) {
            try {
                if (overrideRoom != null) {
                    LogRoomToPNGFile(overrideRoom);
                    ETGModConsole.Log("Succesfully saved room layout to PNG.", false);
                    return;
                } else if (generatedRoomHandler != null) {
                    LogRoomHandlerToPNGFile(generatedRoomHandler);
                    ETGModConsole.Log("Succesfully saved room layout to PNG.", false);
                    return;
                }
                RoomHandler currentRoom = GameManager.Instance.PrimaryPlayer.CurrentRoom;
                int CurrentFloor = GameManager.Instance.CurrentFloor;
                Dungeon dungeon = null;
                if (CurrentFloor == 1) { dungeon = DungeonDatabase.GetOrLoadByName("Base_Castle"); }
                if (CurrentFloor == 2) { dungeon = DungeonDatabase.GetOrLoadByName("Base_Gungeon"); }
                if (CurrentFloor == 3) { dungeon = DungeonDatabase.GetOrLoadByName("Base_Mines"); }
                if (CurrentFloor == 4) { dungeon = DungeonDatabase.GetOrLoadByName("Base_Catacombs"); }
                if (CurrentFloor == 5) { dungeon = DungeonDatabase.GetOrLoadByName("Base_Forge"); }
                if (CurrentFloor == 6) { dungeon = DungeonDatabase.GetOrLoadByName("Base_BulletHell"); }
                if (CurrentFloor == -1) {
                    if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                        dungeon = DungeonDatabase.GetOrLoadByName("Base_Castle");
                    }
                    if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.SEWERGEON) {
                        dungeon = DungeonDatabase.GetOrLoadByName("Base_Sewer");
                    }
                    if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.GUNGEON) {
                        dungeon = DungeonDatabase.GetOrLoadByName("Base_Gungeon");
                    }
                    if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATHEDRALGEON) {
                        dungeon = DungeonDatabase.GetOrLoadByName("Base_Cathedral");
                    }
                    if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.MINEGEON) {
                        dungeon = DungeonDatabase.GetOrLoadByName("Base_Mines");
                    }
                    if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.RATGEON) {
                        dungeon = DungeonDatabase.GetOrLoadByName("Base_ResourcefulRat");
                    }
                    if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATACOMBGEON) {
                        dungeon = DungeonDatabase.GetOrLoadByName("Base_Catacombs");
                    }
                    if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON) {
                        dungeon = DungeonDatabase.GetOrLoadByName("Base_Nakatomi");
                    }
                    if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                        dungeon = DungeonDatabase.GetOrLoadByName("Base_Forge");
                    }
                    if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.HELLGEON) {
                        dungeon = DungeonDatabase.GetOrLoadByName("Base_BulletHell");
                    }
                }
                if (dungeon == null) {
                    ETGModConsole.Log("Could not find current Dungeon prefab for current floor!\n Attempting to use local dungeon data area of room instead.", false);
                    LogRoomHandlerToPNGFile(currentRoom);
                    dungeon = null;
                    return;
                }
                if (dungeon.PatternSettings.flows != null) {
                    if (dungeon.PatternSettings.flows[0].fallbackRoomTable != null) {
                        if (dungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements != null) {
                            if (dungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements.Count > 0) {
                                for (int i = 0; i < dungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements.Count; i++) {
                                    if (dungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements[i].room != null) {
                                        if (currentRoom.GetRoomName().ToLower().StartsWith(dungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements[i].room.name.ToLower())) {
                                            LogRoomToPNGFile(dungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements[i].room);
                                            ETGModConsole.Log("Succesfully saved current room layout to PNG.", false);
                                            dungeon = null;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
                if (dungeon.PatternSettings.flows != null && dungeon.PatternSettings.flows[0].fallbackRoomTable == null) {
                    for (int i = 0; i < dungeon.PatternSettings.flows[0].AllNodes.Count; i++) {
                        if (dungeon.PatternSettings.flows[0].AllNodes[i].overrideExactRoom != null) {
                            if (currentRoom.GetRoomName().ToLower().StartsWith(dungeon.PatternSettings.flows[0].AllNodes[i].overrideExactRoom.name.ToLower())) {
                                LogRoomToPNGFile(dungeon.PatternSettings.flows[0].AllNodes[i].overrideExactRoom);
                                ETGModConsole.Log("Succesfully saved current room layout to PNG.", false);
                                dungeon = null;
                                return;
                            }
                        }
                    }
                }

                ETGModConsole.Log("Current Room's ProtoTypeDungeonRoom prefab not found!\nAttempting to use local DungeonData of RoomHandler object instead...", false);
                LogRoomHandlerToPNGFile(currentRoom);
                dungeon = null;
                ETGModConsole.Log("Succesfully saved current room layout to PNG.", false);
                
            } catch (Exception ex) {
            	ETGModConsole.Log("Failed to save room layout!", false);
            	ETGModConsole.Log("    " + ex.Message, false);
                Debug.LogException(ex);
            }

        }

        public static void LogRoomToPNGFile(PrototypeDungeonRoom room) {
            int width = room.Width;
            int height = room.Height;
            
            Texture2D m_NewImage = new Texture2D(width, height, TextureFormat.RGBA32, false);
            if (!string.IsNullOrEmpty(room.name)) { m_NewImage.name = room.name; }
            
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

            Color BlackPixel = new Color32(0, 0, 0, 255); // NULL Cell

            for (int X = 0; X < width; X++) {
                for (int Y = 0; Y < height; Y++) {
                    CellType? cellData = room.GetCellDataAtPoint(X, Y).state;
                    bool DamageCell = false;
                    DiagonalWallType diagonalWallType = DiagonalWallType.NONE;                    
                    if (room.GetCellDataAtPoint(X, Y) != null && cellData.HasValue) {
                        DamageCell = room.GetCellDataAtPoint(X, Y).doesDamage;
                        diagonalWallType = room.GetCellDataAtPoint(X, Y).diagonalWallType;
                    }
                    if (room.GetCellDataAtPoint(X, Y) == null | !cellData.HasValue) {
                        m_NewImage.SetPixel(X, Y, BlackPixel);
                    } else if (cellData.Value == CellType.FLOOR) {
                        if (DamageCell) {
                            m_NewImage.SetPixel(X, Y, GreenPixel);
                        } else if (room.GetCellDataAtPoint(X, Y).appearance != null) {
                            CellVisualData.CellFloorType overrideFloorType = room.GetCellDataAtPoint(X, Y).appearance.OverrideFloorType;
                            if (overrideFloorType == CellVisualData.CellFloorType.Stone) {
                                m_NewImage.SetPixel(X, Y, BluePixel);
                            } else if (overrideFloorType == CellVisualData.CellFloorType.Ice) {
                                m_NewImage.SetPixel(X, Y, BlueHalfGreenPixel);
                            } else if (overrideFloorType == CellVisualData.CellFloorType.Water) {
                                m_NewImage.SetPixel(X, Y, HalfBluePixel);
                            } else if (overrideFloorType == CellVisualData.CellFloorType.Carpet) {
                                m_NewImage.SetPixel(X, Y, HalfRedPixel);
                            } else if (overrideFloorType == CellVisualData.CellFloorType.Grass) {
                                m_NewImage.SetPixel(X, Y, GreenHalfRBPixel);
                            } else if (overrideFloorType == CellVisualData.CellFloorType.Bone) {
                                m_NewImage.SetPixel(X, Y, HalfWhitePixel);
                            } else if (overrideFloorType == CellVisualData.CellFloorType.Flesh) {
                                m_NewImage.SetPixel(X, Y, OrangePixel);
                            } else if (overrideFloorType == CellVisualData.CellFloorType.ThickGoop) {
                                m_NewImage.SetPixel(X, Y, RedHalfGBPixel);
                            } else {
                                m_NewImage.SetPixel(X, Y, BluePixel);
                            }
                        } else {
                            m_NewImage.SetPixel(X, Y, BluePixel);
                        }
                    } else if (cellData.Value == CellType.WALL) {
                        if (diagonalWallType == DiagonalWallType.NORTHEAST) {
                            m_NewImage.SetPixel(X, Y, PinkPixel);
                        } else if (diagonalWallType == DiagonalWallType.NORTHWEST) {
                            m_NewImage.SetPixel(X, Y, YellowPixel);
                        } else if (diagonalWallType == DiagonalWallType.SOUTHEAST) {
                            m_NewImage.SetPixel(X, Y, HalfPinkPixel);
                        } else if (diagonalWallType == DiagonalWallType.SOUTHWEST) {
                            m_NewImage.SetPixel(X, Y, HalfYellowPixel);
                        } else {
                            m_NewImage.SetPixel(X, Y, WhitePixel);
                        }
                    } else if (cellData.Value == CellType.PIT) {
                        m_NewImage.SetPixel(X, Y, RedPixel);
                    }
                }
            }

            m_NewImage.Apply();

            string basePath = "DumpedRoomLayouts/";

            string fileName = (basePath + m_NewImage.name);
            if (string.IsNullOrEmpty(m_NewImage.name)) { fileName += ("RoomLayout_" + Guid.NewGuid().ToString()); }

            fileName += "_Layout";

            string path = Path.Combine(ETGMod.ResourcesDirectory, fileName.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");

            if (!File.Exists(path)) { Directory.GetParent(path).Create(); }

            File.WriteAllBytes(path, ImageConversion.EncodeToPNG(m_NewImage));
        }
        
        public static void LogRoomHandlerToPNGFile(RoomHandler room) {
            int width = room.area.dimensions.x;
            int height = room.area.dimensions.y;
            IntVector2 basePosition = room.area.basePosition;
            
            Texture2D m_NewImage = new Texture2D(width, height, TextureFormat.RGBA32, false);
            if (!string.IsNullOrEmpty(room.GetRoomName())) { m_NewImage.name = room.GetRoomName(); }
            
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

            Color BlackPixel = new Color32(0, 0, 0, 255); // NULL Cell

            for (int X = 0; X < width; X++) {
                for (int Y = 0; Y < height; Y++) {
                    IntVector2 m_CellPosition = (new IntVector2(X, Y) + basePosition);
                    if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(m_CellPosition.x, m_CellPosition.y)) {                         
                        CellType? cellData = GameManager.Instance.Dungeon.data[m_CellPosition].type;
                        CellData localDungeonData = GameManager.Instance.Dungeon.data[m_CellPosition];
                        bool DamageCell = false;
                        DiagonalWallType diagonalWallType = DiagonalWallType.NONE;                    
                        if (localDungeonData != null) {
                            DamageCell = localDungeonData.doesDamage;
                            diagonalWallType = localDungeonData.diagonalWallType;
                        }
                        if (localDungeonData == null | !cellData.HasValue) {
                            m_NewImage.SetPixel(X, Y, BlackPixel);
                        } else if (cellData.Value == CellType.FLOOR) {
                            if (DamageCell) {
                                m_NewImage.SetPixel(X, Y, GreenPixel);
                            } else {
                                CellVisualData.CellFloorType overrideFloorType = localDungeonData.cellVisualData.floorType;
                                if (overrideFloorType == CellVisualData.CellFloorType.Stone) {
                                    m_NewImage.SetPixel(X, Y, BluePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Ice) {
                                    m_NewImage.SetPixel(X, Y, BlueHalfGreenPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Water) {
                                    m_NewImage.SetPixel(X, Y, HalfBluePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Carpet) {
                                    m_NewImage.SetPixel(X, Y, HalfRedPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Grass) {
                                    m_NewImage.SetPixel(X, Y, GreenHalfRBPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Bone) {
                                    m_NewImage.SetPixel(X, Y, HalfWhitePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Flesh) {
                                    m_NewImage.SetPixel(X, Y, OrangePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.ThickGoop) {
                                    m_NewImage.SetPixel(X, Y, RedHalfGBPixel);
                                } else {
                                    m_NewImage.SetPixel(X, Y, BluePixel);
                                }
                            }
                        } else if (cellData.Value == CellType.WALL) {
                            if (diagonalWallType == DiagonalWallType.NORTHEAST) {
                                m_NewImage.SetPixel(X, Y, PinkPixel);
                            } else if (diagonalWallType == DiagonalWallType.NORTHWEST) {
                                m_NewImage.SetPixel(X, Y, YellowPixel);
                            } else if (diagonalWallType == DiagonalWallType.SOUTHEAST) {
                                m_NewImage.SetPixel(X, Y, HalfPinkPixel);
                            } else if (diagonalWallType == DiagonalWallType.SOUTHWEST) {
                                m_NewImage.SetPixel(X, Y, HalfYellowPixel);
                            } else {
                                m_NewImage.SetPixel(X, Y, WhitePixel);
                            }
                        } else if (cellData.Value == CellType.PIT) {
                            m_NewImage.SetPixel(X, Y, RedPixel);
                        }
                    } else {
                        m_NewImage.SetPixel(X, Y, BlackPixel);
                    }
                }
            }

            m_NewImage.Apply();

            string basePath = "DumpedRoomLayouts/";

            string fileName = (basePath + m_NewImage.name);
            if (string.IsNullOrEmpty(m_NewImage.name)) { fileName += ("RoomLayout_" + Guid.NewGuid().ToString()); }

            fileName += "_Layout";

            string path = Path.Combine(ETGMod.ResourcesDirectory, fileName.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");

            if (!File.Exists(path)) { Directory.GetParent(path).Create(); }

            File.WriteAllBytes(path, ImageConversion.EncodeToPNG(m_NewImage));
        }

        public static void LogDungeonToPNGFile() {
            // int width = GameManager.Instance.Dungeon.data.Height;
            // int height = GameManager.Instance.Dungeon.data.Width;
            int width = 1000;
            int height = 1000;

            Texture2D m_NewImage = new Texture2D(width, height, TextureFormat.RGBA32, false);
            m_NewImage.name = GameManager.Instance.Dungeon.gameObject.name;
            
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

            Color BlackPixel = new Color32(0, 0, 0, 255); // NULL Cell

            for (int X = 0; X < width; X++) {
                for (int Y = 0; Y < height; Y++) {
                    if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(X, Y)) { 
                        IntVector2 m_CellPosition = new IntVector2(X, Y);
                        CellType? cellData = GameManager.Instance.Dungeon.data[m_CellPosition].type;
                        CellData localDungeonData = GameManager.Instance.Dungeon.data[m_CellPosition];
                        bool DamageCell = false;
                        DiagonalWallType diagonalWallType = DiagonalWallType.NONE;                    
                        if (localDungeonData != null) {
                            DamageCell = localDungeonData.doesDamage;
                            diagonalWallType = localDungeonData.diagonalWallType;
                        }
                        if (localDungeonData == null | !cellData.HasValue) {
                            m_NewImage.SetPixel(X, Y, BlackPixel);
                        } else if (cellData.Value == CellType.FLOOR) {
                            if (DamageCell) {
                                m_NewImage.SetPixel(X, Y, GreenPixel);
                            } else {
                                CellVisualData.CellFloorType overrideFloorType = localDungeonData.cellVisualData.floorType;
                                if (overrideFloorType == CellVisualData.CellFloorType.Stone) {
                                    m_NewImage.SetPixel(X, Y, BluePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Ice) {
                                    m_NewImage.SetPixel(X, Y, BlueHalfGreenPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Water) {
                                    m_NewImage.SetPixel(X, Y, HalfBluePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Carpet) {
                                    m_NewImage.SetPixel(X, Y, HalfRedPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Grass) {
                                    m_NewImage.SetPixel(X, Y, GreenHalfRBPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Bone) {
                                    m_NewImage.SetPixel(X, Y, HalfWhitePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Flesh) {
                                    m_NewImage.SetPixel(X, Y, OrangePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.ThickGoop) {
                                    m_NewImage.SetPixel(X, Y, RedHalfGBPixel);
                                } else {
                                    m_NewImage.SetPixel(X, Y, BluePixel);
                                }
                            }
                        } else if (cellData.Value == CellType.WALL) {
                            if (diagonalWallType == DiagonalWallType.NORTHEAST) {
                                m_NewImage.SetPixel(X, Y, PinkPixel);
                            } else if (diagonalWallType == DiagonalWallType.NORTHWEST) {
                                m_NewImage.SetPixel(X, Y, YellowPixel);
                            } else if (diagonalWallType == DiagonalWallType.SOUTHEAST) {
                                m_NewImage.SetPixel(X, Y, HalfPinkPixel);
                            } else if (diagonalWallType == DiagonalWallType.SOUTHWEST) {
                                m_NewImage.SetPixel(X, Y, HalfYellowPixel);
                            } else {
                                m_NewImage.SetPixel(X, Y, WhitePixel);
                            }
                        } else if (cellData.Value == CellType.PIT) {
                            m_NewImage.SetPixel(X, Y, RedPixel);
                        }
                    } else {
                        m_NewImage.SetPixel(X, Y, BlackPixel);
                    }
                }
            }

            m_NewImage.Apply();

            string basePath = "DumpedDungeonLayouts/";

            string fileName = (basePath + m_NewImage.name);
            if (string.IsNullOrEmpty(m_NewImage.name)) { fileName += ("DungeonLayout_" + Guid.NewGuid().ToString()); }

            fileName += "_Layout";

            string path = Path.Combine(ETGMod.ResourcesDirectory, fileName.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");

            if (!File.Exists(path)) { Directory.GetParent(path).Create(); }

            File.WriteAllBytes(path, ImageConversion.EncodeToPNG(m_NewImage));
        }

        public static Texture2D DumpRoomAreaToTexture2D(RoomHandler room) {
            int width = room.area.dimensions.x;
            int height = room.area.dimensions.y;
            IntVector2 basePosition = room.area.basePosition;
            
            Texture2D m_NewImage = new Texture2D(width, height, TextureFormat.RGBA32, false);
            if (!string.IsNullOrEmpty(room.GetRoomName())) { m_NewImage.name = room.GetRoomName(); }
            
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

            Color BlackPixel = new Color32(0, 0, 0, 255); // NULL Cell

            for (int X = 0; X < width; X++) {
                for (int Y = 0; Y < height; Y++) {
                    IntVector2 m_CellPosition = (new IntVector2(X, Y) + basePosition);
                    if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(m_CellPosition.x, m_CellPosition.y)) {                         
                        CellType? cellData = GameManager.Instance.Dungeon.data[m_CellPosition].type;
                        CellData localDungeonData = GameManager.Instance.Dungeon.data[m_CellPosition];
                        bool DamageCell = false;
                        DiagonalWallType diagonalWallType = DiagonalWallType.NONE;                    
                        if (localDungeonData != null) {
                            DamageCell = localDungeonData.doesDamage;
                            diagonalWallType = localDungeonData.diagonalWallType;
                        }
                        if (localDungeonData == null | !cellData.HasValue) {
                            m_NewImage.SetPixel(X, Y, BlackPixel);
                        } else if (cellData.Value == CellType.FLOOR) {
                            if (DamageCell) {
                                m_NewImage.SetPixel(X, Y, GreenPixel);
                            } else {
                                CellVisualData.CellFloorType overrideFloorType = localDungeonData.cellVisualData.floorType;
                                if (overrideFloorType == CellVisualData.CellFloorType.Stone) {
                                    m_NewImage.SetPixel(X, Y, BluePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Ice) {
                                    m_NewImage.SetPixel(X, Y, BlueHalfGreenPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Water) {
                                    m_NewImage.SetPixel(X, Y, HalfBluePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Carpet) {
                                    m_NewImage.SetPixel(X, Y, HalfRedPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Grass) {
                                    m_NewImage.SetPixel(X, Y, GreenHalfRBPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Bone) {
                                    m_NewImage.SetPixel(X, Y, HalfWhitePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Flesh) {
                                    m_NewImage.SetPixel(X, Y, OrangePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.ThickGoop) {
                                    m_NewImage.SetPixel(X, Y, RedHalfGBPixel);
                                } else {
                                    m_NewImage.SetPixel(X, Y, BluePixel);
                                }
                            }
                        } else if (cellData.Value == CellType.WALL) {
                            if (diagonalWallType == DiagonalWallType.NORTHEAST) {
                                m_NewImage.SetPixel(X, Y, PinkPixel);
                            } else if (diagonalWallType == DiagonalWallType.NORTHWEST) {
                                m_NewImage.SetPixel(X, Y, YellowPixel);
                            } else if (diagonalWallType == DiagonalWallType.SOUTHEAST) {
                                m_NewImage.SetPixel(X, Y, HalfPinkPixel);
                            } else if (diagonalWallType == DiagonalWallType.SOUTHWEST) {
                                m_NewImage.SetPixel(X, Y, HalfYellowPixel);
                            } else {
                                m_NewImage.SetPixel(X, Y, WhitePixel);
                            }
                        } else if (cellData.Value == CellType.PIT) {
                            m_NewImage.SetPixel(X, Y, RedPixel);
                        }
                    } else {
                        m_NewImage.SetPixel(X, Y, BlackPixel);
                    }
                }
            }

            m_NewImage.Apply();

            return m_NewImage;
        }


        public static Texture2D LogDungeonToTexture2D() {
            int width = GameManager.Instance.Dungeon.data.Height;
            int height = GameManager.Instance.Dungeon.data.Width;
            // int width = 1000;
            // int height = 1000;

            Texture2D m_NewImage = new Texture2D(width, height, TextureFormat.RGBA32, false);
            m_NewImage.name = GameManager.Instance.Dungeon.gameObject.name;
            
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

            Color BlackPixel = new Color32(0, 0, 0, 255); // NULL Cell

            for (int X = 0; X < width; X++) {
                for (int Y = 0; Y < height; Y++) {
                    if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(X, Y)) { 
                        IntVector2 m_CellPosition = new IntVector2(X, Y);
                        CellType? cellData = GameManager.Instance.Dungeon.data[m_CellPosition].type;
                        CellData localDungeonData = GameManager.Instance.Dungeon.data[m_CellPosition];
                        bool DamageCell = false;
                        DiagonalWallType diagonalWallType = DiagonalWallType.NONE;                    
                        if (localDungeonData != null) {
                            DamageCell = localDungeonData.doesDamage;
                            diagonalWallType = localDungeonData.diagonalWallType;
                        }
                        if (localDungeonData == null | !cellData.HasValue) {
                            m_NewImage.SetPixel(X, Y, BlackPixel);
                        } else if (cellData.Value == CellType.FLOOR) {
                            if (DamageCell) {
                                m_NewImage.SetPixel(X, Y, GreenPixel);
                            } else {
                                CellVisualData.CellFloorType overrideFloorType = localDungeonData.cellVisualData.floorType;
                                if (overrideFloorType == CellVisualData.CellFloorType.Stone) {
                                    m_NewImage.SetPixel(X, Y, BluePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Ice) {
                                    m_NewImage.SetPixel(X, Y, BlueHalfGreenPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Water) {
                                    m_NewImage.SetPixel(X, Y, HalfBluePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Carpet) {
                                    m_NewImage.SetPixel(X, Y, HalfRedPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Grass) {
                                    m_NewImage.SetPixel(X, Y, GreenHalfRBPixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Bone) {
                                    m_NewImage.SetPixel(X, Y, HalfWhitePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.Flesh) {
                                    m_NewImage.SetPixel(X, Y, OrangePixel);
                                } else if (overrideFloorType == CellVisualData.CellFloorType.ThickGoop) {
                                    m_NewImage.SetPixel(X, Y, RedHalfGBPixel);
                                } else {
                                    m_NewImage.SetPixel(X, Y, BluePixel);
                                }
                            }
                        } else if (cellData.Value == CellType.WALL) {
                            if (diagonalWallType == DiagonalWallType.NORTHEAST) {
                                m_NewImage.SetPixel(X, Y, PinkPixel);
                            } else if (diagonalWallType == DiagonalWallType.NORTHWEST) {
                                m_NewImage.SetPixel(X, Y, YellowPixel);
                            } else if (diagonalWallType == DiagonalWallType.SOUTHEAST) {
                                m_NewImage.SetPixel(X, Y, HalfPinkPixel);
                            } else if (diagonalWallType == DiagonalWallType.SOUTHWEST) {
                                m_NewImage.SetPixel(X, Y, HalfYellowPixel);
                            } else {
                                m_NewImage.SetPixel(X, Y, WhitePixel);
                            }
                        } else if (cellData.Value == CellType.PIT) {
                            m_NewImage.SetPixel(X, Y, RedPixel);
                        }
                    } else {
                        m_NewImage.SetPixel(X, Y, BlackPixel);
                    }
                }
            }

            m_NewImage.Apply();

            return m_NewImage;
        }
    }
}

