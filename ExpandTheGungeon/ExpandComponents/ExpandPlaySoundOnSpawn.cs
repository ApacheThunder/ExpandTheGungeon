using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandPlaySoundOnSpawn : BraveBehaviour {

        [SerializeField]
        public string SoundEvent;

        public void Start() {
            if (!string.IsNullOrEmpty(SoundEvent)) {
                AkSoundEngine.PostEvent(SoundEvent, gameObject);
                Destroy(this);
            }
        }

        public void Update() { }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

