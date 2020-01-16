using Dungeonator;
using System.IO;
using UnityEngine;

namespace ExpandTheGungeon.ExpandUtilities {

    class RoomDebug : MonoBehaviour {

        public void DumpRoomLayoutToText(PrototypeDungeonRoom overrideRoom = null) {
            try {
                if (overrideRoom == null) { 
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
                        ETGModConsole.Log("Could not determine current floor/tileset!\n Attempting to use" + currentRoom.GetRoomName() + ".area.ProtoTypeDungeonRoom instead!", false);
                        LogRoomLayout(currentRoom.area.prototypeRoom);
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
                                                LogRoomLayout(dungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements[i].room);
                                                ETGModConsole.Log("Logged current room layout.", false);
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
                                    LogRoomLayout(dungeon.PatternSettings.flows[0].AllNodes[i].overrideExactRoom);
                                    ETGModConsole.Log("Logged current room layout.", false);
                                    dungeon = null;
                                    return;
                                }
                            }
                        }
                    }

                    ETGModConsole.Log("Current Room's ProtoTypeDungeonRoom prefab not found.\n Attempting to use [" + currentRoom.GetRoomName() + "].area.ProtoTypeDungeonRoom instead!", false);
                    LogRoomLayout(currentRoom.area.prototypeRoom);
                    dungeon = null;
                    ETGModConsole.Log("Logged current room layout.", false);
                } else {
                    LogRoomLayout(overrideRoom);
                }
            } catch (System.Exception ex) {
            	ETGModConsole.Log("Failed to log current room layout.", false);
            	ETGModConsole.Log("    " + ex.Message, false);
                Debug.LogException(ex);
            }
        }

        public void LogRoomToFile(string data, string filename) {
            string textstream = data;
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(ETGMod.ResourcesDirectory, filename), false)) { streamWriter.WriteLine(data); }
        }

        public void LogRoomLayout(PrototypeDungeonRoom room) {
            int width = room.Width;
            int height = room.Height;
            string layout = string.Empty;
            for (int Y = height; Y > 0; Y--) {
                for (int X = 0; X < width; X++) {
                    CellType? cellData = room.GetCellDataAtPoint(X, Y - 1).state;
                    if (!cellData.HasValue) {
                        layout += "X";
                    } else if (cellData == CellType.FLOOR) {
                        layout += '-';
                    } else if (cellData == CellType.WALL) {
                        layout += 'W';
                    } else if (cellData == CellType.PIT) {
                        layout += 'P';
                    }
                    if (X == width - 1 && Y != 0) { layout += "\n"; }
                }
            }
            if (string.IsNullOrEmpty(layout)) { return; }
            LogRoomToFile(layout, room.name + "_layout.txt");
        }
    }
}

