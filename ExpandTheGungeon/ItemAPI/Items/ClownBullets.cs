using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandPrefab;
using System.Collections.Generic;
using tk2dRuntime.TileMap;
using ExpandTheGungeon.ExpandUtilities;
using System.Reflection;

namespace ExpandTheGungeon.ItemAPI {
    
    public class ClownBullets : PassiveItem {

        public ClownBullets() {
            ActivationChance = 1f;
            ActivationsPerSecond = 0.09f;
            MinActivationChance = 0.045f;
            ChanceToDevolve = 1;
            NormalizeAcrossFireRate = true;
            TargetGUID = string.Empty;
            EnemyGuidsToIgnore = new List<string>();
        }

        public static GameObject ClownBulletsObject;
        public static int ClownBulletsID;

        public static void Init(AssetBundle expandSharedAssets1) {
            ClownBulletsObject = expandSharedAssets1.LoadAsset<GameObject>("EXClownBullets");
            SpriteSerializer.AddSpriteToObject(ClownBulletsObject, ExpandPrefabs.EXItemCollection, "clownbullets");

            ClownBullets clownBullets = ClownBulletsObject.AddComponent<ClownBullets>();
            ClownBulletsObject.name = "Clown Bullets";
            string shortDesc = "Make fools of your enemy...";
            string longDesc = "Some have said bullet kin are quite foolish compared to the more experienced gundead. Devolve your enemies to the clowns they should be with these special rounds! ";
            ItemBuilder.SetupItem(clownBullets, shortDesc, longDesc, "ex");            
            clownBullets.quality = ItemQuality.D;
            if (!ExpandSettings.EnableEXItems) { clownBullets.quality = ItemQuality.EXCLUDED; }
            ClownBulletsID = clownBullets.PickupObjectId;
        }


        public float ActivationChance;
        public float ChanceToDevolve;

        public string TargetGUID;
        public List<string> EnemyGuidsToIgnore;

        public bool NormalizeAcrossFireRate;

        public float ActivationsPerSecond;        
        public float MinActivationChance;
        
        private PlayerController m_player;

        public override void Pickup(PlayerController player) {
            if (m_pickedUp) { return; }
            m_player = player;
            base.Pickup(player);
            player.PostProcessProjectile += PostProcessProjectile;
            if (string.IsNullOrEmpty(TargetGUID)) { TargetGUID = ExpandCustomEnemyDatabase.ClownkinGUID; }
        }
        
        
        private void PostProcessProjectile(Projectile obj, float effectChanceScalar) {
            float chanceToActivate = ActivationChance;
            Gun gun = (!m_player) ? null : m_player.CurrentGun;
            if (NormalizeAcrossFireRate && gun) {
                float num2 = 1f / gun.DefaultModule.cooldownTime;
                chanceToActivate = Mathf.Clamp01(ActivationsPerSecond / num2);                
                chanceToActivate = Mathf.Max(MinActivationChance, chanceToActivate);
            }            
            if (UnityEngine.Random.value < chanceToActivate) {
                if (!obj.gameObject.GetComponent<ClownBulletsModifier>()) {
                    ClownBulletsModifier clownbulletModifier = obj.gameObject.AddComponent<ClownBulletsModifier>();
                    clownbulletModifier.chanceToDevolve = ChanceToDevolve;
                    clownbulletModifier.ClownKinGUID = TargetGUID;
                    clownbulletModifier.EnemyGuidsToIgnore = EnemyGuidsToIgnore;
                }
            }
            // obj.specRigidbody.OnPreTileCollision += OnPreTileCollision;
        }

