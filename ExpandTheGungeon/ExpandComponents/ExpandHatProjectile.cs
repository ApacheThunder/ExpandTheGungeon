using UnityEngine;
using ExpandTheGungeon.ItemAPI;
using System;
using Dungeonator;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandHatProjectile : Projectile {

        [Header("Mr Cap Input Guiding")]
        public bool IsReturning = true;
        public float TrackingSpeed = 345;
        public float ReturnToPlayerSpeed = 800;
        public float DumbFireTime = 0;
        public float SmartFireTime = 2;
        public float ReturnToPlayerTime = 2.5f;

        public MrCap hatItemOwner;

        public enum FireMode { SmartFire, DumbFire, ReturnToPlayer, Inactive };

        public FireMode fireMode = FireMode.DumbFire;

        private Transform m_ChildSpriteTransform;

        private float m_DumbFireTimer;
        private float m_SmartFireTimer;
        private float m_LifeTimer;

        private bool m_Configured = false;
        

        protected override void Move() {
            if (!m_Configured) {
                specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(HatOnPreRigidBodyCollision));
                specRigidbody.OnPreTileCollision = (SpeculativeRigidbody.OnPreTileCollisionDelegate)Delegate.Combine(specRigidbody.OnPreTileCollision, new SpeculativeRigidbody.OnPreTileCollisionDelegate(HatOnPreTileCollision));
                OnBecameDebris += HatOnDebris;
                OnBecameDebrisGrounded += HatOnDebris;

                if (!m_ChildSpriteTransform) m_ChildSpriteTransform = transform.Find("Sprite");
                BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer((Owner as PlayerController).PlayerIDX);
                Vector2 vector = Vector2.zero;
                if (instanceForPlayer.IsKeyboardAndMouse(false)) {
                    vector = (Owner as PlayerController).unadjustedAimPoint.XY() - specRigidbody.UnitCenter;
                } else {
                    vector = instanceForPlayer.ActiveActions.Aim.Vector;
                }
                float target = vector.ToAngle();
                float z = transform.eulerAngles.z;
                float z2 = Mathf.MoveTowardsAngle(z, target, 100000 * BraveTime.DeltaTime);
                transform.rotation = Quaternion.Euler(0f, 0f, z2);
                if (m_ChildSpriteTransform) m_ChildSpriteTransform.transform.localRotation = Quaternion.Euler(0, 0, -z2);
                specRigidbody.Velocity = (transform.right * baseData.speed);
                LastVelocity = specRigidbody.Velocity;
                m_Configured = true;
                return;
            }
            
            switch (fireMode) {
                default:
                    fireMode = FireMode.DumbFire;
                    break;
                case FireMode.DumbFire:
                    if (DumbFireTime > 0f && m_DumbFireTimer < DumbFireTime) {
                        m_DumbFireTimer += BraveTime.DeltaTime;
                    } else {
                        fireMode = FireMode.SmartFire;
                    }
                    break;
                case FireMode.SmartFire:
                    if (Owner is PlayerController) {
                        BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer((Owner as PlayerController).PlayerIDX);
                        Vector2 vector = Vector2.zero;
                        if (instanceForPlayer.IsKeyboardAndMouse(false)) {
                            vector = (Owner as PlayerController).unadjustedAimPoint.XY() - specRigidbody.UnitCenter;
                        } else {
                            vector = instanceForPlayer.ActiveActions.Aim.Vector;
                        }
                        float target = vector.ToAngle();
                        float z = transform.eulerAngles.z;
                        float z2 = Mathf.MoveTowardsAngle(z, target, TrackingSpeed * BraveTime.DeltaTime);
                        transform.rotation = Quaternion.Euler(0f, 0f, z2);
                        if (m_ChildSpriteTransform) m_ChildSpriteTransform.transform.localRotation = Quaternion.Euler(0, 0, -z2);
                    }
                    if (SmartFireTime > 0f && m_SmartFireTimer < SmartFireTime) {
                        m_SmartFireTimer += BraveTime.DeltaTime;
                    } else {
                        fireMode = FireMode.ReturnToPlayer;
                    }
                    break;
                case FireMode.ReturnToPlayer:
                    m_LifeTimer += BraveTime.DeltaTime;
                    if (m_LifeTimer > ReturnToPlayerTime) {
                        // DieInAir(true, true, true, false);
                        if (hatItemOwner) hatItemOwner.InFlight = false;
                        fireMode = FireMode.Inactive;
                        return;
                    }
                    if (Owner) {
                        Vector3 vector = Owner.transform.position - transform.position;
                        float f = (Mathf.Atan2(vector.y, vector.x) - Mathf.Atan2(specRigidbody.Velocity.y, specRigidbody.Velocity.x)) * 57.29578f;
                        float zAngle = Mathf.Min(Mathf.Abs(f), ReturnToPlayerSpeed * BraveTime.DeltaTime) * Mathf.Sign(f);
                        transform.Rotate(0f, 0f, zAngle);
                        if (m_ChildSpriteTransform) m_ChildSpriteTransform.transform.Rotate(0, 0, -zAngle);
                        if (Vector2.Distance(Owner.CenterPosition, transform.position) < 1f) {
                            // DieInAir(true, true, true, false);
                            fireMode = FireMode.Inactive;
                            return;
                        }
                    }
                    break;
                case FireMode.Inactive:
                    if (hatItemOwner) hatItemOwner.InFlight = false;
                    Destroy(gameObject);
                    return;
            }
            if (fireMode != FireMode.DumbFire) {
                specRigidbody.Velocity = transform.right * baseData.speed;
                LastVelocity = specRigidbody.Velocity;
            }
        }

        public void HatOnPreTileCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, PhysicsEngine.Tile tile, PixelCollider otherPixelCollider) {
            if (PenetratesInternalWalls) {
                IntVector2 position = tile.Position;
                CellData cellData = GameManager.Instance.Dungeon.data[position];
                if (cellData == null || cellData.isRoomInternal) {
                    PhysicsEngine.SkipCollision = true;
                    return;
                }
            }
            PhysicsEngine.SkipCollision = true;
            if (fireMode != FireMode.ReturnToPlayer) {
                TrackingSpeed *= 10.5f;
                fireMode = FireMode.ReturnToPlayer;
            }
        }

        public void HatOnDebris(DebrisObject obj) { Destroy(obj.gameObject); }


        public void HatOnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (!Owner | !(Owner is PlayerController)) return;
            bool IsAttached = false;
            PhysicsEngine.SkipCollision = true;
            PlayerController m_Owner = (Owner as PlayerController);
            if ((myRigidbody.transform.position.GetAbsoluteRoom() == null | m_Owner.CurrentRoom == null) |
                (m_Owner.CurrentRoom != myRigidbody.transform.position.GetAbsoluteRoom())) {
                return;
            }

            if (m_Owner.CurrentRoom != null && m_Owner.CurrentRoom.connectedRooms != null) {
                foreach (RoomHandler room in m_Owner.CurrentRoom.connectedRooms) {
                    if (room.area != null && room.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) {
                        return;
                    }
                }
            }
            

            AIActor m_AIActor = otherRigidbody.gameObject.GetComponent<AIActor>();
            Chest m_Chest = otherRigidbody.gameObject.GetComponent<Chest>();

            if (otherRigidbody.GetComponentInChildren<ExpandHatMindController>())return;
            if (hatItemOwner.InUse)return;
            if (hatItemOwner.CurrentMindControl)return;
            if (!m_Chest && m_AIActor && (m_AIActor.name.ToLower().StartsWith("corrupted ") | m_Owner.CurrentRoom?.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) != null) &&
                m_Owner.CurrentRoom?.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear).Count < 2) {
                if (fireMode != FireMode.ReturnToPlayer) {
                    TrackingSpeed *= 10f;
                    fireMode = FireMode.ReturnToPlayer;
                }
                if (m_AIActor && !m_AIActor.healthHaver.IsDead && !m_AIActor.healthHaver.IsBoss &&
                    !m_AIActor.IsGone && m_AIActor.ParentRoom != null &&
                    m_Owner.CurrentRoom != null && m_AIActor.ParentRoom == m_Owner.CurrentRoom
                ) {
                    if (m_AIActor.behaviorSpeculator) m_AIActor.behaviorSpeculator.Stun(5, true);
                }
                return;
            }
            
            if (m_Chest && !m_AIActor && !m_Chest.IsBroken) {
                hatItemOwner.CurrentMindControl = m_Chest.gameObject.AddComponent<ExpandHatMindController>();
                hatItemOwner.CurrentMindControl.targetType = ExpandHatMindController.TargetType.Chest;
                hatItemOwner.CurrentMindControl.UseFakeActorTarget = false;
                if (Owner)hatItemOwner.CurrentMindControl.Init(m_Owner, m_Chest.gameObject);
                if (hatItemOwner.CurrentMindControl && hatItemOwner.CurrentMindControl.AttachFailed) {
                    hatItemOwner.CurrentMindControl.IsOnDeath = false;
                    Destroy(hatItemOwner.CurrentMindControl);
                    hatItemOwner.CurrentMindControl = null;
                } else {
                    IsAttached = true;
                }
            }

            if (!IsAttached && m_AIActor && !otherRigidbody.gameObject.GetComponent<CompanionController>()) {
                if (!m_AIActor.healthHaver.IsDead && !m_AIActor.healthHaver.IsBoss &&
                    !m_AIActor.IsGone && m_AIActor.ParentRoom != null &&
                    m_Owner.CurrentRoom != null && m_AIActor.ParentRoom == m_Owner.CurrentRoom
                    ) {
                    hatItemOwner.CurrentMindControl = m_AIActor.gameObject.AddComponent<ExpandHatMindController>();
                    hatItemOwner.CurrentMindControl.targetType = ExpandHatMindController.TargetType.AIActor;
                    hatItemOwner.CurrentMindControl.Init(m_Owner, m_AIActor.gameObject);
                    if (hatItemOwner.CurrentMindControl && hatItemOwner.CurrentMindControl.AttachFailed) {
                        hatItemOwner.CurrentMindControl.IsOnDeath = false;
                        Destroy(hatItemOwner.CurrentMindControl);
                        hatItemOwner.CurrentMindControl = null;
                    } else {
                        IsAttached = true;
                    }
                    
                }
            }
            if (IsAttached) {
                AkSoundEngine.PostEvent("Play_EX_CapReturn_01", otherRigidbody.gameObject);
                IsReturning = false;
                hatItemOwner.InFlight = false;
                hatItemOwner.InUse = true;
                Destroy(myRigidbody.gameObject);
            } /*else if (fireMode != FireMode.ReturnToPlayer) {
                TrackingSpeed *= 10.5f;
                fireMode = FireMode.ReturnToPlayer;
            }*/
        }
        

        protected override void OnDestroy() {
            if (hatItemOwner) {
                hatItemOwner.InFlight = false;
                if (IsReturning)AkSoundEngine.PostEvent("Play_obj_katana_slash_01", hatItemOwner.gameObject);
            }
            specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Remove(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(HatOnPreRigidBodyCollision));
            specRigidbody.OnPreTileCollision = (SpeculativeRigidbody.OnPreTileCollisionDelegate)Delegate.Remove(specRigidbody.OnPreTileCollision, new SpeculativeRigidbody.OnPreTileCollisionDelegate(HatOnPreTileCollision));
            OnBecameDebris -= HatOnDebris;
            OnBecameDebrisGrounded -= HatOnDebris;
            base.OnDestroy();
        }
    }
}

