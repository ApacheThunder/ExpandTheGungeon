using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandObjects;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandUsableBasicWarp : BraveBehaviour, IPlayerInteractable {
        
        public ExpandUsableBasicWarp() {            
            IsGlitchFloorExitRoomLatter = true;
            Destroy(gameObject.GetComponent<UsableBasicWarp>());
        }

        public bool IsGlitchFloorExitRoomLatter;

        private bool m_justWarped;

        private void Start() {
            GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(transform.position.IntXY(VectorConversions.Floor)).RegisterInteractable(this);
        }

        public float GetDistanceToPoint(Vector2 point) { return Vector2.Distance(point, sprite.WorldBottomCenter); }

        public void OnEnteredRange(PlayerController interactor) {
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white);
        }

        public void OnExitRange(PlayerController interactor) {
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, true);
        }

        public void Interact(PlayerController interactor) {
            if (m_justWarped) { return; }
            if (!IsGlitchFloorExitRoomLatter) {
                StartCoroutine(HandleWarpCooldown(interactor));
            } else {
                GameManager.Instance.Dungeon.StartCoroutine(HandleWarpCooldown(interactor));
            }
        }

        private IEnumerator HandleWarpCooldown(PlayerController player) {
            m_justWarped = true;
            Pixelator.Instance.FadeToBlack(0.1f, false, 0f);
            yield return new WaitForSeconds(0.1f);
            player.SetInputOverride("arbitrary warp");
            if (IsGlitchFloorExitRoomLatter) {
                ExpandGlitchTrapDoor glitchTrapDoor = FindObjectOfType<ExpandGlitchTrapDoor>();
                if (glitchTrapDoor == null) { yield break; }

                RoomHandler absoluteRoom = glitchTrapDoor.transform.position.GetAbsoluteRoom();
                Vector2 targetPoint = glitchTrapDoor.transform.position.XY() + new Vector2(2f, 3.25f);
                player.WarpToPoint(targetPoint, false, false);
            } else {
                RoomHandler entrance = GameManager.Instance.Dungeon.data.Entrance;
                Vector2 targetPoint3 = entrance.GetCenterCell().ToVector2() + new Vector2(0f, -5f);
                player.WarpToPoint(targetPoint3, false, false);
            }
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

        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }

        public float GetOverrideMaxDistance() { return -1f; }
        
    }
}

