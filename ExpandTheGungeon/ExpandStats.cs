using ExpandTheGungeon.ExpandUtilities;
using UnityEngine;

namespace ExpandTheGungeon {

    public static class ExpandStats {

        public static bool EnableTestDungeonFlow = false;
        public static string TestFlow = "Test_CustomRoom_Flow";
        public static string TestFloor = "tt_phobos";
        public static bool debugMode = false;        
        public static bool youtubeSafeMode = false;
        public static bool IsHardModeBuild = false;
        public static bool RestoreOldRooms = false;
        public static bool EnableJungleRain = true;
        public static bool EnableBloodiedScarfFix = true;
        public static bool ShotgunKinSecret = false;
        public static bool EnableLanguageFix = false;
        public static bool EnableExpandedGlitchFloors = true;
        public static bool EnableGlitchFloorScreenShader = true;
        public static float JungleRainIntensity = 400f;
        // Refer to ExpandUtilities.ExpandUtility.LanguageToInt or IntToLanguage for which language this number can be matched to.
        public static int GameLanguage = 0;

        // These are set during GamePlay, don't read/write them from JSON text.
        public static bool allowGlitchFloor = false;
        public static bool elevatorHasBeenUsed = false;
        public static bool HasSpawnedSecretBoss = false;
        public static float randomSeed = 0.5f;
        
        public static void OverwriteUserSettings(ExpandCachedStats stats) {
            EnableTestDungeonFlow = stats.EnableTestDungeonFlow;
            TestFlow = stats.TestFlow;
            TestFloor = stats.TestFloor;
            debugMode = stats.debugMode;
            youtubeSafeMode = stats.youtubeSafeMode;
            IsHardModeBuild = stats.IsHardModeBuild;
            EnableLanguageFix = stats.EnableLanguageFix;
            EnableJungleRain = stats.EnableJungleRain;
            EnableBloodiedScarfFix = stats.EnableBloodiedScarfFix;
            ShotgunKinSecret = stats.ShotgunKinSecret;
            GameLanguage = stats.GameLanguage;
            EnableExpandedGlitchFloors = stats.EnableExpandedGlitchFloors;
            EnableGlitchFloorScreenShader = stats.EnableGlitchFloorScreenShader;
            JungleRainIntensity = stats.JungleRainIntensity;
        }
    }

    public class ExpandCachedStats : ScriptableObject {
        public bool EnableTestDungeonFlow;
        public string TestFlow;
        public string TestFloor;
        public bool debugMode;
        public bool youtubeSafeMode;
        public bool IsHardModeBuild;
        public bool RestoreOldRooms;
        public bool EnableJungleRain;
        public bool EnableBloodiedScarfFix;
        public bool ShotgunKinSecret;
        public bool EnableLanguageFix;
        public bool EnableExpandedGlitchFloors;
        public bool EnableGlitchFloorScreenShader;
        public float JungleRainIntensity;
        public int GameLanguage;
        
        public ExpandCachedStats() {
            EnableTestDungeonFlow = ExpandStats.EnableTestDungeonFlow;
            TestFlow = ExpandStats.TestFlow;
            TestFloor = ExpandStats.TestFloor;
            debugMode = ExpandStats.debugMode;
            youtubeSafeMode = ExpandStats.youtubeSafeMode;
            IsHardModeBuild = ExpandStats.IsHardModeBuild;
            RestoreOldRooms = ExpandStats.RestoreOldRooms;
            EnableJungleRain = ExpandStats.EnableJungleRain;
            EnableBloodiedScarfFix = ExpandStats.EnableBloodiedScarfFix;
            ShotgunKinSecret = ExpandStats.ShotgunKinSecret;
            EnableLanguageFix = ExpandStats.EnableLanguageFix;
            EnableExpandedGlitchFloors = ExpandStats.EnableExpandedGlitchFloors;
            EnableGlitchFloorScreenShader = ExpandStats.EnableGlitchFloorScreenShader;
            JungleRainIntensity = ExpandStats.JungleRainIntensity;
            GameLanguage = ExpandStats.GameLanguage;
        }
    }
}

