using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class Mimiclay : PlayerItem {

        public static int MimiclayPickupID;

        public static GameObject MimiclayObject;

        public static void Init(AssetBundle expandSharedAssets1) {
            MimiclayObject = expandSharedAssets1.LoadAsset<GameObject>("Mimiclay");
            SpriteSerializer.AddSpriteToObject(MimiclayObject, ExpandPrefabs.EXItemCollection, "ex_mimiclay");
            
            Mimiclay mimiClay = MimiclayObject.AddComponent<Mimiclay>();
            
            string shortDesc = "The Highest Form Of Flattery";
            string longDesc = "Becomes a copy of any item.\n\nMalleable material that formed the mysterious Doppelgunner. After its defeat, it seems oddly content with applying its ability in service of Gungeoneers.";

            ItemBuilder.SetupItem(mimiClay, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(mimiClay, ItemBuilder.CooldownType.Timed, 1);

            mimiClay.consumable = true;
            mimiClay.quality = ItemQuality.SPECIAL;

            MimiclayPickupID = mimiClay.PickupObjectId;
        }

        public override void Pickup(PlayerController player) { base.Pickup(player); }

        protected override void DoEffect(PlayerController user) {
            IPlayerInteractable nearestInteractable = user.CurrentRoom.GetNearestInteractable(user.CenterPosition, 1f, user);
            int pickupID = -1;
            if (nearestInteractable is PassiveItem) {
                pickupID = (nearestInteractable as PassiveItem).PickupObjectId;
            } else if (nearestInteractable is PlayerItem) {
                pickupID = (nearestInteractable as PlayerItem).PickupObjectId;
            }
            if (pickupID != -1) {
                LootEngine.SpawnItem(PickupObjectDatabase.GetById(pickupID).gameObject, user.CenterPosition, Vector2.up, 1f, true, true, false);
            }
        }

        public override bool CanBeUsed(PlayerController user) {
            IPlayerInteractable nearestInteractable = user.CurrentRoom.GetNearestInteractable(user.CenterPosition, 1f, user);
            if (nearestInteractable is PassiveItem) {
                PassiveItem passiveItem = (nearestInteractable as PassiveItem);
                if (passiveItem && passiveItem.PickupObjectId == 531) { return false; }
            }
            return (nearestInteractable is PassiveItem | nearestInteractable is PlayerItem);
        }
    }
}

