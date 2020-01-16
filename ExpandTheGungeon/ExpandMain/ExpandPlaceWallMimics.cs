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
            int iterations = 0;

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

            try {
                int currentFloor = GameManager.Instance.CurrentFloor;
                int numWallMimicsForFloor = MetaInjectionData.GetNumWallMimicsForFloor(dungeon.tileIndices.tilesetId);

                GameManager.LevelOverrideState levelOverrideState = GameManager.Instance.CurrentLevelOverrideState;
                
                if (levelOverrideState != GameManager.LevelOverrideState.NONE | levelOverrideState == GameManager.LevelOverrideState.TUTORIAL) {
                    if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] This floor has been excluded from having Wall Mimics", false); }
                    return;
                }

                if (ExpandStats.debugMode) {
                    ETGModConsole.Log("[DEBUG] Current Floor: " + currentFloor, false);
                    ETGModConsole.Log("[DEBUG] Wall Mimics assigned by RewardManager: " + numWallMimicsForFloor, false);
                }

                if (ExpandTheGungeon.isGlitchFloor && dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.PHOBOSGEON) {
                    dungeon.DungeonFloorName = "A Corrupted Place.";
                    dungeon.DungeonShortName = "A Corrupted Place.";
                    dungeon.DungeonFloorLevelTextOverride = "Beneath the Melting Permafrost.";
                }


                /*if (!ExpandStats.allowGlitchFloor && GameManager.Instance.PrimaryPlayer.HasPickupID(316)) {
                    if (!ExpandSharedHooks.IsHooksInstalled) { ExpandSharedHooks.InstallPrimaryHooks(); }
                    ExpandStats.allowGlitchFloor = true;
                }*/


                SetupSecretDoorDestinations(dungeon);

                if (currentFloor < 4) { PlaceGlitchElevator(dungeon); }

                ExpandJunkEnemySpawneer m_ExpandJunkEnemySpawneer = new ExpandJunkEnemySpawneer();
                m_ExpandJunkEnemySpawneer.PlaceRandomJunkEnemies(dungeon, roomHandler);
                Destroy(m_ExpandJunkEnemySpawneer);

                if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.PHOBOSGEON | dungeon.IsGlitchDungeon) {
                    ETGMod.AIActor.OnPreStart = (Action<AIActor>)Delegate.Combine(ETGMod.AIActor.OnPreStart, new Action<AIActor>(EnemyModRandomizer));
                } else {
                    ETGMod.AIActor.OnPreStart = (Action<AIActor>)Delegate.Remove(ETGMod.AIActor.OnPreStart, new Action<AIActor>(EnemyModRandomizer));
                }

                ExpandPlaceCorruptTiles m_CorruptTilePlayer = new ExpandPlaceCorruptTiles();
                m_CorruptTilePlayer.PlaceCorruptTiles(dungeon);
                Destroy(m_CorruptTilePlayer);

                PlaceAlarmMushRooms(dungeon);
                

                if (numWallMimicsForFloor <= 0) {
            		if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] There will be no Wall Mimics assigned to this floor.", false); }
            		return;
            	}
            	
            	if (levelOverrideState == GameManager.LevelOverrideState.RESOURCEFUL_RAT) {
            		if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] The Resourceful Rat Maze has been excluded from having wall mimics.", false); }
            		return;
            	}
            	
            	if (ExpandStats.debugMode) { ETGModConsole.Log("[DEBUG] Wall Mimics Assigned to Floor: " + numWallMimicsForFloor, false); }

                List<int> roomList = Enumerable.Range(0, dungeon.data.rooms.Count).ToList();
                roomList = roomList.Shuffle();
            	
            	if (roomHandler != null) { roomList = new List<int>(new int[] { dungeon.data.rooms.IndexOf(roomHandler) }); }
            	
            	List<Tuple<IntVector2, DungeonData.Direction>> validWalls = new List<Tuple<IntVector2, DungeonData.Direction>>();
            	List<AIActor> enemiesList = new List<AIActor>();

            	while (iterations < roomList.Count && WallMimicsPlaced < numWallMimicsForFloor) {
            		RoomHandler currentRoom = dungeon.data.rooms[roomList[iterations]];
            		if (!currentRoom.IsShop) {
            			if (!currentRoom.area.IsProceduralRoom || currentRoom.area.proceduralCells == null) {
            				if (currentRoom.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS || !BraveUtility.RandomBool()) {
            					if (!currentRoom.GetRoomName().StartsWith("DraGunRoom") && !currentRoom.IsMaintenanceRoom() && !BannedWallMimicRoomList.Contains(currentRoom.GetRoomName().ToLower())) {
            						if (currentRoom.connectedRooms != null) {
            							for (int i = 0; i < currentRoom.connectedRooms.Count; i++) {
            								if (currentRoom.connectedRooms[i] == null || currentRoom.connectedRooms[i].area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) { }
            							}
            						}
            						if (roomHandler == null) {
            							bool MaxMimicCountReached = false;
            							currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All, ref enemiesList);
                                        for (int j = 0; j < enemiesList.Count; j++) {
            								AIActor aiactor = enemiesList[j];
            								if (aiactor && aiactor.EnemyGuid == GameManager.Instance.RewardManager.WallMimicChances.EnemyGuid) {
                                                MaxMimicCountReached = true;
                                                break;
                                            }
                                        }
            							if (MaxMimicCountReached) { goto IL_EXIT; }
            						}
            						validWalls.Clear();
            						for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
            							for (int Height = -1; Height <= currentRoom.area.dimensions.y; Height++) {
            								int X = currentRoom.area.basePosition.x + Width;
            								int Y = currentRoom.area.basePosition.y + Height;
            								if (dungeon.data.isWall(X, Y) && X % 4 == 0 && Y % 4 == 0) {
            									int WallCount = 0;
            									if (!dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) &&
            										!dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) &&
            										dungeon.data.isWall(X - 1, Y) && dungeon.data.isWall(X, Y) && dungeon.data.isWall(X + 1, Y) && dungeon.data.isWall(X + 2, Y) && 
            										dungeon.data.isWall(X - 1, Y - 1) && dungeon.data.isWall(X, Y - 1) && dungeon.data.isWall(X + 1, Y - 1) && dungeon.data.isWall(X + 2, Y - 1) &&
            										!dungeon.data.isPlainEmptyCell(X - 1, Y - 3) && !dungeon.data.isPlainEmptyCell(X, Y - 3) && !dungeon.data.isPlainEmptyCell(X + 1, Y - 3) && !dungeon.data.isPlainEmptyCell(X + 2, Y - 3))
            									{
            										validWalls.Add(Tuple.Create(new IntVector2(X, Y), DungeonData.Direction.NORTH));
            										WallCount++;
            										SouthWallCount++;
            									} else if (dungeon.data.isWall(X - 1, Y + 2) && dungeon.data.isWall(X, Y + 2) && dungeon.data.isWall(X + 1, Y + 2) && dungeon.data.isWall(X + 2, Y + 2) &&
            											dungeon.data.isWall(X - 1, Y + 1) && dungeon.data.isWall(X, Y + 1) && dungeon.data.isWall(X + 1, Y + 1) && dungeon.data.isWall(X + 2, Y + 1) && 
            											dungeon.data.isWall(X - 1, Y) && dungeon.data.isWall(X, Y) && dungeon.data.isWall(X + 1, Y) && dungeon.data.isWall(X + 2, Y) &&
            											dungeon.data.isPlainEmptyCell(X, Y - 1) && dungeon.data.isPlainEmptyCell(X + 1, Y - 1) &&
            											!dungeon.data.isPlainEmptyCell(X, Y + 4) && !dungeon.data.isPlainEmptyCell(X + 1, Y + 4))
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
            						if (roomHandler == null) {
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
            						        AIActor.Spawn(orLoadByGuid, Position, currentRoom, true, AIActor.AwakenAnimationType.Default, true);
                                            validWalls.Remove(WallCell);
                                            WallMimicsPlaced++;
                                        }
            						}
            					}
            				}
            			}
            		}
            		IL_EXIT:
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
        }
             
        private void PlaceSecretRatGrate(Dungeon dungeon) {
            if (dungeon.IsGlitchDungeon | GameManager.Instance.CurrentGameMode == GameManager.GameMode.BOSSRUSH | GameManager.Instance.CurrentGameMode == GameManager.GameMode.SUPERBOSSRUSH) {
                return;
            }
            PlaceSecretRatGrateInternal(dungeon, new IntVector2(4, 4), Vector2.zero);
        }
        private void PlaceSecretRatGrateInternal(Dungeon dungeon, IntVector2 dimensions, Vector2 offset) {
            List<IntVector2> list = new List<IntVector2>();
            for (int i = 0; i < dungeon.data.rooms.Count; i++) {
                RoomHandler roomHandler = dungeon.data.rooms[i];
                if (!roomHandler.area.IsProceduralRoom && roomHandler.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.NORMAL && !roomHandler.OptionalDoorTopDecorable && !roomHandler.area.prototypeRoom.UseCustomMusic) {
                    for (int j = roomHandler.area.basePosition.x; j < roomHandler.area.basePosition.x + roomHandler.area.dimensions.x; j++) {
                        for (int k = roomHandler.area.basePosition.y; k < roomHandler.area.basePosition.y + roomHandler.area.dimensions.y; k++) {
                            if (ClearForRatGrate(dungeon, dimensions.x, dimensions.y, j, k)) { list.Add(new IntVector2(j, k)); }
                        }
                    }
                }
            }
            if (list.Count > 0) {
                IntVector2 a = list[BraveRandom.GenerationRandomRange(0, list.Count)];
                RoomHandler absoluteRoom = a.ToVector2().GetAbsoluteRoom();
                GameObject gameObject = ExpandPrefabs.EXTrapDoor.GetComponent<ExpandGlitchTrapDoor>().InstantiateObject(absoluteRoom, a - absoluteRoom.area.basePosition, true);
                gameObject.transform.position += offset.ToVector3ZUp(0f);
                ExpandGlitchTrapDoor glitchTrapDoor = gameObject.GetComponent<ExpandGlitchTrapDoor>();
                glitchTrapDoor.ConfigureOnPlacement(absoluteRoom);
                for (int m = 0; m < dimensions.x; m++) {
                    for (int n = 0; n < dimensions.y; n++) {
                        IntVector2 intVector = a + new IntVector2(m, n);
                        if (dungeon.data.CheckInBoundsAndValid(intVector)) { dungeon.data[intVector].cellVisualData.floorTileOverridden = true; }
                    }
                }
            }
        }
        private bool ClearForRatGrate(Dungeon dungeon, int dmx, int dmy, int bpx, int bpy) {
            int num = -1;
            for (int i = 0; i < dmx; i++) {
                for (int j = 0; j < dmy; j++) {
                    IntVector2 intVector = new IntVector2(bpx + i, bpy + j);
                    if (!dungeon.data.CheckInBoundsAndValid(intVector)) { return false; }
                    CellData cellData = dungeon.data[intVector];
                    if (num == -1) {
                        num = cellData.cellVisualData.roomVisualTypeIndex;
                        if (num != 0 && num != 1) { return false; }
                    }
                    if (cellData.parentRoom == null || cellData.parentRoom.IsMaintenanceRoom() || cellData.type != CellType.FLOOR || cellData.isOccupied || !cellData.IsPassable || cellData.containsTrap || cellData.IsTrapZone) {
                        return false;
                    }
                    if (cellData.cellVisualData.roomVisualTypeIndex != num || cellData.HasPitNeighbor(dungeon.data) || cellData.PreventRewardSpawn || cellData.cellVisualData.isPattern || cellData.cellVisualData.IsPhantomCarpet) {
                        return false;
                    }
                    if (cellData.cellVisualData.floorType == CellVisualData.CellFloorType.Water || cellData.cellVisualData.floorType == CellVisualData.CellFloorType.Carpet || cellData.cellVisualData.floorTileOverridden) {
                        return false;
                    }
                    if (cellData.doesDamage || cellData.cellVisualData.preventFloorStamping || cellData.cellVisualData.hasStampedPath || cellData.forceDisallowGoop) {
                        return false;
                    }
                }
            }
            return true;
        }
        
        private void PlaceAlarmMushRooms(Dungeon dungeon, int Clearence = 3) {

            if (dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.MINEGEON) { return; }

            int MushroomCount = BraveRandom.GenerationRandomRange(10, 25);

            DungeonPlaceable MinesEnemySpawns = ScriptableObject.CreateInstance<DungeonPlaceable>();
            MinesEnemySpawns.width = 1;
            MinesEnemySpawns.height = 1;
            MinesEnemySpawns.isPassable = true;
            MinesEnemySpawns.roomSequential = false;
            MinesEnemySpawns.respectsEncounterableDifferentiator = false;
            MinesEnemySpawns.UsePrefabTransformOffset = false;
            MinesEnemySpawns.MarkSpawnedItemsAsRatIgnored = false;
            MinesEnemySpawns.DebugThisPlaceable = false;
            MinesEnemySpawns.IsAnnexTable = false;
            MinesEnemySpawns.variantTiers = new List<DungeonPlaceableVariant>() {
                new DungeonPlaceableVariant() {
                    percentChance = 50,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 25,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "f905765488874846b7ff257ff81d6d0c", // fungun
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 25,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 25,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "db35531e66ce41cbb81d507a34366dfe", // ak47_bullet_kin
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 50,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 60,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "3cadf10c489b461f9fb8814abc1a09c1", // minelet
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 30,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "df7fb62405dc4697b7721862c7b6b3cd", // treadnaughts_bullet_kin
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                }
            };

            
            List<IntVector2> spawnList = new List<IntVector2>();
            for (int i = 0; i < dungeon.data.rooms.Count; i++) {
                RoomHandler roomHandler = dungeon.data.rooms[i];
                if (!roomHandler.area.IsProceduralRoom && roomHandler.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.NORMAL && !roomHandler.OptionalDoorTopDecorable && !roomHandler.area.prototypeRoom.UseCustomMusic) {
                    for (int X = roomHandler.area.basePosition.x; X < roomHandler.area.basePosition.x + roomHandler.area.dimensions.x; X++) {
                        for (int Y = roomHandler.area.basePosition.y; Y < roomHandler.area.basePosition.y + roomHandler.area.dimensions.y; Y++) {
                            if (ClearForAlarmMushroom(dungeon, Clearence, Clearence, X, Y)) { spawnList.Add(new IntVector2(X, Y)); }
                        }
                    }
                }
            }
            for (int i = 0; i < MushroomCount; i++) {
                if (spawnList.Count > 0) {
                    IntVector2 RandomSpawn = spawnList[BraveRandom.GenerationRandomRange(0, spawnList.Count)];
                    RoomHandler absoluteRoom = RandomSpawn.ToVector2().GetAbsoluteRoom();
                    spawnList.Remove(RandomSpawn);

                    GameObject alarmMushroomObject = ExpandPrefabs.EXAlarmMushroom.GetComponent<ExpandAlarmMushroomPlacable>().InstantiateObject(absoluteRoom, RandomSpawn - absoluteRoom.area.basePosition, true);
                    alarmMushroomObject.transform.parent = absoluteRoom.hierarchyParent;

                    ExpandAlarmMushroomPlacable m_AlarmMushRoomPlacable = ExpandPrefabs.EXAlarmMushroom.GetComponent<ExpandAlarmMushroomPlacable>();
                    m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride = MinesEnemySpawns;
                    m_AlarmMushRoomPlacable.ConfigureOnPlacement(absoluteRoom);
                    if (spawnList.Count <= 0) { return; }
                }
            }
        }

        private bool ClearForAlarmMushroom(Dungeon dungeon, int dmx, int dmy, int bpx, int bpy) {
            for (int i = 0; i < dmx; i++) {
                for (int j = 0; j < dmy; j++) {
                    IntVector2 intVector = new IntVector2(bpx + i, bpy + j);
                    if (!dungeon.data.CheckInBoundsAndValid(intVector)) { return false; }
                    CellData cellData = dungeon.data[intVector];
                    if (cellData.parentRoom == null || cellData.parentRoom.IsMaintenanceRoom() || cellData.parentRoom == dungeon.data.Entrance ||
                        cellData.parentRoom.IsShop || cellData.parentRoom.IsDarkAndTerrifying ||  cellData.parentRoom.IsSecretRoom || 
                        string.IsNullOrEmpty(cellData.parentRoom.GetRoomName()) || cellData.parentRoom.GetRoomName().ToLower().StartsWith("boss") ||
                        cellData.parentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS ||
                        cellData.type != CellType.FLOOR ||  cellData.isOccupied || cellData.isExitCell || cellData.isExitNonOccluder || 
                        cellData.isDoorFrameCell || cellData.isNextToWall || cellData.isWallMimicHideout ||
                        !cellData.IsPassable || cellData.containsTrap ||  cellData.IsTrapZone)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void PlaceGlitchElevator(Dungeon dungeon) {

            GameManager.LevelOverrideState levelOverrideState = GameManager.Instance.CurrentLevelOverrideState;

            if (dungeon.IsGlitchDungeon | ExpandTheGungeon.isGlitchFloor | ExpandStats.elevatorHasBeenUsed) { return; }
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
            if (UnityEngine.Random.value > 0.01f) { return; }

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
                    !currentRoom.GetRoomName().ToLower().StartsWith("gungeon_rewardroom") && !currentRoom.GetRoomName().ToLower().StartsWith("reward room"))
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
        								if (dungeon.data.isWall(X, Y)) {
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

            if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.PHOBOSGEON) {
                if (targetActor.EnemyGuid == "ba928393c8ed47819c2c5f593100a5bc") {
                    if (ExpandPrefabs.StoneCubeCollection_West == null) {
                        ExpandPrefabs.StoneCubeCollection_West = ExpandUtility.BuildSpriteCollection(EnemyDatabase.GetOrLoadByGuid("ba928393c8ed47819c2c5f593100a5bc").sprite.Collection, ExpandPrefabs.StoneCubeWestTexture, null, null, false);
                    }
                    ExpandUtility.ApplyCustomTexture(targetActor, prebuiltCollection: ExpandPrefabs.StoneCubeCollection_West);
                }
            }

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

