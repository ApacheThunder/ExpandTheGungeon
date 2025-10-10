using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using FullInspector;
using MonoMod.RuntimeDetour;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandComponents;
using Gungeon;
using ExpandTheGungeon.SpriteAPI;
using Dungeonator;
using ExpandTheGungeon.ExpandComponent;
using HarmonyLib;

namespace ExpandTheGungeon.ExpandPrefab {

    // [HarmonyPatch]
    public static class ExpandEnemyDatabase {

        static ExpandEnemyDatabase() {
            BulletManBossGUID = "170bad1ea59344278c996c4ccc3bee51";
            HotShotCultistGUID = "61a8112544ce4389ab14f2287616a71b";
            HotShotShotgunKinGUID = "758a0a0215e6448ab52adf73bc44ae5e";
            HotShotBulletKinGUID = "8a0b7a287410464bb17b9e656958bd19";
            ExplodyBoyGUID = "27638478e52e4785925b578b52bf79d3";
            RatGrenadeGUID = "1a1dc5ed-92a6-4bd1-bbee-098991e7d2d4";
            HammerCompanionGUID = "05145e1a-1a10-4797-b37e-a15bb26d7641";
            BootlegBullatGUID = "7ef020b9-11fb-4a24-a818-60581e6d3105";
            BootlegBulletManGUID = "a52cfba8-f141-4a98-9022-48816201f834";
            BootlegBulletManBandanaGUID = "7093253e-a118-4813-8feb-703a1ad31665";
            BootlegShotgunManRedGUID = "01e4e238-89bb-4b30-b93a-ae17dc19e748";
            BootlegShotgunManBlueGUID = "f7c0b0ab-3d80-4855-9fd6-38861af1147a";
            CronenbergGUID = "0a2433d6e0784eefb28762c5c127d0b3";
            AggressiveCronenbergGUID = "6d2d7a845e464d3ca453fe1fff5fed84";
            MetalCubeGuyWestGUID = "c1e60b8c0691499183c69393e02c9de9";
            FriendlyCultistGUID = "1d1e1070617842f09e6f45df3cb223f6";
            SonicCompanionGUID = "38e61aef773a481697c4956d85279087";
            corruptedEnemyGUID = "182c39c4d904493283f75ab29775d9c6";
            doppelgunnerbossEnemyGUID = "5f0fa34b5a2e44cdab4a06f89bb5c442";
            ParasiteBossGUID = "acd8d483f24e4c43b964fa4e54068cf1";
            com4nd0GUID = "0a406e36-80eb-43b8-8ad0-c56232f9496e";
            ClownkinGUID = "5736cc6185294b839666c65ac8e082c1";
            ClownkinAltGUID = "dd1505fb84744002ad42ee8316b86ea0";
            ClownkinNoFXGUID = "ccd416569b6d4ca0bb057a837a517d73";
            ClownkinAngryGUID = "3eee833068614536a5f56cbe7dc6cfe9";
            EntityGUID = "0108a031c74940739c56a22068c915b6";
            PoisbulordGUID = "b19ec5d13d754e5f8293910e10bf7ff1";
            PoisbulordCrawlerGUID = "aa060bcd358048b8ac99127571e500ae";
        }

        // Saved GUIDs for use in things like room prefabs
        public static readonly string BulletManBossGUID;
        public static readonly string HotShotCultistGUID;
        public static readonly string HotShotShotgunKinGUID;
        public static readonly string HotShotBulletKinGUID;
        public static readonly string ExplodyBoyGUID;
        public static readonly string RatGrenadeGUID;
        public static readonly string HammerCompanionGUID;
        public static readonly string BootlegBullatGUID;
        public static readonly string BootlegBulletManGUID;
        public static readonly string BootlegBulletManBandanaGUID;
        public static readonly string BootlegShotgunManRedGUID;
        public static readonly string BootlegShotgunManBlueGUID;
        public static readonly string CronenbergGUID;
        public static readonly string AggressiveCronenbergGUID;
        public static readonly string MetalCubeGuyWestGUID;
        public static readonly string FriendlyCultistGUID;
        public static readonly string SonicCompanionGUID;
        public static readonly string corruptedEnemyGUID;
        public static readonly string doppelgunnerbossEnemyGUID;
        public static readonly string ParasiteBossGUID;
        public static readonly string com4nd0GUID;
        public static readonly string ClownkinGUID;
        public static readonly string ClownkinAltGUID;
        public static readonly string ClownkinNoFXGUID;
        public static readonly string ClownkinAngryGUID;
        public static readonly string EntityGUID;
        public static readonly string PoisbulordGUID;
        public static readonly string PoisbulordCrawlerGUID;


        public static Hook loadEnemyGUIDHook;

        public static Dictionary<string, AIActor> enemyPrefabDictionary = new Dictionary<string, AIActor>();

        // SpriteCollections
        public static GameObject BabyGoodHammerCollection;
        public static GameObject BootlegBullatCollection;
        public static GameObject BootlegBulletManCollection;
        public static GameObject BootlegBulletManBandanaCollection;
        public static GameObject BootlegShotgunManBlueCollection;
        public static GameObject BootlegShotgunManRedCollection;
        public static GameObject ClownkinCollection;
        public static GameObject CronenbergCollection;
        public static GameObject CronenbergTallCollection;
        public static GameObject EntityCollection;
        public static GameObject CultistCompanionCollection;
        public static GameObject SonicCompanionCollection;
        public static GameObject GungeoneerMimicCollection;
        public static GameObject WestBrosCollection;        

        // Companions
        public static GameObject HammerCompanionPrefab;
        public static GameObject FriendlyCultistPrefab;
        public static GameObject SonicCompanionPrefab;

        // Normal Enemies
        public static GameObject BuildHotShotGunCultistPrefab;
        public static GameObject HotShotShotgunKinPrefab;
        public static GameObject HotShotBulletKinPrefab;
        public static GameObject RatGrenadePrefab;
        public static GameObject BootlegBullatPrefab;
        public static GameObject BootlegBulletManPrefab;
        public static GameObject BootlegBulletManBandanaPrefab;
        public static GameObject BootlegShotgunManRedPrefab;
        public static GameObject BootlegShotgunManBluePrefab;
        public static GameObject ClownkinPrefab;
        public static GameObject ClownkinAltPrefab;
        public static GameObject ClownkinNoFXPrefab;
        public static GameObject ClownkinAngryPrefab;
        public static GameObject CronenbergPrefab;
        public static GameObject EntityPrefab;
        public static GameObject MetalCubeGuyWestPrefab;
        public static GameObject AggressiveCronenbergPrefab;
        public static GameObject CorruptedEnemyPrefab;

        // Dummy Explody Barrel Enemy for use with paradrop. (for some reason I get broken behavior on regular objects. 
        // Can't resolve yet so just make this a full on AIActor since they all work properly on it for reasons I don't understand yet.
        public static GameObject ExplodyBoyPrefab;

        // Custom/Modified bosses
        // public static GameObject KillStumpsDummy;
        public static GameObject MonsterParasitePrefab;
        public static GameObject com4nd0BossPrefab;
        public static GameObject DoppelGunnerPrefab;
        public static GameObject BulletManBossPrefab;
        public static GameObject PoisbulordPrefab;
        public static GameObject PoisbulordCrawlerPrefab; // Piosbulord Crawler

        // Enemies with pallete system disabled
        public static GameObject RedShotGunMan;
        public static GameObject BlueShotGunMan;
        public static GameObject RedShotgunManCollection;
        public static GameObject BlueShotgunManCollection;
        public static GameObject BulletManEyepatch;
        public static GameObject BulletManEyepatchCollection;


        // Misc Objects
        public static GameObject CronenbergCorpseDebrisObject1;
        public static GameObject CronenbergCorpseDebrisObject2;
        public static GameObject CronenbergCorpseDebrisObject3;
        public static GameObject CronenbergCorpseDebrisObject4;
        public static GameObject AggressiveCronenbergCorpseDebrisObject;
        public static GameObject StoneCubeCollection_West;
        public static GameObject DopplegunnerHand;
        public static GameObject ClownkinWig;
        
        // Poisbulord VFX Objects
        public static GameObject VFX_Poisbulord_Splash;
        public static GameObject VFX_Poisbulord_Die_Big;
        public static GameObject VFX_Poisbulord_Die_Small;

        // Poisbulord Projectiles (these versions produced when he does split attack.
        // These versions leave poison goop
        public static GameObject Poisbulord_Poisbulon_Projectile;
        public static GameObject Poisbulord_Poisbuloid_Projectile;
        // public static GameObject Poisbulord_Poisbulin_Projectile;

        public static Texture2D[] RatGrenadeTextures;
        
        private static AIActor Chameleon;
        private static AIActor Skusketling;

        public static Texture2D ModifiedCompanionsAtlas;
        public static Texture2D PoisbulordAtlas;

        // public static tk2dSpriteCollectionData ModifiedCompanionCollection;


