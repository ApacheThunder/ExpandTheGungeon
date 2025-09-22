using BepInEx;
using ExpandTheGungeon.ExpandUtilities;
using HarmonyLib;
using MonoMod.RuntimeDetour;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static ExpandTheGungeon.ExpandUtilities.ReflectionHelpers;

namespace ExpandTheGungeon.ExpandLoadingScreens {
    
    public class ExpandLoadingScreen : FoyerPreloader {

        public static Hook FoyerPreloadHook; 
        public static FoyerPreloader Instance;

        public static GameObject LoadingScreenObject;
        public static GameObject LoadingBarObject;

        public static dfLabel LoadTextInstance;
        public static dfTextureSprite EXLoadingScreenSprite;
        public static dfTextureSprite EXLoadingScreenWithFrameSprite;
        public static dfTextureSprite EXLoadingBarSprite;

        public static Texture2D EXLoadScreenLogo;
        public static Texture2D EXLoadScreenLogoLoadBarFrame;
        public static Texture2D EXLoadScreenLogoLoadBar;

        // These are instantiated instances. They will be null when the loading screen is Destroyed.
        private static dfTextureSprite m_EXLoadingScreenSprite;
        private static dfTextureSprite m_EXLoadingBarSprite;


        public static Texture2D EXLoadScreenThrobber_West;
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
        };


        public enum OverrideType { None, West, Jungle, Belly, Backrooms, Glitched };
        public static OverrideType overrideType = OverrideType.None;

        public static bool KeepLoading = false;
        public static bool HookActive = true;
        public static bool AssetsReady = false;
        public static bool InitialScreenInit = false;
        public static bool RefreshText = true;



