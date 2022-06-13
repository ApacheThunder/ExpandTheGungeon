using Dungeonator;
using System;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCasinoRoomController : BraveBehaviour {

        public ExpandCasinoRoomController() { }
        
        public ExpandCasinoGameController CasinoGame_Punchout;


        [NonSerialized]
        private RoomHandler m_ParentRoom;

        private void Start() {
            m_ParentRoom = gameObject.transform.position.GetAbsoluteRoom();

            if (CasinoGame_Punchout) {
                CasinoGame_Punchout.ConfigureOnPlacement(m_ParentRoom);
                m_ParentRoom.RegisterInteractable(CasinoGame_Punchout);
            }
        }

        private void Update() { }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