        public static void InitSpriteCollections(AssetBundle expandSharedAssets1) {
            BabyGoodHammerCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "BabyGoodHammerCollection", "BabyGoodHammer_Collection", "BabyGoodHammerCollection");
            BootlegBullatCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "BootlegBullatCollection", "BootlegBullat_Collection", "BootlegBullatCollection");
            BootlegBulletManCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "BootlegBulletManCollection", "BootlegBulletMan_Collection", "BootlegBulletManCollection");
            BootlegBulletManBandanaCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "BootlegBulletManBandanaCollection", "BootlegBulletManBandana_Collection", "BootlegBulletManBandanaCollection");
            BootlegShotgunManBlueCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "BootlegShotgunManBlueCollection", "BootlegShotgunManBlue_Collection", "BootlegShotgunManBlueCollection");
            BootlegShotgunManRedCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "BootlegShotgunManRedCollection", "BootlegShotgunManRed_Collection", "BootlegShotgunManRedCollection");
            ClownkinCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "ClownKinCollection", "Clownkin_Collection", "ClownKinCollection");
            CronenbergCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "CronenbergCollection", "Cronenberg_Collection", "CronenbergCollection");
            CronenbergTallCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "CronenbergTallCollection", "Cronenberg_Tall_Collection", "CronenbergTallCollection");
            EntityCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EntityCollection", "Entity_Collection", "EntityCollection");
            CultistCompanionCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "CultistCompanionCollection", "CultistCompanion_Collection", "CultistCompanionCollection");
            SonicCompanionCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "SonicCompanionCollection", "SonicCompanion_Collection", "SonicCompanionCollection");
            GungeoneerMimicCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "GungeoneerMimicCollection", "GungeoneerMimic_Collection", "GungeoneerMimicCollection");
            WestBrosCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "WestBrosCollection", "WestBros_Collection", "WestBrosCollection");

            // Collider settings for Black/Golden Revolver's projectiles. Adjust this if it's not to your liking. 
            // DefineProjectileCollision extension can be found in ExpandUtility.cs
            tk2dSpriteCollectionData WestBrosCollectionData = WestBrosCollection.GetComponent<tk2dSpriteCollectionData>();
            for (int i = 1; i < 7; i++) {
                WestBrosCollectionData.DefineProjectileCollision("gr_black_revolver_projectile_00" + i, 12, 6, overrideColliderOffsetY: 1);
            }

            ModifiedCompanionsAtlas = expandSharedAssets1.LoadAsset<Texture2D>("ModifiedCompanions_Collection");
            PoisbulordAtlas = expandSharedAssets1.LoadAsset<Texture2D>("Poisbulord_Collection");

            // ModifiedCompanionCollection = ExpandUtility.BuildSpriteCollection(EnemyDatabase.GetOrLoadByGuid("6f9c28403d3248c188c391f5e40774c5").sprite.Collection, ModifiedCompanionsAtlas, null, null, true);
        }

        public static void InitPrefabs(AssetBundle expandSharedAssets1) {

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing EnemyDatabase.GetOrLoadByGuid Hook...."); }
            loadEnemyGUIDHook = new Hook(
                typeof(EnemyDatabase).GetMethod(nameof(EnemyDatabase.GetOrLoadByGuid), BindingFlags.Static | BindingFlags.Public),
                typeof(ExpandEnemyDatabase).GetMethod(nameof(GetOrLoadByGuidHook), BindingFlags.Static | BindingFlags.Public)
            );
            
            // Palette Fix to Red/Blue Shotgun Kin and Veteran Bullet Kin (so they work correctly with glitch shader)
            PaletteFixEnemies(expandSharedAssets1);
            
            // Real Prefabs
            BuildHotShotCultistPrefab(expandSharedAssets1, out BuildHotShotGunCultistPrefab);
            BuildHotShotShotgunManPrefab(expandSharedAssets1, out HotShotShotgunKinPrefab);
            BuildHotShotBulletManPrefab(expandSharedAssets1, out HotShotBulletKinPrefab);
            BuildBabyGoodHammerPrefab(expandSharedAssets1, out HammerCompanionPrefab);
            BuildBootlegBullatPrefab(expandSharedAssets1, out BootlegBullatPrefab);
            BuildBootlegBulletManPrefab(expandSharedAssets1, out BootlegBulletManPrefab);
            BuildBootlegBulletManBandanaPrefab(expandSharedAssets1, out BootlegBulletManBandanaPrefab);
            BuildBootlegShotgunManRedPrefab(expandSharedAssets1, out BootlegShotgunManRedPrefab);
            BuildBootlegShotgunManBluePrefab(expandSharedAssets1, out BootlegShotgunManBluePrefab);
            BuildClownkinPrefab(expandSharedAssets1, out ClownkinPrefab, out ClownkinWig);
            BuildClownkinAltPrefab(expandSharedAssets1, out ClownkinAltPrefab);
            BuildClownkinNoFXPrefab(expandSharedAssets1, out ClownkinNoFXPrefab);
            BuildClownkinAngryPrefab(expandSharedAssets1, out ClownkinAngryPrefab);
            BuildDummyExplodyBarrelGuyPrefab(expandSharedAssets1, out ExplodyBoyPrefab);
            BuildCronenbergPrefab(expandSharedAssets1, out CronenbergPrefab);
            BuildAggressiveCronenbergPrefab(expandSharedAssets1, out AggressiveCronenbergPrefab);
            BuildCultistCompanionPrefab(expandSharedAssets1, out FriendlyCultistPrefab);
            BuildSonicCompanionPrefab(expandSharedAssets1, out SonicCompanionPrefab);
            BuildCorruptedEnemyPrefab(expandSharedAssets1, out CorruptedEnemyPrefab);
            BuildEntityPrefab(expandSharedAssets1, out EntityPrefab);

            BuildDoppelGunnerBossPrefab(expandSharedAssets1, out DoppelGunnerPrefab);

            BuildBulletManBossPrefab(expandSharedAssets1, out BulletManBossPrefab);

            BuildPoisbulordBossPrefab(expandSharedAssets1, out PoisbulordPrefab);

            BuildPoisbulordCrawlerPrefab(expandSharedAssets1, out PoisbulordCrawlerPrefab);
            
            ExpandWesternBrosPrefabBuilder.BuildWestBrosBossPrefabs(expandSharedAssets1);
            
            // Fake Prefabs
            BuildRatGrenadePrefab(out RatGrenadePrefab);
            BuildMetalCubeGuyWestPrefab(expandSharedAssets1, out MetalCubeGuyWestPrefab);
            BuildParasiteBossPrefab(out MonsterParasitePrefab);
            BuildJungleBossPrefab(out com4nd0BossPrefab);

            UpdateAmmonoiconDatabase(expandSharedAssets1);
        }

        public static void UpdateAmmonoiconDatabase(AssetBundle expandSharedAssets1) {
            Chameleon = GetOfficialEnemyByGuid("80ab6cd15bfc46668a8844b2975c6c26");
            Skusketling = GetOfficialEnemyByGuid("c2f902b7cbe745efb3db4399927eab34");
            
            ExpandAmmonomiconDatabase.AddExistingEnemyToAmmonomicon(Chameleon, ExpandAmmonomiconDatabase.Chameleon, false);
            ExpandAmmonomiconDatabase.AddExistingEnemyToAmmonomicon(Skusketling, ExpandAmmonomiconDatabase.Skusketling);
        }

        public static AIActor GetOrLoadByGuidHook(Func<string, AIActor> orig, string guid) {
            AIActor enemyPrefab;
            if (enemyPrefabDictionary != null && enemyPrefabDictionary.TryGetValue(guid, out enemyPrefab)) { return enemyPrefab; } else { return orig(guid); }
        }

        // This was randomly breaking something on loading of a new floor sometimes.
        /*[HarmonyPatch(typeof(EnemyDatabase), nameof(EnemyDatabase.GetOrLoadByGuid), typeof(string))]
        [HarmonyPostfix]
        private static void GetOrLoadByGuidPatch(EnemyDatabase __instance, string guid, ref AIActor __result) {
            if (enemyPrefabDictionary != null) {
                AIActor enemyPrefab;
                if (enemyPrefabDictionary.TryGetValue(guid, out enemyPrefab)) __result = enemyPrefab;
            }
        }*/

        public static AIActor GetOfficialEnemyByGuid(string guid) { return EnemyDatabase.Instance.InternalGetByGuid(guid); }

        public static void PaletteFixEnemies(AssetBundle expandSharedAssets1) {

            RedShotGunMan = EnemyDatabase.GetOrLoadByGuid("128db2f0781141bcb505d8f00f9e4d47").gameObject;
            BlueShotGunMan = EnemyDatabase.GetOrLoadByGuid("b54d89f9e802455cbb2b8a96a31e8259").gameObject;
            BulletManEyepatch = EnemyDatabase.GetOrLoadByGuid("70216cae6c1346309d86d4a0b4603045").gameObject;
            RedShotgunManCollection = expandSharedAssets1.LoadAsset<GameObject>("RedShotgunManCollection");
            BlueShotgunManCollection = expandSharedAssets1.LoadAsset<GameObject>("BlueShotgunManCollection");
            BulletManEyepatchCollection = expandSharedAssets1.LoadAsset<GameObject>("BulletManEyepatchCollection");

            // Red Shotgun Kin
            AIActor RedShotGunEnemy = RedShotGunMan.GetComponent<AIActor>();

            tk2dSpriteCollectionData RedShotGunCollectionData = RedShotgunManCollection.AddComponent<tk2dSpriteCollectionData>();

            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(RedShotGunEnemy.sprite.Collection), RedShotGunCollectionData);

            Material m_NewRedShotGunManMaterial = new Material(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").sprite.Collection.materials[0]);
            m_NewRedShotGunManMaterial.mainTexture = expandSharedAssets1.LoadAsset<Texture2D>("RedBulletShotgunMan");
            RedShotGunCollectionData.materials[0] = m_NewRedShotGunManMaterial;

            foreach (tk2dSpriteDefinition spriteDefinition in RedShotGunCollectionData.spriteDefinitions) { spriteDefinition.material = m_NewRedShotGunManMaterial; }
            RedShotGunEnemy.sprite.Collection = RedShotGunCollectionData;

            ExpandUtility.DuplicateSpriteAnimation(RedShotgunManCollection, RedShotgunManCollection.AddComponent<tk2dSpriteAnimation>(), RedShotGunEnemy.spriteAnimator.Library, RedShotGunCollectionData);
            RedShotGunEnemy.spriteAnimator.Library = RedShotgunManCollection.GetComponent<tk2dSpriteAnimation>();
            RedShotGunEnemy.optionalPalette = null;

            // Blue Shotgun Kin
            AIActor BlueShotGunEnemy = BlueShotGunMan.GetComponent<AIActor>();

            tk2dSpriteCollectionData BlueShotGunCollectionData = BlueShotgunManCollection.AddComponent<tk2dSpriteCollectionData>();

            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(BlueShotGunEnemy.sprite.Collection), BlueShotGunCollectionData);

            Material m_NewBlueShotGunManMaterial = new Material(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").sprite.Collection.materials[0]);
            m_NewBlueShotGunManMaterial.mainTexture = expandSharedAssets1.LoadAsset<Texture2D>("BlueBulletShotgunMan");
            BlueShotGunCollectionData.materials[0] = m_NewBlueShotGunManMaterial;

            foreach (tk2dSpriteDefinition spriteDefinition in BlueShotGunCollectionData.spriteDefinitions) { spriteDefinition.material = m_NewBlueShotGunManMaterial; }
            BlueShotGunEnemy.sprite.Collection = BlueShotGunCollectionData;

            ExpandUtility.DuplicateSpriteAnimation(BlueShotgunManCollection, BlueShotgunManCollection.AddComponent<tk2dSpriteAnimation>(), BlueShotGunEnemy.spriteAnimator.Library, BlueShotGunCollectionData);
            BlueShotGunEnemy.spriteAnimator.Library = BlueShotgunManCollection.GetComponent<tk2dSpriteAnimation>();
            BlueShotGunEnemy.optionalPalette = null;

            // Veteran Bullet Kin
            AIActor BulletManEyepatchEnemy = BulletManEyepatch.GetComponent<AIActor>();

            tk2dSpriteCollectionData BulletManEyepatchCollectionData = BulletManEyepatchCollection.AddComponent<tk2dSpriteCollectionData>();

            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(BulletManEyepatchEnemy.sprite.Collection), BulletManEyepatchCollectionData);

            Material m_NewBulletManEyepatchMaterial = new Material(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").sprite.Collection.materials[0]);
            m_NewBulletManEyepatchMaterial.mainTexture = expandSharedAssets1.LoadAsset<Texture2D>("BulletManEyepatch");
            BulletManEyepatchCollectionData.materials[0] = m_NewBulletManEyepatchMaterial;

            foreach (tk2dSpriteDefinition spriteDefinition in BulletManEyepatchCollectionData.spriteDefinitions) { spriteDefinition.material = m_NewBulletManEyepatchMaterial; }
            BulletManEyepatchEnemy.sprite.Collection = BulletManEyepatchCollectionData;

            ExpandUtility.DuplicateSpriteAnimation(BulletManEyepatchCollection, BulletManEyepatchCollection.AddComponent<tk2dSpriteAnimation>(), BulletManEyepatchEnemy.spriteAnimator.Library, BulletManEyepatchCollectionData);
            BulletManEyepatchEnemy.spriteAnimator.Library = BulletManEyepatchCollection.GetComponent<tk2dSpriteAnimation>();
            BulletManEyepatchEnemy.optionalPalette = null;
        }
        
        public static void AddEnemyToDatabase(GameObject EnemyPrefab, string EnemyGUID, bool IsNormalEnemy = false, bool AddToMTGSpawnPool = true) {
            EnemyDatabaseEntry entry = new EnemyDatabaseEntry {
                myGuid = EnemyGUID,
                placeableWidth = 1,
                placeableHeight = 1,
                isNormalEnemy = IsNormalEnemy
            };
            EnemyDatabase.Instance.Entries.Add(entry);
            enemyPrefabDictionary.Add(EnemyGUID, EnemyPrefab.GetComponent<AIActor>());
            if (AddToMTGSpawnPool && !string.IsNullOrEmpty(EnemyPrefab.GetComponent<AIActor>().ActorName)) {
                string EnemyName = EnemyPrefab.GetComponent<AIActor>().ActorName.Replace(" ", "_").Replace("(", "_").Replace(")", string.Empty).ToLower();
                if (!Game.Enemies.ContainsID(EnemyName)) { Game.Enemies.Add(EnemyName, EnemyPrefab.GetComponent<AIActor>()); }
            }
        }

        public static void AddEnemyToDatabaseAndAmmonomicon(AIActor targetEnemy, string EnemyGUID, ExpandAmmonomiconDatabase.EnemyEntryData enemyEntryData, bool AddToMTGSpawnPool = true) {

            if (!targetEnemy) { return; }

            string m_EnemyNameCode = string.Empty;

            if (!string.IsNullOrEmpty(targetEnemy.ActorName)) {
                m_EnemyNameCode = targetEnemy.ActorName.Replace(" ", "_").Replace("(", "_").Replace(")", string.Empty).ToLower();
            } else {
                return;
            }

            ExpandTheGungeon.Strings.Enemies.Set("#THE_" + m_EnemyNameCode, enemyEntryData.EnemyName);
            ExpandTheGungeon.Strings.Enemies.Set("#THE_" + m_EnemyNameCode + "_SHORTDESC", enemyEntryData.smallDescription);
            ExpandTheGungeon.Strings.Enemies.Set("#THE_" + m_EnemyNameCode + "_LONGDESC", enemyEntryData.bigDescription);

            if (enemyEntryData.TabSpriteIsTexture) {
                SpriteBuilder.AddToAmmonomicon(ExpandAssets.LoadAsset<Texture2D>(enemyEntryData.TabSprite));
            } else if (targetEnemy.sprite.Collection.GetSpriteDefinition(enemyEntryData.TabSprite) != null) {
                SpriteBuilder.AddToAmmonomicon(targetEnemy.sprite.Collection.GetSpriteDefinition(enemyEntryData.TabSprite));
            }

            EncounterTrackable encounterTrackable = targetEnemy.gameObject.GetOrAddComponent<EncounterTrackable>();
            encounterTrackable.EncounterGuid = EnemyGUID;
            encounterTrackable.prerequisites = new DungeonPrerequisite[0];
            encounterTrackable.ProxyEncounterGuid = string.Empty;
            encounterTrackable.journalData = new JournalEntry() {
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
                        
            EnemyDatabaseEntry enemyEntry = new EnemyDatabaseEntry {
                myGuid = EnemyGUID,
                path = EnemyGUID,
                encounterGuid = EnemyGUID,
                difficulty = enemyEntryData.EnemyDifficulty,
                placeableWidth = enemyEntryData.placeableWidth,
                placeableHeight = enemyEntryData.placeableHeight,
                isNormalEnemy = enemyEntryData.IsNormalEnemy,
                ForcedPositionInAmmonomicon = enemyEntryData.ForcedPositionInAmmonomicon,
                isInBossTab = enemyEntryData.IsInBossTab,
            };
            
            EncounterDatabaseEntry encounterEntry = new EncounterDatabaseEntry(encounterTrackable) {
                path = EnemyGUID,
                myGuid = EnemyGUID,
                journalData = encounterTrackable.journalData,
                doNotificationOnEncounter = false
            };
            
            if (!string.IsNullOrEmpty(enemyEntryData.encounterPath)) {
                encounterEntry.path = enemyEntryData.encounterPath;
                enemyEntry.path = enemyEntryData.encounterPath;
            }
            if (!string.IsNullOrEmpty(enemyEntryData.encounterGUID)) {
                encounterEntry.myGuid = enemyEntryData.encounterGUID;
                encounterTrackable.EncounterGuid = enemyEntryData.encounterGUID;
                enemyEntry.encounterGuid = enemyEntryData.encounterGUID;
            }

            EnemyDatabase.Instance.Entries.Add(enemyEntry);
            EncounterDatabase.Instance.Entries.Add(encounterEntry);
            enemyPrefabDictionary.Add(EnemyGUID, targetEnemy);

            if (AddToMTGSpawnPool && !string.IsNullOrEmpty(m_EnemyNameCode)) {
                if (!Game.Enemies.ContainsID(m_EnemyNameCode)) { Game.Enemies.Add(m_EnemyNameCode, targetEnemy); }
            }
        }

        public static void BuildHotShotCultistPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("57255ed50ee24794b7aac1ac3cfb8a95");

            GameObject m_DummyCorpseObject = null;
            
            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("HotShotCultist");
            
            GameObject LeftHand = m_CachedTargetObject.transform.Find("left hand").gameObject;
            GameObject RightHand = m_CachedTargetObject.transform.Find("right hand").gameObject;
            GunHandController LeftHandController = LeftHand.AddComponent<GunHandController>();
            GunHandController RightHandController = RightHand.AddComponent<GunHandController>();

            LeftHandController.GunId = 38;
            LeftHandController.UsesOverrideProjectileData = true;
            LeftHandController.OverrideProjectile = m_CachedEnemyActor.gameObject.GetComponent<AIBulletBank>().Bullets[0].BulletObject.GetComponent<Projectile>();
            LeftHandController.OverrideProjectileData = m_CachedEnemyActor.gameObject.GetComponent<AIBulletBank>().Bullets[0].ProjectileData;
            LeftHandController.GunFlipMaster = RightHandController;
            LeftHandController.handObject = m_CachedEnemyActor.aiShooter.handObject;
            LeftHandController.gunBehindBody = new GunHandController.DirectionalAnimationBoolSixWay() {
                Back = true,
                BackRight = false,
                ForwardRight = false,
                Forward = false,
                ForwardLeft = true,
                BackLeft = true
            };
            LeftHandController.gunBehindBodyEight = new GunHandController.DirectionalAnimationBoolEightWay();
            LeftHandController.isEightWay = false;
            LeftHandController.PreFireDelay = 0;
            LeftHandController.NumShots = 3;
            LeftHandController.ShotCooldown = 0.18f;
            LeftHandController.Cooldown = 1f;
            LeftHandController.RampBullets = false;
            LeftHandController.RampStartHeight = 2;
            LeftHandController.RampTime = 1;


            RightHandController.GunId = 38;
            RightHandController.UsesOverrideProjectileData = true;
            RightHandController.OverrideProjectile = LeftHandController.OverrideProjectile;
            RightHandController.OverrideProjectileData = LeftHandController.OverrideProjectileData;
            RightHandController.handObject = m_CachedEnemyActor.aiShooter.handObject;
            RightHandController.gunBehindBody = new GunHandController.DirectionalAnimationBoolSixWay() {
                Back = true,
                BackRight = true,
                ForwardRight = true,
                Forward = false,
                ForwardLeft = false,
                BackLeft = false
            };
            RightHandController.gunBehindBodyEight = new GunHandController.DirectionalAnimationBoolEightWay();
            RightHandController.isEightWay = false;
            RightHandController.PreFireDelay = 0;
            RightHandController.NumShots = 2;
            RightHandController.ShotCooldown = 0.25f;
            RightHandController.Cooldown = 1.28f;
            RightHandController.RampBullets = false;
            RightHandController.RampStartHeight = 2;
            RightHandController.RampTime = 1;
            
            SpeculativeRigidbody m_CachedRigidBody = m_CachedTargetObject.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateRigidBody(m_CachedRigidBody, m_CachedEnemyActor.specRigidbody);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "HotShot Gun Cultist", HotShotCultistGUID, (tk2dSprite)m_CachedEnemyActor.sprite, instantiateCorpseObject: false, ExternalCorpseObject: m_CachedEnemyActor.CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }
            
            if (m_CachedAIActor.aiAnimator) {
                ExpandUtility.DuplicateComponent(m_CachedAIActor.aiAnimator, m_CachedEnemyActor.aiAnimator);
            }
            if (m_CachedAIActor.spriteAnimator) {
                ExpandUtility.DuplicateComponent(m_CachedAIActor.spriteAnimator, m_CachedEnemyActor.spriteAnimator);
            }

            tk2dSpriteCollectionData HotShotCollectionData = m_CachedTargetObject.AddComponent<tk2dSpriteCollectionData>();

            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(m_CachedEnemyActor.sprite.Collection), HotShotCollectionData);

            Material m_NewHotShotMaterial = new Material(m_CachedEnemyActor.sprite.Collection.materials[0]);
            m_NewHotShotMaterial.mainTexture = expandSharedAssets1.LoadAsset<Texture2D>("HotShotCultist_Collection");
            HotShotCollectionData.materials[0] = m_NewHotShotMaterial;
            
            foreach (tk2dSpriteDefinition spriteDefinition in HotShotCollectionData.spriteDefinitions) { spriteDefinition.material = m_NewHotShotMaterial; }
            m_CachedAIActor.sprite.Collection = HotShotCollectionData;
            
            ExpandUtility.DuplicateSpriteAnimation(m_CachedTargetObject, m_CachedTargetObject.AddComponent<tk2dSpriteAnimation>(), m_CachedEnemyActor.spriteAnimator.Library, HotShotCollectionData);
            m_CachedAIActor.spriteAnimator.Library = m_CachedTargetObject.GetComponent<tk2dSpriteAnimation>();
            

            m_CachedAIActor.MovementSpeed = 3.75f;
            m_CachedAIActor.HasShadow = true;
            m_CachedAIActor.ActorShadowOffset = m_CachedEnemyActor.ActorShadowOffset;
            m_CachedAIActor.EnemySwitchState = m_CachedEnemyActor.EnemySwitchState;
            m_CachedAIActor.ShadowHeightOffGround = m_CachedEnemyActor.ShadowHeightOffGround;
            m_CachedAIActor.shadowHeightOffset = m_CachedEnemyActor.shadowHeightOffset;
            m_CachedAIActor.TransferShadowToCorpse = false;
            m_CachedAIActor.shadowDeathType = AIActor.ShadowDeathType.Fade;
            m_CachedAIActor.ShadowObject = null;

            m_CachedAIActor.PathableTiles = CellTypes.FLOOR;

            m_CachedAIActor.healthHaver.SetHealthMaximum(23);
            m_CachedAIActor.healthHaver.CursedMaximum = (23 * 3);
            
            ExpandHideGunHandsOnDeath hideGunHandsOnDeath = m_CachedTargetObject.AddComponent<ExpandHideGunHandsOnDeath>();
            hideGunHandsOnDeath.gunHands.Add(LeftHandController);
            hideGunHandsOnDeath.gunHands.Add(RightHandController);

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>() {
                new RedBarrelAwareness() { AvoidRedBarrels = true, ShootRedBarrels = true, PushRedBarrels = true }
            };
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 7,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.25f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };

            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new SequentialAttackBehaviorGroup() {
                    OverrideCooldowns = null,
                    RunInClass = false,
                    AttackBehaviors = new List<AttackBehaviorBase>() {
                        new ExpandDashBehavior() {
                            gunHands = new List<GunHandController>() { LeftHandController, RightHandController },
                            dashDirection = ExpandDashBehavior.DashDirection.Random,
                            quantizeDirection = 45,
                            dashDistance = 5,
                            dashTime = 0.65f,
                            postDashSpeed = 0,
                            doubleDashChance = 0,
                            avoidTarget = false,
                            ShootPoint = null,
                            bulletScript = null,
                            fireAtDashStart = false,
                            stopOnCollision = false,
                            chargeAnim = null,
                            dashAnim = "dodgeroll",
                            doDodgeDustUp = true,
                            warpDashAnimLength = true,
                            hideShadow = false,
                            hideGun = true,
                            toggleTrailRenderer = false,
                            enableShadowTrail = false,
                            Cooldown = 0.25f,
                            CooldownVariance = 0,
                            AttackCooldown = 0,
                            GlobalCooldown = 0,
                            InitialCooldown = 0,
                            InitialCooldownVariance = 0,
                            GroupName = null,
                            GroupCooldown = 0,
                            MinRange = 0,
                            Range = 0,
                            MinWallDistance = 0,
                            MaxEnemiesInRoom = 0,
                            MinHealthThreshold = 0,
                            MaxHealthThreshold = 1,
                            HealthThresholds = new float[0],
                            AccumulateHealthThresholds = true,
                            targetAreaStyle = null,
                            IsBlackPhantom = false,
                            resetCooldownOnDamage = null,
                            RequiresLineOfSight = false,
                            MaxUsages = 0
                        },
                        new ExpandDashBehavior() {
                            gunHands = new List<GunHandController>() { LeftHandController, RightHandController },
                            dashDirection = ExpandDashBehavior.DashDirection.Random,
                            quantizeDirection = 45,
                            dashDistance = 5,
                            dashTime = 0.65f,
                            postDashSpeed = 0,
                            doubleDashChance = 0,
                            avoidTarget = false,
                            ShootPoint = null,
                            bulletScript = null,
                            fireAtDashStart = false,
                            stopOnCollision = false,
                            chargeAnim = null,
                            dashAnim = "dodgeroll",
                            doDodgeDustUp = true,
                            warpDashAnimLength = true,
                            hideShadow = false,
                            hideGun = true,
                            toggleTrailRenderer = false,
                            enableShadowTrail = false,
                            Cooldown = 6f,
                            CooldownVariance = 2,
                            AttackCooldown = 0,
                            GlobalCooldown = 0,
                            InitialCooldown = 6,
                            InitialCooldownVariance = 2,
                            GroupName = null,
                            GroupCooldown = 0,
                            MinRange = 0,
                            Range = 0,
                            MinWallDistance = 0,
                            MaxEnemiesInRoom = 0,
                            MinHealthThreshold = 0,
                            MaxHealthThreshold = 1,
                            HealthThresholds = new float[0],
                            AccumulateHealthThresholds = true,
                            targetAreaStyle = null,
                            IsBlackPhantom = false,
                            resetCooldownOnDamage = new BasicAttackBehavior.ResetCooldownOnDamage() { Cooldown = true, AttackCooldown = false, GlobalCooldown = false, GroupCooldown = false, ResetCooldown = 4 },
                            RequiresLineOfSight = false,
                            MaxUsages = 0
                        },
                    }
                },
                new AttackBehaviorGroup() {
                    ShareCooldowns = false,
                    AttackBehaviors = new List<AttackBehaviorGroup.AttackGroupItem>() {
                        new AttackBehaviorGroup.AttackGroupItem() {
                            NickName = "Dual Wield Shoot",
                            Probability = 1,
                            Behavior = new GunHandBasicShootBehavior() {
                                MaxUsages = 0,
                                RequiresLineOfSight = false,
                                resetCooldownOnDamage = null,
                                IsBlackPhantom = false,
                                targetAreaStyle = null,
                                AccumulateHealthThresholds = true,
                                HealthThresholds = new float[0],
                                MaxHealthThreshold = 1,
                                MinHealthThreshold = 0,
                                MaxEnemiesInRoom = 0,
                                MinWallDistance = 0,
                                Range = 12,
                                MinRange = 0,
                                GroupCooldown = 0,
                                GroupName = null,
                                InitialCooldownVariance = 0,
                                InitialCooldown = 0,
                                GlobalCooldown = 0,
                                AttackCooldown = 0.25f,
                                CooldownVariance = 0,
                                Cooldown = 0.25f,
                                GunHands = new List<GunHandController>() { LeftHandController, RightHandController },
                                FireAllGuns = false,
                                LineOfSight = true
                            }
                        }
                    }
                }
            };
            
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>() { LeftHandController, RightHandController };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, HotShotCultistGUID, ExpandAmmonomiconDatabase.HotShotCultist);

            m_CachedEnemyActor = null;
            return;
        }

        public static void BuildHotShotShotgunManPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedEnemyActor = RedShotGunMan.GetComponent<AIActor>(); // Red ShotGun Man

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("HotShotShotgunKin");
            
            GameObject LeftHand = m_CachedTargetObject.transform.Find("left hand").gameObject;
            GameObject RightHand = m_CachedTargetObject.transform.Find("right hand").gameObject;

            GunHandController LeftHandController = LeftHand.AddComponent<GunHandController>();
            GunHandController RightHandController = RightHand.AddComponent<GunHandController>();

            LeftHandController.GunId = HotShotShotGun.HotShotShotGunID;
            LeftHandController.UsesOverrideProjectileData = true;
            LeftHandController.OverrideProjectileData = m_CachedEnemyActor.gameObject.GetComponent<AIBulletBank>().Bullets[0].ProjectileData;
            LeftHandController.GunFlipMaster = RightHandController;
            LeftHandController.handObject = m_CachedEnemyActor.aiShooter.handObject;
            LeftHandController.gunBehindBody = new GunHandController.DirectionalAnimationBoolSixWay() {
                Back = true,
                BackRight = false,
                ForwardRight = false,
                Forward = false,
                ForwardLeft = true,
                BackLeft = true
            };
            LeftHandController.gunBehindBodyEight = new GunHandController.DirectionalAnimationBoolEightWay();
            LeftHandController.isEightWay = false;
            LeftHandController.PreFireDelay = 0.1f;
            LeftHandController.NumShots = 1;
            LeftHandController.ShotCooldown = 1;
            LeftHandController.Cooldown = 2.5f;
            LeftHandController.RampBullets = false;
            LeftHandController.RampStartHeight = 2;
            LeftHandController.RampTime = 1;

            RightHandController.GunId = HotShotShotGun.HotShotShotGunID;
            RightHandController.UsesOverrideProjectileData = true;
            RightHandController.OverrideProjectileData = LeftHandController.OverrideProjectileData;
            RightHandController.handObject = m_CachedEnemyActor.aiShooter.handObject;
            RightHandController.gunBehindBody = new GunHandController.DirectionalAnimationBoolSixWay() {
                Back = true,
                BackRight = true,
                ForwardRight = true,
                Forward = false,
                ForwardLeft = false,
                BackLeft = false
            };
            RightHandController.gunBehindBodyEight = new GunHandController.DirectionalAnimationBoolEightWay();
            RightHandController.isEightWay = false;
            RightHandController.PreFireDelay = 0.2f;
            RightHandController.NumShots = 1;
            RightHandController.ShotCooldown = 0.8f;
            RightHandController.Cooldown = 3;
            RightHandController.RampBullets = false;
            RightHandController.RampStartHeight = 2;
            RightHandController.RampTime = 1;
            
            SpeculativeRigidbody m_CachedRigidBody = m_CachedTargetObject.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateRigidBody(m_CachedRigidBody, m_CachedEnemyActor.specRigidbody);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "HotShot Shotgun Kin", HotShotShotgunKinGUID, (tk2dSprite)m_CachedEnemyActor.sprite, instantiateCorpseObject: false, ExternalCorpseObject: m_CachedEnemyActor.CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }
            
            if (m_CachedAIActor.aiAnimator) {
                ExpandUtility.DuplicateComponent(m_CachedAIActor.aiAnimator, m_CachedEnemyActor.aiAnimator);
            }
            if (m_CachedAIActor.spriteAnimator) {
                ExpandUtility.DuplicateComponent(m_CachedAIActor.spriteAnimator, m_CachedEnemyActor.spriteAnimator);
            }

            tk2dSpriteCollectionData HotShotShotGunCollectionData = m_CachedTargetObject.AddComponent<tk2dSpriteCollectionData>();

            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(m_CachedEnemyActor.sprite.Collection), HotShotShotGunCollectionData);

            Material m_NewHotShotGunManMaterial = new Material(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").sprite.Collection.materials[0]);
            m_NewHotShotGunManMaterial.mainTexture = expandSharedAssets1.LoadAsset<Texture2D>("HotShotShotgunMan");
            HotShotShotGunCollectionData.materials[0] = m_NewHotShotGunManMaterial;

            foreach (tk2dSpriteDefinition spriteDefinition in HotShotShotGunCollectionData.spriteDefinitions) { spriteDefinition.material = m_NewHotShotGunManMaterial; }
            m_CachedAIActor.sprite.Collection = HotShotShotGunCollectionData;

            ExpandUtility.DuplicateSpriteAnimation(m_CachedTargetObject, m_CachedTargetObject.AddComponent<tk2dSpriteAnimation>(), m_CachedEnemyActor.spriteAnimator.Library, HotShotShotGunCollectionData);
            m_CachedAIActor.spriteAnimator.Library = m_CachedTargetObject.GetComponent<tk2dSpriteAnimation>();
            m_CachedAIActor.optionalPalette = null;
            
            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.HasShadow = true;
            m_CachedAIActor.ActorShadowOffset = m_CachedEnemyActor.ActorShadowOffset;
            m_CachedAIActor.EnemySwitchState = m_CachedEnemyActor.EnemySwitchState;
            m_CachedAIActor.ShadowHeightOffGround = m_CachedEnemyActor.ShadowHeightOffGround;
            m_CachedAIActor.shadowHeightOffset = m_CachedEnemyActor.shadowHeightOffset;
            m_CachedAIActor.TransferShadowToCorpse = false;
            m_CachedAIActor.shadowDeathType = AIActor.ShadowDeathType.Fade;
            m_CachedAIActor.ShadowObject = null;

            m_CachedAIActor.PathableTiles = CellTypes.FLOOR;

            m_CachedAIActor.healthHaver.SetHealthMaximum(30);
            m_CachedAIActor.healthHaver.CursedMaximum = (30 * 3);
            m_CachedAIActor.healthHaver.spawnBulletScript = true;
            m_CachedAIActor.healthHaver.chanceToSpawnBulletScript = 0.3f;
            m_CachedAIActor.healthHaver.overrideDeathAnimBulletScript = "burst";
            m_CachedAIActor.healthHaver.noCorpseWhenBulletScriptDeath = true;
            m_CachedAIActor.healthHaver.bulletScriptType = HealthHaver.BulletScriptType.OnAnimEvent;
            m_CachedAIActor.healthHaver.bulletScript = new BulletScriptSelector() { scriptTypeName = "BulletShotgunManDeathBurst1" };

            AIBulletBank bulletBank = m_CachedAIActor.gameObject.AddComponent<AIBulletBank>();
            ExpandUtility.DuplicateComponent(bulletBank, m_CachedEnemyActor.gameObject.GetComponent<AIBulletBank>());

            ExpandHideGunHandsOnDeath hideGunHandsOnDeath = m_CachedTargetObject.AddComponent<ExpandHideGunHandsOnDeath>();
            hideGunHandsOnDeath.gunHands.Add(LeftHandController);
            hideGunHandsOnDeath.gunHands.Add(RightHandController);

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new RideInCartsBehavior(),
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 7,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.25f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };

            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new GunHandBasicShootBehavior() {
                    MaxUsages = 0,
                    RequiresLineOfSight = false,
                    resetCooldownOnDamage = null,
                    IsBlackPhantom = false,
                    targetAreaStyle = null,
                    AccumulateHealthThresholds = true,
                    HealthThresholds = new float[0],
                    MaxHealthThreshold = 1,
                    MinHealthThreshold = 0,
                    MaxEnemiesInRoom = 0,
                    MinWallDistance = 0,
                    Range = 0,
                    MinRange = 0,
                    GroupCooldown = 0,
                    GroupName = null,
                    InitialCooldownVariance = 0,
                    InitialCooldown = 0,
                    GlobalCooldown = 0,
                    AttackCooldown = 0,
                    CooldownVariance = 0,
                    Cooldown = 0,
                    GunHands = new List<GunHandController>() { LeftHandController, RightHandController },
                    FireAllGuns = false,
                    LineOfSight = true
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>() { LeftHandController, RightHandController };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, HotShotShotgunKinGUID, ExpandAmmonomiconDatabase.HotShotShotgunKin);

            m_CachedEnemyActor = null;
            return;
        }
        
        public static void BuildHotShotBulletManPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c"); // Bandana Bullet Kin
            AIActor m_CachedMimicActor = GetOfficialEnemyByGuid("2ebf8ef6728648089babb507dec4edb7"); // Brown Chest Mimic

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("HotShotBulletKin");
            
            GameObject LeftHand = m_CachedTargetObject.transform.Find("left hand").gameObject;
            GameObject RightHand = m_CachedTargetObject.transform.Find("right hand").gameObject;

            GunHandController LeftHandController = LeftHand.AddComponent<GunHandController>();
            GunHandController RightHandController = RightHand.AddComponent<GunHandController>();

            ExpandUtility.DuplicateRigidBody(m_CachedTargetObject.AddComponent<SpeculativeRigidbody>(), m_CachedEnemyActor.specRigidbody);

            LeftHandController.GunId = 38;
            LeftHandController.UsesOverrideProjectileData = true;
            LeftHandController.OverrideProjectile = m_CachedMimicActor.gameObject.transform.Find("left hand").GetComponent<GunHandController>().OverrideProjectile;
            LeftHandController.OverrideProjectileData = m_CachedMimicActor.gameObject.transform.Find("left hand").GetComponent<GunHandController>().OverrideProjectileData;
            LeftHandController.GunFlipMaster = RightHandController;
            LeftHandController.handObject = m_CachedEnemyActor.aiShooter.handObject;
            LeftHandController.gunBehindBody = new GunHandController.DirectionalAnimationBoolSixWay() {
                Back = true,
                BackRight = false,
                ForwardRight = false,
                Forward = false,
                ForwardLeft = true,
                BackLeft = true
            };
            LeftHandController.gunBehindBodyEight = new GunHandController.DirectionalAnimationBoolEightWay();
            LeftHandController.isEightWay = false;
            LeftHandController.PreFireDelay = 0;
            LeftHandController.NumShots = 1;
            LeftHandController.ShotCooldown = 0.4f;
            LeftHandController.Cooldown = 2f;
            LeftHandController.RampBullets = false;
            LeftHandController.RampStartHeight = 2;
            LeftHandController.RampTime = 1;

            RightHandController.GunId = 38;
            RightHandController.UsesOverrideProjectileData = true;
            RightHandController.OverrideProjectile = LeftHandController.OverrideProjectile;
            RightHandController.OverrideProjectileData = LeftHandController.OverrideProjectileData;
            RightHandController.handObject = m_CachedEnemyActor.aiShooter.handObject;
            RightHandController.gunBehindBody = new GunHandController.DirectionalAnimationBoolSixWay() {
                Back = true,
                BackRight = true,
                ForwardRight = true,
                Forward = false,
                ForwardLeft = false,
                BackLeft = false
            };
            RightHandController.gunBehindBodyEight = new GunHandController.DirectionalAnimationBoolEightWay();
            RightHandController.isEightWay = false;
            RightHandController.PreFireDelay = 0;
            RightHandController.NumShots = 1;
            RightHandController.ShotCooldown = 0.4f;
            RightHandController.Cooldown = 2f;
            RightHandController.RampBullets = false;
            RightHandController.RampStartHeight = 2;
            RightHandController.RampTime = 1;


            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "HotShot Bullet Kin", HotShotBulletKinGUID, (tk2dSprite)m_CachedEnemyActor.sprite, instantiateCorpseObject: false, ExternalCorpseObject: GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = CellTypes.FLOOR;
            
            
            if (m_CachedAIActor.aiAnimator) { ExpandUtility.DuplicateComponent(m_CachedAIActor.aiAnimator, m_CachedEnemyActor.aiAnimator); }

            if (m_CachedAIActor.spriteAnimator) { ExpandUtility.DuplicateComponent(m_CachedAIActor.spriteAnimator, m_CachedEnemyActor.spriteAnimator); }

            tk2dSpriteCollectionData HotShotCollectionData = m_CachedTargetObject.AddComponent<tk2dSpriteCollectionData>();

            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(m_CachedEnemyActor.sprite.Collection), HotShotCollectionData);

            Material m_NewHotShotMaterial = new Material(m_CachedEnemyActor.sprite.Collection.materials[0]);
            m_NewHotShotMaterial.mainTexture = expandSharedAssets1.LoadAsset<Texture2D>("HotShotBulletMan_Collection");
            HotShotCollectionData.materials[0] = m_NewHotShotMaterial;

            foreach (tk2dSpriteDefinition spriteDefinition in HotShotCollectionData.spriteDefinitions) { spriteDefinition.material = m_NewHotShotMaterial; }
            m_CachedAIActor.sprite.Collection = HotShotCollectionData;

            ExpandUtility.DuplicateSpriteAnimation(m_CachedTargetObject, m_CachedTargetObject.AddComponent<tk2dSpriteAnimation>(), m_CachedEnemyActor.spriteAnimator.Library, HotShotCollectionData);
            m_CachedAIActor.spriteAnimator.Library = m_CachedTargetObject.GetComponent<tk2dSpriteAnimation>();
            

            ExpandHideGunHandsOnDeath hideGunHandsOnDeath = m_CachedTargetObject.AddComponent<ExpandHideGunHandsOnDeath>();
            hideGunHandsOnDeath.gunHands.Add(LeftHandController);
            hideGunHandsOnDeath.gunHands.Add(RightHandController);

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>() {
                new RedBarrelAwareness() { AvoidRedBarrels = true, ShootRedBarrels = true, PushRedBarrels = true }
            };
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new RideInCartsBehavior(),
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 7,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.25f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };

            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new GunHandBasicShootBehavior() {
                    MaxUsages = 0,
                    RequiresLineOfSight = false,
                    resetCooldownOnDamage = null,
                    IsBlackPhantom = false,
                    targetAreaStyle = null,
                    AccumulateHealthThresholds = true,
                    HealthThresholds = new float[0],
                    MaxHealthThreshold = 1,
                    MinHealthThreshold = 0,
                    MaxEnemiesInRoom = 0,
                    MinWallDistance = 0,
                    Range = 0,
                    MinRange = 0,
                    GroupCooldown = 0,
                    GroupName = null,
                    InitialCooldownVariance = 0,
                    InitialCooldown = 0,
                    GlobalCooldown = 0,
                    AttackCooldown = 0,
                    CooldownVariance = 0,
                    Cooldown = 0,
                    GunHands = new List<GunHandController>() { LeftHandController, RightHandController },
                    FireAllGuns = false,
                    LineOfSight = true
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>() { LeftHandController, RightHandController };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, HotShotBulletKinGUID, ExpandAmmonomiconDatabase.HotShotBulletKin);

            m_CachedEnemyActor = null;
            return;
        }
        
        public static void BuildRatGrenadePrefab(out GameObject m_CachedTargetObject, bool isFakePrefab = true) {
            m_CachedTargetObject = UnityEngine.Object.Instantiate(GetOfficialEnemyByGuid("14ea47ff46b54bb4a98f91ffcffb656d").gameObject);
            m_CachedTargetObject.SetActive(false);
            m_CachedTargetObject.name = "Grenade Rat";
            
            ExplodeOnDeath RatExplodeComponent = m_CachedTargetObject.AddComponent<ExplodeOnDeath>();
            RatExplodeComponent.explosionData = ExpandUtility.GenerateExplosionData();
            RatExplodeComponent.immuneToIBombApp = false;
            RatExplodeComponent.LinearChainExplosion = false;
            RatExplodeComponent.deathType = OnDeathBehavior.DeathType.Death;
            RatExplodeComponent.preDeathDelay = 0.1f;

            AIActor m_CachedAIActor = RatGrenadePrefab.GetComponent<AIActor>();
            m_CachedAIActor.ActorName = "Grenade Rat";
            m_CachedAIActor.OverrideDisplayName = "Grenade Rat";
            m_CachedAIActor.EnemyGuid = RatGrenadeGUID;
            m_CachedAIActor.EnemyId = UnityEngine.Random.Range(10000, 100000);
            m_CachedAIActor.CorpseObject = null;
            UnityEngine.Object.Destroy(m_CachedAIActor.gameObject.GetComponent<EncounterTrackable>());
            
            tk2dSprite m_Sprite = m_CachedTargetObject.GetComponent<tk2dSprite>();
            tk2dSpriteAnimator m_SpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            m_Sprite.Collection = ExpandPrefabs.MouseTrap1.GetComponent<tk2dSprite>().Collection;
            m_SpriteAnimator.Library = ExpandPrefabs.MouseTrap1.GetComponent<tk2dSpriteAnimator>().Library;
            m_SpriteAnimator.DefaultClipId = 19;
            m_Sprite.SetSprite("rat_grenade_move_right_001");

            AIAnimator m_AIAnimator = m_CachedTargetObject.GetComponent<AIAnimator>();
            m_AIAnimator.IdleAnimation.Prefix = "rat_grenade_idle";
            m_AIAnimator.MoveAnimation.Prefix = "rat_grenade_move";
            m_AIAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>(0);
                        
            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = m_CachedTargetObject.GetComponent<BehaviorSpeculator>();
            if (m_TargetBehaviorSpeculatorSerialized != null) {
                m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
                m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
                m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0); // Only the StateKeys has to be predefined!
            }
            
            AddEnemyToDatabase(m_CachedTargetObject, RatGrenadeGUID, true);
            
            if (isFakePrefab) { FakePrefab.MarkAsFakePrefab(m_CachedTargetObject); }
            UnityEngine.Object.DontDestroyOnLoad(m_CachedTargetObject);
        }

        public static void BuildBabyGoodHammerPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5");

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("EXBabyGoodHammer");
                        
            List<string> IdleSpriteList = new List<string>() {
                "babygoodhammer_idle_down_01",
                "babygoodhammer_idle_down_02",
                "babygoodhammer_idle_down_03",
                "babygoodhammer_idle_down_04",
                "babygoodhammer_idle_down_05",
                "babygoodhammer_idle_down_06",
            };

            List<string> MoveLeftSpriteList = new List<string>() {
                "babygoodhammer_move_left_02",
                "babygoodhammer_move_left_02"
            };

            List<string> MoveRightSpriteList = new List<string>() {
                "babygoodhammer_move_right_02",
                "babygoodhammer_move_right_02"
            };
            

            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, BabyGoodHammerCollection, "babygoodhammer_idle_down_01");
                        
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, false, false, false, true, ClipFps: 6);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BabyGoodHammerCollection.GetComponent<tk2dSpriteCollectionData>(), IdleSpriteList, "Hammer_Idle_Down", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BabyGoodHammerCollection.GetComponent<tk2dSpriteCollectionData>(), MoveLeftSpriteList, "Hammer_Move_Left", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BabyGoodHammerCollection.GetComponent<tk2dSpriteCollectionData>(), MoveRightSpriteList, "Hammer_Move_Right", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            
            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "Baby Good Hammer", HammerCompanionGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: m_CachedEnemyActor.CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }
            
            m_CachedAIActor.MovementSpeed = 5;
            // m_CachedAIActor.PreventFallingInPitsEver = true;
            m_CachedAIActor.SetIsFlying(true, "Flying Enemy", true, true);
            m_CachedAIActor.IgnoreForRoomClear = true;


            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 1,
                    ManualOffsetY = 2,
                    ManualWidth = 19,
                    ManualHeight = 11,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 8,
                    ManualOffsetY = 1,
                    ManualWidth = 12,
                    ManualHeight = 16,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Movement;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "idle",
                    // AnimNames = new string[] { "idle_backward", "idle" },
                    AnimNames = new string[] { "Hammer_Idle_Down", "Hammer_Idle_Down", "Hammer_Idle_Down", "Hammer_Idle_Down" },
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "run",
                    AnimNames = new string[] { "Hammer_Idle_Down", "Hammer_Move_Right", "Hammer_Move_Left", "Hammer_Idle_Down" },
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                /*m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                            Prefix = "death",
                            AnimNames = new string[] { "Hammer_Idle_Down", "Hammer_Idle_Down" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    }
                };*/
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };

            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new CompanionFollowPlayerBehavior() {
                    PathInterval = 0.25f,
                    DisableInCombat = false,
                    IdealRadius = 5,
                    CatchUpRadius = 9.5f,
                    CatchUpAccelTime = 3,
                    CatchUpSpeed = 6,
                    CatchUpMaxSpeed = 10,
                    CatchUpAnimation = string.Empty,
                    CatchUpOutAnimation = string.Empty,
                    IdleAnimations = new string[0],
                    CanRollOverPits = false,
                    RollAnimation = string.Empty
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);

            ExpandUtility.MakeCompanion(m_CachedAIActor, null, null, true, false, true, true);

            AddEnemyToDatabase(m_CachedTargetObject, HammerCompanionGUID, false);
            BabyGoodHammer.CompanionGuid = HammerCompanionGUID;

            m_CachedEnemyActor = null;
            return;
        }

        public static void BuildBootlegBullatPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Bootleg Bullat");
            
            List<string> SpriteList = new List<string>() {
                "bullat_idle_001",
                "bullat_idle_002",
                "bullat_idle_003",
                "bullat_idle_004",
                "bullat_idle_005",
                "bullat_idle_006",
                "bullat_die_001",
                "bullat_die_002",
                "bullat_die_003",
                "bullat_die_004"
            };

            List<string> IdleSpriteList = new List<string>() {
                "bullat_idle_001",
                "bullat_idle_002",
                "bullat_idle_003",
                "bullat_idle_004",
                "bullat_idle_005",
                "bullat_idle_006",
            };
            
            List<string> DieSpriteList = new List<string>() {
                "bullat_die_001",
                "bullat_die_002",
                "bullat_die_003",
                "bullat_die_004"
            };
            
            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, BootlegBullatCollection, "bullat_idle_001");

            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBullatCollection.GetComponent<tk2dSpriteCollectionData>(), IdleSpriteList, "idle", tk2dSpriteAnimationClip.WrapMode.Loop, 10);                                    
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBullatCollection.GetComponent<tk2dSpriteCollectionData>(), DieSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            
            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, BootlegBullatGUID, null, instantiateCorpseObject: false, EnemyHasNoShooter: true, EnemyHasNoCorpse: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();
            
            m_CachedAIActor.MovementSpeed = 4;
            m_CachedAIActor.SetIsFlying(true, "Flying Enemy", true, true);
            m_CachedAIActor.procedurallyOutlined = false;
            m_CachedAIActor.EnemySwitchState = "Bullet_Bat";
            m_CachedAIActor.healthHaver.SetHealthMaximum(1);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(1);
            m_CachedAIActor.DiesOnCollison = true;

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 8,
                    ManualOffsetY = 3,
                    ManualWidth = 13,
                    ManualHeight = 8,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 5,
                    ManualOffsetY = 2,
                    ManualWidth = 19,
                    ManualHeight = 10,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Default;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "idle",
                    AnimNames = new string[] { string.Empty },
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.None,
                    Prefix = string.Empty,
                    AnimNames = new string[0],
                    Flipped = new DirectionalAnimation.FlipType[0]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.None,
                    Prefix = string.Empty,
                    AnimNames = new string[0],
                    Flipped = new DirectionalAnimation.FlipType[0]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[] { string.Empty },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() { Radius = 35, LineOfSight = false,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new SeekTargetBehavior() {
                    StopWhenInRange = false,
                    CustomRange = 6,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);

            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, BootlegBullatGUID, ExpandAmmonomiconDatabase.BootlegBullat);
            return;
        }

        public static void BuildBootlegBulletManPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5"); //bullet_kin
            
            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Bootleg BulletMan");

            
            List<string> SpriteList = new List<string>() {
                "bulletman_cover_left_idle_001",
                "bulletman_cover_left_leap_001",
                "bulletman_cover_right_idle_001",
                "bulletman_cover_right_leap_001",
                "bulletman_hit_left_001",
                "bulletman_hit_left_002",
                "bulletman_hit_left_003",
                "bulletman_hit_left_004",
                "bulletman_hit_left_005",
                "bulletman_hit_right_001",
                "bulletman_hit_right_002",
                "bulletman_hit_right_003",
                "bulletman_hit_right_004",
                "bulletman_hit_right_005",
                "bulletman_idle_down_001",
                "bulletman_idle_left_001",
                "bulletman_idle_right_001",
                "bulletman_idle_up_001",
                "bulletman_pitfall_001",
                "bulletman_pitfall_002",
                "bulletman_pitfall_003",
                "bulletman_pitfall_004",
                "bulletman_pitfall_005",
                "bulletman_run_down_001",
                "bulletman_run_down_002",
                "bulletman_run_down_003",
                "bulletman_run_down_004",
                "bulletman_run_down_005",
                "bulletman_run_left_001",
                "bulletman_run_left_002",
                "bulletman_run_left_003",
                "bulletman_run_left_004",
                "bulletman_run_left_005",
                "bulletman_run_right_001",
                "bulletman_run_right_002",
                "bulletman_run_right_003",
                "bulletman_run_right_004",
                "bulletman_run_right_005",
                "bulletman_run_up_001",
                "bulletman_run_up_002",
                "bulletman_run_up_003",
                "bulletman_run_up_004",
                "bulletman_run_up_005",
                "bulletman_spawn_001",
                "bulletman_spawn_002",
                "bulletman_spawn_003",
                "bulletman_corpse"
            };

            List<string> IdleDownSpriteList = new List<string>() {
                "bulletman_idle_down_001",
                "bulletman_idle_down_001"
            };
            List<string> IdleUpSpriteList = new List<string>() {
                "bulletman_idle_up_001",
                "bulletman_idle_up_001"
            };
            List<string> IdleLeftSpriteList = new List<string>() {
                "bulletman_idle_left_001",
                "bulletman_idle_left_001"
            };
            List<string> IdleRightSpriteList = new List<string>() {
                "bulletman_idle_right_001",
                "bulletman_idle_right_001"
            };
            
            List<string> MoveLeftSpriteList = new List<string>() {
                "bulletman_run_left_001",
                "bulletman_run_left_002",
                "bulletman_run_left_003",
                "bulletman_run_left_004",
                "bulletman_run_left_005"
            };
            List<string> MoveRightSpriteList = new List<string>() {
                "bulletman_run_right_001",
                "bulletman_run_right_002",
                "bulletman_run_right_003",
                "bulletman_run_right_004",
                "bulletman_run_right_005",
            };
            List<string> MoveDownSpriteList = new List<string>() {
                "bulletman_run_down_001",
                "bulletman_run_down_002",
                "bulletman_run_down_003",
                "bulletman_run_down_004",
                "bulletman_run_down_005"
            };
            List<string> MoveUpSpriteList = new List<string>() {
                "bulletman_run_up_001",
                "bulletman_run_up_002",
                "bulletman_run_up_003",
                "bulletman_run_up_004",
                "bulletman_run_up_005",
            };

            List<string> HitLeftSpriteList = new List<string>() {
                "bulletman_hit_left_001",
                "bulletman_hit_left_002",
                "bulletman_hit_left_003",
                "bulletman_hit_left_004",
                "bulletman_hit_left_005"
            };
            List<string> HitRightSpriteList = new List<string>() {
                "bulletman_hit_right_001",
                "bulletman_hit_right_002",
                "bulletman_hit_right_003",
                "bulletman_hit_right_004",
                "bulletman_hit_right_005"
            };

            List<string> CoverIdleLeftSpriteList = new List<string>() {
                "bulletman_cover_left_idle_001",
                "bulletman_cover_left_idle_001"
            };
            List<string> CoverIdleRightSpriteList = new List<string>() {
                "bulletman_cover_right_idle_001",
                "bulletman_cover_right_idle_001"
            };
            List<string> CoverLeepLeftSpriteList = new List<string>() {
                "bulletman_cover_left_leap_001",
                "bulletman_cover_left_leap_001"
            };
            List<string> CoverLeepRightSpriteList = new List<string>() {
                "bulletman_cover_right_leap_001",
                "bulletman_cover_right_leap_001"
            };

            List<string> SpawnSpriteList = new List<string>() {
                "bulletman_spawn_001",
                "bulletman_spawn_002",
                "bulletman_spawn_003"
            };

            List<string> PitfallSpriteList = new List<string>() {
                "bulletman_pitfall_001",
                "bulletman_pitfall_002",
                "bulletman_pitfall_003",
                "bulletman_pitfall_004",
                "bulletman_pitfall_005",
            };

            List<string> DeathSpriteList = new List<string>() { "bulletman_corpse", "bulletman_corpse" };
                        
            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, BootlegBulletManCollection, IdleDownSpriteList[0]);
            
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), IdleLeftSpriteList, "idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), IdleRightSpriteList, "idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), IdleDownSpriteList, "idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), IdleUpSpriteList, "idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), MoveLeftSpriteList, "run_west", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), MoveRightSpriteList, "run_east", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), MoveDownSpriteList, "run_south", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), MoveUpSpriteList, "run_north", tk2dSpriteAnimationClip.WrapMode.Loop, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), HitLeftSpriteList, "hit_west", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), HitRightSpriteList, "hit_east", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), HitLeftSpriteList, "hit_north", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), HitRightSpriteList, "hit_south", tk2dSpriteAnimationClip.WrapMode.Once, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), CoverIdleLeftSpriteList, "cover_idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), CoverIdleRightSpriteList, "cover_idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), CoverIdleLeftSpriteList, "cover_idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), CoverIdleRightSpriteList, "cover_idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), CoverLeepLeftSpriteList, "cover_leep_west", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), CoverLeepRightSpriteList, "cover_leep_east", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), CoverLeepLeftSpriteList, "cover_leep_north", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), CoverLeepRightSpriteList, "cover_leep_south", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), IdleDownSpriteList, "awaken", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), PitfallSpriteList, "pitfall_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "death", tk2dSpriteAnimationClip.WrapMode.Once, 8);


            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;

            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, m_CachedEnemyActor.aiShooter, m_CachedEnemyActor.GetComponent<AIBulletBank>(), BootlegGuns.BootlegPistolID, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "Bootleg Bullet Kin", BootlegBulletManGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.aiShooter.handObject = null;
            m_CachedAIActor.procedurallyOutlined = false;

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 1,
                    ManualOffsetY = 1,
                    ManualWidth = 10,
                    ManualHeight = 6,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 1,
                    ManualOffsetY = 1,
                    ManualWidth = 10,
                    ManualHeight = 13,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Default;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "idle",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "run",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "hit",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "cover_idle",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                            Prefix = "cover_idle",
                            AnimNames = new string[4],
                            Flipped = new DirectionalAnimation.FlipType[4]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "cover_leep",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                            Prefix = "cover_leep",
                            AnimNames = new string[4],
                            Flipped = new DirectionalAnimation.FlipType[4]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall_right" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[] { "die" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "death",
                            AnimNames = new string[] { "death" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>() {
                new RedBarrelAwareness() { AvoidRedBarrels = true, ShootRedBarrels = true, PushRedBarrels = true }
            };
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new RideInCartsBehavior(),
                new TakeCoverBehavior() {
                    PathInterval = 0.25f,
                    LineOfSightToLeaveCover = true,
                    MaxCoverDistance = 10,
                    MaxCoverDistanceToTarget = 25,
                    FlipCoverDistance = 0.3f,
                    InsideCoverTime = 2,
                    OutsideCoverTime = 2,
                    PopOutSpeedMultiplier = 3,
                    PopInSpeedMultiplier = 1,
                    InitialCoverChance = 0.5f,
                    RepeatingCoverChance = 0.15f,
                    RepeatingCoverInterval = 1
                },
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 7,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.25f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0.2f,
                    LineOfSight = true,
                    WeaponType = WeaponType.AIShooterProjectile,
                    OverrideBulletName = "default",
                    BulletScript = null,
                    FixTargetDuringAttack = false,
                    StopDuringAttack = false,
                    LeadAmount = 0,
                    LeadChance = 1,
                    RespectReload = true,
                    MagazineCapacity = 6,
                    ReloadSpeed = 2,
                    EmptiesClip = false,
                    SuppressReloadAnim = false,                    
                    TimeBetweenShots = -1,
                    PreventTargetSwitching = false,
                    OverrideAnimation = null,
                    OverrideDirectionalAnimation = null,
                    HideGun = false,
                    UseLaserSight = false,
                    UseGreenLaser = false,
                    PreFireLaserTime = -1,
                    AimAtFacingDirectionWhenSafe = false,
                    Cooldown = 1.6f,
                    CooldownVariance = 0,
                    AttackCooldown = 0,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 12,
                    MinWallDistance = 0,
                    MaxEnemiesInRoom = 0,
                    MinHealthThreshold = 0,
                    MaxHealthThreshold = 1,
                    HealthThresholds = new float[0],
                    AccumulateHealthThresholds = true,
                    targetAreaStyle = null,
                    IsBlackPhantom = false,
                    resetCooldownOnDamage = null,
                    RequiresLineOfSight = false,
                    MaxUsages = 0
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);

            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, BootlegBulletManGUID, ExpandAmmonomiconDatabase.BootlegBulletKin);

            m_CachedEnemyActor = null;
            return;
        }

        public static void BuildBootlegBulletManBandanaPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Bootleg BulletMan Bandana");

            
            List<string> SpriteList = new List<string>() {
                "bulletmanbandana_hit_left_001",
                "bulletmanbandana_hit_left_002",
                "bulletmanbandana_hit_left_003",
                "bulletmanbandana_hit_left_004",
                "bulletmanbandana_hit_left_005",
                "bulletmanbandana_hit_right_001",
                "bulletmanbandana_hit_right_002",
                "bulletmanbandana_hit_right_003",
                "bulletmanbandana_hit_right_004",
                "bulletmanbandana_hit_right_005",
                "bulletmanbandana_idle_down_001",
                "bulletmanbandana_idle_left_001",
                "bulletmanbandana_idle_right_001",
                "bulletmanbandana_idle_up_001",
                "bulletmanbandana_pitfall_001",
                "bulletmanbandana_pitfall_002",
                "bulletmanbandana_pitfall_003",
                "bulletmanbandana_pitfall_004",
                "bulletmanbandana_pitfall_005",
                "bulletmanbandana_run_down_001",
                "bulletmanbandana_run_down_002",
                "bulletmanbandana_run_down_003",
                "bulletmanbandana_run_down_004",
                "bulletmanbandana_run_down_005",
                "bulletmanbandana_run_left_001",
                "bulletmanbandana_run_left_002",
                "bulletmanbandana_run_left_003",
                "bulletmanbandana_run_left_004",
                "bulletmanbandana_run_left_005",
                "bulletmanbandana_run_right_001",
                "bulletmanbandana_run_right_002",
                "bulletmanbandana_run_right_003",
                "bulletmanbandana_run_right_004",
                "bulletmanbandana_run_right_005",
                "bulletmanbandana_run_up_001",
                "bulletmanbandana_run_up_002",
                "bulletmanbandana_run_up_003",
                "bulletmanbandana_run_up_004",
                "bulletmanbandana_run_up_005",
                "bulletmanbandana_spawn_001",
                "bulletmanbandana_spawn_002",
                "bulletmanbandana_spawn_003",
                "bulletmanbandana_corpse"
            };

            List<string> IdleDownSpriteList = new List<string>() {
                "bulletmanbandana_idle_down_001",
                "bulletmanbandana_idle_down_001"
            };
            List<string> IdleUpSpriteList = new List<string>() {
                "bulletmanbandana_idle_up_001",
                "bulletmanbandana_idle_up_001"
            };
            List<string> IdleLeftSpriteList = new List<string>() {
                "bulletmanbandana_idle_left_001",
                "bulletmanbandana_idle_left_001"
            };
            List<string> IdleRightSpriteList = new List<string>() {
                "bulletmanbandana_idle_right_001",
                "bulletmanbandana_idle_right_001"
            };
            
            List<string> MoveLeftSpriteList = new List<string>() {
                "bulletmanbandana_run_left_001",
                "bulletmanbandana_run_left_002",
                "bulletmanbandana_run_left_003",
                "bulletmanbandana_run_left_004",
                "bulletmanbandana_run_left_005"
            };
            List<string> MoveRightSpriteList = new List<string>() {
                "bulletmanbandana_run_right_001",
                "bulletmanbandana_run_right_002",
                "bulletmanbandana_run_right_003",
                "bulletmanbandana_run_right_004",
                "bulletmanbandana_run_right_005",
            };
            List<string> MoveDownSpriteList = new List<string>() {
                "bulletmanbandana_run_down_001",
                "bulletmanbandana_run_down_002",
                "bulletmanbandana_run_down_003",
                "bulletmanbandana_run_down_004",
                "bulletmanbandana_run_down_005"
            };
            List<string> MoveUpSpriteList = new List<string>() {
                "bulletmanbandana_run_up_001",
                "bulletmanbandana_run_up_002",
                "bulletmanbandana_run_up_003",
                "bulletmanbandana_run_up_004",
                "bulletmanbandana_run_up_005",
            };

            List<string> HitLeftSpriteList = new List<string>() {
                "bulletmanbandana_hit_left_001",
                "bulletmanbandana_hit_left_002",
                "bulletmanbandana_hit_left_003",
                "bulletmanbandana_hit_left_004",
                "bulletmanbandana_hit_left_005"
            };
            List<string> HitRightSpriteList = new List<string>() {
                "bulletmanbandana_hit_right_001",
                "bulletmanbandana_hit_right_002",
                "bulletmanbandana_hit_right_003",
                "bulletmanbandana_hit_right_004",
                "bulletmanbandana_hit_right_005"
            };

            List<string> CoverIdleLeftSpriteList = new List<string>() {
                "bulletmanbandana_idle_left_001",
                "bulletmanbandana_idle_left_001"
            };
            List<string> CoverIdleRightSpriteList = new List<string>() {
                "bulletmanbandana_idle_right_001",
                "bulletmanbandana_idle_right_001"
            };
            List<string> CoverLeepLeftSpriteList = new List<string>() {
                "bulletmanbandana_idle_left_001",
                "bulletmanbandana_idle_left_001"
            };
            List<string> CoverLeepRightSpriteList = new List<string>() {
                "bulletmanbandana_idle_right_001",
                "bulletmanbandana_idle_right_001"
            };

            List<string> SpawnSpriteList = new List<string>() {
                "bulletmanbandana_spawn_001",
                "bulletmanbandana_spawn_002",
                "bulletmanbandana_spawn_003"
            };

            List<string> PitfallSpriteList = new List<string>() {
                "bulletmanbandana_pitfall_001",
                "bulletmanbandana_pitfall_002",
                "bulletmanbandana_pitfall_003",
                "bulletmanbandana_pitfall_004",
                "bulletmanbandana_pitfall_005",
            };

            List<string> DeathSpriteList = new List<string>() { "bulletmanbandana_corpse", "bulletmanbandana_corpse" };
                        
            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, BootlegBulletManBandanaCollection, IdleDownSpriteList[0]);
            
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), IdleLeftSpriteList, "idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), IdleRightSpriteList, "idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), IdleDownSpriteList, "idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), IdleUpSpriteList, "idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), MoveLeftSpriteList, "run_west", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), MoveRightSpriteList, "run_east", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), MoveDownSpriteList, "run_south", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), MoveUpSpriteList, "run_north", tk2dSpriteAnimationClip.WrapMode.Loop, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), HitLeftSpriteList, "hit_west", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), HitRightSpriteList, "hit_east", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), HitLeftSpriteList, "hit_north", tk2dSpriteAnimationClip.WrapMode.Once, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), HitRightSpriteList, "hit_south", tk2dSpriteAnimationClip.WrapMode.Once, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), CoverIdleLeftSpriteList, "cover_idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), CoverIdleRightSpriteList, "cover_idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), CoverIdleLeftSpriteList, "cover_idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), CoverIdleRightSpriteList, "cover_idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), CoverLeepLeftSpriteList, "cover_leep_west", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), CoverLeepRightSpriteList, "cover_leep_east", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), CoverLeepLeftSpriteList, "cover_leep_north", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), CoverLeepRightSpriteList, "cover_leep_south", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), IdleDownSpriteList, "awaken", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), PitfallSpriteList, "pitfall_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegBulletManBandanaCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "death", tk2dSpriteAnimationClip.WrapMode.Once, 8);

            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;
                        
            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, m_CachedEnemyActor.aiShooter, m_CachedEnemyActor.GetComponent<AIBulletBank>(), BootlegGuns.BootlegMachinePistolID, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "Bootleg Bandana Bullet Kin", BootlegBulletManBandanaGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.aiShooter.handObject = null;
            m_CachedAIActor.procedurallyOutlined = false;

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 2,
                    ManualOffsetY = 1,
                    ManualWidth = 10,
                    ManualHeight = 6,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 2,
                    ManualOffsetY = 1,
                    ManualWidth = 10,
                    ManualHeight = 13,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Default;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "idle",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "run",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "hit",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "cover_idle",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                            Prefix = "cover_idle",
                            AnimNames = new string[4],
                            Flipped = new DirectionalAnimation.FlipType[4]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "cover_leep",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                            Prefix = "cover_leep",
                            AnimNames = new string[4],
                            Flipped = new DirectionalAnimation.FlipType[4]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall_right" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[] { "die" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "death",
                            AnimNames = new string[] { "death" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 6,
                    LineOfSight = false,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0.2f,
                    LineOfSight = false,
                    WeaponType = WeaponType.AIShooterProjectile,
                    OverrideBulletName = null,
                    BulletScript = null,
                    FixTargetDuringAttack = false,
                    StopDuringAttack = false,
                    LeadAmount = 0,
                    LeadChance = 1,
                    RespectReload = true,
                    MagazineCapacity = 8,
                    ReloadSpeed = 2,
                    EmptiesClip = true,
                    SuppressReloadAnim = false,                    
                    TimeBetweenShots = -1,
                    PreventTargetSwitching = false,
                    OverrideAnimation = null,
                    OverrideDirectionalAnimation = null,
                    HideGun = false,
                    UseLaserSight = false,
                    UseGreenLaser = false,
                    PreFireLaserTime = -1,
                    AimAtFacingDirectionWhenSafe = false,
                    Cooldown = 0.2f,
                    CooldownVariance = 0,
                    AttackCooldown = 0,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 16,
                    MinWallDistance = 0,
                    MaxEnemiesInRoom = 0,
                    MinHealthThreshold = 0,
                    MaxHealthThreshold = 1,
                    HealthThresholds = new float[0],
                    AccumulateHealthThresholds = true,
                    targetAreaStyle = null,
                    IsBlackPhantom = false,
                    resetCooldownOnDamage = null,
                    RequiresLineOfSight = false,
                    MaxUsages = 0
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, BootlegBulletManBandanaGUID, ExpandAmmonomiconDatabase.BootlegBandanaBulletKin);

            m_CachedEnemyActor = null;
            return;
        }

        public static void BuildBootlegShotgunManRedPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Bootleg ShotgunMan Red");
                        
            List<string> SpriteList = new List<string>() {
                "shotgunman_red_up",
                "shotgunman_red_down",
                "shotgunman_red_left",
                "shotgunman_red_right",
                "shotgunman_red_move_down_001",
                "shotgunman_red_move_down_002",
                "shotgunman_red_move_down_003",
                "shotgunman_red_move_down_004",
                "shotgunman_red_move_down_005",
                "shotgunman_red_move_left_001",
                "shotgunman_red_move_left_002",
                "shotgunman_red_move_left_003",
                "shotgunman_red_move_left_004",
                "shotgunman_red_move_left_005",
                "shotgunman_red_move_right_001",
                "shotgunman_red_move_right_002",
                "shotgunman_red_move_right_003",
                "shotgunman_red_move_right_004",
                "shotgunman_red_move_right_005",
                "shotgunman_red_move_up_001",
                "shotgunman_red_move_up_002",
                "shotgunman_red_move_up_003",
                "shotgunman_red_move_up_004",
                "shotgunman_red_move_up_005",
                "shotgunman_red_pitfall_001",
                "shotgunman_red_pitfall_002",
                "shotgunman_red_pitfall_003",
                "shotgunman_red_pitfall_004",
                "shotgunman_red_pitfall_005",
                "shotgunman_red_spawn_001",
                "shotgunman_red_spawn_002",
                "shotgunman_red_spawn_003",
                "shotgunman_red_hit_left",
                "shotgunman_red_hit_right",
                "shotgunman_red_corpse"
            };

            List<string> IdleDownSpriteList = new List<string>() {
                "shotgunman_red_down",
                "shotgunman_red_down"
            };
            List<string> IdleUpSpriteList = new List<string>() {
                "shotgunman_red_up",
                "shotgunman_red_up"
            };
            List<string> IdleLeftSpriteList = new List<string>() {
                "shotgunman_red_left",
                "shotgunman_red_left"
            };
            List<string> IdleRightSpriteList = new List<string>() {
                "shotgunman_red_right",
                "shotgunman_red_right"
            };
            
            List<string> MoveLeftSpriteList = new List<string>() {
                "shotgunman_red_move_left_001",
                "shotgunman_red_move_left_002",
                "shotgunman_red_move_left_003",
                "shotgunman_red_move_left_004",
                "shotgunman_red_move_left_005"
            };
            List<string> MoveRightSpriteList = new List<string>() {
                "shotgunman_red_move_right_001",
                "shotgunman_red_move_right_002",
                "shotgunman_red_move_right_003",
                "shotgunman_red_move_right_004",
                "shotgunman_red_move_right_005",
            };
            List<string> MoveDownSpriteList = new List<string>() {
                "shotgunman_red_move_down_001",
                "shotgunman_red_move_down_002",
                "shotgunman_red_move_down_003",
                "shotgunman_red_move_down_004",
                "shotgunman_red_move_down_005"
            };
            List<string> MoveUpSpriteList = new List<string>() {
                "shotgunman_red_move_up_001",
                "shotgunman_red_move_up_002",
                "shotgunman_red_move_up_003",
                "shotgunman_red_move_up_004",
                "shotgunman_red_move_up_005",
            };
            List<string> HitLeftSpriteList = new List<string>() { "shotgunman_red_hit_left", "shotgunman_red_hit_left" };
            List<string> HitRightSpriteList = new List<string>() { "shotgunman_red_hit_right", "shotgunman_red_hit_right" };                        
            List<string> SpawnSpriteList = new List<string>() {
                "shotgunman_red_spawn_001",
                "shotgunman_red_spawn_002",
                "shotgunman_red_spawn_003"
            };
            List<string> PitfallSpriteList = new List<string>() {
                "shotgunman_red_pitfall_001",
                "shotgunman_red_pitfall_002",
                "shotgunman_red_pitfall_003",
                "shotgunman_red_pitfall_004",
                "shotgunman_red_pitfall_005",
            };
            
            List<string> DeathSpriteList = new List<string>() { "shotgunman_red_corpse", "shotgunman_red_corpse" };
            
            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, BootlegShotgunManRedCollection, IdleDownSpriteList[0]);

            
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), IdleLeftSpriteList, "idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), IdleRightSpriteList, "idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), IdleDownSpriteList, "idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), IdleUpSpriteList, "idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), MoveLeftSpriteList, "move_west", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), MoveRightSpriteList, "move_east", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), MoveDownSpriteList, "move_south", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), MoveUpSpriteList, "move_north", tk2dSpriteAnimationClip.WrapMode.Loop, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), HitLeftSpriteList, "hit_west", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), HitRightSpriteList, "hit_east", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), HitLeftSpriteList, "hit_north", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), HitRightSpriteList, "hit_south", tk2dSpriteAnimationClip.WrapMode.Once, 4);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), PitfallSpriteList, "pitfall_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "death", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManRedCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            
            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;
            
            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, m_CachedEnemyActor.aiShooter, m_CachedEnemyActor.GetComponent<AIBulletBank>(), BootlegGuns.BootlegShotgunID, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "Bootleg Red Shogun Kin", BootlegShotgunManRedGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.aiShooter.handObject = null;
            m_CachedAIActor.procedurallyOutlined = false;
            m_CachedAIActor.EnemySwitchState = "Plastic_Bullet_Man";
            m_CachedAIActor.healthHaver.SetHealthMaximum(30);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(30);
            m_CachedAIActor.healthHaver.damagedAudioEvent = "Play_CHR_plasticBullet_hurt_01";
            
            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 1,
                    ManualOffsetY = 1,
                    ManualWidth = 12,
                    ManualHeight = 12,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 1,
                    ManualOffsetY = 1,
                    ManualWidth = 12,
                    ManualHeight = 24,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Target;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = true;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "idle",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "move",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "hit",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall_right" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[] { "die" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "death",
                            AnimNames = new string[] { "death" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new RideInCartsBehavior(),
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 6,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0.2f,
                    LineOfSight = true,
                    WeaponType = WeaponType.BulletScript,
                    OverrideBulletName = null,
                    BulletScript = new BulletScriptSelector() { scriptTypeName = "BulletShotgunManRedBasicAttack1" },
                    FixTargetDuringAttack = false,
                    StopDuringAttack = true,
                    LeadAmount = 0,
                    LeadChance = 1,
                    RespectReload = true,
                    MagazineCapacity = 1,
                    ReloadSpeed = 3,
                    EmptiesClip = false,
                    SuppressReloadAnim = false,
                    TimeBetweenShots = -1,
                    PreventTargetSwitching = false,
                    OverrideAnimation = null,
                    OverrideDirectionalAnimation = null,
                    HideGun = false,
                    UseLaserSight = false,
                    UseGreenLaser = false,
                    PreFireLaserTime = -1,
                    AimAtFacingDirectionWhenSafe = false,
                    Cooldown = 3.5f,
                    CooldownVariance = 0,
                    AttackCooldown = 0,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 20,
                    MinWallDistance = 0,
                    MaxEnemiesInRoom = 0,
                    MinHealthThreshold = 0,
                    MaxHealthThreshold = 1,
                    HealthThresholds = new float[0],
                    AccumulateHealthThresholds = true,
                    targetAreaStyle = null,
                    IsBlackPhantom = false,
                    resetCooldownOnDamage = null,
                    RequiresLineOfSight = false,
                    MaxUsages = 0
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, BootlegShotgunManRedGUID, ExpandAmmonomiconDatabase.BootlegShotgunKinRed);

            m_CachedEnemyActor = null;
            return;
        }

        public static void BuildBootlegShotgunManBluePrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Bootleg ShotgunMan Blue");
            
            List<string> SpriteList = new List<string>() {
                "shotgunman_blue_up",
                "shotgunman_blue_down",
                "shotgunman_blue_left",
                "shotgunman_blue_right",
                "shotgunman_blue_move_down_001",
                "shotgunman_blue_move_down_002",
                "shotgunman_blue_move_down_003",
                "shotgunman_blue_move_down_004",
                "shotgunman_blue_move_down_005",
                "shotgunman_blue_move_left_001",
                "shotgunman_blue_move_left_002",
                "shotgunman_blue_move_left_003",
                "shotgunman_blue_move_left_004",
                "shotgunman_blue_move_left_005",
                "shotgunman_blue_move_right_001",
                "shotgunman_blue_move_right_002",
                "shotgunman_blue_move_right_003",
                "shotgunman_blue_move_right_004",
                "shotgunman_blue_move_right_005",
                "shotgunman_blue_move_up_001",
                "shotgunman_blue_move_up_002",
                "shotgunman_blue_move_up_003",
                "shotgunman_blue_move_up_004",
                "shotgunman_blue_move_up_005",
                "shotgunman_blue_pitfall_001",
                "shotgunman_blue_pitfall_002",
                "shotgunman_blue_pitfall_003",
                "shotgunman_blue_pitfall_004",
                "shotgunman_blue_pitfall_005",
                "shotgunman_blue_spawn_001",
                "shotgunman_blue_spawn_002",
                "shotgunman_blue_spawn_003",
                "shotgunman_blue_hit_left",
                "shotgunman_blue_hit_right",
                "shotgunman_blue_corpse"
            };

            List<string> IdleDownSpriteList = new List<string>() {
                "shotgunman_blue_down",
                "shotgunman_blue_down"
            };
            List<string> IdleUpSpriteList = new List<string>() {
                "shotgunman_blue_up",
                "shotgunman_blue_up"
            };
            List<string> IdleLeftSpriteList = new List<string>() {
                "shotgunman_blue_left",
                "shotgunman_blue_left"
            };
            List<string> IdleRightSpriteList = new List<string>() {
                "shotgunman_blue_right",
                "shotgunman_blue_right"
            };
            
            List<string> MoveLeftSpriteList = new List<string>() {
                "shotgunman_blue_move_left_001",
                "shotgunman_blue_move_left_002",
                "shotgunman_blue_move_left_003",
                "shotgunman_blue_move_left_004",
                "shotgunman_blue_move_left_005"
            };
            List<string> MoveRightSpriteList = new List<string>() {
                "shotgunman_blue_move_right_001",
                "shotgunman_blue_move_right_002",
                "shotgunman_blue_move_right_003",
                "shotgunman_blue_move_right_004",
                "shotgunman_blue_move_right_005",
            };
            List<string> MoveDownSpriteList = new List<string>() {
                "shotgunman_blue_move_down_001",
                "shotgunman_blue_move_down_002",
                "shotgunman_blue_move_down_003",
                "shotgunman_blue_move_down_004",
                "shotgunman_blue_move_down_005"
            };
            List<string> MoveUpSpriteList = new List<string>() {
                "shotgunman_blue_move_up_001",
                "shotgunman_blue_move_up_002",
                "shotgunman_blue_move_up_003",
                "shotgunman_blue_move_up_004",
                "shotgunman_blue_move_up_005",
            };
            List<string> HitLeftSpriteList = new List<string>() { "shotgunman_blue_hit_left", "shotgunman_blue_hit_left" };
            List<string> HitRightSpriteList = new List<string>() { "shotgunman_blue_hit_right", "shotgunman_blue_hit_right" };                        
            List<string> SpawnSpriteList = new List<string>() {
                "shotgunman_blue_spawn_001",
                "shotgunman_blue_spawn_002",
                "shotgunman_blue_spawn_003"
            };
            List<string> PitfallSpriteList = new List<string>() {
                "shotgunman_blue_pitfall_001",
                "shotgunman_blue_pitfall_002",
                "shotgunman_blue_pitfall_003",
                "shotgunman_blue_pitfall_004",
                "shotgunman_blue_pitfall_005",
            };
            
            List<string> DeathSpriteList = new List<string>() { "shotgunman_blue_corpse", "shotgunman_blue_corpse" };
                        
            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, BootlegShotgunManBlueCollection, IdleDownSpriteList[0]);
            
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), IdleLeftSpriteList, "idle_west", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), IdleRightSpriteList, "idle_east", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), IdleDownSpriteList, "idle_south", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), IdleUpSpriteList, "idle_north", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), MoveLeftSpriteList, "move_west", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), MoveRightSpriteList, "move_east", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), MoveDownSpriteList, "move_south", tk2dSpriteAnimationClip.WrapMode.Loop, 24);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), MoveUpSpriteList, "move_north", tk2dSpriteAnimationClip.WrapMode.Loop, 24);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), HitLeftSpriteList, "hit_west", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), HitRightSpriteList, "hit_east", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), HitLeftSpriteList, "hit_north", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), HitRightSpriteList, "hit_south", tk2dSpriteAnimationClip.WrapMode.Once, 4);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), PitfallSpriteList, "pitfall_right", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "death", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, BootlegShotgunManBlueCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            
            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;
            
            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, m_CachedEnemyActor.aiShooter, m_CachedEnemyActor.GetComponent<AIBulletBank>(), BootlegGuns.BootlegShotgunID, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "Bootleg Blue Shogun Kin", BootlegShotgunManBlueGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.aiShooter.handObject = null;
            m_CachedAIActor.procedurallyOutlined = false;
            m_CachedAIActor.EnemySwitchState = "Plastic_Bullet_Man";
            m_CachedAIActor.healthHaver.SetHealthMaximum(40);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(40);
            m_CachedAIActor.healthHaver.damagedAudioEvent = "Play_CHR_plasticBullet_hurt_01";
            
            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 1,
                    ManualOffsetY = 1,
                    ManualWidth = 12,
                    ManualHeight = 12,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 1,
                    ManualOffsetY = 1,
                    ManualWidth = 12,
                    ManualHeight = 24,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Target;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = true;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "idle",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "move",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                    Prefix = "hit",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall_right" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[] { "die" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "death",
                            AnimNames = new string[] { "death" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new RideInCartsBehavior(),
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 6,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0.2f,
                    LineOfSight = true,
                    WeaponType = WeaponType.BulletScript,
                    OverrideBulletName = null,
                    BulletScript = new BulletScriptSelector() { scriptTypeName = "BulletShotgunManBlueBasicAttack1" },
                    FixTargetDuringAttack = false,
                    StopDuringAttack = true,
                    LeadAmount = 0,
                    LeadChance = 1,
                    RespectReload = true,
                    MagazineCapacity = 1,
                    ReloadSpeed = 2,
                    EmptiesClip = true,
                    SuppressReloadAnim = false,
                    TimeBetweenShots = -1,
                    PreventTargetSwitching = false,
                    OverrideAnimation = null,
                    OverrideDirectionalAnimation = null,
                    HideGun = false,
                    UseLaserSight = false,
                    UseGreenLaser = false,
                    PreFireLaserTime = -1,
                    AimAtFacingDirectionWhenSafe = false,
                    Cooldown = 4,
                    CooldownVariance = 0,
                    AttackCooldown = 0,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 20,
                    MinWallDistance = 0,
                    MaxEnemiesInRoom = 0,
                    MinHealthThreshold = 0,
                    MaxHealthThreshold = 1,
                    HealthThresholds = new float[0],
                    AccumulateHealthThresholds = true,
                    targetAreaStyle = null,
                    IsBlackPhantom = false,
                    resetCooldownOnDamage = null,
                    RequiresLineOfSight = false,
                    MaxUsages = 0
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, BootlegShotgunManBlueGUID, ExpandAmmonomiconDatabase.BootlegShotgunKinBlue);
            m_CachedEnemyActor = null;
            return;
        }
        
        public static void BuildCronenbergPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Cronenberg Abomination");
            
            List<string> SpriteList = new List<string>() {
                "Cronenberg_Idle_001",
                "Cronenberg_Idle_002",
                "Cronenberg_Idle_003",
                "Cronenberg_Idle_004",
                "Cronenberg_Move_001",
                "Cronenberg_Move_002",
                "Cronenberg_Move_003",
                "Cronenberg_Move_004",
                "Cronenberg_Move_005",
                "Cronenberg_Spawn_001",
                "Cronenberg_Spawn_002",
                "Cronenberg_Spawn_003",
                "Cronenberg_Spawn_004",
                "Cronenberg_Spawn_005",
                "Cronenberg_Spawn_006",
                "Cronenberg_Die_001",
                "Cronenberg_Die_002",
                "Cronenberg_Die_003"
            };

            List<string> IdleSpriteList = new List<string>() {
                "Cronenberg_Idle_001",
                "Cronenberg_Idle_002",
                "Cronenberg_Idle_003",
                "Cronenberg_Idle_004"
            };

            List<string> MoveSpriteList = new List<string>() {
                "Cronenberg_Move_001",
                "Cronenberg_Move_002",
                "Cronenberg_Move_003",
                "Cronenberg_Move_004",
                "Cronenberg_Move_005"
            };
                        
            List<string> SpawnSpriteList = new List<string>() {
                "Cronenberg_Spawn_001",
                "Cronenberg_Spawn_002",
                "Cronenberg_Spawn_003",
                "Cronenberg_Spawn_004",
                "Cronenberg_Spawn_005",
                "Cronenberg_Spawn_006",
            };
            
            List<string> DeathSpriteList = new List<string>() { "Cronenberg_Die_001", "Cronenberg_Die_002", "Cronenberg_Die_003" };
                        

            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, CronenbergCollection, SpriteList[0]);
                        
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleSpriteList, "idle", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, MoveSpriteList, "move", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            
            tk2dSpriteAnimationClip m_SpawnAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergCollection.GetComponent<tk2dSpriteCollectionData>(), SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            tk2dSpriteAnimationClip m_HitAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergCollection.GetComponent<tk2dSpriteCollectionData>(), IdleSpriteList, "hit", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            tk2dSpriteAnimationClip m_DeathAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            
            if (m_SpawnAnimation != null && m_DeathAnimation != null && m_HitAnimation != null) {
                m_SpawnAnimation.frames[0].eventAudio = "Play_EX_Cronenberg_Spawn_01";
                m_SpawnAnimation.frames[0].triggerEvent = true;
                m_DeathAnimation.frames[0].eventAudio = "Play_EX_Cronenberg_Die_01";
                m_DeathAnimation.frames[0].triggerEvent = true;
                m_HitAnimation.frames[0].eventAudio = "Play_EX_Cronenberg_Damage_01";
                m_HitAnimation.frames[0].triggerEvent = true;
            }

            
            Gun m_CachedMutantArmGun = (PickupObjectDatabase.GetById(333) as Gun);

            if (m_CachedMutantArmGun) {
                GoopDoer m_BloodGoopDoer = m_CachedTargetObject.AddComponent<GoopDoer>();
                m_BloodGoopDoer.goopDefinition = m_CachedMutantArmGun.singleModule.projectiles[0].gameObject.GetComponent<GoopModifier>().goopDefinition;
                m_BloodGoopDoer.positionSource = GoopDoer.PositionSource.HitBoxCenter;
                m_BloodGoopDoer.updateTiming = GoopDoer.UpdateTiming.Always;
                m_BloodGoopDoer.updateFrequency = 0.05f;
                m_BloodGoopDoer.isTimed = false;
                m_BloodGoopDoer.goopTime = 1;
                m_BloodGoopDoer.updateOnPreDeath = true;
                m_BloodGoopDoer.updateOnDeath = false;
                m_BloodGoopDoer.updateOnAnimFrames = true;
                m_BloodGoopDoer.updateOnCollision = false;
                m_BloodGoopDoer.updateOnGrounded = false;
                m_BloodGoopDoer.updateOnDestroy = false;
                m_BloodGoopDoer.defaultGoopRadius = 1.8f;
                m_BloodGoopDoer.suppressSplashes = false;
                m_BloodGoopDoer.goopSizeVaries = true;
                m_BloodGoopDoer.varyCycleTime = 0.9f;
                m_BloodGoopDoer.radiusMin = 1.8f;
                m_BloodGoopDoer.radiusMax = 2;
                m_BloodGoopDoer.goopSizeRandom = true;
                m_BloodGoopDoer.UsesDispersalParticles = false;
                m_BloodGoopDoer.DispersalDensity = 3;
                m_BloodGoopDoer.DispersalMinCoherency = 0.2f;
                m_BloodGoopDoer.DispersalMaxCoherency = 1;

                CronenbergCorpseDebrisObject1 = expandSharedAssets1.LoadAsset<GameObject>("CronenbergCorpseFragment_01");
                CronenbergCorpseDebrisObject2 = expandSharedAssets1.LoadAsset<GameObject>("CronenbergCorpseFragment_02");
                CronenbergCorpseDebrisObject3 = expandSharedAssets1.LoadAsset<GameObject>("CronenbergCorpseFragment_03");
                CronenbergCorpseDebrisObject4 = expandSharedAssets1.LoadAsset<GameObject>("CronenbergCorpseFragment_04");
                SpriteSerializer.AddSpriteToObject(CronenbergCorpseDebrisObject1, CronenbergCollection, "Cronenberg_Fragment_01");
                SpriteSerializer.AddSpriteToObject(CronenbergCorpseDebrisObject2, CronenbergCollection, "Cronenberg_Fragment_02");
                SpriteSerializer.AddSpriteToObject(CronenbergCorpseDebrisObject3, CronenbergCollection, "Cronenberg_Fragment_03");
                SpriteSerializer.AddSpriteToObject(CronenbergCorpseDebrisObject4, CronenbergCollection, "Cronenberg_Fragment_04");

                ExpandUtility.GenerateSpriteAnimator(CronenbergCorpseDebrisObject1);
                ExpandUtility.GenerateSpriteAnimator(CronenbergCorpseDebrisObject2);
                ExpandUtility.GenerateSpriteAnimator(CronenbergCorpseDebrisObject3);
                ExpandUtility.GenerateSpriteAnimator(CronenbergCorpseDebrisObject4);
                ExpandUtility.AddAnimation(CronenbergCorpseDebrisObject1.GetComponent<tk2dSpriteAnimator>(), CronenbergCollection.GetComponent<tk2dSpriteCollectionData>(), new List<string>() { "Cronenberg_Fragment_01" }, "default");
                ExpandUtility.AddAnimation(CronenbergCorpseDebrisObject2.GetComponent<tk2dSpriteAnimator>(), CronenbergCollection.GetComponent<tk2dSpriteCollectionData>(), new List<string>() { "Cronenberg_Fragment_02" }, "default");
                ExpandUtility.AddAnimation(CronenbergCorpseDebrisObject3.GetComponent<tk2dSpriteAnimator>(), CronenbergCollection.GetComponent<tk2dSpriteCollectionData>(), new List<string>() { "Cronenberg_Fragment_03" }, "default");
                ExpandUtility.AddAnimation(CronenbergCorpseDebrisObject4.GetComponent<tk2dSpriteAnimator>(), CronenbergCollection.GetComponent<tk2dSpriteCollectionData>(), new List<string>() { "Cronenberg_Fragment_04" }, "default");

                m_GenerateCronenbergDebris(CronenbergCorpseDebrisObject1, m_BloodGoopDoer.goopDefinition);
                m_GenerateCronenbergDebris(CronenbergCorpseDebrisObject2, m_BloodGoopDoer.goopDefinition);
                m_GenerateCronenbergDebris(CronenbergCorpseDebrisObject3, m_BloodGoopDoer.goopDefinition);
                m_GenerateCronenbergDebris(CronenbergCorpseDebrisObject4, m_BloodGoopDoer.goopDefinition);

                ExpandExplodeOnDeath m_Exploder = m_CachedTargetObject.AddComponent<ExpandExplodeOnDeath>();
                m_Exploder.shardClusters = new ShardCluster[] {
                    new ShardCluster() {
                        minFromCluster = 6,
                        maxFromCluster = 10,
                        forceMultiplier = 4,
                        forceAxialMultiplier = new Vector3(1.25f, 1.25f, 1.25f),
                        rotationMultiplier = 1.65f,
                        clusterObjects = new DebrisObject[] {
                            CronenbergCorpseDebrisObject1.GetComponent<DebrisObject>(),
                            CronenbergCorpseDebrisObject2.GetComponent<DebrisObject>(),
                            CronenbergCorpseDebrisObject3.GetComponent<DebrisObject>(),
                            CronenbergCorpseDebrisObject4.GetComponent<DebrisObject>()
                        }
                    }
                };
                m_Exploder.spawnShardsOnDeath = true;
                m_Exploder.deathType = OnDeathBehavior.DeathType.Death;
                m_CachedMutantArmGun = null;
            }

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, CronenbergGUID, null, EnemyHasNoCorpse: true, EnemyHasNoShooter: true);
                        
            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            m_CachedAIActor.IsHarmlessEnemy = true;
            m_CachedAIActor.HasShadow = false;
            m_CachedAIActor.MovementSpeed = 0.65f;
            m_CachedAIActor.PathableTiles = CellTypes.FLOOR;
            m_CachedAIActor.procedurallyOutlined = false;
            m_CachedAIActor.EnemySwitchState = string.Empty;
            m_CachedAIActor.PreventFallingInPitsEver = true;
            m_CachedAIActor.IgnoreForRoomClear = true;
            m_CachedAIActor.HitByEnemyBullets = true;
            m_CachedAIActor.CanTargetEnemies = true;
            m_CachedAIActor.CanTargetPlayers = false;
            m_CachedAIActor.CollisionKnockbackStrength = 5;
            m_CachedAIActor.CollisionDamage = 2;
            m_CachedAIActor.healthHaver.SetHealthMaximum(25);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(15);

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 9,
                    ManualOffsetY = 0,
                    ManualWidth = 67,
                    ManualHeight = 16,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 9,
                    ManualOffsetY = 0,
                    ManualWidth = 67,
                    ManualHeight = 28,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
                        
            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Default;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "idle",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "move",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "hit",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[1],
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }
            
            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() { new ExpandSimpleMoveErraticallyBehavior() };
            
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            CronenbergBullets.m_CachedCronenbergBulletsItem.TransmogTargetGuid = CronenbergGUID;
                        
            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, CronenbergGUID, ExpandAmmonomiconDatabase.Cronenberg);
            return;
        }

        public static void BuildAggressiveCronenbergPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Angry Cronenberg Abomination");
            
            List<string> SpriteList = new List<string>() {
                "berg_tall_idle_front_001",
                "berg_tall_idle_front_002",
                "berg_tall_idle_front_003",
                "berg_tall_idle_front_004",
                "berg_tall_idle_back_001",
                "berg_tall_idle_back_002",
                "berg_tall_idle_back_003",
                "berg_tall_idle_back_004",
                "berg_tall_run_left_001",
                "berg_tall_run_left_002",
                "berg_tall_run_left_003",
                "berg_tall_run_left_004",
                "berg_tall_run_left_005",
                "berg_tall_run_left_006",
                "berg_tall_run_right_001",
                "berg_tall_run_right_002",
                "berg_tall_run_right_003",
                "berg_tall_run_right_004",
                "berg_tall_run_right_005",
                "berg_tall_run_right_006",
                "berg_tall_burst_001",
                "berg_tall_burst_002",
                "berg_tall_burst_003",
                "berg_tall_run_back_left_001",
                "berg_tall_run_back_left_002",
                "berg_tall_run_back_left_003",
                "berg_tall_run_back_left_004",
                "berg_tall_run_back_left_005",
                "berg_tall_run_back_left_006",
                "berg_tall_run_back_right_001",
                "berg_tall_run_back_right_002",
                "berg_tall_run_back_right_003",
                "berg_tall_run_back_right_004",
                "berg_tall_run_back_right_005",
                "berg_tall_run_back_right_006",
                "berg_tall_spawn_001",
                "berg_tall_spawn_002",
                "berg_tall_spawn_003",
                "berg_tall_spawn_004",
                "berg_tall_spawn_005"
            };

            List<string> IdleFrontSpriteList = new List<string>() {
                "berg_tall_idle_front_001",
                "berg_tall_idle_front_002",
                "berg_tall_idle_front_003",
                "berg_tall_idle_front_004"
            };

            List<string> IdleBackSpriteList = new List<string>() {
                "berg_tall_idle_back_001",
                "berg_tall_idle_back_002",
                "berg_tall_idle_back_003",
                "berg_tall_idle_back_004"
            };

            List<string> MoveFrontLeftSpriteList = new List<string>() {
                "berg_tall_run_left_001",
                "berg_tall_run_left_002",
                "berg_tall_run_left_003",
                "berg_tall_run_left_004",
                "berg_tall_run_left_005",
                "berg_tall_run_left_006"
            };

            List<string> MoveFrontRightSpriteList = new List<string>() {
                "berg_tall_run_right_001",
                "berg_tall_run_right_002",
                "berg_tall_run_right_003",
                "berg_tall_run_right_004",
                "berg_tall_run_right_005",
                "berg_tall_run_right_006"
            };

            List<string> MoveBackLeftSpriteList = new List<string>() {
                "berg_tall_run_back_left_001",
                "berg_tall_run_back_left_002",
                "berg_tall_run_back_left_003",
                "berg_tall_run_back_left_004",
                "berg_tall_run_back_left_005",
                "berg_tall_run_back_left_006"
            };

            List<string> MoveBackRightSpriteList = new List<string>() {
                "berg_tall_run_back_right_001",
                "berg_tall_run_back_right_002",
                "berg_tall_run_back_right_003",
                "berg_tall_run_back_right_004",
                "berg_tall_run_back_right_005",
                "berg_tall_run_back_right_006"
            };

            
            List<string> SpawnSpriteList = new List<string>() {
                "berg_tall_spawn_001",
                "berg_tall_spawn_002",
                "berg_tall_spawn_003",
                "berg_tall_spawn_004",
                "berg_tall_spawn_005"
            };
            
            List<string> DeathSpriteList = new List<string>() { "berg_tall_burst_001", "berg_tall_burst_002", "berg_tall_burst_003" };
                        
            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, CronenbergTallCollection, SpriteList[0]);
                                    
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergTallCollection.GetComponent<tk2dSpriteCollectionData>(), IdleFrontSpriteList, "idle_front", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergTallCollection.GetComponent<tk2dSpriteCollectionData>(), IdleFrontSpriteList, "idle_back", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergTallCollection.GetComponent<tk2dSpriteCollectionData>(), MoveFrontLeftSpriteList, "move_front_right", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergTallCollection.GetComponent<tk2dSpriteCollectionData>(), MoveFrontRightSpriteList, "move_front_left", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergTallCollection.GetComponent<tk2dSpriteCollectionData>(), MoveBackLeftSpriteList, "move_back_right", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergTallCollection.GetComponent<tk2dSpriteCollectionData>(), MoveBackRightSpriteList, "move_back_left", tk2dSpriteAnimationClip.WrapMode.Loop, 10);

            tk2dSpriteAnimationClip m_SpawnAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergTallCollection.GetComponent<tk2dSpriteCollectionData>(), SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            tk2dSpriteAnimationClip m_HitAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergTallCollection.GetComponent<tk2dSpriteCollectionData>(), IdleFrontSpriteList, "hit", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            tk2dSpriteAnimationClip m_DeathAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CronenbergTallCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            
            if (m_SpawnAnimation != null && m_DeathAnimation != null && m_HitAnimation != null) {
                m_DeathAnimation.frames[0].eventAudio = "Play_EX_Cronenberg_Die_01";
                m_DeathAnimation.frames[0].triggerEvent = true;
                m_HitAnimation.frames[0].eventAudio = "Play_EX_Cronenberg_Damage_01";
                m_HitAnimation.frames[0].triggerEvent = true;
            }

            
            Gun m_CachedMutantArmGun = (PickupObjectDatabase.GetById(333) as Gun);

            if (m_CachedMutantArmGun) {
                GoopDoer m_BloodGoopDoer = m_CachedTargetObject.AddComponent<GoopDoer>();
                m_BloodGoopDoer.goopDefinition = m_CachedMutantArmGun.singleModule.projectiles[0].gameObject.GetComponent<GoopModifier>().goopDefinition;
                m_BloodGoopDoer.positionSource = GoopDoer.PositionSource.HitBoxCenter;
                m_BloodGoopDoer.updateTiming = GoopDoer.UpdateTiming.Always;
                m_BloodGoopDoer.updateFrequency = 0.05f;
                m_BloodGoopDoer.isTimed = false;
                m_BloodGoopDoer.goopTime = 1;
                m_BloodGoopDoer.updateOnPreDeath = true;
                m_BloodGoopDoer.updateOnDeath = false;
                m_BloodGoopDoer.updateOnAnimFrames = true;
                m_BloodGoopDoer.updateOnCollision = false;
                m_BloodGoopDoer.updateOnGrounded = false;
                m_BloodGoopDoer.updateOnDestroy = false;
                m_BloodGoopDoer.defaultGoopRadius = 1.8f;
                m_BloodGoopDoer.suppressSplashes = false;
                m_BloodGoopDoer.goopSizeVaries = true;
                m_BloodGoopDoer.varyCycleTime = 0.9f;
                m_BloodGoopDoer.radiusMin = 1f;
                m_BloodGoopDoer.radiusMax = 1.5f;
                m_BloodGoopDoer.goopSizeRandom = true;
                m_BloodGoopDoer.UsesDispersalParticles = false;
                m_BloodGoopDoer.DispersalDensity = 3;
                m_BloodGoopDoer.DispersalMinCoherency = 0.2f;
                m_BloodGoopDoer.DispersalMaxCoherency = 1;

                AggressiveCronenbergCorpseDebrisObject = expandSharedAssets1.LoadAsset<GameObject>("AngryCronenbergCorpseFragment");
                SpriteSerializer.AddSpriteToObject(AggressiveCronenbergCorpseDebrisObject, CronenbergCollection, "Cronenberg_Fragment_04");

                ExpandUtility.GenerateSpriteAnimator(AggressiveCronenbergCorpseDebrisObject);
                ExpandUtility.AddAnimation(AggressiveCronenbergCorpseDebrisObject.GetComponent<tk2dSpriteAnimator>(), AggressiveCronenbergCorpseDebrisObject.GetComponent<tk2dSprite>().Collection, new List<string>() { "Cronenberg_Fragment_01" }, "default");
                
                m_GenerateCronenbergDebris(AggressiveCronenbergCorpseDebrisObject, m_BloodGoopDoer.goopDefinition);

                ExpandExplodeOnDeath m_Exploder = m_CachedTargetObject.AddComponent<ExpandExplodeOnDeath>();
                m_Exploder.shardClusters = new ShardCluster[] {
                    new ShardCluster() {
                        minFromCluster = 6,
                        maxFromCluster = 10,
                        forceMultiplier = 4,
                        forceAxialMultiplier = new Vector3(1.25f, 1.25f, 1.25f),
                        rotationMultiplier = 1.65f,
                        clusterObjects = new DebrisObject[] {
                            AggressiveCronenbergCorpseDebrisObject.GetComponent<DebrisObject>(),
                            AggressiveCronenbergCorpseDebrisObject.GetComponent<DebrisObject>(),
                            AggressiveCronenbergCorpseDebrisObject.GetComponent<DebrisObject>(),
                            AggressiveCronenbergCorpseDebrisObject.GetComponent<DebrisObject>()
                        }
                    }
                };
                m_Exploder.spawnShardsOnDeath = true;
                m_Exploder.deathType = OnDeathBehavior.DeathType.Death;
                m_CachedMutantArmGun = null;
            }

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, AggressiveCronenbergGUID, null, EnemyHasNoCorpse: true, EnemyHasNoShooter: true);
                        
            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            m_CachedAIActor.HasShadow = false;
            m_CachedAIActor.MovementSpeed = 5f;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.procedurallyOutlined = true;
            m_CachedAIActor.EnemySwitchState = string.Empty;
            m_CachedAIActor.PreventFallingInPitsEver = true;
            m_CachedAIActor.IgnoreForRoomClear = false;
            m_CachedAIActor.HitByEnemyBullets = false;
            m_CachedAIActor.CanTargetEnemies = true;
            m_CachedAIActor.CanTargetPlayers = false;
            m_CachedAIActor.CollisionKnockbackStrength = 5;
            m_CachedAIActor.CollisionDamage = 1;
            m_CachedAIActor.DiesOnCollison = true;
            m_CachedAIActor.healthHaver.SetHealthMaximum(25);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(15);

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 2,
                    ManualOffsetY = 0,
                    ManualWidth = 14,
                    ManualHeight = 16,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 2,
                    ManualOffsetY = 0,
                    ManualWidth = 15,
                    ManualHeight = 29,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
                        
            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Default;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                    Prefix = "idle",
                    AnimNames = new string[2],
                    Flipped = new DirectionalAnimation.FlipType[2]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "move",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "hit",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[1],
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }
            
            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = false,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>();
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new SeekTargetBehavior() {
                    StopWhenInRange = false,
                    CustomRange = 6,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, AggressiveCronenbergGUID, ExpandAmmonomiconDatabase.CronenbergAngry);
            return;
        }

        public static void BuildMetalCubeGuyWestPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            m_CachedTargetObject = UnityEngine.Object.Instantiate(ExpandPrefabs.MetalCubeGuy.gameObject);
            m_CachedTargetObject.SetActive(false);
            m_CachedTargetObject.name = "MetalCubeGuy_TrapVersion_West";

            AIActor m_CachedTargetAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            m_CachedTargetAIActor.ActorName += " West";
            m_CachedTargetAIActor.OverrideDisplayName = m_CachedTargetAIActor.ActorName;
            m_CachedTargetAIActor.EnemyGuid = MetalCubeGuyWestGUID;
            
            StoneCubeCollection_West = expandSharedAssets1.LoadAsset<GameObject>("StoneCubeCollection_West");
            tk2dSpriteCollectionData StoneCubeCollection_WestCollection = StoneCubeCollection_West.AddComponent<tk2dSpriteCollectionData>();

            JsonUtility.FromJsonOverwrite(ExpandAssets.DeserializeJSONDataFromAssetBundle(expandSharedAssets1, "SpriteCollections/StoneCubeCollection_West"), StoneCubeCollection_WestCollection);

            Material m_NewMaterial = new Material(GetOfficialEnemyByGuid("ba928393c8ed47819c2c5f593100a5bc").sprite.Collection.materials[0]);
            m_NewMaterial.mainTexture = expandSharedAssets1.LoadAsset<Texture2D>("Stone_Cube_Collection_West");

            StoneCubeCollection_WestCollection.materials = new Material[] { m_NewMaterial };

            foreach (tk2dSpriteDefinition spriteDefinition in StoneCubeCollection_WestCollection.spriteDefinitions) {
                spriteDefinition.material = m_NewMaterial;
            }

            tk2dSpriteAnimation m_StoneCubeGuyWestAnimation = StoneCubeCollection_West.AddComponent<tk2dSpriteAnimation>();
            ExpandUtility.DuplicateComponent(m_StoneCubeGuyWestAnimation, m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>().Library);

            foreach (tk2dSpriteAnimationClip clip in m_StoneCubeGuyWestAnimation.clips) {
                foreach (tk2dSpriteAnimationFrame frame in clip.frames) {
                    frame.spriteCollection = StoneCubeCollection_WestCollection;
                }
            }

            m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>().Library = m_StoneCubeGuyWestAnimation;
            m_CachedTargetObject.GetComponent<tk2dSprite>().Collection = StoneCubeCollection_WestCollection;
            m_CachedTargetObject.GetComponent<tk2dSprite>().SetSprite(m_CachedTargetObject.GetComponent<tk2dSprite>().spriteId);
            
            AddEnemyToDatabase(m_CachedTargetObject, MetalCubeGuyWestGUID, true);
            FakePrefab.MarkAsFakePrefab(m_CachedTargetObject);
            UnityEngine.Object.DontDestroyOnLoad(m_CachedTargetObject);
        }

        public static void BuildDummyExplodyBarrelGuyPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("EX_ExplodyBoy");
            
            List<string> SpriteList = new List<string>() {
                "EX_ExplodyBarrel",
                "EX_ExplodyBarrel_Explode"
            };

            List<string> IdleSpriteList = new List<string>() {
                "EX_ExplodyBarrel",
                "EX_ExplodyBarrel",
            };

            tk2dSprite m_CachedSprite = m_CachedTargetObject.AddComponent<tk2dSprite>();
            m_CachedSprite.SetSprite(ExpandPrefabs.EXParadropCollection.GetComponent<tk2dSpriteCollectionData>(), "EX_ExplodyBarrel");

            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleSpriteList, "idle", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, m_CachedSprite.Collection, IdleSpriteList, "move", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            
            tk2dSpriteAnimationClip m_SpawnAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ExpandPrefabs.EXParadropCollection.GetComponent<tk2dSpriteCollectionData>(), IdleSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            tk2dSpriteAnimationClip m_HitAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ExpandPrefabs.EXParadropCollection.GetComponent<tk2dSpriteCollectionData>(), IdleSpriteList, "hit", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            tk2dSpriteAnimationClip m_DeathAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ExpandPrefabs.EXParadropCollection.GetComponent<tk2dSpriteCollectionData>(), SpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            
            
            ExpandExplodeOnDeath m_Exploder = m_CachedTargetObject.AddComponent<ExpandExplodeOnDeath>();
            m_Exploder.shardClusters = new ShardCluster[0];
            m_Exploder.spawnShardsOnDeath = false;
            m_Exploder.deathType = OnDeathBehavior.DeathType.Death;


            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "Explody_Boy", ExplodyBoyGUID, null, EnemyHasNoCorpse: true, EnemyHasNoShooter: true);
                        
            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            m_CachedAIActor.HasShadow = false;
            m_CachedAIActor.MovementSpeed = 2f;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.procedurallyOutlined = false;
            m_CachedAIActor.EnemySwitchState = string.Empty;
            m_CachedAIActor.PreventFallingInPitsEver = true;
            m_CachedAIActor.IgnoreForRoomClear = true;
            m_CachedAIActor.HitByEnemyBullets = true;
            m_CachedAIActor.CanTargetEnemies = false;
            m_CachedAIActor.CanTargetPlayers = true;
            m_CachedAIActor.DiesOnCollison = true;
            m_CachedAIActor.healthHaver.SetHealthMaximum(1);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(1);

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 6,
                    ManualOffsetY = 1,
                    ManualWidth = 14,
                    ManualHeight = 16,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 6,
                    ManualOffsetY = 1,
                    ManualWidth = 14,
                    ManualHeight = 20,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            
            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Default;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "idle",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "move",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "hit",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[1],
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }
            
            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() { new ExpandSimpleMoveErraticallyBehavior() };
            
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            AddEnemyToDatabase(m_CachedTargetObject, ExplodyBoyGUID, true);
            return;
        }
        
        public static void BuildSonicCompanionPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5");

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("SonicCompanion");

            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, SonicCompanionCollection, "Sonic_Idle_Forward_01");


            List<string> m_Frames_Move_Left = new List<string>() {
                "Sonic_Move_Left_001",
                "Sonic_Move_Left_002",
                "Sonic_Move_Left_003",
                "Sonic_Move_Left_004",
                "Sonic_Move_Left_005",
                "Sonic_Move_Left_006",
                "Sonic_Move_Left_007",
                "Sonic_Move_Left_008",
                "Sonic_Move_Left_009"
            };

            List<string> m_Frames_Move_Right = new List<string>() {
                "Sonic_Move_Right_001",
                "Sonic_Move_Right_002",
                "Sonic_Move_Right_003",
                "Sonic_Move_Right_004",
                "Sonic_Move_Right_005",
                "Sonic_Move_Right_006",
                "Sonic_Move_Right_007",
                "Sonic_Move_Right_008",
                "Sonic_Move_Right_009"
            };

            List<string> m_Frames_Move_Forward = new List<string>() {
                "Sonic_Move_Forward_001",
                "Sonic_Move_Forward_002",
                "Sonic_Move_Forward_003",
                "Sonic_Move_Forward_004",
                "Sonic_Move_Forward_005",
                "Sonic_Move_Forward_006",
                "Sonic_Move_Forward_007",
                "Sonic_Move_Forward_008",
                "Sonic_Move_Forward_009"
            };

            List<string> m_Frames_Move_Back = new List<string>() {
                "Sonic_Move_Back_001",
                "Sonic_Move_Back_002",
                "Sonic_Move_Back_003",
                "Sonic_Move_Back_004",
                "Sonic_Move_Back_005",
                "Sonic_Move_Back_006",
                "Sonic_Move_Back_007",
                "Sonic_Move_Back_008",
                "Sonic_Move_Back_009"
            };

            List<string> m_Frames_Move_Forward_Left = new List<string>() {
                "Sonic_Move_Forward_Left_001",
                "Sonic_Move_Forward_Left_002",
                "Sonic_Move_Forward_Left_003",
                "Sonic_Move_Forward_Left_004",
                "Sonic_Move_Forward_Left_005",
                "Sonic_Move_Forward_Left_006",
                "Sonic_Move_Forward_Left_007",
                "Sonic_Move_Forward_Left_008",
                "Sonic_Move_Forward_Left_009"
            };
            
            List<string> m_Frames_Move_Forward_Right = new List<string>() {
                "Sonic_Move_Forward_Right_001",
                "Sonic_Move_Forward_Right_002",
                "Sonic_Move_Forward_Right_003",
                "Sonic_Move_Forward_Right_004",
                "Sonic_Move_Forward_Right_005",
                "Sonic_Move_Forward_Right_006",
                "Sonic_Move_Forward_Right_007",
                "Sonic_Move_Forward_Right_008",
                "Sonic_Move_Forward_Right_009",
            };

            List<string> m_Frames_Move_Back_Left = new List<string>() {
                "Sonic_Move_Back_Left_001",
                "Sonic_Move_Back_Left_002",
                "Sonic_Move_Back_Left_003",
                "Sonic_Move_Back_Left_004",
                "Sonic_Move_Back_Left_005",
                "Sonic_Move_Back_Left_006",
                "Sonic_Move_Back_Left_007",
                "Sonic_Move_Back_Left_008",
                "Sonic_Move_Back_Left_009",
                "Sonic_Move_Back_Left_010"
            };

            List<string> m_Frames_Move_Back_Right = new List<string>() {
                "Sonic_Move_Back_Right_001",
                "Sonic_Move_Back_Right_002",
                "Sonic_Move_Back_Right_003",
                "Sonic_Move_Back_Right_004",
                "Sonic_Move_Back_Right_005",
                "Sonic_Move_Back_Right_006",
                "Sonic_Move_Back_Right_007",
                "Sonic_Move_Back_Right_008",
                "Sonic_Move_Back_Right_009",
                "Sonic_Move_Back_Right_010"
            };

            List<string> m_Frames_Idle_Left = new List<string>() { "Sonic_Idle_Left_01", "Sonic_Idle_Left_01" };
            List<string> m_Frames_Idle_Right = new List<string>() { "Sonic_Idle_Right_01", "Sonic_Idle_Right_01" };
            List<string> m_Frames_Idle_Forward = new List<string>() { "Sonic_Idle_Forward_01", "Sonic_Idle_Forward_01" };
            List<string> m_Frames_Idle_Back = new List<string>() { "Sonic_Idle_Back_01", "Sonic_Idle_Back_01" };
            List<string> m_Frames_Idle_Forward_Left = new List<string>() { "Sonic_Idle_Forward_Left_01", "Sonic_Idle_Forward_Left_01" };
            List<string> m_Frames_Idle_Forward_Right = new List<string>() { "Sonic_Idle_Forward_Right_01", "Sonic_Idle_Forward_Right_01" };
            List<string> m_Frames_Idle_Back_Left = new List<string>() { "Sonic_Idle_Back_Left_01", "Sonic_Idle_Back_Left_01" };
            List<string> m_Frames_Idle_Back_Right = new List<string>() { "Sonic_Idle_Back_Right_01", "Sonic_Idle_Back_Right_01" };
            
            List<string> m_Frames_SpindashCharge_South = new List<string>() {
                "Sonic_SpindashCharge_Forward_01",
                "Sonic_SpindashCharge_Forward_02",
                "Sonic_SpindashCharge_Forward_03",
                "Sonic_SpindashCharge_Forward_04",
                "Sonic_SpindashCharge_Forward_05",
                "Sonic_SpindashCharge_Forward_06",
                "Sonic_SpindashCharge_Forward_02",
                "Sonic_SpindashCharge_Forward_03",
                "Sonic_SpindashCharge_Forward_04",
                "Sonic_SpindashCharge_Forward_05",
                "Sonic_SpindashCharge_Forward_06",
                "Sonic_SpindashCharge_Forward_02",
                "Sonic_SpindashCharge_Forward_03",
                "Sonic_SpindashCharge_Forward_04",
                "Sonic_SpindashCharge_Forward_05",
                "Sonic_SpindashCharge_Forward_06"
            };

            List<string> m_Frames_SpindashCharge_North = new List<string>() {
                "Sonic_SpindashCharge_Back_01",
                "Sonic_SpindashCharge_Back_02",
                "Sonic_SpindashCharge_Back_03",
                "Sonic_SpindashCharge_Back_04",
                "Sonic_SpindashCharge_Back_05",
                "Sonic_SpindashCharge_Back_06",
                "Sonic_SpindashCharge_Back_02",
                "Sonic_SpindashCharge_Back_03",
                "Sonic_SpindashCharge_Back_04",
                "Sonic_SpindashCharge_Back_05",
                "Sonic_SpindashCharge_Back_06",
                "Sonic_SpindashCharge_Back_02",
                "Sonic_SpindashCharge_Back_03",
                "Sonic_SpindashCharge_Back_04",
                "Sonic_SpindashCharge_Back_05",
                "Sonic_SpindashCharge_Back_06"
            };

            List<string> m_Frames_SpindashCharge_West = new List<string>() {
                "Sonic_SpindashCharge_Left_01",
                "Sonic_SpindashCharge_Left_02",
                "Sonic_SpindashCharge_Left_03",
                "Sonic_SpindashCharge_Left_04",
                "Sonic_SpindashCharge_Left_05",
                "Sonic_SpindashCharge_Left_06",
                "Sonic_SpindashCharge_Left_02",
                "Sonic_SpindashCharge_Left_03",
                "Sonic_SpindashCharge_Left_04",
                "Sonic_SpindashCharge_Left_05",
                "Sonic_SpindashCharge_Left_06",
                "Sonic_SpindashCharge_Left_02",
                "Sonic_SpindashCharge_Left_03",
                "Sonic_SpindashCharge_Left_04",
                "Sonic_SpindashCharge_Left_05",
                "Sonic_SpindashCharge_Left_06"
            };

            List<string> m_Frames_SpindashCharge_East = new List<string>() {
                "Sonic_SpindashCharge_Right_01",
                "Sonic_SpindashCharge_Right_02",
                "Sonic_SpindashCharge_Right_03",
                "Sonic_SpindashCharge_Right_04",
                "Sonic_SpindashCharge_Right_05",
                "Sonic_SpindashCharge_Right_06",
                "Sonic_SpindashCharge_Right_02",
                "Sonic_SpindashCharge_Right_03",
                "Sonic_SpindashCharge_Right_04",
                "Sonic_SpindashCharge_Right_05",
                "Sonic_SpindashCharge_Right_06",
                "Sonic_SpindashCharge_Right_02",
                "Sonic_SpindashCharge_Right_03",
                "Sonic_SpindashCharge_Right_04",
                "Sonic_SpindashCharge_Right_05",
                "Sonic_SpindashCharge_Right_06"
            };

            List<string> m_Frames_Spindash = new List<string>() {
                "Sonic_Spindash_01",
                "Sonic_Spindash_01",
                "Sonic_Spindash_02",
                "Sonic_Spindash_03",
                "Sonic_Spindash_04",
                "Sonic_Spindash_05",
                "Sonic_Spindash_06"
            };
            
            List<string> m_Frames_SpinDashRebound_Forward = new List<string>() { "Sonic_Rebound_Forward_01", "Sonic_Rebound_Forward_01" };
            List<string> m_Frames_SpinDashRebound_Back = new List<string>() { "Sonic_Rebound_Back_01", "Sonic_Rebound_Back_01" };
            List<string> m_Frames_SpinDashRebound_Forward_Left = new List<string>() { "Sonic_Rebound_Forward_Left_01", "Sonic_Rebound_Forward_Left_01" };
            List<string> m_Frames_SpinDashRebound_Forward_Right = new List<string>() { "Sonic_Rebound_Forward_Right_01", "Sonic_Rebound_Forward_Right_01" };
            List<string> m_Frames_SpinDashRebound_Back_Left = new List<string>() { "Sonic_Rebound_Back_Left_01", "Sonic_Rebound_Back_Left_01" };
            List<string> m_Frames_SpinDashRebound_Back_Right = new List<string>() { "Sonic_Rebound_Back_Right_01", "Sonic_Rebound_Back_Right_01" };

            List<string> m_Frames_Death = new List<string>() { "Sonic_Rebound_Death_01", "Sonic_Rebound_Death_01" };
            List<string> m_Frames_Pitfall = new List<string>() {
                "Sonic_Rebound_Death_01",
                "Sonic_Rebound_Death_02",
                "Sonic_Rebound_Death_03",
                "Sonic_Rebound_Death_04",
                "Sonic_Rebound_Death_05"
            };


            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, DefaultClipId: 0);
            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Left, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop, 2);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Right, "idle_right", tk2dSpriteAnimationClip.WrapMode.Loop, 2);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Forward, "idle_forward", tk2dSpriteAnimationClip.WrapMode.Loop, 2);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Forward_Left, "idle_forward_left", tk2dSpriteAnimationClip.WrapMode.Loop, 2);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Forward_Right, "idle_forward_right", tk2dSpriteAnimationClip.WrapMode.Loop, 2);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Back, "idle_back", tk2dSpriteAnimationClip.WrapMode.Loop, 2);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Back_Left, "idle_back_left", tk2dSpriteAnimationClip.WrapMode.Loop, 2);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Back_Right, "idle_back_right", tk2dSpriteAnimationClip.WrapMode.Loop, 2);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Left, "move_left", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Right, "move_right", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Forward, "move_forward", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Forward_Left, "move_forward_left", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Forward_Right, "move_forward_right", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Back, "move_back", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Back_Left, "move_back_left", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Back_Right, "move_back_right", tk2dSpriteAnimationClip.WrapMode.Loop, 12);

            tk2dSpriteAnimationClip m_SpinDashChargeNorth = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_SpindashCharge_North, "spindashcharge_north", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            m_SpinDashChargeNorth.frames[0].eventAudio = "Play_EX_SonicSpinDashCharge_01";
            m_SpinDashChargeNorth.frames[0].triggerEvent = true;
            tk2dSpriteAnimationClip m_SpinDashChargeSouth = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_SpindashCharge_South, "spindashcharge_south", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            m_SpinDashChargeSouth.frames[0].eventAudio = "Play_EX_SonicSpinDashCharge_01";
            m_SpinDashChargeSouth.frames[0].triggerEvent = true;
            tk2dSpriteAnimationClip m_SpinDashChargeWest = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_SpindashCharge_West, "spindashcharge_west", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            m_SpinDashChargeWest.frames[0].eventAudio = "Play_EX_SonicSpinDashCharge_01";
            m_SpinDashChargeWest.frames[0].triggerEvent = true;
            tk2dSpriteAnimationClip m_SpinDashChargeEast = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_SpindashCharge_East, "spindashcharge_east", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            m_SpinDashChargeEast.frames[0].eventAudio = "Play_EX_SonicSpinDashCharge_01";
            m_SpinDashChargeEast.frames[0].triggerEvent = true;

            tk2dSpriteAnimationClip m_SpinDash = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Spindash, "spindash_release", tk2dSpriteAnimationClip.WrapMode.LoopSection, 12, 1);
            m_SpinDash.frames[0].eventAudio = "Play_EX_SonicSpinDashRelease_01";
            m_SpinDash.frames[0].triggerEvent = true;
            
            tk2dSpriteAnimationClip m_Rebound_Forward = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_SpinDashRebound_Forward, "rebound_forward", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            // m_Rebound_Forward.frames[0].eventAudio = "Play_EX_SonicBrake_01";
            // m_Rebound_Forward.frames[0].triggerEvent = true;

            tk2dSpriteAnimationClip m_Rebound_Forward_Left = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_SpinDashRebound_Forward_Left, "rebound_forward_left", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            // m_Rebound_Forward_Left.frames[0].eventAudio = "Play_EX_SonicBrake_01";
            // m_Rebound_Forward_Left.frames[0].triggerEvent = true;

            tk2dSpriteAnimationClip m_Rebound_Forward_Right = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_SpinDashRebound_Forward_Right, "rebound_forward_right", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            // m_Rebound_Forward_Right.frames[0].eventAudio = "Play_EX_SonicBrake_01";
            // m_Rebound_Forward_Right.frames[0].triggerEvent = true;

            tk2dSpriteAnimationClip m_Rebound_Back = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_SpinDashRebound_Back, "rebound_back", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            // m_Rebound_Back.frames[0].eventAudio = "Play_EX_SonicBrake_01";
            // m_Rebound_Back.frames[0].triggerEvent = true;

            tk2dSpriteAnimationClip m_Rebound_Back_Left = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_SpinDashRebound_Back_Left, "rebound_back_left", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            // m_Rebound_Back_Left.frames[0].eventAudio = "Play_EX_SonicBrake_01";
            // m_Rebound_Back_Left.frames[0].triggerEvent = true;

            tk2dSpriteAnimationClip m_Rebound_Back_Right = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_SpinDashRebound_Back_Right, "rebound_back_right", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            // m_Rebound_Back_Right.frames[0].eventAudio = "Play_EX_SonicBrake_01";
            // m_Rebound_Back_Right.frames[0].triggerEvent = true;

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), new List<string>() { "Sonic_Idle_Forward_01" }, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), new List<string>() { "Sonic_Idle_Forward_01" }, "awaken", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Pitfall, "pitfall", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            tk2dSpriteAnimationClip m_DeathClip = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, SonicCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Death, "death", tk2dSpriteAnimationClip.WrapMode.Once, 16);
            m_DeathClip.frames[0].eventAudio = "Play_EX_SonicDeath_01";
            m_DeathClip.frames[0].triggerEvent = true;

            foreach (tk2dSpriteAnimationClip clip in m_CachedSpriteAnimator.Library.clips) {
                if (clip.name.ToLower().StartsWith("spindash")) {
                    foreach (tk2dSpriteAnimationFrame frame in clip.frames) { frame.invulnerableFrame = true; }
                }
            }

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "Sonic The Hedgehog", SonicCompanionGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: m_CachedEnemyActor.CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            // m_CachedAIActor.HasShadow = false;
            m_CachedAIActor.MovementSpeed = 6;
            m_CachedAIActor.EnemySwitchState = string.Empty;
            // m_CachedAIActor.procedurallyOutlined = false;
            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 12,
                    ManualOffsetY = 10,
                    ManualWidth = 17,
                    ManualHeight = 16,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 12,
                    ManualOffsetY = 16,
                    ManualWidth = 17,
                    ManualHeight = 24,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.Projectile,
                    Enabled = false,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 12,
                    ManualOffsetY = 10,
                    ManualWidth = 17,
                    ManualHeight = 16,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            
            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Movement;
                m_CachedAIActor.aiAnimator.directionalType = AIAnimator.DirectionalType.Sprite;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.AnimatedFacingDirection = -90;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.EightWay,
                    Prefix = "idle",
                    AnimNames = new string[] { "idle_back", "idle_back_right", string.Empty, "idle_forward_right", "idle_forward", "idle_forward_left", string.Empty, "idle_back_left" },
                    Flipped = new DirectionalAnimation.FlipType[8],
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.EightWay,
                    Prefix = "move",
                    AnimNames = new string[] { "move_back", "move_back_right", string.Empty, "move_forward_right", "move_forward", "move_forward_left", string.Empty, "move_back_left" },
                    Flipped = new DirectionalAnimation.FlipType[8],
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "rebound",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.SixWay,
                            Prefix = "rebound",
                            AnimNames = new string[] { string.Empty, string.Empty, "rebound_forward_right", "rebound_forward", "rebound_forward_left", string.Empty },
                            Flipped = new DirectionalAnimation.FlipType[6]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "spindashcharge",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWayCardinal,
                            Prefix = "spindashcharge",
                            AnimNames = new string[4],
                            Flipped = new DirectionalAnimation.FlipType[4]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "spindash_release",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "spindash_release",
                            AnimNames = new string[] { "spindash_release" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "spawn",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "spawn",
                            AnimNames = new string[] { "spawn" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "death",
                            AnimNames = new string[] { "death" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }
            
            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };

            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new CompanionFollowPlayerBehavior() {
                    PathInterval = 0.25f,
                    DisableInCombat = true,
                    IdealRadius = 4,
                    CatchUpRadius = 8f,
                    CatchUpAccelTime = 3,
                    CatchUpSpeed = 6,
                    CatchUpMaxSpeed = 10,
                    CatchUpAnimation = string.Empty,
                    CatchUpOutAnimation = string.Empty,
                    IdleAnimations = new string[0],
                    CanRollOverPits = false,
                    RollAnimation = string.Empty
                },
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = -1,
                    LineOfSight = true,
                    ReturnToSpawn = false,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.25f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                    // ExternalCooldownSource = true
                }
            };

            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ExpandChargeBehavior() {
                    avoidExits = true,
                    avoidWalls = false,
                    minRange = 0,
                    primeTime = -1,
                    stopDuringPrime = true,
                    leadAmount = 1,
                    chargeSpeed = 25,
                    chargeAcceleration = -1,
                    maxChargeDistance = -1,
                    chargeKnockback = 120,
                    chargeDamage = 25,
                    wallRecoilForce = 20,
                    stoppedByProjectiles = false,
                    endWhenChargeAnimFinishes = false,
                    switchCollidersOnCharge = true,
                    collidesWithDodgeRollingPlayers = true,
                    ShootPoint = null,
                    bulletScript = null,
                    primeAnim = "spindashcharge",
                    chargeAnim = "spindash_release",
                    hitAnim = "rebound",
                    HideGun = false,
                    launchVfx = null,
                    trailVfx = null,
                    trailVfxParent = null,
                    hitVfx = null,
                    nonActorHitVfx = null,
                    chargeDustUps = true,
                    chargeDustUpInterval = 0.25f,
                    Cooldown = 2,
                    CooldownVariance = 0,
                    AttackCooldown = 0,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 20,
                    MinWallDistance = 0,
                    MaxEnemiesInRoom = 0,
                    MinHealthThreshold = 0,
                    MaxHealthThreshold = 1,
                    HealthThresholds = new float[0],
                    AccumulateHealthThresholds = true,
                    targetAreaStyle = null,
                    IsBlackPhantom = false,
                    resetCooldownOnDamage = null,
                    RequiresLineOfSight = true,
                    MaxUsages = 0
                }
            };


            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;
            
            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
                        
            m_CachedAIActor.healthHaver.SetHealthMaximum(500);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(30);

            ExpandUtility.MakeCompanion(m_CachedAIActor, null, null, true, false, true, false);

            ExpandCompanionManager m_companionManager = m_CachedTargetObject.AddComponent<ExpandCompanionManager>();
            m_companionManager.SwapFaceTypesOnTarget = false;
            m_companionManager.HideGunsWhenNoTarget = false;
            m_companionManager.Rescale = true;

            ExpandTossSpriteOnDeath spriteTosser = m_CachedTargetObject.AddComponent<ExpandTossSpriteOnDeath>();
            spriteTosser.isPowBlockDeath = false;
            spriteTosser.applyRotation = false;
            spriteTosser.PopupSpeed = 0.08f;
            spriteTosser.DropSpeed = 0.11f;
            spriteTosser.specificSpriteName = "Sonic_Rebound_Death_01";

            AddEnemyToDatabase(m_CachedTargetObject, SonicCompanionGUID, false);
            m_CachedEnemyActor = null;
            return;
        }

        public static void BuildClownkinPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject, out GameObject m_CachedWigObject) {

            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5");

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Clownkin");

            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, ClownkinCollection, "clownkin_idle_left_001");


            List<string> m_Frames_Move_Forward_Left = new List<string>() {
                "clownkin_run_left_001",
                "clownkin_run_left_002",
                "clownkin_run_left_003",
                "clownkin_run_left_004",
                "clownkin_run_left_005",
                "clownkin_run_left_006",
            };

            List<string> m_Frames_Move_Forward_Right = new List<string>() {
                "clownkin_run_right_001",
                "clownkin_run_right_002",
                "clownkin_run_right_003",
                "clownkin_run_right_004",
                "clownkin_run_right_005",
                "clownkin_run_right_006",
            };

            List<string> m_Frames_Move_Back_Left = new List<string>() {
                "clownkin_run_left_back_001",
                "clownkin_run_left_back_002",
                "clownkin_run_left_back_003",
                "clownkin_run_left_back_004",
                "clownkin_run_left_back_005",
                "clownkin_run_left_back_006"
            };

            List<string> m_Frames_Move_Back_Right = new List<string>() {
                "clownkin_run_right_back_001",
                "clownkin_run_right_back_002",
                "clownkin_run_right_back_003",
                "clownkin_run_right_back_004",
                "clownkin_run_right_back_005",
                "clownkin_run_right_back_006",
            };
            
            List<string> m_Frames_Idle_Left = new List<string>() { "clownkin_idle_left_001", "clownkin_idle_left_002" };
            List<string> m_Frames_Idle_Right = new List<string>() { "clownkin_idle_right_001", "clownkin_idle_right_002" };
            List<string> m_Frames_Idle_Back = new List<string>() { "clownkin_idle_back_001", "clownkin_idle_back_002" };


            List<string> m_Frames_Hit_Left = new List<string>() { "clownkin_hit_left_001" };
            List<string> m_Frames_Hit_Right = new List<string>() { "clownkin_hit_right_001" };
            List<string> m_Frames_Hit_Back_Left = new List<string>() { "clownkin_hit_back_left_001" };
            List<string> m_Frames_Hit_Back_Right = new List<string>() { "clownkin_hit_back_right_001" };

  

            List<string> m_Frames_Die_Left = new List<string>() {
                "clownkin_die_left_001",
                "clownkin_die_left_002"
            };
            
            List<string> m_Frames_Die_Right = new List<string>() {
                "clownkin_die_right_001",
                "clownkin_die_right_002"
            };

            List<string> m_Frames_Die_Back_Right = new List<string>() {
                "clownkin_die_right_back_001",
                "clownkin_die_right_back_002",
            };
            
            List<string> m_Frames_Die_Back_Left = new List<string>() {
                "clownkin_die_left_back_001",
                "clownkin_die_left_back_002",
            };
            
            List<string> m_Frames_Death_Left = new List<string>() {
                "clownkin_death_left_front_001",
                "clownkin_death_left_front_002",
                "clownkin_death_left_front_003",
                "clownkin_death_left_front_004",
                "clownkin_death_left_front_005"
            };

            List<string> m_Frames_Death_LeftSide = new List<string>() {
                "clownkin_death_left_side_001",
                "clownkin_death_left_side_002",
                "clownkin_death_left_side_003",
                "clownkin_death_left_side_004"
            };

            List<string> m_Frames_Death_Front_Right = new List<string>() {
                "clownkin_death_right_front_001",
                "clownkin_death_right_front_002",
                "clownkin_death_right_front_003",
                "clownkin_death_right_front_004",
                "clownkin_death_right_front_005",
            };

            List<string> m_Frames_Death_RightSide = new List<string>() {
                "clownkin_death_right_side_001",
                "clownkin_death_right_side_002",
                "clownkin_death_right_side_003",
                "clownkin_death_right_side_004",
            };

            List<string> m_Frames_Death_Right = new List<string>() {
                "clownkin_death_right_side_001",
                "clownkin_death_right_side_002",
                "clownkin_death_right_side_003",
                "clownkin_death_right_side_004",
            };

            List<string> m_Frames_Death_Back_Right = new List<string>() {
                "clownkin_death_right_back_001",
                "clownkin_death_right_back_002",
                "clownkin_death_right_back_003",
                "clownkin_death_right_back_004",
                "clownkin_death_right_back_005"
            };

            List<string> m_Frames_Death_Back_Left = new List<string>() {
                "clownkin_death_left_back_001",
                "clownkin_death_left_back_002",
                "clownkin_death_left_back_003",
                "clownkin_death_left_back_004",
                "clownkin_death_left_back_005"
            };

            List<string> m_Frames_Death_Back_North = new List<string>() {
                "clownkin_death_front_north_001",
                "clownkin_death_front_north_002",
                "clownkin_death_front_north_003",
                "clownkin_death_front_north_004"
            };
            
            List<string> m_Frames_Death_Back_South = new List<string>() {
                "clownkin_death_back_south_001",
                "clownkin_death_back_south_002",
                "clownkin_death_back_south_003",
                "clownkin_death_back_south_004"
            };
            
            List<string> m_Frames_Spawn = new List<string>() {
                "clownkin_spawn_001",
                "clownkin_spawn_002",
                "clownkin_spawn_003",
            };

            List<string> m_Frames_Pitfall = new List<string>() {
                "clownkin_pitfall_001",
                "clownkin_pitfall_002",
                "clownkin_pitfall_003",
                "clownkin_pitfall_004",
                "clownkin_pitfall_005"
            };


            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, DefaultClipId: 0);
            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Left, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop, 5);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Right, "idle_right", tk2dSpriteAnimationClip.WrapMode.Loop, 5);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Back, "idle_back", tk2dSpriteAnimationClip.WrapMode.Loop, 5);
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Forward_Left, "move_forward_left", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Forward_Right, "move_forward_right", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Back_Left, "move_back_left", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Back_Right, "move_back_right", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Spawn, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Pitfall, "pitfall", tk2dSpriteAnimationClip.WrapMode.Once, 15);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Hit_Left, "hit_left", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Hit_Right, "hit_right", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Hit_Back_Left, "hit_back_left", tk2dSpriteAnimationClip.WrapMode.Once, 4);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Hit_Back_Right, "hit_back_right", tk2dSpriteAnimationClip.WrapMode.Once, 4);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Die_Left, "die_left", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Die_Right, "die_right", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Die_Back_Left, "die_back_left", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Die_Back_Right, "die_back_right", tk2dSpriteAnimationClip.WrapMode.Once, 10);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Death_Left, "death_left", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Death_Right, "death_right", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Death_Back_Left, "death_back_left", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Death_Back_Right, "death_back_right", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Death_LeftSide, "death_left_side", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Death_RightSide, "death_right_side", tk2dSpriteAnimationClip.WrapMode.Once, 10);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "ClownKin", ClownkinGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();
            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.EnemySwitchState = "Metal_Bullet_Man";
            m_CachedAIActor.IgnoreForRoomClear = true;
            m_CachedAIActor.specRigidbody.PixelColliders[0].ManualOffsetX = 8;
            m_CachedAIActor.specRigidbody.PixelColliders[1].ManualOffsetX = 8;
            m_CachedAIActor.IsHarmlessEnemy = true;
            m_CachedAIActor.CollisionDamage = 0;


            m_CachedWigObject = expandSharedAssets1.LoadAsset<GameObject>("Clownkin_Wig");
            m_CachedWigObject.layer = 22;

            tk2dSprite m_ClownkinWigSprite = SpriteSerializer.AddSpriteToObject(ClownkinWig, ClownkinCollection, "clownkin_wig");
            m_ClownkinWigSprite.HeightOffGround = 0f;

            List<string> m_ClownWigGroundedFrames = new List<string>() {
                "clownkin_wig",
                "clownkin_wig_grounded"
            };

            ExpandUtility.GenerateSpriteAnimator(m_CachedWigObject, playAutomatically: true);

            ExpandUtility.AddAnimation(m_CachedWigObject.GetComponent<tk2dSpriteAnimator>(), ClownkinCollection.GetComponent<tk2dSpriteCollectionData>(), m_ClownWigGroundedFrames, "wig_drop", frameRate: 2);
            m_CachedWigObject.GetComponent<tk2dSpriteAnimator>().DefaultClipId = 0;

            DebrisObject m_CachedDebrisObject = m_CachedWigObject.AddComponent<DebrisObject>();
            m_CachedDebrisObject.Priority = EphemeralObject.EphemeralPriority.Middling;
            m_CachedDebrisObject.audioEventName = string.Empty;
            m_CachedDebrisObject.playAnimationOnTrigger = false;
            m_CachedDebrisObject.usesDirectionalFallAnimations = false;
            m_CachedDebrisObject.directionalAnimationData = new DebrisDirectionalAnimationInfo() { fallDown = string.Empty, fallLeft = string.Empty, fallRight = string.Empty, fallUp = string.Empty };
            m_CachedDebrisObject.breaksOnFall = true;
            m_CachedDebrisObject.breakOnFallChance = 1;
            m_CachedDebrisObject.changesCollisionLayer = false;
            m_CachedDebrisObject.groundedCollisionLayer = CollisionLayer.LowObstacle;
            m_CachedDebrisObject.followupBehavior = DebrisObject.DebrisFollowupAction.None;
            m_CachedDebrisObject.collisionStopsBullets = false;
            m_CachedDebrisObject.animatePitFall = false;
            m_CachedDebrisObject.pitFallSplash = true;
            m_CachedDebrisObject.inertialMass = 1;
            m_CachedDebrisObject.motionMultiplier = 1;
            m_CachedDebrisObject.canRotate = true;
            m_CachedDebrisObject.angularVelocity = 0;
            m_CachedDebrisObject.angularVelocityVariance = 20;
            m_CachedDebrisObject.bounceCount = 1;
            m_CachedDebrisObject.additionalBounceEnglish = 0;
            m_CachedDebrisObject.decayOnBounce = 0.5f;
            m_CachedDebrisObject.killTranslationOnBounce = false;
            m_CachedDebrisObject.usesLifespan = false;
            m_CachedDebrisObject.lifespanMin = 1;
            m_CachedDebrisObject.lifespanMax = 1;
            m_CachedDebrisObject.shouldUseSRBMotion = false;
            m_CachedDebrisObject.removeSRBOnGrounded = false;
            m_CachedDebrisObject.placementOptions = new DebrisObject.DebrisPlacementOptions { canBeRotated = false, canBeFlippedHorizontally = false, canBeFlippedVertically = false };
            m_CachedDebrisObject.DoesGoopOnRest = false;
            m_CachedDebrisObject.AssignedGoop = null;
            m_CachedDebrisObject.GoopRadius = 1;
            m_CachedDebrisObject.additionalHeightBoost = 0;
            m_CachedDebrisObject.AssignFinalWorldDepth(-1.5f);
            m_CachedDebrisObject.ForceUpdateIfDisabled = false;

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }
                                    
            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Movement;
                m_CachedAIActor.aiAnimator.directionalType = AIAnimator.DirectionalType.Sprite;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = true;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.AnimatedFacingDirection = -90;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "idle",
                    AnimNames = new string[] { "idle_back", "idle_right", "idle_left", "idle_back" },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "move",
                    AnimNames = new string[] { string.Empty, "move_forward_right", "move_forward_left", string.Empty },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "hit",
                    AnimNames = new string[] { "hit_back_left", "hit_left", "hit_right", "hit_back_right" },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>(0);
            }
            
            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() { new ExpandSimpleMoveErraticallyBehavior() };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);

            m_CachedTargetObject.AddComponent<ExpandClownKinBalloonManager>();

            HelmetController m_WigTosser = m_CachedTargetObject.AddComponent<HelmetController>();
            m_WigTosser.helmetEffect = m_CachedWigObject;
            m_WigTosser.helmetForce = 5;
            

            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, ClownkinGUID, ExpandAmmonomiconDatabase.ClownKin);
            m_CachedEnemyActor = null;

            return;
        }
        
        public static void BuildClownkinAltPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin
            AIActor m_CachedEnemyActor2 = GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5"); // BulletKin
            AIActor CachedSpaceTurtle = GetOfficialEnemyByGuid("9216803e9c894002a4b931d7ea9c6bdf");

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("ClownkinAlt");

            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, ClownkinCollection, "clownkin_idle_left_001");
            
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, ClownkinPrefab.GetComponent<tk2dSpriteAnimator>().Library, DefaultClipId: 0);
            
            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;

            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, CachedSpaceTurtle.aiShooter, CachedSpaceTurtle.GetComponent<AIBulletBank>(), 520, m_CachedGunAttachPoint.transform);


            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "ClownKin Companion", ClownkinAltGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);
            
            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();
            m_CachedAIActor.MovementSpeed = 3;
            m_CachedAIActor.IgnoreForRoomClear = true;
            m_CachedAIActor.IsHarmlessEnemy = true;
            m_CachedAIActor.EnemySwitchState = "Metal_Bullet_Man";
            m_CachedAIActor.specRigidbody.PixelColliders[0].ManualOffsetX = 8;
            m_CachedAIActor.specRigidbody.PixelColliders[1].ManualOffsetX = 8;
            m_CachedAIActor.aiShooter.AllowTwoHands = true;
            m_CachedAIActor.aiShooter.handObject = m_CachedEnemyActor2.aiShooter.handObject;
            m_CachedAIActor.CanDropCurrency = false;

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }
                                    
            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Movement;
                m_CachedAIActor.aiAnimator.directionalType = AIAnimator.DirectionalType.Sprite;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = true;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.AnimatedFacingDirection = -90;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "idle",
                    AnimNames = new string[] { "idle_back", "idle_right", "idle_left", "idle_back" },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "move",
                    AnimNames = new string[] { string.Empty, "move_forward_right", "move_forward_left", string.Empty },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "hit",
                    AnimNames = new string[] { "hit_back_left", "hit_left", "hit_right", "hit_back_right" },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "spawn",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWay,
                            Prefix = "spawn",
                            AnimNames = new string[] { "idle_back", "idle_right", "idle_left", "idle_back" },
                            Flipped = new DirectionalAnimation.FlipType[4],
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "awaken",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWay,
                            Prefix = "awaken",
                            AnimNames = new string[] { "idle_back", "idle_right", "idle_left", "idle_back" },
                            Flipped = new DirectionalAnimation.FlipType[4],
                        }
                    },
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new CompanionFollowPlayerBehavior() {
                    PathInterval = 0.25f,
                    DisableInCombat = true,
                    IdealRadius = 3f,
                    CatchUpRadius = 6f,
                    CatchUpAccelTime = 5,
                    CatchUpSpeed = 6,
                    CatchUpMaxSpeed = 10,
                    CatchUpAnimation = string.Empty,
                    CatchUpOutAnimation = string.Empty,
                    IdleAnimations = new string[] { "idle" },
                    CanRollOverPits = false,
                    RollAnimation = string.Empty
                },
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 6,
                    LineOfSight = true,
                    ReturnToSpawn = false,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0,
                    LineOfSight = true,
                    WeaponType = WeaponType.AIShooterProjectile,
                    OverrideBulletName = null,
                    BulletScript = null,
                    FixTargetDuringAttack = false,
                    StopDuringAttack = false,
                    LeadAmount = 1,
                    LeadChance = 0.5f,
                    RespectReload = true,
                    MagazineCapacity = 6,
                    ReloadSpeed = 3.5f,
                    EmptiesClip = false,
                    SuppressReloadAnim = false,
                    TimeBetweenShots = -1,
                    PreventTargetSwitching = false,
                    OverrideAnimation = null,
                    OverrideDirectionalAnimation = null,
                    HideGun = false,
                    UseLaserSight = false,
                    UseGreenLaser = false,
                    PreFireLaserTime = -1,
                    AimAtFacingDirectionWhenSafe = false,
                    Cooldown = 0.25f,
                    CooldownVariance = 0,
                    AttackCooldown = 0.25f,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 12,
                    MinWallDistance = 0,
                    MaxEnemiesInRoom = 0,
                    MinHealthThreshold = 0,
                    MaxHealthThreshold = 1,
                    HealthThresholds = new float[0],
                    AccumulateHealthThresholds = true,
                    targetAreaStyle = null,
                    IsBlackPhantom = false,
                    resetCooldownOnDamage = null,
                    RequiresLineOfSight = false,
                    MaxUsages = 0
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);

            ExpandClownKinBalloonManager m_ClownKinController = m_CachedTargetObject.AddComponent<ExpandClownKinBalloonManager>();
            m_ClownKinController.IsSingleBalloon = true;
            m_ClownKinController.SingleBalloonDoesBlankOnPop = true;
            m_ClownKinController.DoConfettiOnSpawn = false;

            HelmetController m_WigTosser = m_CachedTargetObject.AddComponent<HelmetController>();
            m_WigTosser.helmetEffect = ClownkinWig;
            m_WigTosser.helmetForce = 5;


            ExpandUtility.MakeCompanion(m_CachedAIActor, null, null, true, false, false, true);

            ExpandCompanionManager companionManager = m_CachedTargetObject.AddComponent<ExpandCompanionManager>();
            companionManager.ToggleFaceSouthWhenStopped = true;
            companionManager.WithTargetFaceType = AIAnimator.FacingType.Target;

            AddEnemyToDatabase(m_CachedTargetObject, ClownkinAltGUID, true);
            m_CachedEnemyActor = null;
            m_CachedEnemyActor2 = null;
            CachedSpaceTurtle = null;

            return;
        }

        public static void BuildClownkinNoFXPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {

            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5");

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Clownkin_NoFX");

            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, ClownkinCollection, "clownkin_idle_left_001");

            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, ClownkinPrefab.GetComponent<tk2dSpriteAnimator>().Library, DefaultClipId: 0);
            
            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "ClownKin From Chest", ClownkinNoFXGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5").CorpseObject, EnemyHasNoShooter: true);

            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();
            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.EnemySwitchState = "Metal_Bullet_Man";
            m_CachedAIActor.IgnoreForRoomClear = true;
            m_CachedAIActor.specRigidbody.PixelColliders[0].ManualOffsetX = 8;
            m_CachedAIActor.specRigidbody.PixelColliders[1].ManualOffsetX = 8;
            m_CachedAIActor.IsHarmlessEnemy = true;
            m_CachedAIActor.CollisionDamage = 0;
            
            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }
                                    
            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Movement;
                m_CachedAIActor.aiAnimator.directionalType = AIAnimator.DirectionalType.Sprite;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = true;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.AnimatedFacingDirection = -90;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "idle",
                    AnimNames = new string[] { "idle_back", "idle_right", "idle_left", "idle_back" },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "move",
                    AnimNames = new string[] { string.Empty, "move_forward_right", "move_forward_left", string.Empty },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "hit",
                    AnimNames = new string[] { "hit_back_left", "hit_left", "hit_right", "hit_back_right" },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>(0);
            }
            
            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() { new ExpandSimpleMoveErraticallyBehavior() };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
                        
            HelmetController m_WigTosser = m_CachedTargetObject.AddComponent<HelmetController>();
            m_WigTosser.helmetEffect = ClownkinWig;
            m_WigTosser.helmetForce = 5;
            
            AddEnemyToDatabase(m_CachedTargetObject, ClownkinNoFXGUID, true);
            m_CachedEnemyActor = null;

            return;
        }

        public static void BuildClownkinAngryPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin
            AIActor m_CachedEnemyActor2 = GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5"); // BulletKin

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Clownkin_Angry");

            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, ClownkinCollection, "clownkin_idle_left_001");
            
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, ClownkinNoFXPrefab.GetComponent<tk2dSpriteAnimator>().Library, DefaultClipId: 0);
            
            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;

            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, m_CachedEnemyActor.aiShooter, m_CachedEnemyActor.GetComponent<AIBulletBank>(), 520, m_CachedGunAttachPoint.transform);
            
            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "Angry ClownKin", ClownkinAngryGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);
            
            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();
            m_CachedAIActor.MovementSpeed = 3;
            m_CachedAIActor.IgnoreForRoomClear = false;
            m_CachedAIActor.IsHarmlessEnemy = false;
            m_CachedAIActor.EnemySwitchState = "Metal_Bullet_Man";
            m_CachedAIActor.specRigidbody.PixelColliders[0].ManualOffsetX = 8;
            m_CachedAIActor.specRigidbody.PixelColliders[1].ManualOffsetX = 8;
            m_CachedAIActor.aiShooter.AllowTwoHands = true;
            m_CachedAIActor.aiShooter.handObject = m_CachedEnemyActor2.aiShooter.handObject;

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }
                                    
            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Movement;
                m_CachedAIActor.aiAnimator.directionalType = AIAnimator.DirectionalType.Sprite;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.AnimatedFacingDirection = -90;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "idle",
                    AnimNames = new string[] { "idle_back", "idle_right", "idle_left", "idle_back" },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "move",
                    AnimNames = new string[] { string.Empty, "move_forward_right", "move_forward_left", string.Empty },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "hit",
                    AnimNames = new string[] { "hit_back_left", "hit_left", "hit_right", "hit_back_right" },
                    Flipped = new DirectionalAnimation.FlipType[4],
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "spawn",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWay,
                            Prefix = "spawn",
                            AnimNames = new string[] { "idle_back", "idle_right", "idle_left", "idle_back" },
                            Flipped = new DirectionalAnimation.FlipType[4],
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "awaken",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.FourWay,
                            Prefix = "awaken",
                            AnimNames = new string[] { "idle_back", "idle_right", "idle_left", "idle_back" },
                            Flipped = new DirectionalAnimation.FlipType[4],
                        }
                    },
                };
            }

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 6,
                    LineOfSight = false,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0,
                    LineOfSight = true,
                    WeaponType = WeaponType.AIShooterProjectile,
                    OverrideBulletName = null,
                    BulletScript = null,
                    FixTargetDuringAttack = false,
                    StopDuringAttack = false,
                    LeadAmount = 1,
                    LeadChance = 0.5f,
                    RespectReload = true,
                    MagazineCapacity = 6,
                    ReloadSpeed = 3.5f,
                    EmptiesClip = false,
                    SuppressReloadAnim = false,
                    TimeBetweenShots = -1,
                    PreventTargetSwitching = false,
                    OverrideAnimation = null,
                    OverrideDirectionalAnimation = null,
                    HideGun = false,
                    UseLaserSight = false,
                    UseGreenLaser = false,
                    PreFireLaserTime = -1,
                    AimAtFacingDirectionWhenSafe = false,
                    Cooldown = 0.25f,
                    CooldownVariance = 0,
                    AttackCooldown = 0.25f,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 12,
                    MinWallDistance = 0,
                    MaxEnemiesInRoom = 0,
                    MinHealthThreshold = 0,
                    MaxHealthThreshold = 1,
                    HealthThresholds = new float[0],
                    AccumulateHealthThresholds = true,
                    targetAreaStyle = null,
                    IsBlackPhantom = false,
                    resetCooldownOnDamage = null,
                    RequiresLineOfSight = false,
                    MaxUsages = 0
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 1f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            HelmetController m_WigTosser = m_CachedTargetObject.AddComponent<HelmetController>();
            m_WigTosser.helmetEffect = ClownkinWig;
            m_WigTosser.helmetForce = 5;
            
            AddEnemyToDatabase(m_CachedTargetObject, ClownkinAngryGUID, true);
            m_CachedEnemyActor = null;
            m_CachedEnemyActor2 = null;
            // CachedSpaceTurtle = null;
            return;
        }


        public static void BuildEntityPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Backrooms Entity");
            
            List<string> IdleFrontSpriteList = new List<string>() {
                "entity_idle_front_001",
                "entity_idle_front_002",
                "entity_idle_front_003",
                "entity_idle_front_004"
            };

            List<string> IdleBackSpriteList = new List<string>() {
                "entity_idle_back_001",
                "entity_idle_back_002",
                "entity_idle_back_003",
                "entity_idle_back_004"
            };

            List<string> MoveFrontLeftSpriteList = new List<string>() {
                "entity_run_left_001",
                "entity_run_left_002",
                "entity_run_left_003",
                "entity_run_left_004",
                "entity_run_left_005",
                "entity_run_left_006"
            };

            List<string> MoveFrontRightSpriteList = new List<string>() {
                "entity_run_right_001",
                "entity_run_right_002",
                "entity_run_right_003",
                "entity_run_right_004",
                "entity_run_right_005",
                "entity_run_right_006"
            };

            List<string> MoveBackLeftSpriteList = new List<string>() {
                "entity_run_back_left_001",
                "entity_run_back_left_002",
                "entity_run_back_left_003",
                "entity_run_back_left_004",
                "entity_run_back_left_005",
                "entity_run_back_left_006"
            };

            List<string> MoveBackRightSpriteList = new List<string>() {
                "entity_run_back_right_001",
                "entity_run_back_right_002",
                "entity_run_back_right_003",
                "entity_run_back_right_004",
                "entity_run_back_right_005",
                "entity_run_back_right_006"
            };

            
            List<string> SpawnSpriteList = new List<string>() {
                "entity_spawn_001",
                "entity_spawn_002",
                "entity_spawn_003",
                "entity_spawn_004",
                "entity_spawn_005",
                "entity_spawn_006",
                "entity_spawn_007",
                "entity_spawn_008",
                "entity_spawn_009",
                "entity_spawn_010",
                "entity_spawn_011",
                "entity_spawn_012",
                "entity_spawn_013",
                "entity_spawn_014",
                "entity_spawn_015",
                "entity_spawn_016",
                "entity_spawn_017",
                "entity_spawn_018",
                "entity_spawn_019",
                "entity_spawn_020",
                "entity_spawn_021",
                "entity_spawn_022",
                "entity_spawn_023",
                "entity_spawn_024",
                "entity_spawn_025",
                "entity_spawn_026",
                "entity_spawn_027",
                "entity_spawn_028",
                "entity_spawn_029"
            };

            List<string> DeSpawnSpriteList = new List<string>() {
                "entity_spawn_029",
                "entity_spawn_028",
                "entity_spawn_027",
                "entity_spawn_026",
                "entity_spawn_025",
                "entity_spawn_024",
                "entity_spawn_023",
                "entity_spawn_022",
                "entity_spawn_021",
                "entity_spawn_020",
                "entity_spawn_019",
                "entity_spawn_018",
                "entity_spawn_017",
                "entity_spawn_016",
                "entity_spawn_015",
                "entity_spawn_014",
                "entity_spawn_013",
                "entity_spawn_012",
                "entity_spawn_011",
                "entity_spawn_010",
                "entity_spawn_009",
                "entity_spawn_008",
                "entity_spawn_007",
                "entity_spawn_006",
                "entity_spawn_005",
                "entity_spawn_004",
                "entity_spawn_003",
                "entity_spawn_002",
                "entity_spawn_001"
            };
            


            List<string> DeathSpriteList = new List<string>() { "entity_idle_front_001", "entity_idle_front_001" };
            
            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, EntityCollection, "entity_idle_front_001");
                                    
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, null, 0, 0, playAutomatically: true, clipTime: 0, ClipFps: 0);

            tk2dSpriteAnimator m_CachedSpriteAnimator = m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>();
            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, EntityCollection.GetComponent<tk2dSpriteCollectionData>(), IdleFrontSpriteList, "idle_front", tk2dSpriteAnimationClip.WrapMode.Loop, 6);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, EntityCollection.GetComponent<tk2dSpriteCollectionData>(), IdleFrontSpriteList, "idle_back", tk2dSpriteAnimationClip.WrapMode.Loop, 6);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, EntityCollection.GetComponent<tk2dSpriteCollectionData>(), MoveFrontRightSpriteList, "move_front_right", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, EntityCollection.GetComponent<tk2dSpriteCollectionData>(), MoveFrontLeftSpriteList, "move_front_left", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, EntityCollection.GetComponent<tk2dSpriteCollectionData>(), MoveBackRightSpriteList, "move_back_right", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, EntityCollection.GetComponent<tk2dSpriteCollectionData>(), MoveBackLeftSpriteList, "move_back_left", tk2dSpriteAnimationClip.WrapMode.Loop, 10);

            tk2dSpriteAnimationClip m_DeSpawnAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, EntityCollection.GetComponent<tk2dSpriteCollectionData>(), DeSpawnSpriteList, "despawn", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            tk2dSpriteAnimationClip m_SpawnAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, EntityCollection.GetComponent<tk2dSpriteCollectionData>(), SpawnSpriteList, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            tk2dSpriteAnimationClip m_HitAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, EntityCollection.GetComponent<tk2dSpriteCollectionData>(), IdleFrontSpriteList, "hit", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            tk2dSpriteAnimationClip m_DeathAnimation = ExpandUtility.AddAnimation(m_CachedSpriteAnimator, EntityCollection.GetComponent<tk2dSpriteCollectionData>(), DeathSpriteList, "die", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            if (m_SpawnAnimation != null) {
                m_SpawnAnimation.frames[0].eventAudio = "Play_NPC_creep_creeping_01";
                m_SpawnAnimation.frames[0].triggerEvent = true;
            }

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, m_CachedTargetObject.name, EntityGUID, null, instantiateCorpseObject: false, EnemyHasNoShooter: true);
                        
            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            m_CachedAIActor.EffectResistances = new ActorEffectResistance[] {
                new ActorEffectResistance() {
                    resistAmount = 1,
                    resistType = EffectResistanceType.Poison
                },
            };

            // m_CachedAIActor.HasShadow = false;
            m_CachedAIActor.ActorShadowOffset = new Vector3(0, -0.18f, 0);
            m_CachedAIActor.MovementSpeed = 7f;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            m_CachedAIActor.procedurallyOutlined = true;
            m_CachedAIActor.EnemySwitchState = string.Empty;
            m_CachedAIActor.PreventFallingInPitsEver = true;
            m_CachedAIActor.IgnoreForRoomClear = true;
            m_CachedAIActor.HitByEnemyBullets = false;
            m_CachedAIActor.CanTargetEnemies = true;
            m_CachedAIActor.CanTargetPlayers = false;
            m_CachedAIActor.CollisionKnockbackStrength = 5;
            m_CachedAIActor.CollisionDamage = 1;
            m_CachedAIActor.DiesOnCollison = false;
            m_CachedAIActor.healthHaver.SetHealthMaximum(70);
            m_CachedAIActor.healthHaver.ForceSetCurrentHealth(70);
            m_CachedAIActor.knockbackDoer.weight = 35;
            m_CachedAIActor.procedurallyOutlined = true;
            m_CachedAIActor.isPassable = false;

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    Enabled = false,
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 7,
                    ManualOffsetY = 5,
                    ManualWidth = 11,
                    ManualHeight = 16,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_CachedAIActor.specRigidbody.PixelColliders.Add(
                new PixelCollider() {
                    Enabled = false,
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 6,
                    ManualOffsetY = 6,
                    ManualWidth = 13,
                    ManualHeight = 24,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );

            if (m_CachedAIActor.aiAnimator) {
                m_CachedAIActor.aiAnimator.facingType = AIAnimator.FacingType.Default;
                m_CachedAIActor.aiAnimator.faceSouthWhenStopped = false;
                m_CachedAIActor.aiAnimator.faceTargetWhenStopped = false;
                m_CachedAIActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                m_CachedAIActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                    Prefix = "idle",
                    AnimNames = new string[2],
                    Flipped = new DirectionalAnimation.FlipType[2]
                };
                m_CachedAIActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Prefix = "move",
                    AnimNames = new string[4],
                    Flipped = new DirectionalAnimation.FlipType[4]
                };
                m_CachedAIActor.aiAnimator.HitAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.Single,
                    Prefix = "hit",
                    AnimNames = new string[1],
                    Flipped = new DirectionalAnimation.FlipType[1]
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "die",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "die",
                            AnimNames = new string[1],
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
                m_CachedAIActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "despawn",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "despawn",
                            AnimNames = new string[1],
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    }
                };
            }
            
            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = false,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>();
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new SeekTargetBehavior() {
                    StopWhenInRange = false,
                    CustomRange = 6,
                    LineOfSight = false,
                    ReturnToSpawn = false,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            
            customBehaviorSpeculator.InstantFirstTick = true;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;
            
            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);


            GoopDoer m_BacteriaGoopDoer = m_CachedTargetObject.AddComponent<GoopDoer>();
            m_BacteriaGoopDoer.goopDefinition = ExpandPrefabs.EXBacteriaGoop;
            m_BacteriaGoopDoer.positionSource = GoopDoer.PositionSource.SpecifyGameObject;
            m_BacteriaGoopDoer.goopCenter = m_CachedTargetObject.transform.Find("goopObject")?.gameObject;
            m_BacteriaGoopDoer.updateTiming = GoopDoer.UpdateTiming.Always;
            m_BacteriaGoopDoer.updateFrequency = 0.05f;
            m_BacteriaGoopDoer.isTimed = false;
            m_BacteriaGoopDoer.goopTime = 1;
            m_BacteriaGoopDoer.updateOnPreDeath = true;
            m_BacteriaGoopDoer.updateOnDeath = false;
            m_BacteriaGoopDoer.updateOnAnimFrames = false;
            m_BacteriaGoopDoer.updateOnCollision = false;
            m_BacteriaGoopDoer.updateOnGrounded = false;
            m_BacteriaGoopDoer.updateOnDestroy = false;
            m_BacteriaGoopDoer.defaultGoopRadius = 1.8f;
            m_BacteriaGoopDoer.suppressSplashes = false;
            m_BacteriaGoopDoer.goopSizeVaries = true;
            m_BacteriaGoopDoer.varyCycleTime = 0.9f;
            m_BacteriaGoopDoer.radiusMin = 1.25f;
            m_BacteriaGoopDoer.radiusMax = 1.8f;
            m_BacteriaGoopDoer.goopSizeRandom = true;
            m_BacteriaGoopDoer.UsesDispersalParticles = false;
            m_BacteriaGoopDoer.DispersalDensity = 2;
            m_BacteriaGoopDoer.DispersalMinCoherency = 0.2f;
            m_BacteriaGoopDoer.DispersalMaxCoherency = 1;

            ExplodeOnDeath m_Exploder = m_CachedTargetObject.AddComponent<ExplodeOnDeath>();
            m_Exploder.explosionData = ExpandUtility.GenerateExplosionData();
            m_Exploder.immuneToIBombApp = true;
            m_Exploder.LinearChainExplosion = false;
            m_Exploder.deathType = OnDeathBehavior.DeathType.PreDeath;
            m_Exploder.preDeathDelay = 0.1f;

            m_CachedTargetObject.AddComponent<ExpandEntityManager>();

            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, EntityGUID, ExpandAmmonomiconDatabase.Entity);
            return;
        }

        // Dummy prefab for assinging corrupted enemies to room prefabs.
        public static void BuildCorruptedEnemyPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedSourceActor = GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5"); //bullet_kin
            
            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("EXCorruptedEnemy");
                        
            tk2dSprite m_CachedSprite = m_CachedTargetObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateComponent(m_CachedSprite, m_CachedSourceActor.gameObject.GetComponent<tk2dSprite>());

            tk2dSpriteAnimation m_CachedAnimation = m_CachedTargetObject.AddComponent<tk2dSpriteAnimation>();
            ExpandUtility.DuplicateComponent(m_CachedAnimation, m_CachedSourceActor.gameObject.GetComponent<tk2dSpriteAnimator>().Library);

            tk2dSpriteAnimator m_CachedAnimator = m_CachedTargetObject.AddComponent<tk2dSpriteAnimator>();
            ExpandUtility.DuplicateComponent(m_CachedAnimator, m_CachedSourceActor.gameObject.GetComponent<tk2dSpriteAnimator>());
            m_CachedAnimator.Library = m_CachedAnimation;
            
            SpeculativeRigidbody m_CachedRigidBody = m_CachedTargetObject.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateComponent(m_CachedRigidBody, m_CachedSourceActor.gameObject.GetComponent<SpeculativeRigidbody>());

            AIAnimator m_CachedAIAnimator = m_CachedTargetObject.AddComponent<AIAnimator>();
            ExpandUtility.DuplicateComponent(m_CachedAIAnimator, m_CachedSourceActor.gameObject.GetComponent<AIAnimator>());

            AIActor m_CachedAIActor = ExpandUtility.BuildNewAIActor(m_CachedTargetObject, "Corrupted Enemy", corruptedEnemyGUID, EnemyHasNoShooter: true);

            if (!m_CachedAIActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[ExpandTheGungeon] [BuildCorruptedEnemyPrefab] ERROR: New AIActor component returned null!", false);
                return;
            }

            m_CachedAIActor.invisibleUntilAwaken = true;

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>(0);
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            
            m_CachedTargetObject.AddComponent<ExpandCorruptedEnemyEngageDoer>();
            
            AddEnemyToDatabase(m_CachedTargetObject, corruptedEnemyGUID, true);
            m_CachedSourceActor = null;
            return;
        }

        public static void BuildCultistCompanionPrefab(AssetBundle expandSharedAssets1, out GameObject CachedTargetEnemyObject) {
            GameObject m_CoopCultistPrefab = ExpandAssets.LoadOfficialAsset<GameObject>("PlayerCoopCultist", ExpandAssets.AssetSource.BraveResources).gameObject;
            AIActor CachedEnemyActor = GetOfficialEnemyByGuid("57255ed50ee24794b7aac1ac3cfb8a95");
            AIActor CachedSpaceTurtle = GetOfficialEnemyByGuid("9216803e9c894002a4b931d7ea9c6bdf");
            
            GameObject m_DummyCorpseObject = null;
            
            CachedTargetEnemyObject = expandSharedAssets1.LoadAsset<GameObject>("Cultist Companion");

            // tk2dSprite newSprite = CachedTargetEnemyObject.AddComponent<tk2dSprite>();
            tk2dSprite newSprite = SpriteSerializer.AddSpriteToObject(CachedTargetEnemyObject, CultistCompanionCollection, "cultist_idle_front_001");
            newSprite.HeightOffGround = CachedEnemyActor.sprite.HeightOffGround;
            // ExpandUtility.DuplicateComponent(newSprite, m_SelectedPlayer.GetComponent<tk2dSprite>());
            // If Player sprite was flipped (aka, player aiming/facing towards the left), then this could cause sprite being shifted left on AIActor.
            // Always set false to ensure this doesn't happen.
            // newSprite.FlipX = false;
            
            List<string> m_Frames_Move_Forward = new List<string>() {
                "cultist_move_front_001",
                "cultist_move_front_002",
                "cultist_move_front_003",
                "cultist_move_front_004",
                "cultist_move_front_005",
                "cultist_move_front_006"
            };

            List<string> m_Frames_Move_Back = new List<string>() {
                "cultist_move_back_001",
                "cultist_move_back_002",
                "cultist_move_back_003",
                "cultist_move_back_004",
                "cultist_move_back_005",
                "cultist_move_back_006"
            };

            List<string> m_Frames_Move_Forward_Left = new List<string>() {
                "cultist_move_front_left_001",
                "cultist_move_front_left_002",
                "cultist_move_front_left_003",
                "cultist_move_front_left_004",
                "cultist_move_front_left_005",
                "cultist_move_front_left_006"
            };

            List<string> m_Frames_Move_Forward_Right = new List<string>() {
                "cultist_move_front_right_001",
                "cultist_move_front_right_002",
                "cultist_move_front_right_003",
                "cultist_move_front_right_004",
                "cultist_move_front_right_005",
                "cultist_move_front_right_006"
            };

            List<string> m_Frames_Move_Back_Left = new List<string>() {
                "cultist_move_back_left_001",
                "cultist_move_back_left_002",
                "cultist_move_back_left_003",
                "cultist_move_back_left_004",
                "cultist_move_back_left_005",
                "cultist_move_back_left_006"
            };

            List<string> m_Frames_Move_Back_Right = new List<string>() {
                "cultist_move_back_right_001",
                "cultist_move_back_right_002",
                "cultist_move_back_right_003",
                "cultist_move_back_right_004",
                "cultist_move_back_right_005",
                "cultist_move_back_right_006"
            };

            List<string> m_Frames_Idle_Forward = new List<string>() {
                "cultist_idle_front_001",
                "cultist_idle_front_002",
                "cultist_idle_front_003",
                "cultist_idle_front_004"
            };

            List<string> m_Frames_Idle_Back = new List<string>() {
                "cultist_idle_back_001",
                "cultist_idle_back_002",
                "cultist_idle_back_003",
                "cultist_idle_back_004"
            };

            List<string> m_Frames_Idle_Forward_Left = new List<string>() {
                "cultist_idle_front_left_001",
                "cultist_idle_front_left_002",
                "cultist_idle_front_left_003",
                "cultist_idle_front_left_004"
            };

            List<string> m_Frames_Idle_Forward_Right = new List<string>() {
                "cultist_idle_front_right_001",
                "cultist_idle_front_right_002",
                "cultist_idle_front_right_003",
                "cultist_idle_front_right_004"
            };

            List<string> m_Frames_Idle_Back_Left = new List<string>() {
                "cultist_idle_back_left_001",
                "cultist_idle_back_left_002",
                "cultist_idle_back_left_003",
                "cultist_idle_back_left_004"
            };

            List<string> m_Frames_Idle_Back_Right = new List<string>() {
                "cultist_idle_back_right_001",
                "cultist_idle_back_right_002",
                "cultist_idle_back_right_003",
                "cultist_idle_back_right_004"
            };

            List<string> m_Frames_Dodge_Forward = new List<string>() {
                "cultist_dodge_front_001",
                "cultist_dodge_front_002",
                "cultist_dodge_front_003",
                "cultist_dodge_front_004",
                "cultist_dodge_front_005",
                "cultist_dodge_front_006",
                "cultist_dodge_front_007",
                "cultist_dodge_front_008",
                "cultist_dodge_front_009"
            };

            List<string> m_Frames_Dodge_Back = new List<string>() {
                "cultist_dodge_back_001",
                "cultist_dodge_back_002",
                "cultist_dodge_back_003",
                "cultist_dodge_back_004",
                "cultist_dodge_back_005",
                "cultist_dodge_back_006",
                "cultist_dodge_back_007",
                "cultist_dodge_back_008",
            };

            List<string> m_Frames_Dodge_Forward_Left = new List<string>() {
                "cultist_dodge_front_left_001",
                "cultist_dodge_front_left_002",
                "cultist_dodge_front_left_003",
                "cultist_dodge_front_left_004",
                "cultist_dodge_front_left_005",
                "cultist_dodge_front_left_006",
                "cultist_dodge_front_left_007",
                "cultist_dodge_front_left_008",
                "cultist_dodge_front_left_009"
            };

            List<string> m_Frames_Dodge_Forward_Right = new List<string>() {
                "cultist_dodge_front_right_001",
                "cultist_dodge_front_right_002",
                "cultist_dodge_front_right_003",
                "cultist_dodge_front_right_004",
                "cultist_dodge_front_right_005",
                "cultist_dodge_front_right_006",
                "cultist_dodge_front_right_007",
                "cultist_dodge_front_right_008",
                "cultist_dodge_front_right_009"
            };

            List<string> m_Frames_Dodge_Back_Left = new List<string>() {
                "cultist_dodge_back_left_001",
                "cultist_dodge_back_left_002",
                "cultist_dodge_back_left_003",
                "cultist_dodge_back_left_004",
                "cultist_dodge_back_left_005",
                "cultist_dodge_back_left_006",
                "cultist_dodge_back_left_007",
                "cultist_dodge_back_left_008",
                "cultist_dodge_back_left_009"
            };

            List<string> m_Frames_Dodge_Back_Right = new List<string>() {
                "cultist_dodge_back_right_001",
                "cultist_dodge_back_right_002",
                "cultist_dodge_back_right_003",
                "cultist_dodge_back_right_004",
                "cultist_dodge_back_right_005",
                "cultist_dodge_back_right_006",
                "cultist_dodge_back_right_007",
                "cultist_dodge_back_right_008",
                "cultist_dodge_back_right_009"
            };

            List<string> m_Frames_Death_Left = new List<string>() {
                "cultist_death_left_001",
                "cultist_death_left_002",
                "cultist_death_left_003",
                "cultist_death_left_004",
                "cultist_death_left_005",
                "cultist_death_left_006",
                "cultist_death_left_007"
            };

            List<string> m_Frames_Death_Right = new List<string>() {
                "cultist_death_right_001",
                "cultist_death_right_002",
                "cultist_death_right_003",
                "cultist_death_right_004",
                "cultist_death_right_005",
                "cultist_death_right_006",
                "cultist_death_right_007",
            };

            List<string> m_Frames_Pitfall_Front = new List<string>() {
                "cultist_pitfall_001",
                "cultist_pitfall_002",
                "cultist_pitfall_003",
                "cultist_pitfall_004",
                "cultist_pitfall_005"
            };

            List<string> m_Frames_Pitfall_Back = new List<string>() {
                "cultist_pitfall_down_001",
                "cultist_pitfall_down_002",
                "cultist_pitfall_down_003",
                "cultist_pitfall_down_004",
                "cultist_pitfall_down_005",
                "cultist_pitfall_down_006"
            };

            List<string> m_Frames_Pitfall_Return = new List<string>() {
                "cultist_pitfall_return_001",
                "cultist_pitfall_return_002",
                "cultist_pitfall_return_003",
                "cultist_pitfall_return_004"
            };

            ExpandUtility.GenerateSpriteAnimator(CachedTargetEnemyObject, DefaultClipId: 0);
            tk2dSpriteAnimator m_CachedSpriteAnimator = CachedTargetEnemyObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Forward, "idle_forward", tk2dSpriteAnimationClip.WrapMode.Loop, 8);            
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Forward_Left, "idle_forward_left", tk2dSpriteAnimationClip.WrapMode.Loop, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Forward_Right, "idle_forward_right", tk2dSpriteAnimationClip.WrapMode.Loop, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Back, "idle_back", tk2dSpriteAnimationClip.WrapMode.Loop, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Back_Left, "idle_back_left", tk2dSpriteAnimationClip.WrapMode.Loop, 8);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Idle_Back_Right, "idle_back_right", tk2dSpriteAnimationClip.WrapMode.Loop, 8);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Forward, "move_forward", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Forward_Left, "move_forward_left", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Forward_Right, "move_forward_right", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Back, "move_back", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Back_Left, "move_back_left", tk2dSpriteAnimationClip.WrapMode.Loop, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Move_Back_Right, "move_back_right", tk2dSpriteAnimationClip.WrapMode.Loop, 12);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Dodge_Forward, "dodge_forward", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Dodge_Forward_Left, "dodge_forward_left", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Dodge_Forward_Right, "dodge_forward_right", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Dodge_Back, "dodge_back", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Dodge_Back_Left, "dodge_back_left", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Dodge_Back_Right, "dodge_back_right", tk2dSpriteAnimationClip.WrapMode.Once, 12);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Pitfall_Return, "spawn", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Pitfall_Return, "awaken", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Death_Left, "death_left", tk2dSpriteAnimationClip.WrapMode.Once, 16);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Death_Right, "death_right", tk2dSpriteAnimationClip.WrapMode.Once, 16);

            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Pitfall_Front, "pitfall_front", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            ExpandUtility.AddAnimation(m_CachedSpriteAnimator, CultistCompanionCollection.GetComponent<tk2dSpriteCollectionData>(), m_Frames_Pitfall_Back, "pitfall_back", tk2dSpriteAnimationClip.WrapMode.Once, 10);


            GameObject m_CachedGunAttachPoint = CachedTargetEnemyObject.transform.Find("GunAttachPoint").gameObject;

            ExpandUtility.DuplicateAIShooterAndAIBulletBank(CachedTargetEnemyObject, CachedSpaceTurtle.aiShooter, CachedSpaceTurtle.GetComponent<AIBulletBank>(), 24, m_CachedGunAttachPoint.transform);

            ExpandUtility.DuplicateComponent(CachedTargetEnemyObject.AddComponent<HealthHaver>(), GetOfficialEnemyByGuid("705e9081261446039e1ed9ff16905d04").healthHaver);

            ExpandUtility.GenerateAIActorTemplate(CachedTargetEnemyObject, out m_DummyCorpseObject, "Cultist Companion", FriendlyCultistGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: CachedEnemyActor.CorpseObject, EnemyHasNoShooter: true);

            AIActor CachedCultistCompanion = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (CachedCultistCompanion.aiShooter) {
                CachedCultistCompanion.aiShooter.handObject = m_CoopCultistPrefab.GetComponent<PlayerController>().primaryHand;
                CachedCultistCompanion.aiShooter.gunAttachPoint = m_CachedGunAttachPoint.transform;
                CachedCultistCompanion.aiShooter.AllowTwoHands = true;
            }

            CachedCultistCompanion.CanDropCurrency = false;
            CachedCultistCompanion.IgnoreForRoomClear = true;
            CachedCultistCompanion.DoDustUps = true;
            CachedCultistCompanion.DustUpInterval = 0.4f;
            CachedCultistCompanion.MovementSpeed = 3.5f;
            CachedCultistCompanion.EnemySwitchState = "Gun Cultist";


            if (CachedCultistCompanion.aiAnimator) {
                CachedCultistCompanion.aiAnimator.facingType = AIAnimator.FacingType.Movement;
                CachedCultistCompanion.aiAnimator.directionalType = AIAnimator.DirectionalType.Sprite;
                CachedCultistCompanion.aiAnimator.faceSouthWhenStopped = false;
                CachedCultistCompanion.aiAnimator.faceTargetWhenStopped = false;
                CachedCultistCompanion.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                CachedCultistCompanion.aiAnimator.AnimatedFacingDirection = -90;
                CachedCultistCompanion.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.SixWay,
                    Prefix = "idle",
                    AnimNames = new string[] { string.Empty, string.Empty, "idle_forward_right", "idle_forward", "idle_forward_left", string.Empty },
                    Flipped = new DirectionalAnimation.FlipType[6],
                };
                CachedCultistCompanion.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.SixWay,
                    Prefix = "move",
                    AnimNames = new string[] { string.Empty, string.Empty, "move_forward_right", "move_forward", "move_forward_left", string.Empty },
                    Flipped = new DirectionalAnimation.FlipType[6],
                };
                CachedCultistCompanion.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "dodgeroll",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.SixWay,
                            Prefix = "dodge",
                            AnimNames = new string[] { string.Empty, string.Empty, "dodge_forward_right", "dodge_forward", "dodge_forward_left", string.Empty },
                            Flipped = new DirectionalAnimation.FlipType[6]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall_front", "pitfall_back" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "spawn",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.Single,
                            Prefix = "spawn",
                            AnimNames = new string[] { "spawn" },
                            Flipped = new DirectionalAnimation.FlipType[1]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "death",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                            Prefix = "death",
                            AnimNames = new string[] { "death_left", "death_right" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    },
                };
            }
                        
            BehaviorSpeculator behaviorSpeculator = CachedTargetEnemyObject.AddComponent<BehaviorSpeculator>();
            ExpandUtility.DuplicateComponent(behaviorSpeculator, CachedEnemyActor.behaviorSpeculator);

            behaviorSpeculator.MovementBehaviors.Add(
                new CompanionFollowPlayerBehavior() {
                    PathInterval = 0.25f,
                    DisableInCombat = true,
                    IdealRadius = 3f,
                    CatchUpRadius = 6f,
                    CatchUpAccelTime = 5,
                    CatchUpSpeed = 6,
                    CatchUpMaxSpeed = 10,
                    CatchUpAnimation = "dodge",
                    CatchUpOutAnimation = string.Empty,
                    IdleAnimations = new string[] { "idle" },
                    CanRollOverPits = true,
                    RollAnimation = "dodge",
                }
            );
            
            foreach (AttackBehaviorBase attackBehavior in behaviorSpeculator.AttackBehaviors) {
                if (attackBehavior is ShootGunBehavior) {
                    ShootGunBehavior shootGun = (attackBehavior as ShootGunBehavior);
                    shootGun.GroupCooldownVariance = 0;
                }
            }

            foreach (AttackBehaviorGroup.AttackGroupItem attackBehavior in behaviorSpeculator.AttackBehaviorGroup.AttackBehaviors) {
                if (attackBehavior.NickName == "Basic Shoot") { (attackBehavior.Behavior as ShootGunBehavior).GroupCooldownVariance = 0; }
            }

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = behaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>();
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);

            ExpandUtility.MakeCompanion(CachedCultistCompanion, null, null, true, false, true, false);

            CachedTargetEnemyObject.AddComponent<ExpandCompanionManager>();

            AddEnemyToDatabase(CachedTargetEnemyObject, FriendlyCultistGUID, false);
        }

        public static void BuildParasiteBossPrefab(out GameObject m_CachedTargetObject, bool isFakePrefab = true) {
            
            m_CachedTargetObject = UnityEngine.Object.Instantiate(EnemyDatabase.GetOrLoadByGuid("dc3cd41623d447aeba77c77c99598426").gameObject);
            m_CachedTargetObject.SetActive(false);

            m_CachedTargetObject.name = "BossParasite";

            AIActor m_TargetAIActor = m_CachedTargetObject.GetComponent<AIActor>();
            m_TargetAIActor.EnemyGuid = ParasiteBossGUID;
            m_TargetAIActor.EnemyId = UnityEngine.Random.Range(100000, 999999);

            m_TargetAIActor.ActorName = "Otherwordly Parasite";

            EncounterTrackable ParasiteEncounterable = m_CachedTargetObject.GetComponent<EncounterTrackable>();
            ParasiteEncounterable.EncounterGuid = "afa1216f84714d73af66d613627397cc";
            ParasiteEncounterable.journalData.PrimaryDisplayName = "Parasitic Abomination";
            ParasiteEncounterable.journalData.NotificationPanelDescription = "Parasitic horror inside the beast.";
            ParasiteEncounterable.journalData.AmmonomiconFullEntry = "This abomination may have been responsible for the rampage of the giant worm monster.";

            m_TargetAIActor.SetsFlagOnDeath = false;
            
            ExpandParasiteBossDeathController parasiteDeathController = m_CachedTargetObject.AddComponent<ExpandParasiteBossDeathController>();
            parasiteDeathController.explosionVfx = new List<GameObject>();
            foreach (GameObject vfxObject in m_CachedTargetObject.GetComponent<BossFinalMarineDeathController>().explosionVfx) {
                parasiteDeathController.explosionVfx.Add(vfxObject);
            }
            parasiteDeathController.explosionCount = 9;
            parasiteDeathController.explosionMidDelay = 0.15f;
            parasiteDeathController.bigExplosionVfx = new List<GameObject>();
            foreach (GameObject vfxObject in m_CachedTargetObject.GetComponent<BossFinalMarineDeathController>().bigExplosionVfx) {
                parasiteDeathController.bigExplosionVfx.Add(vfxObject);
            }
            parasiteDeathController.bigExplosionCount = 1;
            parasiteDeathController.bigExplosionMidDelay = 0.3f;

            UnityEngine.Object.Destroy(m_CachedTargetObject.GetComponent<BossFinalMarineDeathController>());
            
            GenericIntroDoer bossIntroDoer = m_CachedTargetObject.GetComponent<GenericIntroDoer>();
            bossIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
            bossIntroDoer.portraitSlideSettings.bossNameString = "Parasetic Abomination";
            bossIntroDoer.portraitSlideSettings.bossSubtitleString= "Parasite Horror";
            bossIntroDoer.portraitSlideSettings.bossQuoteString = string.Empty;

            ObjectVisibilityManager visiblityManager = m_CachedTargetObject.GetComponent<ObjectVisibilityManager>();
            visiblityManager.SuppressPlayerEnteredRoom = false;
            
            BehaviorSpeculator bossBehaviorSpeculator = m_CachedTargetObject.GetComponent<BehaviorSpeculator>();
            
            m_TargetAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;
            
            AttackBehaviorBase m_PortalBehavior = null;
            AttackBehaviorGroup.AttackGroupItem m_PortalBehaviorGroupItem = null;

            foreach (AttackBehaviorBase attackBehavior in bossBehaviorSpeculator.AttackBehaviors) {
                if (attackBehavior is BossFinalMarinePortalBehavior) {
                    m_PortalBehavior = attackBehavior;
                } else if (attackBehavior is TeleportBehavior) {
                    (attackBehavior as TeleportBehavior).ManuallyDefineRoom = false;
                    (attackBehavior as TeleportBehavior).roomMin = new Vector2(4, 4);
                    (attackBehavior as TeleportBehavior).roomMax = new Vector2(15, 15);
                }
            }
            
            foreach (AttackBehaviorGroup.AttackGroupItem behaviorGroupItem in bossBehaviorSpeculator.AttackBehaviorGroup.AttackBehaviors) {
                if (behaviorGroupItem.Behavior is BossFinalMarinePortalBehavior) {
                    m_PortalBehaviorGroupItem = behaviorGroupItem;
                } else if (behaviorGroupItem.NickName == "Frequent Teleport") {
                    (behaviorGroupItem.Behavior as TeleportBehavior).ManuallyDefineRoom = false;
                    (behaviorGroupItem.Behavior as TeleportBehavior).roomMin = new Vector2(4, 4);
                    (behaviorGroupItem.Behavior as TeleportBehavior).roomMax = new Vector2(15, 15);
                }
            }

            bossBehaviorSpeculator.AttackBehaviorGroup.AttackBehaviors.Remove(m_PortalBehaviorGroupItem);
            bossBehaviorSpeculator.AttackBehaviors.Remove(m_PortalBehavior);

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = bossBehaviorSpeculator;
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            m_TargetBehaviorSpeculatorSerialized.SaveState();
            bossBehaviorSpeculator.RegenerateCache();

            m_TargetAIActor.healthHaver.ForceSetCurrentHealth(550);
            m_TargetAIActor.healthHaver.SetHealthMaximum(1200);

            m_TargetAIActor.RegenerateCache();
            
            if (isFakePrefab) {
                AddEnemyToDatabase(m_CachedTargetObject, ParasiteBossGUID, true);
                FakePrefab.MarkAsFakePrefab(m_CachedTargetObject);
                UnityEngine.Object.DontDestroyOnLoad(m_CachedTargetObject);
            }
        }
        
        public static void BuildJungleBossPrefab(out GameObject m_CachedTargetObject, bool isFakePrefab = true) {
            
            m_CachedTargetObject = UnityEngine.Object.Instantiate(EnemyDatabase.GetOrLoadByGuid("880bbe4ce1014740ba6b4e2ea521e49d").gameObject);
            m_CachedTargetObject.SetActive(false);
            m_CachedTargetObject.name = "Com4nd0 Boss";

            AIActor m_TargetAIActor = m_CachedTargetObject.GetComponent<AIActor>();
            m_TargetAIActor.EnemyGuid = com4nd0GUID;
            m_TargetAIActor.EnemyId = UnityEngine.Random.Range(100000, 999999);

            m_TargetAIActor.ActorName = "Com4nd0";

            EncounterTrackable ParasiteEncounterable = m_CachedTargetObject.GetComponent<EncounterTrackable>();
            ParasiteEncounterable.EncounterGuid = "7330c08088cf4f8baf6a640d4f8f5c45";
            ParasiteEncounterable.journalData.PrimaryDisplayName = "Com4nd0";
            ParasiteEncounterable.journalData.NotificationPanelDescription = "The Lost Human";
            ParasiteEncounterable.journalData.AmmonomiconFullEntry = "This human was lost in the Jungle for many years and found refuge at an ancient temple ruins";

            m_TargetAIActor.SetsFlagOnDeath = false;
                      
            // UnityEngine.Object.Destroy(m_CachedTargetObject.GetComponent<BossFinalRobotIntroDoer>());
            UnityEngine.Object.Destroy(m_CachedTargetObject.GetComponent<BossFinalRobotDeathController>());

            GenericIntroDoer bossIntroDoer = m_CachedTargetObject.GetComponent<GenericIntroDoer>();
            bossIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
            bossIntroDoer.portraitSlideSettings.bossNameString = "Com4nd0";
            bossIntroDoer.portraitSlideSettings.bossSubtitleString= "Temple Defence";
            bossIntroDoer.portraitSlideSettings.bossQuoteString = string.Empty;
            // bossIntroDoer.BossMusicEvent = "Play_EX_MUS_ShotsFired_01";



            ObjectVisibilityManager visiblityManager = m_CachedTargetObject.GetComponent<ObjectVisibilityManager>();
            visiblityManager.SuppressPlayerEnteredRoom = false;
            
            BehaviorSpeculator bossBehaviorSpeculator = m_CachedTargetObject.GetComponent<BehaviorSpeculator>();
            
            m_TargetAIActor.PathableTiles = CellTypes.FLOOR;
            
            foreach (AttackBehaviorBase attackBehavior in bossBehaviorSpeculator.AttackBehaviors) {
                if (attackBehavior is SummonEnemyBehavior) {
                    (attackBehavior as SummonEnemyBehavior).ManuallyDefineRoom = false;
                    (attackBehavior as SummonEnemyBehavior).roomMin = Vector2.zero;
                    (attackBehavior as SummonEnemyBehavior).roomMax = Vector2.zero;
                    (attackBehavior as SummonEnemyBehavior).EnemeyGuids = new List<string>() { "39e6f47a16ab4c86bec4b12984aece4c", "05891b158cd542b1a5f3df30fb67a7ff", "6b7ef9e5d05b4f96b04f05ef4a0d1b18", "98fdf153a4dd4d51bf0bafe43f3c77ff" };
                } 
            }
            
            foreach (AttackBehaviorGroup.AttackGroupItem behaviorGroupItem in bossBehaviorSpeculator.AttackBehaviorGroup.AttackBehaviors) {
                if (behaviorGroupItem.NickName == "Teleport") {
                    (behaviorGroupItem.Behavior as TeleportBehavior).ManuallyDefineRoom = false;
                    (behaviorGroupItem.Behavior as TeleportBehavior).roomMin = Vector2.zero;
                    (behaviorGroupItem.Behavior as TeleportBehavior).roomMax = Vector2.zero;
                }
            }
            
            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = bossBehaviorSpeculator;
            // Loading a custom script from text file in place of one from an existing prefab..
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);
            m_TargetBehaviorSpeculatorSerialized.SaveState();
            bossBehaviorSpeculator.RegenerateCache();


            m_TargetAIActor.healthHaver.ForceSetCurrentHealth(725);
            m_TargetAIActor.healthHaver.SetHealthMaximum(725);

            m_TargetAIActor.RegenerateCache();

            if (isFakePrefab) {
                AddEnemyToDatabase(m_CachedTargetObject, com4nd0GUID, true);
                FakePrefab.MarkAsFakePrefab(m_CachedTargetObject);
                UnityEngine.Object.DontDestroyOnLoad(m_CachedTargetObject);                
            }
        }

        public static void BuildDoppelGunnerBossPrefab(AssetBundle expandSharedAssets1, out GameObject CachedTargetEnemyObject) {
            GameObject m_SelectedPlayer = ExpandAssets.LoadOfficialAsset<GameObject>("PlayerCoopCultist", ExpandAssets.AssetSource.BraveResources).transform.Find("PlayerSprite").gameObject;
            
            AIActor CachedEnemyActor = GetOfficialEnemyByGuid("57255ed50ee24794b7aac1ac3cfb8a95");
            AIActor CachedSpaceTurtle = GetOfficialEnemyByGuid("9216803e9c894002a4b931d7ea9c6bdf");
            GameObject m_DummyCorpseObject = null;

            CachedTargetEnemyObject = expandSharedAssets1.LoadAsset<GameObject>("Doppelgunner");
            
            tk2dSprite newSprite = CachedTargetEnemyObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateComponent(newSprite, m_SelectedPlayer.GetComponent<tk2dSprite>());

            // If Player sprite was flipped (aka, player aiming/facing towards the left), then this could cause sprite being shifted left on AIActor.
            // Always set false to ensure this doesn't happen.
            newSprite.FlipX = false;

            GameObject m_CachedGunAttachPoint = CachedTargetEnemyObject.transform.Find("GunAttachPoint").gameObject;

            ExpandUtility.DuplicateAIShooterAndAIBulletBank(CachedTargetEnemyObject, CachedSpaceTurtle.aiShooter, CachedSpaceTurtle.GetComponent<AIBulletBank>(), 24, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(CachedTargetEnemyObject, out m_DummyCorpseObject, "Doppelgunner", doppelgunnerbossEnemyGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: GetOfficialEnemyByGuid("88b6b6a93d4b4234a67844ef4728382c").CorpseObject, EnemyHasNoShooter: true);

            AIActor CachedDoppelGunnerBoss = CachedTargetEnemyObject.GetComponent<AIActor>();

            DopplegunnerHand = expandSharedAssets1.LoadAsset<GameObject>("DopplegunnerHand");
            tk2dSprite newHandSprite = DopplegunnerHand.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateComponent(newHandSprite, CachedSpaceTurtle.aiShooter.handObject.sprite);
            PlayerHandController m_HandController = DopplegunnerHand.AddComponent<PlayerHandController>();
            CachedDoppelGunnerBoss.aiShooter.handObject = m_HandController;
            CachedDoppelGunnerBoss.aiShooter.AllowTwoHands = true;

            CachedDoppelGunnerBoss.DoDustUps = true;
            CachedDoppelGunnerBoss.DustUpInterval = 0.4f;
            CachedDoppelGunnerBoss.MovementSpeed = 3.5f;
            CachedDoppelGunnerBoss.EnemySwitchState = "Gun Cultist";
            

            List<tk2dSpriteAnimationClip> m_AnimationClips = new List<tk2dSpriteAnimationClip>();
            foreach (tk2dSpriteAnimationClip clip in m_SelectedPlayer.GetComponent<tk2dSpriteAnimator>().Library.clips) {
                if (clip != null && !string.IsNullOrEmpty(clip.name)) {
                    if (clip.name.ToLower() == "idle") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "idle_backward") {
                        m_AnimationClips.Add(clip);
                    }  else if (clip.name.ToLower() == "dodge") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "dodge_bw") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "run_down") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "run_up") {
                        m_AnimationClips.Add(clip);
                    } else if(clip.name.ToLower() == "death") {
                        m_AnimationClips.Add(clip);
                    } else if(clip.name.ToLower() == "death_bw") {
                        m_AnimationClips.Add(clip);
                    } else if(clip.name.ToLower() == "pitfall") {
                        m_AnimationClips.Add(clip);
                    } else if(clip.name.ToLower() == "pitfall_down") {
                        m_AnimationClips.Add(clip);
                    }  
                }
            }
            if (!CachedDoppelGunnerBoss.spriteAnimator.Library) { CachedDoppelGunnerBoss.spriteAnimator.Library = CachedTargetEnemyObject.AddComponent<tk2dSpriteAnimation>(); }
            if (m_AnimationClips.Count > 0) { CachedDoppelGunnerBoss.spriteAnimator.Library.clips = m_AnimationClips.ToArray(); }
            CachedDoppelGunnerBoss.spriteAnimator.DefaultClipId = 0;
            CachedDoppelGunnerBoss.spriteAnimator.playAutomatically = true;

            if (CachedDoppelGunnerBoss.aiAnimator) {
                CachedDoppelGunnerBoss.aiAnimator.facingType = AIAnimator.FacingType.Movement;
                CachedDoppelGunnerBoss.aiAnimator.directionalType = AIAnimator.DirectionalType.Sprite;
                CachedDoppelGunnerBoss.aiAnimator.faceSouthWhenStopped = false;
                CachedDoppelGunnerBoss.aiAnimator.faceTargetWhenStopped = false;
                CachedDoppelGunnerBoss.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                CachedDoppelGunnerBoss.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                    Prefix = "idle",
                    AnimNames = new string[] { "idle_backward", "idle" },
                    Flipped = new DirectionalAnimation.FlipType[2],                    
                };
                CachedDoppelGunnerBoss.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                    Prefix = "run",
                    AnimNames = new string[] { "run_up", "run_down" },
                    Flipped = new DirectionalAnimation.FlipType[2],                    
                };
                CachedDoppelGunnerBoss.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "dodgeroll",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                            Prefix = "dodge",
                            AnimNames = new string[] { "dodge_bw", "dodge" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation() {
                        name = "pitfall",
                        anim = new DirectionalAnimation() {
                            Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                            Prefix = "pitfall",
                            AnimNames = new string[] { "pitfall", "pitfall_down" },
                            Flipped = new DirectionalAnimation.FlipType[2]
                        }
                    }
                };
            }
            
            ExpandUtility.DuplicateComponent(CachedDoppelGunnerBoss.healthHaver, GetOfficialEnemyByGuid("705e9081261446039e1ed9ff16905d04").healthHaver);

            string bossName = "Doppelgunner";
            GenericIntroDoer miniBossIntroDoer = CachedTargetEnemyObject.AddComponent<GenericIntroDoer>();
            CachedTargetEnemyObject.AddComponent<ExpandGungeoneerMimicIntroDoer>();
            miniBossIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
            miniBossIntroDoer.initialDelay = 0.15f;
            miniBossIntroDoer.cameraMoveSpeed = 14;
            miniBossIntroDoer.specifyIntroAiAnimator = null;
            miniBossIntroDoer.BossMusicEvent = "Play_MUS_Boss_Theme_Beholster";
            miniBossIntroDoer.PreventBossMusic = false;
            miniBossIntroDoer.InvisibleBeforeIntroAnim = false;
            miniBossIntroDoer.preIntroAnim = string.Empty;
            miniBossIntroDoer.preIntroDirectionalAnim = string.Empty;
            miniBossIntroDoer.introAnim = "idle";
            miniBossIntroDoer.introDirectionalAnim = string.Empty;
            miniBossIntroDoer.continueAnimDuringOutro = false;
            miniBossIntroDoer.cameraFocus = null;
            miniBossIntroDoer.roomPositionCameraFocus = Vector2.zero;
            miniBossIntroDoer.restrictPlayerMotionToRoom = false;
            miniBossIntroDoer.fusebombLock = false;
            miniBossIntroDoer.AdditionalHeightOffset = 0;
            miniBossIntroDoer.SkipBossCard = false;
            miniBossIntroDoer.portraitSlideSettings = new PortraitSlideSettings() {
                bossArtSprite = ExpandAssets.LoadAsset<Texture2D>("MimicInMirror_BossCardBackground"),
                bossNameString = bossName,
                bossSubtitleString = "Imposter!",
                bossQuoteString = "Clone gone rogue...",
                bossSpritePxOffset = IntVector2.Zero,
                topLeftTextPxOffset = IntVector2.Zero,
                bottomRightTextPxOffset = IntVector2.Zero,
                bgColor = new Color(0, 0, 1, 1)
            };
            CachedDoppelGunnerBoss.healthHaver.bossHealthBar = HealthHaver.BossBarType.MainBar;

            miniBossIntroDoer.HideGunAndHand = true;
            miniBossIntroDoer.SkipFinalizeAnimation = true;

            CachedDoppelGunnerBoss.BaseMovementSpeed = 8f;
            CachedDoppelGunnerBoss.MovementSpeed = 8f;

            CachedDoppelGunnerBoss.healthHaver.SetHealthMaximum(1000);
            CachedDoppelGunnerBoss.healthHaver.ForceSetCurrentHealth(1000);
            CachedDoppelGunnerBoss.healthHaver.overrideBossName = bossName;
            CachedDoppelGunnerBoss.OverrideDisplayName = bossName;
            CachedDoppelGunnerBoss.ActorName = bossName;
            CachedDoppelGunnerBoss.name = bossName;

            CachedDoppelGunnerBoss.CanTargetEnemies = false;
            CachedDoppelGunnerBoss.CanTargetPlayers = true;
            CachedDoppelGunnerBoss.spriteAnimator.DefaultClipId = 0;
            CachedDoppelGunnerBoss.spriteAnimator.playAutomatically = false;

            CachedTargetEnemyObject.AddComponent<ExpandGungeoneerMimicBossController>();
            CachedTargetEnemyObject.AddComponent<ExpandGungeoneerMimicDeathController>();

            if (CachedDoppelGunnerBoss.GetComponent<ExpandGungeoneerMimicIntroDoer>()) {                
                FieldInfo field = typeof(GenericIntroDoer).GetField("m_specificIntroDoer", BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(miniBossIntroDoer, CachedDoppelGunnerBoss.GetComponent<ExpandGungeoneerMimicIntroDoer>());
            }

            CachedDoppelGunnerBoss.aiAnimator.enabled = false;

            BehaviorSpeculator customBehaviorSpeculator = CachedTargetEnemyObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>(0);
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>(0);
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90f;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;
            customBehaviorSpeculator.RegenerateCache();

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = new List<string>(0);
            
            AddEnemyToDatabaseAndAmmonomicon(CachedDoppelGunnerBoss, doppelgunnerbossEnemyGUID, ExpandAmmonomiconDatabase.Doppelgunner, false);
        }

        public static void BuildBulletManBossPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("01972dee89fc4404a5c408d50007dad5"); //bullet_kin

            GameObject m_DummyCorpseObject = null;

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("BulletMan Boss");
            
            tk2dSprite m_CachedSprite = SpriteSerializer.AddSpriteToObject(m_CachedTargetObject, m_CachedEnemyActor.sprite.Collection, m_CachedEnemyActor.sprite.Collection.spriteDefinitions[363].name, tk2dBaseSprite.PerpendicularState.PERPENDICULAR);

            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, m_CachedEnemyActor.spriteAnimator.Library, 5, 0, playAutomatically: true, ClipFps: 0);
            
            GameObject m_CachedGunAttachPoint = m_CachedTargetObject.transform.Find("GunAttachPoint").gameObject;

            ExpandUtility.DuplicateAIShooterAndAIBulletBank(m_CachedTargetObject, m_CachedEnemyActor.aiShooter, m_CachedEnemyActor.GetComponent<AIBulletBank>(), 38, m_CachedGunAttachPoint.transform);

            ExpandUtility.GenerateAIActorTemplate(m_CachedTargetObject, out m_DummyCorpseObject, "BulletMan Boss", BulletManBossGUID, null, instantiateCorpseObject: false, ExternalCorpseObject: m_CachedEnemyActor.CorpseObject, EnemyHasNoShooter: true);
                        
            AIActor m_CachedAIActor = m_CachedTargetObject.GetComponent<AIActor>();

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            ExpandUtility.DuplicateComponent(m_CachedAIActor.aiAnimator, m_CachedEnemyActor.aiAnimator);


            m_CachedAIActor.aiAnimator.OtherAnimations.Add(
                new AIAnimator.NamedDirectionalAnimation() {
                    name = "cover_idle_left",
                    anim = new DirectionalAnimation() {
                        Type = DirectionalAnimation.DirectionType.None,
                        Prefix = "cover_idle_left",
                        AnimNames = new string[1],
                        Flipped = new DirectionalAnimation.FlipType[1]
                    }
                }
            );
            m_CachedAIActor.aiAnimator.OtherAnimations.Add(
                new AIAnimator.NamedDirectionalAnimation() {
                    name = "cover_leap_left",
                    anim = new DirectionalAnimation() {
                        Type = DirectionalAnimation.DirectionType.None,
                        Prefix = "cover_leap_left",
                        AnimNames = new string[1],
                        Flipped = new DirectionalAnimation.FlipType[1]
                    }
                }
            );
            m_CachedAIActor.aiAnimator.OtherAnimations.Add(
                new AIAnimator.NamedDirectionalAnimation() {
                    name = "spawn",
                    anim = new DirectionalAnimation() {
                        Type = DirectionalAnimation.DirectionType.None,
                        Prefix = "cover_leap_left",
                        AnimNames = new string[1],
                        Flipped = new DirectionalAnimation.FlipType[1]
                    }
                }
            );
            

            m_CachedAIActor.MovementSpeed = 2;
            m_CachedAIActor.PathableTiles = Dungeonator.CellTypes.FLOOR;

            m_CachedAIActor.specRigidbody.PixelColliders.Clear();

            ExpandUtility.DuplicateRigidBody(m_CachedAIActor.specRigidbody, m_CachedEnemyActor.specRigidbody);


            string bossName = "Just a normal Bullet Kin?";
            GenericIntroDoer bossIntroDoer = m_CachedAIActor.gameObject.AddComponent<GenericIntroDoer>();
            bossIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
            bossIntroDoer.initialDelay = 0.15f;
            bossIntroDoer.cameraMoveSpeed = 14;
            bossIntroDoer.specifyIntroAiAnimator = null;
            bossIntroDoer.BossMusicEvent = "Play_MUS_Boss_Theme_Beholster";
            bossIntroDoer.PreventBossMusic = false;
            bossIntroDoer.InvisibleBeforeIntroAnim = false;
            bossIntroDoer.preIntroDirectionalAnim = string.Empty;
            bossIntroDoer.preIntroAnim = "cover_idle_left";
            bossIntroDoer.introAnim = string.Empty;
            bossIntroDoer.introDirectionalAnim = string.Empty;
            bossIntroDoer.continueAnimDuringOutro = true;
            bossIntroDoer.cameraFocus = null;
            bossIntroDoer.roomPositionCameraFocus = Vector2.zero;
            bossIntroDoer.restrictPlayerMotionToRoom = false;
            bossIntroDoer.fusebombLock = false;
            bossIntroDoer.AdditionalHeightOffset = 0;
            bossIntroDoer.SkipBossCard = false;
            bossIntroDoer.portraitSlideSettings = new PortraitSlideSettings() {
                bossArtSprite = ExpandAssets.LoadAsset<Texture2D>("BulletMan_BossCard"),
                bossNameString = bossName,
                bossSubtitleString = "He's serious?",
                bossQuoteString = "Don't tell Him...",
                bossSpritePxOffset = IntVector2.Zero,
                topLeftTextPxOffset = IntVector2.Zero,
                bottomRightTextPxOffset = IntVector2.Zero,
                bgColor = new Color(0, 0, 1, 1)
            };
            bossIntroDoer.HideGunAndHand = false;
            bossIntroDoer.SkipFinalizeAnimation = false;
            
            m_CachedAIActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.MainBar;
            m_CachedAIActor.healthHaver.overrideBossName = bossName;
            m_CachedAIActor.OverrideDisplayName = bossName;
            
            m_CachedTargetObject.AddComponent<ExpandBulletManBossIntroDoer>();

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>() {
                new RedBarrelAwareness() { AvoidRedBarrels = true, ShootRedBarrels = true, PushRedBarrels = true }
            };
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>() {
                new TargetPlayerBehavior() {
                    Radius = 35,
                    LineOfSight = true,
                    ObjectPermanence = true,
                    SearchInterval = 0.25f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0.25f
                }
            };

            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() {
                new RideInCartsBehavior(),
                new TakeCoverBehavior() {
                    PathInterval = 0.25f,
                    LineOfSightToLeaveCover = true,
                    MaxCoverDistance = 10,
                    MaxCoverDistanceToTarget = 25,
                    FlipCoverDistance = 0.3f,
                    InsideCoverTime = 2,
                    OutsideCoverTime = 2,
                    PopOutSpeedMultiplier = 3,
                    PopInSpeedMultiplier = 1,
                    InitialCoverChance = 0.9f,
                    RepeatingCoverChance = 0.15f,
                    RepeatingCoverInterval = 1
                },
                new SeekTargetBehavior() {
                    StopWhenInRange = true,
                    CustomRange = 7,
                    LineOfSight = true,
                    ReturnToSpawn = true,
                    SpawnTetherDistance = 0,
                    PathInterval = 0.25f,
                    SpecifyRange = false,
                    MinActiveRange = 0,
                    MaxActiveRange = 0
                }
            };
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootGunBehavior() {
                    GroupCooldownVariance = 0.2f,
                    LineOfSight = true,
                    WeaponType = WeaponType.AIShooterProjectile,
                    OverrideBulletName = "default",
                    BulletScript = null,
                    FixTargetDuringAttack = false,
                    StopDuringAttack = false,
                    LeadAmount = 0,
                    LeadChance = 1,
                    RespectReload = true,
                    MagazineCapacity = 6,
                    ReloadSpeed = 2,
                    EmptiesClip = false,
                    SuppressReloadAnim = false,
                    TimeBetweenShots = -1,
                    PreventTargetSwitching = false,
                    OverrideAnimation = null,
                    OverrideDirectionalAnimation = null,
                    HideGun = false,
                    UseLaserSight = false,
                    UseGreenLaser = false,
                    PreFireLaserTime = -1,
                    AimAtFacingDirectionWhenSafe = false,
                    Cooldown = 1.6f,
                    CooldownVariance = 0,
                    AttackCooldown = 0,
                    GlobalCooldown = 0,
                    InitialCooldown = 0,
                    InitialCooldownVariance = 0,
                    GroupName = null,
                    GroupCooldown = 0,
                    MinRange = 0,
                    Range = 12,
                    MinWallDistance = 0,
                    MaxEnemiesInRoom = 0,
                    MinHealthThreshold = 0,
                    MaxHealthThreshold = 1,
                    HealthThresholds = new float[0],
                    AccumulateHealthThresholds = true,
                    targetAreaStyle = null,
                    IsBlackPhantom = false,
                    resetCooldownOnDamage = null,
                    RequiresLineOfSight = false,
                    MaxUsages = 0
                }
            };

            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0.5f;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;
                        
            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>(0);
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);


            ExplodeOnDeath m_Exploder = m_CachedTargetObject.AddComponent<ExplodeOnDeath>();
            m_Exploder.explosionData = ExpandUtility.GenerateExplosionData();
            m_Exploder.immuneToIBombApp = true;
            m_Exploder.LinearChainExplosion = false;
            m_Exploder.deathType = OnDeathBehavior.DeathType.Death;
            m_Exploder.preDeathDelay = 0.1f;

            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, BulletManBossGUID, ExpandAmmonomiconDatabase.BulletManBoss);
            
            m_CachedEnemyActor = null;
            return;
        }
        
        public static void BuildPoisbulordBossPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("1b5810fafbec445d89921a4efb4e42b7"); // blobulord
                        
            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("Poisbulord");
            VFX_Poisbulord_Splash = expandSharedAssets1.LoadAsset<GameObject>("VFX_Poisbulord_Splash");
            VFX_Poisbulord_Die_Big = expandSharedAssets1.LoadAsset<GameObject>("VFX_Poisbulord_Die_Big");
            VFX_Poisbulord_Die_Small = expandSharedAssets1.LoadAsset<GameObject>("VFX_Poisbulord_Die_Small");

            GameObject m_CachedShootPoint = m_CachedTargetObject.transform.Find("shoot point").gameObject;
            GameObject m_CachedSplashVFX = m_CachedTargetObject.transform.Find("splash vfx").gameObject;


            ExpandUtility.DuplicateRigidBody(m_CachedTargetObject.AddComponent<SpeculativeRigidbody>(), m_CachedEnemyActor.specRigidbody);

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            AIActor m_CachedAIActor = m_CachedTargetObject.AddComponent<AIActor>();
            ExpandUtility.DuplicateComponent(m_CachedAIActor, m_CachedEnemyActor);
            m_CachedAIActor.ActorName = "Poisbulord";
            m_CachedAIActor.EnemyGuid = PoisbulordGUID;
            m_CachedAIActor.EnemyId = UnityEngine.Random.Range(11000, 99999);

            m_CachedAIActor.EffectResistances = new ActorEffectResistance[] {
                new ActorEffectResistance() { resistAmount = 1, resistType = EffectResistanceType.Poison },
            };

            

            ExpandUtility.DuplicateComponent(m_CachedTargetObject.AddComponent<AIBulletBank>(), m_CachedEnemyActor.bulletBank);
            ExpandUtility.DuplicateComponent(m_CachedTargetObject.GetOrAddComponent<HitEffectHandler>(), m_CachedEnemyActor.hitEffectHandler);
            ExpandUtility.DuplicateComponent(m_CachedTargetObject.AddComponent<HealthHaver>(), m_CachedEnemyActor.healthHaver);
            ExpandUtility.DuplicateComponent(m_CachedTargetObject.AddComponent<KnockbackDoer>(), m_CachedEnemyActor.knockbackDoer);

            ExpandUtility.DuplicateSprite(m_CachedTargetObject.AddComponent<tk2dSprite>(), m_CachedEnemyActor.gameObject.GetComponent<tk2dSprite>());
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, m_CachedEnemyActor.spriteAnimator.Library, 8, 0, playAutomatically: true, ClipFps: 0);
            

            tk2dSpriteAnimation m_SourceBlobulorAnimationLibrary = m_CachedEnemyActor.spriteAnimator.Library;
            tk2dSpriteAnimation m_PoisbulordLibrary = m_CachedTargetObject.AddComponent<tk2dSpriteAnimation>();
            ExpandUtility.DuplicateComponent(m_PoisbulordLibrary, m_SourceBlobulorAnimationLibrary);

            List<tk2dSpriteAnimationClip> m_Clips = new List<tk2dSpriteAnimationClip>();

            foreach (tk2dSpriteAnimationClip clip in m_SourceBlobulorAnimationLibrary.clips) {
                m_Clips.Add(ExpandUtility.DuplicateAnimationClip(clip));
            }

            foreach (tk2dSpriteAnimationClip clip in m_Clips) {
                foreach (tk2dSpriteAnimationFrame frame in clip.frames) {
                    frame.spriteCollection = ExpandPrefabs.EXPoisbulordCollection.GetComponent<tk2dSpriteCollectionData>();
                }
            }
            
            if (m_Clips.Count > 0) m_PoisbulordLibrary.clips = m_Clips.ToArray();

            m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>().Library = m_PoisbulordLibrary;
            

            ExpandUtility.DuplicateComponent(m_CachedTargetObject.AddComponent<AIAnimator>(), m_CachedEnemyActor.aiAnimator);
            ExpandUtility.DuplicateComponent(m_CachedTargetObject.AddComponent<ObjectVisibilityManager>(), m_CachedEnemyActor.visibilityManager);
            
            AIAnimator m_PoisbulordAIAnimator = m_CachedTargetObject.GetComponent<AIAnimator>();
            m_PoisbulordAIAnimator.OtherVFX[0].anchorTransform = m_CachedSplashVFX.transform;
            m_PoisbulordAIAnimator.OtherVFX[0].vfxPool = new VFXPool() {
                type = VFXPoolType.Single,
                effects = new VFXComplex[] {
                    new VFXComplex() {
                        effects = new VFXObject[] {
                            new VFXObject() {
                                effect = VFX_Poisbulord_Splash,
                                orphaned = false,
                                attached = false,
                                persistsOnDeath = false,
                                usesZHeight = false,
                                zHeight = 0,
                                alignment = VFXAlignment.Fixed,
                                destructible = false
                            }
                        }
                    }
                }
            };

            GameObject m_SourceVFXSplashObject = m_CachedEnemyActor.aiAnimator.OtherVFX[0].vfxPool.effects[0].effects[0].effect;

            ExpandUtility.DuplicateSprite(VFX_Poisbulord_Splash.AddComponent<tk2dSprite>(), m_SourceVFXSplashObject.GetComponent<tk2dSprite>());
            ExpandUtility.DuplicateComponent(VFX_Poisbulord_Splash.AddComponent<tk2dSpriteAnimator>(), m_SourceVFXSplashObject.GetComponent<tk2dSpriteAnimator>());

            tk2dSpriteAnimator m_VFXPoisbulordSplashAnimator = VFX_Poisbulord_Splash.GetComponent<tk2dSpriteAnimator>();
            m_VFXPoisbulordSplashAnimator.Library = m_PoisbulordLibrary;
            m_VFXPoisbulordSplashAnimator.DefaultClipId = 24;

            ExpandUtility.DuplicateComponent(VFX_Poisbulord_Splash.AddComponent<SpriteAnimatorKiller>(), m_SourceVFXSplashObject.GetComponent<SpriteAnimatorKiller>());
            


            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 2;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>();
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>();
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>();
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>();
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);

            foreach (TargetBehaviorBase targetBehavior in m_CachedEnemyActor.behaviorSpeculator.TargetBehaviors) {
                customBehaviorSpeculator.TargetBehaviors.Add(targetBehavior);
            }
            foreach (MovementBehaviorBase movementBehavior in m_CachedEnemyActor.behaviorSpeculator.MovementBehaviors) {
                customBehaviorSpeculator.MovementBehaviors.Add(movementBehavior);
            }
            foreach (AttackBehaviorBase attackBehavior in m_CachedEnemyActor.behaviorSpeculator.AttackBehaviors) {
                if (attackBehavior is AttackBehaviorGroup) {
                    AttackBehaviorGroup attackGroup = (attackBehavior as AttackBehaviorGroup);
                    AttackBehaviorGroup newAttackGroup = new AttackBehaviorGroup();
                    newAttackGroup.ShareCooldowns = attackGroup.ShareCooldowns;
                    newAttackGroup.AttackBehaviors = new List<AttackBehaviorGroup.AttackGroupItem>();
                    foreach (AttackBehaviorGroup.AttackGroupItem behaviorItem in attackGroup.AttackBehaviors) {
                        AttackBehaviorGroup.AttackGroupItem newBehaviorItem = new AttackBehaviorGroup.AttackGroupItem();
                        ExpandUtility.DuplicateComponent(newBehaviorItem, behaviorItem);
                        if (behaviorItem.NickName == "Move and Shoot" && (behaviorItem.Behavior is ShootBehavior)) {
                            ShootBehavior newShootBehavior = new ShootBehavior();
                            ExpandUtility.DuplicateComponent(newShootBehavior, (behaviorItem.Behavior as ShootBehavior));
                            newShootBehavior.ShootPoint = m_CachedShootPoint;
                            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences.Add(newShootBehavior.ShootPoint);
                            newBehaviorItem.Behavior = newShootBehavior;
                            newAttackGroup.AttackBehaviors.Add(newBehaviorItem);
                        } else if (behaviorItem.NickName == "Bouncing Blob Bullets" && (behaviorItem.Behavior is ShootBehavior)) {
                            ShootBehavior newShootBehavior = new ShootBehavior();
                            ExpandUtility.DuplicateComponent(newShootBehavior, (behaviorItem.Behavior as ShootBehavior));
                            newShootBehavior.ShootPoint = m_CachedShootPoint;
                            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences.Add(newShootBehavior.ShootPoint);
                            newBehaviorItem.Behavior = newShootBehavior;
                            newAttackGroup.AttackBehaviors.Add(newBehaviorItem);
                        } else if (behaviorItem.NickName == "Firehose" && (behaviorItem.Behavior is ShootBehavior)) {
                            ShootBehavior newShootBehavior = new ShootBehavior();
                            ExpandUtility.DuplicateComponent(newShootBehavior, (behaviorItem.Behavior as ShootBehavior));
                            newShootBehavior.ShootPoint = m_CachedShootPoint;
                            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences.Add(newShootBehavior.ShootPoint);
                            newBehaviorItem.Behavior = newShootBehavior;
                            newAttackGroup.AttackBehaviors.Add(newBehaviorItem);
                        } else if (behaviorItem.NickName == "Moving Spray" && (behaviorItem.Behavior is ShootBehavior)) {
                            ShootBehavior newShootBehavior = new ShootBehavior();
                            ExpandUtility.DuplicateComponent(newShootBehavior, (behaviorItem.Behavior as ShootBehavior));
                            newShootBehavior.ShootPoint = m_CachedShootPoint;
                            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences.Add(newShootBehavior.ShootPoint);
                            newBehaviorItem.Behavior = newShootBehavior;
                            newAttackGroup.AttackBehaviors.Add(newBehaviorItem);
                        } else if (behaviorItem.NickName == "Split Apart" && behaviorItem.Behavior is SequentialAttackBehaviorGroup) {
                            SequentialAttackBehaviorGroup sequentialAttackBehaviorGroup = (behaviorItem.Behavior as SequentialAttackBehaviorGroup);
                            SequentialAttackBehaviorGroup newSequentialAttackBehaviorGroup = new SequentialAttackBehaviorGroup();
                            newSequentialAttackBehaviorGroup.OverrideCooldowns = sequentialAttackBehaviorGroup.OverrideCooldowns;
                            newSequentialAttackBehaviorGroup.RunInClass = sequentialAttackBehaviorGroup.RunInClass;
                            newSequentialAttackBehaviorGroup.AttackBehaviors = new List<AttackBehaviorBase>();
                            foreach (AttackBehaviorBase sequentialbehavior in sequentialAttackBehaviorGroup.AttackBehaviors) {
                                if (sequentialbehavior is AttackMoveBehavior) {
                                    AttackMoveBehavior newAttackMoveBehavior = new AttackMoveBehavior();
                                    ExpandUtility.DuplicateComponent(newAttackMoveBehavior, (sequentialbehavior as AttackMoveBehavior));
                                    newSequentialAttackBehaviorGroup.AttackBehaviors.Add(newAttackMoveBehavior);
                                } else if (sequentialbehavior is TransformBehavior) {
                                    TransformBehavior newTransformBehavior = new TransformBehavior();
                                    ExpandUtility.DuplicateComponent(newTransformBehavior, (sequentialbehavior as TransformBehavior));
                                    newTransformBehavior.shootPoint = m_CachedShootPoint;
                                    m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences.Add(newTransformBehavior.shootPoint);
                                    newSequentialAttackBehaviorGroup.AttackBehaviors.Add(newTransformBehavior);
                                }
                            }
                            newBehaviorItem.Behavior = newSequentialAttackBehaviorGroup;
                            newAttackGroup.AttackBehaviors.Add(newBehaviorItem);
                        } else if (behaviorItem.NickName == "Slam Burst " && (behaviorItem.Behavior is ShootBehavior)) {
                            ShootBehavior newShootBehavior = new ShootBehavior();
                            ExpandUtility.DuplicateComponent(newShootBehavior, (behaviorItem.Behavior as ShootBehavior));
                            newShootBehavior.ShootPoint = m_CachedShootPoint;
                            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences.Add(newShootBehavior.ShootPoint);
                            newBehaviorItem.Behavior = newShootBehavior;
                            newAttackGroup.AttackBehaviors.Add(newBehaviorItem);
                        }
                    }
                    customBehaviorSpeculator.AttackBehaviors.Add(newAttackGroup);
                }
            }
                        

            string bossName = "POISBULORD";
            GenericIntroDoer bossIntroDoer = m_CachedTargetObject.AddComponent<GenericIntroDoer>();
            ExpandUtility.DuplicateComponent(bossIntroDoer, m_CachedTargetObject.GetComponent<GenericIntroDoer>());
            bossIntroDoer.portraitSlideSettings = new PortraitSlideSettings() {
                bossArtSprite = ExpandAssets.LoadAsset<Texture2D>("Poisbulord_boss_Bosscard_001"),
                bossNameString = bossName,
                bossSubtitleString = "FIVE-STAR GENERAL",
                // bossQuoteString = string.Empty,
            };

            m_CachedTargetObject.AddComponent<BlobulordIntroDoer>();

            BlobulordDeathController m_SourceDeathController = m_CachedEnemyActor.gameObject.GetComponent<BlobulordDeathController>();
            BlobulordDeathController blobulordDeathController = m_CachedTargetObject.AddComponent<BlobulordDeathController>();
            blobulordDeathController.name = string.Empty;
            blobulordDeathController.bigExplosionVfx = VFX_Poisbulord_Die_Big;
            blobulordDeathController.explosionVfx = new List<GameObject>() { VFX_Poisbulord_Die_Small };
            blobulordDeathController.explosionMidDelay = 0.12f;
            blobulordDeathController.explosionCount = 25;
            blobulordDeathController.finalScale = 0.2f;
            blobulordDeathController.crawlerSpawnDelay = 0.3f;
            blobulordDeathController.crawlerGuid = PoisbulordCrawlerGUID;

            ExpandUtility.DuplicateSprite(VFX_Poisbulord_Die_Big.AddComponent<tk2dSprite>(), m_SourceDeathController.bigExplosionVfx.GetComponent<tk2dSprite>());
            ExpandUtility.DuplicateComponent(VFX_Poisbulord_Die_Big.AddComponent<tk2dSpriteAnimator>(), m_SourceDeathController.bigExplosionVfx.GetComponent<tk2dSpriteAnimator>());

            tk2dSpriteAnimator m_VFXPoisbulordDieBigAnimator = VFX_Poisbulord_Die_Big.GetComponent<tk2dSpriteAnimator>();
            m_VFXPoisbulordDieBigAnimator.Library = m_PoisbulordLibrary;
            m_VFXPoisbulordDieBigAnimator.DefaultClipId = 25;

            ExpandUtility.DuplicateComponent(VFX_Poisbulord_Die_Big.AddComponent<SpriteAnimatorKiller>(), m_SourceDeathController.bigExplosionVfx.GetComponent<SpriteAnimatorKiller>());
            

            ExpandUtility.DuplicateSprite(VFX_Poisbulord_Die_Small.AddComponent<tk2dSprite>(), m_SourceDeathController.explosionVfx[0].GetComponent<tk2dSprite>());
            ExpandUtility.DuplicateComponent(VFX_Poisbulord_Die_Small.AddComponent<tk2dSpriteAnimator>(), m_SourceDeathController.explosionVfx[0].GetComponent<tk2dSpriteAnimator>());

            tk2dSpriteAnimator m_VFXPoisbulordDieSmallAnimator = VFX_Poisbulord_Die_Small.GetComponent<tk2dSpriteAnimator>();
            m_VFXPoisbulordDieSmallAnimator.Library = m_PoisbulordLibrary;
            m_VFXPoisbulordDieSmallAnimator.DefaultClipId = 26;

            ExpandUtility.DuplicateComponent(VFX_Poisbulord_Die_Small.AddComponent<SpriteAnimatorKiller>(), m_SourceDeathController.explosionVfx[0].GetComponent<SpriteAnimatorKiller>());

            
            tk2dSpriteAttachPoint spriteAttachPoint = m_CachedTargetObject.AddComponent<tk2dSpriteAttachPoint>();
            spriteAttachPoint.attachPoints = new List<Transform>() { m_CachedShootPoint.transform };
            spriteAttachPoint.deactivateUnusedAttachPoints = false;
            spriteAttachPoint.disableEmissionOnUnusedParticleSystems = false;
            spriteAttachPoint.ignorePosition = false;
            spriteAttachPoint.ignoreScale = false;
            spriteAttachPoint.ignoreRotation = false;
            spriteAttachPoint.centerUnusedAttachPoints = false;

            
            GoopDoer[] m_SourceGoopDoers = m_CachedEnemyActor.gameObject.GetComponents<GoopDoer>();

            GoopDoer m_GoopDoer1 = m_CachedTargetObject.AddComponent<GoopDoer>();
            GoopDoer m_GoopDoer2 = m_CachedTargetObject.AddComponent<GoopDoer>();

            ExpandUtility.DuplicateComponent(m_GoopDoer1, m_SourceGoopDoers[0]);
            ExpandUtility.DuplicateComponent(m_GoopDoer2, m_SourceGoopDoers[1]);

            
            m_GoopDoer1.goopDefinition = ExpandAssets.LoadOfficialAsset<GoopDefinition>("Poison Goop", ExpandAssets.AssetSource.SharedAuto1);
            m_GoopDoer2.goopDefinition = m_GoopDoer1.goopDefinition;
            m_GoopDoer2.DispersalParticleSystemPrefab = m_SourceGoopDoers[1].DispersalParticleSystemPrefab;


            Poisbulord_Poisbulon_Projectile = expandSharedAssets1.LoadAsset<GameObject>("Poisbulord_Poisbulon_Projectile");
            Poisbulord_Poisbuloid_Projectile = expandSharedAssets1.LoadAsset<GameObject>("Poisbulord_Poisbuloid_Projectile");
            // Poisbulord_Poisbulin_Projectile = expandSharedAssets1.LoadAsset<GameObject>("Poisbulord_Poisbulin_Projectile");

            ExpandUtility.DuplicateComponent(Poisbulord_Poisbulon_Projectile.AddComponent<SpeculativeRigidbody>(), m_CachedEnemyActor.bulletBank.Bullets[4].BulletObject.GetComponent<SpeculativeRigidbody>());
            ExpandUtility.DuplicateComponent(Poisbulord_Poisbuloid_Projectile.AddComponent<SpeculativeRigidbody>(), m_CachedEnemyActor.bulletBank.Bullets[5].BulletObject.GetComponent<SpeculativeRigidbody>());
            // ExpandUtility.DuplicateComponent(Poisbulord_Poisbulin_Projectile.AddComponent<SpeculativeRigidbody>(), m_CachedEnemyActor.bulletBank.Bullets[6].BulletObject.GetComponent<SpeculativeRigidbody>());

            ExpandUtility.DuplicateComponent(Poisbulord_Poisbulon_Projectile.AddComponent<Projectile>(), m_CachedEnemyActor.bulletBank.Bullets[4].BulletObject.GetComponent<Projectile>());
            ExpandUtility.DuplicateComponent(Poisbulord_Poisbuloid_Projectile.AddComponent<Projectile>(), m_CachedEnemyActor.bulletBank.Bullets[5].BulletObject.GetComponent<Projectile>());
            // ExpandUtility.DuplicateComponent(Poisbulord_Poisbulin_Projectile.AddComponent<Projectile>(), m_CachedEnemyActor.bulletBank.Bullets[6].BulletObject.GetComponent<Projectile>());

            ExpandUtility.DuplicateComponent(Poisbulord_Poisbulon_Projectile.transform.Find("Sprite").gameObject.AddComponent<tk2dSprite>(), m_CachedEnemyActor.bulletBank.Bullets[4].BulletObject.transform.Find("Sprite").gameObject.GetComponent<tk2dSprite>());
            ExpandUtility.DuplicateComponent(Poisbulord_Poisbuloid_Projectile.transform.Find("Sprite").gameObject.AddComponent<tk2dSprite>(), m_CachedEnemyActor.bulletBank.Bullets[5].BulletObject.transform.Find("Sprite").gameObject.GetComponent<tk2dSprite>());
            // ExpandUtility.DuplicateComponent(Poisbulord_Poisbulin_Projectile.transform.Find("Sprite").gameObject.AddComponent<tk2dSprite>(), m_CachedEnemyActor.bulletBank.Bullets[6].BulletObject.transform.Find("Sprite").gameObject.GetComponent<tk2dSprite>());
            ExpandUtility.DuplicateComponent(Poisbulord_Poisbulon_Projectile.transform.Find("Sprite").gameObject.AddComponent<tk2dSpriteAnimator>(), m_CachedEnemyActor.bulletBank.Bullets[4].BulletObject.transform.Find("Sprite").gameObject.GetComponent<tk2dSpriteAnimator>());
            ExpandUtility.DuplicateComponent(Poisbulord_Poisbuloid_Projectile.transform.Find("Sprite").gameObject.AddComponent<tk2dSpriteAnimator>(), m_CachedEnemyActor.bulletBank.Bullets[5].BulletObject.transform.Find("Sprite").gameObject.GetComponent<tk2dSpriteAnimator>());
            // ExpandUtility.DuplicateComponent(Poisbulord_Poisbulin_Projectile.transform.Find("Sprite").gameObject.AddComponent<tk2dSpriteAnimator>(), m_CachedEnemyActor.bulletBank.Bullets[6].BulletObject.transform.Find("Sprite").gameObject.GetComponent<tk2dSpriteAnimator>());


            
            GoopDoer m_ProjectileGoopDoer1 = Poisbulord_Poisbulon_Projectile.transform.Find("Sprite").gameObject.AddComponent<GoopDoer>();
            GoopDoer m_ProjectileGoopDoer2 = Poisbulord_Poisbuloid_Projectile.transform.Find("Sprite").gameObject.AddComponent<GoopDoer>();
            // GoopDoer m_ProjectileGoopDoer3 = Poisbulord_Poisbulin_Projectile.transform.Find("Sprite").gameObject.AddComponent<GoopDoer>();
            ExpandUtility.DuplicateComponent(m_ProjectileGoopDoer1, m_SourceGoopDoers[0]);
            ExpandUtility.DuplicateComponent(m_ProjectileGoopDoer2, m_SourceGoopDoers[0]);
            // ExpandUtility.DuplicateComponent(m_ProjectileGoopDoer3, m_SourceGoopDoers[0]);
            m_ProjectileGoopDoer1.goopDefinition = m_GoopDoer1.goopDefinition;
            m_ProjectileGoopDoer2.goopDefinition = m_GoopDoer1.goopDefinition;
            // m_ProjectileGoopDoer3.goopDefinition = m_GoopDoer1.goopDefinition;
            m_ProjectileGoopDoer1.positionSource = GoopDoer.PositionSource.SpriteCenter;
            m_ProjectileGoopDoer2.positionSource = GoopDoer.PositionSource.SpriteCenter;
            // m_ProjectileGoopDoer3.positionSource = GoopDoer.PositionSource.SpriteCenter;
            m_ProjectileGoopDoer1.updateFrequency = 0.05f;
            m_ProjectileGoopDoer2.updateFrequency = 0.05f;
            // m_ProjectileGoopDoer3.updateFrequency = 0.05f;
            m_ProjectileGoopDoer1.defaultGoopRadius = 0.8f;
            m_ProjectileGoopDoer2.defaultGoopRadius = 0.6f;
            // m_ProjectileGoopDoer3.defaultGoopRadius = 0.45f;

            m_CachedAIActor.bulletBank.Bullets[4].BulletObject = Poisbulord_Poisbulon_Projectile;
            m_CachedAIActor.bulletBank.Bullets[5].BulletObject = Poisbulord_Poisbuloid_Projectile;
            // m_CachedAIActor.bulletBank.Bullets[6].BulletObject = Poisbulord_Poisbulin_Projectile;

            /*VFX_Poisbulord_Splash.AddComponent<ExpandObjectReshader>();
            VFX_Poisbulord_Die_Big.AddComponent<ExpandObjectReshader>();
            VFX_Poisbulord_Die_Small.AddComponent<ExpandObjectReshader>();
            m_CachedTargetObject.AddComponent<ExpandObjectReshader>();*/

            AddEnemyToDatabaseAndAmmonomicon(m_CachedAIActor, PoisbulordGUID, ExpandAmmonomiconDatabase.Poisbulord);

            m_CachedEnemyActor = null;
            return;
        }
        
        public static void BuildPoisbulordCrawlerPrefab(AssetBundle expandSharedAssets1, out GameObject m_CachedTargetObject) {
            AIActor m_CachedEnemyActor = GetOfficialEnemyByGuid("d1c9781fdac54d9e8498ed89210a0238"); // blobulord crawler

            m_CachedTargetObject = expandSharedAssets1.LoadAsset<GameObject>("PoisbulordCrawler");
            
            ExpandUtility.DuplicateRigidBody(m_CachedTargetObject.AddComponent<SpeculativeRigidbody>(), m_CachedEnemyActor.specRigidbody);

            if (!m_CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for donor enemy is null!", false);
                return;
            }

            AIActor m_CachedAIActor = m_CachedTargetObject.AddComponent<AIActor>();
            ExpandUtility.DuplicateComponent(m_CachedAIActor, m_CachedEnemyActor);
            m_CachedAIActor.ActorName = "Poisbulord Remains";
            m_CachedAIActor.EnemyGuid = PoisbulordCrawlerGUID;
            m_CachedAIActor.EnemyId = UnityEngine.Random.Range(11000, 99999);

            m_CachedAIActor.EffectResistances = new ActorEffectResistance[] {
                new ActorEffectResistance() { resistAmount = 1, resistType = EffectResistanceType.Poison },
            };
            
                        
            ExpandUtility.DuplicateComponent(m_CachedTargetObject.GetOrAddComponent<HitEffectHandler>(), m_CachedEnemyActor.hitEffectHandler);
            ExpandUtility.DuplicateComponent(m_CachedTargetObject.AddComponent<HealthHaver>(), m_CachedEnemyActor.healthHaver);
            ExpandUtility.DuplicateComponent(m_CachedTargetObject.AddComponent<KnockbackDoer>(), m_CachedEnemyActor.knockbackDoer);

            ExpandUtility.DuplicateSprite(m_CachedTargetObject.AddComponent<tk2dSprite>(), m_CachedEnemyActor.gameObject.GetComponent<tk2dSprite>());
            ExpandUtility.GenerateSpriteAnimator(m_CachedTargetObject, m_CachedEnemyActor.spriteAnimator.Library, 12, 0, playAutomatically: true, ClipFps: 0);


            tk2dSpriteAnimation m_SourceBlobulorCrawlerAnimationLibrary = m_CachedEnemyActor.spriteAnimator.Library;
            tk2dSpriteAnimation m_PoisbulordCrawlerLibrary = m_CachedTargetObject.AddComponent<tk2dSpriteAnimation>();
            ExpandUtility.DuplicateComponent(m_PoisbulordCrawlerLibrary, m_SourceBlobulorCrawlerAnimationLibrary);

            List<tk2dSpriteAnimationClip> m_Clips = new List<tk2dSpriteAnimationClip>();

            foreach (tk2dSpriteAnimationClip clip in m_SourceBlobulorCrawlerAnimationLibrary.clips) {
                m_Clips.Add(ExpandUtility.DuplicateAnimationClip(clip));
            }

            foreach (tk2dSpriteAnimationClip clip in m_Clips) {
                foreach (tk2dSpriteAnimationFrame frame in clip.frames) {
                    frame.spriteCollection = ExpandPrefabs.EXPoisbulordCollection.GetComponent<tk2dSpriteCollectionData>();
                }
            }

            if (m_Clips.Count > 0) m_PoisbulordCrawlerLibrary.clips = m_Clips.ToArray();

            m_CachedTargetObject.GetComponent<tk2dSpriteAnimator>().Library = m_PoisbulordCrawlerLibrary;


            ExpandUtility.DuplicateComponent(m_CachedTargetObject.AddComponent<AIAnimator>(), m_CachedEnemyActor.aiAnimator);
            ExpandUtility.DuplicateComponent(m_CachedTargetObject.AddComponent<ObjectVisibilityManager>(), m_CachedEnemyActor.visibilityManager);
            

            BehaviorSpeculator customBehaviorSpeculator = m_CachedTargetObject.AddComponent<BehaviorSpeculator>();
            customBehaviorSpeculator.InstantFirstTick = false;
            customBehaviorSpeculator.TickInterval = 0.1f;
            customBehaviorSpeculator.PostAwakenDelay = 0;
            customBehaviorSpeculator.RemoveDelayOnReinforce = false;
            customBehaviorSpeculator.OverrideStartingFacingDirection = false;
            customBehaviorSpeculator.StartingFacingDirection = -90;
            customBehaviorSpeculator.SkipTimingDifferentiator = false;
            customBehaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
            customBehaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>();
            customBehaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>();
            customBehaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>(0);
            customBehaviorSpeculator.OtherBehaviors = new List<BehaviorBase>(0);

            ISerializedObject m_TargetBehaviorSpeculatorSerialized = customBehaviorSpeculator;
            m_TargetBehaviorSpeculatorSerialized.SerializedObjectReferences = new List<UnityEngine.Object>();
            m_TargetBehaviorSpeculatorSerialized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
            m_TargetBehaviorSpeculatorSerialized.SerializedStateValues = new List<string>(0);

            foreach (TargetBehaviorBase targetBehavior in m_CachedEnemyActor.behaviorSpeculator.TargetBehaviors) {
                customBehaviorSpeculator.TargetBehaviors.Add(targetBehavior);
            }
            foreach (MovementBehaviorBase movementBehavior in m_CachedEnemyActor.behaviorSpeculator.MovementBehaviors) {
                customBehaviorSpeculator.MovementBehaviors.Add(movementBehavior);
            }

            AIActor m_Blobulord = GetOfficialEnemyByGuid("1b5810fafbec445d89921a4efb4e42b7"); // blobulord
            GoopDoer[] m_SourceGoopDoers = m_Blobulord.gameObject.GetComponents<GoopDoer>();

            GoopDoer m_GoopDoer = m_CachedTargetObject.AddComponent<GoopDoer>();

            ExpandUtility.DuplicateComponent(m_GoopDoer, m_SourceGoopDoers[0]);

            m_GoopDoer.goopDefinition = ExpandAssets.LoadOfficialAsset<GoopDefinition>("Poison Goop", ExpandAssets.AssetSource.SharedAuto1);
            m_GoopDoer.DispersalParticleSystemPrefab = m_SourceGoopDoers[1].DispersalParticleSystemPrefab;
            m_GoopDoer.defaultGoopRadius = 0.5f;


            // m_CachedTargetObject.AddComponent<ExpandObjectReshader>();
            
            AddEnemyToDatabase(m_CachedTargetObject, PoisbulordCrawlerGUID);

            m_CachedEnemyActor = null;
            m_Blobulord = null;
            return;
        }

        private static void m_GenerateCronenbergDebris(GameObject targetObject, GoopDefinition goopSource) {
            DebrisObject m_CachedDebrisObject = targetObject.AddComponent<DebrisObject>();
            m_CachedDebrisObject.Priority = EphemeralObject.EphemeralPriority.Minor;
            m_CachedDebrisObject.audioEventName = string.Empty;
            m_CachedDebrisObject.playAnimationOnTrigger = false;
            m_CachedDebrisObject.usesDirectionalFallAnimations = false;
            m_CachedDebrisObject.directionalAnimationData = new DebrisDirectionalAnimationInfo() { fallDown = string.Empty, fallLeft = string.Empty, fallRight = string.Empty, fallUp = string.Empty };
            m_CachedDebrisObject.breaksOnFall = true;
            m_CachedDebrisObject.breakOnFallChance = 1;
            m_CachedDebrisObject.changesCollisionLayer = false;
            m_CachedDebrisObject.groundedCollisionLayer = CollisionLayer.LowObstacle;
            m_CachedDebrisObject.followupBehavior = DebrisObject.DebrisFollowupAction.None;
            m_CachedDebrisObject.collisionStopsBullets = false;
            m_CachedDebrisObject.animatePitFall = false;
            m_CachedDebrisObject.pitFallSplash = true;
            m_CachedDebrisObject.inertialMass = 1;
            m_CachedDebrisObject.motionMultiplier = 1;
            m_CachedDebrisObject.canRotate = true;
            m_CachedDebrisObject.angularVelocity = 10;
            m_CachedDebrisObject.angularVelocityVariance = 3;
            m_CachedDebrisObject.bounceCount = 1;
            m_CachedDebrisObject.additionalBounceEnglish = 0;
            m_CachedDebrisObject.decayOnBounce = 0.5f;
            m_CachedDebrisObject.killTranslationOnBounce = false;
            m_CachedDebrisObject.usesLifespan = false;
            m_CachedDebrisObject.lifespanMin = 1;
            m_CachedDebrisObject.lifespanMax = 1;
            m_CachedDebrisObject.shouldUseSRBMotion = false;
            m_CachedDebrisObject.removeSRBOnGrounded = false;
            m_CachedDebrisObject.placementOptions = new DebrisObject.DebrisPlacementOptions { canBeRotated = true, canBeFlippedHorizontally = false, canBeFlippedVertically = false };
            m_CachedDebrisObject.DoesGoopOnRest = true;
            m_CachedDebrisObject.AssignedGoop = goopSource;
            m_CachedDebrisObject.GoopRadius = 1;
            m_CachedDebrisObject.additionalHeightBoost = 0;
            m_CachedDebrisObject.AssignFinalWorldDepth(-1.5f);
            m_CachedDebrisObject.ForceUpdateIfDisabled = false;
        }
        
    }
}

