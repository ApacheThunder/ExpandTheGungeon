using Dungeonator;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandMain {
    
    public class ExpandDungeonMusicAPI {

        public static Hook switchToStateHook;
        // public static Hook notifyEnteredNewRoomHook;
        public static Hook switchToCustomMusicHook;
        public static Hook switchToEndTimesMusicHook;
        public static Hook switchToBossMusicHook;
        public static Hook switchToDragunTwoHook;
        public static Hook flushMusicAudioHook;
        public static Hook flushAudioHook;

        // Event for stopping all custom music. Ensure you defined this in your sound bank and put the string for it here.
        public static readonly string StopAllMusicEventName = "Stop_EX_MUS_All";

        // Dictionary for custom level music available to the mod. string value is the track name, the bool is for if loop events were setup or not.
        public static readonly Dictionary<string, bool> CustomLevelMusic = new Dictionary<string, bool>() {
            ["Play_EX_MUS_Belly_01"] = true,
            ["Play_EX_MUS_Jungle_01"] = true
        };

        public static readonly List<string> CustomWestFloorMusic = new List<string>() {
            "Play_MUS_Space_Theme_01",
            "Play_MUS_Office_Theme_01"
        };

        // Any custom rooms that use custom music goes here.
        public static readonly List<string> CustomRoomMusic = new List<string>() {
            "Play_EX_MUS_BootlegMusic_01",
            "Play_EX_UnicornMusic_01"
        };
        // All individual stop events for custom room music.
        public static readonly List<string> CustomRoomMusicStopEvents = new List<string>() {
            "Stop_EX_MUS_BootlegMusic_01",
            "Stop_EX_UnicornMusic_01"
        };

        public static readonly List<GlobalDungeonData.ValidTilesets> TilesetsWithCustomShopSecretMusic = new List<GlobalDungeonData.ValidTilesets>() {
            GlobalDungeonData.ValidTilesets.JUNGLEGEON
        };
        
        // Normal Action delegate doesn't support 5 arguments needed for SwitchToCustomMusic hook.
        public delegate void Action5X<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        // Needed to get some private fields. Can omit this if you have a ReflectionHelpers class that already has a version of this.
        public static T ReflectGetField<T>(Type classType, string fieldName, object o = null) {
            FieldInfo field = classType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static));
            return (T)field.GetValue(o);
        }

        // Call this from mod's main Start() call
        public static void InitHooks() {
            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonFloorMusicController.SwitchToState Hook...."); }
            switchToStateHook = new Hook(
                typeof(DungeonFloorMusicController).GetMethod("SwitchToState", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandDungeonMusicAPI).GetMethod(nameof(SwitchToState), BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(DungeonFloorMusicController)
            );
            /*if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonFloorMusicController.NotifyEnteredNewRoom Hook...."); }
            notifyEnteredNewRoomHook = new Hook(
                typeof(DungeonFloorMusicController).GetMethod(nameof(DungeonFloorMusicController.NotifyEnteredNewRoom), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandDungeonMusicAPI).GetMethod(nameof(NotifyEnteredNewRoom)),
                typeof(DungeonFloorMusicController)
            );*/

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonFloorMusicController.SwitchToCustomMusic Hook...."); }
            switchToCustomMusicHook = new Hook(
                typeof(DungeonFloorMusicController).GetMethod(nameof(DungeonFloorMusicController.SwitchToCustomMusic), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandDungeonMusicAPI).GetMethod(nameof(SwitchToCustomMusic)),
                typeof(DungeonFloorMusicController)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonFloorMusicController.SwitchToEndTimesMusic Hook...."); }
            switchToEndTimesMusicHook = new Hook(
                typeof(DungeonFloorMusicController).GetMethod(nameof(DungeonFloorMusicController.SwitchToEndTimesMusic), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandDungeonMusicAPI).GetMethod(nameof(SwitchToEndTimesMusic)),
                typeof(DungeonFloorMusicController)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonFloorMusicController.SwitchToBossMusic Hook...."); }
            switchToBossMusicHook = new Hook(
                typeof(DungeonFloorMusicController).GetMethod(nameof(DungeonFloorMusicController.SwitchToBossMusic), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandDungeonMusicAPI).GetMethod(nameof(SwitchToBossMusic)),
                typeof(DungeonFloorMusicController)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonFloorMusicController.SwitchToDragunTwo Hook...."); }
            switchToDragunTwoHook = new Hook(
                typeof(DungeonFloorMusicController).GetMethod(nameof(DungeonFloorMusicController.SwitchToDragunTwo), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandDungeonMusicAPI).GetMethod(nameof(SwitchToDragunTwo)),
                typeof(DungeonFloorMusicController)
            );
            
            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing GameManager.FlushMusicAudio Hook...."); }
            flushMusicAudioHook = new Hook(
                typeof(GameManager).GetMethod("FlushMusicAudio", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandDungeonMusicAPI).GetMethod(nameof(FlushMusicAudio)),
                typeof(GameManager)
            );
            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing GameManager.FlushAudio Hook...."); }
            flushAudioHook = new Hook(
                typeof(GameManager).GetMethod("FlushAudio", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandDungeonMusicAPI).GetMethod(nameof(FlushAudio)),
                typeof(GameManager)
            );
        }

        // For setting up specific loop events for the floor's custom music and ending custom music while in certain rooms like shops/secret rooms.
        private void SwitchToState(Action<DungeonFloorMusicController, DungeonFloorMusicController.DungeonMusicState> orig, DungeonFloorMusicController self, DungeonFloorMusicController.DungeonMusicState targetState) {
            string m_cachedMusicEventCore = ReflectGetField<string>(typeof(DungeonFloorMusicController), "m_cachedMusicEventCore", self);
            bool SupportsLoopSections = false;

            float m_changedToArcadeTimer = ReflectGetField<float>(typeof(DungeonFloorMusicController), "m_changedToArcadeTimer", self);
            float m_cooldownTimerRemaining = ReflectGetField<float>(typeof(DungeonFloorMusicController), "m_cooldownTimerRemaining", self);
            uint m_coreMusicEventID = ReflectGetField<uint>(typeof(DungeonFloorMusicController), "m_coreMusicEventID", self);
            DungeonFloorMusicController.DungeonMusicState m_currentState = ReflectGetField<DungeonFloorMusicController.DungeonMusicState>(typeof(DungeonFloorMusicController), "m_currentState", self);
            bool m_overrideMusic = ReflectGetField<bool>(typeof(DungeonFloorMusicController), "m_overrideMusic", self);
            
            if (string.IsNullOrEmpty(m_cachedMusicEventCore) | !CustomLevelMusic.TryGetValue(m_cachedMusicEventCore, out SupportsLoopSections)) {
                if (m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                    AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                }
                orig(self, targetState);
                return;
            }

            FieldInfo m_cooldownTimerRemainingField = typeof(DungeonFloorMusicController).GetField("m_cooldownTimerRemaining", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo m_currentStateField = typeof(DungeonFloorMusicController).GetField("m_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (m_changedToArcadeTimer > 0f && targetState == DungeonFloorMusicController.DungeonMusicState.CALM && m_currentState == DungeonFloorMusicController.DungeonMusicState.ARCADE) {
                return;
            }

            Debug.Log(string.Concat(new object[] { "(EX) Attemping to switch to state: ", targetState.ToString(), " with core ID: ", m_coreMusicEventID }));
            if (m_overrideMusic) { return; }
            switch (targetState) {
                case DungeonFloorMusicController.DungeonMusicState.ACTIVE_SIDE_A:
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    if (SupportsLoopSections) {
                        AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_LoopA", self.gameObject);
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_LoopA", self.gameObject);
                    } else if (m_currentState == DungeonFloorMusicController.DungeonMusicState.SHOP |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.SECRET |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.ARCADE |
                            m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)
                        )
                    {
                        if (m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        }
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore, self.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.ACTIVE_SIDE_B:
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    if (SupportsLoopSections) {
                        AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_LoopB", self.gameObject);
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_LoopB", self.gameObject);
                    } else if (m_currentState == DungeonFloorMusicController.DungeonMusicState.SHOP |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.SECRET |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.ARCADE |
                            m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)
                        )
                    {
                        if (m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        }
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore, self.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.ACTIVE_SIDE_C:
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    if (SupportsLoopSections) {
                        AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_LoopC", self.gameObject);
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_LoopC", self.gameObject);
                    } else if (m_currentState == DungeonFloorMusicController.DungeonMusicState.SHOP |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.SECRET |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.ARCADE |
                            m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)
                        )
                    {
                        if (m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        }
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore, self.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.ACTIVE_SIDE_D:                    
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    if (SupportsLoopSections) {
                        AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_LoopD", self.gameObject);
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_LoopD", self.gameObject);
                    } else if (m_currentState == DungeonFloorMusicController.DungeonMusicState.SHOP |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.SECRET |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS |
                            m_currentState == DungeonFloorMusicController.DungeonMusicState.ARCADE |
                            m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)
                        )
                    {
                        if (m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        }
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore, self.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.ARCADE:
                    m_cooldownTimerRemainingField.SetValue(self, -1f);
                    // if (SupportsLoopSections) { AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject); }
                    AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    AkSoundEngine.PostEvent("Play_MUS_Dungeon_Theme_01", self.gameObject);
                    AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Winchester", self.gameObject);
                    AkSoundEngine.PostEvent("Play_MUS_Winchester_State_Drone", self.gameObject);
                    // if (SupportsLoopSections) { AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Calm", self.gameObject); }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.CALM:
                    m_cooldownTimerRemainingField.SetValue(self, -1f);
                    if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER && GameStatsManager.Instance.AnyPastBeaten()) {
                        AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                        AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Winner", self.gameObject);
                    } else {
                        AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                        if (SupportsLoopSections) {
                            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                            AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Calm", self.gameObject);
                        } else if (m_currentState == DungeonFloorMusicController.DungeonMusicState.SHOP |
                                m_currentState == DungeonFloorMusicController.DungeonMusicState.SECRET |
                                m_currentState == DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS |
                                m_currentState == DungeonFloorMusicController.DungeonMusicState.ARCADE |
                                m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)
                            )
                        {
                            if (m_currentState == (DungeonFloorMusicController.DungeonMusicState)(-1)) {
                                AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                            }
                            AkSoundEngine.PostEvent(m_cachedMusicEventCore, self.gameObject);
                        }
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Drone", self.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.FLOOR_INTRO:
                    m_cooldownTimerRemainingField.SetValue(self, -1f);
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    if (SupportsLoopSections) {
                        AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Intro", self.gameObject);
                    } else {
                        AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore, self.gameObject);
                        // AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Intro", self.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.FOYER_ELEVATOR:
                    m_cooldownTimerRemainingField.SetValue(self, -1f);
                    AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    AkSoundEngine.PostEvent(m_cachedMusicEventCore, self.gameObject);
                    // AkSoundEngine.PostEvent("Play_MUS_State_Elevator", self.gameObject);
                    break;
                case DungeonFloorMusicController.DungeonMusicState.FOYER_SORCERESS:
                    m_cooldownTimerRemainingField.SetValue(self, -1f);
                    AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    AkSoundEngine.PostEvent("Play_MUS_Dungeon_Theme_01", self.gameObject);
                    AkSoundEngine.PostEvent("Play_MUS_State_Sorceress", self.gameObject);
                    // AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Sorceress", self.gameObject);
                    break;
                case DungeonFloorMusicController.DungeonMusicState.SECRET:
                    m_cooldownTimerRemainingField.SetValue(self, -1f);
                    AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    if (SupportsLoopSections && TilesetsWithCustomShopSecretMusic.Contains(self.gameObject.GetComponent<GameManager>().Dungeon.tileIndices.tilesetId)) {
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Secret", self.gameObject);
                    } else {
                        AkSoundEngine.PostEvent("Play_MUS_Dungeon_Theme_01", self.gameObject);
                        AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Secret", self.gameObject);
                    }
                    break;
                case DungeonFloorMusicController.DungeonMusicState.SHOP:
                    m_cooldownTimerRemainingField.SetValue(self, -1f);
                    AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
                    AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                    if (SupportsLoopSections && TilesetsWithCustomShopSecretMusic.Contains(self.gameObject.GetComponent<GameManager>().Dungeon.tileIndices.tilesetId)) {
                        AkSoundEngine.PostEvent(m_cachedMusicEventCore + "_Shop", self.gameObject);
                    } else {
                        AkSoundEngine.PostEvent("Play_MUS_Dungeon_Theme_01", self.gameObject);
                        AkSoundEngine.PostEvent("Play_MUS_Dungeon_State_Shop", self.gameObject);
                    }
                    break;
            }
            Debug.Log("(EX) Successfully switched to state: " + targetState.ToString());
            m_currentStateField.SetValue(self, targetState);
        }

        // Ensures custom floor music doesn't overlap and not overlap with other custom room music if player enters one room that has custom room music into another that also has custom room music.
        public void SwitchToCustomMusic(Action5X<DungeonFloorMusicController, string, GameObject, bool, string> orig, DungeonFloorMusicController self, string customMusicEvent, GameObject source, bool useSwitch, string switchEvent) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
            orig(self, customMusicEvent, source, useSwitch, switchEvent);
        }
        
        // This is specific to ExpandTheGungeon's Old West floor currently.
        /*public void NotifyEnteredNewRoom(Action<DungeonFloorMusicController, RoomHandler> orig, DungeonFloorMusicController self, RoomHandler newRoom) {
            if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                self.UpdateCoreMusicEvent();
                FieldInfo m_cachedMusicEventCore = typeof(DungeonFloorMusicController).GetField("m_cachedMusicEventCore", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo m_currentState = typeof(DungeonFloorMusicController).GetField("m_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
                if (string.IsNullOrEmpty((string)m_cachedMusicEventCore.GetValue(self)) | !CustomWestFloorMusic.Contains((string)m_cachedMusicEventCore.GetValue(self))) {
                    orig(self, newRoom);
                    return;
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
            }
            orig(self, newRoom);
        }*/

        // These Hooks ensure custom floor music ends when boss/endtimes musics starts.
        public void SwitchToEndTimesMusic(Action<DungeonFloorMusicController> orig, DungeonFloorMusicController self) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
            orig(self);
        }

        public void SwitchToBossMusic(Action<DungeonFloorMusicController, string, GameObject>orig, DungeonFloorMusicController self, string bossMusicString, GameObject source) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
            orig(self, bossMusicString, source);
        }

        public void SwitchToDragunTwo(Action<DungeonFloorMusicController> orig, DungeonFloorMusicController self) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
            orig(self);
        }
        
        // These hooks ensure our custom music/audio gets stopped properly when leaving a floor.
        public void FlushMusicAudio(Action<GameManager> orig, GameManager self) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
            orig(self);
        }

        public void FlushAudio(Action<GameManager> orig, GameManager self) {
            AkSoundEngine.PostEvent(StopAllMusicEventName, self.gameObject);
            orig(self);
        }
    }
}

