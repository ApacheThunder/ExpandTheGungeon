using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using MonoMod.RuntimeDetour;
using BepInEx;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandLoadingScreens;
using HarmonyLib;

namespace ExpandTheGungeon {

    [BepInDependency("etgmodding.etg.mtgapi", BepInDependency.DependencyFlags.HardDependency)]
    // [BepInDependency("alexandria.etgmod.alexandria", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(GUID, ModName, VERSION)]
    public class ExpandTheGungeon : BaseUnityPlugin {

        public enum LoadStatus {
            PreStartup,
            PreInit,
            LoadStart,
            LoadAudio,
            LoadSprites,
            LoadItems,
            LoadPrefabs,
            LoadEnemies,
            LoadRooms,
            LoadFloors,
            LoadCleanup,
            LoadFinished,
            LoadError,
            Other
        };
        
        public static LoadStatus loadStatus {
            get { return m_LoadStatus; }
            set {
                m_LoadStatus = value;
                if (value != LoadStatus.LoadError) lastNonErrorState = value;
                ExpandLoadingScreen.RefreshText = true;
            }

        }

        public static LoadStatus lastNonErrorState = LoadStatus.PreStartup;

        private static LoadStatus m_LoadStatus = LoadStatus.PreStartup;
        
        
        public static Texture2D ModLogo;
        public static Texture2D ModLogoMini;

        public static Hook GameManagerHook;
        public static Hook initializeMainMenuHook;
        
        
        public const string GUID = "ApacheThunder.etg.ExpandTheGungeon";
        public const string ModName = "ExpandTheGungeon";
        public const string VERSION = "3.0.8";
        public static string ZipFilePath;
        public static string FilePath;
        public static string ResourcesPath;
        
        public static bool ItemAPISetup = false;
        public static bool ListsCleared = false;
        public static bool PreventInput = true;
        public static bool MrCapInUse = false;
        public static bool PortableShipInUse = false;

        public static string ModShaderBundleName = "ExpandShaders";

        public const string ModAssetBundleName = "ExpandSharedAuto";
        public const string ModLinuxShaderBundleName = "ExpandShaders_Linux";
        public const string ModMacOSShaderBundleName = "ExpandShaders_MacOS";
        public const string ModSettingsFileName = "ExpandTheGungeon_Settings.txt";
        public const string ModAudioAssetBundleName = "ExpandAudio";
        public const string ModSpriteAssetBundleName = "ExpandSpritesBase";
        public const string ModSoundBankName = "EX_SFX";
        
