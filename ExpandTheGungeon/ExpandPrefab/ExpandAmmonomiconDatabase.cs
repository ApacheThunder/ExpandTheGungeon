using UnityEngine;

namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandAmmonomiconDatabase {

        public struct EnemyEntryData {
            public int ForcedPositionInAmmonomicon;
            public bool IsNormalEnemy;
            public bool IsInBossTab;
            public bool TabSpriteIsTexture;
            public DungeonPlaceableBehaviour.PlaceableDifficulty EnemyDifficulty;
            public string TabSprite;
            public string FullArtSprite;
            public string EnemyName;
            public string smallDescription;
            public string bigDescription;
        }

        public static EnemyEntryData BootlegBullat;
        public static EnemyEntryData BootlegBulletKin;
        public static EnemyEntryData BootlegBandanaBulletKin;
        public static EnemyEntryData BootlegShotgunKinRed;
        public static EnemyEntryData BootlegShotgunKinBlue;
        public static EnemyEntryData HotShotBulletKin;
        public static EnemyEntryData HotShotShotgunKin;
        public static EnemyEntryData HotShotCultist;
        public static EnemyEntryData Cronenberg;
        public static EnemyEntryData CronenbergAngry;
        public static EnemyEntryData ClownKin;
        public static EnemyEntryData Chameleon;
        public static EnemyEntryData WestBrosAngel;
        public static EnemyEntryData WestBrosNome;
        public static EnemyEntryData WestBrosTuc;
        public static EnemyEntryData Doppelgunner;


        public static void Init(AssetBundle expandSharedAssets1) {
            BootlegBullat = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("BootlegBullat_AmmonomiconData").text);
            BootlegBulletKin = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("BootlegBulletKin_AmmonomiconData").text);
            BootlegBandanaBulletKin = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("BootlegBandanaBulletKin_AmmonomiconData").text);
            BootlegShotgunKinRed = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("BootlegShotgunManRed_AmmonomiconData").text);
            BootlegShotgunKinBlue = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("BootlegShotgunManBlue_AmmonomiconData").text);
            HotShotBulletKin = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("HotShotBulletKin_AmmonomiconData").text);
            HotShotShotgunKin = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("HotShotShotgunKin_AmmonomiconData").text);
            HotShotCultist = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("HotShotCultist_AmmonomiconData").text);
            Cronenberg = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("Cronenberg_AmmonomiconData").text);
            CronenbergAngry = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("CronenbergAngry_AmmonomiconData").text);
            ClownKin = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("ClownKin_AmmonomiconData").text);
            Chameleon = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("Chameleon_AmmonomiconData").text);
            WestBrosAngel = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("WestBrosAngel_AmmonomiconData").text);
            WestBrosNome = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("WestBrosNome_AmmonomiconData").text);
            WestBrosTuc = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("WestBrosTuc_AmmonomiconData").text);
            Doppelgunner = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("Doppelgunner_AmmonomiconData").text);
        }
    }
}

