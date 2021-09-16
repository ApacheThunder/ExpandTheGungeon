using Dungeonator;
using System;
using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class TableTechExpand : PassiveItem {

        public static void Init() {
            string itemName = "Table Tech Expand";
            string resourceName = "ExpandTheGungeon/Textures/Items/tabletech_assassin";
            GameObject itemObj = new GameObject(itemName);
            TableTechExpand item = itemObj.AddComponent<TableTechExpand>();
            ItemBuilder.AddSpriteToObject(itemObj, ExpandAssets.LoadAsset<Texture2D>(resourceName), false, false);

            string shortDesc = "Expand The Table";
            string longDesc = "This forbidden technique causes flipped tables to increase exponentially in size.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ex");
            item.quality = ItemQuality.EXCLUDED;
        }
        

        public TableTechExpand() {
            TableScaleAmount = 2.5f;
            TableExpandSpeed = 1;            
        }
        
        public float TableScaleAmount;
        public float TableExpandSpeed;

        public override void Pickup(PlayerController player) {
            if (m_pickedUp) { return; }
            m_owner = player;
            base.Pickup(player);
            // player.OnTableFlipped = (Action<FlippableCover>)Delegate.Combine(player.OnTableFlipped, new Action<FlippableCover>(DoEffect));
            player.OnTableFlipCompleted = (Action<FlippableCover>)Delegate.Combine(player.OnTableFlipCompleted, new Action<FlippableCover>(DoEffectCompleted));
        }

        // private void DoEffect(FlippableCover obj) { }

        private void DoEffectCompleted(FlippableCover obj) {

            if (!obj.gameObject.name.ToLower().Contains("folding")) { StartCoroutine(HandleExpand(obj)); }
        }
                
        private IEnumerator HandleExpand(FlippableCover target) {
            string TableName = target.gameObject.name;
            yield return new WaitForSeconds(0.15f);
            if (target && target.specRigidbody) {
                AkSoundEngine.PostEvent("Play_WPN_woodbeam_extend_01", gameObject);
                target.specRigidbody.CanBePushed = false;
                // Vector3 SpriteOffset = target.transform.position;
                DungeonData.Direction DirectionFlipped = target.DirectionFlipped;                
                // SpriteOffset -= new Vector3(1.4f, 1.6f, 0);
                // target.transform.position = SpriteOffset;
                yield return null;
                // if (target.specRigidbody) { target.specRigidbody.CanBePushed = false; }
                float elapsed = 0f;
                Vector2 scaleAmount = new Vector2(TableScaleAmount, TableScaleAmount);
                if (!target) { yield break; }
                Vector2 startScale = target.transform.localScale;
                while (elapsed < TableExpandSpeed) {
                    if (!target) { yield break; }
                    elapsed += BraveTime.DeltaTime;
                    target.transform.localScale = Vector2.Lerp(startScale, scaleAmount, (elapsed * TableExpandSpeed));
                    /*if (DirectionFlipped == DungeonData.Direction.EAST && TableName.ToLower().Contains("horizontal")) {
                        target.transform.position -= new Vector3(0.04f, 0.02f, 0);
                    } else {
                        target.transform.position -= new Vector3(0.02f, 0.02f, 0);
                    }*/
                    if (target.sprite) { target.sprite.UpdateZDepth(); }
                    if (target.specRigidbody) {
                        target.specRigidbody.UpdateCollidersOnRotation = true;
                        target.specRigidbody.UpdateCollidersOnScale = true;
                        target.specRigidbody.RegenerateColliders = true;
                        // target.specRigidbody.UpdateColliderPositions();
                        target.specRigidbody.Reinitialize();
                    }
                    yield return null;
                }
            }
            yield break;
        }

        /*private void HandleProjectileEffect(FlippableCover table) {
            GameObject original = (GameObject)ResourceCache.Acquire("Global VFX/VFX_Table_Exhaust");
            Vector2 vector = DungeonData.GetIntVector2FromDirection(table.DirectionFlipped).ToVector2();
            float z = BraveMathCollege.Atan2Degrees(vector);
            Vector3 zero = Vector3.zero;
            switch (table.DirectionFlipped)
            {
                case DungeonData.Direction.NORTH:
                    zero = Vector3.zero;
                    break;
                case DungeonData.Direction.EAST:
                    zero = new Vector3(-0.5f, 0.25f, 0f);
                    break;
                case DungeonData.Direction.SOUTH:
                    zero = new Vector3(0f, 0.5f, 1f);
                    break;
                case DungeonData.Direction.WEST:
                    zero = new Vector3(0.5f, 0.25f, 0f);
                    break;
            }
        }*/
        
        public override DebrisObject Drop(PlayerController player) {
            DebrisObject debrisObject = base.Drop(player);
            debrisObject.GetComponent<TableTechExpand>().m_pickedUpThisRun = true;
            if (player) {
                // player.OnTableFlipped = (Action<FlippableCover>)Delegate.Remove(player.OnTableFlipped, new Action<FlippableCover>(DoEffect));
                player.OnTableFlipCompleted = (Action<FlippableCover>)Delegate.Remove(player.OnTableFlipCompleted, new Action<FlippableCover>(DoEffectCompleted));
            }
            m_owner = null;
            return debrisObject;
        }

        protected override void OnDestroy() {
            if (Owner) {
                PlayerController owner = Owner;
                // owner.OnTableFlipped = (Action<FlippableCover>)Delegate.Remove(owner.OnTableFlipped, new Action<FlippableCover>(DoEffect));
                owner.OnTableFlipCompleted = (Action<FlippableCover>)Delegate.Remove(owner.OnTableFlipCompleted, new Action<FlippableCover>(DoEffectCompleted));
            }
            base.OnDestroy();
        }
    }
}

