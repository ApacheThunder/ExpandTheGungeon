using Dungeonator;
using System;
using System.Collections.Generic;
using UnityEngine;
using Gungeon;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandDungeonFlows;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandPrefabs : MonoBehaviour {

        public static AssetBundle sharedAssets;
        public static AssetBundle sharedAssets2;
        public static AssetBundle braveResources;
        public static AssetBundle enemiesBase;

        private static Dungeon TutorialDungeonPrefab;
        private static Dungeon SewerDungeonPrefab;
        private static Dungeon MinesDungeonPrefab;
        private static Dungeon ratDungeon;
        private static Dungeon CathedralDungeonPrefab;
        private static Dungeon BulletHellDungeonPrefab;
        private static Dungeon ForgeDungeonPrefab;
        private static Dungeon CatacombsDungeonPrefab;
        private static Dungeon NakatomiDungeonPrefab;
        
        // Custom Textures
        public static Texture2D StoneCubeWestTexture;
        public static Texture2D ENV_Tileset_Canyon_Texture;
        public static Texture2D BulletManMonochromeTexture;
        public static Texture2D BulletManUpsideDownTexture;
        public static Texture2D RedBulletShotgunManTexture;
        public static Texture2D BlueBulletShotgunManTexture;
        public static Texture2D BulletManEyepatchTexture;

        
        // Custom Sprite Collections
        public static tk2dSpriteCollectionData StoneCubeCollection_West;
        public static tk2dSpriteCollectionData ENV_Tileset_Canyon;
        public static tk2dSpriteCollectionData BulletManMonochromeCollection;
        public static tk2dSpriteCollectionData BulletManUpsideDownCollection;


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


        public static WeightedRoom[] OfficeAndUnusedWeightedRooms;


        // Modified Loot tables
        public static GenericLootTable Shop_Key_Items_01;
        public static GenericLootTable BlackSmith_Items_01;
        public static GenericLootTable Shop_Truck_Items_01;

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

        // Modified/Reference AIActors
        public static AIActor MetalCubeGuy;
        public static AIActor SerManuel;

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
        public static GameObject EXTrapDoor;
        public static GameObject EXTrapDoorBorder;
        public static GameObject EXTrapDoorPit;
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
        public static GameObject Jungle_ExitLadder;

        // Custom Challenge Modifiers
        public static GameObject Challenge_ChaosMode;
        public static GameObject Challenge_TripleTrouble;
        public static GameObject Challenge_KingsMen;
        

        public static void InitCustomPrefabs() {

            sharedAssets = ResourceManager.LoadAssetBundle("shared_auto_001");
            sharedAssets2 = ResourceManager.LoadAssetBundle("shared_auto_002");
            braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");
            enemiesBase = ResourceManager.LoadAssetBundle("enemies_base_001");
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

            StoneCubeWestTexture = ExpandUtilities.ResourceExtractor.GetTextureFromResource("Textures\\Stone_Cube_Collection_West.png");

            ENV_Tileset_Canyon_Texture = ExpandUtilities.ResourceExtractor.GetTextureFromResource("Textures\\ENV_Tileset_Canyon.png");

            BulletManMonochromeTexture = ExpandUtilities.ResourceExtractor.GetTextureFromResource("Textures\\BulletMan_Monochrome.png");
            BulletManUpsideDownTexture = ExpandUtilities.ResourceExtractor.GetTextureFromResource("Textures\\BulletMan_UpsideDown.png");

            // BulletManMonochromeCollection = ExpandUtility.BuildSpriteCollection(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").sprite.Collection, BulletManMonochromeTexture, null, ShaderCache.Acquire("tk2d/BlendVertexColorUnlitTilted"), false);
            // BulletManUpsideDownCollection = ExpandUtility.BuildSpriteCollection(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").sprite.Collection, BulletManUpsideDownTexture, null, null, false);

            
            
            RedBulletShotgunManTexture = ExpandUtilities.ResourceExtractor.GetTextureFromResource("Textures\\RedBulletShotgunMan.png");
            BlueBulletShotgunManTexture = ExpandUtilities.ResourceExtractor.GetTextureFromResource("Textures\\BlueBulletShotgunMan.png");
            BulletManEyepatchTexture = ExpandUtilities.ResourceExtractor.GetTextureFromResource("Textures\\BulletManEyepatch.png");
            
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
            Arrival = new GameObject("Arrival");
            Arrival.transform.name = "Arrival";
            FakePrefab.MarkAsFakePrefab(Arrival);
            Arrival.SetActive(false);
            DontDestroyOnLoad(Arrival);

            // NPCBabyDragunChaos = Instantiate(sharedAssets2.LoadAsset<GameObject>("BabyDragunJail"));
            NPCBabyDragunChaos = new GameObject("Chaos Baby Dragun");
            NPCBabyDragunChaos.SetActive(false);
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
            DontDestroyOnLoad(NPCBabyDragunChaos);
            FakePrefab.MarkAsFakePrefab(NPCBabyDragunChaos);


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
                    nonDatabasePlaceable = Game.Items.Get("ex:corruption_bomb").gameObject,
                    enemyPlaceableGuid = string.Empty,
                    pickupObjectPlaceableId = -1,
                    forceBlackPhantom = false,
                    addDebrisObject = false,
                    prerequisites = new DungeonPrerequisite[0],
                    materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
                }
            };

            MetalCubeGuy = EnemyDatabase.GetOrLoadByGuid("ba928393c8ed47819c2c5f593100a5bc");
            // LeadCube = EnemyDatabase.GetOrLoadByGuid("33b212b856b74ff09252bf4f2e8b8c57");
            // WallMimic = EnemyDatabase.GetOrLoadByGuid("479556d05c7c44f3b6abb3b2067fc778");
            // Cucco = EnemyDatabase.GetOrLoadByGuid("7bd9c670f35b4b8d84280f52a5cc47f6");
            // Raccoon = EnemyDatabase.GetOrLoadByGuid("e9fa6544000942a79ad05b6e4afb62db");
            SerManuel = EnemyDatabase.GetOrLoadByGuid("fc809bd43a4d41738a62d7565456622c");

            // VeteranBulletKin = EnemyDatabase.GetOrLoadByGuid("70216cae6c1346309d86d4a0b4603045");
            // RedShotGunKin = EnemyDatabase.GetOrLoadByGuid("128db2f0781141bcb505d8f00f9e4d47");
            // BlueShotGunKin = EnemyDatabase.GetOrLoadByGuid("b54d89f9e802455cbb2b8a96a31e8259");

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
                RoomBuilder.GenerateRoomLayoutFromPNG(Hell_Hath_No_Joery_009, "BulletHell\\Hell_Hath_No_Joery_009_Layout.png");
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
                        
            RoomBuilder.GenerateRoomLayoutFromPNG(big_entrance, "Large_Elevator_Entrance_Layout.png");

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

            MetalCubeGuy.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
            MetalCubeGuy.IsHarmlessEnemy = true;
            ExpandExplodeOnDeath metalcubeguyExploder = MetalCubeGuy.healthHaver.gameObject.GetComponent<ExpandExplodeOnDeath>();
            metalcubeguyExploder.deathType = OnDeathBehavior.DeathType.Death;
            ZeldaChargeBehavior zeldaChargeComponent = MetalCubeGuy.behaviorSpeculator.AttackBehaviors[0] as ZeldaChargeBehavior;
            zeldaChargeComponent.primeAnim = null;
            MetalCubeGuy.gameObject.AddComponent<ExpandThwompManager>();

            // Destroy(WallMimic.GetComponent<WallMimicController>());
            // WallMimic.gameObject.AddComponent<ChaosWallMimicManager>();

            // CompanionController cuccoController = Cucco.gameObject.GetComponent<CompanionController>();
            // cuccoController.CanBePet = true;

            // A custom item mod now adds this functionality. To avoid possible issues I have disabled this.
            // Raccoon.behaviorSpeculator.OverrideBehaviors.Add(new ChaosRaccoonManager());


            // Test 

            /*AIActor m_HollowPoint = EnemyDatabase.GetOrLoadByGuid("4db03291a12144d69fe940d5a01de376"); // hollowpoint
            
            BodyPartController[] m_BodyParts = SpectreTest.gameObject.GetComponentsInChildren<BodyPartController>(true);
            
            if (m_BodyParts != null && m_BodyParts.Length > 0) {
                for (int i = 0; i < m_BodyParts.Length; i++) { Destroy(m_BodyParts[i].gameObject); }
            }

            if(SpectreTest.gameObject.transform.Find("eyes 1").gameObject) { Destroy(SpectreTest.gameObject.transform.Find("eyes 1").gameObject); }
            
            SpectreTest.gameObject.GetComponent<BehaviorSpeculator>().AttackBehaviors = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>().AttackBehaviors;
            SpectreTest.gameObject.GetComponent<BehaviorSpeculator>().MovementBehaviors = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>().MovementBehaviors;
            SpectreTest.gameObject.GetComponent<BehaviorSpeculator>().TargetBehaviors = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>().TargetBehaviors;
            SpectreTest.gameObject.GetComponent<BehaviorSpeculator>().InstantFirstTick = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>().InstantFirstTick;
            SpectreTest.gameObject.GetComponent<BehaviorSpeculator>().TickInterval = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>().TickInterval;
            SpectreTest.gameObject.GetComponent<BehaviorSpeculator>().PostAwakenDelay = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>().PostAwakenDelay;
            SpectreTest.gameObject.GetComponent<BehaviorSpeculator>().RemoveDelayOnReinforce = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>().RemoveDelayOnReinforce;
            SpectreTest.gameObject.GetComponent<BehaviorSpeculator>().OverrideStartingFacingDirection = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>().OverrideStartingFacingDirection;
            SpectreTest.gameObject.GetComponent<BehaviorSpeculator>().StartingFacingDirection = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>().StartingFacingDirection;
            SpectreTest.gameObject.GetComponent<BehaviorSpeculator>().SkipTimingDifferentiator = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>().SkipTimingDifferentiator;
                        
            FullInspector.ISerializedObject m_SourceBehaviorSpeculatorSeralized = m_HollowPoint.gameObject.GetComponent<BehaviorSpeculator>();
            FullInspector.ISerializedObject m_TargetBehaviorSpeculatorSeralized = SpectreTest.gameObject.GetComponent<BehaviorSpeculator>();
            m_TargetBehaviorSpeculatorSeralized.SerializedObjectReferences = m_SourceBehaviorSpeculatorSeralized.SerializedObjectReferences;
            m_TargetBehaviorSpeculatorSeralized.SerializedStateKeys = m_SourceBehaviorSpeculatorSeralized.SerializedStateKeys;
            m_TargetBehaviorSpeculatorSeralized.SerializedStateValues = m_SourceBehaviorSpeculatorSeralized.SerializedStateValues;*/

            List<AGDEnemyReplacementTier> ReplacementTiers = GameManager.Instance.EnemyReplacementTiers;

            if (ReplacementTiers != null && ReplacementTiers.Count > 0) {
                foreach (AGDEnemyReplacementTier replacementTier in ReplacementTiers) {
                    if (replacementTier.RoomCantContain == null) { replacementTier.RoomCantContain = new List<string>(); }
                    replacementTier.RoomCantContain.Add(MetalCubeGuy.EnemyGuid);
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

            StoneCubeCollection_West = ExpandUtility.BuildSpriteCollection(EnemyDatabase.GetOrLoadByGuid("ba928393c8ed47819c2c5f593100a5bc").sprite.Collection, StoneCubeWestTexture, null, null, false);
            // DontDestroyOnLoad(StoneCubeCollection_West);


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


            RoomCorruptionAmbience = new GameObject("RoomCorruptionAmbience_Placable") { layer = 0 };
            FakePrefab.MarkAsFakePrefab(RoomCorruptionAmbience);
            RoomCorruptionAmbience.SetActive(false);
            RoomCorruptionAmbience.AddComponent<ExpandCorruptedRoomAmbiencePlacable>();
            DontDestroyOnLoad(RoomCorruptionAmbience);

            EXAlarmMushroom = new GameObject("EX Alarm Mushroom");
            EXAlarmMushroom.SetActive(false);


            string m_AlarmMushRoomSprite_BasePath = "ExpandTheGungeon/Textures/Traps/alarm_mushroom/";

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

            ItemBuilder.AddSpriteToObject(EXAlarmMushroom.name, (m_AlarmMushRoomSprite_BasePath + m_AlarmMushroom_idleSprites[0]), EXAlarmMushroom, false);
            tk2dSprite m_AlarmMushroomSprite = EXAlarmMushroom.GetComponent<tk2dSprite>();

            foreach (string spriteName in m_AlarmMushroom_idleSprites) {
                SpriteBuilder.AddSpriteToCollection((m_AlarmMushRoomSprite_BasePath + spriteName), m_AlarmMushroomSprite.Collection);
            }
            foreach (string spriteName in m_AlarmMushroom_alarmSprites) {
                SpriteBuilder.AddSpriteToCollection((m_AlarmMushRoomSprite_BasePath + spriteName), m_AlarmMushroomSprite.Collection);
            }
            foreach (string spriteName in m_AlarmMushroom_breakSprites) {
                SpriteBuilder.AddSpriteToCollection((m_AlarmMushRoomSprite_BasePath + spriteName), m_AlarmMushroomSprite.Collection);
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

            FakePrefab.MarkAsFakePrefab(EXAlarmMushroom);
            DontDestroyOnLoad(EXAlarmMushroom);
            

            string m_TrapDoorBasePath = "ExpandTheGungeon/Textures/EXTrapDoor/";

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
            DontDestroyOnLoad(EXTrapDoor);

            EXPlayerMimicBoss = new GameObject("Expand Gungeoneer Mimic Boss Placable") { layer = 0 };
            EXPlayerMimicBoss.SetActive(false);
            EXPlayerMimicBoss.AddComponent<ExpandGungeoneerMimicBossPlacable>();
            FakePrefab.MarkAsFakePrefab(EXPlayerMimicBoss);
            DontDestroyOnLoad(EXPlayerMimicBoss);

            EXSawBladeTrap_4x4Zone = new GameObject("EX SawBlade PlacableObject") { layer = 22 };
            EXSawBladeTrap_4x4Zone.SetActive(false);
            EXSawBladeTrap_4x4Zone.AddComponent<ExpandSawBladeTrapPlaceable>();
            FakePrefab.MarkAsFakePrefab(EXSawBladeTrap_4x4Zone);
            DontDestroyOnLoad(EXSawBladeTrap_4x4Zone);

            ExpandBootlegRoomPlaceable.BuildPrefab();


            
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

            RickRollChestObject = new GameObject("Rick Roll Chest") { layer = 22 };
            if (m_RedChestReference.transform.Find("Shadow").gameObject) {
                GameObject RickRollChestShadow = Instantiate(m_RedChestReference.transform.Find("Shadow").gameObject);
                RickRollChestShadow.name = "ChestShadow";
                RickRollChestShadow.transform.position += new Vector3(0, 0.1f, 0);
                RickRollChestShadow.transform.parent = RickRollChestObject.transform;
                
            }

            RickRollChestObject.SetActive(false);

            
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
            
            string m_RickRollBasePath = "ExpandTheGungeon/Textures/RickRoll/";

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

            RickRollAnimationObject = new GameObject("Rick Roll Animation") { layer = 22 };
            RickRollAnimationObject.SetActive(false);
            ItemBuilder.AddSpriteToObject(RickRollAnimationObject, (m_RickRollBasePath + "RickRoll_RiseUp_01"), false, true);

            tk2dBaseSprite m_RickRollBaseSprite = RickRollAnimationObject.GetComponent<tk2dBaseSprite>();

            foreach (string spriteName in m_RickRollRiseFrames) {
                if (spriteName != "RickRoll_RiseUp_01") {
                    SpriteBuilder.AddSpriteToCollection((m_RickRollBasePath + spriteName), m_RickRollBaseSprite.Collection);
                }
            }
            foreach (string spriteName in m_RickRollFrames) {
                SpriteBuilder.AddSpriteToCollection((m_RickRollBasePath + spriteName), m_RickRollBaseSprite.Collection);
            }

            ExpandUtility.GenerateSpriteAnimator(RickRollAnimationObject, DefaultClipId: 0);
            ExpandUtility.AddAnimation(RickRollAnimationObject.GetComponent<tk2dSpriteAnimator>(), m_RickRollBaseSprite.Collection, m_RickRollRiseFrames, "RickRollAnimation_Rise", tk2dSpriteAnimationClip.WrapMode.Once, frameRate: 12);
            ExpandUtility.AddAnimation(RickRollAnimationObject.GetComponent<tk2dSpriteAnimator>(), m_RickRollBaseSprite.Collection, m_RickRollFrames, "RickRollAnimation", tk2dSpriteAnimationClip.WrapMode.Loop, frameRate: 12);

            ExpandRickRollChest RickRollChestComponent = RickRollChestObject.AddComponent<ExpandRickRollChest>();
            RickRollChestComponent.RickRollAnimationObject = RickRollAnimationObject;
            RickRollChestComponent.MinimapIconPrefab = m_RedChestReference.GetComponent<Chest>().MinimapIconPrefab;
            RickRollChestComponent.breakAnimName = m_RedChestReference.GetComponent<Chest>().breakAnimName;
            RickRollChestComponent.openAnimName = m_RedChestReference.GetComponent<Chest>().openAnimName;
            
            RickRollMusicSwitchObject = new GameObject("RickRoll Music Switch") { layer = LayerMask.NameToLayer("FG_Critical") };
            RickRollMusicSwitchObject.SetActive(false);
            ItemBuilder.AddSpriteToObject(RickRollMusicSwitchObject, (m_RickRollBasePath + "music_switch_idle_on_001"), false, true);
            tk2dSprite RickRollSwitchSprite = RickRollMusicSwitchObject.GetComponent<tk2dSprite>();
            SpriteBuilder.AddSpriteToCollection((m_RickRollBasePath + "music_switch_idle_off_001"), RickRollSwitchSprite.Collection);

            foreach (string spriteName in m_RickRollMusicSwitchFrames) {
                SpriteBuilder.AddSpriteToCollection((m_RickRollBasePath + spriteName), RickRollSwitchSprite.Collection);
            }

            ExpandUtility.GenerateSpriteAnimator(RickRollMusicSwitchObject, DefaultClipId: 0);
            ExpandUtility.AddAnimation(RickRollMusicSwitchObject.GetComponent<tk2dSpriteAnimator>(), RickRollSwitchSprite.Collection, m_RickRollMusicSwitchTurnOnFrames, "RickRollSwitch_TurnOn", tk2dSpriteAnimationClip.WrapMode.Once, frameRate: 12);
            ExpandUtility.AddAnimation(RickRollMusicSwitchObject.GetComponent<tk2dSpriteAnimator>(), RickRollSwitchSprite.Collection, m_RickRollMusicSwitchTurnOffFrames, "RickRollSwitch_TurnOff", tk2dSpriteAnimationClip.WrapMode.Once, frameRate: 12);

            ExpandRickRollChest RickRollChest_SwitchComponent = RickRollMusicSwitchObject.AddComponent<ExpandRickRollChest>();
            RickRollChest_SwitchComponent.isMusicSwitch = true;
            RickRollChest_SwitchComponent.switchOnAnimName = "RickRollSwitch_TurnOn";
            RickRollChest_SwitchComponent.switchOffAnimName = "RickRollSwitch_TurnOff";
            
            DontDestroyOnLoad(RickRollChestObject);
            DontDestroyOnLoad(RickRollAnimationObject);
            DontDestroyOnLoad(RickRollMusicSwitchObject);
            FakePrefab.MarkAsFakePrefab(RickRollChestObject);
            FakePrefab.MarkAsFakePrefab(RickRollAnimationObject);
            FakePrefab.MarkAsFakePrefab(RickRollMusicSwitchObject);

            RoomBuilder.AddObjectToRoom(gungeon_entrance, new Vector2(12, 20), ExpandUtility.GenerateDungeonPlacable(RickRollMusicSwitchObject, useExternalPrefab: true), xOffset: 12, yOffset: 6);
            RoomBuilder.AddObjectToRoom(gungeon_entrance_bossrush, new Vector2(12, 20), ExpandUtility.GenerateDungeonPlacable(RickRollMusicSwitchObject, useExternalPrefab: true), xOffset: 12, yOffset: 6);

            ExpandThunderstormPlaceable = new GameObject("ExpandThunderStorm", new Type[] { typeof(ExpandThunderStormPlacable) } ) { layer = 0 };
            ExpandThunderstormPlaceable.SetActive(false);
            DontDestroyOnLoad(ExpandThunderstormPlaceable);
            FakePrefab.MarkAsFakePrefab(ExpandThunderstormPlaceable);
            
                                    
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


            Jungle_LargeTree = new GameObject("Jungle Tree") { layer = 0 };
            GameObject Jungle_Large_Tree_Shadow = new GameObject("Jungle Tree Shadow") { layer = 0 };
            ItemBuilder.AddSpriteToObject(Jungle_Large_Tree_Shadow, "ExpandTheGungeon/Textures/JungleAssets/Jungle_Tree_Large_Shadow", false, false);
            tk2dSprite TreeShadowSprite = Jungle_Large_Tree_Shadow.GetComponent<tk2dSprite>();
            TreeShadowSprite.usesOverrideMaterial = true;
            TreeShadowSprite.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;
            TreeShadowSprite.HeightOffGround = -18;
            TreeShadowSprite.UpdateZDepth();
            Jungle_Large_Tree_Shadow.transform.parent = Jungle_LargeTree.transform;

            ItemBuilder.AddSpriteToObject(Jungle_LargeTree, "ExpandTheGungeon/Textures/JungleAssets/Jungle_Tree_Large", false, false);

            SpriteBuilder.AddSpriteToCollection("ExpandTheGungeon/Textures/JungleAssets/Jungle_Tree_Large_Open", Jungle_LargeTree.GetComponent<tk2dSprite>().Collection);

            tk2dSprite JungleTreeSprite = Jungle_LargeTree.GetComponent<tk2dSprite>();
            JungleTreeSprite.HeightOffGround = -8;
            JungleTreeSprite.UpdateZDepth();
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(23, 20), offset: new IntVector2(84, 39)); // EntranceBlocker
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(10, 20), offset: new IntVector2(74, 39)); // SideCollisions
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 20), offset: new IntVector2(107, 39)); // SideCollisions
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(41, 64), offset: new IntVector2(74, 59)); // Top Collision

            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(41, 64), offset: new IntVector2(74, 59)); // High Obstacle (For projectiles mostly)
            ExpandUtility.GenerateOrAddToRigidBody(Jungle_LargeTree, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(41, 75), offset: new IntVector2(74, 48)); // Enemy Blocker. (Prevents enemies from being siide collision area)

            

            Jungle_LargeTree.AddComponent<JungleTreeController>();

            FakePrefab.MarkAsFakePrefab(Jungle_LargeTree);
            DontDestroyOnLoad(Jungle_LargeTree);

            Jungle_ExitLadder = new GameObject("Jungle Exit Ladder") { layer = 0 };
            ItemBuilder.AddSpriteToObject(Jungle_ExitLadder, "ExpandTheGungeon/Textures/JungleAssets/Jungle_ExitLadder", false, false);
            Jungle_ExitLadder.AddComponent<ExpandJungleExitLadderComponent>();
            FakePrefab.MarkAsFakePrefab(Jungle_ExitLadder);
            DontDestroyOnLoad(Jungle_ExitLadder);
            


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
            Challenge_ChaosMode = braveResources.LoadAsset<GameObject>("TallGrassStrip");
            Challenge_ChaosMode.name = "Challenge_ChaosMode";            
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
                excludedTilesets = GlobalDungeonData.ValidTilesets.PHOBOSGEON,
                tilesetsWithCustomValues = new List<GlobalDungeonData.ValidTilesets>(0),
                CustomValues = new List<int>(0)
            });

            challengeMegaManager.PossibleChallenges.Add(new ChallengeDataEntry() {
                Annotation = "Apache Thunder's Chaos Mode in a room!",
                challenge = Challenge_ChaosMode.GetComponent<ExpandChaosChallengeComponent>(),                
                excludedTilesets = GlobalDungeonData.ValidTilesets.PHOBOSGEON,
                tilesetsWithCustomValues = new List<GlobalDungeonData.ValidTilesets>(0),
                CustomValues = new List<int>(0)
            });


            // For testing            
            /*ChallengeDataEntry testEntry = challengeManager.PossibleChallenges[21];
            challengeManager.PossibleChallenges.Clear();
            challengeManager.PossibleChallenges.Add(testEntry);*/

            // Challenge_TripleTrouble = PickupObjectDatabase.GetById(754).gameObject;
            Challenge_TripleTrouble = new GameObject("Challenge_TripleTrouble") { layer = 0 };            
            Challenge_TripleTrouble.AddComponent<ExpandTripleTroubleChallengeComponent>();            
            DontDestroyOnLoad(Challenge_TripleTrouble);

            Challenge_KingsMen = new GameObject("Challenge_KingsMen") { layer = 0 };
            Challenge_KingsMen.AddComponent<ExpandBulletKingChallenge>();
            DontDestroyOnLoad(Challenge_KingsMen);

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


            ExpandSecretDoorPrefabs.InitPrefabs();

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

