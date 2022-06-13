using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Dungeonator;
using Pathfinding;
using tk2dRuntime.TileMap;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandDungeonFlows;
using ExpandTheGungeon.ExpandMain;

namespace ExpandTheGungeon.ExpandUtilities {

    public class ExpandUtility {

        public static DungeonPlaceable DuplicateDungoenPlaceable(DungeonPlaceable sourcePlaceable) {
            DungeonPlaceable m_cachedPlaceable = ScriptableObject.CreateInstance<DungeonPlaceable>();
            m_cachedPlaceable.width = sourcePlaceable.width;
            m_cachedPlaceable.height = sourcePlaceable.height;
            m_cachedPlaceable.isPassable = sourcePlaceable.isPassable;
            m_cachedPlaceable.roomSequential = sourcePlaceable.roomSequential;
            m_cachedPlaceable.respectsEncounterableDifferentiator = sourcePlaceable.respectsEncounterableDifferentiator;
            m_cachedPlaceable.UsePrefabTransformOffset = sourcePlaceable.UsePrefabTransformOffset;
            m_cachedPlaceable.MarkSpawnedItemsAsRatIgnored = sourcePlaceable.MarkSpawnedItemsAsRatIgnored;
            m_cachedPlaceable.DebugThisPlaceable = sourcePlaceable.DebugThisPlaceable;
            m_cachedPlaceable.IsAnnexTable = sourcePlaceable.IsAnnexTable;
            m_cachedPlaceable.variantTiers = new List<DungeonPlaceableVariant>();

            if (sourcePlaceable.variantTiers != null && sourcePlaceable.variantTiers.Count > 0) {
                for (int i = 0; i < sourcePlaceable.variantTiers.Count; i++) {

                    DungeonPlaceableVariant sourceVarient = sourcePlaceable.variantTiers[i];

                    List<DungeonPrerequisite> m_CachedPrequisitesList = new List<DungeonPrerequisite>();
                    List<DungeonPlaceableRoomMaterialRequirement> m_CachedMaterialRequirementsList = new List<DungeonPlaceableRoomMaterialRequirement>();

                    m_cachedPlaceable.variantTiers.Add(new DungeonPlaceableVariant() {
                        percentChance = sourceVarient.percentChance,
                        unitOffset = sourceVarient.unitOffset,
                        nonDatabasePlaceable = sourceVarient.nonDatabasePlaceable,
                        enemyPlaceableGuid = sourceVarient.enemyPlaceableGuid,
                        forceBlackPhantom = sourceVarient.forceBlackPhantom,
                        addDebrisObject = sourceVarient.addDebrisObject,
                    });

                    DungeonPlaceableVariant cachedVarient = m_cachedPlaceable.variantTiers[i];
                                        
                    if (sourceVarient.prerequisites != null && sourceVarient.prerequisites.Length > 0) {
                        foreach (DungeonPrerequisite prerequisite in sourceVarient.prerequisites) {
                            m_CachedPrequisitesList.Add(new DungeonPrerequisite() {
                                comparisonValue = prerequisite.comparisonValue,
                                encounteredObjectGuid = prerequisite.encounteredObjectGuid,
                                encounteredRoom = prerequisite.encounteredRoom,
                                maxToCheck = prerequisite.maxToCheck,
                                prerequisiteOperation = prerequisite.prerequisiteOperation,
                                prerequisiteType = prerequisite.prerequisiteType,
                                requireCharacter = prerequisite.requireCharacter,
                                requiredCharacter = prerequisite.requiredCharacter,
                                requireDemoMode = prerequisite.requireDemoMode,
                                requiredNumberOfEncounters = prerequisite.requiredNumberOfEncounters,
                                requiredTileset = prerequisite.requiredTileset,
                                requireFlag = prerequisite.requireFlag,
                                requireTileset = prerequisite.requireTileset,
                                saveFlagToCheck = prerequisite.saveFlagToCheck,
                                statToCheck = prerequisite.statToCheck,
                                useSessionStatValue = prerequisite.useSessionStatValue
                            });
                        }
                    }

                    if (sourceVarient.materialRequirements != null && sourceVarient.materialRequirements.Length > 0) {
                        foreach (DungeonPlaceableRoomMaterialRequirement materialRequirement in sourceVarient.materialRequirements) {
                            m_CachedMaterialRequirementsList.Add(new DungeonPlaceableRoomMaterialRequirement() {
                                RequireMaterial = materialRequirement.RequireMaterial,
                                RoomMaterial = materialRequirement.RoomMaterial,
                                TargetTileset = materialRequirement.TargetTileset
                            });
                        }
                    }

                    cachedVarient.prerequisites = m_CachedPrequisitesList.ToArray();
                    cachedVarient.materialRequirements = m_CachedMaterialRequirementsList.ToArray();
                }
            }

            return m_cachedPlaceable;
        }

        public static void SpawnCustomCurrency(Vector2 centerPoint, int amountToDrop, int currencyItemID) {
            List<GameObject> currencyToDrop = GetCustomCurrencyToDrop(currencyItemID, amountToDrop);
            float num = 360f / currencyToDrop.Count;
            Vector3 up = Vector3.up;
            List<CurrencyPickup> list = new List<CurrencyPickup>();
            for (int i = 0; i < currencyToDrop.Count; i++) {
                Vector3 vector = Quaternion.Euler(0f, 0f, num * i) * up;
                vector *= 2f;
                GameObject gameObject = SpawnManager.SpawnDebris(currencyToDrop[i], centerPoint.ToVector3ZUp(centerPoint.y), Quaternion.identity);
                CurrencyPickup component = gameObject.GetComponent<CurrencyPickup>();
                component.PreventPickup = true;
                list.Add(component);
                /*PickupMover component2 = gameObject.GetComponent<PickupMover>();
                if (component2) { component2.enabled = false; }*/
                DebrisObject orAddComponent = gameObject.GetOrAddComponent<DebrisObject>();
                DebrisObject debrisObject = orAddComponent;
                debrisObject.OnGrounded = (Action<DebrisObject>)Delegate.Combine(debrisObject.OnGrounded, new Action<DebrisObject>(delegate (DebrisObject sourceDebris) {
                    sourceDebris.GetComponent<CurrencyPickup>().PreventPickup = false;
                    sourceDebris.OnGrounded = null;
                }));
                orAddComponent.shouldUseSRBMotion = true;
                orAddComponent.angularVelocity = 0f;
                orAddComponent.Priority = EphemeralObject.EphemeralPriority.Critical;
                orAddComponent.Trigger(vector.WithZ(2f) * UnityEngine.Random.Range(1.5f, 2.125f), 0.05f, 1f);
                orAddComponent.canRotate = false;
            }
        }

        public static List<GameObject> GetCustomCurrencyToDrop(int itemID, int amountToDrop) {
            List<GameObject> list = new List<GameObject>();

            PickupObject pickupObject = PickupObjectDatabase.GetById(itemID);

            if (!pickupObject) { return list; }

            int currencyValue = PickupObjectDatabase.GetById(itemID).GetComponent<CurrencyPickup>().currencyValue;
            while (amountToDrop > 0) {
                GameObject currencyObject = null;
                if (amountToDrop >= currencyValue) {
                    amountToDrop -= currencyValue;
                    currencyObject = pickupObject.gameObject;
                }
                if (currencyObject) { list.Add(currencyObject); }
            }
            return list;
        }

        // Better method of defining new node positions on a path. (allows defining placement type at same time as creating it)
        public static SerializedPathNode GeneratePathNode(IntVector2 position, SerializedPathNode.SerializedNodePlacement placement, bool usesAlternateTarget = false, float delayTime = 0, int alternateTargetPathIndex = -1, int alternateTargetNodeIndex = -1) {
            SerializedPathNode m_PathNode = new SerializedPathNode();
            m_PathNode.position = position;
            m_PathNode.delayTime = delayTime;
            m_PathNode.placement = placement;
            m_PathNode.UsesAlternateTarget = usesAlternateTarget;
            m_PathNode.AlternateTargetPathIndex = alternateTargetPathIndex;
            m_PathNode.AlternateTargetNodeIndex = alternateTargetNodeIndex;
            return m_PathNode;
        }

