using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandUtilities;
using System.Reflection;
using FullInspector;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandGungeoneerMimicBossPlacable : DungeonPlaceableBehaviour, IPlaceConfigurable {
                
        private static GameObject m_CorpseObject;
                
        public static void GenerateGungeoneerMimicBoss(GameObject aiActorObject, PlayerController sourcePlayer = null) {
            
            if (sourcePlayer == null) { sourcePlayer = GameManager.Instance.PrimaryPlayer; }
            if (sourcePlayer == null) { return; }
            
            tk2dSprite playerSprite = aiActorObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(playerSprite, (sourcePlayer.sprite as tk2dSprite));

            // If Player sprite was flipped (aka, player aiming/facing towards the left), then this could cause sprite being shifted left on AIActor.
            // Always set false to ensure this doesn't happen.
            playerSprite.FlipX = false;

            ExpandUtility.GenerateAIActorTemplate(aiActorObject, out m_CorpseObject, aiActorObject.name, System.Guid.NewGuid().ToString(), GunAttachOffset: new Vector3(0.3f, 0.25f, 0), StartingGunID: 472);
            
            AIActor CachedEnemyActor = aiActorObject.GetComponent<AIActor>();

            if (!aiActorObject | !CachedEnemyActor) { return; }
            
            if (ExpandStats.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
            }

            GameObject m_NewHandObject = new GameObject("PlayerMimicHand");
            tk2dSprite m_HandSprite = m_NewHandObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(m_HandSprite, sourcePlayer.primaryHand.gameObject.GetComponent<tk2dSprite>());
            PlayerHandController m_HandController = m_NewHandObject.AddComponent<PlayerHandController>();
            m_HandController.ForceRenderersOff = false;
            m_HandController.handHeightFromGun = 0.05f;

            CachedEnemyActor.aiShooter.handObject = m_HandController;

            // Generate BossCard based on current Player.
            Texture2D BossCardForeground = ExpandUtility.FlipTexture(Instantiate(sourcePlayer.BosscardSprites[0]));
            // Mirror thing will be used as static background. (will be the same for all possible boss cards)
            Texture2D BossCardBackground = ExpandUtilities.ResourceExtractor.GetTextureFromResource("Textures\\GungeoneerMimicAssets\\MimicInMirror_BossCardBackground.png");
            // Combine foreground boss card generated from PlayerController onto the static background image loased in earlier. Resolutions must match!
            Texture2D BossCardTexture = ExpandUtility.CombineTextures(BossCardBackground, BossCardForeground);


            
            GenericIntroDoer miniBossIntroDoer = aiActorObject.AddComponent<GenericIntroDoer>();
            aiActorObject.AddComponent<ExpandGungeoneerMimicIntroDoer>();
            miniBossIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
            miniBossIntroDoer.initialDelay = 0.15f;
            miniBossIntroDoer.cameraMoveSpeed = 14;
            miniBossIntroDoer.specifyIntroAiAnimator = null;
            miniBossIntroDoer.BossMusicEvent = "Play_MUS_Boss_Theme_Beholster";
            miniBossIntroDoer.PreventBossMusic = false;
            miniBossIntroDoer.InvisibleBeforeIntroAnim = false;
            miniBossIntroDoer.preIntroAnim = string.Empty;
            miniBossIntroDoer.preIntroDirectionalAnim = string.Empty;
            miniBossIntroDoer.introAnim = "idle";
            miniBossIntroDoer.introDirectionalAnim = string.Empty;
            miniBossIntroDoer.continueAnimDuringOutro = false;
            miniBossIntroDoer.cameraFocus = null;
            miniBossIntroDoer.roomPositionCameraFocus = Vector2.zero;
            miniBossIntroDoer.restrictPlayerMotionToRoom = false;
            miniBossIntroDoer.fusebombLock = false;
            miniBossIntroDoer.AdditionalHeightOffset = 0;
            miniBossIntroDoer.portraitSlideSettings = new PortraitSlideSettings() {
                bossNameString = "Dopplegunner",
                bossSubtitleString = "Imposter!",
                bossQuoteString = "Clone gone rogue...",                    
                bossSpritePxOffset = IntVector2.Zero,
                topLeftTextPxOffset = IntVector2.Zero,
                bottomRightTextPxOffset = IntVector2.Zero,
                bgColor = new Color(0, 0, 1, 1)
            };
            if (BossCardTexture) {
                miniBossIntroDoer.portraitSlideSettings.bossArtSprite = BossCardTexture;
                miniBossIntroDoer.SkipBossCard = false;
                CachedEnemyActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.MainBar;
            } else {
                miniBossIntroDoer.SkipBossCard = true;
                CachedEnemyActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.SubbossBar;
            }
            miniBossIntroDoer.HideGunAndHand = true;
            miniBossIntroDoer.SkipFinalizeAnimation = true;
            miniBossIntroDoer.RegenerateCache();


            CachedEnemyActor.BaseMovementSpeed = 8f;
            CachedEnemyActor.MovementSpeed = 8f;

            CachedEnemyActor.healthHaver.SetHealthMaximum(1000);
            CachedEnemyActor.healthHaver.ForceSetCurrentHealth(1000);
            CachedEnemyActor.healthHaver.overrideBossName = "Dopplegunner";
            CachedEnemyActor.healthHaver.RegenerateCache();

            CachedEnemyActor.EnemyId = Random.Range(2000, 9999);
            CachedEnemyActor.EnemyGuid = System.Guid.NewGuid().ToString();
            CachedEnemyActor.OverrideDisplayName = ("Dopplegunner");
            CachedEnemyActor.ActorName = ("Dopplegunner");
            CachedEnemyActor.name = ("Dopplegunner");
            
            CachedEnemyActor.CanTargetEnemies = false;
            CachedEnemyActor.CanTargetPlayers = true;

            if (sourcePlayer.characterIdentity == PlayableCharacters.Bullet) {
                CachedEnemyActor.EnemySwitchState = "Metal_Bullet_Man";
            } else if (sourcePlayer.characterIdentity == PlayableCharacters.Convict) {
                CachedEnemyActor.EnemySwitchState = "Convict";
            } else if (sourcePlayer.characterIdentity == PlayableCharacters.CoopCultist) {
                CachedEnemyActor.EnemySwitchState = "Cultist";
            } else if (sourcePlayer.characterIdentity == PlayableCharacters.Cosmonaut) {
                CachedEnemyActor.EnemySwitchState = "Cosmonaut";
            } else if (sourcePlayer.characterIdentity == PlayableCharacters.Guide) {
                CachedEnemyActor.EnemySwitchState = "Guide";
            } else if (sourcePlayer.characterIdentity == PlayableCharacters.Gunslinger) {
                CachedEnemyActor.EnemySwitchState = "Gunslinger";
            } else if (sourcePlayer.characterIdentity == PlayableCharacters.Ninja) {
                CachedEnemyActor.EnemySwitchState = "Ninja";
            } else if (sourcePlayer.characterIdentity == PlayableCharacters.Pilot) {
                CachedEnemyActor.EnemySwitchState = "Rogue";
            } else if (sourcePlayer.characterIdentity == PlayableCharacters.Robot) {
                CachedEnemyActor.EnemySwitchState = "Robot";
            } else if (sourcePlayer.characterIdentity == PlayableCharacters.Soldier) {
                CachedEnemyActor.EnemySwitchState = "Marine";
            } else if (sourcePlayer.characterIdentity == PlayableCharacters.Eevee) {
                CachedEnemyActor.EnemySwitchState = "Convict";
                ExpandShaders.ApplyParadoxPlayerShader(CachedEnemyActor.sprite);
            } else { 
                CachedEnemyActor.EnemySwitchState = "Gun Cultist";
            }
            

            ExpandGungeoneerMimicBossController playerMimicController = aiActorObject.AddComponent<ExpandGungeoneerMimicBossController>();
            playerMimicController.m_Player = sourcePlayer;

            aiActorObject.AddComponent<ExpandGungeoneerMimicDeathController>();
            
            if (CachedEnemyActor.GetComponent<ExpandGungeoneerMimicIntroDoer>()) {
                GenericIntroDoer genericIntroDoer = CachedEnemyActor.gameObject.GetComponent<GenericIntroDoer>();
                FieldInfo field = typeof(GenericIntroDoer).GetField("m_specificIntroDoer", BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(genericIntroDoer, CachedEnemyActor.GetComponent<ExpandGungeoneerMimicIntroDoer>());
            }
            
            CachedEnemyActor.aiAnimator.enabled = false;
            CachedEnemyActor.spriteAnimator.Stop();
            CachedEnemyActor.spriteAnimator.DefaultClipId = 0;
            CachedEnemyActor.spriteAnimator.playAutomatically = false;

            List<tk2dSpriteAnimationClip> m_AnimationClips = new List<tk2dSpriteAnimationClip>();
            foreach (tk2dSpriteAnimationClip clip in sourcePlayer.spriteAnimator.Library.clips) {
                if (!string.IsNullOrEmpty(clip.name)) {
                    if (clip.name.ToLower() == "dodge") {
                        m_AnimationClips.Add(clip);
                        if (clip.frames != null && clip.frames.Length > 0) {
                            if (!sourcePlayer.UseArmorlessAnim) { CachedEnemyActor.sprite.SetSprite(clip.frames[0].spriteId); }
                        }
                    } else if (clip.name.ToLower() == "dodge_armorless") {
                        m_AnimationClips.Add(clip);
                        if (clip.frames != null && clip.frames.Length > 0) {
                            if (sourcePlayer.UseArmorlessAnim) { CachedEnemyActor.sprite.SetSprite(clip.frames[0].spriteId); }
                        }
                    } else if (clip.name.ToLower() == "run_down") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "run_down_armorless") {
                        m_AnimationClips.Add(clip);
                    } else if(clip.name.ToLower() == "death_shot") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "death_shot_armorless") {
                        m_AnimationClips.Add(clip);
                    }
                }
            }
            
            if (m_AnimationClips.Count > 0) {
                if (!CachedEnemyActor.spriteAnimator.Library) { CachedEnemyActor.spriteAnimator.Library = aiActorObject.AddComponent<tk2dSpriteAnimation>(); }
                CachedEnemyActor.spriteAnimator.Library.clips = m_AnimationClips.ToArray();
            }
            
            CachedEnemyActor.healthHaver.RegenerateCache();

            BehaviorSpeculator customBehaviorSpeculator = aiActorObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>(0);
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90f;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;
            customBehaviorSpeculator.RegenerateCache();

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<Object>(0);
            m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = new List<string>() { "[]", "[]", "[]", "[]", "[]" };
            
            CachedEnemyActor.RegenerateCache();
            return;
        }

        public ExpandGungeoneerMimicBossPlacable() { isSecretRatFloorBoss = true; }

        bool isSecretRatFloorBoss;

        private void Start() { }

        private void BuildBossPrefab(RoomHandler room) {
            string SpriteBasePath = "ExpandTheGungeon/Textures/GungeoneerMimicAssets/";

            List<string> m_MirrorMimicFadeInSprites = new List<string>() {
                "PlayerMimicMirror_MimicFadeIn_01",
                "PlayerMimicMirror_MimicFadeIn_02",
                "PlayerMimicMirror_MimicFadeIn_03",
                "PlayerMimicMirror_MimicFadeIn_04",
                "PlayerMimicMirror_MimicFadeIn_05",
                "PlayerMimicMirror_MimicFadeIn_06",
                "PlayerMimicMirror_MimicFadeIn_07",
                "PlayerMimicMirror_MimicFadeIn_08",
                "PlayerMimicMirror_MimicFadeIn_09",
                "PlayerMimicMirror_MimicFadeIn_10"
            };

            List<string> m_MirrorCrackSprites = new List<string>() {
                "PlayerMimicMirror_MimicCrack_01",
                "PlayerMimicMirror_MimicCrack_02",
                "PlayerMimicMirror_MimicCrack_03",
                "PlayerMimicMirror_MimicCrack_04",
                "PlayerMimicMirror_MimicCrack_05"
            };

            List<string> m_MirrorShatterFXSprites = new List<string>() {
                "PlayerMimicMirror_ShatterDebris_01",
                "PlayerMimicMirror_ShatterDebris_02",
                "PlayerMimicMirror_ShatterDebris_03",
                "PlayerMimicMirror_ShatterDebris_04",
                "PlayerMimicMirror_ShatterDebris_05",
                "PlayerMimicMirror_ShatterDebris_06",
                "PlayerMimicMirror_ShatterDebris_07",
                "PlayerMimicMirror_ShatterDebris_08",
                "PlayerMimicMirror_ShatterDebris_09",
                "PlayerMimicMirror_ShatterDebris_10"
            };
            
            IntVector2 SpawnPosition = (gameObject.transform.PositionVector2().ToIntVector2() - room.area.basePosition);

            PlayerController CurrentPlayer = GameManager.Instance.PrimaryPlayer;

            GameObject m_CachedNewObject = new GameObject("Gungeoneer Mimic") { layer = 28 };

            GenerateGungeoneerMimicBoss(m_CachedNewObject, CurrentPlayer);
            
            GameObject SpawnedBossObject = m_CachedNewObject.GetComponent<AIActor>().InstantiateObject(room, SpawnPosition, false);
            SpawnedBossObject.transform.parent = room.hierarchyParent;
            Destroy(m_CachedNewObject);


            if (isSecretRatFloorBoss) {
                PickupObject rat_key = PickupObjectDatabase.GetById(727);
                if (rat_key) { SpawnedBossObject.GetComponent<AIActor>().AdditionalSafeItemDrops.Add(rat_key); }
            }

            PickupObject MimiclayItem = PickupObjectDatabase.GetById(Mimiclay.MimiclayPickupID);

            if (MimiclayItem) {
                SpawnedBossObject.GetComponent<AIActor>().AdditionalSafeItemDrops.Add(MimiclayItem);
            }

            MirrorController mirror = ExpandPrefabs.CurrsedMirrorPlacable.gameObject.GetComponent<MirrorController>();

            GameObject MimicMirrorObject = new GameObject("MimicMirrorBase");
            MimicMirrorObject.transform.position = (SpawnedBossObject.transform.position - new Vector3(0.25f, 1));
            MimicMirrorObject.transform.parent = gameObject.transform;
            ItemBuilder.AddSpriteToObject(MimicMirrorObject, (SpriteBasePath + "PlayerMimicMirror_Base"), false, false);

            tk2dSprite MirrorBaseSprite = MimicMirrorObject.GetComponent<tk2dSprite>();

            SpriteBuilder.AddSpriteToCollection((SpriteBasePath + "PlayerMimicMirror_Broken"), MirrorBaseSprite.Collection);
            
            foreach (string SpriteName in m_MirrorMimicFadeInSprites) { SpriteBuilder.AddSpriteToCollection((SpriteBasePath + SpriteName), MirrorBaseSprite.Collection); }
            foreach (string SpriteName in m_MirrorCrackSprites) { SpriteBuilder.AddSpriteToCollection(SpriteBasePath + SpriteName, MirrorBaseSprite.Collection); }
            
            ExpandUtility.GenerateSpriteAnimator(MimicMirrorObject, AnimateDuringBossIntros: true, AlwaysIgnoreTimeScale: true, ignoreTimeScale: true);
            ExpandUtility.AddAnimation(MimicMirrorObject.GetComponent<tk2dSpriteAnimator>(), MirrorBaseSprite.Collection, m_MirrorMimicFadeInSprites, "PlayerMimicFadeIn", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(MimicMirrorObject.GetComponent<tk2dSpriteAnimator>(), MirrorBaseSprite.Collection, m_MirrorCrackSprites, "MirrorGlassCrack", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            
            ExpandGungeoneerMimicIntroDoer playerMimicBossIntroDoer = SpawnedBossObject.GetComponent<ExpandGungeoneerMimicIntroDoer>();
            playerMimicBossIntroDoer.MirrorBase = MimicMirrorObject;
            playerMimicBossIntroDoer.ShatterSystem = Instantiate(mirror.ShatterSystem, MimicMirrorObject.transform.position, Quaternion.identity);
            playerMimicBossIntroDoer.ShatterSystem.SetActive(false);
            playerMimicBossIntroDoer.ShatterSystem.transform.parent = MimicMirrorObject.transform;

            GameObject MimicMirrorFXObject = new GameObject("MirrorShatterFX");
            MimicMirrorFXObject.transform.position = (MimicMirrorObject.transform.position - Vector3.one);
            MimicMirrorFXObject.transform.parent = gameObject.transform.parent;
            ItemBuilder.AddSpriteToObject(MimicMirrorFXObject, (SpriteBasePath + "PlayerMimicMirror_ShatterDebris_01"), false, false);
            foreach (string SpriteName in m_MirrorShatterFXSprites) { SpriteBuilder.AddSpriteToCollection((SpriteBasePath + SpriteName), MimicMirrorFXObject.GetComponent<tk2dSprite>().Collection); }
            ExpandUtility.GenerateSpriteAnimator(MimicMirrorFXObject, AnimateDuringBossIntros: true, AlwaysIgnoreTimeScale: true, ignoreTimeScale: true);
            ExpandUtility.AddAnimation(MimicMirrorFXObject.GetComponent<tk2dSpriteAnimator>(), MimicMirrorFXObject.GetComponent<tk2dSprite>().Collection, m_MirrorShatterFXSprites, "PlayerMimicShatter", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            MimicMirrorFXObject.GetComponent<tk2dSprite>().HeightOffGround = 3.5f;
            MimicMirrorFXObject.GetComponent<tk2dSprite>().UpdateZDepth();
            playerMimicBossIntroDoer.MirrorShatterFX = MimicMirrorFXObject;
            MimicMirrorFXObject.SetActive(false);
        }

        
        public void ConfigureOnPlacement(RoomHandler room) {
            gameObject.SetActive(true);
            enabled = true;

            BuildBossPrefab(room);
        }

        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

