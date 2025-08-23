using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandEntitySpawner : BraveBehaviour {
        
        public ExpandEntitySpawner() { IsReady = false; }

        public bool IsReady;

        public RoomHandler ParentRoom;

        private bool m_Triggered;
        
        public void Awake() { }

        public void Start() { }

        private void SpawnEntity() {
            AIActor Entity = AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(ExpandEnemyDatabase.EntityGUID), transform.position.XY().ToIntVector2(), ParentRoom, true, AIActor.AwakenAnimationType.Spawn, true);
            ExpandEntityManager m_EntityManager = Entity.gameObject.GetComponent<ExpandEntityManager>();
            if (Entity && m_EntityManager) {
                ParentRoom.DeregisterEnemy(Entity);
                if (!string.IsNullOrEmpty(GameManager.Instance.Dungeon.gameObject.name) && GameManager.Instance.Dungeon.gameObject.name.ToLower().StartsWith("base_backrooms")) {
                    m_EntityManager.IsOnBackRoomsFloor = true;
                }
                m_EntityManager.Configured = true;
                if (Entity.gameObject.transform.parent) {
                    Entity.gameObject.transform.SetParent(GameManager.Instance.Dungeon.gameObject.transform);
                }
            }
        }

        private IEnumerator DoSpawn() {
            yield return null;
            float delay = 3f;
            float timer = 0f;
            while (timer < delay) {
                yield return null;
                timer += BraveTime.DeltaTime;
            }
            SpawnEntity();
            yield break;
        }

        public void Update() {
            if (!IsReady) return;
            if (m_Triggered) return;
            if (ParentRoom == null) ParentRoom = gameObject.transform.position.GetAbsoluteRoom();
        
            bool PlayerInRoom = false;
            
            foreach (PlayerController playerController in GameManager.Instance.AllPlayers) {
                if (playerController.CurrentRoom == ParentRoom) { PlayerInRoom = true; }
            }
            
            if (PlayerInRoom) {
                StartCoroutine(DoSpawn());
                m_Triggered = true;
            }
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

