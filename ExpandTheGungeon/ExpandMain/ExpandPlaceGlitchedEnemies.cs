using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandObjects;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandPlaceGlitchedEnemies : MonoBehaviour {

        public ExpandPlaceGlitchedEnemies() { m_GlitchEnemyDatabase = new ExpandGlitchedEnemies(); }

        public ExpandGlitchedEnemies m_GlitchEnemyDatabase;        

        public void PlaceRandomEnemies(Dungeon dungeon, int currentFloor) {
            if (!dungeon.IsGlitchDungeon && /*!ExpandTheGungeon.isGlitchFloor &&*/ dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.PHOBOSGEON) { return; }

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
                            if (m_CachedPositions.Count > 0) { RandomGlitchEnemyVector3 = GetRandomAvailableCellForEnemy(dungeon, currentRoom, m_CachedPositions, ExitClearence: 13); }
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
                AIActor[] allAIActors = FindObjectsOfType<AIActor>();
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
            if (ExpandStats.debugMode) {
                ETGModConsole.Log("[DEBUG] Max Number of Glitched Enemies assigned to floor: " + MaxEnemies, false);
                ETGModConsole.Log("[DEBUG] Number of Glitched Enemies placed: " + RandomEnemiesPlaced, false);
                ETGModConsole.Log("[DEBUG] Number of Glitched Enemies skipped: " + RandomEnemiesSkipped, false);
                if (RandomEnemiesPlaced <= 0) { ETGModConsole.Log("[DEBUG] Error: No Glitched Enemies have been placed!", false); }
            }
            Destroy(m_GlitchEnemyDatabase);
            return;
        }

        private IntVector2? GetRandomAvailableCellForEnemy(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, int Clearence = 2, int ExitClearence = 10) {
            if (dungeon == null | currentRoom == null | validCellsCached == null) { return null; }            
            if (validCellsCached.Count == 0) {
                for (int X = 0; X < currentRoom.area.dimensions.x; X++) {
                    for (int Y = 0; Y < currentRoom.area.dimensions.y; Y++) {
                        bool isInvalid = false;
                        IntVector2 TargetPosition = new IntVector2(currentRoom.area.basePosition.x + X, currentRoom.area.basePosition.y + Y);

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
                        for(int x = (0 - Clearence); x <= Clearence; x++) {
                            for(int y = (0 - Clearence); y <= Clearence; y++) {
                                IntVector2 targetArea1 = (TargetPosition + new IntVector2(x, y));
                                if (dungeon.data.CheckInBoundsAndValid(targetArea1)) {
                                    CellData cellData = dungeon.data[targetArea1];
                                    if (cellData.isWallMimicHideout | cellData.IsAnyFaceWall() | cellData.IsFireplaceCell |
                                        cellData.isDoorFrameCell | cellData.IsTopWall() | 
                                        cellData.isOccludedByTopWall | cellData.IsUpperFacewall() | cellData.isWallMimicHideout |
                                        dungeon.data.isWall(targetArea1.x, targetArea1.y) | dungeon.data.isPit(targetArea1.x, targetArea1.y) |
                                        dungeon.data[targetArea1.x, targetArea1.y].isOccupied)
                                    {
                                        isInvalid = true;
                                        break;
                                    }
                                }
                            }
                            if (isInvalid) { break; }
                        }
                        if (!isInvalid && dungeon.data.CheckInBoundsAndValid(TargetPosition)) { validCellsCached.Add(TargetPosition); }
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

