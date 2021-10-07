using System;
using System.Collections.Generic;
using Dungeonator;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandComponents {

	public class ExpandSpawnGlitchEnemyOnDeath : OnDeathBehavior {
        
        // Using Defaults from Blobulon Prefab
        public ExpandSpawnGlitchEnemyOnDeath() {
            deathType = DeathType.PreDeath;
            preDeathDelay = 0f;
            chanceToSpawn = 1f;
            triggerName = "";
            spawnVfx = "";
            enemySelection = EnemySelection.All;
            spawnPosition = SpawnPosition.InsideCollider;
            extraPixelWidth = 7;
            minSpawnCount = 1;
            maxSpawnCount = 1;
            spawnRadius = 1f;
            guaranteedSpawnGenerations = 0;
            spawnAnim = "awaken";
            spawnsCanDropLoot = true;
            DoNormalReinforcement = false;
            useGlitchedActorPrefab = true;
            IsGlitchedLJ = true;
            ActorPrefabSpawnCount = 1;
        }        

        [EnemyIdentifier]
        public string[] enemyGuidsToSpawn = { "2d4f8b5404614e7d8b235006acde427a", "042edb1dfb614dc385d5ad1b010f2ee3" }; // Blobuloids
        
        public bool useGlitchedActorPrefab;
        public bool IsGlitchedLJ;

        int ActorPrefabSpawnCount;

        public GameObject ActorObjectTarget;
        public GameObject ActorOverrideSource;

        private bool ShowRandomPrams() { return enemySelection == EnemySelection.Random; }	
		private bool ShowInsideColliderParams() { return spawnPosition == SpawnPosition.InsideCollider; }
		private bool ShowInsideRadiusParams() { return spawnPosition == SpawnPosition.InsideRadius; }


        public float chanceToSpawn;
        public string spawnVfx;

        [Header("Enemies to Spawn")]
        public EnemySelection enemySelection;
        [ShowInInspectorIf("ShowRandomPrams", true)]
        public int minSpawnCount;
        [ShowInInspectorIf("ShowRandomPrams", true)]
        public int maxSpawnCount;
        [FormerlySerializedAs("spawnType")]
        [Header("Placement")]
        public SpawnPosition spawnPosition;
        [ShowInInspectorIf("ShowInsideColliderParams", true)]
        public int extraPixelWidth;
        [ShowInInspectorIf("ShowInsideRadiusParams", true)]
        public float spawnRadius;
        [Header("Spawn Parameters")]

        public float guaranteedSpawnGenerations;
        public string spawnAnim;
        public string LJSpawnAnim;
        public bool spawnsCanDropLoot;
        public bool DoNormalReinforcement;
        private bool m_hasTriggered;
        public enum EnemySelection { All = 10, Random = 20 }
        public enum SpawnPosition { InsideCollider, ScreenEdge, InsideRadius = 20 }

        public void ManuallyTrigger(Vector2 damageDirection) { OnTrigger(damageDirection); }

        protected override void OnTrigger(Vector2 damageDirection) {
			if (m_hasTriggered) { return; }
            m_hasTriggered = true;
			if (guaranteedSpawnGenerations <= 0f && chanceToSpawn < 1f && UnityEngine.Random.value > chanceToSpawn) { return; }
			if (!string.IsNullOrEmpty(spawnVfx)) { aiAnimator.PlayVfx(spawnVfx, null, null, null); }
			string[] array = null;
			if (enemySelection == EnemySelection.All) { array = enemyGuidsToSpawn; }
			else if (enemySelection == EnemySelection.Random) {
				array = new string[UnityEngine.Random.Range(minSpawnCount, maxSpawnCount)];
				for (int i = 0; i < array.Length; i++) {
					array[i] = BraveUtility.RandomElement(enemyGuidsToSpawn);
				}
			}
            SpawnEnemies(array);
		}

		private void SpawnEnemies(string[] selectedEnemyGuids) {
            if (useGlitchedActorPrefab) {                
                IntVector2 pos = specRigidbody.UnitCenter.ToIntVector2(VectorConversions.Floor);
				if (aiActor.IsFalling && !IsGlitchedLJ) { return; }
				if (GameManager.Instance.Dungeon.CellIsPit(specRigidbody.UnitCenter.ToVector3ZUp(0f)) && !IsGlitchedLJ) { return; }
				RoomHandler roomFromPosition = GameManager.Instance.Dungeon.GetRoomFromPosition(pos);
				List<SpeculativeRigidbody> list = new List<SpeculativeRigidbody>();
				list.Add(specRigidbody);
				Vector2 unitBottomLeft = specRigidbody.UnitBottomLeft;
				for (int i = 0; i < ActorPrefabSpawnCount; i++) {
                    if (IsGlitchedLJ) {                        
                        if (transform.position.GetAbsoluteRoom() != null) {
                            RoomHandler CurrentRoom = transform.position.GetAbsoluteRoom();
                            IntVector2 actorPosition = specRigidbody.UnitCenter.ToIntVector2(VectorConversions.Floor) - CurrentRoom.area.basePosition;
                            ExpandGlitchedEnemies.Instance.SpawnGlitchedSuperReaper(CurrentRoom, actorPosition);
                            return;
                        }              
                    } else {
                        if (ActorObjectTarget == null) { return; }
                        AIActor.AwakenAnimationType AnimationType = AIActor.AwakenAnimationType.Default;
                        AIActor aiactor = null;
                        GameObject CachedTargetActorObject = Instantiate(ActorObjectTarget);
                        bool ExplodesOnDeath = false;
                        bool spawnsGlitchedObjectOnDeath = false;
                        if (UnityEngine.Random.value <= 0.25f) { ExplodesOnDeath = true; }
                        if (UnityEngine.Random.value <= 0.15f) { spawnsGlitchedObjectOnDeath = true; }
                        aiactor = AIActor.Spawn(ExpandGlitchedEnemies.Instance.GenerateGlitchedActorPrefab(CachedTargetActorObject, ActorOverrideSource, ExplodesOnDeath, spawnsGlitchedObjectOnDeath), specRigidbody.UnitCenter.ToIntVector2(VectorConversions.Floor), roomFromPosition, true, AnimationType, true);
                        if (aiactor == null) { return; }
                        if (aiActor.IsBlackPhantom) { aiactor.BecomeBlackPhantom(); }
                        if (aiactor) {
                            aiactor.specRigidbody.Initialize();
                            Vector2 a = unitBottomLeft - (aiactor.specRigidbody.UnitBottomLeft - aiactor.transform.position.XY());
                            Vector2 vector = a + new Vector2(Mathf.Max(0f, specRigidbody.UnitDimensions.x - aiactor.specRigidbody.UnitDimensions.x), 0f);
                            aiactor.transform.position = Vector2.Lerp(a, vector, (ActorPrefabSpawnCount != 1) ? i / (ActorPrefabSpawnCount - 1f) : 0f);
                            aiactor.specRigidbody.Reinitialize();
                            a -= new Vector2(PhysicsEngine.PixelToUnit(extraPixelWidth), 0f);
                            vector += new Vector2(PhysicsEngine.PixelToUnit(extraPixelWidth), 0f);
                            Vector2 a2 = Vector2.Lerp(a, vector, (ActorPrefabSpawnCount != 1) ? i / (ActorPrefabSpawnCount - 1f) : 0.5f);
                            IntVector2 intVector = PhysicsEngine.UnitToPixel(a2 - aiactor.transform.position.XY());
                            CollisionData collisionData = null;
                            if (PhysicsEngine.Instance.RigidbodyCastWithIgnores(aiactor.specRigidbody, intVector, out collisionData, true, true, null, false, list.ToArray())) {
                                intVector = collisionData.NewPixelsToMove;
                            }
                            CollisionData.Pool.Free(ref collisionData);
                            aiactor.transform.position += PhysicsEngine.PixelToUnit(intVector).ToVector3ZUp(1f);
                            aiactor.specRigidbody.Reinitialize();
                            if (i == 0) { aiactor.aiAnimator.FacingDirection = 180f; }
                            else if (i == ActorPrefabSpawnCount - 1) { aiactor.aiAnimator.FacingDirection = 0f; }
                            HandleSpawn(aiactor);
                            list.Add(aiactor.specRigidbody);
                            Destroy(CachedTargetActorObject);
                        }
                    }                    
				}
                if (list.Count > 0) {
                    for (int j = 0; j < list.Count; j++) {
                        for (int k = 0; k < list.Count; k++) { if (j != k) { list[j].RegisterGhostCollisionException(list[k]); } }
                    }
                }				
            } else if (spawnPosition == SpawnPosition.InsideCollider) {
				IntVector2 pos = specRigidbody.UnitCenter.ToIntVector2(VectorConversions.Floor);
				if (aiActor.IsFalling) { return; }
				if (GameManager.Instance.Dungeon.CellIsPit(specRigidbody.UnitCenter.ToVector3ZUp(0f))) { return; }
				RoomHandler roomFromPosition = GameManager.Instance.Dungeon.GetRoomFromPosition(pos);
				List<SpeculativeRigidbody> list = new List<SpeculativeRigidbody>();
				list.Add(specRigidbody);
				Vector2 unitBottomLeft = specRigidbody.UnitBottomLeft;
				for (int i = 0; i < selectedEnemyGuids.Length; i++) {
                    AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(selectedEnemyGuids[i]);
                    AIActor aiactor = AIActor.Spawn(orLoadByGuid, specRigidbody.UnitCenter.ToIntVector2(VectorConversions.Floor), roomFromPosition, false, AIActor.AwakenAnimationType.Default, true);
					if (aiActor.IsBlackPhantom) { aiactor.ForceBlackPhantom = true; }
					if (aiactor) {
						aiactor.specRigidbody.Initialize();
						Vector2 a = unitBottomLeft - (aiactor.specRigidbody.UnitBottomLeft - aiactor.transform.position.XY());
						Vector2 vector = a + new Vector2(Mathf.Max(0f, specRigidbody.UnitDimensions.x - aiactor.specRigidbody.UnitDimensions.x), 0f);
						aiactor.transform.position = Vector2.Lerp(a, vector, (selectedEnemyGuids.Length != 1) ? i / (selectedEnemyGuids.Length - 1f) : 0f);
						aiactor.specRigidbody.Reinitialize();
						a -= new Vector2(PhysicsEngine.PixelToUnit(extraPixelWidth), 0f);
						vector += new Vector2(PhysicsEngine.PixelToUnit(extraPixelWidth), 0f);
						Vector2 a2 = Vector2.Lerp(a, vector, (selectedEnemyGuids.Length != 1) ? i / (selectedEnemyGuids.Length - 1f) : 0.5f);
						IntVector2 intVector = PhysicsEngine.UnitToPixel(a2 - aiactor.transform.position.XY());
						CollisionData collisionData = null;
						if (PhysicsEngine.Instance.RigidbodyCastWithIgnores(aiactor.specRigidbody, intVector, out collisionData, true, true, null, false, list.ToArray())) {
							intVector = collisionData.NewPixelsToMove;
						}
						CollisionData.Pool.Free(ref collisionData);
                        // aiactor.transform.position += PhysicsEngine.PixelToUnit(intVector);
                        aiactor.transform.position += PhysicsEngine.PixelToUnit(intVector).ToVector3ZUp(1f);
                        aiactor.specRigidbody.Reinitialize();
						if (i == 0) { aiactor.aiAnimator.FacingDirection = 180f; }
						else if (i == selectedEnemyGuids.Length - 1) { aiactor.aiAnimator.FacingDirection = 0f; }
                        HandleSpawn(aiactor);
						list.Add(aiactor.specRigidbody);
					}
				}
				for (int j = 0; j < list.Count; j++) {
					for (int k = 0; k < list.Count; k++) {
						if (j != k) { list[j].RegisterGhostCollisionException(list[k]); }
					}
				}
			} else if (spawnPosition == SpawnPosition.ScreenEdge) {
				for (int l = 0; l < selectedEnemyGuids.Length; l++) {
					AIActor orLoadByGuid2 = EnemyDatabase.GetOrLoadByGuid(selectedEnemyGuids[l]);
					AIActor spawnedActor = AIActor.Spawn(orLoadByGuid2, specRigidbody.UnitCenter.ToIntVector2(VectorConversions.Floor), aiActor.ParentRoom, false, AIActor.AwakenAnimationType.Default, true);
					if (spawnedActor) {
						Vector2 cameraBottomLeft = BraveUtility.ViewportToWorldpoint(new Vector2(0f, 0f), ViewportType.Gameplay);
						Vector2 cameraTopRight = BraveUtility.ViewportToWorldpoint(new Vector2(1f, 1f), ViewportType.Gameplay);
						IntVector2 bottomLeft = cameraBottomLeft.ToIntVector2(VectorConversions.Ceil);
						IntVector2 topRight = cameraTopRight.ToIntVector2(VectorConversions.Floor) - IntVector2.One;
						CellValidator cellValidator = delegate(IntVector2 c) {
							for (int num2 = 0; num2 < spawnedActor.Clearance.x; num2++) {
								for (int num3 = 0; num3 < spawnedActor.Clearance.y; num3++) {
									if (GameManager.Instance.Dungeon.data.isTopWall(c.x + num2, c.y + num3)) { return false; }
									if (GameManager.Instance.Dungeon.data[c.x + num2, c.y + num3].isExitCell) { return false; }
								}
							}
							return c.x >= bottomLeft.x && c.y >= bottomLeft.y && c.x + spawnedActor.Clearance.x - 1 <= topRight.x && c.y + spawnedActor.Clearance.y - 1 <= topRight.y;
						};
						Func<IntVector2, float> cellWeightFinder = delegate(IntVector2 c) {
							float a3 = float.MaxValue;
							a3 = Mathf.Min(a3, c.x - cameraBottomLeft.x);
							a3 = Mathf.Min(a3, c.y - cameraBottomLeft.y);
							a3 = Mathf.Min(a3, cameraTopRight.x - c.x + spawnedActor.Clearance.x);
							return Mathf.Min(a3, cameraTopRight.y - c.y + spawnedActor.Clearance.y);
						};
						Vector2 b = spawnedActor.specRigidbody.UnitCenter - spawnedActor.transform.position.XY();
						IntVector2? randomWeightedAvailableCell = spawnedActor.ParentRoom.GetRandomWeightedAvailableCell(new IntVector2?(spawnedActor.Clearance), new CellTypes?(spawnedActor.PathableTiles), false, cellValidator, cellWeightFinder, 0.25f);
						if (randomWeightedAvailableCell == null) {
							Debug.LogError("Screen Edge Spawn FAILED!", spawnedActor);
							Destroy(spawnedActor);
						} else {
							spawnedActor.transform.position = Pathfinder.GetClearanceOffset(randomWeightedAvailableCell.Value, spawnedActor.Clearance) - b;
							spawnedActor.specRigidbody.Reinitialize();
                            HandleSpawn(spawnedActor);
						}
					}
				}
			} else if (spawnPosition == SpawnPosition.InsideRadius) {
				Vector2 unitCenter = specRigidbody.GetUnitCenter(ColliderType.HitBox);
				List<SpeculativeRigidbody> list2 = new List<SpeculativeRigidbody>();
				list2.Add(specRigidbody);
				for (int m = 0; m < selectedEnemyGuids.Length; m++) {
					Vector2 vector2 = unitCenter + UnityEngine.Random.insideUnitCircle * spawnRadius;
					if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.CHARACTER_PAST && SceneManager.GetActiveScene().name == "fs_robot") {
						RoomHandler entrance = GameManager.Instance.Dungeon.data.Entrance;
						Vector2 lhs = entrance.area.basePosition.ToVector2() + new Vector2(7f, 7f);
						Vector2 lhs2 = entrance.area.basePosition.ToVector2() + new Vector2(38f, 36f);
						vector2 = Vector2.Min(lhs2, Vector2.Max(lhs, vector2));
					}
					AIActor orLoadByGuid3 = EnemyDatabase.GetOrLoadByGuid(selectedEnemyGuids[m]);
					AIActor aiactor2 = AIActor.Spawn(orLoadByGuid3, unitCenter.ToIntVector2(VectorConversions.Floor), aiActor.ParentRoom, true, AIActor.AwakenAnimationType.Default, true);
					if (aiactor2) {
						aiactor2.specRigidbody.Initialize();
						Vector2 unit = vector2 - aiactor2.specRigidbody.GetUnitCenter(ColliderType.HitBox);
						aiactor2.specRigidbody.ImpartedPixelsToMove = PhysicsEngine.UnitToPixel(unit);
                        HandleSpawn(aiactor2);
						list2.Add(aiactor2.specRigidbody);
					}
				}
				for (int n = 0; n < list2.Count; n++) {
					for (int num = 0; num < list2.Count; num++) {
						if (n != num) {
							list2[n].RegisterGhostCollisionException(list2[num]);
						}
					}
				}
			} else {
				Debug.LogError("Unknown spawn type: " + spawnPosition);
			}
		}
	
		private void HandleSpawn(AIActor spawnedActor) {
            if (!IsGlitchedLJ) {
                if (!string.IsNullOrEmpty(spawnAnim)) { spawnedActor.aiAnimator.PlayUntilFinished(spawnAnim, false, null, -1f, false); }
            }
            if (!IsGlitchedLJ) {
                ExpandSpawnGlitchEnemyOnDeath component = spawnedActor.GetComponent<ExpandSpawnGlitchEnemyOnDeath>();
                if (component) { component.guaranteedSpawnGenerations = Mathf.Max(0f, guaranteedSpawnGenerations - 1f); }
            }
            if (!spawnsCanDropLoot) {
				spawnedActor.CanDropCurrency = false;
				spawnedActor.CanDropItems = false;
			}
			if (DoNormalReinforcement) { spawnedActor.HandleReinforcementFallIntoRoom(0.1f); }            
        }

        protected override void OnDestroy() { base.OnDestroy(); }
	}
}

