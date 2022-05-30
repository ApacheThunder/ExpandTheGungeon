using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using System;
using ExpandTheGungeon.SpriteAPI;

namespace ExpandTheGungeon.ItemAPI {

	public class BabyGoodHammer : PlayerItem {

        public static GameObject hammerSpawnFX;
        public static GameObject hammerItemObject;

        public static string CompanionGuid;
        public static int HammerPickupID;

        public static void Init(AssetBundle expandSharedAssets1) {
            
            
            hammerItemObject = expandSharedAssets1.LoadAsset<GameObject>("Baby Good Hammer");
            BabyGoodHammer babyGoodHammer = hammerItemObject.AddComponent<BabyGoodHammer>();
            SpriteSerializer.AddSpriteToObject(hammerItemObject, ExpandPrefabs.EXItemCollection, "babygoodhammer");
            
            string shortDesc = "It's Hammer Time!";
			string longDesc = "Summons a Dead Blow Hammer.\n\nIt's cry sounds a lot like a whistle.\n\nThe closer you are to the Forge, the more powerful the hammers will be.";
            // ItemBuilder.SetupItem(babyGoodHammer, shortDesc, longDesc, "ex");
            ItemBuilder.SetupItem(babyGoodHammer, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(babyGoodHammer, ItemBuilder.CooldownType.Damage, 350f);
            babyGoodHammer.quality = ItemQuality.B;
            if (!ExpandSettings.EnableEXItems) { babyGoodHammer.quality = ItemQuality.EXCLUDED; }

            // Hammer Spawn FX Object
            hammerSpawnFX = expandSharedAssets1.LoadAsset<GameObject>("HammerSpawningFX");
            tk2dSprite hammerSpawnFXSprite = SpriteSerializer.AddSpriteToObject(hammerSpawnFX, ExpandPrefabs.EXItemCollection, "babygoodhammer_spawn_00");
            
            List<string> spritePaths = new List<string>() {
                "babygoodhammer_spawn_00",
                "babygoodhammer_spawn_00",
                "babygoodhammer_spawn_01",
                "babygoodhammer_spawn_01",
                "babygoodhammer_spawn_01",
                "babygoodhammer_spawn_01",
                "babygoodhammer_spawn_02",
                "babygoodhammer_spawn_03",
                "babygoodhammer_spawn_04",
                "babygoodhammer_spawn_05",
                "babygoodhammer_spawn_06",
                "babygoodhammer_spawn_07",
                "babygoodhammer_spawn_08",
                "babygoodhammer_spawn_09",
                "babygoodhammer_spawn_10",
                "babygoodhammer_spawn_11",
                "babygoodhammer_spawn_12",
                "babygoodhammer_spawn_13",
                "babygoodhammer_spawn_14",
                "babygoodhammer_spawn_15",
                "babygoodhammer_spawn_16",
                "babygoodhammer_spawn_17",
                "babygoodhammer_spawn_18",
                "babygoodhammer_spawn_19",
                "babygoodhammer_spawn_20",
                "babygoodhammer_spawn_21",
                "babygoodhammer_spawn_22",
                "babygoodhammer_spawn_23",
                "babygoodhammer_spawn_24",
                "babygoodhammer_spawn_25"
            };

            /*foreach (string spriteName in spritePaths) {
                if (spriteName != "babygoodhammer_spawn_00") {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), hammerSpawnFXSprite.Collection);
                }
            }*/

            ExpandUtility.GenerateSpriteAnimator(hammerSpawnFX);
            tk2dSpriteAnimator hammerAnimator = hammerSpawnFX.GetComponent<tk2dSpriteAnimator>();

            List<string> spritePaths_reversed = new List<string>() {
                "babygoodhammer_spawn_25",
                "babygoodhammer_spawn_24",
                "babygoodhammer_spawn_23",
                "babygoodhammer_spawn_22",
                "babygoodhammer_spawn_21",
                "babygoodhammer_spawn_20",
                "babygoodhammer_spawn_19",
                "babygoodhammer_spawn_18",
                "babygoodhammer_spawn_17",
                "babygoodhammer_spawn_16",
                "babygoodhammer_spawn_15",
                "babygoodhammer_spawn_14",
                "babygoodhammer_spawn_13",
                "babygoodhammer_spawn_12",
                "babygoodhammer_spawn_11",
                "babygoodhammer_spawn_10",
                "babygoodhammer_spawn_09",
                "babygoodhammer_spawn_08",
                "babygoodhammer_spawn_07",
                "babygoodhammer_spawn_06",
                "babygoodhammer_spawn_05",
                "babygoodhammer_spawn_04",
                "babygoodhammer_spawn_03",
                "babygoodhammer_spawn_02",
                "babygoodhammer_spawn_01",
                "babygoodhammer_spawn_01",
                "babygoodhammer_spawn_01",
                "babygoodhammer_spawn_01",
                "babygoodhammer_spawn_00"
            };


            ExpandUtility.AddAnimation(hammerAnimator, ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), spritePaths, "HammerSpawn", tk2dSpriteAnimationClip.WrapMode.Once);
            ExpandUtility.AddAnimation(hammerAnimator, ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), spritePaths_reversed, "HammerReturnSpawn", tk2dSpriteAnimationClip.WrapMode.Once);

