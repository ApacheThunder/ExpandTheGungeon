using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandComponents;

namespace ExpandTheGungeon.ItemAPI {

    public class PowBlock : PlayerItem {

        public static int PowBlockPickupID = -1;

        public static GameObject PowBlockObject;

        public static void Init(AssetBundle expandSharedAssets1) {
            PowBlockObject = expandSharedAssets1.LoadAsset<GameObject>("Pow Block");
            ItemBuilder.AddSpriteToObject(PowBlockObject, expandSharedAssets1.LoadAsset<Texture2D>("PowBlock"), false, false);
            SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>("PowBlock_Used"), PowBlockObject.GetComponent<tk2dSprite>().Collection);

            PowBlock powBlock = PowBlockObject.AddComponent<PowBlock>();
            string shortDesc = "Shaken not stirred...";
            string longDesc = "A special block that when stomped on causes a violent shaking.\n\nEnemies shall lose their footing.";
            ItemBuilder.SetupItem(powBlock, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(powBlock, ItemBuilder.CooldownType.Damage, 380f);
            powBlock.quality = ItemQuality.C;



            PowBlockPickupID = powBlock.PickupObjectId;
        }


        public PowBlock() {
            PowScreenShake = new ScreenShakeSettings() {
                magnitude = 3f,
                speed = 15f,
                time = 0.05f,
                falloff = 0.1f,
                direction = new Vector2(0, -1),
                vibrationType = ScreenShakeSettings.VibrationType.Auto,
                simpleVibrationTime = Vibration.Time.Quick,
                simpleVibrationStrength = Vibration.Strength.Hard
            };

            m_InUse = false;
        }


        public ScreenShakeSettings PowScreenShake;

        private bool m_InUse;

        private bool IsUsableRightNow(PlayerController user) {
            if (m_InUse | !user.IsInCombat) { return false; }
            return true;
        }

        public override bool CanBeUsed(PlayerController user) { return (IsUsableRightNow(user) && base.CanBeUsed(user)); }

        protected override void DoEffect(PlayerController user) {
            m_InUse = true;
            sprite.SetSprite("PowBlock_Used");
            PowTime(user);
        }
                
        public override void Pickup(PlayerController player) { base.Pickup(player); }

        protected override void OnPreDrop(PlayerController player) { base.OnPreDrop(player); }

        public override void Update() { base.Update(); }
        
        
        public void PowTime(PlayerController user) {
            RoomHandler currentRoom = user.CurrentRoom;           
            AkSoundEngine.PostEvent("Play_EX_PowBlock_Trigger", gameObject);
            GameManager.Instance.MainCameraController.DoScreenShake(PowScreenShake, user.transform.position, false);

            if (user.CurrentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                List<AIActor> RoomEnemies = user.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear);

                for (int i = 0; i < RoomEnemies.Count; i++) {
                    if (RoomEnemies[i] && !RoomEnemies[i].IsGone && RoomEnemies[i].specRigidbody &&
                        RoomEnemies[i].sprite && !RoomEnemies[i].healthHaver.IsDead && !RoomEnemies[i].healthHaver.IsBoss) 
                    {
                        if (RoomEnemies[i].EnemyGuid != "cd4a4b7f612a4ba9a720b9f97c52f38c" && RoomEnemies[i].EnemyGuid != "22fc2c2c45fb47cf9fb5f7b043a70122" &&
                            RoomEnemies[i].EnemyGuid != "cd4a4b7f612a4ba9a720b9f97c52f38c" && RoomEnemies[i].EnemyGuid != "9215d1a221904c7386b481a171e52859" &&
                            RoomEnemies[i].EnemyGuid != "9b4fb8a2a60a457f90dcf285d34143ac")
                        {
                            RoomEnemies[i].DiesOnCollison = true;
                            RoomEnemies[i].CollisionDamage = 0;
                            RoomEnemies[i].CorpseObject = null;
                            RoomEnemies[i].EnemySwitchState = string.Empty;
                            RoomEnemies[i].healthHaver.ForceSetCurrentHealth(1);
                            RoomEnemies[i].gameObject.AddComponent<ExpandTossSpriteOnDeath>();
                            RoomEnemies[i].procedurallyOutlined = false;
                            RoomEnemies[i].StealthDeath = true;
                            StartCoroutine(FlipEnemy(RoomEnemies[i]));
                        } else {
                            RoomEnemies[i].healthHaver.ApplyDamage(100000, Vector2.zero, "Pow Block Death", ignoreInvulnerabilityFrames: true, ignoreDamageCaps: true);
                        }
                    }
                }
            }
            
            m_InUse = false;
            return;
        }

        private IEnumerator FlipEnemy(AIActor target) {
            float elapsed = 0;
            float duration = 0.25f;
            float angle = 0;

            target.behaviorSpeculator.Stun(999, true);
                        
            while (elapsed < duration) {
                if (!target) { break; }

                elapsed += BraveTime.DeltaTime;
                
                angle += (BraveTime.DeltaTime * 36f);

                target.gameObject.transform.RotateAround(target.specRigidbody.GetUnitCenter(ColliderType.HitBox), Vector3.forward, angle);
                
                if (target.specRigidbody) {
                    target.specRigidbody.UpdateCollidersOnRotation = true;
                    target.specRigidbody.Reinitialize();
                }
                if (target.sprite) { target.sprite.UpdateZDepth(); }

                yield return null;
            }
            
            yield return new WaitForSeconds(0.5f);
            sprite.SetSprite("PowBlock");
            yield break;
        }
        

        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

