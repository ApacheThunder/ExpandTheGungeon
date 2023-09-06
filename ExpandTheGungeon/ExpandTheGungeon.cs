using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Dungeonator;
using MonoMod.RuntimeDetour;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandDungeonFlows;
using BepInEx;

namespace ExpandTheGungeon {

    [BepInDependency("etgmodding.etg.mtgapi")]
    [BepInPlugin(GUID, ModName, VERSION)]
    public class ExpandTheGungeon : BaseUnityPlugin {
        
        public static Texture2D ModLogo;
        public static Hook GameManagerHook;
        public static Hook initializeMainMenuHook;

        public const string GUID = "ApacheThunder.etg.ExpandTheGungeon";
        public const string ModName = "ExpandTheGungeon";
        public const string VERSION = "2.9.13";
        public static string ZipFilePath;
        public static string FilePath;
        public static string ResourcesPath;
        
        public static bool ItemAPISetup = false;
        public static bool ListsCleared = false;
        
        public const string ModSettingsFileName = "ExpandTheGungeon_Settings.txt";
        public const string ModAssetBundleName = "ExpandSharedAuto";
        public const string ModSpriteAssetBundleName = "ExpandSpritesBase";
        public const string ModSoundBankName = "EX_SFX";
        public const string ConsoleCommandName = "expand";

        public static StringDB Strings;
        public static List<string> ExceptionText;
        
        private static GameObject m_FoyerCheckerOBJ;
        private static List<string> itemList;

        private bool m_IsCommandValid(string[] CommandText, string validCommands, string sourceSubCommand) {
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
                "Old Key"
            };
            
            ExpandAssets.InitCustomAssetBundles(ModName);            
                        
            ETGModMainBehaviour.WaitForGameManagerStart(GMStart);
        }


        public void GMStart(GameManager gameManager) {
            if (ExceptionText.Count > 0) {
                foreach (string text in ExceptionText) { ETGModConsole.Log(text); }
                return;
            }

            try {
                Strings = new StringDB();

                ExpandHooks.InstallMidGameSaveHooks();
                if (ExpandSettings.EnableLogo) {
                    initializeMainMenuHook = new Hook(
                        typeof(MainMenuFoyerController).GetMethod("InitializeMainMenu", BindingFlags.Public | BindingFlags.Instance),
                        typeof(ExpandTheGungeon).GetMethod(nameof(InitializeMainMenuHook), BindingFlags.Public| BindingFlags.Instance),
                        typeof(MainMenuFoyerController)
                    );
                }
                gameManager.OnNewLevelFullyLoaded += ExpandObjectMods.InitSpecialMods;
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: Exception occured while installing hooks!");
                Debug.LogException(ex);
                return;
            }
            
            try {
                ExpandHooks.InstallRequiredHooks();
                ExpandDungeonMusicAPI.InitHooks();
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: Exception occured while installing hooks!");
                Debug.Log("[ExpandTheGungeon] ERROR: Exception occured while installing hooks!");
                Debug.LogException(ex);
                return;
            }

            AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle(ModAssetBundleName);
            AssetBundle sharedAssets = ResourceManager.LoadAssetBundle("shared_auto_001");
            AssetBundle sharedAssets2 = ResourceManager.LoadAssetBundle("shared_auto_002");
            AssetBundle braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");
            AssetBundle enemiesBase = ResourceManager.LoadAssetBundle("enemies_base_001");

            ExpandAssets.InitAudio(expandSharedAssets1, ModSoundBankName);
            // Init Custom GameLevelDefinitions
            ExpandDungeonPrefabs.InitCustomGameLevelDefinitions(braveResources, gameManager);
            // Init Custom Sprite Collections
            ExpandPrefabs.InitSpriteCollections(expandSharedAssets1, sharedAssets);
            ExpandEnemyDatabase.InitSpriteCollections(expandSharedAssets1);

            // Init ItemAPI
            SetupItemAPI(expandSharedAssets1);

            try {
                // Init Prefab Databases
                ExpandPrefabs.InitPrefabs(expandSharedAssets1, sharedAssets, sharedAssets2, braveResources, enemiesBase);
                // Init Custom Enemy Ammonomicon Data
                ExpandAmmonomiconDatabase.Init(expandSharedAssets1);
                // Init Custom Enemy Prefabs
                ExpandEnemyDatabase.InitPrefabs(expandSharedAssets1);
                // Init Custom Room Prefabs
                ExpandRoomPrefabs.InitCustomRooms(expandSharedAssets1, sharedAssets, sharedAssets2, braveResources, enemiesBase);
                // Init Custom DungeonFlow(s)
                ExpandDungeonFlow.InitDungeonFlows(sharedAssets2);
                // Things that need existing stuff created first have code run here
                BootlegGuns.PostInit();
                ClownFriend.PostInit();
                // Dungeon Prefabs
                ExpandDungeonPrefabs.InitDungoenPrefabs(expandSharedAssets1, sharedAssets, sharedAssets2, braveResources);
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: Exception occured while building prefabs!", true);
                Debug.LogException(ex);
                expandSharedAssets1 = null;
                sharedAssets = null;
                sharedAssets2 = null;
                enemiesBase = null;
                braveResources = null;
                return;
            }
            
            // Modified version of Anywhere mod
            DungeonFlowModule.Install();

            InitConsoleCommands(ConsoleCommandName);

            CreateFoyerController();

            ETGModConsole.DungeonDictionary.Add("belly", "tt_belly");
            ETGModConsole.DungeonDictionary.Add("monster", "tt_belly");
            ETGModConsole.DungeonDictionary.Add("jungle", "tt_jungle");
            ETGModConsole.DungeonDictionary.Add("office", "tt_office");
            ETGModConsole.DungeonDictionary.Add("phobos", "tt_phobos");
            ETGModConsole.DungeonDictionary.Add("space", "tt_space");
            ETGModConsole.DungeonDictionary.Add("west", "tt_west");
            ETGModConsole.DungeonDictionary.Add("oldwest", "tt_west");

            // Null bundles when done with them to avoid game crash issues
            expandSharedAssets1 = null;
            sharedAssets = null;
            sharedAssets2 = null;
            enemiesBase = null;
            braveResources = null;
        }
        
