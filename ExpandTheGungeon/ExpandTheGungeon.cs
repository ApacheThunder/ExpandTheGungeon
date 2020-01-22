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

namespace ExpandTheGungeon {

    public class ExpandTheGungeon : ETGModule {

        public static string ConsoleCommandName;

        public static Hook GameManagerHook;
        
        public static bool isGlitchFloor = false;
        public static bool ItemAPISetup = false;

        // public static GameObject TestObject;

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
            ConsoleCommandName = "expand";

            itemList = new List<string>() {
                "Baby Good Hammer",
                "Corruption Bomb",
                "Table Tech Assassin",
                "ex:bloodied_scarf"
            };

            AudioResourceLoader.InitAudio();

            try {
                ExpandSharedHooks.InstallRequiredHooks();
                GameManager.Instance.OnNewLevelFullyLoaded += ExpandObjectMods.Instance.InitSpecialMods;
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: Exception occured while installing hooks!", true);
                Debug.LogException(ex);
                return;
            }
        }
        public override void Start() {
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
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: Exception occured while building prefabs!", true);
                Debug.LogException(ex);
                return;
            }
            // Modified version of Anywhere mod
            DungeonFlowModule.Install();

            InitConsoleCommands(ConsoleCommandName);
        }
        public override void Exit() {
            if (GameManagerHook != null) {
                ETGModConsole.Log("[ExpandTheGungeon] Uninstalling GameManager.Awake hook", true);
                GameManagerHook.Dispose();
                GameManagerHook = null;
                GameManager.Instance.OnNewLevelFullyLoaded -= ExpandObjectMods.Instance.InitSpecialMods;
            }
        }

        private void SetupItemAPI() {
            if (!ItemAPISetup) {
                try {
                    Tools.Init();
                    ItemBuilder.Init();
                    BabyGoodHammer.Init();
                    CorruptionBomb.Init();
                    ExpandRedScarf.Init();
                    TableTechAssassin.Init();
                    CorruptedJunk.Init();
                    ItemAPISetup = true;
                } catch (Exception e2) {
                    Tools.PrintException(e2, "FF0000");
                }
            }
        }

        private void GameManager_Awake(Action<GameManager> orig, GameManager self) {
            orig(self);
            self.OnNewLevelFullyLoaded += ExpandObjectMods.Instance.InitSpecialMods;
        }

        private void InitConsoleCommands(string MainCommandName) {
            ETGModConsole.Commands.AddGroup(MainCommandName, ExpandConsoleInfo);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("dump_layout", ExpandDumpLayout);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("debug", ExpandDebug);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("list_items", ExpandCustomItemsInfo);
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
            string validSubCommands = "toggledebugstats\nclearroom";
            
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
                    System.Collections.Generic.List<AIActor> enemies = currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear);

                    if (enemies != null && enemies.Count > 0) {
                        for (int i = 0; i < enemies.Count; i++) {
                            currentRoom.DeregisterEnemy(enemies[i]);
                            UnityEngine.Object.Destroy(enemies[i].gameObject);
                        }
                    }
                }
            } else {
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

        private void ExpandTestCommand(string[] consoleText) {
            // GameObject soundObject = new GameObject("SoundSource");
            // soundObject.transform.position = GameManager.Instance.BestActivePlayer.transform.position;
            // AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", soundObject);
            // AkSoundEngine.PostEvent("Play_VO_bombshee_death_01", soundObject);
            // ETGModConsole.Log(TestObject.name);
            PlayerController CurrentPlayer = GameManager.Instance.PrimaryPlayer;
            /*Dungeon dungeon = GameManager.Instance.Dungeon;

            if (dungeon && CurrentPlayer) {
                RoomHandler DestinationRoom = null;
                foreach (RoomHandler room in dungeon.data.rooms) {
                    if (!string.IsNullOrEmpty(room.GetRoomName())) {
                        if (room.GetRoomName().StartsWith(ExpandRoomPrefabs.SecretRatEntranceRoom.name)) {
                            DestinationRoom = room;
                            break;
                        }
                    }
                }
                if (DestinationRoom != null) {
                    CurrentPlayer.WarpToPoint((new Vector2(8, 8) + DestinationRoom.area.basePosition.ToVector2()), doFollowers: true);
                }
            }*/
            /*GameObject m_CachedAIActorObject = new GameObject("TestEnemy") { layer = 28 };                
            m_CachedAIActorObject.transform.position = (CurrentPlayer.transform.position + new Vector3(0, 2));
            m_CachedAIActorObject.transform.parent = CurrentPlayer.GetAbsoluteParentRoom().hierarchyParent;
            tk2dSprite newSprite = m_CachedAIActorObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(newSprite, EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").gameObject.GetComponent<tk2dSprite>());*/

            // AIActor newEnemy = ExpandUtility.GenerateAIActorTemplate(m_CachedAIActorObject, "Test Enemy", Guid.NewGuid().ToString());

            /*ExpandGlitchedEnemies glitchedEnemyDatabase = new ExpandGlitchedEnemies();
            glitchedEnemyDatabase.SpawnGlitchedPlayerAsEnemy(CurrentPlayer.GetAbsoluteParentRoom(), new IntVector2(3, 3), true);
            glitchedEnemyDatabase = null;*/

            // LootEngine.SpawnItem(CorruptedJunk.CorruptedJunkObject, (CurrentPlayer.transform.position + Vector3.one), Vector2.zero, 0, doDefaultItemPoof: true);
            // Rooms for floor 4.

            AIActor bootlegbulletman = AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(ExpandCustomEnemyDatabase.BootlegBulletManGUID), (CurrentPlayer.transform.PositionVector2().ToIntVector2() - new IntVector2(2, 0)), CurrentPlayer.GetAbsoluteParentRoom(), true, AIActor.AwakenAnimationType.Spawn, true);
            bootlegbulletman.HandleReinforcementFallIntoRoom();

            return;
        }        
    }
}

