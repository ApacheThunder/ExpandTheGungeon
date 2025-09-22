using System;
using Dungeonator;
using FullInspector;
using Pathfinding;
using UnityEngine;
using System.Collections;

namespace ExpandTheGungeon.ExpandComponents {
    
    public class ExpandTeleportBehavior : BasicAttackBehavior {

        public bool AttackableDuringAnimation;

        public bool AvoidWalls;

        [NonSerialized]
        public bool IsDoingManualTeleport = false;

        public bool StayOnScreen = true;

        public float MinDistanceFromPlayer = 4f;

        public float MaxDistanceFromPlayer = -1f;

        public float GoneTime = 1f;

        [InspectorCategory("Conditions")]
        public bool OnlyTeleportIfPlayerUnreachable;

        [InspectorCategory("Attack")]
        public BulletScriptSelector teleportOutBulletScript;

        [InspectorCategory("Attack")]
        public BulletScriptSelector teleportInBulletScript;

        [InspectorCategory("Attack")]
        public AttackBehaviorBase goneAttackBehavior;

        [InspectorCategory("Attack")]
        public bool AllowCrossRoomTeleportation;

        [InspectorCategory("Visuals")]
        public string teleportOutAnim = "teleport_out";

        [InspectorCategory("Visuals")]
        public string teleportInAnim = "teleport_in";

        [InspectorCategory("Visuals")]
        public bool teleportRequiresTransparency;

        [InspectorCategory("Visuals")]
        public bool hasOutlinesDuringAnim = true;

        [InspectorCategory("Visuals")]
        public ShadowSupport shadowSupport;

        [InspectorCategory("Visuals")]
        [InspectorShowIf("ShowShadowAnimationNames")]
        public string shadowOutAnim;

        [InspectorShowIf("ShowShadowAnimationNames")]
        [InspectorCategory("Visuals")]
        public string shadowInAnim;

        public bool ManuallyDefineRoom;

        [InspectorShowIf("ManuallyDefineRoom")]
        [InspectorIndent]
        public Vector2 roomMin;

        [InspectorShowIf("ManuallyDefineRoom")]
        [InspectorIndent]
        public Vector2 roomMax;

        public enum ShadowSupport { None, Fade, Animate }
        public enum TeleportState { None, TeleportOut, Gone, GoneBehavior, TeleportIn }

        private TeleportState m_state;
        private tk2dBaseSprite m_shadowSprite;
        private Shader m_cachedShader;

        private float m_timer;
        private bool m_shouldFire;

        public static ShadowSupport ConvertTeleportTypeEnum(TeleportBehavior.ShadowSupport sourceEnum) {
            switch (sourceEnum) {
                case TeleportBehavior.ShadowSupport.Animate:
                    return ShadowSupport.Animate;
                case TeleportBehavior.ShadowSupport.Fade:
                    return ShadowSupport.Fade;
                case TeleportBehavior.ShadowSupport.None:
                    return ShadowSupport.None;
                default:
                    return ShadowSupport.None;
            }
        }


        private bool ShowShadowAnimationNames() {
            return shadowSupport == ShadowSupport.Animate;
        }

        public override void Start() {
            base.Start();
            tk2dSpriteAnimator spriteAnimator = m_aiActor.spriteAnimator;
            spriteAnimator.AnimationEventTriggered = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>)Delegate.Combine(spriteAnimator.AnimationEventTriggered, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>(AnimationEventTriggered));
        }

        public override void Upkeep() {
            base.Upkeep();
            DecrementTimer(ref m_timer, false);
            if (goneAttackBehavior != null)goneAttackBehavior.Upkeep();
        }

        public override bool IsReady() {
            if (IsDoingManualTeleport) return false;
            try { 
                if (OnlyTeleportIfPlayerUnreachable) {
                    bool playerInRoom = m_aiActor.GetAbsoluteParentRoom() == GameManager.Instance.BestActivePlayer.CurrentRoom;
                    if (playerInRoom && m_aiActor.Path != null && m_aiActor.Path.WillReachFinalGoal) return false;
                }
                return base.IsReady();
            } catch (Exception) {
                return false;
            }
        }

