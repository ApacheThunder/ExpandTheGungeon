using System;
using Dungeonator;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandNoteDoer : DungeonPlaceableBehaviour, IPlayerInteractable {
    
        public ExpandNoteDoer() {
            stringKey = "Insert String Here";
            useAdditionalStrings = false;
            additionalStrings = new string[0];
            isNormalNote = true;
            useItemsTable = false;
            alreadyLocalized = true;
            DestroyedOnFinish = false;
            noteBackgroundType = NoteBackgroundType.WOOD;
        }
    
        public NoteBackgroundType noteBackgroundType;
    
        public string stringKey;
    
        public bool useAdditionalStrings;
    
        public string[] additionalStrings;
    
        public bool isNormalNote;
    
        public bool useItemsTable;
        
        public bool alreadyLocalized;
    
        public Transform textboxSpawnPoint;
    
        private bool m_boxIsExtant;
    
        public bool DestroyedOnFinish;
    
        private string m_selectedDisplayString;
    
        public enum NoteBackgroundType { LETTER, STONE, WOOD, NOTE }
    
        public void Start() {
            if (base.majorBreakable != null) {
                MajorBreakable majorBreakable = base.majorBreakable;
                majorBreakable.OnBreak = (Action)Delegate.Combine(majorBreakable.OnBreak, new Action(OnBroken));
            }
        }
    
        private void OnBroken() {
            GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(transform.position.IntXY(VectorConversions.Floor)).DeregisterInteractable(this);
        }
    
        private void OnDisable() {
            if (m_boxIsExtant) {
                GameUIRoot.Instance.ShowCoreUI(string.Empty);
                m_boxIsExtant = false;
                TextBoxManager.ClearTextBoxImmediate(textboxSpawnPoint);
            }
        }
    
        public float GetDistanceToPoint(Vector2 point) {
            if (!sprite) {
                if (m_boxIsExtant) { ClearBox(); }
                return 1000f;
            }
            Bounds bounds = sprite.GetBounds();
            bounds.SetMinMax(bounds.min + transform.position, bounds.max + transform.position);
            float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
            float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
            return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
        }
    
        public float GetOverrideMaxDistance() { return -1f; }
    
        public void OnEnteredRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }
    
        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            if (m_boxIsExtant) { ClearBox(); }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, true);
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.black, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }
    
        private void ClearBox() {
            GameUIRoot.Instance.ShowCoreUI(string.Empty);
            m_boxIsExtant = false;
            TextBoxManager.ClearTextBox(textboxSpawnPoint);
            if (DestroyedOnFinish) {
                GetAbsoluteParentRoom().DeregisterInteractable(this);
                RoomHandler.unassignedInteractableObjects.Remove(this);
                LootEngine.DoDefaultItemPoof(sprite.WorldCenter, false, false);
                Destroy(gameObject);
            }
        }
    
        public void Interact(PlayerController interactor) {
        if (m_boxIsExtant) {
            ClearBox();
        } else {
            GameUIRoot.Instance.HideCoreUI(string.Empty);
            m_boxIsExtant = true;
            string text = m_selectedDisplayString;
            if (m_selectedDisplayString == null) {
                text = ((!alreadyLocalized) ? ((!useItemsTable) ? StringTableManager.GetLongString(stringKey) : StringTableManager.GetItemsLongDescription(stringKey)) : stringKey);
                if (useAdditionalStrings) {
                    if (isNormalNote) {
                        text = ((!alreadyLocalized) ? ((!useItemsTable) ? StringTableManager.GetLongString(additionalStrings[UnityEngine.Random.Range(0, additionalStrings.Length)]) : StringTableManager.GetItemsLongDescription(additionalStrings[UnityEngine.Random.Range(0, additionalStrings.Length)])) : additionalStrings[UnityEngine.Random.Range(0, additionalStrings.Length)]);
                    } else if (GameStatsManager.Instance.GetFlag(GungeonFlags.SECRET_NOTE_ENCOUNTERED)) {
                        text = ((!alreadyLocalized) ? ((!useItemsTable) ? StringTableManager.GetLongString(additionalStrings[UnityEngine.Random.Range(0, additionalStrings.Length)]) : StringTableManager.GetItemsLongDescription(additionalStrings[UnityEngine.Random.Range(0, additionalStrings.Length)])) : additionalStrings[UnityEngine.Random.Range(0, additionalStrings.Length)]);
                    }
                }
                if (stringKey == "#IRONCOIN_SHORTDESC") { text = " \n" + text + "\n "; }
                m_selectedDisplayString = text;
            }
            switch (noteBackgroundType) {
                case NoteBackgroundType.LETTER:
                    TextBoxManager.ShowLetterBox(textboxSpawnPoint.position, textboxSpawnPoint, -1f, text, true, false);
                    break;
                case NoteBackgroundType.STONE:
                    TextBoxManager.ShowStoneTablet(textboxSpawnPoint.position, textboxSpawnPoint, -1f, text, true, false);
                    break;
                case NoteBackgroundType.WOOD:
                    TextBoxManager.ShowWoodPanel(textboxSpawnPoint.position, textboxSpawnPoint, -1f, text, true, false);
                    break;
                case NoteBackgroundType.NOTE:
                    TextBoxManager.ShowNote(textboxSpawnPoint.position, textboxSpawnPoint, -1f, text, true, false);
                    break;
            }
            if (useAdditionalStrings && !isNormalNote) { GameStatsManager.Instance.SetFlag(GungeonFlags.SECRET_NOTE_ENCOUNTERED, true); }
        }
    }
    
        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }
    
        protected override void OnDestroy() { base.OnDestroy(); }
    
    }
}

