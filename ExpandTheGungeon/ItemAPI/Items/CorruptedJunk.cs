using System.Reflection;
using System;
using MonoMod.RuntimeDetour;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandUtilities;
using System.Collections.Generic;
using System.Collections;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandComponents;
using System.Collections.ObjectModel;
using tk2dRuntime.TileMap;
using Pathfinding;

namespace ExpandTheGungeon.ItemAPI {
    
    public class CorruptedJunk : PassiveItem {

        private static string[] spritePaths = new string[] {            
            "ExpandTheGungeon/Textures/Items/Animations/corrupted_poopsack/corrupted_poopsack_01",
            "ExpandTheGungeon/Textures/Items/Animations/corrupted_poopsack/corrupted_poopsack_02",
            "ExpandTheGungeon/Textures/Items/Animations/corrupted_poopsack/corrupted_poopsack_03",
            "ExpandTheGungeon/Textures/Items/Animations/corrupted_poopsack/corrupted_poopsack_04",
            "ExpandTheGungeon/Textures/Items/Animations/corrupted_poopsack/corrupted_poopsack_05",
            "ExpandTheGungeon/Textures/Items/Animations/corrupted_poopsack/corrupted_poopsack_06",
            "ExpandTheGungeon/Textures/Items/Animations/corrupted_poopsack/corrupted_poopsack_07",
            "ExpandTheGungeon/Textures/Items/Animations/corrupted_poopsack/corrupted_poopsack_08",
            "ExpandTheGungeon/Textures/Items/Animations/corrupted_poopsack/corrupted_poopsack_09",
            "ExpandTheGungeon/Textures/Items/Animations/corrupted_poopsack/corrupted_poopsack_10"            
        };

        private static List<string> m_SpriteNames = new List<string> {            
            "corrupted_poopsack_01",
            "corrupted_poopsack_02",
            "corrupted_poopsack_03",
            "corrupted_poopsack_04",
            "corrupted_poopsack_05",
            "corrupted_poopsack_06",
            "corrupted_poopsack_07",
            "corrupted_poopsack_08",
            "corrupted_poopsack_09",
            "corrupted_poopsack_10"            
        };

        // ExpandTheGungeon/Textures/Items/corrupted_poopsack
        private static int firstSpriteID;
        private static int lastSpriteID;

        public static GameObject CorruptedJunkObject;

        public static void Init() {
            
            string itemName = "Corrupted Junk";

            CorruptedJunkObject = new GameObject(itemName);

            CorruptedJunk item = CorruptedJunkObject.AddComponent<CorruptedJunk>();
            ItemBuilder.AddSpriteToObject(itemName, spritePaths[8], CorruptedJunkObject, false);

            string shortDesc = "Next Time... What even is this!?";
            string longDesc = "Just some corrupted junk.\n\nCarrying this around makes you question your sanity...";

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ex");
            // item.quality = ItemQuality.S;
            item.quality = ItemQuality.EXCLUDED;

            firstSpriteID = (item.sprite.Collection.spriteDefinitions.Length - 1);

            for (int i = 0; i < spritePaths.Length; i++) { SpriteBuilder.AddSpriteToCollection(spritePaths[i], item.sprite.Collection); }

            lastSpriteID = (firstSpriteID + 10);

            List<int> m_SpriteIDs = new List<int>();

            for (int i = firstSpriteID; i < lastSpriteID; i++) { m_SpriteIDs.Add(i); }

            ExpandUtility.GenerateSpriteAnimator(item.gameObject, playAutomatically: true);
            ExpandUtility.AddAnimation(item.spriteAnimator, item.sprite.Collection, m_SpriteIDs, "idle", tk2dSpriteAnimationClip.WrapMode.RandomLoop, 20);            
        }
        

        public CorruptedJunk() {
            m_PickedUp = false;

            AllowedEffects = new List<ItemEffectType>() {
                ItemEffectType.BigCash,
                ItemEffectType.ArmorUp,
                ItemEffectType.ManyKeys,
                ItemEffectType.BlanksRUS,
                ItemEffectType.HealthPak,
                ItemEffectType.DrStone,
                ItemEffectType.MuchAmmo
            };
        }

