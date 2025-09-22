using Dungeonator;
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

        public static Hook canTeleportFromRoomHook;

        public bool CanTeleportFromRoomHook(Func<RoomHandler, bool> orig, RoomHandler self) {
            if (BlockTeleport) return false;
            return orig(self);
        }

        public ExpandHatMindController() {
            m_attackedThisCycle = true;
            UseFakeActorTarget = true;
            UsesCable = false;
            ActAsDecoy = false;
            AttachPlayer = true;
            targetType = TargetType.AIActor;
            KillTargetExceptions = new List<string>() { "98ea2fe181ab4323ab6e9981955a9bca", "21dd14e5ca2a4a388adab5b11b69a1e1" };
            AttachFailed = false;
            MediumObjectMovementSpeed = 1.2f;
            m_IsMovingToNewRoom = false;

            m_active = false;
            IsOnDeath = false;
            m_TargetLeftAlive = false;
            m_UsedSecondaryAttackThisCycle = false;
            m_DecoyTimer = 1;
            m_SecondaryCooldown = 0;
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
        public ExpandSpriteBobber SpriteBobber;

        public TargetType targetType;


        public enum TargetType { AIActor, Chest, Other }


        public List<string> KillTargetExceptions;

        private bool m_attackedThisCycle;
        private bool m_active;
        
        private bool m_TargetLeftAlive;
        private GameObject parentObject;
        private RoomHandler m_ParentRoom;
        private AIActor m_aiActor;
        private Chest m_Chest;
        private BehaviorSpeculator m_behaviorSpeculator;
        private NonActor m_fakeActor;
        private SpeculativeRigidbody m_fakeTargetRigidbody;
        private SpeculativeRigidbody m_targetRigidbody;
        private ArbitraryCableDrawer m_cable;
        private float m_DecoyTimer;
        private float m_SecondaryCooldown;
        private Vector2 m_LastPlayerPosition;

        private bool m_CachedRigidBodyCanPush;
        private bool m_CachedRigidBodyCanBePushed;
        private bool m_IsMovingToNewRoom;
        private bool m_UsedSecondaryAttackThisCycle;
        private IntVector2 m_CachedOwnerSize;

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
                    m_ParentRoom = m_aiActor.ParentRoom;
                    m_behaviorSpeculator = m_aiActor.behaviorSpeculator;
                    HatPosition = (m_aiActor.sprite.GetBounds().size.y - (MrCap.MrCapVFX.GetComponent<tk2dSprite>().GetBounds().size.y / 2.1f));
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
                    break;
                case TargetType.Chest:
                    if (!parentObject | !parentObject.GetComponent<Chest>()) { AttachFailed = true; return; }
                    m_Chest = parentObject.GetComponent<Chest>();
                    m_ParentRoom = ReflectGetField<RoomHandler>(typeof(Chest), "m_room", m_Chest);
                    if (m_ParentRoom == null)parentObject.transform.position.GetAbsoluteRoom();
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
                        GameObject EXSpriteBobber = new GameObject("EXSpriteBobber") { layer = 22 };
                        SpriteBobber = EXSpriteBobber.AddComponent<ExpandSpriteBobber>();
                        SpriteBobber.AttachBobber(parentObject.transform, null, false, false);
                    }
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
                owner.OnEnteredCombat = (Action)Delegate.Combine(owner.OnEnteredCombat, new Action(PlayerOnEnteredCombat));
                m_LastPlayerPosition = parentTarget.transform.position;
                if (owner.CurrentRoom?.area?.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS) {
                    GameManager.Instance.MainCameraController.SetManualControl(true, true);
                    GameManager.Instance.MainCameraController.OverrideRecoverySpeed = 12;
                    GameManager.Instance.MainCameraController.OverridePosition = parentObject.transform.position;
                }
                owner.transform.position = m_LastPlayerPosition;
                owner.specRigidbody.Reinitialize();
                if (owner.CurrentRoom?.area?.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS)BraveTime.RegisterTimeScaleMultiplier(0.01f, parentTarget);
                owner.SetInputOverride("MrCap Mind Control");
                // if (targetType != TargetType.AIActor) owner.SetIsStealthed(true, "MrCapMindControl");
                ExpandUtility.SetPlayerIsStealthed(owner, true, "MrCapMindControl");
                if (owner.CurrentRoom?.area?.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS)StartCoroutine(DelayedCameraReposition(parentTarget.transform.position));
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
        
        private Vector2 GetPlayerAimPointController(Vector2 aimBase, Vector2 aimDirection) {
            Func<SpeculativeRigidbody, bool> rigidbodyExcluder = (SpeculativeRigidbody otherRigidbody) => otherRigidbody.minorBreakable && !otherRigidbody.minorBreakable.stopsBullets;
            Vector2 result = aimBase + aimDirection * 10f;
            CollisionLayer layer = CollisionLayer.EnemyHitBox;
            int rayMask = CollisionMask.LayerToMask(CollisionLayer.HighObstacle, CollisionLayer.BulletBlocker, layer, CollisionLayer.BulletBreakable);
            RaycastResult raycastResult;
            if (PhysicsEngine.Instance.Raycast(aimBase, aimDirection, 50f, out raycastResult, true, true, rayMask, null, false, rigidbodyExcluder, null)) {
                result = aimBase + aimDirection * raycastResult.Distance;
            }
            RaycastResult.Pool.Free(ref raycastResult);
            return result;
        }

        private void UpdateAimTargetPosition() {
            if (!owner | !UseFakeActorTarget) return;
            PlayerController playerController = owner;
            BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(playerController.PlayerIDX);
            GungeonActions activeActions = instanceForPlayer.ActiveActions;
            if (instanceForPlayer.IsKeyboardAndMouse(false)) {
                m_fakeTargetRigidbody.transform.position = playerController.unadjustedAimPoint.XY();
            } else {
                m_fakeTargetRigidbody.transform.position = GetPlayerAimPointController(playerController.CenterPosition, activeActions.Aim.Vector);
            }
            m_fakeTargetRigidbody.Reinitialize();
        }

        private Vector2? UpdateAimTarget(PlayerController player) {
            if (!player) return null;
            BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(player.PlayerIDX);
            GungeonActions activeActions = instanceForPlayer.ActiveActions;
            if (instanceForPlayer.IsKeyboardAndMouse(false)) {
                return player.unadjustedAimPoint.XY();
            } else {
                return GetPlayerAimPointController(player.CenterPosition, activeActions.Aim.Vector);
            }
        }

        private void ClearOverrides(RoomHandler room) {
            List<AIActor> activeEnemies = room.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
            if (activeEnemies != null) {
                for (int i = 0; i < activeEnemies.Count; i++) {
                    if (activeEnemies[i] && activeEnemies[i] != m_aiActor && activeEnemies[i].OverrideTarget == m_aiActor.specRigidbody) activeEnemies[i].OverrideTarget = null;
                }
            }
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
        
        private void UpdateActions() {
            if (!m_active | !owner | !m_targetRigidbody | IsOnDeath | m_TargetLeftAlive | m_IsMovingToNewRoom) return;
            BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(owner.PlayerIDX);
            if (!instanceForPlayer) return;
            GungeonActions activeActions = instanceForPlayer.ActiveActions;
            
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
                    if (m_behaviorSpeculator) {
                        bool WeaponIsFullyAuto = (m_aiActor.aiShooter && m_aiActor.aiShooter.CurrentGun && m_aiActor.aiShooter.CurrentGun.ClipCapacity > 1 && m_aiActor.aiShooter.CurrentGun.gunClass != GunClass.SHOTGUN);
                        bool usedSecondaryAttack = (owner.AcceptingNonMotionInput && activeActions.DodgeRollAction.WasPressed);
                        if (m_behaviorSpeculator.AttackCooldown <= 0f) {
                            if (!m_attackedThisCycle && m_behaviorSpeculator.ActiveContinuousAttackBehavior != null) {
                                m_attackedThisCycle = true;
                            }
                            if (m_attackedThisCycle && m_behaviorSpeculator.ActiveContinuousAttackBehavior == null) {
                                m_behaviorSpeculator.AttackCooldown = float.MaxValue;
                            }
                        } else if (WeaponIsFullyAuto && activeActions.ShootAction.WasPressedRepeating) {
                            m_attackedThisCycle = false;
                            m_behaviorSpeculator.AttackCooldown = 0f;
                            if (owner.IsStealthed) ExpandUtility.SetPlayerIsStealthed(owner, false, "MrCapMindControl");
                        } else if (!WeaponIsFullyAuto && owner.AcceptingNonMotionInput && activeActions.ShootAction.WasPressed) {
                            m_attackedThisCycle = false;
                            m_behaviorSpeculator.AttackCooldown = 0f;
                            if (owner.IsStealthed) ExpandUtility.SetPlayerIsStealthed(owner, false, "MrCapMindControl");
                        } else if (usedSecondaryAttack && owner.AcceptingNonMotionInput && !m_UsedSecondaryAttackThisCycle && !m_attackedThisCycle) {
                            m_UsedSecondaryAttackThisCycle = true;
                        }

                        if (usedSecondaryAttack && owner.AcceptingNonMotionInput && owner.IsStealthed) {
                            ExpandUtility.SetPlayerIsStealthed(owner, false, "MrCapMindControl");
                        }

                        if (m_UsedSecondaryAttackThisCycle && owner.AcceptingNonMotionInput) {
                            m_SecondaryCooldown -= BraveTime.DeltaTime;
                            if (m_SecondaryCooldown <= 0) {
                                m_SecondaryCooldown = 1;
                                m_UsedSecondaryAttackThisCycle = false;
                            }
                        }

                        if (m_behaviorSpeculator.TargetBehaviors != null && m_behaviorSpeculator.TargetBehaviors.Count > 0) {
                            m_behaviorSpeculator.TargetBehaviors.Clear();
                        }
                        if (m_behaviorSpeculator.MovementBehaviors != null && m_behaviorSpeculator.MovementBehaviors.Count > 0) {
                            for (int i = 0; i < m_behaviorSpeculator.MovementBehaviors.Count; i++) {
                                if (m_behaviorSpeculator.MovementBehaviors[i] is TakeCoverBehavior) {
                                    TakeCoverBehavior m_takeCover = m_behaviorSpeculator.MovementBehaviors[i] as TakeCoverBehavior;
                                    m_takeCover.InitialCoverChance = 0;
                                    InvokeMethod(typeof(TakeCoverBehavior), "BecomeDisinterested", m_takeCover, new object[] { ReflectGetField<int>(typeof(TakeCoverBehavior), "m_tableQuadrant", m_takeCover) });
                                }
                            }
                            m_behaviorSpeculator.MovementBehaviors.Clear();
                        }

                        if (!m_UsedSecondaryAttackThisCycle)usedSecondaryAttack = activeActions.DodgeRollAction.WasPressed;
                        m_UsedSecondaryAttackThisCycle = usedSecondaryAttack;
                        if (m_behaviorSpeculator.AttackBehaviors != null) {
                            for (int i = 0; i < m_behaviorSpeculator.AttackBehaviors.Count; i++) {
                                AttackBehaviorBase attack = m_behaviorSpeculator.AttackBehaviors[i];
                                ProcessAttackAIActor(attack);
                            }
                        }

                        OverridableBool m_IsImmobile = ReflectGetField<OverridableBool>(typeof(KnockbackDoer), "m_isImmobile", m_aiActor.knockbackDoer);
                        if (!m_IsImmobile.Value) m_aiActor.ImpartedVelocity += activeActions.Move.Value * m_aiActor.MovementSpeed;
                    }
                break;
                case TargetType.Chest:
                    if (m_IsMovingToNewRoom) {
                        if (m_targetRigidbody.Velocity != Vector2.zero)m_targetRigidbody.Velocity = Vector2.zero;
                        return;
                    }
                    if (m_Chest && !m_Chest.IsBroken) {
                        if (activeActions.Move.Value != Vector2.zero) {
                            m_targetRigidbody.Velocity += activeActions.Move.Value * MediumObjectMovementSpeed;
                        } else {
                            m_targetRigidbody.Velocity = Vector2.zero;
                        }
                    }
                    if (owner && owner.AcceptingNonMotionInput && activeActions.ShootAction.WasPressed) {
                        if (m_Chest && !m_Chest.IsOpen && !m_Chest.IsBroken) {
                            if (m_Chest.IsLocked)m_Chest.ForceUnlock();
                            m_Chest.ForceOpen(owner);
                            if (attachedHat) attachedHat.transform.localPosition += new Vector3(0, attachedHat.sprite.GetBounds().size.y / 1.5f, 0);
                            instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                            if (m_Chest.IsGlitched)OnPreDeath(Vector2.zero);
                        }
                    }
                    break;
                case TargetType.Other:
                    // Not Implemented Yet.
                break;
            }
        }


        private void Update() {
            if (!m_active | !owner | !m_targetRigidbody | IsOnDeath | m_TargetLeftAlive | m_IsMovingToNewRoom) return;
            if (owner.CurrentRoom != m_ParentRoom) m_ParentRoom = owner.CurrentRoom;
            if (AttachPlayer) {
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
                        if (room.IsSealed && room != m_ParentRoom) {
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
            if (!m_active | !owner | !m_targetRigidbody | m_TargetLeftAlive | m_IsMovingToNewRoom) return;
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
                }
            }
            owner.transform.position = m_LastPlayerPosition;
            owner.specRigidbody.Reinitialize();
            owner.healthHaver.IsVulnerable = false;
            owner.IsVisible = false;
            if (owner.IsInMinecart && owner.currentMineCart) owner.currentMineCart.EvacuateSpecificPlayer(owner);
        }
        
        private bool CheckForNewRoom(bool skipReposition, bool avoidExitCells) {
            if (!AttachPlayer) return false;
            
            RoomHandler m_RoomChecked = owner.gameObject.transform.position.GetAbsoluteRoom();
            if (m_RoomChecked == null) m_RoomChecked = owner.CurrentRoom;

            if (m_RoomChecked != null && m_ParentRoom != m_RoomChecked) {
                m_ParentRoom = m_RoomChecked;
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
                    IntVector2 AIActorSize = new IntVector2(m_aiActor.GetWidth() + 1, m_aiActor.GetHeight() + 1);
                    newPosition = m_ParentRoom.GetNearestAvailableCell(owner.transform.position, AIActorSize, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, AIActorSize, avoidExitCells));
                    if (!newPosition.HasValue)newPosition = m_ParentRoom.GetNearestAvailableCell(owner.transform.position, IntVector2.One, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, IntVector2.One, avoidExitCells));
                    if (!newPosition.HasValue)newPosition = m_ParentRoom.GetBestRewardLocation(AIActorSize, RoomHandler.RewardLocationStyle.PlayerCenter, false);
                    m_LastPlayerPosition = newPosition.Value.ToVector2();
                    break;
                case TargetType.Chest:
                    IntVector2 ActorSize = (new IntVector2(m_Chest.GetWidth(), m_Chest.GetHeight()));
                    newPosition = m_ParentRoom.GetNearestAvailableCell(owner.transform.position, ActorSize, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, ActorSize, avoidExitCells));
                    if (!newPosition.HasValue) newPosition = m_ParentRoom.GetNearestAvailableCell(owner.transform.position, IntVector2.One, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, IntVector2.One, avoidExitCells));
                    if (!newPosition.HasValue) newPosition = m_ParentRoom.GetBestRewardLocation(ActorSize, RoomHandler.RewardLocationStyle.PlayerCenter, false);
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
            m_IsMovingToNewRoom = false;
        }
        

        private void ProcessAttackAIActor(AttackBehaviorBase attack) {
            if (attack == null)return;
            if (attack is BasicAttackBehavior) {
                if (m_UsedSecondaryAttackThisCycle) {
                    m_behaviorSpeculator.AttackCooldown = 0;
                } else {
                    BasicAttackBehavior basicAttackBehavior = attack as BasicAttackBehavior;
                    basicAttackBehavior.IgnoreGlobalCooldown();
                    basicAttackBehavior.Cooldown = 0f;
                    basicAttackBehavior.RequiresLineOfSight = false;
                    basicAttackBehavior.MinRange = -1f;
                    basicAttackBehavior.Range = -1f;
                    if (attack is LeapExplosion) {
                        LeapExplosion leapExploder = attack as LeapExplosion;
                        leapExploder.minLeapDistance = 0;
                        leapExploder.leapDistance = 1000f;
                    }
                    if (basicAttackBehavior is ShootGunBehavior) {
                        ShootGunBehavior shootGunBehavior = basicAttackBehavior as ShootGunBehavior;
                        shootGunBehavior.LineOfSight = false;
                        shootGunBehavior.EmptiesClip = false;
                        shootGunBehavior.RespectReload = false;
                    }
                    if (basicAttackBehavior is GunHandBasicShootBehavior) {
                        GunHandBasicShootBehavior shootDualGunBehavior = basicAttackBehavior as GunHandBasicShootBehavior;
                        shootDualGunBehavior.LineOfSight = false;
                        foreach (GunHandController gunHand in shootDualGunBehavior.GunHands) {
                            gunHand.Cooldown = 1;
                        }
                    }
                }
                if ((attack is TeleportBehavior)) {
                    TeleportBehavior teleportAttack = (attack as TeleportBehavior);
                    if (m_UsedSecondaryAttackThisCycle) {
                        teleportAttack.RequiresLineOfSight = false;
                        teleportAttack.OnlyTeleportIfPlayerUnreachable = false;
                        teleportAttack.AttackCooldown = 0;
                        teleportAttack.AllowCrossRoomTeleportation = true;
                        teleportAttack.MinDistanceFromPlayer = -1;
                        teleportAttack.MaxDistanceFromPlayer = -1;
                        teleportAttack.MinRange = -1;
                        teleportAttack.Range = -1;
                        teleportAttack.GroupCooldown = 0;
                        teleportAttack.Cooldown = 0;
                    } else {
                        teleportAttack.IgnoreGlobalCooldown();
                        teleportAttack.MaxEnemiesInRoom = 0;
                        teleportAttack.StayOnScreen = false;
                        teleportAttack.InitialCooldown = 0;
                        teleportAttack.AttackCooldown = 0;
                        teleportAttack.GroupCooldown = 0;
                        teleportAttack.RequiresLineOfSight = true;
                        teleportAttack.MinRange = 1000f;
                        teleportAttack.Range = 0.1f;
                    }
                }
            } else if (attack is AttackBehaviorGroup) {
                AttackBehaviorGroup attackBehaviorGroup = attack as AttackBehaviorGroup;
                for (int i = 0; i < attackBehaviorGroup.AttackBehaviors.Count; i++) {
                    ProcessAttackAIActor(attackBehaviorGroup.AttackBehaviors[i].Behavior);
                }
            }
        }
        
        public void PlayerOnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (otherRigidbody == m_targetRigidbody | (UseFakeActorTarget && otherRigidbody == m_fakeTargetRigidbody) |
                otherRigidbody.gameObject.GetComponent<Projectile>() | (otherPixelCollider.IsTrigger && !otherRigidbody.gameObject.GetComponent<PickupObject>()) |
                otherPixelCollider.CollisionLayer == CollisionLayer.Trap
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
                otherRigidbody.GetComponent<PickupObject>()
                ) {
                PhysicsEngine.SkipCollision = true;
            }
        }
        
        public void Detach(bool killTarget = true) {
            m_active = false;
            IsOnDeath = true;
            if (ActAsDecoy)ClearOverrides(m_ParentRoom);
            if (SpriteBobber) SpriteBobber.DestroyBobber();
            switch (targetType) {
                case TargetType.AIActor:
                    if (m_aiActor) {
                        m_aiActor.healthHaver.OnPreDeath -= OnPreDeath;
                        m_aiActor.healthHaver.OnDeath -= OnDeath;
                        if (m_targetRigidbody) m_targetRigidbody.OnPreRigidbodyCollision -= EnemyOnPreRigidBodyCollision;
                    }
                    if (killTarget) {
                        if (m_aiActor && m_aiActor.healthHaver.IsAlive && !KillTargetExceptions.Contains(m_aiActor.EnemyGuid)) {
                            m_aiActor.healthHaver.ForceSetCurrentHealth(0);
                            m_aiActor.healthHaver.IsVulnerable = true;
                            m_aiActor.healthHaver.ApplyDamage(100000f, Vector2.zero, "Evac", CoreDamageTypes.None, DamageCategory.Normal, true, null, false);
                        } else if (m_aiActor && m_aiActor.healthHaver.IsAlive && !KillTargetExceptions.Contains(m_aiActor.EnemyGuid)) {
                            m_aiActor.behaviorSpeculator.Stun(-1, true);
                            m_TargetLeftAlive = true;
                        }
                    }
                break;
                case TargetType.Chest:
                    if (!m_Chest.IsBroken && !m_Chest.IsOpen)m_Chest.RegisterChestOnMinimap(m_ParentRoom);
                    if (m_targetRigidbody) m_targetRigidbody.OnPreRigidbodyCollision -= ChestOnPreRigidBodyCollision;
                    if (m_Chest.majorBreakable) m_Chest.majorBreakable.OnBreak -= OnChestBreak;
                    m_targetRigidbody.CanBePushed = m_CachedRigidBodyCanBePushed;
                    m_targetRigidbody.CanPush = m_CachedRigidBodyCanPush;
                    m_targetRigidbody.Velocity = Vector2.zero;
                    if (!m_Chest.IsOpen && !m_Chest.IsBroken) {
                        if (m_ParentRoom != null) {
                            m_ParentRoom.RegisterInteractable(m_Chest);
                        } else {
                            RoomHandler.unassignedInteractableObjects.Remove(m_Chest);
                            RoomHandler.unassignedInteractableObjects.Add(m_Chest);
                        }
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
                    owner.OnEnteredCombat = (Action)Delegate.Remove(owner.OnEnteredCombat, new Action(PlayerOnEnteredCombat));
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
            /*if (MrCapItem) {
                MrCapItem.ResetHat(attachedHat.gameObject, gameObject);
            } else {
                if (attachedHat) Destroy(attachedHat.gameObject);
                Destroy(this);
            }*/
            if (attachedHat) {
                if (sprite)sprite.DetachRenderer(attachedHat.sprite);
                Destroy(attachedHat.gameObject);
            }
            ExpandTheGungeon.MrCapInUse = false;
            Destroy(this);
        }

                
        protected override void OnDestroy() {
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

