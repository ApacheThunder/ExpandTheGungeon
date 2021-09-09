using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandObjects;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandFloorDecorator {
        
        private static int RandomObjectsPlaced = 0;
        private static int RandomObjectsSkipped = 0;

        public static void PlaceFloorDecoration(Dungeon dungeon, List<RoomHandler> roomListOverride = null, bool ignoreTilesetType = false) {

            List<GlobalDungeonData.ValidTilesets> ValidTilesets = new List<GlobalDungeonData.ValidTilesets>() {
                GlobalDungeonData.ValidTilesets.BELLYGEON,
                GlobalDungeonData.ValidTilesets.WESTGEON,
                GlobalDungeonData.ValidTilesets.MINEGEON
            };

            if (!ignoreTilesetType && !ValidTilesets.Contains(dungeon.tileIndices.tilesetId)) { return; }
            
            if ((dungeon.data.rooms == null | dungeon.data.rooms.Count <= 0) && roomListOverride == null) { return; }

            List<RoomHandler> DungeonRooms = dungeon.data.rooms;

            if (roomListOverride != null) { DungeonRooms = roomListOverride; }
            
            foreach (RoomHandler currentRoom in DungeonRooms) {                 
                try {
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                        PlaceRandomCacti(dungeon, currentRoom);
                    } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.BELLYGEON) {
                        PlaceRandomCorpses(dungeon, currentRoom);
                    } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.MINEGEON) {
                        PlaceRandomAlarmMushrooms(dungeon, currentRoom);
                    }
                } catch (System.Exception ex) {                    
                    if (ExpandStats.debugMode && currentRoom != null && !string.IsNullOrEmpty(currentRoom.GetRoomName())) {
                        if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] Exception while setting up objects for room: " + currentRoom.GetRoomName(), false);
                    } else if (ExpandStats.debugMode) {
                        if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] Exception while setting up objects for current room", false);
                    }
                    if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] Skipping current room...", false);
                    if (ExpandStats.debugMode) { ETGModConsole.Log(ex.Message + ex.StackTrace + ex.Source, false); }
                    RandomObjectsPlaced = 0;
                    RandomObjectsSkipped = 0;
                }
            }
            if (ExpandStats.debugMode) {
                ETGModConsole.Log("[DEBUG] Number of floor decoration objects placed: " + RandomObjectsPlaced, false);
                ETGModConsole.Log("[DEBUG] Number of floor decoration objects skipped: " + RandomObjectsSkipped, false);
                if (RandomObjectsPlaced <= 0) { ETGModConsole.Log("[DEBUG] Warning: No decoration objects have been placed!", false); }
            }
            RandomObjectsPlaced = 0;
            RandomObjectsSkipped = 0;
            return;
        }

        private static void PlaceRandomCorpses(Dungeon dungeon, RoomHandler currentRoom) {
            PrototypeDungeonRoom.RoomCategory roomCategory = currentRoom.area.PrototypeRoomCategory;

            int MaxObjectsPerRoom = 12;

            if (currentRoom != null && !string.IsNullOrEmpty(currentRoom.GetRoomName()) && !currentRoom.IsMaintenanceRoom() &&
                !currentRoom.GetRoomName().StartsWith("Boss Foyer"))
            {
                if (Random.value <= 0.6f && roomCategory != PrototypeDungeonRoom.RoomCategory.ENTRANCE && roomCategory != PrototypeDungeonRoom.RoomCategory.REWARD) {
            
                    List<IntVector2> m_CachedPositions = new List<IntVector2>();
                    int MaxCorpseCount = MaxObjectsPerRoom;
                    if (Random.value <= 0.3f){
                        MaxCorpseCount = 20;
                    } else if (roomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) {
                        MaxCorpseCount = 17;
                    }

                    int CorpseCount = Random.Range(6, MaxObjectsPerRoom);
                    
                    for (int i = 0; i < CorpseCount; i++) {
                        IntVector2? RandomVector = GetRandomAvailableCell(dungeon, currentRoom, m_CachedPositions);

                        if (RandomVector.HasValue) {
                            if (Random.value <= 0.08f) {
                                GameObject SkeletonCorpse = Object.Instantiate(ExpandPrefabs.Sarco_Skeleton, RandomVector.Value.ToVector3(), Quaternion.identity);
                                SkeletonCorpse.GetComponent<tk2dSprite>().HeightOffGround = -1;
                                SkeletonCorpse.GetComponent<tk2dSprite>().UpdateZDepth();
                                if (BraveUtility.RandomBool()) { SkeletonCorpse.GetComponent<tk2dSprite>().FlipX = true; }
                                SkeletonCorpse.transform.parent = currentRoom.hierarchyParent;
                                RandomObjectsPlaced++;
                            } else {
                                GameObject WrithingBulletManCorpse = ExpandObjectDatabase.WrithingBulletman.InstantiateObject(currentRoom, (RandomVector.Value - currentRoom.area.basePosition));
                                WrithingBulletManCorpse.transform.parent = currentRoom.hierarchyParent;
                                RandomObjectsPlaced++;
                            }
                            if (m_CachedPositions.Count <= 0) { break; }
                        } else {
                            RandomObjectsSkipped++;
                        }
                    }
                }
            }
        }

        private static void PlaceRandomCacti(Dungeon dungeon, RoomHandler currentRoom) {
            PrototypeDungeonRoom.RoomCategory roomCategory = currentRoom.area.PrototypeRoomCategory;

            if (currentRoom == null | roomCategory == PrototypeDungeonRoom.RoomCategory.REWARD | currentRoom.IsMaintenanceRoom() |
               string.IsNullOrEmpty(currentRoom.GetRoomName()) | currentRoom.GetRoomName().StartsWith("Boss Foyer") |
               currentRoom.RoomVisualSubtype != 0 | !currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear))
            {
                return;
            }
            
            if (Random.value <= 0.8f) {
            
                List<IntVector2> m_CachedPositions = new List<IntVector2>();
                int MaxCactiCount = 12;
                int MinCactiCount = 6;
                if (Random.value <= 0.3f){
                    MaxCactiCount = 20;
                } else if (roomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) {
                    MaxCactiCount = 10;
                }

                int X = currentRoom.area.dimensions.x;
                int Y = currentRoom.area.dimensions.y;
                
                if (X * Y < 100) {
                    MinCactiCount = 3;
                    MaxCactiCount = 6;
                }

                if (!string.IsNullOrEmpty(currentRoom.GetRoomName()) && currentRoom.GetRoomName().ToLower().StartsWith("expand_west_canyon1_tiny")) {
                    MinCactiCount = 1;
                    MaxCactiCount = 3;
                }
                
                int CactusCount = Random.Range(MinCactiCount, MaxCactiCount);

                for (int i = 0; i < CactusCount; i++) {
                    IntVector2? RandomVector = GetRandomAvailableCell(dungeon, currentRoom, m_CachedPositions, ExitClearence: 3, avoidExits: true);

                    List<GameObject> CactiList = new List<GameObject>() { ExpandPrefabs.Cactus_A, ExpandPrefabs.Cactus_B };
                    CactiList = CactiList.Shuffle();

                    if (RandomVector.HasValue) {
                        GameObject Cactus = Object.Instantiate(BraveUtility.RandomElement(CactiList), RandomVector.Value.ToVector3(), Quaternion.identity);
                        Cactus.transform.parent = currentRoom.hierarchyParent;
                        RandomObjectsPlaced++;
                        if (m_CachedPositions.Count <= 0) { break; }
                    } else {
                        RandomObjectsSkipped++;
                    }
                }
            }
        }
        
        private static void PlaceRandomAlarmMushrooms(Dungeon dungeon, RoomHandler currentRoom) {
            PrototypeDungeonRoom.RoomCategory roomCategory = currentRoom.area.PrototypeRoomCategory;
            
            if (currentRoom == null | roomCategory == PrototypeDungeonRoom.RoomCategory.REWARD | string.IsNullOrEmpty(currentRoom.GetRoomName()) |
                currentRoom.GetRoomName().StartsWith("Boss Foyer") | currentRoom.IsMaintenanceRoom() |
               !currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) | roomCategory == PrototypeDungeonRoom.RoomCategory.BOSS)
            {
                return;
            }

            if (currentRoom != null && !string.IsNullOrEmpty(currentRoom.GetRoomName()) && !currentRoom.IsMaintenanceRoom() &&
                !currentRoom.GetRoomName().StartsWith("Boss Foyer") && currentRoom.RoomVisualSubtype == 0)
            {
                if (Random.value <= 0.6f) {
            
                    List<IntVector2> m_CachedPositions = new List<IntVector2>();
                    int MinMushroomCount = 2;
                    int MaxMushroomCount = 6;
                    if (Random.value <= 0.3f) {
                        MinMushroomCount = 6;
                        MaxMushroomCount = 12;
                    } else if (roomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) {
                        MaxMushroomCount = 10;
                    }

                    int X = currentRoom.area.dimensions.x;
                    int Y = currentRoom.area.dimensions.y;
                    
                    if (X * Y < 100) {
                        MinMushroomCount = 1;
                        MaxMushroomCount = 3;
                    }

                    int MushroomCount = Random.Range(MinMushroomCount, MaxMushroomCount);
                    
                    for (int i = 0; i < MushroomCount; i++) {
                        IntVector2? RandomVector = GetRandomAvailableCell(dungeon, currentRoom, m_CachedPositions, ExitClearence: 3, avoidExits: true, PositionRelativeToRoom: true);
                        
                        if (RandomVector.HasValue) {
                            GameObject alarmMushroomObject = ExpandPrefabs.EXAlarmMushroom.GetComponent<ExpandAlarmMushroomPlacable>().InstantiateObject(currentRoom, RandomVector.Value, true);
                            alarmMushroomObject.transform.parent = currentRoom.hierarchyParent;
                            ExpandAlarmMushroomPlacable m_AlarmMushRoomPlacable = ExpandPrefabs.EXAlarmMushroom.GetComponent<ExpandAlarmMushroomPlacable>();
                            m_AlarmMushRoomPlacable.ConfigureOnPlacement(currentRoom);
                            RandomObjectsPlaced++;
                            if (m_CachedPositions.Count <= 0) { break; }
                        } else {
                            RandomObjectsSkipped++;
                        }
                    }
                }
            }
        }

        private static IntVector2? GetRandomAvailableCell(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, int Clearence = 2, int ExitClearence = 10, bool avoidExits = false, bool avoidPits = true, bool PositionRelativeToRoom = false, bool isCactusDecoration = false) {
            if (dungeon == null | currentRoom == null | validCellsCached == null) { return null; }            
            if (validCellsCached.Count == 0) {
                for (int X = 0; X < currentRoom.area.dimensions.x; X++) {
                    for (int Y = 0; Y < currentRoom.area.dimensions.y; Y++) {
                        bool isInvalid = false;
                        IntVector2 TargetPosition = new IntVector2(currentRoom.area.basePosition.x + X, currentRoom.area.basePosition.y + Y);

                        if (!dungeon.data.CheckInBoundsAndValid(TargetPosition)) { isInvalid = true; break; }

                        // Try and detect those pesky untrimmed rooms. :P
                        RoomHandler ActualRoom = dungeon.data.GetAbsoluteRoomFromPosition(TargetPosition);
                        if (ActualRoom == null | ActualRoom != currentRoom) { isInvalid = true; break; }
                        // if (dungeon.data[TargetPosition].parentRoom != currentRoom) { isInvalid = true; break; }

                        if (isCactusDecoration) { if (!dungeon.data.isPlainEmptyCell(TargetPosition.X, TargetPosition.Y)) { isInvalid = true; break; } }

                        if (avoidExits && ExitClearence > 0) {
                            for(int x = (0 - ExitClearence); x <= ExitClearence; x++) {
                                for(int y = (0 - ExitClearence); y <= ExitClearence; y++) {
                                    IntVector2 targetArea1 = (TargetPosition + new IntVector2(x,  y));
                                    if (dungeon.data.CheckInBoundsAndValid(targetArea1)) {
                                        CellData cellData = dungeon.data[targetArea1];
                                        if (cellData.isExitCell | cellData.isDoorFrameCell) { isInvalid = true; break; }
                                    }
                                }
                                if (isInvalid) { break; }
                            }
                        }

                        if (Clearence > 0) { 
                            for(int x = (0 - Clearence); x <= Clearence; x++) {
                                for(int y = (0 - Clearence); y <= Clearence; y++) {
                                    IntVector2 targetArea1 = (TargetPosition + new IntVector2(x, y));
                                    if (dungeon.data.CheckInBoundsAndValid(targetArea1)) {
                                        CellData cellData = dungeon.data[targetArea1];
                                        if (avoidPits && dungeon.data.isPit(targetArea1.x, targetArea1.y)) { isInvalid = true; break; }
                                        if (cellData.isWallMimicHideout | cellData.IsAnyFaceWall() | cellData.IsFireplaceCell |
                                            cellData.IsTopWall() | cellData.isOccludedByTopWall | cellData.IsUpperFacewall() | 
                                            cellData.isWallMimicHideout | dungeon.data.isWall(targetArea1.x, targetArea1.y) | 
                                            dungeon.data[targetArea1.x, targetArea1.y].isOccupied)
                                        {
                                            isInvalid = true;
                                            break;
                                        }
                                    }
                                }
                                if (isInvalid) { break; }
                            }
                        }
                        if (!isInvalid && !validCellsCached.Contains(TargetPosition)) { validCellsCached.Add(TargetPosition); }
                    }
                }
            }
            if (validCellsCached.Count > 0) {
                IntVector2 SelectedCell = BraveUtility.RandomElement(validCellsCached);
                IntVector2 RegisteredCell = (SelectedCell);
                dungeon.data[RegisteredCell].isOccupied = true;
                validCellsCached.Remove(SelectedCell);
                if (PositionRelativeToRoom) { SelectedCell -= currentRoom.area.basePosition; }
                return SelectedCell;
            } else {
                return null;
            }
        }
    }
}

