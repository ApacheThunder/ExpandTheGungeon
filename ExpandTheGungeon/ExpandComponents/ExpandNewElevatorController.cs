using System;
using Dungeonator;
using UnityEngine;
using System.Collections;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandDungeonFlows;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandNewElevatorController : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public ExpandNewElevatorController() {
            ArriveOnSpawn = true;
            Configured = false;
            UsesOverrideTargetFloor = true;
            IsArrivalElevator = false;
            IsGlitchElevator = false;

            elevatorArriveAnimName = "arrive";
            elevatorOpenAnimName = "open";
            elevatorCloseAnimName = "close";
            elevatorDepartAnimName = "depart";
            floorBrokenSpriteName = "portable_elevator_floor_broken";
            floorBrokenAltSpriteName = "portable_elevator_floor_broken_alt";

            OverrideFloorName = "tt_phobos";

            DropHeight = 20;

            arrivalShake = new ScreenShakeSettings() {
                magnitude = 0.8f,
                speed = 8,
                time = 0.15f,
                falloff = 0.1f,
                direction = new Vector2(0, -1),
                vibrationType = ScreenShakeSettings.VibrationType.Auto,
                simpleVibrationTime = Vibration.Time.Normal,
                simpleVibrationStrength = Vibration.Strength.Medium
            };

            doorOpenShake = new ScreenShakeSettings() {
                magnitude = 0,
                speed = 0,
                time = 0.1f,
                falloff = 0,
                direction = new Vector2(1, 0),
                vibrationType = ScreenShakeSettings.VibrationType.Auto,
                simpleVibrationTime = Vibration.Time.Normal,
                simpleVibrationStrength = Vibration.Strength.Medium
            };

            doorCloseShake = new ScreenShakeSettings() {
                magnitude = 0,
                speed = 0,
                time = 0.1f,
                falloff = 0,
                direction = Vector2.zero,
                vibrationType = ScreenShakeSettings.VibrationType.Auto,
                simpleVibrationTime = Vibration.Time.Normal,
                simpleVibrationStrength = Vibration.Strength.Medium
            };

            arrivalShake = new ScreenShakeSettings() {
                magnitude = 0.35f,
                speed = 6,
                time = 0.2f,
                falloff = 0.4f,
                direction = new Vector2(0, -1),
                vibrationType = ScreenShakeSettings.VibrationType.Auto,
                simpleVibrationTime = Vibration.Time.Normal,
                simpleVibrationStrength = Vibration.Strength.Medium
            };

            m_isArrived = false;
            m_ArrivalTriggered = false;
            m_DepartureTriggered = false;
        }
        

        public bool ArriveOnSpawn;
        public bool Configured;
        public bool UsesOverrideTargetFloor;
        public bool IsArrivalElevator;
        public bool IsGlitchElevator;


        public string OverrideFloorName;

        public string elevatorArriveAnimName;
        public string elevatorOpenAnimName;
        public string elevatorCloseAnimName;
        public string elevatorDepartAnimName;
        // public string floorSpriteName;
        // public string floorAltSpriteName;

        public string floorBrokenSpriteName;
        public string floorBrokenAltSpriteName;

        public float DropHeight;
        
        public ScreenShakeSettings arrivalShake;
        public ScreenShakeSettings doorOpenShake;
        public ScreenShakeSettings doorCloseShake;
        public ScreenShakeSettings departureShake;

        public GameObject[] ImpactVFXObjects;

        private bool m_isArrived;
        private bool m_ArrivalTriggered;
        private bool m_DepartureTriggered;
        
        private SpeculativeRigidbody m_MainRigidBody;
        private GameObject m_Floor;
        private GameObject m_Elevator;
        private GameObject m_ElevatorInteriorFloor;

        private tk2dSprite m_ElevatorSprite;
        private tk2dSprite m_ElevatorFloorSprite;
        private tk2dSprite m_ElevatorInteriorFloorSprite;

        private tk2dSpriteAnimator m_ElevatorAnimator;


        private RoomHandler m_ParentRoom;

        private void Start() {  }
        
        public void ConfigureOnPlacement(RoomHandler room) {
            m_Floor = transform.Find("floor").gameObject;
            m_ElevatorFloorSprite = m_Floor.GetComponent<tk2dSprite>();

            m_Elevator = transform.Find("elevator").gameObject;
            m_ElevatorAnimator = m_Elevator.GetComponent<tk2dSpriteAnimator>();
            m_ElevatorSprite = m_Elevator.GetComponent<tk2dSprite>();
            m_Elevator.SetActive(false);

            m_ElevatorInteriorFloor = transform.Find("interiorFloor").gameObject;
            m_ElevatorInteriorFloorSprite = m_ElevatorInteriorFloor.GetComponent<tk2dSprite>();
            m_ElevatorInteriorFloor.SetActive(false);

            m_MainRigidBody = m_Floor.GetComponent<SpeculativeRigidbody>();
            if (IsArrivalElevator) {
                IntVector2 intVector = transform.position.IntXY(VectorConversions.Floor);
                for (int X = 0; X < 6; X++) {
                    for (int Y = 0; Y < 6; Y++) {
                        CellData cellData = GameManager.Instance.Dungeon.data.cellData[intVector.x + X][intVector.y + Y];
                        cellData.cellVisualData.precludeAllTileDrawing = true;
                        if (Y < 5 && Y > 0 && X > 0 && X < 5) {                            
                            cellData.type = CellType.PIT;
                            cellData.fallingPrevented = true;
                        } else {
                            cellData.type = CellType.FLOOR;
                        }
                    }
                }
                for (int X = 0; X < 6; X++) {
                    for (int Y = 0; Y < 8; Y++) {
                        CellData cellData2 = GameManager.Instance.Dungeon.data.cellData[intVector.x + X][intVector.y + Y];
                        cellData2.cellVisualData.containsObjectSpaceStamp = true;
                        cellData2.cellVisualData.containsWallSpaceStamp = true;
                    }
                }
                /*if (GameStatsManager.Instance.GetFlag(GungeonFlags.SHERPA_READY_FOR_UNLOCKS)) {
                    bool UnfinishedUnlocks = false;
                    GlobalDungeonData.ValidTilesets tilesetId = GameManager.Instance.Dungeon.tileIndices.tilesetId;
                    if (tilesetId != GlobalDungeonData.ValidTilesets.GUNGEON) {
                        if (tilesetId != GlobalDungeonData.ValidTilesets.MINEGEON) {
                            if (tilesetId != GlobalDungeonData.ValidTilesets.CATACOMBGEON) {
                                if (tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                                    if (!GameStatsManager.Instance.GetFlag(GungeonFlags.SHERPA_UNLOCK4_COMPLETE) && GameStatsManager.Instance.GetFlag(GungeonFlags.SHERPA_UNLOCK3_COMPLETE)) {
                                        UnfinishedUnlocks = true;
                                    }
                                }
                            } else if (!GameStatsManager.Instance.GetFlag(GungeonFlags.SHERPA_UNLOCK3_COMPLETE) && GameStatsManager.Instance.GetFlag(GungeonFlags.SHERPA_UNLOCK2_COMPLETE)) {
                                UnfinishedUnlocks = true;
                            }
                        } else if (!GameStatsManager.Instance.GetFlag(GungeonFlags.SHERPA_UNLOCK2_COMPLETE) && GameStatsManager.Instance.GetFlag(GungeonFlags.SHERPA_UNLOCK1_COMPLETE)) {
                            UnfinishedUnlocks = true;
                        }
                    } else if (!GameStatsManager.Instance.GetFlag(GungeonFlags.SHERPA_UNLOCK1_COMPLETE)) {
                        UnfinishedUnlocks = true;
                    }
                    if (UnfinishedUnlocks) {
                        GameObject gameObject = ResourceCache.Acquire("Global Prefabs/ElevatorMaintenanceSign") as GameObject;
                        Instantiate(gameObject, transform.position + gameObject.transform.position, Quaternion.identity);
                    }
                }*/
            } else {
                if (!ArriveOnSpawn) {
                    m_MainRigidBody.PixelColliders[0].Enabled = false;
                    m_MainRigidBody.PixelColliders[1].Enabled = false;
                    m_MainRigidBody.PixelColliders[2].Enabled = false;
                    m_MainRigidBody.PixelColliders[3].Enabled = false;
                    m_MainRigidBody.PixelColliders[4].Enabled = false;
                    m_MainRigidBody.PixelColliders[5].Enabled = false;
                    m_MainRigidBody.PixelColliders[6].Enabled = false;
                    m_MainRigidBody.PixelColliders[7].Enabled = false;

                    IntVector2 intVector = transform.position.IntXY(VectorConversions.Floor);
                    for (int X = 0; X < 6; X++) {
                        for (int Y = 0; Y < 6; Y++) {
                            CellData cellData = GameManager.Instance.Dungeon.data.cellData[intVector.x + X][intVector.y + Y];
                            if (room.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.EXIT) {
                                if (Y < 4) {
                                    cellData.type = CellType.FLOOR;
                                    cellData.cellVisualData.precludeAllTileDrawing = true;
                                } else if (X > 1 && X < 4 && Y < 5) {
                                    cellData.type = CellType.FLOOR;
                                    cellData.cellVisualData.precludeAllTileDrawing = true;
                                }
                            } else {
                                cellData.cellVisualData.precludeAllTileDrawing = true;
                                cellData.type = CellType.FLOOR;
                            }
                            cellData.isOccupied = true;
                        }
                    }
                } else {
                    m_MainRigidBody.PixelColliders[2].Enabled = false;

                    Minimap.Instance.RegisterRoomIcon(room, ExpandPrefabs.exit_room_basic.associatedMinimapIcon);
                }

                m_MainRigidBody.Reinitialize();

                SpeculativeRigidbody component = m_MainRigidBody;
                if (component) {
                    component.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(component.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(OnElevatorTriggerEnter));
                }
            }

            m_ParentRoom = room;
            
            Configured = true;
        }

        private void DeflagCells() {
            IntVector2 intVector = transform.position.IntXY(VectorConversions.Floor);
            for (int X = 0; X < 6; X++) {
                for (int Y = 0; Y < 6; Y++) {
                    CellData cellData = GameManager.Instance.Dungeon.data.cellData[intVector.x + X][intVector.y + Y];
                    if (Y < 5 && Y > 0 && X > 0 && X < 5) { cellData.fallingPrevented = false; }
                }
            }
        }

        private void Update() {
            if (!Configured) { return; }
            if (IsGlitchElevator && m_ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                return;
            }
            if (IsArrivalElevator) {
                if (m_isArrived && !m_DepartureTriggered) {
                    bool PlayerLeftElevator = true;
                    for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                        if (Vector2.Distance(transform.position.XY() + new Vector2(2, 2), GameManager.Instance.AllPlayers[i].CenterPosition) < 6f) {
                            PlayerLeftElevator = false;
                            break;
                        }
                    }
                    if (PlayerLeftElevator) { DoDeparture(); }
                }
            } else {
                if (!ArriveOnSpawn && !m_ArrivalTriggered) {
                    PlayerController activePlayerClosestToPoint = GameManager.Instance.GetActivePlayerClosestToPoint((transform.position.XY() + Vector2.one), true);
                    if (activePlayerClosestToPoint != null && !m_isArrived && Vector2.Distance((transform.position.XY() + new Vector2(2, 2)), activePlayerClosestToPoint.CenterPosition) < 8f) {
                        m_ArrivalTriggered = true;
                        DoArrival();
                    }
                } else if (!m_ArrivalTriggered) {
                    m_ArrivalTriggered = true;
                    DoArrival();
                }
            }
        }


        private void OnElevatorTriggerEnter(SpeculativeRigidbody otherSpecRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (IsArrivalElevator) { return; }
            if (Configured && m_isArrived && !m_DepartureTriggered) {
                if (otherSpecRigidbody.GetComponent<PlayerController>() != null) {
                    if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                        bool flag = true;
                        for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                            if (!GameManager.Instance.AllPlayers[i].healthHaver.IsDead) {
                                if (!sourceSpecRigidbody.ContainsPoint(GameManager.Instance.AllPlayers[i].SpriteBottomCenter.XY(), collideWithTriggers: true)) {
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


        public void DoDeparture() {
            m_isArrived = true;
            m_DepartureTriggered = true;
            if (!IsArrivalElevator) {
                if (Minimap.Instance) { Minimap.Instance.PreventAllTeleports = true; }
                if (GameManager.HasInstance && GameManager.Instance.AllPlayers != null) {
                    for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                        if (GameManager.Instance.AllPlayers[i]) { GameManager.Instance.AllPlayers[i].CurrentInputState = PlayerInputState.NoInput; }
                    }
                }
            }
            TransitionToDoorClose(m_ElevatorAnimator, m_ElevatorAnimator.CurrentClip);
        }

        public void DoArrival(float initialDelay = 0) {
            if (m_isArrived) { return; }
            m_isArrived = true;
            if (IsArrivalElevator) {
                for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                    PlayerController playerController = GameManager.Instance.AllPlayers[i];
                    playerController.ToggleGunRenderers(false, string.Empty);
                    playerController.ToggleShadowVisiblity(false);
                    playerController.ToggleHandRenderers(false, string.Empty);
                    playerController.ToggleFollowerRenderers(false);
                    playerController.SetInputOverride("elevator arrival");
                }
                StartCoroutine(HandleArrival(initialDelay));
            } else {
                if (!ArriveOnSpawn) {
                    m_MainRigidBody.PixelColliders[0].Enabled = true;
                    m_MainRigidBody.PixelColliders[1].Enabled = true;
                    m_MainRigidBody.PixelColliders[2].Enabled = true;
                    m_MainRigidBody.PixelColliders[3].Enabled = true;
                    m_MainRigidBody.PixelColliders[4].Enabled = true;
                    m_MainRigidBody.PixelColliders[5].Enabled = true;
                    m_MainRigidBody.PixelColliders[6].Enabled = true;
                    if (!IsArrivalElevator) { m_MainRigidBody.PixelColliders[7].Enabled = true; }
                    StartCoroutine(HandleArrival(0f));
                } else {
                    StartCoroutine(HandleArrival(1f));
                }
            }
        }

        private IEnumerator HandleArrival(float initialDelay) {
            if (IsArrivalElevator) {
                float ela = 0f;
                while (ela < initialDelay) {
                    ela += BraveTime.DeltaTime;
                    for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                        GameManager.Instance.AllPlayers[i].ToggleFollowerRenderers(false);
                    }
                    yield return null;
                }
            } else {
                yield return new WaitForSeconds(initialDelay);
            }
            m_Elevator.SetActive(true);
            m_Elevator.GetComponent<MeshRenderer>().enabled = true;
            Transform elevatorTransform = m_Elevator.transform;
            Vector3 elevatorStartPosition = elevatorTransform.position;
            m_ElevatorInteriorFloor.SetActive(false);
            m_ElevatorAnimator.Play(elevatorArriveAnimName);
            m_ElevatorAnimator.StopAndResetFrame();
            float elapsed = 0f;
            float duration = 0.1f;
            float yDistance = DropHeight;
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                float t = elapsed / duration;
                float yOffset = Mathf.Lerp(yDistance, 0f, t);
                elevatorTransform.position = elevatorStartPosition + new Vector3(0f, yOffset, 0f);
                yield return null;
            }
            string m_floorBrokenFrame = floorBrokenSpriteName;
            if (ArriveOnSpawn) { m_floorBrokenFrame = floorBrokenAltSpriteName; }
            m_ElevatorFloorSprite.SetSprite(m_floorBrokenFrame);
            GameManager.Instance.MainCameraController.DoScreenShake(arrivalShake, null, false);
            m_ElevatorAnimator.Play();
            tk2dSpriteAnimator tk2dSpriteAnimator = m_ElevatorAnimator;
            tk2dSpriteAnimator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Combine(tk2dSpriteAnimator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(TransitionToDoorOpen));
            DoImpactVFX();
            if (IsArrivalElevator) {
                yield return new WaitForSeconds(0.1f);
                for (int j = 0; j < GameManager.Instance.AllPlayers.Length; j++) {
                    PlayerController playerController = GameManager.Instance.AllPlayers[j];
                    playerController.ClearInputOverride("elevator arrival");
                }
            }
            yield break;
        }

        private void DoImpactVFX() {
            if (ImpactVFXObjects != null && ImpactVFXObjects.Length > 0) {
                Vector3 basePosition = transform.position;

                Vector3[] SpawnPositions = new Vector3[] {
                    new Vector3(3.5f, 0),
                    new Vector3(0.5f, 3.25f),
                    new Vector3(4, 1),
                    new Vector3(5f, 3),
                };

                foreach (Vector3 SpawnOffset in SpawnPositions) {
                    Instantiate(BraveUtility.RandomElement(ImpactVFXObjects), (basePosition + SpawnOffset), Quaternion.identity);
                }
            }
        }

        private void TransitionToDoorOpen(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip) {
            animator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Remove(animator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(TransitionToDoorOpen));
            m_ElevatorInteriorFloor.SetActive(true);
            m_MainRigidBody.PixelColliders[0].Enabled = false;
            m_MainRigidBody.PixelColliders[1].Enabled = false;
            if (!IsArrivalElevator) { m_MainRigidBody.PixelColliders[2].Enabled = true; }
            m_MainRigidBody.Reinitialize();
            GameManager.Instance.MainCameraController.DoScreenShake(doorOpenShake, null, false);
            animator.Play(elevatorOpenAnimName);            
            m_isArrived = true;
        }

        private void TransitionToDoorClose(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip) {
            GameManager.Instance.MainCameraController.DoScreenShake(doorCloseShake, null, false);
            animator.Play(elevatorCloseAnimName);
            animator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Combine(animator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(TransitionToDepart));
        }

        private void TransitionToDepart(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip) {
            animator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Remove(animator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(TransitionToDepart));
            if (!IsArrivalElevator) { animator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Combine(animator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(DepartLevel)); }
            GameManager.Instance.MainCameraController.DoDelayedScreenShake(departureShake, 0.25f, null);
            m_ElevatorInteriorFloor.SetActive(false);
            animator.PlayAndDisableObject(elevatorDepartAnimName, null);
            if (IsArrivalElevator) {
                m_MainRigidBody.PixelColliders[2].Enabled = false;
                m_MainRigidBody.PixelColliders[3].Enabled = false;
                m_MainRigidBody.PixelColliders[4].Enabled = false;
                m_MainRigidBody.PixelColliders[5].Enabled = false;
                m_MainRigidBody.PixelColliders[6].Enabled = false;
                m_MainRigidBody.Reinitialize();
                DeflagCells();
            } else {
                HidePlayers();
            }
            if (!IsArrivalElevator) {
                AkSoundEngine.PostEvent("Stop_EX_MUS_All", gameObject);
                AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
                Transform rainFXOBJ = GameManager.Instance.Dungeon.gameObject.transform.Find("ExpandJungleThunderStorm");
                if (rainFXOBJ) {
                    AkSoundEngine.PostEvent("Stop_ENV_rain_loop_01", rainFXOBJ.gameObject);
                    Destroy(rainFXOBJ.gameObject);
                }
            }
        }

        private void DepartLevel(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip) {
            animator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Remove(animator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(DepartLevel));

            Pixelator.Instance.FadeToBlack(0.5f, false, 0f);
            GameUIRoot.Instance.HideCoreUI(string.Empty);
            GameUIRoot.Instance.ToggleLowerPanels(false, false, string.Empty);
            float delay = 0.5f;

            if (!GameManager.Instance.IsFoyer && GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.NONE) {
                GlobalDungeonData.ValidTilesets nextTileset = GameManager.Instance.GetNextTileset(GameManager.Instance.Dungeon.tileIndices.tilesetId);
                GameManager.DoMidgameSave(nextTileset);
            }

            if (IsGlitchElevator) {
                ExpandSettings.glitchElevatorHasBeenUsed = true;
                StartCoroutine(ExpandUtility.DelayedGlitchLevelLoad(delay, BraveUtility.RandomElement(ExpandDungeonFlow.GlitchChestFlows), BraveUtility.RandomBool()));
            } else {
                if (UsesOverrideTargetFloor) {
                    GameManager.Instance.DelayedLoadCustomLevel(delay, OverrideFloorName);
                } else {
                    GameManager.Instance.DelayedLoadNextLevel(delay);
                }
            }
            AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
        }
        
        private void HidePlayers() {
            if (GameManager.HasInstance && GameManager.Instance.AllPlayers != null) {
                foreach (PlayerController player in GameManager.Instance.AllPlayers) { player.PrepareForSceneTransition(); }
            }
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

