using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using FullInspector;
using MonoMod.RuntimeDetour;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandComponents;
using Gungeon;

namespace ExpandTheGungeon.ExpandObjects {
    
    public class ExpandCustomEnemyDatabase : EnemyDatabase {

        public static Hook loadEnemyGUIDHook;
        public static Dictionary<string, GameObject> enemyPrefabDictionary = new Dictionary<string, GameObject>();

        // Companions
        public static GameObject HammerCompanionPrefab;
        public static GameObject FriendlyCultistPrefab;

        // Normal Enemies
        public static GameObject RatGrenadePrefab;
        public static GameObject BootlegBullatPrefab;
        public static GameObject BootlegBulletManPrefab;
        public static GameObject BootlegBulletManBandanaPrefab;
        public static GameObject BootlegShotgunManRedPrefab;
        public static GameObject BootlegShotgunManBluePrefab;
        public static GameObject CronenbergPrefab;
        public static GameObject AggressiveCronenbergPrefab;

        // Custom/Modified bosses
        // public static GameObject KillStumpsDummy;
        public static GameObject MonsterParasitePrefab;
        public static GameObject com4nd0BossPrefab;
        
        // Misc Objects
        public static GameObject CronenbergCorpseDebrisObject1;
        public static GameObject CronenbergCorpseDebrisObject2;
        public static GameObject CronenbergCorpseDebrisObject3;
        public static GameObject CronenbergCorpseDebrisObject4;
        public static GameObject AggressiveCronenbergCorpseDebrisObject;

        public static Texture2D[] RatGrenadeTextures;

        // Saved GUIDs for use in things like room prefabs
        public static string RatGrenadeGUID;
        public static string HammerCompanionGUID;
        public static string BootlegBullatGUID;
        public static string BootlegBulletManGUID;
        public static string BootlegBulletManBandanaGUID;
        public static string BootlegShotgunManRedGUID;
        public static string BootlegShotgunManBlueGUID;
        public static string CronenbergGUID;
        public static string AggressiveCronenbergGUID;
        // public static string KillStumpsGUID;
        public static string ParasiteBossGUID;
        public static string com4nd0GUID;
        public static string FriendlyCultistGUID;

        public static void InitPrefabs() {
            // Unlock Gungeon class so I can add my enemies to spawn pool for spawn command.
            HashSet<string> _LockedNamespaces = ReflectionHelpers.ReflectGetField<HashSet<string>>(typeof(IDPool<AIActor>), "_LockedNamespaces", Game.Enemies);
            _LockedNamespaces.Remove("gungeon");


            if (ExpandStats.debugMode) { Debug.Log("[ExpandTheGungeon] Installing EnemyDatabase.GetOrLoadByGuid Hook...."); }
            loadEnemyGUIDHook = new Hook(
                typeof(EnemyDatabase).GetMethod("GetOrLoadByGuid", BindingFlags.Static | BindingFlags.Public),
                typeof(ExpandCustomEnemyDatabase).GetMethod("GetOrLoadByGuidHook", BindingFlags.Static | BindingFlags.Public)
            );

            AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle("ExpandSharedAuto");

            // Real Prefabs
            BuildBabyGoodHammerPrefab(expandSharedAssets1, out HammerCompanionPrefab);
            BuildBootlegBullatPrefab(expandSharedAssets1, out BootlegBullatPrefab);
            BuildBootlegBulletManPrefab(expandSharedAssets1, out BootlegBulletManPrefab);
            BuildBootlegBulletManBandanaPrefab(expandSharedAssets1, out BootlegBulletManBandanaPrefab);
            BuildBootlegShotgunManRedPrefab(expandSharedAssets1, out BootlegShotgunManRedPrefab);
            BuildBootlegShotgunManBluePrefab(expandSharedAssets1, out BootlegShotgunManBluePrefab);
            BuildCronenbergPrefab(expandSharedAssets1, out CronenbergPrefab);
            BuildAggressiveCronenbergPrefab(expandSharedAssets1, out AggressiveCronenbergPrefab);
            BuildCultistCompanionPrefab(expandSharedAssets1, out FriendlyCultistPrefab);

            // Fake Prefabs
            BuildRatGrenadePrefab(out RatGrenadePrefab);
            BuildParasiteBossPrefab(out MonsterParasitePrefab);
            BuildJungleBossPrefab(out com4nd0BossPrefab);

            // Add R&G enemies to MTG spawn command because Zatherz hasn't done it. :P
            UpdateMTGSpawnPool();
            
            Game.Enemies.LockNamespace("gungeon");

            expandSharedAssets1 = null;
        }

        public static void UpdateMTGSpawnPool() {
            // This entry doesn't work. Forge hammers do not have AIActor components.
            if (Game.Enemies.ContainsID("hammer")) { Game.Enemies.Remove("hammer"); }

            List<Tuple<string, string>> FTAEnemyPool = new List<Tuple<string, string>>() {
                new Tuple<string, string>("226fd90be3a64958a5b13cb0a4f43e97", "musket_kin"),
                new Tuple<string, string>("df4e9fedb8764b5a876517431ca67b86", "bullet_kin_gal_titan_boss"),
                new Tuple<string, string>("1f290ea06a4c416cabc52d6b3cf47266", "bullet_kin_titan_boss"),
                new Tuple<string, string>("c4cf0620f71c4678bb8d77929fd4feff", "bullet_kin_titan"),
                new Tuple<string, string>("6f818f482a5c47fd8f38cce101f6566c", "bullet_kin_pirate"),
                new Tuple<string, string>("143be8c9bbb84e3fb3ab98bcd4cf5e5b", "bullet_kin_fish"),
                new Tuple<string, string>("06f5623a351c4f28bc8c6cda56004b80", "bullet_kin_fish_blue"),
                new Tuple<string, string>("ff4f54ce606e4604bf8d467c1279be3e", "bullet_kin_broccoli"),
                new Tuple<string, string>("39e6f47a16ab4c86bec4b12984aece4c", "bullet_kin_knight"),
                new Tuple<string, string>("f020570a42164e2699dcf57cac8a495c", "bullet_kin_kaliber"),
                new Tuple<string, string>("37de0df92697431baa47894a075ba7e9", "bullet_kin_candle"),
                new Tuple<string, string>("5861e5a077244905a8c25c2b7b4d6ebb", "bullet_kin_cowboy"),
                new Tuple<string, string>("906d71ccc1934c02a6f4ff2e9c07c9ec", "bullet_kin_officetie"),
                new Tuple<string, string>("9eba44a0ea6c4ea386ff02286dd0e6bd", "bullet_kin_officesuit"),
                new Tuple<string, string>("2b6854c0849b4b8fb98eb15519d7db1c", "bullet_kin_mech"),
                new Tuple<string, string>("05cb719e0178478685dc610f8b3e8bfc", "bullet_kin_vest"),
                new Tuple<string, string>("5f15093e6f684f4fb09d3e7e697216b4", "dynamite_kin_office"),
                new Tuple<string, string>("d4f4405e0ff34ab483966fd177f2ece3", "cylinder"),
                new Tuple<string, string>("534f1159e7cf4f6aa00aeea92459065e", "cylinder_red"),
                new Tuple<string, string>("80ab6cd15bfc46668a8844b2975c6c26", "gunzookie_office"),
                new Tuple<string, string>("981d358ffc69419bac918ca1bdf0c7f7", "bullat_gargoyle"),
                new Tuple<string, string>("e861e59012954ab2b9b6977da85cb83c", "snake_office"),
                new Tuple<string, string>("41ee1c8538e8474a82a74c4aff99c712", "agunim_helicopter"),
                new Tuple<string, string>("3b0bd258b4c9432db3339665cc61c356", "cactus_kin"),
                new Tuple<string, string>("4b21a913e8c54056bc05cafecf9da880", "gigi_parrot"),
                new Tuple<string, string>("78e0951b097b46d89356f004dda27c42", "tablet_bookllet"),
                new Tuple<string, string>("216fd3dfb9da439d9bd7ba53e1c76462", "necronomicon_bookllet"),
                new Tuple<string, string>("ddf12a4881eb43cfba04f36dd6377abb", "cowboy_shotgun_kin"),
                new Tuple<string, string>("86dfc13486ee4f559189de53cfb84107", "pirate_shotgun_kin"),
                new Tuple<string, string>("9215d1a221904c7386b481a171e52859", "lead_maiden_fridge"),
                new Tuple<string, string>("3f40178e10dc4094a1565cd4fdc4af56", "baby_shelleton")
            };

            foreach (Tuple<string, string> tuple in FTAEnemyPool) {
                if (!Game.Enemies.ContainsID(tuple.Second)) { Game.Enemies.Add(tuple.Second, GetOrLoadByGuid_Orig(tuple.First)); }
            }
        }


        public static void AddEnemyToDatabase(GameObject EnemyPrefab, string EnemyGUID, bool IsNormalEnemy = false, bool AddToMTGSpawnPool = true) {
            EnemyDatabaseEntry item = new EnemyDatabaseEntry {
                myGuid = EnemyGUID,
                placeableWidth = 2,
                placeableHeight = 2,
                isNormalEnemy = IsNormalEnemy
            };
            Instance.Entries.Add(item);
            enemyPrefabDictionary.Add(EnemyGUID, EnemyPrefab);
            if (AddToMTGSpawnPool && !string.IsNullOrEmpty(EnemyPrefab.GetComponent<AIActor>().ActorName)) {
                string EnemyName = EnemyPrefab.GetComponent<AIActor>().ActorName.Replace(" ", "_").Replace("(", "_").Replace(")", string.Empty).ToLower();
                if (!Game.Enemies.ContainsID(EnemyName)) { Game.Enemies.Add(EnemyName, EnemyPrefab.GetComponent<AIActor>()); }
            }
        }
        
        public static AIActor GetOrLoadByGuidHook(Func<string, AIActor> orig, string guid) {
            foreach (string text in enemyPrefabDictionary.Keys) {
                if (text == guid) { return enemyPrefabDictionary[text].GetComponent<AIActor>(); }
            }
            return orig(guid);
        }

        public static AIActor GetOrLoadByGuid_Orig(string guid) { return Instance.InternalGetByGuid(guid); }


        public static void BuildRatGrenadePrefab(out GameObject m_CachedTargetObject, bool isFakePrefab = true) {
            m_CachedTargetObject = Instantiate(GetOrLoadByGuid_Orig("14ea47ff46b54bb4a98f91ffcffb656d").gameObject);
            m_CachedTargetObject.SetActive(false);
            m_CachedTargetObject.name = "Greande Rat";

            ExpandExplodeOnDeath RatExplodeComponent = m_CachedTargetObject.AddComponent<ExpandExplodeOnDeath>();
            RatExplodeComponent.deathType = OnDeathBehavior.DeathType.Death;

            AIActor m_CachedAIActor = RatGrenadePrefab.GetComponent<AIActor>();
            m_CachedAIActor.OverrideDisplayName = "Grenade Rat";
            m_CachedAIActor.EnemyGuid = "1a1dc5ed-92a6-4bd1-bbee-098991e7d2d4";
            m_CachedAIActor.EnemyId = UnityEngine.Random.Range(10000, 100000);
            m_CachedAIActor.CorpseObject = null;
            Destroy(m_CachedAIActor.gameObject.GetComponent<EncounterTrackable>());
            
            tk2dSprite m_Sprite = m_CachedTargetObject.GetComponent<tk2dSprite>();
            tk2dSpriteAnimator m_SpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            m_Sprite.Collection = ExpandPrefabs.MouseTrap1.GetComponent<tk2dSprite>().Collection;
            m_SpriteAnimator.Library = ExpandPrefabs.MouseTrap1.GetComponent<tk2dSpriteAnimator>().Library;
            m_SpriteAnimator.DefaultClipId = 19;
            m_Sprite.SetSprite("rat_grenade_move_right_001");

            AIAnimator m_AIAnimator = m_CachedTargetObject.GetComponent<AIAnimator>();
            m_AIAnimator.IdleAnimation.Prefix = "rat_grenade_idle";
            m_AIAnimator.MoveAnimation.Prefix = "rat_grenade_move";
            m_AIAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>(0);
                        
            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = m_CachedTargetObject.GetComponent<BehaviorSpeculator>();
            if (m_TargetBehaviorSpeculatorSeralized != null) {
                m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
                m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
                // Loading a custom script from text file in place of one from an existing prefab..
                m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\GrenadeRat_BehaviorScript.txt");
            }
            
            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, true);
            
            if (isFakePrefab) { FakePrefab.MarkAsFakePrefab(m_CachedTargetObject); }
            DontDestroyOnLoad(m_CachedTargetObject);

            RatGrenadeGUID = m_CachedAIActor.EnemyGuid;
        }

        public static void BuildBabyGoodHammerPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            AIActor m_CachedEnemyActor = GetOrLoadByGuid_Orig("01972dee89fc4404a5c408d50007dad5");

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("EXBabyGoodHammer");
                        
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

            ItemBuilder.AddSpriteToObject(m_CachedTargetObject, expandSharedAssets1.LoadAsset<Texture2D>("babygoodhammer_idle_down_01"), false, false);

            tk2dSprite m_CachedSprite = m_CachedTargetObject.GetComponent<tk2dSprite>();

            foreach (string spriteName in IdleSpriteList) {
                if (spriteName != "babygoodhammer_idle_down_01") {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_CachedSprite.Collection);
                }
            }
            foreach (string spriteName in MoveLeftSpriteList) { SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_CachedSprite.Collection); }
            foreach (string spriteName in MoveRightSpriteList) { SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_CachedSprite.Collection); }

            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, false, false, false, true, ClipFps: 6);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleSpriteList, "Hammer_Idle_Down", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveLeftSpriteList, "Hammer_Move_Left", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveRightSpriteList, "Hammer_Move_Right", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            // tk2dSprite newSprite = m_CachedTargetObject.AddComponent<tk2dSprite>();
            // ExpandUtility.DuplicateSprite(newSprite, (m_SelectedPlayer.sprite as tk2dSprite));


            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, "05145e1a-1a10-4797-b37e-a15bb26d7641", null, instantiateCorpseObject: false, ExternalCorpseObject: m_CachedEnemyActor.CorpseObject, EnemyHasNoShooter: true);

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
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\BabyGoodHammer_BehaviorScript.txt");

            ExpandUtility.MakeCompanion(m_CachedAIActor, null, null, true, false, true, true);

            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, false);
            BabyGoodHammer.CompanionGuid = m_CachedAIActor.EnemyGuid;

            m_CachedEnemyActor = null;
            return;
        }

        public static void BuildBootlegBullatPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Bootleg Bullat");
            
            List<string> SpriteList = new List<string>() {
                "bullat_idle_001",
                "bullat_idle_002",
                "bullat_idle_003",
                "bullat_idle_004",
                "bullat_idle_005",
                "bullat_idle_006",
                "bullat_die_001",
                "bullat_die_002",
                "bullat_die_003",
                "bullat_die_004"
            };

            List<string> IdleSpriteList = new List<string>() {
                "bullat_idle_001",
                "bullat_idle_002",
                "bullat_idle_003",
                "bullat_idle_004",
                "bullat_idle_005",
                "bullat_idle_006",
            };
            
            List<string> DieSpriteList = new List<string>() {
                "bullat_die_001",
                "bullat_die_002",
                "bullat_die_003",
                "bullat_die_004"
            };

            ItemBuilder.AddSpriteToObject(m_CachedTargetObject, expandSharedAssets1.LoadAsset<Texture2D>(IdleSpriteList[0]), false, false);

            tk2dSprite m_CachedSprite = m_CachedTargetObject.GetComponent<tk2dSprite>();

            foreach (string spriteName in SpriteList) {
                if (spriteName != IdleSpriteList[0]) {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_CachedSprite.Collection);
                }
            }
            
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleSpriteList, "idle", tk2dSpriteAnimationClip.WrapMode.Loop, 10);                                    
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DieSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            
            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, "7ef020b9-11fb-4a24-a818-60581e6d3105", null, instantiateCorpseObject: false, EnemyHasNoShooter: true, EnemyHasNoCorpse: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();
            
            m_CachedAIActor.MovementSpeed = 4;
            m_CachedAIActor.SetIsFlying(true, "Flying Enemy", true, true);
            m_CachedAIActor.procedurallyOutlined = false;
            m_CachedAIActor.EnemySwitchState = "Bullet_Bat";
            m_CachedAIActor.healthHaver.SetHealthMaximum(1);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(1);
            m_CachedAIActor.DiesOnCollison = true;

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 8,
                    ManualOffsetY = 3,
                    ManualWidth = 13,
                    ManualHeight = 8,
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
                    ManualOffsetX = 5,
                    ManualOffsetY = 2,
                    ManualWidth = 19,
                    ManualHeight = 10,
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
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "idle",
                    AnimNames = new string[] { string.Empty },
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.None,
                    Prefix = string.Empty,
                    AnimNames = new string[0],
                    Flipped = new DirectionalAnimation.FlipType[0]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.None,
                    Prefix = string.Empty,
                    AnimNames = new string[0],
                    Flipped = new DirectionalAnimation.FlipType[0]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[] { string.Empty },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() { Radius = 35, LineOfSight = false,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new SeekTargetBehavior() {
                    StopWhenInRange = false,
                    CustomRange = 6,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
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
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\BootlegBullat_BehaviorScript.txt");

            BootlegBullatGUID = m_CachedAIActor.EnemyGuid;

            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, true);
            return;
        }

        public static void BuildBootlegBulletManPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedEnemyActor = GetOrLoadByGuid_Orig("01972dee89fc4404a5c408d50007dad5"); //bullet_kin
            
            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Bootleg BulletMan");

            
            List<string> SpriteList = new List<string>() {
                "bulletman_cover_left_idle_001",
                "bulletman_cover_left_leap_001",
                "bulletman_cover_right_idle_001",
                "bulletman_cover_right_leap_001",
                "bulletman_hit_left_001",
                "bulletman_hit_left_002",
                "bulletman_hit_left_003",
                "bulletman_hit_left_004",
                "bulletman_hit_left_005",
                "bulletman_hit_right_001",
                "bulletman_hit_right_002",
                "bulletman_hit_right_003",
                "bulletman_hit_right_004",
                "bulletman_hit_right_005",
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
                "bulletman_run_down_002",
                "bulletman_run_down_003",
                "bulletman_run_down_004",
                "bulletman_run_down_005",
                "bulletman_run_left_001",
                "bulletman_run_left_002",
                "bulletman_run_left_003",
                "bulletman_run_left_004",
                "bulletman_run_left_005",
                "bulletman_run_right_001",
                "bulletman_run_right_002",
                "bulletman_run_right_003",
                "bulletman_run_right_004",
                "bulletman_run_right_005",
                "bulletman_run_up_001",
                "bulletman_run_up_002",
                "bulletman_run_up_003",
                "bulletman_run_up_004",
                "bulletman_run_up_005",
                "bulletman_spawn_001",
                "bulletman_spawn_002",
                "bulletman_spawn_003",
                "bulletman_corpse"
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
                "bulletman_run_left_002",
                "bulletman_run_left_003",
                "bulletman_run_left_004",
                "bulletman_run_left_005"
            };
            List<string> MoveRightSpriteList = new List<string>() {
                "bulletman_run_right_001",
                "bulletman_run_right_002",
                "bulletman_run_right_003",
                "bulletman_run_right_004",
                "bulletman_run_right_005",
            };
            List<string> MoveDownSpriteList = new List<string>() {
                "bulletman_run_down_001",
                "bulletman_run_down_002",
                "bulletman_run_down_003",
                "bulletman_run_down_004",
                "bulletman_run_down_005"
            };
            List<string> MoveUpSpriteList = new List<string>() {
                "bulletman_run_up_001",
                "bulletman_run_up_002",
                "bulletman_run_up_003",
                "bulletman_run_up_004",
                "bulletman_run_up_005",
            };

            List<string> HitLeftSpriteList = new List<string>() {
                "bulletman_hit_left_001",
                "bulletman_hit_left_002",
                "bulletman_hit_left_003",
                "bulletman_hit_left_004",
                "bulletman_hit_left_005"
            };
            List<string> HitRightSpriteList = new List<string>() {
                "bulletman_hit_right_001",
                "bulletman_hit_right_002",
                "bulletman_hit_right_003",
                "bulletman_hit_right_004",
                "bulletman_hit_right_005"
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

            List<string> DeathSpriteList = new List<string>() { "bulletman_corpse", "bulletman_corpse" };

            ItemBuilder.AddSpriteToObject(m_CachedTargetObject, expandSharedAssets1.LoadAsset<Texture2D>(IdleDownSpriteList[0]), false, false);

            tk2dSprite m_CachedSprite = m_CachedTargetObject.GetComponent<tk2dSprite>();

            foreach (string spriteName in SpriteList) {
                if (spriteName != IdleDownSpriteList[0]) {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_CachedSprite.Collection);
                }
            }

            // BootlegBulletManCorpse.GetComponent<tk2dSprite>().Collection = m_CachedSprite.Collection;

            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleLeftSpriteList, "idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleRightSpriteList, "idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleDownSpriteList, "idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleUpSpriteList, "idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveLeftSpriteList, "run_west", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveRightSpriteList, "run_east", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveDownSpriteList, "run_south", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveUpSpriteList, "run_north", tk2dSpriteAnimationClip.WrapMode.Loop, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitLeftSpriteList, "hit_west", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitRightSpriteList, "hit_east", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitLeftSpriteList, "hit_north", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitRightSpriteList, "hit_south", tk2dSpriteAnimationClip.WrapMode.Once, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverIdleLeftSpriteList, "cover_idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverIdleRightSpriteList, "cover_idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverIdleLeftSpriteList, "cover_idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverIdleRightSpriteList, "cover_idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverLeepLeftSpriteList, "cover_leep_west", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverLeepRightSpriteList, "cover_leep_east", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverLeepLeftSpriteList, "cover_leep_north", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverLeepRightSpriteList, "cover_leep_south", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleDownSpriteList, "awaken", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, PitfallSpriteList, "pitfall_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DeathSpriteList, "death", tk2dSpriteAnimationClip.WrapMode.Once, 8);


            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;

            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, m_CachedEnemyActor.aiShooter, m_CachedEnemyActor.GetComponent<AIBulletBank>(), BootlegGuns.BootlegPistolID, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, "a52cfba8-f141-4a98-9022-48816201f834", null, instantiateCorpseObject: false, ExternalCorpseObject: GetOrLoadByGuid_Orig("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.aiShooter.handObject = null;
            m_CachedAIActor.procedurallyOutlined = false;

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
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "idle",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "run",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "hit",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "cover_idle",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                            Prefix = "cover_idle",
                            AnimNames = new string[4],
                            Flipped = new DirectionalAnimation.FlipType[4]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "cover_leep",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                            Prefix = "cover_leep",
                            AnimNames = new string[4],
                            Flipped = new DirectionalAnimation.FlipType[4]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall_right" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[] { "die" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "death",
                            AnimNames = new string[] { "death" },
                            Flipped = new DirectionalAnimation.FlipType[1]
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
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\BootlegBulletMan_BehaviorScript.txt");

            BootlegBulletManGUID = m_CachedAIActor.EnemyGuid;

            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, true);
            // DontDestroyOnLoad(m_CachedTargetObject);

            m_CachedEnemyActor = null;
            return;
        }

        public static void BuildBootlegBulletManBandanaPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            AIActor m_CachedEnemyActor = GetOrLoadByGuid_Orig("88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Bootleg BulletMan Bandana");

            
            List<string> SpriteList = new List<string>() {
                "bulletmanbandana_hit_left_001",
                "bulletmanbandana_hit_left_002",
                "bulletmanbandana_hit_left_003",
                "bulletmanbandana_hit_left_004",
                "bulletmanbandana_hit_left_005",
                "bulletmanbandana_hit_right_001",
                "bulletmanbandana_hit_right_002",
                "bulletmanbandana_hit_right_003",
                "bulletmanbandana_hit_right_004",
                "bulletmanbandana_hit_right_005",
                "bulletmanbandana_idle_down_001",
                "bulletmanbandana_idle_left_001",
                "bulletmanbandana_idle_right_001",
                "bulletmanbandana_idle_up_001",
                "bulletmanbandana_pitfall_001",
                "bulletmanbandana_pitfall_002",
                "bulletmanbandana_pitfall_003",
                "bulletmanbandana_pitfall_004",
                "bulletmanbandana_pitfall_005",
                "bulletmanbandana_run_down_001",
                "bulletmanbandana_run_down_002",
                "bulletmanbandana_run_down_003",
                "bulletmanbandana_run_down_004",
                "bulletmanbandana_run_down_005",
                "bulletmanbandana_run_left_001",
                "bulletmanbandana_run_left_002",
                "bulletmanbandana_run_left_003",
                "bulletmanbandana_run_left_004",
                "bulletmanbandana_run_left_005",
                "bulletmanbandana_run_right_001",
                "bulletmanbandana_run_right_002",
                "bulletmanbandana_run_right_003",
                "bulletmanbandana_run_right_004",
                "bulletmanbandana_run_right_005",
                "bulletmanbandana_run_up_001",
                "bulletmanbandana_run_up_002",
                "bulletmanbandana_run_up_003",
                "bulletmanbandana_run_up_004",
                "bulletmanbandana_run_up_005",
                "bulletmanbandana_spawn_001",
                "bulletmanbandana_spawn_002",
                "bulletmanbandana_spawn_003",
                "bulletmanbandana_corpse"
            };

            List<string> IdleDownSpriteList = new List<string>() {
                "bulletmanbandana_idle_down_001",
                "bulletmanbandana_idle_down_001"
            };
            List<string> IdleUpSpriteList = new List<string>() {
                "bulletmanbandana_idle_up_001",
                "bulletmanbandana_idle_up_001"
            };
            List<string> IdleLeftSpriteList = new List<string>() {
                "bulletmanbandana_idle_left_001",
                "bulletmanbandana_idle_left_001"
            };
            List<string> IdleRightSpriteList = new List<string>() {
                "bulletmanbandana_idle_right_001",
                "bulletmanbandana_idle_right_001"
            };
            
            List<string> MoveLeftSpriteList = new List<string>() {
                "bulletmanbandana_run_left_001",
                "bulletmanbandana_run_left_002",
                "bulletmanbandana_run_left_003",
                "bulletmanbandana_run_left_004",
                "bulletmanbandana_run_left_005"
            };
            List<string> MoveRightSpriteList = new List<string>() {
                "bulletmanbandana_run_right_001",
                "bulletmanbandana_run_right_002",
                "bulletmanbandana_run_right_003",
                "bulletmanbandana_run_right_004",
                "bulletmanbandana_run_right_005",
            };
            List<string> MoveDownSpriteList = new List<string>() {
                "bulletmanbandana_run_down_001",
                "bulletmanbandana_run_down_002",
                "bulletmanbandana_run_down_003",
                "bulletmanbandana_run_down_004",
                "bulletmanbandana_run_down_005"
            };
            List<string> MoveUpSpriteList = new List<string>() {
                "bulletmanbandana_run_up_001",
                "bulletmanbandana_run_up_002",
                "bulletmanbandana_run_up_003",
                "bulletmanbandana_run_up_004",
                "bulletmanbandana_run_up_005",
            };

            List<string> HitLeftSpriteList = new List<string>() {
                "bulletmanbandana_hit_left_001",
                "bulletmanbandana_hit_left_002",
                "bulletmanbandana_hit_left_003",
                "bulletmanbandana_hit_left_004",
                "bulletmanbandana_hit_left_005"
            };
            List<string> HitRightSpriteList = new List<string>() {
                "bulletmanbandana_hit_right_001",
                "bulletmanbandana_hit_right_002",
                "bulletmanbandana_hit_right_003",
                "bulletmanbandana_hit_right_004",
                "bulletmanbandana_hit_right_005"
            };

            List<string> CoverIdleLeftSpriteList = new List<string>() {
                "bulletmanbandana_idle_left_001",
                "bulletmanbandana_idle_left_001"
            };
            List<string> CoverIdleRightSpriteList = new List<string>() {
                "bulletmanbandana_idle_right_001",
                "bulletmanbandana_idle_right_001"
            };
            List<string> CoverLeepLeftSpriteList = new List<string>() {
                "bulletmanbandana_idle_left_001",
                "bulletmanbandana_idle_left_001"
            };
            List<string> CoverLeepRightSpriteList = new List<string>() {
                "bulletmanbandana_idle_right_001",
                "bulletmanbandana_idle_right_001"
            };

            List<string> SpawnSpriteList = new List<string>() {
                "bulletmanbandana_spawn_001",
                "bulletmanbandana_spawn_002",
                "bulletmanbandana_spawn_003"
            };

            List<string> PitfallSpriteList = new List<string>() {
                "bulletmanbandana_pitfall_001",
                "bulletmanbandana_pitfall_002",
                "bulletmanbandana_pitfall_003",
                "bulletmanbandana_pitfall_004",
                "bulletmanbandana_pitfall_005",
            };

            List<string> DeathSpriteList = new List<string>() { "bulletmanbandana_corpse", "bulletmanbandana_corpse" };

            ItemBuilder.AddSpriteToObject(m_CachedTargetObject, expandSharedAssets1.LoadAsset<Texture2D>(IdleDownSpriteList[0]), false, false);

            tk2dSprite m_CachedSprite = m_CachedTargetObject.GetComponent<tk2dSprite>();

            foreach (string spriteName in SpriteList) {
                if (spriteName != IdleDownSpriteList[0]) {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_CachedSprite.Collection);
                }
            }
            
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleLeftSpriteList, "idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleRightSpriteList, "idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleDownSpriteList, "idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleUpSpriteList, "idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveLeftSpriteList, "run_west", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveRightSpriteList, "run_east", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveDownSpriteList, "run_south", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveUpSpriteList, "run_north", tk2dSpriteAnimationClip.WrapMode.Loop, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitLeftSpriteList, "hit_west", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitRightSpriteList, "hit_east", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitLeftSpriteList, "hit_north", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitRightSpriteList, "hit_south", tk2dSpriteAnimationClip.WrapMode.Once, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverIdleLeftSpriteList, "cover_idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverIdleRightSpriteList, "cover_idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverIdleLeftSpriteList, "cover_idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverIdleRightSpriteList, "cover_idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverLeepLeftSpriteList, "cover_leep_west", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverLeepRightSpriteList, "cover_leep_east", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverLeepLeftSpriteList, "cover_leep_north", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, CoverLeepRightSpriteList, "cover_leep_south", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleDownSpriteList, "awaken", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, PitfallSpriteList, "pitfall_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DeathSpriteList, "death", tk2dSpriteAnimationClip.WrapMode.Once, 8);

            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;
                        
            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, m_CachedEnemyActor.aiShooter, m_CachedEnemyActor.GetComponent<AIBulletBank>(), BootlegGuns.BootlegMachinePistolID, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, "7093253e-a118-4813-8feb-703a1ad31665", null, instantiateCorpseObject: false, ExternalCorpseObject: GetOrLoadByGuid_Orig("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.aiShooter.handObject = null;
            m_CachedAIActor.procedurallyOutlined = false;

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 2,
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
                    ManualOffsetX = 2,
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
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "idle",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "run",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "hit",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "cover_idle",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                            Prefix = "cover_idle",
                            AnimNames = new string[4],
                            Flipped = new DirectionalAnimation.FlipType[4]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "cover_leep",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                            Prefix = "cover_leep",
                            AnimNames = new string[4],
                            Flipped = new DirectionalAnimation.FlipType[4]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall_right" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[] { "die" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "death",
                            AnimNames = new string[] { "death" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
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
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 6,
                    LineOfSight = false,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0.2f,
                    LineOfSight = false,
                    WeaponType = WeaponType.AIShooterProjectile,
                    OverrideBulletName = null,
                    BulletScript = null,
                    FixTargetDuringAttack = false,
                    StopDuringAttack = false,
                    LeadAmount = 0,
                    LeadChance = 1,
                    RespectReload = true,
                    MagazineCapacity = 8,
                    ReloadSpeed = 2,
                    EmptiesClip = true,
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
                    Cooldown = 0.2f,
                    CooldownVariance = 0,
                    AttackCooldown = 0,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 16,
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
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\BootlegBulletManBandana_BehaviorScript.txt");

            BootlegBulletManBandanaGUID = m_CachedAIActor.EnemyGuid;

            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, true);
            
            m_CachedEnemyActor = null;
            return;
        }

        public static void BuildBootlegShotgunManRedPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            AIActor m_CachedEnemyActor = GetOrLoadByGuid_Orig("128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Bootleg ShotgunMan Red");
                        
            List<string> SpriteList = new List<string>() {
                "shotgunman_red_up",
                "shotgunman_red_down",
                "shotgunman_red_left",
                "shotgunman_red_right",
                "shotgunman_red_move_down_001",
                "shotgunman_red_move_down_002",
                "shotgunman_red_move_down_003",
                "shotgunman_red_move_down_004",
                "shotgunman_red_move_down_005",
                "shotgunman_red_move_left_001",
                "shotgunman_red_move_left_002",
                "shotgunman_red_move_left_003",
                "shotgunman_red_move_left_004",
                "shotgunman_red_move_left_005",
                "shotgunman_red_move_right_001",
                "shotgunman_red_move_right_002",
                "shotgunman_red_move_right_003",
                "shotgunman_red_move_right_004",
                "shotgunman_red_move_right_005",
                "shotgunman_red_move_up_001",
                "shotgunman_red_move_up_002",
                "shotgunman_red_move_up_003",
                "shotgunman_red_move_up_004",
                "shotgunman_red_move_up_005",
                "shotgunman_red_pitfall_001",
                "shotgunman_red_pitfall_002",
                "shotgunman_red_pitfall_003",
                "shotgunman_red_pitfall_004",
                "shotgunman_red_pitfall_005",
                "shotgunman_red_spawn_001",
                "shotgunman_red_spawn_002",
                "shotgunman_red_spawn_003",
                "shotgunman_red_hit_left",
                "shotgunman_red_hit_right",
                "shotgunman_red_corpse"
            };

            List<string> IdleDownSpriteList = new List<string>() {
                "shotgunman_red_down",
                "shotgunman_red_down"
            };
            List<string> IdleUpSpriteList = new List<string>() {
                "shotgunman_red_up",
                "shotgunman_red_up"
            };
            List<string> IdleLeftSpriteList = new List<string>() {
                "shotgunman_red_left",
                "shotgunman_red_left"
            };
            List<string> IdleRightSpriteList = new List<string>() {
                "shotgunman_red_right",
                "shotgunman_red_right"
            };
            
            List<string> MoveLeftSpriteList = new List<string>() {
                "shotgunman_red_move_left_001",
                "shotgunman_red_move_left_002",
                "shotgunman_red_move_left_003",
                "shotgunman_red_move_left_004",
                "shotgunman_red_move_left_005"
            };
            List<string> MoveRightSpriteList = new List<string>() {
                "shotgunman_red_move_right_001",
                "shotgunman_red_move_right_002",
                "shotgunman_red_move_right_003",
                "shotgunman_red_move_right_004",
                "shotgunman_red_move_right_005",
            };
            List<string> MoveDownSpriteList = new List<string>() {
                "shotgunman_red_move_down_001",
                "shotgunman_red_move_down_002",
                "shotgunman_red_move_down_003",
                "shotgunman_red_move_down_004",
                "shotgunman_red_move_down_005"
            };
            List<string> MoveUpSpriteList = new List<string>() {
                "shotgunman_red_move_up_001",
                "shotgunman_red_move_up_002",
                "shotgunman_red_move_up_003",
                "shotgunman_red_move_up_004",
                "shotgunman_red_move_up_005",
            };
            List<string> HitLeftSpriteList = new List<string>() { "shotgunman_red_hit_left", "shotgunman_red_hit_left" };
            List<string> HitRightSpriteList = new List<string>() { "shotgunman_red_hit_right", "shotgunman_red_hit_right" };                        
            List<string> SpawnSpriteList = new List<string>() {
                "shotgunman_red_spawn_001",
                "shotgunman_red_spawn_002",
                "shotgunman_red_spawn_003"
            };
            List<string> PitfallSpriteList = new List<string>() {
                "shotgunman_red_pitfall_001",
                "shotgunman_red_pitfall_002",
                "shotgunman_red_pitfall_003",
                "shotgunman_red_pitfall_004",
                "shotgunman_red_pitfall_005",
            };
            
            List<string> DeathSpriteList = new List<string>() { "shotgunman_red_corpse", "shotgunman_red_corpse" };

            ItemBuilder.AddSpriteToObject(m_CachedTargetObject, expandSharedAssets1.LoadAsset<Texture2D>(IdleDownSpriteList[0]), false, false);

            tk2dSprite m_CachedSprite = m_CachedTargetObject.GetComponent<tk2dSprite>();

            foreach (string spriteName in SpriteList) {
                if (spriteName != IdleDownSpriteList[0]) {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_CachedSprite.Collection);
                }
            }
                        
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleLeftSpriteList, "idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleRightSpriteList, "idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleDownSpriteList, "idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleUpSpriteList, "idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveLeftSpriteList, "move_west", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveRightSpriteList, "move_east", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveDownSpriteList, "move_south", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveUpSpriteList, "move_north", tk2dSpriteAnimationClip.WrapMode.Loop, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitLeftSpriteList, "hit_west", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitRightSpriteList, "hit_east", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitLeftSpriteList, "hit_north", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitRightSpriteList, "hit_south", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, PitfallSpriteList, "pitfall_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DeathSpriteList, "death", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            
            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;
            
            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, m_CachedEnemyActor.aiShooter, m_CachedEnemyActor.GetComponent<AIBulletBank>(), BootlegGuns.BootlegShotgunID, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, "01e4e238-89bb-4b30-b93a-ae17dc19e748", null, instantiateCorpseObject: false, ExternalCorpseObject: GetOrLoadByGuid_Orig("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.aiShooter.handObject = null;
            m_CachedAIActor.procedurallyOutlined = false;
            m_CachedAIActor.EnemySwitchState = "Plastic_Bullet_Man";
            m_CachedAIActor.healthHaver.SetHealthMaximum(30);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(30);
            m_CachedAIActor.healthHaver.damagedAudioEvent = "Play_CHR_plasticBullet_hurt_01";
            
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
                    ManualWidth = 12,
                    ManualHeight = 12,
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
                    ManualWidth = 12,
                    ManualHeight = 24,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Target;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = true;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "idle",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "move",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "hit",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall_right" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[] { "die" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "death",
                            AnimNames = new string[] { "death" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
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
                new RideInCartsBehavior(),
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 6,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0.2f,
                    LineOfSight = true,
                    WeaponType = WeaponType.BulletScript,
                    OverrideBulletName = null,
                    BulletScript = new BulletScriptSelector() { scriptTypeName = "BulletShotgunManRedBasicAttack1" },
                    FixTargetDuringAttack = false,
                    StopDuringAttack = true,
                    LeadAmount = 0,
                    LeadChance = 1,
                    RespectReload = true,
                    MagazineCapacity = 1,
                    ReloadSpeed = 3,
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
                    Cooldown = 3.5f,
                    CooldownVariance = 0,
                    AttackCooldown = 0,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 20,
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
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\BootlegShotgunManRed_BehaviorScript.txt");

            BootlegShotgunManRedGUID = m_CachedAIActor.EnemyGuid;

            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, true);
            
            m_CachedEnemyActor = null;
            return;
        }

        public static void BuildBootlegShotgunManBluePrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            AIActor m_CachedEnemyActor = GetOrLoadByGuid_Orig("b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Bootleg ShotgunMan Blue");
            
            List<string> SpriteList = new List<string>() {
                "shotgunman_blue_up",
                "shotgunman_blue_down",
                "shotgunman_blue_left",
                "shotgunman_blue_right",
                "shotgunman_blue_move_down_001",
                "shotgunman_blue_move_down_002",
                "shotgunman_blue_move_down_003",
                "shotgunman_blue_move_down_004",
                "shotgunman_blue_move_down_005",
                "shotgunman_blue_move_left_001",
                "shotgunman_blue_move_left_002",
                "shotgunman_blue_move_left_003",
                "shotgunman_blue_move_left_004",
                "shotgunman_blue_move_left_005",
                "shotgunman_blue_move_right_001",
                "shotgunman_blue_move_right_002",
                "shotgunman_blue_move_right_003",
                "shotgunman_blue_move_right_004",
                "shotgunman_blue_move_right_005",
                "shotgunman_blue_move_up_001",
                "shotgunman_blue_move_up_002",
                "shotgunman_blue_move_up_003",
                "shotgunman_blue_move_up_004",
                "shotgunman_blue_move_up_005",
                "shotgunman_blue_pitfall_001",
                "shotgunman_blue_pitfall_002",
                "shotgunman_blue_pitfall_003",
                "shotgunman_blue_pitfall_004",
                "shotgunman_blue_pitfall_005",
                "shotgunman_blue_spawn_001",
                "shotgunman_blue_spawn_002",
                "shotgunman_blue_spawn_003",
                "shotgunman_blue_hit_left",
                "shotgunman_blue_hit_right",
                "shotgunman_blue_corpse"
            };

            List<string> IdleDownSpriteList = new List<string>() {
                "shotgunman_blue_down",
                "shotgunman_blue_down"
            };
            List<string> IdleUpSpriteList = new List<string>() {
                "shotgunman_blue_up",
                "shotgunman_blue_up"
            };
            List<string> IdleLeftSpriteList = new List<string>() {
                "shotgunman_blue_left",
                "shotgunman_blue_left"
            };
            List<string> IdleRightSpriteList = new List<string>() {
                "shotgunman_blue_right",
                "shotgunman_blue_right"
            };
            
            List<string> MoveLeftSpriteList = new List<string>() {
                "shotgunman_blue_move_left_001",
                "shotgunman_blue_move_left_002",
                "shotgunman_blue_move_left_003",
                "shotgunman_blue_move_left_004",
                "shotgunman_blue_move_left_005"
            };
            List<string> MoveRightSpriteList = new List<string>() {
                "shotgunman_blue_move_right_001",
                "shotgunman_blue_move_right_002",
                "shotgunman_blue_move_right_003",
                "shotgunman_blue_move_right_004",
                "shotgunman_blue_move_right_005",
            };
            List<string> MoveDownSpriteList = new List<string>() {
                "shotgunman_blue_move_down_001",
                "shotgunman_blue_move_down_002",
                "shotgunman_blue_move_down_003",
                "shotgunman_blue_move_down_004",
                "shotgunman_blue_move_down_005"
            };
            List<string> MoveUpSpriteList = new List<string>() {
                "shotgunman_blue_move_up_001",
                "shotgunman_blue_move_up_002",
                "shotgunman_blue_move_up_003",
                "shotgunman_blue_move_up_004",
                "shotgunman_blue_move_up_005",
            };
            List<string> HitLeftSpriteList = new List<string>() { "shotgunman_blue_hit_left", "shotgunman_blue_hit_left" };
            List<string> HitRightSpriteList = new List<string>() { "shotgunman_blue_hit_right", "shotgunman_blue_hit_right" };                        
            List<string> SpawnSpriteList = new List<string>() {
                "shotgunman_blue_spawn_001",
                "shotgunman_blue_spawn_002",
                "shotgunman_blue_spawn_003"
            };
            List<string> PitfallSpriteList = new List<string>() {
                "shotgunman_blue_pitfall_001",
                "shotgunman_blue_pitfall_002",
                "shotgunman_blue_pitfall_003",
                "shotgunman_blue_pitfall_004",
                "shotgunman_blue_pitfall_005",
            };
            
            List<string> DeathSpriteList = new List<string>() { "shotgunman_blue_corpse", "shotgunman_blue_corpse" };

            ItemBuilder.AddSpriteToObject(m_CachedTargetObject, expandSharedAssets1.LoadAsset<Texture2D>(IdleDownSpriteList[0]), false, false);

            tk2dSprite m_CachedSprite = m_CachedTargetObject.GetComponent<tk2dSprite>();

            foreach (string spriteName in SpriteList) {
                if (spriteName != IdleDownSpriteList[0]) {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_CachedSprite.Collection);
                }
            }

            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleLeftSpriteList, "idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleRightSpriteList, "idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleDownSpriteList, "idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleUpSpriteList, "idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveLeftSpriteList, "move_west", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveRightSpriteList, "move_east", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveDownSpriteList, "move_south", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveUpSpriteList, "move_north", tk2dSpriteAnimationClip.WrapMode.Loop, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitLeftSpriteList, "hit_west", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitRightSpriteList, "hit_east", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitLeftSpriteList, "hit_north", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, HitRightSpriteList, "hit_south", tk2dSpriteAnimationClip.WrapMode.Once, 4);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, PitfallSpriteList, "pitfall_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DeathSpriteList, "death", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            
            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;
            
            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, m_CachedEnemyActor.aiShooter, m_CachedEnemyActor.GetComponent<AIBulletBank>(), BootlegGuns.BootlegShotgunID, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, "f7c0b0ab-3d80-4855-9fd6-38861af1147a", null, instantiateCorpseObject: false, ExternalCorpseObject: GetOrLoadByGuid_Orig("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandStats.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.aiShooter.handObject = null;
            m_CachedAIActor.procedurallyOutlined = false;
            m_CachedAIActor.EnemySwitchState = "Plastic_Bullet_Man";
            m_CachedAIActor.healthHaver.SetHealthMaximum(40);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(40);
            m_CachedAIActor.healthHaver.damagedAudioEvent = "Play_CHR_plasticBullet_hurt_01";
            
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
                    ManualWidth = 12,
                    ManualHeight = 12,
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
                    ManualWidth = 12,
                    ManualHeight = 24,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Target;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = true;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "idle",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "move",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "hit",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall_right" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[] { "die" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "death",
                            AnimNames = new string[] { "death" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
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
                new RideInCartsBehavior(),
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 6,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0.2f,
                    LineOfSight = true,
                    WeaponType = WeaponType.BulletScript,
                    OverrideBulletName = null,
                    BulletScript = new BulletScriptSelector() { scriptTypeName = "BulletShotgunManBlueBasicAttack1" },
                    FixTargetDuringAttack = false,
                    StopDuringAttack = true,
                    LeadAmount = 0,
                    LeadChance = 1,
                    RespectReload = true,
                    MagazineCapacity = 1,
                    ReloadSpeed = 2,
                    EmptiesClip = true,
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
                    Cooldown = 4,
                    CooldownVariance = 0,
                    AttackCooldown = 0,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 20,
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
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\BootlegShotgunManBlue_BehaviorScript.txt");

            BootlegShotgunManBlueGUID = m_CachedAIActor.EnemyGuid;

            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, true);
            m_CachedEnemyActor = null;
            return;
        }
        
        public static void BuildCronenbergPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Cronenberg Abomination");
            
            List<string> SpriteList = new List<string>() {
                "Cronenberg_Idle_001",
                "Cronenberg_Idle_002",
                "Cronenberg_Idle_003",
                "Cronenberg_Idle_004",
                "Cronenberg_Move_001",
                "Cronenberg_Move_002",
                "Cronenberg_Move_003",
                "Cronenberg_Move_004",
                "Cronenberg_Move_005",
                "Cronenberg_Spawn_001",
                "Cronenberg_Spawn_002",
                "Cronenberg_Spawn_003",
                "Cronenberg_Spawn_004",
                "Cronenberg_Spawn_005",
                "Cronenberg_Spawn_006",
                "Cronenberg_Die_001",
                "Cronenberg_Die_002",
                "Cronenberg_Die_003"
            };

            List<string> IdleSpriteList = new List<string>() {
                "Cronenberg_Idle_001",
                "Cronenberg_Idle_002",
                "Cronenberg_Idle_003",
                "Cronenberg_Idle_004"
            };

            List<string> MoveSpriteList = new List<string>() {
                "Cronenberg_Move_001",
                "Cronenberg_Move_002",
                "Cronenberg_Move_003",
                "Cronenberg_Move_004",
                "Cronenberg_Move_005"
            };
                        
            List<string> SpawnSpriteList = new List<string>() {
                "Cronenberg_Spawn_001",
                "Cronenberg_Spawn_002",
                "Cronenberg_Spawn_003",
                "Cronenberg_Spawn_004",
                "Cronenberg_Spawn_005",
                "Cronenberg_Spawn_006",
            };
            
            List<string> DeathSpriteList = new List<string>() { "Cronenberg_Die_001", "Cronenberg_Die_002", "Cronenberg_Die_003" };
            

            ItemBuilder.AddSpriteToObject(m_CachedTargetObject, expandSharedAssets1.LoadAsset<Texture2D>(SpriteList[0]), false, false);

            tk2dSprite m_CachedSprite = m_CachedTargetObject.GetComponent<tk2dSprite>();

            foreach (string spriteName in SpriteList) {
                if (spriteName != SpriteList[0]) {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_CachedSprite.Collection);
                }
            }
                        
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleSpriteList, "idle", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveSpriteList, "move", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            
            tk2dSpriteAnimationClip m_SpawnAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            tk2dSpriteAnimationClip m_HitAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleSpriteList, "hit", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            tk2dSpriteAnimationClip m_DeathAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            
            if (m_SpawnAnimation != null && m_DeathAnimation != null && m_HitAnimation != null) {
                m_SpawnAnimation.frames[0].eventAudio = "Play_EX_Cronenberg_Spawn_01";
                m_SpawnAnimation.frames[0].triggerEvent = true;
                m_DeathAnimation.frames[0].eventAudio = "Play_EX_Cronenberg_Die_01";
                m_DeathAnimation.frames[0].triggerEvent = true;
                m_HitAnimation.frames[0].eventAudio = "Play_EX_Cronenberg_Damage_01";
                m_HitAnimation.frames[0].triggerEvent = true;
            }

            
            Gun m_CachedMutantArmGun = (PickupObjectDatabase.GetById(333) as Gun);

            if (m_CachedMutantArmGun) {
                GoopDoer m_BloodGoopDoer = m_CachedTargetObject.AddComponent<GoopDoer>();
                m_BloodGoopDoer.goopDefinition = m_CachedMutantArmGun.singleModule.projectiles[0].gameObject.GetComponent<GoopModifier>().goopDefinition;
                m_BloodGoopDoer.positionSource = GoopDoer.PositionSource.HitBoxCenter;
                m_BloodGoopDoer.updateTiming = GoopDoer.UpdateTiming.Always;
                m_BloodGoopDoer.updateFrequency = 0.05f;
                m_BloodGoopDoer.isTimed = false;
                m_BloodGoopDoer.goopTime = 1;
                m_BloodGoopDoer.updateOnPreDeath = true;
                m_BloodGoopDoer.updateOnDeath = false;
                m_BloodGoopDoer.updateOnAnimFrames = true;
                m_BloodGoopDoer.updateOnCollision = false;
                m_BloodGoopDoer.updateOnGrounded = false;
                m_BloodGoopDoer.updateOnDestroy = false;
                m_BloodGoopDoer.defaultGoopRadius = 1.8f;
                m_BloodGoopDoer.suppressSplashes = false;
                m_BloodGoopDoer.goopSizeVaries = true;
                m_BloodGoopDoer.varyCycleTime = 0.9f;
                m_BloodGoopDoer.radiusMin = 1.8f;
                m_BloodGoopDoer.radiusMax = 2;
                m_BloodGoopDoer.goopSizeRandom = true;
                m_BloodGoopDoer.UsesDispersalParticles = false;
                m_BloodGoopDoer.DispersalDensity = 3;
                m_BloodGoopDoer.DispersalMinCoherency = 0.2f;
                m_BloodGoopDoer.DispersalMaxCoherency = 1;

                CronenbergCorpseDebrisObject1 = expandSharedAssets1.LoadAsset<GameObject>("CronenbergCorpseFragment_01");
                CronenbergCorpseDebrisObject2 = expandSharedAssets1.LoadAsset<GameObject>("CronenbergCorpseFragment_02");
                CronenbergCorpseDebrisObject3 = expandSharedAssets1.LoadAsset<GameObject>("CronenbergCorpseFragment_03");
                CronenbergCorpseDebrisObject4 = expandSharedAssets1.LoadAsset<GameObject>("CronenbergCorpseFragment_04");
                ItemBuilder.AddSpriteToObject(CronenbergCorpseDebrisObject1, expandSharedAssets1.LoadAsset<Texture2D>("Cronenberg_Fragment_01"), false, false);
                ItemBuilder.AddSpriteToObject(CronenbergCorpseDebrisObject2, expandSharedAssets1.LoadAsset<Texture2D>("Cronenberg_Fragment_02"), false, false);
                ItemBuilder.AddSpriteToObject(CronenbergCorpseDebrisObject3, expandSharedAssets1.LoadAsset<Texture2D>("Cronenberg_Fragment_03"), false, false);
                ItemBuilder.AddSpriteToObject(CronenbergCorpseDebrisObject4, expandSharedAssets1.LoadAsset<Texture2D>("Cronenberg_Fragment_04"), false, false);

                ExpandUtility.GenerateSpriteAnimator(CronenbergCorpseDebrisObject1);
                ExpandUtility.GenerateSpriteAnimator(CronenbergCorpseDebrisObject2);
                ExpandUtility.GenerateSpriteAnimator(CronenbergCorpseDebrisObject3);
                ExpandUtility.GenerateSpriteAnimator(CronenbergCorpseDebrisObject4);
                ExpandUtility.AddAnimation(CronenbergCorpseDebrisObject1.GetComponent<tk2dSpriteAnimator>(), CronenbergCorpseDebrisObject1.GetComponent<tk2dSprite>().Collection, new List<string>() { "Cronenberg_Fragment_01" }, "default");
                ExpandUtility.AddAnimation(CronenbergCorpseDebrisObject2.GetComponent<tk2dSpriteAnimator>(), CronenbergCorpseDebrisObject2.GetComponent<tk2dSprite>().Collection, new List<string>() { "Cronenberg_Fragment_02" }, "default");
                ExpandUtility.AddAnimation(CronenbergCorpseDebrisObject3.GetComponent<tk2dSpriteAnimator>(), CronenbergCorpseDebrisObject3.GetComponent<tk2dSprite>().Collection, new List<string>() { "Cronenberg_Fragment_03" }, "default");
                ExpandUtility.AddAnimation(CronenbergCorpseDebrisObject4.GetComponent<tk2dSpriteAnimator>(), CronenbergCorpseDebrisObject4.GetComponent<tk2dSprite>().Collection, new List<string>() { "Cronenberg_Fragment_04" }, "default");

                m_GenerateCronenbergDebris(CronenbergCorpseDebrisObject1, m_BloodGoopDoer.goopDefinition);
                m_GenerateCronenbergDebris(CronenbergCorpseDebrisObject2, m_BloodGoopDoer.goopDefinition);
                m_GenerateCronenbergDebris(CronenbergCorpseDebrisObject3, m_BloodGoopDoer.goopDefinition);
                m_GenerateCronenbergDebris(CronenbergCorpseDebrisObject4, m_BloodGoopDoer.goopDefinition);

                ExpandExplodeOnDeath m_Exploder = m_CachedTargetObject.AddComponent<ExpandExplodeOnDeath>();
                m_Exploder.shardClusters = new ShardCluster[] {
                    new ShardCluster() {
                        minFromCluster = 6,
                        maxFromCluster = 10,
                        forceMultiplier = 4,
                        forceAxialMultiplier = new Vector3(1.25f, 1.25f, 1.25f),
                        rotationMultiplier = 1.65f,
                        clusterObjects = new DebrisObject[] {
                            CronenbergCorpseDebrisObject1.GetComponent<DebrisObject>(),
                            CronenbergCorpseDebrisObject2.GetComponent<DebrisObject>(),
                            CronenbergCorpseDebrisObject3.GetComponent<DebrisObject>(),
                            CronenbergCorpseDebrisObject4.GetComponent<DebrisObject>()
                        }
                    }
                };
                m_Exploder.spawnShardsOnDeath = true;
                m_Exploder.deathType = OnDeathBehavior.DeathType.Death;
                m_CachedMutantArmGun = null;
            }

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, "0a2433d6e0784eefb28762c5c127d0b3", null, EnemyHasNoCorpse: true, EnemyHasNoShooter: true);
                        
            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            m_CachedAIActor.HasShadow = false;
            m_CachedAIActor.MovementSpeed = 0.65f;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.procedurallyOutlined = false;
            m_CachedAIActor.EnemySwitchState = string.Empty;
            m_CachedAIActor.PreventFallingInPitsEver = true;
            m_CachedAIActor.IgnoreForRoomClear = true;
            m_CachedAIActor.HitByEnemyBullets = true;
            m_CachedAIActor.CanTargetEnemies = true;
            m_CachedAIActor.CanTargetPlayers = false;
            m_CachedAIActor.CollisionKnockbackStrength = 5;
            m_CachedAIActor.CollisionDamage = 2;
            m_CachedAIActor.healthHaver.SetHealthMaximum(25);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(15);

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 9,
                    ManualOffsetY = 0,
                    ManualWidth = 67,
                    ManualHeight = 16,
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
                    ManualOffsetX = 9,
                    ManualOffsetY = 0,
                    ManualWidth = 67,
                    ManualHeight = 28,
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
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "idle",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "move",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "hit",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[1],
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }
            
            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() { new ExpandSimpleMoveErraticallyBehavior() };
            
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\Cronenberg_BehaviorScript.txt");
            
            CronenbergGUID = m_CachedAIActor.EnemyGuid;
            CronenbergBullets.m_CachedCronenbergBulletsItem.TransmogTargetGuid = m_CachedAIActor.EnemyGuid;

            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, true);
            return;
        }

        public static void BuildAggressiveCronenbergPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Angry Cronenberg Abomination");
            
            List<string> SpriteList = new List<string>() {
                "berg_tall_idle_front_001",
                "berg_tall_idle_front_002",
                "berg_tall_idle_front_003",
                "berg_tall_idle_front_004",
                "berg_tall_idle_back_001",
                "berg_tall_idle_back_002",
                "berg_tall_idle_back_003",
                "berg_tall_idle_back_004",
                "berg_tall_run_left_001",
                "berg_tall_run_left_002",
                "berg_tall_run_left_003",
                "berg_tall_run_left_004",
                "berg_tall_run_left_005",
                "berg_tall_run_left_006",
                "berg_tall_run_right_001",
                "berg_tall_run_right_002",
                "berg_tall_run_right_003",
                "berg_tall_run_right_004",
                "berg_tall_run_right_005",
                "berg_tall_run_right_006",
                "berg_tall_burst_001",
                "berg_tall_burst_002",
                "berg_tall_burst_003",
                "berg_tall_run_back_left_001",
                "berg_tall_run_back_left_002",
                "berg_tall_run_back_left_003",
                "berg_tall_run_back_left_004",
                "berg_tall_run_back_left_005",
                "berg_tall_run_back_left_006",
                "berg_tall_run_back_right_001",
                "berg_tall_run_back_right_002",
                "berg_tall_run_back_right_003",
                "berg_tall_run_back_right_004",
                "berg_tall_run_back_right_005",
                "berg_tall_run_back_right_006",
                "berg_tall_spawn_001",
                "berg_tall_spawn_002",
                "berg_tall_spawn_003",
                "berg_tall_spawn_004",
                "berg_tall_spawn_005"
            };

            List<string> IdleFrontSpriteList = new List<string>() {
                "berg_tall_idle_front_001",
                "berg_tall_idle_front_002",
                "berg_tall_idle_front_003",
                "berg_tall_idle_front_004"
            };

            List<string> IdleBackSpriteList = new List<string>() {
                "berg_tall_idle_back_001",
                "berg_tall_idle_back_002",
                "berg_tall_idle_back_003",
                "berg_tall_idle_back_004"
            };

            List<string> MoveFrontLeftSpriteList = new List<string>() {
                "berg_tall_run_left_001",
                "berg_tall_run_left_002",
                "berg_tall_run_left_003",
                "berg_tall_run_left_004",
                "berg_tall_run_left_005",
                "berg_tall_run_left_006"
            };

            List<string> MoveFrontRightSpriteList = new List<string>() {
                "berg_tall_run_right_001",
                "berg_tall_run_right_002",
                "berg_tall_run_right_003",
                "berg_tall_run_right_004",
                "berg_tall_run_right_005",
                "berg_tall_run_right_006"
            };

            List<string> MoveBackLeftSpriteList = new List<string>() {
                "berg_tall_run_back_left_001",
                "berg_tall_run_back_left_002",
                "berg_tall_run_back_left_003",
                "berg_tall_run_back_left_004",
                "berg_tall_run_back_left_005",
                "berg_tall_run_back_left_006"
            };

            List<string> MoveBackRightSpriteList = new List<string>() {
                "berg_tall_run_back_right_001",
                "berg_tall_run_back_right_002",
                "berg_tall_run_back_right_003",
                "berg_tall_run_back_right_004",
                "berg_tall_run_back_right_005",
                "berg_tall_run_back_right_006"
            };

            
            List<string> SpawnSpriteList = new List<string>() {
                "berg_tall_spawn_001",
                "berg_tall_spawn_002",
                "berg_tall_spawn_003",
                "berg_tall_spawn_004",
                "berg_tall_spawn_005"
            };
            
            List<string> DeathSpriteList = new List<string>() { "berg_tall_burst_001", "berg_tall_burst_002", "berg_tall_burst_003" };
            

            ItemBuilder.AddSpriteToObject(m_CachedTargetObject, expandSharedAssets1.LoadAsset<Texture2D>(SpriteList[0]), false, false);

            tk2dSprite m_CachedSprite = m_CachedTargetObject.GetComponent<tk2dSprite>();

            foreach (string spriteName in SpriteList) {
                if (spriteName != SpriteList[0]) {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_CachedSprite.Collection);
                }
            }
                        
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleFrontSpriteList, "idle_front", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleFrontSpriteList, "idle_back", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveFrontLeftSpriteList, "move_front_right", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveFrontRightSpriteList, "move_front_left", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveBackLeftSpriteList, "move_back_right", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveBackRightSpriteList, "move_back_left", tk2dSpriteAnimationClip.WrapMode.Loop, 10);

            tk2dSpriteAnimationClip m_SpawnAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            tk2dSpriteAnimationClip m_HitAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleFrontSpriteList, "hit", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            tk2dSpriteAnimationClip m_DeathAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            
            if (m_SpawnAnimation != null && m_DeathAnimation != null && m_HitAnimation != null) {
                m_DeathAnimation.frames[0].eventAudio = "Play_EX_Cronenberg_Die_01";
                m_DeathAnimation.frames[0].triggerEvent = true;
                m_HitAnimation.frames[0].eventAudio = "Play_EX_Cronenberg_Damage_01";
                m_HitAnimation.frames[0].triggerEvent = true;
            }

            
            Gun m_CachedMutantArmGun = (PickupObjectDatabase.GetById(333) as Gun);

            if (m_CachedMutantArmGun) {
                GoopDoer m_BloodGoopDoer = m_CachedTargetObject.AddComponent<GoopDoer>();
                m_BloodGoopDoer.goopDefinition = m_CachedMutantArmGun.singleModule.projectiles[0].gameObject.GetComponent<GoopModifier>().goopDefinition;
                m_BloodGoopDoer.positionSource = GoopDoer.PositionSource.HitBoxCenter;
                m_BloodGoopDoer.updateTiming = GoopDoer.UpdateTiming.Always;
                m_BloodGoopDoer.updateFrequency = 0.05f;
                m_BloodGoopDoer.isTimed = false;
                m_BloodGoopDoer.goopTime = 1;
                m_BloodGoopDoer.updateOnPreDeath = true;
                m_BloodGoopDoer.updateOnDeath = false;
                m_BloodGoopDoer.updateOnAnimFrames = true;
                m_BloodGoopDoer.updateOnCollision = false;
                m_BloodGoopDoer.updateOnGrounded = false;
                m_BloodGoopDoer.updateOnDestroy = false;
                m_BloodGoopDoer.defaultGoopRadius = 1.8f;
                m_BloodGoopDoer.suppressSplashes = false;
                m_BloodGoopDoer.goopSizeVaries = true;
                m_BloodGoopDoer.varyCycleTime = 0.9f;
                m_BloodGoopDoer.radiusMin = 1f;
                m_BloodGoopDoer.radiusMax = 1.5f;
                m_BloodGoopDoer.goopSizeRandom = true;
                m_BloodGoopDoer.UsesDispersalParticles = false;
                m_BloodGoopDoer.DispersalDensity = 3;
                m_BloodGoopDoer.DispersalMinCoherency = 0.2f;
                m_BloodGoopDoer.DispersalMaxCoherency = 1;

                AggressiveCronenbergCorpseDebrisObject = expandSharedAssets1.LoadAsset<GameObject>("AngryCronenbergCorpseFragment");
                ItemBuilder.AddSpriteToObject(AggressiveCronenbergCorpseDebrisObject, expandSharedAssets1.LoadAsset<Texture2D>("Cronenberg_Fragment_04"), false, false);

                ExpandUtility.GenerateSpriteAnimator(AggressiveCronenbergCorpseDebrisObject);
                ExpandUtility.AddAnimation(AggressiveCronenbergCorpseDebrisObject.GetComponent<tk2dSpriteAnimator>(), AggressiveCronenbergCorpseDebrisObject.GetComponent<tk2dSprite>().Collection, new List<string>() { "Cronenberg_Fragment_01" }, "default");
                
                m_GenerateCronenbergDebris(AggressiveCronenbergCorpseDebrisObject, m_BloodGoopDoer.goopDefinition);

                ExpandExplodeOnDeath m_Exploder = m_CachedTargetObject.AddComponent<ExpandExplodeOnDeath>();
                m_Exploder.shardClusters = new ShardCluster[] {
                    new ShardCluster() {
                        minFromCluster = 6,
                        maxFromCluster = 10,
                        forceMultiplier = 4,
                        forceAxialMultiplier = new Vector3(1.25f, 1.25f, 1.25f),
                        rotationMultiplier = 1.65f,
                        clusterObjects = new DebrisObject[] {
                            AggressiveCronenbergCorpseDebrisObject.GetComponent<DebrisObject>(),
                            AggressiveCronenbergCorpseDebrisObject.GetComponent<DebrisObject>(),
                            AggressiveCronenbergCorpseDebrisObject.GetComponent<DebrisObject>(),
                            AggressiveCronenbergCorpseDebrisObject.GetComponent<DebrisObject>()
                        }
                    }
                };
                m_Exploder.spawnShardsOnDeath = true;
                m_Exploder.deathType = OnDeathBehavior.DeathType.Death;
                m_CachedMutantArmGun = null;
            }

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, "6d2d7a845e464d3ca453fe1fff5fed84", null, EnemyHasNoCorpse: true, EnemyHasNoShooter: true);
                        
            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            m_CachedAIActor.HasShadow = false;
            m_CachedAIActor.MovementSpeed = 5f;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.procedurallyOutlined = true;
            m_CachedAIActor.EnemySwitchState = string.Empty;
            m_CachedAIActor.PreventFallingInPitsEver = true;
            m_CachedAIActor.IgnoreForRoomClear = false;
            m_CachedAIActor.HitByEnemyBullets = false;
            m_CachedAIActor.CanTargetEnemies = true;
            m_CachedAIActor.CanTargetPlayers = false;
            m_CachedAIActor.CollisionKnockbackStrength = 5;
            m_CachedAIActor.CollisionDamage = 1;
            m_CachedAIActor.DiesOnCollison = true;
            m_CachedAIActor.healthHaver.SetHealthMaximum(25);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(15);

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 2,
                    ManualOffsetY = 0,
                    ManualWidth = 14,
                    ManualHeight = 16,
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
                    ManualOffsetX = 2,
                    ManualOffsetY = 0,
                    ManualWidth = 15,
                    ManualHeight = 29,
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
                    Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                    Prefix = "idle",
                    AnimNames = new string[2],
                    Flipped = new DirectionalAnimation.FlipType[2]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "move",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "hit",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[1],
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }
            
            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = false,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>();
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new SeekTargetBehavior() {
                    StopWhenInRange = false,
                    CustomRange = 6,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\AngryCronenberg_BehaviorScript.txt");

            AggressiveCronenbergGUID = m_CachedAIActor.EnemyGuid;

            AddEnemyToDatabase(m_CachedTargetObject, m_CachedAIActor.EnemyGuid, true);
            return;
        }
        
        /*public static void BuildKillStumpsPrefab(out GameObject m_CachedTargetObject, bool isFakePrefab = true) {
            
            AIActorDummy KillPillarsTemplate = (GetOrLoadByGuid_Orig("3f11bbbc439c4086a180eb0fb9990cb4") as AIActorDummy);
            
            m_CachedTargetObject = new GameObject("BossStumpsDummy", new Type[] { typeof(SpeculativeRigidbody), typeof(HitEffectHandler), typeof(AIActorDummy) }) { layer = 0 };
            
            SpeculativeRigidbody rigidBody = m_CachedTargetObject.GetComponent<SpeculativeRigidbody>();
            rigidBody.CollideWithTileMap = true;
            rigidBody.CollideWithOthers = true;
            rigidBody.Velocity = Vector2.zero;
            rigidBody.CapVelocity = false;
            rigidBody.MaxVelocity = Vector2.zero;
            rigidBody.ForceAlwaysUpdate = false;
            rigidBody.CanPush = false;
            rigidBody.CanBePushed = false;
            rigidBody.PushSpeedModifier = 1;
            rigidBody.CanCarry = false;
            rigidBody.CanBeCarried = true;
            rigidBody.PreventPiercing = false;
            rigidBody.SkipEmptyColliders = false;
            rigidBody.RecheckTriggers = false;
            rigidBody.UpdateCollidersOnRotation = false;
            rigidBody.UpdateCollidersOnScale = false;
            rigidBody.AxialScale = Vector2.one;
            rigidBody.DebugParams = new SpeculativeRigidbody.DebugSettings() {
                ShowPosition = false,
                PositionHistory = 0,
                ShowVelocity = false,
                ShowSlope = false
            };
            rigidBody.IgnorePixelGrid = false;
            rigidBody.m_position = new Position() { m_position = IntVector2.Zero, m_remainder = Vector2.zero };
            rigidBody.PixelColliders = new List<PixelCollider>() {
                new PixelCollider() {
                    Enabled = false,
                    CollisionLayer = CollisionLayer.LowObstacle,
                    IsTrigger = false,
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 0,
                    ManualOffsetY = 0,
                    ManualWidth = 0,
                    ManualHeight = 0,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            };

            HitEffectHandler hitEffectHandler = m_CachedTargetObject.GetComponent<HitEffectHandler>();
            hitEffectHandler.overrideHitEffect = new VFXComplex() { effects = new VFXObject[0] };
            hitEffectHandler.overrideHitEffectPool = new VFXPool() { effects = new VFXComplex[0], type = VFXPoolType.None };
            hitEffectHandler.additionalHitEffects = new HitEffectHandler.AdditionalHitEffect[0];
            hitEffectHandler.SuppressAllHitEffects = false;


            AIActorDummy KillStumpsAIActorDummy = m_CachedTargetObject.GetComponent<AIActorDummy>();
            KillStumpsAIActorDummy.placeableWidth = 12;
            KillStumpsAIActorDummy.placeableHeight = 12;
            KillStumpsAIActorDummy.difficulty = 0;
            KillStumpsAIActorDummy.isPassable = true;
            KillStumpsAIActorDummy.ActorName = "Kill Stumps";
            KillStumpsAIActorDummy.OverrideDisplayName = "Kill Stumps";
            KillStumpsAIActorDummy.actorTypes = 0;
            KillStumpsAIActorDummy.HasShadow = true;
            KillStumpsAIActorDummy.ShadowHeightOffGround = 0;
            KillStumpsAIActorDummy.ActorShadowOffset = Vector3.zero;
            KillStumpsAIActorDummy.DoDustUps = false;
            KillStumpsAIActorDummy.DustUpInterval = 0;
            KillStumpsAIActorDummy.FreezeDispelFactor = 20;
            KillStumpsAIActorDummy.ImmuneToAllEffects = false;
            KillStumpsAIActorDummy.EffectResistances = new ActorEffectResistance[0];
            KillStumpsAIActorDummy.OverrideColorOverridden = false;
            KillStumpsAIActorDummy.EnemyId = -1;
            KillStumpsAIActorDummy.EnemyGuid = "12e65e5a-8996-4803-b906-d8b4e5c5d261";
            KillStumpsAIActorDummy.ForcedPositionInAmmonomicon = -1;
            KillStumpsAIActorDummy.SetsFlagOnActivation = false;
            KillStumpsAIActorDummy.SetsFlagOnDeath = false;
            KillStumpsAIActorDummy.FlagToSetOnDeath = 0;
            KillStumpsAIActorDummy.FlagToSetOnActivation = 0;
            KillStumpsAIActorDummy.SetsCharacterSpecificFlagOnDeath = false;
            KillStumpsAIActorDummy.CharacterSpecificFlagToSetOnDeath = 0;
            KillStumpsAIActorDummy.IsNormalEnemy = true;
            KillStumpsAIActorDummy.IsSignatureEnemy = false;
            KillStumpsAIActorDummy.IsHarmlessEnemy = false;
            KillStumpsAIActorDummy.CompanionSettings = new ActorCompanionSettings() { WarpsToRandomPoint = false };
            KillStumpsAIActorDummy.MovementSpeed = 2;
            KillStumpsAIActorDummy.PathableTiles = Dungeonator.CellTypes.FLOOR;
            KillStumpsAIActorDummy.DiesOnCollison = false;
            KillStumpsAIActorDummy.CollisionDamage = 1;
            KillStumpsAIActorDummy.CollisionKnockbackStrength = 5;
            KillStumpsAIActorDummy.CollisionDamageTypes = CoreDamageTypes.None;
            KillStumpsAIActorDummy.EnemyCollisionKnockbackStrengthOverride = -1;
            KillStumpsAIActorDummy.CollisionVFX = new VFXPool() { type = VFXPoolType.None, effects = new VFXComplex[0] };
            KillStumpsAIActorDummy.NonActorCollisionVFX = new VFXPool() { type = VFXPoolType.None, effects = new VFXComplex[0] };
            KillStumpsAIActorDummy.CollisionSetsPlayerOnFire = false;
            KillStumpsAIActorDummy.TryDodgeBullets = true;
            KillStumpsAIActorDummy.AvoidRadius = 4;
            KillStumpsAIActorDummy.ReflectsProjectilesWhileInvulnerable = false;
            KillStumpsAIActorDummy.HitByEnemyBullets = false;
            KillStumpsAIActorDummy.HasOverrideDodgeRollDeath = false;
            KillStumpsAIActorDummy.OverrideDodgeRollDeath = string.Empty;
            KillStumpsAIActorDummy.CanDropCurrency = true;
            KillStumpsAIActorDummy.AdditionalSingleCoinDropChance = 0;
            KillStumpsAIActorDummy.CanDropItems = true;
            KillStumpsAIActorDummy.CanDropDuplicateItems = false;
            KillStumpsAIActorDummy.CustomLootTableMinDrops = 1;
            KillStumpsAIActorDummy.CustomLootTableMaxDrops = 1;
            KillStumpsAIActorDummy.ChanceToDropCustomChest = 0;
            KillStumpsAIActorDummy.IgnoreForRoomClear = false;
            KillStumpsAIActorDummy.SpawnLootAtRewardChestPos = false;
            KillStumpsAIActorDummy.CorpseShadow = true;
            KillStumpsAIActorDummy.TransferShadowToCorpse = false;
            KillStumpsAIActorDummy.shadowDeathType = AIActor.ShadowDeathType.Fade;
            KillStumpsAIActorDummy.PreventDeathKnockback = false;
            KillStumpsAIActorDummy.OnCorpseVFX = new VFXPool() { type = VFXPoolType.None, effects = new VFXComplex[0] };
            KillStumpsAIActorDummy.OnEngagedVFXAnchor  = tk2dBaseSprite.Anchor.LowerLeft;
            KillStumpsAIActorDummy.shadowHeightOffset = 0;
            KillStumpsAIActorDummy.invisibleUntilAwaken = false;
            KillStumpsAIActorDummy.procedurallyOutlined = true;
            KillStumpsAIActorDummy.forceUsesTrimmedBounds = true;
            KillStumpsAIActorDummy.reinforceType = AIActor.ReinforceType.FullVfx;
            KillStumpsAIActorDummy.UsesVaryingEmissiveShaderPropertyBlock = false;
            KillStumpsAIActorDummy.EnemySwitchState = string.Empty;
            KillStumpsAIActorDummy.OverrideSpawnReticleAudio = string.Empty;
            KillStumpsAIActorDummy.OverrideSpawnAppearAudio = string.Empty;
            KillStumpsAIActorDummy.UseMovementAudio = false;
            KillStumpsAIActorDummy.StartMovingEvent = string.Empty;
            KillStumpsAIActorDummy.StopMovingEvent = string.Empty;
            KillStumpsAIActorDummy.animationAudioEvents = new List<ActorAudioEvent>(0);
            KillStumpsAIActorDummy.HealthOverrides = new List<AIActor.HealthOverride>(0);
            KillStumpsAIActorDummy.IdentifierForEffects = AIActor.EnemyTypeIdentifier.UNIDENTIFIED;
            KillStumpsAIActorDummy.BehaviorOverridesVelocity = false;
            KillStumpsAIActorDummy.BehaviorVelocity = Vector2.zero;
            KillStumpsAIActorDummy.AlwaysShowOffscreenArrow = false;
            KillStumpsAIActorDummy.BlackPhantomProperties = new BlackPhantomProperties() {
                BonusHealthPercentIncrease = 0,
                BonusHealthFlatIncrease = 0,
                MaxTotalHealth = 175,
                CooldownMultiplier = 0.66f,
                MovementSpeedMultiplier = 1.5f,
                LocalTimeScaleMultiplier = 1,
                BulletSpeedMultiplier = 1,
                GradientScale = 0.75f,
                ContrastPower = 1.3f
            };
            KillStumpsAIActorDummy.ForceBlackPhantomParticles = false;
            KillStumpsAIActorDummy.OverrideBlackPhantomParticlesCollider = false;
            KillStumpsAIActorDummy.BlackPhantomParticlesCollider = 0;
            KillStumpsAIActorDummy.PreventFallingInPitsEver = false;
            KillStumpsAIActorDummy.isInBossTab = true;

            GameObject m_RealPrefab = Instantiate(KillPillarsTemplate.realPrefab);
            m_RealPrefab.transform.parent = m_CachedTargetObject.transform;

            KillStumpsAIActorDummy.realPrefab = m_RealPrefab;

            m_RealPrefab.name = "BossStumps";

            GenericIntroDoer genericIntroDoer = m_RealPrefab.GetComponent<GenericIntroDoer>();

            genericIntroDoer.portraitSlideSettings = new PortraitSlideSettings() {
                bossNameString = "Kill Stumps",
                bossSubtitleString = "Test Subtitle String",
                bossQuoteString = "Test Quate String",
                bossArtSprite = ExpandUtilities.ResourceExtractor.GetTextureFromResource("Textures\\MiscEnemies\\KillStumps\\KillStumpsBossCard.png"),
                bossSpritePxOffset = IntVector2.Zero,
                topLeftTextPxOffset = IntVector2.Zero,
                bottomRightTextPxOffset = IntVector2.Zero,
                bgColor = new Color(0, 0, 1, 1)                
            };

            GameObject Statue1 = m_RealPrefab.transform.Find("AK47").gameObject;
            GameObject Statue2 = m_RealPrefab.transform.Find("Shotgun").gameObject;
            GameObject Statue3 = m_RealPrefab.transform.Find("Uzi").gameObject;
            GameObject Statue4 = m_RealPrefab.transform.Find("DesertEagle").gameObject;
            
            m_CachedTargetObject.SetActive(false);
            if (isFakePrefab) {
                // KillStumpsGUID = KillStumpsAIActorDummy.EnemyGuid;
                AddEnemyToDatabase(m_CachedTargetObject, KillStumpsAIActorDummy.EnemyGuid, true);
                FakePrefab.MarkAsFakePrefab(m_CachedTargetObject);
                DontDestroyOnLoad(m_CachedTargetObject);
            }
        }*/

        public static void BuildParasiteBossPrefab(out GameObject m_CachedTargetObject, bool isFakePrefab = true) {
            
            m_CachedTargetObject = Instantiate(GetOrLoadByGuid("dc3cd41623d447aeba77c77c99598426").gameObject);
            m_CachedTargetObject.SetActive(false);

            m_CachedTargetObject.name = "BossParasite";

            AIActor m_TargetAIActor = m_CachedTargetObject.GetComponent<AIActor>();
            m_TargetAIActor.EnemyGuid = "acd8d483-f24e-4c43-b964-fa4e54068cf1";
            m_TargetAIActor.EnemyId = UnityEngine.Random.Range(100000, 999999);

            m_TargetAIActor.ActorName = "Otherwordly Parasite";

            EncounterTrackable ParasiteEncounterable = m_CachedTargetObject.GetComponent<EncounterTrackable>();
            ParasiteEncounterable.EncounterGuid = "afa1216f84714d73af66d613627397cc";
            ParasiteEncounterable.journalData.PrimaryDisplayName = "Parasatic Abomination";
            ParasiteEncounterable.journalData.NotificationPanelDescription = "Parasatic horror inside the beast.";
            ParasiteEncounterable.journalData.AmmonomiconFullEntry = "This abomination may have been responsible for the rampage of the giant worm monster.";

            m_TargetAIActor.SetsFlagOnDeath = false;
            
            ExpandParasiteBossDeathController parasiteDeathController = m_CachedTargetObject.AddComponent<ExpandParasiteBossDeathController>();
            parasiteDeathController.explosionVfx = new List<GameObject>();
            foreach (GameObject vfxObject in m_CachedTargetObject.GetComponent<BossFinalMarineDeathController>().explosionVfx) {
                parasiteDeathController.explosionVfx.Add(vfxObject);
            }
            parasiteDeathController.explosionCount = 9;
            parasiteDeathController.explosionMidDelay = 0.15f;
            parasiteDeathController.bigExplosionVfx = new List<GameObject>();
            foreach (GameObject vfxObject in m_CachedTargetObject.GetComponent<BossFinalMarineDeathController>().bigExplosionVfx) {
                parasiteDeathController.bigExplosionVfx.Add(vfxObject);
            }
            parasiteDeathController.bigExplosionCount = 1;
            parasiteDeathController.bigExplosionMidDelay = 0.3f;

            Destroy(m_CachedTargetObject.GetComponent<BossFinalMarineDeathController>());
            
            GenericIntroDoer bossIntroDoer = m_CachedTargetObject.GetComponent<GenericIntroDoer>();
            bossIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
            bossIntroDoer.portraitSlideSettings.bossNameString = "Parasetic Abomination";
            bossIntroDoer.portraitSlideSettings.bossSubtitleString= "Parasite Horror";
            bossIntroDoer.portraitSlideSettings.bossQuoteString = string.Empty;

            ObjectVisibilityManager visiblityManager = m_CachedTargetObject.GetComponent<ObjectVisibilityManager>();
            visiblityManager.SuppressPlayerEnteredRoom = false;
            
            BehaviorSpeculator bossBehaviorSpeculator = m_CachedTargetObject.GetComponent<BehaviorSpeculator>();
            
            m_TargetAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            
            AttackBehaviorBase m_PortalBehavior = null;
            AttackBehaviorGroup.AttackGroupItem m_PortalBehaviorGroupItem = null;

            foreach (AttackBehaviorBase attackBehavior in bossBehaviorSpeculator.AttackBehaviors) {
                if (attackBehavior is BossFinalMarinePortalBehavior) {
                    m_PortalBehavior = attackBehavior;
                } else if (attackBehavior is TeleportBehavior) {
                    (attackBehavior as TeleportBehavior).ManuallyDefineRoom = false;
                    (attackBehavior as TeleportBehavior).roomMin = new Vector2(4, 4);
                    (attackBehavior as TeleportBehavior).roomMax = new Vector2(15, 15);
                }
            }
            
            foreach (AttackBehaviorGroup.AttackGroupItem behaviorGroupItem in bossBehaviorSpeculator.AttackBehaviorGroup.AttackBehaviors) {
                if (behaviorGroupItem.Behavior is BossFinalMarinePortalBehavior) {
                    m_PortalBehaviorGroupItem = behaviorGroupItem;
                } else if (behaviorGroupItem.NickName == "Frequent Teleport") {
                    (behaviorGroupItem.Behavior as TeleportBehavior).ManuallyDefineRoom = false;
                    (behaviorGroupItem.Behavior as TeleportBehavior).roomMin = new Vector2(4, 4);
                    (behaviorGroupItem.Behavior as TeleportBehavior).roomMax = new Vector2(15, 15);
                }
            }

            bossBehaviorSpeculator.AttackBehaviorGroup.AttackBehaviors.Remove(m_PortalBehaviorGroupItem);
            bossBehaviorSpeculator.AttackBehaviors.Remove(m_PortalBehavior);

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = bossBehaviorSpeculator;
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\Parasite_BehaviorScript.txt");
            bossBehaviorSpeculator.RegenerateCache();

            m_TargetAIActor.healthHaver.ForceSetCurrentHealth(700);
            m_TargetAIActor.healthHaver.SetHealthMaximum(1800);

            m_TargetAIActor.RegenerateCache();

            if (isFakePrefab) {
                ParasiteBossGUID = m_TargetAIActor.EnemyGuid;
                AddEnemyToDatabase(m_CachedTargetObject, m_TargetAIActor.EnemyGuid, true);
                FakePrefab.MarkAsFakePrefab(m_CachedTargetObject);
                DontDestroyOnLoad(m_CachedTargetObject);
            }
        }
        
        public static void BuildJungleBossPrefab(out GameObject m_CachedTargetObject, bool isFakePrefab = true) {
            
            m_CachedTargetObject = Instantiate(GetOrLoadByGuid("880bbe4ce1014740ba6b4e2ea521e49d").gameObject);
            m_CachedTargetObject.SetActive(false);
            m_CachedTargetObject.name = "Com4nd0 Boss";

            AIActor m_TargetAIActor = m_CachedTargetObject.GetComponent<AIActor>();
            m_TargetAIActor.EnemyGuid = "0a406e36-80eb-43b8-8ad0-c56232f9496e";
            m_TargetAIActor.EnemyId = UnityEngine.Random.Range(100000, 999999);

            m_TargetAIActor.ActorName = "Com4nd0";

            EncounterTrackable ParasiteEncounterable = m_CachedTargetObject.GetComponent<EncounterTrackable>();
            ParasiteEncounterable.EncounterGuid = "7330c08088cf4f8baf6a640d4f8f5c45";
            ParasiteEncounterable.journalData.PrimaryDisplayName = "Com4nd0 Boss";
            ParasiteEncounterable.journalData.NotificationPanelDescription = "The Lost Human";
            ParasiteEncounterable.journalData.AmmonomiconFullEntry = "This human was lost in the Jungle for many years and found refuge at an ancient temple ruins";

            m_TargetAIActor.SetsFlagOnDeath = false;
                      
            // Destroy(m_CachedTargetObject.GetComponent<BossFinalRobotIntroDoer>());
            Destroy(m_CachedTargetObject.GetComponent<BossFinalRobotDeathController>());

            GenericIntroDoer bossIntroDoer = m_CachedTargetObject.GetComponent<GenericIntroDoer>();
            bossIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
            bossIntroDoer.portraitSlideSettings.bossNameString = "Com4nd0";
            bossIntroDoer.portraitSlideSettings.bossSubtitleString= "Temple Defence";
            bossIntroDoer.portraitSlideSettings.bossQuoteString = string.Empty;



            ObjectVisibilityManager visiblityManager = m_CachedTargetObject.GetComponent<ObjectVisibilityManager>();
            visiblityManager.SuppressPlayerEnteredRoom = false;
            
            BehaviorSpeculator bossBehaviorSpeculator = m_CachedTargetObject.GetComponent<BehaviorSpeculator>();
            
            m_TargetAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            
            foreach (AttackBehaviorBase attackBehavior in bossBehaviorSpeculator.AttackBehaviors) {
                if (attackBehavior is SummonEnemyBehavior) {
                    (attackBehavior as SummonEnemyBehavior).ManuallyDefineRoom = false;
                    (attackBehavior as SummonEnemyBehavior).roomMin = Vector2.zero;
                    (attackBehavior as SummonEnemyBehavior).roomMax = Vector2.zero;
                    (attackBehavior as SummonEnemyBehavior).EnemeyGuids = new List<string>() { "e861e59012954ab2b9b6977da85cb83c", "4b21a913e8c54056bc05cafecf9da880", "a9cc6a4e9b3d46ea871e70a03c9f77d4", "80ab6cd15bfc46668a8844b2975c6c26" };
                } 
            }
            
            foreach (AttackBehaviorGroup.AttackGroupItem behaviorGroupItem in bossBehaviorSpeculator.AttackBehaviorGroup.AttackBehaviors) {
                if (behaviorGroupItem.NickName == "Teleport") {
                    (behaviorGroupItem.Behavior as TeleportBehavior).ManuallyDefineRoom = false;
                    (behaviorGroupItem.Behavior as TeleportBehavior).roomMin = Vector2.zero;
                    (behaviorGroupItem.Behavior as TeleportBehavior).roomMax = Vector2.zero;
                }
            }
            
            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = bossBehaviorSpeculator;
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\Com4nd0_BehaviorScript.txt");
            bossBehaviorSpeculator.RegenerateCache();


            m_TargetAIActor.healthHaver.ForceSetCurrentHealth(1000);
            m_TargetAIActor.healthHaver.SetHealthMaximum(1000);

            m_TargetAIActor.RegenerateCache();

            if (isFakePrefab) {
                com4nd0GUID = m_TargetAIActor.EnemyGuid;
                AddEnemyToDatabase(m_CachedTargetObject, m_TargetAIActor.EnemyGuid, true);
                FakePrefab.MarkAsFakePrefab(m_CachedTargetObject);
                DontDestroyOnLoad(m_CachedTargetObject);
            }
        }

        public static void BuildCultistCompanionPrefab(AssetBundle expandSharedAssets1, out GameObject CachedTargetEnemyObject) {
            AssetBundle braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");

            GameObject m_SelectedPlayer = braveResources.LoadAsset<GameObject>("PlayerCoopCultist").transform.Find("PlayerSprite").gameObject;
                        
            AIActor CachedEnemyActor = GetOrLoadByGuid_Orig("57255ed50ee24794b7aac1ac3cfb8a95");
            AIActor CachedSpaceTurtle = GetOrLoadByGuid_Orig("9216803e9c894002a4b931d7ea9c6bdf");
            GameObject m_DummyCorpseObject = null;

            CachedTargetEnemyObject = expandSharedAssets1.LoadAsset<GameObject>("Cultist Companion");
            
            tk2dSprite newSprite = CachedTargetEnemyObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateComponent(newSprite, m_SelectedPlayer.GetComponent<tk2dSprite>());
            
            // If Player sprite was flipped (aka, player aiming/facing towards the left), then this could cause sprite being shifted left on AIActor.
            // Always set false to ensure this doesn't happen.
            newSprite.FlipX = false;

            GameObject m_CachedGunAttachPoint = CachedTargetEnemyObject.transform.Find("GunAttachPoint").gameObject;

            ExpandUtility.DuplicateAIShooterAndAIBulletBank(CachedTargetEnemyObject, CachedSpaceTurtle.aiShooter, CachedSpaceTurtle.GetComponent<AIBulletBank>(), 24, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(CachedTargetEnemyObject, out m_DummyCorpseObject, "Cultist Companion", "1d1e1070617842f09e6f45df3cb223f6", null, instantiateCorpseObject: false, ExternalCorpseObject: CachedEnemyActor.CorpseObject, EnemyHasNoShooter: true);
            
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();
            
            CachedGlitchEnemyActor.DoDustUps = true;
            CachedGlitchEnemyActor.DustUpInterval = 0.4f;
            CachedGlitchEnemyActor.MovementSpeed = 3.5f;
            CachedGlitchEnemyActor.EnemySwitchState = "Gun Cultist";
            

            List<tk2dSpriteAnimationClip> m_AnimationClips = new List<tk2dSpriteAnimationClip>();
            foreach (tk2dSpriteAnimationClip clip in m_SelectedPlayer.GetComponent<tk2dSpriteAnimator>().Library.clips) {
                if (clip != null && !string.IsNullOrEmpty(clip.name)) {
                    if (clip.name.ToLower() == "idle") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "idle_backward") {
                        m_AnimationClips.Add(clip);
                    }  else if (clip.name.ToLower() == "dodge") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "dodge_bw") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "run_down") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "run_up") {
                        m_AnimationClips.Add(clip);
                    } else if(clip.name.ToLower() == "death") {
                        m_AnimationClips.Add(clip);
                    } else if(clip.name.ToLower() == "death_bw") {
                        m_AnimationClips.Add(clip);
                    } else if(clip.name.ToLower() == "pitfall") {
                        m_AnimationClips.Add(clip);
                    } else if(clip.name.ToLower() == "pitfall_down") {
                        m_AnimationClips.Add(clip);
                    }  
                }
            }
            if (!CachedGlitchEnemyActor.spriteAnimator.Library) { CachedGlitchEnemyActor.spriteAnimator.Library = CachedTargetEnemyObject.AddComponent<tk2dSpriteAnimation>(); }
            if (m_AnimationClips.Count > 0) { CachedGlitchEnemyActor.spriteAnimator.Library.clips = m_AnimationClips.ToArray(); }
            CachedGlitchEnemyActor.spriteAnimator.DefaultClipId = 0;
            CachedGlitchEnemyActor.spriteAnimator.playAutomatically = true;
            
            if (CachedGlitchEnemyActor.aiAnimator) {
                CachedGlitchEnemyActor.aiAnimator.facingType = AIAnimator.FacingType.Movement;
                CachedGlitchEnemyActor.aiAnimator.directionalType = AIAnimator.DirectionalType.Sprite;
                CachedGlitchEnemyActor.aiAnimator.faceSouthWhenStopped = false;
                CachedGlitchEnemyActor.aiAnimator.faceTargetWhenStopped = false;
                CachedGlitchEnemyActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                CachedGlitchEnemyActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                    Prefix = "idle",
                    AnimNames = new string[] { "idle_backward", "idle" },
                    Flipped = new DirectionalAnimation.FlipType[2],                    
                };
                CachedGlitchEnemyActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                    Prefix = "run",
                    AnimNames = new string[] { "run_up", "run_down" },
                    Flipped = new DirectionalAnimation.FlipType[2],                    
                };
                CachedGlitchEnemyActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "dodgeroll",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                            Prefix = "dodge",
                            AnimNames = new string[] { "dodge_bw", "dodge" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall", "pitfall_down" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    }
                };
            }
            
            ExpandUtility.DuplicateComponent(CachedGlitchEnemyActor.healthHaver, GetOrLoadByGuid_Orig("705e9081261446039e1ed9ff16905d04").healthHaver);
                        
            BehaviorSpeculator behaviorSpeculator = CachedTargetEnemyObject.AddComponent<BehaviorSpeculator>();
            ExpandUtility.DuplicateComponent(behaviorSpeculator, CachedEnemyActor.behaviorSpeculator);

            behaviorSpeculator.MovementBehaviors.Add(new CompanionFollowPlayerBehavior() {
                PathInterval = 0.25f,
                DisableInCombat = true,
                IdealRadius = 3f,
                CatchUpRadius = 6f,
                CatchUpAccelTime = 5,
                CatchUpSpeed = 6,
                CatchUpMaxSpeed = 10,
                CatchUpAnimation = "dodge",
                CatchUpOutAnimation = string.Empty,
                IdleAnimations = new string[] { "idle" },
                CanRollOverPits = true,
                RollAnimation = "dodge",
            });
             

            foreach (AttackBehaviorBase attackBehavior in behaviorSpeculator.AttackBehaviors) {
                if (attackBehavior is ShootGunBehavior) {
                    ShootGunBehavior shootGun = (attackBehavior as ShootGunBehavior);
                    shootGun.GroupCooldownVariance = 0;
                }
            }

            foreach (AttackBehaviorGroup.AttackGroupItem attackBehavior in behaviorSpeculator.AttackBehaviorGroup.AttackBehaviors) {
                if (attackBehavior.NickName == "Basic Shoot") { (attackBehavior.Behavior as ShootGunBehavior).GroupCooldownVariance = 0; }
            }

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = behaviorSpeculator;
            // ExpandUtility.DuplicateComponent(m_TargetBehaviorSpeculatorSeralized, (CachedEnemyActor.behaviorSpeculator as ISerializedObject));
            m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<UnityEngine.Object>();
            m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = (CachedEnemyActor.behaviorSpeculator as ISerializedObject).SerializedStateKeys;
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\CultistCompanion_BehaviorScript.txt");
            
            ExpandUtility.MakeCompanion(CachedGlitchEnemyActor, null, null, true, false, true, false);

            AddEnemyToDatabase(CachedTargetEnemyObject, CachedGlitchEnemyActor.EnemyGuid, false);

            FriendlyCultistGUID = CachedGlitchEnemyActor.EnemyGuid;
            braveResources = null;
        }
        

        private static void m_GenerateCronenbergDebris(GameObject targetObject, GoopDefinition goopSource) {
            DebrisObject m_CachedDebrisObject = targetObject.AddComponent<DebrisObject>();
            m_CachedDebrisObject.Priority = EphemeralObject.EphemeralPriority.Minor;
            m_CachedDebrisObject.audioEventName = string.Empty;
            m_CachedDebrisObject.playAnimationOnTrigger = false;
            m_CachedDebrisObject.usesDirectionalFallAnimations = false;
            m_CachedDebrisObject.directionalAnimationData = new DebrisDirectionalAnimationInfo() { fallDown = string.Empty, fallLeft = string.Empty, fallRight = string.Empty, fallUp = string.Empty };
            m_CachedDebrisObject.breaksOnFall = true;
            m_CachedDebrisObject.breakOnFallChance = 1;
            m_CachedDebrisObject.changesCollisionLayer = false;
            m_CachedDebrisObject.groundedCollisionLayer = CollisionLayer.LowObstacle;
            m_CachedDebrisObject.followupBehavior = DebrisObject.DebrisFollowupAction.None;
            m_CachedDebrisObject.collisionStopsBullets = false;
            m_CachedDebrisObject.animatePitFall = false;
            m_CachedDebrisObject.pitFallSplash = true;
            m_CachedDebrisObject.inertialMass = 1;
            m_CachedDebrisObject.motionMultiplier = 1;
            m_CachedDebrisObject.canRotate = true;
            m_CachedDebrisObject.angularVelocity = 10;
            m_CachedDebrisObject.angularVelocityVariance = 3;
            m_CachedDebrisObject.bounceCount = 1;
            m_CachedDebrisObject.additionalBounceEnglish = 0;
            m_CachedDebrisObject.decayOnBounce = 0.5f;
            m_CachedDebrisObject.killTranslationOnBounce = false;
            m_CachedDebrisObject.usesLifespan = false;
            m_CachedDebrisObject.lifespanMin = 1;
            m_CachedDebrisObject.lifespanMax = 1;
            m_CachedDebrisObject.shouldUseSRBMotion = false;
            m_CachedDebrisObject.removeSRBOnGrounded = false;
            m_CachedDebrisObject.placementOptions = new DebrisObject.DebrisPlacementOptions { canBeRotated = true, canBeFlippedHorizontally = false, canBeFlippedVertically = false };
            m_CachedDebrisObject.DoesGoopOnRest = true;
            m_CachedDebrisObject.AssignedGoop = goopSource;
            m_CachedDebrisObject.GoopRadius = 1;
            m_CachedDebrisObject.additionalHeightBoost = 0;
            m_CachedDebrisObject.AssignFinalWorldDepth(-1.5f);
            m_CachedDebrisObject.ForceUpdateIfDisabled = false;
        }
        
    }
}

