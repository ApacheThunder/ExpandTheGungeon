using Dungeonator;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;
using System.Collections;
using UnityEngine;

public class ExpandGlitchPortalController : DungeonPlaceableBehaviour, IPlayerInteractable {


    public ExpandGlitchPortalController() {
        CachedPosition = Vector3.zero;
        Configured = false;
        InitialSize = 0.13f;
        MinSize = 0.05f;
        MaxInteractionRange = 1f;
        DestroyAfterUse = true;

        m_Shrunk = false;
        m_IsMoving = false;
        m_used = false;
    }
    
    public Vector3 CachedPosition;
    public bool Configured;
    public bool DestroyAfterUse;

    public float InitialSize;
    public float MinSize;
    public float MaxInteractionRange;


    private bool m_IsMoving;
    private bool m_Shrunk;
    private bool m_used;

    public RoomHandler ParentRoom;


    public float GetDistanceToPoint(Vector2 point) { return Vector2.Distance(point, transform.position.XY()) / 1.5f; }
    public float GetOverrideMaxDistance() { return MaxInteractionRange; }

    private void Awake() { renderer.material.SetFloat("_UVDistCutoff", InitialSize); }

    public void OnEnteredRange(PlayerController interactor) {
        if(!Configured) { return; }
        if (m_used || !interactor.IsPrimaryPlayer) { return; }
        if (CachedPosition == Vector3.zero | ParentRoom == null | ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
            return;
        }
        if (m_Shrunk && !m_IsMoving && ParentRoom != null && !ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
            m_Shrunk = false;
            StartCoroutine(HandleExpand());
        }
    }

    public void OnExitRange(PlayerController interactor) {
        if (!Configured) { return; }        
        if (!m_Shrunk) {
            m_Shrunk = true;
            StartCoroutine(HandleShrink());
        }
    }

    public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
        shouldBeFlipped = false;
        return string.Empty;
    }

    public void Interact(PlayerController interactor) {
        if (!Configured) { return; }
        if (m_used || !interactor.IsPrimaryPlayer) { return; }
        if (m_IsMoving | CachedPosition == Vector3.zero | ParentRoom == null | ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
            return;
        }
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

    private IEnumerator HandleTeleport(PlayerController targetPlayer, Vector2 targetPoint) {
        TogglePlayerInput(targetPlayer, true);
        ExpandShaders.Instance.GlitchScreenForDuration(1, 1.4f, 0.1f);
        Dungeon dungeon = GameManager.Instance.Dungeon;
        GameObject TempFXObject = new GameObject("EXScreenFXTemp") { layer = 0 };
        TempFXObject.transform.SetParent(dungeon.gameObject.transform);
        TempFXObject.SetActive(false);
        yield return null;
        AkSoundEngine.PostEvent("Play_EX_CorruptionRoomTransition_01", targetPlayer.gameObject);
        ExpandGlitchScreenFXController fxController = TempFXObject.AddComponent<ExpandGlitchScreenFXController>();
        fxController.shaderType = ExpandGlitchScreenFXController.ShaderType.Glitch;
        fxController.GlitchAmount = 0;
        yield return null;
        TempFXObject.SetActive(true);
        while (fxController.GlitchAmount < 1) {
            fxController.GlitchAmount += (BraveTime.DeltaTime / 0.7f);
            yield return null;
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
            cameraController.OverridePosition = cameraController.GetIdealCameraPosition();
        } else {
            cameraController.OverridePosition = (targetPoint + offsetVector).ToVector3ZUp(0f);
        }
        targetPlayer.WarpFollowersToPlayer();
        targetPlayer.WarpCompanionsToPlayer(false);        
        Pixelator.Instance.MarkOcclusionDirty();
        yield return null;
        while (fxController.GlitchAmount > 0) {
            fxController.GlitchAmount -= (BraveTime.DeltaTime / 0.7f);
            yield return null;
        }
        cameraController.SetManualControl(false, true);
        yield return new WaitForSeconds(0.15f);
        if (!DestroyAfterUse) { m_used = false; }
        targetPlayer.DoVibration(Vibration.Time.Normal, Vibration.Strength.Medium);
        PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(targetPlayer.specRigidbody, null, false);
        TogglePlayerInput(targetPlayer, false);
        yield return null;
        if (DestroyAfterUse) { Destroy(gameObject); }
        yield break;
    }
    
    private IEnumerator HandleShrink() {
        while(m_IsMoving) { yield return null; }
        m_IsMoving = true;
        float elapsed = 0f;
        float duration = 1f;
        while (elapsed < duration) {
            elapsed += (BraveTime.DeltaTime * 1f);
            renderer.material.SetFloat("_UVDistCutoff", Mathf.Lerp(InitialSize, MinSize, elapsed / duration));
            yield return null;
        }
        m_IsMoving = false;
        yield break;
    }

    private IEnumerator HandleExpand() {
        while (m_IsMoving) { yield return null; }
        m_IsMoving = true;
        float elapsed = 0f;
        float duration = 1f;
        while (elapsed < duration) {
            elapsed += (BraveTime.DeltaTime * 1f);
            renderer.material.SetFloat("_UVDistCutoff", Mathf.Lerp(MinSize, InitialSize, elapsed / duration));
            yield return null;
        }
        m_IsMoving = false;
        yield break;
    }
}

