using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Dungeonator;
using MonoMod.RuntimeDetour;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandDungeonFlows;
using System.IO;
using System.Collections;
// using System.Runtime;
// using System.Runtime.InteropServices;



namespace ExpandTheGungeon {

    public class ExpandTheGungeon : ETGModule {
        
        public static Texture2D ModLogo;
        public static Hook GameManagerHook;
        public static Hook MainMenuFoyerUpdateHook;
        
        public static string ZipFilePath;
        public static string FilePath;
        
        public static bool ItemAPISetup = false;
        public static bool LogoEnabled = false;

        public static string ExceptionText;
        public static string ExceptionText2;

        public static readonly string ModSettingsFileName = "ExpandTheGungeon_Settings.txt";
        public static readonly string ModAssetBundleName = "ExpandSharedAuto";
        public static readonly string ModSoundBankName = "EX_SFX";
        public static readonly string ConsoleCommandName = "expand";

        private static List<string> itemList;

        private static bool m_ShotGunSecretWasActive = false;

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
        

        public override void Init() {
            
            ExceptionText = string.Empty;
            ExceptionText2 = string.Empty;
            
            ZipFilePath = Metadata.Archive;
            FilePath = Metadata.Directory;

            try { ImportSettings(); } catch (Exception ex) { ExceptionText2 = ex.ToString(); }
            
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
                "Cursed Brick"
                // "Table Tech Expand"
            };
                        
            ExpandAssets.InitCustomAssetBundle();

            ModLogo = ExpandAssets.LoadAsset<Texture2D>("EXLogo");

