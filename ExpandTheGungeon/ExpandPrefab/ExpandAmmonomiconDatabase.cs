using ExpandTheGungeon.ItemAPI;
using UnityEngine;

namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandAmmonomiconDatabase {

        public struct EnemyEntryData {
            public int ForcedPositionInAmmonomicon;
            public bool IsNormalEnemy;
            public bool IsInBossTab;
            public bool TabSpriteIsTexture;
            public DungeonPlaceableBehaviour.PlaceableDifficulty EnemyDifficulty;
            public int placeableWidth;
            public int placeableHeight;
            public string TabSprite;
            public string FullArtSprite;
            public string EnemyName;
            public string smallDescription;
            public string bigDescription;
            public string encounterGUID;
            public string encounterPath;
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
        public static EnemyEntryData Skusketling;
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
            Skusketling = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("Skusketling_AmmonomiconData").text);
            WestBrosAngel = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("WestBrosAngel_AmmonomiconData").text);
            WestBrosNome = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("WestBrosNome_AmmonomiconData").text);
            WestBrosTuc = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("WestBrosTuc_AmmonomiconData").text);
            Doppelgunner = JsonUtility.FromJson<EnemyEntryData>(expandSharedAssets1.LoadAsset<TextAsset>("Doppelgunner_AmmonomiconData").text);
        }

        public static void AddExistingEnemyToAmmonomicon(AIActor targetEnemy, EnemyEntryData enemyEntryData, bool AddToEncounterDatabase = true) {

            if (!targetEnemy) { return; }

            string sourceEnemyGUID = targetEnemy.EnemyGuid;
                    
            string m_EnemyNameCode = string.Empty;

            if (!string.IsNullOrEmpty(targetEnemy.ActorName)) {
                m_EnemyNameCode = targetEnemy.ActorName.Replace(" ", "_").Replace("(", "_").Replace(")", string.Empty).ToLower();
            } else {
                return;
            }
            
            if (enemyEntryData.TabSpriteIsTexture) {
                if (!ExpandAssets.LoadAsset<Texture2D>(enemyEntryData.TabSprite)) { return; }
                SpriteBuilder.AddToAmmonomicon(ExpandAssets.LoadAsset<Texture2D>(enemyEntryData.TabSprite));
            } else if (targetEnemy.sprite.Collection.GetSpriteDefinition(enemyEntryData.TabSprite) != null) {
                if (targetEnemy.sprite.Collection.GetSpriteDefinition(enemyEntryData.TabSprite) == null) { return; }
                SpriteBuilder.AddToAmmonomicon(targetEnemy.sprite.Collection.GetSpriteDefinition(enemyEntryData.TabSprite));
            }
            
            bool hasNewEncounterTrackable = targetEnemy.gameObject.GetComponent<EncounterTrackable>();
            EncounterTrackable encounter = targetEnemy.gameObject.GetOrAddComponent<EncounterTrackable>();
            
            if (!string.IsNullOrEmpty(enemyEntryData.encounterGUID)) {
                encounter.EncounterGuid = enemyEntryData.encounterGUID;
            } else if (hasNewEncounterTrackable) {
                encounter.EncounterGuid = sourceEnemyGUID;
            }
            encounter.prerequisites = new DungeonPrerequisite[0];
            encounter.ProxyEncounterGuid = string.Empty;
            encounter.journalData = new JournalEntry() {
                AmmonomiconSprite = enemyEntryData.TabSprite,
                enemyPortraitSprite = ExpandAssets.LoadAsset<Texture2D>(enemyEntryData.FullArtSprite),
                PrimaryDisplayName = "#THE_" + m_EnemyNameCode,
                NotificationPanelDescription = "#THE_" + m_EnemyNameCode + "_SHORTDESC",
                AmmonomiconFullEntry = "#THE_" + m_EnemyNameCode + "_LONGDESC",
                SpecialIdentifier = JournalEntry.CustomJournalEntryType.NONE,
                SuppressKnownState = false,
                SuppressInAmmonomicon = false,
                IsEnemy = true,
                DisplayOnLoadingScreen = false,
                RequiresLightBackgroundInLoadingScreen = false
            };
            
            ExpandTheGungeon.Strings.Enemies.Set("#THE_" + m_EnemyNameCode, enemyEntryData.EnemyName);
            ExpandTheGungeon.Strings.Enemies.Set("#THE_" + m_EnemyNameCode + "_SHORTDESC", enemyEntryData.smallDescription);
            ExpandTheGungeon.Strings.Enemies.Set("#THE_" + m_EnemyNameCode + "_LONGDESC", enemyEntryData.bigDescription);

            EnemyDatabaseEntry targetEnemyEntry = EnemyDatabase.GetEntry(sourceEnemyGUID);

            if (targetEnemyEntry != null) {
                if (!string.IsNullOrEmpty(enemyEntryData.encounterGUID)) { targetEnemyEntry.encounterGuid = enemyEntryData.encounterGUID; }
                targetEnemyEntry.isNormalEnemy = enemyEntryData.IsNormalEnemy;
                targetEnemyEntry.ForcedPositionInAmmonomicon = enemyEntryData.ForcedPositionInAmmonomicon;
                targetEnemyEntry.isInBossTab = enemyEntryData.IsInBossTab;
            }

            string encounterPath = sourceEnemyGUID;
            if (!string.IsNullOrEmpty(enemyEntryData.encounterPath)) { encounterPath = enemyEntryData.encounterPath; }

            if (AddToEncounterDatabase) {
                EncounterDatabaseEntry encounterEntry = new EncounterDatabaseEntry(targetEnemy.encounterTrackable) {
                    path = encounterPath,
                    myGuid = enemyEntryData.encounterGUID,
                    journalData = targetEnemy.encounterTrackable.journalData,
                    doNotificationOnEncounter = false,
                };
                if (string.IsNullOrEmpty(enemyEntryData.encounterGUID)) { encounterEntry.myGuid = sourceEnemyGUID; }
                EncounterDatabase.Instance.Entries.Add(encounterEntry);
            } else {
                string encounterGUID = sourceEnemyGUID;
                if (encounter != null && !string.IsNullOrEmpty(encounter.EncounterGuid) &&
                    encounter.EncounterGuid != targetEnemy.EnemyGuid
                   ) {
                    encounterGUID = encounter.EncounterGuid;
                }
                if (!string.IsNullOrEmpty(enemyEntryData.encounterGUID)) { encounterGUID = enemyEntryData.encounterGUID; }
                EncounterDatabaseEntry encounterEntry = EncounterDatabase.GetEntry(encounterGUID);
                if (encounterEntry != null) { encounterEntry.journalData = encounter.journalData; }
            }
        }
    }
}

