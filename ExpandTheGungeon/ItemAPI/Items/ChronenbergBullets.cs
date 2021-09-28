using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class CronenbergBullets : BulletStatusEffectItem {

        public static CronenbergBullets m_CachedCronenbergBulletsItem;

        private static GameObject CronebergItemObject;

        public static void Init(AssetBundle expandSharedAssets1) {
            CronebergItemObject = expandSharedAssets1.LoadAsset<GameObject>("Cronenberg Bullets");
            SpriteSerializer.AddSpriteToObject(CronebergItemObject, ExpandPrefabs.EXItemCollection, "cronenbergbullets");
            
            CronenbergBullets chronenbergBullets = CronebergItemObject.AddComponent<CronenbergBullets>();
            
            string shortDesc = "Creates abominations...";
            string longDesc = "Legends say a mad scientist tried to experiment on the gundead to create monsters to do his bidding.\n\nHe ultimiately parished by the hands of his own abominations. The item he crafted was thought to be lost to the Gungeon...Until some lucky (or unlucky?) Gungeoneers found it.";
            ItemBuilder.SetupItem(chronenbergBullets, shortDesc, longDesc, "ex");
            chronenbergBullets.quality = ItemQuality.B;
            if (!ExpandSettings.EnableEXItems) { chronenbergBullets.quality = ItemQuality.EXCLUDED; }

            chronenbergBullets.CustomCost = 50;
            chronenbergBullets.chanceOfActivating = 0.055f;
            chronenbergBullets.chanceFromBeamPerSecond = 0.055f;
            chronenbergBullets.TintBullets = false;
            chronenbergBullets.TintBeams = false;
            chronenbergBullets.TintColor = new Color(0.94f, 0f, 0.992f, 1f);
            chronenbergBullets.TintPriority = 5;
            chronenbergBullets.AddsDamageType = false;
            chronenbergBullets.DamageTypesToAdd = CoreDamageTypes.None;
            chronenbergBullets.AppliesSpeedModifier = false;
            chronenbergBullets.AppliesDamageOverTime = false;
            chronenbergBullets.AppliesCharm = false;
            chronenbergBullets.AppliesFreeze = false;
            chronenbergBullets.FreezeScalesWithDamage = false;
            chronenbergBullets.FreezeAmountPerDamage = 1;
            chronenbergBullets.AppliesFire = false;
            chronenbergBullets.ConfersElectricityImmunity = false;
            chronenbergBullets.AppliesTransmog = true;
            chronenbergBullets.TransmogTargetGuid = "76bc43539fc24648bff4568c75c686d1";
            chronenbergBullets.Synergies = new BulletStatusEffectItemSynergy[0];
            m_CachedCronenbergBulletsItem = chronenbergBullets;
        }

    }
}

