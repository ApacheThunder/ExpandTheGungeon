using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class CursedBrick : PassiveItem {

        public static int CursedBrickID = -1;

        public static GameObject CursedBrickObject;

        public static void Init(AssetBundle expandSharedAssets1) {

            CursedBrickObject = expandSharedAssets1.LoadAsset<GameObject>("Cursed Brick");
            SpriteSerializer.AddSpriteToObject(CursedBrickObject, ExpandPrefabs.EXItemCollection, "cursedbrick");
            
            CursedBrick cursedBrick = CursedBrickObject.AddComponent<CursedBrick>();
            
            string shortDesc = "Fragment of a living wall...";
            string longDesc = "There seems to be sounds emanating from the walls around you!\n\nThis item can't be dropped.";
            ItemBuilder.SetupItem(cursedBrick, shortDesc, longDesc, "ex");
            cursedBrick.quality = ItemQuality.D;
            if (!ExpandSettings.EnableEXItems) { cursedBrick.quality = ItemQuality.EXCLUDED; }
            cursedBrick.CanBeDropped = false;
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
            ExpandPlaceWallMimic.PlayerHasWallMimicItem = true;
            base.Pickup(player);
        }
        
        public override DebrisObject Drop(PlayerController player) {
            DebrisObject debrisObject = base.Drop(player);
            
            if (debrisObject) {
                CursedBrick component = debrisObject.GetComponent<CursedBrick>();
                if (component) { component.m_pickedUpThisRun = true; }
            }
            
            if (player) { m_owner = null; }

            ExpandPlaceWallMimic.PlayerHasWallMimicItem = false;

            return debrisObject;
        }

        protected override void OnDestroy() {
            ExpandPlaceWallMimic.PlayerHasWallMimicItem = false;
            base.OnDestroy();
        }

    }
}

