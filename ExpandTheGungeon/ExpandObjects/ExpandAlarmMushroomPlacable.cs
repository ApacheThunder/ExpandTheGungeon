using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using Pathfinding;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandAlarmMushroomPlacable : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public ExpandAlarmMushroomPlacable() {
            TriggerAnimation = "alarm_mushroom_alarm";
            BreakAnimation = "alarm_mushroom_break";
            TriggerSFX = "Play_EXAlarmMushroom_01";

            // EnemySpawnOffset = new Vector2(0, 2);
            useAirDropSpawn = true;
            m_triggered = false;
        }

        public GameObject TriggerVFX;
        public GameObject DestroyVFX;
        
        public string TriggerAnimation;
        public string BreakAnimation;
        public string TriggerSFX;

        public bool useAirDropSpawn;

        public Vector2? EnemySpawnOffset;

        public DungeonPlaceable EnemySpawnPlacableOverride;

        private bool m_triggered;
        private RoomHandler m_room;

        private GameObject m_TriggerVFX;

        private void Start() {
            if (m_room == null) { m_room = GetAbsoluteParentRoom(); }
            if (m_room != null) {
                SpeculativeRigidbody SpecRigidbody = specRigidbody;
                if (SpecRigidbody) {
                    SpecRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(SpecRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
                }
            }
        }

        private void HandleTriggerCollision(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (m_triggered) { return; }
            PlayerController player = specRigidbody.GetComponent<PlayerController>();
            if (player) { StartCoroutine(Trigger()); }
        }

        private IEnumerator Trigger() {
            if (m_triggered) { yield break; }
            if (!string.IsNullOrEmpty(TriggerAnimation)) { spriteAnimator.Play(TriggerAnimation); }
            if (!string.IsNullOrEmpty(TriggerSFX)) { AkSoundEngine.PostEvent(TriggerSFX, gameObject); }
            m_triggered = true;
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
                        enemyObject.GetComponent<AIActor>().ConfigureOnPlacement(m_room);
                        ExpandUtility.SpawnParaDrop(m_room, SpawnPosition, enemyObject, DropHorizontalOffset: 10, useLandingVFX: false);
                    } else if (selectedVarient.nonDatabasePlaceable) {
                        GameObject ParaDroppedObject = Instantiate(selectedVarient.nonDatabasePlaceable, SpawnPosition, Quaternion.identity);
                        ExpandUtility.SpawnParaDrop(m_room, SpawnPosition, ParaDroppedObject, DropHorizontalOffset: 10, useLandingVFX: false);
                    } else {
                        ExpandUtility.SpawnParaDrop(m_room, SpawnPosition, DropHorizontalOffset: 10, useLandingVFX: false);
                        isExplodyBarrel = true;
                    }
                } else {
                    ExpandUtility.SpawnParaDrop(m_room, SpawnPosition, DropHorizontalOffset: 10, useLandingVFX: false);
                    isExplodyBarrel = true;
                }
                if (!m_room.IsSealed && !isExplodyBarrel) { m_room.SealRoom(); }
                yield return null;
                DestroyMushroom();
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
                    AIActor targetAIActor = AIActor.Spawn(selectedEnemy, specRigidbody.UnitCenter.ToIntVector2(VectorConversions.Floor) + SpawnOffset.ToIntVector2(), m_room, true, AIActor.AwakenAnimationType.Spawn, true);
                    targetAIActor.reinforceType = AIActor.ReinforceType.SkipVfx;
                    targetAIActor.HandleReinforcementFallIntoRoom(0.8f);
                    if (!m_room.IsSealed) { m_room.SealRoom(); }
                    while (targetAIActor.IsGone) { yield return null; }
                    DestroyMushroom();
                }
            }
            yield break;
        }

        private void DestroyMushroom(float additionalDelay = 0) { StartCoroutine(DelayedDestroy(additionalDelay)); }

        private IEnumerator DelayedDestroy(float additionalDelay) {
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
                LootEngine.DoDefaultItemPoof(specRigidbody.UnitCenter, false, false);
            }
            yield return null;
            if (m_TriggerVFX) { Destroy(m_TriggerVFX); }
            spriteAnimator.PlayAndDestroyObject(BreakAnimation);
            yield break;
        }
        
        public void ConfigureOnPlacement(RoomHandler room) { m_room = room; }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

