using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandExplodeOnDeath : ExplodeOnDeath {

        public ExpandExplodeOnDeath() {
            immuneToIBombApp = false;
            deathType = DeathType.PreDeath;
            preDeathDelay = 0.1f;

            useDefaultExplosion = false;            
            spawnItemsOnExplosion = false;
            isCorruptedObject = false;
            ExplosionNotGuranteed = false;
            isCorruptedNPC = false;

            numberOfDefaultItemsToSpawn = 1;

            ExplosionOdds = 0.3f;
            ExplosionDamage = 120f;
            
            m_hasTriggered = false;
            // m_GrenadeGuyPrefab = EnemyDatabase.GetOrLoadByGuid("4d37ce3d666b4ddda8039929225b7ede").gameObject;            
            // ExpandExplosionData = m_GrenadeGuyPrefab.gameObject.GetComponent<ExplodeOnDeath>().explosionData;
            // This uses Explosion Data settings from Grenade Kin by default
            ExpandExplosionData = ExpandUtility.GenerateExplosionData();
        }

        public bool useDefaultExplosion;
        public bool spawnItemsOnExplosion;
        public bool isCorruptedObject;
        public bool isCorruptedNPC;
        public bool ExplosionNotGuranteed;

        public int numberOfDefaultItemsToSpawn;

        public float ExplosionOdds;
        public float ExplosionDamage;

        private bool m_hasTriggered;

        public List<PickupObject> ItemList;

        public ExplosionData ExpandExplosionData;

        public BaseShopController NPCShop;

        public void ManuallyTrigger(Vector2 damageDirection) { OnTrigger(damageDirection); }

        protected override void OnTrigger(Vector2 dirVec) {
            if (m_hasTriggered) {
                return;
            } else {
                m_hasTriggered = true;


                if (isCorruptedNPC) { EnableShopTheftAndCurse(); }
                if (isCorruptedObject) { DoCorruptedDeathSFX(); }

                DoExplosion();

                if (spawnItemsOnExplosion) { DoItemSpawn(); }
            }
        }


        private void DoCorruptedDeathSFX() {
            GameObject SoundDummyFX = new GameObject();
            SoundDummyFX.transform.position = gameObject.transform.position;
            if (SoundDummyFX.transform.position.GetAbsoluteRoom() != null) {
                SoundDummyFX.transform.parent = SoundDummyFX.transform.position.GetAbsoluteRoom().hierarchyParent;
                GameManager.Instance.StartCoroutine(DoDelayedCorruptionDeathSound(SoundDummyFX));
            } else {
                Destroy(SoundDummyFX);
            }
        }

        private void EnableShopTheftAndCurse() {
            PlayerController player = GameManager.Instance.BestActivePlayer;
            StatModifier m_CurseUp = new StatModifier { amount = 1, modifyType = StatModifier.ModifyMethod.ADDITIVE, statToBoost = PlayerStats.StatType.Curse };
            player.ownerlessStatModifiers.Add(m_CurseUp);
            player.stats.RecalculateStats(player);
            if (NPCShop != null) { NPCShop.SetCapableOfBeingStolenFrom(true, "DestroyedShopOwner", null); }
        }

        private void DoExplosion() {

            if (ExplosionNotGuranteed) { if (Random.value > ExplosionOdds) { return; } }

            if (useDefaultExplosion) {
                Exploder.DoDefaultExplosion(specRigidbody.GetUnitCenter(ColliderType.HitBox), Vector2.zero, null, true, CoreDamageTypes.None);
                Exploder.DoRadialDamage(ExplosionDamage, specRigidbody.GetUnitCenter(ColliderType.HitBox), 3.5f, true, true, false);
            } else {
                Exploder.Explode(specRigidbody.GetUnitCenter(ColliderType.HitBox), ExpandExplosionData, Vector2.zero, null, true, CoreDamageTypes.None, false);
            }
        }

        private void DoItemSpawn() {

            if (ItemList == null) {

                ItemList = new List<PickupObject>();               

                if (numberOfDefaultItemsToSpawn == 1) {
                    PickupObject.ItemQuality targetQuality = (Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.C : PickupObject.ItemQuality.D) : PickupObject.ItemQuality.B;
                    GenericLootTable lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
                    PickupObject item = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
                    if (item) { LootEngine.SpawnItem(item.gameObject, specRigidbody.GetUnitCenter(ColliderType.HitBox), Vector2.zero, 0f, true, true, false); }
                    return;
                } else {                    
                    for (int i = 0; i < numberOfDefaultItemsToSpawn; i++ ){
                        PickupObject.ItemQuality targetQuality = (Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.C : PickupObject.ItemQuality.D) : PickupObject.ItemQuality.B;
                        GenericLootTable lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
                        PickupObject item = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
                        if (item) { ItemList.Add(item); }
                    }
                }
            }

            if (ItemList.Count <= 0) { return; }
            if (ItemList.Count == 1) {
                LootEngine.SpawnItem(ItemList[0].gameObject, specRigidbody.GetUnitCenter(ColliderType.HitBox), Vector2.zero, 0f, true, true, false);
                return;
            } else if (ItemList.Count > 1) {
                foreach (PickupObject pickupObject in ItemList) {
                    LootEngine.SpawnItem(pickupObject.gameObject, specRigidbody.GetUnitCenter(ColliderType.HitBox), Vector2.zero, 0f, true, true, false);
                }
            }
            return;
        }

        private IEnumerator DoDelayedCorruptionDeathSound(GameObject parentObject, float delay = 0.2f) {
            yield return new WaitForSeconds(delay);
            AkSoundEngine.PostEvent("Play_EX_CorruptedObjectDestroyed_01", parentObject);
            yield break;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