        public static void CreateFoyerController() {
            if (!m_FoyerCheckerOBJ) {
                m_FoyerCheckerOBJ = new GameObject("ExpandTheGungeon Foyer Checker", new Type[] { typeof(ExpandFoyer) });
            } else {
                return;
            }
        }
                
        private void SetupItemAPI(AssetBundle expandSharedAssets1) {
            if (!ItemAPISetup) {
                try {
                    ETGMod.Assets.SetupSpritesFromAssembly(Assembly.GetExecutingAssembly(), "ExpandTheGungeon/Sprites");
                    Tools.Init();
                    ItemBuilder.Init();
                    BabyGoodHammer.Init(expandSharedAssets1);
                    CorruptionBomb.Init(expandSharedAssets1);
                    if (ExpandSettings.EnableBloodiedScarfFix) { ExpandRedScarf.Init(expandSharedAssets1); }
                    TableTechAssassin.Init(expandSharedAssets1);
                    CorruptedJunk.Init(expandSharedAssets1);
                    BootlegGuns.Init(expandSharedAssets1);
                    CronenbergBullets.Init(expandSharedAssets1);
                    Mimiclay.Init(expandSharedAssets1);
                    TheLeadKey.Init(expandSharedAssets1);
                    RockSlide.Init(expandSharedAssets1);
                    CustomMasterRounds.Init(expandSharedAssets1);
                    WoodenCrest.Init(expandSharedAssets1);
                    BulletKinGun.Init();
                    BabySitter.Init(expandSharedAssets1);
                    PowBlock.Init(expandSharedAssets1);
                    CursedBrick.Init(expandSharedAssets1);
                    SonicRing.Init(expandSharedAssets1);
                    SonicBox.Init(expandSharedAssets1);
                    ThirdEye.Init(expandSharedAssets1);
                    ClownBullets.Init(expandSharedAssets1);
                    ClownFriend.Init(expandSharedAssets1);
                    PortableElevator.Init(expandSharedAssets1);
                    PortableShip.Init(expandSharedAssets1);
                    WestBrosRevolverGenerator.Init();
                    HotShotShotGun.Init();
                    ExpandKeyBulletPickup.Init(expandSharedAssets1);

                    // Setup Custom Synergies. Do this after all custom items have been Init!;
                    ExpandSynergies.Init();
                    
                    ItemAPISetup = true;
                } catch (Exception e2) {
                    Tools.PrintException(e2, "FF0000");
                }
            }
        }

        private void GameManager_Awake(Action<GameManager> orig, GameManager self) {
            orig(self);
            
            self.OnNewLevelFullyLoaded += ExpandObjectMods.InitSpecialMods;
            ExpandDungeonPrefabs.ReInitFloorDefinitions(self);
            CreateFoyerController();
        }