        // Quick and dirty way to clone any (non engine) component.
        // Can't be used to clone things like Texture2D/Materials. But useful for most other things things like components and scriptable objects like DungeonFlows.
        public static void DuplicateComponent(object target, object source, bool SaveOutputToFile = false, string OutputFilepath = null) {
            if (string.IsNullOrEmpty(JsonUtility.ToJson(source))) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: ExpandUtility.DuplicateComponent returned null due to null source on target: " + target.ToString(), true);
                return;
            }
            if (SaveOutputToFile && !string.IsNullOrEmpty(OutputFilepath)) { Tools.LogStringToFile(JsonUtility.ToJson(source), OutputFilepath); }
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(source), target);
        }

        public static void ReflectionShallowCopyFields<T>(T target, T source, BindingFlags flags) {
            foreach (var field in typeof(T).GetFields(flags)) {
                field.SetValue(target, field.GetValue(source));
            }
        }

        public static AIActor BuildNewAIActor(GameObject targetObject, string EnemyName, string EnemyGUID, float EnemyHealth = 15, tk2dSprite spriteSource = null, Transform gunAttachObjectOverride = null, Vector3? GunAttachOffset = null, int StartingGunID = 38, List<PixelCollider> customColliders = null, bool RigidBodyCollidesWithTileMap = true, bool RigidBodyCollidesWithOthers = true, bool RigidBodyCanBeCarried = true, bool RigidBodyCanBePushed = false, bool isFakePrefab = false, GameObject ExternalCorpseObject = null, bool EnemyHasNoShooter = false, bool EnemyHasNoCorpse = false) {

            if (!targetObject | targetObject.GetComponent<AIActor>()) { return null; }

            Transform m_CachedGunAttachPoint = gunAttachObjectOverride;
                        
            if (!m_CachedGunAttachPoint && !EnemyHasNoShooter && targetObject.transform.Find("GunAttachPoint")) {
                m_CachedGunAttachPoint = targetObject.transform.Find("GunAttachPoint");
                if (GunAttachOffset.HasValue) {
                    m_CachedGunAttachPoint.transform.localPosition = GunAttachOffset.Value;
                }
            } else if (m_CachedGunAttachPoint && !EnemyHasNoShooter && !targetObject.transform.Find("GunAttachPoint")) {
                if (GunAttachOffset.HasValue) {
                    m_CachedGunAttachPoint.transform.position = GunAttachOffset.Value;
                } else {
                    m_CachedGunAttachPoint.transform.position = new Vector3(0.3125f, 0.25f, 0);
                }
                m_CachedGunAttachPoint.transform.parent = targetObject.transform;
            } else if (!m_CachedGunAttachPoint && !EnemyHasNoShooter && !targetObject.transform.Find("GunAttachPoint")) {
                GameObject gunAttachPoint = new GameObject("GunAttachPoint") { layer = 0 };
                m_CachedGunAttachPoint = gunAttachPoint.transform;
                if (GunAttachOffset.HasValue) {
                    m_CachedGunAttachPoint.transform.position = GunAttachOffset.Value;
                } else {
                    m_CachedGunAttachPoint.transform.position = new Vector3(0.3125f, 0.25f, 0);
                }
                m_CachedGunAttachPoint.transform.parent = targetObject.transform;
            } 
            

            if (!targetObject.GetComponent<tk2dSprite>() && spriteSource) {
                tk2dSprite newSprite = targetObject.AddComponent<tk2dSprite>();
                DuplicateComponent(newSprite, spriteSource);
            }

            if (!targetObject.GetComponent<SpeculativeRigidbody>()) {
                if (customColliders != null) {
                    foreach (PixelCollider collider in customColliders) {
                        int SizeX = collider.ManualWidth;
                        int SizeY = collider.ManualHeight;
                        int OffsetX = collider.ManualOffsetX;
                        int OffsetY = collider.ManualOffsetY;
                        GenerateOrAddToRigidBody(targetObject, collider.CollisionLayer, collider.ColliderGenerationMode, RigidBodyCollidesWithTileMap, RigidBodyCollidesWithOthers, RigidBodyCanBeCarried, RigidBodyCanBePushed, false, collider.IsTrigger, false, true, new IntVector2(SizeX, SizeY), new IntVector2(OffsetX, OffsetY));
                    }
                } else {
                    GenerateOrAddToRigidBody(targetObject, CollisionLayer.EnemyCollider, PixelCollider.PixelColliderGeneration.Manual, true, true, true, false, false, false, false, true, new IntVector2(12, 4), new IntVector2(5, 0));
                    GenerateOrAddToRigidBody(targetObject, CollisionLayer.EnemyHitBox, PixelCollider.PixelColliderGeneration.Manual, true, true, true, false, false, false, false, true, new IntVector2(12, 23), new IntVector2(5, 0));
                }

                SpeculativeRigidbody targetRigidBody = targetObject.GetComponent<SpeculativeRigidbody>();
                tk2dSprite targetSprite = targetObject.GetComponent<tk2dSprite>();

                targetRigidBody.Reinitialize();
                if (customColliders == null && targetSprite) { targetRigidBody.PixelColliders[1].Sprite = targetSprite; }
            }
                        
            if (!targetObject.GetComponent<tk2dSpriteAnimator>()) {
                GenerateSpriteAnimator(targetObject, null, 0, 0, false, false, false, false, true, false, 0, 0, false);
            }
            
            if (!targetObject.GetComponent<HealthHaver>()) {
                GenerateHealthHaver(targetObject, EnemyHealth, false, false, OnDeathBehavior.DeathType.Death, true, false, false, false, true, true);
            }
            
            if (!targetObject.GetComponent<HitEffectHandler>()) { targetObject.AddComponent<HitEffectHandler>(); }

            HitEffectHandler hitEffectHandler = targetObject.GetComponent<HitEffectHandler>();
            hitEffectHandler.overrideHitEffect = new VFXComplex() { effects = new VFXObject[0] };
            hitEffectHandler.overrideHitEffectPool = new VFXPool() { effects = new VFXComplex[0] };
            hitEffectHandler.additionalHitEffects = new HitEffectHandler.AdditionalHitEffect[0];
            hitEffectHandler.SuppressAllHitEffects = false;
            
            if (!targetObject.GetComponent<KnockbackDoer>()) { targetObject.AddComponent<KnockbackDoer>(); }

            KnockbackDoer knockBackDoer = targetObject.GetComponent<KnockbackDoer>();
            knockBackDoer.weight = 35;
            knockBackDoer.deathMultiplier = 2.5f;
            knockBackDoer.knockbackWhileReflecting = false;
            knockBackDoer.shouldBounce = true;
            knockBackDoer.collisionDecay = 0.5f;

            if (!targetObject.GetComponent<AIAnimator>()) { GenerateBlankAIAnimator(targetObject); }

            if (!targetObject.GetComponent<ObjectVisibilityManager>()) { targetObject.AddComponent<ObjectVisibilityManager>(); }

            ObjectVisibilityManager visibilityManager = targetObject.GetComponent<ObjectVisibilityManager>();
            visibilityManager.SuppressPlayerEnteredRoom = false;

            AIActor bulletManTemplate = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5"); // bullet_kin

            if ((!targetObject.GetComponent<AIShooter>() | !targetObject.GetComponent<AIBulletBank>()) && !EnemyHasNoShooter) {
                DuplicateAIShooterAndAIBulletBank(targetObject, bulletManTemplate.gameObject.GetComponent<AIShooter>(), bulletManTemplate.gameObject.GetComponent<AIBulletBank>(), StartingGunID, m_CachedGunAttachPoint.transform);
            }
            
            AIActor m_CachedAIActor = targetObject.AddComponent<AIActor>();
            DuplicateComponent(m_CachedAIActor, bulletManTemplate);
            m_CachedAIActor.ActorName = EnemyName;
            m_CachedAIActor.OverrideDisplayName = EnemyName;
            m_CachedAIActor.EnemyId = UnityEngine.Random.Range(10000, 99999);
            m_CachedAIActor.EnemyGuid = EnemyGUID;
            m_CachedAIActor.ForcedPositionInAmmonomicon = -1;
            if (EnemyHasNoCorpse) {
                m_CachedAIActor.CorpseObject = null;
                m_CachedAIActor.CorpseShadow = false;
                m_CachedAIActor.TransferShadowToCorpse = false;
                m_CachedAIActor.shadowDeathType = AIActor.ShadowDeathType.Fade;
            } else if (ExternalCorpseObject) {
                m_CachedAIActor.CorpseObject = ExternalCorpseObject;
            }
            
            bulletManTemplate = null;
            return m_CachedAIActor;
        }
        
        public static tk2dSpriteAnimator DuplicateSpriteAnimator(GameObject targetObject, tk2dSpriteAnimator sourceAnimator, bool duplicateAnimationData = false, tk2dSpriteCollectionData overrideSpriteCollection = null) {
            tk2dSpriteAnimator targetAnimator;

            if (targetObject.GetComponent<tk2dSpriteAnimator>()) {
                targetAnimator = targetObject.GetComponent<tk2dSpriteAnimator>();
            } else {
                targetAnimator = targetObject.AddComponent<tk2dSpriteAnimator>();
            }

            if (!string.IsNullOrEmpty(sourceAnimator.name)) { targetAnimator.name = sourceAnimator.name; }
            targetAnimator.DefaultClipId = sourceAnimator.DefaultClipId;
            targetAnimator.AdditionalCameraVisibilityRadius = sourceAnimator.AdditionalCameraVisibilityRadius;
            targetAnimator.AnimateDuringBossIntros = sourceAnimator.AnimateDuringBossIntros;
            targetAnimator.AlwaysIgnoreTimeScale = sourceAnimator.AlwaysIgnoreTimeScale;
            targetAnimator.ForceSetEveryFrame = sourceAnimator.ForceSetEveryFrame;
            targetAnimator.playAutomatically = sourceAnimator.playAutomatically;
            targetAnimator.IsFrameBlendedAnimation = sourceAnimator.IsFrameBlendedAnimation;
            targetAnimator.clipTime = sourceAnimator.clipTime;
            targetAnimator.deferNextStartClip = sourceAnimator.deferNextStartClip;
            if (!duplicateAnimationData) {
                targetAnimator.Library = sourceAnimator.Library;
            } else if (sourceAnimator.Library != null && sourceAnimator.Library.clips != null) {
                tk2dSpriteAnimation m_NewSpriteAnimation;
                if (targetObject.GetComponent<tk2dSpriteAnimation>()) {
                    m_NewSpriteAnimation = targetObject.GetComponent<tk2dSpriteAnimation>();
                } else {
                    m_NewSpriteAnimation = targetObject.AddComponent<tk2dSpriteAnimation>();
                }
                List<tk2dSpriteAnimationClip> m_clipList = new List<tk2dSpriteAnimationClip>();
                foreach (tk2dSpriteAnimationClip clip in sourceAnimator.Library.clips) { m_clipList.Add(DuplicateAnimationClip(clip, overrideSpriteCollection)); }
                m_NewSpriteAnimation.clips = m_clipList.ToArray();
            }

            return targetAnimator;
        }

        public static void DuplicateSpriteAnimator(tk2dSpriteAnimator targetAnimator, tk2dSpriteAnimator sourceAnimator, bool duplicateAnimationData = false) {
            if (!string.IsNullOrEmpty(sourceAnimator.name)) { targetAnimator.name = sourceAnimator.name; }
            targetAnimator.DefaultClipId = sourceAnimator.DefaultClipId;
            targetAnimator.AdditionalCameraVisibilityRadius = sourceAnimator.AdditionalCameraVisibilityRadius;
            targetAnimator.AnimateDuringBossIntros = sourceAnimator.AnimateDuringBossIntros;
            targetAnimator.AlwaysIgnoreTimeScale = sourceAnimator.AlwaysIgnoreTimeScale;
            targetAnimator.ForceSetEveryFrame = sourceAnimator.ForceSetEveryFrame;
            targetAnimator.playAutomatically = sourceAnimator.playAutomatically;
            targetAnimator.IsFrameBlendedAnimation = sourceAnimator.IsFrameBlendedAnimation;
            targetAnimator.clipTime = sourceAnimator.clipTime;
            targetAnimator.deferNextStartClip = sourceAnimator.deferNextStartClip;
            if (!duplicateAnimationData) {
                targetAnimator.Library = sourceAnimator.Library;
            } else if (sourceAnimator.Library != null && sourceAnimator.Library.clips != null) {
                tk2dSpriteAnimation m_NewSpriteAnimation;
                if (targetAnimator.gameObject.GetComponent<tk2dSpriteAnimation>()) {
                    m_NewSpriteAnimation = targetAnimator.gameObject.GetComponent<tk2dSpriteAnimation>();
                } else {
                    m_NewSpriteAnimation = targetAnimator.gameObject.AddComponent<tk2dSpriteAnimation>();
                }
                List<tk2dSpriteAnimationClip> m_clipList = new List<tk2dSpriteAnimationClip>();
                foreach (tk2dSpriteAnimationClip clip in sourceAnimator.Library.clips) { m_clipList.Add(DuplicateAnimationClip(clip)); }
                m_NewSpriteAnimation.clips = m_clipList.ToArray();
            }
        }

        public static ExpandNoteDoer BuildNewCustomSign(GameObject TargetPrefab, GameObject PrefabToClone, string SignName, string SignText) {
            
            if (!TargetPrefab | !TargetPrefab.transform.Find("nooto pointo")) { return null; }
            
            Transform m_CachedNewSignTransform = TargetPrefab.transform.Find("nooto pointo");
            Transform m_CachedNewShadow = TargetPrefab.transform.Find("Sign_Shadow");
            Transform m_SignShadowToClone = PrefabToClone.transform.Find("Sign_Shadow");

            if (m_CachedNewShadow && m_SignShadowToClone) {
                tk2dSprite signShadow = m_CachedNewShadow.gameObject.AddComponent<tk2dSprite>();
                DuplicateSprite(signShadow, m_SignShadowToClone.GetComponent<tk2dSprite>());
            }

            tk2dSprite m_NewSignSprite = TargetPrefab.AddComponent<tk2dSprite>();
            DuplicateSprite(m_NewSignSprite, PrefabToClone.GetComponent<tk2dSprite>());
            SpeculativeRigidbody m_NewSignRigidBody = TargetPrefab.AddComponent<SpeculativeRigidbody>();
            DuplicateRigidBody(m_NewSignRigidBody, PrefabToClone.GetComponent<SpeculativeRigidbody>());

            tk2dSpriteAnimator m_SignAnimatorToClone = PrefabToClone.GetComponent<tk2dSpriteAnimator>();
            tk2dSpriteAnimator m_NewSignAnimator = TargetPrefab.AddComponent<tk2dSpriteAnimator>();
            m_NewSignAnimator.Library = m_SignAnimatorToClone.Library;
            m_NewSignAnimator.DefaultClipId = m_SignAnimatorToClone.DefaultClipId;
            m_NewSignAnimator.AdditionalCameraVisibilityRadius = m_SignAnimatorToClone.AdditionalCameraVisibilityRadius;
            m_NewSignAnimator.AnimateDuringBossIntros = m_SignAnimatorToClone.AnimateDuringBossIntros;
            m_NewSignAnimator.AlwaysIgnoreTimeScale = m_SignAnimatorToClone.AlwaysIgnoreTimeScale;
            m_NewSignAnimator.ForceSetEveryFrame = m_SignAnimatorToClone.ForceSetEveryFrame;
            m_NewSignAnimator.playAutomatically = m_SignAnimatorToClone.playAutomatically;
            m_NewSignAnimator.IsFrameBlendedAnimation = m_SignAnimatorToClone.IsFrameBlendedAnimation;
            m_NewSignAnimator.clipTime = m_SignAnimatorToClone.clipTime;
            m_NewSignAnimator.deferNextStartClip = m_SignAnimatorToClone.deferNextStartClip;

            MajorBreakable m_NewSignBreakable = DuplicateMajorBreakable(TargetPrefab, PrefabToClone.GetComponent<MajorBreakable>(), new List<GameObject>() { m_CachedNewShadow.gameObject });

            ExpandNoteDoer SignComponent = TargetPrefab.AddComponent<ExpandNoteDoer>();
            SignComponent.textboxSpawnPoint = m_CachedNewSignTransform;
            SignComponent.name = SignName;
            SignComponent.stringKey = SignText;

            return SignComponent;
        }

        public static MajorBreakable DuplicateMajorBreakable(GameObject TargetObject, MajorBreakable sourceBreakable, List<GameObject> childrenToDestroy = null, int ItemIDToSpawnOnBreak = -1) {
            if (TargetObject.GetComponent<MajorBreakable>()) { return null; };

            MajorBreakable m_NewBreakable = TargetObject.AddComponent<MajorBreakable>();
            m_NewBreakable.HitPoints = sourceBreakable.HitPoints;
            m_NewBreakable.DamageReduction = sourceBreakable.DamageReduction;
            m_NewBreakable.MinHits = sourceBreakable.MinHits;
            m_NewBreakable.EnemyDamageOverride = sourceBreakable.EnemyDamageOverride;
            m_NewBreakable.ImmuneToBeastMode = sourceBreakable.ImmuneToBeastMode;
            m_NewBreakable.ScaleWithEnemyHealth = sourceBreakable.ScaleWithEnemyHealth;
            m_NewBreakable.OnlyExplosions = sourceBreakable.OnlyExplosions;
            m_NewBreakable.IgnoreExplosions = sourceBreakable.IgnoreExplosions;
            m_NewBreakable.GameActorMotionBreaks = sourceBreakable.GameActorMotionBreaks;
            m_NewBreakable.PlayerRollingBreaks = sourceBreakable.PlayerRollingBreaks;
            m_NewBreakable.spawnShards = sourceBreakable.spawnShards;
            m_NewBreakable.distributeShards = sourceBreakable.distributeShards;
            m_NewBreakable.shardClusters = sourceBreakable.shardClusters;
            m_NewBreakable.minShardPercentSpeed = sourceBreakable.minShardPercentSpeed;
            m_NewBreakable.maxShardPercentSpeed = sourceBreakable.maxShardPercentSpeed;
            m_NewBreakable.shardBreakStyle = sourceBreakable.shardBreakStyle;
            m_NewBreakable.usesTemporaryZeroHitPointsState = sourceBreakable.usesTemporaryZeroHitPointsState;
            m_NewBreakable.spriteNameToUseAtZeroHP = sourceBreakable.spriteNameToUseAtZeroHP;
            m_NewBreakable.destroyedOnBreak = sourceBreakable.destroyedOnBreak;
            if (childrenToDestroy != null) {
                m_NewBreakable.childrenToDestroy = childrenToDestroy;
            } else {
                m_NewBreakable.childrenToDestroy = new List<GameObject>(0);
            }
            m_NewBreakable.playsAnimationOnNotBroken = sourceBreakable.playsAnimationOnNotBroken;
            m_NewBreakable.notBreakAnimation = sourceBreakable.notBreakAnimation;
            m_NewBreakable.handlesOwnBreakAnimation = sourceBreakable.handlesOwnBreakAnimation;
            m_NewBreakable.breakAnimation = sourceBreakable.breakAnimation;
            m_NewBreakable.handlesOwnPrebreakFrames = sourceBreakable.handlesOwnPrebreakFrames;
            m_NewBreakable.prebreakFrames = sourceBreakable.prebreakFrames;
            m_NewBreakable.damageVfx = sourceBreakable.damageVfx;
            m_NewBreakable.damageVfxMinTimeBetween = sourceBreakable.damageVfxMinTimeBetween;
            m_NewBreakable.breakVfxParent = sourceBreakable.breakVfxParent;
            m_NewBreakable.delayDamageVfx = sourceBreakable.delayDamageVfx;
            
            if (ItemIDToSpawnOnBreak != -1) {
                m_NewBreakable.SpawnItemOnBreak = true;
                m_NewBreakable.ItemIdToSpawnOnBreak = ItemIDToSpawnOnBreak;
            } else {
                m_NewBreakable.SpawnItemOnBreak = sourceBreakable.SpawnItemOnBreak;
                m_NewBreakable.ItemIdToSpawnOnBreak = sourceBreakable.ItemIdToSpawnOnBreak;
            }

            m_NewBreakable.HandlePathBlocking = sourceBreakable.HandlePathBlocking;

            return m_NewBreakable;
        }
        
        public static tk2dSpriteCollectionData DuplicateDungeonCollection(GameObject TargetObject, tk2dSpriteCollectionData sourceCollection, string Name) {

            tk2dSpriteCollectionData m_NewDungeonCollection = TargetObject.AddComponent<tk2dSpriteCollectionData>();
            m_NewDungeonCollection.version = 3;
            m_NewDungeonCollection.name = string.Empty;
            m_NewDungeonCollection.materialIdsValid = sourceCollection;
            m_NewDungeonCollection.needMaterialInstance = sourceCollection;
            m_NewDungeonCollection.SpriteIDsWithBagelColliders = sourceCollection.SpriteIDsWithBagelColliders;
            m_NewDungeonCollection.SpriteDefinedBagelColliders = sourceCollection.SpriteDefinedBagelColliders;
            m_NewDungeonCollection.SpriteIDsWithAttachPoints = sourceCollection.SpriteIDsWithAttachPoints;
            m_NewDungeonCollection.SpriteDefinedAttachPoints = sourceCollection.SpriteDefinedAttachPoints;
            m_NewDungeonCollection.SpriteIDsWithNeighborDependencies = sourceCollection.SpriteIDsWithNeighborDependencies;
            m_NewDungeonCollection.SpriteDefinedIndexNeighborDependencies = sourceCollection.SpriteDefinedIndexNeighborDependencies;
            m_NewDungeonCollection.SpriteIDsWithAnimationSequences = sourceCollection.SpriteIDsWithAnimationSequences;
            m_NewDungeonCollection.SpriteDefinedAnimationSequences = sourceCollection.SpriteDefinedAnimationSequences;
            m_NewDungeonCollection.premultipliedAlpha = sourceCollection.premultipliedAlpha;
            m_NewDungeonCollection.shouldGenerateTilemapReflectionData = sourceCollection.shouldGenerateTilemapReflectionData;
            m_NewDungeonCollection.materials = sourceCollection.materials;
            m_NewDungeonCollection.textures = sourceCollection.textures;
            m_NewDungeonCollection.pngTextures = sourceCollection.pngTextures;
            m_NewDungeonCollection.materialPngTextureId = sourceCollection.materialPngTextureId;
            m_NewDungeonCollection.textureFilterMode = sourceCollection.textureFilterMode;
            m_NewDungeonCollection.textureMipMaps = sourceCollection.textureMipMaps;
            m_NewDungeonCollection.allowMultipleAtlases = sourceCollection.allowMultipleAtlases;
            m_NewDungeonCollection.spriteCollectionGUID = Guid.NewGuid().ToString();
            m_NewDungeonCollection.spriteCollectionName = Name;
            m_NewDungeonCollection.assetName = string.Empty;
            m_NewDungeonCollection.loadable = sourceCollection.loadable;
            m_NewDungeonCollection.invOrthoSize = sourceCollection.invOrthoSize;
            m_NewDungeonCollection.halfTargetHeight = sourceCollection.halfTargetHeight;
            m_NewDungeonCollection.buildKey = sourceCollection.buildKey;
            m_NewDungeonCollection.dataGuid = Guid.NewGuid().ToString();
            m_NewDungeonCollection.managedSpriteCollection = sourceCollection.managedSpriteCollection;
            m_NewDungeonCollection.hasPlatformData = sourceCollection.hasPlatformData;
            m_NewDungeonCollection.spriteCollectionPlatforms = sourceCollection.spriteCollectionPlatforms;
            m_NewDungeonCollection.spriteCollectionPlatformGUIDs = sourceCollection.spriteCollectionPlatformGUIDs;

            if (sourceCollection.spriteDefinitions != null && sourceCollection.spriteDefinitions.Length < 0) {
                List<tk2dSpriteDefinition> m_SpriteDefinitions = new List<tk2dSpriteDefinition>();
                foreach (tk2dSpriteDefinition spriteDefinition in sourceCollection.spriteDefinitions) {
                    m_SpriteDefinitions.Add(DuplicateSpriteDefinition(spriteDefinition));
                }
                m_NewDungeonCollection.InitDictionary();
            } else {
                m_NewDungeonCollection.spriteDefinitions = new tk2dSpriteDefinition[0];
            }

            return m_NewDungeonCollection;
        }

        public static tk2dSpriteDefinition DuplicateSpriteDefinition(tk2dSpriteDefinition sourceSpriteDefinition) {
            tk2dSpriteDefinition m_NewSpriteDefinition = new tk2dSpriteDefinition() {
                name = sourceSpriteDefinition.name,
                boundsDataCenter = sourceSpriteDefinition.boundsDataCenter,
                boundsDataExtents = sourceSpriteDefinition.boundsDataExtents,
                untrimmedBoundsDataCenter = sourceSpriteDefinition.untrimmedBoundsDataCenter,
                untrimmedBoundsDataExtents = sourceSpriteDefinition.untrimmedBoundsDataExtents,
                texelSize = sourceSpriteDefinition.texelSize,
                position0 = sourceSpriteDefinition.position0,
                position1 = sourceSpriteDefinition.position1,
                position2 = sourceSpriteDefinition.position2,
                position3 = sourceSpriteDefinition.position3,
                uvs = sourceSpriteDefinition.uvs,
                material = sourceSpriteDefinition.material,
                materialId = sourceSpriteDefinition.materialId,
                extractRegion = sourceSpriteDefinition.extractRegion,
                regionX = sourceSpriteDefinition.regionX,
                regionY = sourceSpriteDefinition.regionY,
                regionW = sourceSpriteDefinition.regionW,
                regionH = sourceSpriteDefinition.regionH,
                flipped = sourceSpriteDefinition.flipped,
                complexGeometry = sourceSpriteDefinition.complexGeometry,
                physicsEngine = sourceSpriteDefinition.physicsEngine,
                colliderType = sourceSpriteDefinition.colliderType,
                collisionLayer = sourceSpriteDefinition.collisionLayer,
                colliderVertices = sourceSpriteDefinition.colliderVertices,
                colliderConvex = sourceSpriteDefinition.colliderConvex,
                colliderSmoothSphereCollisions = sourceSpriteDefinition.colliderSmoothSphereCollisions,
            };
            if (sourceSpriteDefinition.metadata != null) {
                m_NewSpriteDefinition.metadata = new TilesetIndexMetadata() {
                    type = sourceSpriteDefinition.metadata.type,
                    weight = sourceSpriteDefinition.metadata.weight,
                    dungeonRoomSubType = sourceSpriteDefinition.metadata.dungeonRoomSubType,
                    secondRoomSubType = sourceSpriteDefinition.metadata.secondRoomSubType,
                    thirdRoomSubType = sourceSpriteDefinition.metadata.thirdRoomSubType,
                    preventWallStamping = sourceSpriteDefinition.metadata.preventWallStamping,
                    usesAnimSequence = sourceSpriteDefinition.metadata.usesAnimSequence,
                    usesNeighborDependencies = sourceSpriteDefinition.metadata.usesNeighborDependencies,
                    usesPerTileVFX = sourceSpriteDefinition.metadata.usesPerTileVFX,
                    tileVFXPlaystyle = sourceSpriteDefinition.metadata.tileVFXPlaystyle,
                    tileVFXChance = sourceSpriteDefinition.metadata.tileVFXChance,
                    tileVFXPrefab = sourceSpriteDefinition.metadata.tileVFXPrefab,
                    tileVFXOffset = sourceSpriteDefinition.metadata.tileVFXOffset,
                    tileVFXDelayTime = sourceSpriteDefinition.metadata.tileVFXDelayTime,
                    tileVFXDelayVariance = sourceSpriteDefinition.metadata.tileVFXDelayVariance,
                    tileVFXAnimFrame = sourceSpriteDefinition.metadata.tileVFXAnimFrame,
                };
            }
            return m_NewSpriteDefinition;
        }
 
        public static TileIndexGrid BuildNewTileIndexGrid(string name) {
            TileIndexGrid m_NewTileIndexGrid = ScriptableObject.CreateInstance<TileIndexGrid>();

            m_NewTileIndexGrid.name = name;
            m_NewTileIndexGrid.roomTypeRestriction = -1;
            m_NewTileIndexGrid.extendedSet = false;
            m_NewTileIndexGrid.CenterCheckerboard = false;
            m_NewTileIndexGrid.CheckerboardDimension = 1;
            m_NewTileIndexGrid.CenterIndicesAreStrata = false;
            m_NewTileIndexGrid.PitInternalSquareGrids = new List<TileIndexGrid>(0);
            m_NewTileIndexGrid.PitInternalSquareOptions = new PitSquarePlacementOptions() {
                PitSquareChance = 0.1f,
                CanBeFlushLeft = false,
                CanBeFlushRight = false,
                CanBeFlushBottom = false
            };
            m_NewTileIndexGrid.PitBorderIsInternal = false;
            m_NewTileIndexGrid.PitBorderOverridesFloorTile = false;
            m_NewTileIndexGrid.CeilingBorderUsesDistancedCenters = false;
            m_NewTileIndexGrid.PathFacewallStamp = null;
            m_NewTileIndexGrid.PathSidewallStamp = null;
            m_NewTileIndexGrid.PathStubNorth = null;
            m_NewTileIndexGrid.PathStubEast = null;
            m_NewTileIndexGrid.PathStubSouth = null;
            m_NewTileIndexGrid.PathStubWest = null;

            FieldInfo[] TileIndexGridInfo = typeof(TileIndexGrid).GetFields();
            TileIndexList defaultValue = new TileIndexList() { indices = new List<int>() { -1 }, indexWeights = new List<float>() { 1 } };
            foreach (var fieldInfo in TileIndexGridInfo) {
                if ( fieldInfo.FieldType == typeof(TileIndexList)) { fieldInfo.SetValue(m_NewTileIndexGrid, defaultValue); }
            }

            return m_NewTileIndexGrid;
        }

        public static GameObject SpawnCustomBowlerNote(GameObject note, Vector2 position, RoomHandler parentRoom, string customText, bool doPoof = false) {
            GameObject noteObject = UnityEngine.Object.Instantiate(note, position.ToVector3ZisY(0f), Quaternion.identity);
            if (noteObject) {
                NoteDoer BowlerNote = noteObject.GetComponentInChildren<NoteDoer>();
                if (BowlerNote) {
                    if (BowlerNote) {
                        BowlerNote.alreadyLocalized = true;
                        BowlerNote.stringKey = customText;
                        BowlerNote.RegenerateCache();
                    }
                }
                IPlayerInteractable[] interfacesInChildren = noteObject.GetInterfacesInChildren<IPlayerInteractable>();
                for (int i = 0; i < interfacesInChildren.Length; i++) { parentRoom.RegisterInteractable(interfacesInChildren[i]); }
            }
            if (doPoof) {
                GameObject vfxObject = (GameObject)UnityEngine.Object.Instantiate(ResourceCache.Acquire("Global VFX/VFX_Item_Spawn_Poof"));
                tk2dBaseSprite component = vfxObject.GetComponent<tk2dBaseSprite>();
                component.PlaceAtPositionByAnchor(position.ToVector3ZUp(0f) + new Vector3(0.5f, 0.75f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                component.HeightOffGround = 5f;
                component.UpdateZDepth();
            }
            return noteObject;
        }

        public static GameObject SpawnAirDrop(RoomHandler currentRoom, Vector3 landingPosition, GenericLootTable LootTable = null, DungeonPlaceable EnemyPlacable = null, float chanceToExplode = 0, float chanceToSpawnEnemy = 1) {
            if (!LootTable && !EnemyPlacable) { return null; }

            GameObject eCrateOBJ = BraveResources.Load<GameObject>("EmergencyCrate");
            if (!eCrateOBJ) { return null; }

            GameObject eCrateInstance = UnityEngine.Object.Instantiate(eCrateOBJ);
            EmergencyCrateController lootCrate = eCrateInstance.GetComponent<EmergencyCrateController>();
            if (!lootCrate) { return null; }
            
            if (LootTable && EnemyPlacable) {
                lootCrate.EnemyPlaceable = EnemyPlacable;
                lootCrate.gunTable = LootTable;
                lootCrate.ChanceToExplode = chanceToExplode;
                lootCrate.ChanceToSpawnEnemy = chanceToSpawnEnemy;
            } else if (LootTable) {
                lootCrate.gunTable = LootTable;
                lootCrate.ChanceToSpawnEnemy = 0;
            } else if (EnemyPlacable) {
                lootCrate.ChanceToSpawnEnemy = chanceToSpawnEnemy;
                lootCrate.EnemyPlaceable = EnemyPlacable;
            }

            lootCrate.ChanceToExplode = chanceToExplode;

            // Landing Position is not relative to room position! 
            // Use a global world psoition value like transform.position or something derived from it!
            lootCrate.Trigger(new Vector3(-5f, -5f, -5f), (landingPosition + new Vector3(15f, 15f, 15f)), currentRoom, true);
            currentRoom.ExtantEmergencyCrate = eCrateInstance;
            return eCrateInstance;
        }

        // if ObjectDrop and Enemydrop are left null this will spawn a explosive barrel paradrop.
        public static GameObject SpawnParaDrop(RoomHandler currentRoom, Vector3 landingPosition, GameObject ObjectDrop = null, string EnemyDrop = "NULL", Vector2? CustomObjectSize = null, float LandingPositionOffset = 0, float DropSpeed = 2.5f, float DropHeight = 10, float DropHorizontalOffset = 5, bool useLandingVFX = true, bool DeferParaDropStart = false) {
            GameObject m_CachedObject = null;
            bool isExplodyBarrel = false;

            if (!string.IsNullOrEmpty(EnemyDrop) && EnemyDrop != "NULL" && EnemyDatabase.GetOrLoadByGuid(EnemyDrop)) {
                m_CachedObject = AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(EnemyDrop), landingPosition, currentRoom, false, AIActor.AwakenAnimationType.Spawn, true).gameObject;
                if (!m_CachedObject) { isExplodyBarrel = true; }
            } else if (ObjectDrop) {
                m_CachedObject = ObjectDrop;
            } else {                
                isExplodyBarrel = true;
            }

            if (isExplodyBarrel) {
                m_CachedObject = UnityEngine.Object.Instantiate(ExpandPrefabs.EX_ExplodyBarrelDummy, landingPosition, Quaternion.identity);
                // if (m_CachedObject.GetComponent<SpeculativeRigidbody>()) { m_CachedObject.GetComponent<SpeculativeRigidbody>().Reinitialize(); }
            }

            ExpandParadropController paraDropController = m_CachedObject.AddComponent<ExpandParadropController>();
            if (CustomObjectSize.HasValue) {
                paraDropController.UseObjectSizeOverride = true;
                paraDropController.OverrideObjectSize = CustomObjectSize.Value;
            }
            paraDropController.ParentObjectExplodyBarrel = isExplodyBarrel;
            paraDropController.UseLandingVFX = useLandingVFX;
            paraDropController.LandingPositionOffset = LandingPositionOffset;
            paraDropController.DropSpeed = DropSpeed;
            paraDropController.DropHeightHorizontalOffset = DropHorizontalOffset;
            paraDropController.StartHeight = DropHeight;
            paraDropController.StartsIntheAir = true;
            if (!DeferParaDropStart) { paraDropController.Configured = true; }
            return m_CachedObject;
        }

        public static void GenerateAIActorTemplate(GameObject targetObject, out GameObject corpseObject, string EnemyName, string EnemyGUID, tk2dSprite spriteSource = null, GameObject gunAttachObjectOverride = null, Vector3? GunAttachOffset = null, int StartingGunID = 38, List<PixelCollider> customColliders = null, bool RigidBodyCollidesWithTileMap = true, bool RigidBodyCollidesWithOthers = true, bool RigidBodyCanBeCarried = true, bool RigidBodyCanBePushed = false, bool isFakePrefab = false, bool instantiateCorpseObject = true, GameObject ExternalCorpseObject = null, bool EnemyHasNoShooter = false, bool EnemyHasNoCorpse = false, PlayerHandController overrideHandObject = null) {

            if (!targetObject) { targetObject = new GameObject(EnemyName) { layer = 28 }; }

            GameObject m_CachedGunAttachPoint = null;

            corpseObject = null;

            if (instantiateCorpseObject && !EnemyHasNoCorpse) {
                corpseObject = UnityEngine.Object.Instantiate(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").CorpseObject);
                corpseObject.SetActive(false);
                FakePrefab.MarkAsFakePrefab(corpseObject);
            } else if (ExternalCorpseObject && !EnemyHasNoCorpse) {
                corpseObject = ExternalCorpseObject;
            }

            if (!targetObject.GetComponent<AIActor>() && !gunAttachObjectOverride && !EnemyHasNoShooter) {
                if (!targetObject.transform.Find("GunAttachPoint")) {
                    m_CachedGunAttachPoint = new GameObject("GunAttachPoint") { layer = 0 };
                    m_CachedGunAttachPoint.transform.position = targetObject.transform.position;
                    if (GunAttachOffset.HasValue) {
                        m_CachedGunAttachPoint.transform.position = GunAttachOffset.Value;
                    } else {
                        m_CachedGunAttachPoint.transform.position = new Vector3(0.3125f, 0.25f, 0);
                    }
                    m_CachedGunAttachPoint.transform.parent = targetObject.transform;
                } else {
                    if (GunAttachOffset.HasValue) { targetObject.transform.Find("GunAttachPoint").transform.localPosition = GunAttachOffset.Value; }
                }
            } else if (!targetObject.GetComponent<AIActor>() && gunAttachObjectOverride && !EnemyHasNoShooter) {
                // if (targetObject.transform.Find("GunAttachPoint")) { UnityEngine.Object.Destroy(targetObject.transform.Find("GunAttachPoint")); }
                m_CachedGunAttachPoint = new GameObject("GunAttachPoint") { layer = 0 };
                if (GunAttachOffset.HasValue) {
                    m_CachedGunAttachPoint.transform.position = GunAttachOffset.Value;
                } else {
                    m_CachedGunAttachPoint.transform.position = new Vector3(0.3125f, 0.25f, 0);
                }
                m_CachedGunAttachPoint.transform.parent = targetObject.transform;
                
            }

            if (!targetObject.GetComponent<tk2dSprite>() && spriteSource) {
                tk2dSprite newSprite = targetObject.AddComponent<tk2dSprite>();
                DuplicateSprite(newSprite, spriteSource);
            }
            
            tk2dSprite targetSprite = targetObject.GetComponent<tk2dSprite>();
            if (!targetSprite) { return; }

            if (!targetObject.GetComponent<SpeculativeRigidbody>()) {
                if (customColliders != null) {
                    foreach (PixelCollider collider in customColliders) {
                        int SizeX = collider.ManualWidth;
                        int SizeY = collider.ManualHeight;
                        int OffsetX = collider.ManualOffsetX;
                        int OffsetY = collider.ManualOffsetY;
                        GenerateOrAddToRigidBody(targetObject, collider.CollisionLayer, collider.ColliderGenerationMode, RigidBodyCollidesWithTileMap, RigidBodyCollidesWithOthers, RigidBodyCanBeCarried, RigidBodyCanBePushed, false, collider.IsTrigger, false, true, new IntVector2(SizeX, SizeY), new IntVector2(OffsetX, OffsetY));
                    }
                } else {
                    GenerateOrAddToRigidBody(targetObject, CollisionLayer.EnemyCollider, PixelCollider.PixelColliderGeneration.Manual, true, true, true, false, false, false, false, true, new IntVector2(12, 4), new IntVector2(5, 0));
                    GenerateOrAddToRigidBody(targetObject, CollisionLayer.EnemyHitBox, PixelCollider.PixelColliderGeneration.Manual, true, true, true, false, false, false, false, true, new IntVector2(12, 23), new IntVector2(5, 0));
                }

                SpeculativeRigidbody targetRigidBody = targetObject.GetComponent<SpeculativeRigidbody>();
                targetRigidBody.Reinitialize();
                if (customColliders == null) { targetRigidBody.PixelColliders[1].Sprite = targetSprite; }
            }
                        
            if (!targetObject.GetComponent<tk2dSpriteAnimator>()) {
                GenerateSpriteAnimator(targetObject, null, 0, 0, false, false, false, false, true, false, 0, 0, false);
            }

            if (!targetObject.GetComponent<HealthHaver>()) {
                GenerateHealthHaver(targetObject, 15, false, false, OnDeathBehavior.DeathType.Death, true, false, false, false, true, true);
            }
            
            if (!targetObject.GetComponent<HitEffectHandler>()) { targetObject.AddComponent<HitEffectHandler>(); }

            HitEffectHandler hitEffectHandler = targetObject.GetComponent<HitEffectHandler>();
            hitEffectHandler.overrideHitEffect = new VFXComplex() { effects = new VFXObject[0] };
            hitEffectHandler.overrideHitEffectPool = new VFXPool() { effects = new VFXComplex[0] };
            hitEffectHandler.additionalHitEffects = new HitEffectHandler.AdditionalHitEffect[0];
            hitEffectHandler.SuppressAllHitEffects = false;
            
            if (!targetObject.GetComponent<KnockbackDoer>()) { targetObject.AddComponent<KnockbackDoer>(); }

            KnockbackDoer knockBackDoer = targetObject.GetComponent<KnockbackDoer>();
            knockBackDoer.weight = 35;
            knockBackDoer.deathMultiplier = 2.5f;
            knockBackDoer.knockbackWhileReflecting = false;
            knockBackDoer.shouldBounce = true;
            knockBackDoer.collisionDecay = 0.5f;

            if (!targetObject.GetComponent<AIAnimator>()) { GenerateBlankAIAnimator(targetObject); }

            if (!targetObject.GetComponent<ObjectVisibilityManager>()) { targetObject.AddComponent<ObjectVisibilityManager>(); }

            ObjectVisibilityManager visibilityManager = targetObject.GetComponent<ObjectVisibilityManager>();
            visibilityManager.SuppressPlayerEnteredRoom = false;

            AIActor bulletManTemplate = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5"); // bullet_kin

            if ((!targetObject.GetComponent<AIShooter>() | !targetObject.GetComponent<AIBulletBank>()) && !EnemyHasNoShooter) {
                DuplicateAIShooterAndAIBulletBank(targetObject, bulletManTemplate.gameObject.GetComponent<AIShooter>(), bulletManTemplate.gameObject.GetComponent<AIBulletBank>(), StartingGunID, m_CachedGunAttachPoint.transform, overrideHandObject: overrideHandObject);
            }
            
            if (!targetObject.GetComponent<AIActor>()) { targetObject.AddComponent<AIActor>(); }

            AIActor m_CachedAIActor = targetObject.GetComponent<AIActor>();
            m_CachedAIActor.placeableWidth = 1;
            m_CachedAIActor.placeableHeight = 1;
            m_CachedAIActor.difficulty = DungeonPlaceableBehaviour.PlaceableDifficulty.BASE;
            m_CachedAIActor.isPassable = true;
            m_CachedAIActor.ActorName = EnemyName;
            m_CachedAIActor.OverrideDisplayName = EnemyName;
            m_CachedAIActor.actorTypes = 0;
            m_CachedAIActor.HasShadow = true;
            m_CachedAIActor.ShadowHeightOffGround = 0;
            m_CachedAIActor.ActorShadowOffset = Vector3.zero;
            m_CachedAIActor.DoDustUps = false;
            m_CachedAIActor.DustUpInterval = 0;
            m_CachedAIActor.FreezeDispelFactor = 20;
            m_CachedAIActor.ImmuneToAllEffects = false;
            m_CachedAIActor.EffectResistances = new ActorEffectResistance[0];
            m_CachedAIActor.OverrideColorOverridden = false;
            m_CachedAIActor.EnemyId = UnityEngine.Random.Range(10000, 99999);
            m_CachedAIActor.EnemyGuid = EnemyGUID;
            m_CachedAIActor.ForcedPositionInAmmonomicon = -1;
            m_CachedAIActor.SetsFlagOnDeath = false;
            m_CachedAIActor.FlagToSetOnDeath = GungeonFlags.NONE;
            m_CachedAIActor.SetsFlagOnActivation = false;
            m_CachedAIActor.FlagToSetOnActivation = GungeonFlags.NONE;
            m_CachedAIActor.SetsCharacterSpecificFlagOnDeath = false;
            m_CachedAIActor.CharacterSpecificFlagToSetOnDeath = CharacterSpecificGungeonFlags.NONE;
            m_CachedAIActor.IsNormalEnemy = true;
            m_CachedAIActor.IsSignatureEnemy = false;
            m_CachedAIActor.IsHarmlessEnemy = false;
            m_CachedAIActor.CompanionSettings = new ActorCompanionSettings() { WarpsToRandomPoint = false };
            m_CachedAIActor.MovementSpeed = 8;
            m_CachedAIActor.PathableTiles = CellTypes.FLOOR;
            m_CachedAIActor.DiesOnCollison = false;
            m_CachedAIActor.CollisionDamage = 0.5f;
            m_CachedAIActor.CollisionKnockbackStrength = 5;
            m_CachedAIActor.CollisionDamageTypes = CoreDamageTypes.None;
            m_CachedAIActor.EnemyCollisionKnockbackStrengthOverride = -1;
            m_CachedAIActor.CollisionVFX = new VFXPool() { effects = new VFXComplex[0] };
            m_CachedAIActor.NonActorCollisionVFX = new VFXPool() { effects = new VFXComplex[0] };
            m_CachedAIActor.CollisionSetsPlayerOnFire = false;
            m_CachedAIActor.TryDodgeBullets = false;
            m_CachedAIActor.AvoidRadius = 4;
            m_CachedAIActor.ReflectsProjectilesWhileInvulnerable = false;
            m_CachedAIActor.HitByEnemyBullets = false;
            m_CachedAIActor.HasOverrideDodgeRollDeath = false;
            m_CachedAIActor.OverrideDodgeRollDeath = string.Empty;
            m_CachedAIActor.CanDropCurrency = true;
            m_CachedAIActor.AdditionalSingleCoinDropChance = 0;
            m_CachedAIActor.CanDropItems = true;
            m_CachedAIActor.CanDropDuplicateItems = false;
            m_CachedAIActor.CustomLootTableMinDrops = 1;
            m_CachedAIActor.CustomLootTableMaxDrops = 1;
            m_CachedAIActor.ChanceToDropCustomChest = 0;
            m_CachedAIActor.IgnoreForRoomClear = false;
            m_CachedAIActor.SpawnLootAtRewardChestPos = false;
            if (!EnemyHasNoCorpse && corpseObject && !ExternalCorpseObject) {
                m_CachedAIActor.CorpseObject = corpseObject;
            } else if (!EnemyHasNoCorpse && ExternalCorpseObject) {
                m_CachedAIActor.CorpseObject = ExternalCorpseObject;
            } else {
                m_CachedAIActor.CorpseObject = null;
            }
            m_CachedAIActor.CorpseShadow = true;
            m_CachedAIActor.TransferShadowToCorpse = false;
            m_CachedAIActor.shadowDeathType = AIActor.ShadowDeathType.Fade;
            m_CachedAIActor.PreventDeathKnockback = false;
            m_CachedAIActor.OnCorpseVFX = new VFXPool() { effects = new VFXComplex[0] };
            m_CachedAIActor.OnEngagedVFXAnchor = tk2dBaseSprite.Anchor.LowerLeft;
            m_CachedAIActor.shadowHeightOffset = 0;
            m_CachedAIActor.invisibleUntilAwaken = false;
            m_CachedAIActor.procedurallyOutlined = true;
            m_CachedAIActor.forceUsesTrimmedBounds = true;
            m_CachedAIActor.reinforceType = AIActor.ReinforceType.FullVfx;
            m_CachedAIActor.UsesVaryingEmissiveShaderPropertyBlock = false;
            m_CachedAIActor.EnemySwitchState = "Metal_Bullet_Man";
            m_CachedAIActor.OverrideSpawnReticleAudio = string.Empty;
            m_CachedAIActor.OverrideSpawnAppearAudio = string.Empty;
            m_CachedAIActor.UseMovementAudio = false;
            m_CachedAIActor.StartMovingEvent = string.Empty;
            m_CachedAIActor.StopMovingEvent = string.Empty;
            m_CachedAIActor.animationAudioEvents = new List<ActorAudioEvent>() {
                new ActorAudioEvent() { eventTag = "footstep", eventName = "Play_CHR_metalBullet_step_01" }
            };
            m_CachedAIActor.HealthOverrides = new List<AIActor.HealthOverride>(0);
            m_CachedAIActor.IdentifierForEffects = AIActor.EnemyTypeIdentifier.UNIDENTIFIED;
            m_CachedAIActor.BehaviorOverridesVelocity = false;
            m_CachedAIActor.BehaviorVelocity = Vector2.zero;
            m_CachedAIActor.AlwaysShowOffscreenArrow = false;
            m_CachedAIActor.BlackPhantomProperties = new BlackPhantomProperties() {
                BonusHealthPercentIncrease = 2.2f,
                BonusHealthFlatIncrease = 0,
                MaxTotalHealth = 175,
                CooldownMultiplier = 0.66f,
                MovementSpeedMultiplier = 1.5f,
                LocalTimeScaleMultiplier = 1,
                BulletSpeedMultiplier = 1,
                GradientScale = 0.75f,
                ContrastPower = 1.3f
            };
            m_CachedAIActor.ForceBlackPhantomParticles = false;
            m_CachedAIActor.OverrideBlackPhantomParticlesCollider = false;
            m_CachedAIActor.BlackPhantomParticlesCollider = 0;
            m_CachedAIActor.PreventFallingInPitsEver = false;

            if (isFakePrefab) { m_CachedAIActor.RegenerateCache(); }

            bulletManTemplate = null;
        }

        public static void DuplicateAIShooterAndAIBulletBank(GameObject targetObject, AIShooter sourceShooter, AIBulletBank sourceBulletBank, int startingGunOverrideID = 0, Transform gunAttachPointOverride = null, Transform bulletScriptAttachPointOverride = null, PlayerHandController overrideHandObject = null) {
            if (targetObject.GetComponent<AIShooter>() && targetObject.GetComponent<AIBulletBank>()) { return; }

            if (!targetObject.GetComponent<AIBulletBank>()) { 
                AIBulletBank m_TargetBulletBank = targetObject.AddComponent<AIBulletBank>();
                m_TargetBulletBank.Bullets = new List<AIBulletBank.Entry>(0);
                if (sourceBulletBank.Bullets.Count > 0) {
                    foreach (AIBulletBank.Entry bulletEntry in sourceBulletBank.Bullets) {
                        m_TargetBulletBank.Bullets.Add(
                            new AIBulletBank.Entry() {
                                Name = bulletEntry.Name,
                                BulletObject = bulletEntry.BulletObject,
                                OverrideProjectile = bulletEntry.OverrideProjectile,
                                ProjectileData = new ProjectileData() {
                                    damage = bulletEntry.ProjectileData.damage,
                                    speed = bulletEntry.ProjectileData.speed,
                                    range = bulletEntry.ProjectileData.range,
                                    force = bulletEntry.ProjectileData.force,
                                    damping = bulletEntry.ProjectileData.damping,
                                    UsesCustomAccelerationCurve = bulletEntry.ProjectileData.UsesCustomAccelerationCurve,
                                    AccelerationCurve = bulletEntry.ProjectileData.AccelerationCurve,
                                    CustomAccelerationCurveDuration = bulletEntry.ProjectileData.CustomAccelerationCurveDuration,
                                    onDestroyBulletScript = bulletEntry.ProjectileData.onDestroyBulletScript,
                                    IgnoreAccelCurveTime = bulletEntry.ProjectileData.IgnoreAccelCurveTime
                                },
                                PlayAudio = bulletEntry.PlayAudio,
                                AudioSwitch = bulletEntry.AudioSwitch,
                                AudioEvent = bulletEntry.AudioEvent,
                                AudioLimitOncePerFrame = bulletEntry.AudioLimitOncePerFrame,
                                AudioLimitOncePerAttack = bulletEntry.AudioLimitOncePerAttack,
                                MuzzleFlashEffects = new VFXPool() {
                                    effects = bulletEntry.MuzzleFlashEffects.effects,
                                    type = bulletEntry.MuzzleFlashEffects.type
                                },
                                MuzzleLimitOncePerFrame = bulletEntry.MuzzleLimitOncePerFrame,
                                MuzzleInheritsTransformDirection = bulletEntry.MuzzleInheritsTransformDirection,
                                ShellTransform = bulletEntry.ShellTransform,
                                ShellPrefab = bulletEntry.ShellPrefab,
                                ShellForce = bulletEntry.ShellForce,
                                ShellForceVariance = bulletEntry.ShellForceVariance,
                                DontRotateShell = bulletEntry.DontRotateShell,
                                ShellGroundOffset = bulletEntry.ShellGroundOffset,
                                ShellsLimitOncePerFrame = bulletEntry.ShellsLimitOncePerFrame,
                                rampBullets = bulletEntry.rampBullets,
                                conditionalMinDegFromNorth = bulletEntry.conditionalMinDegFromNorth,
                                forceCanHitEnemies = bulletEntry.forceCanHitEnemies,
                                suppressHitEffectsIfOffscreen = bulletEntry.suppressHitEffectsIfOffscreen,
                                preloadCount = bulletEntry.preloadCount
                            }
                        );
                    }
                }
                m_TargetBulletBank.useDefaultBulletIfMissing = true;
                m_TargetBulletBank.transforms = new List<Transform>();
                if (sourceBulletBank.transforms != null && sourceBulletBank.transforms.Count > 0) {
                    foreach (Transform transform in sourceBulletBank.transforms) { m_TargetBulletBank.transforms.Add(transform); }
                }
                m_TargetBulletBank.RegenerateCache();
            }

            if (!targetObject.GetComponent<AIShooter>()) { 
                AIShooter m_targetShooter = targetObject.AddComponent<AIShooter>();
                m_targetShooter.volley = sourceShooter.volley;
                if (startingGunOverrideID != 0) {
                    m_targetShooter.equippedGunId = startingGunOverrideID;
                } else {
                    m_targetShooter.equippedGunId = sourceShooter.equippedGunId;
                }
                m_targetShooter.shouldUseGunReload = true;
                m_targetShooter.volleyShootPosition = sourceShooter.volleyShootPosition;
                m_targetShooter.volleyShellCasing = sourceShooter.volleyShellCasing;
                m_targetShooter.volleyShellTransform = sourceShooter.volleyShellTransform;
                m_targetShooter.volleyShootVfx = sourceShooter.volleyShootVfx;
                m_targetShooter.usesOctantShootVFX = sourceShooter.usesOctantShootVFX;
                m_targetShooter.bulletName = sourceShooter.bulletName;
                m_targetShooter.customShootCooldownPeriod = sourceShooter.customShootCooldownPeriod;
                m_targetShooter.doesScreenShake = sourceShooter.doesScreenShake;
                m_targetShooter.rampBullets = sourceShooter.rampBullets;
                m_targetShooter.rampStartHeight = sourceShooter.rampStartHeight;
                m_targetShooter.rampTime = sourceShooter.rampTime;
                if (gunAttachPointOverride) {
                    m_targetShooter.gunAttachPoint = gunAttachPointOverride;
                } else {
                    m_targetShooter.gunAttachPoint = sourceShooter.gunAttachPoint;
                }
                if (bulletScriptAttachPointOverride) {
                    m_targetShooter.bulletScriptAttachPoint = bulletScriptAttachPointOverride;
                } else {
                    m_targetShooter.bulletScriptAttachPoint = sourceShooter.bulletScriptAttachPoint;
                }
                m_targetShooter.overallGunAttachOffset = sourceShooter.overallGunAttachOffset;
                m_targetShooter.flippedGunAttachOffset = sourceShooter.flippedGunAttachOffset;
                if (overrideHandObject) {
                    m_targetShooter.handObject = overrideHandObject;
                } else {
                    m_targetShooter.handObject = sourceShooter.handObject;
                }
                m_targetShooter.AllowTwoHands = sourceShooter.AllowTwoHands;
                m_targetShooter.ForceGunOnTop = sourceShooter.ForceGunOnTop;
                m_targetShooter.IsReallyBigBoy = sourceShooter.IsReallyBigBoy;
                m_targetShooter.BackupAimInMoveDirection = sourceShooter.BackupAimInMoveDirection;
                m_targetShooter.RegenerateCache();
            }
            return;
        }

        public static AIAnimator GenerateBlankAIAnimator(GameObject targetObject) {
            AIAnimator m_CachedAIAnimator = targetObject.AddComponent<AIAnimator>();
            m_CachedAIAnimator.facingType = AIAnimator.FacingType.Default;
            m_CachedAIAnimator.faceSouthWhenStopped = false;
            m_CachedAIAnimator.faceTargetWhenStopped = false;
            m_CachedAIAnimator.AnimatedFacingDirection = -90;
            m_CachedAIAnimator.directionalType = AIAnimator.DirectionalType.Sprite;
            m_CachedAIAnimator.RotationQuantizeTo = 0;
            m_CachedAIAnimator.RotationOffset = 0;
            m_CachedAIAnimator.ForceKillVfxOnPreDeath = false;
            m_CachedAIAnimator.SuppressAnimatorFallback = false;
            m_CachedAIAnimator.IsBodySprite = true;
            m_CachedAIAnimator.IdleAnimation = new DirectionalAnimation() {
                Type = DirectionalAnimation.DirectionType.None,
                Prefix = string.Empty,
                AnimNames = new string[0],
                Flipped = new DirectionalAnimation.FlipType[0]
            };
            m_CachedAIAnimator.MoveAnimation = new DirectionalAnimation() {
                Type = DirectionalAnimation.DirectionType.None,
                Prefix = string.Empty,
                AnimNames = new string[0],
                Flipped = new DirectionalAnimation.FlipType[0]
            };
            m_CachedAIAnimator.FlightAnimation = new DirectionalAnimation() {
                Type = DirectionalAnimation.DirectionType.None,
                Prefix = string.Empty,
                AnimNames = new string[0],
                Flipped = new DirectionalAnimation.FlipType[0]
            };
            m_CachedAIAnimator.HitAnimation = new DirectionalAnimation() {
                Type = DirectionalAnimation.DirectionType.None,
                Prefix = string.Empty,
                AnimNames = new string[0],
                Flipped = new DirectionalAnimation.FlipType[0]
            };
            m_CachedAIAnimator.TalkAnimation = new DirectionalAnimation() {
                Type = DirectionalAnimation.DirectionType.None,
                Prefix = string.Empty,
                AnimNames = new string[0],
                Flipped = new DirectionalAnimation.FlipType[0]
            };
            m_CachedAIAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>(0);
            m_CachedAIAnimator.OtherVFX = new List<AIAnimator.NamedVFXPool>(0);
            m_CachedAIAnimator.OtherScreenShake = new List<AIAnimator.NamedScreenShake>(0);
            m_CachedAIAnimator.IdleFidgetAnimations = new List<DirectionalAnimation>(0);
            m_CachedAIAnimator.HitReactChance = 1;
            m_CachedAIAnimator.HitType = AIAnimator.HitStateType.Basic;
            return m_CachedAIAnimator;
        }

        /*public static void GenerateSpriteAnimator(GameObject targetObject, tk2dSpriteAnimation library = null, int DefaultClipId = 0, float AdditionalCameraVisibilityRadius = 0, bool AnimateDuringBossIntros = false, bool AlwaysIgnoreTimeScale = false, bool ignoreTimeScale = false, bool ForceSetEveryFrame = false, bool playAutomatically = false, bool IsFrameBlendedAnimation = false, float clipTime = 0, float ClipFps = 15, bool deferNextStartClip = false, bool alwaysUpdateOffscreen = false, bool maximumDeltaOneFrame = false) {
            if (targetObject.GetComponent<tk2dSpriteAnimator>()) { UnityEngine.Object.Destroy(targetObject.GetComponent<tk2dSpriteAnimator>()); }
            tk2dSpriteAnimator newAnimator = targetObject.AddComponent<tk2dSpriteAnimator>();
            newAnimator.Library = library;
            newAnimator.DefaultClipId = DefaultClipId;
            newAnimator.AdditionalCameraVisibilityRadius = AdditionalCameraVisibilityRadius;
            newAnimator.AnimateDuringBossIntros = AnimateDuringBossIntros;
            newAnimator.AlwaysIgnoreTimeScale = AlwaysIgnoreTimeScale;
            newAnimator.ignoreTimeScale = ignoreTimeScale;
            newAnimator.ForceSetEveryFrame = ForceSetEveryFrame;
            newAnimator.playAutomatically = playAutomatically;
            newAnimator.IsFrameBlendedAnimation = IsFrameBlendedAnimation;
            newAnimator.clipTime = clipTime;
            newAnimator.ClipFps = ClipFps;
            newAnimator.deferNextStartClip = deferNextStartClip;
            newAnimator.alwaysUpdateOffscreen = alwaysUpdateOffscreen;
            newAnimator.maximumDeltaOneFrame = maximumDeltaOneFrame;

            return;
        }*/

        public static tk2dSpriteAnimator GenerateSpriteAnimator(GameObject targetObject, tk2dSpriteAnimation library = null, int DefaultClipId = 0, float AdditionalCameraVisibilityRadius = 0, bool AnimateDuringBossIntros = false, bool AlwaysIgnoreTimeScale = false, bool ignoreTimeScale = false, bool ForceSetEveryFrame = false, bool playAutomatically = false, bool IsFrameBlendedAnimation = false, float clipTime = 0, float ClipFps = 15, bool deferNextStartClip = false, bool alwaysUpdateOffscreen = false, bool maximumDeltaOneFrame = false) {
            if (targetObject.GetComponent<tk2dSpriteAnimator>()) { UnityEngine.Object.Destroy(targetObject.GetComponent<tk2dSpriteAnimator>()); }
            tk2dSpriteAnimator newAnimator = targetObject.AddComponent<tk2dSpriteAnimator>();
            newAnimator.Library = library;
            newAnimator.DefaultClipId = DefaultClipId;
            newAnimator.AdditionalCameraVisibilityRadius = AdditionalCameraVisibilityRadius;
            newAnimator.AnimateDuringBossIntros = AnimateDuringBossIntros;
            newAnimator.AlwaysIgnoreTimeScale = AlwaysIgnoreTimeScale;
            newAnimator.ignoreTimeScale = ignoreTimeScale;
            newAnimator.ForceSetEveryFrame = ForceSetEveryFrame;
            newAnimator.playAutomatically = playAutomatically;
            newAnimator.IsFrameBlendedAnimation = IsFrameBlendedAnimation;
            newAnimator.clipTime = clipTime;
            newAnimator.ClipFps = ClipFps;
            newAnimator.deferNextStartClip = deferNextStartClip;
            newAnimator.alwaysUpdateOffscreen = alwaysUpdateOffscreen;
            newAnimator.maximumDeltaOneFrame = maximumDeltaOneFrame;
            return newAnimator;
        }

        public static tk2dSpriteAnimator GenerateSpriteAnimator(tk2dSpriteAnimation library = null, int DefaultClipId = 0, float AdditionalCameraVisibilityRadius = 0, bool AnimateDuringBossIntros = false, bool AlwaysIgnoreTimeScale = false, bool ignoreTimeScale = false, bool ForceSetEveryFrame = false, bool playAutomatically = false, bool IsFrameBlendedAnimation = false, float clipTime = 0, float ClipFps = 15, bool deferNextStartClip = false, bool alwaysUpdateOffscreen = false, bool maximumDeltaOneFrame = false) {
            tk2dSpriteAnimator newAnimator = new tk2dSpriteAnimator();
            newAnimator.Library = library;
            newAnimator.DefaultClipId = DefaultClipId;
            newAnimator.AdditionalCameraVisibilityRadius = AdditionalCameraVisibilityRadius;
            newAnimator.AnimateDuringBossIntros = AnimateDuringBossIntros;
            newAnimator.AlwaysIgnoreTimeScale = AlwaysIgnoreTimeScale;
            newAnimator.ignoreTimeScale = ignoreTimeScale;
            newAnimator.ForceSetEveryFrame = ForceSetEveryFrame;
            newAnimator.playAutomatically = playAutomatically;
            newAnimator.IsFrameBlendedAnimation = IsFrameBlendedAnimation;
            newAnimator.clipTime = clipTime;
            newAnimator.ClipFps = ClipFps;
            newAnimator.deferNextStartClip = deferNextStartClip;
            newAnimator.alwaysUpdateOffscreen = alwaysUpdateOffscreen;
            newAnimator.maximumDeltaOneFrame = maximumDeltaOneFrame;
            return newAnimator;
        }

        public static tk2dSpriteAnimation DuplicateSpriteAnimation(GameObject targetObject, tk2dSpriteAnimation targetAnimation, tk2dSpriteAnimation sourceAnimation, tk2dSpriteCollectionData overrideCollection = null) {
            tk2dSpriteAnimation m_CachedAnimation = null;
            if (!targetObject.GetComponent<tk2dSpriteAnimation>()) {
                m_CachedAnimation = targetObject.AddComponent<tk2dSpriteAnimation>();
                m_CachedAnimation.clips = new tk2dSpriteAnimationClip[0];
			} else {
                m_CachedAnimation = targetObject.GetComponent<tk2dSpriteAnimation>();
                m_CachedAnimation.clips = new tk2dSpriteAnimationClip[0];
            }

            List<tk2dSpriteAnimationClip> animationList = new List<tk2dSpriteAnimationClip>();
            foreach (tk2dSpriteAnimationClip clip in sourceAnimation.clips) {
                animationList.Add(DuplicateAnimationClip(clip, overrideCollection));
            }

            if (animationList.Count <= 0) {
                ETGModConsole.Log("[ExpandTheGungeon] AddAnimation: ERROR! Animation Clip list is empty! No valid clips found in specified source!");
                return null;
            }

            m_CachedAnimation.clips = animationList.ToArray();
            
            return m_CachedAnimation;
        }

        public static void AddAnimation(tk2dSpriteAnimator targetAnimator, tk2dSpriteCollectionData collection, List<int> spriteIDList, string clipName, tk2dSpriteAnimationClip.WrapMode wrapMode = tk2dSpriteAnimationClip.WrapMode.Once, int frameRate = 15, int loopStart = 0, float minFidgetDuration = 0.5f, float maxFidgetDuration = 1) {
            
            if (targetAnimator.Library == null) {
                targetAnimator.Library = targetAnimator.gameObject.AddComponent<tk2dSpriteAnimation>();
                targetAnimator.Library.clips = new tk2dSpriteAnimationClip[0];
			}

			List<tk2dSpriteAnimationFrame> animationList = new List<tk2dSpriteAnimationFrame>();
			for (int i = 0; i < spriteIDList.Count; i++) {
				tk2dSpriteDefinition spriteDefinition = collection.spriteDefinitions[spriteIDList[i]];
				if (spriteDefinition.Valid) {
                    animationList.Add(
                        new tk2dSpriteAnimationFrame {
                            spriteCollection = collection,
                            spriteId = spriteIDList[i],
                            invulnerableFrame = false,
                            groundedFrame = true,
                            requiresOffscreenUpdate = false,
                            eventAudio = string.Empty,
                            eventVfx = string.Empty,
                            eventStopVfx = string.Empty,
                            eventLerpEmissive = false,
                            eventLerpEmissiveTime = 0.5f,
                            eventLerpEmissivePower = 30,
                            forceMaterialUpdate = false,
                            finishedSpawning = false,
                            triggerEvent = false,
                            eventInfo = string.Empty,
                            eventInt = 0,
                            eventFloat = 0,
                            eventOutline = tk2dSpriteAnimationFrame.OutlineModifier.Unspecified,
					    }
                    );
				}
			}
			tk2dSpriteAnimationClip animationClip = new tk2dSpriteAnimationClip() {
                name = clipName,
                frames = animationList.ToArray(),
                fps = frameRate,
                wrapMode = wrapMode,
                loopStart = loopStart,
                minFidgetDuration = minFidgetDuration,
                maxFidgetDuration = maxFidgetDuration,
            };
			Array.Resize(ref targetAnimator.Library.clips, targetAnimator.Library.clips.Length + 1);
            targetAnimator.Library.clips[targetAnimator.Library.clips.Length - 1] = animationClip;
            return;
        }

        public static tk2dSpriteAnimationClip AddAnimation(tk2dSpriteAnimator targetAnimator, tk2dSpriteCollectionData collection, List<string> spriteNameList, string clipName, tk2dSpriteAnimationClip.WrapMode wrapMode = tk2dSpriteAnimationClip.WrapMode.Once, int frameRate = 15, int loopStart = 0, float minFidgetDuration = 0.5f, float maxFidgetDuration = 1) {
            if (!targetAnimator.Library) {
                targetAnimator.Library = targetAnimator.gameObject.AddComponent<tk2dSpriteAnimation>();
                targetAnimator.Library.clips = new tk2dSpriteAnimationClip[0];
			}
            List<tk2dSpriteAnimationFrame> animationList = new List<tk2dSpriteAnimationFrame>();
			for (int i = 0; i < spriteNameList.Count; i++) {
                tk2dSpriteDefinition spriteDefinition = collection.GetSpriteDefinition(spriteNameList[i]);
                if (spriteDefinition != null && spriteDefinition.Valid) {
                    animationList.Add(
                        new tk2dSpriteAnimationFrame {
                            spriteCollection = collection,
                            spriteId = collection.GetSpriteIdByName(spriteNameList[i]),
                            invulnerableFrame = false,
                            groundedFrame = true,
                            requiresOffscreenUpdate = false,
                            eventAudio = string.Empty,
                            eventVfx = string.Empty,
                            eventStopVfx = string.Empty,
                            eventLerpEmissive = false,
                            eventLerpEmissiveTime = 0.5f,
                            eventLerpEmissivePower = 30,
                            forceMaterialUpdate = false,
                            finishedSpawning = false,
                            triggerEvent = false,
                            eventInfo = string.Empty,
                            eventInt = 0,
                            eventFloat = 0,
                            eventOutline = tk2dSpriteAnimationFrame.OutlineModifier.Unspecified,
					    }
                    );
				}
			}

            if (animationList.Count <= 0) {
                ETGModConsole.Log("[ExpandTheGungeon] AddAnimation: ERROR! Animation list is empty! No valid sprites found in specified list!");
                return null;
            }

            tk2dSpriteAnimationClip animationClip = new tk2dSpriteAnimationClip() {
                name = clipName,
                frames = animationList.ToArray(),
                fps = frameRate,
                wrapMode = wrapMode,
                loopStart = loopStart,
                minFidgetDuration = minFidgetDuration,
                maxFidgetDuration = maxFidgetDuration,
            };
            Array.Resize(ref targetAnimator.Library.clips, targetAnimator.Library.clips.Length + 1);
            targetAnimator.Library.clips[targetAnimator.Library.clips.Length - 1] = animationClip;
            return animationClip;
        }
        
        public static void AddAnimation(tk2dSpriteAnimation targetAnimation, tk2dSpriteCollectionData collection, List<string> spriteNameList, string clipName, tk2dSpriteAnimationClip.WrapMode wrapMode = tk2dSpriteAnimationClip.WrapMode.Once, int frameRate = 15, int loopStart = 0, float minFidgetDuration = 0.5f, float maxFidgetDuration = 1) {
            if (targetAnimation.clips == null) { targetAnimation.clips = new tk2dSpriteAnimationClip[0]; }
            List<tk2dSpriteAnimationFrame> animationList = new List<tk2dSpriteAnimationFrame>();
			for (int i = 0; i < spriteNameList.Count; i++) {
                tk2dSpriteDefinition spriteDefinition = collection.GetSpriteDefinition(spriteNameList[i]);
                if (spriteDefinition != null && spriteDefinition.Valid) {
                    animationList.Add(
                        new tk2dSpriteAnimationFrame {
                            spriteCollection = collection,
                            spriteId = collection.GetSpriteIdByName(spriteNameList[i]),
                            invulnerableFrame = false,
                            groundedFrame = true,
                            requiresOffscreenUpdate = false,
                            eventAudio = string.Empty,
                            eventVfx = string.Empty,
                            eventStopVfx = string.Empty,
                            eventLerpEmissive = false,
                            eventLerpEmissiveTime = 0.5f,
                            eventLerpEmissivePower = 30,
                            forceMaterialUpdate = false,
                            finishedSpawning = false,
                            triggerEvent = false,
                            eventInfo = string.Empty,
                            eventInt = 0,
                            eventFloat = 0,
                            eventOutline = tk2dSpriteAnimationFrame.OutlineModifier.Unspecified,
					    }
                    );
				}
			}
            tk2dSpriteAnimationClip animationClip = new tk2dSpriteAnimationClip() {
                name = clipName,
                frames = animationList.ToArray(),
                fps = frameRate,
                wrapMode = wrapMode,
                loopStart = loopStart,
                minFidgetDuration = minFidgetDuration,
                maxFidgetDuration = maxFidgetDuration,
            };
            Array.Resize(ref targetAnimation.clips, targetAnimation.clips.Length + 1);
            targetAnimation.clips[targetAnimation.clips.Length - 1] = animationClip;
            return;
        }

        public static void GenerateCorruptedTilesAtPosition(Dungeon dungeon, RoomHandler parentRoom, IntVector2 targetPosition, IntVector2 areaSize, GameObject parentObject = null, float CorruptionIntensity = 0.5f, bool AllowGlitchShader = true, bool emitsCorruptionNoise = true, bool corruptionNoiseFillsRoom = false, bool isSecretRoomWallMarkerCorruption = true) {

            if (dungeon == null | parentRoom == null) { return; }

            // This centers the point of origin to the center of the radius area instead of bottom left corner.
            IntVector2 position = targetPosition;
            if (areaSize != IntVector2.One) {
                IntVector2 areaSizeOffset = new IntVector2((areaSize.x / 2), (areaSize.y / 2));
                if (areaSizeOffset != IntVector2.Zero) {
                    position = (new IntVector2(targetPosition.x, targetPosition.y) - areaSizeOffset);
                }
            }
            if (emitsCorruptionNoise) {
                if (isSecretRoomWallMarkerCorruption && parentObject) {
                    GameObject CorruptionAmbience = new GameObject("SecretRoomWall_CorruptionAmbience_SFX") { layer = 20 };
                    CorruptionAmbience.transform.position = (position.ToVector3());
                    CorruptionAmbience.transform.parent = parentObject.transform;
                    CorruptionAmbience.AddComponent<ExpandCorruptedObjectAmbienceComponent>();
                    ExpandCorruptedObjectAmbienceComponent m_CorruptedAmbiencePlacable = CorruptionAmbience.GetComponent<ExpandCorruptedObjectAmbienceComponent>();
                    if (m_CorruptedAmbiencePlacable) {
                        if (areaSize != IntVector2.One) {
                            IntVector2 areaSizeOffset = new IntVector2((areaSize.x / 2), (areaSize.y / 2));
                            if (areaSizeOffset != IntVector2.Zero) {
                                m_CorruptedAmbiencePlacable.UnitOffset = areaSizeOffset.ToVector3();
                            }
                        }
                        m_CorruptedAmbiencePlacable.Init(parentRoom);
                    }
                } else {
                    if (parentObject) {
                        if (corruptionNoiseFillsRoom) {
                            AkSoundEngine.PostEvent("Play_EX_CorruptionAmbience_01", parentObject);
                        } else {
                            AkSoundEngine.PostEvent("Play_EX_CorruptionAmbience_02", parentObject);
                        }
                    } else {
                        GameObject CorruptionAmbience = new GameObject("CorruptionAmbience_SFX") { layer = 20 };
                        CorruptionAmbience.transform.position = (position.ToVector2());
                        CorruptionAmbience.transform.parent = parentRoom.hierarchyParent;
                        
                        CorruptionAmbience.AddComponent<ExpandCorruptedRoomAmbiencePlacable>();
                        ExpandCorruptedRoomAmbiencePlacable m_SoundPlacable = CorruptionAmbience.GetComponent<ExpandCorruptedRoomAmbiencePlacable>();
                        m_SoundPlacable.CameFromCorruptionBomb = true;
                        if (corruptionNoiseFillsRoom) {
                            m_SoundPlacable.CorruptionFXPlayEvent = "Play_EX_CorruptionAmbience_01";
                            m_SoundPlacable.CorruptionFXStopEvent = "Stop_EX_CorruptionAmbience_01";
                            // AkSoundEngine.PostEvent("Play_EX_CorruptionAmbience_01", CorruptionAmbience);
                        } else {
                            m_SoundPlacable.CorruptionFXPlayEvent = "Play_EX_CorruptionAmbience_02";
                            m_SoundPlacable.CorruptionFXStopEvent = "Stop_EX_CorruptionAmbience_02";
                            // AkSoundEngine.PostEvent("Play_EX_CorruptionAmbience_02", CorruptionAmbience);
                        }
                        m_SoundPlacable.ConfigureOnPlacement(parentRoom);
                    }
                }
            }

            tk2dSpriteCollectionData dungeonCollection = dungeon.tileIndices.dungeonCollection;

            List<int> CurrentFloorWallIDs = new List<int>();
            List<int> CurrentFloorFloorIDs = new List<int>();
            List<int> CurrentFloorMiscIDs = new List<int>();

            // Select Sprite ID lists based on tileset. (IDs corrispond to different sprites depending on tileset dungeonCollection)
            if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                CurrentFloorWallIDs = ExpandLists.CastleWallIDs;
                CurrentFloorFloorIDs = ExpandLists.CastleFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.CastleMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.GUNGEON) {
                CurrentFloorWallIDs = ExpandLists.GungeonWallIDs;
                CurrentFloorFloorIDs = ExpandLists.GungeonFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.GungeonMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.MINEGEON) {
                CurrentFloorWallIDs = ExpandLists.MinesWallIDs;
                CurrentFloorFloorIDs = ExpandLists.MinesFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.MinesMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATACOMBGEON) {
                CurrentFloorWallIDs = ExpandLists.HollowsWallIDs;
                CurrentFloorFloorIDs = ExpandLists.HollowsFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.HollowsMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                CurrentFloorWallIDs = ExpandLists.ForgeWallIDs;
                CurrentFloorFloorIDs = ExpandLists.ForgeFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.ForgeMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.HELLGEON) {
                CurrentFloorWallIDs = ExpandLists.BulletHell_WallIDs;
                CurrentFloorFloorIDs = ExpandLists.BulletHell_FloorIDs;
                CurrentFloorMiscIDs = ExpandLists.BulletHell_MiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.SEWERGEON) {
                CurrentFloorWallIDs = ExpandLists.SewerWallIDs;
                CurrentFloorFloorIDs = ExpandLists.SewerFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.SewerMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATHEDRALGEON) {
                CurrentFloorWallIDs = ExpandLists.AbbeyWallIDs;
                CurrentFloorFloorIDs = ExpandLists.AbbeyFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.AbbeyMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.RATGEON | dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.JUNGLEGEON) {
                CurrentFloorWallIDs = ExpandLists.RatDenWallIDs;
                CurrentFloorFloorIDs = ExpandLists.RatDenFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.RatDenMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON) {
                foreach (int id in ExpandLists.Nakatomi_OfficeWallIDs) { CurrentFloorWallIDs.Add(id); }
                foreach (int id in ExpandLists.Nakatomi_OfficeFloorIDs) { CurrentFloorFloorIDs.Add(id); }
                foreach (int id in ExpandLists.Nakatomi_OfficeMiscIDs) { CurrentFloorMiscIDs.Add(id); }
                foreach (int id in ExpandLists.Nakatomi_FutureWallIDs) { CurrentFloorWallIDs.Add(id + 704); }
                foreach (int id in ExpandLists.Nakatomi_FutureFloorIDs) { CurrentFloorFloorIDs.Add(id + 704); }
                foreach (int id in ExpandLists.Nakatomi_FutureMiscIDs) { CurrentFloorMiscIDs.Add(id + 704); }
            } else {
                Dungeon tempDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Gungeon");
                dungeonCollection = tempDungeonPrefab.tileIndices.dungeonCollection;
                CurrentFloorWallIDs = ExpandLists.GungeonWallIDs;
                CurrentFloorFloorIDs = ExpandLists.GungeonFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.GungeonMiscIDs;
                tempDungeonPrefab = null;
            }
            
            for (int X = 0; X < areaSize.x; X++) {
                for (int Y = 0; Y < areaSize.y; Y++) {
                    if (UnityEngine.Random.value <= CorruptionIntensity) {
                        bool isWallCell = false;

                        if (dungeon.data.isWall(position.x + X, position.y + Y) | dungeon.data.isAnyFaceWall(position.x + X, position.y + Y) | dungeon.data.isWall(position.x + X, (position.y - 1) + Y)) {
                            isWallCell = true;
                        }

                        GameObject m_GlitchTile = new GameObject("GlitchTile_" + UnityEngine.Random.Range(1000000, 9999999)) { layer = 20 };
                        m_GlitchTile.transform.position = (position.ToVector2() + new Vector2(X, Y));

                        if (isSecretRoomWallMarkerCorruption) {
                            m_GlitchTile.transform.parent = parentRoom.hierarchyParent;
                        } else if (parentObject != null) {
                            m_GlitchTile.transform.parent = parentObject.transform;
                        } else {
                            m_GlitchTile.transform.parent = parentRoom.hierarchyParent;
                        }

                        if (isWallCell) {
                            // m_GlitchTile.layer = 22;
                            m_GlitchTile.layer = LayerMask.NameToLayer("FG_Critical");
                        } else {
                            m_GlitchTile.layer = LayerMask.NameToLayer("BG_Critical");
                        }

                        List<int> spriteIDs = new List<int>();
                        int TileType = UnityEngine.Random.Range(1, 3);
                        if (TileType == 1) { spriteIDs = CurrentFloorWallIDs.Shuffle(); }
                        if (TileType == 2) { spriteIDs = CurrentFloorFloorIDs.Shuffle(); }
                        if (TileType == 3) { spriteIDs = CurrentFloorMiscIDs.Shuffle(); }

                        m_GlitchTile.AddComponent<tk2dSprite>();

                        tk2dSprite m_GlitchSprite = m_GlitchTile.GetComponent<tk2dSprite>();
                        m_GlitchSprite.Collection = dungeonCollection;
                        m_GlitchSprite.SetSprite(m_GlitchSprite.Collection, BraveUtility.RandomElement(spriteIDs));
                        m_GlitchSprite.ignoresTiltworldDepth = false;
                        m_GlitchSprite.depthUsesTrimmedBounds = false;
                        m_GlitchSprite.allowDefaultLayer = false;
                        m_GlitchSprite.OverrideMaterialMode = tk2dBaseSprite.SpriteMaterialOverrideMode.NONE;
                        m_GlitchSprite.independentOrientation = false;
                        m_GlitchSprite.hasOffScreenCachedUpdate = false;
                        if (isWallCell) {
                            m_GlitchSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.PERPENDICULAR;
                        } else {
                            m_GlitchSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.FLAT;
                        }
                        m_GlitchSprite.SortingOrder = 2;
                        m_GlitchSprite.IsBraveOutlineSprite = false;
                        m_GlitchSprite.IsZDepthDirty = false;
                        m_GlitchSprite.ApplyEmissivePropertyBlock = false;
                        m_GlitchSprite.GenerateUV2 = false;
                        m_GlitchSprite.LockUV2OnFrameOne = false;
                        m_GlitchSprite.StaticPositions = false;
                                    
                        if (isWallCell) {                
                            if (dungeon.data.isFaceWallLower(position.x + X, position.y + Y) && !dungeon.data.isWall(position.x + X, (position.y - 1) + Y)) {
                                m_GlitchSprite.HeightOffGround = -1.4f;
                                m_GlitchSprite.UpdateZDepth();
                            } else {
                                m_GlitchSprite.HeightOffGround = 3.5f;
                                m_GlitchSprite.UpdateZDepth();
                            }
                        } else {
                            m_GlitchSprite.HeightOffGround = -1.7f;
                            m_GlitchSprite.SortingOrder = 2;
                            m_GlitchSprite.UpdateZDepth();
                            /*FloorTypeOverrideDoer floorOverride = m_GlitchTile.AddComponent<FloorTypeOverrideDoer>();
                            floorOverride.overrideMode = FloorTypeOverrideDoer.OverrideMode.Placeable;
                            floorOverride.xStartOffset = 0;
                            floorOverride.yStartOffset = 0;
                            floorOverride.width = 1;
                            floorOverride.height = 1;
                            floorOverride.overrideCellFloorType = false;
                            floorOverride.cellFloorType = CellVisualData.CellFloorType.Stone;
                            floorOverride.overrideTileIndex = false;
                            floorOverride.TilesetsToOverrideFloorTile = new GlobalDungeonData.ValidTilesets[0];
                            floorOverride.OverrideFloorTiles = new int[0];
                            floorOverride.preventsOtherFloorDecoration = false;
                            floorOverride.allowWallDecorationTho = true;*/
                        }
                        

                        if (AllowGlitchShader && UnityEngine.Random.value <= 0.4f) {
                            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                            float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                            float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                            float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);

                            ExpandShaders.Instance.ApplyGlitchShader(m_GlitchSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                        }
                    }
                }
            }
        }
        
        public static tk2dSpriteAnimationClip DuplicateAnimationClip(tk2dSpriteAnimationClip sourceClip, tk2dSpriteCollectionData overrideCollection = null) {
            if (sourceClip == null) {
                return new tk2dSpriteAnimationClip() {
                    name = string.Empty,
                    frames = new tk2dSpriteAnimationFrame[0],
                    fps = 30,
                    loopStart = 0,
                    wrapMode = tk2dSpriteAnimationClip.WrapMode.Loop,
                    minFidgetDuration = 1,
                    maxFidgetDuration = 2
                };
            }

            tk2dSpriteAnimationClip m_CachedClip = new tk2dSpriteAnimationClip() {
                name = sourceClip.name,
                fps = sourceClip.fps,
                loopStart = sourceClip.loopStart,
                wrapMode = sourceClip.wrapMode,
                minFidgetDuration = sourceClip.minFidgetDuration,
                maxFidgetDuration = sourceClip.maxFidgetDuration
            };

            List<tk2dSpriteAnimationFrame> m_FrameList = new List<tk2dSpriteAnimationFrame>();

            foreach (tk2dSpriteAnimationFrame Frame in sourceClip.frames) {
                if (Frame != null) {
                    m_FrameList.Add(
                        new tk2dSpriteAnimationFrame() {
                            spriteCollection = overrideCollection != null ? overrideCollection : Frame.spriteCollection,
                            spriteId = Frame.spriteId,
                            invulnerableFrame = Frame.invulnerableFrame,
                            groundedFrame = Frame.groundedFrame,
                            requiresOffscreenUpdate = Frame.requiresOffscreenUpdate,
                            eventAudio = Frame.eventAudio,
                            eventVfx = Frame.eventVfx,
                            eventStopVfx = Frame.eventStopVfx,
                            eventLerpEmissive = Frame.eventLerpEmissive,
                            eventLerpEmissiveTime = Frame.eventLerpEmissiveTime,
                            eventLerpEmissivePower = Frame.eventLerpEmissivePower,
                            forceMaterialUpdate = Frame.forceMaterialUpdate,
                            finishedSpawning = Frame.finishedSpawning,
                            triggerEvent = Frame.triggerEvent,
                            eventInfo = Frame.eventInfo,
                            eventInt = Frame.eventInt,
                            eventFloat = Frame.eventFloat,
                            eventOutline = Frame.eventOutline
                        }    
                    );
                }
            }

            m_CachedClip.frames = m_FrameList.ToArray();

            return m_CachedClip;
        }
        
        public static IntVector2? GetRandomAvailableCellSmart(RoomHandler CurrentRoom, IntVector2 Clearence, bool relativeToRoom = false) {            
            CellValidator cellValidator = delegate (IntVector2 c) {
                for (int X = 0; X < Clearence.x; X++) {
                    for (int Y = 0; Y < Clearence.y; Y++) {
                        if (!GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(c.x + X, c.y + Y) || 
                             GameManager.Instance.Dungeon.data[c.x + X, c.y + Y].type == CellType.PIT || 
                             GameManager.Instance.Dungeon.data[c.x + X, c.y + Y].isOccupied ||
                             GameManager.Instance.Dungeon.data[c.x + X, c.y + Y].type == CellType.WALL)
                        {
                            return false;
                        }
                    }
                }
                return true;
            };
            if (relativeToRoom) {
                return CurrentRoom.GetRandomAvailableCell(new IntVector2?(new IntVector2(Clearence.x, Clearence.y)), new CellTypes?(CellTypes.FLOOR), false, cellValidator) - CurrentRoom.area.basePosition;
            } else {
                return CurrentRoom.GetRandomAvailableCell(new IntVector2?(new IntVector2(Clearence.x, Clearence.y)), new CellTypes?(CellTypes.FLOOR), false, cellValidator);
            }            
        }

        public static IntVector2 GetRandomAvailableCellSmart(RoomHandler CurrentRoom, PlayerController PrimaryPlayer, int MinClearence = 2, bool usePlayerVectorAsFallback = false) {
            Vector2 PlayerVector2 = Vector2.zero;
            IntVector2 PlayerIntVector2 = IntVector2.Zero;
            
            if (PrimaryPlayer) {
                PlayerVector2 = PrimaryPlayer.CenterPosition;
                PlayerIntVector2 = PlayerVector2.ToIntVector2(VectorConversions.Floor);
            }

            CellValidator cellValidator = delegate (IntVector2 c) {
                for (int l = 0; l < MinClearence; l++) {
                    for (int m = 0; m < MinClearence; m++) {
                        if (!GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(c.x + l, c.y + m) || 
                             GameManager.Instance.Dungeon.data[c.x + l, c.y + m].type == CellType.PIT || 
                             GameManager.Instance.Dungeon.data[c.x + l, c.y + m].isOccupied ||
                             GameManager.Instance.Dungeon.data[c.x + l, c.y + m].type == CellType.WALL)
                        {
                            return false;
                        }
                    }
                }
                return true;
            };

            IntVector2? randomAvailableCell = CurrentRoom.GetRandomAvailableCell(new IntVector2?(new IntVector2(MinClearence, MinClearence)), new CellTypes?(CellTypes.FLOOR), false, cellValidator);
            IntVector2 SelectedVector;
            if (randomAvailableCell.HasValue) {
                SelectedVector = randomAvailableCell.Value;
                return SelectedVector;
            } else {
                if (usePlayerVectorAsFallback) { return PlayerIntVector2; } else { return IntVector2.Zero; }
            }
        }

        public static IntVector2? GetRandomAvailableCellForPlayer(Dungeon dungeon, RoomHandler currentRoom, bool relativeToRoom = false) {
            List<IntVector2> validCellsCached = new List<IntVector2>();
            for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                for (int height = -1; height <= currentRoom.area.dimensions.y; height++) {
                    int X = currentRoom.area.basePosition.x + Width;
                    int Y = currentRoom.area.basePosition.y + height;
                    if (!dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) &&
                        !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) &&
                        !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) &&
                        !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) &&
                        !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) &&
                        !dungeon.data[X - 2, Y + 2].isOccupied && !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied && !dungeon.data[X + 2, Y + 2].isOccupied &&
                        !dungeon.data[X - 2, Y + 1].isOccupied && !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied && !dungeon.data[X + 2, Y + 1].isOccupied &&
                        !dungeon.data[X - 2, Y].isOccupied && !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied && !dungeon.data[X + 2, Y].isOccupied &&
                        !dungeon.data[X - 2, Y - 1].isOccupied && !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied && !dungeon.data[X + 2, Y - 1].isOccupied &&
                        !dungeon.data[X - 2, Y - 2].isOccupied && !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied && !dungeon.data[X + 2, Y - 2].isOccupied &&
                        !dungeon.data.isPit(X - 2, Y + 2) && !dungeon.data.isPit(X - 1, Y + 2) && !dungeon.data.isPit(X, Y + 2) && !dungeon.data.isPit(X + 1, Y + 2) && !dungeon.data.isPit(X + 2, Y + 2) &&
                        !dungeon.data.isPit(X - 2, Y + 1) && !dungeon.data.isPit(X - 1, Y + 1) && !dungeon.data.isPit(X, Y + 1) && !dungeon.data.isPit(X + 1, Y + 1) && !dungeon.data.isPit(X + 2, Y + 1) &&
                        !dungeon.data.isPit(X - 2, Y) && !dungeon.data.isPit(X - 1, Y) && !dungeon.data.isPit(X, Y) && !dungeon.data.isPit(X + 1, Y) && !dungeon.data.isPit(X + 2, Y) &&
                        !dungeon.data.isPit(X - 2, Y - 1) && !dungeon.data.isPit(X - 1, Y - 1) && !dungeon.data.isPit(X, Y - 1) && !dungeon.data.isPit(X + 1, Y - 1) && !dungeon.data.isPit(X + 2, Y - 1) &&
                        !dungeon.data.isPit(X - 2, Y - 2) && !dungeon.data.isPit(X - 1, Y - 2) && !dungeon.data.isPit(X, Y - 2) && !dungeon.data.isPit(X + 1, Y - 2) && !dungeon.data.isPit(X + 2, Y - 2))
                    {
                        validCellsCached.Add(new IntVector2(X, Y));
                    }
                }
            }
            if (validCellsCached.Count > 0) {
                IntVector2 SelectedCell = BraveUtility.RandomElement(validCellsCached);
                IntVector2 RegisteredCell = (SelectedCell);
                validCellsCached.Remove(SelectedCell);
                if (relativeToRoom) {
                    return (SelectedCell - currentRoom.area.basePosition);
                } else {
                    return (SelectedCell);
                }
            } else {
                return null;
            }
        }

        public static void CorrectForWalls(AIActor targetActor) {
            bool flag = PhysicsEngine.Instance.OverlapCast(targetActor.specRigidbody, null, true, false, null, null, false, null, null, new SpeculativeRigidbody[0]);
            if (flag) {
                Vector2 a = targetActor.gameObject.transform.position.XY();
                IntVector2[] cardinalsAndOrdinals = IntVector2.CardinalsAndOrdinals;
                int num = 0;
                int num2 = 1;
                for (;;) {
                    for (int i = 0; i < cardinalsAndOrdinals.Length; i++) {
                        targetActor.gameObject.transform.position = a + PhysicsEngine.PixelToUnit(cardinalsAndOrdinals[i] * num2);
                        targetActor.specRigidbody.Reinitialize();
                        if (!PhysicsEngine.Instance.OverlapCast(targetActor.specRigidbody, null, true, false, null, null, false, null, null, new SpeculativeRigidbody[0])) { return; }
                    }
                    num2++;
                    num++;
                    if (num > 200) {
                        Debug.LogError("FREEZE AVERTED!  TELL RUBEL!  (you're welcome) 147");
                        return;
                    }
                }                
            }
            return;
        }

        public static RoomHandler AddCustomRuntimeRoom(Dungeon dungeon, IntVector2 dimensions, GameObject roomPrefab, IntVector2? roomWorldPositionOverride = null,  Vector3? roomPrefabPositionOverride = null) {
            IntVector2 RoomPosition = new IntVector2(10, 10);
            if (roomWorldPositionOverride.HasValue) { RoomPosition = roomWorldPositionOverride.Value; }
            IntVector2 intVector = new IntVector2(dungeon.data.Width + RoomPosition.x, RoomPosition.y);
            int newWidth = dungeon.data.Width + RoomPosition.x + dimensions.x;
            int newHeight = Mathf.Max(dungeon.data.Height, dimensions.y + RoomPosition.y);
            CellData[][] array = BraveUtility.MultidimensionalArrayResize(dungeon.data.cellData, dungeon.data.Width, dungeon.data.Height, newWidth, newHeight);
            CellArea cellArea = new CellArea(intVector, dimensions, 0);
            cellArea.IsProceduralRoom = true;
            dungeon.data.cellData = array;
            dungeon.data.ClearCachedCellData();
            RoomHandler roomHandler = new RoomHandler(cellArea);
            for (int i = 0; i < dimensions.x; i++) {
                for (int j = 0; j < dimensions.y; j++) {
                    IntVector2 p = new IntVector2(i, j) + intVector;
                    CellData cellData = new CellData(p, CellType.FLOOR);
                    cellData.parentArea = cellArea;
                    cellData.parentRoom = roomHandler;
                    cellData.nearestRoom = roomHandler;
                    array[p.x][p.y] = cellData;
                    roomHandler.RuntimeStampCellComplex(p.x, p.y, CellType.FLOOR, DiagonalWallType.NONE);
                    
                }
            }
            dungeon.data.rooms.Add(roomHandler);
            if (roomPrefabPositionOverride.HasValue) {
                float X = roomPrefabPositionOverride.Value.x;
                float Y = roomPrefabPositionOverride.Value.x;
                UnityEngine.Object.Instantiate(roomPrefab, new Vector3(intVector.x + X, intVector.y + Y, 0f), Quaternion.identity);
            } else {
                UnityEngine.Object.Instantiate(roomPrefab, new Vector3(intVector.x, intVector.y, 0f), Quaternion.identity);
            }
            DeadlyDeadlyGoopManager.ReinitializeData();
            return roomHandler;
        }
        public static RoomHandler AddCustomRuntimeRoom(PrototypeDungeonRoom prototype, bool addRoomToMinimap = true, bool addTeleporter = true, bool isSecretRatExitRoom = false, Action<RoomHandler> postProcessCellData = null, DungeonData.LightGenerationStyle lightStyle = DungeonData.LightGenerationStyle.STANDARD, bool allowProceduralDecoration = true, bool allowProceduralLightFixtures = true, bool suppressExceptionMessages = false) {
            Dungeon dungeon = GameManager.Instance.Dungeon;           
            tk2dTileMap m_tilemap = dungeon.MainTilemap;

            if (m_tilemap == null) {
                ETGModConsole.Log("ERROR: TileMap object is null! Something seriously went wrong!");
                Debug.Log("ERROR: TileMap object is null! Something seriously went wrong!");
                return null;
            }

            /*TK2DDungeonAssembler assembler = new TK2DDungeonAssembler();
            assembler.Initialize(dungeon.tileIndices);*/
            TK2DDungeonAssembler assembler = ReflectionHelpers.ReflectGetField<TK2DDungeonAssembler>(typeof(Dungeon), "assembler", dungeon);

            IntVector2 basePosition = IntVector2.Zero;
            IntVector2 basePosition2 = new IntVector2(50, 50);
            int num = basePosition2.x;
            int num2 = basePosition2.y;
            IntVector2 intVector = new IntVector2(int.MaxValue, int.MaxValue);
            IntVector2 intVector2 = new IntVector2(int.MinValue, int.MinValue);
            intVector = IntVector2.Min(intVector, basePosition);
            intVector2 = IntVector2.Max(intVector2, basePosition + new IntVector2(prototype.Width, prototype.Height));
            IntVector2 a = intVector2 - intVector;
            IntVector2 b = IntVector2.Min(IntVector2.Zero, -1 * intVector);
            a += b;
            IntVector2 intVector3 = new IntVector2(dungeon.data.Width + num, num);
            int newWidth = dungeon.data.Width + num * 2 + a.x;
            int newHeight = Mathf.Max(dungeon.data.Height, a.y + num * 2);
            CellData[][] array = BraveUtility.MultidimensionalArrayResize(dungeon.data.cellData, dungeon.data.Width, dungeon.data.Height, newWidth, newHeight);
            dungeon.data.cellData = array;
            dungeon.data.ClearCachedCellData();
            IntVector2 d = new IntVector2(prototype.Width, prototype.Height);
            IntVector2 b2 = basePosition + b;
            IntVector2 intVector4 = intVector3 + b2;
            CellArea cellArea = new CellArea(intVector4, d, 0);
            cellArea.prototypeRoom = prototype;
            RoomHandler targetRoom = new RoomHandler(cellArea);
            for (int k = -num; k < d.x + num; k++) {
                for (int l = -num; l < d.y + num; l++) {
                    IntVector2 p = new IntVector2(k, l) + intVector4;
                    if ((k >= 0 && l >= 0 && k < d.x && l < d.y) || array[p.x][p.y] == null) {
                        CellData cellData = new CellData(p, CellType.WALL);
                        cellData.positionInTilemap = cellData.positionInTilemap - intVector3 + new IntVector2(num2, num2);
                        cellData.parentArea = cellArea;
                        cellData.parentRoom = targetRoom;
                        cellData.nearestRoom = targetRoom;
                        cellData.distanceFromNearestRoom = 0f;
                        array[p.x][p.y] = cellData;
                    }
                }
            }
            dungeon.data.rooms.Add(targetRoom);                        
            try {
                targetRoom.WriteRoomData(dungeon.data);
            } catch (Exception) {
                if (!suppressExceptionMessages) {
                    ETGModConsole.Log("WARNING: Exception caused during WriteRoomData step on room: " + targetRoom.GetRoomName());
                }
            } try {
                dungeon.data.GenerateLightsForRoom(dungeon.decoSettings, targetRoom, GameObject.Find("_Lights").transform, lightStyle);
            } catch (Exception) {
                if (!suppressExceptionMessages) {
                    ETGModConsole.Log("WARNING: Exception caused during GeernateLightsForRoom step on room: " + targetRoom.GetRoomName());
                }
            }

            postProcessCellData?.Invoke(targetRoom);

            if (targetRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.SECRET) { targetRoom.BuildSecretRoomCover(); }
            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(BraveResources.Load("RuntimeTileMap", ".prefab"));
            tk2dTileMap component = gameObject.GetComponent<tk2dTileMap>();
            string str = UnityEngine.Random.Range(10000, 99999).ToString();
            gameObject.name = "Glitch_" + "RuntimeTilemap_" + str;
            component.renderData.name = "Glitch_" + "RuntimeTilemap_" + str + " Render Data";
            component.Editor__SpriteCollection = dungeon.tileIndices.dungeonCollection;
            try {
                TK2DDungeonAssembler.RuntimeResizeTileMap(component, a.x + num2 * 2, a.y + num2 * 2, m_tilemap.partitionSizeX, m_tilemap.partitionSizeY);
                IntVector2 intVector5 = new IntVector2(prototype.Width, prototype.Height);
                IntVector2 b3 = basePosition + b;
                IntVector2 intVector6 = intVector3 + b3;
                for (int num4 = -num2; num4 < intVector5.x + num2; num4++) {
                    for (int num5 = -num2; num5 < intVector5.y + num2 + 2; num5++) {
                        assembler.BuildTileIndicesForCell(dungeon, component, intVector6.x + num4, intVector6.y + num5);
                    }
                }
                RenderMeshBuilder.CurrentCellXOffset = intVector3.x - num2;
                RenderMeshBuilder.CurrentCellYOffset = intVector3.y - num2;
                component.ForceBuild();
                RenderMeshBuilder.CurrentCellXOffset = 0;
                RenderMeshBuilder.CurrentCellYOffset = 0;
                component.renderData.transform.position = new Vector3(intVector3.x - num2, intVector3.y - num2, intVector3.y - num2);
            } catch (Exception ex) {
                if (!suppressExceptionMessages) {
                    ETGModConsole.Log("WARNING: Exception occured during RuntimeResizeTileMap / RenderMeshBuilder steps!");
                    Debug.Log("WARNING: Exception occured during RuntimeResizeTileMap/RenderMeshBuilder steps!");
                    Debug.LogException(ex);
                }
            }
            targetRoom.OverrideTilemap = component;
            if (allowProceduralLightFixtures) {
                for (int num7 = 0; num7 < targetRoom.area.dimensions.x; num7++) {
                    for (int num8 = 0; num8 < targetRoom.area.dimensions.y + 2; num8++) {
                        IntVector2 intVector7 = targetRoom.area.basePosition + new IntVector2(num7, num8);
                        if (dungeon.data.CheckInBoundsAndValid(intVector7)) {
                            CellData currentCell = dungeon.data[intVector7];
                            TK2DInteriorDecorator.PlaceLightDecorationForCell(dungeon, component, currentCell, intVector7);
                        }
                    }
                }
            }

            Pathfinder.Instance.InitializeRegion(dungeon.data, targetRoom.area.basePosition + new IntVector2(-3, -3), targetRoom.area.dimensions + new IntVector2(3, 3));
            
            if (prototype.usesProceduralDecoration && prototype.allowFloorDecoration && allowProceduralDecoration) {
                TK2DInteriorDecorator decorator = new TK2DInteriorDecorator(assembler);
                try {
                    decorator.HandleRoomDecoration(targetRoom, dungeon, m_tilemap);
                } catch (Exception ex) {
                    if (ExpandSettings.debugMode && !suppressExceptionMessages) {
                        ETGModConsole.Log("WARNING: Exception occured during HandleRoomDecoration steps!");
                        Debug.Log("WARNING: Exception occured during RuntimeResizeTileMap/RenderMeshBuilder steps!");
                        Debug.LogException(ex);
                    }
                }
            }

            targetRoom.PostGenerationCleanup();

            if (addRoomToMinimap) {
                targetRoom.visibility = RoomHandler.VisibilityStatus.VISITED;
                GameManager.Instance.StartCoroutine(Minimap.Instance.RevealMinimapRoomInternal(targetRoom, true, true, false));
                if (isSecretRatExitRoom) { targetRoom.visibility = RoomHandler.VisibilityStatus.OBSCURED; }
            }         
            if (addTeleporter) { targetRoom.AddProceduralTeleporterToRoom(); }
            if (addRoomToMinimap) { Minimap.Instance.InitializeMinimap(dungeon.data); }
            DeadlyDeadlyGoopManager.ReinitializeData();
            return targetRoom;
        }

        public static RoomHandler AddCustomRuntimeRoomWithTileSet(Dungeon dungeon2, PrototypeDungeonRoom prototype, bool addRoomToMinimap = true, bool addTeleporter = true, bool isSecretRatExitRoom = false, Action<RoomHandler> postProcessCellData = null, DungeonData.LightGenerationStyle lightStyle = DungeonData.LightGenerationStyle.STANDARD, bool allowProceduralDecoration = true, bool allowProceduralLightFixtures = true, bool RoomExploredOnMinimap = true, string RunTimeTileMapName = "Glitch") {
            Dungeon dungeon = GameManager.Instance.Dungeon;           
            tk2dTileMap m_tilemap = dungeon.MainTilemap;

            if (m_tilemap == null) {
                ETGModConsole.Log("ERROR: TileMap object is null! Something seriously went wrong!");
                Debug.Log("ERROR: TileMap object is null! Something seriously went wrong!");
                return null;
            }

            ExpandTK2DDungeonAssembler assembler = new ExpandTK2DDungeonAssembler();
            assembler.Initialize(dungeon2.tileIndices);

            IntVector2 basePosition = IntVector2.Zero;
            IntVector2 basePosition2 = new IntVector2(50, 50);
            int num = basePosition2.x;
            int num2 = basePosition2.y;
            IntVector2 intVector = new IntVector2(int.MaxValue, int.MaxValue);
            IntVector2 intVector2 = new IntVector2(int.MinValue, int.MinValue);
            intVector = IntVector2.Min(intVector, basePosition);
            intVector2 = IntVector2.Max(intVector2, basePosition + new IntVector2(prototype.Width, prototype.Height));
            IntVector2 a = intVector2 - intVector;
            IntVector2 b = IntVector2.Min(IntVector2.Zero, -1 * intVector);
            a += b;
            IntVector2 intVector3 = new IntVector2(dungeon.data.Width + num, num);
            int newWidth = dungeon.data.Width + num * 2 + a.x;
            int newHeight = Mathf.Max(dungeon.data.Height, a.y + num * 2);
            CellData[][] array = BraveUtility.MultidimensionalArrayResize(dungeon.data.cellData, dungeon.data.Width, dungeon.data.Height, newWidth, newHeight);
            dungeon.data.cellData = array;
            dungeon.data.ClearCachedCellData();
            IntVector2 d = new IntVector2(prototype.Width, prototype.Height);
            IntVector2 b2 = basePosition + b;
            IntVector2 intVector4 = intVector3 + b2;
            CellArea cellArea = new CellArea(intVector4, d, 0);
            cellArea.prototypeRoom = prototype;
            RoomHandler targetRoom = new RoomHandler(cellArea);
            for (int k = -num; k < d.x + num; k++) {
                for (int l = -num; l < d.y + num; l++) {
                    IntVector2 p = new IntVector2(k, l) + intVector4;
                    if ((k >= 0 && l >= 0 && k < d.x && l < d.y) || array[p.x][p.y] == null) {
                        CellData cellData = new CellData(p, CellType.WALL);
                        cellData.positionInTilemap = cellData.positionInTilemap - intVector3 + new IntVector2(num2, num2);
                        cellData.parentArea = cellArea;
                        cellData.parentRoom = targetRoom;
                        cellData.nearestRoom = targetRoom;
                        cellData.distanceFromNearestRoom = 0f;
                        array[p.x][p.y] = cellData;
                    }
                }
            }
            dungeon.data.rooms.Add(targetRoom);                        
            try {
                targetRoom.WriteRoomData(dungeon.data);
            } catch (Exception) {
                ETGModConsole.Log("WARNING: Exception caused during WriteRoomData step on room: " + targetRoom.GetRoomName());
            } try {
                GenerateLightsForRoomFromOtherTileset(dungeon2.decoSettings, targetRoom, GameObject.Find("_Lights").transform, dungeon, dungeon2, lightStyle);
            } catch (Exception ex) {
                ETGModConsole.Log("WARNING: Exception caused during GenerateLightsForRoom step on room: " + targetRoom.GetRoomName());
                ETGModConsole.Log("WARNING: Trying fall back code..." + targetRoom.GetRoomName());
                Debug.LogException(ex);
                try {
                    dungeon.data.GenerateLightsForRoom(dungeon.decoSettings, targetRoom, GameObject.Find("_Lights").transform, lightStyle);
                } catch (Exception ex2) {
                    ETGModConsole.Log("WARNING: Exception caused during GenerateLightsForRoom step on room while attempting fall back code: " + targetRoom.GetRoomName());
                    Debug.LogException(ex2);
                }
            }
            postProcessCellData?.Invoke(targetRoom);
            
            if (targetRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.SECRET) { targetRoom.BuildSecretRoomCover(); }

            MaybeSpawnWallMimics(dungeon, targetRoom, dungeon2.tileIndices.tilesetId, dungeon2.tileIndices.dungeonCollection);
            

            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(BraveResources.Load("RuntimeTileMap", ".prefab"));
            tk2dTileMap component = gameObject.GetComponent<tk2dTileMap>();
            string str = UnityEngine.Random.Range(10000, 99999).ToString();
            gameObject.name = RunTimeTileMapName + "_RuntimeTilemap_" + str;
            component.renderData.name = "Glitch_" + "RuntimeTilemap_" + str + " Render Data";
            component.Editor__SpriteCollection = dungeon2.tileIndices.dungeonCollection;
            try {
                ExpandTK2DDungeonAssembler.RuntimeResizeTileMap(component, a.x + num2 * 2, a.y + num2 * 2, m_tilemap.partitionSizeX, m_tilemap.partitionSizeY);
                IntVector2 intVector5 = new IntVector2(prototype.Width, prototype.Height);
                IntVector2 b3 = basePosition + b;
                IntVector2 intVector6 = intVector3 + b3;
                for (int num4 = -num2; num4 < intVector5.x + num2; num4++) {
                    for (int num5 = -num2; num5 < intVector5.y + num2 + 2; num5++) {
                        assembler.BuildTileIndicesForCell(dungeon, dungeon2, component, intVector6.x + num4, intVector6.y + num5);
                    }
                }
                RenderMeshBuilder.CurrentCellXOffset = intVector3.x - num2;
                RenderMeshBuilder.CurrentCellYOffset = intVector3.y - num2;
                component.ForceBuild();
                RenderMeshBuilder.CurrentCellXOffset = 0;
                RenderMeshBuilder.CurrentCellYOffset = 0;
                component.renderData.transform.position = new Vector3(intVector3.x - num2, intVector3.y - num2, intVector3.y - num2);
            } catch (Exception ex) {
                ETGModConsole.Log("WARNING: Exception occured during RuntimeResizeTileMap / RenderMeshBuilder steps!");
                Debug.Log("WARNING: Exception occured during RuntimeResizeTileMap/RenderMeshBuilder steps!");
                Debug.LogException(ex);
                return null; // Return null to prevent lead key from using this room. In most cases the resulting room is not usable as walls did not generate and there is no collision.
            }
            targetRoom.OverrideTilemap = component;
            if (allowProceduralLightFixtures) {
                for (int num7 = 0; num7 < targetRoom.area.dimensions.x; num7++) {
                    for (int num8 = 0; num8 < targetRoom.area.dimensions.y + 2; num8++) {
                        IntVector2 intVector7 = targetRoom.area.basePosition + new IntVector2(num7, num8);
                        if (dungeon.data.CheckInBoundsAndValid(intVector7)) {
                            CellData currentCell = dungeon.data[intVector7];
                            ExpandTK2DInteriorDecorator.PlaceLightDecorationForCell(dungeon, component, currentCell, intVector7);
                        }
                    }
                }
            }

            Pathfinder.Instance.InitializeRegion(dungeon.data, targetRoom.area.basePosition + new IntVector2(-3, -3), targetRoom.area.dimensions + new IntVector2(3, 3));
            
            if (prototype.usesProceduralDecoration && prototype.allowFloorDecoration && allowProceduralDecoration) {
                ExpandTK2DInteriorDecorator decorator = new ExpandTK2DInteriorDecorator(assembler);
                try {
                    decorator.HandleRoomDecoration(targetRoom, dungeon, dungeon2, m_tilemap);
                } catch (Exception ex) {
                    if (ExpandSettings.debugMode) {
                        ETGModConsole.Log("WARNING: Exception occured during HandleRoomDecoration steps!");
                        Debug.Log("WARNING: Exception occured during RuntimeResizeTileMap/RenderMeshBuilder steps!");
                        Debug.LogException(ex);
                    }
                }
            }

            targetRoom.PostGenerationCleanup();

            GameObject m_CollisionObject = new GameObject(component.renderData.name + "_CollisionObject");
            IntVector2 targetRoomPosition = targetRoom.area.basePosition;
            m_CollisionObject.transform.parent = targetRoom.hierarchyParent;
            IntVector2 targetRoomSize = targetRoom.area.dimensions;
            m_CollisionObject.transform.position = targetRoomPosition.ToVector3();

            for (int width = -2; width < targetRoomSize.x + 2; width++) {
                for (int height = -2; height < targetRoomSize.y + 2; height++) {
                    int X = targetRoomPosition.x + width;
                    int Y = targetRoomPosition.y + height;
                    if (dungeon.data.isWall(X, Y)) {
                        IntVector2 positionOffset = new IntVector2(width, height);
                        if (dungeon.data.isFaceWallLower(X, Y)) {
                            GenerateOrAddToRigidBody(m_CollisionObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, dimensions: IntVector2.One, offset: positionOffset);
                        } else {
                            GenerateOrAddToRigidBody(m_CollisionObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, dimensions: IntVector2.One, offset: positionOffset);
                        }
                    }
                }
            }

            if (!m_CollisionObject.GetComponent<SpeculativeRigidbody>()) { UnityEngine.Object.Destroy(m_CollisionObject); }

            ExpandFloorDecorator.PlaceFloorDecoration(dungeon, new List<RoomHandler>() { targetRoom });

            HandleSpecificRoomAGDInjection(targetRoom, dungeon, dungeon2.tileIndices.tilesetId);

            if (addRoomToMinimap) {
                if (RoomExploredOnMinimap) {
                    targetRoom.visibility = RoomHandler.VisibilityStatus.VISITED;
                } else {
                    targetRoom.visibility = RoomHandler.VisibilityStatus.OBSCURED;
                }
                GameManager.Instance.StartCoroutine(Minimap.Instance.RevealMinimapRoomInternal(targetRoom, true, true, false));
                if (isSecretRatExitRoom && RoomExploredOnMinimap) { targetRoom.visibility = RoomHandler.VisibilityStatus.OBSCURED; }
            }         
            if (addTeleporter) { targetRoom.AddProceduralTeleporterToRoom(); }
            if (addRoomToMinimap) { Minimap.Instance.InitializeMinimap(dungeon.data); }
            DeadlyDeadlyGoopManager.ReinitializeData();
            
            return targetRoom;
        }
        
        public static void MaybeSpawnWallMimics(Dungeon dungeon, RoomHandler currentRoom, GlobalDungeonData.ValidTilesets TilesetOverride = GlobalDungeonData.ValidTilesets.CASTLEGEON, bool GuranteedWallMimic = false, int OverrideWallMimicCount = -1, tk2dSpriteCollectionData FakeWallDungeonCollectionOverride = null) {
            float RandomValue = 0;

            GameObject TempObject = new GameObject("TempObject", new Type[] { typeof(ExpandRandomVarGenerator) });
            RandomValue = TempObject.GetComponent<ExpandRandomVarGenerator>().GenerateRandomFloat();
            UnityEngine.Object.Destroy(TempObject);

            if (!GuranteedWallMimic && !ExpandPlaceWallMimic.PlayerHasWallMimicItem && RandomValue < 0.9f) { return; }

            string RoomName = "NULL";

            if (!string.IsNullOrEmpty(currentRoom.GetRoomName())) { RoomName = currentRoom.GetRoomName(); }

            if (currentRoom.PrecludeTilemapDrawing | currentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.SECRET | currentRoom.IsMaintenanceRoom()) {
                return;
            }

            if (!GuranteedWallMimic) {
                if (currentRoom.IsShop | currentRoom.GetRoomName().StartsWith("DraGunRoom") | ExpandPlaceWallMimic.BannedWallMimicRoomList.Contains(RoomName.ToLower())) {
                    return;
                }
                if (currentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS && BraveUtility.RandomBool()) { return; }
            }

            int WallMimicsPerRoom = 1;

            if (TilesetOverride != GlobalDungeonData.ValidTilesets.CASTLEGEON) { TilesetOverride = dungeon.tileIndices.tilesetId; }

            if (ExpandPlaceWallMimic.PlayerHasWallMimicItem) {
                switch (TilesetOverride) {
                    case GlobalDungeonData.ValidTilesets.CATHEDRALGEON:
                        WallMimicsPerRoom = 2;
                        break;
                    case GlobalDungeonData.ValidTilesets.BELLYGEON:
                        WallMimicsPerRoom = 2;
                        break;
                    case GlobalDungeonData.ValidTilesets.CATACOMBGEON:
                        WallMimicsPerRoom = 2;
                        break;
                    case GlobalDungeonData.ValidTilesets.OFFICEGEON:
                        WallMimicsPerRoom = 2;
                        break;
                    case GlobalDungeonData.ValidTilesets.WESTGEON:
                        WallMimicsPerRoom = 2;
                        break;
                    case GlobalDungeonData.ValidTilesets.FORGEGEON:
                        WallMimicsPerRoom = 2;
                        break;
                    case GlobalDungeonData.ValidTilesets.HELLGEON:
                        WallMimicsPerRoom = 3;
                        break;
                    default:
                        WallMimicsPerRoom = 1;
                        break;
                }
            }
                       

            if (OverrideWallMimicCount != -1) { WallMimicsPerRoom = OverrideWallMimicCount; }
            
            int NorthWallCount = 0;
            int WestWallCount = 0;
            int EastWallCount = 0;
            int WallMimicsPlaced = 0;
            int loopCount = 0;
            int SouthWallCount = 0;
            List<Tuple<IntVector2, DungeonData.Direction>> validWalls = new List<Tuple<IntVector2, DungeonData.Direction>>();
            try { 
                for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                    for (int Height = -1; Height <= currentRoom.area.dimensions.y; Height++) {
                        int X = currentRoom.area.basePosition.x + Width;
                        int Y = currentRoom.area.basePosition.y + Height;
                        if (dungeon.data.isWall(X, Y) && X % 4 == 0 && Y % 4 == 0 && dungeon.data.GetAbsoluteRoomFromPosition(new IntVector2(X, Y)) != null && dungeon.data.GetAbsoluteRoomFromPosition(new IntVector2(X, Y)) == currentRoom) {
                            int WallCount = 0;
                			if (!dungeon.data.isWall(X - 1, Y + 2) &&
                                !dungeon.data.isWall(X, Y + 2) && 
                                !dungeon.data.isWall(X + 1, Y + 2) &&
                                !dungeon.data.isWall(X + 2, Y + 2) &&
                				!dungeon.data.isWall(X - 1, Y + 1) &&
                                !dungeon.data.isWall(X, Y + 1) && 
                                !dungeon.data.isWall(X + 1, Y + 1) &&
                                !dungeon.data.isWall(X + 2, Y + 1) &&
                				dungeon.data.isWall(X - 1, Y) &&
                                dungeon.data.isWall(X, Y) && 
                                dungeon.data.isWall(X + 1, Y) &&
                                dungeon.data.isWall(X + 2, Y) && 
                				dungeon.data.isWall(X - 1, Y - 1) &&
                                dungeon.data.isWall(X, Y - 1) && 
                                dungeon.data.isWall(X + 1, Y - 1) &&
                                dungeon.data.isWall(X + 2, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X - 1, Y - 3) &&
                                !dungeon.data.isPlainEmptyCell(X, Y - 3) && 
                                !dungeon.data.isPlainEmptyCell(X + 1, Y - 3) &&
                                !dungeon.data.isPlainEmptyCell(X + 2, Y - 3))
                			{
                				validWalls.Add(Tuple.Create(new IntVector2(X, Y), DungeonData.Direction.NORTH));
                				WallCount++;
                                SouthWallCount++;
                            } else if (dungeon.data.isWall(X - 1, Y + 2) && 
                                dungeon.data.isWall(X, Y + 2) &&
                                dungeon.data.isWall(X + 1, Y + 2) &&
                                dungeon.data.isWall(X + 2, Y + 2) &&
                                dungeon.data.isWall(X - 1, Y + 1) &&
                                dungeon.data.isWall(X, Y + 1) &&
                                dungeon.data.isWall(X + 1, Y + 1) &&
                                dungeon.data.isWall(X + 2, Y + 1) && 
                				dungeon.data.isWall(X - 1, Y) &&
                                dungeon.data.isWall(X, Y) &&
                                dungeon.data.isWall(X + 1, Y) &&
                                dungeon.data.isWall(X + 2, Y) &&
                				dungeon.data.isPlainEmptyCell(X, Y - 1) &&
                                dungeon.data.isPlainEmptyCell(X + 1, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X, Y + 4) &&
                                !dungeon.data.isPlainEmptyCell(X + 1, Y + 4))
                			{
                				validWalls.Add(Tuple.Create(new IntVector2(X, Y), DungeonData.Direction.SOUTH));
                				WallCount++;
                                NorthWallCount++;
                            } else if (dungeon.data.isWall(X, Y + 2) &&
                				dungeon.data.isWall(X, Y + 1) &&
                				dungeon.data.isWall(X - 1, Y) &&
                				dungeon.data.isWall(X, Y - 1) &&
                				dungeon.data.isWall(X, Y - 2) &&
                				!dungeon.data.isPlainEmptyCell(X - 2, Y + 2) && 
                				!dungeon.data.isPlainEmptyCell(X - 2, Y + 1) && 
                				!dungeon.data.isPlainEmptyCell(X - 2, Y) &&
                				dungeon.data.isPlainEmptyCell(X + 1, Y) &&
                				dungeon.data.isPlainEmptyCell(X + 1, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X - 2, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X - 2, Y - 2))
                			{
                				validWalls.Add(Tuple.Create(new IntVector2(X, Y), DungeonData.Direction.EAST));
                				WallCount++;
                                WestWallCount++;
                            } else if (dungeon.data.isWall(X, Y + 2) && 
                				dungeon.data.isWall(X, Y + 1) &&
                				dungeon.data.isWall(X + 1, Y) &&
                				dungeon.data.isWall(X, Y - 1) &&
                				dungeon.data.isWall(X, Y - 2) &&
                				!dungeon.data.isPlainEmptyCell(X + 2, Y + 2) &&
                				!dungeon.data.isPlainEmptyCell(X + 2, Y + 1) &&
                				!dungeon.data.isPlainEmptyCell(X + 2, Y) &&
                				dungeon.data.isPlainEmptyCell(X - 1, Y) &&
                				dungeon.data.isPlainEmptyCell(X - 1, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X + 2, Y - 1) &&
                				!dungeon.data.isPlainEmptyCell(X + 2, Y - 2))
                			{
                				validWalls.Add(Tuple.Create(new IntVector2(X - 1, Y), DungeonData.Direction.WEST));
                				WallCount++;
                                EastWallCount++;
                            }
                			if (WallCount > 0) {
                				bool WallStillValid = true;
                                int XPadding = -5;
                				while (XPadding <= 5 && WallStillValid) {
                					int YPadding = -5;
                					while (YPadding <= 5 && WallStillValid) {
                						int x = X + XPadding;
                						int y = Y + YPadding;
                						if (dungeon.data.CheckInBoundsAndValid(x, y)) {
                							CellData cellData = dungeon.data[x, y];
                							if (cellData != null) {
                                                if (cellData.type == CellType.PIT | cellData.diagonalWallType != DiagonalWallType.NONE) { WallStillValid = false; }
                                            }
                						}
                						YPadding++;
                					}
                					XPadding++;
                				}
                				if (!WallStillValid) {
                					while (WallCount > 0) {
                						validWalls.RemoveAt(validWalls.Count - 1);
                						WallCount--;
                					}
                				}
                			}
                		}
                	}
                }
                if (validWalls.Count <= 0) {
                    if (ExpandSettings.debugMode) {
                        ETGModConsole.Log("[DEBUG] No valid locations found for room: " + RoomName + " while attempting Wall Mimic placement!", false);
                    }
                }
                while (loopCount < WallMimicsPerRoom && validWalls.Count > 0) {
                    if (validWalls.Count > 0) {
                        Tuple<IntVector2, DungeonData.Direction> WallCell = BraveUtility.RandomElement(validWalls);
                        IntVector2 Position = WallCell.First;
                        DungeonData.Direction Direction = WallCell.Second;
                        if (Direction != DungeonData.Direction.WEST) {
                            currentRoom.RuntimeStampCellComplex(Position.x, Position.y, CellType.FLOOR, DiagonalWallType.NONE);
                        }
                        if (Direction != DungeonData.Direction.EAST) {
                            currentRoom.RuntimeStampCellComplex(Position.x + 1, Position.y, CellType.FLOOR, DiagonalWallType.NONE);
                        }
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(GameManager.Instance.RewardManager.WallMimicChances.EnemyGuid);
                        AIActor WallMimic = AIActor.Spawn(orLoadByGuid, Position, currentRoom, true, AIActor.AwakenAnimationType.Default, true);
                        ExpandWallMimicManager wallMimicController = WallMimic.gameObject.GetComponent<ExpandWallMimicManager>();
                        if (wallMimicController) {
                            if (ExpandPlaceWallMimic.PlayerHasWallMimicItem) { wallMimicController.CursedBrickMode = true; }
                            wallMimicController.DungeonCollectionOverride = FakeWallDungeonCollectionOverride;
                            wallMimicController.SkipPlayerCheck = true;
                        }
                        validWalls.Remove(WallCell);
                        WallMimicsPlaced++;
                    }
                    loopCount++;
                }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] Exception while trying to place WallMimic(s) in room: " + currentRoom.GetRoomName(), false);
                    Debug.LogException(ex);
                }
                return;
            }
            if (WallMimicsPlaced > 0) {
            	if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] Wall Mimic(s) succesfully placed in room: " + currentRoom.GetRoomName(), false);
                    ETGModConsole.Log("[DEBUG] Number of Valid North Wall Mimics locations: " + NorthWallCount, false);
            		ETGModConsole.Log("[DEBUG] Number of Valid South Wall Mimics locations: " + SouthWallCount, false);
            		ETGModConsole.Log("[DEBUG] Number of Valid East Wall Mimics locations: " + EastWallCount, false);
            		ETGModConsole.Log("[DEBUG] Number of Valid West Wall Mimics locations: " + WestWallCount, false);
            		ETGModConsole.Log("[DEBUG] Number of Wall Mimics succesfully placed in room: " + WallMimicsPlaced, false);
            	}
                return;
            } else {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] No valid location found for room: " + currentRoom.GetRoomName() + " while attempting Wall Mimic placement!", false);
                }
                return;
            }
        }
     
        public static void GenerateLightsForRoomFromOtherTileset(TilemapDecoSettings decoSettings, RoomHandler rh, Transform lightParent, Dungeon dungeon, Dungeon dungeon2, DungeonData.LightGenerationStyle style = DungeonData.LightGenerationStyle.STANDARD) {
            if (!dungeon2.roomMaterialDefinitions[rh.RoomVisualSubtype].useLighting) { return; }
            
            bool flag = decoSettings.lightCookies.Length > 0;
            List<Tuple<IntVector2, float>> list = new List<Tuple<IntVector2, float>>();
            bool flag2 = false;
            List<IntVector2> list2;
            int count;
            if (rh.area != null && !rh.area.IsProceduralRoom && !rh.area.prototypeRoom.usesProceduralLighting) {
                list2 = rh.GatherManualLightPositions();
                count = list2.Count;
            } else {
                flag2 = true;
                list2 = rh.GatherOptimalLightPositions(decoSettings);
                count = list2.Count;
                if (rh.area != null && rh.area.prototypeRoom != null) { PostprocessLightPositions(dungeon, list2, rh); }
            }
            if (rh.area.prototypeRoom != null) {
                for (int i = 0; i < rh.area.instanceUsedExits.Count; i++) {
                    RuntimeRoomExitData runtimeRoomExitData = rh.area.exitToLocalDataMap[rh.area.instanceUsedExits[i]];
                    RuntimeExitDefinition runtimeExitDefinition = rh.exitDefinitionsByExit[runtimeRoomExitData];
                    if (runtimeRoomExitData.TotalExitLength > 4 && !runtimeExitDefinition.containsLight) {
                        IntVector2 first = (!runtimeRoomExitData.jointedExit) ? runtimeExitDefinition.GetLinearMidpoint(rh) : (runtimeRoomExitData.ExitOrigin - IntVector2.One);
                        list.Add(new Tuple<IntVector2, float>(first, 0.5f));
                        runtimeExitDefinition.containsLight = true;
                    }
                }
            }
            GlobalDungeonData.ValidTilesets tilesetId = dungeon2.tileIndices.tilesetId;
            float lightCullingPercentage = decoSettings.lightCullingPercentage;
            if (flag2 && lightCullingPercentage > 0f) {
                int num = Mathf.FloorToInt(list2.Count * lightCullingPercentage);
                int num2 = Mathf.FloorToInt(list.Count * lightCullingPercentage);
                if (num == 0 && num2 == 0 && list2.Count + list.Count > 4) {
                    num = 1;
                } while (num > 0 && list2.Count > 0) {
                    list2.RemoveAt(UnityEngine.Random.Range(0, list2.Count));
                    num--;
                } while (num2 > 0 && list.Count > 0) {
                    list.RemoveAt(UnityEngine.Random.Range(0, list.Count));
                    num2--;
                }
            }
            int count2 = list2.Count;
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.NONE && (tilesetId == GlobalDungeonData.ValidTilesets.MINEGEON || tilesetId == GlobalDungeonData.ValidTilesets.GUNGEON || tilesetId == GlobalDungeonData.ValidTilesets.CATACOMBGEON) && (flag2 || rh.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.NORMAL || rh.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.HUB || rh.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.CONNECTOR)) {
                list2.AddRange(rh.GatherPitLighting(decoSettings, list2));
            }
            for (int j = 0; j < list2.Count + list.Count; j++) {
                IntVector2 a = IntVector2.NegOne;
                float num3 = 1f;
                bool flag3 = false;
                if (j < list2.Count && j >= count2) {
                    flag3 = true;
                    num3 = 0.6f;
                }
                if (j < list2.Count) {
                    a = rh.area.basePosition + list2[j];
                } else {
                    a = rh.area.basePosition + list[j - list2.Count].First;
                    num3 = list[j - list2.Count].Second;
                }
                bool flag4 = false;
                if (flag && flag2 && a == rh.GetCenterCell()) { flag4 = true; }
                IntVector2 intVector = a + IntVector2.Up;
                bool flag5 = j >= count;
                bool flag6 = false;
                Vector3 b = Vector3.zero;
                if (dungeon.data[a + IntVector2.Up].type == CellType.WALL) {
                    dungeon.data[intVector].cellVisualData.lightDirection = DungeonData.Direction.NORTH;
                    b = Vector3.down;
                } else if (dungeon.data[a + IntVector2.Right].type == CellType.WALL) {
                    dungeon.data[intVector].cellVisualData.lightDirection = DungeonData.Direction.EAST;
                } else if (dungeon.data[a + IntVector2.Left].type == CellType.WALL) {
                    dungeon.data[intVector].cellVisualData.lightDirection = DungeonData.Direction.WEST;
                } else if (dungeon.data[a + IntVector2.Down].type == CellType.WALL) {
                    flag6 = true;
                    dungeon.data[intVector].cellVisualData.lightDirection = DungeonData.Direction.SOUTH;
                } else {
                    dungeon.data[intVector].cellVisualData.lightDirection = (DungeonData.Direction)(-1);
                }
                int num4 = rh.RoomVisualSubtype;
                float num5 = 0f;
                if (rh.area.prototypeRoom != null) {
                    PrototypeDungeonRoomCellData prototypeDungeonRoomCellData = (j >= list2.Count) ? rh.area.prototypeRoom.ForceGetCellDataAtPoint(list[j - list2.Count].First.x, list[j - list2.Count].First.y) : rh.area.prototypeRoom.ForceGetCellDataAtPoint(list2[j].x, list2[j].y);
                    if (prototypeDungeonRoomCellData != null && prototypeDungeonRoomCellData.containsManuallyPlacedLight) {
                        num4 = prototypeDungeonRoomCellData.lightStampIndex;
                        num5 = prototypeDungeonRoomCellData.lightPixelsOffsetY / 16f;
                    }
                }
                if (num4 < 0 || num4 >= dungeon2.roomMaterialDefinitions.Length) { num4 = 0; }
                DungeonMaterial dungeonMaterial = dungeon2.roomMaterialDefinitions[num4];
                int num6 = -1;
                GameObject original;
                if (style == DungeonData.LightGenerationStyle.FORCE_COLOR || style == DungeonData.LightGenerationStyle.RAT_HALLWAY) {
                    num6 = 0;
                    original = dungeonMaterial.lightPrefabs.elements[0].gameObject;
                } else {
                    original = dungeonMaterial.lightPrefabs.SelectByWeight(out num6, false);
                }
                if ((!dungeonMaterial.facewallLightStamps[num6].CanBeTopWallLight && flag6) || (!dungeonMaterial.facewallLightStamps[num6].CanBeCenterLight && flag5)) {
                    if (num6 >= dungeonMaterial.facewallLightStamps.Count) { num6 = 0; }
                    num6 = dungeonMaterial.facewallLightStamps[num6].FallbackIndex;
                    original = dungeonMaterial.lightPrefabs.elements[num6].gameObject;
                }
                GameObject gameObject = UnityEngine.Object.Instantiate(original, intVector.ToVector3(0f), Quaternion.identity);
                gameObject.transform.parent = lightParent;
                gameObject.transform.position = intVector.ToCenterVector3(intVector.y + decoSettings.lightHeight) + new Vector3(0f, num5, 0f) + b;
                ShadowSystem componentInChildren = gameObject.GetComponentInChildren<ShadowSystem>();
                Light componentInChildren2 = gameObject.GetComponentInChildren<Light>();
                if (componentInChildren2 != null) { componentInChildren2.intensity *= num3; }
                if (style == DungeonData.LightGenerationStyle.FORCE_COLOR || style == DungeonData.LightGenerationStyle.RAT_HALLWAY) {
                    SceneLightManager component = gameObject.GetComponent<SceneLightManager>();
                    if (component) {
                        Color[] validColors = new Color[] { component.validColors[0] };
                        component.validColors = validColors;
                    }
                }
                if (flag3 && componentInChildren != null) {
                    if (componentInChildren2) {
                        componentInChildren2.range += (dungeon2.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.CATACOMBGEON) ? 3 : 5;
                    }
                    componentInChildren.ignoreCustomFloorLight = true;
                }
                if (flag4 && flag && componentInChildren != null) {
                    componentInChildren.uLightCookie = decoSettings.GetRandomLightCookie();
                    componentInChildren.uLightCookieAngle = UnityEngine.Random.Range(0f, 6.28f);
                    componentInChildren2.intensity *= 1.5f;
                }
                if (dungeon.data[intVector].cellVisualData.lightDirection == DungeonData.Direction.NORTH) {
                    bool flag7 = true;
                    for (int k = -2; k < 3; k++) {
                        if (dungeon.data[intVector + IntVector2.Right * k].type == CellType.FLOOR) {
                            flag7 = false;
                            break;
                        }
                    }
                    if (flag7 && componentInChildren) {
                        GameObject original2 = (GameObject)BraveResources.Load("Global VFX/Wall_Light_Cookie", ".prefab");
                        GameObject gameObject2 = UnityEngine.Object.Instantiate(original2);
                        Transform transform = gameObject2.transform;
                        transform.parent = gameObject.transform;
                        transform.localPosition = Vector3.zero;
                        componentInChildren.PersonalCookies.Add(gameObject2.GetComponent<Renderer>());
                    }
                }
                CellData cellData = dungeon.data[intVector + new IntVector2(0, Mathf.RoundToInt(num5))];
                if (dungeon2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                    dungeon.data[cellData.position + IntVector2.Down].cellVisualData.containsObjectSpaceStamp = true;
                }
                BraveUtility.DrawDebugSquare(cellData.position.ToVector2(), Color.magenta, 1000f);
                cellData.cellVisualData.containsLight = true;
                cellData.cellVisualData.lightObject = gameObject;
                LightStampData facewallLightStampData = dungeonMaterial.facewallLightStamps[num6];
                LightStampData sidewallLightStampData = dungeonMaterial.sidewallLightStamps[num6];
                cellData.cellVisualData.facewallLightStampData = facewallLightStampData;
                cellData.cellVisualData.sidewallLightStampData = sidewallLightStampData;
            }
        }

        public static void PostprocessLightPositions(Dungeon dungeon, List<IntVector2> positions, RoomHandler room) {
            CheckCellNeedsAdditionalLight(positions, room, dungeon.data[room.GetCenterCell()]);
            for (int i = 0; i < room.Cells.Count; i++) {
                CellData currentCell = dungeon.data[room.Cells[i]];
                CheckCellNeedsAdditionalLight(positions, room, currentCell);
            }
        }

        public static bool CheckCellNeedsAdditionalLight(List<IntVector2> positions, RoomHandler room, CellData currentCell) {
            int num = (!room.area.IsProceduralRoom) ? 10 : 20;
            if (currentCell.isExitCell) { return false; }
            if (currentCell.type == CellType.WALL) { return false; }
            bool flag = true;
            for (int i = 0; i < positions.Count; i++) {
                int num2 = IntVector2.ManhattanDistance(positions[i] + room.area.basePosition, currentCell.position);
                if (num2 <= num) {
                    flag = false;
                    break;
                }
            }
            if (flag) { positions.Add(currentCell.position - room.area.basePosition); }
            return flag;
        }

        private static void HandleSpecificRoomAGDInjection(RoomHandler TargetRoom, Dungeon dungeon, GlobalDungeonData.ValidTilesets tilesetID, RunData runData = null, List<AGDEnemyReplacementTier> TierList = null) {
            List<AIActor> EnemyList1 = new List<AIActor>();
            // RunData runData = GameManager.Instance.RunData;
            if (runData == null) { runData = new RunData(); }
            if (TierList == null) { TierList = GameManager.Instance.EnemyReplacementTiers; }
            if (runData.AgdInjectionRunCounts == null || runData.AgdInjectionRunCounts.Length != TierList.Count) {
                runData.AgdInjectionRunCounts = new int[TierList.Count];
            }
            int[] agdInjectionRunCounts = runData.AgdInjectionRunCounts;
            for (int i = 0; i < TierList.Count; i++) {
                AGDEnemyReplacementTier agdenemyReplacementTier = TierList[i];
                int num = 0;
                if (agdenemyReplacementTier != null && agdenemyReplacementTier.TargetTileset == tilesetID && !agdenemyReplacementTier.ExcludeForPrereqs()) {
                    if (agdenemyReplacementTier.MaxPerRun <= 0 || agdInjectionRunCounts[i] < agdenemyReplacementTier.MaxPerRun) {   
                        if (TargetRoom.EverHadEnemies && TargetRoom.IsStandardRoom && !agdenemyReplacementTier.ExcludeRoomForColumns(dungeon.data, TargetRoom) && !agdenemyReplacementTier.ExcludeRoom(TargetRoom)) {   
                            TargetRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All, ref EnemyList1);
                            if (!agdenemyReplacementTier.ExcludeRoomForEnemies(TargetRoom, EnemyList1)) {
                                for (int I = 0; I < EnemyList1.Count; I++) {
                                    AIActor enemy = EnemyList1[I];
                                    if (enemy && (enemy.AdditionalSimpleItemDrops == null || enemy.AdditionalSimpleItemDrops.Count <= 0) && (!enemy.healthHaver || !enemy.healthHaver.IsBoss)) {
                                        if (((agdenemyReplacementTier.TargetAllSignatureEnemies && enemy.IsSignatureEnemy) || (agdenemyReplacementTier.TargetAllNonSignatureEnemies && !enemy.IsSignatureEnemy) || (agdenemyReplacementTier.TargetGuids != null && agdenemyReplacementTier.TargetGuids.Contains(enemy.EnemyGuid))) && UnityEngine.Random.value < agdenemyReplacementTier.ChanceToReplace) {
                                            Vector2? vector = null;
                                            if (agdenemyReplacementTier.RemoveAllOtherEnemies) {
                                                vector = new Vector2?(TargetRoom.area.Center);
                                                for (int J = EnemyList1.Count - 1; J >= 0; J--) {
                                                    AIActor aiactor2 = EnemyList1[I];
                                                    if (aiactor2) {
                                                        TargetRoom.DeregisterEnemy(aiactor2, true);
                                                        UnityEngine.Object.Destroy(aiactor2.gameObject);
                                                    }
                                                }
                                            } else {
                                                if (enemy.specRigidbody) {
                                                    enemy.specRigidbody.Initialize();
                                                    vector = new Vector2?(enemy.specRigidbody.UnitBottomLeft);
                                                }
                                                TargetRoom.DeregisterEnemy(enemy, true);
                                                UnityEngine.Object.Destroy(enemy.gameObject);
                                            }
                                            RoomHandler roomHandler2 = TargetRoom;
                                            string enemyGuid = BraveUtility.RandomElement(agdenemyReplacementTier.ReplacementGuids);
                                            Vector2? goalPosition = vector;
                                            roomHandler2.AddSpecificEnemyToRoomProcedurally(enemyGuid, false, goalPosition);
                                            num++;
                                            agdInjectionRunCounts[i]++;
                                            if ((agdenemyReplacementTier.MaxPerFloor > 0 && num >= agdenemyReplacementTier.MaxPerFloor) || (agdenemyReplacementTier.MaxPerRun > 0 && agdInjectionRunCounts[i] >= agdenemyReplacementTier.MaxPerRun) || agdenemyReplacementTier.RemoveAllOtherEnemies) {
                                                break;
                                            }
                                        }
                                    }
                                }
                                if ((agdenemyReplacementTier.MaxPerFloor > 0 && num >= agdenemyReplacementTier.MaxPerFloor) || (agdenemyReplacementTier.MaxPerRun > 0 && agdInjectionRunCounts[i] >= agdenemyReplacementTier.MaxPerRun)) {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            List<AIActor> EnemyList2 = new List<AIActor>();
            float newPlayerEnemyCullFactor = GameStatsManager.Instance.NewPlayerEnemyCullFactor;
            if (newPlayerEnemyCullFactor > 0f && TargetRoom.EverHadEnemies && TargetRoom.IsStandardRoom && !TargetRoom.IsGunslingKingChallengeRoom) {
                if (TargetRoom.area.runtimePrototypeData != null && TargetRoom.area.runtimePrototypeData.roomEvents != null) {
                    bool DarkRoom = false;
                    for (int i = 0; i < TargetRoom.area.runtimePrototypeData.roomEvents.Count; i++) {
                        if (TargetRoom.area.runtimePrototypeData.roomEvents[i].action == RoomEventTriggerAction.BECOME_TERRIFYING_AND_DARK) {
                            DarkRoom = true;
                        }
                    }
                    if (DarkRoom) { return; }
                }
                TargetRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All, ref EnemyList2);
                for (int j = 0; j < EnemyList2.Count; j++) {
                    AIActor TargetEnemy = EnemyList2[j];
                    if (TargetEnemy && (TargetEnemy.AdditionalSimpleItemDrops == null || TargetEnemy.AdditionalSimpleItemDrops.Count <= 0)) {
                        if (TargetEnemy.IsNormalEnemy && !TargetEnemy.IsHarmlessEnemy && TargetEnemy.IsWorthShootingAt && (!TargetEnemy.healthHaver || !TargetEnemy.healthHaver.IsBoss) && UnityEngine.Random.value < newPlayerEnemyCullFactor) {
                            UnityEngine.Object.Destroy(TargetEnemy.gameObject);
                        }
                    }
                }
            }
        }
        
        public static void ApplyCustomTexture(AIActor targetActor, Texture2D newTexture = null, List<Texture2D> spriteList = null, tk2dSpriteCollectionData prebuiltCollection = null, Shader overrideShader = null, bool disablePalette = false, bool makeStatic = false) {
            if (prebuiltCollection != null) {
                tk2dSpriteAnimation spriteAnimator = UnityEngine.Object.Instantiate(targetActor.spriteAnimator.Library);
                if (makeStatic) { UnityEngine.Object.DontDestroyOnLoad(targetActor.spriteAnimator.Library); }
                foreach (tk2dSpriteAnimationClip tk2dSpriteAnimationClip in spriteAnimator.clips) {
                    foreach (tk2dSpriteAnimationFrame frame in tk2dSpriteAnimationClip.frames) { frame.spriteCollection = prebuiltCollection; }
                }
                prebuiltCollection.name = targetActor.OverrideDisplayName;
                targetActor.sprite.Collection = prebuiltCollection;
                targetActor.spriteAnimator.Library = spriteAnimator;
                targetActor.sprite.SetSprite(prebuiltCollection, targetActor.sprite.spriteId);
                return;
            } 
            tk2dSpriteCollectionData collectionData = UnityEngine.Object.Instantiate(targetActor.sprite.Collection);
            if (makeStatic) { UnityEngine.Object.DontDestroyOnLoad(collectionData); }
            tk2dSpriteDefinition[] spriteDefinitions = new tk2dSpriteDefinition[collectionData.spriteDefinitions.Length];
            for (int i = 0; i < collectionData.spriteDefinitions.Length; i++) { spriteDefinitions[i] = collectionData.spriteDefinitions[i].Copy(); }
            collectionData.spriteDefinitions = spriteDefinitions;
            if (newTexture != null) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Using sprite sheet replacement on " + targetActor.GetActorName(), false); }
                Material[] materials = targetActor.sprite.Collection.materials;
                Material[] newMaterials = new Material[materials.Length];
                // collectionData.materials = new Material[materials.Length];

                if (materials != null) {
                    for (int i = 0; i < materials.Length; i++) {
                        newMaterials[i] = materials[i].Copy(newTexture);
                        if (overrideShader) { newMaterials[i].shader = overrideShader; }
                    }
                    collectionData.materials = newMaterials;

                    foreach (Material material2 in collectionData.materials) {
                        foreach (tk2dSpriteDefinition spriteDefinition in collectionData.spriteDefinitions) {
                            bool flag3 = material2 != null && spriteDefinition.material.name.Equals(material2.name);
                            if (flag3) {
                                spriteDefinition.material = material2;
                                spriteDefinition.materialInst = new Material(material2);
                                if (overrideShader) {
                                    spriteDefinition.material.shader = overrideShader;
                                    spriteDefinition.materialInst.shader = overrideShader;
                                }
                                // spriteDefinition.material = new Material(material2);
                                // spriteDefinition.materialInst = new Material(material2);
                            }
                        }
                    }
                }
                /*if (materials != null) {
                    for (int i = 0; i < collectionData.materials.Length; i++) {
                        Material material = new Material(materials[i]);
                        // UnityEngine.Object.DontDestroyOnLoad(material);
                        collectionData.materials[i].mainTexture = newTexture;
                        collectionData.materials[i].name = materials[i].name;
                        collectionData.materials[i] = material;
                    }
                }*/
                
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Step 3"); }
            } else if (spriteList != null) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Using individual sprite replacement on " + targetActor.GetActorName(), false); }

                RuntimeAtlasPage runtimeAtlasPage = new RuntimeAtlasPage(0, 0, TextureFormat.RGBA32, 2);
                for (int m = 0; m < spriteList.Count; m++) {
                    Texture2D texture2D = spriteList[m];
                    float num = texture2D.width / 16f;
                    float num2 = texture2D.height / 16f;
                    tk2dSpriteDefinition spriteData = collectionData.GetSpriteDefinition(texture2D.name);
                    bool flag6 = spriteData != null;
                    if (flag6) {
                        bool flag7 = spriteData.boundsDataCenter != Vector3.zero;
                        if (flag7) {
                            RuntimeAtlasSegment runtimeAtlasSegment = runtimeAtlasPage.Pack(texture2D, false);
                            spriteData.materialInst.mainTexture = runtimeAtlasSegment.texture;
                            spriteData.uvs = runtimeAtlasSegment.uvs;
                            spriteData.extractRegion = true;
                            spriteData.position0 = new Vector3(0f, 0f, 0f);
                            spriteData.position1 = new Vector3(num, 0f, 0f);
                            spriteData.position2 = new Vector3(0f, num2, 0f);
                            spriteData.position3 = new Vector3(num, num2, 0f);
                            spriteData.boundsDataCenter = new Vector2(num / 2f, num2 / 2f);
                            spriteData.untrimmedBoundsDataCenter = spriteData.boundsDataCenter;
                            spriteData.boundsDataExtents = new Vector2(num, num2);
                            spriteData.untrimmedBoundsDataExtents = spriteData.boundsDataExtents;
                        } else {
                            ETGMod.ReplaceTexture(spriteData, texture2D, true);
                        }
                    }
                }
                runtimeAtlasPage.Apply();
            } else {
                ETGModConsole.Log("Not replacing sprites on " + targetActor.GetActorName(), false);                
            }

            tk2dSpriteAnimation spriteAnimator2 = UnityEngine.Object.Instantiate(targetActor.spriteAnimator.Library);            
            if (makeStatic) { UnityEngine.Object.DontDestroyOnLoad(targetActor.spriteAnimator.Library); }
            foreach (tk2dSpriteAnimationClip tk2dSpriteAnimationClip in spriteAnimator2.clips) {
                foreach (tk2dSpriteAnimationFrame frame in tk2dSpriteAnimationClip.frames) { frame.spriteCollection = collectionData; }
            }
            collectionData.name = targetActor.OverrideDisplayName;
            targetActor.sprite.Collection = collectionData;
            targetActor.spriteAnimator.Library = spriteAnimator2;
        }

        public static void ApplyCustomTexture(GameObject targetObject, Texture2D newTexture = null, List<Texture2D> spriteList = null, tk2dSpriteCollectionData prebuiltCollection = null, Shader overrideShader = null, bool disablePalette = false) {
            tk2dSprite m_Sprite = targetObject.GetComponent<tk2dSprite>();
            tk2dSpriteAnimator m_SpriteAnimator = targetObject.GetComponent<tk2dSpriteAnimator>();

            if (!m_Sprite | ! m_SpriteAnimator) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: Target sprite component or sprite animator component is null on target object: '" + targetObject.name + "'!");
                return;
            }
            if (targetObject.GetComponent<tk2dSpriteAnimation>()) { UnityEngine.Object.Destroy(targetObject.GetComponent<tk2dSpriteAnimation>()); }

            tk2dSpriteAnimation m_SpriteAnimation = targetObject.AddComponent<tk2dSpriteAnimation>();
            List<tk2dSpriteAnimationClip> m_ClipList = new List<tk2dSpriteAnimationClip>();

            foreach (tk2dSpriteAnimationClip clip in m_SpriteAnimator.Library.clips) { m_ClipList.Add(DuplicateAnimationClip(clip)); }

            if (m_ClipList.Count <= 0) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: Target object: '" + targetObject.name + "' has no sprite animations!");
                return;
            }

            m_SpriteAnimation.clips = m_ClipList.ToArray();

            if (prebuiltCollection != null) {
                                
                foreach (tk2dSpriteAnimationClip clip in m_SpriteAnimation.clips) {
                    foreach (tk2dSpriteAnimationFrame frame in clip.frames) { frame.spriteCollection = prebuiltCollection; }
                }
                
                m_Sprite.sprite.Collection = prebuiltCollection;
                m_SpriteAnimator.Library = m_SpriteAnimation;
                return;
            }

            tk2dSpriteCollectionData m_CollectionData = DuplicateSpriteCollection(targetObject, m_Sprite.Collection);
            /*tk2dSpriteDefinition[] spriteDefinitions = new tk2dSpriteDefinition[m_CollectionData.spriteDefinitions.Length];
            for (int i = 0; i < m_CollectionData.spriteDefinitions.Length; i++) { spriteDefinitions[i] = m_CollectionData.spriteDefinitions[i].Copy(); }
            m_CollectionData.spriteDefinitions = spriteDefinitions;*/
           
            if (newTexture != null) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Using sprite sheet replacement on " + targetObject.name, false); }
                Material[] materials = m_CollectionData.materials;
                Material[] newMaterials = new Material[materials.Length];
                if (materials != null) {
                    for (int i = 0; i < materials.Length; i++) {
                        newMaterials[i] = materials[i].Copy(newTexture);
                        if (overrideShader) { newMaterials[i].shader = overrideShader; }
                    }
                    m_CollectionData.materials = newMaterials;
                    foreach (Material material2 in m_CollectionData.materials) {
                        foreach (tk2dSpriteDefinition spriteDefinition in m_CollectionData.spriteDefinitions) {
                            if (material2 != null && spriteDefinition.material.name.Equals(material2.name)) {
                                spriteDefinition.material = material2;
                                spriteDefinition.materialInst = new Material(material2);
                                if (overrideShader) {
                                    spriteDefinition.material.shader = overrideShader;
                                    spriteDefinition.materialInst.shader = overrideShader;
                                }
                            }
                        }
                    }
                }                
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Step 3"); }

                m_Sprite.Collection = m_CollectionData;
                m_Sprite.SetSprite(m_SpriteAnimator.DefaultClip.frames[0].spriteId);
                foreach (tk2dSpriteAnimationClip clip in m_SpriteAnimation.clips) {
                    foreach (tk2dSpriteAnimationFrame frame in clip.frames) { frame.spriteCollection = m_CollectionData; }
                }
                return;
            } else if (spriteList != null) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Using individual sprite replacement on " + targetObject.name); }
                RuntimeAtlasPage runtimeAtlasPage = new RuntimeAtlasPage(0, 0, TextureFormat.RGBA32, 2);
                foreach (Texture2D texture in spriteList) {
                    float Width = (texture.width / 16f);
                    float Height = (texture.height / 16f);
                    tk2dSpriteDefinition spriteData = m_CollectionData.GetSpriteDefinition(texture.name);
                    if (spriteData != null) {
                        if (spriteData.boundsDataCenter != Vector3.zero) {
                            RuntimeAtlasSegment runtimeAtlasSegment = runtimeAtlasPage.Pack(texture, false);
                            spriteData.materialInst.mainTexture = runtimeAtlasSegment.texture;
                            spriteData.uvs = runtimeAtlasSegment.uvs;
                            spriteData.extractRegion = true;
                            spriteData.position0 = Vector3.zero;
                            spriteData.position1 = new Vector3(Width, 0, 0);
                            spriteData.position2 = new Vector3(0, Height, 0);
                            spriteData.position3 = new Vector3(Width, Height, 0);
                            spriteData.boundsDataCenter = new Vector2((Width / 2f), (Height / 2f));
                            spriteData.untrimmedBoundsDataCenter = spriteData.boundsDataCenter;
                            spriteData.boundsDataExtents = new Vector2(Width, Height);
                            spriteData.untrimmedBoundsDataExtents = spriteData.boundsDataExtents;
                        } else {
                            ETGMod.ReplaceTexture(spriteData, texture, true);
                        }
                    }
                }
                runtimeAtlasPage.Apply();
                m_Sprite.Collection = m_CollectionData;
                m_Sprite.SetSprite(m_SpriteAnimator.DefaultClip.frames[0].spriteId);
                foreach (tk2dSpriteAnimationClip clip in m_SpriteAnimation.clips) {
                    foreach (tk2dSpriteAnimationFrame frame in clip.frames) { frame.spriteCollection = m_CollectionData; }
                }
                return;
            } else {
                ETGModConsole.Log("Not replacing sprites on " + targetObject.name);
                return;         
            }
        }

        public static tk2dSpriteCollectionData BuildSpriteCollection(tk2dSpriteCollectionData sourceCollection, Texture2D spriteSheet = null, List<Texture2D> spriteList = null, Shader overrideShader = null, bool IsStatic = false) {
            if (sourceCollection == null) { return null; }
            tk2dSpriteCollectionData collectionData = UnityEngine.Object.Instantiate(sourceCollection);
            if (IsStatic) { UnityEngine.Object.DontDestroyOnLoad(collectionData); }
            tk2dSpriteDefinition[] spriteDefinietions = new tk2dSpriteDefinition[collectionData.spriteDefinitions.Length];
            for (int i = 0; i < collectionData.spriteDefinitions.Length; i++) { spriteDefinietions[i] = collectionData.spriteDefinitions[i].Copy(); }
            collectionData.spriteDefinitions = spriteDefinietions;
            if (spriteSheet != null) {                
                Material[] materials = sourceCollection.materials;
                Material[] newMaterials = new Material[materials.Length];

                if (materials != null) {
                    for (int i = 0; i < materials.Length; i++) {
                        newMaterials[i] = materials[i].Copy(spriteSheet);
                        if (overrideShader) { newMaterials[i].shader = overrideShader; }
                    }
                    collectionData.materials = newMaterials;

                    foreach (Material material2 in collectionData.materials) {
                        foreach (tk2dSpriteDefinition spriteDefinition in collectionData.spriteDefinitions) {
                            bool flag3 = material2 != null && spriteDefinition.material.name.Equals(material2.name);
                            if (flag3) {
                                spriteDefinition.material = material2;
                                spriteDefinition.materialInst = new Material(material2);
                                if (overrideShader) {
                                    spriteDefinition.material.shader = overrideShader;
                                    spriteDefinition.materialInst.shader = overrideShader;
                                }
                            }
                        }
                    }
                }                
            } else if (spriteList != null) {
                RuntimeAtlasPage runtimeAtlasPage = new RuntimeAtlasPage(0, 0, TextureFormat.RGBA32, 2);
                for (int m = 0; m < spriteList.Count; m++) {
                    Texture2D texture2D = spriteList[m];
                    float num = texture2D.width / 16f;
                    float num2 = texture2D.height / 16f;
                    tk2dSpriteDefinition spriteData = collectionData.GetSpriteDefinition(texture2D.name);
                    bool flag6 = spriteData != null;
                    if (flag6) {
                        bool flag7 = spriteData.boundsDataCenter != Vector3.zero;
                        if (flag7) {
                            RuntimeAtlasSegment runtimeAtlasSegment = runtimeAtlasPage.Pack(texture2D, false);                            
                            spriteData.materialInst.mainTexture = runtimeAtlasSegment.texture;
                            if (overrideShader != null) { spriteData.materialInst.shader = overrideShader; }
                            spriteData.uvs = runtimeAtlasSegment.uvs;
                            spriteData.extractRegion = true;
                            spriteData.position0 = new Vector3(0f, 0f, 0f);
                            spriteData.position1 = new Vector3(num, 0f, 0f);
                            spriteData.position2 = new Vector3(0f, num2, 0f);
                            spriteData.position3 = new Vector3(num, num2, 0f);
                            spriteData.boundsDataCenter = new Vector2(num / 2f, num2 / 2f);
                            spriteData.untrimmedBoundsDataCenter = spriteData.boundsDataCenter;
                            spriteData.boundsDataExtents = new Vector2(num, num2);
                            spriteData.untrimmedBoundsDataExtents = spriteData.boundsDataExtents;
                        } else {
                            ETGMod.ReplaceTexture(spriteData, texture2D, true);
                        }
                    }
                }
                runtimeAtlasPage.Apply();
            }
            return collectionData;
        }
        
        public static tk2dSpriteCollectionData DuplicateSpriteCollection(GameObject targetObject, tk2dSpriteCollectionData sourceCollection, bool attachCollectionToObject = true, Texture2D spriteSheet = null, List<Texture2D>spriteList = null, Shader overrideShader = null) {
            if (sourceCollection == null) { return null; }

            tk2dSpriteCollectionData newCollection = null;

            if (attachCollectionToObject) {
                if (!targetObject.GetComponent<tk2dSpriteCollectionData>()) { targetObject.AddComponent<tk2dSpriteCollectionData>(); }
                newCollection = targetObject.GetComponent<tk2dSpriteCollectionData>();
            } else {
                newCollection = new tk2dSpriteCollectionData();
            }
            
            if (!newCollection) { return null; }

            newCollection.version = sourceCollection.version;
            newCollection.materialIdsValid = sourceCollection.materialIdsValid;
            newCollection.premultipliedAlpha = sourceCollection.premultipliedAlpha;
            newCollection.shouldGenerateTilemapReflectionData = sourceCollection.shouldGenerateTilemapReflectionData;
            newCollection.textureFilterMode = sourceCollection.textureFilterMode;
            newCollection.textureMipMaps = sourceCollection.textureMipMaps;
            newCollection.allowMultipleAtlases = sourceCollection.allowMultipleAtlases;
            newCollection.spriteCollectionGUID = Guid.NewGuid().ToString();
            newCollection.spriteCollectionName = (sourceCollection.spriteCollectionName + "(Modified)");
            newCollection.assetName = sourceCollection.assetName;
            newCollection.loadable = sourceCollection.loadable;
            newCollection.invOrthoSize = sourceCollection.invOrthoSize;
            newCollection.halfTargetHeight = sourceCollection.halfTargetHeight;
            newCollection.buildKey = sourceCollection.buildKey;
            newCollection.dataGuid = Guid.NewGuid().ToString();
            newCollection.managedSpriteCollection = sourceCollection.managedSpriteCollection;
            newCollection.hasPlatformData = sourceCollection.hasPlatformData;
            newCollection.spriteCollectionPlatforms = sourceCollection.spriteCollectionPlatforms;
            newCollection.SpriteDefinedBagelColliders = new List<BagelColliderData>();
            newCollection.SpriteIDsWithAttachPoints = new List<int>();
            newCollection.SpriteDefinedAttachPoints = new List<AttachPointData>();
            newCollection.SpriteIDsWithNeighborDependencies = new List<int>();
            newCollection.SpriteDefinedIndexNeighborDependencies = new List<NeighborDependencyData>();
            newCollection.SpriteIDsWithAnimationSequences = new List<int>();
            newCollection.SpriteDefinedAnimationSequences = new List<SimpleTilesetAnimationSequence>();
            
            if (sourceCollection.spriteCollectionPlatformGUIDs != null && sourceCollection.spriteCollectionPlatformGUIDs.Length > 0) {
                List<string> m_GUIDList = new List<string>();
                foreach (string GUID in sourceCollection.spriteCollectionPlatformGUIDs) { m_GUIDList.Add(Guid.NewGuid().ToString()); }
                if (m_GUIDList.Count > 0) { newCollection.spriteCollectionPlatformGUIDs = m_GUIDList.ToArray(); }
            }
            newCollection.SpriteIDsWithBagelColliders = new List<int>();
            if (sourceCollection.SpriteIDsWithBagelColliders.Count > 0) {
                foreach (int spriteID in sourceCollection.SpriteIDsWithBagelColliders) { newCollection.SpriteIDsWithBagelColliders.Add(spriteID); }
            }            
            if (sourceCollection.SpriteDefinedBagelColliders.Count > 0) {
                foreach (BagelColliderData colliderData in sourceCollection.SpriteDefinedBagelColliders) {
                    newCollection.SpriteDefinedBagelColliders.Add(
                        new BagelColliderData(colliderData.bagelColliders) { bagelColliders = colliderData.bagelColliders }
                    );
                }
            }
            if (sourceCollection.SpriteIDsWithAttachPoints.Count > 0) {
                foreach (int spriteID in sourceCollection.SpriteIDsWithAttachPoints) { newCollection.SpriteIDsWithAttachPoints.Add(spriteID); }
            }
            if (sourceCollection.SpriteDefinedAttachPoints.Count > 0) {
                foreach (AttachPointData attachPoint in sourceCollection.SpriteDefinedAttachPoints) {
                    newCollection.SpriteDefinedAttachPoints.Add(new AttachPointData(attachPoint.attachPoints) { attachPoints = attachPoint.attachPoints });
                }
            }
            if (sourceCollection.SpriteIDsWithNeighborDependencies.Count > 0) {
                foreach (int spriteID in sourceCollection.SpriteIDsWithNeighborDependencies) { newCollection.SpriteIDsWithNeighborDependencies.Add(spriteID); }
            }
            if (sourceCollection.SpriteDefinedIndexNeighborDependencies.Count > 0) {
                foreach (NeighborDependencyData dependencyData in sourceCollection.SpriteDefinedIndexNeighborDependencies) {
                    newCollection.SpriteDefinedIndexNeighborDependencies.Add(
                        new NeighborDependencyData(dependencyData.neighborDependencies) { neighborDependencies = dependencyData.neighborDependencies }
                    );
                }
            }
            if (sourceCollection.SpriteIDsWithAnimationSequences.Count > 0) {
                foreach (int spriteID in sourceCollection.SpriteIDsWithAnimationSequences) { newCollection.SpriteIDsWithAnimationSequences.Add(spriteID); }
            }
            if (sourceCollection.SpriteDefinedAnimationSequences.Count > 0) {
                foreach (SimpleTilesetAnimationSequence tilesetAnimation in sourceCollection.SpriteDefinedAnimationSequences) {
                    newCollection.SpriteDefinedAnimationSequences.Add(
                        new SimpleTilesetAnimationSequence() {
                            coreceptionMax = tilesetAnimation.coreceptionMax,
                            coreceptionMin = tilesetAnimation.coreceptionMin,
                            entries = tilesetAnimation.entries,
                            loopceptionMax = tilesetAnimation.loopceptionMax,
                            loopceptionMin = tilesetAnimation.loopceptionMin,
                            loopceptionTarget = tilesetAnimation.loopceptionTarget,
                            loopDelayMax = tilesetAnimation.loopDelayMax,
                            loopDelayMin = tilesetAnimation.loopDelayMin,
                            playstyle = tilesetAnimation.playstyle,
                            randomStartFrame = tilesetAnimation.randomStartFrame
                        }
                    );
                }
            }
            if (sourceCollection.materialPngTextureId.Length > 0) {
                List<int> m_materialIDs = new List<int>();
                foreach (int materialID in sourceCollection.materialPngTextureId) { m_materialIDs.Add(materialID); }
                if (m_materialIDs.Count > 0) {
                    newCollection.materialPngTextureId = m_materialIDs.ToArray();
                } else {
                    newCollection.materialPngTextureId = new int[0];
                }
            } else {
                newCollection.materialPngTextureId = new int[0];
            }
            if (sourceCollection.textures.Length > 0 && spriteSheet != null) {
                newCollection.textures = new Texture[] { spriteSheet };
            } else if (sourceCollection.textures.Length > 0) {
                List<Texture> m_Textures = new List<Texture>();
                foreach (Texture texture in sourceCollection.textures) { m_Textures.Add(texture); }
                if (m_Textures.Count > 0) {
                    newCollection.textures = m_Textures.ToArray();
                } else {
                    newCollection.textures = new Texture[0];
                }
            } else {
                newCollection.textures = new Texture[0];
            }
            if (sourceCollection.material) { newCollection.material = sourceCollection.material.Copy(spriteSheet, overrideShader); }
            if (sourceCollection.materials != null && sourceCollection.materials.Length > 0) {
                List<Material> m_Materials = new List<Material>();
                foreach (Material material in sourceCollection.materials) { m_Materials.Add(new Material(material)); }
                if (m_Materials.Count > 0) {
                    newCollection.materials = m_Materials.ToArray();
                } else {
                    newCollection.materials = new Material[0];
                }
            } else {
                sourceCollection.materials = new Material[0];
            }
            if (sourceCollection.spriteDefinitions.Length > 0) {
                List<tk2dSpriteDefinition> m_SpriteDefinitions = new List<tk2dSpriteDefinition>();
                // foreach (tk2dSpriteDefinition spriteDefinition in sourceCollection.spriteDefinitions) { m_SpriteDefinitions.Add(spriteDefinition.Copy()); }
                foreach (tk2dSpriteDefinition spriteDefinition in sourceCollection.spriteDefinitions) {
                    m_SpriteDefinitions.Add(DuplicateSpriteDefinition(spriteDefinition));
                }
                if (m_SpriteDefinitions.Count > 0) {
                    if (overrideShader) {
                        foreach (tk2dSpriteDefinition spriteDefinition in m_SpriteDefinitions) {
                            spriteDefinition.material.shader = overrideShader;
                            spriteDefinition.materialInst = new Material(spriteDefinition.material);
                        }
                    }
                    if (spriteSheet) {
                        foreach (tk2dSpriteDefinition spriteDefinition in m_SpriteDefinitions) {
                            // spriteDefinition.material.SetTexture("_MainTex", spriteSheet);
                            spriteDefinition.material.mainTexture = spriteSheet;
                            if (spriteDefinition.materialInst) { spriteDefinition.materialInst = new Material(spriteDefinition.material); }
                        }
                    }
                    newCollection.spriteDefinitions = m_SpriteDefinitions.ToArray();
                } else {
                    newCollection.spriteDefinitions = new tk2dSpriteDefinition[0];
                }
            } else {
                newCollection.spriteDefinitions = new tk2dSpriteDefinition[0];
            }
            if (spriteList != null) {
                RuntimeAtlasPage m_runtimeAtlasPage = new RuntimeAtlasPage(0, 0, TextureFormat.RGBA32, 2);
                foreach (Texture2D spriteTexture in spriteList) {
                    float width = (spriteTexture.width / 16);
                    float height = (spriteTexture.height / 16);
                    tk2dSpriteDefinition spriteData = newCollection.GetSpriteDefinition(spriteTexture.name);
                    if (spriteData != null) {
                        if (spriteData.boundsDataCenter != Vector3.zero) {
                            RuntimeAtlasSegment runtimeAtlasSegment = m_runtimeAtlasPage.Pack(spriteTexture, false);
                            spriteData.materialInst.mainTexture = runtimeAtlasSegment.texture;
                            if (overrideShader != null) { spriteData.materialInst.shader = overrideShader; }
                            spriteData.uvs = runtimeAtlasSegment.uvs;
                            spriteData.extractRegion = true;
                            spriteData.position0 = new Vector3(0f, 0f, 0f);
                            spriteData.position1 = new Vector3(width, 0f, 0f);
                            spriteData.position2 = new Vector3(0f, height, 0f);
                            spriteData.position3 = new Vector3(width, height, 0f);
                            spriteData.boundsDataCenter = new Vector2(width / 2f, width / 2f);
                            spriteData.untrimmedBoundsDataCenter = spriteData.boundsDataCenter;
                            spriteData.boundsDataExtents = new Vector2(width, height);
                            spriteData.untrimmedBoundsDataExtents = spriteData.boundsDataExtents;
                        } else {
                            ETGMod.ReplaceTexture(spriteData, spriteTexture, true);
                        }
                    }
                }
                m_runtimeAtlasPage.Apply();
            }
            newCollection.InitDictionary();
            // newCollection.InitMaterialIds();
            return newCollection;
        }

        public static void MakeCompanion(AIActor targetActor, AIActor sourceCompanion = null, PlayerController Owner = null, bool targetIsNewAIActor = false, bool ApplyCharmedColorOverride = true, bool blocksEnemyBullets = true, bool ImmuneToAllDamage = false) {

            if (sourceCompanion == null) { sourceCompanion = EnemyDatabase.GetOrLoadByGuid("3a077fa5872d462196bb9a3cb1af02a3"); }

            if (targetActor.EnemyGuid == "479556d05c7c44f3b6abb3b2067fc778") {
                targetActor.CanTargetPlayers = false;
                targetActor.CanTargetEnemies = true;
                targetActor.IgnoreForRoomClear = true;
                targetActor.HitByEnemyBullets = blocksEnemyBullets;
                if (ApplyCharmedColorOverride) { targetActor.RegisterOverrideColor(new Color(0.5f, 0, 0.5f), "Chaos Charm Effect"); }
                return;
            }

            if (!targetIsNewAIActor) { targetActor.behaviorSpeculator.MovementBehaviors.Add(sourceCompanion.behaviorSpeculator.MovementBehaviors[0]); }
            
            targetActor.CanTargetPlayers = false;
            targetActor.CanTargetEnemies = true;
            targetActor.IgnoreForRoomClear = true;
            targetActor.HitByEnemyBullets = blocksEnemyBullets;
            if (!targetIsNewAIActor) { targetActor.name = "CompanionPet"; }
            if (ApplyCharmedColorOverride) { targetActor.RegisterOverrideColor(new Color(0.5f, 0, 0.5f), "Chaos Charm Effect"); }

            targetActor.gameObject.AddComponent<CompanionController>();
            CompanionController companionController = targetActor.gameObject.GetComponent<CompanionController>();
            companionController.CanInterceptBullets = blocksEnemyBullets;
            companionController.IsCop = false;
            companionController.IsCopDead = false;
            companionController.CopDeathStatModifier = new StatModifier() {
                statToBoost = 0,
                modifyType = StatModifier.ModifyMethod.ADDITIVE,
                amount = 0
            };
            companionController.CurseOnCopDeath = 2;
            if (targetActor.IsFlying) {
                companionController.CanCrossPits = true;
            } else {
                companionController.CanCrossPits = false;
            }
            companionController.BlanksOnActiveItemUsed = false;
            companionController.InternalBlankCooldown = 10;
            companionController.HasStealthMode = false;
            companionController.PredictsChests = false;
            companionController.PredictsChestSynergy = 0;
            companionController.CanBePet = false;
            companionController.companionID = CompanionController.CompanionIdentifier.NONE;
            companionController.TeaSynergyHeatRing = new HeatRingModule();
            companionController.m_petOffset = new Vector2(0, 0);

            if (Owner) { companionController.Initialize(Owner); }

            if (targetActor.EnemyGuid == "d4dd2b2bbda64cc9bcec534b4e920518" | 
                targetActor.EnemyGuid == "98fdf153a4dd4d51bf0bafe43f3c77ff" | 
                targetActor.EnemyGuid == "be0683affb0e41bbb699cb7125fdded6" |
                targetActor.EnemyGuid == "c2f902b7cbe745efb3db4399927eab34" |
                targetActor.EnemyGuid == "249db525a9464e5282d02162c88e0357" |
                targetIsNewAIActor)
            {
                targetActor.OverrideHitEnemies = true;
                targetActor.CollisionDamage = 1f;
                targetActor.CollisionDamageTypes = CoreDamageTypes.Electric;
            }

            if (ImmuneToAllDamage) {
                targetActor.healthHaver.SetHealthMaximum(15000);
                targetActor.healthHaver.ForceSetCurrentHealth(15);
                targetActor.healthHaver.PreventAllDamage = true;
            }
        }

        // Spawns objects via DungeonPlacable system. Setup to spawn chests by default if no arguments are supplied.
        public static DungeonPlaceable GenerateDungeonPlacable(GameObject ObjectPrefab = null, bool spawnsEnemy = false, bool useExternalPrefab = false, bool spawnsItem = false, string EnemyGUID = "479556d05c7c44f3b6abb3b2067fc778", int itemID = 307, Vector2? CustomOffset = null, bool itemHasDebrisObject = true, float spawnChance = 1f) {
            AssetBundle m_assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
            AssetBundle m_assetBundle2 = ResourceManager.LoadAssetBundle("shared_auto_002");
            AssetBundle m_resourceBundle = ResourceManager.LoadAssetBundle("brave_resources_001");

            // Used with custom DungeonPlacable        
            GameObject ChestBrownTwoItems = m_assetBundle.LoadAsset<GameObject>("Chest_Wood_Two_Items");
            GameObject Chest_Silver = m_assetBundle.LoadAsset<GameObject>("chest_silver");
            GameObject Chest_Green = m_assetBundle.LoadAsset<GameObject>("chest_green");
            GameObject Chest_Synergy = m_assetBundle.LoadAsset<GameObject>("chest_synergy");
            GameObject Chest_Red = m_assetBundle.LoadAsset<GameObject>("chest_red");
            GameObject Chest_Black = m_assetBundle.LoadAsset<GameObject>("Chest_Black");
            GameObject Chest_Rainbow = m_assetBundle.LoadAsset<GameObject>("Chest_Rainbow");
            // GameObject Chest_Rat = m_assetBundle.LoadAsset<GameObject>("Chest_Rat");

            m_assetBundle = null;
            m_assetBundle2 = null;
            m_resourceBundle = null;

            DungeonPlaceableVariant BlueChestVariant = new DungeonPlaceableVariant();
            BlueChestVariant.percentChance = 0.35f;
            BlueChestVariant.unitOffset = new Vector2(1, 0.8f);
            BlueChestVariant.enemyPlaceableGuid = string.Empty;
            BlueChestVariant.pickupObjectPlaceableId = -1;
            BlueChestVariant.forceBlackPhantom = false;
            BlueChestVariant.addDebrisObject = false;
            BlueChestVariant.prerequisites = null;
            BlueChestVariant.materialRequirements = null;
            BlueChestVariant.nonDatabasePlaceable = Chest_Silver;

            DungeonPlaceableVariant BrownChestVariant = new DungeonPlaceableVariant();
            BrownChestVariant.percentChance = 0.28f;
            BrownChestVariant.unitOffset = new Vector2(1, 0.8f);
            BrownChestVariant.enemyPlaceableGuid = string.Empty;
            BrownChestVariant.pickupObjectPlaceableId = -1;
            BrownChestVariant.forceBlackPhantom = false;
            BrownChestVariant.addDebrisObject = false;
            BrownChestVariant.prerequisites = null;
            BrownChestVariant.materialRequirements = null;
            BrownChestVariant.nonDatabasePlaceable = ChestBrownTwoItems;

            DungeonPlaceableVariant GreenChestVariant = new DungeonPlaceableVariant();
            GreenChestVariant.percentChance = 0.25f;
            GreenChestVariant.unitOffset = new Vector2(1, 0.8f);
            GreenChestVariant.enemyPlaceableGuid = string.Empty;
            GreenChestVariant.pickupObjectPlaceableId = -1;
            GreenChestVariant.forceBlackPhantom = false;
            GreenChestVariant.addDebrisObject = false;
            GreenChestVariant.prerequisites = null;
            GreenChestVariant.materialRequirements = null;
            GreenChestVariant.nonDatabasePlaceable = Chest_Green;

            DungeonPlaceableVariant SynergyChestVariant = new DungeonPlaceableVariant();
            SynergyChestVariant.percentChance = 0.2f;
            SynergyChestVariant.unitOffset = new Vector2(1, 0.8f);
            SynergyChestVariant.enemyPlaceableGuid = string.Empty;
            SynergyChestVariant.pickupObjectPlaceableId = -1;
            SynergyChestVariant.forceBlackPhantom = false;
            SynergyChestVariant.addDebrisObject = false;
            SynergyChestVariant.prerequisites = null;
            SynergyChestVariant.materialRequirements = null;
            SynergyChestVariant.nonDatabasePlaceable = Chest_Synergy;

            DungeonPlaceableVariant RedChestVariant = new DungeonPlaceableVariant();
            RedChestVariant.percentChance = 0.15f;
            RedChestVariant.unitOffset = new Vector2(0.5f, 0.5f);
            RedChestVariant.enemyPlaceableGuid = string.Empty;
            RedChestVariant.pickupObjectPlaceableId = -1;
            RedChestVariant.forceBlackPhantom = false;
            RedChestVariant.addDebrisObject = false;
            RedChestVariant.prerequisites = null;
            RedChestVariant.materialRequirements = null;
            RedChestVariant.nonDatabasePlaceable = Chest_Red;

            DungeonPlaceableVariant BlackChestVariant = new DungeonPlaceableVariant();
            BlackChestVariant.percentChance = 0.1f;
            BlackChestVariant.unitOffset = new Vector2(0.5f, 0.5f);
            BlackChestVariant.enemyPlaceableGuid = string.Empty;
            BlackChestVariant.pickupObjectPlaceableId = -1;
            BlackChestVariant.forceBlackPhantom = false;
            BlackChestVariant.addDebrisObject = false;
            BlackChestVariant.prerequisites = null;
            BlackChestVariant.materialRequirements = null;
            BlackChestVariant.nonDatabasePlaceable = Chest_Black;

            DungeonPlaceableVariant RainbowChestVariant = new DungeonPlaceableVariant();
            RainbowChestVariant.percentChance = 0.005f;
            RainbowChestVariant.unitOffset = new Vector2(0.5f, 0.5f);
            RainbowChestVariant.enemyPlaceableGuid = string.Empty;
            RainbowChestVariant.pickupObjectPlaceableId = -1;
            RainbowChestVariant.forceBlackPhantom = false;
            RainbowChestVariant.addDebrisObject = false;
            RainbowChestVariant.prerequisites = null;
            RainbowChestVariant.materialRequirements = null;
            RainbowChestVariant.nonDatabasePlaceable = Chest_Rainbow;

            DungeonPlaceableVariant ItemVariant = new DungeonPlaceableVariant();
            ItemVariant.percentChance = spawnChance;
            if (CustomOffset.HasValue) {
                ItemVariant.unitOffset = CustomOffset.Value;
            } else {
                ItemVariant.unitOffset = Vector2.zero;
            }
            // ItemVariant.unitOffset = new Vector2(0.5f, 0.8f);
            ItemVariant.enemyPlaceableGuid = string.Empty;
            ItemVariant.pickupObjectPlaceableId = itemID;
            ItemVariant.forceBlackPhantom = false;
            if (itemHasDebrisObject) {
                ItemVariant.addDebrisObject = true;
            } else {
                ItemVariant.addDebrisObject = false;
            }            
            RainbowChestVariant.prerequisites = null;
            RainbowChestVariant.materialRequirements = null;

            List<DungeonPlaceableVariant> ChestTiers = new List<DungeonPlaceableVariant>();
            ChestTiers.Add(BrownChestVariant);
            ChestTiers.Add(BlueChestVariant);
            ChestTiers.Add(GreenChestVariant);
            ChestTiers.Add(SynergyChestVariant);
            ChestTiers.Add(RedChestVariant);
            ChestTiers.Add(BlackChestVariant);
            ChestTiers.Add(RainbowChestVariant);

            DungeonPlaceableVariant EnemyVariant = new DungeonPlaceableVariant();
            EnemyVariant.percentChance = spawnChance;
            EnemyVariant.unitOffset = Vector2.zero;
            EnemyVariant.enemyPlaceableGuid = EnemyGUID;
            EnemyVariant.pickupObjectPlaceableId = -1;
            EnemyVariant.forceBlackPhantom = false;
            EnemyVariant.addDebrisObject = false;
            EnemyVariant.prerequisites = null;
            EnemyVariant.materialRequirements = null;

            List<DungeonPlaceableVariant> EnemyTiers = new List<DungeonPlaceableVariant>();
            EnemyTiers.Add(EnemyVariant);

            List<DungeonPlaceableVariant> ItemTiers = new List<DungeonPlaceableVariant>();
            ItemTiers.Add(ItemVariant);

            DungeonPlaceable m_cachedCustomPlacable = ScriptableObject.CreateInstance<DungeonPlaceable>();
            m_cachedCustomPlacable.name = "CustomChestPlacable";
            if (spawnsEnemy | useExternalPrefab) {
                m_cachedCustomPlacable.width = 2;
                m_cachedCustomPlacable.height = 2;
            } else if (spawnsItem) {
                m_cachedCustomPlacable.width = 1;
                m_cachedCustomPlacable.height = 1;
            } else {
                m_cachedCustomPlacable.width = 4;
                m_cachedCustomPlacable.height = 1;
            }
            m_cachedCustomPlacable.roomSequential = false;
            m_cachedCustomPlacable.respectsEncounterableDifferentiator = true;
            m_cachedCustomPlacable.UsePrefabTransformOffset = false;
            m_cachedCustomPlacable.isPassable = true;
            if (spawnsItem) {
                m_cachedCustomPlacable.MarkSpawnedItemsAsRatIgnored = true;
            } else {
                m_cachedCustomPlacable.MarkSpawnedItemsAsRatIgnored = false;
            }
            
            m_cachedCustomPlacable.DebugThisPlaceable = false;
            if (useExternalPrefab && ObjectPrefab != null) {
                DungeonPlaceableVariant ExternalObjectVariant = new DungeonPlaceableVariant();
                ExternalObjectVariant.percentChance = spawnChance;
                if (CustomOffset.HasValue) {
                    ExternalObjectVariant.unitOffset = CustomOffset.Value;
                } else {
                    ExternalObjectVariant.unitOffset = Vector2.zero;
                }
                ExternalObjectVariant.enemyPlaceableGuid = string.Empty;
                ExternalObjectVariant.pickupObjectPlaceableId = -1;
                ExternalObjectVariant.forceBlackPhantom = false;
                ExternalObjectVariant.addDebrisObject = false;
                ExternalObjectVariant.nonDatabasePlaceable = ObjectPrefab;
                List<DungeonPlaceableVariant> ExternalObjectTiers = new List<DungeonPlaceableVariant>();
                ExternalObjectTiers.Add(ExternalObjectVariant);
                m_cachedCustomPlacable.variantTiers = ExternalObjectTiers;
            } else if (spawnsEnemy) {
                m_cachedCustomPlacable.variantTiers = EnemyTiers;
            } else if (spawnsItem) {
                m_cachedCustomPlacable.variantTiers = ItemTiers;
            } else {
                m_cachedCustomPlacable.variantTiers = ChestTiers;
            }
            return m_cachedCustomPlacable;
        }

        public static Chest GenerateChest(IntVector2 positionInRoom, RoomHandler targetRoom, PickupObject.ItemQuality? targetQuality = null, float overrideMimicChance = -1f, bool allowSynergyChest = true, bool deferConfiguration = true) {
            System.Random random = (!GameManager.Instance.IsSeeded) ? null : BraveRandom.GeneratorRandom;
            FloorRewardData rewardDataForFloor = GameManager.Instance.RewardManager.CurrentRewardData;
            bool forceDChanceZero = StaticReferenceManager.DChestsSpawnedInTotal >= 2;
            if (targetQuality == null) {
                targetQuality = new PickupObject.ItemQuality?(rewardDataForFloor.GetRandomTargetQuality(true, forceDChanceZero));
                if (PassiveItem.IsFlagSetAtAll(typeof(SevenLeafCloverItem))) {
                    targetQuality = new PickupObject.ItemQuality?((((random == null) ? UnityEngine.Random.value : ((float)random.NextDouble())) >= 0.5f) ? PickupObject.ItemQuality.S : PickupObject.ItemQuality.A);
                }
            }
            if (targetQuality.GetValueOrDefault() == PickupObject.ItemQuality.D && targetQuality != null && StaticReferenceManager.DChestsSpawnedOnFloor >= 1 && GameManager.Instance.Dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                targetQuality = new PickupObject.ItemQuality?(PickupObject.ItemQuality.C);
            }
            Vector2 zero = Vector2.zero;
            if ((targetQuality.GetValueOrDefault() == PickupObject.ItemQuality.A && targetQuality != null) || (targetQuality.GetValueOrDefault() == PickupObject.ItemQuality.S && targetQuality != null)) {
                zero = new Vector2(-0.5f, 0f);
            }
            Chest chest = GetTargetChestPrefab(GameManager.Instance.RewardManager, targetQuality.Value);
            if (GameStatsManager.Instance.GetFlag(GungeonFlags.SYNERGRACE_UNLOCKED) && GameManager.Instance.BestGenerationDungeonPrefab.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.CASTLEGEON && allowSynergyChest) {
                float num = (random == null) ? UnityEngine.Random.value : ((float)random.NextDouble());
                if (num < GameManager.Instance.RewardManager.GlobalSynerchestChance) {
                    chest = GameManager.Instance.RewardManager.Synergy_Chest;
                    zero = new Vector2(-0.1875f, 0f);
                }
            }
            Chest.GeneralChestType generalChestType = (BraveRandom.GenerationRandomValue() >= rewardDataForFloor.GunVersusItemPercentChance) ? Chest.GeneralChestType.ITEM : Chest.GeneralChestType.WEAPON;
            if (StaticReferenceManager.ItemChestsSpawnedOnFloor > 0 && StaticReferenceManager.WeaponChestsSpawnedOnFloor == 0) {
                generalChestType = Chest.GeneralChestType.WEAPON;
            } else if (StaticReferenceManager.WeaponChestsSpawnedOnFloor > 0 && StaticReferenceManager.ItemChestsSpawnedOnFloor == 0) {
                generalChestType = Chest.GeneralChestType.ITEM;
            }
            GenericLootTable genericLootTable = (generalChestType != Chest.GeneralChestType.WEAPON) ? GameManager.Instance.RewardManager.ItemsLootTable : GameManager.Instance.RewardManager.GunsLootTable;
            GameObject chestObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(chest.gameObject, targetRoom, positionInRoom, true, AIActor.AwakenAnimationType.Default, false);
            chestObject.transform.position = chestObject.transform.position + zero.ToVector3ZUp(0f);
            Chest chestComponent = chestObject.GetComponent<Chest>();
            Component[] componentsInChildren = chestObject.GetComponentsInChildren(typeof(IPlaceConfigurable));
            for (int i = 0; i < componentsInChildren.Length; i++) {
                IPlaceConfigurable placeConfigurable = componentsInChildren[i] as IPlaceConfigurable;
                if (placeConfigurable != null) { placeConfigurable.ConfigureOnPlacement(targetRoom); }
            }
            if (overrideMimicChance >= 0f) { chestComponent.overrideMimicChance = overrideMimicChance; }            
            if (targetQuality.GetValueOrDefault() == PickupObject.ItemQuality.A && targetQuality != null) {
                GameManager.Instance.Dungeon.GeneratedMagnificence += 1f;
                chestComponent.GeneratedMagnificence += 1f;
            } else if (targetQuality.GetValueOrDefault() == PickupObject.ItemQuality.S && targetQuality != null) {
                GameManager.Instance.Dungeon.GeneratedMagnificence += 1f;
                chestComponent.GeneratedMagnificence += 1f;
            }
            if (chestComponent.specRigidbody) { chestComponent.specRigidbody.Reinitialize(); }
            chestComponent.ChestType = generalChestType;
            chestComponent.lootTable.lootTable = genericLootTable;
            if (chestComponent.lootTable.canDropMultipleItems && chestComponent.lootTable.overrideItemLootTables != null && chestComponent.lootTable.overrideItemLootTables.Count > 0) {
                chestComponent.lootTable.overrideItemLootTables[0] = genericLootTable;
            }
            if (targetQuality.GetValueOrDefault() == PickupObject.ItemQuality.D && targetQuality != null && !chestComponent.IsMimic) {
                StaticReferenceManager.DChestsSpawnedOnFloor++;
                StaticReferenceManager.DChestsSpawnedInTotal++;
                chestComponent.IsLocked = true;
                if (chestComponent.LockAnimator) { chestComponent.LockAnimator.renderer.enabled = true; }
            }
            targetRoom.RegisterInteractable(chestComponent);
            if (GameManager.Instance.RewardManager.SeededRunManifests.ContainsKey(GameManager.Instance.BestGenerationDungeonPrefab.tileIndices.tilesetId)) {
                chestComponent.GenerationDetermineContents(GameManager.Instance.RewardManager.SeededRunManifests[GameManager.Instance.BestGenerationDungeonPrefab.tileIndices.tilesetId], random);
            }
            return chestComponent;
        }

        private static Chest GetTargetChestPrefab(RewardManager rewardManager, PickupObject.ItemQuality targetQuality) {
            Chest result = null;
            switch (targetQuality) {
                case PickupObject.ItemQuality.D:
                    result = rewardManager.D_Chest;
                    break;
                case PickupObject.ItemQuality.C:
                    result = rewardManager.C_Chest;
                    break;
                case PickupObject.ItemQuality.B:
                    result = rewardManager.B_Chest;
                    break;
                case PickupObject.ItemQuality.A:
                    result = rewardManager.A_Chest;
                    break;
                case PickupObject.ItemQuality.S:
                    result = rewardManager.S_Chest;
                    break;
            }
            return result;
        }

        public static void WallStamper(Dungeon dungeon, RoomHandler target, IntVector2 targetPosition, int length = 2, int height = 2, bool isVerticalWall = false, bool useBlockFillMethod = false, bool deferRebuild = true) {
            int X = targetPosition.X + target.area.basePosition.x;
            int Y = targetPosition.Y + target.area.basePosition.y;

            try {
                if (useBlockFillMethod) {
                    for (int i = 0; i < length; i++) {
                        for (int i2 = 0; i2 < height; i2++) {
                            dungeon.ConstructWallAtPosition(X + i, Y + i2, deferRebuild);
                        }
                    }
                } else {
                    for (int i = 0; i < length; i++) {
                        if (isVerticalWall) {
                            dungeon.ConstructWallAtPosition(X, Y + i, deferRebuild);
                        } else {
                            dungeon.ConstructWallAtPosition(X + i, Y, deferRebuild);
                            dungeon.ConstructWallAtPosition(X + i, Y + 1, deferRebuild);
                        }
                    }                
                }
            } catch (Exception ex) {
                ETGModConsole.Log("WARNING: Exception occured while generating wall cells!\nException details will be in log file.");
                Debug.Log("WARNING: Exception occured while generating wall cells!");
                Debug.LogException(ex);
            }            
        }

        public static void FloorStamper(RoomHandler target, IntVector2 targetPosition, int sizeX = 2, int sizeY = 2, CellType floorType = CellType.PIT) {
            int X = targetPosition.X + target.area.basePosition.x;
            int Y = targetPosition.Y + target.area.basePosition.y;

            for (int i = 0; i < sizeX; i++) {
                for (int i2 = 0; i2 < sizeY; i2++) {
                    target.RuntimeStampCellComplex(X + i, Y + i2, floorType, DiagonalWallType.NONE);
                }
            }
        }

        public static void GenerateFakeWall(DungeonData.Direction m_facingDirection, IntVector2 targetPosition, RoomHandler targetRoom, string wallName = "Fake Wall", bool markAsSecret = false, bool hasCollision = false, bool updateMinimapOnly = false, bool isGlitched = false) {
            if (targetRoom == null) { return; }

            IntVector2 pos1 = targetPosition + targetRoom.area.basePosition;
            IntVector2 pos2 = pos1 + IntVector2.Right;

            if (m_facingDirection == DungeonData.Direction.WEST) {
                pos1 = pos2;
            } else if (m_facingDirection == DungeonData.Direction.EAST) {
                pos2 = pos1;
            }

            if (!updateMinimapOnly) {
                try {
                    GameObject m_fakeWall = GenerateWallMesh(m_facingDirection, pos1, wallName, null, true, isGlitched);
                    m_fakeWall.transform.parent = targetRoom.hierarchyParent;
                    m_fakeWall.transform.position = pos1.ToVector3().WithZ(pos1.y - 2) + Vector3.down;
                    if (m_facingDirection == DungeonData.Direction.SOUTH) {
                        StaticReferenceManager.AllShadowSystemDepthHavers.Add(m_fakeWall.transform);
                    } else if (m_facingDirection == DungeonData.Direction.WEST) {
                        m_fakeWall.transform.position = m_fakeWall.transform.position + new Vector3(-0.1875f, 0f);
                    }
                    if (isGlitched && m_fakeWall.GetComponent<MeshRenderer>() != null) {
                        MeshRenderer meshRenderer = m_fakeWall.GetComponent<MeshRenderer>();
                        foreach (Material cellMaterial in meshRenderer.materials) {
                            float GlitchInterval = UnityEngine.Random.Range(0.038f, 0.042f);
                            float DispProbability = UnityEngine.Random.Range(0.066f, 0.074f);
                            float DispIntensity = UnityEngine.Random.Range(0.048f, 0.052f);
                            float ColorProbability = UnityEngine.Random.Range(0.069f, 0.071f);
                            float ColorIntensity = UnityEngine.Random.Range(0.0495f, 0.0605f);
                            ExpandShaders.ApplyGlitchMaterial(cellMaterial, GlitchInterval, DispProbability, DispIntensity, ColorProbability, 0.05f);
                        }
                    }
                } catch (Exception) { }

                try {
                    GameObject m_fakeCeiling = GenerateRoomCeilingMesh(GetCeilingTileSet(pos1, pos2, m_facingDirection), "Fake Ceiling", null, true, isGlitched);
                    m_fakeCeiling.transform.parent = targetRoom.hierarchyParent;
                    m_fakeCeiling.transform.position = pos1.ToVector3().WithZ(pos1.y - 4);
                    if (m_facingDirection == DungeonData.Direction.NORTH) {
                        m_fakeCeiling.transform.position += new Vector3(-1f, 0f);
                    } else if (m_facingDirection == DungeonData.Direction.SOUTH) {
                        m_fakeCeiling.transform.position += new Vector3(-1f, 2f);
                    } else if (m_facingDirection == DungeonData.Direction.EAST) {
                        m_fakeCeiling.transform.position += new Vector3(-1f, 0f);
                    }
                    m_fakeCeiling.transform.position = m_fakeCeiling.transform.position.WithZ(m_fakeCeiling.transform.position.y - 5f);
                    if (isGlitched && m_fakeCeiling.GetComponent<MeshRenderer>() != null) {
                        MeshRenderer meshRenderer = m_fakeCeiling.GetComponent<MeshRenderer>();
                        foreach (Material cellMaterial in meshRenderer.materials) {
                            float GlitchInterval = UnityEngine.Random.Range(0.038f, 0.042f);
                            float DispProbability = UnityEngine.Random.Range(0.066f, 0.074f);
                            float DispIntensity = UnityEngine.Random.Range(0.048f, 0.052f);
                            float ColorProbability = UnityEngine.Random.Range(0.069f, 0.071f);
                            float ColorIntensity = UnityEngine.Random.Range(0.0495f, 0.0605f);
                            ExpandShaders.ApplyGlitchMaterial(cellMaterial, GlitchInterval, DispProbability, DispIntensity, ColorProbability, 0.05f);
                        }
                    }
                } catch (Exception) { }
            }           

            if (markAsSecret | updateMinimapOnly) {
                CellData cellData = GameManager.Instance.Dungeon.data[pos1];
                CellData cellData2 = GameManager.Instance.Dungeon.data[pos2];
                cellData.isSecretRoomCell = true;
                cellData2.isSecretRoomCell = true;
                cellData.forceDisallowGoop = true;
                cellData2.forceDisallowGoop = true;
                cellData.cellVisualData.preventFloorStamping = true;
                cellData2.cellVisualData.preventFloorStamping = true;
                cellData.isWallMimicHideout = true;
                cellData2.isWallMimicHideout = true;
                if (m_facingDirection == DungeonData.Direction.WEST || m_facingDirection == DungeonData.Direction.EAST) {
                    GameManager.Instance.Dungeon.data[pos1 + IntVector2.Up].isSecretRoomCell = true;
                }
            }
        }        

        public static void DestroyWallAtPosition(Dungeon dungeon, RoomHandler targetRoom, IntVector2 position, bool physicsOnly = false, bool deferRebuild = true) {
            int ix = position.x + targetRoom.area.basePosition.x;
            int iy = position.y + targetRoom.area.basePosition.y;

            TK2DDungeonAssembler assembler = new TK2DDungeonAssembler();
            assembler.Initialize(dungeon.tileIndices);
            tk2dTileMap m_tilemap = dungeon.MainTilemap;

            if (dungeon.data.cellData[ix][iy] == null) { return; }
            if (dungeon.data.cellData[ix][iy].type != CellType.WALL) { return; }
            // if (!dungeon.data.cellData[ix][iy].breakable) { return null; }

            dungeon.data.cellData[ix][iy].type = CellType.FLOOR;
            if (dungeon.data.isSingleCellWall(ix, iy + 1)) { dungeon.data[ix, iy + 1].type = CellType.FLOOR; }
            if (dungeon.data.isSingleCellWall(ix, iy - 1)) { dungeon.data[ix, iy - 1].type = CellType.FLOOR; }
            RoomHandler parentRoom = dungeon.data.cellData[ix][iy].parentRoom;
            tk2dTileMap tk2dTileMap = (parentRoom == null || !(parentRoom.OverrideTilemap != null)) ? m_tilemap : parentRoom.OverrideTilemap;
            for (int i = -1; i < 2; i++) {
                for (int j = -2; j < 4; j++) {
                    CellData cellData = dungeon.data.cellData[ix + i][iy + j];
                    if (cellData != null) {
                        cellData.hasBeenGenerated = false;
                        if (!physicsOnly) {
                            if (cellData.parentRoom != null) {
                                List<GameObject> list = new List<GameObject>();
                                for (int k = 0; k < cellData.parentRoom.hierarchyParent.childCount; k++) {
                                    Transform child = cellData.parentRoom.hierarchyParent.GetChild(k);
                                    if (child.name.StartsWith("Chunk_")) { list.Add(child.gameObject); }
                                }
                                for (int l = list.Count - 1; l >= 0; l--) { UnityEngine.Object.Destroy(list[l]); }
                            }
                        }
                        try {
                            assembler.ClearTileIndicesForCell(dungeon, tk2dTileMap, cellData.position.x, cellData.position.y);
                            assembler.BuildTileIndicesForCell(dungeon, tk2dTileMap, cellData.position.x, cellData.position.y);
                        } catch (Exception ex) {
                            if (ExpandSettings.debugMode) {
                                ETGModConsole.Log("[DEBUG] Warning: Exception caught in TK2DDungeonAssembler.ClearTileIndicesForCell and/or TK2DDungeonAssembler.BuildTileIndicesForCell!");
                                Debug.Log("Warning: Exception caught in TK2DDungeonAssembler.ClearTileIndicesForCell and/or TK2DDungeonAssembler.BuildTileIndicesForCell!");
                                Debug.LogException(ex);
                            }
                        }                        
                        cellData.HasCachedPhysicsTile = false;
                        cellData.CachedPhysicsTile = null;
                    }
                }
            }
            if (!deferRebuild) { dungeon.RebuildTilemap(tk2dTileMap); }
            return;
        }

        public static void AddHealthHaver(GameObject target, float maxHealth = 25, bool explodesOnDeath = true, OnDeathBehavior.DeathType explosionDeathType = OnDeathBehavior.DeathType.Death, bool flashesOnDamage = false, bool exploderSpawnsItem = false) {
            if (target.GetComponent<HealthHaver>() != null | target.GetComponentInChildren<HealthHaver>(true) != null |
                target.GetComponent<PlayerController>() != null | target.GetComponentInChildren<PlayerController>(true) != null |
                target.GetComponentInChildren<SpeculativeRigidbody>() == null)
            { return; }

            /*if (target.GetComponentInChildren<TalkDoerLite>() != null) {
                TalkDoerLite npcComponent = target.GetComponentInChildren<TalkDoerLite>();
                UnityEngine.Object.Destroy(npcComponent.ultraFortunesFavor);
            }*/

            target.AddComponent<HealthHaver>();
            HealthHaver m_healthHaver = target.GetComponent<HealthHaver>();
            // FieldInfo field = typeof(HealthHaver).GetField("usesInvulnerabilityPeriod", BindingFlags.Instance | BindingFlags.NonPublic);
            // FieldInfo field2 = typeof(HealthHaver).GetField("m_isFlashing", BindingFlags.Instance | BindingFlags.NonPublic);
            // isPlayerCharacter
            FieldInfo field = typeof(HealthHaver).GetField("isPlayerCharacter", BindingFlags.Instance | BindingFlags.NonPublic);
            field.SetValue(m_healthHaver, false);
            m_healthHaver.quantizeHealth = false;
            m_healthHaver.quantizedIncrement = 0.5f;
            m_healthHaver.flashesOnDamage = flashesOnDamage;
            m_healthHaver.incorporealityOnDamage = false;
            m_healthHaver.incorporealityTime = 0;
            m_healthHaver.PreventAllDamage = false;
            m_healthHaver.persistsOnDeath = false;
            // field.SetValue(m_healthHaver, false);
            // field2.SetValue(m_healthHaver, false);
            m_healthHaver.SetHealthMaximum(maxHealth);
            m_healthHaver.Armor = 0;
            m_healthHaver.CursedMaximum = maxHealth * 3;            
            m_healthHaver.useFortunesFavorInvulnerability = false;
            m_healthHaver.damagedAudioEvent = string.Empty;
            m_healthHaver.overrideDeathAudioEvent = string.Empty;
            m_healthHaver.overrideDeathAnimation = string.Empty;
            m_healthHaver.shakesCameraOnDamage = false;
            m_healthHaver.cameraShakeOnDamage = new ScreenShakeSettings() {
                magnitude = 0.35f,
                speed = 6,
                time = 0.06f,
                falloff = 0,
                direction = Vector2.zero,
                vibrationType = ScreenShakeSettings.VibrationType.Auto,
                simpleVibrationTime = Vibration.Time.Normal,
                simpleVibrationStrength = Vibration.Strength.Medium
            };
            m_healthHaver.damageTypeModifiers = new List<DamageTypeModifier>(0);
            m_healthHaver.healthIsNumberOfHits = false;
            m_healthHaver.OnlyAllowSpecialBossDamage = false;
            m_healthHaver.overrideDeathAnimBulletScript = string.Empty;
            m_healthHaver.noCorpseWhenBulletScriptDeath = false;
            m_healthHaver.spawnBulletScript = false;
            m_healthHaver.chanceToSpawnBulletScript = 0;
            m_healthHaver.bulletScriptType = HealthHaver.BulletScriptType.OnPreDeath;
            m_healthHaver.bulletScript = new BulletScriptSelector() { scriptTypeName = string.Empty };
            m_healthHaver.bossHealthBar = HealthHaver.BossBarType.None;
            m_healthHaver.overrideBossName = string.Empty;
            m_healthHaver.forcePreventVictoryMusic = false;
            m_healthHaver.GlobalPixelColliderDamageMultiplier = 1;
            m_healthHaver.IsVulnerable = true;
           
            if (explodesOnDeath) {
                m_healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                ExpandExplodeOnDeath corpseExploder = m_healthHaver.gameObject.GetComponent<ExpandExplodeOnDeath>();
                corpseExploder.deathType = explosionDeathType;
                if (exploderSpawnsItem) { corpseExploder.spawnItemsOnExplosion = true; }
            }
        }

        public static void GenerateHealthHaver(GameObject target, float maxHealth = 25, bool disableAnimator = true, bool explodesOnDeath = true, OnDeathBehavior.DeathType explosionDeathType = OnDeathBehavior.DeathType.Death, bool flashesOnDamage = true, bool exploderSpawnsItem = false, bool isCorruptedObject = true, bool isRatNPC = false, bool skipAnimatorCheck = false, bool buildLists = false) {
            if (target.GetComponent<HealthHaver>() != null | 
                target.GetComponentInChildren<HealthHaver>(true) != null |
                target.GetComponent<tk2dBaseSprite>() == null |
                target.GetComponentInChildren<SpeculativeRigidbody>() == null
            ) {
                return;
            }

            if (!isRatNPC && !skipAnimatorCheck) {
                // A spriteAnimator is required for a HealthHaver to work properly!
                tk2dBaseSprite baseSprite = target.GetComponent<tk2dBaseSprite>();

                if (disableAnimator) {
                    if (target.GetComponent<AIAnimator>()) { UnityEngine.Object.Destroy(target.GetComponent<AIAnimator>()); }
                    if (target.GetComponent<tk2dSpriteAnimator>()) { UnityEngine.Object.Destroy(target.GetComponent<tk2dSpriteAnimator>()); }
                    target.AddComponent<tk2dSpriteAnimator>();
                    tk2dSpriteAnimator shrineDummyAnimator = target.GetComponent<tk2dSpriteAnimator>();
                    shrineDummyAnimator.Library = null;
                    shrineDummyAnimator.DefaultClipId = 0;
                    shrineDummyAnimator.AdditionalCameraVisibilityRadius = 0;
                    shrineDummyAnimator.AnimateDuringBossIntros = false;
                    shrineDummyAnimator.AlwaysIgnoreTimeScale = true;
                    shrineDummyAnimator.ForceSetEveryFrame = false;
                    shrineDummyAnimator.playAutomatically = false;
                    shrineDummyAnimator.IsFrameBlendedAnimation = false;
                    shrineDummyAnimator.clipTime = 0;
                    shrineDummyAnimator.deferNextStartClip = false;
                    SpriteBuilder.AddAnimation(shrineDummyAnimator, baseSprite.Collection, new List<int>() { baseSprite.spriteId }, "DummyFrame", tk2dSpriteAnimationClip.WrapMode.Once);
                } else if (!target.GetComponent<tk2dSpriteAnimator>() && baseSprite.Collection != null) {
                    target.AddComponent<tk2dSpriteAnimator>();
                    tk2dSpriteAnimator shrineDummyAnimator = target.GetComponent<tk2dSpriteAnimator>();
                    shrineDummyAnimator.Library = null;
                    shrineDummyAnimator.DefaultClipId = 0;
                    shrineDummyAnimator.AdditionalCameraVisibilityRadius = 0;
                    shrineDummyAnimator.AnimateDuringBossIntros = false;
                    shrineDummyAnimator.AlwaysIgnoreTimeScale = true;
                    shrineDummyAnimator.ForceSetEveryFrame = false;
                    shrineDummyAnimator.playAutomatically = false;
                    shrineDummyAnimator.IsFrameBlendedAnimation = false;
                    shrineDummyAnimator.clipTime = 0;
                    shrineDummyAnimator.deferNextStartClip = false;
                    SpriteBuilder.AddAnimation(shrineDummyAnimator, baseSprite.Collection, new List<int>() { baseSprite.spriteId }, "DummyFrame", tk2dSpriteAnimationClip.WrapMode.Once);
                } else if (target.GetComponent<tk2dSpriteAnimator>() && !disableAnimator) {
                    // We need to set all frames in the animator to vulnerable else it may not dake damage from projectiles correctly!
                    tk2dSpriteAnimator existingAnimator = target.GetComponent<tk2dSpriteAnimator>();
                    if (existingAnimator.Library != null && existingAnimator.Library.clips != null && existingAnimator.Library.clips.Length > 0) {
                        tk2dSpriteAnimationClip[] existingClips = existingAnimator.Library.clips;
                        foreach (tk2dSpriteAnimationClip clip in existingClips) {
                            if (clip.frames != null && clip.frames.Length > 0) {
                                foreach (tk2dSpriteAnimationFrame Frame in clip.frames) {
                                    Frame.invulnerableFrame = false;
                                }
                            }
                        }
                    } else {
                        return;
                    }
                } else if (!target.GetComponent<tk2dSpriteAnimator>() && !disableAnimator) {
                    return;
                }
            }
                

            target.AddComponent<HealthHaver>();
            HealthHaver m_healthHaver = target.GetComponent<HealthHaver>();
            FieldInfo field = typeof(HealthHaver).GetField("isPlayerCharacter", BindingFlags.Instance | BindingFlags.NonPublic);
            field.SetValue(m_healthHaver, false);
            m_healthHaver.IsVulnerable = true;
            m_healthHaver.quantizeHealth = false;
            m_healthHaver.quantizedIncrement = 0.5f;
            m_healthHaver.flashesOnDamage = flashesOnDamage;
            m_healthHaver.incorporealityOnDamage = false;
            m_healthHaver.incorporealityTime = 0;
            m_healthHaver.PreventAllDamage = false;
            m_healthHaver.persistsOnDeath = false;
            m_healthHaver.SetHealthMaximum(maxHealth);
            m_healthHaver.Armor = 0;
            m_healthHaver.CursedMaximum = maxHealth * 3;
            m_healthHaver.useFortunesFavorInvulnerability = false;
            m_healthHaver.damagedAudioEvent = string.Empty;
            m_healthHaver.overrideDeathAudioEvent = string.Empty;
            m_healthHaver.overrideDeathAnimation = string.Empty;
            m_healthHaver.shakesCameraOnDamage = false;
            m_healthHaver.cameraShakeOnDamage = new ScreenShakeSettings() {
                magnitude = 0.35f,
                speed = 6,
                time = 0.06f,
                falloff = 0,
                direction = Vector2.zero,
                vibrationType = ScreenShakeSettings.VibrationType.Auto,
                simpleVibrationTime = Vibration.Time.Normal,
                simpleVibrationStrength = Vibration.Strength.Medium
            };
            m_healthHaver.damageTypeModifiers = new List<DamageTypeModifier>(0);
            m_healthHaver.healthIsNumberOfHits = false;
            m_healthHaver.OnlyAllowSpecialBossDamage = false;
            m_healthHaver.overrideDeathAnimBulletScript = string.Empty;
            m_healthHaver.noCorpseWhenBulletScriptDeath = false;
            m_healthHaver.spawnBulletScript = false;
            m_healthHaver.chanceToSpawnBulletScript = 0;
            m_healthHaver.bulletScriptType = HealthHaver.BulletScriptType.OnPreDeath;
            m_healthHaver.bulletScript = new BulletScriptSelector() { scriptTypeName = string.Empty };
            m_healthHaver.bossHealthBar = HealthHaver.BossBarType.None;
            m_healthHaver.overrideBossName = string.Empty;
            m_healthHaver.forcePreventVictoryMusic = false;
            m_healthHaver.GlobalPixelColliderDamageMultiplier = 1;
           
            if (explodesOnDeath) {
                m_healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                ExpandExplodeOnDeath corpseExploder = m_healthHaver.gameObject.GetComponent<ExpandExplodeOnDeath>();
                corpseExploder.deathType = explosionDeathType;
                corpseExploder.spawnItemsOnExplosion = exploderSpawnsItem;
                corpseExploder.isCorruptedObject = isCorruptedObject;
            }

            if (buildLists) {
                m_healthHaver.DamageableColliders = new List<PixelCollider>();

                if (target.GetComponentsInChildren<SpeculativeRigidbody>(true) != null && target.GetComponentsInChildren<SpeculativeRigidbody>(true).Length > 0) {
                    m_healthHaver.bodyRigidbodies = new List<SpeculativeRigidbody>();
                    foreach (SpeculativeRigidbody rigidBody in target.GetComponentsInChildren<SpeculativeRigidbody>(true)) {
                        m_healthHaver.bodyRigidbodies.Add(rigidBody);
                        if (rigidBody.PixelColliders != null && rigidBody.PixelColliders.Count > 0) {
                            foreach (PixelCollider collider in rigidBody.PixelColliders) { m_healthHaver.DamageableColliders.Add(collider); }
                        }
                    }
                } else {
                    m_healthHaver.bodyRigidbodies = new List<SpeculativeRigidbody>() { target.GetComponent<SpeculativeRigidbody>() };
                    if (target.GetComponent<SpeculativeRigidbody>().PixelColliders != null && target.GetComponent<SpeculativeRigidbody>().PixelColliders.Count > 0) {
                        foreach (PixelCollider collider in target.GetComponent<SpeculativeRigidbody>().PixelColliders) { m_healthHaver.DamageableColliders.Add(collider); }
                    };
                }

                if (target.GetComponentsInChildren<tk2dBaseSprite>(true) != null) {
                    m_healthHaver.bodySprites = new List<tk2dBaseSprite>();
                    tk2dBaseSprite[] BaseSprites = target.GetComponentsInChildren<tk2dBaseSprite>(true);
                    foreach (tk2dBaseSprite BaseSprite in BaseSprites) { m_healthHaver.bodySprites.Add(BaseSprite); }
                }
            }

            if (isCorruptedObject) {
                if (string.IsNullOrEmpty(target.name)) {
                    target.name = "Corrupted Object";
                } else if (!target.name.StartsWith("Corrupted")){
                    target.name = ("Corrupted " + target.name);
                }
            }
            try { m_healthHaver.RegenerateCache(); } catch (Exception) { }
        }

        public static ExplosionData GenerateExplosionData(GameObject DefaultVFX = null, GameObject overrideRangeIndicatorEffect = null, List<SpeculativeRigidbody> ignoreList = null, GameActorFreezeEffect freezeEffect = null, bool useDefaultExplosion = false, bool doDamage = true, bool forceUseThisRadius = true, float damageRadius = 3, bool DamagesPlayer = true, float damage = 40, bool breakSecretWalls = false, float secretWallsRadius = 4.5f, bool forcePreventSecretWallDamage = false, bool doDestroyProjectiles = true, bool doForce = true, float pushRadius = 5, float force = 100, float debrisForce = 50, bool preventPlayerForce = false, float explosionDelay = 0.1f, bool usesComprehensiveDelay = false, float comprehensiveDelay = 0, bool doScreenShake = true, bool doStickyFriction = true, bool doExplosionRing = true, bool isFreezeExplosion = false, float freezeRadius = 5, bool playDefaultSFX = true, bool IsChandelierExplosion = false, bool rotateEffectToNormal = false) {
            
            if (!DefaultVFX) {
                DefaultVFX = ExpandAssets.LoadOfficialAsset<GameObject>("VFX_Ring_Explosion_001", ExpandAssets.AssetSource.SharedAuto1);
            }

            if (!DefaultVFX) { return null; }
            if (isFreezeExplosion && freezeEffect == null) { isFreezeExplosion = false; }

            if (ignoreList == null) { ignoreList = new List<SpeculativeRigidbody>(0); }

            float m_DamageToPlayer = 0.5f;
            if (!DamagesPlayer) { m_DamageToPlayer = 0; }

            ExplosionData m_CachedExplosionData = new ExplosionData() {
                useDefaultExplosion = useDefaultExplosion,
                doDamage = doDamage,
                forceUseThisRadius = forceUseThisRadius,
                damageRadius = damageRadius,
                damageToPlayer = m_DamageToPlayer,
                damage = damage,
                breakSecretWalls = breakSecretWalls,
                secretWallsRadius = secretWallsRadius,
                forcePreventSecretWallDamage = forcePreventSecretWallDamage,
                doDestroyProjectiles = doDestroyProjectiles,
                doForce = doForce,
                pushRadius = pushRadius,
                force = force,
                debrisForce = debrisForce,
                preventPlayerForce = preventPlayerForce,
                explosionDelay = explosionDelay,
                usesComprehensiveDelay = usesComprehensiveDelay,
                comprehensiveDelay = comprehensiveDelay,
                effect = DefaultVFX,
                doScreenShake = doScreenShake,
                ss = new ScreenShakeSettings() {
                    magnitude = 2,
                    speed = 7,
                    time = 0.1f,
                    falloff = 0.3f,
                    direction = Vector2.zero,
                    vibrationType = ScreenShakeSettings.VibrationType.Auto,
                    simpleVibrationTime = Vibration.Time.Normal,
                    simpleVibrationStrength = Vibration.Strength.Medium
                },
                doStickyFriction = doStickyFriction,
                doExplosionRing = doExplosionRing,
                isFreezeExplosion = isFreezeExplosion,
                freezeRadius = freezeRadius,
                freezeEffect = freezeEffect,
                playDefaultSFX = playDefaultSFX,
                IsChandelierExplosion = IsChandelierExplosion,
                rotateEffectToNormal = rotateEffectToNormal,
                ignoreList = ignoreList,
                overrideRangeIndicatorEffect = overrideRangeIndicatorEffect
            };

            if (m_CachedExplosionData == null) { return null; }

            return m_CachedExplosionData;
        }
        
        public static IEnumerator DelayedGlitchLevelLoad(float delay, string flowPath, bool useSpaceTileset = false) {
            if (string.IsNullOrEmpty(flowPath)) { yield break; }            
            string flow = flowPath;
            ExpandDungeonFlow.isGlitchFlow = true;
            if (BraveUtility.RandomBool()) { flow = "custom_glitch_flow"; }
            yield return new WaitForSeconds(delay);
            if (useSpaceTileset) {
                GameManager.Instance.LoadCustomFlowForDebug(flow, "Base_Space", "tt_space");
            } else {
                GameManager.Instance.LoadCustomFlowForDebug(flow, "Base_Phobos", "tt_phobos");
            }
            yield break;
        }

        public static GameObject GenerateRoomCeilingMesh(HashSet<IntVector2> cells, string objectName = "secret room ceiling object", DungeonData dungeonData = null, bool mimicCheck = false, bool isGlitched = false, tk2dSpriteCollectionData overrideDungeonCollection = null, Dungeon dungeonOverride = null) {
            if (dungeonData == null) { dungeonData = GameManager.Instance.Dungeon.data; }
            Mesh mesh = new Mesh();
            List<Vector3> list = new List<Vector3>();
            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();
            List<Vector2> list4 = new List<Vector2>();
            Material material = null;
            Material material2 = null;
            tk2dSpriteCollectionData dungeonCollection = overrideDungeonCollection;
            if (!dungeonCollection) { dungeonCollection = GameManager.Instance.Dungeon.tileIndices.dungeonCollection; }
            if (isGlitched) {
                dungeonCollection = UnityEngine.Object.Instantiate(dungeonCollection);
                foreach (tk2dSpriteDefinition spriteInfo in dungeonCollection.spriteDefinitions) {
                    ExpandShaders.ApplyGlitchShaderUnlit(spriteInfo, UnityEngine.Random.Range(0.038f, 0.042f), UnityEngine.Random.Range(0.073f, 0.067f), UnityEngine.Random.Range(0.052f, 0.048f), UnityEngine.Random.Range(0.073f, 0.67f), UnityEngine.Random.Range(0.052f, 0.048f));
                }
            }
            Vector3 b = new Vector3(0f, 0f, -3.01f);
            Vector3 b2 = new Vector3(0f, 0f, -3.02f);
            foreach (IntVector2 position in cells) {
                TileIndexGrid borderGridForCellPosition = GetBorderGridForCellPosition(position, dungeonData, dungeonOverride);
                int indexByWeight = borderGridForCellPosition.centerIndices.GetIndexByWeight();
                int tileFromRawTile = BuilderUtil.GetTileFromRawTile(indexByWeight);
                tk2dSpriteDefinition tk2dSpriteDefinition = dungeonCollection.spriteDefinitions[tileFromRawTile];
                if (material == null) { material = tk2dSpriteDefinition.material; }               
                int count = list.Count;
                Vector3 a = position.ToVector3(position.y);
                Vector3[] array = tk2dSpriteDefinition.ConstructExpensivePositions();
                for (int i = 0; i < array.Length; i++) {
                    Vector3 b3 = array[i].WithZ(array[i].y);
                    list.Add(a + b3 + b);
                    list4.Add(tk2dSpriteDefinition.uvs[i]);
                }
                for (int j = 0; j < tk2dSpriteDefinition.indices.Length; j++) { list2.Add(count + tk2dSpriteDefinition.indices[j]); }
                int x = position.x;
                int y = position.y;
                bool flag = IsTopWall(x, y, dungeonData, cells);
                bool flag2 = IsTopWall(x + 1, y, dungeonData, cells) && !IsTopWall(x, y, dungeonData, cells) && (IsWall(x, y + 1, dungeonData, cells) || IsTopWall(x, y + 1, dungeonData, cells));
                bool flag3 = (!IsWall(x + 1, y, dungeonData, cells) && !IsTopWall(x + 1, y, dungeonData, cells)) || IsFaceWallHigher(x + 1, y, dungeonData, cells);
                bool flag4 = y > 3 && IsFaceWallHigher(x + 1, y - 1, dungeonData, cells) && !IsFaceWallHigher(x, y - 1, dungeonData, cells);
                bool flag5 = y > 3 && IsFaceWallHigher(x, y - 1, dungeonData, cells);
                bool flag6 = y > 3 && IsFaceWallHigher(x - 1, y - 1, dungeonData, cells) && !IsFaceWallHigher(x, y - 1, dungeonData, cells);
                bool flag7 = (!IsWall(x - 1, y, dungeonData, cells) && !IsTopWall(x - 1, y, dungeonData, cells)) || IsFaceWallHigher(x - 1, y, dungeonData, cells);
                bool flag8 = IsTopWall(x - 1, y, dungeonData, cells) && !IsTopWall(x, y, dungeonData, cells) && (IsWall(x, y + 1, dungeonData, cells) || IsTopWall(x, y + 1, dungeonData, cells));
                if (mimicCheck) {
                    flag = IsTopWallOrSecret(x, y, dungeonData, cells);
                    flag2 = (IsTopWallOrSecret(x + 1, y, dungeonData, cells) && !IsTopWallOrSecret(x, y, dungeonData, cells) && (IsWallOrSecret(x, y + 1, dungeonData, cells) || IsTopWallOrSecret(x, y + 1, dungeonData, cells)));
                    flag3 = ((!IsWallOrSecret(x + 1, y, dungeonData, cells) && !IsTopWallOrSecret(x + 1, y, dungeonData, cells)) || IsFaceWallHigherOrSecret(x + 1, y, dungeonData, cells));
                    flag4 = (y > 3 && IsFaceWallHigherOrSecret(x + 1, y - 1, dungeonData, cells) && !IsFaceWallHigherOrSecret(x, y - 1, dungeonData, cells));
                    flag5 = (y > 3 && IsFaceWallHigherOrSecret(x, y - 1, dungeonData, cells));
                    flag6 = (y > 3 && IsFaceWallHigherOrSecret(x - 1, y - 1, dungeonData, cells) && !IsFaceWallHigherOrSecret(x, y - 1, dungeonData, cells));
                    flag7 = ((!IsWallOrSecret(x - 1, y, dungeonData, cells) && !IsTopWallOrSecret(x - 1, y, dungeonData, cells)) || IsFaceWallHigherOrSecret(x - 1, y, dungeonData, cells));
                    flag8 = (IsTopWallOrSecret(x - 1, y, dungeonData, cells) && !IsTopWallOrSecret(x, y, dungeonData, cells) && (IsWallOrSecret(x, y + 1, dungeonData, cells) || IsTopWallOrSecret(x, y + 1, dungeonData, cells)));
                }               
                if (flag || flag2 || flag3 || flag4 || flag5 || flag6 || flag7 || flag8) {
                    int rawTile = borderGridForCellPosition.GetIndexGivenSides(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8);
                    if (borderGridForCellPosition.UsesRatChunkBorders) {
                        bool flag9 = y > 3;
                        if (flag9) { flag9 = !dungeonData[x, y - 1].HasFloorNeighbor(dungeonData, false, true); }
                        TileIndexGrid.RatChunkResult ratChunkResult = TileIndexGrid.RatChunkResult.NONE;
                        rawTile = borderGridForCellPosition.GetRatChunkIndexGivenSides(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, out ratChunkResult);
                    }
                    int tileFromRawTile2 = BuilderUtil.GetTileFromRawTile(rawTile);
                    tk2dSpriteDefinition tk2dSpriteDefinition2 = dungeonCollection.spriteDefinitions[tileFromRawTile2];
                    if (material2 == null) { material2 = tk2dSpriteDefinition2.material; }                    
                    int count2 = list.Count;
                    Vector3 a2 = position.ToVector3(position.y);
                    Vector3[] array2 = tk2dSpriteDefinition2.ConstructExpensivePositions();
                    for (int k = 0; k < array2.Length; k++) {
                        Vector3 b4 = array2[k].WithZ(array2[k].y);
                        list.Add(a2 + b4 + b2);
                        list4.Add(tk2dSpriteDefinition2.uvs[k]);
                    }
                    for (int l = 0; l < tk2dSpriteDefinition2.indices.Length; l++) { list3.Add(count2 + tk2dSpriteDefinition2.indices[l]); }                    
                }
            }
            Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            for (int m = 0; m < list.Count; m++) { vector = Vector3.Min(vector, list[m]); }
            for (int n = 0; n < list.Count; n++) { list[n] -= vector; }
            mesh.vertices = list.ToArray();
            mesh.uv = list4.ToArray();
            mesh.subMeshCount = 2;
            mesh.SetTriangles(list2.ToArray(), 0);
            mesh.SetTriangles(list3.ToArray(), 1);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            GameObject gameObject = new GameObject(objectName);
            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
            gameObject.transform.position = vector;
            meshFilter.mesh = mesh;            
            meshRenderer.materials = new Material[] { material, material2 };
            return gameObject;
        }

        public static GameObject GenerateWallMesh(DungeonData.Direction exitDirection, IntVector2 exitBasePosition, string objectName = "secret room door object", DungeonData dungeonData = null, bool abridged = false, bool isGlitched = false, tk2dSpriteCollectionData DungeonCollectionOverride = null, Dungeon secondaryDungeon = null) {
            if (dungeonData == null) { dungeonData = GameManager.Instance.Dungeon.data; }
            Mesh mesh = new Mesh();
            List<Vector3> list = new List<Vector3>();
            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();
            List<int> list4 = new List<int>();
            List<int> list5 = new List<int>();
            List<Vector2> list6 = new List<Vector2>();
            List<Color> list7 = new List<Color>();
            Material material = null;
            Material material2 = null;
            Material material3 = null;
            Material material4 = null;
            tk2dSpriteCollectionData dungeonCollection = DungeonCollectionOverride;

            if (!DungeonCollectionOverride) { dungeonCollection = GameManager.Instance.Dungeon.tileIndices.dungeonCollection; }

            if (isGlitched) {
                dungeonCollection = UnityEngine.Object.Instantiate(dungeonCollection);
                foreach (tk2dSpriteDefinition spriteInfo in dungeonCollection.spriteDefinitions) {
                    ExpandShaders.ApplyGlitchShader(spriteInfo, UnityEngine.Random.Range(0.038f, 0.042f), UnityEngine.Random.Range(0.073f, 0.067f), UnityEngine.Random.Range(0.052f, 0.048f), UnityEngine.Random.Range(0.073f, 0.67f), UnityEngine.Random.Range(0.052f, 0.048f));
                }
            }
            TileIndexGrid borderGridForCellPosition = GetBorderGridForCellPosition(exitBasePosition, dungeonData, secondaryDungeon);
            CellData cellData = dungeonData[exitBasePosition];
            switch (exitDirection) {
                case DungeonData.Direction.NORTH: {
                        AddCeilingTileAtPosition(exitBasePosition, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        AddCeilingTileAtPosition(exitBasePosition + IntVector2.Right, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        AddTileAtPosition(exitBasePosition, borderGridForCellPosition.leftCapIndices.GetIndexByWeight(), list, list3, list6, list7, ref material2, dungeonCollection, -2.45f, false);
                        AddTileAtPosition(exitBasePosition + IntVector2.Right, borderGridForCellPosition.rightCapIndices.GetIndexByWeight(), list, list3, list6, list7, ref material2, dungeonCollection, -2.45f, false);
                        int indexFromTupleArray = SecretRoomBuilder.GetIndexFromTupleArray(cellData, SecretRoomUtility.metadataLookupTableRef[TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER], cellData.cellVisualData.roomVisualTypeIndex);
                        AddTileAtPosition(exitBasePosition + IntVector2.Down, indexFromTupleArray, list, list4, list6, list7, out material3, dungeonCollection, -0.4f, true, new Color(0f, 1f, 1f), new Color(0f, 0.5f, 1f));
                        indexFromTupleArray = SecretRoomBuilder.GetIndexFromTupleArray(cellData, SecretRoomUtility.metadataLookupTableRef[TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER], cellData.cellVisualData.roomVisualTypeIndex);
                        AddTileAtPosition(exitBasePosition + IntVector2.Down + IntVector2.Right, indexFromTupleArray, list, list4, list6, list7, out material3, dungeonCollection, -0.4f, true, new Color(0f, 1f, 1f), new Color(0f, 0.5f, 1f));
                        indexFromTupleArray = SecretRoomBuilder.GetIndexFromTupleArray(cellData, SecretRoomUtility.metadataLookupTableRef[TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER], cellData.cellVisualData.roomVisualTypeIndex);
                        AddTileAtPosition(exitBasePosition + IntVector2.Down * 2, indexFromTupleArray, list, list4, list6, list7, out material3, dungeonCollection, 1.6f, true, new Color(0f, 0.5f, 1f), new Color(0f, 0f, 1f));
                        indexFromTupleArray = SecretRoomBuilder.GetIndexFromTupleArray(cellData, SecretRoomUtility.metadataLookupTableRef[TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER], cellData.cellVisualData.roomVisualTypeIndex);
                        AddTileAtPosition(exitBasePosition + IntVector2.Down * 2 + IntVector2.Right, indexFromTupleArray, list, list4, list6, list7, out material3, dungeonCollection, 1.6f, true, new Color(0f, 0.5f, 1f), new Color(0f, 0f, 1f));
                        break;
                    }
                case DungeonData.Direction.EAST: {
                        if (!abridged) {
                            AddCeilingTileAtPosition(exitBasePosition + IntVector2.Down, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        }
                        if (!abridged) {
                            AddCeilingTileAtPosition(exitBasePosition + IntVector2.Zero, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        }
                        AddCeilingTileAtPosition(exitBasePosition + IntVector2.Up, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        AddCeilingTileAtPosition(exitBasePosition + IntVector2.Up * 2, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        if (!abridged) {
                            AddCeilingTileAtPosition(exitBasePosition + IntVector2.Up * 3, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        }
                        AddTileAtPosition(exitBasePosition + IntVector2.Up, borderGridForCellPosition.bottomCapIndices.GetIndexByWeight(), list, list3, list6, list7, ref material2, dungeonCollection, -2.45f, false);
                        AddTileAtPosition(exitBasePosition + IntVector2.Up * 2, borderGridForCellPosition.verticalIndices.GetIndexByWeight(), list, list3, list6, list7, ref material2, dungeonCollection, -2.45f, false);
                        if (!abridged) {
                            AddTileAtPosition(exitBasePosition + IntVector2.Up * 3, borderGridForCellPosition.topCapIndices.GetIndexByWeight(), list, list3, list6, list7, ref material2, dungeonCollection, -2.45f, false);
                        }
                        Color color = new Color(0f, 0f, 1f, 0f);
                        AddTileAtPosition(exitBasePosition + IntVector2.Down + IntVector2.Right, GameManager.Instance.Dungeon.tileIndices.aoTileIndices.AOFloorWallLeft, list, list5, list6, list7, out material4, dungeonCollection, 1.55f, true, color, color);
                        AddTileAtPosition(exitBasePosition + IntVector2.Right, GameManager.Instance.Dungeon.tileIndices.aoTileIndices.AOFloorWallLeft, list, list5, list6, list7, out material4, dungeonCollection, 1.55f, true, color, color);
                        if (!abridged) {
                            AddTileAtPosition(exitBasePosition + IntVector2.Up + IntVector2.Right, GameManager.Instance.Dungeon.tileIndices.aoTileIndices.AOFloorWallLeft, list, list5, list6, list7, out material4, dungeonCollection, 1.55f, true, color, color);
                        }
                        break;
                    }
                case DungeonData.Direction.SOUTH: {
                        AddCeilingTileAtPosition(exitBasePosition + IntVector2.Up * 2, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        AddCeilingTileAtPosition(exitBasePosition + IntVector2.Up * 2 + IntVector2.Right, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        AddTileAtPosition(exitBasePosition + IntVector2.Up * 2, borderGridForCellPosition.leftCapIndices.GetIndexByWeight(), list, list3, list6, list7, ref material2, dungeonCollection, -2.45f, false);
                        AddTileAtPosition(exitBasePosition + IntVector2.Up * 2 + IntVector2.Right, borderGridForCellPosition.rightCapIndices.GetIndexByWeight(), list, list3, list6, list7, ref material2, dungeonCollection, -2.45f, false);
                        int indexFromTupleArray = SecretRoomBuilder.GetIndexFromTupleArray(cellData, SecretRoomUtility.metadataLookupTableRef[TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER], cellData.cellVisualData.roomVisualTypeIndex);
                        AddTileAtPosition(exitBasePosition + IntVector2.Up, indexFromTupleArray, list, list4, list6, list7, out material3, dungeonCollection, -0.4f, true, new Color(0f, 1f, 1f), new Color(0f, 0.5f, 1f));
                        indexFromTupleArray = SecretRoomBuilder.GetIndexFromTupleArray(cellData, SecretRoomUtility.metadataLookupTableRef[TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER], cellData.cellVisualData.roomVisualTypeIndex);
                        AddTileAtPosition(exitBasePosition + IntVector2.Up + IntVector2.Right, indexFromTupleArray, list, list4, list6, list7, out material3, dungeonCollection, -0.4f, true, new Color(0f, 1f, 1f), new Color(0f, 0.5f, 1f));
                        indexFromTupleArray = SecretRoomBuilder.GetIndexFromTupleArray(cellData, SecretRoomUtility.metadataLookupTableRef[TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER], cellData.cellVisualData.roomVisualTypeIndex);
                        AddTileAtPosition(exitBasePosition, indexFromTupleArray, list, list4, list6, list7, out material3, dungeonCollection, 1.6f, true, new Color(0f, 0.5f, 1f), new Color(0f, 0f, 1f));
                        indexFromTupleArray = SecretRoomBuilder.GetIndexFromTupleArray(cellData, SecretRoomUtility.metadataLookupTableRef[TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER], cellData.cellVisualData.roomVisualTypeIndex);
                        AddTileAtPosition(exitBasePosition + IntVector2.Right, indexFromTupleArray, list, list4, list6, list7, out material3, dungeonCollection, 1.6f, true, new Color(0f, 0.5f, 1f), new Color(0f, 0f, 1f));
                        Color color2 = new Color(0f, 0f, 1f, 0f);
                        AddTileAtPosition(exitBasePosition, GameManager.Instance.Dungeon.tileIndices.aoTileIndices.AOBottomWallBaseTileIndex, list, list5, list6, list7, out material4, dungeonCollection, 1.55f, true, color2, color2);
                        AddTileAtPosition(exitBasePosition + IntVector2.Right, GameManager.Instance.Dungeon.tileIndices.aoTileIndices.AOBottomWallBaseTileIndex, list, list5, list6, list7, out material4, dungeonCollection, 1.55f, true, color2, color2);
                        AddTileAtPosition(exitBasePosition + IntVector2.Down, GameManager.Instance.Dungeon.tileIndices.aoTileIndices.AOFloorTileIndex, list, list5, list6, list7, out material4, dungeonCollection, 1.55f, false, color2, color2);
                        AddTileAtPosition(exitBasePosition + IntVector2.Down + IntVector2.Right, GameManager.Instance.Dungeon.tileIndices.aoTileIndices.AOFloorTileIndex, list, list5, list6, list7, out material4, dungeonCollection, 1.55f, false, color2, color2);
                        break;
                    }
                case DungeonData.Direction.WEST: {
                        if (!abridged) {
                            AddCeilingTileAtPosition(exitBasePosition + IntVector2.Down, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        }
                        if (!abridged) {
                            AddCeilingTileAtPosition(exitBasePosition + IntVector2.Zero, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        }
                        AddCeilingTileAtPosition(exitBasePosition + IntVector2.Up, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        AddCeilingTileAtPosition(exitBasePosition + IntVector2.Up * 2, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        if (!abridged) {
                            AddCeilingTileAtPosition(exitBasePosition + IntVector2.Up * 3, borderGridForCellPosition, list, list2, list6, list7, out material, dungeonCollection);
                        }
                        AddTileAtPosition(exitBasePosition + IntVector2.Up, borderGridForCellPosition.bottomCapIndices.GetIndexByWeight(), list, list3, list6, list7, ref material2, dungeonCollection, -2.45f, false);
                        AddTileAtPosition(exitBasePosition + IntVector2.Up * 2, borderGridForCellPosition.verticalIndices.GetIndexByWeight(), list, list3, list6, list7, ref material2, dungeonCollection, -2.45f, false);
                        if (!abridged) {
                            AddTileAtPosition(exitBasePosition + IntVector2.Up * 3, borderGridForCellPosition.topCapIndices.GetIndexByWeight(), list, list3, list6, list7, ref material2, dungeonCollection, -2.45f, false);
                        }
                        Color color3 = new Color(0f, 0f, 1f, 0f);
                        AddTileAtPosition(exitBasePosition + IntVector2.Down + IntVector2.Left, GameManager.Instance.Dungeon.tileIndices.aoTileIndices.AOFloorWallRight, list, list5, list6, list7, out material4, dungeonCollection, 1.55f, true, color3, color3);
                        AddTileAtPosition(exitBasePosition + IntVector2.Left, GameManager.Instance.Dungeon.tileIndices.aoTileIndices.AOFloorWallRight, list, list5, list6, list7, out material4, dungeonCollection, 1.55f, true, color3, color3);
                        if (!abridged) {
                            AddTileAtPosition(exitBasePosition + IntVector2.Up + IntVector2.Left, GameManager.Instance.Dungeon.tileIndices.aoTileIndices.AOFloorWallRight, list, list5, list6, list7, out material4, dungeonCollection, 1.55f, true, color3, color3);
                        }
                        break;
                    }
            }
            Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            for (int i = 0; i < list.Count; i++) { vector = Vector3.Min(vector, list[i]); }
            vector.x = Mathf.FloorToInt(vector.x);
            vector.y = Mathf.FloorToInt(vector.y);
            vector.z = Mathf.FloorToInt(vector.z);
            for (int j = 0; j < list.Count; j++) { list[j] -= vector; }
            mesh.vertices = list.ToArray();
            mesh.uv = list6.ToArray();
            mesh.colors = list7.ToArray();
            mesh.subMeshCount = 4;
            mesh.SetTriangles(list2.ToArray(), 0);
            mesh.SetTriangles(list3.ToArray(), 1);
            mesh.SetTriangles(list4.ToArray(), 2);
            mesh.SetTriangles(list5.ToArray(), 3);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            GameObject gameObject = new GameObject(objectName);
            gameObject.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
            gameObject.transform.position = vector;
            meshFilter.mesh = mesh;
            meshRenderer.materials = new Material[] {
                material,
                material2,
                material3,
                material4
            };
            if (!isGlitched) { gameObject.SetLayerRecursively(LayerMask.NameToLayer("ShadowCaster")); }
            return gameObject;
        }

        public static tk2dSprite DuplicateSprite(tk2dSprite sourceSprite) {
            tk2dSprite m_Sprite = new tk2dSprite();

            m_Sprite.automaticallyManagesDepth = sourceSprite.automaticallyManagesDepth;
            m_Sprite.ignoresTiltworldDepth = sourceSprite.ignoresTiltworldDepth;
            m_Sprite.depthUsesTrimmedBounds = sourceSprite.depthUsesTrimmedBounds;
            m_Sprite.allowDefaultLayer = sourceSprite.allowDefaultLayer;
            m_Sprite.attachParent = sourceSprite.attachParent;
            m_Sprite.OverrideMaterialMode = sourceSprite.OverrideMaterialMode;
            m_Sprite.independentOrientation = sourceSprite.independentOrientation;
            m_Sprite.autodetectFootprint = sourceSprite.autodetectFootprint;
            m_Sprite.customFootprintOrigin = sourceSprite.customFootprintOrigin;
            m_Sprite.customFootprint = sourceSprite.customFootprint;
            m_Sprite.hasOffScreenCachedUpdate = sourceSprite.hasOffScreenCachedUpdate;
            m_Sprite.offScreenCachedCollection = sourceSprite.offScreenCachedCollection;
            m_Sprite.offScreenCachedID = sourceSprite.offScreenCachedID;
            m_Sprite.Collection = sourceSprite.Collection;
            m_Sprite.color = sourceSprite.color;
            m_Sprite.scale = sourceSprite.scale;
            m_Sprite.spriteId = sourceSprite.spriteId;
            m_Sprite.boxCollider2D = sourceSprite.boxCollider2D;
            m_Sprite.boxCollider = sourceSprite.boxCollider;
            m_Sprite.meshCollider = sourceSprite.meshCollider;
            m_Sprite.meshColliderPositions = sourceSprite.meshColliderPositions;
            m_Sprite.meshColliderMesh = sourceSprite.meshColliderMesh;
            m_Sprite.CachedPerpState = sourceSprite.CachedPerpState;
            m_Sprite.HeightOffGround = sourceSprite.HeightOffGround;
            m_Sprite.SortingOrder = sourceSprite.SortingOrder;
            m_Sprite.IsBraveOutlineSprite = sourceSprite.IsBraveOutlineSprite;
            m_Sprite.IsZDepthDirty = sourceSprite.IsZDepthDirty;
            m_Sprite.ApplyEmissivePropertyBlock = sourceSprite.ApplyEmissivePropertyBlock;
            m_Sprite.GenerateUV2 = sourceSprite.GenerateUV2;
            m_Sprite.LockUV2OnFrameOne = sourceSprite.LockUV2OnFrameOne;
            m_Sprite.StaticPositions = sourceSprite.StaticPositions;

            return m_Sprite;
        }

        public static void DuplicateSprite(tk2dSprite targetSprite, tk2dSprite sourceSprite) {
            targetSprite.automaticallyManagesDepth = sourceSprite.automaticallyManagesDepth;
            targetSprite.ignoresTiltworldDepth = sourceSprite.ignoresTiltworldDepth;
            targetSprite.depthUsesTrimmedBounds = sourceSprite.depthUsesTrimmedBounds;
            targetSprite.allowDefaultLayer = sourceSprite.allowDefaultLayer;
            targetSprite.attachParent = sourceSprite.attachParent;
            targetSprite.OverrideMaterialMode = sourceSprite.OverrideMaterialMode;
            targetSprite.independentOrientation = sourceSprite.independentOrientation;
            targetSprite.autodetectFootprint = sourceSprite.autodetectFootprint;
            targetSprite.customFootprintOrigin = sourceSprite.customFootprintOrigin;
            targetSprite.customFootprint = sourceSprite.customFootprint;
            targetSprite.hasOffScreenCachedUpdate = sourceSprite.hasOffScreenCachedUpdate;
            targetSprite.offScreenCachedCollection = sourceSprite.offScreenCachedCollection;
            targetSprite.offScreenCachedID = sourceSprite.offScreenCachedID;
            targetSprite.Collection = sourceSprite.Collection;
            targetSprite.color = sourceSprite.color;
            targetSprite.scale = sourceSprite.scale;
            targetSprite.spriteId = sourceSprite.spriteId;
            targetSprite.boxCollider2D = sourceSprite.boxCollider2D;
            targetSprite.boxCollider = sourceSprite.boxCollider;
            targetSprite.meshCollider = sourceSprite.meshCollider;
            targetSprite.meshColliderPositions = sourceSprite.meshColliderPositions;
            targetSprite.meshColliderMesh = sourceSprite.meshColliderMesh;
            targetSprite.CachedPerpState = sourceSprite.CachedPerpState;
            targetSprite.HeightOffGround = sourceSprite.HeightOffGround;
            targetSprite.SortingOrder = sourceSprite.SortingOrder;
            targetSprite.IsBraveOutlineSprite = sourceSprite.IsBraveOutlineSprite;
            targetSprite.IsZDepthDirty = sourceSprite.IsZDepthDirty;
            targetSprite.ApplyEmissivePropertyBlock = sourceSprite.ApplyEmissivePropertyBlock;
            targetSprite.GenerateUV2 = sourceSprite.GenerateUV2;
            targetSprite.LockUV2OnFrameOne = sourceSprite.LockUV2OnFrameOne;
            targetSprite.StaticPositions = sourceSprite.StaticPositions;
        }

        public static void DuplicateSlicedSprite(tk2dSlicedSprite targetSprite, tk2dSlicedSprite sourceSprite) {
            targetSprite.automaticallyManagesDepth = sourceSprite.automaticallyManagesDepth;
            targetSprite.ignoresTiltworldDepth = sourceSprite.ignoresTiltworldDepth;
            targetSprite.depthUsesTrimmedBounds = sourceSprite.depthUsesTrimmedBounds;
            targetSprite.allowDefaultLayer = sourceSprite.allowDefaultLayer;
            targetSprite.attachParent = sourceSprite.attachParent;
            targetSprite.OverrideMaterialMode = sourceSprite.OverrideMaterialMode;
            targetSprite.independentOrientation = sourceSprite.independentOrientation;
            targetSprite.autodetectFootprint = sourceSprite.autodetectFootprint;
            targetSprite.customFootprintOrigin = sourceSprite.customFootprintOrigin;
            targetSprite.customFootprint = sourceSprite.customFootprint;
            targetSprite.hasOffScreenCachedUpdate = sourceSprite.hasOffScreenCachedUpdate;
            targetSprite.offScreenCachedCollection = sourceSprite.offScreenCachedCollection;
            targetSprite.offScreenCachedID = sourceSprite.offScreenCachedID;
            targetSprite.Collection = sourceSprite.Collection;
            targetSprite.color = sourceSprite.color;
            targetSprite.scale = sourceSprite.scale;
            targetSprite.spriteId = sourceSprite.spriteId;
            targetSprite.boxCollider2D = sourceSprite.boxCollider2D;
            targetSprite.boxCollider = sourceSprite.boxCollider;
            targetSprite.meshCollider = sourceSprite.meshCollider;
            targetSprite.meshColliderPositions = sourceSprite.meshColliderPositions;
            targetSprite.meshColliderMesh = sourceSprite.meshColliderMesh;
            targetSprite.CachedPerpState = sourceSprite.CachedPerpState;
            targetSprite.HeightOffGround = sourceSprite.HeightOffGround;
            targetSprite.SortingOrder = sourceSprite.SortingOrder;
            targetSprite.IsBraveOutlineSprite = sourceSprite.IsBraveOutlineSprite;
            targetSprite.IsZDepthDirty = sourceSprite.IsZDepthDirty;
            targetSprite.dimensions = sourceSprite.dimensions;
            targetSprite.anchor = sourceSprite.anchor;
            targetSprite.TileStretchedSprites = sourceSprite.TileStretchedSprites;
            targetSprite.borderTop = sourceSprite.borderTop;
            targetSprite.borderBottom = sourceSprite.borderBottom;
            targetSprite.borderLeft = sourceSprite.borderLeft;
            targetSprite.borderRight = sourceSprite.borderRight;
            targetSprite.borderCornerBottom = sourceSprite.borderCornerBottom;
            sourceSprite.CreateBoxCollider = sourceSprite.CreateBoxCollider;
        }

        public static void DuplicateRigidBody(SpeculativeRigidbody targetRigidBody, SpeculativeRigidbody sourceRigidBody) {

            targetRigidBody.CollideWithTileMap = sourceRigidBody.CollideWithTileMap;
            targetRigidBody.CollideWithOthers = sourceRigidBody.CollideWithOthers;
            targetRigidBody.Velocity = sourceRigidBody.Velocity;
            targetRigidBody.CapVelocity = sourceRigidBody.CapVelocity;
            targetRigidBody.MaxVelocity = sourceRigidBody.MaxVelocity;
            targetRigidBody.ForceAlwaysUpdate = sourceRigidBody.ForceAlwaysUpdate;
            targetRigidBody.CanPush = sourceRigidBody.CanPush;
            targetRigidBody.CanBePushed = sourceRigidBody.CanBePushed;
            targetRigidBody.PushSpeedModifier = sourceRigidBody.PushSpeedModifier;
            targetRigidBody.CanCarry = sourceRigidBody.CanCarry;
            targetRigidBody.CanBeCarried = sourceRigidBody.CanBeCarried;
            targetRigidBody.PreventPiercing = sourceRigidBody.PreventPiercing;
            targetRigidBody.SkipEmptyColliders = sourceRigidBody.SkipEmptyColliders;
            targetRigidBody.TK2DSprite = sourceRigidBody.TK2DSprite;
            targetRigidBody.RecheckTriggers = sourceRigidBody.RecheckTriggers;
            targetRigidBody.UpdateCollidersOnRotation = sourceRigidBody.UpdateCollidersOnRotation;
            targetRigidBody.UpdateCollidersOnScale = sourceRigidBody.UpdateCollidersOnScale;
            targetRigidBody.AxialScale = sourceRigidBody.AxialScale;
            targetRigidBody.DebugParams = sourceRigidBody.DebugParams;
            targetRigidBody.IgnorePixelGrid = sourceRigidBody.IgnorePixelGrid;
            targetRigidBody.PixelColliders = new List<PixelCollider>();
            targetRigidBody.m_position = sourceRigidBody.m_position;
            

            if (sourceRigidBody.PixelColliders != null && sourceRigidBody.PixelColliders.Count > 0) {
                foreach (PixelCollider collider in sourceRigidBody.PixelColliders) {
                    targetRigidBody.PixelColliders.Add(DuplicatePixelCollider(collider));
                }
            }
        }

        public static PixelCollider DuplicatePixelCollider(PixelCollider source) {
            if (source == null) { return null; }
            PixelCollider m_NewCollider = new PixelCollider() {
                Enabled = source.Enabled,
                CollisionLayer = source.CollisionLayer,
                IsTrigger = source.IsTrigger,
                ColliderGenerationMode = source.ColliderGenerationMode,
                BagleUseFirstFrameOnly = source.BagleUseFirstFrameOnly,
                SpecifyBagelFrame = source.SpecifyBagelFrame,
                BagelColliderNumber = source.BagelColliderNumber,
                ManualOffsetX = source.ManualOffsetX,
                ManualOffsetY = source.ManualOffsetY,
                ManualWidth = source.ManualWidth,
                ManualHeight = source.ManualHeight,
                ManualDiameter = source.ManualDiameter,
                ManualLeftX = source.ManualLeftX,
                ManualLeftY = source.ManualLeftY,
                ManualRightX = source.ManualRightX,
                ManualRightY = source.ManualRightY
            };
            return m_NewCollider;
        }
        
        public static SpeculativeRigidbody GenerateNewEnemyRigidBody(AIActor targetEnemy, IntVector2 offset, IntVector2 dimensions) {
            SpeculativeRigidbody orAddComponent = GameObjectExtensions.GetOrAddComponent<SpeculativeRigidbody>(targetEnemy.gameObject);
            PixelCollider enemyCollider = new PixelCollider() {
                CollisionLayer = CollisionLayer.EnemyCollider,
                ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                SpecifyBagelFrame = string.Empty,
                BagelColliderNumber = 0,
                ManualOffsetX = offset.x,
                ManualOffsetY = offset.y,
                ManualWidth = dimensions.x,
                ManualHeight = dimensions.y,
                ManualDiameter = 0,
                ManualLeftX = 0,
                ManualLeftY = 0,
                ManualRightX = 0,
                ManualRightY = 0                
            };
            PixelCollider enemyHitBox = new PixelCollider() {
                ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                CollisionLayer = CollisionLayer.EnemyHitBox,
                SpecifyBagelFrame = string.Empty,
                BagelColliderNumber = 0,
                ManualOffsetX = offset.x,
                ManualOffsetY = offset.y,
                ManualWidth = dimensions.x,
                ManualHeight = dimensions.y,
                ManualDiameter = 0,
                ManualLeftX = 0,
                ManualLeftY = 0,
                ManualRightX = 0,
                ManualRightY = 0
            };            
            orAddComponent.PixelColliders = new List<PixelCollider> { enemyCollider, enemyHitBox };
            return orAddComponent;
        }

        public static SpeculativeRigidbody GenerateNewEnemyRigidBody(GameObject targetObject, IntVector2 dimensions, IntVector2? offset = null) {
            SpeculativeRigidbody orAddComponent = GameObjectExtensions.GetOrAddComponent<SpeculativeRigidbody>(targetObject);
            IntVector2 Offset = IntVector2.Zero;
            IntVector2 Dimensions = new IntVector2(dimensions.x * 16, dimensions.y * 16);
            if (offset.HasValue) {
                Offset = offset.Value;
            }
            PixelCollider enemyCollider = new PixelCollider() {
                CollisionLayer = CollisionLayer.EnemyCollider,
                ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                SpecifyBagelFrame = string.Empty,
                BagelColliderNumber = 0,
                ManualOffsetX = Offset.x,
                ManualOffsetY = Offset.y,
                ManualWidth = Dimensions.x,
                ManualHeight = Dimensions.y,
                ManualDiameter = 0,
                ManualLeftX = 0,
                ManualLeftY = 0,
                ManualRightX = 0,
                ManualRightY = 0                
            };
            PixelCollider enemyHitBox = new PixelCollider() {
                ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                CollisionLayer = CollisionLayer.EnemyHitBox,
                SpecifyBagelFrame = string.Empty,
                BagelColliderNumber = 0,
                ManualOffsetX = Offset.x,
                ManualOffsetY = Offset.y,
                ManualWidth = Dimensions.x,
                ManualHeight = Dimensions.y,
                ManualDiameter = 0,
                ManualLeftX = 0,
                ManualLeftY = 0,
                ManualRightX = 0,
                ManualRightY = 0
            };            
            orAddComponent.PixelColliders = new List<PixelCollider> { enemyCollider, enemyHitBox };
            return orAddComponent;
        }

        public static SpeculativeRigidbody GenerateOrAddToRigidBody(GameObject targetObject, CollisionLayer collisionLayer, PixelCollider.PixelColliderGeneration colliderGenerationMode = PixelCollider.PixelColliderGeneration.Tk2dPolygon, bool collideWithTileMap = false, bool CollideWithOthers = true, bool CanBeCarried = true, bool CanBePushed = false, bool RecheckTriggers = false, bool IsTrigger = false, bool replaceExistingColliders = false, bool UsesPixelsAsUnitSize = false, IntVector2? dimensions = null, IntVector2? offset = null) {
            SpeculativeRigidbody m_CachedRigidBody = GameObjectExtensions.GetOrAddComponent<SpeculativeRigidbody>(targetObject);
            m_CachedRigidBody.CollideWithOthers = CollideWithOthers;
            m_CachedRigidBody.CollideWithTileMap = collideWithTileMap;
            m_CachedRigidBody.Velocity = Vector2.zero;
            m_CachedRigidBody.MaxVelocity = Vector2.zero;
            m_CachedRigidBody.ForceAlwaysUpdate = false;
            m_CachedRigidBody.CanPush = false;
            m_CachedRigidBody.CanBePushed = CanBePushed;
            m_CachedRigidBody.PushSpeedModifier = 1f;
            m_CachedRigidBody.CanCarry = false;
            m_CachedRigidBody.CanBeCarried = CanBeCarried;
            m_CachedRigidBody.PreventPiercing = false;
            m_CachedRigidBody.SkipEmptyColliders = false;
            m_CachedRigidBody.RecheckTriggers = RecheckTriggers;
            m_CachedRigidBody.UpdateCollidersOnRotation = false;
            m_CachedRigidBody.UpdateCollidersOnScale = false;
            
            IntVector2 Offset = IntVector2.Zero;
            IntVector2 Dimensions = IntVector2.Zero;
            if (colliderGenerationMode != PixelCollider.PixelColliderGeneration.Tk2dPolygon) {
                if (dimensions.HasValue) {
                    Dimensions = dimensions.Value;
                    if (!UsesPixelsAsUnitSize) {
                        Dimensions = (new IntVector2(Dimensions.x * 16, Dimensions.y * 16));
                    }
                }
                if (offset.HasValue) {
                    Offset = offset.Value;
                    if (!UsesPixelsAsUnitSize) {
                        Offset = (new IntVector2(Offset.x * 16, Offset.y * 16));
                    }
                }
            }
            PixelCollider m_CachedCollider = new PixelCollider() {
                ColliderGenerationMode = colliderGenerationMode,
                CollisionLayer = collisionLayer,
                IsTrigger = IsTrigger,
                BagleUseFirstFrameOnly = (colliderGenerationMode == PixelCollider.PixelColliderGeneration.Tk2dPolygon),
                SpecifyBagelFrame = string.Empty,                
                BagelColliderNumber = 0,
                ManualOffsetX = Offset.x,
                ManualOffsetY = Offset.y,
                ManualWidth = Dimensions.x,
                ManualHeight = Dimensions.y,
                ManualDiameter = 0,
                ManualLeftX = 0,
                ManualLeftY = 0,
                ManualRightX = 0,
                ManualRightY = 0
            };

            if (replaceExistingColliders | m_CachedRigidBody.PixelColliders == null) {
                m_CachedRigidBody.PixelColliders = new List<PixelCollider> { m_CachedCollider };
            } else {
                m_CachedRigidBody.PixelColliders.Add(m_CachedCollider);
            }

            if (m_CachedRigidBody.sprite && colliderGenerationMode == PixelCollider.PixelColliderGeneration.Tk2dPolygon) {
                Bounds bounds = m_CachedRigidBody.sprite.GetBounds();
                m_CachedRigidBody.sprite.GetTrueCurrentSpriteDef().colliderVertices = new Vector3[] { bounds.center - bounds.extents, bounds.center + bounds.extents };                
                // m_CachedRigidBody.ForceRegenerate();
                // m_CachedRigidBody.RegenerateCache();
            }

            return m_CachedRigidBody;
        }

        private static HashSet<IntVector2> GetCeilingTileSet(IntVector2 pos1, IntVector2 pos2, DungeonData.Direction facingDirection) {
            IntVector2 intVector;
            IntVector2 intVector2;
            if (facingDirection == DungeonData.Direction.NORTH) {
                intVector = pos1 + new IntVector2(-1, 0);
                intVector2 = pos2 + new IntVector2(1, 1);
            } else if (facingDirection == DungeonData.Direction.SOUTH) {
                intVector = pos1 + new IntVector2(-1, 2);
                intVector2 = pos2 + new IntVector2(1, 3);
            } else if (facingDirection == DungeonData.Direction.EAST) {
                intVector = pos1 + new IntVector2(-1, 0);
                intVector2 = pos2 + new IntVector2(0, 3);
            } else {
                if (facingDirection != DungeonData.Direction.WEST) { return null; }
                intVector = pos1 + new IntVector2(0, 0);
                intVector2 = pos2 + new IntVector2(1, 3);
            }
            HashSet<IntVector2> hashSet = new HashSet<IntVector2>();
            for (int i = intVector.x; i <= intVector2.x; i++) {
                for (int j = intVector.y; j <= intVector2.y; j++) {
                    IntVector2 item = new IntVector2(i, j);
                    hashSet.Add(item);
                }
            }
            return hashSet;
        }

        private static bool IsTopWall(int x, int y, DungeonData data, HashSet<IntVector2> cells) {
            return data.cellData[x][y].type != CellType.WALL && (data.cellData[x][y - 1].type == CellType.WALL || cells.Contains(new IntVector2(x, y - 1))) && !cells.Contains(new IntVector2(x, y + 1));
        }

        private static bool IsWall(int x, int y, DungeonData data, HashSet<IntVector2> cells) {
            return cells.Contains(new IntVector2(x, y)) || data[x, y].type == CellType.WALL;
        }

        private static bool IsTopWallOrSecret(int x, int y, DungeonData data, HashSet<IntVector2> cells) {
            return data[x, y].type != CellType.WALL && !data[x, y].isSecretRoomCell && IsWallOrSecret(x, y - 1, data, cells);
        }

        private static bool IsWallOrSecret(int x, int y, DungeonData data, HashSet<IntVector2> cells) {
            return data[x, y].type == CellType.WALL || data[x, y].isSecretRoomCell || cells.Contains(new IntVector2(x, y));
        }

        private static bool IsFaceWallHigherOrSecret(int x, int y, DungeonData data, HashSet<IntVector2> cells) {
            return IsFaceWallHigher(x, y, data, cells);
        }

        private static bool IsFaceWallHigher(int x, int y, DungeonData data, HashSet<IntVector2> cells) {
            return !cells.Contains(new IntVector2(x, y)) && ((data.cellData[x][y].type == CellType.WALL || data.cellData[x][y].isSecretRoomCell) && data.cellData[x][y - 2].type != CellType.WALL && !data.cellData[x][y - 2].isSecretRoomCell);
        }

        private static TileIndexGrid GetBorderGridForCellPosition(IntVector2 position, DungeonData data, Dungeon dungeonOverride = null) {
            TileIndexGrid roomCeilingBorderGrid;
            if (dungeonOverride) {
                roomCeilingBorderGrid = dungeonOverride.roomMaterialDefinitions[data.cellData[position.x][position.y].cellVisualData.roomVisualTypeIndex].roomCeilingBorderGrid;
            } else {
                roomCeilingBorderGrid = GameManager.Instance.Dungeon.roomMaterialDefinitions[data.cellData[position.x][position.y].cellVisualData.roomVisualTypeIndex].roomCeilingBorderGrid;
            }
            if (!roomCeilingBorderGrid) { roomCeilingBorderGrid = GameManager.Instance.Dungeon.roomMaterialDefinitions[0].roomCeilingBorderGrid; }
            return roomCeilingBorderGrid;
        }

        private static void AddCeilingTileAtPosition(IntVector2 position, TileIndexGrid indexGrid, List<Vector3> verts, List<int> tris, List<Vector2> uvs, List<Color> colors, out Material ceilingMaterial, tk2dSpriteCollectionData spriteData) {
            int indexByWeight = indexGrid.centerIndices.GetIndexByWeight();
            int tileFromRawTile = BuilderUtil.GetTileFromRawTile(indexByWeight);
            tk2dSpriteDefinition tk2dSpriteDefinition = spriteData.spriteDefinitions[tileFromRawTile];                        
            ceilingMaterial = tk2dSpriteDefinition.material;            
            int count = verts.Count;
            Vector3 a = position.ToVector3(position.y - 2.4f);
            Vector3[] array = tk2dSpriteDefinition.ConstructExpensivePositions();
            for (int i = 0; i < array.Length; i++) {
                Vector3 b = array[i].WithZ(array[i].y);
                verts.Add(a + b);
                uvs.Add(tk2dSpriteDefinition.uvs[i]);
                colors.Add(Color.black);
            }
            for (int j = 0; j < tk2dSpriteDefinition.indices.Length; j++) { tris.Add(count + tk2dSpriteDefinition.indices[j]); }
        }

        private static void AddTileAtPosition(IntVector2 position, int index, List<Vector3> verts, List<int> tris, List<Vector2> uvs, List<Color> colors, out Material targetMaterial, tk2dSpriteCollectionData spriteData, float zOffset, bool tilted, Color topColor, Color bottomColor) {
            int tileFromRawTile = BuilderUtil.GetTileFromRawTile(index);
            tk2dSpriteDefinition tk2dSpriteDefinition = spriteData.spriteDefinitions[tileFromRawTile];
            targetMaterial = tk2dSpriteDefinition.material;
            int count = verts.Count;
            Vector3 a = position.ToVector3(position.y + zOffset);
            Vector3[] array = tk2dSpriteDefinition.ConstructExpensivePositions();
            for (int i = 0; i < array.Length; i++) {
                Vector3 b = (!tilted) ? array[i].WithZ(array[i].y) : array[i].WithZ(-array[i].y);
                verts.Add(a + b);
                uvs.Add(tk2dSpriteDefinition.uvs[i]);
            }
            colors.Add(bottomColor);
            colors.Add(bottomColor);
            colors.Add(topColor);
            colors.Add(topColor);
            for (int j = 0; j < tk2dSpriteDefinition.indices.Length; j++) { tris.Add(count + tk2dSpriteDefinition.indices[j]); }            
        }

        private static void AddTileAtPosition(IntVector2 position, int index, List<Vector3> verts, List<int> tris, List<Vector2> uvs, List<Color> colors, ref Material targetMaterial, tk2dSpriteCollectionData spriteData, float zOffset, bool tilted = false) {
            int tileFromRawTile = BuilderUtil.GetTileFromRawTile(index);
            if (tileFromRawTile < 0 || tileFromRawTile >= spriteData.spriteDefinitions.Length) {
                Debug.Log(tileFromRawTile.ToString() + " index is out of bounds in SecretRoomBuilder, of indices: " + spriteData.spriteDefinitions.Length.ToString());
                return;
            }
            tk2dSpriteDefinition tk2dSpriteDefinition = spriteData.spriteDefinitions[tileFromRawTile];            
            targetMaterial = tk2dSpriteDefinition.material;
            int count = verts.Count;
            Vector3 a = position.ToVector3(position.y + zOffset);
            Vector3[] array = tk2dSpriteDefinition.ConstructExpensivePositions();
            for (int i = 0; i < array.Length; i++) {
                Vector3 b = (!tilted) ? array[i].WithZ(array[i].y) : array[i].WithZ(-array[i].y);
                verts.Add(a + b);
                uvs.Add(tk2dSpriteDefinition.uvs[i]);
                colors.Add(Color.black);
            }
            for (int j = 0; j < tk2dSpriteDefinition.indices.Length; j++) { tris.Add(count + tk2dSpriteDefinition.indices[j]); }
        }

        public static Texture2D FlipTexture(Texture2D original) {
            if (!original) { return null; }
            Texture2D flipped = new Texture2D(original.width, original.height);
            int xN = original.width;
            int yN = original.height;
            for (int X = 0; X < xN; X++) {
                for (int Y = 0; Y < yN; Y++) {
                    flipped.SetPixel((xN - X - 1), Y, original.GetPixel(X, Y));
                }
            }
            flipped.Apply();
            return flipped;
        }
        
        public static Texture2D CombineTextures(Texture2D aBottom, Texture2D aTop) {
            if (!aBottom) { return null; } else if (!aTop) { return aBottom; }

            if (aBottom.width != aTop.width || aBottom.height != aTop.height) {
                // throw new InvalidOperationException("AlphaBlend only works with two equal sized images");
                return null;
            }
                
            Color[] bData = aBottom.GetPixels();
            Color[] tData = aTop.GetPixels();
            int count = bData.Length;
            var rData = new Color[count];
            for (int i = 0; i < count; i++) {
                Color B = bData[i];
                Color T = tData[i];
                float srcF = T.a;
                float destF = 1f - T.a;
                float alpha = srcF + destF * B.a;
                Color R = (T * srcF + B * B.a * destF) / alpha;
                R.a = alpha;
                rData[i] = R;
            }
            Texture2D res = new Texture2D(aTop.width, aTop.height);
            res.SetPixels(rData);
            res.Apply();
            return res;
        }

        public static int LanguageToInt(StringTableManager.GungeonSupportedLanguages language) {
            switch (language) {
                case StringTableManager.GungeonSupportedLanguages.ENGLISH:
                    return 0;
                case StringTableManager.GungeonSupportedLanguages.RUBEL_TEST:
                    //return 1;
                    return 0;
                case StringTableManager.GungeonSupportedLanguages.FRENCH:
                    return 2;
                case StringTableManager.GungeonSupportedLanguages.SPANISH:
                    return 3;
                case StringTableManager.GungeonSupportedLanguages.ITALIAN:
                    return 4;
                case StringTableManager.GungeonSupportedLanguages.GERMAN:
                    return 5;
                case StringTableManager.GungeonSupportedLanguages.BRAZILIANPORTUGUESE:
                    return 6;
                case StringTableManager.GungeonSupportedLanguages.JAPANESE:
                    return 7;
                case StringTableManager.GungeonSupportedLanguages.KOREAN:
                    return 8;
                case StringTableManager.GungeonSupportedLanguages.RUSSIAN:
                    return 9;
                case StringTableManager.GungeonSupportedLanguages.POLISH:
                    return 10;
                case StringTableManager.GungeonSupportedLanguages.CHINESE:
                    return 11;
            }
            return 0;
        }

        public static StringTableManager.GungeonSupportedLanguages IntToLanguage(int input) {
            switch (input) {
                case 0:
                    return StringTableManager.GungeonSupportedLanguages.ENGLISH;
                case 1:
                    //return StringTableManager.GungeonSupportedLanguages.RUBEL_TEST;
                    return StringTableManager.GungeonSupportedLanguages.ENGLISH;
                case 2:
                    return StringTableManager.GungeonSupportedLanguages.FRENCH;                    
                case 3:
                    return StringTableManager.GungeonSupportedLanguages.SPANISH;
                case 4:
                    return StringTableManager.GungeonSupportedLanguages.ITALIAN;
                case 5:
                    return StringTableManager.GungeonSupportedLanguages.GERMAN;
                case 6:
                    return StringTableManager.GungeonSupportedLanguages.BRAZILIANPORTUGUESE;
                case 7:
                    return StringTableManager.GungeonSupportedLanguages.JAPANESE;
                case 8:
                    return StringTableManager.GungeonSupportedLanguages.KOREAN;
                case 9:
                    return StringTableManager.GungeonSupportedLanguages.RUSSIAN;
                case 10:
                    return StringTableManager.GungeonSupportedLanguages.POLISH;
                case 11:
                    return StringTableManager.GungeonSupportedLanguages.CHINESE;
            }
            return 0;
        }

        public static void TryDestroyWallTileAtPosition(Dungeon dungeon, RoomHandler room, IntVector2 position, bool UseFX = false) {
            if (UseFX) { AkSoundEngine.PostEvent("Play_OBJ_stone_crumble_01", GameManager.Instance.gameObject); }
            if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] Attempting to destroy wall tile at position: " + position.ToString());
            tk2dTileMap tk2dTileMap = null;
            bool m_WasSuccessful = false;
            CellData cellData = (!dungeon.data.CheckInBoundsAndValid(position)) ? null : dungeon.data[position];
            if (cellData != null && cellData.type == CellType.WALL && cellData.HasTypeNeighbor(dungeon.data, CellType.FLOOR)) {
                m_WasSuccessful = true;
                cellData.breakable = true;
                cellData.occlusionData.overrideOcclusion = true;
                cellData.occlusionData.cellOcclusionDirty = true;
                tk2dTileMap = dungeon.DestroyWallAtPosition(cellData.position.x, cellData.position.y, true);
                if (UseFX) {
                    PaydayDrillItem drill = PickupObjectDatabase.GetById(625)?.gameObject?.GetComponent<PaydayDrillItem>();
                    if (drill) {
                        drill.VFXDustPoof.SpawnAtPosition(position.ToCenterVector3(position.y), 0f, null, null, null, null, false, null, null, false);
                    }
                }
                room.Cells.Add(cellData.position);
                room.CellsWithoutExits.Add(cellData.position);
                room.RawCells.Add(cellData.position);
                dungeon.data.ClearCachedCellData();
            }
            if (m_WasSuccessful) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] Succesfully destroyed wall tile at position: " + position.ToString());
                Pixelator.Instance.MarkOcclusionDirty();
                Pixelator.Instance.ProcessOcclusionChange(room.Epicenter, 1f, room, false);
                if (tk2dTileMap) {
                    dungeon.RebuildTilemap(tk2dTileMap);
                    // tk2dTileMap.ForceBuild();
                    // tk2dTileMap.Build();
                }
            }
        }

        public static Texture2D GenerateTexture2DFromRenderTexture(RenderTexture rTex) {
            Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
            var old_rt = RenderTexture.active;
            RenderTexture.active = rTex;
            tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex.Apply();
            RenderTexture.active = old_rt;
            return tex;
        }
        
        public static Texture2D BytesToTexture(byte[] bytes, string resourceName) {
            Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            ImageConversion.LoadImage(texture2D, bytes);
            texture2D.filterMode = FilterMode.Point;
            texture2D.name = resourceName;
            texture2D.Apply();
            return texture2D;
        }
    }

    public static class ExpandExtensions {

        public static IntVector2 ToIntVector2(this Vector3 vector, VectorConversions convertMethod = VectorConversions.Round) {
            if (convertMethod == VectorConversions.Ceil) {
                return new IntVector2(Mathf.CeilToInt(vector.x), Mathf.CeilToInt(vector.y));
            }
            if (convertMethod == VectorConversions.Floor) {
                return new IntVector2(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y));
            }
            return new IntVector2(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
        }

        public static bool IsActuallyWildWestEntrance(this RoomHandler room){
            if (room?.GetRoomName() != null && ExpandRoomPrefabs.Expand_West_Entrance?.name != null) {
                return room.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.Expand_West_Entrance.name.ToLower());
            } else {
                return false;
            }
        }

        public static void DefineProjectileCollision(this tk2dSpriteCollectionData spriteCollection, string name, int pixelWidth, int pixelHeight, int? overrideColliderPixelWidth = null, int? overrideColliderPixelHeight = null, int? overrideColliderOffsetX = null, int? overrideColliderOffsetY = null) {

            if (!overrideColliderPixelWidth.HasValue) { overrideColliderPixelWidth = pixelWidth; }
            if (!overrideColliderPixelHeight.HasValue) { overrideColliderPixelHeight = pixelHeight; }
            if (!overrideColliderOffsetX.HasValue) { overrideColliderOffsetX = 0; }
            if (!overrideColliderOffsetY.HasValue) { overrideColliderOffsetY = 0; }

            float trueWidth = (float)pixelWidth / 16;
            float trueHeight = (float)pixelHeight / 16;
            float colliderWidth = (float)overrideColliderPixelWidth.Value / 16;
            float colliderHeight = (float)overrideColliderPixelHeight.Value / 16;
            float colliderOffsetX = (float)overrideColliderOffsetX.Value / 16;
            float colliderOffsetY = (float)overrideColliderOffsetY.Value / 16;

            tk2dSpriteDefinition spriteDefinition = spriteCollection.GetSpriteDefinition(name);
            spriteDefinition.boundsDataCenter = new Vector3(trueWidth / 2f, trueHeight / 2f, 0f);
            spriteDefinition.boundsDataExtents = new Vector3(trueWidth, trueHeight, 0f);
            spriteDefinition.untrimmedBoundsDataCenter = new Vector3(trueWidth / 2f, trueHeight / 2f, 0f);
            spriteDefinition.untrimmedBoundsDataExtents = new Vector3(trueWidth, trueHeight, 0f);
            spriteDefinition.texelSize = new Vector2(1 / 16f, 1 / 16f);
            spriteDefinition.position0 = new Vector3(0f, 0f, 0f);
            spriteDefinition.position1 = new Vector3(0f + trueWidth, 0f, 0f);
            spriteDefinition.position2 = new Vector3(0f, 0f + trueHeight, 0f);
            spriteDefinition.position3 = new Vector3(0f + trueWidth, 0f + trueHeight, 0f);
            spriteDefinition.colliderVertices = new Vector3[] {
                new Vector3(colliderOffsetX, colliderOffsetY, 0f),
                new Vector3(colliderWidth / 2, colliderHeight / 2)
            };
        }
        

        public static Material Copy(this Material orig, Texture2D textureOverride = null, Shader shaderOverride = null) {
            Material m_NewMaterial = new Material(orig.shader) {
                name = orig.name,
                shaderKeywords = orig.shaderKeywords,
                globalIlluminationFlags = orig.globalIlluminationFlags,
                enableInstancing = orig.enableInstancing,
                doubleSidedGI = orig.doubleSidedGI,
                mainTextureOffset = orig.mainTextureOffset,
                mainTextureScale = orig.mainTextureScale,
                renderQueue = orig.renderQueue,
                color = orig.color,
                hideFlags = orig.hideFlags                
            };            
            if (textureOverride != null) {
                m_NewMaterial.mainTexture = textureOverride;
            } else {
                m_NewMaterial.mainTexture = orig.mainTexture;
            }

            if (shaderOverride != null) {
                m_NewMaterial.shader = shaderOverride;
            } else {
                m_NewMaterial.shader = orig.shader;
            }
            return m_NewMaterial;
        }

        public static tk2dSpriteDefinition Copy(this tk2dSpriteDefinition orig) {
            tk2dSpriteDefinition m_newSpriteCollection = new tk2dSpriteDefinition();

            m_newSpriteCollection.boundsDataCenter = orig.boundsDataCenter;
            m_newSpriteCollection.boundsDataExtents = orig.boundsDataExtents;
            m_newSpriteCollection.colliderConvex = orig.colliderConvex;
            m_newSpriteCollection.colliderSmoothSphereCollisions = orig.colliderSmoothSphereCollisions;
            m_newSpriteCollection.colliderType = orig.colliderType;
            m_newSpriteCollection.colliderVertices = orig.colliderVertices;
            m_newSpriteCollection.collisionLayer = orig.collisionLayer;
            m_newSpriteCollection.complexGeometry = orig.complexGeometry;
            m_newSpriteCollection.extractRegion = orig.extractRegion;
            m_newSpriteCollection.flipped = orig.flipped;
            m_newSpriteCollection.indices = orig.indices;
            if (orig.material != null) { m_newSpriteCollection.material = new Material(orig.material); }
            m_newSpriteCollection.materialId = orig.materialId;
            if (orig.materialInst != null) { m_newSpriteCollection.materialInst = new Material(orig.materialInst); }
            m_newSpriteCollection.metadata = orig.metadata;
            m_newSpriteCollection.name = orig.name;
            m_newSpriteCollection.normals = orig.normals;
            m_newSpriteCollection.physicsEngine = orig.physicsEngine;
            m_newSpriteCollection.position0 = orig.position0;
            m_newSpriteCollection.position1 = orig.position1;
            m_newSpriteCollection.position2 = orig.position2;
            m_newSpriteCollection.position3 = orig.position3;
            m_newSpriteCollection.regionH = orig.regionH;
            m_newSpriteCollection.regionW = orig.regionW;
            m_newSpriteCollection.regionX = orig.regionX;
            m_newSpriteCollection.regionY = orig.regionY;
            m_newSpriteCollection.tangents = orig.tangents;
            m_newSpriteCollection.texelSize = orig.texelSize;
            m_newSpriteCollection.untrimmedBoundsDataCenter = orig.untrimmedBoundsDataCenter;
            m_newSpriteCollection.untrimmedBoundsDataExtents = orig.untrimmedBoundsDataExtents;
            m_newSpriteCollection.uvs = orig.uvs;

            return m_newSpriteCollection;
            /*return new tk2dSpriteDefinition {
                boundsDataCenter = orig.boundsDataCenter,
                boundsDataExtents = orig.boundsDataExtents,
                colliderConvex = orig.colliderConvex,
                colliderSmoothSphereCollisions = orig.colliderSmoothSphereCollisions,
                colliderType = orig.colliderType,
                colliderVertices = orig.colliderVertices,
                collisionLayer = orig.collisionLayer,
                complexGeometry = orig.complexGeometry,
                extractRegion = orig.extractRegion,
                flipped = orig.flipped,
                indices = orig.indices,
                material = new Material(orig.material),
                materialId = orig.materialId,
                materialInst = new Material(orig.materialInst),
                metadata = orig.metadata,
                name = orig.name,
                normals = orig.normals,
                physicsEngine = orig.physicsEngine,
                position0 = orig.position0,
                position1 = orig.position1,
                position2 = orig.position2,
                position3 = orig.position3,
                regionH = orig.regionH,
                regionW = orig.regionW,
                regionX = orig.regionX,
                regionY = orig.regionY,
                tangents = orig.tangents,
                texelSize = orig.texelSize,
                untrimmedBoundsDataCenter = orig.untrimmedBoundsDataCenter,
                untrimmedBoundsDataExtents = orig.untrimmedBoundsDataExtents,
                uvs = orig.uvs
            };*/
        }
    }

    public static class ReflectionHelpers {

        public delegate void ActionEX<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        public delegate void ActionEX<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        public delegate void ActionEX<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
        public delegate void ActionEX<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
        public delegate void ActionEX<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
        public delegate void ActionEX<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);
        public delegate void ActionEX<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10);
        public delegate void ActionEX<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);

        public delegate TResult FuncEX<T1, T2, T3, T4, T5, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        public delegate TResult FuncEX<T1, T2, T3, T4, T5, T6, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
        public delegate TResult FuncEX<T1, T2, T3, T4, T5, T6, T7, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
        public delegate TResult FuncEX<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);

        public static IList CreateDynamicList(Type type) {
            bool flag = type == null;
            if (flag) { throw new ArgumentNullException("type", "Argument cannot be null."); }
            ConstructorInfo[] constructors = typeof(List<>).MakeGenericType(new Type[] { type }).GetConstructors();
            foreach (ConstructorInfo constructorInfo in constructors) {
                ParameterInfo[] parameters = constructorInfo.GetParameters();
                bool flag2 = parameters.Length != 0;
                if (!flag2) { return (IList)constructorInfo.Invoke(null, null); }
            }
            throw new ApplicationException("Could not create a new list with type <" + type.ToString() + ">.");
        }
        
        public static IDictionary CreateDynamicDictionary(Type typeKey, Type typeValue) {
            bool flag = typeKey == null;
            if (flag) {
                throw new ArgumentNullException("type_key", "Argument cannot be null.");
            }
            bool flag2 = typeValue == null;
            if (flag2) { throw new ArgumentNullException("type_value", "Argument cannot be null."); }
            ConstructorInfo[] constructors = typeof(Dictionary<,>).MakeGenericType(new Type[] { typeKey, typeValue }).GetConstructors();
            foreach (ConstructorInfo constructorInfo in constructors) {
                ParameterInfo[] parameters = constructorInfo.GetParameters();
                bool flag3 = parameters.Length != 0;
                if (!flag3) { return (IDictionary)constructorInfo.Invoke(null, null); }
            }
            throw new ApplicationException(string.Concat(new string[] {
                "Could not create a new dictionary with types <",
                typeKey.ToString(),
                ",",
                typeValue.ToString(),
                ">."
            }));
        }

        public static T ReflectGetField<T>(Type classType, string fieldName, object o = null) {
            FieldInfo field = classType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static));
            return (T)field.GetValue(o);
        }

        public static void ReflectSetField<T>(Type classType, string fieldName, T value, object o = null) {
            FieldInfo field = classType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static));
            field.SetValue(o, value);
        }

        public static T ReflectGetProperty<T>(Type classType, string propName, object o = null, object[] indexes = null) {
            PropertyInfo property = classType.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static));
            return (T)property.GetValue(o, indexes);
        }

        public static void ReflectSetProperty<T>(Type classType, string propName, T value, object o = null, object[] indexes = null) {
            PropertyInfo property = classType.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static));
            property.SetValue(o, value, indexes);
        }        

        public static MethodInfo ReflectGetMethod(Type classType, string methodName, Type[] methodArgumentTypes = null, Type[] genericMethodTypes = null, bool? isStatic = null) {
            MethodInfo[] array = ReflectTryGetMethods(classType, methodName, methodArgumentTypes, genericMethodTypes, isStatic);
            bool flag = array.Count() == 0;
            if (flag) { throw new MissingMethodException("Cannot reflect method, not found based on input parameters."); }
            bool flag2 = array.Count() > 1;
            if (flag2) { throw new InvalidOperationException("Cannot reflect method, more than one method matched based on input parameters."); }
            return array[0];
        }

        public static MethodInfo ReflectTryGetMethod(Type classType, string methodName, Type[] methodArgumentTypes = null, Type[] genericMethodTypes = null, bool? isStatic = null) {
            MethodInfo[] array = ReflectTryGetMethods(classType, methodName, methodArgumentTypes, genericMethodTypes, isStatic);
            bool flag = array.Count() == 0;
            MethodInfo result;
            if (flag) {
                result = null;
            } else {
                bool flag2 = array.Count() > 1;
                if (flag2) { result = null; } else { result = array[0]; }
            }
            return result;
        }

        public static MethodInfo[] ReflectTryGetMethods(Type classType, string methodName, Type[] methodArgumentTypes = null, Type[] genericMethodTypes = null, bool? isStatic = null) {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
            bool flag = isStatic == null || isStatic.Value;
            if (flag) { bindingFlags |= BindingFlags.Static; }
            bool flag2 = isStatic == null || !isStatic.Value;
            if (flag2) { bindingFlags |= BindingFlags.Instance; }
            MethodInfo[] methods = classType.GetMethods(bindingFlags);
            List<MethodInfo> list = new List<MethodInfo>();
            for (int i = 0; i < methods.Length; i++) { 
            // foreach (MethodInfo methodInfo in methods) {
                bool flag3 = methods[i].Name != methodName;
                if (!flag3) {
                    bool isGenericMethodDefinition = methods[i].IsGenericMethodDefinition;
                    if (isGenericMethodDefinition) {
                        bool flag4 = genericMethodTypes == null || genericMethodTypes.Length == 0;
                        if (flag4) { goto IL_14D; }
                        Type[] genericArguments = methods[i].GetGenericArguments();
                        bool flag5 = genericArguments.Length != genericMethodTypes.Length;
                        if (flag5) { goto IL_14D; }
                        methods[i] = methods[i].MakeGenericMethod(genericMethodTypes);
                    } else {
                        bool flag6 = genericMethodTypes != null && genericMethodTypes.Length != 0;
                        if (flag6) { goto IL_14D; }
                    }
                    ParameterInfo[] parameters = methods[i].GetParameters();
                    bool flag7 = methodArgumentTypes != null;
                    if (!flag7) { goto IL_141; }
                    bool flag8 = parameters.Length != methodArgumentTypes.Length;
                    if (!flag8) {
                        for (int j = 0; j < parameters.Length; j++) {
                            ParameterInfo parameterInfo = parameters[j];
                            bool flag9 = parameterInfo.ParameterType != methodArgumentTypes[j];
                            if (flag9) { goto IL_14A; }
                        }
                        goto IL_141;
                    }
                    IL_14A:
                    goto IL_14D;
                    IL_141:
                    list.Add(methods[i]);
                }
                IL_14D:;
            }
            return list.ToArray();
        }

        public static T InvokeMethod<T>(Type type, string methodName, object typeInstance = null, object[] methodParams = null) {
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | (typeInstance == null ? BindingFlags.Static : BindingFlags.Instance);
            return (T)type.GetMethod(methodName, bindingFlags).Invoke(typeInstance, methodParams);
        }

        public static void InvokeMethod(Type type, string methodName, object typeInstance = null, object[] methodParams = null) {
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | (typeInstance == null ? BindingFlags.Static : BindingFlags.Instance);
            type.GetMethod(methodName, bindingFlags).Invoke(typeInstance, methodParams);
        }

        public static object InvokeRefs<T0>(MethodInfo methodInfo, object o, T0 p0) {
            object[] parameters = new object[] { p0 };
            return methodInfo.Invoke(o, parameters);
        }

        public static object InvokeRefs<T0>(MethodInfo methodInfo, object o, ref T0 p0) {
            object[] array = new object[] { p0 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            return result;
        }

        public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, T0 p0, T1 p1) {
            object[] parameters = new object[] { p0, p1 };
            return methodInfo.Invoke(o, parameters);
        }

        public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, ref T0 p0, T1 p1) {
            object[] array = new object[] { p0, p1 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            return result;
        }

        public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, T0 p0, ref T1 p1) {
            object[] array = new object[] { p0, p1 };
            object result = methodInfo.Invoke(o, array);
            p1 = (T1)array[1];
            return result;
        }

        public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, ref T0 p0, ref T1 p1) {
            object[] array = new object[] { p0, p1 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            p1 = (T1)array[1];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, T1 p1, T2 p2) {
            object[] parameters = new object[] { p0, p1, p2 };
            return methodInfo.Invoke(o, parameters);
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, T1 p1, T2 p2) {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, ref T1 p1, T2 p2) {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p1 = (T1)array[1];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, T1 p1, ref T2 p2) {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p2 = (T2)array[2];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, ref T1 p1, T2 p2) {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            p1 = (T1)array[1];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, T1 p1, ref T2 p2) {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            p2 = (T2)array[2];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, ref T1 p1, ref T2 p2) {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p1 = (T1)array[1];
            p2 = (T2)array[2];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, ref T1 p1, ref T2 p2) {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            p1 = (T1)array[1];
            p2 = (T2)array[2];
            return result;
        }
    }
}

