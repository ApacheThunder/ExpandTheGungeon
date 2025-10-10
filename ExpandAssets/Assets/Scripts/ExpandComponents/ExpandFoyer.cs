using System;
using UnityEngine;

namespace ExpandTheGungeon {

    public class ExpandFoyer : BraveBehaviour {
		
        [NonSerialized]
        public static GameObject EXFoyerChecker;
		[NonSerialized]
        public static ExpandFoyer Instance;
        
        public ExpandFoyer() {
            m_State = State.PreFoyerCheck;
        }

        private enum State { PreFoyerCheck, CheckSettings, SpawnObjects, Exit, Inactive };
        private State m_State;
        private GameObject m_FoyerButton;

        public void Awake() { }
        public void Start() { }

        public void Update() { }

        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