        public static StringDB Strings;
        public static List<string> ExceptionTextList;
        public static string ExceptionText;
        
        
        public void Start() {
            Harmony HarmonyPatches = new Harmony(GUID);
            HarmonyPatches.PatchAll(Assembly.GetExecutingAssembly());

            FilePath = this.FolderPath();
            ZipFilePath = this.FolderPath();
            
            ResourcesPath = ETGMod.ResourcesDirectory;
            
            ExceptionTextList = new List<string>();

            bool m_SettingsError = false;

            try {
                ExpandSettings.LoadSettings();
            } catch (Exception ex) {
                m_SettingsError = true;
                ExpandAssets.LastException = ex;
            }

            ExpandLoadingScreen.Init();

            if (m_SettingsError) {
                ExpandAssets.HandleError();
                ExpandConsole.itemList = new List<string>();
                return;
            }

            ExpandConsole.itemList = new List<string>() {
                    "Baby Good Hammer",
                    "Corruption Bomb",
                    "Table Tech Assassin",
                    "ex:bloodied_scarf",
                    "Cronenberg Bullets",
                    "Mimiclay",
                    "The Lead Key",
                    "RockSlide",
                    "Corrupted Master Round",
                    "Wooden Crest",
                    "Bootleg Pistol",
                    "Bootleg Shotgun",
                    "Bootleg Machine Pistol",
                    "Bulletkin Gun",
                    "Baby Sitter",
                    "Pow Block",
                    "Cursed Brick",
                    "Black Revolver",
                    "Golden Revolver",
                    "Sonic Box",
                    "The Third Eye",
                    "Clown Friend",
                    "Clown Bullets",
                    "Portable Elevator",
                    "Portable Ship",
                    "Old Key",
                    "Mr Cap"
                };

            switch (Application.platform) {
                case RuntimePlatform.LinuxPlayer:
                    ModShaderBundleName = ModLinuxShaderBundleName;
                    break;
                case RuntimePlatform.OSXPlayer:
                    ModShaderBundleName = ModMacOSShaderBundleName;
                    break;
            }

            loadStatus = LoadStatus.PreInit;
            
            try {
                ExpandAssets.InitCustomAssetBundles();

                AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle(ModAssetBundleName);

                if (expandSharedAssets1) {
                    ExpandFoyer.EXFoyerChecker = expandSharedAssets1.LoadAsset<GameObject>("EXFoyerChecker");
                    ModLogo = expandSharedAssets1.LoadAsset<Texture2D>("EXLogo");
                    ModLogoMini = expandSharedAssets1.LoadAsset<Texture2D>("EXLogoMini");
                }
                
                expandSharedAssets1 = null;
                
                ExpandObjectDatabase.InitObjectDatabase();

                ExpandPrefabs.PreInit();
            } catch (Exception ex) {
                ExpandAssets.LastException = ex;
                ExpandAssets.HandleError();
                return;
            }
            ETGModMainBehaviour.WaitForGameManagerStart(GMStart);
        }

        
        public void GMStart(GameManager gameManager) {
            loadStatus = LoadStatus.LoadStart;
            if(ExpandSettings.EnableAsyncAssetLoading)ExpandLoadingScreen.UpdateLoadingBar(loadStatus);

            if (ExceptionTextList.Count > 0) {
                foreach (string text in ExceptionTextList)ETGModConsole.Log(text);
                return;
            }

            ExpandFoyer.CreateFoyerController();
            
            try {
                Strings = new StringDB();

                ExpandHooks.InstallMidGameSaveHooks();
                if (ExpandSettings.EnableLogo) {
                    initializeMainMenuHook = new Hook(
                        typeof(MainMenuFoyerController).GetMethod("InitializeMainMenu", BindingFlags.Public | BindingFlags.Instance),
                        typeof(ExpandTheGungeon).GetMethod(nameof(ExpandTheGungeon.InitializeMainMenuHook), BindingFlags.Public | BindingFlags.Instance),
                        typeof(MainMenuFoyerController)
                    );
                }
                gameManager.OnNewLevelFullyLoaded += ExpandObjectMods.InitSpecialMods;

                ExpandHooks.InstallRequiredHooks();
                // ExpandDungeonMusicAPI.InitHooks();
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: Exception occured while installing hooks!");
                ExpandLoadingScreen.LoadText[LoadStatus.LoadError] = "ERROR: Exception while installing hooks!";
                loadStatus = LoadStatus.LoadError;
                ExpandLoadingScreen.UpdateText(ExpandLoadingScreen.LoadText[loadStatus], true);
                Debug.Log("[ExpandTheGungeon] ERROR: Exception occured while installing hooks!");
                Debug.LogException(ex);
                return;
            }
                        
            loadStatus = LoadStatus.LoadAudio;
            
            AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle(ModAssetBundleName);
            AssetBundle expandAudio = ResourceManager.LoadAssetBundle(ModAudioAssetBundleName);
            AssetBundle sharedAssets = ResourceManager.LoadAssetBundle("shared_auto_001");
            AssetBundle braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");
            
            try {
                ExpandAssets.InitAudio(expandAudio, ModSoundBankName);
            } catch (Exception ex) {
                ExpandAssets.LastException = ex;
                ExpandAssets.HandleError();
                return;
            }

            loadStatus = LoadStatus.LoadSprites;
                        
            try {
                // Init Custom GameLevelDefinitions
                ExpandDungeonPrefabs.InitFloorDefinitions(gameManager);
                // Init Custom Sprite Collections
                ExpandPrefabs.InitSpriteCollections(expandSharedAssets1, sharedAssets);
                ExpandEnemyDatabase.InitSpriteCollections(expandSharedAssets1);
                
            } catch (Exception ex) {
                expandAudio = null;
                sharedAssets = null;
                braveResources = null;
                ExpandAssets.LastException = ex;
                ExpandAssets.HandleError();
                return;
            }
            
            loadStatus = LoadStatus.LoadItems;
            
            // Init ItemAPI
            ExpandAssets.SetupItemAPI(expandSharedAssets1);
            
            expandSharedAssets1 = null;
            expandAudio = null;
            sharedAssets = null;
            braveResources = null;

            if (loadStatus == LoadStatus.LoadError) {
                ExpandAssets.HandleError();
                return;
            }

            if (ExpandLoadingScreen.Instance) {
                ExpandLoadingScreen.Instance.StartCoroutine(ExpandAssets.InitAssets(gameManager));
            } else {
                gameManager.StartCoroutine(ExpandAssets.InitAssets(gameManager));
            }
        }
        
        
        /*public static void GameManager_Awake(Action<GameManager> orig, GameManager self) {
            orig(self);
            self.OnNewLevelFullyLoaded += ExpandObjectMods.InitSpecialMods;
            ExpandDungeonPrefabs.ReInitFloorDefinitions(self);
            ExpandFoyer.CreateFoyerController();
            ExpandSettings.HasVisitedBackrooms = false;
            ExpandSettings.allowGlitchFloor = false;
        }*/