        public override BehaviorResult Update() {
            base.Update();
            if (m_shadowSprite == null)m_shadowSprite = m_aiActor.ShadowObject.GetComponent<tk2dBaseSprite>();
            if (!IsReady())return BehaviorResult.Continue;
            if (!m_aiActor.TargetRigidbody) return BehaviorResult.Continue;
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
                if (!m_aiAnimator.IsPlaying(teleportOutAnim))State = TeleportState.Gone;
            } else if (State == TeleportState.Gone) {
                if (m_timer <= 0f)State = TeleportState.GoneBehavior;
            } else if (State == TeleportState.GoneBehavior) {
                if (goneAttackBehavior.ContinuousUpdate() == ContinuousBehaviorResult.Finished)State = TeleportState.TeleportIn;
            } else if (State == TeleportState.TeleportIn) {
                if (shadowSupport == ShadowSupport.Fade) {
                    m_shadowSprite.color = m_shadowSprite.color.WithAlpha(m_aiAnimator.CurrentClipProgress);
                }
                if (m_aiShooter) m_aiShooter.ToggleGunAndHandRenderers(false, "ExpandTeleportBehavior");
                if (!m_aiAnimator.IsPlaying(teleportInAnim)) {
                    State = TeleportState.None;
                    return ContinuousBehaviorResult.Finished;
                }
            }
            return ContinuousBehaviorResult.Continue;
        }

        public override void EndContinuousUpdate() {
            base.EndContinuousUpdate();
            if (teleportRequiresTransparency && m_cachedShader) {
                m_aiActor.sprite.usesOverrideMaterial = false;
                m_aiActor.renderer.material.shader = m_cachedShader;
                m_cachedShader = null;
            }
            m_aiActor.sprite.renderer.enabled = true;
            if (m_aiActor.knockbackDoer)m_aiActor.knockbackDoer.SetImmobile(false, "teleport");
            m_aiActor.specRigidbody.CollideWithOthers = true;
            m_aiActor.IsGone = false;
            if (m_aiShooter)m_aiShooter.ToggleGunAndHandRenderers(true, "ExpandTeleportBehavior");
            if (!hasOutlinesDuringAnim)SpriteOutlineManager.ToggleOutlineRenderers(m_aiActor.sprite, true);
            if (goneAttackBehavior != null && State == TeleportState.GoneBehavior)goneAttackBehavior.EndContinuousUpdate();
            m_aiAnimator.EndAnimationIf(teleportOutAnim);
            m_aiAnimator.EndAnimationIf(teleportInAnim);
            if (shadowSupport == ShadowSupport.Fade) {
                m_shadowSprite.color = m_shadowSprite.color.WithAlpha(1f);
            } else if (shadowSupport == ShadowSupport.Animate) {
                tk2dSpriteAnimationClip clipByName = m_shadowSprite.spriteAnimator.GetClipByName(shadowInAnim);
                m_shadowSprite.spriteAnimator.Play(clipByName, clipByName.frames.Length - 1, clipByName.fps, false);
            }
            m_state = TeleportState.None;
            m_updateEveryFrame = false;
            UpdateCooldowns();
        }

        public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter) {
            base.Init(gameObject, aiActor, aiShooter);
            if (goneAttackBehavior != null)goneAttackBehavior.Init(gameObject, aiActor, aiShooter);
        }

        public override void SetDeltaTime(float deltaTime) {
            base.SetDeltaTime(deltaTime);
            if (goneAttackBehavior != null)goneAttackBehavior.SetDeltaTime(deltaTime);
        }

        public override bool UpdateEveryFrame() {
            if (goneAttackBehavior != null && m_state == TeleportState.GoneBehavior)return goneAttackBehavior.UpdateEveryFrame();
            return base.UpdateEveryFrame();
        }

        public void AnimationEventTriggered(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frame) {
            if (m_shouldFire && clip.GetFrame(frame).eventInfo == "fire") {
                if (State == TeleportState.TeleportIn) {
                    SpawnManager.SpawnBulletScript(m_aiActor, teleportInBulletScript, null, null, false, null);
                } else if (State == TeleportState.TeleportOut) {
                    SpawnManager.SpawnBulletScript(m_aiActor, teleportOutBulletScript, null, null, false, null);
                }
                m_shouldFire = false;
            } else if (State == TeleportState.TeleportOut && clip.GetFrame(frame).eventInfo == "teleport_collider_off") {
                m_aiActor.specRigidbody.CollideWithOthers = false;
                m_aiActor.IsGone = true;
            }
        }

        public TeleportState State {
            get { return m_state; }
            set {
                EndState(m_state);
                m_state = value;
                BeginState(m_state);
            }
        }

        public void DoManualTeleport(TeleportState state) {
            if (!IsDoingManualTeleport) {
                IsDoingManualTeleport = true;
                GameManager.Instance.StartCoroutine(HandleManualTeleport());
            }
        }

        public IEnumerator HandleManualTeleport() {
            if (m_state != TeleportState.None) {
                EndState(m_state);
                while (m_state != TeleportState.None) yield return null;
            }
            State = TeleportState.TeleportOut;
            float m_timer2 = 2;
            while (m_timer2 > 0) {
                m_timer2 -= BraveTime.DeltaTime;
                yield return null;
            }
            State = TeleportState.Gone;
            while (m_timer > 0) yield return null;
            State = TeleportState.Gone;
            while (State != TeleportState.TeleportIn) yield return null;
            while (!m_aiAnimator.IsPlaying(teleportInAnim)) yield return null;
            EndState(TeleportState.TeleportIn);
            IsDoingManualTeleport = false;
            yield break;
        }

        private void BeginState(TeleportState state) {
            try { 
                if (state == TeleportState.TeleportOut) {
                    if (teleportOutBulletScript != null && !teleportOutBulletScript.IsNull)m_shouldFire = true;
                    if (teleportRequiresTransparency) {
                        m_cachedShader = m_aiActor.renderer.material.shader;
                        m_aiActor.sprite.usesOverrideMaterial = true;
                        m_aiActor.renderer.material.shader = ShaderCache.Acquire("Brave/LitBlendUber");
                    }
                    m_aiAnimator.PlayUntilCancelled(teleportOutAnim, true, null, -1f, false);
                    if (shadowSupport == ShadowSupport.Animate)m_shadowSprite.spriteAnimator.PlayAndForceTime(shadowOutAnim, m_aiAnimator.CurrentClipLength);
                    if (m_aiActor.knockbackDoer)m_aiActor.knockbackDoer.SetImmobile(true, "teleport");
                    m_aiActor.ClearPath();
                    if (!AttackableDuringAnimation) {
                        m_aiActor.specRigidbody.CollideWithOthers = false;
                        m_aiActor.IsGone = true;
                    }
                    if (m_aiShooter)m_aiShooter.ToggleGunAndHandRenderers(false, "ExpandTeleportBehavior");
                    if (!hasOutlinesDuringAnim)SpriteOutlineManager.ToggleOutlineRenderers(m_aiActor.sprite, false);
                } else if (state == TeleportState.Gone) {
                    if (GoneTime <= 0f) {
                        State = TeleportState.GoneBehavior;
                        return;
                    }
                    m_timer = GoneTime;
                    m_aiActor.specRigidbody.CollideWithOthers = false;
                    m_aiActor.IsGone = true;
                    m_aiActor.sprite.renderer.enabled = false;
                } else if (State == TeleportState.GoneBehavior) {
                    if (goneAttackBehavior == null) {
                        State = TeleportState.TeleportIn;
                        return;
                    }
                    BehaviorResult behaviorResult = goneAttackBehavior.Update();
                    if (behaviorResult != BehaviorResult.RunContinuous && behaviorResult != BehaviorResult.RunContinuousInClass)State = TeleportState.TeleportIn;
                } else if (state == TeleportState.TeleportIn) {
                    if (teleportInBulletScript != null && !teleportInBulletScript.IsNull)m_shouldFire = true;
                    DoTeleport();
                    m_aiAnimator.PlayUntilFinished(teleportInAnim, true, null, -1f, false);
                    if (shadowSupport == ShadowSupport.Animate)m_shadowSprite.spriteAnimator.PlayAndForceTime(shadowInAnim, m_aiAnimator.CurrentClipLength);
                    m_shadowSprite.renderer.enabled = true;
                    if (AttackableDuringAnimation) {
                        m_aiActor.specRigidbody.CollideWithOthers = true;
                        m_aiActor.IsGone = false;
                    }
                    m_aiActor.sprite.renderer.enabled = true;
                    if (m_aiShooter)m_aiShooter.ToggleGunAndHandRenderers(false, "ExpandTeleportBehavior");
                    if (hasOutlinesDuringAnim)SpriteOutlineManager.ToggleOutlineRenderers(m_aiActor.sprite, true);
                }
            } catch (Exception) {

            }
        }

        private void EndState(TeleportState state) {
            try { 
                if (state == TeleportState.TeleportOut) {
                    m_shadowSprite.renderer.enabled = false;
                    if (hasOutlinesDuringAnim)SpriteOutlineManager.ToggleOutlineRenderers(m_aiActor.sprite, false);
                    if (teleportOutBulletScript != null && !teleportOutBulletScript.IsNull && m_shouldFire) {
                        SpawnManager.SpawnBulletScript(m_aiActor, teleportOutBulletScript, null, null, false, null);
                        m_shouldFire = false;
                    }
                } else if (state == TeleportState.TeleportIn) {
                    if (teleportRequiresTransparency && m_cachedShader) {
                        m_aiActor.sprite.usesOverrideMaterial = false;
                        m_aiActor.renderer.material.shader = m_cachedShader;
                        m_cachedShader = null;
                    }
                    if (shadowSupport == ShadowSupport.Fade)m_shadowSprite.color = m_shadowSprite.color.WithAlpha(1f);
                    if (m_aiActor.knockbackDoer)m_aiActor.knockbackDoer.SetImmobile(false, "teleport");
                    m_aiActor.specRigidbody.CollideWithOthers = true;
                    m_aiActor.IsGone = false;
                    if (m_aiShooter)m_aiShooter.ToggleGunAndHandRenderers(true, "ExpandTeleportBehavior");
                    if (teleportInBulletScript != null && !teleportInBulletScript.IsNull && m_shouldFire) {
                        SpawnManager.SpawnBulletScript(m_aiActor, teleportInBulletScript, null, null, false, null);
                        m_shouldFire = false;
                    }
                    if (!hasOutlinesDuringAnim)SpriteOutlineManager.ToggleOutlineRenderers(m_aiActor.sprite, true);
                }
            } catch (Exception) { }
        }

        private void DoTeleport() {
            float minDistanceFromPlayerSquared = MinDistanceFromPlayer * MinDistanceFromPlayer;
            float maxDistanceFromPlayerSquared = MaxDistanceFromPlayer * MaxDistanceFromPlayer;
            Vector2 playerLowerLeft = Vector2.zero;
            Vector2 playerUpperRight = Vector2.zero;
            bool hasOtherPlayer = false;
            Vector2 otherPlayerLowerLeft = Vector2.zero;
            Vector2 otherPlayerUpperRight = Vector2.zero;
            bool hasDistChecks = (MinDistanceFromPlayer > 0f || MaxDistanceFromPlayer > 0f) && m_aiActor.TargetRigidbody;
            if (hasDistChecks) {
                playerLowerLeft = m_aiActor.TargetRigidbody.HitboxPixelCollider.UnitBottomLeft;
                playerUpperRight = m_aiActor.TargetRigidbody.HitboxPixelCollider.UnitTopRight;
                PlayerController playerController = m_behaviorSpeculator.PlayerTarget as PlayerController;
                if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER && playerController)
                {
                    PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(playerController);
                    if (otherPlayer && otherPlayer.healthHaver.IsAlive)
                    {
                        hasOtherPlayer = true;
                        otherPlayerLowerLeft = otherPlayer.specRigidbody.HitboxPixelCollider.UnitBottomLeft;
                        otherPlayerUpperRight = otherPlayer.specRigidbody.HitboxPixelCollider.UnitTopRight;
                    }
                }
            }
            IntVector2 bottomLeft = IntVector2.Zero;
            IntVector2 topRight = IntVector2.Zero;
            if (StayOnScreen) {
                bottomLeft = (BraveUtility.ViewportToWorldpoint(new Vector2(0f, 0f), ViewportType.Gameplay)).XY().ToIntVector2(VectorConversions.Ceil);
                topRight = (BraveUtility.ViewportToWorldpoint(new Vector2(1f, 1f), ViewportType.Gameplay)).XY().ToIntVector2(VectorConversions.Floor) - IntVector2.One;
            }
            CellValidator cellValidator = delegate (IntVector2 c) {
                for (int i = 0; i < m_aiActor.Clearance.x; i++) {
                    int num = c.x + i;
                    for (int j = 0; j < m_aiActor.Clearance.y; j++) {
                        int num2 = c.y + j;
                        if (GameManager.Instance.Dungeon.data.isTopWall(num, num2))return false;
                        if (ManuallyDefineRoom && (num < roomMin.x || num > roomMax.x || num2 < roomMin.y || num2 > roomMax.y))return false;
                    }
                }
                if (hasDistChecks) {
                    PixelCollider hitboxPixelCollider = m_aiActor.specRigidbody.HitboxPixelCollider;
                    Vector2 vector = new Vector2(c.x + 0.5f * (m_aiActor.Clearance.x - hitboxPixelCollider.UnitWidth), c.y);
                    Vector2 aMax = vector + hitboxPixelCollider.UnitDimensions;
                    if (MinDistanceFromPlayer > 0f) {
                        if (BraveMathCollege.AABBDistanceSquared(vector, aMax, playerLowerLeft, playerUpperRight) < minDistanceFromPlayerSquared)return false;
                        if (hasOtherPlayer && BraveMathCollege.AABBDistanceSquared(vector, aMax, otherPlayerLowerLeft, otherPlayerUpperRight) < minDistanceFromPlayerSquared)return false;
                    }
                    if (MaxDistanceFromPlayer > 0f) {
                        if (BraveMathCollege.AABBDistanceSquared(vector, aMax, playerLowerLeft, playerUpperRight) > maxDistanceFromPlayerSquared)return false;
                        
                        if (hasOtherPlayer && BraveMathCollege.AABBDistanceSquared(vector, aMax, otherPlayerLowerLeft, otherPlayerUpperRight) > maxDistanceFromPlayerSquared)return false;
                        
                    }
                }
                if (StayOnScreen && (c.x < bottomLeft.x || c.y < bottomLeft.y || c.x + m_aiActor.Clearance.x - 1 > topRight.x || c.y + m_aiActor.Clearance.y - 1 > topRight.y))return false;
                if (AvoidWalls) {
                    int k = -1;
                    int l;
                    for (l = -1; l < m_aiActor.Clearance.y + 1; l++) {
                        if (GameManager.Instance.Dungeon.data.isWall(c.x + k, c.y + l))return false;
                    }
                    k = m_aiActor.Clearance.x;
                    for (l = -1; l < m_aiActor.Clearance.y + 1; l++) {
                        if (GameManager.Instance.Dungeon.data.isWall(c.x + k, c.y + l)) return false;
                    }
                    l = -1;
                    for (k = -1; k < m_aiActor.Clearance.x + 1; k++) {
                        if (GameManager.Instance.Dungeon.data.isWall(c.x + k, c.y + l))return false;
                    }
                    l = m_aiActor.Clearance.y;
                    for (k = -1; k < m_aiActor.Clearance.x + 1; k++) {
                        if (GameManager.Instance.Dungeon.data.isWall(c.x + k, c.y + l))return false;
                    }
                }
                return true;
            };
            Vector2 b = m_aiActor.specRigidbody.UnitBottomCenter - m_aiActor.transform.position.XY();
            // IntVector2? intVector = null;
            IntVector2? randomAvailableCell;
            if (AllowCrossRoomTeleportation) {
                randomAvailableCell = GameManager.Instance.BestActivePlayer.CurrentRoom.GetRandomAvailableCell(new IntVector2?(m_aiActor.Clearance), new CellTypes?(m_aiActor.PathableTiles), false, cellValidator);
            } else {
                randomAvailableCell = m_aiActor.ParentRoom.GetRandomAvailableCell(new IntVector2?(m_aiActor.Clearance), new CellTypes?(m_aiActor.PathableTiles), false, cellValidator);
            }
            if (randomAvailableCell != null) {
                m_aiActor.transform.position = Pathfinder.GetClearanceOffset(randomAvailableCell.Value, m_aiActor.Clearance).WithY(randomAvailableCell.Value.y) - b;
                m_aiActor.specRigidbody.Reinitialize();
            } else {
                Debug.LogWarning("TELEPORT FAILED!", m_aiActor);
            }
        }
    }
}

