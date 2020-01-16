using System.Collections.Generic;
using Dungeonator;
using Pathfinding;
using UnityEngine;

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
                "01972dee89fc4404a5c408d50007dad5" // bullet_kin
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
        
        private float m_SpawnTimer;

        private GameObject m_LootCratePrefab;        

        private void Start() {

            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER | GameManager.Instance.IsLoadingLevel | Dungeon.IsGenerating) { return; }

            m_CurrentRoom = GameManager.Instance.BestActivePlayer.CurrentRoom;
            m_SpawnTimer = Random.Range(MinTimeBetweenSpawns, MaxTimeBetweenSpawns);

            if (m_CurrentRoom == null) { return; }
            if (m_CurrentRoom.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS) { return; }
            if (ToadieGUIDs == null | ToadieGUIDs.Count <= 0) { return; }

            AssetBundle assetBundle = ResourceManager.LoadAssetBundle("brave_resources_001");
            m_LootCratePrefab = assetBundle.LoadAsset<GameObject>("EmergencyCrate");
            assetBundle = null;

            for (int i = 0; i < MaxToadiesPerWave; i++) { RandomToadieAirDrop(m_LootCratePrefab, m_CurrentRoom); }
        }

        private void Update() {
            if (m_CurrentRoom == null) { return; }
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
                    for (int i = 0; i < ToadiesToSpawn; i++) { RandomToadieAirDrop(m_LootCratePrefab, m_CurrentRoom); }
                }
            }
        }

        private void RandomToadieAirDrop(GameObject lootCratePrefab, RoomHandler currentRoom, IntVector2? Clearence = null) {

            if (!Clearence.HasValue) { Clearence = new IntVector2(2, 2); }

            IntVector2? DropLocation = FindRandomDropLocation(currentRoom, Clearence.Value);

            if (DropLocation.HasValue) {
                EmergencyCrateController lootCrate = Instantiate(lootCratePrefab, DropLocation.Value.ToVector2().ToVector3ZUp(1f), Quaternion.identity).GetComponent<EmergencyCrateController>();
                if (lootCrate == null) { return; }

                lootCrate.ChanceToExplode = 0;
                lootCrate.ChanceToSpawnEnemy = 1;
                lootCrate.EnemyPlaceable = m_CustomEnemyPlacable(BraveUtility.RandomElement(ToadieGUIDs));
                
                lootCrate.Trigger(new Vector3(-5f, -5f, -5f), DropLocation.Value.ToVector3() + new Vector3(15f, 15f, 15f), currentRoom, true);
                currentRoom.ExtantEmergencyCrate = lootCrate.gameObject;
            }
            return;
        }

        private DungeonPlaceable m_CustomEnemyPlacable(string EnemyGUID = "b5e699a0abb94666bda567ab23bd91c4", bool forceBlackPhantom = false) {
            DungeonPlaceableVariant EnemyVariant = new DungeonPlaceableVariant();
            EnemyVariant.percentChance = 1f;
            EnemyVariant.unitOffset = Vector2.zero;
            EnemyVariant.enemyPlaceableGuid = EnemyGUID;
            EnemyVariant.pickupObjectPlaceableId = -1;
            EnemyVariant.forceBlackPhantom = forceBlackPhantom;
            EnemyVariant.addDebrisObject = false;
            EnemyVariant.prerequisites = null;
            EnemyVariant.materialRequirements = null;

            List<DungeonPlaceableVariant> EnemyTiers = new List<DungeonPlaceableVariant>();
            EnemyTiers.Add(EnemyVariant);

            DungeonPlaceable m_cachedPlacable = ScriptableObject.CreateInstance<DungeonPlaceable>();
            m_cachedPlacable.name = "CustomEnemyPlacable";
            m_cachedPlacable.width = 1;
            m_cachedPlacable.height = 1;
            m_cachedPlacable.roomSequential = false;
            m_cachedPlacable.respectsEncounterableDifferentiator = false;
            m_cachedPlacable.UsePrefabTransformOffset = false;
            m_cachedPlacable.MarkSpawnedItemsAsRatIgnored = false;
            m_cachedPlacable.DebugThisPlaceable = false;
            m_cachedPlacable.variantTiers = EnemyTiers;

            return m_cachedPlacable;
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
        
        /*private IntVector2? FindRandomDropLocation(RoomHandler currentRoom, bool allowPlacingOverPits = false, int gridSnap = 1) {
            Dungeon dungeon = GameManager.Instance.Dungeon;
            List<IntVector2> validCellsCached = new List<IntVector2>();
            for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                for (int height = -1; height <= currentRoom.area.dimensions.y; height++) {
                    int X = currentRoom.area.basePosition.x + Width;
                    int Y = currentRoom.area.basePosition.y + height;
                    if (X % gridSnap == 0 && Y % gridSnap == 0) {
                        if (allowPlacingOverPits) {
                            if (!dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) &&
                                !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) &&
                                !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) &&
                                !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) &&
                                !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) &&
                                !dungeon.data[X - 2, Y + 2].isOccupied && !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied && !dungeon.data[X + 2, Y + 2].isOccupied &&
                                !dungeon.data[X - 2, Y + 1].isOccupied && !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied && !dungeon.data[X + 2, Y + 1].isOccupied &&
                                !dungeon.data[X - 2, Y].isOccupied && !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied && !dungeon.data[X + 2, Y].isOccupied &&
                                !dungeon.data[X - 2, Y - 1].isOccupied && !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied && !dungeon.data[X + 2, Y - 1].isOccupied &&
                                !dungeon.data[X - 2, Y - 2].isOccupied && !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied && !dungeon.data[X + 2, Y - 2].isOccupied)
                            {
                                validCellsCached.Add(new IntVector2(X, Y));
                            }
                        } else {
                            if (!dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) &&
                                !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) &&
                                !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) &&
                                !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) &&
                                !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) &&
                                !dungeon.data[X - 2, Y + 2].isOccupied && !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied && !dungeon.data[X + 2, Y + 2].isOccupied &&
                                !dungeon.data[X - 2, Y + 1].isOccupied && !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied && !dungeon.data[X + 2, Y + 1].isOccupied &&
                                !dungeon.data[X - 2, Y].isOccupied && !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied && !dungeon.data[X + 2, Y].isOccupied &&
                                !dungeon.data[X - 2, Y - 1].isOccupied && !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied && !dungeon.data[X + 2, Y - 1].isOccupied &&
                                !dungeon.data[X - 2, Y - 2].isOccupied && !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied && !dungeon.data[X + 2, Y - 2].isOccupied &&
                                !dungeon.data.isPit(X - 2, Y + 2) && !dungeon.data.isPit(X - 1, Y + 2) && !dungeon.data.isPit(X, Y + 2) && !dungeon.data.isPit(X + 1, Y + 2) && !dungeon.data.isPit(X + 2, Y + 2) &&
                                !dungeon.data.isPit(X - 2, Y + 1) && !dungeon.data.isPit(X - 1, Y + 1) && !dungeon.data.isPit(X, Y + 1) && !dungeon.data.isPit(X + 1, Y + 1) && !dungeon.data.isPit(X + 2, Y + 1) &&
                                !dungeon.data.isPit(X - 2, Y) && !dungeon.data.isPit(X - 1, Y) && !dungeon.data.isPit(X, Y) && !dungeon.data.isPit(X + 1, Y) && !dungeon.data.isPit(X + 2, Y) &&
                                !dungeon.data.isPit(X - 2, Y - 1) && !dungeon.data.isPit(X - 1, Y - 1) && !dungeon.data.isPit(X, Y - 1) && !dungeon.data.isPit(X + 1, Y - 1) && !dungeon.data.isPit(X + 2, Y - 1) &&
                                !dungeon.data.isPit(X - 2, Y - 2) && !dungeon.data.isPit(X - 1, Y - 2) && !dungeon.data.isPit(X, Y - 2) && !dungeon.data.isPit(X + 1, Y - 2) && !dungeon.data.isPit(X + 2, Y - 2))
                            {
                                validCellsCached.Add(new IntVector2(X, Y));
                            }
                        }
                    }
                }
            }
            if (validCellsCached.Count > 0) {
                validCellsCached = validCellsCached.Shuffle();
                IntVector2 SelectedCell = BraveUtility.RandomElement(validCellsCached);
                IntVector2 RegisteredCell = SelectedCell;
                validCellsCached.Clear();
                return (SelectedCell - currentRoom.area.basePosition);
            } else {
                return null;
            }
        }*/
        

        // private void LateUpdate() { }
        private void OnDestroy() { Destroy(m_LootCratePrefab); }
    }
}

