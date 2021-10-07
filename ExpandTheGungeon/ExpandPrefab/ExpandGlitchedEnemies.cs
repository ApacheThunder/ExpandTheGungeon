using Dungeonator;
using System;
using System.Collections.Generic;
using UnityEngine;
using FullInspector;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandGlitchedEnemies {        

        public static ExpandGlitchedEnemies Instance {
            get {
                if (m_instance == null) { m_instance = new ExpandGlitchedEnemies(); }
                return m_instance;
            }
        }

        private static ExpandGlitchedEnemies m_instance;


        public ExpandGlitchedEnemies() {
            GrenadeGuyPrefab = EnemyDatabase.GetOrLoadByGuid("4d37ce3d666b4ddda8039929225b7ede").gameObject;
            IceCubeGuyPrefab = EnemyDatabase.GetOrLoadByGuid("f155fd2759764f4a9217db29dd21b7eb").gameObject;
            KeybulletManPrefab = EnemyDatabase.GetOrLoadByGuid("699cd24270af4cd183d671090d8323a1").gameObject;
            ChanceBulletManPrefab = EnemyDatabase.GetOrLoadByGuid("a446c626b56d4166915a4e29869737fd").gameObject;
            SunburstPrefab = EnemyDatabase.GetOrLoadByGuid("ffdc8680bdaa487f8f31995539f74265").gameObject;
            CultistPrefab = EnemyDatabase.GetOrLoadByGuid("57255ed50ee24794b7aac1ac3cfb8a95").gameObject;
            GhostPrefab = EnemyDatabase.GetOrLoadByGuid("4db03291a12144d69fe940d5a01de376").gameObject;
            BulletManPrefab = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").gameObject;
            ArrowheadManPrefab = EnemyDatabase.GetOrLoadByGuid("05891b158cd542b1a5f3df30fb67a7ff").gameObject;
            BulletRifleManPrefab = EnemyDatabase.GetOrLoadByGuid("31a3ea0c54a745e182e22ea54844a82d").gameObject;
            AshBulletManPrefab = EnemyDatabase.GetOrLoadByGuid("1a78cfb776f54641b832e92c44021cf2").gameObject;
            AshBulletShotgunManPrefab = EnemyDatabase.GetOrLoadByGuid("1bd8e49f93614e76b140077ff2e33f2b").gameObject;
            BulletCardinalPrefab = EnemyDatabase.GetOrLoadByGuid("8bb5578fba374e8aae8e10b754e61d62").gameObject;
            BulletMachineGunManPrefab = EnemyDatabase.GetOrLoadByGuid("db35531e66ce41cbb81d507a34366dfe").gameObject;
            BulletManDevilPrefab = EnemyDatabase.GetOrLoadByGuid("5f3abc2d561b4b9c9e72b879c6f10c7e").gameObject;
            BulletManShroomedPrefab = EnemyDatabase.GetOrLoadByGuid("e5cffcfabfae489da61062ea20539887").gameObject;
            BulletSkeletonHelmetPrefab = EnemyDatabase.GetOrLoadByGuid("95ec774b5a75467a9ab05fa230c0c143").gameObject;
            BulletShotgunManSawedOffPrefab = EnemyDatabase.GetOrLoadByGuid("2752019b770f473193b08b4005dc781f").gameObject;
            BulletShotgunManMutantPrefab = EnemyDatabase.GetOrLoadByGuid("7f665bd7151347e298e4d366f8818284").gameObject;
            BulletManMutantPrefab = EnemyDatabase.GetOrLoadByGuid("d4a9836f8ab14f3fadd0f597438b1f1f").gameObject;
            BulletShotgrubManPrefab = EnemyDatabase.GetOrLoadByGuid("044a9f39712f456597b9762893fbc19c").gameObject;
            BulletManBandanaPrefab = EnemyDatabase.GetOrLoadByGuid("88b6b6a93d4b4234a67844ef4728382c").gameObject;
            FloatingEyePrefab = EnemyDatabase.GetOrLoadByGuid("7b0b1b6d9ce7405b86b75ce648025dd6").gameObject;
            ChickenPrefab = EnemyDatabase.GetOrLoadByGuid("76bc43539fc24648bff4568c75c686d1").gameObject;
            SnakePrefab = EnemyDatabase.GetOrLoadByGuid("1386da0f42fb4bcabc5be8feb16a7c38").gameObject;
            AngryBookPrefab = EnemyDatabase.GetOrLoadByGuid("c0ff3744760c4a2eb0bb52ac162056e6").gameObject;
            AngryBookBluePrefab = EnemyDatabase.GetOrLoadByGuid("6f22935656c54ccfb89fca30ad663a64").gameObject;
            AngryBookGreenPrefab = EnemyDatabase.GetOrLoadByGuid("a400523e535f41ac80a43ff6b06dc0bf").gameObject;
            GunNutPrefab = EnemyDatabase.GetOrLoadByGuid("ec8ea75b557d4e7b8ceeaacdf6f8238c").gameObject;
            GunNutSpectrePrefab = EnemyDatabase.GetOrLoadByGuid("383175a55879441d90933b5c4e60cf6f").gameObject;
            GunNutChainPrefab = EnemyDatabase.GetOrLoadByGuid("463d16121f884984abe759de38418e48").gameObject;
            LeadWizardBluePrefab = EnemyDatabase.GetOrLoadByGuid("c50a862d19fc4d30baeba54795e8cb93").gameObject;
            BirdPrefab = EnemyDatabase.GetOrLoadByGuid("ed37fa13e0fa4fcf8239643957c51293").gameObject;
            BulletSharkPrefab = EnemyDatabase.GetOrLoadByGuid("72d2f44431da43b8a3bae7d8a114a46d").gameObject;
            NecromancerPrefab = EnemyDatabase.GetOrLoadByGuid("b1540990a4f1480bbcb3bea70d67f60d").gameObject;
            BombsheePrefab = EnemyDatabase.GetOrLoadByGuid("19b420dec96d4e9ea4aebc3398c0ba7a").gameObject;
            JamromancerPrefab = EnemyDatabase.GetOrLoadByGuid("8b4a938cdbc64e64822e841e482ba3d2").gameObject;
            BullatGiantPrefab = EnemyDatabase.GetOrLoadByGuid("1a4872dafdb34fd29fe8ac90bd2cea67").gameObject;
            BlizzbulonPrefab = EnemyDatabase.GetOrLoadByGuid("022d7c822bc146b58fe3b0287568aaa2").gameObject;
            BlobulonPrefab = EnemyDatabase.GetOrLoadByGuid("0239c0680f9f467dbe5c4aab7dd1eca6").gameObject;
            PoisbulonPrefab = EnemyDatabase.GetOrLoadByGuid("e61cab252cfb435db9172adc96ded75f").gameObject;
            PhaseSpiderPrefab = EnemyDatabase.GetOrLoadByGuid("98ca70157c364750a60f5e0084f9d3e2").gameObject;
            WizardRedPrefab = EnemyDatabase.GetOrLoadByGuid("c4fba8def15e47b297865b18e36cbef8").gameObject;
            WizardYellowPrefab = EnemyDatabase.GetOrLoadByGuid("206405acad4d4c33aac6717d184dc8d4").gameObject;
            WizardBluePrefab = EnemyDatabase.GetOrLoadByGuid("9b2cf2949a894599917d4d391a0b7394").gameObject;
            PowderSkullBlackPrefab = EnemyDatabase.GetOrLoadByGuid("1cec0cdf383e42b19920787798353e46").gameObject;
            BulletManKaliberPrefab = EnemyDatabase.GetOrLoadByGuid("f020570a42164e2699dcf57cac8a495c").gameObject;
            BulletShotgunManCowboyPrefab = EnemyDatabase.GetOrLoadByGuid("ddf12a4881eb43cfba04f36dd6377abb").gameObject;
            BulletRifleProfessionalPrefab = EnemyDatabase.GetOrLoadByGuid("c5b11bfc065d417b9c4d03a5e385fe2c").gameObject;
            BulletManEyepatchPrefab = EnemyDatabase.GetOrLoadByGuid("70216cae6c1346309d86d4a0b4603045").gameObject;
            
            BulletBrosSmileyPrefab = EnemyDatabase.GetOrLoadByGuid("ea40fcc863d34b0088f490f4e57f8913").gameObject;
            BulletBrosShadesPrefab = EnemyDatabase.GetOrLoadByGuid("c00390483f394a849c36143eb878998f").gameObject;
            ResourcefulRatBossPrefab = EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").gameObject;
            GatlingGullPrefab = EnemyDatabase.GetOrLoadByGuid("ec6b674e0acd4553b47ee94493d66422").gameObject;
            ManfredsRivalPrefab = EnemyDatabase.GetOrLoadByGuid("bb73eeeb9e584fbeaf35877ec176de28").gameObject;
            BeholsterPrefab = EnemyDatabase.GetOrLoadByGuid("4b992de5b4274168a8878ef9bf7ea36b").gameObject;
            BossDoorMimicPrefab = EnemyDatabase.GetOrLoadByGuid("9189f46c47564ed588b9108965f975c9").gameObject;
            HighPriestPrefab = EnemyDatabase.GetOrLoadByGuid("6c43fddfd401456c916089fdd1c99b1c").gameObject;

            
            CopPrefab = EnemyDatabase.GetOrLoadByGuid("705e9081261446039e1ed9ff16905d04").gameObject;
            CopAndroidPrefab = EnemyDatabase.GetOrLoadByGuid("640238ba85dd4e94b3d6f68888e6ecb8").gameObject;
            SuperSpaceTurtlePrefab = EnemyDatabase.GetOrLoadByGuid("3a077fa5872d462196bb9a3cb1af02a3").gameObject;
            CursedSuperSpaceTurtlePrefab = EnemyDatabase.GetOrLoadByGuid("9216803e9c894002a4b931d7ea9c6bdf").gameObject;
            PayDayShootPrefab = EnemyDatabase.GetOrLoadByGuid("2976522ec560460c889d11bb54fbe758").gameObject;
            R2G2Prefab = EnemyDatabase.GetOrLoadByGuid("1ccdace06ebd42dc984d46cb1f0db6cf").gameObject;
            PortableTurretPrefab = EnemyDatabase.GetOrLoadByGuid("998807b57e454f00a63d67883fcf90d6").gameObject;
            BabyGoodMimicPrefab = EnemyDatabase.GetOrLoadByGuid("e456b66ed3664a4cb590eab3a8ff3814").gameObject;
            DogPrefab = EnemyDatabase.GetOrLoadByGuid("c07ef60ae32b404f99e294a6f9acba75").gameObject;
            WolfPrefab = EnemyDatabase.GetOrLoadByGuid("ededff1deaf3430eaf8321d0c6b2bd80").gameObject;
            SerJunkanPrefab = EnemyDatabase.GetOrLoadByGuid("c6c8e59d0f5d41969c74e802c9d67d07").gameObject;
            CaterpillarPrefab = EnemyDatabase.GetOrLoadByGuid("d375913a61d1465f8e4ffcf4894e4427").gameObject;
            RaccoonPrefab = EnemyDatabase.GetOrLoadByGuid("e9fa6544000942a79ad05b6e4afb62db").gameObject;
            TurkeyPrefab = EnemyDatabase.GetOrLoadByGuid("6f9c28403d3248c188c391f5e40774c5").gameObject;
            BlankyPrefab = EnemyDatabase.GetOrLoadByGuid("5695e8ffa77c4d099b4d9bd9536ff35e").gameObject;
            BabyShelletonPrefab = EnemyDatabase.GetOrLoadByGuid("3f40178e10dc4094a1565cd4fdc4af56").gameObject;
            BulletKingsToadieObject = EnemyDatabase.GetOrLoadByGuid("d4dd2b2bbda64cc9bcec534b4e920518").gameObject; // Bullet King's Toadie Revenge
            TinyBlobulordObject = EnemyDatabase.GetOrLoadByGuid("d1c9781fdac54d9e8498ed89210a0238").gameObject; // tiny blobulord
            LordOfTheJammedPrefab = EnemyDatabase.GetOrLoadByGuid("0d3f7c641557426fbac8596b61c9fb45").gameObject;

            RatPrefabs = new GameObject[] {
                EnemyDatabase.GetOrLoadByGuid("6ad1cafc268f4214a101dca7af61bc91").gameObject, // Rat
                EnemyDatabase.GetOrLoadByGuid("14ea47ff46b54bb4a98f91ffcffb656d").gameObject // Rat Candle
            };

            // Kill Pillars is technically a AIActorDummy object. The real AIActor objects are buried in the "realPrefab" field of said AiActorDummy component.
            KillPillarsPrefab = EnemyDatabase.GetOrLoadByGuid("3f11bbbc439c4086a180eb0fb9990cb4").gameObject;
        }

        public GameObject GrenadeGuyPrefab;
        public GameObject IceCubeGuyPrefab;
        public GameObject KeybulletManPrefab;
        public GameObject ChanceBulletManPrefab;
        public GameObject SunburstPrefab;

        public GameObject CultistPrefab;
        public GameObject GhostPrefab;
        public GameObject BulletManPrefab;
        public GameObject ArrowheadManPrefab;
        public GameObject BulletRifleManPrefab;
        public GameObject AshBulletManPrefab;
        public GameObject AshBulletShotgunManPrefab;
        public GameObject BulletCardinalPrefab;
        public GameObject BulletMachineGunManPrefab;
        public GameObject BulletManDevilPrefab;
        public GameObject BulletManShroomedPrefab;
        public GameObject BulletSkeletonHelmetPrefab;
        public GameObject BulletShotgunManSawedOffPrefab;
        public GameObject BulletShotgunManMutantPrefab;
        public GameObject BulletManMutantPrefab;
        public GameObject BulletShotgrubManPrefab;
        public GameObject BulletManBandanaPrefa;
        public GameObject FloatingEyePrefab;
        public GameObject BulletManBandanaPrefab;
        public GameObject BulletManKaliberPrefab;
        public GameObject BulletShotgunManCowboyPrefab;

        public GameObject ChickenPrefab;
        public GameObject SnakePrefab;
        public GameObject[] RatPrefabs;

        public GameObject AngryBookPrefab;
        public GameObject AngryBookBluePrefab;
        public GameObject AngryBookGreenPrefab;


        //Enemies without guns but don't teleport
        public GameObject GunNutPrefab;
        public GameObject GunNutSpectrePrefab;
        public GameObject GunNutChainPrefab;
        public GameObject LeadWizardBluePrefab;
        public GameObject BirdPrefab;
        public GameObject BulletSharkPrefab;
        public GameObject NecromancerPrefab;
        public GameObject BombsheePrefab;
        public GameObject JamromancerPrefab;
        public GameObject BullatGiantPrefab;
        public GameObject BlizzbulonPrefab;
        public GameObject BlobulonPrefab;
        public GameObject PoisbulonPrefab;
                
        // Enemies without guns that Teleport
        public GameObject PhaseSpiderPrefab;
        public GameObject WizardRedPrefab;
        public GameObject WizardYellowPrefab;
        public GameObject WizardBluePrefab;

        // Only to be used as source Enemies
        public GameObject PowderSkullBlackPrefab;
        public GameObject BulletShotgunManRedPrefab;
        public GameObject BulletShotgunManBluePrefab;
        public GameObject BulletRifleProfessionalPrefab;
        public GameObject BulletManEyepatchPrefab;

        //Glitched Bosses
        public GameObject BulletBrosSmileyPrefab;
        public GameObject BulletBrosShadesPrefab;
        public GameObject ResourcefulRatBossPrefab;
        public GameObject GatlingGullPrefab;
        public GameObject ManfredsRivalPrefab;
        public GameObject BeholsterPrefab;
        public GameObject BossDoorMimicPrefab;
        public GameObject HighPriestPrefab;

        // Companions (as targets only)
        public GameObject CopPrefab;
        public GameObject CopAndroidPrefab;
        public GameObject SuperSpaceTurtlePrefab;
        public GameObject CursedSuperSpaceTurtlePrefab;
        public GameObject PayDayShootPrefab;
        public GameObject R2G2Prefab;
        public GameObject PortableTurretPrefab;
        public GameObject BabyGoodMimicPrefab;
        public GameObject BabyShelletonPrefab;
        public GameObject DogPrefab;
        public GameObject WolfPrefab;
        public GameObject SerJunkanPrefab;
        public GameObject CaterpillarPrefab;
        public GameObject RaccoonPrefab;
        public GameObject TurkeyPrefab;
        public GameObject BlankyPrefab;
        
        // Misc Things
        public GameObject BulletKingsToadieObject; // Bullet King's Toadie Revenge
        public GameObject TinyBlobulordObject; // tiny blobulord
        public GameObject LordOfTheJammedPrefab;

        // Kill Pillars. (handle this one different. Uses AIActorDummy instread of AIActor component)
        public GameObject KillPillarsPrefab;


        public static void GlitchExistingEnemy(AIActor aiActor, bool overrideGUIDCheck = false) { 
            if (string.IsNullOrEmpty(aiActor.EnemyGuid)) { return; }
            if (aiActor.GetActorName().ToLower().StartsWith("glitched")) { return; }
            if (aiActor.name.ToLower().StartsWith("glitched")) { return; }
            if (aiActor.gameObject.GetComponent<ExpandSpawnGlitchObjectOnDeath>() != null) { return; }

            if (aiActor.EnemyGuid == "0239c0680f9f467dbe5c4aab7dd1eca6" | aiActor.EnemyGuid == "e61cab252cfb435db9172adc96ded75f") {
                SpawnEnemyOnDeath CachedBlobSpawnEnemyOnDeath = aiActor.gameObject.GetComponent<SpawnEnemyOnDeath>();
                if (CachedBlobSpawnEnemyOnDeath.maxSpawnCount == 3) { return; }
                CachedBlobSpawnEnemyOnDeath.enemyGuidsToSpawn = ExpandLists.SpawnEnemyOnDeathGUIDList;
                CachedBlobSpawnEnemyOnDeath.enemySelection = SpawnEnemyOnDeath.EnemySelection.Random;
                CachedBlobSpawnEnemyOnDeath.minSpawnCount = 2;
                CachedBlobSpawnEnemyOnDeath.maxSpawnCount = 3;
                return;
            } else if (aiActor.EnemyGuid == "1a4872dafdb34fd29fe8ac90bd2cea67") {
                SpawnEnemyOnDeath CachedBullatSpawnEnemyOnDeath = aiActor.gameObject.GetComponent<SpawnEnemyOnDeath>();
                CachedBullatSpawnEnemyOnDeath.enemyGuidsToSpawn = ExpandLists.SpawnEnemyOnDeathGUIDList;
                return;
            } else if (overrideGUIDCheck) {
                aiActor.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>();
            }
        }                
        public static void StunGlitchedEnemiesInRoom(RoomHandler targetRoom, float StunDuration) {
            List<AIActor> activeEnemies = targetRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
            if (activeEnemies == null | activeEnemies.Count == 0) { return; }
            for (int i = 0; i < activeEnemies.Count; i++) {
                if (activeEnemies[i].IsNormalEnemy && activeEnemies[i].healthHaver && !activeEnemies[i].healthHaver.IsBoss) {
                    // Vector2 vector = (!activeEnemies[i].specRigidbody) ? activeEnemies[i].sprite.WorldBottomLeft : activeEnemies[i].specRigidbody.UnitBottomLeft;
                    // Vector2 vector2 = (!activeEnemies[i].specRigidbody) ? activeEnemies[i].sprite.WorldTopRight : activeEnemies[i].specRigidbody.UnitTopRight;
                    if (activeEnemies[i].name.ToLower().StartsWith("glitched") | activeEnemies[i].GetActorName().ToLower().StartsWith("glitched")) {
                        StunGlitchedEnemy(activeEnemies[i], StunDuration);
                    }                    
                }
            }
        }
        
        protected static void StunGlitchedEnemy(AIActor target, float StunDuration) {
            if (target && target.behaviorSpeculator) {
                target.behaviorSpeculator.Stun(StunDuration, true);
            }
        }

        public AIActor GenerateGlitchedActorPrefab(GameObject TargetEnemyObject, GameObject SourceEnemy, bool ExplodesOnDeath = false, bool spawnsGlitchedObjectOnDeath = false, Action<AIActor> SpecificEnemyMods = null) {            
            if (TargetEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Target Actor Prefab to spawn is null!", false);
                return null;
            }           
            if (SourceEnemy == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source Actor Prefab is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = SourceEnemy.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = TargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
            }

            if (CachedGlitchEnemyActor.EnemyGuid != CachedEnemyActor.EnemyGuid) {

                AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

                CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
                CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
                CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
                CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
                CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
                CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
                CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
                CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

                CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
                CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
                CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
                CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
                CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                // CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                // CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();

                CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth());
                CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
                CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
                CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
                CachedGlitchEnemyActor.IsNormalEnemy = true;
                CachedGlitchEnemyActor.ImmuneToAllEffects = false;

                CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
                CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

                CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
                CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
                CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
                CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
                CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
                CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
                CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
                CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
                CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
                CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
                CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
                CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
                CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
                CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
                CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
                CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
                CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
                CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
                CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;                
                CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
                CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
                CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;
            }

            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.ActorName);
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;

            CachedGlitchEnemyActor.EnemyId = UnityEngine.Random.Range(10000, 99999);
            CachedGlitchEnemyActor.EnemyGuid = Guid.NewGuid().ToString();

            if (CachedGlitchEnemyActor.EnemyGuid == CachedEnemyActor.EnemyGuid) {
                if (CachedGlitchEnemyActor.sprite.GetComponentsInChildren<tk2dBaseSprite>(true) != null) {
                    foreach (tk2dBaseSprite actorSprite in CachedGlitchEnemyActor.sprite.GetComponentsInChildren<tk2dBaseSprite>(true)) {
                        ExpandShaders.Instance.ApplyGlitchShader(actorSprite);
                    }
                }
            } else {
                if (CachedGlitchEnemyActor.sprite != null) {
                    ExpandShaders.Instance.ApplySuperGlitchShader(CachedGlitchEnemyActor.sprite, CachedEnemyActor);
                }
            }

            if (ExplodesOnDeath) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
            if (spawnsGlitchedObjectOnDeath) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>(); }

            SpecificEnemyMods?.Invoke(CachedGlitchEnemyActor);

            return CachedGlitchEnemyActor;
        }
        /*public static void ModifyActorPrefab(AIActor TargetEnemy, AIActor SourceEnemy, bool ExplodesOnDeath = false, bool spawnsGlitchedObjectOnDeath = false, Action<AIActor> SpecificEnemyMods = null) {            
            if (TargetEnemy == null) {
                if (Stats.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Target Actor Prefab to spawn is null!", false);
                return;
            }           
            if (SourceEnemy == null) {
                if (Stats.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source Actor Prefab is null!", false);
                return;
            }            

            if (Stats.debugMode) {
                ETGModConsole.Log("Spawning '" + TargetEnemy.ActorName + "' with GUID: " + TargetEnemy.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + SourceEnemy.ActorName + "' with GUID: " + SourceEnemy.EnemyGuid + " .", false);
            }

            if (TargetEnemy.EnemyGuid != SourceEnemy.EnemyGuid) {

                //TargetEnemy.behaviorSpeculator.enabled = false;

                AddOrReplaceExistingAIActorConfig(TargetEnemy, SourceEnemy);

                TargetEnemy.ManualKnockbackHandling = SourceEnemy.ManualKnockbackHandling;
                TargetEnemy.KnockbackVelocity = SourceEnemy.KnockbackVelocity;
                TargetEnemy.PreventDeathKnockback = SourceEnemy.PreventDeathKnockback;
                TargetEnemy.IsWorthShootingAt = SourceEnemy.IsWorthShootingAt;
                TargetEnemy.CollisionKnockbackStrength = SourceEnemy.CollisionKnockbackStrength;
                TargetEnemy.EnemyCollisionKnockbackStrengthOverride = SourceEnemy.EnemyCollisionKnockbackStrengthOverride;
                TargetEnemy.healthHaver.spawnBulletScript = SourceEnemy.healthHaver.spawnBulletScript;
                TargetEnemy.healthHaver.SuppressDeathSounds = SourceEnemy.healthHaver.SuppressDeathSounds;

                TargetEnemy.healthHaver.ManualDeathHandling = SourceEnemy.healthHaver.ManualDeathHandling;
                TargetEnemy.healthHaver.deathEffect = SourceEnemy.healthHaver.deathEffect;
                TargetEnemy.healthHaver.noCorpseWhenBulletScriptDeath = SourceEnemy.healthHaver.noCorpseWhenBulletScriptDeath;
                TargetEnemy.StealthDeath = SourceEnemy.StealthDeath;
                TargetEnemy.SpeculatorDelayTime = SourceEnemy.SpeculatorDelayTime;

                TargetEnemy.BehaviorOverridesVelocity = SourceEnemy.BehaviorOverridesVelocity;
                TargetEnemy.behaviorSpeculator.RefreshBehaviors();
                TargetEnemy.behaviorSpeculator.RegenerateCache();
                // TargetEnemy.behaviorSpeculator.enabled = true;

                TargetEnemy.healthHaver.SetHealthMaximum(SourceEnemy.healthHaver.GetCurrentHealth());
                TargetEnemy.healthHaver.minimumHealth = SourceEnemy.healthHaver.minimumHealth;
                TargetEnemy.healthHaver.OnlyAllowSpecialBossDamage = false;
                TargetEnemy.healthHaver.PreventAllDamage = false;
                TargetEnemy.IsNormalEnemy = true;
                TargetEnemy.ImmuneToAllEffects = false;

                TargetEnemy.healthHaver.spawnBulletScript = SourceEnemy.healthHaver.spawnBulletScript;
                TargetEnemy.healthHaver.SuppressDeathSounds = SourceEnemy.healthHaver.SuppressDeathSounds;

                TargetEnemy.OnHandleRewards = SourceEnemy.OnHandleRewards;
                TargetEnemy.CustomChestTable = SourceEnemy.CustomChestTable;
                TargetEnemy.CustomLootTable = SourceEnemy.CustomLootTable;
                TargetEnemy.CustomLootTableMinDrops = SourceEnemy.CustomLootTableMinDrops;
                TargetEnemy.SpawnLootAtRewardChestPos = SourceEnemy.SpawnLootAtRewardChestPos;
                TargetEnemy.ManualKnockbackHandling = SourceEnemy.ManualKnockbackHandling;
                TargetEnemy.BaseMovementSpeed = SourceEnemy.BaseMovementSpeed;
                TargetEnemy.MovementSpeed = SourceEnemy.MovementSpeed;
                TargetEnemy.OnCorpseVFX = SourceEnemy.OnCorpseVFX;
                TargetEnemy.CollisionVFX = SourceEnemy.CollisionVFX;
                TargetEnemy.CollisionSetsPlayerOnFire = SourceEnemy.CollisionSetsPlayerOnFire;
                TargetEnemy.CollisionDamage = SourceEnemy.CollisionDamage;
                TargetEnemy.CollisionDamageTypes = SourceEnemy.CollisionDamageTypes;
                TargetEnemy.NonActorCollisionVFX = SourceEnemy.NonActorCollisionVFX;
                TargetEnemy.OnEngagedVFX = SourceEnemy.OnEngagedVFX;
                TargetEnemy.OnEngagedVFXAnchor = SourceEnemy.OnEngagedVFXAnchor;
                TargetEnemy.TryDodgeBullets = SourceEnemy.TryDodgeBullets;
                TargetEnemy.AvoidRadius = SourceEnemy.AvoidRadius;
                TargetEnemy.CorpseObject = SourceEnemy.CorpseObject;                
                TargetEnemy.PreventFallingInPitsEver = SourceEnemy.PreventFallingInPitsEver;
                TargetEnemy.UseMovementAudio = SourceEnemy.UseMovementAudio;
                TargetEnemy.EnemySwitchState = SourceEnemy.EnemySwitchState;
            }

            TargetEnemy.HitByEnemyBullets = BraveUtility.RandomBool();
            TargetEnemy.OverrideDisplayName = ("Corrupted " + SourceEnemy.ActorName);
            TargetEnemy.ActorName = ("Corrupted " + TargetEnemy.GetActorName());
            TargetEnemy.name = ("Corrupted " + TargetEnemy.name);
            TargetEnemy.CanTargetEnemies = false;
            TargetEnemy.CanTargetPlayers = true;

            TargetEnemy.EnemyId = UnityEngine.Random.Range(10000, 99999);
            TargetEnemy.EnemyGuid = Guid.NewGuid().ToString();

            if (TargetEnemy.EnemyGuid == SourceEnemy.EnemyGuid) {
                if (TargetEnemy.sprite.GetComponentsInChildren<tk2dBaseSprite>(true) != null) {
                    foreach (tk2dBaseSprite actorSprite in TargetEnemy.sprite.GetComponentsInChildren<tk2dBaseSprite>(true)) {
                        ChaosShaders.Instance.ApplyGlitchShader(null, actorSprite);
                    }
                }
            } else {
                if (TargetEnemy.sprite != null) {
                    ChaosShaders.Instance.ApplySuperGlitchShader(TargetEnemy.sprite, SourceEnemy);
                }
            }

            if (ExplodesOnDeath) { TargetEnemy.healthHaver.gameObject.AddComponent<ChaosExplodeOnDeath>(); }
            if (spawnsGlitchedObjectOnDeath) { TargetEnemy.healthHaver.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>(); }

            SpecificEnemyMods?.Invoke(TargetEnemy);

            return;
        }*/

        public void SpawnGlitchedSuperReaper(RoomHandler targetRoom, IntVector2 SpawnLocation, bool isCursed = false) {
            GameObject superReaperPrefab = DungeonPlaceableUtility.InstantiateDungeonPlaceable(LordOfTheJammedPrefab, targetRoom, SpawnLocation, true, AIActor.AwakenAnimationType.Awaken, true);
             
            if (superReaperPrefab == null) { return; }
            AIActor m_cachedSuperReaperActor = superReaperPrefab.GetComponent<AIActor>();
            if (m_cachedSuperReaperActor == null) { return; }
            
            UnityEngine.Object.Destroy(m_cachedSuperReaperActor.gameObject.GetComponentInChildren<SuperReaperController>(true));
            m_cachedSuperReaperActor.gameObject.AddComponent<ExpandGlitchedSuperReaperController>();
            if (isCursed) {
                ExpandGlitchedSuperReaperController glitchReaperController = m_cachedSuperReaperActor.gameObject.GetComponent<ExpandGlitchedSuperReaperController>();
                glitchReaperController.becomesBlackPhantom = true;
            }

            m_cachedSuperReaperActor.EnemyId = 6666;
            m_cachedSuperReaperActor.EnemyGuid = Guid.NewGuid().ToString();
            m_cachedSuperReaperActor.ActorName = ("Corrupted " + m_cachedSuperReaperActor.GetActorName());
            m_cachedSuperReaperActor.name = ("Corrupted " + m_cachedSuperReaperActor.name);
            m_cachedSuperReaperActor.CanTargetEnemies = false;
            m_cachedSuperReaperActor.CanTargetPlayers = true;
            m_cachedSuperReaperActor.IgnoreForRoomClear = false;
            m_cachedSuperReaperActor.encounterTrackable.journalData.PrimaryDisplayName = "Glitched Lord of the Jammed";
            m_cachedSuperReaperActor.encounterTrackable.journalData.NotificationPanelDescription = "Glitched Lord of the Jammed";            

            m_cachedSuperReaperActor.ConfigureOnPlacement(targetRoom);
            return;
        }

        public GameObject SpawnRandomGlitchEnemy(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {

            // int GlitchEnemyNumber = UnityEngine.Random.Range(0, 52);
            int GlitchEnemyCount = 52;
            List<int> GlitchEnemyNumberList = new List<int>();

            for (int i = 0; i < GlitchEnemyCount; i++) { GlitchEnemyNumberList.Add(i); }

            GlitchEnemyNumberList = GlitchEnemyNumberList.Shuffle();

            int GlitchEnemyNumber = BraveUtility.RandomElement(GlitchEnemyNumberList);

            GameObject targetObject = null;

            try {
                if (UnityEngine.Random.value <= 0.85f) {
                    if (GlitchEnemyNumber == 0) { targetObject = SpawnGlitchedBulletKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 1) { targetObject = SpawnGlitchedCultist(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 2) { targetObject = SpawnGlitchedGhost(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 3) { targetObject = SpawnGlitchedArrowheadKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 4) { targetObject = SpawnGlitchedSniperKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 5) { targetObject = SpawnGlitchedAshBulletKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 6) { targetObject = SpawnGlitchedAshShotGunKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 7) { targetObject = SpawnGlitchedCardinalBulletKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 8) { targetObject = SpawnGlitchedBulletMachineGunMan(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 9) { targetObject = SpawnGlitchedBulletManDevil(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 10) { targetObject = SpawnGlitchedBulletManShroomed(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 11) { targetObject = SpawnGlitchedBulletSkeletonHelmet(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 12) { targetObject = SpawnGlitchedVeteranShotGunKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 13) { targetObject = SpawnGlitchedMutantShotGunKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 14) { targetObject = SpawnGlitchedMutantBulletKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 15) { targetObject = SpawnGlitchedShotGrubKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 16) { targetObject = SpawnGlitchedWizardRed(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 17) { targetObject = SpawnGlitchedWizardYellow(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 18) { targetObject = SpawnGlitchedWizardBlue(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 19) { targetObject = SpawnGlitchedWizardBlue(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 20) { targetObject = SpawnGlitchedChicken(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 21) { targetObject = SpawnGlitchedBird(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 22) { targetObject = SpawnGlitchedBulletShark(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 23) { targetObject = SpawnGlitchedBlueLeadWizard(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 24) { targetObject = SpawnGlitchedNecromancer(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 25) { targetObject = SpawnGlitchedJamromancer(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 26) { targetObject = SpawnGlitchedAngryBook(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 27) { targetObject = SpawnGlitchedBullatGiant(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 28) { targetObject = SpawnGlitchedResourcefulRat(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 29) { targetObject = SpawnGlitchedBlockner(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 30) { targetObject = SpawnGlitchedBandanaBulletKin(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 31) { targetObject = SpawnGlitchedAngryBook(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 32) { targetObject = SpawnGlitchedBeadie(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 33) { targetObject = SpawnGlitchedSnake(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 34) { targetObject = SpawnGlitchedCop(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 35) { targetObject = SpawnGlitchedCopAndroid(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 36) { targetObject = SpawnGlitchedSpaceTurtle(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 37) { targetObject = SpawnGlitchedCursedSpaceTurtle(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 38) { targetObject = SpawnGlitchedPayDayShotGunGuy(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 39) { targetObject = SpawnGlitchedR2G2(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 40) { targetObject = SpawnGlitchedPortableTurret(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 41) { targetObject = SpawnGlitchedBabyMimic(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 42) { targetObject = SpawnGlitchedDog(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 43) { targetObject = SpawnGlitchedWolf(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 44) { targetObject = SpawnGlitchedSerJunkan(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 45) { targetObject = SpawnGlitchedCaterpillar(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 46) { targetObject = SpawnGlitchedRaccoon(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 47) { targetObject = SpawnGlitchedTurkey(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 48) { targetObject = SpawnGlitchedBlizzbulon(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 49) { targetObject = SpawnGlitchedRandomBlob(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 50) { targetObject = SpawnGlitchedBlanky(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 51) { targetObject = SpawnGlitchedBabyShelleton(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    if (GlitchEnemyNumber == 52) { targetObject = SpawnGlitchedPlayerAsEnemy(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
                    END:;

                } else {
                    try {
                        targetObject = SpawnGlitchedBigEnemy(CurrentRoom, position, autoEngage, awakenAnimType);
                    } catch (Exception ex) {
                        if (ExpandSettings.debugMode) {
                            ETGModConsole.Log("[DEBUG] WARNING: Exception while attempting to spawn glitched big enemy!");
                        }
                        Debug.Log("WARNING: Exception while attempting to spawn glitched big enemy!");
                        Debug.LogException(ex);
                    }
                }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] WARNING: Exception while attempting to spawn glitched enemy with ID: " + GlitchEnemyNumber.ToString());
                }
                Debug.Log("WARNING: Exception while attempting to spawn glitched enemy with ID: " + GlitchEnemyNumber.ToString());
                Debug.LogException(ex);
            }

            if (targetObject) {
                targetObject.transform.SetParent(CurrentRoom.hierarchyParent, true);
                if (targetObject.GetComponent<AIActor>()) {
                    CurrentRoom.RegisterEnemy(targetObject.GetComponent<AIActor>());
                }
            }
            return targetObject;
        }
        public GameObject SpawnRandomGlitchBoss(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            int GlitchBossNumber = UnityEngine.Random.Range(0, 6);

            GameObject targetObject = null;

            if (GlitchBossNumber == 0) { targetObject = SpawnGlitchedBulletBros(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
            if (GlitchBossNumber == 1) { targetObject = SpawnGlitchedGatlingGull(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
            if (GlitchBossNumber == 2) { targetObject = SpawnGlitchedBeholster(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
            if (GlitchBossNumber == 3) { targetObject = SpawnGlitchedBossDoorMimic(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
            if (GlitchBossNumber == 4) { targetObject = SpawnGlitchedHighPriest(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }
            if (GlitchBossNumber >= 5) { targetObject = SpawnGlitchedKillPillar(CurrentRoom, position, autoEngage, awakenAnimType); goto END; }

            END:;

            if (targetObject) {
                targetObject.transform.SetParent(CurrentRoom.hierarchyParent, true);
                if (targetObject.GetComponent<AIActor>()) { CurrentRoom.RegisterEnemy(targetObject.GetComponent<AIActor>()); }
            }

            return targetObject;
        }

        public void AddOrReplaceAIActorConfig(AIActor target, AIActor source, bool isResourcefulRat = false) {
            if (target == null | source == null) { return; }
            if (target.EnemyGuid == source.EnemyGuid) { return; }
            
            try { 
                if (target.behaviorSpeculator != null && source.behaviorSpeculator != null) {
                    target.behaviorSpeculator.OtherBehaviors = source.behaviorSpeculator.OtherBehaviors;
                    target.behaviorSpeculator.TargetBehaviors = source.behaviorSpeculator.TargetBehaviors;
                    target.behaviorSpeculator.OverrideBehaviors = source.behaviorSpeculator.OverrideBehaviors;
                    target.behaviorSpeculator.AttackBehaviors = source.behaviorSpeculator.AttackBehaviors;
                    target.behaviorSpeculator.MovementBehaviors = source.behaviorSpeculator.MovementBehaviors;
                    // Remove certain problamatic behaviors if they don't work correctly on the target enemy.
                    // (they can cause exceptions and hurt game performance)
                    if (target.behaviorSpeculator.MovementBehaviors != null && target.behaviorSpeculator.MovementBehaviors.Count > 0) {
                        for (int i = 0; i < target.behaviorSpeculator.MovementBehaviors.Count; i++) {
                            if (target.behaviorSpeculator.MovementBehaviors[i].GetType() == typeof(TakeCoverBehavior)/*|
                                target.behaviorSpeculator.MovementBehaviors[i].GetType() == typeof(SeekTargetBehavior)*/) {
                                target.behaviorSpeculator.MovementBehaviors.Remove(target.behaviorSpeculator.MovementBehaviors[i]);
                            }
                        }
                    }
                    if (target.behaviorSpeculator.OverrideBehaviors != null && target.behaviorSpeculator.OverrideBehaviors.Count > 0) {
                        for (int i = 0; i < target.behaviorSpeculator.OverrideBehaviors.Count; i++) {
                            if (target.behaviorSpeculator.OverrideBehaviors[i].GetType() == typeof(RedBarrelAwareness)) {
                                target.behaviorSpeculator.OverrideBehaviors.Remove(target.behaviorSpeculator.OverrideBehaviors[i]);
                            }
                        }
                    }
                    target.behaviorSpeculator.AttackCooldown = source.behaviorSpeculator.AttackCooldown;
                    target.behaviorSpeculator.CooldownScale = source.behaviorSpeculator.CooldownScale;
                    target.behaviorSpeculator.FleePlayerData = source.behaviorSpeculator.FleePlayerData;
                    target.behaviorSpeculator.GlobalCooldown = source.behaviorSpeculator.GlobalCooldown;
                    target.behaviorSpeculator.ImmuneToStun = source.behaviorSpeculator.ImmuneToStun;
                    target.behaviorSpeculator.InstantFirstTick = source.behaviorSpeculator.InstantFirstTick;
                    target.behaviorSpeculator.LocalTimeScale = source.behaviorSpeculator.LocalTimeScale;
                    target.behaviorSpeculator.majorBreakable = source.behaviorSpeculator.majorBreakable;
                    // target.behaviorSpeculator.name = source.behaviorSpeculator.name;
                    target.behaviorSpeculator.PlayerTarget = source.behaviorSpeculator.PlayerTarget;
                    target.behaviorSpeculator.PostAwakenDelay = source.behaviorSpeculator.PostAwakenDelay;
                    target.behaviorSpeculator.PreventMovement = source.behaviorSpeculator.PreventMovement;
                    target.behaviorSpeculator.RemoveDelayOnReinforce = source.behaviorSpeculator.RemoveDelayOnReinforce;
                    target.behaviorSpeculator.SkipTimingDifferentiator = source.behaviorSpeculator.SkipTimingDifferentiator;                
                    target.behaviorSpeculator.StartingFacingDirection = source.behaviorSpeculator.StartingFacingDirection;
                    target.behaviorSpeculator.TickInterval = source.behaviorSpeculator.TickInterval;
                    source.behaviorSpeculator.specRigidbody = source.behaviorSpeculator.specRigidbody;

                    target.PathableTiles = source.PathableTiles;
                }
            } catch (Exception) { }

            try { 
                if (source.bulletBank != null) {
                    if (!isResourcefulRat) {
                        if (target.bulletBank != null) { UnityEngine.Object.Destroy(target.bulletBank); }
                        target.gameObject.AddComponent<AIBulletBank>();
                    }
                    AIBulletBank m_bulletBank = target.gameObject.GetComponent<AIBulletBank>();
                    if (source.bulletBank.name != null) m_bulletBank.name = source.bulletBank.name;
                    m_bulletBank.Bullets = source.bulletBank.Bullets;
                    m_bulletBank.useDefaultBulletIfMissing = source.bulletBank.useDefaultBulletIfMissing;
                    m_bulletBank.transforms = source.bulletBank.transforms;
                    m_bulletBank.enabled = source.bulletBank.enabled;
                }
            } catch (Exception) { }

            try { 
                if (source.GetComponent<AIShooter>() != null) {
                    if (!isResourcefulRat) {
                        if (target.aiShooter != null) { UnityEngine.Object.Destroy(target.GetComponent<AIShooter>()); }
                        target.gameObject.AddComponent<AIShooter>();
                    }                    
                    AIShooter m_aiShooter = target.gameObject.GetComponent<AIShooter>();
                    if (source.aiShooter.name != null) m_aiShooter.name = source.aiShooter.name;
                    m_aiShooter.volley = source.aiShooter.volley;
                    m_aiShooter.volleyShootPosition = source.aiShooter.volleyShootPosition;
                    m_aiShooter.volleyShellCasing = source.aiShooter.volleyShellCasing;
                    m_aiShooter.volleyShellTransform = source.aiShooter.volleyShellTransform;
                    m_aiShooter.volleyShootVfx = source.aiShooter.volleyShootVfx;
                    m_aiShooter.usesOctantShootVFX = source.aiShooter.usesOctantShootVFX;
                    m_aiShooter.bulletName = source.aiShooter.bulletName;
                    m_aiShooter.customShootCooldownPeriod = source.aiShooter.customShootCooldownPeriod;
                    m_aiShooter.doesScreenShake = source.aiShooter.doesScreenShake;
                    m_aiShooter.rampBullets = source.aiShooter.rampBullets;
                    m_aiShooter.rampStartHeight = source.aiShooter.rampStartHeight;
                    m_aiShooter.rampTime = source.aiShooter.rampTime;
                    m_aiShooter.bulletScriptAttachPoint = source.aiShooter.bulletScriptAttachPoint;
                    m_aiShooter.BackupAimInMoveDirection = source.aiShooter.BackupAimInMoveDirection;
                    m_aiShooter.equippedGunId = source.aiShooter.equippedGunId;
                    if (source.aiShooter.gunAttachPoint != null) {
                        GameObject m_GunAttachPoint = new GameObject("GunAttachPoint");
                        m_aiShooter.gunAttachPoint = m_GunAttachPoint.transform;
                        m_aiShooter.gunAttachPoint.parent = target.gameObject.transform;
                        m_GunAttachPoint.transform.localPosition = source.aiShooter.gunAttachPoint.transform.localPosition;
                        m_GunAttachPoint.transform.localRotation = source.aiShooter.gunAttachPoint.transform.localRotation;
                        m_GunAttachPoint.transform.localScale = source.aiShooter.gunAttachPoint.transform.localScale;
                    }
                    if (source.aiShooter.Inventory != null) {
                        m_aiShooter.Inventory.ForceNoGun = source.aiShooter.Inventory.ForceNoGun;
                        m_aiShooter.Inventory.GunChangeForgiveness = source.aiShooter.Inventory.GunChangeForgiveness;
                        m_aiShooter.Inventory.GunLocked = source.aiShooter.Inventory.GunLocked;
                        m_aiShooter.Inventory.maxGuns = source.aiShooter.Inventory.maxGuns;
                        m_aiShooter.Inventory.m_gunClassOverrides = source.aiShooter.Inventory.m_gunClassOverrides;
                        m_aiShooter.GunAngle = source.aiShooter.GunAngle;
                        m_aiShooter.shouldUseGunReload = source.aiShooter.shouldUseGunReload;
                        m_aiShooter.overallGunAttachOffset = source.aiShooter.overallGunAttachOffset;
                        m_aiShooter.flippedGunAttachOffset = source.aiShooter.flippedGunAttachOffset;
                        m_aiShooter.handObject = source.aiShooter.handObject;
                        m_aiShooter.AllowTwoHands = source.aiShooter.AllowTwoHands;
                        m_aiShooter.ForceGunOnTop = source.aiShooter.ForceGunOnTop;
                        m_aiShooter.IsReallyBigBoy = source.aiShooter.IsReallyBigBoy;
                        m_aiShooter.gunAttachPoint = source.aiShooter.gunAttachPoint;
                    }                    
                }

                // Add teleport transition effect to target enemy if source is HollowPoint
                if (source.EnemyGuid == "4db03291a12144d69fe940d5a01de376" && source.gameObject.GetComponent<tk2dSpriteAnimation>() != null) {
                    bool hadNullAnimation = false;                    
                    if (target.gameObject.GetComponent<tk2dSpriteAnimation>() == null) {
                        target.gameObject.AddComponent<tk2dSpriteAnimation>();
                        hadNullAnimation = true;
                    }
                    tk2dSpriteAnimation m_sourcespriteAnimation = source.gameObject.GetComponent<tk2dSpriteAnimation>();
                    tk2dSpriteAnimation m_targetspriteAnimation = target.gameObject.GetComponent<tk2dSpriteAnimation>();
                    if (hadNullAnimation | m_targetspriteAnimation.clips == null | m_targetspriteAnimation.clips.Length <= 0) {
                        if (m_sourcespriteAnimation.name != null) m_targetspriteAnimation.name = m_sourcespriteAnimation.name;
                        m_targetspriteAnimation.clips = new tk2dSpriteAnimationClip[] { m_sourcespriteAnimation.clips[13], m_sourcespriteAnimation.clips[14] };
                    } else {
                        List<tk2dSpriteAnimationClip> m_tempClipList = new List<tk2dSpriteAnimationClip>();
                        for (int i = 0; i < m_targetspriteAnimation.clips.Length; i++) {
                            m_tempClipList.Add(m_targetspriteAnimation.clips[i]);
                        }
                        if (m_tempClipList.Count > 0) {
                            m_tempClipList.Add(m_sourcespriteAnimation.clips[13]);
                            m_tempClipList.Add(m_sourcespriteAnimation.clips[14]);
                            m_targetspriteAnimation.clips = m_tempClipList.ToArray();
                        } else {
                            m_targetspriteAnimation.clips = new tk2dSpriteAnimationClip[] { m_sourcespriteAnimation.clips[13], m_sourcespriteAnimation.clips[14] };
                        }
                    }
                }

                // Fix collision of Snake
                if (target.EnemyGuid == "1386da0f42fb4bcabc5be8feb16a7c38" && target.gameObject.GetComponentInChildren<SpeculativeRigidbody>() != null) {
                    SpeculativeRigidbody snakeRigidBody = target.gameObject.GetComponent<SpeculativeRigidbody>();
                    snakeRigidBody.PixelColliders[0].CollisionLayer = CollisionLayer.EnemyCollider;
                    snakeRigidBody.PixelColliders[1].Enabled = true;
                    // snakeRigidBody.ForceRegenerate();
                    if (source.EnemyGuid != "d1c9781fdac54d9e8498ed89210a0238") {
                        target.IgnoreForRoomClear = false;
                        target.DiesOnCollison = false;
                        if (UnityEngine.Random.value < 0.2f) {
                            target.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                            ExpandExplodeOnDeath CachedExploder = target.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                            CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                        }
                    } else {
                        target.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                        ExpandExplodeOnDeath CachedExploder = target.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                        CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                        target.AlwaysShowOffscreenArrow = true;
                        target.DiesOnCollison = true;
                    }
                    target.behaviorSpeculator.RefreshBehaviors();
                    target.behaviorSpeculator.RegenerateCache();
                }

                if (source.gameObject.GetComponent<CrazedController>()) {
                    CrazedController crazedController = target.gameObject.AddComponent<CrazedController>();
                    ExpandUtility.DuplicateComponent(crazedController, source.gameObject.GetComponent<CrazedController>());
                }

                // Fix missing magic circle thingy on wizards/jammerlengo/necromancer
                if (source.aiAnimator.OtherVFX != null && source.aiAnimator.OtherVFX.Count > 0) {
                    target.aiAnimator.OtherVFX = new List<AIAnimator.NamedVFXPool>();

                    foreach (AIAnimator.NamedVFXPool vfx in source.aiAnimator.OtherVFX) {
                        AIAnimator.NamedVFXPool NewVFXEntry = new AIAnimator.NamedVFXPool() { name = vfx.name, vfxPool = vfx.vfxPool };
                        if (vfx.anchorTransform) {
                            GameObject anchorObject = UnityEngine.Object.Instantiate(vfx.anchorTransform.gameObject, target.transform.position, Quaternion.identity);
                            anchorObject.name.Replace("(Clone)", string.Empty);
                            anchorObject.name.Replace("(clone)", string.Empty);
                            anchorObject.transform.SetParent(target.transform, false);
                            anchorObject.transform.localPosition = vfx.anchorTransform.localPosition;
                            NewVFXEntry.anchorTransform = anchorObject.transform;
                        }
                        target.aiAnimator.OtherVFX.Add(NewVFXEntry);
                    }
                }
            } catch (Exception) { }
        }


        public GameObject SpawnGlitchedBulletKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletManPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                /*if (CachedSourceEnemyObject == SunburstPrefab.gameObject) {
                    CachedGlitchEnemyActor.GetComponent<DashBehavior>().UnityEngine.Object.Destroy();
                    CachedGlitchEnemyActor.GetComponent<SequentialAttackBehaviorGroup>().UnityEngine.Object.Destroy();
                }*/

                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            CachedGlitchEnemyActor.EnemyId = 601;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000001";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);


            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            // CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            // CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;
            
            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedCultist(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(CultistPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 602;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000002";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);

            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedGhost(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(GhostPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 603;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000003";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);

            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedArrowheadKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(ArrowheadManPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 604;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000004";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);

            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedSniperKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletRifleManPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 605;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000005";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedAshBulletKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(AshBulletManPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 606;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000006";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedAshShotGunKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(AshBulletShotgunManPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 607;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000007";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedCardinalBulletKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletCardinalPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 608;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000008";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBulletMachineGunMan(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletMachineGunManPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {                
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 609;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000009";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBulletManDevil(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletManDevilPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 610;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000010";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBulletManShroomed(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletManShroomedPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 611;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000011";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;


            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBulletSkeletonHelmet(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletSkeletonHelmetPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 612;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000012";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedVeteranShotGunKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletShotgunManSawedOffPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 613;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000013";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedMutantShotGunKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletShotgunManMutantPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 614;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000014";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedMutantBulletKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletManMutantPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 615;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000015";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedShotGrubKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletShotgrubManPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 615;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000015";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBird(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BirdPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {                
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }
                // CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 620;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000020";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBulletShark(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletSharkPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 621;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000021";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBlueLeadWizard(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(LeadWizardBluePrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);            
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CrazedController>());

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 619;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000019";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedNecromancer(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(NecromancerPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);   
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CrazedController>());

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 621;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000021";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedJamromancer(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(JamromancerPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CrazedController>());            

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 624;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000024";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedWizardRed(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(WizardRedPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 626;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000026";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedWizardYellow(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(WizardYellowPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 627;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000027";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedWizardBlue(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(WizardBluePrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 628;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000028";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedSunburst(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(SunburstPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 629;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000029";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") { CachedGlitchEnemyActor.DiesOnCollison = true; }
            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBullatGiant(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BullatGiantPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            // ValidSourceEnemies.Add(PowderSkullBlackPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            SpawnEnemyOnDeath CachedSpawnEnemyOnDeath = CachedGlitchEnemyActor.GetComponent<SpawnEnemyOnDeath>();
            CachedSpawnEnemyOnDeath.enemyGuidsToSpawn = ExpandLists.SpawnEnemyOnDeathGUIDList;
            // CachedSpawnEnemyOnDeath.enemySelection = SpawnEnemyOnDeath.EnemySelection.Random;
            // CachedSpawnEnemyOnDeath.minSpawnCount = 2;
            // CachedSpawnEnemyOnDeath.maxSpawnCount = 3;

            CachedGlitchEnemyActor.EnemyId = 630;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000030";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedResourcefulRat(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            // GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(ResourcefulRatBossPrefab);
            // DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            GameObject CachedTargetEnemyObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").gameObject, CurrentRoom, position, true, awakenAnimType, autoEngage);
            GameObject CachedSourceEnemyObject;

            if (CachedTargetEnemyObject == null) { return null; }

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor, true);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 1002;
            CachedGlitchEnemyActor.EnemyGuid = "ff000000000000000000000000001002";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<ResourcefulRatController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<ResourcefulRatDeathController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<ResourcefulRatRewardRoomController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GenericIntroDoer>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<ResourcefulRatIntroDoer>());

            CachedGlitchEnemyActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.None;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth());
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;            
            CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>();
            ExpandSpawnGlitchObjectOnDeath ObjectSpawnerComponent = CachedGlitchEnemyActor.healthHaver.gameObject.GetComponent<ExpandSpawnGlitchObjectOnDeath>();
            ObjectSpawnerComponent.spawnRatCorpse = true;
            ObjectSpawnerComponent.ratCorpseSpawnsItemOnExplosion = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Awakening;
                        
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;
            CachedGlitchEnemyActor.StealthDeath = true;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            // DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            CachedGlitchEnemyActor.ConfigureOnPlacement(CurrentRoom);
            // UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return CachedTargetEnemyObject;
        }
        public GameObject SpawnGlitchedBlockner(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(ManfredsRivalPrefab);
            GameObject CachedSourceEnemyObject;

            if (CachedTargetEnemyObject == null) { return null; }

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);

            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 1004;
            CachedGlitchEnemyActor.EnemyGuid = "ff000000000000000000000000001004";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<ManfredsRivalIntroDoer>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<ManfredsRivalKnightsController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<ManfredsRivalNPCKnightsController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GenericIntroDoer>());


            CachedGlitchEnemyActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.None;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), null, false);
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;


            


            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBandanaBulletKin(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletManBandanaPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);

            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 600;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000000";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedAngryBook(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> RandomSourceEnemyPrefabs = new List<GameObject>();
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            RandomSourceEnemyPrefabs.Add(AngryBookPrefab);
            RandomSourceEnemyPrefabs.Add(AngryBookBluePrefab);
            RandomSourceEnemyPrefabs.Add(AngryBookGreenPrefab);
            RandomSourceEnemyPrefabs = RandomSourceEnemyPrefabs.Shuffle();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BraveUtility.RandomElement(RandomSourceEnemyPrefabs));
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede" && CachedEnemyActor.EnemyGuid != "19b420dec96d4e9ea4aebc3398c0ba7a") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f && CachedEnemyActor.EnemyGuid != "19b420dec96d4e9ea4aebc3398c0ba7a") { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = UnityEngine.Random.Range(900, 1000);
            CachedGlitchEnemyActor.EnemyGuid = ("f0ff000000000000000000000000f" + UnityEngine.Random.Range(100,999));
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBeadie(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(FloatingEyePrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            // SpecialSourceEnemies.Add(SunburstPrefab);
            // SpecialSourceEnemies.Add(PowderSkullBlackPrefab);

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<ExplodeOnDeath>());
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) {
                        UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<ExplodeOnDeath>());
                        CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 641;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000041";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;


            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedChicken(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(WolfPrefab);
            ValidSourceEnemies.Add(TinyBlobulordObject);
            ValidSourceEnemies.Add(BulletKingsToadieObject);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(ChickenPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }
            
            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedSourceEnemyObject != TinyBlobulordObject) {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) {
                        CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                        ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                        CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                    }
                }
                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.CollideWithTileMap = true;
            specRigidbody.PixelColliders.Add(CachedEnemyActor.specRigidbody.GroundPixelCollider);
            specRigidbody.GroundPixelCollider.Enabled = true;
            // specRigidbody.ClearFrameSpecificCollisionExceptions();
            // specRigidbody.ClearSpecificCollisionExceptions();
            // specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));
            specRigidbody.ForceRegenerate(true, true);
            specRigidbody.RegenerateCache();

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CuccoController>());

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            

            CachedGlitchEnemyActor.EnemyId = UnityEngine.Random.Range(700, 2000);
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000050";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            


            if (CachedSourceEnemyObject == WolfPrefab) { CachedGlitchEnemyActor.DiesOnCollison = false; }
            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            if (CachedEnemyActor.EnemyGuid == "4db03291a12144d69fe940d5a01de376") { CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState; };

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedSnake(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies.Add(TinyBlobulordObject);
            ValidSourceEnemies.Add(BulletKingsToadieObject);

            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(SnakePrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies);

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }
            
            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            

            CachedGlitchEnemyActor.EnemyId = UnityEngine.Random.Range(700, 2000);
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000051";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            
            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBlizzbulon(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BlizzbulonPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            // ValidSourceEnemies.Add(PowderSkullBlackPrefab);

            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 652;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000052";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedRandomBlob(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject BlobulonObject = BlobulonPrefab;

            if (BraveUtility.RandomBool()) { BlobulonObject = PoisbulonPrefab; }

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BlobulonObject);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            // ValidSourceEnemies.Add(PowderSkullBlackPrefab);

            CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            SpawnEnemyOnDeath CachedSpawnEnemyOnDeath = CachedGlitchEnemyActor.GetComponent<SpawnEnemyOnDeath>();
            CachedSpawnEnemyOnDeath.enemyGuidsToSpawn = ExpandLists.SpawnEnemyOnDeathGUIDList;
            CachedSpawnEnemyOnDeath.enemySelection = SpawnEnemyOnDeath.EnemySelection.Random;
            CachedSpawnEnemyOnDeath.minSpawnCount = 2;
            CachedSpawnEnemyOnDeath.maxSpawnCount = 3;

            CachedGlitchEnemyActor.EnemyId = 653;
            CachedGlitchEnemyActor.EnemyGuid = "f0000000000000000000000000000053";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            if (CachedSourceEnemyObject == BlobulonPrefab) {
                ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);
            } else {
                ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);
            }

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedRat(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>() {
                GhostPrefab,
                // CultistPrefab,
                ArrowheadManPrefab,
                BulletRifleManPrefab,
                AshBulletManPrefab,
                AshBulletShotgunManPrefab,
                BulletMachineGunManPrefab,
                BulletManDevilPrefab,
                BulletManShroomedPrefab,
                BulletSkeletonHelmetPrefab,
                BulletShotgunManSawedOffPrefab,
                BulletShotgunManMutantPrefab,
                BulletManKaliberPrefab,
                BulletShotgunManCowboyPrefab,
                BulletShotgrubManPrefab,
                BulletManBandanaPrefab,
                FloatingEyePrefab,
                WolfPrefab,
            };


            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BraveUtility.RandomElement(RatPrefabs));
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }
            
            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {                
                CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
               
                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.CollideWithTileMap = true;
            // specRigidbody.PixelColliders.Add(CachedEnemyActor.specRigidbody.GroundPixelCollider);
            // specRigidbody.GroundPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));
            specRigidbody.ForceRegenerate(true, true);
            specRigidbody.RegenerateCache();            

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = CachedEnemyActor.State;
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth());
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.IsWorthShootingAt = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.IsHarmlessEnemy = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;            
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            

            CachedGlitchEnemyActor.EnemyId = UnityEngine.Random.Range(800, 2000);
            CachedGlitchEnemyActor.EnemyGuid = Guid.NewGuid().ToString();
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            // if (CachedSourceEnemyObject == WolfPrefab) { CachedGlitchEnemyActor.DiesOnCollison = false; }
            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject CorruptedRat = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            // CurrentRoom.RegisterEnemy(CorruptedRat.GetComponent<AIActor>());
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return CorruptedRat;
        }

        public GameObject SpawnGlitchedBigEnemy(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> RandomSourceEnemyPrefabs = new List<GameObject>();
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            RandomSourceEnemyPrefabs.Add(PhaseSpiderPrefab);
            RandomSourceEnemyPrefabs.Add(BombsheePrefab);
            RandomSourceEnemyPrefabs.Add(GunNutPrefab);
            RandomSourceEnemyPrefabs.Add(GunNutSpectrePrefab);
            RandomSourceEnemyPrefabs.Add(GunNutChainPrefab);


            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BraveUtility.RandomElement(RandomSourceEnemyPrefabs), new Vector2(0,0).ToVector3ZUp(1f), Quaternion.identity);


            if (CachedTargetEnemyObject == GunNutSpectrePrefab | 
                CachedTargetEnemyObject == GunNutChainPrefab | 
                CachedTargetEnemyObject == BombsheePrefab)
            {
                ValidSourceEnemies.Add(GhostPrefab);
                ValidSourceEnemies.Add(CultistPrefab);
                ValidSourceEnemies.Add(ArrowheadManPrefab);
                ValidSourceEnemies.Add(BulletRifleManPrefab);
                ValidSourceEnemies.Add(AshBulletManPrefab);
                ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
                ValidSourceEnemies.Add(BulletMachineGunManPrefab);
                ValidSourceEnemies.Add(BulletManDevilPrefab);
                ValidSourceEnemies.Add(BulletManShroomedPrefab);
                ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
                ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
                ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
                ValidSourceEnemies.Add(BulletManKaliberPrefab);
                ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
                ValidSourceEnemies.Add(BulletShotgrubManPrefab);
                ValidSourceEnemies.Add(BulletManBandanaPrefab);
                ValidSourceEnemies.Add(FloatingEyePrefab);
                ValidSourceEnemies.Add(GrenadeGuyPrefab);
            } else {
                ValidSourceEnemies.Add(GrenadeGuyPrefab);
            }

            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }
            
            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
               if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede" && CachedGlitchEnemyActor.EnemyGuid != "19b420dec96d4e9ea4aebc3398c0ba7a") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f && CachedGlitchEnemyActor.EnemyGuid != "19b420dec96d4e9ea4aebc3398c0ba7a") {
                        CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            if (UnityEngine.Random.value <= 0.1f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            if (CachedGlitchEnemyActor.gameObject.GetComponentInChildren<BulletLimbController>() != null) {
                UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponentInChildren<BulletLimbController>());
            }

            CachedGlitchEnemyActor.EnemyId = UnityEngine.Random.Range(700, 800);
            CachedGlitchEnemyActor.EnemyGuid = ("ff000000000000000000000000000" + UnityEngine.Random.Range(100,999));
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = 0.5f;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedSourceEnemyObject == BulletKingsToadieObject | CachedSourceEnemyObject == WolfPrefab) {
                CachedGlitchEnemyActor.DiesOnCollison = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }

        public GameObject SpawnGlitchedCop(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(CopPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;


            try {
               if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 900;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000000";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedCopAndroid(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(CopAndroidPrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;


            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 902;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000002";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedSpaceTurtle(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(SuperSpaceTurtlePrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            if (UnityEngine.Random.value <= 0.25f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 901;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000001";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedCursedSpaceTurtle(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(CursedSuperSpaceTurtlePrefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();

            } catch (Exception) { }

            if (BraveUtility.RandomBool()) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }


            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());
            
            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 903;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000003";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            CachedGlitchEnemyActor.ForceBlackPhantomParticles = true;
            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            // tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            // ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);

            UnityEngine.Object.Destroy(CachedTargetEnemyObject);

            CachedGlitchEnemyActor.BecomeBlackPhantom();
            return targetObject;
        }
        public GameObject SpawnGlitchedPayDayShotGunGuy(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(PayDayShootPrefab);

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();

            ValidSourceEnemies.Add(BulletManPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<PaydaySynergyProcessor>());

            if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 904;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000004";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            
            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedR2G2(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(R2G2Prefab);
            GameObject CachedSourceEnemyObject;

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();

            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(NecromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);
            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 905;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000005";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            
            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedPortableTurret(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(PortableTurretPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                UnityEngine.Object.Destroy(CachedGlitchEnemyActor.healthHaver.GetComponent<ExplodeOnDeath>());
                CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                CachedExploder.deathType = OnDeathBehavior.DeathType.Death;


                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<PortableTurretController>());
            
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;
                        
            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 906;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000006";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            CachedGlitchEnemyActor.BehaviorVelocity = CachedEnemyActor.BehaviorVelocity;
            CachedGlitchEnemyActor.healthHaver.ApplyDamage(80f, new Vector2(1, 1), "TrimHPCount", CoreDamageTypes.None, DamageCategory.Normal, true, null, true);

            

            /*if (autoEngage) {
                CachedGlitchEnemyActor.State = AIActor.ActorState.Awakening;
            } else {
                CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            }*/

            CachedGlitchEnemyActor.HitByEnemyBullets = true;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.CanTargetEnemies = false;

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBabyMimic(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BabyGoodMimicPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.specRigidbody;
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 907;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000007";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedDog(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(DogPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
               if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.specRigidbody;
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 908;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000008";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;


            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);
            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedWolf(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletKingsToadieObject);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(WolfPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.specRigidbody;
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 909;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000009";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;


            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();

            if (CachedSourceEnemyObject != WolfPrefab) {
                ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);
            } else {
                float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
                float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
                float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
                float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);
                ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);
            }
            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedSerJunkan(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            ValidSourceEnemies.Add(SerJunkanPrefab);
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(SerJunkanPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            
            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            bool isMaxLevelJunkan = false;
            int RandomJunkType = 0;
            if (CachedSourceEnemyObject != SerJunkanPrefab) { RandomJunkType = UnityEngine.Random.Range(0, 6); }

            if (UnityEngine.Random.value <= 0.15 && CachedSourceEnemyObject == SerJunkanPrefab) { isMaxLevelJunkan = true; }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            try {
                if (CachedSourceEnemyObject == SerJunkanPrefab && isMaxLevelJunkan && !CachedGlitchEnemyActor.gameActor.IsFlying) {
                    CachedGlitchEnemyActor.gameActor.SetIsFlying(true, "angel", false, true);
                }
            } catch (Exception) { }


            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            if (CachedTargetEnemyObject != SerJunkanPrefab) { 
                try {
                    if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                        CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                        ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                        CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                    } else {
                        if (UnityEngine.Random.value <= 0.2f | CachedSourceEnemyObject == SerJunkanPrefab) {
                            CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                            ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                            CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                        }
                    }

                    CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                    CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                    CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
                } catch (Exception) { }
                                
                UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());
                UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<SackKnightController>());
            } else {
                try {
                    if (CachedEnemyActor.aiShooter != null) {
                        AIBulletBank CachedGlitchEnemyBulletBank = CachedGlitchEnemyActor.GetComponent<AIBulletBank>();
                        CachedGlitchEnemyBulletBank = CachedEnemyActor.bulletBank;
                        CachedGlitchEnemyActor.bulletBank.Bullets = CachedEnemyActor.bulletBank.Bullets;
                        CachedGlitchEnemyActor.bulletBank.useDefaultBulletIfMissing = true;
                        if (CachedGlitchEnemyActor.aiShooter == null) { CachedGlitchEnemyActor.gameObject.AddComponent<AIShooter>(); }
                    }
                } catch (Exception) { }
                UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());
            }

            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.specRigidbody;
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 911;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000011";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;



            if (CachedSourceEnemyObject == SerJunkanPrefab) { 
                SackKnightController CachedSackKnight = CachedGlitchEnemyActor.GetComponent<SackKnightController>();
                AIAnimator CachedAnimator = CachedGlitchEnemyActor.GetComponent<AIAnimator>();
                if (isMaxLevelJunkan) {
                    CachedSackKnight.CurrentForm = SackKnightController.SackKnightPhase.ANGELIC_KNIGHT;
                    CachedAnimator.IdleAnimation.AnimNames[0] = "junk_a_idle_right";
                    CachedAnimator.IdleAnimation.AnimNames[1] = "junk_a_idle_left";
                    CachedAnimator.MoveAnimation.AnimNames[0] = "junk_a_idle_right";
                    CachedAnimator.MoveAnimation.AnimNames[1] = "junk_a_idle_left";
                    CachedAnimator.TalkAnimation.AnimNames[0] = "junk_a_talk_right";
                    CachedAnimator.TalkAnimation.AnimNames[1] = "junk_a_talk_left";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[0] = "junk_a_attack_right";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[1] = "junk_a_attack_left";
                } else {
                    CachedSackKnight.CurrentForm = SackKnightController.SackKnightPhase.KNIGHT_COMMANDER;
                    CachedAnimator.IdleAnimation.AnimNames[0] = "junk_shspc_idle_right";
                    CachedAnimator.IdleAnimation.AnimNames[1] = "junk_shspc_idle_left";
                    CachedAnimator.MoveAnimation.AnimNames[0] = "junk_shspc_move_right";
                    CachedAnimator.MoveAnimation.AnimNames[1] = "junk_shspc_move_left";
                    CachedAnimator.TalkAnimation.AnimNames[0] = "junk_shspc_talk_right";
                    CachedAnimator.TalkAnimation.AnimNames[1] = "junk_shspc_talk_left";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[0] = "junk_shspc_attack_right";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[1] = "junk_shspc_attack_left";
                }
            } else {
                AIAnimator CachedAnimator = CachedGlitchEnemyActor.GetComponent<AIAnimator>();

                if (RandomJunkType == 1) {
                    CachedAnimator.IdleAnimation.AnimNames[0] = "junk_h_idle_right";
                    CachedAnimator.IdleAnimation.AnimNames[1] = "junk_h_idle_left";
                    CachedAnimator.MoveAnimation.AnimNames[0] = "junk_h_move_right";
                    CachedAnimator.MoveAnimation.AnimNames[1] = "junk_h_move_left";
                    CachedAnimator.TalkAnimation.AnimNames[0] = "junk_h_talk_right";
                    CachedAnimator.TalkAnimation.AnimNames[1] = "junk_h_talk_left";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[0] = "junk_h_attack_right";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[1] = "junk_h_attack_left";
                }
                if (RandomJunkType == 2) {
                    CachedAnimator.IdleAnimation.AnimNames[0] = "junk_sh_idle_right";
                    CachedAnimator.IdleAnimation.AnimNames[1] = "junk_sh_idle_left";
                    CachedAnimator.MoveAnimation.AnimNames[0] = "junk_sh_move_right";
                    CachedAnimator.MoveAnimation.AnimNames[1] = "junk_sh_move_left";
                    CachedAnimator.TalkAnimation.AnimNames[0] = "junk_sh_talk_right";
                    CachedAnimator.TalkAnimation.AnimNames[1] = "junk_sh_talk_left";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[0] = "junk_sh_attack_right";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[1] = "junk_sh_attack_left";
                }
                if (RandomJunkType == 3) {
                    CachedAnimator.IdleAnimation.AnimNames[0] = "junk_shs_idle_right";
                    CachedAnimator.IdleAnimation.AnimNames[1] = "junk_shs_idle_left";
                    CachedAnimator.MoveAnimation.AnimNames[0] = "junk_shs_move_right";
                    CachedAnimator.MoveAnimation.AnimNames[1] = "junk_shs_move_left";
                    CachedAnimator.TalkAnimation.AnimNames[0] = "junk_shs_talk_right";
                    CachedAnimator.TalkAnimation.AnimNames[1] = "junk_shs_talk_left";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[0] = "junk_shs_attack_right";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[1] = "junk_shs_attack_left";
                }
                if (RandomJunkType == 4) {
                    CachedAnimator.IdleAnimation.AnimNames[0] = "junk_shsp_idle_right";
                    CachedAnimator.IdleAnimation.AnimNames[1] = "junk_shsp_idle_left";
                    CachedAnimator.MoveAnimation.AnimNames[0] = "junk_shsp_move_right";
                    CachedAnimator.MoveAnimation.AnimNames[1] = "junk_shsp_move_left";
                    CachedAnimator.TalkAnimation.AnimNames[0] = "junk_shsp_talk_right";
                    CachedAnimator.TalkAnimation.AnimNames[1] = "junk_shsp_talk_left";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[0] = "junk_shsp_attack_right";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[1] = "junk_shsp_attack_left";
                }
                if (RandomJunkType == 5) {
                    CachedAnimator.IdleAnimation.AnimNames[0] = "junk_shspcg_idle_right";
                    CachedAnimator.IdleAnimation.AnimNames[1] = "junk_shspcg_idle_left";
                    CachedAnimator.MoveAnimation.AnimNames[0] = "junk_shspcg_move_right";
                    CachedAnimator.MoveAnimation.AnimNames[1] = "junk_shspcg_move_left";
                    CachedAnimator.TalkAnimation.AnimNames[0] = "junk_shspcg_talk_right";
                    CachedAnimator.TalkAnimation.AnimNames[1] = "junk_shspcg_talk_left";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[0] = "junk_shspcg_attack_right";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[1] = "junk_shspcg_attack_left";
                }
                if (RandomJunkType > 5) {
                    SpeculativeRigidbody CachedRigidBody = CachedGlitchEnemyActor.GetComponent<SpeculativeRigidbody>();
                    CachedRigidBody.PixelColliders[0].ManualOffsetX = 30;
                    CachedRigidBody.PixelColliders[0].ManualOffsetY = 3;
                    CachedRigidBody.PixelColliders[0].ManualWidth = 17;
                    CachedRigidBody.PixelColliders[0].ManualHeight = 16;
                    CachedRigidBody.PixelColliders[1].ManualOffsetX = 30;
                    CachedRigidBody.PixelColliders[1].ManualOffsetY = 3;
                    CachedRigidBody.PixelColliders[1].ManualWidth = 17;
                    CachedRigidBody.PixelColliders[1].ManualHeight = 28;
                    CachedRigidBody.PixelColliders[0].Regenerate(CachedGlitchEnemyActor.transform, true, true);
                    CachedRigidBody.PixelColliders[1].Regenerate(CachedGlitchEnemyActor.transform, true, true);
                    CachedRigidBody.Reinitialize();

                    CachedAnimator.IdleAnimation.AnimNames[0] = "junk_g_idle_right";
                    CachedAnimator.IdleAnimation.AnimNames[1] = "junk_g_idle_left";
                    CachedAnimator.MoveAnimation.AnimNames[0] = "junk_g_move_right";
                    CachedAnimator.MoveAnimation.AnimNames[1] = "junk_g_move_left";
                    CachedAnimator.TalkAnimation.AnimNames[0] = "junk_g_talk_right";
                    CachedAnimator.TalkAnimation.AnimNames[1] = "junk_g_talk_left";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[0] = "junk_g_sword_right";
                    CachedAnimator.OtherAnimations[0].anim.AnimNames[1] = "junk_g_sword_left";
                }
            }

            if (UnityEngine.Random.value <= 0.3f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.aiActor.CorpseObject = CachedEnemyActor.aiActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();

            if (CachedSourceEnemyObject != SerJunkanPrefab) {
                ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);
            } else {
                float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
                float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
                float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
                float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);
                ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);
            }

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedCaterpillar(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(CaterpillarPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            
            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.specRigidbody;
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 912;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000012";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;


            if (CachedSourceEnemyObject == CaterpillarPrefab) {
                CachedGlitchEnemyActor.CollisionDamage = 0f;
                CachedGlitchEnemyActor.DiesOnCollison = true;
                // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            } else {
                CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
                CachedGlitchEnemyActor.EnemyScale = new Vector2(1.5f, 1.5f);
                CachedGlitchEnemyActor.procedurallyOutlined = false;
            }
            CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedRaccoon(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken, bool isNonGlitchedVersion = false) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(BulletKingsToadieObject);
            ValidSourceEnemies.Add(TinyBlobulordObject);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();
            

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(RaccoonPrefab);
            GameObject CachedSourceEnemyObject;
            if (isNonGlitchedVersion) {
                CachedSourceEnemyObject = TinyBlobulordObject.gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid != "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }
                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            if (!isNonGlitchedVersion && UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 913;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000013";
            if (isNonGlitchedVersion) {
                CachedGlitchEnemyActor.OverrideDisplayName = "Junk Raccoon";
                CachedGlitchEnemyActor.ActorName = "Junk Raccoon";
                CachedGlitchEnemyActor.name = "Junk Raccoon";
            } else {
                CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
                CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
                CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            }
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;

            if (CachedEnemyActor.EnemyGuid != "4d37ce3d666b4ddda8039929225b7ede") {
                CachedGlitchEnemyActor.DiesOnCollison = true;
            }

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            if (!isNonGlitchedVersion) { CachedGlitchEnemyActor.HitByEnemyBullets = true; }
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;


            if (!isNonGlitchedVersion) {
                tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
                ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite);
            }
            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedTurkey(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken, bool isNonGlitchedVersion = false) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(TinyBlobulordObject);
            ValidSourceEnemies.Add(BulletKingsToadieObject);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(TurkeyPrefab);
            GameObject CachedSourceEnemyObject;

            if (isNonGlitchedVersion) {
                CachedSourceEnemyObject = TinyBlobulordObject.gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                CachedExploder.deathType = OnDeathBehavior.DeathType.Death;                

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            if (!isNonGlitchedVersion && UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 914;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000014";
            if (isNonGlitchedVersion) {
                CachedGlitchEnemyActor.OverrideDisplayName = "Junk Turkey";
                CachedGlitchEnemyActor.ActorName = "Junk Turkey";
                CachedGlitchEnemyActor.name = "Junk Turkey";
            } else {
                CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
                CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
                CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            }
            if (CachedEnemyActor.EnemyGuid != "4d37ce3d666b4ddda8039929225b7ede") {
                CachedGlitchEnemyActor.DiesOnCollison = true;
            }

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            if (!isNonGlitchedVersion) { CachedGlitchEnemyActor.HitByEnemyBullets = true; }
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            if (!isNonGlitchedVersion) {
                tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
                ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);
            }

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBlanky(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(TinyBlobulordObject);
            ValidSourceEnemies.Add(BulletKingsToadieObject);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BlankyPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                CachedExploder.deathType = OnDeathBehavior.DeathType.Death;                

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.gameObject.GetComponent<SpeculativeRigidbody>();
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());

            if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 915;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000015";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            if (CachedEnemyActor.EnemyGuid != "4d37ce3d666b4ddda8039929225b7ede") {
                CachedGlitchEnemyActor.DiesOnCollison = true;
            }


            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.HitByEnemyBullets = true;
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBabyShelleton(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            ValidSourceEnemies.Add(GhostPrefab);
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BabyShelletonPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;
            CachedGlitchEnemyActor.AlwaysShowOffscreenArrow = true;

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                    ExpandExplodeOnDeath CachedExploder = CachedGlitchEnemyActor.healthHaver.GetComponent<ExpandExplodeOnDeath>();
                    CachedExploder.deathType = OnDeathBehavior.DeathType.Death;
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }



            SpeculativeRigidbody specRigidbody = CachedGlitchEnemyActor.specRigidbody;
            specRigidbody.PrimaryPixelCollider.Enabled = true;
            specRigidbody.HitboxPixelCollider.Enabled = true;
            specRigidbody.ClearFrameSpecificCollisionExceptions();
            specRigidbody.ClearSpecificCollisionExceptions();
            specRigidbody.RemoveCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<CompanionController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<AIBeamShooter>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.GetComponent<BasicBeamController>());

            if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), 0f, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.PreventAllDamage = false;
            CachedGlitchEnemyActor.IsNormalEnemy = true;
            CachedGlitchEnemyActor.ImmuneToAllEffects = false;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.EnemyId = 907;
            CachedGlitchEnemyActor.EnemyGuid = "a0000000000000000000000000000007";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.CanTargetEnemies = false;
            CachedGlitchEnemyActor.CanTargetPlayers = true;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RnadomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplyGlitchShader(GlitchActorSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RnadomColorIntensityFloat);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }


        public GameObject SpawnGlitchedBulletBros(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject;
            GameObject CachedSourceEnemyObject;

            if (BraveUtility.RandomBool()) {
                CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletBrosSmileyPrefab);
            } else {
                CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletBrosShadesPrefab);
            }

            if (CachedTargetEnemyObject == null) { return null; }

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            SpecialSourceEnemies.Clear();
            
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);

            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            if (UnityEngine.Random.value <= 0.1f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.EnemyId = 1001;
            CachedGlitchEnemyActor.EnemyGuid = "ff000000000000000000000000001001";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BulletBroDeathController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BulletBrosIntroDoer>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BroController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GenericIntroDoer>());
            CachedGlitchEnemyActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.None;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), null, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedGatlingGull(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(GatlingGullPrefab);
            GameObject CachedSourceEnemyObject;

            if (CachedTargetEnemyObject == null) { return null; }

            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            List<GameObject> SpecialSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);

            SpecialSourceEnemies.Add(IceCubeGuyPrefab);
            SpecialSourceEnemies.Add(GrenadeGuyPrefab);

            SpecialSourceEnemies = SpecialSourceEnemies.Shuffle();
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = BraveUtility.RandomElement(SpecialSourceEnemies).gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }


            CachedGlitchEnemyActor.EnemyId = 1003;
            CachedGlitchEnemyActor.EnemyGuid = "ff000000000000000000000000001003";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GatlingGullIntroDoer>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GatlingGullOutroDoer>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GatlingGullDeathController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GatlingGullCrowController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GenericIntroDoer>());


            CachedGlitchEnemyActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.None;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), null, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            if (UnityEngine.Random.value <= 0.1f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }


            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBeholster(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(IceCubeGuyPrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BeholsterPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedTargetEnemyObject == null) { return null; }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {                
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) {
                        CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                    }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
                
                if (UnityEngine.Random.value <= 0.2f) {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                }                
            } catch (Exception) { }

            if (UnityEngine.Random.value <= 0.1f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.EnemyId = 1005;
            CachedGlitchEnemyActor.EnemyGuid = "ff000000000000000000000000001005";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BeholsterController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BeholsterTentacleController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GenericIntroDoer>());


            CachedGlitchEnemyActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.None;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), null, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;

            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            if (CachedEnemyActor.EnemyGuid == "f155fd2759764f4a9217db29dd21b7eb") {
                CachedGlitchEnemyActor.gameObject.AddComponent<KillOnRoomClear>();
                CachedGlitchEnemyActor.IgnoreForRoomClear = true;
            }

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedBossDoorMimic(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BossDoorMimicPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedTargetEnemyObject == null) { return null; }
            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            if (UnityEngine.Random.value <= 0.1f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.EnemyId = 1007;
            CachedGlitchEnemyActor.EnemyGuid = "ff000000000000000000000000001007";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BossDoorMimicDeathController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BossDoorMimicIntroDoer>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GenericIntroDoer>());

            CachedGlitchEnemyActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.None;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), null, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;


            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;



            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }
        public GameObject SpawnGlitchedHighPriest(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(HighPriestPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedTargetEnemyObject == null) { return null; }
            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();

            } catch (Exception) { }

            if (UnityEngine.Random.value <= 0.1f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.EnemyId = 1008;
            CachedGlitchEnemyActor.EnemyGuid = "ff000000000000000000000000001008";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<HighPriestIntroDoer>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GenericIntroDoer>());

            CachedGlitchEnemyActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.None;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), null, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;



            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;
            
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }

        public GameObject SpawnGlitchedKillPillar(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);

            ValidSourceEnemies.Add(JamromancerPrefab);
            ValidSourceEnemies.Add(LeadWizardBluePrefab);
            ValidSourceEnemies.Add(GrenadeGuyPrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BossDoorMimicPrefab);
            GameObject CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject;

            if (CachedTargetEnemyObject == null) { return null; }
            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();
            /*UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BossStatueDeathController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BossStatueController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BossStatueDeathController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponentInChildren<BossStatuesController>(true));
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponentInChildren<BossStatuesIntroDoer>(true));
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponentInChildren<GenericIntroDoer>(true));*/

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);
            
            try {
                CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();

            } catch (Exception) { }

            if (UnityEngine.Random.value <= 0.1f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }

            CachedGlitchEnemyActor.EnemyId = 1009;
            CachedGlitchEnemyActor.EnemyGuid = "ff000000000000000000000000001009";
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);

            CachedGlitchEnemyActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.None;
            CachedGlitchEnemyActor.healthHaver.OnlyAllowSpecialBossDamage = false;
            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(CachedEnemyActor.healthHaver.GetCurrentHealth(), null, false);
            CachedGlitchEnemyActor.healthHaver.minimumHealth = CachedEnemyActor.healthHaver.minimumHealth;

            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            CachedGlitchEnemyActor.HasBeenEngaged = false;
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BossDoorMimicDeathController>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<BossDoorMimicIntroDoer>());
            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.gameObject.GetComponent<GenericIntroDoer>());


            // if (Stats.randomEnemySizeEnabled) { ChaosEnemyResizer.Instance.EnemyScale(CachedGlitchEnemyActor, Vector2.one); }
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.OnEngagedVFX = CachedEnemyActor.OnEngagedVFX;
            CachedGlitchEnemyActor.OnEngagedVFXAnchor = CachedEnemyActor.OnEngagedVFXAnchor;

            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;
            

            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();
            // Have to go a different route to obtain individual Kill Pillar AIActors. ;)
            AIActorDummy KillPillarsDummy = KillPillarsPrefab.GetComponent<AIActorDummy>();
            AIActor KillPillerAIActor = KillPillarsDummy.realPrefab.GetComponent<BossStatuesController>().allStatues[UnityEngine.Random.Range(0, 3)].gameObject.GetComponent<AIActor>();

            ExpandUtility.ApplyCustomTexture(CachedGlitchEnemyActor, prebuiltCollection: KillPillerAIActor.sprite.Collection);

            CachedGlitchEnemyActor.specRigidbody.PixelColliders = KillPillerAIActor.specRigidbody.PixelColliders;
            CachedGlitchEnemyActor.specRigidbody.RegenerateColliders = true;

            ExpandShaders.Instance.ApplySuperGlitchShader(GlitchActorSprite, CachedEnemyActor);

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            return targetObject;
        }

        public GameObject SpawnGlitchedObjectAsEnemy(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            GameObject CachedTargetEnemyObject = UnityEngine.Object.Instantiate(BulletManPrefab);
            GameObject CachedSourceEnemyObject;
            
            List<GameObject> ValidSourceEnemies = new List<GameObject>();
            ValidSourceEnemies.Clear();
            ValidSourceEnemies.Add(CultistPrefab);
            ValidSourceEnemies.Add(ArrowheadManPrefab);
            ValidSourceEnemies.Add(BulletRifleManPrefab);
            ValidSourceEnemies.Add(AshBulletManPrefab);
            ValidSourceEnemies.Add(AshBulletShotgunManPrefab);
            ValidSourceEnemies.Add(BulletMachineGunManPrefab);
            ValidSourceEnemies.Add(BulletManDevilPrefab);
            ValidSourceEnemies.Add(BulletManShroomedPrefab);
            ValidSourceEnemies.Add(BulletSkeletonHelmetPrefab);
            ValidSourceEnemies.Add(BulletShotgunManSawedOffPrefab);
            ValidSourceEnemies.Add(BulletShotgunManMutantPrefab);
            ValidSourceEnemies.Add(BulletManKaliberPrefab);
            ValidSourceEnemies.Add(BulletShotgunManCowboyPrefab);
            ValidSourceEnemies.Add(BulletShotgrubManPrefab);
            ValidSourceEnemies.Add(BulletManBandanaPrefab);
            ValidSourceEnemies.Add(FloatingEyePrefab);
            ValidSourceEnemies = ValidSourceEnemies.Shuffle();

            if (UnityEngine.Random.value <= 0.2f) {
                CachedSourceEnemyObject = GrenadeGuyPrefab.gameObject;
            } else {
                CachedSourceEnemyObject = BraveUtility.RandomElement(ValidSourceEnemies).gameObject; 
            }

            if (CachedSourceEnemyObject == null) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            AIActor CachedEnemyActor = CachedSourceEnemyObject.GetComponent<AIActor>();
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }

            AddOrReplaceAIActorConfig(CachedGlitchEnemyActor, CachedEnemyActor);

            try {
                if (CachedEnemyActor.EnemyGuid == "4d37ce3d666b4ddda8039929225b7ede") {
                    CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>();
                } else {
                    if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); }
                }

                CachedGlitchEnemyActor.BehaviorOverridesVelocity = CachedEnemyActor.BehaviorOverridesVelocity;
                CachedGlitchEnemyActor.behaviorSpeculator.RefreshBehaviors();
                CachedGlitchEnemyActor.behaviorSpeculator.RegenerateCache();
            } catch (Exception) { }

            CachedGlitchEnemyActor.EnemyId = 9900;
            CachedGlitchEnemyActor.EnemyGuid = Guid.NewGuid().ToString();
            CachedGlitchEnemyActor.OverrideDisplayName = ("Corrupted " + CachedEnemyActor.GetActorName());
            CachedGlitchEnemyActor.ActorName = ("Corrupted " + CachedGlitchEnemyActor.GetActorName());
            CachedGlitchEnemyActor.name = ("Corrupted " + CachedGlitchEnemyActor.name);


            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.KnockbackVelocity = CachedEnemyActor.KnockbackVelocity;
            CachedGlitchEnemyActor.PreventDeathKnockback = CachedEnemyActor.PreventDeathKnockback;
            CachedGlitchEnemyActor.IsWorthShootingAt = CachedEnemyActor.IsWorthShootingAt;
            CachedGlitchEnemyActor.CollisionKnockbackStrength = CachedEnemyActor.CollisionKnockbackStrength;
            CachedGlitchEnemyActor.EnemyCollisionKnockbackStrengthOverride = CachedEnemyActor.EnemyCollisionKnockbackStrengthOverride;
            CachedGlitchEnemyActor.healthHaver.spawnBulletScript = CachedEnemyActor.healthHaver.spawnBulletScript;
            CachedGlitchEnemyActor.healthHaver.SuppressDeathSounds = CachedEnemyActor.healthHaver.SuppressDeathSounds;

            CachedGlitchEnemyActor.healthHaver.ManualDeathHandling = CachedEnemyActor.healthHaver.ManualDeathHandling;
            CachedGlitchEnemyActor.healthHaver.deathEffect = CachedEnemyActor.healthHaver.deathEffect;
            CachedGlitchEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath = CachedEnemyActor.healthHaver.noCorpseWhenBulletScriptDeath;
            CachedGlitchEnemyActor.StealthDeath = CachedEnemyActor.StealthDeath;
            CachedGlitchEnemyActor.SpeculatorDelayTime = CachedEnemyActor.SpeculatorDelayTime;
            CachedGlitchEnemyActor.State = AIActor.ActorState.Inactive;
            
            CachedGlitchEnemyActor.IgnoreForRoomClear = false;
            CachedGlitchEnemyActor.OnHandleRewards = CachedEnemyActor.OnHandleRewards;
            CachedGlitchEnemyActor.CustomChestTable = CachedEnemyActor.CustomChestTable;
            CachedGlitchEnemyActor.CustomLootTable = CachedEnemyActor.CustomLootTable;
            CachedGlitchEnemyActor.CustomLootTableMinDrops = CachedEnemyActor.CustomLootTableMinDrops;
            CachedGlitchEnemyActor.SpawnLootAtRewardChestPos = CachedEnemyActor.SpawnLootAtRewardChestPos;
            CachedGlitchEnemyActor.ManualKnockbackHandling = CachedEnemyActor.ManualKnockbackHandling;
            CachedGlitchEnemyActor.BaseMovementSpeed = CachedEnemyActor.BaseMovementSpeed;
            CachedGlitchEnemyActor.MovementSpeed = CachedEnemyActor.MovementSpeed;
            CachedGlitchEnemyActor.OnCorpseVFX = CachedEnemyActor.OnCorpseVFX;
            CachedGlitchEnemyActor.CollisionVFX = CachedEnemyActor.CollisionVFX;
            CachedGlitchEnemyActor.CollisionSetsPlayerOnFire = CachedEnemyActor.CollisionSetsPlayerOnFire;
            CachedGlitchEnemyActor.CollisionDamage = CachedEnemyActor.CollisionDamage;
            CachedGlitchEnemyActor.CollisionDamageTypes = CachedEnemyActor.CollisionDamageTypes;
            CachedGlitchEnemyActor.NonActorCollisionVFX = CachedEnemyActor.NonActorCollisionVFX;
            CachedGlitchEnemyActor.TryDodgeBullets = CachedEnemyActor.TryDodgeBullets;
            CachedGlitchEnemyActor.AvoidRadius = CachedEnemyActor.AvoidRadius;
            CachedGlitchEnemyActor.CorpseObject = CachedEnemyActor.CorpseObject;
            CachedGlitchEnemyActor.HitByEnemyBullets = BraveUtility.RandomBool();
            CachedGlitchEnemyActor.PreventFallingInPitsEver = CachedEnemyActor.PreventFallingInPitsEver;
            CachedGlitchEnemyActor.UseMovementAudio = CachedEnemyActor.UseMovementAudio;
            CachedGlitchEnemyActor.EnemySwitchState = CachedEnemyActor.EnemySwitchState;

            
            tk2dBaseSprite GlitchActorSprite = CachedGlitchEnemyActor.sprite.GetComponent<tk2dBaseSprite>();

            List<TalkDoerLite> NPCList = new List<TalkDoerLite>() {
                ExpandObjectDatabase .NPCEvilMuncher.GetComponent<TalkDoerLite>(),
                ExpandObjectDatabase .NPCGunMuncher.GetComponent<TalkDoerLite>(),
                ExpandObjectDatabase .NPCOldMan.GetComponent<TalkDoerLite>(),
                ExpandObjectDatabase .NPCTonic.GetComponent<TalkDoerLite>(),
                ExpandObjectDatabase .NPCTruthKnower.GetComponent<TalkDoerLite>(),
                ExpandObjectDatabase .NPCCursola.GetComponent<TalkDoerLite>()
            };

            List<tk2dBaseSprite> OtherObjectsList = new List<tk2dBaseSprite>() {
                ExpandObjectDatabase .ConvictPastCrowdNPC_01.GetComponent<tk2dBaseSprite>(),
                ExpandObjectDatabase .PlayerCorpse.GetComponent<tk2dBaseSprite>(),
                ExpandObjectDatabase .TimefallCorpse.GetComponent<tk2dBaseSprite>(),
                ExpandPrefabs.Teleporter_Info_Sign.GetComponent<tk2dBaseSprite>(),
                ExpandPrefabs.PlayerLostRatNote.GetComponent<tk2dBaseSprite>(),
                ExpandObjectDatabase .LockedDoor.GetComponent<tk2dBaseSprite>(),
                ExpandPrefabs.MouseTrap1.GetComponent<tk2dBaseSprite>()
            };

            bool isConvictPastCrowdNPC = false;

            TalkDoerLite m_SelectedNPC = BraveUtility.RandomElement(NPCList);
            tk2dBaseSprite m_SelectedNPCSprite = m_SelectedNPC.GetComponent<tk2dBaseSprite>();

            if (UnityEngine.Random.value <= 0.2f) {
                m_SelectedNPCSprite = BraveUtility.RandomElement(OtherObjectsList);
                isConvictPastCrowdNPC = true;
            }

            ExpandUtility.ApplyCustomTexture(CachedGlitchEnemyActor, prebuiltCollection: m_SelectedNPCSprite.Collection);
            
            IntVector2 RigidBodyUnitSize = IntVector2.One;

            if (isConvictPastCrowdNPC) {
                if (m_SelectedNPCSprite.name.StartsWith("Dancer") | m_SelectedNPCSprite == OtherObjectsList[3]) {
                    RigidBodyUnitSize = new IntVector2(3, 4);
                } else if(m_SelectedNPCSprite == OtherObjectsList[1]) {
                    RigidBodyUnitSize = new IntVector2(1, 2);
                } else if(m_SelectedNPCSprite == OtherObjectsList[2]) {
                    RigidBodyUnitSize = new IntVector2(1, 1);
                } else if(m_SelectedNPCSprite == OtherObjectsList[5]) {
                    RigidBodyUnitSize = new IntVector2(3, 2);
                }
            } else if (m_SelectedNPC == NPCList[0] | m_SelectedNPC == NPCList[1]) {
                RigidBodyUnitSize = new IntVector2(3, 2);
            } else if (m_SelectedNPC == NPCList[2] | m_SelectedNPC == NPCList[4] | m_SelectedNPC == NPCList[5]) {  
                RigidBodyUnitSize = new IntVector2(2, 2);
            } else if (m_SelectedNPC == NPCList[3]) {
                RigidBodyUnitSize = new IntVector2(2, 3);
            }

            IntVector2 m_RigidBodyPixelSize = new IntVector2(RigidBodyUnitSize.x * 16, RigidBodyUnitSize.y * 16);

            CachedGlitchEnemyActor.specRigidbody = ExpandUtility.GenerateNewEnemyRigidBody(CachedGlitchEnemyActor, IntVector2.Zero, m_RigidBodyPixelSize);

            UnityEngine.Object.Destroy(CachedGlitchEnemyActor.aiAnimator);
            CachedGlitchEnemyActor.specRigidbody.RegenerateColliders = true;

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);            
            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            /*if (!isConvictPastCrowdNPC) {
                ETGModConsole.Log(m_SelectedNPC.name);
            } else {
                ETGModConsole.Log(m_SelectedNPCSprite.name);
            }*/
            
            return targetObject;
        }
        
        public GameObject SpawnGlitchedPlayerAsEnemy(RoomHandler CurrentRoom, IntVector2 position, bool autoEngage = false, AIActor.AwakenAnimationType awakenAnimType = AIActor.AwakenAnimationType.Awaken) {
            PlayerController m_SelectedPlayer = GameManager.Instance.PrimaryPlayer;

            AIActor CachedEnemyActor = CultistPrefab.GetComponent<AIActor>();
            GameObject m_DummyCorpseObject = null;

            GameObject CachedTargetEnemyObject = new GameObject("Corrupted Player Mimic") { layer = 28 };
            tk2dSprite newSprite = CachedTargetEnemyObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(newSprite, (m_SelectedPlayer.sprite as tk2dSprite));
            
            // If Player sprite was flipped (aka, player aiming/facing towards the left), then this could cause sprite being shifted left on AIActor.
            // Always set false to ensure this doesn't happen.
            newSprite.FlipX = false;

            ExpandUtility.GenerateAIActorTemplate(CachedTargetEnemyObject, out m_DummyCorpseObject, "Corrupted Player Mimic", Guid.NewGuid().ToString(), null, m_SelectedPlayer.gunAttachPoint.gameObject, instantiateCorpseObject: false, ExternalCorpseObject: CachedEnemyActor.CorpseObject);
            
            AIActor CachedGlitchEnemyActor = CachedTargetEnemyObject.GetComponent<AIActor>();

            if (!CachedEnemyActor) {
                if (ExpandSettings.debugMode) ETGModConsole.Log("[DEBUG] ERROR: Source object for random donor enemy is null!", false);
                return null;
            }

            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("Spawning '" + CachedGlitchEnemyActor.ActorName + "' with GUID: " + CachedGlitchEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy spawned has it's behaviors replaced with the enemy: '" + CachedEnemyActor.ActorName + "' with GUID: " + CachedEnemyActor.EnemyGuid + " .", false);
                ETGModConsole.Log("The enemy was spawned in the following room: '" + CurrentRoom.GetRoomName(), false);
            }
            
            // try { if (UnityEngine.Random.value <= 0.2f) { CachedGlitchEnemyActor.gameObject.AddComponent<ExpandExplodeOnDeath>(); } } catch (Exception) { }
            
            CachedGlitchEnemyActor.DoDustUps = true;
            CachedGlitchEnemyActor.DustUpInterval = 0.4f;
            CachedGlitchEnemyActor.MovementSpeed = 3.5f;
            CachedGlitchEnemyActor.EnemySwitchState = "Gun Cultist";
            

            List<tk2dSpriteAnimationClip> m_AnimationClips = new List<tk2dSpriteAnimationClip>();
            foreach (tk2dSpriteAnimationClip clip in m_SelectedPlayer.spriteAnimator.Library.clips) {
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
            if (!CachedGlitchEnemyActor.spriteAnimator.Library) { CachedGlitchEnemyActor.spriteAnimator.Library = CachedTargetEnemyObject.AddComponent<tk2dSpriteAnimation>(); }
            if (m_AnimationClips.Count > 0) { CachedGlitchEnemyActor.spriteAnimator.Library.clips = m_AnimationClips.ToArray(); }
            CachedGlitchEnemyActor.spriteAnimator.DefaultClipId = 0;
            CachedGlitchEnemyActor.spriteAnimator.playAutomatically = true;
            
            if (CachedGlitchEnemyActor.aiAnimator) {
                CachedGlitchEnemyActor.aiAnimator.facingType = AIAnimator.FacingType.Target;
                CachedGlitchEnemyActor.aiAnimator.directionalType = AIAnimator.DirectionalType.Sprite;
                CachedGlitchEnemyActor.aiAnimator.faceSouthWhenStopped = false;
                CachedGlitchEnemyActor.aiAnimator.faceTargetWhenStopped = true;
                CachedGlitchEnemyActor.aiAnimator.HitType = AIAnimator.HitStateType.Basic;
                CachedGlitchEnemyActor.aiAnimator.IdleAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                    Prefix = "idle",
                    AnimNames = new string[] { "idle_backward", "idle" },
                    Flipped = new DirectionalAnimation.FlipType[2],                    
                };
                CachedGlitchEnemyActor.aiAnimator.MoveAnimation = new DirectionalAnimation() {
                    Type = DirectionalAnimation.DirectionType.TwoWayVertical,
                    Prefix = "run",
                    AnimNames = new string[] { "run_up", "run_down" },
                    Flipped = new DirectionalAnimation.FlipType[2],                    
                };
                CachedGlitchEnemyActor.aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>() {
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

            CachedGlitchEnemyActor.healthHaver.SetHealthMaximum(80);
            CachedGlitchEnemyActor.healthHaver.ForceSetCurrentHealth(80);

            if (m_SelectedPlayer.characterIdentity == PlayableCharacters.Eevee) { ExpandShaders.ApplyParadoxPlayerShader(CachedGlitchEnemyActor.sprite); }
            
            BehaviorSpeculator behaviorSpeculator = CachedTargetEnemyObject.AddComponent<BehaviorSpeculator>();
            behaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>();
            behaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>();
            behaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>();
            behaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>();
            behaviorSpeculator.OtherBehaviors = new List<BehaviorBase>();
            
            if (CachedEnemyActor.behaviorSpeculator.OverrideBehaviors.Count > 0) {
                foreach (OverrideBehaviorBase overrideBehavior in CachedEnemyActor.gameObject.GetComponent<BehaviorSpeculator>().OverrideBehaviors) {
                    behaviorSpeculator.OverrideBehaviors.Add(overrideBehavior);
                }
            }
            if (CachedEnemyActor.behaviorSpeculator.TargetBehaviors.Count > 0) {
                foreach (TargetBehaviorBase targetBehavior in CachedEnemyActor.gameObject.GetComponent<BehaviorSpeculator>().TargetBehaviors) {
                    behaviorSpeculator.TargetBehaviors.Add(targetBehavior);
                }
            }
            if (CachedEnemyActor.behaviorSpeculator.MovementBehaviors.Count > 0) {
                foreach (MovementBehaviorBase movementBehavior in CachedEnemyActor.gameObject.GetComponent<BehaviorSpeculator>().MovementBehaviors) {
                    behaviorSpeculator.MovementBehaviors.Add(movementBehavior);
                }
            }
            if (CachedEnemyActor.behaviorSpeculator.AttackBehaviors.Count > 0) {
                foreach (AttackBehaviorBase attackBehavior in CachedEnemyActor.gameObject.GetComponent<BehaviorSpeculator>().AttackBehaviors) {
                    behaviorSpeculator.AttackBehaviors.Add(attackBehavior);
                }
            }
            if (CachedEnemyActor.behaviorSpeculator.OtherBehaviors.Count > 0) {
                foreach (BehaviorBase otherBehavior in CachedEnemyActor.gameObject.GetComponent<BehaviorSpeculator>().OtherBehaviors) {
                    behaviorSpeculator.OtherBehaviors.Add(otherBehavior);
                }
            }
            
            behaviorSpeculator.InstantFirstTick = CachedEnemyActor.behaviorSpeculator.InstantFirstTick;
            behaviorSpeculator.TickInterval = CachedEnemyActor.behaviorSpeculator.TickInterval;
            behaviorSpeculator.PostAwakenDelay = CachedEnemyActor.behaviorSpeculator.PostAwakenDelay;
            behaviorSpeculator.RemoveDelayOnReinforce = CachedEnemyActor.behaviorSpeculator.RemoveDelayOnReinforce;
            behaviorSpeculator.OverrideStartingFacingDirection = CachedEnemyActor.behaviorSpeculator.OverrideStartingFacingDirection;
            behaviorSpeculator.StartingFacingDirection = CachedEnemyActor.behaviorSpeculator.StartingFacingDirection;
            behaviorSpeculator.SkipTimingDifferentiator = CachedEnemyActor.behaviorSpeculator.SkipTimingDifferentiator;            

            // BehaviorSpeculator is a serialized object. You must build these lists (or create new empty lists) and save them before the game can instantiate it correctly!
            ISerializedObject m_TargetBehaviorSpeculatorSeralized = behaviorSpeculator;
            m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<UnityEngine.Object>();
            m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = new List<string>();
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = new List<string>();

            List<UnityEngine.Object> m_SourceSerializedObjectReferences = (CachedEnemyActor.behaviorSpeculator as ISerializedObject).SerializedObjectReferences;
            List<string> m_SourceSerializedStateKeys = (CachedEnemyActor.behaviorSpeculator as ISerializedObject).SerializedStateKeys;
            List<string> m_SourceSerializedStateValues = (CachedEnemyActor.behaviorSpeculator as ISerializedObject).SerializedStateValues;

            if (m_SourceSerializedObjectReferences != null && m_SourceSerializedObjectReferences.Count > 0) {
                foreach (UnityEngine.Object unityObject in m_SourceSerializedObjectReferences) {
                    m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences.Add(unityObject);
                }
            }
            if (m_SourceSerializedStateKeys != null && m_SourceSerializedStateKeys.Count > 0) {
                foreach (string stateKey in m_SourceSerializedStateKeys) {
                    m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys.Add(stateKey);
                }
            }
            if (m_SourceSerializedStateValues != null && m_SourceSerializedStateValues.Count > 0) {
                foreach (string stateValue in m_SourceSerializedStateValues) {
                    m_TargetBehaviorSpeculatorSeralized.SerializedStateValues.Add(stateValue);
                }
            }

            behaviorSpeculator.RegenerateCache();

            GameObject targetObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(CachedGlitchEnemyActor.gameObject, CurrentRoom, position, false, awakenAnimType, autoEngage);

            UnityEngine.Object.Destroy(CachedTargetEnemyObject);
            CachedEnemyActor = null;
            return targetObject;
        }        
    }
}

