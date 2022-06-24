using System;
using UnityEngine;
using System.Collections;
using Dungeonator;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCasinoWarpTrigger : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandCasinoWarpTrigger() {
            TargetSpawnPosition = new Vector3(-3, 2);
            TargetSpawnPosition2 = new Vector3(6, 1);

            TargetDoorOpenAnim = "open";
            TargetDoorCloseAnim = "close";

            m_Interacted = false;
        }
        
        public string TargetDoorOpenAnim;
        public string TargetDoorCloseAnim;
        public GameObject TargetSpawnObject;
        public Vector3 TargetSpawnPosition;
        public Vector3 TargetSpawnPosition2;


        [NonSerialized]
        private bool m_Interacted;
        [NonSerialized]
        GameObject m_SpawnedObject;
        [NonSerialized]
        GameObject m_SpawnedObject2;
        [NonSerialized]
        GameObject m_SpawnedObject3;
        [NonSerialized]
        GameObject m_SpawnedObject4;
        [NonSerialized]
        private tk2dSpriteAnimator m_TargetAnimator;
        [NonSerialized]
        private SpeculativeRigidbody m_TargetRigidBody;
        [NonSerialized]
        private SpeculativeRigidbody m_TargetRigidBody2;
        [NonSerialized]
        private ExpandWarpManager m_FoyerDoorWarp;
        [NonSerialized]
        private ExpandWarpManager m_FoyerDoorWarp2;
        [NonSerialized]
        private RoomHandler m_TargetRoom;
        [NonSerialized]
        private RoomHandler m_ParentRoom;

        /*private void Awake() { }*/
        private void Update() { }
        public void ConfigureOnPlacement(RoomHandler room) { }

        public void Interact(PlayerController player) { return; }
                
        public void OnEnteredRange(PlayerController interactor) { }

        public void OnExitRange(PlayerController interactor) { }

        public float GetDistanceToPoint(Vector2 point) {
            if (m_Interacted) { return float.PositiveInfinity; }
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
    }
}

