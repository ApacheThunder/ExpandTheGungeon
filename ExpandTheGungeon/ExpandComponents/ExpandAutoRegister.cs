using System;
using Dungeonator;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandAutoRegister : BraveBehaviour {

        public ExpandAutoRegister() {
            DestroyIfNearPit = true;
            CorrectForWalls = true;
            OnlyCheckWallsPits = false;
            NorthWallOffset = 0.2f;
            WestWallOffset = 0;
            EastWallOffset = 0.5f;
        }

        [SerializeField]
        public bool CorrectForWalls;
        [SerializeField]
        public bool DestroyIfNearPit;
        [SerializeField]
        public bool OnlyCheckWallsPits;
        [SerializeField]
        public float NorthWallOffset;
        [SerializeField]
        public float WestWallOffset;
        [SerializeField]
        public float EastWallOffset;

        [NonSerialized]
        public IPlayerInteractable ParentInteractible;

        private bool m_Registered;

        private RoomHandler m_ParentRoom;

        public void Start() {
            if (!OnlyCheckWallsPits) {
                m_ParentRoom = gameObject.transform.position.GetAbsoluteRoom();
                ParentInteractible = gameObject.GetComponent<IPlayerInteractable>();
            }
        }

        public void Update() {
            if (m_Registered | (m_ParentRoom == null && !OnlyCheckWallsPits) | Dungeon.IsGenerating | (GameManager.HasInstance & GameManager.Instance.IsLoadingLevel)) return;

            m_Registered = true;
            IntVector2 m_CurrentPosition = transform.position.XY().ToIntVector2(VectorConversions.Floor);
            Vector3 m_NewPosition = transform.position;

            if (DestroyIfNearPit) {
                bool IsOverOrNearPit = (GameManager.Instance.Dungeon.data.isPit(m_CurrentPosition.x, m_CurrentPosition.y) | GameManager.Instance.Dungeon.data.isPit(m_CurrentPosition.x, m_CurrentPosition.y + 1) | GameManager.Instance.Dungeon.data.isPit(m_CurrentPosition.x + 1, m_CurrentPosition.y) | GameManager.Instance.Dungeon.data.isPit(m_CurrentPosition.x + 1, m_CurrentPosition.y + 1));
                if (IsOverOrNearPit) {
                    Destroy(gameObject);
                    return;
                }
            }

            if (CorrectForWalls && GameManager.HasInstance && GameManager.Instance.Dungeon) {
                bool IsBelowWall = (GameManager.Instance.Dungeon.data.isFaceWallLower(m_CurrentPosition.x, m_CurrentPosition.y) | GameManager.Instance.Dungeon.data.isFaceWallLower(m_CurrentPosition.x, m_CurrentPosition.y + 1));
                bool IsBesideLeftWall = (GameManager.Instance.Dungeon.data.isLeftSideWall(m_CurrentPosition.x, m_CurrentPosition.y) | GameManager.Instance.Dungeon.data.isLeftSideWall(m_CurrentPosition.x - 1, m_CurrentPosition.y));
                bool IsBesideRightWall = (GameManager.Instance.Dungeon.data.isRightSideWall(m_CurrentPosition.x, m_CurrentPosition.y) | GameManager.Instance.Dungeon.data.isRightSideWall(m_CurrentPosition.x + 1, m_CurrentPosition.y));
                if (IsBelowWall && NorthWallOffset != 0) m_NewPosition -= new Vector3(0, NorthWallOffset, 0);
                if (IsBesideLeftWall && WestWallOffset != 0) m_NewPosition += new Vector3(WestWallOffset, 0, 0);
                if (IsBesideRightWall && EastWallOffset != 0) m_NewPosition -= new Vector3(EastWallOffset, 0, 0);
                if (m_NewPosition != transform.position) {
                    transform.position = m_NewPosition;
                    if (specRigidbody) specRigidbody.Reinitialize();
                }
            }
            
            if (!OnlyCheckWallsPits && ParentInteractible != null && !m_ParentRoom.IsRegistered(ParentInteractible) && !RoomHandler.unassignedInteractableObjects.Contains(ParentInteractible)) {
                m_ParentRoom.RegisterInteractable(ParentInteractible);
            }
            Destroy(this);
            return;
        }
    }
}

