using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandTossSpriteOnDeath : OnDeathBehavior {

        public ExpandTossSpriteOnDeath() {
            deathType = DeathType.PreDeath;
            preDeathDelay = 0f;
            triggerName = "";
        }

        private bool m_hasTriggered;

        public void ManuallyTrigger(Vector2 damageDirection) { OnTrigger(damageDirection); }

        protected override void OnTrigger(Vector2 damageDirection) {
            if (m_hasTriggered) { return; }
            m_hasTriggered = true;

            if (!gameObject.GetComponent<tk2dSprite>()) { return; }
            if (aiActor) { aiActor.SetOutlines(false); }
            GameObject TossedSpriteVFX = new GameObject(gameObject.name + "Sprite Toss VFX") { layer = LayerMask.NameToLayer("FG_Critical") };
            tk2dSprite newSprite = TossedSpriteVFX.AddComponent<tk2dSprite>();
            newSprite.Collection = sprite.Collection;
            newSprite.SetSprite(gameObject.GetComponent<tk2dSprite>().spriteId);
            newSprite.HeightOffGround = 4;
            TossedSpriteVFX.SetLayerRecursively(LayerMask.NameToLayer("Unoccluded"));
            TossedSpriteVFX.transform.position = transform.position;
            TossedSpriteVFX.transform.rotation = transform.rotation;
            newSprite.UpdateZDepth();
            TossedSpriteVFX.AddComponent<ExpandTossVFX>().Init();
            AkSoundEngine.PostEvent("Play_EX_PowBlock_EnemyDeath", TossedSpriteVFX);
            sprite.renderer.enabled = false;
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }

    public class ExpandTossVFX : BraveBehaviour {

        public ExpandTossVFX() { m_Finished = false; }

        private bool m_Finished;

        private void Start() { }

        public void Init() {
            StartCoroutine(HandleSpriteToss(gameObject));
            StartCoroutine(HandleSpriteRotation(gameObject));
        }

        private void Update() {
            if (m_Finished) {
                m_Finished = false;
                Destroy(gameObject);
                return;
            }
        }

        private IEnumerator HandleSpriteToss(GameObject vfxObject) {
            float elapsed = 0;
            float duration = 0.5f;
            float duration2 = 4f;

            float MovementSpeed1 = 0.07f;
            float MovementSpeed2 = 0.09f;
                        
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                vfxObject.transform.position += new Vector3(0, MovementSpeed1, 0);
                yield return null;
            }
            yield return null;
            while (elapsed < duration2) {
                elapsed += BraveTime.DeltaTime;
                vfxObject.transform.position -= new Vector3(0, MovementSpeed2, 0);
                yield return null;
            }
            yield return null;
            m_Finished = true;
            yield break;
        }

        private IEnumerator HandleSpriteRotation(GameObject vfxObject) {
            float angle = 0;

            while (vfxObject) {
                if (!vfxObject) { break; }

                angle += (BraveTime.DeltaTime * 6f);
            
                vfxObject.transform.RotateAround(sprite.WorldCenter, Vector3.forward, angle);
                yield return null;
            }
            
            yield break;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}
