using Dungeonator;
using UnityEngine;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandSpawnItemOnBreak : BraveBehaviour {

        public ExpandSpawnItemOnBreak() {
            // Setup Jungle Tree Defaults
            CommonLoot = new List<int>() { 70, 68, 73, 565 };
            RareLoot = new List<int>() { 74, 85, 120, 600, 78, 224, 67, 297 };

            BannedRoomCategories = new List<PrototypeDungeonRoom.RoomCategory>();
            AllowedTilesets = new List<GlobalDungeonData.ValidTilesets>() {
                 GlobalDungeonData.ValidTilesets.JUNGLEGEON
            };

            BreakOnEnemyCollision = true;

            LootOdds = 0.1f;
            RareLootOdds = 0.08f;
        }

        public bool BreakOnEnemyCollision;
        public float LootOdds;
        public float RareLootOdds;
        public int ItemCount;

        public List<int> CommonLoot;
        public List<int> RareLoot;

        public List<PrototypeDungeonRoom.RoomCategory> BannedRoomCategories;
        public List<GlobalDungeonData.ValidTilesets> AllowedTilesets;


        private RoomHandler m_ParentRoom;
        
        public void Start() {
            m_ParentRoom = gameObject.transform.position.GetAbsoluteRoom();

            if (!majorBreakable) {
                BreakOnEnemyCollision = false;
                Destroy(this);
                return;
            }

            if (AllowedTilesets.Count > 0) {
                if (!AllowedTilesets.Contains(GameManager.Instance.Dungeon.tileIndices.tilesetId)) {
                    BreakOnEnemyCollision = false;
                    Destroy(this);
                    return;
                }
            }

            if (BannedRoomCategories.Count > 0) {
                if (m_ParentRoom != null && BannedRoomCategories.Contains(m_ParentRoom.area.PrototypeRoomCategory)) {
                    BreakOnEnemyCollision = false;
                    Destroy(this);
                    return;
                }
            }

            if (m_ParentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS)BreakOnEnemyCollision = false;

            if (specRigidbody && BreakOnEnemyCollision) {
                specRigidbody.OnPreRigidbodyCollision += OnPreRigidBodyCollision;
            } else if (!specRigidbody) {
                BreakOnEnemyCollision = false;
            }

            if (CommonLoot.Count < 1 && RareLoot.Count < 1)return;

            if (CommonLoot.Count > 1)CommonLoot = CommonLoot.Shuffle();
            if (RareLoot.Count > 1)RareLoot = RareLoot.Shuffle();

            if (Random.value > LootOdds) return;

            majorBreakable.SpawnItemOnBreak = true;

            if (Random.value < RareLootOdds) {
                majorBreakable.ItemIdToSpawnOnBreak = BraveUtility.RandomElement(RareLoot);
            } else {
                majorBreakable.ItemIdToSpawnOnBreak = BraveUtility.RandomElement(CommonLoot);
            }
        }

        public void OnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (BreakOnEnemyCollision && majorBreakable && otherRigidbody.GetComponent<AIActor>()) {
                if (!otherRigidbody.GetComponent<AIActor>().IgnoreForRoomClear && !otherRigidbody.GetComponent<CompanionController>()) {
                    BreakOnEnemyCollision = false;
                    majorBreakable.SpawnItemOnBreak = false;
                    majorBreakable.ItemIdToSpawnOnBreak = -1;
                    majorBreakable.Break(otherRigidbody.Velocity);
                    Destroy(this);
                }
            }
        }

        protected override void OnDestroy() {
            if (BreakOnEnemyCollision && specRigidbody)specRigidbody.OnPreRigidbodyCollision -= OnPreRigidBodyCollision;
            base.OnDestroy();
        }
    }
}

