using System;
using UnityEngine;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ItemAPI {
    
    public class SonicBox : PassiveItem {
                
        public static GameObject SonicBoxObject;
        
        private static readonly List<string> m_IdleFrames = new List<string>() { "SonicBox_Idle_01", "SonicBox_Idle_02", "SonicBox_Idle_03" };

        public static void Init(AssetBundle expandSharedAssets1) {

            SonicBoxObject = expandSharedAssets1.LoadAsset<GameObject>("EXSonicBox");
            SpriteSerializer.AddSpriteToObject(SonicBoxObject, ExpandPrefabs.EXItemCollection, "SonicBox_Idle_02");
            
            ExpandUtility.GenerateSpriteAnimator(SonicBoxObject, playAutomatically: true);
            ExpandUtility.AddAnimation(SonicBoxObject.GetComponent<tk2dSpriteAnimator>(), ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), m_IdleFrames, "Idle", tk2dSpriteAnimationClip.WrapMode.Loop, 16);
            
            SonicBox sonicBoxItem = SonicBoxObject.AddComponent<SonicBox>();

            string name = "Sonic Box";
            string shortDesc = "Gotta Go Fast!";
            string longDesc = "Sonic the Hedgehog has found his way to the Gungeon and loves to spin dash into the Gundead!";

            ItemBuilder.SetupEXItem(sonicBoxItem, name, shortDesc, longDesc);
            sonicBoxItem.quality = ItemQuality.A;
            if (!ExpandSettings.EnableEXItems) { sonicBoxItem.quality = ItemQuality.EXCLUDED; };

            ExpandLists.CompanionItems.Add(SonicBoxObject);
        }
        

        public SonicBox() {
            CompanionGuid = ExpandCustomEnemyDatabase.SonicCompanionGUID;

            DeathStatModifier = new StatModifier() {
                amount = 1.5f,
                statToBoost = PlayerStats.StatType.Damage,
                modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
            };
            RingDropChance = 0.25f;
            HitAnimation = "rebound";

            m_PickedUp = false;
            m_HasDied = false;
            m_HasGivenStats = false;
            m_canSpawnRings = false;
            PreventRespawnOnFloorLoad = false;
            m_healthRemaining = 0;
            m_RingCoolDown = 0;
        }
        
        [EnemyIdentifier]
        public string CompanionGuid;

        public StatModifier DeathStatModifier;

        public string HitAnimation;
        public float RingDropChance;
        

        [NonSerialized]
        private bool m_PickedUp;
        [NonSerialized]
        private bool m_HasDied;
        [NonSerialized]
        private bool m_HasGivenStats;
        [NonSerialized]
        private float m_healthRemaining;
        private float m_maxHealthRemaining;

        [NonSerialized]
        public bool PreventRespawnOnFloorLoad;
                
        private GameObject m_extantCompanion;

        private bool m_canSpawnRings;

        private float m_RingCoolDown;
     
        private void CompanionOnDamaged(float resultValue, float maxValue, CoreDamageTypes damageTypes, DamageCategory damageCategory, Vector2 damageDirection) {
            m_healthRemaining = resultValue;
            m_maxHealthRemaining = maxValue;

            if (m_canSpawnRings && RingDropChance > UnityEngine.Random.value) {
                if (m_extantCompanion) {
                    m_extantCompanion.GetComponent<AIActor>().behaviorSpeculator.Stun(1);
                    m_extantCompanion.GetComponent<AIAnimator>().PlayUntilFinished(HitAnimation);
                    int RingCount = UnityEngine.Random.Range(3, 10);
                    ExpandUtility.SpawnCustomCurrency(m_extantCompanion.GetComponent<SpeculativeRigidbody>().GetUnitCenter(ColliderType.HitBox), RingCount, SonicRing.RingID);
                    AkSoundEngine.PostEvent("Play_EX_SonicLoseRings_01", m_extantCompanion);
                }
            }
            m_canSpawnRings = false;
        }

        private void OnPreDeath(Vector2 direction) {
            if (this && m_owner && !m_HasDied && !m_HasGivenStats) {
                m_HasGivenStats = true;
                StatModifier curseMod = new StatModifier() {
                    statToBoost = PlayerStats.StatType.Curse,
                    modifyType = StatModifier.ModifyMethod.ADDITIVE,
                    amount = 2
                };
                m_owner.ownerlessStatModifiers.Add(curseMod);
                m_owner.ownerlessStatModifiers.Add(DeathStatModifier);
                m_owner.stats.RecalculateStats(m_owner, false, false);
                m_HasDied = true;
                m_owner.DropPassiveItem(this);
            }
        }

        private void CreateCompanion(PlayerController owner) {
            if (PreventRespawnOnFloorLoad | m_HasDied) { return; }
            
            string guid = CompanionGuid;
            
            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
            Vector3 vector = owner.transform.position;
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER) { vector += new Vector3(1.125f, -0.3125f, 0f); }
            GameObject extantCompanion2 = Instantiate(orLoadByGuid.gameObject, vector, Quaternion.identity);
            m_extantCompanion = extantCompanion2;
            CompanionController orAddComponent = m_extantCompanion.GetOrAddComponent<CompanionController>();
            orAddComponent.Initialize(owner);
            extantCompanion2.GetComponent<HealthHaver>().OnPreDeath += OnPreDeath;
            extantCompanion2.GetComponent<HealthHaver>().OnDamaged += CompanionOnDamaged;
            if (m_healthRemaining > 0) {
                extantCompanion2.GetComponent<HealthHaver>().SetHealthMaximum(m_maxHealthRemaining);
                extantCompanion2.GetComponent<HealthHaver>().ForceSetCurrentHealth(m_healthRemaining);
            }
            if (orAddComponent.specRigidbody) {
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent.specRigidbody, null, false);
            }
        }

        private void DestroyCompanion() {
            if (!m_extantCompanion) { return; }
            if (m_extantCompanion.GetComponent<HealthHaver>()) {
                m_extantCompanion.GetComponent<HealthHaver>().OnPreDeath -= OnPreDeath;
                m_extantCompanion.GetComponent<HealthHaver>().OnDamaged -= CompanionOnDamaged;
            }
            if (!m_HasDied) {
                m_maxHealthRemaining = m_extantCompanion.GetComponent<HealthHaver>().GetMaxHealth();
                m_healthRemaining = m_extantCompanion.GetComponent<HealthHaver>().GetCurrentHealth();
            }
            Destroy(m_extantCompanion);
            m_extantCompanion = null;
        }

        protected override void Update() {
            if (!m_canSpawnRings) {
                m_RingCoolDown += BraveTime.DeltaTime;
                if (m_RingCoolDown > 2) {
                    m_RingCoolDown = 0;
                    m_canSpawnRings = true;
                }
            }
            base.Update();
        }

        public override void Pickup(PlayerController player) {
            if (m_HasDied) {
                spriteAnimator.playAutomatically = false;
                spriteAnimator.Stop();
                sprite.SetSprite("SonicBox_Broken_01");
            } else {
                spriteAnimator.playAutomatically = false;
                spriteAnimator.Stop();
                sprite.SetSprite("SonicBox_Idle_02");
            }
            base.Pickup(player);
            player.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Combine(player.OnNewFloorLoaded, new Action<PlayerController>(HandleNewFloor));
            CreateCompanion(player);
        }

        private void HandleNewFloor(PlayerController obj) {
            DestroyCompanion();
            if (!PreventRespawnOnFloorLoad | !m_HasDied) { CreateCompanion(obj); }
        }

        public override DebrisObject Drop(PlayerController player) {
            DestroyCompanion();
            player.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Remove(player.OnNewFloorLoaded, new Action<PlayerController>(HandleNewFloor));
            DebrisObject drop = base.Drop(player);
            
            if (drop) {
                SonicBox component = drop.gameObject.GetComponent<SonicBox>();                
                if (component) {
                    component.m_HasDied = m_HasDied;
                    component.m_PickedUp = true;
                    component.m_healthRemaining = m_healthRemaining;
                    component.m_maxHealthRemaining = m_maxHealthRemaining;
                    if (component.m_HasDied) {
                        component.m_pickedUpThisRun = true;
                        component.spriteAnimator.playAutomatically = false;
                        component.spriteAnimator.Stop();
                        component.sprite.SetSprite("SonicBox_Broken_01");
                        component.Break();
                    } else {
                        component.spriteAnimator.playAutomatically = true;
                        component.spriteAnimator.Play();
                    }
                }
            }
            return drop;
        }

        public void Break() {
            m_pickedUp = true;
            Destroy(gameObject, 1f);
        }

        public override void MidGameSerialize(List<object> data) {
            base.MidGameSerialize(data);
            data.Add(m_PickedUp);
            data.Add(m_HasDied);
            data.Add(m_healthRemaining);
            data.Add(m_maxHealthRemaining);
            data.Add(m_HasGivenStats);
        }

        public override void MidGameDeserialize(List<object> data) {
            base.MidGameDeserialize(data);
            if (data.Count == 4) {
                m_PickedUp = (bool)data[0];
                m_HasDied = (bool)data[1];
                m_healthRemaining = (float)data[2];
                m_maxHealthRemaining = (float)data[3];
                m_HasGivenStats = (bool)data[4];
            }
        }

        protected override void OnDestroy() {
            if (m_owner != null) {
                PlayerController owner = m_owner;
                owner.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Remove(owner.OnNewFloorLoaded, new Action<PlayerController>(HandleNewFloor));
            }
            DestroyCompanion();
            base.OnDestroy();
        }
    }
}

