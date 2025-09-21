using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandComponents;
using System.Reflection;

namespace ExpandTheGungeon.ItemAPI {

	public class MrCap : PlayerItem {

        public static int MrCapPickupID;

        public static GameObject MrCapObject;
        public static GameObject MrCapProjectile;
        public static GameObject MrCapVFX;

        public static float MrCapTossCooldown = 3;

        public static void Init(AssetBundle expandSharedAssets1) {
            MrCapObject = expandSharedAssets1.LoadAsset<GameObject>("Mr Cap");
            tk2dSprite MrCapsprite = SpriteSerializer.AddSpriteToObject(MrCapObject, ExpandPrefabs.EXItemCollection, "hatty_item");


            MrCap MrCap = MrCapObject.AddComponent<MrCap>();
            string shortDesc = "Total Control";
			string longDesc = "Place Holder";
			ItemBuilder.SetupItem(MrCap, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(MrCap, ItemBuilder.CooldownType.Timed, MrCapTossCooldown);
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

            GameObject m_PilotShipReference = BraveResources.Load<GameObject>("PlayerRogueShip", ".prefab");

            GameObject m_CapTrailObject = m_CapProjSpriteObject.transform.Find("Trail").gameObject;

            TrailRenderer m_CapProjTrailRenderer = m_CapTrailObject.GetComponent<TrailRenderer>();
            TrailRenderer m_SourceTrail = m_PilotShipReference.transform.Find("PlayerRotatePoint").transform.Find("PlayerSprite").transform.Find("engine 1").transform.Find("trail mix (1)").gameObject.GetComponent<TrailRenderer>();

            m_CapProjTrailRenderer.material = new Material(m_SourceTrail.material);


            ExpandHatProjectile MrCapProjectileComponent = MrCapProjectile.AddComponent<ExpandHatProjectile>();
            ExpandUtility.DuplicateComponent(MrCapProjectileComponent, (PickupObjectDatabase.GetById(448) as SpawnObjectPlayerItem).objectToSpawn.GetComponent<Projectile>());
            MrCapProjectileComponent.DestroyMode = Projectile.ProjectileDestroyMode.Destroy;
            MrCapProjectileComponent.PenetratesInternalWalls = true;

            SpeculativeRigidbody MrCapProjectileRigidBody = MrCapProjectile.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateRigidBody(MrCapProjectileRigidBody, (PickupObjectDatabase.GetById(448) as SpawnObjectPlayerItem).objectToSpawn.GetComponent<SpeculativeRigidbody>());
            MrCapProjectileRigidBody.CanPush = false;

            tk2dSpriteAnimator MrCapProjAnimator = ExpandUtility.GenerateSpriteAnimator(m_CapProjSpriteObject, playAutomatically: true);
            ExpandUtility.AddAnimation(MrCapProjAnimator, ExpandPrefabs.EXItemCollection, projSpritePaths, "spin", tk2dSpriteAnimationClip.WrapMode.Loop, frameRate: 12);
            

            PierceProjModifier m_CapPiercer = MrCapProjectile.AddComponent<PierceProjModifier>();
            m_CapPiercer.penetration = 1000;
            m_CapPiercer.penetratesBreakables = true;
            m_CapPiercer.preventPenetrationOfActors = true;
            m_CapPiercer.BeastModeLevel = PierceProjModifier.BeastModeStatus.BEAST_MODE_LEVEL_ONE;
            m_CapPiercer.UsesMaxBossImpacts = false;
            m_CapPiercer.MaxBossImpacts = -1;

            /*BounceProjModifier m_CapBouncer = MrCapProjectile.AddComponent<BounceProjModifier>();
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
            m_CapBouncer.TrackEnemyChance = 1;*/


            MrCapVFX = expandSharedAssets1.LoadAsset<GameObject>("EXMrCapVFX");
            tk2dSprite MrCapVFXSprite = SpriteSerializer.AddSpriteToObject(MrCapVFX, ExpandPrefabs.EXItemCollection, "hatty_001");
            MrCapVFX.AddComponent<ExpandHatVFX>();
        }
        

        public MrCap() {
            m_PickedUp = false;
            // m_Ready = true;
            InUse = false;
            m_FlightTimer = 6;
            m_MaxFlightTime = 6;
        }

        public bool InUse;
        public bool InFlight;

        public MrCap currentMrCap;

        
        public ExpandHatVFX attachedPlayerHat;
        public ExpandHatMindController CurrentMindControl;


        private bool m_PickedUp;
        //private bool m_Ready;
        // private bool m_DoingHatVFX;

        private float m_FlightTimer;
        private float m_MaxFlightTime;

        private GameObject spawnedHatObject;
        

        private bool IsUsableRightNow(PlayerController user) {
            // if (!m_Ready) return false;
            if (!user) return false;
            if (InFlight) return false;
            // if (!user.IsInCombat)return false;
            if (IsOnCooldown) return false;
            return true;
        }

        public override bool CanBeUsed(PlayerController user) { return (IsUsableRightNow(user)); }

        protected override void DoEffect(PlayerController user) {
            if (!m_PickedUp)m_PickedUp = true;
            if (!currentMrCap) currentMrCap = this;
            HandleMrCapToss(user);
		}
        

        protected override void OnPreDrop(PlayerController player) {
            base.OnPreDrop(player);
            if (CurrentMindControl)CurrentMindControl.Detach();
            if (spawnedHatObject)Destroy(spawnedHatObject);
            InUse = false;
            m_PickedUp = false;
            m_FlightTimer = m_MaxFlightTime;
            RemovePlayerHat(player);
        }

        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            if (player.gameObject.GetComponentInChildren<ExpandHatVFX>()) {
                attachedPlayerHat = player.gameObject.GetComponentInChildren<ExpandHatVFX>();
                attachedPlayerHat.targetType = ExpandHatVFX.TargetType.Player;
                attachedPlayerHat.vanishOverride = false;
            } else {
                DoHatVFX(player);
            }
            currentMrCap = this;
            m_PickedUp = true;
            m_FlightTimer = m_MaxFlightTime;
        }

