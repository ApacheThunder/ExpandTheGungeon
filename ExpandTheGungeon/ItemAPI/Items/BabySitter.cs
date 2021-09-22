using System.Reflection;
using System;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandUtilities;
using System.Collections.Generic;
using System.Collections;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandMain;
using System.Collections.ObjectModel;
using tk2dRuntime.TileMap;
using Pathfinding;

namespace ExpandTheGungeon.ItemAPI {
    
    public class BabySitter : PassiveItem {
                
        public static GameObject BabySitterobject;
        
        public static void Init(AssetBundle expandSharedAssets1) {

            BabySitterobject = expandSharedAssets1.LoadAsset<GameObject>("Baby Sitter");
            tk2dSprite BabySitterSprite = BabySitterobject.AddComponent<tk2dSprite>();
            BabySitterSprite.SetSprite(ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), "babysitter");

            BabySitter babysitItem = BabySitterobject.AddComponent<BabySitter>();
            // ItemBuilder.AddSpriteToObject(BabySitterobject, expandSharedAssets1.LoadAsset<Texture2D>("babysitter"));
            
            string shortDesc = "You've got a friend in me...";
            string longDesc = "Looks like you're stuck baby sitting him today.\n\nHe'll try his best to be useful.\nTry not to get him killed.";

            ItemBuilder.SetupItem(babysitItem, shortDesc, longDesc, "ex");
            babysitItem.quality = ItemQuality.B;
            if (!ExpandStats.EnableEXItems) { babysitItem.quality = ItemQuality.EXCLUDED; }
            babysitItem.CompanionGuid = "1d1e1070617842f09e6f45df3cb223f6";
            babysitItem.DeathStatModifier = new StatModifier() {
                amount = 1.8f,
                statToBoost = PlayerStats.StatType.Damage,
                modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
            };
        }
        

        public BabySitter() {
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

        [NonSerialized]
        public bool PreventRespawnOnFloorLoad;
                
        private GameObject m_extantCompanion;
        
        public GameObject ExtantCompanion { get { return m_extantCompanion; } }

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
            if (m_healthRemaining > 0) { extantCompanion2.GetComponent<HealthHaver>().ForceSetCurrentHealth(m_healthRemaining); }
            if (orAddComponent.specRigidbody) {
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent.specRigidbody, null, false);
            }
        }
                

        public void OnPreDeath(Vector2 direction) {
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

        public void ForceCompanionRegeneration(PlayerController owner, Vector2? overridePosition) {
            bool flag = false;
            Vector2 vector = Vector2.zero;
            if (m_extantCompanion) {
                flag = true;
                vector = m_extantCompanion.transform.position.XY();
            }
            if (overridePosition != null) {
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
            m_extantCompanion.GetComponent<HealthHaver>().OnPreDeath -= OnPreDeath;
            Destroy(m_extantCompanion);
            m_extantCompanion = null;
        }

        protected override void Update() {
            base.Update();
            if (this && !m_HasDied && m_PickedUp && m_extantCompanion) {
                m_healthRemaining = m_extantCompanion.GetComponent<HealthHaver>().GetCurrentHealth();
            }
        }

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
            data.Add(m_HasGivenStats);
        }

        public override void MidGameDeserialize(List<object> data) {
            base.MidGameDeserialize(data);
            if (data.Count == 4) {
                m_PickedUp = (bool)data[0];
                m_HasDied = (bool)data[1];
                m_healthRemaining = (float)data[2];
                m_HasGivenStats = (bool)data[3];
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

