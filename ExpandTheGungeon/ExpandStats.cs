using ExpandTheGungeon.ExpandUtilities;
using UnityEngine;

namespace ExpandTheGungeon {

    public static class ExpandStats {

        public static bool debugMode = false;        
        public static bool youtubeSafeMode = false;
        public static bool IsHardModeBuild = false;
        public static bool RestoreOldRooms = false;
        public static bool EnableLanguageFix = false;
        // Refer to ExpandUtilities.ExpandUtility.LanguageToInt or IntToLanguage for which language this number can be matched to.
        public static int GameLanguage = 0;

        // These are set during GamePlay, don't read/write them from JSON text.
        public static bool allowGlitchFloor = false;
        public static bool elevatorHasBeenUsed = false;
        public static bool HasSpawnedSecretBoss = false;
        public static float randomSeed = 0.5f;
        
        public static void OverwriteUserSettings(ExpandCachedStats stats) {
            debugMode = stats.debugMode;
            youtubeSafeMode = stats.youtubeSafeMode;
            IsHardModeBuild = stats.IsHardModeBuild;
            EnableLanguageFix = stats.EnableLanguageFix;
            GameLanguage = stats.GameLanguage;
        }

    }

    public class ExpandCachedStats : ScriptableObject {
        public bool debugMode;
        public bool youtubeSafeMode;
        public bool IsHardModeBuild;
        public bool RestoreOldRooms;
        public bool EnableLanguageFix;
        public int GameLanguage;
        
        public ExpandCachedStats() {
            debugMode = ExpandStats.debugMode;
            youtubeSafeMode = ExpandStats.youtubeSafeMode;
            IsHardModeBuild = ExpandStats.IsHardModeBuild;
            RestoreOldRooms = ExpandStats.RestoreOldRooms;
            EnableLanguageFix = ExpandStats.EnableLanguageFix;
            GameLanguage = ExpandStats.GameLanguage;
        }
    }
}

