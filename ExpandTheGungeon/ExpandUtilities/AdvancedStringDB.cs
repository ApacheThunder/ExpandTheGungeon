using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExpandTheGungeon.ExpandUtilities {

    public class AdvancedStringDB {

        public readonly AdvancedStringDBTable Core;
        public readonly AdvancedStringDBTable Items;
        public readonly AdvancedStringDBTable Enemies;
        public readonly AdvancedStringDBTable Intro;
        public readonly AdvancedStringDBTable Synergies;

        public static FieldInfo m_synergyTable = typeof(StringTableManager).GetField("m_synergyTable", BindingFlags.Static | BindingFlags.NonPublic);

        public Action<StringTableManager.GungeonSupportedLanguages> OnLanguageChanged;

        public StringTableManager.GungeonSupportedLanguages CurrentLanguage {
            get { return GameManager.Options.CurrentLanguage; }
            set { StringTableManager.SetNewLanguage(value, true); }
        }

        public AdvancedStringDB() {
            StringDB strings = ETGMod.Databases.Strings;
            strings.OnLanguageChanged = (Action<StringTableManager.GungeonSupportedLanguages>)Delegate.Combine(strings.OnLanguageChanged, new Action<StringTableManager.GungeonSupportedLanguages>(this.LanguageChanged));
            Core = new AdvancedStringDBTable(() => StringTableManager.CoreTable);
            Items = new AdvancedStringDBTable(() => StringTableManager.ItemTable);
            Enemies = new AdvancedStringDBTable(() => StringTableManager.EnemyTable);
            Intro = new AdvancedStringDBTable(() => StringTableManager.IntroTable);
            Synergies = new AdvancedStringDBTable(() => AdvancedStringDB.SynergyTable);
        }

        public void LanguageChanged(StringTableManager.GungeonSupportedLanguages newLang) {
            Core.LanguageChanged();
            Items.LanguageChanged();
            Enemies.LanguageChanged();
            Intro.LanguageChanged();
            Synergies.LanguageChanged();
            Action<StringTableManager.GungeonSupportedLanguages> onLanguageChanged = this.OnLanguageChanged;
            bool flag = onLanguageChanged == null;
            bool flag2 = !flag;
            if (flag2) { onLanguageChanged(newLang); }
        }

        public static Dictionary<string, StringTableManager.StringCollection> SynergyTable {
            get {
                StringTableManager.GetSynergyString("ThisExistsOnlyToLoadTables", -1);
                return (Dictionary<string, StringTableManager.StringCollection>)m_synergyTable.GetValue(null);
            }
        }
    }

    public sealed class AdvancedStringDBTable {
        private readonly Func<Dictionary<string, StringTableManager.StringCollection>> _GetTable;

        private Dictionary<string, StringTableManager.StringCollection> _CachedTable;

        private readonly List<string> _ChangeKeys;

        private readonly List<StringTableManager.StringCollection> _ChangeValues;

        public Dictionary<string, StringTableManager.StringCollection> Table {
            get {
                Dictionary<string, StringTableManager.StringCollection> result;
                bool flag = (result = _CachedTable) == null;
                if (flag) {
                    return (_CachedTable = _GetTable());
                } else {
                    return result;
                } 
            }
        }

        public StringTableManager.StringCollection this[string key] {
            get { return Table[key]; }
            set {
                Table[key] = value;
                int num = _ChangeKeys.IndexOf(key);;
                if (num > 0) {
                    _ChangeValues[num] = value;
                } else {
                    _ChangeKeys.Add(key);
                    _ChangeValues.Add(value);
                }
                JournalEntry.ReloadDataSemaphore++;
            }
        }

        public AdvancedStringDBTable(Func<Dictionary<string, StringTableManager.StringCollection>> _getTable) {
            _ChangeKeys = new List<string>();
            _ChangeValues = new List<StringTableManager.StringCollection>();
            _GetTable = _getTable;
        }

        public bool ContainsKey(string key) { return Table.ContainsKey(key); }

        public void Set(string key, string value) {
            StringTableManager.StringCollection stringCollection = new StringTableManager.SimpleStringCollection();
            stringCollection.AddString(value, 1f);
            bool flag = Table.ContainsKey(key);
            if (flag) { Table[key] = stringCollection; } else { Table.Add(key, stringCollection); }
            int num = _ChangeKeys.IndexOf(key);
            bool flag2 = num > 0;
            if (flag2) {
                _ChangeValues[num] = stringCollection;
            } else {
                _ChangeKeys.Add(key);
                _ChangeValues.Add(stringCollection);
            }
            JournalEntry.ReloadDataSemaphore++;
        }

        public void SetComplex(string key, params string[] values) {
            StringTableManager.StringCollection stringCollection = new StringTableManager.ComplexStringCollection();
            foreach (string text in values) { stringCollection.AddString(text, 1f); }
            Table[key] = stringCollection;
            int num = _ChangeKeys.IndexOf(key);
            bool flag = num > 0;
            if (flag) {
                _ChangeValues[num] = stringCollection;
            } else {
                _ChangeKeys.Add(key);
                _ChangeValues.Add(stringCollection);
            }
            JournalEntry.ReloadDataSemaphore++;
        }

        public void SetComplex(string key, List<string> values, List<float> weights) {
            StringTableManager.StringCollection stringCollection = new StringTableManager.ComplexStringCollection();
            for (int i = 0; i < values.Count; i++) {
                string text = values[i];
                float num = weights[i];
                stringCollection.AddString(text, num);
            }
            Table[key] = stringCollection;
            int num2 = _ChangeKeys.IndexOf(key);
            bool flag = num2 > 0;
            if (flag) {
                _ChangeValues[num2] = stringCollection;
            } else {
                _ChangeKeys.Add(key);
                _ChangeValues.Add(stringCollection);
            }
            JournalEntry.ReloadDataSemaphore++;
        }

        public string Get(string key) { return StringTableManager.GetString(key); }

        public void LanguageChanged() {
            _CachedTable = null;
            Dictionary<string, StringTableManager.StringCollection> table = Table;
            for (int i = 0; i < _ChangeKeys.Count; i++) { table[_ChangeKeys[i]] = _ChangeValues[i]; }
        }
    }
}

