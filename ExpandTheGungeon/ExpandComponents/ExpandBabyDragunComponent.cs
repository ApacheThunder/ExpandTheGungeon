using System.Collections;
using Dungeonator;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandBabyDragunComponent : BraveBehaviour {

        public ExpandBabyDragunComponent() { 
			
            CagedBabyDragun = gameObject.GetComponent<BabyDragunJailController>().CagedBabyDragun;
            SellRegionRigidbody = gameObject.GetComponent<BabyDragunJailController>().SellRegionRigidbody;

            RequiredEnemies = 6;
            ItemID = 735;

            m_currentlyEatingEnemy = false;
            m_currentlyEatingNPC = false;

            m_enemiesEaten = 0;
        }

        public int RequiredEnemies;

        [PickupIdentifier]
        public int ItemID;

        private tk2dSprite CagedBabyDragun;
        private SpeculativeRigidbody SellRegionRigidbody;
        private RoomHandler m_room;
        private int m_enemiesEaten;
        private bool m_isOpen;
        private bool m_currentlyEatingEnemy;
        private bool m_currentlyEatingNPC;

        private void Start() {            
            m_room = transform.position.GetAbsoluteRoom();
            m_isOpen = true;
        }

        private void Update() {
            if (Dungeon.IsGenerating | !m_isOpen) { return; }
            bool PlayerEnteredRoom = false;
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) { if (GameManager.Instance.AllPlayers[i].CurrentRoom == m_room) { PlayerEnteredRoom = true; break; } }
            if (PlayerEnteredRoom && m_enemiesEaten < RequiredEnemies) {
                if (!m_currentlyEatingEnemy && !m_currentlyEatingNPC) {
                    for (int k = 0; k < StaticReferenceManager.AllNpcs.Count; k++) {
                        TalkDoerLite talkDoerLite = StaticReferenceManager.AllNpcs[k];
                        if (talkDoerLite && !talkDoerLite.name.Contains("ResourcefulRat_Beaten")) {
                            float magnitude = (talkDoerLite.specRigidbody.UnitCenter - CagedBabyDragun.WorldCenter).magnitude;
                            if (magnitude < 3f) {
                                RoomHandler.unassignedInteractableObjects.Remove(talkDoerLite);
                                StartCoroutine(EatNPC(talkDoerLite));
                            }
                        }
                    }
                    for (int i = 0; i < StaticReferenceManager.AllEnemies.Count; i++) {
                        AIActor targetEnemy = StaticReferenceManager.AllEnemies[i];
                        if (targetEnemy && !targetEnemy.healthHaver.IsBoss && !targetEnemy.IgnoreForRoomClear) {
                            float magnitude = (targetEnemy.specRigidbody.UnitCenter - CagedBabyDragun.WorldCenter).magnitude;
                            if (magnitude < 3f) { StartCoroutine(EatEnemy(targetEnemy)); }
                        }
                    }
                }
            }            
        }

        private IEnumerator EatNPC(TalkDoerLite targetNPC) {
            float elapsed = 0f;
            float duration = 0.5f;
            Vector3 startPos = targetNPC.transform.position;
            Vector3 finalOffset = CagedBabyDragun.WorldCenter - startPos.XY();
            tk2dBaseSprite targetSprite = targetNPC.GetComponentInChildren<tk2dBaseSprite>();
            Destroy(targetNPC);
            Destroy(targetNPC.specRigidbody);
            CagedBabyDragun.spriteAnimator.PlayForDuration("baby_dragun_weak_eat", -1f, "baby_dragun_weak_idle", false);
            AkSoundEngine.PostEvent("Play_NPC_BabyDragun_Munch_01", gameObject);
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                if (!targetSprite || !targetSprite.transform) { m_currentlyEatingNPC = false; yield break; }
                targetSprite.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.01f, 0.01f, 1f), elapsed / duration);
                targetSprite.transform.position = Vector3.Lerp(startPos, startPos + finalOffset, elapsed / duration);
                yield return null;
            }
            if (!targetSprite || !targetSprite.transform) { m_currentlyEatingNPC = false; yield break; }
            Destroy(targetSprite.gameObject);
            yield break;
        }

        private IEnumerator EatEnemy(AIActor targetEnemy) {
            float elapsed = 0f;
            float duration = 0.5f;
            Vector3 startPos = targetEnemy.transform.position;
            Vector3 finalOffset = CagedBabyDragun.WorldCenter - startPos.XY();
            tk2dBaseSprite targetSprite = targetEnemy.GetComponentInChildren<tk2dBaseSprite>();
            targetEnemy.behaviorSpeculator.enabled = false;
            RoomHandler m_ParentRoom = targetEnemy.GetAbsoluteParentRoom();
            if (m_ParentRoom != null) { m_ParentRoom.DeregisterEnemy(targetEnemy); }
            Destroy(targetEnemy);
            Destroy(targetEnemy.specRigidbody);
            CagedBabyDragun.spriteAnimator.PlayForDuration("baby_dragun_weak_eat", -1f, "baby_dragun_weak_idle", false);
            AkSoundEngine.PostEvent("Play_NPC_BabyDragun_Munch_01", gameObject);
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                if (!targetSprite || !targetSprite.transform) { m_currentlyEatingEnemy = false; yield break; }
                targetSprite.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.01f, 0.01f, 1f), elapsed / duration);
                targetSprite.transform.position = Vector3.Lerp(startPos, startPos + finalOffset, elapsed / duration);
                yield return null;
            }
            if (!targetSprite || !targetSprite.transform) { m_currentlyEatingEnemy = false; yield break; }
            Destroy(targetSprite.gameObject);
            m_enemiesEaten++;
            if (m_enemiesEaten >= RequiredEnemies) {
                while (CagedBabyDragun.spriteAnimator.IsPlaying("baby_dragun_weak_eat")) { yield return null; }
                LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(ItemID).gameObject, GameManager.Instance.BestActivePlayer);
                Destroy(gameObject);
            }
            yield break;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

