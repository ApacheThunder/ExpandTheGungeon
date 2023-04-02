using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandArkController : BraveBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandArkController() {
            LidAnimator = gameObject.transform.Find("G_Lid").gameObject.GetComponent<tk2dSpriteAnimator>();
            ChestAnimator = gameObject.GetComponent<tk2dSpriteAnimator>();
            PoofAnimator = gameObject.transform.Find("G_Poof").gameObject.GetComponent<tk2dSpriteAnimator>();
            LightSpriteBeam = gameObject.transform.Find("G_Light").gameObject.GetComponent<tk2dSprite>();
            HellCrackSprite = null;
            GunSpawnPoint = gameObject.transform.Find("Spawn");
            GunPrefab = ExpandObjectDatabase.EndTimesChest.GetComponent<ArkController>().GunPrefab;
            HeldGunPrefab = ExpandObjectDatabase.EndTimesChest.GetComponent<ArkController>().HeldGunPrefab;
            IsTrollChest = true;
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

        [NonSerialized]
        public RoomHandler ParentRoom;

        [NonSerialized]
        private Transform m_heldPastGun;
        [NonSerialized]
        private GameObject minimapIconInstance;

        private bool m_hasBeenInteracted;
        protected bool m_isLocalPointing;

        

        private IEnumerator Start() {
            if (!IsTrollChest) {
                ParentRoom = GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(transform.position.IntXY(VectorConversions.Floor));
                yield return null;
                RoomHandler.unassignedInteractableObjects.Add(this);
            }
            yield break;
        }

        private void Update() { }

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
            if (!m_hasBeenInteracted) { m_hasBeenInteracted = true; }
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
            PlayerController shotPlayer = null;
            bool didShootHellTrigger = false;
            Vector3 lastJitterAmount = Vector3.zero;
            bool m_isPlayingChargeAudio = false;
            for (;;) {
                UpdateCameraPositionDuringClockhair(interactor.CenterPosition);
                clockhair.transform.position = clockhair.transform.position - lastJitterAmount;
                clockhair.transform.position = GetTargetClockhairPosition(currentInput, clockhair.transform.position.XY());
                clockhair.sprite.UpdateZDepth();
                bool isTargetingValidTarget = CheckPlayerTarget(GameManager.Instance.PrimaryPlayer, clockhairTransform);
                shotPlayer = GameManager.Instance.PrimaryPlayer;
                if (!isTargetingValidTarget && GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                    isTargetingValidTarget = CheckPlayerTarget(GameManager.Instance.SecondaryPlayer, clockhairTransform);
                    shotPlayer = GameManager.Instance.SecondaryPlayer;
                }
                if (!isTargetingValidTarget && GameStatsManager.Instance.AllCorePastsBeaten()) {
                    isTargetingValidTarget = CheckHellTarget(HellCrackSprite, clockhairTransform);
                    didShootHellTrigger = isTargetingValidTarget;
                }
                if (isTargetingValidTarget) { clockhair.SetMotionType(-10f); } else { clockhair.SetMotionType(1f); }
                if ((currentInput.ActiveActions.ShootAction.IsPressed || currentInput.ActiveActions.InteractAction.IsPressed) && isTargetingValidTarget) {
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
                if ((currentInput.ActiveActions.ShootAction.WasReleased || currentInput.ActiveActions.InteractAction.WasReleased) && isTargetingValidTarget && shotTargetTime > holdDuration && !GameManager.Instance.IsPaused) {
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
            Pixelator.Instance.FadeToColor(1f, Color.white, true, 0.2f);
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
                yield return StartCoroutine(HandleGameOver(shotPlayer));
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

        private void ResetPlayers(bool isGunslingerPast = false) {
            IsResettingPlayers = true;
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                if (GameManager.Instance.AllPlayers[i].healthHaver.IsAlive) {
                    if (!isGunslingerPast) {
                        GameManager.Instance.AllPlayers[i].ResetToFactorySettings(true, true, false);
                    }
                    if (!isGunslingerPast) {
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

        private IEnumerator HandleGameOver(PlayerController interactor) {
            interactor.healthHaver.lastIncurredDamageSource = "HaHa April Fools!";
            interactor.healthHaver.ForceSetCurrentHealth(0);
            interactor.healthHaver.Armor = 0;
            interactor.healthHaver.Die(Vector2.zero);
            while (interactor.healthHaver.IsDead) { yield return null; }
            if (interactor.healthHaver.IsAlive) {
                GameManager.Instance.MainCameraController.SetManualControl(false, true);
                Pixelator.Instance.LerpToLetterbox(1, 0.25f);
                Pixelator.Instance.DoFinalNonFadedLayer = false;
                ResetPlayers(false);
                Destroy(LightSpriteBeam.gameObject);
                Destroy(this);
            }
            yield break;
        }

        public void RegisterChestOnMinimap(GameObject MinimapIconPrefab) {
            if (ParentRoom != null) {
                GameObject iconPrefab = MinimapIconPrefab ?? (BraveResources.Load("Global Prefabs/Minimap_Treasure_Icon", ".prefab") as GameObject);
                minimapIconInstance = Minimap.Instance.RegisterRoomIcon(ParentRoom, iconPrefab, false);
            }
        }

        public void DeregisterChestOnMinimap() {
            if (minimapIconInstance && ParentRoom != null) { Minimap.Instance.DeregisterRoomIcon(ParentRoom, minimapIconInstance); }
        }

        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }

        public float GetOverrideMaxDistance() { return -1f; }

        protected override void OnDestroy() { base.OnDestroy(); }

        public void ConfigureOnPlacement(RoomHandler room) {
            if (IsTrollChest) {
                ParentRoom = room;
                ParentRoom.RegisterInteractable(this);
                RegisterChestOnMinimap(GameManager.Instance.RewardManager.S_Chest.MinimapIconPrefab);
            }
        }
    }
}

