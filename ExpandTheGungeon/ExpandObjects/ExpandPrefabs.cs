using Dungeonator;
using System;
using System.Collections.Generic;
using UnityEngine;
using Gungeon;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandDungeonFlows;
using System.Reflection;
using FullInspector;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandPrefabs : MonoBehaviour {
        
        private static Dungeon TutorialDungeonPrefab;
        private static Dungeon SewerDungeonPrefab;
        private static Dungeon MinesDungeonPrefab;
        private static Dungeon ratDungeon;
        private static Dungeon CathedralDungeonPrefab;
        private static Dungeon BulletHellDungeonPrefab;
        private static Dungeon ForgeDungeonPrefab;
        private static Dungeon CatacombsDungeonPrefab;
        private static Dungeon NakatomiDungeonPrefab;

        // Materials
        public static Material SpaceFog;        

        // Custom Textures
        public static Texture2D ENV_Tileset_Belly_Texture;
        public static Texture2D ENV_Tileset_West_Texture;
        public static Texture2D BulletManMonochromeTexture;
        public static Texture2D BulletManUpsideDownTexture;
        public static Texture2D RedBulletShotgunManTexture;
        public static Texture2D BlueBulletShotgunManTexture;
        public static Texture2D BulletManEyepatchTexture;
        
        // Rat Trap Door
        public static GameObject RatTrapdoor;
        public static ResourcefulRatMinesHiddenTrapdoor RRMinesHiddenTrapDoorController;

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
        
        // public static PrototypeDungeonRoom beholsterroom01 = sharedAssets.LoadAsset<PrototypeDungeonRoom>("beholsterroom01");

        // Secret rooms from Rat Trap door
        public static PrototypeDungeonRoom ResourcefulRat_LongMinecartRoom_01;
        public static PrototypeDungeonRoom ResourcefulRat_FirstSecretRoom_01;
        public static PrototypeDungeonRoom ResourcefulRat_SecondSecretRoom_01;

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

        public static WeightedRoom[] OfficeAndUnusedWeightedRooms;


        // Modified Loot tables
        public static GenericLootTable Shop_Key_Items_01;
        public static GenericLootTable BlackSmith_Items_01;
        public static GenericLootTable Shop_Truck_Items_01;
        public static GenericLootTable Shop_Curse_Items_01;

        // Modified Flow Injection Data
        public static ProceduralFlowModifierData AbbeyFlowModifierData;

        // Items
        // public static PickupObject RatKeyItem;

        // Object Prefabs
        private static GameObject MetalGearRatPrefab;
        private static GameObject ResourcefulRatBossPrefab;
        private static AIActor MetalGearRatActorPrefab;
        private static AIActor ResourcefulRatBossActorPrefab;
        private static MetalGearRatDeathController MetalGearRatDeathPrefab;
        private static PunchoutController PunchoutPrefab;
        private static ResourcefulRatController resourcefulRatControllerPrefab;
        private static FoldingTableItem FoldingTablePrefab;
        // public static GameObject NPCLunk;
        // public static GameObject FoldingTable;




        public static GameObject MimicNPC;
        public static GameObject RatCorpseNPC;
        public static GameObject PlayerLostRatNote;
        public static GameObject MouseTrap1;
        public static GameObject MouseTrap2;
        public static GameObject MouseTrap3;
        // Custom Trap Objects
        public static GameObject EXTrap_Apache;


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
        // public static GameObject SellPit;


        // DungeonPlacables        
        public static DungeonPlaceable ElevatorDeparture;
        public static DungeonPlaceable ElevatorArrival;

        // Custom Placables
        public static DungeonPlaceable TinySecretRoomRewards;
        public static DungeonPlaceable TinySecretRoomJunkReward;
        public static DungeonPlaceable RatTrapPlacable;
        public static DungeonPlaceable CorruptedSecretRoomSpecialItem;
        public static DungeonPlaceable Jungle_Doors;
        public static DungeonPlaceable Belly_Doors;
        public static DungeonPlaceable West_Doors;

        // Modified/Reference AIActors
        public static GameObject MetalCubeGuy;
        public static GameObject SerManuel;
        public static GameObject SkusketHead;
        public static GameObject CandleGuy;
        public static GameObject WallMimic;

        // Test
        // public static AIActor SpectreTest = EnemyDatabase.GetOrLoadByGuid("56f5a0f2c1fc4bc78875aea617ee31ac"); // spectre
        
        public static DungeonPlaceableBehaviour RatJailDoorPlacable;
        public static DungeonPlaceableBehaviour CurrsedMirrorPlacable;
        
        // Used for forcing Arrival Elevator to spawn on phobos floor tileset ID.
        private static DungeonPlaceableVariant ElevatorArrivalVarientForNakatomi;
        private static DungeonPlaceableVariant ElevatorDepartureVarientForRatNakatomi;

        // Modified Challenge Modifiers/Challenge Objects
        public static GameObject ChallengeManagerObject;
        public static GameObject ChallengeMegaManagerObject;
        public static FlameTrapChallengeModifier RatsRevengChallenge;
        public static GameObject Challenge_BlobulinAmmo;
        public static GameObject Challenge_BooRoom;
        public static GameObject Challenge_ZoneControl;

        // Custom Objects
        public static GameObject RoomCorruptionAmbience;
        public static GameObject EXAlarmMushroom;
        // public static GameObject EXTrapDoor;
        // public static GameObject EXTrapDoorBorder;
        // public static GameObject EXTrapDoorPit;
        public static GameObject EXPlayerMimicBoss;
        public static GameObject EXSawBladeTrap_4x4Zone;
        public static GameObject EXFriendlyForgeHammer;
        public static GameObject EXFriendlyForgeHammerBullet;
        public static GameObject EXBootlegRoomObject;
        public static GameObject EXBootlegRoomDoorTriggers;
        // RewardPedastal for Glitch Secret Room
        public static GameObject CorruptedRewardPedestal;
        public static GameObject RickRollChestObject;
        public static GameObject RickRollAnimationObject;
        public static GameObject RickRollMusicSwitchObject;
        public static GameObject ExpandThunderstormPlaceable;
        public static GameObject Door_Horizontal_Jungle;
        public static GameObject Door_Vertical_Jungle;
        public static GameObject Jungle_LargeTree;
        public static GameObject Jungle_LargeTreeTopFrame;
        public static GameObject EXJungleTree_MinimapIcon;
        public static GameObject EXJungleCrest_MinimapIcon;
        public static GameObject Jungle_ExitLadder;
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
        public static GameObject EXSpaceFloor_50x50;
        public static GameObject EXSpaceFloorPitBorder_50x50;

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

        // Custom Object for Glitch Floor Screen FX controller
        public static GameObject EXGlitchFloorScreenFX;

        // Modified Nakatomi Light to match the one Jungle used
        public static GameObject JungleLight;
        // Belly Light prefabs for Belly DungeonMaterial
        public static GameObject BellyLight;
        // West Light
        public static GameObject WestLight;

        // Cactus Objects for West
        public static GameObject Cactus_A;
        public static GameObject Cactus_B;
        public static GameObject CactusShard1;
        public static GameObject CactusShard2;

        // Custom Reward Pedestals (using custom component)
        public static GameObject BlankRewardPedestal;
        public static GameObject RatKeyRewardPedestal;

        // Custom Dungeon Sprite Collection Objects. (now loaded via custom asset bundle! These aren't fake prefabs!)
        public static GameObject ENV_Tileset_Belly;
        public static GameObject ENV_Tileset_West;

        // Custom Challenge Modifiers
        public static GameObject Challenge_ChaosMode;
        public static GameObject Challenge_TripleTrouble;
        public static GameObject Challenge_KingsMen;

        // Modified Items
        public static Gun ChamberGun;
       
        public static void InitCustomPrefabs(AssetBundle expandSharedAssets1, AssetBundle sharedAssets, AssetBundle sharedAssets2, AssetBundle braveResources, AssetBundle enemiesBase) {

            TutorialDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Tutorial");
            SewerDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Sewer");
            MinesDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Mines");
            ratDungeon = DungeonDatabase.GetOrLoadByName("base_resourcefulrat");
            CathedralDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Cathedral");
            BulletHellDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_BulletHell");
            ForgeDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Forge");
            CatacombsDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Catacombs");
            NakatomiDungeonPrefab = DungeonDatabase.GetOrLoadByName("base_nakatomi");
                                    
            ExpandObjectDatabase objectDatabase = new ExpandObjectDatabase();

            SpaceFog = PickupObjectDatabase.GetById(597).gameObject.GetComponent<GunParticleSystemController>().TargetSystem.gameObject.GetComponent<ParticleSystemRenderer>().materials[0];
                                    
            ENV_Tileset_Belly_Texture = expandSharedAssets1.LoadAsset<Texture2D>("ENV_Tileset_Belly");
            ENV_Tileset_West_Texture = expandSharedAssets1.LoadAsset<Texture2D>("ENV_Tileset_West");
                                    
            ENV_Tileset_Belly = expandSharedAssets1.LoadAsset<GameObject>("ENV_Tileset_Belly");
            ExpandDungeonCollections.ENV_Tileset_Belly(ENV_Tileset_Belly, sharedAssets);

            ENV_Tileset_West = expandSharedAssets1.LoadAsset<GameObject>("ENV_Tileset_West");
            ExpandDungeonCollections.ENV_Tileset_West(ENV_Tileset_West, sharedAssets);
            

            BulletManMonochromeTexture = expandSharedAssets1.LoadAsset<Texture2D>("BulletMan_Monochrome");
            BulletManUpsideDownTexture = expandSharedAssets1.LoadAsset<Texture2D>("BulletMan_UpsideDown");
            
            RedBulletShotgunManTexture = expandSharedAssets1.LoadAsset<Texture2D>("RedBulletShotgunMan");
            BlueBulletShotgunManTexture = expandSharedAssets1.LoadAsset<Texture2D>("BlueBulletShotgunMan");
            BulletManEyepatchTexture = expandSharedAssets1.LoadAsset<Texture2D>("BulletManEyepatch");
            
            RatTrapdoor = MinesDungeonPrefab.RatTrapdoor;
            RRMinesHiddenTrapDoorController = RatTrapdoor.GetComponent<ResourcefulRatMinesHiddenTrapdoor>();

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
            
            ResourcefulRat_LongMinecartRoom_01 = RRMinesHiddenTrapDoorController.TargetMinecartRoom;
            ResourcefulRat_FirstSecretRoom_01 = RRMinesHiddenTrapDoorController.FirstSecretRoom;
            ResourcefulRat_SecondSecretRoom_01 = RRMinesHiddenTrapDoorController.SecondSecretRoom;

            tiny_entrance = Instantiate(TutorialDungeonPrefab.PatternSettings.flows[0].AllNodes[4].overrideExactRoom);
            tiny_exit = Instantiate(TutorialDungeonPrefab.PatternSettings.flows[0].AllNodes[4].overrideExactRoom);
            reward_room = sharedAssets2.LoadAsset<PrototypeDungeonRoom>("reward room");
            tutorial_minibossroom = Instantiate(TutorialDungeonPrefab.PatternSettings.flows[0].AllNodes[8].overrideExactRoom);
            bossrush_alternate_entrance = Instantiate(test_entrance);
            tutorial_fakeboss = Instantiate(DraGunRoom01);
            big_entrance = Instantiate(sharedAssets.LoadAsset<PrototypeDungeonRoom>("GatlingGullRoom05"));

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

            gungeon_entrance_bossrush = Instantiate(gungeon_entrance);
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
            
            Shop_Curse_Items_01.defaultItemDrops.Add(
                new WeightedGameObject() {
                    rawGameObject = null,
                    pickupId = CursedBrick.CursedBrickID,
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
            

            // RatKeyItem = PickupObjectDatabase.GetById(727);

            MetalGearRatPrefab = enemiesBase.LoadAsset<GameObject>("MetalGearRat");
            ResourcefulRatBossPrefab = enemiesBase.LoadAsset<GameObject>("ResourcefulRat_Boss");
            MetalGearRatActorPrefab = MetalGearRatPrefab.GetComponent<AIActor>();
            ResourcefulRatBossActorPrefab = ResourcefulRatBossPrefab.GetComponent<AIActor>();
            MetalGearRatDeathPrefab = MetalGearRatActorPrefab.GetComponent<MetalGearRatDeathController>();
            PunchoutPrefab = MetalGearRatDeathPrefab.PunchoutMinigamePrefab.GetComponent<PunchoutController>();
            resourcefulRatControllerPrefab = ResourcefulRatBossActorPrefab.GetComponent<ResourcefulRatController>();
            // FoldingTablePrefab = ETGMod.Databases.Items[644].GetComponent<FoldingTableItem>();
            FoldingTablePrefab = PickupObjectDatabase.GetById(644).GetComponent<FoldingTableItem>();

            SewersRatExitEoom = SewerDungeonPrefab.PatternSettings.flows[0].sharedInjectionData[1].InjectionData[0].exactRoom;
            // SewersRatExitEoom.placedObjects[0].nonenemyBehaviour.gameObject.AddComponent<ExpandSecretFloorController>();

            // FoldingTable = ETGMod.Databases.Items[644].GetComponent<FoldingTableItem>().TableToSpawn.gameObject;
            // FoldingTable = FoldingTablePrefab.TableToSpawn.gameObject;

            // NPCLunk = sharedAssets.LoadAsset<GameObject>("NPC_LostAdventurer");

            MimicNPC = ratDungeon.PatternSettings.flows[0].AllNodes[12].overrideExactRoom.additionalObjectLayers[0].placedObjects[13].nonenemyBehaviour.gameObject;

            RatCorpseNPC = PunchoutPrefab.PlayerWonRatNPC.gameObject;
            PlayerLostRatNote = PunchoutPrefab.PlayerLostNotePrefab.gameObject;
            MouseTrap1 = resourcefulRatControllerPrefab.MouseTraps[0];
            MouseTrap2 = resourcefulRatControllerPrefab.MouseTraps[1];
            MouseTrap3 = resourcefulRatControllerPrefab.MouseTraps[2];

            // Custom Trap Object for Rainbow room
            EXTrap_Apache = expandSharedAssets1.LoadAsset<GameObject>("EX_Trap_Apache");
            ItemBuilder.AddSpriteToObject(EXTrap_Apache, expandSharedAssets1.LoadAsset<Texture2D>("EX_Trap_Apache_01"), false, false);
            SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>("EX_Trap_Apache_02"), EXTrap_Apache.GetComponent<tk2dSprite>().Collection);
            EXTrap_Apache.GetComponent<tk2dSprite>().HeightOffGround = 2;

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
            
            ExpandUtility.AddAnimation(ApacheTrapAnimator, EXTrap_Apache.GetComponent<tk2dSprite>().Collection, ApacheTrap_Normal, "ApacheTrap_Normal", tk2dSpriteAnimationClip.WrapMode.Loop, frameRate: 8);
            ExpandUtility.AddAnimation(ApacheTrapAnimator, EXTrap_Apache.GetComponent<tk2dSprite>().Collection, ApacheTrap_Flipped, "ApacheTrap_Flipped", tk2dSpriteAnimationClip.WrapMode.Loop, frameRate: 8);
            
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
                // ExpandUtility.DuplicateComponent(EXTrap_Apache.GetComponent<BehaviorSpeculator>(), RadialFireBurster.GetComponent<BehaviorSpeculator>());
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
                // Loading a custom script from text file in place of one from an existing prefab..
                m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = ExpandUtilities.ResourceExtractor.BuildStringListFromEmbeddedResource("SerializedData\\BehaviorScripts\\ApacheTrap_BehaviorScript.txt");
                
                EXTrap_Apache.AddComponent<TrapEnemyConfigurator>();
            }
            

            
            Teleporter_Gungeon_01 = braveResources.LoadAsset<GameObject>("Teleporter_Gungeon_01");
            ElevatorMaintanenceRoomIcon = sharedAssets2.LoadAsset<GameObject>("Minimap_Maintenance_Icon");
            Teleporter_Info_Sign = sharedAssets2.LoadAsset<GameObject>("teleporter_info_sign");
            RewardPedestalPrefab = sharedAssets.LoadAsset<GameObject>("Boss_Reward_Pedestal");
            Minimap_Maintenance_Icon = sharedAssets2.LoadAsset<GameObject>("minimap_maintenance_icon");

            // Forge Hammer prefab for Baby Good Hammer
            ForgeHammer = sharedAssets.LoadAsset<GameObject>("Forge_Hammer");

            EXFriendlyForgeHammerBullet = Instantiate(ForgeHammer.GetComponent<ForgeHammerController>().bulletBank.Bullets[0].BulletObject);
            EXFriendlyForgeHammerBullet.SetActive(false);
            EXFriendlyForgeHammerBullet.name = "8x8_fireball_companion_projectile_dark";
            FakePrefab.MarkAsFakePrefab(EXFriendlyForgeHammerBullet);
            DontDestroyOnLoad(EXFriendlyForgeHammerBullet);

            ExpandForgeHammerComponent.BuildPrefab();


            // SquareLightCookie = sharedAssets2.LoadAsset<GameObject>("SquareLightCookie");
            // Arrival = SquareLightCookie.transform.Find("Arrival");
            Arrival = expandSharedAssets1.LoadAsset<GameObject>("Arrival");
            Arrival.transform.name = "Arrival";
            /*FakePrefab.MarkAsFakePrefab(Arrival);
            Arrival.SetActive(false);
            DontDestroyOnLoad(Arrival);*/

            // NPCBabyDragunChaos = Instantiate(sharedAssets2.LoadAsset<GameObject>("BabyDragunJail"));
            NPCBabyDragunChaos = expandSharedAssets1.LoadAsset<GameObject>("Chaos Baby Dragun");
            // NPCBabyDragunChaos.SetActive(false);
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
            /*DontDestroyOnLoad(NPCBabyDragunChaos);
            FakePrefab.MarkAsFakePrefab(NPCBabyDragunChaos);*/


            // SellPit = sharedAssets2.LoadAsset<GameObject>("SellPit");

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

            RatJailDoorPlacable = ratDungeon.PatternSettings.flows[0].AllNodes[13].overrideExactRoom.placedObjects[1].nonenemyBehaviour;
            CurrsedMirrorPlacable = basic_special_rooms.includedRooms.elements[1].room.placedObjects[0].nonenemyBehaviour;

            ElevatorArrivalVarientForNakatomi = new DungeonPlaceableVariant() {
                percentChance = 0.1f,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                // nonDatabasePlaceable = ElevatorArrival.variantTiers[4].nonDatabasePlaceable,
                nonDatabasePlaceable = ElevatorArrival.variantTiers[0].nonDatabasePlaceable,
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

            ElevatorDepartureVarientForRatNakatomi = new DungeonPlaceableVariant() {
                percentChance = 0.1f,
                percentChanceMultiplier = 1,
                unitOffset = Vector2.zero,
                nonDatabasePlaceable = ElevatorDeparture.variantTiers[8].nonDatabasePlaceable,
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
                RoomBuilder.GenerateRoomLayoutFromTexture2D(Hell_Hath_No_Joery_009, expandSharedAssets1.LoadAsset<Texture2D>("Hell_Hath_No_Joery_009_Layout"));
            }

            List<PrototypeDungeonRoom> m_GatlingGullRooms = new List<PrototypeDungeonRoom>();

            foreach (WeightedRoom wRoom in bosstable_01_gatlinggull.includedRooms.elements) { m_GatlingGullRooms.Add(Instantiate(wRoom.room)); }

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

            PrototypeDungeonRoom m_gungeon_rewardroom_1 = Instantiate(gungeon_rewardroom_1);

            // Add teleporter to make it like the other reward rooms post AG&D update.
            RoomBuilder.AddObjectToRoom(reward_room, new Vector2(3, 1), NonEnemyBehaviour: Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());            
            // This Room Prefab didn't include a chest placer...lol. We'll use the one from gungeon_rewardroom_1. :P
            reward_room.additionalObjectLayers.Add(m_gungeon_rewardroom_1.additionalObjectLayers[1]);
            reward_room.additionalObjectLayers[1].placedObjects[0].contentsBasePosition = new Vector2(4f, 7.5f);
            reward_room.additionalObjectLayers[1].placedObjectBasePositions[0] = new Vector2(4f, 7.5f);


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
                        
            RoomBuilder.GenerateRoomLayoutFromTexture2D(big_entrance, expandSharedAssets1.LoadAsset<Texture2D>("Large_Elevator_Entrance_Layout"));

            MegaChallengeShrineTable.includedRooms = new WeightedRoomCollection();
            MegaChallengeShrineTable.includedRooms.elements = new List<WeightedRoom>();
            MegaChallengeShrineTable.includedRoomTables = new List<GenericRoomTable>(0);

            /*foreach (WeightedRoom weightedRoom in castle_challengeshrine_roomtable.includedRooms.elements) {
                MegaChallengeShrineTable.includedRooms.Add(weightedRoom);
            }*/
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
                MegaMiniBossRoomTable.includedRooms.Add(ExpandRoomPrefabs.GenerateWeightedRoom(Instantiate(weightedRoom.room), LimitedCopies: false));
            }
            foreach (WeightedRoom weightedRoom in phantomagunim_table_01.includedRooms.elements) {
                MegaMiniBossRoomTable.includedRooms.Add(ExpandRoomPrefabs.GenerateWeightedRoom(Instantiate(weightedRoom.room), LimitedCopies: false));
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


            // RatKeyItem.RespawnsIfPitfall = true;
            
            ElevatorArrival.variantTiers.Add(ElevatorArrivalVarientForNakatomi);
            ElevatorDeparture.variantTiers.Add(ElevatorDepartureVarientForRatNakatomi);

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
                Destroy(WallMimic.GetComponent<WallMimicController>());
                WallMimic.AddComponent<ExpandWallMimicManager>();
            }
            
            
            // CompanionController cuccoController = Cucco.gameObject.GetComponent<CompanionController>();
            // cuccoController.CanBePet = true;

            List<AGDEnemyReplacementTier> ReplacementTiers = GameManager.Instance.EnemyReplacementTiers;

            if (ReplacementTiers != null && ReplacementTiers.Count > 0) {
                foreach (AGDEnemyReplacementTier replacementTier in ReplacementTiers) {
                    if (replacementTier.RoomCantContain == null) { replacementTier.RoomCantContain = new List<string>(); }
                    replacementTier.RoomCantContain.Add(MetalCubeGuy.GetComponent<AIActor>().EnemyGuid);
                }
            }
            
            // SellPit.AddComponent<ExpandSellCellManager>();

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
                    winchesterRoom.associatedMinimapIcon = objectDatabase.WinchesterMinimapIcon;
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


            RoomCorruptionAmbience = expandSharedAssets1.LoadAsset<GameObject>("RoomCorruptionAmbience_Placable");

            RoomCorruptionAmbience.AddComponent<ExpandCorruptedRoomAmbiencePlacable>();
            DontDestroyOnLoad(RoomCorruptionAmbience);

            EXAlarmMushroom = expandSharedAssets1.LoadAsset<GameObject>("EX Alarm Mushroom");
            

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

            ItemBuilder.AddSpriteToObject(EXAlarmMushroom, expandSharedAssets1.LoadAsset<Texture2D>(m_AlarmMushroom_idleSprites[0]), false, false);
            tk2dSprite m_AlarmMushroomSprite = EXAlarmMushroom.GetComponent<tk2dSprite>();

            foreach (string spriteName in m_AlarmMushroom_idleSprites) {
                SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_AlarmMushroomSprite.Collection);
            }
            foreach (string spriteName in m_AlarmMushroom_alarmSprites) {
                SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_AlarmMushroomSprite.Collection);
            }
            foreach (string spriteName in m_AlarmMushroom_breakSprites) {
                SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_AlarmMushroomSprite.Collection);
            }
            
            ExpandUtility.GenerateSpriteAnimator(EXAlarmMushroom, playAutomatically: true, ClipFps: 8);
            tk2dSpriteAnimator m_AlarmMushroomAnimator = EXAlarmMushroom.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(m_AlarmMushroomAnimator, m_AlarmMushroomSprite.Collection, m_AlarmMushroom_idleSprites, "alarm_mushroom_idle", frameRate: 8, wrapMode: tk2dSpriteAnimationClip.WrapMode.LoopFidget, minFidgetDuration: 1, maxFidgetDuration: 2);
            ExpandUtility.AddAnimation(m_AlarmMushroomAnimator, m_AlarmMushroomSprite.Collection, m_AlarmMushroom_alarmSprites, "alarm_mushroom_alarm", frameRate: 8, wrapMode: tk2dSpriteAnimationClip.WrapMode.Loop);
            ExpandUtility.AddAnimation(m_AlarmMushroomAnimator, m_AlarmMushroomSprite.Collection, m_AlarmMushroom_breakSprites, "alarm_mushroom_break", frameRate: 8, wrapMode: tk2dSpriteAnimationClip.WrapMode.Once);
            SpeculativeRigidbody m_EXAlarmMushroomRigidBody = ExpandUtility.GenerateOrAddToRigidBody(EXAlarmMushroom, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(7, 10), offset: new IntVector2(2, 7));

            ExpandAlarmMushroomPlacable m_AlarmMushRoomPlacable = EXAlarmMushroom.AddComponent<ExpandAlarmMushroomPlacable>();
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

            /*FakePrefab.MarkAsFakePrefab(EXAlarmMushroom);
            DontDestroyOnLoad(EXAlarmMushroom);*/
            

            /*string m_TrapDoorBasePath = "ExpandTheGungeon/Textures/EXTrapDoor/";

            List<string> m_TrapDoorSprites = new List<string>() {
                "RR_mine_lair_floor_door_001",
                "RR_mine_lair_floor_door_002",
                "RR_mine_lair_floor_door_003",
                "RR_mine_lair_floor_door_004",
                "RR_mine_lair_floor_door_005",
                "RR_mine_lair_floor_door_006",
                "RR_mine_lair_floor_door_007",
                "RR_mine_lair_floor_door_008"
            };
            
            EXTrapDoor = new GameObject("EX Trap Door") { layer = 20 };
            EXTrapDoorBorder = new GameObject("EX Trap Door Border") { layer = 20 };
            EXTrapDoorPit = new GameObject("EX Trap Door Pit") { layer = 20 };
            EXTrapDoor.transform.localPosition += new Vector3(0, 0, 1.25f);

            EXTrapDoorBorder.transform.parent = EXTrapDoor.transform;
            EXTrapDoorPit.transform.parent = EXTrapDoor.transform;

            ItemBuilder.AddSpriteToObject(EXTrapDoor.name, (m_TrapDoorBasePath + m_TrapDoorSprites[0]), EXTrapDoor, false);
            ItemBuilder.AddSpriteToObject(EXTrapDoorBorder.name, (m_TrapDoorBasePath + "RR_mine_lair_floor_001"), EXTrapDoorBorder, false);
            ItemBuilder.AddSpriteToObject(EXTrapDoorPit.name, (m_TrapDoorBasePath + "RR_mine_lair_floor_pit_001"), EXTrapDoorPit, false);

            tk2dSprite m_TrapDoorBorderSprite = EXTrapDoorBorder.GetComponent<tk2dSprite>();
            m_TrapDoorBorderSprite.HeightOffGround = -1f;
            m_TrapDoorBorderSprite.UpdateZDepth();
            tk2dSprite m_EXTrapDoorPitSprite = EXTrapDoorPit.GetComponent<tk2dSprite>();
            m_EXTrapDoorPitSprite.HeightOffGround = -2f;
            m_EXTrapDoorPitSprite.UpdateZDepth();
            

            tk2dSprite m_TrapDoorSprite = EXTrapDoor.GetComponent<tk2dSprite>();
            m_TrapDoorSprite.HeightOffGround = -2f;
            m_TrapDoorSprite.UpdateZDepth();
            
            foreach (string spriteName in m_TrapDoorSprites) {
                SpriteBuilder.AddSpriteToCollection((m_TrapDoorBasePath + spriteName), m_TrapDoorSprite.Collection);
            }
            
            ExpandUtility.GenerateSpriteAnimator(EXTrapDoor, ClipFps: 8);
            tk2dSpriteAnimator m_TrapDoorAnimator = EXTrapDoor.GetComponent<tk2dSpriteAnimator>();
            ExpandUtility.AddAnimation(m_TrapDoorAnimator, m_TrapDoorSprite.Collection, m_TrapDoorSprites, "trapdoor_open", frameRate: 8);

            ExpandGlitchTrapDoor m_TrapDoorComponent = EXTrapDoor.AddComponent<ExpandGlitchTrapDoor>();
            m_TrapDoorComponent.PitBorderObject = EXTrapDoorBorder;
            m_TrapDoorComponent.PitObject = EXTrapDoorPit;

            ExpandUtility.GenerateOrAddToRigidBody(EXTrapDoor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 16));

            Vector3 LockPositionOffset = (EXTrapDoor.transform.position + new Vector3(0.2f, 1f));

            GameObject m_LockObject = Instantiate(RatJailDoorPlacable.gameObject.GetComponent<InteractableDoorController>().WorldLocks[0].gameObject, LockPositionOffset, Quaternion.identity);
            m_LockObject.SetActive(false);
            m_LockObject.GetComponent<InteractableLock>().lockMode = InteractableLock.InteractableLockMode.RESOURCEFUL_RAT;
            m_TrapDoorComponent.Lock = m_LockObject.GetComponent<InteractableLock>();            
            m_LockObject.transform.parent = EXTrapDoor.transform;
            m_LockObject.transform.localPosition += new Vector3(0, 0, 0.812f);
            DontDestroyOnLoad(EXTrapDoor);*/

            EXPlayerMimicBoss = expandSharedAssets1.LoadAsset<GameObject>("Expand Gungeoneer Mimic Boss Placable");
            // EXPlayerMimicBoss.SetActive(false);
            EXPlayerMimicBoss.AddComponent<ExpandGungeoneerMimicBossPlacable>();
            /*FakePrefab.MarkAsFakePrefab(EXPlayerMimicBoss);
            DontDestroyOnLoad(EXPlayerMimicBoss);*/

            EXSawBladeTrap_4x4Zone = expandSharedAssets1.LoadAsset<GameObject>("EX SawBlade PlacableObject");
            // EXSawBladeTrap_4x4Zone.SetActive(false);
            EXSawBladeTrap_4x4Zone.AddComponent<ExpandSawBladeTrapPlaceable>();
            /*FakePrefab.MarkAsFakePrefab(EXSawBladeTrap_4x4Zone);
            DontDestroyOnLoad(EXSawBladeTrap_4x4Zone);*/

            ExpandBootlegRoomPlaceable.BuildPrefab(expandSharedAssets1);
            
            
            CorruptedRewardPedestal = Instantiate(RewardPedestalPrefab);
            CorruptedRewardPedestal.SetActive(false);
            CorruptedRewardPedestal.name = "Corrupted Reward Pedestal";
            RewardPedestal m_CorruptedPedestal = CorruptedRewardPedestal.GetComponent<RewardPedestal>();
            m_CorruptedPedestal.SpecificItemId = Game.Items.Get("ex:corruption_bomb").PickupObjectId;
            m_CorruptedPedestal.SpawnsTertiarySet = false;
            m_CorruptedPedestal.UsesSpecificItem = true;
            m_CorruptedPedestal.overrideMimicChance = 0f;
            DontDestroyOnLoad(CorruptedRewardPedestal);
            FakePrefab.MarkAsFakePrefab(CorruptedRewardPedestal);


            GameObject m_RedChestReference = sharedAssets2.LoadAsset<GameObject>("HighDragunfire_Chest_Red");

            RickRollChestObject = expandSharedAssets1.LoadAsset<GameObject>("Expand_RickRollChest");
            if (m_RedChestReference.transform.Find("Shadow").gameObject) {
                GameObject RickRollChestShadow = expandSharedAssets1.LoadAsset<GameObject>("Expand_RickRollChestShadow");
                RickRollChestShadow.transform.SetParent(RickRollChestObject.transform);
                RickRollChestShadow.layer = m_RedChestReference.transform.Find("Shadow").gameObject.layer;
                tk2dSprite RickRollChestShadowSprite = RickRollChestShadow.AddComponent<tk2dSprite>();
                ExpandUtility.DuplicateSprite(RickRollChestShadowSprite, m_RedChestReference.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>());
                RickRollChestShadow.transform.localPosition = m_RedChestReference.transform.Find("Shadow").gameObject.transform.localPosition;
            }
            
            
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
            RickRollChestBreakable.overrideSpriteNameToUseAtZeroHP = "chest_redgold_break_001";
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
            ItemBuilder.AddSpriteToObject(RickRollAnimationObject, expandSharedAssets1.LoadAsset<Texture2D>("RickRoll_RiseUp_01"), false, false);

            tk2dBaseSprite m_RickRollBaseSprite = RickRollAnimationObject.GetComponent<tk2dBaseSprite>();

            foreach (string spriteName in m_RickRollRiseFrames) {
                if (spriteName != "RickRoll_RiseUp_01") {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_RickRollBaseSprite.Collection);
                }
            }
            foreach (string spriteName in m_RickRollFrames) {
                SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_RickRollBaseSprite.Collection);
            }

            ExpandUtility.GenerateSpriteAnimator(RickRollAnimationObject, DefaultClipId: 0);
            ExpandUtility.AddAnimation(RickRollAnimationObject.GetComponent<tk2dSpriteAnimator>(), m_RickRollBaseSprite.Collection, m_RickRollRiseFrames, "RickRollAnimation_Rise", tk2dSpriteAnimationClip.WrapMode.Once, frameRate: 12);
            ExpandUtility.AddAnimation(RickRollAnimationObject.GetComponent<tk2dSpriteAnimator>(), m_RickRollBaseSprite.Collection, m_RickRollFrames, "RickRollAnimation", tk2dSpriteAnimationClip.WrapMode.Loop, frameRate: 12);

            ExpandRickRollChest RickRollChestComponent = RickRollChestObject.AddComponent<ExpandRickRollChest>();
            RickRollChestComponent.RickRollAnimationObject = RickRollAnimationObject;
            RickRollChestComponent.MinimapIconPrefab = m_RedChestReference.GetComponent<Chest>().MinimapIconPrefab;
            RickRollChestComponent.breakAnimName = m_RedChestReference.GetComponent<Chest>().breakAnimName;
            RickRollChestComponent.openAnimName = m_RedChestReference.GetComponent<Chest>().openAnimName;

            RickRollMusicSwitchObject = expandSharedAssets1.LoadAsset<GameObject>("ExpandRickRoll_MusicSwitch");
            RickRollMusicSwitchObject.layer = LayerMask.NameToLayer("FG_Critical");
            ItemBuilder.AddSpriteToObject(RickRollMusicSwitchObject, expandSharedAssets1.LoadAsset<Texture2D>("music_switch_idle_on_001"), false, false);
            tk2dSprite RickRollSwitchSprite = RickRollMusicSwitchObject.GetComponent<tk2dSprite>();
            SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>("music_switch_idle_off_001"), RickRollSwitchSprite.Collection);

            foreach (string spriteName in m_RickRollMusicSwitchFrames) {
                SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), RickRollSwitchSprite.Collection);
            }

            ExpandUtility.GenerateSpriteAnimator(RickRollMusicSwitchObject, DefaultClipId: 0);
            ExpandUtility.AddAnimation(RickRollMusicSwitchObject.GetComponent<tk2dSpriteAnimator>(), RickRollSwitchSprite.Collection, m_RickRollMusicSwitchTurnOnFrames, "RickRollSwitch_TurnOn", tk2dSpriteAnimationClip.WrapMode.Once, frameRate: 12);
            ExpandUtility.AddAnimation(RickRollMusicSwitchObject.GetComponent<tk2dSpriteAnimator>(), RickRollSwitchSprite.Collection, m_RickRollMusicSwitchTurnOffFrames, "RickRollSwitch_TurnOff", tk2dSpriteAnimationClip.WrapMode.Once, frameRate: 12);

            ExpandRickRollChest RickRollChest_SwitchComponent = RickRollMusicSwitchObject.AddComponent<ExpandRickRollChest>();
            RickRollChest_SwitchComponent.isMusicSwitch = true;
            RickRollChest_SwitchComponent.switchOnAnimName = "RickRollSwitch_TurnOn";
            RickRollChest_SwitchComponent.switchOffAnimName = "RickRollSwitch_TurnOff";
            
            RoomBuilder.AddObjectToRoom(gungeon_entrance, new Vector2(12, 20), ExpandUtility.GenerateDungeonPlacable(RickRollMusicSwitchObject, useExternalPrefab: true), xOffset: 12, yOffset: 6);
            RoomBuilder.AddObjectToRoom(gungeon_entrance_bossrush, new Vector2(12, 20), ExpandUtility.GenerateDungeonPlacable(RickRollMusicSwitchObject, useExternalPrefab: true), xOffset: 12, yOffset: 6);

            ExpandThunderstormPlaceable = expandSharedAssets1.LoadAsset<GameObject>("ExpandThunderStorm");
            ExpandThunderstormPlaceable.AddComponent<ExpandThunderStormPlacable>();


            Door_Horizontal_Jungle = Instantiate(ForgeDungeonPrefab.doorObjects.variantTiers[0].nonDatabasePlaceable);
            Door_Horizontal_Jungle.SetActive(false);
            Door_Vertical_Jungle = Instantiate(ForgeDungeonPrefab.doorObjects.variantTiers[1].nonDatabasePlaceable);
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


            Jungle_Doors = Instantiate(ForgeDungeonPrefab.doorObjects);
            Jungle_Doors.variantTiers[0].nonDatabasePlaceable = Door_Vertical_Jungle;
            Jungle_Doors.variantTiers[1].nonDatabasePlaceable = Door_Horizontal_Jungle;
            FakePrefab.MarkAsFakePrefab(Door_Horizontal_Jungle);
            FakePrefab.MarkAsFakePrefab(Door_Vertical_Jungle);
            DontDestroyOnLoad(Door_Horizontal_Jungle);
            DontDestroyOnLoad(Door_Vertical_Jungle);


            Jungle_LargeTree = expandSharedAssets1.LoadAsset<GameObject>("ExpandJungle_Tree");
            GameObject Jungle_Large_Tree_Shadow = Jungle_LargeTree.transform.Find("shadow").gameObject;
            ItemBuilder.AddSpriteToObject(Jungle_Large_Tree_Shadow, expandSharedAssets1.LoadAsset<Texture2D>("Jungle_Tree_Large_Shadow"), false, false);
            tk2dSprite TreeShadowSprite = Jungle_Large_Tree_Shadow.GetComponent<tk2dSprite>();
            TreeShadowSprite.usesOverrideMaterial = true;
            TreeShadowSprite.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;
            TreeShadowSprite.HeightOffGround = -18;

            ItemBuilder.AddSpriteToObject(Jungle_LargeTree, expandSharedAssets1.LoadAsset<Texture2D>("Jungle_Tree_Large"), false, false);

            SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>("Jungle_Tree_Large_Open"), Jungle_LargeTree.GetComponent<tk2dSprite>().Collection);

            tk2dSprite JungleTreeSprite = Jungle_LargeTree.GetComponent<tk2dSprite>();
            JungleTreeSprite.HeightOffGround = -8;
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(23, 20), offset: new IntVector2(84, 39)); // EntranceBlocker
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(10, 20), offset: new IntVector2(74, 39)); // SideCollisions
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 20), offset: new IntVector2(107, 39)); // SideCollisions
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(41, 64), offset: new IntVector2(74, 59)); // Top Collision

            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(41, 64), offset: new IntVector2(74, 59)); // High Obstacle (For projectiles mostly)
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(41, 75), offset: new IntVector2(74, 48)); // Enemy Blocker. (Prevents enemies from being siide collision area)

            ExpandJungleTreeController JungleTreeController = Jungle_LargeTree.AddComponent<ExpandJungleTreeController>();
            
            Jungle_LargeTreeTopFrame = expandSharedAssets1.LoadAsset<GameObject>("Jungle Tree Frame");
            ItemBuilder.AddSpriteToObject(Jungle_LargeTreeTopFrame, expandSharedAssets1.LoadAsset<Texture2D>("Jungle_Tree_Large_Frame"), false, false);
            Jungle_LargeTreeTopFrame.GetComponent<tk2dSprite>().HeightOffGround = 3;
            JungleTreeController.JungleTreeTopFrame = Jungle_LargeTreeTopFrame;

            EXJungleTree_MinimapIcon = expandSharedAssets1.LoadAsset<GameObject>("EXJungleTree_MinimapIcon");
            ItemBuilder.AddSpriteToObject(EXJungleTree_MinimapIcon, expandSharedAssets1.LoadAsset<Texture2D>("JungleTree_MinimapIcon"), false, false);

            EXJungleCrest_MinimapIcon = expandSharedAssets1.LoadAsset<GameObject>("EXJungleCrest_MinimapIcon");
            ItemBuilder.AddSpriteToObject(EXJungleCrest_MinimapIcon, expandSharedAssets1.LoadAsset<Texture2D>("junglecrest_minimapicon"), false, false);
            
            Jungle_ExitLadder = expandSharedAssets1.LoadAsset<GameObject>("ExpandJungle_ExitLadder");
            ItemBuilder.AddSpriteToObject(Jungle_ExitLadder, expandSharedAssets1.LoadAsset<Texture2D>("Jungle_ExitLadder"), false, false);
            Jungle_ExitLadder.AddComponent<ExpandJungleExitLadderComponent>();
            
            Jungle_BlobLostSign = expandSharedAssets1.LoadAsset<GameObject>("Expand_JungleSign");
            ExpandUtility.BuildNewCustomSign(Jungle_BlobLostSign, Teleporter_Info_Sign, "Lost Blob Note", "This poor fella got lost on his way home.");
            
            Jungle_ItemStump = expandSharedAssets1.LoadAsset<GameObject>("ExpandJungle_ItemStump");
            ItemBuilder.AddSpriteToObject(Jungle_ItemStump, expandSharedAssets1.LoadAsset<Texture2D>("Jungle_TreeStump"), false, false);
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_ItemStump, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(3, 2), dimensions: new IntVector2(26, 24));
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_ItemStump, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(3, 2), dimensions: new IntVector2(26, 24));
            ExpandJungleTreeStumpItemPedestal StumpPedestal = Jungle_ItemStump.AddComponent<ExpandJungleTreeStumpItemPedestal>();
            StumpPedestal.ItemID = WoodenCrest.WoodCrestID;
            

            Door_Horizontal_Belly = Instantiate(NakatomiDungeonPrefab.doorObjects.variantTiers[0].nonDatabasePlaceable);
            Door_Vertical_Belly = Instantiate(NakatomiDungeonPrefab.doorObjects.variantTiers[1].nonDatabasePlaceable);

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
            ItemBuilder.AddSpriteToObject(Belly_Shipwreck_Left, expandSharedAssets1.LoadAsset<Texture2D>("Shipwreck_Left"), false, false);
            ItemBuilder.AddSpriteToObject(Belly_Shipwreck_Right, expandSharedAssets1.LoadAsset<Texture2D>("Shipwreck_Right"), false, false);
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
            Destroy(Door_Horizontal_Belly.gameObject.transform.Find("BarsLeft").gameObject.GetComponent<tk2dSpriteAnimator>());
            Destroy(Door_Horizontal_Belly.gameObject.transform.Find("BarsRight").gameObject.GetComponent<tk2dSpriteAnimator>());
            
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

            Belly_Doors = Instantiate(NakatomiDungeonPrefab.doorObjects);
            Belly_Doors.variantTiers[0].nonDatabasePlaceable = Door_Vertical_Belly;
            Belly_Doors.variantTiers[1].nonDatabasePlaceable = Door_Horizontal_Belly;
            FakePrefab.MarkAsFakePrefab(Door_Vertical_Belly);
            FakePrefab.MarkAsFakePrefab(Door_Horizontal_Belly);
            DontDestroyOnLoad(Door_Vertical_Belly);
            DontDestroyOnLoad(Door_Horizontal_Belly);


            West_PuzzleSetupPlacable = expandSharedAssets1.LoadAsset<GameObject>("EXWest_PuzzleSetupPlacable");
            West_PuzzleSetupPlacable.AddComponent<ExpandWestPuzzleRoomController>();


            Door_Horizontal_West = Instantiate(NakatomiDungeonPrefab.doorObjects.variantTiers[0].nonDatabasePlaceable);
            Door_Vertical_West = Instantiate(NakatomiDungeonPrefab.doorObjects.variantTiers[1].nonDatabasePlaceable);

            DungeonDoorController Door_Horizontal_West_Controller = Door_Horizontal_West.GetComponent<DungeonDoorController>();
            typeof(DungeonDoorController).GetField("doorClosesAfterEveryOpen", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Door_Horizontal_West_Controller, true);
            Door_Horizontal_West_Controller.sealAnimationName = "west_blocker_horizontal_down";
            Door_Horizontal_West_Controller.unsealAnimationName = "west_blocker_horizontal_up";
            Door_Horizontal_West_Controller.playerNearSealedAnimationName = string.Empty;
            // Door_Horizontal_West_Controller.hideSealAnimators = true;
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
            // Door_Horizontal_West_Controller.gameObject.transform.Find("AO_Floor_Left").localPosition = new Vector3(-0.625f, 1.0625f, 2.0625f);
            // Door_Horizontal_West_Controller.gameObject.transform.Find("AO_Floor_Right").localPosition = new Vector3(0.5625f, 1.0625f, 2.0625f);
            Door_Horizontal_West_Controller.gameObject.transform.Find("AO_Floor_Left").localPosition = new Vector3(-0.755f, 1.0625f, 2.0625f);
            Door_Horizontal_West_Controller.gameObject.transform.Find("AO_Floor_Right").localPosition = new Vector3(0.655f, 1.0625f, 2.0625f);


            Door_Horizontal_West_Controller.gameObject.transform.Find("DoorTop").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("west_door_horizontal_top_001");
            Door_Horizontal_West_Controller.gameObject.transform.Find("DoorBottom").gameObject.GetComponent<tk2dSprite>().sprite.SetSprite("west_door_horizontal_bottom_001");
            Destroy(Door_Horizontal_West.gameObject.transform.Find("BarsLeft").gameObject.GetComponent<tk2dSpriteAnimator>());
            Destroy(Door_Horizontal_West.gameObject.transform.Find("BarsRight").gameObject.GetComponent<tk2dSpriteAnimator>());

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
            

            West_Doors = Instantiate(NakatomiDungeonPrefab.doorObjects);
            West_Doors.variantTiers[0].nonDatabasePlaceable = Door_Vertical_West;
            West_Doors.variantTiers[1].nonDatabasePlaceable = Door_Horizontal_West;
            FakePrefab.MarkAsFakePrefab(Door_Vertical_West);
            FakePrefab.MarkAsFakePrefab(Door_Horizontal_West);
            DontDestroyOnLoad(Door_Vertical_West);
            DontDestroyOnLoad(Door_Horizontal_West);

            
            // Sarcophagus Objects have unused sprites still in the game. I'll set them up to use them for my Belly entrance room for Gungeon Proper.
            Sarcophagus_ShotgunBook_Kaliber = Instantiate(sharedAssets.LoadAsset<GameObject>("Sarcophagus_ShotgunBook")); 
            Sarcophagus_ShotgunMace_Kaliber = Instantiate(sharedAssets.LoadAsset<GameObject>("Sarcophagus_ShotgunMace"));
            Sarcophagus_BulletSword_Kaliber = Instantiate(sharedAssets.LoadAsset<GameObject>("Sarcophagus_BulletSword"));
            Sarcophagus_BulletShield_Kaliber = Instantiate(sharedAssets.LoadAsset<GameObject>("Sarcophagus_BulletShield"));
            Sarcophagus_ShotgunBook_Kaliber.SetActive(false);
            Sarcophagus_ShotgunMace_Kaliber.SetActive(false);
            Sarcophagus_BulletSword_Kaliber.SetActive(false);
            Sarcophagus_BulletShield_Kaliber.SetActive(false);

            Sarcophagus_ShotgunBook_Kaliber.name = "Sarcophagus_ShotgunBook_Kaliber";
            Sarcophagus_ShotgunMace_Kaliber.name = "Sarcophagus_ShotgunMace_Kaliber";
            Sarcophagus_BulletSword_Kaliber.name = "Sarcophagus_BulletSword_Kaliber";
            Sarcophagus_BulletShield_Kaliber.name = "Sarcophagus_BulletShield_Kaliber";

            Destroy(Sarcophagus_BulletShield_Kaliber.transform.Find("sarcophashadow (1)").gameObject);
            Destroy(Sarcophagus_BulletSword_Kaliber.transform.Find("sarcophashadow (2)").gameObject);
            Destroy(Sarcophagus_ShotgunBook_Kaliber.transform.Find("sarcophashadow (3)").gameObject);
            Destroy(Sarcophagus_ShotgunMace_Kaliber.transform.Find("sarcophashadow (4)").gameObject);

            Sarcophagus_ShotgunBook_Kaliber.GetComponent<tk2dSprite>().SetSprite("sarco_shotbook_kaliber_001");
            Sarcophagus_ShotgunMace_Kaliber.GetComponent<tk2dSprite>().SetSprite("sarco_shotmace_kaliber_001");
            Sarcophagus_BulletSword_Kaliber.GetComponent<tk2dSprite>().SetSprite("sarco_bullsword_kaliber_001");
            Sarcophagus_BulletShield_Kaliber.GetComponent<tk2dSprite>().SetSprite("sarco_bullshield_kaliber_001");
            
            FakePrefab.MarkAsFakePrefab(Sarcophagus_ShotgunBook_Kaliber);
            FakePrefab.MarkAsFakePrefab(Sarcophagus_ShotgunMace_Kaliber);
            FakePrefab.MarkAsFakePrefab(Sarcophagus_BulletSword_Kaliber);
            FakePrefab.MarkAsFakePrefab(Sarcophagus_BulletShield_Kaliber);
            DontDestroyOnLoad(Sarcophagus_ShotgunBook_Kaliber);
            DontDestroyOnLoad(Sarcophagus_ShotgunMace_Kaliber);
            DontDestroyOnLoad(Sarcophagus_BulletSword_Kaliber);
            DontDestroyOnLoad(Sarcophagus_BulletShield_Kaliber);


            Sarco_WoodShieldPedestal = expandSharedAssets1.LoadAsset<GameObject>("ExpandSarco_Pedestal");
            ItemBuilder.AddSpriteToObject(Sarco_WoodShieldPedestal, expandSharedAssets1.LoadAsset<Texture2D>("PedestalRuins"), false, false);

            SpeculativeRigidbody Sarco_WoodShieldPedestalRigidBody = ExpandUtility.GenerateOrAddToRigidBody(Sarco_WoodShieldPedestal, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 3), dimensions: new IntVector2(26, 23));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_WoodShieldPedestal,  CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(0, 3), dimensions: new IntVector2(26, 23));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_WoodShieldPedestal, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(1, 5), dimensions: new IntVector2(24, 21));

            ExpandBellyWoodCrestEntranceController WoodCrestController = Sarco_WoodShieldPedestal.AddComponent<ExpandBellyWoodCrestEntranceController>();
            WoodCrestController.ItemID = WoodenCrest.WoodCrestID;

            Sarco_Door = expandSharedAssets1.LoadAsset<GameObject>("ExpandSarco_Door");
            Sarco_Door.layer = LayerMask.NameToLayer("FG_Critical");
            tk2dSprite Sarco_DoorSprite = Sarco_Door.AddComponent<tk2dSprite>();
            Sarco_DoorSprite.Collection = sharedAssets.LoadAsset<GameObject>("Environment_Gungeon_Collection").GetComponent<tk2dSpriteCollectionData>();
            Sarco_DoorSprite.SetSprite("sarco_door_001");
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
            ItemBuilder.AddSpriteToObject(Sarco_Floor, expandSharedAssets1.LoadAsset<Texture2D>("Belly_GungeonMonsterRoomFloor"), false, false);
            Sarco_Floor.GetComponent<tk2dSprite>().HeightOffGround = -1.5f;
            Sarco_Floor.GetComponent<tk2dSprite>().UpdateZDepth();
                                    
            List<string> BellyMonsterSprites = new List<string>() {
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

            Sarco_MonsterObject = expandSharedAssets1.LoadAsset<GameObject>("ExpandSarco_Monster");
                        
            ItemBuilder.AddSpriteToObject(Sarco_MonsterObject, expandSharedAssets1.LoadAsset<Texture2D>("Belly_Monster_Move_001"), false, false);

            tk2dSprite Sarco_MonsterSprite = Sarco_MonsterObject.GetComponent<tk2dSprite>();

            SpriteBuilder.AddSpritesToCollection(expandSharedAssets1, BellyMonsterSprites, Sarco_MonsterSprite.Collection);

            ExpandUtility.GenerateSpriteAnimator(Sarco_MonsterObject);
            ExpandUtility.AddAnimation(Sarco_MonsterObject.GetComponent<tk2dSpriteAnimator>(), Sarco_MonsterSprite.Collection, BellyMonsterAnimationFrames, "MonsterChase", tk2dSpriteAnimationClip.WrapMode.Loop, 10);

            ExpandUtility.GenerateOrAddToRigidBody(Sarco_MonsterObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(57, 0), dimensions: new IntVector2(243, 1024));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_MonsterObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(57, 0), dimensions: new IntVector2(243, 1024));
            ExpandUtility.GenerateOrAddToRigidBody(Sarco_MonsterObject, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(57, 0), dimensions: new IntVector2(243, 1024));

            Sarco_MonsterObject.GetComponent<tk2dSpriteAnimator>().Library.clips[0].frames[6].eventInfo = "slam";
            Sarco_MonsterObject.GetComponent<tk2dSpriteAnimator>().Library.clips[0].frames[6].triggerEvent = true;

            // Sarco_MonsterObject.SetActive(false);

            ExpandBellyMonsterController BellyMonster = Sarco_MonsterObject.AddComponent<ExpandBellyMonsterController>();
            BellyMonster.ImpactVFXObjects = new GameObject[] {
                sharedAssets.LoadAsset<GameObject>("VFX_Dust_Explosion"),
                sharedAssets.LoadAsset<GameObject>("VFX_Tombstone_Impact"),
                sharedAssets.LoadAsset<GameObject>("VFX_Big_Dust_Poof")
            };

            // FakePrefab.MarkAsFakePrefab(Sarco_MonsterObject);
            // DontDestroyOnLoad(Sarco_MonsterObject);

            Sarco_Skeleton = expandSharedAssets1.LoadAsset<GameObject>("ExpandDeadSkeleton");
            Sarco_Skeleton.AddComponent<tk2dSprite>();
            tk2dSprite SarcoSkeletonSprite = Sarco_Skeleton.GetComponent<tk2dSprite>();
            SarcoSkeletonSprite.Collection = sharedAssets.LoadAsset<GameObject>("EnvironmentCollection 4").GetComponent<tk2dSpriteCollectionData>();
            SarcoSkeletonSprite.SetSprite("skeleton_floor_001");
            SarcoSkeletonSprite.HeightOffGround = -2f;
            SarcoSkeletonSprite.UpdateZDepth();


            /*Sarco_Skeleton.SetActive(false);
            FakePrefab.MarkAsFakePrefab(Sarco_Skeleton);
            DontDestroyOnLoad(Sarco_Skeleton);*/


            List<Color> m_ColorBytes = new List<Color>();

            int Length = (32 * 64);

            for (int i = 0; i < Length; i++) { m_ColorBytes.Add(new Color(0, 0, 0, 1)); }

            Color[] m_ColorBytesArray = m_ColorBytes.ToArray();

            Texture2D Voidtexture = new Texture2D(32, 64, TextureFormat.RGBA32, false);

            Voidtexture.SetPixels(m_ColorBytesArray);
            Voidtexture.Apply();

            Belly_ExitWarp = expandSharedAssets1.LoadAsset<GameObject>("ExpandBelly_ExitWarp");
            Belly_ExitWarp.layer = LayerMask.NameToLayer("FG_Critical");
            // Belly_ExitWarp.SetActive(false);
            ItemBuilder.AddSpriteToObject(Belly_ExitWarp, Voidtexture, false, false);
            ExpandUtility.GenerateOrAddToRigidBody(Belly_ExitWarp, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(2, 2));
            ExpandBellyWarpWingDoor Belly_ExitWarpController = Belly_ExitWarp.AddComponent<ExpandBellyWarpWingDoor>();
            Belly_ExitWarpController.IsBellyExitDoor = true;

            /*FakePrefab.MarkAsFakePrefab(Belly_ExitWarp);
            DontDestroyOnLoad(Belly_ExitWarp);*/

            Belly_ExitRoomIcon = expandSharedAssets1.LoadAsset<GameObject>("BellyExitRoomIcon");
            ItemBuilder.AddSpriteToObject(Belly_ExitRoomIcon, expandSharedAssets1.LoadAsset<Texture2D>("Belly_ExitRoomIcon"), false, false);


            JungleLight = Instantiate(sharedAssets.LoadAsset<GameObject>("Castle Light"));
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
            DontDestroyOnLoad(JungleLight);

            BellyLight = Instantiate(ratDungeon.roomMaterialDefinitions[0].lightPrefabs.elements[0].rawGameObject);
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
            DontDestroyOnLoad(BellyLight);
            

            WestLight = Instantiate(ratDungeon.roomMaterialDefinitions[0].lightPrefabs.elements[0].rawGameObject);
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
            DontDestroyOnLoad(WestLight);


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
            // ExpandUtility.GenerateOrAddToRigidBody(Cactus_A, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(1, -3), dimensions: new IntVector2(6, 6));

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
            // ExpandUtility.GenerateOrAddToRigidBody(Cactus_B, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, offset: new IntVector2(2, -3), dimensions: new IntVector2(10, 5));

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

            ExpandRewardPedestal BlankPedestal = BlankRewardPedestal.AddComponent<ExpandRewardPedestal>();
            BlankPedestal.spawnTransform = BlankRewardPedestal.transform.Find("Reward_Spawn");

            ExpandRewardPedestal ratKeyPedestal = RatKeyRewardPedestal.AddComponent<ExpandRewardPedestal>();
            ratKeyPedestal.spawnTransform = RatKeyRewardPedestal.transform.Find("Reward_Spawn");
            ratKeyPedestal.ItemID = 727;


            EXSpaceFloor_50x50 = expandSharedAssets1.LoadAsset<GameObject>("EXSpaceFloor_50x50");
            EXSpaceFloorPitBorder_50x50 = expandSharedAssets1.LoadAsset<GameObject>("EXSpaceFloorPitBorder_50x50");

            ItemBuilder.AddSpriteToObject(EXSpaceFloor_50x50, expandSharedAssets1.LoadAsset<Texture2D>("RainbowRoad"), false, false);
            ItemBuilder.AddSpriteToObject(EXSpaceFloorPitBorder_50x50, expandSharedAssets1.LoadAsset<Texture2D>("RainbowRoad_PitBorders"), false, false);
            EXSpaceFloor_50x50.GetComponent<tk2dSprite>().HeightOffGround = -200;
            EXSpaceFloor_50x50.GetComponent<tk2dSprite>().renderer.material = new Material(ShaderCache.Acquire("Brave/Internal/RainbowChestShader"));
            //EXSpaceFloor_50x50.GetComponent<tk2dSprite>().renderer.material = new Material(braveResources.LoadAsset<Shader>("finalgunroom_bg_02"));
            EXSpaceFloor_50x50.GetComponent<tk2dSprite>().renderer.material.mainTexture = expandSharedAssets1.LoadAsset<Texture2D>("RainbowRoad");
            EXSpaceFloor_50x50.GetComponent<tk2dSprite>().usesOverrideMaterial = true;            
            EXSpaceFloor_50x50.AddComponent<ExpandEnableSpacePitOnEnterComponent>();
            EXSpaceFloorPitBorder_50x50.GetComponent<tk2dSprite>().HeightOffGround = -195;

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
            Destroy(Challenge_BlobulinAmmo.GetComponent<BlobulinAmmoChallengeModifier>());
            


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
            Destroy(Challenge_BooRoom.GetComponent<BooRoomChallengeModifier>());

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
            Destroy(Challenge_ZoneControl.GetComponent<ZoneControlChallengeModifier>());

            challengeManager.PossibleChallenges[21].challenge = Challenge_ZoneControl.GetComponent<ExpandZoneControlChallengeComponent>();
            challengeMegaManager.PossibleChallenges[21].challenge = Challenge_ZoneControl.GetComponent<ExpandZoneControlChallengeComponent>();

            //  Challenge_ChaosMode = new GameObject("Challenge_ChaosMode") { layer = 0 };
            Challenge_ChaosMode = expandSharedAssets1.LoadAsset<GameObject>("ExpandChallenge_ChaosMode");
            // Challenge_ChaosMode.name = "Challenge_ChaosMode";
            Challenge_ChaosMode.AddComponent<ExpandChaosChallengeComponent>();
            // DontDestroyOnLoad(Challenge_ChaosMode);
            
            
            ExpandChaosChallengeComponent expandChaosChallenge = Challenge_ChaosMode.GetComponent<ExpandChaosChallengeComponent>();
            expandChaosChallenge.MutuallyExclusive = new List<ChallengeModifier>() {
                challengeMegaManager.PossibleChallenges[3].challenge,
                challengeMegaManager.PossibleChallenges[4].challenge,
                challengeMegaManager.PossibleChallenges[10].challenge
            };

            // Clear challenges to test just one.
            // challengeManager.PossibleChallenges.Clear();
            // challengeMegaManager.PossibleChallenges.Clear();

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


            // For testing            
            /*ChallengeDataEntry testEntry = challengeManager.PossibleChallenges[21];
            challengeManager.PossibleChallenges.Clear();
            challengeManager.PossibleChallenges.Add(testEntry);*/

            // Challenge_TripleTrouble = PickupObjectDatabase.GetById(754).gameObject;
            // Challenge_TripleTrouble = new GameObject("Challenge_TripleTrouble") { layer = 0 };
            Challenge_TripleTrouble = expandSharedAssets1.LoadAsset<GameObject>("ExpandChallenge_TripleTrouble");
            Challenge_TripleTrouble.AddComponent<ExpandTripleTroubleChallengeComponent>();            
            // DontDestroyOnLoad(Challenge_TripleTrouble);

            // Challenge_KingsMen = new GameObject("Challenge_KingsMen") { layer = 0 };
            Challenge_KingsMen = expandSharedAssets1.LoadAsset<GameObject>("ExpandChallenge_KingsMen");
            Challenge_KingsMen.AddComponent<ExpandBulletKingChallenge>();
            // DontDestroyOnLoad(Challenge_KingsMen);

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

            ChamberGun = PickupObjectDatabase.GetById(647) as Gun;

            if (ChamberGun.gameObject.GetComponent<ChamberGunProcessor>()) {
                Destroy(ChamberGun.gameObject.GetComponent<ChamberGunProcessor>());
                ChamberGun.gameObject.AddComponent<ExpandChamberGunProcessor>();
            }


            EXGlitchFloorScreenFX = expandSharedAssets1.LoadAsset<GameObject>("EXGlitchFloorScreenFX");
            EXGlitchFloorScreenFX.AddComponent<ExpandGlitchScreenFXController>();

            MetalGearRatPrefab = null;
            MetalGearRatActorPrefab = null;
            ResourcefulRatBossPrefab = null;
            ResourcefulRatBossActorPrefab = null;
            MetalGearRatDeathPrefab = null;
            PunchoutPrefab = null;
            resourcefulRatControllerPrefab = null;
            FoldingTablePrefab = null;            
            m_gungeon_rewardroom_1 = null;
            objectDatabase = null;

            // Null any Dungeon prefabs you call up when done else you'll break level generation for that prefab on future level loads!
            SewerDungeonPrefab = null;
            MinesDungeonPrefab = null;
            CathedralDungeonPrefab = null;
            BulletHellDungeonPrefab = null;
            ForgeDungeonPrefab = null;
            CatacombsDungeonPrefab = null;
            NakatomiDungeonPrefab = null;
            ratDungeon = null;
        }
    }
}

