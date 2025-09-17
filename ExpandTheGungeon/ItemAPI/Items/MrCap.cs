using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandComponents;

namespace ExpandTheGungeon.ItemAPI {

	public class MrCap : PlayerItem {

        public static int MrCapPickupID;

        public static GameObject MrCapObject;
        public static GameObject MrCapProjectile;
        public static GameObject MrCapVFX;

        public static void Init(AssetBundle expandSharedAssets1) {
            MrCapObject = expandSharedAssets1.LoadAsset<GameObject>("Mr Cap");
            tk2dSprite MrCapsprite = SpriteSerializer.AddSpriteToObject(MrCapObject, ExpandPrefabs.EXItemCollection, "hatty_item");

            MrCap MrCap = MrCapObject.AddComponent<MrCap>();
            string shortDesc = "Total Control";
			string longDesc = "Place Holder";
			ItemBuilder.SetupItem(MrCap, shortDesc, longDesc, "ex");
            // ItemBuilder.SetCooldownType(MrCap, ItemBuilder.CooldownType.Damage, 250);
            ItemBuilder.SetCooldownType(MrCap, ItemBuilder.CooldownType.Timed, 3);
            MrCap.quality = ItemQuality.A;
            if (!ExpandSettings.EnableEXItems)MrCap.quality = ItemQuality.EXCLUDED;

            List<string> spritePaths = new List<string>() {
                "hatty_item",
                "hatty_item",
                "hatty_item_active_blue",
                "hatty_item_active_blue",
                "hatty_item_active_red",
                "hatty_item_active_red",
                "hatty_item_active_blue",
                "hatty_item_active_blue",
                "hatty_item_active_red",
                "hatty_item_active_red",
                "hatty_item_active_blue",
                "hatty_item_active_blue",
                "hatty_item"
            };

            List<string> projSpritePaths = new List<string>() {
                "hatty_001",
                "hatty_002",
                "hatty_003",
                "hatty_004",
                "hatty_005",
                "hatty_006"
            };
          

            ExpandUtility.GenerateSpriteAnimator(MrCapObject);

            tk2dSpriteAnimator MrCapAnimator = MrCapObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(MrCapAnimator, ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), spritePaths, "Activate", frameRate: 8);

            MrCapPickupID = MrCap.PickupObjectId;

            MrCapProjectile = expandSharedAssets1.LoadAsset<GameObject>("EXMrCapProjectile");

            GameObject m_CapProjSpriteObject = MrCapProjectile.transform.Find("Sprite").gameObject;

            tk2dSprite MrCapProjectileSprite = SpriteSerializer.AddSpriteToObject(m_CapProjSpriteObject, ExpandPrefabs.EXItemCollection, "hatty_001");


            ExpandHatProjectile MrCapProjectileComponent = MrCapProjectile.AddComponent<ExpandHatProjectile>();
            ExpandUtility.DuplicateComponent(MrCapProjectileComponent, (PickupObjectDatabase.GetById(448) as SpawnObjectPlayerItem).objectToSpawn.GetComponent<Projectile>());
            MrCapProjectileComponent.DestroyMode = Projectile.ProjectileDestroyMode.Destroy;

            SpeculativeRigidbody MrCapProjectileRigidBody = MrCapProjectile.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateRigidBody(MrCapProjectileRigidBody, (PickupObjectDatabase.GetById(448) as SpawnObjectPlayerItem).objectToSpawn.GetComponent<SpeculativeRigidbody>());

            tk2dSpriteAnimator MrCapProjAnimator = ExpandUtility.GenerateSpriteAnimator(m_CapProjSpriteObject, playAutomatically: true);
            ExpandUtility.AddAnimation(MrCapProjAnimator, ExpandPrefabs.EXItemCollection, projSpritePaths, "spin", tk2dSpriteAnimationClip.WrapMode.Loop, frameRate: 9);
            

            /*PierceProjModifier m_CapPiercer = MrCapProjectile.AddComponent<PierceProjModifier>();
            m_CapPiercer.penetration = 1000;
            m_CapPiercer.penetratesBreakables = true;
            m_CapPiercer.preventPenetrationOfActors = true;
            m_CapPiercer.BeastModeLevel = PierceProjModifier.BeastModeStatus.BEAST_MODE_LEVEL_ONE;
            m_CapPiercer.UsesMaxBossImpacts = false;
            m_CapPiercer.MaxBossImpacts = -1;*/

