using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandUtilities;
using Dungeonator;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandRoomPrefabs : MonoBehaviour {
        // Custom Room Prefabs
        public static PrototypeDungeonRoom Giant_Elevator_Room;
        // This room prefab was removed in 2.1.8. I will recreate in code now plus with a few extras of my own design.
        public static PrototypeDungeonRoom Utiliroom;
        public static PrototypeDungeonRoom Utiliroom_SpecialPit;
        public static PrototypeDungeonRoom Utiliroom_Pitfall;
        // Special Room Prefabs for secret glitch floor
        public static PrototypeDungeonRoom SpecialWallMimicRoom;
        public static PrototypeDungeonRoom SpecialMaintenanceRoom;
        public static PrototypeDungeonRoom ShopBackRoom;
        public static PrototypeDungeonRoom SecretRewardRoom;
        public static PrototypeDungeonRoom SecretBossRoom;
        public static PrototypeDungeonRoom FakeBossRoom;
        public static PrototypeDungeonRoom SecretExitRoom;
        public static PrototypeDungeonRoom ThwompCrossingVertical;
        public static PrototypeDungeonRoom ThwompCrossingVerticalNoRain;
        public static PrototypeDungeonRoom ThwompCrossingHorizontal;
        public static PrototypeDungeonRoom PuzzleRoom3;

        // Special Rooms used under certain circumstances when Player picks up Corrupted Junk.
        public static PrototypeDungeonRoom CreepyGlitchRoom;
        public static PrototypeDungeonRoom CreepyGlitchRoom_Entrance;
        // General Purpose Boss Room for Gungeoneer Mimic Boss;
        public static PrototypeDungeonRoom GungeoneerMimicBossRoom;

        // Custom rooms for the regular floors. (most designs provided courtasy of TheTurtleMelon)

        // Rooms for Floor 1. 
        public static PrototypeDungeonRoom Expand_Explode;
        public static PrototypeDungeonRoom Expand_C_Hub;
        public static PrototypeDungeonRoom Expand_C_Gap;
        public static PrototypeDungeonRoom Expand_ChainGap;
        public static PrototypeDungeonRoom Expand_Challange1;
        public static PrototypeDungeonRoom Expand_Pit_Line;
        public static PrototypeDungeonRoom Expand_Singer_Gap;
        public static PrototypeDungeonRoom Expand_Flying_Gap;
        public static PrototypeDungeonRoom Expand_Battle;
        public static PrototypeDungeonRoom Expand_Cross;
        public static PrototypeDungeonRoom Expand_Blocks;
        public static PrototypeDungeonRoom Expand_Blocks_Pits;
        public static PrototypeDungeonRoom Expand_Wall_Pit;
        public static PrototypeDungeonRoom Expand_Gate_Cross;
        public static PrototypeDungeonRoom Expand_Passage;
        public static PrototypeDungeonRoom Expand_Pit_Jump;
        public static PrototypeDungeonRoom Expand_Pit_Passage;
        public static PrototypeDungeonRoom Expand_R_Blocks;
        public static PrototypeDungeonRoom Expand_Small_Passage;
        public static PrototypeDungeonRoom Expand_Box;
        public static PrototypeDungeonRoom Expand_Steps;
        public static PrototypeDungeonRoom Expand_Spiral;
        public static PrototypeDungeonRoom Expand_Apache_Hub;
        public static PrototypeDungeonRoom Expand_Box_Hub;
        public static PrototypeDungeonRoom Expand_Enclose_Hub;

        // Rooms for floor 4.
        public static PrototypeDungeonRoom Expand_SpiderMaze;
        
        // Custom Secret Rooms
        public static PrototypeDungeonRoom Expand_TinySecret;
        public static PrototypeDungeonRoom Expand_GlitchedSecret;
        public static PrototypeDungeonRoom Expand_SecretElevatorEntranceRoom;

        // Custom Rooms for handling entrance to custom secret floor on Hollows
        public static PrototypeDungeonRoom SecretExitRoom2;
        public static PrototypeDungeonRoom SecretRatEntranceRoom;

        // General purpose destination room for "normal" version of custom elevator object.
        public static PrototypeDungeonRoom Expand_SecretElevatorDestinationRoom;

        public static WeightedRoom GenerateWeightedRoom(PrototypeDungeonRoom Room, float Weight = 1, bool LimitedCopies = true, int MaxCopies = 1, DungeonPrerequisite[] AdditionalPrerequisites = null) {
            if (Room == null) { return null; }
            if (AdditionalPrerequisites == null) { AdditionalPrerequisites = new DungeonPrerequisite[0]; }
            return new WeightedRoom() { room = Room, weight = Weight, limitedCopies = LimitedCopies, maxCopies = MaxCopies, additionalPrerequisites = AdditionalPrerequisites };
        }


        public static void InitCustomRooms() {

            Giant_Elevator_Room = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Utiliroom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Utiliroom_SpecialPit = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Utiliroom_Pitfall = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            SpecialWallMimicRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            SpecialMaintenanceRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            ShopBackRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            SecretRewardRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            SecretBossRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            FakeBossRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            SecretExitRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            ThwompCrossingVertical = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            ThwompCrossingVerticalNoRain = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            ThwompCrossingHorizontal = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            PuzzleRoom3 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            CreepyGlitchRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            CreepyGlitchRoom_Entrance = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            GungeoneerMimicBossRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            Expand_Explode = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_C_Hub = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_C_Gap = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_ChainGap = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Challange1 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Pit_Line = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Singer_Gap = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Flying_Gap = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Battle = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Cross = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Blocks = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Blocks_Pits = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Wall_Pit = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Gate_Cross = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Passage = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Pit_Jump = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Pit_Passage = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_R_Blocks = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Small_Passage = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Box = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Steps = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Spiral = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Apache_Hub = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Box_Hub = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Enclose_Hub = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            Expand_SpiderMaze = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            SecretExitRoom2 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            SecretRatEntranceRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            Expand_SecretElevatorDestinationRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            Expand_TinySecret = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_GlitchedSecret = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_SecretElevatorEntranceRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();



            ExpandObjectDatabase objectDatabase = new ExpandObjectDatabase();
            AssetBundle sharedAssets = ResourceManager.LoadAssetBundle("shared_auto_001");
            AssetBundle sharedAssets2 = ResourceManager.LoadAssetBundle("shared_auto_002");

            FakeBossRoom.name = "Fake Boss Room";
            FakeBossRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            FakeBossRoom.GUID = Guid.NewGuid().ToString();
            FakeBossRoom.PreventMirroring = false;
            FakeBossRoom.category = PrototypeDungeonRoom.RoomCategory.BOSS;
            FakeBossRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.MINI_BOSS;
            FakeBossRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            FakeBossRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            FakeBossRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            FakeBossRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            FakeBossRoom.pits = new List<PrototypeRoomPitEntry>();
            FakeBossRoom.placedObjects = new List<PrototypePlacedObjectData>();
            FakeBossRoom.placedObjectPositions = new List<Vector2>();
            FakeBossRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            FakeBossRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            FakeBossRoom.overriddenTilesets = 0;
            FakeBossRoom.prerequisites = new List<DungeonPrerequisite>();
            FakeBossRoom.InvalidInCoop = false;
            FakeBossRoom.cullProceduralDecorationOnWeakPlatforms = false;
            FakeBossRoom.preventAddedDecoLayering = false;
            FakeBossRoom.precludeAllTilemapDrawing = false;
            FakeBossRoom.drawPrecludedCeilingTiles = false;
            FakeBossRoom.preventBorders = false;
            FakeBossRoom.preventFacewallAO = false;
            FakeBossRoom.usesCustomAmbientLight = false;
            FakeBossRoom.customAmbientLight = Color.white;
            FakeBossRoom.ForceAllowDuplicates = false;
            FakeBossRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            FakeBossRoom.IsLostWoodsRoom = false;
            FakeBossRoom.UseCustomMusic = false;
            FakeBossRoom.UseCustomMusicState = false;
            FakeBossRoom.CustomMusicEvent = string.Empty;
            FakeBossRoom.UseCustomMusicSwitch = false;
            FakeBossRoom.CustomMusicSwitch = string.Empty;
            FakeBossRoom.overrideRoomVisualTypeForSecretRooms = false;
            FakeBossRoom.rewardChestSpawnPosition = new IntVector2(12, 12);
            FakeBossRoom.Width = 25;
            FakeBossRoom.Height = 25;
            FakeBossRoom.associatedMinimapIcon = ExpandPrefabs.GatlingGullRoom05.associatedMinimapIcon;
            RoomFromText.AddExitToRoom(FakeBossRoom, new Vector2(0, 12), DungeonData.Direction.WEST, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomFromText.AddExitToRoom(FakeBossRoom, new Vector2(12, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(FakeBossRoom, new Vector2(26, 12), DungeonData.Direction.EAST, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomFromText.AddExitToRoom(FakeBossRoom, new Vector2(12, 26), DungeonData.Direction.NORTH, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomFromText.GenerateDefaultRoomLayout(FakeBossRoom);
            RoomFromText.AddObjectToRoom(FakeBossRoom, new Vector2(8, 18), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5");
            FakeBossRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "fc809bd43a4d41738a62d7565456622c", // Ser_Manuel
                            contentsBasePosition = new Vector2(12, 12),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() { new Vector2(12, 12) },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 2,
                    suppressPlayerChecks = true,
                    delayTime = 4,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };


            Giant_Elevator_Room.name = "Giant Elevator Room";
            Giant_Elevator_Room.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Giant_Elevator_Room.GUID = Guid.NewGuid().ToString();
            Giant_Elevator_Room.PreventMirroring = false;
            Giant_Elevator_Room.category = PrototypeDungeonRoom.RoomCategory.ENTRANCE;
            Giant_Elevator_Room.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Giant_Elevator_Room.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Giant_Elevator_Room.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Giant_Elevator_Room.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Giant_Elevator_Room.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Giant_Elevator_Room.pits = new List<PrototypeRoomPitEntry>();
            Giant_Elevator_Room.placedObjects = new List<PrototypePlacedObjectData>();
            Giant_Elevator_Room.placedObjectPositions = new List<Vector2>();
            Giant_Elevator_Room.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Giant_Elevator_Room.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Giant_Elevator_Room.overriddenTilesets = 0;
            Giant_Elevator_Room.prerequisites = new List<DungeonPrerequisite>();
            Giant_Elevator_Room.InvalidInCoop = false;
            Giant_Elevator_Room.cullProceduralDecorationOnWeakPlatforms = false;
            Giant_Elevator_Room.preventAddedDecoLayering = false;
            Giant_Elevator_Room.precludeAllTilemapDrawing = false;
            Giant_Elevator_Room.drawPrecludedCeilingTiles = false;
            Giant_Elevator_Room.preventBorders = false;
            Giant_Elevator_Room.preventFacewallAO = false;
            Giant_Elevator_Room.usesCustomAmbientLight = false;
            Giant_Elevator_Room.customAmbientLight = Color.white;
            Giant_Elevator_Room.ForceAllowDuplicates = false;
            Giant_Elevator_Room.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Giant_Elevator_Room.IsLostWoodsRoom = false;
            Giant_Elevator_Room.UseCustomMusic = false;
            Giant_Elevator_Room.UseCustomMusicState = false;
            Giant_Elevator_Room.CustomMusicEvent = string.Empty;
            Giant_Elevator_Room.UseCustomMusicSwitch = false;
            Giant_Elevator_Room.CustomMusicSwitch = string.Empty;
            Giant_Elevator_Room.overrideRoomVisualTypeForSecretRooms = false;
            Giant_Elevator_Room.rewardChestSpawnPosition = new IntVector2(25, 25);
            Giant_Elevator_Room.associatedMinimapIcon = ExpandPrefabs.elevator_entrance.associatedMinimapIcon;
            Giant_Elevator_Room.Width = 100;
            Giant_Elevator_Room.Height = 100;
            Giant_Elevator_Room.overrideRoomVisualType = 3;
            // Left/Right Exits
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 5), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 5), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 14), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 14), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 23), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 23), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 32), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 32), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 41), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 41), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 50), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 50), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 59), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 59), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 68), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 68), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 77), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 77), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 86), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 86), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 95), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 95), DungeonData.Direction.EAST);
            // Top/Bottom Exits
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(5, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(5, 101), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(14, 101), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(23, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(23, 101), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(32, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(32, 101), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(41, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(41, 101), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(50, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(50, 101), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(59, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(59, 101), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(68, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(68, 101), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(77, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(77, 101), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(86, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(86, 101), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(95, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Giant_Elevator_Room, new Vector2(95, 101), DungeonData.Direction.NORTH);
            // Generate Cell Data
            RoomFromText.GenerateRoomFromText(Giant_Elevator_Room, "RoomCellData.Giant_Elevator_Room_Layout.txt");
            // Add Object Spawns
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(47, 49), ExpandPrefabs.ElevatorArrival);
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(48, 41), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(49, 33), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(49, 66), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(29, 49), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(70, 49), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(17, 4), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(28, 4), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(49, 4), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(69, 4), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(80, 4), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(17, 96), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(28, 96), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(49, 96), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(69, 96), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(80, 96), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(3, 16), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(3, 32), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(3, 49), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(3, 66), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(3, 82), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(96, 16), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(96, 32), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(96, 49), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(96, 66), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(96, 82), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(7, 24), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // Blobulon
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(45, 13), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // Bullet Kin
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(87, 17), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // Bullet Kin
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(14, 23), EnemyBehaviourGuid: "db35531e66ce41cbb81d507a34366dfe"); // AK47 Bullet Kin
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(13, 60), EnemyBehaviourGuid: "2752019b770f473193b08b4005dc781f"); // Veteran Shotgun Kin
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(85, 74), EnemyBehaviourGuid: "e861e59012954ab2b9b6977da85cb83c"); // Snake (Office)
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(85, 49), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // Gun Nut
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(28, 77), EnemyBehaviourGuid: "eed5addcc15148179f300cc0d9ee7f94"); // Spogre
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(63, 84), EnemyBehaviourGuid: "98fdf153a4dd4d51bf0bafe43f3c77ff"); // Tazie
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(35, 91), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // Bullet Kin
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(60, 90), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // Bullet Kin
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(35, 85), EnemyBehaviourGuid: "70216cae6c1346309d86d4a0b4603045"); // Veteran Bullet Kin
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(75, 8), EnemyBehaviourGuid: "c5b11bfc065d417b9c4d03a5e385fe2c"); // Professional
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(72, 86), EnemyBehaviourGuid: "3b0bd258b4c9432db3339665cc61c356"); // Cactus Kin
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(12, 39), EnemyBehaviourGuid: "3b0bd258b4c9432db3339665cc61c356"); // Cactus Kin
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(15, 14), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(85, 14), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(15, 86), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(85, 86), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(59, 67), EnemyBehaviourGuid: "479556d05c7c44f3b6abb3b2067fc778"); // Wall Mimic
            /*RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(48.55f, 27), ChaosUtility.GenerateDungeonPlacable(ChaosPrefabs.TableHorizontalStone, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(48.55f, 72), ChaosUtility.GenerateDungeonPlacable(ChaosPrefabs.TableHorizontalStone, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(23, 48.59f), ChaosUtility.GenerateDungeonPlacable(ChaosPrefabs.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(76, 48.59f), ChaosUtility.GenerateDungeonPlacable(ChaosPrefabs.TableVertical, useExternalPrefab: true));*/
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(13, 89), ExpandUtility.GenerateDungeonPlacable(objectDatabase.YellowDrum, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(84, 89), ExpandUtility.GenerateDungeonPlacable(objectDatabase.YellowDrum, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(14, 10), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(84, 10), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(45, 10), objectDatabase.Brazier);
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(45, 89), objectDatabase.Brazier);
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(9, 62), objectDatabase.Brazier);
            RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(89, 62), objectDatabase.Brazier);
            // RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(50, 55), ChaosGlitchFloorGenerator.Instance.CustomGlitchDungeonPlacable(ChaosPrefabs.RainFXObject, useExternalPrefab: true));


            // Replacement to Utiliroom which was removed in 2.1.8
            Utiliroom.name = "Utiliroom";
            Utiliroom.RoomId = 31;
            Utiliroom.QAID = "Xc0003";
            Utiliroom.GUID = "f1f6b58f-b186-4dca-afc4-984daa0d40ad";
            Utiliroom.PreventMirroring = false;
            Utiliroom.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Utiliroom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Utiliroom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Utiliroom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Utiliroom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Utiliroom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Utiliroom.pits = new List<PrototypeRoomPitEntry>();
            Utiliroom.placedObjects = new List<PrototypePlacedObjectData>();
            Utiliroom.placedObjectPositions = new List<Vector2>();
            Utiliroom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Utiliroom.roomEvents = new List<RoomEventDefinition>(0);
            Utiliroom.overriddenTilesets = 0;
            Utiliroom.prerequisites = new List<DungeonPrerequisite>();
            Utiliroom.InvalidInCoop = false;
            Utiliroom.cullProceduralDecorationOnWeakPlatforms = false;
            Utiliroom.preventAddedDecoLayering = false;
            Utiliroom.precludeAllTilemapDrawing = false;
            Utiliroom.drawPrecludedCeilingTiles = false;
            Utiliroom.preventBorders = false;
            Utiliroom.preventFacewallAO = false;
            Utiliroom.usesCustomAmbientLight = false;
            Utiliroom.customAmbientLight = Color.white;
            Utiliroom.ForceAllowDuplicates = false;
            Utiliroom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Utiliroom.IsLostWoodsRoom = false;
            Utiliroom.UseCustomMusic = false;
            Utiliroom.UseCustomMusicState = false;
            Utiliroom.CustomMusicEvent = string.Empty;
            Utiliroom.UseCustomMusicSwitch = false;
            Utiliroom.CustomMusicSwitch = string.Empty;
            Utiliroom.overrideRoomVisualTypeForSecretRooms = false;
            Utiliroom.rewardChestSpawnPosition = IntVector2.One;
            Utiliroom.Width = 4;
            Utiliroom.Height = 4;
            RoomFromText.AddExitToRoom(Utiliroom, new Vector2(0, 2), DungeonData.Direction.WEST, ContainsDoor: false);
            RoomFromText.AddExitToRoom(Utiliroom, new Vector2(2, 0), DungeonData.Direction.SOUTH, ContainsDoor: false);
            RoomFromText.AddExitToRoom(Utiliroom, new Vector2(2, 5), DungeonData.Direction.NORTH, ContainsDoor: false);
            RoomFromText.AddExitToRoom(Utiliroom, new Vector2(5, 2), DungeonData.Direction.EAST, ContainsDoor: false);
            RoomFromText.GenerateRoomFromText(Utiliroom, "RoomCellData.Utiliroom_Layout.txt");

            Utiliroom_SpecialPit.name = "Utiliroom (Special Pit)";
            Utiliroom_SpecialPit.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Utiliroom_SpecialPit.GUID = Guid.NewGuid().ToString();
            Utiliroom_SpecialPit.PreventMirroring = false;
            Utiliroom_SpecialPit.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Utiliroom_SpecialPit.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Utiliroom_SpecialPit.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Utiliroom_SpecialPit.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Utiliroom_SpecialPit.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Utiliroom_SpecialPit.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Utiliroom_SpecialPit.pits = new List<PrototypeRoomPitEntry>();
            Utiliroom_SpecialPit.placedObjects = new List<PrototypePlacedObjectData>();
            Utiliroom_SpecialPit.placedObjectPositions = new List<Vector2>();
            Utiliroom_SpecialPit.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Utiliroom_SpecialPit.roomEvents = new List<RoomEventDefinition>(0);
            Utiliroom_SpecialPit.overriddenTilesets = 0;
            Utiliroom_SpecialPit.prerequisites = new List<DungeonPrerequisite>();
            Utiliroom_SpecialPit.InvalidInCoop = false;
            Utiliroom_SpecialPit.cullProceduralDecorationOnWeakPlatforms = false;
            Utiliroom_SpecialPit.preventAddedDecoLayering = false;
            Utiliroom_SpecialPit.precludeAllTilemapDrawing = false;
            Utiliroom_SpecialPit.drawPrecludedCeilingTiles = false;
            Utiliroom_SpecialPit.preventBorders = false;
            Utiliroom_SpecialPit.preventFacewallAO = false;
            Utiliroom_SpecialPit.usesCustomAmbientLight = false;
            Utiliroom_SpecialPit.customAmbientLight = Color.white;
            Utiliroom_SpecialPit.ForceAllowDuplicates = false;
            Utiliroom_SpecialPit.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Utiliroom_SpecialPit.IsLostWoodsRoom = false;
            Utiliroom_SpecialPit.UseCustomMusic = false;
            Utiliroom_SpecialPit.UseCustomMusicState = false;
            Utiliroom_SpecialPit.CustomMusicEvent = string.Empty;
            Utiliroom_SpecialPit.UseCustomMusicSwitch = false;
            Utiliroom_SpecialPit.CustomMusicSwitch = string.Empty;
            Utiliroom_SpecialPit.overrideRoomVisualTypeForSecretRooms = false;
            Utiliroom_SpecialPit.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Utiliroom_SpecialPit.Width = 8;
            Utiliroom_SpecialPit.Height = 8;
            Utiliroom_SpecialPit.allowFloorDecoration = false;
            Utiliroom_SpecialPit.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomFromText.AddExitToRoom(Utiliroom_SpecialPit, new Vector2(1, 4), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Utiliroom_SpecialPit, new Vector2(8, 4), DungeonData.Direction.EAST);
            RoomFromText.GenerateRoomFromText(Utiliroom_SpecialPit, "RoomCellData.Utiliroom_SpecialPit_Layout.txt");


            Utiliroom_Pitfall.name = "Utiliroom (Pitfall)";
            Utiliroom_Pitfall.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Utiliroom_Pitfall.GUID = Guid.NewGuid().ToString();
            Utiliroom_Pitfall.PreventMirroring = false;
            Utiliroom_Pitfall.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Utiliroom_Pitfall.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Utiliroom_Pitfall.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Utiliroom_Pitfall.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Utiliroom_Pitfall.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Utiliroom_Pitfall.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Utiliroom_Pitfall.pits = new List<PrototypeRoomPitEntry>();
            Utiliroom_Pitfall.placedObjects = new List<PrototypePlacedObjectData>();
            Utiliroom_Pitfall.placedObjectPositions = new List<Vector2>();
            Utiliroom_Pitfall.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Utiliroom_Pitfall.roomEvents = new List<RoomEventDefinition>(0);
            Utiliroom_Pitfall.overriddenTilesets = 0;
            Utiliroom_Pitfall.prerequisites = new List<DungeonPrerequisite>();
            Utiliroom_Pitfall.InvalidInCoop = false;
            Utiliroom_Pitfall.cullProceduralDecorationOnWeakPlatforms = false;
            Utiliroom_Pitfall.preventAddedDecoLayering = false;
            Utiliroom_Pitfall.precludeAllTilemapDrawing = false;
            Utiliroom_Pitfall.drawPrecludedCeilingTiles = false;
            Utiliroom_Pitfall.preventBorders = false;
            Utiliroom_Pitfall.preventFacewallAO = false;
            Utiliroom_Pitfall.usesCustomAmbientLight = false;
            Utiliroom_Pitfall.customAmbientLight = Color.white;
            Utiliroom_Pitfall.ForceAllowDuplicates = false;
            Utiliroom_Pitfall.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Utiliroom_Pitfall.IsLostWoodsRoom = false;
            Utiliroom_Pitfall.UseCustomMusic = false;
            Utiliroom_Pitfall.UseCustomMusicState = false;
            Utiliroom_Pitfall.CustomMusicEvent = string.Empty;
            Utiliroom_Pitfall.UseCustomMusicSwitch = false;
            Utiliroom_Pitfall.CustomMusicSwitch = string.Empty;
            Utiliroom_Pitfall.overrideRoomVisualTypeForSecretRooms = false;
            Utiliroom_Pitfall.rewardChestSpawnPosition = IntVector2.One;
            Utiliroom_Pitfall.Width = 8;
            Utiliroom_Pitfall.Height = 8;
            RoomFromText.AddExitToRoom(Utiliroom_Pitfall, new Vector2(0, 4), DungeonData.Direction.WEST, ContainsDoor: false);
            RoomFromText.AddExitToRoom(Utiliroom_Pitfall, new Vector2(4, 0), DungeonData.Direction.SOUTH, ContainsDoor: false);
            RoomFromText.AddExitToRoom(Utiliroom_Pitfall, new Vector2(4, 9), DungeonData.Direction.NORTH, ContainsDoor: false);
            RoomFromText.AddExitToRoom(Utiliroom_Pitfall, new Vector2(9, 4), DungeonData.Direction.EAST, ContainsDoor: false);
            RoomFromText.GenerateRoomFromText(Utiliroom_Pitfall, "RoomCellData.Utiliroom_Pitfall_Layout.txt");


            SpecialWallMimicRoom.name = "Special WallMimic Room";
            SpecialWallMimicRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            SpecialWallMimicRoom.GUID = Guid.NewGuid().ToString();
            SpecialWallMimicRoom.PreventMirroring = false;
            SpecialWallMimicRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            SpecialWallMimicRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            SpecialWallMimicRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            SpecialWallMimicRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            SpecialWallMimicRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            SpecialWallMimicRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            SpecialWallMimicRoom.pits = new List<PrototypeRoomPitEntry>();
            SpecialWallMimicRoom.placedObjects = new List<PrototypePlacedObjectData>();
            SpecialWallMimicRoom.placedObjectPositions = new List<Vector2>();
            SpecialWallMimicRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            SpecialWallMimicRoom.roomEvents = new List<RoomEventDefinition>(0);
            SpecialWallMimicRoom.overriddenTilesets = 0;
            SpecialWallMimicRoom.prerequisites = new List<DungeonPrerequisite>();
            SpecialWallMimicRoom.InvalidInCoop = false;
            SpecialWallMimicRoom.cullProceduralDecorationOnWeakPlatforms = false;
            SpecialWallMimicRoom.preventAddedDecoLayering = false;
            SpecialWallMimicRoom.precludeAllTilemapDrawing = false;
            SpecialWallMimicRoom.drawPrecludedCeilingTiles = false;
            SpecialWallMimicRoom.preventBorders = false;
            SpecialWallMimicRoom.preventFacewallAO = false;
            SpecialWallMimicRoom.usesCustomAmbientLight = false;
            SpecialWallMimicRoom.customAmbientLight = Color.white;
            SpecialWallMimicRoom.ForceAllowDuplicates = false;
            SpecialWallMimicRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            SpecialWallMimicRoom.IsLostWoodsRoom = false;
            SpecialWallMimicRoom.UseCustomMusic = false;
            SpecialWallMimicRoom.UseCustomMusicState = false;
            SpecialWallMimicRoom.CustomMusicEvent = string.Empty;
            SpecialWallMimicRoom.UseCustomMusicSwitch = false;
            SpecialWallMimicRoom.CustomMusicSwitch = string.Empty;
            SpecialWallMimicRoom.overrideRoomVisualTypeForSecretRooms = false;
            SpecialWallMimicRoom.rewardChestSpawnPosition = new IntVector2(16, 9);
            SpecialWallMimicRoom.Width = 20;
            SpecialWallMimicRoom.Height = 20;
            SpecialWallMimicRoom.overrideRoomVisualType = 3;
            RoomFromText.AddExitToRoom(SpecialWallMimicRoom, new Vector2(0, 10), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(SpecialWallMimicRoom, new Vector2(10, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(SpecialWallMimicRoom, new Vector2(10, 21), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(SpecialWallMimicRoom, new Vector2(21, 10), DungeonData.Direction.EAST);
            RoomFromText.GenerateRoomFromText(SpecialWallMimicRoom, "RoomCellData.SpecialWallMimicRoom_Layout.txt");
            RoomFromText.AddObjectToRoom(SpecialWallMimicRoom, new Vector2(9, 6), EnemyBehaviourGuid: "479556d05c7c44f3b6abb3b2067fc778"); // Wall_Mimic
            RoomFromText.AddObjectToRoom(SpecialWallMimicRoom, new Vector2(9, 13), EnemyBehaviourGuid: "479556d05c7c44f3b6abb3b2067fc778"); // Wall_Mimic


            SpecialMaintenanceRoom.name = "Special Maintenance Room";
            SpecialMaintenanceRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            SpecialMaintenanceRoom.GUID = Guid.NewGuid().ToString();
            SpecialMaintenanceRoom.PreventMirroring = false;
            SpecialMaintenanceRoom.category = PrototypeDungeonRoom.RoomCategory.HUB;
            SpecialMaintenanceRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            SpecialMaintenanceRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            SpecialMaintenanceRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            SpecialMaintenanceRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            SpecialMaintenanceRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            SpecialMaintenanceRoom.pits = new List<PrototypeRoomPitEntry>();
            SpecialMaintenanceRoom.placedObjects = new List<PrototypePlacedObjectData>();
            SpecialMaintenanceRoom.placedObjectPositions = new List<Vector2>();
            SpecialMaintenanceRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            SpecialMaintenanceRoom.roomEvents = new List<RoomEventDefinition>(0);
            SpecialMaintenanceRoom.overriddenTilesets = 0;
            SpecialMaintenanceRoom.prerequisites = new List<DungeonPrerequisite>();
            SpecialMaintenanceRoom.InvalidInCoop = false;
            SpecialMaintenanceRoom.cullProceduralDecorationOnWeakPlatforms = false;
            SpecialMaintenanceRoom.preventAddedDecoLayering = false;
            SpecialMaintenanceRoom.precludeAllTilemapDrawing = false;
            SpecialMaintenanceRoom.drawPrecludedCeilingTiles = false;
            SpecialMaintenanceRoom.preventBorders = false;
            SpecialMaintenanceRoom.preventFacewallAO = false;
            SpecialMaintenanceRoom.usesCustomAmbientLight = false;
            SpecialMaintenanceRoom.customAmbientLight = Color.white;
            SpecialMaintenanceRoom.ForceAllowDuplicates = false;
            SpecialMaintenanceRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            SpecialMaintenanceRoom.IsLostWoodsRoom = false;
            SpecialMaintenanceRoom.UseCustomMusic = false;
            SpecialMaintenanceRoom.UseCustomMusicState = false;
            SpecialMaintenanceRoom.CustomMusicEvent = string.Empty;
            SpecialMaintenanceRoom.UseCustomMusicSwitch = false;
            SpecialMaintenanceRoom.CustomMusicSwitch = string.Empty;
            SpecialMaintenanceRoom.overrideRoomVisualTypeForSecretRooms = false;
            SpecialMaintenanceRoom.rewardChestSpawnPosition = new IntVector2(10, 10);
            SpecialMaintenanceRoom.associatedMinimapIcon = ExpandPrefabs.elevator_maintenance_room.associatedMinimapIcon;
            SpecialMaintenanceRoom.Width = 30;
            SpecialMaintenanceRoom.Height = 30;
            // SpecialMaintenanceRoom.usesProceduralDecoration = false;
            SpecialMaintenanceRoom.allowFloorDecoration = false;
            SpecialMaintenanceRoom.overrideRoomVisualType = 1;
            RoomFromText.AddExitToRoom(SpecialMaintenanceRoom, new Vector2(0, 16), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(SpecialMaintenanceRoom, new Vector2(31, 16), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(SpecialMaintenanceRoom, new Vector2(15, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(SpecialMaintenanceRoom, new Vector2(15, 31), DungeonData.Direction.NORTH);
            RoomFromText.GenerateRoomFromText(SpecialMaintenanceRoom, "RoomCellData.SpecialMaintenanceRoom_Layout.txt");
            RoomFromText.AddObjectToRoom(SpecialMaintenanceRoom, new Vector2(8, 9), NonEnemyBehaviour: ExpandPrefabs.elevator_maintenance_room.placedObjects[0].nonenemyBehaviour);
            RoomFromText.AddObjectToRoom(SpecialMaintenanceRoom, new Vector2(18, 18), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Arrival, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(SpecialMaintenanceRoom, new Vector2(14, 5), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Teleporter_Info_Sign, useExternalPrefab: true));


            ShopBackRoom.name = "Shop Back Room";
            ShopBackRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            ShopBackRoom.GUID = Guid.NewGuid().ToString();
            ShopBackRoom.PreventMirroring = false;
            ShopBackRoom.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            ShopBackRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            ShopBackRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            ShopBackRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            ShopBackRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            ShopBackRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            ShopBackRoom.pits = new List<PrototypeRoomPitEntry>();
            ShopBackRoom.placedObjects = new List<PrototypePlacedObjectData>();
            ShopBackRoom.placedObjectPositions = new List<Vector2>();
            ShopBackRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            ShopBackRoom.roomEvents = new List<RoomEventDefinition>(0);
            ShopBackRoom.overriddenTilesets = 0;
            ShopBackRoom.prerequisites = new List<DungeonPrerequisite>();
            ShopBackRoom.InvalidInCoop = false;
            ShopBackRoom.cullProceduralDecorationOnWeakPlatforms = false;
            ShopBackRoom.preventAddedDecoLayering = false;
            ShopBackRoom.precludeAllTilemapDrawing = false;
            ShopBackRoom.drawPrecludedCeilingTiles = false;
            ShopBackRoom.preventBorders = false;
            ShopBackRoom.preventFacewallAO = false;
            ShopBackRoom.usesCustomAmbientLight = false;
            ShopBackRoom.customAmbientLight = Color.white;
            ShopBackRoom.ForceAllowDuplicates = false;
            ShopBackRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            ShopBackRoom.IsLostWoodsRoom = false;
            ShopBackRoom.UseCustomMusic = false;
            ShopBackRoom.UseCustomMusicState = false;
            ShopBackRoom.CustomMusicEvent = string.Empty;
            ShopBackRoom.UseCustomMusicSwitch = false;
            ShopBackRoom.CustomMusicSwitch = string.Empty;
            ShopBackRoom.overrideRoomVisualTypeForSecretRooms = false;
            ShopBackRoom.rewardChestSpawnPosition = new IntVector2(9, 2);
            ShopBackRoom.Width = 18;
            ShopBackRoom.Height = 34;
            ShopBackRoom.overrideRoomVisualType = 1;
            ShopBackRoom.associatedMinimapIcon = ExpandPrefabs.basic_special_rooms.includedRooms.elements[1].room.associatedMinimapIcon;
            RoomFromText.AddExitToRoom(ShopBackRoom, new Vector2(0, 2), DungeonData.Direction.WEST, PrototypeRoomExit.ExitType.ENTRANCE_ONLY);
            RoomFromText.AddExitToRoom(ShopBackRoom, new Vector2(9, 0), DungeonData.Direction.SOUTH, PrototypeRoomExit.ExitType.ENTRANCE_ONLY);
            RoomFromText.AddExitToRoom(ShopBackRoom, new Vector2(19, 2), DungeonData.Direction.EAST, PrototypeRoomExit.ExitType.ENTRANCE_ONLY);
            RoomFromText.AddExitToRoom(ShopBackRoom, new Vector2(14, 35), DungeonData.Direction.NORTH, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomFromText.GenerateRoomFromText(ShopBackRoom, "RoomCellData.ShopBackRoom_Layout.txt");
            RoomFromText.AddObjectToRoom(ShopBackRoom, new Vector2(3, 5), NonEnemyBehaviour: ExpandPrefabs.RatJailDoorPlacable);
            RoomFromText.AddObjectToRoom(ShopBackRoom, new Vector2(13, 4), NonEnemyBehaviour: ExpandPrefabs.RatJailDoorPlacable);
            RoomFromText.AddObjectToRoom(ShopBackRoom, new Vector2(13, 6), NonEnemyBehaviour: ExpandPrefabs.RatJailDoorPlacable);
            RoomFromText.AddObjectToRoom(ShopBackRoom, new Vector2(13, 32), NonEnemyBehaviour: ExpandPrefabs.RatJailDoorPlacable);

            SecretRewardRoom.name = "Secret Reward Room";
            SecretRewardRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            SecretRewardRoom.GUID = Guid.NewGuid().ToString();
            SecretRewardRoom.PreventMirroring = false;
            SecretRewardRoom.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            SecretRewardRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            SecretRewardRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            SecretRewardRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            SecretRewardRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            SecretRewardRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            SecretRewardRoom.pits = new List<PrototypeRoomPitEntry>();
            SecretRewardRoom.placedObjects = new List<PrototypePlacedObjectData>();
            SecretRewardRoom.placedObjectPositions = new List<Vector2>();
            SecretRewardRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            SecretRewardRoom.roomEvents = new List<RoomEventDefinition>(0);
            SecretRewardRoom.overriddenTilesets = 0;
            SecretRewardRoom.prerequisites = new List<DungeonPrerequisite>();
            SecretRewardRoom.InvalidInCoop = false;
            SecretRewardRoom.cullProceduralDecorationOnWeakPlatforms = false;
            SecretRewardRoom.preventAddedDecoLayering = false;
            SecretRewardRoom.precludeAllTilemapDrawing = false;
            SecretRewardRoom.drawPrecludedCeilingTiles = false;
            SecretRewardRoom.preventBorders = false;
            SecretRewardRoom.preventFacewallAO = false;
            SecretRewardRoom.usesCustomAmbientLight = false;
            SecretRewardRoom.customAmbientLight = Color.white;
            SecretRewardRoom.ForceAllowDuplicates = false;
            SecretRewardRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            SecretRewardRoom.IsLostWoodsRoom = false;
            SecretRewardRoom.UseCustomMusic = false;
            SecretRewardRoom.UseCustomMusicState = false;
            SecretRewardRoom.CustomMusicEvent = string.Empty;
            SecretRewardRoom.UseCustomMusicSwitch = false;
            SecretRewardRoom.CustomMusicSwitch = string.Empty;
            SecretRewardRoom.overrideRoomVisualTypeForSecretRooms = false;
            SecretRewardRoom.rewardChestSpawnPosition = new IntVector2(8, 2);
            SecretRewardRoom.Width = 20;
            SecretRewardRoom.Height = 64;
            // SecretRewardRoom.usesProceduralDecoration = false;
            SecretRewardRoom.overrideRoomVisualType = 3;
            RoomFromText.AddExitToRoom(SecretRewardRoom, new Vector2(0, 2), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(SecretRewardRoom, new Vector2(8, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(SecretRewardRoom, new Vector2(21, 2), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(SecretRewardRoom, new Vector2(0, 31), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(SecretRewardRoom, new Vector2(21, 31), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(SecretRewardRoom, new Vector2(0, 60), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(SecretRewardRoom, new Vector2(21, 60), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(SecretRewardRoom, new Vector2(8, 65), DungeonData.Direction.NORTH);
            RoomFromText.GenerateRoomFromText(SecretRewardRoom, "RoomCellData.SecretRewardRoom_Layout.txt");
            RoomFromText.AddObjectToRoom(SecretRewardRoom, new Vector2(8, 0), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Teleporter_Gungeon_01, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 5), NonEnemyBehaviour: ExpandPrefabs.RatJailDoorPlacable);
            RoomFromText.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 7), NonEnemyBehaviour: ExpandPrefabs.RatJailDoorPlacable);
            RoomFromText.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 9), NonEnemyBehaviour: ExpandPrefabs.RatJailDoorPlacable);
            RoomFromText.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 11), NonEnemyBehaviour: ExpandPrefabs.RatJailDoorPlacable);
            RoomFromText.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 21), NonEnemyBehaviour: ExpandPrefabs.RatJailDoorPlacable);
            RoomFromText.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 26), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 34), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 46), NonEnemyBehaviour: ExpandPrefabs.RatJailDoorPlacable);
            RoomFromText.AddObjectToRoom(SecretRewardRoom, new Vector2(6, 4), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Teleporter_Info_Sign, useExternalPrefab: true));


            SecretBossRoom.name = "Secret Boss Room";
            SecretBossRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            SecretBossRoom.GUID = Guid.NewGuid().ToString();
            SecretBossRoom.PreventMirroring = false;
            SecretBossRoom.category = PrototypeDungeonRoom.RoomCategory.BOSS;
            SecretBossRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.MINI_BOSS;
            SecretBossRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            SecretBossRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            SecretBossRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            SecretBossRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            SecretBossRoom.pits = new List<PrototypeRoomPitEntry>();
            SecretBossRoom.placedObjects = new List<PrototypePlacedObjectData>();
            SecretBossRoom.placedObjectPositions = new List<Vector2>();
            SecretBossRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            SecretBossRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            SecretBossRoom.overriddenTilesets = 0;
            SecretBossRoom.prerequisites = new List<DungeonPrerequisite>();
            SecretBossRoom.InvalidInCoop = false;
            SecretBossRoom.cullProceduralDecorationOnWeakPlatforms = false;
            SecretBossRoom.preventAddedDecoLayering = false;
            SecretBossRoom.precludeAllTilemapDrawing = false;
            SecretBossRoom.drawPrecludedCeilingTiles = false;
            SecretBossRoom.preventBorders = false;
            SecretBossRoom.preventFacewallAO = false;
            SecretBossRoom.usesCustomAmbientLight = false;
            SecretBossRoom.customAmbientLight = Color.white;
            SecretBossRoom.ForceAllowDuplicates = false;
            SecretBossRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            SecretBossRoom.IsLostWoodsRoom = false;
            SecretBossRoom.UseCustomMusic = false;
            SecretBossRoom.UseCustomMusicState = false;
            SecretBossRoom.CustomMusicEvent = string.Empty;
            SecretBossRoom.UseCustomMusicSwitch = false;
            SecretBossRoom.CustomMusicSwitch = string.Empty;
            SecretBossRoom.overrideRoomVisualTypeForSecretRooms = false;
            SecretBossRoom.rewardChestSpawnPosition = new IntVector2(18, 20);
            SecretBossRoom.Width = 36;
            SecretBossRoom.Height = 30;
            SecretBossRoom.overrideRoomVisualType = 3;
            SecretBossRoom.associatedMinimapIcon = ExpandPrefabs.DraGunRoom01.associatedMinimapIcon;
            RoomFromText.AddExitToRoom(SecretBossRoom, new Vector2(17, 0), DungeonData.Direction.SOUTH, PrototypeRoomExit.ExitType.ENTRANCE_ONLY, exitSize: 4);
            RoomFromText.AddExitToRoom(SecretBossRoom, new Vector2(18, 31), DungeonData.Direction.NORTH, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomFromText.GenerateDefaultRoomLayout(SecretBossRoom);


            SecretExitRoom.name = "Secret Exit Room";
            SecretExitRoom.QAID = "AA" + UnityEngine.Random.Range(1000, 9999);
            SecretExitRoom.GUID = Guid.NewGuid().ToString();
            SecretExitRoom.PreventMirroring = false;
            SecretExitRoom.category = PrototypeDungeonRoom.RoomCategory.EXIT;
            SecretExitRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            SecretExitRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            SecretExitRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            SecretExitRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            SecretExitRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            SecretExitRoom.pits = new List<PrototypeRoomPitEntry>();
            SecretExitRoom.placedObjects = new List<PrototypePlacedObjectData>();
            SecretExitRoom.placedObjectPositions = new List<Vector2>();
            SecretExitRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            SecretExitRoom.roomEvents = new List<RoomEventDefinition>(0);
            SecretExitRoom.overriddenTilesets = 0;
            SecretExitRoom.prerequisites = new List<DungeonPrerequisite>();
            SecretExitRoom.InvalidInCoop = false;
            SecretExitRoom.cullProceduralDecorationOnWeakPlatforms = false;
            SecretExitRoom.preventAddedDecoLayering = false;
            SecretExitRoom.precludeAllTilemapDrawing = false;
            SecretExitRoom.drawPrecludedCeilingTiles = false;
            SecretExitRoom.preventBorders = false;
            SecretExitRoom.preventFacewallAO = false;
            SecretExitRoom.usesCustomAmbientLight = false;
            SecretExitRoom.customAmbientLight = Color.white;
            SecretExitRoom.ForceAllowDuplicates = false;
            SecretExitRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            SecretExitRoom.IsLostWoodsRoom = false;
            SecretExitRoom.UseCustomMusic = false;
            SecretExitRoom.UseCustomMusicState = false;
            SecretExitRoom.CustomMusicEvent = string.Empty;
            SecretExitRoom.UseCustomMusicSwitch = false;
            SecretExitRoom.CustomMusicSwitch = string.Empty;
            SecretExitRoom.overrideRoomVisualTypeForSecretRooms = false;
            SecretExitRoom.rewardChestSpawnPosition = IntVector2.One;
            SecretExitRoom.Width = 8;
            SecretExitRoom.Height = 12;
            SecretExitRoom.associatedMinimapIcon = ExpandPrefabs.exit_room_basic.associatedMinimapIcon;
            // SecretExitRoom.usesProceduralDecoration = false;
            // SecretExitRoom.usesProceduralLighting = false;
            // SecretExitRoom.allowFloorDecoration = false;            
            SecretExitRoom.usesProceduralDecoration = true;
            SecretExitRoom.usesProceduralLighting = true;
            RoomFromText.AddExitToRoom(SecretExitRoom, new Vector2(0, 2), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(SecretExitRoom, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(SecretExitRoom, new Vector2(9, 2), DungeonData.Direction.EAST);
            RoomFromText.GenerateRoomFromText(SecretExitRoom, "RoomCellData.SecretExitRoom_Layout.txt");
            RoomFromText.AddObjectToRoom(SecretExitRoom, new Vector2(1, 6), ExpandPrefabs.ElevatorDeparture);
            RoomFromText.AddObjectToRoom(SecretExitRoom, new Vector2(2, 0), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Teleporter_Gungeon_01, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(SecretExitRoom, new Vector2(3, 1), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Arrival, useExternalPrefab: true));



            ThwompCrossingVertical.name = "Thwomp_Crossing_Vertical";
            ThwompCrossingVertical.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            ThwompCrossingVertical.GUID = Guid.NewGuid().ToString();
            ThwompCrossingVertical.PreventMirroring = false;
            ThwompCrossingVertical.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            ThwompCrossingVertical.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            ThwompCrossingVertical.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            ThwompCrossingVertical.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            ThwompCrossingVertical.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            ThwompCrossingVertical.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            ThwompCrossingVertical.pits = new List<PrototypeRoomPitEntry>();
            ThwompCrossingVertical.placedObjects = new List<PrototypePlacedObjectData>();
            ThwompCrossingVertical.placedObjectPositions = new List<Vector2>();
            ThwompCrossingVertical.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            ThwompCrossingVertical.roomEvents = new List<RoomEventDefinition>(0);
            ThwompCrossingVertical.overriddenTilesets = 0;
            ThwompCrossingVertical.prerequisites = new List<DungeonPrerequisite>();
            ThwompCrossingVertical.InvalidInCoop = false;
            ThwompCrossingVertical.cullProceduralDecorationOnWeakPlatforms = false;
            ThwompCrossingVertical.preventAddedDecoLayering = false;
            ThwompCrossingVertical.precludeAllTilemapDrawing = false;
            ThwompCrossingVertical.drawPrecludedCeilingTiles = false;
            ThwompCrossingVertical.preventBorders = false;
            ThwompCrossingVertical.preventFacewallAO = false;
            ThwompCrossingVertical.usesCustomAmbientLight = false;
            ThwompCrossingVertical.customAmbientLight = Color.white;
            ThwompCrossingVertical.ForceAllowDuplicates = false;
            ThwompCrossingVertical.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            ThwompCrossingVertical.IsLostWoodsRoom = false;
            ThwompCrossingVertical.UseCustomMusic = false;
            ThwompCrossingVertical.UseCustomMusicState = false;
            ThwompCrossingVertical.CustomMusicEvent = string.Empty;
            ThwompCrossingVertical.UseCustomMusicSwitch = false;
            ThwompCrossingVertical.CustomMusicSwitch = string.Empty;
            ThwompCrossingVertical.overrideRoomVisualTypeForSecretRooms = false;
            ThwompCrossingVertical.rewardChestSpawnPosition = new IntVector2(7, 7);
            ThwompCrossingVertical.Width = 14;
            ThwompCrossingVertical.Height = 30;
            RoomFromText.AddExitToRoom(ThwompCrossingVertical, new Vector2(7, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(ThwompCrossingVertical, new Vector2(7, 31), DungeonData.Direction.NORTH);
            /*RoomFromText.AddExitToRoom(ThwompCrossingVertical, new Vector2(0, 2), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(ThwompCrossingVertical, new Vector2(0, 26), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(ThwompCrossingVertical, new Vector2(15, 2), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(ThwompCrossingVertical, new Vector2(15, 26), DungeonData.Direction.EAST);*/
            RoomFromText.GenerateRoomFromText(ThwompCrossingVertical, "RoomCellData.ThwompCrossingVertical_Layout.txt");
            RoomFromText.AddObjectToRoom(ThwompCrossingVertical, new Vector2(11, 7), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomFromText.AddObjectToRoom(ThwompCrossingVertical, new Vector2(11, 16), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomFromText.AddObjectToRoom(ThwompCrossingVertical, new Vector2(11, 11), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomFromText.AddObjectToRoom(ThwompCrossingVertical, new Vector2(11, 22), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)

            ThwompCrossingVerticalNoRain.name = "Thwomp_Crossing_Vertical(NoRain)";
            ThwompCrossingVerticalNoRain.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            ThwompCrossingVerticalNoRain.GUID = Guid.NewGuid().ToString();
            ThwompCrossingVerticalNoRain.PreventMirroring = false;
            ThwompCrossingVerticalNoRain.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            ThwompCrossingVerticalNoRain.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            ThwompCrossingVerticalNoRain.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            ThwompCrossingVerticalNoRain.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            ThwompCrossingVerticalNoRain.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            ThwompCrossingVerticalNoRain.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            ThwompCrossingVerticalNoRain.pits = new List<PrototypeRoomPitEntry>();
            ThwompCrossingVerticalNoRain.placedObjects = new List<PrototypePlacedObjectData>();
            ThwompCrossingVerticalNoRain.placedObjectPositions = new List<Vector2>();
            ThwompCrossingVerticalNoRain.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            ThwompCrossingVerticalNoRain.roomEvents = new List<RoomEventDefinition>(0);
            ThwompCrossingVerticalNoRain.overriddenTilesets = 0;
            ThwompCrossingVerticalNoRain.prerequisites = new List<DungeonPrerequisite>();
            ThwompCrossingVerticalNoRain.InvalidInCoop = false;
            ThwompCrossingVerticalNoRain.cullProceduralDecorationOnWeakPlatforms = false;
            ThwompCrossingVerticalNoRain.preventAddedDecoLayering = false;
            ThwompCrossingVerticalNoRain.precludeAllTilemapDrawing = false;
            ThwompCrossingVerticalNoRain.drawPrecludedCeilingTiles = false;
            ThwompCrossingVerticalNoRain.preventBorders = false;
            ThwompCrossingVerticalNoRain.preventFacewallAO = false;
            ThwompCrossingVerticalNoRain.usesCustomAmbientLight = false;
            ThwompCrossingVerticalNoRain.customAmbientLight = Color.white;
            ThwompCrossingVerticalNoRain.ForceAllowDuplicates = false;
            ThwompCrossingVerticalNoRain.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            ThwompCrossingVerticalNoRain.IsLostWoodsRoom = false;
            ThwompCrossingVerticalNoRain.UseCustomMusic = false;
            ThwompCrossingVerticalNoRain.UseCustomMusicState = false;
            ThwompCrossingVerticalNoRain.CustomMusicEvent = string.Empty;
            ThwompCrossingVerticalNoRain.UseCustomMusicSwitch = false;
            ThwompCrossingVerticalNoRain.CustomMusicSwitch = string.Empty;
            ThwompCrossingVerticalNoRain.overrideRoomVisualTypeForSecretRooms = false;
            ThwompCrossingVerticalNoRain.rewardChestSpawnPosition = new IntVector2(7, 7);
            ThwompCrossingVerticalNoRain.Width = 14;
            ThwompCrossingVerticalNoRain.Height = 30;
            ThwompCrossingVerticalNoRain.overrideRoomVisualType = 3;
            RoomFromText.AddExitToRoom(ThwompCrossingVerticalNoRain, new Vector2(7, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(ThwompCrossingVerticalNoRain, new Vector2(7, 31), DungeonData.Direction.NORTH);
            RoomFromText.GenerateRoomFromText(ThwompCrossingVerticalNoRain, "RoomCellData.ThwompCrossingVertical_Layout.txt");
            RoomFromText.AddObjectToRoom(ThwompCrossingVerticalNoRain, new Vector2(11, 7), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomFromText.AddObjectToRoom(ThwompCrossingVerticalNoRain, new Vector2(11, 11), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomFromText.AddObjectToRoom(ThwompCrossingVerticalNoRain, new Vector2(11, 16), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomFromText.AddObjectToRoom(ThwompCrossingVerticalNoRain, new Vector2(11, 22), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)


            ThwompCrossingHorizontal.name = "Thwomp_Crossing_Horizontal";
            ThwompCrossingHorizontal.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            ThwompCrossingHorizontal.GUID = Guid.NewGuid().ToString();
            ThwompCrossingHorizontal.PreventMirroring = false;
            ThwompCrossingHorizontal.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            ThwompCrossingHorizontal.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            ThwompCrossingHorizontal.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            ThwompCrossingHorizontal.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            ThwompCrossingHorizontal.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            ThwompCrossingHorizontal.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            ThwompCrossingHorizontal.pits = new List<PrototypeRoomPitEntry>();
            ThwompCrossingHorizontal.placedObjects = new List<PrototypePlacedObjectData>();
            ThwompCrossingHorizontal.placedObjectPositions = new List<Vector2>();
            ThwompCrossingHorizontal.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            ThwompCrossingHorizontal.roomEvents = new List<RoomEventDefinition>(0);
            ThwompCrossingHorizontal.overriddenTilesets = 0;
            ThwompCrossingHorizontal.prerequisites = new List<DungeonPrerequisite>();
            ThwompCrossingHorizontal.InvalidInCoop = false;
            ThwompCrossingHorizontal.cullProceduralDecorationOnWeakPlatforms = false;
            ThwompCrossingHorizontal.preventAddedDecoLayering = false;
            ThwompCrossingHorizontal.precludeAllTilemapDrawing = false;
            ThwompCrossingHorizontal.drawPrecludedCeilingTiles = false;
            ThwompCrossingHorizontal.preventBorders = false;
            ThwompCrossingHorizontal.preventFacewallAO = false;
            ThwompCrossingHorizontal.usesCustomAmbientLight = false;
            ThwompCrossingHorizontal.customAmbientLight = Color.white;
            ThwompCrossingHorizontal.ForceAllowDuplicates = false;
            ThwompCrossingHorizontal.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            ThwompCrossingHorizontal.IsLostWoodsRoom = false;
            ThwompCrossingHorizontal.UseCustomMusic = false;
            ThwompCrossingHorizontal.UseCustomMusicState = false;
            ThwompCrossingHorizontal.CustomMusicEvent = string.Empty;
            ThwompCrossingHorizontal.UseCustomMusicSwitch = false;
            ThwompCrossingHorizontal.CustomMusicSwitch = string.Empty;
            ThwompCrossingHorizontal.overrideRoomVisualTypeForSecretRooms = false;
            ThwompCrossingHorizontal.rewardChestSpawnPosition = new IntVector2(1, 12);
            ThwompCrossingHorizontal.Width = 30;
            ThwompCrossingHorizontal.Height = 14;
            RoomFromText.AddExitToRoom(ThwompCrossingHorizontal, new Vector2(0, 7), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(ThwompCrossingHorizontal, new Vector2(31, 7), DungeonData.Direction.EAST);
            RoomFromText.GenerateRoomFromText(ThwompCrossingHorizontal, "RoomCellData.ThwompCrossingHorizontal_Layout.txt");
            RoomFromText.AddObjectToRoom(ThwompCrossingHorizontal, new Vector2(7, 12), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomFromText.AddObjectToRoom(ThwompCrossingHorizontal, new Vector2(11, 12), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomFromText.AddObjectToRoom(ThwompCrossingHorizontal, new Vector2(16, 12), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomFromText.AddObjectToRoom(ThwompCrossingHorizontal, new Vector2(21, 12), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)


            PuzzleRoom3.name = "Zelda Puzzle Room 3";
            PuzzleRoom3.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            PuzzleRoom3.GUID = Guid.NewGuid().ToString();
            PuzzleRoom3.PreventMirroring = false;
            PuzzleRoom3.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            PuzzleRoom3.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            PuzzleRoom3.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            PuzzleRoom3.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            PuzzleRoom3.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            PuzzleRoom3.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            PuzzleRoom3.pits = ExpandPrefabs.gungeon_gauntlet_001.pits;
            PuzzleRoom3.placedObjects = ExpandPrefabs.gungeon_gauntlet_001.placedObjects;
            PuzzleRoom3.placedObjectPositions = ExpandPrefabs.gungeon_gauntlet_001.placedObjectPositions;
            PuzzleRoom3.additionalObjectLayers = ExpandPrefabs.gungeon_gauntlet_001.additionalObjectLayers;
            PuzzleRoom3.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            PuzzleRoom3.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            PuzzleRoom3.overriddenTilesets = 0;
            PuzzleRoom3.prerequisites = new List<DungeonPrerequisite>();
            PuzzleRoom3.InvalidInCoop = false;
            PuzzleRoom3.cullProceduralDecorationOnWeakPlatforms = false;
            PuzzleRoom3.preventAddedDecoLayering = false;
            PuzzleRoom3.precludeAllTilemapDrawing = false;
            PuzzleRoom3.drawPrecludedCeilingTiles = false;
            PuzzleRoom3.preventBorders = false;
            PuzzleRoom3.preventFacewallAO = false;
            PuzzleRoom3.usesCustomAmbientLight = false;
            PuzzleRoom3.customAmbientLight = Color.white;
            PuzzleRoom3.ForceAllowDuplicates = false;
            PuzzleRoom3.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            PuzzleRoom3.IsLostWoodsRoom = false;
            PuzzleRoom3.UseCustomMusic = false;
            PuzzleRoom3.UseCustomMusicState = false;
            PuzzleRoom3.CustomMusicEvent = string.Empty;
            PuzzleRoom3.UseCustomMusicSwitch = false;
            PuzzleRoom3.CustomMusicSwitch = string.Empty;
            PuzzleRoom3.overrideRoomVisualTypeForSecretRooms = false;
            PuzzleRoom3.rewardChestSpawnPosition = new IntVector2(-1, -1);
            PuzzleRoom3.Width = 38;
            PuzzleRoom3.Height = 22;
            PuzzleRoom3.overrideRoomVisualType = 3;
            RoomFromText.AddExitToRoom(PuzzleRoom3, new Vector2(0, 10), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(PuzzleRoom3, new Vector2(39, 10), DungeonData.Direction.EAST);
            RoomFromText.GenerateRoomFromText(PuzzleRoom3, "RoomCellData.PuzzleRoom3_Layout.txt");


            CreepyGlitchRoom.name = "Creepy Glitched Room";
            CreepyGlitchRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            CreepyGlitchRoom.GUID = Guid.NewGuid().ToString();
            CreepyGlitchRoom.PreventMirroring = false;
            CreepyGlitchRoom.category = PrototypeDungeonRoom.RoomCategory.BOSS;
            CreepyGlitchRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            CreepyGlitchRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            CreepyGlitchRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            CreepyGlitchRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            CreepyGlitchRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            CreepyGlitchRoom.pits = new List<PrototypeRoomPitEntry>();
            CreepyGlitchRoom.placedObjects = new List<PrototypePlacedObjectData>();
            CreepyGlitchRoom.placedObjectPositions = new List<Vector2>();
            CreepyGlitchRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            CreepyGlitchRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            CreepyGlitchRoom.overriddenTilesets = 0;
            CreepyGlitchRoom.prerequisites = new List<DungeonPrerequisite>();
            CreepyGlitchRoom.InvalidInCoop = false;
            CreepyGlitchRoom.cullProceduralDecorationOnWeakPlatforms = false;
            CreepyGlitchRoom.preventAddedDecoLayering = false;
            CreepyGlitchRoom.precludeAllTilemapDrawing = false;
            CreepyGlitchRoom.drawPrecludedCeilingTiles = false;
            CreepyGlitchRoom.preventBorders = false;
            CreepyGlitchRoom.preventFacewallAO = false;
            CreepyGlitchRoom.usesCustomAmbientLight = false;
            CreepyGlitchRoom.customAmbientLight = Color.white;
            CreepyGlitchRoom.ForceAllowDuplicates = false;
            CreepyGlitchRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            CreepyGlitchRoom.IsLostWoodsRoom = false;
            CreepyGlitchRoom.UseCustomMusic = false;
            CreepyGlitchRoom.UseCustomMusicState = false;
            CreepyGlitchRoom.CustomMusicEvent = string.Empty;
            CreepyGlitchRoom.UseCustomMusicSwitch = false;
            CreepyGlitchRoom.CustomMusicSwitch = string.Empty;
            CreepyGlitchRoom.overrideRoomVisualTypeForSecretRooms = false;
            CreepyGlitchRoom.rewardChestSpawnPosition = new IntVector2(12, 19);
            CreepyGlitchRoom.Width = 26;
            CreepyGlitchRoom.Height = 26;
            CreepyGlitchRoom.associatedMinimapIcon = ExpandPrefabs.doublebeholsterroom01.associatedMinimapIcon;
            RoomFromText.AddExitToRoom(CreepyGlitchRoom, new Vector2(0, 13), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(CreepyGlitchRoom, new Vector2(27, 13), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(CreepyGlitchRoom, new Vector2(13, 1), DungeonData.Direction.SOUTH);
            RoomFromText.AddObjectToRoom(CreepyGlitchRoom, new Vector2(12, 23), NonEnemyBehaviour: ExpandPrefabs.EXPlayerMimicBoss.GetComponent<ExpandGungeoneerMimicBossPlacable>());
            // RoomFromText.AddObjectToRoom(CreepyGlitchRoom, new Vector2(13, 13), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RoomCorruptionAmbience, useExternalPrefab: true));
            RoomFromText.GenerateRoomFromText(CreepyGlitchRoom, "RoomCellData.CreepyGlitchRoom_Layout.txt");

            CreepyGlitchRoom_Entrance.name = "Creepy Glitched Room Entrance";
            CreepyGlitchRoom_Entrance.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            CreepyGlitchRoom_Entrance.GUID = Guid.NewGuid().ToString();
            CreepyGlitchRoom_Entrance.PreventMirroring = false;
            CreepyGlitchRoom_Entrance.category = PrototypeDungeonRoom.RoomCategory.SPECIAL;
            CreepyGlitchRoom_Entrance.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            CreepyGlitchRoom_Entrance.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            CreepyGlitchRoom_Entrance.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            CreepyGlitchRoom_Entrance.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            CreepyGlitchRoom_Entrance.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            CreepyGlitchRoom_Entrance.pits = new List<PrototypeRoomPitEntry>();
            CreepyGlitchRoom_Entrance.placedObjects = new List<PrototypePlacedObjectData>();
            CreepyGlitchRoom_Entrance.placedObjectPositions = new List<Vector2>();
            CreepyGlitchRoom_Entrance.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            CreepyGlitchRoom_Entrance.roomEvents = new List<RoomEventDefinition>(0);
            CreepyGlitchRoom_Entrance.overriddenTilesets = 0;
            CreepyGlitchRoom_Entrance.prerequisites = new List<DungeonPrerequisite>();
            CreepyGlitchRoom_Entrance.InvalidInCoop = false;
            CreepyGlitchRoom_Entrance.cullProceduralDecorationOnWeakPlatforms = false;
            CreepyGlitchRoom_Entrance.preventAddedDecoLayering = false;
            CreepyGlitchRoom_Entrance.precludeAllTilemapDrawing = false;
            CreepyGlitchRoom_Entrance.drawPrecludedCeilingTiles = false;
            CreepyGlitchRoom_Entrance.preventBorders = false;
            CreepyGlitchRoom_Entrance.preventFacewallAO = false;
            CreepyGlitchRoom_Entrance.usesCustomAmbientLight = false;
            CreepyGlitchRoom_Entrance.customAmbientLight = Color.white;
            CreepyGlitchRoom_Entrance.ForceAllowDuplicates = false;
            CreepyGlitchRoom_Entrance.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            CreepyGlitchRoom_Entrance.IsLostWoodsRoom = false;
            CreepyGlitchRoom_Entrance.UseCustomMusic = false;
            CreepyGlitchRoom_Entrance.UseCustomMusicState = false;
            CreepyGlitchRoom_Entrance.CustomMusicEvent = string.Empty;
            CreepyGlitchRoom_Entrance.UseCustomMusicSwitch = false;
            CreepyGlitchRoom_Entrance.CustomMusicSwitch = string.Empty;
            CreepyGlitchRoom_Entrance.overrideRoomVisualTypeForSecretRooms = false;
            CreepyGlitchRoom_Entrance.rewardChestSpawnPosition = new IntVector2(2, 8);
            CreepyGlitchRoom_Entrance.Width = 8;
            CreepyGlitchRoom_Entrance.Height = 16;            
            RoomFromText.AddExitToRoom(CreepyGlitchRoom_Entrance, new Vector2(9, 13), DungeonData.Direction.EAST, overrideDoorObject: ExpandPrefabs.boss_foyertable.includedRooms.elements[1].room.exitData.exits[3].specifiedDoor);
            RoomFromText.AddObjectToRoom(CreepyGlitchRoom_Entrance, new Vector2(2, 4), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Teleporter_Gungeon_01, useExternalPrefab: true));
            RoomFromText.GenerateDefaultRoomLayout(CreepyGlitchRoom_Entrance);

            GungeoneerMimicBossRoom.name = "Creepy Glitched Room";
            GungeoneerMimicBossRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            GungeoneerMimicBossRoom.GUID = Guid.NewGuid().ToString();
            GungeoneerMimicBossRoom.PreventMirroring = false;
            GungeoneerMimicBossRoom.category = PrototypeDungeonRoom.RoomCategory.BOSS;
            GungeoneerMimicBossRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            GungeoneerMimicBossRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            GungeoneerMimicBossRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            GungeoneerMimicBossRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            GungeoneerMimicBossRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            GungeoneerMimicBossRoom.pits = new List<PrototypeRoomPitEntry>();
            GungeoneerMimicBossRoom.placedObjects = new List<PrototypePlacedObjectData>();
            GungeoneerMimicBossRoom.placedObjectPositions = new List<Vector2>();
            GungeoneerMimicBossRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            GungeoneerMimicBossRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            GungeoneerMimicBossRoom.overriddenTilesets = 0;
            GungeoneerMimicBossRoom.prerequisites = new List<DungeonPrerequisite>();
            GungeoneerMimicBossRoom.InvalidInCoop = false;
            GungeoneerMimicBossRoom.cullProceduralDecorationOnWeakPlatforms = false;
            GungeoneerMimicBossRoom.preventAddedDecoLayering = false;
            GungeoneerMimicBossRoom.precludeAllTilemapDrawing = false;
            GungeoneerMimicBossRoom.drawPrecludedCeilingTiles = false;
            GungeoneerMimicBossRoom.preventBorders = false;
            GungeoneerMimicBossRoom.preventFacewallAO = false;
            GungeoneerMimicBossRoom.usesCustomAmbientLight = false;
            GungeoneerMimicBossRoom.customAmbientLight = Color.white;
            GungeoneerMimicBossRoom.ForceAllowDuplicates = false;
            GungeoneerMimicBossRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            GungeoneerMimicBossRoom.IsLostWoodsRoom = false;
            GungeoneerMimicBossRoom.UseCustomMusic = false;
            GungeoneerMimicBossRoom.UseCustomMusicState = false;
            GungeoneerMimicBossRoom.CustomMusicEvent = string.Empty;
            GungeoneerMimicBossRoom.UseCustomMusicSwitch = false;
            GungeoneerMimicBossRoom.CustomMusicSwitch = string.Empty;
            GungeoneerMimicBossRoom.overrideRoomVisualTypeForSecretRooms = false;
            GungeoneerMimicBossRoom.rewardChestSpawnPosition = new IntVector2(13, 25);
            GungeoneerMimicBossRoom.Width = 32;
            GungeoneerMimicBossRoom.Height = 32;
            GungeoneerMimicBossRoom.associatedMinimapIcon = ExpandPrefabs.doublebeholsterroom01.associatedMinimapIcon;
            RoomFromText.AddExitToRoom(GungeoneerMimicBossRoom, new Vector2(0, 16), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(GungeoneerMimicBossRoom, new Vector2(33, 16), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(GungeoneerMimicBossRoom, new Vector2(16, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddObjectToRoom(GungeoneerMimicBossRoom, new Vector2(16, 29), NonEnemyBehaviour: ExpandPrefabs.EXPlayerMimicBoss.GetComponent<ExpandGungeoneerMimicBossPlacable>());
            RoomFromText.GenerateDefaultRoomLayout(GungeoneerMimicBossRoom);

            // Castle Custom Rooms
            Expand_Explode.name = "Expand TurtleMelon Explode";
            Expand_Explode.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Explode.GUID = Guid.NewGuid().ToString();
            Expand_Explode.PreventMirroring = false;
            Expand_Explode.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Explode.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Explode.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Explode.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Explode.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Explode.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Explode.pits = new List<PrototypeRoomPitEntry>();
            Expand_Explode.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Explode.placedObjectPositions = new List<Vector2>();
            Expand_Explode.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Explode.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Explode.overriddenTilesets = 0;
            Expand_Explode.prerequisites = new List<DungeonPrerequisite>();
            Expand_Explode.InvalidInCoop = false;
            Expand_Explode.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Explode.preventAddedDecoLayering = false;
            Expand_Explode.precludeAllTilemapDrawing = false;
            Expand_Explode.drawPrecludedCeilingTiles = false;
            Expand_Explode.preventBorders = false;
            Expand_Explode.preventFacewallAO = false;
            Expand_Explode.usesCustomAmbientLight = false;
            Expand_Explode.customAmbientLight = Color.white;
            Expand_Explode.ForceAllowDuplicates = false;
            Expand_Explode.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Explode.IsLostWoodsRoom = false;
            Expand_Explode.UseCustomMusic = false;
            Expand_Explode.UseCustomMusicState = false;
            Expand_Explode.CustomMusicEvent = string.Empty;
            Expand_Explode.UseCustomMusicSwitch = false;
            Expand_Explode.CustomMusicSwitch = string.Empty;
            Expand_Explode.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Explode.rewardChestSpawnPosition = new IntVector2(6, 14);
            Expand_Explode.Width = 28;
            Expand_Explode.Height = 21;
            Expand_Explode.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(15, 16),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "0239c0680f9f467dbe5c4aab7dd1eca6", // blobulon
                            contentsBasePosition = new Vector2(4, 6),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(23, 3),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(15, 16),
                        new Vector2(4, 6),
                        new Vector2(23, 3)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 4,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_Explode, new Vector2(0, 10), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Explode, new Vector2(29, 10), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Explode, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Explode, new Vector2(14, 22), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Explode, new Vector2(17, 17), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Explode, new Vector2(8, 6), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Explode, new Vector2(4, 9), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Explode, new Vector2(4, 11.5f), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Explode, new Vector2(6, 19), EnemyBehaviourGuid: "206405acad4d4c33aac6717d184dc8d4"); // apprentice_gunjurer
            RoomFromText.AddObjectToRoom(Expand_Explode, new Vector2(23, 3), EnemyBehaviourGuid: "ed37fa13e0fa4fcf8239643957c51293"); // gigi
            RoomFromText.AddObjectToRoom(Expand_Explode, new Vector2(7, 11), EnemyBehaviourGuid: "cf2b7021eac44e3f95af07db9a7c442c"); // LeadWizard (gunsinger)
            RoomFromText.GenerateRoomFromText(Expand_Explode, "RoomCellData.Castle.Expand_Explode_Layout.txt");


            Expand_C_Hub.name = "Expand TurtleMelon C Hub";
            Expand_C_Hub.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_C_Hub.GUID = Guid.NewGuid().ToString();
            Expand_C_Hub.PreventMirroring = false;
            Expand_C_Hub.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_C_Hub.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_C_Hub.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_C_Hub.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_C_Hub.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_C_Hub.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_C_Hub.pits = new List<PrototypeRoomPitEntry>();
            Expand_C_Hub.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_C_Hub.placedObjectPositions = new List<Vector2>();
            Expand_C_Hub.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_C_Hub.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_C_Hub.overriddenTilesets = 0;
            Expand_C_Hub.prerequisites = new List<DungeonPrerequisite>();
            Expand_C_Hub.InvalidInCoop = false;
            Expand_C_Hub.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_C_Hub.preventAddedDecoLayering = false;
            Expand_C_Hub.precludeAllTilemapDrawing = false;
            Expand_C_Hub.drawPrecludedCeilingTiles = false;
            Expand_C_Hub.preventBorders = false;
            Expand_C_Hub.preventFacewallAO = false;
            Expand_C_Hub.usesCustomAmbientLight = false;
            Expand_C_Hub.customAmbientLight = Color.white;
            Expand_C_Hub.ForceAllowDuplicates = false;
            Expand_C_Hub.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_C_Hub.IsLostWoodsRoom = false;
            Expand_C_Hub.UseCustomMusic = false;
            Expand_C_Hub.UseCustomMusicState = false;
            Expand_C_Hub.CustomMusicEvent = string.Empty;
            Expand_C_Hub.UseCustomMusicSwitch = false;
            Expand_C_Hub.CustomMusicSwitch = string.Empty;
            Expand_C_Hub.overrideRoomVisualTypeForSecretRooms = false;
            Expand_C_Hub.rewardChestSpawnPosition = new IntVector2(14, 14);
            Expand_C_Hub.Width = 40;
            Expand_C_Hub.Height = 40;
            Expand_C_Hub.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4db03291a12144d69fe940d5a01de376", // hollowpoint
                            contentsBasePosition = new Vector2(20, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4db03291a12144d69fe940d5a01de376", // hollowpoint
                            contentsBasePosition = new Vector2(20, 37),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(3, 20),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                         new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(36, 20),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(20, 2),
                        new Vector2(20, 37),
                        new Vector2(3, 20),
                        new Vector2(36, 20)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 4,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_C_Hub, new Vector2(0, 20), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_C_Hub, new Vector2(41, 20), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_C_Hub, new Vector2(7, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_C_Hub, new Vector2(30, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_C_Hub, new Vector2(7, 41), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_C_Hub, new Vector2(30, 41), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_C_Hub, new Vector2(18, 14), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomFromText.AddObjectToRoom(Expand_C_Hub, new Vector2(20, 2), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomFromText.AddObjectToRoom(Expand_C_Hub, new Vector2(20, 37), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomFromText.AddObjectToRoom(Expand_C_Hub, new Vector2(5, 20), EnemyBehaviourGuid: "ed37fa13e0fa4fcf8239643957c51293"); // gigi
            RoomFromText.AddObjectToRoom(Expand_C_Hub, new Vector2(33, 20), EnemyBehaviourGuid: "ed37fa13e0fa4fcf8239643957c51293"); // gigi
            RoomFromText.GenerateRoomFromText(Expand_C_Hub, "RoomCellData.Castle.Expand_C_Hub_Layout.txt");


            Expand_C_Gap.name = "Expand TurtleMelon C Gap";
            Expand_C_Gap.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_C_Gap.GUID = Guid.NewGuid().ToString();
            Expand_C_Gap.PreventMirroring = false;
            Expand_C_Gap.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_C_Gap.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_C_Gap.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_C_Gap.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_C_Gap.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_C_Gap.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_C_Gap.pits = new List<PrototypeRoomPitEntry>();
            Expand_C_Gap.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_C_Gap.placedObjectPositions = new List<Vector2>();
            Expand_C_Gap.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_C_Gap.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_C_Gap.overriddenTilesets = 0;
            Expand_C_Gap.prerequisites = new List<DungeonPrerequisite>();
            Expand_C_Gap.InvalidInCoop = false;
            Expand_C_Gap.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_C_Gap.preventAddedDecoLayering = false;
            Expand_C_Gap.precludeAllTilemapDrawing = false;
            Expand_C_Gap.drawPrecludedCeilingTiles = false;
            Expand_C_Gap.preventBorders = false;
            Expand_C_Gap.preventFacewallAO = false;
            Expand_C_Gap.usesCustomAmbientLight = false;
            Expand_C_Gap.customAmbientLight = Color.white;
            Expand_C_Gap.ForceAllowDuplicates = false;
            Expand_C_Gap.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_C_Gap.IsLostWoodsRoom = false;
            Expand_C_Gap.UseCustomMusic = false;
            Expand_C_Gap.UseCustomMusicState = false;
            Expand_C_Gap.CustomMusicEvent = string.Empty;
            Expand_C_Gap.UseCustomMusicSwitch = false;
            Expand_C_Gap.CustomMusicSwitch = string.Empty;
            Expand_C_Gap.overrideRoomVisualTypeForSecretRooms = false;
            Expand_C_Gap.rewardChestSpawnPosition = new IntVector2(23, 9);
            Expand_C_Gap.Width = 28;
            Expand_C_Gap.Height = 21;
            Expand_C_Gap.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
                            contentsBasePosition = new Vector2(24, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
                            contentsBasePosition = new Vector2(24, 18),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(24, 2),
                        new Vector2(24, 18)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 4,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(6, 6),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(6, 14),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(6, 6),
                        new Vector2(6, 14)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 4,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
            };
            RoomFromText.AddExitToRoom(Expand_C_Gap, new Vector2(0, 1), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_C_Gap, new Vector2(0, 20), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_C_Gap, new Vector2(29, 10), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_C_Gap, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_C_Gap, new Vector2(14, 22), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_C_Gap, new Vector2(9, 1), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_C_Gap, new Vector2(9, 19), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_C_Gap, new Vector2(13, 6), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_C_Gap, new Vector2(13, 14), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_C_Gap, new Vector2(4, 7), EnemyBehaviourGuid: "8bb5578fba374e8aae8e10b754e61d62"); // cardinal
            RoomFromText.AddObjectToRoom(Expand_C_Gap, new Vector2(3, 13), EnemyBehaviourGuid: "8bb5578fba374e8aae8e10b754e61d62"); // cardinal
            RoomFromText.GenerateRoomFromText(Expand_C_Gap, "RoomCellData.Castle.Expand_C_Gap_Layout.txt");


            Expand_ChainGap.name = "Expand TurtleMelon Chain Gap";
            Expand_ChainGap.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_ChainGap.GUID = Guid.NewGuid().ToString();
            Expand_ChainGap.PreventMirroring = false;
            Expand_ChainGap.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_ChainGap.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_ChainGap.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_ChainGap.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_ChainGap.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_ChainGap.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_ChainGap.pits = new List<PrototypeRoomPitEntry>();
            Expand_ChainGap.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_ChainGap.placedObjectPositions = new List<Vector2>();
            Expand_ChainGap.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_ChainGap.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_ChainGap.overriddenTilesets = 0;
            Expand_ChainGap.prerequisites = new List<DungeonPrerequisite>();
            Expand_ChainGap.InvalidInCoop = false;
            Expand_ChainGap.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_ChainGap.preventAddedDecoLayering = false;
            Expand_ChainGap.precludeAllTilemapDrawing = false;
            Expand_ChainGap.drawPrecludedCeilingTiles = false;
            Expand_ChainGap.preventBorders = false;
            Expand_ChainGap.preventFacewallAO = false;
            Expand_ChainGap.usesCustomAmbientLight = false;
            Expand_ChainGap.customAmbientLight = Color.white;
            Expand_ChainGap.ForceAllowDuplicates = false;
            Expand_ChainGap.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_ChainGap.IsLostWoodsRoom = false;
            Expand_ChainGap.UseCustomMusic = false;
            Expand_ChainGap.UseCustomMusicState = false;
            Expand_ChainGap.CustomMusicEvent = string.Empty;
            Expand_ChainGap.UseCustomMusicSwitch = false;
            Expand_ChainGap.CustomMusicSwitch = string.Empty;
            Expand_ChainGap.overrideRoomVisualTypeForSecretRooms = false;
            Expand_ChainGap.rewardChestSpawnPosition = new IntVector2(12, 18);
            Expand_ChainGap.Width = 28;
            Expand_ChainGap.Height = 21;
            Expand_ChainGap.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomFromText.AddExitToRoom(Expand_ChainGap, new Vector2(0, 10), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_ChainGap, new Vector2(29, 10), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_ChainGap, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_ChainGap, new Vector2(14, 22), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(13.25f, 3), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(13.25f, 17), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(4, 9), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(22, 9), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(9, 3), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(16, 3), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(9, 17), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(16, 17), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(4, 6), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(4, 11), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(22, 6), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(22, 11), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(13, 8), EnemyBehaviourGuid: "463d16121f884984abe759de38418e48"); // chain_gunner
            RoomFromText.AddObjectToRoom(Expand_ChainGap, new Vector2(13, 12), EnemyBehaviourGuid: "463d16121f884984abe759de38418e48"); // chain_gunner
            RoomFromText.GenerateRoomFromText(Expand_ChainGap, "RoomCellData.Castle.Expand_Chain_Gap_Layout.txt");


            Expand_Challange1.name = "Expand TurtleMelon Challenge 1";
            Expand_Challange1.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Challange1.GUID = Guid.NewGuid().ToString();
            Expand_Challange1.PreventMirroring = false;
            Expand_Challange1.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Challange1.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Challange1.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Challange1.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Challange1.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Challange1.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Challange1.pits = new List<PrototypeRoomPitEntry>();
            Expand_Challange1.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Challange1.placedObjectPositions = new List<Vector2>();
            Expand_Challange1.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Challange1.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Challange1.overriddenTilesets = 0;
            Expand_Challange1.prerequisites = new List<DungeonPrerequisite>();
            Expand_Challange1.InvalidInCoop = false;
            Expand_Challange1.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Challange1.preventAddedDecoLayering = false;
            Expand_Challange1.precludeAllTilemapDrawing = false;
            Expand_Challange1.drawPrecludedCeilingTiles = false;
            Expand_Challange1.preventBorders = false;
            Expand_Challange1.preventFacewallAO = false;
            Expand_Challange1.usesCustomAmbientLight = false;
            Expand_Challange1.customAmbientLight = Color.white;
            Expand_Challange1.ForceAllowDuplicates = false;
            Expand_Challange1.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Challange1.IsLostWoodsRoom = false;
            Expand_Challange1.UseCustomMusic = false;
            Expand_Challange1.UseCustomMusicState = false;
            Expand_Challange1.CustomMusicEvent = string.Empty;
            Expand_Challange1.UseCustomMusicSwitch = false;
            Expand_Challange1.CustomMusicSwitch = string.Empty;
            Expand_Challange1.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Challange1.rewardChestSpawnPosition = new IntVector2(18, 3);
            Expand_Challange1.Width = 40;
            Expand_Challange1.Height = 40;
            Expand_Challange1.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "699cd24270af4cd183d671090d8323a1", // key_bullet_kin
                            contentsBasePosition = new Vector2(23, 30),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "a446c626b56d4166915a4e29869737fd", // chance_bullet_kin
                            contentsBasePosition = new Vector2(24, 33),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(23, 30),
                        new Vector2(24, 33)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 7,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.TIMER,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_Challange1, new Vector2(19, 0), DungeonData.Direction.SOUTH, PrototypeRoomExit.ExitType.ENTRANCE_ONLY);
            RoomFromText.AddExitToRoom(Expand_Challange1, new Vector2(4, 40), DungeonData.Direction.NORTH, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomFromText.AddObjectToRoom(Expand_Challange1, new Vector2(3, 35), EnemyBehaviourGuid: "463d16121f884984abe759de38418e48"); // chain_gunner
            RoomFromText.AddObjectToRoom(Expand_Challange1, new Vector2(4, 38), EnemyBehaviourGuid: "4db03291a12144d69fe940d5a01de376"); // hollowpoint
            RoomFromText.AddObjectToRoom(Expand_Challange1, new Vector2(20, 22), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.GenerateRoomFromText(Expand_Challange1, "RoomCellData.Castle.Expand_Challange1_Layout.txt");


            Expand_Pit_Line.name = "Expand TurtleMelon Pit Line";
            Expand_Pit_Line.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Pit_Line.GUID = Guid.NewGuid().ToString();
            Expand_Pit_Line.PreventMirroring = false;
            Expand_Pit_Line.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Pit_Line.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Pit_Line.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Pit_Line.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Pit_Line.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Pit_Line.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Pit_Line.pits = new List<PrototypeRoomPitEntry>();
            Expand_Pit_Line.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Pit_Line.placedObjectPositions = new List<Vector2>();
            Expand_Pit_Line.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Pit_Line.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Pit_Line.overriddenTilesets = 0;
            Expand_Pit_Line.prerequisites = new List<DungeonPrerequisite>();
            Expand_Pit_Line.InvalidInCoop = false;
            Expand_Pit_Line.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Pit_Line.preventAddedDecoLayering = false;
            Expand_Pit_Line.precludeAllTilemapDrawing = false;
            Expand_Pit_Line.drawPrecludedCeilingTiles = false;
            Expand_Pit_Line.preventBorders = false;
            Expand_Pit_Line.preventFacewallAO = false;
            Expand_Pit_Line.usesCustomAmbientLight = false;
            Expand_Pit_Line.customAmbientLight = Color.white;
            Expand_Pit_Line.ForceAllowDuplicates = false;
            Expand_Pit_Line.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Pit_Line.IsLostWoodsRoom = false;
            Expand_Pit_Line.UseCustomMusic = false;
            Expand_Pit_Line.UseCustomMusicState = false;
            Expand_Pit_Line.CustomMusicEvent = string.Empty;
            Expand_Pit_Line.UseCustomMusicSwitch = false;
            Expand_Pit_Line.CustomMusicSwitch = string.Empty;
            Expand_Pit_Line.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Pit_Line.rewardChestSpawnPosition = new IntVector2(10, 2);
            Expand_Pit_Line.Width = 22;
            Expand_Pit_Line.Height = 30;
            Expand_Pit_Line.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomFromText.AddExitToRoom(Expand_Pit_Line, new Vector2(0, 15), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Pit_Line, new Vector2(23, 15), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Pit_Line, new Vector2(11, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Pit_Line, new Vector2(11, 31), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Pit_Line, new Vector2(10, 2), EnemyBehaviourGuid: "88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Pit_Line, new Vector2(10, 27), EnemyBehaviourGuid: "88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Pit_Line, new Vector2(5, 14), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomFromText.AddObjectToRoom(Expand_Pit_Line, new Vector2(16, 7), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomFromText.AddObjectToRoom(Expand_Pit_Line, new Vector2(16, 22), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomFromText.GenerateRoomFromText(Expand_Pit_Line, "RoomCellData.Castle.Expand_Pit_Line_Layout.txt");


            Expand_Singer_Gap.name = "Expand TurtleMelon Singer Gap";
            Expand_Singer_Gap.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Singer_Gap.GUID = Guid.NewGuid().ToString();
            Expand_Singer_Gap.PreventMirroring = false;
            Expand_Singer_Gap.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Singer_Gap.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Singer_Gap.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Singer_Gap.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Singer_Gap.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Singer_Gap.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Singer_Gap.pits = new List<PrototypeRoomPitEntry>();
            Expand_Singer_Gap.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Singer_Gap.placedObjectPositions = new List<Vector2>();
            Expand_Singer_Gap.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Singer_Gap.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Singer_Gap.overriddenTilesets = 0;
            Expand_Singer_Gap.prerequisites = new List<DungeonPrerequisite>();
            Expand_Singer_Gap.InvalidInCoop = false;
            Expand_Singer_Gap.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Singer_Gap.preventAddedDecoLayering = false;
            Expand_Singer_Gap.precludeAllTilemapDrawing = false;
            Expand_Singer_Gap.drawPrecludedCeilingTiles = false;
            Expand_Singer_Gap.preventBorders = false;
            Expand_Singer_Gap.preventFacewallAO = false;
            Expand_Singer_Gap.usesCustomAmbientLight = false;
            Expand_Singer_Gap.customAmbientLight = Color.white;
            Expand_Singer_Gap.ForceAllowDuplicates = false;
            Expand_Singer_Gap.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Singer_Gap.IsLostWoodsRoom = false;
            Expand_Singer_Gap.UseCustomMusic = false;
            Expand_Singer_Gap.UseCustomMusicState = false;
            Expand_Singer_Gap.CustomMusicEvent = string.Empty;
            Expand_Singer_Gap.UseCustomMusicSwitch = false;
            Expand_Singer_Gap.CustomMusicSwitch = string.Empty;
            Expand_Singer_Gap.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Singer_Gap.rewardChestSpawnPosition = new IntVector2(6, 4);
            Expand_Singer_Gap.Width = 30;
            Expand_Singer_Gap.Height = 26;
            Expand_Singer_Gap.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "463d16121f884984abe759de38418e48", // chain_gunner
                            contentsBasePosition = new Vector2(7, 15),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "2d4f8b5404614e7d8b235006acde427a", // shotgat
                            contentsBasePosition = new Vector2(9, 23),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "2feb50a6a40f4f50982e89fd276f6f15", // bullat
                            contentsBasePosition = new Vector2(25, 22),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(7, 15),
                        new Vector2(9, 23),
                        new Vector2(25, 22)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_Singer_Gap, new Vector2(31, 14), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Singer_Gap, new Vector2(11, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Singer_Gap, new Vector2(11, 26), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Singer_Gap, new Vector2(3, 2), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomFromText.AddObjectToRoom(Expand_Singer_Gap, new Vector2(19, 2), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomFromText.AddObjectToRoom(Expand_Singer_Gap, new Vector2(23, 10), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
            RoomFromText.AddObjectToRoom(Expand_Singer_Gap, new Vector2(23, 19), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
            RoomFromText.AddObjectToRoom(Expand_Singer_Gap, new Vector2(23, 15), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomFromText.AddObjectToRoom(Expand_Singer_Gap, new Vector2(10, 23), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomFromText.AddObjectToRoom(Expand_Singer_Gap, new Vector2(10, 17), EnemyBehaviourGuid: "cf2b7021eac44e3f95af07db9a7c442c"); // LeadWizard
            RoomFromText.GenerateRoomFromText(Expand_Singer_Gap, "RoomCellData.Castle.Expand_Singer_Gap_Layout.txt");

            Expand_Flying_Gap.name = "Expand TurtleMelon Flying Gap";
            Expand_Flying_Gap.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Flying_Gap.GUID = Guid.NewGuid().ToString();
            Expand_Flying_Gap.PreventMirroring = false;
            Expand_Flying_Gap.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Flying_Gap.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Flying_Gap.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Flying_Gap.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Flying_Gap.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Flying_Gap.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Flying_Gap.pits = new List<PrototypeRoomPitEntry>();
            Expand_Flying_Gap.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Flying_Gap.placedObjectPositions = new List<Vector2>();
            Expand_Flying_Gap.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Flying_Gap.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Flying_Gap.overriddenTilesets = 0;
            Expand_Flying_Gap.prerequisites = new List<DungeonPrerequisite>();
            Expand_Flying_Gap.InvalidInCoop = false;
            Expand_Flying_Gap.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Flying_Gap.preventAddedDecoLayering = false;
            Expand_Flying_Gap.precludeAllTilemapDrawing = false;
            Expand_Flying_Gap.drawPrecludedCeilingTiles = false;
            Expand_Flying_Gap.preventBorders = false;
            Expand_Flying_Gap.preventFacewallAO = false;
            Expand_Flying_Gap.usesCustomAmbientLight = false;
            Expand_Flying_Gap.customAmbientLight = Color.white;
            Expand_Flying_Gap.ForceAllowDuplicates = false;
            Expand_Flying_Gap.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Flying_Gap.IsLostWoodsRoom = false;
            Expand_Flying_Gap.UseCustomMusic = false;
            Expand_Flying_Gap.UseCustomMusicState = false;
            Expand_Flying_Gap.CustomMusicEvent = string.Empty;
            Expand_Flying_Gap.UseCustomMusicSwitch = false;
            Expand_Flying_Gap.CustomMusicSwitch = string.Empty;
            Expand_Flying_Gap.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Flying_Gap.rewardChestSpawnPosition = new IntVector2(12, 19);
            Expand_Flying_Gap.Width = 28;
            Expand_Flying_Gap.Height = 21;
            Expand_Flying_Gap.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "0239c0680f9f467dbe5c4aab7dd1eca6", // blobulon
                            contentsBasePosition = new Vector2(23, 6),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                            contentsBasePosition = new Vector2(4, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(23, 6),
                        new Vector2(4, 17)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_Flying_Gap, new Vector2(0, 6), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Flying_Gap, new Vector2(29, 6), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Flying_Gap, new Vector2(3, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Flying_Gap, new Vector2(25, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Flying_Gap, new Vector2(14, 22), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Flying_Gap, new Vector2(10, 6), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Flying_Gap, new Vector2(12, 6), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Flying_Gap, new Vector2(14, 6), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Flying_Gap, new Vector2(18, 4), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomFromText.AddObjectToRoom(Expand_Flying_Gap, new Vector2(18, 4), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomFromText.AddObjectToRoom(Expand_Flying_Gap, new Vector2(18, 23), EnemyBehaviourGuid: "88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Flying_Gap, new Vector2(13, 3), EnemyBehaviourGuid: "a400523e535f41ac80a43ff6b06dc0bf"); // green_bookllet
            RoomFromText.AddObjectToRoom(Expand_Flying_Gap, new Vector2(13, 10), EnemyBehaviourGuid: "6f22935656c54ccfb89fca30ad663a64"); // blue_bookllet
            RoomFromText.GenerateRoomFromText(Expand_Flying_Gap, "RoomCellData.Castle.Expand_Flying_Gap_Layout.txt");


            Expand_Battle.name = "Expand TurtleMelon Battle Hub";
            Expand_Battle.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Battle.GUID = Guid.NewGuid().ToString();
            Expand_Battle.PreventMirroring = false;
            Expand_Battle.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_Battle.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Battle.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Battle.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Battle.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Battle.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Battle.pits = new List<PrototypeRoomPitEntry>();
            Expand_Battle.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Battle.placedObjectPositions = new List<Vector2>();
            Expand_Battle.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Battle.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Battle.overriddenTilesets = 0;
            Expand_Battle.prerequisites = new List<DungeonPrerequisite>();
            Expand_Battle.InvalidInCoop = false;
            Expand_Battle.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Battle.preventAddedDecoLayering = false;
            Expand_Battle.precludeAllTilemapDrawing = false;
            Expand_Battle.drawPrecludedCeilingTiles = false;
            Expand_Battle.preventBorders = false;
            Expand_Battle.preventFacewallAO = false;
            Expand_Battle.usesCustomAmbientLight = false;
            Expand_Battle.customAmbientLight = Color.white;
            Expand_Battle.ForceAllowDuplicates = false;
            Expand_Battle.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Battle.IsLostWoodsRoom = false;
            Expand_Battle.UseCustomMusic = false;
            Expand_Battle.UseCustomMusicState = false;
            Expand_Battle.CustomMusicEvent = string.Empty;
            Expand_Battle.UseCustomMusicSwitch = false;
            Expand_Battle.CustomMusicSwitch = string.Empty;
            Expand_Battle.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Battle.rewardChestSpawnPosition = new IntVector2(14, 14);
            Expand_Battle.Width = 30;
            Expand_Battle.Height = 30;
            Expand_Battle.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "206405acad4d4c33aac6717d184dc8d4", // apprentice_gunjurer
                            contentsBasePosition = new Vector2(24, 7),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4db03291a12144d69fe940d5a01de376", // hollowpoint
                            contentsBasePosition = new Vector2(9, 10),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "844657ad68894a4facb1b8e1aef1abf9", // hooded_bullet
                            contentsBasePosition = new Vector2(16, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4db03291a12144d69fe940d5a01de376", // hollowpoint
                            contentsBasePosition = new Vector2(23, 23),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "206405acad4d4c33aac6717d184dc8d4", // apprentice_gunjurer
                            contentsBasePosition = new Vector2(6, 24),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(24, 7),
                        new Vector2(9, 10),
                        new Vector2(16, 17),
                        new Vector2(23, 23),
                        new Vector2(6, 24)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ec8ea75b557d4e7b8ceeaacdf6f8238c", // gun_nut
                            contentsBasePosition = new Vector2(6, 6),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "463d16121f884984abe759de38418e48", // chain_gunner
                            contentsBasePosition = new Vector2(23, 14),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(24, 23),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ec8ea75b557d4e7b8ceeaacdf6f8238c", // gun_nut
                            contentsBasePosition = new Vector2(6, 24),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(6, 6),
                        new Vector2(23, 14),
                        new Vector2(24, 23),
                        new Vector2(6, 24)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "7ec3e8146f634c559a7d58b19191cd43", // spirat
                            contentsBasePosition = new Vector2(6, 7),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "b4666cb6ef4f4b038ba8924fd8adf38f", // grenat
                            contentsBasePosition = new Vector2(24, 7),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "2d4f8b5404614e7d8b235006acde427a", // shotgat
                            contentsBasePosition = new Vector2(5, 13),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "1a4872dafdb34fd29fe8ac90bd2cea67", // king_bullat
                            contentsBasePosition = new Vector2(14, 16),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "7ec3e8146f634c559a7d58b19191cd43", // spirat
                            contentsBasePosition = new Vector2(6, 23),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "2feb50a6a40f4f50982e89fd276f6f15", // bullat
                            contentsBasePosition = new Vector2(25, 23),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(24, 7),
                        new Vector2(24, 7),
                        new Vector2(5, 13),
                        new Vector2(14, 16),
                        new Vector2(6, 23),
                        new Vector2(25, 23)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "a446c626b56d4166915a4e29869737fd", // chance_bullet_kin
                            contentsBasePosition = new Vector2(15, 10),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "699cd24270af4cd183d671090d8323a1", // key_bullet_kin
                            contentsBasePosition = new Vector2(15, 22),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(15, 10),
                        new Vector2(15, 22)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_Battle, new Vector2(0, 4), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Battle, new Vector2(31, 4), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Battle, new Vector2(0, 26), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Battle, new Vector2(31, 26), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Battle, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Battle, new Vector2(4, 31), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_Battle, new Vector2(26, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Battle, new Vector2(26, 31), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Battle, new Vector2(13, 13), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomFromText.AddObjectToRoom(Expand_Battle, new Vector2(25, 7), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomFromText.AddObjectToRoom(Expand_Battle, new Vector2(6, 9), EnemyBehaviourGuid: "70216cae6c1346309d86d4a0b4603045"); // veteran_bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Battle, new Vector2(18, 15), EnemyBehaviourGuid: "cf2b7021eac44e3f95af07db9a7c442c"); // LeadWizard
            RoomFromText.AddObjectToRoom(Expand_Battle, new Vector2(20, 20), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
            RoomFromText.AddObjectToRoom(Expand_Battle, new Vector2(4, 26), EnemyBehaviourGuid: "ed37fa13e0fa4fcf8239643957c51293"); // gigi
            RoomFromText.GenerateDefaultRoomLayout(Expand_Battle);


            Expand_Cross.name = "Expand TurtleMelon Cross";
            Expand_Cross.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Cross.GUID = Guid.NewGuid().ToString();
            Expand_Cross.PreventMirroring = false;
            Expand_Cross.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Cross.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Cross.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Cross.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Cross.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Cross.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Cross.pits = new List<PrototypeRoomPitEntry>();
            Expand_Cross.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Cross.placedObjectPositions = new List<Vector2>();
            Expand_Cross.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Cross.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Cross.overriddenTilesets = 0;
            Expand_Cross.prerequisites = new List<DungeonPrerequisite>();
            Expand_Cross.InvalidInCoop = false;
            Expand_Cross.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Cross.preventAddedDecoLayering = false;
            Expand_Cross.precludeAllTilemapDrawing = false;
            Expand_Cross.drawPrecludedCeilingTiles = false;
            Expand_Cross.preventBorders = false;
            Expand_Cross.preventFacewallAO = false;
            Expand_Cross.usesCustomAmbientLight = false;
            Expand_Cross.customAmbientLight = Color.white;
            Expand_Cross.ForceAllowDuplicates = false;
            Expand_Cross.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Cross.IsLostWoodsRoom = false;
            Expand_Cross.UseCustomMusic = false;
            Expand_Cross.UseCustomMusicState = false;
            Expand_Cross.CustomMusicEvent = string.Empty;
            Expand_Cross.UseCustomMusicSwitch = false;
            Expand_Cross.CustomMusicSwitch = string.Empty;
            Expand_Cross.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Cross.rewardChestSpawnPosition = new IntVector2(7, 13);
            Expand_Cross.overrideRoomVisualType = 6;
            Expand_Cross.Width = 30;
            Expand_Cross.Height = 31;
            Expand_Cross.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(16, 1),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(14, 28),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4db03291a12144d69fe940d5a01de376", // hollowpoint
                            contentsBasePosition = new Vector2(3, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4db03291a12144d69fe940d5a01de376", // hollowpoint
                            contentsBasePosition = new Vector2(27, 13),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(16, 1),
                        new Vector2(14, 28),
                        new Vector2(3, 17),
                        new Vector2(27, 13)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ec8ea75b557d4e7b8ceeaacdf6f8238c", // gun_nut
                            contentsBasePosition = new Vector2(22, 13),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ec8ea75b557d4e7b8ceeaacdf6f8238c", // gun_nut
                            contentsBasePosition = new Vector2(8, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(22, 13),
                        new Vector2(8, 17)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_Cross, new Vector2(0, 15), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Cross, new Vector2(31, 15), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Cross, new Vector2(15, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Cross, new Vector2(15, 31), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Cross, new Vector2(15, 2), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Cross, new Vector2(15, 8), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Cross, new Vector2(15, 15), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Cross, new Vector2(15, 22), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Cross, new Vector2(15, 27), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Cross, new Vector2(5, 15), EnemyBehaviourGuid: "8bb5578fba374e8aae8e10b754e61d62"); // cardinal
            RoomFromText.AddObjectToRoom(Expand_Cross, new Vector2(25, 15), EnemyBehaviourGuid: "8bb5578fba374e8aae8e10b754e61d62"); // cardinal
            RoomFromText.GenerateRoomFromText(Expand_Cross, "RoomCellData.Castle.Expand_Cross_Layout.txt");


            Expand_Blocks.name = "Expand TurtleMelon Blocks";
            Expand_Blocks.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Blocks.GUID = Guid.NewGuid().ToString();
            Expand_Blocks.PreventMirroring = false;
            Expand_Blocks.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Blocks.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Blocks.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Blocks.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Blocks.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Blocks.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Blocks.pits = new List<PrototypeRoomPitEntry>();
            Expand_Blocks.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Blocks.placedObjectPositions = new List<Vector2>();
            Expand_Blocks.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Blocks.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Blocks.overriddenTilesets = 0;
            Expand_Blocks.prerequisites = new List<DungeonPrerequisite>();
            Expand_Blocks.InvalidInCoop = false;
            Expand_Blocks.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Blocks.preventAddedDecoLayering = false;
            Expand_Blocks.precludeAllTilemapDrawing = false;
            Expand_Blocks.drawPrecludedCeilingTiles = false;
            Expand_Blocks.preventBorders = false;
            Expand_Blocks.preventFacewallAO = false;
            Expand_Blocks.usesCustomAmbientLight = false;
            Expand_Blocks.customAmbientLight = Color.white;
            Expand_Blocks.ForceAllowDuplicates = false;
            Expand_Blocks.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Blocks.IsLostWoodsRoom = false;
            Expand_Blocks.UseCustomMusic = false;
            Expand_Blocks.UseCustomMusicState = false;
            Expand_Blocks.CustomMusicEvent = string.Empty;
            Expand_Blocks.UseCustomMusicSwitch = false;
            Expand_Blocks.CustomMusicSwitch = string.Empty;
            Expand_Blocks.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Blocks.rewardChestSpawnPosition = new IntVector2(8, 14);
            Expand_Blocks.overrideRoomVisualType = -1;
            Expand_Blocks.Width = 31;
            Expand_Blocks.Height = 31;
            Expand_Blocks.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "1a4872dafdb34fd29fe8ac90bd2cea67", // king_bullat
                            contentsBasePosition = new Vector2(8, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "1a4872dafdb34fd29fe8ac90bd2cea67", // king_bullat
                            contentsBasePosition = new Vector2(10, 18),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(8, 2),
                        new Vector2(10, 18)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
            };
            RoomFromText.AddExitToRoom(Expand_Blocks, new Vector2(0, 19), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Blocks, new Vector2(32, 13), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Blocks, new Vector2(10, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Blocks, new Vector2(16, 31), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Blocks, new Vector2(23, 10), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Blocks, new Vector2(6, 17), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Blocks, new Vector2(16, 25), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Blocks, new Vector2(10, 2), EnemyBehaviourGuid: "c0ff3744760c4a2eb0bb52ac162056e6"); // bookllet
            RoomFromText.AddObjectToRoom(Expand_Blocks, new Vector2(24, 10), EnemyBehaviourGuid: "c0ff3744760c4a2eb0bb52ac162056e6"); // bookllet
            RoomFromText.AddObjectToRoom(Expand_Blocks, new Vector2(8, 18), EnemyBehaviourGuid: "6f22935656c54ccfb89fca30ad663a64"); // blue_bookllet
            RoomFromText.AddObjectToRoom(Expand_Blocks, new Vector2(16, 27), EnemyBehaviourGuid: "a400523e535f41ac80a43ff6b06dc0bf"); // green_bookllet
            RoomFromText.GenerateRoomFromText(Expand_Blocks, "RoomCellData.Castle.Expand_Blocks_Layout.txt");



            Expand_Blocks_Pits.name = "Expand TurtleMelon Blocks Pits";
            Expand_Blocks_Pits.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Blocks_Pits.GUID = Guid.NewGuid().ToString();
            Expand_Blocks_Pits.PreventMirroring = false;
            Expand_Blocks_Pits.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Blocks_Pits.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Blocks_Pits.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Blocks_Pits.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Blocks_Pits.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Blocks_Pits.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Blocks_Pits.pits = new List<PrototypeRoomPitEntry>();
            Expand_Blocks_Pits.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Blocks_Pits.placedObjectPositions = new List<Vector2>();
            Expand_Blocks_Pits.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Blocks_Pits.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Blocks_Pits.overriddenTilesets = 0;
            Expand_Blocks_Pits.prerequisites = new List<DungeonPrerequisite>();
            Expand_Blocks_Pits.InvalidInCoop = false;
            Expand_Blocks_Pits.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Blocks_Pits.preventAddedDecoLayering = false;
            Expand_Blocks_Pits.precludeAllTilemapDrawing = false;
            Expand_Blocks_Pits.drawPrecludedCeilingTiles = false;
            Expand_Blocks_Pits.preventBorders = false;
            Expand_Blocks_Pits.preventFacewallAO = false;
            Expand_Blocks_Pits.usesCustomAmbientLight = false;
            Expand_Blocks_Pits.customAmbientLight = Color.white;
            Expand_Blocks_Pits.ForceAllowDuplicates = false;
            Expand_Blocks_Pits.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Blocks_Pits.IsLostWoodsRoom = false;
            Expand_Blocks_Pits.UseCustomMusic = false;
            Expand_Blocks_Pits.UseCustomMusicState = false;
            Expand_Blocks_Pits.CustomMusicEvent = string.Empty;
            Expand_Blocks_Pits.UseCustomMusicSwitch = false;
            Expand_Blocks_Pits.CustomMusicSwitch = string.Empty;
            Expand_Blocks_Pits.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Blocks_Pits.rewardChestSpawnPosition = new IntVector2(16, 11);
            Expand_Blocks_Pits.overrideRoomVisualType = -1;
            Expand_Blocks_Pits.Width = 22;
            Expand_Blocks_Pits.Height = 18;
            Expand_Blocks_Pits.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "88b6b6a93d4b4234a67844ef4728382c", // bandana_bullet_kin
                            contentsBasePosition = new Vector2(17, 9),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() { new Vector2(17, 9) },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = false,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
            };
            RoomFromText.AddExitToRoom(Expand_Blocks_Pits, new Vector2(0, 9), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Blocks_Pits, new Vector2(23, 9), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Blocks_Pits, new Vector2(11, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Blocks_Pits, new Vector2(11, 19), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Blocks_Pits, new Vector2(10, 2), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Blocks_Pits, new Vector2(2, 10), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Blocks_Pits, new Vector2(17, 9), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.GenerateRoomFromText(Expand_Blocks_Pits, "RoomCellData.Castle.Expand_Blocks_Pits_Layout.txt");


            Expand_Wall_Pit.name = "Expand TurtleMelon Wall Pit";
            Expand_Wall_Pit.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Wall_Pit.GUID = Guid.NewGuid().ToString();
            Expand_Wall_Pit.PreventMirroring = false;
            Expand_Wall_Pit.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Wall_Pit.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Wall_Pit.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Wall_Pit.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Wall_Pit.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Wall_Pit.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Wall_Pit.pits = new List<PrototypeRoomPitEntry>();
            Expand_Wall_Pit.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Wall_Pit.placedObjectPositions = new List<Vector2>();
            Expand_Wall_Pit.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Wall_Pit.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Wall_Pit.overriddenTilesets = 0;
            Expand_Wall_Pit.prerequisites = new List<DungeonPrerequisite>();
            Expand_Wall_Pit.InvalidInCoop = false;
            Expand_Wall_Pit.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Wall_Pit.preventAddedDecoLayering = false;
            Expand_Wall_Pit.precludeAllTilemapDrawing = false;
            Expand_Wall_Pit.drawPrecludedCeilingTiles = false;
            Expand_Wall_Pit.preventBorders = false;
            Expand_Wall_Pit.preventFacewallAO = false;
            Expand_Wall_Pit.usesCustomAmbientLight = false;
            Expand_Wall_Pit.customAmbientLight = Color.white;
            Expand_Wall_Pit.ForceAllowDuplicates = false;
            Expand_Wall_Pit.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Wall_Pit.IsLostWoodsRoom = false;
            Expand_Wall_Pit.UseCustomMusic = false;
            Expand_Wall_Pit.UseCustomMusicState = false;
            Expand_Wall_Pit.CustomMusicEvent = string.Empty;
            Expand_Wall_Pit.UseCustomMusicSwitch = false;
            Expand_Wall_Pit.CustomMusicSwitch = string.Empty;
            Expand_Wall_Pit.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Wall_Pit.rewardChestSpawnPosition = new IntVector2(4, 9);
            Expand_Wall_Pit.overrideRoomVisualType = -1;
            Expand_Wall_Pit.Width = 22;
            Expand_Wall_Pit.Height = 18;
            Expand_Wall_Pit.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
                            contentsBasePosition = new Vector2(5, 8),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "2752019b770f473193b08b4005dc781f", // veteran_shotgun_kin
                            contentsBasePosition = new Vector2(16, 8),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(5, 8),
                        new Vector2(16, 8)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
            };
            RoomFromText.AddExitToRoom(Expand_Wall_Pit, new Vector2(0, 9), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Wall_Pit, new Vector2(23, 9), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Wall_Pit, new Vector2(2, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Wall_Pit, new Vector2(20, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Wall_Pit, new Vector2(2, 19), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_Wall_Pit, new Vector2(20, 19), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Wall_Pit, new Vector2(9, 8), EnemyBehaviourGuid: "1a4872dafdb34fd29fe8ac90bd2cea67"); // king_bullat
            RoomFromText.GenerateRoomFromText(Expand_Wall_Pit, "RoomCellData.Castle.Expand_Wall_Pit_Layout.txt");



            Expand_Gate_Cross.name = "Expand TurtleMelon Gate Cross";
            Expand_Gate_Cross.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Gate_Cross.GUID = Guid.NewGuid().ToString();
            Expand_Gate_Cross.PreventMirroring = false;
            Expand_Gate_Cross.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Gate_Cross.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Gate_Cross.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Gate_Cross.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Gate_Cross.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Gate_Cross.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Gate_Cross.pits = new List<PrototypeRoomPitEntry>();
            Expand_Gate_Cross.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Gate_Cross.placedObjectPositions = new List<Vector2>();
            Expand_Gate_Cross.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Gate_Cross.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Gate_Cross.overriddenTilesets = 0;
            Expand_Gate_Cross.prerequisites = new List<DungeonPrerequisite>();
            Expand_Gate_Cross.InvalidInCoop = false;
            Expand_Gate_Cross.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Gate_Cross.preventAddedDecoLayering = false;
            Expand_Gate_Cross.precludeAllTilemapDrawing = false;
            Expand_Gate_Cross.drawPrecludedCeilingTiles = false;
            Expand_Gate_Cross.preventBorders = false;
            Expand_Gate_Cross.preventFacewallAO = false;
            Expand_Gate_Cross.usesCustomAmbientLight = false;
            Expand_Gate_Cross.customAmbientLight = Color.white;
            Expand_Gate_Cross.ForceAllowDuplicates = false;
            Expand_Gate_Cross.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Gate_Cross.IsLostWoodsRoom = false;
            Expand_Gate_Cross.UseCustomMusic = false;
            Expand_Gate_Cross.UseCustomMusicState = false;
            Expand_Gate_Cross.CustomMusicEvent = string.Empty;
            Expand_Gate_Cross.UseCustomMusicSwitch = false;
            Expand_Gate_Cross.CustomMusicSwitch = string.Empty;
            Expand_Gate_Cross.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Gate_Cross.rewardChestSpawnPosition = new IntVector2(15, 9);
            Expand_Gate_Cross.overrideRoomVisualType = -1;
            Expand_Gate_Cross.Width = 20;
            Expand_Gate_Cross.Height = 20;
            Expand_Gate_Cross.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(4, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(16, 12),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                            contentsBasePosition = new Vector2(16, 7),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                            contentsBasePosition = new Vector2(4, 18),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(4, 2),
                        new Vector2(16, 12),
                        new Vector2(16, 8),
                        new Vector2(4, 18)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
            };
            RoomFromText.AddExitToRoom(Expand_Gate_Cross, new Vector2(0, 10), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Gate_Cross, new Vector2(15, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Gate_Cross, new Vector2(15, 21), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Gate_Cross, new Vector2(16, 1), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Gate_Cross, new Vector2(5, 2), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Gate_Cross, new Vector2(5, 17), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Gate_Cross, new Vector2(16, 18), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Gate_Cross, new Vector2(16, 7), EnemyBehaviourGuid: "cf2b7021eac44e3f95af07db9a7c442c"); // LeadWizard
            RoomFromText.AddObjectToRoom(Expand_Gate_Cross, new Vector2(16, 12), EnemyBehaviourGuid: "206405acad4d4c33aac6717d184dc8d4"); // apprentice_gunjurer
            RoomFromText.GenerateRoomFromText(Expand_Gate_Cross, "RoomCellData.Castle.Expand_Gate_Cross_Layout.txt");

            Expand_Passage.name = "Expand TurtleMelon Passage";
            Expand_Passage.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Passage.GUID = Guid.NewGuid().ToString();
            Expand_Passage.PreventMirroring = false;
            Expand_Passage.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Passage.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Passage.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Passage.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Passage.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Passage.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Passage.pits = new List<PrototypeRoomPitEntry>();
            Expand_Passage.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Passage.placedObjectPositions = new List<Vector2>();
            Expand_Passage.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Passage.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Passage.overriddenTilesets = 0;
            Expand_Passage.prerequisites = new List<DungeonPrerequisite>();
            Expand_Passage.InvalidInCoop = false;
            Expand_Passage.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Passage.preventAddedDecoLayering = false;
            Expand_Passage.precludeAllTilemapDrawing = false;
            Expand_Passage.drawPrecludedCeilingTiles = false;
            Expand_Passage.preventBorders = false;
            Expand_Passage.preventFacewallAO = false;
            Expand_Passage.usesCustomAmbientLight = false;
            Expand_Passage.customAmbientLight = Color.white;
            Expand_Passage.ForceAllowDuplicates = false;
            Expand_Passage.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Passage.IsLostWoodsRoom = false;
            Expand_Passage.UseCustomMusic = false;
            Expand_Passage.UseCustomMusicState = false;
            Expand_Passage.CustomMusicEvent = string.Empty;
            Expand_Passage.UseCustomMusicSwitch = false;
            Expand_Passage.CustomMusicSwitch = string.Empty;
            Expand_Passage.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Passage.rewardChestSpawnPosition = new IntVector2(6, 2);
            Expand_Passage.overrideRoomVisualType = -1;
            Expand_Passage.Width = 24;
            Expand_Passage.Height = 20;
            Expand_Passage.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "b54d89f9e802455cbb2b8a96a31e8259", // blue_shotgun_kin
                            contentsBasePosition = new Vector2(12, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "128db2f0781141bcb505d8f00f9e4d47", // red_shotgun_kin
                            contentsBasePosition = new Vector2(12, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(4, 2),
                        new Vector2(12, 17)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
            };
            RoomFromText.AddExitToRoom(Expand_Passage, new Vector2(12, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Passage, new Vector2(12, 21), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Passage, new Vector2(2, 2), EnemyBehaviourGuid: "4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
            RoomFromText.AddObjectToRoom(Expand_Passage, new Vector2(20, 2), EnemyBehaviourGuid: "4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
            RoomFromText.AddObjectToRoom(Expand_Passage, new Vector2(11, 17), EnemyBehaviourGuid: "c0260c286c8d4538a697c5bf24976ccf"); // dynamite_kin
            RoomFromText.GenerateRoomFromText(Expand_Passage, "RoomCellData.Castle.Expand_Passage_Layout.txt");


            Expand_Pit_Jump.name = "Expand TurtleMelon Pit Jump";
            Expand_Pit_Jump.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Pit_Jump.GUID = Guid.NewGuid().ToString();
            Expand_Pit_Jump.PreventMirroring = false;
            Expand_Pit_Jump.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Pit_Jump.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Pit_Jump.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Pit_Jump.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Pit_Jump.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Pit_Jump.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Pit_Jump.pits = new List<PrototypeRoomPitEntry>();
            Expand_Pit_Jump.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Pit_Jump.placedObjectPositions = new List<Vector2>();
            Expand_Pit_Jump.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Pit_Jump.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Pit_Jump.overriddenTilesets = 0;
            Expand_Pit_Jump.prerequisites = new List<DungeonPrerequisite>();
            Expand_Pit_Jump.InvalidInCoop = false;
            Expand_Pit_Jump.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Pit_Jump.preventAddedDecoLayering = false;
            Expand_Pit_Jump.precludeAllTilemapDrawing = false;
            Expand_Pit_Jump.drawPrecludedCeilingTiles = false;
            Expand_Pit_Jump.preventBorders = false;
            Expand_Pit_Jump.preventFacewallAO = false;
            Expand_Pit_Jump.usesCustomAmbientLight = false;
            Expand_Pit_Jump.customAmbientLight = Color.white;
            Expand_Pit_Jump.ForceAllowDuplicates = false;
            Expand_Pit_Jump.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Pit_Jump.IsLostWoodsRoom = false;
            Expand_Pit_Jump.UseCustomMusic = false;
            Expand_Pit_Jump.UseCustomMusicState = false;
            Expand_Pit_Jump.CustomMusicEvent = string.Empty;
            Expand_Pit_Jump.UseCustomMusicSwitch = false;
            Expand_Pit_Jump.CustomMusicSwitch = string.Empty;
            Expand_Pit_Jump.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Pit_Jump.rewardChestSpawnPosition = new IntVector2(10, 12);
            Expand_Pit_Jump.overrideRoomVisualType = -1;
            Expand_Pit_Jump.Width = 30;
            Expand_Pit_Jump.Height = 24;
            Expand_Pit_Jump.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomFromText.AddExitToRoom(Expand_Pit_Jump, new Vector2(0, 2), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Pit_Jump, new Vector2(31, 22), DungeonData.Direction.EAST);
            RoomFromText.AddObjectToRoom(Expand_Pit_Jump, new Vector2(12, 3), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomFromText.AddObjectToRoom(Expand_Pit_Jump, new Vector2(15, 13), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // Blobulon
            RoomFromText.AddObjectToRoom(Expand_Pit_Jump, new Vector2(16, 21), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
            RoomFromText.GenerateRoomFromText(Expand_Pit_Jump, "RoomCellData.Castle.Expand_Pit_Jump_Layout.txt");


            Expand_Pit_Passage.name = "Expand TurtleMelon Pit Passage";
            Expand_Pit_Passage.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Pit_Passage.GUID = Guid.NewGuid().ToString();
            Expand_Pit_Passage.PreventMirroring = false;
            Expand_Pit_Passage.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Pit_Passage.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Pit_Passage.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Pit_Passage.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Pit_Passage.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Pit_Passage.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Pit_Passage.pits = new List<PrototypeRoomPitEntry>();
            Expand_Pit_Passage.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Pit_Passage.placedObjectPositions = new List<Vector2>();
            Expand_Pit_Passage.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Pit_Passage.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Pit_Passage.overriddenTilesets = 0;
            Expand_Pit_Passage.prerequisites = new List<DungeonPrerequisite>();
            Expand_Pit_Passage.InvalidInCoop = false;
            Expand_Pit_Passage.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Pit_Passage.preventAddedDecoLayering = false;
            Expand_Pit_Passage.precludeAllTilemapDrawing = false;
            Expand_Pit_Passage.drawPrecludedCeilingTiles = false;
            Expand_Pit_Passage.preventBorders = false;
            Expand_Pit_Passage.preventFacewallAO = false;
            Expand_Pit_Passage.usesCustomAmbientLight = false;
            Expand_Pit_Passage.customAmbientLight = Color.white;
            Expand_Pit_Passage.ForceAllowDuplicates = false;
            Expand_Pit_Passage.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Pit_Passage.IsLostWoodsRoom = false;
            Expand_Pit_Passage.UseCustomMusic = false;
            Expand_Pit_Passage.UseCustomMusicState = false;
            Expand_Pit_Passage.CustomMusicEvent = string.Empty;
            Expand_Pit_Passage.UseCustomMusicSwitch = false;
            Expand_Pit_Passage.CustomMusicSwitch = string.Empty;
            Expand_Pit_Passage.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Pit_Passage.rewardChestSpawnPosition = new IntVector2(12, 6);
            Expand_Pit_Passage.overrideRoomVisualType = -1;
            Expand_Pit_Passage.Width = 20;
            Expand_Pit_Passage.Height = 20;
            Expand_Pit_Passage.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomFromText.AddExitToRoom(Expand_Pit_Passage, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Pit_Passage, new Vector2(14, 21), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Pit_Passage, new Vector2(13, 4), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomFromText.AddObjectToRoom(Expand_Pit_Passage, new Vector2(5, 10), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomFromText.AddObjectToRoom(Expand_Pit_Passage, new Vector2(14, 15), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomFromText.GenerateRoomFromText(Expand_Pit_Passage, "RoomCellData.Castle.Expand_Pit_Passage_Layout.txt");


            Expand_R_Blocks.name = "Expand TurtleMelon R Blocks";
            Expand_R_Blocks.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_R_Blocks.GUID = Guid.NewGuid().ToString();
            Expand_R_Blocks.PreventMirroring = false;
            Expand_R_Blocks.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_R_Blocks.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_R_Blocks.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_R_Blocks.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_R_Blocks.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_R_Blocks.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_R_Blocks.pits = new List<PrototypeRoomPitEntry>();
            Expand_R_Blocks.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_R_Blocks.placedObjectPositions = new List<Vector2>();
            Expand_R_Blocks.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_R_Blocks.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_R_Blocks.overriddenTilesets = 0;
            Expand_R_Blocks.prerequisites = new List<DungeonPrerequisite>();
            Expand_R_Blocks.InvalidInCoop = false;
            Expand_R_Blocks.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_R_Blocks.preventAddedDecoLayering = false;
            Expand_R_Blocks.precludeAllTilemapDrawing = false;
            Expand_R_Blocks.drawPrecludedCeilingTiles = false;
            Expand_R_Blocks.preventBorders = false;
            Expand_R_Blocks.preventFacewallAO = false;
            Expand_R_Blocks.usesCustomAmbientLight = false;
            Expand_R_Blocks.customAmbientLight = Color.white;
            Expand_R_Blocks.ForceAllowDuplicates = false;
            Expand_R_Blocks.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_R_Blocks.IsLostWoodsRoom = false;
            Expand_R_Blocks.UseCustomMusic = false;
            Expand_R_Blocks.UseCustomMusicState = false;
            Expand_R_Blocks.CustomMusicEvent = string.Empty;
            Expand_R_Blocks.UseCustomMusicSwitch = false;
            Expand_R_Blocks.CustomMusicSwitch = string.Empty;
            Expand_R_Blocks.overrideRoomVisualTypeForSecretRooms = false;
            Expand_R_Blocks.rewardChestSpawnPosition = new IntVector2(6, 17);
            Expand_R_Blocks.overrideRoomVisualType = -1;
            Expand_R_Blocks.Width = 42;
            Expand_R_Blocks.Height = 34;
            Expand_R_Blocks.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "88b6b6a93d4b4234a67844ef4728382c", // bandana_bullet_kin
                            contentsBasePosition = new Vector2(8, 5),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "88b6b6a93d4b4234a67844ef4728382c", // bandana_bullet_kin
                            contentsBasePosition = new Vector2(6, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "88b6b6a93d4b4234a67844ef4728382c", // bandana_bullet_kin
                            contentsBasePosition = new Vector2(35, 25),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "88b6b6a93d4b4234a67844ef4728382c", // bandana_bullet_kin
                            contentsBasePosition = new Vector2(7, 31),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(8, 5),
                        new Vector2(6, 17),
                        new Vector2(35, 25),
                        new Vector2(7, 31)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
            };
            RoomFromText.AddExitToRoom(Expand_R_Blocks, new Vector2(42, 27), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_R_Blocks, new Vector2(10, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_R_Blocks, new Vector2(13, 35), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_R_Blocks, new Vector2(9, 3), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_R_Blocks, new Vector2(34, 8), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_R_Blocks, new Vector2(9, 17), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_R_Blocks, new Vector2(9, 28), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.GenerateRoomFromText(Expand_R_Blocks, "RoomCellData.Castle.Expand_R_Blocks_Layout.txt");


            Expand_Small_Passage.name = "Expand TurtleMelon Small Passage 1";
            Expand_Small_Passage.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Small_Passage.GUID = Guid.NewGuid().ToString();
            Expand_Small_Passage.PreventMirroring = false;
            Expand_Small_Passage.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Small_Passage.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Small_Passage.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Small_Passage.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Small_Passage.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Small_Passage.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Small_Passage.pits = new List<PrototypeRoomPitEntry>();
            Expand_Small_Passage.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Small_Passage.placedObjectPositions = new List<Vector2>();
            Expand_Small_Passage.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Small_Passage.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Small_Passage.overriddenTilesets = 0;
            Expand_Small_Passage.prerequisites = new List<DungeonPrerequisite>();
            Expand_Small_Passage.InvalidInCoop = false;
            Expand_Small_Passage.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Small_Passage.preventAddedDecoLayering = false;
            Expand_Small_Passage.precludeAllTilemapDrawing = false;
            Expand_Small_Passage.drawPrecludedCeilingTiles = false;
            Expand_Small_Passage.preventBorders = false;
            Expand_Small_Passage.preventFacewallAO = false;
            Expand_Small_Passage.usesCustomAmbientLight = false;
            Expand_Small_Passage.customAmbientLight = Color.white;
            Expand_Small_Passage.ForceAllowDuplicates = false;
            Expand_Small_Passage.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Small_Passage.IsLostWoodsRoom = false;
            Expand_Small_Passage.UseCustomMusic = false;
            Expand_Small_Passage.UseCustomMusicState = false;
            Expand_Small_Passage.CustomMusicEvent = string.Empty;
            Expand_Small_Passage.UseCustomMusicSwitch = false;
            Expand_Small_Passage.CustomMusicSwitch = string.Empty;
            Expand_Small_Passage.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Small_Passage.rewardChestSpawnPosition = new IntVector2(8, 16);
            Expand_Small_Passage.overrideRoomVisualType = 6;
            Expand_Small_Passage.allowFloorDecoration = false;
            Expand_Small_Passage.Width = 20;
            Expand_Small_Passage.Height = 18;
            Expand_Small_Passage.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "8bb5578fba374e8aae8e10b754e61d62", // cardinal
                            contentsBasePosition = new Vector2(0, 14),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() { new Vector2(0, 14) },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 0.6f,
                    numberTimesEncounteredRequired = 0
                },
            };
            RoomFromText.AddExitToRoom(Expand_Small_Passage, new Vector2(0, 1), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Small_Passage, new Vector2(20, 17), DungeonData.Direction.EAST);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(10, 0), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(11, 0), objectDatabase.BushFlowers);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(11, 1), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(10, 6), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(10, 7), objectDatabase.BushFlowers);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(9, 7), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(0, 16), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(0, 17), objectDatabase.BushFlowers);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(1, 17), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(2, 8), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(1, 8), objectDatabase.BushFlowers);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(1, 9), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(2, 1), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(3, 1), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(4, 1), objectDatabase.BushFlowers);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(5, 1), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(8, 17), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(9, 17), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(10, 17), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(11, 17), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(11, 3), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(11, 4), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(0, 11), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(0, 12), objectDatabase.BushFlowers);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(0, 13), objectDatabase.Bush);
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(1, 13), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Small_Passage, new Vector2(5, 8), EnemyBehaviourGuid: "4db03291a12144d69fe940d5a01de376"); // hollowpoint
            RoomFromText.GenerateRoomFromText(Expand_Small_Passage, "RoomCellData.Castle.Expand_Small_Passage_Layout.txt");


            Expand_Box.name = "Expand TurtleMelon Box";
            Expand_Box.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Box.GUID = Guid.NewGuid().ToString();
            Expand_Box.PreventMirroring = false;
            Expand_Box.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Box.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Box.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Box.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Box.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Box.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Box.pits = new List<PrototypeRoomPitEntry>();
            Expand_Box.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Box.placedObjectPositions = new List<Vector2>();
            Expand_Box.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Box.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Box.overriddenTilesets = 0;
            Expand_Box.prerequisites = new List<DungeonPrerequisite>();
            Expand_Box.InvalidInCoop = false;
            Expand_Box.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Box.preventAddedDecoLayering = false;
            Expand_Box.precludeAllTilemapDrawing = false;
            Expand_Box.drawPrecludedCeilingTiles = false;
            Expand_Box.preventBorders = false;
            Expand_Box.preventFacewallAO = false;
            Expand_Box.usesCustomAmbientLight = false;
            Expand_Box.customAmbientLight = Color.white;
            Expand_Box.ForceAllowDuplicates = false;
            Expand_Box.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Box.IsLostWoodsRoom = false;
            Expand_Box.UseCustomMusic = false;
            Expand_Box.UseCustomMusicState = false;
            Expand_Box.CustomMusicEvent = string.Empty;
            Expand_Box.UseCustomMusicSwitch = false;
            Expand_Box.CustomMusicSwitch = string.Empty;
            Expand_Box.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Box.rewardChestSpawnPosition = new IntVector2(4, 10);
            Expand_Box.overrideRoomVisualType = -1;
            Expand_Box.Width = 20;
            Expand_Box.Height = 22;
            Expand_Box.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                            contentsBasePosition = new Vector2(5, 4),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                            contentsBasePosition = new Vector2(12, 13),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "128db2f0781141bcb505d8f00f9e4d47", // red_shotgun_kin
                            contentsBasePosition = new Vector2(14, 19),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(5, 4),
                        new Vector2(12, 13),
                        new Vector2(14, 19)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "844657ad68894a4facb1b8e1aef1abf9", // hooded_bullet
                            contentsBasePosition = new Vector2(5, 4),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "844657ad68894a4facb1b8e1aef1abf9", // hooded_bullet
                            contentsBasePosition = new Vector2(11, 13),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(5, 4),
                        new Vector2(11, 13)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 0.7f,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_Box, new Vector2(0, 13), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Box, new Vector2(21, 4), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Box, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Box, new Vector2(14, 23), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Box, new Vector2(5, 4), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Box, new Vector2(12, 13), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Box, new Vector2(14, 4), EnemyBehaviourGuid: "4db03291a12144d69fe940d5a01de376"); // hollowpoint
            RoomFromText.GenerateRoomFromText(Expand_Box, "RoomCellData.Castle.Expand_Box_Layout.txt");


            Expand_Steps.name = "Expand TurtleMelon Steps";
            Expand_Steps.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Steps.GUID = Guid.NewGuid().ToString();
            Expand_Steps.PreventMirroring = false;
            Expand_Steps.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Steps.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Steps.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Steps.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Steps.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Steps.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Steps.pits = new List<PrototypeRoomPitEntry>();
            Expand_Steps.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Steps.placedObjectPositions = new List<Vector2>();
            Expand_Steps.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Steps.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Steps.overriddenTilesets = 0;
            Expand_Steps.prerequisites = new List<DungeonPrerequisite>();
            Expand_Steps.InvalidInCoop = false;
            Expand_Steps.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Steps.preventAddedDecoLayering = false;
            Expand_Steps.precludeAllTilemapDrawing = false;
            Expand_Steps.drawPrecludedCeilingTiles = false;
            Expand_Steps.preventBorders = false;
            Expand_Steps.preventFacewallAO = false;
            Expand_Steps.usesCustomAmbientLight = false;
            Expand_Steps.customAmbientLight = Color.white;
            Expand_Steps.ForceAllowDuplicates = false;
            Expand_Steps.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Steps.IsLostWoodsRoom = false;
            Expand_Steps.UseCustomMusic = false;
            Expand_Steps.UseCustomMusicState = false;
            Expand_Steps.CustomMusicEvent = string.Empty;
            Expand_Steps.UseCustomMusicSwitch = false;
            Expand_Steps.CustomMusicSwitch = string.Empty;
            Expand_Steps.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Steps.rewardChestSpawnPosition = new IntVector2(16, 3);
            Expand_Steps.overrideRoomVisualType = -1;
            Expand_Steps.Width = 20;
            Expand_Steps.Height = 20;
            Expand_Steps.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomFromText.AddExitToRoom(Expand_Steps, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Steps, new Vector2(16, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Steps, new Vector2(4, 21), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_Steps, new Vector2(16, 21), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Steps, new Vector2(1, 4), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Steps, new Vector2(1, 10), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Steps, new Vector2(18, 7), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Steps, new Vector2(18, 13), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Steps, new Vector2(5, 2), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Steps, new Vector2(13, 5), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Steps, new Vector2(14, 11), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_Steps, new Vector2(4, 7), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // blobulon
            RoomFromText.AddObjectToRoom(Expand_Steps, new Vector2(4, 15), EnemyBehaviourGuid: "206405acad4d4c33aac6717d184dc8d4"); // apprentice_gunjurer
            RoomFromText.GenerateRoomFromText(Expand_Steps, "RoomCellData.Castle.Expand_Steps_Layout.txt");


            Expand_Spiral.name = "Expand TurtleMelon Spiral";
            Expand_Spiral.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Spiral.GUID = Guid.NewGuid().ToString();
            Expand_Spiral.PreventMirroring = false;
            Expand_Spiral.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Spiral.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Spiral.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Spiral.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Spiral.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Spiral.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Spiral.pits = new List<PrototypeRoomPitEntry>();
            Expand_Spiral.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Spiral.placedObjectPositions = new List<Vector2>();
            Expand_Spiral.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Spiral.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Spiral.overriddenTilesets = 0;
            Expand_Spiral.prerequisites = new List<DungeonPrerequisite>();
            Expand_Spiral.InvalidInCoop = false;
            Expand_Spiral.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Spiral.preventAddedDecoLayering = false;
            Expand_Spiral.precludeAllTilemapDrawing = false;
            Expand_Spiral.drawPrecludedCeilingTiles = false;
            Expand_Spiral.preventBorders = false;
            Expand_Spiral.preventFacewallAO = false;
            Expand_Spiral.usesCustomAmbientLight = false;
            Expand_Spiral.customAmbientLight = Color.white;
            Expand_Spiral.ForceAllowDuplicates = false;
            Expand_Spiral.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Spiral.IsLostWoodsRoom = false;
            Expand_Spiral.UseCustomMusic = false;
            Expand_Spiral.UseCustomMusicState = false;
            Expand_Spiral.CustomMusicEvent = string.Empty;
            Expand_Spiral.UseCustomMusicSwitch = false;
            Expand_Spiral.CustomMusicSwitch = string.Empty;
            Expand_Spiral.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Spiral.rewardChestSpawnPosition = new IntVector2(15, 2);
            Expand_Spiral.overrideRoomVisualType = -1;
            Expand_Spiral.allowFloorDecoration = false;
            Expand_Spiral.Width = 38;
            Expand_Spiral.Height = 40;
            Expand_Spiral.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(7, 8),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(36, 9),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(4, 14),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(27, 23),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(34, 29),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(7, 31),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(9, 31),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4db03291a12144d69fe940d5a01de376", // hollowpoint
                            contentsBasePosition = new Vector2(13, 13),
                            spawnChance = 0.5f,
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0,
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(7, 8),
                        new Vector2(36, 9),
                        new Vector2(4, 14),
                        new Vector2(27, 23),
                        new Vector2(34, 29),
                        new Vector2(7, 31),
                        new Vector2(9, 31),
                        new Vector2(13, 13),
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_Spiral, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Spiral, new Vector2(4, 41), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_Spiral, new Vector2(34, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Spiral, new Vector2(34, 41), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(5, 8), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(27, 8), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(33, 10), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(26, 13), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(3, 15), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(36, 16), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(19, 18), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(9, 21), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(16, 23), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(34, 25), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(27, 26), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(4, 28), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(21, 31), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(0, 5), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(13, 17), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(36, 34), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Spiral, new Vector2(18, 22), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.GenerateRoomFromText(Expand_Spiral, "RoomCellData.Castle.Expand_Spiral_Layout.txt");


            Expand_Apache_Hub.name = "Expand MelonTurtle Apache Hub";
            Expand_Apache_Hub.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Apache_Hub.GUID = Guid.NewGuid().ToString();
            Expand_Apache_Hub.PreventMirroring = false;
            Expand_Apache_Hub.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_Apache_Hub.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Apache_Hub.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Apache_Hub.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Apache_Hub.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Apache_Hub.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Apache_Hub.pits = new List<PrototypeRoomPitEntry>();
            Expand_Apache_Hub.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Apache_Hub.placedObjectPositions = new List<Vector2>();
            Expand_Apache_Hub.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Apache_Hub.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Apache_Hub.overriddenTilesets = 0;
            Expand_Apache_Hub.prerequisites = new List<DungeonPrerequisite>();
            Expand_Apache_Hub.InvalidInCoop = false;
            Expand_Apache_Hub.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Apache_Hub.preventAddedDecoLayering = false;
            Expand_Apache_Hub.precludeAllTilemapDrawing = false;
            Expand_Apache_Hub.drawPrecludedCeilingTiles = false;
            Expand_Apache_Hub.preventBorders = false;
            Expand_Apache_Hub.preventFacewallAO = false;
            Expand_Apache_Hub.usesCustomAmbientLight = false;
            Expand_Apache_Hub.customAmbientLight = Color.white;
            Expand_Apache_Hub.ForceAllowDuplicates = false;
            Expand_Apache_Hub.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Apache_Hub.IsLostWoodsRoom = false;
            Expand_Apache_Hub.UseCustomMusic = false;
            Expand_Apache_Hub.UseCustomMusicState = false;
            Expand_Apache_Hub.CustomMusicEvent = string.Empty;
            Expand_Apache_Hub.UseCustomMusicSwitch = false;
            Expand_Apache_Hub.CustomMusicSwitch = string.Empty;
            Expand_Apache_Hub.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Apache_Hub.rewardChestSpawnPosition = new IntVector2(11, 33);
            Expand_Apache_Hub.overrideRoomVisualType = -1;
            Expand_Apache_Hub.Width = 40;
            Expand_Apache_Hub.Height = 50;
            Expand_Apache_Hub.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "88b6b6a93d4b4234a67844ef4728382c", // bandana_bullet_kin
                            contentsBasePosition = new Vector2(35, 10),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4db03291a12144d69fe940d5a01de376", // hollowpoint
                            contentsBasePosition = new Vector2(4, 44),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(35, 10),
                        new Vector2(4, 44)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 0.6f,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_Apache_Hub, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Apache_Hub, new Vector2(4, 51), DungeonData.Direction.NORTH);            
            RoomFromText.AddExitToRoom(Expand_Apache_Hub, new Vector2(36, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Apache_Hub, new Vector2(36, 51), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Apache_Hub, new Vector2(18, 17), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomFromText.AddObjectToRoom(Expand_Apache_Hub, new Vector2(34, 25), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomFromText.AddObjectToRoom(Expand_Apache_Hub, new Vector2(3, 20), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomFromText.AddObjectToRoom(Expand_Apache_Hub, new Vector2(35, 15), EnemyBehaviourGuid: "463d16121f884984abe759de38418e48"); // chain_gunner
            RoomFromText.GenerateRoomFromText(Expand_Apache_Hub, "RoomCellData.Castle.Expand_Apache_Hub_Layout.txt");


            Expand_Box_Hub.name = "Expand MelonTurtle Box Hub";
            Expand_Box_Hub.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Box_Hub.GUID = Guid.NewGuid().ToString();
            Expand_Box_Hub.PreventMirroring = false;
            Expand_Box_Hub.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_Box_Hub.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Box_Hub.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Box_Hub.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Box_Hub.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Box_Hub.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Box_Hub.pits = new List<PrototypeRoomPitEntry>();
            Expand_Box_Hub.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Box_Hub.placedObjectPositions = new List<Vector2>();
            Expand_Box_Hub.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Box_Hub.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Box_Hub.overriddenTilesets = 0;
            Expand_Box_Hub.prerequisites = new List<DungeonPrerequisite>();
            Expand_Box_Hub.InvalidInCoop = false;
            Expand_Box_Hub.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Box_Hub.preventAddedDecoLayering = false;
            Expand_Box_Hub.precludeAllTilemapDrawing = false;
            Expand_Box_Hub.drawPrecludedCeilingTiles = false;
            Expand_Box_Hub.preventBorders = false;
            Expand_Box_Hub.preventFacewallAO = false;
            Expand_Box_Hub.usesCustomAmbientLight = false;
            Expand_Box_Hub.customAmbientLight = Color.white;
            Expand_Box_Hub.ForceAllowDuplicates = false;
            Expand_Box_Hub.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Box_Hub.IsLostWoodsRoom = false;
            Expand_Box_Hub.UseCustomMusic = false;
            Expand_Box_Hub.UseCustomMusicState = false;
            Expand_Box_Hub.CustomMusicEvent = string.Empty;
            Expand_Box_Hub.UseCustomMusicSwitch = false;
            Expand_Box_Hub.CustomMusicSwitch = string.Empty;
            Expand_Box_Hub.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Box_Hub.rewardChestSpawnPosition = new IntVector2(23, 19);
            Expand_Box_Hub.overrideRoomVisualType = -1;
            Expand_Box_Hub.Width = 40;
            Expand_Box_Hub.Height = 40;
            Expand_Box_Hub.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                            contentsBasePosition = new Vector2(9, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                            contentsBasePosition = new Vector2(17, 29),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(26, 10),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(19, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(11, 11),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(34, 22),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(9, 37),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(9, 2),
                        new Vector2(17, 29),
                        new Vector2(26, 10),
                        new Vector2(19, 17),
                        new Vector2(11, 11),
                        new Vector2(34, 22),
                        new Vector2(9, 37)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 0.9f,
                    numberTimesEncounteredRequired = 0
                }
            };

            RoomFromText.AddExitToRoom(Expand_Box_Hub, new Vector2(0, 20), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_Box_Hub, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Box_Hub, new Vector2(4, 41), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_Box_Hub, new Vector2(41, 20), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_Box_Hub, new Vector2(35, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Box_Hub, new Vector2(35, 41), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Box_Hub, new Vector2(17, 11), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomFromText.AddObjectToRoom(Expand_Box_Hub, new Vector2(27, 9), EnemyBehaviourGuid: "6f22935656c54ccfb89fca30ad663a64"); // blue_bookllet
            RoomFromText.AddObjectToRoom(Expand_Box_Hub, new Vector2(3, 20), EnemyBehaviourGuid: "c0ff3744760c4a2eb0bb52ac162056e6"); // bookllet
            RoomFromText.AddObjectToRoom(Expand_Box_Hub, new Vector2(11, 15), EnemyBehaviourGuid: "c0ff3744760c4a2eb0bb52ac162056e6"); // bookllet
            RoomFromText.AddObjectToRoom(Expand_Box_Hub, new Vector2(34, 28), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomFromText.AddObjectToRoom(Expand_Box_Hub, new Vector2(10, 36), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // red_shotgun_kin
            RoomFromText.GenerateRoomFromText(Expand_Box_Hub, "RoomCellData.Castle.Expand_Box_Hub_Layout.txt");



            Expand_Enclose_Hub.name = "Expand MelonTurtle Enclose Hub";
            Expand_Enclose_Hub.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Enclose_Hub.GUID = Guid.NewGuid().ToString();
            Expand_Enclose_Hub.PreventMirroring = false;
            Expand_Enclose_Hub.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_Enclose_Hub.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Enclose_Hub.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Enclose_Hub.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Enclose_Hub.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Enclose_Hub.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Enclose_Hub.pits = new List<PrototypeRoomPitEntry>();
            Expand_Enclose_Hub.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Enclose_Hub.placedObjectPositions = new List<Vector2>();
            Expand_Enclose_Hub.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Enclose_Hub.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Enclose_Hub.overriddenTilesets = 0;
            Expand_Enclose_Hub.prerequisites = new List<DungeonPrerequisite>();
            Expand_Enclose_Hub.InvalidInCoop = false;
            Expand_Enclose_Hub.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Enclose_Hub.preventAddedDecoLayering = false;
            Expand_Enclose_Hub.precludeAllTilemapDrawing = false;
            Expand_Enclose_Hub.drawPrecludedCeilingTiles = false;
            Expand_Enclose_Hub.preventBorders = false;
            Expand_Enclose_Hub.preventFacewallAO = false;
            Expand_Enclose_Hub.usesCustomAmbientLight = false;
            Expand_Enclose_Hub.customAmbientLight = Color.white;
            Expand_Enclose_Hub.ForceAllowDuplicates = false;
            Expand_Enclose_Hub.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Enclose_Hub.IsLostWoodsRoom = false;
            Expand_Enclose_Hub.UseCustomMusic = false;
            Expand_Enclose_Hub.UseCustomMusicState = false;
            Expand_Enclose_Hub.CustomMusicEvent = string.Empty;
            Expand_Enclose_Hub.UseCustomMusicSwitch = false;
            Expand_Enclose_Hub.CustomMusicSwitch = string.Empty;
            Expand_Enclose_Hub.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Enclose_Hub.rewardChestSpawnPosition = new IntVector2(23, 19);
            Expand_Enclose_Hub.overrideRoomVisualType = 6;
            Expand_Enclose_Hub.Width = 40;
            Expand_Enclose_Hub.Height = 39;
            Expand_Enclose_Hub.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "8bb5578fba374e8aae8e10b754e61d62", // cardinal
                            contentsBasePosition = new Vector2(30, 8),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "8bb5578fba374e8aae8e10b754e61d62", // cardinal
                            contentsBasePosition = new Vector2(8, 30),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "1a4872dafdb34fd29fe8ac90bd2cea67", // king_bullat
                            contentsBasePosition = new Vector2(19, 21),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(30, 8),
                        new Vector2(8, 30),
                        new Vector2(19, 21)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(26, 7),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(13, 9),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(12, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(28, 19),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(18, 24),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(26, 29),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(26, 7),
                        new Vector2(13, 9),
                        new Vector2(12, 17),
                        new Vector2(28, 19),
                        new Vector2(18, 24),
                        new Vector2(26, 29)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(34, 1),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(5, 37),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                            contentsBasePosition = new Vector2(5, 1),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                            contentsBasePosition = new Vector2(33, 37),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(12, 7),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(29, 11),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(11, 27),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(29, 28),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(34, 1),
                        new Vector2(5, 37),
                        new Vector2(5, 1),
                        new Vector2(33, 37),
                        new Vector2(12, 7),
                        new Vector2(29, 11),
                        new Vector2(11, 27),
                        new Vector2(29, 28)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 0.8f,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_Enclose_Hub, new Vector2(5, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Enclose_Hub, new Vector2(5, 40), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_Enclose_Hub, new Vector2(20, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Enclose_Hub, new Vector2(20, 40), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_Enclose_Hub, new Vector2(35, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_Enclose_Hub, new Vector2(35, 40), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(10, 7), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(27, 13), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(10, 31), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(30, 32), objectDatabase.ExplodyBarrel);
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(28, 9), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(23, 23), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(15, 27), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(28, 28), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableVertical, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(14, 12), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(27, 19), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(11, 20), ExpandUtility.GenerateDungeonPlacable(objectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(18, 18), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(19, 6), EnemyBehaviourGuid: "463d16121f884984abe759de38418e48"); // chain_gunner
            RoomFromText.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(19, 32), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomFromText.GenerateRoomFromText(Expand_Enclose_Hub, "RoomCellData.Castle.Expand_Enclose_Hub_Layout.txt");



            Expand_SpiderMaze.name = "Expand Apache SpiderMaze";
            Expand_SpiderMaze.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_SpiderMaze.GUID = Guid.NewGuid().ToString();
            Expand_SpiderMaze.PreventMirroring = false;
            Expand_SpiderMaze.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_SpiderMaze.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_SpiderMaze.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_SpiderMaze.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_SpiderMaze.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_SpiderMaze.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_SpiderMaze.pits = new List<PrototypeRoomPitEntry>();
            Expand_SpiderMaze.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_SpiderMaze.placedObjectPositions = new List<Vector2>();
            Expand_SpiderMaze.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_SpiderMaze.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_SpiderMaze.overriddenTilesets = 0;
            Expand_SpiderMaze.prerequisites = new List<DungeonPrerequisite>();
            Expand_SpiderMaze.InvalidInCoop = false;
            Expand_SpiderMaze.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_SpiderMaze.preventAddedDecoLayering = false;
            Expand_SpiderMaze.precludeAllTilemapDrawing = false;
            Expand_SpiderMaze.drawPrecludedCeilingTiles = false;
            Expand_SpiderMaze.preventBorders = false;
            Expand_SpiderMaze.preventFacewallAO = false;
            Expand_SpiderMaze.usesCustomAmbientLight = false;
            Expand_SpiderMaze.customAmbientLight = Color.white;
            Expand_SpiderMaze.ForceAllowDuplicates = false;
            Expand_SpiderMaze.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_SpiderMaze.IsLostWoodsRoom = false;
            Expand_SpiderMaze.UseCustomMusic = false;
            Expand_SpiderMaze.UseCustomMusicState = false;
            Expand_SpiderMaze.CustomMusicEvent = string.Empty;
            Expand_SpiderMaze.UseCustomMusicSwitch = false;
            Expand_SpiderMaze.CustomMusicSwitch = string.Empty;
            Expand_SpiderMaze.overrideRoomVisualTypeForSecretRooms = false;
            Expand_SpiderMaze.rewardChestSpawnPosition = new IntVector2(20, 20);
            Expand_SpiderMaze.overrideRoomVisualType = -1;
            Expand_SpiderMaze.Width = 50;
            Expand_SpiderMaze.Height = 50;
            Expand_SpiderMaze.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomFromText.AddExitToRoom(Expand_SpiderMaze, new Vector2(0, 13), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_SpiderMaze, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_SpiderMaze, new Vector2(0, 32), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_SpiderMaze, new Vector2(23, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_SpiderMaze, new Vector2(51, 13), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_SpiderMaze, new Vector2(9, 51), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_SpiderMaze, new Vector2(51, 29), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_SpiderMaze, new Vector2(46, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_SpiderMaze, new Vector2(36, 51), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(19, 20), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(17, 21), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(25, 21), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(20, 18), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(20, 25), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(23, 36), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(28, 36), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(25, 29), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(41, 15), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(2, 15), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(15, 7), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(20, 40), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(45, 44), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(32, 10), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(9, 34), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(21, 21), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(24, 34), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(30, 30), EnemyBehaviourGuid: "249db525a9464e5282d02162c88e0357"); // spent
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(6, 35), EnemyBehaviourGuid: "249db525a9464e5282d02162c88e0357"); // spent
            RoomFromText.AddObjectToRoom(Expand_SpiderMaze, new Vector2(29, 34), EnemyBehaviourGuid: "5288e86d20184fa69c91ceb642d31474"); // gummy
            RoomFromText.GenerateRoomFromText(Expand_SpiderMaze, "RoomCellData.Hollows.Expand_SpiderMaze_Layout.txt");


            SecretExitRoom2.name = "Secret Elevator Exit";
            SecretExitRoom2.QAID = "AA" + UnityEngine.Random.Range(1000, 9999);
            SecretExitRoom2.GUID = Guid.NewGuid().ToString();
            SecretExitRoom2.PreventMirroring = false;
            SecretExitRoom2.category = PrototypeDungeonRoom.RoomCategory.EXIT;
            SecretExitRoom2.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            SecretExitRoom2.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            SecretExitRoom2.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            SecretExitRoom2.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            SecretExitRoom2.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>(0) };
            SecretExitRoom2.pits = new List<PrototypeRoomPitEntry>();
            SecretExitRoom2.placedObjects = new List<PrototypePlacedObjectData>();
            SecretExitRoom2.placedObjectPositions = new List<Vector2>();
            SecretExitRoom2.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            SecretExitRoom2.roomEvents = new List<RoomEventDefinition>(0);
            SecretExitRoom2.overriddenTilesets = 0;
            SecretExitRoom2.prerequisites = new List<DungeonPrerequisite>();
            SecretExitRoom2.InvalidInCoop = false;
            SecretExitRoom2.cullProceduralDecorationOnWeakPlatforms = false;
            SecretExitRoom2.preventAddedDecoLayering = false;
            SecretExitRoom2.precludeAllTilemapDrawing = false;
            SecretExitRoom2.drawPrecludedCeilingTiles = false;
            SecretExitRoom2.preventBorders = false;
            SecretExitRoom2.preventFacewallAO = false;
            SecretExitRoom2.usesCustomAmbientLight = false;
            SecretExitRoom2.customAmbientLight = Color.white;
            SecretExitRoom2.ForceAllowDuplicates = false;
            SecretExitRoom2.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            SecretExitRoom2.IsLostWoodsRoom = false;
            SecretExitRoom2.UseCustomMusic = false;
            SecretExitRoom2.UseCustomMusicState = false;
            SecretExitRoom2.CustomMusicEvent = string.Empty;
            SecretExitRoom2.UseCustomMusicSwitch = false;
            SecretExitRoom2.CustomMusicSwitch = string.Empty;
            SecretExitRoom2.overrideRoomVisualTypeForSecretRooms = false;
            SecretExitRoom2.rewardChestSpawnPosition = IntVector2.One;
            SecretExitRoom2.Width = 16;
            SecretExitRoom2.Height = 12;
            SecretExitRoom2.associatedMinimapIcon = ExpandPrefabs.elevator_maintenance_room.associatedMinimapIcon;
            SecretExitRoom2.overrideRoomVisualType = 0;
            SecretExitRoom2.usesProceduralDecoration = true;
            SecretExitRoom2.usesProceduralLighting = true;
            SecretExitRoom2.allowFloorDecoration = false;
            RoomFromText.AddObjectToRoom(SecretExitRoom2, new Vector2(1, 6), ExpandPrefabs.ElevatorDeparture);
            RoomFromText.AddObjectToRoom(SecretExitRoom2, new Vector2(11, 4), NonEnemyBehaviour: ExpandSecretDoorPrefabs.EXSecretDoorDestination.GetComponent<ExpandSecretDoorExitPlacable>());
            RoomFromText.AddObjectToRoom(SecretExitRoom2, new Vector2(9, 2), ExpandUtility.GenerateDungeonPlacable(objectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomFromText.GenerateRoomFromText(SecretExitRoom2, "RoomCellData.SecretExitRoom2_Layout.txt");


            SecretRatEntranceRoom.name = "Secret Rat MiniElevator Room";
            SecretRatEntranceRoom.QAID = "AA" + UnityEngine.Random.Range(1000, 9999);
            SecretRatEntranceRoom.GUID = Guid.NewGuid().ToString();
            SecretRatEntranceRoom.PreventMirroring = false;
            SecretRatEntranceRoom.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            SecretRatEntranceRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            SecretRatEntranceRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            SecretRatEntranceRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            SecretRatEntranceRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            SecretRatEntranceRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            SecretRatEntranceRoom.pits = new List<PrototypeRoomPitEntry>();
            SecretRatEntranceRoom.placedObjects = new List<PrototypePlacedObjectData>();
            SecretRatEntranceRoom.placedObjectPositions = new List<Vector2>();
            SecretRatEntranceRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            SecretRatEntranceRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            SecretRatEntranceRoom.overriddenTilesets = 0;
            SecretRatEntranceRoom.prerequisites = new List<DungeonPrerequisite>();
            SecretRatEntranceRoom.InvalidInCoop = false;
            SecretRatEntranceRoom.cullProceduralDecorationOnWeakPlatforms = false;
            SecretRatEntranceRoom.preventAddedDecoLayering = false;
            SecretRatEntranceRoom.precludeAllTilemapDrawing = false;
            SecretRatEntranceRoom.drawPrecludedCeilingTiles = false;
            SecretRatEntranceRoom.preventBorders = false;
            SecretRatEntranceRoom.preventFacewallAO = false;
            SecretRatEntranceRoom.usesCustomAmbientLight = false;
            SecretRatEntranceRoom.customAmbientLight = Color.white;
            SecretRatEntranceRoom.ForceAllowDuplicates = false;
            SecretRatEntranceRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            SecretRatEntranceRoom.IsLostWoodsRoom = false;
            SecretRatEntranceRoom.UseCustomMusic = false;
            SecretRatEntranceRoom.UseCustomMusicState = false;
            SecretRatEntranceRoom.CustomMusicEvent = string.Empty;
            SecretRatEntranceRoom.UseCustomMusicSwitch = false;
            SecretRatEntranceRoom.CustomMusicSwitch = string.Empty;
            SecretRatEntranceRoom.overrideRoomVisualTypeForSecretRooms = false;
            SecretRatEntranceRoom.rewardChestSpawnPosition = IntVector2.One;
            SecretRatEntranceRoom.Width = 16;
            SecretRatEntranceRoom.Height = 18;
            SecretRatEntranceRoom.associatedMinimapIcon = ExpandPrefabs.elevator_maintenance_room.associatedMinimapIcon;
            SecretRatEntranceRoom.usesProceduralDecoration = true;
            SecretRatEntranceRoom.usesProceduralLighting = true;
            SecretRatEntranceRoom.allowFloorDecoration = false;
            RoomFromText.AddExitToRoom(SecretRatEntranceRoom, new Vector2(0, 8), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(SecretRatEntranceRoom, new Vector2(17, 8), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(SecretRatEntranceRoom, new Vector2(8, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(SecretRatEntranceRoom, new Vector2(2, 19), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(SecretRatEntranceRoom, new Vector2(14, 19), DungeonData.Direction.NORTH);            
            RoomFromText.AddObjectToRoom(SecretRatEntranceRoom, new Vector2(6, 6), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomFromText.AddObjectToRoom(SecretRatEntranceRoom, new Vector2(6, 16), NonEnemyBehaviour: ExpandSecretDoorPrefabs.EXSecretDoor.GetComponent<ExpandSecretDoorPlacable>());
            RoomFromText.GenerateRoomFromText(SecretRatEntranceRoom, "RoomCellData.SecretRatEntranceRoom_Layout.txt");

            
            Expand_SecretElevatorEntranceRoom.name = "Secret MiniElevator Room";
            Expand_SecretElevatorEntranceRoom.QAID = "AA" + UnityEngine.Random.Range(1000, 9999);
            Expand_SecretElevatorEntranceRoom.GUID = Guid.NewGuid().ToString();
            Expand_SecretElevatorEntranceRoom.PreventMirroring = false;
            Expand_SecretElevatorEntranceRoom.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_SecretElevatorEntranceRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_SecretElevatorEntranceRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_SecretElevatorEntranceRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_SecretElevatorEntranceRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_SecretElevatorEntranceRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_SecretElevatorEntranceRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_SecretElevatorEntranceRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_SecretElevatorEntranceRoom.placedObjectPositions = new List<Vector2>();
            Expand_SecretElevatorEntranceRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_SecretElevatorEntranceRoom.roomEvents = new List<RoomEventDefinition>(0);
            Expand_SecretElevatorEntranceRoom.overriddenTilesets = 0;
            Expand_SecretElevatorEntranceRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_SecretElevatorEntranceRoom.InvalidInCoop = false;
            Expand_SecretElevatorEntranceRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_SecretElevatorEntranceRoom.preventAddedDecoLayering = false;
            Expand_SecretElevatorEntranceRoom.precludeAllTilemapDrawing = false;
            Expand_SecretElevatorEntranceRoom.drawPrecludedCeilingTiles = false;
            Expand_SecretElevatorEntranceRoom.preventBorders = false;
            Expand_SecretElevatorEntranceRoom.preventFacewallAO = false;
            Expand_SecretElevatorEntranceRoom.usesCustomAmbientLight = false;
            Expand_SecretElevatorEntranceRoom.customAmbientLight = Color.white;
            Expand_SecretElevatorEntranceRoom.ForceAllowDuplicates = false;
            Expand_SecretElevatorEntranceRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_SecretElevatorEntranceRoom.IsLostWoodsRoom = false;
            Expand_SecretElevatorEntranceRoom.UseCustomMusic = false;
            Expand_SecretElevatorEntranceRoom.UseCustomMusicState = false;
            Expand_SecretElevatorEntranceRoom.CustomMusicEvent = string.Empty;
            Expand_SecretElevatorEntranceRoom.UseCustomMusicSwitch = false;
            Expand_SecretElevatorEntranceRoom.CustomMusicSwitch = string.Empty;
            Expand_SecretElevatorEntranceRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_SecretElevatorEntranceRoom.rewardChestSpawnPosition = new IntVector2(4, 4);
            Expand_SecretElevatorEntranceRoom.Width = 10;
            Expand_SecretElevatorEntranceRoom.Height = 10;            
            Expand_SecretElevatorEntranceRoom.usesProceduralDecoration = true;
            Expand_SecretElevatorEntranceRoom.usesProceduralLighting = true;
            Expand_SecretElevatorEntranceRoom.allowFloorDecoration = false;
            Expand_SecretElevatorEntranceRoom.associatedMinimapIcon = ExpandPrefabs.elevator_maintenance_room.associatedMinimapIcon;
            RoomFromText.AddExitToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(0, 4), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(11, 4), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(5, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(1, 9), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(9, 9), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(3, 8), NonEnemyBehaviour: ExpandSecretDoorPrefabs.EXSecretDoor_Normal.GetComponent<ExpandSecretDoorPlacable>());
            RoomFromText.GenerateRoomFromText(Expand_SecretElevatorEntranceRoom, "RoomCellData.Expand_SecretElevatorEntranceRoom_Layout.txt");

            // This will share same layout as it's entrance version.
            Expand_SecretElevatorDestinationRoom.name = "Destination MiniElevator Room";
            Expand_SecretElevatorDestinationRoom.QAID = "AA" + UnityEngine.Random.Range(1000, 9999);
            Expand_SecretElevatorDestinationRoom.GUID = Guid.NewGuid().ToString();
            Expand_SecretElevatorDestinationRoom.PreventMirroring = false;
            Expand_SecretElevatorDestinationRoom.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_SecretElevatorDestinationRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_SecretElevatorDestinationRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_SecretElevatorDestinationRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_SecretElevatorDestinationRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_SecretElevatorDestinationRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_SecretElevatorDestinationRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_SecretElevatorDestinationRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_SecretElevatorDestinationRoom.placedObjectPositions = new List<Vector2>();
            Expand_SecretElevatorDestinationRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_SecretElevatorDestinationRoom.roomEvents = new List<RoomEventDefinition>(0);
            Expand_SecretElevatorDestinationRoom.overriddenTilesets = 0;
            Expand_SecretElevatorDestinationRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_SecretElevatorDestinationRoom.InvalidInCoop = false;
            Expand_SecretElevatorDestinationRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_SecretElevatorDestinationRoom.preventAddedDecoLayering = false;
            Expand_SecretElevatorDestinationRoom.precludeAllTilemapDrawing = false;
            Expand_SecretElevatorDestinationRoom.drawPrecludedCeilingTiles = false;
            Expand_SecretElevatorDestinationRoom.preventBorders = false;
            Expand_SecretElevatorDestinationRoom.preventFacewallAO = false;
            Expand_SecretElevatorDestinationRoom.usesCustomAmbientLight = false;
            Expand_SecretElevatorDestinationRoom.customAmbientLight = Color.white;
            Expand_SecretElevatorDestinationRoom.ForceAllowDuplicates = false;
            Expand_SecretElevatorDestinationRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_SecretElevatorDestinationRoom.IsLostWoodsRoom = false;
            Expand_SecretElevatorDestinationRoom.UseCustomMusic = false;
            Expand_SecretElevatorDestinationRoom.UseCustomMusicState = false;
            Expand_SecretElevatorDestinationRoom.CustomMusicEvent = string.Empty;
            Expand_SecretElevatorDestinationRoom.UseCustomMusicSwitch = false;
            Expand_SecretElevatorDestinationRoom.CustomMusicSwitch = string.Empty;
            Expand_SecretElevatorDestinationRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_SecretElevatorDestinationRoom.rewardChestSpawnPosition = new IntVector2(4, 4);
            Expand_SecretElevatorDestinationRoom.Width = 10;
            Expand_SecretElevatorDestinationRoom.Height = 10;
            Expand_SecretElevatorDestinationRoom.usesProceduralDecoration = true;
            Expand_SecretElevatorDestinationRoom.usesProceduralLighting = true;
            Expand_SecretElevatorDestinationRoom.allowFloorDecoration = false;
            Expand_SecretElevatorDestinationRoom.associatedMinimapIcon = ExpandPrefabs.elevator_maintenance_room.associatedMinimapIcon;
            RoomFromText.AddExitToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(0, 4), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(11, 4), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(5, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(1, 9), DungeonData.Direction.NORTH);
            RoomFromText.AddExitToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(9, 9), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(3, 8), NonEnemyBehaviour: ExpandSecretDoorPrefabs.EXSecretDoorDestination.GetComponent<ExpandSecretDoorExitPlacable>());
            RoomFromText.GenerateRoomFromText(Expand_SecretElevatorDestinationRoom, "RoomCellData.Expand_SecretElevatorEntranceRoom_Layout.txt");



            Expand_TinySecret.name = "Expand Apache Tiny Secret";
            Expand_TinySecret.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_TinySecret.GUID = Guid.NewGuid().ToString();
            Expand_TinySecret.PreventMirroring = false;
            Expand_TinySecret.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_TinySecret.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_TinySecret.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_TinySecret.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_TinySecret.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_TinySecret.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_TinySecret.pits = new List<PrototypeRoomPitEntry>();
            Expand_TinySecret.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_TinySecret.placedObjectPositions = new List<Vector2>();
            Expand_TinySecret.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_TinySecret.roomEvents = new List<RoomEventDefinition>(0);
            Expand_TinySecret.overriddenTilesets = 0;
            Expand_TinySecret.prerequisites = new List<DungeonPrerequisite>();
            Expand_TinySecret.InvalidInCoop = false;
            Expand_TinySecret.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_TinySecret.preventAddedDecoLayering = false;
            Expand_TinySecret.precludeAllTilemapDrawing = false;
            Expand_TinySecret.drawPrecludedCeilingTiles = false;
            Expand_TinySecret.preventBorders = false;
            Expand_TinySecret.preventFacewallAO = false;
            Expand_TinySecret.usesCustomAmbientLight = false;
            Expand_TinySecret.customAmbientLight = Color.white;
            Expand_TinySecret.ForceAllowDuplicates = false;
            Expand_TinySecret.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_TinySecret.IsLostWoodsRoom = false;
            Expand_TinySecret.UseCustomMusic = false;
            Expand_TinySecret.UseCustomMusicState = false;
            Expand_TinySecret.CustomMusicEvent = string.Empty;
            Expand_TinySecret.UseCustomMusicSwitch = false;
            Expand_TinySecret.CustomMusicSwitch = string.Empty;
            Expand_TinySecret.overrideRoomVisualTypeForSecretRooms = false;
            Expand_TinySecret.rewardChestSpawnPosition = new IntVector2(10, 12);
            Expand_TinySecret.overrideRoomVisualType = -1;
            Expand_TinySecret.Width = 2;
            Expand_TinySecret.Height = 2;
            Expand_TinySecret.allowFloorDecoration = false;
            Expand_TinySecret.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);            
            RoomFromText.AddExitToRoom(Expand_TinySecret, new Vector2(0, 1), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_TinySecret, new Vector2(3, 1), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_TinySecret, new Vector2(1, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_TinySecret, new Vector2(1, 3), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_TinySecret, new Vector2(0, 0), ExpandPrefabs.TinySecretRoomJunkReward, xOffset: 1, yOffset: 10);
            RoomFromText.AddObjectToRoom(Expand_TinySecret, new Vector2(1, 0), ExpandPrefabs.TinySecretRoomRewards, yOffset: 10);
            RoomFromText.GenerateDefaultRoomLayout(Expand_TinySecret);

            Expand_GlitchedSecret.name = "Expand Apache Corrupted Secret";
            Expand_GlitchedSecret.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_GlitchedSecret.GUID = Guid.NewGuid().ToString();
            Expand_GlitchedSecret.PreventMirroring = false;
            Expand_GlitchedSecret.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_GlitchedSecret.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_GlitchedSecret.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_GlitchedSecret.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_GlitchedSecret.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_GlitchedSecret.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_GlitchedSecret.pits = new List<PrototypeRoomPitEntry>();
            Expand_GlitchedSecret.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_GlitchedSecret.placedObjectPositions = new List<Vector2>();
            Expand_GlitchedSecret.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_GlitchedSecret.roomEvents = new List<RoomEventDefinition>(0);
            Expand_GlitchedSecret.overriddenTilesets = 0;
            Expand_GlitchedSecret.prerequisites = new List<DungeonPrerequisite>();
            Expand_GlitchedSecret.InvalidInCoop = false;
            Expand_GlitchedSecret.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_GlitchedSecret.preventAddedDecoLayering = false;
            Expand_GlitchedSecret.precludeAllTilemapDrawing = false;
            Expand_GlitchedSecret.drawPrecludedCeilingTiles = false;
            Expand_GlitchedSecret.preventBorders = false;
            Expand_GlitchedSecret.preventFacewallAO = false;
            Expand_GlitchedSecret.usesCustomAmbientLight = false;
            Expand_GlitchedSecret.customAmbientLight = Color.white;
            Expand_GlitchedSecret.ForceAllowDuplicates = false;
            Expand_GlitchedSecret.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_GlitchedSecret.IsLostWoodsRoom = false;
            Expand_GlitchedSecret.UseCustomMusic = false;
            Expand_GlitchedSecret.UseCustomMusicState = false;
            Expand_GlitchedSecret.CustomMusicEvent = string.Empty;
            Expand_GlitchedSecret.UseCustomMusicSwitch = false;
            Expand_GlitchedSecret.CustomMusicSwitch = string.Empty;
            Expand_GlitchedSecret.overrideRoomVisualTypeForSecretRooms = false;
            Expand_GlitchedSecret.rewardChestSpawnPosition = new IntVector2(8, 8);
            Expand_GlitchedSecret.overrideRoomVisualType = -1;
            Expand_GlitchedSecret.Width = 16;
            Expand_GlitchedSecret.Height = 16;
            Expand_GlitchedSecret.allowFloorDecoration = false;
            Expand_GlitchedSecret.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            placeableContents = sharedAssets2.LoadAsset<DungeonPlaceable>("secret_room_chest_placeable"),
                            contentsBasePosition = new Vector2(11, 3),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = sharedAssets2.LoadAsset<DungeonPlaceable>("secret_room_chest_placeable"),
                            contentsBasePosition = new Vector2(10, 8),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = sharedAssets2.LoadAsset<DungeonPlaceable>("secret_room_chest_placeable"),
                            contentsBasePosition = new Vector2(2, 12),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = ExpandPrefabs.TinySecretRoomRewards,
                            contentsBasePosition = new Vector2(4, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = ExpandPrefabs.TinySecretRoomJunkReward,
                            contentsBasePosition = new Vector2(5, 4),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = ExpandPrefabs.TinySecretRoomRewards,
                            contentsBasePosition = new Vector2(9, 6),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = ExpandPrefabs.TinySecretRoomJunkReward,
                            contentsBasePosition = new Vector2(4, 9),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = ExpandPrefabs.TinySecretRoomRewards,
                            contentsBasePosition = new Vector2(10, 13),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = ExpandUtility.GenerateDungeonPlacable(objectDatabase.NPCMonsterManuel, useExternalPrefab: true),
                            contentsBasePosition = new Vector2(2, 6),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = ExpandUtility.GenerateDungeonPlacable(objectDatabase.NPCVampire, useExternalPrefab: true),
                            contentsBasePosition = new Vector2(9, 10),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = ExpandUtility.GenerateDungeonPlacable(objectDatabase.NPCMonsterManuel, useExternalPrefab: true, spawnChance: 0.4f),
                            contentsBasePosition = new Vector2(6, 13),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            placeableContents = ExpandPrefabs.CorruptedSecretRoomSpecialItem,
                            contentsBasePosition = new Vector2(14, 14),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(11, 3),
                        new Vector2(10, 8),
                        new Vector2(2, 12),
                        new Vector2(4, 2),
                        new Vector2(5, 4),
                        new Vector2(9, 6),
                        new Vector2(4, 9),
                        new Vector2(10, 13),
                        new Vector2(2, 6),
                        new Vector2(9, 10),
                        new Vector2(6, 13),
                        new Vector2(14, 14)
                    },
                    layerIsReinforcementLayer = false,
                    shuffle = true,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 1,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomFromText.AddExitToRoom(Expand_GlitchedSecret, new Vector2(0, 8), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(Expand_GlitchedSecret, new Vector2(17, 8), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(Expand_GlitchedSecret, new Vector2(8, 0), DungeonData.Direction.SOUTH);
            RoomFromText.AddExitToRoom(Expand_GlitchedSecret, new Vector2(8, 17), DungeonData.Direction.NORTH);
            RoomFromText.AddObjectToRoom(Expand_GlitchedSecret, new Vector2(8, 8), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RoomCorruptionAmbience, useExternalPrefab: true));
            RoomFromText.GenerateDefaultRoomLayout(Expand_GlitchedSecret);



            WeightedRoom[] CustomTrapRooms = new WeightedRoom[] {
                GenerateWeightedRoom(ThwompCrossingVertical),
                GenerateWeightedRoom(ThwompCrossingHorizontal)
            };

            foreach (WeightedRoom room in CustomTrapRooms) {
                ExpandPrefabs.CastleRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.Gungeon_RoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.CatacombsRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.ForgeRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.CustomRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.CustomRoomTable2.includedRooms.elements.Add(room);
                ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(room);
            }

            WeightedRoom[] CustomSecretRooms = new WeightedRoom[] {
                GenerateWeightedRoom(Expand_TinySecret, 8),
                GenerateWeightedRoom(Expand_GlitchedSecret, 2)
            };

            WeightedRoom[] CustomCastleRooms = new WeightedRoom[] {
                GenerateWeightedRoom(Expand_Explode),
                GenerateWeightedRoom(Expand_C_Hub),
                GenerateWeightedRoom(Expand_C_Gap),
                GenerateWeightedRoom(Expand_ChainGap),
                GenerateWeightedRoom(Expand_Challange1),
                GenerateWeightedRoom(Expand_Pit_Line),
                GenerateWeightedRoom(Expand_Singer_Gap),
                GenerateWeightedRoom(Expand_Flying_Gap),
                GenerateWeightedRoom(Expand_Battle),
                GenerateWeightedRoom(Expand_Cross),
                GenerateWeightedRoom(Expand_Blocks),
                GenerateWeightedRoom(Expand_Blocks_Pits),
                GenerateWeightedRoom(Expand_Wall_Pit),
                GenerateWeightedRoom(Expand_Gate_Cross),
                GenerateWeightedRoom(Expand_Passage),
                GenerateWeightedRoom(Expand_Pit_Jump),
                GenerateWeightedRoom(Expand_Pit_Passage),
                GenerateWeightedRoom(Expand_R_Blocks),
                GenerateWeightedRoom(Expand_Small_Passage),
                GenerateWeightedRoom(Expand_Box),
                GenerateWeightedRoom(Expand_Steps),
                GenerateWeightedRoom(Expand_Spiral),
                GenerateWeightedRoom(Expand_Apache_Hub),
                GenerateWeightedRoom(Expand_Box_Hub),
                GenerateWeightedRoom(Expand_Enclose_Hub)
            };

            WeightedRoom[] CustomHollowsRooms = new WeightedRoom[] {
                GenerateWeightedRoom(Expand_SpiderMaze)
            };

            foreach (WeightedRoom room in CustomCastleRooms) {
                ExpandPrefabs.CastleRoomTable.includedRooms.elements.Add(room);
                if (room.room.overrideRoomVisualType == -1) {
                    ExpandPrefabs.CustomRoomTable.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTable2.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(room);
                }
            }
            foreach (WeightedRoom room in CustomHollowsRooms) {
                ExpandPrefabs.CatacombsRoomTable.includedRooms.elements.Add(room);
                if (room.room.overrideRoomVisualType == -1) {
                    ExpandPrefabs.CustomRoomTable.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTable2.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(room);
                }
            }
            foreach (WeightedRoom room in CustomSecretRooms) {
                ExpandPrefabs.SecretRoomTable.includedRooms.elements.Add(room);
            }
            
            objectDatabase = null;
            sharedAssets = null;
            sharedAssets2 = null;
        }
    }
}

