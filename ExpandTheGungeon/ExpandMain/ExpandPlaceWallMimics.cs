using ExpandTheGungeon.ExpandObjects;
using Dungeonator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using MonoMod.RuntimeDetour;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;

namespace ExpandTheGungeon.ExpandMain {

    class ExpandPlaceWallMimic : MonoBehaviour {

        public static List<string> BannedWallMimicRoomList = new List<string>() {
            "tutorial_room_007_bosstime",
            "endtimes_chamber",
            "dragunroom01",
            "demonwallroom01",
            "elevatormaintenanceroom"
        };

        private void PlaceWallMimics(Action<Dungeon, RoomHandler> orig, Dungeon dungeon, RoomHandler roomHandler) {

            // Used for debug read out information
            int NorthWallCount = 0;
            int SouthWallCount = 0;
            int EastWallCount = 0;
            int WestWallCount = 0;
            int WallMimicsPlaced = 0;
            int CorruptedRooms = 0;
            int iterations = 0;

            bool PlayerHasWallMimicItem = false;
            bool PlayerHasCorruptedJunk = false;
            bool DontPlaceWallMimics = false;

            if (ExpandTheGungeon.LogoEnabled && GameManager.Instance.CurrentLevelOverrideState != GameManager.LevelOverrideState.FOYER) { ExpandTheGungeon.LogoEnabled = false; }

            if (ExpandTheGungeon.GameManagerHook == null) {
                if (ExpandStats.debugMode) { Debug.Log("[ExpandTheGungeon] Installing GameManager.Awake Hook...."); }
                ExpandTheGungeon.GameManagerHook = new Hook(
                    typeof(GameManager).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                    typeof(ExpandTheGungeon).GetMethod("GameManager_Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                    typeof(GameManager)
                );
            }

            ExpandStaticReferenceManager.ClearStaticPerLevelData();
            ExpandStaticReferenceManager.PopulateLists();

            if (GameManager.Instance.PrimaryPlayer) {
                if (GameManager.Instance.PrimaryPlayer.HasPassiveItem(CursedBrick.CursedBrickID)) { PlayerHasWallMimicItem = true; }
                if (GameManager.Instance.PrimaryPlayer.HasPassiveItem(CorruptedJunk.CorruptedJunkID)) { PlayerHasCorruptedJunk = true; }
            } else if (GameManager.Instance.SecondaryPlayer) {
                if (GameManager.Instance.SecondaryPlayer.HasPassiveItem(CursedBrick.CursedBrickID)) { PlayerHasWallMimicItem = true; }
                if (GameManager.Instance.SecondaryPlayer.HasPassiveItem(CorruptedJunk.CorruptedJunkID)) { PlayerHasCorruptedJunk = true; }
            }

            ExpandPlaceCorruptTiles m_CorruptTilePlacer = new ExpandPlaceCorruptTiles();
            ExpandPlaceGlitchedEnemies m_PlaceCorruptedEnemies = new ExpandPlaceGlitchedEnemies();

            try {
                int currentFloor = GameManager.Instance.CurrentFloor;
                int numWallMimicsForFloor = MetaInjectionData.GetNumWallMimicsForFloor(dungeon.tileIndices.tilesetId);
                int WallMimicsPerRoom = 1;
                
                GameManager.LevelOverrideState levelOverrideState = GameManager.Instance.CurrentLevelOverrideState;
                
                if (PlayerHasWallMimicItem) {
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON) { WallMimicsPerRoom = 1; }
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.SEWERGEON) { WallMimicsPerRoom = 1; }
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.JUNGLEGEON) { WallMimicsPerRoom = 1; }
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.GUNGEON) { WallMimicsPerRoom = 1; }
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATHEDRALGEON) { WallMimicsPerRoom = 2; }
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.BELLYGEON) { WallMimicsPerRoom = 2; }
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATACOMBGEON) { WallMimicsPerRoom = 2; }
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON) { WallMimicsPerRoom = 2; }
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) { WallMimicsPerRoom = 2; }
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) { WallMimicsPerRoom = 3; }
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.HELLGEON) { WallMimicsPerRoom = 3; }

                    numWallMimicsForFloor = dungeon.data.rooms.Count;

                    if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] Wall Mimics assigned by Mod: " + numWallMimicsForFloor, false); }
                } else {
                    if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] Wall Mimics assigned by RewardManager: " + numWallMimicsForFloor, false); }
                }
                                
            	if (levelOverrideState == GameManager.LevelOverrideState.RESOURCEFUL_RAT) {
            		if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] The Resourceful Rat Maze has been excluded from having wall mimics.", false); }
                    return;
                }
                
                if (levelOverrideState != GameManager.LevelOverrideState.NONE | levelOverrideState == GameManager.LevelOverrideState.TUTORIAL) {
                    if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] This floor has been excluded from having Wall Mimics", false); }
                    return;
                }

                if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] Current Floor: " + currentFloor, false); }
                                
                SetupSecretDoorDestinations(dungeon);

                if (currentFloor < 4) { PlaceGlitchElevator(dungeon); }

                ExpandJunkEnemySpawneer m_ExpandJunkEnemySpawneer = new ExpandJunkEnemySpawneer();
                m_ExpandJunkEnemySpawneer.PlaceRandomJunkEnemies(dungeon, roomHandler);
                m_ExpandJunkEnemySpawneer = null;

                if (dungeon.IsGlitchDungeon) {
                    ETGMod.AIActor.OnPreStart = (Action<AIActor>)Delegate.Combine(ETGMod.AIActor.OnPreStart, new Action<AIActor>(EnemyModRandomizer));
                } else {
                    ETGMod.AIActor.OnPreStart = (Action<AIActor>)Delegate.Remove(ETGMod.AIActor.OnPreStart, new Action<AIActor>(EnemyModRandomizer));
                }
                
                m_CorruptTilePlacer.PlaceCorruptTiles(dungeon);

                ExpandFloorDecorator FloorDecorator = new ExpandFloorDecorator();
                FloorDecorator.PlaceFloorDecoration(dungeon);
                FloorDecorator = null;

                if (numWallMimicsForFloor <= 0) {
            		if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] There will be no Wall Mimics assigned to this floor.", false); }
                    if (!PlayerHasCorruptedJunk) { return; }
                    DontPlaceWallMimics = true;
                }

                if (ExpandStats.debugMode && !DontPlaceWallMimics) { ETGModConsole.Log("[DEBUG] Wall Mimics Assigned to Floor: " + numWallMimicsForFloor, false); }
                
                List<int> roomList = Enumerable.Range(0, dungeon.data.rooms.Count).ToList();
                roomList = roomList.Shuffle();
                
                if (roomHandler != null) { roomList = new List<int>(new int[] { dungeon.data.rooms.IndexOf(roomHandler) }); }

                while (iterations < roomList.Count && WallMimicsPlaced < numWallMimicsForFloor) {
            		RoomHandler currentRoom = dungeon.data.rooms[roomList[iterations]];
                    if (!currentRoom.IsShop && !currentRoom.PrecludeTilemapDrawing && !string.IsNullOrEmpty(currentRoom.GetRoomName())) {
            			if (!currentRoom.area.IsProceduralRoom || currentRoom.area.proceduralCells == null) {
            				if (currentRoom.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS || !BraveUtility.RandomBool()) {
            					if (!currentRoom.GetRoomName().StartsWith("DraGunRoom") && !currentRoom.IsMaintenanceRoom() && !BannedWallMimicRoomList.Contains(currentRoom.GetRoomName().ToLower())) {
                                    List<Tuple<IntVector2, DungeonData.Direction>> validWalls = new List<Tuple<IntVector2, DungeonData.Direction>>();
                                    if (!DontPlaceWallMimics) {
            						    for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
            						    	for (int Height = -1; Height <= currentRoom.area.dimensions.y; Height++) {
            						    		int X = currentRoom.area.basePosition.x + Width;
            						    		int Y = currentRoom.area.basePosition.y + Height;
                                                if (dungeon.data.isWall(X, Y) && X % 4 == 0 && Y % 4 == 0 && dungeon.data[new IntVector2(currentRoom.area.basePosition.x, currentRoom.area.basePosition.y)].parentRoom == currentRoom) {
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
            						    				bool flag2 = true;
            						    				int XPadding = -5;
            						    				while (XPadding <= 5 && flag2) {
            						    					int YPadding = -5;
            						    					while (YPadding <= 5 && flag2) {
            						    						int x = X + XPadding;
            						    						int y = Y + YPadding;
            						    						if (dungeon.data.CheckInBoundsAndValid(x, y)) {
            						    							CellData cellData = dungeon.data[x, y];
            						    							if (cellData != null) {
            						    								if (cellData.type == CellType.PIT || cellData.diagonalWallType != DiagonalWallType.NONE) { flag2 = false; }
            						    							}
            						    						}
            						    						YPadding++;
            						    					}
            						    					XPadding++;
            						    				}
            						    				if (!flag2) {
            						    					while (WallCount > 0) {
            						    						validWalls.RemoveAt(validWalls.Count - 1);
            						    						WallCount--;
            						    					}
            						    				}
            						    			}
            						    		}
            						    	}
            						    }
                                    }					
            						if (roomHandler == null) {
                                        if (PlayerHasCorruptedJunk && CorruptedRooms < 2 && !dungeon.IsGlitchDungeon && !ExpandDungeonFlows.ExpandDungeonFlow.isGlitchFlow &&
                                            currentRoom.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS && 
                                            currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) && UnityEngine.Random.value <= 0.2f &&
                                            !currentRoom.GetRoomName().ToLower().StartsWith("corrupted"))
                                        {
                                            string RoomName = currentRoom.GetRoomName();
                                            currentRoom.area.PrototypeRoomName = "Corrupted " + RoomName;
                                            
                                            m_CorruptTilePlacer.PlaceCorruptTiles(dungeon, currentRoom, isCorruptedJunkRoom: true);
                                            m_PlaceCorruptedEnemies.PlaceRandomEnemies(dungeon, currentFloor, currentRoom);

                                            CorruptedRooms++;
                                        }
                                        int loopCount = 0;
                                        if (!DontPlaceWallMimics) {
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
                                        }
            						}
            					}
            				}
            			}
            		}
            		iterations++;
            	}
                if (WallMimicsPlaced > 0) {
            		PhysicsEngine.Instance.ClearAllCachedTiles();
            		if (ExpandStats.debugMode) {
            			ETGModConsole.Log("[DEBUG] Number of Valid North Wall Mimics locations: " + NorthWallCount, false);
            			ETGModConsole.Log("[DEBUG] Number of Valid South Wall Mimics locations: " + SouthWallCount, false);
            			ETGModConsole.Log("[DEBUG] Number of Valid East Wall Mimics locations: " + EastWallCount, false);
            			ETGModConsole.Log("[DEBUG] Number of Valid West Wall Mimics locations: " + WestWallCount, false);
            			ETGModConsole.Log("[DEBUG] Number of Wall Mimics succesfully placed: " + WallMimicsPlaced, false);
            		}
            	}
            } catch (Exception ex) {
                if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] Exception occured in Dungeon.PlaceWallMimics!"); }
                Debug.Log("Exception caught in Dungeon.PlaceWallMimics!");
                Debug.LogException(ex);
                if (WallMimicsPlaced > 0) {
                    PhysicsEngine.Instance.ClearAllCachedTiles();
                    if (ExpandStats.debugMode) {
                        ETGModConsole.Log("[DEBUG] Number of Valid North Wall Mimics locations: " + NorthWallCount, false);
                        ETGModConsole.Log("[DEBUG] Number of Valid South Wall Mimics locations: " + SouthWallCount, false);
                        ETGModConsole.Log("[DEBUG] Number of Valid East Wall Mimics locations: " + EastWallCount, false);
                        ETGModConsole.Log("[DEBUG] Number of Valid West Wall Mimics locations: " + WestWallCount, false);
                        ETGModConsole.Log("[DEBUG] Number of Wall Mimics succesfully placed: " + WallMimicsPlaced, false);
                    }
                }
            }
            m_CorruptTilePlacer = null;
            m_PlaceCorruptedEnemies = null;
        }
                
        private void PlaceGlitchElevator(Dungeon dungeon) {

            GameManager.LevelOverrideState levelOverrideState = GameManager.Instance.CurrentLevelOverrideState;

            if (dungeon.IsGlitchDungeon | ExpandStats.elevatorHasBeenUsed) { return; }
            if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.BOSSRUSH | GameManager.Instance.CurrentGameMode == GameManager.GameMode.SUPERBOSSRUSH) { return; }

            if (levelOverrideState == GameManager.LevelOverrideState.FOYER | levelOverrideState == GameManager.LevelOverrideState.TUTORIAL) {
                ExpandStats.elevatorHasBeenUsed = false;
                return;
            }

            if (levelOverrideState == GameManager.LevelOverrideState.CHARACTER_PAST) {
                ExpandStats.elevatorHasBeenUsed = false;
                return;
            }
            

            if (levelOverrideState == GameManager.LevelOverrideState.END_TIMES) { return; }
            if (GameManager.Instance.CurrentFloor >= 5) { return; }
            if (UnityEngine.Random.value > 0.003f) { return; }

            int MaxNumberOfElevators = 1;
            int ElevatorsPlaced = 0;
            int ElevatorLocations = 0;
            int SelectedRoom = 0;

            List<int> roomList = Enumerable.Range(0, dungeon.data.rooms.Count).ToList();
            roomList = roomList.Shuffle();            
            List<IntVector2> validWalls = new List<IntVector2>();
            while (SelectedRoom < roomList.Count && ElevatorsPlaced < MaxNumberOfElevators) {
        		RoomHandler currentRoom = dungeon.data.rooms[roomList[SelectedRoom]];
        		if (!currentRoom.IsShop && !currentRoom.IsMaintenanceRoom() && !currentRoom.GetRoomName().ToLower().StartsWith("exit") &&
                    !currentRoom.GetRoomName().ToLower().StartsWith("tiny_exit") && !currentRoom.GetRoomName().ToLower().StartsWith("elevator") &&
                    !currentRoom.GetRoomName().ToLower().StartsWith("tiny_entrance") && !currentRoom.GetRoomName().ToLower().StartsWith("gungeon entrance") &&
                    !currentRoom.GetRoomName().ToLower().StartsWith("gungeon_rewardroom") && !currentRoom.GetRoomName().ToLower().StartsWith("reward room") &&
                    !currentRoom.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.Expand_BootlegRoom.name.ToLower()) && !currentRoom.area.prototypeRoom.precludeAllTilemapDrawing)
                {
        			if (!currentRoom.area.IsProceduralRoom || currentRoom.area.proceduralCells == null) {
        				if (currentRoom.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS || (PlayerStats.GetTotalCurse() >= 5 && !BraveUtility.RandomBool())) {
        					if (!currentRoom.GetRoomName().StartsWith("DraGunRoom")) {
        						if (currentRoom.connectedRooms != null) {
        							for (int i = 0; i < currentRoom.connectedRooms.Count; i++) {
        								if (currentRoom.connectedRooms[i] == null || currentRoom.connectedRooms[i].area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) { }
        							}
        						}        						
        						validWalls.Clear();
        						for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
        							for (int Height = -1; Height <= currentRoom.area.dimensions.y; Height++) {
        								int X = currentRoom.area.basePosition.x + Width;
        								int Y = currentRoom.area.basePosition.y + Height;
        								if (dungeon.data.isWall(X, Y) && dungeon.data[new IntVector2(X, Y)].parentRoom == currentRoom) {
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
                                                ElevatorLocations++;

                                            }
        									if (WallCellCount > 0) {
        										bool flag2 = true;
        										int XPadding = -5;
        										while (XPadding <= 5 && flag2) {
        											int YPadding = -5;
        											while (YPadding <= 5 && flag2) {
        												int x = X + XPadding;
        												int y = Y + YPadding;
        												if (dungeon.data.CheckInBoundsAndValid(x, y)) {
        													CellData cellData = dungeon.data[x, y];
        													if (cellData != null) {
        														if (cellData.type == CellType.PIT || cellData.diagonalWallType != DiagonalWallType.NONE) { flag2 = false; }
        													}
        												}
        												YPadding++;
        											}
        											XPadding++;
        										}
        										if (!flag2) {
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
                                    ElevatorObject.AddComponent<ExpandElevatorDepartureManager>();
                                    ExpandElevatorDepartureManager elevatorComponent = ElevatorObject.GetComponent<ExpandElevatorDepartureManager>();
                                    elevatorComponent.OverrideTargetFloor = GlobalDungeonData.ValidTilesets.OFFICEGEON;
                                    elevatorComponent.UsesOverrideTargetFloor = true;
                                    if (elevatorComponent.gameObject.GetComponentsInChildren<tk2dBaseSprite>(true) != null) {
                                        foreach (tk2dBaseSprite baseSprite in elevatorComponent.gameObject.GetComponentsInChildren<tk2dBaseSprite>(true)) {
                                            ExpandShaders.Instance.ApplyGlitchShader(baseSprite);
                                        }
                                    }
                                    validWalls.Remove(WallCell);
                                    ElevatorsPlaced++;
                                }
        					}
        				}
        			}
        		}
                SelectedRoom++;
            }
            if (ExpandStats.debugMode) {
                ETGModConsole.Log("[DEBUG] Number of Valid Glitch Elevator locations found: " + ElevatorLocations, false);
                ETGModConsole.Log("[DEBUG] Number of Glitch Elevators placed: " + ElevatorsPlaced, false);
            }
        }

        // Note that this door currently only supports maps that place exactly two of these.
        // Run this during PlaceWallMimics step of floor generation.
        public static void SetupSecretDoorDestinations(Dungeon dungeon) {
            ExpandSecretDoorPlacable m_SecretDoorEntrance = FindObjectOfType<ExpandSecretDoorPlacable>();
            ExpandSecretDoorExitPlacable m_SecretDoorDestination = FindObjectOfType<ExpandSecretDoorExitPlacable>();

            if (m_SecretDoorDestination && m_SecretDoorEntrance) {
                m_SecretDoorDestination.m_Destination = (m_SecretDoorEntrance.transform.position + new Vector3(1.25f, 0.6f));
                m_SecretDoorEntrance.m_Destination = (m_SecretDoorDestination.transform.position + new Vector3(1.25f, 0.6f));
                m_SecretDoorDestination.m_DestinationDoor = m_SecretDoorEntrance;
                m_SecretDoorEntrance.m_DestinationDoor = m_SecretDoorDestination;
                m_SecretDoorDestination.hasBeenSetup = true;
                m_SecretDoorEntrance.hasBeenSetup = true;
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

