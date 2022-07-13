using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandSpawnBowlerNoteOnDeath : OnDeathBehavior {

        public ExpandSpawnBowlerNoteOnDeath() {
            deathType = DeathType.PreDeath;
            preDeathDelay = 0f;
            triggerName = "";
            PossibleStrings = new string[] {
                "This enemy doesn't look like a {wb}rainbow chest{w} to me!\n\nNo RAAAAAIIIINBOW, no item!\n\n{wb}-Bowler{w}",
            };
        }

        public string[] PossibleStrings;
        
        private bool m_hasTriggered;
        

        public void ManuallyTrigger(Vector2 damageDirection) { OnTrigger(damageDirection); }

        protected override void OnTrigger(Vector2 damageDirection) {
            if (m_hasTriggered) { return; }
            m_hasTriggered = true;
            if (aiActor.ParentRoom != null && PossibleStrings != null && PossibleStrings.Length > 0) {
                if (PossibleStrings.Length > 1) {
                    ExpandUtility.SpawnCustomBowlerNote(GameManager.Instance.RewardManager.BowlerNoteOtherSource, sprite.WorldCenter, aiActor.ParentRoom, BraveUtility.RandomElement(PossibleStrings), false);
                } else {
                    ExpandUtility.SpawnCustomBowlerNote(GameManager.Instance.RewardManager.BowlerNoteOtherSource, sprite.WorldCenter, aiActor.ParentRoom, PossibleStrings[0], false);
                }
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

