using Dungeonator;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandAutoRegister : BraveBehaviour {

        public IPlayerInteractable m_ParentInteractible;

        private bool m_Registered;

        private RoomHandler m_ParentRoom;

        public void Start() {
            m_ParentRoom = gameObject.transform.position.GetAbsoluteRoom();
            m_ParentInteractible = gameObject.GetComponent<IPlayerInteractable>();
        }

        public void Update() {
            if (m_Registered | m_ParentRoom == null | Dungeon.IsGenerating | (GameManager.HasInstance & GameManager.Instance.IsLoadingLevel)) return;

            m_Registered = true;
            
            if (m_ParentInteractible != null) {
                if (!m_ParentRoom.IsRegistered(m_ParentInteractible) && !RoomHandler.unassignedInteractableObjects.Contains(m_ParentInteractible)) {
                    m_ParentRoom.RegisterInteractable(m_ParentInteractible);
                    Destroy(this);
                    return;
                }
            }
        }
    }
}

