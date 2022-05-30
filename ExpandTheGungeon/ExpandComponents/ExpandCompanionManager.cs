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
            ToggleFaceSouthWhenStopped = false;

            NoTargetFaceType = AIAnimator.FacingType.Movement;
            WithTargetFaceType = AIAnimator.FacingType.Default;

            Scale = 0.65f;
            m_WasRescaled = false;

            UpdateTimer = 1.5f;
            m_Timer = UpdateTimer;
        }
        
        public AIAnimator.FacingType NoTargetFaceType;
        public AIAnimator.FacingType WithTargetFaceType;

        public float Scale;
        public float UpdateTimer;

        public bool Rescale;
        public bool SwapFaceTypesOnTarget;
        public bool ToggleFaceSouthWhenStopped;
        public bool HideGunsWhenNoTarget;

        [NonSerialized]
        private AIActor m_AIActor;
        [NonSerialized]
        private AIShooter m_AIShooter;
        [NonSerialized]
        private bool m_WasRescaled;
        [NonSerialized]
        private float m_Timer;

        private void Awake() {
            m_AIActor = aiActor;
            m_AIShooter = aiShooter;
            if (HideGunsWhenNoTarget && (!m_AIActor | !m_AIShooter | m_AIActor.TargetRigidbody)) { return; }
            m_AIShooter.ToggleGunAndHandRenderers(false, "Companion gun toggle for target change");
        }

        private void Start() { }

        private void Update() {
            if (!m_AIActor) { Destroy(this); return; }

            if (Rescale && !m_WasRescaled) {
                m_WasRescaled = true;
                gameObject.layer = LayerMask.NameToLayer("Unpixelated");
                SpriteOutlineManager.ChangeOutlineLayer(sprite, gameObject.layer);
                m_AIActor.EnemyScale = new Vector2(Scale, Scale);
                sprite.UpdateZDepth();
            }

            if (UpdateTimer != -1 && m_AIActor && !m_AIActor.TargetRigidbody) {
                m_Timer -= BraveTime.DeltaTime;
            } else if (UpdateTimer != -1 && m_AIActor && m_AIActor.TargetRigidbody && m_Timer != UpdateTimer) {
                m_Timer = UpdateTimer;
            }
            

            if (HideGunsWhenNoTarget && m_AIShooter && m_AIShooter.CurrentGun) {
                if (!m_AIActor.TargetRigidbody && m_AIShooter.CurrentGun.renderer.enabled && m_Timer < 0) {
                    m_AIShooter.ToggleGunAndHandRenderers(false, "Companion gun toggle for target change");
                } else if (m_AIActor.TargetRigidbody && !m_AIShooter.CurrentGun.renderer.enabled) {
                    m_AIShooter.ToggleGunAndHandRenderers(true, "Companion gun toggle for target change");
                }
            }

            if (SwapFaceTypesOnTarget && !m_AIActor.aiAnimator) { Destroy(this); return; }

            if (SwapFaceTypesOnTarget && m_AIActor.aiAnimator.facingType != NoTargetFaceType && !m_AIActor.TargetRigidbody && m_Timer < 0) {
                m_AIActor.aiAnimator.facingType = NoTargetFaceType;
            } else if (SwapFaceTypesOnTarget && m_AIActor.aiAnimator.facingType != WithTargetFaceType && m_AIActor.TargetRigidbody) {
                m_AIActor.aiAnimator.facingType = WithTargetFaceType;
            }
            
            if (ToggleFaceSouthWhenStopped && m_AIActor.TargetRigidbody) {
                if (m_AIActor.aiAnimator.faceSouthWhenStopped) { m_AIActor.aiAnimator.faceSouthWhenStopped = false; }
                if (!m_AIActor.aiAnimator.faceTargetWhenStopped) { m_AIActor.aiAnimator.faceTargetWhenStopped = true; } 
            } else if (ToggleFaceSouthWhenStopped && !m_AIActor.TargetRigidbody && m_Timer < 0) {
                if (!m_AIActor.aiAnimator.faceSouthWhenStopped) { m_AIActor.aiAnimator.faceSouthWhenStopped = true; }
                if (m_AIActor.aiAnimator.faceTargetWhenStopped) { m_AIActor.aiAnimator.faceTargetWhenStopped = false; }
            }

            if (UpdateTimer != -1 && m_Timer < 0) { m_Timer = UpdateTimer; }
        }       

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

