using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandMain;
using Dungeonator;
using ExpandTheGungeon.ExpandComponents;

namespace ExpandTheGungeon.ItemAPI {
    
    public class ThirdEye : PassiveItem {

        public static GameObject ThirdEyeObject;
        public static int ThirdEyeID;

        public static void Init(AssetBundle expandSharedAssets1) {            
            ThirdEyeObject = expandSharedAssets1.LoadAsset<GameObject>("EXThirdEye");
            SpriteSerializer.AddSpriteToObject(ThirdEyeObject, ExpandPrefabs.EXItemCollection, "thethirdeye");

            ThirdEye thirdEye = ThirdEyeObject.AddComponent<ThirdEye>();
            ThirdEyeObject.name = "The Third Eye";
            string shortDesc = "Peering beyond the vail...";
            string longDesc = "There was a once a man who tried to peer across the vail of the Gungeon to find new treasure but it drove him to madness. This item is said to be made from his third eye which is all that remains of him.";
            ItemBuilder.SetupItem(thirdEye, shortDesc, longDesc, "ex");

            thirdEye.passiveStatModifiers = new StatModifier[] {
                new StatModifier() {
                    statToBoost = PlayerStats.StatType.Curse,
                    amount = 1,
                    modifyType = StatModifier.ModifyMethod.ADDITIVE,
                    isMeatBunBuff = false
                }
            };

            thirdEye.quality = ItemQuality.D;
            if (!ExpandSettings.EnableEXItems) { thirdEye.quality = ItemQuality.EXCLUDED; }
            ThirdEyeID = thirdEye.PickupObjectId;
        }

        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            ExpandPlaceWallMimic.PlayerHasThirdEye = true;
            Pixelator.Instance.DoOcclusionLayer = false;
            player.OnRoomClearEvent += OnRoomCleared;            
        }
        
        private void OnRoomCleared(PlayerController player) {
            bool debugMode = false;
            if (Random.value <= 0.2f | debugMode) {
                IntVector2 bestRewardLocation = player.CurrentRoom.GetBestRewardLocation(new IntVector2(2, 1), RoomHandler.RewardLocationStyle.CameraCenter, true);
                GameObject m_EnemyChest = Instantiate(ExpandPrefabs.SurpriseChestObject, bestRewardLocation.ToVector3(), Quaternion.identity);                
                
                if (m_EnemyChest) {
                    ExpandFakeChest enemyChest = m_EnemyChest.GetComponent<ExpandFakeChest>();
                    if (enemyChest) {
                        enemyChest.surpriseChestDoesSpawnAnim = true;
                        enemyChest.ConfigureOnPlacement(player.CurrentRoom);                        
                    }
                }
            }
        }

        public override DebrisObject Drop(PlayerController player) {
            DebrisObject drop = base.Drop(player);
            ExpandPlaceWallMimic.PlayerHasThirdEye = false;
            Pixelator.Instance.DoOcclusionLayer = true;
            player.OnRoomClearEvent -= OnRoomCleared;
            return drop;
        }
        

        protected override void OnDestroy() {
            if (Pixelator.Instance) { Pixelator.Instance.DoOcclusionLayer = true; }
            ExpandPlaceWallMimic.PlayerHasThirdEye = false;
            base.OnDestroy();
        }
    }    
}

