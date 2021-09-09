﻿using Dungeonator;
using ExpandTheGungeon.ExpandObjects;
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

        //private GenericIntroDoer genericIntroDoer;
        //private FieldInfo m_currentPhase;
        //private string lastPhase;

        //public void Awake()
        //{
        //    this.genericIntroDoer = this.GetComponent<GenericIntroDoer>();
        //    m_currentPhase = typeof(GenericIntroDoer).GetField("m_currentPhase", BindingFlags.NonPublic | BindingFlags.Instance);
        //}

        public void Update()
        {
            //string currentPhase = m_currentPhase.GetValue(genericIntroDoer).ToString();

            //if (lastPhase != currentPhase)
            //{
            //    lastPhase = currentPhase;

            //    ETGModConsole.Log(currentPhase);
            //}

            if (!this.initialized)
            {
                this.thisWesternBro = base.aiAnimator;

                otherWesternBros = new List<AIAnimator>();

                foreach (var bro in StaticReferenceManager.AllBros)
                {
                    if (bro.gameObject != base.gameObject)
                    {
                        this.otherWesternBros.Add(bro.aiAnimator);
                    }
                }

                this.thisWesternBro.aiShooter.AimAtPoint(this.thisWesternBro.aiActor.CenterPosition + negativeGunOffset);
                this.thisWesternBro.FacingDirection = facingDirection;

                foreach (var bro in otherWesternBros)
                {
                    bro.aiShooter.AimAtPoint(bro.aiActor.CenterPosition + gunOffset);
                    bro.FacingDirection = facingDirection;
                }

                this.initialized = true;
            }
        }

        public override void PlayerWalkedIn(PlayerController player, List<tk2dSpriteAnimator> animators)
        {
            if (gameObject.GetComponent<AIActor>() && !m_ScreenFXObject) 
            {
                RoomHandler parentRoom = this.gameObject.GetComponent<AIActor>().GetAbsoluteParentRoom();
                m_ScreenFXObject = Instantiate(ExpandPrefabs.EXWestFloorBossIntroScreenFX, parentRoom.area.UnitCenter, Quaternion.identity);
                m_ScreenFXObject.transform.SetParent(GameManager.Instance.Dungeon.gameObject.transform);
            }
            

            if (this.thisWesternBro && this.otherWesternBros != null)
            {
                foreach (var bro in otherWesternBros)
                {
                    animators.Add(bro.spriteAnimator);
                }

                this.thisWesternBro.PlayUntilFinished("idle", false, null, -1f, false);
                this.thisWesternBro.aiShooter.AimAtPoint(this.thisWesternBro.aiActor.CenterPosition + negativeGunOffset);
                this.thisWesternBro.FacingDirection = facingDirection;

                foreach (var bro in otherWesternBros)
                {
                    bro.PlayUntilFinished("idle", false, null, -1f, false);
                    bro.aiShooter.AimAtPoint(bro.aiActor.CenterPosition + gunOffset);
                    bro.FacingDirection = facingDirection;
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
            //this.thisWesternBro.aiActor.ToggleRenderers(true);
            this.thisWesternBro.PlayUntilFinished("intro", false, null, -1f, false);
            //SpriteOutlineManager.ToggleOutlineRenderers(this.thisWesternBro.sprite, true);

            this.thisWesternBro.aiShooter.AimAtPoint(this.thisWesternBro.aiActor.CenterPosition + negativeGunOffset);
            this.thisWesternBro.FacingDirection = facingDirection;

            foreach (var bro in otherWesternBros)
            {
                //bro.aiActor.ToggleRenderers(true);
                bro.PlayUntilFinished("intro", false, null, -1f, false);
                //SpriteOutlineManager.ToggleOutlineRenderers(bro.sprite, true);

                bro.aiShooter.AimAtPoint(bro.aiActor.CenterPosition + gunOffset);
                bro.FacingDirection = facingDirection;
            }

            while (this.thisWesternBro.IsPlaying("intro"))
            {
                this.thisWesternBro.aiShooter.ToggleGunAndHandRenderers(false, rendererReason);

                foreach (var bro in otherWesternBros)
                {
                    bro.aiShooter.ToggleGunAndHandRenderers(false, rendererReason);
                }

                yield return null;
            }

            this.thisWesternBro.PlayUntilFinished("idle", false, null, -1f, false);
            this.thisWesternBro.aiShooter.ToggleGunAndHandRenderers(true, rendererReason);

            this.thisWesternBro.aiShooter.AimAtPoint(this.thisWesternBro.aiActor.CenterPosition + negativeGunOffset);
            this.thisWesternBro.FacingDirection = facingDirection;

            foreach (var bro in otherWesternBros)
            {
                bro.PlayUntilFinished("idle", false, null, -1f, false);
                bro.aiShooter.ToggleGunAndHandRenderers(true, rendererReason);

                bro.aiShooter.AimAtPoint(bro.aiActor.CenterPosition + gunOffset);
                bro.FacingDirection = facingDirection;
            }

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

            this.thisWesternBro.aiActor.ToggleRenderers(true);
            SpriteOutlineManager.ToggleOutlineRenderers(this.thisWesternBro.sprite, true);
            this.thisWesternBro.sprite.renderer.enabled = true;
            this.thisWesternBro.EndAnimation();
            this.thisWesternBro.aiShooter.ToggleGunAndHandRenderers(true, rendererReason);
            this.thisWesternBro.specRigidbody.CollideWithOthers = true;
            this.thisWesternBro.aiActor.IsGone = false;
            this.thisWesternBro.aiActor.State = AIActor.ActorState.Normal;

            this.thisWesternBro.aiShooter.AimAtPoint(this.thisWesternBro.aiActor.CenterPosition + negativeGunOffset);
            this.thisWesternBro.FacingDirection = facingDirection;

            foreach (var bro in otherWesternBros)
            {
                bro.aiActor.ToggleRenderers(true);
                SpriteOutlineManager.ToggleOutlineRenderers(bro.sprite, true);
                bro.sprite.renderer.enabled = true;
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