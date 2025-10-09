using Dungeonator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {
    
    public class ExpandSignPostController : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public ExpandSignPostController() {

            ActivationAnimation = "spin_future";
            ActivationSound = "Play_EX_SignPost_Future";

            placeableWidth = 1;
            placeableHeight = 3;
            m_Triggered = false;
            m_Configured = false;
        }

        [SerializeField]
        public GameObject MinimapIcon;
        [SerializeField]
        public string ActivationAnimation;
        [SerializeField]
        public string ActivationSound;

        private bool m_Triggered;
        private bool m_Configured;
        private RoomHandler m_ParentRoom;

        public void Start() {
            if (specRigidbody)specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(OnPreRigidBodyCollision));
        }

        
        public void ConfigureOnPlacement(RoomHandler room) {
            m_ParentRoom = room;
            if (MinimapIcon) Minimap.Instance.RegisterRoomIcon(room, MinimapIcon, false);
            m_Configured = true;
        }

        private void OnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (!m_Configured) return;

            if (otherRigidbody.GetComponent<Projectile>() && otherRigidbody.GetComponent<Projectile>().Owner && (otherRigidbody.GetComponent<Projectile>().Owner is AIActor)) {
                PhysicsEngine.SkipCollision = true;
                return;
            }
            
            if (!m_Triggered) {
                if (otherRigidbody.GetComponent<PlayerController>() | otherRigidbody.GetComponent<ExpandHatMindController>()) {
                    m_Triggered = true;
                    ExpandSettings.SewersIsFuture = true;
                    int value = ~CollisionMask.LayerToMask(CollisionLayer.PlayerCollider, CollisionLayer.PlayerHitBox);
                    if (spriteAnimator && !string.IsNullOrEmpty(ActivationAnimation)) spriteAnimator.Play(ActivationAnimation);
                    if (!string.IsNullOrEmpty(ActivationSound))AkSoundEngine.PostEvent(ActivationSound, gameObject);
                    myRigidbody.RegisterGhostCollisionException(otherRigidbody);
                    PhysicsEngine.SkipCollision = true;
                    return;
                }
            }

            if (otherRigidbody.GetComponent<AIActor>()) {
                PhysicsEngine.SkipCollision = true;
                return;
            }
        }

        protected override void OnDestroy() {
            base.OnDestroy();
        }
    }
}

