using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;

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
            if (TriggerVFX) { SpawnManager.SpawnVFX(TriggerVFX, specRigidbody.UnitCenter + SpawnOffset, Quaternion.identity); }
            if (useAirDropSpawn) {
                EmergencyCrateController spawnedEnemyCrate = null;
                if (!EnemySpawnPlacableOverride) {
                    RobotDaveIdea targetIdea = (!GameManager.Instance.Dungeon.UsesCustomFloorIdea) ? GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultProceduralIdea : GameManager.Instance.Dungeon.FloorIdea;
                    GameObject eCrateInstance = ExpandUtility.SpawnAirDrop(m_room, sprite.WorldCenter, null, targetIdea.ValidEasyEnemyPlaceables[UnityEngine.Random.Range(0, targetIdea.ValidEasyEnemyPlaceables.Length)], 0.2f);
                    if (eCrateInstance) { spawnedEnemyCrate = eCrateInstance.GetComponent<EmergencyCrateController>(); }
                    // spawnedEnemyCrate = EnemyAirDrop(m_room, sprite.WorldCenter, targetIdea.ValidEasyEnemyPlaceables[UnityEngine.Random.Range(0, targetIdea.ValidEasyEnemyPlaceables.Length)]);
                } else {
                    // spawnedEnemyCrate = EnemyAirDrop(m_room, sprite.WorldCenter, EnemySpawnPlacableOverride);
                    GameObject eCrateInstance = ExpandUtility.SpawnAirDrop(m_room, sprite.WorldCenter, null, EnemySpawnPlacableOverride, 0.2f);
                    if (eCrateInstance) { spawnedEnemyCrate = eCrateInstance.GetComponent<EmergencyCrateController>(); }
                }                
                if (!m_room.IsSealed && spawnedEnemyCrate) {
                    m_room.npcSealState = RoomHandler.NPCSealState.SealAll;
                    m_room.SealRoom();
                }
                yield return new WaitForSeconds(2.25f);
                DestroyMushroom(false);
                if (spawnedEnemyCrate) {
                    while (ReflectionHelpers.ReflectGetField<bool?>(typeof(EmergencyCrateController), "m_hasBeenTriggered", spawnedEnemyCrate).HasValue && ReflectionHelpers.ReflectGetField<bool?>(typeof(EmergencyCrateController), "m_hasBeenTriggered", spawnedEnemyCrate).Value) {
                        if (!spawnedEnemyCrate) { break; }
                        yield return null;
                    }
                }
                yield return new WaitForSeconds(1f);
                m_room.npcSealState = RoomHandler.NPCSealState.SealNone;
                if (spriteAnimator.IsPlaying(BreakAnimation)) {
                    while (spriteAnimator.IsPlaying(BreakAnimation)) { yield return null; }
                }
                Destroy(gameObject);
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

        private void DestroyMushroom(bool DestroyAfterAnimation = true) {
            if (DestroyVFX) {
                SpawnManager.SpawnVFX(DestroyVFX, specRigidbody.UnitCenter, Quaternion.identity);
            } else {
                LootEngine.DoDefaultItemPoof(specRigidbody.UnitCenter, false, false);
            }
            if (!string.IsNullOrEmpty(BreakAnimation)) {
                if (DestroyAfterAnimation) {
                    spriteAnimator.PlayAndDestroyObject(BreakAnimation, null);
                } else {
                    spriteAnimator.PlayAndDisableRenderer(BreakAnimation);
                }
            } else {
                if (DestroyAfterAnimation) { Destroy(gameObject); }
            }
        }

        /*private EmergencyCrateController EnemyAirDrop(RoomHandler currentRoom, Vector3 landingPosition, DungeonPlaceable EnemyPlacable) {
            EmergencyCrateController lootCrate = Instantiate(BraveResources.Load<GameObject>("EmergencyCrate")).GetComponent<EmergencyCrateController>();
            if (lootCrate == null) { return null; }

            lootCrate.ChanceToExplode = 0.2f;
            lootCrate.ChanceToSpawnEnemy = 1;
            lootCrate.EnemyPlaceable = EnemyPlacable;
            
            lootCrate.Trigger(new Vector3(-5f, -5f, -5f), (landingPosition + new Vector3(15f, 15f, 15f)), currentRoom, true);
            currentRoom.ExtantEmergencyCrate = lootCrate.gameObject;
            return lootCrate;
        }*/

        public void ConfigureOnPlacement(RoomHandler room) { m_room = room; /*enabled = true;*/ }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