        private bool m_PickedUp;

        public enum ItemEffectType { BigCash, ArmorUp, ManyKeys, BlanksRUS, HealthPak, DrStone, MuchAmmo }

        public List<ItemEffectType> AllowedEffects;

        private ItemEffectType m_SelectedEffect;

        public override void Pickup(PlayerController player) {                        
            base.Pickup(player);
            HandleUIAnimation();
            HandleRandomEffect(player);
            HandleGlitchRoomSpawn(player);
            m_PickedUp = true;
        }

        private void HandleUIAnimation() {

            if (m_PickedUp) { return; }

            MinimapUIController minimapDock = null;

            if (Minimap.Instance) {
                minimapDock = Minimap.Instance.UIMinimap;
            } else {
                minimapDock = FindObjectOfType<MinimapUIController>();
            }
            
            if (minimapDock) {
                List<Tuple<tk2dSprite, PassiveItem>> m_DockItems = ReflectionHelpers.ReflectGetField<List<Tuple<tk2dSprite, PassiveItem>>>(typeof(MinimapUIController), "dockItems", minimapDock);
                
                if (m_DockItems != null && m_DockItems.Count > 0) {
                    for (int i = 0; i < m_DockItems.Count; i++) {
                        if (m_DockItems[i].Second is CorruptedJunk) {
                            if (!m_DockItems[i].First.gameObject.GetComponent<tk2dSpriteAnimator>()) {
                                List<int> m_SpriteIDs = new List<int>();
                                for (int I = firstSpriteID; I < lastSpriteID; I++) { m_SpriteIDs.Add(I); }
                                ExpandUtility.GenerateSpriteAnimator(m_DockItems[i].First.gameObject, playAutomatically: true);
                                ExpandUtility.AddAnimation(m_DockItems[i].First.gameObject.GetComponent<tk2dSpriteAnimator>(), m_DockItems[i].First.Collection, m_SpriteIDs, "idle", tk2dSpriteAnimationClip.WrapMode.RandomLoop, 20);
                            }
                            if (m_DockItems[i].First.spriteAnimator) {
                                if (!m_DockItems[i].First.spriteAnimator.IsPlaying("idle")) {
                                    m_DockItems[i].First.spriteAnimator.Play("idle");
                                }
                            }
                        } else {
                            if (!m_PickedUp && UnityEngine.Random.value <= 0.6f) { ExpandShaders.Instance.ApplyGlitchShader(m_DockItems[i].First); }
                        }
                    }
                }

                List<Tuple<tk2dSprite, PassiveItem>> m_secondaryDockItems = ReflectionHelpers.ReflectGetField<List<Tuple<tk2dSprite, PassiveItem>>>(typeof(MinimapUIController), "secondaryDockItems", minimapDock);

                if (m_secondaryDockItems != null && m_secondaryDockItems.Count > 0) {
                    for (int i = 0; i < m_secondaryDockItems.Count; i++) {
                        if (m_secondaryDockItems[i].Second is CorruptedJunk) {
                            if (!m_secondaryDockItems[i].First.gameObject.GetComponent<tk2dSpriteAnimator>()) {
                                List<int> m_SpriteIDs = new List<int>();
                                for (int I = firstSpriteID; I < lastSpriteID; I++) { m_SpriteIDs.Add(I); }
                                ExpandUtility.GenerateSpriteAnimator(m_secondaryDockItems[i].First.gameObject, playAutomatically: true);
                                ExpandUtility.AddAnimation(m_secondaryDockItems[i].First.gameObject.GetComponent<tk2dSpriteAnimator>(), m_secondaryDockItems[i].First.Collection, m_SpriteIDs, "idle", tk2dSpriteAnimationClip.WrapMode.RandomLoop, 20);
                            }
                            if (m_secondaryDockItems[i].First.spriteAnimator) {
                                if (!m_secondaryDockItems[i].First.spriteAnimator.IsPlaying("idle")) {
                                    m_secondaryDockItems[i].First.spriteAnimator.Play("idle");
                                }
                            }
                        } else {
                            if (!m_PickedUp && UnityEngine.Random.value <= 0.6f) { ExpandShaders.Instance.ApplyGlitchShader(m_secondaryDockItems[i].First); }
                        }
                    }
                }
            }
        }


