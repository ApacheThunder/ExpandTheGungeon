using System.Collections.Generic;
using Dungeonator;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents {

    class ExpandTripleTroubleChallengeComponent : ChallengeModifier {

        public ExpandTripleTroubleChallengeComponent() {
            DisplayName = "Triple Trouble!";
            AlternateLanguageDisplayName = string.Empty;
            AtlasSpriteName = "Big_Boss_icon_001";
            ValidInBossChambers = true;
            MutuallyExclusive = new List<ChallengeModifier>(0);

            ReplaceReinforcementWaves = true;
            AlternateEnemySpawnOdds = 0.5f;
        }

        public bool ReplaceReinforcementWaves;
        public float AlternateEnemySpawnOdds;

        private void Start() {

            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER | GameManager.Instance.IsLoadingLevel | Dungeon.IsGenerating) { return; }            

            RoomHandler currentRoom = GameManager.Instance.BestActivePlayer.CurrentRoom;
            if (currentRoom == null) { return; }
            if (currentRoom.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS) { return; }

            List<string> TriggerTwins = new List<string>() { "ea40fcc863d34b0088f490f4e57f8913", "c00390483f394a849c36143eb878998f" };
            AIActor triggertwin = AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(BraveUtility.RandomElement(TriggerTwins)), (new IntVector2(18, 19) + currentRoom.area.basePosition), currentRoom, true, AIActor.AwakenAnimationType.Default, true);
            triggertwin.HandleReinforcementFallIntoRoom(1);
            
            if (!ReplaceReinforcementWaves) { return; }

            // Trigger Twins uses the reinforcement waves of the room to trigger the bullet kin spawns.
            List<PrototypeRoomObjectLayer> m_remainingReinforcementLayers = ReflectionHelpers.ReflectGetField<List<PrototypeRoomObjectLayer>>(typeof(RoomHandler), "remainingReinforcementLayers", currentRoom);
            
            if (m_remainingReinforcementLayers != null) {
                string ReinforcementEnemySpawn1 = "70216cae6c1346309d86d4a0b4603045"; // veteran_bullet_kin
                string ReinforcementEnemySpawn2 = "70216cae6c1346309d86d4a0b4603045"; // veteran_bullet_kin

                if (UnityEngine.Random.value <= AlternateEnemySpawnOdds) {
                    ReinforcementEnemySpawn1 = "05cb719e0178478685dc610f8b3e8bfc"; // bullet_kin_vest
                    ReinforcementEnemySpawn2 = "5861e5a077244905a8c25c2b7b4d6ebb"; // bullet_kin_cowboy
                }

                if (m_remainingReinforcementLayers[0] != null) {
                    if (m_remainingReinforcementLayers[0].placedObjects[0] != null) {
                        m_remainingReinforcementLayers[0].placedObjects[0].enemyBehaviourGuid = ReinforcementEnemySpawn1;
                    }
                    if (m_remainingReinforcementLayers[0].placedObjects[1] != null) {
                        m_remainingReinforcementLayers[0].placedObjects[1].enemyBehaviourGuid = ReinforcementEnemySpawn2;
                    }
                    if (m_remainingReinforcementLayers[0].placedObjects[2] != null) {
                        m_remainingReinforcementLayers[0].placedObjects[2].enemyBehaviourGuid = ReinforcementEnemySpawn1;
                    }
                    if (m_remainingReinforcementLayers[0].placedObjects[3] != null) {
                        m_remainingReinforcementLayers[0].placedObjects[3].enemyBehaviourGuid = ReinforcementEnemySpawn2;
                    }
                }
                if (m_remainingReinforcementLayers[1] != null) {
                    if (m_remainingReinforcementLayers[1].placedObjects[0] != null) {
                        m_remainingReinforcementLayers[1].placedObjects[0].enemyBehaviourGuid = ReinforcementEnemySpawn2;
                    }
                    if (m_remainingReinforcementLayers[1].placedObjects[1] != null) {
                        m_remainingReinforcementLayers[1].placedObjects[1].enemyBehaviourGuid = ReinforcementEnemySpawn1;
                    }
                    if (m_remainingReinforcementLayers[1].placedObjects[2] != null) {
                        m_remainingReinforcementLayers[1].placedObjects[2].enemyBehaviourGuid = ReinforcementEnemySpawn2;
                    }
                    if (m_remainingReinforcementLayers[1].placedObjects[3] != null) {
                        m_remainingReinforcementLayers[1].placedObjects[3].enemyBehaviourGuid = ReinforcementEnemySpawn1;
                    }
                }
            }
        }

        private void Update() { }
        private void LateUpdate() { }
        private void OnDestroy() { }
    }
}

