using Gungeon;
using System;

namespace ExpandTheGungeon.ItemAPI {

    public class BootlegGuns : Gun {

        public static int BootlegPistolID;
        public static int BootlegMachinePistolID;
        public static int BootlegShotgunID;

        public static void Init() {
           
            Gun pistol = ETGMod.Databases.Items.NewGun("Bootleg Pistol", "BootlegGun");
            Game.Items.Rename("outdated_gun_mods:bootleg_pistol", "ex:bootleg_pistol");
            pistol.SetShortDescription("Of questionable quality...");
            pistol.SetLongDescription("It's a counterfeit gun. Luckily it somehow works...");
            pistol.AddProjectileModuleFrom("Magnum", true, false);
            pistol.DefaultModule.ammoCost = 1;
            pistol.PreventOutlines = true;
            pistol.Volley = (PickupObjectDatabase.GetById(38) as Gun).Volley;
            pistol.singleModule = (PickupObjectDatabase.GetById(38) as Gun).singleModule;
            pistol.RawSourceVolley = (PickupObjectDatabase.GetById(38) as Gun).RawSourceVolley;
            pistol.alternateVolley = (PickupObjectDatabase.GetById(38) as Gun).alternateVolley;
            pistol.reloadTime = 1;
            pistol.gunClass = GunClass.PISTOL;
            pistol.ammo = 140;
            pistol.SetBaseMaxAmmo(140);
            pistol.quality = ItemQuality.D;
            pistol.encounterTrackable.EncounterGuid = Guid.NewGuid().ToString();

            pistol.SetupSprite(defaultSprite: "bootleg_pistol_idle_001", fps: 8);
            pistol.SetAnimationFPS(pistol.shootAnimation, 8);
                       
            ETGMod.Databases.Items.Add(pistol);

            BootlegPistolID = pistol.PickupObjectId;


            Gun machinepistol = ETGMod.Databases.Items.NewGun("Bootleg Machine Pistol", "BootlegMachinePistol");
            Game.Items.Rename("outdated_gun_mods:bootleg_machine_pistol", "ex:bootleg_machine_pistol");
            machinepistol.SetShortDescription("Of questionable quality...");
            machinepistol.SetLongDescription("It's a counterfeit machine gun. Luckily it somehow works...");
            machinepistol.SetupSprite(defaultSprite: "bootleg_machinepistol_idle_001", fps: 8);
            machinepistol.SetAnimationFPS(machinepistol.shootAnimation, 8);
            machinepistol.AddProjectileModuleFrom("Magnum", true, false);
            machinepistol.Volley = (PickupObjectDatabase.GetById(43) as Gun).Volley;
            machinepistol.singleModule = (PickupObjectDatabase.GetById(43) as Gun).singleModule;
            machinepistol.RawSourceVolley = (PickupObjectDatabase.GetById(43) as Gun).RawSourceVolley;
            machinepistol.alternateVolley = (PickupObjectDatabase.GetById(43) as Gun).alternateVolley;
            machinepistol.PreventOutlines = true;
            machinepistol.reloadTime = 1.2f;
            machinepistol.gunClass = GunClass.FULLAUTO;
            machinepistol.ammo = 600;
            machinepistol.SetBaseMaxAmmo(600);
            machinepistol.quality = ItemQuality.D;
            machinepistol.gunSwitchGroup = "Uzi";
            machinepistol.encounterTrackable.EncounterGuid = Guid.NewGuid().ToString();

            ETGMod.Databases.Items.Add(machinepistol);

            BootlegMachinePistolID = machinepistol.PickupObjectId;


            Gun shotgun = ETGMod.Databases.Items.NewGun("Bootleg Shotgun", "BootlegShotgun");
            Game.Items.Rename("outdated_gun_mods:bootleg_shotgun", "ex:bootleg_shotgun");
            shotgun.SetShortDescription("Of questionable quality...");
            shotgun.SetLongDescription("It's a counterfeit shotgun. Oddly, it looks like a pistol...");
            shotgun.SetupSprite(defaultSprite: "bootleg_shotgun_idle_001", fps: 8);
            shotgun.SetAnimationFPS(machinepistol.shootAnimation, 8);
            shotgun.AddProjectileModuleFrom("AK-47", true, false);
            shotgun.Volley = (PickupObjectDatabase.GetById(51) as Gun).Volley;
            shotgun.singleModule = (PickupObjectDatabase.GetById(51) as Gun).singleModule;
            shotgun.RawSourceVolley = (PickupObjectDatabase.GetById(51) as Gun).RawSourceVolley;
            shotgun.alternateVolley = (PickupObjectDatabase.GetById(51) as Gun).alternateVolley;
            shotgun.PreventOutlines = true;
            shotgun.reloadTime = 1.8f;
            shotgun.gunClass = GunClass.SHOTGUN;
            shotgun.ammo = 150;
            shotgun.SetBaseMaxAmmo(150);
            shotgun.quality = ItemQuality.D;
            shotgun.gunSwitchGroup = "Shotgun";
            shotgun.encounterTrackable.EncounterGuid = Guid.NewGuid().ToString();

            ETGMod.Databases.Items.Add(shotgun);

            BootlegShotgunID = shotgun.PickupObjectId;
        }
    }
}

