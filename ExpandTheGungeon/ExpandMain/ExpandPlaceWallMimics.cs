using ExpandTheGungeon.ExpandPrefab;
using Dungeonator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using MonoMod.RuntimeDetour;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandPlaceWallMimic {

        public static List<string> BannedWallMimicRoomList = new List<string>() {
            "tutorial_room_007_bosstime",
            "endtimes_chamber",
            "dragunroom01",
            "demonwallroom01",
            "elevatormaintenanceroom"
        };

        public static bool PlayerHasWallMimicItem = false;
        public static bool PlayerHasCorruptedJunk = false;
        public static bool PlayerHasThirdEye = false;

        public void PlaceWallMimics(Action<Dungeon, RoomHandler>orig, Dungeon dungeon, RoomHandler roomHandler) {
            int WallMimicsPlaced = 0;
            int WallMimicsPerRoom = 1;
            int numWallMimicsForFloor = MetaInjectionData.GetNumWallMimicsForFloor(dungeon.tileIndices.tilesetId);
            int currentFloor = GameManager.Instance.CurrentFloor;

            if (roomHandler != null) { goto IL_SKIP; }
            
            if (ExpandTheGungeon.LogoEnabled && GameManager.Instance.CurrentLevelOverrideState != GameManager.LevelOverrideState.FOYER) { ExpandTheGungeon.LogoEnabled = false; }
                        
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER) {                
                ExpandSettings.glitchElevatorHasBeenUsed = false;
            }
            
            GameManager.LevelOverrideState levelOverrideState = GameManager.Instance.CurrentLevelOverrideState;

            if (ExpandSettings.debugMode) { ETGModConsole.Log("[DEBUG] Current Floor: " + currentFloor, false); }

            try {
                /*if (ExpandTheGungeon.GameManagerHook == null) {
                    if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing GameManager.Awake Hook...."); }
                    ExpandTheGungeon.GameManagerHook = new Hook(
                        typeof(GameManager).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                        typeof(ExpandTheGungeon).GetMethod("GameManager_Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                        typeof(GameManager)
                    );
                }*/

                if (ExpandSettings.debugMode) { ETGModConsole.Log("[DEBUG] Number of Wall Mimics RewardManager wants to spawn: " + numWallMimicsForFloor, false); }
                
                if (levelOverrideState != GameManager.LevelOverrideState.NONE | levelOverrideState == GameManager.LevelOverrideState.TUTORIAL | levelOverrideState == GameManager.LevelOverrideState.FOYER | dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.RATGEON) {
                    if (ExpandSettings.debugMode) { ETGModConsole.Log("[DEBUG] This floor has been excluded from having Wall Mimics", false); }
                    if (ExpandSettings.debugMode && dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.RATGEON) { ETGModConsole.Log("[DEBUG] The Resourceful Rat Maze/tileset has been excluded from having wall mimics and other floor mods!", false); }
                    return;
                }

                if (dungeon.data == null | dungeon.data.rooms.Count <= 0) {
                    if (ExpandSettings.debugMode) { ETGModConsole.Log("Dungeon has no rooms or Dungeon.data is null! This is super abnormal!", false); }
                    return;
                }
                
                PlaceGlitchElevator(dungeon, currentFloor);

                ExpandJunkEnemySpawneer.PlaceRandomJunkEnemies(dungeon, roomHandler);

                if (ExpandSettings.EnableExpandedGlitchFloors) {
                    if (dungeon.IsGlitchDungeon) {
                        ETGMod.AIActor.OnPreStart = (Action<AIActor>)Delegate.Combine(ETGMod.AIActor.OnPreStart, new Action<AIActor>(EnemyModRandomizer));
                    } else {
                        ETGMod.AIActor.OnPreStart = (Action<AIActor>)Delegate.Remove(ETGMod.AIActor.OnPreStart, new Action<AIActor>(EnemyModRandomizer));
                    }

                    ExpandPlaceCorruptTiles.PlaceCorruptTiles(dungeon);
                }

                ExpandFloorDecorator.PlaceFloorDecoration(dungeon);

                if (PlayerHasCorruptedJunk | PlayerHasThirdEye) { CorruptRandomRooms(dungeon, currentFloor); }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("[DEBUG] Exception caught in early setup code for ExpandMain.ExpandPlaceWallMimics!"); }
                Debug.Log("Exception caught in early setup code for ExpandMain.ExpandPlaceWallMimics!");
                Debug.LogException(ex);
            }
            
            if (numWallMimicsForFloor <= 0 && !PlayerHasWallMimicItem) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("[DEBUG] There will be no Wall Mimics for this floor.", false); }
                PhysicsEngine.Instance.ClearAllCachedTiles();
                return;
            }
            
            IL_SKIP:
            
            if (roomHandler != null) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("[DEBUG] Wall Mimics Assigned to specific room: " + numWallMimicsForFloor, false); }
                if(SpawnWallMimic(dungeon, roomHandler) == 0) {
                    if (ExpandSettings.debugMode) { ETGModConsole.Log("[DEBUG] Failed to find valid locations for a Wall Mimic in room: " + numWallMimicsForFloor + "!", false); }
                }
            } else {
                List<RoomHandler> RoomList = new List<RoomHandler>();
                foreach (RoomHandler room in dungeon.data.rooms) {
                    if (room.area != null && !room.IsShop && !room.PrecludeTilemapDrawing && !string.IsNullOrEmpty(room.GetRoomName()) && room.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.SECRET) {
                        if (!room.area.IsProceduralRoom || room.area.proceduralCells == null) {
                            if (room.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS || !BraveUtility.RandomBool()) {
                                if (!room.GetRoomName().StartsWith("DraGunRoom") && !room.IsMaintenanceRoom() && !BannedWallMimicRoomList.Contains(room.GetRoomName().ToLower())) {
                                    RoomList.Add(room);
                                }
                            }
                        }
                    }
                }

                int ValidRooms = RoomList.Count;
                int RoomsChecked = 0;
                if (PlayerHasWallMimicItem) {
                    switch (dungeon.tileIndices.tilesetId) {
                        case GlobalDungeonData.ValidTilesets.CATHEDRALGEON:
                            WallMimicsPerRoom = 2;
                            break;
                        case GlobalDungeonData.ValidTilesets.BELLYGEON:
                            WallMimicsPerRoom = 2;
                            break;
                        case GlobalDungeonData.ValidTilesets.CATACOMBGEON:
                            WallMimicsPerRoom = 2;
                            break;
                        case GlobalDungeonData.ValidTilesets.OFFICEGEON:
                            WallMimicsPerRoom = 2;
                            break;
                        case GlobalDungeonData.ValidTilesets.WESTGEON:
                            WallMimicsPerRoom = 2;
                            break;
                        case GlobalDungeonData.ValidTilesets.FORGEGEON:
                            WallMimicsPerRoom = 2;
                            break;
                        case GlobalDungeonData.ValidTilesets.HELLGEON:
                            WallMimicsPerRoom = 3;
                            break;
                        default:
                            WallMimicsPerRoom = 1;
                            break;
                    }
                    numWallMimicsForFloor = (ValidRooms * WallMimicsPerRoom);
                    if (ExpandSettings.debugMode) { ETGModConsole.Log("[DEBUG] Wall Mimics assigned by Cursed Bricks: " + (ValidRooms * WallMimicsPerRoom), false); }
                }
                IL_CHECKNEXTROOM:
                if (WallMimicsPlaced >= numWallMimicsForFloor | RoomList.Count <= 0 | RoomsChecked >= ValidRooms) {
                    if (ExpandSettings.debugMode) {
                        ETGModConsole.Log("[DEBUG] All valid rooms for Wall Mimics have been checked. Placement complete.");
                        ETGModConsole.Log("[DEBUG] Wall Mimics Succewsfully Placed: " + WallMimicsPlaced);
                    }
                    PhysicsEngine.Instance.ClearAllCachedTiles();
                    return;
                }
                if (RoomList.Count > 1) { RoomList = RoomList.Shuffle(); }
                RoomHandler CurrentRoom = BraveUtility.RandomElement(RoomList);
                RoomList.Remove(CurrentRoom);
                RoomsChecked++;
                WallMimicsPlaced += SpawnWallMimic(dungeon, CurrentRoom, WallMimicsPerRoom);
                if (RoomsChecked < ValidRooms && WallMimicsPlaced < numWallMimicsForFloor) { goto IL_CHECKNEXTROOM; }
            }
            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("[DEBUG] All valid rooms for Wall Mimics have been checked. Placement complete.");
                ETGModConsole.Log("[DEBUG] Wall Mimics Succewsfully Placed: " + WallMimicsPlaced);
            }
            PhysicsEngine.Instance.ClearAllCachedTiles();
        }

        public int SpawnWallMimic(Dungeon dungeon, RoomHandler currentRoom, int WallMimicsPerRoom = 1) {
            int SouthWallCount = 0;
            int NorthWallCount = 0;
            int WestWallCount = 0;
            int EastWallCount = 0;
            int WallMimicsPlaced = 0;
            int loopCount = 0;
            List<Tuple<IntVector2, DungeonData.Direction>> validWalls = new List<Tuple<IntVector2, DungeonData.Direction>>();
            try { 
                for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                    for (int Height = -1; Height <= currentRoom.area.dimensions.y; Height++) {
                        int X = currentRoom.area.basePosition.x + Width;
                        int Y = currentRoom.area.basePosition.y + Height;
                        // if (dungeon.data.isWall(X, Y) && X % 4 == 0 && Y % 4 == 0 && dungeon.data[new IntVector2(X, Y)].parentRoom != null &&  dungeon.data[new IntVector2(X, Y)].parentRoom == currentRoom) {
                        // if (dungeon.data.isWall(X, Y) && X % 4 == 0 && Y % 4 == 0) {
                        if (dungeon.data.isWall(X, Y) && X % 4 == 0 && Y % 4 == 0 && dungeon.data.GetAbsoluteRoomFromPosition(new IntVector2(X, Y)) != null && dungeon.data.GetAbsoluteRoomFromPosition(new IntVector2(X, Y)) == currentRoom) {
                            int WallCount = 0;
                			if (!dungeon.data.isWall(X - 1, Y + 2) &&
                                !dungeon.data.isWall(X, Y + 2) && 
                                !dungeon.data.isWall(X + 1, Y + 2) &&
                                !dungeon.data.isWall(X + 2, Y + 2) &&
                				!dungeon.data.isWall(X - 1, Y + 1) &&
                                !dungeon.data.isWall(X, Y + 1) && 
                                !dungeon.data.isWall(X + 1, Y + 1) &&
                                !dungeon.data.isWall(X + 2, Y + 1) &&
                				dungeon.data.isWall(X - 1, Y) &&
                                dungeon.data.isWall(X, Y) && 
                                dungeon.data.isWall(X + 1, Y) &&
                                dungeon.data.isWall(X + 2, Y) && 
                				dungeon.data.isWall(X - 1, Y - 1) &&
                                dungeon.data.isWall(X, Y - 1) && 
                                dungeon.data.isWall(X + 1, Y - 1) &&
                                dungeon.data.isWall(X + 2, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X - 1, Y - 3) &&
                                !dungeon.data.isPlainEmptyCell(X, Y - 3) && 
                                !dungeon.data.isPlainEmptyCell(X + 1, Y - 3) &&
                                !dungeon.data.isPlainEmptyCell(X + 2, Y - 3))
                			{
                				validWalls.Add(Tuple.Create(new IntVector2(X, Y), DungeonData.Direction.NORTH));
                				WallCount++;
                                SouthWallCount++;
                            } else if (dungeon.data.isWall(X - 1, Y + 2) && 
                                dungeon.data.isWall(X, Y + 2) &&
                                dungeon.data.isWall(X + 1, Y + 2) &&
                                dungeon.data.isWall(X + 2, Y + 2) &&
                                dungeon.data.isWall(X - 1, Y + 1) &&
                                dungeon.data.isWall(X, Y + 1) &&
                                dungeon.data.isWall(X + 1, Y + 1) &&
                                dungeon.data.isWall(X + 2, Y + 1) && 
                				dungeon.data.isWall(X - 1, Y) &&
                                dungeon.data.isWall(X, Y) &&
                                dungeon.data.isWall(X + 1, Y) &&
                                dungeon.data.isWall(X + 2, Y) &&
                				dungeon.data.isPlainEmptyCell(X, Y - 1) &&
                                dungeon.data.isPlainEmptyCell(X + 1, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X, Y + 4) &&
                                !dungeon.data.isPlainEmptyCell(X + 1, Y + 4))
                			{
                				validWalls.Add(Tuple.Create(new IntVector2(X, Y), DungeonData.Direction.SOUTH));
                				WallCount++;
                                NorthWallCount++;
                            } else if (dungeon.data.isWall(X, Y + 2) &&
                				dungeon.data.isWall(X, Y + 1) &&
                				dungeon.data.isWall(X - 1, Y) &&
                				dungeon.data.isWall(X, Y - 1) &&
                				dungeon.data.isWall(X, Y - 2) &&
                				!dungeon.data.isPlainEmptyCell(X - 2, Y + 2) && 
                				!dungeon.data.isPlainEmptyCell(X - 2, Y + 1) && 
                				!dungeon.data.isPlainEmptyCell(X - 2, Y) &&
                				dungeon.data.isPlainEmptyCell(X + 1, Y) &&
                				dungeon.data.isPlainEmptyCell(X + 1, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X - 2, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X - 2, Y - 2))
                			{
                				validWalls.Add(Tuple.Create(new IntVector2(X, Y), DungeonData.Direction.EAST));
                				WallCount++;
                                WestWallCount++;
                            } else if (dungeon.data.isWall(X, Y + 2) && 
                				dungeon.data.isWall(X, Y + 1) &&
                				dungeon.data.isWall(X + 1, Y) &&
                				dungeon.data.isWall(X, Y - 1) &&
                				dungeon.data.isWall(X, Y - 2) &&
                				!dungeon.data.isPlainEmptyCell(X + 2, Y + 2) &&
                				!dungeon.data.isPlainEmptyCell(X + 2, Y + 1) &&
                				!dungeon.data.isPlainEmptyCell(X + 2, Y) &&
                				dungeon.data.isPlainEmptyCell(X - 1, Y) &&
                				dungeon.data.isPlainEmptyCell(X - 1, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X + 2, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X + 2, Y - 2))
                			{
                				validWalls.Add(Tuple.Create(new IntVector2(X - 1, Y), DungeonData.Direction.WEST));
                				WallCount++;
                                EastWallCount++;
                            }
                			if (WallCount > 0) {
                				bool WallStillValid = true;
                                int XPadding = -5;
                				while (XPadding <= 5 && WallStillValid) {
                					int YPadding = -5;
                					while (YPadding <= 5 && WallStillValid) {
                						int x = X + XPadding;
                						int y = Y + YPadding;
                						if (dungeon.data.CheckInBoundsAndValid(x, y)) {
                							CellData cellData = dungeon.data[x, y];
                							if (cellData != null) {
                                                // if (cellData.type == CellType.PIT || cellData.diagonalWallType != DiagonalWallType.NONE) { WallStillValid = false; }
                                                if (cellData.type == CellType.PIT | cellData.diagonalWallType != DiagonalWallType.NONE) { WallStillValid = false; }
                                            }
                						}
                						YPadding++;
                					}
                					XPadding++;
                				}
                				if (!WallStillValid) {
                					while (WallCount > 0) {
                						validWalls.RemoveAt(validWalls.Count - 1);
                						WallCount--;
                					}
                				}
                			}
                		}
                	}
                }
                if (validWalls.Count <= 0) {
                    if (ExpandSettings.debugMode) {
                        ETGModConsole.Log("[DEBUG] No valid locations found for room: " + currentRoom.GetRoomName() + " while attempting Wall Mimic placement!", false);
                    }
                    return 0;
                }
                while (loopCount < WallMimicsPerRoom && validWalls.Count > 0) {
                    if (validWalls.Count > 0) {
                        Tuple<IntVector2, DungeonData.Direction> WallCell = BraveUtility.RandomElement(validWalls);
                        IntVector2 Position = WallCell.First;
                        DungeonData.Direction Direction = WallCell.Second;
                        if (Direction != DungeonData.Direction.WEST) {
                            currentRoom.RuntimeStampCellComplex(Position.x, Position.y, CellType.FLOOR, DiagonalWallType.NONE);
                        }
                        if (Direction != DungeonData.Direction.EAST) {
                                currentRoom.RuntimeStampCellComplex(Position.x + 1, Position.y, CellType.FLOOR, DiagonalWallType.NONE);
                        }
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(GameManager.Instance.RewardManager.WallMimicChances.EnemyGuid);
                        AIActor WallMimic = AIActor.Spawn(orLoadByGuid, Position, currentRoom, true, AIActor.AwakenAnimationType.Default, true);
                                                
                        if (PlayerHasWallMimicItem) {
                            if (WallMimic && WallMimic.GetComponent<ExpandWallMimicManager>()) {
                                ExpandWallMimicManager wallMimicController = WallMimic.gameObject.GetComponent<ExpandWallMimicManager>();
                                if (wallMimicController) { wallMimicController.CursedBrickMode = true; }
                            }
                        }
                        validWalls.Remove(WallCell);
                        WallMimicsPlaced++;
                    }
                    loopCount++;
                }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] Exception while trying to place WallMimic(s) in room: " + currentRoom.GetRoomName(), false);
                    Debug.LogException(ex);
                }
                return WallMimicsPlaced;
            }
            if (WallMimicsPlaced > 0) {            	
            	if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] Wall Mimic(s) succesfully placed in room: " + currentRoom.GetRoomName(), false);
                    ETGModConsole.Log("[DEBUG] Number of Valid North Wall Mimics locations: " + NorthWallCount, false);
            		ETGModConsole.Log("[DEBUG] Number of Valid South Wall Mimics locations: " + SouthWallCount, false);
            		ETGModConsole.Log("[DEBUG] Number of Valid East Wall Mimics locations: " + EastWallCount, false);
            		ETGModConsole.Log("[DEBUG] Number of Valid West Wall Mimics locations: " + WestWallCount, false);
            		ETGModConsole.Log("[DEBUG] Number of Wall Mimics succesfully placed in room: " + WallMimicsPlaced, false);
            	}
                return WallMimicsPlaced;
            } else {
                ETGModConsole.Log("[DEBUG] No valid location found for room: " + currentRoom.GetRoomName() + " while attempting Wall Mimic placement!", false);
                return 0;
            }
        }
                
        private void PlaceGlitchElevator(Dungeon dungeon, int CurrentFloor) {
            GameManager.LevelOverrideState levelOverrideState = GameManager.Instance.CurrentLevelOverrideState;
            if (dungeon.IsGlitchDungeon | ExpandSettings.glitchElevatorHasBeenUsed | CurrentFloor > 4) { return; }
            if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.BOSSRUSH | GameManager.Instance.CurrentGameMode == GameManager.GameMode.SUPERBOSSRUSH) { return; }
            if (levelOverrideState == GameManager.LevelOverrideState.FOYER | levelOverrideState == GameManager.LevelOverrideState.TUTORIAL) {
                ExpandSettings.glitchElevatorHasBeenUsed = false;
                return;
            }
            if (levelOverrideState == GameManager.LevelOverrideState.CHARACTER_PAST) {
                ExpandSettings.glitchElevatorHasBeenUsed = false;
                return;
            }
            if (levelOverrideState == GameManager.LevelOverrideState.END_TIMES) { return; }
            if (GameManager.Instance.CurrentFloor >= 5) { return; }
            if (UnityEngine.Random.value > 0.004f) { return; }
            if (ExpandSettings.debugMode) { ETGModConsole.Log("[DEBUG] Attempting to place a Glitch Elevator!"); }
            List<RoomHandler> Rooms = new List<RoomHandler>();
            foreach (RoomHandler room in dungeon.data.rooms) {
                if (!string.IsNullOrEmpty(room.GetRoomName()) && !room.IsShop && !room.IsMaintenanceRoom() && !room.GetRoomName().ToLower().StartsWith("exit") &&
                    !room.GetRoomName().ToLower().StartsWith("tiny_exit") && !room.GetRoomName().ToLower().StartsWith("elevator") &&
                    !room.GetRoomName().ToLower().StartsWith("tiny_entrance") && !room.GetRoomName().ToLower().StartsWith("gungeon entrance") &&
                    !room.GetRoomName().ToLower().StartsWith("gungeon_rewardroom") && !room.GetRoomName().ToLower().StartsWith("reward room") &&
                    !room.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.Expand_BootlegRoom.name.ToLower()) && !room.area.prototypeRoom.precludeAllTilemapDrawing)
                {
                    Rooms.Add(room);
                }
            }
            int SpawnAttempts = 0;
            IL_RETRY:
            if (Rooms.Count <= 0) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] No rooms that are allowed to contain a Glitch Elevator or have valid locations for one are present on this floor!", false);
                }
                return;
            }
            if (Rooms.Count > 1) { Rooms = Rooms.Shuffle(); }
            RoomHandler SelectedRoom = BraveUtility.RandomElement(Rooms);
            Rooms.Remove(SelectedRoom);
            SpawnAttempts++;
            if (!SpawnGlitchElevator(dungeon, SelectedRoom) && SpawnAttempts < dungeon.data.rooms.Count) { goto IL_RETRY; }
            return;
        }

        public bool SpawnGlitchElevator(Dungeon dungeon, RoomHandler currentRoom) {
            if (currentRoom.area == null) { return false; }

            List<IntVector2> validWalls = new List<IntVector2>();
            for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                for (int Height = -1; Height <= currentRoom.area.dimensions.y; Height++) {
                    int X = currentRoom.area.basePosition.x + Width;
                    int Y = currentRoom.area.basePosition.y + Height;
                    if (dungeon.data.isWall(X, Y) && dungeon.data.GetAbsoluteRoomFromPosition(new IntVector2(X, Y)) == currentRoom) {
                        int WallCellCount = 0;
                        if (!dungeon.data.isPlainEmptyCell(X - 3, Y + 6) && !dungeon.data.isPlainEmptyCell(X - 2, Y + 6) && !dungeon.data.isPlainEmptyCell(X - 1, Y + 6) && !dungeon.data.isPlainEmptyCell(X, Y + 6) && !dungeon.data.isPlainEmptyCell(X + 1, Y + 6) && !dungeon.data.isPlainEmptyCell(X + 2, Y + 6) && !dungeon.data.isPlainEmptyCell(X + 3, Y + 6) && !dungeon.data.isPlainEmptyCell(X + 4, Y + 6) && !dungeon.data.isPlainEmptyCell(X + 5, Y + 6) &&
                            !dungeon.data.isPlainEmptyCell(X - 3, Y + 5) && !dungeon.data.isPlainEmptyCell(X - 2, Y + 5) && !dungeon.data.isPlainEmptyCell(X - 1, Y + 5) && !dungeon.data.isPlainEmptyCell(X, Y + 5) && !dungeon.data.isPlainEmptyCell(X + 1, Y + 5) && !dungeon.data.isPlainEmptyCell(X + 2, Y + 5) && !dungeon.data.isPlainEmptyCell(X + 3, Y + 5) && !dungeon.data.isPlainEmptyCell(X + 4, Y + 5) && !dungeon.data.isPlainEmptyCell(X + 5, Y + 5) &&
                            !dungeon.data.isPlainEmptyCell(X - 3, Y + 4) && !dungeon.data.isPlainEmptyCell(X - 2, Y + 4) && !dungeon.data.isPlainEmptyCell(X - 1, Y + 4) && !dungeon.data.isPlainEmptyCell(X, Y + 4) && !dungeon.data.isPlainEmptyCell(X + 1, Y + 4) && !dungeon.data.isPlainEmptyCell(X + 2, Y + 4) && !dungeon.data.isPlainEmptyCell(X + 3, Y + 4) && !dungeon.data.isPlainEmptyCell(X + 4, Y + 4) && !dungeon.data.isPlainEmptyCell(X + 5, Y + 4) &&
                            !dungeon.data.isPlainEmptyCell(X - 3, Y + 3) && !dungeon.data.isPlainEmptyCell(X - 2, Y + 3) && !dungeon.data.isPlainEmptyCell(X - 1, Y + 3) && !dungeon.data.isPlainEmptyCell(X, Y + 3) && !dungeon.data.isPlainEmptyCell(X + 1, Y + 3) && !dungeon.data.isPlainEmptyCell(X + 2, Y + 3) && !dungeon.data.isPlainEmptyCell(X + 3, Y + 3) && !dungeon.data.isPlainEmptyCell(X + 4, Y + 3) && !dungeon.data.isPlainEmptyCell(X + 5, Y + 3) &&
                            !dungeon.data.isPlainEmptyCell(X - 3, Y + 2) && !dungeon.data.isPlainEmptyCell(X - 2, Y + 2) && !dungeon.data.isPlainEmptyCell(X - 1, Y + 2) && !dungeon.data.isPlainEmptyCell(X, Y + 2) && !dungeon.data.isPlainEmptyCell(X + 1, Y + 2) && !dungeon.data.isPlainEmptyCell(X + 2, Y + 2) && !dungeon.data.isPlainEmptyCell(X + 3, Y + 2) && !dungeon.data.isPlainEmptyCell(X + 4, Y + 2) && !dungeon.data.isPlainEmptyCell(X + 5, Y + 2) &&
                            !dungeon.data.isPlainEmptyCell(X - 4, Y + 1) && dungeon.data.isWall(X - 3, Y + 1) && dungeon.data.isWall(X - 2, Y + 1) && dungeon.data.isWall(X - 1, Y + 1) && dungeon.data.isWall(X, Y + 1) && dungeon.data.isWall(X + 1, Y + 1) && dungeon.data.isWall(X + 2, Y + 1) && dungeon.data.isWall(X + 3, Y + 1) && dungeon.data.isWall(X + 4, Y + 1) && dungeon.data.isWall(X + 5, Y + 1) && dungeon.data.isWall(X + 6, Y + 1) && dungeon.data.isWall(X + 7, Y + 1) && !dungeon.data.isPlainEmptyCell(X + 8, Y + 1) && !dungeon.data.isPlainEmptyCell(X + 9, Y + 1) &&
                            !dungeon.data.isPlainEmptyCell(X - 4, Y) && dungeon.data.isWall(X - 3, Y) && dungeon.data.isWall(X - 2, Y) && dungeon.data.isWall(X - 1, Y) && dungeon.data.isWall(X, Y) && dungeon.data.isWall(X + 1, Y) && dungeon.data.isWall(X + 2, Y) && dungeon.data.isWall(X + 3, Y) && dungeon.data.isWall(X + 4, Y) && dungeon.data.isWall(X + 5, Y) && dungeon.data.isWall(X + 6, Y) && dungeon.data.isWall(X + 7, Y) && !dungeon.data.isPlainEmptyCell(X + 8, Y) && !dungeon.data.isPlainEmptyCell(X + 9, Y) &&
                             dungeon.data.isPlainEmptyCell(X - 3, Y - 1) && dungeon.data.isPlainEmptyCell(X - 2, Y - 1) && dungeon.data.isPlainEmptyCell(X - 1, Y - 1) && dungeon.data.isPlainEmptyCell(X, Y - 1) && dungeon.data.isPlainEmptyCell(X + 1, Y - 1) && dungeon.data.isPlainEmptyCell(X + 2, Y - 1) && dungeon.data.isPlainEmptyCell(X + 3, Y - 1) && dungeon.data.isPlainEmptyCell(X + 4, Y - 1) && dungeon.data.isPlainEmptyCell(X + 5, Y - 1) && dungeon.data.isPlainEmptyCell(X + 6, Y - 1) && dungeon.data.isPlainEmptyCell(X + 7, Y - 1) &&
                             dungeon.data.isPlainEmptyCell(X - 3, Y - 2) && dungeon.data.isPlainEmptyCell(X - 2, Y - 2) && dungeon.data.isPlainEmptyCell(X - 1, Y - 2) && dungeon.data.isPlainEmptyCell(X, Y - 2) && dungeon.data.isPlainEmptyCell(X + 1, Y - 2) && dungeon.data.isPlainEmptyCell(X + 2, Y - 2) && dungeon.data.isPlainEmptyCell(X + 3, Y - 2) && dungeon.data.isPlainEmptyCell(X + 4, Y - 2) && dungeon.data.isPlainEmptyCell(X + 5, Y - 2) && dungeon.data.isPlainEmptyCell(X + 6, Y - 2) && dungeon.data.isPlainEmptyCell(X + 7, Y - 2))
                        {
                            validWalls.Add(new IntVector2(X, Y));
                            WallCellCount++;
                        }
                        if (WallCellCount > 0) {
                            bool ValidCell = true;
                            int XPadding = -5;
                            while (XPadding <= 5 && ValidCell) {
                                int YPadding = -5;
                                while (YPadding <= 5 && ValidCell) {
                                    int x = X + XPadding;
                                    int y = Y + YPadding;
                                    if (dungeon.data.CheckInBoundsAndValid(x, y)) {
                                        CellData cellData = dungeon.data[x, y];
                                        if (cellData != null) {
                                            if (cellData.type == CellType.PIT || cellData.diagonalWallType != DiagonalWallType.NONE) { ValidCell = false; }
                                        }
                                    }
                                    YPadding++;
                                }
                                XPadding++;
                            }
                            if (!ValidCell) {
                                while (WallCellCount > 0) {
                                    validWalls.RemoveAt(validWalls.Count - 1);
                                    WallCellCount--;
                                }
                            }
                        }
                    }
                }
            }
            if (validWalls.Count > 0) {
                IntVector2 WallCell = (BraveUtility.RandomElement(validWalls) - currentRoom.area.basePosition);
                GameObject ElevatorObject = ExpandPrefabs.ElevatorDeparture.InstantiateObject(currentRoom, WallCell, false);
                if (ElevatorObject.GetComponent<ElevatorDepartureController>()) {
                    ElevatorObject.AddComponent<ExpandElevatorDepartureManager>();
                    ExpandElevatorDepartureManager elevatorComponent = ElevatorObject.GetComponent<ExpandElevatorDepartureManager>();
                    elevatorComponent.UsesOverrideTargetFloor = true;
                    elevatorComponent.IsGlitchElevator = true;
                } else if (ElevatorObject.GetComponent<ExpandNewElevatorController>()) {
                    ExpandNewElevatorController exElevatorController = ElevatorObject.GetComponent<ExpandNewElevatorController>();                    
                    exElevatorController.IsGlitchElevator = true;
                }
                
                if (ElevatorObject.GetComponentsInChildren<tk2dBaseSprite>(true) != null) {
                    foreach (tk2dBaseSprite baseSprite in ElevatorObject.GetComponentsInChildren<tk2dBaseSprite>(true)) {
                        ExpandShaders.Instance.ApplyGlitchShader(baseSprite);
                    }
                }
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] Number of Valid Glitch Elevator locations found: " + validWalls.Count, false);
                    ETGModConsole.Log("[DEBUG] Glitch Elevator Successfully placed in room: " + currentRoom.GetRoomName(), false);
                }
                return true;
            } else {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] No valid locations found for room: " + currentRoom.GetRoomName() + ".  This room was skipped!", false);
                }
                return false;
            }
        }

        private void CorruptRandomRooms(Dungeon dungeon, int currentFloor) {
            if (dungeon.IsGlitchDungeon | ExpandDungeonFlows.ExpandDungeonFlow.isGlitchFlow) { return; }

            List<string> LoggedExceptions = new List<string>();
            
            List<RoomHandler> m_Rooms = new List<RoomHandler>();

            foreach (RoomHandler room in dungeon.data.rooms) {
                try {
                    if (room.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS &&
                        room.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.SECRET &&
                        room.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) && !room.PrecludeTilemapDrawing &&
                        (!room.area.IsProceduralRoom || room.area.proceduralCells == null)
                       )
                    {
                        if (UnityEngine.Random.value <= 0.25f) { m_Rooms.Add(room); }
                    }
                } catch (Exception ex) {
                    string RoomName = room.GetRoomName();
                    string ExceptionText = string.Empty;

                    if (!string.IsNullOrEmpty(RoomName)) {
                        ExceptionText = ("Exception caught while adding room: " + RoomName + "in ExpandMain.CorruptRandomRooms!");
                    } else {
                        ExceptionText = ("Exception caught while building room list in ExpandMain.CorruptRandomRooms!");
                    }
                    LoggedExceptions.Add(ExceptionText);
                    Debug.Log(ExceptionText);
                    Debug.LogException(ex);
                }
            }

            if (m_Rooms.Count <= 0) { return; }

            m_Rooms = m_Rooms.Shuffle();

            RoomHandler Room1 = BraveUtility.RandomElement(m_Rooms);
            RoomHandler Room2 = null;
            m_Rooms.Remove(Room1);

            if (m_Rooms.Count > 0) { Room2 = BraveUtility.RandomElement(m_Rooms); }

            m_Rooms.Clear();
            m_Rooms.Add(Room1);

            if (Room2 != null && (UnityEngine.Random.value > 0.5f | PlayerHasThirdEye)) { m_Rooms.Add(Room2); }

            foreach (RoomHandler room in m_Rooms) {
                try {
                    if (PlayerHasCorruptedJunk) {
                        string RoomName = room.GetRoomName();

                        if (!string.IsNullOrEmpty(RoomName)) {
                            room.area.PrototypeRoomName = "Corrupted " + RoomName;
                        } else {
                            room.area.PrototypeRoomName = "Corrupted Room";
                        }
                        ExpandPlaceCorruptTiles.PlaceCorruptTiles(dungeon, room, isCorruptedJunkRoom: true);
                    }
                    ExpandPlaceGlitchedEnemies.PlaceRandomEnemies(dungeon, currentFloor, room);
                } catch (Exception ex) {
                    string RoomName = room.GetRoomName();
                    string ExceptionText = string.Empty;

                    if (!string.IsNullOrEmpty(RoomName)) {
                        ExceptionText = ("Exception caught while corrupting room: " + RoomName + "in ExpandMain.CorruptRandomRooms!");
                    } else {
                        ExceptionText = ("Exception caught while corrupting a room in ExpandMain.CorruptRandomRooms!");
                    }
                    LoggedExceptions.Add(ExceptionText);
                    Debug.Log(ExceptionText);
                    Debug.LogException(ex);
                }
            }

            if (ExpandSettings.debugMode && LoggedExceptions.Count > 0) {
                foreach (string exception in LoggedExceptions) { ETGModConsole.Log(exception, false); }
            }
        }

        private void EnemyModRandomizer(AIActor targetActor) {
            
            if (string.IsNullOrEmpty(targetActor.EnemyGuid)) { return; }
            
            if (GameManager.Instance.Dungeon.IsGlitchDungeon && !targetActor.IsBlackPhantom && !string.IsNullOrEmpty(targetActor.EnemyGuid)) {
                if (UnityEngine.Random.value <= 0.3 && targetActor.healthHaver != null && !ExpandLists.DontGlitchMeList.Contains(targetActor.EnemyGuid) && !ExpandLists.blobsAndCritters.Contains(targetActor.EnemyGuid) && targetActor.EnemyGuid != "5e0af7f7d9de4755a68d2fd3bbc15df4") {
                    if (!targetActor.healthHaver.IsBoss && !targetActor.sprite.usesOverrideMaterial && targetActor.optionalPalette == null) {
                        float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                        float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
                        float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
                        float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
                        float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);
                        
                        ExpandShaders.Instance.BecomeGlitched(targetActor, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);
                        if (targetActor.GetComponent<ExpandSpawnGlitchObjectOnDeath>() == null) {
                            if (UnityEngine.Random.value <= 0.25) { targetActor.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>(); }
                        }
                        ExpandGlitchedEnemies.GlitchExistingEnemy(targetActor);

                        if (UnityEngine.Random.value <= 0.1f && targetActor.EnemyGuid != "4d37ce3d666b4ddda8039929225b7ede" && targetActor.EnemyGuid != "19b420dec96d4e9ea4aebc3398c0ba7a" && targetActor.GetComponent<ExplodeOnDeath>() == null && targetActor.GetComponent<ExpandSpawnGlitchObjectOnDeath>() == null && targetActor.GetComponent<ExpandSpawnGlitchEnemyOnDeath>() == null) {
                            try { targetActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); } catch (Exception) { }
                        }
                    }
                }
                return;
            }
        }

    }
}