        private void HandleRandomEffect(PlayerController player) {

            if (m_PickedUp | AllowedEffects == null | AllowedEffects.Count <=0 ) { return; }

            ExpandShaders.Instance.GlitchScreenForDuration();

            AllowedEffects = AllowedEffects.Shuffle();

            m_SelectedEffect = BraveUtility.RandomElement(AllowedEffects);

            if (m_SelectedEffect == ItemEffectType.ArmorUp) {
                player.healthHaver.Armor += 10f;
            } else if (m_SelectedEffect == ItemEffectType.BigCash) {
                player.carriedConsumables.Currency = 999;
            } else if (m_SelectedEffect == ItemEffectType.ManyKeys) {
                player.carriedConsumables.KeyBullets = 999;
            } else if (m_SelectedEffect == ItemEffectType.BlanksRUS) {
                PickupObject blankPickup = PickupObjectDatabase.GetById(224);
                for (int i = 0; i < UnityEngine.Random.Range(5, 10); i++) {
                    if (blankPickup) { LootEngine.GivePrefabToPlayer(blankPickup.gameObject, player); }
                }
            } else if (m_SelectedEffect == ItemEffectType.HealthPak) {
                if (player.characterIdentity != PlayableCharacters.Robot) {
                    StatModifier healthUp = new StatModifier() {
                        statToBoost = PlayerStats.StatType.Health,
                        amount = UnityEngine.Random.Range(4, 6),
                        modifyType = StatModifier.ModifyMethod.ADDITIVE,
                        isMeatBunBuff = false,
                    };
                    player.ownerlessStatModifiers.Add(healthUp);
                    player.stats.RecalculateStats(player, false, false);
                } else {
                    player.healthHaver.Armor += UnityEngine.Random.Range(4, 6);
                }
            } else if (m_SelectedEffect == ItemEffectType.DrStone) {
                PickupObject glassStone = PickupObjectDatabase.GetById(565);
                for (int i = 0; i < UnityEngine.Random.Range(8, 12); i++) {
                    if (glassStone) { LootEngine.GivePrefabToPlayer(glassStone.gameObject, player); }
                }
            } else if (m_SelectedEffect == ItemEffectType.MuchAmmo) {
                StatModifier ManyBullets = new StatModifier() {
                    statToBoost = PlayerStats.StatType.AmmoCapacityMultiplier,
                    amount = UnityEngine.Random.Range(1.5f, 3),
                    modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                    isMeatBunBuff = false,
                };
                player.ownerlessStatModifiers.Add(ManyBullets);
                player.stats.RecalculateStats(player, false, false);
            }
            return;
        }


        private void HandleGlitchRoomSpawn(PlayerController player) {
            if (player.CurrentRoom != null && player.CurrentRoom.CompletelyPreventLeaving | (player.CurrentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS && player.CurrentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear))) {
                return;
            }
            RoomHandler[] CreepyGlitchRooms = GenerateCorruptedRoomCluster(lightStyle: DungeonData.LightGenerationStyle.STANDARD);
            
            ExpandPlaceCorruptTiles corruptedTilePlacer = new ExpandPlaceCorruptTiles();
            corruptedTilePlacer.PlaceCorruptTiles(GameManager.Instance.Dungeon, CreepyGlitchRooms[1]);

            EscapeToCreepyRoom(player, targetRoom: CreepyGlitchRooms[0]);
            
            corruptedTilePlacer = null;
        }

        public void EscapeToCreepyRoom(PlayerController player, RoomHandler targetRoom, bool resetCurrentRoom = true) {
            RespawnInCreepyRoom(player, targetRoom, resetCurrentRoom);
            if (targetRoom != null) { targetRoom.EnsureUpstreamLocksUnlocked(); }
            player.specRigidbody.Velocity = Vector2.zero;
            player.knockbackDoer.TriggerTemporaryKnockbackInvulnerability(1f);
        }

