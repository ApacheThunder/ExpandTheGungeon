using System.Collections;
using System.Reflection;
using System;
using MonoMod.RuntimeDetour;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;

namespace ExpandTheGungeon.ItemAPI {
    
    public class TableTechAssassin : PassiveItem {

        public static GameObject TableTechAssasinObject;
        public static int TableTechAssasinID;

        public static void Init(AssetBundle expandSharedAssets1) {
            TableTechAssassinHook.TableExplosionData = ExpandUtility.GenerateExplosionData(damage: 60);
                    
            TableTechAssasinObject = expandSharedAssets1.LoadAsset<GameObject>("Table Tech Assassin");
            SpriteSerializer.AddSpriteToObject(TableTechAssasinObject, ExpandPrefabs.EXItemCollection, "tabletech_assassin");

            TableTechAssassin tableTechAssassin = TableTechAssasinObject.AddComponent<TableTechAssassin>();

            string shortDesc = "Betray the Flipper";
            string longDesc = "A forbidden technique thought lost was recovered in the Gungeon.\n\nAll that was written was this: \n\n 'Do upon the flipper that which the flipper had done to you'";
            ItemBuilder.SetupItem(tableTechAssassin, shortDesc, longDesc, "ex");
            tableTechAssassin.quality = ItemQuality.D;
            if (!ExpandSettings.EnableEXItems) { tableTechAssassin.quality = ItemQuality.EXCLUDED; }
            TableTechAssasinID = tableTechAssassin.PickupObjectId;
        }

        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            HandleFlipHook(false);
        }

        public override DebrisObject Drop(PlayerController player) {
            DebrisObject drop = base.Drop(player);
            HandleFlipHook(true);
            return drop;
        }

        public void HandleFlipHook(bool isDropping = false) {
            if (isDropping) {
                if (TableTechAssassinHook.m_flipHook != null) {
                    TableTechAssassinHook.m_flipHook.Dispose();
                    TableTechAssassinHook.m_flipHook = null;
                }
            } else {
                 if (TableTechAssassinHook.m_flipHook == null) {
                    TableTechAssassinHook.m_flipHook = new Hook(
                        typeof(FlippableCover).GetMethod("Flip", new Type[] { typeof(SpeculativeRigidbody) }),
                        typeof(TableTechAssassinHook).GetMethod("FlipHook", BindingFlags.Public | BindingFlags.Instance),
                        typeof(TableTechAssassinHook)
                    );
                }
            }
        }

        protected override void OnDestroy() {
            HandleFlipHook(true);
            base.OnDestroy();
        }
    }


    public class TableTechAssassinHook : MonoBehaviour {

        public static Hook m_flipHook;
        public static ExplosionData TableExplosionData;

        public void FlipHook(Action<FlippableCover, SpeculativeRigidbody> orig, FlippableCover self, SpeculativeRigidbody flipperRigidbody) {
            try {
                orig(self, flipperRigidbody);
                if (self && flipperRigidbody && flipperRigidbody.gameObject.GetComponent<AIActor>()) {
                    AkSoundEngine.PostEvent("Play_EX_TableAssassin_01", self.gameObject);
                    GameManager.Instance.StartCoroutine(HandleDelayedTableExplosion(self, flipperRigidbody, 1));
                }
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] WARNING: Excpetion caught in TableTechAssassinHook.FlipHook!");
                Debug.Log("[ExpandTheGungeon] WARNING: Excpetion caught in TableTechAssassinHook.FlipHook!");
                Debug.LogException(ex, self);
            }
        }

        private IEnumerator HandleDelayedTableExplosion(FlippableCover sourceTable, SpeculativeRigidbody flipperSource, float delay) {
            yield return new WaitForSeconds(delay);
            
            if (sourceTable.sprite) {
                Vector2 ExplosionCenterPosition = sourceTable.sprite.WorldCenter;

                if (sourceTable.specRigidbody) { sourceTable.specRigidbody.CollideWithOthers = false; }

                MajorBreakable breakable = sourceTable.GetComponentInChildren<MajorBreakable>();                
                if (breakable) { breakable.ApplyDamage(90, Vector2.zero, false, true, false); }
                Exploder.Explode(ExplosionCenterPosition, TableExplosionData, Vector2.zero, null, false, CoreDamageTypes.None, false);
            } else if (sourceTable.specRigidbody) {
                Vector2 ExplosionCenterPosition = sourceTable.transform.position;

                if (sourceTable.specRigidbody) { sourceTable.specRigidbody.CollideWithOthers = false; }

                MajorBreakable breakable = sourceTable.GetComponentInChildren<MajorBreakable>();                
                if (breakable) { breakable.ApplyDamage(90, Vector2.zero, false, true, false); }
                Exploder.Explode(ExplosionCenterPosition, TableExplosionData, Vector2.zero, null, false, CoreDamageTypes.None, false);
            }
            yield break;
        }
    }
}

