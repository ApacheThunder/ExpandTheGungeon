using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandArkController : BraveBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandArkController() {
            HellCrackSprite = null;
            IsTrollChest = true;
            TrollText = "HaHa April Fools!";

            CultistCutoutNames = new List<string> {
                "cultistbaldbowbackleft_cutout",
                "cultistbaldbowbackright_cutout",
                "cultistbaldbowback_cutout",
                "cultisthoodbowback_cutout",
                "cultisthoodbowleft_cutout",
                "cultisthoodbowright_cutout",
            };
            CultistCutoutFreakoutAnim = "freakout";
            CultistNPCName = "cultistbaldbowleft_cutout";
            CultistEnemyGUID = "57255ed50ee24794b7aac1ac3cfb8a95";

            CultistCutoutDialog = new List<string>() {
                "Blasphemy! Begone with you!",
                "How could you!",
                "You monster!",
                "You'll pay for that!",
                "You were supposed to die! Not him!",
                "We will have our revenge!",
                "You'll die for this!",
                "We will pump you full of lead!",
                "How dare you!"
            };

            m_InitFinished = false;
            m_Configured = false;
        }

        public tk2dSpriteAnimator LidAnimator;
        public tk2dSpriteAnimator ChestAnimator;
        public tk2dSpriteAnimator PoofAnimator;
        public tk2dSprite LightSpriteBeam;
        public tk2dSprite HellCrackSprite;
        public Transform GunSpawnPoint;
        public GameObject GunPrefab;
        public GameObject HeldGunPrefab;
        
        public bool IsTrollChest;
        public static bool IsResettingPlayers = false;
        public string TrollText;

        public List<string> CultistCutoutNames;
        public List<string> CultistCutoutDialog;
        public string CultistCutoutFreakoutAnim;
        public string CultistNPCName;
        public string CultistEnemyGUID;


        [NonSerialized]
        public RoomHandler ParentRoom;

        [NonSerialized]
        private Transform m_heldPastGun;
        [NonSerialized]
        private GameObject minimapIconInstance;

        [NonSerialized]
        private bool m_hasBeenInteracted;

        [NonSerialized]
        protected bool m_isLocalPointing;

        [NonSerialized]
        private bool m_InitFinished;

        [NonSerialized]
        private bool m_Configured;
        
        [NonSerialized]
        private ExpandNPCController m_CultistNPC;
        
        [NonSerialized]
        private List<tk2dSpriteAnimator> m_CultistCutouts;

        [NonSerialized]
        private PlayerController m_ShotPlayer;

        public void Init() {
            ChestAnimator = gameObject.GetComponent<tk2dSpriteAnimator>();
            LidAnimator = gameObject.transform.Find("G_Lid").gameObject.GetComponent<tk2dSpriteAnimator>();
            PoofAnimator = gameObject.transform.Find("G_Poof").gameObject.GetComponent<tk2dSpriteAnimator>();
            LightSpriteBeam = gameObject.transform.Find("G_Light").gameObject.GetComponent<tk2dSprite>();
            GunSpawnPoint = gameObject.transform.Find("Spawn");
            GunPrefab = ExpandObjectDatabase.EndTimesChest.GetComponent<ArkController>().GunPrefab;
            HeldGunPrefab = ExpandObjectDatabase.EndTimesChest.GetComponent<ArkController>().HeldGunPrefab;
            m_InitFinished = true;
        }

        public void Awake() {
            if (!m_InitFinished)Init();
        }

        public void Update() {
            if (m_Configured | !m_InitFinished | Dungeon.IsGenerating | GameManager.Instance.IsLoadingLevel | ParentRoom == null) return;
            if (ParentRoom != null) ParentRoom = GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(transform.position.IntXY(VectorConversions.Floor));
            if (IsTrollChest && CultistCutoutNames != null && CultistCutoutNames.Count > 0) {
                int m_RoomChilds = 0;
                if (ParentRoom.hierarchyParent?.childCount > 0) {
                    m_CultistCutouts = new List<tk2dSpriteAnimator>();
                    m_RoomChilds = ParentRoom.hierarchyParent.childCount;
                    for (int i = 0; i < m_RoomChilds; i++) {
                        foreach (string cultist in CultistCutoutNames) {
                            if (ParentRoom.hierarchyParent.GetChild(i).gameObject.name.ToLower().StartsWith(cultist)) {
                                if (ParentRoom.hierarchyParent.GetChild(i).gameObject.GetComponent<tk2dSpriteAnimator>() && !ParentRoom.hierarchyParent.GetChild(i).gameObject.GetComponent<TalkDoerLite>()) {
                                    m_CultistCutouts.Add(ParentRoom.hierarchyParent.GetChild(i).gameObject.GetComponent<tk2dSpriteAnimator>());
                                    break;
                                }
                            } else if (ParentRoom.hierarchyParent.GetChild(i).gameObject.name.ToLower().StartsWith(CultistNPCName) &&
                              ParentRoom.hierarchyParent.GetChild(i).gameObject.GetComponent<TalkDoerLite>()
                          ) {
                                m_CultistNPC = ParentRoom.hierarchyParent.GetChild(i).gameObject.AddComponent<ExpandNPCController>();
                                m_CultistNPC.ConfigureOnPlacement(ParentRoom);
                                break;
                            }
                        }
                    }
                }
            } else {
                RoomHandler.unassignedInteractableObjects.Add(this);
            }
            m_Configured = true;
        }
        
        public float GetDistanceToPoint(Vector2 point) {
            if (m_hasBeenInteracted) { return 100000f; }
            return Vector2.Distance(point, specRigidbody.UnitCenter) / 2f;
        }

        public void OnEnteredRange(PlayerController interactor) {
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white);
            SpriteOutlineManager.AddOutlineToSprite(LidAnimator.sprite, Color.white);
        }

        public void OnExitRange(PlayerController interactor) {
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, true);
            SpriteOutlineManager.RemoveOutlineFromSprite(LidAnimator.sprite, true);
        }

        public void Interact(PlayerController interactor) {
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            SpriteOutlineManager.RemoveOutlineFromSprite(LidAnimator.sprite, false);
            if (!m_hasBeenInteracted)m_hasBeenInteracted = true;
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) { GameManager.Instance.AllPlayers[i].RemoveBrokenInteractable(this); }
            BraveInput.DoVibrationForAllPlayers(Vibration.Time.Normal, Vibration.Strength.Medium);
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(interactor);
                float num = Vector2.Distance(otherPlayer.CenterPosition, interactor.CenterPosition);
                if (num > 8f || num < 0.75f) {
                    Vector2 a = Vector2.right;
                    if (interactor.CenterPosition.x < ChestAnimator.sprite.WorldCenter.x) { a = Vector2.left; }
                    otherPlayer.WarpToPoint(otherPlayer.transform.position.XY() + a * 2f, true, false);
                }
            }
            StartCoroutine(Open(interactor));
            if (IsTrollChest && m_CultistNPC)m_CultistNPC.InteractPlayerless(ExpandNPCController.CultistDialogState.OpenedChest);
        }

        private IEnumerator HandleLightSprite() {
            yield return new WaitForSeconds(0.5f);
            float elapsed = 0f;
            float duration = 1f;
            LightSpriteBeam.renderer.enabled = true;
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
                LightSpriteBeam.transform.localScale = new Vector3(1f, Mathf.Lerp(0f, 1f, t), 1f);
                LightSpriteBeam.transform.localPosition = new Vector3(0f, Mathf.Lerp(1.375f, 0f, t), 0f);
                LightSpriteBeam.UpdateZDepth();
                yield return null;
            }
            yield break;
        }

        private IEnumerator Open(PlayerController interactor) {
            if (IsTrollChest) { DeregisterChestOnMinimap(); }
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                if (GameManager.Instance.AllPlayers[i].healthHaver.IsAlive) { GameManager.Instance.AllPlayers[i].SetInputOverride("fakeArk"); }
            }
            LidAnimator.Play();
            ChestAnimator.Play();
            PoofAnimator.PlayAndDisableObject(string.Empty, null);
            specRigidbody.Reinitialize();
            GameManager.Instance.MainCameraController.OverrideRecoverySpeed = 2f;
            GameManager.Instance.MainCameraController.OverridePosition = ChestAnimator.sprite.WorldCenter + new Vector2(0f, 2f);
            GameManager.Instance.MainCameraController.SetManualControl(true, true);
            StartCoroutine(HandleLightSprite());
            while (LidAnimator.IsPlaying(LidAnimator.CurrentClip)) { yield return null; }
            yield return StartCoroutine(HandleGun(interactor));
            yield return new WaitForSeconds(0.5f);
            Pixelator.Instance.DoFinalNonFadedLayer = true;
            yield return StartCoroutine(HandleClockhair(interactor));
            interactor.ClearInputOverride("fakeArk");
            yield break;
        }

        private Vector2 GetTargetClockhairPosition(BraveInput input, Vector2 currentClockhairPosition) {
            Vector2 rhs;
            if (input.IsKeyboardAndMouse(false)) {
                rhs = GameManager.Instance.MainCameraController.Camera.ScreenToWorldPoint(Input.mousePosition).XY() + new Vector2(0.375f, -0.25f);
            } else {
                rhs = currentClockhairPosition + input.ActiveActions.Aim.Vector * 10f * BraveTime.DeltaTime;
            }
            rhs = Vector2.Max(GameManager.Instance.MainCameraController.MinVisiblePoint, rhs);
            return Vector2.Min(GameManager.Instance.MainCameraController.MaxVisiblePoint, rhs);
        }

        private void UpdateCameraPositionDuringClockhair(Vector2 targetPosition) {
            float num = Vector2.Distance(targetPosition, ChestAnimator.sprite.WorldCenter);
            if (num > 8f) { targetPosition = ChestAnimator.sprite.WorldCenter; }
            Vector2 vector = GameManager.Instance.MainCameraController.OverridePosition;
            if (Vector2.Distance(vector, targetPosition) > 10f) { vector = GameManager.Instance.MainCameraController.transform.position.XY(); }
            GameManager.Instance.MainCameraController.OverridePosition = Vector3.MoveTowards(vector, targetPosition, BraveTime.DeltaTime);
        }

        private bool CheckPlayerTarget(PlayerController target, Transform clockhairTransform) {
            if (target.IsGhost | target.healthHaver.IsDead) return false;
            Vector2 a = clockhairTransform.position.XY() + new Vector2(-0.375f, 0.25f);
            return Vector2.Distance(a, target.CenterPosition) < 0.625f;
        }

        private bool CheckNPCTarget(ExpandNPCController target, Transform clockhairTransform) {
            Vector2 a = clockhairTransform.position.XY() + new Vector2(-0.375f, 0.25f);
            return Vector2.Distance(a, target.sprite.WorldCenter) < 0.625f;
        }

        private bool CheckEnemyTarget(AIActor target, Transform clockhairTransform) {
            if (target.IsGone | target.healthHaver.IsDead) return false;
            Vector2 a = clockhairTransform.position.XY() + new Vector2(-0.375f, 0.25f);
            return Vector2.Distance(a, target.CenterPosition) < 0.625f;
        }

        private bool CheckHellTarget(tk2dBaseSprite hellTarget, Transform clockhairTransform) {
            if (hellTarget == null) { return false; }
            Vector2 a = clockhairTransform.position.XY() + new Vector2(-0.375f, 0.25f);
            return Vector2.Distance(a, hellTarget.WorldCenter) < 0.625f;
        }

        public void HandleHeldGunSpriteFlip(bool flipped) {
            tk2dSprite component = m_heldPastGun.GetComponent<tk2dSprite>();
            if (flipped) {
                if (!component.FlipY) { component.FlipY = true; }
            } else if (component.FlipY) {
                component.FlipY = false;
            }
            Transform transform = m_heldPastGun.Find("PrimaryHand");
            m_heldPastGun.localPosition = -transform.localPosition;
            if (flipped) {
                m_heldPastGun.localPosition = Vector3.Scale(m_heldPastGun.localPosition, new Vector3(1f, -1f, 1f));
            }
            m_heldPastGun.localPosition = BraveUtility.QuantizeVector(m_heldPastGun.localPosition, 16f);
            component.ForceRotationRebuild();
            component.UpdateZDepth();
        }

        private void PointGunAtClockhair(PlayerController interactor, Transform clockhairTransform) {
            Vector2 centerPosition = interactor.CenterPosition;
            Vector2 vector = clockhairTransform.position.XY() - centerPosition;
            if (m_isLocalPointing && vector.sqrMagnitude > 9f) {
                m_isLocalPointing = false;
            } else if (m_isLocalPointing || vector.sqrMagnitude < 4f) {
                m_isLocalPointing = true;
                float t = vector.sqrMagnitude / 4f - 0.05f;
                vector = Vector2.Lerp(Vector2.right, vector, t);
            }
            float num = BraveMathCollege.Atan2Degrees(vector);
            num = num.Quantize(3f);
            interactor.GunPivot.rotation = Quaternion.Euler(0f, 0f, num);
            interactor.ForceIdleFacePoint(vector, false);
            HandleHeldGunSpriteFlip(interactor.SpriteFlipped);
        }

        private IEnumerator HandleClockhair(PlayerController interactor) {
            Transform clockhairTransform = Instantiate(BraveResources.Load<GameObject>("Clockhair", ".prefab")).transform;
            ClockhairController clockhair = clockhairTransform.GetComponent<ClockhairController>();
            float elapsed = 0f;
            float duration = clockhair.ClockhairInDuration;
            Vector3 clockhairTargetPosition = interactor.CenterPosition;
            Vector3 clockhairStartPosition = clockhairTargetPosition + new Vector3(-20f, 5f, 0f);
            clockhair.renderer.enabled = true;
            clockhair.spriteAnimator.alwaysUpdateOffscreen = true;
            clockhair.spriteAnimator.Play("clockhair_intro");
            clockhair.hourAnimator.Play("hour_hand_intro");
            clockhair.minuteAnimator.Play("minute_hand_intro");
            clockhair.secondAnimator.Play("second_hand_intro");
            BraveInput currentInput = BraveInput.GetInstanceForPlayer(interactor.PlayerIDX);
            while (elapsed < duration) {
                UpdateCameraPositionDuringClockhair(interactor.CenterPosition);
                if (GameManager.INVARIANT_DELTA_TIME == 0f) { elapsed += 0.05f; }
                elapsed += GameManager.INVARIANT_DELTA_TIME;
                float t = elapsed / duration;
                float smoothT = Mathf.SmoothStep(0f, 1f, t);
                clockhairTargetPosition = GetTargetClockhairPosition(currentInput, clockhairTargetPosition);
                Vector3 currentPosition = Vector3.Slerp(clockhairStartPosition, clockhairTargetPosition, smoothT);
                clockhairTransform.position = currentPosition.WithZ(0f);
                if (t > 0.5f) {
                    clockhair.renderer.enabled = true;
                }
                if (t > 0.75f) {
                    clockhair.hourAnimator.GetComponent<Renderer>().enabled = true;
                    clockhair.minuteAnimator.GetComponent<Renderer>().enabled = true;
                    clockhair.secondAnimator.GetComponent<Renderer>().enabled = true;
                    GameCursorController.CursorOverride.SetOverride("fakeArk", true, null);
                }
                clockhair.sprite.UpdateZDepth();
                PointGunAtClockhair(interactor, clockhairTransform);
                yield return null;
            }
            clockhair.SetMotionType(1f);
            float shotTargetTime = 0f;
            float holdDuration = 4f;
            PlayerController shotPlayer = interactor;
            AIActor m_EnemyTarget = null;
            AIActor[] m_Enemies = null;
            ExpandNPCController m_NPCTarget = null;
            if (IsTrollChest) m_Enemies = FindObjectsOfType<AIActor>();
            bool didShootHellTrigger = false;
            Vector3 lastJitterAmount = Vector3.zero;
            bool m_isPlayingChargeAudio = false;
            bool isTargetingEnemy = false;
            bool isTargetingNPC = false;
            for (;;) {
                UpdateCameraPositionDuringClockhair(interactor.CenterPosition);
                clockhair.transform.position = clockhair.transform.position - lastJitterAmount;
                clockhair.transform.position = GetTargetClockhairPosition(currentInput, clockhair.transform.position.XY());
                clockhair.sprite.UpdateZDepth();
                bool isTargetingValidTarget = CheckPlayerTarget(interactor, clockhairTransform);
                if (isTargetingValidTarget)shotPlayer = interactor;
                if (IsTrollChest && !isTargetingValidTarget) {
                    if (m_Enemies != null && m_Enemies.Length > 0) {
                        for (int i = 0; i < m_Enemies.Length; i++) {
                            if (m_Enemies[i] && m_Enemies[i].gameObject.activeInHierarchy && m_Enemies[i].gameObject.activeSelf &&
                                !m_Enemies[i].IsGone && m_Enemies[i].healthHaver && !m_Enemies[i].healthHaver.IsDead)
                            {
                                isTargetingEnemy = CheckEnemyTarget(m_Enemies[i], clockhairTransform);
                                if (isTargetingEnemy) {
                                    m_EnemyTarget = m_Enemies[i];
                                    m_NPCTarget = null;
                                    isTargetingNPC = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (!isTargetingEnemy) {
                        m_EnemyTarget = null;
                        m_NPCTarget = null;
                        isTargetingNPC = false;
                        if (m_CultistNPC) {
                            isTargetingNPC = CheckNPCTarget(m_CultistNPC, clockhairTransform);
                            if (isTargetingNPC) { m_NPCTarget = m_CultistNPC; } else { m_NPCTarget = null; }
                        }
                    }
                } else {
                    m_NPCTarget = null;
                    m_EnemyTarget = null;
                    isTargetingNPC = false;
                    isTargetingEnemy = false;
                }
                if (!isTargetingValidTarget && !isTargetingEnemy && !isTargetingNPC && GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                    isTargetingValidTarget = CheckPlayerTarget(GameManager.Instance.GetOtherPlayer(interactor), clockhairTransform);
                    shotPlayer = GameManager.Instance.GetOtherPlayer(interactor);
                }
                if (!isTargetingValidTarget && !isTargetingEnemy && !isTargetingNPC && GameStatsManager.Instance.AllCorePastsBeaten()) {
                    isTargetingValidTarget = CheckHellTarget(HellCrackSprite, clockhairTransform);
                    didShootHellTrigger = isTargetingValidTarget;
                }
                if (isTargetingValidTarget | isTargetingEnemy | isTargetingNPC) { clockhair.SetMotionType(-10f); } else { clockhair.SetMotionType(1f); }
                if ((currentInput.ActiveActions.ShootAction.IsPressed || currentInput.ActiveActions.InteractAction.IsPressed) && (isTargetingValidTarget | isTargetingEnemy | isTargetingNPC)) {
                    if (!m_isPlayingChargeAudio) {
                        m_isPlayingChargeAudio = true;
                        AkSoundEngine.PostEvent("Play_OBJ_pastkiller_charge_01", gameObject);
                    }
                    shotTargetTime += BraveTime.DeltaTime;
                } else {
                    shotTargetTime = Mathf.Max(0f, shotTargetTime - BraveTime.DeltaTime * 3f);
                    if (m_isPlayingChargeAudio) {
                        m_isPlayingChargeAudio = false;
                        AkSoundEngine.PostEvent("Stop_OBJ_pastkiller_charge_01", gameObject);
                    }
                }
                if ((currentInput.ActiveActions.ShootAction.WasReleased || currentInput.ActiveActions.InteractAction.WasReleased) && (isTargetingValidTarget | isTargetingEnemy | isTargetingNPC) && shotTargetTime > holdDuration && !GameManager.Instance.IsPaused) {
                    if (IsTrollChest)currentInput.ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                    break;
                }
                if (shotTargetTime > 0f) {
                    float distortionPower = Mathf.Lerp(0f, 0.35f, shotTargetTime / holdDuration);
                    float distortRadius = 0.5f;
                    float edgeRadius = Mathf.Lerp(4f, 7f, shotTargetTime / holdDuration);
                    clockhair.UpdateDistortion(distortionPower, distortRadius, edgeRadius);
                    float desatRadiusUV = Mathf.Lerp(2f, 0.25f, shotTargetTime / holdDuration);
                    clockhair.UpdateDesat(true, desatRadiusUV);
                    shotTargetTime = Mathf.Min(holdDuration + 0.25f, shotTargetTime + BraveTime.DeltaTime);
                    float d = Mathf.Lerp(0f, 0.5f, (shotTargetTime - 1f) / (holdDuration - 1f));
                    Vector3 vector = (UnityEngine.Random.insideUnitCircle * d).ToVector3ZUp(0f);
                    BraveInput.DoSustainedScreenShakeVibration(shotTargetTime / holdDuration * 0.8f);
                    clockhair.transform.position = clockhair.transform.position + vector;
                    lastJitterAmount = vector;
                    clockhair.SetMotionType(Mathf.Lerp(-10f, -2400f, shotTargetTime / holdDuration));
                } else {
                    lastJitterAmount = Vector3.zero;
                    clockhair.UpdateDistortion(0f, 0f, 0f);
                    clockhair.UpdateDesat(false, 0f);
                    shotTargetTime = 0f;
                    BraveInput.DoSustainedScreenShakeVibration(0f);
                }
                PointGunAtClockhair(interactor, clockhairTransform);
                yield return null;
            }
            BraveInput.DoSustainedScreenShakeVibration(0f);
            BraveInput.DoVibrationForAllPlayers(Vibration.Time.Normal, Vibration.Strength.Hard);
            clockhair.StartCoroutine(clockhair.WipeoutDistortionAndFade(0.5f));
            clockhair.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Unoccluded"));
            if (!IsTrollChest)Pixelator.Instance.FadeToColor(1f, Color.white, true, 0.2f);
            Pixelator.Instance.DoRenderGBuffer = false;
            clockhair.spriteAnimator.Play("clockhair_fire");
            clockhair.hourAnimator.GetComponent<Renderer>().enabled = false;
            clockhair.minuteAnimator.GetComponent<Renderer>().enabled = false;
            clockhair.secondAnimator.GetComponent<Renderer>().enabled = false;
            yield return null;
            if (IsTrollChest) {
                Destroy(m_heldPastGun.gameObject);
                interactor.ToggleGunRenderers(true, "fakeArk");
                GameCursorController.CursorOverride.RemoveOverride("fakeArk");
                Pixelator.Instance.LerpToLetterbox(0.35f, 0.25f);
                yield return StartCoroutine(HandleCrosshairShot(shotPlayer, m_EnemyTarget, m_NPCTarget));
                yield break;
            }
            yield return null;
            TimeTubeCreditsController ttcc = new TimeTubeCreditsController();
            bool isShortTunnel = didShootHellTrigger || shotPlayer.characterIdentity == PlayableCharacters.CoopCultist || CharacterStoryComplete(shotPlayer.characterIdentity);
            Destroy(m_heldPastGun.gameObject);
            interactor.ToggleGunRenderers(true, "fakeArk");
            GameCursorController.CursorOverride.RemoveOverride("fakeArk");
            Pixelator.Instance.LerpToLetterbox(0.35f, 0.25f);
            yield return StartCoroutine(ttcc.HandleTimeTubeCredits(clockhair.sprite.WorldCenter, isShortTunnel, clockhair.spriteAnimator, (!didShootHellTrigger) ? shotPlayer.PlayerIDX : 0, false));
            if (!IsTrollChest) {
                if (isShortTunnel) {
                    Pixelator.Instance.FadeToBlack(1f, false, 0f);
                    yield return new WaitForSeconds(1f);
                }
                if (didShootHellTrigger) {
                    GameManager.DoMidgameSave(GlobalDungeonData.ValidTilesets.HELLGEON);
                    GameManager.Instance.LoadCustomLevel("tt_bullethell");
                } else if (shotPlayer.characterIdentity == PlayableCharacters.CoopCultist) {
                    GameManager.IsCoopPast = true;
                    ResetPlayers(false);
                    GameManager.Instance.LoadCustomLevel("fs_coop");
                } else if (CharacterStoryComplete(shotPlayer.characterIdentity) && shotPlayer.characterIdentity == PlayableCharacters.Gunslinger) {
                    GameManager.DoMidgameSave(GlobalDungeonData.ValidTilesets.FINALGEON);
                    GameManager.IsGunslingerPast = true;
                    ResetPlayers(true);
                    GameManager.Instance.LoadCustomLevel("tt_bullethell");
                } else if (CharacterStoryComplete(shotPlayer.characterIdentity)) {
                    bool flag = false;
                    GameManager.DoMidgameSave(GlobalDungeonData.ValidTilesets.FINALGEON);
                    switch (shotPlayer.characterIdentity) {
                        case PlayableCharacters.Pilot:
                            flag = true;
                            ResetPlayers(false);
                            GameManager.Instance.LoadCustomLevel("fs_pilot");
                            break;
                        case PlayableCharacters.Convict:
                            flag = true;
                            ResetPlayers(false);
                            GameManager.Instance.LoadCustomLevel("fs_convict");
                            break;
                        case PlayableCharacters.Robot:
                            flag = true;
                            ResetPlayers(false);
                            GameManager.Instance.LoadCustomLevel("fs_robot");
                            break;
                        case PlayableCharacters.Soldier:
                            flag = true;
                            ResetPlayers(false);
                            GameManager.Instance.LoadCustomLevel("fs_soldier");
                            break;
                        case PlayableCharacters.Guide:
                            flag = true;
                            ResetPlayers(false);
                            GameManager.Instance.LoadCustomLevel("fs_guide");
                            break;
                        case PlayableCharacters.Bullet:
                            flag = true;
                            ResetPlayers(false);
                            GameManager.Instance.LoadCustomLevel("fs_bullet");
                            break;
                    }
                    if (!flag) {
                        AmmonomiconController.Instance.OpenAmmonomicon(true, true);
                    } else {
                        GameUIRoot.Instance.ToggleUICamera(false);
                    }
                } else {
                    AmmonomiconController.Instance.OpenAmmonomicon(true, true);
                }
            }
            
            for (;;) { yield return null; }
            // yield break;
        }

        private void ResetPlayers(bool isGunslingerPast = false, bool survivedTrollChest = false) {
            IsResettingPlayers = true;
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                if (GameManager.Instance.AllPlayers[i].healthHaver.IsAlive | survivedTrollChest) {
                    if (!isGunslingerPast && !survivedTrollChest) {
                        GameManager.Instance.AllPlayers[i].ResetToFactorySettings(true, true, false);
                        GameManager.Instance.AllPlayers[i].CharacterUsesRandomGuns = false;
                    }
                    GameManager.Instance.AllPlayers[i].IsVisible = true;
                    GameManager.Instance.AllPlayers[i].ClearInputOverride("fakeArk");
                    GameManager.Instance.AllPlayers[i].ClearAllInputOverrides();
                }
            }
            IsResettingPlayers = false;
        }
        
        private void DestroyPlayers() {
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) { Destroy(GameManager.Instance.AllPlayers[i].gameObject); }
        }

        private bool CharacterStoryComplete(PlayableCharacters shotCharacter) {
            return GameStatsManager.Instance.GetFlag(GungeonFlags.BLACKSMITH_BULLET_COMPLETE) && GameManager.Instance.PrimaryPlayer.PastAccessible;
        }

        private void SpawnVFX(string vfxResourcePath, Vector2 pos) {
            GameObject original = (GameObject)BraveResources.Load(vfxResourcePath, typeof(GameObject), ".prefab");
            GameObject gameObject = Instantiate(original);
            tk2dSprite component = gameObject.GetComponent<tk2dSprite>();
            component.PlaceAtPositionByAnchor(pos, tk2dBaseSprite.Anchor.MiddleCenter);
            component.UpdateZDepth();
        }

        private IEnumerator HandleGun(PlayerController interactor) {
            interactor.ToggleGunRenderers(false, "fakeArk");
            GameObject instanceGun = Instantiate(GunPrefab, GunSpawnPoint.position, Quaternion.identity);
            Material gunMaterial = instanceGun.transform.Find("GunThatCanKillThePast").GetComponent<MeshRenderer>().sharedMaterial;
            tk2dSprite instanceGunSprite = instanceGun.transform.Find("GunThatCanKillThePast").GetComponent<tk2dSprite>();
            instanceGunSprite.HeightOffGround = 5f;
            gunMaterial.SetColor("_OverrideColor", Color.white);
            float elapsed = 0f;
            float raiseTime = 4f;
            Vector3 targetMidHeightPosition = GunSpawnPoint.position + new Vector3(0f, 6.5f, 0f);
            interactor.ForceIdleFacePoint(new Vector2(1f, -1f), false);
            while (elapsed < raiseTime) {
                elapsed += BraveTime.DeltaTime;
                float t = Mathf.Clamp01(elapsed / raiseTime);
                t = BraveMathCollege.LinearToSmoothStepInterpolate(0f, 1f, t);
                instanceGun.transform.position = Vector3.Lerp(GunSpawnPoint.position, targetMidHeightPosition, t);
                instanceGun.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 2f, t);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            while (instanceGunSprite.spriteAnimator.CurrentFrame != 0) { yield return null; }
            instanceGunSprite.spriteAnimator.Pause();
            Pixelator.Instance.FadeToColor(0.2f, Color.white, true, 0.2f);
            yield return new WaitForSeconds(0.1f);
            Transform burstObject = instanceGun.transform.Find("GTCKTP_Burst");
            if (burstObject != null) { burstObject.gameObject.SetActive(true); }
            BraveInput.DoVibrationForAllPlayers(Vibration.Time.Slow, Vibration.Strength.Medium);
            yield return new WaitForSeconds(0.2f);
            instanceGunSprite.spriteAnimator.Resume();
            elapsed = 0f;
            float fadeTime = 1f;
            while (elapsed < fadeTime) {
                elapsed += BraveTime.DeltaTime;
                float t2 = Mathf.Clamp01(elapsed / fadeTime);
                gunMaterial.SetColor("_OverrideColor", Color.Lerp(Color.white, new Color(1f, 1f, 1f, 0f), t2));
                yield return null;
            }
            yield return new WaitForSeconds(2f);
            elapsed = 0f;
            float reraiseTime = 2f;
            while (elapsed < reraiseTime) {
                elapsed += BraveTime.DeltaTime;
                float t3 = Mathf.Clamp01(elapsed / reraiseTime);
                t3 = BraveMathCollege.SmoothStepToLinearStepInterpolate(0f, 1f, t3);
                instanceGun.transform.position = Vector3.Lerp(targetMidHeightPosition, interactor.CenterPosition.ToVector3ZUp(targetMidHeightPosition.z - 10f), t3);
                instanceGun.transform.localScale = Vector3.Lerp(Vector3.one * 2f, Vector3.one, t3);
                yield return null;
            }
            GameObject pickupVFXPrefab = ResourceCache.Acquire("Global VFX/VFX_Item_Pickup") as GameObject;
            interactor.PlayEffectOnActor(pickupVFXPrefab, Vector3.zero, true, false, false);
            GameObject instanceEquippedGun = Instantiate(HeldGunPrefab);
            AkSoundEngine.PostEvent("Play_OBJ_weapon_pickup_01", gameObject);
            tk2dSprite instanceEquippedSprite = instanceEquippedGun.GetComponent<tk2dSprite>();
            instanceEquippedSprite.HeightOffGround = 2f;
            instanceEquippedSprite.attachParent = interactor.sprite;
            m_heldPastGun = instanceEquippedGun.transform;
            m_heldPastGun.parent = interactor.GunPivot;
            Transform primaryHandXform = m_heldPastGun.Find("PrimaryHand");
            m_heldPastGun.localRotation = Quaternion.identity;
            m_heldPastGun.localPosition = -primaryHandXform.localPosition;
            instanceEquippedSprite.UpdateZDepth();
            Destroy(instanceGun);
            yield break;
        }

        private IEnumerator HandleCrosshairShot(PlayerController interactor, AIActor EnemyTarget = null, ExpandNPCController m_CultistTarget = null) {
            bool m_KilledCultist = false;
            bool m_KilledCompanion = false;
            bool m_ExtraLifeTriggered = false;
            bool m_SecondPlayerExistsAndIsGhost = (GameManager.Instance.GetOtherPlayer(interactor) && GameManager.Instance.GetOtherPlayer(interactor).IsGhost);
            bool m_SecondPlayerExistsAndIsNotGhost = (GameManager.Instance.GetOtherPlayer(interactor) && !GameManager.Instance.GetOtherPlayer(interactor).IsGhost);
            bool m_AllowExtraLife = (DateTime.Now.Day != 1 && DateTime.Now.Month != 4);
            if (m_SecondPlayerExistsAndIsNotGhost) m_AllowExtraLife = false;
            m_ShotPlayer = interactor;
            if (EnemyTarget) {
                m_KilledCompanion = true;
                Vector2 m_EnemyTargetPosition = EnemyTarget.sprite.WorldCenter;
                yield return null;
                EnemyTarget.healthHaver.lastIncurredDamageSource = TrollText;
                EnemyTarget.healthHaver.ForceSetCurrentHealth(0);
                EnemyTarget.healthHaver.Die(Vector2.zero);
                yield return null;
                HandleAIActorLoot(m_EnemyTargetPosition);
            } else if (!m_CultistTarget) {
                if (m_AllowExtraLife) {
                    m_ExtraLifeTriggered = true;
                    interactor.healthHaver.OnPreDeath += HandlePreDeath;
                } else if (m_SecondPlayerExistsAndIsNotGhost) {
                    m_KilledCompanion = true;
                }
                Vector2 m_CompanionTargetPosition = interactor.sprite.WorldCenter;
                yield return null;
                interactor.healthHaver.lastIncurredDamageSource = TrollText;
                interactor.healthHaver.ForceSetCurrentHealth(0);
                interactor.healthHaver.Armor = 0;
                interactor.healthHaver.Die(Vector2.zero);
                if (m_KilledCompanion)HandleAIActorLoot(m_CompanionTargetPosition);
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
            if (m_KilledCompanion | EnemyTarget | m_CultistTarget) {
                Pixelator.Instance.DoFinalNonFadedLayer = false;
                LightSpriteBeam.renderer.enabled = false;
                if (m_CultistTarget && ParentRoom != null) {
                    m_CultistTarget.Kill();
                    m_KilledCultist = true;
                    int RandomCultistDialogCount = UnityEngine.Random.Range(2, 4);
                    yield return new WaitForSeconds(2f);
                    if (m_CultistCutouts != null && m_CultistCutouts.Count > 0) {
                        List<Transform> m_CultistDialogTargets = new List<Transform>();
                        foreach (tk2dSpriteAnimator cultist in m_CultistCutouts) {
                            cultist.Play(CultistCutoutFreakoutAnim);
                            m_CultistDialogTargets.Add(cultist.transform);
                            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.25f));
                        }
                        foreach (Transform cultistTransform in m_CultistDialogTargets) {
                            CultistCutoutDialog = CultistCutoutDialog.Shuffle();
                            string m_SelectedDialog = BraveUtility.RandomElement(CultistCutoutDialog);
                            if (CultistCutoutDialog.Count > 1) {
                                CultistCutoutDialog.Remove(m_SelectedDialog);
                                CultistCutoutDialog = CultistCutoutDialog.Shuffle();
                            }
                            TextBoxManager.ShowTextBox(cultistTransform.position + new Vector3(-0.9375f, 1.375f), cultistTransform, UnityEngine.Random.Range(3f, 4.5f), m_SelectedDialog, instant: false, slideOrientation: TextBoxManager.BoxSlideOrientation.FORCE_RIGHT, showContinueText: false);
                            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
                            RandomCultistDialogCount--;
                            if (RandomCultistDialogCount <= 0) break;
                        }
                        yield return new WaitForSeconds(UnityEngine.Random.Range(3, 4));
                        foreach (tk2dSpriteAnimator cultist in m_CultistCutouts) {
                            cultist.Stop();
                            if (cultist.sprite?.renderer) cultist.sprite.renderer.enabled = false;
                            cultist.enabled = false;
                            if (cultist.specRigidbody) cultist.specRigidbody.enabled = false;
                            AIActor m_EnemyCultist = AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(CultistEnemyGUID), cultist.sprite.WorldBottomCenter, ParentRoom, false, AIActor.AwakenAnimationType.Awaken, true);
                            if (m_EnemyCultist) m_EnemyCultist.procedurallyOutlined = false;
                            TextBoxManager.ClearTextBox(cultist.transform);
                        }
                        while (!ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) yield return null;
                        ParentRoom.SealRoom();
                        for (int i = 0; i < m_CultistCutouts.Count; i++) Destroy(m_CultistCutouts[i].gameObject);
                        m_CultistCutouts.Clear();
                        yield return null;
                    }
                }
            }
            if (m_KilledCompanion) {
                m_CultistNPC.InteractPlayerless(ExpandNPCController.CultistDialogState.ShotCompanion);
            } else if (!m_KilledCultist && m_ExtraLifeTriggered) {
                m_CultistNPC.InteractPlayerless(ExpandNPCController.CultistDialogState.PlayerShot);
            }
            GameManager.Instance.MainCameraController.SetManualControl(false, false);
            Pixelator.Instance.LerpToLetterbox(1, 0.25f);
            ResetPlayers(false, true);
            if (ParentRoom != null) {
                ParentRoom.DeregisterInteractable(this);
                if (m_KilledCultist) {
                    while (ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) yield return null;
                    ParentRoom.UnsealRoom();
                }
            }
            Destroy(this);           
            yield break;
        }

        private void HandlePreDeath(Vector2 damageDirection) {
            if (!m_ShotPlayer) return;
            m_ShotPlayer.healthHaver.OnPreDeath -= HandlePreDeath;
            if (m_ShotPlayer.IsInMinecart)m_ShotPlayer.currentMineCart.EvacuateSpecificPlayer(m_ShotPlayer, true);
            foreach (PassiveItem passive in m_ShotPlayer.passiveItems) {
                if ((passive is CompanionItem) && (passive as CompanionItem).DisplayName == "Pig")return;
                if ((passive is ExtraLifeItem) && (passive as ExtraLifeItem).extraLifeMode == ExtraLifeItem.ExtraLifeMode.DARK_SOULS) return;
            }
            m_ShotPlayer.HandleCloneItem(null);
        }

        private void HandleAIActorLoot(Vector2 targetPosition) {
            PickupObject.ItemQuality targetQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.C : PickupObject.ItemQuality.B) : PickupObject.ItemQuality.A;
            GenericLootTable lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
            PickupObject item = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
            if (item) {
                List<int> m_AllowedItemsInRainbowMode = new List<int>() {
                    GlobalItemIds.SmallHeart,
                    GlobalItemIds.FullHeart,
                    GlobalItemIds.AmmoPickup,
                    GlobalItemIds.SpreadAmmoPickup,
                    GlobalItemIds.Spice,
                    GlobalItemIds.Junk,
                    GlobalItemIds.GoldJunk,
                    GlobalItemIds.Key,
                    GlobalItemIds.GlassGuonStone,
                    GlobalItemIds.Junk,
                    GlobalItemIds.GoldJunk,
                    GlobalItemIds.SackKnightBoon,
                    GlobalItemIds.Blank,
                    GlobalItemIds.Map,
                    120 // armor
                };
                if (!GameStatsManager.Instance.IsRainbowRun | m_AllowedItemsInRainbowMode.Contains(item.PickupObjectId)) {
                    LootEngine.SpawnItem(item.gameObject, targetPosition, Vector2.zero, 0f, true, true, false);
                    return;
                } else {
                    if (ParentRoom != null && GameManager.Instance.RewardManager.BowlerNoteOtherSource) {
                        string CustomText = "Corpses aren't {wb}Rainbow Chests{w}!\n\nNo RAAAAAIIIINBOW, no item!\n\n{wb}-Bowler{w}";
                        ExpandUtility.SpawnCustomBowlerNote(GameManager.Instance.RewardManager.BowlerNoteOtherSource, targetPosition, ParentRoom, CustomText, false);
                        return;
                    }
                }
            }
        }

        public void RegisterChestOnMinimap(GameObject MinimapIconPrefab) {
            if (ParentRoom != null) {
                GameObject iconPrefab = MinimapIconPrefab ?? (BraveResources.Load("Global Prefabs/Minimap_Treasure_Icon", ".prefab") as GameObject);
                minimapIconInstance = Minimap.Instance.RegisterRoomIcon(ParentRoom, iconPrefab, false);
            }
        }

        public void DeregisterChestOnMinimap() {
            if (minimapIconInstance && ParentRoom != null)Minimap.Instance.DeregisterRoomIcon(ParentRoom, minimapIconInstance);
        }

        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }

        public float GetOverrideMaxDistance() { return -1f; }

        protected override void OnDestroy() { base.OnDestroy(); }

        public void ConfigureOnPlacement(RoomHandler room) {
            if (!m_InitFinished)Init();
            if (IsTrollChest) {
                ParentRoom = room;
                ParentRoom.RegisterInteractable(this);
                RegisterChestOnMinimap(GameManager.Instance.RewardManager.S_Chest.MinimapIconPrefab);
            }
        }
    }
}

