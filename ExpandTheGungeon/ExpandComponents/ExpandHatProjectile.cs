using UnityEngine;
using ExpandTheGungeon.ItemAPI;
using System;
using Dungeonator;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandHatProjectile : Projectile {

        [Header("Mr Cap Input Guiding")]
        public bool AllowSingleEnemy = true;
        public bool IsReturning = true;
        public float TrackingSpeed = 345;
        public float ReturnToPlayerSpeed = 3000;
        public float DumbFireTime = 0.1f;
        public float SmartFireTime = 2;
        public float ReturnToPlayerTime = 3;

        public string HatReturnSFX = "Play_obj_katana_slash_01";

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
                transform.rotation = Quaternion.Euler(0f, 0f, target);
                if (m_ChildSpriteTransform) m_ChildSpriteTransform.transform.localRotation = Quaternion.Euler(0, 0, -target);
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
                        Vector2 OwnerPosition = Owner.transform.position.XY();
                        if (Owner.sprite) OwnerPosition = Owner.sprite.WorldTopCenter;

                        float target = (OwnerPosition - transform.position.XY()).ToAngle();
                        float z = transform.eulerAngles.z;
                        float z2 = Mathf.MoveTowardsAngle(z, target, ReturnToPlayerSpeed * BraveTime.DeltaTime);
                        transform.rotation = Quaternion.Euler(0f, 0f, z2);
                        if (m_ChildSpriteTransform) m_ChildSpriteTransform.transform.localRotation = Quaternion.Euler(0, 0, -z2);
                        if (Vector2.Distance(OwnerPosition, transform.position) < 0.8f) {
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

        public void HatOnDebris(DebrisObject obj) { Destroy(obj.gameObject); }

        public void HatOnPreTileCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, PhysicsEngine.Tile tile, PixelCollider otherPixelCollider) {
            PhysicsEngine.SkipCollision = true;
            if (fireMode == FireMode.DumbFire | fireMode == FireMode.ReturnToPlayer) return;
            if (PenetratesInternalWalls) {
                IntVector2 position = tile.Position;
                CellData cellData = GameManager.Instance.Dungeon.data[position];
                if (cellData == null || cellData.isRoomInternal)return;
            }
            fireMode = FireMode.ReturnToPlayer;
        }

        public void HatOnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (!Owner | !(Owner is PlayerController)) return;
            bool IsAttached = false;
            PhysicsEngine.SkipCollision = true;
            if (fireMode == FireMode.DumbFire) return;
            if (ExpandTheGungeon.PortableShipInUse)return;

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
            if (!m_Chest && m_AIActor && (m_AIActor.name.ToLower().StartsWith("corrupted ") | (!AllowSingleEnemy && m_Owner.CurrentRoom?.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) != null)) &&
                m_Owner.CurrentRoom?.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear).Count < 2) {
                if (fireMode != FireMode.ReturnToPlayer)fireMode = FireMode.ReturnToPlayer;
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
                hatItemOwner.CurrentMindControl.MrCapItem = hatItemOwner;
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
                    hatItemOwner.CurrentMindControl.MrCapItem = hatItemOwner;
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
                PlayCaptureSFX(otherRigidbody.gameObject);
                IsReturning = false;
                hatItemOwner.InFlight = false;
                hatItemOwner.InUse = true;
                Destroy(myRigidbody.gameObject);
            }
        }

        private void PlayCaptureSFX(GameObject target) {
            if (!string.IsNullOrEmpty(HatReturnSFX))AkSoundEngine.PostEvent(HatReturnSFX, target);
        }
        

        protected override void OnDestroy() {
            if (hatItemOwner) {
                hatItemOwner.InFlight = false;
                if (IsReturning) PlayCaptureSFX(hatItemOwner.gameObject);
            }
            specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Remove(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(HatOnPreRigidBodyCollision));
            specRigidbody.OnPreTileCollision = (SpeculativeRigidbody.OnPreTileCollisionDelegate)Delegate.Remove(specRigidbody.OnPreTileCollision, new SpeculativeRigidbody.OnPreTileCollisionDelegate(HatOnPreTileCollision));
            OnBecameDebris -= HatOnDebris;
            OnBecameDebrisGrounded -= HatOnDebris;
            base.OnDestroy();
        }
    }
}