            HammerPickupID = babyGoodHammer.PickupObjectId;

            ExpandLists.CompanionItems.Add(hammerItemObject);
        }
        

        public BabyGoodHammer() {
            PreventRespawnOnFloorLoad = false;
            m_HammersHidden = false;
        }

        public bool PreventRespawnOnFloorLoad;

        public GameObject ExtantCompanion { get { return m_extantCompanion; } }

        private GameObject m_extantCompanion;

        private bool m_HammersHidden;
        private bool m_PickedUp;

        public override bool CanBeUsed(PlayerController user) {
            return IsUsableRightNow(user) && base.CanBeUsed(user);
        }

        private bool IsUsableRightNow(PlayerController user) {
            if (user.IsInCombat) {                
                if (ExpandStaticReferenceManager.AllFriendlyHammers != null) {
                    if (ExpandStaticReferenceManager.AllFriendlyHammers.Count > 0) {
                        foreach (ExpandForgeHammerComponent hammer in ExpandStaticReferenceManager.AllFriendlyHammers) {
                            if (hammer != null && hammer.GetAbsoluteParentRoom() == user.CurrentRoom) {
                                if (hammer.IsActive) { return false; }
                            }
                        }
                    }
                }
                return true;
            } else {
                return false;
            }
        }

        protected override void DoEffect(PlayerController user) {
            
            // AkSoundEngine.PostEvent("Play_BOSS_bulletbros_anger_01", gameObject);
            if (BraveUtility.RandomBool()) {
                AkSoundEngine.PostEvent("Play_ENM_smiley_whistle_01", gameObject);
            } else {
                AkSoundEngine.PostEvent("Play_ENM_smiley_whistle_02", gameObject);
            }
            
            if (StaticReferenceManager.AllEnemies.Count > 0) {
                List<AIActor> m_BabyGoodHammers = new List<AIActor>();
                foreach (AIActor enemy in StaticReferenceManager.AllEnemies) {
                    if (enemy.EnemyGuid == CompanionGuid) { m_BabyGoodHammers.Add(enemy); }
                }
                if (m_BabyGoodHammers.Count <= 0) {
                    StartCoroutine(HandleSpawnAnimation(user));
                    StartCoroutine(SpawnHammer(user));
                } else {
                    m_HammersHidden = true;
                    foreach (AIActor enemy in m_BabyGoodHammers) {
                        enemy.gameObject.GetComponent<CompanionController>().enabled = false;
                        enemy.ToggleRenderers(false);
                        enemy.IsGone = true;
                        enemy.behaviorSpeculator.InterruptAndDisable();
                        enemy.specRigidbody.CollideWithOthers = false;
                        StartCoroutine(HandleSpawnAnimation(user, enemy.sprite.WorldBottomLeft));
                        StartCoroutine(SpawnHammer(user));
                    }
                }
            } else {
                StartCoroutine(HandleSpawnAnimation(user));
                StartCoroutine(SpawnHammer(user));
            }
		}
        