        public static void Init() {
            FoyerPreloadHook = new Hook(
                typeof(FoyerPreloader).GetMethod(nameof(Update), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandLoadingScreen).GetMethod(nameof(EXUpdate), BindingFlags.Public | BindingFlags.Instance),
                typeof(FoyerPreloader)
            );

            EXLoadScreenLogo = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadScreenLogo.png", new IntVector2(218, 99));
            EXLoadScreenThrobber_West = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadingScreen_West.png", new IntVector2(1024, 512));
            EXLoadScreenLogoLoadBarFrame = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadScreenLogo_LoadBarFrame.png", new IntVector2(218, 99));
            EXLoadScreenLogoLoadBar = ExpandUtility.GetTextureFromResource("ExpandLoadingScreens/EXLoadScreenLogo_LoadBar.png", new IntVector2(218, 99));

            AssetsReady = true;
        }

        
        public void EXUpdate(Action<FoyerPreloader>orig, FoyerPreloader self) {
            if (ExpandSettings.EnableAsyncAssetLoading) {
                if (ExpandTheGungeon.loadStatus != ExpandTheGungeon.LoadStatus.LoadFinished && ExpandTheGungeon.PreventInput) {
                    if (UnityInput.Current != null) UnityInput.Current.ResetInputAxes();
                } else if (ExpandTheGungeon.loadStatus == ExpandTheGungeon.LoadStatus.LoadFinished && ExpandTheGungeon.PreventInput) {
                    ExpandTheGungeon.PreventInput = false;
                }
            }
            if (!HookActive) { orig(self); return; }
            if (!AssetsReady) return;
            if (!Instance) Instance = self;
            if (!self) return;

            bool m_wasFirstLoadScreen = ReflectGetField<bool>(typeof(FoyerPreloader), "m_wasFirstLoadScreen", self);

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
                    if (!string.IsNullOrEmpty(m_NewText)) UpdateText(m_NewText);
                    UpdateLoadingBar(ExpandTheGungeon.loadStatus);
                }
                RefreshText = false;
            }

            bool m_isLoading = ReflectGetField<bool>(typeof(FoyerPreloader), "m_isLoading", self);
            
            if (m_wasFirstLoadScreen && Time.frameCount >= 5 && !m_isLoading) {
                self.StartCoroutine(EXAsyncLoadFoyer(self, m_wasFirstLoadScreen));
                typeof(FoyerPreloader).GetField("m_isLoading", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(self, true);
            }

            if (!m_wasFirstLoadScreen && overrideType != OverrideType.None)MaybeOverrideGraphics(self);
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
                    CreateOldWestLoadingScreen(foyerPreloader);
                    overrideType = OverrideType.None;
                    return;
                case OverrideType.Jungle:
                    overrideType = OverrideType.None;
                    return;
                case OverrideType.Belly:
                    overrideType = OverrideType.None;
                    return;
                case OverrideType.Backrooms:
                    CreateBackroomsLoadingScreen(foyerPreloader);
                    overrideType = OverrideType.None;
                    return;
                case OverrideType.Glitched:
                    CreateGlitchedLoadingScreen(foyerPreloader);
                    overrideType = OverrideType.None;
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
            DebugTime.Log("Starting to destroy the load screen", new object[0]);
            for (int i = 0; i < skipFrames; i++) yield return null;
            DebugTime.Log("Finished destroying the load screen", new object[0]);
            Destroy(Preloader.gameObject);
            yield break;
        }

        public static void UpdateLoadingBar(ExpandTheGungeon.LoadStatus status) {
            if (!ExpandSettings.EnableAsyncAssetLoading) return; // Additional load screen assets are not available when async loading is disabled.
            switch (status) {
                case ExpandTheGungeon.LoadStatus.PreInit:
                    break;
                case ExpandTheGungeon.LoadStatus.LoadStart:
                    m_EXLoadingScreenSprite.Texture = EXLoadScreenLogoLoadBarFrame;
                    m_EXLoadingBarSprite.IsVisible = true;
                    m_EXLoadingBarSprite.Width = 10;
                    break;
                case ExpandTheGungeon.LoadStatus.LoadAudio:
                    m_EXLoadingScreenSprite.Texture = EXLoadScreenLogoLoadBarFrame;
                    m_EXLoadingBarSprite.Width = 54;
                    break;
                case ExpandTheGungeon.LoadStatus.LoadSprites:
                    m_EXLoadingBarSprite.Width = 65;
                    break;
                case ExpandTheGungeon.LoadStatus.LoadItems:
                    m_EXLoadingBarSprite.Width = 88;
                    break;
                case ExpandTheGungeon.LoadStatus.LoadPrefabs:
                    m_EXLoadingBarSprite.Width = 130;
                    break;
                case ExpandTheGungeon.LoadStatus.LoadEnemies:
                    m_EXLoadingBarSprite.Width = 152;
                    break;
                case ExpandTheGungeon.LoadStatus.LoadRooms:
                    m_EXLoadingBarSprite.Width = 185;
                    break;
                case ExpandTheGungeon.LoadStatus.LoadFloors:
                    m_EXLoadingBarSprite.Width = 196;
                    break;
                case ExpandTheGungeon.LoadStatus.LoadCleanup:
                    m_EXLoadingBarSprite.Width = 200;
                    break;
                case ExpandTheGungeon.LoadStatus.LoadFinished:
                    m_EXLoadingBarSprite.Width = 218;
                    break;
            }
        }


        public static void CreateInitialLoadingScreen(FoyerPreloader foyerPreloader, string InitialText) {
            if (!foyerPreloader) return;
            foyerPreloader.LoadingLabel.gameObject.SetActive(true);
            foyerPreloader.LanguageManager.enabled = true;
            LoadingScreenObject = new GameObject("Expand Startup Loading Screen Panel Child");
            LoadingBarObject = new GameObject("Expand LoadBar Panel Child");
            DontDestroyOnLoad(LoadingScreenObject);
            DontDestroyOnLoad(LoadingBarObject);

            EXLoadingScreenSprite = LoadingScreenObject.AddComponent<dfTextureSprite>();
            EXLoadingScreenSprite.Anchor = dfAnchorStyle.All;
            EXLoadingScreenSprite.IsVisible = true;
            EXLoadingScreenSprite.IsInteractive = true;
            EXLoadingScreenSprite.Size = new Vector2(218, 99);
            EXLoadingScreenSprite.MinimumSize = Vector2.zero;
            EXLoadingScreenSprite.MaximumSize = Vector2.zero;
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
            // typeof(dfTextureSprite).GetField("renderOrder", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(EXLoadingScreenSprite, 6);


            EXLoadingBarSprite = LoadingBarObject.AddComponent<dfTextureSprite>();
            EXLoadingBarSprite.Anchor = dfAnchorStyle.All;
            EXLoadingBarSprite.IsVisible = false;
            EXLoadingBarSprite.IsInteractive = true;
            EXLoadingBarSprite.Size = new Vector2(218, 99);
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
            // typeof(dfTextureSprite).GetField("renderOrder", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(EXLoadingBarSprite, 5);

            LoadTextInstance = foyerPreloader.LoadingLabel;
            LoadTextInstance.Text = InitialText;
            LoadTextInstance.VerticalAlignment = dfVerticalAlignment.Bottom;
            LoadTextInstance.TextAlignment = TextAlignment.Center;
            LoadTextInstance.AutoSize = false;
            LoadTextInstance.ClipChildren = true;
            LoadTextInstance.AutoHeight = false;
            LoadTextInstance.Size = new Vector2(218, 99);
            LoadTextInstance.Position += new Vector3(0, 32);
            LoadTextInstance.IsLocalized = false;

            if (ExpandSettings.EnableAsyncAssetLoading) {
                /*LoadTextInstance.AddPrefab(LoadingBarObject);
                LoadTextInstance.AddPrefab(LoadingScreenObject);*/
                m_EXLoadingBarSprite = AddPrefab(LoadTextInstance, LoadingBarObject);
                m_EXLoadingScreenSprite = AddPrefab(LoadTextInstance, LoadingScreenObject);
            } else {
                m_EXLoadingScreenSprite = AddPrefab(LoadTextInstance, LoadingScreenObject);
            }
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

        public static void CreateOldWestLoadingScreen (FoyerPreloader foyerPreloader) {
            foyerPreloader.Throbber.Atlas.Material.mainTexture = EXLoadScreenThrobber_West;
        }

        public static void CreateGlitchedLoadingScreen (FoyerPreloader foyerPreloader) {
            foyerPreloader.LoadingLabel.Glitchy = true;
        }
        
        public static void CreateBackroomsLoadingScreen (FoyerPreloader foyerPreloader) {
            foyerPreloader.LoadingLabel.Text = ("No-Clipping" + txtCloser);
        }
    }
}

