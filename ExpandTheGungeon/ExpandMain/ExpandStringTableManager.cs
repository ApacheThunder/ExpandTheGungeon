using System.Collections.Generic;
using System.IO;
using MonoMod;
using UnityEngine;
using System;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandMain {

    public static class ExpandStringTableManager {

        static ExpandStringTableManager() {
            m_currentFile = "english";
            m_currentSubDirectory = "english_items";
        }

        public static Dictionary<string, StringTableManager.StringCollection> m_enemiesTable;
        public static Dictionary<string, StringTableManager.StringCollection> m_backupEnemiesTable;

        private static string m_currentFile;
        private static string m_currentSubDirectory;
                
        public static string GetEnemiesString(string key, int index = -1) {

            Dictionary<string, StringTableManager.StringCollection> m_tempEnemyTable = ReflectionHelpers.ReflectGetField<Dictionary<string, StringTableManager.StringCollection>>(typeof(StringTableManager), "m_enemiesTable");

            // This forces original StringTableManager to load it's dictionaries which I will try to use first before loading it with my code.
            // THis hopefully fixes interactiosn with mods that add custom enemy names to the dictionaries.
            if (m_tempEnemyTable == null) { string Temp = StringTableManager.GetEnemiesLongDescription("TEST"); }

            if (m_tempEnemyTable != null) { m_enemiesTable = m_tempEnemyTable; }

            if (m_enemiesTable == null) { m_enemiesTable = LoadEnemiesTable(m_currentSubDirectory); }

            if (m_backupEnemiesTable == null) { m_backupEnemiesTable = LoadEnemiesTable("english_items"); }
            
            if (m_enemiesTable.ContainsKey(key)) {
                if (index == -1) {
                    string weightedString = m_enemiesTable[key].GetWeightedString();
                    return StringTableManager.PostprocessString(weightedString);
                }
                return StringTableManager.PostprocessString(m_enemiesTable[key].GetExactString(index));
            } else {
                // Return key directly instead of "ENEMY_STRING_NOT_FOUND" text. Used to display custom names for glitch enemies without the hassle of messing with the string table.
                if (!m_backupEnemiesTable.ContainsKey(key)) { return key; }
                if (index == -1) {
                    string weightedString2 = m_backupEnemiesTable[key].GetWeightedString();
                    return StringTableManager.PostprocessString(weightedString2);
                }
                return StringTableManager.PostprocessString(m_backupEnemiesTable[key].GetExactString(index));
            }
        }

        // Allow for custom synergy text
        public static string GetSynergyString(Func<string, int, string> action, string key, int index = -1) {
            string text = action(key, index);
            if (string.IsNullOrEmpty(text)) { text = key; }
            return text;
        }

        private static Dictionary<string, StringTableManager.StringCollection> LoadEnemiesTable(string subDirectory) {
            TextAsset textAsset = (TextAsset)BraveResources.Load("strings/" + subDirectory + "/enemies", typeof(TextAsset), ".txt");
            if (textAsset == null) {
                Debug.LogError("Failed to load string table: ENEMIES.");
                return null;
            }
            StringReader stringReader = new StringReader(textAsset.text);
            Dictionary<string, StringTableManager.StringCollection> dictionary = new Dictionary<string, StringTableManager.StringCollection>();
            StringTableManager.StringCollection stringCollection = null;
            string text;
            while ((text = stringReader.ReadLine()) != null) {
                if (!text.StartsWith("//")) {
                    if (text.StartsWith("#")) {
                        stringCollection = new StringTableManager.ComplexStringCollection();
                        if (dictionary.ContainsKey(text)) {
                            Debug.LogError("Failed to add duplicate key to items table: " + text);
                        } else {
                            dictionary.Add(text, stringCollection);
                        }
                    } else {
                        string[] array = text.Split(new char[] { '|' });
                        if (array.Length == 1) {
                            stringCollection.AddString(array[0], 1f);
                        } else {
                            stringCollection.AddString(array[1], float.Parse(array[0]));
                        }
                    }
                }
            }
            return dictionary;
        }

        [MonoModOriginalName("orig_SetNewLanguage")]
        public static void SetNewLanguage(StringTableManager.GungeonSupportedLanguages language, bool force = false) {            
            orig_SetNewLanguage(language, force);
            ETGMod.Databases.Strings.LanguageChanged();
        }

        [MonoModOriginalName(null)]
        public static void orig_SetNewLanguage(StringTableManager.GungeonSupportedLanguages language, bool force = false) {
            if (!force && StringTableManager.CurrentLanguage == language) { return; }
            if (m_currentFile == null) { goto IL_176; }

            IL_176:;
            switch (language) {
                case StringTableManager.GungeonSupportedLanguages.ENGLISH:
                    m_currentFile = "english";
                    m_currentSubDirectory = "english_items";
                    goto IL_179;
                case StringTableManager.GungeonSupportedLanguages.FRENCH:
                    m_currentFile = "french";
                    m_currentSubDirectory = "french_items";
                    goto IL_179;
                case StringTableManager.GungeonSupportedLanguages.SPANISH:
                    m_currentFile = "spanish";
                    m_currentSubDirectory = "spanish_items";
                    goto IL_179;
                case StringTableManager.GungeonSupportedLanguages.ITALIAN:
                    m_currentFile = "italian";
                    m_currentSubDirectory = "italian_items";
                    goto IL_179;
                case StringTableManager.GungeonSupportedLanguages.GERMAN:
                    m_currentFile = "german";
                    m_currentSubDirectory = "german_items";
                    goto IL_179;
                case StringTableManager.GungeonSupportedLanguages.BRAZILIANPORTUGUESE:
                    m_currentFile = "portuguese";
                    m_currentSubDirectory = "portuguese_items";
                    goto IL_179;
                case StringTableManager.GungeonSupportedLanguages.JAPANESE:
                    m_currentFile = "japanese";
                    m_currentSubDirectory = "japanese_items";
                    goto IL_179;
                case StringTableManager.GungeonSupportedLanguages.KOREAN:
                    m_currentFile = "korean";
                    m_currentSubDirectory = "korean_items";
                    goto IL_179;
                case StringTableManager.GungeonSupportedLanguages.RUSSIAN:
                    m_currentFile = "russian";
                    m_currentSubDirectory = "russian_items";
                    goto IL_179;
                case StringTableManager.GungeonSupportedLanguages.POLISH:
                    m_currentFile = "polish";
                    m_currentSubDirectory = "polish_items";
                    goto IL_179;
                case StringTableManager.GungeonSupportedLanguages.CHINESE:
                    m_currentFile = "chinese";
                    m_currentSubDirectory = "chinese_items";
                    goto IL_179;
            }
            m_currentFile = "english";
            m_currentSubDirectory = "english_items";
            IL_179:
            StringTableManager.ReloadAllTables();
            dfLanguageManager.ChangeGungeonLanguage();
            JournalEntry.ReloadDataSemaphore++;
        }
    }
}

