using System.Collections;
using Dungeonator;
using UnityEngine;
using System;
using ExpandTheGungeon.ExpandLoadingScreens;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandWarpManager : DungeonPlaceableBehaviour, IPlaceConfigurable {
        
        public ExpandWarpManager() {
            TargetPoint = new IntVector2(4, 2);
            IsOpenForTeleport = false;
            warpType = WarpType.Normal;
            OverrideTargetFloor = string.Empty;
            m_justWarped = false;
        }

        public RoomHandler TargetRoom;
        public IntVector2 TargetPoint;

        public enum WarpType { OldWestFloorWarp, FloorWarp, Normal };
        public WarpType warpType;

        public bool IsOpenForTeleport;
        public string OverrideTargetFloor;

        private bool m_justWarped;


        private void Start() {
            switch (warpType) {
                case WarpType.Normal:
                    specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
                    break;
                default: // Do nothing if anything else.
                    return;
            }
        }

        private void Update() { }

        private void HandleTriggerCollision(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (m_justWarped | !IsOpenForTeleport) { return; }
            PlayerController player = specRigidbody.GetComponent<PlayerController>();
            if (player) {
                switch (warpType) {
                    case WarpType.Normal:
                        m_justWarped = true;
                        player.SetInputOverride("arbitrary warp");
                        StartCoroutine(HandleWarp(player));
                        return;
                    case WarpType.FloorWarp:
                        StartCoroutine(HandleExitFloor());
                        m_justWarped = true;
                        return;
                    case WarpType.OldWestFloorWarp:
                        ExpandLoadingScreen.overrideType = ExpandLoadingScreen.OverrideType.West;
                        StartCoroutine(HandleOldWestExitFloor(player));
                        m_justWarped = true;
                        break;
                    default:
                        break;
                }
            }
        }
        

        private IEnumerator HandleWarp(PlayerController player) {
            Pixelator.Instance.FadeToBlack(0.1f, false, 0f);
            PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(player);
            yield return new WaitForSeconds(0.1f);
            IntVector2 targetPoint = TargetRoom.area.basePosition + TargetPoint;
            player.WarpToPoint(targetPoint.ToVector2(), false, false);
            if (otherPlayer && otherPlayer.healthHaver.IsAlive) { otherPlayer.ReuniteWithOtherPlayer(player, false); }
            GameManager.Instance.MainCameraController.ForceToPlayerPosition(player);
            Pixelator.Instance.FadeToBlack(0.1f, true, 0f);
            player.ClearInputOverride("arbitrary warp");
            yield return new WaitForSeconds(1f);
            m_justWarped = false;
            yield break;
        }

        private IEnumerator HandleExitFloor(float delay = 0.5f, bool skipFade = false) {
            foreach (PlayerController player in GameManager.Instance.AllPlayers) {
                player.PrepareForSceneTransition();
                if (!string.IsNullOrEmpty(OverrideTargetFloor) && OverrideTargetFloor.ToLower().StartsWith("tt_backrooms")) {
                    player.healthHaver.IsVulnerable = false;
                    yield return new WaitForSeconds(2f);
                }
            }
            if (!skipFade) {
                if (ExpandDebugCamera.DebugCameraEnabled) {
                    ExpandDebugCamera.SetInitialCameraPosition(Pixelator.Instance, GameManager.Instance.MainCameraController);
                } else {
                    Pixelator.Instance.FadeToBlack(0.5f, false, 0f);
                }
                yield return new WaitForSeconds(1f);
            }
            GameUIRoot.Instance.HideCoreUI(string.Empty);
            GameUIRoot.Instance.ToggleLowerPanels(false, false, string.Empty);
            yield return null;
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
                    if (OverrideTargetFloor.ToLower().StartsWith("tt_backrooms")) {
                        ExpandLoadingScreen.overrideType = ExpandLoadingScreen.OverrideType.Backrooms;
                        ExpandSettings.HasVisitedBackrooms = true;
                        yield return new WaitForSeconds(1f);
                    }
                    GameManager.Instance.DelayedLoadCustomLevel(delay, OverrideTargetFloor);
                } else {
                    GameManager.Instance.DelayedLoadNextLevel(delay);
                }
                AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
            }
            yield break;
        }

        private IEnumerator HandleOldWestExitFloor(PlayerController targetPlayer) {
            targetPlayer.SetInputOverride("Going to Old West");
            targetPlayer.specRigidbody.CollideWithOthers = false;
            if (targetPlayer.IsDodgeRolling) { targetPlayer.ForceStopDodgeRoll(); }
            // Vector2 lastPosition = targetPlayer.transform.position;
            Vector2 lastPosition = GameManager.Instance.MainCameraController.transform.position.XY();
            GameManager.Instance.MainCameraController.StopTrackingPlayer();
            GameManager.Instance.MainCameraController.SetManualControl(true, false);
            GameManager.Instance.MainCameraController.OverridePosition = lastPosition;
            targetPlayer.ForceMoveInDirectionUntilThreshold(Vector2.up, targetPlayer.CenterPosition.y + 15, maximumTime: 4);
            OverrideTargetFloor = "tt_west";
            yield return new WaitForSeconds(3f);
            if (ExpandDebugCamera.DebugCameraEnabled) {
                ExpandDebugCamera.SetInitialCameraPosition(Pixelator.Instance, GameManager.Instance.MainCameraController);
            } else {
                Pixelator.Instance.FadeToBlack(0.5f, false, 0f);
            }
            yield return new WaitForSeconds(1f);
            targetPlayer.specRigidbody.Velocity = Vector2.zero;
            targetPlayer.specRigidbody.CollideWithOthers = true;
            targetPlayer.ClearAllInputOverrides();
            StartCoroutine(HandleExitFloor());
            yield break;
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            if (specRigidbody) {
                switch (warpType) {
                    case WarpType.FloorWarp:
                        specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
                        IsOpenForTeleport = true;
                        if (sprite) {
                            sprite.HeightOffGround = -1f;
                            sprite.UpdateZDepth();
                        }
                        return;
                    case WarpType.OldWestFloorWarp:
                        specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
                        IsOpenForTeleport = true;
                        break;
                    case WarpType.Normal: // Do nothing if Normal
                        return;
                }
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

