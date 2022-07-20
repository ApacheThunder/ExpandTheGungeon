using Dungeonator;
using System;
using UnityEngine;
using MonoMod.RuntimeDetour;
using System.Reflection;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;
using System.Collections;
using HutongGames.PlayMaker;
using System.Collections.ObjectModel;
using InControl;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandDungeonFlows;
using static ExpandTheGungeon.ExpandUtilities.ReflectionHelpers;
// using Pathfinding;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandSharedHooks {
        public static Hook cellhook;
        public static Hook enterRoomHook;

        public static Hook igniteGoopsCircleHook;
        public static Hook buildPatchHook;
        public static Hook escapeRopeCanBeUsedHook;
        public static Hook clearPerLevelDataHook;
        public static Hook clearActiveGameDataHook;
        public static Hook wallmimichook;
        public static Hook Stringhook;
        public static Hook flowhook;
        public static Hook objectstamphook;
        public static Hook placeRoomHook;
        public static Hook getRandomFlowHook;
        public static Hook handlePitfallHook;
        public static Hook arrivalElevatorUpdateHook;
        public static Hook constructTK2DDungeonHook;
        public static Hook handleGrabbyGrabHook;
        public static Hook dungeonStartHook;
        public static Hook sellPitConfigureOnPlacementHook;
        public static Hook playerFiredHook;
        public static Hook flagCellsHook;
        public static Hook generateRoomDoorMeshHook;
        public static Hook animationCompletedHook;
        public static Hook teardownPunchoutHook;
        public static Hook GetSynergyStringHook;
        public static Hook buildorderLayerCenterJungleHook;
        public static Hook buildOcclusionLayerCenterJungle;
        public static Hook getTypeBorderGridForBorderIndexHook;
        public static Hook paydayDrillCombatWaveHook;
        public static Hook postGenerationCleanupHook;
        public static Hook checkforPlayerCollisionHook;
        public static Hook doorOpenHook;
        public static Hook throwGunHook;
        public static Hook onContinueGameSelectedHook;
        public static Hook getNextTilesetHook;
        public static Hook placePlayerInRoomHook;
        public static Hook floodFillDungeonInteriorHook;
        public static Hook getNextNearbyTileHook;
        public static Hook getAllNearbyTilesHook;
        public static Hook initNearbyTileCheckHook;
        public static Hook getTileHook;
        public static Hook floorChestPlacerConfigureOnPlacementHook;
        public static Hook applyBenefitHook;

        public static bool IsHooksInstalled = false;
        
        public static void InstallPrimaryHooks(bool InstallHooks = true) {
            if (InstallHooks) {
                    
                if (cellhook == null) {
                    if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing FlagCells Hook...."); }
                    cellhook = new Hook(
                        typeof(OccupiedCells).GetMethod("FlagCells", BindingFlags.Public | BindingFlags.Instance),
                        typeof(ExpandSharedHooks).GetMethod("FlagCellsHook", BindingFlags.NonPublic | BindingFlags.Instance),
                        typeof(OccupiedCells)
                    );
                }
                
                IsHooksInstalled = true;
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Primary hooks installed...", false); }
                return;
            } else {
                if (cellhook != null) { cellhook.Dispose(); cellhook = null; }
                IsHooksInstalled = false;
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Primary hooks removed...", false); }
                return;
            }
        }

        public static void InstallMidGameSaveHooks() {
            // Fix MidGame save stuff involving custom floors.
            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing MainMenuFoyerController.OnContinueGameSelected Hook...."); }
            onContinueGameSelectedHook = new Hook(
                typeof(MainMenuFoyerController).GetMethod("OnContinueGameSelected", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("OnContinueGameSelected", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(MainMenuFoyerController)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing GameManager.GetNextTileset Hook...."); }
            generateRoomDoorMeshHook = new Hook(
                typeof(GameManager).GetMethod("GetNextTileset", new Type[] { typeof(GlobalDungeonData.ValidTilesets) }),
                typeof(ExpandSharedHooks).GetMethod("GetNextTileset", BindingFlags.Public | BindingFlags.Instance),
                typeof(GameManager)
            );
        }


        public static void InstallRequiredHooks() {

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DeadlyDeadlyGoopManager.IgniteGoopsCircle Hook..."); }
            igniteGoopsCircleHook = new Hook(
                typeof(DeadlyDeadlyGoopManager).GetMethod(nameof(DeadlyDeadlyGoopManager.IgniteGoopsCircle), BindingFlags.Public | BindingFlags.Static),
                typeof(ExpandSharedHooks).GetMethod(nameof(IgniteGoopsCircleHook), BindingFlags.Public | BindingFlags.Static)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing TallGrassPatch.BuildPatch Hook..."); }
            buildPatchHook = new Hook(
                typeof(TallGrassPatch).GetMethod(nameof(TallGrassPatch.BuildPatch), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod(nameof(BuildPatchHook), BindingFlags.Public | BindingFlags.Instance),
                typeof(TallGrassPatch)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing EscapeRopeItem.CanBeUsed Hook..."); }
            escapeRopeCanBeUsedHook = new Hook(
                typeof(EscapeRopeItem).GetMethod("CanBeUsed", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod(nameof(EscapeRopCanBeUsedHook), BindingFlags.Public | BindingFlags.Instance),
                typeof(EscapeRopeItem)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing ClearPerLevelData Hook...."); }
            clearPerLevelDataHook = new Hook(
                typeof(GameManager).GetMethod(nameof(GameManager.ClearPerLevelData), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod(nameof(ClearPerLevelData), BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(GameManager)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing ClearActiveGameData Hook...."); }
            clearActiveGameDataHook = new Hook(
                typeof(GameManager).GetMethod(nameof(GameManager.ClearActiveGameData), BindingFlags.Public | BindingFlags.Instance), 
                typeof(ExpandSharedHooks).GetMethod(nameof(ClearActiveGameData), BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(GameManager)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing PlaceWallMimics Hook...."); }
            wallmimichook = new Hook(
                typeof(Dungeon).GetMethod("PlaceWallMimics", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandPlaceWallMimic).GetMethod(nameof(ExpandPlaceWallMimic.PlaceWallMimics), BindingFlags.Public | BindingFlags.Instance),
                typeof(Dungeon)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing GetEnemiesString Hook...."); }
            Stringhook = new Hook(
                typeof(StringTableManager).GetMethod("GetEnemiesString", BindingFlags.Public | BindingFlags.Static),
                typeof(ExpandSharedHooks).GetMethod("GetEnemiesString", BindingFlags.Public | BindingFlags.Static)
                // typeof(ExpandStringTableManager).GetMethod("GetEnemiesString", BindingFlags.Public | BindingFlags.Static)
            );
            

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing FlowDatabase.GetOrLoadByName Hook...."); }
            flowhook = new Hook(
                typeof(FlowDatabase).GetMethod("GetOrLoadByName", BindingFlags.Public | BindingFlags.Static),
                typeof(ExpandDungeonFlow).GetMethod("LoadCustomFlow", BindingFlags.Public | BindingFlags.Static)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing ApplyObjectStamp Hook...."); }
            objectstamphook = new Hook(
                typeof(TK2DDungeonAssembler).GetMethod("ApplyObjectStamp", BindingFlags.Public | BindingFlags.Static),
                typeof(ExpandSharedHooks).GetMethod("ApplyObjectStampHook", BindingFlags.Public | BindingFlags.Static)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing LoopBuilderComposite.PlaceRoom Hook...."); }
            placeRoomHook = new Hook(
                typeof(LoopBuilderComposite).GetMethod("PlaceRoom", BindingFlags.Public | BindingFlags.Static),
                typeof(ExpandSharedHooks).GetMethod("PlaceRoomHook", BindingFlags.Public | BindingFlags.Static)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing SemioticDungeonGenSettings.GetRandomFlow Hook...."); }
            getRandomFlowHook = new Hook(
                typeof(SemioticDungeonGenSettings).GetMethod("GetRandomFlow", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("GetRandomFlowHook", BindingFlags.Public | BindingFlags.Instance),
                typeof(SemioticDungeonGenSettings)
            );
            
            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing GetKicked.HandlePitfall Hook...."); }
            handlePitfallHook = new Hook(
                typeof(GetKicked).GetMethod("HandlePitfall", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("HandlePitfallHook", BindingFlags.Public | BindingFlags.Instance),
                typeof(GetKicked)
            );
            
            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing ElevatorArrivalController.Update Hook...."); }
            arrivalElevatorUpdateHook = new Hook(
                typeof(ElevatorArrivalController).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("ArrivalElevatorUpdateHook", BindingFlags.Public | BindingFlags.Instance),
                typeof(ElevatorArrivalController)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing TK2DDungeonAssembler.ConstructTK2DDungeon Hook...."); }
            constructTK2DDungeonHook = new Hook(
                typeof(TK2DDungeonAssembler).GetMethod("ConstructTK2DDungeon", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("ConstructTK2DDungeonHook", BindingFlags.Public | BindingFlags.Instance),
                typeof(TK2DDungeonAssembler)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing HellDragZoneController.HandleGrabbyGrab Hook...."); }
            handleGrabbyGrabHook = new Hook(
                typeof(HellDragZoneController).GetMethod("HandleGrabbyGrab", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("HandleGrabbyGrabHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(HellDragZoneController)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing ItemDB.DungeonStart Hook...."); }
            dungeonStartHook = new Hook(
                typeof(ItemDB).GetMethod("DungeonStart", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("DungeonStartHook", BindingFlags.Public | BindingFlags.Instance),
                typeof(ItemDB)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing SellCellController.ConfigureOnPlacement Hook...."); }
            sellPitConfigureOnPlacementHook = new Hook(
                typeof(SellCellController).GetMethod("ConfigureOnPlacement", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("SellPitConfigureOnPlacementHook", BindingFlags.Public | BindingFlags.Instance),
                typeof(SellCellController)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing BaseShopController.PlayerFired Hook...."); }
            playerFiredHook = new Hook(
               typeof(BaseShopController).GetMethod("PlayerFired", BindingFlags.NonPublic | BindingFlags.Instance),
               typeof(ExpandSharedHooks).GetMethod("PlayerFiredHook", BindingFlags.NonPublic | BindingFlags.Instance),
               typeof(BaseShopController)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing OccupiedCells.FlagCells Hook...."); }
            flagCellsHook  = new Hook(
                typeof(OccupiedCells).GetMethod("FlagCells", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("FlagCellsHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(OccupiedCells)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing SecretRoomBuilder.GenerateRoomDoorMesh Hook...."); }
            generateRoomDoorMeshHook = new Hook(
                typeof(SecretRoomBuilder).GetMethod("GenerateRoomDoorMesh", BindingFlags.NonPublic | BindingFlags.Static),
                typeof(ExpandSharedHooks).GetMethod("GenerateRoomDoorMeshHook", BindingFlags.NonPublic | BindingFlags.Static)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing AIAnimator.AnimationCompleted Hook...."); }
            animationCompletedHook = new Hook(
                typeof(AIAnimator).GetMethod("AnimationCompleted", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("AnimationCompletedHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(AIAnimator)
            );
            
            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing PunchoutController.TeardownPunchout Hook...."); }
            teardownPunchoutHook = new Hook(
                typeof(PunchoutController).GetMethod("TeardownPunchout", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod(nameof(TeardownPunchout_Hook), BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(PunchoutController)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing StringTableManager.GetSynergyString Hook...."); }
            GetSynergyStringHook = new Hook(
                typeof(StringTableManager).GetMethod("GetSynergyString", BindingFlags.Static | BindingFlags.Public),
                typeof(ExpandSharedHooks).GetMethod("GetSynergyString", BindingFlags.Static | BindingFlags.Public)
                // typeof(ExpandStringTableManager).GetMethod("GetSynergyString", BindingFlags.Static | BindingFlags.Public)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing TK2DDungeonAssembler.BuildBorderLayerCenterJungle Hook...."); }
            buildorderLayerCenterJungleHook = new Hook(
                typeof(TK2DDungeonAssembler).GetMethod("BuildBorderLayerCenterJungle", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("BuildBorderLayerCenterJungleHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(TK2DDungeonAssembler)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing TK2DDungeonAssembler.BuildOcclusionLayerCenterJungle Hook...."); }
            buildOcclusionLayerCenterJungle = new Hook(
                typeof(TK2DDungeonAssembler).GetMethod("BuildOcclusionLayerCenterJungle", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("BuildBorderLayerCenterJungleHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(TK2DDungeonAssembler)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing TK2DDungeonAssembler.GetTypeBorderGridForBorderIndex Hook...."); }
            getTypeBorderGridForBorderIndexHook = new Hook(
                typeof(TK2DDungeonAssembler).GetMethod("GetTypeBorderGridForBorderIndex", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("GetTypeBorderGridForBorderIndexHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(TK2DDungeonAssembler)
            );

            /*if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing AkSoundEngine.PostEvent Hook...."); }
            Hook postEventHook = new Hook(
                typeof(AkSoundEngine).GetMethod("PostEvent", new Type[] { typeof(string), typeof(GameObject) }),
                typeof(ExpandSharedHooks).GetMethod("PostEventHook", BindingFlags.Public | BindingFlags.Static)
            );*/

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing PaydayDrillItem.HandleCombatWaves Hook...."); }
            paydayDrillCombatWaveHook = new Hook(
                typeof(PaydayDrillItem).GetMethod("HandleCombatWaves", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandPaydayDrillItemFixes).GetMethod("HandleCombatWavesHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(PaydayDrillItem)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing PaydayDrillItem.HandleSeamlessTransitionToCombatRoom Hook...."); }
            Hook handleSeamlessTransitionToCombatRoomHook = new Hook(
                typeof(PaydayDrillItem).GetMethod("HandleSeamlessTransitionToCombatRoom", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandPaydayDrillItemFixes).GetMethod("ExpandHandleSeamlessTransitionToCombatRoomHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(PaydayDrillItem)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonData.PostGenerationCleanup Hook...."); }
            postGenerationCleanupHook = new Hook(
                typeof(DungeonData).GetMethod("PostGenerationCleanup", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("PostGenerationCleanupHook", BindingFlags.Public | BindingFlags.Instance),
                typeof(DungeonData)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonDoorController.CheckForPlayerCollision Hook...."); }
            checkforPlayerCollisionHook = new Hook(
                typeof(DungeonDoorController).GetMethod("CheckForPlayerCollision", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandDungeonDoorManager).GetMethod("CheckForPlayerCollisionHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(DungeonDoorController)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonDoorController.Open Hook...."); }
            doorOpenHook = new Hook(
                typeof(DungeonDoorController).GetMethod("Open", BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandDungeonDoorManager).GetMethod("Expand_Open", BindingFlags.Public | BindingFlags.Instance),
                typeof(DungeonDoorController)
            );
            
            /*Hook handleBulletDeletionFramesHook = new Hook(
                typeof(Exploder).GetMethod("HandleBulletDeletionFrames", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("HandleBulletDeletionFrames", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Exploder)
            );*/

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing Gun.ThrowGun Hook...."); }
            throwGunHook = new Hook(
                typeof(Gun).GetMethod("ThrowGun", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod("ThrowGunHook", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Gun)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing Dungeon.PlacePlayerInRoom Hook...."); }
            placePlayerInRoomHook = new Hook(
                typeof(Dungeon).GetMethod("PlacePlayerInRoom", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod(nameof(PlacePlayerInRoomHook), BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(Dungeon)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing DungeonData.FloodFillDungeonInterior Hook...."); }
            floodFillDungeonInteriorHook = new Hook(
                typeof(DungeonData).GetMethod("FloodFillDungeonInterior", BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod(nameof(FloodFillDungeonInteriorHook), BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(DungeonData)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing PhysicsEngine.InitNearbyTileCheck Hook...."); }
            initNearbyTileCheckHook = new Hook(
                typeof(PhysicsEngine).GetMethod("InitNearbyTileCheck", BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder, CallingConventions.Any, new Type[] { typeof(float), typeof(float), typeof(float), typeof(float), typeof(tk2dTileMap), typeof(IntVector2), typeof(float), typeof(float), typeof(IntVector2), typeof(DungeonData) }, new ParameterModifier[0]),
                typeof(ExpandSharedHooks).GetMethod(nameof(InitNearbyTileCheck), BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(PhysicsEngine)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing PhysicsEngine.GetTile Hook...."); }
            getTileHook = new Hook(
                typeof(PhysicsEngine).GetMethod("GetTile", BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder, CallingConventions.Any, new Type[] { typeof(int), typeof(int), typeof(tk2dTileMap), typeof(int), typeof(string), typeof(DungeonData) }, new ParameterModifier[0]),
                typeof(ExpandSharedHooks).GetMethod(nameof(GetTile), BindingFlags.NonPublic | BindingFlags.Instance),
                typeof(PhysicsEngine)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing FloorChestPlacer.ConfigureOnPlacement Hook...."); }
            floorChestPlacerConfigureOnPlacementHook = new Hook(
                typeof(FloorChestPlacer).GetMethod(nameof(FloorChestPlacer.ConfigureOnPlacement), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod(nameof(FloorChestPlacerConfigureOnPlacementHook)),
                typeof(FloorChestPlacer)
            );

            if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing ShrineBenefit.ApplyBenefit Hook...."); }
            applyBenefitHook = new Hook(
                typeof(ShrineBenefit).GetMethod(nameof(ShrineBenefit.ApplyBenefit), BindingFlags.Public | BindingFlags.Instance),
                typeof(ExpandSharedHooks).GetMethod(nameof(ApplyBenefit)),
                typeof(ShrineBenefit)
            );
            
            return;
        }

        private void FloodFillDungeonInteriorHook(Action<DungeonData>orig, DungeonData self) {
            Stack<CellData> stack = new Stack<CellData>();
            for (int i = 0; i < self.rooms.Count; i++) {
                if (self.rooms[i] == self.Entrance || self.rooms[i].IsStartOfWarpWing) {
                    try {
                        stack.Push(self[self.rooms[i].GetRandomAvailableCellDumb()]);
                    } catch (Exception ex) {
                        ETGModConsole.Log("[ExpandTheGungeon] Warning: Exception caught at DungeonData.FloodFillDungeonInterior!");
                        Debug.LogException(ex);
                    }
                }
            }
            try { 
                while (stack.Count > 0) {
                    CellData cellData = stack.Pop();
                    if (cellData.type != CellType.WALL) {
                        List<CellData> cellNeighbors = self.GetCellNeighbors(cellData, false);
                        cellData.isGridConnected = true;
                        for (int j = 0; j < cellNeighbors.Count; j++) {
                            if (cellNeighbors[j] != null && cellNeighbors[j].type != CellType.WALL && !cellNeighbors[j].isGridConnected) {
                                stack.Push(cellNeighbors[j]);
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] Warning: Exception caught at DungeonData.FloodFillDungeonInterior!");
                Debug.LogException(ex);
            }
        }

        public static void IgniteGoopsCircleHook(Action<Vector2, float>orig, Vector2 position, float radius) {
            orig(position, radius);
            for (int j = 0; j < ExpandStaticReferenceManager.AllGrasses.Count; j++) {
                ExpandStaticReferenceManager.AllGrasses[j].IgniteCircle(position, radius);
            }
        }

        public void BuildPatchHook(Action<TallGrassPatch>orig, TallGrassPatch self) {
            ExpandTallGrassPatchSystem exTallGrassSystem = self.gameObject.AddComponent<ExpandTallGrassPatchSystem>();
            exTallGrassSystem.cells = self.cells;
            UnityEngine.Object.Destroy(self.gameObject.GetComponent<TallGrassPatch>());
            exTallGrassSystem.BuildPatch();
        }

        public bool EscapeRopCanBeUsedHook(Func<EscapeRopeItem, PlayerController, bool> orig, EscapeRopeItem self, PlayerController user) {
            return orig(self, user) && user?.CurrentRoom != null && !user.CurrentRoom.IsActuallyWildWestEntrance();
        }
        
        private void ClearPerLevelData(Action<GameManager> orig, GameManager self) {
            ExpandStaticReferenceManager.ClearStaticPerLevelData();
            orig(self);
        }

        private void ClearActiveGameData(Action<GameManager, bool, bool> orig, GameManager self, bool destroyGameManager, bool endSession) {
            ExpandStaticReferenceManager.ClearStaticPerLevelData();
            orig(self, destroyGameManager, endSession);
        }

        private void FlagCellsHook(Action<OccupiedCells> orig, OccupiedCells self) {
            try { orig(self); } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] Warning: Exception caught in OccupiedCells.FlagCells!");
                    Debug.Log("Warning: Exception caught in OccupiedCells.FlagCells!");
                    Debug.LogException(ex);
                }
                return;
            }
        }

        protected virtual void EnteredNewRoomHook(Action<RoomHandler, PlayerController> orig, RoomHandler self, PlayerController player) {
            orig(self, player);

            if (string.IsNullOrEmpty(self.GetRoomName())) {
                ETGModConsole.Log("[DEBUG] Player entered a room with null name field (hallway room?)", false);
                return;
            }

            ETGModConsole.Log("[DEBUG] Player entered room with name '" + self.GetRoomName() + "' .", false);
            
        }
        
        public static string GetEnemiesString(Func<string, int, string> orig, string key, int index = -1) {
            string m_EnemyString = orig(key, index);
            if (m_EnemyString != "ENEMIES_STRING_NOT_FOUND") { return m_EnemyString; } else { return key; }
        }

        public static string GetSynergyString(Func<string, int, string> orig, string key, int index = -1) {
            string m_Text = orig(key, index);
            if (!string.IsNullOrEmpty(m_Text)) { return orig(key, index); } else { return key; }
        }

        public static GameObject ApplyObjectStampHook(int ix, int iy, ObjectStampData osd, Dungeon d, tk2dTileMap map, bool flipX = false, bool isLightStamp = false) {
            try {
                DungeonTileStampData.StampSpace occupySpace = osd.occupySpace;
                for (int i = 0; i < osd.width; i++) {
                    for (int j = 0; j < osd.height; j++) {
                        CellData cellData = d.data.cellData[ix + i][iy + j];
                        CellVisualData cellVisualData = cellData.cellVisualData;
                        if (cellVisualData.forcedMatchingStyle != DungeonTileStampData.IntermediaryMatchingStyle.ANY && cellVisualData.forcedMatchingStyle != osd.intermediaryMatchingStyle) {
                            return null;
                        }
                        if (osd.placementRule != DungeonTileStampData.StampPlacementRule.ALONG_LEFT_WALLS || !isLightStamp) {
                            bool flag = cellVisualData.containsWallSpaceStamp;
                            if (cellVisualData.facewallGridPreventsWallSpaceStamp && isLightStamp) { flag = false; }
                            if (occupySpace == DungeonTileStampData.StampSpace.BOTH_SPACES) {
                                if (cellVisualData.containsObjectSpaceStamp || flag || (!isLightStamp && cellVisualData.containsLight)) {
                                    return null;
                                }
                                if (cellData.type == CellType.PIT) { return null; }
                            } else if (occupySpace == DungeonTileStampData.StampSpace.OBJECT_SPACE) {
                                if (cellVisualData.containsObjectSpaceStamp) { return null; }
                                if (cellData.type == CellType.PIT) { return null; }
                            } else if (occupySpace == DungeonTileStampData.StampSpace.WALL_SPACE && (flag || (!isLightStamp && cellVisualData.containsLight))) {
                                return null;
                            }
                        }
                    }
                }
                int num = (occupySpace != DungeonTileStampData.StampSpace.OBJECT_SPACE) ? GlobalDungeonData.wallStampLayerIndex : GlobalDungeonData.objectStampLayerIndex;
                float z = map.data.Layers[num].z;
                Vector3 vector = osd.objectReference.transform.position;
                ObjectStampOptions component = osd.objectReference.GetComponent<ObjectStampOptions>();
                if (component != null) { vector = component.GetPositionOffset(); }
                GameObject gameObject = UnityEngine.Object.Instantiate(osd.objectReference);            
                gameObject.transform.position = new Vector3(ix, iy, z) + vector;
                if (!isLightStamp && osd.placementRule == DungeonTileStampData.StampPlacementRule.ALONG_LEFT_WALLS) {
                    gameObject.transform.position = new Vector3(ix + 1, iy, z) + vector.WithX(-vector.x);
                }
                tk2dSprite component2 = gameObject.GetComponent<tk2dSprite>();
                RoomHandler absoluteRoomFromPosition = GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(new IntVector2(ix, iy));
                MinorBreakable componentInChildren = gameObject.GetComponentInChildren<MinorBreakable>();
                if (componentInChildren) {
                    if (osd.placementRule == DungeonTileStampData.StampPlacementRule.ON_ANY_FLOOR) {
                        componentInChildren.IgnoredForPotShotsModifier = true;
                    }
                    componentInChildren.IsDecorativeOnly = true;
                }
                IPlaceConfigurable @interface = gameObject.GetInterface<IPlaceConfigurable>();
                if (@interface != null) { @interface.ConfigureOnPlacement(absoluteRoomFromPosition); }
                SurfaceDecorator component3 = gameObject.GetComponent<SurfaceDecorator>();
                if (component3 != null) {
                    component3.Decorate(absoluteRoomFromPosition);
                }
                if (flipX) {
                    if (component2 != null) {
                        component2.FlipX = true;
                        float x = Mathf.Ceil(component2.GetBounds().size.x);
                        gameObject.transform.position = gameObject.transform.position + new Vector3(x, 0f, 0f);
                    } else {
                        gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(-1f, 1f, 1f));
                    }
                }
                gameObject.transform.parent = ((absoluteRoomFromPosition == null) ? null : absoluteRoomFromPosition.hierarchyParent);
                DepthLookupManager.ProcessRenderer(gameObject.GetComponentInChildren<Renderer>());
                if (component2 != null) {
                    component2.UpdateZDepth();
                }
                for (int k = 0; k < osd.width; k++) {
                    for (int l = 0; l < osd.height; l++) {
                        CellVisualData cellVisualData2 = d.data.cellData[ix + k][iy + l].cellVisualData;
                        if (occupySpace == DungeonTileStampData.StampSpace.OBJECT_SPACE) {
                            cellVisualData2.containsObjectSpaceStamp = true;
                        }
                        if (occupySpace == DungeonTileStampData.StampSpace.WALL_SPACE) {
                            cellVisualData2.containsWallSpaceStamp = true;
                        }
                        if (occupySpace == DungeonTileStampData.StampSpace.BOTH_SPACES) {
                            cellVisualData2.containsObjectSpaceStamp = true;
                            cellVisualData2.containsWallSpaceStamp = true;
                        }
                    }
                }
                return gameObject;
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("Warning: Exception caught during ApplyObjectStamp method during Dungeon generation!");
                    Debug.Log("Warning: Exception caught during ApplyObjectStamp method during Dungeon generation!");
                    Debug.LogException(ex);
                }
                return null;
            }
        }        
        
        public static RoomHandler PlaceRoomHook(BuilderFlowNode current, SemioticLayoutManager layout, IntVector2 newRoomPosition) {
            try {
                // Verify that visual subtype override actually exists in the current dungoen prefab's DungeonMaterials. (Dungeon.roomMaterialDefinitions)
                // If not, override the override to avoid array index out of range exception!
                Dungeon dungeon;
                if (GameManager.Instance.CurrentlyGeneratingDungeonPrefab) {
                    dungeon = GameManager.Instance.BestGenerationDungeonPrefab;
                } else {
                    dungeon = GameManager.Instance.Dungeon;
                }
                if (current.assignedPrototypeRoom.overrideRoomVisualType >= dungeon.roomMaterialDefinitions.Length) {
                    if (ExpandSettings.debugMode) {
                        string Message1 = "[ExpandTheGungeon] WARNING: Exception prevented during LoopBuilderComposite.PlaceRoom!";
                        string Message2 = "Room visual subtype isn't supported by current tileset! Default visual style will be used to prevent exceptions/softlock!";
                        ETGModConsole.Log(Message1);
                        ETGModConsole.Log(Message2);
                        Debug.Log(Message1);
                        Debug.Log(Message2);
                        if (!string.IsNullOrEmpty(current.assignedPrototypeRoom.name)) {
                            ETGModConsole.Log("Name of assigned room: " + current.assignedPrototypeRoom.name);
                            Debug.Log("Name of assigned room: " + current.assignedPrototypeRoom.name);
                        }
                    }
                    // As far as I can tell it does not instantiate this during floor generation so best to instantiate before modifying it!
                    PrototypeDungeonRoom m_FixedRoom = UnityEngine.Object.Instantiate(current.assignedPrototypeRoom);
                    m_FixedRoom.overrideRoomVisualType = -1; // Setting this to -1 allows it to assign a random available subtype to the room during RoomHandler creation.
                    // Assign "fixed" room over the old one. 
                    // This must occur before the RoomHandler object is created as that is where the exception would have occured!
                    current.assignedPrototypeRoom = m_FixedRoom;
                }

                IntVector2 d = new IntVector2(current.assignedPrototypeRoom.Width, current.assignedPrototypeRoom.Height);
			    CellArea cellArea = new CellArea(newRoomPosition, d, 0);
                cellArea.prototypeRoom = current.assignedPrototypeRoom;
			    cellArea.instanceUsedExits = new List<PrototypeRoomExit>();
			    if (current.usesOverrideCategory) { cellArea.PrototypeRoomCategory = current.overrideCategory; }
                RoomHandler roomHandler = new RoomHandler(cellArea);
			    roomHandler.distanceFromEntrance = 0;
			    roomHandler.CalculateOpulence();
			    roomHandler.CanReceiveCaps = current.node.receivesCaps;
			    current.instanceRoom = roomHandler;
			    if (roomHandler.area.prototypeRoom != null && current.Category == PrototypeDungeonRoom.RoomCategory.SECRET && current.parentBuilderNode != null && current.parentBuilderNode.instanceRoom != null) {
                    roomHandler.AssignRoomVisualType(current.parentBuilderNode.instanceRoom.RoomVisualSubtype, false);
			    }
			    layout.StampCellAreaToLayout(roomHandler, false);
			    return roomHandler;
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] [DEBUG] ERROR: Exception during LoopBuilderComposite.PlaceRoom!");
                ETGModConsole.Log("[DEBUG] Name of assigned room: " + current.assignedPrototypeRoom.name);
                if (current.instanceRoom != null) {
                    ETGModConsole.Log("[DEBUG] Name of instanced room: " + current.instanceRoom.GetRoomName());
                }
                Debug.Log("ERROR: Exception during LoopBuilderComposite.PlaceRoom!");
                Debug.Log("Name of assigned room: " + current.assignedPrototypeRoom.name);
                Debug.LogException(ex);
                return null;
            }
		}

        public DungeonFlow GetRandomFlowHook(Func<SemioticDungeonGenSettings, DungeonFlow> orig, SemioticDungeonGenSettings self) {
            try {
                return orig(self);
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("[DEBUG] WARNING: Attempted to return a null DungeonFlow or primary flow list is empty in SemioticDungeonGenSettings.GetRandomFlow!"); }
                Debug.Log("WARNING: Attempted to return a null DungeonFlow or primary flow list is empty in SemioticDungeonGenSettings.GetRandomFlow!");
                Debug.LogException(ex);
                // Falling back to mod's compiled list of Flows                
                if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER) {
                    return ExpandDungeonFlow.Foyer_Flow;
                } else {
                    Dungeon dungeon = GameManager.Instance.Dungeon;
                    if (!dungeon) { return FlowDatabase.GetOrLoadByName("Complex_Flow_Test"); }
                    List<DungeonFlow> m_fallbacklist = new List<DungeonFlow>();
                    switch (dungeon.tileIndices.tilesetId) {
                        case GlobalDungeonData.ValidTilesets.CASTLEGEON:
                            foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                                if (!string.IsNullOrEmpty(flow?.name) && flow.name.ToLower().StartsWith("f1_castle_flow")) { m_fallbacklist.Add(flow); }
                            }                            
                            break;
                        case GlobalDungeonData.ValidTilesets.SEWERGEON:
                            foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                                if (!string.IsNullOrEmpty(flow?.name) && flow.name.ToLower().StartsWith("f1a_sewers_flow")) { m_fallbacklist.Add(flow); }
                            }
                            break;
                        case GlobalDungeonData.ValidTilesets.GUNGEON:
                            foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                                if (!string.IsNullOrEmpty(flow?.name) && flow.name.ToLower().StartsWith("f2_gungeon_flow")) { m_fallbacklist.Add(flow); }
                            }
                            break;
                        case GlobalDungeonData.ValidTilesets.CATHEDRALGEON:
                            foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                                if (!string.IsNullOrEmpty(flow?.name) && flow.name.ToLower().StartsWith("f2a_cathedral_flow")) { m_fallbacklist.Add(flow); }
                            }
                            break;
                        case GlobalDungeonData.ValidTilesets.MINEGEON:
                            foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                                if (!string.IsNullOrEmpty(flow?.name) && flow.name.ToLower().StartsWith("f3_mines_flow")) { m_fallbacklist.Add(flow); }
                            }
                            break;
                        case GlobalDungeonData.ValidTilesets.RATGEON:
                            foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                                if (!string.IsNullOrEmpty(flow?.name) && flow.name.ToLower().StartsWith("resourcefulratlair_flow")) { m_fallbacklist.Add(flow); }
                            }
                            break;
                        case GlobalDungeonData.ValidTilesets.CATACOMBGEON:
                            foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                                if (!string.IsNullOrEmpty(flow?.name) && flow.name.ToLower().StartsWith("f4_catacomb_flow")) { m_fallbacklist.Add(flow); }
                            }
                            break;
                        case GlobalDungeonData.ValidTilesets.OFFICEGEON:
                            foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                                if (!string.IsNullOrEmpty(flow?.name) && flow.name.ToLower().StartsWith("fs4_nakatomi_flow")) { m_fallbacklist.Add(flow); }
                            }
                            break;
                        case GlobalDungeonData.ValidTilesets.FORGEGEON:
                            foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                                if (!string.IsNullOrEmpty(flow?.name) && flow.name.ToLower().StartsWith("f5_forge_flow")) { m_fallbacklist.Add(flow); }
                            }
                            break;
                        case GlobalDungeonData.ValidTilesets.HELLGEON:
                            foreach (DungeonFlow flow in ExpandDungeonFlow.KnownFlows) {
                                if (!string.IsNullOrEmpty(flow?.name) && flow.name.ToLower().StartsWith("f6_bullethell_flow")) { m_fallbacklist.Add(flow); }
                            }
                            break;
                    }
                    if (m_fallbacklist.Count > 0) {
                        if (m_fallbacklist.Count == 1) {
                            return m_fallbacklist[0];
                        } else {
                            return m_fallbacklist[BraveRandom.GenerationRandomRange(0, m_fallbacklist.Count)];
                        }
                    }
                    if (ExpandSettings.debugMode) {
                        ETGModConsole.Log("[DEBUG] WARNING: Could not determine a proper fallback flow! Using a default flow instead!");
                    }
                    Debug.Log("WARNING: Could not determine a proper fallback flow! Using a default flow instead!");
                    return FlowDatabase.GetOrLoadByName("Complex_Flow_Test");
                }
            }            
        }
        
        // Fix exception if Rat Corpse is kicked into a pit in a room that doesn't have TargetPitFallRoom setup.
        public IEnumerator HandlePitfallHook(Func<GetKicked, SpeculativeRigidbody, IEnumerator> orig, GetKicked self, SpeculativeRigidbody srb) {
            FieldInfo field = typeof(GetKicked).GetField("m_isFalling", BindingFlags.Instance | BindingFlags.NonPublic);
            field.SetValue(self, true);
                        
            RoomHandler firstRoom = srb.UnitCenter.GetAbsoluteRoom();
            TalkDoerLite talkdoer = srb.GetComponent<TalkDoerLite>();
            firstRoom.DeregisterInteractable(talkdoer);
            srb.Velocity = srb.Velocity.normalized * 0.125f;
            AIAnimator anim = srb.GetComponent<AIAnimator>();
            anim.PlayUntilFinished("pitfall", false, null, -1f, false);
            while (anim.IsPlaying("pitfall")) { yield return null; }
            anim.PlayUntilCancelled("kick1", false, null, -1f, false);
            srb.Velocity = Vector2.zero;        
            // if TargetPitfallRoom is null/not setup, we will choose a random room (or maintance room if rat fell in the elevator shaft in entrance room)    
            if (firstRoom.TargetPitfallRoom != null) {
                RoomHandler targetRoom = firstRoom.TargetPitfallRoom;
                Transform[] childTransforms = targetRoom.hierarchyParent.GetComponentsInChildren<Transform>(true);
                Transform arrivalTransform = null;
                for (int i = 0; i < childTransforms.Length; i++) {
                    if (childTransforms[i].name == "Arrival") { arrivalTransform = childTransforms[i]; break; }
                }
                if (arrivalTransform != null) {
                    srb.transform.position = arrivalTransform.position + new Vector3(1f, 1f, 0f);
                    srb.Reinitialize();
                    RoomHandler.unassignedInteractableObjects.Add(talkdoer);
                    yield break;
                } else {
                    if (ExpandSettings.debugMode) {
                        ETGModConsole.Log("[DEBUG] Destination room does not have an Arrival object! Using a random location for the landing spot.");
                    }
                    Debug.Log("[HutongGames.PlayMaker.Actions.GetKicked] Destination room does not have an Arrival object! Using a random location for the landing spot.");
                    // if target room doesn't have arrival object, choose a random landing spot instead.
                    IntVector2? randomPosition = ExpandUtility.GetRandomAvailableCellSmart(targetRoom, new IntVector2(2, 2));
                    if (randomPosition != null && randomPosition.HasValue) {
                        srb.transform.position = randomPosition.Value.ToVector3(srb.transform.position.z);
                        srb.Reinitialize();
                        RoomHandler.unassignedInteractableObjects.Add(talkdoer);
                        field.SetValue(self, false);
                        yield break;
                    } else {
                        // Could not find a suitable location to place corpse. Let's just destroy it and call it a day. :P
                        UnityEngine.Object.Destroy(talkdoer.gameObject);
                        yield break;
                    }
                }                
            } else {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[DEBUG] Rat corpse fell into a pit that belonged to a room that didn't have TargetPitFallRoom setup! Using fall back method.");
                }
                Debug.Log("[HutongGames.PlayMaker.Actions.GetKicked] Rat corpse fell into a pit that belonged to a room that didn't have TargetPitFallRoom setup! Using fall back method instead.");
                if (GameManager.Instance.Dungeon.data.rooms != null && GameManager.Instance.Dungeon.data.rooms.Count > 0) {
                    RoomHandler startRoom = srb.UnitCenter.GetAbsoluteRoom();
                    RoomHandler randomTargetRoom = BraveUtility.RandomElement(GameManager.Instance.Dungeon.data.rooms);
                    RoomHandler maintanenceRoom = null;
                    if (startRoom == null) { UnityEngine.Object.Destroy(talkdoer.gameObject); yield break; }

                    for (int i = 0; i < GameManager.Instance.Dungeon.data.rooms.Count; i++) {
                        if (GameManager.Instance.Dungeon.data.rooms[i] != null &&
                            GameManager.Instance.Dungeon.data.rooms[i].GetRoomName() != null &&
                            GameManager.Instance.Dungeon.data.rooms[i].GetRoomName() != string.Empty)
                        {
                            if (GameManager.Instance.Dungeon.data.rooms[i].GetRoomName().ToLower().StartsWith("elevatormaintenance")) {
                                maintanenceRoom = GameManager.Instance.Dungeon.data.rooms[i];
                                break;
                            }
                        }                        
                    }
                    if (startRoom.GetRoomName().StartsWith(GameManager.Instance.Dungeon.data.Entrance.GetRoomName()) && maintanenceRoom != null) {
                        srb.transform.position = (new IntVector2(6, 6) + maintanenceRoom.area.basePosition).ToVector3(srb.transform.position.z);
                        srb.Reinitialize();
                        RoomHandler.unassignedInteractableObjects.Add(talkdoer);
                        field.SetValue(self, false);
                        yield break;
                    } else {
                        if (randomTargetRoom == null) { UnityEngine.Object.Destroy(talkdoer.gameObject); yield break; }
                        IntVector2? RandomPosition = ExpandUtility.GetRandomAvailableCellSmart(randomTargetRoom, new IntVector2(2, 2));
                        if (RandomPosition.HasValue) {
                            srb.transform.position = RandomPosition.Value.ToVector3(srb.transform.position.z);
                        } else {
                            UnityEngine.Object.Destroy(talkdoer.gameObject);
                            yield break;
                        }                        
                        srb.Reinitialize();
                        RoomHandler.unassignedInteractableObjects.Add(talkdoer);
                        field.SetValue(self, false);
                        yield break;
                    }
                } else {
                    UnityEngine.Object.Destroy(talkdoer.gameObject);
                    yield break;
                }
            }
        }
        
        // Prevent Arrival Elevator from departing while room still has active enemies. (currently only relevent to custom Giant Elevator Room)
        // Used to prevent player from going down elevator shaft while there are still enemies to clear.
        public void ArrivalElevatorUpdateHook(Action<ElevatorArrivalController>orig, ElevatorArrivalController self) {
            if (!self.gameObject.transform.position.GetAbsoluteRoom().HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { orig(self); }
        }
        
        public IEnumerator ConstructTK2DDungeonHook(Func<TK2DDungeonAssembler, Dungeon, tk2dTileMap, IEnumerator>orig, TK2DDungeonAssembler self, Dungeon d, tk2dTileMap map) {
            for (int j = 0; j < d.data.Width; j++) {
                for (int k = 0; k < d.data.Height; k++) {
                    try {
                        self.BuildTileIndicesForCell(d, map, j, k);
                    } catch (Exception ex) {
                        if (ExpandSettings.debugMode) {
                            ETGModConsole.Log("[DEBUG] Exception caught in TK2DDungeonAssembler.ConstructTK2DDungeonHook at TK2DDungeonAssembler.BuildTileIndicesForCell!");
                        }
                        Debug.Log("Exception caught in TK2DDungeonAssembler.ConstructTK2DDungeonHook at TK2DDungeonAssembler.BuildTileIndicesForCell!");
                        Debug.LogException(ex);
                    }
                }
            }

            yield return null;

            TileIndices tileIndices = ReflectGetField<TileIndices>(typeof(TK2DDungeonAssembler), "t", self);

            if (d.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON || d.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FINALGEON) {
                for (int l = 0; l < d.data.Width; l++) {
                    for (int m = 0; m < d.data.Height; m++) {
                        CellData cellData = d.data.cellData[l][m];
                        tileIndices = ReflectGetField<TileIndices>(typeof(TK2DDungeonAssembler), "t", self);
                        if (cellData != null) {
                            if (cellData.type == CellType.FLOOR) {
                                InvokeMethod(typeof(TK2DDungeonAssembler), "BuildShadowIndex", self, new object[] { cellData, d, map, l, m });
                            } else if (cellData.type == CellType.PIT) {
                                InvokeMethod(typeof(TK2DDungeonAssembler), "BuildPitShadowIndex", self, new object[] { cellData, d, map, l, m });
                            }
                        }
                    }
                }
            }
            TK2DInteriorDecorator decorator = new TK2DInteriorDecorator(self);
            decorator.PlaceLightDecoration(d, map);
            for (int i = 0; i < d.data.rooms.Count; i++) {
                if (d.data.rooms[i].area.prototypeRoom == null || d.data.rooms[i].area.prototypeRoom.usesProceduralDecoration) {
                    decorator.HandleRoomDecoration(d.data.rooms[i], d, map);
                } else {
                    decorator.HandleRoomDecorationMinimal(d.data.rooms[i], d, map);
                }
                if (i % 5 == 0) { yield return null; }
            }
            if ((d.data.rooms.Count - 1) % 5 != 0) { yield return null; }
            tileIndices = ReflectGetField<TileIndices>(typeof(TK2DDungeonAssembler), "t", self);
            map.Editor__SpriteCollection = tileIndices.dungeonCollection;
            if (d.ActuallyGenerateTilemap) {
                IEnumerator BuildTracker = map.DeferredBuild(tk2dTileMap.BuildFlags.Default);
                while (BuildTracker.MoveNext()) { yield return null; }
            }
            yield break;
        }


        // Make the HellDragZone thing actually take player to direct to bullet hell instead of using normal DelayedLoadNextLevel().
        // Since if the EndTimes room is loaded from a different level other then Forge, this could cause issues. :P
        private IEnumerator HandleGrabbyGrabHook(Func<HellDragZoneController, PlayerController, IEnumerator>orig, HellDragZoneController self, PlayerController grabbedPlayer) {
            FsmBool m_cryoBool = ReflectionHelpers.ReflectGetField<FsmBool>(typeof(HellDragZoneController), "m_cryoBool", self);
            grabbedPlayer.specRigidbody.Velocity = Vector2.zero;
            grabbedPlayer.specRigidbody.CapVelocity = true;
            grabbedPlayer.specRigidbody.MaxVelocity = Vector2.zero;
            yield return new WaitForSeconds(0.2f);
            grabbedPlayer.IsVisible = false;
            yield return new WaitForSeconds(2.3f);
            grabbedPlayer.specRigidbody.CapVelocity = false;
            Pixelator.Instance.FadeToBlack(0.5f, false, 0f);
            if (m_cryoBool != null && m_cryoBool.Value) {
                AkSoundEngine.PostEvent("Stop_MUS_All", self.gameObject);
                GameManager.DoMidgameSave(GlobalDungeonData.ValidTilesets.HELLGEON);
                float delay = 0.6f;
                GameManager.Instance.DelayedLoadCharacterSelect(delay, true, true);
            } else {
                GameManager.DoMidgameSave(GlobalDungeonData.ValidTilesets.HELLGEON);
                GameManager.Instance.DelayedLoadCustomLevel(0.5f, "tt_bullethell");
            }
            yield break;
        }

        public void DungeonStartHook(Action<ItemDB>orig, ItemDB self) {
            List<WeightedGameObject> collection;
            if (self.ModLootPerFloor.TryGetValue("ANY", out collection)) {
                GameManager.Instance.Dungeon.baseChestContents.defaultItemDrops.elements.AddRange(collection);
            }
            string dungeonFloorName = GameManager.Instance.Dungeon.DungeonFloorName;
            string key = string.Empty;
            try {
                key = dungeonFloorName.Substring(1, dungeonFloorName.IndexOf('_') - 1);
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("WARNING: dungoenFloorname.SubString() returned a negative or 0 length value!"); }
                Debug.Log("WARNING: dungoenFloorname.SubString() returned a negative or 0 length value!");
                Debug.LogException(ex);
                if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) { key = "TEST"; } else { key = "???"; }
            }
            if (self.ModLootPerFloor.TryGetValue(key, out collection)) {
                GameManager.Instance.Dungeon.baseChestContents.defaultItemDrops.elements.AddRange(collection);
            }
        }        
        
        // Fix Pit size + make sure it only creates pit on the special room from Catacombs. Creating pit under all instances of sell pit makes selling items difficult post FTA update.
        public void SellPitConfigureOnPlacementHook(Action<SellCellController, RoomHandler>orig, SellCellController self, RoomHandler room) {
            if (room != null && room.GetRoomName().StartsWith("SubShop_SellCreep_CatacombsSpecial")) {
                for (int X = 0; X < self.GetWidth(); X++) {
                    for (int Y = 0; Y < self.GetHeight(); Y++) {
                        IntVector2 intVector = self.transform.position.IntXY(VectorConversions.Round) + new IntVector2(X, Y);
                        if (GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(intVector)) {
                            CellData cellData = GameManager.Instance.Dungeon.data[intVector];
                            cellData.type = CellType.PIT;
                            cellData.fallingPrevented = true;
                        }
                    }
                }
            }
        }
        
        // Buff Disarming Personality Item. Players holding the item can now shoot in Bello's shop without causing him to get angry/vanish.
        // (does not prevent getting caught stealing though. :P )
        private void PlayerFiredHook(Action<BaseShopController>orig, BaseShopController self){
            foreach (PlayerController player in GameManager.Instance.AllPlayers) {
                if (player && player.IsFiring && !player.HasPassiveItem(187)) { orig(self); }
            }
        }

        private static GameObject GenerateRoomDoorMeshHook(RuntimeExitDefinition exit, RoomHandler room, DungeonData dungeonData) {
            try {
                DungeonData.Direction directionFromRoom = exit.GetDirectionFromRoom(room);
                IntVector2 intVector;
                if (exit.upstreamRoom == room) {
                    intVector = exit.GetDownstreamBasePosition();
                } else {
                    intVector = exit.GetUpstreamBasePosition();
                }
                DungeonData.Direction exitDirection = directionFromRoom;
                IntVector2 exitBasePosition = intVector;         
                if (!string.IsNullOrEmpty(room.GetRoomName())) {
                    if (room.GetRoomName().StartsWith(ExpandRoomPrefabs.Expand_GlitchedSecret.name)) {
                        IntVector2 TilePositionOffset = (exitBasePosition + new IntVector2(1, 0));
                        IntVector2 TilePlacerSize = new IntVector2(8, 4);
                        int OffsetAmount = 1;
                        int OffsetAmount2 = 3;
                        if (exitDirection == DungeonData.Direction.NORTH) {
                            TilePositionOffset += new IntVector2(0, OffsetAmount);
                        } else if (exitDirection == DungeonData.Direction.SOUTH) {
                            TilePositionOffset += new IntVector2(0, OffsetAmount);
                        } else if (exitDirection == DungeonData.Direction.EAST) {
                            TilePositionOffset += new IntVector2(0, OffsetAmount2);
                            TilePlacerSize = new IntVector2(4, 8);
                        } else {
                            TilePositionOffset += new IntVector2(0, OffsetAmount2);
                            TilePositionOffset -= new IntVector2(OffsetAmount, 0);
                            TilePlacerSize = new IntVector2(4, 8);
                        }
                        GameObject m_SecretWallMesh = ExpandUtility.GenerateWallMesh(exitDirection, exitBasePosition, "secret room door object", dungeonData, false, true);
                        ExpandUtility.GenerateCorruptedTilesAtPosition(GameManager.Instance.Dungeon, exit.upstreamRoom, TilePositionOffset, TilePlacerSize, m_SecretWallMesh, 0.55f);
                        return m_SecretWallMesh;
                    } else {
                        return SecretRoomBuilder.GenerateWallMesh(exitDirection, exitBasePosition, "secret room door object", dungeonData, false);
                    }
                } else {
                    return SecretRoomBuilder.GenerateWallMesh(exitDirection, exitBasePosition, "secret room door object", dungeonData, false);
                }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[ExpandTheGungeon] Warning: Exception caught in SecretRoomBuilder.GenerateRoomDoorMesh!");
                    Debug.Log(ex);
                }
                return new GameObject("NULL Secret Room Door") { layer = 0 };
            }
        }
        
        private void AnimationCompletedHook(Action<AIAnimator, tk2dSpriteAnimator, tk2dSpriteAnimationClip> orig, AIAnimator self, tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip) {
            try {
                orig(self, animator, clip);
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[ExpandTheGungeon] Warning: Exception caught in AIAnimator.AnimationCompleted on object '" + self.gameObject.name + "'!");
                    Debug.Log("[ExpandTheGungeon] Warning: Exception caught in AIAnimator.AnimationCompleted on object '" + self.gameObject.name + "'!");
                    Debug.LogException(ex);
                }
                return;
            }
        }

        private void TeardownPunchout_Hook(Action<PunchoutController> orig, PunchoutController self) {
            if (!GameStatsManager.Instance.IsRainbowRun && !ExpandSettings.PlayingPunchoutArcade) { orig(self); return; }
            
            if (ReflectGetField<bool>(typeof(PunchoutController), "m_isInitialized", self)) {
                Minimap.Instance.TemporarilyPreventMinimap = false;
                GameUIRoot.Instance.ShowCoreUI("punchout");
                GameUIRoot.Instance.ShowCoreUI(string.Empty);
                GameUIRoot.Instance.ToggleLowerPanels(true, false, string.Empty);
                CameraController mainCameraController = GameManager.Instance.MainCameraController;
                mainCameraController.SetManualControl(false, false);
                mainCameraController.LockToRoom = false;
                mainCameraController.SetZoomScaleImmediate(1f);
                foreach (PlayerController playerController in GameManager.Instance.AllPlayers) {
                    playerController.ClearInputOverride("punchout");
                    playerController.healthHaver.IsVulnerable = true;
                    playerController.SuppressEffectUpdates = false;
                    playerController.IsOnFire = false;
                    playerController.CurrentFireMeterValue = 0f;
                    playerController.CurrentPoisonMeterValue = 0f;
                }
                GameManager.Instance.DungeonMusicController.EndBossMusic();
                if (!ExpandSettings.PlayingPunchoutArcade) {
                    MetalGearRatRoomController metalGearRatRoomController = UnityEngine.Object.FindObjectOfType<MetalGearRatRoomController>();
                    if (metalGearRatRoomController) {
                        GameObject gameObject = PickupObjectDatabase.GetById(GlobalItemIds.RatKey).gameObject;
                        Vector3 position = metalGearRatRoomController.transform.position;
                        if (self.Opponent.NumKeysDropped >= 1) {
                            LootEngine.SpawnItem(gameObject, position + new Vector3(14.25f, 17f), Vector2.zero, 0f, true, false, false);
                        }
                        if (self.Opponent.NumKeysDropped >= 2) {
                            LootEngine.SpawnItem(gameObject, position + new Vector3(13.25f, 14.5f), Vector2.zero, 0f, true, false, false);
                        }
                        if (self.Opponent.NumKeysDropped >= 3) {
                            LootEngine.SpawnItem(gameObject, position + new Vector3(14.25f, 12f), Vector2.zero, 0f, true, false, false);
                        }
                        if (self.Opponent.NumKeysDropped >= 4) {
                            LootEngine.SpawnItem(gameObject, position + new Vector3(30.25f, 17f), Vector2.zero, 0f, true, false, false);
                        }
                        if (self.Opponent.NumKeysDropped >= 5) {
                            LootEngine.SpawnItem(gameObject, position + new Vector3(31.25f, 14.5f), Vector2.zero, 0f, true, false, false);
                        }
                        if (self.Opponent.NumKeysDropped >= 6) {
                            LootEngine.SpawnItem(gameObject, position + new Vector3(30.25f, 12f), Vector2.zero, 0f, true, false, false);
                        }
                        Vector2 a = position + new Vector3(22.25f, 14.5f);

                        List<int> m_AllowedItemsInRainbowMode = new List<int>() {
                            GlobalItemIds.SmallHeart,
                            GlobalItemIds.FullHeart,
                            GlobalItemIds.AmmoPickup,
                            GlobalItemIds.SpreadAmmoPickup,
                            GlobalItemIds.Spice,
                            GlobalItemIds.Junk,
                            GlobalItemIds.GoldJunk,
                            GlobalItemIds.Key,
                            GlobalItemIds.GlassGuonStone,
                            GlobalItemIds.Junk,
                            GlobalItemIds.GoldJunk,
                            GlobalItemIds.SackKnightBoon,
                            GlobalItemIds.Blank,
                            GlobalItemIds.RatKey,
                            GlobalItemIds.Map,
                            120, // armor
                        };

                        foreach (int id in self.Opponent.DroppedRewardIds) {
                            float degrees = ((!BraveUtility.RandomBool()) ? 180 : 0) + UnityEngine.Random.Range(-30f, 30f);
                            // RoomHandler m_room = self.gameObject.transform.position.GetAbsoluteRoom();
                            RoomHandler m_room = GameManager.Instance.PrimaryPlayer.transform.position.GetAbsoluteRoom();                        
                            if (m_AllowedItemsInRainbowMode.Contains(id)) {
                                GameObject gameObject2 = PickupObjectDatabase.GetById(id).gameObject;
                                LootEngine.SpawnItem(gameObject2, (a + new Vector2(11f, 0f).Rotate(degrees)), Vector2.zero, 0f, true, false, false);
                            } else {
                                if (m_room != null && GameManager.Instance.RewardManager.BowlerNoteOtherSource) {
                                    string[] CustomText = new string[] {
                                        "Doesn't look like this rat stole this from a {wb}Rainbow Chest{w}.\n\nNo RAAAAAIIIINBOW, no item!\n\n{wb}-Bowler{w}",
                                        "Rats are GROOOOOOSS!\n\n{wb}-Bowler{w}"
                                    };
                                    ExpandUtility.SpawnCustomBowlerNote(GameManager.Instance.RewardManager.BowlerNoteOtherSource, (a + new Vector2(11f, 0f).Rotate(degrees)), m_room, BraveUtility.RandomElement(CustomText), false);
                                }
                            }
                        }
                    }
                } else {
                    if (self.Player.state is PunchoutPlayerController.WinState) { ExpandPunchoutArcadeController.WonRatGame = true; }
                    ExpandPunchoutArcadeController.RatKeyCount = self.Opponent.NumKeysDropped;
                }
                ExpandSettings.PlayingPunchoutArcade = false;
                GameStatsManager.Instance.SetFlag(GungeonFlags.ITEMSPECIFIC_BOXING_GLOVE, true);
                BraveTime.ClearMultiplier(self.Player.gameObject);
                UnityEngine.Object.Destroy(self.gameObject);
            } else {
                self.Reset();                
            }            
        }
        
        private void BuildOcclusionLayerCenterJungleHook(TK2DDungeonAssembler self, CellData current, Dungeon d, tk2dTileMap map, int ix, int iy) {
            if (current == null | !d | !map) { return; }

            if (!IsValidJungleOcclusionCell(self, current, d, ix, iy)) { return; }
            bool flag = true;
            bool flag2 = true;
            if (!self.BCheck(d, ix, iy)) { flag = false; flag2 = false; }
            if (current.UniqueHash < 0.05f) { flag = false; flag2 = false; }
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (!IsValidJungleOcclusionCell(self, d.data[ix + i, iy + j], d, ix + i, iy + j)) {
                        flag2 = false;
                        if (i < 2 || j < 2) { flag = false; }
                    }
                    if (!flag2 && !flag) { break; }
                }
                if (!flag2 && !flag) { break; }
            }
            if (flag2 && current.UniqueHash < 0.75f) {
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, 352);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y, 353);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 2, current.positionInTilemap.y, 354);
                d.data[ix + 1, iy].cellVisualData.occlusionHasBeenProcessed = true;
                d.data[ix + 2, iy].cellVisualData.occlusionHasBeenProcessed = true;
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, 330);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y + 1, 331);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 2, current.positionInTilemap.y + 1, 332);
                d.data[ix, iy + 1].cellVisualData.occlusionHasBeenProcessed = true;
                d.data[ix + 1, iy + 1].cellVisualData.occlusionHasBeenProcessed = true;
                d.data[ix + 2, iy + 1].cellVisualData.occlusionHasBeenProcessed = true;
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 2, 308);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y + 2, 309);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 2, current.positionInTilemap.y + 2, 310);
                d.data[ix, iy + 2].cellVisualData.occlusionHasBeenProcessed = true;
                d.data[ix + 1, iy + 2].cellVisualData.occlusionHasBeenProcessed = true;
                d.data[ix + 2, iy + 2].cellVisualData.occlusionHasBeenProcessed = true;
            } else if (flag) {
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, 418);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y, 419);
                d.data[ix + 1, iy].cellVisualData.occlusionHasBeenProcessed = true;
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, 396);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y + 1, 397);
                d.data[ix, iy + 1].cellVisualData.occlusionHasBeenProcessed = true;
                d.data[ix + 1, iy + 1].cellVisualData.occlusionHasBeenProcessed = true;
            } else {
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, 374);
            }
            d.data[ix, iy].cellVisualData.occlusionHasBeenProcessed = true;
        }

        private void BuildBorderLayerCenterJungleHook(TK2DDungeonAssembler self, CellData current, Dungeon d, tk2dTileMap map, int ix, int iy) {
            if (current == null | !d | !map) { return; }

            if (!IsValidJungleBorderCell(current, d, ix, iy)) { return; }
            bool flag = true;
            bool flag2 = true;
            if (!self.BCheck(d, ix, iy)) { flag = false; flag2 = false; }
            if (current.UniqueHash < 0.05f) { flag = false; flag2 = false; }
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (!IsValidJungleBorderCell(d.data[ix + i, iy + j], d, ix + i, iy + j)) {
                        flag2 = false;
                        if (i < 2 || j < 2) { flag = false; }
                    }
                    if (!flag2 && !flag) { break; }
                }
                if (!flag2 && !flag) { break; }
            }
            if (flag2 && current.UniqueHash < 0.75f) {
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, 352);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, 352);
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y, 353);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y, 353);
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x + 2, current.positionInTilemap.y, 354);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 2, current.positionInTilemap.y, 354);
                d.data[ix + 1, iy].cellVisualData.ceilingHasBeenProcessed = true;
                d.data[ix + 2, iy].cellVisualData.ceilingHasBeenProcessed = true;
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, 330);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, 330);
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y + 1, 331);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y + 1, 331);
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x + 2, current.positionInTilemap.y + 1, 332);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 2, current.positionInTilemap.y + 1, 332);
                d.data[ix, iy + 1].cellVisualData.ceilingHasBeenProcessed = true;
                d.data[ix + 1, iy + 1].cellVisualData.ceilingHasBeenProcessed = true;
                d.data[ix + 2, iy + 1].cellVisualData.ceilingHasBeenProcessed = true;
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 2, 308);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 2, 308);
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y + 2, 309);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y + 2, 309);
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x + 2, current.positionInTilemap.y + 2, 310);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 2, current.positionInTilemap.y + 2, 310);
                d.data[ix, iy + 2].cellVisualData.ceilingHasBeenProcessed = true;
                d.data[ix + 1, iy + 2].cellVisualData.ceilingHasBeenProcessed = true;
                d.data[ix + 2, iy + 2].cellVisualData.ceilingHasBeenProcessed = true;
            } else if (flag) {
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, 418);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, 418);
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y, 419);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y, 419);
                d.data[ix + 1, iy].cellVisualData.ceilingHasBeenProcessed = true;
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, 396);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, 396);
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y + 1, 397);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x + 1, current.positionInTilemap.y + 1, 397);
                d.data[ix, iy + 1].cellVisualData.ceilingHasBeenProcessed = true;
                d.data[ix + 1, iy + 1].cellVisualData.ceilingHasBeenProcessed = true;
            } else {
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, 374);
                map.Layers[GlobalDungeonData.occlusionPartitionIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, 374);
            }
            d.data[ix, iy].cellVisualData.ceilingHasBeenProcessed = true;
        }
        
        private bool IsValidJungleBorderCell(CellData current, Dungeon d, int ix, int iy) {
            bool isValid = false;
            try {
                isValid = !current.cellVisualData.ceilingHasBeenProcessed && !IsCardinalBorder(current, d, ix, iy) && current.type == CellType.WALL && (iy < 2 || !d.data.isFaceWallLower(ix, iy)) && !d.data.isTopDiagonalWall(ix, iy);
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    Debug.Log("[ExpandTheGungeon] Excpetion caught in TK2DDungeonAssembler.IsValidJungleBorderCell!");
                    Debug.LogException(ex);
                }
                return false;
            }
            return isValid;
        }

        private bool IsValidJungleOcclusionCell(TK2DDungeonAssembler assembler, CellData current, Dungeon d, int ix, int iy) {
            bool isValid = false;
            try {
                isValid = assembler.BCheck(d, ix, iy, 1) && (!current.cellVisualData.ceilingHasBeenProcessed && !current.cellVisualData.occlusionHasBeenProcessed) && (current.type != CellType.WALL || IsCardinalBorder(current, d, ix, iy) || (iy > 2 && (d.data.isFaceWallLower(ix, iy) || d.data.isFaceWallHigher(ix, iy))));
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    Debug.Log("[ExpandTheGungeon] Excpetion caught in TK2DDungeonAssembler.IsValidJungleOcclusionCell!");
                    Debug.LogException(ex);
                }
                return false;
            }
            return isValid;
        }

        private bool IsCardinalBorder(CellData current, Dungeon d, int ix, int iy) {
            bool flag = d.data.isTopWall(ix, iy);
            flag = (flag && !d.data[ix, iy + 1].cellVisualData.shouldIgnoreBorders);
            bool flag2 = (!d.data.isWallRight(ix, iy) && !d.data.isRightTopWall(ix, iy)) || d.data.isFaceWallHigher(ix + 1, iy) || d.data.isFaceWallLower(ix + 1, iy);
            flag2 = (flag2 && !d.data[ix + 1, iy].cellVisualData.shouldIgnoreBorders);
            bool flag3 = iy > 3 && d.data.isFaceWallHigher(ix, iy - 1);
            flag3 = (flag3 && !d.data[ix, iy - 1].cellVisualData.shouldIgnoreBorders);
            bool flag4 = (!d.data.isWallLeft(ix, iy) && !d.data.isLeftTopWall(ix, iy)) || d.data.isFaceWallHigher(ix - 1, iy) || d.data.isFaceWallLower(ix - 1, iy);
            flag4 = (flag4 && !d.data[ix - 1, iy].cellVisualData.shouldIgnoreBorders);
            return flag || flag2 || flag3 || flag4;
        }
        
        private TileIndexGrid GetTypeBorderGridForBorderIndexHook(TK2DDungeonAssembler self, CellData current, Dungeon d, out int usedVisualType) {
            TileIndexGrid roomCeilingBorderGrid;

            try {
                roomCeilingBorderGrid = d.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].roomCeilingBorderGrid;
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    Debug.Log("[ExpandTheGungeon] [WARNING] Exception caught in TK2DDungeonAssembler.GetTypeBorderGridForBorderIndex !");
                    Debug.LogException(ex);
                }
                roomCeilingBorderGrid = null;
                usedVisualType = 0;
                return null;
            }
            usedVisualType = current.cellVisualData.roomVisualTypeIndex;
            
            if (d.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                if (current.nearestRoom != null && current.distanceFromNearestRoom < 4f) {
                    if (current.cellVisualData.IsFacewallForInteriorTransition) {
                        roomCeilingBorderGrid = d.roomMaterialDefinitions[current.cellVisualData.InteriorTransitionIndex].roomCeilingBorderGrid;
                        usedVisualType = current.cellVisualData.InteriorTransitionIndex;
                    } else if (!current.cellVisualData.IsFeatureCell) {
                        int? VisualSubType = current.nearestRoom.RoomVisualSubtype;
                        if (VisualSubType.HasValue) {
                            roomCeilingBorderGrid = d.roomMaterialDefinitions[VisualSubType.Value].roomCeilingBorderGrid;
                            usedVisualType = current.nearestRoom.RoomVisualSubtype;
                        }
                    }
                }
            } else if (d.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON) {
                roomCeilingBorderGrid = d.roomMaterialDefinitions[current.nearestRoom.RoomVisualSubtype].roomCeilingBorderGrid;
                usedVisualType = current.nearestRoom.RoomVisualSubtype;
            }
            if (roomCeilingBorderGrid == null) {
                roomCeilingBorderGrid = d.roomMaterialDefinitions[0].roomCeilingBorderGrid;
                usedVisualType = 0;
            }
            return roomCeilingBorderGrid;
        }

        /*public static uint PostEventHook(Func<string, GameObject, uint> orig, string in_pszEventName, GameObject in_gameObjectID) {
            if (in_pszEventName == "play_OBJ_door_open_01" && GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.BELLYGEON) {
                return AkSoundEngine.PostEvent("Play_EX_BellyDoor_Open", in_gameObjectID);
            } else if (in_pszEventName == "play_OBJ_door_close_01" && GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.BELLYGEON) {
                return AkSoundEngine.PostEvent("Play_EX_BellyDoor_Close", in_gameObjectID);
            } else if (in_pszEventName == "Play_OBJ_gate_slam_01" && GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.BELLYGEON) {
                return AkSoundEngine.PostEvent("Play_EX_BellyDoor_Seal", in_gameObjectID);
            } else if (in_pszEventName == "Play_OBJ_gate_open_01" && GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.BELLYGEON) {
                return AkSoundEngine.PostEvent("Play_EX_BellyDoor_Unseal", in_gameObjectID);
            }
            if (!string.IsNullOrEmpty(in_pszEventName) && in_pszEventName == "Stop_MUS_All") { AkSoundEngine.PostEvent("Stop_EX_MUS_All", in_gameObjectID); }
            return orig(in_pszEventName, in_gameObjectID);
        }*/
        
        public void PostGenerationCleanupHook(Action<DungeonData>orig, DungeonData self) {
            try {
                orig(self);
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[ExpandTheGungeon] [Warning] Exception caught at DungeonData.PostGenerationCleanup!");
                    Debug.Log("[ExpandTheGungeon] [Warning] Exception caught at DungeonData.PostGenerationCleanup!");
                    Debug.LogException(ex);
                    return;
                }
            }
        }

        private IEnumerator HandleBulletDeletionFrames(Func<Exploder, Vector3, float, float, IEnumerator>orig, Exploder self, Vector3 centerPosition, float bulletDeletionSqrRadius, float duration) {
            float elapsed = 0f;
            /*if (GameManager.HasInstance && GameManager.Instance.Dungeon) {
                Dungeon dungeon = GameManager.Instance.Dungeon;
                bulletDeletionSqrRadius *= Mathf.InverseLerp(0.66f, 1f, dungeon.ExplosionBulletDeletionMultiplier);
                if (!dungeon.IsExplosionBulletDeletionRecovering) {
                    dungeon.ExplosionBulletDeletionMultiplier = Mathf.Clamp01(dungeon.ExplosionBulletDeletionMultiplier - 0.8f);
                }
                if (bulletDeletionSqrRadius <= 0f) { yield break; }
            }*/
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                ReadOnlyCollection<Projectile> allProjectiles = StaticReferenceManager.AllProjectiles;
                for (int i = allProjectiles.Count - 1; i >= 0; i--) {
                    Projectile projectile = allProjectiles[i];
                    if (projectile) {
                        if (!(projectile.Owner is PlayerController)) {
                            Vector2 vector = (projectile.transform.position - centerPosition).XY();
                            if (projectile.CanBeKilledByExplosions && vector.sqrMagnitude < bulletDeletionSqrRadius) {
                                projectile.DieInAir(false, true, true, false);
                            }
                        }
                    }
                }
                List<BasicTrapController> allTraps = StaticReferenceManager.AllTriggeredTraps;
                for (int j = allTraps.Count - 1; j >= 0; j--) {
                    BasicTrapController basicTrapController = allTraps[j];
                    if (basicTrapController && basicTrapController.triggerOnBlank) {
                        float sqrMagnitude = (basicTrapController.CenterPoint() - centerPosition.XY()).sqrMagnitude;
                        if (sqrMagnitude < bulletDeletionSqrRadius) { basicTrapController.Trigger(); }
                    }
                }
                yield return null;
            }
            yield break;
        }

        private void ThrowGunHook(Action<Gun>orig, Gun self) {
            orig(self);
            if (!self.renderer.enabled) { self.renderer.enabled = true; }
            if (self.sprite && !self.sprite.renderer.enabled) { self.sprite.renderer.enabled = true; }
        }

        private void StartHook(Action<CharacterCostumeSwapper> orig, CharacterCostumeSwapper self) {
            orig(self);

            if (self.TargetCharacter == PlayableCharacters.Bullet) {
                FieldInfo m_active = typeof(CharacterCostumeSwapper).GetField("m_active", BindingFlags.NonPublic | BindingFlags.Instance);

                bool killedPast = GameStatsManager.Instance.GetCharacterSpecificFlag(self.TargetCharacter, CharacterSpecificGungeonFlags.KILLED_PAST);

                if (self.HasCustomTrigger) {
                    if (self.CustomTriggerIsFlag) {
                        killedPast = GameStatsManager.Instance.GetFlag(self.TriggerFlag);
                    } else if (self.CustomTriggerIsSpecialReserve) {
                        killedPast = (GameStatsManager.Instance.GetFlag(GungeonFlags.SECRET_BULLETMAN_SEEN_05));
                    }
                }

                m_active.SetValue(self, killedPast);
                self.AlternateCostumeSprite.renderer.enabled = killedPast;
                self.CostumeSprite.renderer.enabled = false;
            }
        }

        public GlobalDungeonData.ValidTilesets GetNextTileset(GameManager orig, GlobalDungeonData.ValidTilesets tilesetID) {
            switch (tilesetID) {
                case GlobalDungeonData.ValidTilesets.GUNGEON:
                    return GlobalDungeonData.ValidTilesets.MINEGEON;
                case GlobalDungeonData.ValidTilesets.CASTLEGEON:
                    return GlobalDungeonData.ValidTilesets.GUNGEON;
                case GlobalDungeonData.ValidTilesets.SEWERGEON:
                    return GlobalDungeonData.ValidTilesets.GUNGEON;
                case GlobalDungeonData.ValidTilesets.JUNGLEGEON:
                    return GlobalDungeonData.ValidTilesets.GUNGEON;
                case GlobalDungeonData.ValidTilesets.CATHEDRALGEON:
                    return GlobalDungeonData.ValidTilesets.MINEGEON;
                case GlobalDungeonData.ValidTilesets.BELLYGEON:
                    return GlobalDungeonData.ValidTilesets.MINEGEON;
                case GlobalDungeonData.ValidTilesets.WESTGEON:
                    return GlobalDungeonData.ValidTilesets.FORGEGEON;
                default:
                    if (tilesetID == GlobalDungeonData.ValidTilesets.MINEGEON) { return GlobalDungeonData.ValidTilesets.CATACOMBGEON; }
                    if (tilesetID == GlobalDungeonData.ValidTilesets.CATACOMBGEON) { return GlobalDungeonData.ValidTilesets.FORGEGEON; }
                    if (tilesetID == GlobalDungeonData.ValidTilesets.FORGEGEON) { return GlobalDungeonData.ValidTilesets.HELLGEON; }
                    if (tilesetID == GlobalDungeonData.ValidTilesets.OFFICEGEON) { return GlobalDungeonData.ValidTilesets.FORGEGEON; }
                    if (tilesetID != GlobalDungeonData.ValidTilesets.RATGEON) { return GlobalDungeonData.ValidTilesets.CASTLEGEON; }
                    return GlobalDungeonData.ValidTilesets.CATACOMBGEON;
            }
            // return (orig(tilesetID));
        }

        private void OnContinueGameSelected(Action<MainMenuFoyerController, dfControl, dfMouseEventArgs> orig, MainMenuFoyerController self, dfControl control, dfMouseEventArgs mouseEvent) {
            MidGameSaveData.ContinuePressedDevice = InputManager.ActiveDevice;
            bool m_faded = ReflectGetField<bool>(typeof(MainMenuFoyerController), "m_faded", self);
            bool m_wasFadedThisFrame = ReflectGetField<bool>(typeof(MainMenuFoyerController), "m_wasFadedThisFrame", self);
            if (m_faded || m_wasFadedThisFrame) { return; }
            if (!IsDioramaRevealed(self, true)) { return; }
            if (!Foyer.DoMainMenu) { return; }
            MidGameSaveData midGameSaveData = null;
            GameManager.VerifyAndLoadMidgameSave(out midGameSaveData, true);
            Dungeon.ShouldAttemptToLoadFromMidgameSave = true;
            self.DisableMainMenu();
            Pixelator.Instance.FadeToBlack(0.15f, false, 0.05f);
            GameManager.Instance.FlushAudio();
            AkSoundEngine.PostEvent("Play_UI_menu_confirm_01", self.gameObject);
            GameManager.Instance.SetNextLevelIndex(GetTargetLevelIndexFromSavedTileset(midGameSaveData.levelSaved));
            GameManager.Instance.GeneratePlayersFromMidGameSave(midGameSaveData);
            GameManager.Instance.IsFoyer = false;
            Foyer.DoIntroSequence = false;
            Foyer.DoMainMenu = false;
            GameManager.Instance.IsSelectingCharacter = false;
            DelayedLoadMidgameSave(GameManager.Instance, 0.25f, midGameSaveData);
        }

        // Fix for allowing custom floors with the unused tilesets to load from midgame saves correctly instead of game defaulting to floor 1.
        public void DelayedLoadMidgameSave(GameManager self, float delay, MidGameSaveData saveToContinue) {
            GlobalDungeonData.ValidTilesets levelSaved = saveToContinue.levelSaved;
            string destinationLevel = "tt_castle";
            bool isPast = false;
            bool isBaseFloor = false;

            switch (levelSaved) {
                case GlobalDungeonData.ValidTilesets.CASTLEGEON:
                    isBaseFloor = true;
                    break;
                case GlobalDungeonData.ValidTilesets.SEWERGEON:
                    destinationLevel = "tt_sewer";
                    break;
                case GlobalDungeonData.ValidTilesets.JUNGLEGEON:
                    destinationLevel = "tt_jungle";
                    break;
                case GlobalDungeonData.ValidTilesets.GUNGEON:
                    isBaseFloor = true;
                    break;
                case GlobalDungeonData.ValidTilesets.CATHEDRALGEON:
                    destinationLevel = "tt_cathedral";
                    break;
                case GlobalDungeonData.ValidTilesets.BELLYGEON:
                    destinationLevel = "tt_belly";
                    break;
                case GlobalDungeonData.ValidTilesets.MINEGEON:
                    isBaseFloor = true;
                    break;
                case GlobalDungeonData.ValidTilesets.RATGEON:
                    destinationLevel = "ss_resourcefulrat";
                    break;
                case GlobalDungeonData.ValidTilesets.CATACOMBGEON:
                    isBaseFloor = true;
                    break;
                case GlobalDungeonData.ValidTilesets.OFFICEGEON:
                    destinationLevel = "tt_nakatomi";
                    break;
                case GlobalDungeonData.ValidTilesets.WESTGEON:
                    destinationLevel = "tt_west";
                    break;
                case GlobalDungeonData.ValidTilesets.FORGEGEON:
                    isBaseFloor = true;
                    break;
                case GlobalDungeonData.ValidTilesets.HELLGEON:
                    isBaseFloor = true;
                    break;
                case GlobalDungeonData.ValidTilesets.FINALGEON:
                    isPast = true;
                    break;
                // Additional checks for possible future custom floors from other mods.
                // tt_space doesn't normally exist. If a mod adds this floor they must add new tt_space level definition with correct
                // dungeonSceneName that matches tt_phobos!
                case GlobalDungeonData.ValidTilesets.PHOBOSGEON:
                    destinationLevel = "tt_phobos";
                    break;
                case GlobalDungeonData.ValidTilesets.SPACEGEON:
                    if (self.customFloors != null && self.customFloors.Count > 0) {
                        foreach (GameLevelDefinition levelDefinition in self.customFloors) {
                            if (!string.IsNullOrEmpty(levelDefinition.dungeonSceneName) && levelDefinition.dungeonSceneName.ToLower() == "tt_space") {
                                destinationLevel = "tt_space";
                                break;
                            }
                        }
                    }
                    break;
                default:
                    // If unexpected tileset is selected (which shouldn't be possible), default to normal base floor setting.
                    isBaseFloor = true;
                    break;
            }

            if (isBaseFloor && !isPast) {
                // This code path is if one of the base floors was detected as destination. 
                // Original code didn't set specific destination for these.
                self.DelayedLoadNextLevel(0.25f);
                return;
            } else if (isPast) {
                switch (saveToContinue.playerOneData.CharacterIdentity) {
                    case PlayableCharacters.Pilot:
                        self.DelayedLoadCustomLevel(delay, "fs_pilot");
                        break;
                    case PlayableCharacters.Convict:
                        self.DelayedLoadCustomLevel(delay, "fs_convict");
                        break;
                    case PlayableCharacters.Robot:
                        self.DelayedLoadCustomLevel(delay, "fs_robot");
                        break;
                    case PlayableCharacters.Soldier:
                        self.DelayedLoadCustomLevel(delay, "fs_soldier");
                        break;
                    case PlayableCharacters.Guide:
                        self.DelayedLoadCustomLevel(delay, "fs_guide");
                        break;
                    case PlayableCharacters.Bullet:
                        self.DelayedLoadCustomLevel(delay, "fs_bullet");
                        break;
                    case PlayableCharacters.Gunslinger:
                        GameManager.IsGunslingerPast = true;
                        self.DelayedLoadCustomLevel(delay, "tt_bullethell");
                        break;
                    default:
                        // Default to normal level load if none of the normal character identities was set.
                        self.DelayedLoadNextLevel(0.25f);
                        break;
                }
                return;
            } else if (destinationLevel == "tt_castle") {
                // If custom/secret floor was detected but somehow destinationLevel wasn't changed, the default behavior will be to load next level instead of specific level.
                self.DelayedLoadNextLevel(0.25f);
                return;
            } else {
                // This code path runs only if not past and destination floor is not a base floor. (aka not secret floor/custom floor)
                self.DelayedLoadCustomLevel(delay, destinationLevel);
                return;
            }
        }
        
        private bool IsDioramaRevealed(MainMenuFoyerController self, bool doReveal = false) {
            TitleDioramaController m_tdc = ReflectGetField<TitleDioramaController>(typeof(MainMenuFoyerController), "m_tdc", self);
            FieldInfo m_tdcfield = typeof(MainMenuFoyerController).GetField("m_tdc", BindingFlags.Instance | BindingFlags.NonPublic);
            if (m_tdc == null) {
                m_tdc = UnityEngine.Object.FindObjectOfType<TitleDioramaController>();
                m_tdcfield.SetValue(self, m_tdc);
                m_tdc = ReflectGetField<TitleDioramaController>(typeof(MainMenuFoyerController), "m_tdc", self);
            }
            return !m_tdc || m_tdc.IsRevealed(doReveal);
        }
        
        private IEnumerator FadeToBlack(FinalIntroSequenceManager self, float duration, bool startAtCurrent = false, bool force = false) {
            float elapsed = 0f;
            float startValue = 0f;
            if (startAtCurrent) { startValue = self.FadeMaterial.GetColor("_Color").a; }
            bool m_skipCycle = ReflectionHelpers.ReflectGetField<bool>(typeof(FinalIntroSequenceManager), "m_skipCycle", self);
            while (elapsed < duration) {
                if (!force && m_skipCycle) { yield break; }
                m_skipCycle = ReflectionHelpers.ReflectGetField<bool>(typeof(FinalIntroSequenceManager), "m_skipCycle", self);
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                self.FadeMaterial.SetColor("_Color", new Color(0f, 0f, 0f, Mathf.Lerp(startValue, 1f, t)));
                yield return null;
            }
            self.FadeMaterial.SetColor("_Color", new Color(0f, 0f, 0f, 1f));
            yield break;
        }

        public int GetTargetLevelIndexFromSavedTileset(GlobalDungeonData.ValidTilesets tilesetID) {
            switch (tilesetID) {
                case GlobalDungeonData.ValidTilesets.CASTLEGEON:
                    return 1;
                case GlobalDungeonData.ValidTilesets.SEWERGEON:
                    return 2;
                case GlobalDungeonData.ValidTilesets.JUNGLEGEON:
                    return 2;
                case GlobalDungeonData.ValidTilesets.GUNGEON:
                    return 2;
                case GlobalDungeonData.ValidTilesets.CATHEDRALGEON:
                    return 3;
                case GlobalDungeonData.ValidTilesets.BELLYGEON:
                    return 3;
                case GlobalDungeonData.ValidTilesets.MINEGEON:
                    return 3;
                case GlobalDungeonData.ValidTilesets.RATGEON:
                    return 4;
                case GlobalDungeonData.ValidTilesets.CATACOMBGEON:
                    return 4;
                case GlobalDungeonData.ValidTilesets.OFFICEGEON:
                    return 5;
                case GlobalDungeonData.ValidTilesets.WESTGEON:
                    return 5;
                case GlobalDungeonData.ValidTilesets.FORGEGEON:
                    return 5;
                case GlobalDungeonData.ValidTilesets.HELLGEON:
                    return 6;
                case GlobalDungeonData.ValidTilesets.FINALGEON:
                    return 6;
                case GlobalDungeonData.ValidTilesets.SPACEGEON:
                    return 1;
                case GlobalDungeonData.ValidTilesets.PHOBOSGEON:
                    return 1;
                default:
                    return 1;
            }
        }

        public bool IsWildWestEntranceHook(Func<RoomHandler, bool> orig, RoomHandler self) {
            if (self?.GetRoomName() != null && ExpandRoomPrefabs.Expand_West_Entrance?.name != null) {
                return self.GetRoomName().ToLower().StartsWith(ExpandRoomPrefabs.Expand_West_Entrance.name.ToLower()) || orig(self);
            } else {
                return orig(self);
            }
        }

        private void PlacePlayerInRoomHook(Action<Dungeon, tk2dTileMap, RoomHandler>orig, Dungeon self, tk2dTileMap map, RoomHandler startRoom) {
            PlayerController[] allPlayers = GameManager.Instance.AllPlayers;
            if (allPlayers.Length == 0) { return; }
            int num = (allPlayers.Length >= 2) ? allPlayers.Length : 1;
            for (int i = 0; i < num; i++) {
                PlayerController playerController = (allPlayers.Length >= 2) ? allPlayers[i] : GameManager.Instance.PrimaryPlayer;
                EntranceController entranceController = UnityEngine.Object.FindObjectOfType<EntranceController>();
                ElevatorArrivalController elevatorArrivalController = UnityEngine.Object.FindObjectOfType<ElevatorArrivalController>();
                ExpandNewElevatorController[] exElevatorArrivalControllers = null;
                ExpandNewElevatorController exElevatorArrivalController = null;
                if (!elevatorArrivalController && !GameManager.IsReturningToFoyerWithPlayer) {
                    exElevatorArrivalControllers = UnityEngine.Object.FindObjectsOfType<ExpandNewElevatorController>();
                    if (exElevatorArrivalControllers?.Length > 0) {
                        foreach (ExpandNewElevatorController exElevator in exElevatorArrivalControllers) {
                            if (exElevator.IsArrivalElevator) {
                                exElevatorArrivalController = exElevator;
                                break;
                            }
                        }
                    }
                }
                Vector2 vector = Vector2.zero;
                float num2 = 0.25f;
                if (GameManager.IsReturningToFoyerWithPlayer) {
                    vector = GameObject.Find("ReturnToFoyerPoint").transform.position.XY();
                    vector += Vector2.right * i;
                    playerController.transform.position = vector.ToVector3ZUp(-0.1f);
                    playerController.Reinitialize();
                } else {
                    if (elevatorArrivalController | exElevatorArrivalController) {
                        if (elevatorArrivalController) {
                            vector = elevatorArrivalController.spawnTransform.position.XY();
                            num2 = 1f;
                            elevatorArrivalController.DoArrival(playerController, num2);
                            num2 += 0.4f;
                        } else {
                            vector = exElevatorArrivalController.gameObject.transform.position.XY() + new Vector2(2, 2);
                            num2 = 1f;
                            exElevatorArrivalController.DoArrival(num2);
                            num2 += 0.4f;
                        }
                    } else {
                        if (entranceController) {
                            vector = entranceController.spawnTransform.position.XY();
                            vector += Vector2.right * i;
                            playerController.transform.position = new Vector3(map.transform.position.x + vector.x - 0.5f, map.transform.position.y + vector.y, -0.1f);
                            playerController.Reinitialize();
                            num2 += 0.4f;
                            playerController.DoSpinfallSpawn(num2);
                            goto AbortPlayerSearch;
                        }
                        if (i == 1 && GameObject.Find("SecondaryPlayerSpawnPoint") != null) {
                            vector = GameObject.Find("SecondaryPlayerSpawnPoint").transform.position.XY();
                            vector += Vector2.right * i;
                            playerController.transform.position = vector.ToVector3ZUp(-0.1f);
                            playerController.Reinitialize();
                            goto AbortPlayerSearch;
                        }
                        if (GameObject.Find("PlayerSpawnPoint") != null) {
                            vector = GameObject.Find("PlayerSpawnPoint").transform.position.XY();
                            vector += Vector2.right * i;
                            playerController.transform.position = vector.ToVector3ZUp(-0.1f);
                            playerController.Reinitialize();
                            goto AbortPlayerSearch;
                        }
                        vector = startRoom.GetCenterCell().ToVector2();
                        if (self.data[vector.ToIntVector2(VectorConversions.Round)].type == CellType.WALL || self.data[vector.ToIntVector2(VectorConversions.Round)].type == CellType.PIT) {
                            vector = startRoom.Epicenter.ToVector2();
                        }
                        vector += Vector2.right * i;
                    }
                    Vector3 position = new Vector3(map.transform.position.x + vector.x + 0.5f, map.transform.position.y + vector.y + 0.5f, -0.1f);
                    playerController.transform.position = position;
                    playerController.Reinitialize();
                    playerController.DoInitialFallSpawn(num2);
                }
                AbortPlayerSearch:;
            }
            GameManager.IsReturningToFoyerWithPlayer = false;
            GameManager.Instance.MainCameraController.ForceToPlayerPosition(GameManager.Instance.PrimaryPlayer);
        }

        // Catch exceptions with tilemap collisions in AddCustomRuntimeRoomWithTileSet with certain tileset combinations.
        private void InitNearbyTileCheck(ActionEX<PhysicsEngine, float, float, float, float, tk2dTileMap, IntVector2, float, float, IntVector2, DungeonData> orig, PhysicsEngine self, float worldMinX, float worldMinY, float worldMaxX, float worldMaxY, tk2dTileMap tileMap, IntVector2 pixelColliderDimensions, float positionX, float positionY, IntVector2 pixelsToMove, DungeonData dungeonData) {
            try {
                orig(self, worldMinX, worldMinY, worldMaxX, worldMaxY, tileMap, pixelColliderDimensions, positionX, positionY, pixelsToMove, dungeonData);
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[ExpandTheGungeon] Warning: Exception caught in PhysicsEngine.InitNearbyTileCheck!");
                    Debug.LogException(ex);
                }
                return;
            }
        }
        
        // Catch exceptions with tilemap collisions in AddCustomRuntimeRoomWithTileSet with certain tileset combinations.
        private PhysicsEngine.Tile GetTile(FuncEX<PhysicsEngine, int, int, tk2dTileMap, int, string, DungeonData, PhysicsEngine.Tile> orig, PhysicsEngine self, int x, int y, tk2dTileMap tileMap, int layer, string layerName, DungeonData dungeonData) {
            try {
                PhysicsEngine.Tile tile = orig(self, x, y, tileMap, layer, layerName, dungeonData);
                return tile;
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[ExpandTheGungeon] Warning: Exception caught in PhysicsEngine.GetTile!");
                    Debug.LogException(ex);
                }
                return null;
            }
        }

        public void FloorChestPlacerConfigureOnPlacementHook(Action<FloorChestPlacer, RoomHandler>orig, FloorChestPlacer self, RoomHandler room) {
            if (!self.UseOverrideChest && room.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.REWARD 
                 && UnityEngine.Random.value < 0.15f
               ) {
                Vector2 ChestPosition = self.transform.position.IntXY(VectorConversions.Round).ToVector3();
                GameObject chestOBJ = UnityEngine.Object.Instantiate(BraveUtility.RandomElement(ExpandLists.CustomChests), ChestPosition, Quaternion.identity);
                ExpandFakeChest fakeChest = null;
                if (chestOBJ) { fakeChest = chestOBJ.GetComponent<ExpandFakeChest>(); }
                if (fakeChest) {
                    if (self.CenterChestInRegion) {
                        SpeculativeRigidbody component = fakeChest.gameObject.GetComponent<SpeculativeRigidbody>();
                        if (component) {
                            Vector2 Base = component.UnitCenter - fakeChest.gameObject.transform.position.XY();
                            Vector2 Offset = self.transform.position.XY() + new Vector2(self.xPixelOffset / 16f, self.yPixelOffset / 16f) + new Vector2((float)self.placeableWidth / 2f, (float)self.placeableHeight / 2f);
                            Vector2 Vector = (Offset - Base);
                            fakeChest.gameObject.transform.position = Vector.ToVector3ZisY(0f).Quantize(0.0625f);
                            component.Reinitialize();
                        }
                    }
                    if (fakeChest.chestType == ExpandFakeChest.ChestType.RickRoll | fakeChest.chestType == ExpandFakeChest.ChestType.SurpriseChest) {
                        if (room.area.prototypeRoom != null) {
                            if (room.area.prototypeRoom.roomEvents == null) { room.area.prototypeRoom.roomEvents = new List<RoomEventDefinition>(); }
                            if (!room.area.prototypeRoom.roomEvents.Contains(new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM))) {
                                room.area.prototypeRoom.roomEvents.Add(new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM));
                            }
                            if (!room.area.prototypeRoom.roomEvents.Contains(new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM))) {
                                room.area.prototypeRoom.roomEvents.Add(new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM));
                            }
                        }
                        if (room.area.runtimePrototypeData != null) {
                            if (room.area.runtimePrototypeData.roomEvents == null) { room.area.runtimePrototypeData.roomEvents = new List<RoomEventDefinition>(); }
                            if (!room.area.runtimePrototypeData.roomEvents.Contains(new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM))) {
                                room.area.runtimePrototypeData.roomEvents.Add(new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM));
                            }
                            if (!room.area.runtimePrototypeData.roomEvents.Contains(new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM))) {
                                room.area.runtimePrototypeData.roomEvents.Add(new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM));
                            }
                        }
                    }
                    fakeChest.ConfigureOnPlacement(room);
                    room.RegisterInteractable(fakeChest);
                    UnityEngine.Object.Destroy(self.gameObject);
                    return;
                } else {
                    orig(self, room);
                    return;
                }
            } else {
                orig(self, room);
            }
        }

        public void ApplyBenefit(Action<ShrineBenefit, PlayerController>orig, ShrineBenefit self, PlayerController interactor) {
            float ItemOdds = 0.2f;
            if (self.benefitType == ShrineBenefit.BenefitType.COMPANION && UnityEngine.Random.value < ItemOdds) {
                PickupObject TurkeyItem = null;
                GameStatsManager.Instance.RegisterStatChange(TrackedStats.TIMES_COMPANION_SHRINED, 1f);
                if (GameStatsManager.Instance.GetPlayerStatValue(TrackedStats.TIMES_COMPANION_SHRINED) >= 2f) {
                    GameStatsManager.Instance.SetFlag(GungeonFlags.ITEMSPECIFIC_TURKEY, true);
                    if (GameStatsManager.Instance.GetPlayerStatValue(TrackedStats.TIMES_COMPANION_SHRINED) == 2f) {
                        TurkeyItem = PickupObjectDatabase.GetById(self.TurkeyCompanionForCompanionShrine);
                    }
                }
                if (GameStatsManager.Instance.IsRainbowRun) {
                    LootEngine.SpawnBowlerNote(GameManager.Instance.RewardManager.BowlerNoteOtherSource, interactor.transform.position + new Vector3(0f, -0.5f, 0f), interactor.CurrentRoom, true);
                } else if (TurkeyItem) {
                    LootEngine.GivePrefabToPlayer(TurkeyItem.gameObject, interactor);
                } else {
                    LootEngine.GivePrefabToPlayer(BraveUtility.RandomElement(ExpandLists.CompanionItems.Shuffle()), interactor);
                }
            } else {
                orig(self, interactor);
            }
            return;
        }
    }
}

