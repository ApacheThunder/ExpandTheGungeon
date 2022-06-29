using Dungeonator;
using System;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandDungeonFlows;
using System.Reflection;
using FullInspector;
using ExpandTheGungeon.SpriteAPI;

namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandPrefabs {
     
        // Custom Sprite Collections (this gets setup before ItemAPI
        public static GameObject EXItemCollection;
        public static GameObject EXGunCollection;
        public static GameObject EXChestCollection;
        public static GameObject EXTrapCollection;
        public static GameObject EXJungleCollection;
        public static GameObject EXParadropCollection;
        public static GameObject EXLargeMonsterCollection;
        public static GameObject EXMonsterCollection;
        public static GameObject EXSecretDoorCollection;
        public static GameObject EXBootlegRoomCollection;
        public static GameObject SecretElevatorExitTilesetCollection;
        public static GameObject EXBalloonCollection;
        public static GameObject EXPortableElevatorCollection;
        public static GameObject EXOfficeCollection;
        public static GameObject EXSpaceCollection;
        public static GameObject EXFoyerCollection;

        // Materials
        public static Material SpaceFog;        

        // Custom Textures
        public static Texture2D BulletManMonochromeTexture;
        public static Texture2D BulletManUpsideDownTexture;
        
        // Rat Trap Door
        public static GameObject RatTrapdoor;

        // Room Prefabs
        public static PrototypeDungeonRoom shop02;
        public static PrototypeDungeonRoom fusebombroom01;
        public static PrototypeDungeonRoom elevator_entrance;
        public static PrototypeDungeonRoom gungeon_entrance;
        public static PrototypeDungeonRoom gungeon_entrance_bossrush;
        public static PrototypeDungeonRoom elevator_maintenance_room;
        public static PrototypeDungeonRoom test_entrance;
        public static PrototypeDungeonRoom exit_room_basic;
        public static PrototypeDungeonRoom boss_foyer;
        public static PrototypeDungeonRoom gungeon_rewardroom_1;
        public static PrototypeDungeonRoom paradox_04;
        public static PrototypeDungeonRoom paradox_04_copy;
        public static PrototypeDungeonRoom blobulordroom01;
        public static PrototypeDungeonRoom doublebeholsterroom01;
        public static PrototypeDungeonRoom bossstatuesroom01;
        public static PrototypeDungeonRoom oldbulletking_room_01;
        public static PrototypeDungeonRoom DragunBossFoyerRoom;
        public static PrototypeDungeonRoom DraGunRoom01;
        public static PrototypeDungeonRoom DraGunExitRoom;
        public static PrototypeDungeonRoom DraGunEndTimesRoom;
        public static PrototypeDungeonRoom BlacksmithShop;
        public static PrototypeDungeonRoom GatlingGullRoom05;
        public static PrototypeDungeonRoom letsgetsomeshrines_001;
        public static PrototypeDungeonRoom shop_special_key_01;
        public static PrototypeDungeonRoom square_hub;
        public static PrototypeDungeonRoom subshop_muncher_01;
        public static PrototypeDungeonRoom black_market;
        public static PrototypeDungeonRoom gungeon_checkerboard;
        public static PrototypeDungeonRoom gungeon_normal_fightinaroomwithtonsoftraps;
        public static PrototypeDungeonRoom gungeon_gauntlet_001;

        // Secret rooms from Rat Trap door
        public static PrototypeDungeonRoom ResourcefulRat_LongMinecartRoom_01;
        public static PrototypeDungeonRoom ResourcefulRat_FirstSecretRoom_01;
        public static PrototypeDungeonRoom ResourcefulRat_SecondSecretRoom_01;

        // Rat Floor Entrance Room
        public static PrototypeDungeonRoom ResourcefulRat_Entrance;

        // Unused Rat Floor Entrance room from Sewers
        public static PrototypeDungeonRoom SewersRatExitEoom;

        // Modified Room Prefabs
        public static PrototypeDungeonRoom tiny_entrance;
        public static PrototypeDungeonRoom tiny_exit;
        public static PrototypeDungeonRoom reward_room;
        public static PrototypeDungeonRoom tutorial_minibossroom;
        public static PrototypeDungeonRoom bossrush_alternate_entrance;
        public static PrototypeDungeonRoom tutorial_fakeboss;
        public static PrototypeDungeonRoom big_entrance;
        public static PrototypeDungeonRoom Hell_Hath_No_Joery_009;
        public static PrototypeDungeonRoom[] gatlinggull_noTileVisualOverrides;
        // Foyer Room
        // public static PrototypeDungeonRoom GungeonFoyer;

        // Exposed as a static array to insure changes to them stick.
        public static PrototypeDungeonRoom[] winchesterrooms;
        public static PrototypeDungeonRoom[] minibossrooms;

        // Array of Bonus chest rooms
        public static PrototypeDungeonRoom[] BonusChestRooms;


        // Room tables
        public static GenericRoomTable castle_challengeshrine_roomtable;
        public static GenericRoomTable catacombs_challengeshrine_roomtable;
        public static GenericRoomTable forge_challengeshrine_roomtable;
        public static GenericRoomTable gungeon_challengeshrine_roomtable;
        public static GenericRoomTable mines_challengeshrine_roomtable;
        public static GenericRoomTable shop_room_table;
        public static GenericRoomTable CastleRoomTable;
        public static GenericRoomTable Gungeon_RoomTable;
        public static GenericRoomTable SecretRoomTable;
        public static GenericRoomTable bosstable_02_beholster;
        public static GenericRoomTable bosstable_01_bulletbros;
        public static GenericRoomTable bosstable_01_bulletking;
        public static GenericRoomTable bosstable_01_gatlinggull;
        public static GenericRoomTable bosstable_02_meduzi;
        public static GenericRoomTable bosstable_02a_highpriest;
        public static GenericRoomTable bosstable_03_mineflayer;
        public static GenericRoomTable bosstable_03_powderskull;
        public static GenericRoomTable bosstable_03_tank;
        public static GenericRoomTable bosstable_04_demonwall;
        public static GenericRoomTable bosstable_04_statues;
        public static GenericRoomTable blocknerminiboss_table_01;
        public static GenericRoomTable phantomagunim_table_01;
        public static GenericRoomTable basic_special_rooms;
        public static GenericRoomTable winchesterroomtable;
        public static GenericRoomTable boss_foyertable;

        // Dungeon Specific Room Tables (from Dungeon AssetBundles)
        public static GenericRoomTable SewersRoomTable;
        public static GenericRoomTable AbbeyRoomTable;
        public static GenericRoomTable MinesRoomTable;
        public static GenericRoomTable CatacombsRoomTable;
        public static GenericRoomTable ForgeRoomTable;
        public static GenericRoomTable BulletHellRoomTable;
        

        // Custom Room Tables
        public static GenericRoomTable CastleGungeonMergedTable;
        public static GenericRoomTable CustomRoomTable;
        public static GenericRoomTable CustomRoomTable2;
        public static GenericRoomTable CustomRoomTableSecretGlitchFloor;
        public static GenericRoomTable MegaBossRoomTable;        
        public static GenericRoomTable MegaChallengeShrineTable;
        public static GenericRoomTable MegaMiniBossRoomTable;
        public static GenericRoomTable basic_special_rooms_noBlackMarket;
        public static GenericRoomTable bosstable_01_gatlinggull_custom;
        public static GenericRoomTable AbbeyAblernRoomTable;
        public static GenericRoomTable JungleRoomTable;
        public static GenericRoomTable BellyRoomTable;
        public static GenericRoomTable WestRoomTable;
        public static GenericRoomTable WestCanyonRoomTable;
        public static GenericRoomTable WestTinyCanyonRoomTable;
        public static GenericRoomTable WestInterior1RoomTable;
        public static GenericRoomTable AbbeyRoomTableForOffice;

        public static WeightedRoom[] OfficeAndUnusedWeightedRooms;


        // Modified Loot tables
        public static GenericLootTable Shop_Key_Items_01;
        public static GenericLootTable BlackSmith_Items_01;
        public static GenericLootTable Shop_Truck_Items_01;
        public static GenericLootTable Shop_Curse_Items_01;

        // Modified Flow Injection Data
        public static ProceduralFlowModifierData AbbeyFlowModifierData;


        // Object Prefabs
        public static GameObject MetalGearRatPrefab;
        private static GameObject ResourcefulRatBossPrefab;


        public static GameObject MimicNPC;
        public static GameObject RatCorpseNPC;
        public static GameObject PlayerLostRatNote;
        public static GameObject MouseTrap1;
        public static GameObject MouseTrap2;
        public static GameObject MouseTrap3;
        // Custom Trap Objects
        public static GameObject EXTrap_Apache;


        // Room Tileset sprite object for Exit room that leads to Old West
        public static GameObject SecretElevatorExitTileset;

        public static GameObject Teleporter_Gungeon_01;
        public static GameObject ElevatorMaintanenceRoomIcon;
        public static GameObject Teleporter_Info_Sign;
        public static GameObject RewardPedestalPrefab;
        public static GameObject Minimap_Maintenance_Icon;

        // Forge Hammer. Used by Baby Good hammer
        public static GameObject ForgeHammer;

        // Use for Arrival location for destination rooms setup by TargetPitFallRoom
        public static GameObject Arrival;

        public static GameObject NPCBabyDragunChaos;

        // DungeonPlacables
        public static DungeonPlaceable ElevatorDeparture;
        public static DungeonPlaceable ElevatorArrival;

        // Custom Placables
        public static DungeonPlaceable TinySecretRoomRewards;
        public static DungeonPlaceable TinySecretRoomJunkReward;
        public static DungeonPlaceable RatTrapPlacable;
        public static DungeonPlaceable CorruptedSecretRoomSpecialItem;
        public static DungeonPlaceable Jungle_Doors;
        public static DungeonPlaceable Jungle_OneWayDoors;
        public static DungeonPlaceable Belly_Doors;
        public static DungeonPlaceable West_Doors;
        public static DungeonPlaceable Office_OneWayDoors;

        // Modified/Reference AIActors
        public static GameObject MetalCubeGuy;
        public static GameObject SerManuel;
        public static GameObject SkusketHead;
        public static GameObject CandleGuy;
        public static GameObject WallMimic;
        public static GameObject AK47BulletKin;

        public static GameObject RatJailDoor;
        public static GameObject CurrsedMirror;

        // Used for forcing Arrival Elevator to spawn on phobos floor tileset ID.
        public static DungeonPlaceableVariant ElevatorArrivalVarientForUnknownTilesets;
        public static DungeonPlaceableVariant ElevatorArrivalVarientForOffice; 
        public static DungeonPlaceableVariant ElevatorArrivalVarientForJungle;
        public static DungeonPlaceableVariant ElevatorArrivalVarientForBelly;
        public static DungeonPlaceableVariant ElevatorArrivalVarientForOldWest;
        public static DungeonPlaceableVariant ElevatorArrivalVarientForPhobos;
        public static DungeonPlaceableVariant ElevatorArrivalVarientForSpace;
        // New Departure Elevator for all custom/unused tilesets.
        public static DungeonPlaceableVariant ElevatorDepartureVarientForOffice;        
        public static DungeonPlaceableVariant ElevatorDepartureVarientForJungle;
        public static DungeonPlaceableVariant ElevatorDepartureVarientForBelly;
        public static DungeonPlaceableVariant ElevatorDepartureVarientForOldWest;
        public static DungeonPlaceableVariant ElevatorDepartureVarientForPhobos;
        public static DungeonPlaceableVariant ElevatorDepartureVarientForSpace;

        // Modified Challenge Modifiers/Challenge Objects
        public static GameObject ChallengeManagerObject;
        public static GameObject ChallengeMegaManagerObject;
        public static FlameTrapChallengeModifier RatsRevengChallenge;
        public static GameObject Challenge_BlobulinAmmo;
        public static GameObject Challenge_BooRoom;
        public static GameObject Challenge_ZoneControl;

        // Custom Objects
        public static GameObject DoppelgunnerMirror;
        public static GameObject DoppelgunnerMirrorFX;
        public static GameObject RoomCorruptionAmbience;
        public static GameObject EXAlarmMushroom;
        public static GameObject EXSawBladeTrap_4x4Zone;
        public static GameObject EXFriendlyForgeHammer;
        public static GameObject EXFriendlyForgeHammerBullet;
        public static GameObject EXBootlegRoomObject;
        public static GameObject EXBootlegRoomDoorTriggers;
        public static GameObject RickRollChestObject;
        public static GameObject RickRollAnimationObject;
        public static GameObject RickRollMusicSwitchObject;
        public static GameObject SurpriseChestObject;
        public static GameObject ExpandThunderstormPlaceable;
        public static GameObject Door_Horizontal_Jungle;
        public static GameObject Door_Vertical_Jungle;
        public static GameObject DoorOneWay_Horizontal_Jungle;
        public static GameObject DoorOneWay_Vertical_Jungle;
        public static GameObject Jungle_LargeTree;
        public static GameObject Jungle_LargeTreeTopFrame;
        public static GameObject EXJungleTree_MinimapIcon;
        public static GameObject EXJungleCrest_MinimapIcon;
        public static GameObject Jungle_ExitLadder;
        public static GameObject Jungle_ExitLadder_Destination;
        public static GameObject Jungle_ExitLadder_Hole;
        public static GameObject Jungle_BlobLostSign;
        public static GameObject Jungle_ItemStump;
        public static GameObject Door_Horizontal_Belly;
        public static GameObject Door_Vertical_Belly;
        public static GameObject Belly_ExitWarp;
        public static GameObject Belly_ExitRoomIcon;
        public static GameObject Belly_DoorAnimations;
        public static GameObject Belly_Shipwreck_Left;
        public static GameObject Belly_Shipwreck_Right;
        public static GameObject Door_Horizontal_West;
        public static GameObject Door_Vertical_West;
        public static GameObject West_PuzzleSetupPlacable;
        public static GameObject EXOldWestWarp;
        public static GameObject EXSpaceFloor_50x50;
        public static GameObject EXSpaceFloorPitBorder_50x50;
        public static GameObject DoorOneWay_Vertical_Office;
        public static GameObject DoorOneWay_Horizontal_Office;
        
        // Sarcophagus Objects with Kaliber sprites set.
        public static GameObject Sarcophagus_ShotgunBook_Kaliber;
        public static GameObject Sarcophagus_ShotgunMace_Kaliber;
        public static GameObject Sarcophagus_BulletSword_Kaliber;
        public static GameObject Sarcophagus_BulletShield_Kaliber;

        // Belly Entrance Room stuff
        public static GameObject Sarco_WoodShieldPedestal;
        public static GameObject Sarco_Door;
        public static GameObject Sarco_Floor;
        
        public static GameObject Sarco_MonsterObject;
        public static GameObject Sarco_Skeleton;
        
        // Belly Pit VFX to account for non functional pit bubble animations. (tileset sprites seems to suggets it has one but the animations don't work?)
        public static GameObject Belly_PitVFX1;
        public static GameObject Belly_PitVFX2;
        public static GameObject Belly_PitVFX3;

        // Modified Nakatomi Light to match the one Jungle used
        public static GameObject JungleLight;
        // Belly Light prefabs for Belly DungeonMaterial
        public static GameObject BellyLight;
        // West Light
        public static GameObject WestLight;
        // Phobos Light
        public static GameObject PhobosLight;

        // Cactus Objects for West
        public static GameObject Cactus_A;
        public static GameObject Cactus_B;
        public static GameObject CactusShard1;
        public static GameObject CactusShard2;

        // Custom Reward Pedestals (using custom component)
        public static GameObject BlankRewardPedestal;
        public static GameObject RatKeyRewardPedestal;
        public static GameObject CorruptionBombRewardPedestal; // Reward Pedestal for corrupted secret room.

        // Glitch Portal Object
        public static GameObject EX_GlitchPortal;

        // ParaDrop animation assets
        public static GameObject EX_Parachute;
        public static GameObject EX_ParadropAnchor;
        public static GameObject EX_ExplodyBarrelDummy;
        public static GameObject EX_ItemDropper;

        // Custom Chest used on West for secret puzzle
        public static GameObject EX_Chest_West;

        // Custom Balloon Objects (based off balloon used for robot arm side quest)
        public static GameObject EX_RedBalloon;
        public static GameObject EX_BlueBalloon;
        public static GameObject EX_GreenBalloon;
        public static GameObject EX_PinkBalloon;
        public static GameObject EX_YellowBalloon;

        // Custom Elevator for Portable Elevator Item
        public static GameObject EXPortableElevator_Departure;
        public static GameObject EXPortableElevator_Reticle;
        // Placable versions of Elevator for exit rooms
        public static GameObject EXPortableElevator_Departure_Placable;
        public static GameObject EXJungleElevator_Departure_Placable;
        // Arrival version of placable version of portable elevator
        public static GameObject EXElevator_Arrival_Placable;

        // Custom Dungeon Sprite Collection Objects. (now loaded via custom asset bundle! These aren't fake prefabs!)
        public static GameObject ENV_Tileset_Jungle;
        public static GameObject ENV_Tileset_Belly;
        public static GameObject ENV_Tileset_West;
        public static GameObject ENV_Tileset_Phobos;
        public static GameObject ENV_Tileset_Office;

        // Grass sprites conveted to objects from "space" tileset (unused tileset used by all the pasts)
        public static GameObject EXSpace_Grass_01;
        public static GameObject EXSpace_Grass_02;
        public static GameObject EXSpace_Grass_03;
        public static GameObject EXSpace_Grass_04;
        
        // Custom Challenge Modifiers
        public static GameObject Challenge_ChaosMode;
        public static GameObject Challenge_TripleTrouble;
        public static GameObject Challenge_KingsMen;

        // Modified Items
        public static GameObject ChamberGun;


        // More Prefabs. (setup in second CS file to avoid lag due to size of main CS file)
        public static GameObject EXRatDoor_4xLocks;

        // Custom Foyer stuff
        public static GameObject EXFoyerTrigger;
        public static GameObject EXFoyerWarpDoor;
        public static GameObject EXCasinoHub;
        public static GameObject EXPunchoutArcadeCoin;
        public static GameObject EXArcadeGame_Prop;
        public static GameObject EXArcadeGame_Prop_Depressed;
        public static GameObject EXCasino_HatRack;
        public static GameObject EXCasino_Litter_Cans;
        public static GameObject EXCasino_Litter_Paper;


        public static void InitSpriteCollections(AssetBundle expandSharedAssets1, AssetBundle sharedAssets) {
            ENV_Tileset_Jungle = expandSharedAssets1.LoadAsset<GameObject>("ENV_Tileset_Jungle");
            ENV_Tileset_Belly = expandSharedAssets1.LoadAsset<GameObject>("ENV_Tileset_Belly");
            ENV_Tileset_West = expandSharedAssets1.LoadAsset<GameObject>("ENV_Tileset_West");
            ENV_Tileset_Phobos = expandSharedAssets1.LoadAsset<GameObject>("ENV_Tileset_Phobos");
            ENV_Tileset_Office = expandSharedAssets1.LoadAsset<GameObject>("ENV_Tileset_Office");
            ExpandDungeonCollections.ENV_Tileset_Jungle(ENV_Tileset_Jungle, expandSharedAssets1.LoadAsset<Texture2D>("ENV_Tileset_Jungle"), sharedAssets, expandSharedAssets1);
            ExpandDungeonCollections.ENV_Tileset_Belly(ENV_Tileset_Belly, expandSharedAssets1.LoadAsset<Texture2D>("ENV_Tileset_Belly"), sharedAssets, expandSharedAssets1);
            ExpandDungeonCollections.ENV_Tileset_Phobos(ENV_Tileset_Phobos, expandSharedAssets1.LoadAsset<Texture2D>("ENV_Tileset_Phobos"), sharedAssets, expandSharedAssets1);
            ExpandDungeonCollections.ENV_Tileset_West(ENV_Tileset_West, expandSharedAssets1.LoadAsset<Texture2D>("ENV_Tileset_West"), sharedAssets, expandSharedAssets1);
            ExpandDungeonCollections.ENV_Tileset_Office(ENV_Tileset_Office, expandSharedAssets1.LoadAsset<Texture2D>("ENV_Tileset_Nakatomi"), sharedAssets, expandSharedAssets1);

            EXItemCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXItemCollection", "EXItem_Collection", "EXItemCollection");
            EXGunCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXGunCollection", "EXGun_Collection", "EXGunCollection");
            EXChestCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXChestCollection", "EXChest_Collection", "EXChestCollection");
            EXTrapCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXTrapCollection", "EXTrap_Collection", "EXTrapCollection");
            EXJungleCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXJungleCollection", "EXJungle_Collection", "EXJungleCollection");
            EXParadropCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXParadropCollection", "EXParadrop_Collection", "EXParadropCollection");
            EXLargeMonsterCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXLargeMonsterCollection", "EXLargeMonster_Collection", "EXLargeMonsterCollection");
            EXMonsterCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXMonsterCollection", "EXMonster_Collection", "EXMonsterCollection");
            EXSecretDoorCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXSecretDoorCollection", "EXSecretDoor_Collection", "EXSecretDoorCollection");
            EXBootlegRoomCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXBootlegRoomCollection", "EXBootlegRoom_Collection", "EXBootlegRoomCollection");
            SecretElevatorExitTilesetCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "SecretElevatorExitTilesetCollection", "SecretElevatorExitTileset_Collection", "SecretElevatorExitTilesetCollection");
            EXBalloonCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXBalloonCollection", "EXBalloon_Collection", "EXBalloonCollection");
            EXPortableElevatorCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXPortableElevatorCollection", "EXPortableElevator_Collection", "EXPortableElevatorCollection");
            EXOfficeCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXOfficeCollection", "EXOffice_Collection", "EXOfficeCollection");
            EXSpaceCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXSpaceCollection", "EXSpace_Collection", "EXSpaceCollection");
            EXFoyerCollection = SpriteSerializer.DeserializeSpriteCollectionFromAssetBundle(expandSharedAssets1, "EXFoyerCollection", "EXFoyer_Collection", "EXFoyerCollection");

            tk2dSpriteCollectionData gunCollection = EXGunCollection.GetComponent<tk2dSpriteCollectionData>();
            gunCollection.DefineProjectileCollision("bootleg_pistol_projectile_001", 8, 8, 4, 4, 0, 0);
        }

        public static void InitCustomPrefabs(AssetBundle expandSharedAssets1, AssetBundle sharedAssets, AssetBundle sharedAssets2, AssetBundle braveResources, AssetBundle enemiesBase) {

            Dungeon TutorialDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Tutorial");
            Dungeon CastleDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Castle");
            Dungeon GungeonDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Gungeon");
            Dungeon SewerDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Sewer");
            Dungeon MinesDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Mines");
            Dungeon ratDungeon = DungeonDatabase.GetOrLoadByName("base_resourcefulrat");
            Dungeon CathedralDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Cathedral");
            Dungeon BulletHellDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_BulletHell");
            Dungeon ForgeDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Forge");
            Dungeon CatacombsDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Catacombs");
            Dungeon NakatomiDungeonPrefab = DungeonDatabase.GetOrLoadByName("base_nakatomi");

            SpaceFog = PickupObjectDatabase.GetById(597).gameObject.GetComponent<GunParticleSystemController>().TargetSystem.gameObject.GetComponent<ParticleSystemRenderer>().materials[0];
            
            BulletManMonochromeTexture = expandSharedAssets1.LoadAsset<Texture2D>("BulletMan_Monochrome");
            BulletManUpsideDownTexture = expandSharedAssets1.LoadAsset<Texture2D>("BulletMan_UpsideDown");

            RatTrapdoor = MinesDungeonPrefab.RatTrapdoor;

            shop02 = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("shop02");
            fusebombroom01 = sharedAssets.LoadAsset<PrototypeDungeonRoom>("fusebombroom01");
            elevator_entrance = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("elevator entrance");
            gungeon_entrance = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("Gungeon Entrance");
            elevator_maintenance_room = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("ElevatorMaintenanceRoom");
            test_entrance = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("test entrance");
            exit_room_basic = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("exit_room_basic");
            boss_foyer = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("boss foyer");
            gungeon_rewardroom_1 = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("gungeon_rewardroom_1");
            paradox_04 = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("paradox_04");
            paradox_04_copy = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("paradox_04 copy");
            blobulordroom01 = sharedAssets.LoadAsset<PrototypeDungeonRoom>("BlobulordRoom01");
            doublebeholsterroom01 = ExpandDungeonFlow.LoadOfficialFlow("Secret_DoubleBeholster_Flow").AllNodes[2].overrideExactRoom;
            bossstatuesroom01 = sharedAssets.LoadAsset<PrototypeDungeonRoom>("bossstatuesroom01");
            oldbulletking_room_01 = sharedAssets.LoadAsset<PrototypeDungeonRoom>("oldbulletking_room_01");
            DragunBossFoyerRoom = ForgeDungeonPrefab.PatternSettings.flows[0].AllNodes[1].overrideExactRoom;
            DraGunRoom01 = sharedAssets.LoadAsset<PrototypeDungeonRoom>("dragunroom01");
            DraGunExitRoom = ForgeDungeonPrefab.PatternSettings.flows[0].AllNodes[3].overrideExactRoom;
            DraGunEndTimesRoom = ForgeDungeonPrefab.PatternSettings.flows[0].AllNodes[12].overrideExactRoom;
            BlacksmithShop = ForgeDungeonPrefab.PatternSettings.flows[0].AllNodes[10].overrideExactRoom;
            GatlingGullRoom05 = sharedAssets.LoadAsset<PrototypeDungeonRoom>("GatlingGullRoom05");
            letsgetsomeshrines_001 = sharedAssets.LoadAsset<PrototypeDungeonRoom>("letsgetsomeshrines_001");
            shop_special_key_01 = sharedAssets.LoadAsset<PrototypeDungeonRoom>("shop_special_key_01");
            square_hub = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("square hub");
            subshop_muncher_01 = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("subshop_muncher_01");
            black_market = sharedAssets.LoadAsset<PrototypeDungeonRoom>("Black Market");
            gungeon_checkerboard = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("gungeon_checkerboard");
            gungeon_normal_fightinaroomwithtonsoftraps = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("gungeon_normal_fightinaroomwithtonsoftraps");
            gungeon_gauntlet_001 = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("gungeon_gauntlet_001");

            BonusChestRooms = new PrototypeDungeonRoom[] {
                sharedAssets2.LoadAsset<PrototypeDungeonRoom>("lockedcellminireward_01"),
                sharedAssets2.LoadAsset<PrototypeDungeonRoom>("lockedcellminireward_02"),
                sharedAssets2.LoadAsset<PrototypeDungeonRoom>("lockedcellminireward_03"),
                sharedAssets2.LoadAsset<PrototypeDungeonRoom>("lockedcellminireward_04"),
                sharedAssets2.LoadAsset<PrototypeDungeonRoom>("lockedcellminireward_05"),
                sharedAssets2.LoadAsset<PrototypeDungeonRoom>("lockedcellminireward_06"),
                sharedAssets2.LoadAsset<PrototypeDungeonRoom>("lockedcellminireward_07"),
                sharedAssets2.LoadAsset<PrototypeDungeonRoom>("lockedcellminireward_08"),
                sharedAssets2.LoadAsset<PrototypeDungeonRoom>("lockedcellminireward_09"),
                sharedAssets2.LoadAsset<PrototypeDungeonRoom>("lockedcellminireward_10")
            };

            ResourcefulRat_LongMinecartRoom_01 = RatTrapdoor.GetComponent<ResourcefulRatMinesHiddenTrapdoor>().TargetMinecartRoom;
            ResourcefulRat_FirstSecretRoom_01 = RatTrapdoor.GetComponent<ResourcefulRatMinesHiddenTrapdoor>().FirstSecretRoom;
            ResourcefulRat_SecondSecretRoom_01 = RatTrapdoor.GetComponent<ResourcefulRatMinesHiddenTrapdoor>().SecondSecretRoom;

            ResourcefulRat_Entrance = ratDungeon.PatternSettings.flows[0].AllNodes[0].overrideExactRoom;
            // Remove arrival elevator...Why does it even exist for this floor. :P
            ResourcefulRat_Entrance.placedObjects[0].placeableContents = null;


            tiny_entrance = UnityEngine.Object.Instantiate(TutorialDungeonPrefab.PatternSettings.flows[0].AllNodes[4].overrideExactRoom);
            tiny_exit = UnityEngine.Object.Instantiate(TutorialDungeonPrefab.PatternSettings.flows[0].AllNodes[4].overrideExactRoom);
            reward_room = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("reward room");
            tutorial_minibossroom = UnityEngine.Object.Instantiate(TutorialDungeonPrefab.PatternSettings.flows[0].AllNodes[8].overrideExactRoom);
            bossrush_alternate_entrance = UnityEngine.Object.Instantiate(test_entrance);
            tutorial_fakeboss = UnityEngine.Object.Instantiate(DraGunRoom01);
            big_entrance = UnityEngine.Object.Instantiate(sharedAssets.LoadAsset<PrototypeDungeonRoom>("GatlingGullRoom05"));

            castle_challengeshrine_roomtable = sharedAssets.LoadAsset<GenericRoomTable>("castle_challengeshrine_roomtable");
            catacombs_challengeshrine_roomtable = sharedAssets.LoadAsset<GenericRoomTable>("catacombs_challengeshrine_roomtable");
            forge_challengeshrine_roomtable = sharedAssets.LoadAsset<GenericRoomTable>("forge_challengeshrine_roomtable");
            gungeon_challengeshrine_roomtable = sharedAssets.LoadAsset<GenericRoomTable>("gungeon_challengeshrine_roomtable");
            mines_challengeshrine_roomtable = sharedAssets.LoadAsset<GenericRoomTable>("mines_challengeshrine_roomtable");
            shop_room_table = sharedAssets2.LoadAsset<GenericRoomTable>("Shop Room Table");
            CastleRoomTable = sharedAssets2.LoadAsset<GenericRoomTable>("Castle_RoomTable");
            Gungeon_RoomTable = sharedAssets2.LoadAsset<GenericRoomTable>("Gungeon_RoomTable");
            SecretRoomTable = sharedAssets2.LoadAsset<GenericRoomTable>("secret_room_table_01");
            bosstable_02_beholster = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_02_beholster");
            bosstable_01_bulletbros = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_01_bulletbros");
            bosstable_01_bulletking = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_01_bulletking");
            bosstable_01_gatlinggull = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_01_gatlinggull");
            bosstable_02_meduzi = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_02_meduzi");
            bosstable_02a_highpriest = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_02a_highpriest");
            bosstable_03_mineflayer = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_03_mineflayer");
            bosstable_03_powderskull = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_03_powderskull");
            bosstable_03_tank = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_03_tank");
            bosstable_04_demonwall = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_04_demonwall");
            bosstable_04_statues = sharedAssets.LoadAsset<GenericRoomTable>("bosstable_04_statues");
            blocknerminiboss_table_01 = sharedAssets.LoadAsset<GenericRoomTable>("BlocknerMiniboss_Table_01");
            phantomagunim_table_01 = sharedAssets.LoadAsset<GenericRoomTable>("PhantomAgunim_Table_01");
            basic_special_rooms = sharedAssets.LoadAsset<GenericRoomTable>("basic special rooms (shrines, etc)");
            winchesterroomtable = sharedAssets.LoadAsset<GenericRoomTable>("winchesterroomtable");
            SewersRoomTable = SewerDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
            AbbeyRoomTable = CathedralDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
            MinesRoomTable = MinesDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
            CatacombsRoomTable = CatacombsDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
            ForgeRoomTable = ForgeDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
            BulletHellRoomTable = BulletHellDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
            boss_foyertable = sharedAssets2.LoadAsset<GenericRoomTable>("Boss Foyers");
            
            gungeon_entrance_bossrush = UnityEngine.Object.Instantiate(gungeon_entrance);
            gungeon_entrance_bossrush.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            gungeon_entrance_bossrush.name = "Bossrush Curse Shrine";
            gungeon_entrance_bossrush.associatedMinimapIcon = null;

            AbbeyFlowModifierData = CathedralDungeonPrefab.PatternSettings.flows[0].sharedInjectionData[1].InjectionData[0];
            AbbeyAblernRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();

            AbbeyAblernRoomTable.name = "Alburn Secret Rooms";
            AbbeyAblernRoomTable.includedRooms = new WeightedRoomCollection();
            AbbeyAblernRoomTable.includedRooms.elements = new List<WeightedRoom>();
            AbbeyAblernRoomTable.includedRoomTables = new List<GenericRoomTable>(0);

            AbbeyAblernRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(CathedralDungeonPrefab.PatternSettings.flows[0].sharedInjectionData[1].InjectionData[0].exactRoom));
            AbbeyFlowModifierData.exactRoom = null;
            AbbeyFlowModifierData.roomTable = AbbeyAblernRoomTable;
            JungleRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            JungleRoomTable.includedRooms = new WeightedRoomCollection();
            JungleRoomTable.includedRooms.elements = new List<WeightedRoom>();
            JungleRoomTable.includedRoomTables = new List<GenericRoomTable>(0);

            BellyRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            BellyRoomTable.includedRooms = new WeightedRoomCollection();
            BellyRoomTable.includedRooms.elements = new List<WeightedRoom>();
            BellyRoomTable.includedRoomTables = new List<GenericRoomTable>(0);

            WestRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            WestRoomTable.includedRooms = new WeightedRoomCollection();
            WestRoomTable.includedRooms.elements = new List<WeightedRoom>();
            WestRoomTable.includedRoomTables = new List<GenericRoomTable>(0);

            WestCanyonRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            WestCanyonRoomTable.includedRooms = new WeightedRoomCollection();
            WestCanyonRoomTable.includedRooms.elements = new List<WeightedRoom>();
            WestCanyonRoomTable.includedRoomTables = new List<GenericRoomTable>(0);

            WestTinyCanyonRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            WestTinyCanyonRoomTable.includedRooms = new WeightedRoomCollection();
            WestTinyCanyonRoomTable.includedRooms.elements = new List<WeightedRoom>();
            WestTinyCanyonRoomTable.includedRoomTables = new List<GenericRoomTable>(0);

            WestInterior1RoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            WestInterior1RoomTable.includedRooms = new WeightedRoomCollection();
            WestInterior1RoomTable.includedRooms.elements = new List<WeightedRoom>();
            WestInterior1RoomTable.includedRoomTables = new List<GenericRoomTable>(0);

            AbbeyRoomTableForOffice = ScriptableObject.CreateInstance<GenericRoomTable>();
            AbbeyRoomTableForOffice.name = "Office_RoomTable";
            AbbeyRoomTableForOffice.includedRooms = new WeightedRoomCollection();
            AbbeyRoomTableForOffice.includedRooms.elements = new List<WeightedRoom>();
            AbbeyRoomTableForOffice.includedRoomTables = AbbeyRoomTable.includedRoomTables;

            foreach (WeightedRoom room in AbbeyRoomTable.includedRooms.elements) {
                // room.room.FullCellData
                bool hasPits = false;
                foreach (PrototypeDungeonRoomCellData cellData in room.room.FullCellData) {
                    if (cellData.state == CellType.PIT) { hasPits = true; break; }
                }
                if (!hasPits) { AbbeyRoomTableForOffice.includedRooms.elements.Add(room); }
            }



            OfficeAndUnusedWeightedRooms = new WeightedRoom[] {
                ExpandRoomPrefabs.GenerateWeightedRoom(NakatomiDungeonPrefab.PatternSettings.flows[0].AllNodes[2].overrideExactRoom),
                ExpandRoomPrefabs.GenerateWeightedRoom(NakatomiDungeonPrefab.PatternSettings.flows[0].AllNodes[3].overrideExactRoom),
                ExpandRoomPrefabs.GenerateWeightedRoom(NakatomiDungeonPrefab.PatternSettings.flows[0].AllNodes[4].overrideExactRoom),
                ExpandRoomPrefabs.GenerateWeightedRoom(NakatomiDungeonPrefab.PatternSettings.flows[0].AllNodes[5].overrideExactRoom),
                ExpandRoomPrefabs.GenerateWeightedRoom(NakatomiDungeonPrefab.PatternSettings.flows[0].AllNodes[6].overrideExactRoom),
                ExpandRoomPrefabs.GenerateWeightedRoom(NakatomiDungeonPrefab.PatternSettings.flows[0].AllNodes[7].overrideExactRoom),
                ExpandRoomPrefabs.GenerateWeightedRoom(NakatomiDungeonPrefab.PatternSettings.flows[0].AllNodes[8].overrideExactRoom),
                ExpandRoomPrefabs.GenerateWeightedRoom(NakatomiDungeonPrefab.PatternSettings.flows[0].AllNodes[9].overrideExactRoom),
                ExpandRoomPrefabs.GenerateWeightedRoom(paradox_04),
                ExpandRoomPrefabs.GenerateWeightedRoom(paradox_04_copy)
            };

            Shop_Key_Items_01 = sharedAssets.LoadAsset<GenericLootTable>("Shop_Key_Items_01");
            Shop_Truck_Items_01 = sharedAssets.LoadAsset<GenericLootTable>("Shop_Truck_Items_01");
            BlackSmith_Items_01 = (BlacksmithShop.placedObjects[8].nonenemyBehaviour as BaseShopController).shopItemsGroup2;
            Shop_Curse_Items_01 = sharedAssets.LoadAsset<GenericLootTable>("Shop_Curse_Items_01");



            BlackSmith_Items_01.defaultItemDrops.Add(
                new WeightedGameObject() {
                    rawGameObject = null,
                    pickupId = BabyGoodHammer.HammerPickupID,
                    weight = 1,
                    forceDuplicatesPossible = false,
                    additionalPrerequisites = new DungeonPrerequisite[0],
                }
            );

            Shop_Key_Items_01.defaultItemDrops.Add(
                new WeightedGameObject() {
                    rawGameObject = null,
                    pickupId = TheLeadKey.TheLeadKeyPickupID,
                    weight = 1,
                    forceDuplicatesPossible = false,
                    additionalPrerequisites = new DungeonPrerequisite[0],
                }
            );

            Shop_Truck_Items_01.defaultItemDrops.Add(
                new WeightedGameObject() {
                    rawGameObject = null,
                    pickupId = RockSlide.RockSlidePickupID,
                    weight = 1,
                    forceDuplicatesPossible = false,
                    additionalPrerequisites = new DungeonPrerequisite[0],
                }
            );

            Shop_Truck_Items_01.defaultItemDrops.Add(
                new WeightedGameObject() {
                    rawGameObject = null,
                    pickupId = PowBlock.PowBlockPickupID,
                    weight = 1,
                    forceDuplicatesPossible = false,
                    additionalPrerequisites = new DungeonPrerequisite[0],
                }
            );

            Shop_Truck_Items_01.defaultItemDrops.Add(
                new WeightedGameObject() {
                    rawGameObject = null,
                    pickupId = ClownBullets.ClownBulletsID,
                    weight = 1,
                    forceDuplicatesPossible = false,
                    additionalPrerequisites = new DungeonPrerequisite[0],
                }
            );

            Shop_Truck_Items_01.defaultItemDrops.Add(
                new WeightedGameObject() {
                    rawGameObject = null,
                    pickupId = PortableShip.PortableShipID,
                    weight = 1,
                    forceDuplicatesPossible = false,
                    additionalPrerequisites = new DungeonPrerequisite[0],
                }
            );

            Shop_Curse_Items_01.defaultItemDrops.Add(
                new WeightedGameObject() {
                    rawGameObject = null,
                    pickupId = CursedBrick.CursedBrickID,
                    weight = 1,
                    forceDuplicatesPossible = false,
                    additionalPrerequisites = new DungeonPrerequisite[0],
                }
            );

            Shop_Curse_Items_01.defaultItemDrops.Add(
                new WeightedGameObject() {
                    rawGameObject = null,
                    pickupId = ThirdEye.ThirdEyeID,
                    weight = 1,
                    forceDuplicatesPossible = false,
                    additionalPrerequisites = new DungeonPrerequisite[0],
                }
            );

            CastleGungeonMergedTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            CustomRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            CustomRoomTable2 = ScriptableObject.CreateInstance<GenericRoomTable>();
            CustomRoomTableSecretGlitchFloor = ScriptableObject.CreateInstance<GenericRoomTable>();
            MegaBossRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            MegaChallengeShrineTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            MegaMiniBossRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();
            basic_special_rooms_noBlackMarket = ScriptableObject.CreateInstance<GenericRoomTable>();
            bosstable_01_gatlinggull_custom = ScriptableObject.CreateInstance<GenericRoomTable>();

            gatlinggull_noTileVisualOverrides = new PrototypeDungeonRoom[0];

            // GungeonFoyer = sharedAssets2.LoadAsset<DungeonFlow>("Foyer Flow").AllNodes[0].overrideExactRoom;

            // RoomBuilder.AddObjectToRoom(GungeonFoyer, new Vector2(38.9f, 49.8f), Jungle_ExitLadder, 15, 13);
            

            MetalGearRatPrefab = enemiesBase.LoadAsset<GameObject>("MetalGearRat");
            ResourcefulRatBossPrefab = enemiesBase.LoadAsset<GameObject>("ResourcefulRat_Boss");

            SewersRatExitEoom = SewerDungeonPrefab.PatternSettings.flows[0].sharedInjectionData[1].InjectionData[0].exactRoom;

            MimicNPC = ratDungeon.PatternSettings.flows[0].AllNodes[12].overrideExactRoom.additionalObjectLayers[0].placedObjects[13].nonenemyBehaviour.gameObject;

            RatCorpseNPC = MetalGearRatPrefab.GetComponent<MetalGearRatDeathController>().PunchoutMinigamePrefab.GetComponent<PunchoutController>().PlayerWonRatNPC.gameObject;
            PlayerLostRatNote = MetalGearRatPrefab.GetComponent<MetalGearRatDeathController>().PunchoutMinigamePrefab.GetComponent<PunchoutController>().PlayerLostNotePrefab.gameObject;
            MouseTrap1 = ResourcefulRatBossPrefab.GetComponent<ResourcefulRatController>().MouseTraps[0];
            MouseTrap2 = ResourcefulRatBossPrefab.GetComponent<ResourcefulRatController>().MouseTraps[1];
            MouseTrap3 = ResourcefulRatBossPrefab.GetComponent<ResourcefulRatController>().MouseTraps[2];

            SecretElevatorExitTileset = expandSharedAssets1.LoadAsset<GameObject>("SecretElevatorExitTileset");
            GameObject SecretElevatorExitTileset_Floor = SecretElevatorExitTileset.transform.Find("Floor").gameObject;
            GameObject SecretElevatorExitTileset_Roof = SecretElevatorExitTileset.transform.Find("Roof").gameObject;
            // SecretElevatorExitTileset_Roof.layer = LayerMask.NameToLayer("FG_Critical");

            tk2dSprite m_SecretElevatorExitTilesetFloorSprite = SpriteSerializer.AddSpriteToObject(SecretElevatorExitTileset_Floor, SecretElevatorExitTilesetCollection, "SecretElevatorExitTileset_Floor", tk2dBaseSprite.PerpendicularState.FLAT);
            // m_SecretElevatorExitTilesetFloorSprite.HeightOffGround = -5;
            m_SecretElevatorExitTilesetFloorSprite.HeightOffGround = -1.7f;

            tk2dSprite m_SecretElevatorExitTilesetRoofSprite = SpriteSerializer.AddSpriteToObject(SecretElevatorExitTileset_Roof, SecretElevatorExitTilesetCollection, "SecretElevatorExitTileset_Roof", tk2dBaseSprite.PerpendicularState.FLAT);
            // m_SecretElevatorExitTilesetRoofSprite.HeightOffGround = 6f;
            m_SecretElevatorExitTilesetRoofSprite.HeightOffGround = 2f;


            // Custom Trap Object for Rainbow room
            EXTrap_Apache = expandSharedAssets1.LoadAsset<GameObject>("EX_Trap_Apache");
            tk2dSprite m_EXTrapApacheSprite = SpriteSerializer.AddSpriteToObject(EXTrap_Apache, EXTrapCollection, "EX_Trap_Apache_01");
            m_EXTrapApacheSprite.HeightOffGround = 2;

            ExpandUtility.GenerateSpriteAnimator(EXTrap_Apache, playAutomatically: true, ClipFps: 8);

            tk2dSpriteAnimator ApacheTrapAnimator = EXTrap_Apache.GetComponent<tk2dSpriteAnimator>();

            List<string> ApacheTrap_Normal = new List<string>() {
                "EX_Trap_Apache_01",
                "EX_Trap_Apache_01"
            };
            List<string> ApacheTrap_Flipped = new List<string>() {
                "EX_Trap_Apache_02",
                "EX_Trap_Apache_02"
            };

            ExpandUtility.AddAnimation(ApacheTrapAnimator, EXTrapCollection.GetComponent<tk2dSpriteCollectionData>(), ApacheTrap_Normal, "ApacheTrap_Normal", tk2dSpriteAnimationClip.WrapMode.Loop, frameRate: 8);
            ExpandUtility.AddAnimation(ApacheTrapAnimator, EXTrapCollection.GetComponent<tk2dSpriteCollectionData>(), ApacheTrap_Flipped, "ApacheTrap_Flipped", tk2dSpriteAnimationClip.WrapMode.Loop, frameRate: 8);

            SpeculativeRigidbody EXTrap_ApacheRigidBody = ExpandUtility.GenerateOrAddToRigidBody(EXTrap_Apache, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(44, 58), offset: new IntVector2(10, 1));


            PathingTrapController EXTrapApache_PathTrap = EXTrap_Apache.AddComponent<PathingTrapController>();
            EXTrapApache_PathTrap.placeableHeight = 2;
            EXTrapApache_PathTrap.placeableWidth = 2;
            EXTrapApache_PathTrap.difficulty = DungeonPlaceableBehaviour.PlaceableDifficulty.BASE;
            EXTrapApache_PathTrap.isPassable = true;
            // SoundEffect?
            EXTrapApache_PathTrap.TrapSwitchState = string.Empty;
            EXTrapApache_PathTrap.damage = 0.5f;
            EXTrapApache_PathTrap.knockbackStrength = 50;
            EXTrapApache_PathTrap.hitsEnemies = false;
            EXTrapApache_PathTrap.enemyDamage = 0;
            EXTrapApache_PathTrap.enemyKnockbackStrength = 0;
            EXTrapApache_PathTrap.usesBloodyAnimation = false;
            EXTrapApache_PathTrap.usesDirectionalAnimations = true;
            EXTrapApache_PathTrap.northAnimation = "ApacheTrap_Flipped";
            EXTrapApache_PathTrap.northShadowAnimation = string.Empty;
            EXTrapApache_PathTrap.eastAnimation = "ApacheTrap_Normal";
            EXTrapApache_PathTrap.eastShadowAnimation = string.Empty;
            EXTrapApache_PathTrap.southAnimation = "ApacheTrap_Normal";
            EXTrapApache_PathTrap.southShadowAnimation = string.Empty;
            EXTrapApache_PathTrap.westAnimation = "ApacheTrap_Flipped";
            EXTrapApache_PathTrap.westShadowAnimation = string.Empty;
            EXTrapApache_PathTrap.usesDirectionalShadowAnimations = false;
            EXTrapApache_PathTrap.pauseAnimationOnRest = false;

            // This is a moving trap. It relies on paths. Please add paths to any rooms you intend to use this in!
            PathMover EXTrapApache_PathMover = EXTrap_Apache.AddComponent<PathMover>();
            EXTrapApache_PathMover.Paused = false;
            EXTrapApache_PathMover.OriginalPathSpeed = 6;
            EXTrapApache_PathMover.AdditionalNodeDelay = 0;
            EXTrapApache_PathMover.ForceCornerDelayHack = false;
            EXTrapApache_PathMover.IsUsingAlternateTargets = false;


            PrototypeDungeonRoom m_hell_connector_pathburst_01 = null;

            foreach (WeightedRoom wRoom in BulletHellRoomTable.includedRooms.elements) {
                if (wRoom.room.name.ToLower().StartsWith("hell_connector_pathburst_01")) {
                    m_hell_connector_pathburst_01 = wRoom.room;
                    break;
                }
            }

            if (m_hell_connector_pathburst_01 && m_hell_connector_pathburst_01.placedObjects.Count > 2 && m_hell_connector_pathburst_01.placedObjects[3].nonenemyBehaviour &&
                m_hell_connector_pathburst_01.placedObjects[3].nonenemyBehaviour.gameObject && m_hell_connector_pathburst_01.placedObjects[3].nonenemyBehaviour.gameObject.name == "RadialFireBurster") {

                EXTrap_Apache.AddComponent<DungeonPlaceableBehaviour>();
                EXTrap_Apache.GetComponent<DungeonPlaceableBehaviour>().placeableWidth = 2;
                EXTrap_Apache.GetComponent<DungeonPlaceableBehaviour>().placeableHeight = 2;
                EXTrap_Apache.GetComponent<DungeonPlaceableBehaviour>().difficulty = DungeonPlaceableBehaviour.PlaceableDifficulty.BASE;

                GameObject RadialFireBurster = m_hell_connector_pathburst_01.placedObjects[3].nonenemyBehaviour.gameObject;

                EXTrap_Apache.AddComponent<AIBulletBank>();
                ExpandUtility.DuplicateComponent(EXTrap_Apache.GetComponent<AIBulletBank>(), RadialFireBurster.GetComponent<AIBulletBank>());

                BehaviorSpeculator EXTrap_ApacheBehavior = EXTrap_Apache.AddComponent<BehaviorSpeculator>();
                EXTrap_ApacheBehavior.OverrideBehaviors = new List<OverrideBehaviorBase>(0);
                EXTrap_ApacheBehavior.OtherBehaviors = new List<BehaviorBase>(0);
                EXTrap_ApacheBehavior.TargetBehaviors = new List<TargetBehaviorBase>(0);
                EXTrap_ApacheBehavior.MovementBehaviors = new List<MovementBehaviorBase>(0);
                EXTrap_ApacheBehavior.InstantFirstTick = false;
                EXTrap_ApacheBehavior.TickInterval = 0.1f;
                EXTrap_ApacheBehavior.PostAwakenDelay = 0;
                EXTrap_ApacheBehavior.RemoveDelayOnReinforce = false;
                EXTrap_ApacheBehavior.OverrideStartingFacingDirection = false;
                EXTrap_ApacheBehavior.StartingFacingDirection = -90;
                EXTrap_ApacheBehavior.SkipTimingDifferentiator = false;

                EXTrap_ApacheBehavior.AttackBehaviors = new List<AttackBehaviorBase>() {
                    new ShootBehavior() {
                        ShootPoint = EXTrap_Apache.transform.Find("shoot point").gameObject,
                        BulletScript = new BulletScriptSelector() { scriptTypeName = "CircleBurst12" },
                        BulletName = null,
                        LeadAmount = 0,
                        StopDuring = ShootBehavior.StopType.None,
                        ImmobileDuringStop = false,
                        MoveSpeedModifier = 1,
                        LockFacingDirection = false,
                        ContinueAimingDuringTell = false,
                        ReaimOnFire = false,
                        MultipleFireEvents = false,
                        RequiresTarget = false,
                        PreventTargetSwitching = false,
                        Uninterruptible = false,
                        ClearGoop = false,
                        ClearGoopRadius = 2,
                        ShouldOverrideFireDirection = false,
                        OverrideFireDirection = -1,
                        SpecifyAiAnimator = null,
                        ChargeAnimation = null,
                        ChargeTime = 0,
                        TellAnimation = null,
                        FireAnimation = null,
                        PostFireAnimation = null,
                        HideGun = true,
                        OverrideBaseAnims = false,
                        OverrideIdleAnim = null,
                        OverrideMoveAnim = null,
                        UseVfx = false,
                        ChargeVfx = null,
                        TellVfx = null,
                        FireVfx = null,
                        Vfx = null,
                        EnabledDuringAttack = new GameObject[0],
                        Cooldown = 4,
                        CooldownVariance = 0,
                        AttackCooldown = 0,
                        GlobalCooldown = 0,
                        InitialCooldown = 0,
                        InitialCooldownVariance = 0,
                        GroupName = null,
                        GroupCooldown = 0,
                        MinRange = 0,
                        Range = 0,
                        MinWallDistance = 0,
                        MaxEnemiesInRoom = 0,
                        MinHealthThreshold = 0,
                        MaxHealthThreshold = 1,
                        HealthThresholds = new float[0],
                        AccumulateHealthThresholds = true,
                        targetAreaStyle = null,
                        IsBlackPhantom = false,
                        resetCooldownOnDamage = null,
                        RequiresLineOfSight = false,
                        MaxUsages = 0
                    }
                };

                ISerializedObject m_TargetBehaviorSpeculatorSeralized = EXTrap_ApacheBehavior;
                m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = new List<UnityEngine.Object>() { EXTrap_Apache.transform.Find("shoot point").gameObject };
                m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = new List<string>() { "OverrideBehaviors", "TargetBehaviors", "MovementBehaviors", "AttackBehaviors", "OtherBehaviors" };
                m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = new List<string>(0);
                EXTrap_Apache.AddComponent<TrapEnemyConfigurator>();
            }



            Teleporter_Gungeon_01 = braveResources.LoadAsset<GameObject>("Teleporter_Gungeon_01");
            ElevatorMaintanenceRoomIcon = sharedAssets2.LoadAsset<GameObject>("Minimap_Maintenance_Icon");
            Teleporter_Info_Sign = sharedAssets2.LoadAsset<GameObject>("teleporter_info_sign");
            RewardPedestalPrefab = sharedAssets.LoadAsset<GameObject>("Boss_Reward_Pedestal");
            Minimap_Maintenance_Icon = sharedAssets2.LoadAsset<GameObject>("minimap_maintenance_icon");

            // Forge Hammer prefab for Baby Good Hammer
            ForgeHammer = sharedAssets.LoadAsset<GameObject>("Forge_Hammer");
            EXFriendlyForgeHammerBullet = UnityEngine.Object.Instantiate(ForgeHammer.GetComponent<ForgeHammerController>().bulletBank.Bullets[0].BulletObject);
            EXFriendlyForgeHammerBullet.SetActive(false);
            EXFriendlyForgeHammerBullet.name = "8x8_fireball_companion_projectile_dark";
            FakePrefab.MarkAsFakePrefab(EXFriendlyForgeHammerBullet);
            UnityEngine.Object.DontDestroyOnLoad(EXFriendlyForgeHammerBullet);

            ExpandForgeHammerComponent.BuildPrefab();


            Arrival = expandSharedAssets1.LoadAsset<GameObject>("Arrival");
            Arrival.transform.name = "Arrival";

            NPCBabyDragunChaos = expandSharedAssets1.LoadAsset<GameObject>("Chaos Baby Dragun");
            NPCBabyDragunChaos.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(NPCBabyDragunChaos.GetComponent<tk2dSprite>(), sharedAssets2.LoadAsset<GameObject>("BabyDragunJail").GetComponentInChildren<tk2dSprite>());
            NPCBabyDragunChaos.AddComponent<tk2dSpriteAnimation>();
            NPCBabyDragunChaos.AddComponent<tk2dSpriteAnimator>();

            tk2dSpriteAnimation BabyDragunAnimation = NPCBabyDragunChaos.AddComponent<tk2dSpriteAnimation>();
            List<tk2dSpriteAnimationClip> m_NPCBabyDragunAnimationClips = new List<tk2dSpriteAnimationClip>();
            foreach (tk2dSpriteAnimationClip clip in sharedAssets2.LoadAsset<GameObject>("BabyDragunJail").GetComponentInChildren<tk2dSpriteAnimator>().Library.clips) {
                if (clip.name == "baby_dragun_weak_idle" | clip.name == "baby_dragun_weak_eat") { m_NPCBabyDragunAnimationClips.Add(clip); }
            }
            BabyDragunAnimation.clips = m_NPCBabyDragunAnimationClips.ToArray();

            tk2dSpriteAnimator NPCBabyDragunAnimator = NPCBabyDragunChaos.GetComponent<tk2dSpriteAnimator>();
            NPCBabyDragunAnimator.Library = BabyDragunAnimation;
            NPCBabyDragunAnimator.DefaultClipId = 0;
            NPCBabyDragunAnimator.AdditionalCameraVisibilityRadius = 0;
            NPCBabyDragunAnimator.AnimateDuringBossIntros = false;
            NPCBabyDragunAnimator.AlwaysIgnoreTimeScale = false;
            NPCBabyDragunAnimator.ForceSetEveryFrame = false;
            NPCBabyDragunAnimator.playAutomatically = false;
            NPCBabyDragunAnimator.IsFrameBlendedAnimation = false;
            NPCBabyDragunAnimator.clipTime = 0;
            NPCBabyDragunAnimator.deferNextStartClip = false;

            NPCBabyDragunChaos.AddComponent<ExpandBabyDragunComponent>();
            
            //Set this up first so we can use it in ElevatorDeparture dungoen placable.

            EXPortableElevator_Departure = expandSharedAssets1.LoadAsset<GameObject>("EXPortableElevator_Departure");

            GameObject m_ElevatorChild_Departure = EXPortableElevator_Departure.transform.Find("elevator").gameObject;
            GameObject m_ElevatorChild_Floor = EXPortableElevator_Departure.transform.Find("floor").gameObject;
            GameObject m_ElevatorChild_InteriorFloor = EXPortableElevator_Departure.transform.Find("interiorFloor").gameObject;


            tk2dSprite m_EXPortableElevator_DepartureSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorChild_Departure, EXPortableElevatorCollection, "portable_elevator_arrive_01");
            m_EXPortableElevator_DepartureSprite.HeightOffGround = -4.75f;

            tk2dSprite m_EXPortableElevator_FloorSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorChild_Floor, EXPortableElevatorCollection, "portable_elevator_floor_alt", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXPortableElevator_FloorSprite.HeightOffGround = -1.75f;

            tk2dSprite m_EXPortableElevator_InteriorFloorSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorChild_InteriorFloor, EXPortableElevatorCollection, "portable_elevator_interiorfloor", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXPortableElevator_InteriorFloorSprite.HeightOffGround = -0.75f;


            List<string> m_PortableElevatorArriveFrames = new List<string>() {
                "portable_elevator_arrive_01",
                "portable_elevator_arrive_02",
                "portable_elevator_arrive_03",
                "portable_elevator_arrive_04",
            };

            List<string> m_PortableElevatorDepartFrames = new List<string>() {
                "portable_elevator_depart_01",
                "portable_elevator_depart_02",
                "portable_elevator_depart_03",
                "portable_elevator_depart_04",
                "portable_elevator_depart_05",
                "portable_elevator_depart_06",
                "portable_elevator_depart_07",
                "portable_elevator_depart_08",
                "portable_elevator_depart_09",
                "portable_elevator_depart_10",
                "portable_elevator_depart_11",
            };

            List<string> m_PortableElevatorOpenFrames = new List<string>() {
                "portable_elevator_open_01",
                "portable_elevator_open_02",
                "portable_elevator_open_03",
                "portable_elevator_open_04",
                "portable_elevator_open_05",
            };

            List<string> m_PortableElevatorCloseFrames = new List<string>() {
                "portable_elevator_open_05",
                "portable_elevator_open_04",
                "portable_elevator_open_03",
                "portable_elevator_open_02",
                "portable_elevator_open_01"
            };

            ExpandUtility.GenerateSpriteAnimator(m_ElevatorChild_Departure);

            tk2dSpriteAnimator m_EXPortableElevator_DepartureAnimator = m_ElevatorChild_Departure.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_EXPortableElevator_DepartureAnimator, EXPortableElevatorCollection.GetComponent<tk2dSpriteCollectionData>(), m_PortableElevatorArriveFrames, "arrive", frameRate: 16);
            m_EXPortableElevator_DepartureAnimator.Library.clips[0].frames[0].eventAudio = "Play_OBJ_elevator_arrive_01";
            m_EXPortableElevator_DepartureAnimator.Library.clips[0].frames[0].triggerEvent = true;

            ExpandUtility.AddAnimation(m_EXPortableElevator_DepartureAnimator, EXPortableElevatorCollection.GetComponent<tk2dSpriteCollectionData>(), m_PortableElevatorDepartFrames, "depart", frameRate: 30); // 30
            m_EXPortableElevator_DepartureAnimator.Library.clips[1].frames[0].eventAudio = "Play_OBJ_elevator_leave_01";
            m_EXPortableElevator_DepartureAnimator.Library.clips[1].frames[0].triggerEvent = true;

            ExpandUtility.AddAnimation(m_EXPortableElevator_DepartureAnimator, EXPortableElevatorCollection.GetComponent<tk2dSpriteCollectionData>(), m_PortableElevatorOpenFrames, "open", frameRate: 9);
            ExpandUtility.AddAnimation(m_EXPortableElevator_DepartureAnimator, EXPortableElevatorCollection.GetComponent<tk2dSpriteCollectionData>(), m_PortableElevatorCloseFrames, "close", frameRate: 9);


            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 12), offset: new IntVector2(32, 16));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 36), offset: new IntVector2(32, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorChild_Floor, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(32, 64));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(11, 7), offset: new IntVector2(21, 21));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(11, 7), offset: new IntVector2(64, 21));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 78), offset: new IntVector2(16, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 78), offset: new IntVector2(64, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 13), offset: new IntVector2(32, 93));


            ExpandNewElevatorController m_EXElevatorController = EXPortableElevator_Departure.AddComponent<ExpandNewElevatorController>();
            m_EXElevatorController.ImpactVFXObjects = new GameObject[] {
                sharedAssets.LoadAsset<GameObject>("VFX_Dust_Explosion"),
                sharedAssets.LoadAsset<GameObject>("VFX_Tombstone_Impact"),
                sharedAssets.LoadAsset<GameObject>("VFX_Big_Dust_Poof")
            };


            EXPortableElevator_Reticle = expandSharedAssets1.LoadAsset<GameObject>("EXPortableElevator_Reticle");
            SpriteSerializer.AddSpriteToObject(EXPortableElevator_Reticle, EXPortableElevatorCollection, "portable_elevator_reticle_green", tk2dBaseSprite.PerpendicularState.FLAT);

            ExpandReticleRiserEffect m_PortableElevatorReticle = EXPortableElevator_Reticle.AddComponent<ExpandReticleRiserEffect>();
            m_PortableElevatorReticle.UpdateSpriteDefinitions = true;
            m_PortableElevatorReticle.CurrentSpriteName = "portable_elevator_reticle_green";
            m_PortableElevatorReticle.NumRisers = 3;
            m_PortableElevatorReticle.RiserHeight = 1;
            m_PortableElevatorReticle.RiseTime = 1.5f;

            // EXJungleElevator_Departure_Placable;
            EXPortableElevator_Departure_Placable = expandSharedAssets1.LoadAsset<GameObject>("EXPortableElevator_Departure_Placable");

            GameObject m_ElevatorPlacableChild_Departure = EXPortableElevator_Departure_Placable.transform.Find("elevator").gameObject;
            GameObject m_ElevatorPlacableChild_Floor = EXPortableElevator_Departure_Placable.transform.Find("floor").gameObject;
            GameObject m_ElevatorPlacableChild_FloorBorder = EXPortableElevator_Departure_Placable.transform.Find("floorBorder").gameObject;
            GameObject m_ElevatorPlacableChild_InteriorFloor = EXPortableElevator_Departure_Placable.transform.Find("interiorFloor").gameObject;
            
            // m_ElevatorPlacableChild_FloorBorder.transform.localPosition -= new Vector3(1, 1);

            tk2dSprite m_EXPortableElevator_Departure_PlacableChildSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorPlacableChild_Departure, EXPortableElevatorCollection, "portable_elevator_arrive_01");
            m_EXPortableElevator_Departure_PlacableChildSprite.HeightOffGround = -4.75f;

            tk2dSprite m_EXPortableElevatorPlacable_FloorSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorPlacableChild_Floor, EXPortableElevatorCollection, "portable_elevator_floor", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXPortableElevatorPlacable_FloorSprite.HeightOffGround = -1.7f;

            tk2dSprite m_EXPortableElevatorPlacable_FloorBorderSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorPlacableChild_FloorBorder, EXPortableElevatorCollection, "portable_elevator_floor_border", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXPortableElevatorPlacable_FloorBorderSprite.HeightOffGround = -1.75f;

            tk2dSprite m_EXPortableElevator_InteriorFloorPlacableSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorPlacableChild_InteriorFloor, EXPortableElevatorCollection, "portable_elevator_interiorfloor", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXPortableElevator_InteriorFloorPlacableSprite.HeightOffGround = -0.75f;


            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorPlacableChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 12), offset: new IntVector2(32, 16));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorPlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 36), offset: new IntVector2(32, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorPlacableChild_Floor, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(32, 64));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorPlacableChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(11, 7), offset: new IntVector2(21, 21));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorPlacableChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(11, 7), offset: new IntVector2(64, 21));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorPlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 78), offset: new IntVector2(16, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorPlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 78), offset: new IntVector2(64, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorPlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 13), offset: new IntVector2(32, 93));

            ExpandUtility.GenerateSpriteAnimator(m_ElevatorPlacableChild_Departure);

            tk2dSpriteAnimator m_EXPortableElevator_DeparturePlacableAnimator = m_ElevatorPlacableChild_Departure.GetComponent<tk2dSpriteAnimator>();
            m_EXPortableElevator_DeparturePlacableAnimator.Library = m_EXPortableElevator_DepartureAnimator.Library;

            ExpandNewElevatorController m_EXElevatorPlacableController = EXPortableElevator_Departure_Placable.AddComponent<ExpandNewElevatorController>();
            m_EXElevatorPlacableController.ArriveOnSpawn = false;
            m_EXElevatorPlacableController.UsesOverrideTargetFloor = false;
            m_EXElevatorPlacableController.ImpactVFXObjects = new GameObject[] {
                sharedAssets.LoadAsset<GameObject>("VFX_Dust_Explosion"),
                sharedAssets.LoadAsset<GameObject>("VFX_Tombstone_Impact"),
                sharedAssets.LoadAsset<GameObject>("VFX_Big_Dust_Poof")
            };

            EXJungleElevator_Departure_Placable = expandSharedAssets1.LoadAsset<GameObject>("EXJungleElevator_Departure_Placable");

            GameObject m_JunglePlacableChild_Departure = EXJungleElevator_Departure_Placable.transform.Find("elevator").gameObject;
            GameObject m_JunglePlacableChild_Floor = EXJungleElevator_Departure_Placable.transform.Find("floor").gameObject;
            GameObject m_JunglePlacableChild_FloorBorder = EXJungleElevator_Departure_Placable.transform.Find("floorBorder").gameObject;
            GameObject m_JunglePlacableChild_InteriorFloor = EXJungleElevator_Departure_Placable.transform.Find("interiorFloor").gameObject;

            tk2dSprite m_EXJungleElevator_Departure_PlacableChildSprite = SpriteSerializer.AddSpriteToObject(m_JunglePlacableChild_Departure, EXPortableElevatorCollection, "portable_elevator_arrive_01");
            m_EXJungleElevator_Departure_PlacableChildSprite.HeightOffGround = -4.75f;

            tk2dSprite m_EXJungleElevatorPlacable_FloorSprite = SpriteSerializer.AddSpriteToObject(m_JunglePlacableChild_Floor, EXPortableElevatorCollection, "portable_elevator_floor", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXJungleElevatorPlacable_FloorSprite.HeightOffGround = -1.7f;

            tk2dSprite m_EXJungleElevatorPlacable_FloorBorderSprite = SpriteSerializer.AddSpriteToObject(m_JunglePlacableChild_FloorBorder, EXPortableElevatorCollection, "portable_elevator_floor_border", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXJungleElevatorPlacable_FloorBorderSprite.HeightOffGround = -1.75f;

            tk2dSprite m_EXJungleElevator_InteriorFloorPlacableSprite = SpriteSerializer.AddSpriteToObject(m_JunglePlacableChild_InteriorFloor, EXPortableElevatorCollection, "portable_elevator_interiorfloor", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXJungleElevator_InteriorFloorPlacableSprite.HeightOffGround = -0.75f;


            ExpandUtility.GenerateOrAddToRigidBody(m_JunglePlacableChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 12), offset: new IntVector2(32, 16));
            ExpandUtility.GenerateOrAddToRigidBody(m_JunglePlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 36), offset: new IntVector2(32, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_JunglePlacableChild_Floor, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(32, 64));
            ExpandUtility.GenerateOrAddToRigidBody(m_JunglePlacableChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(11, 7), offset: new IntVector2(21, 21));
            ExpandUtility.GenerateOrAddToRigidBody(m_JunglePlacableChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(11, 7), offset: new IntVector2(64, 21));
            ExpandUtility.GenerateOrAddToRigidBody(m_JunglePlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 78), offset: new IntVector2(16, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_JunglePlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 78), offset: new IntVector2(64, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_JunglePlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 13), offset: new IntVector2(32, 93));

            ExpandUtility.GenerateSpriteAnimator(m_JunglePlacableChild_Departure);

            tk2dSpriteAnimator m_EXJungleElevator_DeparturePlacableAnimator = m_JunglePlacableChild_Departure.GetComponent<tk2dSpriteAnimator>();
            m_EXJungleElevator_DeparturePlacableAnimator.Library = m_EXPortableElevator_DepartureAnimator.Library;

            ExpandNewElevatorController m_EXJunglePlacableController = EXJungleElevator_Departure_Placable.AddComponent<ExpandNewElevatorController>();
            m_EXJunglePlacableController.ArriveOnSpawn = false;
            m_EXJunglePlacableController.UsesOverrideTargetFloor = true;
            m_EXJunglePlacableController.OverrideFloorName = "tt_jungle";
            m_EXJunglePlacableController.ImpactVFXObjects = new GameObject[] {
                sharedAssets.LoadAsset<GameObject>("VFX_Dust_Explosion"),
                sharedAssets.LoadAsset<GameObject>("VFX_Tombstone_Impact"),
                sharedAssets.LoadAsset<GameObject>("VFX_Big_Dust_Poof")
            };


            EXElevator_Arrival_Placable = expandSharedAssets1.LoadAsset<GameObject>("EXElevator_Arrival_Placable");

            GameObject m_ElevatorArrivalPlacableChild_Departure = EXElevator_Arrival_Placable.transform.Find("elevator").gameObject;
            GameObject m_ElevatorArrivalPlacableChild_Floor = EXElevator_Arrival_Placable.transform.Find("floor").gameObject;
            GameObject m_ElevatorArrivalPlacableChild_FloorBorder = EXElevator_Arrival_Placable.transform.Find("floorBorder").gameObject;
            GameObject m_ElevatorArrivalPlacableChild_InteriorFloor = EXElevator_Arrival_Placable.transform.Find("interiorFloor").gameObject;

            tk2dSprite m_EXElevator_Arrival_PlacableChildSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorArrivalPlacableChild_Departure, EXPortableElevatorCollection, "portable_elevator_arrive_01");
            m_EXElevator_Arrival_PlacableChildSprite.HeightOffGround = -4.75f;

            tk2dSprite m_EXElevatorArrivalPlacable_FloorSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorArrivalPlacableChild_Floor, EXPortableElevatorCollection, "portable_elevator_floor", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXElevatorArrivalPlacable_FloorSprite.HeightOffGround = -1.7f;

            tk2dSprite m_EXElevatorArrivalPlacable_FloorBorderSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorArrivalPlacableChild_FloorBorder, EXPortableElevatorCollection, "portable_elevator_floor_border", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXElevatorArrivalPlacable_FloorBorderSprite.HeightOffGround = -1.75f;

            tk2dSprite m_EXPElevatorArrival_InteriorFloorPlacableSprite = SpriteSerializer.AddSpriteToObject(m_ElevatorArrivalPlacableChild_InteriorFloor, EXPortableElevatorCollection, "portable_elevator_interiorfloor", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXPElevatorArrival_InteriorFloorPlacableSprite.HeightOffGround = -0.75f;


            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorArrivalPlacableChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 12), offset: new IntVector2(32, 16));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorArrivalPlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 36), offset: new IntVector2(32, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorArrivalPlacableChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(11, 7), offset: new IntVector2(21, 21));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorArrivalPlacableChild_Floor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(11, 7), offset: new IntVector2(64, 21));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorArrivalPlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 78), offset: new IntVector2(16, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorArrivalPlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 78), offset: new IntVector2(64, 28));
            ExpandUtility.GenerateOrAddToRigidBody(m_ElevatorArrivalPlacableChild_Floor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 13), offset: new IntVector2(32, 93));

            ExpandUtility.GenerateSpriteAnimator(m_ElevatorArrivalPlacableChild_Departure);

            tk2dSpriteAnimator m_EXElevator_ArrivalPlacableAnimator = m_ElevatorArrivalPlacableChild_Departure.GetComponent<tk2dSpriteAnimator>();
            m_EXElevator_ArrivalPlacableAnimator.Library = m_EXPortableElevator_DepartureAnimator.Library;

            ExpandNewElevatorController m_EXElevatorArrivalPlacableController = EXElevator_Arrival_Placable.AddComponent<ExpandNewElevatorController>();
            m_EXElevatorArrivalPlacableController.IsArrivalElevator = true;
            m_EXElevatorArrivalPlacableController.ArriveOnSpawn = false;
            m_EXElevatorArrivalPlacableController.ImpactVFXObjects = new GameObject[] {
                sharedAssets.LoadAsset<GameObject>("VFX_Dust_Explosion"),
                sharedAssets.LoadAsset<GameObject>("VFX_Tombstone_Impact"),
                sharedAssets.LoadAsset<GameObject>("VFX_Big_Dust_Poof")
            };

            ElevatorDeparture = sharedAssets2.LoadAsset<DungeonPlaceable>("Elevator_Departure");
            ElevatorArrival = sharedAssets2.LoadAsset<DungeonPlaceable>("Elevator_Arrival");

            TinySecretRoomRewards = ScriptableObject.CreateInstance<DungeonPlaceable>();
            TinySecretRoomJunkReward = ScriptableObject.CreateInstance<DungeonPlaceable>();
            CorruptedSecretRoomSpecialItem = ScriptableObject.CreateInstance<DungeonPlaceable>();

            TinySecretRoomRewards.name = "Tiny Secret Room Reward";
            TinySecretRoomRewards.width = 1;
            TinySecretRoomRewards.height = 1;
            TinySecretRoomRewards.isPassable = true;
            TinySecretRoomRewards.roomSequential = false;
            TinySecretRoomRewards.respectsEncounterableDifferentiator = false;
            TinySecretRoomRewards.UsePrefabTransformOffset = false;
            TinySecretRoomRewards.MarkSpawnedItemsAsRatIgnored = true;
            TinySecretRoomRewards.DebugThisPlaceable = false;
            TinySecretRoomRewards.IsAnnexTable = false;
            TinySecretRoomRewards.variantTiers = new List<DungeonPlaceableVariant>() {
                new DungeonPlaceableVariant() {
                    percentChance = 0.6f,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = 127,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 0.35f,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = 147,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 0.3f,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = 224,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 0.3f,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = 600,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 0.3f,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = 78,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 0.3f,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = 120,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 0.3f,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = 565,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 0.02f,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = 641,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 0.02f,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = 580,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                }
            };

            TinySecretRoomJunkReward.name = "Tiny Secret Room Junk Reward";
            TinySecretRoomJunkReward.width = 1;
            TinySecretRoomJunkReward.height = 1;
            TinySecretRoomJunkReward.isPassable = true;
            TinySecretRoomJunkReward.roomSequential = false;
            TinySecretRoomJunkReward.respectsEncounterableDifferentiator = false;
            TinySecretRoomJunkReward.UsePrefabTransformOffset = false;
            TinySecretRoomJunkReward.MarkSpawnedItemsAsRatIgnored = true;
            TinySecretRoomJunkReward.DebugThisPlaceable = false;
            TinySecretRoomJunkReward.IsAnnexTable = false;
            TinySecretRoomJunkReward.variantTiers = new List<DungeonPlaceableVariant>() {
                 new DungeonPlaceableVariant() {
                    percentChance = 1f,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = 127,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                }
            };

            CorruptedSecretRoomSpecialItem.name = "Corrupte Secret Room Corruption Bomb Item";
            CorruptedSecretRoomSpecialItem.width = 1;
            CorruptedSecretRoomSpecialItem.height = 1;
            CorruptedSecretRoomSpecialItem.isPassable = true;
            CorruptedSecretRoomSpecialItem.roomSequential = false;
            CorruptedSecretRoomSpecialItem.respectsEncounterableDifferentiator = false;
            CorruptedSecretRoomSpecialItem.UsePrefabTransformOffset = false;
            CorruptedSecretRoomSpecialItem.MarkSpawnedItemsAsRatIgnored = true;
            CorruptedSecretRoomSpecialItem.DebugThisPlaceable = false;
            CorruptedSecretRoomSpecialItem.IsAnnexTable = false;
            CorruptedSecretRoomSpecialItem.variantTiers = new List<DungeonPlaceableVariant>() {
                 new DungeonPlaceableVariant() {
                    percentChance = 1f,
                    unitOffset = Vector2.zero,
                    // nonDatabasePlaceable = Game.Items.Get("ex:corruption_bomb").gameObject,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = CorruptionBomb.CorruptionBombPickupID,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                }
            };

            MetalCubeGuy = EnemyDatabase.GetOrLoadByGuid("ba928393c8ed47819c2c5f593100a5bc").gameObject;
            SerManuel = EnemyDatabase.GetOrLoadByGuid("fc809bd43a4d41738a62d7565456622c").gameObject;
            SkusketHead = EnemyDatabase.GetOrLoadByGuid("c2f902b7cbe745efb3db4399927eab34").gameObject;
            AK47BulletKin = EnemyDatabase.GetOrLoadByGuid("db35531e66ce41cbb81d507a34366dfe").gameObject;

            // Fix missing death sound
            AK47BulletKin.GetComponent<AIActor>().EnemySwitchState = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").EnemySwitchState;

            RatJailDoor = ratDungeon.PatternSettings.flows[0].AllNodes[13].overrideExactRoom.placedObjects[1].nonenemyBehaviour.gameObject;
            CurrsedMirror = basic_special_rooms.includedRooms.elements[1].room.placedObjects[0].nonenemyBehaviour.gameObject;
            
            ElevatorArrivalVarientForUnknownTilesets = new DungeonPlaceableVariant() {
                percentChance = 0,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXElevator_Arrival_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[0]
            };


            ElevatorArrivalVarientForOffice = new DungeonPlaceableVariant() {
                percentChance = 1f,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXElevator_Arrival_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.OFFICEGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };
            ElevatorArrivalVarientForJungle = new DungeonPlaceableVariant() {
                percentChance = 1f,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXElevator_Arrival_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.JUNGLEGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };
            ElevatorArrivalVarientForBelly = new DungeonPlaceableVariant() {
                percentChance = 1f,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXElevator_Arrival_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.BELLYGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };
            ElevatorArrivalVarientForOldWest = new DungeonPlaceableVariant() {
                percentChance = 1f,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXElevator_Arrival_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.WESTGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };
            ElevatorArrivalVarientForPhobos = new DungeonPlaceableVariant() {
                percentChance = 1f,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXElevator_Arrival_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.PHOBOSGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };
            ElevatorArrivalVarientForSpace = new DungeonPlaceableVariant() {
                percentChance = 1f,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXElevator_Arrival_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.SPACEGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };

            
            ElevatorDepartureVarientForOffice = new DungeonPlaceableVariant() {
                percentChance = 1,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXPortableElevator_Departure_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.OFFICEGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };

            ElevatorDepartureVarientForJungle = new DungeonPlaceableVariant() {
                percentChance = 1,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXPortableElevator_Departure_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.JUNGLEGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };

            ElevatorDepartureVarientForBelly = new DungeonPlaceableVariant() {
                percentChance = 1,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXPortableElevator_Departure_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.BELLYGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };

            ElevatorDepartureVarientForOldWest = new DungeonPlaceableVariant() {
                percentChance = 1,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXPortableElevator_Departure_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.WESTGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };

            ElevatorDepartureVarientForPhobos = new DungeonPlaceableVariant() {
                percentChance = 1,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXPortableElevator_Departure_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.PHOBOSGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };

            ElevatorDepartureVarientForSpace = new DungeonPlaceableVariant() {
                percentChance = 1,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = EXPortableElevator_Departure_Placable,
                enemyPlaceableGuid = string.Empty,
                pickupObjectPlaceableId = -1,
                forceBlackPhantom = false,
                addDebrisObject = false,
                materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0],
                prerequisites = new DungeonPrerequisite[] {
                    new DungeonPrerequisite() {
                        prerequisiteType = DungeonPrerequisite.PrerequisiteType.TILESET,
                        prerequisiteOperation = DungeonPrerequisite.PrerequisiteOperation.LESS_THAN,
                        statToCheck = TrackedStats.BULLETS_FIRED,
                        maxToCheck = TrackedMaximums.MOST_KEYS_HELD,
                        comparisonValue = 0,
                        useSessionStatValue = false,
                        encounteredObjectGuid = string.Empty,
                        requiredNumberOfEncounters = 0,
                        requireCharacter = false,
                        requiredCharacter = PlayableCharacters.Pilot,
                        requiredTileset = GlobalDungeonData.ValidTilesets.SPACEGEON,
                        requireTileset = true,
                        requireFlag = false,
                        requireDemoMode = false
                    }
                }
            };

            
            ElevatorArrival.variantTiers.Add(ElevatorArrivalVarientForUnknownTilesets);
            ElevatorArrival.variantTiers.Add(ElevatorArrivalVarientForOffice);
            ElevatorArrival.variantTiers.Add(ElevatorArrivalVarientForJungle);
            ElevatorArrival.variantTiers.Add(ElevatorArrivalVarientForBelly);
            ElevatorArrival.variantTiers.Add(ElevatorArrivalVarientForOldWest);
            ElevatorArrival.variantTiers.Add(ElevatorArrivalVarientForPhobos);
            ElevatorArrival.variantTiers.Add(ElevatorArrivalVarientForSpace);
            
            ElevatorDeparture.variantTiers.Add(ElevatorDepartureVarientForOffice);
            ElevatorDeparture.variantTiers.Add(ElevatorDepartureVarientForJungle);
            ElevatorDeparture.variantTiers.Add(ElevatorDepartureVarientForBelly);
            ElevatorDeparture.variantTiers.Add(ElevatorDepartureVarientForOldWest);
            ElevatorDeparture.variantTiers.Add(ElevatorDepartureVarientForPhobos);
            ElevatorDeparture.variantTiers.Add(ElevatorDepartureVarientForSpace);            
            ElevatorDeparture.variantTiers[6].nonDatabasePlaceable = EXPortableElevator_Departure_Placable;

            // Build Room table with Castle and Gungeon room tables merged
            CastleGungeonMergedTable.name = "CastleGungeonMergedTable";
            CastleGungeonMergedTable.includedRooms = new WeightedRoomCollection();
            CastleGungeonMergedTable.includedRooms.elements = new List<WeightedRoom>();
            CastleGungeonMergedTable.includedRoomTables = new List<GenericRoomTable>() { SecretRoomTable };

            CastleGungeonMergedTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(paradox_04));
            CastleGungeonMergedTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(paradox_04_copy));

            for (int i = 0; i < CastleRoomTable.includedRooms.elements.Count; i++) {
                CastleGungeonMergedTable.includedRooms.elements.Add(CastleRoomTable.includedRooms.elements[i]);
            }
            for (int i = 0; i < Gungeon_RoomTable.includedRooms.elements.Count; i++) {
                CastleGungeonMergedTable.includedRooms.elements.Add(Gungeon_RoomTable.includedRooms.elements[i]);
            }


            // Build Mega Room table with all main Dungeon rooms in one table
            CustomRoomTable.name = "Test Mega Room Table";
            CustomRoomTable.includedRooms = new WeightedRoomCollection();
            CustomRoomTable.includedRooms.elements = new List<WeightedRoom>();
            CustomRoomTable.includedRoomTables = new List<GenericRoomTable>() { SecretRoomTable };

            CustomRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(paradox_04));

            CustomRoomTable.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(paradox_04_copy));

            // Build Mega Room table with all main Dungeon rooms in one table but with Castle rooms not included.
            CustomRoomTable2.name = "Test Mega Room Table 2";
            CustomRoomTable2.includedRooms = new WeightedRoomCollection();
            CustomRoomTable2.includedRooms.elements = new List<WeightedRoom>();
            CustomRoomTable2.includedRoomTables = new List<GenericRoomTable>() { SecretRoomTable };

            CustomRoomTableSecretGlitchFloor.name = "Test Mega Room Table Secret";
            CustomRoomTableSecretGlitchFloor.includedRooms = new WeightedRoomCollection();
            CustomRoomTableSecretGlitchFloor.includedRooms.elements = new List<WeightedRoom>();
            CustomRoomTableSecretGlitchFloor.includedRoomTables = new List<GenericRoomTable>() { SecretRoomTable };

            foreach (WeightedRoom roomElement in OfficeAndUnusedWeightedRooms) {
                if (roomElement.room != null) {
                    CustomRoomTable.includedRooms.elements.Add(roomElement);
                    CustomRoomTable2.includedRooms.elements.Add(roomElement);
                    CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in CastleRoomTable.includedRooms.elements) {
                if (roomElement.room != null && roomElement.room.overrideRoomVisualType == -1) {
                    CustomRoomTable.includedRooms.elements.Add(roomElement);
                    CustomRoomTable2.includedRooms.elements.Add(roomElement);
                    CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in SewerDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements) {
                if (roomElement.room != null) {
                    CustomRoomTable.includedRooms.elements.Add(roomElement);
                    CustomRoomTable2.includedRooms.elements.Add(roomElement);
                    CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in Gungeon_RoomTable.includedRooms.elements) {
                if (roomElement.room != null && !roomElement.room.name.ToLower().StartsWith("gungeon_snipe_city")) {
                    CustomRoomTable.includedRooms.elements.Add(roomElement);
                    CustomRoomTable2.includedRooms.elements.Add(roomElement);
                    CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in CathedralDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements) {
                if (roomElement.room != null) {
                    CustomRoomTable.includedRooms.elements.Add(roomElement);
                    CustomRoomTable2.includedRooms.elements.Add(roomElement);
                    CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in MinesDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements) {
                if (roomElement.room != null) {
                    if (roomElement.room.paths == null | roomElement.room.paths.Count <= 0) {
                        CustomRoomTable.includedRooms.elements.Add(roomElement);
                        CustomRoomTable2.includedRooms.elements.Add(roomElement);
                        CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(roomElement);
                    }
                }
            }
            foreach (WeightedRoom roomElement in CatacombsDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements) {
                if (roomElement.room != null) {
                    CustomRoomTable.includedRooms.elements.Add(roomElement);
                    CustomRoomTable2.includedRooms.elements.Add(roomElement);
                    CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in ForgeDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements) {
                if (roomElement.room != null) {
                    if (!roomElement.room.name.ToLower().EndsWith("(final)") && !roomElement.room.name.ToLower().EndsWith("exit_room_forge") &&
                        !roomElement.room.name.ToLower().EndsWith("testroom") && !roomElement.room.name.ToLower().EndsWith("endtimes_chamber") &&
                        !roomElement.room.name.ToLower().StartsWith("forge_joe_hot_fire_011") && !roomElement.room.name.ToLower().StartsWith("Forge_Joe_Hot_Fire_019"))
                    {
                        CustomRoomTable.includedRooms.elements.Add(roomElement);
                        CustomRoomTable2.includedRooms.elements.Add(roomElement);
                        CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(roomElement);
                    }
                }
            }
            foreach (WeightedRoom roomElement in BulletHellDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements) {
                if (roomElement.room != null) {
                    CustomRoomTable.includedRooms.elements.Add(roomElement);
                    CustomRoomTable2.includedRooms.elements.Add(roomElement);
                    CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(roomElement);
                    if (!string.IsNullOrEmpty(roomElement.room.name) && roomElement.room.name.ToLower().StartsWith("hell_hath_no_joery_009")) {
                        Hell_Hath_No_Joery_009 = roomElement.room;
                    }
                }
            }


            if (Hell_Hath_No_Joery_009 != null) {
                RoomBuilder.GenerateRoomLayout(Hell_Hath_No_Joery_009, "Hell_Hath_No_Joery_009_Layout");
            }

            List<PrototypeDungeonRoom> m_GatlingGullRooms = new List<PrototypeDungeonRoom>();

            foreach (WeightedRoom wRoom in bosstable_01_gatlinggull.includedRooms.elements) { m_GatlingGullRooms.Add(UnityEngine.Object.Instantiate(wRoom.room)); }

            // Disabling overrideRoomVisualType fixes exceptions on some tilesets. ;)
            foreach (PrototypeDungeonRoom room in m_GatlingGullRooms) { room.overrideRoomVisualType = -1; }

            gatlinggull_noTileVisualOverrides = m_GatlingGullRooms.ToArray();
            bosstable_01_gatlinggull_custom.includedRooms = new WeightedRoomCollection();
            bosstable_01_gatlinggull_custom.includedRooms.elements = new List<WeightedRoom>();
            bosstable_01_gatlinggull_custom.includedRoomTables = new List<GenericRoomTable>(0);

            foreach (PrototypeDungeonRoom room in gatlinggull_noTileVisualOverrides) {
                if (room.name.StartsWith("GatlingGullRoom04") | room.name.StartsWith("GatlingGullRoom05")) {
                    bosstable_01_gatlinggull_custom.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(room, 0.5f));
                } else {
                    bosstable_01_gatlinggull_custom.includedRooms.elements.Add(ExpandRoomPrefabs.GenerateWeightedRoom(room));
                }
            }

            MegaBossRoomTable.includedRooms = new WeightedRoomCollection();
            MegaBossRoomTable.includedRooms.elements = new List<WeightedRoom>();
            MegaBossRoomTable.includedRoomTables = new List<GenericRoomTable>(0);

            foreach (WeightedRoom roomElement in bosstable_01_bulletbros.includedRooms.elements) {
                if (roomElement.room != null) {
                    MegaBossRoomTable.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in bosstable_01_bulletking.includedRooms.elements) {
                if (roomElement.room != null) {
                    MegaBossRoomTable.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in bosstable_01_gatlinggull_custom.includedRooms.elements) {
                if (roomElement.room != null) { MegaBossRoomTable.includedRooms.elements.Add(roomElement); }
            }
            foreach (WeightedRoom roomElement in bosstable_02_meduzi.includedRooms.elements) {
                if (roomElement.room != null) {
                    MegaBossRoomTable.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in bosstable_02a_highpriest.includedRooms.elements) {
                if (roomElement.room != null) {
                    MegaBossRoomTable.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in bosstable_03_powderskull.includedRooms.elements) {
                if (roomElement.room != null) {
                    MegaBossRoomTable.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in bosstable_03_tank.includedRooms.elements) {
                if (roomElement.room != null) {
                    MegaBossRoomTable.includedRooms.elements.Add(roomElement);
                }
            }
            foreach (WeightedRoom roomElement in bosstable_04_demonwall.includedRooms.elements) {
                if (roomElement.room != null) { MegaBossRoomTable.includedRooms.elements.Add(roomElement); }
            }
            foreach (WeightedRoom roomElement in bosstable_04_statues.includedRooms.elements) {
                if (roomElement.room != null) {
                    MegaBossRoomTable.includedRooms.elements.Add(roomElement);
                }
            }

            // Randomize room order in these tables. Custom Secret Floor doesn't seem to want to randomize them on it's own.
            winchesterroomtable.includedRooms.elements = winchesterroomtable.includedRooms.elements.Shuffle();

            PrototypeDungeonRoom m_gungeon_rewardroom_1 = UnityEngine.Object.Instantiate(gungeon_rewardroom_1);

            // Add teleporter to make it like the other reward rooms post AG&D update.
            RoomBuilder.AddObjectToRoom(reward_room, new Vector2(3, 1), NonEnemyBehaviour: Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            // This Room Prefab didn't include a chest placer...lol. We'll use the one from gungeon_rewardroom_1. :P
            reward_room.additionalObjectLayers.Add(m_gungeon_rewardroom_1.additionalObjectLayers[1]);
            int m_rewardRoomObjectLayerIndex = (reward_room.additionalObjectLayers.Count - 1);
            reward_room.additionalObjectLayers[m_rewardRoomObjectLayerIndex].placedObjects[0].contentsBasePosition = new Vector2(4f, 7.5f);
            reward_room.additionalObjectLayers[m_rewardRoomObjectLayerIndex].placedObjectBasePositions[0] = new Vector2(4f, 7.5f);


            // Replace exit elevator with entrance elevator from normal elevator room. 
            // Add teleporter using existing data from entrance elevator room.
            tiny_entrance.name = "Tiny Elevator Entrance";
            tiny_entrance.placedObjects = new List<PrototypePlacedObjectData>();
            tiny_entrance.placedObjectPositions = new List<Vector2>();
            tiny_entrance.exitData.exits = new List<PrototypeRoomExit>();
            tiny_entrance.category = PrototypeDungeonRoom.RoomCategory.ENTRANCE;
            tiny_entrance.associatedMinimapIcon = elevator_entrance.associatedMinimapIcon;
            RoomBuilder.AddObjectToRoom(tiny_entrance, new Vector2(3, 8), ElevatorArrival);
            RoomBuilder.AddObjectToRoom(tiny_entrance, new Vector2(4, 1), NonEnemyBehaviour: Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddExitToRoom(tiny_entrance, new Vector2(0, 6), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(tiny_entrance, new Vector2(6, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(tiny_entrance, new Vector2(13, 6), DungeonData.Direction.EAST);


            tiny_exit.name = "Tiny Exit";
            tiny_exit.category = PrototypeDungeonRoom.RoomCategory.EXIT;
            tiny_exit.placedObjects = new List<PrototypePlacedObjectData>();
            tiny_exit.placedObjectPositions = new List<Vector2>();
            tiny_exit.exitData.exits = new List<PrototypeRoomExit>();
            RoomBuilder.AddObjectToRoom(tiny_exit, new Vector2(3, 8), ElevatorDeparture);
            RoomBuilder.AddObjectToRoom(tiny_exit, new Vector2(4, 1), NonEnemyBehaviour: Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddObjectToRoom(tiny_exit, new Vector2(9, 6), NonEnemyBehaviour: exit_room_basic.placedObjects[2].nonenemyBehaviour);
            RoomBuilder.AddExitToRoom(tiny_exit, new Vector2(0, 6), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(tiny_exit, new Vector2(6, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(tiny_exit, new Vector2(13, 6), DungeonData.Direction.EAST);


            tutorial_minibossroom.name = "Tutorial Miniboss(Custom)";
            tutorial_minibossroom.placedObjects = new List<PrototypePlacedObjectData>();
            RoomBuilder.AddObjectToRoom(tutorial_minibossroom, new Vector2(4, 9), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            tutorial_minibossroom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "fc809bd43a4d41738a62d7565456622c", // Ser_Manuel
                            contentsBasePosition = new Vector2(14, 9),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() { new Vector2(14, 9) },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 2,
                    suppressPlayerChecks = true,
                    delayTime = 4,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
            };
            tutorial_minibossroom.overriddenTilesets = 0;
            tutorial_minibossroom.usesProceduralDecoration = true;
            tutorial_minibossroom.associatedMinimapIcon = fusebombroom01.associatedMinimapIcon;
            tutorial_minibossroom.category = PrototypeDungeonRoom.RoomCategory.BOSS;
            tutorial_minibossroom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.UNSPECIFIED_SPECIAL;
            tutorial_minibossroom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.MINI_BOSS;
            tutorial_minibossroom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM) {
                    condition = RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES,
                    action = RoomEventTriggerAction.SEAL_ROOM
                },
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM) {
                    condition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    action = RoomEventTriggerAction.UNSEAL_ROOM
                }
            };

            // Allow elevator entrance room to warp player to this room. (For use in Boss Rush DungeonFlows only!)            
            bossrush_alternate_entrance.name = "ElevatorMaintenanceRoom";
            bossrush_alternate_entrance.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            bossrush_alternate_entrance.associatedMinimapIcon = ElevatorMaintanenceRoomIcon;


            tutorial_fakeboss.placedObjectPositions = new List<Vector2>();
            tutorial_fakeboss.placedObjects = new List<PrototypePlacedObjectData>();
            RoomBuilder.AddObjectToRoom(tutorial_fakeboss, new Vector2(8, 20), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            tutorial_fakeboss.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "fc809bd43a4d41738a62d7565456622c", // Ser_Manuel
                            contentsBasePosition = new Vector2(18, 22),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        }
                    },
                    placedObjectBasePositions = new List<Vector2>() { new Vector2(18, 22) },
                    layerIsReinforcementLayer = true,
                    shuffle = true,
                    randomize = 2,
                    suppressPlayerChecks = true,
                    delayTime = 4,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ON_ENEMIES_CLEARED,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
            };


            big_entrance.name = "Large Elevator Entrance";
            big_entrance.associatedMinimapIcon = tiny_entrance.associatedMinimapIcon;
            big_entrance.roomEvents.Clear();
            big_entrance.additionalObjectLayers.Clear();
            big_entrance.category = PrototypeDungeonRoom.RoomCategory.ENTRANCE;
            big_entrance.GUID = Guid.NewGuid().ToString();
            big_entrance.placedObjects[0].nonenemyBehaviour = null;
            big_entrance.placedObjects[0] = new PrototypePlacedObjectData() {
                placeableContents = ElevatorArrival,
                contentsBasePosition = new Vector2(22, 21),
                layer = 0,
                xMPxOffset = 0,
                yMPxOffset = 0,
                fieldData = new List<PrototypePlacedObjectFieldData>(0),
                instancePrerequisites = new DungeonPrerequisite[0],
                linkedTriggerAreaIDs = new List<int>(0),
                assignedPathStartNode = 0
            };
            big_entrance.placedObjects[1] = new PrototypePlacedObjectData() {
                nonenemyBehaviour = Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>(),
                contentsBasePosition = new Vector2(23, 8),
                layer = 0,
                xMPxOffset = 0,
                yMPxOffset = 0,
                fieldData = new List<PrototypePlacedObjectFieldData>(0),
                instancePrerequisites = new DungeonPrerequisite[0],
                linkedTriggerAreaIDs = new List<int>(0),
                assignedPathStartNode = 0
            };
            big_entrance.placedObjectPositions[0] = big_entrance.placedObjects[0].contentsBasePosition;
            big_entrance.placedObjectPositions[1] = big_entrance.placedObjects[1].contentsBasePosition;

            RoomBuilder.GenerateRoomLayout(big_entrance, "Large_Elevator_Entrance_Layout");

            MegaChallengeShrineTable.includedRooms = new WeightedRoomCollection();
            MegaChallengeShrineTable.includedRooms.elements = new List<WeightedRoom>();
            MegaChallengeShrineTable.includedRoomTables = new List<GenericRoomTable>(0);

            foreach (WeightedRoom weightedRoom in gungeon_challengeshrine_roomtable.includedRooms.elements) {
                MegaChallengeShrineTable.includedRooms.Add(weightedRoom);
            }
            foreach (WeightedRoom weightedRoom in mines_challengeshrine_roomtable.includedRooms.elements) {
                MegaChallengeShrineTable.includedRooms.Add(weightedRoom);
            }
            foreach (WeightedRoom weightedRoom in catacombs_challengeshrine_roomtable.includedRooms.elements) {
                MegaChallengeShrineTable.includedRooms.Add(weightedRoom);
            }
            foreach (WeightedRoom weightedRoom in forge_challengeshrine_roomtable.includedRooms.elements) {
                MegaChallengeShrineTable.includedRooms.Add(weightedRoom);
            }

            MegaMiniBossRoomTable.includedRooms = new WeightedRoomCollection();
            MegaMiniBossRoomTable.includedRooms.elements = new List<WeightedRoom>();
            MegaMiniBossRoomTable.includedRoomTables = new List<GenericRoomTable>(0);

            List<PrototypeDungeonRoom> m_CachedMiniBossRoomList = new List<PrototypeDungeonRoom>();
            foreach (WeightedRoom wRoom in blocknerminiboss_table_01.includedRooms.elements) {
                if (wRoom.room != null) { m_CachedMiniBossRoomList.Add(wRoom.room); }
            }

            foreach (WeightedRoom wRoom in phantomagunim_table_01.includedRooms.elements) {
                if (wRoom.room != null) { m_CachedMiniBossRoomList.Add(wRoom.room); }
            }

            minibossrooms = m_CachedMiniBossRoomList.ToArray();

            foreach (PrototypeDungeonRoom room in minibossrooms) { room.associatedMinimapIcon = fusebombroom01.associatedMinimapIcon; }

            foreach (WeightedRoom weightedRoom in blocknerminiboss_table_01.includedRooms.elements) {
                MegaMiniBossRoomTable.includedRooms.Add(ExpandRoomPrefabs.GenerateWeightedRoom(UnityEngine.Object.Instantiate(weightedRoom.room), LimitedCopies: false));
            }
            foreach (WeightedRoom weightedRoom in phantomagunim_table_01.includedRooms.elements) {
                MegaMiniBossRoomTable.includedRooms.Add(ExpandRoomPrefabs.GenerateWeightedRoom(UnityEngine.Object.Instantiate(weightedRoom.room), LimitedCopies: false));
            }

            foreach (WeightedRoom weightedRoom in MegaMiniBossRoomTable.includedRooms.elements) { weightedRoom.room.category = PrototypeDungeonRoom.RoomCategory.NORMAL; }



            // Special room table but without Black market.
            basic_special_rooms_noBlackMarket.name = "Special Rooms (no blackmarket)";
            basic_special_rooms_noBlackMarket.includedRooms = new WeightedRoomCollection();
            basic_special_rooms_noBlackMarket.includedRooms.elements = new List<WeightedRoom>();
            basic_special_rooms_noBlackMarket.includedRoomTables = new List<GenericRoomTable>(0);
            basic_special_rooms_noBlackMarket.includedRooms.elements.Add(basic_special_rooms.includedRooms.elements[0]);
            basic_special_rooms_noBlackMarket.includedRooms.elements.Add(basic_special_rooms.includedRooms.elements[1]);
            basic_special_rooms_noBlackMarket.includedRooms.elements.Add(basic_special_rooms.includedRooms.elements[3]);
            basic_special_rooms_noBlackMarket.includedRooms.elements.Add(basic_special_rooms.includedRooms.elements[4]);

            

            if (MetalCubeGuy) {
                MetalCubeGuy.AddComponent<ExpandExplodeOnDeath>();
                MetalCubeGuy.GetComponent<AIActor>().IsHarmlessEnemy = true;
                ExpandExplodeOnDeath metalcubeguyExploder = MetalCubeGuy.GetComponent<ExpandExplodeOnDeath>();
                metalcubeguyExploder.deathType = OnDeathBehavior.DeathType.Death;
                ZeldaChargeBehavior zeldaChargeComponent = MetalCubeGuy.GetComponent<BehaviorSpeculator>().AttackBehaviors[0] as ZeldaChargeBehavior;
                zeldaChargeComponent.primeAnim = null;
                MetalCubeGuy.gameObject.AddComponent<ExpandThwompManager>();
                MetalCubeGuy.GetComponent<BehaviorSpeculator>().PostAwakenDelay = 0;
            }

            if (SkusketHead) { SkusketHead.GetComponent<AIActor>().DiesOnCollison = true; }

            CandleGuy = EnemyDatabase.GetOrLoadByGuid("eeb33c3a5a8e4eaaaaf39a743e8767bc").gameObject;
            if (CandleGuy) { CandleGuy.AddComponent<ExpandCandleGuyEngageDoer>(); }


            WallMimic = EnemyDatabase.GetOrLoadByGuid("479556d05c7c44f3b6abb3b2067fc778").gameObject;
            if (WallMimic) {
                UnityEngine.Object.Destroy(WallMimic.GetComponent<WallMimicController>());
                WallMimic.AddComponent<ExpandWallMimicManager>();
            }

            
            List<AGDEnemyReplacementTier> ReplacementTiers = GameManager.Instance.EnemyReplacementTiers;

            if (ReplacementTiers != null && ReplacementTiers.Count > 0) {
                foreach (AGDEnemyReplacementTier replacementTier in ReplacementTiers) {
                    if (replacementTier.RoomCantContain == null) { replacementTier.RoomCantContain = new List<string>(); }
                    replacementTier.RoomCantContain.Add(MetalCubeGuy.GetComponent<AIActor>().EnemyGuid);
                }
            }
            
            List<WeightedRoom> m_wRoomList = new List<WeightedRoom>();

            foreach (WeightedRoom wRoom in CustomRoomTableSecretGlitchFloor.includedRooms.elements) {
                if (wRoom.room != null && wRoom.room.FullCellData != null) {
                    foreach (PrototypeDungeonRoomCellData cellData in wRoom.room.FullCellData) {
                        if (cellData.state == CellType.PIT) { m_wRoomList.Add(wRoom); }
                    }
                }
            }

            if (m_wRoomList.Count > 0) {
                foreach (WeightedRoom wRoom in m_wRoomList) { CustomRoomTableSecretGlitchFloor.includedRooms.elements.Remove(wRoom); }
                m_wRoomList.Clear();
            }

            List<PrototypeDungeonRoom> m_WinchesterRooms = new List<PrototypeDungeonRoom>();

            foreach (WeightedRoom wRoom in winchesterroomtable.includedRooms.elements) { m_WinchesterRooms.Add(wRoom.room); }

            if (m_WinchesterRooms.Count > 0) {
                foreach (PrototypeDungeonRoom winchesterRoom in m_WinchesterRooms) {
                    winchesterRoom.associatedMinimapIcon = ExpandObjectDatabase.WinchesterMinimapIcon;
                }
            }


            RatTrapPlacable = ScriptableObject.CreateInstance<DungeonPlaceable>();
            RatTrapPlacable.name = "Rat Trap Placable";
            RatTrapPlacable.width = 1;
            RatTrapPlacable.height = 1;
            RatTrapPlacable.isPassable = true;
            RatTrapPlacable.roomSequential = false;
            RatTrapPlacable.respectsEncounterableDifferentiator = false;
            RatTrapPlacable.UsePrefabTransformOffset = false;
            RatTrapPlacable.MarkSpawnedItemsAsRatIgnored = false;
            RatTrapPlacable.DebugThisPlaceable = false;
            RatTrapPlacable.IsAnnexTable = false;
            RatTrapPlacable.variantTiers = new List<DungeonPlaceableVariant>() {
                new DungeonPlaceableVariant() {
                    percentChance = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = MouseTrap1,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = MouseTrap2,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = MouseTrap3,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
            };

            DoppelgunnerMirror = expandSharedAssets1.LoadAsset<GameObject>("DoppelgunnerMirror");
            
            tk2dSprite MirrorBaseSprite = SpriteSerializer.AddSpriteToObject(DoppelgunnerMirror, ExpandCustomEnemyDatabase.GungeoneerMimicCollection, "PlayerMimicMirror_Base");

            List<string> m_MirrorMimicFadeInSprites = new List<string>() {
                "PlayerMimicMirror_MimicFadeIn_01",
                "PlayerMimicMirror_MimicFadeIn_02",
                "PlayerMimicMirror_MimicFadeIn_03",
                "PlayerMimicMirror_MimicFadeIn_04",
                "PlayerMimicMirror_MimicFadeIn_05",
                "PlayerMimicMirror_MimicFadeIn_06",
                "PlayerMimicMirror_MimicFadeIn_07",
                "PlayerMimicMirror_MimicFadeIn_08",
                "PlayerMimicMirror_MimicFadeIn_09",
                "PlayerMimicMirror_MimicFadeIn_10"
            };

            List<string> m_MirrorCrackSprites = new List<string>() {
                "PlayerMimicMirror_MimicCrack_01",
                "PlayerMimicMirror_MimicCrack_02",
                "PlayerMimicMirror_MimicCrack_03",
                "PlayerMimicMirror_MimicCrack_04",
                "PlayerMimicMirror_MimicCrack_05"
            };

            List<string> m_MirrorShatterFXSprites = new List<string>() {
                "PlayerMimicMirror_ShatterDebris_01",
                "PlayerMimicMirror_ShatterDebris_02",
                "PlayerMimicMirror_ShatterDebris_03",
                "PlayerMimicMirror_ShatterDebris_04",
                "PlayerMimicMirror_ShatterDebris_05",
                "PlayerMimicMirror_ShatterDebris_06",
                "PlayerMimicMirror_ShatterDebris_07",
                "PlayerMimicMirror_ShatterDebris_08",
                "PlayerMimicMirror_ShatterDebris_09",
                "PlayerMimicMirror_ShatterDebris_10"
            };

            ExpandUtility.GenerateSpriteAnimator(DoppelgunnerMirror, AnimateDuringBossIntros: true, AlwaysIgnoreTimeScale: true, ignoreTimeScale: true);
            ExpandUtility.AddAnimation(DoppelgunnerMirror.GetComponent<tk2dSpriteAnimator>(), ExpandCustomEnemyDatabase.GungeoneerMimicCollection.GetComponent<tk2dSpriteCollectionData>(), m_MirrorMimicFadeInSprites, "PlayerMimicFadeIn", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            ExpandUtility.AddAnimation(DoppelgunnerMirror.GetComponent<tk2dSpriteAnimator>(), ExpandCustomEnemyDatabase.GungeoneerMimicCollection.GetComponent<tk2dSpriteCollectionData>(), m_MirrorCrackSprites, "MirrorGlassCrack", tk2dSpriteAnimationClip.WrapMode.Once, 6);


            DoppelgunnerMirrorFX = expandSharedAssets1.LoadAsset<GameObject>("DoppelgunnerMirrorFX");

            tk2dSprite MimicMirrorFXSprite = SpriteSerializer.AddSpriteToObject(DoppelgunnerMirrorFX, ExpandCustomEnemyDatabase.GungeoneerMimicCollection, "PlayerMimicMirror_ShatterDebris_01");
            MimicMirrorFXSprite.HeightOffGround = 3.5f;

            ExpandUtility.GenerateSpriteAnimator(DoppelgunnerMirrorFX, AnimateDuringBossIntros: true, AlwaysIgnoreTimeScale: true, ignoreTimeScale: true);
            ExpandUtility.AddAnimation(DoppelgunnerMirrorFX.GetComponent<tk2dSpriteAnimator>(), ExpandCustomEnemyDatabase.GungeoneerMimicCollection.GetComponent<tk2dSpriteCollectionData>(), m_MirrorShatterFXSprites, "PlayerMimicShatter", tk2dSpriteAnimationClip.WrapMode.Once, 12);


            RoomCorruptionAmbience = expandSharedAssets1.LoadAsset<GameObject>("RoomCorruptionAmbience_Placable");
            
            RoomCorruptionAmbience.AddComponent<ExpandCorruptedRoomAmbiencePlacable>();
            
            EXAlarmMushroom = expandSharedAssets1.LoadAsset<GameObject>("EX Alarm Mushroom");
            tk2dSprite m_AlarmMushroomSprite = SpriteSerializer.AddSpriteToObject(EXAlarmMushroom, EXTrapCollection, "alarm_mushroom2_idle_001");
            m_AlarmMushroomSprite.HeightOffGround = -1;

            List<string> m_AlarmMushroom_idleSprites = new List<string>() {
                "alarm_mushroom2_idle_001",
                "alarm_mushroom2_idle_002",
                "alarm_mushroom2_idle_003",
                "alarm_mushroom2_idle_004",
                "alarm_mushroom2_idle_005"
            };

            List<string> m_AlarmMushroom_alarmSprites = new List<string>() {
                "alarm_mushroom2_alarm_001",
                "alarm_mushroom2_alarm_002",
                "alarm_mushroom2_alarm_003",
                "alarm_mushroom2_alarm_004",
                "alarm_mushroom2_alarm_005",
                "alarm_mushroom2_alarm_006"
            };

            List<string> m_AlarmMushroom_breakSprites = new List<string>() {
                "alarm_mushroom2_break_001",
                "alarm_mushroom2_break_002",
                "alarm_mushroom2_break_003",
                "alarm_mushroom2_break_004"
            };
            
            ExpandUtility.GenerateSpriteAnimator(EXAlarmMushroom, playAutomatically: true, ClipFps: 8);
            tk2dSpriteAnimator m_AlarmMushroomAnimator = EXAlarmMushroom.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_AlarmMushroomAnimator, EXTrapCollection.GetComponent<tk2dSpriteCollectionData>(), m_AlarmMushroom_idleSprites, "alarm_mushroom_idle", frameRate: 8, wrapMode: tk2dSpriteAnimationClip.WrapMode.LoopFidget, minFidgetDuration: 1, maxFidgetDuration: 2);
            ExpandUtility.AddAnimation(m_AlarmMushroomAnimator, EXTrapCollection.GetComponent<tk2dSpriteCollectionData>(), m_AlarmMushroom_alarmSprites, "alarm_mushroom_alarm", frameRate: 8, wrapMode: tk2dSpriteAnimationClip.WrapMode.Loop);
            ExpandUtility.AddAnimation(m_AlarmMushroomAnimator, EXTrapCollection.GetComponent<tk2dSpriteCollectionData>(), m_AlarmMushroom_breakSprites, "alarm_mushroom_break", frameRate: 8, wrapMode: tk2dSpriteAnimationClip.WrapMode.Once);
            SpeculativeRigidbody m_EXAlarmMushroomRigidBody = ExpandUtility.GenerateOrAddToRigidBody(EXAlarmMushroom, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(7, 10), offset: new IntVector2(2, 7));

            ExpandAlarmMushroomPlacable m_AlarmMushRoomPlacable = EXAlarmMushroom.AddComponent<ExpandAlarmMushroomPlacable>();
            m_AlarmMushRoomPlacable.TriggerVFX = braveResources.LoadAsset<GameObject>("EmergencyCrate").GetComponent<EmergencyCrateController>().landingTargetSprite;

            tk2dSprite m_PlungerShadowSprite = ExpandObjectDatabase.Plunger.transform.Find("TNT_Plunger_Shadow").gameObject.GetComponent<tk2dSprite>();

            GameObject m_AlarmMushroomShadowChild = EXAlarmMushroom.transform.Find("Shadow").gameObject;
            tk2dSprite m_AlarmMushroomShadowSprite = m_AlarmMushroomShadowChild.AddComponent<tk2dSprite>();
            tk2dSpriteCollectionData m_AlarmushroomShadowSpriteCollection = m_AlarmMushroomShadowChild.AddComponent<tk2dSpriteCollectionData>();
            ExpandUtility.DuplicateComponent(m_AlarmushroomShadowSpriteCollection, m_PlungerShadowSprite.Collection);
            m_AlarmMushroomShadowSprite.renderer.material = new Material(sharedAssets.LoadAsset<GameObject>("Chest_Collection").GetComponent<tk2dSpriteCollectionData>().materials[2]);
            m_AlarmMushroomShadowSprite.renderer.material.mainTexture = m_PlungerShadowSprite.Collection.spriteDefinitions[148].material.mainTexture;
            m_AlarmMushroomShadowSprite.renderer.materials = new Material[] { m_AlarmMushroomShadowSprite.renderer.material };
            m_AlarmushroomShadowSpriteCollection.spriteDefinitions[125].material = m_AlarmMushroomShadowSprite.renderer.material;
            m_AlarmMushroomShadowSprite.SetSprite(m_AlarmushroomShadowSpriteCollection, "alarm_mushroom2_shadow_001");
            m_AlarmMushroomShadowSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.FLAT;
            m_AlarmMushroomShadowSprite.HeightOffGround = -1.7f;
            m_PlungerShadowSprite = null;


            
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride = ScriptableObject.CreateInstance<DungeonPlaceable>();
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride.width = 1;
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride.height = 1;
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride.isPassable = true;
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride.roomSequential = false;
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride.respectsEncounterableDifferentiator = false;
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride.UsePrefabTransformOffset = false;
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride.MarkSpawnedItemsAsRatIgnored = false;
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride.DebugThisPlaceable = false;
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride.IsAnnexTable = false;
            m_AlarmMushRoomPlacable.EnemySpawnPlacableOverride.variantTiers = new List<DungeonPlaceableVariant>() {
                new DungeonPlaceableVariant() {
                    percentChance = 50,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 25,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "f905765488874846b7ff257ff81d6d0c", // fungun
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 25,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 25,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "db35531e66ce41cbb81d507a34366dfe", // ak47_bullet_kin
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 50,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 60,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "3cadf10c489b461f9fb8814abc1a09c1", // minelet
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                },
                new DungeonPlaceableVariant() {
                    percentChance = 30,
                    percentChanceMultiplier = 1,
                    unitOffset = Vector2.zero,
                    nonDatabasePlaceable = null,
                    enemyPlaceableGuid = "df7fb62405dc4697b7721862c7b6b3cd", // treadnaughts_bullet_kin
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                }
            };
            
            EXSawBladeTrap_4x4Zone = expandSharedAssets1.LoadAsset<GameObject>("EX SawBlade PlacableObject");
            EXSawBladeTrap_4x4Zone.AddComponent<ExpandSawBladeTrapPlaceable>();

            ExpandBootlegRoomPlaceable.BuildPrefab(expandSharedAssets1);
            
            GameObject m_RedChestReference = ExpandObjectDatabase.ChestRed;

            RickRollChestObject = expandSharedAssets1.LoadAsset<GameObject>("Expand_RickRollChest");
            if (m_RedChestReference.transform.Find("Shadow").gameObject) {
                GameObject RickRollChestShadow = RickRollChestObject.transform.Find("Expand_RickRollChestShadow").gameObject;
                tk2dSprite RickRollChestShadowSprite = RickRollChestShadow.AddComponent<tk2dSprite>();
                ExpandUtility.DuplicateSprite(RickRollChestShadowSprite, m_RedChestReference.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>());
            }
            
            EX_GlitchPortal = expandSharedAssets1.LoadAsset<GameObject>("EX_GlitchPortal");

            GameObject m_ParadoxPortal = BraveResources.Load<GameObject>("Global Prefabs/VFX_ParadoxPortal");
            if (m_ParadoxPortal) {
                MeshRenderer m_EXGlitchPortalRenderer = EX_GlitchPortal.GetComponent<MeshRenderer>();
                if (m_ParadoxPortal?.GetComponent<MeshRenderer>()?.material) {
                    // m_EXGlitchPortalRenderer.materials = new Material[] { new Material(m_ParadoxPortal.GetComponent<MeshRenderer>().materials[0]) };
                    m_EXGlitchPortalRenderer.material = new Material(m_ParadoxPortal.GetComponent<MeshRenderer>().material);
                    m_EXGlitchPortalRenderer.material.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
                    m_EXGlitchPortalRenderer.material.SetTexture("_PortalTex", expandSharedAssets1.LoadAsset<Texture2D>("EX_GlitchPortalDefaultTexture"));
                }
            }

            ForceToSortingLayer m_EXGlitchPortalSortingLayer = m_ParadoxPortal.AddComponent<ForceToSortingLayer>();
            m_EXGlitchPortalSortingLayer.sortingLayer = DepthLookupManager.GungeonSortingLayer.PLAYFIELD;
            m_EXGlitchPortalSortingLayer.targetSortingOrder = -1;
            EX_GlitchPortal.AddComponent<ExpandGlitchPortalController>();

            m_ParadoxPortal = null;
            

            tk2dSprite RickRollChestSprite = RickRollChestObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(RickRollChestSprite, m_RedChestReference.GetComponent<tk2dSprite>());

            tk2dSpriteAnimator RickRollChestAnimator = RickRollChestObject.AddComponent<tk2dSpriteAnimator>();
            RickRollChestAnimator.Library = m_RedChestReference.GetComponent<tk2dSpriteAnimator>().Library;
            RickRollChestAnimator.DefaultClipId = 8;
            RickRollChestAnimator.AdditionalCameraVisibilityRadius = 0;
            RickRollChestAnimator.AlwaysIgnoreTimeScale = false;
            RickRollChestAnimator.ForceSetEveryFrame = false;
            RickRollChestAnimator.playAutomatically = false;
            RickRollChestAnimator.IsFrameBlendedAnimation = false;
            RickRollChestAnimator.clipTime = 0;
            RickRollChestAnimator.deferNextStartClip = false;

            SpeculativeRigidbody RickRollChestRigidBody = RickRollChestObject.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateRigidBody(RickRollChestRigidBody, m_RedChestReference.GetComponent<SpeculativeRigidbody>());

            MajorBreakable RickRollChestBreakable = RickRollChestObject.AddComponent<MajorBreakable>();
            RickRollChestBreakable.HitPoints = 40;
            RickRollChestBreakable.DamageReduction = 0;
            RickRollChestBreakable.MinHits = 0;
            RickRollChestBreakable.EnemyDamageOverride = -1;
            RickRollChestBreakable.ImmuneToBeastMode = false;
            RickRollChestBreakable.ScaleWithEnemyHealth = false;
            RickRollChestBreakable.OnlyExplosions = false;
            RickRollChestBreakable.IgnoreExplosions = false;
            RickRollChestBreakable.GameActorMotionBreaks = false;
            RickRollChestBreakable.PlayerRollingBreaks = false;
            RickRollChestBreakable.spawnShards = true;
            RickRollChestBreakable.distributeShards = false;
            RickRollChestBreakable.shardClusters = new ShardCluster[0];
            RickRollChestBreakable.minShardPercentSpeed = 0.05f;
            RickRollChestBreakable.maxShardPercentSpeed = 0.3f;
            RickRollChestBreakable.shardBreakStyle = MinorBreakable.BreakStyle.CONE;
            RickRollChestBreakable.usesTemporaryZeroHitPointsState = true;
            RickRollChestBreakable.spriteNameToUseAtZeroHP= "chest_redgold_break_001";
            RickRollChestBreakable.destroyedOnBreak = false;
            RickRollChestBreakable.childrenToDestroy = new List<GameObject>(0);
            RickRollChestBreakable.playsAnimationOnNotBroken = false;
            RickRollChestBreakable.notBreakAnimation = string.Empty;
            RickRollChestBreakable.handlesOwnBreakAnimation = false;
            RickRollChestBreakable.breakAnimation = string.Empty;
            RickRollChestBreakable.handlesOwnPrebreakFrames = false;
            RickRollChestBreakable.prebreakFrames = new BreakFrame[0];
            RickRollChestBreakable.damageVfx = new VFXPool { type = VFXPoolType.None, effects = new VFXComplex[0] };
            RickRollChestBreakable.damageVfxMinTimeBetween = 0.2f;
            RickRollChestBreakable.breakVfx = new VFXPool { type = VFXPoolType.None, effects = new VFXComplex[0] };
            RickRollChestBreakable.breakVfxParent = null;
            RickRollChestBreakable.delayDamageVfx = false;
            RickRollChestBreakable.SpawnItemOnBreak = true;
            RickRollChestBreakable.ItemIdToSpawnOnBreak = GlobalItemIds.Junk;
            RickRollChestBreakable.HandlePathBlocking = false;
                        
            List<string> m_RickRollRiseFrames = new List<string>() {
                "RickRoll_RiseUp_01",
                "RickRoll_RiseUp_02",
                "RickRoll_RiseUp_03",
                "RickRoll_RiseUp_04",
                "RickRoll_RiseUp_05",
                "RickRoll_RiseUp_06",
                "RickRoll_RiseUp_07",
                "RickRoll_RiseUp_08",
                "RickRoll_RiseUp_09",
                "RickRoll_RiseUp_10",
                "RickRoll_RiseUp_11",
                "RickRoll_RiseUp_12"
            };

            List<string> m_RickRollFrames = new List<string>() {
                "RickRoll_01",
                "RickRoll_02",
                "RickRoll_03",
                "RickRoll_04",
                "RickRoll_05",
                "RickRoll_06",
                "RickRoll_07",
                "RickRoll_08",
                "RickRoll_09",
                "RickRoll_10",
                "RickRoll_11",
                "RickRoll_12",
                "RickRoll_13",
                "RickRoll_14",
                "RickRoll_15",
                "RickRoll_16",
                "RickRoll_17",
                "RickRoll_18",
                "RickRoll_19",
                "RickRoll_20",
                "RickRoll_21",
                "RickRoll_22",
                "RickRoll_23",
                "RickRoll_24",
                "RickRoll_25",
                "RickRoll_26",
                "RickRoll_27",
                "RickRoll_28"
            };

            List<string> m_RickRollMusicSwitchFrames = new List<string>() {
                "music_switch_idle_off_001",
                "music_switch_activate_001",
                "music_switch_activate_002",
                "music_switch_activate_002_reverse",
                "music_switch_activate_003",
                "music_switch_activate_004",
                "music_switch_activate_005"
            };

            List<string> m_RickRollMusicSwitchTurnOffFrames = new List<string>() {                
                "music_switch_activate_001",
                "music_switch_activate_002",
                "music_switch_activate_003",
                "music_switch_activate_004",
                "music_switch_activate_005",
                "music_switch_idle_off_001"
            };

            List<string> m_RickRollMusicSwitchTurnOnFrames = new List<string>() {                
                "music_switch_activate_005",
                "music_switch_activate_004",
                "music_switch_activate_003",
                "music_switch_activate_002_reverse",
                "music_switch_activate_001",
                "music_switch_idle_on_001"
            };

            RickRollAnimationObject = expandSharedAssets1.LoadAsset<GameObject>("Expand_RickRollAnimation");
            tk2dBaseSprite m_RickRollBaseSprite = SpriteSerializer.AddSpriteToObject(RickRollAnimationObject, EXTrapCollection, "RickRoll_RiseUp_01");
            

            ExpandUtility.GenerateSpriteAnimator(RickRollAnimationObject, DefaultClipId: 0);
            ExpandUtility.AddAnimation(RickRollAnimationObject.GetComponent<tk2dSpriteAnimator>(),EXTrapCollection.GetComponent<tk2dSpriteCollectionData>(), m_RickRollRiseFrames, "RickRollAnimation_Rise", tk2dSpriteAnimationClip.WrapMode.Once, frameRate: 12);
            ExpandUtility.AddAnimation(RickRollAnimationObject.GetComponent<tk2dSpriteAnimator>(), EXTrapCollection.GetComponent<tk2dSpriteCollectionData>(), m_RickRollFrames, "RickRollAnimation", tk2dSpriteAnimationClip.WrapMode.Loop, frameRate: 12);

            ExpandFakeChest RickRollChestComponent = RickRollChestObject.AddComponent<ExpandFakeChest>();
            RickRollChestComponent.RickRollAnimationObject = RickRollAnimationObject;
            RickRollChestComponent.MinimapIconPrefab = m_RedChestReference.GetComponent<Chest>().MinimapIconPrefab;
            RickRollChestComponent.breakAnimName = m_RedChestReference.GetComponent<Chest>().breakAnimName;
            RickRollChestComponent.openAnimName = m_RedChestReference.GetComponent<Chest>().openAnimName;

            RickRollMusicSwitchObject = expandSharedAssets1.LoadAsset<GameObject>("ExpandRickRoll_MusicSwitch");
            RickRollMusicSwitchObject.layer = LayerMask.NameToLayer("FG_Critical");
            tk2dSprite RickRollSwitchSprite = SpriteSerializer.AddSpriteToObject(RickRollMusicSwitchObject, EXTrapCollection, "music_switch_idle_on_001");
            
            ExpandUtility.GenerateSpriteAnimator(RickRollMusicSwitchObject, DefaultClipId: 0);
            ExpandUtility.AddAnimation(RickRollMusicSwitchObject.GetComponent<tk2dSpriteAnimator>(), EXTrapCollection.GetComponent<tk2dSpriteCollectionData>(), m_RickRollMusicSwitchTurnOnFrames, "RickRollSwitch_TurnOn", tk2dSpriteAnimationClip.WrapMode.Once, frameRate: 12);
            ExpandUtility.AddAnimation(RickRollMusicSwitchObject.GetComponent<tk2dSpriteAnimator>(), EXTrapCollection.GetComponent<tk2dSpriteCollectionData>(), m_RickRollMusicSwitchTurnOffFrames, "RickRollSwitch_TurnOff", tk2dSpriteAnimationClip.WrapMode.Once, frameRate: 12);

            ExpandFakeChest RickRollChest_SwitchComponent = RickRollMusicSwitchObject.AddComponent<ExpandFakeChest>();
            RickRollChest_SwitchComponent.chestType = ExpandFakeChest.ChestType.MusicSwitch;
            
            RoomBuilder.AddObjectToRoom(gungeon_entrance, new Vector2(12, 20), ExpandUtility.GenerateDungeonPlacable(RickRollMusicSwitchObject, useExternalPrefab: true), xOffset: 12, yOffset: 6);
            RoomBuilder.AddObjectToRoom(gungeon_entrance_bossrush, new Vector2(12, 20), ExpandUtility.GenerateDungeonPlacable(RickRollMusicSwitchObject, useExternalPrefab: true), xOffset: 12, yOffset: 6);
            

            GameObject m_BrownChestReference = ExpandObjectDatabase.ChestBrownTwoItems;

            SurpriseChestObject = expandSharedAssets1.LoadAsset<GameObject>("Expand_SurpriseChest");

            if (m_BrownChestReference.transform.Find("Shadow").gameObject) {
                GameObject SurpriseChestChestShadow = SurpriseChestObject.transform.Find("Expand_SurpriseChestShadow").gameObject;
                tk2dSprite SurpriseChestShadowSprite = SurpriseChestChestShadow.AddComponent<tk2dSprite>();
                ExpandUtility.DuplicateSprite(SurpriseChestShadowSprite, m_BrownChestReference.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>());
            }
            
            tk2dSprite SurpriseChestSprite = SurpriseChestObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(SurpriseChestSprite, m_BrownChestReference.GetComponent<tk2dSprite>());
            SurpriseChestSprite.SetSprite("coop_chest_idle_001"); // coop_chest_open_001

            tk2dSpriteAnimator SurpriseChestAnimator = SurpriseChestObject.AddComponent<tk2dSpriteAnimator>();
            SurpriseChestAnimator.Library = m_BrownChestReference.GetComponent<tk2dSpriteAnimator>().Library;
            SurpriseChestAnimator.DefaultClipId = 31;
            SurpriseChestAnimator.AdditionalCameraVisibilityRadius = 0;
            SurpriseChestAnimator.AlwaysIgnoreTimeScale = false;
            SurpriseChestAnimator.ForceSetEveryFrame = false;
            SurpriseChestAnimator.playAutomatically = false;
            SurpriseChestAnimator.IsFrameBlendedAnimation = false;
            SurpriseChestAnimator.clipTime = 0;
            SurpriseChestAnimator.deferNextStartClip = false;

            SpeculativeRigidbody SurpriseChestRigidBody = SurpriseChestObject.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateRigidBody(SurpriseChestRigidBody, m_BrownChestReference.GetComponent<SpeculativeRigidbody>());

            PixelCollider SurpriseChestPixelCollider = SurpriseChestRigidBody.PrimaryPixelCollider;
            SurpriseChestPixelCollider.ManualOffsetX = 3;
            SurpriseChestPixelCollider.ManualOffsetY = 0;
            SurpriseChestPixelCollider.ManualWidth = 25;
            SurpriseChestPixelCollider.ManualHeight = 14;

            MajorBreakable SurpriseChestBreakable = SurpriseChestObject.AddComponent<MajorBreakable>();
            SurpriseChestBreakable.HitPoints = 40;
            SurpriseChestBreakable.DamageReduction = 0;
            SurpriseChestBreakable.MinHits = 0;
            SurpriseChestBreakable.EnemyDamageOverride = -1;
            SurpriseChestBreakable.ImmuneToBeastMode = false;
            SurpriseChestBreakable.ScaleWithEnemyHealth = false;
            SurpriseChestBreakable.OnlyExplosions = false;
            SurpriseChestBreakable.IgnoreExplosions = false;
            SurpriseChestBreakable.GameActorMotionBreaks = false;
            SurpriseChestBreakable.PlayerRollingBreaks = false;
            SurpriseChestBreakable.spawnShards = true;
            SurpriseChestBreakable.distributeShards = false;
            SurpriseChestBreakable.shardClusters = new ShardCluster[0];
            SurpriseChestBreakable.minShardPercentSpeed = 0.05f;
            SurpriseChestBreakable.maxShardPercentSpeed = 0.3f;
            SurpriseChestBreakable.shardBreakStyle = MinorBreakable.BreakStyle.CONE;
            SurpriseChestBreakable.usesTemporaryZeroHitPointsState = true;
            SurpriseChestBreakable.overrideSpriteNameToUseAtZeroHP = "coop_chest_break001";
            SurpriseChestBreakable.destroyedOnBreak = false;
            SurpriseChestBreakable.childrenToDestroy = new List<GameObject>(0);
            SurpriseChestBreakable.playsAnimationOnNotBroken = false;
            SurpriseChestBreakable.notBreakAnimation = string.Empty;
            SurpriseChestBreakable.handlesOwnBreakAnimation = false;
            SurpriseChestBreakable.breakAnimation = string.Empty;
            SurpriseChestBreakable.handlesOwnPrebreakFrames = false;
            SurpriseChestBreakable.prebreakFrames = new BreakFrame[0];
            SurpriseChestBreakable.damageVfx = new VFXPool { type = VFXPoolType.None, effects = new VFXComplex[0] };
            SurpriseChestBreakable.damageVfxMinTimeBetween = 0.2f;
            SurpriseChestBreakable.breakVfx = new VFXPool { type = VFXPoolType.None, effects = new VFXComplex[0] };
            SurpriseChestBreakable.breakVfxParent = null;
            SurpriseChestBreakable.delayDamageVfx = false;
            SurpriseChestBreakable.SpawnItemOnBreak = true;
            SurpriseChestBreakable.ItemIdToSpawnOnBreak = GlobalItemIds.Junk;
            SurpriseChestBreakable.HandlePathBlocking = false;

            ExpandFakeChest SurpriseChestComponent = SurpriseChestObject.AddComponent<ExpandFakeChest>();
            SurpriseChestComponent.chestType = ExpandFakeChest.ChestType.SurpriseChest;
            SurpriseChestComponent.MinimapIconPrefab = m_BrownChestReference.GetComponent<Chest>().MinimapIconPrefab;
            SurpriseChestComponent.breakAnimName = "coop_chest_break";
            SurpriseChestComponent.openAnimName = "coop_chest_open";
            

            ExpandThunderstormPlaceable = expandSharedAssets1.LoadAsset<GameObject>("ExpandThunderStorm");
            ExpandThunderstormPlaceable.AddComponent<ExpandThunderStormPlacable>();
            


            Door_Horizontal_Jungle = UnityEngine.Object.Instantiate(ForgeDungeonPrefab.doorObjects.variantTiers[0].nonDatabasePlaceable);
            Door_Horizontal_Jungle.SetActive(false);
            Door_Vertical_Jungle = UnityEngine.Object.Instantiate(ForgeDungeonPrefab.doorObjects.variantTiers[1].nonDatabasePlaceable);
            Door_Vertical_Jungle.SetActive(false);

            DungeonDoorController Door_Horizontal_Jungle_Controller = Door_Horizontal_Jungle.GetComponent<DungeonDoorController>();            
            Door_Horizontal_Jungle_Controller.sealAnimationName = "jungle_blocker_horizontal_down";
            Door_Horizontal_Jungle_Controller.unsealAnimationName = "jungle_blocker_horizontal_up";
            Door_Horizontal_Jungle_Controller.doorModules[0].openAnimationName = "jungle_door_east_top_open";
            Door_Horizontal_Jungle_Controller.doorModules[0].closeAnimationName = "jungle_door_east_top_close";
            Door_Horizontal_Jungle_Controller.doorModules[1].openAnimationName = "jungle_door_east_bottom_open";
            Door_Horizontal_Jungle_Controller.doorModules[1].closeAnimationName = "jungle_door_east_bottom_close";
            Door_Horizontal_Jungle_Controller.gameObject.transform.Find("DoorTop").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("jungle_door_horizontal_top_001");
            Door_Horizontal_Jungle_Controller.gameObject.transform.Find("DoorBottom").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("jungle_door_horizontal_bottom_001");


            DungeonDoorController Door_Vertical_Jungle_Controller = Door_Vertical_Jungle.GetComponent<DungeonDoorController>();
            Door_Vertical_Jungle_Controller.sealAnimationName = "jungle_blocker_vertical_down";
            Door_Vertical_Jungle_Controller.unsealAnimationName = "jungle_blocker_vertical_up";
            Door_Vertical_Jungle_Controller.playerNearSealedAnimationName = "jungle_blocker_headshake";
            Door_Vertical_Jungle_Controller.doorModules[0].openAnimationName = "jungle_door_north_left_open";
            Door_Vertical_Jungle_Controller.doorModules[0].closeAnimationName = "jungle_door_north_left_close";
            Door_Vertical_Jungle_Controller.doorModules[1].openAnimationName = "jungle_door_north_right_open";
            Door_Vertical_Jungle_Controller.doorModules[1].closeAnimationName = "jungle_door_north_right_close";
            Door_Vertical_Jungle_Controller.gameObject.transform.Find("DoorLeft").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("jungle_door_north_left_001");
            Door_Vertical_Jungle_Controller.gameObject.transform.Find("DoorRight").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("jungle_door_north_right_001");


            // Jungle_Doors = UnityEngine.Object.Instantiate(ForgeDungeonPrefab.doorObjects);
            Jungle_Doors = ExpandUtility.DuplicateDungoenPlaceable(ForgeDungeonPrefab.doorObjects);
            Jungle_Doors.variantTiers[0].nonDatabasePlaceable = Door_Vertical_Jungle;
            Jungle_Doors.variantTiers[1].nonDatabasePlaceable = Door_Horizontal_Jungle;
            FakePrefab.MarkAsFakePrefab(Door_Horizontal_Jungle);
            FakePrefab.MarkAsFakePrefab(Door_Vertical_Jungle);
            UnityEngine.Object.DontDestroyOnLoad(Door_Horizontal_Jungle);
            UnityEngine.Object.DontDestroyOnLoad(Door_Vertical_Jungle);


            DoorOneWay_Vertical_Jungle = UnityEngine.Object.Instantiate(SewerDungeonPrefab.oneWayDoorObjects.variantTiers[0].nonDatabasePlaceable);
            GameObject m_DoorOneWay_Vertical_Jungle_Bottom = DoorOneWay_Vertical_Jungle.GetComponent<DungeonDoorController>().sealAnimators[0].gameObject;
            GameObject m_DoorOneWay_Vertical_Jungle_Top = m_DoorOneWay_Vertical_Jungle_Bottom.transform.Find("Door").gameObject;
            m_DoorOneWay_Vertical_Jungle_Bottom.GetComponent<tk2dSprite>().SetSprite("jungel_one_way_blocker_vertical_bottom_001");
            m_DoorOneWay_Vertical_Jungle_Top.GetComponent<tk2dSprite>().SetSprite("jungel_one_way_blocker_vertical_top_001");
            m_DoorOneWay_Vertical_Jungle_Top.transform.localPosition -= new Vector3(0, 0.1f);
            m_DoorOneWay_Vertical_Jungle_Top.GetComponent<tk2dSprite>().HeightOffGround = 2;
            m_DoorOneWay_Vertical_Jungle_Top.GetComponent<tk2dSprite>().UpdateZDepthLater();
            DoorOneWay_Vertical_Jungle.SetActive(false);
            
            DoorOneWay_Horizontal_Jungle = UnityEngine.Object.Instantiate(SewerDungeonPrefab.oneWayDoorObjects.variantTiers[1].nonDatabasePlaceable);
            GameObject m_DoorOneWay_Horizontal_Jungle_Bottom = DoorOneWay_Horizontal_Jungle.GetComponent<DungeonDoorController>().sealAnimators[0].gameObject;
            GameObject m_DoorOneWay_Horizontal_Jungle_Top = m_DoorOneWay_Horizontal_Jungle_Bottom.transform.Find("Door").gameObject;
            m_DoorOneWay_Horizontal_Jungle_Bottom.GetComponent<tk2dSprite>().SetSprite("jungel_one_way_blocker_horizontal_bottom_001");
            m_DoorOneWay_Horizontal_Jungle_Top.GetComponent<tk2dSprite>().SetSprite("jungel_one_way_blocker_horizontal_top_001");
            m_DoorOneWay_Horizontal_Jungle_Top.transform.localPosition -= new Vector3(0, 0.25f);
            m_DoorOneWay_Horizontal_Jungle_Top.GetComponent<tk2dSprite>().HeightOffGround = 2f;
            m_DoorOneWay_Horizontal_Jungle_Top.GetComponent<tk2dSprite>().UpdateZDepthLater();
            DoorOneWay_Horizontal_Jungle.SetActive(false);
                        

            FakePrefab.MarkAsFakePrefab(DoorOneWay_Vertical_Jungle);
            FakePrefab.MarkAsFakePrefab(DoorOneWay_Horizontal_Jungle);
            UnityEngine.Object.DontDestroyOnLoad(DoorOneWay_Vertical_Jungle);
            UnityEngine.Object.DontDestroyOnLoad(DoorOneWay_Horizontal_Jungle);

            Jungle_OneWayDoors = ExpandUtility.DuplicateDungoenPlaceable(CastleDungeonPrefab.oneWayDoorObjects);

            Jungle_OneWayDoors.variantTiers[0].nonDatabasePlaceable = DoorOneWay_Vertical_Jungle;
            Jungle_OneWayDoors.variantTiers[1].nonDatabasePlaceable = DoorOneWay_Horizontal_Jungle;



            DoorOneWay_Vertical_Office = UnityEngine.Object.Instantiate(SewerDungeonPrefab.oneWayDoorObjects.variantTiers[0].nonDatabasePlaceable);
            GameObject m_DoorOneWay_Vertical_Office_Bottom = DoorOneWay_Vertical_Office.GetComponent<DungeonDoorController>().sealAnimators[0].gameObject;
            GameObject m_DoorOneWay_Vertical_Office_Top = m_DoorOneWay_Vertical_Office_Bottom.transform.Find("Door").gameObject;
            m_DoorOneWay_Vertical_Office_Bottom.GetComponent<tk2dSprite>().SetSprite(EXOfficeCollection.GetComponent<tk2dSpriteCollectionData>(), "office_one_way_blocker_vertical_bottom_001");
            m_DoorOneWay_Vertical_Office_Top.GetComponent<tk2dSprite>().SetSprite(EXOfficeCollection.GetComponent<tk2dSpriteCollectionData>(), "office_one_way_blocker_vertical_top_001");
            m_DoorOneWay_Vertical_Office_Top.transform.localPosition -= new Vector3(0, 0.1f);
            m_DoorOneWay_Vertical_Office_Top.GetComponent<tk2dSprite>().HeightOffGround = 2;
            m_DoorOneWay_Vertical_Office_Top.GetComponent<tk2dSprite>().UpdateZDepthLater();
            DoorOneWay_Vertical_Office.SetActive(false);

            DoorOneWay_Horizontal_Office = UnityEngine.Object.Instantiate(SewerDungeonPrefab.oneWayDoorObjects.variantTiers[1].nonDatabasePlaceable);
            GameObject m_DoorOneWay_Horizontal_Office_Bottom = DoorOneWay_Horizontal_Office.GetComponent<DungeonDoorController>().sealAnimators[0].gameObject;
            GameObject m_DoorOneWay_Horizontal_Office_Top = m_DoorOneWay_Horizontal_Office_Bottom.transform.Find("Door").gameObject;
            m_DoorOneWay_Horizontal_Office_Bottom.GetComponent<tk2dSprite>().SetSprite(EXOfficeCollection.GetComponent<tk2dSpriteCollectionData>(), "office_one_way_blocker_horizontal_bottom_001");
            m_DoorOneWay_Horizontal_Office_Top.GetComponent<tk2dSprite>().SetSprite(EXOfficeCollection.GetComponent<tk2dSpriteCollectionData>(), "office_one_way_blocker_horizontal_top_001");
            m_DoorOneWay_Horizontal_Office_Top.transform.localPosition -= new Vector3(0, 0.25f);
            m_DoorOneWay_Horizontal_Office_Top.GetComponent<tk2dSprite>().HeightOffGround = 2f;
            m_DoorOneWay_Horizontal_Office_Top.GetComponent<tk2dSprite>().UpdateZDepthLater();
            DoorOneWay_Horizontal_Office.SetActive(false);


            FakePrefab.MarkAsFakePrefab(DoorOneWay_Vertical_Office);
            FakePrefab.MarkAsFakePrefab(DoorOneWay_Horizontal_Office);
            UnityEngine.Object.DontDestroyOnLoad(DoorOneWay_Vertical_Office);
            UnityEngine.Object.DontDestroyOnLoad(DoorOneWay_Horizontal_Office);

            Office_OneWayDoors = ExpandUtility.DuplicateDungoenPlaceable(CastleDungeonPrefab.oneWayDoorObjects);
            Office_OneWayDoors.variantTiers[0].nonDatabasePlaceable = DoorOneWay_Vertical_Office;
            Office_OneWayDoors.variantTiers[1].nonDatabasePlaceable = DoorOneWay_Horizontal_Office;

            
            Jungle_LargeTree = expandSharedAssets1.LoadAsset<GameObject>("ExpandJungle_Tree");
            tk2dSprite JungleTreeSprite = SpriteSerializer.AddSpriteToObject(Jungle_LargeTree, EXJungleCollection, "Jungle_Tree_Large");
            JungleTreeSprite.HeightOffGround = -8;


            GameObject Jungle_Large_Tree_Shadow = Jungle_LargeTree.transform.Find("shadow").gameObject;
            tk2dSprite TreeShadowSprite = SpriteSerializer.AddSpriteToObject(Jungle_Large_Tree_Shadow, EXJungleCollection, "Jungle_Tree_Large_Shadow");
            TreeShadowSprite.usesOverrideMaterial = true;
            TreeShadowSprite.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;
            // TreeShadowSprite.HeightOffGround = -18;
            TreeShadowSprite.HeightOffGround = -1.7f;

            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(23, 20), offset: new IntVector2(84, 39)); // EntranceBlocker
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(10, 20), offset: new IntVector2(74, 39)); // SideCollisions
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 20), offset: new IntVector2(107, 39)); // SideCollisions
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(41, 64), offset: new IntVector2(74, 59)); // Top Collision

            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(41, 64), offset: new IntVector2(74, 59)); // High Obstacle (For projectiles mostly)
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(41, 75), offset: new IntVector2(74, 48)); // Enemy Blocker. (Prevents enemies from being siide collision area)

            ExpandJungleTreeController JungleTreeController = Jungle_LargeTree.AddComponent<ExpandJungleTreeController>();
                        
            Jungle_LargeTreeTopFrame = expandSharedAssets1.LoadAsset<GameObject>("Jungle Tree Frame");
            tk2dSprite m_JungleLargeTreeTopFrameSprite = SpriteSerializer.AddSpriteToObject(Jungle_LargeTreeTopFrame, EXJungleCollection, "Jungle_Tree_Large_Frame");
            m_JungleLargeTreeTopFrameSprite.HeightOffGround = 3;
            JungleTreeController.JungleTreeTopFrame = Jungle_LargeTreeTopFrame;

            
            EXJungleTree_MinimapIcon = expandSharedAssets1.LoadAsset<GameObject>("EXJungle_TreeMinimapIcon");
            tk2dSprite m_jungleTreeMinimapIconSprite = SpriteSerializer.AddSpriteToObject(EXJungleTree_MinimapIcon, EXJungleCollection, "JungleTree_MinimapIcon");
            
            EXJungleCrest_MinimapIcon = expandSharedAssets1.LoadAsset<GameObject>("EXJungleCrest_MinimapIcon");
            tk2dSprite m_jungleCrestMinimapIconSprite = SpriteSerializer.AddSpriteToObject(EXJungleCrest_MinimapIcon, EXJungleCollection, "junglecrest_minimapicon");
            
            Jungle_ExitLadder = expandSharedAssets1.LoadAsset<GameObject>("ExpandJungle_ExitLadder");
            tk2dSprite m_jungleExitLadderSprite = SpriteSerializer.AddSpriteToObject(Jungle_ExitLadder, EXJungleCollection, "Jungle_ExitLadder");
            Jungle_ExitLadder.AddComponent<ExpandJungleExitLadderComponent>();
            
            Jungle_ExitLadder_Destination = expandSharedAssets1.LoadAsset<GameObject>("ExpandJungle_ExitLadder_Destination");
            tk2dSprite m_jungleExitLadderDestinationSprite = SpriteSerializer.AddSpriteToObject(Jungle_ExitLadder_Destination, EXJungleCollection, "Jungle_ExitLadder_Destination");
            m_jungleExitLadderDestinationSprite.HeightOffGround = -4;
            Jungle_ExitLadder_Destination.AddComponent<ExpandJungleExitLadderComponent>();

            Jungle_ExitLadder_Hole = expandSharedAssets1.LoadAsset<GameObject>("ExpandJungle_ExitLadder_Hole");
            tk2dSprite m_jungleExitLadderHoleSprite = SpriteSerializer.AddSpriteToObject(Jungle_ExitLadder_Hole, EXJungleCollection, "Jungle_ExitLadder_Destination_Hole", tk2dBaseSprite.PerpendicularState.FLAT);
            m_jungleExitLadderHoleSprite.HeightOffGround = -1.7f;
            Jungle_ExitLadder_Hole.SetLayerRecursively(LayerMask.NameToLayer("BG_Critical"));

            ExpandUtility.GenerateOrAddToRigidBody(Jungle_ExitLadder_Hole, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(30, 30), offset: IntVector2.One);


            Jungle_BlobLostSign = expandSharedAssets1.LoadAsset<GameObject>("Expand_JungleSign");
            ExpandUtility.BuildNewCustomSign(Jungle_BlobLostSign, Teleporter_Info_Sign, "Lost Blob Note", "This poor fella got lost on his way home.");
                        
            Jungle_ItemStump = expandSharedAssets1.LoadAsset<GameObject>("ExpandJungle_ItemStump");
            tk2dSprite m_jungleItemStumpSprite = SpriteSerializer.AddSpriteToObject(Jungle_ItemStump, EXJungleCollection, "Jungle_TreeStump");
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_ItemStump, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(3, 2), dimensions: new IntVector2(26, 24));
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_ItemStump, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(3, 2), dimensions: new IntVector2(26, 24));
            ExpandJungleTreeStumpItemPedestal StumpPedestal = Jungle_ItemStump.AddComponent<ExpandJungleTreeStumpItemPedestal>();
            StumpPedestal.ItemID = WoodenCrest.WoodCrestID;
            

            Door_Horizontal_Belly = UnityEngine.Object.Instantiate(NakatomiDungeonPrefab.doorObjects.variantTiers[0].nonDatabasePlaceable);
            Door_Vertical_Belly = UnityEngine.Object.Instantiate(NakatomiDungeonPrefab.doorObjects.variantTiers[1].nonDatabasePlaceable);

            Belly_DoorAnimations = sharedAssets2.LoadAsset<GameObject>("MonstroNakatomiWest_Door_Animation");
            foreach (tk2dSpriteAnimationClip clip in Belly_DoorAnimations.GetComponent<tk2dSpriteAnimation>().clips) {
                if (clip.name == "monstro_door_horizontall_top_open" | 
                    clip.name == "monstro_door_horizontall_bottom_open" |
                    clip.name == "monstro_door_vertical_left_open" |
                    clip.name == "monstro_door_vertical_right_open")
                {
                    clip.frames[0].eventAudio = "Play_EX_BellyDoor_Open";
                    clip.frames[0].triggerEvent = true;
                }
                if (clip.name == "monstro_door_horizontal_top_close" |
                    clip.name == "monstro_door_horizontal_bottom_close" |
                    clip.name == "monstro_door_vertical_left_close" |
                    clip.name == "monstro_door_vertical_right_close")
                {
                    clip.frames[0].eventAudio = "Play_EX_BellyDoor_Close";
                    clip.frames[0].triggerEvent = true;
                }
                if (clip.name == "monstro_blocker_horizontal_down" | clip.name == "monstro_blocker_vertical_down") {
                    clip.frames[0].eventAudio = "Play_EX_BellyDoor_Seal";
                    clip.frames[0].triggerEvent = true;
                }
                if (clip.name == "monstro_blocker_horizontal_up" | clip.name == "monstro_blocker_vertical_up") {
                    clip.frames[0].eventAudio = "Play_EX_BellyDoor_Unseal";
                    clip.frames[0].triggerEvent = true;
                }
            }

            Belly_Shipwreck_Left = expandSharedAssets1.LoadAsset<GameObject>("EXShipwreck_Left");
            Belly_Shipwreck_Right = expandSharedAssets1.LoadAsset<GameObject>("EXShipwreck_Right");
            tk2dSprite m_BellyShipwreckSprite_Left = SpriteSerializer.AddSpriteToObject(Belly_Shipwreck_Left, EXLargeMonsterCollection, "Shipwreck_Left");
            tk2dSprite m_BellyShipwreckSprite_Right = SpriteSerializer.AddSpriteToObject(Belly_Shipwreck_Right, EXLargeMonsterCollection, "Shipwreck_Right");
            Belly_Shipwreck_Left.GetComponent<tk2dSprite>().HeightOffGround = -8;
            Belly_Shipwreck_Right.GetComponent<tk2dSprite>().HeightOffGround = -8;


            DungeonDoorController Door_Horizontal_Belly_Controller = Door_Horizontal_Belly.GetComponent<DungeonDoorController>();
            Door_Horizontal_Belly_Controller.sealAnimationName = "monstro_blocker_horizontal_down";
            Door_Horizontal_Belly_Controller.unsealAnimationName = "monstro_blocker_horizontal_up";
            Door_Horizontal_Belly_Controller.playerNearSealedAnimationName = string.Empty;
            Door_Horizontal_Belly_Controller.doorModules[0].openAnimationName = "monstro_door_horizontall_top_open";
            Door_Horizontal_Belly_Controller.doorModules[0].closeAnimationName = "monstro_door_horizontal_top_close";
            Door_Horizontal_Belly_Controller.doorModules[0].horizontalFlips = false;
            Door_Horizontal_Belly_Controller.doorModules[1].openAnimationName = "monstro_door_horizontall_bottom_open";
            Door_Horizontal_Belly_Controller.doorModules[1].closeAnimationName = "monstro_door_horizontal_bottom_close";
            Door_Horizontal_Belly_Controller.doorModules[1].horizontalFlips = false;
            Door_Horizontal_Belly_Controller.doorModules[1].openDepth = 1;
            Door_Horizontal_Belly_Controller.doorModules[1].closedDepth = 1;
            Door_Horizontal_Belly_Controller.gameObject.transform.Find("BarsLeft").localPosition = new Vector3(0.375f, 0.75f, 1.5f);
            Door_Horizontal_Belly_Controller.gameObject.transform.Find("BarsRight").localPosition = new Vector3(1.0625f, 0.75f, 1.5f);
            Door_Horizontal_Belly_Controller.gameObject.transform.Find("DoorTop").localPosition = new Vector3(0.375f, -0.4375f, 4.8125f);
            Door_Horizontal_Belly_Controller.gameObject.transform.Find("DoorBottom").localPosition = new Vector3(0.375f, -0.4375f, 1.8125f);
            Door_Horizontal_Belly_Controller.gameObject.transform.Find("AO_Wall_Left").localPosition = new Vector3(1.0625f, 2.0625f, 3.0625f);
            Door_Horizontal_Belly_Controller.gameObject.transform.Find("AO_Wall_Right").localPosition = new Vector3(-0.5625f, 2.0625f, 3.0625f);
            Door_Horizontal_Belly_Controller.gameObject.transform.Find("AO_Floor_Left").localPosition = new Vector3(-0.5625f, 1.0625f, 2.0625f);
            Door_Horizontal_Belly_Controller.gameObject.transform.Find("AO_Floor_Right").localPosition = new Vector3(1.0625f, 1.0625f, 2.0625f);
            Door_Horizontal_Belly_Controller.gameObject.transform.Find("DoorTop").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("monstro_door_horizontal_top_001");
            Door_Horizontal_Belly_Controller.gameObject.transform.Find("DoorBottom").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("monstro_door_horizontal_bottom_001");
            UnityEngine.Object.Destroy(Door_Horizontal_Belly.gameObject.transform.Find("BarsLeft").gameObject.GetComponent<tk2dSpriteAnimator>());
            UnityEngine.Object.Destroy(Door_Horizontal_Belly.gameObject.transform.Find("BarsRight").gameObject.GetComponent<tk2dSpriteAnimator>());
            
            DungeonDoorController Door_Vertical_Belly_Controller = Door_Vertical_Belly.GetComponent<DungeonDoorController>();
            Door_Vertical_Belly_Controller.sealAnimationName = "monstro_blocker_vertical_down";
            Door_Vertical_Belly_Controller.unsealAnimationName = "monstro_blocker_vertical_up";
            Door_Vertical_Belly_Controller.playerNearSealedAnimationName = "monstro_blocker_headshake";
            Door_Vertical_Belly_Controller.doorModules[0].openAnimationName = "monstro_door_vertical_left_open";
            Door_Vertical_Belly_Controller.doorModules[0].closeAnimationName = "monstro_door_vertical_left_close";
            Door_Vertical_Belly_Controller.doorModules[0].openDepth = -1.25f;
            Door_Vertical_Belly_Controller.doorModules[0].closedDepth = -2.25f;
            Door_Vertical_Belly_Controller.doorModules[1].openAnimationName = "monstro_door_vertical_right_open";
            Door_Vertical_Belly_Controller.doorModules[1].closeAnimationName = "monstro_door_vertical_right_close";
            Door_Vertical_Belly_Controller.doorModules[1].openDepth = -1.25f;
            Door_Vertical_Belly_Controller.doorModules[1].closedDepth = -2.25f;
            Door_Vertical_Belly_Controller.gameObject.transform.Find("DoorLeft").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("monstro_door_vertical_open_left_001");
            Door_Vertical_Belly_Controller.gameObject.transform.Find("DoorRight").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("monstro_door_vertical_open_right_001");

            tk2dSpriteAnimation Door_Horizontal_Belly_Animation_Left = Door_Horizontal_Belly.gameObject.transform.Find("BarsLeft").gameObject.AddComponent<tk2dSpriteAnimation>();
            tk2dSpriteAnimation Door_Horizontal_Belly_Animation_Right = Door_Horizontal_Belly.gameObject.transform.Find("BarsRight").gameObject.AddComponent<tk2dSpriteAnimation>();
            Door_Horizontal_Belly_Animation_Left.clips = Door_Vertical_Belly_Controller.sealAnimators[0].Library.clips;
            Door_Horizontal_Belly_Animation_Right.clips = Door_Vertical_Belly_Controller.sealAnimators[0].Library.clips;

            tk2dSpriteAnimator Door_Horizontal_Belly_Animator_Left = Door_Horizontal_Belly.gameObject.transform.Find("BarsLeft").gameObject.AddComponent<tk2dSpriteAnimator>();
            Door_Horizontal_Belly_Animator_Left.Library = Door_Horizontal_Belly_Animation_Left;
            Door_Horizontal_Belly_Animator_Left.DefaultClipId = 1;
            Door_Horizontal_Belly_Animator_Left.AdditionalCameraVisibilityRadius = 0;
            Door_Horizontal_Belly_Animator_Left.AnimateDuringBossIntros = false;
            Door_Horizontal_Belly_Animator_Left.ForceSetEveryFrame = false;
            Door_Horizontal_Belly_Animator_Left.playAutomatically = false;
            Door_Horizontal_Belly_Animator_Left.IsFrameBlendedAnimation = false;
            Door_Horizontal_Belly_Animator_Left.clipTime = 0;
            Door_Horizontal_Belly_Animator_Left.deferNextStartClip = false;

            tk2dSpriteAnimator Door_Horizontal_Belly_Animator_Right = Door_Horizontal_Belly.gameObject.transform.Find("BarsRight").gameObject.AddComponent<tk2dSpriteAnimator>();
            Door_Horizontal_Belly_Animator_Right.Library = Door_Horizontal_Belly_Animation_Right;
            Door_Horizontal_Belly_Animator_Right.DefaultClipId = 1;
            Door_Horizontal_Belly_Animator_Right.AdditionalCameraVisibilityRadius = 0;
            Door_Horizontal_Belly_Animator_Right.AnimateDuringBossIntros = false;
            Door_Horizontal_Belly_Animator_Right.ForceSetEveryFrame = false;
            Door_Horizontal_Belly_Animator_Right.playAutomatically = false;
            Door_Horizontal_Belly_Animator_Right.IsFrameBlendedAnimation = false;
            Door_Horizontal_Belly_Animator_Right.clipTime = 0;
            Door_Horizontal_Belly_Animator_Right.deferNextStartClip = false;

            Door_Horizontal_Belly_Controller.sealAnimators = new tk2dSpriteAnimator[] { Door_Horizontal_Belly_Animator_Left, Door_Horizontal_Belly_Animator_Right };

            Door_Horizontal_Belly.SetActive(false);
            Door_Vertical_Belly.SetActive(false);

            // Belly_Doors = UnityEngine.Object.Instantiate(NakatomiDungeonPrefab.doorObjects);
            Belly_Doors = ExpandUtility.DuplicateDungoenPlaceable(NakatomiDungeonPrefab.doorObjects);
            Belly_Doors.variantTiers[0].nonDatabasePlaceable = Door_Vertical_Belly;
            Belly_Doors.variantTiers[1].nonDatabasePlaceable = Door_Horizontal_Belly;
            FakePrefab.MarkAsFakePrefab(Door_Vertical_Belly);
            FakePrefab.MarkAsFakePrefab(Door_Horizontal_Belly);
            UnityEngine.Object.DontDestroyOnLoad(Door_Vertical_Belly);
            UnityEngine.Object.DontDestroyOnLoad(Door_Horizontal_Belly);


            West_PuzzleSetupPlacable = expandSharedAssets1.LoadAsset<GameObject>("EXWest_PuzzleSetupPlacable");
            West_PuzzleSetupPlacable.AddComponent<ExpandWestPuzzleRoomController>();

            EXOldWestWarp = expandSharedAssets1.LoadAsset<GameObject>("EXOldWestWarp");
            ExpandUtility.GenerateOrAddToRigidBody(EXOldWestWarp, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(32, 28));

            ExpandWarpManager m_oldWestWarp = EXOldWestWarp.AddComponent<ExpandWarpManager>();
            m_oldWestWarp.warpType = ExpandWarpManager.WarpType.OldWestFloorWarp;


            Door_Horizontal_West = UnityEngine.Object.Instantiate(NakatomiDungeonPrefab.doorObjects.variantTiers[0].nonDatabasePlaceable);
            Door_Vertical_West = UnityEngine.Object.Instantiate(NakatomiDungeonPrefab.doorObjects.variantTiers[1].nonDatabasePlaceable);

            DungeonDoorController Door_Horizontal_West_Controller = Door_Horizontal_West.GetComponent<DungeonDoorController>();
            typeof(DungeonDoorController).GetField("doorClosesAfterEveryOpen", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Door_Horizontal_West_Controller, true);
            Door_Horizontal_West_Controller.sealAnimationName = "west_blocker_horizontal_down";
            Door_Horizontal_West_Controller.unsealAnimationName = "west_blocker_horizontal_up";
            Door_Horizontal_West_Controller.playerNearSealedAnimationName = string.Empty;
            Door_Horizontal_West_Controller.doorModules[0].openAnimationName = "west_door_east_top_open";
            Door_Horizontal_West_Controller.doorModules[0].closeAnimationName = "west_door_east_top_close";
            Door_Horizontal_West_Controller.doorModules[0].horizontalFlips = true;
            Door_Horizontal_West_Controller.doorModules[0].openPerpendicular = true;
            Door_Horizontal_West_Controller.doorModules[1].openDepth = -1;
            Door_Horizontal_West_Controller.doorModules[1].closedDepth = -1;
            Door_Horizontal_West_Controller.doorModules[1].openAnimationName = "west_door_east_bottom_open";
            Door_Horizontal_West_Controller.doorModules[1].closeAnimationName = "west_door_east_bottom_close";
            Door_Horizontal_West_Controller.doorModules[1].horizontalFlips = true;
            Door_Horizontal_West_Controller.doorModules[1].openPerpendicular = true;
            Door_Horizontal_West_Controller.doorModules[1].openDepth = -1;
            Door_Horizontal_West_Controller.doorModules[1].closedDepth = -1;
            Door_Horizontal_West_Controller.gameObject.transform.Find("BarsLeft").localPosition = new Vector3(0.375f, 0.5625f, -0.0625f);
            Door_Horizontal_West_Controller.gameObject.transform.Find("BarsRight").localPosition = new Vector3(0.625f, 0.5625f, 0.3125f);
            Door_Horizontal_West_Controller.gameObject.transform.Find("DoorTop").localPosition = new Vector3(0.4375f, -0.5f, 3f);
            Door_Horizontal_West_Controller.gameObject.transform.Find("DoorBottom").localPosition = new Vector3(0.4375f, -0.4375f, 0.4375f);
            Door_Horizontal_West_Controller.gameObject.transform.Find("AO_Wall_Left").localPosition = new Vector3(0.5625f, 2.0625f, 3.0625f);
            Door_Horizontal_West_Controller.gameObject.transform.Find("AO_Wall_Right").localPosition = new Vector3(-0.5625f, 2.0625f, 3.0625f);
            Door_Horizontal_West_Controller.gameObject.transform.Find("AO_Floor_Left").localPosition = new Vector3(-0.755f, 1.0625f, 2.0625f);
            Door_Horizontal_West_Controller.gameObject.transform.Find("AO_Floor_Right").localPosition = new Vector3(0.655f, 1.0625f, 2.0625f);


            Door_Horizontal_West_Controller.gameObject.transform.Find("DoorTop").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("west_door_horizontal_top_001");
            Door_Horizontal_West_Controller.gameObject.transform.Find("DoorBottom").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("west_door_horizontal_bottom_001");
            UnityEngine.Object.Destroy(Door_Horizontal_West.gameObject.transform.Find("BarsLeft").gameObject.GetComponent<tk2dSpriteAnimator>());
            UnityEngine.Object.Destroy(Door_Horizontal_West.gameObject.transform.Find("BarsRight").gameObject.GetComponent<tk2dSpriteAnimator>());

            DungeonDoorController Door_Vertical_West_Controller = Door_Vertical_West.GetComponent<DungeonDoorController>();
            typeof(DungeonDoorController).GetField("doorClosesAfterEveryOpen", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Door_Vertical_West_Controller, true);
            Door_Vertical_West_Controller.sealAnimationName = "west_blocker_vertical_down";
            Door_Vertical_West_Controller.unsealAnimationName = "west_blocker_vertical_up";
            Door_Vertical_West_Controller.playerNearSealedAnimationName = string.Empty;
            // Door_Vertical_West_Controller.hideSealAnimators = true;
            Door_Vertical_West_Controller.doorModules[0].openAnimationName = "west_door_north_left_open";
            Door_Vertical_West_Controller.doorModules[0].closeAnimationName = "west_door_north_left_close";
            Door_Vertical_West_Controller.doorModules[0].openDepth = -1.25f;
            Door_Vertical_West_Controller.doorModules[0].closedDepth = -2.25f;
            Door_Vertical_West_Controller.doorModules[0].openPerpendicular = true;
            Door_Vertical_West_Controller.doorModules[0].horizontalFlips = true;
            Door_Vertical_West_Controller.doorModules[1].openAnimationName = "west_door_north_right_open";
            Door_Vertical_West_Controller.doorModules[1].closeAnimationName = "west_door_north_right_close";
            Door_Vertical_West_Controller.doorModules[1].openDepth = -1.25f;
            Door_Vertical_West_Controller.doorModules[1].closedDepth = -2.25f;
            Door_Vertical_West_Controller.doorModules[0].openPerpendicular = true;
            Door_Vertical_West_Controller.doorModules[0].horizontalFlips = true;
            Door_Vertical_West_Controller.gameObject.transform.Find("DoorLeft").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("west_door_north_left_001");
            Door_Vertical_West_Controller.gameObject.transform.Find("DoorRight").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("west_door_north_right_001");
            
            tk2dSpriteAnimation Door_Horizontal_West_Animation_Left = Door_Horizontal_West.gameObject.transform.Find("BarsLeft").gameObject.AddComponent<tk2dSpriteAnimation>();
            tk2dSpriteAnimation Door_Horizontal_West_Animation_Right = Door_Horizontal_West.gameObject.transform.Find("BarsRight").gameObject.AddComponent<tk2dSpriteAnimation>();
            Door_Horizontal_West_Animation_Left.clips = Door_Vertical_West_Controller.sealAnimators[0].Library.clips;
            Door_Horizontal_West_Animation_Right.clips = Door_Vertical_West_Controller.sealAnimators[0].Library.clips;

            tk2dSpriteAnimator Door_Horizontal_West_Animator_Left = Door_Horizontal_West.gameObject.transform.Find("BarsLeft").gameObject.AddComponent<tk2dSpriteAnimator>();
            Door_Horizontal_West_Animator_Left.Library = Door_Horizontal_West_Animation_Left;
            Door_Horizontal_West_Animator_Left.DefaultClipId = 36;
            Door_Horizontal_West_Animator_Left.AdditionalCameraVisibilityRadius = 0;
            Door_Horizontal_West_Animator_Left.AnimateDuringBossIntros = false;
            Door_Horizontal_West_Animator_Left.ForceSetEveryFrame = false;
            Door_Horizontal_West_Animator_Left.playAutomatically = false;
            Door_Horizontal_West_Animator_Left.IsFrameBlendedAnimation = false;
            Door_Horizontal_West_Animator_Left.clipTime = 0;
            Door_Horizontal_West_Animator_Left.deferNextStartClip = false;

            tk2dSpriteAnimator Door_Horizontal_West_Animator_Right = Door_Horizontal_West.gameObject.transform.Find("BarsRight").gameObject.AddComponent<tk2dSpriteAnimator>();
            Door_Horizontal_West_Animator_Right.Library = Door_Horizontal_West_Animation_Right;
            Door_Horizontal_West_Animator_Right.DefaultClipId = 37;
            Door_Horizontal_West_Animator_Right.AdditionalCameraVisibilityRadius = 0;
            Door_Horizontal_West_Animator_Right.AnimateDuringBossIntros = false;
            Door_Horizontal_West_Animator_Right.ForceSetEveryFrame = false;
            Door_Horizontal_West_Animator_Right.playAutomatically = false;
            Door_Horizontal_West_Animator_Right.IsFrameBlendedAnimation = false;
            Door_Horizontal_West_Animator_Right.clipTime = 0;
            Door_Horizontal_West_Animator_Right.deferNextStartClip = false;
            
            Door_Horizontal_West_Controller.sealAnimators = new tk2dSpriteAnimator[] { Door_Horizontal_West_Animator_Left, Door_Horizontal_West_Animator_Right };
            
            Door_Horizontal_West.SetActive(false);
            Door_Vertical_West.SetActive(false);
            

            West_Doors = UnityEngine.Object.Instantiate(NakatomiDungeonPrefab.doorObjects);
            West_Doors.variantTiers[0].nonDatabasePlaceable = Door_Vertical_West;
            West_Doors.variantTiers[1].nonDatabasePlaceable = Door_Horizontal_West;
            FakePrefab.MarkAsFakePrefab(Door_Vertical_West);
            FakePrefab.MarkAsFakePrefab(Door_Horizontal_West);
            UnityEngine.Object.DontDestroyOnLoad(Door_Vertical_West);
            UnityEngine.Object.DontDestroyOnLoad(Door_Horizontal_West);


            // Sarcophagus Objects have unused sprites still in the game. I'll set them up to use them for my Belly entrance room for Gungeon Proper.
            // Todo: Make these into real prefabs.

            tk2dSprite m_Sarcophagus_ShotgunBookSprite = sharedAssets.LoadAsset<GameObject>("Sarcophagus_ShotgunBook").GetComponent<tk2dSprite>();

            Sarcophagus_ShotgunBook_Kaliber = expandSharedAssets1.LoadAsset<GameObject>("Sarcophagus_ShotgunBook_Kaliber");
            Sarcophagus_ShotgunMace_Kaliber = expandSharedAssets1.LoadAsset<GameObject>("Sarcophagus_ShotgunMace_Kaliber");
            Sarcophagus_BulletSword_Kaliber = expandSharedAssets1.LoadAsset<GameObject>("Sarcophagus_BulletSword_Kaliber");
            Sarcophagus_BulletShield_Kaliber = expandSharedAssets1.LoadAsset<GameObject>("Sarcophagus_BulletShield_Kaliber");
            tk2dSprite SarcophagusShotgunBookKaliberSprite = Sarcophagus_ShotgunBook_Kaliber.AddComponent<tk2dSprite>();
            tk2dSprite SarcophagusShotgunMaceKaliberSprite = Sarcophagus_ShotgunMace_Kaliber.AddComponent<tk2dSprite>();
            tk2dSprite SarcophagusBulletSwordKaliberSprite = Sarcophagus_BulletSword_Kaliber.AddComponent<tk2dSprite>();
            tk2dSprite SarcophagusBulletShieldKaliberSprite = Sarcophagus_BulletShield_Kaliber.AddComponent<tk2dSprite>();
            tk2dSprite SarcophagusShotgunBookKaliberShadowSprite = Sarcophagus_ShotgunBook_Kaliber.transform.Find("Shadow").gameObject.AddComponent<tk2dSprite>();
            tk2dSprite SarcophagusShotgunMaceKaliberShadowSprite = Sarcophagus_ShotgunMace_Kaliber.transform.Find("Shadow").gameObject.AddComponent<tk2dSprite>();
            tk2dSprite SarcophagusBulletSwordKaliberShadowSprite = Sarcophagus_BulletSword_Kaliber.transform.Find("Shadow").gameObject.AddComponent<tk2dSprite>();
            tk2dSprite SarcophagusBulletShieldKaliberShadowSprite = Sarcophagus_BulletShield_Kaliber.transform.Find("Shadow").gameObject.AddComponent<tk2dSprite>();
            SarcophagusShotgunBookKaliberSprite.SetSprite(m_Sarcophagus_ShotgunBookSprite.Collection, "sarco_shotbook_kaliber_001");
            SarcophagusShotgunMaceKaliberSprite.SetSprite(m_Sarcophagus_ShotgunBookSprite.Collection, "sarco_shotmace_kaliber_001");
            SarcophagusBulletSwordKaliberSprite.SetSprite(m_Sarcophagus_ShotgunBookSprite.Collection, "sarco_bullsword_kaliber_001");
            SarcophagusBulletShieldKaliberSprite.SetSprite(m_Sarcophagus_ShotgunBookSprite.Collection, "sarco_bullshield_kaliber_001");
            SarcophagusShotgunBookKaliberShadowSprite.SetSprite(m_Sarcophagus_ShotgunBookSprite.Collection, "sarco_shadow_001");
            SarcophagusShotgunMaceKaliberShadowSprite.SetSprite(m_Sarcophagus_ShotgunBookSprite.Collection, "sarco_shadow_001");
            SarcophagusBulletSwordKaliberShadowSprite.SetSprite(m_Sarcophagus_ShotgunBookSprite.Collection, "sarco_shadow_001");
            SarcophagusBulletShieldKaliberShadowSprite.SetSprite(m_Sarcophagus_ShotgunBookSprite.Collection, "sarco_shadow_001");
            SarcophagusShotgunBookKaliberShadowSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.FLAT;
            SarcophagusShotgunMaceKaliberShadowSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.FLAT;
            SarcophagusBulletSwordKaliberShadowSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.FLAT;
            SarcophagusBulletShieldKaliberShadowSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.FLAT;
            SarcophagusShotgunBookKaliberSprite.HeightOffGround = -1;
            SarcophagusShotgunMaceKaliberSprite.HeightOffGround = -1;
            SarcophagusBulletSwordKaliberSprite.HeightOffGround = -1;
            SarcophagusBulletShieldKaliberSprite.HeightOffGround = -1;
            SarcophagusShotgunBookKaliberShadowSprite.HeightOffGround = -1.7f;
            SarcophagusShotgunMaceKaliberShadowSprite.HeightOffGround = -1.7f;
            SarcophagusBulletSwordKaliberShadowSprite.HeightOffGround = -1.7f;
            SarcophagusBulletShieldKaliberShadowSprite.HeightOffGround = -1.7f;

            SpeculativeRigidbody SarcophagusShotgunBookKaliberRigidBody = Sarcophagus_ShotgunBook_Kaliber.AddComponent<SpeculativeRigidbody>();
            SpeculativeRigidbody SarcophagusShotgunMaceKaliberRigidBody = Sarcophagus_ShotgunMace_Kaliber.AddComponent<SpeculativeRigidbody>();
            SpeculativeRigidbody SarcophagusBulletSwordKaliberRigidBody = Sarcophagus_BulletSword_Kaliber.AddComponent<SpeculativeRigidbody>(); 
            SpeculativeRigidbody SarcophagusBulletShieldKaliberRigidBody = Sarcophagus_BulletShield_Kaliber.AddComponent<SpeculativeRigidbody>();

            ExpandUtility.DuplicateRigidBody(SarcophagusShotgunBookKaliberRigidBody, m_Sarcophagus_ShotgunBookSprite.gameObject.GetComponent<SpeculativeRigidbody>());
            ExpandUtility.DuplicateRigidBody(SarcophagusShotgunMaceKaliberRigidBody, m_Sarcophagus_ShotgunBookSprite.gameObject.GetComponent<SpeculativeRigidbody>());
            ExpandUtility.DuplicateRigidBody(SarcophagusBulletSwordKaliberRigidBody, m_Sarcophagus_ShotgunBookSprite.gameObject.GetComponent<SpeculativeRigidbody>());
            ExpandUtility.DuplicateRigidBody(SarcophagusBulletShieldKaliberRigidBody, m_Sarcophagus_ShotgunBookSprite.gameObject.GetComponent<SpeculativeRigidbody>());

            PlacedBlockerConfigurable SarcophagusShotgunBookKaliberBlocker = Sarcophagus_ShotgunBook_Kaliber.AddComponent<PlacedBlockerConfigurable>();
            PlacedBlockerConfigurable SarcophagusShotgunMaceKaliberBlocker = Sarcophagus_ShotgunMace_Kaliber.AddComponent<PlacedBlockerConfigurable>();
            PlacedBlockerConfigurable SarcophagusBulletSwordKaliberBlocker = Sarcophagus_BulletSword_Kaliber.AddComponent<PlacedBlockerConfigurable>();
            PlacedBlockerConfigurable SarcophagusBulletShieldKaliberBlocker = Sarcophagus_BulletShield_Kaliber.AddComponent<PlacedBlockerConfigurable>();
            SarcophagusShotgunBookKaliberBlocker.colliderSelection = PlacedBlockerConfigurable.ColliderSelection.Single;
            SarcophagusShotgunBookKaliberBlocker.SpecifyPixelCollider = true;
            SarcophagusShotgunBookKaliberBlocker.SpecifiedPixelCollider = 1;
            SarcophagusShotgunMaceKaliberBlocker.colliderSelection = PlacedBlockerConfigurable.ColliderSelection.Single;
            SarcophagusShotgunMaceKaliberBlocker.SpecifyPixelCollider = true;
            SarcophagusShotgunMaceKaliberBlocker.SpecifiedPixelCollider = 1;
            SarcophagusBulletSwordKaliberBlocker.colliderSelection = PlacedBlockerConfigurable.ColliderSelection.Single;
            SarcophagusBulletSwordKaliberBlocker.SpecifyPixelCollider = true;
            SarcophagusBulletSwordKaliberBlocker.SpecifiedPixelCollider = 1;
            SarcophagusBulletShieldKaliberBlocker.colliderSelection = PlacedBlockerConfigurable.ColliderSelection.Single;
            SarcophagusBulletShieldKaliberBlocker.SpecifyPixelCollider = true;
            SarcophagusBulletShieldKaliberBlocker.SpecifiedPixelCollider = 1;
            DungeonPlaceableBehaviour SarcophagusShotgunBookKaliberPlaceable = Sarcophagus_ShotgunBook_Kaliber.AddComponent<DungeonPlaceableBehaviour>();
            DungeonPlaceableBehaviour SarcophagusShotgunMaceKaliberPlaceable = Sarcophagus_ShotgunMace_Kaliber.AddComponent<DungeonPlaceableBehaviour>();
            DungeonPlaceableBehaviour SarcophagusBulletSwordKaliberPlaceable = Sarcophagus_BulletSword_Kaliber.AddComponent<DungeonPlaceableBehaviour>(); 
            DungeonPlaceableBehaviour SarcophagusBulletShieldKaliberPlaceable = Sarcophagus_BulletShield_Kaliber.AddComponent<DungeonPlaceableBehaviour>();
            SarcophagusShotgunBookKaliberPlaceable.placeableWidth = 3;
            SarcophagusShotgunBookKaliberPlaceable.placeableHeight = 3;
            SarcophagusShotgunBookKaliberPlaceable.difficulty = DungeonPlaceableBehaviour.PlaceableDifficulty.BASE;
            SarcophagusShotgunBookKaliberPlaceable.isPassable = false;            
            SarcophagusShotgunMaceKaliberPlaceable.placeableWidth = 3;
            SarcophagusShotgunMaceKaliberPlaceable.placeableHeight = 3;
            SarcophagusShotgunMaceKaliberPlaceable.difficulty = DungeonPlaceableBehaviour.PlaceableDifficulty.BASE;
            SarcophagusShotgunMaceKaliberPlaceable.isPassable = false;
            SarcophagusBulletSwordKaliberPlaceable.placeableWidth = 3;
            SarcophagusBulletSwordKaliberPlaceable.placeableHeight = 3;
            SarcophagusBulletSwordKaliberPlaceable.difficulty = DungeonPlaceableBehaviour.PlaceableDifficulty.BASE;
            SarcophagusBulletSwordKaliberPlaceable.isPassable = false;
            SarcophagusBulletShieldKaliberPlaceable.placeableWidth = 3;
            SarcophagusBulletShieldKaliberPlaceable.placeableHeight = 3;
            SarcophagusBulletShieldKaliberPlaceable.difficulty = DungeonPlaceableBehaviour.PlaceableDifficulty.BASE;
            SarcophagusBulletShieldKaliberPlaceable.isPassable = false;

            m_Sarcophagus_ShotgunBookSprite = null;


            Sarco_WoodShieldPedestal = expandSharedAssets1.LoadAsset<GameObject>("ExpandSarco_Pedestal");
            tk2dSprite m_SarcoPedestalSprite = SpriteSerializer.AddSpriteToObject(Sarco_WoodShieldPedestal, EXMonsterCollection, "PedestalRuins");

            SpeculativeRigidbody Sarco_WoodShieldPedestalRigidBody = ExpandUtility.GenerateOrAddToRigidBody(Sarco_WoodShieldPedestal, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 3), dimensions: new IntVector2(26, 23));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_WoodShieldPedestal,  CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 3), dimensions: new IntVector2(26, 23));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_WoodShieldPedestal, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(1, 5), dimensions: new IntVector2(24, 21));

            ExpandBellyWoodCrestEntranceController WoodCrestController = Sarco_WoodShieldPedestal.AddComponent<ExpandBellyWoodCrestEntranceController>();
            WoodCrestController.ItemID = WoodenCrest.WoodCrestID;

            Sarco_Door = expandSharedAssets1.LoadAsset<GameObject>("ExpandSarco_Door");
            Sarco_Door.layer = LayerMask.NameToLayer("FG_Critical");
            tk2dSprite Sarco_DoorSprite = Sarco_Door.AddComponent<tk2dSprite>();
            Sarco_DoorSprite.SetSprite(sharedAssets.LoadAsset<GameObject>("Environment_Gungeon_Collection").GetComponent<tk2dSpriteCollectionData>(), "sarco_door_001");
            Sarco_DoorSprite.HeightOffGround = -1f;

            tk2dSpriteAnimation Sarco_DoorAnimation = Sarco_Door.AddComponent<tk2dSpriteAnimation>();
            Sarco_DoorAnimation.clips = new tk2dSpriteAnimationClip[0];

            List<string> SarcoDoor_OpenFrames = new List<string>() {
                "sarco_door_open_001",
                "sarco_door_open_002",
                "sarco_door_open_003",
                "sarco_door_open_004",
                "sarco_door_open_005",
                "sarco_door_open_006"
            };

            ExpandUtility.AddAnimation(Sarco_DoorAnimation, Sarco_DoorSprite.Collection, SarcoDoor_OpenFrames, "Sarco_Door_Open", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.GenerateSpriteAnimator(Sarco_Door, Sarco_DoorAnimation);

            ExpandUtility.GenerateOrAddToRigidBody(Sarco_Door, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(10, 3), dimensions: new IntVector2(32, 28));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_Door, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(10, 12), dimensions: new IntVector2(32, 28));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_Door, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 3), dimensions: new IntVector2(10, 28));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_Door, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(42, 3), dimensions: new IntVector2(10, 28));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_Door, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 31), dimensions: new IntVector2(52, 12));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_Door, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 3), dimensions: new IntVector2(10, 28));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_Door, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 12), dimensions: new IntVector2(10, 28));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_Door, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(42, 12), dimensions: new IntVector2(10, 28));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_Door, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 40), dimensions: new IntVector2(52, 5));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_Door, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 3), dimensions: new IntVector2(52, 42));


            Sarco_Floor = expandSharedAssets1.LoadAsset<GameObject>("ExpandSarco_Floor");
            tk2dSprite m_SarcoFloorSprite = SpriteSerializer.AddSpriteToObject(Sarco_Floor, EXLargeMonsterCollection, "Belly_GungeonMonsterRoomFloor", tk2dBaseSprite.PerpendicularState.FLAT);
            m_SarcoFloorSprite.HeightOffGround = -1.7f;

            Sarco_MonsterObject = expandSharedAssets1.LoadAsset<GameObject>("ExpandSarco_Monster");                        
            tk2dSprite Sarco_MonsterSprite = SpriteSerializer.AddSpriteToObject(Sarco_MonsterObject, EXLargeMonsterCollection, "Belly_Monster_Move_001");
            
            List<string> BellyMonsterAnimationFrames = new List<string>() {
                "Belly_Monster_Move_001",
                "Belly_Monster_Move_002",
                "Belly_Monster_Move_003",
                "Belly_Monster_Move_004",
                "Belly_Monster_Move_005",
                "Belly_Monster_Move_006",
                "Belly_Monster_Move_007",
                "Belly_Monster_Move_008",
                "Belly_Monster_Move_009",
                "Belly_Monster_Move_010",
                "Belly_Monster_Move_011",
                "Belly_Monster_Move_012"
            };

            ExpandUtility.GenerateSpriteAnimator(Sarco_MonsterObject);
            ExpandUtility.AddAnimation(Sarco_MonsterObject.GetComponent<tk2dSpriteAnimator>(), EXLargeMonsterCollection.GetComponent<tk2dSpriteCollectionData>(), BellyMonsterAnimationFrames, "MonsterChase", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            
            SpeculativeRigidbody bellyMonsterRigidBody = ExpandUtility.GenerateOrAddToRigidBody(Sarco_MonsterObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(57, 0), dimensions: new IntVector2(243, 1024), CanBeCarried: false);
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_MonsterObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(57, 0), dimensions: new IntVector2(243, 1024), CanBeCarried: false);
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_MonsterObject, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(57, 0), dimensions: new IntVector2(243, 1024), CanBeCarried: false);
            bellyMonsterRigidBody.MaxVelocity = new Vector2(-1.5f, 0);

            Sarco_MonsterObject.GetComponent<tk2dSpriteAnimator>().Library.clips[0].frames[6].eventInfo = "slam";
            Sarco_MonsterObject.GetComponent<tk2dSpriteAnimator>().Library.clips[0].frames[6].triggerEvent = true;

            ExpandBellyMonsterController BellyMonster = Sarco_MonsterObject.AddComponent<ExpandBellyMonsterController>();
            BellyMonster.ImpactVFXObjects = new GameObject[] {
                sharedAssets.LoadAsset<GameObject>("VFX_Dust_Explosion"),
                sharedAssets.LoadAsset<GameObject>("VFX_Tombstone_Impact"),
                sharedAssets.LoadAsset<GameObject>("VFX_Big_Dust_Poof")
            };

            Sarco_Skeleton = expandSharedAssets1.LoadAsset<GameObject>("ExpandDeadSkeleton");
            Sarco_Skeleton.AddComponent<tk2dSprite>();
            tk2dSprite SarcoSkeletonSprite = Sarco_Skeleton.GetComponent<tk2dSprite>();;
            SarcoSkeletonSprite.SetSprite(sharedAssets.LoadAsset<GameObject>("EnvironmentCollection 4").GetComponent<tk2dSpriteCollectionData>(), "skeleton_floor_001");
            SarcoSkeletonSprite.HeightOffGround = -2f;

            Belly_ExitWarp = expandSharedAssets1.LoadAsset<GameObject>("ExpandBelly_ExitWarp");
            Belly_ExitWarp.layer = LayerMask.NameToLayer("FG_Critical");
            tk2dSprite Belly_ExitSprite = SpriteSerializer.AddSpriteToObject(Belly_ExitWarp, EXMonsterCollection, "Belly_Void");
            
            ExpandUtility.GenerateOrAddToRigidBody(Belly_ExitWarp, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(2, 2));
            ExpandWarpManager Belly_ExitWarpController = Belly_ExitWarp.AddComponent<ExpandWarpManager>();
            Belly_ExitWarpController.warpType = ExpandWarpManager.WarpType.FloorWarp;
                        
            Belly_ExitRoomIcon = expandSharedAssets1.LoadAsset<GameObject>("BellyExitRoomIcon");
            tk2dSprite Belly_ExitRoomIconSprite = SpriteSerializer.AddSpriteToObject(Belly_ExitRoomIcon, EXMonsterCollection, "Belly_ExitRoomIcon");

            Belly_PitVFX1 = expandSharedAssets1.LoadAsset<GameObject>("Belly_PitVFX1");
            tk2dSprite m_Belly_PitVFX1Sprite = SpriteSerializer.AddSpriteToObject(Belly_PitVFX1, EXMonsterCollection, "Belly_PitVFX1_01");
            m_Belly_PitVFX1Sprite.HeightOffGround = -4;

            List<string> m_BellyPitVFX1Sprites = new List<string>() {
                "Belly_PitVFX1_02",
                "Belly_PitVFX1_03",
                "Belly_PitVFX1_04",
                "Belly_PitVFX1_05",
                "Belly_PitVFX1_06",
                "Belly_PitVFX1_07",
            };

            List<string> m_BellyPitVFX1AnimationSprites = new List<string>() {
                "Belly_PitVFX1_01",
                "Belly_PitVFX1_02",
                "Belly_PitVFX1_03",
                "Belly_PitVFX1_04",
                "Belly_PitVFX1_05",
                "Belly_PitVFX1_06",
                "Belly_PitVFX1_07",
            };
            

            ExpandUtility.GenerateSpriteAnimator(Belly_PitVFX1, playAutomatically: true);

            tk2dSpriteAnimator BellyPitVFX1Animator = Belly_PitVFX1.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(BellyPitVFX1Animator, EXMonsterCollection.GetComponent<tk2dSpriteCollectionData>(), m_BellyPitVFX1AnimationSprites, "bubbleup", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            
            SpriteAnimatorKiller BellyPitVFX1AnimatorKiller = Belly_PitVFX1.AddComponent<SpriteAnimatorKiller>();
            BellyPitVFX1AnimatorKiller.onlyDisable = true;
            BellyPitVFX1AnimatorKiller.deparentOnStart = false;
            BellyPitVFX1AnimatorKiller.childObjectToDisable = new List<GameObject>(0);
            BellyPitVFX1AnimatorKiller.hasChildAnimators = false;
            BellyPitVFX1AnimatorKiller.deparentAllChildren = false;
            BellyPitVFX1AnimatorKiller.delayDestructionTime = 0;
            BellyPitVFX1AnimatorKiller.fadeTime = 0;

            Belly_PitVFX2 = expandSharedAssets1.LoadAsset<GameObject>("Belly_PitVFX2");            
            tk2dSprite m_Belly_PitVFX2Sprite = SpriteSerializer.AddSpriteToObject(Belly_PitVFX2, EXMonsterCollection, "Belly_PitVFX2_01");
            m_Belly_PitVFX2Sprite.HeightOffGround = -4;
            
            List<string> m_BellyPitVFX2AnimationSprites = new List<string>() {
                "Belly_PitVFX2_01",
                "Belly_PitVFX2_02",
                "Belly_PitVFX2_03",
                "Belly_PitVFX2_04",
                "Belly_PitVFX2_05",
                "Belly_PitVFX2_06"
            };

            ExpandUtility.GenerateSpriteAnimator(Belly_PitVFX2, playAutomatically: true);

            tk2dSpriteAnimator BellyPitVFX2Animator = Belly_PitVFX2.GetComponent<tk2dSpriteAnimator>();
            ExpandUtility.AddAnimation(BellyPitVFX2Animator, m_Belly_PitVFX2Sprite.Collection, m_BellyPitVFX2AnimationSprites, "splash", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            BellyPitVFX2Animator.Library.clips[0].frames[0].eventInfo = "splash";
            BellyPitVFX2Animator.Library.clips[0].frames[0].triggerEvent = true;


            SpriteAnimatorKiller BellyPitVFX2AnimatorKiller = Belly_PitVFX2.AddComponent<SpriteAnimatorKiller>();
            BellyPitVFX2AnimatorKiller.onlyDisable = true;
            BellyPitVFX2AnimatorKiller.deparentOnStart = false;
            BellyPitVFX2AnimatorKiller.childObjectToDisable = new List<GameObject>(0);
            BellyPitVFX2AnimatorKiller.hasChildAnimators = false;
            BellyPitVFX2AnimatorKiller.deparentAllChildren = false;
            BellyPitVFX2AnimatorKiller.delayDestructionTime = 0;
            BellyPitVFX2AnimatorKiller.fadeTime = 0;

            AudioAnimatorListener BellyPitVFX2AudioListener = Belly_PitVFX2.AddComponent<AudioAnimatorListener>();
            BellyPitVFX2AudioListener.animationAudioEvents = new ActorAudioEvent[] {
                new ActorAudioEvent() { eventTag = "splash", eventName = "Play_ENV_water_splash_01" }
            };




            Belly_PitVFX3 = expandSharedAssets1.LoadAsset<GameObject>("Belly_PitVFX3");
            tk2dSprite Belly_PitVFX3Sprite = SpriteSerializer.AddSpriteToObject(Belly_PitVFX3, EXMonsterCollection, "Belly_PitVFX3_01");
            Belly_PitVFX3Sprite.HeightOffGround = -4;
            
            List<string> m_BellyPitVFX3AnimationSprites = new List<string>() {
                "Belly_PitVFX3_01",
                "Belly_PitVFX3_02",
                "Belly_PitVFX3_03",
                "Belly_PitVFX3_04",
                "Belly_PitVFX3_05",
                "Belly_PitVFX3_06",
                "Belly_PitVFX3_07"
            };
            
            ExpandUtility.GenerateSpriteAnimator(Belly_PitVFX3, playAutomatically: true);

            tk2dSpriteAnimator BellyPitVFX3Animator = Belly_PitVFX3.GetComponent<tk2dSpriteAnimator>();
            ExpandUtility.AddAnimation(BellyPitVFX3Animator, EXMonsterCollection.GetComponent<tk2dSpriteCollectionData>(), m_BellyPitVFX3AnimationSprites, "bubbleup", tk2dSpriteAnimationClip.WrapMode.Once, 8);
            
            SpriteAnimatorKiller BellyPitVFX3AnimatorKiller = Belly_PitVFX3.AddComponent<SpriteAnimatorKiller>();
            BellyPitVFX3AnimatorKiller.onlyDisable = true;
            BellyPitVFX3AnimatorKiller.deparentOnStart = false;
            BellyPitVFX3AnimatorKiller.childObjectToDisable = new List<GameObject>(0);
            BellyPitVFX3AnimatorKiller.hasChildAnimators = false;
            BellyPitVFX3AnimatorKiller.deparentAllChildren = false;
            BellyPitVFX3AnimatorKiller.delayDestructionTime = 0;
            BellyPitVFX3AnimatorKiller.fadeTime = 0;


            JungleLight = UnityEngine.Object.Instantiate(sharedAssets.LoadAsset<GameObject>("Castle Light"));
            JungleLight.name = "Jungle Light";
            GameObject JungleShadowSettingsObject = JungleLight.transform.Find("Shadow Render Settings").gameObject;
            GameObject JungleLightSettingsObject = JungleLight.transform.Find("Point light").gameObject;

            Light JungleLightComponent = JungleLightSettingsObject.GetComponent<Light>();
            JungleLightComponent.color = new Color(0.849914f, 0.958546f, 0.963235f, 1);
            JungleLightComponent.intensity = 1.6f;
            JungleLightComponent.range = 11;

            SceneLightManager JungleSceneLightManager = JungleShadowSettingsObject.GetComponent<SceneLightManager>();
            JungleSceneLightManager.validColors = new Color[] { new Color(0.678309f, 0.744067f, 0.75f, 1) };

            JungleLight.SetActive(false);
            FakePrefab.MarkAsFakePrefab(JungleLight);
            UnityEngine.Object.DontDestroyOnLoad(JungleLight);

            BellyLight = UnityEngine.Object.Instantiate(ratDungeon.roomMaterialDefinitions[0].lightPrefabs.elements[0].rawGameObject);
            BellyLight.name = "Belly Light";
            GameObject BellyShadowSettingsObject = BellyLight.transform.Find("Shadow Render Settings").gameObject;
            GameObject BellyLightSettingsObject = BellyLight.transform.Find("Point light").gameObject;

            Light BellyLightComponent = BellyLightSettingsObject.GetComponent<Light>();
            BellyLightComponent.type = LightType.Point;
            BellyLightComponent.color = new Color(0.963235f, 0.95542f, 0.849914f, 1);
            BellyLightComponent.spotAngle = 30;
            BellyLightComponent.range = 11;
            BellyLightComponent.intensity = 1.6f;
            BellyLightComponent.cookieSize = 10;
            BellyLightComponent.shadowResolution = UnityEngine.Rendering.LightShadowResolution.FromQualitySettings;
            BellyLightComponent.shadowCustomResolution = -1;
            BellyLightComponent.shadowStrength = 1;
            BellyLightComponent.shadowBias = 0.05f;
            BellyLightComponent.shadowNormalBias = 0.4f;
            BellyLightComponent.shadowNearPlane = 0.2f;
            BellyLightComponent.renderMode = LightRenderMode.Auto;
            BellyLightComponent.bounceIntensity = 1;

            SceneLightManager BellySceneLightManager = BellyShadowSettingsObject.GetComponent<SceneLightManager>();
            BellySceneLightManager.validColors = new Color[] { new Color(0.516305f, 0.544118f, 0.472102f, 1) };

            BellyLight.SetActive(false);
            FakePrefab.MarkAsFakePrefab(BellyLight);
            UnityEngine.Object.DontDestroyOnLoad(BellyLight);
            

            WestLight = UnityEngine.Object.Instantiate(ratDungeon.roomMaterialDefinitions[0].lightPrefabs.elements[0].rawGameObject);
            WestLight.name = "Belly Light";
            GameObject WestShadowSettingsObject = WestLight.transform.Find("Shadow Render Settings").gameObject;
            GameObject WestLightSettingsObject = WestLight.transform.Find("Point light").gameObject;

            Light WestLightComponent = WestLightSettingsObject.GetComponent<Light>();
            WestLightComponent.type = LightType.Point;
            WestLightComponent.color = new Color(0.878568f, 0.949484f, 0.955882f, 1);
            WestLightComponent.spotAngle = 30;
            WestLightComponent.intensity = 2;
            WestLightComponent.range = 14;
            WestLightComponent.cookieSize = 10;
            WestLightComponent.shadowResolution = UnityEngine.Rendering.LightShadowResolution.FromQualitySettings;
            WestLightComponent.shadowCustomResolution = -1;
            WestLightComponent.shadowStrength = 1;
            WestLightComponent.shadowBias = 0.05f;
            WestLightComponent.shadowNormalBias = 0.4f;
            WestLightComponent.shadowNearPlane = 0.2f;
            WestLightComponent.renderMode = LightRenderMode.Auto;
            WestLightComponent.bounceIntensity = 1;

            SceneLightManager WestSceneLightManager = WestShadowSettingsObject.GetComponent<SceneLightManager>();
            WestSceneLightManager.validColors = new Color[] { new Color(0.509516f, 0.556783f, 0.558824f, 1) };

            WestLight.SetActive(false);
            FakePrefab.MarkAsFakePrefab(WestLight);
            UnityEngine.Object.DontDestroyOnLoad(WestLight);


            PhobosLight = UnityEngine.Object.Instantiate(ratDungeon.roomMaterialDefinitions[0].lightPrefabs.elements[0].rawGameObject);
            PhobosLight.name = "Phobos Light";
            GameObject PhobosShadowSettingsObject = PhobosLight.transform.Find("Shadow Render Settings").gameObject;
            GameObject PhobosLightSettingsObject = PhobosLight.transform.Find("Point light").gameObject;

            Light PhobosLightComponent = PhobosLightSettingsObject.GetComponent<Light>();
            PhobosLightComponent.type = LightType.Point;
            PhobosLightComponent.color = new Color(0.849914f, 0.958546f, 0.963235f, 1);
            PhobosLightComponent.intensity = 1.6f;
            PhobosLightComponent.range = 11;
            PhobosLightComponent.spotAngle = 30;
            PhobosLightComponent.cookieSize = 10;
            PhobosLightComponent.shadowResolution = UnityEngine.Rendering.LightShadowResolution.FromQualitySettings;
            PhobosLightComponent.shadowCustomResolution = -1;
            PhobosLightComponent.shadowStrength = 1;
            PhobosLightComponent.shadowBias = 0.05f;
            PhobosLightComponent.shadowNormalBias = 0.4f;
            PhobosLightComponent.shadowNearPlane = 0.2f;
            PhobosLightComponent.renderMode = LightRenderMode.Auto;
            PhobosLightComponent.bounceIntensity = 1;

            SceneLightManager PhobosSceneLightManager = PhobosShadowSettingsObject.GetComponent<SceneLightManager>();
            PhobosSceneLightManager.validColors = new Color[] { new Color(0.678309f, 0.744067f, 0.75f, 1) };

            PhobosLight.SetActive(false);
            FakePrefab.MarkAsFakePrefab(PhobosLight);
            UnityEngine.Object.DontDestroyOnLoad(PhobosLight);
            
            // Reconstruct unused West Cactus destructibles. (the sprites and animation data still exist!)

            CactusShard1 = expandSharedAssets1.LoadAsset<GameObject>("ExpandCactus_Shard_001");
            CactusShard1.AddComponent<tk2dSprite>();
            CactusShard1.AddComponent<DebrisObject>();
            CactusShard1.GetComponent<tk2dSprite>().Collection = sharedAssets2.LoadAsset<GameObject>("Environment_Gunpowder_Mine_Animation").GetComponent<tk2dSpriteAnimation>().clips[10].frames[0].spriteCollection;
            CactusShard1.GetComponent<tk2dSprite>().SetSprite("cactus_shard_001");

            DebrisObject CactusChard1Debris = CactusShard1.GetComponent<DebrisObject>();
            CactusChard1Debris.Priority = EphemeralObject.EphemeralPriority.Minor;
            CactusChard1Debris.audioEventName = string.Empty;
            CactusChard1Debris.playAnimationOnTrigger = false;
            CactusChard1Debris.usesDirectionalFallAnimations = false;
            CactusChard1Debris.directionalAnimationData = new DebrisDirectionalAnimationInfo() {
                fallUp = string.Empty,
                fallRight = string.Empty,
                fallDown = string.Empty,
                fallLeft = string.Empty
            };
            CactusChard1Debris.breaksOnFall = true;
            CactusChard1Debris.breakOnFallChance = 1;
            CactusChard1Debris.changesCollisionLayer = false;
            CactusChard1Debris.groundedCollisionLayer = CollisionLayer.LowObstacle;
            CactusChard1Debris.followupBehavior = DebrisObject.DebrisFollowupAction.None;
            CactusChard1Debris.collisionStopsBullets = false;
            CactusChard1Debris.animatePitFall = false;
            CactusChard1Debris.pitFallSplash = false;
            CactusChard1Debris.inertialMass = 1;
            CactusChard1Debris.motionMultiplier = 1;
            CactusChard1Debris.canRotate = true;
            CactusChard1Debris.angularVelocity = 1080;
            CactusChard1Debris.angularVelocityVariance = 0;
            CactusChard1Debris.bounceCount = 1;
            CactusChard1Debris.additionalBounceEnglish = 0;
            CactusChard1Debris.decayOnBounce = 0.5f;
            CactusChard1Debris.killTranslationOnBounce = false;
            CactusChard1Debris.usesLifespan = false;
            CactusChard1Debris.lifespanMin = 1;
            CactusChard1Debris.lifespanMax = 1;
            CactusChard1Debris.shouldUseSRBMotion = false;
            CactusChard1Debris.placementOptions = new DebrisObject.DebrisPlacementOptions() {
                canBeRotated = false,
                canBeFlippedHorizontally = false,
                canBeFlippedVertically = false
            };
            CactusChard1Debris.DoesGoopOnRest = false;
            CactusChard1Debris.GoopRadius = 1;
            CactusChard1Debris.additionalHeightBoost = 0;
            CactusChard1Debris.AssignFinalWorldDepth(-1.5f);

            CactusShard2 = expandSharedAssets1.LoadAsset<GameObject>("ExpandCactus_Shard_002");
            CactusShard2.AddComponent<tk2dSprite>();
            CactusShard2.AddComponent<DebrisObject>();
            CactusShard2.GetComponent<tk2dSprite>().Collection = sharedAssets2.LoadAsset<GameObject>("Environment_Gunpowder_Mine_Animation").GetComponent<tk2dSpriteAnimation>().clips[10].frames[0].spriteCollection;
            CactusShard2.GetComponent<tk2dSprite>().SetSprite("cactus_shard_002");

            DebrisObject CactusChard2Debris = CactusShard2.GetComponent<DebrisObject>();
            CactusChard2Debris.Priority = EphemeralObject.EphemeralPriority.Minor;
            CactusChard2Debris.audioEventName = string.Empty;
            CactusChard2Debris.playAnimationOnTrigger = false;
            CactusChard2Debris.usesDirectionalFallAnimations = false;
            CactusChard2Debris.directionalAnimationData = new DebrisDirectionalAnimationInfo() {
                fallUp = string.Empty,
                fallRight = string.Empty,
                fallDown = string.Empty,
                fallLeft = string.Empty
            };
            CactusChard2Debris.breaksOnFall = true;
            CactusChard2Debris.breakOnFallChance = 1;
            CactusChard2Debris.changesCollisionLayer = false;
            CactusChard2Debris.groundedCollisionLayer = CollisionLayer.LowObstacle;
            CactusChard2Debris.followupBehavior = DebrisObject.DebrisFollowupAction.None;
            CactusChard2Debris.collisionStopsBullets = false;
            CactusChard2Debris.animatePitFall = false;
            CactusChard2Debris.pitFallSplash = false;
            CactusChard2Debris.inertialMass = 1;
            CactusChard2Debris.motionMultiplier = 1;
            CactusChard2Debris.canRotate = true;
            CactusChard2Debris.angularVelocity = 1080;
            CactusChard2Debris.angularVelocityVariance = 0;
            CactusChard2Debris.bounceCount = 1;
            CactusChard2Debris.additionalBounceEnglish = 0;
            CactusChard2Debris.decayOnBounce = 0.5f;
            CactusChard2Debris.killTranslationOnBounce = false;
            CactusChard2Debris.usesLifespan = false;
            CactusChard2Debris.lifespanMin = 1;
            CactusChard2Debris.lifespanMax = 1;
            CactusChard2Debris.shouldUseSRBMotion = false;
            CactusChard2Debris.placementOptions = new DebrisObject.DebrisPlacementOptions() {
                canBeRotated = false,
                canBeFlippedHorizontally = false,
                canBeFlippedVertically = false
            };
            CactusChard2Debris.DoesGoopOnRest = false;
            CactusChard2Debris.GoopRadius = 1;
            CactusChard2Debris.additionalHeightBoost = 0;
            CactusChard2Debris.AssignFinalWorldDepth(-1.5f);

            Cactus_A = expandSharedAssets1.LoadAsset<GameObject>("ExpandCactus_A");

            tk2dSprite Cactus_A_Sprite = Cactus_A.AddComponent<tk2dSprite>();
            Cactus_A_Sprite.Collection = sharedAssets2.LoadAsset<GameObject>("Environment_Gunpowder_Mine_Animation").GetComponent<tk2dSpriteAnimation>().clips[10].frames[0].spriteCollection;
            Cactus_A_Sprite.SetSprite("cactus_A_idle_001");

            tk2dSpriteAnimator Cactus_A_SpriteAnimator = Cactus_A.AddComponent<tk2dSpriteAnimator>();
            Cactus_A_SpriteAnimator.Library = sharedAssets2.LoadAsset<GameObject>("Environment_Gunpowder_Mine_Animation").GetComponent<tk2dSpriteAnimation>();
            Cactus_A_SpriteAnimator.DefaultClipId = 10;
            Cactus_A_SpriteAnimator.AdditionalCameraVisibilityRadius = 0;
            Cactus_A_SpriteAnimator.AnimateDuringBossIntros = false;
            Cactus_A_SpriteAnimator.ForceSetEveryFrame = false;
            Cactus_A_SpriteAnimator.playAutomatically = false;
            Cactus_A_SpriteAnimator.IsFrameBlendedAnimation = false;
            Cactus_A_SpriteAnimator.clipTime = 0;
            Cactus_A_SpriteAnimator.deferNextStartClip = false;

            ExpandUtility.GenerateOrAddToRigidBody(Cactus_A, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(1, -3), dimensions: new IntVector2(6, 6));
            ExpandUtility.GenerateOrAddToRigidBody(Cactus_A, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(2, 3), dimensions: new IntVector2(4, 12));
            
            MajorBreakable Cactus_A_Braakable = Cactus_A.AddComponent<MajorBreakable>();
            Cactus_A_Braakable.HitPoints = 25;
            Cactus_A_Braakable.MinHits = 2;
            Cactus_A_Braakable.EnemyDamageOverride = -1;
            Cactus_A_Braakable.ImmuneToBeastMode = false;
            Cactus_A_Braakable.ScaleWithEnemyHealth = false;
            Cactus_A_Braakable.OnlyExplosions = false;
            Cactus_A_Braakable.IgnoreExplosions = false;
            Cactus_A_Braakable.GameActorMotionBreaks = false;
            Cactus_A_Braakable.PlayerRollingBreaks = false;
            Cactus_A_Braakable.spawnShards = true;
            Cactus_A_Braakable.distributeShards = false;
            Cactus_A_Braakable.shardClusters = new ShardCluster[] {
                new ShardCluster() {
                    minFromCluster = 3,
                    maxFromCluster = 6,
                    forceMultiplier = 1,
                    forceAxialMultiplier = Vector3.one,
                    clusterObjects = new DebrisObject[] { CactusChard1Debris, CactusChard2Debris }
                }
            };
            Cactus_A_Braakable.minShardPercentSpeed = 0.05f;
            Cactus_A_Braakable.maxShardPercentSpeed = 0.3f;
            Cactus_A_Braakable.shardBreakStyle = MinorBreakable.BreakStyle.CONE;
            Cactus_A_Braakable.usesTemporaryZeroHitPointsState = false;
            Cactus_A_Braakable.spriteNameToUseAtZeroHP = string.Empty;
            Cactus_A_Braakable.destroyedOnBreak = true;
            Cactus_A_Braakable.childrenToDestroy = new List<GameObject>(0);
            Cactus_A_Braakable.playsAnimationOnNotBroken = true;
            Cactus_A_Braakable.notBreakAnimation = "cactus_A_wobble";
            Cactus_A_Braakable.handlesOwnBreakAnimation = false;
            Cactus_A_Braakable.breakAnimation = string.Empty;
            Cactus_A_Braakable.handlesOwnPrebreakFrames = false;
            Cactus_A_Braakable.prebreakFrames = new BreakFrame[0];
            Cactus_A_Braakable.damageVfx = new VFXPool() { type = VFXPoolType.None, effects = new VFXComplex[0] };
            Cactus_A_Braakable.damageVfxMinTimeBetween = 0.2f;
            Cactus_A_Braakable.breakVfx = new VFXPool() { type = VFXPoolType.None, effects = new VFXComplex[0] };
            Cactus_A_Braakable.delayDamageVfx = false;
            Cactus_A_Braakable.SpawnItemOnBreak = false;
            Cactus_A_Braakable.HandlePathBlocking = false;



            Cactus_B = expandSharedAssets1.LoadAsset<GameObject>("ExpandCactus_B");

            tk2dSprite Cactus_B_Sprite = Cactus_B.AddComponent<tk2dSprite>();
            Cactus_B_Sprite.Collection = sharedAssets2.LoadAsset<GameObject>("Environment_Gunpowder_Mine_Animation").GetComponent<tk2dSpriteAnimation>().clips[10].frames[0].spriteCollection;
            Cactus_B_Sprite.SetSprite("catcus_B_idle_001"); // Yes the devs really did mispell the sprite name for this. :P

            tk2dSpriteAnimator Cactus_B_SpriteAnimator = Cactus_B.AddComponent<tk2dSpriteAnimator>();
            Cactus_B_SpriteAnimator.Library = sharedAssets2.LoadAsset<GameObject>("Environment_Gunpowder_Mine_Animation").GetComponent<tk2dSpriteAnimation>();
            Cactus_B_SpriteAnimator.DefaultClipId = 11;
            Cactus_B_SpriteAnimator.AdditionalCameraVisibilityRadius = 0;
            Cactus_B_SpriteAnimator.AnimateDuringBossIntros = false;
            Cactus_B_SpriteAnimator.ForceSetEveryFrame = false;
            Cactus_B_SpriteAnimator.playAutomatically = false;
            Cactus_B_SpriteAnimator.IsFrameBlendedAnimation = false;
            Cactus_B_SpriteAnimator.clipTime = 0;
            Cactus_B_SpriteAnimator.deferNextStartClip = false;

            ExpandUtility.GenerateOrAddToRigidBody(Cactus_B, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(2, -3), dimensions: new IntVector2(10, 5));
            ExpandUtility.GenerateOrAddToRigidBody(Cactus_B, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(3, 3), dimensions: new IntVector2(8, 14));

            MajorBreakable Cactus_B_Braakable = Cactus_B.AddComponent<MajorBreakable>();
            Cactus_B_Braakable.HitPoints = 25;
            Cactus_B_Braakable.MinHits = 2;
            Cactus_B_Braakable.EnemyDamageOverride = -1;
            Cactus_B_Braakable.ImmuneToBeastMode = false;
            Cactus_B_Braakable.ScaleWithEnemyHealth = false;
            Cactus_B_Braakable.OnlyExplosions = false;
            Cactus_B_Braakable.IgnoreExplosions = false;
            Cactus_B_Braakable.GameActorMotionBreaks = false;
            Cactus_B_Braakable.PlayerRollingBreaks = false;
            Cactus_B_Braakable.spawnShards = true;
            Cactus_B_Braakable.distributeShards = false;
            Cactus_B_Braakable.shardClusters = new ShardCluster[] {
                new ShardCluster() {
                    minFromCluster = 3,
                    maxFromCluster = 6,
                    forceMultiplier = 1,
                    forceAxialMultiplier = Vector3.one,
                    clusterObjects = new DebrisObject[] { CactusChard1Debris, CactusChard2Debris }
                }
            };
            Cactus_B_Braakable.minShardPercentSpeed = 0.05f;
            Cactus_B_Braakable.maxShardPercentSpeed = 0.3f;
            Cactus_B_Braakable.shardBreakStyle = MinorBreakable.BreakStyle.CONE;
            Cactus_B_Braakable.usesTemporaryZeroHitPointsState = false;
            Cactus_B_Braakable.spriteNameToUseAtZeroHP = string.Empty;
            Cactus_B_Braakable.destroyedOnBreak = true;
            Cactus_B_Braakable.childrenToDestroy = new List<GameObject>(0);
            Cactus_B_Braakable.playsAnimationOnNotBroken = true;
            Cactus_B_Braakable.notBreakAnimation = "cactus_B_wobble";
            Cactus_B_Braakable.handlesOwnBreakAnimation = false;
            Cactus_B_Braakable.breakAnimation = string.Empty;
            Cactus_B_Braakable.handlesOwnPrebreakFrames = false;
            Cactus_B_Braakable.prebreakFrames = new BreakFrame[0];
            Cactus_B_Braakable.damageVfx = new VFXPool() { type = VFXPoolType.None, effects = new VFXComplex[0] };
            Cactus_B_Braakable.damageVfxMinTimeBetween = 0.2f;
            Cactus_B_Braakable.breakVfx = new VFXPool() { type = VFXPoolType.None, effects = new VFXComplex[0] };
            Cactus_B_Braakable.delayDamageVfx = false;
            Cactus_B_Braakable.SpawnItemOnBreak = false;
            Cactus_B_Braakable.HandlePathBlocking = false;


            CorruptionBombRewardPedestal = expandSharedAssets1.LoadAsset<GameObject>("EXReward_Pedestal_CorruptionBomb");
            tk2dSprite CorruptionBombRewardPedestalSprite = CorruptionBombRewardPedestal.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateComponent(CorruptionBombRewardPedestalSprite, RewardPedestalPrefab.GetComponent<tk2dSprite>());

            tk2dSprite CorruptionBombRewardPedestalShadowSprite = CorruptionBombRewardPedestal.transform.Find("shadow").gameObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateComponent(CorruptionBombRewardPedestalShadowSprite, RewardPedestalPrefab.transform.Find("Pedestal_Shadow").gameObject.GetComponent<tk2dSprite>());
            CorruptionBombRewardPedestalShadowSprite.SetSprite("pedestal_gun_shadow_002");

            SpeculativeRigidbody CorruptionBombPedestalRigidBody = CorruptionBombRewardPedestal.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateComponent(CorruptionBombPedestalRigidBody, RewardPedestalPrefab.GetComponent<SpeculativeRigidbody>());


            BlankRewardPedestal = expandSharedAssets1.LoadAsset<GameObject>("EXReward_Pedestal_Blank");
            tk2dSprite BlankRewardPedestalSprite = BlankRewardPedestal.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateComponent(BlankRewardPedestalSprite, RewardPedestalPrefab.GetComponent<tk2dSprite>());
            

            tk2dSprite BlankRewardPedestalShadowSprite = BlankRewardPedestal.transform.Find("shadow").gameObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateComponent(BlankRewardPedestalShadowSprite, RewardPedestalPrefab.transform.Find("Pedestal_Shadow").gameObject.GetComponent<tk2dSprite>());
            BlankRewardPedestalShadowSprite.SetSprite("pedestal_gun_shadow_002");

            SpeculativeRigidbody BlankRewardPedestalRigidBody = BlankRewardPedestal.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateComponent(BlankRewardPedestalRigidBody, RewardPedestalPrefab.GetComponent<SpeculativeRigidbody>());


            RatKeyRewardPedestal = expandSharedAssets1.LoadAsset<GameObject>("EXReward_Pedestal_RatKey");
            tk2dSprite RatKeyRewardPedestalSprite = RatKeyRewardPedestal.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateComponent(RatKeyRewardPedestalSprite, RewardPedestalPrefab.GetComponent<tk2dSprite>());


            tk2dSprite RatKeyPedestalShadowSprite = RatKeyRewardPedestal.transform.Find("shadow").gameObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateComponent(RatKeyPedestalShadowSprite, RewardPedestalPrefab.transform.Find("Pedestal_Shadow").gameObject.GetComponent<tk2dSprite>());
            RatKeyPedestalShadowSprite.SetSprite("pedestal_gun_shadow_002");

            SpeculativeRigidbody RatKeyPedestalRigidBody = RatKeyRewardPedestal.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateComponent(RatKeyPedestalRigidBody, RewardPedestalPrefab.GetComponent<SpeculativeRigidbody>());


            ExpandRewardPedestal CorruptionBombPedestal = CorruptionBombRewardPedestal.AddComponent<ExpandRewardPedestal>();
            CorruptionBombPedestal.spawnTransform = CorruptionBombRewardPedestal.transform.Find("Reward_Spawn");
            CorruptionBombPedestal.ItemID = CorruptionBomb.CorruptionBombPickupID;

            ExpandRewardPedestal BlankPedestal = BlankRewardPedestal.AddComponent<ExpandRewardPedestal>();
            BlankPedestal.spawnTransform = BlankRewardPedestal.transform.Find("Reward_Spawn");

            ExpandRewardPedestal ratKeyPedestal = RatKeyRewardPedestal.AddComponent<ExpandRewardPedestal>();
            ratKeyPedestal.spawnTransform = RatKeyRewardPedestal.transform.Find("Reward_Spawn");
            ratKeyPedestal.ItemID = 727;


            EXSpaceFloor_50x50 = expandSharedAssets1.LoadAsset<GameObject>("EXSpaceFloor_50x50");
            EXSpaceFloorPitBorder_50x50 = expandSharedAssets1.LoadAsset<GameObject>("EXSpaceFloorPitBorder_50x50");
            tk2dSprite EXSpaceFloor50x50Sprite = SpriteSerializer.AddSpriteToObject(EXSpaceFloor_50x50, EXTrapCollection, "RainbowRoad", tk2dBaseSprite.PerpendicularState.FLAT);
            tk2dSprite EXSpaceFloorPitBorder50x50Sprite = SpriteSerializer.AddSpriteToObject(EXSpaceFloorPitBorder_50x50, EXTrapCollection, "RainbowRoad_PitBorders", tk2dBaseSprite.PerpendicularState.FLAT);

            // EXSpaceFloor50x50Sprite.HeightOffGround = -200;
            // EXSpaceFloorPitBorder50x50Sprite.HeightOffGround = -195;
            EXSpaceFloor50x50Sprite.HeightOffGround = -1.72f;
            EXSpaceFloorPitBorder50x50Sprite.HeightOffGround = -1.6f;
            EXSpaceFloor50x50Sprite.renderer.material = new Material(ShaderCache.Acquire("Brave/Internal/RainbowChestShader"));
            EXSpaceFloor50x50Sprite.renderer.material.mainTexture = expandSharedAssets1.LoadAsset<Texture2D>("RainbowRoad");
            EXSpaceFloor50x50Sprite.usesOverrideMaterial = true;            
            EXSpaceFloor_50x50.AddComponent<ExpandEnableSpacePitOnEnterComponent>();

            EX_Parachute = expandSharedAssets1.LoadAsset<GameObject>("EX_Parachute");
            tk2dSprite m_EXParachuteSprite = SpriteSerializer.AddSpriteToObject(EX_Parachute, EXParadropCollection, "EX_Parachute");
            
            List<string> EXParachute_OpenFrames = new List<string>() {
                "EX_Parachute_Open_01",
                "EX_Parachute_Open_02",
                "EX_Parachute_Open_03",
                "EX_Parachute_Open_04",
                "EX_Parachute_Open_05"
            };

            List<string> EXParachute_LandedFrames = new List<string>() { "EX_Parachute_Land", "EX_Parachute_Land_Blank" };
            
            EXParachute_OpenFrames.Add("EX_Parachute");

            EXParachute_LandedFrames = new List<string>() {
                "EX_Parachute_Land",
                "EX_Parachute_Land_Blank",
                "EX_Parachute_Land",
                "EX_Parachute_Land_Blank",
                "EX_Parachute_Land",
                "EX_Parachute_Land_Blank"
            };

            
            ExpandUtility.GenerateSpriteAnimator(EX_Parachute);
            ExpandUtility.AddAnimation(EX_Parachute.GetComponent<tk2dSpriteAnimator>(), EXParadropCollection.GetComponent<tk2dSpriteCollectionData>(), EXParachute_OpenFrames, "ParachuteDeploy", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(EX_Parachute.GetComponent<tk2dSpriteAnimator>(), EXParadropCollection.GetComponent<tk2dSpriteCollectionData>(), EXParachute_LandedFrames, "ParachuteLanded", tk2dSpriteAnimationClip.WrapMode.Once, 10);

            EX_ParadropAnchor = ExpandAssets.LoadAsset<GameObject>("EX_ParadropAnchor");

            EX_ExplodyBarrelDummy = expandSharedAssets1.LoadAsset<GameObject>("EX_ExplodyBarrelDummy");
            tk2dSprite m_ExplodyBarrelDummySprite = SpriteSerializer.AddSpriteToObject(EX_ExplodyBarrelDummy, EXParadropCollection, "EX_ExplodyBarrel");
            
            ExpandUtility.GenerateSpriteAnimator(EX_ExplodyBarrelDummy, ClipFps: 5);
            ExpandUtility.AddAnimation(EX_ExplodyBarrelDummy.GetComponent<tk2dSpriteAnimator>(), EXParadropCollection.GetComponent<tk2dSpriteCollectionData>(), new List<string>() { "EX_ExplodyBarrel", "EX_ExplodyBarrel_Explode" }, "explode", tk2dSpriteAnimationClip.WrapMode.Once, 6);

            SpeculativeRigidbody m_ExplodyBarrelDummyRigidBody = ExpandUtility.GenerateOrAddToRigidBody(EX_ExplodyBarrelDummy, CollisionLayer.EnemyCollider);

            m_ExplodyBarrelDummyRigidBody.PixelColliders.Clear();
            m_ExplodyBarrelDummyRigidBody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyCollider,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 6,
                    ManualOffsetY = 1,
                    ManualWidth = 14,
                    ManualHeight = 16,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );
            m_ExplodyBarrelDummyRigidBody.PixelColliders.Add(
                new PixelCollider() {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyHitBox,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 6,
                    ManualOffsetY = 1,
                    ManualWidth = 14,
                    ManualHeight = 20,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                }
            );


            EX_ItemDropper = expandSharedAssets1.LoadAsset<GameObject>("EX_ItemParadrop");
            tk2dSprite EX_ItemDropperSprite = SpriteSerializer.AddSpriteToObject(EX_ItemDropper, EXParadropCollection, "EX_Paradrop_Crate_Idle");
            EX_ItemDropperSprite.HeightOffGround = -1;

            ExpandUtility.GenerateOrAddToRigidBody(EX_ItemDropper, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CollideWithOthers: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 18), offset: new IntVector2(10, 0));

            List<string> EXItemDropCrateLandSpites = new List<string>() {
                "EX_Paradrop_Crate_Land_001",
                "EX_Paradrop_Crate_Land_002",
                "EX_Paradrop_Crate_Land_003",
                "EX_Paradrop_Crate_Land_004",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_006",
                "EX_Paradrop_Crate_Land_Blank"
            };

            List<string> EXItemDropCrateLandAnimation = new List<string>() {
                "EX_Paradrop_Crate_Land_001",
                "EX_Paradrop_Crate_Land_002",
                "EX_Paradrop_Crate_Land_003",
                "EX_Paradrop_Crate_Land_004",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_006",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_Blank",
                "EX_Paradrop_Crate_Land_Blank",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_Blank",
                "EX_Paradrop_Crate_Land_Blank",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_Blank",
                "EX_Paradrop_Crate_Land_Blank",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_005",
                "EX_Paradrop_Crate_Land_Blank",
                "EX_Paradrop_Crate_Land_Blank"
            };

            ExpandUtility.GenerateSpriteAnimator(EX_ItemDropper);

            ExpandUtility.AddAnimation(EX_ItemDropper.GetComponent<tk2dSpriteAnimator>(), EXParadropCollection.GetComponent<tk2dSpriteCollectionData>(), EXItemDropCrateLandAnimation, "bustopen", tk2dSpriteAnimationClip.WrapMode.Once, 12);

            tk2dSpriteAnimator EXItemDropCrateAnimator = EX_ItemDropper.GetComponent<tk2dSpriteAnimator>();
            EXItemDropCrateAnimator.Library.clips[0].frames[0].eventAudio = "Play_OBJ_supplycrate_open_01";
            EXItemDropCrateAnimator.Library.clips[0].frames[0].triggerEvent = true;

            ExpandParadropController EXItemDropController = EX_ItemDropper.AddComponent<ExpandParadropController>();
            EXItemDropController.StartsIntheAir = true;
            EXItemDropController.IsItemCrate = true;
            EXItemDropController.UseLandingVFX = true;
            EXItemDropController.DropHeightHorizontalOffset = 8;
                        


            EX_Chest_West = expandSharedAssets1.LoadAsset<GameObject>("EX_Chest_West");
            tk2dSprite ChestWestSprite = SpriteSerializer.AddSpriteToObject(EX_Chest_West, EXChestCollection, "chest_west_idle_001");
            ChestWestSprite.SetSprite(EXChestCollection.GetComponent<tk2dSpriteCollectionData>(), "chest_west_idle_001");
            ChestWestSprite.HeightOffGround = -1;
            
            List<string> chestWestOpen = new List<string>() {
                "chest_west_open_001",
                "chest_west_open_002",
                "chest_west_open_003",
                "chest_west_open_004",
                "chest_west_open_005",
                "chest_west_open_006",
                "chest_west_open_007",
                "chest_west_open_008",
                "chest_west_open_009",
                "chest_west_open_010"
            };

            List<string> chestWestAppear = new List<string>() {
                "chest_west_appear_001",
                "chest_west_appear_002",
                "chest_west_appear_003",
                "chest_west_appear_004",
                "chest_west_appear_004",
                "chest_west_appear_004",
                "chest_west_appear_004",
                "chest_west_appear_005",
                "chest_west_appear_006"
            };
                        
            List<string> chestWestBreak = new List<string>() {
                "chest_west_break_001",
                "chest_west_break_002",
                "chest_west_break_003",
                "chest_west_break_004"
            };
            
            ExpandUtility.GenerateSpriteAnimator(EX_Chest_West);
            
            tk2dSpriteAnimator ChestWestAnimator = EX_Chest_West.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(ChestWestAnimator, EXChestCollection.GetComponent<tk2dSpriteCollectionData>(), chestWestAppear, "west_chest_appear", tk2dSpriteAnimationClip.WrapMode.Once, 9);
            ChestWestAnimator.Library.clips[0].frames[0].eventAudio = "Play_OBJ_smallchest_spawn_01";
            ChestWestAnimator.Library.clips[0].frames[0].triggerEvent = true;
            ExpandUtility.AddAnimation(ChestWestAnimator, EXChestCollection.GetComponent<tk2dSpriteCollectionData>(), chestWestOpen, "west_chest_open", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            ExpandUtility.AddAnimation(ChestWestAnimator, EXChestCollection.GetComponent<tk2dSpriteCollectionData>(), chestWestBreak, "west_chest_break", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ChestWestAnimator.Library.clips[2].frames[0].eventAudio = "Play_OBJ_barrel_break_01";
            ChestWestAnimator.Library.clips[2].frames[0].triggerEvent = true;
            
            ExpandUtility.GenerateOrAddToRigidBody(EX_Chest_West, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, collideWithTileMap: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(27, 16), offset: new IntVector2(2, 2));
            
            // Note Brown Chest's child object PoofCloud and Groundhit sprites use same collection asset as parent brown chest as well as the sprite animation clip library.
            GameObject m_ChestWestPoofCloudChild = EX_Chest_West.transform.Find("PoofCloud").gameObject;
            tk2dSprite m_ChestWestPoofCloudSprite = m_ChestWestPoofCloudChild.AddComponent<tk2dSprite>();
            m_ChestWestPoofCloudSprite.SetSprite(ExpandObjectDatabase.ChestBrownTwoItems.GetComponent<tk2dSprite>().Collection, "cloud_woodchest_appear_001");
            m_ChestWestPoofCloudSprite.attachParent = ChestWestSprite;
            m_ChestWestPoofCloudSprite.HeightOffGround = 1;

            tk2dSpriteAnimator m_ChestWestPoofCloudAnimator = ExpandUtility.DuplicateSpriteAnimator(m_ChestWestPoofCloudChild, ExpandObjectDatabase.ChestBrownTwoItems.GetComponent<tk2dSpriteAnimator>());
            m_ChestWestPoofCloudAnimator.DefaultClipId = 15;
            m_ChestWestPoofCloudAnimator.playAutomatically = true;


            SpriteAnimatorKiller m_ChestWestPoofCloudChildAnimatorKiller = m_ChestWestPoofCloudChild.AddComponent<SpriteAnimatorKiller>();
            m_ChestWestPoofCloudChildAnimatorKiller.onlyDisable = true;
            m_ChestWestPoofCloudChildAnimatorKiller.deparentOnStart = false;
            m_ChestWestPoofCloudChildAnimatorKiller.childObjectToDisable = new List<GameObject>(0);
            m_ChestWestPoofCloudChildAnimatorKiller.hasChildAnimators = false;
            m_ChestWestPoofCloudChildAnimatorKiller.deparentAllChildren = false;
            m_ChestWestPoofCloudChildAnimatorKiller.disableRendererOnDelay = false;
            m_ChestWestPoofCloudChildAnimatorKiller.delayDestructionTime = 0;
            m_ChestWestPoofCloudChildAnimatorKiller.fadeTime = 0;
            
            TimedObjectKiller m_ChestWestPoofCloudChildTimedKiller = m_ChestWestPoofCloudChild.AddComponent<TimedObjectKiller>();
            m_ChestWestPoofCloudChildTimedKiller.lifeTime = 1;
            m_ChestWestPoofCloudChildTimedKiller.m_poolType = TimedObjectKiller.PoolType.Pooled;
                        

            GameObject m_ChestWestGroundHitChild = EX_Chest_West.transform.Find("GroundHit").gameObject;
            tk2dSprite m_ChestWestGroundHitSprite = m_ChestWestGroundHitChild.AddComponent<tk2dSprite>();
            m_ChestWestGroundHitSprite.SetSprite(ExpandObjectDatabase.ChestBrownTwoItems.GetComponent<tk2dSprite>().Collection, "low_chest_dustland_001");
            m_ChestWestGroundHitSprite.attachParent = ChestWestSprite;
            m_ChestWestGroundHitSprite.HeightOffGround = 1;

            tk2dSpriteAnimator m_ChestWestGroundHitAnimator = ExpandUtility.DuplicateSpriteAnimator(m_ChestWestGroundHitChild, ExpandObjectDatabase.ChestBrownTwoItems.GetComponent<tk2dSpriteAnimator>());
            m_ChestWestGroundHitAnimator.DefaultClipId = 20;
            m_ChestWestGroundHitAnimator.playAutomatically = true;

            SpriteAnimatorKiller m_ChestWestGroundHitChildAnimatorKiller = m_ChestWestGroundHitChild.AddComponent<SpriteAnimatorKiller>();
            m_ChestWestGroundHitChildAnimatorKiller.onlyDisable = false;
            m_ChestWestGroundHitChildAnimatorKiller.deparentOnStart = false;
            m_ChestWestGroundHitChildAnimatorKiller.childObjectToDisable = new List<GameObject>(0);
            m_ChestWestGroundHitChildAnimatorKiller.hasChildAnimators = false;
            m_ChestWestGroundHitChildAnimatorKiller.deparentAllChildren = false;
            m_ChestWestGroundHitChildAnimatorKiller.disableRendererOnDelay = false;
            m_ChestWestGroundHitChildAnimatorKiller.delayDestructionTime = 0;
            m_ChestWestGroundHitChildAnimatorKiller.fadeTime = 0;


            
            SimpleLightIntensityCurve m_ChestWest_LightCurve = m_ChestWestPoofCloudChild.transform.Find("Point Light").gameObject.AddComponent<SimpleLightIntensityCurve>();
            m_ChestWest_LightCurve.Duration = 1;
            m_ChestWest_LightCurve.MinIntensity = 0;
            m_ChestWest_LightCurve.MaxIntensity = 1.1f;
            m_ChestWest_LightCurve.Curve = new AnimationCurve() {
                preWrapMode = WrapMode.Default,
                postWrapMode = WrapMode.Default,
                keys = new Keyframe[] {
                    new Keyframe() {
                        time = 0,
                        value = 0,
                        inTangent = 0,
                        outTangent = 0
                    },
                    new Keyframe() {
                        time = 0.119112f,
                        value = 1,
                        inTangent = 15.147778f,
                        outTangent = -0.6802f
                    },
                    new Keyframe() {
                        time = 1,
                        value = 0,
                        inTangent = 0,
                        outTangent = 0
                    },
                }

            };

            GameObject ChestWestShadow = EX_Chest_West.transform.Find("Shadow").gameObject;
            tk2dSprite ChestWestShadowSprite = ChestWestShadow.AddComponent<tk2dSprite>();
            ChestWestShadowSprite.SetSprite(ExpandObjectDatabase.ChestBrownTwoItems.GetComponent<tk2dSprite>().Collection, "low_chest_shadow_001");
            ChestWestShadowSprite.HeightOffGround = -2;
            
            GameObject ChestWestLock = EX_Chest_West.transform.Find("Lock").gameObject;
            tk2dSprite ChestWestLockSprite = SpriteSerializer.AddSpriteToObject(ChestWestLock, EXChestCollection, "west_lock_idle_001");
            ChestWestLockSprite.HeightOffGround = -0.5f;
            
            List<string> ChestWestLockOpen = new List<string>() {
                "west_lock_idle_001",
                "west_lock_open_001",
                "west_lock_open_002",
                "west_lock_open_003",
                "west_lock_open_004",
                "west_lock_open_005",
                "west_lock_open_006",
                "west_lock_open_007"
            };

            List<string> ChestWestLockNoKey = new List<string>() {
                "west_lock_idle_001",
                "west_lock_nokey_001",
                "west_lock_idle_001",
                "west_lock_nokey_002",
                "west_lock_idle_001",
                "west_lock_nokey_001",
                "west_lock_idle_001",
                "west_lock_nokey_002",
                "west_lock_idle_001",
                "west_lock_nokey_001",
                "west_lock_idle_001"
            };

            List<string> ChestWestLockBreak = new List<string>() { "west_lock_broke_001", };
            
            ExpandUtility.GenerateSpriteAnimator(ChestWestLock);

            tk2dSpriteAnimator ChestWestLockAnimator = ChestWestLock.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(ChestWestLockAnimator, EXChestCollection.GetComponent<tk2dSpriteCollectionData>(), ChestWestLockOpen, "west_lock_open", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            ChestWestLockAnimator.Library.clips[0].frames[0].eventAudio = "Play_OBJ_chest_unlock_01";
            ChestWestLockAnimator.Library.clips[0].frames[0].triggerEvent = true;
            ExpandUtility.AddAnimation(ChestWestLockAnimator, EXChestCollection.GetComponent<tk2dSpriteCollectionData>(), ChestWestLockNoKey, "west_lock_nokey", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            ChestWestLockAnimator.Library.clips[1].frames[0].eventAudio = "Play_OBJ_lock_jiggle_01";
            ChestWestLockAnimator.Library.clips[1].frames[0].triggerEvent = true;
            ExpandUtility.AddAnimation(ChestWestLockAnimator, EXChestCollection.GetComponent<tk2dSpriteCollectionData>(), ChestWestLockBreak, "west_lock_break", tk2dSpriteAnimationClip.WrapMode.Once, 12);
            ChestWestLockAnimator.Library.clips[2].frames[0].eventAudio = "Play_WPN_gun_empty_01";
            ChestWestLockAnimator.Library.clips[2].frames[0].triggerEvent = true;
            

            Chest m_chestWest = EX_Chest_West.AddComponent<Chest>();
            m_chestWest.placeableWidth = 3;
            m_chestWest.placeableHeight = 1;
            m_chestWest.difficulty = DungeonPlaceableBehaviour.PlaceableDifficulty.BASE;
            m_chestWest.isPassable = true;
            m_chestWest.ChestIdentifier = Chest.SpecialChestIdentifier.NORMAL;
            m_chestWest.ChestType = Chest.GeneralChestType.ITEM;
            m_chestWest.forceContentIds = new List<int>(0);
            m_chestWest.lootTable = ExpandObjectDatabase.ChestRed.GetComponent<Chest>().lootTable;
            m_chestWest.breakertronNothingChance = 0.1f;
            m_chestWest.breakertronLootTable = ExpandObjectDatabase.ChestRed.GetComponent<Chest>().breakertronLootTable;
            m_chestWest.prefersDungeonProcContents = true;
            m_chestWest.pickedUp = false;
            m_chestWest.spawnAnimName = "west_chest_appear"; // VFXPrespawn and VFX_GroundHit can't be null because spawn behaviour doesn't null check them. Leave string empty if VFX objects not setup yet.
            m_chestWest.openAnimName = "west_chest_open";
            m_chestWest.breakAnimName = "west_chest_break";
            m_chestWest.overrideJunkId = -1;
            m_chestWest.VFX_PreSpawn = m_ChestWestPoofCloudChild; // Must be as child object. (PoofCloud child)
            m_chestWest.VFX_GroundHit = m_ChestWestGroundHitChild; // Must be as child object. (GroundHit child)
            m_chestWest.groundHitDelay = 0.73f;
            m_chestWest.spawnTransform = EX_Chest_West.transform.Find("SpawnTransform");
            m_chestWest.spawnCurve = new AnimationCurve() {
                keys = new Keyframe[] {
                    new Keyframe() {
                        time = 0,
                        value = 0,
                        inTangent = 3.562501f,
                        outTangent = 3.562501f,                        
                    },
                    new Keyframe() {
                        time = 1,
                        value = 1.0125f,
                        inTangent = 0.09381f,
                        outTangent = 0.09381f
                    }

                },
                preWrapMode = WrapMode.Default,
                postWrapMode = WrapMode.Default
            };
            m_chestWest.LockAnimator = ChestWestLockAnimator; // This must be child object of Chest            
            m_chestWest.LockOpenAnim = "west_lock_open";
            m_chestWest.LockBreakAnim = "west_lock_break";
            m_chestWest.LockNoKeyAnim = "west_lock_nokey";
            m_chestWest.SubAnimators = new tk2dSpriteAnimator[0];
            m_chestWest.IsLocked = true;
            m_chestWest.IsSealed = false;
            m_chestWest.IsOpen = false;
            m_chestWest.IsBroken = false;
            m_chestWest.AlwaysBroadcastsOpenEvent = false;
            m_chestWest.IsRainbowChest = false;
            m_chestWest.IsMirrorChest = false;
            m_chestWest.MimicGuid = string.Empty;
            m_chestWest.mimicOffset = IntVector2.Zero;
            m_chestWest.preMimicIdleAnim = string.Empty;
            m_chestWest.preMimicIdleAnimDelay = 3;
            m_chestWest.overrideMimicChance = 0;
            m_chestWest.MinimapIconPrefab = ExpandObjectDatabase.ChestRed.GetComponent<Chest>().MinimapIconPrefab;

            MajorBreakable chestWestBreakable = EX_Chest_West.AddComponent<MajorBreakable>();
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(ExpandObjectDatabase.ChestBrownTwoItems.GetComponent<MajorBreakable>()), chestWestBreakable);
            chestWestBreakable.spriteNameToUseAtZeroHP = "chest_west_break_001";
            
            
            
            EX_RedBalloon = expandSharedAssets1.LoadAsset<GameObject>("EX_RedBalloon");
            tk2dSprite m_RedBalloonSprite = SpriteSerializer.AddSpriteToObject(EX_RedBalloon, EXBalloonCollection, "redballoon_idle_001");
            m_RedBalloonSprite.HeightOffGround = 1;
            EX_RedBalloon.AddComponent<ExpandBalloonController>();

            EX_GreenBalloon = expandSharedAssets1.LoadAsset<GameObject>("EX_GreenBalloon");
            tk2dSprite m_GreenBalloonSprite = SpriteSerializer.AddSpriteToObject(EX_GreenBalloon, EXBalloonCollection, "greenballoon_idle_001");
            m_GreenBalloonSprite.HeightOffGround = 2;
            EX_GreenBalloon.AddComponent<ExpandBalloonController>();

            EX_BlueBalloon = expandSharedAssets1.LoadAsset<GameObject>("EX_BlueBalloon");
            tk2dSprite m_BlueBalloonSprite = SpriteSerializer.AddSpriteToObject(EX_BlueBalloon, EXBalloonCollection, "blueballoon_idle_001");
            m_BlueBalloonSprite.HeightOffGround = 1.25f;
            EX_BlueBalloon.AddComponent<ExpandBalloonController>();

            EX_PinkBalloon = expandSharedAssets1.LoadAsset<GameObject>("EX_PinkBalloon");
            tk2dSprite m_PinkBalloonSprite = SpriteSerializer.AddSpriteToObject(EX_PinkBalloon, EXBalloonCollection, "pinkballoon_idle_001");
            m_PinkBalloonSprite.HeightOffGround = 1.3f;
            EX_PinkBalloon.AddComponent<ExpandBalloonController>();

            EX_YellowBalloon = expandSharedAssets1.LoadAsset<GameObject>("EX_YellowBalloon");
            tk2dSprite m_YellowBalloonSprite = SpriteSerializer.AddSpriteToObject(EX_YellowBalloon, EXBalloonCollection, "yellowballoon_idle_001");
            m_YellowBalloonSprite.HeightOffGround = 1.45f;
            EX_YellowBalloon.AddComponent<ExpandBalloonController>();

                        
            List<string> m_RedBalloonPopFrames = new List<string>() {
                "redballoon_pop_001",
                "redballoon_pop_002",
                "redballoon_pop_003",
            };

            List<string> m_BlueBalloonPopFrames = new List<string>() {
                "blueballoon_pop_001",
                "blueballoon_pop_002",
                "blueballoon_pop_003",
            };

            List<string> m_GreenBalloonPopFrames = new List<string>() {
                "greenballoon_pop_001",
                "greenballoon_pop_002",
                "greenballoon_pop_003",
            };

            List<string> m_PinkBalloonPopFrames = new List<string>() {
                "pinkballoon_pop_001",
                "pinkballoon_pop_002",
                "pinkballoon_pop_003",
            };

            List<string> m_YellowBalloonPopFrames = new List<string>() {
                "yellowballoon_pop_001",
                "yellowballoon_pop_002",
                "yellowballoon_pop_003",
            };
            
            ExpandUtility.GenerateSpriteAnimator(EX_RedBalloon);
            ExpandUtility.GenerateSpriteAnimator(EX_BlueBalloon);
            ExpandUtility.GenerateSpriteAnimator(EX_GreenBalloon);
            ExpandUtility.GenerateSpriteAnimator(EX_PinkBalloon);
            ExpandUtility.GenerateSpriteAnimator(EX_YellowBalloon);

            ExpandUtility.AddAnimation(EX_RedBalloon.GetComponent<tk2dSpriteAnimator>(), EXBalloonCollection.GetComponent<tk2dSpriteCollectionData>(), m_RedBalloonPopFrames, "pop", frameRate: 12);
            ExpandUtility.AddAnimation(EX_BlueBalloon.GetComponent<tk2dSpriteAnimator>(), EXBalloonCollection.GetComponent<tk2dSpriteCollectionData>(), m_BlueBalloonPopFrames, "pop", frameRate: 12);
            ExpandUtility.AddAnimation(EX_GreenBalloon.GetComponent<tk2dSpriteAnimator>(), EXBalloonCollection.GetComponent<tk2dSpriteCollectionData>(), m_GreenBalloonPopFrames, "pop", frameRate: 12);
            ExpandUtility.AddAnimation(EX_PinkBalloon.GetComponent<tk2dSpriteAnimator>(), EXBalloonCollection.GetComponent<tk2dSpriteCollectionData>(), m_PinkBalloonPopFrames, "pop", frameRate: 12);
            ExpandUtility.AddAnimation(EX_YellowBalloon.GetComponent<tk2dSpriteAnimator>(), EXBalloonCollection.GetComponent<tk2dSpriteCollectionData>(), m_YellowBalloonPopFrames, "pop", frameRate: 12);


            EXSpace_Grass_01 = expandSharedAssets1.LoadAsset<GameObject>("EXSpace_Grass_01");
            //EXSpace_Grass_01.layer = LayerMask.NameToLayer("BG_Critical");
            tk2dSprite m_SpaceGrassSprite_01 = SpriteSerializer.AddSpriteToObject(EXSpace_Grass_01, EXSpaceCollection,"spacegrass_01");
            m_SpaceGrassSprite_01.HeightOffGround = -1;
            m_SpaceGrassSprite_01.usesOverrideMaterial = true;
            m_SpaceGrassSprite_01.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;

            EXSpace_Grass_02 = expandSharedAssets1.LoadAsset<GameObject>("EXSpace_Grass_02");
            tk2dSprite m_SpaceGrassSprite_02 = SpriteSerializer.AddSpriteToObject(EXSpace_Grass_02, EXSpaceCollection, "spacegrass_02");
            m_SpaceGrassSprite_02.HeightOffGround = -1;
            m_SpaceGrassSprite_02.usesOverrideMaterial = true;
            m_SpaceGrassSprite_02.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;

            EXSpace_Grass_03 = expandSharedAssets1.LoadAsset<GameObject>("EXSpace_Grass_03");
            tk2dSprite m_SpaceGrassSprite_03 = SpriteSerializer.AddSpriteToObject(EXSpace_Grass_03, EXSpaceCollection, "spacegrass_03");
            m_SpaceGrassSprite_03.HeightOffGround = -1;
            m_SpaceGrassSprite_03.usesOverrideMaterial = true;
            m_SpaceGrassSprite_03.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;

            EXSpace_Grass_04 = expandSharedAssets1.LoadAsset<GameObject>("EXSpace_Grass_04");
            tk2dSprite m_SpaceGrassSprite_04 = SpriteSerializer.AddSpriteToObject(EXSpace_Grass_04, EXSpaceCollection, "spacegrass_04");
            m_SpaceGrassSprite_04.HeightOffGround = -1;
            m_SpaceGrassSprite_04.usesOverrideMaterial = true;
            m_SpaceGrassSprite_04.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;


            
            ChallengeManagerObject = braveResources.LoadAsset<GameObject>("_ChallengeManager");
            ChallengeMegaManagerObject = braveResources.LoadAsset<GameObject>("_ChallengeMegaManager");

            ChallengeManager challengeManager = ChallengeManagerObject.GetComponent<ChallengeManager>();
            ChallengeManager challengeMegaManager = ChallengeMegaManagerObject.GetComponent<ChallengeManager>();
            

            RatsRevengChallenge = challengeManager.PossibleChallenges[8].challenge.GetComponent<FlameTrapChallengeModifier>();
            RatsRevengChallenge.FlameTrap = RatTrapPlacable;

            Challenge_BlobulinAmmo = challengeManager.PossibleChallenges[1].challenge.gameObject;
            Challenge_BlobulinAmmo.AddComponent<ExpandBlobulinRancherChallengeComponent>();
            ExpandBlobulinRancherChallengeComponent m_ExpandBlobRancher = Challenge_BlobulinAmmo.GetComponent<ExpandBlobulinRancherChallengeComponent>();
            BlobulinAmmoChallengeModifier m_BlobRancher = Challenge_BlobulinAmmo.GetComponent<BlobulinAmmoChallengeModifier>();
            m_ExpandBlobRancher.DisplayName = m_BlobRancher.DisplayName;
            m_ExpandBlobRancher.AlternateLanguageDisplayName = m_BlobRancher.AtlasSpriteName;
            m_ExpandBlobRancher.AtlasSpriteName = m_BlobRancher.AtlasSpriteName;
            m_ExpandBlobRancher.ValidInBossChambers = m_BlobRancher.ValidInBossChambers;
            m_ExpandBlobRancher.MutuallyExclusive = m_BlobRancher.MutuallyExclusive;
            m_ExpandBlobRancher.CooldownBetweenSpawns = m_BlobRancher.CooldownBetweenSpawns;
            m_ExpandBlobRancher.SafeRadius = m_BlobRancher.SafeRadius;
            m_ExpandBlobRancher.SpawnTargetGuid = m_BlobRancher.SpawnTargetGuid;
            UnityEngine.Object.Destroy(Challenge_BlobulinAmmo.GetComponent<BlobulinAmmoChallengeModifier>());
            
            challengeManager.PossibleChallenges[1].challenge = Challenge_BlobulinAmmo.GetComponent<ExpandBlobulinRancherChallengeComponent>();
            challengeMegaManager.PossibleChallenges[1].challenge = Challenge_BlobulinAmmo.GetComponent<ExpandBlobulinRancherChallengeComponent>();

            Challenge_BooRoom = challengeManager.PossibleChallenges[2].challenge.gameObject;
            Challenge_BooRoom.AddComponent<ExpandBooRoomChallengeComponent>();
            BooRoomChallengeModifier m_BooRoomChallenge = Challenge_BooRoom.GetComponent<BooRoomChallengeModifier>();
            ExpandBooRoomChallengeComponent m_ExpandBooRoomChallenge = Challenge_BooRoom.GetComponent<ExpandBooRoomChallengeComponent>();
            m_ExpandBooRoomChallenge.DisplayName = m_BooRoomChallenge.DisplayName;
            m_ExpandBooRoomChallenge.AlternateLanguageDisplayName = m_BooRoomChallenge.AtlasSpriteName;
            m_ExpandBooRoomChallenge.AtlasSpriteName = m_BooRoomChallenge.AtlasSpriteName;
            m_ExpandBooRoomChallenge.ValidInBossChambers = m_BooRoomChallenge.ValidInBossChambers;
            m_ExpandBooRoomChallenge.MutuallyExclusive = m_BooRoomChallenge.MutuallyExclusive;
            m_ExpandBooRoomChallenge.ConeAngle = m_BooRoomChallenge.ConeAngle;
            m_ExpandBooRoomChallenge.DarknessEffectShader = m_BooRoomChallenge.DarknessEffectShader;
            UnityEngine.Object.Destroy(Challenge_BooRoom.GetComponent<BooRoomChallengeModifier>());

            challengeManager.PossibleChallenges[2].challenge = Challenge_BooRoom.GetComponent<ExpandBooRoomChallengeComponent>();
            challengeMegaManager.PossibleChallenges[2].challenge = Challenge_BooRoom.GetComponent<ExpandBooRoomChallengeComponent>();


            Challenge_ZoneControl = challengeManager.PossibleChallenges[21].challenge.gameObject;
            Challenge_ZoneControl.AddComponent<ExpandZoneControlChallengeComponent>();
            ZoneControlChallengeModifier m_ZoneControlChallenge = Challenge_ZoneControl.GetComponent<ZoneControlChallengeModifier>();
            ExpandZoneControlChallengeComponent m_ExpandZoneControlChallenge = Challenge_ZoneControl.GetComponent<ExpandZoneControlChallengeComponent>();
            m_ExpandZoneControlChallenge.DisplayName = m_ZoneControlChallenge.DisplayName;
            m_ExpandZoneControlChallenge.AlternateLanguageDisplayName = m_ZoneControlChallenge.AtlasSpriteName;
            m_ExpandZoneControlChallenge.AtlasSpriteName = m_ZoneControlChallenge.AtlasSpriteName;
            m_ExpandZoneControlChallenge.ValidInBossChambers = m_ZoneControlChallenge.ValidInBossChambers;
            m_ExpandZoneControlChallenge.MutuallyExclusive = m_ZoneControlChallenge.MutuallyExclusive;
            m_ExpandZoneControlChallenge.BoxPlaceable = m_ZoneControlChallenge.BoxPlaceable;
            m_ExpandZoneControlChallenge.AuraRadius = m_ZoneControlChallenge.AuraRadius;
            m_ExpandZoneControlChallenge.WinTimer = m_ZoneControlChallenge.WinTimer;
            m_ExpandZoneControlChallenge.DecayScale = m_ZoneControlChallenge.DecayScale;
            m_ExpandZoneControlChallenge.MinBoxes = m_ZoneControlChallenge.MinBoxes;
            m_ExpandZoneControlChallenge.ExtraBoxAboveArea = m_ZoneControlChallenge.ExtraBoxAboveArea;
            m_ExpandZoneControlChallenge.ExtraBoxEveryArea = m_ZoneControlChallenge.ExtraBoxEveryArea;
            UnityEngine.Object.Destroy(Challenge_ZoneControl.GetComponent<ZoneControlChallengeModifier>());

            challengeManager.PossibleChallenges[21].challenge = Challenge_ZoneControl.GetComponent<ExpandZoneControlChallengeComponent>();
            challengeMegaManager.PossibleChallenges[21].challenge = Challenge_ZoneControl.GetComponent<ExpandZoneControlChallengeComponent>();

            Challenge_ChaosMode = expandSharedAssets1.LoadAsset<GameObject>("ExpandChallenge_ChaosMode");
            Challenge_ChaosMode.AddComponent<ExpandChaosChallengeComponent>();
            
            
            ExpandChaosChallengeComponent expandChaosChallenge = Challenge_ChaosMode.GetComponent<ExpandChaosChallengeComponent>();
            expandChaosChallenge.MutuallyExclusive = new List<ChallengeModifier>() {
                challengeMegaManager.PossibleChallenges[3].challenge,
                challengeMegaManager.PossibleChallenges[4].challenge,
                challengeMegaManager.PossibleChallenges[10].challenge
            };
                        
            challengeManager.PossibleChallenges.Add(new ChallengeDataEntry() {
                Annotation = "Apache Thunder's Chaos Mode in a room!",
                challenge = Challenge_ChaosMode.GetComponent<ExpandChaosChallengeComponent>(),
                excludedTilesets = 0,
                tilesetsWithCustomValues = new List<GlobalDungeonData.ValidTilesets>(0),
                CustomValues = new List<int>(0)
            });

            challengeMegaManager.PossibleChallenges.Add(new ChallengeDataEntry() {
                Annotation = "Apache Thunder's Chaos Mode in a room!",
                challenge = Challenge_ChaosMode.GetComponent<ExpandChaosChallengeComponent>(),                
                excludedTilesets = 0,
                tilesetsWithCustomValues = new List<GlobalDungeonData.ValidTilesets>(0),
                CustomValues = new List<int>(0)
            });


            Challenge_TripleTrouble = expandSharedAssets1.LoadAsset<GameObject>("ExpandChallenge_TripleTrouble");
            Challenge_TripleTrouble.AddComponent<ExpandTripleTroubleChallengeComponent>();            
            
            Challenge_KingsMen = expandSharedAssets1.LoadAsset<GameObject>("ExpandChallenge_KingsMen");
            Challenge_KingsMen.AddComponent<ExpandBulletKingChallenge>();
            
            challengeManager.BossChallenges.Add(new BossChallengeData() {
                Annotation = "Trigger Twin Triple Trouble!",
                BossGuids = new string[] { "ea40fcc863d34b0088f490f4e57f8913", "c00390483f394a849c36143eb878998f" },
                Modifiers = new ChallengeModifier[] { Challenge_TripleTrouble.GetComponent<ExpandTripleTroubleChallengeComponent>() },
                NumToSelect = 1                
            });

            challengeMegaManager.BossChallenges.Add(new BossChallengeData() {
                Annotation = "Trigger Twin Triple Trouble!",
                BossGuids = new string[] { "ea40fcc863d34b0088f490f4e57f8913", "c00390483f394a849c36143eb878998f" },
                Modifiers = new ChallengeModifier[] { Challenge_TripleTrouble.GetComponent<ExpandTripleTroubleChallengeComponent>() },
                NumToSelect = 1
            });

            challengeManager.BossChallenges.Add(new BossChallengeData() {
                Annotation = "All the King's Men",
                BossGuids = new string[] { "ffca09398635467da3b1f4a54bcfda80", "5729c8b5ffa7415bb3d01205663a33ef" },
                Modifiers = new ChallengeModifier[] { Challenge_KingsMen.GetComponent<ExpandBulletKingChallenge>() },
                NumToSelect = 1
            });

            challengeMegaManager.BossChallenges.Add(new BossChallengeData() {
                Annotation = "All the King's Men",
                BossGuids = new string[] { "ffca09398635467da3b1f4a54bcfda80", "5729c8b5ffa7415bb3d01205663a33ef" },
                Modifiers = new ChallengeModifier[] { Challenge_KingsMen.GetComponent<ExpandBulletKingChallenge>() },
                NumToSelect = 1
            });

            winchesterrooms = m_WinchesterRooms.ToArray();
            
            ExpandSecretDoorPrefabs.InitPrefabs(expandSharedAssets1);

            ChamberGun = (PickupObjectDatabase.GetById(647) as Gun).gameObject;

            if (ChamberGun.gameObject.GetComponent<ChamberGunProcessor>()) {
                UnityEngine.Object.Destroy(ChamberGun.gameObject.GetComponent<ChamberGunProcessor>());
                ChamberGun.gameObject.AddComponent<ExpandChamberGunProcessor>();
            }
                        
            ExpandLists.CustomChests.Add(RickRollChestObject);
            ExpandLists.CustomChests.Add(SurpriseChestObject);
            
            m_gungeon_rewardroom_1 = null;
            // Null any Dungeon prefabs you call up when done else you'll break level generation for that prefab on future level loads!
            TutorialDungeonPrefab = null;
            CastleDungeonPrefab = null;
            GungeonDungeonPrefab = null;
            SewerDungeonPrefab = null;
            MinesDungeonPrefab = null;
            ratDungeon = null;
            CathedralDungeonPrefab = null;
            BulletHellDungeonPrefab = null;
            ForgeDungeonPrefab = null;
            CatacombsDungeonPrefab = null;
            NakatomiDungeonPrefab = null;

            ExpandMorePrefabs.InitMoreCustomPrefabs(expandSharedAssets1, sharedAssets, sharedAssets2, braveResources, enemiesBase);
        }
    }
}

