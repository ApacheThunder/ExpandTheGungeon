using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.SpriteAPI;

namespace ExpandTheGungeon.ExpandComponents { 
        
    public class ExpandForgeHammerComponent : DungeonPlaceableBehaviour, IPlaceConfigurable {


        public static GameObject BuildHammerPrefab(AssetBundle expandAssets, string HammerAssetName, string HammerBulletName, bool SkipBulletBuild, bool IsFriendlyBullet, bool isHattyHammer, out GameObject HammerBullet) {
            GameObject newHammer = expandAssets.LoadAsset<GameObject>(HammerAssetName);
            if (SkipBulletBuild) {
                HammerBullet = null;
            } else {
                HammerBullet = expandAssets.LoadAsset<GameObject>(HammerBulletName);
            }
                        
            tk2dSprite m_NewHammerSprite = newHammer.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(m_NewHammerSprite, ExpandObjectDatabase.ForgeHammer.GetComponent<tk2dSprite>());
            if (isHattyHammer) m_NewHammerSprite.Collection = ExpandPrefabs.EXHattyHammerCollection.GetComponent<tk2dSpriteCollectionData>();
            
            tk2dSpriteAnimation m_SourceHammerAnimationLibrary = ExpandObjectDatabase.ForgeHammer.GetComponent<tk2dSpriteAnimator>().Library;
            tk2dSpriteAnimation m_HammerAnimationLibrary = newHammer.AddComponent<tk2dSpriteAnimation>();

            List<tk2dSpriteAnimationClip> m_Clips = new List<tk2dSpriteAnimationClip>();

            foreach (tk2dSpriteAnimationClip clip in m_SourceHammerAnimationLibrary.clips) {
                if (!string.IsNullOrEmpty(clip.name)) {
                    if (clip.name.ToLower().StartsWith("hammer_right_out")) {
                        m_Clips.Add(ExpandUtility.DuplicateAnimationClip(clip));
                    } else if (clip.name.ToLower().StartsWith("hammer_left_out")) {
                        m_Clips.Add(ExpandUtility.DuplicateAnimationClip(clip));
                    } else if (clip.name.ToLower().StartsWith("hammer_right_slam")) {
                        m_Clips.Add(ExpandUtility.DuplicateAnimationClip(clip));
                    } else if (clip.name.ToLower().StartsWith("hammer_left_slam")) {
                        m_Clips.Add(ExpandUtility.DuplicateAnimationClip(clip));
                    }
                }
            }

            if (isHattyHammer) {
                foreach (tk2dSpriteAnimationClip clip in m_Clips) {
                    foreach (tk2dSpriteAnimationFrame frame in clip.frames) {
                        frame.spriteCollection = ExpandPrefabs.EXHattyHammerCollection.GetComponent<tk2dSpriteCollectionData>();
                    }
                }
            }

            if (m_Clips.Count > 0)m_HammerAnimationLibrary.clips = m_Clips.ToArray();

            ExpandUtility.GenerateSpriteAnimator(newHammer, m_HammerAnimationLibrary, 0, 0, false, false, false, false, false, false, 0, 0, false, false, false);

            ExpandUtility.GenerateOrAddToRigidBody(newHammer, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, true, true, false, false, false, false, false, true, new IntVector2(30, 24), new IntVector2(16, 24));
            ExpandUtility.GenerateOrAddToRigidBody(newHammer, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, true, true, false, false, false, false, false, true, new IntVector2(30, 8), new IntVector2(16, 16));

            GoopDefinition m_HammerGoopDefinition = ExpandUtility.DuplicateGoop(ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().GoopToDo, "EXNapalmGoopQuickIgnite");

            ExpandForgeHammerComponent EXFriendlyHammerController = newHammer.AddComponent<ExpandForgeHammerComponent>();
            EXFriendlyHammerController.GoopToDo = m_HammerGoopDefinition;

            AIBulletBank m_HammerBulletBank = newHammer.AddComponent<AIBulletBank>();
            m_HammerBulletBank.useDefaultBulletIfMissing = true;
            m_HammerBulletBank.transforms = new List<Transform>(0);
            m_HammerBulletBank.Bullets = new List<AIBulletBank.Entry>() {
                new AIBulletBank.Entry() {
                    Name = "default",
                    OverrideProjectile = false,
                    PlayAudio = false,
                    AudioSwitch = string.Empty,
                    AudioEvent = string.Empty,
                    AudioLimitOncePerFrame = false,
                    AudioLimitOncePerAttack = false,
                    MuzzleLimitOncePerFrame = false,
                    MuzzleInheritsTransformDirection = false,
                    SpawnShells = false,
                    ShellForce = 1.75f,
                    ShellForceVariance = 0.75f,
                    DontRotateShell = false,
                    ShellGroundOffset = 0,
                    ShellsLimitOncePerFrame = false,
                    rampBullets = false,
                    rampStartHeight = 0,
                    rampTime = 0,
                    conditionalMinDegFromNorth = 0,
                    forceCanHitEnemies = true,
                    suppressHitEffectsIfOffscreen = false,
                    preloadCount = 0,
                    ProjectileData = new ProjectileData() {
                        damage = 0,
                        speed = 0,
                        range = 0,
                        damping = 0,
                        UsesCustomAccelerationCurve = false,
                        AccelerationCurve = new AnimationCurve(),
                        CustomAccelerationCurveDuration = 0,
                        onDestroyBulletScript = new BulletScriptSelector() { scriptTypeName = string.Empty }
                    },
                    MuzzleFlashEffects = new VFXPool() { type = VFXPoolType.None, effects = new VFXComplex[0] }
                }
            };

            if (!SkipBulletBuild) {
                Projectile m_BulletProjectile = HammerBullet.AddComponent<Projectile>();
                SpeculativeRigidbody m_BulletRigidBody = HammerBullet.AddComponent<SpeculativeRigidbody>();
                tk2dSprite m_BulletSprite = HammerBullet.transform.Find("Sprite").gameObject.AddComponent<tk2dSprite>();
                tk2dSpriteAnimator m_BulletAnimator = m_BulletSprite.gameObject.AddComponent<tk2dSpriteAnimator>();
                ExpandUtility.DuplicateComponent(m_BulletProjectile, ExpandObjectDatabase.ForgeHammer.GetComponent<AIBulletBank>().Bullets[0].BulletObject.GetComponent<Projectile>());
                ExpandUtility.DuplicateRigidBody(m_BulletRigidBody, ExpandObjectDatabase.ForgeHammer.GetComponent<AIBulletBank>().Bullets[0].BulletObject.GetComponent<SpeculativeRigidbody>());
                ExpandUtility.DuplicateSprite(m_BulletSprite, ExpandObjectDatabase.ForgeHammer.GetComponent<AIBulletBank>().Bullets[0].BulletObject.transform.Find("Sprite").gameObject.GetComponent<tk2dSprite>());
                ExpandUtility.DuplicateSpriteAnimator(m_BulletAnimator, ExpandObjectDatabase.ForgeHammer.GetComponent<AIBulletBank>().Bullets[0].BulletObject.transform.Find("Sprite").gameObject.GetComponent<tk2dSpriteAnimator>());
                if (IsFriendlyBullet) {
                    m_BulletProjectile.collidesWithPlayer = false;
                    m_BulletProjectile.collidesWithEnemies = true;
                    m_BulletProjectile.TreatedAsNonProjectileForChallenge = true;
                }
                m_HammerBulletBank.Bullets[0].BulletObject = HammerBullet;
            }

            GameObject m_TargetObject = newHammer.transform.Find("Target").gameObject;
            
            tk2dSprite m_TargetSprite = m_TargetObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(m_TargetSprite, ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().TargetAnimator.gameObject.GetComponent<tk2dSprite>());
            tk2dSpriteAnimator m_TargetAnimator = ExpandUtility.DuplicateSpriteAnimator(m_TargetObject, ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().TargetAnimator);

            GameObject m_HitEffectObject = newHammer.transform.Find("HitEffect").gameObject;
            tk2dSprite m_HitEffectSprite = m_HitEffectObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(m_HitEffectSprite, ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().HitEffectAnimator.gameObject.GetComponent<tk2dSprite>());
            tk2dSpriteAnimator m_HitEffectAnimator = ExpandUtility.DuplicateSpriteAnimator(m_HitEffectObject, ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().HitEffectAnimator);

            GameObject m_ShadowObject = newHammer.transform.Find("Shadow").gameObject;
            tk2dSprite m_ShadowSprite = m_ShadowObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(m_ShadowSprite, ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().ShadowAnimator.gameObject.GetComponent<tk2dSprite>());
            tk2dSpriteAnimator m_ShadowAnimator = ExpandUtility.DuplicateSpriteAnimator(m_ShadowObject, ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().ShadowAnimator);
            m_ShadowAnimator.playAutomatically = false;
            m_ShadowAnimator.DefaultClipId = 28;

            EXFriendlyHammerController.TargetAnimator = m_TargetAnimator;
            EXFriendlyHammerController.HitEffectAnimator = m_HitEffectAnimator;
            EXFriendlyHammerController.ShadowAnimator = m_ShadowAnimator;
            EXFriendlyHammerController.ShootPoint = newHammer.transform.Find("ShootPoint");

            return newHammer;
        }


