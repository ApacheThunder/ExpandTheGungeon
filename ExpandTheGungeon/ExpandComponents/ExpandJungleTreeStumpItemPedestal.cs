using System;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandJungleTreeStumpItemPedestal : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandJungleTreeStumpItemPedestal() {
            ItemID = 305; // Old Crest
            AddOutline = false;
            BaseOutlineColor = Color.black;
            
            m_Interacted = false;
        }

        public int ItemID;
        public bool AddOutline;
        public Color BaseOutlineColor;

        private bool m_Interacted;

        private GameObject m_SubSpriteObject;
        private tk2dSprite m_ItemSprite;

        private RoomHandler m_ParentRoom;

        public void Start() {
            if (m_SubSpriteObject && m_ParentRoom != null) {
                m_ParentRoom.RegisterInteractable(this);
            } else if (!m_SubSpriteObject) {
                ConfigureOnPlacement(transform.position.GetAbsoluteRoom());
                if (m_SubSpriteObject) {
                    m_ParentRoom.RegisterInteractable(this);
                }
            }
        }
        // private void Awake() { }
        // private void Update() { }

        public void Interact(PlayerController player) {
            if (!m_Interacted && player) { GiveItem(player); }
            return;
        }        

        private void GiveItem(PlayerController player) {
            m_Interacted = true;
            PickupObject itemToGive = PickupObjectDatabase.GetById(ItemID);
            if (itemToGive) {
                LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(ItemID).gameObject, player);
                if (itemToGive is Gun) {
                    AkSoundEngine.PostEvent("Play_OBJ_weapon_pickup_01", gameObject);
                } else {
                    AkSoundEngine.PostEvent("Play_OBJ_item_pickup_01", gameObject);
                }
                GameObject VFXObject = Instantiate((GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Pickup"));
                if (VFXObject) {
                    tk2dSprite component = VFXObject.GetComponent<tk2dSprite>();
                    if (component) {
                        component.PlaceAtPositionByAnchor(m_ItemSprite.WorldCenter, tk2dBaseSprite.Anchor.MiddleCenter);
                        component.HeightOffGround = 6f;
                        component.UpdateZDepth();
                    }
                }
                if (m_ParentRoom != null) { m_ParentRoom.DeregisterInteractable(this); }
                Destroy(m_SubSpriteObject);
                Destroy(this);
            }
        }
        
        public void ConfigureOnPlacement(RoomHandler room) {
            m_ParentRoom = room;

            if (PickupObjectDatabase.GetById(ItemID) && (PickupObjectDatabase.GetById(ItemID).sprite as tk2dSprite) != null) {
                m_SubSpriteObject = new GameObject("Item Display Object", new Type[] {typeof(tk2dSprite)}) { layer = 0 };
                m_SubSpriteObject.transform.position = (transform.position + new Vector3(0.95f, 1));
                m_ItemSprite = m_SubSpriteObject.GetComponent<tk2dSprite>();
                ExpandUtility.DuplicateSprite(m_ItemSprite, (PickupObjectDatabase.GetById(ItemID).sprite as tk2dSprite));
                if (m_ParentRoom != null) { m_SubSpriteObject.transform.parent = m_ParentRoom.hierarchyParent; }
                if (AddOutline) { SpriteOutlineManager.AddOutlineToSprite(m_ItemSprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL); }
            }

            sprite.HeightOffGround = -2;

            if (m_ItemSprite) {
                sprite.AttachRenderer(m_ItemSprite);
                if (AddOutline) { SpriteOutlineManager.AddOutlineToSprite(m_ItemSprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL); }
                m_ItemSprite.HeightOffGround = 0.5f;
                m_ItemSprite.depthUsesTrimmedBounds = true;
                m_ItemSprite.UpdateZDepth();
            }

            sprite.UpdateZDepth();
        }

        public void OnEnteredRange(PlayerController interactor) {
            if (!this) { return; }
            if (m_ItemSprite) {
                if (AddOutline) { SpriteOutlineManager.RemoveOutlineFromSprite(m_ItemSprite, true); }
                SpriteOutlineManager.AddOutlineToSprite(m_ItemSprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            }
            sprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            if (m_ItemSprite) {
                SpriteOutlineManager.RemoveOutlineFromSprite(m_ItemSprite, true);
                if (AddOutline) { SpriteOutlineManager.AddOutlineToSprite(m_ItemSprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL); }
                sprite.UpdateZDepth();
            }
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

