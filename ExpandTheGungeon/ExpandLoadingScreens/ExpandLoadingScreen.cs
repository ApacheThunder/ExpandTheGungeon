using BepInEx;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;
using MonoMod.RuntimeDetour;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static ExpandTheGungeon.ExpandUtilities.ReflectionHelpers;

namespace ExpandTheGungeon.ExpandLoadingScreens {
    
    public class ExpandLoadingScreen : FoyerPreloader {

        public static GameObject EXDebugObject;

        public static Hook FoyerPreloadHook; 
        public static FoyerPreloader Instance;

        public static GameObject LoadingScreenObject;
        public static GameObject LoadingBarFrameObject;
        public static GameObject LoadingBarObject;
        public static GameObject OptionalLoadingScreenObject;

        public static dfLabel LoadTextInstance;
        
        public static dfTextureSprite EXLoadingScreenSprite;
        public static dfTextureSprite EXLoadingBarFrameSprite;
        public static dfTextureSprite EXLoadingBarSprite;
        public static dfTextureSprite EXAdditionalLogoSprite;

        public static Texture2D EXLoadScreenLogo;
        public static Texture2D EXLoadScreenLogoLoadBarFrame;
        public static Texture2D EXLoadScreenLogoLoadBar;
        

        // The values defined below are for external mod use if they so choose.
        public static Texture2D ExternalLogoOverride = null;
        // Control progress bar by altering it's size on horizontal axis. Current graphic is 218 on horizontal resolution. So anything between 0 and 218 gives you the normal coverage under the frame.
        public static float? ProgressBarWidthOverride = null;
        // Color override for Progress Bar. Only set if LoadStatus set to other.
        public static Color? ProgressBarColorOverride = null;
        // Anchor style. Control centering of optional logo graphics. If null, center in middle of screen is default.
        public static dfAnchorStyle? ExternalLogoAnchorStyle = null;

        // These are instantiated instances. They will be null when the loading screen is Destroyed.
        private static dfTextureSprite m_EXLoadingScreenSprite;
        private static dfTextureSprite m_EXLoadingBarFrameSprite;
        private static dfTextureSprite m_EXLoadingBarSprite;

        public static Texture2D EXLoadScreenThrobber_Error;
        public static Texture2D EXLoadScreenThrobber_West;
        public static Texture2D EXLoadScreenThrobber_ThirdEye;
        public static Texture2D EXLoadScreenThrobber_Glitch;
        public static Texture2D EXLoadScreenThrobber_Glitch2;
        /*public static Texture2D EXLoadScreenThrobber_Backrooms;
        public static Texture2D EXLoadScreenThrobber_Jungle;
        public static Texture2D EXLoadScreenThrobber_Belly;*/

        public static readonly string txtCloser = "...";
        /*public static string CurrentText = "Preparing Gungeon Expansion...";
        public static string CurrentTextAlt = "Building Mod Assets. Please Wait!";*/
        
        public static Dictionary<ExpandTheGungeon.LoadStatus, string> LoadText = new Dictionary<ExpandTheGungeon.LoadStatus, string>() {
            [ExpandTheGungeon.LoadStatus.PreStartup] = "Preparing Gungeon Expansion...",
            [ExpandTheGungeon.LoadStatus.PreInit] = "Building Mod Assets. Please Wait!",
            [ExpandTheGungeon.LoadStatus.LoadAudio] = "Loading Audio Assetbundle",
            [ExpandTheGungeon.LoadStatus.LoadSprites] = "Building Sprite Collections",
            [ExpandTheGungeon.LoadStatus.LoadItems] = "Building Item Prefabs",
            [ExpandTheGungeon.LoadStatus.LoadPrefabs] = "Building Main Prefabs",
            [ExpandTheGungeon.LoadStatus.LoadEnemies] = "Building AIActor Prefabs",
            [ExpandTheGungeon.LoadStatus.LoadRooms] = "Building Room Prefabs",
            [ExpandTheGungeon.LoadStatus.LoadFloors] = "Building Gungeon Floor Prefabs",
            [ExpandTheGungeon.LoadStatus.LoadCleanup] = "Final Cleanup",
            [ExpandTheGungeon.LoadStatus.LoadFinished] = "Gungeon Expansion Completed",
            [ExpandTheGungeon.LoadStatus.LoadError] = "ERROR",
            [ExpandTheGungeon.LoadStatus.Other] = "Insert Message Here"
        };


