using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using Pathfinding;
using ExpandTheGungeon.ExpandMain;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandAlarmMushroomPlacable : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public ExpandAlarmMushroomPlacable() {
            TriggerSFX = "Play_EXAlarmMushroom_01";
            TriggerAnimation = "alarm_mushroom_alarm";
            BreakAnimation = "alarm_mushroom_break";
            DeadSpriteName = "alarm_mushroom2_dead_001";
            // EnemySpawnOffset = new Vector2(0, 2);
            useAirDropSpawn = true;
            IsDead = false;

            m_triggered = false;
        }

        [SerializeField]
        public GameObject TriggerVFX;
        [SerializeField]
        public GameObject DestroyVFX;
        [SerializeField]
        public string TriggerSFX;
        [SerializeField]
        public string TriggerAnimation;
        [SerializeField]
        public string BreakAnimation;
        [SerializeField]
        public string DeadSpriteName;
        [SerializeField]
        public bool useAirDropSpawn;
        [SerializeField]
        public Vector2? EnemySpawnOffset;
        [SerializeField]
        public DungeonPlaceable EnemySpawnPlacableOverride;

        [NonSerialized]
        public RoomHandler ParentRoom;
        [NonSerialized]
        public bool IsDead;

        [NonSerialized]
        private bool m_triggered;
        [NonSerialized]
        private GameObject m_TriggerVFX;

        private void Start() {
            if (ParentRoom == null) { ParentRoom = GetAbsoluteParentRoom(); }
            if (ParentRoom != null) {
                SpeculativeRigidbody SpecRigidbody = specRigidbody;
                if (SpecRigidbody) {
                    SpecRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(SpecRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
                }
            }
        }

        private void Update() {
            if (IsDead | GameManager.Instance.IsLoadingLevel | m_triggered | ParentRoom == null) { return; }
            
            if (!ParentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) && !ParentRoom.IsSealed) {
                IsDead = true;
                spriteAnimator.Stop();
                sprite.SetSprite(DeadSpriteName);
                Destroy(specRigidbody);
                Destroy(this);
            }
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            ParentRoom = room;
            if (ExpandStaticReferenceManager.AllAlarmMushrooms != null) {
                ExpandStaticReferenceManager.AllAlarmMushrooms.Add(this);
            } else {
                ExpandStaticReferenceManager.AllAlarmMushrooms = new List<ExpandAlarmMushroomPlacable>() { this };
            }
        }

        public void TriggerNow(bool DoSoundFX) {
            if (m_triggered) { return; }
            StartCoroutine(Trigger(DoSoundFX));
        }

        private void HandleTriggerCollision(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (m_triggered) { return; }
            PlayerController player = specRigidbody.GetComponent<PlayerController>();
            if (player) {
                ExpandAlarmMushroomPlacable[] AllMushrooms = FindObjectsOfType<ExpandAlarmMushroomPlacable>();
                if (AllMushrooms != null && AllMushrooms.Length > 0) {
                    foreach (ExpandAlarmMushroomPlacable alarmMushroom in AllMushrooms) {
                        if (alarmMushroom && alarmMushroom != this && !alarmMushroom.IsDead && alarmMushroom.ParentRoom == ParentRoom) {
                            alarmMushroom.TriggerNow(false);
                        }
                    }
                }
                StartCoroutine(Trigger(true));
            }
        }
        

        private IEnumerator Trigger(bool DoSoundFX) {
            if (m_triggered) { yield break; }
            m_triggered = true;
            if (!string.IsNullOrEmpty(TriggerAnimation)) { spriteAnimator.Play(TriggerAnimation); }
            if (DoSoundFX && !string.IsNullOrEmpty(TriggerSFX)) { AkSoundEngine.PostEvent(TriggerSFX, gameObject); }
            Vector2 SpawnOffset = Vector2.zero;
            if (EnemySpawnOffset.HasValue) { SpawnOffset = EnemySpawnOffset.Value; }
            if (TriggerVFX) {
                if (useAirDropSpawn) {
                    m_TriggerVFX = SpawnManager.SpawnVFX(TriggerVFX, specRigidbody.UnitBottomCenter - new Vector2(0, 0.25f), Quaternion.identity);
                } else {
                    m_TriggerVFX = SpawnManager.SpawnVFX(TriggerVFX, specRigidbody.UnitBottomCenter + SpawnOffset, Quaternion.identity);
                }
            }
            
            if (useAirDropSpawn) {
                Vector2 SpawnPosition = transform.position;

                DungeonPlaceable selectedPlacable = null;
                bool isExplodyBarrel = false;
                if (EnemySpawnPlacableOverride) {
                    selectedPlacable = EnemySpawnPlacableOverride;
                } else {
                    RobotDaveIdea targetIdea = (!GameManager.Instance.Dungeon.UsesCustomFloorIdea) ? GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultProceduralIdea : GameManager.Instance.Dungeon.FloorIdea;
                    selectedPlacable = BraveUtility.RandomElement(targetIdea.ValidEasyEnemyPlaceables);
                }
                DungeonPlaceableVariant selectedVarient = BraveUtility.RandomElement(selectedPlacable.variantTiers);
                if (selectedVarient != null && UnityEngine.Random.value > 0.2f) {
                    if (!string.IsNullOrEmpty(selectedVarient.enemyPlaceableGuid)) {
                        GameObject enemyObject = Instantiate(EnemyDatabase.GetOrLoadByGuid(selectedVarient.enemyPlaceableGuid).gameObject, SpawnPosition, Quaternion.identity);
                        enemyObject.GetComponent<AIActor>().ConfigureOnPlacement(ParentRoom);
                        ExpandUtility.SpawnParaDrop(ParentRoom, SpawnPosition, enemyObject, DropHorizontalOffset: 10, useLandingVFX: false);
                    } else if (selectedVarient.nonDatabasePlaceable) {
                        GameObject ParaDroppedObject = Instantiate(selectedVarient.nonDatabasePlaceable, SpawnPosition, Quaternion.identity);
                        ExpandUtility.SpawnParaDrop(ParentRoom, SpawnPosition, ParaDroppedObject, DropHorizontalOffset: 10, useLandingVFX: false);
                    } else {
                        ExpandUtility.SpawnParaDrop(ParentRoom, SpawnPosition, DropHorizontalOffset: 10, useLandingVFX: false);
                        isExplodyBarrel = true;
                    }
                } else {
                    ExpandUtility.SpawnParaDrop(ParentRoom, SpawnPosition, DropHorizontalOffset: 10, useLandingVFX: false);
                    isExplodyBarrel = true;
                }
                if (!ParentRoom.IsSealed && !isExplodyBarrel) { ParentRoom.SealRoom(); }
                yield return null;
                DestroyMushroom(DoSoundFX);
            } else {
                AIActor selectedEnemy = null;
                if (EnemySpawnPlacableOverride) {
                    DungeonPlaceableVariant enemyVariant = EnemySpawnPlacableOverride.SelectFromTiersFull();
                    selectedEnemy = enemyVariant.GetOrLoadPlaceableObject.GetComponent<AIActor>();
                } else {
                    RobotDaveIdea targetIdea = (!GameManager.Instance.Dungeon.UsesCustomFloorIdea) ? GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultProceduralIdea : GameManager.Instance.Dungeon.FloorIdea;
                    DungeonPlaceable backupEnemyPlaceable = targetIdea.ValidEasyEnemyPlaceables[UnityEngine.Random.Range(0, targetIdea.ValidEasyEnemyPlaceables.Length)];
                    DungeonPlaceableVariant enemyVariant = backupEnemyPlaceable.SelectFromTiersFull();
                }
                if (selectedEnemy) {
                    AIActor targetAIActor = AIActor.Spawn(selectedEnemy, specRigidbody.UnitCenter.ToIntVector2(VectorConversions.Floor) + SpawnOffset.ToIntVector2(), ParentRoom, true, AIActor.AwakenAnimationType.Spawn, true);
                    targetAIActor.reinforceType = AIActor.ReinforceType.SkipVfx;
                    targetAIActor.HandleReinforcementFallIntoRoom(0.8f);
                    if (!ParentRoom.IsSealed) { ParentRoom.SealRoom(); }
                    while (targetAIActor.IsGone) { yield return null; }
                    DestroyMushroom(DoSoundFX);
                }
            }
            yield break;
        }

        private void DestroyMushroom(bool DoSoundFX, float additionalDelay = 0) {
            if (ExpandStaticReferenceManager.AllAlarmMushrooms != null && ExpandStaticReferenceManager.AllAlarmMushrooms.Count > 0) {
                ExpandStaticReferenceManager.AllAlarmMushrooms.Remove(this);
            }
            bool MuteSX = false;
            if (!DoSoundFX) { MuteSX = true; }
            StartCoroutine(DelayedDestroy(additionalDelay, MuteSX));
        }

        private IEnumerator DelayedDestroy(float additionalDelay, bool MuteAudio) {
            spriteAnimator.Play(TriggerAnimation);
            float elapsed = 0;
            float delay = (2.5f + additionalDelay);
            while (elapsed < delay) {
                elapsed += BraveTime.DeltaTime;
                yield return null;
            }
            spriteAnimator.Play(BreakAnimation);
            if (DestroyVFX) {
                SpawnManager.SpawnVFX(DestroyVFX, specRigidbody.UnitCenter, Quaternion.identity);
            } else {
                LootEngine.DoDefaultItemPoof(specRigidbody.UnitCenter, false, MuteAudio);
            }
            yield return null;
            if (m_TriggerVFX) { Destroy(m_TriggerVFX); }
            spriteAnimator.PlayAndDestroyObject(BreakAnimation);
            yield break;
        }
        
        protected override void OnDestroy() {
            if (ExpandStaticReferenceManager.AllAlarmMushrooms != null && ExpandStaticReferenceManager.AllAlarmMushrooms.Count > 0 && ExpandStaticReferenceManager.AllAlarmMushrooms.Contains(this)) {
                ExpandStaticReferenceManager.AllAlarmMushrooms.Remove(this);
            }
            base.OnDestroy();
        }
    }
}

