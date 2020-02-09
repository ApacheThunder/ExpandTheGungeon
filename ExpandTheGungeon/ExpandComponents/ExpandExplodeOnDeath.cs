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
            spawnShardsOnDeath = false;

            numberOfDefaultItemsToSpawn = 1;

            ExplosionOdds = 0.3f;
            ExplosionDamage = 120f;

            breakStyle = MinorBreakable.BreakStyle.BURST;
            direction = Vector2.zero;
            minAngle = 0;
            maxAngle = 0;
            verticalSpeed = 0.4f;
            minMagnitude = 0.25f;
            maxMagnitude = 0.5f;
            heightOffGround = 0.1f;

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
        public bool spawnShardsOnDeath;

        public int numberOfDefaultItemsToSpawn;

        public float ExplosionOdds;
        public float ExplosionDamage;

        private bool m_hasTriggered;

        public List<PickupObject> ItemList;

        public ExplosionData ExpandExplosionData;

        public BaseShopController NPCShop;

        public MinorBreakable.BreakStyle breakStyle;

        public Vector2 direction;

        public float minAngle;
        public float maxAngle;
        public float verticalSpeed;
        public float minMagnitude;
        public float maxMagnitude;
        public float heightOffGround;

        public ShardCluster[] shardClusters;

        public void ManuallyTrigger(Vector2 damageDirection) { OnTrigger(damageDirection); }

        protected override void OnTrigger(Vector2 dirVec) {
            if (m_hasTriggered) {
                return;
            } else {
                m_hasTriggered = true;


                if (isCorruptedNPC) { EnableShopTheftAndCurse(); }
                if (isCorruptedObject) { DoCorruptedDeathSFX(); }

                if (spawnShardsOnDeath) { HandleShardSpawns(dirVec); }

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

        public void HandleShardSpawns(Vector2 sourceVelocity) {
            MinorBreakable.BreakStyle breakStyle = this.breakStyle;
            if (sourceVelocity == Vector2.zero && this.breakStyle != MinorBreakable.BreakStyle.CUSTOM) { breakStyle = MinorBreakable.BreakStyle.BURST; }
            float num = 1.5f;
            switch (breakStyle) {
                case MinorBreakable.BreakStyle.CONE:
                    SpawnShards(sourceVelocity, -45f, 45f, num, sourceVelocity.magnitude * 0.5f, sourceVelocity.magnitude * 1.5f);
                    break;
                case MinorBreakable.BreakStyle.BURST:
                    SpawnShards(Vector2.right, -180f, 180f, num, 1f, 2f);
                    break;
                case MinorBreakable.BreakStyle.JET:
                    SpawnShards(sourceVelocity, -15f, 15f, num, sourceVelocity.magnitude * 0.5f, sourceVelocity.magnitude * 1.5f);
                    break;
                default:
                    if (breakStyle == MinorBreakable.BreakStyle.CUSTOM) {
                        SpawnShards(direction, minAngle, maxAngle, verticalSpeed, minMagnitude, maxMagnitude);
                    }
                    break;
            }
        }

        public void SpawnShards(Vector2 direction, float minAngle, float maxAngle, float verticalSpeed, float minMagnitude, float maxMagnitude) {
            Vector3 position = specRigidbody.GetUnitCenter(ColliderType.HitBox);
            if (shardClusters != null && shardClusters.Length > 0) {
                int num = Random.Range(0, 10);
                for (int i = 0; i < shardClusters.Length; i++) {
                    ShardCluster shardCluster = shardClusters[i];
                    int num2 = Random.Range(shardCluster.minFromCluster, shardCluster.maxFromCluster + 1);
                    int num3 = Random.Range(0, shardCluster.clusterObjects.Length);
                    for (int j = 0; j < num2; j++) {
                        float lowDiscrepancyRandom = BraveMathCollege.GetLowDiscrepancyRandom(num);
                        num++;
                        float z = Mathf.Lerp(minAngle, maxAngle, lowDiscrepancyRandom);
                        Vector3 vector = Quaternion.Euler(0f, 0f, z) * (direction.normalized * Random.Range(minMagnitude, maxMagnitude)).ToVector3ZUp(verticalSpeed);
                        int num4 = (num3 + j) % shardCluster.clusterObjects.Length;
                        GameObject gameObject = SpawnManager.SpawnDebris(shardCluster.clusterObjects[num4].gameObject, position, Quaternion.identity);
                        tk2dSprite component = gameObject.GetComponent<tk2dSprite>();
                        if (sprite.attachParent != null && component != null) {
                            component.attachParent = sprite.attachParent;
                            component.HeightOffGround = sprite.HeightOffGround;
                        }
                        DebrisObject component2 = gameObject.GetComponent<DebrisObject>();
                        vector = Vector3.Scale(vector, shardCluster.forceAxialMultiplier) * shardCluster.forceMultiplier;
                        component2.Trigger(vector, heightOffGround, shardCluster.rotationMultiplier);
                    }
                }
            }
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