        /*public static void BuildPrefab() {

            ExpandPrefabs.EXFriendlyForgeHammer = new GameObject("Friendly_Forge_Hammer") { layer = 22 };
            
            GameObject m_HitEffectObject = Instantiate(ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().HitEffectAnimator.gameObject);
            GameObject m_TargetAnimatorObject = Instantiate(ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().TargetAnimator.gameObject);
            GameObject m_ShadowObject = Instantiate(ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().ShadowAnimator.gameObject);
            GameObject m_ShootPointObject = Instantiate(ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().ShootPoint.gameObject);
            m_ShootPointObject.name = ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().ShootPoint.gameObject.name;

            m_HitEffectObject.transform.parent = ExpandPrefabs.EXFriendlyForgeHammer.transform;
            m_TargetAnimatorObject.transform.parent = ExpandPrefabs.EXFriendlyForgeHammer.transform;
            m_ShadowObject.transform.parent = ExpandPrefabs.EXFriendlyForgeHammer.transform;
            m_ShootPointObject.transform.parent = ExpandPrefabs.EXFriendlyForgeHammer.transform;

            ExpandPrefabs.EXFriendlyForgeHammer.SetActive(false);

            m_ShadowObject.GetComponent<tk2dSpriteAnimator>().playAutomatically = false;
            m_ShadowObject.GetComponent<tk2dSpriteAnimator>().DefaultClipId = 28;
                        
            tk2dSprite m_NewHammerSprite = ExpandPrefabs.EXFriendlyForgeHammer.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(m_NewHammerSprite, ExpandObjectDatabase.ForgeHammer.GetComponent<tk2dSprite>());
                                    
            tk2dSpriteAnimation m_SourceHammerAnimationLibrary = ExpandObjectDatabase.ForgeHammer.GetComponent<tk2dSpriteAnimator>().Library;
            tk2dSpriteAnimation m_HammerAnimationLibrary = ExpandPrefabs.EXFriendlyForgeHammer.AddComponent<tk2dSpriteAnimation>();

            List<tk2dSpriteAnimationClip> m_Clips = new List<tk2dSpriteAnimationClip>();

            foreach (tk2dSpriteAnimationClip clip in m_SourceHammerAnimationLibrary.clips) {
                if (!string.IsNullOrEmpty(clip.name)) {
                    if (clip.name.ToLower().StartsWith("hammer_right_out")) {
                        m_Clips.Add(ExpandUtility.DuplicateAnimationClip(clip));
                    } else if (clip.name.ToLower().StartsWith("hammer_left_out")) {
                        m_Clips.Add(ExpandUtility.DuplicateAnimationClip(clip));
                    } else if (clip.name.ToLower().StartsWith("hammer_right_slam")) {
                        m_Clips.Add(ExpandUtility.DuplicateAnimationClip(clip));
                    } else if (clip.name.ToLower().StartsWith("hammer_left_slam")) {
                        m_Clips.Add(ExpandUtility.DuplicateAnimationClip(clip));
                    }
                }
            }
            
            if (m_Clips.Count > 0) { m_HammerAnimationLibrary.clips = m_Clips.ToArray(); }
            
            ExpandUtility.GenerateSpriteAnimator(ExpandPrefabs.EXFriendlyForgeHammer, m_HammerAnimationLibrary, 0, 0, false, false, false, false, false, false, 0, 0, false, false, false);
                        
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXFriendlyForgeHammer, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, true, true, false, false, false, false, false, true, new IntVector2(30, 24), new IntVector2(16, 24));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXFriendlyForgeHammer, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, true, true, false, false, false, false, false, true, new IntVector2(30, 8), new IntVector2(16, 16));
            
            AIBulletBank m_HammerBulletBank = ExpandPrefabs.EXFriendlyForgeHammer.AddComponent<AIBulletBank>();
            m_HammerBulletBank.useDefaultBulletIfMissing = true;
            m_HammerBulletBank.transforms = new List<Transform>(0);
            m_HammerBulletBank.Bullets = new List<AIBulletBank.Entry>() {
                new AIBulletBank.Entry() {
                    Name = "default",
                    BulletObject = ExpandPrefabs.EXFriendlyForgeHammerBullet,
                    OverrideProjectile = false,
                    PlayAudio = false,
                    AudioSwitch = string.Empty,
                    AudioEvent = string.Empty,
                    AudioLimitOncePerFrame = false,
                    AudioLimitOncePerAttack = false,
                    MuzzleLimitOncePerFrame = false,
                    MuzzleInheritsTransformDirection = false,
                    SpawnShells = false,
                    ShellForce = 1.75f,
                    ShellForceVariance = 0.75f,
                    DontRotateShell = false,
                    ShellGroundOffset = 0,
                    ShellsLimitOncePerFrame = false,
                    rampBullets = false,
                    rampStartHeight = 0,
                    rampTime = 0,
                    conditionalMinDegFromNorth = 0,
                    forceCanHitEnemies = true,
                    suppressHitEffectsIfOffscreen = false,
                    preloadCount = 0,
                    ProjectileData = new ProjectileData() {
                        damage = 0,
                        speed = 0,
                        range = 0,
                        damping = 0,
                        UsesCustomAccelerationCurve = false,
                        AccelerationCurve = new AnimationCurve(),
                        CustomAccelerationCurveDuration = 0,
                        onDestroyBulletScript = new BulletScriptSelector() { scriptTypeName = string.Empty }
                    },
                    MuzzleFlashEffects = new VFXPool() { type = VFXPoolType.None, effects = new VFXComplex[0] }
                }
            };

            GoopDefinition m_HammerGoopDefinition = Instantiate(ExpandObjectDatabase.ForgeHammer.GetComponent<ForgeHammerController>().GoopToDo);
            
            ExpandForgeHammerComponent EXFriendlyHammerController = ExpandPrefabs.EXFriendlyForgeHammer.AddComponent<ExpandForgeHammerComponent>();
            EXFriendlyHammerController.HitEffectAnimator = m_HitEffectObject.GetComponent<tk2dSpriteAnimator>();
            EXFriendlyHammerController.TargetAnimator = m_TargetAnimatorObject.GetComponent<tk2dSpriteAnimator>();
            EXFriendlyHammerController.ShadowAnimator = m_ShadowObject.GetComponent<tk2dSpriteAnimator>();
            EXFriendlyHammerController.ShootPoint = m_ShootPointObject.transform;
            EXFriendlyHammerController.GoopToDo = m_HammerGoopDefinition;

            FakePrefab.MarkAsFakePrefab(ExpandPrefabs.EXFriendlyForgeHammer);
            DontDestroyOnLoad(ExpandPrefabs.EXFriendlyForgeHammer);
        }*/
        
