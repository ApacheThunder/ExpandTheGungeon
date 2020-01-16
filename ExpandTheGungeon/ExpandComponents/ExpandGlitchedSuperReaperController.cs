using ExpandTheGungeon.ExpandUtilities;
using System;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandGlitchedSuperReaperController : BraveBehaviour {
    
        public ExpandGlitchedSuperReaperController() {
            // ShootTimer = 4.5f;
            MinSpeed = 3f;
            MaxSpeed = 6f;
            MinSpeedDistance = 10f;
            MaxSpeedDistance = 50f;
            c_particlesPerSecond = 50;
            knockbackComponent = Vector2.zero;
            becomesBlackPhantom = false;
        }

        private static ExpandGlitchedSuperReaperController m_instance;

        public static ExpandGlitchedSuperReaperController Instance { get { return m_instance; } }

        public static bool PreventShooting;
        public BulletScriptSelector BulletScript;
        public Transform ShootPoint;
        // public float ShootTimer;
        public bool becomesBlackPhantom;
        public float MinSpeed;
        public float MaxSpeed;
        public float MinSpeedDistance;
        public float MaxSpeedDistance;

        [NonSerialized]
        public Vector2 knockbackComponent;

        private PlayerController m_currentTargetPlayer;

        private int c_particlesPerSecond;

        private void Start() {

            m_instance = this;            

            ShootPoint = aiActor.transform;
            aiAnimator.PlayUntilCancelled("idle", false, null, -1f, false);
            aiAnimator.PlayUntilFinished("intro", false, null, -1f, false);            

            specRigidbody.RegisterSpecificCollisionException(GameManager.Instance.PrimaryPlayer.specRigidbody);
            specRigidbody.PrimaryPixelCollider.Enabled = false;
            specRigidbody.CollideWithTileMap = false;

            healthHaver.SetHealthMaximum(150f);
            healthHaver.PreventAllDamage = false;
            aiActor.actorTypes = CoreActorTypes.Ghost;

            healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
            ExpandExplodeOnDeath CachedExploder = healthHaver.GetComponent<ExpandExplodeOnDeath>();
            CachedExploder.deathType = OnDeathBehavior.DeathType.Death;

            TeleportBehavior LJTeleportor = behaviorSpeculator.AttackBehaviors[1] as TeleportBehavior;
            LJTeleportor.MaxUsages = 1;

            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                specRigidbody.RegisterSpecificCollisionException(GameManager.Instance.SecondaryPlayer.specRigidbody);
            }

            if (becomesBlackPhantom) { aiActor.BecomeBlackPhantom(); }

            if (aiActor.GetComponentsInChildren<tk2dBaseSprite>(true) != null) {
                float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
                float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
                float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
                float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

                foreach (tk2dBaseSprite reaperSprite in aiActor.GetComponentsInChildren<tk2dBaseSprite>()) {
                    ExpandShaders.Instance.ApplyGlitchShader(reaperSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);
                }
            }

            m_currentTargetPlayer = GameManager.Instance.GetRandomActivePlayer();

        }

        protected override void OnDestroy() {
            base.OnDestroy();
            m_instance = null;
            // if (aiActor != null) { Destroy(aiActor.gameObject); }
        }


        private void Update() {
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.END_TIMES || GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.CHARACTER_PAST || GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER) {
                return;
            }
            if (BossKillCam.BossDeathCamRunning || GameManager.Instance.PreventPausing) { return; }
            if (TimeTubeCreditsController.IsTimeTubing) { gameObject.SetActive(false); return; }
            HandleMotion();            
            UpdateBlackPhantomParticles();
        }       

        private void UpdateBlackPhantomParticles() {
            if (GameManager.Options.ShaderQuality != GameOptions.GenericHighMedLowOption.LOW &&
                GameManager.Options.ShaderQuality != GameOptions.GenericHighMedLowOption.VERY_LOW &&
                !aiAnimator.IsPlaying("intro") && !becomesBlackPhantom)
            {
                Vector3 vector = specRigidbody.UnitBottomLeft.ToVector3ZisY(0f);
                Vector3 vector2 = specRigidbody.UnitTopRight.ToVector3ZisY(0f);
                float num = (vector2.y - vector.y) * (vector2.x - vector.x);
                float num2 = c_particlesPerSecond * num;
                int num3 = Mathf.CeilToInt(Mathf.Max(1f, num2 * BraveTime.DeltaTime));
                int num4 = num3;
                Vector3 minPosition = vector;
                Vector3 maxPosition = vector2;
                Vector3 up = Vector3.up;
                float angleVariance = 120f;
                float magnitudeVariance = 0.5f;
                float? startLifetime = new float?(UnityEngine.Random.Range(1f, 1.65f));
                GlobalSparksDoer.DoRandomParticleBurst(num4, minPosition, maxPosition, up, angleVariance, magnitudeVariance, null, startLifetime, null, GlobalSparksDoer.SparksType.BLACK_PHANTOM_SMOKE);
            }
        }

        private void HandleMotion() {
            specRigidbody.Velocity = Vector2.zero;
            if (aiAnimator.IsPlaying("intro")) { return; }
            if (m_currentTargetPlayer == null) { return; }
            if (m_currentTargetPlayer.healthHaver.IsDead || m_currentTargetPlayer.IsGhost) { m_currentTargetPlayer = GameManager.Instance.GetRandomActivePlayer(); }
            Vector2 centerPosition = m_currentTargetPlayer.CenterPosition;
            Vector2 vector = centerPosition - specRigidbody.UnitCenter;
            float magnitude = vector.magnitude;
            float d = Mathf.Lerp(MinSpeed, MaxSpeed, (magnitude - MinSpeedDistance) / (MaxSpeedDistance - MinSpeedDistance));
            specRigidbody.Velocity = vector.normalized * d;
            specRigidbody.Velocity += knockbackComponent;
        }        
    }
}

