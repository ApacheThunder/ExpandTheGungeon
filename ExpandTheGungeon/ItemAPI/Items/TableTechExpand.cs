using Dungeonator;
using ExpandTheGungeon.ExpandUtilities;
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
            ItemBuilder.AddSpriteToObject(itemName, resourceName, itemObj);
                        
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
            if (target.sprite) {
                AkSoundEngine.PostEvent("Play_WPN_woodbeam_extend_01", gameObject);
                GameObject dummyTable = new GameObject(("Big " + target.gameObject.name)) { layer = LayerMask.NameToLayer("Unpixelated") };
                GameObject dummyTableShadow = null;
                tk2dSprite dummyTableShadowSprite = null;
                SpeculativeRigidbody TableRigidBody = null;
                if (target.shadowSprite) {
                    dummyTableShadow = new GameObject("ExpandedTableShadow") { layer = LayerMask.NameToLayer("Unpixelated") };
                    dummyTableShadowSprite = dummyTableShadow.AddComponent<tk2dSprite>();
                    ExpandUtility.DuplicateSprite(dummyTableShadowSprite, (target.shadowSprite as tk2dSprite));
                    dummyTableShadow.transform.parent = dummyTable.transform;
                }
                if (dummyTableShadow) { TableRigidBody = ExpandUtility.GenerateOrAddToRigidBody(dummyTableShadow, CollisionLayer.HighObstacle); }
                tk2dSprite dummyTableSprite = dummyTable.AddComponent<tk2dSprite>();
                ExpandUtility.DuplicateSprite(dummyTableSprite, (target.sprite as tk2dSprite));
                Vector3 SpriteOffset = target.transform.position;
                DungeonData.Direction DirectionFlipped = target.DirectionFlipped;                
                SpriteOffset -= new Vector3(1.4f, 1.6f, 0);
                /*switch (DirectionFlipped) {
                    case DungeonData.Direction.NORTH:
                        SpriteOffset -= new Vector3(1.5f, 1.5f, 0);
                        break;
                    case DungeonData.Direction.EAST:
                        SpriteOffset -= new Vector3(1.5f, 1.5f, 0);
                        break;
                    case DungeonData.Direction.SOUTH:
                        SpriteOffset -= new Vector3(1.5f, 1.5f, 0);
                        break;
                    case DungeonData.Direction.WEST:
                        SpriteOffset -= new Vector3(1.5f, 1.5f, 0);
                        break;
                }*/
                dummyTable.transform.position = SpriteOffset;
                yield return null;
                Destroy(target.gameObject);
                // if (target.specRigidbody) { target.specRigidbody.CanBePushed = false; }
                float elapsed = 0f;
                Vector2 scaleAmount = new Vector2(TableScaleAmount, TableScaleAmount);
                if (!target) { yield break; }
                Vector2 startScale = dummyTable.transform.localScale;
                while (elapsed < TableExpandSpeed) {
                    if (!dummyTable) { yield break; }
                    elapsed += BraveTime.DeltaTime;
                    dummyTable.transform.localScale = Vector2.Lerp(startScale, scaleAmount, (elapsed * TableExpandSpeed));
                    if (DirectionFlipped == DungeonData.Direction.EAST && TableName.ToLower().Contains("horizontal")) {
                        dummyTable.transform.position -= new Vector3(0.04f, 0.02f, 0);
                    } else {
                        dummyTable.transform.position -= new Vector3(0.02f, 0.02f, 0);
                    }
                    if (dummyTableSprite) { dummyTableSprite.UpdateZDepth(); }
                    // if (dummyTableShadow) { dummyTableShadow.transform.localScale = Vector2.Lerp(startScale, scaleAmount, (elapsed * TableExpandSpeed)); }
                    /*dummyTable.transform.localScale = Vector2.Lerp(startScale, scaleAmount, (elapsed * TableExpandSpeed));
                    // if (dummyTableShadow) { dummyTableShadow.transform.localScale = Vector2.Lerp(startScale, scaleAmount, (elapsed * TableExpandSpeed)); }
                    if (dummyTableSprite) { dummyTableSprite.UpdateZDepth(); }                    
                    switch (DirectionFlipped) {
                        case DungeonData.Direction.NORTH:
                            dummyTable.transform.position -= new Vector3(0.005f, 0.01f, 0);
                            break;
                        case DungeonData.Direction.EAST:
                            dummyTable.transform.position -= new Vector3(0.01f, 0.005f, 0);
                            break;
                        case DungeonData.Direction.SOUTH:
                            dummyTable.transform.position -= new Vector3(0.008f, 0.01f, 0);
                            break;
                        case DungeonData.Direction.WEST:
                            dummyTable.transform.position -= new Vector3(0.01f, 0, 0);
                            dummyTable.transform.position -= new Vector3(0, 0.005f, 0);
                            break;
                    }*/
                    if (TableRigidBody) {
                        TableRigidBody.UpdateCollidersOnScale = true;
                        TableRigidBody.RegenerateColliders = true;
                    }
                    yield return null;
                }
                yield return null;                
                // if (target.gameObject.GetComponent<MajorBreakable>()) { target.gameObject.GetComponent<MajorBreakable>().HitPoints += 40; }
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