        public ExpandForgeHammerComponent() {
            IsCapturedHammer = false;
            TracksPlayer = false;
            TracksRandomEnemy = true;
            PowerScalesWithFloors = true;
            DisablesRegularForgeHammers = true;
            BulletIsAlreadlyFriendly = true;

            DoGoopOnImpact = false;
            DoesBulletsOnImpact = false;
            DeactivateOnEnemiesCleared = true;

            DamageToEnemies = 30f;
            InitialDelay = 1.5f;
            MinTimeBetweenAttacks = 1.5f;
            MaxTimeBetweenAttacks = 3f;

            ForceLeft = false;
            ForceRight = false;
            DoScreenShake = true;

            FlashDurationBeforeAttack = 0.75f;
            AdditionalTrackingTime = 0.5f;
            
            KnockbackForcePlayers = 50f;
            KnockbackForceEnemies = 65f;
            MinTimeToRestOnGround = 0.75f;
            MaxTimeToRestOnGround = 0.75f;
            ScreenShake = new ScreenShakeSettings() {
                magnitude = 0.48f,
                speed = 6,
                time = 0.08f,
                falloff = 0.1f,
                direction = Vector2.zero,
                vibrationType = ScreenShakeSettings.VibrationType.Auto,
                simpleVibrationTime = Vibration.Time.Normal,
                simpleVibrationStrength = Vibration.Strength.Medium,
            };
            Hammer_Anim_In_Left = "hammer_left_slam";
            Hammer_Anim_Out_Left = "hammer_left_out";
            Hammer_Anim_In_Right = "hammer_right_slam";
            Hammer_Anim_Out_Right = "hammer_right_out";

            BulletScript = new BulletScriptSelector() { scriptTypeName = "ForgeHammerCircle1" };

            m_localTimeScale = 1f;
            m_state = ExpandHammerState.Gone;

            m_isActive = false;
            m_destroyOnGone = false;
            IsActive = false;
        }

