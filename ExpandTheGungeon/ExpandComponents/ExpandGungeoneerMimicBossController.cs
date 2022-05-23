using System.Collections;
using UnityEngine;
using Dungeonator;
using System.Reflection;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using Pathfinding;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandGungeoneerMimicBossController : BraveBehaviour {
        
        public ExpandGungeoneerMimicBossController() {
            IntroDone = true;
            MovedToCenter = false;
            RotationAngle = 90;
            UsesRotationInsteadOfInversion = false;
            UseGlitchShader = false;
            EnableFallBackAttacks = true;
            IsConfigured = false;

            m_SpecialChargeWeapons = new List<int>() {
                332, 393, 541, 719, 393, 8, 37, 200, 210,
                327, 328, 358, 359, 370, 382, 478, 535, 382,
                686, 688, 693, 748
            };
            
            m_HasBeenActivated = false;
            m_DelayedActive = false;
            m_MirrorGunToggle = true;
            m_IsPathfindingToCenter = false;
            m_RandomMovement = false;
            m_BeamSweepingBack = false;
            m_InvertSweepAngle = false;
            m_CameraChanged = false;

            m_IsFiring = false;
            m_PauseAutoFire = false;

            m_RandomRefresh = 1f;
            m_RandomFireRate = 0.5f;
            m_ChargingTime = 0;

            m_BeamSweepAngle = 0;
            
            m_StartingGuns = new List<int>() { 51, 15 };

            m_IsDisconnected = false;
        }
        
        public PlayerController m_Player;

        public bool IntroDone;
        public bool MovedToCenter;
        public bool UsesRotationInsteadOfInversion;
        public bool UseGlitchShader;
        public bool EnableFallBackAttacks;
        public bool IsConfigured;

        public float RotationAngle;
        
        // Thins like Casey go here
        public List<int> m_SpecialChargeWeapons;

        private List<int> m_StartingGuns;
        
        private AIActor m_AIActor;
        
        private bool m_HasBeenActivated;
        private bool m_DelayedActive;
        private bool m_MirrorGunToggle;
        private bool m_IsPathfindingToCenter;
        private bool m_RandomMovement;

        private bool m_IsFiring;
        private bool m_PauseAutoFire;

        private float m_RandomRefresh;
        private float m_RandomFireRate;
        private float m_ChargingTime;

        private float m_BeamSweepAngle;
        private bool m_BeamSweepingBack;
        private bool m_InvertSweepAngle;
        private bool m_IsDisconnected;
        private bool m_CameraChanged;
        
        private RoomHandler m_ParentRoom;
        private ExpandGungeoneerMimicIntroDoer m_IntroDoer;

        protected bool m_CanAttack {
            get {
                return (!m_Player.IsDodgeRolling || m_Player.IsSlidingOverSurface) && !m_IsDisconnected && !m_Player.IsGunLocked &&
                        m_Player.CurrentStoneGunTimer <= 0f && !m_Player.CurrentGun.IsReloading && m_MirrorGunToggle &&
                        (m_AIActor && m_AIActor.aiShooter.CurrentGun && !m_AIActor.aiShooter.CurrentGun.HasShootStyle(ProjectileModule.ShootStyle.Beam));
            }
        }

        private bool m_HasShootStyle(ProjectileModule.ShootStyle ShootStyle) {
            if (!m_AIActor | !m_AIActor.aiShooter | !m_AIActor.aiShooter.CurrentGun) { return false; }
            return m_AIActor.aiShooter.CurrentGun.HasShootStyle(ShootStyle);
        }
        
        public void Init() {

            if (!m_Player) { m_Player = GameManager.Instance.PrimaryPlayer; }
           
            m_AIActor = aiActor;
            m_IntroDoer = gameObject.GetComponent<ExpandGungeoneerMimicIntroDoer>();

            if (!m_AIActor) {
                Destroy(gameObject);
                return;
            }
            
            m_RandomRefresh = Random.Range(15, 30);
            m_RandomFireRate = Random.Range(0.4f, 0.8f);
            m_ParentRoom = m_AIActor.GetAbsoluteParentRoom();
            
            if (m_StartingGuns != null && m_StartingGuns.Count > 0) {
                foreach (int GunID in m_StartingGuns) { ChangeOrAddGun(m_AIActor.aiShooter.Inventory, GunID); }
            }

            if (UseGlitchShader) { SetupPlayerGlitchShader(); };

            IsConfigured = true;
        }
        
        public void SetupPlayerGlitchShader() {
            float RandomIntervalFloat = Random.Range(0.02f, 0.04f);
            float RandomDispFloat = Random.Range(0.06f, 0.08f);
            float RandomDispIntensityFloat = Random.Range(0.07f, 0.1f);
            float RandomColorProbFloat = Random.Range(0.035f, 0.1f);
            float RandomColorIntensityFloat = Random.Range(0.05f, 0.1f);
            ExpandShaders.Instance.ApplySuperGlitchShader(m_AIActor.sprite, m_Player.sprite, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
            
        }

        public void ModifyCamera(bool value) {
            if (!GameManager.HasInstance || GameManager.Instance.IsLoadingLevel || GameManager.IsReturningToBreach) { return; }
            CameraController mainCameraController = GameManager.Instance.MainCameraController;
            if (!mainCameraController) { return; }
            if (value && m_ParentRoom != null) {
                m_CameraChanged = true;
                mainCameraController.OverrideZoomScale = 0.55f;
                mainCameraController.SetManualControl(true);
                mainCameraController.StopTrackingPlayer();
                mainCameraController.OverridePosition = m_ParentRoom.area.Center + new Vector2(0, 0.75f);
            } else if (!value) {
                m_CameraChanged = false;
                mainCameraController.OverrideZoomScale = 1f;
                mainCameraController.SetManualControl(false);
                mainCameraController.StartTrackingPlayer();
            }
        }

        private void Update() {
            try {
                if (!IsConfigured) { return; }

                if (!m_HasBeenActivated && m_Player && m_Player.GetAbsoluteParentRoom() != null && transform.position.GetAbsoluteRoom() != null && m_Player.GetAbsoluteParentRoom() == transform.position.GetAbsoluteRoom()) {
                    m_Player.PostProcessBeam += HandleBeam;
                    m_AIActor.specRigidbody.CollideWithTileMap = true;
                    m_AIActor.BehaviorOverridesVelocity = true;
                    m_HasBeenActivated = true;                
                    StartCoroutine(DelayedActivate());
                }

                if (m_HasBeenActivated && m_Player == null) { m_Player = GameManager.Instance.BestActivePlayer; }

                if (!m_IntroDoer.m_finished | !IntroDone | m_AIActor.healthHaver.IsDead | m_Player.spriteAnimator.IsPlaying("death_shot") | m_Player.spriteAnimator.IsPlaying("death_shot_armorless")) { return; }

                if (!MovedToCenter) {
                    m_IsPathfindingToCenter = true;
                    MovedToCenter = true;
                    StartCoroutine(PathBackToCenter(m_AIActor, m_ParentRoom.area.UnitCenter));
                }
                
                if (!m_DelayedActive) { return; }

                if (m_AIActor.PlayerTarget == null | m_AIActor.TargetRigidbody == null) { m_AIActor.PlayerTarget = m_Player; }

                if (m_Player && m_AIActor) {
                    UpdateSprites();
                    if (m_AIActor.spriteAnimator.IsPlaying("run_down")) { m_AIActor.spriteAnimator.Stop(); }
                }

                if (m_MirrorGunToggle) {
                    if (m_AIActor.aiShooter.EquippedGun.PickupObjectId != m_Player.CurrentGun.PickupObjectId && ExpandLists.AllowedMimicBossWeapons.Contains(m_Player.CurrentGun.PickupObjectId)) {
                        ChangeOrAddGun(m_AIActor.aiShooter.Inventory, m_Player.CurrentGun.PickupObjectId);
                    } else if (!ExpandLists.AllowedMimicBossWeapons.Contains(m_Player.CurrentGun.PickupObjectId)) {
                        if (m_AIActor.aiShooter.CurrentGun.PickupObjectId != m_StartingGuns[0]) {
                            ChangeOrAddGun(m_AIActor.aiShooter.Inventory, m_StartingGuns[0]);
                        }
                    }

                    if (m_AIActor.aiShooter.CurrentGun.PickupObjectId == m_Player.CurrentGun.PickupObjectId) {
                        if (m_AIActor.aiShooter.CurrentGun.spriteAnimator.enabled) { m_AIActor.aiShooter.CurrentGun.spriteAnimator.enabled = false; }
                        if (m_AIActor.aiShooter.CurrentGun.sprite.spriteId != m_Player.CurrentGun.sprite.spriteId) {
                            m_AIActor.aiShooter.CurrentGun.sprite.SetSprite(m_Player.CurrentGun.sprite.spriteId);
                        }
                    }
                    
                    if ((!m_IsFiring && m_CanAttack && (m_HasShootStyle(ProjectileModule.ShootStyle.Charged) | m_SpecialChargeWeapons.Contains(m_AIActor.aiShooter.CurrentGun.PickupObjectId))) && (m_Player.CurrentGun.IsFiring | m_Player.CurrentGun.IsCharging)) {
                        m_IsFiring = true;
                        StartCoroutine(HandleFireGun());
                    } else if ((m_Player.IsFiring | m_Player.CurrentGun.IsCharging) && !m_IsFiring && m_CanAttack && !m_AIActor.aiShooter.CurrentGun.IsHeroSword) {
                        m_AIActor.aiShooter.CurrentGun.Attack();
                        m_AIActor.aiShooter.CurrentGun.ClearReloadData();
                        m_IsFiring = false;
                    }
                } else if (!m_MirrorGunToggle) {
                    if (!m_StartingGuns.Contains(m_AIActor.aiShooter.CurrentGun.PickupObjectId)) {
                        ChangeOrAddGun(m_AIActor.aiShooter.Inventory, BraveUtility.RandomElement(m_StartingGuns));
                    }

                    if (m_AIActor.aiShooter.CurrentGun.PickupObjectId != 15 | m_PauseAutoFire) {
                        m_RandomFireRate -= BraveTime.DeltaTime;
                    } else {
                        m_RandomFireRate += BraveTime.DeltaTime;
                    }

                    if (m_PauseAutoFire && m_RandomFireRate <= 0) { m_PauseAutoFire = false; }

                    if (m_RandomFireRate <= 0 && m_AIActor.aiShooter.CurrentGun.PickupObjectId != 15) {
                        if (!m_AIActor.aiShooter.CurrentGun.IsFiring) { FireStartingGun(m_AIActor.aiShooter.CurrentGun, true); }
                        m_RandomFireRate = Random.Range(0.6f, 0.85f);
                    } else if (!m_PauseAutoFire && m_AIActor.aiShooter.CurrentGun.PickupObjectId == 15) {
                        if (m_AIActor.aiShooter.CurrentGun.PickupObjectId == 15) { FireStartingGun(m_AIActor.aiShooter.CurrentGun, false); }
                        if (m_RandomFireRate > 4) {
                            m_RandomFireRate = 2;
                            m_PauseAutoFire = true;
                        }
                    } else {
                        if (m_AIActor.aiShooter.CurrentGun.IsFiring) { m_AIActor.aiShooter.CurrentGun.CeaseAttack(); }
                    }
                    
                }
            } catch (System.Exception ex) {
                if (ExpandSettings.debugMode) { Debug.LogException(ex); }
            }
        }
        
        private void LateUpdate() {
            try { 
                if (m_IsDisconnected | !IsConfigured | !m_DelayedActive) { return; }
                if (!m_IntroDoer.m_finished | !IntroDone | m_AIActor.healthHaver.IsDead) { return; }
                
                m_RandomRefresh -= BraveTime.DeltaTime;

                if (!m_IsPathfindingToCenter) {
                    if (m_ParentRoom != null) {
                        Vector2 RoomCenterPosition = m_ParentRoom.area.UnitCenter;
                        float MaxDistanceFromCenterX = (m_ParentRoom.area.dimensions.x / 2f);
                        float MaxDistanceFromCenterY = (m_ParentRoom.area.dimensions.y / 2f);

                        Vector2 CurrentPosition = m_AIActor.gameObject.transform.position;
                        if (Vector2.Distance(CurrentPosition, RoomCenterPosition) > MaxDistanceFromCenterX | Vector2.Distance(CurrentPosition, RoomCenterPosition) > MaxDistanceFromCenterY) {
                            if (!m_RandomMovement) {
                                m_IsPathfindingToCenter = true;
                                StartCoroutine(PathBackToCenter(m_AIActor, RoomCenterPosition));
                            }
                        }
                    }
                }
                
                if (EnableFallBackAttacks && m_RandomRefresh <= 0) {
                    if (m_MirrorGunToggle) {
                        m_MirrorGunToggle = false;
                        m_RandomRefresh = Random.Range(15, 30);
                    } else {
                        m_MirrorGunToggle = true;
                        m_RandomRefresh = Random.Range(10, 18);
                    }
                }

                if (m_BeamSweepingBack) {
                    m_BeamSweepAngle -= (BraveTime.DeltaTime * 30);
                    if (m_BeamSweepAngle <= 0) {
                        m_BeamSweepingBack = false;
                        if (!m_InvertSweepAngle) {
                            m_InvertSweepAngle = true;
                        } else if (m_InvertSweepAngle) {
                            m_InvertSweepAngle = false;
                        }
                    }
                } else {
                    m_BeamSweepAngle += (BraveTime.DeltaTime * 30);
                    if (m_BeamSweepAngle >= 15) {
                        m_BeamSweepingBack = true;
                    }
                }

                if (!m_Player | m_Player.GetAbsoluteParentRoom() == null | m_Player.GetAbsoluteParentRoom() != m_AIActor.ParentRoom) {
                    return;
                }

                if (m_MirrorGunToggle && m_RandomMovement) {
                    m_RandomMovement = false;
                } else if (!m_MirrorGunToggle && !m_RandomMovement) {
                    m_RandomMovement = true;
                    StartCoroutine(HandleRandomPathing(m_AIActor));
                }
                
                if (m_IsPathfindingToCenter | m_RandomMovement | !m_MirrorGunToggle) { return; }

                if (!m_AIActor.BehaviorOverridesVelocity) { m_AIActor.BehaviorOverridesVelocity = true; }

                if (UsesRotationInsteadOfInversion) {
                    m_AIActor.BehaviorVelocity = (Quaternion.Euler(0f, 0f, RotationAngle) * m_AIActor.TargetRigidbody.Velocity).XY();
                } else {
                    m_AIActor.BehaviorVelocity = (m_AIActor.TargetRigidbody.Velocity * -1f);
                }
            } catch (System.Exception ex) {
                if (ExpandSettings.debugMode) { Debug.LogException(ex); }
            }
        }
        
        public void ChangeOrAddGun(GunInventory gunInventory, int pickupObjectID) {
            FieldInfo m_currentGunSet = typeof(GunInventory).GetField("m_currentGun", BindingFlags.Instance | BindingFlags.NonPublic);
            Gun currentGun = gunInventory.CurrentGun;
            if (currentGun != null) {
                currentGun.CeaseAttack(false, null);
                currentGun.PostProcessProjectile -= PostProcessEnemyProjectile;
                currentGun.gameObject.SetActive(false);
            }
            Gun NextGun = null;
            // Find gun in inventory.
            if (gunInventory.AllGuns.Count > 0) {
                foreach (Gun gun in gunInventory.AllGuns) {
                    if (gun.PickupObjectId == pickupObjectID) {
                        NextGun = gun;
                        break;
                    }
                }
            }
            // If gun isn't in inventory, load new gun from PickupObjectDatabase and add it to inventory.
            if (!NextGun) {
                NextGun = Instantiate((PickupObjectDatabase.GetById(pickupObjectID) as Gun));
                NextGun.gameObject.SetActive(false);
                if (NextGun) {
                    gunInventory.AddGunToInventory(NextGun, true);
                    gunInventory.CurrentGun.PostProcessProjectile += PostProcessEnemyProjectile;
                    if (NextGun.IsHeroSword && !NextGun.HeroSwordDoesntBlank) { NextGun.HeroSwordDoesntBlank = true; }
                    return;
                } else {
                    return;
                }
            }
            // If gun was found from AllGuns list, this code will bet set. (if gun was added, this code is never reached since it's not needed)
            m_currentGunSet.SetValue(gunInventory, NextGun);
            gunInventory.CurrentGun.gameObject.SetActive(true);
            gunInventory.CurrentGun.PostProcessProjectile += PostProcessEnemyProjectile;
            if (NextGun.IsHeroSword && !NextGun.HeroSwordDoesntBlank) { NextGun.HeroSwordDoesntBlank = true; }
            return;
        }
        
        private void FireStartingGun(Gun currentGun, bool overrideCoolDown = false) {
            currentGun.Attack();
            if (overrideCoolDown) { currentGun.ClearCooldowns(); }
            currentGun.ClearReloadData();
            return;
        }

        private void PostProcessEnemyProjectile(Projectile source) {
            if (!source) { return; }
            if (m_IsDisconnected) { return; }
            if (m_AIActor.TargetRigidbody != null) { source.specRigidbody.specRigidbody.DeregisterSpecificCollisionException(m_AIActor.TargetRigidbody); }
            // if (m_Player) { m_Player.DoPostProcessProjectile(source); }
            source.SetOwnerSafe(m_AIActor, m_AIActor.ActorName);
            source.Shooter = m_AIActor.specRigidbody;
            source.SetNewShooter(m_AIActor.specRigidbody);
            source.MakeLookLikeEnemyBullet(true);
            // source.RemovePlayerOnlyModifiers();
            if (source.baseData != null) {
                if (!GameManager.IsTurboMode) {
                    source.baseData.speed = (source.baseData.speed / 1.2f);
                } else {
                    source.baseData.speed = (source.baseData.speed / 1.27f);
                }
            }
        }
        
        private void HandleBeam(BeamController obj) {
            if (!obj || !obj.projectile) { return; }
            if (m_IsDisconnected) { return; }
            if (!m_MirrorGunToggle) { return; }
            if (m_AIActor.TargetRigidbody == null) { m_AIActor.PlayerTarget = m_Player; }
            Vector2 SpawnOrigin = m_AIActor.sprite.WorldCenter;
            if (m_AIActor.CurrentGun) { SpawnOrigin = m_AIActor.CurrentGun.sprite.WorldCenter; }
            GameObject gameObject = SpawnManager.SpawnProjectile(obj.projectile.gameObject, SpawnOrigin, Quaternion.identity, true);
            Projectile component = gameObject.GetComponent<Projectile>();
            component.Owner = m_AIActor;
            component.SetOwnerSafe(m_AIActor, m_AIActor.ActorName);
            component.SetNewShooter(m_AIActor.specRigidbody);
            component.MakeLookLikeEnemyBullet(true);            
            BeamController component2 = gameObject.GetComponent<BeamController>();
            BasicBeamController basicBeamController = component2 as BasicBeamController;
            if (basicBeamController) { basicBeamController.SkipPostProcessing = true; }
            component2.Owner = m_AIActor;
            component2.HitsPlayers = true;
            component2.HitsEnemies = false;
            if (component2.IgnoreRigidbodes == null) { component2.IgnoreRigidbodes = new List<SpeculativeRigidbody>(); }
            if (!component2.IgnoreRigidbodes.Contains(m_AIActor.specRigidbody)) { component2.IgnoreRigidbodes.Add(m_AIActor.specRigidbody); }
            float angle = (m_AIActor.TargetRigidbody.GetUnitCenter(ColliderType.HitBox) - m_AIActor.specRigidbody.UnitCenter).ToAngle();
            Vector3 v = BraveMathCollege.DegreesToVector(angle, 1f);
            if (m_AIActor.CurrentGun) {
                float angle2 = (m_AIActor.TargetRigidbody.GetUnitCenter(ColliderType.HitBox) - m_AIActor.CurrentGun.sprite.WorldCenter).ToAngle();
                v = BraveMathCollege.DegreesToVector(angle2, 1f);
            }
            component2.Direction = v;
            if (m_AIActor.CurrentGun) {
                component2.Origin = m_AIActor.CurrentGun.sprite.WorldCenter;
            } else {
                component2.Origin = m_AIActor.specRigidbody.UnitCenter;
            }
            StartCoroutine(HandleFiringBeam(component2));
        }

        // This prevents boss from getting stuck in door ways at the edge of the room.
        private IEnumerator PathBackToCenter(AIActor target, Vector2 returnPosition) {
            if (!target) {
                m_IsPathfindingToCenter = false;
                if (target) { target.BehaviorOverridesVelocity = false; }
                yield break;
            }
            float elapsed = 0f;
            target.BehaviorOverridesVelocity = false;
            target.ClearPath();
            target.PathfindToPosition(returnPosition, canPassOccupied: true);
            yield return null;
            while (!target.PathComplete) {
                elapsed += BraveTime.DeltaTime;
                if (elapsed > 20) {
                    target.BehaviorOverridesVelocity = true;
                    target.ClearPath();
                    m_IsPathfindingToCenter = false;
                    yield break;
                }
                if (m_RandomMovement) { break; }
                yield return null;
            }            
            target.BehaviorOverridesVelocity = true;
            m_IsPathfindingToCenter = false;
            yield break;
        }

        private IEnumerator HandleRandomPathing(AIActor target) {
            IL_RESTART:;
            if (!target | m_MirrorGunToggle | !m_RandomMovement | m_IsPathfindingToCenter) {
                if (target | !m_IsPathfindingToCenter) { target.BehaviorOverridesVelocity = true; }
                yield break;
            }
            float elapsed = 0f;
            float maxPathAttemptTime = Random.Range(2, 10);
            Vector2? PathTarget = GetRandomPathTarget();
            while (!PathTarget.HasValue) {
                if (!target | m_MirrorGunToggle | !m_RandomMovement | m_IsPathfindingToCenter) {
                    if (target | !m_IsPathfindingToCenter) { target.BehaviorOverridesVelocity = true; }
                    break;
                }
                yield return null;
            }
            if (m_MirrorGunToggle | !target) {
                m_RandomMovement = false;
                yield break;
            }
            if (!m_IsPathfindingToCenter) { target.BehaviorOverridesVelocity = false; }
            target.ClearPath();
            target.PathfindToPosition(PathTarget.Value, canPassOccupied: true);
            yield return null;
            while (!target.PathComplete) {
                elapsed += BraveTime.DeltaTime;
                if (elapsed > maxPathAttemptTime) {
                    target.ClearPath();
                    elapsed = 0;
                    maxPathAttemptTime = Random.Range(2, 10);
                    break;
                }
                if (!target | m_MirrorGunToggle | !m_RandomMovement | m_IsPathfindingToCenter) { break; }
                yield return null;
            }
            if (!target | m_MirrorGunToggle | !m_RandomMovement | m_IsPathfindingToCenter) {
                if (target && !m_IsPathfindingToCenter) { target.BehaviorOverridesVelocity = true; }
                yield break;
            } else {
                goto IL_RESTART;
            }
        }

        private Vector2? GetRandomPathTarget() {            
            CellValidator cellValidator = delegate (IntVector2 c) {
                for (int X = 0; X < 2; X++) {
                    for (int Y = 0; Y < 2; Y++) {
                        if (!GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(c.x + X, c.y + Y) | 
                             GameManager.Instance.Dungeon.data[c.x + X, c.y + Y].type == CellType.PIT | 
                             GameManager.Instance.Dungeon.data[c.x + X, c.y + Y].isOccupied |
                             GameManager.Instance.Dungeon.data[c.x + X, c.y + Y].type == CellType.WALL)
                        {
                            return false;
                        }
                    }
                }
                return true;
            };
            IntVector2? m_PossibleLocation = m_ParentRoom.GetRandomAvailableCell(new IntVector2?(new IntVector2(2, 2)), new CellTypes?(CellTypes.FLOOR), false, cellValidator);

            if (m_PossibleLocation.HasValue) {
                return m_PossibleLocation.Value.ToVector2();
            } else {
                return null;
            }
        }

        private IEnumerator HandleFireGun() {

            if (!m_CanAttack | m_IsDisconnected) {
                if (m_AIActor.aiShooter.CurrentGun.IsFiring | m_AIActor.aiShooter.CurrentGun.IsCharging) {
                    m_AIActor.aiShooter.CurrentGun.CeaseAttack(m_MirrorGunToggle);
                }
                m_AIActor.aiShooter.CurrentGun.ClearReloadData();
                m_IsFiring = false;
                yield break;
            }
           
            yield return null;
            
            if (!m_Player.CurrentGun.IsCharging && !m_Player.CurrentGun.IsFiring) {
                m_ChargingTime = 0;
                m_IsFiring = false;
                m_AIActor.aiShooter.CurrentGun.ClearReloadData();
                yield break;
            }

            float? ChargeTime = m_AIActor.aiShooter.CurrentGun.DefaultModule.maxChargeTime;
            m_ChargingTime = 0;
            yield return null;
            if (ChargeTime.HasValue) {
                while ((m_Player.CurrentGun.IsCharging | m_Player.CurrentGun.IsFiring) && (m_HasShootStyle(ProjectileModule.ShootStyle.Charged) | m_SpecialChargeWeapons.Contains(m_AIActor.aiShooter.CurrentGun.PickupObjectId)) ) {
                    m_ChargingTime += BraveTime.DeltaTime;
                    if (!m_MirrorGunToggle | m_IsDisconnected) {
                        m_IsFiring = false;
                        m_ChargingTime = 0;
                        m_AIActor.aiShooter.CurrentGun.ClearReloadData();
                        yield break;
                    }
                    yield return null;
                }
            }
            if (!m_MirrorGunToggle | m_IsDisconnected) {
                m_ChargingTime = 0;
                m_IsFiring = false;
                yield break;
            }
            yield return null;
            if ((m_ChargingTime >= ChargeTime.Value) | (!m_HasShootStyle(ProjectileModule.ShootStyle.Charged) && !m_SpecialChargeWeapons.Contains(m_AIActor.aiShooter.CurrentGun.PickupObjectId))) {
                while (m_AIActor.aiShooter.CurrentGun.Attack() != Gun.AttackResult.Success && (m_Player.CurrentGun.IsCharging | m_Player.CurrentGun.IsFiring)) {
                    if (m_IsDisconnected) {
                        m_ChargingTime = 0;
                        m_IsFiring = false;
                        m_AIActor.aiShooter.CurrentGun.ClearReloadData();
                        yield break;
                    }
                    if (!m_MirrorGunToggle | (!m_HasShootStyle(ProjectileModule.ShootStyle.Charged) && !m_SpecialChargeWeapons.Contains(m_AIActor.aiShooter.CurrentGun.PickupObjectId))) {
                        if (m_AIActor.aiShooter.CurrentGun.IsFiring | m_AIActor.aiShooter.CurrentGun.IsCharging) {
                            m_AIActor.aiShooter.CurrentGun.CeaseAttack(m_MirrorGunToggle);
                        }
                        if (m_AIActor.aiShooter.CurrentGun.IsCharging) {
                            m_AIActor.aiShooter.CurrentGun.ContinueAttack(m_MirrorGunToggle);
                        }
                        m_ChargingTime = 0;
                        m_IsFiring = false;
                        m_AIActor.aiShooter.CurrentGun.ClearReloadData();
                        yield break;
                    }
                    if (!m_AIActor.aiShooter.CurrentGun.IsFiring && !m_AIActor.aiShooter.CurrentGun.IsCharging) {
                        m_AIActor.aiShooter.CurrentGun.Attack();
                    }
                    m_AIActor.aiShooter.CurrentGun.ClearReloadData();
                    yield return null;
                }
            }
            yield return null;
            if (m_IsDisconnected) { m_AIActor.aiShooter.CurrentGun.CeaseAttack(false); }
            m_AIActor.aiShooter.CurrentGun.ClearReloadData();
            m_IsFiring = false;
            yield break;
        }
        
        private IEnumerator DelayedActivate() {
            yield return new WaitForSeconds(1.5f);
            if (m_IsDisconnected) { yield break; }
            m_AIActor.aiShooter.ToggleHandRenderers(false, "Using Player's Gun Now");
            if (m_Player) { aiActor.PlayerTarget = m_Player; }
            m_AIActor.spriteAnimator.Stop();
            m_DelayedActive = true;
            yield break;
        }

        private IEnumerator HandleFiringBeam(BeamController beam) {
            float elapsed = 0f;
            yield return null;
            while (m_Player && m_Player.IsFiring && this && m_AIActor.sprite && m_AIActor.healthHaver && !m_IsDisconnected) {
                elapsed += BraveTime.DeltaTime;
                if (!m_AIActor.TargetRigidbody) { m_AIActor.PlayerTarget = m_Player; }
                if (!m_MirrorGunToggle) {
                    beam.CeaseAttack();
                    beam.DestroyBeam();
                    yield break;
                }
                if (m_AIActor.CurrentGun) {
                    beam.Origin = m_AIActor.CurrentGun.sprite.WorldCenter;
                    beam.LateUpdatePosition(m_AIActor.CurrentGun.sprite.WorldCenter);
                } else {
                    beam.Origin = m_AIActor.specRigidbody.UnitCenter;
                    beam.LateUpdatePosition(m_AIActor.specRigidbody.UnitCenter);
                }
                // beam.Origin = m_AIActor.specRigidbody.UnitCenter;
                // beam.LateUpdatePosition(m_AIActor.specRigidbody.UnitCenter);
                if (m_Player) {
                    float angle = ((m_AIActor.TargetRigidbody.GetUnitCenter(ColliderType.HitBox) - m_AIActor.specRigidbody.UnitCenter).ToAngle());
                    if (m_AIActor.CurrentGun) { angle = ((m_AIActor.TargetRigidbody.GetUnitCenter(ColliderType.HitBox) - m_AIActor.CurrentGun.sprite.WorldCenter).ToAngle()); }
                    if (m_InvertSweepAngle) {
                        angle = (angle + m_BeamSweepAngle);
                    } else {
                        angle = (angle - m_BeamSweepAngle);
                    }
                    beam.Direction = BraveMathCollege.DegreesToVector(angle, 1);
                    if (m_Player.IsDodgeRolling) {
                        beam.CeaseAttack();
                        beam.DestroyBeam();
                        yield break;
                    }
                }
                yield return null;
            }
            beam.CeaseAttack();
            beam.DestroyBeam();
            yield break;
        }
        
        // Modified version of PlayerController.GetMirrorSprite()
        private void UpdateSprites() {
            if (m_AIActor.aiAnimator.enabled) { m_AIActor.aiAnimator.enabled = false; }
            if (m_Player.IsDodgeRolling | !m_AIActor.aiShooter.CurrentGun) {
                if (m_Player.characterIdentity == PlayableCharacters.Eevee) {
                    m_AIActor.sprite.SetSprite(m_Player.sprite.Collection, m_Player.sprite.spriteId);
                } else {
                    m_AIActor.sprite.SetSprite(m_Player.sprite.spriteId);
                }
                return;
            }
            float Angle = -90;
            float CurrentAngle = m_AIActor.aiShooter.CurrentGun.CurrentAngle;
            // Player sprite is flipped when facing left. Facing left sprite IDs don't exist. Not sure how to replicate this on an AIActor without breaking things.
            // Will only allow Up or down facing animations for now.
            if (CurrentAngle < 155 && CurrentAngle > 25) { Angle = 90; }            
            string baseAnimationName = GetBaseAnimationName(m_Player, m_AIActor.specRigidbody.Velocity.WithY(m_AIActor.specRigidbody.Velocity.y * -1f), Angle, true, false);
            tk2dSpriteAnimationClip clipByName = m_Player.spriteAnimator.GetClipByName(baseAnimationName);
            int currentFrame = m_Player.spriteAnimator.CurrentFrame;
            if (clipByName != null && currentFrame >= 0 && currentFrame < clipByName.frames.Length) {
                if (m_Player.characterIdentity == PlayableCharacters.Eevee) {
                    m_AIActor.sprite.SetSprite(m_Player.sprite.Collection, clipByName.frames[currentFrame].spriteId);
                } else {
                    m_AIActor.sprite.SetSprite(clipByName.frames[currentFrame].spriteId);
                }
            } else {
                if (m_Player.characterIdentity == PlayableCharacters.Eevee) {
                    m_AIActor.sprite.SetSprite(m_Player.sprite.Collection, m_Player.sprite.spriteId);
                } else {
                    m_AIActor.sprite.SetSprite(m_Player.sprite.spriteId);
                }
            }
            return;
        }

        protected virtual string GetBaseAnimationName(PlayerController Player, Vector2 v, float gunAngle, bool invertThresholds = false, bool forceTwoHands = false) {
            string text = string.Empty;
            bool hasGun = Player.CurrentGun != null;
            if (hasGun && Player.CurrentGun.Handedness == GunHandedness.NoHanded) { forceTwoHands = true; }
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.END_TIMES) { hasGun = false; }
            float NorthWest = 155f;
            float EastEastNorth = 25f;
            if (invertThresholds) { NorthWest = -155f; EastEastNorth -= 50f; }
            float NorthNorthWest = 120f;
            float NorthEast = 60f;
            float SouthEast = -60f;
            float SouthWest = -120f;
            bool flag2 = gunAngle <= NorthWest && gunAngle >= EastEastNorth;
            if (invertThresholds) {
                flag2 = (gunAngle <= NorthWest || gunAngle >= EastEastNorth);
            }
            if (Player.IsGhost) {
                if (flag2) {
                    if (gunAngle < NorthNorthWest && gunAngle >= NorthEast) {
                        text = "ghost_idle_back";
                    } else {
                        float num7 = 105f;
                        if (Mathf.Abs(gunAngle) > num7) {
                            text = "ghost_idle_back_left";
                        } else {
                            text = "ghost_idle_back_right";
                        }
                    }
                } else if (gunAngle <= SouthEast && gunAngle >= SouthWest) {
                    text = "ghost_idle_front";
                } else {
                    float num8 = 105f;
                    if (Mathf.Abs(gunAngle) > num8) {
                        text = "ghost_idle_left";
                    } else {
                        text = "ghost_idle_right";
                    }
                }
            } else if (Player.IsFlying) {
                if (flag2) {
                    if (gunAngle < NorthNorthWest && gunAngle >= NorthEast) {
                        text = "jetpack_up";
                    } else {
                        text = "jetpack_right_bw";
                    }
                } else if (gunAngle <= SouthEast && gunAngle >= SouthWest) {
                    text = ((!RenderBodyHand(Player)) ? "jetpack_down" : "jetpack_down_hand");
                } else {
                    text = ((!RenderBodyHand(Player)) ? "jetpack_right" : "jetpack_right_hand");
                }
            } else if (v == Vector2.zero || Player.IsStationary) {
                if (Player.IsPetting) {
                    text = "pet";
                } else if (flag2) {
                    if (gunAngle < NorthNorthWest && gunAngle >= NorthEast) {
                        string text2 = ((!forceTwoHands && hasGun) || Player.ForceHandless) ? ((!RenderBodyHand(Player)) ? "idle_backward" : "idle_backward_hand") : "idle_backward_twohands";
                        text = text2;
                    } else {
                        string text3 = ((!forceTwoHands && hasGun) || Player.ForceHandless) ? "idle_bw" : "idle_bw_twohands";
                        text = text3;
                    }
                } else if (gunAngle <= SouthEast && gunAngle >= SouthWest) {
                    string text4 = ((!forceTwoHands && hasGun) || Player.ForceHandless) ? ((!RenderBodyHand(Player)) ? "idle_forward" : "idle_forward_hand") : "idle_forward_twohands";
                    text = text4;
                } else {
                    string text5 = ((!forceTwoHands && hasGun) || Player.ForceHandless) ? ((!RenderBodyHand(Player)) ? "idle" : "idle_hand") : "idle_twohands";
                    text = text5;
                }
            } else if (flag2) {
                string text6 = ((!forceTwoHands && hasGun) || Player.ForceHandless) ? "run_right_bw" : "run_right_bw_twohands";
                if (gunAngle < NorthNorthWest && gunAngle >= NorthEast) {
                    text6 = (((!forceTwoHands && hasGun) || Player.ForceHandless) ? ((!RenderBodyHand(Player)) ? "run_up" : "run_up_hand") : "run_up_twohands");
                }
                text = text6;
            } else {
                string text7 = "run_right";
                if (gunAngle <= SouthEast && gunAngle >= SouthWest) {
                    text7 = "run_down";
                }
                if ((forceTwoHands || !hasGun) && !Player.ForceHandless) {
                    text7 += "_twohands";
                } else if (RenderBodyHand(Player)) {
                    text7 += "_hand";
                }
                text = text7;
            }
            if (Player.UseArmorlessAnim && !Player.IsGhost) {
                text += "_armorless";
            }
            return text;
        }

        private bool RenderBodyHand(PlayerController player) {
            return !player.ForceHandless && player.CurrentSecondaryGun == null && (player.CurrentGun == null || player.CurrentGun.Handedness != GunHandedness.TwoHanded);
        }
        
        public void Disconnect(bool externalDisconnect = false) {
            if (m_ParentRoom != null) { m_ParentRoom.CompletelyPreventLeaving = false; }
            m_IsDisconnected = true;
            m_PauseAutoFire = true;
            m_MirrorGunToggle = true;
            if (m_Player) { m_Player.PostProcessBeam -= HandleBeam; }
            if (m_AIActor) {
                /*if (m_AIActor.aiShooter && m_AIActor.aiShooter.CurrentGun && m_AIActor.aiShooter.CurrentGun.IsFiring) {
                    m_AIActor.aiShooter.CurrentGun.CeaseAttack(false);
                    m_AIActor.aiShooter.CurrentGun.PostProcessProjectile -= PostProcessEnemyProjectile;
                }*/
                m_AIActor.BehaviorVelocity = Vector2.zero;
                m_AIActor.BehaviorOverridesVelocity = false;
            }
            if (!externalDisconnect) { StopAllCoroutines(); }
        }

        protected override void OnDestroy() {
            if (m_CameraChanged) { ModifyCamera(false); }
            if (!m_IsDisconnected) { Disconnect(); }
            base.OnDestroy();
        }
    }
}

