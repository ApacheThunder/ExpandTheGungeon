using FullInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandDashBehavior : BasicAttackBehavior {

        public ExpandDashBehavior() {
            dashDirection = DashDirection.Random;
            warpDashAnimLength = true;
            gunHands = new List<GunHandController>();
        }


        public List<GunHandController> gunHands;
        public DashDirection dashDirection;

        public float quantizeDirection;
        public float dashDistance;
        public float dashTime;
        public float postDashSpeed;
        public float doubleDashChance;
        public bool avoidTarget;

        [InspectorCategory("Attack")]
        public GameObject ShootPoint;

        [InspectorCategory("Attack")]
        public BulletScriptSelector bulletScript;

        [InspectorCategory("Attack")]
        public bool fireAtDashStart;

        [InspectorCategory("Attack")]
        public bool stopOnCollision;

        [InspectorCategory("Visuals")]
        public string chargeAnim;

        [InspectorCategory("Visuals")]
        public string dashAnim;

        [InspectorCategory("Visuals")]
        public bool doDodgeDustUp;

        [InspectorCategory("Visuals")]
        public bool warpDashAnimLength;

        [InspectorCategory("Visuals")]
        public bool hideShadow;

        [InspectorCategory("Visuals")]
        public bool hideGun;

        [InspectorCategory("Visuals")]
        public bool toggleTrailRenderer;

        [InspectorCategory("Visuals")]
        public bool enableShadowTrail;

        private tk2dBaseSprite m_shadowSprite;
        private TrailRenderer m_trailRenderer;
        private AfterImageTrailController m_shadowTrail;
        private BulletScriptSource m_bulletSource;
        private bool m_cachedDoDustups;
        private bool m_cachedGrounded;
        private Vector2 m_dashDirection;
        private float m_dashTimer;
        private bool m_shouldFire;
        private bool m_lastDashWasDouble;
        private DashState m_state;

        public enum DashDirection {
            PerpendicularToTarget = 10,
            KindaTowardTarget = 20,
            TowardTarget = 25,
            Random = 30
        }

        private enum DashState {
            Idle,
            Charge,
            Dash
        }
        


        public override void Start() {
            base.Start();
            m_trailRenderer = m_aiActor.GetComponentInChildren<TrailRenderer>();
            if (toggleTrailRenderer && m_trailRenderer) { m_trailRenderer.enabled = false; }
            m_shadowTrail = m_aiActor.GetComponent<AfterImageTrailController>();
            if (bulletScript != null && !bulletScript.IsNull) {
                tk2dSpriteAnimator spriteAnimator = m_aiActor.spriteAnimator;
                spriteAnimator.AnimationEventTriggered = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>)Delegate.Combine(spriteAnimator.AnimationEventTriggered, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>(AnimationEventTriggered));
            }
            if (stopOnCollision) {
                SpeculativeRigidbody specRigidbody = m_aiActor.specRigidbody;
                specRigidbody.OnCollision = (Action<CollisionData>)Delegate.Combine(specRigidbody.OnCollision, new Action<CollisionData>(OnCollision));
            }
        }

        public override void Upkeep() {
            base.Upkeep();
            DecrementTimer(ref m_dashTimer, false);
        }

        public override BehaviorResult Update() {
            if (m_shadowSprite == null) { m_shadowSprite = m_aiActor.ShadowObject.GetComponent<tk2dBaseSprite>(); }
            BehaviorResult behaviorResult = base.Update();
            if (behaviorResult != BehaviorResult.Continue) {
                return behaviorResult;
            }
            if (!IsReady()) { return BehaviorResult.Continue; }
            if (!m_aiActor.TargetRigidbody) { return BehaviorResult.Continue; }
            m_dashDirection = GetDashDirection();
            if (!string.IsNullOrEmpty(chargeAnim)) { State = DashState.Charge; } else { State = DashState.Dash; }
            m_updateEveryFrame = true;
            return BehaviorResult.RunContinuous;
        }

        public override ContinuousBehaviorResult ContinuousUpdate() {
            base.ContinuousUpdate();
            if (State == DashState.Charge) {
                if (!m_aiAnimator.IsPlaying(chargeAnim)) { State = DashState.Dash; }
            } else if (State == DashState.Dash) {
                if (doDodgeDustUp) {
                    bool flag = m_aiActor.spriteAnimator.QueryGroundedFrame();
                    if (!m_cachedGrounded && flag && !m_aiActor.IsFalling) {
                        GameManager.Instance.Dungeon.dungeonDustups.InstantiateLandDustup(m_aiActor.specRigidbody.UnitCenter);
                        m_aiActor.DoDustUps = m_cachedDoDustups;
                    }
                    m_cachedGrounded = flag;
                }
                if (enableShadowTrail && m_dashTimer <= 0.1f) { m_shadowTrail.spawnShadows = false; }
                if (m_dashTimer <= 0f) { return ContinuousBehaviorResult.Finished; }
            } else if (State == DashState.Idle) {
                return ContinuousBehaviorResult.Finished;
            }
            return ContinuousBehaviorResult.Continue;
        }

        public override void EndContinuousUpdate() {
            base.EndContinuousUpdate();
            m_updateEveryFrame = false;
            if (postDashSpeed > 0f) {
                m_aiActor.BehaviorVelocity = (m_dashDirection.normalized * postDashSpeed);
            } else {
                m_aiActor.BehaviorOverridesVelocity = false;
                m_aiAnimator.LockFacingDirection = false;
            }
            State = DashState.Idle;
            UpdateCooldowns();
            if (!m_lastDashWasDouble) {
                if (doubleDashChance > 0f && UnityEngine.Random.value < doubleDashChance) {
                    m_cooldownTimer = 0f;
                    m_lastDashWasDouble = true;
                }
            } else {
                m_lastDashWasDouble = false;
            }
        }

        public void AnimationEventTriggered(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frame) {
            if (m_state == DashState.Dash && m_shouldFire && clip.GetFrame(frame).eventInfo == "fire") { Fire(); }
        }

        public void OnCollision(CollisionData collisionData) {
            if (m_state == DashState.Dash) {
                if (collisionData.IsTriggerCollision) { return; }
                if (collisionData.OtherRigidbody && collisionData.OtherRigidbody.projectile) { return; }
                State = DashState.Idle;
            }
        }

        private float[] GetDirections() {
            float[] array = new float[0];
            if (dashDirection == DashDirection.PerpendicularToTarget) {
                float num = (m_aiActor.TargetRigidbody.GetUnitCenter(ColliderType.Ground) - m_aiActor.specRigidbody.UnitCenter).ToAngle();
                array = new float[] { num + 90f, num - 90f };
                BraveUtility.RandomizeArray(array, 0, -1);
            } else if (dashDirection == DashDirection.KindaTowardTarget) {
                float num2 = (m_aiActor.TargetRigidbody.GetUnitCenter(ColliderType.Ground) - m_aiActor.specRigidbody.UnitCenter).ToAngle();
                array = new float[] { num2, (num2 - quantizeDirection), (num2 + quantizeDirection) };
                BraveUtility.RandomizeArray(array, 1, -1);
            } else if (dashDirection == DashDirection.TowardTarget) {
                float num3 = (m_aiActor.TargetRigidbody.GetUnitCenter(ColliderType.Ground) - m_aiActor.specRigidbody.UnitCenter).ToAngle();
                array = new float[] { num3 };
            } else if (dashDirection == DashDirection.Random) {
                if (quantizeDirection <= 0f) {
                    array = new float[16];
                    for (int i = 0; i < array.Length; i++) { array[i] = UnityEngine.Random.Range(0f, 360f); }
                } else {
                    int num4 = Mathf.RoundToInt(360f / quantizeDirection);
                    array = new float[num4];
                    for (int j = 0; j < array.Length; j++) {
                        array[j] = (j * quantizeDirection);
                    }
                    BraveUtility.RandomizeArray(array, 0, -1);
                }
            } else if (dashDirection == DashDirection.Random) {
                if (quantizeDirection <= 0f) {
                    array = new float[16];
                    for (int k = 0; k < array.Length; k++) { array[k] = UnityEngine.Random.Range(0f, 360f); }
                } else {
                    int num5 = Mathf.RoundToInt(360f / quantizeDirection);
                    array = new float[num5];
                    for (int l = 0; l < array.Length; l++) { array[l] = (l * quantizeDirection); }
                    BraveUtility.RandomizeArray(array, 0, -1);
                }
            }
            if (quantizeDirection > 0f) {
                for (int m = 0; m < array.Length; m++) { array[m] = BraveMathCollege.QuantizeFloat(array[m], quantizeDirection); }
            }
            return array;
        }

        private Vector2 GetDashDirection() {
            float[] directions = GetDirections();
            Vector2 lhs = Vector2.zero;
            Vector2 unitCenter = m_aiActor.specRigidbody.GetUnitCenter(ColliderType.Ground);
            for (int i = 0; i < directions.Length; i++) {
                bool flag = false;
                bool flag2 = false;
                Vector2 vector = BraveMathCollege.DegreesToVector(directions[i], 1f);
                RaycastResult raycastResult;
                bool flag3 = PhysicsEngine.Instance.Raycast(unitCenter, vector, dashDistance, out raycastResult, true, true, int.MaxValue, new CollisionLayer?(CollisionLayer.EnemyCollider), false, null, m_aiActor.specRigidbody);
                RaycastResult.Pool.Free(ref raycastResult);
                float num = 0.25f;
                while (num <= dashDistance && !flag && !flag3) {
                    Vector2 vector2 = unitCenter + num * vector;
                    if (!GameManager.Instance.Dungeon.CellExists(vector2)) {
                        flag = true;
                    } else if (GameManager.Instance.Dungeon.ShouldReallyFall(vector2)) {
                        flag = true;
                    }
                    num += 0.25f;
                }
                num = 0.25f;
                while (num <= dashDistance && !flag && !flag2 && !flag3) {
                    IntVector2 intVector = (unitCenter + num * vector).ToIntVector2(VectorConversions.Floor);
                    if (!GameManager.Instance.Dungeon.CellExists(intVector)) {
                        flag2 = true;
                    } else if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(intVector) && GameManager.Instance.Dungeon.data[intVector].isExitCell) {
                        flag2 = true;
                    }
                    num += 0.25f;
                }
                if (avoidTarget && m_behaviorSpeculator.TargetRigidbody && !flag && !flag2 && !flag3) {
                    Vector2 unitCenter2 = m_aiActor.specRigidbody.GetUnitCenter(ColliderType.HitBox);
                    Vector2 vector3 = m_behaviorSpeculator.TargetRigidbody.GetUnitCenter(ColliderType.HitBox) - unitCenter2;
                    float num2 = dashDistance + 2f;
                    if (vector3.magnitude < num2 && BraveMathCollege.AbsAngleBetween(vector3.ToAngle(), directions[i]) < 80f) { flag3 = true; }
                    if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER && !flag3) {
                        PlayerController playerController = m_aiActor.PlayerTarget as PlayerController;
                        if (playerController) {
                            PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(playerController);
                            if (otherPlayer && otherPlayer.healthHaver.IsAlive) {
                                vector3 = otherPlayer.specRigidbody.GetUnitCenter(ColliderType.HitBox) - unitCenter2;
                                if (vector3.magnitude < num2 && BraveMathCollege.AbsAngleBetween(vector3.ToAngle(), directions[i]) < 80f) {
                                    flag3 = true;
                                }
                            }
                        }
                    }
                }
                if (!flag3 && !flag && !flag2) { lhs = vector; break; }
            }
            if (lhs != Vector2.zero) { return lhs.normalized; }
            if (directions.Length > 0) { return BraveMathCollege.DegreesToVector(directions[directions.Length - 1], 1f); }
            float num3 = UnityEngine.Random.Range(0f, 360f);
            if (quantizeDirection > 0f) { BraveMathCollege.QuantizeFloat(num3, quantizeDirection); }
            return BraveMathCollege.DegreesToVector(num3, 1f);
        }

        private void Fire() {
            if (!m_aiActor.bulletBank) { m_shouldFire = false; return; }
            if (!m_bulletSource) { m_bulletSource = ShootPoint.GetOrAddComponent<BulletScriptSource>(); }
            m_bulletSource.BulletManager = m_aiActor.bulletBank;
            m_bulletSource.BulletScript = bulletScript;
            m_bulletSource.Initialize();
            m_shouldFire = false;
        }

        private DashState State {
            get { return m_state; }
            set {
                if (m_state != value) {
                    EndState(m_state);
                    m_state = value;
                    BeginState(m_state);
                }
            }
        }

        private void BeginState(DashState state) {
            if (state == DashState.Charge) {
                m_aiAnimator.LockFacingDirection = true;
                m_aiAnimator.FacingDirection = m_dashDirection.ToAngle();
                m_aiAnimator.PlayUntilFinished(chargeAnim, true, null, -1f, false);
                m_aiActor.ClearPath();
                if (!m_aiActor.BehaviorOverridesVelocity) {
                    m_aiActor.BehaviorOverridesVelocity = true;
                    m_aiActor.BehaviorVelocity = Vector2.zero;
                }
            } else if (state == DashState.Dash) {
                if (bulletScript != null && !bulletScript.IsNull) { m_shouldFire = true; }
                m_aiAnimator.LockFacingDirection = true;
                m_aiAnimator.FacingDirection = m_dashDirection.ToAngle();
                if (!string.IsNullOrEmpty(dashAnim)) {
                    if (warpDashAnimLength) {
                        AIAnimator aiAnimator = m_aiAnimator;
                        string name = dashAnim;
                        bool suppressHitStates = true;
                        float warpClipDuration = dashTime;
                        aiAnimator.PlayUntilFinished(name, suppressHitStates, null, warpClipDuration, false);
                    } else {
                        m_aiAnimator.PlayUntilFinished(dashAnim, true, null, -1f, false);
                    }
                }
                if (doDodgeDustUp) {
                    m_cachedDoDustups = m_aiActor.DoDustUps;
                    m_aiActor.DoDustUps = false;
                    GameManager.Instance.Dungeon.dungeonDustups.InstantiateDodgeDustup(m_dashDirection, m_aiActor.specRigidbody.UnitBottomCenter);
                    m_cachedGrounded = true;
                }
                if (hideShadow && m_shadowSprite) { m_shadowSprite.renderer.enabled = false; }
                if (hideGun && m_aiShooter) { m_aiShooter.ToggleGunAndHandRenderers(false, "ExpandDashBehavior"); }
                if (hideGun && gunHands != null && gunHands.Count > 0) {
                    foreach (GunHandController gunHand in gunHands) {
                        if (gunHand.Gun) { gunHand.Gun.sprite.renderer.enabled = false; }
                        if (gunHand.handObject) { gunHand.handObject.sprite.renderer.enabled = false; }
                    }
                }
                if (toggleTrailRenderer && m_trailRenderer) { m_trailRenderer.enabled = true; }
                if (enableShadowTrail) {
                    m_shadowTrail.spawnShadows = true;
                    AkSoundEngine.PostEvent("Play_ENM_highpriest_dash_01", GameManager.Instance.PrimaryPlayer.gameObject);
                }
                float d = dashDistance / dashTime;
                m_dashTimer = dashTime;
                m_aiActor.ClearPath();
                m_aiActor.BehaviorOverridesVelocity = true;
                m_aiActor.BehaviorVelocity = d * m_dashDirection;
                if (bulletScript != null && !bulletScript.IsNull && fireAtDashStart) { Fire(); }
            }
        }

        private void EndState(DashState state) {
            if (state == DashState.Dash) {
                if (!string.IsNullOrEmpty(dashAnim)) { m_aiAnimator.EndAnimationIf(dashAnim); }
                if (bulletScript != null && !bulletScript.IsNull && m_shouldFire) { Fire(); }
                if (doDodgeDustUp) { m_aiActor.DoDustUps = m_cachedDoDustups; }
                if (hideShadow && m_shadowSprite) { m_shadowSprite.renderer.enabled = true; }
                if (hideGun && m_aiShooter) { m_aiShooter.ToggleGunAndHandRenderers(true, "ExpandDashBehavior"); }
                if (hideGun && gunHands != null && gunHands.Count > 0) {
                    foreach (GunHandController gunHand in gunHands) {
                        if (gunHand.Gun) { gunHand.Gun.sprite.renderer.enabled = true; }
                        if (gunHand.handObject) { gunHand.handObject.sprite.renderer.enabled = true; }
                    }
                }
                if (toggleTrailRenderer && m_trailRenderer) { m_trailRenderer.enabled = false; }
                if (enableShadowTrail) { m_shadowTrail.spawnShadows = false; }
                if (postDashSpeed <= 0f) { m_aiActor.BehaviorVelocity = Vector2.zero; }
                if (m_bulletSource != null) { m_bulletSource.ForceStop(); }
            }
        }
    }
}

