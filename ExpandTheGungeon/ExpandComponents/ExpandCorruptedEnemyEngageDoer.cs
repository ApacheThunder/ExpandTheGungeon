using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCorruptedEnemyEngageDoer : CustomEngageDoer {
    
        public ExpandCorruptedEnemyEngageDoer() {
            /*ValidTransformEnemies = new List<string> {
                "4d37ce3d666b4ddda8039929225b7ede", // GrenadeGuyPrefab
                "f155fd2759764f4a9217db29dd21b7eb", // IceCubeGuyPrefab
                "699cd24270af4cd183d671090d8323a1", // KeybulletManPrefab
                "a446c626b56d4166915a4e29869737fd", // ChanceBulletManPrefab
                "ffdc8680bdaa487f8f31995539f74265", // SunburstPrefab
                "57255ed50ee24794b7aac1ac3cfb8a95", // CultistPrefab
                "4db03291a12144d69fe940d5a01de376", // GhostPrefab
                "05891b158cd542b1a5f3df30fb67a7ff", // ArrowheadManPrefab
                "31a3ea0c54a745e182e22ea54844a82d", // BulletRifleManPrefab
                "1a78cfb776f54641b832e92c44021cf2", // AshBulletManPrefab
                "1bd8e49f93614e76b140077ff2e33f2b", // AshBulletShotgunManPrefab
                "8bb5578fba374e8aae8e10b754e61d62", // BulletCardinalPrefab
                "db35531e66ce41cbb81d507a34366dfe", // BulletMachineGunManPrefab
                "5f3abc2d561b4b9c9e72b879c6f10c7e", // BulletManDevilPrefab
                "e5cffcfabfae489da61062ea20539887", // BulletManShroomedPrefab
                "95ec774b5a75467a9ab05fa230c0c143", // BulletSkeletonHelmetPrefab
                "2752019b770f473193b08b4005dc781f", // BulletShotgunManSawedOffPrefab
                "7f665bd7151347e298e4d366f8818284", // BulletShotgunManMutantPrefab
                "d4a9836f8ab14f3fadd0f597438b1f1f", // BulletManMutantPrefab
                "044a9f39712f456597b9762893fbc19c", // BulletShotgrubManPrefab
                "88b6b6a93d4b4234a67844ef4728382c", // BulletManBandanaPrefab
                "7b0b1b6d9ce7405b86b75ce648025dd6", // FloatingEyePrefab
                "76bc43539fc24648bff4568c75c686d1", // ChickenPrefab
                "1386da0f42fb4bcabc5be8feb16a7c38", // SnakePrefab
                "c0ff3744760c4a2eb0bb52ac162056e6", // AngryBookPrefab
                "6f22935656c54ccfb89fca30ad663a64", // AngryBookBluePrefab
                "a400523e535f41ac80a43ff6b06dc0bf", // AngryBookGreenPrefab
                "c50a862d19fc4d30baeba54795e8cb93", // LeadWizardBluePrefab
                "ed37fa13e0fa4fcf8239643957c51293", // BirdPrefab
                "72d2f44431da43b8a3bae7d8a114a46d", // BulletSharkPrefab
                "b1540990a4f1480bbcb3bea70d67f60d", // NecromancerPrefab
                "19b420dec96d4e9ea4aebc3398c0ba7a", // BombsheePrefab
                "8b4a938cdbc64e64822e841e482ba3d2", // JamromancerPrefab
                "1a4872dafdb34fd29fe8ac90bd2cea67", // BullatGiantPrefab
                "0239c0680f9f467dbe5c4aab7dd1eca6", // BlobulonPrefab
                "e61cab252cfb435db9172adc96ded75f", // PoisbulonPrefab
                "98ca70157c364750a60f5e0084f9d3e2", // PhaseSpiderPrefab
                "c4fba8def15e47b297865b18e36cbef8", // WizardRedPrefab
                "206405acad4d4c33aac6717d184dc8d4", // WizardYellowPrefab
                "9b2cf2949a894599917d4d391a0b7394", // WizardBluePrefab
                "1cec0cdf383e42b19920787798353e46", // PowderSkullBlackPrefab
                "f020570a42164e2699dcf57cac8a495c", // BulletManKaliberPrefab
                "ddf12a4881eb43cfba04f36dd6377abb", // BulletShotgunManCowboyPrefab
                "c5b11bfc065d417b9c4d03a5e385fe2c", // BulletRifleProfessionalPrefab
                "70216cae6c1346309d86d4a0b4603045", // BulletManEyepatchPrefab
                "6868795625bd46f3ae3e4377adce288b", // ResourcefulRatBossPrefab
                "bb73eeeb9e584fbeaf35877ec176de28", // ManfredsRivalPrefab
                "705e9081261446039e1ed9ff16905d04", // CopPrefab
                "640238ba85dd4e94b3d6f68888e6ecb8", // CopAndroidPrefab
                "3a077fa5872d462196bb9a3cb1af02a3", // SuperSpaceTurtlePrefab
                "9216803e9c894002a4b931d7ea9c6bdf", // CursedSuperSpaceTurtlePrefab
                "2976522ec560460c889d11bb54fbe758", // PayDayShootPrefab
                "1ccdace06ebd42dc984d46cb1f0db6cf", // R2G2Prefab
                "998807b57e454f00a63d67883fcf90d6", // PortableTurretPrefab
                "e456b66ed3664a4cb590eab3a8ff3814", // BabyGoodMimicPrefab
                "c07ef60ae32b404f99e294a6f9acba75", // DogPrefab
                "ededff1deaf3430eaf8321d0c6b2bd80", // WolfPrefab
                "c6c8e59d0f5d41969c74e802c9d67d07", // SerJunkanPrefab
                "d375913a61d1465f8e4ffcf4894e4427", // CaterpillarPrefab
                "e9fa6544000942a79ad05b6e4afb62db", // RaccoonPrefab
                "6f9c28403d3248c188c391f5e40774c5", // TurkeyPrefab
                "5695e8ffa77c4d099b4d9bd9536ff35e", // BlankyPrefab
                "3f40178e10dc4094a1565cd4fdc4af56", // BabyShelletonPrefab
                "d4dd2b2bbda64cc9bcec534b4e920518", // Bullet King's Toadie Revenge
                "d1c9781fdac54d9e8498ed89210a0238", // tiny blobulord
                "6ad1cafc268f4214a101dca7af61bc91", // Rat
                "14ea47ff46b54bb4a98f91ffcffb656d", // Rat Candle
            };

            ValidTransformSpecialEnemies = new List<string>() {
                // "0d3f7c641557426fbac8596b61c9fb45", // LordOfTheJammedPrefab
                "ec6b674e0acd4553b47ee94493d66422", // GatlingGullPrefab
                "ea40fcc863d34b0088f490f4e57f8913", // BulletBrosSmileyPrefab
                "c00390483f394a849c36143eb878998f", // BulletBrosShadesPrefab
                "ec8ea75b557d4e7b8ceeaacdf6f8238c", // GunNutPrefab
                "383175a55879441d90933b5c4e60cf6f", // GunNutSpectrePrefab
                "463d16121f884984abe759de38418e48", // GunNutChainPrefab
                "9189f46c47564ed588b9108965f975c9", // BossDoorMimicPrefab
                "6c43fddfd401456c916089fdd1c99b1c", // HighPriestPrefab
                "4b992de5b4274168a8878ef9bf7ea36b", // BeholsterPrefab
                "3f11bbbc439c4086a180eb0fb9990cb4" // KillPillarsPrefab // Kill Pillars is technically a AIActorDummy object. The real AIActor objects are buried in the "realPrefab" field of said AiActorDummy component.
            };


            ValidSourceEnemies = new List<string>() {
                "4d37ce3d666b4ddda8039929225b7ede", // GrenadeGuyPrefab
                "f155fd2759764f4a9217db29dd21b7eb", // IceCubeGuyPrefab
                "4db03291a12144d69fe940d5a01de376", // GhostPrefab
                "05891b158cd542b1a5f3df30fb67a7ff", // ArrowheadManPrefab
                "31a3ea0c54a745e182e22ea54844a82d", // BulletRifleManPrefab
                "1a78cfb776f54641b832e92c44021cf2", // AshBulletManPrefab
                "1bd8e49f93614e76b140077ff2e33f2b", // AshBulletShotgunManPrefab
                "8bb5578fba374e8aae8e10b754e61d62", // BulletCardinalPrefab
                "db35531e66ce41cbb81d507a34366dfe", // BulletMachineGunManPrefab
                "5f3abc2d561b4b9c9e72b879c6f10c7e", // BulletManDevilPrefab
                "e5cffcfabfae489da61062ea20539887", // BulletManShroomedPrefab
                "95ec774b5a75467a9ab05fa230c0c143", // BulletSkeletonHelmetPrefab
                "2752019b770f473193b08b4005dc781f", // BulletShotgunManSawedOffPrefab
                "7f665bd7151347e298e4d366f8818284", // BulletShotgunManMutantPrefab
                "d4a9836f8ab14f3fadd0f597438b1f1f", // BulletManMutantPrefab
                "044a9f39712f456597b9762893fbc19c", // BulletShotgrubManPrefab
                "88b6b6a93d4b4234a67844ef4728382c", // BulletManBandanaPrefab
                "7b0b1b6d9ce7405b86b75ce648025dd6", // FloatingEyePrefab
                "c50a862d19fc4d30baeba54795e8cb93", // LeadWizardBluePrefab
                "b1540990a4f1480bbcb3bea70d67f60d", // NecromancerPrefab
                "8b4a938cdbc64e64822e841e482ba3d2", // JamromancerPrefab
                "0239c0680f9f467dbe5c4aab7dd1eca6", // BlobulonPrefab
                "f020570a42164e2699dcf57cac8a495c", // BulletManKaliberPrefab
                "ddf12a4881eb43cfba04f36dd6377abb", // BulletShotgunManCowboyPrefab
                "c5b11bfc065d417b9c4d03a5e385fe2c", // BulletRifleProfessionalPrefab
                "70216cae6c1346309d86d4a0b4603045" // BulletManEyepatchPrefab
            };*/
        }

        /*public List<string> ValidTransformEnemies;
        public List<string> ValidTransformSpecialEnemies;
        public List<string> ValidSourceEnemies;*/

        private bool m_isFinished;
    
        public void Awake() { aiActor.HasDonePlayerEnterCheck = true; }
    
        public override void StartIntro() {
            if (m_isFinished) { return; }
            m_isFinished = true;
            HandleChooseCorruptionEnemySource();
        }
    
        private void HandleChooseCorruptionEnemySource() {
            aiActor.enabled = false;
            behaviorSpeculator.enabled = false;
            aiActor.ToggleRenderers(false);
            specRigidbody.enabled = false;
            aiActor.IsGone = true;
            RoomHandler currentRoom = transform.position.GetAbsoluteRoom();
            IntVector2 cachedPosition = (transform.PositionVector2().ToIntVector2() - currentRoom.area.basePosition);
            // bool WillBeBigEnemy = UnityEngine.Random.value <= 0.2f;
            // bool WillBeGlitchObject = UnityEngine.Random.value <= 0.2f;
            
            if (aiShooter) { aiShooter.ToggleGunAndHandRenderers(false, "ExpandCorruptedEnemyEngageDoer"); }
            GameObject newEnemy = null;
            
            try {
                newEnemy = ExpandGlitchedEnemies.Instance.SpawnRandomGlitchEnemy(currentRoom, cachedPosition, true);
            } catch (Exception) {
                // If something broke, destroy broken Enemy (if it exist) to prevent possible softlocks.
                if (newEnemy) {
                    if (newEnemy.GetComponent<AIActor>()) { currentRoom.DeregisterEnemy(newEnemy.GetComponent<AIActor>()); }
                    Destroy(newEnemy);
                }
            }
                        
            if (newEnemy && newEnemy.GetComponent<AIActor>()) {
                newEnemy.GetComponent<AIActor>().aiAnimator.PlayDefaultAwakenedState();
                newEnemy.GetComponent<AIActor>().aiActor.State = AIActor.ActorState.Normal;
                newEnemy.GetComponent<AIActor>().aiActor.invisibleUntilAwaken = false;
            }

            currentRoom.DeregisterEnemy(aiActor);
            Destroy(gameObject);
            return;
            /*string SelectedEnemyToBecome;

            if (WillBeBigEnemy) {
                SelectedEnemyToBecome = BraveUtility.RandomElement(ValidTransformSpecialEnemies);
            } else {
                SelectedEnemyToBecome = BraveUtility.RandomElement(ValidTransformEnemies);
                ValidSourceEnemies.Remove(SelectedEnemyToBecome);
            }
            
            string SelectedEnemyToUseAttacks = BraveUtility.RandomElement(ValidSourceEnemies);

            ReplaceEnemyData(SelectedEnemyToBecome, SelectedEnemyToUseAttacks, WillBeBigEnemy, WillBeGlitchObject);

            aiActor.enabled = true;
            RegenerateCache();
            behaviorSpeculator.enabled = true;
            behaviorSpeculator.RegenerateCache();
            // behaviorSpeculator.RefreshBehaviors();
            aiActor.ToggleRenderers(true);
            if (aiShooter) { aiShooter.ToggleGunAndHandRenderers(true, "ExpandCorruptedEnemyEngageDoer"); }
            specRigidbody.enabled = true;
            specRigidbody.RegenerateCache();
            specRigidbody.RegenerateColliders = true;
            // specRigidbody.Reinitialize();
            healthHaver.RegenerateCache();
            aiActor.IsGone = false;
            aiAnimator.PlayDefaultAwakenedState();
            aiActor.State = AIActor.ActorState.Normal;
            aiActor.invisibleUntilAwaken = false;
            // transform.position = cachedPosition;
            // specRigidbody.Reinitialize();
            return;*/
        }


        /*private void ReplaceEnemyData(string EnemyToBecome, string EnemyToCloneAttacksFrom, bool isBigEnemy, bool isGlitchObject) {
            AIActor aiActorToTransformInto;
            AIActor aiActorToUseAttacksFrom = EnemyDatabase.GetOrLoadByGuid(EnemyToCloneAttacksFrom);

            if (isBigEnemy && isGlitchObject) {
                if (BraveUtility.RandomBool()) { isGlitchObject = false; } else { isBigEnemy = false; }
            }
            
            if (isGlitchObject) {
                aiActorToTransformInto = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5");

                HandleTransformIntoGlitchObject();

                aiActorToTransformInto.healthHaver.SetHealthMaximum(100);
            } else {

                if (EnemyToBecome == "3f11bbbc439c4086a180eb0fb9990cb4") {
                AIActorDummy KillPillarsDummy = EnemyDatabase.GetOrLoadByGuid(EnemyToBecome).gameObject.GetComponent<AIActorDummy>();
                if (KillPillarsDummy) {
                    aiActorToTransformInto = BraveUtility.RandomElement(KillPillarsDummy.realPrefab.GetComponent<BossStatuesController>().allStatues).gameObject.GetComponent<AIActor>();
                    } else {
                        aiActorToTransformInto = EnemyDatabase.GetOrLoadByGuid("ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gunnut
                    }
                } else {
                    aiActorToTransformInto = EnemyDatabase.GetOrLoadByGuid(EnemyToBecome);
                }

                sprite.Collection = aiActorToUseAttacksFrom.sprite.Collection;
                sprite.SetSprite(aiActorToTransformInto.sprite.spriteId);

                if (aiActorToTransformInto.specRigidbody) { ExpandUtility.DuplicateComponent(specRigidbody, aiActorToTransformInto.specRigidbody); }

                ExpandUtility.DuplicateComponent(spriteAnimator, aiActorToTransformInto.spriteAnimator);

                ExpandUtility.DuplicateComponent(aiAnimator, aiActorToTransformInto.aiAnimator);

                if (aiActorToTransformInto.healthHaver && !aiActorToTransformInto.healthHaver.IsBoss && !aiActorToTransformInto.gameObject.GetComponent<CompanionController>()) {
                    healthHaver.SetHealthMaximum(aiActorToTransformInto.healthHaver.GetCurrentHealth());
                } else if (aiActorToTransformInto.healthHaver && aiActorToTransformInto.gameObject.GetComponent<CompanionController>()) {
                    healthHaver.SetHealthMaximum(100);
                }

                aiActor.EnemySwitchState = aiActorToTransformInto.EnemySwitchState;
                aiActor.DiesOnCollison = aiActorToTransformInto.DiesOnCollison;

                if (aiActorToTransformInto.GetComponent<SpawnShardsOnDeath>()) {
                    SpawnShardsOnDeath shardSpawner = gameObject.AddComponent<SpawnShardsOnDeath>();
                    ExpandUtility.DuplicateComponent(shardSpawner, aiActorToTransformInto.GetComponent<SpawnShardsOnDeath>());
                }

                if (aiActorToTransformInto.GetComponent<SpawnEnemyOnDeath>()) {
                    SpawnEnemyOnDeath CachedSpawnEnemyOnDeath = gameObject.AddComponent<SpawnEnemyOnDeath>();
                    ExpandUtility.DuplicateComponent(CachedSpawnEnemyOnDeath, aiActorToTransformInto.GetComponent<SpawnEnemyOnDeath>());
                    CachedSpawnEnemyOnDeath.enemyGuidsToSpawn = ExpandLists.SpawnEnemyOnDeathGUIDList;
                    if (EnemyToBecome != "1a4872dafdb34fd29fe8ac90bd2cea67") {
                        CachedSpawnEnemyOnDeath.enemySelection = SpawnEnemyOnDeath.EnemySelection.Random;
                        CachedSpawnEnemyOnDeath.minSpawnCount = 2;
                        CachedSpawnEnemyOnDeath.maxSpawnCount = 3;
                    }
                }
                
                if (EnemyToBecome == "6868795625bd46f3ae3e4377adce288b") {
                    ExpandSpawnGlitchObjectOnDeath ObjectSpawnerComponent = gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>();
                    ObjectSpawnerComponent.spawnRatCorpse = true;
                    ObjectSpawnerComponent.ratCorpseSpawnsItemOnExplosion = BraveUtility.RandomBool();
                }

                if (isBigEnemy | aiActorToTransformInto.gameObject.GetComponent<CompanionController>()) {
                    float spawnOdds = 0.1f;
                    if (gameObject.GetComponent<CompanionController>()) { spawnOdds += 0.15f; }
                    if (UnityEngine.Random.value <= spawnOdds) { gameObject.AddComponent<ExpandSpawnGlitchEnemyOnDeath>(); }
                }
            }

            aiActor.MovementSpeed = aiActorToUseAttacksFrom.MovementSpeed;

            if (aiActorToUseAttacksFrom.gameObject.GetComponent<ExplodeOnDeath>()) {
                ExplodeOnDeath exploder = gameObject.AddComponent<ExplodeOnDeath>();
                ExpandUtility.DuplicateComponent(exploder, aiActorToUseAttacksFrom.gameObject.GetComponent<ExplodeOnDeath>());
            } else if (UnityEngine.Random.value <= 0.2f) {
                gameObject.AddComponent<ExpandExplodeOnDeath>();
            }
            
            if (aiActorToUseAttacksFrom.aiShooter) {
                Transform GunAttachPoint = gameObject.transform.Find("GunAttachPoint");

                try {
                    Bounds spriteBounds = sprite.GetBounds();
                    Vector3 spriteSize = spriteBounds.max;
                    GunAttachPoint.localPosition += new Vector3((spriteSize.x / 2), (spriteSize.y / 4), 0);
                } catch (Exception) { }

                if (EnemyToBecome == "ec8ea75b557d4e7b8ceeaacdf6f8238c") {
                    GunAttachPoint.localPosition = new Vector3(0.7f, 0.5f, 0);
                }

                ExpandUtility.DuplicateAIShooterAndAIBulletBank(gameObject, aiActorToUseAttacksFrom.aiShooter, aiActorToUseAttacksFrom.bulletBank, aiActorToUseAttacksFrom.aiShooter.equippedGunId, gameObject.transform.Find("GunAttachPoint"));
            }
            
            if (aiActorToUseAttacksFrom.gameObject.GetComponent<AIBulletBank>() && gameObject.GetComponent<AIBulletBank>()) {
                AIBulletBank currentBulletBank = gameObject.GetComponent<AIBulletBank>();
                if (aiActorToUseAttacksFrom.bulletBank.transforms != null && aiActorToUseAttacksFrom.bulletBank.transforms.Count > 0) {
                    currentBulletBank.transforms = new List<Transform>() { gameObject.transform.Find("shoot point") };
                    if (aiActorToUseAttacksFrom.bulletBank.transforms[0].name == "shoot point") {
                        currentBulletBank.transforms[0].localPosition = aiActorToUseAttacksFrom.bulletBank.transforms[0].localPosition;
                    }
                }
                currentBulletBank.RegenerateCache();
            }
            
            if (gameObject.GetComponent<AIShooter>()) { gameObject.GetComponent<AIShooter>().Initialize(); }
            
            if (aiActorToUseAttacksFrom.GetComponent<CrazedController>()) {
                aiActor.DiesOnCollison = true;
                CrazedController crazedController = gameObject.AddComponent<CrazedController>();
                ExpandUtility.DuplicateComponent(crazedController, aiActorToUseAttacksFrom.GetComponent<CrazedController>());
            }

            if (aiActorToUseAttacksFrom.GetComponent<KillOnRoomClear>()) {
                gameObject.AddComponent<KillOnRoomClear>();
                aiActor.IgnoreForRoomClear = aiActorToUseAttacksFrom.IgnoreForRoomClear;
            }
            
            ExpandUtility.DuplicateComponent(behaviorSpeculator, aiActorToUseAttacksFrom.behaviorSpeculator);
            

            if (behaviorSpeculator.MovementBehaviors != null && behaviorSpeculator.MovementBehaviors.Count > 0) {
                for (int i = 0; i < behaviorSpeculator.MovementBehaviors.Count; i++) {
                    if (behaviorSpeculator.MovementBehaviors[i].GetType() == typeof(TakeCoverBehavior)) {
                        behaviorSpeculator.MovementBehaviors.Remove(behaviorSpeculator.MovementBehaviors[i]);
                    }
                }
            }
            if (behaviorSpeculator.OverrideBehaviors != null && behaviorSpeculator.OverrideBehaviors.Count > 0) {
                for (int i = 0; i < behaviorSpeculator.OverrideBehaviors.Count; i++) {
                    if (behaviorSpeculator.OverrideBehaviors[i].GetType() == typeof(RedBarrelAwareness)) {
                        behaviorSpeculator.OverrideBehaviors.Remove(behaviorSpeculator.OverrideBehaviors[i]);
                    }
                }
            }

            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
            float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
            float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
            float RandomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

            
            if (isGlitchObject | aiActorToTransformInto.GetComponent<CompanionController>()) {
                if (!isGlitchObject) { aiActor.DiesOnCollison = true; }
                ExpandShaders.Instance.ApplyGlitchShader(sprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
            } else {
                ExpandShaders.Instance.ApplySuperGlitchShader(sprite, aiActorToUseAttacksFrom.sprite, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
            }
            
            if (aiActorToTransformInto.EnemyGuid == "9216803e9c894002a4b931d7ea9c6bdf") { aiActor.BecomeBlackPhantom(); }
        }
        
        private void HandleTransformIntoGlitchObject() {
            ExpandObjectDatabase objectDatabase = new ExpandObjectDatabase();

            List<TalkDoerLite> NPCList = new List<TalkDoerLite>() {
                objectDatabase.NPCEvilMuncher.GetComponent<TalkDoerLite>(),
                objectDatabase.NPCGunMuncher.GetComponent<TalkDoerLite>(),
                objectDatabase.NPCOldMan.GetComponent<TalkDoerLite>(),
                objectDatabase.NPCTonic.GetComponent<TalkDoerLite>(),
                objectDatabase.NPCTruthKnower.GetComponent<TalkDoerLite>(),
                objectDatabase.NPCCursola.GetComponent<TalkDoerLite>()
            };

            List<tk2dBaseSprite> OtherObjectsList = new List<tk2dBaseSprite>() {
                objectDatabase.ConvictPastCrowdNPC_01.GetComponent<tk2dBaseSprite>(),
                objectDatabase.PlayerCorpse.GetComponent<tk2dBaseSprite>(),
                objectDatabase.TimefallCorpse.GetComponent<tk2dBaseSprite>(),
                ExpandPrefabs.Teleporter_Info_Sign.GetComponent<tk2dBaseSprite>(),
                ExpandPrefabs.PlayerLostRatNote.GetComponent<tk2dBaseSprite>(),
                objectDatabase.LockedDoor.GetComponent<tk2dBaseSprite>(),
                ExpandPrefabs.MouseTrap1.GetComponent<tk2dBaseSprite>()
            };

            bool isConvictPastCrowdNPC = false;

            TalkDoerLite m_SelectedNPC = BraveUtility.RandomElement(NPCList);
            tk2dBaseSprite m_SelectedSprite = m_SelectedNPC.GetComponent<tk2dBaseSprite>();

            if (UnityEngine.Random.value <= 0.2f) {
                m_SelectedSprite = BraveUtility.RandomElement(OtherObjectsList);
                isConvictPastCrowdNPC = true;
            }

            // ExpandUtility.ApplyCustomTexture(aiActor, prebuiltCollection: m_SelectedSprite.Collection);
            sprite.Collection = m_SelectedSprite.Collection;
            sprite.SetSprite(m_SelectedSprite.spriteId);

            IntVector2 RigidBodyUnitSize = IntVector2.One;

            if (isConvictPastCrowdNPC) {
                if (m_SelectedSprite.name.StartsWith("Dancer") | m_SelectedSprite == OtherObjectsList[3]) {
                    RigidBodyUnitSize = new IntVector2(3, 4);
                } else if(m_SelectedSprite == OtherObjectsList[1]) {
                    RigidBodyUnitSize = new IntVector2(1, 2);
                } else if(m_SelectedSprite == OtherObjectsList[2]) {
                    RigidBodyUnitSize = new IntVector2(1, 1);
                } else if(m_SelectedSprite == OtherObjectsList[5]) {
                    RigidBodyUnitSize = new IntVector2(3, 2);
                }
            } else if (m_SelectedNPC == NPCList[0] | m_SelectedNPC == NPCList[1]) {
                RigidBodyUnitSize = new IntVector2(3, 2);
            } else if (m_SelectedNPC == NPCList[2] | m_SelectedNPC == NPCList[4] | m_SelectedNPC == NPCList[5]) {  
                RigidBodyUnitSize = new IntVector2(2, 2);
            } else if (m_SelectedNPC == NPCList[3]) {
                RigidBodyUnitSize = new IntVector2(1, 1);
            }

            IntVector2 m_RigidBodyPixelSize = new IntVector2(RigidBodyUnitSize.x * 16, RigidBodyUnitSize.y * 16);

            ExpandUtility.GenerateNewEnemyRigidBody(aiActor, IntVector2.Zero, m_RigidBodyPixelSize);

            Destroy(gameObject.GetComponent<AIAnimator>());
            ExpandUtility.GenerateBlankAIAnimator(gameObject);
            specRigidbody.RegenerateColliders = true;
            objectDatabase = null;
        }*/

        public override bool IsFinished { get { return m_isFinished; } }
    }
}

