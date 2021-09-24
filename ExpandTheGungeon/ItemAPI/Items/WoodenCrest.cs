using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class WoodenCrest : PassiveItem {

        public static int WoodCrestID = -1;

        public static GameObject WoodCrestObject;

        public static void Init(AssetBundle expandSharedAssets1) {
            
            WoodCrestObject = expandSharedAssets1.LoadAsset<GameObject>("Wooden Crest");
            SpriteSerializer.AddSpriteToObject(WoodCrestObject, ExpandPrefabs.EXItemCollection, "junglecrest");

            WoodenCrest woodenCrest = WoodCrestObject.AddComponent<WoodenCrest>();
            
            string shortDesc = "Protection of Wood";
            string longDesc = "A shield made of wood. Provides fleeting protection.";
            ItemBuilder.SetupItem(woodenCrest, shortDesc, longDesc, "ex");
            // woodenCrest.quality = ItemQuality.SPECIAL;
            woodenCrest.quality = ItemQuality.EXCLUDED;
            woodenCrest.ItemSpansBaseQualityTiers = false;
            woodenCrest.additionalMagnificenceModifier = 0;
            woodenCrest.ItemRespectsHeartMagnificence = true;
            woodenCrest.associatedItemChanceMods = new LootModData[0];
            woodenCrest.contentSource = ContentSource.BASE;
            woodenCrest.ShouldBeExcludedFromShops = false;
            woodenCrest.CanBeDropped = false;
            woodenCrest.PreventStartingOwnerFromDropping = false;
            woodenCrest.PersistsOnDeath = false;
            woodenCrest.PersistsOnPurchase = false;
            woodenCrest.RespawnsIfPitfall = false;
            woodenCrest.PreventSaveSerialization = false;
            woodenCrest.IgnoredByRat = false;
            woodenCrest.SaveFlagToSetOnAcquisition = 0;
            woodenCrest.UsesCustomCost = false;
            woodenCrest.CustomCost = 65;
            woodenCrest.CanBeSold = false;
            woodenCrest.passiveStatModifiers = new StatModifier[0];
            woodenCrest.ArmorToGainOnInitialPickup = 0;

            WoodCrestID = woodenCrest.PickupObjectId;
        }

        public void Break() {
            m_pickedUp = true;
            Destroy(gameObject, 1f);
        }
        public override void Pickup(PlayerController player) {
            if (m_pickedUp) { return; }
            base.Pickup(player);
            // player.healthHaver.HasCrest = true;
            player.OnReceivedDamage += PlayerDamaged;
            player.healthHaver.Armor += 1f;
        }

        private void PlayerDamaged(PlayerController obj) {
            // obj.healthHaver.HasCrest = false;
            // obj.RemovePassiveItem(PickupObjectId);
            obj.DropPassiveItem(this);
        }

        public override DebrisObject Drop(PlayerController player) {
            DebrisObject debrisObject = base.Drop(player);
            player.healthHaver.HasCrest = false;
            player.OnReceivedDamage -= PlayerDamaged;
            
            if (debrisObject) {
                WoodenCrest component = debrisObject.GetComponent<WoodenCrest>();
                if (component) {
                    component.m_pickedUpThisRun = true;
                    component.Break();
                }
            }
            
            return debrisObject;
        }

        protected override void OnDestroy() {
            if (m_pickedUp && GameManager.HasInstance && Owner) {
                // Owner.healthHaver.HasCrest = false;
                Owner.OnReceivedDamage -= PlayerDamaged;
            }
            base.OnDestroy();
        }

    }
}

