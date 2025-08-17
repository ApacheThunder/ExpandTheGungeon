using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    class ExpandStainPlacer : DungeonPlaceableBehaviour, IPlaceConfigurable  {

        ExpandStainPlacer() {
            m_RandomObjectsPlaced = 0;
        }

        private enum CarpetSize { Small, Medium, Large }

        private int m_RandomObjectsPlaced;

        private Dungeon m_Dungeon;
        private RoomHandler m_CurrentRoom;

        public void Start() {
            if (!m_Dungeon | !m_Dungeon.gameObject.name.ToLower().StartsWith("base_backrooms"))return;
            try {
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
            
            int MaxStainCount = 8;
            int MinStainCount = 2;
            int RoomSize = m_CurrentRoom.area.dimensions.x + m_CurrentRoom.area.dimensions.x;
            
            if (RoomSize > 15) {
                MinStainCount = 4;
                MaxStainCount = 15;
            } else if (Random.value <= 0.2f) {
                MinStainCount = 3;
                MaxStainCount = 12;
            }

            int StainCount = Random.Range(MinStainCount, MaxStainCount);
            
            List<IntVector2> m_PlacedStains = new List<IntVector2>();
            
            for (int i = 0; i < StainCount; i++) {
                CarpetSize carpetSize = CarpetSize.Small;
            
                int StainClearance = 1;
                int randomSize = BraveRandom.GenerationRandomRange(0, 3).RoundToNearest(1);
                GameObject m_SelectedStainObject = ExpandPrefabs.EXBackRoomsCarpetStain_Small;
            
                switch (randomSize) {
                    case 0: carpetSize = CarpetSize.Small; break;
                    case 1: carpetSize = CarpetSize.Medium; break;
                    case 2: carpetSize = CarpetSize.Large; break;
                    default: carpetSize = CarpetSize.Small; break;
                }
                
            
                switch (carpetSize) {
                    case CarpetSize.Small: break;
                    case CarpetSize.Medium:
                        m_SelectedStainObject = ExpandPrefabs.EXBackRoomsCarpetStain_Medium;
                        StainClearance = 2;
                        break;
                    case CarpetSize.Large:
                        m_SelectedStainObject = ExpandPrefabs.EXBackRoomsCarpetStain_Large;
                        StainClearance = 3;
                        break;
                }
            
                List<IntVector2> m_CachedPositions = ExpandUtility.FindAllValidLocations(m_Dungeon, m_CurrentRoom, StainClearance);
            
                for (int I = 0; I < m_CachedPositions.Count; I++) {
                    if (m_PlacedStains.Count > 0 && m_PlacedStains.Contains(m_CachedPositions[I])) {
                        m_CachedPositions.Remove(m_CachedPositions[I]);
                    } else if ((m_PlacedStains.Count > 0) && (m_CachedPositions.Count > 0)) {
                        for (int P = 0; P < m_PlacedStains.Count; P++) {
                            Vector2 m_PlacedStainVector = new Vector2(m_PlacedStains[P].X, m_PlacedStains[P].Y);
                            Vector2 m_CachedStainVector = new Vector2(m_CachedPositions[I].X, m_CachedPositions[I].Y);
                            float m_Distance = Vector2.Distance(m_PlacedStainVector, m_CachedStainVector);
                            if (m_Distance < StainClearance) m_CachedPositions.Remove(m_CachedPositions[I]);
                        }
                    }
                }
            
                if (m_CachedPositions.Count > 0) {
                    IntVector2 RandomVector = BraveUtility.RandomElement(m_CachedPositions);
                    m_PlacedStains.Add(RandomVector);
                    m_CachedPositions.Remove(RandomVector);
                    if (m_CachedPositions.Count > 1) { m_CachedPositions = m_CachedPositions.Shuffle(); }
                    GameObject m_PlacedStain = Object.Instantiate(m_SelectedStainObject, RandomVector.ToVector2(), Quaternion.identity);
            
                    if (m_PlacedStain) {
                        m_PlacedStain.transform.parent.SetParent(m_CurrentRoom.hierarchyParent);
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
