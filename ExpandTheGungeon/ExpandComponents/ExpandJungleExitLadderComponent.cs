using System;
using System.Collections;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandJungleExitLadderComponent : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandJungleExitLadderComponent() {
            IsLevelExitWarp = false;
            IsDestinationWarp = false;
            UsesOverrideTargetFloor = false;
            OverrideTargetFloorName = "tt5";
            TargetRoomTileset = "Base_Castle";

            TargetPointInRoom = new Vector2(2, 4);
            BaseOutlineColor = Color.black;

            m_Interacted = false;
            m_Configured = false;       
        }

        public bool IsLevelExitWarp;
        public bool IsDestinationWarp;
        public bool UsesOverrideTargetFloor;

        public string OverrideTargetFloorName;
        public string TargetRoomTileset;

        public PrototypeDungeonRoom TargetRoomPrefab;
        public RoomHandler TargetRoom;
        
        public Vector2 TargetPointInRoom;
        public Vector2? PreviousLadderLocation;
        public Color BaseOutlineColor;

        private bool m_Interacted;
        private bool m_Configured;

        private RoomHandler m_ParentRoom;
        
        private void Update() {
            if (!m_Configured) { return; }

            SpriteOutlineManager.AddOutlineToSprite(sprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);

            if (IsDestinationWarp) {
                sprite.HeightOffGround = -4;
                sprite.UpdateZDepth();
            } else {
                sprite.HeightOffGround = -2;
                sprite.UpdateZDepth();
            }

            m_Configured = false;
        }

        private void Awake() { }

        public void ConfigureOnPlacement(RoomHandler room) {
            m_ParentRoom = room;
            m_Configured = true;
        }

        public void Interact(PlayerController player) {
            if (!m_Interacted && player) {
                m_Interacted = true;

                if (IsLevelExitWarp) {
                    StartCoroutine(HandleExitFloor());
                    return;
                }
                
                if (TargetRoom != null) {
                    if (PreviousLadderLocation.HasValue) { TargetPointInRoom = PreviousLadderLocation.Value; }
                    StartCoroutine(HandleWarp(player, TargetRoom, TargetPointInRoom));
                    m_Interacted = false;
                    return;
                }

                if (TargetRoomPrefab == null) { TargetRoomPrefab = ExpandRoomPrefabs.Expand_ExitRoom_NewElevator; }

                Dungeon dungeon2 = DungeonDatabase.GetOrLoadByName(TargetRoomTileset);
                TargetRoom = ExpandUtility.AddCustomRuntimeRoomWithTileSet(dungeon2, TargetRoomPrefab, true, false);
                dungeon2 = null;
                
                if (TargetRoom == null) {
                    StartCoroutine(HandleExitFloor());
                    return;
                }

                Instantiate(ExpandPrefabs.Jungle_ExitLadder_Hole, TargetRoom.area.basePosition.ToVector3() + TargetPointInRoom.ToVector3ZUp() - new Vector2(0, 1).ToVector3ZUp(), Quaternion.identity);
                
                GameObject m_DestinationLadder = DungeonPlaceableUtility.InstantiateDungeonPlaceable(ExpandPrefabs.Jungle_ExitLadder_Destination, TargetRoom, TargetPointInRoom.ToIntVector2() - new IntVector2(0, 1), true);
                m_DestinationLadder.transform.position -= new Vector3(0, 0.2f);

                ExpandJungleExitLadderComponent m_DestinationLadderController = m_DestinationLadder.GetComponent<ExpandJungleExitLadderComponent>();
                m_DestinationLadderController.IsDestinationWarp = true;
                m_DestinationLadderController.TargetRoom = m_ParentRoom;
                m_DestinationLadderController.PreviousLadderLocation = new Vector2(4, 6.8f);
                m_DestinationLadderController.ConfigureOnPlacement(TargetRoom);
                
                TargetRoom.RegisterInteractable(m_DestinationLadderController);

                TargetPointInRoom += new Vector2(0.45f, 1.5f);

                StartCoroutine(HandleWarp(player, TargetRoom, TargetPointInRoom));
                m_Interacted = false;
            }
            return;
        }        
        
        private IEnumerator HandleWarp(PlayerController player, RoomHandler targetRoomn, Vector2 TargetLocation) {
            Pixelator.Instance.FadeToBlack(0.1f, false, 0f);
            PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(player);
            yield return new WaitForSeconds(0.1f);
            Vector2 targetPoint = (targetRoomn.area.basePosition.ToVector2() + TargetLocation);
            player.WarpToPoint(targetPoint, false, false);
            if (otherPlayer && otherPlayer.healthHaver.IsAlive) { otherPlayer.ReuniteWithOtherPlayer(player, false); }
            GameManager.Instance.MainCameraController.ForceToPlayerPosition(player);
            Pixelator.Instance.FadeToBlack(0.1f, true, 0f);
            yield return new WaitForSeconds(1f);
            player.ClearInputOverride("arbitrary warp");
            m_Interacted = false;
            yield break;
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

