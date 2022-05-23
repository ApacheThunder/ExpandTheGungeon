using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using tk2dRuntime.TileMap;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandTK2DDungeonAssembler {
        
        public static void RuntimeResizeTileMap(tk2dTileMap tileMap, int w, int h, int partitionSizeX, int partitionSizeY) {
            foreach (Layer layer in tileMap.Layers) {
                layer.DestroyGameData(tileMap);
                if (layer.gameObject != null) {
                    tk2dUtil.DestroyImmediate(layer.gameObject);
                    layer.gameObject = null;
                }
            }
            Layer[] array = new Layer[tileMap.Layers.Length];
            for (int j = 0; j < tileMap.Layers.Length; j++) {
                Layer layer2 = tileMap.Layers[j];
                array[j] = new Layer(layer2.hash, w, h, partitionSizeX, partitionSizeY);
                Layer layer3 = array[j];
                if (!layer2.IsEmpty) {
                    int num = Mathf.Min(tileMap.height, h);
                    int num2 = Mathf.Min(tileMap.width, w);
                    for (int k = 0; k < num; k++) {
                        for (int l = 0; l < num2; l++) { layer3.SetRawTile(l, k, layer2.GetRawTile(l, k)); }
                    }
                    layer3.Optimize();
                }
            }
            bool flag = tileMap.ColorChannel != null && !tileMap.ColorChannel.IsEmpty;
            ColorChannel colorChannel = new ColorChannel(w, h, partitionSizeX, partitionSizeY);
            if (flag) {
                int num3 = Mathf.Min(tileMap.height, h) + 1;
                int num4 = Mathf.Min(tileMap.width, w) + 1;
                for (int m = 0; m < num3; m++) {
                    for (int n = 0; n < num4; n++) { colorChannel.SetColor(n, m, tileMap.ColorChannel.GetColor(n, m)); }
                }
                colorChannel.Optimize();
            }
            tileMap.ColorChannel = colorChannel;
            tileMap.Layers = array;
            tileMap.width = w;
            tileMap.height = h;
            tileMap.partitionSizeX = partitionSizeX;
            tileMap.partitionSizeY = partitionSizeY;
            tileMap.ForceBuild();
        }
        
        // Modified for future use but not currently required for Runtime room generation as nothing else in this class uses this as is.
        public static GameObject ApplyObjectStamp(int ix, int iy, ObjectStampData osd, Dungeon d, tk2dTileMap map, bool flipX = false, bool isLightStamp = false) {
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
                                if (cellVisualData.containsObjectSpaceStamp || flag || (!isLightStamp && cellVisualData.containsLight)) { return null; }
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
                if (!osd.objectReference) { return null; }
                Vector3 vector = osd.objectReference.transform.position;
                ObjectStampOptions component = osd.objectReference.GetComponent<ObjectStampOptions>();
                if (component != null) { vector = component.GetPositionOffset(); }
                GameObject gameObject = UnityEngine.Object.Instantiate(osd.objectReference);
                gameObject.transform.position = new Vector3(ix, iy, z) + vector;
                if (!isLightStamp && osd.placementRule == DungeonTileStampData.StampPlacementRule.ALONG_LEFT_WALLS) {
                    gameObject.transform.position = new Vector3((ix + 1), iy, z) + vector.WithX(-vector.x);
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
                if (component3 != null) { component3.Decorate(absoluteRoomFromPosition); }
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
                if (component2 != null) { component2.UpdateZDepth(); }
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
                    ETGModConsole.Log("WARNING: Exception occured during ExpandTK2DDungeonAssembler.ApplyObjectStamp!");
                    Debug.Log("WARNING: Exception occured during RuntimeResizeTileMap/RenderMeshBuilder steps!");
                    Debug.LogException(ex);                    
                }
                return null;
            }
        }
        
        public void ApplyTileStamp(int ix, int iy, TileStampData tsd, Dungeon d, Dungeon d2, tk2dTileMap map, int overrideTileLayerIndex = -1) {
            DungeonTileStampData.StampSpace occupySpace = tsd.occupySpace;
            for (int i = 0; i < tsd.width; i++) {
                for (int j = 0; j < tsd.height; j++) {
                    CellVisualData cellVisualData = d.data.cellData[ix + i][iy + j].cellVisualData;
                    if (occupySpace == DungeonTileStampData.StampSpace.BOTH_SPACES) {
                        if (cellVisualData.containsObjectSpaceStamp || cellVisualData.containsWallSpaceStamp || cellVisualData.containsLight) { return; }
                    } else if (occupySpace == DungeonTileStampData.StampSpace.OBJECT_SPACE) {
                        if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                            if (cellVisualData.containsObjectSpaceStamp || cellVisualData.containsLight) { return; }
                        } else if (cellVisualData.containsObjectSpaceStamp) {
                            return;
                        }
                    } else if (occupySpace == DungeonTileStampData.StampSpace.WALL_SPACE && (cellVisualData.containsWallSpaceStamp || cellVisualData.containsLight)) {
                        return;
                    }
                }
            }
            for (int k = 0; k < tsd.width; k++) {
                for (int l = 0; l < tsd.height; l++) {
                    CellData cellData = d.data.cellData[ix + k][iy + l];
                    CellVisualData cellVisualData2 = cellData.cellVisualData;
                    int num = (occupySpace != DungeonTileStampData.StampSpace.OBJECT_SPACE) ? GlobalDungeonData.wallStampLayerIndex : GlobalDungeonData.objectStampLayerIndex;
                    if (d.data.isFaceWallHigher(ix + k, iy + l - 1)) { num = GlobalDungeonData.aboveBorderLayerIndex; }
                    if (!d.data.isAnyFaceWall(ix + k, iy + l) && d.data.cellData[ix + k][iy + l].type == CellType.WALL) {
                        num = GlobalDungeonData.aboveBorderLayerIndex;
                    }
                    if (overrideTileLayerIndex != -1) { num = overrideTileLayerIndex; }
                    map.Layers[num].SetTile(cellData.positionInTilemap.x, cellData.positionInTilemap.y, tsd.stampTileIndices[(tsd.height - 1 - l) * tsd.width + k]);
                    if (occupySpace == DungeonTileStampData.StampSpace.OBJECT_SPACE) { cellVisualData2.containsObjectSpaceStamp = true; }
                    if (occupySpace == DungeonTileStampData.StampSpace.WALL_SPACE) { cellVisualData2.containsWallSpaceStamp = true; }
                    if (occupySpace == DungeonTileStampData.StampSpace.BOTH_SPACES) {
                        cellVisualData2.containsObjectSpaceStamp = true;
                        cellVisualData2.containsWallSpaceStamp = true;
                    }
                }
            }
        }

        public void ApplyStampGeneric(int ix, int iy, StampDataBase sd, Dungeon d, Dungeon d2, tk2dTileMap map, bool flipX = false, int overrideTileLayerIndex = -1) {
            if (sd is TileStampData) {
                ApplyTileStamp(ix, iy, sd as TileStampData, d, d2, map, overrideTileLayerIndex);
            } else if (sd is SpriteStampData) {
                ApplySpriteStamp(ix, iy, sd as SpriteStampData, d, map);
            } else if (sd is ObjectStampData) {
                ApplyObjectStamp(ix, iy, sd as ObjectStampData, d, map, flipX, false);
            }
        }

        public void ApplySpriteStamp(int ix, int iy, SpriteStampData ssd, Dungeon d, tk2dTileMap map) {
            DungeonTileStampData.StampSpace occupySpace = ssd.occupySpace;
            for (int i = 0; i < ssd.width; i++) {
                for (int j = 0; j < ssd.height; j++) {
                    CellVisualData cellVisualData = d.data.cellData[ix + i][iy + j].cellVisualData;
                    if (occupySpace == DungeonTileStampData.StampSpace.BOTH_SPACES) {
                        if (cellVisualData.containsObjectSpaceStamp || cellVisualData.containsWallSpaceStamp) { return; }
                    } else if (occupySpace == DungeonTileStampData.StampSpace.OBJECT_SPACE) {
                        if (cellVisualData.containsObjectSpaceStamp) { return; }
                    } else if (occupySpace == DungeonTileStampData.StampSpace.WALL_SPACE && cellVisualData.containsWallSpaceStamp) {
                        return;
                    }
                }
            }
            int num = (occupySpace != DungeonTileStampData.StampSpace.OBJECT_SPACE) ? GlobalDungeonData.wallStampLayerIndex : GlobalDungeonData.objectStampLayerIndex;
            float z = map.data.Layers[num].z;
            SpriteRenderer spriteRenderer = new GameObject(ssd.spriteReference.name) { transform = { position = new Vector3(ix, iy, z) } }.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = ssd.spriteReference;
            DepthLookupManager.ProcessRenderer(spriteRenderer);
            for (int k = 0; k < ssd.width; k++) {
                for (int l = 0; l < ssd.height; l++) {
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
        }
        


        public TileIndices m_tileIndices;

        public Dictionary<TilesetIndexMetadata.TilesetFlagType, List<Tuple<int, TilesetIndexMetadata>>> m_metadataLookupTable;
        
        public bool BCheck(Dungeon d, int ix, int iy, int thresh) { return d.data.CheckInBounds(new IntVector2(ix, iy), 3 + thresh); }

        public bool BCheck(Dungeon d, int ix, int iy) { return BCheck(d, ix, iy, 0); }

        private bool HasMetadataForRoomType(TilesetIndexMetadata.TilesetFlagType flagType, int roomType) {
            if (m_metadataLookupTable[flagType] == null) { return false; }
            List<Tuple<int, TilesetIndexMetadata>> list = m_metadataLookupTable[flagType];
            foreach (Tuple<int, TilesetIndexMetadata> Metadata in list) {
                if (Metadata.Second.dungeonRoomSubType == roomType || Metadata.Second.secondRoomSubType == roomType || Metadata.Second.thirdRoomSubType == roomType) {
                    return true;
                }
            }
            return false;
        }
        
        public void Initialize(TileIndices indices) {
            m_metadataLookupTable = new Dictionary<TilesetIndexMetadata.TilesetFlagType, List<Tuple<int, TilesetIndexMetadata>>>();
            TilesetIndexMetadata.TilesetFlagType[] array = (TilesetIndexMetadata.TilesetFlagType[])Enum.GetValues(typeof(TilesetIndexMetadata.TilesetFlagType));
            foreach (TilesetIndexMetadata.TilesetFlagType flagType in array) {
                m_metadataLookupTable.Add(flagType, indices.dungeonCollection.GetIndicesForTileType(flagType));
            }
            SecretRoomUtility.metadataLookupTableRef = m_metadataLookupTable;
            m_tileIndices = indices;
        }


        public void BuildTileIndicesForCell(Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            CellData cellData = d.data.cellData[ix][iy];
            if (cellData == null) { return; }

            BuildOcclusionPartitionIndex(cellData, d, d2, map, ix, iy);

            cellData.isOccludedByTopWall = d.data.isTopWall(ix, iy);
            if (cellData.cellVisualData.hasAlreadyBeenTilemapped) { return; }
            if (cellData.cellVisualData.precludeAllTileDrawing) { return; }
            bool flag = BCheck(d, ix, iy, 3) && d.data[ix, iy - 2] != null && d.data[ix, iy - 2].isExitCell;
            if (cellData.nearestRoom != null && cellData.nearestRoom.PrecludeTilemapDrawing && (!cellData.nearestRoom.DrawPrecludedCeilingTiles || (!cellData.isExitCell && !flag))) {
                if (cellData.nearestRoom.DrawPrecludedCeilingTiles) {
                    BuildCollisionIndex(cellData, d, d2, map, ix, iy);
                    BuildBorderIndicesForCell(cellData, d, d2, map, ix, iy);
                }
                cellData.cellVisualData.precludeAllTileDrawing = true;
                return;
            }
            if (cellData.parentRoom != null && cellData.parentRoom.PrecludeTilemapDrawing && (!cellData.nearestRoom.DrawPrecludedCeilingTiles || (!cellData.isExitCell && !flag))) {
                if (cellData.parentRoom.DrawPrecludedCeilingTiles) {
                    BuildCollisionIndex(cellData, d, d2, map, ix, iy);
                    BuildBorderIndicesForCell(cellData, d, d2, map, ix, iy);
                }
                cellData.cellVisualData.precludeAllTileDrawing = true;
                return;
            }
            DungeonMaterial dungeonMaterial = d2.roomMaterialDefinitions[cellData.cellVisualData.roomVisualTypeIndex];
            if (dungeonMaterial.overrideStoneFloorType && cellData.cellVisualData.floorType == CellVisualData.CellFloorType.Stone) {
                cellData.cellVisualData.floorType = dungeonMaterial.overrideFloorType;
            }
            bool flag2 = cellData.type == CellType.FLOOR || d.data.isFaceWallLower(ix, iy);

            if (flag2) { BuildFloorIndex(cellData, d, d2, map, ix, iy); }
            BuildDecoIndices(cellData, d, d2, map, ix, iy);
            if (flag2) { BuildFloorEdgeBorderTiles(cellData, d, d2, map, ix, iy); }
            BuildFeatureEdgeBorderTiles(cellData, d, d2, map, ix, iy);
            
            BuildCollisionIndex(cellData, d, d2, map, ix, iy);

            if (BCheck(d, ix, iy, -2)) { ProcessFacewallIndices(cellData, d, d2, map, ix, iy); }
            BuildBorderIndicesForCell(cellData, d, d2, map, ix, iy);

            TileIndexGrid tileIndexGrid = d2.roomMaterialDefinitions[cellData.cellVisualData.roomVisualTypeIndex].pitBorderFlatGrid;
            TileIndexGrid additionalPitBorderFlatGrid = dungeonMaterial.additionalPitBorderFlatGrid;

            PrototypeRoomPitEntry.PitBorderType pitBorderType = cellData.GetPitBorderType(d.data);
            if (pitBorderType == PrototypeRoomPitEntry.PitBorderType.FLAT) {
                tileIndexGrid = dungeonMaterial.pitBorderFlatGrid;
            } else if (pitBorderType == PrototypeRoomPitEntry.PitBorderType.RAISED) {
                tileIndexGrid = dungeonMaterial.pitBorderRaisedGrid;
            }

            int num = (pitBorderType != PrototypeRoomPitEntry.PitBorderType.RAISED) ? GlobalDungeonData.patternLayerIndex : GlobalDungeonData.actorCollisionLayerIndex;
            int num2 = num;
            bool walls_ARE_PITS = d.debugSettings.WALLS_ARE_PITS;
            if (cellData.type == CellType.FLOOR) {
                if (d2.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.WESTGEON && d2.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.FINALGEON) {
                    BuildShadowIndex(cellData, d, map, ix, iy);
                }
                if (tileIndexGrid != null) { HandlePitBorderTilePlacement(cellData, tileIndexGrid, map.Layers[num], map, d, d2); }
                if (additionalPitBorderFlatGrid != null) {
                    HandlePitBorderTilePlacement(cellData, additionalPitBorderFlatGrid, map.Layers[num2], map, d, d2);
                }
            } else if (cellData.type == CellType.PIT && d2.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.WESTGEON && d2.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.FINALGEON) {
                BuildPitShadowIndex(cellData, d, d2, map, ix, iy);
            }
            if (cellData.type == CellType.PIT || (walls_ARE_PITS && cellData.isExitCell)) {
                TileIndexGrid pitLayoutGrid = dungeonMaterial.pitLayoutGrid;

                if (pitLayoutGrid == null) { pitLayoutGrid = d2.roomMaterialDefinitions[0].pitLayoutGrid; }

                map.data.Layers[GlobalDungeonData.pitLayerIndex].ForceNonAnimating = true;
                HandlePitTilePlacement(cellData, pitLayoutGrid, map.Layers[GlobalDungeonData.pitLayerIndex], d);
                if (tileIndexGrid != null) { HandlePitBorderTilePlacement(cellData, tileIndexGrid, map.Layers[num], map, d, d2); }
                if (additionalPitBorderFlatGrid != null) {
                    HandlePitBorderTilePlacement(cellData, additionalPitBorderFlatGrid, map.Layers[num2], map, d, d2);
                }
            }
            if (d.data.isTopDiagonalWall(ix, iy)) {
                if (cellData.diagonalWallType == DiagonalWallType.NORTHEAST) {
                    AssignSpecificColorsToTile(cellData.positionInTilemap.x, cellData.positionInTilemap.y, GlobalDungeonData.collisionLayerIndex, new Color(0f, 0.5f, 1f), new Color(0f, 1f, 1f), new Color(0f, 1f, 1f), new Color(0f, 1f, 1f), map);
                } else if (cellData.diagonalWallType == DiagonalWallType.NORTHWEST) {
                    AssignSpecificColorsToTile(cellData.positionInTilemap.x, cellData.positionInTilemap.y, GlobalDungeonData.collisionLayerIndex, new Color(0f, 1f, 1f), new Color(0f, 0.5f, 1f), new Color(0f, 1f, 1f), new Color(0f, 1f, 1f), map);
                }
            }
            if (cellData.cellVisualData.pathTilesetGridIndex > -1) {
                TileIndexGrid pathGrid = d2.pathGridDefinitions[cellData.cellVisualData.pathTilesetGridIndex];
                HandlePathTilePlacement(cellData, d, d2, map, pathGrid);
            }
            if (cellData.cellVisualData.UsesCustomIndexOverride01) {
                map.SetTile(cellData.positionInTilemap.x, cellData.positionInTilemap.y, cellData.cellVisualData.CustomIndexOverride01Layer, cellData.cellVisualData.CustomIndexOverride01);
            }
            if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.JUNGLEGEON) {
                try {
                    BuildOcclusionLayerCenterJungle(cellData, d, map, ix, iy);
                } catch (Exception ex) {
                    if (ExpandSettings.debugMode) { Debug.LogException(ex); }
                }
            }
            if (cellData.distanceFromNearestRoom < 4f && cellData.nearestRoom.area.PrototypeLostWoodsRoom) {
                HandleLostWoodsMirroring(cellData, d, map, ix, iy);
            }            

            cellData.hasBeenGenerated = true;
        }
                
        private void BuildBorderIndicesForCell(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            if (m_tileIndices.placeBorders) {
                if (current.nearestRoom != null && current.nearestRoom.area.prototypeRoom != null && current.nearestRoom.area.prototypeRoom.preventBorders) {
                    return;
                }
                if (BCheck(d, ix, iy, -2) && (current.type == CellType.WALL || d.data.isTopWall(ix, iy)) && !d.data.isFaceWallHigher(ix, iy) && !d.data.isFaceWallLower(ix, iy)) {
                    BuildBorderIndex(current, d, d2, map, ix, iy);
                }
                if (BCheck(d, ix, iy, -2) && (current.type != CellType.WALL || d.data.isAnyFaceWall(ix, iy)) && !d.data.isTopWall(ix, iy) && d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].outerCeilingBorderGrid != null) {
                    BuildOuterBorderIndex(current, d, d2, map, ix, iy);
                }
            }
        }

        private void BuildFeatureEdgeBorderTiles(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                TileIndexGrid exteriorFacadeBorderGrid = d2.roomMaterialDefinitions[1].exteriorFacadeBorderGrid;
                List<CellData> cellNeighbors = d.data.GetCellNeighbors(current, true);
                bool[] array = new bool[8];
                for (int i = 0; i < array.Length; i++) {
                    if (cellNeighbors[i] != null) {
                        array[i] = (cellNeighbors[i].cellVisualData.IsFeatureCell || cellNeighbors[i].cellVisualData.IsFeatureAdditional);
                    }
                }
                int indexGivenEightSides = exteriorFacadeBorderGrid.GetIndexGivenEightSides(array);
                if (indexGivenEightSides != -1) {
                    map.Layers[GlobalDungeonData.decalLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, indexGivenEightSides);
                }
            }
        }
        
        private void BuildFloorEdgeBorderTiles(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            if (current.type != CellType.FLOOR && !d.data.isFaceWallLower(ix, iy)) { return; }
            TileIndexGrid tileIndexGrid = d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].roomFloorBorderGrid;
            if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON && current.cellVisualData.IsFacewallForInteriorTransition) {
                tileIndexGrid = d2.roomMaterialDefinitions[current.cellVisualData.InteriorTransitionIndex].exteriorFacadeBorderGrid;
            }
            if (tileIndexGrid != null) {
                if (current.diagonalWallType == DiagonalWallType.NONE || !d.data.isFaceWallLower(ix, iy)) {
                    List<CellData> cellNeighbors = d.data.GetCellNeighbors(current, true);
                    bool[] array = new bool[8];
                    for (int i = 0; i < array.Length; i++) {
                        if (cellNeighbors[i] != null) {
                            array[i] = (cellNeighbors[i].type == CellType.WALL && !d.data.isTopWall(cellNeighbors[i].position.x, cellNeighbors[i].position.y + 1) && cellNeighbors[i].diagonalWallType == DiagonalWallType.NONE);
                            bool flag = cellNeighbors[i].isSecretRoomCell || (d.data[cellNeighbors[i].position + IntVector2.Up].IsTopWall() && d.data[cellNeighbors[i].position + IntVector2.Up].isSecretRoomCell);
                            array[i] = (array[i] || flag != current.isSecretRoomCell);
                        }
                    }
                    int indexGivenEightSides = tileIndexGrid.GetIndexGivenEightSides(array);
                    if (indexGivenEightSides != -1) {
                        map.Layers[GlobalDungeonData.decalLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, indexGivenEightSides);
                    }
                } else {
                    int indexByWeight = tileIndexGrid.quadNubs.GetIndexByWeight();
                    if (indexByWeight != -1) {
                        map.Layers[GlobalDungeonData.decalLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, indexByWeight);
                    }
                }
            }
        }

        private void BuildOuterBorderIndex(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            bool isNorthBorder = (d.data.isWall(ix, iy + 1) || d.data.isTopWall(ix, iy + 1)) && !d.data.isAnyFaceWall(ix, iy + 1);
            bool isNortheastBorder = (d.data.isWall(ix + 1, iy + 1) || d.data.isTopWall(ix + 1, iy + 1)) && !d.data.isAnyFaceWall(ix + 1, iy + 1);
            bool isEastBorder = (d.data.isWall(ix + 1, iy) || d.data.isTopWall(ix + 1, iy)) && !d.data.isAnyFaceWall(ix + 1, iy);
            bool isSoutheastBorder = (d.data.isWall(ix + 1, iy - 1) || d.data.isTopWall(ix + 1, iy - 1)) && !d.data.isAnyFaceWall(ix + 1, iy - 1);
            bool isSouthBorder = (d.data.isWall(ix, iy - 1) || d.data.isTopWall(ix, iy - 1)) && !d.data.isAnyFaceWall(ix, iy - 1);
            bool isSouthwestBorder = (d.data.isWall(ix - 1, iy - 1) || d.data.isTopWall(ix - 1, iy - 1)) && !d.data.isAnyFaceWall(ix - 1, iy - 1);
            bool isWestBorder = (d.data.isWall(ix - 1, iy) || d.data.isTopWall(ix - 1, iy)) && !d.data.isAnyFaceWall(ix - 1, iy);
            bool isNorthwestBorder = (d.data.isWall(ix - 1, iy + 1) || d.data.isTopWall(ix - 1, iy + 1)) && !d.data.isAnyFaceWall(ix - 1, iy + 1);
            TileIndexGrid outerCeilingBorderGrid = d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].outerCeilingBorderGrid;
            int indexGivenSides = outerCeilingBorderGrid.GetIndexGivenSides(isNorthBorder, isNortheastBorder, isEastBorder, isSoutheastBorder, isSouthBorder, isSouthwestBorder, isWestBorder, isNorthwestBorder);
            if (indexGivenSides != -1 && !current.cellVisualData.shouldIgnoreWallDrawing) {
                map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, indexGivenSides);
            }
        }

        private void BuildOcclusionPartitionIndex(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            if (current == null) { return; }
            if (current.cellVisualData.ceilingHasBeenProcessed || current.cellVisualData.occlusionHasBeenProcessed) { return; }
            int num = -1;
            TileIndexGrid typeBorderGridForBorderIndex = GetTypeBorderGridForBorderIndex(current, d, d2, out num);
            if (typeBorderGridForBorderIndex != null) {
                List<CellData> cellNeighbors = d.data.GetCellNeighbors(current, true);
                bool[] array = new bool[8];
                int num2 = -1;
                for (int i = 0; i < array.Length; i++) {
                    if (cellNeighbors[i] != null) {
                        GetTypeBorderGridForBorderIndex(cellNeighbors[i], d, d2, out num2);
                        if (d2.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.WESTGEON || num2 == 0 || num == 0) { array[i] = (num != num2); }
                    }
                }
                int num3 = typeBorderGridForBorderIndex.GetIndexGivenEightSides(array);
                if (num3 == -1) { num3 = typeBorderGridForBorderIndex.centerIndices.GetIndexByWeight(); }
                map.SetTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.occlusionPartitionIndex, num3);
            }
        }

        private void BuildBorderIndex(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            if (current.cellVisualData.ceilingHasBeenProcessed) { return; }
            bool flag = d.data[ix, iy + 1] != null && d.data[ix, iy + 1].diagonalWallType != DiagonalWallType.NONE && (d.data[ix, iy + 1].IsTopWall() || d.data[ix, iy + 1].type == CellType.WALL);
            bool flag2 = d.data[ix + 1, iy] != null && d.data[ix + 1, iy].diagonalWallType != DiagonalWallType.NONE && (d.data[ix + 1, iy].IsTopWall() || d.data[ix + 1, iy].type == CellType.WALL);
            bool flag3 = d.data[ix, iy - 1] != null && d.data[ix, iy - 1].diagonalWallType != DiagonalWallType.NONE && (d.data[ix, iy - 1].IsTopWall() || d.data[ix, iy - 1].type == CellType.WALL);
            bool flag4 = d.data[ix - 1, iy] != null && d.data[ix - 1, iy].diagonalWallType != DiagonalWallType.NONE && (d.data[ix - 1, iy].IsTopWall() || d.data[ix - 1, iy].type == CellType.WALL);
            bool flag5 = d.data.isTopWall(ix, iy);
            flag5 = (flag5 && d.data[ix, iy + 1] != null && !d.data[ix, iy + 1].cellVisualData.shouldIgnoreBorders);
            bool flag6 = (!d.data.isWallRight(ix, iy) && !d.data.isRightTopWall(ix, iy)) || d.data.isFaceWallHigher(ix + 1, iy) || d.data.isFaceWallLower(ix + 1, iy);
            flag6 = (flag6 && d.data[ix + 1, iy] != null && !d.data[ix + 1, iy].cellVisualData.shouldIgnoreBorders);
            bool flag7 = iy > 3 && d.data.isFaceWallHigher(ix, iy - 1);
            flag7 = (flag7 && d.data[ix, iy - 1] != null && !d.data[ix, iy - 1].cellVisualData.shouldIgnoreBorders);
            bool flag8 = (!d.data.isWallLeft(ix, iy) && !d.data.isLeftTopWall(ix, iy)) || d.data.isFaceWallHigher(ix - 1, iy) || d.data.isFaceWallLower(ix - 1, iy);
            flag8 = (flag8 && d.data[ix - 1, iy] != null && !d.data[ix - 1, iy].cellVisualData.shouldIgnoreBorders);
            bool flag9 = (!flag || !flag2) && d.data.isTopWall(ix + 1, iy) && !d.data.isTopWall(ix, iy) && (d.data.isWall(ix, iy + 1) || d.data.isTopWall(ix, iy + 1));
            flag9 = (flag9 && d.data[ix + 1, iy + 1] != null && !d.data[ix + 1, iy + 1].cellVisualData.shouldIgnoreBorders);
            bool flag10 = (!flag || !flag4) && d.data.isTopWall(ix - 1, iy) && !d.data.isTopWall(ix, iy) && (d.data.isWall(ix, iy + 1) || d.data.isTopWall(ix, iy + 1));
            flag10 = (flag10 && d.data[ix - 1, iy + 1] != null && !d.data[ix - 1, iy + 1].cellVisualData.shouldIgnoreBorders);
            bool flag11 = (!flag3 || !flag2) && iy > 3 && d.data.isFaceWallHigher(ix + 1, iy - 1) && !d.data.isFaceWallHigher(ix, iy - 1);
            flag11 = (flag11 && d.data[ix + 1, iy - 1] != null && !d.data[ix + 1, iy - 1].cellVisualData.shouldIgnoreBorders);
            bool flag12 = (!flag3 || !flag4) && iy > 3 && d.data.isFaceWallHigher(ix - 1, iy - 1) && !d.data.isFaceWallHigher(ix, iy - 1);
            flag12 = (flag12 && d.data[ix - 1, iy - 1] != null && !d.data[ix - 1, iy - 1].cellVisualData.shouldIgnoreBorders);
            int num = -1;
            int num2 = -1;
            TileIndexGrid typeBorderGridForBorderIndex = GetTypeBorderGridForBorderIndex(current, d, d2, out num2);
            if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                int num3 = -1;
                if (!flag5) {
                    flag5 = (typeBorderGridForBorderIndex != GetTypeBorderGridForBorderIndex(d.data[current.position + IntVector2.North], d, d2, out num3) && (num3 == 0 || num2 == 0));
                }
                if (!flag9) {
                    flag9 = (typeBorderGridForBorderIndex != GetTypeBorderGridForBorderIndex(d.data[current.position + IntVector2.NorthEast], d, d2, out num3) && (num3 == 0 || num2 == 0));
                }
                if (!flag6) {
                    flag6 = (typeBorderGridForBorderIndex != GetTypeBorderGridForBorderIndex(d.data[current.position + IntVector2.East], d, d2, out num3) && (num3 == 0 || num2 == 0));
                }
                if (!flag11) {
                    flag11 = (typeBorderGridForBorderIndex != GetTypeBorderGridForBorderIndex(d.data[current.position + IntVector2.SouthEast], d, d2, out num3) && (num3 == 0 || num2 == 0));
                }
                if (!flag7) {
                    flag7 = (typeBorderGridForBorderIndex != GetTypeBorderGridForBorderIndex(d.data[current.position + IntVector2.South], d, d2, out num3) && (num3 == 0 || num2 == 0));
                }
                if (!flag12) {
                    flag12 = (typeBorderGridForBorderIndex != GetTypeBorderGridForBorderIndex(d.data[current.position + IntVector2.SouthWest], d, d2, out num3) && (num3 == 0 || num2 == 0));
                }
                if (!flag8) {
                    flag8 = (typeBorderGridForBorderIndex != GetTypeBorderGridForBorderIndex(d.data[current.position + IntVector2.West], d, d2, out num3) && (num3 == 0 || num2 == 0));
                }
                if (!flag10) {
                    flag10 = (typeBorderGridForBorderIndex != GetTypeBorderGridForBorderIndex(d.data[current.position + IntVector2.NorthWest], d, d2, out num3) && (num3 == 0 || num2 == 0));
                }
            }
            if (current.diagonalWallType == DiagonalWallType.NONE) {
                if (!flag5 && !flag9 && !flag6 && !flag11 && !flag7 && !flag12 && !flag8 && !flag10) {
                    if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.JUNGLEGEON) {
                        BuildBorderLayerCenterJungle(current, d, map, ix, iy);
                        num = -1;
                    } else if (typeBorderGridForBorderIndex.CeilingBorderUsesDistancedCenters) {
                        int count = typeBorderGridForBorderIndex.centerIndices.indices.Count;
                        int index = Mathf.Max(0, Mathf.Min(Mathf.FloorToInt(current.distanceFromNearestRoom) - 1, count - 1));
                        num = typeBorderGridForBorderIndex.centerIndices.indices[index];
                    } else {
                        num = typeBorderGridForBorderIndex.centerIndices.GetIndexByWeight();
                        if (d2.tileIndices.globalSecondBorderTiles.Count > 0 && current.distanceFromNearestRoom < 3f && UnityEngine.Random.value > 0.5f) {
                            num = d2.tileIndices.globalSecondBorderTiles[UnityEngine.Random.Range(0, d2.tileIndices.globalSecondBorderTiles.Count)];
                        }
                    }
                } else if (typeBorderGridForBorderIndex.UsesRatChunkBorders) {
                    bool flag13 = iy > 3;
                    if (flag13) { flag13 = !d.data[ix, iy - 1].HasFloorNeighbor(d.data, false, true); }
                    TileIndexGrid.RatChunkResult ratChunkResult = TileIndexGrid.RatChunkResult.NONE;
                    if (d.data[ix, iy].nearestRoom.area.PrototypeLostWoodsRoom) {
                        num = typeBorderGridForBorderIndex.GetRatChunkIndexGivenSidesStatic(flag5, flag9, flag6, flag11, flag7, flag12, flag8, flag10, flag13, out ratChunkResult);
                    } else {
                        num = typeBorderGridForBorderIndex.GetRatChunkIndexGivenSides(flag5, flag9, flag6, flag11, flag7, flag12, flag8, flag10, flag13, out ratChunkResult);
                    }
                    if (ratChunkResult == TileIndexGrid.RatChunkResult.CORNER) { HandleRatChunkOverhangs(d, ix, iy, map); }
                } else {
                    num = typeBorderGridForBorderIndex.GetIndexGivenSides(flag5, flag9, flag6, flag11, flag7, flag12, flag8, flag10);
                }
            } else {
                switch (current.diagonalWallType) {
                    case DiagonalWallType.NORTHEAST:
                        if (flag7 && flag8) { num = typeBorderGridForBorderIndex.diagonalBorderNE.GetIndexByWeight(); }
                        break;
                    case DiagonalWallType.SOUTHEAST:
                        if (flag5 && flag8) {
                            num = typeBorderGridForBorderIndex.diagonalBorderSE.GetIndexByWeight();
                        } else {
                            num = typeBorderGridForBorderIndex.GetIndexGivenSides(flag5, flag9, flag6, flag11, flag7, flag12, flag8, flag10);
                        }
                        break;
                    case DiagonalWallType.SOUTHWEST:
                        if (flag5 && flag6) {
                            num = typeBorderGridForBorderIndex.diagonalBorderSW.GetIndexByWeight();
                        } else {
                            num = typeBorderGridForBorderIndex.GetIndexGivenSides(flag5, flag9, flag6, flag11, flag7, flag12, flag8, flag10);
                        }
                        break;
                    case DiagonalWallType.NORTHWEST:
                        if (flag7 && flag6) { num = typeBorderGridForBorderIndex.diagonalBorderNW.GetIndexByWeight(); }
                        break;
                }
            }
            TileIndexGrid typeBorderGridForBorderIndex2 = GetTypeBorderGridForBorderIndex(current, d, d2, out num2);
            if (num != -1) {
                if (!current.cellVisualData.shouldIgnoreWallDrawing) {
                    map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, num);
                }
                if (current.cellVisualData.shouldIgnoreWallDrawing) {
                    BraveUtility.DrawDebugSquare(current.position.ToVector2(), Color.blue, 1000f);
                }
                if (flag5 && current.diagonalWallType != DiagonalWallType.NONE) {
                    int num4 = -1;
                    DiagonalWallType diagonalWallType = current.diagonalWallType;
                    if (diagonalWallType != DiagonalWallType.SOUTHEAST) {
                        if (diagonalWallType == DiagonalWallType.SOUTHWEST) {
                            num4 = typeBorderGridForBorderIndex2.diagonalCeilingSW.GetIndexByWeight();
                        }
                    } else {
                        num4 = typeBorderGridForBorderIndex2.diagonalCeilingSE.GetIndexByWeight();
                    }
                    if (num4 != -1) {
                        map.Layers[GlobalDungeonData.ceilingLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, num4);
                        AssignColorOverrideToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.ceilingLayerIndex, new Color(1f, 1f, 1f, 0f), map);
                    }
                    num4 = GetCeilingCenterIndex(current, typeBorderGridForBorderIndex2);
                    map.Layers[GlobalDungeonData.ceilingLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y - 1, num4);
                    AssignColorOverrideToTile(current.positionInTilemap.x, current.positionInTilemap.y - 1, GlobalDungeonData.ceilingLayerIndex, new Color(1f, 1f, 1f, 0f), map);
                } else if (flag5) {
                    int ceilingCenterIndex = GetCeilingCenterIndex(current, typeBorderGridForBorderIndex2);
                    map.Layers[GlobalDungeonData.ceilingLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, ceilingCenterIndex);
                    AssignColorOverrideToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.ceilingLayerIndex, new Color(1f, 1f, 1f, 0f), map);
                } else if (flag7 && current.diagonalWallType != DiagonalWallType.NONE) {
                    int num5 = -1;
                    DiagonalWallType diagonalWallType2 = current.diagonalWallType;
                    if (diagonalWallType2 != DiagonalWallType.NORTHEAST) {
                        if (diagonalWallType2 == DiagonalWallType.NORTHWEST) { num5 = typeBorderGridForBorderIndex2.diagonalCeilingNW.GetIndexByWeight(); }
                    } else {
                        num5 = typeBorderGridForBorderIndex2.diagonalCeilingNE.GetIndexByWeight();
                    }
                    if (num5 != -1) {
                        map.Layers[GlobalDungeonData.ceilingLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, num5);
                        AssignColorOverrideToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.ceilingLayerIndex, new Color(1f, 1f, 1f, 0f), map);
                    }
                } else if (flag6 || flag8 || flag9 || flag10 || flag7 || flag11 || flag12) {
                    int ceilingCenterIndex2 = GetCeilingCenterIndex(current, typeBorderGridForBorderIndex2);
                    map.Layers[GlobalDungeonData.ceilingLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, ceilingCenterIndex2);
                    AssignColorOverrideToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.ceilingLayerIndex, new Color(1f, 1f, 1f, 0f), map);
                }
                if (flag5 || (d.data[current.position + IntVector2.Up] != null && d.data[current.position + IntVector2.Up].IsTopWall())) {
                    AssignColorOverrideToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.borderLayerIndex, new Color(1f, 1f, 1f, 0f), map);
                } else {
                    AssignColorOverrideToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.borderLayerIndex, new Color(0f, 0f, 0f), map);
                }
            }
        }

        public void BuildCollisionIndex(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            if (current.type == CellType.WALL && (iy < 2 || !d.data.isFaceWallLower(ix, iy)) && !d.data.isTopDiagonalWall(ix, iy)) {
                TileIndexGrid roomCeilingBorderGrid = d.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].roomCeilingBorderGrid;
                if ((d.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON || d.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON) && current.nearestRoom != null) {
                    roomCeilingBorderGrid = d.roomMaterialDefinitions[current.nearestRoom.RoomVisualSubtype].roomCeilingBorderGrid;
                }
                if (roomCeilingBorderGrid == null) { roomCeilingBorderGrid = d.roomMaterialDefinitions[0].roomCeilingBorderGrid; }
                map.Layers[GlobalDungeonData.collisionLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, roomCeilingBorderGrid.centerIndices.indices[0]);
            }
        }
        
        private void BuildPitShadowIndex(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            if (!d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].doPitAO) { return; }
            if (current != null && current.cellVisualData.hasStampedPath) { return; }
            int floorLayerIndex = GlobalDungeonData.floorLayerIndex;
            if (BCheck(d, ix, iy, -2)) {
                CellData cellData = d.data.cellData[ix - 1][iy];
                CellData cellData2 = d.data.cellData[ix + 1][iy];
                CellData cellData3 = d.data.cellData[ix][iy + 1];
                CellData cellData4 = d.data.cellData[ix][iy + 2];
                CellData cellData5 = d.data.cellData[ix + 1][iy + 2];
                CellData cellData6 = d.data.cellData[ix + 1][iy + 1];
                CellData cellData7 = d.data.cellData[ix - 1][iy + 2];
                CellData cellData8 = d.data.cellData[ix - 1][iy + 1];
                DungeonMaterial dungeonMaterial = d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex];
                bool flag;
                bool flag2;
                bool flag3;
                bool flag4;
                bool flag5;
                if (dungeonMaterial.pitsAreOneDeep) {
                    flag = (cellData.type != CellType.PIT);
                    flag2 = (cellData2.type != CellType.PIT);
                    flag3 = (cellData3.type != CellType.PIT);
                    flag4 = (cellData6.type != CellType.PIT);
                    flag5 = (cellData8.type != CellType.PIT);
                } else {
                    flag = (cellData3.type == CellType.PIT && cellData8.type != CellType.PIT);
                    flag2 = (cellData3.type == CellType.PIT && cellData6.type != CellType.PIT);
                    flag3 = (cellData4.type != CellType.PIT && cellData3.type == CellType.PIT);
                    flag4 = (cellData5.type != CellType.PIT && cellData6.type == CellType.PIT);
                    flag5 = (cellData7.type != CellType.PIT && cellData8.type == CellType.PIT);
                }
                if (dungeonMaterial.pitfallVFXPrefab != null && dungeonMaterial.pitfallVFXPrefab.name.ToLowerInvariant().Contains("splash")) {
                    if (flag3 && flag && !flag2) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallUpAndLeft);
                    } else if (flag3 && flag2 && !flag) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallUpAndRight);
                    } else if (flag3 && flag && flag2) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallUpAndBoth);
                    } else if (flag3 && !flag && !flag2) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorTileIndex);
                    }
                } else if (flag3 && flag && !flag2) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOBottomWallTileLeftIndex);
                } else if (flag3 && flag2 && !flag) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOBottomWallTileRightIndex);
                } else if (flag3 && flag && flag2) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOBottomWallTileBothIndex);
                } else if (flag3 && !flag && !flag2) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOBottomWallBaseTileIndex);
                }
                if (!flag3) {
                    bool flag6 = flag && !d.data.isTopWall(current.positionInTilemap.x - 1, current.positionInTilemap.y + 1);
                    bool flag7 = flag2 && !d.data.isTopWall(current.positionInTilemap.x + 1, current.positionInTilemap.y + 1);
                    if (flag6 && flag7) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallBoth);
                    } else if (flag6) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallLeft);
                    } else if (flag7) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallRight);
                    }
                }
                if (!flag3 && flag5 && !flag && !flag2 && !flag4) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorPizzaSliceLeft);
                } else if (!flag3 && !flag5 && !flag && !flag2 && flag4) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorPizzaSliceRight);
                } else if (!flag3 && flag5 && !flag2 && !flag && flag4) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorPizzaSliceBoth);
                } else if (!flag3 && flag5 && !flag && flag2) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorPizzaSliceLeftWallRight);
                } else if (!flag3 && flag && !flag2 && flag4) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorPizzaSliceRightWallLeft);
                }
            }
        }

        private TileIndexGrid GetTypeBorderGridForBorderIndex(CellData current, Dungeon d, Dungeon d2, out int usedVisualType) {
            TileIndexGrid roomCeilingBorderGrid;
            
            try {
                roomCeilingBorderGrid = d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].roomCeilingBorderGrid;
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    Debug.Log("[ExpandTheGungeon] [WARNING] Exception caught in ExpandTK2DDungeonAssembler.GetTypeBorderGridForBorderIndex !");
                    Debug.LogException(ex);
                }
                roomCeilingBorderGrid = null;
                usedVisualType = 0;
                return null;
            }
            
            usedVisualType = current.cellVisualData.roomVisualTypeIndex;
            if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                if (current.nearestRoom != null && current.distanceFromNearestRoom < 4f) {
                    if (current.cellVisualData.IsFacewallForInteriorTransition) {
                        roomCeilingBorderGrid = d2.roomMaterialDefinitions[current.cellVisualData.InteriorTransitionIndex].roomCeilingBorderGrid;
                        usedVisualType = current.cellVisualData.InteriorTransitionIndex;
                    } else if (!current.cellVisualData.IsFeatureCell) {
                        roomCeilingBorderGrid = d2.roomMaterialDefinitions[current.nearestRoom.RoomVisualSubtype].roomCeilingBorderGrid;
                        usedVisualType = current.nearestRoom.RoomVisualSubtype;
                    }
                }
            } else if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON) {
                roomCeilingBorderGrid = d2.roomMaterialDefinitions[current.nearestRoom.RoomVisualSubtype].roomCeilingBorderGrid;
                usedVisualType = current.nearestRoom.RoomVisualSubtype;
            }
            if (roomCeilingBorderGrid == null) {
                roomCeilingBorderGrid = d2.roomMaterialDefinitions[0].roomCeilingBorderGrid;
                usedVisualType = 0;
            }
            return roomCeilingBorderGrid;
        }
        
        private void BuildFloorIndex(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            if (current.cellVisualData.inheritedOverrideIndex != -1) {
                map.Layers[(!current.cellVisualData.inheritedOverrideIndexIsFloor) ? GlobalDungeonData.patternLayerIndex : GlobalDungeonData.floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, current.cellVisualData.inheritedOverrideIndex);
            }
            if (current.cellVisualData.inheritedOverrideIndex == -1 || !current.cellVisualData.inheritedOverrideIndexIsFloor) {
                bool flag = true;
                TileIndexGrid randomGridFromArray = d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].GetRandomGridFromArray(d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].floorSquares);
                if (randomGridFromArray == null) { flag = false; }
                if (flag) {
                    for (int i = 0; i < 3; i++) {
                        for (int j = 0; j < 3; j++) {
                            if (!BCheck(d, ix + i, iy + j)) { flag = false; }
                            if (d.data.isWall(ix + i, iy + j) || d.data.isAnyFaceWall(ix + i, iy + j)) { flag = false; }
                            CellData cellData = d.data.cellData[ix + i][iy + j];
                            if (cellData.HasWallNeighbor(true, false) || cellData.HasPitNeighbor(d.data)) { flag = false; }
                            if (cellData.cellVisualData.roomVisualTypeIndex != current.cellVisualData.roomVisualTypeIndex) { flag = false; }
                            if (cellData.cellVisualData.inheritedOverrideIndex != -1) { flag = false; }
                            if (cellData.cellVisualData.floorType == CellVisualData.CellFloorType.Ice) { flag = false; }
                            if (cellData.doesDamage) { flag = false; }
                            if (!flag) { break; }
                        }
                        if (!flag) { break; }
                    }
                }
                if (flag && current.UniqueHash < d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].floorSquareDensity) {
                    TileIndexGrid tileIndexGrid = randomGridFromArray;
                    int num = (current.UniqueHash >= 0.025f) ? 3 : 2;
                    if (tileIndexGrid.topIndices.indices[0] == -1) { num = 2; }
                    for (int k = 0; k < num; k++) {
                        for (int l = 0; l < num; l++) {
                            bool isNorthBorder = l == num - 1;
                            bool isSouthBorder = l == 0;
                            bool isEastBorder = k == num - 1;
                            bool isWestBorder = k == 0;
                            int indexGivenSides = tileIndexGrid.GetIndexGivenSides(isNorthBorder, isEastBorder, isSouthBorder, isWestBorder);
                            if (BCheck(d, ix + k, iy + l)) {
                                if (!d.data.isFaceWallLower(ix + k, iy + l)) {
                                    if (d.data.cellData[ix + k][iy + l].type != CellType.PIT) {
                                        CellData cellData2 = d.data.cellData[ix + k][iy + l];
                                        cellData2.cellVisualData.inheritedOverrideIndex = indexGivenSides;
                                        cellData2.cellVisualData.inheritedOverrideIndexIsFloor = true;
                                        map.Layers[GlobalDungeonData.floorLayerIndex].SetTile(cellData2.positionInTilemap.x, cellData2.positionInTilemap.y, indexGivenSides);
                                    }
                                }
                            }
                        }
                    }
                } else if (current.cellVisualData.floorType == CellVisualData.CellFloorType.Ice && d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].supportsIceSquares) {
                    TileIndexGrid randomGridFromArray2 = d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].GetRandomGridFromArray(d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].iceGrids);
                    List<CellData> cellNeighbors = d.data.GetCellNeighbors(current, true);
                    bool isNorthBorder2 = cellNeighbors[0].cellVisualData.floorType != CellVisualData.CellFloorType.Ice;
                    bool isNortheastBorder = cellNeighbors[1].cellVisualData.floorType != CellVisualData.CellFloorType.Ice;
                    bool isEastBorder2 = cellNeighbors[2].cellVisualData.floorType != CellVisualData.CellFloorType.Ice;
                    bool isSoutheastBorder = cellNeighbors[3].cellVisualData.floorType != CellVisualData.CellFloorType.Ice;
                    bool isSouthBorder2 = cellNeighbors[4].cellVisualData.floorType != CellVisualData.CellFloorType.Ice;
                    bool isSouthwestBorder = cellNeighbors[5].cellVisualData.floorType != CellVisualData.CellFloorType.Ice;
                    bool isWestBorder2 = cellNeighbors[6].cellVisualData.floorType != CellVisualData.CellFloorType.Ice;
                    bool isNorthwestBorder = cellNeighbors[7].cellVisualData.floorType != CellVisualData.CellFloorType.Ice;
                    int indexGivenSides2 = randomGridFromArray2.GetIndexGivenSides(isNorthBorder2, isNortheastBorder, isEastBorder2, isSoutheastBorder, isSouthBorder2, isSouthwestBorder, isWestBorder2, isNorthwestBorder);
                    map.Layers[GlobalDungeonData.floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, indexGivenSides2);
                    map.Layers[GlobalDungeonData.patternLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, indexGivenSides2);
                } else if (current.doesDamage && d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].supportsLavaOrLavalikeSquares) {
                    TileIndexGrid randomGridFromArray3 = d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].GetRandomGridFromArray(d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].lavaGrids);
                    List<CellData> cellNeighbors2 = d.data.GetCellNeighbors(current, true);
                    bool isNorthBorder3 = !cellNeighbors2[0].doesDamage;
                    bool isNortheastBorder2 = !cellNeighbors2[1].doesDamage;
                    bool isEastBorder3 = !cellNeighbors2[2].doesDamage;
                    bool isSoutheastBorder2 = !cellNeighbors2[3].doesDamage;
                    bool isSouthBorder3 = !cellNeighbors2[4].doesDamage;
                    bool isSouthwestBorder2 = !cellNeighbors2[5].doesDamage;
                    bool isWestBorder3 = !cellNeighbors2[6].doesDamage;
                    bool isNorthwestBorder2 = !cellNeighbors2[7].doesDamage;
                    int indexGivenSides3 = randomGridFromArray3.GetIndexGivenSides(isNorthBorder3, isNortheastBorder2, isEastBorder3, isSoutheastBorder2, isSouthBorder3, isSouthwestBorder2, isWestBorder3, isNorthwestBorder2);
                    map.Layers[GlobalDungeonData.floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, indexGivenSides3);
                    map.Layers[GlobalDungeonData.patternLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, indexGivenSides3);
                } else {
                    RoomInternalMaterialTransition roomInternalMaterialTransition = (current != null && current.parentRoom != null) ? GetMaterialTransitionFromSubtypes(d2, current.parentRoom.RoomVisualSubtype, current.cellVisualData.roomVisualTypeIndex) : null;
                    if (roomInternalMaterialTransition != null) {
                        List<CellData> cellNeighbors3 = d.data.GetCellNeighbors(current, true);
                        bool flag2 = cellNeighbors3[0].cellVisualData.roomVisualTypeIndex == current.parentRoom.RoomVisualSubtype;
                        bool flag3 = cellNeighbors3[1].cellVisualData.roomVisualTypeIndex == current.parentRoom.RoomVisualSubtype;
                        bool flag4 = cellNeighbors3[2].cellVisualData.roomVisualTypeIndex == current.parentRoom.RoomVisualSubtype;
                        bool flag5 = cellNeighbors3[3].cellVisualData.roomVisualTypeIndex == current.parentRoom.RoomVisualSubtype;
                        bool flag6 = cellNeighbors3[4].cellVisualData.roomVisualTypeIndex == current.parentRoom.RoomVisualSubtype;
                        bool flag7 = cellNeighbors3[5].cellVisualData.roomVisualTypeIndex == current.parentRoom.RoomVisualSubtype;
                        bool flag8 = cellNeighbors3[6].cellVisualData.roomVisualTypeIndex == current.parentRoom.RoomVisualSubtype;
                        bool flag9 = cellNeighbors3[7].cellVisualData.roomVisualTypeIndex == current.parentRoom.RoomVisualSubtype;
                        bool flag10 = flag2 || flag3 || flag4 || flag5 || flag6 || flag7 || flag8 || flag9;
                        int tile = GetIndexFromTupleArray(current, m_metadataLookupTable[TilesetIndexMetadata.TilesetFlagType.FLOOR_TILE], current.cellVisualData.roomVisualTypeIndex);
                        if (flag10) {
                            tile = roomInternalMaterialTransition.transitionGrid.GetIndexGivenSides(flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9);
                        }
                        map.Layers[GlobalDungeonData.floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tile);
                    } else {
                        int indexFromTupleArray = GetIndexFromTupleArray(current, m_metadataLookupTable[TilesetIndexMetadata.TilesetFlagType.FLOOR_TILE], current.cellVisualData.roomVisualTypeIndex);
                        map.Layers[GlobalDungeonData.floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, indexFromTupleArray);
                    }
                }
            }
            if (d.data.HasDoorAtPosition(new IntVector2(ix, iy)) || d.data[ix, iy].cellVisualData.doorFeetOverrideMode != 0) {
                DungeonDoorController dungeonDoorController = null;
                IntVector2 key = new IntVector2(ix, iy);
                if (d.data.doors.ContainsKey(key)) { dungeonDoorController = d.data.doors[key]; }
                if (d.data[ix, iy].cellVisualData.doorFeetOverrideMode == 1 || (dungeonDoorController != null && dungeonDoorController.northSouth)) {
                    int tile2 = -1;
                    GetMetadataFromTupleArray(current, m_metadataLookupTable[TilesetIndexMetadata.TilesetFlagType.DOOR_FEET_NS], -1, out tile2);
                    map.Layers[GlobalDungeonData.decalLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tile2);
                    map.Layers[GlobalDungeonData.patternLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tile2);
                } else if (d.data[ix, iy].cellVisualData.doorFeetOverrideMode == 2 || (dungeonDoorController != null && !dungeonDoorController.northSouth)) {
                    int tile3 = -1;
                    GetMetadataFromTupleArray(current, m_metadataLookupTable[TilesetIndexMetadata.TilesetFlagType.DOOR_FEET_EW], -1, out tile3);
                    map.Layers[GlobalDungeonData.patternLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tile3);
                    map.Layers[GlobalDungeonData.decalLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tile3);
                }
            }
        }

        private void BuildDecoIndices(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            if ((current.type == CellType.FLOOR || current.IsLowerFaceWall()) && !d.data.isTopWall(ix, iy) && !current.cellVisualData.floorTileOverridden && current.cellVisualData.inheritedOverrideIndex == -1) {
                DungeonMaterial dungeonMaterial = d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex];
                if (current.HasPitNeighbor(d.data)) { return; }
                if (current.cellVisualData.isPattern) {
                    List<CellData> cellNeighbors = d.data.GetCellNeighbors(current, true);
                    bool[] array = new bool[8];
                    for (int i = 0; i < array.Length; i++) {
                        array[i] = (!cellNeighbors[i].cellVisualData.isPattern && cellNeighbors[i].type != CellType.WALL);
                    }
                    TileIndexGrid tileIndexGrid = (!dungeonMaterial.usesPatternLayer) ? m_tileIndices.patternIndexGrid : dungeonMaterial.patternIndexGrid;
                    current.cellVisualData.preventFloorStamping = true;
                    if (tileIndexGrid != null) {
                        map.Layers[GlobalDungeonData.patternLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndexGrid.GetIndexGivenSides(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]));
                    }
                }
                if (current.cellVisualData.isDecal) {
                    List<CellData> cellNeighbors2 = d.data.GetCellNeighbors(current, true);
                    bool[] array2 = new bool[8];
                    for (int j = 0; j < array2.Length; j++) {
                        array2[j] = (!cellNeighbors2[j].cellVisualData.isDecal && cellNeighbors2[j].type != CellType.WALL);
                    }
                    TileIndexGrid tileIndexGrid2 = (!dungeonMaterial.usesDecalLayer) ? m_tileIndices.decalIndexGrid : dungeonMaterial.decalIndexGrid;
                    current.cellVisualData.preventFloorStamping = true;
                    if (tileIndexGrid2 != null) {
                        map.Layers[GlobalDungeonData.decalLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndexGrid2.GetIndexGivenSides(array2[0], array2[1], array2[2], array2[3], array2[4], array2[5], array2[6], array2[7]));
                    }
                }
            }
        }

        private void ProcessFacewallIndices(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy) {
            if (current.cellVisualData.shouldIgnoreWallDrawing) { return; }
            DungeonMaterial dungeonMaterial = d2.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex];
            if (current.cellVisualData.IsFacewallForInteriorTransition) {
                dungeonMaterial = d2.roomMaterialDefinitions[current.cellVisualData.InteriorTransitionIndex];
            }
            if (d.data.isSingleCellWall(ix, iy)) {
                map.Layers[GlobalDungeonData.collisionLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, GetIndexFromTileArray(current, m_tileIndices.chestHighWallIndices).index);
            } else if (d.data.isFaceWallLower(ix, iy)) {
                if (dungeonMaterial != null && dungeonMaterial.usesFacewallGrids) {
                    FacewallIndexGridDefinition facewallIndexGridDefinition = dungeonMaterial.facewallGrids[UnityEngine.Random.Range(0, dungeonMaterial.facewallGrids.Length)];
                    if (current.cellVisualData.faceWallOverrideIndex == -1 && UnityEngine.Random.value < facewallIndexGridDefinition.chanceToPlaceIfPossible) {
                        AssignFacewallGrid(current, d, map, ix, iy, facewallIndexGridDefinition);
                    }
                }
                bool flag = d.data.isWallLeft(ix, iy) && !d.data.isFaceWallLeft(ix, iy);
                bool flag2 = d.data.isWallRight(ix, iy) && !d.data.isFaceWallRight(ix, iy);
                bool flag3 = !d.data.isWallLeft(ix, iy);
                bool flag4 = !d.data.isWallRight(ix, iy);
                if (flag3 && dungeonMaterial.forceEdgesDiagonal) { current.diagonalWallType = DiagonalWallType.NORTHEAST; }
                if (flag4 && dungeonMaterial.forceEdgesDiagonal) { current.diagonalWallType = DiagonalWallType.NORTHWEST; }
                if (flag3 && !flag4 && HasMetadataForRoomType(TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER_LEFTEDGE, current.cellVisualData.roomVisualTypeIndex)) {
                    ProcessFacewallType(current, d, d2, map, ix, iy, TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER, TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER_LEFTEDGE);
                } else if (flag4 && !flag3 && HasMetadataForRoomType(TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER_RIGHTEDGE, current.cellVisualData.roomVisualTypeIndex)) {
                    ProcessFacewallType(current, d, d2, map, ix, iy, TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER, TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER_RIGHTEDGE);
                } else if (flag && !flag2 && HasMetadataForRoomType(TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER_LEFTCORNER, current.cellVisualData.roomVisualTypeIndex)) {
                    ProcessFacewallType(current, d, d2, map, ix, iy, TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER, TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER_LEFTCORNER);
                } else if (flag2 && !flag && HasMetadataForRoomType(TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER_RIGHTCORNER, current.cellVisualData.roomVisualTypeIndex)) {
                    ProcessFacewallType(current, d, d2, map, ix, iy, TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER, TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER_RIGHTCORNER);
                } else {
                    ProcessFacewallType(current, d, d2, map, ix, iy, TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER, TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER);
                }
            } else if (d.data.isFaceWallHigher(ix, iy)) {
                bool flag5 = d.data.isWallLeft(ix, iy) && !d.data.isFaceWallLeft(ix, iy);
                bool flag6 = d.data.isWallRight(ix, iy) && !d.data.isFaceWallRight(ix, iy);
                bool flag7 = !d.data.isWallLeft(ix, iy) || (d.data.isFaceWallLeft(ix, iy) && !d.data[ix - 1, iy].IsUpperFacewall());
                bool flag8 = !d.data.isWallRight(ix, iy) || (d.data.isFaceWallRight(ix, iy) && !d.data[ix + 1, iy].IsUpperFacewall());
                if (flag7 && !flag8 && dungeonMaterial.forceEdgesDiagonal) { current.diagonalWallType = DiagonalWallType.NORTHEAST; }
                if (flag8 && !flag7 && dungeonMaterial.forceEdgesDiagonal) { current.diagonalWallType = DiagonalWallType.NORTHWEST; }
                if (flag7 && !flag8 && HasMetadataForRoomType(TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER_LEFTEDGE, current.cellVisualData.roomVisualTypeIndex)) {
                    ProcessFacewallType(current, d, d2, map, ix, iy, TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER, TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER_LEFTEDGE);
                } else if (flag8 && !flag7 && HasMetadataForRoomType(TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER_RIGHTEDGE, current.cellVisualData.roomVisualTypeIndex)) {
                    ProcessFacewallType(current, d, d2, map, ix, iy, TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER, TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER_RIGHTEDGE);
                } else if (flag5 && !flag6 && HasMetadataForRoomType(TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER_LEFTCORNER, current.cellVisualData.roomVisualTypeIndex)) {
                    ProcessFacewallType(current, d, d2, map, ix, iy, TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER, TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER_LEFTCORNER);
                } else if (flag6 && !flag5 && HasMetadataForRoomType(TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER_RIGHTCORNER, current.cellVisualData.roomVisualTypeIndex)) {
                    ProcessFacewallType(current, d, d2, map, ix, iy, TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER, TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER_RIGHTCORNER);
                } else {
                    ProcessFacewallType(current, d, d2, map, ix, iy, TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER, TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER);
                }
            }
        }

        private void HandlePitBorderTilePlacement(CellData cell, TileIndexGrid borderGrid, Layer tileMapLayer, tk2dTileMap tileMap, Dungeon d, Dungeon d2) {
            if (borderGrid.PitBorderIsInternal) {
                if (cell.type == CellType.PIT) {
                    List<CellData> cellNeighbors = d.data.GetCellNeighbors(cell, true);
                    bool flag = cellNeighbors[0] != null && cellNeighbors[0].type == CellType.PIT;
                    bool flag2 = cellNeighbors[1] != null && cellNeighbors[1].type == CellType.PIT;
                    bool flag3 = cellNeighbors[2] != null && cellNeighbors[2].type == CellType.PIT;
                    bool flag4 = cellNeighbors[3] != null && cellNeighbors[3].type == CellType.PIT;
                    bool flag5 = cellNeighbors[4] != null && cellNeighbors[4].type == CellType.PIT;
                    bool flag6 = cellNeighbors[5] != null && cellNeighbors[5].type == CellType.PIT;
                    bool flag7 = cellNeighbors[6] != null && cellNeighbors[6].type == CellType.PIT;
                    bool flag8 = cellNeighbors[7] != null && cellNeighbors[7].type == CellType.PIT;
                    int indexGivenSides = borderGrid.GetIndexGivenSides(!flag, !flag2, !flag3, !flag4, !flag5, !flag6, !flag7, !flag8);
                    tileMapLayer.SetTile(cell.positionInTilemap.x, cell.positionInTilemap.y, indexGivenSides);
                }
            } else if (cell.type == CellType.PIT) {
                List<CellData> cellNeighbors2 = d.data.GetCellNeighbors(cell, false);
                bool isNorthBorder = cellNeighbors2[0] != null && cellNeighbors2[0].type == CellType.PIT;
                bool isEastBorder = cellNeighbors2[1] != null && cellNeighbors2[1].type == CellType.PIT;
                bool isSouthBorder = cellNeighbors2[2] != null && cellNeighbors2[2].type == CellType.PIT;
                bool isWestBorder = cellNeighbors2[3] != null && cellNeighbors2[3].type == CellType.PIT;
                int internalIndexGivenSides = borderGrid.GetInternalIndexGivenSides(isNorthBorder, isEastBorder, isSouthBorder, isWestBorder);
                if (internalIndexGivenSides != -1) {
                    tileMapLayer.SetTile(cell.positionInTilemap.x, cell.positionInTilemap.y, internalIndexGivenSides);
                }
            } else {
                List<CellData> cellNeighbors3 = d.data.GetCellNeighbors(cell, true);
                bool flag9 = cellNeighbors3[0] != null && (cellNeighbors3[0].type == CellType.PIT || cellNeighbors3[0].cellVisualData.RequiresPitBordering);
                bool flag10 = cellNeighbors3[1] != null && (cellNeighbors3[1].type == CellType.PIT || cellNeighbors3[1].cellVisualData.RequiresPitBordering);
                bool flag11 = cellNeighbors3[2] != null && (cellNeighbors3[2].type == CellType.PIT || cellNeighbors3[2].cellVisualData.RequiresPitBordering);
                bool flag12 = cellNeighbors3[3] != null && (cellNeighbors3[3].type == CellType.PIT || cellNeighbors3[3].cellVisualData.RequiresPitBordering);
                bool flag13 = cellNeighbors3[4] != null && (cellNeighbors3[4].type == CellType.PIT || cellNeighbors3[4].cellVisualData.RequiresPitBordering);
                bool flag14 = cellNeighbors3[5] != null && (cellNeighbors3[5].type == CellType.PIT || cellNeighbors3[5].cellVisualData.RequiresPitBordering);
                bool flag15 = cellNeighbors3[6] != null && (cellNeighbors3[6].type == CellType.PIT || cellNeighbors3[6].cellVisualData.RequiresPitBordering);
                bool flag16 = cellNeighbors3[7] != null && (cellNeighbors3[7].type == CellType.PIT || cellNeighbors3[7].cellVisualData.RequiresPitBordering);
                if (!flag9 && !flag10 && !flag11 && !flag12 && !flag13 && !flag14 && !flag15 && !flag16) { return; }
                int indexGivenSides2 = borderGrid.GetIndexGivenSides(flag9, flag10, flag11, flag12, flag13, flag14, flag15, flag16);
                if (borderGrid.PitBorderOverridesFloorTile) {
                    tileMap.SetTile(cell.positionInTilemap.x, cell.positionInTilemap.y, GlobalDungeonData.floorLayerIndex, indexGivenSides2);
                } else {
                    tileMapLayer.SetTile(cell.positionInTilemap.x, cell.positionInTilemap.y, indexGivenSides2);
                }
                if (borderGrid.PitBorderOverridesFloorTile) {
                    TileIndexGrid pitLayoutGrid = d2.roomMaterialDefinitions[cell.cellVisualData.roomVisualTypeIndex].pitLayoutGrid;
                    if (pitLayoutGrid == null) { pitLayoutGrid = d2.roomMaterialDefinitions[0].pitLayoutGrid; }
                    tileMap.Layers[GlobalDungeonData.pitLayerIndex].SetTile(cell.positionInTilemap.x, cell.positionInTilemap.y, pitLayoutGrid.centerIndices.GetIndexByWeight());
                }
            }
        }

        private void HandlePathTilePlacement(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, TileIndexGrid pathGrid) {
            List<CellData> cellNeighbors = d.data.GetCellNeighbors(current, true);
            bool[] array = new bool[8];
            for (int i = 0; i < array.Length; i++) {
                if (current.cellVisualData.pathTilesetGridIndex == cellNeighbors[i].cellVisualData.pathTilesetGridIndex) { array[i] = true; }
            }
            int num = pathGrid.GetIndexGivenSides(!array[0], !array[2], !array[4], !array[6]);
            int num2 = GlobalDungeonData.patternLayerIndex;
            if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.MINEGEON && current.type != CellType.PIT) {
                if (array[0] == array[4] && array[0] != array[2] && array[0] != array[6]) { num += ((!array[0]) ? 2 : 1); }
            } else if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.MINEGEON) {
                num2 = GlobalDungeonData.killLayerIndex;
                if (cellNeighbors[4] != null && !array[4] && cellNeighbors[4].type == CellType.PIT) {
                    int tile = pathGrid.PathPitPosts.indices[cellNeighbors[4].cellVisualData.roomVisualTypeIndex];
                    if (array[0] && array[2]) {
                        tile = pathGrid.PathPitPostsBL.indices[cellNeighbors[4].cellVisualData.roomVisualTypeIndex];
                    } else if (array[0] && array[6]) {
                        tile = pathGrid.PathPitPostsBR.indices[cellNeighbors[4].cellVisualData.roomVisualTypeIndex];
                    }
                    map.Layers[GlobalDungeonData.killLayerIndex].SetTile(cellNeighbors[4].positionInTilemap.x, cellNeighbors[4].positionInTilemap.y, tile);
                }
            }
            map.Layers[num2].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, num);
        }

        private void ProcessFacewallType(CellData current, Dungeon d, Dungeon d2, tk2dTileMap map, int ix, int iy, TilesetIndexMetadata.TilesetFlagType wallType, TilesetIndexMetadata.TilesetFlagType tileOverrideType) {
            int num = current.cellVisualData.roomVisualTypeIndex;
            if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON && num == 0) {
                bool flag = false;
                int num2 = -1;
                for (int i = 0; i < current.nearestRoom.connectedRooms.Count; i++) {
                    if (current.nearestRoom.GetDirectionToConnectedRoom(current.nearestRoom.connectedRooms[i]) == DungeonData.Direction.NORTH && current.nearestRoom.connectedRooms[i].RoomVisualSubtype != 0) {
                        flag = true;
                        num2 = current.nearestRoom.connectedRooms[i].RoomVisualSubtype;
                        break;
                    }
                }
                if (flag && current.cellVisualData.IsFacewallForInteriorTransition) { num = num2; }
            }
            CellData cellData = d.data.cellData[ix + 1][iy];
            CellData cellData2 = d.data.cellData[ix - 1][iy];
            if (current.cellVisualData.faceWallOverrideIndex != -1) {
                List<IndexNeighborDependency> dependencies = d2.tileIndices.dungeonCollection.GetDependencies(current.cellVisualData.faceWallOverrideIndex);
                if (dependencies != null && dependencies.Count > 0 && current.IsUpperFacewall()) {
                    foreach (IndexNeighborDependency dependency in dependencies) {
                        if (dependency.neighborDirection == DungeonData.Direction.NORTH) {
                            d.data.cellData[ix][iy + 1].cellVisualData.UsesCustomIndexOverride01 = true;
                            d.data.cellData[ix][iy + 1].cellVisualData.CustomIndexOverride01 = dependency.neighborIndex;
                            d.data.cellData[ix][iy + 1].cellVisualData.CustomIndexOverride01Layer = GlobalDungeonData.borderLayerIndex;
                        }
                    }
                    /*for (int j = 0; j < dependencies.Count; j++) {
                        if (dependencies[j].neighborDirection == DungeonData.Direction.NORTH) {
                            d.data.cellData[ix][iy + 1].cellVisualData.UsesCustomIndexOverride01 = true;
                            d.data.cellData[ix][iy + 1].cellVisualData.CustomIndexOverride01 = dependencies[j].neighborIndex;
                            d.data.cellData[ix][iy + 1].cellVisualData.CustomIndexOverride01Layer = GlobalDungeonData.borderLayerIndex;
                        }
                    }*/
                }
                map.Layers[GlobalDungeonData.collisionLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, current.cellVisualData.faceWallOverrideIndex);
            } else {
                if (current.diagonalWallType != DiagonalWallType.NONE) {
                    int tile = -1;
                    if (current.diagonalWallType == DiagonalWallType.NORTHEAST) {
                        if (wallType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER) {
                            GetMetadataFromTupleArray(current, m_metadataLookupTable[TilesetIndexMetadata.TilesetFlagType.DIAGONAL_FACEWALL_LOWER_NE], num, out tile);
                            map.Layers[GlobalDungeonData.collisionLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tile);
                        } else if (wallType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER) {
                            GetMetadataFromTupleArray(current, m_metadataLookupTable[TilesetIndexMetadata.TilesetFlagType.DIAGONAL_FACEWALL_UPPER_NE], num, out tile);
                            map.Layers[GlobalDungeonData.collisionLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tile);
                            GetMetadataFromTupleArray(current, m_metadataLookupTable[TilesetIndexMetadata.TilesetFlagType.DIAGONAL_FACEWALL_TOP_NE], num, out tile);
                            map.Layers[GlobalDungeonData.collisionLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, tile);
                        }
                    } else if (current.diagonalWallType == DiagonalWallType.NORTHWEST) {
                        if (wallType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER) {
                            GetMetadataFromTupleArray(current, m_metadataLookupTable[TilesetIndexMetadata.TilesetFlagType.DIAGONAL_FACEWALL_LOWER_NW], num, out tile);
                            map.Layers[GlobalDungeonData.collisionLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tile);
                        } else if (wallType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER) {
                            GetMetadataFromTupleArray(current, m_metadataLookupTable[TilesetIndexMetadata.TilesetFlagType.DIAGONAL_FACEWALL_UPPER_NW], num, out tile);
                            map.Layers[GlobalDungeonData.collisionLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tile);
                            GetMetadataFromTupleArray(current, m_metadataLookupTable[TilesetIndexMetadata.TilesetFlagType.DIAGONAL_FACEWALL_TOP_NW], num, out tile);
                            map.Layers[GlobalDungeonData.collisionLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, tile);
                        }
                    } else {
                        Debug.LogError("Attempting to stamp a facewall tile on a non-facewall diagonal type.");
                    }
                    if (wallType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER) {
                        if (current.diagonalWallType == DiagonalWallType.NORTHEAST) {
                            AssignSpecificColorsToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.collisionLayerIndex, new Color(0f, 0f, 1f), new Color(0f, 0f, 1f), new Color(0f, 0f, 1f), new Color(0f, 0.5f, 1f), map);
                        } else if (current.diagonalWallType == DiagonalWallType.NORTHWEST) {
                            AssignSpecificColorsToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.collisionLayerIndex, new Color(0f, 0f, 1f), new Color(0f, 0f, 1f), new Color(0f, 0.5f, 1f), new Color(0f, 0f, 1f), map);
                        }
                    } else if (wallType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER) {
                        if (current.diagonalWallType == DiagonalWallType.NORTHEAST) {
                            AssignSpecificColorsToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.collisionLayerIndex, new Color(0f, 0f, 1f), new Color(0f, 0.5f, 1f), new Color(0f, 0.5f, 1f), new Color(0f, 1f, 1f), map);
                        } else if (current.diagonalWallType == DiagonalWallType.NORTHWEST) {
                            AssignSpecificColorsToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.collisionLayerIndex, new Color(0f, 0.5f, 1f), new Color(0f, 0f, 1f), new Color(0f, 1f, 1f), new Color(0f, 0.5f, 1f), map);
                        }
                    }
                    return;
                }
                int num3 = -1;
                bool flag2 = false;
                int num4 = 0;
                while (!flag2 && num4 < 1000) {
                    num4++;
                    flag2 = true;
                    TilesetIndexMetadata metadataFromTupleArray = GetMetadataFromTupleArray(current, m_metadataLookupTable[tileOverrideType], num, out num3);
                    List<IndexNeighborDependency> dependencies2 = d2.tileIndices.dungeonCollection.GetDependencies(num3);
                    if (metadataFromTupleArray != null && dependencies2 != null && dependencies2.Count > 0) {
                        flag2 = ProcessFacewallNeighborMetadata(ix, iy, d, dependencies2, metadataFromTupleArray.preventWallStamping);
                    }
                }
                if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON && (tileOverrideType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER_RIGHTCORNER || tileOverrideType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER_LEFTCORNER || tileOverrideType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER_RIGHTCORNER || tileOverrideType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_UPPER_LEFTCORNER)) {
                    current.cellVisualData.containsWallSpaceStamp = true;
                }
                BraveUtility.Assert(num3 == -1, "FACEWALL INDEX -1, there are no facewalls defined", false);
                map.Layers[GlobalDungeonData.collisionLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, num3);
            }
            if (current.parentRoom == null || current.parentRoom.area.prototypeRoom == null || !current.parentRoom.area.prototypeRoom.preventFacewallAO) {
                if (wallType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOBottomWallBaseTileIndex);
                }
                bool flag3 = cellData.type == CellType.WALL && cellData.diagonalWallType == DiagonalWallType.NONE && (!d.data.isFaceWallRight(ix, iy) || (wallType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER && cellData.IsUpperFacewall()));
                bool flag4 = cellData2.type == CellType.WALL && cellData2.diagonalWallType == DiagonalWallType.NONE && (!d.data.isFaceWallLeft(ix, iy) || (wallType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER && cellData2.IsUpperFacewall()));
                if (flag4 && flag3) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, (wallType != TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER) ? m_tileIndices.aoTileIndices.AOTopFacewallBothIndex : m_tileIndices.aoTileIndices.AOBottomWallTileBothIndex);
                } else if (flag4) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, (wallType != TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER) ? m_tileIndices.aoTileIndices.AOTopFacewallLeftIndex : m_tileIndices.aoTileIndices.AOBottomWallTileLeftIndex);
                } else if (flag3) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, (wallType != TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER) ? m_tileIndices.aoTileIndices.AOTopFacewallRightIndex : m_tileIndices.aoTileIndices.AOBottomWallTileRightIndex);
                }
            }
            if (wallType == TilesetIndexMetadata.TilesetFlagType.FACEWALL_LOWER) {
                AssignColorGradientToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.collisionLayerIndex, new Color(0f, 0f, 1f), new Color(0f, 0.5f, 1f), map);
            } else {
                AssignColorGradientToTile(current.positionInTilemap.x, current.positionInTilemap.y, GlobalDungeonData.collisionLayerIndex, new Color(0f, 0.5f, 1f), new Color(0f, 1f, 1f), map);
            }
        }

        private void BuildOcclusionLayerCenterJungle(CellData current, Dungeon d, tk2dTileMap map, int ix, int iy) {
            if (!IsValidJungleOcclusionCell(current, d, ix, iy)) { return; }

            bool flag = true;
            bool flag2 = true;
            if (!BCheck(d, ix, iy)) {
                flag = false;
                flag2 = false;
            }
            if (current.UniqueHash < 0.05f) {
                flag = false;
                flag2 = false;
            }
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (!IsValidJungleOcclusionCell(d.data[ix + i, iy + j], d, ix + i, iy + j)) {
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

        private void BuildBorderLayerCenterJungle(CellData current, Dungeon d, tk2dTileMap map, int ix, int iy) {
            if (!IsValidJungleBorderCell(current, d, ix, iy)) { return; }
            
            bool flag = true;
            bool flag2 = true;
            if (!BCheck(d, ix, iy)) {
                flag = false;
                flag2 = false;
            }
            if (current.UniqueHash < 0.05f) {
                flag = false;
                flag2 = false;
            }
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
            bool IsValid = false;
            try {
                IsValid = !current.cellVisualData.ceilingHasBeenProcessed && !IsCardinalBorder(current, d, ix, iy) && current.type == CellType.WALL && (iy < 2 || !d.data.isFaceWallLower(ix, iy)) && !d.data.isTopDiagonalWall(ix, iy);
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    Debug.Log("[ExpandTheGungeon] Excpetion caught in ExpandTK2DDungeonAssembler.IsValidJungleBorderCell!");
                    Debug.LogException(ex);
                }
            }
            return IsValid;
        }

        private bool IsValidJungleOcclusionCell(CellData current, Dungeon d, int ix, int iy) {
            bool IsValid = false;
            try {
                IsValid = BCheck(d, ix, iy, 1) && (!current.cellVisualData.ceilingHasBeenProcessed && !current.cellVisualData.occlusionHasBeenProcessed) && (current.type != CellType.WALL || IsCardinalBorder(current, d, ix, iy) || (iy > 2 && (d.data.isFaceWallLower(ix, iy) || d.data.isFaceWallHigher(ix, iy))));
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    Debug.Log("[ExpandTheGungeon] Excpetion caught in ExpandTK2DDungeonAssembler.IsValidJungleOcclusionCell!");
                    Debug.LogException(ex);
                }
            }
            return IsValid;
        }
        
        
        // Everything here is unmodified (aside from formatting changes)

        private RoomInternalMaterialTransition GetMaterialTransitionFromSubtypes(Dungeon d, int roomType, int cellType) {
            if (!d.roomMaterialDefinitions[roomType].usesInternalMaterialTransitions) { return null; }
            if (roomType == cellType) { return null; }
            foreach (RoomInternalMaterialTransition materialTransition in d.roomMaterialDefinitions[roomType].internalMaterialTransitions) {
                if (materialTransition.materialTransition == cellType) { return materialTransition; }
            }
            return null;
        }
        
        // Appears to be unused?
        /*private TileIndexGrid GetCeilingBorderIndexGrid(CellData current, Dungeon d)
        {
            TileIndexGrid roomCeilingBorderGrid = d.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].roomCeilingBorderGrid;
            if (roomCeilingBorderGrid == null)
            {
                roomCeilingBorderGrid = d.roomMaterialDefinitions[0].roomCeilingBorderGrid;
            }
            return roomCeilingBorderGrid;
        }*/

        private void BuildShadowIndex(CellData current, Dungeon d, tk2dTileMap map, int ix, int iy) {
            if (BCheck(d, ix, iy, -2)) {
                CellData cellData = d.data.cellData[ix - 1][iy];
                CellData cellData2 = d.data.cellData[ix + 1][iy];
                CellData cellData3 = d.data.cellData[ix][iy + 1];
                CellData cellData4 = d.data.cellData[ix + 1][iy + 1];
                CellData cellData5 = d.data.cellData[ix - 1][iy + 1];
                bool flag = cellData.type == CellType.WALL && cellData.diagonalWallType == DiagonalWallType.NONE;
                bool flag2 = cellData2.type == CellType.WALL && cellData2.diagonalWallType == DiagonalWallType.NONE;
                bool flag3 = cellData3.type == CellType.WALL;
                bool flag4 = cellData4.type == CellType.WALL && cellData4.diagonalWallType == DiagonalWallType.NONE;
                bool flag5 = cellData5.type == CellType.WALL && cellData5.diagonalWallType == DiagonalWallType.NONE;
                if (current.parentRoom != null && current.parentRoom.area.prototypeRoom != null && current.parentRoom.area.prototypeRoom.preventFacewallAO) {
                    flag3 = false;
                    flag4 = false;
                    flag5 = false;
                }
                bool flag6 = cellData3.isSecretRoomCell != current.isSecretRoomCell;
                if (cellData3.diagonalWallType == DiagonalWallType.NONE) {
                    if (flag3 && flag && !flag2) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallUpAndLeft);
                    } else if (flag3 && flag2 && !flag) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallUpAndRight);
                    } else if (flag3 && flag && flag2) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallUpAndBoth);
                    } else if (flag3 && !flag && !flag2) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorTileIndex);
                    }
                } else if (cellData3.diagonalWallType == DiagonalWallType.NORTHEAST && cellData3.type == CellType.WALL) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, m_tileIndices.aoTileIndices.AOFloorDiagonalWallNortheast);
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, (!flag2) ? m_tileIndices.aoTileIndices.AOFloorDiagonalWallNortheastLower : m_tileIndices.aoTileIndices.AOFloorDiagonalWallNortheastLowerJoint);
                } else if (cellData3.diagonalWallType == DiagonalWallType.NORTHWEST && cellData3.type == CellType.WALL) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, m_tileIndices.aoTileIndices.AOFloorDiagonalWallNorthwest);
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, (!flag) ? m_tileIndices.aoTileIndices.AOFloorDiagonalWallNorthwestLower : m_tileIndices.aoTileIndices.AOFloorDiagonalWallNorthwestLowerJoint);
                }
                if (!flag3) {
                    bool flag7 = flag && !d.data.isTopWall(ix - 1, iy + 1);
                    bool flag8 = flag2 && !d.data.isTopWall(ix + 1, iy + 1);
                    if (flag7 && flag8) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallBoth);
                    } else if (flag7) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallLeft);
                    } else if (flag8) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorWallRight);
                    }
                }
                if (!flag3 && flag5 && !flag6 && !flag && !flag2 && !flag4) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorPizzaSliceLeft);
                } else if (!flag3 && !flag5 && !flag && !flag2 && flag4 && !flag6) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorPizzaSliceRight);
                } else if (!flag3 && flag5 && !flag6 && !flag2 && !flag && flag4 && !flag6) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorPizzaSliceBoth);
                } else if (!flag3 && flag5 && !flag6 && !flag && flag2) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorPizzaSliceLeftWallRight);
                } else if (!flag3 && flag && !flag2 && flag4 && !flag6) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, m_tileIndices.aoTileIndices.AOFloorPizzaSliceRightWallLeft);
                }
            }
        }
        
        private TileIndexVariant GetIndexFromTileArray(CellData current, List<TileIndexVariant> list) {
            float uniqueHash = current.UniqueHash;
            float num = 0f;
            foreach (TileIndexVariant variant in list) { num += variant.likelihood; }
            float num2 = uniqueHash * num;
            foreach (TileIndexVariant variant in list) {
                num2 -= variant.likelihood;
                if (num2 <= 0f) { return variant; }
            }
            return list[0];
        }

        private int GetIndexFromTupleArray(CellData current, List<Tuple<int, TilesetIndexMetadata>> list, int roomTypeIndex) {
            float uniqueHash = current.UniqueHash;
            float num = 0f;
            foreach (Tuple<int, TilesetIndexMetadata> MetadataTuple in list) {
                if (MetadataTuple.Second.dungeonRoomSubType == roomTypeIndex || MetadataTuple.Second.secondRoomSubType == roomTypeIndex || MetadataTuple.Second.thirdRoomSubType == roomTypeIndex) {
                    num += MetadataTuple.Second.weight;
                }
            }
            float num2 = uniqueHash * num;
            foreach (Tuple<int, TilesetIndexMetadata> MetadataTuple in list) {
                if (MetadataTuple.Second.dungeonRoomSubType == roomTypeIndex || MetadataTuple.Second.secondRoomSubType == roomTypeIndex || MetadataTuple.Second.thirdRoomSubType == roomTypeIndex) {
                    num2 -= MetadataTuple.Second.weight;
                    if (num2 <= 0f) { return MetadataTuple.First; }
                }
            }
            return list[0].First;
        }

        private TilesetIndexMetadata GetMetadataFromTupleArray(CellData current, List<Tuple<int, TilesetIndexMetadata>> list, int roomTypeIndex, out int index) {
            if (list == null) {
                index = -1;
                return null;
            }
            float num = 0f;
            foreach (Tuple<int, TilesetIndexMetadata> MetadataTuple in list) {
                if (MetadataTuple.Second.dungeonRoomSubType == -1 || MetadataTuple.Second.dungeonRoomSubType == roomTypeIndex || MetadataTuple.Second.secondRoomSubType == roomTypeIndex || MetadataTuple.Second.thirdRoomSubType == roomTypeIndex) {
                    num += MetadataTuple.Second.weight;
                }
            }
            float num2 = UnityEngine.Random.value * num;
            foreach (Tuple<int, TilesetIndexMetadata> MetadataTuple in list) {
                if (MetadataTuple.Second.dungeonRoomSubType == -1 || MetadataTuple.Second.dungeonRoomSubType == roomTypeIndex || MetadataTuple.Second.secondRoomSubType == roomTypeIndex || MetadataTuple.Second.thirdRoomSubType == roomTypeIndex) {
                    num2 -= MetadataTuple.Second.weight;
                    if (num2 <= 0f) {
                        index = MetadataTuple.First;
                        return MetadataTuple.Second;
                    }
                }
            }
            index = list[0].First;
            return list[0].Second;
        }

        public void ClearData(tk2dTileMap map) {
            for (int i = 0; i < map.Layers.Length; i++) { map.DeleteSprites(i, 0, 0, map.width - 1, map.height - 1); }
        }
        
        public void ClearTileIndicesForCell(Dungeon d, tk2dTileMap map, int ix, int iy) {
            CellData cellData = (!d.data.CheckInBoundsAndValid(ix, iy)) ? null : d.data[ix, iy];
            int x = (cellData == null) ? ix : cellData.positionInTilemap.x;
            int y = (cellData == null) ? iy : cellData.positionInTilemap.y;
            for (int i = 0; i < map.Layers.Length; i++) { map.Layers[i].SetTile(x, y, -1); }
            if (TK2DTilemapChunkAnimator.PositionToAnimatorMap.ContainsKey(cellData.positionInTilemap)) {
                for (int j = 0; j < TK2DTilemapChunkAnimator.PositionToAnimatorMap[cellData.positionInTilemap].Count; j++) {
                    TilemapAnimatorTileManager tilemapAnimatorTileManager = TK2DTilemapChunkAnimator.PositionToAnimatorMap[cellData.positionInTilemap][j];
                    if (tilemapAnimatorTileManager.animator) {
                        TK2DTilemapChunkAnimator.PositionToAnimatorMap[cellData.positionInTilemap].RemoveAt(j);
                        j--;
                        UnityEngine.Object.Destroy(tilemapAnimatorTileManager.animator.gameObject);
                        tilemapAnimatorTileManager.animator = null;
                    }
                }
            }
        }

        private bool CheckLostWoodsCellValidity(Dungeon d, IntVector2 p1, IntVector2 p2) {
            CellData cellData = d.data[p1];
            CellData cellData2 = d.data[p2];
            return cellData != null && cellData2 != null && cellData2.isExitCell == cellData.isExitCell && cellData2.IsAnyFaceWall() == cellData.IsAnyFaceWall() && cellData2.IsTopWall() == cellData.IsTopWall() && cellData2.type == cellData.type;
        }

        private void HandleLostWoodsMirroring(CellData current, Dungeon d, tk2dTileMap map, int ix, int iy) {
            RoomHandler nearestRoom = current.nearestRoom;
            IntVector2 a = new IntVector2(ix - current.nearestRoom.area.basePosition.x, iy - current.nearestRoom.area.basePosition.y);
            foreach (RoomHandler roomHandler in d.data.rooms) {
                if (roomHandler != nearestRoom && roomHandler.area.PrototypeLostWoodsRoom) {
                    CellData cellData = d.data[a + roomHandler.area.basePosition];
                    if (cellData != null && current != null) {
                        if (cellData.isExitCell == current.isExitCell) {
                            if (cellData.IsAnyFaceWall() == current.IsAnyFaceWall()) {
                                if (cellData.IsTopWall() == current.IsTopWall()) {
                                    if (cellData.type == current.type) {
                                        if (CheckLostWoodsCellValidity(d, current.position + new IntVector2(0, 1), cellData.position + new IntVector2(0, 1))) {
                                            if (CheckLostWoodsCellValidity(d, current.position + new IntVector2(0, -1), cellData.position + new IntVector2(0, -1))) {
                                                if (CheckLostWoodsCellValidity(d, current.position + new IntVector2(1, 0), cellData.position + new IntVector2(1, 0))) {
                                                    if (CheckLostWoodsCellValidity(d, current.position + new IntVector2(-1, 0), cellData.position + new IntVector2(-1, 0))) {
                                                        if (!cellData.cellVisualData.hasAlreadyBeenTilemapped) {
                                                            cellData.cellVisualData.hasAlreadyBeenTilemapped = true;
                                                            for (int j = 0; j < map.Layers.Length; j++) {
                                                                map.Layers[j].SetTile(cellData.positionInTilemap.x, cellData.positionInTilemap.y, map.Layers[j].GetTile(current.positionInTilemap.x, current.positionInTilemap.y));
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        private void AssignSpecificColorsToTile(int ix, int iy, int layer, Color32 bottomLeft, Color32 bottomRight, Color32 topLeft, Color32 topRight, tk2dTileMap map) {
            if (!map.HasColorChannel()) { map.CreateColorChannel(); }
            ColorChannel colorChannel = map.ColorChannel;
            map.data.Layers[layer].useColor = true;
            colorChannel.SetTileColorGradient(ix, iy, bottomLeft, bottomRight, topLeft, topRight);
        }

        private void AssignColorGradientToTile(int ix, int iy, int layer, Color32 bottom, Color32 top, tk2dTileMap map) {
            if (!map.HasColorChannel()) { map.CreateColorChannel(); }
            ColorChannel colorChannel = map.ColorChannel;
            map.data.Layers[layer].useColor = true;
            colorChannel.SetTileColorGradient(ix, iy, bottom, bottom, top, top);
        }

        private void AssignColorOverrideToTile(int ix, int iy, int layer, Color32 color, tk2dTileMap map) {
            if (!map.HasColorChannel()) { map.CreateColorChannel(); }
            ColorChannel colorChannel = map.ColorChannel;
            map.data.Layers[layer].useColor = true;
            colorChannel.SetTileColorOverride(ix, iy, color);
        }

        private void ClearAllIndices(CellData current, Dungeon d, tk2dTileMap map, int ix, int iy) {
            for (int i = 0; i < map.Layers.Length; i++) {
                map.Layers[i].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, -1);
            }
        }

        private bool CheckHasValidFloorGridForRoomSubType(List<TileIndexGrid> indexGrids, int roomType) {
            foreach (TileIndexGrid indexGrid in indexGrids) {
                if (indexGrid.roomTypeRestriction == -1 || indexGrid.roomTypeRestriction == roomType) { return true; }
            }
            return false;
        }
        
        private int GetCeilingCenterIndex(CellData current, TileIndexGrid gridToUse) {
            if (gridToUse.CeilingBorderUsesDistancedCenters) {
                int count = gridToUse.centerIndices.indices.Count;
                int index = Mathf.Max(0, Mathf.Min(Mathf.FloorToInt(current.distanceFromNearestRoom) - 1, count - 1));
                return gridToUse.centerIndices.indices[index];
            }
            return gridToUse.centerIndices.GetIndexByWeight();
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
        
        private bool IsBorderCell(Dungeon d, int ix, int iy) {
            bool flag = d.data[ix, iy + 1].diagonalWallType != DiagonalWallType.NONE && (d.data[ix, iy + 1].IsTopWall() || d.data[ix, iy + 1].type == CellType.WALL);
            bool flag2 = d.data[ix + 1, iy].diagonalWallType != DiagonalWallType.NONE && (d.data[ix + 1, iy].IsTopWall() || d.data[ix + 1, iy].type == CellType.WALL);
            bool flag3 = d.data[ix, iy - 1].diagonalWallType != DiagonalWallType.NONE && (d.data[ix, iy - 1].IsTopWall() || d.data[ix, iy - 1].type == CellType.WALL);
            bool flag4 = d.data[ix - 1, iy].diagonalWallType != DiagonalWallType.NONE && (d.data[ix - 1, iy].IsTopWall() || d.data[ix - 1, iy].type == CellType.WALL);
            bool flag5 = d.data.isTopWall(ix, iy);
            flag5 = (flag5 && !d.data[ix, iy + 1].cellVisualData.shouldIgnoreBorders);
            bool flag6 = (!d.data.isWallRight(ix, iy) && !d.data.isRightTopWall(ix, iy)) || d.data.isFaceWallHigher(ix + 1, iy) || d.data.isFaceWallLower(ix + 1, iy);
            flag6 = (flag6 && !d.data[ix + 1, iy].cellVisualData.shouldIgnoreBorders);
            bool flag7 = iy > 3 && d.data.isFaceWallHigher(ix, iy - 1);
            flag7 = (flag7 && !d.data[ix, iy - 1].cellVisualData.shouldIgnoreBorders);
            bool flag8 = (!d.data.isWallLeft(ix, iy) && !d.data.isLeftTopWall(ix, iy)) || d.data.isFaceWallHigher(ix - 1, iy) || d.data.isFaceWallLower(ix - 1, iy);
            flag8 = (flag8 && !d.data[ix - 1, iy].cellVisualData.shouldIgnoreBorders);
            bool flag9 = (!flag || !flag2) && d.data.isTopWall(ix + 1, iy) && !d.data.isTopWall(ix, iy) && (d.data.isWall(ix, iy + 1) || d.data.isTopWall(ix, iy + 1));
            flag9 = (flag9 && !d.data[ix + 1, iy + 1].cellVisualData.shouldIgnoreBorders);
            bool flag10 = (!flag || !flag4) && d.data.isTopWall(ix - 1, iy) && !d.data.isTopWall(ix, iy) && (d.data.isWall(ix, iy + 1) || d.data.isTopWall(ix, iy + 1));
            flag10 = (flag10 && !d.data[ix - 1, iy + 1].cellVisualData.shouldIgnoreBorders);
            bool flag11 = (!flag3 || !flag2) && iy > 3 && d.data.isFaceWallHigher(ix + 1, iy - 1) && !d.data.isFaceWallHigher(ix, iy - 1);
            flag11 = (flag11 && !d.data[ix + 1, iy - 1].cellVisualData.shouldIgnoreBorders);
            bool flag12 = (!flag3 || !flag4) && iy > 3 && d.data.isFaceWallHigher(ix - 1, iy - 1) && !d.data.isFaceWallHigher(ix, iy - 1);
            flag12 = (flag12 && !d.data[ix - 1, iy - 1].cellVisualData.shouldIgnoreBorders);
            return flag5 || flag6 || flag8 || flag7 || flag9 || flag10 || flag11 || flag12;
        }

        private void HandleRatChunkOverhangs(Dungeon d, int ix, int iy, tk2dTileMap map) {
            bool flag = IsBorderCell(d, ix, iy + 1);
            bool flag2 = IsBorderCell(d, ix + 1, iy);
            bool flag3 = IsBorderCell(d, ix, iy - 1);
            bool flag4 = IsBorderCell(d, ix - 1, iy);
            bool flag5 = (flag && flag2) || (flag2 && flag3) || (flag3 && flag4) || (flag4 && flag);
            if (flag5) {
                if (!flag) {
                    map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(d.data[ix, iy + 1].positionInTilemap.x, d.data[ix, iy + 1].positionInTilemap.y, 312);
                    d.data[ix, iy + 1].cellVisualData.ceilingHasBeenProcessed = true;
                }
                if (!flag2) {
                    map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(d.data[ix + 1, iy].positionInTilemap.x, d.data[ix + 1, iy].positionInTilemap.y, 315);
                    d.data[ix + 1, iy].cellVisualData.ceilingHasBeenProcessed = true;
                }
                if (!flag3) {
                    map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(d.data[ix, iy - 1].positionInTilemap.x, d.data[ix, iy - 1].positionInTilemap.y, 313);
                }
                if (!flag4) {
                    map.Layers[GlobalDungeonData.borderLayerIndex].SetTile(d.data[ix - 1, iy].positionInTilemap.x, d.data[ix - 1, iy].positionInTilemap.y, 314);
                }
            }
        }
                
        private bool ProcessFacewallNeighborMetadata(int ix, int iy, Dungeon d, List<IndexNeighborDependency> neighborDependencies, bool preventWallStamping = false) {
            bool flag = d.data.isFaceWallLower(ix, iy);
            CellData cellData = d.data[ix, iy];
            cellData.cellVisualData.containsWallSpaceStamp = (cellData.cellVisualData.containsWallSpaceStamp || preventWallStamping);
            bool flag2 = true;
            List<CellData> list = new List<CellData>();
            foreach (IndexNeighborDependency dependency in neighborDependencies) {
                CellData cellData2 = d.data[new IntVector2(ix, iy) + DungeonData.GetIntVector2FromDirection(dependency.neighborDirection)];
                if (cellData2.cellVisualData.faceWallOverrideIndex != -1 || !cellData2.IsAnyFaceWall()) {
                    flag2 = false;
                    break;
                }
                if (cellData2.cellVisualData.roomVisualTypeIndex != d.data.cellData[ix][iy].cellVisualData.roomVisualTypeIndex) {
                    flag2 = false;
                    break;
                }
                if (cellData2.position.y == iy && d.data.isFaceWallLower(cellData2.position.x, cellData2.position.y) != flag) {
                    flag2 = false;
                    break;
                }
                list.Add(cellData2);
                CellData cellData3 = cellData2;
                cellData3.cellVisualData.containsWallSpaceStamp = (cellData3.cellVisualData.containsWallSpaceStamp || preventWallStamping);
                cellData2.cellVisualData.faceWallOverrideIndex = dependency.neighborIndex;
            }
            if (!flag2) {
                foreach (CellData celldata in list) { celldata.cellVisualData.faceWallOverrideIndex = -1; }
            }
            return flag2;
        }

        private bool FaceWallTypesMatch(CellData c1, CellData c2) {
            return (c1.IsLowerFaceWall() && c2.IsLowerFaceWall()) || (c1.IsUpperFacewall() && c2.IsUpperFacewall());
        }

        private bool IsNorthernmostColumnarFacewall(CellData current, Dungeon d, int ix, int iy) {
            for (CellData cellData = d.data[ix, iy + 1]; cellData != null; cellData = d.data[cellData.position.x, cellData.position.y + 1]) {
                if (cellData.nearestRoom != current.nearestRoom) { return true; }
                if (cellData.type == CellType.FLOOR) { return false; }
                if (!d.data.CheckInBounds(cellData.position.x, cellData.position.y + 1)) { return true; }
            }
            return true;
        }
        
        private int FindValidFacewallExpanse(int ix, int iy, Dungeon d, FacewallIndexGridDefinition gridDefinition) {
            int num = 0;
            int roomVisualTypeIndex = d.data[ix, iy].cellVisualData.roomVisualTypeIndex;
            while (d.data.isFaceWallLower(ix, iy) && d.data[ix, iy].cellVisualData.faceWallOverrideIndex == -1 && d.data[ix, iy].cellVisualData.roomVisualTypeIndex == roomVisualTypeIndex) {
                bool flag = !d.data.isFaceWallLeft(ix, iy) || !d.data.isFaceWallRight(ix, iy);
                if (!gridDefinition.canExistInCorners && flag) { break; }
                if (d.data[ix, iy - 2].isExitCell && !gridDefinition.canBePlacedInExits) { break; }
                ix++;
                num++;
                if (num >= gridDefinition.maxWidth) { break; }
                if (num > gridDefinition.minWidth && UnityEngine.Random.value < gridDefinition.perTileFailureRate) { break; }
            }
            return num;
        }

        private bool AssignFacewallGrid(CellData current, Dungeon d, tk2dTileMap map, int ix, int iy, FacewallIndexGridDefinition gridDefinition) {
            int num = FindValidFacewallExpanse(ix, iy, d, gridDefinition);
            if (num < gridDefinition.minWidth) { return false; }
            TileIndexGrid grid = gridDefinition.grid;
            int num2 = 0;
            int i = num;
            int num3 = i;
            int num4 = 0;
            if (gridDefinition.hasIntermediaries) {
                num3 = UnityEngine.Random.Range(gridDefinition.minIntermediaryBuffer, gridDefinition.maxIntermediaryBuffer + 1);
            }
            bool flag = true;
            int num5 = 0;
            while (i > 0) {
                CellData cellData = d.data[ix + num2, iy];
                CellData cellData2 = d.data[ix + num2, iy + 1];
                if (num4 > 0) {
                    num4--;
                    cellData.cellVisualData.faceWallOverrideIndex = grid.doubleNubsBottom.GetIndexByWeight();
                    cellData2.cellVisualData.faceWallOverrideIndex = grid.doubleNubsTop.GetIndexByWeight();
                    if (num4 == 0) {
                        flag = true;
                        num3 = UnityEngine.Random.Range(gridDefinition.minIntermediaryBuffer, gridDefinition.maxIntermediaryBuffer + 1);
                    }
                } else {
                    bool flag2 = false;
                    BraveUtility.DrawDebugSquare(cellData.position.ToVector2(), Color.blue, 1000f);
                    num3--;
                    if (num3 <= 0) {
                        if (gridDefinition.hasIntermediaries) {
                            num4 = UnityEngine.Random.Range(gridDefinition.minIntermediaryLength, gridDefinition.maxIntermediaryLength + 1);
                        }
                        flag2 = true;
                    }
                    if (flag) {
                        cellData.cellVisualData.faceWallOverrideIndex = grid.bottomLeftIndices.GetIndexByWeight();
                        cellData2.cellVisualData.faceWallOverrideIndex = grid.topLeftIndices.GetIndexByWeight();
                        cellData.cellVisualData.containsWallSpaceStamp = true;
                        cellData2.cellVisualData.containsWallSpaceStamp = true;
                    } else if (flag2 || i == 1) {
                        cellData.cellVisualData.faceWallOverrideIndex = grid.bottomRightIndices.GetIndexByWeight();
                        cellData2.cellVisualData.faceWallOverrideIndex = grid.topRightIndices.GetIndexByWeight();
                        cellData.cellVisualData.containsWallSpaceStamp = true;
                        cellData2.cellVisualData.containsWallSpaceStamp = true;
                        if (flag2 && num4 == 0) {
                            num3 = UnityEngine.Random.Range(gridDefinition.minIntermediaryBuffer, gridDefinition.maxIntermediaryBuffer + 1);
                        }
                    } else {
                        cellData.cellVisualData.faceWallOverrideIndex = ((!gridDefinition.middleSectionSequential) ? grid.bottomIndices.GetIndexByWeight() : grid.bottomIndices.indices[num5]);
                        if (gridDefinition.topsMatchBottoms) {
                            cellData2.cellVisualData.faceWallOverrideIndex = grid.topIndices.indices[grid.bottomIndices.GetIndexOfIndex(cellData.cellVisualData.faceWallOverrideIndex)];
                        } else {
                            cellData2.cellVisualData.faceWallOverrideIndex = ((!gridDefinition.middleSectionSequential) ? grid.topIndices.GetIndexByWeight() : grid.topIndices.indices[num5]);
                        }
                        num5 = (num5 + 1) % grid.bottomIndices.indices.Count;
                        cellData.cellVisualData.forcedMatchingStyle = gridDefinition.forcedStampMatchingStyle;
                        cellData2.cellVisualData.forcedMatchingStyle = gridDefinition.forcedStampMatchingStyle;
                    }
                    flag = false;
                    cellData.cellVisualData.containsObjectSpaceStamp = (cellData.cellVisualData.containsObjectSpaceStamp || !gridDefinition.canAcceptFloorDecoration);
                    cellData2.cellVisualData.containsObjectSpaceStamp = (cellData2.cellVisualData.containsObjectSpaceStamp || !gridDefinition.canAcceptFloorDecoration);
                    cellData.cellVisualData.facewallGridPreventsWallSpaceStamp = !gridDefinition.canAcceptWallDecoration;
                    cellData2.cellVisualData.facewallGridPreventsWallSpaceStamp = !gridDefinition.canAcceptWallDecoration;
                    cellData.cellVisualData.containsWallSpaceStamp = (cellData.cellVisualData.containsWallSpaceStamp || !gridDefinition.canAcceptWallDecoration);
                    cellData2.cellVisualData.containsWallSpaceStamp = (cellData2.cellVisualData.containsWallSpaceStamp || !gridDefinition.canAcceptWallDecoration);
                }
                num2++;
                i--;
            }
            return true;
        }
        
        private void HandlePitTilePlacement(CellData cell, TileIndexGrid pitGrid, Layer tileMapLayer, Dungeon d) {
            if (pitGrid == null) { return; }
            List<CellData> cellNeighbors = d.data.GetCellNeighbors(cell, false);
            bool flag = cellNeighbors[0] != null && cellNeighbors[0].type == CellType.PIT;
            bool flag2 = cellNeighbors[1] != null && cellNeighbors[1].type == CellType.PIT;
            bool flag3 = cellNeighbors[2] != null && cellNeighbors[2].type == CellType.PIT;
            bool flag4 = cellNeighbors[3] != null && cellNeighbors[3].type == CellType.PIT;
            bool flag5 = BCheck(d, cell.position.x, cell.position.y + 2) && d.data.cellData[cell.position.x][cell.position.y + 2].type == CellType.PIT;
            bool flag6 = BCheck(d, cell.position.x, cell.position.y + 3) && d.data.cellData[cell.position.x][cell.position.y + 3].type == CellType.PIT;
            if (cell.cellVisualData.pitOverrideIndex > -1) {
                tileMapLayer.SetTile(cell.positionInTilemap.x, cell.positionInTilemap.y, cell.cellVisualData.pitOverrideIndex);
            } else {
                if (d.debugSettings.WALLS_ARE_PITS) {
                    if (cellNeighbors[2] != null && cellNeighbors[2].isExitCell) { flag3 = true; }
                    if (cellNeighbors[0] != null && cellNeighbors[0].isExitCell) { flag = true; }
                    if (BCheck(d, cell.position.x, cell.position.y + 2) && d.data.cellData[cell.position.x][cell.position.y + 2].isExitCell) { flag5 = true; }
                    if (BCheck(d, cell.position.x, cell.position.y + 3) && d.data.cellData[cell.position.x][cell.position.y + 3].isExitCell) { flag6 = true; }
                }
                int tile = pitGrid.GetIndexGivenSides(!flag, !flag5, !flag6, !flag2, !flag3, !flag4);
                if (pitGrid.PitInternalSquareGrids.Count > 0 && UnityEngine.Random.value < pitGrid.PitInternalSquareOptions.PitSquareChance && (pitGrid.PitInternalSquareOptions.CanBeFlushLeft || flag4) && (pitGrid.PitInternalSquareOptions.CanBeFlushBottom || flag3) && flag2 && flag && flag5 && flag6) {
                    bool flag7 = BCheck(d, cell.position.x + 2, cell.position.y) && d.data.cellData[cell.position.x + 2][cell.position.y].type == CellType.PIT;
                    bool flag8 = BCheck(d, cell.position.x + 1, cell.position.y + 1) && d.data.cellData[cell.position.x + 1][cell.position.y + 1].type == CellType.PIT;
                    bool flag9 = BCheck(d, cell.position.x + 1, cell.position.y + 2) && d.data.cellData[cell.position.x + 1][cell.position.y + 2].type == CellType.PIT;
                    bool flag10 = BCheck(d, cell.position.x + 1, cell.position.y + 3) && d.data.cellData[cell.position.x + 1][cell.position.y + 3].type == CellType.PIT;
                    if ((pitGrid.PitInternalSquareOptions.CanBeFlushRight || flag7) && flag8 && flag10 && flag9) {
                        TileIndexGrid tileIndexGrid = pitGrid.PitInternalSquareGrids[UnityEngine.Random.Range(0, pitGrid.PitInternalSquareGrids.Count)];
                        tile = tileIndexGrid.bottomLeftIndices.GetIndexByWeight();
                        d.data.cellData[cell.position.x + 1][cell.position.y].cellVisualData.pitOverrideIndex = tileIndexGrid.bottomRightIndices.GetIndexByWeight();
                        d.data.cellData[cell.position.x][cell.position.y + 1].cellVisualData.pitOverrideIndex = tileIndexGrid.topLeftIndices.GetIndexByWeight();
                        d.data.cellData[cell.position.x + 1][cell.position.y + 1].cellVisualData.pitOverrideIndex = tileIndexGrid.topRightIndices.GetIndexByWeight();
                    }
                }
                tileMapLayer.SetTile(cell.positionInTilemap.x, cell.positionInTilemap.y, tile);
            }
            if (flag && !flag5) {
                AssignColorGradientToTile(cell.positionInTilemap.x, cell.positionInTilemap.y, GlobalDungeonData.pitLayerIndex, new Color(1f, 1f, 1f, 1f), new Color(0f, 0f, 0f, 1f), GameManager.Instance.Dungeon.MainTilemap);
            }
        }
    }
    
    public class ExpandTK2DInteriorDecorator {

        public ExpandTK2DInteriorDecorator(ExpandTK2DDungeonAssembler assembler) {
            roomUsedStamps = new List<StampDataBase>();
            expanseUsedStamps = new List<StampDataBase>();
            m_assembler = assembler;
            DEBUG_DRAW = false;
        }

        public bool DEBUG_DRAW;

        private ExpandTK2DDungeonAssembler m_assembler;

        private Dictionary<DungeonTileStampData.StampPlacementRule, IntVector2> wallPlacementOffsets;

        private List<ViableStampCategorySet> viableCategorySets;

        private List<DungeonTileStampData.StampPlacementRule> validNorthernPlacements;

        private List<DungeonTileStampData.StampPlacementRule> validEasternPlacements;

        private List<DungeonTileStampData.StampPlacementRule> validWesternPlacements;

        private List<DungeonTileStampData.StampPlacementRule> validSouthernPlacements;
        
        private List<StampDataBase> roomUsedStamps;

        private List<StampDataBase> expanseUsedStamps;

        protected class ViableStampCategorySet {
            public ViableStampCategorySet(DungeonTileStampData.StampCategory c, DungeonTileStampData.StampPlacementRule p, DungeonTileStampData.StampSpace s) {
                category = c;
                placement = p;
                space = s;
            }

            public override int GetHashCode() {
                return 1597 * category.GetHashCode() + 5347 * placement.GetHashCode() + 13 * space.GetHashCode();
            }

            public override bool Equals(object obj) {
                if (obj is ViableStampCategorySet) {
                    ViableStampCategorySet viableStampCategorySet = obj as ViableStampCategorySet;
                    return viableStampCategorySet.category == category && viableStampCategorySet.space == space && viableStampCategorySet.placement == placement;
                }
                return false;
            }

            public DungeonTileStampData.StampCategory category;

            public DungeonTileStampData.StampPlacementRule placement;

            public DungeonTileStampData.StampSpace space;
        }

        public enum DecorateErrorCode {
            ALL_OK,
            FAILED_SPACE,
            FAILED_CHANCE
        }

        public struct WallExpanse {
            public WallExpanse(IntVector2 bp, int w) {
                basePosition = bp;
                width = w;
                hasMirror = false;
                mirroredExpanseBasePosition = IntVector2.Zero;
                mirroredExpanseWidth = 0;
            }

            public IntVector2 GetPositionInMirroredExpanse(int basePlacement, int stampWidth) {
                IntVector2 a = mirroredExpanseBasePosition + mirroredExpanseWidth * IntVector2.Right;
                return a + (basePlacement + stampWidth) * IntVector2.Left;
            }

            public IntVector2 basePosition;

            public int width;

            public bool hasMirror;

            public IntVector2 mirroredExpanseBasePosition;

            public int mirroredExpanseWidth;
        }

        public static void PlaceLightDecorationForCell(Dungeon d, tk2dTileMap map, CellData currentCell, IntVector2 currentPosition) {
            if (currentCell.cellVisualData.containsLight && currentCell.cellVisualData.lightDirection != DungeonData.Direction.SOUTH && currentCell.cellVisualData.lightDirection != (DungeonData.Direction)(-1)) {
                DungeonTileStampData.StampPlacementRule stampPlacementRule = DungeonTileStampData.StampPlacementRule.ON_LOWER_FACEWALL;
                bool flipX = false;
                if (currentCell.cellVisualData.lightDirection == DungeonData.Direction.EAST) {
                    stampPlacementRule = DungeonTileStampData.StampPlacementRule.ALONG_LEFT_WALLS;
                    flipX = true;
                } else if (currentCell.cellVisualData.lightDirection == DungeonData.Direction.WEST) {
                    stampPlacementRule = DungeonTileStampData.StampPlacementRule.ALONG_LEFT_WALLS;
                }
                LightStampData lightStampData = (stampPlacementRule != DungeonTileStampData.StampPlacementRule.ALONG_LEFT_WALLS) ? currentCell.cellVisualData.facewallLightStampData : currentCell.cellVisualData.sidewallLightStampData;
                if (lightStampData != null) {
                    GameObject gameObject = ExpandTK2DDungeonAssembler.ApplyObjectStamp(currentPosition.x, currentPosition.y, lightStampData, d, map, flipX, true);
                    if (gameObject) {
                        TorchController component = gameObject.GetComponent<TorchController>();
                        if (component) { component.Cell = currentCell; }
                    } else if (currentCell.cellVisualData.lightObject != null) {
                        ShadowSystem componentInChildren = currentCell.cellVisualData.lightObject.GetComponentInChildren<ShadowSystem>();
                        if (componentInChildren) {
                            for (int i = 0; i < componentInChildren.PersonalCookies.Count; i++) {
                                componentInChildren.PersonalCookies[i].enabled = false;
                                componentInChildren.PersonalCookies.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
            }
        }
        
        private void DecorateRoomExit(RoomHandler r, RuntimeRoomExitData usedExit, Dungeon d, Dungeon d2, tk2dTileMap map) {
            RoomHandler roomHandler = r.connectedRoomsByExit[usedExit.referencedExit];
            if (usedExit.referencedExit.exitDirection == DungeonData.Direction.NORTH) {
                IntVector2 a = r.area.basePosition + usedExit.ExitOrigin - IntVector2.One;
                int num = 0;
                while (d.data.isFaceWallLower(a.x - num - 1, a.y)) { num++; }
                int num2 = 0;
                while (d.data.isFaceWallLower(a.x + usedExit.referencedExit.ExitCellCount + num2, a.y)) { num2++; }
                int num3 = Math.Min(num, num2);
                int num4 = 0;
                if (num3 > 0) {
                    for (int i = 0; i < 3; i++) {
                        IntVector2 b = IntVector2.Zero;
                        StampDataBase stampDataComplex;
                        if (i == 0 || i == 2) {
                            stampDataComplex = d2.stampData.GetStampDataComplex(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL, DungeonTileStampData.StampSpace.BOTH_SPACES, DungeonTileStampData.StampCategory.STRUCTURAL, roomHandler.opulence, r.RoomVisualSubtype, num3);
                        } else {
                            stampDataComplex = d2.stampData.GetStampDataComplex(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL, DungeonTileStampData.StampSpace.OBJECT_SPACE, DungeonTileStampData.StampCategory.MUNDANE, roomHandler.opulence, r.RoomVisualSubtype, num3);
                            b = IntVector2.Up;
                        }
                        IntVector2 intVector = a + IntVector2.Down + IntVector2.Left * (stampDataComplex.width + num4) + b;
                        IntVector2 intVector2 = a + IntVector2.Down + IntVector2.Right * (usedExit.referencedExit.ExitCellCount + num4) + b;
                        if (stampDataComplex is TileStampData) {
                            m_assembler.ApplyTileStamp(intVector.x, intVector.y, stampDataComplex as TileStampData, d, d2, map, -1);
                            m_assembler.ApplyTileStamp(intVector2.x, intVector2.y, stampDataComplex as TileStampData, d, d2, map, -1);
                        } else if (stampDataComplex is SpriteStampData) {
                            m_assembler.ApplySpriteStamp(intVector.x, intVector.y, stampDataComplex as SpriteStampData, d, map);
                            m_assembler.ApplySpriteStamp(intVector2.x, intVector2.y, stampDataComplex as SpriteStampData, d, map);
                        } else if (stampDataComplex is ObjectStampData) {
                            Debug.Log("object instantiate");
                            ExpandTK2DDungeonAssembler.ApplyObjectStamp(intVector.x, intVector.y, stampDataComplex as ObjectStampData, d, map, false, false);
                            ExpandTK2DDungeonAssembler.ApplyObjectStamp(intVector2.x, intVector2.y, stampDataComplex as ObjectStampData, d, map, false, false);
                        }
                        num3 -= stampDataComplex.width;
                        num4 += stampDataComplex.width;
                        if (num3 <= 0) { break; }
                    }
                }
            }
        }
        
        public void PlaceLightDecoration(Dungeon d, tk2dTileMap map) {
            for (int i = 0; i < d.data.Width; i++) {
                for (int j = 1; j < d.data.Height; j++) {
                    IntVector2 intVector = new IntVector2(i, j);
                    CellData cellData = d.data[intVector];
                    if (cellData != null) { PlaceLightDecorationForCell(d, map, cellData, intVector); }
                }
            }
        }

        protected bool IsValidPondCell(CellData cell, RoomHandler parentRoom, Dungeon d) {
            return cell != null && (parentRoom.ContainsPosition(cell.position) && cell.type == CellType.FLOOR && !cell.doesDamage && !cell.HasNonTopWallWallNeighbor() && !cell.HasPitNeighbor(d.data) && !cell.isOccupied && !cell.cellVisualData.floorTileOverridden && cell.cellVisualData.roomVisualTypeIndex == parentRoom.RoomVisualSubtype);
        }

        protected bool IsValidChannelCell(CellData cell, RoomHandler parentRoom, Dungeon d) {
            return cell != null && (parentRoom.ContainsPosition(cell.position) && cell.type == CellType.FLOOR && !cell.doesDamage && !cell.HasPitNeighbor(d.data) && !cell.isOccupied && !cell.cellVisualData.floorTileOverridden && cell.cellVisualData.roomVisualTypeIndex == parentRoom.RoomVisualSubtype);
        }

        public void DigChannels(RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map) {
            if (!d2.roomMaterialDefinitions[r.RoomVisualSubtype].supportsChannels) { return; }
            if (d2.roomMaterialDefinitions[r.RoomVisualSubtype].channelGrids.Length == 0) { return; }
            DungeonMaterial dungeonMaterial = d2.roomMaterialDefinitions[r.RoomVisualSubtype];
            TileIndexGrid tileIndexGrid = dungeonMaterial.channelGrids[UnityEngine.Random.Range(0, d2.roomMaterialDefinitions[r.RoomVisualSubtype].channelGrids.Length)];
            if (tileIndexGrid == null) { return; }
            int num = UnityEngine.Random.Range(dungeonMaterial.minChannelPools, dungeonMaterial.maxChannelPools);
            List<IntVector2> list = new List<IntVector2>();
            HashSet<IntVector2> hashSet = new HashSet<IntVector2>();
            for (int i = 0; i < num; i++) {
                HashSet<IntVector2> hashSet2 = new HashSet<IntVector2>();
                int num2 = UnityEngine.Random.Range(2, 5);
                int num3 = UnityEngine.Random.Range(2, 5);
                int num4 = UnityEngine.Random.Range(0, r.area.dimensions.x - num2);
                int num5 = UnityEngine.Random.Range(0, r.area.dimensions.y - num3);
                IntVector2 item = r.area.basePosition + new IntVector2(num4 + num2 / 2, num5 + num3 / 2);
                bool flag = false;
                if (num4 >= 0 && num5 >= 0) {
                    for (int j = num4; j < num4 + num2; j++) {
                        for (int k = num5; k < num5 + num3; k++) {
                            IntVector2 intVector = r.area.basePosition + new IntVector2(j, k);
                            CellData cell = d.data[intVector];
                            if (!IsValidPondCell(cell, r, d)) {
                                flag = true;
                                goto IL_1A3;
                            }
                            hashSet2.Add(intVector);
                        }
                    }
                }
                IL_1A3:
                if (!flag && hashSet2.Count > 5) {
                    list.Add(item);
                    foreach (IntVector2 item2 in hashSet2) { hashSet.Add(item2); }
                } else if (UnityEngine.Random.value < dungeonMaterial.channelTenacity) {
                    i--;
                }
            }
            for (int l = 0; l < list.Count; l++) {
                int num6 = UnityEngine.Random.Range(1, 4);
                for (int m = 0; m < num6; m++) {
                    HashSet<IntVector2> hashSet3 = new HashSet<IntVector2>();
                    IntVector2 intVector2 = list[l];
                    IntVector2 a = intVector2;
                    bool flag2;
                    do {
                        int num7 = UnityEngine.Random.Range(3, 8);
                        List<IntVector2> list2 = new List<IntVector2>(IntVector2.Cardinals);
                        IntVector2 intVector3 = list2[UnityEngine.Random.Range(0, 4)];
                        list2.Remove(intVector3);
                        list2.Remove(intVector3 * -1);
                        flag2 = false;
                        for (int n = 0; n < num7; n++) {
                            IntVector2 intVector4 = a + intVector3;
                            CellData cellData = d.data[intVector4];
                            if (cellData == null || cellData.type == CellType.WALL) {
                                flag2 = true;
                                break;
                            }
                            if (IsValidChannelCell(cellData, r, d) && !hashSet3.Contains(intVector4)) {
                                if (list2.Count < 3) {
                                    list2 = new List<IntVector2>(IntVector2.Cardinals);
                                    list2.Remove(intVector3);
                                    list2.Remove(intVector3 * -1);
                                }
                                a = intVector4;
                                hashSet.Add(intVector4);
                                hashSet3.Add(intVector4);
                            } else {
                                if (list2.Count <= 1) {
                                    flag2 = true;
                                    break;
                                }
                                intVector3 = list2[UnityEngine.Random.Range(0, list2.Count)];
                                list2.Remove(intVector3);
                                list2.Remove(intVector3 * -1);
                            }
                        }
                    }
                    while (!flag2);
                }
            }
            IntVector2[] cardinalsAndOrdinals = IntVector2.CardinalsAndOrdinals;
            foreach (IntVector2 intVector5 in hashSet) {
                bool[] array = new bool[8];
                int num8 = 0;
                for (int num9 = 0; num9 < array.Length; num9++) {
                    array[num9] = !hashSet.Contains(intVector5 + cardinalsAndOrdinals[num9]);
                    if (array[num9]) { num8++; }
                }
                if (num8 == 1) {
                    for (int num10 = 0; num10 < array.Length; num10 += 2) {
                        if (d.data[intVector5 + cardinalsAndOrdinals[num10]].type == CellType.WALL) {
                            array[num10] = true;
                            num8++;
                        }
                    }
                }
                int indexGivenSides = tileIndexGrid.GetIndexGivenSides(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]);
                map.SetTile(intVector5.x, intVector5.y, GlobalDungeonData.patternLayerIndex, indexGivenSides);
                d.data[intVector5].cellVisualData.floorType = CellVisualData.CellFloorType.Water;
                d.data[intVector5].cellVisualData.IsChannel = true;
            }
        }

        public void ProcessHardcodedUpholstery(RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map) {
            DungeonMaterial dungeonMaterial = d2.roomMaterialDefinitions[r.RoomVisualSubtype];
            if (dungeonMaterial.carpetGrids.Length == 0) { return; }
            HashSet<IntVector2> hashSet = new HashSet<IntVector2>();
            TileIndexGrid carpetGrid = dungeonMaterial.carpetGrids[UnityEngine.Random.Range(0, dungeonMaterial.carpetGrids.Length)];
            for (int i = 0; i < r.area.dimensions.x; i++) {
                for (int j = 0; j < r.area.dimensions.y; j++) {
                    IntVector2 intVector = r.area.basePosition + new IntVector2(i, j);
                    CellData cellData = d.data[intVector];
                    if (cellData != null) {
                        if (cellData.type == CellType.FLOOR && cellData.parentRoom == r && cellData.cellVisualData.IsPhantomCarpet && !cellData.HasPitNeighbor(d.data)) {
                            hashSet.Add(intVector);
                            BraveUtility.DrawDebugSquare(cellData.position.ToVector2(), cellData.position.ToVector2() + Vector2.one, Color.yellow, 1000f);
                        }
                    }
                }
            }
            try { ApplyCarpetedHashset(hashSet, carpetGrid, d, map); } catch (Exception) { }
        }

        // Fixed
        public void UpholsterRoom(RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map) {
            DungeonMaterial dungeonMaterial = d2.roomMaterialDefinitions[r.RoomVisualSubtype];
            if (!dungeonMaterial.supportsUpholstery) { return; }
            if (dungeonMaterial.carpetGrids.Length == 0) { return; }
            TileIndexGrid tileIndexGrid = d2.roomMaterialDefinitions[r.RoomVisualSubtype].carpetGrids[UnityEngine.Random.Range(0, d2.roomMaterialDefinitions[r.RoomVisualSubtype].carpetGrids.Length)];
            if (tileIndexGrid == null) { return; }
            HashSet<IntVector2> hashSet = new HashSet<IntVector2>();
            if (dungeonMaterial.carpetIsMainFloor) {
                for (int i = 0; i < r.area.dimensions.x; i++) {
                    for (int j = 0; j < r.area.dimensions.y; j++) {
                        IntVector2 intVector = r.area.basePosition + new IntVector2(i, j);
                        CellData cellData = d.data[intVector];
                        if (cellData != null && cellData.type == CellType.FLOOR && cellData.parentRoom == r && !cellData.doesDamage && !cellData.cellVisualData.floorTileOverridden && cellData.cellVisualData.roomVisualTypeIndex == r.RoomVisualSubtype) {
                            bool flag = cellData.HasWallNeighbor(true, false) || cellData.HasPitNeighbor(d.data);
                            bool flag2 = cellData.HasPhantomCarpetNeighbor(true);
                            if (!flag && !flag2) { hashSet.Add(intVector); }
                        }
                    }
                }
                hashSet = Carpetron.PostprocessFullRoom(hashSet);
            } else {
                bool flag3 = true;
                List<Tuple<IntVector2, IntVector2>> list = new List<Tuple<IntVector2, IntVector2>>();
                Tuple<IntVector2, IntVector2> tuple = null;
                int num = 1;
                if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON) { num = 2; }
                while (flag3) {
                    Tuple<IntVector2, IntVector2> tuple2 = Carpetron.MaxSubmatrix(d.data.cellData, r.area.basePosition, r.area.dimensions, false, false, false, r.RoomVisualSubtype);
                    IntVector2 b = tuple2.Second + IntVector2.One - tuple2.First;
                    int num2 = b.x * b.y;
                    if (num2 < 15 || b.x < 3 || b.y < 3) { break; }
                    if (tuple != null) {
                        IntVector2 a = tuple.Second + IntVector2.One - tuple.First;
                        if (a != b) {
                            num--;
                            if (num <= 0) { break; }
                        }
                    }
                    for (int k = tuple2.First.x; k < tuple2.Second.x + 1; k++) {
                        for (int l = tuple2.First.y; l < tuple2.Second.y + 1; l++) {
                            IntVector2 key = r.area.basePosition + new IntVector2(k, l);
                            d.data[key].cellVisualData.floorTileOverridden = true;
                        }
                    }
                    list.Add(tuple2);
                    tuple = tuple2;
                }
                if (list.Count == 1) {
                    Tuple<IntVector2, IntVector2> tuple3 = null;
                    list[0] = Carpetron.PostprocessSubmatrix(list[0], out tuple3);
                    if (tuple3 != null) { list.Add(tuple3); }
                }
                for (int m = 0; m < list.Count; m++) {
                    Tuple<IntVector2, IntVector2> tuple4 = list[m];
                    for (int n = tuple4.First.x; n < tuple4.Second.x + 1; n++) {
                        for (int num3 = tuple4.First.y; num3 < tuple4.Second.y + 1; num3++) {
                            IntVector2 item = r.area.basePosition + new IntVector2(n, num3);
                            hashSet.Add(item);
                        }
                    }
                }
            }
            try { ApplyCarpetedHashset(hashSet, tileIndexGrid, d, map); } catch (Exception) { return; }
        }

        private void ApplyCarpetedHashset(HashSet<IntVector2> cellsToEncarpet, TileIndexGrid carpetGrid, Dungeon d, tk2dTileMap map) {
            IntVector2[] cardinalsAndOrdinals = IntVector2.CardinalsAndOrdinals;
            Dictionary<IntVector2, int> dictionary = new Dictionary<IntVector2, int>(new IntVector2EqualityComparer());
            if (carpetGrid.CenterIndicesAreStrata) {
                HashSet<IntVector2> hashSet = new HashSet<IntVector2>();
                HashSet<IntVector2> hashSet2 = new HashSet<IntVector2>();
                HashSet<IntVector2> hashSet3 = new HashSet<IntVector2>();
                foreach (IntVector2 intVector in cellsToEncarpet) {
                    bool[] array = new bool[8];
                    for (int i = 0; i < array.Length; i++) { array[i] = !cellsToEncarpet.Contains(intVector + cardinalsAndOrdinals[i]); }
                    if (array[0] || array[1] || array[2] || array[3] || array[4] || array[5] || array[6] || array[7]) { hashSet2.Add(intVector); }
                }
                int num = 0;
                while (hashSet2.Count > 0) {
                    foreach (IntVector2 intVector2 in hashSet2) {
                        hashSet.Add(intVector2);
                        for (int j = 0; j < 8; j++) {
                            IntVector2 intVector3 = intVector2 + cardinalsAndOrdinals[j];
                            if (!hashSet.Contains(intVector3) && !hashSet2.Contains(intVector3) && !hashSet3.Contains(intVector3) && cellsToEncarpet.Contains(intVector3)) {
                                hashSet3.Add(intVector3);
                                dictionary.Add(intVector3, carpetGrid.centerIndices.indices[Mathf.Clamp(num, 0, carpetGrid.centerIndices.indices.Count - 1)]);
                            }
                        }
                    }
                    hashSet2 = hashSet3;
                    hashSet3 = new HashSet<IntVector2>();
                    num++;
                }
                if (num < 3) { dictionary.Clear(); }
            }
            foreach (IntVector2 intVector4 in cellsToEncarpet) {
                bool[] array2 = new bool[8];
                for (int k = 0; k < array2.Length; k++) {
                    array2[k] = !cellsToEncarpet.Contains(intVector4 + cardinalsAndOrdinals[k]);
                }
                bool flag = !array2[0] && !array2[1] && !array2[2] && !array2[3] && !array2[4] && !array2[5] && !array2[6] && !array2[7];
                int tile;
                if (dictionary.ContainsKey(intVector4)) {
                    tile = dictionary[intVector4];
                } else if (flag && carpetGrid.CenterIndicesAreStrata) {
                    tile = carpetGrid.centerIndices.indices[0];
                } else {
                    tile = carpetGrid.GetIndexGivenSides(array2[0], array2[1], array2[2], array2[3], array2[4], array2[5], array2[6], array2[7]);
                }
                map.SetTile(intVector4.x, intVector4.y, GlobalDungeonData.patternLayerIndex, tile);
                d.data[intVector4].cellVisualData.floorType = CellVisualData.CellFloorType.Carpet;
                d.data[intVector4].cellVisualData.floorTileOverridden = true;
            }
        }

        public void HandleRoomDecorationMinimal(RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map) {
            roomUsedStamps.Clear();
            if (r.area.prototypeRoom == null) { return; }
            if (viableCategorySets == null) {
                BuildStampLookupTable(d2);
                BuildValidPlacementLists();
            }
            try { ProcessHardcodedUpholstery(r, d, d2, map); } catch (Exception) { }
            roomUsedStamps.Clear();
        }

        public void HandleRoomDecoration(RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map) {
            roomUsedStamps.Clear();
            try { ProcessHardcodedUpholstery(r, d, d2, map); } catch (Exception) { }
            if (r.area.prototypeRoom == null || !r.area.prototypeRoom.preventAddedDecoLayering) {
                try { UpholsterRoom(r, d, d2, map); } catch (Exception) { }
                if (!r.ForcePreventChannels) { try { DigChannels(r, d, d2, map); } catch (Exception) { } }
            }
            if (viableCategorySets == null) {
                BuildStampLookupTable(d2);
                BuildValidPlacementLists();
            }
            for (int i = 0; i < r.area.instanceUsedExits.Count; i++) {
                PrototypeRoomExit key = r.area.instanceUsedExits[i];
                RoomHandler roomHandler = r.connectedRoomsByExit[key];
                if (roomHandler != null && (!(roomHandler.area.prototypeRoom != null) || roomHandler.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.SECRET)) {
                    DecorateRoomExit(r, r.area.exitToLocalDataMap[key], d, d2, map);
                }
            }
            List<TK2DInteriorDecorator.WallExpanse> list = r.GatherExpanses(DungeonData.Direction.NORTH, false, false, false);
            for (int j = 0; j < list.Count; j++) {
                TK2DInteriorDecorator.WallExpanse value = list[j];
                TK2DInteriorDecorator.WallExpanse? wallExpanse = null;
                int index = -1;
                for (int k = j + 1; k < list.Count; k++) {
                    TK2DInteriorDecorator.WallExpanse value2 = list[k];
                    if (value.basePosition.y == value2.basePosition.y && value.width == value2.width) {
                        bool flag = true;
                        for (int l = 0; l < value2.width; l++) {
                            if (d.data[r.area.basePosition + value.basePosition + IntVector2.Up + IntVector2.Right * l].cellVisualData.forcedMatchingStyle != d.data[r.area.basePosition + value2.basePosition + IntVector2.Up + IntVector2.Right * l].cellVisualData.forcedMatchingStyle) {
                                flag = false;
                                break;
                            }
                        }
                        if (flag) {
                            wallExpanse = new TK2DInteriorDecorator.WallExpanse?(value2);
                            index = k;
                        }
                    }
                }
                if (wallExpanse != null) {
                    value.hasMirror = true;
                    value.mirroredExpanseBasePosition = wallExpanse.Value.basePosition;
                    value.mirroredExpanseWidth = wallExpanse.Value.width;
                    list.RemoveAt(index);
                    list[j] = value;
                }
            }
            wallPlacementOffsets = new Dictionary<DungeonTileStampData.StampPlacementRule, IntVector2>();
            wallPlacementOffsets.Add(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL, IntVector2.Zero);
            wallPlacementOffsets.Add(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL_LEFT_CORNER, IntVector2.Zero);
            wallPlacementOffsets.Add(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL_RIGHT_CORNER, IntVector2.Zero);
            wallPlacementOffsets.Add(DungeonTileStampData.StampPlacementRule.ON_LOWER_FACEWALL, IntVector2.Up);
            wallPlacementOffsets.Add(DungeonTileStampData.StampPlacementRule.ON_UPPER_FACEWALL, IntVector2.Up * 2);
            wallPlacementOffsets.Add(DungeonTileStampData.StampPlacementRule.ON_ANY_FLOOR, IntVector2.Zero);
            wallPlacementOffsets.Add(DungeonTileStampData.StampPlacementRule.ON_ANY_CEILING, IntVector2.Zero);
            wallPlacementOffsets.Add(DungeonTileStampData.StampPlacementRule.ALONG_LEFT_WALLS, IntVector2.Left);
            wallPlacementOffsets.Add(DungeonTileStampData.StampPlacementRule.ALONG_RIGHT_WALLS, IntVector2.Zero);
            wallPlacementOffsets.Add(DungeonTileStampData.StampPlacementRule.ON_TOPWALL, IntVector2.Zero);
            for (int m = 0; m < list.Count; m++) {
                expanseUsedStamps.Clear();
                TK2DInteriorDecorator.WallExpanse expanse = list[m];
                if (expanse.hasMirror) {
                    DecorateExpanseRandom(expanse, r, d, d2, map);
                } else if (expanse.width > 2) {
                    float num = UnityEngine.Random.value;
                    for (int n = 0; n < expanse.width; n++) {
                        if (d.data[r.area.basePosition + expanse.basePosition + IntVector2.Up + IntVector2.Right * n].cellVisualData.forcedMatchingStyle != DungeonTileStampData.IntermediaryMatchingStyle.ANY) {
                            num = 1000f;
                        }
                    }
                    if (num < d2.stampData.SymmetricFrameChance) {
                        DecorateExpanseSymmetricFrame(1, expanse, r, d, d2, map);
                    } else if (num >= d2.stampData.SymmetricFrameChance && num < d2.stampData.SymmetricFrameChance + d2.stampData.SymmetricCompleteChance) {
                        DecorateExpanseSymmetric(expanse, r, d, d2, map);
                    } else {
                        DecorateExpanseRandom(expanse, r, d, d2, map);
                    }
                } else {
                    DecorateExpanseRandom(expanse, r, d, d2, map);
                }
            }
            DecorateCeilingCorners(r, d, d2, map);
            List<TK2DInteriorDecorator.WallExpanse> list2 = r.GatherExpanses(DungeonData.Direction.EAST, false, false, false);
            List<TK2DInteriorDecorator.WallExpanse> list3 = r.GatherExpanses(DungeonData.Direction.WEST, false, false, false);
            for (int num2 = 0; num2 < list2.Count; num2++) {
                TK2DInteriorDecorator.WallExpanse value3 = list2[num2];
                if (value3.width > 1) {
                    value3.width--;
                    list2[num2] = value3;
                } else {
                    list2.RemoveAt(num2);
                    num2--;
                }
            }
            for (int num3 = 0; num3 < list3.Count; num3++) {
                TK2DInteriorDecorator.WallExpanse value4 = list3[num3];
                if (value4.width > 1) {
                    value4.width--;
                    list3[num3] = value4;
                } else {
                    list3.RemoveAt(num3);
                    num3--;
                }
            }
            int num4 = 0;
            while (num4 < list2.Count) {
                expanseUsedStamps.Clear();
                TK2DInteriorDecorator.WallExpanse expanse2 = list2[num4];
                TK2DInteriorDecorator.WallExpanse? wallExpanse2 = null;
                for (int num5 = 0; num5 < list3.Count; num5++) {
                    TK2DInteriorDecorator.WallExpanse value5 = list3[num5];
                    if (value5.basePosition.y == expanse2.basePosition.y && value5.width == expanse2.width) {
                        wallExpanse2 = new TK2DInteriorDecorator.WallExpanse?(value5);
                        list3.RemoveAt(num5);
                        break;
                    }
                }
                int num6 = 1;
                for (;;) {
                    int num7 = expanse2.width - num6;
                    if (num7 == 0) { break; }
                    IntVector2 basePosition = r.area.basePosition + expanse2.basePosition + num6 * IntVector2.Up;
                    StampDataBase stampDataBase = null;
                    DecorateErrorCode decorateErrorCode = DecorateWallSection(basePosition, num7, r, d, d2, map, validEasternPlacements, expanse2, out stampDataBase, Mathf.Max(0.55f, r.RoomMaterial.stampFailChance), true);
                    if (decorateErrorCode == DecorateErrorCode.FAILED_SPACE) { break; }
                    if (stampDataBase == null || decorateErrorCode == DecorateErrorCode.FAILED_CHANCE) {
                        num6++;
                    } else {
                        if (wallExpanse2 != null) {
                            IntVector2 basePosition2 = r.area.basePosition + wallExpanse2.Value.basePosition + (expanse2.width - num7) * IntVector2.Up;
                            StampDataBase stampDataBase2 = null;
                            DecorateWallSection(basePosition2, num7, r, d, d2, map, validWesternPlacements, wallExpanse2.Value, out stampDataBase2, 0f, true);
                        }
                        num6 += stampDataBase.height;
                    }
                }
                // IL_75E:
                num4++;
                continue;
                // goto IL_75E;
            }
            int num8 = 0;
            while (num8 < list3.Count) {
                expanseUsedStamps.Clear();
                TK2DInteriorDecorator.WallExpanse expanse3 = list3[num8];
                int num9 = 1;
                for (;;) {
                    int num10 = expanse3.width - num9;
                    if (num10 == 0) { break; }
                    IntVector2 basePosition3 = r.area.basePosition + expanse3.basePosition + num9 * IntVector2.Up;
                    StampDataBase stampDataBase3 = null;
                    DecorateErrorCode decorateErrorCode2 = DecorateWallSection(basePosition3, num10, r, d, d2, map, validWesternPlacements, expanse3, out stampDataBase3, Mathf.Max(0.55f, r.RoomMaterial.stampFailChance), true);
                    if (decorateErrorCode2 == DecorateErrorCode.FAILED_SPACE) { break; }
                    if (stampDataBase3 == null || decorateErrorCode2 == DecorateErrorCode.FAILED_CHANCE) {
                        num9++;
                    } else {
                        num9 += stampDataBase3.height;
                    }
                }
                // IL_83F:
                num8++;
                continue;
                // goto IL_83F;
            }
            List<TK2DInteriorDecorator.WallExpanse> list4 = r.GatherExpanses(DungeonData.Direction.SOUTH, true, false, false);
            int num11 = 0;
            while (num11 < list4.Count) {
                expanseUsedStamps.Clear();
                TK2DInteriorDecorator.WallExpanse expanse4 = list4[num11];
                int num12 = 1;
                for (;;) {
                    int num13 = Mathf.FloorToInt((expanse4.width - 2 * num12) / 2f);
                    if (num13 == 0) { break; }
                    IntVector2 basePosition4 = r.area.basePosition + expanse4.basePosition + num12 * IntVector2.Right;
                    StampDataBase stampDataBase4 = null;
                    DecorateErrorCode decorateErrorCode3 = DecorateWallSection(basePosition4, num13, r, d, d2, map, validSouthernPlacements, expanse4, out stampDataBase4, 0.5f, false);
                    if (decorateErrorCode3 == DecorateErrorCode.FAILED_SPACE) { break; }
                    if (stampDataBase4 == null || decorateErrorCode3 == DecorateErrorCode.FAILED_CHANCE) {
                        num12++;
                    } else {
                        IntVector2 intVector = r.area.basePosition + expanse4.basePosition + (expanse4.width - num12 - stampDataBase4.width) * IntVector2.Right + wallPlacementOffsets[stampDataBase4.placementRule];
                        m_assembler.ApplyStampGeneric(intVector.x, intVector.y, stampDataBase4, d, d2, map, false, GlobalDungeonData.aboveBorderLayerIndex);
                        num12 += stampDataBase4.width;
                        if (stampDataBase4.width == 1) { num12 += 2; }
                    }
                }
                // IL_9B1:
                num11++;
                continue;
                // goto IL_9B1;
            }
            for (int num14 = 2; num14 < r.area.dimensions.x - 2; num14++) {
                for (int num15 = 2; num15 < r.area.dimensions.y - 2; num15++) {
                    IntVector2 basePosition5 = r.area.basePosition + new IntVector2(num14, num15);
                    CellData cellData = d.data.cellData[basePosition5.x][basePosition5.y];
                    if (cellData != null) {
                        if (cellData.type == CellType.FLOOR) {
                            if (!cellData.cellVisualData.floorTileOverridden) {
                                if (!cellData.cellVisualData.preventFloorStamping) {
                                    StampDataBase stampDataBase5 = null;
                                    float failChance = 0.8f;
                                    if (d2.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON) { failChance = 0.99f; }
                                    DecorateFloorSquare(basePosition5, r, d, d2, map, out stampDataBase5, failChance);
                                }
                            }
                        }
                    }
                }
            }
            roomUsedStamps.Clear();
        }

        private void DecorateCeilingCorners(RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map) {
            if (d2.tileIndices.edgeDecorationTiles == null) { return; }
            if (r == d.data.Entrance) { return; }
            if (r == d.data.Exit) { return; }
            CellArea area = r.area;
            for (int i = 0; i < area.dimensions.x; i++) {
                for (int j = 0; j < area.dimensions.y; j++) {
                    IntVector2 intVector = area.basePosition + new IntVector2(i, j);
                    CellData cellData = d.data.cellData[intVector.x][intVector.y];
                    if (cellData != null && cellData.type != CellType.WALL) {
                        if (cellData.diagonalWallType == DiagonalWallType.NONE) {
                            List<CellData> cellNeighbors = d.data.GetCellNeighbors(cellData, false);
                            bool flag = cellNeighbors[0] != null && cellNeighbors[0].type == CellType.WALL && cellNeighbors[0].diagonalWallType == DiagonalWallType.NONE;
                            bool isEastBorder = cellNeighbors[1] != null && cellNeighbors[1].type == CellType.WALL && cellNeighbors[1].diagonalWallType == DiagonalWallType.NONE;
                            bool isSouthBorder = cellNeighbors[2] != null && cellNeighbors[2].type == CellType.WALL && cellNeighbors[2].diagonalWallType == DiagonalWallType.NONE;
                            bool isWestBorder = cellNeighbors[3] != null && cellNeighbors[3].type == CellType.WALL && cellNeighbors[3].diagonalWallType == DiagonalWallType.NONE;
                            int indexGivenSides = d2.tileIndices.edgeDecorationTiles.GetIndexGivenSides(flag, isEastBorder, isSouthBorder, isWestBorder);
                            bool flag2 = UnityEngine.Random.value < 0.25f;
                            if (indexGivenSides != -1 && flag2) {
                                int num = (!flag) ? 1 : 2;
                                map.SetTile(intVector.x, intVector.y + num, GlobalDungeonData.aboveBorderLayerIndex, indexGivenSides);
                            }
                        }
                    }
                }
            }
        }

        private void DecorateExpanseSymmetricFrame(int frameIterations, TK2DInteriorDecorator.WallExpanse expanse, RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map) {
            int num = 0;
            for (int i = 0; i < frameIterations; i++) {
                int num2 = Mathf.FloorToInt((expanse.width - 2 * num) / 2f);
                if (num2 == 0) { break; }
                IntVector2 intVector = r.area.basePosition + expanse.basePosition + num * IntVector2.Right;
                StampDataBase stampDataBase = null;
                DecorateErrorCode decorateErrorCode = DecorateWallSection(intVector, num2, r, d, d2, map, validNorthernPlacements, expanse, out stampDataBase, r.RoomMaterial.stampFailChance, false);
                if (decorateErrorCode == DecorateErrorCode.FAILED_SPACE) { break; }
                if (stampDataBase == null || decorateErrorCode == DecorateErrorCode.FAILED_CHANCE) {
                    num++;
                } else {
                    if (stampDataBase.indexOfSymmetricPartner != -1) {
                        stampDataBase = d2.stampData.objectStamps[stampDataBase.indexOfSymmetricPartner];
                    }
                    IntVector2 a = r.area.basePosition + expanse.basePosition + (expanse.width - num - stampDataBase.width) * IntVector2.Right + wallPlacementOffsets[stampDataBase.placementRule];
                    if (!stampDataBase.preventRoomRepeats) {
                        m_assembler.ApplyStampGeneric(a.x, a.y, stampDataBase, d, d2, map, false, -1);
                    } else {
                        StampDataBase stampDataBase2 = d2.stampData.AttemptGetSimilarStampForRoomDuplication(stampDataBase, roomUsedStamps, r.RoomVisualSubtype);
                        if (stampDataBase2 != null) {
                            m_assembler.ApplyStampGeneric(a.x, a.y, stampDataBase2, d, d2, map, false, -1);
                            roomUsedStamps.Add(stampDataBase2);
                        }
                    }
                    if (DEBUG_DRAW) {
                        BraveUtility.DrawDebugSquare(intVector.ToVector2(), (intVector + IntVector2.Up + stampDataBase.width * IntVector2.Right).ToVector2(), Color.red, 1000f);
                        BraveUtility.DrawDebugSquare(a.ToVector2(), (a + IntVector2.Up + stampDataBase.width * IntVector2.Right).ToVector2(), Color.red, 1000f);
                    }
                    num += stampDataBase.width;
                }
            }
            int num3 = expanse.width - 2 * num;
            if (num3 > 0) {
                TK2DInteriorDecorator.WallExpanse expanse2 = new TK2DInteriorDecorator.WallExpanse(expanse.basePosition + num * IntVector2.Right, num3);
                DecorateExpanseRandom(expanse2, r, d, d2, map);
            }
        }

        private void DecorateExpanseSymmetric(TK2DInteriorDecorator.WallExpanse expanse, RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map) {
            int num = 0;
            for (;;) {
                int num2 = Mathf.FloorToInt((expanse.width - 2 * num) / 2f);
                if (num2 == 0) { break; }
                IntVector2 intVector = r.area.basePosition + expanse.basePosition + num * IntVector2.Right;
                StampDataBase stampDataBase = null;
                DecorateErrorCode decorateErrorCode = DecorateWallSection(intVector, num2, r, d, d2, map, validNorthernPlacements, expanse, out stampDataBase, r.RoomMaterial.stampFailChance, false);
                if (decorateErrorCode == DecorateErrorCode.FAILED_SPACE) { break; }
                if (stampDataBase == null || decorateErrorCode == DecorateErrorCode.FAILED_CHANCE) {
                    num++;
                } else {
                    if (stampDataBase.indexOfSymmetricPartner != -1) {
                        stampDataBase = d2.stampData.objectStamps[stampDataBase.indexOfSymmetricPartner];
                    }
                    IntVector2 a = r.area.basePosition + expanse.basePosition + (expanse.width - num - stampDataBase.width) * IntVector2.Right + wallPlacementOffsets[stampDataBase.placementRule];
                    if (!stampDataBase.preventRoomRepeats) {
                        m_assembler.ApplyStampGeneric(a.x, a.y, stampDataBase, d, d2, map, false, -1);
                    } else {
                        StampDataBase stampDataBase2 = d2.stampData.AttemptGetSimilarStampForRoomDuplication(stampDataBase, roomUsedStamps, r.RoomVisualSubtype);
                        if (stampDataBase2 != null) {
                            m_assembler.ApplyStampGeneric(a.x, a.y, stampDataBase2, d, d2, map, false, -1);
                            roomUsedStamps.Add(stampDataBase2);
                        }
                    }
                    if (DEBUG_DRAW) {
                        BraveUtility.DrawDebugSquare(intVector.ToVector2(), (intVector + IntVector2.Up + stampDataBase.width * IntVector2.Right).ToVector2(), Color.yellow, 1000f);
                        BraveUtility.DrawDebugSquare(a.ToVector2(), (a + IntVector2.Up + stampDataBase.width * IntVector2.Right).ToVector2(), Color.yellow, 1000f);
                    }
                    num += stampDataBase.width;
                }
            }
        }

        private void DecorateExpanseRandom(TK2DInteriorDecorator.WallExpanse expanse, RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map) {
            int num = 0;
            for (;;) {
                int num2 = expanse.width - num;
                if (num2 == 0) { break; }
                IntVector2 intVector = r.area.basePosition + expanse.basePosition + num * IntVector2.Right;
                StampDataBase stampDataBase = null;
                DecorateErrorCode decorateErrorCode = DecorateWallSection(intVector, num2, r, d, d2, map, validNorthernPlacements, expanse, out stampDataBase, r.RoomMaterial.stampFailChance, false);
                if (decorateErrorCode == DecorateErrorCode.FAILED_SPACE) { break; }
                if (stampDataBase == null || decorateErrorCode == DecorateErrorCode.FAILED_CHANCE) {
                    num++;
                } else {
                    if (expanse.hasMirror) {
                        IntVector2 a = r.area.basePosition + expanse.GetPositionInMirroredExpanse(num, stampDataBase.width);
                        Debug.DrawLine(a.ToVector3(), a.ToVector3() + new Vector3(1f, 1f, 0f), Color.cyan, 1000f);
                        if (stampDataBase.indexOfSymmetricPartner != -1) {
                            stampDataBase = d2.stampData.objectStamps[stampDataBase.indexOfSymmetricPartner];
                        }
                        IntVector2 intVector2 = a + wallPlacementOffsets[stampDataBase.placementRule];
                        if (!stampDataBase.preventRoomRepeats) {
                            m_assembler.ApplyStampGeneric(intVector2.x, intVector2.y, stampDataBase, d, d2, map, false, -1);
                        } else {
                            StampDataBase stampDataBase2 = d2.stampData.AttemptGetSimilarStampForRoomDuplication(stampDataBase, roomUsedStamps, r.RoomVisualSubtype);
                            if (stampDataBase2 != null) {
                                m_assembler.ApplyStampGeneric(intVector2.x, intVector2.y, stampDataBase2, d, d2, map, false, -1);
                                roomUsedStamps.Add(stampDataBase2);
                            }
                        }
                    }
                    if (DEBUG_DRAW) {
                        BraveUtility.DrawDebugSquare(intVector.ToVector2(), (intVector + IntVector2.Up + stampDataBase.width * IntVector2.Right).ToVector2(), Color.magenta, 1000f);
                    }
                    num += stampDataBase.width;
                }
            }
        }

        private DungeonTileStampData.StampSpace GetValidSpaceForSection(IntVector2 basePosition, int viableWidth, Dungeon d)
        {
            List<DungeonTileStampData.StampSpace> list = new List<DungeonTileStampData.StampSpace>();
            list.Add(DungeonTileStampData.StampSpace.OBJECT_SPACE);
            bool flag = true;
            for (int i = 0; i < viableWidth; i++)
            {
                IntVector2 a = basePosition + IntVector2.Up + IntVector2.Right * i;
                CellVisualData cellVisualData = d.data.cellData[a.x][a.y].cellVisualData;
                if (cellVisualData.containsWallSpaceStamp)
                {
                    flag = false;
                    break;
                }
                a += IntVector2.Up;
                cellVisualData = d.data.cellData[a.x][a.y].cellVisualData;
                if (cellVisualData.containsWallSpaceStamp)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                list.Add(DungeonTileStampData.StampSpace.WALL_SPACE);
                list.Add(DungeonTileStampData.StampSpace.BOTH_SPACES);
            }
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        private void BuildValidPlacementLists()
        {
            validNorthernPlacements = new List<DungeonTileStampData.StampPlacementRule>();
            validNorthernPlacements.Add(DungeonTileStampData.StampPlacementRule.ABOVE_UPPER_FACEWALL);
            validNorthernPlacements.Add(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL);
            validNorthernPlacements.Add(DungeonTileStampData.StampPlacementRule.ON_LOWER_FACEWALL);
            validNorthernPlacements.Add(DungeonTileStampData.StampPlacementRule.ON_UPPER_FACEWALL);
            validEasternPlacements = new List<DungeonTileStampData.StampPlacementRule>();
            validEasternPlacements.Add(DungeonTileStampData.StampPlacementRule.ALONG_RIGHT_WALLS);
            validEasternPlacements.Add(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL);
            validWesternPlacements = new List<DungeonTileStampData.StampPlacementRule>();
            validWesternPlacements.Add(DungeonTileStampData.StampPlacementRule.ALONG_LEFT_WALLS);
            validWesternPlacements.Add(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL);
            validSouthernPlacements = new List<DungeonTileStampData.StampPlacementRule>();
            validSouthernPlacements.Add(DungeonTileStampData.StampPlacementRule.ON_TOPWALL);
        }

        private void BuildStampLookupTable(Dungeon d) {
            viableCategorySets = new List<ViableStampCategorySet>();
            for (int i = 0; i < d.stampData.stamps.Length; i++) {
                StampDataBase stampDataBase = d.stampData.stamps[i];
                ViableStampCategorySet item = new ViableStampCategorySet(stampDataBase.stampCategory, stampDataBase.placementRule, stampDataBase.occupySpace);
                if (!viableCategorySets.Contains(item)) { viableCategorySets.Add(item); }
            }
            for (int j = 0; j < d.stampData.spriteStamps.Length; j++) {
                StampDataBase stampDataBase2 = d.stampData.spriteStamps[j];
                ViableStampCategorySet item2 = new ViableStampCategorySet(stampDataBase2.stampCategory, stampDataBase2.placementRule, stampDataBase2.occupySpace);
                if (!viableCategorySets.Contains(item2)) { viableCategorySets.Add(item2); }
            }
            for (int k = 0; k < d.stampData.objectStamps.Length; k++) {
                StampDataBase stampDataBase3 = d.stampData.objectStamps[k];
                ViableStampCategorySet item3 = new ViableStampCategorySet(stampDataBase3.stampCategory, stampDataBase3.placementRule, stampDataBase3.occupySpace);
                if (!viableCategorySets.Contains(item3)) { viableCategorySets.Add(item3); }
            }
        }

        private ViableStampCategorySet GetCategorySet(List<DungeonTileStampData.StampPlacementRule> validRules) {
            List<ViableStampCategorySet> list = new List<ViableStampCategorySet>();
            for (int i = 0; i < viableCategorySets.Count; i++) {
                if (validRules.Contains(viableCategorySets[i].placement)) { list.Add(viableCategorySets[i]); }
            }
            if (list.Count == 0) { return null; }
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        private bool CheckExpanseStampValidity(TK2DInteriorDecorator.WallExpanse expanse, StampDataBase stamp) {
            if (stamp.preventRoomRepeats && roomUsedStamps.Contains(stamp)) { return false; }
            int preferredIntermediaryStamps = stamp.preferredIntermediaryStamps;
            for (int i = 0; i < preferredIntermediaryStamps; i++) {
                int num = expanseUsedStamps.Count - (1 + i);
                if (num < 0) { break; }
                if (stamp.intermediaryMatchingStyle == DungeonTileStampData.IntermediaryMatchingStyle.ANY) {
                    if (expanseUsedStamps[num] == stamp) { return false; }
                } else if (expanseUsedStamps[num].intermediaryMatchingStyle == stamp.intermediaryMatchingStyle) {
                    return false;
                }
            }
            return true;
        }

        private bool DecorateFloorSquare(IntVector2 basePosition, RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map, out StampDataBase placedStamp, float failChance = 0.2f) {
            if (UnityEngine.Random.value < failChance) {
                placedStamp = null;
                return true;
            }
            placedStamp = null;
            List<DungeonTileStampData.StampPlacementRule> list = new List<DungeonTileStampData.StampPlacementRule>();
            list.Add(DungeonTileStampData.StampPlacementRule.ON_ANY_FLOOR);
            ViableStampCategorySet categorySet = GetCategorySet(list);
            if (categorySet == null) { return false; }
            StampDataBase stampDataComplex = d2.stampData.GetStampDataComplex(list, categorySet.space, categorySet.category, r.opulence, r.RoomVisualSubtype, 1);
            if (stampDataComplex == null) { return false; }
            IntVector2 intVector = basePosition + wallPlacementOffsets[stampDataComplex.placementRule];
            m_assembler.ApplyStampGeneric(intVector.x, intVector.y, stampDataComplex, d, d2, map, false, -1);
            placedStamp = stampDataComplex;
            return true;
        }

        private DecorateErrorCode DecorateWallSection(IntVector2 basePosition, int viableWidth, RoomHandler r, Dungeon d, Dungeon d2, tk2dTileMap map, List<DungeonTileStampData.StampPlacementRule> validRules, TK2DInteriorDecorator.WallExpanse expanse, out StampDataBase placedStamp, float failChance = 0.2f, bool excludeWallSpace = false) {
            if (GameManager.Options.DebrisQuantity == GameOptions.GenericHighMedLowOption.VERY_LOW) { failChance = Mathf.Min(failChance * 2f, 0.75f); }
            if (UnityEngine.Random.value < failChance) {
                placedStamp = null;
                return DecorateErrorCode.FAILED_CHANCE;
            }
            StampDataBase stampDataBase = null;
            if (validRules.Contains(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL)) {
                if (d.data.GetCellTypeSafe(basePosition + IntVector2.Left) == CellType.WALL) {
                    validRules.Add(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL_LEFT_CORNER);
                }
                if (d.data.GetCellTypeSafe(basePosition + IntVector2.Right) == CellType.WALL) {
                    validRules.Add(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL_RIGHT_CORNER);
                }
            }
            int i = 0;
            while (i < 10) {
                if (!d.data.CheckInBoundsAndValid(basePosition) || !d.data.CheckInBoundsAndValid(basePosition + IntVector2.Up)) {
                    stampDataBase = null;
                    break;
                }
                if (d.data[basePosition + IntVector2.Up].cellVisualData.forcedMatchingStyle == DungeonTileStampData.IntermediaryMatchingStyle.ANY) {
                    stampDataBase = d2.stampData.GetStampDataSimple(validRules, r.opulence, r.RoomVisualSubtype, viableWidth, excludeWallSpace, roomUsedStamps);
                    if (stampDataBase == null || !stampDataBase.requiresForcedMatchingStyle) { goto IL_20F; }
                } else {
                    BraveUtility.DrawDebugSquare((basePosition + IntVector2.Up).ToVector2() + new Vector2(0.2f, 0.2f), (basePosition + IntVector2.Up + IntVector2.One).ToVector2() + new Vector2(-0.2f, -0.2f), Color.red, 1000f);
                    stampDataBase = d2.stampData.GetStampDataSimpleWithForcedRule(validRules, d.data[basePosition + IntVector2.Up].cellVisualData.forcedMatchingStyle, r.opulence, r.RoomVisualSubtype, viableWidth, excludeWallSpace);
                    if (stampDataBase != null && stampDataBase.intermediaryMatchingStyle != d.data[basePosition + IntVector2.Up].cellVisualData.forcedMatchingStyle) {
                        break;
                    }
                    goto IL_20F;
                }
                IL_247:
                i++;
                continue;
                IL_20F:
                if (stampDataBase == null) { break; }
                if (excludeWallSpace && stampDataBase.width > 1) { goto IL_247; }
                if (CheckExpanseStampValidity(expanse, stampDataBase)) { break; }
                stampDataBase = null;
                goto IL_247;
            }
            validRules.Remove(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL_LEFT_CORNER);
            validRules.Remove(DungeonTileStampData.StampPlacementRule.BELOW_LOWER_FACEWALL_RIGHT_CORNER);
            if (stampDataBase == null) {
                placedStamp = null;
                return DecorateErrorCode.FAILED_SPACE;
            }
            expanseUsedStamps.Add(stampDataBase);
            roomUsedStamps.Add(stampDataBase);
            IntVector2 intVector = basePosition + wallPlacementOffsets[stampDataBase.placementRule];
            bool flag = stampDataBase.placementRule == DungeonTileStampData.StampPlacementRule.ALONG_LEFT_WALLS || stampDataBase.placementRule == DungeonTileStampData.StampPlacementRule.ALONG_RIGHT_WALLS || stampDataBase.placementRule == DungeonTileStampData.StampPlacementRule.ON_TOPWALL;
            int overrideTileLayerIndex = (!flag) ? -1 : GlobalDungeonData.aboveBorderLayerIndex;
            m_assembler.ApplyStampGeneric(intVector.x, intVector.y, stampDataBase, d, d2, map, false, overrideTileLayerIndex);
            placedStamp = stampDataBase;
            return DecorateErrorCode.ALL_OK;
        }
    }
}

