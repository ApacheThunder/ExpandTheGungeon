using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.SpriteAPI;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class SonicRing : CurrencyPickup {

        public static int RingID = -1;

        public static GameObject SonicRingObject;

        public static void Init(AssetBundle expandSharedAssets1) {
            
            SonicRingObject = expandSharedAssets1.LoadAsset<GameObject>("EXSonicRing");
            SpriteSerializer.AddSpriteToObject(SonicRingObject, ExpandPrefabs.EXItemCollection, "SonicRing_Idle_05");
            
            List<string> m_RingFrames = new List<string>();

            for (int i = 1; i < 16; i++) {
                if (i < 10) {
                    m_RingFrames.Add("SonicRing_Idle_0" + i.ToString());
                } else {
                    m_RingFrames.Add("SonicRing_Idle_" + i.ToString());
                }
            }

            ExpandUtility.GenerateSpriteAnimator(SonicRingObject, playAutomatically: true);

            ExpandUtility.AddAnimation(SonicRingObject.GetComponent<tk2dSpriteAnimator>(), ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), m_RingFrames, "idle", tk2dSpriteAnimationClip.WrapMode.Loop);
            
            ExpandUtility.GenerateOrAddToRigidBody(SonicRingObject, CollisionLayer.Pickup, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 1), dimensions: new IntVector2(15, 14));

            SonicRingObject.GetComponent<SpeculativeRigidbody>().PixelColliders[0].IsTrigger = true;

            PickupMover pickupMover = SonicRingObject.AddComponent<PickupMover>();
            pickupMover.pathInterval = 0.25f;
            pickupMover.acceleration = 7;
            pickupMover.maxSpeed = 15;
            pickupMover.minRadius = 0;

            SonicRing sonicRing = SonicRingObject.AddComponent<SonicRing>();

            string name = "Sonic Ring";
            string shortDesc = "A Ring";
            string longDesc = "A Simple Ring. Equilivent to one casing.";
            ItemBuilder.SetupEXItem(sonicRing, name, shortDesc, longDesc, "ex", false);
            sonicRing.quality = ItemQuality.COMMON;
            sonicRing.ItemSpansBaseQualityTiers = false;
            sonicRing.additionalMagnificenceModifier = 0;
            sonicRing.ItemRespectsHeartMagnificence = false;
            sonicRing.associatedItemChanceMods = new LootModData[0];
            sonicRing.contentSource = ContentSource.BASE;
            sonicRing.ShouldBeExcludedFromShops = false;
            sonicRing.CanBeDropped = true;
            sonicRing.PreventStartingOwnerFromDropping = false;
            sonicRing.PersistsOnDeath = false;
            sonicRing.PersistsOnPurchase = false;
            sonicRing.RespawnsIfPitfall = true;
            sonicRing.PreventSaveSerialization = false;
            sonicRing.IgnoredByRat = false;
            sonicRing.SaveFlagToSetOnAcquisition = 0;
            sonicRing.UsesCustomCost = false;
            sonicRing.CustomCost = 0;
            sonicRing.CanBeSold = true;
            sonicRing.ForcedPositionInAmmonomicon = -1;
            sonicRing.currencyValue = 1;
            sonicRing.IsMetaCurrency = false;
            sonicRing.overrideBloopSpriteName = "SonicRing_Idle_05";
            
            RingID = sonicRing.PickupObjectId;
        }

        public override void Pickup(PlayerController player) {
            if (player) { AkSoundEngine.PostEvent("Play_EX_SonicRingCollect_01", player.gameObject); }
            base.Pickup(player);
        }
    }
}

