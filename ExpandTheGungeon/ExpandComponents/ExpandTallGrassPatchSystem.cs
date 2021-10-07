using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandUtilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandTallGrassPatchSystem : MonoBehaviour {

        public ExpandTallGrassPatchSystem() {
            m_fireData = new Dictionary<IntVector2, EnflamedGrassData>(new IntVector2EqualityComparer());
            m_tiledSpritePool = new Dictionary<IntVector2, tk2dTiledSprite>();
        }

        [NonSerialized]
        public List<IntVector2> cells;

        private enum IndexPosition { TOP = 124, MIDDLE = 147, MIDDLEBOTTOM = 146, BOTTOM = 168 };
        
        private Dictionary<IntVector2, EnflamedGrassData> m_fireData;

        private ParticleSystem m_fireSystem;
        private ParticleSystem m_fireIntroSystem;
        private ParticleSystem m_fireOutroSystem;

        private Dictionary<IntVector2, tk2dTiledSprite> m_tiledSpritePool;

        private bool m_isPlayingFireAudio;

        private GameObject m_stripPrefab;

        private GoopDefinition m_GoopDefinition;

        internal struct EnflamedGrassData {
            public float fireTime;
            public bool hasEnflamedNeighbors;
            public bool HasPlayedFireOutro;
            public bool HasPlayedFireIntro;
            public float ParticleTimer;
        }

        private void LateUpdate() {
            bool flag = false;
            for (int i = 0; i < cells.Count; i++) {
                if (m_fireData.ContainsKey(cells[i])) {
                    EnflamedGrassData enflamedGrassData = m_fireData[cells[i]];
                    enflamedGrassData.fireTime += BraveTime.DeltaTime;
                    enflamedGrassData.ParticleTimer -= BraveTime.DeltaTime;
                    if (!m_fireData[cells[i]].hasEnflamedNeighbors && m_fireData[cells[i]].fireTime > 0.1f) {
                        IgniteCell(cells[i] + IntVector2.North);
                        IgniteCell(cells[i] + IntVector2.East);
                        IgniteCell(cells[i] + IntVector2.South);
                        IgniteCell(cells[i] + IntVector2.West);
                        enflamedGrassData.hasEnflamedNeighbors = true;
                    }
                    if (m_fireData[cells[i]].fireTime > 2.6f && m_tiledSpritePool.ContainsKey(cells[i])) { DestroyPatch(cells[i]); }
                    if (enflamedGrassData.HasPlayedFireOutro && enflamedGrassData.ParticleTimer <= 0f) {
                        RemovePosition(cells[i]);
                        i--;
                    } else {
                        enflamedGrassData = DoParticleAtPosition(cells[i], enflamedGrassData);
                        m_fireData[cells[i]] = enflamedGrassData;
                    }
                }
            }
            if (flag && !m_isPlayingFireAudio) {
                m_isPlayingFireAudio = true;
                AkSoundEngine.PostEvent("Play_ENV_oilfire_ignite_01", GameManager.Instance.PrimaryPlayer.gameObject);
            }
        }

        private void InitializeParticleSystem() {
            if (m_fireSystem != null) { return; }
            GameObject fireMain = GameObject.Find("Gungeon_Fire_Main");
            if (fireMain == null) {
                fireMain = (GameObject)Instantiate(BraveResources.Load("Particles/Gungeon_Fire_Main_raw", ".prefab"), Vector3.zero, Quaternion.identity);
                fireMain.name = "Gungeon_Fire_Main";
            }
            m_fireSystem = fireMain.GetComponent<ParticleSystem>();
            GameObject fireIntro = GameObject.Find("Gungeon_Fire_Intro");
            if (fireIntro == null) {
                fireIntro = (GameObject)Instantiate(BraveResources.Load("Particles/Gungeon_Fire_Intro_raw", ".prefab"), Vector3.zero, Quaternion.identity);
                fireIntro.name = "Gungeon_Fire_Intro";
            }
            m_fireIntroSystem = fireIntro.GetComponent<ParticleSystem>();
            GameObject fireOutro = GameObject.Find("Gungeon_Fire_Outro");
            if (fireOutro == null) {
                fireOutro = (GameObject)Instantiate(BraveResources.Load("Particles/Gungeon_Fire_Outro_raw", ".prefab"), Vector3.zero, Quaternion.identity);
                fireOutro.name = "Gungeon_Fire_Outro";
            }
            m_fireOutroSystem = fireOutro.GetComponent<ParticleSystem>();
        }

        private IndexPosition GetTargetIndexForPosition(IntVector2 current) {
            bool North = cells.Contains(current + IntVector2.North);
            bool South = cells.Contains(current + IntVector2.South);
            bool SouthSouth = cells.Contains(current + IntVector2.South + IntVector2.South);
            if (North && South && SouthSouth) {
                return IndexPosition.MIDDLE;
            } else if (North && South) {
                return IndexPosition.MIDDLEBOTTOM;
            } else if (North && !South) {
                return IndexPosition.BOTTOM;
            } else if (!North && South) {
                return IndexPosition.TOP;
            } else {
                return IndexPosition.BOTTOM;
            }
        }

        public void IgniteCircle(Vector2 center, float radius) {
            for (int i = Mathf.FloorToInt(center.x - radius); i < Mathf.CeilToInt(center.x + radius); i++) {
                for (int j = Mathf.FloorToInt(center.y - radius); j < Mathf.CeilToInt(center.y + radius); j++) {
                    if (Vector2.Distance(new Vector2(i, j), center) < radius) { IgniteCell(new IntVector2(i, j)); }
                }
            }
        }

        public void IgniteCell(IntVector2 cellPosition) {
            if (cells.Contains(cellPosition)) {
                if (m_fireData.ContainsKey(cellPosition)) { return; }
                m_fireData.Add(cellPosition, default(EnflamedGrassData));
                DoFireAtPosition(cellPosition, 4);
            }
        }

        private EnflamedGrassData DoParticleAtPosition(IntVector2 worldPos, EnflamedGrassData fireData) {
            if (m_fireSystem != null && fireData.ParticleTimer <= 0f) {
                bool ContainsCell = cells.Contains(worldPos + IntVector2.South);
                for (int X = 0; X < 2; X++) {
                    for (int Y = 0; Y < 2; Y++) {
                        if (ContainsCell || Y != 0) {
                            float num = UnityEngine.Random.Range(1f, 1.5f);
                            float num2 = UnityEngine.Random.Range(0.75f, 1f);
                            Vector2 vector = worldPos.ToVector3() + new Vector3(0.33f + 0.33f * X, 0.33f + 0.33f * Y, 0f);
                            vector += UnityEngine.Random.insideUnitCircle / 5f;
                            if (!fireData.HasPlayedFireOutro) {
                                if (!fireData.HasPlayedFireOutro && fireData.fireTime > 3f && m_fireOutroSystem != null) {
                                    num = num2;
                                    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams {
                                        position = vector,
                                        velocity = Vector3.zero,
                                        startSize = m_fireSystem.main.startSize.constant,
                                        startLifetime = num2,
                                        startColor = m_fireSystem.main.startColor.color,
                                        randomSeed = (uint)(UnityEngine.Random.value * 4.2949673E+09f)
                                    };
                                    m_fireOutroSystem.Emit(emitParams, 1);
                                    if (X == 1 && Y == 1) { fireData.HasPlayedFireOutro = true; }
                                } else if (!fireData.HasPlayedFireIntro && m_fireIntroSystem != null) {
                                    num = UnityEngine.Random.Range(0.75f, 1f);
                                    ParticleSystem.EmitParams emitParams2 = new ParticleSystem.EmitParams {
                                        position = vector,
                                        velocity = Vector3.zero,
                                        startSize = m_fireSystem.main.startSize.constant,
                                        startLifetime = num,
                                        startColor = m_fireSystem.main.startColor.color,
                                        randomSeed = (uint)(UnityEngine.Random.value * 4.2949673E+09f)
                                    };
                                    m_fireIntroSystem.Emit(emitParams2, 1);
                                    if (X == 1 && Y == 1) { fireData.HasPlayedFireIntro = true; }
                                } else if (UnityEngine.Random.value < 0.5f) {
                                    ParticleSystem.EmitParams emitParams3 = new ParticleSystem.EmitParams {
                                        position = vector,
                                        velocity = Vector3.zero,
                                        startSize = m_fireSystem.main.startSize.constant,
                                        startLifetime = num,
                                        startColor = m_fireSystem.main.startColor.color,
                                        randomSeed = (uint)(UnityEngine.Random.value * 4.2949673E+09f)
                                    };
                                    m_fireSystem.Emit(emitParams3, 1);
                                }
                            }
                            if (X == 1 && Y == 1) { fireData.ParticleTimer = num - 0.125f; }
                        }
                    }
                }
            }
            return fireData;
        }

        private void DoFireAtPosition(IntVector2 position, float timeToLive) {
            if (!m_GoopDefinition) {
                m_GoopDefinition = ExpandAssets.LoadOfficialAsset<GoopDefinition>("NapalmGoopThatWorks", ExpandAssets.AssetSource.SharedAuto1);
            }
            GameObject newFireGoopObject = new GameObject("Jungle Fire Object" + UnityEngine.Random.Range(0, 99999));
            newFireGoopObject.transform.position = position.ToVector3(); // + new Vector3(0.5f, 0.5f);
            if (newFireGoopObject.transform.position.GetAbsoluteRoom() != null) {
                newFireGoopObject.transform.SetParent(newFireGoopObject.transform.position.GetAbsoluteRoom().hierarchyParent);
            }
            newFireGoopObject.SetActive(false);
            TimedObjectKiller objectKiller = newFireGoopObject.AddComponent<TimedObjectKiller>();
            objectKiller.lifeTime = timeToLive;
            objectKiller.m_poolType = TimedObjectKiller.PoolType.NonPooled;            
            ExpandUtility.GenerateOrAddToRigidBody(newFireGoopObject, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(1, 1));
            SpeculativeRigidbody rigidBody = newFireGoopObject.GetComponent<SpeculativeRigidbody>();
            rigidBody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(rigidBody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
            newFireGoopObject.SetActive(true);
        }
        
        private void HandleTriggerCollision(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            PlayerController player = specRigidbody.GetComponent<PlayerController>();
            AIActor aiActor = specRigidbody.GetComponent<AIActor>();
            if (player) {
                if (!player.IsOnFire && player.IsGrounded && !player.IsSlidingOverSurface && player.healthHaver && player.healthHaver.IsVulnerable) {
                    player.IsOnFire = true;
                    player.CurrentFireMeterValue += BraveTime.DeltaTime * 0.5f;
                    player.ApplyEffect(m_GoopDefinition.fireEffect);
                }
            } else if (aiActor && aiActor.GetResistanceForEffectType(EffectResistanceType.Fire) < 1f) {
                float num = 0f;
                if (aiActor.GetResistanceForEffectType(EffectResistanceType.Fire) < 1f) { num += 1f * BraveTime.DeltaTime; }
                aiActor.ApplyEffect(m_GoopDefinition.fireEffect);
                if (num > 0f) {
                    aiActor.healthHaver.ApplyDamage(num, Vector2.zero, StringTableManager.GetEnemiesString("#GOOP", -1), CoreDamageTypes.Fire, DamageCategory.Environment, false, null, false);
                }
            }
        }

        private void RemovePosition(IntVector2 pos) {
            if (cells.Contains(pos)) { cells.Remove(pos); }
        }

        private void DestroyPatch(IntVector2 pos) {
            if (m_tiledSpritePool.ContainsKey(pos)) {
                tk2dTiledSprite tiledSprite;
                m_tiledSpritePool.TryGetValue(pos, out tiledSprite);
                if (tiledSprite) { SpawnManager.Despawn(tiledSprite.gameObject); }
                m_tiledSpritePool.Remove(pos);
            }
        }

        public void BuildPatch() {
            if (m_stripPrefab == null) { m_stripPrefab = (GameObject)BraveResources.Load("Global Prefabs/TallGrassStrip", ".prefab"); }
            HashSet<IntVector2> hashSet = new HashSet<IntVector2>();
            for (int j = 0; j < cells.Count; j++) {
                IntVector2 intVector = cells[j];
                if (!hashSet.Contains(intVector)) {
                    hashSet.Add(intVector);
                    int num = 1;
                    var targetIndexForPosition = GetTargetIndexForPosition(intVector);
                    IntVector2 intVector2 = intVector;
                    while(true) { 
                        intVector2 += IntVector2.Right;
                        if (hashSet.Contains(intVector2)) { break; }
                        if (!cells.Contains(intVector2)) { break; }
                        if (targetIndexForPosition != GetTargetIndexForPosition(intVector2)) { break; }
                        num++;
                        hashSet.Add(intVector2);
                    }
                    GameObject grassObject = SpawnManager.SpawnVFX(m_stripPrefab, false);
                    tk2dTiledSprite tiledSprite = grassObject.GetComponent<tk2dTiledSprite>();
                    tiledSprite.SetSprite(GameManager.Instance.Dungeon.tileIndices.dungeonCollection, (int)targetIndexForPosition);
                    tiledSprite.dimensions = new Vector2((16 * num), 16f);
                    grassObject.transform.position = new Vector3(intVector.x, intVector.y, intVector.y);
                    m_tiledSpritePool.Add(intVector, tiledSprite);
                    switch (targetIndexForPosition) {
                        case IndexPosition.BOTTOM:
                            tiledSprite.HeightOffGround = -2f;
                            tiledSprite.IsPerpendicular = true;
                            tiledSprite.transform.position += new Vector3(0f, 0.6875f, 0f);
                            break;
                        case IndexPosition.TOP:
                            tiledSprite.IsPerpendicular = true;
                            break;
                        default:
                            tiledSprite.IsPerpendicular = false;
                            break;
                    }
                    tiledSprite.UpdateZDepth();
                }
            }
            if (!ExpandStaticReferenceManager.AllGrasses.Contains(this)) { ExpandStaticReferenceManager.AllGrasses.Add(this); }
            InitializeParticleSystem();
        }
    }
}