            try {
                ExpandSharedHooks.InstallMidGameSaveHooks();
                MainMenuFoyerUpdateHook = new Hook(
                    typeof(MainMenuFoyerController).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance),
                    typeof(ExpandTheGungeon).GetMethod("MainMenuUpdateHook", BindingFlags.NonPublic | BindingFlags.Instance),
                    typeof(MainMenuFoyerController)
                );
                GameManager.Instance.OnNewLevelFullyLoaded += ExpandObjectMods.InitSpecialMods;
            } catch (Exception ex) {
                // ETGModConsole can't be called by anything that occurs in Init(), so write message to static strinng and check it later.
                ExceptionText = "[ExpandTheGungeon] ERROR: Exception occured while installing hooks!";
                Debug.LogException(ex);
                return;
            }
        }


        public override void Start() {
            
            if (!string.IsNullOrEmpty(ExceptionText) | !string.IsNullOrEmpty(ExceptionText2)) {
                if (!string.IsNullOrEmpty(ExceptionText)) { ETGModConsole.Log(ExceptionText); }
                if (!string.IsNullOrEmpty(ExceptionText2)) { ETGModConsole.Log(ExceptionText2); }
                return;
            }
            
            ExpandSharedHooks.InstallRequiredHooks();
            
            AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle(ModAssetBundleName);
            AssetBundle sharedAssets = ResourceManager.LoadAssetBundle("shared_auto_001");
            AssetBundle sharedAssets2 = ResourceManager.LoadAssetBundle("shared_auto_002");
            AssetBundle braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");
            AssetBundle enemiesBase = ResourceManager.LoadAssetBundle("enemies_base_001");

            ExpandAssets.InitAudio(expandSharedAssets1, ModSoundBankName);

            ExpandObjectDatabase.BuildDatabase();


            // Init Custom GameLevelDefinitions
            ExpandCustomDungeonPrefabs.InitCustomGameLevelDefinitions(braveResources);

            // Init ItemAPI
            SetupItemAPI(expandSharedAssets1);

            try
            {
                // Init Prefab Databases
                ExpandPrefabs.InitCustomPrefabs(expandSharedAssets1, sharedAssets, sharedAssets2, braveResources, enemiesBase);
                // Init Custom Enemy Prefabs
                ExpandCustomEnemyDatabase.InitPrefabs(expandSharedAssets1);
                // Init Custom Room Prefabs
                ExpandRoomPrefabs.InitCustomRooms(expandSharedAssets1, sharedAssets, sharedAssets2, braveResources, enemiesBase);
                // Init Custom DungeonFlow(s)
                ExpandDungeonFlow.InitDungeonFlows(sharedAssets2);
                // Post Init
                // Things thta need existing stuff created first have code run here
                BootlegGuns.PostInit();

                ExpandLists.InvalidRatFloorRainRooms = new List<string>() {
                    ExpandRoomPrefabs.SecretBossRoom.name,
                    ExpandRoomPrefabs.ThwompCrossingVerticalNoRain.name,
                    ExpandRoomPrefabs.SecretRewardRoom.name,
                    ExpandPrefabs.DragunBossFoyerRoom.name,
                    ExpandPrefabs.DraGunExitRoom.name,
                    ExpandPrefabs.DraGunEndTimesRoom.name,
                    ExpandPrefabs.BlacksmithShop.name,
                    "Zelda Puzzle Room 1",
                    "Zelda Puzzle Room 2",
                    "Zelda Puzzle Room 3",
                    "Special Entrance"
                };
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

            if (ExpandStats.ShotgunKinSecret | ExpandStats.EnableTestDungeonFlow | ExpandStats.EnableLanguageFix) {
                GameManager.Instance.StartCoroutine(WaitForFoyerLoad());
            }

            if (ExpandStats.EnableLanguageFix) {
                GameManager.Options.CurrentLanguage = StringTableManager.GungeonSupportedLanguages.ENGLISH;
                StringTableManager.CurrentLanguage = StringTableManager.GungeonSupportedLanguages.ENGLISH;
            }

            // This should fix issus with Pasts trying to spawn inactive versions of custom enemies
            // (and any other mod that has created a custom AIActor or object that has a HealthHaver component)
            StaticReferenceManager.AllHealthHavers.Clear();
            // Remove any custom instances that use BroController
            StaticReferenceManager.AllBros.Clear();

            // Null bundles when done with them to avoid game crash issues
            expandSharedAssets1 = null;
            sharedAssets = null;
            sharedAssets2 = null;
            braveResources = null;
            enemiesBase = null;
            // HUDGC.ShowGcData = true;
        }


        public override void Exit() {  }



        private void ImportSettings() {
            if (File.Exists(Path.Combine(ETGMod.ResourcesDirectory, ModSettingsFileName))) {
                string CachedJSONText = File.ReadAllText(Path.Combine(ETGMod.ResourcesDirectory, ModSettingsFileName));
                ExpandCachedStats cachedStats = ScriptableObject.CreateInstance<ExpandCachedStats>();
                JsonUtility.FromJsonOverwrite(CachedJSONText, cachedStats);
                ExpandStats.OverwriteUserSettings(cachedStats);
            } else {
                ExpandExportSettings(null);
                return;
            }
        }


        private static IEnumerator WaitForFoyerLoad() {
            while (Foyer.DoIntroSequence && Foyer.DoMainMenu) { yield return null; }
            if (ExpandStats.EnableLanguageFix) {
                GameManager.Options.CurrentLanguage = ExpandUtility.IntToLanguage(ExpandStats.GameLanguage);
                StringTableManager.CurrentLanguage = ExpandUtility.IntToLanguage(ExpandStats.GameLanguage);
            }
            if (ExpandStats.ShotgunKinSecret) {
                CharacterCostumeSwapper[] m_Characters = UnityEngine.Object.FindObjectsOfType<CharacterCostumeSwapper>();
                if (m_Characters != null && m_Characters.Length > 0) {
                    CharacterCostumeSwapper BulletManSelector = null;
                    foreach (CharacterCostumeSwapper m_Character in m_Characters) {
                        if (m_Character?.TargetLibrary?.name == "Playable_Shotgun_Man_Swap_Animation") {
                            BulletManSelector = m_Character;
                            break;
                        }
                    }
                    yield return null;
                    if (BulletManSelector) {
                        bool Allow = (GameStatsManager.Instance.GetFlag(GungeonFlags.SECRET_BULLETMAN_SEEN_05) && GameStatsManager.Instance.GetCharacterSpecificFlag(BulletManSelector.TargetCharacter, CharacterSpecificGungeonFlags.KILLED_PAST));
                        if (Allow) {
                            FieldInfo m_active = typeof(CharacterCostumeSwapper).GetField("m_active", BindingFlags.Instance | BindingFlags.NonPublic);
                            m_active.SetValue(BulletManSelector, true);
                            BulletManSelector.AlternateCostumeSprite.renderer.enabled = true;
                            BulletManSelector.CostumeSprite.renderer.enabled = false;
                            m_ShotGunSecretWasActive = true;
                        }
                    }
                }
            }
            if (ExpandStats.EnableTestDungeonFlow) {
                GameManager.Instance.InjectedFlowPath = ExpandStats.TestFlow;
                GameManager.Instance.InjectedLevelName = ExpandStats.TestFloor;
            }
            yield break;
        }
        
        private void SetupItemAPI(AssetBundle expandSharedAssets1) {
            if (!ItemAPISetup) {
                try {
                    Tools.Init();
                    ItemBuilder.Init();
                    BabyGoodHammer.Init(expandSharedAssets1);
                    CorruptionBomb.Init(expandSharedAssets1);
                    if (ExpandStats.EnableBloodiedScarfFix) { ExpandRedScarf.Init(expandSharedAssets1); }
                    TableTechAssassin.Init(expandSharedAssets1);
                    CorruptedJunk.Init(expandSharedAssets1);
                    BootlegGuns.Init();
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

                    WestBrosRevolverGenerator.Init();

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
            ExpandCustomDungeonPrefabs.ReInitFloorDefinitions(self);
            if ((m_ShotGunSecretWasActive && ExpandStats.ShotgunKinSecret) | ExpandStats.EnableTestDungeonFlow) {
                GameManager.Instance.StartCoroutine(WaitForFoyerLoad());
            }
        }

        private void MainMenuUpdateHook(Action<MainMenuFoyerController> orig, MainMenuFoyerController self) {
            orig(self);
            if (((dfTextureSprite)self.TitleCard).Texture.name != ModLogo.name) {
                ((dfTextureSprite)self.TitleCard).Texture = ModLogo;
                LogoEnabled = true;
            }
        }

        private void InitConsoleCommands(string MainCommandName) {
            ETGModConsole.Commands.AddGroup(MainCommandName, ExpandConsoleInfo);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("dump_layout", ExpandDumpLayout);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("debug", ExpandDebug);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("list_items", ExpandCustomItemsInfo);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("youtubemode", ExpandYouTubeSafeCommand);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("savesettings", ExpandExportSettings);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("togglelanguagefix", ExpandToggleLanguageFix);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("test", ExpandTestCommand);
            return;
        }

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
            string validSubCommands = "toggledebugstats\nclearroom\nunsealroom\nfixplayerinput";
            
            if (!m_IsCommandValid(consoleText, validSubCommands, "debug")) { return; }

            RoomHandler currentRoom = GameManager.Instance.PrimaryPlayer.CurrentRoom;

            switch (consoleText[0].ToLower()) {
                case "toggledebugstats":
                    if (!ExpandStats.debugMode) {
                        ExpandStats.debugMode = true;
                        ETGModConsole.Log("[ExpandTheGungeon] Installing RoomHandler.OnEntered Hook....");
                        ExpandSharedHooks.enterRoomHook = new Hook(
                            typeof(RoomHandler).GetMethod("OnEntered", BindingFlags.NonPublic | BindingFlags.Instance),
                            typeof(ExpandSharedHooks).GetMethod("EnteredNewRoomHook", BindingFlags.NonPublic | BindingFlags.Instance),
                            typeof(RoomHandler)
                        );
                    } else {
                        if (ExpandStats.debugMode) {
                            ExpandStats.debugMode = false;
                            if (ExpandSharedHooks.enterRoomHook != null) {
                                ETGModConsole.Log("[ExpandTheGungeon] Uninstalling RoomHandler.OnEntered Hook....");
                                ExpandSharedHooks.enterRoomHook.Dispose();
                                ExpandSharedHooks.enterRoomHook = null;
                            }
                        }
                    }                
                    ETGModConsole.Log("[ExpandTheGungeon] Debug Stats: ", false);
                    ETGModConsole.Log("Debug Stats: " + ExpandStats.debugMode.ToString(), false);
                    ETGModConsole.Log("Debug Exceptions: " + ExpandStats.debugMode.ToString(), false);
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
            if (ExpandStats.youtubeSafeMode) {
                ETGModConsole.Log("No longer YouTube safe.", false);
                ExpandStats.youtubeSafeMode = false;
            } else {
                ETGModConsole.Log("Now YouTube Safe.", false);
                ExpandStats.youtubeSafeMode = true;
            }
        }
        
        private void ExpandExportSettings(string[] consoleText) {

            string CachedJSONText = string.Empty;

            ExpandCachedStats cachedStats = ScriptableObject.CreateInstance<ExpandCachedStats>();
            
            CachedJSONText = JsonUtility.ToJson(cachedStats);

            if (File.Exists(Path.Combine(ETGMod.ResourcesDirectory, ModSettingsFileName))) {
                File.Delete(Path.Combine(ETGMod.ResourcesDirectory, ModSettingsFileName));
            }
            
            ExpandAssets.SaveStringToFile(CachedJSONText, ETGMod.ResourcesDirectory, ModSettingsFileName);

            ETGModConsole.Log("[ExpandTheGungeon] Settings have been saved!");

            return;
        }

        private void ExpandToggleLanguageFix(string[] consoleText) {
            if (ExpandStats.EnableLanguageFix) {
                ExpandStats.EnableLanguageFix = false;
                GameManager.Options.CurrentLanguage = StringTableManager.GungeonSupportedLanguages.ENGLISH;
                StringTableManager.CurrentLanguage = StringTableManager.GungeonSupportedLanguages.ENGLISH;
                ETGModConsole.Log("[ExpandTheGungeon] Language override disabled!");
                ETGModConsole.Log("[ExpandTheGungeon] Game Language set back to English!\n\nSet game language back to your desired language before re-enabling this feature!");
            } else {
                ExpandStats.EnableLanguageFix = true;
                ETGModConsole.Log("[ExpandTheGungeon] Language override enabled!");
            }

            ExpandStats.GameLanguage = ExpandUtility.LanguageToInt(GameManager.Options.CurrentLanguage);

            ExpandExportSettings(consoleText);
        }
        
        private void ExpandTestCommand(string[] consoleText) {
            
            PlayerController CurrentPlayer = GameManager.Instance.PrimaryPlayer;

            // UnityEngine.Object.Instantiate(ExpandPrefabs.EX_ItemDropper, CurrentPlayer.transform.position, Quaternion.identity);

            ExpandAssets.SaveStringToFile(JsonUtility.ToJson(ExpandCustomEnemyDatabase.BootlegBulletManPrefab.GetComponent<BehaviorSpeculator>().TargetBehaviors), FilePath, "EXBootlegBulletMand_TargetBehaviors.txt");
            

            /*GameObject AlarmMushroom = UnityEngine.Object.Instantiate(ExpandPrefabs.EXAlarmMushroom, CurrentPlayer.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
            AlarmMushroom.GetComponent<ExpandAlarmMushroomPlacable>().ConfigureOnPlacement(CurrentPlayer.CurrentRoom);

            if (consoleText.Length > 1) {
                float X = float.Parse(consoleText[0]);
                float Y = float.Parse(consoleText[1]);
                AlarmMushroom.transform.Find("Shadow").localPosition = new Vector3(X, Y);
            }*/

            // ExpandUtilities.ResourceExtractor.DumpSpriteCollection(ExpandPrefabs.ENV_Tileset_Phobos.GetComponent<tk2dSpriteCollectionData>());            

            /*ExpandComponents.ExpandFakeChest SupriseChest = UnityEngine.Object.Instantiate(ExpandPrefabs.SurpriseChestObject, CurrentPlayer.transform.position + new Vector3(0, 2), Quaternion.identity).GetComponent<ExpandComponents.ExpandFakeChest>();
            SupriseChest.ConfigureOnPlacement(CurrentPlayer.CurrentRoom);
            CurrentPlayer.CurrentRoom.RegisterInteractable(SupriseChest);*/

            // ExpandUtility.SpawnParaDrop(CurrentPlayer.CurrentRoom, CurrentPlayer.specRigidbody.GetUnitCenter(ColliderType.HitBox), DropHorizontalOffset: 0);

            // GameObject AlarmMushRoomTest = UnityEngine.Object.Instantiate(ExpandPrefabs.EXAlarmMushroom, CurrentPlayer.transform.position + new Vector3(0, 2), Quaternion.identity);

            // AlarmMushRoomTest.GetComponent<ExpandAlarmMushroomPlacable>().ConfigureOnPlacement(CurrentPlayer.CurrentRoom);

            /*GameObject TestPortal = UnityEngine.Object.Instantiate(ExpandPrefabs.EX_RainbowRoomFloor, CurrentPlayer.transform.position, Quaternion.identity);

            if (consoleText.Length > 1) {
                float X = float.Parse(consoleText[0]);
                float Y = float.Parse(consoleText[1]);
                TestPortal.transform.localScale = new Vector3(X, Y);
                // ExpandPrefabs.EX_Chest_West.gameObject.transform.Find("Shadow").localPosition = new Vector3(X, Y);
            }

            TestPortal.transform.position -= new Vector3(0, 0, -50);*/


            // Chest TestWestChest = Chest.Spawn(ExpandPrefabs.EX_Chest_West.GetComponent<Chest>(), CurrentPlayer.transform.PositionVector2().ToIntVector2() + new IntVector2(0, 2), CurrentPlayer.CurrentRoom);
            // TestWestChest.spawnCurve.keys[1].value = float.Parse(consoleText[0]);


            // GameObject EXGlitchFloorScreenFX = UnityEngine.Object.Instantiate(ExpandPrefabs.EXGlitchFloorScreenFX);
            // EXGlitchFloorScreenFX.transform.SetParent(GameManager.Instance.Dungeon.gameObject.transform);
            // ExpandGlitchedEnemies m_GlitchedEnemies = new ExpandGlitchedEnemies();
            // GameObject TestEnemy = m_GlitchedEnemies.SpawnRandomGlitchEnemy(CurrentPlayer.CurrentRoom, new IntVector2(2, 2), true);

            /*BlinkPassiveItem m_BlinkPassive = PickupObjectDatabase.GetById(436).GetComponent<BlinkPassiveItem>();

            AIActor[] AllEnemies = UnityEngine.Object.FindObjectsOfType<AIActor>();
            ScarfAttachmentDoer m_Scarf = m_BlinkPassive.ScarfPrefab;

            if (AllEnemies != null && AllEnemies.Length > 0) {
                foreach (AIActor enemy in AllEnemies) {                    

                    GameObject m_ExpandScarfObject = new GameObject("VFX_EXScarf");
                    ExpandComponents.ExpandScarfComponent m_ExpandScarf = m_ExpandScarfObject.AddComponent<ExpandComponents.ExpandScarfComponent>();

                    m_ExpandScarf.StartWidth = m_Scarf.StartWidth;
                    m_ExpandScarf.EndWidth = m_Scarf.EndWidth;
                    m_ExpandScarf.AnimationSpeed = m_Scarf.AnimationSpeed;
                    m_ExpandScarf.ScarfLength = m_Scarf.ScarfLength;
                    m_ExpandScarf.AngleLerpSpeed = m_Scarf.AngleLerpSpeed;
                    m_ExpandScarf.BackwardZOffset = m_Scarf.BackwardZOffset;
                    m_ExpandScarf.CatchUpScale = m_Scarf.CatchUpScale;
                    m_ExpandScarf.SinSpeed = m_Scarf.SinSpeed;
                    m_ExpandScarf.AmplitudeMod = m_Scarf.AmplitudeMod;
                    m_ExpandScarf.WavelengthMod = m_Scarf.WavelengthMod;
                    m_ExpandScarf.ScarfMaterial = Material.Instantiate(m_Scarf.ScarfMaterial);
                    m_ExpandScarf.ScarfMaterial.SetColor("_OverrideColor", new Color(0.1f, 0.7f, 0.1f));

                    m_ExpandScarf.Initialize(enemy);
                }
            }*/

            // UnityEngine.Object.Instantiate(ExpandPrefabs.BlankRewardPedestal, (CurrentPlayer.transform.position + new Vector3(-2, 2)), Quaternion.identity);
            /* 
            // UnityEngine.Object.Instantiate(ExpandPrefabs.RatKeyRewardPedestal, (CurrentPlayer.transform.position + new Vector3(2, 2)), Quaternion.identity);
            Dungeon m_dungeonPrefab = DungeonDatabase.GetOrLoadByName("finalscenario_guide");
            DungeonFlow dungeonFlowPrefab = m_dungeonPrefab.PatternSettings.flows[0];
            PrototypeDungeonRoom GuidePastRoom = dungeonFlowPrefab.AllNodes[0].overrideExactRoom;
            GameObject GuidePastRoomObject = GuidePastRoom.placedObjects[0].nonenemyBehaviour.gameObject;
            GameObject m_RainPrefab = GuidePastRoomObject.transform.Find("Rain").gameObject;
            
            AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle(ExpandTheGungeon.ModAssetBundleName);
            AssetBundle SharedAssets1 = ResourceManager.LoadAssetBundle("shared_auto_001");

            GameObject TestObject = expandSharedAssets1.LoadAsset<GameObject>("ExpandDustStormFX");

            ParticleSystemRenderer testSystem = TestObject.GetComponent<ParticleSystemRenderer>();
            // testSystem.materials[0].shader = m_RainPrefab.GetComponent<ThunderstormController>().RainSystemTransform.gameObject.GetComponent<ParticleSystemRenderer>().materials[0].shader;
            // testSystem.materials[0].shader = SharedAssets1.LoadAsset<Shader>("BlendVertexColorTilted");
            testSystem.materials[0].shader = SharedAssets1.LoadAsset<Shader>("BraveParticlesAdditive");
            testSystem.materials[0].mainTexture = expandSharedAssets1.LoadAsset<Texture2D>("ExpandDustFXSprite_001");
            testSystem.allowOcclusionWhenDynamic = true;

            GameObject NewTestObject = UnityEngine.Object.Instantiate(TestObject, CurrentPlayer.transform.position.GetAbsoluteRoom().area.basePosition.ToVector3(), Quaternion.identity);*/



            /*RoomHandler SelectedRoom = null;
            foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms) {
                if (!string.IsNullOrEmpty(room.GetRoomName()) && room.GetRoomName().StartsWith(ExpandRoomPrefabs.Expand_Gungeon_BellyEntranceRoom.name)) {
                    SelectedRoom = room;
                    break;
                }
            }


            if (SelectedRoom != null) {
                IntVector2 targetPoint = SelectedRoom.area.basePosition + new IntVector2(8, 6);
                CurrentPlayer.WarpToPoint(targetPoint.ToVector2(), false, false);
            } else {
                GameManager.Instance.LoadNextLevel();
            }*/

            // LootEngine.SpawnItem(CorruptedJunk.CorruptedJunkObject, (CurrentPlayer.transform.position + Vector3.one), Vector2.zero, 0, doDefaultItemPoof: true);
            // Rooms for floor 4.
            // GameManager.Instance.StartCoroutine(ExpandUtility.DelayedGlitchLevelLoad(1, "SecretGlitchFloor_Flow", true));

            // 
            return;
        }
    }
}

