using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using FullInspector;
using MonoMod.RuntimeDetour;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandCustomEnemyDatabase : EnemyDatabase {

        public static Hook loadEnemyGUIDHook;
        public static Dictionary<string, GameObject> enemyPrefabDictionary = new Dictionary<string, GameObject>();

        public static GameObject HammerCompanionPrefab;
        public static GameObject BootlegBulletManPrefab;

        public static string HammerCompanionGUID;
        public static string BootlegBulletManGUID;

        public static void InitPrefabs() {
            loadEnemyGUIDHook = new Hook(
                typeof(EnemyDatabase).GetMethod("GetOrLoadByGuid", BindingFlags.Static | BindingFlags.Public),
                typeof(ExpandCustomEnemyDatabase).GetMethod("GetOrLoadByGuidHook", BindingFlags.Static | BindingFlags.Public)
            );

            BuildBabyGoodHammerPrefab(out HammerCompanionPrefab);

            BuildBootlegBulletManPrefab(out BootlegBulletManPrefab);
        }

        public static void AddEnemyToDatabase(GameObject EnemyPrefab, string EnemyGUID, bool IsNormalEnemy = false) {
            EnemyDatabaseEntry item = new EnemyDatabaseEntry {
                myGuid = EnemyGUID,
                placeableWidth = 2,
                placeableHeight = 2,
                isNormalEnemy = IsNormalEnemy
            };
            Instance.Entries.Add(item);
            enemyPrefabDictionary.Add(EnemyGUID, EnemyPrefab);
        }
        
        public static AIActor GetOrLoadByGuidHook(Func<string, AIActor> orig, string guid) {
            foreach (string text in enemyPrefabDictionary.Keys) {
                if (text == guid) { return enemyPrefabDictionary[text].GetComponent<AIActor>(); }
            }
            return orig(guid);
        }

        public static AIActor GetOrLoadByGuid_Orig(string guid) { return Instance.InternalGetByGuid(guid); }


        public static void BuildBabyGoodHammerPrefab(out GameObject m_CachedTargetObject, bool isFakeprefab = true) {

            AIActor m_CachedEnemyActor = GetOrLoadByGuid_Orig("01972dee89fc4404a5c408d50007dad5");

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = new GameObject("Baby Good Hammer") { layer = 28 };

            string basePath = "ExpandTheGungeon/Textures/Items/Animations/babygoodhammer/AIActorAnimations/";

            List<string> IdleSpriteList = new List<string>() {
                "babygoodhammer_idle_down_01",
                "babygoodhammer_idle_down_02",
                "babygoodhammer_idle_down_03",
                "babygoodhammer_idle_down_04",
                "babygoodhammer_idle_down_05",
                "babygoodhammer_idle_down_06",
            };

            List<string> MoveLeftSpriteList = new List<string>() {
                "babygoodhammer_move_left_02",
                "babygoodhammer_move_left_02"
            };

            List<string> MoveRightSpriteList = new List<string>() {
                "babygoodhammer_move_right_02",
                "babygoodhammer_move_right_02"
            };

            ItemAPI.ItemBuilder.AddSpriteToObject(m_CachedTargetObject, (basePath + IdleSpriteList[0]), false, isFakeprefab);

            tk2dSprite m_CachedSprite = m_CachedTargetObject.GetComponent<tk2dSprite>();

            foreach (string spriteName in IdleSpriteList) { ItemAPI.SpriteBuilder.AddSpriteToCollection((basePath + spriteName), m_CachedSprite.Collection); }
            foreach (string spriteName in MoveLeftSpriteList) { ItemAPI.SpriteBuilder.AddSpriteToCollection((basePath + spriteName), m_CachedSprite.Collection); }
            foreach (string spriteName in MoveRightSpriteList) { ItemAPI.SpriteBuilder.AddSpriteToCollection((basePath + spriteName), m_CachedSprite.Collection); }

            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, false, false, false, true, ClipFps: 6);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleSpriteList, "Hammer_Idle_Down", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveLeftSpriteList, "Hammer_Move_Left", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveRightSpriteList, "Hammer_Move_Right", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            // tk2dSprite newSprite = m_CachedTargetObject.AddComponent<tk2dSprite>();
            // ExpandUtility.DuplicateSprite(newSprite, (m_SelectedPlayer.sprite as tk2dSprite));


            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, Guid.NewGuid().ToString(), null, instantiateCorpseObject: false, ExternalCorpseObject: m_CachedEnemyActor.CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            HammerCompanionGUID = m_CachedAIActor.EnemyGuid;

            m_CachedAIActor.MovementSpeed = 5;
            // m_CachedAIActor.PreventFallingInPitsEver = true;
            m_CachedAIActor.SetIsFlying(true, "Flying Enemy", true, true);

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 1,
                    ManualOffsetY = 2,
                    ManualWidth = 19,
                    ManualHeight = 11,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 8,
                    ManualOffsetY = 1,
                    ManualWidth = 12,
                    ManualHeight = 16,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Movement;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "idle",
                    // AnimNames = new string[] { "idle_backward", "idle" },
                    AnimNames = new string[] { "Hammer_Idle_Down", "Hammer_Idle_Down", "Hammer_Idle_Down", "Hammer_Idle_Down" },
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "run",
                    AnimNames = new string[] { "Hammer_Idle_Down", "Hammer_Move_Right", "Hammer_Move_Left", "Hammer_Idle_Down" },
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                /*m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                            Prefix = "death",
                            AnimNames = new string[] { "Hammer_Idle_Down", "Hammer_Idle_Down" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    }
                };*/
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };

            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new CompanionFollowPlayerBehavior() {
                    PathInterval = 0.25f,
                    DisableInCombat = false,
                    IdealRadius = 5,
                    CatchUpRadius = 9.5f,
                    CatchUpAccelTime = 3,
                    CatchUpSpeed = 6,
                    CatchUpMaxSpeed = 10,
                    CatchUpAnimation = string.Empty,
                    CatchUpOutAnimation = string.Empty,
                    IdleAnimations = new string[0],
                    CanRollOverPits = false,
                    RollAnimation = string.Empty
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ResourceExtractor.BuildStringListFromEmbeddedResource("BehaviorScripts\\BabyGoodHammer_BehaviorScript.txt");

            ExpandUtility.MakeCompanion(m_CachedAIActor, null, null, true, false, true, true);

            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, false);
            ItemAPI.BabyGoodHammer.CompanionGuid = m_CachedAIActor.EnemyGuid;
            DontDestroyOnLoad(m_CachedTargetObject);

            m_CachedEnemyActor = null;
            return;
        }
        
        public static void BuildBootlegBulletManPrefab(out GameObject m_CachedTargetObject, bool isFakeprefab = true) {

            AIActor m_CachedEnemyActor = GetOrLoadByGuid_Orig("01972dee89fc4404a5c408d50007dad5"); //bullet_kin

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = new GameObject("Bootleg BulletMan") { layer = 28 };

            string basePath = "ExpandTheGungeon/Textures/BootlegEnemies2/BulletMan/";

            List<string> SpriteLIst = new List<string>() {
                "bulletman_cover_left_idle_001",
                "bulletman_cover_left_leap_001",
                "bulletman_cover_right_idle_001",
                "bulletman_cover_right_leap_001",
                "bulletman_hit_left_001",
                "bulletman_hit_right_001",
                "bulletman_idle_down_001",
                "bulletman_idle_left_001",
                "bulletman_idle_right_001",
                "bulletman_idle_up_001",
                "bulletman_pitfall_001",
                "bulletman_pitfall_002",
                "bulletman_pitfall_003",
                "bulletman_pitfall_004",
                "bulletman_pitfall_005",
                "bulletman_run_down_001",
                "bulletman_run_left_001",
                "bulletman_run_right_001",
                "bulletman_run_up_001",
                "bulletman_spawn_001",
                "bulletman_spawn_002",
                "bulletman_spawn_003"
            };

            List<string> IdleDownSpriteList = new List<string>() {
                "bulletman_idle_down_001",
                "bulletman_idle_down_001"
            };
            List<string> IdleUpSpriteList = new List<string>() {
                "bulletman_idle_up_001",
                "bulletman_idle_up_001"
            };
            List<string> IdleLeftSpriteList = new List<string>() {
                "bulletman_idle_left_001",
                "bulletman_idle_left_001"
            };
            List<string> IdleRightSpriteList = new List<string>() {
                "bulletman_idle_right_001",
                "bulletman_idle_right_001"
            };
            
            List<string> MoveLeftSpriteList = new List<string>() {
                "bulletman_run_left_001",
                "bulletman_run_left_001"
            };
            List<string> MoveRightSpriteList = new List<string>() {
                "bulletman_run_right_001",
                "bulletman_run_right_001"
            };
            List<string> MoveDownSpriteList = new List<string>() {
                "bulletman_run_down_001",
                "bulletman_run_down_001"
            };
            List<string> MoveUpSpriteList = new List<string>() {
                "bulletman_run_up_001",
                "bulletman_run_up_001"
            };

            List<string> HitLeftSpriteList = new List<string>() {
                "bulletman_hit_left_001",
                "bulletman_hit_left_001"
            };
            List<string> HitRightSpriteList = new List<string>() {
                "bulletman_hit_right_001",
                "bulletman_hit_right_001"
            };

            List<string> CoverIdleLeftSpriteList = new List<string>() {
                "bulletman_cover_left_idle_001",
                "bulletman_cover_left_idle_001"
            };
            List<string> CoverIdleRightSpriteList = new List<string>() {
                "bulletman_cover_right_idle_001",
                "bulletman_cover_right_idle_001"
            };
            List<string> CoverLeepLeftSpriteList = new List<string>() {
                "bulletman_cover_left_leap_001",
                "bulletman_cover_left_leap_001"
            };
            List<string> CoverLeepRightSpriteList = new List<string>() {
                "bulletman_cover_right_leap_001",
                "bulletman_cover_right_leap_001"
            };

            List<string> SpawnSpriteList = new List<string>() {
                "bulletman_spawn_001",
                "bulletman_spawn_002",
                "bulletman_spawn_003"
            };

            List<string> PitfallSpriteList = new List<string>() {
                "bulletman_pitfall_001",
                "bulletman_pitfall_002",
                "bulletman_pitfall_003",
                "bulletman_pitfall_004",
                "bulletman_pitfall_005",
            };

            ItemAPI.ItemBuilder.AddSpriteToObject(m_CachedTargetObject, (basePath + IdleDownSpriteList[0]), false, isFakeprefab);

            tk2dSprite m_CachedSprite = m_CachedTargetObject.GetComponent<tk2dSprite>();

            foreach (string spriteName in SpriteLIst) { ItemAPI.SpriteBuilder.AddSpriteToCollection((basePath + spriteName), m_CachedSprite.Collection); }
            
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleLeftSpriteList, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleRightSpriteList, "idle_right", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleDownSpriteList, "idle_down", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleUpSpriteList, "idle_up", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveLeftSpriteList, "move_left", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveRightSpriteList, "move_right", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveDownSpriteList, "move_down", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveUpSpriteList, "move_down", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitLeftSpriteList, "hit_left", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitRightSpriteList, "hit_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverIdleLeftSpriteList, "cover_idle_left", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverIdleRightSpriteList, "cover_idle_right", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverLeepLeftSpriteList, "cover_leep_left", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverLeepRightSpriteList, "cover_leep_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, PitfallSpriteList, "pitfall_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, Guid.NewGuid().ToString(), null, instantiateCorpseObject: false, ExternalCorpseObject: m_CachedEnemyActor.CorpseObject);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 1,
                    ManualOffsetY = 1,
                    ManualWidth = 10,
                    ManualHeight = 6,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 1,
                    ManualOffsetY = 1,
                    ManualWidth = 10,
                    ManualHeight = 13,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Default;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "idle",
                    AnimNames = new string[] { "idle_down", "idle_right", "idle_left", "idle_up" },
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "run",
                    AnimNames = new string[] { "move_down", "move_right", "move_left", "move_up" },
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Prefix = "hit",
                    AnimNames = new string[] { "hit_left", "hit_right" },
                    Flipped = new DirectionalAnimation.FlipType[2]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "cover_idle",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                            Prefix = "cover_idle",
                            AnimNames = new string[] { "cover_idle_right", "cover_idle_left" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "cover_leep",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                            Prefix = "cover_leep",
                            AnimNames = new string[] { "cover_leep_right", "cover_leep_left" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall_right", "pitfall_right" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    }
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>() {
                new RedBarrelAwareness() { AvoidRedBarrels = true, ShootRedBarrels = true, PushRedBarrels = true }
            };
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new RideInCartsBehavior(),
                new TakeCoverBehavior() {
                    PathInterval = 0.25f,
                    LineOfSightToLeaveCover = true,
                    MaxCoverDistance = 10,
                    MaxCoverDistanceToTarget = 25,
                    FlipCoverDistance = 0.3f,
                    InsideCoverTime = 2,
                    OutsideCoverTime = 2,
                    PopOutSpeedMultiplier = 3,
                    PopInSpeedMultiplier = 1,
                    InitialCoverChance = 0.5f,
                    RepeatingCoverChance = 0.15f,
                    RepeatingCoverInterval = 1
                },
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 7,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.25f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0.2f,
                    LineOfSight = true,
                    WeaponType = WeaponType.AIShooterProjectile,
                    OverrideBulletName = "default",
                    BulletScript = null,
                    FixTargetDuringAttack = false,
                    StopDuringAttack = false,
                    LeadAmount = 0,
                    LeadChance = 1,
                    RespectReload = true,
                    MagazineCapacity = 6,
                    ReloadSpeed = 2,
                    EmptiesClip = false,
                    SuppressReloadAnim = false,                    
                    TimeBetweenShots = -1,
                    PreventTargetSwitching = false,
                    OverrideAnimation = null,
                    OverrideDirectionalAnimation = null,
                    HideGun = false,
                    UseLaserSight = false,
                    UseGreenLaser = false,
                    PreFireLaserTime = -1,
                    AimAtFacingDirectionWhenSafe = false,
                    Cooldown = 1.6f,
                    CooldownVariance = 0,
                    AttackCooldown = 0,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 12,
                    MinWallDistance = 0,
                    MaxEnemiesInRoom = 0,
                    MinHealthThreshold = 0,
                    MaxHealthThreshold = 1,
                    HealthThresholds = new float[0],
                    AccumulateHealthThresholds = true,
                    targetAreaStyle = null,
                    IsBlackPhantom = false,
                    resetCooldownOnDamage = null,
                    RequiresLineOfSight = false,
                    MaxUsages = 0
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ResourceExtractor.BuildStringListFromEmbeddedResource("BehaviorScripts\\BootlegBulletMan_BehaviorScript.txt");

            BootlegBulletManGUID = m_CachedAIActor.EnemyGuid;

            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, true);
            DontDestroyOnLoad(m_CachedTargetObject);

            m_CachedEnemyActor = null;
            return;
        }       
    }
}

