using Dungeonator;
using ExpandTheGungeon.ItemAPI;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExpandTheGungeon.ExpandUtilities.ReflectionHelpers;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandMrCapMindController : BraveBehaviour {

        public ExpandMrCapMindController() {
            m_attackedThisCycle = true;
            UseFakeActorTarget = true;
            UsesCable = false;
            ActAsDecoy = false;
            AttachPlayer = true;
            targetType = TargetType.AIActor;
            m_active = false;
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

        public TargetType targetType;


        public enum TargetType { AIActor, Other }

        private bool m_attackedThisCycle;
        private bool m_active;
        private GameObject parentObject;
        private RoomHandler m_ParentRoom;
        private AIActor m_aiActor;
        private BehaviorSpeculator m_behaviorSpeculator;
        private NonActor m_fakeActor;
        private SpeculativeRigidbody m_fakeTargetRigidbody;
        private SpeculativeRigidbody m_targetRigidbody;
        private ArbitraryCableDrawer m_cable;
        private GameObject m_overheadVFX;
        private float m_DecoyTimer;
        private Vector2 m_LastPlayerPosition;

        private CellValidator m_cellValidator(DungeonData data, IntVector2 Clearence) {
            return delegate (IntVector2 c) {
                for (int X = 0; X < Clearence.x; X++) {
                    for (int Y = 0; Y < Clearence.y; Y++) {
                        if (!data.CheckInBoundsAndValid(c.x + X, c.y + Y)) return false;
                        if (data[c.x + X, c.y + Y].type == CellType.PIT) return false;
                        if (data[c.x + X, c.y + Y].isOccupied) return false;
                        if (data[c.x + X, c.y + Y].type == CellType.WALL) return false;
                        if (data[c.x + X, c.y + Y].IsLowerFaceWall()) return false;
                        if (data[c.x + X, c.y + Y].IsTopWall()) return false;
                    }
                }
                return true;
            };
        }

        public void Init(PlayerController parentPlayer, GameObject parentTarget) {
            if (!parentPlayer | !parentTarget) return;
            m_targetRigidbody = parentTarget.GetComponent<SpeculativeRigidbody>();
            parentObject = parentTarget;
            if (!m_targetRigidbody) return;
            owner = parentPlayer;
            if (AttachPlayer) {
                GameObject poofObject = (GameObject)Instantiate(ResourceCache.Acquire("Global VFX/Spinfall_Poof_VFX"));
                tk2dBaseSprite poofSprite = poofObject.GetComponent<tk2dBaseSprite>();
                poofSprite.PlaceAtPositionByAnchor(owner.sprite.WorldCenter.ToVector3ZUp(0f) + new Vector3(0f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                poofSprite.HeightOffGround = 5f;
                poofSprite.UpdateZDepth();
            }
            switch (targetType) {
                case TargetType.AIActor:
                    m_aiActor = parentObject.GetComponent<AIActor>();
                    if (!m_aiActor) return;
                    m_ParentRoom = m_aiActor.ParentRoom;
                    m_behaviorSpeculator = m_aiActor.behaviorSpeculator;
                    if (UseFakeActorTarget) {
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
                    if (UsesCable) {
                        m_cable = m_aiActor.gameObject.AddComponent<ArbitraryCableDrawer>();
                        m_cable.Attach1Offset = owner.CenterPosition - owner.transform.position.XY();
                        m_cable.Attach2Offset = m_aiActor.CenterPosition - m_aiActor.transform.position.XY();
                        m_cable.Initialize(owner.transform, m_aiActor.transform);
                    }
                    m_overheadVFX = m_aiActor.PlayEffectOnActor((GameObject)ResourceCache.Acquire("Global VFX/VFX_Controller_Status"), new Vector3(0f, m_targetRigidbody.HitboxPixelCollider.UnitDimensions.y, 0f), true, false, true);
                    m_targetRigidbody.AddCollisionLayerOverride(CollisionMask.LayerToMask(CollisionLayer.Projectile));
                    // m_aiActor.healthHaver.OnPreDeath += OnDeath;
                    m_aiActor.HitByEnemyBullets = true;
                    m_aiActor.CanTargetEnemies = true;
                    m_aiActor.OverrideHitEnemies = true;
                    break;
                case TargetType.Other:
                    break; // Not implemented yet
            }
            if (m_ParentRoom == null)m_ParentRoom = parentObject.transform.position.GetAbsoluteRoom();
            if (AttachPlayer) {
                if (m_targetRigidbody)m_targetRigidbody.OnPreRigidbodyCollision += EnemyOnPreRigidBodyCollision;
                if (owner.IsInMinecart && owner.currentMineCart) owner.currentMineCart.EvacuateSpecificPlayer(owner);
                owner.IsVisible = false;
                owner.IsGone = true;
                owner.SetIsFlying(true, "MrCap Mind Control", false, false);
                owner.healthHaver.IsVulnerable = false;
                owner.specRigidbody.OnPreRigidbodyCollision += PlayerOnPreRigidBodyCollision;
            } else {
                owner.SetIsStealthed(true, "MrCapMindControl");
            }
            owner.IsGunLocked = true;
            if (AttachPlayer) {
                owner.SetInputOverride("MrCap Mind Control");
                m_LastPlayerPosition = parentTarget.transform.position;
                if (owner.CurrentRoom?.area?.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS) {
                    GameManager.Instance.MainCameraController.SetManualControl(true, true);
                    GameManager.Instance.MainCameraController.OverrideRecoverySpeed = 12;
                    GameManager.Instance.MainCameraController.OverridePosition = parentObject.transform.position;
                }
                owner.transform.position = m_LastPlayerPosition;
                owner.specRigidbody.Reinitialize();
                BraveTime.RegisterTimeScaleMultiplier(0.1f, parentTarget);
                StartCoroutine(DelayedCameraReposition(parentTarget.transform.position));
            } else {
                m_LastPlayerPosition = owner.transform.position;
            }
            m_active = true;
        }

        private IEnumerator DelayedCameraReposition(Vector3 targetPosition) {
            while (Vector2.Distance(GameManager.Instance.MainCameraController.transform.position.XY(), targetPosition) > 0.5f)yield return null;
            GameManager.Instance.MainCameraController.OverrideRecoverySpeed = 8;
            GameManager.Instance.MainCameraController.SetManualControl(false, true);
            yield return null;
            BraveTime.ClearMultiplier(parentObject);
            owner.ClearInputOverride("MrCap Mind Control");
            yield break;
        }

        private void OnDeath() {
            m_active = false;
            if (AttachPlayer) {
                GameObject poofObject = (GameObject)Instantiate(ResourceCache.Acquire("Global VFX/Spinfall_Poof_VFX"));
                tk2dBaseSprite poofSprite = poofObject.GetComponent<tk2dBaseSprite>();
                poofSprite.PlaceAtPositionByAnchor(owner.sprite.WorldCenter.ToVector3ZUp(0f) + new Vector3(0f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                poofSprite.HeightOffGround = 5f;
                poofSprite.UpdateZDepth();
            } else {
                owner.SetIsStealthed(false, "MrCapMindControl");
            }
            owner.IsGunLocked = false;
            GameManager.Instance.MainCameraController.SetManualControl(false, true);
            if (ActAsDecoy)ClearOverrides(m_ParentRoom);
            if (MrCapItem) {
                MrCapItem.InUse = false;
                // MrCapItem.ClearCooldowns();
            }
        }

        public void Detach() {
            m_active = false;
            switch (targetType) {
                case TargetType.AIActor:
                    // if (m_aiActor) m_aiActor.healthHaver.OnPreDeath -= OnPreDeath;
                    if (m_aiActor && m_aiActor.healthHaver.IsAlive) {
                        m_aiActor.healthHaver.ForceSetCurrentHealth(0);
                        m_aiActor.healthHaver.IsVulnerable = true;
                        m_aiActor.healthHaver.ApplyDamage(100000f, Vector2.zero, "Evac", CoreDamageTypes.None, DamageCategory.Normal, true, null, false);
                    }
                    break;
            }
            Destroy(this);
        }

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
        
        private void Update() {
            if (!m_active | !owner | !m_targetRigidbody) return;
            if (ActAsDecoy) {
                m_DecoyTimer -= BraveTime.DeltaTime;
                if (m_DecoyTimer <= 0) {
                    m_DecoyTimer = 1;
                    if (m_ParentRoom != null) AttractEnemies(m_ParentRoom);
                }
            }
            switch (targetType) {
                case TargetType.AIActor: { 
                    if (!m_aiActor) return;
                    if (UseFakeActorTarget) m_fakeActor.specRigidbody = m_fakeTargetRigidbody;
                    if (m_aiActor) {
                        m_aiActor.CanTargetEnemies = true;
                        m_aiActor.CanTargetPlayers = false;
                        if (UseFakeActorTarget) {
                            m_aiActor.PlayerTarget = m_fakeActor;
                            // m_aiActor.OverrideTarget = m_fakeTargetRigidbody;
                        } else {
                            m_aiActor.PlayerTarget = null;
                            // m_aiActor.OverrideTarget = null;
                        }
                        m_aiActor.IgnoreForRoomClear = true;
                        if (UseFakeActorTarget) UpdateAimTargetPosition();
                        if (m_aiActor.aiShooter) {
                            if (UseFakeActorTarget) {
                                m_aiActor.aiShooter.AimAtPoint(m_behaviorSpeculator.PlayerTarget.CenterPosition);
                            } else if (UpdateAimTarget(owner).HasValue) {
                                m_aiActor.aiShooter.AimAtPoint(UpdateAimTarget(owner).Value);
                            }
                        }
                    }
                    if (m_behaviorSpeculator) {
                        m_aiActor.ClearPath();
                        PlayerController playerController = owner;
                        BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(playerController.PlayerIDX);
                        GungeonActions activeActions = instanceForPlayer.ActiveActions;
                        bool WeaponIsFullyAuto = (m_aiActor.aiShooter && m_aiActor.aiShooter.CurrentGun && m_aiActor.aiShooter.CurrentGun.ClipCapacity > 1 && m_aiActor.aiShooter.CurrentGun.gunClass != GunClass.SHOTGUN);
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
                        } else if (!WeaponIsFullyAuto && activeActions.ShootAction.WasPressed) {
                            m_attackedThisCycle = false;
                            m_behaviorSpeculator.AttackCooldown = 0f;
                        }
                        if (m_behaviorSpeculator.TargetBehaviors != null && m_behaviorSpeculator.TargetBehaviors.Count > 0) {
                            m_behaviorSpeculator.TargetBehaviors.Clear();
                        }
                        if (m_behaviorSpeculator.MovementBehaviors != null && m_behaviorSpeculator.MovementBehaviors.Count > 0) {
                            for (int i = 0; i < m_behaviorSpeculator.MovementBehaviors.Count; i++) {
                                if (m_behaviorSpeculator.MovementBehaviors[i] is TakeCoverBehavior) {
                                    TakeCoverBehavior m_takeCover = m_behaviorSpeculator.MovementBehaviors[i] as TakeCoverBehavior;
                                    m_takeCover.InitialCoverChance = 0;
                                    m_takeCover.Start();
                                }
                            }
                            m_behaviorSpeculator.MovementBehaviors.Clear();
                        }
                        m_aiActor.ImpartedVelocity += activeActions.Move.Value * m_aiActor.MovementSpeed;
                        if (m_behaviorSpeculator.AttackBehaviors != null) {
                            for (int i = 0; i < m_behaviorSpeculator.AttackBehaviors.Count; i++) {
                                AttackBehaviorBase attack = m_behaviorSpeculator.AttackBehaviors[i];
                                ProcessAttackAIActor(attack);
                            }
                        }
                    }
                } break;
                case TargetType.Other: {
                    // Not Implemented Yet.
                } break;
            }
        }

        private void LateUpdate() {
            if (!m_active | !owner | !m_targetRigidbody) return;
            if (AttachPlayer) {
                if (m_targetRigidbody.HitboxPixelCollider != null) {
                    m_LastPlayerPosition = m_targetRigidbody.HitboxPixelCollider.UnitBottomLeft;
                } else {
                    m_LastPlayerPosition = parentObject.transform.position;
                }
            }
            owner.transform.position = m_LastPlayerPosition;
            owner.specRigidbody.Reinitialize();
            owner.healthHaver.IsVulnerable = false;
            owner.IsVisible = false;
            if (owner.IsInMinecart && owner.currentMineCart) owner.currentMineCart.EvacuateSpecificPlayer(owner);
            
        }

        private void ProcessAttackAIActor(AttackBehaviorBase attack) {
            if (attack == null)return;
            if (attack is BasicAttackBehavior) {
                BasicAttackBehavior basicAttackBehavior = attack as BasicAttackBehavior;
                basicAttackBehavior.Cooldown = 0f;
                basicAttackBehavior.RequiresLineOfSight = false;
                basicAttackBehavior.MinRange = -1f;
                basicAttackBehavior.Range = -1f;
                if (attack is LeapExplosion) {
                    LeapExplosion leapExploder = attack as LeapExplosion;
                    leapExploder.minLeapDistance = 0;
                    leapExploder.leapDistance = 1000f;
                }
                if (attack is TeleportBehavior) {
                    basicAttackBehavior.RequiresLineOfSight = true;
                    basicAttackBehavior.MinRange = 1000f;
                    basicAttackBehavior.Range = 0.1f;
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
            } else if (attack is AttackBehaviorGroup) {
                AttackBehaviorGroup attackBehaviorGroup = attack as AttackBehaviorGroup;
                for (int i = 0; i < attackBehaviorGroup.AttackBehaviors.Count; i++) {
                    ProcessAttackAIActor(attackBehaviorGroup.AttackBehaviors[i].Behavior);
                }
            }
        }
        
        public void PlayerOnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (otherRigidbody == m_targetRigidbody | (UseFakeActorTarget && otherRigidbody == m_fakeTargetRigidbody) |
                otherRigidbody.gameObject.GetComponent<Projectile>() | otherPixelCollider.IsTrigger |
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


        protected override void OnDestroy() {
            if (MrCapItem) {
                MrCapItem.InUse = false;
                /*if (MrCapItem.spriteAnimator)MrCapItem.spriteAnimator.Stop();
                if (MrCapItem.IsOnCooldown) {
                    MrCapItem.SetSprite("hatty_item_active_red");
                } else {
                    MrCapItem.SetSprite("hatty_item");
                }*/
            }
            if (owner) {
                OnDeath();
                if (AttachPlayer) {
                    GameManager.Instance.MainCameraController.SetManualControl(false, false);
                    if (parentObject)BraveTime.ClearMultiplier(parentObject);
                    Vector2 m_cachedPosition = ReflectGetField<Vector2>(typeof(GameActor), "m_cachedPosition", owner);
                    m_cachedPosition = m_cachedPosition.ToVector3ZUp(owner.transform.position.z);
                    IntVector2? m_SafePosition = null;
                    if (m_ParentRoom != null)m_SafePosition = m_ParentRoom.GetNearestAvailableCell(m_cachedPosition, IntVector2.One, CellTypes.FLOOR, false, m_cellValidator(GameManager.Instance.Dungeon.data, IntVector2.One));
                    if (m_SafePosition.HasValue)m_LastPlayerPosition = m_SafePosition.Value.ToVector2();
                    owner.transform.position = m_LastPlayerPosition;
                    owner.specRigidbody.Reinitialize();
                    owner.specRigidbody.OnPreRigidbodyCollision -= PlayerOnPreRigidBodyCollision;
                    owner.SetIsFlying(false, "MrCap Mind Control", false, false);
                    owner.IsVisible = true;
                    owner.IsGone = false;
                    owner.healthHaver.IsVulnerable = true;
                    owner.ClearInputOverride("MrCap Mind Control");
                }
            } else {
                owner.SetIsStealthed(false, "MrCapMindControl");
            }
            base.OnDestroy();
        }
    }
}

