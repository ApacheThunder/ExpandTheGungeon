using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandHideGunHandsOnDeath : BraveBehaviour {

        public ExpandHideGunHandsOnDeath() {
            gunHands = new List<GunHandController>();
            m_hasTriggered = false;
        }
        
        public List<GunHandController> gunHands;

        private bool m_hasTriggered;

        public void Start() { healthHaver.OnPreDeath += OnPreDeath; }
        
        private void OnPreDeath(Vector2 dirVec) {
            if (m_hasTriggered) {
                return;
            } else {
                m_hasTriggered = true;
                if (gunHands.Count <= 0) { return; }
                foreach (GunHandController gunHand in gunHands) {
                    if (gunHand.Gun) {                        
                        gunHand.Gun.renderer.enabled = false;
                        SpriteOutlineManager.ToggleOutlineRenderers(gunHand.Gun.sprite, false);
                    }
                    if (gunHand.handObject) {
                        gunHand.handObject.sprite.renderer.enabled = false;
                        SpriteOutlineManager.ToggleOutlineRenderers(gunHand.handObject.sprite, false);
                        gunHand.handObject.gameObject.SetActive(false);
                    }
                    if (gunHand.sprite) {
                        gunHand.sprite.renderer.enabled = false;
                        SpriteOutlineManager.ToggleOutlineRenderers(gunHand.sprite, false);
                    }
                }
            }
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