        public enum OverrideType { None, West, Jungle, Belly, Backrooms, Glitched };
        public static OverrideType overrideType = OverrideType.None;

        public static bool KeepLoading = false;
        public static bool HookActive = true;
        public static bool AssetsReady = false;
        public static bool InitialScreenInit = false;
        public static bool RefreshText = true;
        public static int MaxProgressBarWidth;
        public static int MinProgressBarWidth;


        public static void Init() {
            FoyerPreloadHook = new Hook(
                typeof(FoyerPreloader).GetMethod(nameof(Update), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandLoadingScreen).GetMethod(nameof(EXUpdate), BindingFlags.Public | BindingFlags.Instance),
                typeof(FoyerPreloader)
            );

            EXLoadScreenLogo = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadScreenLogo.png", new IntVector2(218, 96));
            EXLoadScreenThrobber_Error = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadingScreen_Error.png", new IntVector2(1024, 512));
            EXLoadScreenThrobber_ThirdEye = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadingScreen_ThirdEye.png", new IntVector2(1024, 512));
            EXLoadScreenThrobber_Glitch = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadingScreen_Glitch.png", new IntVector2(1024, 512));
            EXLoadScreenThrobber_Glitch2 = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadingScreen_Glitch2.png", new IntVector2(1024, 512));
            EXLoadScreenThrobber_West = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadingScreen_West.png", new IntVector2(1024, 512));
            EXLoadScreenLogoLoadBarFrame = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadScreenLogo_LoadBarFrame.png", new IntVector2(218, 24));
            EXLoadScreenLogoLoadBar = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadScreenLogo_LoadBar.png", new IntVector2(218, 24));

            MaxProgressBarWidth = (EXLoadScreenLogoLoadBar.width - 2);
            MinProgressBarWidth = 5;

            AssetsReady = true;
        }
        
