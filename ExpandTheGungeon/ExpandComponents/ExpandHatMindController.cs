using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;
using MonoMod.RuntimeDetour;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static ExpandTheGungeon.ExpandUtilities.ReflectionHelpers;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandHatMindController : BraveBehaviour {

        public static bool BlockTeleport = true;
        public static bool ForceLineOfSight = false;

        public static Hook canTeleportFromRoomHook;
        public static Hook hasLineOfSightToTargetHook;

        public bool CanTeleportFromRoomHook(Func<RoomHandler, bool> orig, RoomHandler self) {
            if (BlockTeleport) return false;
            return orig(self);
        }

        public bool HasLineOfSightToRigidbody(Func<AIActor, SpeculativeRigidbody, bool>orig, AIActor self, SpeculativeRigidbody rigidBody) {
            if (self?.gameObject?.GetComponent<ExpandHatMindController>() && ForceLineOfSight)return true;
            return orig(self, rigidBody);
        }

        public static void ToggleLineOfSightHook(bool remove) {
            if (remove && hasLineOfSightToTargetHook != null) {
                hasLineOfSightToTargetHook.Dispose();
                hasLineOfSightToTargetHook = null;
            } else if (!remove && hasLineOfSightToTargetHook == null) {
                hasLineOfSightToTargetHook = new Hook(
                   typeof(AIActor).GetMethod(nameof(AIActor.HasLineOfSightToRigidbody), BindingFlags.Public | BindingFlags.Instance),
                   typeof(ExpandHatMindController).GetMethod(nameof(HasLineOfSightToRigidbody), BindingFlags.Public | BindingFlags.Instance),
                   typeof(AIActor)
               );
            }
        }



        public ExpandHatMindController() {
            m_attackedThisCycle = true;
            UseFakeActorTarget = true;
            UsesCable = false;
            ActAsDecoy = false;
            AttachPlayer = true;
            targetType = TargetType.AIActor;
            KillTargetExceptions = new List<string>() { "98ea2fe181ab4323ab6e9981955a9bca", "21dd14e5ca2a4a388adab5b11b69a1e1" };

            SingleVolleyEnemies = new List<string>() { "758a0a0215e6448ab52adf73bc44ae5e" };

            OverrideFacingType = new Dictionary<string, AIAnimator.FacingType>() {
                ["479556d05c7c44f3b6abb3b2067fc778"] = AIAnimator.FacingType.Target,
                ["ec8ea75b557d4e7b8ceeaacdf6f8238c"] = AIAnimator.FacingType.Target,
                ["383175a55879441d90933b5c4e60cf6f"] = AIAnimator.FacingType.Target
            };

            OverrideHatPosition = new Dictionary<string, float>() {
                ["479556d05c7c44f3b6abb3b2067fc778"] = -1.5f
            };

            OverrideActorSize = new Dictionary<string, IntVector2>() {
                ["d5a7b95774cd41f080e517bea07bf495"] = new IntVector2(5, 4), // Revolvenant
                ["479556d05c7c44f3b6abb3b2067fc778"] = new IntVector2(4, 4), // Wall Mimic
                ["1bc2a07ef87741be90c37096910843ab"] = new IntVector2(4, 4) // Chancebulons
            };

            m_PrimaryBehaviors = new List<AttackBehaviorBase>();
            m_SecondaryBehaviors = new List<AttackBehaviorBase>();

            AttachFailed = false;
            m_HasNoAttacks = false;
            MediumObjectMovementSpeed = 1.2f;
            SmallObjectMovementSpeed = 0.8f;
            MaxRoomClearsWithHammer = 5;
            m_IsMovingToNewRoom = false;
            m_GunHandGunsConfigured = false;
            m_GunHandFiredThisCycle = false;
            m_GunHandPostProccessAdded = false;
            m_SecondaryAttackIsConditional = false;

            m_active = false;
            IsOnDeath = false;
            m_TargetLeftAlive = false;
            m_HasSecondaryAttack = false;
            m_UsedSecondaryAttackThisCycle = false;
            m_SecondaryIsWallMimicSlam = false;
            m_DecoyTimer = 1;
        }

        
        [SerializeField]
        public bool UsesCable;

        [SerializeField]
        public bool UseFakeActorTarget;

        [SerializeField]
        public bool ActAsDecoy;

        [SerializeField]
        public bool AttachPlayer;

        [NonSerialized]
        public PlayerController owner;
        public MrCap MrCapItem;
        public bool AttachFailed;
        public bool IsOnDeath;
        public float MediumObjectMovementSpeed;
        public float SmallObjectMovementSpeed;
        public int MaxRoomClearsWithHammer;
        public ExpandSpriteBobber SpriteBobber;
        public ForgeHammerController PreviousHammer;
                
        public TargetType targetType;
        public enum TargetType { AIActor, Chest, Breakable, Hammer, Other }


        public List<string> KillTargetExceptions;
        public List<string> SingleVolleyEnemies;

        public Dictionary<string, AIAnimator.FacingType> OverrideFacingType;
        public Dictionary<string, float> OverrideHatPosition;
        public Dictionary<string, IntVector2> OverrideActorSize;

        private bool m_attackedThisCycle;
        private bool m_active;
        
        private bool m_TargetLeftAlive;
        private bool m_HasNoAttacks;
        private GameObject parentObject;
        private RoomHandler m_ParentRoom;
        private AIActor m_aiActor;
        private Chest m_Chest;
        private MajorBreakable m_Breakable;
        private ExpandForgeHammerComponent m_Hammer;
        private BehaviorSpeculator m_behaviorSpeculator;
        private NonActor m_fakeActor;
        private SpeculativeRigidbody m_fakeTargetRigidbody;
        private SpeculativeRigidbody m_targetRigidbody;
        private ArbitraryCableDrawer m_cable;
        private float m_DecoyTimer;
        
        private Vector2 m_LastPlayerPosition;
        
        private bool m_CachedRigidBodyCanPush;
        private bool m_CachedRigidBodyCanBePushed;
        private bool m_IsMovingToNewRoom;
        private bool m_UsedSecondaryAttackThisCycle;
        private IntVector2 m_CachedOwnerSize;
        private int m_HammerRoomClears;
        private List<AttackBehaviorBase> m_PrimaryBehaviors;
        private List<AttackBehaviorBase> m_SecondaryBehaviors;
        private GunHandBasicShootBehavior m_GunHandBehavior;
        private GunHandController[] GunHands;
        private Gun[] GunHandGuns;
        private bool m_HasSecondaryAttack;
        private bool m_GunHandGunsConfigured;
        private bool m_GunHandFiredThisCycle;
        private bool m_GunHandPostProccessAdded;
        private bool m_SecondaryIsWallMimicSlam;
        private bool m_SecondaryAttackIsConditional;
        private ShootBehavior m_WallMimicFaceSlamBehavior;
        private AIAnimator.FacingType m_PreviousFacingType;
        private List<AIActor> m_AllSummonedEnemies;

        private bool m_CanUseSecondaryAttack(bool primaryWasPressed) {
            if (primaryWasPressed) return false;
            if (!m_HasSecondaryAttack) return false;
            if (m_SecondaryAttackIsConditional && m_ParentRoom != null && m_ParentRoom.GetActiveEnemiesCount(RoomHandler.ActiveEnemyType.RoomClear) <= 0) return false;
            if (ForceLineOfSight) return false;
            if (m_GunHandFiredThisCycle) return false;
            return true;
        }
        

        private ExpandHatVFX attachedHat;

        private CellValidator m_cellValidator(DungeonData data, IntVector2 Clearence, bool excludeExitCells = false) {
            return delegate (IntVector2 c) {
                for (int X = 0; X < Clearence.x; X++) {
                    for (int Y = 0; Y < Clearence.y; Y++) {
                        if (!data.CheckInBoundsAndValid(c.x + X, c.y + Y)) return false;
                        if (data[c.x + X, c.y + Y].type == CellType.PIT) return false;
                        if (data[c.x + X, c.y + Y].isOccupied) return false;
                        if (data[c.x + X, c.y + Y].type == CellType.WALL) return false;
                        if (data[c.x + X, c.y + Y].IsLowerFaceWall()) return false;
                        if (data[c.x + X, c.y + Y].IsTopWall()) return false;
                        if (Clearence == IntVector2.One) {
                            if (data[c.x + X, c.y + Y].isNextToWall) return false;
                        }
                        if (excludeExitCells) {
                            if (data[c.x + X, c.y + Y].isExitCell) return false;
                            if (data[c.x + X, c.y + Y].isExitNonOccluder) return false;
                        }
                    }
                }
                return true;
            };
        }

        public void Init(PlayerController parentPlayer, GameObject parentTarget) {
            if (!parentPlayer | !parentTarget) { AttachFailed = true;  return; }
            m_targetRigidbody = parentTarget.GetComponent<SpeculativeRigidbody>();
            if (!m_targetRigidbody) m_targetRigidbody.GetComponentInChildren<SpeculativeRigidbody>();
            parentObject = parentTarget;
            if (!m_targetRigidbody) { AttachFailed = true; return; }
            owner = parentPlayer;
            m_CachedOwnerSize = new IntVector2(owner.GetWidth() + 1, owner.GetHeight() + 1);
            if (AttachPlayer) {
                GameObject poofObject = (GameObject)Instantiate(ResourceCache.Acquire("Global VFX/Spinfall_Poof_VFX"));
                tk2dBaseSprite poofSprite = poofObject.GetComponent<tk2dBaseSprite>();
                poofSprite.PlaceAtPositionByAnchor(owner.sprite.WorldCenter.ToVector3ZUp(0f) + new Vector3(0f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                poofSprite.HeightOffGround = 5f;
                poofSprite.UpdateZDepth();
            }
            float HatPosition = 0;
            switch (targetType) { 
                case TargetType.AIActor:
                    m_aiActor = parentObject.GetComponent<AIActor>();
                    if (!m_aiActor) return;
                    m_ParentRoom = m_aiActor.CenterPosition.GetAbsoluteRoom();
                    m_behaviorSpeculator = m_aiActor.behaviorSpeculator;
                    HatPosition = (m_aiActor.sprite.GetBounds().size.y - (MrCap.MrCapVFX.GetComponent<tk2dSprite>().GetBounds().size.y / 2.1f));
                    if (OverrideHatPosition.ContainsKey(m_aiActor.EnemyGuid))HatPosition += OverrideHatPosition[m_aiActor.EnemyGuid];
                    m_aiActor.PlayEffectOnActor(MrCap.MrCapVFX, new Vector3(0f, HatPosition, 0f), true, false, true);
                    attachedHat = m_aiActor.gameObject.GetComponentInChildren<ExpandHatVFX>();
                    if (attachedHat) {
                        attachedHat.hatOwner = owner;
                        attachedHat.targetType = ExpandHatVFX.TargetType.AIActor;
                    }
                    m_targetRigidbody.AddCollisionLayerOverride(CollisionMask.LayerToMask(CollisionLayer.Projectile));
                    m_aiActor.HitByEnemyBullets = true;
                    m_aiActor.CanTargetEnemies = true;
                    m_aiActor.OverrideHitEnemies = true;
                    m_aiActor.IsHarmlessEnemy = true;
                    m_aiActor.behaviorSpeculator.PostAwakenDelay = 0;
                    m_aiActor.behaviorSpeculator.InstantFirstTick = true;
                    // ExpandUtility.ConvertTeleportBehaviors(m_aiActor.behaviorSpeculator, true);
                    if (!KillTargetExceptions.Contains(m_aiActor.EnemyGuid))m_aiActor.IgnoreForRoomClear = true;
                    m_aiActor.healthHaver.OnPreDeath += OnPreDeath;
                    m_aiActor.healthHaver.OnDeath += OnDeath;
                    if (AttachPlayer && m_targetRigidbody)m_targetRigidbody.OnPreRigidbodyCollision += EnemyOnPreRigidBodyCollision;
                    if (m_aiActor.visibilityManager) Destroy(m_aiActor.visibilityManager);
                    m_ParentRoom.DeregisterEnemy(m_aiActor);
                    if (parentObject.transform.parent)parentObject.transform.SetParent(GameManager.Instance.Dungeon.gameObject.transform);
                    m_aiActor.ClearPath();
                    tk2dSpriteAnimator SpriteAnimator = m_aiActor.spriteAnimator;
                    SpriteAnimator.AnimationEventTriggered = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>)Delegate.Combine(SpriteAnimator.AnimationEventTriggered, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>(AnimEventTriggered));
                    if (aiAnimator.ChildAnimator) {
                        tk2dSpriteAnimator SpriteAnimator2 = m_aiActor.aiAnimator.ChildAnimator.spriteAnimator;
                        SpriteAnimator2.AnimationEventTriggered = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>)Delegate.Combine(SpriteAnimator2.AnimationEventTriggered, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>(AnimEventTriggered));
                    }
                    if (m_aiActor.aiAnimator) {
                        m_PreviousFacingType = m_aiActor.aiAnimator.facingType;
                        if (OverrideFacingType.ContainsKey(m_aiActor.EnemyGuid)) {
                            m_aiActor.aiAnimator.facingType = OverrideFacingType[m_aiActor.EnemyGuid];
                        }
                    }
                    break;
                case TargetType.Chest:
                    if (!parentObject | !parentObject.GetComponent<Chest>()) { AttachFailed = true; return; }
                    m_Chest = parentObject.GetComponent<Chest>();
                    // m_ParentRoom = ReflectGetField<RoomHandler>(typeof(Chest), "m_room", m_Chest);
                    m_ParentRoom = parentObject.transform.position.GetAbsoluteRoom();
                    // if (m_ParentRoom == null)parentObject.transform.position.GetAbsoluteRoom();
                    if (m_ParentRoom == null) { AttachFailed = true; return; }
                    m_Chest.DeregisterChestOnMinimap();
                    m_ParentRoom.DeregisterInteractable(m_Chest);
                    if (m_targetRigidbody) m_targetRigidbody.OnPreRigidbodyCollision += ChestOnPreRigidBodyCollision;
                    if (m_Chest.majorBreakable) m_Chest.majorBreakable.OnBreak += OnChestBreak;
                    HatPosition = (m_Chest.sprite.GetBounds().size.y - (MrCap.MrCapVFX.GetComponent<tk2dSprite>().GetBounds().size.y / 1.8f));
                    attachedHat = ExpandHatVFX.PlaceHatOnObject(parentObject, new Vector3(0f, HatPosition, 0f), ExpandHatVFX.TargetType.Generic, true, false, true);
                    if (m_Chest.IsOpen && attachedHat)attachedHat.transform.localPosition += new Vector3(0, attachedHat.sprite.GetBounds().size.y / 1.5f, 0);
                    m_CachedRigidBodyCanPush = m_targetRigidbody.CanBePushed;
                    m_CachedRigidBodyCanBePushed = m_targetRigidbody.CanBePushed;
                    m_targetRigidbody.CanBePushed = true;
                    m_targetRigidbody.CanPush = true;
                    m_targetRigidbody.CapVelocity = true;
                    m_targetRigidbody.MaxVelocity = new Vector2(MediumObjectMovementSpeed, MediumObjectMovementSpeed);
                    SpriteBobber = parentObject.GetComponentInChildren<ExpandSpriteBobber>();
                    if (SpriteBobber) {
                        SpriteBobber.ShowBobber();
                    } else {
                        GameObject EXSpriteBobber = new GameObject("EXChestSpriteBobber") { layer = 22 };
                        SpriteBobber = EXSpriteBobber.AddComponent<ExpandSpriteBobber>();
                        SpriteBobber.AttachBobber(parentObject.transform);
                    }
                    break;
                case TargetType.Breakable:
                    if (!parentObject | !parentObject.GetComponent<MajorBreakable>()) { AttachFailed = true; return; }
                    m_Breakable = parentObject.GetComponent<MajorBreakable>();
                    m_ParentRoom = parentObject.transform.position.GetAbsoluteRoom();
                    if (parentObject.GetComponent<ExpandFakeChest>()) {
                        ExpandFakeChest m_FakeChest = parentObject.GetComponent<ExpandFakeChest>();
                        m_FakeChest.DeregisterChestOnMinimap();
                        m_ParentRoom.DeregisterInteractable(m_FakeChest);
                    }
                    if (m_targetRigidbody) m_targetRigidbody.OnPreRigidbodyCollision += BreakableOnPreRigidBodyCollision;
                    // if (m_ParentRoom == null)parentObject.transform.position.GetAbsoluteRoom();
                    if (m_ParentRoom == null) { AttachFailed = true; return; }
                    HatPosition = (m_Breakable.sprite.GetBounds().size.y - (MrCap.MrCapVFX.GetComponent<tk2dSprite>().GetBounds().size.y / 1.8f));
                    attachedHat = ExpandHatVFX.PlaceHatOnObject(parentObject, new Vector3(0f, HatPosition, 0f), ExpandHatVFX.TargetType.Generic, true, false, true);
                    m_CachedRigidBodyCanPush = m_targetRigidbody.CanBePushed;
                    m_CachedRigidBodyCanBePushed = m_targetRigidbody.CanBePushed;
                    m_targetRigidbody.CanBePushed = true;
                    m_targetRigidbody.CanPush = true;
                    m_targetRigidbody.CapVelocity = true;
                    m_targetRigidbody.MaxVelocity = new Vector2(MediumObjectMovementSpeed, MediumObjectMovementSpeed);
                    SpriteBobber = parentObject.GetComponentInChildren<ExpandSpriteBobber>();
                    if (SpriteBobber) {
                        SpriteBobber.ShowBobber();
                    } else {
                        GameObject EXSpriteBobber = new GameObject("EXPotSpriteBobber") { layer = 22 };
                        SpriteBobber = EXSpriteBobber.AddComponent<ExpandSpriteBobber>();
                        if (!parentObject.GetComponent<ExpandFakeChest>())SpriteBobber.bobType = ExpandSpriteBobber.BobType.TinyBoi;
                        SpriteBobber.AttachBobber(parentObject.transform);
                    }
                    m_Breakable.OnBreak += BreakableOnBreak;
                    break;
                case TargetType.Hammer:
                    if (!parentObject | !parentObject.GetComponent<ExpandForgeHammerComponent>()) { AttachFailed = true; return; }
                    m_Hammer = parentObject.GetComponent<ExpandForgeHammerComponent>();
                    m_ParentRoom = m_Hammer.ParentRoom;
                    if (m_ParentRoom == null) m_ParentRoom = owner.CenterPosition.GetAbsoluteRoom();
                    if (m_ParentRoom == null) parentObject.transform.position.GetAbsoluteRoom();
                    if (m_ParentRoom == null) { AttachFailed = true; return; }
                    if (PreviousHammer) {
                        GameObject previousHammerHatVFX = new GameObject("Expand Hammer Mirror Child", new Type[] { typeof(ExpandSpriteMirror) }) { layer = parentObject.layer };
                        ExpandSpriteMirror previousHammerMirror = previousHammerHatVFX.AddComponent<ExpandSpriteMirror>();
                        if (PreviousHammer.sprite.GetCurrentSpriteDef().name.Contains("_right_")) {
                            previousHammerMirror.AttachMirror(PreviousHammer.transform, ExpandPrefabs.EXHattyHammerCollection.GetComponent<tk2dSpriteCollectionData>(), new Vector3(-3.8f, -2.6f), true);
                        } else {
                            previousHammerMirror.AttachMirror(PreviousHammer.transform, ExpandPrefabs.EXHattyHammerCollection.GetComponent<tk2dSpriteCollectionData>(), new Vector3(-2.8f, -2.6f), true);
                        }
                        PreviousHammer.Deactivate();
                    }
                    owner.OnRoomClearEvent += HandleOnRoomCleared;
                    m_HammerRoomClears = 0;
                    break;
                case TargetType.Other:
                    break; // Not implemented yet
            }
            if (UseFakeActorTarget) BuildFakeActor();
            if (UsesCable) AttachCable();
            if (m_ParentRoom == null)m_ParentRoom = parentObject.transform.position.GetAbsoluteRoom();
            if (AttachPlayer) {
                if (canTeleportFromRoomHook == null) {
                    canTeleportFromRoomHook = new Hook(
                        typeof(RoomHandler).GetMethod(nameof(RoomHandler.CanTeleportFromRoom), BindingFlags.Public | BindingFlags.Instance),
                        typeof(ExpandHatMindController).GetMethod(nameof(CanTeleportFromRoomHook), BindingFlags.Public | BindingFlags.Instance),
                        typeof(RoomHandler)
                    );
                }
                // CanTeleport = false;
                if (owner.IsInMinecart && owner.currentMineCart) owner.currentMineCart.EvacuateSpecificPlayer(owner);
                owner.IsVisible = false;
                owner.IsGone = true;
                owner.SetIsFlying(true, "MrCap Mind Control", false, false);
                owner.healthHaver.IsVulnerable = false;
                owner.specRigidbody.OnPreRigidbodyCollision += PlayerOnPreRigidBodyCollision;
                if (owner.IsOnFire) owner.IsOnFire = false;
                if (targetType == TargetType.Hammer) {
                    m_LastPlayerPosition = PreviousHammer.transform.Find("ShootPoint").PositionVector2() - Vector2.one;
                } else {
                    owner.OnEnteredCombat = (Action)Delegate.Combine(owner.OnEnteredCombat, new Action(PlayerOnEnteredCombat));
                    m_LastPlayerPosition = parentTarget.transform.position;
                }
                if (owner.CurrentRoom?.area?.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS) {
                    GameManager.Instance.MainCameraController.SetManualControl(true, true);
                    GameManager.Instance.MainCameraController.OverrideRecoverySpeed = 12;
                    GameManager.Instance.MainCameraController.OverridePosition = parentObject.transform.position;
                }
                owner.transform.position = m_LastPlayerPosition;
                owner.specRigidbody.Reinitialize();
                ExpandUtility.SetPlayerIsStealthed(owner, true, "MrCapMindControl");
                if (owner.CurrentRoom?.area?.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS) {
                    owner.SetInputOverride("MrCap Mind Control");
                    BraveTime.RegisterTimeScaleMultiplier(0.01f, parentTarget);
                    StartCoroutine(DelayedCameraReposition(parentTarget.transform.position));
                }
            } else {
                // owner.SetIsStealthed(true, "MrCapMindControl");
                ExpandUtility.SetPlayerIsStealthed(owner, true, "MrCapMindControl");
                m_LastPlayerPosition = owner.transform.position;
            }
            owner.SetCapableOfStealing(true, "MrCapMineControl");
            owner.IsGunLocked = true;
            m_active = true;
            ExpandTheGungeon.MrCapInUse = true;
        }

        private void BuildFakeActor() {
            GameObject fakeObject = new GameObject("fake target");
            m_fakeActor = fakeObject.AddComponent<NonActor>();
            m_fakeActor.HasShadow = false;
            m_fakeTargetRigidbody = fakeObject.AddComponent<SpeculativeRigidbody>();
            m_fakeTargetRigidbody.PixelColliders = new List<PixelCollider>();
            m_fakeTargetRigidbody.CollideWithTileMap = false;
            m_fakeTargetRigidbody.CollideWithOthers = false;
            m_fakeTargetRigidbody.CanBeCarried = false;
            m_fakeTargetRigidbody.CanBePushed = false;
            m_fakeTargetRigidbody.CanCarry = false;
            PixelCollider pixelCollider = new PixelCollider();
            pixelCollider.ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual;
            pixelCollider.CollisionLayer = CollisionLayer.TileBlocker;
            pixelCollider.ManualWidth = 4;
            pixelCollider.ManualHeight = 4;
            pixelCollider.IsTrigger = true;
            pixelCollider.CollisionLayerIgnoreOverride |= CollisionMask.LayerToMask(CollisionLayer.Projectile);
            m_fakeTargetRigidbody.PixelColliders.Add(pixelCollider);
        }
        
        private void AttachCable() {
            m_cable = parentObject.AddComponent<ArbitraryCableDrawer>();
            m_cable.Attach1Offset = owner.CenterPosition - owner.transform.position.XY();
            if (targetType == TargetType.AIActor) {
                m_cable.Attach2Offset = m_aiActor.CenterPosition - m_aiActor.transform.position.XY();
            } else {
                m_cable.Attach2Offset = parentObject.GetComponent<tk2dSprite>().WorldCenter - parentObject.transform.position.XY();
            }
            m_cable.Initialize(owner.transform, parentObject.transform);
        }

        private IEnumerator DelayedCameraReposition(Vector3 targetPosition) {
            float SystemTime = Time.realtimeSinceStartup;
            while (Vector2.Distance(GameManager.Instance.MainCameraController.transform.position.XY(), targetPosition) > 0.5f) {
                if ((Time.realtimeSinceStartup - SystemTime) > 10) {
                    ETGModConsole.Log("[ExpandTheGungeon] MrCap: Camera took longer then 10 seconds to lock back to player!", true);
                    break;
                }
                yield return null;
            }
            GameManager.Instance.MainCameraController.OverrideRecoverySpeed = 8;
            GameManager.Instance.MainCameraController.SetManualControl(false, true);
            yield return null;
            BraveTime.ClearMultiplier(parentObject);
            owner.ClearInputOverride("MrCap Mind Control");
            yield break;
        }

        private void OnPreDeath(Vector2 direction) {
            if (!IsOnDeath) {
                IsOnDeath = true;
                if (MrCapItem && MrCapItem.CurrentMindControl == this) {
                    MrCapItem.DoDetach(false);
                } else {
                    Detach(false);
                }
            }
        }

        private void OnDeath(Vector2 direction) {
            if (!IsOnDeath) {
                IsOnDeath = true;
                if (MrCapItem && MrCapItem.CurrentMindControl == this) {
                    MrCapItem.DoDetach(false);
                } else {
                    Detach(false);
                }
            }
        }

        private void OnChestBreak() { OnDeath(Vector2.zero); }
        
        private void UpdateAimTargetPosition() {
            if (!owner | !UseFakeActorTarget) return;
            PlayerController playerController = owner;
            BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(playerController.PlayerIDX);
            GungeonActions activeActions = instanceForPlayer.ActiveActions;
            m_fakeTargetRigidbody.transform.position = playerController.unadjustedAimPoint.XY();
            m_fakeTargetRigidbody.Reinitialize();
        }

        private Vector2? UpdateAimTarget(PlayerController player) {
            if (!player) return null;
            BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(player.PlayerIDX);
            GungeonActions activeActions = instanceForPlayer.ActiveActions;
            return player.unadjustedAimPoint.XY();
        }

        private void ClearOverrides(RoomHandler room) {
            List<AIActor> activeEnemies = room.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
            if (activeEnemies != null) {
                for (int i = 0; i < activeEnemies.Count; i++) {
                    if (activeEnemies[i] && activeEnemies[i] != m_aiActor && activeEnemies[i].OverrideTarget == m_aiActor.specRigidbody) activeEnemies[i].OverrideTarget = null;
                }
            }
        }

        private void Update() {
            if (!m_active | !owner | !m_targetRigidbody | IsOnDeath | m_TargetLeftAlive | m_IsMovingToNewRoom) return;
            if (owner.CurrentRoom != m_ParentRoom) m_ParentRoom = owner.CurrentRoom;
            if (AttachPlayer) {
                if (owner.IsFalling) {
                    OnPreDeath(Vector2.zero);
                    return;
                }
                m_IsMovingToNewRoom = CheckForNewRoom(true, false);
                if (m_ParentRoom != null && m_ParentRoom.connectedRooms != null) {
                    foreach (RoomHandler room in m_ParentRoom.connectedRooms) {
                        if (room.area != null && room.area?.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) {
                            m_active = false;
                            if (MrCapItem && MrCapItem.CurrentMindControl == this) {
                                MrCapItem.DoDetach(true);
                            } else {
                                Detach(false);
                            }
                            return;
                        }
                        if (targetType != TargetType.Hammer && room.IsSealed && room != m_ParentRoom) {
                            ETGModConsole.Log("[ExpandTheGungeon] MrCap: A sealed room was found with the player not in it! Fix that ***! :P", true);
                            m_active = false;
                            m_ParentRoom = room;
                            DoReposition(true);
                            return;
                        }
                    }
                }
                m_IsMovingToNewRoom = false;
            }
            if (ActAsDecoy) {
                m_DecoyTimer -= BraveTime.DeltaTime;
                if (m_DecoyTimer <= 0) {
                    m_DecoyTimer = 1;
                    if (m_ParentRoom != null) AttractEnemies(m_ParentRoom);
                }
            }
            UpdateActions();
        }

        private void LateUpdate() {
            if (!m_active | !owner | !m_targetRigidbody | m_TargetLeftAlive | m_IsMovingToNewRoom | (targetType == TargetType.Hammer && !m_Hammer)) return;
            bool UpdatePlayerPosition = true;
            if (AttachPlayer) {
                switch (targetType) {
                    case TargetType.AIActor:
                        if (m_targetRigidbody.HitboxPixelCollider != null) {
                            m_LastPlayerPosition = m_targetRigidbody.HitboxPixelCollider.UnitBottomLeft;
                        } else {
                            m_LastPlayerPosition = parentObject.transform.position;
                        }
                    break;
                    case TargetType.Chest:
                        if (m_targetRigidbody.GroundPixelCollider != null) {
                            m_LastPlayerPosition = m_targetRigidbody.HitboxPixelCollider.UnitCenterLeft;
                        } else {
                            m_LastPlayerPosition = parentObject.transform.position;
                        }
                    break;
                    case TargetType.Breakable:
                        if (m_targetRigidbody.GroundPixelCollider != null) {
                            m_LastPlayerPosition = m_targetRigidbody.HitboxPixelCollider.UnitCenterLeft;
                        } else {
                            m_LastPlayerPosition = parentObject.transform.position;
                        }
                    break;
                    case TargetType.Hammer:
                        if ((PreviousHammer && !PreviousHammer.renderer.enabled) &&
                            (m_Hammer.State == ExpandForgeHammerComponent.ExpandHammerState.Gone |
                            m_Hammer.State == ExpandForgeHammerComponent.ExpandHammerState.PreSwing)
                        ) {
                            UpdatePlayerPosition = false;
                        }
                    break;
                }
            }

            if (UpdatePlayerPosition) {
                if (targetType == TargetType.Hammer && owner.IsStealthed) ExpandUtility.SetPlayerIsStealthed(owner, false, "MrCapMindControl");
                owner.transform.position = m_LastPlayerPosition;
                owner.specRigidbody.Reinitialize();
            } else {
                if (targetType == TargetType.Hammer) {
                    m_LastPlayerPosition = owner.transform.position;
                    if (!owner.IsStealthed) ExpandUtility.SetPlayerIsStealthed(owner, true, "MrCapMindControl");
                }
            }
            
            owner.healthHaver.IsVulnerable = false;
            owner.IsVisible = false;
            if (owner.IsInMinecart && owner.currentMineCart) owner.currentMineCart.EvacuateSpecificPlayer(owner);
        }

        private void UpdateActions() {
            if (!m_active | !owner | !m_targetRigidbody | IsOnDeath | m_TargetLeftAlive | m_IsMovingToNewRoom) return;
            BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(owner.PlayerIDX);
            if (!instanceForPlayer) return;
            GungeonActions activeActions = instanceForPlayer.ActiveActions;
            bool m_FireButtonPressed = instanceForPlayer.GetButtonDown(GungeonActions.GungeonActionType.Shoot);
            bool usedSecondaryAttack = instanceForPlayer.GetButtonDown(GungeonActions.GungeonActionType.DodgeRoll);
            if (!owner.AcceptingNonMotionInput) {
                if (usedSecondaryAttack) {
                    instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.DodgeRoll);
                    usedSecondaryAttack = false;
                }
                if (m_FireButtonPressed) {
                    instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                    m_FireButtonPressed = false;
                }
            }
            
            switch (targetType) {
                case TargetType.AIActor: 
                    if (!m_aiActor) return;
                    if (UseFakeActorTarget) m_fakeActor.specRigidbody = m_fakeTargetRigidbody;
                    if (UseFakeActorTarget) {
                        m_aiActor.PlayerTarget = m_fakeActor;
                    } else {
                        m_aiActor.PlayerTarget = null;
                    }
                    if (UseFakeActorTarget) UpdateAimTargetPosition();
                    if (m_aiActor.aiShooter) {
                        if (UseFakeActorTarget) {
                            m_aiActor.aiShooter.AimAtPoint(m_behaviorSpeculator.PlayerTarget.CenterPosition);
                        } else if (UpdateAimTarget(owner).HasValue) {
                            m_aiActor.aiShooter.AimAtPoint(UpdateAimTarget(owner).Value);
                        }
                    }

                    bool m_IsImmobile = false;
                    if (m_aiActor.knockbackDoer) {
                        OverridableBool IsImmobile = ReflectGetField<OverridableBool>(typeof(KnockbackDoer), "m_isImmobile", m_aiActor.knockbackDoer);
                        if (IsImmobile != null && IsImmobile.Value) m_IsImmobile = true;
                    }

                    if (!m_IsImmobile)m_aiActor.ImpartedVelocity += (activeActions.Move.Value * m_aiActor.MovementSpeed);

                    if (!m_behaviorSpeculator | m_HasNoAttacks) return;
                    m_behaviorSpeculator.InstantFirstTick = true;
                    m_behaviorSpeculator.PostAwakenDelay = 0;
                    
                    if (m_UsedSecondaryAttackThisCycle && m_FireButtonPressed) {
                        instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                        m_FireButtonPressed = false;
                    }

                    if (usedSecondaryAttack && !m_CanUseSecondaryAttack(m_FireButtonPressed)) {
                        instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.DodgeRoll);
                        usedSecondaryAttack = false;
                    }

                    if (!m_UsedSecondaryAttackThisCycle && usedSecondaryAttack) {
                        m_UsedSecondaryAttackThisCycle = true;
                        instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.DodgeRoll);
                        if (owner.IsStealthed) ExpandUtility.SetPlayerIsStealthed(owner, false, "MrCapMindControl");
                        if (m_FireButtonPressed) {
                            instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                            m_FireButtonPressed = false;
                        }
                        
                        StartCoroutine(ProcessSecondaryAttackTimer());
                    }

                    if (m_GunHandFiredThisCycle && m_FireButtonPressed) {
                        instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                        m_FireButtonPressed = false;
                    }


                    // Handle dual wielders via seperate routine. They require special treatment to behave correctly.
                    // Using the standard code path would result in them firing their guns forever once you trigger their fire sequence.
                    if (m_GunHandGunsConfigured) {
                        if (!m_UsedSecondaryAttackThisCycle && m_FireButtonPressed && m_GunHandGunsConfigured && !m_GunHandFiredThisCycle) {
                            if (GunHands != null && GunHands.Length > 0) {
                                m_FireButtonPressed = false;
                                instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                                m_GunHandFiredThisCycle = true;
                                bool m_UseSingleVolley = false;
                                if (owner.IsStealthed) ExpandUtility.SetPlayerIsStealthed(owner, false, "MrCapMindControl");
                                if (SingleVolleyEnemies.Count > 0) {
                                    foreach (string enemyGUID in SingleVolleyEnemies) {
                                        if (m_aiActor.EnemyGuid == enemyGUID) {
                                            m_UseSingleVolley = true;
                                            break;
                                        }
                                    }
                                }
                                StartCoroutine(ProcessDualWieldAttack(activeActions, instanceForPlayer, m_UseSingleVolley));
                                return;
                            }
                            if (GunHands == null | GunHands.Length <= 0) m_GunHandGunsConfigured = false;
                        }
                        return;
                    }
                    
                    bool WeaponIsAuto = (m_aiActor?.aiShooter?.CurrentGun?.ClipCapacity > 1 && m_aiActor?.aiShooter?.CurrentGun?.gunClass != GunClass.SHOTGUN && m_aiActor.aiShooter.CurrentGun.IsAutomatic);

                    if (!m_UsedSecondaryAttackThisCycle) {
                        if (m_behaviorSpeculator.AttackCooldown <= 0) {
                            if (!m_attackedThisCycle && m_behaviorSpeculator.ActiveContinuousAttackBehavior != null)m_attackedThisCycle = true;
                            if (m_attackedThisCycle && m_behaviorSpeculator.ActiveContinuousAttackBehavior == null)m_behaviorSpeculator.AttackCooldown = float.MaxValue;
                        } else if (m_FireButtonPressed) {
                            m_attackedThisCycle = false;
                            m_behaviorSpeculator.AttackCooldown = 0;
                            if (!WeaponIsAuto) {
                                instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                                m_FireButtonPressed = false;
                            }
                            if (owner.IsStealthed) ExpandUtility.SetPlayerIsStealthed(owner, false, "MrCapMindControl");
                            if (!WeaponIsAuto && !ForceLineOfSight && hasLineOfSightToTargetHook != null) {
                                ForceLineOfSight = true;
                                StartCoroutine(ProcessForcedLineOfSight());
                            }
                        }
                    }
                    
                    if (m_behaviorSpeculator.TargetBehaviors != null && m_behaviorSpeculator.TargetBehaviors.Count > 0)m_behaviorSpeculator.TargetBehaviors.Clear();
                    
                    if (m_behaviorSpeculator.MovementBehaviors != null && m_behaviorSpeculator.MovementBehaviors.Count > 0) {
                        for (int i = 0; i < m_behaviorSpeculator.MovementBehaviors.Count; i++) {
                            if (m_behaviorSpeculator.MovementBehaviors[i] is TakeCoverBehavior) {
                                TakeCoverBehavior m_takeCover = m_behaviorSpeculator.MovementBehaviors[i] as TakeCoverBehavior;
                                m_takeCover.InitialCoverChance = 0;
                                // If a bullet kin was taking cover, force them out so their animation state doesn't become stuck in the cover animation and get all funny looking when the player starts moving them around. :P
                                InvokeMethod(typeof(TakeCoverBehavior), "BecomeDisinterested", m_takeCover, new object[] { ReflectGetField<int>(typeof(TakeCoverBehavior), "m_tableQuadrant", m_takeCover) });
                            }
                        }
                        m_behaviorSpeculator.MovementBehaviors.Clear();
                    }
                                        
                    if (m_behaviorSpeculator.AttackBehaviors != null && m_behaviorSpeculator.AttackBehaviors.Count > 0) {
                        for (int i = 0; i < m_behaviorSpeculator.AttackBehaviors.Count; i++) {
                            AttackBehaviorBase attack = m_behaviorSpeculator.AttackBehaviors[i];
                            ProcessAttackAIActor(attack);
                        }
                    } else if (!m_HasNoAttacks) {
                        m_HasNoAttacks = true;
                    }
                break;
                case TargetType.Chest:
                    if (m_IsMovingToNewRoom) {
                        if (m_targetRigidbody.Velocity != Vector2.zero)m_targetRigidbody.Velocity = Vector2.zero;
                        return;
                    }
                    if (m_Chest && !m_Chest.IsBroken) {
                        if (activeActions.Move.Value != Vector2.zero && !owner.IsOverPitAtAll) {
                            m_targetRigidbody.Velocity += activeActions.Move.Value * MediumObjectMovementSpeed;
                        } else {
                            m_targetRigidbody.Velocity = Vector2.zero;
                        }
                    }
                    if (owner && m_FireButtonPressed) {
                        instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                        if (m_Chest && !m_Chest.IsOpen && !m_Chest.IsBroken) {
                            if (m_Chest.IsLocked)m_Chest.ForceUnlock();
                            m_Chest.ForceOpen(owner);
                            if (attachedHat) attachedHat.transform.localPosition += new Vector3(0, attachedHat.sprite.GetBounds().size.y / 1.5f, 0);
                            instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                            if (m_Chest.IsGlitched)OnPreDeath(Vector2.zero);
                        }
                    }
                    break;
                case TargetType.Breakable:
                    if (m_IsMovingToNewRoom) {
                        if (m_targetRigidbody.Velocity != Vector2.zero)m_targetRigidbody.Velocity = Vector2.zero;
                        return;
                    }
                    if (m_Breakable && !m_Breakable.IsDestroyed) {
                        if (activeActions.Move.Value != Vector2.zero && !owner.IsOverPitAtAll) {
                            m_targetRigidbody.Velocity += activeActions.Move.Value * MediumObjectMovementSpeed;
                        } else {
                            m_targetRigidbody.Velocity = Vector2.zero;
                        }
                    }
                    if (owner && m_FireButtonPressed) {
                        instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                        if (m_Breakable && m_Breakable.GetComponent<ExpandFakeChest>()) {
                            ExpandFakeChest m_FakeChest = m_Breakable.GetComponent<ExpandFakeChest>();
                            if (m_FakeChest.Opened) return;
                            m_FakeChest.Interact(owner);
                            if (m_FakeChest.chestType == ExpandFakeChest.ChestType.RickRoll) {
                                OnPreDeath(Vector2.zero);
                                return;
                            } else if (m_FakeChest.chestType == ExpandFakeChest.ChestType.SurpriseChest) {
                                if (attachedHat) attachedHat.transform.localPosition += new Vector3(0, attachedHat.sprite.GetBounds().size.y / 1.5f, 0);
                            }
                        }
                    }
                    break;
                case TargetType.Hammer:
                    if (PreviousHammer && PreviousHammer.renderer.enabled) return;
                    if (owner && m_FireButtonPressed) {
                        instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                        if (m_Hammer && m_Hammer.DoManualHammerBlow())return;
                    }
                    break;
                case TargetType.Other:
                    // Not Implemented Yet.
                break;
            }
        }

        private IEnumerator ProcessForcedLineOfSight() {
            while (m_behaviorSpeculator?.ActiveContinuousAttackBehavior == null) {
                if (m_UsedSecondaryAttackThisCycle) break;
                yield return null;
            }
            while (m_behaviorSpeculator?.ActiveContinuousAttackBehavior != null) {
                if (m_UsedSecondaryAttackThisCycle) break;
                yield return null;
            }
            ForceLineOfSight = false;
            yield break;
        }
        
        private IEnumerator ProcessSecondaryAttackTimer() {
            // ETGModConsole.Log("Started", true);
            if (m_SecondaryIsWallMimicSlam) { 
                if (m_WallMimicFaceSlamBehavior == null | !m_aiActor | !m_aiActor.aiAnimator | m_aiActor.aiAnimator.IsPlaying(m_WallMimicFaceSlamBehavior.FireAnimation)) {
                    m_UsedSecondaryAttackThisCycle = false;
                    yield break;
                }
                if (!string.IsNullOrEmpty(m_WallMimicFaceSlamBehavior.TellAnimation)) {
                    if (m_WallMimicFaceSlamBehavior.UseVfx && !string.IsNullOrEmpty(m_WallMimicFaceSlamBehavior.ChargeVfx)) {
                        m_aiActor.aiAnimator.PlayVfx(m_WallMimicFaceSlamBehavior.ChargeVfx, null, null, null);
                    }
                    m_aiActor.aiAnimator.PlayUntilFinished(m_WallMimicFaceSlamBehavior.TellAnimation, true, null, -1f, false);
                    if (m_aiActor.knockbackDoer) m_aiActor.knockbackDoer.SetImmobile(true, "ShootBulletScript");
                    yield return new WaitForEndOfFrame();
                    while (m_aiActor.aiAnimator.IsPlaying(m_WallMimicFaceSlamBehavior.TellAnimation)) yield return null;
                    if (m_WallMimicFaceSlamBehavior.UseVfx && !string.IsNullOrEmpty(m_WallMimicFaceSlamBehavior.FireVfx)) m_aiActor.aiAnimator.StopVfx(m_WallMimicFaceSlamBehavior.FireVfx);
                    if (m_aiActor.knockbackDoer) m_aiActor.knockbackDoer.SetImmobile(false, "ShootBulletScript");
                }
                m_UsedSecondaryAttackThisCycle = false;
                yield break;
            }
            while (m_behaviorSpeculator?.ActiveContinuousAttackBehavior != null) {
                m_behaviorSpeculator.Interrupt();
                yield return null;
            }
            if (m_behaviorSpeculator)m_behaviorSpeculator.AttackCooldown = 0;
            while (m_behaviorSpeculator?.ActiveContinuousAttackBehavior == null)yield return null;
            if (m_behaviorSpeculator) m_behaviorSpeculator.AttackCooldown = float.MaxValue;
            while (m_behaviorSpeculator?.ActiveContinuousAttackBehavior != null) yield return null;
            // ETGModConsole.Log("Finished", true);
            m_UsedSecondaryAttackThisCycle = false;
            yield break;
        }
        
        private IEnumerator ProcessDualWieldAttack(GungeonActions action, BraveInput instance, bool useSingleVolley) {
            // GunHands dislike manual control for some reason so we gotta bypass their cooldowns and call their Fire function directly.
            // This requires replicating the cooldowns on our end.
            float elapsed = 0;
            float duration = 0.1f;
            float postFireDelay = 0.35f;
            while (elapsed < duration) {
                if (!m_active) yield break;
                foreach (GunHandController gunHand in GunHands) {
                    if (!m_active | !m_aiActor | !gunHand) yield break;
                    elapsed += BraveTime.DeltaTime;
                    gunHand.Gun.CeaseAttack();
                    gunHand.Gun.ClearCooldowns();
                    gunHand.Gun.ClearReloadData();
                    gunHand.Gun.Attack();
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.04f, 0.15f));
                }
                if (useSingleVolley) break;
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.1f));
            }
            if (useSingleVolley) postFireDelay = 1;
            yield return new WaitForSeconds(postFireDelay);
            m_GunHandFiredThisCycle = false;
            yield break;
        }

        private IEnumerator DelayedDualWieldInit(GunHandBasicShootBehavior gunShootBehavior) {
            yield return new WaitForSeconds(1);
            if (!m_GunHandGunsConfigured) {
                List<Gun> guns = new List<Gun>();
                List<GunHandController> gunHands = new List<GunHandController>();
                foreach (GunHandController gunHand in gunShootBehavior.GunHands) {
                    if (gunHand) {
                        gunHand.PreFireDelay = float.MaxValue;
                        gunHand.Cooldown = float.MaxValue;
                        gunHand.CeaseAttack();
                        gunHand.Gun.PostProcessProjectile += GunHandPostProcessProjectile;
                        gunHands.Add(gunHand);
                    }
                }
                GunHandGuns = guns.ToArray();
                if (gunHands.Count > 0) {
                    GunHands = gunHands.ToArray();
                    m_GunHandPostProccessAdded = true;
                    m_GunHandGunsConfigured = true;
                }
            }
            yield break;
        }
        
        private void ProcessAttackAIActor(AttackBehaviorBase attack) {
            if (attack == null)return;
            if (attack is LeapExplosion) {
                LeapExplosion leapExploder = (attack as LeapExplosion);
                leapExploder.maxTravelDistance = 0;
                leapExploder.minLeapDistance = 0;
                leapExploder.leapDistance = 1000;
                if (!m_PrimaryBehaviors.Contains(leapExploder)) m_PrimaryBehaviors.Add(leapExploder);
            } else if (attack is ExplodeInRadius) {
                ExplodeInRadius explodeInRadius = (attack as ExplodeInRadius);
                explodeInRadius.explodeDistance = float.MaxValue;
                if (!m_PrimaryBehaviors.Contains(explodeInRadius)) m_PrimaryBehaviors.Add(explodeInRadius);
            } else if (attack is BasicAttackBehavior) {
                BasicAttackBehavior basicAttackBehavior = (attack as BasicAttackBehavior);
                if (m_UsedSecondaryAttackThisCycle && (attack is TeleportBehavior)) {
                    TeleportBehavior teleportAttack = (attack as TeleportBehavior);
                    teleportAttack.InitialCooldown = 0;
                    teleportAttack.Cooldown = 0;
                    teleportAttack.RequiresLineOfSight = false;
                    teleportAttack.OnlyTeleportIfPlayerUnreachable = false;
                    teleportAttack.AllowCrossRoomTeleportation = true;
                    teleportAttack.MinRange = -1f;
                    teleportAttack.Range = -1f;
                    teleportAttack.MinDistanceFromPlayer = -1;
                    teleportAttack.MaxDistanceFromPlayer = -1;
                } else if (!m_UsedSecondaryAttackThisCycle && (attack is TeleportBehavior)) {
                    TeleportBehavior teleportAttack = (attack as TeleportBehavior);
                    if (!m_HasSecondaryAttack) m_HasSecondaryAttack = true;
                    if (!m_SecondaryBehaviors.Contains(teleportAttack))m_SecondaryBehaviors.Add(teleportAttack);
                    teleportAttack.Cooldown = float.MaxValue;
                    teleportAttack.StayOnScreen = false;
                    teleportAttack.RequiresLineOfSight = true;
                    teleportAttack.MaxEnemiesInRoom = 0;
                    teleportAttack.InitialCooldown = 0;
                    teleportAttack.MinRange = 1000;
                    teleportAttack.Range = 0.1f;
                } else if (!(attack is TeleportBehavior)) {
                    if (m_UsedSecondaryAttackThisCycle) {
                        basicAttackBehavior.RequiresLineOfSight = true;
                        basicAttackBehavior.Cooldown = float.MaxValue;
                    } else {
                        basicAttackBehavior.InitialCooldown = 0;
                        basicAttackBehavior.Cooldown = 0;
                        basicAttackBehavior.RequiresLineOfSight = false;
                        basicAttackBehavior.MinRange = -1f;
                        basicAttackBehavior.Range = -1f;
                    }
                    if (attack is WizardSpinShootBehavior) {
                        WizardSpinShootBehavior wizardBulletToss = (attack as WizardSpinShootBehavior);
                        if (!m_PrimaryBehaviors.Contains(wizardBulletToss)) m_PrimaryBehaviors.Add(wizardBulletToss);
                        ToggleLineOfSightHook(false);
                        wizardBulletToss.CanHitEnemies = true;
                        wizardBulletToss.LineOfSight = true;
                        wizardBulletToss.FirstSpawnDelay = 0;
                    } else if (attack is SummonEnemyBehavior) {
                        SummonEnemyBehavior summonEnemyBehavior = (attack as SummonEnemyBehavior);
                        if (m_UsedSecondaryAttackThisCycle) {
                            summonEnemyBehavior.InitialCooldown = 0;
                            summonEnemyBehavior.Cooldown = 0;
                            summonEnemyBehavior.RequiresLineOfSight = false;
                            summonEnemyBehavior.MinRange = -1;
                            summonEnemyBehavior.Range = -1;
                        } else {
                            summonEnemyBehavior.RequiresLineOfSight = true;
                            summonEnemyBehavior.Cooldown = float.MaxValue;
                            summonEnemyBehavior.MaxSummonedAtOnce = -1;
                            summonEnemyBehavior.MaxToSpawn = -1;
                        }
                        m_AllSummonedEnemies = ReflectGetField<List<AIActor>>(typeof(SummonEnemyBehavior), "m_allSpawnedActors", summonEnemyBehavior);
                        if (m_AllSummonedEnemies != null && m_AllSummonedEnemies.Count > 0) {
                            GameActorCharmEffect charmEffect = (PickupObjectDatabase.GetById(206) as RadialCharmItem).CharmEffect;
                            for (int i = 0; i < m_AllSummonedEnemies.Count; i++) {
                                if (m_AllSummonedEnemies[i]) {
                                    m_AllSummonedEnemies[i].HitByEnemyBullets = true;
                                    m_AllSummonedEnemies[i].CanTargetPlayers = false;
                                    m_AllSummonedEnemies[i].CanTargetEnemies = true;
                                    m_AllSummonedEnemies[i].OverrideHitEnemies = true;
                                    m_AllSummonedEnemies[i].IsHarmlessEnemy = true;
                                    m_AllSummonedEnemies[i].IgnoreForRoomClear = true;
                                    m_AllSummonedEnemies[i].ApplyEffect(charmEffect, 1, null);
                                    if (!m_AllSummonedEnemies[i].gameObject.GetComponent<ExpandEnemyAutoKiller>()) {
                                        ExpandEnemyAutoKiller m_KillComponent = m_AllSummonedEnemies[i].gameObject.AddComponent<ExpandEnemyAutoKiller>();
                                        if (m_aiActor) {
                                            m_KillComponent.SummonerParent = m_aiActor;
                                            m_KillComponent.KillifSummonerDies = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (!m_SecondaryBehaviors.Contains(summonEnemyBehavior)) m_SecondaryBehaviors.Add(summonEnemyBehavior);
                        if (!m_HasSecondaryAttack) m_HasSecondaryAttack = true;
                        if (!m_SecondaryAttackIsConditional) m_SecondaryAttackIsConditional = true;
                    } else if (attack is TransformBehavior) {
                        TransformBehavior transformBehavior = (attack as TransformBehavior);
                        if (!m_HasSecondaryAttack) m_HasSecondaryAttack = true;
                        if (!m_SecondaryBehaviors.Contains(transformBehavior)) m_SecondaryBehaviors.Add(transformBehavior);
                        if (m_UsedSecondaryAttackThisCycle) {
                            transformBehavior.InitialCooldown = 0;
                            transformBehavior.Cooldown = 0;
                            transformBehavior.RequiresLineOfSight = false;
                            transformBehavior.MinRange = -1;
                            transformBehavior.Range = -1;
                        } else {
                            transformBehavior.RequiresLineOfSight = true;
                            transformBehavior.Cooldown = float.MaxValue;
                        }
                    } else if (basicAttackBehavior is MirrorImageBehavior) {
                        // Ensures mirrors are more useful. (aka Killithid clones)
                        MirrorImageBehavior mirrorImageBehavior = (basicAttackBehavior as MirrorImageBehavior);
                        List<AIActor> mirrors = ReflectGetField<List<AIActor>>(typeof(MirrorImageBehavior), "m_allImages", mirrorImageBehavior); 
                        if (mirrors?.Count > 0) {
                            for (int i = 0; i < mirrors.Count; i++) {
                                if (mirrors[i]) {
                                    if (mirrors[i].CanTargetPlayers) mirrors[i].CanTargetPlayers = false;
                                    if (!mirrors[i].CanTargetEnemies) mirrors[i].CanTargetEnemies = true;
                                    if (!mirrors[i].HitByEnemyBullets) mirrors[i].HitByEnemyBullets = true;
                                    if (!mirrors[i].OverrideHitEnemies) mirrors[i].OverrideHitEnemies = true;
                                }
                            }
                        }
                    } else if (basicAttackBehavior is ShootGunBehavior) {
                        ShootGunBehavior shootGunBehavior = basicAttackBehavior as ShootGunBehavior;
                        if (!m_PrimaryBehaviors.Contains(shootGunBehavior)) m_PrimaryBehaviors.Add(shootGunBehavior);
                        if (m_UsedSecondaryAttackThisCycle) {
                            shootGunBehavior.LineOfSight = true;
                            shootGunBehavior.Cooldown = float.MaxValue;
                        } else {
                            shootGunBehavior.LineOfSight = false;
                            shootGunBehavior.EmptiesClip = false;
                            shootGunBehavior.RespectReload = false;
                            shootGunBehavior.Cooldown = 0;
                        }
                    } else if (basicAttackBehavior is ShootBehavior) {
                        ShootBehavior shootBehavior = (basicAttackBehavior as ShootBehavior);
                        if (shootBehavior.BulletScript != null && !string.IsNullOrEmpty(shootBehavior.BulletScript.scriptTypeName) &&
                            shootBehavior.BulletScript.scriptTypeName == "WallMimicSlam1")
                        {
                            if (!m_HasSecondaryAttack) m_HasSecondaryAttack = true;
                            if (!m_SecondaryIsWallMimicSlam) m_SecondaryIsWallMimicSlam = true;
                            m_behaviorSpeculator.AttackCooldown = float.MaxValue;
                            m_behaviorSpeculator.Interrupt();
                            if (m_WallMimicFaceSlamBehavior == null) m_WallMimicFaceSlamBehavior = shootBehavior;
                        } else {
                            if (!m_PrimaryBehaviors.Contains(shootBehavior)) m_PrimaryBehaviors.Add(shootBehavior);
                            shootBehavior.Cooldown = 0;
                            shootBehavior.RequiresLineOfSight = false;
                            shootBehavior.RequiresTarget = false;
                        }
                    } else if (m_GunHandBehavior == null && (basicAttackBehavior is GunHandBasicShootBehavior)) {
                        GunHandBasicShootBehavior shootDualGunBehavior = basicAttackBehavior as GunHandBasicShootBehavior;
                        if (shootDualGunBehavior != null) {
                            shootDualGunBehavior.LineOfSight = false;
                            if (shootDualGunBehavior.GunHands != null && shootDualGunBehavior.GunHands.Count > 0) {
                                int ValidGunHandCount = 0;
                                foreach (GunHandController gunHand in shootDualGunBehavior.GunHands) {
                                    if (gunHand != null && gunHand.Gun != null && gunHand.enabled && gunHand.Gun.enabled) {
                                        ValidGunHandCount++;
                                    }
                                }
                                if (ValidGunHandCount == shootDualGunBehavior.GunHands.Count) {
                                    m_GunHandBehavior = shootDualGunBehavior;
                                    StartCoroutine(DelayedDualWieldInit(m_GunHandBehavior)); // Have to delay this to give GunHands a chance to init. They don't seem to work if the enemy is captured really quicly as it first awakens.
                                }
                            }
                        }
                    } else {
                        if (!m_PrimaryBehaviors.Contains(attack)) m_PrimaryBehaviors.Add(attack);
                    }
                }
            } else if (attack is AttackBehaviorGroup) {
                AttackBehaviorGroup attackBehaviorGroup = attack as AttackBehaviorGroup;
                for (int i = 0; i < attackBehaviorGroup.AttackBehaviors.Count; i++) {
                    ProcessAttackAIActor(attackBehaviorGroup.AttackBehaviors[i].Behavior);
                }
            }
        }

        // Allows dual wielding enemies like Chest/Wall mimics and HotShot enemies to work. Otherwise their projectiles don't interact with enemies.
        public void GunHandPostProcessProjectile(Projectile projectile) {
            projectile.collidesWithEnemies = true;
            projectile.collidesWithPlayer = false;
            projectile.TreatedAsNonProjectileForChallenge = true;
        }
        
        private void SpawnWallMimicSlamProjectiles(ShootBehavior shootBehavior) {
            AIBulletBank m_bulletBank = m_behaviorSpeculator.bulletBank;
            BulletScriptSource m_bulletSource = shootBehavior.ShootPoint.GetOrAddComponent<BulletScriptSource>();
            Vector2 m_cachedTargetCenter = Vector2.zero;
            if (m_behaviorSpeculator.TargetRigidbody) {
                m_cachedTargetCenter = m_behaviorSpeculator.TargetRigidbody.GetUnitCenter(ColliderType.HitBox);
            }
            if (shootBehavior.IsBulletScript) {
                if (!m_bulletSource)m_bulletSource = shootBehavior.ShootPoint.GetOrAddComponent<BulletScriptSource>();
                m_bulletSource.BulletManager = m_bulletBank;
                m_bulletSource.BulletScript = shootBehavior.BulletScript;
                m_bulletSource.Initialize();
                return;
            }
            if (shootBehavior.IsSingleBullet) {
                AIBulletBank.Entry bullet = m_bulletBank.GetBullet(shootBehavior.BulletName);
                GameObject bulletObject = bullet.BulletObject;
                Vector2 vector = m_cachedTargetCenter;
                if (m_behaviorSpeculator.TargetRigidbody)vector = m_behaviorSpeculator.TargetRigidbody.GetUnitCenter(ColliderType.HitBox);
                float direction;
                if (shootBehavior.ShouldOverrideFireDirection) {
                    direction = shootBehavior.OverrideFireDirection;
                } else {
                    if (shootBehavior.LeadAmount > 0f) {
                        Vector2 value = shootBehavior.ShootPoint.transform.position;
                        float? overrideProjectileSpeed = (!bullet.OverrideProjectile) ? null : new float?(bullet.ProjectileData.speed);
                        Projectile component = bulletObject.GetComponent<Projectile>();
                        Vector2 predictedTargetPosition = component.GetPredictedTargetPosition(vector, m_behaviorSpeculator.TargetVelocity, new Vector2?(value), overrideProjectileSpeed);
                        vector = Vector2.Lerp(vector, predictedTargetPosition, shootBehavior.LeadAmount);
                    }
                    Vector2 vector2 = vector - shootBehavior.ShootPoint.transform.position.XY();
                    direction = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
                }
                GameObject gameObject = m_bulletBank.CreateProjectileFromBank(shootBehavior.ShootPoint.transform.position, direction, shootBehavior.BulletName, null, false, true, false);
                m_bulletBank.OnProjectileCreatedWithSource?.Invoke(shootBehavior.ShootPoint.transform.name, gameObject.GetComponent<Projectile>());
                ArcProjectile component2 = gameObject.GetComponent<ArcProjectile>();
                if (component2)component2.AdjustSpeedToHit(vector);
            }
        }

        public void PlayerOnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (otherRigidbody == m_targetRigidbody | (UseFakeActorTarget && otherRigidbody == m_fakeTargetRigidbody) |
                otherRigidbody.gameObject.GetComponent<Projectile>() | (otherPixelCollider.IsTrigger && !otherRigidbody.gameObject.GetComponent<PickupObject>()) |
                otherPixelCollider.CollisionLayer == CollisionLayer.Trap | otherRigidbody.GetComponent<AIActor>()
            ) {
                PhysicsEngine.SkipCollision = true;
            }
        }

        public void EnemyOnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (otherRigidbody == owner.specRigidbody |
                otherRigidbody.gameObject.GetComponent<CompanionController>() |
                (UseFakeActorTarget && otherRigidbody == m_fakeTargetRigidbody) |
                otherRigidbody.GetComponent<CurrencyPickup>() |
                otherRigidbody.GetComponent<PickupObject>()
                ) {
                PhysicsEngine.SkipCollision = true;
            }
        }

        public void ChestOnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (otherRigidbody == owner.specRigidbody | otherRigidbody.GetComponent<CurrencyPickup>() |
                otherRigidbody.GetComponent<PickupObject>() | otherRigidbody.GetComponent<CompanionController>()
                ) {
                PhysicsEngine.SkipCollision = true;
            }
        }

        public void BreakableOnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (otherRigidbody.gameObject.GetComponent<PlayerController>() | otherRigidbody.GetComponent<CurrencyPickup>() | otherRigidbody.GetComponent<PickupObject>() |
                otherRigidbody.GetComponent<CompanionController>()) {
                PhysicsEngine.SkipCollision = true;
            }
        }

        public void BreakableOnBreak() { OnPreDeath(Vector2.zero); }



        private void AnimEventTriggered(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip, int frameNum) {
            if (!this | !m_active | !m_aiActor | m_WallMimicFaceSlamBehavior == null) return;
            tk2dSpriteAnimationFrame frame = clip.GetFrame(frameNum);
            if (m_WallMimicFaceSlamBehavior.UseVfx && !string.IsNullOrEmpty(m_WallMimicFaceSlamBehavior.FireVfx)) m_aiActor.aiAnimator.PlayVfx(m_WallMimicFaceSlamBehavior.FireVfx, null, null, null);
            if (m_UsedSecondaryAttackThisCycle && frame.eventInfo == "fire")SpawnWallMimicSlamProjectiles(m_WallMimicFaceSlamBehavior);
        }


        public void HandleOnRoomCleared(PlayerController player) {
            m_HammerRoomClears++;
            if (m_HammerRoomClears > MaxRoomClearsWithHammer) {
                m_HammerRoomClears = 0;
                OnPreDeath(Vector2.zero);
                return;
            }
        }

        private bool CheckForNewRoom(bool skipReposition, bool avoidExitCells) {
            if (!AttachPlayer) return false;
            
            RoomHandler m_RoomChecked = owner.gameObject.transform.position.GetAbsoluteRoom();
            if (m_RoomChecked == null) m_RoomChecked = owner.CurrentRoom;

            if (m_RoomChecked != null && m_ParentRoom != m_RoomChecked) {
                m_ParentRoom = m_RoomChecked;
                switch (targetType) {
                    case TargetType.AIActor:
                        if (m_aiActor) m_aiActor.ParentRoom = m_ParentRoom;
                        break;
                    case TargetType.Hammer:
                        if (m_Hammer) m_Hammer.ParentRoom = m_ParentRoom;
                        break;
                    case TargetType.Breakable:
                        if (m_Breakable && m_Breakable.GetComponent<ExpandFakeChest>()) {
                            ExpandFakeChest m_FakeChest = m_Breakable.GetComponent<ExpandFakeChest>();
                            m_FakeChest.ParentRoom = m_ParentRoom;
                        }
                        break;
                }
                if (!skipReposition) {
                    DoReposition(avoidExitCells);
                    return true;
                }
            }
            return false;
        }
        
        private void DoReposition(bool avoidExitCells) {
            if (m_ParentRoom == null) return;
            m_active = false;
            IntVector2? newPosition = null;
            switch (targetType) {
                case TargetType.AIActor:
                    if (!m_aiActor) return;
                    // IntVector2 AIActorSize = new IntVector2(m_aiActor.GetWidth() + 1, m_aiActor.GetHeight() + 1);
                    IntVector2 AIActorSize = m_aiActor.Clearance + IntVector2.One;
                    if (OverrideActorSize.ContainsKey(m_aiActor.EnemyGuid)) AIActorSize = OverrideActorSize[m_aiActor.EnemyGuid];
                    // newPosition = m_ParentRoom.GetNearestAvailableCell(owner.transform.position, AIActorSize, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, AIActorSize, avoidExitCells));
                    newPosition = m_ParentRoom.GetNearestAvailableCell(m_aiActor.transform.position, AIActorSize, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, AIActorSize, avoidExitCells));
                    // if (!newPosition.HasValue) newPosition = m_ParentRoom.GetNearestAvailableCell(owner.transform.position, IntVector2.One, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, IntVector2.One, avoidExitCells));
                    if (!newPosition.HasValue)newPosition = m_ParentRoom.GetNearestAvailableCell(m_aiActor.transform.position, IntVector2.One, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, IntVector2.One, avoidExitCells));
                    if (!newPosition.HasValue)newPosition = m_ParentRoom.GetBestRewardLocation(AIActorSize, RoomHandler.RewardLocationStyle.PlayerCenter, false);
                    m_LastPlayerPosition = newPosition.Value.ToVector2();
                    break;
                case TargetType.Chest:
                    if (!m_Chest) return;
                    IntVector2 ActorSize = (new IntVector2(m_Chest.GetWidth(), m_Chest.GetHeight()));
                    // newPosition = m_ParentRoom.GetNearestAvailableCell(owner.transform.position, ActorSize, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, ActorSize, avoidExitCells));
                    newPosition = m_ParentRoom.GetNearestAvailableCell(m_Chest.transform.position, ActorSize, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, ActorSize, avoidExitCells));
                    // if (!newPosition.HasValue) newPosition = m_ParentRoom.GetNearestAvailableCell(owner.transform.position, IntVector2.One, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, IntVector2.One, avoidExitCells));
                    if (!newPosition.HasValue) newPosition = m_ParentRoom.GetNearestAvailableCell(m_Chest.transform.position, IntVector2.One, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, IntVector2.One, avoidExitCells));
                    // if (!newPosition.HasValue) newPosition = m_ParentRoom.GetBestRewardLocation(ActorSize, RoomHandler.RewardLocationStyle.PlayerCenter, false);
                    m_LastPlayerPosition = newPosition.Value.ToVector2();
                    break;
                case TargetType.Breakable:
                    if (!m_Breakable) return;
                    IntVector2 BreakableSize = IntVector2.One;
                    if (m_targetRigidbody.UnitDimensions.x > 1 && m_targetRigidbody.UnitDimensions.y > 1) {
                        BreakableSize = m_targetRigidbody.UnitDimensions.ToIntVector2();
                    } 
                    newPosition = m_ParentRoom.GetNearestAvailableCell(m_Breakable.transform.position, BreakableSize, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, BreakableSize, avoidExitCells));
                    if (!newPosition.HasValue) newPosition = m_ParentRoom.GetNearestAvailableCell(m_Breakable.transform.position, IntVector2.One, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, IntVector2.One, avoidExitCells));
                    m_LastPlayerPosition = newPosition.Value.ToVector2();
                    break;
            }
            parentObject.transform.position = m_LastPlayerPosition;
            m_targetRigidbody.Reinitialize();
            if (m_targetRigidbody.HitboxPixelCollider != null) {
                m_LastPlayerPosition = m_targetRigidbody.HitboxPixelCollider.UnitCenterLeft;
            } else {
                m_LastPlayerPosition = parentObject.transform.position;
            }
            owner.transform.position = m_LastPlayerPosition;
            owner.specRigidbody.Reinitialize();
            m_active = true;
        }
        
        public void PlayerOnEnteredCombat() {
            if (!m_active | m_ParentRoom == null | !owner) return;
            if (!owner.IsStealthed)ExpandUtility.SetPlayerIsStealthed(owner, true, "MrCapMindControl");
            m_IsMovingToNewRoom = CheckForNewRoom(true, true);
            m_IsMovingToNewRoom = true;
            DoReposition(true);
            StartCoroutine(DelayedWallCheck());
            m_IsMovingToNewRoom = false;
        }
        
        private IEnumerator DelayedWallCheck() {
            yield return new WaitForSeconds(0.1f);
            if (!owner | !m_aiActor) yield break;
            if (targetType != TargetType.Hammer) ExpandUtility.CorrectForWalls(m_targetRigidbody, new SpeculativeRigidbody[] { owner.specRigidbody }, true);
            yield break;
        }

        private void AttractEnemies(RoomHandler room) {
            if (!m_targetRigidbody)return;
            List<AIActor> activeEnemies = room.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
            if (activeEnemies != null) {
                for (int i = 0; i < activeEnemies.Count; i++) {
                    if (activeEnemies[i] && activeEnemies[i] != m_aiActor && activeEnemies[i].OverrideTarget == null) activeEnemies[i].OverrideTarget = m_targetRigidbody;
                }
            }
        }

        /*private bool PrimaryBehaviorsReady() {
            if (hasLineOfSightToTargetHook != null) return true;
            if (m_PrimaryBehaviors.Count > 0) {
                foreach (AttackBehaviorBase behavior in m_PrimaryBehaviors) {
                    if (!behavior.IsReady()) return false;
                }
            }
            return true;
        }
        

        private bool SecondaryBehaviorsReady() {
            if (m_SecondaryBehaviors.Count > 0) {
                foreach (AttackBehaviorBase behavior in m_SecondaryBehaviors) {
                    if (!behavior.IsReady()) return false;
                }
            }
            return true;
        }*/
        
        public void Detach(bool killTarget = true) {
            m_active = false;
            IsOnDeath = true;
            ForceLineOfSight = false;
            if (ActAsDecoy)ClearOverrides(m_ParentRoom);
            if (SpriteBobber) SpriteBobber.DestroyBobber();
            switch (targetType) {
                case TargetType.AIActor:
                    if (m_aiActor) {
                        m_aiActor.healthHaver.OnPreDeath -= OnPreDeath;
                        m_aiActor.healthHaver.OnDeath -= OnDeath;
                        if (m_GunHandPostProccessAdded && GunHandGuns != null && GunHandGuns.Length > 0) {
                            foreach (Gun gun in GunHandGuns) gun.PostProcessProjectile -= GunHandPostProcessProjectile;
                        }
                        if (m_targetRigidbody) m_targetRigidbody.OnPreRigidbodyCollision -= EnemyOnPreRigidBodyCollision;
                    }
                    m_PrimaryBehaviors.Clear();
                    m_SecondaryBehaviors.Clear();
                    ToggleLineOfSightHook(true);
                    if (killTarget) {
                        if (m_aiActor && m_aiActor.healthHaver.IsAlive && !KillTargetExceptions.Contains(m_aiActor.EnemyGuid)) {
                            m_aiActor.healthHaver.ForceSetCurrentHealth(0);
                            m_aiActor.healthHaver.IsVulnerable = true;
                            m_aiActor.healthHaver.ApplyDamage(float.MaxValue, Vector2.zero, "Evac", CoreDamageTypes.None, DamageCategory.Normal, true, null, false);
                        } else if (m_aiActor && m_aiActor.healthHaver.IsAlive && !KillTargetExceptions.Contains(m_aiActor.EnemyGuid)) {
                            m_aiActor.behaviorSpeculator.Stun(-1, true);
                            m_TargetLeftAlive = true;
                            if (m_aiActor.aiAnimator && m_aiActor.aiAnimator.facingType != m_PreviousFacingType) {
                                m_aiActor.aiAnimator.facingType = m_PreviousFacingType;
                            }
                        }
                    }
                    break;
                case TargetType.Chest:
                    if (!m_Chest.IsBroken && !m_Chest.IsOpen)m_Chest.RegisterChestOnMinimap(m_ParentRoom);
                    if (m_Chest.majorBreakable) m_Chest.majorBreakable.OnBreak -= OnChestBreak;
                    if (m_targetRigidbody) {
                        m_targetRigidbody.OnPreRigidbodyCollision -= ChestOnPreRigidBodyCollision;
                        m_targetRigidbody.CanBePushed = m_CachedRigidBodyCanBePushed;
                        m_targetRigidbody.CanPush = m_CachedRigidBodyCanPush;
                        m_targetRigidbody.Velocity = Vector2.zero;
                    }
                    if (!m_Chest.IsOpen && !m_Chest.IsBroken) {
                        if (m_ParentRoom != null) {
                            m_ParentRoom.RegisterInteractable(m_Chest);
                        } else {
                            RoomHandler.unassignedInteractableObjects.Remove(m_Chest);
                            RoomHandler.unassignedInteractableObjects.Add(m_Chest);
                        }
                    }
                    break;
                case TargetType.Breakable:
                    if (m_Breakable) m_Breakable.OnBreak -= BreakableOnBreak;
                    if (m_targetRigidbody) {
                        m_targetRigidbody.OnPreRigidbodyCollision -= BreakableOnPreRigidBodyCollision;
                        m_targetRigidbody.CanBePushed = m_CachedRigidBodyCanBePushed;
                        m_targetRigidbody.CanPush = m_CachedRigidBodyCanPush;
                        m_targetRigidbody.Velocity = Vector2.zero;
                    }
                    if (!m_Breakable.IsDestroyed && !m_Breakable.TemporarilyInvulnerable && !m_Breakable.GetComponent<ExpandFakeChest>()) {
                        m_Breakable.Break(Vector2.zero);
                    } else if (!m_Breakable.IsDestroyed && parentObject.GetComponent<ExpandFakeChest>()) {
                        ExpandFakeChest m_FakeChest = parentObject.GetComponent<ExpandFakeChest>();
                        if (m_FakeChest && !m_FakeChest.Opened && !m_FakeChest.IsBroken) {
                            m_FakeChest.RegisterFakeChestOnMinimap(m_ParentRoom);
                            m_ParentRoom.RegisterInteractable(m_FakeChest);
                        }
                    }
                    break;
                case TargetType.Hammer:
                    if (m_Hammer) {
                        GameObject hammerHatRemoveVFX = new GameObject("Expand Hammer Mirror Child 2", new Type[] { typeof(ExpandSpriteMirror) }) { layer = parentObject.layer };
                        ExpandSpriteMirror hammerMirror = hammerHatRemoveVFX.AddComponent<ExpandSpriteMirror>();
                        if (m_Hammer.renderer.enabled && m_Hammer.State != ExpandForgeHammerComponent.ExpandHammerState.Gone |
                            m_Hammer.State != ExpandForgeHammerComponent.ExpandHammerState.PreSwing |
                            m_Hammer.State != ExpandForgeHammerComponent.ExpandHammerState.InitialDelay
                            ) {
                            if (m_Hammer.sprite.GetCurrentSpriteDef().name.Contains("_right_")) {
                                hammerMirror.AttachMirror(m_Hammer.transform, ExpandObjectDatabase.ForgeHammer.GetComponent<tk2dSprite>().Collection, new Vector3(-3.8f, -2.6f), true);
                            } else {
                                hammerMirror.AttachMirror(m_Hammer.transform, ExpandObjectDatabase.ForgeHammer.GetComponent<tk2dSprite>().Collection, new Vector3(-2.8f, -2.6f), true);
                            }
                        }
                        if (owner)owner.OnRoomClearEvent -= HandleOnRoomCleared;
                        m_Hammer.Deactivate();
                    }
                    break;

            }
            if (owner) {
                owner.specRigidbody.OnPreRigidbodyCollision -= PlayerOnPreRigidBodyCollision;
                owner.specRigidbody.RegisterGhostCollisionException(m_targetRigidbody);
                if (AttachPlayer) {
                    GameManager.Instance.MainCameraController.SetManualControl(false, false);
                    GameObject poofObject = (GameObject)Instantiate(ResourceCache.Acquire("Global VFX/Spinfall_Poof_VFX"));
                    tk2dBaseSprite poofSprite = poofObject.GetComponent<tk2dBaseSprite>();
                    poofSprite.PlaceAtPositionByAnchor(owner.sprite.WorldCenter.ToVector3ZUp(0f) + new Vector3(0f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                    poofSprite.HeightOffGround = 5f;
                    poofSprite.UpdateZDepth();
                    if (parentObject) BraveTime.ClearMultiplier(parentObject);
                    Vector2 m_cachedPosition = owner.transform.position;
                    if (ReflectGetField<Vector2>(typeof(GameActor), "m_cachedPosition", owner) != null) {
                        m_cachedPosition = ReflectGetField<Vector2>(typeof(GameActor), "m_cachedPosition", owner);
                    }
                    m_cachedPosition = m_cachedPosition.ToVector3ZUp(owner.transform.position.z);
                    IntVector2? m_SafePosition = null;
                    if (m_ParentRoom != null) m_SafePosition = m_ParentRoom.GetNearestAvailableCell(m_cachedPosition, m_CachedOwnerSize, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, m_CachedOwnerSize));
                    if (m_SafePosition.HasValue) {
                        m_LastPlayerPosition = m_SafePosition.Value.ToVector2();
                    } else {
                        m_LastPlayerPosition = m_cachedPosition;
                    }
                    owner.transform.position = m_LastPlayerPosition;
                    owner.specRigidbody.Reinitialize();
                    owner.SetIsFlying(false, "MrCap Mind Control", false, false);
                    owner.IsVisible = true;
                    owner.IsGone = false;
                    owner.healthHaver.IsVulnerable = true;
                    owner.IsGunLocked = false;
                    owner.ClearInputOverride("MrCap Mind Control");
                    ExpandUtility.TriggerInvulnerableFrames(owner, true, 1);
                    if (targetType != TargetType.Hammer)owner.OnEnteredCombat = (Action)Delegate.Remove(owner.OnEnteredCombat, new Action(PlayerOnEnteredCombat));
                    // owner.SetIsStealthed(false, "MrCapMindControl");
                    ExpandUtility.SetPlayerIsStealthed(owner, false, "MrCapMindControl");
                    canTeleportFromRoomHook.Dispose();
                    canTeleportFromRoomHook = null;
                } else {
                    // owner.SetIsStealthed(false, "MrCapMindControl");
                    ExpandUtility.SetPlayerIsStealthed(owner, false, "MrCapMindControl");
                    owner.IsGunLocked = false;
                }
                BraveInput input = BraveInput.GetInstanceForPlayer(owner.PlayerIDX);
                GungeonActions activeActions = input.ActiveActions;
                if (input && activeActions != null && (activeActions.ShootAction.WasPressed | activeActions.ShootAction.WasPressedRepeating))input.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                owner.SetCapableOfStealing(false, "MrCapMineControl");
            }
            if (attachedHat) {
                if (sprite)sprite.DetachRenderer(attachedHat.sprite);
                Destroy(attachedHat.gameObject);
            }
            ExpandTheGungeon.MrCapInUse = false;
            Destroy(this);
        }
                        
        protected override void OnDestroy() {
            if (GameManager.IsShuttingDown) return;
            if (!owner) {
                base.OnDestroy();
                return;
            }
            if (!IsOnDeath) {
                if (MrCapItem && MrCapItem.CurrentMindControl && MrCapItem.CurrentMindControl == this) {
                    MrCapItem.DoDetach(false);
                } else {
                    Detach(false);
                }
            }
            base.OnDestroy();
        }
    }
}

