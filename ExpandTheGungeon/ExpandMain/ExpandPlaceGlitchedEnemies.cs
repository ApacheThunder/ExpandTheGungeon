using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandObjects;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandPlaceGlitchedEnemies : MonoBehaviour {

        public ExpandPlaceGlitchedEnemies() { m_GlitchEnemyDatabase = new ExpandGlitchedEnemies(); }

        public ExpandGlitchedEnemies m_GlitchEnemyDatabase;        

        public void PlaceRandomEnemies(Dungeon dungeon, int currentFloor) {
            if (!dungeon.IsGlitchDungeon && !ExpandTheGungeon.isGlitchFloor && dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.PHOBOSGEON) { return; }

            List<string> BannedRooms = new List<string>();

            foreach (WeightedRoom wRoom in ExpandPrefabs.MegaMiniBossRoomTable.includedRooms.elements) {
                if (wRoom.room != null) { BannedRooms.Add(wRoom.room.name); }
            }
            foreach (WeightedRoom wRoom in ExpandPrefabs.MegaBossRoomTable.includedRooms.elements) {
                if (wRoom.room != null) { BannedRooms.Add(wRoom.room.name); }
            }

            PlayerController player = GameManager.Instance.PrimaryPlayer;
            int RandomEnemiesPlaced = 0;
            int RandomEnemiesSkipped = 0;
            int MaxEnemies = 20;
            float GlitchedBossOdds = 0.15f;
            float BonusGlitchEnemyOdds = 0.05f;
            
            if (dungeon.IsGlitchDungeon) { MaxEnemies = 65; GlitchedBossOdds = 0.3f; BonusGlitchEnemyOdds = 0.28f; }
            if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.PHOBOSGEON) { MaxEnemies = 60; GlitchedBossOdds = 0.3f; BonusGlitchEnemyOdds = 0.28f; }
            
            if (dungeon.data.rooms == null | dungeon.data.rooms.Count <= 0) { return; }
            foreach (RoomHandler currentRoom in dungeon.data.rooms) {             
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
                            IntVector2? RandomGlitchEnemyVector = GetRandomAvailableCellForEnemy(dungeon, currentRoom, m_CachedPositions);
                            IntVector2? RandomGlitchEnemyVector2 = null;
                            IntVector2? RandomGlitchEnemyVector3 = null;
                            IntVector2? RandomGlitchEnemyVector4 = null;

                            if (m_CachedPositions.Count > 0) { RandomGlitchEnemyVector2 = GetRandomAvailableCellForEnemy(dungeon, currentRoom, m_CachedPositions); }
                            if (m_CachedPositions.Count > 0) { RandomGlitchEnemyVector3 = GetRandomAvailableCellForEnemy(dungeon, currentRoom, m_CachedPositions); }
                            if (m_CachedPositions.Count > 0) { RandomGlitchEnemyVector4 = GetRandomAvailableCellForEnemy(dungeon, currentRoom, m_CachedPositions); }
                            

                            if (RandomGlitchEnemyVector.HasValue) {
                                m_GlitchEnemyDatabase.SpawnRandomGlitchEnemy(currentRoom, RandomGlitchEnemyVector.Value, false, AIActor.AwakenAnimationType.Spawn);
                            } else { RandomEnemiesSkipped++; }

                            if (RandomGlitchEnemyVector2.HasValue && Random.value <= BonusGlitchEnemyOdds) {
                                m_GlitchEnemyDatabase.SpawnRandomGlitchEnemy(currentRoom, RandomGlitchEnemyVector2.Value, false, AIActor.AwakenAnimationType.Spawn);
                            } else { RandomEnemiesSkipped++; }

                            if (RandomGlitchEnemyVector3.HasValue && Random.value <= GlitchedBossOdds) {
                                m_GlitchEnemyDatabase.SpawnRandomGlitchBoss(currentRoom, RandomGlitchEnemyVector3.Value, false, AIActor.AwakenAnimationType.Spawn);
                            }

                            if (RandomGlitchEnemyVector4.HasValue && Random.value <= GlitchedBossOdds) {
                                if (Random.value <= 0.65f) {
                                    m_GlitchEnemyDatabase.SpawnGlitchedObjectAsEnemy(currentRoom, RandomGlitchEnemyVector4.Value, false, AIActor.AwakenAnimationType.Spawn);
                                } else {
                                    m_GlitchEnemyDatabase.SpawnGlitchedPlayerAsEnemy(currentRoom, RandomGlitchEnemyVector4.Value, false, AIActor.AwakenAnimationType.Spawn);
                                }
                            }

                            RandomEnemiesPlaced++;
                            if (RandomEnemiesPlaced + RandomEnemiesSkipped >= MaxEnemies) { break; }
                        }
                    }
                } catch (System.Exception ex) {
                    if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] Exception while setting up or placing enemy for current room" /*+ currentRoom.GetRoomName()*/, false);
                    if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] Skipping current room...", false);
                    if (ExpandStats.debugMode) { ETGModConsole.Log(ex.Message + ex.StackTrace + ex.Source, false); }
                    continue;
                }
            }            
            if (ExpandStats.debugMode) {
                ETGModConsole.Log("[DEBUG] Max Number of Glitched Enemies assigned to floor: " + MaxEnemies, false);
                ETGModConsole.Log("[DEBUG] Number of Glitched Enemies placed: " + RandomEnemiesPlaced, false);
                ETGModConsole.Log("[DEBUG] Number of Glitched Enemies skipped: " + RandomEnemiesSkipped, false);
                if (RandomEnemiesPlaced <= 0) { ETGModConsole.Log("[DEBUG] Error: No Glitched Enemies have been placed!", false); }
            }
            Destroy(m_GlitchEnemyDatabase);
            return;
        }

        private IntVector2? GetRandomAvailableCellForEnemy(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, int gridSnap = 1) {
            if (dungeon == null | currentRoom == null | validCellsCached == null) { return null; }            
            if (validCellsCached.Count == 0) {
                for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                    for (int height = -1; height <= currentRoom.area.dimensions.y; height++) {
                        int X = currentRoom.area.basePosition.x + Width;
                        int Y = currentRoom.area.basePosition.y + height;
                        if (X % gridSnap == 0 && Y % gridSnap == 0) {
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

