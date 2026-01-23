using Dungeonator;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandMain {

    [HarmonyPatch]
    public class ExpandDungeonMusicAPI {

        public static string TempCustomBossMusic = string.Empty;
        public static bool EnteredNewCustomFloor = false;
                
        // Event for stopping all custom music. Ensure you defined this in your sound bank and put the string for it here.
        public static readonly string StopAllMusicEventName = "Stop_EX_MUS_All";

        // Dictionary for custom level music available to the mod. string value is the track name, the bool is for if loop events were setup or not.
        public static readonly Dictionary<string, bool> CustomLevelMusic = new Dictionary<string, bool>() {
            ["Play_EX_MUS_Belly_01"] = true,
            ["Play_EX_MUS_Jungle_01"] = true,
            ["Play_EX_MUS_West_01"] = true,
            ["Play_EX_MUS_DeepDungeon_01"] = false
        };

        /*public static readonly List<string> CustomWestFloorMusic = new List<string>() {
            "Play_EX_MUS_West_01",
            "Play_MUS_Office_Theme_01"
        };*/

        // Any custom rooms that use custom music goes here.
        public static readonly List<string> CustomRoomMusic = new List<string>() {
            "Play_EX_MUS_BootlegMusic_01",
            "Play_EX_UnicornMusic_01"
        };
        // All individual stop events for custom room music.
        public static readonly List<string> CustomRoomMusicStopEvents = new List<string>() {
            "Stop_EX_MUS_BootlegMusic_01",
            "Stop_EX_UnicornMusic_01",
            "Stop_EX_MUS_DeepDungeon_01"
        };

        public static readonly List<GlobalDungeonData.ValidTilesets> TilesetsWithCustomShopSecretMusic = new List<GlobalDungeonData.ValidTilesets>() {
            GlobalDungeonData.ValidTilesets.JUNGLEGEON,
            GlobalDungeonData.ValidTilesets.BELLYGEON,
            GlobalDungeonData.ValidTilesets.WESTGEON
        };
        
        // Normal Action delegate doesn't support 5 arguments needed for SwitchToCustomMusic hook.
        // public delegate void Action5X<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        // Needed to get some private fields. Can omit this if you have a ReflectionHelpers class that already has a version of this.
        public static T ReflectGetField<T>(Type classType, string fieldName, object o = null) {
            FieldInfo field = classType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static));
            return (T)field.GetValue(o);
        }
        
        // For setting up specific loop events for the floor's custom music and ending custom music while in certain rooms like shops/secret rooms.
        [HarmonyPatch(typeof(DungeonFloorMusicController), "SwitchToState", typeof(DungeonFloorMusicController.DungeonMusicState))]
        [HarmonyPrefix]
        private static bool SwitchToState(DungeonFloorMusicController __instance, DungeonFloorMusicController.DungeonMusicState targetState) {
            string m_cachedMusicEventCore = ReflectGetField<string>(typeof(DungeonFloorMusicController), "m_cachedMusicEventCore", __instance);
            bool SupportsLoopSections = false;

            float m_changedToArcadeTimer = ReflectGetField<float>(typeof(DungeonFloorMusicController), "m_changedToArcadeTimer", __instance);
            float m_cooldownTimerRemaining = ReflectGetField<float>(typeof(DungeonFloorMusicController), "m_cooldownTimerRemaining", __instance);
            uint m_coreMusicEventID = ReflectGetField<uint>(typeof(DungeonFloorMusicController), "m_coreMusicEventID", __instance);
            // DungeonFloorMusicController.DungeonMusicState m_currentState = ReflectGetField<DungeonFloorMusicController.DungeonMusicState>(typeof(DungeonFloorMusicController), "m_currentState", self);
            // bool m_overrideMusic = ReflectGetField<bool>(typeof(DungeonFloorMusicController), "m_overrideMusic", self);
            
            if (string.IsNullOrEmpty(m_cachedMusicEventCore) | !CustomLevelMusic.TryGetValue(m_cachedMusicEventCore, out SupportsLoopSections)) {
                if (__instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                    AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                }
                return true;
            }

            FieldInfo m_cooldownTimerRemainingField = typeof(DungeonFloorMusicController).GetField("m_cooldownTimerRemaining", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo m_currentStateField = typeof(DungeonFloorMusicController).GetField("m_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (m_changedToArcadeTimer > 0f && targetState == DungeonFloorMusicController.DungeonMusicState.CALM && __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.ARCADE) {
                return false;
            }

            Debug.Log(string.Concat(new object[] { "(EX) Attemping to switch to state: ", targetState.ToString(), " with core ID: ", m_coreMusicEventID }));
            if (__instance.MusicOverridden)return false;
            switch (targetState) {
                /*default:
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                    AkSoundEngine.PostEvent(m_cachedMusicEventCore, self.gameObject);
                    break;*/
                case DungeonFloorMusicController.DungeonMusicState.ACTIVE_SIDE_A:
                    if (SupportsLoopSections) {
                        if (EnteredNewCustomFloor) {
                            AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                            AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_LoopA", __instance.gameObject);
                        } else {
                            EnteredNewCustomFloor = true;
                        }
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_LoopA", self.gameObject);
                    } else if (__instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SHOP |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SECRET |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.ARCADE |
                            __instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)
                        )
                    {
                        AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                        if (__instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                        }
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore, __instance.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.ACTIVE_SIDE_B:
                    if (SupportsLoopSections) {
                        if (EnteredNewCustomFloor) {
                            AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                            AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_LoopB", __instance.gameObject);
                        } else {
                            EnteredNewCustomFloor = true;
                        }
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_LoopB", self.gameObject);
                    } else if (__instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SHOP |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SECRET |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.ARCADE |
                            __instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)
                        )
                    {
                        AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                        if (__instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                        }
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore, __instance.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.ACTIVE_SIDE_C:
                    if (SupportsLoopSections) {
                        if (EnteredNewCustomFloor) {
                            AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                            AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_LoopC", __instance.gameObject);
                        } else {
                            EnteredNewCustomFloor = true;
                        }
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_LoopC", self.gameObject);
                    } else if (__instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SHOP |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SECRET |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.ARCADE |
                            __instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)
                        )
                    {
                        AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                        if (__instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                        }
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore, __instance.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.ACTIVE_SIDE_D:
                    if (SupportsLoopSections) {
                        if (EnteredNewCustomFloor) {
                            AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                            AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_LoopD", __instance.gameObject);
                        } else {
                            EnteredNewCustomFloor = true;
                        }
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_LoopD", self.gameObject);
                    } else if (__instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SHOP |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SECRET |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS |
                            __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.ARCADE |
                            __instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)
                        )
                    {
                        AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                        if (__instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                        }
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore, __instance.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.ARCADE:
                    m_cooldownTimerRemainingField.SetValue(__instance, -1f);
                    // if (SupportsLoopSections) { AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject); }
                    AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                    AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                    AkSoundEngine.PostEvent("Play_MUS_Dungeon_Theme_01", __instance.gameObject);
                    AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Winchester", __instance.gameObject);
                    AkSoundEngine.PostEvent("Play_MUS_Winchester_State_Drone", __instance.gameObject);
                    // if (SupportsLoopSections) { AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Calm", self.gameObject); }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.CALM:
                    m_cooldownTimerRemainingField.SetValue(__instance, -1f);
                    if (__instance.gameObject.GetComponent<GameManager>().Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                        if (__instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SECRET) {
                            Debug.Log("(EX) Skipped switching to state on Old West Floor: " + targetState.ToString());
                            m_currentStateField.SetValue(__instance, targetState);
                            return false;
                        }
                    }
                    if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER && GameStatsManager.Instance.AnyPastBeaten()) {
                        AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                        AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                        AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Winner", __instance.gameObject);
                    } else {
                        AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                        if (SupportsLoopSections) {
                            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                            AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Calm", __instance.gameObject);
                        } else if (__instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SHOP |
                                __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.SECRET |
                                __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS |
                                __instance.CurrentState == DungeonFloorMusicController.DungeonMusicState.ARCADE |
                                __instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)
                            )
                        {
                            if (__instance.CurrentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                                AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                            }
                            AkSoundEngine.PostEvent(m_cachedMusicEventCore, __instance.gameObject);
                        }
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Drone", self.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.FLOOR_INTRO:
                    m_cooldownTimerRemainingField.SetValue(__instance, -1f);
                    AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                    if (SupportsLoopSections) {
                        AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Intro", __instance.gameObject);
                    } else {
                        AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore, __instance.gameObject);
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Intro", self.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.FOYER_ELEVATOR:
                    m_cooldownTimerRemainingField.SetValue(__instance, -1f);
                    AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                    AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                    AkSoundEngine.PostEvent(m_cachedMusicEventCore, __instance.gameObject);
                    // AkSoundEngine.PostEvent("Play_MUS_State_Elevator", self.gameObject);
                    break;
                case DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS:
                    m_cooldownTimerRemainingField.SetValue(__instance, -1f);
                    AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                    AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                    AkSoundEngine.PostEvent("Play_MUS_Dungeon_Theme_01", __instance.gameObject);
                    AkSoundEngine.PostEvent("Play_MUS_State_Sorceress", __instance.gameObject);
                    // AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Sorceress", self.gameObject);
                    break;
                case DungeonFloorMusicController.DungeonMusicState.SECRET:
                    if (__instance.gameObject.GetComponent<GameManager>().Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) return false;
                    m_cooldownTimerRemainingField.SetValue(__instance, -1f);
                    AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                    AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                    if (SupportsLoopSections && TilesetsWithCustomShopSecretMusic.Contains(__instance.gameObject.GetComponent<GameManager>().Dungeon.tileIndices.tilesetId)) {
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Secret", __instance.gameObject);
                    } else {
                        AkSoundEngine.PostEvent("Play_MUS_Dungeon_Theme_01", __instance.gameObject);
                        AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Secret", __instance.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.SHOP:
                    m_cooldownTimerRemainingField.SetValue(__instance, -1f);
                    AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
                    AkSoundEngine.PostEvent("Stop_MUS_All", __instance.gameObject);
                    if (SupportsLoopSections && TilesetsWithCustomShopSecretMusic.Contains(__instance.gameObject.GetComponent<GameManager>().Dungeon.tileIndices.tilesetId)) {
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Shop", __instance.gameObject);
                    } else {
                        AkSoundEngine.PostEvent("Play_MUS_Dungeon_Theme_01", __instance.gameObject);
                        AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Shop", __instance.gameObject);
                    }
                    break;
            }            
            Debug.Log("(EX) Successfully switched to state: " + targetState.ToString());
            m_currentStateField.SetValue(__instance, targetState);
            return false;
        }

        // Ensures custom floor music doesn't overlap and not overlap with other custom room music if player enters one room that has custom room music into another that also has custom room music.
        [HarmonyPatch(typeof(DungeonFloorMusicController), nameof(DungeonFloorMusicController.SwitchToCustomMusic), typeof(string), typeof(GameObject), typeof(bool), typeof(string))]
        [HarmonyPrefix]
        public static bool SwitchToCustomMusic(DungeonFloorMusicController __instance, string customMusicEvent, GameObject source, bool useSwitch, string switchEvent) {
            if (customMusicEvent == "Play_MUS_Dungeon_State_NPC") {
                string m_cachedMusicEventCore = ReflectGetField<string>(typeof(DungeonFloorMusicController), "m_cachedMusicEventCore", __instance);
                bool SupportsLoopSections = false;
                if (CustomLevelMusic.TryGetValue(m_cachedMusicEventCore, out SupportsLoopSections)) return false;
            }
            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
            Debug.Log("(EX) Successfully switched to custom music: " + customMusicEvent);
            return true;
        }

        // This is specific to ExpandTheGungeon's Old West floor currently.
        [HarmonyPatch(typeof(DungeonFloorMusicController), nameof(DungeonFloorMusicController.NotifyEnteredNewRoom), typeof(RoomHandler))]
        [HarmonyPrefix]
        public static bool NotifyEnteredNewRoom(DungeonFloorMusicController __instance, RoomHandler newRoom) {
            /*if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                self.UpdateCoreMusicEvent();
                FieldInfo m_cachedMusicEventCore = typeof(DungeonFloorMusicController).GetField("m_cachedMusicEventCore", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo m_currentState = typeof(DungeonFloorMusicController).GetField("m_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
                if (string.IsNullOrEmpty((string)m_cachedMusicEventCore.GetValue(self)) | !CustomWestFloorMusic.Contains((string)m_cachedMusicEventCore.GetValue(self))) {
                    return true;
                }
                if (newRoom != null && (newRoom.RoomVisualSubtype == 1 || newRoom.RoomVisualSubtype == 2)) {
                    if ((string)m_cachedMusicEventCore.GetValue(self) != CustomWestFloorMusic[0]) {
                        AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                        AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        m_currentState.SetValue(self, DungeonFloorMusicController.DungeonMusicState.FLOOR_INTRO);
                        m_cachedMusicEventCore.SetValue(self, CustomWestFloorMusic[0]);
                        AkSoundEngine.PostEvent(CustomWestFloorMusic[0], self.gameObject);
                    }
                } else if ((string)m_cachedMusicEventCore.GetValue(self) != CustomWestFloorMusic[1]) {
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                    m_currentState.SetValue(self, DungeonFloorMusicController.DungeonMusicState.FLOOR_INTRO);
                    m_cachedMusicEventCore.SetValue(self, CustomWestFloorMusic[1]);
                    AkSoundEngine.PostEvent(CustomWestFloorMusic[1], self.gameObject);
                }
            }*/
            try { 
                if (!EnteredNewCustomFloor && newRoom.area != null && newRoom.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.ENTRANCE) {
                    if (newRoom?.parentRoom?.area != null && newRoom.parentRoom.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.ENTRANCE) {
                        EnteredNewCustomFloor = true;
                    }
                } /*else {
                    string m_cachedMusicEventCore = ReflectGetField<string>(typeof(DungeonFloorMusicController), "m_cachedMusicEventCore", self);
                    if (!string.IsNullOrEmpty(m_cachedMusicEventCore) && m_cachedMusicEventCore == "Play_EX_MUS_Belly_01") {
                        EnteredNewCustomFloor = true;
                    }
                }*/
            } catch (Exception) { };
            return true;
        }

        // These Hooks ensure custom floor music ends when boss/endtimes musics starts.
        [HarmonyPatch(typeof(DungeonFloorMusicController), nameof(DungeonFloorMusicController.SwitchToEndTimesMusic))]
        [HarmonyPrefix]
        public static bool SwitchToEndTimesMusic(DungeonFloorMusicController __instance) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
            return true;
        }

        [HarmonyPatch(typeof(DungeonFloorMusicController), nameof(DungeonFloorMusicController.SwitchToBossMusic), typeof(string), typeof(GameObject))]
        [HarmonyPrefix]
        public static bool SwitchToBossMusic(DungeonFloorMusicController __instance, string bossMusicString, GameObject source) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
            return true;
        }

        [HarmonyPatch(typeof(DungeonFloorMusicController), nameof(DungeonFloorMusicController.SwitchToDragunTwo))]
        [HarmonyPrefix]
        public static bool SwitchToDragunTwo(DungeonFloorMusicController __instance) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
            return true;
        }
        
        // Ensures any custom boss music is cleared.
        [HarmonyPatch(typeof(DungeonFloorMusicController), nameof(DungeonFloorMusicController.EndBossMusic))]
        [HarmonyPrefix]
        public static bool EndBossMusic(DungeonFloorMusicController __instance) {
            AkSoundEngine.PostEvent("Stop_EX_MUS_All", __instance.gameObject);
            return true;
        }

        // These hooks ensure our custom music/audio gets stopped properly when leaving a floor.
        [HarmonyPatch(typeof(GameManager), nameof(GameManager.FlushMusicAudio))]
        [HarmonyPrefix]
        public static bool FlushMusicAudio(GameManager __instance) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
            return true;
        }

        [HarmonyPatch(typeof(GameManager), nameof(GameManager.FlushAudio))]
        [HarmonyPrefix]
        public static bool FlushAudio(GameManager __instance) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, __instance.gameObject);
            return true;
        }
    }
}

