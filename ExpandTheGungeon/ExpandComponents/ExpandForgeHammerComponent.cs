using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents { 
        
    public class ExpandForgeHammerComponent : DungeonPlaceableBehaviour, IPlaceConfigurable {
        
        public static void BuildPrefab() {

            ExpandPrefabs.EXFriendlyForgeHammer = new GameObject("Friendly_Forge_Hammer") { layer = 22 };
            
            GameObject m_HitEffectObject = Instantiate(ExpandPrefabs.ForgeHammer.GetComponent<ForgeHammerController>().HitEffectAnimator.gameObject);
            GameObject m_TargetAnimatorObject = Instantiate(ExpandPrefabs.ForgeHammer.GetComponent<ForgeHammerController>().TargetAnimator.gameObject);
            GameObject m_ShadowObject = Instantiate(ExpandPrefabs.ForgeHammer.GetComponent<ForgeHammerController>().ShadowAnimator.gameObject);
            GameObject m_ShootPointObject = Instantiate(ExpandPrefabs.ForgeHammer.GetComponent<ForgeHammerController>().ShootPoint.gameObject);
            m_ShootPointObject.name = ExpandPrefabs.ForgeHammer.GetComponent<ForgeHammerController>().ShootPoint.gameObject.name;

            m_HitEffectObject.transform.parent = ExpandPrefabs.EXFriendlyForgeHammer.transform;
            m_TargetAnimatorObject.transform.parent = ExpandPrefabs.EXFriendlyForgeHammer.transform;
            m_ShadowObject.transform.parent = ExpandPrefabs.EXFriendlyForgeHammer.transform;
            m_ShootPointObject.transform.parent = ExpandPrefabs.EXFriendlyForgeHammer.transform;

            ExpandPrefabs.EXFriendlyForgeHammer.SetActive(false);

            m_ShadowObject.GetComponent<tk2dSpriteAnimator>().playAutomatically = false;
            m_ShadowObject.GetComponent<tk2dSpriteAnimator>().DefaultClipId = 28;
                        
            tk2dSprite m_NewHammerSprite = ExpandPrefabs.EXFriendlyForgeHammer.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(m_NewHammerSprite, ExpandPrefabs.ForgeHammer.GetComponent<tk2dSprite>());
                                    
            tk2dSpriteAnimation m_SourceHammerAnimationLibrary = ExpandPrefabs.ForgeHammer.GetComponent<tk2dSpriteAnimator>().Library;
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

            GoopDefinition m_HammerGoopDefinition = Instantiate(ExpandPrefabs.ForgeHammer.GetComponent<ForgeHammerController>().GoopToDo);
            
            ExpandForgeHammerComponent EXFriendlyHammerController = ExpandPrefabs.EXFriendlyForgeHammer.AddComponent<ExpandForgeHammerComponent>();
            EXFriendlyHammerController.HitEffectAnimator = m_HitEffectObject.GetComponent<tk2dSpriteAnimator>();
            EXFriendlyHammerController.TargetAnimator = m_TargetAnimatorObject.GetComponent<tk2dSpriteAnimator>();
            EXFriendlyHammerController.ShadowAnimator = m_ShadowObject.GetComponent<tk2dSpriteAnimator>();
            EXFriendlyHammerController.ShootPoint = m_ShootPointObject.transform;
            EXFriendlyHammerController.GoopToDo = m_HammerGoopDefinition;

            FakePrefab.MarkAsFakePrefab(ExpandPrefabs.EXFriendlyForgeHammer);
            DontDestroyOnLoad(ExpandPrefabs.EXFriendlyForgeHammer);
        }
        
        public ExpandForgeHammerComponent() {
            TracksPlayer = false;
            TracksRandomEnemy = true;
            PowerScalesWithFloors = true;
            DisablesRegularForgeHammers = true;

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
            IsActive = false;
        }

        public bool TracksRandomEnemy;
        public bool IsActive;
        public bool PowerScalesWithFloors;
        public bool DisablesRegularForgeHammers;
        
        public PlayerController m_Owner;

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

        public bool DoGoopOnImpact;

        [ShowInInspectorIf("DoGoopOnImpact", false)]
        public GoopDefinition GoopToDo;

        [DwarfConfigurable]
        public bool DoesBulletsOnImpact;

        [ShowInInspectorIf("DoGoopOnImpact", false)]
        public BulletScriptSelector BulletScript;

        [ShowInInspectorIf("DoGoopOnImpact", false)]
        public Transform ShootPoint;

        private float m_localTimeScale;

        private ExpandHammerState m_state;

        private float m_timer;

        private PlayerController m_targetPlayer;
        private AIActor m_SelectedEnemy;

        private Vector2 m_targetOffset;

        private string m_inAnim;
        private string m_outAnim;

        private float m_additionalTrackTimer;

        private RoomHandler m_room;
        
        private Vector2 m_LastKnownPosition;

        private bool m_isActive;
        private bool m_attackIsLeft;

        private BulletScriptSource m_bulletSource;

        private enum ExpandHammerState {
            InitialDelay,
            PreSwing,
            Swing,
            Grounded,
            UpSwing,
            Gone
        }
        
        private ExpandHammerState SetState(ExpandHammerState? state) {
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
        }

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

            GlobalDungeonData.ValidTilesets currentTileset = GameManager.Instance.Dungeon.tileIndices.tilesetId;
            if (PowerScalesWithFloors) {
                if (currentTileset == GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                    DamageToEnemies = 30f;
                    DoesBulletsOnImpact = false;
                    DoGoopOnImpact = false;
                } else if (currentTileset == GlobalDungeonData.ValidTilesets.SEWERGEON) {
                    DamageToEnemies = 40f;
                    DoesBulletsOnImpact = false;
                    DoGoopOnImpact = false;
                } else if (currentTileset == GlobalDungeonData.ValidTilesets.GUNGEON) {
                    DamageToEnemies = 35f;
                    DoesBulletsOnImpact = false;
                    DoGoopOnImpact = false;
                } else if (currentTileset == GlobalDungeonData.ValidTilesets.CATHEDRALGEON) {
                    DamageToEnemies = 45f;
                    DoesBulletsOnImpact = false;
                    DoGoopOnImpact = false;
                } else if (currentTileset == GlobalDungeonData.ValidTilesets.MINEGEON) {
                    DamageToEnemies = 40f;
                    DoesBulletsOnImpact = false;
                    DoGoopOnImpact = true;
                } else if (currentTileset == GlobalDungeonData.ValidTilesets.RATGEON) {
                    DamageToEnemies = 50f;
                    DoesBulletsOnImpact = false;
                    DoGoopOnImpact = true;
                } else if (currentTileset == GlobalDungeonData.ValidTilesets.CATACOMBGEON) {
                    DamageToEnemies = 45f;
                    DoesBulletsOnImpact = false;
                    DoGoopOnImpact = true;
                } else if (currentTileset == GlobalDungeonData.ValidTilesets.OFFICEGEON) {
                    DamageToEnemies = 55f;
                    DoesBulletsOnImpact = false;
                    DoGoopOnImpact = true;
                } else if (currentTileset == GlobalDungeonData.ValidTilesets.WESTGEON) {
                    DamageToEnemies = 57f;
                    DoesBulletsOnImpact = false;
                    DoGoopOnImpact = true;
                } else if (currentTileset == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                    DamageToEnemies = 60f;
                    DoesBulletsOnImpact = true;
                    DoGoopOnImpact = true;
                    MinTimeBetweenAttacks = 1.25f;
                    MaxTimeBetweenAttacks = 2.5f;
                } else if (currentTileset == GlobalDungeonData.ValidTilesets.HELLGEON) {
                    DamageToEnemies = 65f;
                    DoesBulletsOnImpact = true;
                    DoGoopOnImpact = true;
                    MinTimeBetweenAttacks = 1.25f;
                    MaxTimeBetweenAttacks = 2.5f;
                } else {
                    DamageToEnemies = 30f;
                    DoesBulletsOnImpact = false;
                    DoGoopOnImpact = false;
                }
            }
        }

        public void Update() {
            if (!m_isActive && SetState(null) == ExpandHammerState.Gone) { return; }
            if (m_isActive && SetState(null) == ExpandHammerState.Gone) {
                Vector2 unitBottomLeft = m_room.area.UnitBottomLeft;
                Vector2 unitTopRight = m_room.area.UnitTopRight;
                int num = 0;
                for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                    PlayerController playerController = GameManager.Instance.AllPlayers[i];
                    if (playerController && playerController.healthHaver.IsAlive) {
                        Vector2 centerPosition = playerController.CenterPosition;
                        if (BraveMathCollege.AABBContains(unitBottomLeft - Vector2.one, unitTopRight + Vector2.one, centerPosition)) { num++; }
                    }
                }
                if (num == 0) { Deactivate(); }
            }
            m_timer = Mathf.Max(0f, m_timer - LocalDeltaTime);
            UpdateState(SetState(null));
        }
        
        public void Activate() {            
            if (m_isActive) { return; }            
            if (DeactivateOnEnemiesCleared && !m_room.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                ForceStop();
                return;
            }
            if (TracksRandomEnemy) {
                m_SelectedEnemy = m_room.GetRandomActiveEnemy(false);
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
            if (SetState(null) == ExpandHammerState.Gone) { SetState(ExpandHammerState.InitialDelay); }
        }

        public void Deactivate() {
            if (!m_isActive) { return; }
            if (encounterTrackable) {
                GameStatsManager.Instance.HandleEncounteredObject(encounterTrackable);
            }
            
            IsActive = false;
            m_isActive = false;
        }
        
        private void BeginHammerState(ExpandHammerState state) {
            if (state == ExpandHammerState.InitialDelay) {
                TargetAnimator.renderer.enabled = false;
                HitEffectAnimator.renderer.enabled = false;
                sprite.renderer.enabled = false;
                m_timer = InitialDelay;
            } else if (state == ExpandHammerState.PreSwing) {
                if (m_Owner != null) {
                    m_targetPlayer = m_Owner;
                } else {
                    m_targetPlayer = GameManager.Instance.GetRandomActivePlayer();
                }
                if (TracksRandomEnemy) {
                    m_SelectedEnemy = m_room.GetRandomActiveEnemy(false);
                    if (DisablesRegularForgeHammers && GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                        if (StaticReferenceManager.AllForgeHammers != null && StaticReferenceManager.AllForgeHammers.Count > 0) {
                            List<ForgeHammerController> AllForgeHammers = StaticReferenceManager.AllForgeHammers;
                            for (int i = 0; i < AllForgeHammers.Count; i++) {
                                if (AllForgeHammers[i].transform.position.GetAbsoluteRoom() == m_room) { AllForgeHammers[i].Deactivate(); }
                            }
                        }
                    }
                }
                if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                    List<PlayerController> list = new List<PlayerController>(2);
                    Vector2 unitBottomLeft = m_room.area.UnitBottomLeft;
                    Vector2 unitTopRight = m_room.area.UnitTopRight;
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
            } else if (state == ExpandHammerState.Swing) {
                sprite.renderer.enabled = true;
                spriteAnimator.Play(m_inAnim);
                ShadowAnimator.renderer.enabled = true;
                ShadowAnimator.Play((!m_attackIsLeft) ? "hammer_right_slam_shadow" : "hammer_left_slam_shadow");
                sprite.HeightOffGround = -2.5f;
                sprite.UpdateZDepth();
                m_additionalTrackTimer = AdditionalTrackingTime;
            } else if (state == ExpandHammerState.Grounded) {
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
                        if (!m_bulletSource) {
                            m_bulletSource = ShootPoint.gameObject.GetOrAddComponent<BulletScriptSource>();
                        }
                        m_bulletSource.BulletManager = bulletBank;
                        m_bulletSource.BulletScript = BulletScript;                       
                        m_bulletSource.Initialize();
                        if (TracksRandomEnemy && m_Owner != null) {
                            if (bulletBank) {
                                bulletBank.OnProjectileCreated = (Action<Projectile>)Delegate.Combine(bulletBank.OnProjectileCreated, new Action<Projectile>(HandleForgeHammerPostProcessProjectile));
                                /*if (gameObject.GetComponent<AIBulletBank>()) {
                                    AIBulletBank aiBulletBank = gameObject.GetComponent<AIBulletBank>();
                                    aiBulletBank.Bullets[0].forceCanHitEnemies = true;
                                }*/
                            }
                        }
                    }
                }
                m_timer = UnityEngine.Random.Range(MinTimeToRestOnGround, MaxTimeToRestOnGround);
            } else if (state == ExpandHammerState.UpSwing) {
                spriteAnimator.Play(m_outAnim);
                ShadowAnimator.PlayAndDisableRenderer((!m_attackIsLeft) ? "hammer_right_out_shadow" : "hammer_left_out_shadow");
            } else if (state == ExpandHammerState.Gone) {
                sprite.renderer.enabled = false;                
                m_timer = UnityEngine.Random.Range(MinTimeBetweenAttacks, MaxTimeBetweenAttacks);
            }
        }

        private void UpdateState(ExpandHammerState state) {
            if (state == ExpandHammerState.InitialDelay) {
                if (m_timer <= 0f) { SetState(ExpandHammerState.PreSwing); }
            } else if (state == ExpandHammerState.PreSwing) {
                if (m_timer <= 0f) { SetState(ExpandHammerState.Swing); }
            } else if (state == ExpandHammerState.Swing) {
                m_additionalTrackTimer -= LocalDeltaTime;
                if (!spriteAnimator.IsPlaying(m_inAnim)) { SetState(ExpandHammerState.Grounded); }
            } else if (state == ExpandHammerState.Grounded) {
                if (m_timer <= 0f) { SetState(ExpandHammerState.UpSwing); }
            } else if (state == ExpandHammerState.UpSwing) {
                if (!spriteAnimator.IsPlaying(m_outAnim)) { SetState(ExpandHammerState.Gone); }
            } else if (state == ExpandHammerState.Gone && m_timer <= 0f) {                
                SetState(ExpandHammerState.PreSwing);
                if (TracksRandomEnemy) { m_SelectedEnemy = m_room.GetRandomActiveEnemy(false); }
            }
        }

        private void EndHammerState(ExpandHammerState state) {
            if (state == ExpandHammerState.Grounded) { specRigidbody.enabled = false; }
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            ExpandStaticReferenceManager.AllFriendlyHammers.Add(this);            
            m_room = room;
            gameObject.SetActive(true);
            ShootPoint.gameObject.SetActive(true);
            enabled = true;

            if (room.visibility == RoomHandler.VisibilityStatus.CURRENT) {
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
            if (ForceLeft) {
                transform.position += new Vector3(-1f, -1f, 0f);
            } else if (ForceRight) {
                transform.position += new Vector3(-3.5625f, -1f, 0f);
            }
            m_LastKnownPosition = transform.position;
            tk2dSpriteAnimator spriteAnimator = base.spriteAnimator;
            spriteAnimator.AnimationEventTriggered = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>)Delegate.Combine(spriteAnimator.AnimationEventTriggered, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>(HandleAnimationEvent));
            m_room.Entered += delegate (PlayerController a) { Activate(); };
            
            if (activateNow) { Activate(); }
            if (DeactivateOnEnemiesCleared) {
                RoomHandler room = m_room;
                room.OnEnemiesCleared = (Action)Delegate.Combine(room.OnEnemiesCleared, new Action(Deactivate));
            }
        }

        private void HandleAnimationEvent(tk2dSpriteAnimator sourceAnimator, tk2dSpriteAnimationClip sourceClip, int sourceFrame) {
            if (SetState(null) == ExpandHammerState.Swing && sourceClip.frames[sourceFrame].eventInfo == "impact") { SetState(ExpandHammerState.Grounded); }
        }

        private void OnPostRigidbodyMovement() {
            if ((TracksPlayer | TracksRandomEnemy) && (SetState(null) == ExpandHammerState.PreSwing || (m_additionalTrackTimer > 0f && SetState(null) == ExpandHammerState.Swing))) {
                if (TracksPlayer) {
                    UpdatePosition(null);
                } else if (TracksRandomEnemy) {
                    UpdatePosition(m_SelectedEnemy);
                } else {
                    UpdatePosition(null);
                }
            }
        }

        private void UpdatePosition(AIActor targetActor = null) {
            Vector2 unitBottomLeft = m_room.area.UnitBottomLeft;
            Vector2 unitTopRight = m_room.area.UnitTopRight;
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
            m_LastKnownPosition = BraveMathCollege.ClampToBounds(m_LastKnownPosition, unitBottomLeft + Vector2.one, unitTopRight - Vector2.one);
            transform.position = (m_LastKnownPosition - m_targetOffset).Quantize(0.0625f);
            TargetAnimator.sprite.UpdateZDepth();
            sprite.UpdateZDepth();
        }

        protected void HandleForgeHammerPostProcessProjectile(Projectile obj) {
            if (obj) {
                obj.collidesWithPlayer = false;
                obj.collidesWithEnemies = true;
                obj.TreatedAsNonProjectileForChallenge = true;
                if (m_Owner) {
                    if (PassiveItem.IsFlagSetForCharacter(m_Owner, typeof(BattleStandardItem))) {
                        obj.baseData.damage *= BattleStandardItem.BattleStandardCompanionDamageMultiplier;
                    }
                    if (m_Owner.CurrentGun && m_Owner.CurrentGun.LuteCompanionBuffActive) {
                        obj.baseData.damage *= 2f;
                        obj.RuntimeUpdateScale(1f / obj.AdditionalScaleMultiplier);
                        obj.RuntimeUpdateScale(1.75f);
                    }
                    obj.baseData.damage *= 5f;
                    m_Owner.DoPostProcessProjectile(obj);
                } else {
                    obj.baseData.damage *= 4;
                }
            }
        }

        private void ForceStop() {
            if (TargetAnimator) { TargetAnimator.renderer.enabled = false; }
            if (HitEffectAnimator) { HitEffectAnimator.renderer.enabled = false; }
            if (sprite) { sprite.renderer.enabled = false; }
            if (ShadowAnimator) { ShadowAnimator.renderer.enabled = false; }
            specRigidbody.enabled = false;
            if (m_bulletSource) { m_bulletSource.ForceStop(); }
            SetState(ExpandHammerState.Gone);
        }

        protected override void OnDestroy() {
            ExpandStaticReferenceManager.AllFriendlyHammers.Remove(this);
            base.OnDestroy();
        }
    }
}

