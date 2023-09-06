using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using System;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ItemAPI {

    public class ExpandKeyBulletPickup : PassiveItem {

        public static GameObject OldKeyMinimapObject;
        public static GameObject OldKeyObject;
        public static int OldKeyID;

        public static void Init(AssetBundle expandSharedAssets1) {
            OldKeyObject = expandSharedAssets1.LoadAsset<GameObject>("Old Key");

            ExpandKeyBulletPickup oldKey = OldKeyObject.AddComponent<ExpandKeyBulletPickup>();
            SpriteSerializer.AddSpriteToObject(OldKeyObject, ExpandPrefabs.EXItemCollection, "west_key_001");
            ExpandUtility.GenerateOrAddToRigidBody(OldKeyObject, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(7, 18), offset: new IntVector2(2, 1));

            oldKey.specRigidbody.PixelColliders[0].IsTrigger = true;
                        
            OldKeyMinimapObject = expandSharedAssets1.LoadAsset<GameObject>("EX_WestKeyMinimap");
            SpriteSerializer.AddSpriteToObject(OldKeyMinimapObject, ExpandPrefabs.EXItemCollection, "west_key_minimap", tk2dBaseSprite.PerpendicularState.FLAT);

            oldKey.minimapIcon = OldKeyMinimapObject;
            
            string shortDesc = "A mysterious old Key...";
            string longDesc = "It has rusted a little from the beast's stomach acid.\n\nIt unlocks something...";
            
            ItemBuilder.SetupItem(oldKey, shortDesc, longDesc, "ex");
            OldKeyID = oldKey.PickupObjectId;
        }


        public int numberKeyBullets;
        public bool IsOldWestBigKey;
        public string overrideBloopSpriteName;

        private bool m_hasBeenPickedUp;
        private SpeculativeRigidbody m_srb;

        public ExpandKeyBulletPickup() {
            numberKeyBullets = 0;
            IsOldWestBigKey = true;
            IgnoredByRat = true;
            overrideBloopSpriteName = string.Empty;

            quality = ItemQuality.SPECIAL;
            CanBeDropped = false;
            CanBeSold = false;
            
            m_hasBeenPickedUp = false;
        }
        
        private void Start() {
            m_srb = specRigidbody;
            m_srb.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(m_srb.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(OnPreCollision));
            // minimapIcon = OldKeyMinimapObject;
            RegisterMinimapIcon();
            IsBeingSold = true;
        }

        protected override void Update() {
            if (IsOldWestBigKey && !PickedUp && !m_hasBeenPickedUp && !GameManager.Instance.IsLoadingLevel && this && !GameManager.Instance.IsAnyPlayerInRoom(transform.position.GetAbsoluteRoom())) {
                PlayerController bestActivePlayer = GameManager.Instance.BestActivePlayer;
                if (bestActivePlayer && !bestActivePlayer.IsGhost && bestActivePlayer.AcceptingAnyInput) {
                    m_hasBeenPickedUp = true;
                    Pickup(bestActivePlayer);
                }
            }
        }
                
        private void OnPreCollision(SpeculativeRigidbody otherRigidbody, SpeculativeRigidbody source, CollisionData collisionData) {
            if (PickedUp | m_hasBeenPickedUp) { return; }
            PlayerController Player = otherRigidbody.GetComponent<PlayerController>();
            if (Player) {
                SpriteOutlineManager.RemoveOutlineFromSprite(sprite, true);
                m_hasBeenPickedUp = true;
                Pickup(Player);
            }
        }

        public override void Pickup(PlayerController player) {
            if (spriteAnimator) { spriteAnimator.StopAndResetFrame(); }
            player.BloopItemAboveHead(sprite, overrideBloopSpriteName);
            if (!IsOldWestBigKey) { player.carriedConsumables.KeyBullets += numberKeyBullets; }
            AkSoundEngine.PostEvent("Play_OBJ_key_pickup_01", gameObject);
            m_hasBeenPickedUp = true;
            IsBeingSold = false;
            base.Pickup(player);
            // Destroy(gameObject);
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

