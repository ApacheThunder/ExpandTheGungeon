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
        public static Hook MainMenuFoyerUpdateHook;

        public const string GUID = "ApacheThunder.etg.ExpandTheGungeon";
        public const string ModName = "ExpandTheGungeon";
        public const string VERSION = "2.9.7";
        public static string ZipFilePath;
        public static string FilePath;
        public static string ResourcesPath;
        
        public static bool ItemAPISetup = false;
        public static bool LogoEnabled = false;
        public static bool ListsCleared = false;

        public static string ExceptionText;
        public static string ExceptionText2;

        public const string ModSettingsFileName = "ExpandTheGungeon_Settings.txt";
        public const string ModAssetBundleName = "ExpandSharedAuto";
        public const string ModSpriteAssetBundleName = "ExpandSpritesBase";
        public const string ModSoundBankName = "EX_SFX";
        public const string ConsoleCommandName = "expand";

        public static AdvancedStringDB Strings;

        private static List<string> itemList;
        
        private enum WaitType { ShotgunSecret, LanguageFix, DebugFlow };

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

        private static GameObject m_FoyerCheckerOBJ;

        public void Start() {
            ExceptionText = string.Empty;
            ExceptionText2 = string.Empty;

            FilePath = this.FolderPath();
            ZipFilePath = this.FolderPath();
            
            ResourcesPath = ETGMod.ResourcesDirectory;

            try { ExpandSettings.LoadSettings(); } catch (Exception ex) { ExceptionText2 = ex.ToString(); }
           
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
                "Portable Ship"
            };
            
            ExpandAssets.InitCustomAssetBundles(ModName);

            if (!string.IsNullOrEmpty(ExceptionText)) { return; }

            if (ExpandSettings.EnableLogo) { ModLogo = ExpandAssets.LoadAsset<Texture2D>("EXLogo"); }
            
            ETGModMainBehaviour.WaitForGameManagerStart(GMStart);
        }


        public void GMStart(GameManager gameManager) {            
            try {
                Strings = new AdvancedStringDB();

                ExpandSharedHooks.InstallMidGameSaveHooks();
                if (ExpandSettings.EnableLogo) {
                    MainMenuFoyerUpdateHook = new Hook(
                        typeof(MainMenuFoyerController).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance),
                        typeof(ExpandTheGungeon).GetMethod(nameof(MainMenuUpdateHook), BindingFlags.NonPublic | BindingFlags.Instance),
                        typeof(MainMenuFoyerController)
                    );
                }
                gameManager.OnNewLevelFullyLoaded += ExpandObjectMods.InitSpecialMods;
            } catch (Exception ex) {
                // ETGModConsole can't be called by anything that occurs in Init(), so write message to static strinng and check it later.
                ExceptionText = "[ExpandTheGungeon] ERROR: Exception occured while installing hooks!";
                Debug.LogException(ex);
                return;
            }

            if (!string.IsNullOrEmpty(ExceptionText) | !string.IsNullOrEmpty(ExceptionText2)) {
                if (!string.IsNullOrEmpty(ExceptionText)) { ETGModConsole.Log(ExceptionText); }
                if (!string.IsNullOrEmpty(ExceptionText2)) { ETGModConsole.Log(ExceptionText2); }
                return;
            }

            try {
                ExpandSharedHooks.InstallRequiredHooks();
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
                // Post Init
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
                braveResources = null;
                enemiesBase = null;
                return;
            }
            
            // Modified version of Anywhere mod
            DungeonFlowModule.Install();

            InitConsoleCommands(ConsoleCommandName);

            CreateFoyerController();

            // Null bundles when done with them to avoid game crash issues
            expandSharedAssets1 = null;
            sharedAssets = null;
            sharedAssets2 = null;
            braveResources = null;
            enemiesBase = null;
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

        private void MainMenuUpdateHook(Action<MainMenuFoyerController> orig, MainMenuFoyerController self) {
            orig(self);
            if (ExpandSettings.EnableLogo && ((dfTextureSprite)self.TitleCard).Texture.name != ModLogo.name) {
                ((dfTextureSprite)self.TitleCard).Texture = ModLogo;
                LogoEnabled = true;
            }
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
            GameObject NewChestTest = Instantiate(ExpandObjectDatabase.EndTimesChest, (GameManager.Instance.PrimaryPlayer.transform.position + new Vector3(0, 2, 0)), Quaternion.identity);

            GameManager.Instance.PrimaryPlayer.CurrentRoom.RegisterInteractable(NewChestTest.GetComponent<ArkController>());
            
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
                        ExpandSharedHooks.enterRoomHook = new Hook(
                            typeof(RoomHandler).GetMethod("OnEntered", BindingFlags.NonPublic | BindingFlags.Instance),
                            typeof(ExpandSharedHooks).GetMethod("EnteredNewRoomHook", BindingFlags.NonPublic | BindingFlags.Instance),
                            typeof(RoomHandler)
                        );
                    } else {
                        if (ExpandSettings.debugMode) {
                            ExpandSettings.debugMode = false;
                            if (ExpandSharedHooks.enterRoomHook != null) {
                                ETGModConsole.Log("[ExpandTheGungeon] Uninstalling RoomHandler.OnEntered Hook....");
                                ExpandSharedHooks.enterRoomHook.Dispose();
                                ExpandSharedHooks.enterRoomHook = null;
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
                    ["EXFoyerCollection"] = ExpandLists.EXFoyerCollection
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

