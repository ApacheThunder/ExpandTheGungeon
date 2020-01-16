using System.Collections;
using Dungeonator;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandSellCellManager : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public float SellValueModifier;
        public string ExplodedSellSpriteName;
        public TalkDoerLite SellPitDweller;
        public GameObject SellExplosionVFX;
        public tk2dSprite CellTopSprite;

        private bool m_forceExploded;
        private bool m_isExploded;
        private int m_thingsSold;
        private int m_masteryRoundsSold;
        private bool m_currentlySellingAnItem;        
        private float m_timeHovering;
        private RoomHandler m_parentRoom;


        public ExpandSellCellManager() {
            placeableWidth = 3;
            placeableHeight = 3;
            isPassable = true;
            m_forceExploded = false;
            difficulty = PlaceableDifficulty.BASE;
            SellValueModifier = 0.45f;
            ExplodedSellSpriteName = "sell_creep_gate_open_001";
        }        

        public void ConfigureOnPlacement(RoomHandler room) { m_parentRoom = room; }

        private void Start() {
            SellPitDweller = gameObject.GetComponent<SellCellController>().SellPitDweller;
            CellTopSprite = gameObject.GetComponent<SellCellController>().CellTopSprite;
            SellExplosionVFX = gameObject.GetComponent<SellCellController>().SellExplosionVFX;            
            Destroy(gameObject.GetComponent<SellCellController>());
            if (m_parentRoom == null) { m_parentRoom = GetAbsoluteParentRoom(); }
            if (m_parentRoom != null) { m_parentRoom.RegisterInteractable(SellPitDweller); }
            if (SellPitDweller && SellPitDweller.spriteAnimator) { SellPitDweller.spriteAnimator.alwaysUpdateOffscreen = true; }            
            // if (GameStatsManager.Instance.GetPlayerStatValue(TrackedStats.TIMES_REACHED_NAKATOMI) >= 1f) { StartPreExploded = true; }
        }

        public void CheckForItem(DebrisObject targetDebris) {
            if (m_isExploded) { return; }
            if (!targetDebris | !targetDebris.IsPickupObject | !targetDebris.Static) { return; }
            PickupObject targetItem = targetDebris.GetComponentInChildren<PickupObject>();
            if (targetItem == null) { return; }
            if (!targetItem.CanBeSold) { return; }
            if (targetItem.IsBeingSold) { return; }
            if (targetItem is CurrencyPickup || targetItem is KeyBulletPickup || targetItem is HealthPickup) { return; }
            // if (specRigidbody.ContainsPoint(targetItem.sprite.WorldCenter, 2147483647, true)) { StartCoroutine(HandleSoldItem(targetItem)); } else { return; }
            // Use Magnitude calculation instead. Chaos modes may cause specRigidBody to not operate correctly with original method.
            float magnitude = (targetItem.sprite.WorldCenter - specRigidbody.UnitCenter).magnitude;
            if (magnitude < 1.8f) { StartCoroutine(HandleSoldItem(targetItem)); } else { return; }
        }

        private IEnumerator HandleSoldItem(PickupObject targetItem) {
            if (!targetItem) { m_currentlySellingAnItem = false; yield break; }            
            while (m_currentlySellingAnItem) { yield return null; }
            m_currentlySellingAnItem = true;
            targetItem.IsBeingSold = true;
            float magnitude = (targetItem.sprite.WorldCenter - specRigidbody.UnitCenter).magnitude;            
            if (!targetItem.sprite || magnitude > 1.8f) {
                targetItem.IsBeingSold = false;
                m_currentlySellingAnItem = false;
                yield break;
            }
            IPlayerInteractable ixable = null;
            if (targetItem is PassiveItem) {
                PassiveItem passiveItem = targetItem as PassiveItem;
                passiveItem.GetRidOfMinimapIcon();
                ixable = (targetItem as PassiveItem);
            } else if (targetItem is Gun) {
                Gun gun = targetItem as Gun;
                gun.GetRidOfMinimapIcon();
                ixable = (targetItem as Gun);
            } else if (targetItem is PlayerItem) {
                PlayerItem playerItem = targetItem as PlayerItem;
                playerItem.GetRidOfMinimapIcon();
                ixable = (targetItem as PlayerItem);
            }
            if (ixable != null) {
                RoomHandler.unassignedInteractableObjects.Remove(ixable);
                GameManager.Instance.PrimaryPlayer.RemoveBrokenInteractable(ixable);
            }
            float elapsed = 0f;
            float duration = 0.5f;
            Vector3 startPos = targetItem.transform.position;
            Vector3 finalOffset = Vector3.zero;
            tk2dBaseSprite targetSprite = targetItem.GetComponentInChildren<tk2dBaseSprite>();
            if (targetSprite) { finalOffset = targetSprite.GetBounds().extents; }
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                if (!targetItem || !targetItem.transform) { m_currentlySellingAnItem = false; yield break; }
                targetItem.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.01f, 0.01f, 1f), elapsed / duration);
                targetItem.transform.position = Vector3.Lerp(startPos, startPos + new Vector3(finalOffset.x, 0f, 0f), elapsed / duration);
                yield return null;
            }
            if (!targetItem || !targetItem.transform) { m_currentlySellingAnItem = false; yield break; }
            SellPitDweller.SendPlaymakerEvent("playerSoldSomething");
            int sellPrice = Mathf.Clamp(Mathf.CeilToInt(targetItem.PurchasePrice * SellValueModifier), 0, 200);
            if (targetItem.quality == PickupObject.ItemQuality.SPECIAL || targetItem.quality == PickupObject.ItemQuality.EXCLUDED) { sellPrice = 3; }
            LootEngine.SpawnCurrency(targetItem.sprite.WorldCenter, sellPrice, false);
            m_thingsSold++;
            if (targetItem.PickupObjectId == GlobalItemIds.MasteryToken_Castle || targetItem.PickupObjectId == GlobalItemIds.MasteryToken_Catacombs || targetItem.PickupObjectId == GlobalItemIds.MasteryToken_Gungeon || targetItem.PickupObjectId == GlobalItemIds.MasteryToken_Forge || targetItem.PickupObjectId == GlobalItemIds.MasteryToken_Mines) {
                m_masteryRoundsSold++;
            }
            if (targetItem is Gun && targetItem.GetComponentInParent<DebrisObject>()) {
                Destroy(targetItem.transform.parent.gameObject);
            } else {
                Destroy(targetItem.gameObject);
            }
            if (m_thingsSold >= 3 && m_masteryRoundsSold > 0) { StartCoroutine(HandleSellPitOpening()); }
            m_currentlySellingAnItem = false;
            yield break;
        }

        private void HandleFlightCollider() {
            if (!GameManager.Instance.IsLoadingLevel && m_isExploded) {
                for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                    PlayerController playerController = GameManager.Instance.AllPlayers[i];
                    if (playerController && !playerController.IsGhost && playerController.IsFlying) {
                        Rect rect = new Rect(transform.position.XY(), new Vector2(3f, 3f));
                        if (rect.Contains(playerController.CenterPosition)) {
                            m_timeHovering += BraveTime.DeltaTime;
                            if (m_timeHovering > 2f) { playerController.ForceFall(); m_timeHovering = 0f; }
                        }
                    }
                }
            }
        }

        public void ForcePitOpening() { StartCoroutine(HandleSellPitOpening(true)); }
        
        private IEnumerator HandleSellPitOpening(bool overrideRoomCheck = false) {
            // if (GameManager.Instance.Dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.CATACOMBGEON && !StartPreExploded) { yield break; }
            if (overrideRoomCheck || (m_parentRoom != null && !m_parentRoom.GetRoomName().StartsWith("SubShop_SellCreep_CatacombsSpecial"))) { yield break; }
            m_isExploded = true;
            SellPitDweller.PreventInteraction = true;
            SellPitDweller.PreventCoopInteraction = true;
            SellPitDweller.playerApproachRadius = -1f;
            if (m_parentRoom != null) { m_parentRoom.DeregisterInteractable(SellPitDweller); }
            yield return new WaitForSeconds(3f);
            Instantiate(SellExplosionVFX, transform.position, Quaternion.identity);
            float elapsed = 0f;
            while (elapsed < 0.25f) { elapsed += BraveTime.DeltaTime; yield return null; }
            CellTopSprite.SetSprite(ExplodedSellSpriteName);
            for (int i = 0; i < GetWidth(); i++) {
                for (int j = 0; j < GetHeight(); j++) {
                    IntVector2 intVector = transform.position.IntXY(VectorConversions.Round) + new IntVector2(i, j);
                    if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(intVector)) {
                        CellData cellData = GameManager.Instance.Dungeon.data[intVector];
                        if (overrideRoomCheck && cellData.type != CellType.PIT) { cellData.type = CellType.PIT; }
                        cellData.fallingPrevented = false;
                    }
                }
            }
            if (overrideRoomCheck) { m_forceExploded = true; }
            yield break;
        }

        private void OnDisable() {
            if (m_isExploded && CellTopSprite.CurrentSprite.name != ExplodedSellSpriteName) {
                CellTopSprite.SetSprite(ExplodedSellSpriteName);
                for (int i = 0; i < GetWidth(); i++) {
                    for (int j = 0; j < GetHeight(); j++) {
                        IntVector2 intVector = transform.position.IntXY(VectorConversions.Round) + new IntVector2(i, j);
                        if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(intVector)) {
                            CellData cellData = GameManager.Instance.Dungeon.data[intVector];
                            if (cellData.type != CellType.PIT && m_forceExploded) { cellData.type = CellType.PIT; }
                            cellData.fallingPrevented = false;
                        }
                    }
                }
            }
        }        
        
        private void Update() {
            if (Dungeon.IsGenerating) { return; }
            if (m_isExploded) {
                HandleFlightCollider();
                return;
            }
            bool PlayerIsInRoom = false;
            // Only becomes active when player enters room this component is in   
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                if (m_parentRoom != null && GameManager.Instance.AllPlayers[i].CurrentRoom == m_parentRoom) {
                    PlayerIsInRoom = true;
                    break;
                }
            }
            // DebrisObject class has method for loading original component to do this for us.
            // But this is a new component so we must now do this ourselves (much like how BabyDragunJailController does this)
            if (PlayerIsInRoom && !m_currentlySellingAnItem && !m_isExploded) {
                for (int j = 0; j < StaticReferenceManager.AllDebris.Count; j++) {
                    DebrisObject debrisObject = StaticReferenceManager.AllDebris[j];
                    if (debrisObject) { CheckForItem(debrisObject); }
                }
            }            
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