        public void InitializeMainMenuHook(Action<MainMenuFoyerController> orig, MainMenuFoyerController self) {
            orig(self);
            if (ExpandSettings.EnableLogo) {
                bool frostfireInstalled = false;
                if (FindObjectsOfType<BaseUnityPlugin>() != null) {
                    foreach (BaseUnityPlugin plugin in FindObjectsOfType<BaseUnityPlugin>()) {
                        if (plugin.Info.Metadata.GUID.ToLower().Contains("frostandgunfire")) { frostfireInstalled = true; }
                    }
                }
                if (frostfireInstalled) {
                    SetupLabel(self.TitleCard, ("ExpandTheGungeon: " + "v" + VERSION), Color.white, new Vector2(380f, 22), new Vector2(264, 22), new Vector2(276, 22));
                } else {
                    if (ModLogo == null) { ModLogo = ExpandAssets.LoadAsset<Texture2D>("EXLogo"); }
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

        private void InitConsoleCommands(string MainCommandName) {
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

        /*private void ExpandTestCommand(string[] consoleText) {
            // Tools.ExportTexture((GameManager.Instance.PrimaryPlayer.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)[0].sprite.Collection.materials[0].mainTexture as Texture2D).GetRW());
            // Tools.DumpSpecificSpriteCollection(ExpandWesternBrosPrefabBuilder.Collection);
            // GameObject NewChestTest = Instantiate(ExpandObjectDatabase.EndTimesChest, (GameManager.Instance.PrimaryPlayer.transform.position + new Vector3(0, 2, 0)), Quaternion.identity);

            // GameManager.Instance.PrimaryPlayer.CurrentRoom.RegisterInteractable(NewChestTest.GetComponent<ArkController>());

            // Tools.ExportTexture(Pixelator.Instance.sourceOcclusionTexture);

            // m_texturedOcclusionTarget
            
        }*/

        private void ExpandConsoleInfo(string[] consoleText) {
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
        
        private void ExpandDebug(string[] consoleText) {
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

        private void ExpandDumpLayout(string[] consoleText) {
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
        
        private void ExpandCustomItemsInfo(string[] consoleText) {
            ETGModConsole.Log("Custom Items: ", false);
            foreach (string str in itemList) { ETGModConsole.Log("    " + str, false); }
        }

        private void ExpandYouTubeSafeCommand(string[] consoleTest) {
            if (ExpandSettings.youtubeSafeMode) {
                ETGModConsole.Log("No longer YouTube safe.", false);
                ExpandSettings.youtubeSafeMode = false;
            } else {
                ETGModConsole.Log("Now YouTube Safe.", false);
                ExpandSettings.youtubeSafeMode = true;
            }
        }
        
        private void ExpandExportSettings(string[] consoleText) {
            ExpandSettings.SaveSettings();
            ETGModConsole.Log("[ExpandTheGungeon] Settings have been saved!");
            return;
        }
        
        // Setup console command to point to this function. Expects name of collection followed by resolution X/Y (exmaple: 512 512 EXItemCollection)
        // If you wish to manually specify path of output files add path as 4th parameter.
        public void ExpandSerializeCollection(string[] consoleText) {
            try {
                ExpandAssets.InitSpritesAssetBundle();
            } catch (Exception ex) {
                string ErrorMessage = "[ExpandTheGungeon] ERROR: Exception while loading sprite asset bundles! This is an option asset bundle however it is required for building sprite collections!";
                Debug.Log(ErrorMessage);
                Debug.LogException(ex);
            }
            if (ExpandLists.SpriteCollections == null) {
                ExpandLists.SpriteCollections = new Dictionary<string, List<string>>() {
                    ["EXTrapCollection"] = ExpandLists.EXTrapCollection,
                    ["EXSpaceCollection"] = ExpandLists.EXSpaceCollection,
                    ["EXOfficeCollection"] = ExpandLists.EXOfficeCollection,
                    ["EXJungleCollection"] = ExpandLists.EXJungleCollection,
                    ["EXPortableElevatorCollection"] = ExpandLists.EXPortableElevatorCollection,
                    ["EXBalloonCollection"] = ExpandLists.EXBalloonCollection,
                    ["EXItemCollection"] = ExpandLists.EXItemCollection,
                    ["ClownkinCollection"] = ExpandLists.ClownkinCollection,
                    ["EXFoyerCollection"] = ExpandLists.EXFoyerCollection,
                    ["GungeoneerMimicCollection"] = ExpandLists.EXGungeoneerMimicCollection,
                    ["EXSecretDoorCollection"] = ExpandLists.EXSecretDoorCollection
                };
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

