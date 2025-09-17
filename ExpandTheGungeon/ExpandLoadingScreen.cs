using BepInEx;
using ExpandTheGungeon.ExpandUtilities;
using MonoMod.RuntimeDetour;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using static ExpandTheGungeon.ExpandUtilities.ReflectionHelpers;

namespace ExpandTheGungeon {

    public class ExpandLoadingScreen : MonoBehaviour {

        public static Hook FoyerPreloadHook; 
        public static FoyerPreloader Instance;

        public static dfLabel LoadTextInstance;
        public static dfTextureSprite EXLoadingScreenSprite;

        public static Texture2D EXLoadScreenLogo;
        public static Texture2D EXLoadScreenThrobber_West;
        public static Texture2D EXLoadScreenThrobber_Backrooms;
        public static Texture2D EXLoadScreenThrobber_Jungle;
        public static Texture2D EXLoadScreenThrobber_Belly;


        public static readonly string txtOffset = "\n\n\n\n\n";
        public static string CurrentText = "Preparing Gungeon Expansion. Please wait...";


        public static bool HookActive = false;
        public static bool AssetsReady = false;
        
        private static GameObject LoadingScreenObject;

        public static void Init() {
            FoyerPreloadHook = new Hook(
                typeof(FoyerPreloader).GetMethod(nameof(FoyerPreloader.Update), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandLoadingScreen).GetMethod(nameof(UpdateHook), BindingFlags.Public | BindingFlags.Instance),
                typeof(FoyerPreloader)
            );

            EXLoadScreenLogo = ExpandUtility.GetTextureFromResource("EXLoadScreenLogo.png", new IntVector2(218, 99));
            EXLoadScreenThrobber_West = ExpandUtility.GetTextureFromResource("EXLoadingScreen_West.png", new IntVector2(1024, 512));
            AssetsReady = true;
            HookActive = true;
        }

        public void UpdateHook(Action<FoyerPreloader>orig, FoyerPreloader self) {
            if (!ExpandTheGungeon.ModInitFinished && ExpandTheGungeon.PreventInput) {
                if (UnityInput.Current != null) UnityInput.Current.ResetInputAxes();
            } else if (ExpandTheGungeon.ModInitFinished && ExpandTheGungeon.PreventInput) {
                ExpandTheGungeon.PreventInput = false;
                ETGModConsole.Log("test");
            }

            if (!AssetsReady) return;
            if (!HookActive) {
                orig(self);
                return; 
            }
            if (!Instance) Instance = self;
            if (!self) return;

            bool m_wasFirstLoadScreen = ReflectGetField<bool>(typeof(FoyerPreloader), "m_wasFirstLoadScreen", self);
                        
            if (!LoadingScreenObject && m_wasFirstLoadScreen) CreateInitialLoadingScreen(self);
            
            bool m_isLoading = ReflectGetField<bool>(typeof(FoyerPreloader), "m_isLoading", self);
            
            if (m_wasFirstLoadScreen && Time.frameCount >= 5 && !m_isLoading) {
                self.StartCoroutine(EXAsyncLoadFoyer(self, m_wasFirstLoadScreen));
                typeof(FoyerPreloader).GetField("m_isLoading", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(self, true);
            }
        }

        public static void UpdateText(string Text) {
            if (Instance) {
                Instance.LoadingLabel.Text = (txtOffset + Text);
                if (ExpandSettings.debugMode) Debug.Log("[" + ExpandTheGungeon.ModName + "] " + Text);
            }
        }

        public static IEnumerator UpdateTextOnFrame(string Text) {
            if (Instance) {
                Instance.LoadingLabel.Text = (txtOffset + Text);
                if (ExpandSettings.debugMode) Debug.Log("[" + ExpandTheGungeon.ModName + "] " + Text);
                yield return new WaitForEndOfFrame();
            }
            yield break;
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

        public static IEnumerator WaitForFrame() {
            yield return new WaitForEndOfFrame();
            yield break;
        }

        /*public static IEnumerator WaitForTime(float waitTime = 30) {
            yield return new WaitForSeconds(waitTime);
            yield break;
        }*/


        public IEnumerator DestroyLoadScreen(FoyerPreloader Preloader) {
            DebugTime.Log("Starting to destroy the load screen", new object[0]);
            int skipFrames = 3;
            for (int i = 0; i < skipFrames; i++) yield return null;
            DebugTime.Log("Finished destroying the load screen", new object[0]);
            Destroy(Preloader.gameObject);
            yield break;
        }

        public static void CreateInitialLoadingScreen(FoyerPreloader foyerPreloader) {
            if (!foyerPreloader) return;
            foyerPreloader.LoadingLabel.gameObject.SetActive(true);
            foyerPreloader.LanguageManager.enabled = true;
            LoadingScreenObject = new GameObject("Expand Loading Screen Panel Child");
            // LoadingScreenObject.transform.position = new Vector3(-0.8074036f, 0.4370346f);
            /*BoxCollider loadScreenCollider = LoadingScreenObject.AddComponent<BoxCollider>();
            loadScreenCollider.size = new Vector3(1.6148149f, 0.5160494f, 0.001f);
            loadScreenCollider.center = new Vector3(0.80740744f, -0.2580247f);*/

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

            LoadTextInstance = foyerPreloader.LoadingLabel;
            LoadTextInstance.AddPrefab(LoadingScreenObject);
            LoadTextInstance.AutoSize = false;
            LoadTextInstance.AutoHeight = false;
            LoadTextInstance.Size = new Vector2(218, 99);
            LoadTextInstance.Position += new Vector3(0, 10);
            LoadTextInstance.TextAlignment = TextAlignment.Center;
            LoadTextInstance.Text = (txtOffset + CurrentText);
            LoadTextInstance.IsLocalized = false;            
        }

        public static void CreateOldWestLoadingScreen (FoyerPreloader foyerPreloader) {
            foyerPreloader.Throbber.Atlas.Material.mainTexture = EXLoadScreenThrobber_West;
        }


    }
}

