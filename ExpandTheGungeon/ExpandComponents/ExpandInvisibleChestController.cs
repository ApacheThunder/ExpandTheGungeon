

using Dungeonator;
using System;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandInvisibleChestController : DungeonPlaceableBehaviour, IPlayerInteractable {

        public ExpandInvisibleChestController() { m_Interacted = false; }
        
        private bool m_Interacted;        
        private Chest m_ParentChest;
        private RoomHandler m_room;

        public void Start() {
            if (base.majorBreakable != null) {
                MajorBreakable majorBreakable = base.majorBreakable;
                majorBreakable.OnBreak = (Action)Delegate.Combine(majorBreakable.OnBreak, new Action(OnBroken));
            }
        }

        public void Interact(PlayerController interactor) {
            if (!m_Interacted && m_ParentChest) {
                m_Interacted = true;
                sprite.renderer.enabled = true;
                sprite.specRigidbody.enabled = true;
                if (m_ParentChest.LockAnimator) { m_ParentChest.LockAnimator.Sprite.renderer.enabled = true; }
                if (m_ParentChest.ShadowSprite) { m_ParentChest.ShadowSprite.renderer.enabled = true; }
                ETGModConsole.Log("Test Interact");
            }
        }


        public void ConfigureOnPlacement(RoomHandler room) {
            m_room = room;
            if (gameObject.GetComponent<Chest>()) { m_ParentChest = gameObject.GetComponent<Chest>(); }
        }

        private void OnBroken() { m_room.DeregisterInteractable(this); }
        
        public float GetDistanceToPoint(Vector2 point) {        
            Bounds bounds = sprite.GetBounds();
            bounds.SetMinMax(bounds.min + transform.position, bounds.max + transform.position);
            float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
            float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
            return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
        }

        public float GetOverrideMaxDistance() { return -1f; }

        public void OnEnteredRange(PlayerController interactor) { }

        public void OnExitRange(PlayerController interactor) { }

        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
           shouldBeFlipped = false;
            return string.Empty;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

