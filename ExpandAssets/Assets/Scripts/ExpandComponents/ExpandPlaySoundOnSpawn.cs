using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandPlaySoundOnSpawn : BraveBehaviour {

        [SerializeField]
		public string SoundEvent;

        public void Start() { }

        public void Update() { }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

