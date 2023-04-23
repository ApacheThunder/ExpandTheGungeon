using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandUtilities {

    public class AdvancedStringDB {

        public readonly AdvancedStringDBTable Core;
        public readonly AdvancedStringDBTable Items;
        public readonly AdvancedStringDBTable Enemies;
        public readonly AdvancedStringDBTable Intro;
        public readonly AdvancedStringDBTable Synergies;

        public Action<StringTableManager.GungeonSupportedLanguages> OnLanguageChanged;

        private static FieldInfo m_synergyTable = typeof(StringTableManager).GetField("m_synergyTable", BindingFlags.Static | BindingFlags.NonPublic);

        
        public StringTableManager.GungeonSupportedLanguages CurrentLanguage {
            get { return GameManager.Options.CurrentLanguage; }
            set { StringTableManager.SetNewLanguage(value, true); }
        }

        public AdvancedStringDB() {
            StringDB strings = ETGMod.Databases.Strings;
            strings.OnLanguageChanged = (Action<StringTableManager.GungeonSupportedLanguages>)Delegate.Combine(strings.OnLanguageChanged, new Action<StringTableManager.GungeonSupportedLanguages>(LanguageChanged));
            Core = new AdvancedStringDBTable(() => StringTableManager.CoreTable);
            Items = new AdvancedStringDBTable(() => StringTableManager.ItemTable);
            Enemies = new AdvancedStringDBTable(() => StringTableManager.EnemyTable);
            Intro = new AdvancedStringDBTable(() => StringTableManager.IntroTable);
            Synergies = new AdvancedStringDBTable(() => SynergyTable);
        }

        public void LanguageChanged(StringTableManager.GungeonSupportedLanguages newLang) {
            Core.LanguageChanged();
            Items.LanguageChanged();
            Enemies.LanguageChanged();
            Intro.LanguageChanged();
            Synergies.LanguageChanged();
            Action<StringTableManager.GungeonSupportedLanguages> onLanguageChanged = OnLanguageChanged;
            onLanguageChanged?.Invoke(newLang);
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
                if ((result = _CachedTable) == null) { result = (_CachedTable = _GetTable()); }
                return result;
            }
        }

        /// <summary>
        /// Gets or sets a string from this table using <paramref name="key"/> as the key.
        /// </summary>
        /// <param name="key">The key for the string.</param>
        /// <returns>The string.</returns>
        public StringTableManager.StringCollection this[string key] {
            get { return Table[key]; }
            set {
                Table[key] = value;
                int num = _ChangeKeys.IndexOf(key);
                if (num > 0) {
                    _ChangeValues[num] = value;
                } else {
                    _ChangeKeys.Add(key);
                    _ChangeValues.Add(value);
                }
                JournalEntry.ReloadDataSemaphore++;
            }
        }

        /// <summary>
        /// Creates a new <see cref="AdvancedStringDBTable"/>.
        /// </summary>
        /// <param name="_getTable">Method for getting the strings for the table.</param>
        public AdvancedStringDBTable(Func<Dictionary<string, StringTableManager.StringCollection>> _getTable) {
            _ChangeKeys = new List<string>();
            _ChangeValues = new List<StringTableManager.StringCollection>();
            _GetTable = _getTable;
        }

        /// <summary>
        /// Returns <see langword="true"/> if this string table contains <paramref name="key"/> in it's list of keys, returns <see langword="false"/> otherwise.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns><see langword="true"/> if this string table contains <paramref name="key"/> in it's list of keys, <see langword="false"/> otherwise.</returns>
        public bool ContainsKey(string key) { return Table.ContainsKey(key); }

        /// <summary>
        /// Sets <paramref name="key"/>'s value to <paramref name="value"/> in this string table.
        /// </summary>
        /// <param name="key">The key for the string to set.</param>
        /// <param name="value">The new value for the string.</param>
        public void Set(string key, string value) {
            StringTableManager.StringCollection stringCollection = new StringTableManager.SimpleStringCollection();
            stringCollection.AddString(value, 1f);
            if (Table.ContainsKey(key)) { Table[key] = stringCollection; } else { Table.Add(key, stringCollection); }
            int num = _ChangeKeys.IndexOf(key);
            if (num > 0) {
                _ChangeValues[num] = stringCollection;
            } else {
                _ChangeKeys.Add(key);
                _ChangeValues.Add(stringCollection);
            }
            JournalEntry.ReloadDataSemaphore++;
        }

        /// <summary>
        /// Sets <paramref name="key"/>'s value to <paramref name="values"/> in this string table.
        /// </summary>
        /// <param name="key">The key for the strings to set.</param>
        /// <param name="values">The new values for the string.</param>
        public void SetComplex(string key, params string[] values) {
            StringTableManager.StringCollection stringCollection = new StringTableManager.ComplexStringCollection();
            for (int i = 0; i < values.Length; i++) {
                string value = values[i];
                stringCollection.AddString(value, 1f);
            }
            Table[key] = stringCollection;
            int num = _ChangeKeys.IndexOf(key);
            if (num > 0) {
                _ChangeValues[num] = stringCollection;
            } else {
                _ChangeKeys.Add(key);
                _ChangeValues.Add(stringCollection);
            }
            JournalEntry.ReloadDataSemaphore++;
        }

        /// <summary>
        /// Sets <paramref name="key"/>'s value to <paramref name="values"/> and makes sets their weights to <paramref name="weights"/> in this string table.
        /// </summary>
        /// <param name="key">The key for the strings to set.</param>
        /// <param name="values">The new values for the string.</param>
        /// <param name="weights">The weights for the new values.</param>
        public void SetComplex(string key, List<string> values, List<float> weights) {
            StringTableManager.StringCollection stringCollection = new StringTableManager.ComplexStringCollection();
            for (int i = 0; i < values.Count; i++) {
                string value = values[i];
                float weight = weights[i];
                stringCollection.AddString(value, weight);
            }
            Table[key] = stringCollection;
            int num = _ChangeKeys.IndexOf(key);
            if (num > 0) {
                _ChangeValues[num] = stringCollection;
            } else {
                _ChangeKeys.Add(key);
                _ChangeValues.Add(stringCollection);
            }
            JournalEntry.ReloadDataSemaphore++;
        }

        /// <summary>
        /// Gets a string using <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key for the string.</param>
        /// <returns>The found string.</returns>
        public string Get(string key) { return StringTableManager.GetString(key); }

        /// <summary>
        /// Makes all the new/changed strings not be lost when changing the game's language.
        /// </summary>
        public void LanguageChanged() {
            _CachedTable = null;
            Dictionary<string, StringTableManager.StringCollection> table = Table;
            for (int i = 0; i < _ChangeKeys.Count; i++) { table[_ChangeKeys[i]] = _ChangeValues[i]; }
        }
    }

    /*public class TextMaker : MonoBehaviour {
        dfControl nameLabel;
        public tk2dSprite sprite = null;

        public string Text;
        public Color Color;
        public float Opacity;
        public float TextSize;
        //============================//
        public dfPivotPoint anchor;
        public Vector3 offset;
        public GameObject GameObjectToAttachTo;
        //============================//
        public float FadeInTime;
        public float FadeOutTime;
        public float ExistTime;
        public float Delay;
        public bool TextDisappearsEver;
        //============================//
        private float Elapsed = 0;
        private float ExitFade;
        //============================//

        private float CalculateCenterXoffset(dfLabel label) { return label.GetCenter().x - label.transform.position.x; }
        private float xOffSet = 0;
        private bool IsForceFading;

        public TextMaker() {
            Text = "test";
            Color = Color.white;
            Opacity = 1;
            TextSize = 3;
            TextDisappearsEver = true;
            //============================//
            anchor = dfPivotPoint.TopCenter;
            offset = new Vector3(0, 0);
            //this.GameObjectToAttachTo = GameManager.Instance.PrimaryPlayer.gameObject;
            //============================//
            FadeInTime = 0;
            FadeOutTime = 0;
            ExistTime = 1;
            Delay = 0;

        }

        public void Start() {

            ExistTime += FadeInTime;
            ExistTime += FadeOutTime;
            ExitFade = FadeOutTime;
            sprite = this.gameObject.GetComponent<tk2dSprite>();
            GameObject gameObject = Instantiate(BraveResources.Load<GameObject>("DamagePopupLabel", ".prefab"), GameUIRoot.Instance.transform);
            Vector3 worldPosition = transform.position;
            dfLabel Label = gameObject.GetComponent<dfLabel>();

            dfLabel targetLabel = (Label as dfLabel);

            targetLabel.gameObject.SetActive(true);

            targetLabel.Text = Text;
            targetLabel.Color = Color;
            if (FadeInTime > 0) {
                targetLabel.Opacity = 0;
            } else {
                targetLabel.Opacity = Opacity;
            }
            targetLabel.TextScale = TextSize;
            

            //targetLabel.transform.position = dfFollowObject.ConvertWorldSpaces(worldPosition, GameManager.Instance.MainCameraController.Camera, GameUIRoot.Instance.Manager.RenderCamera).WithZ(0f);
            //targetLabel.transform.position = targetLabel.transform.position.QuantizeFloor(targetLabel.PixelsToUnits() / (Pixelator.Instance.ScaleTileScale / Pixelator.Instance.CurrentTileScale));
            //xOffSet = CalculateCenterXoffset(targetLabel);

            dfFollowObject component = targetLabel.gameObject.AddComponent<dfFollowObject>();
            component.attach = transform.gameObject;
            component.enabled = true;
            component.mainCamera = GameManager.Instance.MainCameraController.Camera;
            component.anchor = anchor;
            component.offset = new Vector3(CalculateCenterXoffset(targetLabel), 0) + offset;
            nameLabel = targetLabel;
        }
        

        public void Update() {
            dfLabel targetLabel = nameLabel as dfLabel;
            if (nameLabel != null) {
                if (GameManager.Instance.IsPaused) {
                    targetLabel.IsVisible = false;
                } else {
                    if (TextDisappearsEver == true && IsForceFading == false) {
                        targetLabel.IsVisible = true;
                        Elapsed += BraveTime.DeltaTime;
                        if (Elapsed < FadeInTime && FadeInTime != 0) {
                            float t = Elapsed / FadeInTime;
                            targetLabel.Opacity = t * Opacity;
                        } else if (Elapsed > ExistTime - FadeOutTime && FadeOutTime != 0) {
                            float t = ExitFade / FadeOutTime;
                            targetLabel.Opacity = t * Opacity;
                            ExitFade -= BraveTime.DeltaTime;
                        } else if (Elapsed > FadeInTime && Elapsed < ExistTime - FadeOutTime) {
                            targetLabel.Opacity = Opacity;
                        } else if (Elapsed < ExistTime && !GameManager.Instance.IsPaused) {
                            targetLabel.Opacity = Opacity;
                        } else if (Elapsed > ExistTime) {
                            Destroy(targetLabel.gameObject);
                        }
                    } else if (IsForceFading == false) {
                        targetLabel.IsVisible = true;
                        Elapsed += BraveTime.DeltaTime;
                        if (Elapsed < FadeInTime && FadeInTime != 0) {
                            float t = Elapsed / FadeInTime;
                            targetLabel.Opacity = t * Opacity;
                        } else if (Elapsed > FadeInTime && Elapsed < ExistTime - FadeOutTime) {
                            targetLabel.Opacity = Opacity;
                        } else if (Elapsed < ExistTime && !GameManager.Instance.IsPaused) {
                            targetLabel.Opacity = Opacity;
                        }
                    }
                }
            }
        }

        public void ChangeText(string text) {
            dfLabel targetLabel = nameLabel as dfLabel;
            if (nameLabel) { targetLabel.Text = text; }
        }

        public void ChangeColor(Color color) {
            dfLabel targetLabel = nameLabel as dfLabel;
            if (nameLabel) { targetLabel.Color = color; }
        }

        public void ChangeSize(float size) {
            dfLabel targetLabel = nameLabel as dfLabel;
            if (nameLabel) { targetLabel.TextScale = size; }
        }

        public void ChangeOffset(Vector3 vector) {
            dfFollowObject targetLabel = nameLabel.GetComponent<dfFollowObject>();
            if (targetLabel && nameLabel) { targetLabel.offset = vector; }
        }

        public void ForceFadeOut(float Length) {
            dfFollowObject targetLabel = nameLabel.GetComponent<dfFollowObject>();
            if (targetLabel && nameLabel) { GameManager.Instance.StartCoroutine(ForceFadeMethod(Length)); }
        }
        
        private IEnumerator ForceFadeMethod(float Length) {
            float ExitFadeForce = Length;
            IsForceFading = true;
            dfLabel targetLabel = (nameLabel as dfLabel);
            if (targetLabel && nameLabel) {
                Elapsed += BraveTime.DeltaTime; {
                    float t = ExitFadeForce / Length;
                    targetLabel.Opacity = t * Opacity;
                    ExitFadeForce -= BraveTime.DeltaTime;
                }
            }
            yield break;
        }

        private void OnDestroy() {
            if (nameLabel && nameLabel.gameObject) { Destroy(nameLabel.gameObject); }
        }
    }*/
}

