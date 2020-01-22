﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandDungeonFlows;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandMain;


namespace ExpandTheGungeon {

    public class ExpandObjectMods : MonoBehaviour {

        public static ExpandObjectMods Instance { get { return new ExpandObjectMods(); } }
        
        public void InitSpecialMods() {

            ExpandStats.randomSeed = Random.value;

            if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                List<AGDEnemyReplacementTier> m_cachedReplacementTiers = GameManager.Instance.EnemyReplacementTiers;
                // Removes special enemies added after the secret floor
                for (int i = 0; i < m_cachedReplacementTiers.Count; i++) {
                    if (m_cachedReplacementTiers[i].Name.ToLower().EndsWith("_forge") | m_cachedReplacementTiers[i].Name.ToLower().EndsWith("_hell")) {
                        m_cachedReplacementTiers.Remove(m_cachedReplacementTiers[i]);
                    }
                }
                // Add some of the new FTA enemies to the old secret floors
                ExpandEnemyReplacements.Init(m_cachedReplacementTiers);
            }
            
            /*if (ExpandDungeonFlows.Custom_GlitchChest_Flow.sharedInjectionData.Count > 0) {
                ExpandDungeonFlows.Custom_GlitchChest_Flow.sharedInjectionData.Clear();
            }

            if (ExpandDungeonFlows.Custom_Glitch_Flow.sharedInjectionData.Count > 0) {
                ExpandDungeonFlows.Custom_Glitch_Flow.sharedInjectionData.Clear();
            }

            if (ExpandDungeonFlows.Custom_GlitchChestAlt_Flow.sharedInjectionData.Count > 0) {
                ExpandDungeonFlows.Custom_GlitchChestAlt_Flow.sharedInjectionData.Clear();
            }*/

            if (ExpandTheGungeon.isGlitchFloor && GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.PHOBOSGEON) {
                GameManager.Instance.StartCoroutine(secretglitchfloor_flow.InitCustomObjects(Random.value, BraveUtility.RandomBool(), BraveUtility.RandomBool()));                
            } else {
                if (ETGModMainBehaviour.Instance.gameObject.GetComponent<ExpandRatFloorRainController>() != null) {
                    Destroy(ETGModMainBehaviour.Instance.gameObject.GetComponent<ExpandRatFloorRainController>()); 
                }
            }

            InitObjectMods(GameManager.Instance.Dungeon);

            ExpandDungeonFlow.isGlitchFlow = false;
            ExpandTheGungeon.isGlitchFloor = false;
        }

        private void InitObjectMods(Dungeon dungeon) {

            // Disable victory music for Ser Manuel if not on tutorial floor. (it can cause double music bug if you kill him on other floors)
            if (dungeon.LevelOverrideType != GameManager.LevelOverrideState.TUTORIAL) {
                ExpandPrefabs.SerManuel.healthHaver.forcePreventVictoryMusic = true;
            } else {
                ExpandPrefabs.SerManuel.healthHaver.forcePreventVictoryMusic = false;
            }

            // Assign pitfall destination to entrance on Floor 1 if in Bossrush mode and special entrance room to Miniboss room path is available.
            // Glitch Chest floors now added for this since they now have elevator entrance rooms.
            if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.BOSSRUSH |
                GameManager.Instance.CurrentGameMode == GameManager.GameMode.SUPERBOSSRUSH | 
                dungeon.IsGlitchDungeon)
            {   
                foreach (RoomHandler specificRoom in dungeon.data.rooms) {
                    if (!string.IsNullOrEmpty(specificRoom.GetRoomName())) {                        
                        if (specificRoom.GetRoomName().ToLower().StartsWith("elevatormaintenance") && (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON | dungeon.IsGlitchDungeon)) {
                            if (dungeon.data.Entrance != null && dungeon.data.Entrance.GetRoomName().ToLower().StartsWith("elevator entrance")) {
                                dungeon.data.Entrance.TargetPitfallRoom = specificRoom;
                                dungeon.data.Entrance.ForcePitfallForFliers = true;
                            }
                        }
                    }
                }
            }
            
