using Gungeon;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using UnityEngine;
using ExpandTheGungeon.SpriteAPI;
using System.Reflection;

namespace ExpandTheGungeon.ItemAPI {

    public class BootlegGuns : Gun {

        public static int BootlegPistolID;
        public static int BootlegMachinePistolID;
        public static int BootlegShotgunID;

        public static Gun BootlegPistol;
        public static Gun BootlegMachinePistol;
        public static Gun BootlegShotgun;

        public static GameObject PistolProjectile;
        public static GameObject MachinePistolProjectile;
        public static GameObject ShotgunProjectile;

        public static void Init(AssetBundle expandSharedAssets1) {

            Gun pistol = ETGMod.Databases.Items.NewGun("Bootleg Pistol", "bootleg_pistol");
            Game.Items.Rename("outdated_gun_mods:bootleg_pistol", "ex:bootleg_pistol");
            pistol.SetShortDescription("Of questionable quality...");
            pistol.SetLongDescription("It's a counterfeit gun.\n\nDue to low quality standards, this weapon may be prone to exploding under certain circumstances...");
            GunExt.SetupSprite(pistol, null, "bootleg_pistol_idle_001", 18);            
            pistol.AddProjectileModuleFrom("Magnum", true, false);
            pistol.barrelOffset.localPosition -= new Vector3(0.3f, 0.2f, 0);
            pistol.DefaultModule.ammoCost = 1;
            pistol.PreventOutlines = true;
            pistol.reloadTime = 1;
            pistol.gunClass = GunClass.PISTOL;
            pistol.ammo = 140;
            pistol.SetBaseMaxAmmo(140);
            pistol.quality = ItemQuality.D;
            if (!ExpandSettings.EnableEXItems) { pistol.quality = ItemQuality.EXCLUDED; }
            pistol.UsesCustomCost = true;
            pistol.CustomCost = 10;
            pistol.encounterTrackable.EncounterGuid = "baad9dd6d005458daf02933f6a1ba926";            
            pistol.gameObject.AddComponent<ExpandRemoveGunOnAmmoDepletion>();
            pistol.gameObject.AddComponent<ExpandMaybeLoseAmmoOnDamage>();
            ETGMod.Databases.Items.Add(pistol);
            BootlegPistolID = pistol.PickupObjectId;

            PistolProjectile = expandSharedAssets1.LoadAsset<GameObject>("EXBootlegPistolProjectile");
            tk2dSprite PistolProjectileSprite = SpriteSerializer.AddSpriteToObject(PistolProjectile.transform.Find("Sprite").gameObject, ExpandPrefabs.EXGunCollection, "bootleg_pistol_projectile_001");
            SpeculativeRigidbody pistolProjectileRigidBody = PistolProjectile.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateRigidBody(pistolProjectileRigidBody, pistol.DefaultModule.projectiles[0].specRigidbody);
            Projectile PistolProjectileComponent = PistolProjectile.AddComponent<Projectile>();
            ExpandUtility.DuplicateComponent(PistolProjectileComponent, pistol.DefaultModule.projectiles[0]);
            pistol.DefaultModule.projectiles[0] = PistolProjectileComponent;
            PistolProjectile.gameObject.transform.localPosition = pistol.barrelOffset.localPosition;


            Gun machinepistol = ETGMod.Databases.Items.NewGun("Bootleg Machine Pistol", "bootleg_machinepistol");
            Game.Items.Rename("outdated_gun_mods:bootleg_machine_pistol", "ex:bootleg_machine_pistol");
            machinepistol.SetShortDescription("Of questionable quality...");
            machinepistol.SetLongDescription("It's a counterfeit machine gun.\n\nDue to low quality standards, this weapon may be prone to exploding under certain circumstances...");
            GunExt.SetupSprite(machinepistol, null, "bootleg_machinepistol_idle_001", 30);
            machinepistol.AddProjectileModuleFrom(PickupObjectDatabase.GetById(43).name, true, false);
            machinepistol.barrelOffset.localPosition -= new Vector3(0.3f, 0.2f, 0);
            machinepistol.PreventOutlines = true;
            machinepistol.reloadTime = 1.2f;
            machinepistol.gunClass = GunClass.FULLAUTO;
            machinepistol.ammo = 600;
            machinepistol.SetBaseMaxAmmo(600);
            machinepistol.quality = ItemQuality.D;
            if (!ExpandSettings.EnableEXItems) { machinepistol.quality = ItemQuality.EXCLUDED; }
            machinepistol.gunSwitchGroup = "Uzi";
            machinepistol.UsesCustomCost = true;
            machinepistol.CustomCost = 15;
            machinepistol.encounterTrackable.EncounterGuid = "e56adda5081347e5b9e0cf2556689b0e";
            machinepistol.gameObject.AddComponent<ExpandRemoveGunOnAmmoDepletion>();
            machinepistol.gameObject.AddComponent<ExpandMaybeLoseAmmoOnDamage>();
            ETGMod.Databases.Items.Add(machinepistol);
            BootlegMachinePistolID = machinepistol.PickupObjectId;

            MachinePistolProjectile = expandSharedAssets1.LoadAsset<GameObject>("EXBootlegMachinePistolProjectile");
            tk2dSprite MachinePistolProjectileSprite = SpriteSerializer.AddSpriteToObject(MachinePistolProjectile.transform.Find("Sprite").gameObject, ExpandPrefabs.EXGunCollection, "bootleg_pistol_projectile_001");
            SpeculativeRigidbody machinePistolProjectileRigidBody = MachinePistolProjectile.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateRigidBody(machinePistolProjectileRigidBody, machinepistol.DefaultModule.projectiles[0].specRigidbody);
            Projectile MachinePistolProjectileComponent = MachinePistolProjectile.AddComponent<Projectile>();
            ExpandUtility.DuplicateComponent(MachinePistolProjectileComponent, machinepistol.DefaultModule.projectiles[0]);
            machinepistol.DefaultModule.projectiles[0] = MachinePistolProjectileComponent;
            MachinePistolProjectile.gameObject.transform.localPosition = machinepistol.barrelOffset.localPosition;


            Gun shotgun = ETGMod.Databases.Items.NewGun("Bootleg Shotgun", "bootleg_shotgun");
            Game.Items.Rename("outdated_gun_mods:bootleg_shotgun", "ex:bootleg_shotgun");
            shotgun.SetShortDescription("Of questionable quality...");
            shotgun.SetLongDescription("It's a counterfeit shotgun.\n\nDue to low quality standards, this weapon may be prone to exploding under certain circumstances...");
            GunExt.SetupSprite(shotgun, null, "bootleg_shotgun_idle_001", 18);
            shotgun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(51).name, true, false);
            shotgun.barrelOffset.localPosition -= new Vector3(0.3f, 0.2f, 0);
            shotgun.PreventOutlines = true;
            shotgun.reloadTime = 1.8f;
            shotgun.gunClass = GunClass.SHOTGUN;
            shotgun.ammo = 150;
            shotgun.SetBaseMaxAmmo(150);
            shotgun.quality = ItemQuality.D;
            if (!ExpandSettings.EnableEXItems) { shotgun.quality = ItemQuality.EXCLUDED; }
            shotgun.gunSwitchGroup = "Shotgun";
            shotgun.UsesCustomCost = true;
            shotgun.CustomCost = 18;
            shotgun.encounterTrackable.EncounterGuid = "fa0575b4cf0140ddb6b0ed6d962bff47";
            shotgun.gameObject.AddComponent<ExpandRemoveGunOnAmmoDepletion>();
            shotgun.gameObject.AddComponent<ExpandMaybeLoseAmmoOnDamage>();
            shotgun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.SHOTGUN;
            ETGMod.Databases.Items.Add(shotgun);
            BootlegShotgunID = shotgun.PickupObjectId;
            

