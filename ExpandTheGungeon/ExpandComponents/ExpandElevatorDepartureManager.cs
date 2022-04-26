using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandDungeonFlows;

namespace ExpandTheGungeon.ExpandComponents {

    class ExpandElevatorDepartureManager : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public tk2dSpriteAnimator elevatorAnimator;
        public tk2dSpriteAnimator ceilingAnimator;
        public tk2dSpriteAnimator facewallAnimator;
        public tk2dSpriteAnimator floorAnimator;
        public tk2dSprite[] priorSprites;
        public tk2dSprite[] postSprites;
        public BreakableChunk chunker;
        public Transform spawnTransform;
        public GameObject elevatorFloor;
        public tk2dSpriteAnimator crumblyBumblyAnimator;
        public tk2dSpriteAnimator smokeAnimator;
        public string elevatorDescendAnimName;
        public string elevatorOpenAnimName;
        public string elevatorCloseAnimName;
        public string elevatorDepartAnimName;
        public ScreenShakeSettings arrivalShake;
        public ScreenShakeSettings doorOpenShake;
        public ScreenShakeSettings doorCloseShake;
        public ScreenShakeSettings departureShake;
        public bool UsesOverrideTargetFloor;
        public bool ConfigurationWasDeferred;
        public GlobalDungeonData.ValidTilesets OverrideTargetFloor;
        public bool IsGlitchElevator;
        public string OverrideExactLevelName;
        public string OverrideTargetFlorDungeonFlow;

        public const bool c_savingEnabled = true;

        private Tribool m_isArrived;
        private bool m_depatureIsPlayerless;
        private bool m_hasEverArrived;

        public ExpandElevatorDepartureManager() {
            m_isArrived = Tribool.Unready;
            ConfigurationWasDeferred = false;
            UsesOverrideTargetFloor = true;
            IsGlitchElevator = false;
            OverrideExactLevelName = "tt_tutorial";
            OverrideTargetFlorDungeonFlow = string.Empty;
            OverrideTargetFloor = GlobalDungeonData.ValidTilesets.WESTGEON;
        }

