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

        private static AssetBundle sharedAssets;
        private static AssetBundle sharedAssets2;
        private static AssetBundle braveResources;
        private static AssetBundle enemiesBase;

        private static Dungeon TutorialDungeonPrefab;
        private static Dungeon SewerDungeonPrefab;
        private static Dungeon MinesDungeonPrefab;
        private static Dungeon ratDungeon;
        private static Dungeon CathedralDungeonPrefab;
        private static Dungeon BulletHellDungeonPrefab;
        private static Dungeon ForgeDungeonPrefab;
        private static Dungeon CatacombsDungeonPrefab;
        private static Dungeon NakatomiDungeonPrefab;

        private static Dungeon FinalScenarioBulletPrefab;
        private static Dungeon FinalScenarioPilotPrefab;

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

        public static WeightedRoom[] OfficeAndUnusedWeightedRooms;

        // Items
        public static PickupObject RatKeyItem;

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

        // Modified/Reference AIActors
        public static AIActor MetalCubeGuy;
        public static AIActor SerManuel;

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

        // Custom Challenge Modifiers
        public static GameObject Challenge_ChaosMode;
        public static GameObject Challenge_TripleTrouble;
        public static GameObject Challenge_KingsMen;

        public static void InitPrefabResources() {

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
            

            RatKeyItem = PickupObjectDatabase.GetById(727);

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
        }

        public static void InitCustomPrefabs() {

            InitPrefabResources();

            ExpandObjectDatabase objectDatabase = new ExpandObjectDatabase();

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


            RatKeyItem.RespawnsIfPitfall = true;
            
            ElevatorArrival.variantTiers.Add(ElevatorArrivalVarientForNakatomi);
            ElevatorDeparture.variantTiers.Add(ElevatorDepartureVarientForRatNakatomi);

            MetalCubeGuy.healthHaver.gameObject.AddComponent<ExpandExplodeOnDeath>();
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
            sharedAssets = null;
            sharedAssets2 = null;
            braveResources = null;
            enemiesBase = null;
        }

        public static void InitCanyonTileSet(Dungeon dungeon, GlobalDungeonData.ValidTilesets tilesetID) {
            /*braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");            
            tk2dTiledSprite grassStripTileSprite = braveResources.LoadAsset<GameObject>("TallGrassStrip").GetComponent<tk2dTiledSprite>();
            tk2dSpriteCollectionData jungleTileSet = grassStripTileSprite.Collection;*/
            MinesDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Mines");
            FinalScenarioPilotPrefab = DungeonDatabase.GetOrLoadByName("FinalScenario_Pilot");
            FinalScenarioBulletPrefab = DungeonDatabase.GetOrLoadByName("FinalScenario_Bullet");

            if (ENV_Tileset_Canyon == null) {
                ENV_Tileset_Canyon = ExpandUtility.BuildSpriteCollection(FinalScenarioPilotPrefab.tileIndices.dungeonCollection, ENV_Tileset_Canyon_Texture, null, null, true);
            }

            // SewerDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Sewer");
            // dungeon.decoSettings = FinalScenarioBulletPrefab.decoSettings;
            dungeon.decoSettings = new TilemapDecoSettings {
                standardRoomVisualSubtypes = new WeightedIntCollection {
                    elements = new WeightedInt[] {
                        MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[3],
                        MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[3],
                        // MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[0],
                        // MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[1],
                        MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[2], // shop visual type. Do not remove
                        MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[3],
                        // MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[4],
                        MinesDungeonPrefab.decoSettings.standardRoomVisualSubtypes.elements[3],
                    }
                },
                decalLayerStyle = MinesDungeonPrefab.decoSettings.decalLayerStyle,
                decalSize = MinesDungeonPrefab.decoSettings.decalSize,
                decalSpacing = MinesDungeonPrefab.decoSettings.decalSpacing,
                decalExpansion = MinesDungeonPrefab.decoSettings.decalExpansion,
                patternLayerStyle = MinesDungeonPrefab.decoSettings.patternLayerStyle,
                patternSize = MinesDungeonPrefab.decoSettings.patternSize,
                patternSpacing = MinesDungeonPrefab.decoSettings.patternSpacing,
                patternExpansion = MinesDungeonPrefab.decoSettings.patternExpansion,
                decoPatchFrequency = MinesDungeonPrefab.decoSettings.decoPatchFrequency,
                ambientLightColor = MinesDungeonPrefab.decoSettings.ambientLightColor,
                ambientLightColorTwo = MinesDungeonPrefab.decoSettings.ambientLightColorTwo,
                lowQualityAmbientLightColor = MinesDungeonPrefab.decoSettings.lowQualityAmbientLightColor,
                lowQualityAmbientLightColorTwo = MinesDungeonPrefab.decoSettings.lowQualityAmbientLightColorTwo,
                lowQualityCheapLightVector = MinesDungeonPrefab.decoSettings.lowQualityCheapLightVector,
                UsesAlienFXFloorColor = MinesDungeonPrefab.decoSettings.UsesAlienFXFloorColor,
                AlienFXFloorColor = MinesDungeonPrefab.decoSettings.AlienFXFloorColor,
                generateLights = MinesDungeonPrefab.decoSettings.generateLights,
                lightCullingPercentage = MinesDungeonPrefab.decoSettings.lightCullingPercentage,
                lightOverlapRadius = MinesDungeonPrefab.decoSettings.lightOverlapRadius,
                nearestAllowedLight = MinesDungeonPrefab.decoSettings.nearestAllowedLight,
                minLightExpanseWidth = MinesDungeonPrefab.decoSettings.minLightExpanseWidth,
                lightHeight = MinesDungeonPrefab.decoSettings.lightHeight,
                lightCookies = MinesDungeonPrefab.decoSettings.lightCookies,
                debug_view = false
            };
            dungeon.tileIndices = FinalScenarioBulletPrefab.tileIndices;
            dungeon.tileIndices.dungeonCollection = ENV_Tileset_Canyon;
            dungeon.roomMaterialDefinitions = new DungeonMaterial[] {
                MinesDungeonPrefab.roomMaterialDefinitions[0],
                MinesDungeonPrefab.roomMaterialDefinitions[1],
                MinesDungeonPrefab.roomMaterialDefinitions[2],
                MinesDungeonPrefab.roomMaterialDefinitions[3],
                MinesDungeonPrefab.roomMaterialDefinitions[4],
                MinesDungeonPrefab.roomMaterialDefinitions[5],
                MinesDungeonPrefab.roomMaterialDefinitions[6],
                MinesDungeonPrefab.roomMaterialDefinitions[7]
            };
            dungeon.pathGridDefinitions = MinesDungeonPrefab.pathGridDefinitions;            
            dungeon.doorObjects = MinesDungeonPrefab.doorObjects;
            dungeon.oneWayDoorObjects = MinesDungeonPrefab.oneWayDoorObjects;
            dungeon.oneWayDoorPressurePlate = MinesDungeonPrefab.oneWayDoorPressurePlate;
            dungeon.lockedDoorObjects = MinesDungeonPrefab.lockedDoorObjects;
            dungeon.PlayerLightColor = MinesDungeonPrefab.PlayerLightColor;
            dungeon.PlayerLightIntensity = MinesDungeonPrefab.PlayerLightIntensity;
            dungeon.PlayerLightRadius = MinesDungeonPrefab.PlayerLightRadius;
            dungeon.tileIndices.tilesetId = tilesetID;
            dungeon.stampData.stamps = new TileStampData[] {
                MinesDungeonPrefab.stampData.stamps[0],
                MinesDungeonPrefab.stampData.stamps[1],
                MinesDungeonPrefab.stampData.stamps[2],
                MinesDungeonPrefab.stampData.stamps[3],
                MinesDungeonPrefab.stampData.stamps[4],
                MinesDungeonPrefab.stampData.stamps[5],
                MinesDungeonPrefab.stampData.stamps[6],
                MinesDungeonPrefab.stampData.stamps[7],
                MinesDungeonPrefab.stampData.stamps[8],
                MinesDungeonPrefab.stampData.stamps[9],
                MinesDungeonPrefab.stampData.stamps[10],
                MinesDungeonPrefab.stampData.stamps[11]
            };
            /*dungeon.decoSettings.ambientLightColor = MinesDungeonPrefab.decoSettings.ambientLightColor;
            dungeon.decoSettings.ambientLightColorTwo = MinesDungeonPrefab.decoSettings.ambientLightColorTwo;
            dungeon.decoSettings.lowQualityAmbientLightColor = MinesDungeonPrefab.decoSettings.lowQualityAmbientLightColor;
            dungeon.decoSettings.lowQualityAmbientLightColorTwo = MinesDungeonPrefab.decoSettings.lowQualityAmbientLightColorTwo;
            dungeon.decoSettings.lowQualityCheapLightVector = MinesDungeonPrefab.decoSettings.lowQualityCheapLightVector;
            dungeon.decoSettings.lightCullingPercentage = MinesDungeonPrefab.decoSettings.lightCullingPercentage;
            dungeon.decoSettings.lightOverlapRadius = MinesDungeonPrefab.decoSettings.lightOverlapRadius;
            dungeon.decoSettings.nearestAllowedLight = MinesDungeonPrefab.decoSettings.nearestAllowedLight;
            dungeon.decoSettings.minLightExpanseWidth = MinesDungeonPrefab.decoSettings.minLightExpanseWidth;
            dungeon.decoSettings.lightHeight = MinesDungeonPrefab.decoSettings.lightHeight;
            dungeon.decoSettings.lightCookies = MinesDungeonPrefab.decoSettings.lightCookies;*/
            

            dungeon.PlayerLightColor = FinalScenarioBulletPrefab.PlayerLightColor;
            dungeon.PlayerLightIntensity = FinalScenarioBulletPrefab.PlayerLightIntensity;
            dungeon.PlayerLightRadius = FinalScenarioBulletPrefab.PlayerLightRadius;
            // FinalScenarioBulletPrefab = null;

            /*jungleIndices.dungeonCollection = jungleTileSet;
            
            dungeon.tileIndices = jungleIndices;
            dungeon.stampData = JungleStampData;
            dungeon.roomMaterialDefinitions = MinesDungeonPrefab.roomMaterialDefinitions;
            //dungeon.decoSettings = jungleDeco;
            dungeon.stampData = JungleStampData;
            dungeon.pathGridDefinitions = MinesDungeonPrefab.pathGridDefinitions;
            dungeon.PlayerLightColor = new Color { r = 1, g = 1, b = 1, a = 1 };
            dungeon.PlayerLightIntensity = 3;
            dungeon.PlayerLightRadius = 5;
            braveResources = null;*/
            FinalScenarioBulletPrefab = null;
            FinalScenarioPilotPrefab = null;
            MinesDungeonPrefab = null;
            // SewerDungeonPrefab = null;
        }
    }
}