            ShotgunProjectile = expandSharedAssets1.LoadAsset<GameObject>("EXBootlegShotgunProjectile");
            tk2dSprite ShotgunProjectileSprite = SpriteSerializer.AddSpriteToObject(ShotgunProjectile.transform.Find("Sprite").gameObject, ExpandPrefabs.EXGunCollection, "bootleg_pistol_projectile_001");
            SpeculativeRigidbody ShotgunProjectileRigidBody = ShotgunProjectile.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateRigidBody(ShotgunProjectileRigidBody, shotgun.DefaultModule.projectiles[0].specRigidbody);
            Projectile ShotgunProjectileComponent = ShotgunProjectile.AddComponent<Projectile>();
            ExpandUtility.DuplicateComponent(ShotgunProjectileComponent, shotgun.DefaultModule.projectiles[0]);
            shotgun.DefaultModule.projectiles[0] = ShotgunProjectileComponent;

            ProjectileVolleyData shotgunVollyData = ScriptableObject.CreateInstance<ProjectileVolleyData>();
            shotgunVollyData.projectiles = new List<ProjectileModule>() {
                shotgun.DefaultModule,
                new ProjectileModule(),
                new ProjectileModule(),
                new ProjectileModule(),
                new ProjectileModule(),
                new ProjectileModule(),
            };

            for (int i = 1; i < shotgunVollyData.projectiles.Count; i++) {
                JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(shotgun.DefaultModule), shotgunVollyData.projectiles[i]);
                shotgunVollyData.projectiles[i].ammoType = GameUIAmmoType.AmmoType.SMALL_BULLET;
                shotgunVollyData.projectiles[i].ammoCost = 0;
            }

            shotgunVollyData.UsesBeamRotationLimiter = false;
            shotgunVollyData.BeamRotationDegreesPerSecond = 30;
            shotgunVollyData.ModulesAreTiers = false;
            shotgunVollyData.UsesShotgunStyleVelocityRandomizer = true;
            shotgunVollyData.DecreaseFinalSpeedPercentMin = -15;
            shotgunVollyData.IncreaseFinalSpeedPercentMax = 15;
            

            shotgun.Volley = shotgunVollyData;
            ShotgunProjectileComponent.gameObject.transform.localPosition = shotgun.barrelOffset.localPosition;


            BootlegPistol = pistol;
            BootlegMachinePistol = machinepistol;
            BootlegShotgun = shotgun;
        }

        public static void PostInit() {
            if (BootlegPistol && BootlegPistol.gameObject.GetComponent<ExpandMaybeLoseAmmoOnDamage>()) {
                BootlegPistol.gameObject.GetComponent<ExpandMaybeLoseAmmoOnDamage>().TransfmorgifyTargetGUIDs = new List<string>() {
                    ExpandCustomEnemyDatabase.BootlegBulletManGUID
                };
            }
            if (BootlegMachinePistol && BootlegMachinePistol.gameObject.GetComponent<ExpandMaybeLoseAmmoOnDamage>()) {
                BootlegMachinePistol.gameObject.GetComponent<ExpandMaybeLoseAmmoOnDamage>().TransfmorgifyTargetGUIDs = new List<string>() {
                    ExpandCustomEnemyDatabase.BootlegBulletManBandanaGUID
                };
            }
            if (BootlegShotgun && BootlegShotgun.gameObject.GetComponent<ExpandMaybeLoseAmmoOnDamage>()) {
                BootlegShotgun.gameObject.GetComponent<ExpandMaybeLoseAmmoOnDamage>().IsBootlegShotgun = true;
            }
        }
    }
}

