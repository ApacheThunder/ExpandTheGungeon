using Dungeonator;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandPrefab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents
{
    [RequireComponent(typeof(GenericIntroDoer))]
    public class ExpandWesternBroIntroDoer : SpecificIntroDoer
    {
        private bool initialized;

        private bool finished;

        private GameObject m_ScreenFXObject;

        private AIAnimator thisWesternBro;

        private List<AIAnimator> otherWesternBros;

        private Vector2 gunOffset = new Vector2(10f, -2f);

        private Vector2 negativeGunOffset = new Vector2(-10f, -2f);

        private readonly float facingDirection = -90f;

        private readonly string rendererReason = "WesternBrosIntroDoer";

        public override Vector2? OverrideIntroPosition
        {
            get
            {
                return new Vector2?(this.thisWesternBro.specRigidbody.GetUnitCenter(ColliderType.HitBox) + new Vector2(4, 0));
            }
        }

        public override bool IsIntroFinished
        {
            get
            {
                return this.finished;
            }
        }

        public void Update()
        {
            if (!this.initialized)
            {
                this.thisWesternBro = base.aiAnimator;

                otherWesternBros = new List<AIAnimator>();

                foreach (var bro in ExpandStaticReferenceManager.AllWesternBros)
                {
                    if (bro.gameObject != base.gameObject)
                    {
                        this.otherWesternBros.Add(bro.aiAnimator);
                    }
                }

                this.thisWesternBro.aiShooter.ToggleGunAndHandRenderers(false, rendererReason);
                this.thisWesternBro.aiShooter.AimAtPoint(this.thisWesternBro.aiActor.CenterPosition + negativeGunOffset);
                this.thisWesternBro.FacingDirection = facingDirection;

                foreach (var bro in otherWesternBros)
                {
                    bro.aiShooter.ToggleGunAndHandRenderers(false, rendererReason);
                    bro.aiShooter.AimAtPoint(bro.aiActor.CenterPosition + gunOffset);
                    bro.FacingDirection = facingDirection;
                }

                this.initialized = true;
            }
        }

        private void AnimationEventTriggered(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frame)
        {
            if (clip.GetFrame(frame).eventInfo == "guntoggle")
            {
                this.thisWesternBro.aiShooter.ToggleGunAndHandRenderers(true, rendererReason);
                foreach (var bro in otherWesternBros)
                {
                    bro.aiShooter.ToggleGunAndHandRenderers(true, rendererReason);
                }
            }
        }

        public override void PlayerWalkedIn(PlayerController player, List<tk2dSpriteAnimator> animators)
        {
            if (gameObject.GetComponent<AIActor>() && !m_ScreenFXObject)
            {
                RoomHandler parentRoom = this.gameObject.GetComponent<AIActor>().GetAbsoluteParentRoom();
                m_ScreenFXObject = Instantiate(ExpandAssets.LoadAsset<GameObject>("EXWestFloorBossIntroScreenFX"), parentRoom.area.UnitCenter, Quaternion.identity);
                m_ScreenFXObject.transform.SetParent(GameManager.Instance.Dungeon.gameObject.transform);
            }

            if (this.thisWesternBro && this.otherWesternBros != null)
            {
                foreach (var bro in otherWesternBros)
                {
                    animators.Add(bro.spriteAnimator);
                }

                this.thisWesternBro.aiAnimator.enabled = false;
                this.thisWesternBro.spriteAnimator.AnimationEventTriggered = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>)Delegate.Combine(spriteAnimator.AnimationEventTriggered, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>(AnimationEventTriggered));

                this.thisWesternBro.spriteAnimator.Play("idle");
                this.thisWesternBro.aiShooter.AimAtPoint(this.thisWesternBro.aiActor.CenterPosition + negativeGunOffset);
                this.thisWesternBro.FacingDirection = facingDirection;

                foreach (var bro in otherWesternBros)
                {
                    bro.spriteAnimator.Play("idle");
                    bro.aiShooter.AimAtPoint(bro.aiActor.CenterPosition + gunOffset);
                    bro.FacingDirection = facingDirection;
                    bro.aiAnimator.enabled = false;
                    bro.spriteAnimator.AnimationEventTriggered = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>)Delegate.Combine(spriteAnimator.AnimationEventTriggered, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>(AnimationEventTriggered));
                }
            }

            base.StartCoroutine(this.FuckOutlines());
        }

        private IEnumerator FuckOutlines()
        {
            yield return null;

            SpriteOutlineManager.ToggleOutlineRenderers(this.thisWesternBro.sprite, false);

            foreach (var bro in otherWesternBros)
            {
                SpriteOutlineManager.ToggleOutlineRenderers(bro.sprite, false);
            }

            yield break;
        }

        public override void StartIntro(List<tk2dSpriteAnimator> animators)
        {
            base.StartCoroutine(this.DoIntro());
        }

        public override void OnBossCard()
        {
            this.thisWesternBro.aiShooter.ToggleGunAndHandRenderers(true, rendererReason);

            foreach (var bro in otherWesternBros)
            {
                bro.aiShooter.ToggleGunAndHandRenderers(true, rendererReason);
            }
        }

        private IEnumerator DoIntro()
        {
            this.thisWesternBro.spriteAnimator.Play("intro");
            this.thisWesternBro.aiShooter.AimAtPoint(this.thisWesternBro.aiActor.CenterPosition + negativeGunOffset);
            this.thisWesternBro.FacingDirection = facingDirection;

            foreach (var bro in otherWesternBros)
            {
                bro.spriteAnimator.Play("intro");
                bro.aiShooter.AimAtPoint(bro.aiActor.CenterPosition + gunOffset);
                bro.FacingDirection = facingDirection;
            }

            this.thisWesternBro.aiShooter.ToggleGunAndHandRenderers(false, rendererReason);

            foreach (var bro in otherWesternBros)
            {
                bro.aiShooter.ToggleGunAndHandRenderers(false, rendererReason);
            }

            while (this.thisWesternBro.spriteAnimator.IsPlaying("intro"))
            {
                yield return null;
            }

            foreach (var bro in otherWesternBros)
            {
                bro.spriteAnimator.Play("intro2");

                bro.aiShooter.AimAtPoint(bro.aiActor.CenterPosition + gunOffset);
                bro.FacingDirection = facingDirection;
            }
            this.thisWesternBro.spriteAnimator.Play("intro2");

            this.thisWesternBro.aiShooter.AimAtPoint(this.thisWesternBro.aiActor.CenterPosition + negativeGunOffset);
            this.thisWesternBro.FacingDirection = facingDirection;

            float elapsed = 0f;
            float duration = 1f;

            while (elapsed < duration)
            {
                yield return null;
                elapsed += GameManager.INVARIANT_DELTA_TIME;
            }
            this.finished = true;
            yield break;
        }

        public override void EndIntro()
        {
            this.finished = true;
            base.StopAllCoroutines();

            this.thisWesternBro.aiAnimator.enabled = true;

            SpriteOutlineManager.ToggleOutlineRenderers(this.thisWesternBro.sprite, true);
            this.thisWesternBro.EndAnimation();
            this.thisWesternBro.aiShooter.ToggleGunAndHandRenderers(true, rendererReason);
            this.thisWesternBro.specRigidbody.CollideWithOthers = true;
            this.thisWesternBro.aiActor.IsGone = false;
            this.thisWesternBro.aiActor.State = AIActor.ActorState.Normal;

            this.thisWesternBro.aiShooter.AimAtPoint(this.thisWesternBro.aiActor.CenterPosition + negativeGunOffset);
            this.thisWesternBro.FacingDirection = facingDirection;

            foreach (var bro in otherWesternBros)
            {
                bro.aiAnimator.enabled = true;
                SpriteOutlineManager.ToggleOutlineRenderers(bro.sprite, true);
                bro.EndAnimation();
                bro.aiShooter.ToggleGunAndHandRenderers(true, rendererReason);
                bro.specRigidbody.CollideWithOthers = true;
                bro.aiActor.IsGone = false;
                bro.aiActor.State = AIActor.ActorState.Normal;
                bro.aiShooter.AimAtPoint(bro.aiActor.CenterPosition + gunOffset);
                bro.FacingDirection = facingDirection;
            }

            if (this.m_ScreenFXObject)
            {
                Destroy(m_ScreenFXObject);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}