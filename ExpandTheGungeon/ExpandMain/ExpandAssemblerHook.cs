using ExpandTheGungeon.ExpandUtilities;
using Dungeonator;
using System;
using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ExpandMain {

    class ExpandAssemblerHook : MonoBehaviour {

        public IEnumerator ConstructTK2DDungeonHook(Action<TK2DDungeonAssembler, Dungeon, tk2dTileMap>orig, TK2DDungeonAssembler self, Dungeon d, tk2dTileMap map) {
            for (int j = 0; j < d.data.Width; j++) {
                for (int k = 0; k < d.data.Height; k++) {
                    try {
                        self.BuildTileIndicesForCell(d, map, j, k);
                    } catch (Exception ex) {
                        if (ExpandStats.debugMode) {
                            ETGModConsole.Log("[DEBUG] Exception caught in TK2DDungeonAssembler.ConstructTK2DDungeonHook at TK2DDungeonAssembler.BuildTileIndicesForCell!");
                        }
                        Debug.Log("Exception caught in TK2DDungeonAssembler.ConstructTK2DDungeonHook at TK2DDungeonAssembler.BuildTileIndicesForCell!");
                        Debug.LogException(ex);
                    }
                }
            }

            yield return null;

            TileIndices tileIndices = ReflectionHelpers.ReflectGetField<TileIndices>(typeof(TK2DDungeonAssembler), "t", self);

            if (d.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON || d.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FINALGEON) {
                for (int l = 0; l < d.data.Width; l++) {
                    for (int m = 0; m < d.data.Height; m++) {
                        CellData cellData = d.data.cellData[l][m];
                        if (cellData != null) {
                            if (cellData.type == CellType.FLOOR) { BuildShadowIndex(self, tileIndices, cellData, d, map, l, m);
                            } else if (cellData.type == CellType.PIT) {
                                BuildPitShadowIndex(self, tileIndices, cellData, d, map, l, m);
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
            map.Editor__SpriteCollection = tileIndices.dungeonCollection;
            if (d.ActuallyGenerateTilemap) {
                IEnumerator BuildTracker = map.DeferredBuild(tk2dTileMap.BuildFlags.Default);
                while (BuildTracker.MoveNext()) { yield return null; }
            }
            yield break;
        }

        private void BuildShadowIndex(TK2DDungeonAssembler self, TileIndices tileIndices, CellData current, Dungeon d, tk2dTileMap map, int ix, int iy) {
            if (self.BCheck(d, ix, iy, -2)) {
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
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallUpAndLeft);
                    } else if (flag3 && flag2 && !flag) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallUpAndRight);
                    } else if (flag3 && flag && flag2) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallUpAndBoth);
                    } else if (flag3 && !flag && !flag2) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorTileIndex);
                    }
                } else if (cellData3.diagonalWallType == DiagonalWallType.NORTHEAST && cellData3.type == CellType.WALL) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, tileIndices.aoTileIndices.AOFloorDiagonalWallNortheast);
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, (!flag2) ? tileIndices.aoTileIndices.AOFloorDiagonalWallNortheastLower : tileIndices.aoTileIndices.AOFloorDiagonalWallNortheastLowerJoint);
                } else if (cellData3.diagonalWallType == DiagonalWallType.NORTHWEST && cellData3.type == CellType.WALL) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y + 1, tileIndices.aoTileIndices.AOFloorDiagonalWallNorthwest);
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, (!flag) ? tileIndices.aoTileIndices.AOFloorDiagonalWallNorthwestLower : tileIndices.aoTileIndices.AOFloorDiagonalWallNorthwestLowerJoint);
                }
                if (!flag3) {
                    bool flag7 = flag && !d.data.isTopWall(ix - 1, iy + 1);
                    bool flag8 = flag2 && !d.data.isTopWall(ix + 1, iy + 1);
                    if (flag7 && flag8) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallBoth);
                    } else if (flag7) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallLeft);
                    } else if (flag8) {
                        map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallRight);
                    }
                }
                if (!flag3 && flag5 && !flag6 && !flag && !flag2 && !flag4) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorPizzaSliceLeft);
                } else if (!flag3 && !flag5 && !flag && !flag2 && flag4 && !flag6) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorPizzaSliceRight);
                } else if (!flag3 && flag5 && !flag6 && !flag2 && !flag && flag4 && !flag6) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorPizzaSliceBoth);
                } else if (!flag3 && flag5 && !flag6 && !flag && flag2) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorPizzaSliceLeftWallRight);
                } else if (!flag3 && flag && !flag2 && flag4 && !flag6) {
                    map.Layers[GlobalDungeonData.shadowLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorPizzaSliceRightWallLeft);
                }
            }
        }

        private void BuildPitShadowIndex(TK2DDungeonAssembler self, TileIndices tileIndices, CellData current, Dungeon d, tk2dTileMap map, int ix, int iy) {
            if (!d.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex].doPitAO) { return; }
            if (current != null && current.cellVisualData.hasStampedPath) { return; }
            int floorLayerIndex = GlobalDungeonData.floorLayerIndex;
            if (self.BCheck(d, ix, iy, -2)) {
                CellData cellData = d.data.cellData[ix - 1][iy];
                CellData cellData2 = d.data.cellData[ix + 1][iy];
                CellData cellData3 = d.data.cellData[ix][iy + 1];
                CellData cellData4 = d.data.cellData[ix][iy + 2];
                CellData cellData5 = d.data.cellData[ix + 1][iy + 2];
                CellData cellData6 = d.data.cellData[ix + 1][iy + 1];
                CellData cellData7 = d.data.cellData[ix - 1][iy + 2];
                CellData cellData8 = d.data.cellData[ix - 1][iy + 1];
                DungeonMaterial dungeonMaterial = d.roomMaterialDefinitions[current.cellVisualData.roomVisualTypeIndex];
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
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallUpAndLeft);
                    } else if (flag3 && flag2 && !flag) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallUpAndRight);
                    } else if (flag3 && flag && flag2) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallUpAndBoth);
                    } else if (flag3 && !flag && !flag2) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorTileIndex);
                    }
                } else if (flag3 && flag && !flag2) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOBottomWallTileLeftIndex);
                } else if (flag3 && flag2 && !flag) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOBottomWallTileRightIndex);
                } else if (flag3 && flag && flag2) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOBottomWallTileBothIndex);
                } else if (flag3 && !flag && !flag2) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOBottomWallBaseTileIndex);
                }
                if (!flag3) {
                    bool flag6 = flag && !d.data.isTopWall(current.positionInTilemap.x - 1, current.positionInTilemap.y + 1);
                    bool flag7 = flag2 && !d.data.isTopWall(current.positionInTilemap.x + 1, current.positionInTilemap.y + 1);
                    if (flag6 && flag7) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallBoth);
                    } else if (flag6) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallLeft);
                    } else if (flag7) {
                        map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorWallRight);
                    }
                }
                if (!flag3 && flag5 && !flag && !flag2 && !flag4) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorPizzaSliceLeft);
                } else if (!flag3 && !flag5 && !flag && !flag2 && flag4) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorPizzaSliceRight);
                } else if (!flag3 && flag5 && !flag2 && !flag && flag4) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorPizzaSliceBoth);
                } else if (!flag3 && flag5 && !flag && flag2) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorPizzaSliceLeftWallRight);
                } else if (!flag3 && flag && !flag2 && flag4) {
                    map.Layers[floorLayerIndex].SetTile(current.positionInTilemap.x, current.positionInTilemap.y, tileIndices.aoTileIndices.AOFloorPizzaSliceRightWallLeft);
                }
            }
        }

    }
}

