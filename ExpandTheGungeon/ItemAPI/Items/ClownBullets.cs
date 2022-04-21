using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandPrefab;
using System.Collections.Generic;

namespace ExpandTheGungeon.ItemAPI {
    
    public class ClownBullets : PassiveItem {

        public ClownBullets() {
            ActivationChance = 1f;
            ActivationsPerSecond = 0.09f;
            MinActivationChance = 0.045f;
            ChanceToDevolve = 1;
            NormalizeAcrossFireRate = true;
            TargetGUID = string.Empty;
            EnemyGuidsToIgnore = new List<string>();
        }

        public static GameObject ClownBulletsObject;
        public static int ClownBulletsID;

        public static void Init(AssetBundle expandSharedAssets1) {
            ClownBulletsObject = expandSharedAssets1.LoadAsset<GameObject>("EXClownBullets");
            SpriteSerializer.AddSpriteToObject(ClownBulletsObject, ExpandPrefabs.EXItemCollection, "clownbullets");

            ClownBullets clownBullets = ClownBulletsObject.AddComponent<ClownBullets>();
            ClownBulletsObject.name = "Clown Bullets";
            string shortDesc = "Make fools of your enemy...";
            string longDesc = "Some have said bullet kin are quite foolish compared to the more experienced gundead. Devolve your enemies to the clowns they should be with these special rounds! ";
            ItemBuilder.SetupItem(clownBullets, shortDesc, longDesc, "ex");            
            clownBullets.quality = ItemQuality.D;
            if (!ExpandSettings.EnableEXItems) { clownBullets.quality = ItemQuality.EXCLUDED; }
            ClownBulletsID = clownBullets.PickupObjectId;
        }


        public float ActivationChance;
        public float ChanceToDevolve;

        public string TargetGUID;
        public List<string> EnemyGuidsToIgnore;

        public bool NormalizeAcrossFireRate;

        public float ActivationsPerSecond;        
        public float MinActivationChance;
        
        private PlayerController m_player;

        public override void Pickup(PlayerController player) {
            if (m_pickedUp) { return; }
            m_player = player;
            base.Pickup(player);
            player.PostProcessProjectile += PostProcessProjectile;
            if (string.IsNullOrEmpty(TargetGUID)) { TargetGUID = ExpandCustomEnemyDatabase.ClownkinGUID; }
        }
        
        
        private void PostProcessProjectile(Projectile obj, float effectChanceScalar) {
            float chanceToActivate = ActivationChance;
            Gun gun = (!m_player) ? null : m_player.CurrentGun;
            if (NormalizeAcrossFireRate && gun) {
                float num2 = 1f / gun.DefaultModule.cooldownTime;
                chanceToActivate = Mathf.Clamp01(ActivationsPerSecond / num2);                
                chanceToActivate = Mathf.Max(MinActivationChance, chanceToActivate);
            }            
            if (UnityEngine.Random.value < chanceToActivate) {
                if (!obj.gameObject.GetComponent<ClownBulletsModifier>()) {
                    ClownBulletsModifier clownbulletModifier = obj.gameObject.AddComponent<ClownBulletsModifier>();
                    clownbulletModifier.chanceToDevolve = ChanceToDevolve;
                    clownbulletModifier.ClownKinGUID = TargetGUID;
                    clownbulletModifier.EnemyGuidsToIgnore = EnemyGuidsToIgnore;
                }
            }
        }
        
        public override DebrisObject Drop(PlayerController player) {
            DebrisObject debrisObject = base.Drop(player);
            m_player = null;
            debrisObject.GetComponent<ClownBullets>().m_pickedUpThisRun = true;
            player.PostProcessProjectile -= PostProcessProjectile;
            return debrisObject;
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            if (m_player) {
                m_player.PostProcessProjectile -= PostProcessProjectile;
                PlayerController player = m_player;
            }
        }
    }


    public class ClownBulletsModifier : MonoBehaviour {

        public ClownBulletsModifier() {
            chanceToDevolve = 0.1f;
            ClownKinGUID = string.Empty;
            EnemyGuidsToIgnore = new List<string>();
        }
        
        public float chanceToDevolve;

        public string ClownKinGUID;

        public List<string> EnemyGuidsToIgnore;
        
        private void Start() {
            Projectile component = GetComponent<Projectile>();
            if (component) {
                Projectile projectile = component;
                projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.HandleHitEnemy));
            }
        }

        private void HandleHitEnemy(Projectile sourceProjectile, SpeculativeRigidbody enemyRigidbody, bool killingBlow) {
            if (killingBlow) { return; }
            if (!enemyRigidbody || !enemyRigidbody.aiActor) { return; }
            if (UnityEngine.Random.value > chanceToDevolve) { return; }
            AIActor aiActor = enemyRigidbody.aiActor;
            if (!aiActor.IsNormalEnemy || aiActor.IsHarmlessEnemy || aiActor.healthHaver.IsBoss) { return; }
            string enemyGuid = aiActor.EnemyGuid;
            for (int i = 0; i < EnemyGuidsToIgnore.Count; i++) {
                if (EnemyGuidsToIgnore[i] == enemyGuid) { return; }
            }            
            aiActor.Transmogrify(EnemyDatabase.GetOrLoadByGuid(ClownKinGUID), (GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Spawn_Poof"));
            AkSoundEngine.PostEvent("Play_WPN_devolver_morph_01", gameObject);
        }
    }
}

