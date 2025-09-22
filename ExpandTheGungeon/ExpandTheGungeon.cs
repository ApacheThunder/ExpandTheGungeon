using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Dungeonator;
using MonoMod.RuntimeDetour;
using BepInEx;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandLoadingScreens;
using ExpandTheGungeon.ItemAPI;

namespace ExpandTheGungeon {

    [BepInDependency("etgmodding.etg.mtgapi", BepInDependency.DependencyFlags.HardDependency)]
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
            LoadFinished
        };

        public static LoadStatus loadStatus = LoadStatus.PreStartup;

        public static Texture2D ModLogo;
        public static Texture2D ModLogoMini;

        public static Hook GameManagerHook;
        public static Hook initializeMainMenuHook;
        
        
        public const string GUID = "ApacheThunder.etg.ExpandTheGungeon";
        public const string ModName = "ExpandTheGungeon";
        public const string VERSION = "3.0.0";
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
        public const string ConsoleCommandName = "expand";

        public static StringDB Strings;
        public static List<string> ExceptionText;
        
        private static GameObject m_FoyerCheckerOBJ;
        private static List<string> itemList;

        private static bool m_IsCommandValid(string[] CommandText, string validCommands, string sourceSubCommand) {
            if (CommandText == null) {
                if (!string.IsNullOrEmpty(validCommands) && !string.IsNullOrEmpty(sourceSubCommand)) { ETGModConsole.Log("[ExpandTheGungeon] [" + sourceSubCommand + "] ERROR: Invalid console command specified! Valid Sub-Commands: \n" + validCommands); }
                return false;
            } else if (CommandText.Length <= 0) {
                if (!string.IsNullOrEmpty(validCommands) && !string.IsNullOrEmpty(sourceSubCommand)) { ETGModConsole.Log("[ExpandTheGungeon] [" + sourceSubCommand + "] No sub-command specified. Valid Sub-Commands: \n" + validCommands); }
                return false;
            } else if (string.IsNullOrEmpty(CommandText[0])) {
                if (!string.IsNullOrEmpty(validCommands) && !string.IsNullOrEmpty(sourceSubCommand)) { ETGModConsole.Log("[ExpandTheGungeon] [" + sourceSubCommand + "] No sub-command specified. Valid Sub-Commands: \n" + validCommands); }
                return false;
            } else if (CommandText.Length > 1) {
                if (!string.IsNullOrEmpty(validCommands) && !string.IsNullOrEmpty(sourceSubCommand)) { ETGModConsole.Log("[ExpandTheGungeon] [" + sourceSubCommand + "] ERROR: Only one sub-command is accepted!. Valid Commands: \n" + validCommands); }
                return false;
            }
            return true;
        }

        public void Start() {
            FilePath = this.FolderPath();
            ZipFilePath = this.FolderPath();
            
            ResourcesPath = ETGMod.ResourcesDirectory;
            
            ExceptionText = new List<string>();

            try { ExpandSettings.LoadSettings(); } catch (Exception ex) { ExceptionText.Add(ex.ToString()); }

            ExpandLoadingScreen.Init();
            
            loadStatus = LoadStatus.PreInit;

            itemList = new List<string>() {
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
            
            ExpandAssets.InitCustomAssetBundles();

            AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle(ModAssetBundleName);

            if (expandSharedAssets1) {
                ExpandFoyer.EXFoyerChecker = expandSharedAssets1.LoadAsset<GameObject>("EXFoyerChecker");

                ModLogo = expandSharedAssets1.LoadAsset<Texture2D>("EXLogo");
                ModLogo.filterMode = FilterMode.Point;

                ModLogoMini = expandSharedAssets1.LoadAsset<Texture2D>("EXLogoMini");
                ModLogoMini.filterMode = FilterMode.Point;
            }
            
            expandSharedAssets1 = null;

            ExpandPrefabs.PreInit();
            
            ETGModMainBehaviour.WaitForGameManagerStart(GMStart);
        }

        
        public void GMStart(GameManager gameManager) {
            loadStatus = LoadStatus.LoadStart;
            if(ExpandSettings.EnableAsyncAssetLoading)ExpandLoadingScreen.UpdateLoadingBar(loadStatus);

            if (ExceptionText.Count > 0) {
                foreach (string text in ExceptionText) { ETGModConsole.Log(text); }
                return;
            }

            CreateFoyerController();
            
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
                ExpandDungeonMusicAPI.InitHooks();
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: Exception occured while installing hooks!");
                ExpandLoadingScreen.UpdateText("ERROR: Exception occured while installing hooks!");
                Debug.Log("[ExpandTheGungeon] ERROR: Exception occured while installing hooks!");
                Debug.LogException(ex);
                return;
            }

            BlackAndGoldenRevolver.InitExceptionsAndHooks();

            if (ExpandSettings.EnableAsyncAssetLoading) {
                if (ExpandLoadingScreen.Instance) {
                    ExpandLoadingScreen.Instance.StartCoroutine(ExpandAssets.InitAssetsAsync(gameManager));
                } else {
                    gameManager.StartCoroutine(ExpandAssets.InitAssetsAsync(gameManager));
                }
            } else {
                ExpandAssets.InitAssets(gameManager);
            }
        }


        public static void CreateFoyerController() {
            if (!m_FoyerCheckerOBJ) {
                m_FoyerCheckerOBJ = Instantiate(ExpandFoyer.EXFoyerChecker, Vector3.zero, Quaternion.identity);
                DontDestroyOnLoad(m_FoyerCheckerOBJ);
            } else {
                return;
            }
        }
                
        
        public void GameManager_Awake(Action<GameManager> orig, GameManager self) {
            orig(self);
            self.OnNewLevelFullyLoaded += ExpandObjectMods.InitSpecialMods;
            ExpandDungeonPrefabs.ReInitFloorDefinitions(self);
            CreateFoyerController();
            ExpandSettings.HasVisitedBackrooms = false;
            ExpandSettings.allowGlitchFloor = false;
        }

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

        public static void InitConsoleCommands(string MainCommandName) {
            ETGModConsole.Commands.AddGroup(MainCommandName, ExpandConsoleInfo);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("createSpriteCollection", ExpandSerializeCollection);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("dump_layout", ExpandDumpLayout);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("debug", ExpandDebug);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("list_items", ExpandCustomItemsInfo);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("youtubemode", ExpandYouTubeSafeCommand);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("savesettings", ExpandExportSettings);
            // ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("test", ExpandTestCommand);
            return;
        }

        /*private static void ExpandTestCommand(string[] consoleText) {
            // Tools.ExportTexture((GameManager.Instance.PrimaryPlayer.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)[0].sprite.Collection.materials[0].mainTexture as Texture2D).GetRW());
            // Tools.DumpSpecificSpriteCollection(ExpandWesternBrosPrefabBuilder.Collection);
            // GameObject NewChestTest = Instantiate(ExpandObjectDatabase.EndTimesChest, (GameManager.Instance.PrimaryPlayer.transform.position + new Vector3(0, 2, 0)), Quaternion.identity);

            // GameManager.Instance.PrimaryPlayer.CurrentRoom.RegisterInteractable(NewChestTest.GetComponent<ArkController>());

            // Tools.ExportTexture(Pixelator.Instance.sourceOcclusionTexture);

            // m_texturedOcclusionTarget

            // SpriteSerializer.DumpSpriteCollection(ExpandPrefabs.ElevatorMaintanenceRoomIcon.GetComponent<tk2dSprite>().Collection);
            // SpriteSerializer.DumpSpriteCollection(ExpandObjectDatabase.ChestBrownTwoItems.GetComponent<tk2dSprite>().Collection);
            
            SpriteSerializer.DumpSpriteCollection((PickupObjectDatabase.GetById(448) as SpawnObjectPlayerItem).objectToSpawn.transform.Find("Sprite").gameObject.GetComponent<tk2dSprite>().Collection);
            // FieldInfo field = typeof(GameManager).GetField("m_dungeon", BindingFlags.Instance | BindingFlags.NonPublic);
            // field.SetValue(GameManager.Instance, Instantiate(ExpandDungeonPrefabs.Base_Office).GetComponent<Dungeon>());
            return;
        }*/

        private static void ExpandConsoleInfo(string[] consoleText) {
            if (ETGModConsole.Commands.GetGroup(ConsoleCommandName) != null && ETGModConsole.Commands.GetGroup(ConsoleCommandName).GetAllUnitNames() != null) {
                List<string> m_CommandList = new List<string>();

                foreach (string Command in ETGModConsole.Commands.GetGroup(ConsoleCommandName).GetAllUnitNames()) { m_CommandList.Add(Command); }

                if (m_CommandList.Count <=0) { return; }

                if (!m_IsCommandValid(consoleText, string.Empty, string.Empty)) {
                    ETGModConsole.Log("[ExpandTheGungeon] No sub command specified! The following console commands are available for ExpandTheGungeon:\n", false);
                    foreach (string Command in m_CommandList) { ETGModConsole.Log("    " + Command + "\n", false); }
                    return;
                } else if (!m_CommandList.Contains(consoleText[0].ToLower())) {
                    ETGModConsole.Log("[ExpandTheGungeon] Invalid sub-command! The following console commands are available for ExpandTheGungeon:\n", false);
                    foreach (string Command in m_CommandList) { ETGModConsole.Log("    " + Command + "\n", false); }
                    return;
                }
            } else {
                return;
            }
        }
        
        private static void ExpandDebug(string[] consoleText) {
            string validSubCommands = "stats\nclearroom\nunsealroom\nfixplayerinput";
            
            if (!m_IsCommandValid(consoleText, validSubCommands, "debug")) { return; }

            RoomHandler currentRoom = GameManager.Instance.PrimaryPlayer.CurrentRoom;

            switch (consoleText[0].ToLower()) {
                case "stats":
                    if (!ExpandSettings.debugMode) {
                        ExpandSettings.debugMode = true;
                        ETGModConsole.Log("[ExpandTheGungeon] Installing RoomHandler.OnEntered Hook....");
                        ExpandHooks.enterRoomHook = new Hook(
                            typeof(RoomHandler).GetMethod("OnEntered", BindingFlags.NonPublic | BindingFlags.Instance),
                            typeof(ExpandHooks).GetMethod("EnteredNewRoomHook", BindingFlags.NonPublic | BindingFlags.Instance),
                            typeof(RoomHandler)
                        );
                    } else {
                        if (ExpandSettings.debugMode) {
                            ExpandSettings.debugMode = false;
                            if (ExpandHooks.enterRoomHook != null) {
                                ETGModConsole.Log("[ExpandTheGungeon] Uninstalling RoomHandler.OnEntered Hook....");
                                ExpandHooks.enterRoomHook.Dispose();
                                ExpandHooks.enterRoomHook = null;
                            }
                        }
                    }                
                    ETGModConsole.Log("[ExpandTheGungeon] Debug Stats: ", false);
                    ETGModConsole.Log("Debug Stats: " + ExpandSettings.debugMode.ToString(), false);
                    ETGModConsole.Log("Debug Exceptions: " + ExpandSettings.debugMode.ToString(), false);
                    break;
                case "clearroom":
                    if (currentRoom != null) {
                        List<AIActor> enemies = currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear);
                        if (enemies != null && enemies.Count > 0) {
                            for (int i = 0; i < enemies.Count; i++) {
                                currentRoom.DeregisterEnemy(enemies[i]);
                                UnityEngine.Object.Destroy(enemies[i].gameObject);
                            }
                        }
                    }
                    break;
                case "unsealroom":
                    if (currentRoom != null) {
                        if (currentRoom.IsSealed) { currentRoom.UnsealRoom(); }
                    }
                    break;
                case "fixplayerinput":
                    PlayerController primaryPlayer = GameManager.Instance.PrimaryPlayer;
                    CameraController cameraController = GameManager.Instance.MainCameraController;
                    if (cameraController && primaryPlayer) {
                        cameraController.OverridePosition = primaryPlayer.transform.position;
                        cameraController.SetManualControl(false, true);
                    }
                    if (primaryPlayer) {
                        primaryPlayer.CurrentInputState = PlayerInputState.AllInput;
                        primaryPlayer.healthHaver.IsVulnerable = true;
                    }
                    if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                        PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(primaryPlayer);
                        if (otherPlayer) {
                            otherPlayer.CurrentInputState = PlayerInputState.AllInput;
                            otherPlayer.healthHaver.IsVulnerable = true;
                        }
                    }
                    break;
                default:
                    ETGModConsole.Log("[ExpandTheGungeon] ERROR: Unknown sub-command. Valid Commands: \n" + validSubCommands);
                    return;
            }
        }

        private static void ExpandDumpLayout(string[] consoleText) {
            string validSubCommands = "currentroom\ncurrentroomhandler\nallknownroomprefabs\ncurrentdungeonlayout";

            if (!m_IsCommandValid(consoleText, validSubCommands, "dump_layout")) { return; }

            if (consoleText[0].ToLower() == "currentroom") {
                RoomDebug.DumpCurrentRoomLayout();
            } else if (consoleText[0].ToLower() == "currentroomhandler") {
                RoomHandler CurrentRoom = GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom();
                RoomDebug.DumpCurrentRoomLayout(generatedRoomHandler: CurrentRoom);
            } else if (consoleText[0] == "allknownroomprefabs") {
                ETGModConsole.Log("Saving room layouts to PNG files. Please wait...");
                foreach (WeightedRoom wRoom in ExpandPrefabs.CastleRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.SewersRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.Gungeon_RoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.AbbeyRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.MinesRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.CatacombsRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.ForgeRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.BulletHellRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                
                foreach (WeightedRoom wRoom in ExpandPrefabs.SecretRoomTable.includedRooms.elements) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                 
                ETGModConsole.Log("Room dump process complete!");
            } else if (consoleText[0].ToLower() == "currentdungeonlayout") {
                RoomDebug.LogDungeonToPNGFile();
                ETGModConsole.Log("Current Dungeon '" + GameManager.Instance.Dungeon.gameObject.name + "' has been succesfully dumped.");
            } else {
                ETGModConsole.Log("[ExpandTheGungeon] [dump_layout] ERROR: Unknown sub-command. Valid Commands: \n" + validSubCommands);
                return;
            }
        }
        
        private static void ExpandCustomItemsInfo(string[] consoleText) {
            ETGModConsole.Log("Custom Items: ", false);
            foreach (string str in itemList) { ETGModConsole.Log("    " + str, false); }
        }

        private static void ExpandYouTubeSafeCommand(string[] consoleTest) {
            if (ExpandSettings.youtubeSafeMode) {
                ETGModConsole.Log("No longer YouTube safe.", false);
                ExpandSettings.youtubeSafeMode = false;
            } else {
                ETGModConsole.Log("Now YouTube Safe.", false);
                ExpandSettings.youtubeSafeMode = true;
            }
        }
        
        private static void ExpandExportSettings(string[] consoleText) {
            ExpandSettings.SaveSettings();
            ETGModConsole.Log("[ExpandTheGungeon] Settings have been saved!");
            return;
        }
        
        // Setup console command to point to this function. Expects name of collection followed by resolution X/Y (exmaple: 512 512 EXItemCollection)
        // If you wish to manually specify path of output files add path as 4th parameter.
        public static void ExpandSerializeCollection(string[] consoleText) {
            try {
                ExpandAssets.InitSpritesAssetBundle();
            } catch (Exception ex) {
                string ErrorMessage = "[ExpandTheGungeon] ERROR: Exception while loading sprite asset bundles! This is an option asset bundle however it is required for building sprite collections!";
                Debug.Log(ErrorMessage);
                Debug.LogException(ex);
            }
            int X = 2048;
            int Y = 2048;
            List<string> SpriteList = null;
            string FallBackListName = "EXTrapCollection";
            string CollectionName = FallBackListName;
            string OverridePath = string.Empty;
            if (!ExpandLists.SpriteCollections.TryGetValue(FallBackListName, out SpriteList)) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: Default list not found!");
                return;
            }
            if (consoleText.Length > 1) {
                X = int.Parse(consoleText[0]);
                Y = int.Parse(consoleText[1]);
            } else {
                ETGModConsole.Log("[ExpandTheGungeon] Not enough commands or too many! Must provide atlas name and resolution! Please specify a name, width, and height!");
                ETGModConsole.Log("[ExpandTheGungeon] Using default resolution and collection...");
                SpriteSerializer.SerializeSpriteCollection(FallBackListName, SpriteList, X, Y);
                return;
            }
            if (consoleText.Length > 2) {
                CollectionName = consoleText[2];
                if (!ExpandLists.SpriteCollections.TryGetValue(CollectionName, out SpriteList)) {
                    ETGModConsole.Log("[ExpandTheGungeon] Requested Collection not found! Using predefined list instead!");
                    ExpandLists.SpriteCollections.TryGetValue(FallBackListName, out SpriteList);
                }
            } else {
                ETGModConsole.Log("[ExpandTheGungeon] Collection name not specified! Using predefined fall back list...");
            }
            if (consoleText.Length > 3) { OverridePath = consoleText[3]; }
            SpriteSerializer.SerializeSpriteCollection(CollectionName, SpriteList, X, Y, OverridePath);
            ETGModConsole.Log("[ExpandTheGungeon] Sprite collection successfully built and exported!");
        }
    }
}

