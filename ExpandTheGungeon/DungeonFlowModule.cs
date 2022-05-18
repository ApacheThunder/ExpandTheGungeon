using System;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandDungeonFlows;
using Dungeonator;

namespace ExpandTheGungeon {

	// A slightly rewritten version of old Anywhere Mod by stellatedHexahedron
	public class DungeonFlowModule {

        private static List<string> knownFlows = new List<string>();	
		private static List<string> knownTilesets = new List<string>();
        private static List<string> knownScenes = new List<string>();		
        
        private static string[] ReturnMatchesFromList(string matchThis, List<string> inThis) {
            List<string> result = new List<string>();
            string matchString = matchThis.ToLower();
            foreach (string text in inThis) {
                string textString = text.ToLower();
                bool flag = StringAutocompletionExtensions.AutocompletionMatch(textString, matchString);
                if (flag) { result.Add(textString); }
            }
            return result.ToArray();
		}
		
		public static void Install() {

            if (ExpandDungeonFlow.KnownFlows != null && ExpandDungeonFlow.KnownFlows.Count > 0) {
                foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                    if (flow.name != null && flow.name != string.Empty) { knownFlows.Add(flow.name.ToLower()); }
                }
            }

            foreach (GameLevelDefinition dungeonFloors in GameManager.Instance.dungeonFloors) {
                if (dungeonFloors.dungeonPrefabPath != null && dungeonFloors.dungeonPrefabPath != string.Empty) {
                    knownTilesets.Add(dungeonFloors.dungeonPrefabPath.ToLower());
                }
                if (dungeonFloors.dungeonSceneName != null && dungeonFloors.dungeonSceneName != string.Empty) {
                    knownScenes.Add(dungeonFloors.dungeonSceneName.ToLower());
                }
            }
            
            foreach (GameLevelDefinition customFloors in GameManager.Instance.customFloors) {
                if (customFloors.dungeonPrefabPath != null && customFloors.dungeonPrefabPath != string.Empty) {
                    knownTilesets.Add(customFloors.dungeonPrefabPath.ToLower());
                }
                if (customFloors.dungeonSceneName != null && customFloors.dungeonSceneName != string.Empty) {
                    knownScenes.Add(customFloors.dungeonSceneName.ToLower());
                }                
            }
            
            ETGModConsole.Commands.AddUnit("load_flow", new Action<string[]>(LoadFlowFunction), new AutocompletionSettings(delegate(int index, string input) {
				if (index == 0) {
                    return ReturnMatchesFromList(input.ToLower(), knownFlows);
                } else if (index == 1) {
                    return ReturnMatchesFromList(input.ToLower(), knownTilesets);
                } else if (index == 2) {
                    return ReturnMatchesFromList(input.ToLower(), knownScenes);
                } else {
                    return new string[0];
                }
            }));
        }	
	
		public static void LoadFlowFunction(string[] args) {
			if (args == null | args.Length == 0 | args[0].ToLower() == "help") {
				ETGModConsole.Log("WARNING: this command can crash gungeon! \nIf the game hangs on loading screen, use console to load a different level!\nUsage: load_flow [FLOW NAME] [TILESET NAME]. [TILESET NAME] is optional. Press tab for a list of each.\nOnce you run the command and you press escape, you should see the loading screen. If nothing happens when you use the command, the flow you tried to load doesn't exist or the path to it needs to be manually specified. Example: \"load_flow NPCParadise\".\nIf it hangs on loading screen then the tileset you tried to use doesn't exist, is no longer functional, or the flow uses rooms that are not compatible with the chosen tileset.\nAlso, you should probably know that if you run this command from the breach, the game never gives you the ability to shoot or use active items, so you should probably start a run first.");
			} else if (args != null && args.Length > 3) {
                ETGModConsole.Log("ERROR: Too many arguments specified! DungoenFlow name, dungoen prefab name, and dungoen scene name are the expected arguments!");
            } else {
                bool tilesetSpecified = args.Length > 1;
                bool sceneSpecified = args.Length > 2;

                if (tilesetSpecified && !knownTilesets.Contains(args[1]) && DungeonDatabase.GetOrLoadByName(args[1])) {
                    knownTilesets.Add(args[1]);
                }

                bool invalidTileset = tilesetSpecified && !knownTilesets.Contains(args[1]);
                string flowName = args[0].Replace('-', ' ');

                if (flowName.ToLower().StartsWith("custom_glitchchest_flow") |
                    flowName.ToLower().StartsWith("custom_glitchchestalt_flow") |
                    flowName.ToLower().StartsWith("custom_glitch_flow"))
                {
                    ExpandDungeonFlow.isGlitchFlow = true;
                }
                if (invalidTileset) {
                    ETGModConsole.Log("Not a valid tileset!");
                } else {
                    try {
                        string LogMessage = ("Attempting to load Dungeon Flow \"" + args[0] + "\"");

                        if (tilesetSpecified) { LogMessage += (" with tileset \"" + args[1] + "\""); }
                        if (sceneSpecified) { LogMessage += (" and scene \"" + args[2] + "\""); }
                        LogMessage += ".";
                        // ETGModConsole.Log("Attempting to load Dungeon Flow \"" + args[0] + (tilesetSpecified ? ("\" with tileset \"" + args[1]) : string.Empty) + "\"" + (sceneSpecified ? ("\" and scene " + args[2]) : string.Empty) + ".");
                        ETGModConsole.Log(LogMessage);
                        if (args.Length == 1) {
                            GameManager.Instance.LoadCustomFlowForDebug(flowName);
                        } else if (args.Length == 2) {
                            string tilesetName = args[1];
                            GameManager.Instance.LoadCustomFlowForDebug(flowName, tilesetName);
                        } else if (args.Length == 3) {
                            string tilesetName = args[1];
                            string sceneName = args[2];
                            GameManager.Instance.LoadCustomFlowForDebug(flowName, tilesetName, sceneName);
                        } else {
                            ETGModConsole.Log("If you're trying to go nowhere, you're succeeding.");
                        }
                    } catch (Exception ex) {
                        ETGModConsole.Log("WHOOPS! Something went wrong! Most likely you tried to load a broken flow, or the tileset is incomplete and doesn't have the right tiles for the flow.");
                        ETGModConsole.Log("In order to get the game back into working order, the mod is now loading NPCParadise, with the castle tileset.");
                        Debug.Log("WHOOPS! Something went wrong! Most likely you tried to load a broken flow, or the tileset is incomplete and doesn't have the right tiles for the flow.");
                        Debug.Log("In order to get the game back into working order, the mod is now loading NPCParadise, with the castle tileset.");
                        Debug.LogException(ex);
                        ExpandDungeonFlow.isGlitchFlow = false;
                        GameManager.Instance.LoadCustomFlowForDebug("npcparadise", "Base_Castle", "tt_castle");
                    }
                }
            }
        }	
    }
}

	