            if (dungeon.IsGlitchDungeon | ExpandDungeonFlow.isGlitchFlow) {
                foreach (AIActor enemy in FindObjectsOfType<AIActor>()) {
                    if (!enemy.IsBlackPhantom && !enemy.healthHaver.IsBoss && !string.IsNullOrEmpty(enemy.EnemyGuid) && enemy.optionalPalette == null && (string.IsNullOrEmpty(enemy.OverrideDisplayName) | !enemy.OverrideDisplayName.StartsWith("Corrupted"))) {
                        if (!ExpandLists.DontGlitchMeList.Contains(enemy.EnemyGuid)) {
                            if (Random.value <= 0.6f) {
                                ExpandShaders.Instance.BecomeGlitched(enemy, 0.04f, 0.07f, 0.05f, 0.07f, 0.05f);
                                ExpandGlitchedEnemies.GlitchExistingEnemy(enemy);
                            }
                            if (Random.value <= 0.25f && !ExpandLists.blobsAndCritters.Contains(enemy.EnemyGuid) && enemy.GetComponent<ExpandSpawnGlitchObjectOnDeath>() == null) {
                                enemy.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>();
                            }
                        }
                    }
                }
                foreach (BraveBehaviour targetObject in FindObjectsOfType<BraveBehaviour>()) {
                    if (Random.value <= 0.05f && targetObject.gameObject && !targetObject.gameObject.GetComponent<AIActor>() && !targetObject.gameObject.GetComponent<Chest>()) {
                        if (string.IsNullOrEmpty(targetObject.gameObject.name) | (!targetObject.gameObject.name.ToLower().StartsWith("glitchtile") && !targetObject.gameObject.name.ToLower().StartsWith("ex secret door") && !targetObject.gameObject.name.ToLower().StartsWith("lock") && !targetObject.gameObject.name.ToLower().StartsWith("chest"))) {
                            float RandomIntervalFloat = Random.Range(0.02f, 0.04f);
                            float RandomDispFloat = Random.Range(0.06f, 0.08f);
                            float RandomDispIntensityFloat = Random.Range(0.07f, 0.1f);
                            float RandomColorProbFloat = Random.Range(0.035f, 0.1f);
                            float RandomColorIntensityFloat = Random.Range(0.05f, 0.1f);
                            ExpandShaders.Instance.BecomeGlitched(targetObject, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                        }
                    }
                }

                ExpandPlaceGlitchedEnemies m_GlitchEnemyRandomizer = new ExpandPlaceGlitchedEnemies();
                m_GlitchEnemyRandomizer.PlaceRandomEnemies(dungeon, GameManager.Instance.CurrentFloor);
                Destroy(m_GlitchEnemyRandomizer);

                MaybeSetupGlitchEnemyStun(dungeon);
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.PHOBOSGEON) {
                foreach (AIActor enemy in FindObjectsOfType<AIActor>()) {
                    if (!ExpandLists.DontGlitchMeList.Contains(enemy.EnemyGuid) && !enemy.healthHaver.IsBoss) {
                        if (Random.value <= 0.6f) {
                            ExpandShaders.Instance.BecomeGlitched(enemy, 0.04f, 0.07f, 0.05f, 0.07f, 0.05f);
                            ExpandGlitchedEnemies.GlitchExistingEnemy(enemy);
                        }
                    }
                }

                ExpandPlaceGlitchedEnemies m_GlitchEnemyRandomizer = new ExpandPlaceGlitchedEnemies();
                m_GlitchEnemyRandomizer.PlaceRandomEnemies(dungeon, GameManager.Instance.CurrentFloor);
                Destroy(m_GlitchEnemyRandomizer);

                MaybeSetupGlitchEnemyStun(dungeon);
            }

            if (ExpandUtility.RatDungeon != null) { ExpandUtility.RatDungeon = null; }
        }

        private void MaybeSetupGlitchEnemyStun(Dungeon dungeon) {

            if (!dungeon.IsGlitchDungeon && dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.PHOBOSGEON) {
                return;
            }

            List<RoomHandler> RoomList = dungeon.data.rooms;
            foreach (RoomHandler room in RoomList) {
                if (!string.IsNullOrEmpty(room.GetRoomName()) && room.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS && room.HasActiveEnemies(RoomHandler.ActiveEnemyType.All)) {
                    foreach (AIActor enemy in room.GetActiveEnemies(RoomHandler.ActiveEnemyType.All)) {
                        if (!string.IsNullOrEmpty(enemy.OverrideDisplayName)) {
                            if (enemy.OverrideDisplayName.ToLower().StartsWith("corrupted")) {
                                enemy.gameObject.AddComponent<ExpandGlitchedEnemyStunManager>();
                            }
                        }
                    }
                }
            }
        }
    }
}

