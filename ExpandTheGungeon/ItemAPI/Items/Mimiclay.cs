using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class Mimiclay : PlayerItem {

        public static int MimiclayPickupID;

        public static void Init() {
            string itemName = "Mimiclay";
            string resourceName = "ExpandTheGungeon/Textures/Items/mimiclay";
            GameObject obj = new GameObject(itemName);
            Mimiclay item = obj.AddComponent<Mimiclay>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "The Highest Form Of Flattery";
            string longDesc = "Becomes a copy of any item.\n\nMalleable material that formed the mysterious Doppelgunner. After its defeat, it seems oddly content with applying its ability in service of Gungeoneers.";

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Timed, 1);

            item.consumable = true;
            item.quality = ItemQuality.SPECIAL;

            MimiclayPickupID = item.PickupObjectId;
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

