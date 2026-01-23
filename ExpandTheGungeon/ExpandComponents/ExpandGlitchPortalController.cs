using Dungeonator;
using ExpandTheGungeon.ExpandLoadingScreens;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandGlitchPortalController : DungeonPlaceableBehaviour, IPlayerInteractable {
        
        public ExpandGlitchPortalController() {
            CachedPosition = Vector3.zero;
            Configured = false;
            ActivationDelay = 1;
            ExpandedSize = 0.09f;
            ShrunkSize = 0.04f;
            MaxInteractionRange = 1.5f;
            DestroyAfterUse = true;
    
            EdgeColor = new Color(0.7f, 0, 0.4f, 1);
            BorderColor = new Color(0.3f, 0, 0, 1);
    
            ExpandedHoleDepth = 2;
            ShrunkHoleDepth = 12;
            SizeChangeSpeed = 1;
    
            m_IsActive = false;
            m_Shrunk = false;
            m_IsMoving = false;
            m_used = false;
            m_PositionIsValid = false;
        }
        
        public Vector3 CachedPosition;
        public bool Configured;
        public bool DestroyAfterUse;
    
        public float ActivationDelay;
        public float ShrunkSize;
        public float ExpandedSize;
        public float ExpandedHoleDepth;
        public float ShrunkHoleDepth;
        public float MaxInteractionRange;
        public float SizeChangeSpeed;
    
        public Color EdgeColor;
        public Color BorderColor;
    
        private bool m_IsActive;
        private bool m_IsMoving;
        private bool m_Shrunk;
        private bool m_used;
        private bool m_PositionIsValid;
    
        public RoomHandler ParentRoom;
    
    
        public float GetDistanceToPoint(Vector2 point) { return Vector2.Distance(point, transform.position.XY()) / 1.5f; }
        public float GetOverrideMaxDistance() { return MaxInteractionRange; }
        
        private void Awake() {        
            renderer.material.SetColor("_EdgeColor", EdgeColor);
            renderer.material.SetColor("_BorderColor", BorderColor);
            renderer.material.SetFloat("_UVDistCutoff", ExpandedSize);
            renderer.material.SetFloat("_HoleEdgeDepth", ExpandedHoleDepth);
            StartCoroutine(HandleDelayedActivation(ActivationDelay));
        }
    
        private void Update() {
            if (!Configured | ParentRoom == null | !m_IsActive) { return; }
            if (!m_used && !m_PositionIsValid) {
                Dungeon dungeon = GameManager.Instance.Dungeon;
                if (!dungeon.data.CheckInBoundsAndValid(transform.PositionVector2().ToIntVector2().x, transform.PositionVector2().ToIntVector2().y) | dungeon.data.isWall(transform.PositionVector2().ToIntVector2().x, transform.PositionVector2().ToIntVector2().y)) {
                    m_used = true;
                    if (ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) || !ParentRoom.EverHadEnemies) {
                        ParentRoom.ResetPredefinedRoomLikeDarkSouls();
                    }
                    DestroyAfterUse = true;
                    ParentRoom.DeregisterInteractable(this);
                    if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.SUPERBOSSRUSH |
                        GameManager.Instance.CurrentGameMode == GameManager.GameMode.BOSSRUSH |
                        ExpandSettings.HasVisitedBackrooms | Random.value > 0.3f) {
                        StartCoroutine(HandleTeleport(GameManager.Instance.PrimaryPlayer, CachedPosition, 1));
                    } else {
                        ExpandLoadingScreen.overrideType = ExpandLoadingScreen.OverrideType.Backrooms;
                        StartCoroutine(HandleBackroomsAccident(GameManager.Instance.PrimaryPlayer));
                    }
                } else {
                    m_PositionIsValid = true;
                }
            }
            if (m_IsActive && m_PositionIsValid && !m_used && !m_IsMoving) {
                if (!ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) && m_Shrunk) {
                    m_Shrunk = false;
                    StartCoroutine(HandleExpand());
                } else if (ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) && !m_Shrunk) {
                    m_Shrunk = true;
                    StartCoroutine(HandleShrink());
                }
            }
        }
        
        // Added to allow COOP player to use portal if player 1 is dead.
        public bool IsPrimaryPlayerOrSoleLivingPlayer(PlayerController player) {
            if (player.IsPrimaryPlayer)return true;
            if (GameManager.Instance.GetOtherPlayer(player) && GameManager.Instance.GetOtherPlayer(player).IsGhost) return true;
            return false;
        }

        public void OnEnteredRange(PlayerController interactor) { }
        public void OnExitRange(PlayerController interactor) { }
    
        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }
    
        public void Interact(PlayerController interactor) {
            if (!Configured | !m_IsActive) { return; }
            if (m_used || !IsPrimaryPlayerOrSoleLivingPlayer(interactor)) { return; }
            if (CachedPosition == Vector3.zero | ParentRoom == null | ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { return; }
            m_used = true;
            if (DestroyAfterUse) { ParentRoom.DeregisterInteractable(this); }
            StartCoroutine(HandleTeleport(interactor, CachedPosition));
        }
    
        private void TogglePlayerInput(PlayerController targetPlayer, bool lockState) {
            if (lockState) {
                targetPlayer.ForceStopDodgeRoll();
                targetPlayer.CurrentInputState = PlayerInputState.NoInput;
                targetPlayer.healthHaver.IsVulnerable = false;
            } else {
                targetPlayer.CurrentInputState = PlayerInputState.AllInput;
                targetPlayer.healthHaver.IsVulnerable = true;
            }
        }
    
        private IEnumerator HandleDelayedActivation(float delay) {
            yield return new WaitForSeconds(delay);
            m_IsActive = true;
            yield break;
        }
    
        private IEnumerator HandleTeleport(PlayerController targetPlayer, Vector2 targetPoint, float delay = -1) {
            TogglePlayerInput(targetPlayer, true);
            if (delay != -1) { yield return new WaitForSeconds(delay); }
            ExpandShaders.Instance.GlitchScreenForDuration(1, 1.4f, 0.1f);
            Dungeon dungeon = GameManager.Instance.Dungeon;
            GameObject TempFXObject = Instantiate(ExpandAssets.LoadAsset<GameObject>("EXLeadKeyGlitchScreenFX"), transform.position, Quaternion.identity);
            TempFXObject.transform.SetParent(dungeon.gameObject.transform);
            AkSoundEngine.PostEvent("Play_EX_CorruptionRoomTransition_01", targetPlayer.gameObject);
            ExpandScreenFXController fxController = TempFXObject.GetComponent<ExpandScreenFXController>();
            yield return null;
            if (!ExpandLists.InvalidGraphicsModes.Contains(SystemInfo.graphicsDeviceType)) {
                while (fxController.GlitchAmount < 1) {
                    fxController.GlitchAmount += (BraveTime.DeltaTime / 0.7f);
                    yield return null;
                }
            } else {
                fxController.GlitchAmount = 0;
                Destroy(fxController);
                Destroy(TempFXObject);
            }
            CameraController cameraController = GameManager.Instance.MainCameraController;
            Vector2 offsetVector = (cameraController.transform.position - targetPlayer.transform.position);
            offsetVector -= cameraController.GetAimContribution();
            cameraController.SetManualControl(true, false);
            cameraController.OverridePosition = cameraController.transform.position;
            yield return new WaitForSeconds(0.1f);
            targetPlayer.transform.position = targetPoint;
            targetPlayer.specRigidbody.Reinitialize();
            targetPlayer.specRigidbody.RecheckTriggers = true;
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(targetPlayer);
                if (otherPlayer) otherPlayer.WarpToPoint(targetPlayer.transform.position);
                yield return null;
                cameraController.OverridePosition = cameraController.GetIdealCameraPosition();
            } else {
                cameraController.OverridePosition = (targetPoint + offsetVector).ToVector3ZUp(0f);
            }
            targetPlayer.WarpFollowersToPlayer();
            targetPlayer.WarpCompanionsToPlayer(false);        
            // Pixelator.Instance.MarkOcclusionDirty();
            yield return null;
            if (!ExpandLists.InvalidGraphicsModes.Contains(SystemInfo.graphicsDeviceType) && fxController) {
                while (fxController.GlitchAmount > 0) {
                    fxController.GlitchAmount -= (BraveTime.DeltaTime / 0.7f);
                    yield return null;
                }
            }
            cameraController.SetManualControl(false, true);
            yield return new WaitForSeconds(0.15f);
            if (!DestroyAfterUse) { m_used = false; }
            targetPlayer.DoVibration(Vibration.Time.Normal, Vibration.Strength.Medium);
            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(targetPlayer.specRigidbody, null, false);
            TogglePlayerInput(targetPlayer, false);
            yield return null;
            /*if (targetPlayer.transform.position.GetAbsoluteRoom() != null) {
                targetPlayer.ForceChangeRoom(targetPlayer.transform.position.GetAbsoluteRoom());
            }*/
            if (DestroyAfterUse) {
                fxController.GlitchAmount = 0;
                Destroy(fxController);
                Destroy(TempFXObject);
                Destroy(gameObject);
            }
            yield break;
        }
        
        private IEnumerator HandleBackroomsAccident(PlayerController player, float delay = 0.5f, bool skipFade = false) {
            if (player)player.PrepareForSceneTransition();
            yield return new WaitForSeconds(3f);
            if (!skipFade) {
                Pixelator.Instance.FadeToBlack(0.5f, false, 0f);
                yield return new WaitForSeconds(1f);
            }
            GameUIRoot.Instance.HideCoreUI(string.Empty);
            GameUIRoot.Instance.ToggleLowerPanels(false, false, string.Empty);
            ExpandSettings.HasVisitedBackrooms = true;
            yield return null;
            GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_backrooms");
            AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
            yield break;
        }


        private IEnumerator HandleShrink() {
            while(m_IsMoving) { yield return null; }
            m_IsMoving = true;
            float elapsed = 0f;
            while (elapsed < SizeChangeSpeed) {
                elapsed += (BraveTime.DeltaTime * 1f);
                renderer.material.SetFloat("_UVDistCutoff", Mathf.Lerp(ExpandedSize, ShrunkSize, elapsed / SizeChangeSpeed));
                renderer.material.SetFloat("_HoleEdgeDepth", Mathf.Lerp(ExpandedHoleDepth, ShrunkHoleDepth, elapsed / SizeChangeSpeed));
                yield return null;
            }
            m_IsMoving = false;
            yield break;
        }
    
        private IEnumerator HandleExpand() {
            while (m_IsMoving) { yield return null; }
            m_IsMoving = true;
            float elapsed = 0f;
            while (elapsed < SizeChangeSpeed) {
                elapsed += (BraveTime.DeltaTime * 1f);
                renderer.material.SetFloat("_UVDistCutoff", Mathf.Lerp(ShrunkSize, ExpandedSize, elapsed / SizeChangeSpeed));
                renderer.material.SetFloat("_HoleEdgeDepth", Mathf.Lerp(ShrunkHoleDepth, ExpandedHoleDepth, elapsed / SizeChangeSpeed));
                yield return null;
            }
            m_IsMoving = false;
            yield break;
        }
    }
}

