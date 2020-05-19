using System.Collections;
using Dungeonator;
using UnityEngine;
using System;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandBellyWarpWingDoor : DungeonPlaceableBehaviour, IPlaceConfigurable {
        
        public ExpandBellyWarpWingDoor() {
            TargetPoint = new IntVector2(4, 2);
            IsOpenForTeleport = false;
            m_justWarped = false;
        }

        public RoomHandler TargetRoom;
        public IntVector2 TargetPoint;

        public bool IsOpenForTeleport;

        private bool m_justWarped;


        private void Start() {
            if (specRigidbody) {
                specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
            }
        }

        private void Update() { }

        private void HandleTriggerCollision(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (m_justWarped | !IsOpenForTeleport) { return; }
            PlayerController player = specRigidbody.GetComponent<PlayerController>();
            if (player) {
                m_justWarped = true;
                player.SetInputOverride("arbitrary warp");
                StartCoroutine(HandleWarp(player));
                return;
            }
        }

        private IEnumerator HandleWarp(PlayerController player) {
            Pixelator.Instance.FadeToBlack(0.1f, false, 0f);
            yield return new WaitForSeconds(0.1f);
            IntVector2 targetPoint = TargetRoom.area.basePosition + TargetPoint;
            player.WarpToPoint(targetPoint.ToVector2(), false, false);
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(player);
                if (otherPlayer && otherPlayer.healthHaver.IsAlive) { otherPlayer.ReuniteWithOtherPlayer(player, false); }
            }
            GameManager.Instance.MainCameraController.ForceToPlayerPosition(player);
            Pixelator.Instance.FadeToBlack(0.1f, true, 0f);
            player.ClearInputOverride("arbitrary warp");
            yield return new WaitForSeconds(1f);
            m_justWarped = false;
            yield break;
        }

        public void ConfigureOnPlacement(RoomHandler room) { }

        protected override void OnDestroy() {
            base.OnDestroy();
        }
    }
}