        private IEnumerator HandleSpawnAnimation(PlayerController user, Vector3? overridePosition = null) {

            Vector3 SpawnFXPosition = (user.transform.position + new Vector3(0, 1.25f));
            if (overridePosition.HasValue) { SpawnFXPosition = overridePosition.Value; }

            GameObject targetObject = Instantiate(hammerSpawnFX, SpawnFXPosition, Quaternion.identity);
            yield return null;
            tk2dSpriteAnimator hammerAnimator = targetObject.GetComponent<tk2dSpriteAnimator>();
            if (hammerAnimator != null) {
                if (!overridePosition.HasValue) {
                    LootEngine.DoDefaultSynergyPoof(targetObject.transform.position + new Vector3(0.1f, 0.1f));
                    LootEngine.DoDefaultItemPoof(targetObject.transform.position + new Vector3(0.1f, 0.1f));
                }
                hammerAnimator.Play("HammerSpawn");
                while (hammerAnimator.Playing) {
                    targetObject.transform.position += new Vector3(0, 0.02f);
                    yield return null;
                }
                Destroy(targetObject);
            } else {
                Destroy(targetObject);
                yield break;
            }
        }

        private IEnumerator HandleHammerReturn(AIActor targetHammer, Vector3 returnPosition) {
            GameObject targetObject = Instantiate(hammerSpawnFX, returnPosition, Quaternion.identity);
            yield return null;
            tk2dSpriteAnimator hammerAnimator = targetObject.GetComponent<tk2dSpriteAnimator>();
            if (hammerAnimator != null) {
                hammerAnimator.Play("HammerReturnSpawn");
                while (hammerAnimator.Playing) {
                    targetObject.transform.position -= new Vector3(0, 0.02f);
                    yield return null;
                }
                Vector3 m_LastSpritePosition = targetObject.transform.position;
                targetHammer.gameObject.GetComponent<CompanionController>().enabled = true;
                targetHammer.ToggleRenderers(true);
                targetHammer.IsGone = false;
                targetHammer.behaviorSpeculator.enabled = true;                
                targetHammer.gameObject.transform.position = m_LastSpritePosition;
                if (targetHammer.specRigidbody) {
                    targetHammer.specRigidbody.CollideWithOthers = true;
                    targetHammer.specRigidbody.Reinitialize();
                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(targetHammer.specRigidbody, null, false);
                }
                Destroy(targetObject);
            } else {
                targetHammer.gameObject.GetComponent<CompanionController>().enabled = true;
                targetHammer.ToggleRenderers(true);
                targetHammer.IsGone = false;
                targetHammer.behaviorSpeculator.enabled = true;                
                targetHammer.gameObject.transform.position = (returnPosition - new Vector3(0, 4.5f));
                if (targetHammer.specRigidbody) {
                    targetHammer.specRigidbody.CollideWithOthers = true;
                    targetHammer.specRigidbody.Reinitialize();
                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(targetHammer.specRigidbody, null, false);
                }
                Destroy(targetObject);
                yield break;
            }
        }