        public bool TracksRandomEnemy;
        public bool IsActive;
        public bool IsCapturedHammer;
        public bool PowerScalesWithFloors;
        public bool DisablesRegularForgeHammers;
        public bool BulletIsAlreadlyFriendly;
        
        
        public PlayerController Owner;

        [DwarfConfigurable]
        public bool TracksPlayer;

        [DwarfConfigurable]
        public bool DeactivateOnEnemiesCleared;

        [DwarfConfigurable]
        public bool ForceLeft;

        [DwarfConfigurable]
        public bool ForceRight;

        public float FlashDurationBeforeAttack;
        public float AdditionalTrackingTime;
        public float DamageToEnemies;
        public float KnockbackForcePlayers;
        public float KnockbackForceEnemies;

        [DwarfConfigurable]
        public float InitialDelay;

        [DwarfConfigurable]
        public float MinTimeBetweenAttacks;

        [DwarfConfigurable]
        public float MaxTimeBetweenAttacks;

        [DwarfConfigurable]
        public float MinTimeToRestOnGround;

        [DwarfConfigurable]
        public float MaxTimeToRestOnGround;

        public bool DoScreenShake;

        public ScreenShakeSettings ScreenShake;

        public string Hammer_Anim_In_Left;
        public string Hammer_Anim_Out_Left;
        public string Hammer_Anim_In_Right;
        public string Hammer_Anim_Out_Right;

        public tk2dSpriteAnimator HitEffectAnimator;
        public tk2dSpriteAnimator TargetAnimator;
        public tk2dSpriteAnimator ShadowAnimator;
        public tk2dSprite StaticTarget;

        public bool DoGoopOnImpact;
        
        [ShowInInspectorIf("DoGoopOnImpact", false)]
        public GoopDefinition GoopToDo;

        [DwarfConfigurable]
        public bool DoesBulletsOnImpact;

        [ShowInInspectorIf("DoGoopOnImpact", false)]
        public BulletScriptSelector BulletScript;

        [ShowInInspectorIf("DoGoopOnImpact", false)]
        public Transform ShootPoint;

        public RoomHandler ParentRoom;

        private float m_localTimeScale;

        private ExpandHammerState m_state;

        private float m_timer;

        private PlayerController m_targetPlayer;
        private AIActor m_SelectedEnemy;

        private Vector2 m_targetOffset;

        private string m_inAnim;
        private string m_outAnim;

        private float m_additionalTrackTimer;

        private Vector2 m_LastKnownPosition;

        private bool m_isActive;
        private bool m_destroyOnGone;
        private bool m_attackIsLeft;

        private BulletScriptSource m_bulletSource;

        public enum ExpandHammerState { InitialDelay, PreSwing, Swing, Grounded, UpSwing, Gone }
        
        public ExpandHammerState State {
            get { return m_state; }
            set {
                if (value != m_state) {
                    EndHammerState(m_state);
                    m_state = value;
                    BeginHammerState(m_state);
                }
            }
        }

        /*private ExpandHammerState SetState(ExpandHammerState? state) {
            if (!state.HasValue) {
                return m_state;
            } else {
                if (state.Value != m_state) {
                    EndHammerState(m_state);
                    m_state = state.Value;
                    BeginHammerState(m_state);
                }
                return m_state;
            }
        }*/

        private float LocalDeltaTime {
            get { return BraveTime.DeltaTime * LocalTimeScale; }
        }

        public float LocalTimeScale {
            get { return m_localTimeScale; }
            set {
                spriteAnimator.OverrideTimeScale = value;
                m_localTimeScale = value;
            }
        }

        public void Start() {
            PhysicsEngine.Instance.OnPostRigidbodyMovement += OnPostRigidbodyMovement;
        }

        public void Update() {
            if (!m_isActive && State == ExpandHammerState.Gone) {
                if (m_destroyOnGone) Destroy(gameObject);
                return;
            }

            if (m_isActive && State == ExpandHammerState.Gone && !IsCapturedHammer) {
                Vector2 unitBottomLeft = ParentRoom.area.UnitBottomLeft;
                Vector2 unitTopRight = ParentRoom.area.UnitTopRight;
                int num = 0;
                for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                    PlayerController playerController = GameManager.Instance.AllPlayers[i];
                    if (playerController && playerController.healthHaver.IsAlive) {
                        Vector2 centerPosition = playerController.CenterPosition;
                        if (BraveMathCollege.AABBContains(unitBottomLeft - Vector2.one, unitTopRight + Vector2.one, centerPosition)) { num++; }
                    }
                }
                if (num == 0) Deactivate();
            }
            if (!IsCapturedHammer | (IsCapturedHammer && State != ExpandHammerState.Gone)) {
                m_timer = Mathf.Max(0f, m_timer - LocalDeltaTime);
            }

            UpdateState(State);

