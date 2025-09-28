using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandMain;
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
            BannedRooms = BannedRooms = new List<string> {
                "endtimes_chamber",
                "lichroom03"
            };

            MaxChestSpawnsPerFloor = 1;

            m_DoRoomActivations = false;
            m_pickedUp = false;
        }

        public int MaxChestSpawnsPerFloor;

        public List<string> BannedRooms;

        private RoomHandler m_CurrentRoom;

        private bool m_PickedUp;
        private bool m_DoRoomActivations;
        private int m_ChestsSpawnsThisFloor;

        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            ExpandPlaceWallMimic.PlayerHasThirdEye = true;
            Pixelator.Instance.DoOcclusionLayer = false;
            player.OnRoomClearEvent += OnRoomCleared;
            player.OnNewFloorLoaded += OnFloorEntered;
            ExpandDebugCamera.DebugCameraEnabled = true;
            if (GameManager.HasInstance && GameManager.Instance.Dungeon?.data?.rooms != null) {
                foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms) {
                    room.SetRoomActive(true);
                    room.ForcedActiveState = true;
                }
            }
            m_PickedUp = true;
        }
        
        private void OnRoomCleared(PlayerController player) {
            // bool debugMode = false;
            if (/*!debugMode && */m_ChestsSpawnsThisFloor > MaxChestSpawnsPerFloor) return;
            if (m_CurrentRoom != null && m_CurrentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) { return; }
            if (m_CurrentRoom != player.CurrentRoom && (Random.value <= 0.15f/* | debugMode*/)) {
                IntVector2 bestRewardLocation = player.CurrentRoom.GetBestRewardLocation(new IntVector2(2, 1), RoomHandler.RewardLocationStyle.CameraCenter, true);
                GameObject m_EnemyChest = Instantiate(ExpandPrefabs.SurpriseChestObject, bestRewardLocation.ToVector3(), Quaternion.identity);                
                
                if (m_EnemyChest) {
                    ExpandFakeChest enemyChest = m_EnemyChest.GetComponent<ExpandFakeChest>();
                    m_ChestsSpawnsThisFloor++;
                    if (enemyChest) {
                        enemyChest.surpriseChestDoesSpawnAnim = true;
                        enemyChest.ConfigureOnPlacement(player.CurrentRoom);                        
                    }
                }
            }
            m_CurrentRoom = player.CurrentRoom;
        }

        private void OnFloorEntered(PlayerController player) {
            m_ChestsSpawnsThisFloor = 0;
            m_DoRoomActivations = true;
        }


        protected override void Update() {
            if (Dungeon.IsGenerating | (GameManager.Instance && GameManager.Instance.IsLoadingLevel))return;

            if (m_PickedUp && m_DoRoomActivations && GameManager.HasInstance && GameManager.Instance.Dungeon?.data?.rooms != null) {
                m_DoRoomActivations = false;
                foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms) {
                    room.SetRoomActive(true);
                    room.ForcedActiveState = true;
                }
            }
            if (Pixelator.Instance && Pixelator.Instance.DoOcclusionLayer) {
                if (m_owner && m_owner.CurrentRoom != null && !string.IsNullOrEmpty(m_owner.CurrentRoom.GetRoomName()) &&
                   !BannedRooms.Contains(m_owner.CurrentRoom.GetRoomName().ToLower())
                   )
                {   
                    Pixelator.Instance.DoOcclusionLayer = false;
                }
            } else if (Pixelator.Instance && !Pixelator.Instance.DoOcclusionLayer) {
                if (m_owner && m_owner.CurrentRoom != null &&
                   !string.IsNullOrEmpty(m_owner.CurrentRoom.GetRoomName()) && BannedRooms.Contains(m_owner.CurrentRoom.GetRoomName().ToLower())
                   )
                {
                    Pixelator.Instance.DoOcclusionLayer = true;
                }
            }
            if (m_PickedUp && !ExpandDebugCamera.DebugCameraEnabled)ExpandDebugCamera.DebugCameraEnabled = true;
            base.Update();
        }

        public override DebrisObject Drop(PlayerController player) {
            DebrisObject drop = base.Drop(player);
            ExpandPlaceWallMimic.PlayerHasThirdEye = false;
            Pixelator.Instance.DoOcclusionLayer = true;
            m_PickedUp = false;
            m_DoRoomActivations = false;
            if (GameManager.HasInstance && GameManager.Instance.Dungeon?.data?.rooms != null) {
                foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms) room.ForcedActiveState = null;
            }
            ExpandDebugCamera.DebugCameraEnabled = false;
            player.OnRoomClearEvent -= OnRoomCleared;
            player.OnNewFloorLoaded -= OnFloorEntered;
            return drop;
        }
        

        protected override void OnDestroy() {
            if (Pixelator.Instance)Pixelator.Instance.DoOcclusionLayer = true;
            ExpandPlaceWallMimic.PlayerHasThirdEye = false;
            ExpandDebugCamera.DebugCameraEnabled = false;
            m_PickedUp = false;
            base.OnDestroy();
        }
    }    
}

