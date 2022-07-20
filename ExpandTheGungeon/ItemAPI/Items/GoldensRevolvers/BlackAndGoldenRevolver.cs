using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.SpriteAPI;
using Gungeon;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI
{
    public class BlackAndGoldenRevolver : GunBehaviour
    {
        public static int GoldenRevolverID = -1;
        public static int BlackRevolverID = -1;
        public static Dictionary<string, float> exceptionEnemies = new Dictionary<string, float>();

        public static GameObject WestBrosBlackRevolverProjectile;
        public static GameObject WestBrosGoldenRevolverProjectile;

        public static readonly List<string> ProjectileSpriteList = new List<string>()
        {
            "gr_black_revolver_projectile_001",
            "gr_black_revolver_projectile_002",
            "gr_black_revolver_projectile_003",
            "gr_black_revolver_projectile_004",
            "gr_black_revolver_projectile_005",
            "gr_black_revolver_projectile_006"
        };


        public static void AddBothVariants()
        {
            WestBrosBlackRevolverProjectile = ExpandAssets.LoadAsset<GameObject>("WestBrosBlackRevolverProjectile");
            WestBrosGoldenRevolverProjectile = ExpandAssets.LoadAsset<GameObject>("WestBrosGoldenRevolverProjectile");
            GameObject WestBrosBlackRevolverProjectileChild = WestBrosBlackRevolverProjectile.transform.Find("Sprite").gameObject;
            GameObject WestBrosGoldenRevolverProjectileChild = WestBrosGoldenRevolverProjectile.transform.Find("Sprite").gameObject;
            
            Add(true);
            Add(false);

            if (ExpandSettings.debugMode)
            {
                Debug.Log("[ExpandTheGungeon] Now setting up projectile hook");
            }

            ProjectileHookClass.AddHook();

            if (ExpandSettings.debugMode)
            {
                Debug.Log("[ExpandTheGungeon] Done setting up projectile hook");
            }

            foreach (var item in EnemyDatabase.Instance.Entries)
            {
                if (item != null)
                {
                    var enemy = EnemyDatabase.GetOrLoadByGuid(item.myGuid);

                    if (enemy && enemy.BlackPhantomProperties != null && enemy.healthHaver && !enemy.healthHaver.healthIsNumberOfHits && !enemy.healthHaver.IsBoss)
                    {
                        float jammedHealthMultiplier = 1 + enemy.BlackPhantomProperties.BonusHealthPercentIncrease + BlackPhantomProperties.GlobalPercentIncrease;

                        if (enemy.BlackPhantomProperties.MaxTotalHealth > 0f && enemy.BlackPhantomProperties.MaxTotalHealth < enemy.healthHaver.GetMaxHealth() * jammedHealthMultiplier)
                        {
                            var ratio = enemy.BlackPhantomProperties.MaxTotalHealth / enemy.healthHaver.GetMaxHealth();

                            exceptionEnemies.Add(enemy.EnemyGuid, ratio);
                        }
                        
                    }
                }
            }
            if (ExpandSettings.debugMode)
            {
                Debug.Log("[ExpandTheGungeon] Done setting up black and golden revolver");
            }
        }

        private static void Add(bool isGoldenVersion)
        {

            string upperColor = isGoldenVersion ? "Golden" : "Black";
            string lowerColor = isGoldenVersion ? "golden" : "black";

            Gun gun = ETGMod.Databases.Items.NewGun($"{upperColor} Revolver", $"gr_{lowerColor}_revolver");
            
            Game.Items.Rename($"outdated_gun_mods:{lowerColor}_revolver", $"ex:{lowerColor}_revolver");
            
            gun.gameObject.AddComponent<BlackAndGoldenRevolver>();
            gun.SetShortDescription("Six Deep");
            
            string longDescription = "Bullets fired from this cursed revolver jam enemies on hit, but also completely ignore their increased strength, damaging them as if they were unjammed.";

            if (isGoldenVersion)
            {
                longDescription += "\n\nThree bullet kin, that were completely unalike each other, set aside their differences to obtain this revolver, that was once carried by the bearer of a terrible curse. Their presence can still be felt while wielding it.";
            }
            else
            {
                longDescription += "\n\nThis revolver was once carried by the bearer of a terrible curse. It is cold to the touch. A dark wind blows.";
            }
            
            gun.SetLongDescription(longDescription);
            
            // frame rate can be adjusted if we don't like the current idle animation speed
            gun.SetupSprite(null, $"gr_{lowerColor}_revolver_idle_001", 5);
            
            var idleAnimation = gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.idleAnimation);
            
            var list = idleAnimation.frames.ToList();
            
            // manually add the reversal of the animation so we don't load the same sprite multiple times for no reason (these frames don't really need to be duplicated)
            list.Add(list[2]);
            list.Add(list[1]);
            
            // add a few more frames of the idle frame so the animation has a slight pause before looping
            for (int i = 0; i < 5; i++)
            {
                list.Add(list[0]);
            }
            
            idleAnimation.frames = list.ToArray();
            
            gun.SetAnimationFPS(gun.shootAnimation, 12);
            gun.SetAnimationFPS(gun.reloadAnimation, 14);
            
            // Every modded gun has base projectile it works with that is borrowed from other guns in the game.
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.
            // which means its the ETGMod.Databases.Items / PickupObjectDatabase.Instance.InternalGetByName name, aka the pickupobject.name

            var defaultGun = PickupObjectDatabase.GetById(22) as Gun;
            
            gun.AddProjectileModuleFrom(defaultGun, true, false);
            
            // move the gun up with the hand in the second frame of the shooting animation (hand movement was done in GAE with a y offset of 1)
            //gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).frames[1].FrameToDefinition().MakeOffset(new Vector2(0, 1));

            gun.gunSwitchGroup = defaultGun.gunSwitchGroup;
            gun.muzzleFlashEffects = defaultGun.muzzleFlashEffects;
            
            gun.AddMuzzle();
            gun.muzzleOffset.localPosition = new Vector3(1.2f, 0.8f, 0f);
            gun.barrelOffset.localPosition = new Vector3(1.4f, 0.8f, 0f);
            
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.DefaultModule.cooldownTime = 0.07f;
            gun.DefaultModule.numberOfShotsInClip = 6;
            gun.DefaultModule.angleVariance = 0;
            gun.DefaultModule.ammoCost = 1;
            
            var curseWhileHeld = new StatModifier
            {
                amount = 2,
                statToBoost = PlayerStats.StatType.Curse,
                modifyType = StatModifier.ModifyMethod.ADDITIVE
            };
            
            gun.currentGunStatModifiers = new StatModifier[] { curseWhileHeld };
            
            gun.shellCasing = defaultGun.shellCasing;
            gun.shellsToLaunchOnFire = 0;
            gun.shellsToLaunchOnReload = gun.DefaultModule.numberOfShotsInClip;
            gun.reloadShellLaunchFrame = defaultGun.reloadShellLaunchFrame;
            
            gun.reloadTime = 1f;
            gun.gunClass = GunClass.PISTOL;
            gun.SetBaseMaxAmmo(666);
            gun.quality = PickupObject.ItemQuality.EXCLUDED;
            
            gun.encounterTrackable.EncounterGuid = $"this is the {lowerColor} skull revolver";

            Projectile projectile = isGoldenVersion ? WestBrosGoldenRevolverProjectile.AddComponent<Projectile>() : WestBrosBlackRevolverProjectile.AddComponent<Projectile>();
            GameObject projectileChild = projectile.transform.Find("Sprite").gameObject;
            tk2dSprite projetileSprite = SpriteSerializer.AddSpriteToObject(projectileChild, ExpandCustomEnemyDatabase.WestBrosCollection, "gr_black_revolver_projectile_001");
            ExpandUtility.GenerateSpriteAnimator(projectileChild, playAutomatically: true);
            ExpandUtility.AddAnimation(projectileChild.GetComponent<tk2dSpriteAnimator>(), projetileSprite.Collection, ProjectileSpriteList, "idle", tk2dSpriteAnimationClip.WrapMode.Loop, 13);
            
            SpeculativeRigidbody projectileRigidBody = projectile.gameObject.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateRigidBody(projectileRigidBody, defaultGun.DefaultModule.projectiles[0].specRigidbody);
            ExpandUtility.DuplicateComponent(projectile, defaultGun.DefaultModule.projectiles[0]);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 14f;
            projectile.baseData.speed = 25f;
            projectile.baseData.force = 14f;


            // projectile.transform.parent = gun.barrelOffset;
            projectile.transform.localPosition = gun.barrelOffset.localPosition;

            projectile.shouldRotate = true;
            var comp = projectile.gameObject.AddComponent<SkullRevolverBullet>();
            comp.jamsEnemies = true;

            ETGMod.Databases.Items.Add(gun, null, "ANY");

            if (isGoldenVersion)
            {
                AddHoveringGunComponent(gun);
                GoldenRevolverID = gun.PickupObjectId;
            }
            else
            {
                BlackRevolverID = gun.PickupObjectId;
            }
        }

        // TODO add audio event
        private static void AddHoveringGunComponent(Gun goldenRevolver)
        {
            var hover = goldenRevolver.gameObject.AddComponent<CustomHoveringGunSynergyProcessor>();

            hover.RequiresSynergy = false;
            hover.UsesMultipleGuns = true;
            hover.TargetGunIDs = new int[]
            {
                WestBrosRevolverGenerator.GetWestBrosRevolverID(ExpandPrefab.WestBros.Angel),
                WestBrosRevolverGenerator.GetWestBrosRevolverID(ExpandPrefab.WestBros.Nome),
                WestBrosRevolverGenerator.GetWestBrosRevolverID(ExpandPrefab.WestBros.Tuc)
            };
            hover.PositionType = HoveringGunController.HoverPosition.CIRCULATE;
            hover.AimType = HoveringGunController.AimType.NEAREST_ENEMY;
            hover.FireType = HoveringGunController.FireType.ON_COOLDOWN;
            hover.FireCooldown = 1f;
            hover.FireDuration = 0f;
            hover.OnlyOnEmptyReload = false;
            hover.ShootAudioEvent = null;
            hover.OnEveryShotAudioEvent = null;
            hover.FinishedShootingAudioEvent = null;
            hover.Trigger = HoveringGunSynergyProcessor.TriggerStyle.CONSTANT;
            hover.NumToTrigger = 3;
            hover.TriggerDuration = -1f;
            hover.ConsumesTargetGunAmmo = false;
            hover.ChanceToConsumeTargetGunAmmo = 0f;
        }

        private bool HasReloaded;

        public override void OnPostFired(PlayerController player, Gun gun)
        {
            base.OnPostFired(player, gun);
        }

        public override void Update()
        {
            base.Update();
            if (gun.CurrentOwner)
            {
                if (!gun.IsReloading && !HasReloaded)
                {
                    this.HasReloaded = true;
                }
            }
        }

        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            if (gun.IsReloading && this.HasReloaded)
            {
                HasReloaded = false;

                base.OnReloadPressed(player, gun, bSOMETHING);
            }
        }
    }

    public class SkullRevolverBullet : MonoBehaviour
    {
        public bool jamsEnemies = false;
        private bool currentlyAppliedDamageBonus = false;

        public void Start()
        {
            var projectile = this.GetComponent<Projectile>();

            ProjectileHookClass.OnPreDidDamage += AddDamageModifier;
            ProjectileHookClass.OnPostDidDamage += RemoveDamageModifier;

            if (jamsEnemies)
            {
                projectile.OnHitEnemy += JamEnemyOnHit;
            }
        }

        public void OnDestroy()
        {
            ProjectileHookClass.OnPreDidDamage -= AddDamageModifier;
            ProjectileHookClass.OnPostDidDamage -= RemoveDamageModifier;
        }

        public void AddDamageModifier(Projectile projectile, SpeculativeRigidbody enemyRigidbody, bool alive)
        {
            ApplyDamageModifier(projectile, enemyRigidbody, alive, true);
        }

        public void RemoveDamageModifier(Projectile projectile, SpeculativeRigidbody enemyRigidbody, bool alive)
        {
            ApplyDamageModifier(projectile, enemyRigidbody, alive, false);
        }

        public void ApplyDamageModifier(Projectile projectile, SpeculativeRigidbody enemyRigidbody, bool alive, bool addDamage)
        {
            if (this && projectile && enemyRigidbody && currentlyAppliedDamageBonus != addDamage)
            {
                AIActor enemy = enemyRigidbody.GetComponent<AIActor>();

                if (enemy && alive && enemy.healthHaver && enemy.IsBlackPhantom)
                {
                    float jammedHealthMultiplier;

                    if (enemy.healthHaver.IsBoss)
                    {
                        jammedHealthMultiplier = 1 + enemy.BlackPhantomProperties.BonusHealthPercentIncrease + BlackPhantomProperties.GlobalBossPercentIncrease;
                    }
                    else
                    {
                        if (!BlackAndGoldenRevolver.exceptionEnemies.ContainsKey(enemy.EnemyGuid))
                        {
                            jammedHealthMultiplier = 1 + enemy.BlackPhantomProperties.BonusHealthPercentIncrease + BlackPhantomProperties.GlobalPercentIncrease;
                        }
                        else
                        {
                            jammedHealthMultiplier = BlackAndGoldenRevolver.exceptionEnemies[enemy.EnemyGuid];
                        }
                    }

                    if (addDamage)
                    {
                        projectile.BlackPhantomDamageMultiplier *= jammedHealthMultiplier;
                    }
                    else
                    {
                        projectile.BlackPhantomDamageMultiplier /= jammedHealthMultiplier;
                    }

                    currentlyAppliedDamageBonus = addDamage;
                }
            }
        }

        public void JamEnemyOnHit(Projectile projectile, SpeculativeRigidbody enemyRigidbody, bool killedEnemy)
        {
            if (this && projectile && enemyRigidbody)
            {
                AIActor enemy = enemyRigidbody.GetComponent<AIActor>();

                if (enemy && enemy.healthHaver && enemy.healthHaver.IsAlive && !enemy.healthHaver.IsBoss && !enemy.IsBlackPhantom)
                {
                    enemy.BecomeBlackPhantom();
                }
            }
        }
    }

    // because Projectile.HandleDamageResult is protected, we inherit from Projectile just to create the hook
    public class ProjectileHookClass : Projectile
    {
        public static void AddHook()
        {
            new Hook(typeof(Projectile).GetMethod("HandleDamage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance),
                typeof(ProjectileHookClass).GetMethod(nameof(ProjectileHookClass.HandleDamageHook), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static));
        }

        public delegate T7 CustomFunc<T1, T2, T3, T4, T5, T6, T7>(T1 p1, T2 p2, T3 p3, out T4 p4, T5 p5, T6 p6);

        public static Action<Projectile, SpeculativeRigidbody, bool> OnPreDidDamage;

        public static Action<Projectile, SpeculativeRigidbody, bool> OnPostDidDamage;

        // because Projectile.HandleDamageResult is protected, this hook can't be public
        private static Projectile.HandleDamageResult HandleDamageHook(CustomFunc<Projectile, SpeculativeRigidbody, PixelCollider, bool, PlayerController, bool, Projectile.HandleDamageResult> orig, Projectile self, SpeculativeRigidbody rigidbody, PixelCollider hitPixelCollider, out bool killedTarget, PlayerController player, bool alreadyPlayerDelayed)
        {
            bool wasAlive = rigidbody && rigidbody.healthHaver && rigidbody.healthHaver.IsAlive;

            if (OnPreDidDamage != null)
            {
                OnPreDidDamage.Invoke(self, rigidbody, wasAlive);
            }

            var ret = orig(self, rigidbody, hitPixelCollider, out killedTarget, player, alreadyPlayerDelayed);

            if (OnPostDidDamage != null)
            {
                OnPostDidDamage.Invoke(self, rigidbody, wasAlive);
            }

            return ret;
        }
    }
}