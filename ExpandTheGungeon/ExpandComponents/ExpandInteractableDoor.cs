using System;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;


namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandInteractableDoor : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandInteractableDoor() {
            placeableWidth = 2;
            placeableHeight = 2;
            difficulty = PlaceableDifficulty.BASE;
            isPassable = true;

            OpensAutomaticallyOnUnlocked = true;
            OutlineSpriteIsOnDoorOnly = false;
            Locks = new List<ExpandInteractableLock>();

            m_hasOpened = false;
        }

        public bool OpensAutomaticallyOnUnlocked;
        public bool OutlineSpriteIsOnDoorOnly;
        public List<ExpandInteractableLock> Locks;

        [NonSerialized]
        private bool m_hasOpened;
        [NonSerialized]
        private RoomHandler m_ParentRoom;

        public override GameObject InstantiateObject(RoomHandler targetRoom, IntVector2 loc, bool deferConfiguration = false) {
            m_ParentRoom = targetRoom;
            return base.InstantiateObject(targetRoom, loc, deferConfiguration);
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            if (m_ParentRoom == null) { m_ParentRoom = room; }
            foreach (ExpandInteractableLock Lock in Locks) {
                Lock.ParentDoor = this;
                m_ParentRoom.RegisterInteractable(Lock);
            }
            if (specRigidbody) {
                SpeculativeRigidbody rigidBody = specRigidbody;
                rigidBody.OnRigidbodyCollision = (SpeculativeRigidbody.OnRigidbodyCollisionDelegate)Delegate.Combine(rigidBody.OnRigidbodyCollision, new SpeculativeRigidbody.OnRigidbodyCollisionDelegate(HandleRigidbodyCollision));
            }
        }

        private void HandleRigidbodyCollision(CollisionData rigidbodyCollision) {
            if (rigidbodyCollision != null && rigidbodyCollision.OtherRigidbody && rigidbodyCollision.OtherRigidbody.GetComponent<KeyProjModifier>()) {
                foreach (ExpandInteractableLock Lock in Locks) {
                    if (Lock && Lock.IsLocked && Lock.lockMode == ExpandInteractableLock.InteractableLockMode.NORMAL) {
                        Lock.ForceUnlock();
                    }
                }
            }
        }

        private void Open() {
            m_hasOpened = true;
            spriteAnimator.Play();
            specRigidbody.enabled = false;
        }

        public void Interact(PlayerController player) {
            if (IsValidForUse()) { Open(); }
        }

        private void Update() {
            if (!m_hasOpened && OpensAutomaticallyOnUnlocked && IsValidForUse()) { Open(); }
        }

        private bool IsValidForUse() {
            if (m_hasOpened) { return false; }
            bool result = true;
            foreach (ExpandInteractableLock Lock in Locks) {
                if (Lock.IsLocked || Lock.spriteAnimator.IsPlaying(Lock.spriteAnimator.CurrentClip)) { result = false; }
            }
            return result;
        }

        public void OnEnteredRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            sprite.UpdateZDepth();
        }

        public float GetDistanceToPoint(Vector2 point) {
            if (!IsValidForUse() | OpensAutomaticallyOnUnlocked | OutlineSpriteIsOnDoorOnly) { return 1000f; }
            Bounds bounds = sprite.GetBounds();
            bounds.SetMinMax(bounds.min + transform.position, bounds.max + transform.position);
            float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
            float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
            return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
        }

        public float GetOverrideMaxDistance() { return -1f; }
        
        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }

        protected override void OnDestroy() {
            if (specRigidbody) {
                SpeculativeRigidbody rigidBody = specRigidbody;
                rigidBody.OnRigidbodyCollision = (SpeculativeRigidbody.OnRigidbodyCollisionDelegate)Delegate.Remove(rigidBody.OnRigidbodyCollision, new SpeculativeRigidbody.OnRigidbodyCollisionDelegate(HandleRigidbodyCollision));
            }
            base.OnDestroy();
        }
    }
}

