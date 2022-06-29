using System;
using System.Collections;
using UnityEngine;
using Dungeonator;
using System.Reflection;
using MonoMod.RuntimeDetour;
using System.Collections.Generic;
using static ExpandTheGungeon.ExpandUtilities.ReflectionHelpers;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandPunchoutArcadeController : BraveBehaviour {

        public ExpandPunchoutArcadeController() {
            m_Configured = false;
            m_PunchOutEnded = false;
        }

        [NonSerialized]
        public static List<int> RewardIDs = new List<int>();
        [NonSerialized]
        public static int RatKeyCount = -1;
        [NonSerialized]
        public static bool WonRatGame = false;
        [NonSerialized]
        public static Hook doLoseFadeHook;
        [NonSerialized]
        public static Hook dropRewardHook;
        [NonSerialized]
        public static Hook dropKeyHook;

        [NonSerialized]
        public GameObject ScanlineFX;

        [NonSerialized]
        public PlayerController Player;
        [NonSerialized]
        public ExpandCasinoGameController ParentGameController;
        
        [NonSerialized]
        private PunchoutController m_punchoutController;
        [NonSerialized]
        private dfTextureSprite m_PunchoutOverlay;
        [NonSerialized]
        private bool m_Configured;
        [NonSerialized]
        private bool m_PunchOutEnded;
        
        public void StartPunchout(PlayerController player) {
            Player = player;
            RatKeyCount = -1;
            WonRatGame = false;

            RewardIDs = new List<int>();
            
            ExpandSettings.PlayingPunchoutArcade = true;
                        
            doLoseFadeHook = new Hook(
                typeof(PunchoutController).GetMethod("DoLoseFade", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandPunchoutArcadeController).GetMethod("DoLoseFadeHook", BindingFlags.Public | BindingFlags.Instance),
                typeof(PunchoutController)
            );
            dropRewardHook = new Hook(
                typeof(PunchoutAIActor).GetMethod("DropReward", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandPunchoutArcadeController).GetMethod("DropRewardHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(PunchoutAIActor)
            );
            dropKeyHook = new Hook(
                typeof(PunchoutAIActor).GetMethod("DropKey", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandPunchoutArcadeController).GetMethod("DropKeyHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(PunchoutAIActor)
            );

            RoomHandler CurrentRoom = GameManager.Instance.PrimaryPlayer.CurrentRoom;
            GameObject MetalGearRatPrefab = ExpandAssets.LoadOfficialAsset<GameObject>("MetalGearRat", ExpandAssets.AssetSource.EnemiesBase);
            MetalGearRatDeathController MetalGearRatDeathPrefab = MetalGearRatPrefab.GetComponent<MetalGearRatDeathController>();
            
            IntVector2 RoomPosition = CurrentRoom.area.basePosition;
            Vector3 RoomPositionVec3 = RoomPosition.ToVector3() - new Vector3(5, 5);
            Vector3 RoomPositionPunchoutVec3 = RoomPosition.ToVector3() + new Vector3(0, 10);

            SuperReaperController.PreventShooting = true;
            foreach (PlayerController playerController in GameManager.Instance.AllPlayers) { playerController.SetInputOverride("starting punchout"); }
            GameObject punchoutMinigame = Instantiate(MetalGearRatDeathPrefab.PunchoutMinigamePrefab, RoomPositionPunchoutVec3, Quaternion.identity);
            m_punchoutController = punchoutMinigame.GetComponent<PunchoutController>();
            m_punchoutController.PlayerWonRatNPC = null;
            StartCoroutine(StartPunchout());
        }

        private IEnumerator StartPunchout() {
            m_punchoutController.Init();
            if (!ScanlineFX) { ScanlineFX = Instantiate(ExpandAssets.LoadAsset<GameObject>("EXArcadeGameScanlineFX"), gameObject.transform.position, Quaternion.identity); }
            for (int i = 0; i < 20; i++) { yield return null; }
            Pixelator.Instance.FadeToColor(1f, Color.black, true, 0f);
            yield return null;
            SetupUI();
            m_Configured = true;
            yield break;
        }

        private void SetupUI(float ZoomLevel = 0.8f) {
            CameraController mainCameraController = GameManager.Instance.MainCameraController;
            mainCameraController.SetZoomScaleImmediate(ZoomLevel);
            // mainCameraController.OverridePosition -= new Vector3(0, 16, 0);
            FieldInfo m_cameraCenterPosField = typeof(PunchoutController).GetField("m_cameraCenterPos", BindingFlags.Instance | BindingFlags.NonPublic);
            Vector2 m_cameraCenterPos = (Vector2)m_cameraCenterPosField.GetValue(m_punchoutController);
            m_cameraCenterPosField.SetValue(m_punchoutController, (m_cameraCenterPos - new Vector2(1, 0)));

            m_punchoutController.UiPanel.Size -= new Vector2(290, 0);
            m_punchoutController.UiPanel.Position += new Vector3(145, 10);
            // m_PunchoutOverlay = m_punchoutController.UiManager.AddControl<dfTextureSprite>();
            m_PunchoutOverlay = GameUIRoot.Instance.Manager.AddControl<dfTextureSprite>();
            m_PunchoutOverlay.Anchor = dfAnchorStyle.CenterHorizontal | dfAnchorStyle.CenterVertical;
            m_PunchoutOverlay.IsInteractive = true;
            m_PunchoutOverlay.Tooltip = string.Empty;
            m_PunchoutOverlay.Pivot = dfPivotPoint.MiddleCenter;
            m_PunchoutOverlay.zindex = 8;
            m_PunchoutOverlay.Color = new Color32(255, 255, 255, 255);
            m_PunchoutOverlay.DisabledColor = new Color32(255, 255, 255, 255);
            m_PunchoutOverlay.Size = new Vector2(1440, 810);
            m_PunchoutOverlay.MinimumSize = Vector2.zero;
            m_PunchoutOverlay.MinimumSize = Vector2.zero;
            m_PunchoutOverlay.ClipChildren = false;
            m_PunchoutOverlay.InverseClipChildren = false;
            m_PunchoutOverlay.TabIndex = -1;
            m_PunchoutOverlay.CanFocus = false;
            m_PunchoutOverlay.AutoFocus = false;
            m_PunchoutOverlay.IsLocalized = false;
            m_PunchoutOverlay.HotZoneScale = Vector2.one;
            m_PunchoutOverlay.AllowSignalEvents = true;
            m_PunchoutOverlay.PrecludeUpdateCycle = false;
            m_PunchoutOverlay.Texture = ExpandAssets.LoadAsset<Texture2D>("PunchoutArcadeOverlay");
            m_PunchoutOverlay.Opacity = 1;
            m_PunchoutOverlay.Flip = dfSpriteFlip.None;
            m_PunchoutOverlay.FillDirection = dfFillDirection.Horizontal;
            m_PunchoutOverlay.CropRect = new Rect(Vector2.zero, Vector2.one);
            m_PunchoutOverlay.CropTexture = false;
            m_PunchoutOverlay.Position = new Vector3(-720, 405, 0);
            m_PunchoutOverlay.IsEnabled = true;
            m_PunchoutOverlay.IsVisible = true;
            
            GameUIRoot.Instance.PauseMenuPanel.GetComponent<PauseMenuController>().metaCurrencyPanel.IsVisible = false;
        }

        public void DestroyOverlayUI() {
            if (m_PunchoutOverlay) {
                m_PunchoutOverlay.Opacity = 0;
                m_PunchoutOverlay.IsEnabled = false;
                m_PunchoutOverlay.IsVisible = true;
                Destroy(m_PunchoutOverlay);
            }
        }
        
        private void LateUpdate() {
            if (!m_Configured | !PunchoutController.IsActive | !m_PunchoutOverlay) { return; }
            m_PunchoutOverlay.IsVisible = !GameManager.Instance.IsPaused;
        }

        private void Update() {
            if (!m_Configured | m_PunchOutEnded | PunchoutController.IsActive) { return; }
            Minimap.Instance.TemporarilyPreventMinimap = false;
            Pixelator.Instance.FadeToColor(1f, Color.white, true, 0f);
            PickupObject.RatBeatenAtPunchout = false;
            m_PunchOutEnded = true;
            ExpandSettings.PlayingPunchoutArcade = false;
            if (doLoseFadeHook != null) { doLoseFadeHook.Dispose(); doLoseFadeHook = null; }
            if (dropRewardHook != null) { dropRewardHook.Dispose(); dropRewardHook = null; }
            if (dropKeyHook != null) { dropKeyHook.Dispose(); dropKeyHook = null; }
            if (ScanlineFX) { Destroy(ScanlineFX); }
            if (ParentGameController) { ParentGameController.Finished = true; }
        }

        public void MaybeGiveRewards(float Delay = 0.25f) { StartCoroutine(DelayedItemPickups(Delay)); }

        private IEnumerator DelayedItemPickups(float delay) {
            yield return new WaitForSeconds(delay);
            if (WonRatGame) {
                bool GainedHegemonyCurrency = false;
                bool GainedCasings = false;
                if (RatKeyCount != -1) {
                    for (int K = 0; K < RatKeyCount; K++) {
                        foreach (int ItemID in RewardIDs) {
                            switch (ItemID) {
                                case 68:
                                    Player.carriedConsumables.Currency += 1;
                                    if (!GainedCasings) { GainedCasings = true; }
                                    break;
                                case 70:
                                    Player.carriedConsumables.Currency += 5;
                                    if (!GainedCasings) { GainedCasings = true; }
                                    break;
                                case 74:
                                    Player.carriedConsumables.Currency += 50;
                                    if (!GainedCasings) { GainedCasings = true; }
                                    break;
                                case 297:
                                    if (!GainedHegemonyCurrency) { GainedHegemonyCurrency = true; }
                                    GameStatsManager.Instance.RegisterStatChange(TrackedStats.META_CURRENCY, 5);
                                    break;
                            }
                        }
                    }
                } else {
                    foreach (int ItemID in RewardIDs) {
                        switch (ItemID) {
                            case 68:
                                Player.carriedConsumables.Currency += 1;
                                if (!GainedCasings) { GainedCasings = true; }
                                break;
                            case 70:
                                Player.carriedConsumables.Currency += 5;
                                if (!GainedCasings) { GainedCasings = true; }
                                break;
                            case 74:
                                Player.carriedConsumables.Currency += 50;
                                if (!GainedCasings) { GainedCasings = true; }
                                break;
                            case 297:
                                if (!GainedHegemonyCurrency) { GainedHegemonyCurrency = true; }
                                GameStatsManager.Instance.RegisterStatChange(TrackedStats.META_CURRENCY, 1);
                                break;
                        }
                    }
                }
                Player.carriedConsumables.ForceUpdateUI();
                if (GainedHegemonyCurrency) {
                    CurrencyPickup hegemonyCurrency = (PickupObjectDatabase.GetById(297) as CurrencyPickup);
                    tk2dBaseSprite targetAutomaticSprite = hegemonyCurrency.gameObject.GetComponent<HologramDoer>().TargetAutomaticSprite;
                    Player.BloopItemAboveHead(targetAutomaticSprite, hegemonyCurrency.overrideBloopSpriteName);
                    GameObject gameOBJ = Instantiate((GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Pickup"));
                    tk2dSprite component = gameOBJ.GetComponent<tk2dSprite>();
                    component.PlaceAtPositionByAnchor(Player.sprite.WorldCenter, tk2dBaseSprite.Anchor.MiddleCenter);
                    component.UpdateZDepth();
                    AkSoundEngine.PostEvent("Play_OBJ_metacoin_collect_01", gameObject);
                }
                if (GainedCasings) {
                    CurrencyPickup casingCurrency = (PickupObjectDatabase.GetById(68) as CurrencyPickup);
                    Player.BloopItemAboveHead(casingCurrency.sprite, casingCurrency.overrideBloopSpriteName);
                    GameObject gameOBJ2 = Instantiate((GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Pickup"));
                    tk2dSprite component2 = gameOBJ2.GetComponent<tk2dSprite>();
                    component2.PlaceAtPositionByAnchor(Player.sprite.WorldCenter, tk2dBaseSprite.Anchor.MiddleCenter);
                    component2.UpdateZDepth();
                    if (RatKeyCount > 2) {
                        AkSoundEngine.PostEvent("Play_OBJ_coin_large_01", gameObject);
                    } else {
                        AkSoundEngine.PostEvent("Play_OBJ_coin_small_01", gameObject);
                    }
                }
            }
            RatKeyCount = -1;
            RewardIDs = new List<int>();
            WonRatGame = false;
            yield break;
        }
        

        public void DoLoseFadeHook(Action<PunchoutController, bool>orig, PunchoutController self, bool skipDelay) {
            GameManager.Instance.StartCoroutine(DoLoseFadeCR(self, skipDelay));
        }
        
        private void DropRewardHook(Action<PunchoutAIActor, bool, PickupObject.ItemQuality[]>orig, PunchoutAIActor self, bool isLeft, params PickupObject.ItemQuality[] targetQualities) {
            GameManager.Instance.StartCoroutine(DropRewardCR(self, isLeft));
        }

        private void DropKeyHook(Action<PunchoutAIActor, bool>orig, PunchoutAIActor self, bool isLeft) {
            GameManager.Instance.StartCoroutine(DropKeyCR(self, isLeft));
        }

        private static IEnumerator DoLoseFadeCR(PunchoutController self, bool skipDelay) {
            if (!skipDelay) { yield return new WaitForSeconds(2f); }
            float ela = 0f;
            float duration = 3f;
            Material vignetteMaterial = Pixelator.Instance.FadeMaterial;
            while (ela < duration) {
                ela += BraveTime.DeltaTime;
                float t = Mathf.Lerp(0f, 1f, ela / duration);
                vignetteMaterial.SetColor("_VignetteColor", Color.black);
                vignetteMaterial.SetFloat("_VignettePower", Mathf.Lerp(0.5f, 10f, t));
                t = Mathf.Lerp(0f, 1f, ela / 0.2f);
                self.HideUiAmount = t;
                self.UiManager.Invalidate();
                yield return null;
            }
            Pixelator.Instance.FadeToColor(1f, Color.black, false, 0f);
            yield return new WaitForSeconds(1.5f);
            Pixelator.Instance.FadeToColor(1f, Color.black, true, 0f);
            vignetteMaterial.SetColor("_VignetteColor", Color.black);
            vignetteMaterial.SetFloat("_VignettePower", 1f);
            InvokeMethod(typeof(PunchoutController), "TeardownPunchout", self);
            yield break;
        }

        private IEnumerator DropRewardCR(PunchoutAIActor punchoutAIActor, bool isLeft) {
            int rewardId = -1;
            if (UnityEngine.Random.value < 0.01f) {
                rewardId = 74; // 50 casing
            } else if (UnityEngine.Random.value < 0.2f) {
                rewardId = 297; // Hegemony Credit
            } else {
                if (UnityEngine.Random.value < 0.1f) {
                    rewardId = 70; // 5 casing
                } else {
                    rewardId = 68; // casing
                }
            }            
            if (rewardId != -1) {
                punchoutAIActor.DroppedRewardIds.Add(rewardId);
                RewardIDs.Add(rewardId);
                while (punchoutAIActor.state is PunchoutAIActor.ThrowAmmoState) { yield return null; }
                GameObject droppedItem = SpawnManager.SpawnVFX(punchoutAIActor.DroppedItemPrefab, punchoutAIActor.transform.position + new Vector3(-0.25f, 2.5f), Quaternion.identity);
                tk2dSprite droppedItemSprite = droppedItem.GetComponent<tk2dSprite>();
                tk2dSprite rewardSprite = PickupObjectDatabase.GetById(rewardId).GetComponent<tk2dSprite>();
                droppedItemSprite.SetSprite(rewardSprite.Collection, rewardSprite.spriteId);
                droppedItem.GetComponent<PunchoutDroppedItem>().Init(isLeft);
            }
            yield break;
        }

        private IEnumerator DropKeyCR(PunchoutAIActor punchoutAIActor, bool isLeft) {
            punchoutAIActor.NumKeysDropped++;
            while (punchoutAIActor.state is PunchoutAIActor.ThrowAmmoState) { yield return null; }
            GameObject droppedItem = SpawnManager.SpawnVFX(ExpandPrefabs.EXPunchoutArcadeCoin, punchoutAIActor.transform.position + new Vector3(-0.25f, 2.5f), Quaternion.identity);
            if (droppedItem) {
                tk2dSprite droppedItemSprite = droppedItem.GetComponent<tk2dSprite>();
                PunchoutDroppedItem punchoutDroppedItem = droppedItem.GetComponent<PunchoutDroppedItem>();
                if (droppedItemSprite) {
                    string text = "left";
                    if (!isLeft) { text = "right"; }
                    droppedItemSprite.SetSprite("punchout_coin_" + text);
                }
                if (punchoutDroppedItem) { punchoutDroppedItem.Init(isLeft); }
            }
            yield break;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