            if (IsCapturedHammer && TracksPlayer && State != ExpandHammerState.UpSwing && State != ExpandHammerState.Grounded && State != ExpandHammerState.PreSwing && (m_additionalTrackTimer <= 0 && State != ExpandHammerState.Swing)) {
                UpdatePosition();
            }
        }


        public bool DoManualHammerBlow() {
            if (!m_isActive) { Activate(); return false; }
            if (State == ExpandHammerState.Gone) {
                m_timer = 0;
                return true;
            }
            return false;
        }

        public void Activate() {            
            if (m_isActive)return;
            if (IsCapturedHammer && !StaticTarget)StaticTarget = Instantiate(MrCap.MrCapHammerTarget, Owner.CenterPosition, Quaternion.identity).transform.Find("Sprite").gameObject.GetComponent<tk2dSprite>();
            if (DeactivateOnEnemiesCleared && !ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                ForceStop();
                return;
            }
            if (TracksRandomEnemy) {
                m_SelectedEnemy = ParentRoom.GetRandomActiveEnemy(false);
                if (m_SelectedEnemy) {
                    if (m_SelectedEnemy.healthHaver) {
                        if (!m_SelectedEnemy.healthHaver.IsDead) {
                            if (m_SelectedEnemy.specRigidbody) {
                                m_LastKnownPosition = m_SelectedEnemy.specRigidbody.GetUnitCenter(ColliderType.Ground);
                            } else {
                                m_LastKnownPosition = m_SelectedEnemy.sprite.WorldCenter;
                            }
                        } else {
                            m_LastKnownPosition = transform.position;
                        }
                    } else {
                        m_LastKnownPosition = transform.position;
                    }
                } else {
                    m_LastKnownPosition = transform.position;
                }
            }
            IsActive = true;
            m_isActive = true;
            if (State == ExpandHammerState.Gone)State = ExpandHammerState.InitialDelay;
        }

        public void Deactivate() {
            if (!m_isActive)return;
            if (encounterTrackable) GameStatsManager.Instance.HandleEncounteredObject(encounterTrackable);
            if (StaticTarget) Destroy(StaticTarget.transform.parent.gameObject);
            IsActive = false;
            m_isActive = false;
        }

        public void Deactivate(bool destroy) {
            if (!m_isActive) return;
            if (encounterTrackable) GameStatsManager.Instance.HandleEncounteredObject(encounterTrackable);
            if (StaticTarget) Destroy(StaticTarget.transform.parent.gameObject);
            IsActive = false;
            m_destroyOnGone = destroy;
            m_isActive = false;
        }

        private void UpdateState(ExpandHammerState state) {
            switch (state) {
                case ExpandHammerState.InitialDelay:
                    if (IsCapturedHammer) {
                        m_targetPlayer = Owner;
                        IntVector2 a = m_targetPlayer.CenterPosition.ToIntVector2(VectorConversions.Floor);
                        if (ForceLeft) {
                            m_attackIsLeft = true;
                        } else if (ForceRight) {
                            m_attackIsLeft = false;
                        } else {
                            int num = 0;
                            while (GameManager.Instance.Dungeon.data[a + IntVector2.Left * num].type != CellType.WALL) { num++; }
                            int num2 = 0;
                            while (GameManager.Instance.Dungeon.data[a + IntVector2.Right * num2].type != CellType.WALL) { num2++; }
                            m_attackIsLeft = (num < num2);
                        }
                        m_inAnim = ((!m_attackIsLeft) ? Hammer_Anim_In_Right : Hammer_Anim_In_Left);
                        m_outAnim = ((!m_attackIsLeft) ? Hammer_Anim_Out_Right : Hammer_Anim_Out_Left);
                        TargetAnimator.renderer.enabled = false;
                        // TargetAnimator.PlayAndDisableRenderer((!m_attackIsLeft) ? "hammer_right_target" : "hammer_left_target");
                        // TargetAnimator.StopAndResetFrame();
                        specRigidbody.enabled = false;
                        State = ExpandHammerState.Gone;
                        // m_timer = 999;
                        return;
                    }
                    if (m_timer <= 0f)State = ExpandHammerState.PreSwing;
                    return;
                case ExpandHammerState.PreSwing:
                    if (m_timer <= 0f) State = ExpandHammerState.Swing;
                    return;
                case ExpandHammerState.Swing:
                    m_additionalTrackTimer -= LocalDeltaTime;
                    if (!spriteAnimator.IsPlaying(m_inAnim)) State = ExpandHammerState.Grounded;
                    return;
                case ExpandHammerState.Grounded:
                    if (m_timer <= 0f) State = ExpandHammerState.UpSwing;
                    return;
                case ExpandHammerState.UpSwing:
                    if (!spriteAnimator.IsPlaying(m_outAnim)) State = ExpandHammerState.Gone;
                    return;
                case ExpandHammerState.Gone:
                    if (m_timer <= 0) {
                        State = ExpandHammerState.PreSwing;
                        if (TracksRandomEnemy) m_SelectedEnemy = ParentRoom.GetRandomActiveEnemy(false);
                    }
                    return;
                default: return;
            }
        }


