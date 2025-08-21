using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    class ExpandStainPlacer : DungeonPlaceableBehaviour, IPlaceConfigurable  {

        ExpandStainPlacer() {
            m_RandomObjectsPlaced = 0;
            m_ValidCells = new List<IntVector2>();
        }
               
        private int m_RandomObjectsPlaced;

        private Dungeon m_Dungeon;
        private RoomHandler m_CurrentRoom;


        private List<GameObject> m_stainList;
        private List<IntVector2> m_ValidCells;

        private List<IntVector2> FindAllValidLocations(Dungeon dungeon, RoomHandler currentRoom, int Clearence = 1, int ExitClearence = 10, bool avoidExits = false, bool avoidPits = true, bool PositionRelativeToRoom = false) {
            List<IntVector2> m_ValidCellsCached = new List<IntVector2>();
            if (dungeon == null | currentRoom == null) { return m_ValidCellsCached; }
            
            for (int X = 0; X < currentRoom.area.dimensions.x; X++) {
                for (int Y = 0; Y < currentRoom.area.dimensions.y; Y++) {
                    try {
                        bool isInvalid = false;
                        IntVector2 TargetPosition = new IntVector2(currentRoom.area.basePosition.x + X, currentRoom.area.basePosition.y + Y);
                        if (!m_ValidCellsCached.Contains(TargetPosition)) {
                            for (int x = 0; x < Clearence; x++) {
                                for (int y = 0; y < Clearence; y++) {
                                    IntVector2 intVector = (TargetPosition + new IntVector2(x, y));
                                    if (dungeon.data.CheckInBoundsAndValid(intVector)) {
                                        CellData cellData = dungeon.data[intVector];
                                        if (cellData.type != CellType.FLOOR) { isInvalid = true; }
                                        if (cellData.HasPitNeighbor(dungeon.data) && Clearence > 1) { isInvalid = true; }
                                    } else {
                                        isInvalid = true;
                                    }
                                }
                            }
                            if (!isInvalid && avoidExits) {
                                for (int x = 0; x < ExitClearence; x++) {
                                    for (int y = 0; y < ExitClearence; y++) {
                                        IntVector2 intVector = (TargetPosition + new IntVector2(x, y));
                                        if (dungeon.data.CheckInBoundsAndValid(intVector)) {
                                            CellData cellData = dungeon.data[intVector];
                                            if (cellData.isExitCell) { isInvalid = true; }
                                        }
                                    }
                                }
                            }
                            if (!isInvalid) {
                                if (PositionRelativeToRoom) {
                                    m_ValidCellsCached.Add(new IntVector2(X, Y));
                                } else {
                                    m_ValidCellsCached.Add(TargetPosition);
                                }
                            }
                        }
                    } catch (System.Exception EX) {
                        if (ExpandSettings.debugMode) {
                            Debug.Log("[ExpandFloorDecorator.FindAllValidLocations] Exception while looking for valid cells in current room.");
                            Debug.LogException(EX);
                        }
                    }
                }
            }
            if (m_ValidCellsCached.Count > 1 && Clearence > 0) {
                for (int I = 0; I < m_ValidCellsCached.Count; I++) {
                    if (I + 1 > m_ValidCellsCached.Count) break;
                    Vector2 m_Position1 = m_ValidCellsCached[I].ToVector2();
                    for (int I2 = (I + 1); I2 < m_ValidCellsCached.Count; I2++) {
                        Vector2 m_Position2 = m_ValidCellsCached[I2].ToVector2();
                        if (Vector2.Distance(m_Position1, m_Position2) < Clearence) {
                            m_ValidCellsCached.Remove(m_ValidCellsCached[I2]);
                        }
                    }
                }
            } else if (m_ValidCellsCached.Count > 1) {
                m_ValidCellsCached = m_ValidCellsCached.Shuffle();
            }
            return m_ValidCellsCached;
        }

        public void Start() {
            if (!m_Dungeon) return;
            if (string.IsNullOrEmpty(m_Dungeon.gameObject.name.ToLower())) return;
            if (!m_Dungeon.gameObject.name.ToLower().StartsWith("base_backrooms")) return;

            m_stainList = new List<GameObject>() {
                ExpandPrefabs.EXBackRoomsCarpetStain_Small,
                ExpandPrefabs.EXBackRoomsCarpetStain_Medium,
                ExpandPrefabs.EXBackRoomsCarpetStain_Large
            };

            try {
                if (m_CurrentRoom == null) m_CurrentRoom = GetAbsoluteParentRoom();
                PlaceRandomCarpetStains();
            } catch (System.Exception ex) {
                if (ExpandSettings.debugMode) {
                    string m_RoomName = "NULL";
                    if (m_CurrentRoom != null && !string.IsNullOrEmpty(m_CurrentRoom.GetRoomName()))m_RoomName = m_CurrentRoom.GetRoomName();
                    ETGModConsole.Log("[DEBUG] Exception in ExpandStainPlayer on room: " + m_RoomName, false);
                    Debug.LogException(ex);
                }
            }
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            m_CurrentRoom = room;
            m_Dungeon = GameManager.Instance.Dungeon;

            if (!m_Dungeon | !m_Dungeon.gameObject.name.ToLower().StartsWith("base_backrooms")) return;
        }

        private void PlaceRandomCarpetStains() {
            PrototypeDungeonRoom.RoomCategory roomCategory = m_CurrentRoom.area.PrototypeRoomCategory;
                        
            int MinStainCount = 1;
            int MaxStainCount = 3;
            
            m_ValidCells = FindAllValidLocations(m_Dungeon, m_CurrentRoom, 0);

            if (m_ValidCells.Count < 1) return;

            if (m_ValidCells.Count > 15) {
                MinStainCount = 2;
                MaxStainCount = 4;
            } else if (m_ValidCells.Count > 20) {
                MinStainCount = 3;
                MaxStainCount = 6;
            } else if (m_ValidCells.Count > 40) {
                MinStainCount = 4;
                MaxStainCount = 8;
            } else if (m_ValidCells.Count > 60) {
                MinStainCount = 5;
                MaxStainCount = 10;
            } else if (m_ValidCells.Count > 80) {
                MinStainCount = 7;
                MaxStainCount = 15;
            } else if (m_ValidCells.Count > 100) {
                MinStainCount = 9;
                MaxStainCount = 20;
            } else if (m_ValidCells.Count > 140) {
                MinStainCount = 10;
                MaxStainCount = 25;
            } else if (Random.value < 0.1f) {
                MinStainCount = 2;
                MaxStainCount = 4;
            }

            int StainCount = Random.Range(MinStainCount, MaxStainCount);

            for (int i = 0; i < StainCount; i++) {
                m_stainList = m_stainList.Shuffle();
                int StainClearance = 2;
                if (m_ValidCells.Count > 0) {
                    IntVector2 RandomVector = BraveUtility.RandomElement(m_ValidCells);
                    
                    GameObject m_PlacedStain = Instantiate(BraveUtility.RandomElement(m_stainList), RandomVector.ToVector2(), Quaternion.identity);
                    m_ValidCells.Remove(RandomVector);
                    if (m_PlacedStain) {
                        if (m_CurrentRoom != null && m_CurrentRoom.hierarchyParent) {
                            m_PlacedStain.transform.SetParent(m_CurrentRoom.hierarchyParent);
                        }

                        if (m_PlacedStain.name.ToLower().StartsWith("exbackroomscarpetstain_medium")) StainClearance = 2;
                        if (m_PlacedStain.name.ToLower().StartsWith("exbackroomscarpetstain_large")) StainClearance = 3;

                        if (StainClearance > 0) {
                            for (int posX = 0; posX < StainClearance; posX++) {
                                if (m_ValidCells.Count < 1) break;
                                for (int posY = 0; posY < StainClearance; posY++) {
                                    if (m_ValidCells.Count < 1)break;
                                    int X = RandomVector.x;
                                    int Y = RandomVector.Y;
                                    if (((X - posX) > 0) && ((Y - posY) > 0)) {
                                        IntVector2 vector1 = new IntVector2(X - posX, Y);
                                        IntVector2 vector2 = new IntVector2(X, Y - posY);
                                        IntVector2 vector3 = new IntVector2(X - posX, Y - posY);
                                        IntVector2 vector4 = new IntVector2(X + posX, Y);
                                        IntVector2 vector5 = new IntVector2(X, Y + posY);
                                        IntVector2 vector6 = new IntVector2(X + posX, Y + posY);
                                        if (m_ValidCells.Contains(vector1)) m_ValidCells.Remove(vector1);
                                        if (m_ValidCells.Count < 1) break;
                                        if (m_ValidCells.Contains(vector2)) m_ValidCells.Remove(vector2);
                                        if (m_ValidCells.Count < 1) break;
                                        if (m_ValidCells.Contains(vector3)) m_ValidCells.Remove(vector3);
                                        if (m_ValidCells.Count < 1) break;
                                        if (m_ValidCells.Contains(vector4)) m_ValidCells.Remove(vector4);
                                        if (m_ValidCells.Count < 1) break;
                                        if (m_ValidCells.Contains(vector5)) m_ValidCells.Remove(vector5);
                                        if (m_ValidCells.Count < 1) break;
                                        if (m_ValidCells.Contains(vector6)) m_ValidCells.Remove(vector6);
                                    }
                                }
                            }
                        } else {
                            m_ValidCells.Remove(RandomVector);
                        }
                    }
                    m_RandomObjectsPlaced++;
                }
            }
            if (ExpandSettings.debugMode) {
                string m_RoomName = "NULL";
                if (!string.IsNullOrEmpty(m_CurrentRoom.GetRoomName()))m_RoomName = m_CurrentRoom.GetRoomName();
                if (m_RandomObjectsPlaced > 0)ETGModConsole.Log("[DEBUG] Carpert Stains succesfully placed in room: " + m_RoomName, false);
            }
        }
    }
}
