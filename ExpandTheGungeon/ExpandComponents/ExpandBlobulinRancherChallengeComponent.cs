using UnityEngine;
using Dungeonator;


namespace ExpandTheGungeon.ExpandComponents {

    class ExpandBlobulinRancherChallengeComponent : ChallengeModifier {

        public ExpandBlobulinRancherChallengeComponent() {
            SpawnTarget2Guid = "b8103805af174924b578c98e95313074"; // poisbulin
            PoisonBlobSpawnChance = 0.3f;
        }

        [EnemyIdentifier]
        public string SpawnTargetGuid;
        public string SpawnTarget2Guid;

        public float CooldownBetweenSpawns;
        public float SafeRadius;
        public float PoisonBlobSpawnChance;

        private float m_cooldown;

        private void Start() {
            foreach (PlayerController player in GameManager.Instance.AllPlayers) { player.PostProcessProjectile += ModifyProjectile; }
        }

        private void ModifyProjectile(Projectile proj, float somethin) {
            if (proj && proj.Owner is PlayerController && !proj.SpawnedFromNonChallengeItem && !proj.TreatedAsNonProjectileForChallenge && !(proj is InstantDamageOneEnemyProjectile) && !(proj is InstantlyDamageAllProjectile)) {
                proj.OnDestruction += HandleProjectileDeath;
            }

        }

        private bool CellIsValid(IntVector2 cellPos) {

            if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(cellPos)) {
                CellData cellData = GameManager.Instance.Dungeon.data[cellPos];
                return (cellData == null || cellData.parentRoom == null || cellData.parentRoom.GetActiveEnemiesCount(RoomHandler.ActiveEnemyType.RoomClear) != 0) && (cellData != null && cellData.type == CellType.FLOOR && cellData.IsPassable && cellData.parentRoom == GameManager.Instance.BestActivePlayer.CurrentRoom && !cellData.isExitCell);
            }

            return false;
        }

        private void Update() { m_cooldown -= BraveTime.DeltaTime; }

        private void HandleProjectileDeath(Projectile obj) {
            if (!this) { return; }
            if (obj && !obj.HasImpactedEnemy && !obj.HasDiedInAir) {
                float num = 0f;
                GameManager.Instance.GetPlayerClosestToPoint(obj.specRigidbody.UnitCenter, out num);
                if (num < SafeRadius) { return; }
                IntVector2 intVector = obj.specRigidbody.UnitCenter.ToIntVector2(VectorConversions.Round);
                if (GameManager.Instance.Dungeon.data.isFaceWallHigher(intVector.x, intVector.y)) {
                    intVector += new IntVector2(0, -2);
                } else if (GameManager.Instance.Dungeon.data.isFaceWallLower(intVector.x, intVector.y)) {
                    intVector += new IntVector2(0, -1);
                }
                bool flag = CellIsValid(intVector);
                if (!flag) {
                    for (int i = -1; i < 2; i++) {
                        for (int j = -1; j < 2; j++) {
                            IntVector2 intVector2 = intVector + new IntVector2(i, j);
                            flag = CellIsValid(intVector2);
                            if (flag) { intVector = intVector2; break; }
                        }
                        if (flag) { break; }
                    }
                    if (!flag) {
                        IntVector2? nearestAvailableCell = GameManager.Instance.PrimaryPlayer.CurrentRoom.GetNearestAvailableCell(obj.specRigidbody.UnitCenter, new IntVector2?(IntVector2.One), new CellTypes?(CellTypes.FLOOR), false, null);
                        if (nearestAvailableCell != null) {
                            flag = true;
                            intVector = nearestAvailableCell.Value;
                        }
                    }
                }
                if (obj.Owner is PlayerController) {
                    if (!(obj.Owner as PlayerController).IsInCombat) { flag = false; }
                } else {
                    flag = false;
                }
                if (flag && m_cooldown <= 0f) {
                    m_cooldown = CooldownBetweenSpawns;
                    if (Random.value <= PoisonBlobSpawnChance) {
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(SpawnTarget2Guid);
                        AIActor.Spawn(orLoadByGuid, intVector, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector), true, AIActor.AwakenAnimationType.Default, true);
                    } else {
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(SpawnTargetGuid);
                        AIActor.Spawn(orLoadByGuid, intVector, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector), true, AIActor.AwakenAnimationType.Default, true);
                    }
                }
            }
        }

        private void OnDestroy() {
            foreach (PlayerController player in GameManager.Instance.AllPlayers) { player.PostProcessProjectile -= ModifyProjectile; }
        }

    }
}

