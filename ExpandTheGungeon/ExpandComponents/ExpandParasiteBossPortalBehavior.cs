using Pathfinding;
using UnityEngine;


namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandParasiteBossPortalBehavior : BasicAttackBehavior {

        public ExpandParasiteBossPortalBehavior() {
            GoneTime = 1f;
            PortalSize = 16f;
            teleportOutAnim = "teleport_out";
            teleportInAnim = "teleport_in";
            hasOutlinesDuringAnim = false;      
            portalAnim = "weak_attack_charge";
            shadowSupport = ShadowSupport.None;
            shadowOutAnim = null;
            shadowInAnim = null;
            ManuallyDefineRoom = true;
            roomMin = new Vector2(12, 47);
            roomMax = new Vector2(42, 63);
            Cooldown = 15;
            CooldownVariance = 15;
            AttackCooldown = 0;
            GlobalCooldown = 0;
            InitialCooldown = 0;
            InitialCooldownVariance = 0;
            GroupName = "teleport";
            GroupCooldown = 6;
            MinRange = 0;
            Range = 0;
            MinWallDistance = 0;
            MaxEnemiesInRoom = 0;
            MinHealthThreshold = 0;
            MaxHealthThreshold = 1;
            HealthThresholds = new float[0];
            AccumulateHealthThresholds = true;
            targetAreaStyle = null;
            IsBlackPhantom = false;
            resetCooldownOnDamage = null;
            RequiresLineOfSight = false;
            MaxUsages = 0;
        }

        public bool AttackableDuringAnimation;

        public float GoneTime;
        public float PortalSize;
        public string teleportOutAnim;
        public string teleportInAnim;
        public bool teleportRequiresTransparency;
        public bool hasOutlinesDuringAnim;
        public string portalAnim;

        public ShadowSupport shadowSupport;

        public string shadowOutAnim;
        public string shadowInAnim;

        public bool ManuallyDefineRoom;

        public Vector2 roomMin;
        public Vector2 roomMax;

        private DimensionFogController m_portalController;

        private tk2dBaseSprite m_shadowSprite;

        private Shader m_cachedShader;

        private float m_timer;

        private TeleportState m_state;

        public enum ShadowSupport { None, Fade, Animate }

        private enum TeleportState { None, TeleportOut, Gone, TeleportIn, PostTeleport }

        
        // private bool ShowShadowAnimationNames() { return shadowSupport == ShadowSupport.Animate; }

        public override void Start() {
            base.Start();
            m_portalController = Object.FindObjectOfType<DimensionFogController>();
        }

        public override void Upkeep() {
            base.Upkeep();
            DecrementTimer(ref m_timer, false);
        }

        public override BehaviorResult Update() {
            base.Update();
            if (m_shadowSprite == null) { m_shadowSprite = m_aiActor.ShadowObject.GetComponent<tk2dBaseSprite>(); }
            if (!IsReady()) { return BehaviorResult.Continue; }
            if (!m_aiActor.TargetRigidbody) { return BehaviorResult.Continue; }
            State = TeleportState.TeleportOut;
            m_updateEveryFrame = true;
            return BehaviorResult.RunContinuous;
        }

        public override ContinuousBehaviorResult ContinuousUpdate() {
            base.ContinuousUpdate();
            if (State == TeleportState.TeleportOut) {
                if (shadowSupport == ShadowSupport.Fade) {
                    m_shadowSprite.color = m_shadowSprite.color.WithAlpha(1f - m_aiAnimator.CurrentClipProgress);
                }
                if (!m_aiAnimator.IsPlaying(teleportOutAnim)) { State = TeleportState.Gone; }
            } else if (State == TeleportState.Gone) {
                if (m_timer <= 0f) { State = TeleportState.TeleportIn; }
            } else if (State == TeleportState.TeleportIn) {
                if (shadowSupport == ShadowSupport.Fade) { m_shadowSprite.color = m_shadowSprite.color.WithAlpha(m_aiAnimator.CurrentClipProgress); }
                if (m_aiShooter) { m_aiShooter.ToggleGunAndHandRenderers(false, "ExpandParasiteBossPortalBehavior"); }
                if (!m_aiAnimator.IsPlaying(teleportInAnim)) { State = TeleportState.PostTeleport; }
            } else if (State == TeleportState.PostTeleport && !m_aiAnimator.IsPlaying(portalAnim)) {
                State = TeleportState.None;
                return ContinuousBehaviorResult.Finished;
            }
            return ContinuousBehaviorResult.Continue;
        }

        public override void EndContinuousUpdate() {
            base.EndContinuousUpdate();
            m_updateEveryFrame = false;
            UpdateCooldowns();
        }

        public override bool IsOverridable() { return false; }

        private TeleportState State {
            get { return m_state; }
            set { EndState(m_state); m_state = value; BeginState(m_state); }
        }

        private void BeginState(TeleportState state) {
            if (state == TeleportState.TeleportOut) {
                if (teleportRequiresTransparency) {
                    m_cachedShader = m_aiActor.renderer.material.shader;
                    m_aiActor.sprite.usesOverrideMaterial = true;
                    m_aiActor.renderer.material.shader = ShaderCache.Acquire("Brave/LitBlendUber");
                }
                m_aiAnimator.PlayUntilCancelled(teleportOutAnim, true, null, -1f, false);
                if (shadowSupport == ShadowSupport.Animate) { m_shadowSprite.spriteAnimator.PlayAndForceTime(shadowOutAnim, m_aiAnimator.CurrentClipLength); }
                m_aiActor.ClearPath();
                if (!AttackableDuringAnimation) {
                    m_aiActor.specRigidbody.CollideWithOthers = false;
                    m_aiActor.IsGone = true;
                }
                if (m_aiShooter) { m_aiShooter.ToggleGunAndHandRenderers(false, "ExpandParasiteBossPortalBehavior"); }
                if (!hasOutlinesDuringAnim) { SpriteOutlineManager.ToggleOutlineRenderers(m_aiActor.sprite, false); }
            } else if (state == TeleportState.Gone) {
                if (GoneTime <= 0f) {
                    State = TeleportState.TeleportIn;
                    return;
                }
                m_timer = GoneTime;
                m_aiActor.specRigidbody.CollideWithOthers = false;
                m_aiActor.IsGone = true;
                m_aiActor.sprite.renderer.enabled = false;
            } else if (state == TeleportState.TeleportIn) {
                DoTeleport();
                m_aiAnimator.PlayUntilFinished(teleportInAnim, true, null, -1f, false);
                if (shadowSupport == ShadowSupport.Animate) { m_shadowSprite.spriteAnimator.PlayAndForceTime(shadowInAnim, m_aiAnimator.CurrentClipLength); }
                m_shadowSprite.renderer.enabled = true;
                if (AttackableDuringAnimation) {
                    m_aiActor.specRigidbody.CollideWithOthers = true;
                    m_aiActor.IsGone = false;
                }
                m_aiActor.sprite.renderer.enabled = true;
                if (m_aiShooter) { m_aiShooter.ToggleGunAndHandRenderers(false, "ExpandParasiteBossPortalBehavior"); }
                if (hasOutlinesDuringAnim) { SpriteOutlineManager.ToggleOutlineRenderers(m_aiActor.sprite, true); }
            } else if (state == TeleportState.PostTeleport) {
                m_aiAnimator.PlayUntilFinished(portalAnim, true, null, -1f, false);
                if (m_portalController) { m_portalController.targetRadius = PortalSize; }
            }
        }

        private void EndState(TeleportState state) {
            if (state == TeleportState.TeleportOut) {
                m_shadowSprite.renderer.enabled = false;
                if (hasOutlinesDuringAnim) { SpriteOutlineManager.ToggleOutlineRenderers(m_aiActor.sprite, false); }
            } else if (state == TeleportState.TeleportIn) {
                if (teleportRequiresTransparency) {
                    m_aiActor.sprite.usesOverrideMaterial = false;
                    m_aiActor.renderer.material.shader = m_cachedShader;
                }
                if (shadowSupport == ShadowSupport.Fade) { m_shadowSprite.color = m_shadowSprite.color.WithAlpha(1f); }
                m_aiActor.specRigidbody.CollideWithOthers = true;
                m_aiActor.IsGone = false;
                if (m_aiShooter) { m_aiShooter.ToggleGunAndHandRenderers(true, "BossFinalMarinePortalBehavior"); }
                if (!hasOutlinesDuringAnim) { SpriteOutlineManager.ToggleOutlineRenderers(m_aiActor.sprite, true); }
            }
        }

        private void DoTeleport() {
            Vector2 b = m_aiActor.specRigidbody.UnitCenter - m_aiActor.transform.position.XY();
            IntVector2 pos = (roomMin + (roomMax - roomMin + Vector2.one) / 2f).ToIntVector2(VectorConversions.Floor);
            m_aiActor.transform.position = Pathfinder.GetClearanceOffset(pos, m_aiActor.Clearance) - b;
            m_aiActor.specRigidbody.Reinitialize();
        }

    }
}

