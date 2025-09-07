using Dungeonator;
using System;
using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandEntitySpawner : BraveBehaviour {
        
        public ExpandEntitySpawner() {
            AllowForceSpawn = true;
            ForceSpawnDelay = 360;
            DelayBetweenForceSpawnChance = 40;
            ForceSpawnChanceAfterDelayOdds = 0.35f;
            SpawnOffset = new Vector2(0.5f, 0.5f);
            SpawnClearance = new IntVector2(1, 2);

            SpawnState = EntitySpawnState.Inactive;
            m_SpawnTimer = ForceSpawnDelay;
            m_SpawnChanceTimer = DelayBetweenForceSpawnChance;
        }

        [SerializeField]
        public string EntityGUID;
        
        [SerializeField]
        public bool AllowForceSpawn;

        [SerializeField]
        public float ForceSpawnDelay;

        [SerializeField]
        public float ForceSpawnChanceAfterDelayOdds;

        [SerializeField]
        public float DelayBetweenForceSpawnChance;

        [SerializeField]
        public Vector2 SpawnOffset;

        [SerializeField]
        public IntVector2 SpawnClearance;

        [NonSerialized]
        public EntitySpawnState SpawnState;

        public enum EntitySpawnState { Inactive, WaitingToSpawn, PreSpawn, DoCleanup };
        
        [NonSerialized]
        public RoomHandler ParentRoom;
        
        [NonSerialized]
        private Vector2? m_SpawnPositionOverride;

        [NonSerialized]
        private float m_SpawnTimer;

        [NonSerialized]
        private float m_SpawnChanceTimer;

        public void Start() {
            if (!EnemyDatabase.GetOrLoadByGuid(EntityGUID)) {
                SpawnState = EntitySpawnState.DoCleanup;
                return;
            }
        }
        
        public void Update() {
            if (!GameManager.HasInstance | GameManager.Instance.IsLoadingLevel | !GameManager.Instance.Dungeon |
                Dungeon.IsGenerating
                ) {
                return;
            }
            switch (SpawnState) {
                default:
                    return;
                case EntitySpawnState.Inactive:
                    return;
                case EntitySpawnState.WaitingToSpawn:
                    if (string.IsNullOrEmpty(EntityGUID)) {
                        SpawnState = EntitySpawnState.DoCleanup;
                        return;
                    }
                    if (ParentRoom == null) ParentRoom = gameObject.transform.position.GetAbsoluteRoom();
                    if (ParentRoom == null) {
                        SpawnState = EntitySpawnState.DoCleanup;
                        return;
                    }
                    if (GameManager.Instance.AllPlayers != null && GameManager.Instance.AllPlayers.Length > 0) {
                        for (int p = 0; p < GameManager.Instance.AllPlayers.Length; p++) {
                            if (GameManager.Instance.AllPlayers[p] && GameManager.Instance.AllPlayers[p].CurrentRoom == ParentRoom) {
                                m_SpawnPositionOverride = null;
                                SpawnState = EntitySpawnState.PreSpawn;
                                return;
                            }
                        }
                    }
                    if (AllowForceSpawn) {
                        if (m_SpawnTimer <= 0 && m_SpawnChanceTimer <= 0) {
                            if (UnityEngine.Random.value < ForceSpawnChanceAfterDelayOdds) {
                                if (GameManager.Instance.PrimaryPlayer && GameManager.Instance.PrimaryPlayer.CurrentRoom != null) {
                                    if (GameManager.Instance.PrimaryPlayer.CurrentRoom.connectedRooms != null && GameManager.Instance.PrimaryPlayer.CurrentRoom.connectedRooms.Count > 0) {
                                        RoomHandler m_ParentRoom = BraveUtility.RandomElement(GameManager.Instance.PrimaryPlayer.CurrentRoom.connectedRooms);
                                        if (m_ParentRoom != null) {
                                            ParentRoom = m_ParentRoom;
                                            m_SpawnPositionOverride = (SpawnOffset + FindSafeSpawnInRoom(ParentRoom, SpawnClearance));
                                            SpawnState = EntitySpawnState.PreSpawn;
                                            return;
                                        }
                                    }
                                }
                            }
                            m_SpawnChanceTimer = DelayBetweenForceSpawnChance;
                        }
                        if (m_SpawnTimer > 0) {
                            m_SpawnTimer -= BraveTime.DeltaTime;
                        } else if (m_SpawnChanceTimer > 0) {
                            m_SpawnChanceTimer -= BraveTime.DeltaTime;
                        }
                    }
                    return;
                case EntitySpawnState.PreSpawn:
                    SpawnState = EntitySpawnState.Inactive;
                    StartCoroutine(DoSpawn(m_SpawnPositionOverride));
                    return;
                case EntitySpawnState.DoCleanup:
                    SpawnState = EntitySpawnState.Inactive;
                    Destroy(this);
                    return;
            }
        }
        

        private IEnumerator DoSpawn(Vector2? SpawnPosition) {
            yield return null;
            float delay = 3f;
            float timer = 0f;
            while (timer < delay) {
                yield return null;
                timer += BraveTime.DeltaTime;
            }
            SpawnEntity(SpawnPosition);
            yield break;
        }

        private void SpawnEntity(Vector2? spawnPosition) {
            Vector2? m_TargetPosition = spawnPosition;
            if (!m_TargetPosition.HasValue)m_TargetPosition = transform.position;
            AIActor Entity = AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(EntityGUID), m_TargetPosition.Value, ParentRoom, true, AIActor.AwakenAnimationType.Spawn, true);
            if (Entity) {
                ParentRoom.DeregisterEnemy(Entity);
                ExpandEntityManager m_EntityManager = Entity.gameObject.GetComponent<ExpandEntityManager>();
                if (m_EntityManager) {
                    if (!string.IsNullOrEmpty(GameManager.Instance.Dungeon.gameObject.name) && GameManager.Instance.Dungeon.gameObject.name.ToLower().StartsWith("base_backrooms")) {
                        m_EntityManager.IsOnBackRoomsFloor = true;
                    }
                    m_EntityManager.Configured = true;
                    if (Entity.gameObject.transform.parent) Entity.gameObject.transform.SetParent(GameManager.Instance.Dungeon.gameObject.transform);
                }
            }
            SpawnState = EntitySpawnState.DoCleanup;
        }

        private Vector2 FindSafeSpawnInRoom(RoomHandler targetRoom, IntVector2 clearance) {
            Vector2 m_result;
            IntVector2? randomAvailableCell = targetRoom.GetRandomAvailableCell(new IntVector2?(clearance), new CellTypes?(CellTypes.FLOOR), false, null);
            m_result = ((randomAvailableCell == null) ? targetRoom.GetCenterCell().ToVector2() : randomAvailableCell.Value.ToVector2());
            return m_result;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

