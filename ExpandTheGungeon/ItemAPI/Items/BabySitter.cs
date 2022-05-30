using System;
using UnityEngine;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;

namespace ExpandTheGungeon.ItemAPI {
    
    public class BabySitter : PassiveItem {
                
        public static GameObject BabySitterObject;
        
        public static void Init(AssetBundle expandSharedAssets1) {

            BabySitterObject = expandSharedAssets1.LoadAsset<GameObject>("Baby Sitter");
            SpriteSerializer.AddSpriteToObject(BabySitterObject, ExpandPrefabs.EXItemCollection, "babysitter");

            BabySitter babysitItem = BabySitterObject.AddComponent<BabySitter>();
            
            string shortDesc = "You've got a friend in me...";
            string longDesc = "Looks like you're stuck baby sitting him today.\n\nHe'll try his best to be useful.\nTry not to get him killed.";

            ItemBuilder.SetupItem(babysitItem, shortDesc, longDesc, "ex");
            babysitItem.quality = ItemQuality.B;
            if (!ExpandSettings.EnableEXItems) { babysitItem.quality = ItemQuality.EXCLUDED; }
            ExpandLists.CompanionItems.Add(BabySitterObject);
        }
        

        public BabySitter() {

            DeathStatModifier = new StatModifier() {
                amount = 1.8f,
                statToBoost = PlayerStats.StatType.Damage,
                modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
            };

            CompanionGuid = ExpandCustomEnemyDatabase.FriendlyCultistGUID;
            
            m_PickedUp = false;
            m_HasDied = false;
            m_HasGivenStats = false;
            PreventRespawnOnFloorLoad = false;
            m_healthRemaining = 0;
        }
        
        [EnemyIdentifier]
        public string CompanionGuid;

        public StatModifier DeathStatModifier;

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
        
        private void CompanionOnHealthChanged(float resultValue, float maxValue) {
            m_healthRemaining = resultValue;
            m_maxHealthRemaining = maxValue;
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
            extantCompanion2.GetComponent<HealthHaver>().OnHealthChanged += CompanionOnHealthChanged;
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
                m_extantCompanion.GetComponent<HealthHaver>().OnHealthChanged -= CompanionOnHealthChanged;
            }
            if (!m_HasDied) {
                m_maxHealthRemaining = m_extantCompanion.GetComponent<HealthHaver>().GetMaxHealth();
                m_healthRemaining = m_extantCompanion.GetComponent<HealthHaver>().GetCurrentHealth();
            }
            Destroy(m_extantCompanion);
            m_extantCompanion = null;
        }

        protected override void Update() { base.Update(); }

        public override void Pickup(PlayerController player) {
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
                BabySitter component = drop.gameObject.GetComponent<BabySitter>();                
                if (component) {
                    component.m_HasDied = m_HasDied;
                    component.m_PickedUp = true;
                    component.m_healthRemaining = m_healthRemaining;
                    component.m_maxHealthRemaining = m_maxHealthRemaining;
                    if (component.m_HasDied) {
                        component.m_pickedUpThisRun = true;
                        component.Break();
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

