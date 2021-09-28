using Gungeon;
using System;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;


namespace ExpandTheGungeon.ItemAPI {

    public class BulletKinGun : Gun {

        public static int BulletKinGunID = -1;
                
        public static void Init() {
            Gun bulletkinGun = ETGMod.Databases.Items.NewGun("The Bullet Kin Gun", "bulletkin_gun");
            Game.Items.Rename("outdated_gun_mods:the_bullet_kin_gun", "ex:bulletkin_gun");
            bulletkinGun.SetShortDescription("Fires Bullet Kin...");
            bulletkinGun.SetLongDescription("This gun fires Bullet Kins....Don't ask questions. Just accept it.");
            GunExt.SetupSprite(bulletkinGun, null, "bulletkin_gun_idle_001", 18);
            bulletkinGun.AddProjectileModuleFrom("Magnum", true, false);
            bulletkinGun.DefaultModule.ammoCost = 1;
            bulletkinGun.DefaultModule.angleVariance = 0;
            bulletkinGun.DefaultModule.numberOfShotsInClip = 1;
            bulletkinGun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            bulletkinGun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            bulletkinGun.DefaultModule.cooldownTime = 0.15f;
            bulletkinGun.DefaultModule.positionOffset = new Vector3(0.05f, 0.01f);
            bulletkinGun.reloadTime = 0.6f;
            bulletkinGun.gunClass = GunClass.PISTOL;
            bulletkinGun.ammo = 140;
            bulletkinGun.SetBaseMaxAmmo(140);
            bulletkinGun.quality = ItemQuality.C;
            if (!ExpandSettings.EnableEXItems) { bulletkinGun.quality = ItemQuality.EXCLUDED; }
            bulletkinGun.alternateSwitchGroup = (PickupObjectDatabase.GetById(150) as Gun).gunSwitchGroup;
            bulletkinGun.gunSwitchGroup = (PickupObjectDatabase.GetById(150) as Gun).gunSwitchGroup;
            bulletkinGun.encounterTrackable.EncounterGuid = "43a080b46fa448ef8d2be35f93ab6e64";
            bulletkinGun.gameObject.AddComponent<ExpandFireEnemiesGunMod>();

            ETGMod.Databases.Items.Add(bulletkinGun);

            BulletKinGunID = bulletkinGun.PickupObjectId;
        }
    }


    public class ExpandFireEnemiesGunMod : MonoBehaviour, IGunInheritable {

        public ExpandFireEnemiesGunMod() {
            EnemyGUIDs = new List<string>() { "01972dee89fc4404a5c408d50007dad5" };
            m_baseDamageMod = 1.1f;
        }

        public List<string> EnemyGUIDs;
        
        private bool m_FiresJammedEnemies;
        private float m_baseDamageMod;

        private Gun m_gun;

        private void Awake() {
            if (!m_gun) { m_gun = GetComponent<Gun>(); }
            if (m_gun) {
                m_gun.OnInitializedWithOwner = (Action<GameActor>)Delegate.Combine(m_gun.OnInitializedWithOwner, new Action<GameActor>(OnGunInitialized));
                if (m_gun.CurrentOwner != null) { OnGunInitialized(m_gun.CurrentOwner); }
            }
        }

