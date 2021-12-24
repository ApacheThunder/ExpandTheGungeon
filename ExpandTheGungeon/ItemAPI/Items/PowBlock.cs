using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ItemAPI {

    public class PowBlock : PlayerItem {

        public static int PowBlockPickupID = -1;

        public static GameObject PowBlockObject;

        private static readonly List<string> PowBlockIdleSprites = new List<string>() {
                "PowBlock",
                "PowBlock_Idle_01",
                "PowBlock_Idle_02",
                "PowBlock_Idle_03",
                "PowBlock_Idle_04",
                "PowBlock_Idle_05",
                "PowBlock_Idle_06",
                "PowBlock_Idle_07",
                "PowBlock_Idle_07",
                "PowBlock_Idle_07",
                "PowBlock_Idle_07",
                "PowBlock_Idle_08",
                "PowBlock_Idle_09",
                "PowBlock_Idle_10",
                "PowBlock_Idle_11",
                "PowBlock_Idle_12",
                "PowBlock_Idle_13",
                "PowBlock_Idle_14"
            };

        private static readonly List<string> PowBlockUsedSprites = new List<string>() { "PowBlock_Used", "PowBlock_Used", };
        
        public static void Init(AssetBundle expandSharedAssets1) {
            PowBlockObject = expandSharedAssets1.LoadAsset<GameObject>("Pow Block");

            tk2dSprite m_PowBlockSprite = SpriteSerializer.AddSpriteToObject(PowBlockObject, ExpandPrefabs.EXItemCollection, "PowBlock");
            
            ExpandUtility.GenerateSpriteAnimator(PowBlockObject, playAutomatically: true);

            tk2dSpriteAnimator m_PowBlockAnimator = PowBlockObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_PowBlockAnimator, m_PowBlockSprite.Collection, PowBlockIdleSprites, "Idle", tk2dSpriteAnimationClip.WrapMode.Loop, 8);
            ExpandUtility.AddAnimation(m_PowBlockAnimator, m_PowBlockSprite.Collection, PowBlockUsedSprites, "POW", tk2dSpriteAnimationClip.WrapMode.Loop, 2);


            PowBlock powBlock = PowBlockObject.AddComponent<PowBlock>();
            string shortDesc = "Shaken not stirred...";
            string longDesc = "A special block that when stomped on causes a violent shaking.\n\nEnemies shall lose their footing.";
            ItemBuilder.SetupItem(powBlock, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(powBlock, ItemBuilder.CooldownType.Damage, 380f);
            powBlock.quality = ItemQuality.C;
            if (!ExpandSettings.EnableEXItems) { powBlock.quality = ItemQuality.EXCLUDED; }


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

            ExcludedEnemies = new List<string> {
                "cd4a4b7f612a4ba9a720b9f97c52f38c",
                "22fc2c2c45fb47cf9fb5f7b043a70122",
                "9215d1a221904c7386b481a171e52859",
                "9b4fb8a2a60a457f90dcf285d34143ac",
                "45192ff6d6cb43ed8f1a874ab6bef316"
            };

            m_InUse = false;
        }


        public List<string> ExcludedEnemies;

        public ScreenShakeSettings PowScreenShake;

        private bool m_InUse;

        private bool IsUsableRightNow(PlayerController user) {
            if (m_InUse | !user.IsInCombat) { return false; }
            return true;
        }

        public override bool CanBeUsed(PlayerController user) { return (IsUsableRightNow(user) && base.CanBeUsed(user)); }

        protected override void DoEffect(PlayerController user) {
            m_InUse = true;
            spriteAnimator.Play("POW");
            PowTime(user);
        }
                
        public override void Pickup(PlayerController player) { base.Pickup(player); }

        protected override void OnPreDrop(PlayerController player) { base.OnPreDrop(player); }

        public override void Update() {
            base.Update();
            if (!m_InUse && IsOnCooldown && !spriteAnimator.IsPlaying("POW")) {
                spriteAnimator.Play("POW");
            } else if (!m_InUse && !IsOnCooldown && !spriteAnimator.IsPlaying("Idle")) {
                spriteAnimator.Play("Idle");
            }
        }
        
        
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
                        if (ExcludedEnemies.Contains(RoomEnemies[i].EnemyGuid)) {
                            RoomEnemies[i].healthHaver.ApplyDamage(100000, Vector2.zero, "Pow Block Death", ignoreInvulnerabilityFrames: true, ignoreDamageCaps: true);
                        } else {
                            if (RoomEnemies[i].gameObject.GetComponent<KaliberController>()) {
                                Destroy(RoomEnemies[i].gameObject.GetComponent<KaliberController>());
                            }
                            if (RoomEnemies[i].gameObject.GetComponent<BloodbulonController>()) {
                                Destroy(RoomEnemies[i].gameObject.GetComponent<BloodbulonController>());
                            }
                            if (RoomEnemies[i].gameObject.GetComponent<TBulonController>()) {
                                Destroy(RoomEnemies[i].gameObject.GetComponent<TBulonController>());
                            }
                            if (RoomEnemies[i].gameObject.GetComponent<ShelletonRespawnController>()) { Destroy(RoomEnemies[i].gameObject.GetComponent<ShelletonRespawnController>()); }
                            RoomEnemies[i].healthHaver.minimumHealth = 0f;
                            RoomEnemies[i].DiesOnCollison = true;
                            RoomEnemies[i].CollisionDamage = 0;
                            RoomEnemies[i].CorpseObject = null;
                            RoomEnemies[i].EnemySwitchState = string.Empty;
                            RoomEnemies[i].healthHaver.ForceSetCurrentHealth(1);
                            RoomEnemies[i].gameObject.AddComponent<ExpandTossSpriteOnDeath>();
                            RoomEnemies[i].StealthDeath = true;
                            if (RoomEnemies[i].ShadowObject) {
                                RoomEnemies[i].HasShadow = false;
                                Destroy(RoomEnemies[i].ShadowObject);
                            }
                            GameObject m_RotationAnchor = new GameObject(RoomEnemies[i].name + " PowBlockRotationSource") { layer = RoomEnemies[i].gameObject.layer };
                            m_RotationAnchor.transform.position = RoomEnemies[i].specRigidbody.GetPixelCollider(ColliderType.HitBox).UnitCenter;
                            RoomEnemies[i].gameObject.transform.SetParent(m_RotationAnchor.transform);
                            StartCoroutine(FlipEnemy(RoomEnemies[i], m_RotationAnchor.transform));
                        }
                    }
                }
            }
            
            m_InUse = false;
            return;
        }
        
        private IEnumerator FlipEnemy(AIActor target, Transform rotationAnchor) {
            float elapsed = 0;
            float duration = 0.25f;
            Quaternion startAngle = Quaternion.Euler(new Vector3(0, 0, 0));
            Quaternion endAngle = Quaternion.Euler(new Vector3(0, 0, 180));

            target.behaviorSpeculator.Stun(999, true);
            if (target.aiShooter) { target.aiShooter.ToggleGunAndHandRenderers(false, "Was Powed"); }
            if (!target.specRigidbody) { yield break; }
            
            while (elapsed < duration) {
                if (!target) { break; }
                elapsed += BraveTime.DeltaTime;
                rotationAnchor.localRotation = Quaternion.Lerp(startAngle, endAngle, (elapsed / duration));
                target.specRigidbody.UpdateCollidersOnRotation = true;
                target.specRigidbody.LastRotation = target.gameObject.transform.eulerAngles.z;
                if (target.specRigidbody.TK2DSprite != null) { target.specRigidbody.TK2DSprite.UpdateZDepth(); }
                target.specRigidbody.ForceRegenerate(true);
                target.specRigidbody.Reinitialize();                
                if (target.sprite) { target.sprite.UpdateZDepth(); }
                yield return null;
            }
            rotationAnchor.DetachChildren();
            yield return null;
            Destroy(rotationAnchor.gameObject);
            yield break;
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

