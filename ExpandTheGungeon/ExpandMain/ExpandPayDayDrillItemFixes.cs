using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Pathfinding;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandPaydayDrillItemFixes {
               
        private IEnumerator HandleCombatWavesHook(Func<PaydayDrillItem, Dungeon, RoomHandler, Chest, IEnumerator>orig, PaydayDrillItem self, Dungeon d, RoomHandler newRoom, Chest sourceChest) {
            DrillWaveDefinition[] wavesToUse = self.D_Quality_Waves;
            switch (GameManager.Instance.RewardManager.GetQualityFromChest(sourceChest)) {
                case PickupObject.ItemQuality.C:
                    wavesToUse = self.C_Quality_Waves;
                    break;
                case PickupObject.ItemQuality.B:
                    wavesToUse = self.B_Quality_Waves;
                    break;
                case PickupObject.ItemQuality.A:
                    wavesToUse = self.A_Quality_Waves;
                    break;
                case PickupObject.ItemQuality.S:
                    wavesToUse = self.S_Quality_Waves;
                    break;
            }
            foreach (DrillWaveDefinition currentWave in wavesToUse) {
                int numEnemiesToSpawn = UnityEngine.Random.Range(currentWave.MinEnemies, currentWave.MaxEnemies + 1);
                for (int i = 0; i < numEnemiesToSpawn; i++) {
                    string EnemyGUID = d.GetWeightedProceduralEnemy().enemyGuid;
                    if (string.IsNullOrEmpty(EnemyGUID)) {
                        List<string> FallbackGUIDs = new List<string>() {
                            ExpandCustomEnemyDatabase.BootlegBullatGUID,
                            ExpandCustomEnemyDatabase.BootlegBulletManGUID,
                            ExpandCustomEnemyDatabase.BootlegBulletManBandanaGUID,
                            ExpandCustomEnemyDatabase.BootlegShotgunManBlueGUID,
                            ExpandCustomEnemyDatabase.BootlegShotgunManRedGUID
                        };
                        FallbackGUIDs = FallbackGUIDs.Shuffle();
                        EnemyGUID = BraveUtility.RandomElement(FallbackGUIDs);
                    }
                    AddSpecificEnemyToRoomProcedurallyFixed(newRoom, EnemyGUID, true);
                }
                yield return new WaitForSeconds(3f);
                while (newRoom.GetActiveEnemiesCount(RoomHandler.ActiveEnemyType.RoomClear) > 0) {
                    yield return new WaitForSeconds(1f);
                }
                if (newRoom.GetActiveEnemiesCount(RoomHandler.ActiveEnemyType.All) > 0) {
                    List<AIActor> activeEnemies = newRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                    for (int j = 0; j < activeEnemies.Count; j++) {
                        if (activeEnemies[j].IsNormalEnemy) { activeEnemies[j].EraseFromExistence(false); }
                    }
                }
            }
            yield break;
        }
        
        protected IEnumerator ExpandHandleSeamlessTransitionToCombatRoomHook(Func<PaydayDrillItem, RoomHandler, Chest, IEnumerator>orig, PaydayDrillItem self, RoomHandler sourceRoom, Chest sourceChest) {
            Dungeon dungeon = GameManager.Instance.Dungeon;
            if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.JUNGLEGEON) {
                GameManager.Instance.StartCoroutine(ExpandHandleTransitionToFallbackCombatRoom(self, sourceRoom, sourceChest));
                yield break;
            } else {
                sourceChest.majorBreakable.TemporarilyInvulnerable = true;
                sourceRoom.DeregisterInteractable(sourceChest);
                int tmapExpansion = 13;
                RoomHandler newRoom = dungeon.RuntimeDuplicateChunk(sourceRoom.area.basePosition, sourceRoom.area.dimensions, tmapExpansion, sourceRoom, true);
                newRoom.CompletelyPreventLeaving = true;
                List<Transform> movedObjects = new List<Transform>();
                string[] c_rewardRoomObjects = ReflectionHelpers.ReflectGetField<string[]>(typeof(PaydayDrillItem), "c_rewardRoomObjects", self);
                for (int i = 0; i < c_rewardRoomObjects.Length; i++) {
                    Transform transform = sourceRoom.hierarchyParent.Find(c_rewardRoomObjects[i]);
                    if (transform) {
                        movedObjects.Add(transform);
                        ExpandMoveObjectBetweenRooms(transform, sourceRoom, newRoom);
                    }
                }
                ExpandMoveObjectBetweenRooms(sourceChest.transform, sourceRoom, newRoom);
                if (sourceChest.specRigidbody) { PathBlocker.BlockRigidbody(sourceChest.specRigidbody, false); }
                Vector3 m_baseChestOffset = ReflectionHelpers.ReflectGetField<Vector3>(typeof(PaydayDrillItem), "m_baseChestOffset", self);
                Vector3 m_largeChestOffset = ReflectionHelpers.ReflectGetField<Vector3>(typeof(PaydayDrillItem), "m_largeChestOffset", self);
                Vector3 chestOffset = m_baseChestOffset;
                if (sourceChest.name.Contains("_Red") || sourceChest.name.Contains("_Black")) { chestOffset += m_largeChestOffset; }
                GameObject spawnedVFX = SpawnManager.SpawnVFX(self.DrillVFXPrefab, sourceChest.transform.position + chestOffset, Quaternion.identity);
                tk2dBaseSprite spawnedSprite = spawnedVFX.GetComponent<tk2dBaseSprite>();
                spawnedSprite.HeightOffGround = 1f;
                spawnedSprite.UpdateZDepth();
                Vector2 oldPlayerPosition = GameManager.Instance.BestActivePlayer.transform.position.XY();
                Vector2 playerOffset = oldPlayerPosition - sourceRoom.area.basePosition.ToVector2();
                Vector2 newPlayerPosition = newRoom.area.basePosition.ToVector2() + playerOffset;
                Pixelator.Instance.FadeToColor(0.25f, Color.white, true, 0.125f);
                Pathfinder.Instance.InitializeRegion(dungeon.data, newRoom.area.basePosition, newRoom.area.dimensions);
                GameManager.Instance.BestActivePlayer.WarpToPoint(newPlayerPosition, false, false);
                if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                    GameManager.Instance.GetOtherPlayer(GameManager.Instance.BestActivePlayer).ReuniteWithOtherPlayer(GameManager.Instance.BestActivePlayer, false);
                }
                yield return null;
                for (int j = 0; j < GameManager.Instance.AllPlayers.Length; j++) {
                    GameManager.Instance.AllPlayers[j].WarpFollowersToPlayer(false);
                    GameManager.Instance.AllPlayers[j].WarpCompanionsToPlayer(false);
                }
                yield return dungeon.StartCoroutine(HandleCombatRoomExpansion(self, sourceRoom, newRoom, sourceChest));
                self.DisappearDrillPoof.SpawnAtPosition(spawnedSprite.WorldBottomLeft + new Vector2(-0.0625f, 0.25f), 0f, null, null, null, new float?(3f), false, null, null, false);
                UnityEngine.Object.Destroy(spawnedVFX.gameObject);
                sourceChest.ForceUnlock();
                AkSoundEngine.PostEvent("Stop_OBJ_paydaydrill_loop_01", GameManager.Instance.gameObject);
                AkSoundEngine.PostEvent("Play_OBJ_item_spawn_01", GameManager.Instance.gameObject);
                bool goodToGo = false;
                while (!goodToGo) {
                    goodToGo = true;
                    for (int k = 0; k < GameManager.Instance.AllPlayers.Length; k++) {
                        float num = Vector2.Distance(sourceChest.specRigidbody.UnitCenter, GameManager.Instance.AllPlayers[k].CenterPosition);
                        if (num > 3f) { goodToGo = false; }
                    }
                    yield return null;
                }
                GameManager.Instance.MainCameraController.SetManualControl(true, true);
                GameManager.Instance.MainCameraController.OverridePosition = GameManager.Instance.BestActivePlayer.CenterPosition;
                for (int l = 0; l < GameManager.Instance.AllPlayers.Length; l++) {
                    GameManager.Instance.AllPlayers[l].SetInputOverride("shrinkage");
                }
                yield return dungeon.StartCoroutine(HandleCombatRoomShrinking(newRoom));
                for (int m = 0; m < GameManager.Instance.AllPlayers.Length; m++) {
                    GameManager.Instance.AllPlayers[m].ClearInputOverride("shrinkage");
                }
                Pixelator.Instance.FadeToColor(0.25f, Color.white, true, 0.125f);
                AkSoundEngine.PostEvent("Play_OBJ_paydaydrill_end_01", GameManager.Instance.gameObject);
                GameManager.Instance.MainCameraController.SetManualControl(false, false);
                GameManager.Instance.BestActivePlayer.WarpToPoint(oldPlayerPosition, false, false);
                if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                    GameManager.Instance.GetOtherPlayer(GameManager.Instance.BestActivePlayer).ReuniteWithOtherPlayer(GameManager.Instance.BestActivePlayer, false);
                }
                ExpandMoveObjectBetweenRooms(sourceChest.transform, newRoom, sourceRoom);
                for (int n = 0; n < movedObjects.Count; n++) {
                    ExpandMoveObjectBetweenRooms(movedObjects[n], newRoom, sourceRoom);
                }
                sourceRoom.RegisterInteractable(sourceChest);
                FieldInfo m_inEffectField = typeof(PaydayDrillItem).GetField("m_inEffect", BindingFlags.Instance | BindingFlags.NonPublic);
                m_inEffectField.SetValue(self, false);
            }
            yield break;
        }
        
        protected IEnumerator ExpandHandleTransitionToFallbackCombatRoom(PaydayDrillItem drillItem, RoomHandler sourceRoom, Chest sourceChest) {
            Dungeon d = GameManager.Instance.Dungeon;
            sourceChest.majorBreakable.TemporarilyInvulnerable = true;
            sourceRoom.DeregisterInteractable(sourceChest);
            // RoomHandler newRoom = ExpandUtility.Instance.AddCustomRuntimeRoom(drillItem.GenericFallbackCombatRoom, true, false, lightStyle: DungeonData.LightGenerationStyle.STANDARD);
            RoomHandler newRoom = d.AddRuntimeRoom(drillItem.GenericFallbackCombatRoom, null, DungeonData.LightGenerationStyle.FORCE_COLOR);
            newRoom.CompletelyPreventLeaving = true;
            Vector3 oldChestPosition = sourceChest.transform.position;
            sourceChest.transform.position = newRoom.Epicenter.ToVector3();
            if (sourceChest.transform.parent == sourceRoom.hierarchyParent) { sourceChest.transform.parent = newRoom.hierarchyParent; }
            SpeculativeRigidbody component = sourceChest.GetComponent<SpeculativeRigidbody>();
            if (component) {
                component.Reinitialize();
                PathBlocker.BlockRigidbody(component, false);
            }
            tk2dBaseSprite component2 = sourceChest.GetComponent<tk2dBaseSprite>();
            if (component2) { component2.UpdateZDepth(); }
            Vector3 m_baseChestOffset = ReflectionHelpers.ReflectGetField<Vector3>(typeof(PaydayDrillItem), "m_baseChestOffset", drillItem);
            Vector3 m_largeChestOffset = ReflectionHelpers.ReflectGetField<Vector3>(typeof(PaydayDrillItem), "m_largeChestOffset", drillItem);
            Vector3 chestOffset = m_baseChestOffset;
            if (sourceChest.name.Contains("_Red") || sourceChest.name.Contains("_Black")) { chestOffset += m_largeChestOffset; }
            GameObject spawnedVFX = SpawnManager.SpawnVFX(drillItem.DrillVFXPrefab, sourceChest.transform.position + chestOffset, Quaternion.identity);
            tk2dBaseSprite spawnedSprite = spawnedVFX.GetComponent<tk2dBaseSprite>();
            spawnedSprite.HeightOffGround = 1f;
            spawnedSprite.UpdateZDepth();
            Vector2 oldPlayerPosition = GameManager.Instance.BestActivePlayer.transform.position.XY();
            Vector2 newPlayerPosition = newRoom.Epicenter.ToVector2() + new Vector2(0f, -3f);
            Pixelator.Instance.FadeToColor(0.25f, Color.white, true, 0.125f);
            Pathfinder.Instance.InitializeRegion(d.data, newRoom.area.basePosition, newRoom.area.dimensions);
            GameManager.Instance.BestActivePlayer.WarpToPoint(newPlayerPosition, false, false);
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                GameManager.Instance.GetOtherPlayer(GameManager.Instance.BestActivePlayer).ReuniteWithOtherPlayer(GameManager.Instance.BestActivePlayer, false);
            }
            yield return null;
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++) {
                GameManager.Instance.AllPlayers[i].WarpFollowersToPlayer(false);
                GameManager.Instance.AllPlayers[i].WarpCompanionsToPlayer(false);
            }
            yield return new WaitForSeconds(drillItem.DelayPostExpansionPreEnemies);
            yield return GameManager.Instance.StartCoroutine(ExpandHandleCombatWaves(drillItem, d, newRoom, sourceChest));
            drillItem.DisappearDrillPoof.SpawnAtPosition(spawnedSprite.WorldBottomLeft + new Vector2(-0.0625f, 0.25f), 0f, null, null, null, new float?(3f), false, null, null, false);
            UnityEngine.Object.Destroy(spawnedVFX.gameObject);
            AkSoundEngine.PostEvent("Stop_OBJ_paydaydrill_loop_01", GameManager.Instance.gameObject);
            AkSoundEngine.PostEvent("Play_OBJ_item_spawn_01", GameManager.Instance.gameObject);
            sourceChest.ForceUnlock();
            bool goodToGo = false;
            while (!goodToGo) {
                goodToGo = true;
                for (int j = 0; j < GameManager.Instance.AllPlayers.Length; j++) {
                    float num = Vector2.Distance(sourceChest.specRigidbody.UnitCenter, GameManager.Instance.AllPlayers[j].CenterPosition);
                    if (num > 3f) { goodToGo = false; }
                }
                yield return null;
            }
            Pixelator.Instance.FadeToColor(0.25f, Color.white, true, 0.125f);
            GameManager.Instance.BestActivePlayer.WarpToPoint(oldPlayerPosition, false, false);
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                GameManager.Instance.GetOtherPlayer(GameManager.Instance.BestActivePlayer).ReuniteWithOtherPlayer(GameManager.Instance.BestActivePlayer, false);
            }
            sourceChest.transform.position = oldChestPosition;
            if (sourceChest.transform.parent == newRoom.hierarchyParent) {
                sourceChest.transform.parent = sourceRoom.hierarchyParent;
            }
            SpeculativeRigidbody component3 = sourceChest.GetComponent<SpeculativeRigidbody>();
            if (component3) { component3.Reinitialize(); }
            tk2dBaseSprite component4 = sourceChest.GetComponent<tk2dBaseSprite>();
            if (component4) { component4.UpdateZDepth(); }
            sourceRoom.RegisterInteractable(sourceChest);
            FieldInfo m_inEffectField = typeof(PaydayDrillItem).GetField("m_inEffect", BindingFlags.Instance | BindingFlags.NonPublic);
            m_inEffectField.SetValue(drillItem, false);
            // m_inEffect = false;
            yield break;
        }

        protected IEnumerator ExpandHandleCombatWaves(PaydayDrillItem drillItem, Dungeon d, RoomHandler newRoom, Chest sourceChest) {
            DrillWaveDefinition[] wavesToUse = drillItem.D_Quality_Waves;
            switch (GameManager.Instance.RewardManager.GetQualityFromChest(sourceChest)) {
                case PickupObject.ItemQuality.C:
                    wavesToUse = drillItem.C_Quality_Waves;
                    break;
                case PickupObject.ItemQuality.B:
                    wavesToUse = drillItem.B_Quality_Waves;
                    break;
                case PickupObject.ItemQuality.A:
                    wavesToUse = drillItem.A_Quality_Waves;
                    break;
                case PickupObject.ItemQuality.S:
                    wavesToUse = drillItem.S_Quality_Waves;
                    break;
            }
            foreach (DrillWaveDefinition currentWave in wavesToUse) {
                int numEnemiesToSpawn = UnityEngine.Random.Range(currentWave.MinEnemies, currentWave.MaxEnemies + 1);
                for (int i = 0; i < numEnemiesToSpawn; i++) {
                    string EnemyGUID = d.GetWeightedProceduralEnemy().enemyGuid;
                    if (string.IsNullOrEmpty(EnemyGUID)) {
                        List<string> FallbackGUIDs = new List<string>() {
                            ExpandCustomEnemyDatabase.BootlegBullatGUID,
                            ExpandCustomEnemyDatabase.BootlegBulletManGUID,
                            ExpandCustomEnemyDatabase.BootlegBulletManBandanaGUID,
                            ExpandCustomEnemyDatabase.BootlegShotgunManBlueGUID,
                            ExpandCustomEnemyDatabase.BootlegShotgunManRedGUID
                        };
                        FallbackGUIDs = FallbackGUIDs.Shuffle();
                        EnemyGUID = BraveUtility.RandomElement(FallbackGUIDs);
                    }
                    if (!EnemyDatabase.GetOrLoadByGuid(EnemyGUID).GetComponent<CompanionController>()) {
                        AddSpecificEnemyToRoomProcedurallyFixed(newRoom, EnemyGUID, true);
                    }
                }
                yield return new WaitForSeconds(3f);
                while (newRoom.GetActiveEnemiesCount(RoomHandler.ActiveEnemyType.RoomClear) > 0) {
                    yield return new WaitForSeconds(1f);
                }
                if (newRoom.GetActiveEnemiesCount(RoomHandler.ActiveEnemyType.All) > 0) {
                    List<AIActor> activeEnemies = newRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                    for (int j = 0; j < activeEnemies.Count; j++) {
                        if (activeEnemies[j].IsNormalEnemy) { activeEnemies[j].EraseFromExistence(false); }
                    }
                }
            }
            yield break;
        }
        
        // This function doesn't null check orLoadByGuid. If non fake prefab custom enemies are spawned (like the special rats on Hollow), then this would cause exception.
        // Added fall back GUIDs and use one of those for AIActor instead if this happens.
        public void AddSpecificEnemyToRoomProcedurallyFixed(RoomHandler room, string enemyGuid, bool reinforcementSpawn = false, Vector2? goalPosition = null) {
            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(enemyGuid);
            if (!orLoadByGuid) {
                List<string> FallbackGUIDs = new List<string>() {
                            ExpandCustomEnemyDatabase.BootlegBullatGUID,
                            ExpandCustomEnemyDatabase.BootlegBulletManGUID,
                            ExpandCustomEnemyDatabase.BootlegBulletManBandanaGUID,
                            ExpandCustomEnemyDatabase.BootlegShotgunManBlueGUID,
                            ExpandCustomEnemyDatabase.BootlegShotgunManRedGUID
                };
                FallbackGUIDs = FallbackGUIDs.Shuffle();
                orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(BraveUtility.RandomElement(FallbackGUIDs));
            }
            IntVector2 clearance = orLoadByGuid.specRigidbody.UnitDimensions.ToIntVector2(VectorConversions.Ceil);
            CellValidator cellValidator = delegate (IntVector2 c) {
                for (int i = 0; i < clearance.x; i++) {
                    int x = c.x + i;
                    for (int j = 0; j < clearance.y; j++) {
                        int y = c.y + j;
                        if (GameManager.Instance.Dungeon.data.isTopWall(x, y)) { return false; }
                    }
                }
                return true;
            };
            IntVector2? intVector;
            if (goalPosition != null) {
                intVector = room.GetNearestAvailableCell(goalPosition.Value, new IntVector2?(clearance), new CellTypes?(CellTypes.FLOOR), false, cellValidator);
            } else {
                intVector = room.GetRandomAvailableCell(new IntVector2?(clearance), new CellTypes?(CellTypes.FLOOR), false, cellValidator);
            }
            if (intVector != null) {
                AIActor aiactor = AIActor.Spawn(orLoadByGuid, intVector.Value, room, true, AIActor.AwakenAnimationType.Spawn, false);
                if (aiactor && reinforcementSpawn) {
                    if (aiactor.specRigidbody) { aiactor.specRigidbody.CollideWithOthers = false; }
                    aiactor.HandleReinforcementFallIntoRoom(0f);
                }
            } else {
                Debug.LogError("failed placement");
            }
        }

        private void ExpandMoveObjectBetweenRooms(Transform foundObject, RoomHandler fromRoom, RoomHandler toRoom) {
            Vector2 b = foundObject.position.XY() - fromRoom.area.basePosition.ToVector2();
            Vector2 v = toRoom.area.basePosition.ToVector2() + b;
            foundObject.transform.position = v;
            if (foundObject.parent == fromRoom.hierarchyParent) { foundObject.parent = toRoom.hierarchyParent; }
            SpeculativeRigidbody component = foundObject.GetComponent<SpeculativeRigidbody>();
            if (component) { component.Reinitialize(); }
            tk2dBaseSprite component2 = foundObject.GetComponent<tk2dBaseSprite>();
            if (component2) { component2.UpdateZDepth(); }
        }

        private IEnumerator HandleCombatRoomShrinking(RoomHandler targetRoom) {
            float elapsed = 5.5f;
            int numExpansionsDone = 6;
            while (elapsed > 0f) {
                elapsed -= BraveTime.DeltaTime * 9f;
                while (elapsed < numExpansionsDone && numExpansionsDone > 0) {
                    numExpansionsDone--;
                    ShrinkRoom(targetRoom);
                }
                yield return null;
            }
            yield break;
        }

        private IEnumerator HandleCombatRoomExpansion(PaydayDrillItem drillItem, RoomHandler sourceRoom, RoomHandler targetRoom, Chest sourceChest) {
            yield return new WaitForSeconds(drillItem.DelayPreExpansion);
            float duration = 5.5f;
            float elapsed = 0f;
            int numExpansionsDone = 0;
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime * 9f;
                while (elapsed > numExpansionsDone) {
                    numExpansionsDone++;
                    ExpandRoom(drillItem, targetRoom);
                    AkSoundEngine.PostEvent("Play_OBJ_rock_break_01", GameManager.Instance.gameObject);
                }
                yield return null;
            }
            Dungeon d = GameManager.Instance.Dungeon;
            Pathfinder.Instance.InitializeRegion(d.data, targetRoom.area.basePosition + new IntVector2(-5, -5), targetRoom.area.dimensions + new IntVector2(10, 10));
            yield return new WaitForSeconds(drillItem.DelayPostExpansionPreEnemies);
            yield return ExpandHandleCombatWaves(drillItem, d, targetRoom, sourceChest);
            yield break;
        }

        private void ShrinkRoom(RoomHandler r) {
            Dungeon dungeon = GameManager.Instance.Dungeon;
            AkSoundEngine.PostEvent("Play_OBJ_stone_crumble_01", GameManager.Instance.gameObject);
            tk2dTileMap tk2dTileMap = null;
            HashSet<IntVector2> hashSet = new HashSet<IntVector2>();
            for (int i = -5; i < r.area.dimensions.x + 5; i++) {
                for (int j = -5; j < r.area.dimensions.y + 5; j++) {
                    IntVector2 intVector = r.area.basePosition + new IntVector2(i, j);
                    CellData cellData = (!dungeon.data.CheckInBoundsAndValid(intVector)) ? null : dungeon.data[intVector];
                    if (cellData != null && cellData.type != CellType.WALL && cellData.HasTypeNeighbor(dungeon.data, CellType.WALL)) {
                        hashSet.Add(cellData.position);
                    }
                }
            }
            foreach (IntVector2 key in hashSet) {
                CellData cellData2 = dungeon.data[key];
                cellData2.breakable = true;
                cellData2.occlusionData.overrideOcclusion = true;
                cellData2.occlusionData.cellOcclusionDirty = true;
                tk2dTileMap = dungeon.ConstructWallAtPosition(key.x, key.y, true);
                r.Cells.Remove(cellData2.position);
                r.CellsWithoutExits.Remove(cellData2.position);
                r.RawCells.Remove(cellData2.position);
            }
            Pixelator.Instance.MarkOcclusionDirty();
            Pixelator.Instance.ProcessOcclusionChange(r.Epicenter, 1f, r, false);
            if (tk2dTileMap) { dungeon.RebuildTilemap(tk2dTileMap); }
        }

        private void ExpandRoom(PaydayDrillItem drillItem, RoomHandler r) {
            Dungeon dungeon = GameManager.Instance.Dungeon;
            AkSoundEngine.PostEvent("Play_OBJ_stone_crumble_01", GameManager.Instance.gameObject);
            tk2dTileMap tk2dTileMap = null;
            HashSet<IntVector2> hashSet = new HashSet<IntVector2>();
            for (int i = -5; i < r.area.dimensions.x + 5; i++) {
                for (int j = -5; j < r.area.dimensions.y + 5; j++) {
                    IntVector2 intVector = r.area.basePosition + new IntVector2(i, j);
                    CellData cellData = (!dungeon.data.CheckInBoundsAndValid(intVector)) ? null : dungeon.data[intVector];
                    if (cellData != null && cellData.type == CellType.WALL && cellData.HasTypeNeighbor(dungeon.data, CellType.FLOOR)) {
                        hashSet.Add(cellData.position);
                    }
                }
            }
            foreach (IntVector2 key in hashSet) {
                CellData cellData2 = dungeon.data[key];
                cellData2.breakable = true;
                cellData2.occlusionData.overrideOcclusion = true;
                cellData2.occlusionData.cellOcclusionDirty = true;
                tk2dTileMap = dungeon.DestroyWallAtPosition(key.x, key.y, true);
                if (UnityEngine.Random.value < 0.25f) {
                    drillItem.VFXDustPoof.SpawnAtPosition(key.ToCenterVector3((float)key.y), 0f, null, null, null, null, false, null, null, false);
                }
                r.Cells.Add(cellData2.position);
                r.CellsWithoutExits.Add(cellData2.position);
                r.RawCells.Add(cellData2.position);
            }
            Pixelator.Instance.MarkOcclusionDirty();
            Pixelator.Instance.ProcessOcclusionChange(r.Epicenter, 1f, r, false);
            if (tk2dTileMap) { dungeon.RebuildTilemap(tk2dTileMap); }
        }
    }
}


