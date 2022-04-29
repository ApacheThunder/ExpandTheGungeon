using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.SpriteAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class PortableShip : PlayerItem {
        
        public static int PortableShipID;

        public static GameObject PortableShipOBJ;

        public static void Init(AssetBundle expandSharedAssets1) {
            PortableShipOBJ = expandSharedAssets1.LoadAsset<GameObject>("Portable Ship");
            SpriteSerializer.AddSpriteToObject(PortableShipOBJ, ExpandPrefabs.EXItemCollection, "portableship");

            PortableShip portableShip = PortableShipOBJ.AddComponent<PortableShip>();
            string shortDesc = "Become Space Ship";
            string longDesc = "There were stories of someone with a ship fetish who got lost in the Gungeon. He found a way to transform himself into a ship based on the one a certain well known Gungeoneer used. Some say he's still down there roaming around as a minature space ship...";
            ItemBuilder.SetupItem(portableShip, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(portableShip, ItemBuilder.CooldownType.Timed, 3);
            portableShip.quality = ItemQuality.A;

            ItemBuilder.AddPassiveStatModifier(portableShip, PlayerStats.StatType.AdditionalItemCapacity, 1, StatModifier.ModifyMethod.ADDITIVE);

            if (!ExpandSettings.EnableEXItems) { portableShip.quality = ItemQuality.EXCLUDED; }
            
            PortableShipID = portableShip.PickupObjectId;
        }

        

        public PortableShip() {
            ShipPrefab = BraveResources.Load<GameObject>("PlayerRogueShip", ".prefab");
            CoopShipPalette = BraveResources.Load<GameObject>("PlayerCoopShip", ".prefab").GetComponent<PlayerSpaceshipController>().PaletteTex;

            ShipExplosionData = ExpandUtility.GenerateExplosionData(DamagesPlayer: false);

            m_RocketItemSprite = PickupObjectDatabase.GetById(502).sprite;
            
            itemState = ItemState.NotConfigured;

            m_FireProjectile = false;
            m_DoingDodgeRoll = false;
            m_DamageCooldown = 240;
            m_DodgeCooldown = 0.12f;
            m_MissileCooldown = 3;
            m_LaserCooldown = 0.15f;

            m_ShipHidden = false;
            m_HasCoopSynergy = false;


            m_CurrentLaserCooldown = 0;
            m_CurrentDodgeCooldown = 0;
            m_currentAngle = 0;

            m_CachedShipStartSpriteName = string.Empty;
        }
        
        public GameObject ShipPrefab;
        public GameObject CoopShipPrefab;
        public Texture2D CoopShipPalette;

        public ExplosionData ShipExplosionData;


        private GameObject m_ShipPrefabInstance;
        
        private ItemState itemState;

        private enum ItemState {
            NotConfigured,
            Inactive,
            Active
        }

        private tk2dSprite m_ShipSprite;
        private tk2dSpriteAnimator m_ShipAnimator;
        

        private tk2dBaseSprite m_RocketItemSprite;

        private AIBulletBank m_ShipBulletBank;
                
        private List<Transform> LaserShootPoints;

        private bool m_FireProjectile;
        private bool m_DoingDodgeRoll;
        private bool m_ShipHidden;
        private bool m_HasCoopSynergy;
        
        private float m_MissileCooldown;
        private float m_LaserCooldown;
        private float m_DodgeCooldown;
        private float m_DamageCooldown;
        private float m_CurrentLaserCooldown;
        private float m_CurrentDodgeCooldown;


        private string m_CachedShipStartSpriteName;
        
        private float m_currentAngle;
        

        protected bool IsKeyboardAndMouse(PlayerController player) {
            return BraveInput.GetInstanceForPlayer(player.PlayerIDX).IsKeyboardAndMouse(false);
        }

        public override void Pickup(PlayerController player) {
            
            base.Pickup(player);
            
            if (!m_ShipPrefabInstance) {
                m_ShipPrefabInstance = Instantiate(ShipPrefab.transform.Find("PlayerRotatePoint").gameObject, player.sprite.WorldCenter, Quaternion.identity);
                m_ShipPrefabInstance.name = "ShipRotatePoint";

                m_ShipSprite = m_ShipPrefabInstance.transform.Find("PlayerSprite").gameObject.GetComponent<tk2dSprite>();
                m_ShipSprite.gameObject.name = "ShipSprite";
                m_ShipSprite.HeightOffGround = 1f;
                m_ShipSprite.UpdateZDepth();

                m_ShipPrefabInstance.transform.SetParent(player.gameObject.transform);


                AIBulletBank sourceBulletBank = ShipPrefab.GetComponent<AIBulletBank>();
                m_ShipBulletBank = player.gameObject.AddComponent<AIBulletBank>();
                m_ShipBulletBank.Bullets = new List<AIBulletBank.Entry>() { sourceBulletBank.Bullets[0], sourceBulletBank.Bullets[1] };
                m_ShipBulletBank.useDefaultBulletIfMissing = false;
                m_ShipBulletBank.transforms = new List<Transform>();


                m_CachedShipStartSpriteName = m_ShipSprite.CurrentSprite.name;

                m_ShipAnimator = m_ShipSprite.gameObject.GetComponent<tk2dSpriteAnimator>();

                if (LastOwner.HasPassiveItem(326)) {
                    m_HasCoopSynergy = true;
                    m_ShipSprite.OverrideMaterialMode = tk2dBaseSprite.SpriteMaterialOverrideMode.OVERRIDE_MATERIAL_COMPLEX;
                    m_ShipSprite.renderer.material.SetTexture("_PaletteTex", CoopShipPalette);
                }

                LaserShootPoints = new List<Transform>() { m_ShipSprite.gameObject.transform.Find("Fire1"), m_ShipSprite.gameObject.transform.Find("Fire2") };

                m_ShipPrefabInstance.SetActive(false);
            }

            SpeculativeRigidbody specRigidbody = player.specRigidbody;
            specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(HandlePrerigidbodyCollision));

            player.OnReceivedDamage += HandlePlayerDamaged;
            
            itemState = ItemState.Inactive;
        }


        protected override void DoEffect(PlayerController user) {
            switch (itemState) {
                case ItemState.Active:
                    FireMissileVolley(user, m_ShipBulletBank);
                    ClearCooldowns();
                    return;
                case ItemState.Inactive:
                    m_currentAngle = BraveMathCollege.Atan2Degrees(user.unadjustedAimPoint.XY() - user.CenterPosition);
                    m_ShipPrefabInstance.SetActive(true);
                    timeCooldown = m_MissileCooldown;
                    sprite.SetSprite(m_RocketItemSprite.Collection, m_RocketItemSprite.GetCurrentSpriteDef().name);
                    user.SetIsFlying(true, "PlayerIsShip", false, false);
                    user.IsVisible = false;
                    user.ToggleShadowVisiblity(false);
                    user.healthHaver.Armor += 1;
                    AkSoundEngine.PostEvent("Play_OBJ_computer_boop_01", user.gameObject);
                    // user.AdditionalCanDodgeRollWhileFlying.AddOverride("IsAFlyingShip");
                    itemState = ItemState.Active;
                    ClearCooldowns();
                    return;
                case ItemState.NotConfigured:
                    ClearCooldowns();
                    return;
            }
        }



        public override void Update() {
            base.Update();
            switch (itemState) {
                case ItemState.NotConfigured:
                    return;
                case ItemState.Active:
                    if (!LastOwner | m_ShipHidden) { return; }

                    if (!m_HasCoopSynergy && LastOwner.HasPassiveItem(326)) {
                        m_HasCoopSynergy = true;
                        m_ShipSprite.OverrideMaterialMode = tk2dBaseSprite.SpriteMaterialOverrideMode.OVERRIDE_MATERIAL_COMPLEX;
                        m_ShipSprite.renderer.material.SetTexture("_PaletteTex", CoopShipPalette);
                    }
                                        
                    if (LastOwner.AcceptingNonMotionInput || LastOwner.CurrentInputState == PlayerInputState.FoyerInputOnly) {
                        CheckPlayerInput(LastOwner, m_ShipBulletBank);
                        if (m_CurrentDodgeCooldown > m_DodgeCooldown) { CheckDodgeRollInput(LastOwner); }
                        m_currentAngle = BraveMathCollege.Atan2Degrees(LastOwner.unadjustedAimPoint.XY() - LastOwner.CenterPosition);
                    }
                    if (m_DoingDodgeRoll) {
                        LastOwner.healthHaver.IsVulnerable = false;
                    } else {
                        if (!LastOwner.healthHaver.IsVulnerable) { LastOwner.healthHaver.IsVulnerable = true; }
                    }
                    m_currentAngle = BraveMathCollege.Atan2Degrees(LastOwner.unadjustedAimPoint.XY() - LastOwner.CenterPosition);
                    m_ShipPrefabInstance.transform.rotation = Quaternion.Euler(0f, 0f, m_currentAngle);
                    m_ShipSprite.ForceRotationRebuild();

                    LastOwner.IsOnFire = false;
                    LastOwner.IsVisible = false;
                    LastOwner.ToggleShadowVisiblity(false);
                    LastOwner.CurrentPoisonMeterValue = 0f;

                    LastOwner.IsGunLocked = true;


                    if (m_FireProjectile && m_CurrentLaserCooldown <= 0) {
                        FireProjectiles(LastOwner, m_ShipBulletBank);
                        m_CurrentLaserCooldown = m_LaserCooldown;
                    }
                    
                    m_CurrentLaserCooldown -= BraveTime.DeltaTime;
                    m_CurrentDodgeCooldown += BraveTime.DeltaTime;

                    if (m_CurrentLaserCooldown <= 0) {
                        LastOwner.ToggleRenderer(false);
                        LastOwner.ToggleHandRenderers(false);
                        LastOwner.ToggleGunRenderers(false);
                    }

                    if (!m_DoingDodgeRoll && !m_ShipAnimator.IsPlaying("dodgeroll_right_e") && 
                        !m_ShipAnimator.IsPlaying("dodgeroll_left_e") && 
                        !m_ShipAnimator.IsPlaying("fire_e") && 
                        !m_ShipAnimator.IsPlaying("idle_e") &&
                        !m_FireProjectile)
                    {
                        m_ShipAnimator.Play("idle_e");
                    }
                    return;
                case ItemState.Inactive:
                    return;
            }
        }

        
        private void CheckPlayerInput(PlayerController player, AIBulletBank bulletbank) {
            bool m_CanAttack = (!player.IsDodgeRolling || player.IsSlidingOverSurface) && player.CurrentStoneGunTimer <= 0f;
                    
            if (m_FireProjectile && !BraveInput.GetInstanceForPlayer(player.PlayerIDX).GetButton(GungeonActions.GungeonActionType.Shoot)) {
                m_FireProjectile = false;
            }
            if (player.SuppressThisClick) {
                while (BraveInput.GetInstanceForPlayer(player.PlayerIDX).GetButtonDown(GungeonActions.GungeonActionType.Shoot)) {
                    BraveInput.GetInstanceForPlayer(player.PlayerIDX).ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot);
                    if (BraveInput.GetInstanceForPlayer(player.PlayerIDX).GetButtonUp(GungeonActions.GungeonActionType.Shoot)) {
                        BraveInput.GetInstanceForPlayer(player.PlayerIDX).ConsumeButtonUp(GungeonActions.GungeonActionType.Shoot);
                    }
                }
                if (!BraveInput.GetInstanceForPlayer(player.PlayerIDX).GetButton(GungeonActions.GungeonActionType.Shoot)) {
                    player.SuppressThisClick = false;
                }
            } else if (m_CanAttack && BraveInput.GetInstanceForPlayer(player.PlayerIDX).GetButtonDown(GungeonActions.GungeonActionType.Shoot)) {
                bool ConsumeButton = false;
                m_FireProjectile = true;
                ConsumeButton |= true;
                if (ConsumeButton) { BraveInput.GetInstanceForPlayer(player.PlayerIDX).ConsumeButtonDown(GungeonActions.GungeonActionType.Shoot); }
            } else if (BraveInput.GetInstanceForPlayer(player.PlayerIDX).GetButtonUp(GungeonActions.GungeonActionType.Shoot)) {
                m_FireProjectile = false;
                BraveInput.GetInstanceForPlayer(player.PlayerIDX).ConsumeButtonUp(GungeonActions.GungeonActionType.Shoot);
            }
        }

        protected void CheckDodgeRollInput(PlayerController player) {
            if (player.WasPausedThisFrame) { return; }
            if (m_DoingDodgeRoll) { return; }
            Vector2 direction = player.specRigidbody.Velocity;
            if (direction == Vector2.zero) { return; }
            bool AttemptDodgeRollAnimation = false;
            BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(player.PlayerIDX);
            if (instanceForPlayer.GetButtonDown(GungeonActions.GungeonActionType.DodgeRoll)) {
                instanceForPlayer.ConsumeButtonDown(GungeonActions.GungeonActionType.DodgeRoll);
                AttemptDodgeRollAnimation = true;
            }
            if (AttemptDodgeRollAnimation) { StartCoroutine(PlayDodgeRollAnimation(player, direction)); }
            return;
        }

        
        protected IEnumerator PlayDodgeRollAnimation(PlayerController player, Vector2 direction) {
            tk2dSpriteAnimationClip tk2dSpriteAnimationClip = null;
            direction.Normalize();
            float directionAngle = direction.ToAngle();
            float movementAngle = BraveMathCollege.ClampAngle180(directionAngle - m_currentAngle);
            string text = (movementAngle < 0f) ? "dodgeroll_right_e" : "dodgeroll_left_e";
            tk2dSpriteAnimationClip = m_ShipAnimator.GetClipByName(text);
            yield return null;
            if (tk2dSpriteAnimationClip != null) {
                float overrideFps = tk2dSpriteAnimationClip.frames.Length / player.rollStats.GetModifiedTime(player);
                m_ShipAnimator.Play(tk2dSpriteAnimationClip, 0f, overrideFps, false);
                while (m_ShipAnimator.IsPlaying(tk2dSpriteAnimationClip)) {
                    m_DoingDodgeRoll = true;
                    yield return null;
                }
                m_ShipAnimator.Play("idle_e");
                m_CurrentDodgeCooldown = 0;
            }
            m_DoingDodgeRoll = false;
            yield break;
        }


        protected void FireMissileVolley(PlayerController player, AIBulletBank bulletbank) {
            foreach (Transform laserShootPoint in LaserShootPoints) {
                for (int VolleySize = 0; VolleySize < 5; VolleySize++) {
                    FireBullet(player, bulletbank, laserShootPoint, Quaternion.Euler(0f, 0f, -90f + m_currentAngle + Mathf.Lerp(-20f, 20f, VolleySize / 4f)) * Vector2.up, "missile");
                }
            }
        }

        protected void FireProjectiles(PlayerController player, AIBulletBank bulletbank) {
            if (Time.timeScale == 0 | GameManager.IsBossIntro | GameManager.Instance.PreventPausing) { return; }
            foreach (Transform laserShootPoint in LaserShootPoints) {
                FireBullet(player, bulletbank, laserShootPoint, Quaternion.Euler(0f, 0f, -90f + m_currentAngle) * Vector2.up, "default");
            }
            if (!m_DoingDodgeRoll && !m_ShipAnimator.IsPlaying("dodgeroll_right_e") && !m_ShipAnimator.IsPlaying("dodgeroll_left_e") && !m_ShipAnimator.IsPlaying("fire_e")) {
                m_ShipAnimator.Play("fire_e");
            }
        }

        private void FireBullet(PlayerController player, AIBulletBank bulletbank, Transform shootPoint, Vector2 dirVec, string bulletType) {
            GameObject m_BulletBank = bulletbank.CreateProjectileFromBank(shootPoint.position, BraveMathCollege.Atan2Degrees(dirVec.normalized), bulletType, null, false, true, false);
            Projectile projectile = m_BulletBank.GetComponent<Projectile>();
            projectile.Owner = player;
            projectile.Shooter = player.specRigidbody;
            projectile.collidesWithPlayer = false;
            projectile.specRigidbody.RegisterSpecificCollisionException(player.specRigidbody);
            if (ExpandSettings.debugMode) { player.DoPostProcessProjectile(projectile); }
        }


        private void ResetConfig(PlayerController player = null, bool ShipWasDestroyed = false, bool OnDestroy = false) {
            if (player) {
                if (!player.healthHaver.IsVulnerable) { player.healthHaver.IsVulnerable = true; }
                player.SetIsFlying(false, "PlayerIsShip", false, false);
                player.IsVisible = true;
                player.ToggleShadowVisiblity(true);
                player.IsGunLocked = false;
                LastOwner.ToggleRenderer(true);
                LastOwner.ToggleGunRenderers(true);
                LastOwner.ToggleHandRenderers(true);
                // player.AdditionalCanDodgeRollWhileFlying.RemoveOverride("IsAFlyingShip");
                if (!ShipWasDestroyed) {
                    SpeculativeRigidbody specRigidbody3 = player.specRigidbody;
                    specRigidbody3.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Remove(specRigidbody3.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(HandlePrerigidbodyCollision));

                    player.OnReceivedDamage -= HandlePlayerDamaged;
                }
                if (itemState == ItemState.Active) {
                    if (player.healthHaver.Armor > 0) { player.healthHaver.Armor -= 1; }
                }
            }
            if (ExpandSettings.debugMode) {
                sprite.SetSprite(ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), "portableship_alt");
            } else {
                sprite.SetSprite(ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), "portableship");
            }
            
            itemState = ItemState.Inactive;            
            if (ShipWasDestroyed) {
                AkSoundEngine.PostEvent("Play_OBJ_metronome_fail_01", player.gameObject);
                Exploder.Explode(m_ShipPrefabInstance.transform.position, ShipExplosionData, Vector2.zero, null, true, CoreDamageTypes.None, false);
                m_ShipPrefabInstance.SetActive(false);
                timeCooldown = m_DamageCooldown;                
                ApplyCooldown(player);
                return;
            }
            m_HasCoopSynergy = false;
            m_ShipBulletBank = null;
            m_ShipSprite = null;
            m_ShipHidden = false;
            if (m_ShipPrefabInstance) { m_ShipPrefabInstance.transform.parent = null; }
            LaserShootPoints.Clear();
            Destroy(m_ShipPrefabInstance);
            Destroy(m_ShipBulletBank);
            m_ShipPrefabInstance = null;

            m_currentAngle = 0;
            m_FireProjectile = false;
            m_DoingDodgeRoll = false;
            m_CurrentLaserCooldown = 0;
            m_CurrentDodgeCooldown = 0;
        }

        private void HandlePrerigidbodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            switch (itemState) {
                case ItemState.Active:
                    if (m_DoingDodgeRoll && otherRigidbody && otherPixelCollider != null && otherPixelCollider.CollisionLayer == CollisionLayer.Projectile) {
                        PhysicsEngine.SkipCollision = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void HandlePlayerDamaged(PlayerController player) {
            if (player && itemState == ItemState.Active) {
                itemState = ItemState.Inactive;
                ResetConfig(player, true);
            }
        }


        protected override void OnPreDrop(PlayerController user) {
            ResetConfig(user);
            base.OnPreDrop(user);
        }
        
        protected override void OnDestroy() {
            ResetConfig(OnDestroy: true);
            base.OnDestroy();
        }
    }
}

