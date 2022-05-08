using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandFloorDecorator {
        
        private static int RandomObjectsPlaced = 0;
        private static int RandomObjectsSkipped = 0;

        private static readonly bool DebugMode = false;

        public static void PlaceFloorDecoration(Dungeon dungeon, List<RoomHandler> roomListOverride = null, bool ignoreTilesetType = false) {
            
            List<GlobalDungeonData.ValidTilesets> ValidTilesets = new List<GlobalDungeonData.ValidTilesets>() {
                GlobalDungeonData.ValidTilesets.BELLYGEON,
                GlobalDungeonData.ValidTilesets.WESTGEON,
                GlobalDungeonData.ValidTilesets.MINEGEON,
                GlobalDungeonData.ValidTilesets.SPACEGEON,
            };

            if (!ignoreTilesetType && !ValidTilesets.Contains(dungeon.tileIndices.tilesetId)) { return; }
            
            if ((dungeon.data.rooms == null | dungeon.data.rooms.Count <= 0) && roomListOverride == null) { return; }
            
            List<RoomHandler> DungeonRooms = dungeon.data.rooms;

            if (roomListOverride != null) { DungeonRooms = roomListOverride; }

            if (dungeon.gameObject.name.ToLower().StartsWith("base_office")) {
                FlippableCover[] AllTables = Object.FindObjectsOfType<FlippableCover>();
                
                if (AllTables != null && AllTables.Length > 0) {
                    for (int i = 0; i < AllTables.Length; i++) {
                        Vector3 CachedTablePosition = AllTables[i].gameObject.transform.position;
                        List<GameObject> m_surfaceObjects = new List<GameObject>();
                        bool ReplaceThisTable = false;
                        bool IsVerticalTable = false;
                        bool TableHadDecoration = false;
                        bool TableWasDecorated = false;
                        float ChanceToDecorate = 1;
                        GameObject Table = null;
                        RoomHandler parentRoom = AllTables[i].transform.position.GetAbsoluteRoom();
                        if (AllTables[i].gameObject.name.ToLower().Contains("table_vertical") | AllTables[i].gameObject.name.ToLower().Contains("coffin_vertical")) {
                            ReplaceThisTable = true;
                            IsVerticalTable = true;
                        } else if (AllTables[i].gameObject.name.ToLower().Contains("table_horizontal") | AllTables[i].gameObject.name.ToLower().Contains("coffin_horizontal")) {
                            ReplaceThisTable = true;
                        }
                        if (ReplaceThisTable) {
                            SurfaceDecorator m_TableDecorator = AllTables[i].gameObject.GetComponent<SurfaceDecorator>();
                            if (m_TableDecorator) {
                                m_surfaceObjects = ReflectionHelpers.ReflectGetField<List<GameObject>>(typeof(SurfaceDecorator), "m_surfaceObjects", m_TableDecorator);
                                if (m_surfaceObjects != null && m_surfaceObjects.Count > 0) {
                                    TableWasDecorated = true;
                                    for (int I = 0; I < m_surfaceObjects.Count; I++) { Object.Destroy(m_surfaceObjects[I]); }
                                }
                                ChanceToDecorate = m_TableDecorator.chanceToDecorate;
                                TableHadDecoration = true;
                            }
                            parentRoom.DeregisterInteractable(AllTables[i]);
                            Object.Destroy(AllTables[i].gameObject);
                            if (IsVerticalTable) {
                                Table = Object.Instantiate(ExpandObjectDatabase.TableVerticalSteel, CachedTablePosition, Quaternion.identity);
                            } else {
                                Table = Object.Instantiate(ExpandObjectDatabase.TableHorizontalSteel, CachedTablePosition, Quaternion.identity);
                            }
                            if (Table) {
                                FlippableCover NewTable = Table.GetComponent<FlippableCover>();
                                Table.transform.parent = parentRoom.hierarchyParent;
                                NewTable.ConfigureOnPlacement(parentRoom);
                                if (TableHadDecoration) {
                                    SurfaceDecorator newDecorator = Table.GetComponent<SurfaceDecorator>();
                                    if (newDecorator) {
                                        if (TableWasDecorated) {
                                            newDecorator.chanceToDecorate = 1;
                                        } else {
                                            newDecorator.chanceToDecorate = ChanceToDecorate;
                                        }
                                        newDecorator.Decorate(parentRoom);
                                    }
                                }
                                parentRoom.RegisterInteractable(NewTable);
                            }
                        }
                    }
                }
            }

            foreach (RoomHandler currentRoom in DungeonRooms) {
                try {
                    switch (dungeon.tileIndices.tilesetId) {
                        case GlobalDungeonData.ValidTilesets.WESTGEON:
                            PlaceRandomCacti(dungeon, currentRoom);
                            break;
                        case GlobalDungeonData.ValidTilesets.BELLYGEON:
                            PlaceRandomCorpses(dungeon, currentRoom);
                            break;
                        case GlobalDungeonData.ValidTilesets.MINEGEON:
                            PlaceRandomAlarmMushrooms(dungeon, currentRoom);
                            break;
                        case GlobalDungeonData.ValidTilesets.SPACEGEON:
                            if (dungeon.gameObject.name.ToLower().StartsWith("base_office")) {
                                PlaceRandomOfficeSupplies(dungeon, currentRoom);
                            }
                            break;
                    }
                } catch (System.Exception ex) {                    
                    if (ExpandSettings.debugMode && currentRoom != null && !string.IsNullOrEmpty(currentRoom.GetRoomName())) {
                        if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] Exception while setting up objects for room: " + currentRoom.GetRoomName(), DebugMode);
                    } else if (ExpandSettings.debugMode) {
                        if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] Exception while setting up objects for current room", DebugMode);
                    }
                    if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] Skipping current room...", DebugMode);
                    if (ExpandSettings.debugMode) { ETGModConsole.Log(ex.Message + ex.StackTrace + ex.Source, DebugMode); }
                }
            }
            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("[DEBUG] Number of floor decoration objects placed: " + RandomObjectsPlaced, DebugMode);
                ETGModConsole.Log("[DEBUG] Number of floor decoration objects skipped: " + RandomObjectsSkipped, DebugMode);
                if (RandomObjectsPlaced <= 0) { ETGModConsole.Log("[DEBUG] Warning: No decoration objects have been placed!", DebugMode); }
            }
            
            RandomObjectsPlaced = 0;
            RandomObjectsSkipped = 0;
            return;
        }

        private static void PlaceRandomCorpses(Dungeon dungeon, RoomHandler currentRoom) {
            PrototypeDungeonRoom.RoomCategory roomCategory = currentRoom.area.PrototypeRoomCategory;

            int MaxObjectsPerRoom = 12;

            if (currentRoom != null && !string.IsNullOrEmpty(currentRoom.GetRoomName()) && !currentRoom.IsMaintenanceRoom() &&
                !currentRoom.GetRoomName().StartsWith("Boss Foyer") && !currentRoom.PrecludeTilemapDrawing)
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
               currentRoom.RoomVisualSubtype != 0 | !currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) |
               currentRoom.PrecludeTilemapDrawing)
            {
                return;
            }
            

            if (Random.value <= 0.8f | currentRoom.GetRoomName().ToLower().StartsWith("expand_west_entrance")) {
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

                if (!currentRoom.GetRoomName().ToLower().StartsWith("expand_west_entrance")) {
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
                } else {
                    List<GameObject> CactiList = new List<GameObject>() { ExpandPrefabs.Cactus_A, ExpandPrefabs.Cactus_B };
                    CactiList = CactiList.Shuffle();

                    m_CachedPositions = new List<IntVector2>() {
                        new IntVector2(34, 49),
                        new IntVector2(29, 43),
                        new IntVector2(16, 43),
                        new IntVector2(2, 19),
                        new IntVector2(49, 17),
                        new IntVector2(9, 23),
                        new IntVector2(40, 23),
                        new IntVector2(30, 20),
                        new IntVector2(22, 29),
                        new IntVector2(31, 31),
                        new IntVector2(14, 14),
                        new IntVector2(14, 37),
                        new IntVector2(37, 14),
                        new IntVector2(37, 37),
                        new IntVector2(33, 2),
                        new IntVector2(33, 10),
                        new IntVector2(3, 17),
                        new IntVector2(2, 34),
                        new IntVector2(14, 20),
                        new IntVector2(16, 39),
                        new IntVector2(31, 38),
                        new IntVector2(49, 34),
                        new IntVector2(38, 29),
                        new IntVector2(21, 21),
                        new IntVector2(20, 32),
                        new IntVector2(31, 22),
                    };
                    m_CachedPositions = m_CachedPositions.Shuffle();
                    for (int i = 0; i < 14; i++) {
                        IntVector2 selectedPosition = BraveUtility.RandomElement(m_CachedPositions);
                        m_CachedPositions.Remove(selectedPosition);
                        m_CachedPositions = m_CachedPositions.Shuffle();
                        GameObject Cactus = Object.Instantiate(BraveUtility.RandomElement(CactiList), (selectedPosition + currentRoom.area.basePosition).ToVector3(), Quaternion.identity);
                        Cactus.transform.SetParent(currentRoom.hierarchyParent);
                        RandomObjectsPlaced++;
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
            if (DebugMode) { Debug.Log("[ExpandTheGungeon] Checking room for valid Mushroom locations... "); }
            if (currentRoom != null && !string.IsNullOrEmpty(currentRoom.GetRoomName()) && !currentRoom.IsMaintenanceRoom() &&
                !currentRoom.GetRoomName().StartsWith("Boss Foyer"))
            {
                if (Random.value <= 0.6f) {
            
                    List<IntVector2> m_CachedPositions = new List<IntVector2>();
                    int MinMushroomCount = 2;
                    int MaxMushroomCount = 6;
                    if (Random.value <= 0.3f) {
                        MinMushroomCount = 6;
                        MaxMushroomCount = 12;
                    }

                    int X = currentRoom.area.dimensions.x;
                    int Y = currentRoom.area.dimensions.y;
                    
                    if (X * Y < 100) {
                        MinMushroomCount = 1;
                        MaxMushroomCount = 3;
                    }

                    int MushroomCount = Random.Range(MinMushroomCount, MaxMushroomCount);
                    
                    for (int i = 0; i < MushroomCount; i++) {
                        if (DebugMode) { Debug.Log("[ExpandTheGungeon] Test Mushroom Iteration: " + i.ToString()); }
                        if (DebugMode) { if (!string.IsNullOrEmpty(currentRoom.GetRoomName())) { ETGModConsole.Log("[ExpandTheGungeon] On Room: " + currentRoom.GetRoomName()); } }

                        IntVector2? RandomVector = GetRandomAvailableCell(dungeon, currentRoom, m_CachedPositions, 1, 4, avoidExits: true, PositionRelativeToRoom: true);

                        if (RandomVector.HasValue) {
                            if (DebugMode) { ETGModConsole.Log("[ExpandTheGungeon] Valid Location found. Placing Mushroom..."); }
                            try {
                                GameObject alarmMushroomObject = ExpandPrefabs.EXAlarmMushroom.GetComponent<ExpandAlarmMushroomPlacable>().InstantiateObject(currentRoom, RandomVector.Value, true);
                                alarmMushroomObject.transform.parent = currentRoom.hierarchyParent;
                                ExpandAlarmMushroomPlacable m_AlarmMushRoomPlacable = ExpandPrefabs.EXAlarmMushroom.GetComponent<ExpandAlarmMushroomPlacable>();
                                m_AlarmMushRoomPlacable.ConfigureOnPlacement(currentRoom);
                            } catch (System.Exception ex) {
                                if (DebugMode) {
                                    ETGModConsole.Log("[ExpandTheGungeon] Exception While placing/configuring mushroom!", DebugMode);
                                    Debug.LogException(ex);
                                }
                            }
                            RandomObjectsPlaced++;
                            if (DebugMode) { Debug.Log("[ExpandTheGungeon] Mushroom successfully placed!"); }
                            if (m_CachedPositions.Count <= 0) { break; }
                        } else {
                            if (DebugMode) { Debug.Log("[ExpandTheGungeon] No valid cells found. Mushroom skipped!"); }
                            RandomObjectsSkipped++;
                        }
                    }
                }
            }
        }
        
        private static void PlaceRandomOfficeSupplies(Dungeon dungeon, RoomHandler currentRoom) {
            PrototypeDungeonRoom.RoomCategory roomCategory = currentRoom.area.PrototypeRoomCategory;

            Dungeon NakatomiPrefab = ExpandCustomDungeonPrefabs.LoadOfficialDungeonPrefab("Base_Nakatomi");

            List<GameObject> m_ObjectList = new List<GameObject>();

            m_ObjectList.Add(NakatomiPrefab.stampData.objectStamps[6].objectReference);
            m_ObjectList.Add(NakatomiPrefab.stampData.objectStamps[9].objectReference);
            m_ObjectList.Add(NakatomiPrefab.stampData.objectStamps[10].objectReference);
            m_ObjectList.Add(NakatomiPrefab.stampData.objectStamps[11].objectReference);
            m_ObjectList.Add(NakatomiPrefab.stampData.objectStamps[12].objectReference);
            m_ObjectList.Add(ExpandObjectDatabase.KitchenChair_Front);
            m_ObjectList.Add(ExpandObjectDatabase.KitchenChair_Left);
            m_ObjectList.Add(ExpandObjectDatabase.KitchenChair_Right);
            m_ObjectList.Add(ExpandObjectDatabase.KitchenCounter);
            m_ObjectList = m_ObjectList.Shuffle();

            NakatomiPrefab = null;
            
            int MaxObjectsPerRoom = 8;

            if (currentRoom != null && !currentRoom.IsShop && !currentRoom.IsSecretRoom && !string.IsNullOrEmpty(currentRoom.GetRoomName()) && 
                !currentRoom.IsMaintenanceRoom() && !currentRoom.GetRoomName().StartsWith("Boss Foyer") && !currentRoom.PrecludeTilemapDrawing &&
                roomCategory != PrototypeDungeonRoom.RoomCategory.REWARD && Random.value <= 0.6f)
            {
                List<IntVector2> m_CachedPositions = new List<IntVector2>();
                int MaxObjectCount = MaxObjectsPerRoom;
                if (Random.value <= 0.3f){
                    MaxObjectCount = 16;
                } else if (roomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) {
                    MaxObjectCount = 5;
                }

                int ObjectCount = Random.Range(6, MaxObjectsPerRoom);
                
                for (int i = 0; i < ObjectCount; i++) {
                    IntVector2? RandomVector = GetRandomAvailableCell(dungeon, currentRoom, m_CachedPositions);

                    if (RandomVector.HasValue) {
                        GameObject selectedObject = Object.Instantiate(BraveUtility.RandomElement(m_ObjectList), RandomVector.Value.ToVector3(), Quaternion.identity);
                        selectedObject.transform.parent = currentRoom.hierarchyParent;
                        RandomObjectsPlaced++;
                        if (m_CachedPositions.Count <= 0) { break; }
                    } else {
                        RandomObjectsSkipped++;
                    }
                }
            }
        }

        private static IntVector2? GetRandomAvailableCell(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, int Clearence = 1, int ExitClearence = 10, bool avoidExits = false, bool avoidPits = true, bool PositionRelativeToRoom = false, bool isCactusDecoration = false) {
            if (dungeon == null | currentRoom == null | validCellsCached == null) { return null; }
            try { 
                if (validCellsCached.Count == 0) {
                    for (int X = 0; X < currentRoom.area.dimensions.x; X++) {
                        for (int Y = 0; Y < currentRoom.area.dimensions.y; Y++) {
                            bool isInvalid = false;
                            IntVector2 TargetPosition = new IntVector2(currentRoom.area.basePosition.x + X, currentRoom.area.basePosition.y + Y);
                            if (!validCellsCached.Contains(TargetPosition)) {
                                RoomHandler ActualRoom = dungeon.data.GetAbsoluteRoomFromPosition(TargetPosition);
                                for (int x = 0; x < Clearence; x++) {
                                    for (int y = 0; y < Clearence; y++) {
                                        IntVector2 intVector = (TargetPosition + new IntVector2(x, y));
                                        if (dungeon.data.CheckInBoundsAndValid(intVector)) {
                                            CellData cellData = dungeon.data[intVector];
                                            if (cellData.parentRoom == null | cellData.type != CellType.FLOOR | cellData.isOccupied | !cellData.IsPassable ) { isInvalid = true; }
                                            if (ActualRoom != currentRoom | cellData.HasPitNeighbor(dungeon.data)) { isInvalid = true; }
                                            if (cellData.cellVisualData.floorType == CellVisualData.CellFloorType.Water/*| cellData.cellVisualData.floorTileOverridden*/) { isInvalid = true; }
                                            if (cellData.HasWallNeighbor()) { isInvalid = true; }
                                        } else {
                                            isInvalid = true;
                                        }
                                    }
                                }
                                for (int x = 0; x < ExitClearence; x++) {
                                    for (int y = 0; y < ExitClearence; y++) {
                                        IntVector2 intVector = (TargetPosition + new IntVector2(x, y));
                                        if (dungeon.data.CheckInBoundsAndValid(intVector)) {
                                            CellData cellData = dungeon.data[intVector];
                                            if (cellData.isExitCell) { isInvalid = true; }
                                        } else {
                                            isInvalid = true;
                                        }
                                    }
                                }
                                if (!isInvalid) { validCellsCached.Add(TargetPosition); }
                            }
                        }
                    }
                }
            } catch (System.Exception){
                if (DebugMode) { Debug.Log("GetRandomAvailableCell Exception while looking for valid cells in current room."); }
            }
            if (validCellsCached.Count > 0) {
                IntVector2 SelectedCell = BraveUtility.RandomElement(validCellsCached);
                validCellsCached.Remove(SelectedCell);
                if (PositionRelativeToRoom) { SelectedCell -= currentRoom.area.basePosition; }
                return SelectedCell;
            } else {
                return null;
            }
        }
    }
}

