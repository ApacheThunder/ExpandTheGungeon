using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using System.Linq;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandJunkEnemySpawneer {
        
        public static void PlaceRandomJunkEnemies(Dungeon dungeon, RoomHandler roomHandler) {
            if (dungeon.IsGlitchDungeon) { return; }
            if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.RATGEON) { return; }

            if (Random.value <= 0.85f) { return; }
            
            int RandomEnemiesPlaced = 0;
            int RandomEnemiesSkipped = 0;
            int MaxEnemies = 1;
            int iterations = 0;

            if (Random.value <= 0.1f) { MaxEnemies = 2; }
            
            if (dungeon.data.rooms == null | dungeon.data.rooms.Count <= 0) { return; }

            List<int> roomList = Enumerable.Range(0, dungeon.data.rooms.Count).ToList();
            roomList = roomList.Shuffle();

            if (roomHandler != null) { roomList = new List<int>(new int[] { dungeon.data.rooms.IndexOf(roomHandler) }); }
            
            while (iterations < roomList.Count && RandomEnemiesPlaced < MaxEnemies) {
                RoomHandler currentRoom = dungeon.data.rooms[roomList[iterations]];
                if (currentRoom == null | currentRoom.area == null) { continue; }
                PrototypeDungeonRoom.RoomCategory roomCategory = currentRoom.area.PrototypeRoomCategory;
                try {
                    if (!string.IsNullOrEmpty(currentRoom.GetRoomName()) &&
                        currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) && !currentRoom.IsMaintenanceRoom() &&
                       !currentRoom.IsSecretRoom && !currentRoom.IsWinchesterArcadeRoom && !currentRoom.IsGunslingKingChallengeRoom &&
                       !currentRoom.GetRoomName().StartsWith("Boss Foyer") && !currentRoom.GetRoomName().StartsWith(ExpandRoomPrefabs.Expand_Keep_TreeRoom.name) &&
                       !currentRoom.GetRoomName().StartsWith(ExpandRoomPrefabs.Expand_Keep_TreeRoom2.name))
                    {
                        if (roomCategory != PrototypeDungeonRoom.RoomCategory.BOSS && roomCategory != PrototypeDungeonRoom.RoomCategory.ENTRANCE &&
                            roomCategory != PrototypeDungeonRoom.RoomCategory.REWARD && roomCategory != PrototypeDungeonRoom.RoomCategory.EXIT)
                        {
                            if (RandomEnemiesPlaced >= MaxEnemies) { break; }

                            List<IntVector2> m_CachedPositions = new List<IntVector2>();
                            IntVector2? RandomGlitchEnemyVector = GetRandomAvailableCellForEnemy(dungeon, currentRoom, m_CachedPositions);

                            if (RandomGlitchEnemyVector.HasValue) {
                                if (Random.value <= 0.5f) {
                                    ExpandGlitchedEnemies.Instance.SpawnGlitchedRaccoon(currentRoom, RandomGlitchEnemyVector.Value, false, AIActor.AwakenAnimationType.Spawn, true);
                                } else {
                                    ExpandGlitchedEnemies.Instance.SpawnGlitchedTurkey(currentRoom, RandomGlitchEnemyVector.Value, false, AIActor.AwakenAnimationType.Spawn, true);
                                }
                            } else { RandomEnemiesSkipped++; }

                            RandomEnemiesPlaced++;
                        }
                    }
                    iterations++;
                } catch (System.Exception ex) {
                    if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] Exception while setting up or placing enemy for current room" /*+ currentRoom.GetRoomName()*/, false);
                    if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] Skipping current room...", false);
                    if (ExpandSettings.debugMode) { ETGModConsole.Log(ex.Message + ex.StackTrace + ex.Source, false); }
                    if (RandomEnemiesPlaced >= MaxEnemies) { break; }
                    iterations++;
                }
            }
            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("[DEBUG] Max Number of Junk Enemies assigned to floor: " + MaxEnemies, false);
                ETGModConsole.Log("[DEBUG] Number of Junk Enemies placed: " + RandomEnemiesPlaced, false);
                ETGModConsole.Log("[DEBUG] Number of Junk Enemies skipped: " + RandomEnemiesSkipped, false);
                if (RandomEnemiesPlaced <= 0) { ETGModConsole.Log("[DEBUG] Error: No Junk Enemies have been placed!", false); }
            }
            if (RandomEnemiesPlaced > 0) {
                AIActor[] actors = Object.FindObjectsOfType<AIActor>();
                if (actors != null & actors.Length > 0) {
                    foreach (AIActor actor in actors) {
                        if (!string.IsNullOrEmpty(actor.ActorName)) {
                            if (actor.ActorName == "Junk Raccoon") {
                                actor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(127));
                                if (Random.value <= 0.2f) {
                                    actor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(127));
                                } else {
                                    if (Random.value <= 0.1f) {
                                        actor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(74));
                                    } else {
                                        actor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(85));
                                    }
                                }
                                if (Random.value <= 0.01f) {
                                    if (GameManager.Instance.PrimaryPlayer.HasPickupID(641)) {
                                        actor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(74));
                                    } else {
                                        actor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(641));
                                    }
                                } else if (Random.value <= 0.01f) {
                                    if (GameManager.Instance.PrimaryPlayer.HasPickupID(580)) {
                                        actor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(74));
                                    } else {
                                        actor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(580));
                                    }
                                }
                            } else if (actor.ActorName == "Junk Turkey") {
                                if (BraveUtility.RandomBool()) {
                                    actor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(600));
                                } else {
                                    actor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(78));
                                }
                            }
                        }

                    }
                }
            }
            return;
        }

        private static IntVector2? GetRandomAvailableCellForEnemy(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, int gridSnap = 1) {
            if (dungeon == null | currentRoom == null | validCellsCached == null) { return null; }
            if (validCellsCached.Count == 0) {
                for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                    for (int height = -1; height <= currentRoom.area.dimensions.y; height++) {
                        int X = currentRoom.area.basePosition.x + Width;
                        int Y = currentRoom.area.basePosition.y + height;
                        if (X % gridSnap == 0 && Y % gridSnap == 0 && dungeon.data[new IntVector2(X, Y)].parentRoom == currentRoom) {
                            if (!dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) &&
                                !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) &&
                                !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) &&
                                !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) &&
                                !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) &&
                                !dungeon.data[X - 2, Y + 2].isOccupied && !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied && !dungeon.data[X + 2, Y + 2].isOccupied &&
                                !dungeon.data[X - 2, Y + 1].isOccupied && !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied && !dungeon.data[X + 2, Y + 1].isOccupied &&
                                !dungeon.data[X - 2, Y].isOccupied && !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied && !dungeon.data[X + 2, Y].isOccupied &&
                                !dungeon.data[X - 2, Y - 1].isOccupied && !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied && !dungeon.data[X + 2, Y - 1].isOccupied &&
                                !dungeon.data[X - 2, Y - 2].isOccupied && !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied && !dungeon.data[X + 2, Y - 2].isOccupied &&
                                !dungeon.data.isPit(X - 2, Y + 2) && !dungeon.data.isPit(X - 1, Y + 2) && !dungeon.data.isPit(X, Y + 2) && !dungeon.data.isPit(X + 1, Y + 2) && !dungeon.data.isPit(X + 2, Y + 2) &&
                                !dungeon.data.isPit(X - 2, Y + 1) && !dungeon.data.isPit(X - 1, Y + 1) && !dungeon.data.isPit(X, Y + 1) && !dungeon.data.isPit(X + 1, Y + 1) && !dungeon.data.isPit(X + 2, Y + 1) &&
                                !dungeon.data.isPit(X - 2, Y) && !dungeon.data.isPit(X - 1, Y) && !dungeon.data.isPit(X, Y) && !dungeon.data.isPit(X + 1, Y) && !dungeon.data.isPit(X + 2, Y) &&
                                !dungeon.data.isPit(X - 2, Y - 1) && !dungeon.data.isPit(X - 1, Y - 1) && !dungeon.data.isPit(X, Y - 1) && !dungeon.data.isPit(X + 1, Y - 1) && !dungeon.data.isPit(X + 2, Y - 1) &&
                                !dungeon.data.isPit(X - 2, Y - 2) && !dungeon.data.isPit(X - 1, Y - 2) && !dungeon.data.isPit(X, Y - 2) && !dungeon.data.isPit(X + 1, Y - 2) && !dungeon.data.isPit(X + 2, Y - 2))
                            {
                                validCellsCached.Add(new IntVector2(X, Y));
                            }
                        }
                    }
                }
            }
            if (validCellsCached.Count > 0) {
                IntVector2 SelectedCell = BraveUtility.RandomElement(validCellsCached);
                IntVector2 RegisteredCell = (SelectedCell);
                dungeon.data[RegisteredCell].isOccupied = true;
                validCellsCached.Remove(SelectedCell);
                return (SelectedCell - currentRoom.area.basePosition);
            } else {
                return null;
            }
        }
    }
}

