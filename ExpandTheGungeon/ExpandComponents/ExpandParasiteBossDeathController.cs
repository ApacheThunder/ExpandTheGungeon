using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandParasiteBossDeathController : BraveBehaviour {
    
        public ExpandParasiteBossDeathController() {
            explosionMidDelay = 0.3f;
            explosionCount = 10;
            bigExplosionMidDelay = 0.3f;
            bigExplosionCount = 10;
        }
    
    
        public List<GameObject> explosionVfx;
        public List<GameObject> bigExplosionVfx;

        public float explosionMidDelay;
        public float bigExplosionMidDelay;

        public int explosionCount;
        public int bigExplosionCount;
    
        public void Start() {
            healthHaver.ManualDeathHandling = true;
            healthHaver.OnPreDeath += OnBossDeath;
            healthHaver.OverrideKillCamTime = new float?(5f);
        }
    
        
        private void OnBossDeath(Vector2 dir) {
            behaviorSpeculator.enabled = false;
            aiActor.BehaviorOverridesVelocity = true;
            aiActor.BehaviorVelocity = Vector2.zero;
            aiAnimator.PlayUntilCancelled("die", false, null, -1f, false);
            GameManager.Instance.Dungeon.StartCoroutine(OnDeathExplosionsCR());
        }
    
        private IEnumerator OnDeathExplosionsCR() {
            // PastLabMarineController plmc = FindObjectOfType<PastLabMarineController>();
            PixelCollider collider = specRigidbody.HitboxPixelCollider;
            for (int i = 0; i < explosionCount; i++) {
                Vector2 minPos = collider.UnitBottomLeft;
                Vector2 maxPos = collider.UnitTopRight;
                GameObject vfxPrefab = BraveUtility.RandomElement(explosionVfx);
                Vector2 pos = BraveUtility.RandomVector2(minPos, maxPos, new Vector2(0.5f, 0.5f));
                GameObject vfxObj = SpawnManager.SpawnVFX(vfxPrefab, pos, Quaternion.identity);
                tk2dBaseSprite vfxSprite = vfxObj.GetComponent<tk2dBaseSprite>();
                vfxSprite.HeightOffGround = 3f;
                sprite.AttachRenderer(vfxSprite);
                sprite.UpdateZDepth();
                if (i < explosionCount - 1) { yield return new WaitForSeconds(explosionMidDelay); }
            }
            for (int j = 0; j < bigExplosionCount; j++) {
                Vector2 minPos2 = collider.UnitBottomLeft;
                Vector2 maxPos2 = collider.UnitTopRight;
                GameObject vfxPrefab2 = BraveUtility.RandomElement(bigExplosionVfx);
                Vector2 pos2 = BraveUtility.RandomVector2(minPos2, maxPos2, new Vector2(1f, 1f));
                GameObject vfxObj2 = SpawnManager.SpawnVFX(vfxPrefab2, pos2, Quaternion.identity);
                tk2dBaseSprite vfxSprite2 = vfxObj2.GetComponent<tk2dBaseSprite>();
                vfxSprite2.HeightOffGround = 3f;
                sprite.AttachRenderer(vfxSprite2);
                sprite.UpdateZDepth();
                if (j < bigExplosionCount - 1) { yield return new WaitForSeconds(bigExplosionMidDelay); }
            }
            healthHaver.DeathAnimationComplete(null, null);
            Destroy(gameObject);
            yield return new WaitForSeconds(2f);
            // Pixelator.Instance.FadeToColor(2f, Color.white, false, 0f);
            // plmc.OnBossKilled();
            yield break;
        }
    
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

