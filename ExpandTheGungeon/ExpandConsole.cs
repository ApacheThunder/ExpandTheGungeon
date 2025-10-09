using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Dungeonator;
using MonoMod.RuntimeDetour;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandMain;

namespace ExpandTheGungeon {

    public class ExpandConsole {

        public const string ConsoleCommandName = "expand";

        public static List<string> itemList;

        public static List<string> debugCommands = new List<string>() { "debugcamera", "stats", "clearroom", "unsealroom", "fixplayerinput" };
        
                                
        public static void InitConsoleCommands(string MainCommandName) {
            ETGModConsole.Commands.AddGroup(MainCommandName, ExpandConsoleInfo);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("createSpriteCollection", ExpandSerializeCollection);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("dump_layout", ExpandDumpLayout);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("debug", ExpandDebug);
            ETGModConsole.Commands.GetGroup(MainCommandName).AddUnit("debug", ExpandDebug, new AutocompletionSettings(delegate (int index, string input) {
                switch (index) {
                    case 0:
                        return DungeonFlowModule.ReturnMatchesFromList(input.ToLower(), debugCommands);
                    default:
                        return new string[0];
                }
            }));


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
            // Ooze_Tank
            GameObject oozeTank = DungeonPlaceableUtility.InstantiateDungeonPlaceable(ExpandPrefabs.Ooze_Tank, GameManager.Instance.PrimaryPlayer.CurrentRoom, (GameManager.Instance.PrimaryPlayer.CenterPosition.ToIntVector2() - GameManager.Instance.PrimaryPlayer.CurrentRoom.area.basePosition), false);
            GameManager.Instance.PrimaryPlayer.CurrentRoom.RegisterInteractable(oozeTank.GetComponent<KickableObject>());

            // GameObject ForgeHammer = DungeonPlaceableUtility.InstantiateDungeonPlaceable(ItemAPI.MrCap.MrCapHammer, GameManager.Instance.PrimaryPlayer.CurrentRoom, (GameManager.Instance.PrimaryPlayer.CenterPosition.ToIntVector2() - GameManager.Instance.PrimaryPlayer.CurrentRoom.area.basePosition), true);
            // GameObject ForgeHammer = UnityEngine.Object.Instantiate(ItemAPI.MrCap.MrCapHammer, GameManager.Instance.PrimaryPlayer.CenterPosition, Quaternion.identity);
            // GameObject ForgeHammer = DungeonPlaceableUtility.InstantiateDungeonPlaceable(ExpandObjectDatabase.ForgeHammer, GameManager.Instance.PrimaryPlayer.CurrentRoom, (GameManager.Instance.PrimaryPlayer.CenterPosition.ToIntVector2() - GameManager.Instance.PrimaryPlayer.CurrentRoom.area.basePosition), true);
            // ForgeHammerController hammer = ForgeHammer.GetComponent<ForgeHammerController>();
            // hammer = ForgeHammer.GetComponent<ForgeHammerController>();
            // hammer.DeactivateOnEnemiesCleared = false;
            // hammer.TracksPlayer = false;
            // hammer.ConfigureOnPlacement(GameManager.Instance.PrimaryPlayer.CurrentRoom);
            // SpriteSerializer.DumpSpriteCollection(ExpandPrefabs.ElevatorMaintanenceRoomIcon.GetComponent<tk2dSprite>().Collection);
            // SpriteSerializer.DumpSpriteCollection(ExpandObjectDatabase.ChestBrownTwoItems.GetComponent<tk2dSprite>().Collection);
            // SpriteSerializer.DumpSpriteCollection((PickupObjectDatabase.GetById(448) as SpawnObjectPlayerItem).objectToSpawn.transform.Find("Sprite").gameObject.GetComponent<tk2dSprite>().Collection);
            // FieldInfo field = typeof(GameManager).GetField("m_dungeon", BindingFlags.Instance | BindingFlags.NonPublic);
            // field.SetValue(GameManager.Instance, Instantiate(ExpandDungeonPrefabs.Base_Office).GetComponent<Dungeon>());
            SpriteSerializer.DumpSpriteCollection(DungeonDatabase.GetOrLoadByName("Base_Nakatomi").tileIndices.dungeonCollection);
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
            // string validSubCommands = "debugcamera\nstats\nclearroom\nunsealroom\nfixplayerinput";
            string validSubCommands = string.Empty;

            foreach (string command in debugCommands)validSubCommands += "\n" + command;

            if (!m_IsCommandValid(consoleText, validSubCommands, "debug"))return;

            RoomHandler currentRoom = GameManager.Instance.PrimaryPlayer.CurrentRoom;

            switch (consoleText[0].ToLower()) {
                case "debugcamera":
                    if (ExpandDebugCamera.DebugCameraEnabled) {
                        ExpandDebugCamera.DebugCameraEnabled = false;
                        ETGModConsole.Log("[ExpandTheGungeon] Loading screen camera disabled!");
                    } else {
                        ExpandDebugCamera.DebugCameraEnabled = true;
                        ETGModConsole.Log("[ExpandTheGungeon] Loading screen camera enabled!");
                    }
                    break;
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
    }
}

