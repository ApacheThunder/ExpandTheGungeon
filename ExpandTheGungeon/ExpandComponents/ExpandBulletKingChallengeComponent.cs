using System.Collections.Generic;
using Dungeonator;
using Pathfinding;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandComponents {

    class ExpandBulletKingChallenge : ChallengeModifier {

        public ExpandBulletKingChallenge() {
            DisplayName = "All the King's Men";
            AlternateLanguageDisplayName = string.Empty;
            AtlasSpriteName = "Big_Boss_icon_001";
            ValidInBossChambers = true;
            MutuallyExclusive = new List<ChallengeModifier>(0);

            ToadieGUIDs = new List<string>() {
                "b5e699a0abb94666bda567ab23bd91c4", // bullet_kings_toadie
                "d4dd2b2bbda64cc9bcec534b4e920518", // bullet_kings_toadie_revenge
                "02a14dec58ab45fb8aacde7aacd25b01", // old_kings_toadie
                "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                ExpandCustomEnemyDatabase.ClownkinAngryGUID
            };

            MinTimeBetweenSpawns = 10f;
            MaxTimeBetweenSpawns = 20f;
            MaxActiveToadies = 16;
            MaxToadiesPerWave = 6;
        }
        
        public List<string> ToadieGUIDs;

        public float MinTimeBetweenSpawns;
        public float MaxTimeBetweenSpawns;
        public int MaxActiveToadies;
        public int MaxToadiesPerWave;

        private RoomHandler m_CurrentRoom;

        private AIActor m_Boss;
        
        private float m_SpawnTimer;

        private void Start() {

            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER | GameManager.Instance.IsLoadingLevel | Dungeon.IsGenerating) { return; }

            m_CurrentRoom = GameManager.Instance.BestActivePlayer.CurrentRoom;
            m_SpawnTimer = Random.Range(MinTimeBetweenSpawns, MaxTimeBetweenSpawns);

            if (m_CurrentRoom == null) { return; }
            if (m_CurrentRoom.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS) { return; }
            if (ToadieGUIDs == null | ToadieGUIDs.Count <= 0) { return; }

            foreach (AIActor enemy in m_CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                if (enemy.healthHaver.IsBoss) {
                    m_Boss = enemy;
                    break;
                }
            }

            for (int i = 0; i < MaxToadiesPerWave; i++) { RandomToadieAirDrop(BraveUtility.RandomElement(ToadieGUIDs), m_CurrentRoom); }
        }

        private void Update() {
            if (m_CurrentRoom == null | !m_CurrentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { return; }
            if (!m_Boss | m_Boss.healthHaver.IsDead) { return; }

            m_SpawnTimer -= BraveTime.DeltaTime;
            if (m_SpawnTimer <= 0) {
                m_SpawnTimer = Random.Range(MinTimeBetweenSpawns, MaxTimeBetweenSpawns);
                int ActiveToadies = 0;
                List<AIActor> enemies = m_CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                if (enemies != null && enemies.Count > 0) {
                    foreach (AIActor enemy in enemies) {
                        if (!string.IsNullOrEmpty(enemy.EnemyGuid) && ToadieGUIDs.Contains(enemy.EnemyGuid)) { ActiveToadies++; }
                    }
                }
                
                if (ActiveToadies >= MaxActiveToadies) {
                    return;
                } else {
                    int ToadiesToSpawn = Random.Range(4, MaxToadiesPerWave);                    
                    for (int i = 0; i < ToadiesToSpawn; i++) { RandomToadieAirDrop(BraveUtility.RandomElement(ToadieGUIDs), m_CurrentRoom); }
                }
            }
        }

        private void RandomToadieAirDrop(string EnemyGUID, RoomHandler currentRoom, IntVector2? Clearence = null) {
            if (!Clearence.HasValue) { Clearence = new IntVector2(2, 2); }
            IntVector2? DropLocation = ExpandUtility.GetRandomAvailableCellSmart(currentRoom, Clearence.Value, false);
            if (DropLocation.HasValue) {
                bool isToadie = false;
                if (EnemyGUID.ToLower() != ExpandCustomEnemyDatabase.ClownkinAngryGUID && EnemyGUID.ToLower() != "01972dee89fc4404a5c408d50007dad5") {
                    isToadie = true;
                }
                ExpandUtility.SpawnEnemyParaDrop(currentRoom, DropLocation.Value.ToVector3(), EnemyGUID, IsToadie: isToadie);
            }
            return;
        }

        private IntVector2? FindRandomDropLocation(RoomHandler currentRoom, IntVector2 Clearance, float maxDistanceFromPlayer = 20f, bool markLocationAsOccupied = false) {
            CellValidator cellValidator = delegate (IntVector2 pos) {
                for (int j = 0; j < GameManager.Instance.AllPlayers.Length; j++) {
                    if (Vector2.Distance(GameManager.Instance.AllPlayers[j].CenterPosition, pos.ToCenterVector2()) < maxDistanceFromPlayer) { return false; }
                }
                return true;
            };

            IntVector2? randomAvailableCell = currentRoom.GetRandomAvailableCell(new IntVector2?(Clearance), new CellTypes?(CellTypes.FLOOR), false, cellValidator);

            if (randomAvailableCell.HasValue) {
                CellData cellData = GameManager.Instance.Dungeon.data[randomAvailableCell.Value];
                if (cellData.parentRoom == currentRoom && cellData.type == CellType.FLOOR && !cellData.isOccupied && !cellData.containsTrap && !cellData.isOccludedByTopWall) {
                    if (markLocationAsOccupied) { cellData.isOccupied = true; }
                    return randomAvailableCell;
                } else {
                    return null;
                }
            } else {
                return null;
            }
        }

        private void OnDestroy() { }
    }
}