        private void Start() { }
        private void Update() {
            if (m_gun && m_gun.CurrentOwner && m_gun.CurrentOwner && (m_gun.CurrentOwner is PlayerController)) {
                PlayerController owner = (m_gun.CurrentOwner as PlayerController);
                if ((owner.HasGun(39) | owner.HasPassiveItem(815))&& !EnemyGUIDs.Contains("4d37ce3d666b4ddda8039929225b7ede")) {
                    EnemyGUIDs.Add("4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
                } else if (!owner.HasGun(39) && !owner.HasPassiveItem(815) && EnemyGUIDs.Contains("4d37ce3d666b4ddda8039929225b7ede")) {
                    EnemyGUIDs.Remove("4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
                }
                if ((owner.HasGun(51) | owner.HasPassiveItem(815)) && !EnemyGUIDs.Contains("b54d89f9e802455cbb2b8a96a31e8259") && !EnemyGUIDs.Contains("128db2f0781141bcb505d8f00f9e4d47")) {
                    EnemyGUIDs.Add("128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
                    EnemyGUIDs.Add("b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
                } else if (!owner.HasGun(51) && !owner.HasPassiveItem(815) && EnemyGUIDs.Contains("b54d89f9e802455cbb2b8a96a31e8259") && EnemyGUIDs.Contains("128db2f0781141bcb505d8f00f9e4d47")) {
                    EnemyGUIDs.Remove("128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
                    EnemyGUIDs.Remove("b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
                }
                if ((owner.HasPassiveItem(572) | owner.HasPassiveItem(815)) && !EnemyGUIDs.Contains("76bc43539fc24648bff4568c75c686d1")) {
                    EnemyGUIDs.Add("76bc43539fc24648bff4568c75c686d1"); // chicken
                } else if (!owner.HasPassiveItem(572) && !owner.HasPassiveItem(815) && EnemyGUIDs.Contains("76bc43539fc24648bff4568c75c686d1")) {
                    EnemyGUIDs.Remove("76bc43539fc24648bff4568c75c686d1"); // chicken
                }
                if ((owner.HasPassiveItem(407) | owner.HasPassiveItem(815)) && !m_FiresJammedEnemies) {
                    m_FiresJammedEnemies = true;
                } else if (!owner.HasPassiveItem(407) && !owner.HasPassiveItem(815) && m_FiresJammedEnemies) {
                    m_FiresJammedEnemies = false;
                }
            }
        }
        
        private void OnGunInitialized(GameActor actor) {
            if (actor == null) { return; }
            if (actor is PlayerController) {
                // explosionData.ignoreList = new List<SpeculativeRigidbody>() { (actor as PlayerController).specRigidbody };
                m_gun.PostProcessProjectile += EXPostProcessProjectile;
            }
        }
        
        public void EXPostProcessProjectile(Projectile projectile) {
            
            if (EnemyGUIDs == null | EnemyGUIDs.Count <= 0) { return; }
                        
            AIActor sourceActor = EnemyDatabase.GetOrLoadByGuid(BraveUtility.RandomElement(EnemyGUIDs));

            if (!sourceActor) { return; }

            float damageMod = m_baseDamageMod;

            if (projectile.sprite && projectile.sprite.renderer) { projectile.sprite.renderer.enabled = false; }

            if (sourceActor.EnemyGuid == "128db2f0781141bcb505d8f00f9e4d47" | sourceActor.EnemyGuid == "b54d89f9e802455cbb2b8a96a31e8259") {
                damageMod += 0.35f;
            }
            
            if (m_FiresJammedEnemies) { damageMod += 0.25f; }

            projectile.baseData.damage *= damageMod;

            if (sourceActor.EnemyGuid == "76bc43539fc24648bff4568c75c686d1") {
                projectile.baseData.damage /= 2f;
                projectile.AppliesStun = true;
                projectile.AppliedStunDuration = 3f;
                projectile.StunApplyChance = 0.4f;
            }

            if (!sourceActor.gameObject.GetComponent<ExplodeOnDeath>() && sourceActor.EnemyGuid != "76bc43539fc24648bff4568c75c686d1") {
                projectile.baseData.force += 5;
            }

            projectile.pierceMinorBreakables = true;

            Vector3 targetPosition = projectile.transform.position;
            AIActor targetAIActor = Instantiate(sourceActor, targetPosition, projectile.transform.rotation);

            if (projectile.Owner && projectile.Owner is PlayerController) {
                float ScaleMod = (projectile.Owner as PlayerController).BulletScaleModifier;
                targetAIActor.gameObject.layer = LayerMask.NameToLayer("Unpixelated");
                targetAIActor.EnemyScale = new Vector2(ScaleMod, ScaleMod);
                SpriteOutlineManager.ChangeOutlineLayer(targetAIActor.sprite, LayerMask.NameToLayer("Unpixelated"));
                if (targetAIActor.EnemyScale != Vector2.one) {
                    targetAIActor.HasShadow = false;
                    Destroy(targetAIActor.ShadowObject);
                };
            }
            targetAIActor.specRigidbody.enabled = false;
            targetAIActor.IgnoreForRoomClear = true;
            targetAIActor.IsHarmlessEnemy = true;
            targetAIActor.CanTargetEnemies = true;
            targetAIActor.CanTargetPlayers = false;
            targetAIActor.procedurallyOutlined = false;
            if (!m_FiresJammedEnemies && targetAIActor.EnemyGuid != "76bc43539fc24648bff4568c75c686d1") {
                if (targetAIActor.EnemyGuid == "01972dee89fc4404a5c408d50007dad5") {
                    targetAIActor.RegisterOverrideColor(new Color32(255, 240, 190, 220), "Pale BulletKin");
                } else if (targetAIActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    targetAIActor.RegisterOverrideColor(new Color32(170, 170, 170, 190), "Pale Generic Enemy");
                } else {
                    targetAIActor.RegisterOverrideColor(new Color32(160, 160, 160, 170), "Pale Generic Enemy");
                }
            }
            
            if (targetAIActor.specRigidbody.GetPixelCollider(ColliderType.Ground) != null) {
                PixelCollider EnemyCollider = targetAIActor.specRigidbody.GetPixelCollider(ColliderType.HitBox);
                PixelCollider NewProjCollider = ExpandUtility.DuplicatePixelCollider(EnemyCollider);
                NewProjCollider.CollisionLayer = CollisionLayer.Projectile;
                projectile.specRigidbody.PixelColliders = new List<PixelCollider>() { NewProjCollider };
                projectile.specRigidbody.Reinitialize();
            }
            
            targetAIActor.gameObject.transform.parent = projectile.transform;
            
            if (sourceActor.EnemyGuid == "01972dee89fc4404a5c408d50007dad5" | sourceActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                // targetPosition -= new Vector3(0.5f, 0.5f);
                targetAIActor.gameObject.transform.localPosition -= new Vector3(0.5f, 0.5f);
            } else if (sourceActor.EnemyGuid == "128db2f0781141bcb505d8f00f9e4d47" | sourceActor.EnemyGuid == "b54d89f9e802455cbb2b8a96a31e8259") {
                // targetPosition -= new Vector3(0.5f, 1);
                targetAIActor.gameObject.transform.localPosition -= new Vector3(0.5f, 1);
            }

            targetAIActor.sprite.UpdateZDepth();

            if (m_FiresJammedEnemies) { targetAIActor.BecomeBlackPhantom(); }

            projectile.OnDestruction += OnDestruction;
        }
        

        public void OnDestruction(Projectile projectile) {
            
            AIActor childAIActor = projectile.GetComponentInChildren<AIActor>();
            
            List<SpeculativeRigidbody> m_IgnoreRigidBodies = new List<SpeculativeRigidbody>();
            
            if (childAIActor) {
                childAIActor.specRigidbody.enabled = true;
                childAIActor.specRigidbody.HitboxPixelCollider.IsTrigger = true;
                childAIActor.specRigidbody.HitboxPixelCollider.CollisionLayerIgnoreOverride |= CollisionMask.LayerToMask(CollisionLayer.Projectile);
                childAIActor.specRigidbody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));
                childAIActor.sprite.UpdateZDepth();
                childAIActor.specRigidbody.UpdateCollidersOnScale = true;
                childAIActor.specRigidbody.UpdateCollidersOnRotation = true;
                childAIActor.specRigidbody.Reinitialize();
                childAIActor.specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(childAIActor.specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(HandlePreCollision));

                if (m_FiresJammedEnemies && UnityEngine.Random.value <= 0.9f) { childAIActor.CanDropCurrency = false; }

                Dungeonator.RoomHandler CurrentRoom = projectile.transform.position.GetAbsoluteRoom();

                if (childAIActor.EnemyGuid == "128db2f0781141bcb505d8f00f9e4d47" | childAIActor.EnemyGuid == "b54d89f9e802455cbb2b8a96a31e8259") {
                    childAIActor.healthHaver.spawnBulletScript = false;
                }

                if (CurrentRoom == null) {
                    childAIActor.gameObject.transform.parent = null;
                    if (childAIActor.gameObject.GetComponent<ExplodeOnDeath>() && childAIActor.gameObject.GetComponent<ExplodeOnDeath>().explosionData != null) {
                        childAIActor.gameObject.GetComponent<ExplodeOnDeath>().explosionData.ignoreList = new List<SpeculativeRigidbody>();
                        childAIActor.gameObject.GetComponent<ExplodeOnDeath>().explosionData.damageToPlayer = 0;
                        if (projectile.Owner && (projectile.Owner is PlayerController)) {
                            childAIActor.gameObject.GetComponent<ExplodeOnDeath>().explosionData.ignoreList.Add(projectile.Owner.specRigidbody);
                        }
                    }
                    childAIActor.healthHaver.ApplyDamage(10000, Vector2.zero, "Self Destruct", CoreDamageTypes.None, DamageCategory.Unstoppable, true, ignoreDamageCaps: true);
                    return;
                }
                childAIActor.gameObject.transform.parent = CurrentRoom.hierarchyParent;
                float SurvivalOdds = 0.08f;
                if (m_FiresJammedEnemies) { SurvivalOdds += 0.03f; }
                if (UnityEngine.Random.value <= SurvivalOdds && CurrentRoom.HasActiveEnemies(Dungeonator.RoomHandler.ActiveEnemyType.RoomClear)) {
                    if (childAIActor.EnemyScale == Vector2.one) { childAIActor.procedurallyOutlined = true; };
                    ExpandUtility.CorrectForWalls(childAIActor);
                    childAIActor.CanTargetEnemies = true;
                    childAIActor.CanTargetPlayers = false;
                    childAIActor.IgnoreForRoomClear = true;

                    if (childAIActor.gameObject.GetComponent<ObjectVisibilityManager>()) {
                        childAIActor.gameObject.GetComponent<ObjectVisibilityManager>().Initialize(CurrentRoom, true);
                    }
                    if (childAIActor.bulletBank) {
                        childAIActor.bulletBank.OnProjectileCreated = (Action<Projectile>)Delegate.Combine(childAIActor.bulletBank.OnProjectileCreated, new Action<Projectile>(HandleCompanionPostProcessProjectile));
                    }
                    if (childAIActor.aiShooter) {
                        childAIActor.aiShooter.PostProcessProjectile = (Action<Projectile>)Delegate.Combine(childAIActor.aiShooter.PostProcessProjectile, new Action<Projectile>(HandleCompanionPostProcessProjectile));
                    }
                    childAIActor.ConfigureOnPlacement(CurrentRoom);
                    childAIActor.gameObject.AddComponent<KillOnRoomClear>();
                    return;
                } else {
                    if (CurrentRoom != null) {
                        childAIActor.ConfigureOnPlacement(CurrentRoom);
                        childAIActor.gameObject.transform.parent = CurrentRoom.hierarchyParent;
                    } else {
                        childAIActor.gameObject.transform.parent = null;
                    }
                    if (childAIActor.gameObject.GetComponent<ExplodeOnDeath>() && childAIActor.gameObject.GetComponent<ExplodeOnDeath>().explosionData != null) {
                        childAIActor.gameObject.GetComponent<ExplodeOnDeath>().explosionData.ignoreList = new List<SpeculativeRigidbody>();
                        childAIActor.gameObject.GetComponent<ExplodeOnDeath>().explosionData.damageToPlayer = 0;
                        if (projectile.Owner && (projectile.Owner is PlayerController)) {
                            childAIActor.gameObject.GetComponent<ExplodeOnDeath>().explosionData.ignoreList.Add(projectile.Owner.specRigidbody);
                        }
                    }
                    childAIActor.healthHaver.ApplyDamage(10000, Vector2.zero, "Self Destruct", CoreDamageTypes.None, DamageCategory.Unstoppable, true, ignoreDamageCaps: true);
                    return;
                }
            }

        }

        protected static void HandlePreCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (otherRigidbody.aiActor && otherRigidbody.healthHaver && !otherRigidbody.healthHaver.IsBoss) { PhysicsEngine.SkipCollision = true; }
        }


        protected void HandleCompanionPostProcessProjectile(Projectile obj) {
            if (obj) {
                obj.collidesWithPlayer = false;
                obj.TreatedAsNonProjectileForChallenge = true;

                obj.baseData.damage *= 8f;
                if (m_FiresJammedEnemies) { obj.baseData.damage *= 2; }
                obj.RuntimeUpdateScale(1f / obj.AdditionalScaleMultiplier);
                obj.RuntimeUpdateScale(0.75f);
            }
        }       

        public void InheritData(Gun sourceGun) { }

        public void MidGameSerialize(List<object> data, int dataIndex) { }

        public void MidGameDeserialize(List<object> data, ref int dataIndex) { dataIndex++; }

        private void OnDestroy() { }
    }
}