        public void DoDetach() {
            if (CurrentMindControl)CurrentMindControl.Detach();
            InUse = false;
            timeCooldown = 60;
            if (LastOwner)ApplyCooldown(LastOwner);
        }

        public override void Update() {
            if (Dungeon.IsGenerating | !GameManager.HasInstance | GameManager.IsShuttingDown | GameManager.Instance.IsLoadingLevel) return;
            /*if (InUse && LastOwner && CurrentMindControl && CurrentMindControl.targetType == ExpandHatMindController.TargetType.AIActor && !LastOwner.IsInCombat) {
                CurrentMindControl.Detach();
                InUse = false;
            } else */if (InUse && LastOwner && !CurrentMindControl) {
                InUse = false;
            }
            base.Update();
        }

        public void LateUpdate() {
            if (Dungeon.IsGenerating | !GameManager.HasInstance | GameManager.IsShuttingDown | GameManager.Instance.IsLoadingLevel) return;
            if (spriteAnimator && !spriteAnimator.IsPlaying("Activate")) {
                if (sprite.GetCurrentSpriteDef().name != "hatty_item") sprite.SetSprite("hatty_item");
            }
            if (m_PickedUp && (InUse | InFlight)) {
                if (InFlight) {
                    m_FlightTimer -= BraveTime.DeltaTime;
                    if (m_FlightTimer <= 0) {
                        m_FlightTimer = m_MaxFlightTime;
                        InFlight = false;
                    }
                }
                if (attachedPlayerHat)attachedPlayerHat.vanishOverride = true;
            } else if (m_PickedUp && attachedPlayerHat) {
                attachedPlayerHat.vanishOverride = false;
            }
        }

        private void HandleMrCapToss(PlayerController user) {
            // float m_UseDelay = 0.5f;
            if (InUse && CurrentMindControl) {
                CurrentMindControl.Detach();
                InUse = false;
                timeCooldown = 60;
                ClearCooldowns();
            } else {
                timeCooldown = MrCapTossCooldown;
                ClearCooldowns();
                DoHatToss(user, MrCapProjectile, 0);
            }
        }

        private void DoHatToss(PlayerController user, GameObject objectToSpawn, float angleFromAim) {
            spriteAnimator.Play("Activate");
            if (spawnedHatObject) Destroy(spawnedHatObject);

            // GameObject spawnedHat = Instantiate(MrCapProjectile, user.sprite.WorldTopCenter, Quaternion.identity);
            GameObject spawnedHat = SpawnManager.SpawnProjectile(MrCapProjectile, user.sprite.WorldTopCenter, Quaternion.identity, false);

            spawnedHatObject = spawnedHat;
            
            ExpandHatProjectile hatProjectile = spawnedHatObject.GetComponent<ExpandHatProjectile>();
            if (hatProjectile) {
                AkSoundEngine.PostEvent("Play_EX_CapToss_01", gameObject);
                // SpawnManager.SpawnVFX(ExpandObjectDatabase.VFXKatanaBullets, user.sprite.WorldTopCenter, Quaternion.identity);
                
                m_FlightTimer = m_MaxFlightTime;
                hatProjectile.Owner = user;
                hatProjectile.TreatedAsNonProjectileForChallenge = true;
                hatProjectile.OnBecameDebris += HatOnDebris;
                hatProjectile.OnBecameDebrisGrounded += HatOnDebris;
                InFlight = true;
                hatProjectile.hatItemOwner = currentMrCap;
            }
        }

        public void HatOnDebris(DebrisObject obj) {
            Destroy(obj.gameObject);
        }
        
        
        public void DoHatVFX(PlayerController player) {
            // m_DoingHatVFX = true;
            RemovePlayerHat(player);
            float HatPosition = (player.sprite.GetBounds().size.y - (MrCapVFX.GetComponent<tk2dSprite>().GetBounds().size.y / 2.1f));
            player.PlayEffectOnActor(MrCapVFX, new Vector3(0f, HatPosition, 0f), true, false, true);
            attachedPlayerHat = player.gameObject.GetComponentInChildren<ExpandHatVFX>();
            if (attachedPlayerHat) {
                attachedPlayerHat.hatOwner = player;
                attachedPlayerHat.targetType = ExpandHatVFX.TargetType.Player;
            }
            // m_DoingHatVFX = false;
        }

        public void RemovePlayerHat(PlayerController player) {
            // m_DoingHatVFX = true;
            if (attachedPlayerHat) {
                Destroy(attachedPlayerHat.gameObject);
                attachedPlayerHat = null;
            }
            if (player && player.gameObject.GetComponentInChildren<ExpandHatVFX>()) {
                Destroy(player.gameObject.GetComponentInChildren<ExpandHatVFX>().gameObject);
            }
        }

        public void ResetHat(GameObject otherHat, GameObject sourceTarget) {
            // m_DoingHatVFX = true;
            InUse = false;
            InFlight = false;
            // m_Ready = true;
            if (otherHat)Destroy(otherHat);
            if (sourceTarget && sourceTarget.GetComponentInChildren<ExpandHatMindController>()) {
                Destroy(sourceTarget.GetComponentInChildren<ExpandHatMindController>());
            }
            if (CurrentMindControl) {
                Destroy(CurrentMindControl);
                CurrentMindControl = null;
            }
            DoHatVFX(LastOwner);
        }
        
        protected override void OnDestroy() {
            m_PickedUp = false;
            base.OnDestroy();
        }
    }
}

