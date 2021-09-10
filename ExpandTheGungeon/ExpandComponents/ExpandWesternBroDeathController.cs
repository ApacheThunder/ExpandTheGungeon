using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents
{
    internal class ExpandWesternBroDeathController : BraveBehaviour
    {
        private void Start()
        {
            base.healthHaver.OnDeath += this.OnDeath;
        }
        
        private void OnDeath(Vector2 finalDeathDir)
        {
            bool oneSurvivor = false;
            foreach (var bro in ExpandWesternBroController.AllWesternBros)
            {
                // if bro not null, not ourself and alive
                if (bro && this.gameObject != bro.gameObject && bro.healthHaver && bro.healthHaver.IsAlive)
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