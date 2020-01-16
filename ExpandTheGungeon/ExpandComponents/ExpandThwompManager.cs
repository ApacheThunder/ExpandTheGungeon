using Dungeonator;
using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandThwompManager : BraveBehaviour {

        public ExpandThwompManager() { destroyOnRoomDeparture = true; inactiveWhilePlayerOutsideRoom = true; }

        public bool destroyOnRoomDeparture;
        public bool inactiveWhilePlayerOutsideRoom;

        private RoomHandler m_StartRoom;

        private void Start() { m_StartRoom = aiActor.transform.position.GetAbsoluteRoom(); }

        private void Update() {
            if (destroyOnRoomDeparture) { StartCoroutine(CheckCurrentRoom()); }            
            if (inactiveWhilePlayerOutsideRoom) { CheckPlayerRoom(); }
        }

        private void CheckPlayerRoom() {
            if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() == null |
                GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != m_StartRoom)
            {
                aiActor.behaviorSpeculator.enabled = false;
                return;
            } else {
                aiActor.behaviorSpeculator.enabled = true;
                return;
            }            
        }

        private IEnumerator CheckCurrentRoom() {
            if (aiActor.transform.position.GetAbsoluteRoom() == null | aiActor.transform.position.GetAbsoluteRoom() != m_StartRoom) {
                destroyOnRoomDeparture = false;
                if (healthHaver.PreventAllDamage) { healthHaver.PreventAllDamage = false; }
                if (aiActor.CollisionDamage > 0) { aiActor.CollisionDamage = 0f; }
                if (aiActor.CollisionKnockbackStrength > 0) { aiActor.CollisionKnockbackStrength = 0f; }
                healthHaver.ApplyDamage(100000f, Vector2.zero, "Telefrag", CoreDamageTypes.None, DamageCategory.Normal, true, null, false);
                yield break;
            } else {
                yield break;
            }            
        }

        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

