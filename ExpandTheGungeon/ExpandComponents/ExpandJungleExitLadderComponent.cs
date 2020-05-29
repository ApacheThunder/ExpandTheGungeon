using System;
using System.Collections;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandJungleExitLadderComponent : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandJungleExitLadderComponent() {
            UsesOverrideTargetFloor = false;
            OverrideTargetFloorName = "tt5";
            BaseOutlineColor = Color.black;

            m_Interacted = false;
            
        }

        public bool UsesOverrideTargetFloor;

        public string OverrideTargetFloorName;
        public Color BaseOutlineColor;

        private bool m_Interacted;

        public void Interact(PlayerController player) {
            if (!m_Interacted && player) {
                m_Interacted = true;
                StartCoroutine(HandleExitFloor());
            }
            return;
        }        

        private IEnumerator HandleExitFloor() {
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) { GameManager.Instance.AllPlayers[i].PrepareForSceneTransition(); }
            Pixelator.Instance.FadeToBlack(0.5f, false, 0f);
            yield return new WaitForSeconds(1f);
            GameUIRoot.Instance.HideCoreUI(string.Empty);
            GameUIRoot.Instance.ToggleLowerPanels(false, false, string.Empty);
            yield return null;
            float delay = 0.5f;
            if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.SUPERBOSSRUSH) {
                GameManager.Instance.DelayedLoadBossrushFloor(delay);
            } else if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.BOSSRUSH) {
                GameManager.Instance.DelayedLoadBossrushFloor(delay);
            } else {
                if (!GameManager.Instance.IsFoyer && GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.NONE) {
                    GlobalDungeonData.ValidTilesets nextTileset = GameManager.Instance.GetNextTileset(GameManager.Instance.Dungeon.tileIndices.tilesetId);
                    GameManager.DoMidgameSave(nextTileset);
                }
                if (UsesOverrideTargetFloor) {
                    GameManager.Instance.DelayedLoadCustomLevel(delay, OverrideTargetFloorName);
                } else {
                    GameManager.Instance.DelayedLoadNextLevel(delay);
                }
                AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
            }
            yield break;
        }

        private void Awake() {
            SpriteOutlineManager.AddOutlineToSprite(sprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.HeightOffGround = -2;
            sprite.UpdateZDepth();
        }

        // public void Start() { }
        // private void Update() { }

        public void ConfigureOnPlacement(RoomHandler room) { }
        
        public void OnEnteredRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            SpriteOutlineManager.AddOutlineToSprite(sprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }

        public float GetDistanceToPoint(Vector2 point) {
            Bounds bounds = sprite.GetBounds();
            bounds.SetMinMax(bounds.min + transform.position, bounds.max + transform.position);
            float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
            float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
            return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
        }

        public float GetOverrideMaxDistance() { return -1f; }
        
        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

