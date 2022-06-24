using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandPrefab {

    public static class ExpandLists {

        public static List<GameObject> CustomChests = new List<GameObject>();
        public static List<GameObject> CompanionItems = new List<GameObject>();
        public static Dictionary<string, List<string>> SpriteCollections;

        public static readonly List<string> EXFoyerCollection = new List<string> {
            "casino_hub_floor_001",
            "casino_hub_backwall_001",
            "casino_hub_border_001",
            "casino_carpet_001",
            "floortrigger_idle_01",
            "floortrigger_active_01",
            "foyerdoor_field_01",
            "foyerdoor_open_01",
            "foyerdoor_open_02",
            "foyerdoor_open_03",
            "foyerdoor_open_04",
            "foyerdoor_open_05",
            "foyerdoor_open_06",
            "foyerdoor_open_07",
            "foyerdoor_open_08",
            "foyerdoor_open_09",
            "foyerdoor_open_10",
            "foyerdoor_open_11",
            "foyerdoor_open_12",
            "foyerdoor_open_13",
            "casino_poker_table_shadow",
            "casino_poker_table_001",
            "casino_poker_table_props_001",
            "casino_poker_table_props_002",
            "casino_hatrack_001",
            "casino_litter_paper_001",
            "casino_litter_cans_001",
            "cabinet_covered_001",
            "cabinet_decorative_001",
            "cabinet_idle_001",
            "cabinet_idle_002",
            "cabinet_idle_003",
            "cabinet_idle_004",
            "cabinet_idle_005",
            "cabinet_idle_006",
            "cabinet_idle_007",
            "cabinet_idle_008",
            "cabinet_idle_009",
            "cabinet_idle_010",
            "cabinet_idle_011",
            "cabinet_idle_012",
            "cabinet_interact_001",
            "cabinet_interact_002",
            "cabinet_interact_003",
            "cabinet_interact_004",
            "cabinet_sleep_001",
            "cabinet_sleep_002",
            "cabinet_sleep_003",
            "cabinet_sleep_004",
            "cabinet_sleep_005",
            "cabinet_sleep_006",
            "cabinet_sleep_007",
            "cabinet_sleep_008",
            "cabinet_sleep_009",
            "cabinet_sleep_010",
            "cabinet_sleep_011",
            "cabinet_sleep_012",
            "cabinet_sleep_013",
            "cabinet_sleep_014",
            "cabinet_fight_001",
            "cabinet_fight_002",
            "cabinet_fight_003",
            "cabinet_fight_004",
            "cabinet_fight_005",
            "cabinet_fight_006",
            "cabinet_fight_007",
            "cabinet_fight_008",
            "cabinet_fight_009",
            "cabinet_fight_010",
            "cabinet_fight_011",
            "cabinet_fight_012",
            "cabinet_fight_013",
            "cabinet_fight_014",
            "cabinet_shadow_001",
            "punchout_coin_left",
            "punchout_coin_right",
            "depressedcabinet_blink_001",
            "depressedcabinet_blink_002",
            "depressedcabinet_blink_003",
            "depressedcabinet_idle_001",
            "depressedcabinet_sigh_001",
            "depressedcabinet_sigh_002",
            "depressedcabinet_sigh_003",
            "depressedcabinet_sigh_004",
            "depressedcabinet_sigh_005",
            "depressedcabinet_sigh_006",
            "depressedcabinet_sigh_007",
            "gunball_idle_001",
            "gunball_idle_002",
            "gunball_idle_003",
            "gunball_idle_004",
            "gunball_interact_001",
            "gunball_interact_002",
            "gunball_interact_003",
            "gunball_interact_004",
            "gunball_interact_005",
            "gunball_interact_006",
            "gunball_use_001",
            "gunball_use_002",
            "gunball_use_003",
            "gunball_use_004",
            "gunball_use_005",
            "gunball_use_006",
            "gunball_use_007",
            "gunball_use_008",
            "gunball_use_009",
            "gunball_use_010",
            "gunball_use_011",
            "gunball_use_012",
            "gunball_use_013",
            "gunball_use_014",
            "gunball_use_015",
            "gunball_use_016",
            "gunball_use_017",
            "gunball_use_018",
            "gunball_use_019",
            "gunball_use_020",
            "gunball_use_021",
            "gunball_use_022",
            "gunball_use_023",
            "gunball_use_024",
            "gunball_use_025",
            "gunball_use_026",
            "gunball_use_027",
            "gunball_use_028",
            "gunball_empty_001",
            "gunball_shadow_001"
        };

        public static readonly List<string> EXItemCollection = new List<string> {
            "babygoodhammer",
            "babygoodhammer_spawn_00",
            "babygoodhammer_spawn_01",
            "babygoodhammer_spawn_02",
            "babygoodhammer_spawn_03",
            "babygoodhammer_spawn_04",
            "babygoodhammer_spawn_05",
            "babygoodhammer_spawn_06",
            "babygoodhammer_spawn_07",
            "babygoodhammer_spawn_08",
            "babygoodhammer_spawn_09",
            "babygoodhammer_spawn_10",
            "babygoodhammer_spawn_11",
            "babygoodhammer_spawn_12",
            "babygoodhammer_spawn_13",
            "babygoodhammer_spawn_14",
            "babygoodhammer_spawn_15",
            "babygoodhammer_spawn_16",
            "babygoodhammer_spawn_17",
            "babygoodhammer_spawn_18",
            "babygoodhammer_spawn_19",
            "babygoodhammer_spawn_20",
            "babygoodhammer_spawn_21",
            "babygoodhammer_spawn_22",
            "babygoodhammer_spawn_23",
            "babygoodhammer_spawn_24",
            "babygoodhammer_spawn_25",
            "babysitter",
            "corrupted_poopsack_01",
            "corrupted_poopsack_02",
            "corrupted_poopsack_03",
            "corrupted_poopsack_04",
            "corrupted_poopsack_05",
            "corrupted_poopsack_06",
            "corrupted_poopsack_07",
            "corrupted_poopsack_08",
            "corrupted_poopsack_09",
            "corrupted_poopsack_10",
            "corruptionbomb",
            "corruptionbomb_minimapicon",
            "corruptionbomb_spin_01",
            "corruptionbomb_spin_02",
            "corruptionbomb_spin_03",
            "corruptionbomb_spin_04",
            "corruptionbomb_spin_05",
            "corruptionbomb_spin_06",
            "corruptionbomb_spin_07",
            "corruptionbomb_spin_08",
            "corruptionbomb_spin_09",
            "corruptionbomb_spin_10",
            "cronenbergbullets",
            "cursedbrick",
            "ex_mimiclay",
            "glitchround",
            "junglecrest",
            "plunger_fire_001",
            "plunger_fire_002",
            "plunger_fire_003",
            "plunger_fire_004",
            "plunger_fire_005",
            "plunger_fire_006",
            "PowBlock",
            "PowBlock_Idle_01",
            "PowBlock_Idle_02",
            "PowBlock_Idle_03",
            "PowBlock_Idle_04",
            "PowBlock_Idle_05",
            "PowBlock_Idle_06",
            "PowBlock_Idle_07",
            "PowBlock_Idle_08",
            "PowBlock_Idle_09",
            "PowBlock_Idle_10",
            "PowBlock_Idle_11",
            "PowBlock_Idle_12",
            "PowBlock_Idle_13",
            "PowBlock_Idle_14",
            "PowBlock_Used",
            "rockslide",
            "SonicBox_Broken_01",
            "SonicBox_Idle_01",
            "SonicBox_Idle_02",
            "SonicBox_Idle_03",
            "SonicRing_Idle_01",
            "SonicRing_Idle_02",
            "SonicRing_Idle_03",
            "SonicRing_Idle_04",
            "SonicRing_Idle_05",
            "SonicRing_Idle_06",
            "SonicRing_Idle_07",
            "SonicRing_Idle_08",
            "SonicRing_Idle_09",
            "SonicRing_Idle_10",
            "SonicRing_Idle_11",
            "SonicRing_Idle_12",
            "SonicRing_Idle_13",
            "SonicRing_Idle_14",
            "SonicRing_Idle_15",
            "tabletech_assassin",
            "theleadkey",
            "thethirdeye",
            "clownbullets",
            "portable_elevator_pushed",
            "portable_elevator",
            "portableship",
            "portableship_alt",
            "clownfriend"
        };

        public static readonly List<string> ClownkinCollection = new List<string> {
            "clownkin_death_back_south_001",
            "clownkin_death_back_south_002",
            "clownkin_death_back_south_003",
            "clownkin_death_back_south_004",
            "clownkin_death_front_north_001",
            "clownkin_death_front_north_002",
            "clownkin_death_front_north_003",
            "clownkin_death_front_north_004",
            "clownkin_death_left_back_001",
            "clownkin_death_left_back_002",
            "clownkin_death_left_back_003",
            "clownkin_death_left_back_004",
            "clownkin_death_left_back_005",
            "clownkin_death_left_front_001",
            "clownkin_death_left_front_002",
            "clownkin_death_left_front_003",
            "clownkin_death_left_front_004",
            "clownkin_death_left_front_005",
            "clownkin_death_left_side_001",
            "clownkin_death_left_side_002",
            "clownkin_death_left_side_003",
            "clownkin_death_left_side_004",
            "clownkin_death_right_back_001",
            "clownkin_death_right_back_002",
            "clownkin_death_right_back_003",
            "clownkin_death_right_back_004",
            "clownkin_death_right_back_005",
            "clownkin_death_right_front_001",
            "clownkin_death_right_front_002",
            "clownkin_death_right_front_003",
            "clownkin_death_right_front_004",
            "clownkin_death_right_front_005",
            "clownkin_death_right_side_001",
            "clownkin_death_right_side_002",
            "clownkin_death_right_side_003",
            "clownkin_death_right_side_004",
            "clownkin_die_left_001",
            "clownkin_die_left_002",
            "clownkin_die_left_back_001",
            "clownkin_die_left_back_002",
            "clownkin_die_right_001",
            "clownkin_die_right_002",
            "clownkin_die_right_back_001",
            "clownkin_die_right_back_002",
            "clownkin_hit_back_left_001",
            "clownkin_hit_back_right_001",
            "clownkin_hit_left_001",
            "clownkin_hit_right_001",
            "clownkin_idle_back_001",
            "clownkin_idle_back_002",
            "clownkin_idle_left_001",
            "clownkin_idle_left_002",
            "clownkin_idle_right_001",
            "clownkin_idle_right_002",
            "clownkin_pitfall_001",
            "clownkin_pitfall_002",
            "clownkin_pitfall_003",
            "clownkin_pitfall_004",
            "clownkin_pitfall_005",
            "clownkin_run_left_001",
            "clownkin_run_left_002",
            "clownkin_run_left_003",
            "clownkin_run_left_004",
            "clownkin_run_left_005",
            "clownkin_run_left_006",
            "clownkin_run_left_back_001",
            "clownkin_run_left_back_002",
            "clownkin_run_left_back_003",
            "clownkin_run_left_back_004",
            "clownkin_run_left_back_005",
            "clownkin_run_left_back_006",
            "clownkin_run_right_001",
            "clownkin_run_right_002",
            "clownkin_run_right_003",
            "clownkin_run_right_004",
            "clownkin_run_right_005",
            "clownkin_run_right_006",
            "clownkin_run_right_back_001",
            "clownkin_run_right_back_002",
            "clownkin_run_right_back_003",
            "clownkin_run_right_back_004",
            "clownkin_run_right_back_005",
            "clownkin_run_right_back_006",
            "clownkin_spawn_001",
            "clownkin_spawn_002",
            "clownkin_spawn_003",
            "clownkin_wig",
            "clownkin_wig_grounded"
        };

        public static readonly List<string> EXBalloonCollection = new List<string> {
            "blueballoon_idle_001",
            "blueballoon_pop_001",
            "blueballoon_pop_002",
            "blueballoon_pop_003",
            "greenballoon_idle_001",
            "greenballoon_pop_001",
            "greenballoon_pop_002",
            "greenballoon_pop_003",
            "pinkballoon_pop_003",
            "pinkballoon_idle_001",
            "pinkballoon_pop_001",
            "pinkballoon_pop_002",
            "redballoon_idle_001",
            "redballoon_pop_001",
            "redballoon_pop_002",
            "redballoon_pop_003",
            "yellowballoon_idle_001",
            "yellowballoon_pop_001",
            "yellowballoon_pop_002",
            "yellowballoon_pop_003"
        };

        public static readonly List<string> EXPortableElevatorCollection = new List<string> {
            "portable_elevator_interiorfloor",
            "portable_elevator_floor",
            "portable_elevator_floor_border",
            "portable_elevator_floor_alt",
            "portable_elevator_floor_broken",
            "portable_elevator_floor_broken_alt",
            "portable_elevator_reticle_red",
            "portable_elevator_reticle_green",
            "portable_elevator_arrive_01",
            "portable_elevator_arrive_02",
            "portable_elevator_arrive_03",
            "portable_elevator_arrive_04",
            "portable_elevator_open_01",
            "portable_elevator_open_02",
            "portable_elevator_open_03",
            "portable_elevator_open_04",
            "portable_elevator_open_05",
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
            "portable_elevator_depart_11"
        };

        public static readonly List<string> EXJungleCollection = new List<string>() {
            "Jungle_Tree_Large",
            "Jungle_Tree_Large_Frame",
            "Jungle_Tree_Large_Open",
            "Jungle_Tree_Large_Shadow",
            "JungleTree_MinimapIcon",
            "junglecrest_minimapicon.png",
            "Jungle_TreeStump",
            "Jungle_ExitLadder",
            "Jungle_ExitLadder_Destination",
            "Jungle_ExitLadder_Destination_Hole"
        };
        public static readonly List<string> EXOfficeCollection = new List<string>() {
            "office_one_way_blocker_vertical_top_001",
            "office_one_way_blocker_vertical_bottom_001",
            "office_one_way_blocker_horizontal_bottom_001",
            "office_one_way_blocker_horizontal_top_001"
        };
        public static readonly List<string> EXSpaceCollection = new List<string>() {
            "spacegrass_01",
            "spacegrass_02",
            "spacegrass_03",
            "spacegrass_04"
        };
        public static readonly List<string> EXTrapCollection = new List<string>() {
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
            "RickRoll_RiseUp_12",
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
            "RickRoll_28",
            "RainbowRoad",
            "RainbowRoad_PitBorders",
            "EX_Trap_Apache_01",
            "EX_Trap_Apache_02",
            "music_switch_idle_on_001",
            "music_switch_idle_off_001",
            "music_switch_activate_001",
            "music_switch_activate_002",
            "music_switch_activate_002_reverse",
            "music_switch_activate_003",
            "music_switch_activate_004",
            "music_switch_activate_005",
            "alarm_mushroom2_alarm_001",
            "alarm_mushroom2_alarm_002",
            "alarm_mushroom2_alarm_003",
            "alarm_mushroom2_alarm_004",
            "alarm_mushroom2_alarm_005",
            "alarm_mushroom2_alarm_006",
            "alarm_mushroom2_break_001",
            "alarm_mushroom2_break_002",
            "alarm_mushroom2_break_003",
            "alarm_mushroom2_break_004",
            "alarm_mushroom2_dead_001",
            "alarm_mushroom2_idle_001",
            "alarm_mushroom2_idle_002",
            "alarm_mushroom2_idle_003",
            "alarm_mushroom2_idle_004",
            "alarm_mushroom2_idle_005",
            "alarm_mushroom2_shadow_001"
        };

        public static readonly List<int> RatChestItems = new List<int>() {
            626, // elimentaler
            662, // partially_eaten_cheese
            663, // resourceful_sack
            667 // rat_boots
        };

        public static readonly List<string> OverrideFallIntoPitsList = new List<string>() {
            "ed37fa13e0fa4fcf8239643957c51293", // gigi
            "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
            "98fdf153a4dd4d51bf0bafe43f3c77ff", // tazie
            "2feb50a6a40f4f50982e89fd276f6f15", // bullat
            "2d4f8b5404614e7d8b235006acde427a", // shotgat
            "b4666cb6ef4f4b038ba8924fd8adf38f", // grenat
            "7ec3e8146f634c559a7d58b19191cd43", // spirat
            "4db03291a12144d69fe940d5a01de376", // hollowpoint
            "b70cbd875fea498aa7fd14b970248920", // great_bullet_shark
            "72d2f44431da43b8a3bae7d8a114a46d", // bullet_shark
            "c182a5cb704d460d9d099a47af49c913", // pot_fairy
            "9b4fb8a2a60a457f90dcf285d34143ac", // gat
            "48d74b9c65f44b888a94f9e093554977", // x_det
            "c5a0fd2774b64287bf11127ca59dd8b4", // diagonal_x_det
            "b67ffe82c66742d1985e5888fd8e6a03", // vertical_det
            "d9632631a18849539333a92332895ebd", // diagonal_det
            "1898f6fe1ee0408e886aaf05c23cc216", // horizontal_det
            "abd816b0bcbf4035b95837ca931169df", // vertical_x_det
            "07d06d2b23cc48fe9f95454c839cb361", // horizontal_x_det
            "ccf6d241dad64d989cbcaca2a8477f01", // t_bulon
            "864ea5a6a9324efc95a0dd2407f42810", // cubulon
            "1bc2a07ef87741be90c37096910843ab", // chancebulon
            "88f037c3f93b4362a040a87b30770407", // gunreaper
            "0d3f7c641557426fbac8596b61c9fb45", // lord_of_the_jammed
            "d4f4405e0ff34ab483966fd177f2ece3", // cylinder
            "534f1159e7cf4f6aa00aeea92459065e", // cylinder_red
            "981d358ffc69419bac918ca1bdf0c7f7", // bullat_gargoyle
            "4b21a913e8c54056bc05cafecf9da880", // gigi_parrot
            "78e0951b097b46d89356f004dda27c42", // tablet_bookllet
            "216fd3dfb9da439d9bd7ba53e1c76462", // necronomicon_bookllet
            // Companions
            "c07ef60ae32b404f99e294a6f9acba75", // dog
            "7bd9c670f35b4b8d84280f52a5cc47f6", // cucco
            "705e9081261446039e1ed9ff16905d04", // cop
            "998807b57e454f00a63d67883fcf90d6", // portable_turret
            "11a14dbd807e432985a89f69b5f9b31e", // phoenix
            "640238ba85dd4e94b3d6f68888e6ecb8", // cop_android
            "6f9c28403d3248c188c391f5e40774c5", // turkey
            "3a077fa5872d462196bb9a3cb1af02a3", // super_space_turtle
            "1ccdace06ebd42dc984d46cb1f0db6cf", // r2g2
            "fe51c83b41ce4a46b42f54ab5f31e6d0", // pig
            "ededff1deaf3430eaf8321d0c6b2bd80", // hunters_past_dog
            "d375913a61d1465f8e4ffcf4894e4427", // caterpillar
            "5695e8ffa77c4d099b4d9bd9536ff35e", // blank_companion
            "c6c8e59d0f5d41969c74e802c9d67d07", // ser_junkan
            "86237c6482754cd29819c239403a2de7", // pig_synergy
            "ad35abc5a3bf451581c3530417d89f2c", // blank_companion_synergy
            "e9fa6544000942a79ad05b6e4afb62db", // raccoon
            "ebf2314289ff4a4ead7ea7ef363a0a2e", // dog_synergy_1
            "ab4a779d6e8f429baafa4bf9e5dca3a9", // dog_synergy_2
            "9216803e9c894002a4b931d7ea9c6bdf", // super_space_turtle_synergy
            "cc9c41aa8c194e17b44ac45f993dd212", // super_space_turtle_dummy
            "45f5291a60724067bd3ccde50f65ac22", // payday_shooter_01
            "41ab10778daf4d3692e2bc4b370ab037", // payday_shooter_02
            "2976522ec560460c889d11bb54fbe758", // payday_shooter_03
            "e456b66ed3664a4cb590eab3a8ff3814", // baby_mimic
            "3f40178e10dc4094a1565cd4fdc4af56" // baby_shelleton
        };

        public static readonly List<string> DontDieOnCollisionWhenTinyGUIDList = new List<string>() {
            "76bc43539fc24648bff4568c75c686d1", // chicken - This already dies on contact, plus I don't want to override it's death sound. :P
            // Companions
            "c07ef60ae32b404f99e294a6f9acba75", // dog
            "7bd9c670f35b4b8d84280f52a5cc47f6", // cucco
            "705e9081261446039e1ed9ff16905d04", // cop
            "998807b57e454f00a63d67883fcf90d6", // portable_turret
            "11a14dbd807e432985a89f69b5f9b31e", // phoenix
            "640238ba85dd4e94b3d6f68888e6ecb8", // cop_android
            "6f9c28403d3248c188c391f5e40774c5", // turkey
            "3a077fa5872d462196bb9a3cb1af02a3", // super_space_turtle
            "1ccdace06ebd42dc984d46cb1f0db6cf", // r2g2
            "fe51c83b41ce4a46b42f54ab5f31e6d0", // pig
            "ededff1deaf3430eaf8321d0c6b2bd80", // hunters_past_dog
            "d375913a61d1465f8e4ffcf4894e4427", // caterpillar
            "5695e8ffa77c4d099b4d9bd9536ff35e", // blank_companion
            "c6c8e59d0f5d41969c74e802c9d67d07", // ser_junkan
            "86237c6482754cd29819c239403a2de7", // pig_synergy
            "ad35abc5a3bf451581c3530417d89f2c", // blank_companion_synergy
            "e9fa6544000942a79ad05b6e4afb62db", // raccoon
            "ebf2314289ff4a4ead7ea7ef363a0a2e", // dog_synergy_1
            "ab4a779d6e8f429baafa4bf9e5dca3a9", // dog_synergy_2
            "9216803e9c894002a4b931d7ea9c6bdf", // super_space_turtle_synergy
            "cc9c41aa8c194e17b44ac45f993dd212", // super_space_turtle_dummy
            "45f5291a60724067bd3ccde50f65ac22", // payday_shooter_01
            "41ab10778daf4d3692e2bc4b370ab037", // payday_shooter_02
            "2976522ec560460c889d11bb54fbe758", // payday_shooter_03
            "e456b66ed3664a4cb590eab3a8ff3814" // baby_mimic
            // "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
            // "c0260c286c8d4538a697c5bf24976ccf" // dynamite_kin
            /*
            "f38686671d524feda75261e469f30e0b", // ammoconda_ball
            "21dd14e5ca2a4a388adab5b11b69a1e1", // shelleton"
            "ec8ea75b557d4e7b8ceeaacdf6f8238c", // gun_nut
            "383175a55879441d90933b5c4e60cf6f", // spectre_gun_nut
            "463d16121f884984abe759de38418e48", // chain_gunner
            "ac9d345575444c9a8d11b799e8719be0", // rat_chest_mimic
            "d8d651e3484f471ba8a2daa4bf535ce6", // blue_chest_mimic
            "abfb454340294a0992f4173d6e5898a8", // green_chest_mimic
            "6450d20137994881aff0ddd13e3d40c8", // black_chest_mimic
            "d8fd592b184b4ac9a3be217bc70912a2", // red_chest_mimic
            "b70cbd875fea498aa7fd14b970248920", // great_bullet_shark
            "78a8ee40dff2477e9c2134f6990ef297", // mine_flayers_bell
            "eed5addcc15148179f300cc0d9ee7f94", // spogre
            "0239c0680f9f467dbe5c4aab7dd1eca6", // blobulon
            "e61cab252cfb435db9172adc96ded75f", // poisbulon
            "c5b11bfc065d417b9c4d03a5e385fe2c" // professional
            */
        };

        public static readonly List<string> BannedEnemyGUIDList = new List<string>() {
            // Bosses and MiniBosses
            "3f11bbbc439c4086a180eb0fb9990cb4", // kill_pillars
            "6c43fddfd401456c916089fdd1c99b1c", // high_priest
            "1b5810fafbec445d89921a4efb4e42b7", // blobulord
            "4b992de5b4274168a8878ef9bf7ea36b", // beholster
            "ec6b674e0acd4553b47ee94493d66422", // gatling_gull
            "fa76c8cfdf1c4a88b55173666b4bc7fb", // treadnaught
            "df7fb62405dc4697b7721862c7b6b3cd", // treadnaughts_bullet_kin
            "f3b04a067a65492f8b279130323b41f0", // wallmonger
            "ffca09398635467da3b1f4a54bcfda80", // bullet_king
            "5729c8b5ffa7415bb3d01205663a33ef", // old_king
            "465da2bb086a4a88a803f79fe3a27677", // dragun
            "5e0af7f7d9de4755a68d2fd3bbc15df4", // cannonbalrog
            "6868795625bd46f3ae3e4377adce288b", // resourceful_rat
            "4d164ba3f62648809a4a82c90fc22cae", // resourceful_rat_mech
            "05b8afe0b6cc4fffa9dc6036fa24c8ec", // dragun_advanced
            "2e6223e42e574775b56c6349921f42cb", // dragun_knife_advanced
            "39de9bd6a863451a97906d949c103538", // tsar_bomba
            "8d441ad4e9924d91b6070d5b3438d066", // dr_wolfs_monster
            "ce2d2a0dced0444fb751b262ec6af08a", // dr_wolf
            "cd88c3ce60c442e9aa5b3904d31652bc", // lich
            "68a238ed6a82467ea85474c595c49c6e", // megalich
            "7c5d5f09911e49b78ae644d2b50ff3bf", // infinilich
            "dc3cd41623d447aeba77c77c99598426", // interdimensional_horror
            "8b913eea3d174184be1af362d441910d", // black_stache
            "b98b10fca77d469e80fb45f3c5badec5", // hm_absolution
            "78eca975263d4482a4bfa4c07b32e252", // draguns_knife
            "8b0dd96e2fe74ec7bebc1bc689c0008a", // mine_flayer
            "2ccaa1b7ae10457396a1796decda9cf6", // agunim
            "39dca963ae2b4688b016089d926308ab", // cannon
            "db97e486ef02425280129e1e27c33118", // shadow_agunim
            // Normal Enemies
            "ba928393c8ed47819c2c5f593100a5bc", // metal_cube_guy
            "2b6854c0849b4b8fb98eb15519d7db1c", // bullet_kin_mech
            "9215d1a221904c7386b481a171e52859", // lead_maiden_fridge
            "226fd90be3a64958a5b13cb0a4f43e97", // musket_kin
            "df4e9fedb8764b5a876517431ca67b86", // bullet_kin_gal_titan_boss
            "1f290ea06a4c416cabc52d6b3cf47266", // bullet_kin_titan_boss
            "c4cf0620f71c4678bb8d77929fd4feff", // bullet_kin_titan
            "3cadf10c489b461f9fb8814abc1a09c1", // minelet
            "21dd14e5ca2a4a388adab5b11b69a1e1", // shelleton
            "1bc2a07ef87741be90c37096910843ab", // chancebulon
            "57255ed50ee24794b7aac1ac3cfb8a95", // gun_cultist
            "4db03291a12144d69fe940d5a01de376", // hollowpoint
            "206405acad4d4c33aac6717d184dc8d4", // apprentice_gunjurer
            "c4fba8def15e47b297865b18e36cbef8", // gunjurer
            "56fb939a434140308b8f257f0f447829", // lore_gunjurer
            "9b2cf2949a894599917d4d391a0b7394", // high_gunjurer
            "43426a2e39584871b287ac31df04b544", // wizbang
            "699cd24270af4cd183d671090d8323a1", // key_bullet_kin // Flee behaviour generates an exception in the logs.
            "a446c626b56d4166915a4e29869737fd", // chance_bullet_kin // His drops sometimes don't appear correctly when resized.
            "22fc2c2c45fb47cf9fb5f7b043a70122", // grip_master // Being tossed from a room from tiny Grip Master can soft lock the game.
            "42be66373a3d4d89b91a35c9ff8adfec", // blobulin
            "b8103805af174924b578c98e95313074", // poisbulin
            "3e98ccecf7334ff2800188c417e67c15", // killithid
            "ffdc8680bdaa487f8f31995539f74265", // muzzle_wisp
            "d8a445ea4d944cc1b55a40f22821ae69", // muzzle_flare
            "c2f902b7cbe745efb3db4399927eab34", // skusket_head
            "98ca70157c364750a60f5e0084f9d3e2", // phaser_spider
            "14ea47ff46b54bb4a98f91ffcffb656d", // rat_candle
            "6ad1cafc268f4214a101dca7af61bc91", // rat
            "479556d05c7c44f3b6abb3b2067fc778", // wall_mimic
            "796a7ed4ad804984859088fc91672c7f", // pedestal_mimic
            "475c20c1fd474dfbad54954e7cba29c1", // tarnisher
            "45192ff6d6cb43ed8f1a874ab6bef316", // misfire_beast
            "eeb33c3a5a8e4eaaaaf39a743e8767bc", // candle_guy
            "56f5a0f2c1fc4bc78875aea617ee31ac", // spectre
            "2feb50a6a40f4f50982e89fd276f6f15", // bullat
            "2d4f8b5404614e7d8b235006acde427a", // shotgat
            "b4666cb6ef4f4b038ba8924fd8adf38f", // grenat
            "7ec3e8146f634c559a7d58b19191cd43", // spirat
            "af84951206324e349e1f13f9b7b60c1a", // skusket
            "e667fdd01f1e43349c03a18e5b79e579", // tutorial_turret
            "41ba74c517534f02a62f2e2028395c58", // faster_tutorial_turret
            "ac986dabc5a24adab11d48a4bccf4cb1", // det
            "3f6d6b0c4a7c4690807435c7b37c35a5", // agonizer
            "cd4a4b7f612a4ba9a720b9f97c52f38c", // lead_maiden
            "98ea2fe181ab4323ab6e9981955a9bca", // shambling_round
            "d5a7b95774cd41f080e517bea07bf495", // revolvenant
            "88f037c3f93b4362a040a87b30770407", // gunreaper
            "1386da0f42fb4bcabc5be8feb16a7c38", // snake
            "566ecca5f3b04945ac6ce1f26dedbf4f", // mine_flayers_claymore
            // Thwomp type enemies
            "f155fd2759764f4a9217db29dd21b7eb", // mountain_cube
            "33b212b856b74ff09252bf4f2e8b8c57", // lead_cube
            "3f2026dc3712490289c4658a2ba4a24b", // flesh_cube
            // Chest Mimics
            "2ebf8ef6728648089babb507dec4edb7", // brown_chest_mimic
            "d8d651e3484f471ba8a2daa4bf535ce6", // blue_chest_mimic
            "abfb454340294a0992f4173d6e5898a8", // green_chest_mimic
            "d8fd592b184b4ac9a3be217bc70912a2", // red_chest_mimic
            "6450d20137994881aff0ddd13e3d40c8", // black_chest_mimic
            "ac9d345575444c9a8d11b799e8719be0", // rat_chest_mimic            
            // Companions
            "705e9081261446039e1ed9ff16905d04", // cop
            "640238ba85dd4e94b3d6f68888e6ecb8", // cop_android
            "3a077fa5872d462196bb9a3cb1af02a3", // super_space_turtle
            "1ccdace06ebd42dc984d46cb1f0db6cf", // r2g2
            "fe51c83b41ce4a46b42f54ab5f31e6d0", // pig
            "ededff1deaf3430eaf8321d0c6b2bd80", // hunters_past_dog
            "d375913a61d1465f8e4ffcf4894e4427", // caterpillar
            "5695e8ffa77c4d099b4d9bd9536ff35e", // blank_companion
            "c6c8e59d0f5d41969c74e802c9d67d07", // ser_junkan
            "86237c6482754cd29819c239403a2de7", // pig_synergy
            "ad35abc5a3bf451581c3530417d89f2c", // blank_companion_synergy
            "e9fa6544000942a79ad05b6e4afb62db", // raccoon
            "ebf2314289ff4a4ead7ea7ef363a0a2e", // dog_synergy_1
            "ab4a779d6e8f429baafa4bf9e5dca3a9", // dog_synergy_2
            "9216803e9c894002a4b931d7ea9c6bdf", // super_space_turtle_synergy
            "cc9c41aa8c194e17b44ac45f993dd212", // super_space_turtle_dummy
            "45f5291a60724067bd3ccde50f65ac22", // payday_shooter_01
            "41ab10778daf4d3692e2bc4b370ab037", // payday_shooter_02
            "2976522ec560460c889d11bb54fbe758", // payday_shooter_03
            "e456b66ed3664a4cb590eab3a8ff3814", // baby_mimic
            "3f40178e10dc4094a1565cd4fdc4af56" // baby_shelleton
        };

        public static readonly string[] blobsAndCritters = {
            "0239c0680f9f467dbe5c4aab7dd1eca6", // blobulon
            "042edb1dfb614dc385d5ad1b010f2ee3", // blobuloid
            "42be66373a3d4d89b91a35c9ff8adfec", // blobulin
            "e61cab252cfb435db9172adc96ded75f", // poisbulon
            "fe3fe59d867347839824d5d9ae87f244", // poisbuloid
            "b8103805af174924b578c98e95313074", // poisbulin
            "1a4872dafdb34fd29fe8ac90bd2cea67", // king_bullat
            "6ad1cafc268f4214a101dca7af61bc91", // rat
            "14ea47ff46b54bb4a98f91ffcffb656d", // rat_candle
            "76bc43539fc24648bff4568c75c686d1", // chicken
            "1386da0f42fb4bcabc5be8feb16a7c38", // snake
            "95ea1a31fc9e4415a5f271b9aedf9b15", // robots_past_critter_1
            "42432592685e47c9941e339879379d3a", // robots_past_critter_2
            "4254a93fc3c84c0dbe0a8f0dddf48a5a", // robots_past_critter_3
            "8b43a5c59b854eb780f9ab669ec26b7a", // dragun_egg_slimeguy
            "d1c9781fdac54d9e8498ed89210a0238" // tiny_blobulord
        };

        public static readonly string[] DontGlitchMeList = {
            "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
            // These enemies can now be glitched via normal means
            // "128db2f0781141bcb505d8f00f9e4d47", // red_shotgun_kin
            // "b54d89f9e802455cbb2b8a96a31e8259", // blue_shotgun_kin
            // "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
            "45192ff6d6cb43ed8f1a874ab6bef316", // misfire_beast
            "ba928393c8ed47819c2c5f593100a5bc" // MetalCubeGuy (trap version)
        };

        public static string[] ValidSourceEnemyGUIDList = {
            "01972dee89fc4404a5c408d50007dad5",	// BulletMan
            "57255ed50ee24794b7aac1ac3cfb8a95",	// Cultist
	        "4db03291a12144d69fe940d5a01de376",	// Ghost
	        "05891b158cd542b1a5f3df30fb67a7ff",	// ArrowheadMan
	        "31a3ea0c54a745e182e22ea54844a82d",	// BulletRifleMan
	        "c5b11bfc065d417b9c4d03a5e385fe2c",	// BulletRifleProfessional
	        "1a78cfb776f54641b832e92c44021cf2",	// AshBulletMan
	        "1bd8e49f93614e76b140077ff2e33f2b",	// AshBulletShotgunMan
	        "8bb5578fba374e8aae8e10b754e61d62",	// BulletCardinal
	        "db35531e66ce41cbb81d507a34366dfe",	// BulletMachineGunMan
        	"5f3abc2d561b4b9c9e72b879c6f10c7e",	// BulletManDevil
	        "e5cffcfabfae489da61062ea20539887",	// BulletManShroomed
	        "95ec774b5a75467a9ab05fa230c0c143",	// BulletSkeletonHelmet
	        "2752019b770f473193b08b4005dc781f",	// BulletShotgunMan_SawedOff <-- Veteran Shotgun Kin
	        "7f665bd7151347e298e4d366f8818284",	// BulletShotgunMan_Mutant
	        "d4a9836f8ab14f3fadd0f597438b1f1f",	// BulletMan_Mutant
	        "044a9f39712f456597b9762893fbc19c"	// BulletShotgrubMan
        };
        
        public static readonly string SafeEnemyGUIDList = "eeb33c3a5a8e4eaaaaf39a743e8767bc";  // candle_guy
        
        public static readonly string[] SpawnEnemyOnDeathGUIDList = {
            "6ad1cafc268f4214a101dca7af61bc91", // rat
            "14ea47ff46b54bb4a98f91ffcffb656d", // rat_candle
            "76bc43539fc24648bff4568c75c686d1", // chicken
            "1386da0f42fb4bcabc5be8feb16a7c38", // snake
            "8b43a5c59b854eb780f9ab669ec26b7a", // dragun_egg_slimeguy
            "d1c9781fdac54d9e8498ed89210a0238", // tiny_blobulord
            "95ea1a31fc9e4415a5f271b9aedf9b15", // robots_past_critter_1
            "42432592685e47c9941e339879379d3a", // robots_past_critter_2
            "4254a93fc3c84c0dbe0a8f0dddf48a5a", // robots_past_critter_3
            "042edb1dfb614dc385d5ad1b010f2ee3", // blobuloid
            "fe3fe59d867347839824d5d9ae87f244", // poisbuloid
            "01972dee89fc4404a5c408d50007dad5", // bullet_kin
            "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
            "d4a9836f8ab14f3fadd0f597438b1f1f", // mutant_bullet_kin
            "d4dd2b2bbda64cc9bcec534b4e920518", // bullet_kings_toadie_revenge
            "05891b158cd542b1a5f3df30fb67a7ff", // arrow_head
            "699cd24270af4cd183d671090d8323a1", // key_bullet_kin
            "022d7c822bc146b58fe3b0287568aaa2", // blizzbulon
            "7ec3e8146f634c559a7d58b19191cd43", // spirat
            "b4666cb6ef4f4b038ba8924fd8adf38f", // grenat
            "2feb50a6a40f4f50982e89fd276f6f15", // bullat
            "2d4f8b5404614e7d8b235006acde427a" // shotgat
        };
        
        public static readonly string[] ReplacementEnemyGUIDList = {
            "01972dee89fc4404a5c408d50007dad5", // bullet_kin
            "05891b158cd542b1a5f3df30fb67a7ff", // arrow_head
            "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
            "8bb5578fba374e8aae8e10b754e61d62", // cardinal
            "f905765488874846b7ff257ff81d6d0c", // fungun
            "37340393f97f41b2822bc02d14654172", // creech
            "5f3abc2d561b4b9c9e72b879c6f10c7e", // fallen_bullet_kin
            "f38686671d524feda75261e469f30e0b", // ammoconda_ball
            "1bd8e49f93614e76b140077ff2e33f2b", // ashen_shotgun_kin
            "1a78cfb776f54641b832e92c44021cf2", // ashen_bullet_kin
            "2752019b770f473193b08b4005dc781f", // veteran_shotgun_kin
            "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
            "d4dd2b2bbda64cc9bcec534b4e920518", // bullet_kings_toadie_revenge
            "a9cc6a4e9b3d46ea871e70a03c9f77d4", // marines_past_imp
            "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
            "98fdf153a4dd4d51bf0bafe43f3c77ff", // tazie
            "be0683affb0e41bbb699cb7125fdded6", // mouser
            "8b43a5c59b854eb780f9ab669ec26b7a", // dragun_egg_slimeguy
            "d1c9781fdac54d9e8498ed89210a0238", // tiny_blobulord
            "76bc43539fc24648bff4568c75c686d1", // chicken
            "226fd90be3a64958a5b13cb0a4f43e97", // musket_kin
            "df4e9fedb8764b5a876517431ca67b86", // bullet_kin_gal_titan_boss
            "1f290ea06a4c416cabc52d6b3cf47266", // bullet_kin_titan_boss
            "c4cf0620f71c4678bb8d77929fd4feff", // bullet_kin_titan
            "6f818f482a5c47fd8f38cce101f6566c", // bullet_kin_pirate
            "143be8c9bbb84e3fb3ab98bcd4cf5e5b", // bullet_kin_fish
            "06f5623a351c4f28bc8c6cda56004b80", // bullet_kin_fish_blue
            "ff4f54ce606e4604bf8d467c1279be3e", // bullet_kin_broccoli
            "39e6f47a16ab4c86bec4b12984aece4c", // bullet_kin_knight
            "f020570a42164e2699dcf57cac8a495c", // bullet_kin_kaliber
            "37de0df92697431baa47894a075ba7e9", // bullet_kin_candle
            "5861e5a077244905a8c25c2b7b4d6ebb", // bullet_kin_cowboy
            "906d71ccc1934c02a6f4ff2e9c07c9ec", // bullet_kin_officetie
            "9eba44a0ea6c4ea386ff02286dd0e6bd", // bullet_kin_officesuit
            "2b6854c0849b4b8fb98eb15519d7db1c", // bullet_kin_mech
            "05cb719e0178478685dc610f8b3e8bfc", // bullet_kin_vest
        };


        public static readonly List<string> PotCritterGUIDList = new List<string>() {
            "6ad1cafc268f4214a101dca7af61bc91", // rat
            "14ea47ff46b54bb4a98f91ffcffb656d", // rat_candle
            "76bc43539fc24648bff4568c75c686d1", // chicken
            "1386da0f42fb4bcabc5be8feb16a7c38", // snake
            "95ea1a31fc9e4415a5f271b9aedf9b15", // robots_past_critter_1
            "42432592685e47c9941e339879379d3a", // robots_past_critter_2
            "4254a93fc3c84c0dbe0a8f0dddf48a5a" // robots_past_critter_3
        };

        
        public static readonly string BulletKinEnemyGUID = "01972dee89fc4404a5c408d50007dad5";
        public static readonly string LeadMaidenEnemyGUID = "cd4a4b7f612a4ba9a720b9f97c52f38c";
        public static readonly string tombstonerEnemyGUID = "cf27dd464a504a428d87a8b2560ad40a";
        public static readonly string poisbuloidEnemyGUID = "fe3fe59d867347839824d5d9ae87f244";
        public static readonly string skusketHeadEnemyGUID = "c2f902b7cbe745efb3db4399927eab34";
        public static readonly string fungunEnemyGUID = "f905765488874846b7ff257ff81d6d0c";
        // public static readonly string chancekinEnemyGUID = "a446c626b56d4166915a4e29869737fd";
        public static readonly string snakeGUID = "1386da0f42fb4bcabc5be8feb16a7c38";
        // public static readonly string wallmongerGUID = "f3b04a067a65492f8b279130323b41f0";

        /*public static readonly AIActor BulletKinGUID = EnemyDatabase.GetOrLoadByGuid(BulletKinEnemyGUID);
        public static readonly AIActor TombstonerGUID = EnemyDatabase.GetOrLoadByGuid(tombstonerEnemyGUID);
        public static readonly AIActor PoisbuloidGUID = EnemyDatabase.GetOrLoadByGuid(poisbuloidEnemyGUID);
        public static readonly AIActor skusketHeadGUID = EnemyDatabase.GetOrLoadByGuid(skusketHeadEnemyGUID);
        public static readonly AIActor fungunGUID = EnemyDatabase.GetOrLoadByGuid(fungunEnemyGUID);*/
        // public static readonly AIActor chancekinGUID = EnemyDatabase.GetOrLoadByGuid(chancekinEnemyGUID);

        public static readonly List<int> CastleWallIDs = new List<int>() {
            22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
            34, 35, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53,
            54, 55, 56, 57, 58, 59, 60, 61, 62, 73, 74, 75,
            76, 80, 81, 82, 83, 95, 96, 97, 98, 176, 332,
            333, 334, 354, 355, 356
        };
        public static readonly List<int> CastleFloorIDs = new List<int>() {
            90, 91, 92, 110, 111, 112, 113, 114, 115, 116, 129, 130,
            132, 133, 134, 135, 136, 137, 138, 201, 274, 296, 297,
            318, 319, 340, 341, 362, 363, 370, 384, 385, 386, 387,
            406, 407, 408, 409, 412, 414, 415, 416, 428, 429, 430,
            431, 434, 436, 438, 450, 451, 452, 453, 454, 455, 456,
            458, 459, 460, 472, 473, 474, 475, 476, 477, 478, 494,
            496, 497, 498, 516, 517, 518, 519, 520, 538, 539, 540,
            560, 561, 562, 582
        };
        public static readonly List<int> CastleMiscIDs = new List<int>() {
            107, 118, 119, 120, 121, 122, 140, 142, 143, 144,
            151, 162, 163, 164, 185, 186, 187, 202, 203, 204,
            205, 206, 207, 208, 209, 210, 211, 220, 224, 226,
            227, 228, 229, 231, 232, 233, 246, 247, 248, 249,
            250, 251, 252, 253, 254, 266, 267, 286, 287, 291,
            292, 293, 294, 308, 309, 310, 311, 313, 314, 315,
            316, 330, 331, 335, 336, 352, 353, 357, 358, 374,
            375, 376, 377, 379, 380, 381, 382, 396, 397, 398,
            399, 401, 402, 403, 404, 418, 419, 420, 421, 423,
            424, 425, 426, 440, 441, 442, 443, 445, 446, 447,
            448, 462, 464, 465, 466, 467, 469, 470, 471, 484,
            485, 486, 487, 488, 489, 490, 491, 492, 493, 506,
            507, 508, 509, 510, 511, 512, 513, 514, 515, 528,
            533, 550, 555, 572, 577, 594, 599
        };


        public static readonly List<int> GungeonWallIDs = new List<int>() {
            22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 44, 45,
            46, 47, 48, 49, 50, 51, 52, 54, 55, 128, 129, 130, 146,
            155, 156, 157, 304, 333, 334, 335, 340, 355, 356, 357,
            362
        };
        public static readonly List<int> GungeonFloorIDs = new List<int>() { 73, 76, 77, 82, 83, 90, 91, 93, 95, 113, 122, 123, 124, 125, 126, };
        public static readonly List<int> GungeonMiscIDs = new List<int>() {
            57, 58, 59, 60, 61, 66, 67, 68, 69, 70, 71, 72, 74,
            79, 80, 81, 88, 89, 92, 94, 96, 98, 99, 101, 102,
            103, 104, 105, 106, 110, 111, 112, 114, 115, 116,
            117, 118, 119, 120, 121, 127, 132, 133, 134, 135,
            136, 137, 138, 139, 140, 141, 144, 145, 147, 148,
            149, 150, 151, 152, 154, 158, 161, 162, 163, 166,
            167, 168, 169, 170, 171, 172, 173, 174, 176, 177,
            178, 179, 185, 186, 187, 193, 194, 195, 196, 198,
            199, 200, 201, 202, 203, 204, 205, 206, 207, 208,
            209, 210, 211, 212, 213, 214, 215, 216, 222, 223,
            224, 225, 226, 227, 228, 229, 231, 232, 233, 234,
            235, 236, 237, 238, 244, 245, 246, 247, 248, 249,
            250, 251, 252, 253, 256, 257, 258, 264, 265, 266,
            267, 268, 269, 270, 271, 272, 273, 274, 281, 286,
            287, 288, 289, 290, 292, 293, 294, 295, 298, 299,
            303, 308, 309, 310, 311, 312, 313, 314, 317, 320,
            321, 325, 326, 330, 331, 332, 336, 337, 338, 341,
            342, 343, 347, 348, 352, 353, 354, 358, 359, 360,
            363, 364, 365, 369, 370, 374, 375, 376, 377, 378,
            379, 380, 381, 382, 386, 387, 388, 389, 391, 392,
            393, 394, 396, 397, 398, 399, 400, 401, 402, 404,
            408, 409, 410, 411, 413, 414, 415, 416, 418, 419,
            420, 421, 422, 423, 424, 425, 426, 430, 431, 432,
            433, 435, 436, 437, 438, 440, 441, 442, 443, 444,
            445, 446, 447, 452, 453, 454, 455, 457, 458, 459,
            460, 462, 463, 464, 465, 466, 467, 468, 469, 474,
            475, 477, 478, 479, 480, 481, 482, 483, 484, 485,
            486, 487, 488, 489, 496, 497, 498, 499, 500, 501,
            503, 504, 505, 506, 507, 508, 509, 510, 518, 519,
            520, 521, 522, 523, 524, 525, 526, 527, 528, 540,
            545, 546, 547, 550, 562, 567, 568, 569, 572, 573,
            584, 589, 594, 606, 628
        };

        public static readonly List<int> MinesWallIDs = new List<int>() {
            22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34,
            35, 36, 37, 38, 39, 44, 45, 46, 47, 48, 49, 50, 51,
            52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 150, 151,
            172, 173, 203, 213, 246, 247, 248, 249, 250, 259,
            260, 268, 270, 271, 272, 290, 291, 292, 332, 333,
            334, 335, 354, 355, 356, 357, 413, 414
        };
        public static readonly List<int> MinesFloorIDs = new List<int>() {
            80, 81, 82, 83, 84, 88, 89, 91, 92, 93, 94, 95, 96,
            97, 98, 99, 100, 102, 103, 104, 105, 106, 108, 109,
            110, 111, 112, 114, 115, 116, 117, 120, 121, 122,
            124, 125, 126, 127, 128, 129, 130, 131, 132, 133,
            154, 155, 176, 177, 198, 253, 254, 255, 256, 257,
            258, 261, 262, 263, 264, 269, 275, 276, 277, 278,
            279, 280, 281, 282, 283, 284, 285, 303, 304, 305,
            306, 307, 312, 313, 314, 315, 316, 320, 321, 324,
            325, 326, 327, 328, 330, 331, 337, 338, 342, 343,
            345, 346, 347, 348, 349, 350, 351, 352, 353, 359,
            360, 364, 365, 367, 368, 369, 370, 371, 372, 373,
            374, 375, 376, 377, 381, 382, 383, 384, 386, 387,
            388, 389, 391, 393, 394, 395, 396, 397, 398, 399,
            403, 404, 405, 406, 408, 409, 410, 411, 415, 416,
            417, 418, 419, 420, 421, 422, 423, 424, 435, 436,
            444, 445, 446, 457, 458, 479, 480, 501, 502, 503,
            504, 523, 524, 525, 526, 545, 546, 547, 548, 567,
            568, 569, 570, 589, 590, 592, 593, 611, 612, 613,
            614, 615, 633, 634, 635, 636, 637, 655, 656, 657,
            677, 678, 679, 699
        };
        public static readonly List<int> MinesMiscIDs = new List<int>() {
            66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78,
            156, 157, 178, 179, 180, 181, 182, 183, 184, 185,
            186, 187, 188, 189, 190, 191, 192, 193, 194, 200,
            201, 202, 204, 205, 206, 207, 209, 210, 211, 212,
            214, 215, 216, 222, 223, 224, 225, 226, 227, 228,
            229, 230, 231, 234, 235, 236, 237, 238, 244, 245,
            266, 267, 286, 287, 288, 289, 293, 294, 298, 299,
            308, 309, 310, 311, 425, 426, 427, 428, 430, 431,
            432, 433, 437, 438, 439, 440, 441, 442, 443, 447,
            448, 449, 450, 452, 453, 454, 455, 459, 460, 461,
            462, 463, 465, 466, 467, 468, 469, 470, 472, 473,
            474, 475, 477, 478, 481, 482, 483, 484, 485, 486,
            487, 488, 489, 490, 491, 492, 494, 495, 496, 497,
            499, 500, 506, 507, 508, 509, 510, 513, 514, 515,
            516, 517, 518, 519, 520, 521, 522, 528, 529, 535,
            536, 540, 541, 550, 557, 562, 572, 579, 584, 594,
            601, 606
        };

        public static readonly List<int> HollowsWallIDs = new List<int>() {
            22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35,
            36, 37, 38, 39, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53,
            54, 55, 56, 57, 58, 59, 60, 61, 102, 103, 127, 128,
            149, 150, 151, 152, 332, 333, 354, 355, 382, 383
        };

        public static List<int> HollowsFloorIDs = new List<int>() {
            66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 80,
            81, 82, 83, 84, 85, 88, 89, 90, 91, 92, 93, 94, 95, 96,
            97, 98, 99, 100, 101, 104, 105, 106, 107, 110, 111, 112,
            113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123,
            124, 125, 126, 129, 130, 132, 133, 134, 135, 136, 137,
            138, 139, 141, 142, 143, 144, 145, 146, 147, 148, 154,
            155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165,
            166, 167, 168, 169, 170, 176, 177, 178, 179, 180, 181,
            182, 183, 184, 185, 186, 187, 193, 194, 195, 196, 197,
            198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208,
            209, 213, 214, 215, 216, 217, 218, 219, 222, 223, 224,
            225, 226, 227, 228, 229, 235, 236, 237, 238, 239, 240,
            241, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253,
            257, 258, 259, 260, 261, 266, 267, 268, 269, 270, 271,
            272, 273, 274, 275, 279, 280, 281, 282, 283, 284, 286,
            288, 289, 290, 291, 292, 298, 301, 302, 303, 305, 306,
            307, 320, 321, 327, 328, 329, 342, 343, 349, 350, 351,
            364, 365, 371, 372, 373, 386, 387, 395, 408, 409, 410,
            411, 417, 430, 431, 432, 433, 452, 453, 454, 455, 474,
            475, 476, 477, 496, 497, 498, 499, 500, 518, 519, 520,
            521, 522, 540, 541, 542, 548, 549, 562, 570, 571, 584,
            591, 592, 593, 606, 613, 614, 615
        };

        public static readonly List<int> HollowsMiscIDs = new List<int>() {
            188, 189, 190, 210, 212, 232, 233, 234, 294, 295, 309,
            314, 315, 316, 317, 325, 326, 331, 336, 337, 338, 339,
            347, 348, 353, 358, 359, 360, 361, 369, 370, 375, 380,
            381, 391, 392, 396, 397, 398, 399, 402, 403, 404, 405,
            413, 414, 415, 416, 418, 419, 420, 421, 424, 425, 426,
            427, 435, 436, 437, 438, 440, 441, 442, 443, 446, 447,
            448, 449, 457, 458, 459, 460, 462, 463, 464, 465, 468,
            469, 470, 471, 479, 480, 481, 482, 484, 490, 491, 492,
            493, 494, 501, 502, 503, 504, 505, 506, 512, 513, 514,
            515, 516, 523, 524, 525, 526, 527, 528, 534, 535, 536,
            545, 546, 547, 550, 556, 557, 567, 568, 572, 578, 589,
            594, 600, 611, 622, 633
        };

        public static readonly List<int> ForgeWallIDs = new List<int>() {
            22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34,
            35, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55
        };

        public static readonly List<int> ForgeFloorIDs = new List<int>() {
            56, 57, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 78,
            79, 80, 88, 89, 90, 91, 92, 93, 94, 100, 101, 102,
            103, 104, 105, 106, 110, 111, 112, 113, 114, 115, 120,
            121, 122, 123, 124, 125, 126, 127, 128, 132, 133, 134,
            135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145,
            146, 147, 148, 149, 150, 151, 154, 155, 156, 157, 158,
            159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 171,
            172, 173, 178, 179, 180, 181, 182, 183, 184, 185, 186,
            187, 188, 189, 190, 193, 194, 195, 200, 201, 202, 203,
            204, 213, 222, 223, 224, 225, 226, 244, 245, 246, 247,
            248, 280, 281, 282, 283, 284, 286, 287, 288, 289, 296,
            301, 302, 303, 304, 305, 306, 318, 319, 323, 324, 340,
            341, 345, 346, 362, 363, 367, 368, 384, 385, 389, 390,
            406, 407, 408, 409, 411, 412, 413, 414, 428, 429, 430,
            431, 433, 434, 435, 436, 450, 451, 452, 453, 455, 456,
            457, 458, 472, 473, 474, 475, 477, 478, 479, 480, 494,
            495, 497, 498, 499, 500, 502, 503, 516, 517, 518, 519,
            520, 521, 522, 523, 524, 525, 538, 539, 540, 541, 542,
            543, 544, 545, 546, 547, 560, 565, 582, 587, 604, 609,
        };

        public static readonly List<int> ForgeMiscIDs = new List<int>() {
            215, 216, 217, 218, 219, 237, 239, 240, 241, 259, 260,
            261, 262, 263, 308, 309, 313, 314, 330, 331, 335, 336,
            352, 353, 357, 358, 374, 375, 379, 380, 396, 397, 398,
            399, 401, 402, 403, 404, 418, 419, 420, 421, 423, 424,
            425, 426, 440, 441, 442, 443, 445, 446, 447, 448, 462,
            463, 464, 465, 467, 468, 469, 470, 484, 485, 487, 488,
            489, 490, 492, 493, 506, 507, 508, 509, 510, 511, 512,
            513, 514, 515, 528, 529, 530, 531, 532, 533, 534, 535,
            536, 537, 550, 555, 572, 577, 594, 599, 616, 621
        };

        public static readonly List<int> SewerWallIDs = new List<int>() {
            22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 44,
            45, 46, 47, 48, 49, 50, 51, 52, 333, 334, 335, 355,
            356, 357, 380, 381
        };
        public static readonly List<int> SewerFloorIDs = new List<int>() {
            53, 54, 55, 66, 67, 68, 69, 70, 88, 89, 90, 91, 95,
            110, 111, 112, 114, 115, 116, 117, 119, 120, 121, 124,
            125, 126, 127, 128, 136, 137, 138, 139, 141, 142, 143,
            146, 147, 148, 149, 150, 163, 164, 165, 168, 169, 170,
            171, 172, 177, 179, 180, 181, 182, 185, 186, 187, 188,
            189, 199, 200, 201, 202, 203, 204, 207, 208, 209, 210,
            211, 221, 222, 223, 224, 225, 229, 230, 231, 232, 233,
            244, 245, 246, 247, 264
        };
        public static readonly List<int> SewerMiscIDs = new List<int>() {
            286, 287, 288, 298, 299, 308, 309, 310, 320, 321,
            330, 331, 332, 342, 343, 352, 353, 354, 364, 365,
            374, 375, 376, 377, 386, 387, 388, 389, 396, 397,
            398, 399, 408, 409, 410, 411, 418, 419, 420, 421,
            430, 431, 432, 433, 440, 441, 442, 443, 452, 453,
            454, 455, 462, 463, 465, 466, 474, 475, 477, 478,
            484, 485, 486, 487, 488, 496, 497, 498, 499, 500,
            506, 507, 508, 509, 510, 518, 519, 520, 521, 522,
            528, 540, 550, 562, 572, 584, 594
        };


        public static readonly List<int> AbbeyWallIDs = new List<int>() {
            22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34,
            35, 36, 37, 38, 44, 45, 46, 47, 48, 49, 50, 51, 52,
            53, 54, 55, 56, 57, 333, 334, 335, 355, 356, 357
        };
        public static readonly List<int> AbbeyFloorIDs = new List<int>() {
            58, 59, 60, 88, 89, 90, 91, 92, 93, 94, 110, 111, 112,
            113, 115, 116, 124, 125, 126, 127, 132, 133, 134, 137,
            138, 146, 147, 148, 149, 155, 156, 168, 169, 170, 171,
            176, 177, 178, 179, 198, 199, 200, 201, 202, 203, 204,
            205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215,
            216, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229,
            231, 232, 233, 234, 235, 236, 237, 238, 242, 243, 244,
            245, 246, 247, 248, 251, 252, 253, 256, 257, 258, 259,
            260, 264, 268, 269, 270, 271, 272, 273, 274, 275, 276,
            277, 278, 279, 280, 281, 282, 290, 291, 292, 293, 294,
            295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 312,
            313, 314, 317, 318, 319, 320, 321, 322, 323, 324, 339,
            340, 341, 361, 362, 363
        };
        public static readonly List<int> AbbeyMiscIDs = new List<int>() {
            286, 287, 288, 308, 309, 310, 330, 331, 352, 353, 374,
            375, 376, 377, 396, 397, 398, 399, 418, 419, 420, 421,
            440, 441, 442, 443, 445, 446, 462, 463, 465, 466, 484,
            485, 486, 487, 488, 489, 490, 506, 507, 508, 509, 510,
            511, 512, 528, 529, 530, 531, 550, 551, 552, 553, 572,
            594
        };

        public static readonly List<int> RatDenWallIDs = new List<int>() { 55, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 44, 45, 46, 48, 49, 50, 52, 53, 54 };
        public static readonly List<int> RatDenFloorIDs = new List<int>() {
            47, 51, 56, 66, 67, 68, 69, 70, 71, 72, 73, 74, 89, 90,
            91, 92, 93, 94, 95, 96, 111, 112, 113, 114, 115, 116, 133,
            134, 135, 136, 137, 138, 155, 156, 157, 158, 159, 160, 176,
            198, 201, 202, 203, 204, 205, 206, 223, 224, 226, 227, 228,
            246, 247, 248, 249, 250, 264
        };
        public static readonly List<int> RatDenMiscIDs = new List<int>() { 286, 287, 288, 289, 290, 292, 308, 309, 310, 311, 312, 313, 314, 315 };

        public static readonly List<int> Nakatomi_OfficeWallIDs = new List<int>() {
            22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35,
            36, 37, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55,
            56, 57, 58, 59
        };
        public static readonly List<int> Nakatomi_OfficeFloorIDs = new List<int>() {
            66, 67, 68, 69, 70, 71, 72, 73, 74, 79, 81, 82, 83, 84,
            85, 88, 89, 90, 91, 92, 93, 94, 95, 96, 98, 99, 100, 103,
            104, 105, 106, 107, 112, 113, 114, 115, 116, 117, 118,
            119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 134,
            135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145,
            146, 147, 148, 149, 160, 161, 164, 165, 166, 167, 168,
            177, 178, 179, 180, 182, 183, 186, 187, 188, 189, 190,
            198, 199, 200, 201, 202, 208, 209, 210, 230, 231, 232,
            233, 252, 253, 254, 255, 256, 274, 275, 276, 277, 278
        };
        public static readonly List<int> Nakatomi_OfficeMiscIDs = new List<int>() {
            60, 220, 221, 242, 243, 264, 265, 286, 287, 308, 309,
            310, 311, 330, 331, 332, 333, 352, 353, 354, 355, 374,
            375, 376, 377, 396, 397, 399, 400, 418, 419, 420, 421,
            422, 440, 441, 442, 443, 444, 462, 484, 506, 528
        };
        public static readonly List<int> Nakatomi_FutureWallIDs = new List<int>() {
            22, 23, 24, 25, 26, 27, 28, 30, 31, 32, 33, 34, 35, 36, 37, 38,
            40, 44, 45, 46, 47, 48, 49, 50, 52, 53, 54, 55, 56, 57, 58, 59,
            60, 332, 333, 378, 379, 400, 401
        };
        public static readonly List<int> Nakatomi_FutureFloorIDs = new List<int>() {
            66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81,
            88, 91, 92, 93, 94, 96, 98, 99, 113, 114, 135, 136, 154, 155,
            156, 177, 199, 221, 264
        };
        public static readonly List<int> Nakatomi_FutureMiscIDs = new List<int>() {
            286, 287, 288, 289, 290, 291, 298, 299, 300, 301, 308, 309,
            310, 311, 312, 313, 320, 321, 322, 323, 330, 331, 342, 343,
            352, 353, 364, 365, 374, 375, 376, 377, 386, 387, 388, 389,
            396, 397, 398, 399, 408, 409, 410, 411, 418, 419, 420, 421,
            430, 431, 432, 433, 440, 441, 442, 443, 452, 453, 454, 455,
            462, 463, 465, 466, 474, 475, 477, 478, 484, 485, 486, 487,
            488, 496, 497, 498, 499, 500, 506, 507, 508, 509, 510, 518,
            519, 520, 521, 522, 528, 540, 550, 562, 572, 584, 594
        };

        public static readonly List<int> BulletHell_WallIDs = new List<int>() {
            22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36,
            37, 38, 39, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55,
            56, 57, 58, 59, 60, 61, 398, 399, 404, 405, 420, 421
        };
        public static readonly List<int> BulletHell_FloorIDs = new List<int>() {
            66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80,
            81, 82, 83, 84, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98,
            99, 100, 101, 102, 103, 106, 107, 108, 109, 113, 114, 115,
            116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 128,
            129, 130, 131, 132, 133, 134, 141, 142, 143, 144, 145, 146,
            152, 153, 154, 155, 159, 160, 161, 162, 163, 164, 165, 166,
            167, 168, 174, 175, 176, 177, 178, 179, 181, 182, 183, 184,
            185, 186, 187, 188, 189, 190, 194, 195, 198, 199, 200, 201,
            203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 216, 217,
            218, 219, 220, 221, 222, 223, 225, 226, 227, 228, 229, 230,
            231, 232, 233, 234, 235, 238, 239, 240, 241, 247, 248, 249,
            250, 251, 252, 253, 254, 255, 256, 257, 260, 261, 262, 263,
            269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279, 282,
            283, 284, 285, 287, 288, 291, 292, 293, 296, 297, 298, 299,
            304, 305, 306, 307, 309, 310, 318, 319, 330, 346
        };
        public static readonly List<int> BulletHell_MiscIDs = new List<int>() {
            352, 353, 357, 358, 360, 361, 363, 364, 368, 369, 374, 375,
            379, 380, 382, 383, 385, 386, 390, 391, 396, 397, 401, 402,
            407, 408, 412, 413, 418, 419, 423, 424, 429, 430, 434, 435,
            440, 441, 442, 443, 445, 446, 447, 448, 451, 452, 453, 454,
            456, 457, 458, 459, 462, 463, 464, 465, 467, 468, 469, 470,
            473, 474, 475, 476, 478, 479, 480, 481, 484, 485, 486, 487,
            489, 490, 491, 492, 495, 496, 497, 498, 500, 501, 502, 503,
            506, 507, 508, 509, 511, 512, 513, 514, 517, 518, 519, 520,
            522, 523, 524, 525, 528, 529, 531, 532, 533, 534, 536, 537,
            539, 540, 542, 543, 544, 545, 547, 548, 550, 551, 552, 553,
            554, 555, 556, 557, 558, 559, 561, 562, 563, 564, 565, 566,
            567, 568, 569, 570, 572, 573, 574, 575, 576, 577, 578, 579,
            580, 581, 583, 584, 585, 586, 587, 588, 589, 590, 591, 592,
            594, 599, 605, 610, 616, 621, 627, 632, 638, 643, 649, 654,
            660, 665, 671
        };

        public static readonly List<int> AllowedMimicBossWeapons = new List<int>() {
            0, // magic_lamp
            1, // winchester
            2, // thompson
            3, // screecher
            4, // sticky_crossbow
            5, // awp
            6, // zorgun
            7, // barrel
            8, // bow
            9, // dueling_pistol
            10, // mega_douser
            12, // crossbow
            13, // thunderclap
            14, // bee_hive
            15, // ak47
            16, // yari_launcher
            17, // heck_blaster
            18, // blooper
            19, // grenade_launcher
            20, // moonscraper
            21, // bsg
            22, // shades_revolver
            23, // dungeon_eagle
            24, // dart_gun
            25, // m1
            26, // nail_gun
            30, // m1911
            31, // klobbe
            32, // void_marshal
            33, // tear_jerker
            35, // smileys_revolver
            36, // megahand
            37, // serious_cannon
            38, // magnum
            39, // rpg
            40, // freeze_ray
            41, // heroine
            42, // trank_gun
            43, // machine_pistol
            47, // jolter
            49, // sniper_rifle
            50, // saa
            51, // regular_shotgun
            52, // crescent_crossbow
            53, // au_gun
            54, // laser_rifle
            55, // void_shotgun
            56, // 38_special
            57, // alien_sidearm
            58, // void_core_assault_rifle
            59, // hegemony_rifle
            60, // demon_head
            61, // bundle_of_wands
            62, // colt_1851
            76, // eye_jewel
            79, // makarov
            80, // budget_revolver
            81, // deck4rd
            82, // elephant_gun
            83, // unfinished_gun
            84, // vulcan_cannon
            86, // marine_sidearm
            87, // gamma_ray
            88, // robots_right_hand
            89, // rogue_special
            90, // eye_of_the_beholster
            91, // h4mmer
            92, // stinger
            93, // old_goldie
            94, // mac10
            95, // akey47
            96, // m16
            97, // polaris
            99, // rusty_sidearm
            100, // unicorn_horn
            121, // disintegrator
            123, // pulse_cannon
            124, // cactus
            125, // flame_hand
            126, // shotbow
            128, // rube_adyne_mk2
            129, // com4nd0
            130, // glacier
            142, // rube_adyne_prototype
            143, // shotgun_full_of_hate
            145, // witch_pistol
            146, // dragunfire
            149, // face_melter
            150, // t_shirt_cannon
            151, // the_membrane
            152, // the_kiln
            154, // trashcannon
            156, // laser_lotus
            157, // big_iron
            175, // tangler
            176, // gungeon_ant
            177, // alien_engine
            178, // crestfaller
            179, // proton_backpack
            180, // grasschopper
            181, // winchester_rifle
            182, // grey_mauser
            183, // ser_manuels_revolver
            186, // machine_fist 
            196, // fossilized_gun
            197, // pea_shooter
            198, // gunslingers_ashes
            199, // luxin_cannon
            200, // charmed_bow
            202, // sawed_off
            207, // plague_pistol
            208, // plunger
            210, // gunbow
            221, // tutorial_ak47
            223, // cold_45
            227, // wristbow
            // 228, // particulator
            229, // hegemony_carbine
            230, // helix
            231, // gilded_hydra
            251, // prize_pistol
            274, // dark_marker
            275, // flare_gun
            292, // molotov_launcher
            296, // yari_launcher_dupe_1
            299, // super_space_turtles_gun
            327, // corsair
            328, // charge_shot
            329, // zilla_shotgun
            330, // the_emperor
            331, // science_cannon
            332, // lil_bomber
            334, // wind_up_gun
            335, // silencer
            336, // pitchfork
            338, // gunther
            339, // mahoguny
            340, // lower_case_r
            341, // buzzkill
            345, // fightsabre
            346, // huntsman
            347, // shotgrub
            355, // chromesteel_assault_rifle
            358, // railgun
            359, // compressed_air_tank
            360, // snakemaker
            363, // trick_gun
            365, // mass_shotgun
            366, // molotov
            369, // bait_launcher
            370, // prototype_railgun
            372, // rc_rocket
            376, // brick_breaker
            377, // excaliber
            378, // derringer
            379, // shotgun_full_of_love
            380, // betrayers_shield
            381, // triple_crossbow
            382, // sling
            383, // flash_ray
            384, // phoenix
            385, // hexagun
            387, // frost_giant
            393, // anvillain
            394, // mine_cutter
            395, // staff_of_firepower
            401, // gungine
            402, // snowballer
            404, // siren
            406, // rattler
            413, // heros_sword
            444, // trident
            445, // the_scrambler
            464, // shellegun
            // 472, // gummy_gun
            475, // quad_laser
            476, // microtransaction_gun
            477, // origuni
            478, // banana
            479, // super_meat_gun
            482, // gunzheng
            483, // tetrominator
            484, // devolver
            486, // treadnaught_cannon
            497, // yari_launcher_dupe_2
            501, // yari_launcher_dupe_3
            503, // bullet
            504, // hyper_light_blaster
            505, // huntsman_dupe_1
            506, // really_special_lute
            507, // starpew
            508, // dueling_laser
            510, // jk47
            511, // 3rd_party_controller
            512, // shell
            514, // directional_pad
            516, // triple_gun
            519, // combined_rifle
            535, // bow_dupe_1
            537, // vorpal_gun
            539, // boxing_glove
            540, // glass_cannon
            541, // casey
            542, // strafe_gun
            543, // the_predator
            545, // ac15
            546, // windgunner
            550, // knights_gun
            551, // crown_of_guns
            562, // the_fat_line
            563, // the_exotic
            566, // rad_gun
            576, // robots_left_hand
            577, // turbo_gun
            594, // moonlight_tiara
            597, // mr_accretion_jr
            599, // bubble_blaster
            601, // big_shotgun
            602, // gunner
            603, // lamey_gun
            604, // slinger
            609, // rube_adyne+rubensteins_monster
            610, // wood_beam
            611, // ak47+island_forme
            612, // heroine+wave_beam
            613, // heroine+ice_beam
            614, // heroine+plasma_beam
            615, // heroine+hyber_beam
            616, // casey+careful_iteration
            617, // megahand+quick_boomerang
            618, // megahand+time_stopper
            619, // megahand+metal_blade
            620, // megahand+leaf_shield
            621, // megahand+atomic_fire
            622, // megahand+bubble_lead
            623, // megahand+air_shooter
            624, // megahand+crash_bomber
            626, // elimentaler
            647, // chamber_gun
            648, // lower_case_r_dupe_1
            649, // uppercase_r
            651, // rogue_special_dupe_1
            652, // budget_revolver_dupe_1
            657, // flash_ray_dupe_1
            658, // proton_backpack_dupe_1
            659, // the_exotic_dupe_1
            660, // regular_shotgun_dupe_1
            668, // enemy_elimentaler
            670, // high_dragunfire
            671, // gamma_ray+beta_ray
            672, // elephant_gun+the_elephant_in_the_room
            673, // machine_pistol+pistol_machine
            674, // pea_shooter+pea_cannon
            675, // dueling_pistol+dualing_pistol
            676, // laser_rifle+laser_light_show
            677, // dragunfire+kalibreath
            679, // snowballer+snowball_shotgun
            680, // excaliber+armored_corps
            681, // 38_special+unknown
            682, // plague_pistol+pandemic_pistol
            683, // thunderclap+alistairs_ladder
            684, // m1+m1_multi_tool
            685, // thompson+future_gangster
            686, // corsair+black_flag
            687, // crestfaller+five_oclock_somewhere
            688, // banana+fruits_and_vegetables
            690, // klobbe+klobbering_time
            691, // molotov_launcher+special_reserve
            692, // nail_gun+nailed_it
            693, // gunbow+show_across_the_bow
            694, // big_iron+iron_slug
            695, // hyper_light_blaster+hard_light
            696, // alien_sidearm+chief_master
            698, // flame_hand+maximize_spell
            699, // hegemony_rifle+hegemony_special_forces
            700, // cactus+cactus_flower
            701, // luxin_cannon+noxin_cannon
            702, // face_melter+alternative_rock
            703, // bee_hive+apiary
            704, // trashcannon+recycling_bin
            705, // flash_ray+savior_of_the_universe
            706, // flare_gun+firing_with_flair
            707, // vulcan_cannon+not_quite_as_mini
            708, // helix+double_double_helix
            709, // barrel+like_shooting_fish
            710, // freeze_ray+ice_cap
            711, // light_gun+peripheral_vision
            713, // moonscraper+double_moon_7
            714, // laser_lotus+lotus_bloom
            715, // h4mmer+hammer_and_nail
            716, // awp+arctic_warfare
            718, // polaris+square_brace
            719, // lil_bomber+king_bomber
            720, // proton_backpack+electron_pack
            721, // jolter+heavy_jolt
            722, // pitchfork+pitch_perfect
            723, // com4nd0+commammo_belt
            724, // hegemony_carbine+ruby_carbine
            725, // tear_jerker+unknown
            726, // akey47+akey_breaky
            732, // gunderfury_lv10
            733, // unknown_11
            734, // mimic_gun
            736, // phoenix+phoenix_up
            737, // betrayers_shield+betrayers_lies
            738, // lower_case_r+unknown
            739, // gungeon_ant+great_queen_ant
            740, // buzzkill+not_so_sawed_off
            741, // tear_jerker+wrath_of_the_blam
            742, // alien_engine+contrail
            743, // rad_gun+kung_fu_hippie_rappin_surfer
            744, // origuni+parchmental
            745, // ice_breaker+gunderlord
            747, // high_dragunfire+unknown
            748, // sunlight_javelin
            749, // shotbow+second_accident
            750, // dungeon_eagle+dont_hoot_the_messenger
            751, // magnum+unknown_synergy
            752, // smileys_revolver+unknown_synergy_1
            753, // smileys_revolver+unknown_synergy_2
            754, // smileys_revolver+unknown_synergy_3
            755, // evolver
            756, // shell+unknown_synergy_1
            757, // shell+unknown_synergy_2
            758, // shell+unknown_synergy_3
            759, // shell+unknown_synergy_4
            760, // shell+unknown_synergy_5
            761, // high_kaliber
            762, // finished_gun
            763, // regular_shotgun+unknown_synergy_1
            806, // unfinished_gun+unknown_synergy_2
            807, // unfinished_gun+unknown_synergy_3
            808, // the_exotic+unknown_synergy_1
            809, // marine_sidearm_alt
            810, // rusty_sidearm_alt
            811, // dart_gun_alt
            812, // robots_right_hand_alt
            816, // trank_gun_dupe_1
            819, // glass_cannon+steel_skin
            823 // wood_beam_dupe_1
        };
    }
}

