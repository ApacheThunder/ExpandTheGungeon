using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandPlaceGlitchedEnemies {

        public static void PlaceRandomEnemies(Dungeon dungeon, int currentFloor, RoomHandler roomHandler = null) {
            
            if (!dungeon.IsGlitchDungeon && roomHandler == null) { return; }

            List<string> BannedRooms = new List<string>();

            foreach (WeightedRoom wRoom in ExpandPrefabs.MegaMiniBossRoomTable.includedRooms.elements) {
                if (wRoom.room != null) { BannedRooms.Add(wRoom.room.name); }
            }
            foreach (WeightedRoom wRoom in ExpandPrefabs.MegaBossRoomTable.includedRooms.elements) {
                if (wRoom.room != null) { BannedRooms.Add(wRoom.room.name); }
            }
            
            int RandomEnemiesPlaced = 0;
            int RandomEnemiesSkipped = 0;
            int MaxEnemies = 20;
            float GlitchedBossOdds = 0.15f;
            float BonusGlitchEnemyOdds = 0.05f;
            
            if (dungeon.IsGlitchDungeon) { MaxEnemies = 65; GlitchedBossOdds = 0.3f; BonusGlitchEnemyOdds = 0.28f; }

            List<RoomHandler> rooms = new List<RoomHandler>();

            if (roomHandler != null) {
                rooms.Add(roomHandler);
            } else {
                rooms = dungeon.data.rooms;
            }

            if (rooms == null | rooms.Count <= 0) { return; }

            foreach (RoomHandler currentRoom in rooms) {             
                PrototypeDungeonRoom.RoomCategory roomCategory = currentRoom.area.PrototypeRoomCategory;                
                try {
                    if (currentRoom != null && !string.IsNullOrEmpty(currentRoom.GetRoomName()) &&
                        currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) && !currentRoom.IsMaintenanceRoom() && 
                       !currentRoom.IsSecretRoom && !currentRoom.IsWinchesterArcadeRoom && !currentRoom.IsGunslingKingChallengeRoom &&
                       !currentRoom.GetRoomName().StartsWith("Boss Foyer") && !BannedRooms.Contains(currentRoom.GetRoomName()))
                    {
                        if (roomCategory != PrototypeDungeonRoom.RoomCategory.BOSS && roomCategory != PrototypeDungeonRoom.RoomCategory.ENTRANCE && 
                            roomCategory != PrototypeDungeonRoom.RoomCategory.REWARD && roomCategory != PrototypeDungeonRoom.RoomCategory.EXIT)
                        {
                            List<IntVector2> m_CachedPositions = new List<IntVector2>();
                            IntVector2? RandomGlitchEnemyVector = GetRandomAvailableCell(dungeon, currentRoom, m_CachedPositions);
                            IntVector2? RandomGlitchEnemyVector2 = null;
                            IntVector2? RandomGlitchEnemyVector3 = null;
                            IntVector2? RandomGlitchEnemyVector4 = null;

                            if (m_CachedPositions.Count > 0) { RandomGlitchEnemyVector2 = GetRandomAvailableCell(dungeon, currentRoom, m_CachedPositions); }
                            if (m_CachedPositions.Count > 0) { RandomGlitchEnemyVector3 = GetRandomAvailableCell(dungeon, currentRoom, m_CachedPositions, ExitClearence: 13); }
                            if (m_CachedPositions.Count > 0) { RandomGlitchEnemyVector4 = GetRandomAvailableCell(dungeon, currentRoom, m_CachedPositions); }
                            
                            
                            if (RandomGlitchEnemyVector.HasValue) {
                                ExpandGlitchedEnemies.Instance.SpawnRandomGlitchEnemy(currentRoom, RandomGlitchEnemyVector.Value, false, AIActor.AwakenAnimationType.Spawn);
                            } else { RandomEnemiesSkipped++; }

                            if (RandomGlitchEnemyVector2.HasValue && Random.value <= BonusGlitchEnemyOdds) {
                                ExpandGlitchedEnemies.Instance.SpawnRandomGlitchEnemy(currentRoom, RandomGlitchEnemyVector2.Value, false, AIActor.AwakenAnimationType.Spawn);
                            } else { RandomEnemiesSkipped++; }

                            if (RandomGlitchEnemyVector3.HasValue && Random.value <= GlitchedBossOdds) {
                                ExpandGlitchedEnemies.Instance.SpawnRandomGlitchBoss(currentRoom, RandomGlitchEnemyVector3.Value, false, AIActor.AwakenAnimationType.Spawn);
                            }

                            if (RandomGlitchEnemyVector4.HasValue && Random.value <= GlitchedBossOdds) {
                                if (Random.value <= 0.65f) {
                                    ExpandGlitchedEnemies.Instance.SpawnGlitchedObjectAsEnemy(currentRoom, RandomGlitchEnemyVector4.Value, false, AIActor.AwakenAnimationType.Spawn);
                                } else {
                                    ExpandGlitchedEnemies.Instance.SpawnGlitchedPlayerAsEnemy(currentRoom, RandomGlitchEnemyVector4.Value, false, AIActor.AwakenAnimationType.Spawn);
                                }
                            }

                            RandomEnemiesPlaced++;
                            if (RandomEnemiesPlaced + RandomEnemiesSkipped >= MaxEnemies) { break; }
                        }
                    }
                } catch (System.Exception ex) {
                    if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] Exception while setting up or placing enemy for current room" /*+ currentRoom.GetRoomName()*/, false);
                    if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] Skipping current room...", false);
                    if (ExpandSettings.debugMode) { ETGModConsole.Log(ex.Message + ex.StackTrace + ex.Source, false); }
                    continue;
                }
                AIActor[] allAIActors = Object.FindObjectsOfType<AIActor>();
                if (allAIActors != null && allAIActors.Length > 0) {
                    foreach (AIActor enemy in allAIActors) {
                        if (enemy.name.ToLower().StartsWith("corrupted")) {
                            RoomHandler ParentRoom = enemy.transform.position.GetAbsoluteRoom();
                            if (ParentRoom != null) {
                                if (!enemy.gameObject.transform.parent) { enemy.transform.parent = ParentRoom.hierarchyParent; }
                            }
                        }
                    }
                }
            }            
            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("[DEBUG] Max Number of Glitched Enemies assigned to floor: " + MaxEnemies, false);
                ETGModConsole.Log("[DEBUG] Number of Glitched Enemies placed: " + RandomEnemiesPlaced, false);
                ETGModConsole.Log("[DEBUG] Number of Glitched Enemies skipped: " + RandomEnemiesSkipped, false);
                if (RandomEnemiesPlaced <= 0) { ETGModConsole.Log("[DEBUG] Error: No Glitched Enemies have been placed!", false); }
            }
            return;
        }

        private static IntVector2? GetRandomAvailableCell(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, int Clearence = 2, int ExitClearence = 10, bool avoidExits = false, bool avoidPits = true, bool PositionRelativeToRoom = true) {
            if (dungeon == null | currentRoom == null | validCellsCached == null) { return null; }            
            if (validCellsCached.Count == 0) {
                for (int X = 0; X < currentRoom.area.dimensions.x; X++) {
                    for (int Y = 0; Y < currentRoom.area.dimensions.y; Y++) {
                        bool isInvalid = false;
                        IntVector2 TargetPosition = new IntVector2(currentRoom.area.basePosition.x + X, currentRoom.area.basePosition.y + Y);
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
                                        dungeon.data[targetArea1.x, targetArea1.y].isOccupied | cellData.parentRoom != currentRoom)
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

