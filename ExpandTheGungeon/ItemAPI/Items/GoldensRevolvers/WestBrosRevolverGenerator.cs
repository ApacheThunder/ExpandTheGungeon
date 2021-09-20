using ExpandTheGungeon.ExpandPrefab;
using Gungeon;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI
{
    public class WestBrosRevolverGenerator
    {
        public static int WestBrosAngelGunID = -1;
        public static int WestBrosNomeGunID = -1;
        public static int WestBrosTucGunID = -1;

        public static void Init()
        {
            Generate(WestBros.Angel);
            Generate(WestBros.Nome);
            Generate(WestBros.Tuc);

            BlackAndGoldenRevolver.AddBothVariants();
        }

        public static void Generate(WestBros whichBro)
        {
            string name = whichBro.ToString();
            string lowerName = name.ToLower();

            Gun gun = ETGMod.Databases.Items.NewGun($"{name}'s Revolver", $"gr_{lowerName}_rev");

            Game.Items.Rename($"outdated_gun_mods:{lowerName}'s_revolver", $"ex:{lowerName}s_revolver");

            // shades
            var baseGun = PickupObjectDatabase.GetById(22) as Gun;
            //// smiley
            //var baseGun = PickupObjectDatabase.GetById(35) as Gun;

            //gun.gameObject.AddComponent<NomesRevolver>();
            gun.SetShortDescription("TODO");
            gun.SetLongDescription("TODO");

            gun.SetupSprite(null, $"gr_{lowerName}_rev_idle_001", 8);

            gun.SetAnimationFPS(gun.shootAnimation, 12);
            gun.SetAnimationFPS(gun.enemyPreFireAnimation, 8);
            gun.SetAnimationFPS(gun.reloadAnimation, 8);

            // enemyPreFireAnimation is set to Loop by default for some reason
            gun.spriteAnimator.GetClipByName(gun.enemyPreFireAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.Once;

            //ETGModConsole.Log("Shadess revolver animations");
            //foreach (var item in baseGun.spriteAnimator.Library.clips)
            //{
            //    ETGModConsole.Log(item.name + " " + item.frames + " " + item.fps + " " + item.maxFidgetDuration + " " + item.minFidgetDuration + " " + item.wrapMode + " " + item.loopStart, true);
            //}
            //ETGModConsole.Log("West Bros revolver animations");
            //foreach (var item in gun.spriteAnimator.Library.clips)
            //{
            //    ETGModConsole.Log(item.name + " " + item.frames + " " + item.fps + " " + item.maxFidgetDuration + " " + item.minFidgetDuration + " " + item.wrapMode + " " + item.loopStart, true);
            //}

            // Every modded gun has base projectile it works with that is borrowed from other guns in the game.
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.
            // which means its the ETGMod.Databases.Items / PickupObjectDatabase.Instance.InternalGetByName name, aka the pickupobject.name

            gun.AddProjectileModuleFrom(baseGun, true, false);

            gun.gunSwitchGroup = baseGun.gunSwitchGroup;
            gun.muzzleFlashEffects = baseGun.muzzleFlashEffects;

            //gun.AddMuzzle();
            //gun.muzzleOffset.localPosition = new Vector3(0.9f, 0.3f, 0f);
            gun.barrelOffset.localPosition = new Vector3(1.1f, 0.3f, 0f);

            //gun.shellCasing = defaultGun.shellCasing;
            //gun.shellsToLaunchOnFire = 0;
            //gun.shellsToLaunchOnReload = defaultGun.shellsToLaunchOnReload;
            //gun.reloadShellLaunchFrame = defaultGun.reloadShellLaunchFrame;

            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.DefaultModule.cooldownTime = 0.07f;
            gun.DefaultModule.numberOfShotsInClip = 6;
            gun.DefaultModule.angleVariance = 4;
            gun.DefaultModule.ammoCost = 0;

            gun.reloadTime = 1f;
            gun.gunClass = GunClass.PISTOL;
            gun.SetBaseMaxAmmo(350);
            gun.quality = PickupObject.ItemQuality.EXCLUDED;
            gun.encounterTrackable.EncounterGuid = $"this is {name}'s new revolver";

            Projectile projectile = Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            Object.DontDestroyOnLoad(projectile);

            gun.DefaultModule.projectiles[0] = projectile;

            projectile.baseData.damage = 6f;
            projectile.transform.parent = gun.barrelOffset;

            var comp = projectile.gameObject.AddComponent<SkullRevolverBullet>();
            comp.jamsEnemies = false;

            ETGMod.Databases.Items.Add(gun, null, "ANY");

            switch (whichBro)
            {
                case WestBros.Angel:
                    WestBrosAngelGunID = gun.PickupObjectId;
                    break;

                case WestBros.Nome:
                    WestBrosNomeGunID = gun.PickupObjectId;
                    break;

                case WestBros.Tuc:
                    WestBrosTucGunID = gun.PickupObjectId;
                    break;
            }
        }

        public static int GetWestBrosRevolverID(WestBros whichBro)
        {
            switch (whichBro)
            {
                case WestBros.Angel:
                    return WestBrosAngelGunID;

                case WestBros.Nome:
                    return WestBrosNomeGunID;

                case WestBros.Tuc:
                    return WestBrosTucGunID;

                default:
                    throw new System.Exception("Invalid enum value");
            }
        }
    }
}