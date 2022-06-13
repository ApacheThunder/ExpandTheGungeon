using Dungeonator;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandComponents;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandPlaceCorruptTiles {

        public static void PlaceCorruptTiles(Dungeon dungeon, RoomHandler roomHandler = null, GameObject parentObject = null, bool corruptWallsOnly = false, bool isLeadKeyRoom = false, bool isCorruptedJunkRoom = false) {

            bool m_CorruptedSecretRoomsPresent = false;

            if (roomHandler == null) {
                foreach(RoomHandler room in dungeon.data.rooms) {
                    if (room.GetRoomName() != null) {
                        if (room.GetRoomName().ToLower().StartsWith("expand apache corrupted secret")) {
                            m_CorruptedSecretRoomsPresent = true;
                            break;
                        }
                    }
                }
            }

            if (roomHandler == null && dungeon.IsGlitchDungeon) {
                if (StaticReferenceManager.AllNpcs != null && StaticReferenceManager.AllNpcs.Count > 0) {
                    foreach (TalkDoerLite npc in StaticReferenceManager.AllNpcs) { npc.SpeaksGleepGlorpenese = true; }
                }
            } else if (roomHandler != null && !isCorruptedJunkRoom && StaticReferenceManager.AllNpcs != null && StaticReferenceManager.AllNpcs.Count > 0) {
                foreach (TalkDoerLite npc in StaticReferenceManager.AllNpcs) {
                    if (npc.GetAbsoluteParentRoom() == roomHandler) {
                        npc.SpeaksGleepGlorpenese = true;
                        if (npc.GetAbsoluteParentRoom() != null && !string.IsNullOrEmpty(npc.GetAbsoluteParentRoom().GetRoomName())) {
                            if (npc.GetAbsoluteParentRoom().GetRoomName().ToLower().StartsWith("expand apache corrupted secret")) {
                                ExpandShaders.Instance.ApplyGlitchShader(npc.GetComponent<tk2dBaseSprite>());
                            }
                        }
                    }
                }
            }

            if (!dungeon.IsGlitchDungeon && roomHandler == null && !m_CorruptedSecretRoomsPresent) {
                if (roomHandler == null | !isCorruptedJunkRoom) { return; }
            }

            if (dungeon.IsGlitchDungeon | roomHandler != null) {
                if (!isCorruptedJunkRoom) { m_CorruptedSecretRoomsPresent = false; }
            }

            tk2dSpriteCollectionData dungeonCollection = dungeon.tileIndices.dungeonCollection;

            // Used for debug read out information
            int CorruptWallTilesPlaced = 0;
            int CorruptOpenAreaTilesPlaced = 0;
            int iterations = 0;

            GameObject GlitchedTileObject = new GameObject("GlitchTile_" + UnityEngine.Random.Range(1000000, 9999999)) { layer = 22 };
            if (parentObject != null) { GlitchedTileObject.transform.parent = parentObject.transform; }
            GlitchedTileObject.AddComponent<tk2dSprite>();
            tk2dSprite glitchSprite = GlitchedTileObject.GetComponent<tk2dSprite>();
            glitchSprite.Collection = dungeonCollection;
            glitchSprite.SetSprite(glitchSprite.Collection, 22);
            glitchSprite.ignoresTiltworldDepth = false;
            glitchSprite.depthUsesTrimmedBounds = false;
            glitchSprite.allowDefaultLayer = false;
            glitchSprite.OverrideMaterialMode = tk2dBaseSprite.SpriteMaterialOverrideMode.NONE;
            glitchSprite.independentOrientation = false;
            glitchSprite.hasOffScreenCachedUpdate = false;
            glitchSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.PERPENDICULAR;
            glitchSprite.SortingOrder = 2;
            glitchSprite.IsBraveOutlineSprite = false;
            glitchSprite.IsZDepthDirty = false;
            glitchSprite.ApplyEmissivePropertyBlock = false;
            glitchSprite.GenerateUV2 = false;
            glitchSprite.LockUV2OnFrameOne = false;
            glitchSprite.StaticPositions = false;
           
            List<int> CurrentFloorWallIDs = new List<int>();
            List<int> CurrentFloorFloorIDs = new List<int>();
            List<int> CurrentFloorMiscIDs = new List<int>();

            // Select Sprite ID lists based on tileset. (IDs corrispond to different sprites depending on tileset dungeonCollection)
            if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                CurrentFloorWallIDs = ExpandLists.CastleWallIDs;
                CurrentFloorFloorIDs = ExpandLists.CastleFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.CastleMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.GUNGEON) {
                CurrentFloorWallIDs = ExpandLists.GungeonWallIDs;
                CurrentFloorFloorIDs = ExpandLists.GungeonFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.GungeonMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.MINEGEON) {
                CurrentFloorWallIDs = ExpandLists.MinesWallIDs;
                CurrentFloorFloorIDs = ExpandLists.MinesFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.MinesMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATACOMBGEON) {
                CurrentFloorWallIDs = ExpandLists.HollowsWallIDs;
                CurrentFloorFloorIDs = ExpandLists.HollowsFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.HollowsMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                CurrentFloorWallIDs = ExpandLists.ForgeWallIDs;
                CurrentFloorFloorIDs = ExpandLists.ForgeFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.ForgeMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.HELLGEON) {
                CurrentFloorWallIDs = ExpandLists.BulletHell_WallIDs;
                CurrentFloorFloorIDs = ExpandLists.BulletHell_FloorIDs;
                CurrentFloorMiscIDs = ExpandLists.BulletHell_MiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.SEWERGEON) {
                CurrentFloorWallIDs = ExpandLists.SewerWallIDs;
                CurrentFloorFloorIDs = ExpandLists.SewerFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.SewerMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATHEDRALGEON) {
                CurrentFloorWallIDs = ExpandLists.AbbeyWallIDs;
                CurrentFloorFloorIDs = ExpandLists.AbbeyFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.AbbeyMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.RATGEON | dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.JUNGLEGEON) {
                CurrentFloorWallIDs = ExpandLists.RatDenWallIDs;
                CurrentFloorFloorIDs = ExpandLists.RatDenFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.RatDenMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON) {
                foreach (int id in ExpandLists.Nakatomi_OfficeWallIDs) { CurrentFloorWallIDs.Add(id); }
                foreach (int id in ExpandLists.Nakatomi_OfficeFloorIDs) { CurrentFloorFloorIDs.Add(id); }
                foreach (int id in ExpandLists.Nakatomi_OfficeMiscIDs) { CurrentFloorMiscIDs.Add(id); }
                // This floor stores both Office and Future tilesets it uses into the same sprite collection atlas.
                // Each section has a specific size and and the ID of the last tileset will be subtracted from entries from the next.
                // Office tileset IDs end at id 703
                // Future tileset IDs start at id 704
                // Future tileset IDs have 704 added to them to get the correct ID in the main sprite collection.
                foreach (int id in ExpandLists.Nakatomi_FutureWallIDs) { CurrentFloorWallIDs.Add(id + 704); }
                foreach (int id in ExpandLists.Nakatomi_FutureFloorIDs) { CurrentFloorFloorIDs.Add(id + 704); }
                foreach (int id in ExpandLists.Nakatomi_FutureMiscIDs) { CurrentFloorMiscIDs.Add(id + 704); }
            } else {
                // Unkown Tilesets will use Gungeon as placeholder
                Dungeon tempDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Gungeon");
                dungeonCollection = tempDungeonPrefab.tileIndices.dungeonCollection;
                CurrentFloorWallIDs = ExpandLists.GungeonWallIDs;
                CurrentFloorFloorIDs = ExpandLists.GungeonFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.GungeonMiscIDs;
                tempDungeonPrefab = null;
            }

            List<int> roomList = Enumerable.Range(0, dungeon.data.rooms.Count).ToList();
            roomList = roomList.Shuffle();

            if (roomHandler != null) { roomList = new List<int>() { 0 }; } 

            List<IntVector2> cachedWallTiles = new List<IntVector2>();
            List<IntVector2> validWalls = new List<IntVector2>();
            List<IntVector2> validOpenAreas = new List<IntVector2>();

            RoomHandler RoomBeingWorkedOn = null;
            
            while (iterations < roomList.Count) {
                try {
                    RoomHandler currentRoom = null;

                    if (roomHandler == null) {
                        currentRoom = dungeon.data.rooms[roomList[iterations]];                        
                    } else {
                        currentRoom = roomHandler;
                    }

                    if (currentRoom == null) { break; }

                    RoomBeingWorkedOn = currentRoom;

                    if (string.IsNullOrEmpty(currentRoom.GetRoomName())) {
                        currentRoom.area.PrototypeRoomName = ("ProceduralRoom_" + UnityEngine.Random.Range(100000, 999999));
                    }
                    
                    if (!m_CorruptedSecretRoomsPresent || (currentRoom.GetRoomName().ToLower().StartsWith("expand apache corrupted secret") | dungeon.IsGlitchDungeon | roomHandler != null)) {

                        bool isCorruptedSecretRoom = false;

                        if ((currentRoom.GetRoomName().ToLower().StartsWith("expand apache corrupted secret") | isCorruptedJunkRoom) && !isLeadKeyRoom) {
                            GameObject m_CorruptionMarkerObject = new GameObject("CorruptionAmbienceMarkerObject") { layer = 0 };
                            m_CorruptionMarkerObject.transform.position = currentRoom.area.Center;
                            m_CorruptionMarkerObject.transform.parent = currentRoom.hierarchyParent;
                            ExpandStaticReferenceManager.AllCorruptionSoundObjects.Add(m_CorruptionMarkerObject);
                            isCorruptedSecretRoom = true;

                            if (isCorruptedJunkRoom) {
                                m_CorruptionMarkerObject.AddComponent<ExpandCorruptedRoomAmbiencePlacable>();
                                ExpandCorruptedRoomAmbiencePlacable m_SoundPlacable = m_CorruptionMarkerObject.GetComponent<ExpandCorruptedRoomAmbiencePlacable>();
                                m_SoundPlacable.CorruptionFXPlayEvent = "Play_EX_CorruptionAmbience_01";
                                m_SoundPlacable.CorruptionFXStopEvent = "Stop_EX_CorruptionAmbience_01";
                                m_SoundPlacable.ConfigureOnPlacement(currentRoom);
                            }
                        }

                        if ((m_CorruptedSecretRoomsPresent | isLeadKeyRoom) && !isCorruptedJunkRoom) {
                            foreach (TalkDoerLite npc in StaticReferenceManager.AllNpcs) {
                                if (npc.GetAbsoluteParentRoom() != null && npc.GetAbsoluteParentRoom() == currentRoom) {
                                    npc.SpeaksGleepGlorpenese = true;
                                    ExpandShaders.Instance.ApplyGlitchShader(npc.GetComponent<tk2dBaseSprite>());
                                }
                            }
                        }

                        validWalls.Clear();
                        validOpenAreas.Clear();

                        for (int Width = -1; Width <= currentRoom.area.dimensions.x + 3; Width++) {
                            for (int Height = -1; Height <= currentRoom.area.dimensions.y + 3; Height++) {
                                int X = currentRoom.area.basePosition.x + Width;
                                int Y = currentRoom.area.basePosition.y + Height;
                                if (!cachedWallTiles.Contains(new IntVector2(X, Y)) && (
                                        dungeon.data.isWall(X, Y) | dungeon.data.isAnyFaceWall(X, Y) | dungeon.data.isWall(X, Y - 1))
                                    )
                                {
                                    validWalls.Add(new IntVector2(X, Y));
                                }
                            }
                        }

                        int WallCorruptionIntensity = (validWalls.Count / UnityEngine.Random.Range(2, 4));
                        if (roomHandler == null && !isCorruptedSecretRoom && UnityEngine.Random.value <= 0.1f) { WallCorruptionIntensity = 0; }

                        if (WallCorruptionIntensity > 0) {
                            for (int C = 0; C < WallCorruptionIntensity; C++) {
                                if (validWalls.Count > 0) {
                                    IntVector2 WallPosition = BraveUtility.RandomElement(validWalls);
                                    cachedWallTiles.Add(WallPosition);

                                    float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                                    float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                                    float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                                    float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                                    float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);

                                    GameObject m_GlitchTile = UnityEngine.Object.Instantiate(GlitchedTileObject, (WallPosition.ToVector2()), Quaternion.identity);
                                    m_GlitchTile.name += ("_" + UnityEngine.Random.Range(100000, 999999).ToString());
                                    m_GlitchTile.layer = 22;
                                    if (parentObject != null) {
                                        m_GlitchTile.transform.parent = parentObject.transform;
                                    } else {
                                        m_GlitchTile.transform.parent = currentRoom.hierarchyParent;
                                    }

                                    tk2dSprite m_GlitchSprite = m_GlitchTile.GetComponent<tk2dSprite>();

                                    int TileType = UnityEngine.Random.Range(1, 3);
                                    List<int> spriteIDs = new List<int>();
                                    if (TileType == 1) { spriteIDs = CurrentFloorWallIDs; }
                                    if (TileType == 2) { spriteIDs = CurrentFloorFloorIDs; }
                                    if (TileType == 3) { spriteIDs = CurrentFloorMiscIDs; }

                                    m_GlitchSprite.SetSprite(BraveUtility.RandomElement(spriteIDs));

                                    if (dungeon.data.isFaceWallLower(WallPosition.x, WallPosition.y) && !dungeon.data.isWall(WallPosition.x, WallPosition.y - 1)) {
                                        DepthLookupManager.ProcessRenderer(m_GlitchSprite.renderer, DepthLookupManager.GungeonSortingLayer.BACKGROUND);
                                        m_GlitchSprite.IsPerpendicular = false;
                                        m_GlitchSprite.HeightOffGround = 0;
                                        m_GlitchSprite.UpdateZDepth();
                                    } else {
                                        m_GlitchSprite.HeightOffGround = 3;
                                        m_GlitchSprite.UpdateZDepth();
                                        m_GlitchTile.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
                                    }
                                    
                                    if (roomHandler != null && !isCorruptedSecretRoom && !isLeadKeyRoom) {
                                        m_GlitchTile.AddComponent<DebrisObject>();
                                        DebrisObject m_GlitchDebris = m_GlitchTile.GetComponent<DebrisObject>();
                                        m_GlitchDebris.angularVelocity = 0;
                                        m_GlitchDebris.angularVelocityVariance = 0;
                                        m_GlitchDebris.animatePitFall = false;
                                        m_GlitchDebris.bounceCount = 0;
                                        m_GlitchDebris.breakOnFallChance = 0;
                                        m_GlitchDebris.breaksOnFall = false;
                                        m_GlitchDebris.canRotate = false;
                                        m_GlitchDebris.changesCollisionLayer = false;
                                        m_GlitchDebris.collisionStopsBullets = false;
                                        m_GlitchDebris.followupBehavior = DebrisObject.DebrisFollowupAction.None;
                                        m_GlitchDebris.IsAccurateDebris = true;
                                        m_GlitchDebris.IsCorpse = false;
                                        m_GlitchDebris.motionMultiplier = 0;
                                        m_GlitchDebris.pitFallSplash = false;
                                        m_GlitchDebris.playAnimationOnTrigger = false;
                                        m_GlitchDebris.PreventAbsorption = true;
                                        m_GlitchDebris.PreventFallingInPits = true;
                                        m_GlitchDebris.Priority = EphemeralObject.EphemeralPriority.Ephemeral;
                                        m_GlitchDebris.shouldUseSRBMotion = false;
                                        m_GlitchDebris.usesDirectionalFallAnimations = false;
                                        m_GlitchDebris.lifespanMax = 600;
                                        m_GlitchDebris.lifespanMin = 500;
                                        m_GlitchDebris.usesLifespan = true;
                                        
                                    } else {
                                        m_GlitchTile.AddComponent<ExpandCorruptedObjectDummyComponent>();
                                        ExpandCorruptedObjectDummyComponent dummyComponent = m_GlitchTile.GetComponent<ExpandCorruptedObjectDummyComponent>();
                                        dummyComponent.Init();
                                    }

                                    if (UnityEngine.Random.value <= 0.5f) {
                                        ExpandShaders.Instance.ApplyGlitchShader(m_GlitchSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                    }
                                    CorruptWallTilesPlaced++;
                                    validWalls.Remove(WallPosition);
                                }
                            }
                        }

                        for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                            for (int Height = -1; Height <= currentRoom.area.dimensions.y; Height++) {
                                int X = currentRoom.area.basePosition.x + Width;
                                int Y = currentRoom.area.basePosition.y + Height;
                                if (!dungeon.data.isWall(X, Y) && !dungeon.data.isAnyFaceWall(X, Y)) { validOpenAreas.Add(new IntVector2(X, Y)); }
                            }
                        }

                        int OpenAreaCorruptionIntensity = (validOpenAreas.Count / UnityEngine.Random.Range(5, 10));

                        if (UnityEngine.Random.value <= 0.2f | isCorruptedSecretRoom) {
                            if (isCorruptedSecretRoom) {
                                if (isCorruptedJunkRoom) {
                                    OpenAreaCorruptionIntensity = (validOpenAreas.Count / UnityEngine.Random.Range(3, 6));
                                } else {
                                    OpenAreaCorruptionIntensity = (validOpenAreas.Count / UnityEngine.Random.Range(2, 5));
                                }
                            } else {
                                OpenAreaCorruptionIntensity = (validOpenAreas.Count / UnityEngine.Random.Range(3, 6));
                            }
                            
                        }

                        if ((roomHandler == null && !isCorruptedSecretRoom && UnityEngine.Random.value <= 0.15f) | corruptWallsOnly) {
                            OpenAreaCorruptionIntensity = 0;
                        }

                        if (OpenAreaCorruptionIntensity > 0 && !currentRoom.IsShop && currentRoom.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.BOSS) {
                            for (int S = 0; S <= OpenAreaCorruptionIntensity; S++) {
                                IntVector2 OpenAreaPosition = BraveUtility.RandomElement(validOpenAreas);
                        
                                float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                                float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                                float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                                float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                                float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);
                        
                                GameObject m_GlitchTile = UnityEngine.Object.Instantiate(GlitchedTileObject, (OpenAreaPosition.ToVector2()), Quaternion.identity);
                                m_GlitchTile.name += ("_" + UnityEngine.Random.Range(100000, 999999).ToString());
                                m_GlitchTile.SetLayerRecursively(LayerMask.NameToLayer("BG_Critical"));

                                if (parentObject != null) {
                                    m_GlitchTile.transform.parent = parentObject.transform;
                                } else {
                                    m_GlitchTile.transform.parent = currentRoom.hierarchyParent;
                                }
                        
                                tk2dSprite m_GlitchSprite = m_GlitchTile.GetComponent<tk2dSprite>();
                                int TileType = UnityEngine.Random.Range(1, 3);
                                List<int> spriteIDs = new List<int>();
                                if (TileType == 1) { spriteIDs = CurrentFloorWallIDs; }
                                if (TileType == 2) { spriteIDs = CurrentFloorFloorIDs; }
                                if (TileType == 3) { spriteIDs = CurrentFloorMiscIDs; }
                        
                                m_GlitchSprite.SetSprite(BraveUtility.RandomElement(spriteIDs));
                        
                                if (UnityEngine.Random.value <= 0.3f) {
                                    ExpandShaders.Instance.ApplyGlitchShader(m_GlitchSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                }
                                DepthLookupManager.ProcessRenderer(m_GlitchSprite.renderer, DepthLookupManager.GungeonSortingLayer.BACKGROUND);
                                m_GlitchSprite.IsPerpendicular = false;
                                m_GlitchSprite.HeightOffGround = -1.7f;
                                m_GlitchSprite.SortingOrder = 2;
                                m_GlitchSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.FLAT;
                                m_GlitchSprite.UpdateZDepth();

                                if (roomHandler != null && !isCorruptedSecretRoom) {
                                    m_GlitchTile.AddComponent<DebrisObject>();
                                    DebrisObject m_GlitchDebris = m_GlitchTile.GetComponent<DebrisObject>();
                                    m_GlitchDebris.angularVelocity = 0;
                                    m_GlitchDebris.angularVelocityVariance = 0;
                                    m_GlitchDebris.animatePitFall = false;
                                    m_GlitchDebris.bounceCount = 0;
                                    m_GlitchDebris.breakOnFallChance = 0;
                                    m_GlitchDebris.breaksOnFall = false;
                                    m_GlitchDebris.canRotate = false;
                                    m_GlitchDebris.changesCollisionLayer = false;
                                    m_GlitchDebris.collisionStopsBullets = false;
                                    m_GlitchDebris.followupBehavior = DebrisObject.DebrisFollowupAction.None;
                                    m_GlitchDebris.IsAccurateDebris = true;
                                    m_GlitchDebris.IsCorpse = false;
                                    m_GlitchDebris.motionMultiplier = 0;
                                    m_GlitchDebris.pitFallSplash = false;
                                    m_GlitchDebris.playAnimationOnTrigger = false;
                                    m_GlitchDebris.PreventAbsorption = true;
                                    m_GlitchDebris.PreventFallingInPits = true;
                                    m_GlitchDebris.Priority = EphemeralObject.EphemeralPriority.Ephemeral;
                                    m_GlitchDebris.shouldUseSRBMotion = false;
                                    m_GlitchDebris.usesDirectionalFallAnimations = false;
                                    if (!isCorruptedSecretRoom && roomHandler != null) {
                                        m_GlitchDebris.lifespanMax = 600;
                                        m_GlitchDebris.lifespanMin = 500;
                                        m_GlitchDebris.usesLifespan = true;
                                    } else {
                                        m_GlitchDebris.usesLifespan = false;
                                    }
                                } else {
                                    m_GlitchTile.AddComponent<ExpandCorruptedObjectDummyComponent>();
                                    ExpandCorruptedObjectDummyComponent dummyComponent = m_GlitchTile.GetComponent<ExpandCorruptedObjectDummyComponent>();
                                    dummyComponent.Init();
                                }
                                CorruptOpenAreaTilesPlaced++;
                                validOpenAreas.Remove(OpenAreaPosition);
                            }
                        }
                    }
                    iterations++;
                } catch (Exception ex) {
                    if (ExpandSettings.debugMode) {
                        if (RoomBeingWorkedOn != null && !string.IsNullOrEmpty(RoomBeingWorkedOn.GetRoomName())) {
                            ETGModConsole.Log("[DEBUG] Exception occured in Dungeon.PlaceWallMimics with room: " + RoomBeingWorkedOn.GetRoomName());
                            Debug.Log("Exception caught in Dungeon.PlaceWallMimics with room: " + RoomBeingWorkedOn.GetRoomName());
                        } else {
                            ETGModConsole.Log("[DEBUG] Exception occured in Dungeon.PlaceWallMimics!");
                            Debug.Log("Exception caught in Dungeon.PlaceWallMimics!");
                        }                        
                        Debug.LogException(ex);
                    }
                    if (CorruptWallTilesPlaced > 0) {
                        if (ExpandSettings.debugMode) {
                            ETGModConsole.Log("[DEBUG] Number of corrupted wall tiles succesfully placed: " + CorruptWallTilesPlaced, false);
                            ETGModConsole.Log("[DEBUG] Number of corrupted ppen area tiles succesfully placed: " + CorruptOpenAreaTilesPlaced, false);
                        }
                    }
                    if (RoomBeingWorkedOn != null) { RoomBeingWorkedOn = null; }
                    iterations++;
                }
            }
            if (CorruptWallTilesPlaced > 0) {
            	if (ExpandSettings.debugMode) {
            		ETGModConsole.Log("[DEBUG] Number of Valid Corrupted Wall Tile locations: " + CorruptWallTilesPlaced, false);
            		ETGModConsole.Log("[DEBUG] Number of Valid Corrupted locations: " + CorruptOpenAreaTilesPlaced, false);
            	}
            }
            UnityEngine.Object.Destroy(GlitchedTileObject);
            return;
        }
    }
}

