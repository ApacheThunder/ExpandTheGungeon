using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandObjects;
using System.Linq;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandFloorDecorator : MonoBehaviour {

        public ExpandFloorDecorator() { }
        

        public void PlaceFloorDecoration(Dungeon dungeon, List<RoomHandler> roomListOverride = null, bool ignoreTilesetType = false) {

            if (!ignoreTilesetType && dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.BELLYGEON &&
                dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.WESTGEON)
            {
                return;
            }
            
            int RandomObjectsPlaced = 0;
            int RandomObjectsSkipped = 0;
            int MaxObjectsPerRoom = 12;

            ExpandObjectDatabase objectDatabase = new ExpandObjectDatabase();

            if ((dungeon.data.rooms == null | dungeon.data.rooms.Count <= 0) && roomListOverride == null) { return; }

            List<RoomHandler> DungeonRooms = dungeon.data.rooms;

            if (roomListOverride != null) { DungeonRooms = roomListOverride; }
            
            foreach (RoomHandler currentRoom in DungeonRooms) {                 
                try {
                    if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                        PlaceRandomCacti(dungeon, currentRoom, objectDatabase, MaxObjectsPerRoom, RandomObjectsPlaced, RandomObjectsSkipped);
                    } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.BELLYGEON) {
                        PlaceRandomCorpses(dungeon, currentRoom, objectDatabase, MaxObjectsPerRoom, RandomObjectsPlaced, RandomObjectsSkipped);
                    }
                } catch (System.Exception ex) {
                    if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] Exception while setting up or objects for current room", false);
                    if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] Skipping current room...", false);
                    if (ExpandStats.debugMode) { ETGModConsole.Log(ex.Message + ex.StackTrace + ex.Source, false); }
                }
            }
            if (ExpandStats.debugMode) {
                ETGModConsole.Log("[DEBUG] Max Number of floor decoration objects assignable per room: " + MaxObjectsPerRoom, false);
                ETGModConsole.Log("[DEBUG] Number of floor decoration objects placed: " + RandomObjectsPlaced, false);
                ETGModConsole.Log("[DEBUG] Number of floor decoration objects skipped: " + RandomObjectsSkipped, false);
                if (RandomObjectsPlaced <= 0) { ETGModConsole.Log("[DEBUG] Warning: No corpse objects have been placed!", false); }
            }
            objectDatabase = null;
            return;
        }

        private void PlaceRandomCorpses(Dungeon dungeon, RoomHandler currentRoom, ExpandObjectDatabase objectDatabase, int MaxObjectsPerRoom, int RandomObjectsPlaced, int RandomObjectsSkipped) {
            PrototypeDungeonRoom.RoomCategory roomCategory = currentRoom.area.PrototypeRoomCategory;
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
                                GameObject SkeletonCorpse = Instantiate(ExpandPrefabs.Sarco_Skeleton, RandomVector.Value.ToVector3(), Quaternion.identity);
                                SkeletonCorpse.GetComponent<tk2dSprite>().HeightOffGround = -1;
                                SkeletonCorpse.GetComponent<tk2dSprite>().UpdateZDepth();
                                SkeletonCorpse.transform.parent = currentRoom.hierarchyParent;
                                RandomObjectsPlaced++;
                            } else {
                                GameObject WrithingBulletManCorpse = objectDatabase.WrithingBulletman.InstantiateObject(currentRoom, (RandomVector.Value - currentRoom.area.basePosition));
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

        private void PlaceRandomCacti(Dungeon dungeon, RoomHandler currentRoom, ExpandObjectDatabase objectDatabase, int MaxObjectPerRoom, int RandomObjectsPlaced, int RandomObjectsSkipped) {
            PrototypeDungeonRoom.RoomCategory roomCategory = currentRoom.area.PrototypeRoomCategory;
            if (currentRoom != null && !string.IsNullOrEmpty(currentRoom.GetRoomName()) && !currentRoom.IsMaintenanceRoom() &&
                !currentRoom.GetRoomName().StartsWith("Boss Foyer") && currentRoom.RoomVisualSubtype == 0)
            {
                if (Random.value <= 0.8f && roomCategory != PrototypeDungeonRoom.RoomCategory.REWARD) {
            
                    List<IntVector2> m_CachedPositions = new List<IntVector2>();
                    int MaxCactiCount = MaxObjectPerRoom;
                    if (Random.value <= 0.3f){
                        MaxCactiCount = 20;
                    } else if (roomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) {
                        MaxCactiCount = 10;
                    }

                    int CactusCount = Random.Range(6, MaxCactiCount);
                    
                    for (int i = 0; i < CactusCount; i++) {
                        IntVector2? RandomVector = GetRandomAvailableCell(dungeon, currentRoom, m_CachedPositions, ExitClearence: 3, avoidExits: true);

                        List<GameObject> CactiList = new List<GameObject>() { ExpandPrefabs.Cactus_A, ExpandPrefabs.Cactus_B };
                        CactiList = CactiList.Shuffle();

                        if (RandomVector.HasValue) {
                            GameObject Cactus = Instantiate(BraveUtility.RandomElement(CactiList), RandomVector.Value.ToVector3(), Quaternion.identity);
                            Cactus.transform.parent = currentRoom.hierarchyParent;
                            RandomObjectsPlaced++;
                            if (m_CachedPositions.Count <= 0) { break; }
                        } else {
                            RandomObjectsSkipped++;
                        }
                    }
                }
            }
        }


        private IntVector2? GetRandomAvailableCell(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, int Clearence = 2, int ExitClearence = 10, bool avoidExits = false, bool avoidPits = true, bool PositionRelativeToRoom = false, bool isCactusDecoration = false) {
            if (dungeon == null | currentRoom == null | validCellsCached == null) { return null; }            
            if (validCellsCached.Count == 0) {
                for (int X = 0; X < currentRoom.area.dimensions.x; X++) {
                    for (int Y = 0; Y < currentRoom.area.dimensions.y; Y++) {
                        bool isInvalid = false;
                        IntVector2 TargetPosition = new IntVector2(currentRoom.area.basePosition.x + X, currentRoom.area.basePosition.y + Y);
                        if (isCactusDecoration && GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(TargetPosition)) {
                            if (!GameManager.Instance.Dungeon.data.isPlainEmptyCell(TargetPosition.X, TargetPosition.Y)) {
                                isInvalid = true;
                                break;
                            }
                        }

                        if (avoidExits) {
                            for(int x = (0 - ExitClearence); x <= ExitClearence; x++) {
                                for(int y = (0 - ExitClearence); y <= ExitClearence; y++) {
                                    IntVector2 targetArea1 = (TargetPosition + new IntVector2(x,  y));
                                    if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(targetArea1)) {
                                        CellData cellData = GameManager.Instance.Dungeon.data[targetArea1];
                                        if (cellData.isExitCell | cellData.isDoorFrameCell) {
                                            isInvalid = true;
                                            break;
                                        }
                                    }
                                }
                                if (isInvalid) { break; }
                            }
                        }
                        for(int x = (0 - Clearence); x <= Clearence; x++) {
                            for(int y = (0 - Clearence); y <= Clearence; y++) {
                                IntVector2 targetArea1 = (TargetPosition + new IntVector2(x, y));
                                if (dungeon.data.CheckInBoundsAndValid(targetArea1)) {
                                    CellData cellData = dungeon.data[targetArea1];
                                    if (avoidPits && dungeon.data.isPit(targetArea1.x, targetArea1.y)) {
                                        isInvalid = true;
                                        break;
                                    }
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
                        if (!isInvalid && dungeon.data.CheckInBoundsAndValid(TargetPosition) && !validCellsCached.Contains(TargetPosition)) {
                            validCellsCached.Add(TargetPosition);
                        }
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

