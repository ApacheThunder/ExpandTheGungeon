using Dungeonator;
using System;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCasinoRoomController : BraveBehaviour {

        public ExpandCasinoRoomController() { }
        
        public ExpandCasinoGameController CasinoGame_Punchout;
        public ExpandCasinoGameController CasinoGame_GunBall;
        public GameObject Table;
		public GameObject Table2;

        [NonSerialized]
        private RoomHandler m_ParentRoom;

        private void Start() {
        }

        private void Update() { }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

