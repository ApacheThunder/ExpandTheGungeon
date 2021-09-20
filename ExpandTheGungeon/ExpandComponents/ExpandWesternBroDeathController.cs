using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ItemAPI;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents
{
    internal class ExpandWesternBroDeathController : BraveBehaviour
    {
        protected void Start()
        {
            base.healthHaver.OnPreDeath += this.OnDeath;
        }

        // TODO could this have a problem with two bros dying in the same damage instance because its now on pre death (because on death doesn't allow to use AdditionalSafeItemDrops, we could use LootEngine.SpawnItem ourselves though)?
        private void OnDeath(Vector2 finalDeathDir)
        {
            bool oneSurvivor = false;
            foreach (var bro in ExpandStaticReferenceManager.AllWesternBros)
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
                PickupObject rewardRevolver;

                if (aiActor.ParentRoom != null && !aiActor.ParentRoom.PlayerHasTakenDamageInThisRoom)
                {
                    rewardRevolver = PickupObjectDatabase.GetById(BlackAndGoldenRevolver.GoldenRevolverID);
                }
                else
                {
                    rewardRevolver = PickupObjectDatabase.GetById(BlackAndGoldenRevolver.BlackRevolverID);
                }

                if (rewardRevolver && base.aiActor)
                {
                    base.aiActor.AdditionalSafeItemDrops.Add(rewardRevolver);
                }
            }
        }
    }
}