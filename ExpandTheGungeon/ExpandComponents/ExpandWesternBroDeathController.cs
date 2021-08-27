using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents
{
    internal class ExpandWesternBroDeathController : BraveBehaviour
    {
        private void Start()
        {
            base.healthHaver.OnDeath += this.OnDeath;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        
        private void OnDeath(Vector2 finalDeathDir)
        {
            bool oneSurvivor = false;
            foreach (var bro in StaticReferenceManager.AllBros)
            {
                // if bro not null and not ourself
                if (bro && this.gameObject != bro.gameObject)
                {
                    bro.Enrage();
                    oneSurvivor = true;
                }
            }

            if (!oneSurvivor)
            {
                // TODO give reward or set flag here
                ETGModConsole.Log("Western Bros defeated");

                if (aiActor.ParentRoom != null && !aiActor.ParentRoom.PlayerHasTakenDamageInThisRoom)
                {
                    ETGModConsole.Log("Flawless Victory");
                }
            }
        }
    }
}