        /*public void OnPreTileCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, PhysicsEngine.Tile tile, PixelCollider tilePixelCollider) {
            if (IsDestroyingTileMap) { return; }
            RoomHandler baseRoom = myRigidbody.gameObject.transform.position.GetAbsoluteRoom();
            IntVector2 position = tile.Position;
            CellData cellData = GameManager.Instance.Dungeon.data[position];
            // if (cellData != null && cellData.isRoomInternal) {
            if (cellData != null) {
                cellData.breakable = true;
                cellData.occlusionData.overrideOcclusion = true;
                cellData.occlusionData.cellOcclusionDirty = true;
                tk2dTileMap tilemap = GameManager.Instance.Dungeon.DestroyWallAtPosition(position.x, position.y, true);
                baseRoom.Cells.Add(cellData.position);
                baseRoom.CellsWithoutExits.Add(cellData.position);
                baseRoom.RawCells.Add(cellData.position);
                Pixelator.Instance.MarkOcclusionDirty();
                Pixelator.Instance.ProcessOcclusionChange(baseRoom.Epicenter, 1f, baseRoom, false);
                if (tilemap) { StartCoroutine(RebuildTilemap(tilemap, tk2dTileMap.BuildFlags.Default)); }
                // if (tilemap) { RebuildTilemap2(tilemap, tk2dTileMap.BuildFlags.Default); }
            }
        }

        public bool IsDestroyingTileMap = false;

        public IEnumerator RebuildTilemap(tk2dTileMap targetTilemap, tk2dTileMap.BuildFlags buildFlags) {
            IsDestroyingTileMap = true;
            RenderMeshBuilder.CurrentCellXOffset = Mathf.RoundToInt(targetTilemap.renderData.transform.position.x);
            RenderMeshBuilder.CurrentCellYOffset = Mathf.RoundToInt(targetTilemap.renderData.transform.position.y);
            IEnumerator enumerator = targetTilemap.DeferredBuild(buildFlags);
            while (enumerator.MoveNext()) { yield return null; }
            targetTilemap.renderData.transform.position = new Vector3(RenderMeshBuilder.CurrentCellXOffset, RenderMeshBuilder.CurrentCellYOffset, RenderMeshBuilder.CurrentCellYOffset);
            RenderMeshBuilder.CurrentCellXOffset = 0;
            RenderMeshBuilder.CurrentCellYOffset = 0;
            IsDestroyingTileMap = false;
            yield break;
        }

        public void RebuildTilemap2(tk2dTileMap targetTilemap, tk2dTileMap.BuildFlags buildFlags) {
            IsDestroyingTileMap = true;
            RenderMeshBuilder.CurrentCellXOffset = Mathf.RoundToInt(targetTilemap.renderData.transform.position.x);
            RenderMeshBuilder.CurrentCellYOffset = Mathf.RoundToInt(targetTilemap.renderData.transform.position.y);
            IEnumerator enumerator = DeferredBuild(targetTilemap, tk2dTileMap.BuildFlags.Default);
            while (enumerator.MoveNext()) { }
            targetTilemap.renderData.transform.position = new Vector3(RenderMeshBuilder.CurrentCellXOffset, RenderMeshBuilder.CurrentCellYOffset, targetTilemap.renderData.transform.position.z);
            RenderMeshBuilder.CurrentCellXOffset = 0;
            RenderMeshBuilder.CurrentCellYOffset = 0;
            IsDestroyingTileMap = false;
        }

        public IEnumerator DeferredBuild(tk2dTileMap tileMap, tk2dTileMap.BuildFlags buildFlags) {
            if (tileMap.data != null && tileMap.Editor__SpriteCollection != null) {
                int spriteCollectionKey = ReflectionHelpers.ReflectGetField<int>(typeof(tk2dTileMap), "spriteCollectionKey", tileMap); ;
                Layer[] layers = ReflectionHelpers.ReflectGetField<Layer[]>(typeof(tk2dTileMap), "layers", tileMap); ;
                ColorChannel colorChannel = ReflectionHelpers.ReflectGetField<ColorChannel>(typeof(tk2dTileMap), "colorChannel", tileMap);
                if (tileMap.data.tilePrefabs == null) {
                    tileMap.data.tilePrefabs = new GameObject[tileMap.SpriteCollectionInst.Count];
                } else if (tileMap.data.tilePrefabs.Length != tileMap.SpriteCollectionInst.Count) {
                    Array.Resize(ref tileMap.data.tilePrefabs, tileMap.SpriteCollectionInst.Count);
                }
                BuilderUtil.InitDataStore(tileMap);
                if (tileMap.SpriteCollectionInst) { tileMap.SpriteCollectionInst.InitMaterialIds(); }
                bool forceBuild = (buildFlags & tk2dTileMap.BuildFlags.ForceBuild) != tk2dTileMap.BuildFlags.Default;
                if (tileMap.SpriteCollectionInst && tileMap.SpriteCollectionInst.buildKey != spriteCollectionKey) {
                    forceBuild = true;
                }
                Dictionary<Layer, bool> layersActive = new Dictionary<Layer, bool>();
                if (layers != null) {
                    for (int i = 0; i < layers.Length; i++) {
                        Layer layer = layers[i];
                        if (layer != null && layer.gameObject != null) {
                            layersActive[layer] = layer.gameObject.activeSelf;
                        }
                    }
                }
                if (forceBuild) { ReflectionHelpers.InvokeMethod(typeof(tk2dTileMap), "ClearSpawnedInstances", tileMap); }
                BuilderUtil.CreateRenderData(tileMap, tileMap.AllowEdit, layersActive);
                SpriteChunk.s_roomChunks = new Dictionary<LayerInfo, List<SpriteChunk>>();
                if (Application.isPlaying && GameManager.Instance.Dungeon != null && GameManager.Instance.Dungeon.data != null && GameManager.Instance.Dungeon.MainTilemap == this) {
                    List<RoomHandler> rooms = GameManager.Instance.Dungeon.data.rooms;
                    if (rooms != null && rooms.Count > 0) {
                        for (int j = 0; j < tileMap.data.Layers.Length; j++) {
                            if (tileMap.data.Layers[j].overrideChunkable) {
                                for (int k = 0; k < rooms.Count; k++) {
                                    if (!SpriteChunk.s_roomChunks.ContainsKey(tileMap.data.Layers[j])) {
                                        SpriteChunk.s_roomChunks.Add(tileMap.data.Layers[j], new List<SpriteChunk>());
                                    }
                                    SpriteChunk spriteChunk = new SpriteChunk(rooms[k].area.basePosition.x + tileMap.data.Layers[j].overrideChunkXOffset, rooms[k].area.basePosition.y + tileMap.data.Layers[j].overrideChunkYOffset, rooms[k].area.basePosition.x + rooms[k].area.dimensions.x + tileMap.data.Layers[j].overrideChunkXOffset, rooms[k].area.basePosition.y + rooms[k].area.dimensions.y + tileMap.data.Layers[j].overrideChunkYOffset);
                                    spriteChunk.roomReference = rooms[k];
                                    string prototypeRoomName = rooms[k].area.PrototypeRoomName;
                                    tileMap.Layers[j].CreateOverrideChunk(spriteChunk);
                                    BuilderUtil.CreateOverrideChunkData(spriteChunk, tileMap, j, prototypeRoomName);
                                    spriteChunk.gameObject.transform.position = new Vector3(spriteChunk.gameObject.transform.position.x, spriteChunk.gameObject.transform.position.y, tileMap.renderData.transform.position.z);
                                    SpriteChunk.s_roomChunks[tileMap.data.Layers[j]].Add(spriteChunk);
                                }
                            }
                        }
                    }
                }
                IEnumerator BuildTracker = RenderMeshBuilder.Build(tileMap, tileMap.AllowEdit, forceBuild);
                while (BuildTracker.MoveNext()) { yield return null; }
                if (!tileMap.AllowEdit && tileMap.isGungeonTilemap) { BuilderUtil.SpawnAnimatedTiles(tileMap, forceBuild); }
                if (!tileMap.AllowEdit) {
                    tk2dSpriteDefinition firstValidDefinition = tileMap.SpriteCollectionInst.FirstValidDefinition;
                    if (firstValidDefinition != null && firstValidDefinition.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics2D) {
                        ColliderBuilder2D.Build(tileMap, forceBuild);
                    } else {
                        ColliderBuilder3D.Build(tileMap, forceBuild);
                    }
                    BuilderUtil.SpawnPrefabs(tileMap, forceBuild);
                }
                foreach (Layer layer2 in layers) { layer2.ClearDirtyFlag(); }
                if (colorChannel != null) { colorChannel.ClearDirtyFlag(); }
                if (tileMap.SpriteCollectionInst) {
                    FieldInfo m_spriteCollectionKey = typeof(tk2dTileMap).GetField("spriteCollectionKey", BindingFlags.NonPublic | BindingFlags.Instance);
                    m_spriteCollectionKey.SetValue(tileMap, tileMap.SpriteCollectionInst.buildKey);
                }
                yield break;
            }
            yield break;
        }*/

