using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandTossSpriteOnDeath : OnDeathBehavior {

        public ExpandTossSpriteOnDeath() {
            deathType = DeathType.PreDeath;
            preDeathDelay = 0f;
            triggerName = string.Empty;
            applyRotation = true;
            isPowBlockDeath = true;

            PopupSpeed = 0.07f;
            DropSpeed = 0.09f;

            specificSpriteName = string.Empty;
        }

        public bool applyRotation;
        public bool isPowBlockDeath;

        public float PopupSpeed;
        public float DropSpeed;

        public string specificSpriteName;

        private bool m_hasTriggered;

        public void ManuallyTrigger(Vector2 damageDirection) { OnTrigger(damageDirection); }

        protected override void OnTrigger(Vector2 damageDirection) {
            if (m_hasTriggered) { return; }
            m_hasTriggered = true;

            if (!gameObject.GetComponent<tk2dSprite>()) { return; }
            if (aiActor) { aiActor.SetOutlines(false); }
            GameObject TossedSpriteVFX = new GameObject(gameObject.name + "Sprite Toss VFX") { layer = LayerMask.NameToLayer("Unoccluded") };
            tk2dSprite newSprite = TossedSpriteVFX.AddComponent<tk2dSprite>();
            if (!string.IsNullOrEmpty(specificSpriteName)) {
                newSprite.SetSprite(sprite.Collection, specificSpriteName);
            } else {
                newSprite.SetSprite(sprite.Collection, gameObject.GetComponent<tk2dSprite>().spriteId);
            }
            newSprite.HeightOffGround = 4;
            TossedSpriteVFX.SetLayerRecursively(LayerMask.NameToLayer("Unoccluded"));
            TossedSpriteVFX.transform.position = transform.position;
            TossedSpriteVFX.transform.localScale = transform.localScale;

            if (applyRotation) { TossedSpriteVFX.transform.rotation = transform.rotation; }
            newSprite.UpdateZDepth();

            ExpandTossVFX tossVFX = TossedSpriteVFX.AddComponent<ExpandTossVFX>();
            tossVFX.DoRotation = applyRotation;
            tossVFX.MovementSpeed1 = PopupSpeed;
            tossVFX.MovementSpeed2 = DropSpeed;
            tossVFX.Init();

            if (isPowBlockDeath) { AkSoundEngine.PostEvent("Play_EX_PowBlock_EnemyDeath", TossedSpriteVFX); }
            sprite.renderer.enabled = false;
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }

    public class ExpandTossVFX : BraveBehaviour {

        public ExpandTossVFX() {
            DoRotation = true;

            MovementSpeed1 = 0.07f;
            MovementSpeed2 = 0.09f;

            m_Finished = false;
        }

        public bool DoRotation;

        public float MovementSpeed1;
        public float MovementSpeed2;


        private bool m_Finished;

        private void Start() { }

        public void Init() {
            StartCoroutine(HandleSpriteToss(gameObject));
            if (DoRotation) { StartCoroutine(HandleSpriteRotation(gameObject)); }
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
