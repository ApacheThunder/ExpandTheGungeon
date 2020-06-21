using System;
using UnityEngine;
using Dungeonator;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandRewardPedestal : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandRewardPedestal() {
            ItemID = 224; // blank
            AddOutline = false;
            DoSpawnVFX = false;
            BaseOutlineColor = Color.black;
            
            m_Interacted = false;
            
        }

        public int ItemID;
        public bool AddOutline;
        public bool DoSpawnVFX;
        public Color BaseOutlineColor;

        public Transform spawnTransform;

        [NonSerialized]
        public PickupObject contents;

        private tk2dBaseSprite m_itemDisplaySprite;
        private GameObject minimapIconInstance;
        private RoomHandler m_ParentRoom;

        private bool m_Interacted;

        public void Start() {
            if (m_itemDisplaySprite && m_ParentRoom!= null) {
                m_ParentRoom.RegisterInteractable(this);
                RegisterChestOnMinimap(m_ParentRoom);
            } else if (!m_itemDisplaySprite) {
                ConfigureOnPlacement(transform.position.GetAbsoluteRoom());
                if (m_itemDisplaySprite) {
                    m_ParentRoom.RegisterInteractable(this);
                    RegisterChestOnMinimap(m_ParentRoom);
                }
            }
        }

        // private void Awake() { }
        // private void Update() { }
        // private void OnEnable() { }

        public void RegisterChestOnMinimap(RoomHandler r) {
            minimapIconInstance = Minimap.Instance.RegisterRoomIcon(r, BraveResources.Load<GameObject>("Global Prefabs/Minimap_Treasure_Icon", ".prefab"), false);
        }

        public void Interact(PlayerController player) {
            if (!m_Interacted && player) { GiveItem(player); }
            return;
        }        

        private void GiveItem(PlayerController player) {
            m_Interacted = true;
            if (minimapIconInstance != null) { Minimap.Instance.DeregisterRoomIcon(m_ParentRoom, minimapIconInstance); }
            if (contents) {
                LootEngine.GivePrefabToPlayer(contents.gameObject, player);
                if (contents is Gun) {
                    AkSoundEngine.PostEvent("Play_OBJ_weapon_pickup_01", gameObject);
                } else {
                    AkSoundEngine.PostEvent("Play_OBJ_item_pickup_01", gameObject);
                }
                GameObject VFXObject = Instantiate((GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Pickup"));
                if (VFXObject) {
                    tk2dSprite component = VFXObject.GetComponent<tk2dSprite>();
                    if (component) {
                        component.PlaceAtPositionByAnchor(m_itemDisplaySprite.WorldCenter, tk2dBaseSprite.Anchor.MiddleCenter);
                        component.HeightOffGround = 6f;
                        component.UpdateZDepth();
                    }
                }
            }
            if (m_ParentRoom != null) { m_ParentRoom.DeregisterInteractable(this); }
            Destroy(m_itemDisplaySprite);
            Destroy(spawnTransform.gameObject);
            Destroy(this);
        }
        
        public void ConfigureOnPlacement(RoomHandler room) {
            m_ParentRoom = room;

            if (!m_itemDisplaySprite) {

                if (!contents) { contents = PickupObjectDatabase.GetById(ItemID); }
                if (!contents) { return; }

                m_itemDisplaySprite = tk2dSprite.AddComponent(new GameObject("Display Sprite") { transform = { parent = spawnTransform } }, contents.sprite.Collection, contents.sprite.spriteId);
                if (AddOutline) { SpriteOutlineManager.AddOutlineToSprite(m_itemDisplaySprite, Color.black, 0.1f, 0.05f, SpriteOutlineManager.OutlineType.NORMAL); }
                sprite.AttachRenderer(m_itemDisplaySprite);
                m_itemDisplaySprite.HeightOffGround = 0.25f;
                m_itemDisplaySprite.depthUsesTrimmedBounds = true;
                m_itemDisplaySprite.PlaceAtPositionByAnchor(spawnTransform.position, tk2dBaseSprite.Anchor.LowerCenter);
                m_itemDisplaySprite.transform.position = m_itemDisplaySprite.transform.position.Quantize(0.0625f);
                if (DoSpawnVFX) {
                    GameObject gameObject = Instantiate((GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Spawn_Poof"));
                    tk2dBaseSprite component = gameObject.GetComponent<tk2dBaseSprite>();
                    component.PlaceAtPositionByAnchor(m_itemDisplaySprite.WorldCenter.ToVector3ZUp(0f), tk2dBaseSprite.Anchor.MiddleCenter);
                    component.HeightOffGround = 5f;
                    component.UpdateZDepth();
                }
                sprite.UpdateZDepth();
            }
        }

        public void OnEnteredRange(PlayerController interactor) {
            if (!this) { return; }
            if (m_itemDisplaySprite) {
                if (AddOutline) { SpriteOutlineManager.RemoveOutlineFromSprite(m_itemDisplaySprite, true); }
                SpriteOutlineManager.AddOutlineToSprite(m_itemDisplaySprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            }
            sprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            if (m_itemDisplaySprite) {
                SpriteOutlineManager.RemoveOutlineFromSprite(m_itemDisplaySprite, true);
                if (AddOutline) { SpriteOutlineManager.AddOutlineToSprite(m_itemDisplaySprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL); }
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

