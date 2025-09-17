using System;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandHatProjectile : Projectile {

        [Header("Mr Cap Input Guiding")]
        public float TrackignSpeed = 345;
        public float ReturnToPlayerSpeed = 800;
        public float DumbFireTime = 0.4f;
        public float SmartFireTime = 2;
        public float ReturnToPlayerTime = 5;

        public enum FireMode { SmartFire, DumbFire, ReturnToPlayer, Inactive };

        public FireMode fireMode = FireMode.DumbFire;

        private Transform m_ChildSpriteTransform;

        private float m_DumbFireTimer;
        private float m_SmartFireTimer;
        private float m_LifeTimer;

        private bool m_Configured = false;

        protected override void Move() {
            if (!m_Configured) {
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
                        float z2 = Mathf.MoveTowardsAngle(z, target, TrackignSpeed * BraveTime.DeltaTime);
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
                    Destroy(gameObject);
                    return;
            }
            if (fireMode != FireMode.DumbFire) {
                specRigidbody.Velocity = transform.right * baseData.speed;
                LastVelocity = specRigidbody.Velocity;
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

