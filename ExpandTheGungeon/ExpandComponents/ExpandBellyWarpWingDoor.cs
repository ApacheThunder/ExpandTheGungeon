using System.Collections;
using Dungeonator;
using UnityEngine;
using System;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandBellyWarpWingDoor : DungeonPlaceableBehaviour, IPlaceConfigurable {
        
        public ExpandBellyWarpWingDoor() {
            TargetPoint = new IntVector2(4, 2);
            IsOpenForTeleport = false;
            IsBellyExitDoor = false;
            OverrideTargetFloor = string.Empty;
            m_justWarped = false;
        }

        public RoomHandler TargetRoom;
        public IntVector2 TargetPoint;

        public bool IsOpenForTeleport;
        public bool IsBellyExitDoor;
        public string OverrideTargetFloor;

        private bool m_justWarped;


        private void Start() {
            if (specRigidbody && !IsBellyExitDoor) {
                specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
            }
        }

        private void Update() { }

        private void HandleTriggerCollision(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (m_justWarped | !IsOpenForTeleport) { return; }
            PlayerController player = specRigidbody.GetComponent<PlayerController>();
            if (player && !IsBellyExitDoor) {
                m_justWarped = true;
                player.SetInputOverride("arbitrary warp");
                StartCoroutine(HandleWarp(player));
                return;
            } else if (player && IsBellyExitDoor) {
                StartCoroutine(HandleExitFloor());
                m_justWarped = true;
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

        private IEnumerator HandleExitFloor() {
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) { GameManager.Instance.AllPlayers[i].PrepareForSceneTransition(); }
            Pixelator.Instance.FadeToBlack(0.5f, false, 0f);
            yield return new WaitForSeconds(1f);
            GameUIRoot.Instance.HideCoreUI(string.Empty);
            GameUIRoot.Instance.ToggleLowerPanels(false, false, string.Empty);
            yield return null;
            float delay = 0.5f;
            if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.SUPERBOSSRUSH) {
                GameManager.Instance.DelayedLoadBossrushFloor(delay);
            } else if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.BOSSRUSH) {
                GameManager.Instance.DelayedLoadBossrushFloor(delay);
            } else {
                if (!GameManager.Instance.IsFoyer && GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.NONE) {
                    GlobalDungeonData.ValidTilesets nextTileset = GameManager.Instance.GetNextTileset(GameManager.Instance.Dungeon.tileIndices.tilesetId);
                    GameManager.DoMidgameSave(nextTileset);
                }
                if (!string.IsNullOrEmpty(OverrideTargetFloor)) {
                    GameManager.Instance.DelayedLoadCustomLevel(delay, OverrideTargetFloor);
                } else {
                    GameManager.Instance.DelayedLoadNextLevel(delay);
                }
                AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
            }
            yield break;
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            if (specRigidbody && IsBellyExitDoor) {
                specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
                IsOpenForTeleport = true;
                sprite.HeightOffGround = -1f;
                sprite.UpdateZDepth();
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