        public void EXUpdate(Action<FoyerPreloader>orig, FoyerPreloader self) {
            if (!self) return;
            if (!HookActive) {
                ExpandSettings.EnableAsyncAssetLoading = false;
                orig(self);
                return;
            }
            if (!Instance) Instance = self;

            bool m_wasFirstLoadScreen = ReflectGetField<bool>(typeof(FoyerPreloader), "m_wasFirstLoadScreen", self);
            bool m_isLoading = ReflectGetField<bool>(typeof(FoyerPreloader), "m_isLoading", self);

            if (!AssetsReady) return;

            if (ExpandSettings.EnableAsyncAssetLoading) {
                if (ExpandTheGungeon.loadStatus != ExpandTheGungeon.LoadStatus.LoadFinished && ExpandTheGungeon.PreventInput) {
                    if (UnityInput.Current != null) UnityInput.Current.ResetInputAxes();
                } else if (ExpandTheGungeon.loadStatus == ExpandTheGungeon.LoadStatus.LoadFinished && ExpandTheGungeon.PreventInput) {
                    ExpandTheGungeon.PreventInput = false;
                }
            }
            
            if (m_wasFirstLoadScreen && RefreshText) {
                string m_NewText = string.Empty;
                if (!InitialScreenInit) {
                    if (ExpandSettings.EnableAsyncAssetLoading) {
                        LoadText.TryGetValue(ExpandTheGungeon.LoadStatus.PreStartup, out m_NewText);
                    } else {
                        LoadText.TryGetValue(ExpandTheGungeon.LoadStatus.PreInit, out m_NewText);
                    }
                    if (!LoadingScreenObject)CreateInitialLoadingScreen(self, m_NewText);
                    UpdateLoadingBar(ExpandTheGungeon.loadStatus);
                    InitialScreenInit = true;
                } else {
                    LoadText.TryGetValue(ExpandTheGungeon.loadStatus, out m_NewText);
                    if (!string.IsNullOrEmpty(m_NewText)) UpdateText(m_NewText, (ExpandTheGungeon.loadStatus == ExpandTheGungeon.LoadStatus.LoadError));
                    UpdateLoadingBar(ExpandTheGungeon.loadStatus);
                }
                RefreshText = false;
            }
                        
            if (m_wasFirstLoadScreen && Time.frameCount > 4 && !m_isLoading) {
                self.StartCoroutine(EXAsyncLoadFoyer(self, m_wasFirstLoadScreen));
                typeof(FoyerPreloader).GetField("m_isLoading", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(self, true);
                return;
            } else if (!m_wasFirstLoadScreen) {
                if (Instance && overrideType != OverrideType.None)MaybeOverrideGraphics(self);
                if (Instance && ExpandDebugCamera.DebugCameraEnabled) {
                    CreateThirdEyeLoadingScreen(Instance);
                    ExpandDebugCamera.ClearLoadScreenBackground(Instance);
                }
            }
        }

        public static IEnumerator WaitForTextUpdate() {
            RefreshText = true;
            while (RefreshText && Instance)yield return null;
            yield return new WaitForEndOfFrame();
            yield break;
        }

        private void MaybeOverrideGraphics(FoyerPreloader foyerPreloader) {
            switch (overrideType) {
                case OverrideType.None:
                    return;
                case OverrideType.West:
                    overrideType = OverrideType.None;
                    CreateOldWestLoadingScreen(foyerPreloader);
                    return;
                case OverrideType.Jungle:
                    overrideType = OverrideType.None;
                    return;
                case OverrideType.Belly:
                    overrideType = OverrideType.None;
                    return;
                case OverrideType.Backrooms:
                    overrideType = OverrideType.None;
                    CreateBackroomsLoadingScreen(foyerPreloader);
                    return;
                case OverrideType.Glitched:
                    overrideType = OverrideType.None;
                    CreateGlitchedLoadingScreen(foyerPreloader);
                    return;
            }
        }

        public static void UpdateText(string Text, bool noCloser = false) {
            if (Instance) {
                if (noCloser) {
                    Instance.LoadingLabel.Text = Text;
                } else {
                    Instance.LoadingLabel.Text = Text + txtCloser;
                }
                if (ExpandTheGungeon.loadStatus == ExpandTheGungeon.LoadStatus.LoadError) {
                    Instance.LoadingLabel.Color = new Color(1, 0, 0, 1);
                }
                if (ExpandSettings.debugMode) Debug.Log("[" + ExpandTheGungeon.ModName + "] " + Text);
            }
        }
        
        private IEnumerator EXAsyncLoadFoyer(FoyerPreloader preloader, bool m_wasFirstLoadScreen) {
            DebugTime.Log("FoyerLoader.AsyncLoadFoyer()", new object[0]);
            GameManager.AttemptSoundEngineInitializationAsync();
            yield return preloader.StartCoroutine(ResourceManager.InitAsync());
            DebugTime.RecordStartTime();
            GameManager targetManager = (BraveResources.Load("_GameManager", ".prefab") as GameObject).GetComponent<GameManager>();
            DebugTime.Log("Preloaded GameManager", new object[0]);
            yield return null;
            EnemyDatabase enemyDatabasePreload = EnemyDatabase.Instance;
            yield return null;
            EncounterDatabase encounterDatabasePreload = EncounterDatabase.Instance;
            yield return null;
            while (!GameManager.AUDIO_ENABLED) yield return null;
            if (ExpandSettings.debugMode)Debug.Log("[ExpandTheGungeon] Successfully hijacked FoyerPreloader. Preventing FoyerPreloader self delete ...");
            if (m_wasFirstLoadScreen) DontDestroyOnLoad(preloader.gameObject);
            AssetBundle assetBundle = ResourceManager.LoadAssetBundle("foyer_001");
            DebugTime.RecordStartTime();
            ResourceManager.LoadLevelFromBundle(assetBundle);
            if (ExpandSettings.debugMode)Debug.Log("[ExpandTheGungeon] Finishing Foyer Load...");
            DebugTime.Log("Application.LoadLevel(foyer)", new object[0]);
            // if (m_wasFirstLoadScreen)yield return preloader.StartCoroutine(DestroyLoadScreen(preloader)); 
            yield break;
        }

        public static IEnumerator DestroyLoadScreen(FoyerPreloader Preloader, int skipFrames = 3) {
            bool DontDestroyItAfterAll = false;
            for (int i = 0; i < Preloader.gameObject.transform.childCount; i++) {
                if (Preloader.gameObject.transform.GetChild(i).name == "DontDestroyMePlease") DontDestroyItAfterAll = true;
            }
            DebugTime.Log("Starting to destroy the load screen", new object[0]);
            for (int i = 0; i < skipFrames; i++) yield return null;
            DebugTime.Log("Finished destroying the load screen", new object[0]);

            if (!DontDestroyItAfterAll)Destroy(Preloader.gameObject);
            yield break;
        }

        public static void UpdateLoadingBar(ExpandTheGungeon.LoadStatus status) {
            if (!ExpandSettings.EnableAsyncAssetLoading) return;
            switch (status) {
                case ExpandTheGungeon.LoadStatus.PreInit:
                    break;
                case ExpandTheGungeon.LoadStatus.LoadStart:
                    // if (m_EXLoadingScreenSprite) m_EXLoadingScreenSprite.Texture = EXLoadScreenLogoLoadBarFrame;
                    if (m_EXLoadingBarFrameSprite) m_EXLoadingBarFrameSprite.IsVisible = true;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.IsVisible = true;
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = 18;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = (int)(Mathf.Lerp(MinProgressBarWidth, MaxProgressBarWidth, 0.025f));
                    break;
                case ExpandTheGungeon.LoadStatus.LoadAudio:
                    // if (m_EXLoadingScreenSprite) m_EXLoadingScreenSprite.Texture = EXLoadScreenLogoLoadBarFrame;
                    if (m_EXLoadingBarFrameSprite) m_EXLoadingBarFrameSprite.IsVisible = true;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.IsVisible = true;
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = 48;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = (int)(Mathf.Lerp(MinProgressBarWidth, MaxProgressBarWidth, 0.03f));
                    break;
                case ExpandTheGungeon.LoadStatus.LoadSprites:
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = 58;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = (int)(Mathf.Lerp(MinProgressBarWidth, MaxProgressBarWidth, 0.04f));
                    break;
                case ExpandTheGungeon.LoadStatus.LoadItems:
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = 68;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = (int)(Mathf.Lerp(MinProgressBarWidth, MaxProgressBarWidth, 0.06f));
                    break;
                case ExpandTheGungeon.LoadStatus.LoadPrefabs:
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = 88;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = (int)(Mathf.Lerp(MinProgressBarWidth, MaxProgressBarWidth, 0.1f));
                    break;
                case ExpandTheGungeon.LoadStatus.LoadEnemies:
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = 140;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = (int)(Mathf.Lerp(MinProgressBarWidth, MaxProgressBarWidth, 0.4f));
                    break;
                case ExpandTheGungeon.LoadStatus.LoadRooms:
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = 165;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = (int)(Mathf.Lerp(MinProgressBarWidth, MaxProgressBarWidth, 0.5f));
                    break;
                case ExpandTheGungeon.LoadStatus.LoadFloors:
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = 175;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = (int)(Mathf.Lerp(MinProgressBarWidth, MaxProgressBarWidth, 0.7f));
                    break;
                case ExpandTheGungeon.LoadStatus.LoadCleanup:
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = (int)(Mathf.Lerp(MinProgressBarWidth, MaxProgressBarWidth, 0.85f));
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = 190;
                    break;
                case ExpandTheGungeon.LoadStatus.LoadFinished:
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = 214;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Width = (int)(Mathf.Lerp(MinProgressBarWidth, MaxProgressBarWidth, 1));
                    break;
                case ExpandTheGungeon.LoadStatus.LoadError:
                    // if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Texture = EXLoadScreenLogoLoadBarError;
                    if (m_EXLoadingBarSprite) m_EXLoadingBarSprite.Color = new Color(1, 0, 0);
                    if (Instance)Instance.Throbber.Atlas.Material.mainTexture = EXLoadScreenThrobber_Error;
                    break;
                case ExpandTheGungeon.LoadStatus.Other:
                    if (ProgressBarWidthOverride.HasValue) {
                        m_EXLoadingBarSprite.Width = ProgressBarWidthOverride.Value;
                        ProgressBarWidthOverride = null;
                        
                    }
                    if (ProgressBarColorOverride.HasValue) {
                        m_EXLoadingBarSprite.Color = ProgressBarColorOverride.Value;
                    }
                    break;
                default: break;
            }
        }


        public void CreateInitialLoadingScreen(FoyerPreloader foyerPreloader, string InitialText) {
            if (!foyerPreloader) return;
            
            foyerPreloader.LoadingLabel.gameObject.SetActive(true);
            foyerPreloader.LanguageManager.enabled = true;

            LoadingScreenObject = new GameObject("Expand Startup Loading Screen Panel Child");
            LoadingBarFrameObject = new GameObject("Expand Startup LoadBar Frame Panel Child");
            LoadingBarObject = new GameObject("Expand LoadBar Panel Child");
                       
            
            DontDestroyOnLoad(LoadingScreenObject);
            DontDestroyOnLoad(LoadingBarFrameObject);
            DontDestroyOnLoad(LoadingBarObject);


            EXLoadingScreenSprite = LoadingScreenObject.AddComponent<dfTextureSprite>();
            EXLoadingScreenSprite.Anchor = (dfAnchorStyle.Left | dfAnchorStyle.Bottom);
            EXLoadingScreenSprite.IsVisible = true;
            EXLoadingScreenSprite.IsInteractive = true;
            EXLoadingScreenSprite.Size = new Vector2(EXLoadScreenLogo.width, EXLoadScreenLogo.height);
            EXLoadingScreenSprite.MinimumSize = EXLoadingScreenSprite.Size;
            EXLoadingScreenSprite.MaximumSize = EXLoadingScreenSprite.Size;
            EXLoadingScreenSprite.ClipChildren = false;
            EXLoadingScreenSprite.InverseClipChildren = false;
            EXLoadingScreenSprite.TabIndex = -1;
            EXLoadingScreenSprite.CanFocus = false;
            EXLoadingScreenSprite.AutoFocus = false;
            EXLoadingScreenSprite.IsLocalized = false;
            EXLoadingScreenSprite.HotZoneScale = Vector2.one;
            EXLoadingScreenSprite.AllowSignalEvents = true;
            EXLoadingScreenSprite.PrecludeUpdateCycle = false;
            EXLoadingScreenSprite.Flip = dfSpriteFlip.None;
            EXLoadingScreenSprite.FillDirection = dfFillDirection.Horizontal;
            EXLoadingScreenSprite.FillAmount = 1;
            EXLoadingScreenSprite.InvertFill = false;
            EXLoadingScreenSprite.CropRect = new Rect() { x = 0, y = 0, width = 1, height = 1 };
            EXLoadingScreenSprite.Texture = EXLoadScreenLogo;

            EXLoadingBarFrameSprite = LoadingBarFrameObject.AddComponent<dfTextureSprite>();
            EXLoadingBarFrameSprite.Anchor = (dfAnchorStyle.CenterHorizontal | dfAnchorStyle.Bottom);
            EXLoadingBarFrameSprite.Pivot = dfPivotPoint.BottomLeft;
            EXLoadingBarFrameSprite.IsVisible = true;
            EXLoadingBarFrameSprite.IsInteractive = true;
            EXLoadingBarFrameSprite.Size = new Vector2(EXLoadScreenLogoLoadBarFrame.width, EXLoadScreenLogoLoadBarFrame.height);
            EXLoadingBarFrameSprite.MinimumSize = Vector2.zero;
            EXLoadingBarFrameSprite.MaximumSize = Vector2.zero;
            EXLoadingBarFrameSprite.ClipChildren = false;
            EXLoadingBarFrameSprite.InverseClipChildren = false;
            EXLoadingBarFrameSprite.TabIndex = -1;
            EXLoadingBarFrameSprite.CanFocus = false;
            EXLoadingBarFrameSprite.AutoFocus = false;
            EXLoadingBarFrameSprite.IsLocalized = false;
            EXLoadingBarFrameSprite.HotZoneScale = Vector2.one;
            EXLoadingBarFrameSprite.AllowSignalEvents = true;
            EXLoadingBarFrameSprite.PrecludeUpdateCycle = false;
            EXLoadingBarFrameSprite.Flip = dfSpriteFlip.None;
            EXLoadingBarFrameSprite.FillDirection = dfFillDirection.Horizontal;
            EXLoadingBarFrameSprite.FillAmount = 1;
            EXLoadingBarFrameSprite.InvertFill = false;
            EXLoadingBarFrameSprite.CropRect = new Rect() { x = 0, y = 0, width = 1, height = 1 };
            EXLoadingBarFrameSprite.Texture = EXLoadScreenLogoLoadBarFrame;


            EXLoadingBarSprite = LoadingBarObject.AddComponent<dfTextureSprite>();
            EXLoadingBarSprite.Anchor = (dfAnchorStyle.Left | dfAnchorStyle.Bottom);
            EXLoadingBarSprite.IsVisible = true;
            EXLoadingBarSprite.IsInteractive = true;
            EXLoadingBarSprite.Size = new Vector2(EXLoadScreenLogoLoadBar.width, EXLoadScreenLogoLoadBar.height);
            EXLoadingBarSprite.MinimumSize = Vector2.zero;
            EXLoadingBarSprite.MaximumSize = Vector2.zero;
            EXLoadingBarSprite.ClipChildren = false;
            EXLoadingBarSprite.InverseClipChildren = false;
            EXLoadingBarSprite.TabIndex = -1;
            EXLoadingBarSprite.CanFocus = false;
            EXLoadingBarSprite.AutoFocus = false;
            EXLoadingBarSprite.IsLocalized = false;
            EXLoadingBarSprite.HotZoneScale = Vector2.one;
            EXLoadingBarSprite.AllowSignalEvents = true;
            EXLoadingBarSprite.PrecludeUpdateCycle = false;
            EXLoadingBarSprite.Flip = dfSpriteFlip.None;
            EXLoadingBarSprite.FillDirection = dfFillDirection.Horizontal;
            EXLoadingBarSprite.FillAmount = 1;
            EXLoadingBarSprite.InvertFill = false;
            EXLoadingBarSprite.CropRect = new Rect() { x = 0, y = 0, width = 1, height = 1 };
            EXLoadingBarSprite.Texture = EXLoadScreenLogoLoadBar;
            EXLoadingBarSprite.Color = new Color(0, 1, 0);

            LoadTextInstance = foyerPreloader.LoadingLabel;
            LoadTextInstance.Text = InitialText;
            LoadTextInstance.IsLocalized = false;
            
            LoadTextInstance.TextAlignment = TextAlignment.Center;
            LoadTextInstance.VerticalAlignment = dfVerticalAlignment.Bottom;
            LoadTextInstance.AutoSize = false;
            LoadTextInstance.ClipChildren = true;
            LoadTextInstance.AutoHeight = false;
            LoadTextInstance.Size = new Vector2(EXLoadScreenLogo.width, EXLoadScreenLogo.height);
            LoadTextInstance.Position += new Vector3(0, 28);

            
            m_EXLoadingBarSprite = AddPrefab(LoadTextInstance, LoadingBarObject);
            m_EXLoadingBarFrameSprite = AddPrefab(LoadTextInstance, LoadingBarFrameObject);
            m_EXLoadingScreenSprite = AddPrefab(LoadTextInstance, LoadingScreenObject);

            // Set Specific names here to help external mods find them
            m_EXLoadingBarSprite.gameObject.name = "EXBarChild";
            m_EXLoadingBarFrameSprite.gameObject.name = "EXBarFrameChild";
            m_EXLoadingScreenSprite.gameObject.name = "EXLogoChild";

            // Generalized dummy object. Does nothing but serve as a marker for other mods to find.
            GameObject m_DummyObject = new GameObject("ExpandLoadingScreenDummy");
            m_DummyObject.gameObject.transform.SetParent(foyerPreloader.gameObject.transform);

            m_EXLoadingBarFrameSprite.IsVisible = false;
            m_EXLoadingBarSprite.IsVisible = false;
            
        }
        
        public static dfControl CreateAdditionalLoadingScreen(FoyerPreloader foyerPreloader, Texture2D logoTexture, dfAnchorStyle anchorStyle, Vector2 textureSize) {
            if (!foyerPreloader) return null;
            if (!foyerPreloader.gameObject.GetComponentInChildren<dfGUIManager>()) return null;
            OptionalLoadingScreenObject = new GameObject("Optional Startup Loading Screen Panel Child");
            DontDestroyOnLoad(OptionalLoadingScreenObject);
            
            EXAdditionalLogoSprite = OptionalLoadingScreenObject.AddComponent<dfTextureSprite>();
            EXAdditionalLogoSprite.Anchor = anchorStyle;
            EXAdditionalLogoSprite.IsVisible = true;
            EXAdditionalLogoSprite.IsInteractive = false;
            EXAdditionalLogoSprite.Size = new Vector2(textureSize.x, textureSize.y);
            EXAdditionalLogoSprite.MinimumSize = textureSize;
            EXAdditionalLogoSprite.MaximumSize = textureSize;
            EXAdditionalLogoSprite.ClipChildren = false;
            EXAdditionalLogoSprite.InverseClipChildren = false;
            EXAdditionalLogoSprite.TabIndex = -1;
            EXAdditionalLogoSprite.CanFocus = false;
            EXAdditionalLogoSprite.AutoFocus = false;
            EXAdditionalLogoSprite.IsLocalized = false;
            EXAdditionalLogoSprite.HotZoneScale = Vector2.one;
            EXAdditionalLogoSprite.AllowSignalEvents = true;
            EXAdditionalLogoSprite.PrecludeUpdateCycle = false;
            EXAdditionalLogoSprite.Flip = dfSpriteFlip.None;
            EXAdditionalLogoSprite.FillDirection = dfFillDirection.Horizontal;
            EXAdditionalLogoSprite.FillAmount = 1;
            EXAdditionalLogoSprite.InvertFill = false;
            EXAdditionalLogoSprite.CropRect = new Rect() { x = 0, y = 0, width = 1, height = 1 };
            EXAdditionalLogoSprite.Texture = logoTexture;

            return foyerPreloader.gameObject.GetComponentInChildren<dfGUIManager>().AddPrefab(OptionalLoadingScreenObject);

            // return AddPrefab(foyerPreloader.gameObject.GetComponentInChildren<dfGUIManager>(), OptionalLoadingScreenObject);
        }

        public static dfTextureSprite AddPrefab(dfLabel parentLabel, GameObject prefab) {
            if (!prefab.GetComponent<dfTextureSprite>())throw new InvalidCastException();
            GameObject gameObject = Instantiate(prefab);
            gameObject.transform.parent = parentLabel.transform;
            gameObject.layer = parentLabel.gameObject.layer;
            dfTextureSprite component = gameObject.GetComponent<dfTextureSprite>();
            typeof(dfTextureSprite).GetField("parent", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(component, parentLabel);
            component.zindex = -1;
            parentLabel.AddControl(component);
            return component;
        }

        public static dfTextureSprite AddPrefab(dfGUIManager parentLabel, GameObject prefab) {
            if (!prefab.GetComponent<dfTextureSprite>())throw new InvalidCastException();
            GameObject gameObject = Instantiate(prefab);
            gameObject.transform.parent = parentLabel.transform;
            gameObject.layer = parentLabel.gameObject.layer;
            dfTextureSprite component = gameObject.GetComponent<dfTextureSprite>();
            typeof(dfTextureSprite).GetField("parent", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(component, parentLabel);
            component.zindex = -1;
            // parentLabel.AddControl(component);
            return component;
        }

        public static void CreateOldWestLoadingScreen (FoyerPreloader foyerPreloader) {
            if (ExpandDebugCamera.DebugCameraEnabled) return;
            foyerPreloader.Throbber.Atlas.Material.mainTexture = EXLoadScreenThrobber_West;
        }

        public static void CreateThirdEyeLoadingScreen(FoyerPreloader foyerPreloader) {
            if (foyerPreloader.Throbber.Atlas.Material.mainTexture != EXLoadScreenThrobber_ThirdEye &&
                foyerPreloader.Throbber.Atlas.Material.mainTexture != EXLoadScreenThrobber_Glitch &&
                foyerPreloader.Throbber.Atlas.Material.mainTexture != EXLoadScreenThrobber_Glitch2)
            {
                foyerPreloader.Throbber.Atlas.Material.mainTexture = EXLoadScreenThrobber_ThirdEye;
            }
            
        }

        public static void CreateGlitchedLoadingScreen (FoyerPreloader foyerPreloader) {
            foyerPreloader.LoadingLabel.Glitchy = true;
            if (BraveUtility.RandomBool()) {
                foyerPreloader.Throbber.Atlas.Material.mainTexture = EXLoadScreenThrobber_Glitch;
            } else {
                foyerPreloader.Throbber.Atlas.Material.mainTexture = EXLoadScreenThrobber_Glitch2;
            }
        }
        
        public static void CreateBackroomsLoadingScreen (FoyerPreloader foyerPreloader) {
            foyerPreloader.LoadingLabel.Text = ("No-Clipping" + txtCloser);
        }
    }
}