        public static IEnumerator SpawnHammer(PlayerController user, float spawnDelay = 0) {
            if (spawnDelay > 0) { yield return new WaitForSeconds(spawnDelay); }

			// Tools.Print("Spawning A Friendly Hammer!", "FFFFFF", false);
			RoomHandler room = user.CurrentRoom;
            IntVector2? spawnPosition = room.GetRandomVisibleClearSpot(2, 2);
            yield return new WaitForSeconds(1f);
            if (!spawnPosition.HasValue) { spawnPosition = (user.CenterPosition.ToIntVector2() - user.CurrentRoom.area.basePosition);  }
			if (spawnPosition.HasValue) {
                RoomHandler currentRoom = user.CurrentRoom;
                GameObject ForgeHammer = DungeonPlaceableUtility.InstantiateDungeonPlaceable(ExpandPrefabs.EXFriendlyForgeHammer, currentRoom, spawnPosition.Value, true);
                yield return null;
                
                if (ForgeHammer) {
                    ForgeHammer.AddComponent<ExpandForgeHammerComponent>();
                    ExpandForgeHammerComponent expandForgeHammer = ForgeHammer.GetComponent<ExpandForgeHammerComponent>();
                    expandForgeHammer.m_Owner = user;
                    expandForgeHammer.ConfigureOnPlacement(currentRoom);                    
                }

                yield return null;
            }
			yield break;
		}

        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            m_PickedUp = true;
            player.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Combine(player.OnNewFloorLoaded, new Action<PlayerController>(HandleNewFloor));
            CreateCompanion(player);
        }

        protected override void OnPreDrop(PlayerController player) {
            base.OnPreDrop(player);
            m_PickedUp = false;
            DestroyCompanion();
            player.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Remove(player.OnNewFloorLoaded, new Action<PlayerController>(HandleNewFloor));
        }

        public override void Update() {
            base.Update();
            if (!Dungeon.IsGenerating && m_PickedUp) {
                if (m_HammersHidden && LastOwner && LastOwner.GetAbsoluteParentRoom() != null) {
                    if (!LastOwner.IsInCombat) {
                        m_HammersHidden = false;
                        if (StaticReferenceManager.AllEnemies.Count > 0) {
                            List<AIActor> m_BabyHammers = new List<AIActor>();
                            foreach (AIActor enemy in StaticReferenceManager.AllEnemies) {
                                if (enemy.EnemyGuid == CompanionGuid && enemy.IsGone) {
                                    RoomHandler CurrentRoom = LastOwner.GetAbsoluteParentRoom();                                    
                                    // Vector3 ReturnPosition = (LastOwner.gameObject.transform.position + new Vector3(UnityEngine.Random.Range(0, 0.25f), 4.5f));
                                    Vector3 ReturnPosition = (new Vector3(0, 5.5f) + LastOwner.CurrentRoom.GetBestRewardLocation(new IntVector2(2, 2), RoomHandler.RewardLocationStyle.PlayerCenter, false).ToVector3());
                                    StartCoroutine(HandleHammerReturn(enemy, ReturnPosition));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateCompanion(PlayerController owner) {
            if (PreventRespawnOnFloorLoad) { return; }
            
            if (string.IsNullOrEmpty(CompanionGuid)) { return; }

            AIActor m_NewAIActor = EnemyDatabase.GetOrLoadByGuid(CompanionGuid);
            Vector3 vector = owner.transform.position;
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER) { vector += new Vector3(1.125f, -0.3125f, 0f); }
            m_extantCompanion = Instantiate(m_NewAIActor.gameObject, vector, Quaternion.identity);
            
            if (m_extantCompanion) {
                CompanionController orAddComponent = m_extantCompanion.GetOrAddComponent<CompanionController>();
                orAddComponent.Initialize(owner);

                if (orAddComponent.specRigidbody) { PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent.specRigidbody, null, false); }

                if (m_HammersHidden) {
                    orAddComponent.enabled = false;
                    m_extantCompanion.GetComponent<AIActor>().ToggleRenderers(false);
                    m_extantCompanion.GetComponent<AIActor>().IsGone = true;
                    m_extantCompanion.GetComponent<AIActor>().behaviorSpeculator.InterruptAndDisable();
                    orAddComponent.specRigidbody.CollideWithOthers = false;
                }
            }
        }

        public void ForceCompanionRegeneration(PlayerController owner, Vector2? overridePosition) {
            bool flag = false;
            Vector2 vector = Vector2.zero;
            if (m_extantCompanion) {
                flag = true;
                vector = m_extantCompanion.transform.position.XY();
            }
            if (overridePosition.HasValue) {
                flag = true;
                vector = overridePosition.Value;
            }
            
            DestroyCompanion();
            CreateCompanion(owner);

            if (m_extantCompanion && flag) {
                m_extantCompanion.transform.position = vector.ToVector3ZisY(0f);
                SpeculativeRigidbody component = m_extantCompanion.GetComponent<SpeculativeRigidbody>();
                if (component) { component.Reinitialize(); }
            }
        }

        public void ForceDisconnectCompanion() { m_extantCompanion = null; }
        
        private void DestroyCompanion() {
            if (!m_extantCompanion) { return; }
            Destroy(m_extantCompanion);
            m_extantCompanion = null;
        }

        private void HandleNewFloor(PlayerController obj) {
            DestroyCompanion();
            if (!PreventRespawnOnFloorLoad) { CreateCompanion(obj); }
        }

        protected override void OnDestroy() {
            if (LastOwner != null) {
                PlayerController owner = LastOwner;
                owner.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Remove(owner.OnNewFloorLoaded, new Action<PlayerController>(HandleNewFloor));
            }
            m_PickedUp = false;
            DestroyCompanion();
            base.OnDestroy();
        }
    }
}