            BounceProjModifier m_CapBouncer = MrCapProjectile.AddComponent<BounceProjModifier>();
            m_CapBouncer.numberOfBounces = 1000;
            m_CapBouncer.chanceToDieOnBounce = 0;
            m_CapBouncer.percentVelocityToLoseOnBounce = 0;
            m_CapBouncer.usesAdditionalScreenShake = false;
            m_CapBouncer.useLayerLimit = false;
            m_CapBouncer.layerLimit = 0;
            m_CapBouncer.ExplodeOnEnemyBounce = false;
            m_CapBouncer.removeBulletScriptControl = true;
            m_CapBouncer.suppressHitEffectsOnBounce = true;
            m_CapBouncer.onlyBounceOffTiles = false;
            m_CapBouncer.bouncesTrackEnemies = false;
            m_CapBouncer.bounceTrackRadius = 5;
            m_CapBouncer.TrackEnemyChance = 1;


            MrCapVFX = expandSharedAssets1.LoadAsset<GameObject>("EXMrCapVFX");

        }
        

        public MrCap() {
            m_PickedUp = false;
            m_Ready = true;
            InUse = false;
        }

        public bool InUse;

        private bool m_PickedUp;
        private bool m_Ready;

        private ExpandMrCapMindController m_CurrentMindControl;
        private GameObject spawnedHatObject;


        private bool IsUsableRightNow(PlayerController user) {
            if (!m_Ready) return false;
            if (!user) return false;
            if (spawnedHatObject) return false;
            // if (!user.IsInCombat)return false;
            return true;
        }

        public override bool CanBeUsed(PlayerController user) {
            return (IsUsableRightNow(user) && base.CanBeUsed(user));
        }

        protected override void DoEffect(PlayerController user) {
            if (!m_PickedUp)m_PickedUp = true;
            AkSoundEngine.PostEvent("Play_OBJ_computer_boop_01", user.gameObject);
            m_Ready = false;
            StartCoroutine(HandleMrCapToss(user));
		}

        private IEnumerator HandleMrCapToss(PlayerController user) {
            // AttachMrCap(user);
            float m_UseDelay = 0.5f;
            if (InUse && m_CurrentMindControl) {
                m_CurrentMindControl.Detach();
                InUse = false;
                m_Ready = true;
                m_UseDelay = 1;
            } else {
                DoHatToss(user, MrCapProjectile, 0);
            }
            yield return null;
            yield return new WaitForSeconds(m_UseDelay);
            m_Ready = true;
            yield break;
        }

        private void DoHatToss(PlayerController user, GameObject objectToSpawn, float angleFromAim) {
            spriteAnimator.Play("Activate");
            if (spawnedHatObject) Destroy(spawnedHatObject);

            GameObject spawnedHat = Instantiate(MrCapProjectile, user.sprite.WorldTopCenter, Quaternion.identity);
            /*Transform spawnedHatChild = spawnedHat.transform.Find("Sprite");
            
            tk2dBaseSprite objectSprite = spawnedHatChild.gameObject.GetComponent<tk2dBaseSprite>();
            if (objectSprite)objectSprite.PlaceAtPositionByAnchor(vector2, tk2dBaseSprite.Anchor.MiddleCenter);*/

            spawnedHatObject = spawnedHat;
            
            ExpandHatProjectile hatProjectile = spawnedHatObject.GetComponent<ExpandHatProjectile>();
            if (hatProjectile) {
                hatProjectile.Owner = user;
                hatProjectile.TreatedAsNonProjectileForChallenge = true;
            }
            
            if (spawnedHatObject && spawnedHatObject.GetComponent<SpeculativeRigidbody>()) {
                spawnedHatObject.GetComponent<SpeculativeRigidbody>().OnPreRigidbodyCollision += HatOnPreRigidBodyCollision;
                spawnedHatObject.GetComponent<SpeculativeRigidbody>().OnPreTileCollision += HatOnPreTileCollision;
            }
        }