        private void Start() {
            ElevatorDepartureController departureComponent = gameObject.GetComponent<ElevatorDepartureController>();

            if (departureComponent) {
                elevatorAnimator = departureComponent.elevatorAnimator;
                ceilingAnimator = departureComponent.ceilingAnimator;
                facewallAnimator = departureComponent.facewallAnimator;
                floorAnimator = departureComponent.floorAnimator;
                priorSprites = departureComponent.priorSprites;
                postSprites = departureComponent.postSprites;
                chunker = departureComponent.chunker;
                spawnTransform = departureComponent.spawnTransform;
                elevatorFloor = departureComponent.elevatorFloor;
                crumblyBumblyAnimator = departureComponent.crumblyBumblyAnimator;
                smokeAnimator = departureComponent.smokeAnimator;
                elevatorDescendAnimName = departureComponent.elevatorDescendAnimName;
                elevatorOpenAnimName = departureComponent.elevatorOpenAnimName;
                elevatorCloseAnimName = departureComponent.elevatorCloseAnimName;
                elevatorDepartAnimName = departureComponent.elevatorDepartAnimName;
                arrivalShake = departureComponent.arrivalShake;
                doorOpenShake = departureComponent.doorOpenShake;
                doorCloseShake = departureComponent.doorCloseShake;
                departureShake = departureComponent.departureShake;
                // Remove CryoButton. This version of the elevator won't support that.
                GameObject[] objects = FindObjectsOfType<GameObject>();
                List<GameObject> CryoButtons = new List<GameObject>();
                if (objects != null && objects.Length > 0) {
                    foreach (GameObject targetObject in objects) {
                        if (targetObject != null && targetObject.transform != null && !string.IsNullOrEmpty(targetObject.name)){
                            if (targetObject.name.StartsWith("CryoElevatorButton") && targetObject.transform.position.GetAbsoluteRoom() == GetAbsoluteParentRoom()) {
                                float magnitude = (targetObject.transform.PositionVector2() - gameObject.transform.PositionVector2()).magnitude;
                                if (magnitude <= 7) { CryoButtons.Add(targetObject); }
                            }
                        }
                    }
                }

                if (CryoButtons.Count > 0) { for (int i = 0; i < CryoButtons.Count; i++) { Destroy(CryoButtons[i]); } }

                departureComponent.enabled = false;
                Destroy(departureComponent);
                Destroy(gameObject.GetComponent<ElevatorDepartureController>());

                SpeculativeRigidbody component = elevatorFloor.GetComponent<SpeculativeRigidbody>();
                if (component) {
                    if (ConfigurationWasDeferred) {
                        component.PrimaryPixelCollider.ManualOffsetY -= 8;
                        component.PrimaryPixelCollider.ManualHeight += 8;
                        component.Reinitialize();
                    }
                    SpeculativeRigidbody speculativeRigidbody = component;
                    speculativeRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(speculativeRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(OnElevatorTriggerEnter));
                }
                ToggleSprites(true);
            } else {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] ERROR: ElevatorDepatureComponent is null!");
                    Destroy(this);
                }
            }

            if (ConfigurationWasDeferred) {
                Material material = Instantiate(priorSprites[1].renderer.material);
                material.shader = ShaderCache.Acquire("Brave/Unity Transparent Cutout");
                priorSprites[1].renderer.material = material;
                Material material2 = Instantiate(postSprites[2].renderer.material);
                material2.shader = ShaderCache.Acquire("Brave/Unity Transparent Cutout");
                postSprites[2].renderer.material = material2;
                postSprites[1].HeightOffGround = postSprites[1].HeightOffGround - 0.0625f;
                postSprites[3].HeightOffGround = postSprites[3].HeightOffGround - 0.0625f;
                postSprites[1].UpdateZDepth();
            }
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            if (ConfigurationWasDeferred) {
                IntVector2 intVector = transform.position.IntXY(VectorConversions.Floor);
                for (int i = 0; i < 6; i++) {
                    for (int j = -2; j < 6; j++) {
                        CellData cellData = GameManager.Instance.Dungeon.data.cellData[intVector.x + i][intVector.y + j];
                        cellData.cellVisualData.precludeAllTileDrawing = true;
                        if (j < 4) {
                            cellData.type = CellType.PIT;
                            cellData.fallingPrevented = true;
                        }
                        cellData.isOccupied = true;
                    }
                }
                if ((GameManager.Instance.CurrentGameMode == GameManager.GameMode.NORMAL || GameManager.Instance.CurrentGameMode == GameManager.GameMode.SHORTCUT) && GameManager.Instance.CurrentLevelOverrideState != GameManager.LevelOverrideState.TUTORIAL) {
                    GameObject gameObject = (GameObject)Instantiate(BraveResources.Load("Global Prefabs/CryoElevatorButton", ".prefab"), transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
                    IntVector2 a = transform.position.IntXY(VectorConversions.Floor) + new IntVector2(-2, 0);
                    for (int k = 0; k < 2; k++) {
                        for (int l = -1; l < 2; l++) {
                            if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(a + new IntVector2(k, l))) {
                                CellData cellData2 = GameManager.Instance.Dungeon.data[a + new IntVector2(k, l)];
                                cellData2.cellVisualData.containsWallSpaceStamp = true;
                                cellData2.cellVisualData.containsObjectSpaceStamp = true;
                            }
                        }
                    }
                }
            }            
        }


        private void ToggleSprites(bool prior) {
            if (priorSprites != null & priorSprites.Length > 0) {
                foreach (tk2dSprite priorSprite in priorSprites) {
                    if (priorSprite && priorSprite.renderer) { priorSprite.renderer.enabled = prior; }
                }
            }
            if (postSprites != null && postSprites.Length > 0) {
                foreach (tk2dSprite postSprite in postSprites) {
                    if (postSprite && postSprite.renderer) { postSprite.renderer.enabled = !prior; }
                }
            }
        }

        private void TransitionToDoorOpen(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip) {
            animator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Remove(animator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(TransitionToDoorOpen));
            elevatorFloor.SetActive(true);
            elevatorFloor.GetComponent<MeshRenderer>().enabled = true;
            smokeAnimator.gameObject.SetActive(true);
            smokeAnimator.PlayAndDisableObject(string.Empty, null);
            GameManager.Instance.MainCameraController.DoScreenShake(doorOpenShake, null, false);
            animator.Play(elevatorOpenAnimName);
        }

        private void TransitionToDoorClose(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip) {
            GameManager.Instance.MainCameraController.DoScreenShake(doorCloseShake, null, false);
            animator.Play(elevatorCloseAnimName);
            animator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Combine(animator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(TransitionToDepart));
        }

        private void TransitionToDepart(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip) {
            
            GameManager.Instance.MainCameraController.DoDelayedScreenShake(departureShake, 0.25f, null);
            if (!m_depatureIsPlayerless) {                
                for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) { GameManager.Instance.AllPlayers[i].PrepareForSceneTransition(); }
                float delay = 0.5f;
                Pixelator.Instance.FadeToBlack(delay, false, 0f);
                GameUIRoot.Instance.HideCoreUI(string.Empty);
                GameUIRoot.Instance.ToggleLowerPanels(false, false, string.Empty);
                if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.SUPERBOSSRUSH) {
                    GameManager.Instance.DelayedLoadBossrushFloor(delay);
                } else if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.BOSSRUSH) {
                    GameManager.Instance.DelayedLoadBossrushFloor(delay);
                } else {
                    if (!GameManager.Instance.IsFoyer && GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.NONE) {
                        GlobalDungeonData.ValidTilesets nextTileset = GameManager.Instance.GetNextTileset(GameManager.Instance.Dungeon.tileIndices.tilesetId);
                        GameManager.DoMidgameSave(nextTileset);
                    }
                    if (IsGlitchElevator) {
                        ExpandSettings.glitchElevatorHasBeenUsed = true;
                        GameManager.Instance.StartCoroutine(ExpandUtility.DelayedGlitchLevelLoad(delay, BraveUtility.RandomElement(ExpandDungeonFlow.GlitchChestFlows), BraveUtility.RandomBool()));
                    } else if (UsesOverrideTargetFloor) {
                        GlobalDungeonData.ValidTilesets overrideTargetFloor = OverrideTargetFloor;
                        if (!string.IsNullOrEmpty(OverrideTargetFlorDungeonFlow)) {
                            GameManager.Instance.InjectedFlowPath = OverrideTargetFlorDungeonFlow;
                        }
                        switch (overrideTargetFloor) {
                            case GlobalDungeonData.ValidTilesets.CASTLEGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_castle");
                                break;
                            case GlobalDungeonData.ValidTilesets.SEWERGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_sewer");
                                break;
                            case GlobalDungeonData.ValidTilesets.JUNGLEGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_jungle");
                                break;
                            case GlobalDungeonData.ValidTilesets.GUNGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt5");
                                break;
                            case GlobalDungeonData.ValidTilesets.CATHEDRALGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_cathedral");
                                break;
                            case GlobalDungeonData.ValidTilesets.BELLYGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_belly");
                                break;
                            case GlobalDungeonData.ValidTilesets.MINEGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_mines");
                                break;
                            case GlobalDungeonData.ValidTilesets.RATGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "ss_resourcefulrat");
                                break;
                            case GlobalDungeonData.ValidTilesets.CATACOMBGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_catacombs");
                                break;
                            case GlobalDungeonData.ValidTilesets.OFFICEGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_nakatomi");
                                break;
                            case GlobalDungeonData.ValidTilesets.WESTGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_west");
                                break;
                            case GlobalDungeonData.ValidTilesets.FORGEGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_forge");
                                break;
                            case GlobalDungeonData.ValidTilesets.HELLGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_bullethell");
                                break;
                            case GlobalDungeonData.ValidTilesets.SPACEGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_space");
                                break;
                            case GlobalDungeonData.ValidTilesets.PHOBOSGEON:
                                GameManager.Instance.DelayedLoadCustomLevel(delay, "tt_phobos");
                                break;
                            case GlobalDungeonData.ValidTilesets.FINALGEON: // Use FINALGEON to specify a name that does not have a matching tilesetID
                                GameManager.Instance.DelayedLoadCustomLevel(delay, OverrideExactLevelName);
                                break;
                        }
                    } else {
                        GameManager.Instance.DelayedLoadNextLevel(delay);
                    }
                    AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
                }
            }
            elevatorFloor.SetActive(false);
            animator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Remove(animator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(TransitionToDepart));
            animator.PlayAndDisableObject(elevatorDepartAnimName, null);
        }

        private void DeflagCells() {
            IntVector2 intVector = transform.position.IntXY(VectorConversions.Floor);
            for (int i = 0; i < 6; i++) {
                for (int j = -2; j < 6; j++) {
                    if (j != -2 || (i >= 2 && i <= 3)) {
                        if (j != -1 || (i >= 1 && i <= 4)) {
                            CellData cellData = GameManager.Instance.Dungeon.data.cellData[intVector.x + i][intVector.y + j];
                            if (j < 4) { cellData.fallingPrevented = false; }
                        }
                    }
                }
            }
        }

        private IEnumerator HandleDepartMotion() {
            Transform elevatorTransform = elevatorAnimator.transform;
            Vector3 elevatorStartDepartPosition = elevatorTransform.position;
            float elapsed = 0f;
            float duration = 0.55f;
            float yDistance = 20f;
            bool hasLayerSwapped = false;
            while (elapsed < duration) {
                if (elapsed > 0.15f && !crumblyBumblyAnimator.gameObject.activeSelf) {
                    crumblyBumblyAnimator.gameObject.SetActive(true);
                    crumblyBumblyAnimator.PlayAndDisableObject(string.Empty, null);
                }
                elapsed += BraveTime.DeltaTime;
                float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
                float yOffset = BraveMathCollege.SmoothLerp(0f, -yDistance, t);
                if (yOffset < -2f && !hasLayerSwapped) {
                    hasLayerSwapped = true;
                    elevatorAnimator.gameObject.SetLayerRecursively(LayerMask.NameToLayer("BG_Critical"));
                }
                elevatorTransform.position = elevatorStartDepartPosition + new Vector3(0f, yOffset, 0f);
                if (facewallAnimator != null) { facewallAnimator.Sprite.UpdateZDepth(); }
                yield return null;
            }
            yield break;
        }        

        private void OnElevatorTriggerEnter(SpeculativeRigidbody otherSpecRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (m_isArrived == Tribool.Ready) {
                if (otherSpecRigidbody.GetComponent<PlayerController>() != null) {
                    if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                        bool flag = true;
                        for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                            if (!GameManager.Instance.AllPlayers[i].healthHaver.IsDead) {
                                if (!sourceSpecRigidbody.ContainsPoint(GameManager.Instance.AllPlayers[i].SpriteBottomCenter.XY(), 2147483647, true)) {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if (flag) { DoDeparture(); }
                    } else {
                        DoDeparture();
                    }
                }
            }
        }

        private void Update() {
            PlayerController activePlayerClosestToPoint = GameManager.Instance.GetActivePlayerClosestToPoint(spawnTransform.position.XY(), true);
            if (activePlayerClosestToPoint != null && m_isArrived == Tribool.Unready && Vector2.Distance(spawnTransform.position.XY(), activePlayerClosestToPoint.CenterPosition) < 8f) {
                if (!GetAbsoluteParentRoom().HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { DoArrival(); }
            }
        }

        public void DoPlayerlessDeparture() {
            m_depatureIsPlayerless = true;
            m_isArrived = Tribool.Complete;
            TransitionToDoorClose(elevatorAnimator, elevatorAnimator.CurrentClip);
        }
        
        public void DoDeparture() {
            m_depatureIsPlayerless = false;
            m_isArrived = Tribool.Complete;
            if (Minimap.Instance) { Minimap.Instance.PreventAllTeleports = true; }
            if (GameManager.HasInstance && GameManager.Instance.AllPlayers != null) {
                for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                    if (GameManager.Instance.AllPlayers[i]) { GameManager.Instance.AllPlayers[i].CurrentInputState = PlayerInputState.NoInput; }
                }
            }
            TransitionToDoorClose(elevatorAnimator, elevatorAnimator.CurrentClip);
        }

        public void DoArrival() {
            m_isArrived = Tribool.Ready;
            m_hasEverArrived = true;
            StartCoroutine(HandleArrival(0f));
        }

        private IEnumerator HandleArrival(float initialDelay) {
            yield return new WaitForSeconds(initialDelay);
            elevatorAnimator.gameObject.SetActive(true);
            Transform elevatorTransform = elevatorAnimator.transform;
            Vector3 elevatorStartPosition = elevatorTransform.position;
            int cachedCeilingFrame = (!(ceilingAnimator != null)) ? -1 : ceilingAnimator.Sprite.spriteId;
            int cachedFacewallFrame = (!(facewallAnimator != null)) ? -1 : facewallAnimator.Sprite.spriteId;
            int cachedFloorframe = floorAnimator.Sprite.spriteId;
            elevatorFloor.SetActive(false);
            elevatorAnimator.Play(elevatorDescendAnimName);
            elevatorAnimator.StopAndResetFrame();
            if (ceilingAnimator != null) { ceilingAnimator.Sprite.SetSprite(cachedCeilingFrame); }
            if (facewallAnimator != null) { facewallAnimator.Sprite.SetSprite(cachedFacewallFrame); }
            floorAnimator.Sprite.SetSprite(cachedFloorframe);
            if (!m_hasEverArrived) { ToggleSprites(true); }
            float elapsed = 0f;
            float duration = 0.1f;
            float yDistance = 20f;
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                float t = elapsed / duration;
                float yOffset = Mathf.Lerp(yDistance, 0f, t);
                elevatorTransform.position = elevatorStartPosition + new Vector3(0f, yOffset, 0f);
                if (facewallAnimator != null) { facewallAnimator.Sprite.UpdateZDepth(); }
                yield return null;
            }
            GameManager.Instance.MainCameraController.DoScreenShake(arrivalShake, null, false);
            elevatorAnimator.Play();
            tk2dSpriteAnimator tk2dSpriteAnimator = elevatorAnimator;
            tk2dSpriteAnimator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Combine(tk2dSpriteAnimator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(TransitionToDoorOpen));
            ToggleSprites(false);
            if (chunker != null) { chunker.Trigger(true, new Vector3?(transform.position + new Vector3(3f, 3f, 3f))); }
            if (ceilingAnimator != null) { ceilingAnimator.Play(); }
            if (facewallAnimator != null) { facewallAnimator.Play(); }
            floorAnimator.Play();
            yield break;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

