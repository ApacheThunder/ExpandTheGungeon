using System;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    // Set this up to toggle direction face type based on whether or not my companion has a target.
    // Currently used with Cultist Companion.

    public class ExpandCompanionManager : BraveBehaviour {

        public ExpandCompanionManager() {
            SwapFaceTypesOnTarget = true;
            HideGunsWhenNoTarget = true;
            Rescale = false;

            NoTargetFaceType = AIAnimator.FacingType.Movement;
            WithTargetFaceType = AIAnimator.FacingType.Default;

            Scale = 0.65f;

            m_WasRescaled = false;
        }
        
        public AIAnimator.FacingType NoTargetFaceType;
        public AIAnimator.FacingType WithTargetFaceType;

        public float Scale;

        public bool Rescale;
        public bool SwapFaceTypesOnTarget;
        public bool HideGunsWhenNoTarget;

        [NonSerialized]
        private AIActor m_AIActor;
        [NonSerialized]
        private AIShooter m_AIShooter;
        [NonSerialized]
        private bool m_WasRescaled;

        private void Start() { m_AIActor = aiActor; m_AIShooter = aiShooter; }

        private void Update() {
            if (!m_AIActor) { Destroy(this); return; }

            if (Rescale && !m_WasRescaled) {
                m_WasRescaled = true;
                gameObject.layer = LayerMask.NameToLayer("Unpixelated");
                SpriteOutlineManager.ChangeOutlineLayer(sprite, gameObject.layer);
                m_AIActor.EnemyScale = new Vector2(Scale, Scale);
                sprite.UpdateZDepth();
            }

            if (HideGunsWhenNoTarget && m_AIShooter && m_AIShooter.CurrentGun) {
                if (!m_AIActor.TargetRigidbody && m_AIShooter.CurrentGun.renderer.enabled) {
                    m_AIShooter.ToggleGunAndHandRenderers(false, "Companion gun toggle for target change");
                } else if (m_AIActor.TargetRigidbody && !m_AIShooter.CurrentGun.renderer.enabled) {
                    m_AIShooter.ToggleGunAndHandRenderers(true, "Companion gun toggle for target change");
                }
            }

            if (SwapFaceTypesOnTarget && !m_AIActor.aiAnimator) { Destroy(this); return; }

            if (SwapFaceTypesOnTarget && m_AIActor.aiAnimator.facingType != NoTargetFaceType && !m_AIActor.TargetRigidbody) {
                m_AIActor.aiAnimator.facingType = NoTargetFaceType;
            } else if (SwapFaceTypesOnTarget && m_AIActor.aiAnimator.facingType != WithTargetFaceType && m_AIActor.TargetRigidbody) {
                m_AIActor.aiAnimator.facingType = WithTargetFaceType;
            }
        }       

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

