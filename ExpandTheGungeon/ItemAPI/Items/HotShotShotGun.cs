using System.Collections.Generic;
using Gungeon;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class HotShotShotGun {

        public static int HotShotShotGunID;

        public static Gun HotShot_ShotGun;
        
        public static void Init() {
            
            Gun shotgun = ETGMod.Databases.Items.NewGun("HotShot Shotgun", (PickupObjectDatabase.GetById(51) as Gun), "hotshot_shotgun");
            Game.Items.Rename("outdated_gun_mods:hotshot_shotgun", "ex:hotshot_shotgun");
            shotgun.SetShortDescription("You aren't supposed to have this...");
            shotgun.SetLongDescription("The gun used by Hot Shot Shotgun Kin.");
            // GunExt.SetupSprite(shotgun, null, "bootleg_shotgun_idle_001", 18);
            shotgun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(51).name, true, false);
            // shotgun.barrelOffset.localPosition -= new Vector3(0.3f, 0.2f, 0);
            shotgun.barrelOffset.localPosition = (PickupObjectDatabase.GetById(51) as Gun).barrelOffset.localPosition;
            if ((PickupObjectDatabase.GetById(51) as Gun).muzzleOffset && shotgun.muzzleOffset) {
                shotgun.muzzleOffset.transform.localPosition = (PickupObjectDatabase.GetById(51) as Gun).muzzleOffset.localPosition;
            }
            shotgun.reloadTime = 1.8f;
            shotgun.gunClass = GunClass.SHOTGUN;
            shotgun.ammo = 150;
            shotgun.SetBaseMaxAmmo(150);
            shotgun.quality = PickupObject.ItemQuality.EXCLUDED;
            shotgun.gunSwitchGroup = "Shotgun";
            shotgun.encounterTrackable.EncounterGuid = "dc52f8e79c7c4a679238643a5bcb49c3";
            ETGMod.Databases.Items.Add(shotgun);
            HotShotShotGunID = shotgun.PickupObjectId;

            Projectile ShotgunProjectileComponent = EnemyDatabase.GetOrLoadByGuid("128db2f0781141bcb505d8f00f9e4d47").gameObject.GetComponent<AIBulletBank>().Bullets[0].BulletObject.GetComponent<Projectile>();

            // ExpandUtility.DuplicateComponent(ShotgunProjectileComponent, shotgun.DefaultModule.projectiles[0]);
            shotgun.DefaultModule.projectiles[0] = ShotgunProjectileComponent;
            ProjectileVolleyData shotGunVollyData = ScriptableObject.CreateInstance<ProjectileVolleyData>();
            shotGunVollyData.projectiles = new List<ProjectileModule>() {
                shotgun.DefaultModule,
                new ProjectileModule(),
                new ProjectileModule(),
                new ProjectileModule(),
                new ProjectileModule(),
                new ProjectileModule(),
            };
            shotGunVollyData.UsesBeamRotationLimiter = false;
            shotGunVollyData.BeamRotationDegreesPerSecond = 30;
            shotGunVollyData.ModulesAreTiers = false;
            shotGunVollyData.UsesShotgunStyleVelocityRandomizer = true;
            shotGunVollyData.DecreaseFinalSpeedPercentMin = -15;
            shotGunVollyData.IncreaseFinalSpeedPercentMax = 15;
            
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(shotgun.DefaultModule), shotGunVollyData.projectiles[1]);
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(shotgun.DefaultModule), shotGunVollyData.projectiles[2]);
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(shotgun.DefaultModule), shotGunVollyData.projectiles[3]);
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(shotgun.DefaultModule), shotGunVollyData.projectiles[4]);
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(shotgun.DefaultModule), shotGunVollyData.projectiles[5]);
            shotGunVollyData.projectiles[1].ammoType = GameUIAmmoType.AmmoType.SMALL_BULLET;
            shotGunVollyData.projectiles[2].ammoType = GameUIAmmoType.AmmoType.SMALL_BULLET;
            shotGunVollyData.projectiles[3].ammoType = GameUIAmmoType.AmmoType.SMALL_BULLET;
            shotGunVollyData.projectiles[4].ammoType = GameUIAmmoType.AmmoType.SMALL_BULLET;
            shotGunVollyData.projectiles[5].ammoType = GameUIAmmoType.AmmoType.SMALL_BULLET;
            shotgun.Volley = shotGunVollyData;
            // ShotgunProjectileComponent.gameObject.transform.localPosition = shotgun.barrelOffset.localPosition;

            HotShot_ShotGun = shotgun;
        }
    }
}