        public void HatOnPreTileCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, PhysicsEngine.Tile tile, PixelCollider otherPixelCollider) {
            PhysicsEngine.SkipCollision = true;
            ExpandHatProjectile hatProjectile = myRigidbody.GetComponent<ExpandHatProjectile>();
            if (hatProjectile) {
                hatProjectile.TrackignSpeed *= 100f;
                hatProjectile.fireMode = ExpandHatProjectile.FireMode.ReturnToPlayer;
            }
        }

        public void HatOnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (LastOwner) {
                if (LastOwner.CurrentRoom?.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) != null) {
                    if (LastOwner.CurrentRoom?.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear).Count < 2 |
                        LastOwner.CurrentRoom?.area?.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS |
                        myRigidbody.transform.position.GetAbsoluteRoom() == null | LastOwner.CurrentRoom == null |
                        LastOwner.CurrentRoom != myRigidbody.transform.position.GetAbsoluteRoom()) {
                        PhysicsEngine.SkipCollision = true;
                        if (myRigidbody.GetComponent<ExpandHatProjectile>()) {
                            myRigidbody.GetComponent<ExpandHatProjectile>().TrackignSpeed *= 1.5f;
                            myRigidbody.GetComponent<ExpandHatProjectile>().fireMode = ExpandHatProjectile.FireMode.ReturnToPlayer;
                        }
                        return;
                    }
                }
            }
            bool m_AttachedToEnemy = false;

            if (otherRigidbody.gameObject.GetComponent<AIActor>() && !otherRigidbody.gameObject.GetComponent<CompanionController>()) {
                AIActor m_AIActor = otherRigidbody.gameObject.GetComponent<AIActor>();
                if (!m_AIActor.healthHaver.IsDead && !m_AIActor.healthHaver.IsBoss && 
                    m_AIActor.ParentRoom != null && LastOwner.CurrentRoom != null &&
                    m_AIActor.ParentRoom == LastOwner.CurrentRoom
                    ) {
                    if (m_CurrentMindControl) {
                        m_CurrentMindControl.Detach();
                        PhysicsEngine.SkipCollision = true;
                        if (myRigidbody.GetComponent<ExpandHatProjectile>()) {
                            myRigidbody.GetComponent<ExpandHatProjectile>().TrackignSpeed *= 1.5f;
                            myRigidbody.GetComponent<ExpandHatProjectile>().fireMode = ExpandHatProjectile.FireMode.ReturnToPlayer;
                        }
                        return;
                    }
                    m_CurrentMindControl = m_AIActor.gameObject.AddComponent<ExpandMrCapMindController>();
                    m_CurrentMindControl.targetType = ExpandMrCapMindController.TargetType.AIActor;
                    if (myRigidbody.gameObject.GetComponent<ExpandHatProjectile>() && (myRigidbody.gameObject.GetComponent<ExpandHatProjectile>().Owner is PlayerController)) {
                        m_CurrentMindControl.Init((myRigidbody.gameObject.GetComponent<ExpandHatProjectile>().Owner as PlayerController), m_AIActor.gameObject);
                    } else {
                        m_CurrentMindControl.Init(LastOwner, m_AIActor.gameObject);
                    }
                    m_AttachedToEnemy = true;
                }
                InUse = m_AttachedToEnemy;
                if (InUse) {
                    PhysicsEngine.SkipCollision = true;
                    Destroy(myRigidbody.gameObject);
                }
            }
            if (!m_AttachedToEnemy && otherRigidbody.GetComponentInChildren<MinorBreakable>()) {
                PhysicsEngine.SkipCollision = true;
            } else if (!m_AttachedToEnemy) {
                PhysicsEngine.SkipCollision = true;
                if (myRigidbody.GetComponent<ExpandHatProjectile>()) {
                    myRigidbody.GetComponent<ExpandHatProjectile>().fireMode = ExpandHatProjectile.FireMode.ReturnToPlayer;
                    myRigidbody.GetComponent<ExpandHatProjectile>().TrackignSpeed *= 1.5f;
                }
            }
        }

        /*public void SetSprite(string NameOverride = null) {
            if (!string.IsNullOrEmpty(NameOverride)) {
                if (spriteAnimator && spriteAnimator.IsPlaying("Activate")) spriteAnimator.Stop();
                sprite.SetSprite(NameOverride);
                return;
            }
            if (spriteAnimator && spriteAnimator.IsPlaying("Activate")) return;

            if (IsOnCooldown && sprite.GetCurrentSpriteDef().name != "hatty_item_active_red") {
                sprite.SetSprite("hatty_item_active_red");
                return;
            }

            if (InUse && sprite.GetCurrentSpriteDef().name != "hatty_item_active_blue") {
                sprite.SetSprite("hatty_item_active_blue");
                return;
            }
            if (sprite.GetCurrentSpriteDef().name != "hatty_item") sprite.SetSprite("hatty_item");
        }*/

        public override void Update() {
            if (Dungeon.IsGenerating) return;
            if (InUse && LastOwner && m_CurrentMindControl && !LastOwner.IsInCombat) {
                m_CurrentMindControl.Detach();
                // if (spriteAnimator)spriteAnimator.Stop();
            }
            // if (m_PickedUp)SetSprite();
            if (spriteAnimator && !spriteAnimator.IsPlaying("Activate")) {
                if (sprite.GetCurrentSpriteDef().name != "hatty_item") sprite.SetSprite("hatty_item");
            }
            base.Update();
        }

        
        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            m_PickedUp = true;
            // if (spriteAnimator) spriteAnimator.Stop();
            // SetSprite();
        }

        protected override void OnPreDrop(PlayerController player) {
            base.OnPreDrop(player);
            if (m_CurrentMindControl)m_CurrentMindControl.Detach();
            if (spawnedHatObject)Destroy(spawnedHatObject);
            // if (spriteAnimator) spriteAnimator.Stop();
            // sprite.SetSprite("hatty_item");
            InUse = false;
            m_PickedUp = false;
        }
        
        protected override void OnDestroy() {
            m_PickedUp = false;
            base.OnDestroy();
        }
    }
}

