using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandEnableCorpseOnDeath : OnDeathBehavior {
        
        public ExpandEnableCorpseOnDeath() {
            deathType = DeathType.PreDeath;
            preDeathDelay = 0f;
            triggerName = "";            
        }
        
        private bool m_hasTriggered;

        public void ManuallyTrigger(Vector2 damageDirection) { OnTrigger(damageDirection); }

        protected override void OnTrigger(Vector2 damageDirection) {
			if (m_hasTriggered) { return; }
            m_hasTriggered = true;
            aiActor.CorpseObject.SetActive(true);
		}

        protected override void OnDestroy() { base.OnDestroy(); }
	}
}
