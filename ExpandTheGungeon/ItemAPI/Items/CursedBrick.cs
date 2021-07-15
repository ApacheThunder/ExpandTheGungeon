using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class CursedBrick : PassiveItem {

        public static int CursedBrickID = -1;

        public static GameObject CursedBrickObject;

        public static void Init(AssetBundle expandSharedAssets1) {

            CursedBrickObject = expandSharedAssets1.LoadAsset<GameObject>("Cursed Brick");
            ItemBuilder.AddSpriteToObject(CursedBrickObject, expandSharedAssets1.LoadAsset<Texture2D>("cursedbrick"), false, false);

            CursedBrick cursedBrick = CursedBrickObject.AddComponent<CursedBrick>();
            
            string shortDesc = "Fragment of a living wall...";
            string longDesc = "There seems to be sounds emanating from the walls around you!";
            ItemBuilder.SetupItem(cursedBrick, shortDesc, longDesc, "ex");
            cursedBrick.quality = ItemQuality.D;
            cursedBrick.ItemSpansBaseQualityTiers = false;
            cursedBrick.additionalMagnificenceModifier = 0;
            cursedBrick.ItemRespectsHeartMagnificence = true;
            cursedBrick.associatedItemChanceMods = new LootModData[0];
            cursedBrick.contentSource = ContentSource.BASE;
            cursedBrick.ShouldBeExcludedFromShops = false;
            cursedBrick.CanBeDropped = false;
            cursedBrick.PreventStartingOwnerFromDropping = false;
            cursedBrick.PersistsOnDeath = false;
            cursedBrick.PersistsOnPurchase = false;
            cursedBrick.RespawnsIfPitfall = false;
            cursedBrick.PreventSaveSerialization = false;
            cursedBrick.IgnoredByRat = false;
            cursedBrick.SaveFlagToSetOnAcquisition = 0;
            cursedBrick.UsesCustomCost = false;
            cursedBrick.CustomCost = 65;
            cursedBrick.CanBeSold = false;
            cursedBrick.passiveStatModifiers = new StatModifier[0];
            cursedBrick.ArmorToGainOnInitialPickup = 0;

            cursedBrick.passiveStatModifiers = new StatModifier[] {
                new StatModifier() {
                    statToBoost = PlayerStats.StatType.Curse,
                    amount = 1,
                    modifyType = StatModifier.ModifyMethod.ADDITIVE,
                    isMeatBunBuff = false
                }
            };

            CursedBrickID = cursedBrick.PickupObjectId;
        }

        public override void Pickup(PlayerController player) {
            if (m_pickedUp) { return; }
            m_owner = player;
            base.Pickup(player);
        }
        
        public override DebrisObject Drop(PlayerController player) {
            DebrisObject debrisObject = base.Drop(player);
            
            if (debrisObject) {
                CursedBrick component = debrisObject.GetComponent<CursedBrick>();
                if (component) { component.m_pickedUpThisRun = true; }
            }
            
            if (player) { m_owner = null; }

            return debrisObject;
        }

        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