        public void InitializeMainMenuHook(Action<MainMenuFoyerController> orig, MainMenuFoyerController self) {
            orig(self);
            if (ExpandSettings.EnableLogo) {
                bool frostfireInstalled = false;
                if (FindObjectsOfType<BaseUnityPlugin>() != null) {
                    foreach (BaseUnityPlugin plugin in FindObjectsOfType<BaseUnityPlugin>()) {
                        if (plugin.Info.Metadata.GUID.ToLower().Contains("frostandgunfire"))frostfireInstalled = true;
                    }
                }
                if (frostfireInstalled) {
                    SetupLabel(self.TitleCard, ("ExpandTheGungeon: " + "v" + VERSION), Color.white, new Vector2(380f, 22), new Vector2(264, 22), new Vector2(276, 22));
                } else {
                    if (ModLogo == null) {
                        ModLogo = ExpandAssets.LoadAsset<Texture2D>("EXLogo");
                        ModLogo.filterMode = FilterMode.Point;
                    }
                    ((dfTextureSprite)self.TitleCard).Texture = ModLogo;
                    SetupLabel(self.TitleCard, ("v" + VERSION), Color.black, new Vector2(564f, -28), new Vector2(64, 16), new Vector2(74, 16));
                }
            }
        }
        

        private void SetupLabel(dfControl controlParent, string TextString, Color TextColor, Vector3 UIPosition, Vector2 Size, Vector2 MaxSize) {
            dfTiledSprite referenceLabel = ExpandAssets.LoadOfficialAsset<GameObject>("Weapon Skull Ammo FG", ExpandAssets.AssetSource.SharedAuto1).GetComponent<dfTiledSprite>();
            dfFont referenceFont = ExpandAssets.LoadOfficialAsset<GameObject>("04b03_df40", ExpandAssets.AssetSource.SharedAuto1).GetComponent<dfFont>();
            dfLabel m_NewLabel = controlParent.AddControl<dfLabel>();
            m_NewLabel.name = "EXVersionLabel";
            m_NewLabel.Atlas = referenceLabel.Atlas;
            m_NewLabel.Font = referenceFont;
            m_NewLabel.Anchor = dfAnchorStyle.Right;
            m_NewLabel.IsEnabled = true;
            m_NewLabel.IsVisible = true;
            m_NewLabel.IsInteractive = true;
            m_NewLabel.Tooltip = string.Empty;
            m_NewLabel.Pivot = dfPivotPoint.BottomRight;
            m_NewLabel.zindex = 9;
            m_NewLabel.Opacity = 1f;
            m_NewLabel.Color = TextColor;
            m_NewLabel.DisabledColor = Color.gray;
            m_NewLabel.Size = Size;
            m_NewLabel.MinimumSize = m_NewLabel.Size;
            m_NewLabel.MaximumSize = MaxSize;
            m_NewLabel.ClipChildren = false;
            m_NewLabel.InverseClipChildren = false;
            m_NewLabel.TabIndex = -1;
            m_NewLabel.CanFocus = false;
            m_NewLabel.AutoFocus = false;
            m_NewLabel.IsLocalized = false;
            m_NewLabel.HotZoneScale = Vector2.one;
            m_NewLabel.AllowSignalEvents = true;
            m_NewLabel.PrecludeUpdateCycle = false;
            m_NewLabel.PerCharacterOffset = Vector2.zero;
            m_NewLabel.PreventFontChanges = true;
            m_NewLabel.BackgroundSprite = string.Empty;
            m_NewLabel.BackgroundColor = Color.white;
            if (TextColor == Color.white) { m_NewLabel.BackgroundColor = Color.black; }
            m_NewLabel.AutoSize = true;
            m_NewLabel.AutoHeight = false;
            m_NewLabel.WordWrap = false;
            m_NewLabel.Text = TextString;
            m_NewLabel.BottomColor = Color.white;
            if (TextColor == Color.white) { m_NewLabel.BottomColor = Color.black; }
            m_NewLabel.TextAlignment = TextAlignment.Right;
            m_NewLabel.VerticalAlignment = dfVerticalAlignment.Top;
            m_NewLabel.TextScale = 0.5f;
            m_NewLabel.TextScaleMode = dfTextScaleMode.None;
            m_NewLabel.CharacterSpacing = 0;
            m_NewLabel.ColorizeSymbols = false;
            m_NewLabel.ProcessMarkup = false;
            m_NewLabel.Outline = false;
            m_NewLabel.OutlineSize = 0;
            m_NewLabel.ShowGradient = false;
            m_NewLabel.OutlineColor = Color.white;
            if (TextColor == Color.white) { m_NewLabel.OutlineColor = Color.black; }
            m_NewLabel.Shadow = false;
            m_NewLabel.ShadowColor = Color.gray;
            m_NewLabel.ShadowOffset = new Vector2(1, -1);
            m_NewLabel.Padding = new RectOffset() { left = 0, right = 0, top = 0, bottom = 0 };
            m_NewLabel.TabSize = 48;
            m_NewLabel.MaintainJapaneseFont = false;
            m_NewLabel.MaintainKoreanFont = false;
            m_NewLabel.MaintainRussianFont = false;
            m_NewLabel.Position = UIPosition;
            referenceFont = null;
            referenceLabel = null;
        }
    }
}