        private void BeginHammerState(ExpandHammerState state) {
            switch (state) {
                case ExpandHammerState.InitialDelay:
                    TargetAnimator.renderer.enabled = false;
                    HitEffectAnimator.renderer.enabled = false;
                    sprite.renderer.enabled = false;
                    m_timer = InitialDelay;
                    return;
                case ExpandHammerState.PreSwing:
                    if (Owner){
                        m_targetPlayer = Owner;
                    } else {
                        m_targetPlayer = GameManager.Instance.GetRandomActivePlayer();
                    }
                    if (TracksRandomEnemy) {
                        m_SelectedEnemy = ParentRoom.GetRandomActiveEnemy(false);
                        if (DisablesRegularForgeHammers && GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                            if (StaticReferenceManager.AllForgeHammers != null && StaticReferenceManager.AllForgeHammers.Count > 0) {
                                List<ForgeHammerController> AllForgeHammers = StaticReferenceManager.AllForgeHammers;
                                for (int i = 0; i < AllForgeHammers.Count; i++) {
                                    if (AllForgeHammers[i].transform.position.GetAbsoluteRoom() == ParentRoom) { AllForgeHammers[i].Deactivate(); }
                                }
                            }
                        }
                    }
                    if (!IsCapturedHammer && GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                        List<PlayerController> list = new List<PlayerController>(2);
                        Vector2 unitBottomLeft = ParentRoom.area.UnitBottomLeft;
                        Vector2 unitTopRight = ParentRoom.area.UnitTopRight;
                        for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                            PlayerController playerController = GameManager.Instance.AllPlayers[i];
                            if (playerController && playerController.healthHaver.IsAlive) {
                                Vector2 centerPosition = playerController.CenterPosition;
                                if (BraveMathCollege.AABBContains(unitBottomLeft - Vector2.one, unitTopRight + Vector2.one, centerPosition)) {
                                    list.Add(playerController);
                                }
                            }
                        }
                        if (list.Count > 0) { m_targetPlayer = BraveUtility.RandomElement(list); }
                    }
                    IntVector2 a = m_targetPlayer.CenterPosition.ToIntVector2(VectorConversions.Floor);
                    if (ForceLeft) {
                        m_attackIsLeft = true;
                    } else if (ForceRight) {
                        m_attackIsLeft = false;
                    } else {
                        int num = 0;
                        while (GameManager.Instance.Dungeon.data[a + IntVector2.Left * num].type != CellType.WALL) { num++; }
                        int num2 = 0;
                        while (GameManager.Instance.Dungeon.data[a + IntVector2.Right * num2].type != CellType.WALL) { num2++; }
                        m_attackIsLeft = (num < num2);
                    }
                    m_inAnim = ((!m_attackIsLeft) ? Hammer_Anim_In_Right : Hammer_Anim_In_Left);
                    m_outAnim = ((!m_attackIsLeft) ? Hammer_Anim_Out_Right : Hammer_Anim_Out_Left);
                    TargetAnimator.StopAndResetFrame();
                    if (TracksPlayer | TracksRandomEnemy) {
                        TargetAnimator.renderer.enabled = true;
                    } else {
                        TargetAnimator.renderer.enabled = false;
                    }
                    TargetAnimator.PlayAndDisableRenderer((!m_attackIsLeft) ? "hammer_right_target" : "hammer_left_target");
                    m_targetOffset = ((!m_attackIsLeft) ? new Vector2(4.625f, 1.9375f) : new Vector2(1.9375f, 1.9375f));
                    m_timer = FlashDurationBeforeAttack;
                    return;
                case ExpandHammerState.Swing:
                    sprite.renderer.enabled = true;
                    spriteAnimator.Play(m_inAnim);
                    ShadowAnimator.renderer.enabled = true;
                    ShadowAnimator.Play((!m_attackIsLeft) ? "hammer_right_slam_shadow" : "hammer_left_slam_shadow");
                    sprite.HeightOffGround = -2.5f;
                    sprite.UpdateZDepth();
                    m_additionalTrackTimer = AdditionalTrackingTime;
                    return;
                case ExpandHammerState.Grounded:
                    if (DoScreenShake) {
                        GameManager.Instance.MainCameraController.DoScreenShake(ScreenShake, new Vector2?(specRigidbody.UnitCenter), false);
                    }
                    specRigidbody.enabled = true;
                    specRigidbody.PixelColliders[0].ManualOffsetX = ((!m_attackIsLeft) ? 59 : 16);
                    specRigidbody.PixelColliders[1].ManualOffsetX = ((!m_attackIsLeft) ? 59 : 16);
                    specRigidbody.ForceRegenerate(null, null);
                    specRigidbody.Reinitialize();
                    Exploder.DoRadialMinorBreakableBreak(TargetAnimator.sprite.WorldCenter, 4f);
                    HitEffectAnimator.renderer.enabled = true;
                    HitEffectAnimator.PlayAndDisableRenderer((!m_attackIsLeft) ? "hammer_right_slam_vfx" : "hammer_left_slam_vfx");
                    List<SpeculativeRigidbody> overlappingRigidbodies = PhysicsEngine.Instance.GetOverlappingRigidbodies(specRigidbody, null, false);
                    for (int j = 0; j < overlappingRigidbodies.Count; j++) {
                        if (overlappingRigidbodies[j].gameActor) {
                            Vector2 direction = overlappingRigidbodies[j].UnitCenter - specRigidbody.UnitCenter;
                            if (overlappingRigidbodies[j].gameActor is PlayerController) {
                                if (!IsCapturedHammer) {
                                    PlayerController playerController2 = overlappingRigidbodies[j].gameActor as PlayerController;
                                    if (overlappingRigidbodies[j].CollideWithOthers) {
                                        if (!playerController2.DodgeRollIsBlink || !playerController2.IsDodgeRolling) {
                                            if (overlappingRigidbodies[j].healthHaver) {
                                                overlappingRigidbodies[j].healthHaver.ApplyDamage(0.5f, direction, StringTableManager.GetEnemiesString("#FORGE_HAMMER", -1), CoreDamageTypes.None, DamageCategory.Normal, false, null, false);
                                            }
                                            if (overlappingRigidbodies[j].knockbackDoer) {
                                                overlappingRigidbodies[j].knockbackDoer.ApplyKnockback(direction, KnockbackForcePlayers, false);
                                            }
                                        }
                                    }
                                }
                            } else {
                                if (overlappingRigidbodies[j].healthHaver) {
                                    overlappingRigidbodies[j].healthHaver.ApplyDamage(DamageToEnemies, direction, StringTableManager.GetEnemiesString("#FORGE_HAMMER", -1), CoreDamageTypes.None, DamageCategory.Normal, false, null, false);
                                }
                                if (overlappingRigidbodies[j].knockbackDoer) {
                                    overlappingRigidbodies[j].knockbackDoer.ApplyKnockback(direction, KnockbackForceEnemies, false);
                                }
                            }
                        }
                    }
                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(specRigidbody, null, false);
                    if (DoGoopOnImpact) {
                        DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(GoopToDo).AddGoopRect(specRigidbody.UnitCenter + new Vector2(-1f, -1.25f), specRigidbody.UnitCenter + new Vector2(1f, 0.75f));
                    }
                    if (DoesBulletsOnImpact && m_isActive) {
                        ShootPoint.transform.position = specRigidbody.UnitCenter;
                        CellData cell = ShootPoint.transform.position.GetCell();
                        if (cell != null && cell.type != CellType.WALL) {
                            if (!m_bulletSource) m_bulletSource = ShootPoint.gameObject.GetOrAddComponent<BulletScriptSource>();
                            m_bulletSource.BulletManager = bulletBank;
                            m_bulletSource.BulletScript = BulletScript;
                            m_bulletSource.Initialize();
                            if ((TracksRandomEnemy | IsCapturedHammer) && Owner && bulletBank) {
                                bulletBank.OnProjectileCreated = (Action<Projectile>)Delegate.Combine(bulletBank.OnProjectileCreated, new Action<Projectile>(HandleForgeHammerPostProcessProjectile));
                            }
                        }
                    }
                    m_timer = UnityEngine.Random.Range(MinTimeToRestOnGround, MaxTimeToRestOnGround);
                    return;
                case ExpandHammerState.UpSwing:
                    spriteAnimator.Play(m_outAnim);
                    ShadowAnimator.PlayAndDisableRenderer((!m_attackIsLeft) ? "hammer_right_out_shadow" : "hammer_left_out_shadow");
                    return;
                case ExpandHammerState.Gone:
                    sprite.renderer.enabled = false;
                    if (IsCapturedHammer) {
                        m_timer = 999;
                    } else {
                        m_timer = UnityEngine.Random.Range(MinTimeBetweenAttacks, MaxTimeBetweenAttacks);
                    }
                    return;
                default: return;
            }
        }
        
        private void EndHammerState(ExpandHammerState state) {
            if (state == ExpandHammerState.Grounded)specRigidbody.enabled = false;
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            ExpandStaticReferenceManager.AllFriendlyHammers.Add(this);
            ParentRoom = room;
            m_targetPlayer = Owner;

            if (IsCapturedHammer)StaticTarget = Instantiate(MrCap.MrCapHammerTarget, Owner.CenterPosition, Quaternion.identity).transform.Find("Sprite").gameObject.GetComponent<tk2dSprite>();

            if (room.visibility == RoomHandler.VisibilityStatus.CURRENT | IsCapturedHammer) {
                DoRealConfigure(true);
            } else {
                StartCoroutine(FrameDelayedConfigure());
            }
        }

        private IEnumerator FrameDelayedConfigure() {
            yield return null;
            DoRealConfigure(false);
            yield break;
        }

        private void DoRealConfigure(bool activateNow) {
            InitStats();
            if (IsCapturedHammer) transform.position = m_targetPlayer.CenterPosition;
            if (ForceLeft) {
                transform.position += new Vector3(-1f, -1f, 0f);
            } else if (ForceRight) {
                transform.position += new Vector3(-3.5625f, -1f, 0f);
            }
            m_LastKnownPosition = transform.position;
            tk2dSpriteAnimator SpriteAnimator = spriteAnimator;
            SpriteAnimator.AnimationEventTriggered = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>)Delegate.Combine(spriteAnimator.AnimationEventTriggered, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>(HandleAnimationEvent));
            if (!IsCapturedHammer)ParentRoom.Entered += delegate (PlayerController a) { Activate(); };
            
            if (activateNow)Activate();
            if (DeactivateOnEnemiesCleared) {
                ParentRoom.OnEnemiesCleared = (Action)Delegate.Combine(ParentRoom.OnEnemiesCleared, new Action(Deactivate));
            }
        }

        private void InitStats() {
            if (IsCapturedHammer && !PowerScalesWithFloors) {
                DamageToEnemies = 60f;
                DoesBulletsOnImpact = true;
                DoGoopOnImpact = true;
                MinTimeBetweenAttacks = 1.25f;
                MaxTimeBetweenAttacks = 2.5f;
            } else if (PowerScalesWithFloors) {
                switch (GameManager.Instance.Dungeon.tileIndices.tilesetId) {
                    default:
                        DamageToEnemies = 40;
                        DoesBulletsOnImpact = false;
                        DoGoopOnImpact = false;
                        return;
                    case GlobalDungeonData.ValidTilesets.CASTLEGEON:
                        DamageToEnemies = 30f;
                        DoesBulletsOnImpact = false;
                        DoGoopOnImpact = false;
                        return;
                    case GlobalDungeonData.ValidTilesets.SEWERGEON:
                        DamageToEnemies = 40f;
                        DoesBulletsOnImpact = false;
                        DoGoopOnImpact = false;
                        return;
                    case GlobalDungeonData.ValidTilesets.GUNGEON:
                        DamageToEnemies = 35f;
                        DoesBulletsOnImpact = false;
                        DoGoopOnImpact = false;
                        return;
                    case GlobalDungeonData.ValidTilesets.CATHEDRALGEON:
                        DamageToEnemies = 45f;
                        DoesBulletsOnImpact = false;
                        DoGoopOnImpact = false;
                        return;
                    case GlobalDungeonData.ValidTilesets.MINEGEON:
                        DamageToEnemies = 40f;
                        DoesBulletsOnImpact = false;
                        DoGoopOnImpact = true;
                        return;
                    case GlobalDungeonData.ValidTilesets.RATGEON:
                        DamageToEnemies = 50f;
                        DoesBulletsOnImpact = false;
                        DoGoopOnImpact = true;
                        return;
                    case GlobalDungeonData.ValidTilesets.CATACOMBGEON:
                        DamageToEnemies = 45f;
                        DoesBulletsOnImpact = false;
                        DoGoopOnImpact = true;
                        return;
                    case GlobalDungeonData.ValidTilesets.OFFICEGEON:
                        DamageToEnemies = 55f;
                        DoesBulletsOnImpact = false;
                        DoGoopOnImpact = true;
                        return;
                    case GlobalDungeonData.ValidTilesets.WESTGEON:
                        DamageToEnemies = 57f;
                        DoesBulletsOnImpact = false;
                        DoGoopOnImpact = true;
                        return;
                    case GlobalDungeonData.ValidTilesets.FORGEGEON:
                        DamageToEnemies = 60f;
                        DoesBulletsOnImpact = true;
                        DoGoopOnImpact = true;
                        MinTimeBetweenAttacks = 1.25f;
                        MaxTimeBetweenAttacks = 2.5f;
                        return;
                    case GlobalDungeonData.ValidTilesets.HELLGEON:
                        DamageToEnemies = 65f;
                        DoesBulletsOnImpact = true;
                        DoGoopOnImpact = true;
                        MinTimeBetweenAttacks = 1.25f;
                        MaxTimeBetweenAttacks = 2.5f;
                        return;
                }
            }

        }

        private void HandleAnimationEvent(tk2dSpriteAnimator sourceAnimator, tk2dSpriteAnimationClip sourceClip, int sourceFrame) {
            if (State == ExpandHammerState.Swing && sourceClip.frames[sourceFrame].eventInfo == "impact") State = ExpandHammerState.Grounded;
        }

        private void OnPostRigidbodyMovement() {
            if ((TracksPlayer | TracksRandomEnemy) && (State == ExpandHammerState.PreSwing || (m_additionalTrackTimer > 0f && State == ExpandHammerState.Swing))) {
                if (TracksPlayer) {
                    UpdatePosition();
                } else if (TracksRandomEnemy) {
                    UpdatePosition(m_SelectedEnemy);
                } else {
                    UpdatePosition();
                }
            }
        }

        private void UpdatePosition(AIActor targetActor = null) {
            Vector2 unitBottomLeft = ParentRoom.area.UnitBottomLeft;
            Vector2 unitTopRight = ParentRoom.area.UnitTopRight;
            if (TracksRandomEnemy) {
                if (targetActor != null && targetActor.healthHaver != null) {
                    if (!targetActor.healthHaver.IsDead) {
                        if (targetActor.specRigidbody) {
                            m_LastKnownPosition = targetActor.specRigidbody.GetUnitCenter(ColliderType.Ground);
                        } else {
                            m_LastKnownPosition = targetActor.sprite.WorldCenter;
                        }
                    } 
                }
            } else {
                m_LastKnownPosition = m_targetPlayer.CenterPosition;
            }
            if (!IsCapturedHammer)m_LastKnownPosition = BraveMathCollege.ClampToBounds(m_LastKnownPosition, unitBottomLeft + Vector2.one, unitTopRight - Vector2.one);
            transform.position = (m_LastKnownPosition - m_targetOffset).Quantize(0.0625f);
            if (StaticTarget) {
                StaticTarget.gameObject.transform.parent.position = m_targetPlayer.CenterPosition;
                StaticTarget.UpdateZDepth();
            }
            TargetAnimator.sprite.UpdateZDepth();
            sprite.UpdateZDepth();
        }

        protected void HandleForgeHammerPostProcessProjectile(Projectile obj) {
            if (obj) {
                if (!BulletIsAlreadlyFriendly) {
                    obj.collidesWithPlayer = false;
                    obj.collidesWithEnemies = true;
                    obj.TreatedAsNonProjectileForChallenge = true;
                }
                if (Owner) {
                    if (PassiveItem.IsFlagSetForCharacter(Owner, typeof(BattleStandardItem))) {
                        obj.baseData.damage *= BattleStandardItem.BattleStandardCompanionDamageMultiplier;
                    }
                    if (Owner.CurrentGun && Owner.CurrentGun.LuteCompanionBuffActive) {
                        obj.baseData.damage *= 2f;
                        obj.RuntimeUpdateScale(1f / obj.AdditionalScaleMultiplier);
                        obj.RuntimeUpdateScale(1.75f);
                    }
                    obj.baseData.damage *= 5f;
                    Owner.DoPostProcessProjectile(obj);
                } else {
                    obj.baseData.damage *= 4;
                }
            }
        }

        private void ForceStop() {
            if (TargetAnimator)TargetAnimator.renderer.enabled = false;
            if (HitEffectAnimator) HitEffectAnimator.renderer.enabled = false;
            if (sprite) sprite.renderer.enabled = false;
            if (ShadowAnimator)ShadowAnimator.renderer.enabled = false;
            specRigidbody.enabled = false;
            if (m_bulletSource)m_bulletSource.ForceStop();
            State = ExpandHammerState.Gone;
        }

        protected override void OnDestroy() {
            ExpandStaticReferenceManager.AllFriendlyHammers.Remove(this);
            if (StaticTarget) Destroy(StaticTarget.transform.parent.gameObject);
            base.OnDestroy();
        }
    }
}

