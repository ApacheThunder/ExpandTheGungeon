﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Dungeonator;
using MonoMod.RuntimeDetour;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandAudio;
using ExpandTheGungeon.ExpandDungeonFlows;
using ExpandTheGungeon.SerializedData;

namespace ExpandTheGungeon {

    public class ExpandTheGungeon : ETGModule {

        public static string ConsoleCommandName;
        
        public static Texture2D ModLogo;
        public static Hook GameManagerHook;
        public static Hook MainMenuFoyerUpdateHook;

        // public static bool isGlitchFloor = false;
        public static bool ItemAPISetup = false;
        public static bool LogoEnabled = false;

        public static string ExceptionText;
        
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

        public override void Init() {

            ExceptionText = string.Empty;

            ConsoleCommandName = "expand";

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
                "Pow Block"
                // "Table Tech Expand"
            };
            
            AudioResourceLoader.InitAudio();

            InitCustomAssetBundle();

            ModLogo = ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<Texture2D>("EXLogo");
            
            try {
                MainMenuFoyerUpdateHook = new Hook(
                    typeof(MainMenuFoyerController).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance),
                    typeof(ExpandTheGungeon).GetMethod("MainMenuUpdateHook", BindingFlags.NonPublic | BindingFlags.Instance),
                    typeof(MainMenuFoyerController)
                );
                GameManager.Instance.OnNewLevelFullyLoaded += ExpandObjectMods.Instance.InitSpecialMods;
            } catch (Exception ex) {
                // ETGModConsole can't be called by anything that occurs in Init(), so write message to static strinng and check it later.
                ExceptionText = "[ExpandTheGungeon] ERROR: Exception occured while installing hooks!";
                // ETGModConsole.Log("[ExpandTheGungeon] ERROR: Exception occured while installing hooks!", true);
                Debug.LogException(ex);
                return;
            }
        }

        public override void Start() {

            if (!string.IsNullOrEmpty(ExceptionText)) {
                ETGModConsole.Log(ExceptionText);
                return;
            }
            
            ExpandSharedHooks.InstallRequiredHooks();
            
            // Init ItemAPI
            SetupItemAPI();
            try {
                // Init Prefab Databases
                ExpandPrefabs.InitCustomPrefabs();
                // Init Custom Enemy Prefabs
                ExpandCustomEnemyDatabase.InitPrefabs();
                // Init Custom Room Prefabs
                ExpandRoomPrefabs.InitCustomRooms();
                // Init Custom DungeonFlow(s)
                ExpandDungeonFlow.InitDungeonFlows();
                // Init Custom Dungeons Prefabs
                ExpandCustomDungeonPrefabs.InitCustomDungeons();
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
                ExpandPrefabs.sharedAssets = null;
                ExpandPrefabs.sharedAssets2 = null;
                ExpandPrefabs.braveResources = null;
                ExpandPrefabs.enemiesBase = null;
                return;
            }
            // Modified version of Anywhere mod
            DungeonFlowModule.Install();

            InitConsoleCommands(ConsoleCommandName);
            
            // Null bundles when done with them to avoid game crash issues.
            ExpandPrefabs.sharedAssets = null;
            ExpandPrefabs.sharedAssets2 = null;
            ExpandPrefabs.braveResources = null;
            ExpandPrefabs.enemiesBase = null;
        }
        public override void Exit() {
            if (GameManagerHook != null) {
                ETGModConsole.Log("[ExpandTheGungeon] Uninstalling GameManager.Awake hook", true);
                GameManagerHook.Dispose();
                GameManagerHook = null;
                GameManager.Instance.OnNewLevelFullyLoaded -= ExpandObjectMods.Instance.InitSpecialMods;
            }
        }

        
        public static void InitCustomAssetBundle() {
            
            FieldInfo m_AssetBundlesField = typeof(ResourceManager).GetField("LoadedBundles", BindingFlags.Static | BindingFlags.NonPublic);
            Dictionary<string, AssetBundle> m_AssetBundles = (Dictionary<string, AssetBundle>)m_AssetBundlesField.GetValue(typeof(ResourceManager));

            AssetBundle m_ExpandSharedAssets1 = null;

            try {
                m_ExpandSharedAssets1 = ExpandAssets.LoadFromModZIPOrModFolder();
                if (m_ExpandSharedAssets1 != null) {
                    m_AssetBundles.Add("ExpandSharedAuto", m_ExpandSharedAssets1);
                } else {
                    string ErrorMessage = "[ExpandTheGungeon] ERROR: ExpandSharedAuto asset bundle not found!";
                    Debug.Log(ErrorMessage);
                    ExceptionText = ErrorMessage;
                    return;
                }
            } catch (Exception ex) {
                string ErrorMessage = "[ExpandTheGungeon] ERROR: Exception while loading ExpandSharedAuto asset bundle! Possible GUID conflict with other custom AssetBundles?";
                Debug.Log(ErrorMessage);
                Debug.LogException(ex);
                ExceptionText = ErrorMessage;
                return;
            }
        }

        private void SetupItemAPI() {
            if (!ItemAPISetup) {
                try {
                    AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle("ExpandSharedAuto");

                    Tools.Init();
                    ItemBuilder.Init();
                    BabyGoodHammer.Init(expandSharedAssets1);
                    CorruptionBomb.Init(expandSharedAssets1);
                    ExpandRedScarf.Init(expandSharedAssets1);
                    TableTechAssassin.Init(expandSharedAssets1);
                    CorruptedJunk.Init(expandSharedAssets1);
                    BootlegGuns.Init();
                    CronenbergBullets.Init(expandSharedAssets1);
                    Mimiclay.Init(expandSharedAssets1);
                    TheLeadKey.Init(expandSharedAssets1);
                    // TableTechExpand.Init();
                    RockSlide.Init(expandSharedAssets1);
                    CustomMasterRounds.Init(expandSharedAssets1);
                    WoodenCrest.Init(expandSharedAssets1);
                    BulletKinGun.Init();
                    BabySitter.Init(expandSharedAssets1);
                    PowBlock.Init(expandSharedAssets1);

                    // Setup Custom Synergies. Do this after all custom items have been Init!;
                    ExpandSynergies.Init();

                    expandSharedAssets1 = null;

                    ItemAPISetup = true;
                } catch (Exception e2) {
                    Tools.PrintException(e2, "FF0000");
                }
            }
        }

        private void GameManager_Awake(Action<GameManager> orig, GameManager self) {
            orig(self);
            self.OnNewLevelFullyLoaded += ExpandObjectMods.Instance.InitSpecialMods;
            ExpandCustomDungeonPrefabs.ReInitFloorDefinitions();
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
            string validSubCommands = "toggledebugstats\nclearroom\nunsealroom";
            
            if (!m_IsCommandValid(consoleText, validSubCommands, "debug")) { return; }

            if (consoleText[0] == "toggledebugstats") {
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
                            ExpandSharedHooks.enterRoomHook.Dispose(); ExpandSharedHooks.enterRoomHook = null;
                        }
                    }
                }                
                ETGModConsole.Log("[ExpandTheGungeon] Debug Stats: ", false);
                ETGModConsole.Log("Debug Stats: " + ExpandStats.debugMode.ToString(), false);
                ETGModConsole.Log("Debug Exceptions: " + ExpandStats.debugMode.ToString(), false);
            } else if (consoleText[0] == "clearroom") {
                RoomHandler currentRoom = GameManager.Instance.PrimaryPlayer.CurrentRoom;
                if (currentRoom != null) {
                    List<AIActor> enemies = currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear);

                    if (enemies != null && enemies.Count > 0) {
                        for (int i = 0; i < enemies.Count; i++) {
                            currentRoom.DeregisterEnemy(enemies[i]);
                            UnityEngine.Object.Destroy(enemies[i].gameObject);
                        }
                    }
                }
            } else if (consoleText[0] == "unsealroom") {
                RoomHandler currentRoom = GameManager.Instance.PrimaryPlayer.CurrentRoom;
                if (currentRoom != null) {
                    if (currentRoom.IsSealed) { currentRoom.UnsealRoom(); }
                }
            } else if (consoleText[0] == "shotgun") {
                PlayerController CurrentPlayer = GameManager.Instance.PrimaryPlayer;

                if (CurrentPlayer.characterIdentity != PlayableCharacters.Bullet) {
                    ETGModConsole.Log("[ERROR] This isn't compatible with the currently active player!");
                    return;
                }

                if (!ExpandCustomSpriteCollections.ShotgunReskinObject) { ExpandCustomSpriteCollections.InitShotgunKinCollection(); }

                if (!ExpandCustomSpriteCollections.ShotgunReskinObject) {
                    ETGModConsole.Log("Shotgun Man Data not found! Not available for public use!");
                    return;
                }
                CurrentPlayer.OverrideAnimationLibrary = ExpandCustomSpriteCollections.ShotgunReskinObject.GetComponent<tk2dSpriteAnimation>();
                CurrentPlayer.SetOverrideShader(ShaderCache.Acquire(CurrentPlayer.LocalShaderName));
            }  else {
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
        
        private void ExpandTestCommand(string[] consoleText) {
            PlayerController CurrentPlayer = GameManager.Instance.PrimaryPlayer;

            // UnityEngine.Object.Instantiate(ExpandPrefabs.BlankRewardPedestal, (CurrentPlayer.transform.position + new Vector3(-2, 2)), Quaternion.identity);
            /*// UnityEngine.Object.Instantiate(ExpandPrefabs.RatKeyRewardPedestal, (CurrentPlayer.transform.position + new Vector3(2, 2)), Quaternion.identity);
            Dungeon m_dungeonPrefab = DungeonDatabase.GetOrLoadByName("finalscenario_guide");
            DungeonFlow dungeonFlowPrefab = m_dungeonPrefab.PatternSettings.flows[0];
            PrototypeDungeonRoom GuidePastRoom = dungeonFlowPrefab.AllNodes[0].overrideExactRoom;
            GameObject GuidePastRoomObject = GuidePastRoom.placedObjects[0].nonenemyBehaviour.gameObject;
            GameObject m_RainPrefab = GuidePastRoomObject.transform.Find("Rain").gameObject;
            
            AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle("ExpandSharedAuto");
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

