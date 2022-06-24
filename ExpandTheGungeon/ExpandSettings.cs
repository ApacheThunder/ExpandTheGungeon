using System.IO;
using UnityEngine;

namespace ExpandTheGungeon {

    public static class ExpandSettings {

        public static bool EnableLogo = true;
        public static bool EnableTestDungeonFlow = false;
        public static string TestFlow = "Test_CustomRoom_Flow";
        public static string TestFloor = "tt_phobos";
        public static bool debugMode = false;        
        public static bool youtubeSafeMode = false;
        public static bool IsHardModeBuild = false;
        public static bool RestoreOldRooms = false;
        public static bool EnableJungleRain = true;
        public static bool EnableBloodiedScarfFix = true;
        public static bool EnableLanguageFix = false;
        public static bool EnableExpandedGlitchFloors = true;
        public static bool EnableGlitchFloorScreenShader = true;
        public static bool EnableEXItems = true;
        public static float JungleRainIntensity = 400f;
        // Refer to ExpandUtilities.ExpandUtility.LanguageToInt or IntToLanguage for which language this number can be matched to.
        public static int GameLanguage = 0;

        // These are set during GamePlay, don't read/write them from JSON text.
        public static bool spritesBundlePresent = false;
        public static bool allowGlitchFloor = false;
        public static bool glitchElevatorHasBeenUsed = false;
        public static bool HasSpawnedSecretBoss = false;
        public static float randomSeed = 0.5f;
        public static bool PlayingPunchoutArcade = false;
        
        public static void LoadSettings() {
            if (File.Exists(Path.Combine(ETGMod.ResourcesDirectory, ExpandTheGungeon.ModSettingsFileName))) {
                string CachedJSONText = File.ReadAllText(Path.Combine(ETGMod.ResourcesDirectory, ExpandTheGungeon.ModSettingsFileName));
                ExpandCachedStats cachedStats = ScriptableObject.CreateInstance<ExpandCachedStats>();
                JsonUtility.FromJsonOverwrite(CachedJSONText, cachedStats);
                OverwriteUserSettings(cachedStats);
            } else {
                SaveSettings();
                return;
            }
        }

        public static void SaveSettings() {

            string CachedJSONText = string.Empty;

            ExpandCachedStats cachedStats = ScriptableObject.CreateInstance<ExpandCachedStats>();

            CachedJSONText = JsonUtility.ToJson(cachedStats, true);

            if (File.Exists(Path.Combine(ETGMod.ResourcesDirectory, ExpandTheGungeon.ModSettingsFileName))) {
                File.Delete(Path.Combine(ETGMod.ResourcesDirectory, ExpandTheGungeon.ModSettingsFileName));
            }

            ExpandAssets.SaveStringToFile(CachedJSONText, ETGMod.ResourcesDirectory, ExpandTheGungeon.ModSettingsFileName);
            return;
        }
        
        public static void OverwriteUserSettings(ExpandCachedStats stats) {
            EnableLogo = stats.EnableLogo;
            EnableTestDungeonFlow = stats.EnableTestDungeonFlow;
            TestFlow = stats.TestFlow;
            TestFloor = stats.TestFloor;
            debugMode = stats.debugMode;
            youtubeSafeMode = stats.youtubeSafeMode;
            IsHardModeBuild = stats.IsHardModeBuild;
            EnableLanguageFix = stats.EnableLanguageFix;
            EnableJungleRain = stats.EnableJungleRain;
            EnableBloodiedScarfFix = stats.EnableBloodiedScarfFix;
            GameLanguage = stats.GameLanguage;
            EnableExpandedGlitchFloors = stats.EnableExpandedGlitchFloors;
            EnableGlitchFloorScreenShader = stats.EnableGlitchFloorScreenShader;
            EnableEXItems = stats.EnableEXItems;
            JungleRainIntensity = stats.JungleRainIntensity;
        }
    }

    public class ExpandCachedStats : ScriptableObject {
        public bool EnableLogo;
        public bool EnableTestDungeonFlow;
        public string TestFlow;
        public string TestFloor;
        public bool debugMode;
        public bool youtubeSafeMode;
        public bool IsHardModeBuild;
        public bool RestoreOldRooms;
        public bool EnableJungleRain;
        public bool EnableBloodiedScarfFix;
        public bool EnableLanguageFix;
        public bool EnableExpandedGlitchFloors;
        public bool EnableGlitchFloorScreenShader;
        public bool EnableEXItems;
        public float JungleRainIntensity;
        public int GameLanguage;
        
        public ExpandCachedStats() {
            EnableLogo = ExpandSettings.EnableLogo;
            EnableTestDungeonFlow = ExpandSettings.EnableTestDungeonFlow;
            TestFlow = ExpandSettings.TestFlow;
            TestFloor = ExpandSettings.TestFloor;
            debugMode = ExpandSettings.debugMode;
            youtubeSafeMode = ExpandSettings.youtubeSafeMode;
            IsHardModeBuild = ExpandSettings.IsHardModeBuild;
            RestoreOldRooms = ExpandSettings.RestoreOldRooms;
            EnableJungleRain = ExpandSettings.EnableJungleRain;
            EnableBloodiedScarfFix = ExpandSettings.EnableBloodiedScarfFix;
            EnableLanguageFix = ExpandSettings.EnableLanguageFix;
            EnableExpandedGlitchFloors = ExpandSettings.EnableExpandedGlitchFloors;
            EnableGlitchFloorScreenShader = ExpandSettings.EnableGlitchFloorScreenShader;
            EnableEXItems = ExpandSettings.EnableEXItems;
            JungleRainIntensity = ExpandSettings.JungleRainIntensity;
            GameLanguage = ExpandSettings.GameLanguage;
        }
    }
}