        public override DebrisObject Drop(PlayerController player) {
            DebrisObject debrisObject = base.Drop(player);
            m_player = null;
            debrisObject.GetComponent<ClownBullets>().m_pickedUpThisRun = true;
            player.PostProcessProjectile -= PostProcessProjectile;
            return debrisObject;
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            if (m_player) {
                m_player.PostProcessProjectile -= PostProcessProjectile;
                PlayerController player = m_player;
            }
        }
    }


    public class ClownBulletsModifier : MonoBehaviour {

        public ClownBulletsModifier() {
            chanceToDevolve = 0.1f;
            ClownKinGUID = string.Empty;
            EnemyGuidsToIgnore = new List<string>();
        }
        
        public float chanceToDevolve;

        public string ClownKinGUID;

        public List<string> EnemyGuidsToIgnore;
        
        private void Start() {
            Projectile component = GetComponent<Projectile>();
            if (component) {
                Projectile projectile = component;
                projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.HandleHitEnemy));
            }
        }

        private void HandleHitEnemy(Projectile sourceProjectile, SpeculativeRigidbody enemyRigidbody, bool killingBlow) {
            if (killingBlow) { return; }
            if (!enemyRigidbody || !enemyRigidbody.aiActor) { return; }
            if (UnityEngine.Random.value > chanceToDevolve) { return; }
            AIActor aiActor = enemyRigidbody.aiActor;
            if (!aiActor.IsNormalEnemy || aiActor.IsHarmlessEnemy || aiActor.healthHaver.IsBoss) { return; }
            string enemyGuid = aiActor.EnemyGuid;
            for (int i = 0; i < EnemyGuidsToIgnore.Count; i++) {
                if (EnemyGuidsToIgnore[i] == enemyGuid) { return; }
            }            
            aiActor.Transmogrify(EnemyDatabase.GetOrLoadByGuid(ClownKinGUID), (GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Spawn_Poof"));
            AkSoundEngine.PostEvent("Play_WPN_devolver_morph_01", gameObject);
        }
    }
}

