using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandHideGunHandsOnDeath : OnDeathBehavior {

        public ExpandHideGunHandsOnDeath() {
            deathType = DeathType.PreDeath;
            preDeathDelay = 0.001f;
            gunHands = new List<GunHandController>();
            m_hasTriggered = false;
        }
        
        public List<GunHandController> gunHands;

        private bool m_hasTriggered;

        public void ManuallyTrigger(Vector2 damageDirection) { OnTrigger(damageDirection); }

        protected override void OnTrigger(Vector2 dirVec) {
            if (m_hasTriggered) {
                return;
            } else {
                m_hasTriggered = true;
                if (gunHands.Count <= 0) { return; }
                foreach (GunHandController gunHand in gunHands) {
                    if (gunHand.Gun) { gunHand.Gun.renderer.enabled = false; }
                    if (gunHand.handObject) { gunHand.handObject.sprite.renderer.enabled = false; }
                }
            }
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