        public void RespawnInCreepyRoom(PlayerController player, RoomHandler targetRoom, bool resetCurrentRoom = true) {
            if (targetRoom == null) { return; }
            RoomHandler currentRoom = player.CurrentRoom;
            FieldInfo m_lastInteractionTarget = typeof(PlayerController).GetField("m_lastInteractionTarget", BindingFlags.Instance | BindingFlags.NonPublic);
            m_lastInteractionTarget.SetValue(player, null);
            StartCoroutine(HandleResetAndRespawn(player, targetRoom, currentRoom));
        }
        
        private IEnumerator HandleResetAndRespawn(PlayerController player, RoomHandler roomToSpawnIn, RoomHandler roomToReset) {
            if (player.CurrentGun) { player.CurrentGun.CeaseAttack(false, null); }
            
            player.CurrentInputState = PlayerInputState.NoInput;
            GameManager.Instance.MainCameraController.SetManualControl(true, false);
            Transform cameraTransform = GameManager.Instance.MainCameraController.transform;
            Vector3 cameraStartPosition = cameraTransform.position;
            Vector3 cameraEndPosition = player.CenterPosition;
            GameManager.Instance.MainCameraController.OverridePosition = cameraStartPosition;
            player.ToggleGunRenderers(false, "death");
            player.ToggleHandRenderers(false, "death");
            float elapsed = 0f;
            float duration = 0.8f;
            while (elapsed < duration) {
                elapsed += GameManager.INVARIANT_DELTA_TIME;
                float smoothT = Mathf.SmoothStep(0f, 1f, elapsed / duration);
                GameManager.Instance.MainCameraController.OverridePosition = Vector3.Lerp(cameraStartPosition, cameraEndPosition, smoothT);
                yield return null;
            }      
            elapsed = 0f;
            duration = 0.5f;
            while (elapsed < duration) {
                elapsed += GameManager.INVARIANT_DELTA_TIME;
                yield return null;
            }
            FieldInfo m_interruptingPitRespawn = typeof(PlayerController).GetField("m_interruptingPitRespawn", BindingFlags.Instance | BindingFlags.NonPublic);
            m_interruptingPitRespawn.SetValue(player, true);
            IntVector2 availableCell = roomToSpawnIn.GetCenteredVisibleClearSpot(3, 3);
            player.transform.position = new Vector3(availableCell.x + 0.5f, availableCell.y + 0.5f, -0.1f);
            player.ForceChangeRoom(roomToSpawnIn);
            player.Reinitialize();
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                PlayerController otherPlayer2 = GameManager.Instance.GetOtherPlayer(player);
                if (otherPlayer2.healthHaver.IsAlive) {
                    otherPlayer2.transform.position = player.transform.position + Vector3.right;
                    otherPlayer2.ForceChangeRoom(roomToSpawnIn);
                    otherPlayer2.Reinitialize();
                }
            }
            GameUIRoot.Instance.bossController.DisableBossHealth();
            GameUIRoot.Instance.bossController2.DisableBossHealth();
            GameUIRoot.Instance.bossControllerSide.DisableBossHealth();
            GameManager.Instance.MainCameraController.OverridePosition = player.CenterPosition;
            yield return null;
            player.ToggleGunRenderers(true, "death");
            player.ToggleHandRenderers(true, "death");
            //GameManager.Instance.ForceUnpause();
            GameManager.Instance.PreventPausing = false;
            player.CurrentInputState = PlayerInputState.AllInput;
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                PlayerController otherPlayer3 = GameManager.Instance.GetOtherPlayer(player);
                otherPlayer3.CurrentInputState = PlayerInputState.AllInput;
            }
            if (roomToReset != GameManager.Instance.Dungeon.data.Entrance && (roomToReset.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) || !roomToReset.EverHadEnemies)) {
                roomToReset.ResetPredefinedRoomLikeDarkSouls();
            }
            ReadOnlyCollection<Projectile> allProjectiles = StaticReferenceManager.AllProjectiles;
            for (int i = allProjectiles.Count - 1; i >= 0; i--) {
                if (allProjectiles[i]) { allProjectiles[i].DieInAir(false, true, true, false); }
            }
            GameManager.Instance.MainCameraController.SetManualControl(false, false);
            yield break;
        }


        public RoomHandler[] GenerateCorruptedRoomCluster(Action<RoomHandler> postProcessCellData = null, DungeonData.LightGenerationStyle lightStyle = DungeonData.LightGenerationStyle.FORCE_COLOR) {
            Dungeon dungeon = GameManager.Instance.Dungeon;

            PrototypeDungeonRoom[] RoomArray = new PrototypeDungeonRoom[] {
                ExpandRoomPrefabs.CreepyGlitchRoom_Entrance,
                ExpandRoomPrefabs.CreepyGlitchRoom
            };

            IntVector2[] basePositions = new IntVector2[] { IntVector2.Zero, new IntVector2(14, 0) };
            
                        
            GameObject tileMapObject = GameObject.Find("TileMap");
            tk2dTileMap m_tilemap = tileMapObject.GetComponent<tk2dTileMap>();

            if (m_tilemap == null) {
                if (ExpandStats.debugMode) { ETGModConsole.Log("ERROR: TileMap object is null! Something seriously went wrong!"); }
                return null;
            }

            TK2DDungeonAssembler assembler = new TK2DDungeonAssembler();
            assembler.Initialize(dungeon.tileIndices);

            if (RoomArray.Length != basePositions.Length) {
                Debug.LogError("Attempting to add a malformed room cluster at runtime!");
                return null;
            }

            RoomHandler[] RoomClusterArray = new RoomHandler[RoomArray.Length];
            int num = 6;
            int num2 = 3;
            IntVector2 intVector = new IntVector2(int.MaxValue, int.MaxValue);
            IntVector2 intVector2 = new IntVector2(int.MinValue, int.MinValue);
            for (int i = 0; i < RoomArray.Length; i++) {
                intVector = IntVector2.Min(intVector, basePositions[i]);
                intVector2 = IntVector2.Max(intVector2, basePositions[i] + new IntVector2(RoomArray[i].Width, RoomArray[i].Height));
            }
            IntVector2 a = intVector2 - intVector;
            IntVector2 b = IntVector2.Min(IntVector2.Zero, -1 * intVector);
            a += b;
            IntVector2 intVector3 = new IntVector2(dungeon.data.Width + num, num);
            int newWidth = dungeon.data.Width + num * 2 + a.x;
            int newHeight = Mathf.Max(dungeon.data.Height, a.y + num * 2);
            CellData[][] array = BraveUtility.MultidimensionalArrayResize(dungeon.data.cellData, dungeon.data.Width, dungeon.data.Height, newWidth, newHeight);
            dungeon.data.cellData = array;
            dungeon.data.ClearCachedCellData();
            for (int j = 0; j < RoomArray.Length; j++) {
                IntVector2 d = new IntVector2(RoomArray[j].Width, RoomArray[j].Height);
                IntVector2 b2 = basePositions[j] + b;
                IntVector2 intVector4 = intVector3 + b2;
                CellArea cellArea = new CellArea(intVector4, d, 0);
                cellArea.prototypeRoom = RoomArray[j];
                RoomHandler SelectedRoomInArray = new RoomHandler(cellArea);
                for (int k = -num; k < d.x + num; k++) {
                    for (int l = -num; l < d.y + num; l++) {
                        IntVector2 p = new IntVector2(k, l) + intVector4;
                        if ((k >= 0 && l >= 0 && k < d.x && l < d.y) || array[p.x][p.y] == null) {
                            CellData cellData = new CellData(p, CellType.WALL);
                            cellData.positionInTilemap = cellData.positionInTilemap - intVector3 + new IntVector2(num2, num2);
                            cellData.parentArea = cellArea;
                            cellData.parentRoom = SelectedRoomInArray;
                            cellData.nearestRoom = SelectedRoomInArray;
                            cellData.distanceFromNearestRoom = 0f;
                            array[p.x][p.y] = cellData;
                        }
                    }
                }
                dungeon.data.rooms.Add(SelectedRoomInArray);
                RoomClusterArray[j] = SelectedRoomInArray;
            }

            ConnectClusteredRooms(RoomClusterArray[1], RoomClusterArray[0], RoomArray[1], RoomArray[0], 0, 0, 3, 3);
            try { 
                for (int n = 0; n < RoomClusterArray.Length; n++) {
                    try {
                        RoomClusterArray[n].WriteRoomData(dungeon.data);
                    } catch (Exception) {
                        if (ExpandStats.debugMode) { ETGModConsole.Log("WARNING: Exception caused during WriteRoomData step on room: " + RoomClusterArray[n].GetRoomName()); }
                    } try {
                        dungeon.data.GenerateLightsForRoom(dungeon.decoSettings, RoomClusterArray[n], GameObject.Find("_Lights").transform, lightStyle);
                    } catch (Exception) {
                        if (ExpandStats.debugMode) { ETGModConsole.Log("WARNING: Exception caused during GeernateLightsForRoom step on room: " + RoomClusterArray[n].GetRoomName()); }
                    }
                    postProcessCellData?.Invoke(RoomClusterArray[n]);
                    if (RoomClusterArray[n].area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.SECRET) {
                        RoomClusterArray[n].BuildSecretRoomCover();
                    }
                }
                GameObject gameObject = (GameObject)Instantiate(BraveResources.Load("RuntimeTileMap", ".prefab"));
                tk2dTileMap component = gameObject.GetComponent<tk2dTileMap>();
                string str = UnityEngine.Random.Range(10000, 99999).ToString();
                gameObject.name = "Corrupted_" + "RuntimeTilemap_" + str;
                component.renderData.name = "Corrupted_" + "RuntimeTilemap_" + str + " Render Data";
                component.Editor__SpriteCollection = dungeon.tileIndices.dungeonCollection;
                
                TK2DDungeonAssembler.RuntimeResizeTileMap(component, a.x + num2 * 2, a.y + num2 * 2, m_tilemap.partitionSizeX, m_tilemap.partitionSizeY);
                for (int num3 = 0; num3 < RoomArray.Length; num3++) {
                    IntVector2 intVector5 = new IntVector2(RoomArray[num3].Width, RoomArray[num3].Height);
                    IntVector2 b3 = basePositions[num3] + b;
                    IntVector2 intVector6 = intVector3 + b3;
                    for (int num4 = -num2; num4 < intVector5.x + num2; num4++) {
                        for (int num5 = -num2; num5 < intVector5.y + num2 + 2; num5++) {
                            try {
                                assembler.BuildTileIndicesForCell(dungeon, component, intVector6.x + num4, intVector6.y + num5);
                            } catch (Exception ex) {
                                if (ExpandStats.debugMode) {
                                    ETGModConsole.Log("WARNING: Exception caused during BuildTileIndicesForCell step on room: " + RoomArray[num3].name);
                                    Debug.Log("WARNING: Exception caused during BuildTileIndicesForCell step on room: " + RoomArray[num3].name);
                                    Debug.LogException(ex);
                                }
                            }
                        }
                    }
                }
                RenderMeshBuilder.CurrentCellXOffset = intVector3.x - num2;
                RenderMeshBuilder.CurrentCellYOffset = intVector3.y - num2;
                component.ForceBuild();
                RenderMeshBuilder.CurrentCellXOffset = 0;
                RenderMeshBuilder.CurrentCellYOffset = 0;
                component.renderData.transform.position = new Vector3(intVector3.x - num2, intVector3.y - num2, intVector3.y - num2);
                for (int num6 = 0; num6 < RoomClusterArray.Length; num6++) {
                    RoomClusterArray[num6].OverrideTilemap = component;
                    for (int num7 = 0; num7 < RoomClusterArray[num6].area.dimensions.x; num7++) {
                        for (int num8 = 0; num8 < RoomClusterArray[num6].area.dimensions.y + 2; num8++) {
                            IntVector2 intVector7 = RoomClusterArray[num6].area.basePosition + new IntVector2(num7, num8);
                            if (dungeon.data.CheckInBoundsAndValid(intVector7)) {
                                CellData currentCell = dungeon.data[intVector7];
                                TK2DInteriorDecorator.PlaceLightDecorationForCell(dungeon, component, currentCell, intVector7);
                            }
                        }
                    }
                    Pathfinder.Instance.InitializeRegion(dungeon.data, RoomClusterArray[num6].area.basePosition + new IntVector2(-3, -3), RoomClusterArray[num6].area.dimensions + new IntVector2(3, 3));
                    if (!RoomClusterArray[num6].IsSecretRoom) {
                        RoomClusterArray[num6].RevealedOnMap = true;
                        RoomClusterArray[num6].visibility = RoomHandler.VisibilityStatus.VISITED;
                        StartCoroutine(Minimap.Instance.RevealMinimapRoomInternal(RoomClusterArray[num6], true, true, false));
                    }
                    
                    RoomClusterArray[num6].PostGenerationCleanup();
                }

                if (RoomArray.Length == RoomClusterArray.Length) {
                    for (int i = 0; i < RoomArray.Length; i++) {
                        if (RoomArray[i].usesProceduralDecoration && RoomArray[i].allowFloorDecoration) {
                            TK2DInteriorDecorator decorator = new TK2DInteriorDecorator(assembler);
                            decorator.HandleRoomDecoration(RoomClusterArray[i], dungeon, m_tilemap);
                        }
                    }
                }
            } catch (Exception) { }

            DeadlyDeadlyGoopManager.ReinitializeData();
            Minimap.Instance.InitializeMinimap(dungeon.data);
            return RoomClusterArray;
        }

        private void ConnectClusteredRooms(RoomHandler first, RoomHandler second, PrototypeDungeonRoom firstPrototype, PrototypeDungeonRoom secondPrototype, int firstRoomExitIndex, int secondRoomExitIndex, int room1ExitLengthPadding = 3, int room2ExitLengthPadding = 3) {
            if (first.area.instanceUsedExits == null | second.area.exitToLocalDataMap == null |
                second.area.instanceUsedExits == null | first.area.exitToLocalDataMap == null)
            { return; }
            try {
                first.area.instanceUsedExits.Add(firstPrototype.exitData.exits[firstRoomExitIndex]);
                RuntimeRoomExitData runtimeRoomExitData = new RuntimeRoomExitData(firstPrototype.exitData.exits[firstRoomExitIndex]);
                first.area.exitToLocalDataMap.Add(firstPrototype.exitData.exits[firstRoomExitIndex], runtimeRoomExitData);
                second.area.instanceUsedExits.Add(secondPrototype.exitData.exits[secondRoomExitIndex]);
                RuntimeRoomExitData runtimeRoomExitData2 = new RuntimeRoomExitData(secondPrototype.exitData.exits[secondRoomExitIndex]);
                second.area.exitToLocalDataMap.Add(secondPrototype.exitData.exits[secondRoomExitIndex], runtimeRoomExitData2);
                first.connectedRooms.Add(second);
                first.connectedRoomsByExit.Add(firstPrototype.exitData.exits[firstRoomExitIndex], second);
                first.childRooms.Add(second);
                second.connectedRooms.Add(first);
                second.connectedRoomsByExit.Add(secondPrototype.exitData.exits[secondRoomExitIndex], first);
                second.parentRoom = first;
                runtimeRoomExitData.linkedExit = runtimeRoomExitData2;
                runtimeRoomExitData2.linkedExit = runtimeRoomExitData;
                runtimeRoomExitData.additionalExitLength = room1ExitLengthPadding;
                runtimeRoomExitData2.additionalExitLength = room2ExitLengthPadding;
            } catch (Exception) {
                ETGModConsole.Log("WARNING: Exception caused during CoonectClusteredRunTimeRooms method!");
                return;
            }
        }
        


        public override DebrisObject Drop(PlayerController player) {
            DebrisObject drop = base.Drop(player);
            GetComponent<CorruptedJunk>().m_pickedUpThisRun = true;
            GetComponent<CorruptedJunk>().m_PickedUp = true;
            return drop;
        }


        public override void MidGameSerialize(List<object> data) {
            base.MidGameSerialize(data);
            data.Add(m_PickedUp);
        }

        public override void MidGameDeserialize(List<object> data) {
            base.MidGameDeserialize(data);
            if (data.Count == 1) { m_PickedUp = (bool)data[0]; }
        }


        protected override void Update() {
            base.Update();
        }

        protected override void OnDestroy() {
            base.OnDestroy();
        }
    }
}

