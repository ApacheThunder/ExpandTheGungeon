using System;
using UnityEngine;

namespace ExpandTheGungeon {

    public class ExpandFoyer : BraveBehaviour {
		
		[NonSerialized]
		public static GameObject EXFoyerChecker;

        public ExpandFoyer() { m_State = State.PreFoyerCheck; }

        private enum State { PreFoyerCheck, CheckSettings, SpawnObjects, Exit };
        private State m_State;

        public void Awake() { }
        public void Start() { }

        public void Update() { }

        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

