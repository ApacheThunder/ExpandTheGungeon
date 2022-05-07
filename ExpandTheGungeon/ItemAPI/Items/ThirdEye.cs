using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandMain;
using Dungeonator;
using ExpandTheGungeon.ExpandComponents;
using System.Collections.Generic;

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
        

        public ThirdEye() {
            m_BannedRooms = m_BannedRooms = new List<string> {
                "endtimes_chamber",
                "lichroom03"
            };
            m_pickedUp = false;
        }

        private List<string> m_BannedRooms;

        private RoomHandler m_CurrentRoom;

        private bool m_PickedUp;

        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            ExpandPlaceWallMimic.PlayerHasThirdEye = true;
            Pixelator.Instance.DoOcclusionLayer = false;
            player.OnRoomClearEvent += OnRoomCleared;
            m_PickedUp = true;
        }
        
        private void OnRoomCleared(PlayerController player) {
            bool debugMode = false;
            if (m_CurrentRoom != null && m_CurrentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) { return; }
            if (m_CurrentRoom != player.CurrentRoom && (Random.value <= 0.15f | debugMode)) {
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
            m_CurrentRoom = player.CurrentRoom;
        }

        protected override void Update() {
            if (m_PickedUp) { foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms) { room.SetRoomActive(true); } }
            if (Pixelator.Instance && Pixelator.Instance.DoOcclusionLayer) {
                if (m_owner && m_owner.CurrentRoom != null && !string.IsNullOrEmpty(m_owner.CurrentRoom.GetRoomName()) &&
                   !m_BannedRooms.Contains(m_owner.CurrentRoom.GetRoomName().ToLower())
                   )
                {   
                    Pixelator.Instance.DoOcclusionLayer = false;
                }
            } else if (Pixelator.Instance && !Pixelator.Instance.DoOcclusionLayer) {
                if (m_owner && m_owner.CurrentRoom != null &&
                   !string.IsNullOrEmpty(m_owner.CurrentRoom.GetRoomName()) && m_BannedRooms.Contains(m_owner.CurrentRoom.GetRoomName().ToLower())
                   )
                {
                    Pixelator.Instance.DoOcclusionLayer = true;
                }
            }
            base.Update();
        }

        public override DebrisObject Drop(PlayerController player) {
            DebrisObject drop = base.Drop(player);
            ExpandPlaceWallMimic.PlayerHasThirdEye = false;
            Pixelator.Instance.DoOcclusionLayer = true;
            m_PickedUp = false;
            foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms) {
                if (player.CurrentRoom != null && player.CurrentRoom != room && !player.CurrentRoom.connectedRooms.Contains(room))
                room.SetRoomActive(false);
            }
            player.OnRoomClearEvent -= OnRoomCleared;
            return drop;
        }
        

        protected override void OnDestroy() {
            if (Pixelator.Instance) { Pixelator.Instance.DoOcclusionLayer = true; }
            ExpandPlaceWallMimic.PlayerHasThirdEye = false;
            m_PickedUp = false;
            base.OnDestroy();
        }
    }    
}

