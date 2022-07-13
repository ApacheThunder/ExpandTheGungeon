using Dungeonator;
using System;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandRoomPrefabs {

        // Custom Room Prefabs
        public static PrototypeDungeonRoom Giant_Elevator_Room;
        // This room prefab was removed in 2.1.8. I will recreate in code now plus with a few extras of my own design.
        public static PrototypeDungeonRoom Utiliroom;
        public static PrototypeDungeonRoom Utiliroom_SpecialPit;
        public static PrototypeDungeonRoom Utiliroom_Pitfall;
        // Special Room Prefabs for secret glitch floor
        public static PrototypeDungeonRoom SecretRewardRoom;
        public static PrototypeDungeonRoom SecretBossRoom;
        public static PrototypeDungeonRoom FakeBossRoom;
        public static PrototypeDungeonRoom SecretExitRoom;
        public static PrototypeDungeonRoom PuzzleRoom1;
        public static PrototypeDungeonRoom PuzzleRoom2;
        public static PrototypeDungeonRoom PuzzleRoom3;
        
        
        // Special Rooms used under certain circumstances when Player picks up Corrupted Junk.
        public static PrototypeDungeonRoom CreepyGlitchRoom;
        public static PrototypeDungeonRoom CreepyGlitchRoom_Entrance;
        // General Purpose Boss Room for Gungeoneer Mimic Boss;
        public static PrototypeDungeonRoom GungeoneerMimicBossRoom;
        public static PrototypeDungeonRoom GungeoneerMimicBossFoyerRoom;

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
        public static PrototypeDungeonRoom Expand_Apache_Hub;
        public static PrototypeDungeonRoom Expand_Box_Hub;
        public static PrototypeDungeonRoom Expand_Enclose_Hub;

        // Special Jungle Entrance/Exit rooms for Floor 1
        public static PrototypeDungeonRoom Expand_Keep_TreeRoom;
        public static PrototypeDungeonRoom Expand_Keep_TreeRoom2;
        public static PrototypeDungeonRoom Expand_Keep_JungleElevatorRoom;

        // New Exit room for new Elevator object
        public static PrototypeDungeonRoom Expand_ExitRoom_NewElevator;


        // Rooms for floor 2.
        public static PrototypeDungeonRoom Expand_Crosshairs;
        public static PrototypeDungeonRoom Expand_Basic;
        public static PrototypeDungeonRoom Expand_JumpInThePit;
        public static PrototypeDungeonRoom Expand_LongSpikeTrap;
        public static PrototypeDungeonRoom Expand_SpikeTrap;
        public static PrototypeDungeonRoom Expand_ThinRoom;
        public static PrototypeDungeonRoom Expand_SniperRoom;
        public static PrototypeDungeonRoom Expand_TableRoom;

        // Rooms for floor 3.
        public static PrototypeDungeonRoom Expand_GoopTroop;
        public static PrototypeDungeonRoom Expand_HopScotch;
        public static PrototypeDungeonRoom Expand_Pit;
        public static PrototypeDungeonRoom Expand_Singer;
        public static PrototypeDungeonRoom Expand_TableRoom2;
        public static PrototypeDungeonRoom Expand_OilRoom;
        public static PrototypeDungeonRoom Expand_Walkway;

        // Rooms for floor 4.
        public static PrototypeDungeonRoom Expand_SpiderMaze;
        public static PrototypeDungeonRoom Expand_BlobRoom;
        // public static PrototypeDungeonRoom Expand_CubeRoom;
        public static PrototypeDungeonRoom Expand_HellInACell;
        public static PrototypeDungeonRoom Expand_IceIsNice;
        public static PrototypeDungeonRoom Expand_IceScotch;
        public static PrototypeDungeonRoom Expand_MrPresident;
        public static PrototypeDungeonRoom Expand_SawRoom;
        // RoomFactory Rooms
        public static PrototypeDungeonRoom Expand_Agony;
        public static PrototypeDungeonRoom Expand_ice1;
        public static PrototypeDungeonRoom Expand_Ice2;
        public static PrototypeDungeonRoom Expand_Ice3;
        public static PrototypeDungeonRoom Expand_Ice4;
        public static PrototypeDungeonRoom Expand_LargeMany;
        public static PrototypeDungeonRoom Expand_Roundabout;
        public static PrototypeDungeonRoom Expand_Shells;
        public static PrototypeDungeonRoom Expand_Spooky;
        public static PrototypeDungeonRoom Expand_Undead1;
        public static PrototypeDungeonRoom Expand_Undead2;
        public static PrototypeDungeonRoom Expand_Undead3;
        public static PrototypeDungeonRoom Expand_Undead4;
                
        // Rooms for floor 5.
        public static PrototypeDungeonRoom Expand_Arena;
        public static PrototypeDungeonRoom Expand_CaptainCrunch;
        public static PrototypeDungeonRoom Expand_CorridorOfDoom;
        public static PrototypeDungeonRoom Expand_FireRoom;
        public static PrototypeDungeonRoom Expand_Pits;
        public static PrototypeDungeonRoom Expand_SkullRoom;
        public static PrototypeDungeonRoom Expand_TableRoomAgain;

        // Rooms for Sewers
        public static PrototypeDungeonRoom Expand_4wave;
        public static PrototypeDungeonRoom Expand_Spiralbomb;
        public static PrototypeDungeonRoom Expand_Bat;
        public static PrototypeDungeonRoom Expand_Batsmall;
        public static PrototypeDungeonRoom Expand_BIRDS;
        public static PrototypeDungeonRoom Expand_Blobs;
        public static PrototypeDungeonRoom Expand_BoogalooFailure2;
        public static PrototypeDungeonRoom Expand_Chess;
        public static PrototypeDungeonRoom Expand_Cornerpits;
        public static PrototypeDungeonRoom Expand_Enclosed;
        public static PrototypeDungeonRoom Expand_Funky;
        public static PrototypeDungeonRoom Expand_Gapsniper;
        public static PrototypeDungeonRoom Expand_Hallway;
        public static PrototypeDungeonRoom Expand_HUB_1wave;
        public static PrototypeDungeonRoom Expand_Islands;
        public static PrototypeDungeonRoom Expand_Long;
        public static PrototypeDungeonRoom Expand_Mushroom;
        public static PrototypeDungeonRoom Expand_Mutant;
        public static PrototypeDungeonRoom Expand_Oddshroom;
        public static PrototypeDungeonRoom Expand_Pitzag;
        public static PrototypeDungeonRoom Expand_Shotgun;
        public static PrototypeDungeonRoom Expand_Smallcentral;


        // Custom Trap Rooms        
        public static PrototypeDungeonRoom ThwompCrossingVertical;
        public static PrototypeDungeonRoom ThwompCrossingHorizontal;
        public static PrototypeDungeonRoom Expand_Apache_FieldOfSaws;
        public static PrototypeDungeonRoom Expand_Apache_TheCrushZone;
        public static PrototypeDungeonRoom Expand_Apache_SpikeAndPits;
        public static PrototypeDungeonRoom Expand_Apache_PitTraps;
        public static PrototypeDungeonRoom Expand_Apache_RickRollChest;
        public static PrototypeDungeonRoom Expand_Apache_SurpriseChest;
        public static PrototypeDungeonRoom Expand_Apache_RainbowRoom;


        // Custom Secret Rooms
        public static PrototypeDungeonRoom Expand_TinySecret;
        public static PrototypeDungeonRoom Expand_TinySecretEmpty;
        public static PrototypeDungeonRoom Expand_GlitchedSecret;
        public static PrototypeDungeonRoom Expand_SecretElevatorEntranceRoom;
        public static PrototypeDungeonRoom Expand_RickRollSecret;
        public static PrototypeDungeonRoom Expand_Secret_Falsechest;

        // Custom Rooms for handling entrance to custom secret floor on Hollows
        public static PrototypeDungeonRoom SecretExitRoom2;
        public static PrototypeDungeonRoom SecretRatEntranceRoom;

        // General purpose destination room for "normal" version of custom elevator object.
        public static PrototypeDungeonRoom Expand_SecretElevatorDestinationRoom;

        // Misc Custom Rooms
        public static PrototypeDungeonRoom Expand_BootlegRoom;
        public static PrototypeDungeonRoom Bunny_Special4ShrineRoom; // Room from Bunnie's room pack integrated into mod to fix an issue with exit setup.

        // Unsorted Room Factory Rooms
        public static PrototypeDungeonRoom Expand_Secret_Brother10;
        public static PrototypeDungeonRoom Expand_Secret_Brother9;
        public static PrototypeDungeonRoom Expand_Secret_Brother8;
        public static PrototypeDungeonRoom Expand_Secret_Brother7;
        public static PrototypeDungeonRoom Expand_Secret_Brother6;
        public static PrototypeDungeonRoom Expand_Secret_Brother5;
        public static PrototypeDungeonRoom Expand_Secret_Brother4;
        public static PrototypeDungeonRoom Expand_Secret_Brother3;
        public static PrototypeDungeonRoom Expand_Secret_Brother2;
        public static PrototypeDungeonRoom Expand_Secret_Brother1;
        public static PrototypeDungeonRoom Expand_Secret_Witch10;
        public static PrototypeDungeonRoom Expand_Secret_Witch9;
        public static PrototypeDungeonRoom Expand_Secret_Witch8;
        public static PrototypeDungeonRoom Expand_Secret_Witch7;
        public static PrototypeDungeonRoom Expand_Secret_Witch6;
        public static PrototypeDungeonRoom Expand_Secret_Witch5;
        public static PrototypeDungeonRoom Expand_Secret_Witch4;
        public static PrototypeDungeonRoom Expand_Secret_Witch3;
        public static PrototypeDungeonRoom Expand_Secret_Witch2;
        public static PrototypeDungeonRoom Expand_Secret_Witch1;
        public static PrototypeDungeonRoom Expand_Secret_Pickups;
        public static PrototypeDungeonRoom Expand_Secret_Owl10;
        public static PrototypeDungeonRoom Expand_Secret_Owl9;
        public static PrototypeDungeonRoom Expand_Secret_Owl8;
        public static PrototypeDungeonRoom Expand_Secret_Owl7;
        public static PrototypeDungeonRoom Expand_Secret_Owl6;
        public static PrototypeDungeonRoom Expand_Secret_Owl5;
        public static PrototypeDungeonRoom Expand_Secret_Owl4;
        public static PrototypeDungeonRoom Expand_Secret_Owl3;
        public static PrototypeDungeonRoom Expand_Secret_Owl2;
        public static PrototypeDungeonRoom Expand_Secret_Owl1;
        public static PrototypeDungeonRoom Expand_Secret_Oldman10;
        public static PrototypeDungeonRoom Expand_Secret_Oldman9;
        public static PrototypeDungeonRoom Expand_Secret_Oldman8;
        public static PrototypeDungeonRoom Expand_Secret_Oldman7;
        public static PrototypeDungeonRoom Expand_Secret_Oldman6;
        public static PrototypeDungeonRoom Expand_Secret_Oldman5;
        public static PrototypeDungeonRoom Expand_Secret_Oldman4;
        public static PrototypeDungeonRoom Expand_Secret_Oldman3;
        public static PrototypeDungeonRoom Expand_Secret_Oldman2;
        public static PrototypeDungeonRoom Expand_Secret_Oldman1;
        public static PrototypeDungeonRoom Secret_Expand_reto;
        // public static PrototypeDungeonRoom Secret_Expand_logo;
        public static PrototypeDungeonRoom Secret_Expand_Achievement_The_Password;
        public static PrototypeDungeonRoom B_Expand_demon_onehand;
        public static PrototypeDungeonRoom B_Expand_Achievement_Rage_Mode;
        public static PrototypeDungeonRoom B_Expand_Achievement_Jammed;
        // public static PrototypeDungeonRoom Trap_Expand_Turtlemelon;
        public static PrototypeDungeonRoom PMO_Expand_Achievement_Demolition_Man;
        public static PrototypeDungeonRoom PMO_Expand_Achievement_Beep;
        public static PrototypeDungeonRoom PMHFOA_Expand_Achievement_Gun_Game;
        public static PrototypeDungeonRoom PMH_Expand_Kyle;
        public static PrototypeDungeonRoom PMH_Expand_Achievement_ReArmed;
        public static PrototypeDungeonRoom PMH_Expand_Achievement_Dat_Plat;
        public static PrototypeDungeonRoom PM_Expand_bubblebuddy;
        public static PrototypeDungeonRoom PM_Expand_Achievement_Squad_Captain;
        public static PrototypeDungeonRoom PM_Expand_Achievement_Reverence_for_the_Dead;
        public static PrototypeDungeonRoom PM_Expand_Achievement_Patron;
        public static PrototypeDungeonRoom PM_Expand_Achievement_Castle_Crasher;
        public static PrototypeDungeonRoom PM_Expand_Achievement_Case_Closed;
        public static PrototypeDungeonRoom P_Expand_MasterRound2;
        public static PrototypeDungeonRoom P_Expand_beholdter;
        public static PrototypeDungeonRoom O_Expand_Achievement_Sworn_Gun;
        public static PrototypeDungeonRoom O_Expand_Achievement_Pit_Lord;
        public static PrototypeDungeonRoom MHF_Expand_Achievement_Great_Hall;
        public static PrototypeDungeonRoom MH_Expand_TheSellCreep;
        public static PrototypeDungeonRoom MH_Expand_Achievement_Trickshot;
        public static PrototypeDungeonRoom MH_Expand_Achievement_Lead_God;
        public static PrototypeDungeonRoom MH_Expand_Achievement_Going_Down;
        public static PrototypeDungeonRoom MH_Expand_Achievement_Challenger;
        public static PrototypeDungeonRoom MFA_Expand_bigshotgun;
        public static PrototypeDungeonRoom MA_Expand_Achievement_Dungeon_Diver;
        public static PrototypeDungeonRoom MA_Expand_Achievement_Double_Jeopardy;
        public static PrototypeDungeonRoom M_Expand_Retrashed;
        public static PrototypeDungeonRoom M_Expand_ratkey;
        public static PrototypeDungeonRoom M_Expand_MasterRound3;
        public static PrototypeDungeonRoom M_Expand_ArcaneGunpowder;
        public static PrototypeDungeonRoom M_Expand_AK;
        public static PrototypeDungeonRoom M_Expand_Achievement_Rider;
        public static PrototypeDungeonRoom M_Expand_Achievement_Resourceful;
        public static PrototypeDungeonRoom M_Expand_Achievement_Mine_Master;
        public static PrototypeDungeonRoom KPMHFAO_Expand_Achievement_Woodsie_Lord;
        public static PrototypeDungeonRoom KPMHFAO_Expand_Achievement_Wingman;
        public static PrototypeDungeonRoom KPM_Expand_Achievement_Last_Stop;
        public static PrototypeDungeonRoom KP_Expand_hegemony;
        public static PrototypeDungeonRoom KP_Expand_Achievement_Not_Just_A_Box;
        public static PrototypeDungeonRoom KP_Expand_Achievement_Historian;
        public static PrototypeDungeonRoom K_Expand_Prime_Primer;
        public static PrototypeDungeonRoom K_Expand_MasterRound1;
        public static PrototypeDungeonRoom K_Expand_bonk;
        public static PrototypeDungeonRoom K_Expand_BigBird;
        public static PrototypeDungeonRoom K_Expand_Achievement_Money_Pit;
        public static PrototypeDungeonRoom K_Expand_Achievement_Hedge_Slinger;
        public static PrototypeDungeonRoom K_Expand_Achievement_Gungeon_Acolyte;
        public static PrototypeDungeonRoom K_Expand_Achievement_Biggest_Wallet;
        public static PrototypeDungeonRoom HFAB_Expand_Achievement_Forger;
        public static PrototypeDungeonRoom HF_Expand_Achievement_Weird_Tale;
        public static PrototypeDungeonRoom HF_Expand_Achievement_Cartographers_Assistant;
        public static PrototypeDungeonRoom HB_Expand_Achievement_Hollowed_Out;
        public static PrototypeDungeonRoom H_Expand_Planar_Lead;
        public static PrototypeDungeonRoom H_Expand_MasterRound4;
        public static PrototypeDungeonRoom H_Expand_Highpriest;
        public static PrototypeDungeonRoom H_Expand_Achievement_SledgeDog;
        public static PrototypeDungeonRoom H_Expand_Achievement_Exorcist;
        public static PrototypeDungeonRoom FB_Expand_Achievement_Time_Paradox;
        public static PrototypeDungeonRoom FB_Expand_Achievement_Lion_Leap;
        public static PrototypeDungeonRoom FB_Expand_Achievement_Gunsmith;
        public static PrototypeDungeonRoom FB_Expand_Achievement_Gungeon_Master;
        public static PrototypeDungeonRoom FB_Expand_Achievement_Day_Ruiner;
        public static PrototypeDungeonRoom F_Expand_Obsidian_Shell_Casing;
        public static PrototypeDungeonRoom F_Expand_MasterRound5;
        public static PrototypeDungeonRoom F_Expand_dragun;
        public static PrototypeDungeonRoom F_Expand_Achievement_Terminated;
        public static PrototypeDungeonRoom F_Expand_Achievement_Slayer;
        public static PrototypeDungeonRoom F_Expand_Achievement_I_Knew_Someone_Would_Do_It;
        public static PrototypeDungeonRoom F_Expand_Achievement_Advanced_Slayer;
        public static PrototypeDungeonRoom Expand_Proper_Regular1;
        public static PrototypeDungeonRoom Expand_Proper_Regular2;
        public static PrototypeDungeonRoom Expand_Proper_Regular3;
        public static PrototypeDungeonRoom Expand_Proper_Regular4;
        public static PrototypeDungeonRoom Expand_Proper_Regular5;
        public static PrototypeDungeonRoom Expand_Proper_Regular6;
        public static PrototypeDungeonRoom Expand_Proper_Regular7;
        public static PrototypeDungeonRoom Expand_Proper_Regular8;
        public static PrototypeDungeonRoom Expand_Proper_Regular9;
        public static PrototypeDungeonRoom Expand_Proper_Regular10;
        public static PrototypeDungeonRoom Expand_Proper_Regular11;
        public static PrototypeDungeonRoom Expand_Proper_Regular12;
        public static PrototypeDungeonRoom Expand_Proper_Regular13;
        public static PrototypeDungeonRoom Expand_Proper_Regular14;
        public static PrototypeDungeonRoom Expand_Proper_Regular15;
        public static PrototypeDungeonRoom Expand_Proper_Regular16;
        public static PrototypeDungeonRoom Expand_Proper_Regular17;
        public static PrototypeDungeonRoom Expand_Proper_Regular18;
        public static PrototypeDungeonRoom Expand_Proper_Regular19;
        public static PrototypeDungeonRoom Expand_Proper_Regular20;
        public static PrototypeDungeonRoom Expand_Proper_Regular21;
        public static PrototypeDungeonRoom Expand_Proper_Regular22;
        public static PrototypeDungeonRoom Expand_Proper_Regular23;
        public static PrototypeDungeonRoom Expand_Proper_Regular24;
        public static PrototypeDungeonRoom Expand_Proper_Regular25;
        public static PrototypeDungeonRoom Expand_Proper_Regular26;
        public static PrototypeDungeonRoom Expand_Proper_Regular27;
        public static PrototypeDungeonRoom Expand_Proper_Regular28;
        public static PrototypeDungeonRoom Expand_Proper_Regular29;
        public static PrototypeDungeonRoom Expand_Proper_Regular31;
        public static PrototypeDungeonRoom Expand_Proper_Regular32;
        public static PrototypeDungeonRoom Expand_Proper_Regular33;
        public static PrototypeDungeonRoom Expand_Proper_Regular34;
        public static PrototypeDungeonRoom Expand_Proper_Regular35;
        public static PrototypeDungeonRoom Expand_Proper_Regular36;
        public static PrototypeDungeonRoom Expand_Proper_Regular37;
        public static PrototypeDungeonRoom Expand_Proper_Regular38;
        public static PrototypeDungeonRoom Expand_Proper_Regular39;
        public static PrototypeDungeonRoom Expand_Proper_Regular40;
        public static PrototypeDungeonRoom Expand_Proper_Regular41;
        public static PrototypeDungeonRoom Expand_Proper_Regular42;
        public static PrototypeDungeonRoom Expand_Proper_Regular43;
        public static PrototypeDungeonRoom Expand_Proper_Regular44;
        public static PrototypeDungeonRoom Expand_Proper_Regular45;
        public static PrototypeDungeonRoom Expand_Proper_Regular46;
        public static PrototypeDungeonRoom Expand_Proper_Regular47;
        public static PrototypeDungeonRoom Expand_Proper_Regular48;
        public static PrototypeDungeonRoom Expand_Proper_Regular49;
        public static PrototypeDungeonRoom Expand_Proper_Regular50;
        public static PrototypeDungeonRoom Expand_Proper_Speshul_Regular30;
        public static PrototypeDungeonRoom Expand_Mines_Regular1;
        public static PrototypeDungeonRoom Expand_Mines_Regular2;
        public static PrototypeDungeonRoom Expand_Mines_Regular3;
        public static PrototypeDungeonRoom Expand_Mines_Regular4;
        public static PrototypeDungeonRoom Expand_Mines_Regular5;
        public static PrototypeDungeonRoom Expand_Mines_Regular6;
        public static PrototypeDungeonRoom Expand_Mines_Regular7;
        public static PrototypeDungeonRoom Expand_Mines_Regular8;
        public static PrototypeDungeonRoom Expand_Mines_Regular9;
        public static PrototypeDungeonRoom Expand_Mines_Regular10;
        public static PrototypeDungeonRoom Expand_Mines_Regular11;
        public static PrototypeDungeonRoom Expand_Mines_Regular12;
        public static PrototypeDungeonRoom Expand_Mines_Regular13;
        public static PrototypeDungeonRoom Expand_Mines_Regular14;
        public static PrototypeDungeonRoom Expand_Mines_Regular15;
        public static PrototypeDungeonRoom Expand_Mines_Regular16;
        public static PrototypeDungeonRoom Expand_Mines_Regular17;
        public static PrototypeDungeonRoom Expand_Mines_Regular18;
        public static PrototypeDungeonRoom Expand_Mines_Regular19;
        public static PrototypeDungeonRoom Expand_Mines_Regular20;
        public static PrototypeDungeonRoom Expand_Mines_Regular21;
        public static PrototypeDungeonRoom Expand_Mines_Regular22;
        public static PrototypeDungeonRoom Expand_Mines_Regular23;
        public static PrototypeDungeonRoom Expand_Mines_Regular24;
        public static PrototypeDungeonRoom Expand_Mines_Regular25;
        public static PrototypeDungeonRoom Expand_Mines_Regular26;
        public static PrototypeDungeonRoom Expand_Mines_Regular27;
        public static PrototypeDungeonRoom Expand_Mines_Regular28;
        public static PrototypeDungeonRoom Expand_Mines_Regular29;
        public static PrototypeDungeonRoom Expand_Mines_Regular30;
        public static PrototypeDungeonRoom Expand_Mines_Regular31;
        public static PrototypeDungeonRoom Expand_Mines_Regular32;
        public static PrototypeDungeonRoom Expand_Mines_Regular33;
        public static PrototypeDungeonRoom Expand_Mines_Regular34;
        public static PrototypeDungeonRoom Expand_Mines_Regular35;
        public static PrototypeDungeonRoom Expand_Mines_Regular36;
        public static PrototypeDungeonRoom Expand_Mines_Regular37;
        public static PrototypeDungeonRoom Expand_Mines_Regular38;
        public static PrototypeDungeonRoom Expand_Mines_Regular39;
        public static PrototypeDungeonRoom Expand_Mines_Regular40;
        public static PrototypeDungeonRoom Expand_Mines_Regular41;
        public static PrototypeDungeonRoom Expand_Mines_Regular42;
        public static PrototypeDungeonRoom Expand_Mines_Regular43;
        public static PrototypeDungeonRoom Expand_Mines_Regular44;
        public static PrototypeDungeonRoom Expand_Mines_Regular45;
        public static PrototypeDungeonRoom Expand_Mines_Regular46;
        public static PrototypeDungeonRoom Expand_Mines_Regular47;
        public static PrototypeDungeonRoom Expand_Mines_Regular48;
        public static PrototypeDungeonRoom Expand_Mines_Regular49;
        public static PrototypeDungeonRoom Expand_Mines_Regular50;
        public static PrototypeDungeonRoom Expand_Keep_Spiral;

        public static PrototypeDungeonRoom Expand_Jungle_Entrance;
        public static PrototypeDungeonRoom Expand_Jungle_OldCrest;
        public static PrototypeDungeonRoom Expand_Jungle_Exit;
        public static PrototypeDungeonRoom Expand_Jungle_SecretDragun;
        public static PrototypeDungeonRoom Expand_Jungle_Boss;
        // All jungle rooms except special ones will be stored here
        public static PrototypeDungeonRoom[] Expand_Jungle_Rooms;
        public static List<string> Expand_Jungle_RoomList;
        public static List<string> Expand_Belly_RoomList;
        public static List<string> Expand_BulletHell_RoomList;
        public static List<string> Expand_West_CanyonRoomList;
        public static List<string> Expand_West_TinyCanyonRoomList;
        public static List<string> Expand_West_Interior1RoomList;

        public static PrototypeDungeonRoom Expand_Belly_Entrance;
        public static PrototypeDungeonRoom Expand_Belly_BossRoom;
        public static PrototypeDungeonRoom Expand_Belly_Connector_01;
        public static PrototypeDungeonRoom Expand_Belly_Connector_02;
        public static PrototypeDungeonRoom Expand_Belly_Connector_03;
        public static PrototypeDungeonRoom Expand_Belly_Connector_04;
        public static PrototypeDungeonRoom Expand_Belly_Connector_05;
        public static PrototypeDungeonRoom Expand_Belly_Connector_06;
        public static PrototypeDungeonRoom Expand_Belly_Shrine;
        public static PrototypeDungeonRoom Expand_Belly_ExitHub;
        public static PrototypeDungeonRoom Expand_Belly_RealExit;
        public static PrototypeDungeonRoom Expand_Belly_Reward;

        // All West rooms except special ones will be stored here
        public static PrototypeDungeonRoom[] Expand_West_CanyonRooms;
        public static PrototypeDungeonRoom[] Expand_West_TinyCanyonRooms;
        public static PrototypeDungeonRoom[] Expand_West_Interior1Rooms;
        public static PrototypeDungeonRoom Expand_West_Entrance;
        public static PrototypeDungeonRoom Expand_West_SecretWarp;
        public static PrototypeDungeonRoom Expand_West_SecretHub;
        public static PrototypeDungeonRoom Expand_West_SecretHub2;
        public static PrototypeDungeonRoom Expand_West_SecretShopWarp;
        public static PrototypeDungeonRoom Expand_West_SecretKeyShop;
        public static PrototypeDungeonRoom Expand_West_ChestRoom;
        public static PrototypeDungeonRoom Expand_West_BlankPedestalRoom;
        public static PrototypeDungeonRoom Expand_West_RatKeyPedestalRoom;
        public static PrototypeDungeonRoom Expand_West_SecretRatKeyPedestalRoom;
        public static PrototypeDungeonRoom Expand_West_ShrineRoom;
        public static PrototypeDungeonRoom Expand_West_WestBrosBossRoom;

        // Foyer Rooms
        // public static PrototypeDungeonRoom Expand_Casino_Hub;

        // All Belly rooms except special ones will be stored here
        // public static PrototypeDungeonRoom[] Expand_Belly_Rooms;
        public static PrototypeDungeonRoom[] Expand_Belly_Rooms;        
        public static PrototypeDungeonRoom Expand_Gungeon_BellyEntranceRoom;
        public static PrototypeDungeonRoom Expand_Gungeon_HiddenMonsterRoom;

        public static PrototypeDungeonRoom[] Expand_BulletHell_Rooms;

        public static WeightedRoom GenerateWeightedRoom(PrototypeDungeonRoom Room, float Weight = 1, bool LimitedCopies = true, int MaxCopies = 1, DungeonPrerequisite[] AdditionalPrerequisites = null) {
            if (Room == null) { return null; }
            if (AdditionalPrerequisites == null) { AdditionalPrerequisites = new DungeonPrerequisite[0]; }
            return new WeightedRoom() { room = Room, weight = Weight, limitedCopies = LimitedCopies, maxCopies = MaxCopies, additionalPrerequisites = AdditionalPrerequisites };
        }


        public static void InitCustomRooms(AssetBundle expandSharedAssets1, AssetBundle sharedAssets, AssetBundle sharedAssets2, AssetBundle braveResources, AssetBundle EnemiesBundle) {
            
            AssetBundle[] AssetBundles = new AssetBundle[] { sharedAssets, sharedAssets2, braveResources, EnemiesBundle };
            
            Expand_Jungle_RoomList = new List<string>() {
                "Expand_Forest_ZookieShroom",
                "Expand_Forest_AnimalEnclosed",
                "Expand_Forest_AnimalHunter1",
                "Expand_Forest_Animals1",
                "Expand_Forest_Animals2",
                "Expand_Forest_AnimalsArrows",
                "Expand_Forest_AnimalsFairy",
                "Expand_Forest_AnimalsHunters1",
                "Expand_Forest_AnimalSpider",
                "Expand_Forest_AnimalsTribal",
                "Expand_Forest_ArrowCat",
                "Expand_Forest_ArrowSpent",
                "Expand_Forest_BandanaSnake",
                "Expand_Forest_BIRDS",
                "Expand_Forest_BullatFairy",
                "Expand_Forest_Bush",
                "Expand_Forest_ConfirmedSnakes",
                "Expand_Forest_Extra1",
                "Expand_Forest_FishBats",
                "Expand_Forest_FungunTribal",
                "Expand_Forest_GummyBullatPit",
                "Expand_Forest_GunFairy",
                "Expand_Forest_Hunter1",
                "Expand_Forest_Hunter2",
                "Expand_Forest_HunterSporge",
                "Expand_Forest_HunterVets",
                "Expand_Forest_MushFairy",
                "Expand_Forest_Mushroom",
                "Expand_Forest_MushZookie",
                "Expand_Forest_ParrotShroom",
                "Expand_Forest_Professionals",
                "Expand_Forest_RegularSnakeParrot",
                "Expand_Forest_ShamblingArrows",
                "Expand_Forest_SniperTotemBuff",
                "Expand_Forest_SpendRush",
                "Expand_Forest_SpiderSnakesArrow",
                "Expand_Forest_TotemBuff",
                "Expand_Forest_Tree",
                "Expand_Forest_Tribal1",
                "Expand_Forest_Tribal2",
                "Expand_Forest_Tribal3",
                "Expand_Forest_Tribal4",
                "Expand_Forest_Tribal5",
                "Expand_Forest_Tribal6",
                "Expand_Forest_TribalBirds",
                "Expand_Forest_TribalC",
                "Expand_Forest_TribalNugget",
                "Expand_Forest_TribalSummon",
                "Expand_Forest_Trunk",
                "Expand_Forest_Trunk2",
                "Expand_Forest_Tribal31",
                "Expand_Forest_Tribal32",
                "Expand_Forest_Animals3",
                "Expand_Forest_Animals4",
                "Expand_Forest_Animals5",
                "Expand_Forest_Animals6",
                "Expand_Forest_Animals7",
                "Expand_Forest_Animals8",
                "Expand_Forest_Animals9",
                "Expand_Forest_Animals10",
                "Expand_Forest_Animals11",
                "Expand_Forest_Animals12",
                "Expand_Forest_Animals13",
                "Expand_Forest_Animals14",
                "Expand_Forest_Animals15",
                "Expand_Forest_Animals16",
                "Expand_Forest_Animals17",
                "Expand_Forest_Animals18",
                "Expand_Forest_Animals19",
                "Expand_Forest_Animals20",
                "Expand_Forest_Animals21",
                "Expand_Forest_Animals22",
                "Expand_Forest_Animals23",
                "Expand_Forest_Animals24",
                "Expand_Forest_Animals25",
                "Expand_Forest_Animals26",
                "Expand_Forest_Animals27",
                "Expand_Forest_Hunter3",
                "Expand_Forest_Hunter4",
                "Expand_Forest_Hunter5",
                "Expand_Forest_Hunter6",
                "Expand_Forest_Hunter7",
                "Expand_Forest_Hunter8",
                "Expand_Forest_Hunter9",
                "Expand_Forest_Hunter10",
                "Expand_Forest_Hunter11",
                "Expand_Forest_Hunter12",
                "Expand_Forest_Hunter13",
                "Expand_Forest_Hunter14",
                "Expand_Forest_Hunter15",
                "Expand_Forest_Hunter16",
                "Expand_Forest_Hunter17",
                "Expand_Forest_Hunter18",
                "Expand_Forest_Hunter19",
                "Expand_Forest_Hunter20",
                "Expand_Forest_Hunter21",
                "Expand_Forest_Hunter22",
                "Expand_Forest_Hunter23",
                "Expand_Forest_Hunter24",
                "Expand_Forest_Hunter25",
                "Expand_Forest_Hunter26",
                "Expand_Forest_Hunter27",
                "Expand_Forest_Mixed1",
                "Expand_Forest_Mixed2",
                "Expand_Forest_Mixed3",
                "Expand_Forest_Mixed4",
                "Expand_Forest_Mixed5",
                "Expand_Forest_Mixed6",
                "Expand_Forest_Mixed7",
                "Expand_Forest_Mixed8",
                "Expand_Forest_Mixed9",
                "Expand_Forest_Mixed10",
                "Expand_Forest_Mixed11",
                "Expand_Forest_Mixed12",
                "Expand_Forest_Mixed13",
                "Expand_Forest_Mixed14",
                "Expand_Forest_Mixed15",
                "Expand_Forest_Mixed16",
                "Expand_Forest_Mixed17",
                "Expand_Forest_Mixed18",
                "Expand_Forest_Mixed19",
                "Expand_Forest_Mixed20",
                "Expand_Forest_Mixed21",
                "Expand_Forest_Mixed22",
                "Expand_Forest_Mixed23",
                "Expand_Forest_Mixed24",
                "Expand_Forest_Mixed25",
                "Expand_Forest_Tribal7",
                "Expand_Forest_Tribal8",
                "Expand_Forest_Tribal9",
                "Expand_Forest_Tribal10",
                "Expand_Forest_Tribal11",
                "Expand_Forest_Tribal12",
                "Expand_Forest_Tribal13",
                "Expand_Forest_Tribal14",
                "Expand_Forest_Tribal15",
                "Expand_Forest_Tribal16",
                "Expand_Forest_Tribal17",
                "Expand_Forest_Tribal18",
                "Expand_Forest_Tribal19",
                "Expand_Forest_Tribal20",
                "Expand_Forest_Tribal21",
                "Expand_Forest_Tribal22",
                "Expand_Forest_Tribal23",
                "Expand_Forest_Tribal24",
                "Expand_Forest_Tribal25",
                "Expand_Forest_Tribal27",
                "Expand_Forest_Tribal28",
                "Expand_Forest_Tribal29",
                "Expand_Forest_Tribal30"
            };

            Expand_Belly_RoomList = new List<string>() {
                "Expand_Belly_Mixed3",
                "Expand_Belly_Mixed4",
                "Expand_Belly_Mixed5",
                "Expand_Belly_Mixed6",
                "Expand_Belly_Mixed7",
                "Expand_Belly_Mixed8",
                "Expand_Belly_Mixed9",
                "Expand_Belly_Mixed10",
                "Expand_Belly_Mixed11",
                "Expand_Belly_Mixed12",
                "Expand_Belly_Mixed13",
                "Expand_Belly_Mixed14",
                "Expand_Belly_Mixed15",
                "Expand_Belly_Mixed16",
                "Expand_Belly_Mixed17",
                "Expand_Belly_Mixed18",
                "Expand_Belly_Mixed19",
                "Expand_Belly_Mixed20",
                "Expand_Belly_Pirate1",
                "Expand_Belly_Pirate2",
                "Expand_Belly_Pirate3",
                "Expand_Belly_Pirate4",
                "Expand_Belly_Pirate5",
                "Expand_Belly_Pirate6",
                "Expand_Belly_Pirate7",
                "Expand_Belly_Pirate8",
                "Expand_Belly_Pirate9",
                "Expand_Belly_Pirate10",
                "Expand_Belly_Skeletal1",
                "Expand_Belly_Skeletal2",
                "Expand_Belly_Skeletal3",
                "Expand_Belly_Skeletal4",
                "Expand_Belly_Skeletal5",
                "Expand_Belly_Skeletal6",
                "Expand_Belly_Skeletal7",
                "Expand_Belly_Skeletal8",
                "Expand_Belly_Skeletal9",
                "Expand_Belly_Skeletal10",
                "Expand_Belly_Viscera1",
                "Expand_Belly_Viscera2",
                "Expand_Belly_Viscera3",
                "Expand_Belly_Viscera4",
                "Expand_Belly_Viscera5",
                "Expand_Belly_Viscera6",
                "Expand_Belly_Viscera7",
                "Expand_Belly_Viscera8",
                "Expand_Belly_Viscera9",
                "Expand_Belly_Viscera10",
                "Expand_Belly_Mixed1",
                "Expand_Belly_Mixed2",
                "Expand_Belly_Pirate17",
                "Expand_Belly_Pirate18",
                "Expand_Belly_Pirate19",
                "Expand_Belly_Pirate20",
                "Expand_Belly_Pirate21",
                "Expand_Belly_Pirate22",
                "Expand_Belly_Pirate23",
                "Expand_Belly_Pirate24",
                "Expand_Belly_Pirate25",
                "Expand_Belly_Pirate26",
                "Expand_Belly_Pirate27",
                "Expand_Belly_Pirate28",
                "Expand_Belly_Pirate29",
                "Expand_Belly_Pirate30",
                "Expand_Belly_Skeletal_RupturousClot",
                "Expand_Belly_Skeletal11",
                "Expand_Belly_Skeletal12",
                "Expand_Belly_Skeletal13",
                "Expand_Belly_Skeletal14",
                "Expand_Belly_Skeletal15",
                "Expand_Belly_Skeletal16",
                "Expand_Belly_Skeletal17",
                "Expand_Belly_Skeletal18",
                "Expand_Belly_Skeletal19",
                "Expand_Belly_Skeletal20",
                "Expand_Belly_Skeletal21",
                "Expand_Belly_Skeletal22",
                "Expand_Belly_Skeletal23",
                "Expand_Belly_Skeletal24",
                "Expand_Belly_Skeletal25",
                "Expand_Belly_Skeletal26",
                "Expand_Belly_Skeletal27",
                "Expand_Belly_Skeletal28",
                "Expand_Belly_Skeletal29",
                "Expand_Belly_Skeletal30",
                "Expand_Belly_Viscera_EyeDiagram",
                "Expand_Belly_Viscera_FloppyKnobs",
                "Expand_Belly_Viscera_Globulate",
                "Expand_Belly_Viscera_GutsyBulges",
                "Expand_Belly_Viscera_HeartAttack",
                "Expand_Belly_Viscera_NubblyBubbly",
                "Expand_Belly_Viscera_Shitfactory",
                "Expand_Belly_Viscera11",
                "Expand_Belly_Pirate_CellStronghold",
                "Expand_Belly_Pirate_DanglyBits",
                "Expand_Belly_Pirate_FishInTheEar",
                "Expand_Belly_Pirate11",
                "Expand_Belly_Pirate12",
                "Expand_Belly_Pirate13",
                "Expand_Belly_Pirate14",
                "Expand_Belly_Pirate15",
                "Expand_Belly_Pirate16",
                "Expand_Belly_Viscera30",
                "Expand_Belly_Viscera12",
                "Expand_Belly_Viscera13",
                "Expand_Belly_Viscera14",
                "Expand_Belly_Viscera15",
                "Expand_Belly_Viscera16",
                "Expand_Belly_Viscera17",
                "Expand_Belly_Viscera18",
                "Expand_Belly_Viscera19",
                "Expand_Belly_Viscera20",
                "Expand_Belly_Viscera21",
                "Expand_Belly_Viscera22",
                "Expand_Belly_Viscera23",
                "Expand_Belly_Viscera24",
                "Expand_Belly_Viscera25",
                "Expand_Belly_Viscera26",
                "Expand_Belly_Viscera27",
                "Expand_Belly_Viscera28",
                "Expand_Belly_Viscera29"
            };

            Expand_West_CanyonRoomList = new List<string>() {
                "Expand_West_Canyon_Yshape4",
                "Expand_West_Canyon_AP_Passage",
                "Expand_West_Canyon_Arm1",
                "Expand_West_Canyon_Arm2",
                "Expand_West_Canyon_BigGap1",
                "Expand_West_Canyon_BigGap2",
                "Expand_West_Canyon_BigGap3",
                "Expand_West_Canyon_Circleish1",
                "Expand_West_Canyon_Circleish2",
                "Expand_West_Canyon_Circleish3",
                "Expand_West_Canyon_Circleish4",
                "Expand_West_Canyon_CornerBomb1",
                "Expand_West_Canyon_CornerBomb2",
                "Expand_West_Canyon_CornerBomb3",
                "Expand_West_Canyon_CornerBomb4",
                "Expand_West_Canyon_Fight1",
                "Expand_West_Canyon_Fight2",
                "Expand_West_Canyon_Fight3",
                "Expand_West_Canyon_Fight4",
                "Expand_West_Canyon_Fight5",
                "Expand_West_Canyon_LongOdd1",
                "Expand_West_Canyon_LongOdd2",
                "Expand_West_Canyon_LongOdd3",
                "Expand_West_Canyon_Odd1",
                "Expand_West_Canyon_Odd2",
                "Expand_West_Canyon_Odd3",
                "Expand_West_Canyon_Odd4",
                "Expand_West_Canyon_OddShape1",
                "Expand_West_Canyon_OddShape2",
                "Expand_West_Canyon_OddShape3",
                "Expand_West_Canyon_OddShape4",
                "Expand_West_Canyon_OddSquare1",
                "Expand_West_Canyon_OddSquare2",
                "Expand_West_Canyon_OddSquare3",
                "Expand_West_Canyon_OddSquare4",
                "Expand_West_Canyon_smol1",
                "Expand_West_Canyon_smol2",
                "Expand_West_Canyon_smol3",
                "Expand_West_Canyon_smol4",
                "Expand_West_Canyon_smol5",
                "Expand_West_Canyon_tight1",
                "Expand_West_Canyon_tight2",
                "Expand_West_Canyon_tight3",
                "Expand_West_Canyon_tight4",
                "Expand_West_Canyon_Weird1",
                "Expand_West_Canyon_Weird2",
                "Expand_West_Canyon_Weird3",
                "Expand_West_Canyon_Weird4",
                "Expand_West_Canyon_Yshape1",
                "Expand_West_Canyon_Yshape2",
                "Expand_West_Canyon_Yshape3"
            };

            Expand_West_TinyCanyonRoomList = new List<string>() {
                "Expand_West_Canyon1_Straight1",
                "Expand_West_Canyon1_Straight2",
                "Expand_West_Canyon1_Straight3",
                "Expand_West_Canyon1_Tiny1",
                "Expand_West_Canyon1_Tiny2",
                "Expand_West_Canyon1_Tiny3",
                "Expand_West_Canyon1_Tiny4",
                "Expand_West_Canyon1_Tiny5",
                "Expand_West_Canyon1_Tiny6",
                "Expand_West_Canyon1_Tiny7",
                "Expand_West_Canyon1_Tiny8",
                "Expand_West_Canyon1_Tiny9",
                "Expand_West_Canyon1_Tiny10",
                "Expand_West_Canyon1_Tiny11",
                "Expand_West_Canyon1_Tiny12",
                "Expand_West_Canyon1_Tiny13",
                "Expand_West_Canyon1_Tiny14",
                "Expand_West_Canyon1_Tiny15",
                "Expand_West_Canyon1_Tiny16",
                "Expand_West_Canyon1_Tiny17",
                "Expand_West_Canyon1_Tiny18",
                "Expand_West_Canyon1_Tiny19",
                "Expand_West_Canyon1_Tiny20",
                "Expand_West_Canyon1_TinyPassage1",
                "Expand_West_Canyon1_TinyPassage2",
                "Expand_West_Canyon1_TinyPassage3",
                "Expand_West_Canyon1_TinyPassage4",
                "Expand_West_Canyon1_TinyPassage5"
            };

            Expand_West_Interior1RoomList = new List<string>() {
                "Expand_West_Interior1_Tabled1",
                "Expand_West_Interior1_Tabled2",
                "Expand_West_Interior1_Tabled3",
                "Expand_West_Interior1_Teleport1",
                "Expand_West_Interior1_Teleport2",
                "Expand_West_Interior1_Teleport2_1",
                "Expand_West_Interior1_Teleport2_2",
                "Expand_West_Interior1_Teleport2_3",
                "Expand_West_Interior1_Teleport2_4",
                "Expand_West_Interior1_Teleport2_5",
                "Expand_West_Interior1_Teleport3",
                "Expand_West_Interior1_Thin1",
                "Expand_West_Interior1_Thin2",
                "Expand_West_Interior1_Thin3",
                "Expand_West_Interior1_Thin4",
                "Expand_West_Interior1_Thin5",
                "Expand_West_Interior1_Trapped1",
                "Expand_West_Interior1_Trapped2",
                "Expand_West_Interior1_AP_HTrap",
                "Expand_West_Interior1_AP_West",
                "Expand_West_Interior1_Bar1",
                "Expand_West_Interior1_Bar2",
                "Expand_West_Interior1_BoomLine1",
                "Expand_West_Interior1_BoomLine2",
                "Expand_West_Interior1_BoomLine3",
                "Expand_West_Interior1_Boxes1",
                "Expand_West_Interior1_Boxes2",
                "Expand_West_Interior1_Boxes3",
                "Expand_West_Interior1_Boxes4",
                "Expand_West_Interior1_Boxes5",
                "Expand_West_Interior1_ImportantBusiness",
                "Expand_West_Interior1_Square1",
                "Expand_West_Interior1_Square2",
                "Expand_West_Interior1_Square3",
                "Expand_West_Interior1_Square4",
                "Expand_West_Interior1_Square5",
                "Expand_West_Interior1_Square6",
                "Expand_West_Interior1_Squareish1",
                "Expand_West_Interior1_Squareish2",
                "Expand_West_Interior1_Squareish3",
                "Expand_West_Interior1_Squareish4",
                "Expand_West_Interior1_Squareish5",
                "Expand_West_Interior1_Squareish6",
                "Expand_West_Interior1_Squareish7",
                "Expand_West_Interior1_Squareish8",
                "Expand_West_Interior1_Steps1",
                "Expand_West_Interior1_Steps2",
                "Expand_West_Interior1_Steps3",
                "Expand_West_Interior1_Steps4"
            };
                        
            Expand_BulletHell_RoomList = new List<string>() { "BHell_TheReunion" };

            List<PrototypeDungeonRoom> m_BulletHellRoomList = new List<PrototypeDungeonRoom>() { };

            foreach (string roomName in Expand_BulletHell_RoomList) {
                m_BulletHellRoomList.Add(RoomFactory.BuildFromAssetBundle(AssetBundles, roomName, true, true));
            }

            List<PrototypeDungeonRoom> m_West_Canyon_RoomList = new List<PrototypeDungeonRoom>();
            List<PrototypeDungeonRoom> m_West_TinyCanyon_RoomList = new List<PrototypeDungeonRoom>();
            List<PrototypeDungeonRoom> m_West_Interior1_RoomList = new List<PrototypeDungeonRoom>();

            foreach (string roomName in Expand_West_CanyonRoomList) {
                m_West_Canyon_RoomList.Add(RoomFactory.BuildFromAssetBundle(AssetBundles, roomName, true, false));
            }

            foreach (string roomName in Expand_West_TinyCanyonRoomList) {
                m_West_TinyCanyon_RoomList.Add(RoomFactory.BuildFromAssetBundle(AssetBundles, roomName, true, false));
            }

            foreach (string roomName in Expand_West_Interior1RoomList) {
                m_West_Interior1_RoomList.Add(RoomFactory.BuildFromAssetBundle(AssetBundles, roomName, true, false));
            }

            foreach (PrototypeDungeonRoom room in m_West_Canyon_RoomList) { room.overrideRoomVisualType = 0; }
            foreach (PrototypeDungeonRoom room in m_West_TinyCanyon_RoomList) { room.overrideRoomVisualType = 0; }
            foreach (PrototypeDungeonRoom room in m_West_Interior1_RoomList) { room.overrideRoomVisualType = 1; }

            Expand_West_CanyonRooms = m_West_Canyon_RoomList.ToArray();
            Expand_West_TinyCanyonRooms = m_West_TinyCanyon_RoomList.ToArray();
            Expand_West_Interior1Rooms = m_West_Interior1_RoomList.ToArray();
            
            Expand_Jungle_Entrance = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Jungle_Entrance", true);
            // RoomBuilder.AddObjectToRoom(Expand_Jungle_Entrance, new Vector2(4, 5), ExpandObjectDatabase.GodRays);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Entrance, new Vector2(5, 7), ExpandPrefabs.EXElevator_Arrival_Placable);
            Expand_Jungle_Entrance.associatedMinimapIcon = ExpandPrefabs.elevator_entrance.associatedMinimapIcon;

            Expand_Jungle_OldCrest = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Jungle_OldCrest", true);
            Expand_Jungle_OldCrest.associatedMinimapIcon = ExpandPrefabs.EXJungleCrest_MinimapIcon;
            RoomBuilder.AddObjectToRoom(Expand_Jungle_OldCrest, new Vector2(5, 7), ExpandObjectDatabase.GodRays);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_OldCrest, new Vector2(7, 8), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Jungle_ItemStump, useExternalPrefab: true), xOffset: 6);
            

            Expand_Jungle_Exit = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Jungle_Exit", true);
            Expand_Jungle_Exit.associatedMinimapIcon = ExpandPrefabs.exit_room_basic.associatedMinimapIcon;
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Exit, new Vector2(4, 7), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Jungle_ExitLadder, useExternalPrefab: true), xOffset: 8, yOffset: 13);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Exit, new Vector2(2, 5), ExpandObjectDatabase.GodRays);

            Expand_Jungle_SecretDragun = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Jungle_SecretDragun", true);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_SecretDragun, new Vector2(4, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.GatlingGullNest, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Jungle_SecretDragun, new Vector2(6, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.BabyDragunNPC, useExternalPrefab: true));

            Expand_Jungle_Boss = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Jungle_Boss");
            Expand_Jungle_Boss.category = PrototypeDungeonRoom.RoomCategory.BOSS;
            Expand_Jungle_Boss.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Jungle_Boss.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Jungle_Boss.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Jungle_Boss.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Jungle_Boss.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Jungle_Boss.associatedMinimapIcon = ExpandPrefabs.doublebeholsterroom01.associatedMinimapIcon;
            Expand_Jungle_Boss.usesProceduralLighting = false;
            Expand_Jungle_Boss.usesProceduralDecoration = false;
            Expand_Jungle_Boss.rewardChestSpawnPosition = new IntVector2(25, 20);
            Expand_Jungle_Boss.overriddenTilesets = GlobalDungeonData.ValidTilesets.JUNGLEGEON;
            Expand_Jungle_Boss.overrideRoomVisualType = 0;
            foreach (PrototypeRoomExit exit in Expand_Jungle_Boss.exitData.exits) { exit.exitType = PrototypeRoomExit.ExitType.ENTRANCE_ONLY; }
            RoomBuilder.AddExitToRoom(Expand_Jungle_Boss, new Vector2(26, 37), DungeonData.Direction.NORTH, PrototypeRoomExit.ExitType.EXIT_ONLY, PrototypeRoomExit.ExitGroup.B);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(7, 25), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.bosstable_01_bulletking.includedRooms.elements[0].room.placedObjects[1].nonenemyBehaviour.gameObject, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(7, 25), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.bosstable_01_bulletking.includedRooms.elements[0].room.additionalObjectLayers[0].placedObjects[0].nonenemyBehaviour.gameObject, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(7, 25), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.bosstable_01_bulletking.includedRooms.elements[0].room.additionalObjectLayers[1].placedObjects[0].nonenemyBehaviour.gameObject, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(7, 23), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.bosstable_01_bulletking.includedRooms.elements[0].room.additionalObjectLayers[2].placedObjects[0].nonenemyBehaviour.gameObject, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(8, 25), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.JungleLight, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(20, 30), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.JungleLight, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(30, 30), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.JungleLight, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(42, 25), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.JungleLight, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(8, 2), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.JungleLight, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(25, 2), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.JungleLight, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(42, 2), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.JungleLight, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(3, 13), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.JungleLight, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(47, 13), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.JungleLight, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(8, 25), ExpandObjectDatabase.DefaultTorch, xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(20, 30), ExpandObjectDatabase.DefaultTorch, xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(30, 30), ExpandObjectDatabase.DefaultTorch, xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(42, 25), ExpandObjectDatabase.DefaultTorch, xOffset: 8);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(22, 14), ExpandObjectDatabase.GodRays);
            RoomBuilder.AddObjectToRoom(Expand_Jungle_Boss, new Vector2(24, 16), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.com4nd0GUID);


            Expand_Belly_Entrance = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_Entrance", true);


            Expand_Gungeon_BellyEntranceRoom = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Gungeon_BellyEntranceRoom", true);
            Expand_Gungeon_BellyEntranceRoom.usesProceduralLighting = false;
            Expand_Gungeon_BellyEntranceRoom.usesProceduralDecoration = false;
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(6, 25), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarco_Door, useExternalPrefab: true), xOffset: 7, yOffset: -3);
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(10, 23), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarco_WoodShieldPedestal, useExternalPrefab: true), xOffset: 11, yOffset: -6);
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(3, 6), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarcophagus_ShotgunBook_Kaliber, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(3, 12), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarcophagus_ShotgunMace_Kaliber, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(3, 18), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarcophagus_ShotgunBook_Kaliber, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(11, 6), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarcophagus_BulletSword_Kaliber, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(11, 12), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarcophagus_BulletShield_Kaliber, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(11, 18), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarcophagus_BulletSword_Kaliber, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(1, 8), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.GungeonLightPurple, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(3, 26), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.GungeonLightPurple, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_BellyEntranceRoom, new Vector2(12, 26), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.GungeonLightPurple, useExternalPrefab: true));
            // RoomBuilder.GenerateRoomLayout(Expand_Gungeon_BellyEntranceRoom, "Expand_Gungeon_BellyEntranceRoom_ext", DamageCellsType: CoreDamageTypes.Water); // use old system to force carpet floor cells


            Expand_Belly_BossRoom = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_BossRoom");
            Expand_Belly_BossRoom.category = PrototypeDungeonRoom.RoomCategory.BOSS;
            Expand_Belly_BossRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Belly_BossRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Belly_BossRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Belly_BossRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Belly_BossRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Belly_BossRoom.associatedMinimapIcon = ExpandPrefabs.doublebeholsterroom01.associatedMinimapIcon;
            Expand_Belly_BossRoom.usesProceduralDecoration = false;
            Expand_Belly_BossRoom.rewardChestSpawnPosition = new IntVector2(18, 23);
            Expand_Belly_BossRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = ExpandCustomEnemyDatabase.AggressiveCronenbergGUID, // 
                            contentsBasePosition = new Vector2(17, 12),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = ExpandCustomEnemyDatabase.AggressiveCronenbergGUID, // 
                            contentsBasePosition = new Vector2(15, 18),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = ExpandCustomEnemyDatabase.AggressiveCronenbergGUID, // 
                            contentsBasePosition = new Vector2(11, 12),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = ExpandCustomEnemyDatabase.AggressiveCronenbergGUID, // 
                            contentsBasePosition = new Vector2(19, 27),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = ExpandCustomEnemyDatabase.AggressiveCronenbergGUID, // 
                            contentsBasePosition = new Vector2(27, 15),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = ExpandCustomEnemyDatabase.AggressiveCronenbergGUID, // 
                            contentsBasePosition = new Vector2(29, 19),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(17, 12),
                        new Vector2(15, 18),
                        new Vector2(11, 12),
                        new Vector2(19, 27),
                        new Vector2(27, 15),
                        new Vector2(29, 19),
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 15,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ENEMY_BEHAVIOR,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            RoomBuilder.AddObjectToRoom(Expand_Belly_BossRoom, new Vector2(20, 19), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.ParasiteBossGUID);
            // RoomBuilder.AddObjectToRoom(Expand_Belly_BossRoom, new Vector2(1, 30), ExpandPrefab.Belly_Shipwreck_Left);
            // RoomBuilder.AddObjectToRoom(Expand_Belly_BossRoom, new Vector2(26, 31), ExpandPrefab.Belly_Shipwreck_Right);


            Expand_Gungeon_HiddenMonsterRoom = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Gungeon_HiddenMonsterRoom", true);
            Expand_Gungeon_HiddenMonsterRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.NPC_TRIGGER_A, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.NPC_TRIGGER_C, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, Vector2.zero, ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarco_Floor, useExternalPrefab: true));
            // RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(36, 7), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.CronenbergGUID);
            // RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(24, 2), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.CronenbergGUID);
            // RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(18, 4), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.CronenbergGUID);
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(64, 0), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarco_MonsterObject, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(13, 2), ExpandObjectDatabase.WrithingBulletman);
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(33, 5), ExpandObjectDatabase.WrithingBulletman);
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(40, 2), ExpandObjectDatabase.WrithingBulletman);
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(48, 3), ExpandObjectDatabase.WrithingBulletman);
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(57, 5), ExpandObjectDatabase.WrithingBulletman);
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(58, 2), ExpandObjectDatabase.WrithingBulletman);
            RoomBuilder.AddObjectToRoom(Expand_Gungeon_HiddenMonsterRoom, new Vector2(10, 3), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Sarco_Skeleton, useExternalPrefab: true));
            Expand_Gungeon_HiddenMonsterRoom.usesProceduralLighting = false;
            Expand_Gungeon_HiddenMonsterRoom.allowFloorDecoration = false;
            Expand_Gungeon_HiddenMonsterRoom.allowWallDecoration = false;
            Expand_Gungeon_HiddenMonsterRoom.usesProceduralDecoration = false;
            Expand_Gungeon_HiddenMonsterRoom.precludeAllTilemapDrawing = true;


            Expand_Belly_Connector_01 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_Connector_01", true);
            Expand_Belly_Connector_02 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_Connector_02", true);
            Expand_Belly_Connector_03 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_Connector_03", true);
            Expand_Belly_Connector_04 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_Connector_04", true);
            Expand_Belly_Connector_05 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_Connector_05", true);
            Expand_Belly_Connector_06 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_Connector_06", true);

            Expand_Belly_Reward = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_Reward", true);
            Expand_Belly_Shrine = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_Shrine", true);
            Expand_Belly_ExitHub = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_ExitHub", true);

            Expand_Belly_RealExit = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Belly_RealExit", true);
            Expand_Belly_RealExit.associatedMinimapIcon = ExpandPrefabs.Belly_ExitRoomIcon;
            Expand_Belly_RealExit.usesProceduralDecoration = false;
            Expand_Belly_RealExit.usesProceduralLighting = false;

            RoomBuilder.AddObjectToRoom(Expand_Belly_RealExit, new Vector2(11, 38), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Door_Vertical_Belly, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Belly_RealExit, new Vector2(11, 39), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Belly_ExitWarp, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Belly_RealExit, new Vector2(6, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.GungeonLightStone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Belly_RealExit, new Vector2(9, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.GungeonLightStone, useExternalPrefab: true));

            Expand_West_Entrance = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_West_Entrance", true);
            Expand_West_Entrance.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_West_Entrance.usesProceduralDecoration = false;
            Expand_West_Entrance.overrideRoomVisualType = 0;
            RoomBuilder.AddObjectToRoom(Expand_West_Entrance, new Vector2(16, 25), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Expand_West_Entrance, new Vector2(34, 25), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Expand_West_Entrance, new Vector2(25, 17), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Expand_West_Entrance, new Vector2(25, 35), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Expand_West_Entrance, new Vector2(9, 42), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.MetalCubeGuyWestGUID);
            RoomBuilder.AddObjectToRoom(Expand_West_Entrance, new Vector2(41, 10), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.MetalCubeGuyWestGUID);
            RoomBuilder.AddObjectToRoom(Expand_West_Entrance, Vector2.one, ExpandPrefabs.West_PuzzleSetupPlacable);



            Expand_West_SecretWarp = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_West_SecretWarp", true);
            Expand_West_SecretWarp.usesProceduralDecoration = false;
            Expand_West_SecretWarp.overrideRoomVisualType = 1;
            
            Expand_West_SecretHub = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_West_SecretHub", true);
            Expand_West_SecretHub.overrideRoomVisualType = 1;
            Expand_West_SecretHub.usesProceduralDecoration = false;

            Expand_West_SecretHub2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_West_SecretHub", true);
            Expand_West_SecretHub2.name = "Expand_West_SecretHub2";
            Expand_West_SecretHub2.overrideRoomVisualType = 2;
            Expand_West_SecretHub2.usesProceduralDecoration = false;
            RoomBuilder.AddObjectToRoom(Expand_West_SecretHub2, Vector2.one, ExpandPrefabs.West_PuzzleSetupPlacable);

            Expand_West_SecretShopWarp = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_West_SecretShopWarp", true);
            Expand_West_SecretShopWarp.overrideRoomVisualType = 2;
            Expand_West_SecretShopWarp.usesProceduralDecoration = false;
            RoomBuilder.AddObjectToRoom(Expand_West_SecretShopWarp, new Vector2(2, 5), ExpandPrefabs.EXRatDoor_4xLocks);
            

            Expand_West_SecretKeyShop = UnityEngine.Object.Instantiate(ExpandPrefabs.shop_special_key_01);
            Expand_West_SecretKeyShop.overrideRoomVisualType = 0;
            Expand_West_SecretKeyShop.category = PrototypeDungeonRoom.RoomCategory.SECRET;

            Expand_West_ChestRoom = UnityEngine.Object.Instantiate(ExpandPrefabs.reward_room);
            Expand_West_ChestRoom.overrideRoomVisualType = 0;

            Expand_West_BlankPedestalRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_West_RatKeyPedestalRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_West_SecretRatKeyPedestalRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_West_ShrineRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            Giant_Elevator_Room = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Utiliroom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Utiliroom_SpecialPit = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Utiliroom_Pitfall = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            SecretRewardRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            SecretBossRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            FakeBossRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            SecretExitRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            PuzzleRoom1 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            PuzzleRoom2 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            PuzzleRoom3 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            CreepyGlitchRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            CreepyGlitchRoom_Entrance = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            GungeoneerMimicBossRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            GungeoneerMimicBossFoyerRoom = UnityEngine.Object.Instantiate(ExpandPrefabs.DragunBossFoyerRoom);
            GungeoneerMimicBossFoyerRoom.overrideRoomVisualType = 2;
            Expand_West_WestBrosBossRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            
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
            Expand_Apache_Hub = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Box_Hub = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Enclose_Hub = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            Expand_Crosshairs = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Basic = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_JumpInThePit = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_LongSpikeTrap = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_SpikeTrap = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_ThinRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_SniperRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_TableRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            Expand_GoopTroop = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_HopScotch = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Pit = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Singer = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_TableRoom2 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_OilRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Walkway = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            Expand_SpiderMaze = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_BlobRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            // Expand_CubeRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_HellInACell = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_IceIsNice = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_IceScotch = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_MrPresident = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_SawRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Agony = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_ice1 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Ice2 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Ice3 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Ice4 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_LargeMany = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Roundabout = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Shells = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Spooky = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Undead1 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Undead2 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Undead3 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Undead4 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();



            Expand_Arena = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_CaptainCrunch = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_CorridorOfDoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_FireRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Pits = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_SkullRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_TableRoomAgain = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();


            ThwompCrossingVertical = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            ThwompCrossingHorizontal = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Apache_FieldOfSaws = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Apache_TheCrushZone = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Apache_SpikeAndPits = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Apache_PitTraps = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Apache_RickRollChest = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Apache_SurpriseChest = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            /*Expand_Casino_Hub = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_Casino_Hub.name = "Expand Casino Hub";
            Expand_Casino_Hub.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Casino_Hub.GUID = Guid.NewGuid().ToString();
            Expand_Casino_Hub.PreventMirroring = false;
            Expand_Casino_Hub.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_Casino_Hub.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Casino_Hub.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Casino_Hub.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Casino_Hub.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Casino_Hub.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Casino_Hub.pits = new List<PrototypeRoomPitEntry>();
            Expand_Casino_Hub.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Casino_Hub.placedObjectPositions = new List<Vector2>();
            Expand_Casino_Hub.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Casino_Hub.roomEvents = new List<RoomEventDefinition>(0);
            Expand_Casino_Hub.overriddenTilesets = 0;
            Expand_Casino_Hub.prerequisites = new List<DungeonPrerequisite>();
            Expand_Casino_Hub.InvalidInCoop = false;
            Expand_Casino_Hub.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Casino_Hub.preventAddedDecoLayering = false;
            Expand_Casino_Hub.preventBorders = false;
            Expand_Casino_Hub.preventFacewallAO = false;
            Expand_Casino_Hub.customAmbientLight = Color.white;
            Expand_Casino_Hub.ForceAllowDuplicates = false;
            Expand_Casino_Hub.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Casino_Hub.IsLostWoodsRoom = false;
            Expand_Casino_Hub.UseCustomMusic = false;
            Expand_Casino_Hub.UseCustomMusicState = false;
            Expand_Casino_Hub.CustomMusicEvent = string.Empty;
            Expand_Casino_Hub.UseCustomMusicSwitch = false;
            Expand_Casino_Hub.CustomMusicSwitch = string.Empty;
            Expand_Casino_Hub.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Casino_Hub.rewardChestSpawnPosition = new IntVector2(5, 5);
            Expand_Casino_Hub.overrideRoomVisualType = 0;
            Expand_Casino_Hub.Width = 16;
            Expand_Casino_Hub.Height = 20;
            Expand_Casino_Hub.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            Expand_Casino_Hub.usesProceduralLighting = false;
            Expand_Casino_Hub.usesCustomAmbientLight = true;
            Expand_Casino_Hub.customAmbientLight = new Color(0.7f, 0.7f, 0.7f, 1);
            Expand_Casino_Hub.allowFloorDecoration = false;
            Expand_Casino_Hub.allowWallDecoration = false;
            Expand_Casino_Hub.usesProceduralDecoration = false;
            Expand_Casino_Hub.drawPrecludedCeilingTiles = false;
            Expand_Casino_Hub.precludeAllTilemapDrawing = true;
            RoomBuilder.AddObjectToRoom(Expand_Casino_Hub, Vector2.zero, ExpandPrefabs.EXCasinoHub);
            RoomBuilder.GenerateBasicRoomLayout(Expand_Casino_Hub);*/


            Expand_Apache_RainbowRoom = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_RainbowRoom", true, false, assignDecorationSettings: true);
            Expand_Apache_RainbowRoom.overrideRoomVisualType = 0;
            Expand_Apache_RainbowRoom.preventAddedDecoLayering = true;
            Expand_Apache_RainbowRoom.precludeAllTilemapDrawing = true;
            Expand_Apache_RainbowRoom.UseCustomMusic = true;
            Expand_Apache_RainbowRoom.UseCustomMusicState = false;
            Expand_Apache_RainbowRoom.CustomMusicEvent = "Play_MUS_Dungeon_State_Winner";
            Expand_Apache_RainbowRoom.UseCustomMusicSwitch = true;
            Expand_Apache_RainbowRoom.CustomMusicSwitch = "Play_EX_UnicornMusic_01";
            Expand_Apache_RainbowRoom.usesProceduralLighting = false;
            Expand_Apache_RainbowRoom.usesCustomAmbientLight = true;
            Expand_Apache_RainbowRoom.customAmbientLight = new Color(0.8f, 0.8f, 0.8f, 1);
            Expand_Apache_RainbowRoom.allowFloorDecoration = false;
            Expand_Apache_RainbowRoom.allowWallDecoration = false;
            Expand_Apache_RainbowRoom.usesProceduralDecoration = false;
            Expand_Apache_RainbowRoom.drawPrecludedCeilingTiles = false;
            RoomBuilder.AddObjectToRoom(Expand_Apache_RainbowRoom, Vector2.zero, ExpandPrefabs.EXSpaceFloor_50x50);
            RoomBuilder.AddObjectToRoom(Expand_Apache_RainbowRoom, Vector2.zero, ExpandPrefabs.EXSpaceFloorPitBorder_50x50);
            if (ExpandPrefabs.EXTrap_Apache.GetComponent<DungeonPlaceableBehaviour>()) {
                RoomBuilder.AddObjectToRoom(Expand_Apache_RainbowRoom, new Vector2(19, 47), null, ExpandPrefabs.EXTrap_Apache.GetComponent<DungeonPlaceableBehaviour>(), PathID: 0, PathStartNode: 0);
                RoomBuilder.AddObjectToRoom(Expand_Apache_RainbowRoom, new Vector2(5, 2), null, ExpandPrefabs.EXTrap_Apache.GetComponent<DungeonPlaceableBehaviour>(), PathID: 0, PathStartNode: 2);
                RoomBuilder.AddObjectToRoom(Expand_Apache_RainbowRoom, new Vector2(45, 30), null, ExpandPrefabs.EXTrap_Apache.GetComponent<DungeonPlaceableBehaviour>(), PathID: 1, PathStartNode: 0);
            } else {
                RoomBuilder.AddObjectToRoom(Expand_Apache_RainbowRoom, new Vector2(19, 47), ExpandPrefabs.EXTrap_Apache, PathID: 0, PathStartNode: 0);
                RoomBuilder.AddObjectToRoom(Expand_Apache_RainbowRoom, new Vector2(5, 2), ExpandPrefabs.EXTrap_Apache, PathID: 0, PathStartNode: 2);
                RoomBuilder.AddObjectToRoom(Expand_Apache_RainbowRoom, new Vector2(45, 30), ExpandPrefabs.EXTrap_Apache, PathID: 1, PathStartNode: 0);
            }
            
            
            // Setup Path for Rainbow Room Trap object (EXTrap_Apache)
            SerializedPath EXTrapApachePath = new SerializedPath(IntVector2.Zero); // Entire node list will be rebuilt so position value used here is not used
            EXTrapApachePath.wrapMode = SerializedPath.SerializedPathWrapMode.Loop;
            // Set this to 0 for Mines (mine cart paths usually). Otherwise set to -1 or leave undefined. (new path defaults as -1). Unless you are a mad lad and figured out how to define a custom tilesetindex grid for your custom floor! :P
            EXTrapApachePath.tilesetPathGrid = -1;
            // Generate path nodes. They must be in a specific order. Placement field is mostly relevent for SawBlade type PathMovers. MineCarts expect all nodes to have SouthWest. (must be unused I guess)
            EXTrapApachePath.nodes = new List<SerializedPathNode>() {
                ExpandUtility.GeneratePathNode(new IntVector2(19, 47), SerializedPathNode.SerializedNodePlacement.NorthEast),
                ExpandUtility.GeneratePathNode(new IntVector2(2, 29), SerializedPathNode.SerializedNodePlacement.NorthWest),
                ExpandUtility.GeneratePathNode(new IntVector2(5, 2), SerializedPathNode.SerializedNodePlacement.SouthWest),
                ExpandUtility.GeneratePathNode(new IntVector2(47, 16), SerializedPathNode.SerializedNodePlacement.SouthEast),
            };


            SerializedPath EXTrapApachePath2 = new SerializedPath(IntVector2.Zero);
            EXTrapApachePath2.wrapMode = SerializedPath.SerializedPathWrapMode.Loop;
            EXTrapApachePath2.tilesetPathGrid = -1;
            EXTrapApachePath2.nodes = new List<SerializedPathNode>() {
                ExpandUtility.GeneratePathNode(new IntVector2(45, 30), SerializedPathNode.SerializedNodePlacement.West),
                ExpandUtility.GeneratePathNode(new IntVector2(12, 30), SerializedPathNode.SerializedNodePlacement.East)
            };
            Expand_Apache_RainbowRoom.paths = new List<SerializedPath>() { EXTrapApachePath, EXTrapApachePath2 };
            


            SecretExitRoom2 = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            SecretRatEntranceRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            Expand_SecretElevatorDestinationRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            Expand_TinySecret = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_TinySecretEmpty = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_GlitchedSecret = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_SecretElevatorEntranceRoom = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();
            Expand_RickRollSecret = ScriptableObject.CreateInstance<PrototypeDungeonRoom>();

            
            Bunny_Special4ShrineRoom = RoomFactory.BuildFromAssetBundle(AssetBundles, "Bunny_Special4ShrineRoom", true, true);
            foreach (PrototypeRoomExit exit in Bunny_Special4ShrineRoom.exitData.exits) { exit.exitType = PrototypeRoomExit.ExitType.ENTRANCE_ONLY; }

            Expand_Keep_TreeRoom = RoomFactory.BuildFromAssetBundle(AssetBundles, "Keep_TreeRoom", true, assignDecorationSettings: true);
            Expand_Keep_TreeRoom.overrideRoomVisualType = 6;
            Expand_Keep_TreeRoom.associatedMinimapIcon = ExpandPrefabs.EXJungleTree_MinimapIcon;
            RoomBuilder.AddObjectToRoom(Expand_Keep_TreeRoom, new Vector2(5, 12), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Jungle_LargeTree, useExternalPrefab: true));

            Expand_Keep_TreeRoom2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Keep_TreeRoom2", true, assignDecorationSettings: true);
            Expand_Keep_TreeRoom2.overrideRoomVisualType = 6;
            Expand_Keep_TreeRoom2.associatedMinimapIcon = ExpandPrefabs.EXJungleTree_MinimapIcon;
            RoomBuilder.AddObjectToRoom(Expand_Keep_TreeRoom2, new Vector2(4, 20), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Jungle_LargeTree, useExternalPrefab: true));


            Expand_Keep_JungleElevatorRoom = RoomFactory.BuildFromAssetBundle(AssetBundles, "Keep_JungleElevatorRoom", true, assignDecorationSettings: true);
            Expand_Keep_JungleElevatorRoom.overrideRoomVisualType = 2;
            RoomBuilder.AddObjectToRoom(Expand_Keep_JungleElevatorRoom, new Vector2(7, 15), ExpandPrefabs.EXJungleElevator_Departure_Placable);
            RoomBuilder.AddObjectToRoom(Expand_Keep_JungleElevatorRoom, new Vector2(6, 3), ExpandObjectDatabase.GodRays);
            // RoomBuilder.AddObjectToRoom(Expand_Keep_JungleElevatorRoom, new Vector2(9, 6), ExpandPrefabs.Arrival);

            Expand_ExitRoom_NewElevator = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_ExitRoom_NewElevator", true);
            Expand_ExitRoom_NewElevator.associatedMinimapIcon = ExpandPrefabs.exit_room_basic.associatedMinimapIcon;
            RoomBuilder.AddObjectToRoom(Expand_ExitRoom_NewElevator, new Vector2(5, 7), ExpandPrefabs.EXPortableElevator_Departure_Placable);

            Expand_West_BlankPedestalRoom.name = "Blank Reward Room";
            Expand_West_BlankPedestalRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_West_BlankPedestalRoom.GUID = Guid.NewGuid().ToString();
            Expand_West_BlankPedestalRoom.PreventMirroring = false;
            Expand_West_BlankPedestalRoom.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_West_BlankPedestalRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_West_BlankPedestalRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_West_BlankPedestalRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_West_BlankPedestalRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_West_BlankPedestalRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_West_BlankPedestalRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_West_BlankPedestalRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_West_BlankPedestalRoom.placedObjectPositions = new List<Vector2>();
            Expand_West_BlankPedestalRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_West_BlankPedestalRoom.roomEvents = new List<RoomEventDefinition>(0);
            Expand_West_BlankPedestalRoom.overriddenTilesets = 0;
            Expand_West_BlankPedestalRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_West_BlankPedestalRoom.InvalidInCoop = false;
            Expand_West_BlankPedestalRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_West_BlankPedestalRoom.preventAddedDecoLayering = false;
            Expand_West_BlankPedestalRoom.precludeAllTilemapDrawing = false;
            Expand_West_BlankPedestalRoom.drawPrecludedCeilingTiles = false;
            Expand_West_BlankPedestalRoom.preventBorders = false;
            Expand_West_BlankPedestalRoom.preventFacewallAO = false;
            Expand_West_BlankPedestalRoom.usesCustomAmbientLight = false;
            Expand_West_BlankPedestalRoom.customAmbientLight = Color.white;
            Expand_West_BlankPedestalRoom.ForceAllowDuplicates = false;
            Expand_West_BlankPedestalRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_West_BlankPedestalRoom.IsLostWoodsRoom = false;
            Expand_West_BlankPedestalRoom.UseCustomMusic = false;
            Expand_West_BlankPedestalRoom.UseCustomMusicState = false;
            Expand_West_BlankPedestalRoom.CustomMusicEvent = string.Empty;
            Expand_West_BlankPedestalRoom.UseCustomMusicSwitch = false;
            Expand_West_BlankPedestalRoom.CustomMusicSwitch = string.Empty;
            Expand_West_BlankPedestalRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_West_BlankPedestalRoom.rewardChestSpawnPosition = IntVector2.One;
            Expand_West_BlankPedestalRoom.Width = 4;
            Expand_West_BlankPedestalRoom.Height = 4;
            Expand_West_BlankPedestalRoom.usesProceduralDecoration = false;
            Expand_West_BlankPedestalRoom.overrideRoomVisualType = 0;
            RoomBuilder.AddExitToRoom(Expand_West_BlankPedestalRoom, new Vector2(0, 2), DungeonData.Direction.WEST, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_West_BlankPedestalRoom, new Vector2(2, 0), DungeonData.Direction.SOUTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_West_BlankPedestalRoom, new Vector2(2, 5), DungeonData.Direction.NORTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_West_BlankPedestalRoom, new Vector2(5, 2), DungeonData.Direction.EAST, ContainsDoor: false);
            RoomBuilder.AddObjectToRoom(Expand_West_BlankPedestalRoom, Vector2.one, ExpandPrefabs.BlankRewardPedestal, 3, 8);
            RoomBuilder.GenerateBasicRoomLayout(Expand_West_BlankPedestalRoom);

            Expand_West_RatKeyPedestalRoom.name = "Special Key Reward Room";
            Expand_West_RatKeyPedestalRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_West_RatKeyPedestalRoom.GUID = Guid.NewGuid().ToString();
            Expand_West_RatKeyPedestalRoom.PreventMirroring = false;
            Expand_West_RatKeyPedestalRoom.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_West_RatKeyPedestalRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_West_RatKeyPedestalRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_West_RatKeyPedestalRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_West_RatKeyPedestalRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_West_RatKeyPedestalRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_West_RatKeyPedestalRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_West_RatKeyPedestalRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_West_RatKeyPedestalRoom.placedObjectPositions = new List<Vector2>();
            Expand_West_RatKeyPedestalRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_West_RatKeyPedestalRoom.roomEvents = new List<RoomEventDefinition>(0);
            Expand_West_RatKeyPedestalRoom.overriddenTilesets = 0;
            Expand_West_RatKeyPedestalRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_West_RatKeyPedestalRoom.InvalidInCoop = false;
            Expand_West_RatKeyPedestalRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_West_RatKeyPedestalRoom.preventAddedDecoLayering = false;
            Expand_West_RatKeyPedestalRoom.precludeAllTilemapDrawing = false;
            Expand_West_RatKeyPedestalRoom.drawPrecludedCeilingTiles = false;
            Expand_West_RatKeyPedestalRoom.preventBorders = false;
            Expand_West_RatKeyPedestalRoom.preventFacewallAO = false;
            Expand_West_RatKeyPedestalRoom.usesCustomAmbientLight = false;
            Expand_West_RatKeyPedestalRoom.customAmbientLight = Color.white;
            Expand_West_RatKeyPedestalRoom.ForceAllowDuplicates = false;
            Expand_West_RatKeyPedestalRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_West_RatKeyPedestalRoom.IsLostWoodsRoom = false;
            Expand_West_RatKeyPedestalRoom.UseCustomMusic = false;
            Expand_West_RatKeyPedestalRoom.UseCustomMusicState = false;
            Expand_West_RatKeyPedestalRoom.CustomMusicEvent = string.Empty;
            Expand_West_RatKeyPedestalRoom.UseCustomMusicSwitch = false;
            Expand_West_RatKeyPedestalRoom.CustomMusicSwitch = string.Empty;
            Expand_West_RatKeyPedestalRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_West_RatKeyPedestalRoom.rewardChestSpawnPosition = IntVector2.One;
            Expand_West_RatKeyPedestalRoom.Width = 4;
            Expand_West_RatKeyPedestalRoom.Height = 4;
            Expand_West_RatKeyPedestalRoom.usesProceduralDecoration = false;
            Expand_West_RatKeyPedestalRoom.overrideRoomVisualType = 0;
            RoomBuilder.AddExitToRoom(Expand_West_RatKeyPedestalRoom, new Vector2(0, 2), DungeonData.Direction.WEST, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_West_RatKeyPedestalRoom, new Vector2(2, 0), DungeonData.Direction.SOUTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_West_RatKeyPedestalRoom, new Vector2(2, 5), DungeonData.Direction.NORTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_West_RatKeyPedestalRoom, new Vector2(5, 2), DungeonData.Direction.EAST, ContainsDoor: false);
            RoomBuilder.AddObjectToRoom(Expand_West_RatKeyPedestalRoom, Vector2.one, ExpandPrefabs.RatKeyRewardPedestal, 3, 8);
            RoomBuilder.GenerateBasicRoomLayout(Expand_West_RatKeyPedestalRoom);

            Expand_West_SecretRatKeyPedestalRoom.name = "Secret Special Key Reward Room";
            Expand_West_SecretRatKeyPedestalRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_West_SecretRatKeyPedestalRoom.GUID = Guid.NewGuid().ToString();
            Expand_West_SecretRatKeyPedestalRoom.PreventMirroring = false;
            Expand_West_SecretRatKeyPedestalRoom.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_West_SecretRatKeyPedestalRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_West_SecretRatKeyPedestalRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_West_SecretRatKeyPedestalRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_West_SecretRatKeyPedestalRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_West_SecretRatKeyPedestalRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_West_SecretRatKeyPedestalRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_West_SecretRatKeyPedestalRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_West_SecretRatKeyPedestalRoom.placedObjectPositions = new List<Vector2>();
            Expand_West_SecretRatKeyPedestalRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_West_SecretRatKeyPedestalRoom.roomEvents = new List<RoomEventDefinition>(0);
            Expand_West_SecretRatKeyPedestalRoom.overriddenTilesets = 0;
            Expand_West_SecretRatKeyPedestalRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_West_SecretRatKeyPedestalRoom.InvalidInCoop = false;
            Expand_West_SecretRatKeyPedestalRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_West_SecretRatKeyPedestalRoom.preventAddedDecoLayering = false;
            Expand_West_SecretRatKeyPedestalRoom.precludeAllTilemapDrawing = false;
            Expand_West_SecretRatKeyPedestalRoom.drawPrecludedCeilingTiles = false;
            Expand_West_SecretRatKeyPedestalRoom.preventBorders = false;
            Expand_West_SecretRatKeyPedestalRoom.preventFacewallAO = false;
            Expand_West_SecretRatKeyPedestalRoom.usesCustomAmbientLight = false;
            Expand_West_SecretRatKeyPedestalRoom.customAmbientLight = Color.white;
            Expand_West_SecretRatKeyPedestalRoom.ForceAllowDuplicates = false;
            Expand_West_SecretRatKeyPedestalRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_West_SecretRatKeyPedestalRoom.IsLostWoodsRoom = false;
            Expand_West_SecretRatKeyPedestalRoom.UseCustomMusic = false;
            Expand_West_SecretRatKeyPedestalRoom.UseCustomMusicState = false;
            Expand_West_SecretRatKeyPedestalRoom.CustomMusicEvent = string.Empty;
            Expand_West_SecretRatKeyPedestalRoom.UseCustomMusicSwitch = false;
            Expand_West_SecretRatKeyPedestalRoom.CustomMusicSwitch = string.Empty;
            Expand_West_SecretRatKeyPedestalRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_West_SecretRatKeyPedestalRoom.rewardChestSpawnPosition = IntVector2.One;
            Expand_West_SecretRatKeyPedestalRoom.Width = 4;
            Expand_West_SecretRatKeyPedestalRoom.Height = 4;
            Expand_West_SecretRatKeyPedestalRoom.usesProceduralDecoration = false;
            Expand_West_SecretRatKeyPedestalRoom.overrideRoomVisualType = 0;
            RoomBuilder.AddExitToRoom(Expand_West_SecretRatKeyPedestalRoom, new Vector2(0, 2), DungeonData.Direction.WEST, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_West_SecretRatKeyPedestalRoom, new Vector2(2, 0), DungeonData.Direction.SOUTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_West_SecretRatKeyPedestalRoom, new Vector2(2, 5), DungeonData.Direction.NORTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_West_SecretRatKeyPedestalRoom, new Vector2(5, 2), DungeonData.Direction.EAST, ContainsDoor: false);
            RoomBuilder.AddObjectToRoom(Expand_West_SecretRatKeyPedestalRoom, Vector2.one, ExpandPrefabs.RatKeyRewardPedestal, 3, 8);
            RoomBuilder.GenerateBasicRoomLayout(Expand_West_SecretRatKeyPedestalRoom);


            ExpandUtility.DuplicateComponent(Expand_West_ShrineRoom, ExpandPrefabs.letsgetsomeshrines_001);
            Expand_West_ShrineRoom.name = "Old West Shrine Room";
            Expand_West_ShrineRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_West_ShrineRoom.GUID = Guid.NewGuid().ToString();
            Expand_West_ShrineRoom.overrideRoomVisualType = 0;
            RoomBuilder.GenerateBasicRoomLayout(Expand_West_ShrineRoom);

            Expand_West_WestBrosBossRoom.name = "West Bros Boss Room";
            Expand_West_WestBrosBossRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_West_WestBrosBossRoom.GUID = Guid.NewGuid().ToString();
            Expand_West_WestBrosBossRoom.PreventMirroring = false;
            Expand_West_WestBrosBossRoom.category = PrototypeDungeonRoom.RoomCategory.BOSS;
            Expand_West_WestBrosBossRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_West_WestBrosBossRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_West_WestBrosBossRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_West_WestBrosBossRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_West_WestBrosBossRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_West_WestBrosBossRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_West_WestBrosBossRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_West_WestBrosBossRoom.placedObjectPositions = new List<Vector2>();
            Expand_West_WestBrosBossRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_West_WestBrosBossRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_West_WestBrosBossRoom.overriddenTilesets = 0;
            Expand_West_WestBrosBossRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_West_WestBrosBossRoom.InvalidInCoop = false;
            Expand_West_WestBrosBossRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_West_WestBrosBossRoom.preventAddedDecoLayering = false;
            Expand_West_WestBrosBossRoom.precludeAllTilemapDrawing = false;
            Expand_West_WestBrosBossRoom.drawPrecludedCeilingTiles = false;
            Expand_West_WestBrosBossRoom.preventBorders = false;
            Expand_West_WestBrosBossRoom.preventFacewallAO = false;
            Expand_West_WestBrosBossRoom.usesCustomAmbientLight = false;
            Expand_West_WestBrosBossRoom.customAmbientLight = Color.white;
            Expand_West_WestBrosBossRoom.ForceAllowDuplicates = false;
            Expand_West_WestBrosBossRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_West_WestBrosBossRoom.IsLostWoodsRoom = false;
            Expand_West_WestBrosBossRoom.UseCustomMusic = false;
            Expand_West_WestBrosBossRoom.UseCustomMusicState = false;
            Expand_West_WestBrosBossRoom.CustomMusicEvent = string.Empty;
            Expand_West_WestBrosBossRoom.UseCustomMusicSwitch = false;
            Expand_West_WestBrosBossRoom.CustomMusicSwitch = string.Empty;
            Expand_West_WestBrosBossRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_West_WestBrosBossRoom.rewardChestSpawnPosition = new IntVector2(18, 21);
            Expand_West_WestBrosBossRoom.Width = 38;
            Expand_West_WestBrosBossRoom.Height = 27;
            Expand_West_WestBrosBossRoom.overrideRoomVisualType = 1;
            Expand_West_WestBrosBossRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "5861e5a077244905a8c25c2b7b4d6ebb", // Bullet Kin Cowboy
                            contentsBasePosition = new Vector2(9, 16),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "5861e5a077244905a8c25c2b7b4d6ebb", // Bullet Kin Cowboy
                            contentsBasePosition = new Vector2(12, 20),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "05cb719e0178478685dc610f8b3e8bfc", // Bullet Kin Explosive Vest
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
                            enemyBehaviourGuid = "ddf12a4881eb43cfba04f36dd6377abb", // Shotgun Kin Cowboy
                            contentsBasePosition = new Vector2(9, 11),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "05cb719e0178478685dc610f8b3e8bfc", // Bullet Kin Explosive Vest
                            contentsBasePosition = new Vector2(12, 13),
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
                        new Vector2(9, 16),
                        new Vector2(12, 20),
                        new Vector2(12, 7),
                        new Vector2(9, 11),
                        new Vector2(12, 13)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 15,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ENEMY_BEHAVIOR,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                },
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ddf12a4881eb43cfba04f36dd6377abb", // Shotgun Kin Cowboy
                            contentsBasePosition = new Vector2(26, 20),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "05cb719e0178478685dc610f8b3e8bfc", // Bullet Kin Explosive Vest
                            contentsBasePosition = new Vector2(25, 9),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "5861e5a077244905a8c25c2b7b4d6ebb", // Bullet Kin Cowboy
                            contentsBasePosition = new Vector2(28, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "5861e5a077244905a8c25c2b7b4d6ebb", // Bullet Kin Cowboy
                            contentsBasePosition = new Vector2(28, 12),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },                        
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ddf12a4881eb43cfba04f36dd6377abb", // Shotgun Kin Cowboy
                            contentsBasePosition = new Vector2(25, 15),
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
                        new Vector2(26, 20),
                        new Vector2(25, 9),
                        new Vector2(28, 17),
                        new Vector2(28, 12),
                        new Vector2(25, 15)
                    },
                    layerIsReinforcementLayer = true,
                    shuffle = false,
                    randomize = 0,
                    suppressPlayerChecks = true,
                    delayTime = 15,
                    reinforcementTriggerCondition = RoomEventTriggerCondition.ENEMY_BEHAVIOR,
                    probability = 1,
                    numberTimesEncounteredRequired = 0
                }
            };
            Expand_West_WestBrosBossRoom.associatedMinimapIcon = ExpandPrefabs.doublebeholsterroom01.associatedMinimapIcon;
            RoomBuilder.AddExitToRoom(Expand_West_WestBrosBossRoom, new Vector2(0, 14), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_West_WestBrosBossRoom, new Vector2(39, 14), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_West_WestBrosBossRoom, new Vector2(19, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_West_WestBrosBossRoom, new Vector2(19, 28), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_West_WestBrosBossRoom, new Vector2(16, 15), EnemyBehaviourGuid: ExpandWesternBrosPrefabBuilder.WestBrosNomeGUID);
            RoomBuilder.AddObjectToRoom(Expand_West_WestBrosBossRoom, new Vector2(18, 18), EnemyBehaviourGuid: ExpandWesternBrosPrefabBuilder.WestBrosAngelGUID);
            RoomBuilder.AddObjectToRoom(Expand_West_WestBrosBossRoom, new Vector2(20, 15), EnemyBehaviourGuid: ExpandWesternBrosPrefabBuilder.WestBrosTucGUID);
            RoomBuilder.GenerateBasicRoomLayout(Expand_West_WestBrosBossRoom);
                        
            
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
            RoomBuilder.AddExitToRoom(FakeBossRoom, new Vector2(0, 12), DungeonData.Direction.WEST, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomBuilder.AddExitToRoom(FakeBossRoom, new Vector2(12, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(FakeBossRoom, new Vector2(26, 12), DungeonData.Direction.EAST, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomBuilder.AddExitToRoom(FakeBossRoom, new Vector2(12, 26), DungeonData.Direction.NORTH, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomBuilder.GenerateBasicRoomLayout(FakeBossRoom);
            RoomBuilder.AddObjectToRoom(FakeBossRoom, new Vector2(8, 18), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5");
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
            Giant_Elevator_Room.overrideRoomVisualType = 0;
            // Left/Right Exits
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 5), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 5), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 14), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 14), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 23), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 23), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 32), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 32), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 41), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 41), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 50), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 50), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 59), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 59), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 68), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 68), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 77), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 77), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 86), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 86), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(0, 95), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(101, 95), DungeonData.Direction.EAST);
            // Top/Bottom Exits
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(5, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(5, 101), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(14, 101), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(23, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(23, 101), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(32, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(32, 101), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(41, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(41, 101), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(50, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(50, 101), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(59, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(59, 101), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(68, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(68, 101), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(77, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(77, 101), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(86, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(86, 101), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(95, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Giant_Elevator_Room, new Vector2(95, 101), DungeonData.Direction.NORTH);
            // Add Object Spawns
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(47, 49), ExpandPrefabs.ElevatorArrival);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(48, 41), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(49, 33), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(49, 66), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(29, 49), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(70, 49), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(17, 4), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(28, 4), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(49, 4), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(69, 4), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(80, 4), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(17, 96), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(28, 96), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(49, 96), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(69, 96), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(80, 96), ExpandPrefabs.Door_Vertical_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(3, 16), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(3, 32), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(3, 49), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(3, 66), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(3, 82), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(96, 16), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(96, 32), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(96, 49), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(96, 66), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(96, 82), ExpandPrefabs.Door_Horizontal_West);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(7, 24), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // Blobulon
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(45, 13), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // Bullet Kin
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(87, 17), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // Bullet Kin
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(14, 23), EnemyBehaviourGuid: "db35531e66ce41cbb81d507a34366dfe"); // AK47 Bullet Kin
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(13, 60), EnemyBehaviourGuid: "2752019b770f473193b08b4005dc781f"); // Veteran Shotgun Kin
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(85, 74), EnemyBehaviourGuid: "e861e59012954ab2b9b6977da85cb83c"); // Snake (Office)
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(85, 49), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // Gun Nut
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(28, 77), EnemyBehaviourGuid: "eed5addcc15148179f300cc0d9ee7f94"); // Spogre
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(63, 84), EnemyBehaviourGuid: "98fdf153a4dd4d51bf0bafe43f3c77ff"); // Tazie
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(35, 91), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // Bullet Kin
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(60, 90), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // Bullet Kin
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(35, 85), EnemyBehaviourGuid: "70216cae6c1346309d86d4a0b4603045"); // Veteran Bullet Kin
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(75, 8), EnemyBehaviourGuid: "c5b11bfc065d417b9c4d03a5e385fe2c"); // Professional
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(72, 86), EnemyBehaviourGuid: "3b0bd258b4c9432db3339665cc61c356"); // Cactus Kin
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(12, 39), EnemyBehaviourGuid: "3b0bd258b4c9432db3339665cc61c356"); // Cactus Kin
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(15, 14), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(85, 14), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(15, 86), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(85, 86), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(59, 67), EnemyBehaviourGuid: "479556d05c7c44f3b6abb3b2067fc778"); // Wall Mimic
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(13, 89), ExpandObjectDatabase.YellowDrum);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(84, 89), ExpandObjectDatabase.YellowDrum);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(14, 10), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(84, 10), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(45, 10), ExpandObjectDatabase.Brazier);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(45, 89), ExpandObjectDatabase.Brazier);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(9, 62), ExpandObjectDatabase.Brazier);
            RoomBuilder.AddObjectToRoom(Giant_Elevator_Room, new Vector2(89, 62), ExpandObjectDatabase.Brazier);
            // RoomFromText.AddObjectToRoom(Giant_Elevator_Room, new Vector2(50, 55), ChaosGlitchFloorGenerator.Instance.CustomGlitchDungeonPlacable(ChaosPrefabs.RainFXObject, useExternalPrefab: true));
            // Generate Cell Data
            RoomBuilder.GenerateRoomLayout(Giant_Elevator_Room, "Giant_Elevator_Room_Layout");


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
            RoomBuilder.AddExitToRoom(Utiliroom, new Vector2(0, 2), DungeonData.Direction.WEST, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Utiliroom, new Vector2(2, 0), DungeonData.Direction.SOUTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Utiliroom, new Vector2(2, 5), DungeonData.Direction.NORTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Utiliroom, new Vector2(5, 2), DungeonData.Direction.EAST, ContainsDoor: false);
            RoomBuilder.GenerateBasicRoomLayout(Utiliroom);

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
            RoomBuilder.AddExitToRoom(Utiliroom_SpecialPit, new Vector2(1, 4), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Utiliroom_SpecialPit, new Vector2(8, 4), DungeonData.Direction.EAST);
            RoomBuilder.GenerateRoomLayout(Utiliroom_SpecialPit, "Utiliroom_SpecialPit_Layout");


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
            RoomBuilder.AddExitToRoom(Utiliroom_Pitfall, new Vector2(0, 4), DungeonData.Direction.WEST, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Utiliroom_Pitfall, new Vector2(4, 0), DungeonData.Direction.SOUTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Utiliroom_Pitfall, new Vector2(4, 9), DungeonData.Direction.NORTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Utiliroom_Pitfall, new Vector2(9, 4), DungeonData.Direction.EAST, ContainsDoor: false);
            RoomBuilder.GenerateRoomLayout(Utiliroom_Pitfall, "Utiliroom_Pitfall_Layout");

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
            SecretRewardRoom.Height = 66;
            SecretRewardRoom.usesProceduralDecoration = false;
            SecretRewardRoom.overrideRoomVisualType = 2;
            RoomBuilder.AddExitToRoom(SecretRewardRoom, new Vector2(0, 2), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(SecretRewardRoom, new Vector2(10, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(SecretRewardRoom, new Vector2(21, 2), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(SecretRewardRoom, new Vector2(0, 31), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(SecretRewardRoom, new Vector2(21, 31), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(SecretRewardRoom, new Vector2(0, 60), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(SecretRewardRoom, new Vector2(21, 60), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(SecretRewardRoom, new Vector2(10, 67), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(SecretRewardRoom, new Vector2(8, 2), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Teleporter_Gungeon_01, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 10), ExpandPrefabs.EXRatDoor_4xLocks);
            RoomBuilder.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 21), ExpandPrefabs.RatJailDoor);
            RoomBuilder.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 26), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 34), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 46), ExpandPrefabs.RatJailDoor);
            RoomBuilder.AddObjectToRoom(SecretRewardRoom, new Vector2(9, 15), ExpandPrefabs.RatKeyRewardPedestal);
            RoomBuilder.AddObjectToRoom(SecretRewardRoom, Vector2.one, ExpandPrefabs.West_PuzzleSetupPlacable);
            RoomBuilder.GenerateRoomLayout(SecretRewardRoom, "Secret_Reward_Room_Layout");

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
            RoomBuilder.AddExitToRoom(SecretBossRoom, new Vector2(17, 0), DungeonData.Direction.SOUTH, PrototypeRoomExit.ExitType.ENTRANCE_ONLY, exitSize: 4);
            RoomBuilder.AddExitToRoom(SecretBossRoom, new Vector2(18, 31), DungeonData.Direction.NORTH, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomBuilder.AddObjectToRoom(SecretBossRoom, Vector2.one, ExpandPrefabs.West_PuzzleSetupPlacable);
            RoomBuilder.GenerateBasicRoomLayout(SecretBossRoom);


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
            RoomBuilder.AddExitToRoom(SecretExitRoom, new Vector2(0, 2), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(SecretExitRoom, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(SecretExitRoom, new Vector2(9, 2), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(SecretExitRoom, new Vector2(1, 6), ExpandPrefabs.ElevatorDeparture);
            RoomBuilder.AddObjectToRoom(SecretExitRoom, new Vector2(2, 0), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Teleporter_Gungeon_01, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(SecretExitRoom, new Vector2(3, 1), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Arrival, useExternalPrefab: true));
            RoomBuilder.GenerateRoomLayout(SecretExitRoom, "Secret_Exit_Room_Layout");


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
            RoomBuilder.AddExitToRoom(ThwompCrossingVertical, new Vector2(7, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(ThwompCrossingVertical, new Vector2(7, 31), DungeonData.Direction.NORTH);
            /*RoomFromText.AddExitToRoom(ThwompCrossingVertical, new Vector2(0, 2), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(ThwompCrossingVertical, new Vector2(0, 26), DungeonData.Direction.WEST);
            RoomFromText.AddExitToRoom(ThwompCrossingVertical, new Vector2(15, 2), DungeonData.Direction.EAST);
            RoomFromText.AddExitToRoom(ThwompCrossingVertical, new Vector2(15, 26), DungeonData.Direction.EAST);*/
            RoomBuilder.AddObjectToRoom(ThwompCrossingVertical, new Vector2(11, 7), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomBuilder.AddObjectToRoom(ThwompCrossingVertical, new Vector2(11, 16), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomBuilder.AddObjectToRoom(ThwompCrossingVertical, new Vector2(11, 11), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomBuilder.AddObjectToRoom(ThwompCrossingVertical, new Vector2(11, 22), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomBuilder.GenerateRoomLayout(ThwompCrossingVertical, "Expand_Thwomp_Crossing_Vertical_Layout");
            
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
            RoomBuilder.AddExitToRoom(ThwompCrossingHorizontal, new Vector2(0, 7), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(ThwompCrossingHorizontal, new Vector2(31, 7), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(ThwompCrossingHorizontal, new Vector2(7, 12), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomBuilder.AddObjectToRoom(ThwompCrossingHorizontal, new Vector2(11, 12), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomBuilder.AddObjectToRoom(ThwompCrossingHorizontal, new Vector2(16, 12), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomBuilder.AddObjectToRoom(ThwompCrossingHorizontal, new Vector2(21, 12), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // Metal Cube Guy (trap version)
            RoomBuilder.GenerateRoomLayout(ThwompCrossingHorizontal, "Expand_Thwomp_Crossing_Horizontal_Layout");



            ExpandUtility.DuplicateComponent(PuzzleRoom1, ExpandPrefabs.gungeon_checkerboard);
            PuzzleRoom1.name = "Zelda Puzzle Room 1";
            PuzzleRoom1.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            PuzzleRoom1.GUID = Guid.NewGuid().ToString();
            PuzzleRoom1.overrideRoomVisualType = 2;
            RoomBuilder.AddObjectToRoom(PuzzleRoom1, Vector2.one, ExpandPrefabs.West_PuzzleSetupPlacable);
            RoomBuilder.GenerateBasicRoomLayout(PuzzleRoom1);


            ExpandUtility.DuplicateComponent(PuzzleRoom2, ExpandPrefabs.gungeon_normal_fightinaroomwithtonsoftraps);
            PuzzleRoom2.name = "Zelda Puzzle Room 2";
            PuzzleRoom2.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            PuzzleRoom2.GUID = Guid.NewGuid().ToString();
            PuzzleRoom2.overrideRoomVisualType = 2;
            RoomBuilder.AddObjectToRoom(PuzzleRoom2, Vector2.one, ExpandPrefabs.West_PuzzleSetupPlacable);
            PuzzleRoom2.UpdatePrecalculatedData();

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
            PuzzleRoom3.pits = new List<PrototypeRoomPitEntry>(0);
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
            PuzzleRoom3.overrideRoomVisualType = 2;
            RoomBuilder.AddExitToRoom(PuzzleRoom3, new Vector2(0, 10), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(PuzzleRoom3, new Vector2(39, 10), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(PuzzleRoom3, Vector2.one, ExpandPrefabs.West_PuzzleSetupPlacable);
            RoomBuilder.GenerateRoomLayout(PuzzleRoom3, "Zelda_Puzzle_Room_3_Layout");


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
            RoomBuilder.AddExitToRoom(CreepyGlitchRoom, new Vector2(0, 13), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(CreepyGlitchRoom, new Vector2(27, 13), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(CreepyGlitchRoom, new Vector2(13, 1), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(CreepyGlitchRoom, new Vector2(12, 23), EnemyBehaviourGuid: "5f0fa34b5a2e44cdab4a06f89bb5c442");
            // RoomBuilder.AddObjectToRoom(CreepyGlitchRoom, new Vector2(12, 23), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXPlayerMimicBoss, useExternalPrefab: true));
            // RoomFromText.AddObjectToRoom(CreepyGlitchRoom, new Vector2(13, 13), ExpandUtility.GenerateDungeonPlacable(ExpandPrefab.RoomCorruptionAmbience, useExternalPrefab: true));
            // RoomBuilder.GenerateRoomLayout(CreepyGlitchRoom, "CreepyGlitchRoom_Layout");
            RoomBuilder.GenerateRoomLayout(CreepyGlitchRoom, "Creepy_Glitched_Room_Layout");

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
            RoomBuilder.AddExitToRoom(CreepyGlitchRoom_Entrance, new Vector2(9, 13), DungeonData.Direction.EAST, overrideDoorObject: ExpandPrefabs.boss_foyertable.includedRooms.elements[1].room.exitData.exits[3].specifiedDoor);
            RoomBuilder.AddObjectToRoom(CreepyGlitchRoom_Entrance, new Vector2(2, 4), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Teleporter_Gungeon_01, useExternalPrefab: true));
            RoomBuilder.GenerateBasicRoomLayout(CreepyGlitchRoom_Entrance);

            GungeoneerMimicBossRoom.name = "Creepy Mirror Boss Room";
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
            GungeoneerMimicBossRoom.overrideRoomVisualType = 2;
            GungeoneerMimicBossRoom.associatedMinimapIcon = ExpandPrefabs.doublebeholsterroom01.associatedMinimapIcon;
            // RoomBuilder.AddExitToRoom(GungeoneerMimicBossRoom, new Vector2(0, 16), DungeonData.Direction.WEST);
            // RoomBuilder.AddExitToRoom(GungeoneerMimicBossRoom, new Vector2(33, 16), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(GungeoneerMimicBossRoom, new Vector2(16, 33), DungeonData.Direction.NORTH, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomBuilder.AddExitToRoom(GungeoneerMimicBossRoom, new Vector2(15, 0), DungeonData.Direction.SOUTH, PrototypeRoomExit.ExitType.ENTRANCE_ONLY, exitSize: 4);
            // RoomBuilder.AddObjectToRoom(GungeoneerMimicBossRoom, new Vector2(16, 29), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXPlayerMimicBoss, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(GungeoneerMimicBossRoom, new Vector2(16, 29), EnemyBehaviourGuid: "5f0fa34b5a2e44cdab4a06f89bb5c442");
            RoomBuilder.GenerateRoomLayout(GungeoneerMimicBossRoom, "Creepy_MirrorBoss_Room_Layout");

            // Castle Custom Rooms
            Expand_Explode.name = "Expand TurtleMelon Explode";
            Expand_Explode.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Explode.GUID = Guid.NewGuid().ToString();
            Expand_Explode.PreventMirroring = false;
            Expand_Explode.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Explode.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Explode.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Explode, new Vector2(0, 10), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Explode, new Vector2(29, 10), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Explode, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Explode, new Vector2(14, 22), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Explode, new Vector2(17, 17), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Explode, new Vector2(8, 6), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Explode, new Vector2(4, 9), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Explode, new Vector2(4, 11.5f), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Explode, new Vector2(6, 19), EnemyBehaviourGuid: "206405acad4d4c33aac6717d184dc8d4"); // apprentice_gunjurer
            RoomBuilder.AddObjectToRoom(Expand_Explode, new Vector2(23, 3), EnemyBehaviourGuid: "ed37fa13e0fa4fcf8239643957c51293"); // gigi
            RoomBuilder.AddObjectToRoom(Expand_Explode, new Vector2(7, 11), EnemyBehaviourGuid: "cf2b7021eac44e3f95af07db9a7c442c"); // LeadWizard (gunsinger)
            RoomBuilder.GenerateRoomLayout(Expand_Explode, "Expand_TurtleMelon_Explode_Layout");


            Expand_C_Hub.name = "Expand TurtleMelon C Hub";
            Expand_C_Hub.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_C_Hub.GUID = Guid.NewGuid().ToString();
            Expand_C_Hub.PreventMirroring = false;
            Expand_C_Hub.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_C_Hub.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_C_Hub.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_C_Hub, new Vector2(0, 20), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_C_Hub, new Vector2(41, 20), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_C_Hub, new Vector2(7, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_C_Hub, new Vector2(30, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_C_Hub, new Vector2(7, 41), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_C_Hub, new Vector2(30, 41), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_C_Hub, new Vector2(18, 14), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddObjectToRoom(Expand_C_Hub, new Vector2(20, 2), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomBuilder.AddObjectToRoom(Expand_C_Hub, new Vector2(20, 37), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomBuilder.AddObjectToRoom(Expand_C_Hub, new Vector2(5, 20), EnemyBehaviourGuid: "ed37fa13e0fa4fcf8239643957c51293"); // gigi
            RoomBuilder.AddObjectToRoom(Expand_C_Hub, new Vector2(33, 20), EnemyBehaviourGuid: "ed37fa13e0fa4fcf8239643957c51293"); // gigi
            RoomBuilder.GenerateRoomLayout(Expand_C_Hub, "Expand_TurtleMelon_C_Hub_Layout");


            Expand_C_Gap.name = "Expand TurtleMelon C Gap";
            Expand_C_Gap.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_C_Gap.GUID = Guid.NewGuid().ToString();
            Expand_C_Gap.PreventMirroring = false;
            Expand_C_Gap.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_C_Gap.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_C_Gap.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_C_Gap, new Vector2(0, 1), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_C_Gap, new Vector2(0, 20), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_C_Gap, new Vector2(29, 10), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_C_Gap, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_C_Gap, new Vector2(14, 22), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_C_Gap, new Vector2(9, 1), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_C_Gap, new Vector2(9, 19), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_C_Gap, new Vector2(13, 6), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_C_Gap, new Vector2(13, 14), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_C_Gap, new Vector2(4, 7), EnemyBehaviourGuid: "8bb5578fba374e8aae8e10b754e61d62"); // cardinal
            RoomBuilder.AddObjectToRoom(Expand_C_Gap, new Vector2(3, 13), EnemyBehaviourGuid: "8bb5578fba374e8aae8e10b754e61d62"); // cardinal
            RoomBuilder.GenerateRoomLayout(Expand_C_Gap, "Expand_TurtleMelon_C_Gap_Layout");


            Expand_ChainGap.name = "Expand TurtleMelon Chain Gap";
            Expand_ChainGap.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_ChainGap.GUID = Guid.NewGuid().ToString();
            Expand_ChainGap.PreventMirroring = false;
            Expand_ChainGap.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_ChainGap.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_ChainGap.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_ChainGap, new Vector2(0, 10), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_ChainGap, new Vector2(29, 10), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_ChainGap, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_ChainGap, new Vector2(14, 22), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(13.25f, 3), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(13.25f, 17), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(4, 9), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(22, 9), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(9, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(16, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(9, 17), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(16, 17), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(4, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(4, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(22, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(22, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(13, 8), EnemyBehaviourGuid: "463d16121f884984abe759de38418e48"); // chain_gunner
            RoomBuilder.AddObjectToRoom(Expand_ChainGap, new Vector2(13, 12), EnemyBehaviourGuid: "463d16121f884984abe759de38418e48"); // chain_gunner
            RoomBuilder.GenerateRoomLayout(Expand_ChainGap, "Expand_TurtleMelon_Chain_Gap_Layout");


            Expand_Challange1.name = "Expand TurtleMelon Challenge 1";
            Expand_Challange1.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Challange1.GUID = Guid.NewGuid().ToString();
            Expand_Challange1.PreventMirroring = false;
            Expand_Challange1.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Challange1.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Challange1.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Challange1, new Vector2(19, 0), DungeonData.Direction.SOUTH, PrototypeRoomExit.ExitType.ENTRANCE_ONLY);
            RoomBuilder.AddExitToRoom(Expand_Challange1, new Vector2(4, 40), DungeonData.Direction.NORTH, PrototypeRoomExit.ExitType.EXIT_ONLY);
            RoomBuilder.AddObjectToRoom(Expand_Challange1, new Vector2(3, 35), EnemyBehaviourGuid: "463d16121f884984abe759de38418e48"); // chain_gunner
            RoomBuilder.AddObjectToRoom(Expand_Challange1, new Vector2(4, 38), EnemyBehaviourGuid: "4db03291a12144d69fe940d5a01de376"); // hollowpoint
            RoomBuilder.AddObjectToRoom(Expand_Challange1, new Vector2(20, 22), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.GenerateRoomLayout(Expand_Challange1, "Expand_TurtleMelon_Challenge_1_Layout");


            Expand_Pit_Line.name = "Expand TurtleMelon Pit Line";
            Expand_Pit_Line.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Pit_Line.GUID = Guid.NewGuid().ToString();
            Expand_Pit_Line.PreventMirroring = false;
            Expand_Pit_Line.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Pit_Line.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Pit_Line.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Pit_Line, new Vector2(0, 15), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Pit_Line, new Vector2(23, 15), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Pit_Line, new Vector2(11, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Pit_Line, new Vector2(11, 31), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Pit_Line, new Vector2(10, 2), EnemyBehaviourGuid: "88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Pit_Line, new Vector2(10, 27), EnemyBehaviourGuid: "88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Pit_Line, new Vector2(5, 14), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomBuilder.AddObjectToRoom(Expand_Pit_Line, new Vector2(16, 7), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomBuilder.AddObjectToRoom(Expand_Pit_Line, new Vector2(16, 22), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomBuilder.GenerateRoomLayout(Expand_Pit_Line, "Expand_TurtleMelon_Pit_Line_Layout");


            Expand_Singer_Gap.name = "Expand TurtleMelon Singer Gap";
            Expand_Singer_Gap.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Singer_Gap.GUID = Guid.NewGuid().ToString();
            Expand_Singer_Gap.PreventMirroring = false;
            Expand_Singer_Gap.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Singer_Gap.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Singer_Gap.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Singer_Gap, new Vector2(31, 14), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Singer_Gap, new Vector2(11, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Singer_Gap, new Vector2(11, 26), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Singer_Gap, new Vector2(3, 2), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomBuilder.AddObjectToRoom(Expand_Singer_Gap, new Vector2(19, 2), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomBuilder.AddObjectToRoom(Expand_Singer_Gap, new Vector2(23, 10), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Singer_Gap, new Vector2(23, 19), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Singer_Gap, new Vector2(23, 15), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Singer_Gap, new Vector2(10, 23), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Singer_Gap, new Vector2(10, 17), EnemyBehaviourGuid: "cf2b7021eac44e3f95af07db9a7c442c"); // LeadWizard
            RoomBuilder.GenerateRoomLayout(Expand_Singer_Gap, "Expand_TurtleMelon_Singer_Gap_Layout");

            Expand_Flying_Gap.name = "Expand TurtleMelon Flying Gap";
            Expand_Flying_Gap.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Flying_Gap.GUID = Guid.NewGuid().ToString();
            Expand_Flying_Gap.PreventMirroring = false;
            Expand_Flying_Gap.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Flying_Gap.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Flying_Gap.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Flying_Gap, new Vector2(0, 6), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Flying_Gap, new Vector2(29, 6), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Flying_Gap, new Vector2(3, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Flying_Gap, new Vector2(25, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Flying_Gap, new Vector2(14, 22), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Flying_Gap, new Vector2(10, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Flying_Gap, new Vector2(12, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Flying_Gap, new Vector2(14, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Flying_Gap, new Vector2(18, 4), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Flying_Gap, new Vector2(18, 4), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Flying_Gap, new Vector2(18, 23), EnemyBehaviourGuid: "88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Flying_Gap, new Vector2(13, 3), EnemyBehaviourGuid: "a400523e535f41ac80a43ff6b06dc0bf"); // green_bookllet
            RoomBuilder.AddObjectToRoom(Expand_Flying_Gap, new Vector2(13, 10), EnemyBehaviourGuid: "6f22935656c54ccfb89fca30ad663a64"); // blue_bookllet
            RoomBuilder.GenerateRoomLayout(Expand_Flying_Gap, "Expand_TurtleMelon_Flying_Gap_Layout");


            Expand_Battle.name = "Expand TurtleMelon Battle Hub";
            Expand_Battle.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Battle.GUID = Guid.NewGuid().ToString();
            Expand_Battle.PreventMirroring = false;
            Expand_Battle.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_Battle.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Battle.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Battle, new Vector2(0, 4), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Battle, new Vector2(31, 4), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Battle, new Vector2(0, 26), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Battle, new Vector2(31, 26), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Battle, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Battle, new Vector2(4, 31), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Battle, new Vector2(26, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Battle, new Vector2(26, 31), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Battle, new Vector2(13, 13), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddObjectToRoom(Expand_Battle, new Vector2(25, 7), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Battle, new Vector2(6, 9), EnemyBehaviourGuid: "70216cae6c1346309d86d4a0b4603045"); // veteran_bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Battle, new Vector2(18, 15), EnemyBehaviourGuid: "cf2b7021eac44e3f95af07db9a7c442c"); // LeadWizard
            RoomBuilder.AddObjectToRoom(Expand_Battle, new Vector2(20, 20), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Battle, new Vector2(4, 26), EnemyBehaviourGuid: "ed37fa13e0fa4fcf8239643957c51293"); // gigi
            RoomBuilder.GenerateBasicRoomLayout(Expand_Battle);


            Expand_Cross.name = "Expand TurtleMelon Cross";
            Expand_Cross.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Cross.GUID = Guid.NewGuid().ToString();
            Expand_Cross.PreventMirroring = false;
            Expand_Cross.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Cross.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Cross.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Cross, new Vector2(0, 15), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Cross, new Vector2(31, 15), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Cross, new Vector2(15, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Cross, new Vector2(15, 31), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Cross, new Vector2(15, 2), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Cross, new Vector2(15, 8), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Cross, new Vector2(15, 15), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Cross, new Vector2(15, 22), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Cross, new Vector2(15, 27), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Cross, new Vector2(5, 15), EnemyBehaviourGuid: "8bb5578fba374e8aae8e10b754e61d62"); // cardinal
            RoomBuilder.AddObjectToRoom(Expand_Cross, new Vector2(25, 15), EnemyBehaviourGuid: "8bb5578fba374e8aae8e10b754e61d62"); // cardinal
            RoomBuilder.GenerateRoomLayout(Expand_Cross, "Expand_TurtleMelon_Cross_Layout");


            Expand_Blocks.name = "Expand TurtleMelon Blocks";
            Expand_Blocks.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Blocks.GUID = Guid.NewGuid().ToString();
            Expand_Blocks.PreventMirroring = false;
            Expand_Blocks.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Blocks.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Blocks.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Blocks, new Vector2(0, 19), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Blocks, new Vector2(32, 13), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Blocks, new Vector2(10, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Blocks, new Vector2(16, 31), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Blocks, new Vector2(23, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Blocks, new Vector2(6, 17), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Blocks, new Vector2(16, 25), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Blocks, new Vector2(10, 2), EnemyBehaviourGuid: "c0ff3744760c4a2eb0bb52ac162056e6"); // bookllet
            RoomBuilder.AddObjectToRoom(Expand_Blocks, new Vector2(24, 10), EnemyBehaviourGuid: "c0ff3744760c4a2eb0bb52ac162056e6"); // bookllet
            RoomBuilder.AddObjectToRoom(Expand_Blocks, new Vector2(8, 18), EnemyBehaviourGuid: "6f22935656c54ccfb89fca30ad663a64"); // blue_bookllet
            RoomBuilder.AddObjectToRoom(Expand_Blocks, new Vector2(16, 27), EnemyBehaviourGuid: "a400523e535f41ac80a43ff6b06dc0bf"); // green_bookllet
            RoomBuilder.GenerateRoomLayout(Expand_Blocks, "Expand_TurtleMelon_Blocks_Layout");



            Expand_Blocks_Pits.name = "Expand TurtleMelon Blocks Pits";
            Expand_Blocks_Pits.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Blocks_Pits.GUID = Guid.NewGuid().ToString();
            Expand_Blocks_Pits.PreventMirroring = false;
            Expand_Blocks_Pits.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Blocks_Pits.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Blocks_Pits.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Blocks_Pits, new Vector2(0, 9), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Blocks_Pits, new Vector2(23, 9), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Blocks_Pits, new Vector2(11, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Blocks_Pits, new Vector2(11, 19), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Blocks_Pits, new Vector2(10, 2), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Blocks_Pits, new Vector2(2, 10), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Blocks_Pits, new Vector2(17, 9), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.GenerateRoomLayout(Expand_Blocks_Pits, "Expand_TurtleMelon_Blocks_Pits_Layout");


            Expand_Wall_Pit.name = "Expand TurtleMelon Wall Pit";
            Expand_Wall_Pit.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Wall_Pit.GUID = Guid.NewGuid().ToString();
            Expand_Wall_Pit.PreventMirroring = false;
            Expand_Wall_Pit.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Wall_Pit.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Wall_Pit.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Wall_Pit, new Vector2(0, 9), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Wall_Pit, new Vector2(23, 9), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Wall_Pit, new Vector2(2, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Wall_Pit, new Vector2(20, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Wall_Pit, new Vector2(2, 19), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Wall_Pit, new Vector2(20, 19), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Wall_Pit, new Vector2(9, 8), EnemyBehaviourGuid: "1a4872dafdb34fd29fe8ac90bd2cea67"); // king_bullat
            RoomBuilder.GenerateRoomLayout(Expand_Wall_Pit, "Expand_TurtleMelon_Wall_Pit_Layout");



            Expand_Gate_Cross.name = "Expand TurtleMelon Gate Cross";
            Expand_Gate_Cross.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Gate_Cross.GUID = Guid.NewGuid().ToString();
            Expand_Gate_Cross.PreventMirroring = false;
            Expand_Gate_Cross.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Gate_Cross.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Gate_Cross.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Gate_Cross, new Vector2(0, 10), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Gate_Cross, new Vector2(15, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Gate_Cross, new Vector2(15, 21), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Gate_Cross, new Vector2(16, 1), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Gate_Cross, new Vector2(5, 2), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Gate_Cross, new Vector2(5, 17), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Gate_Cross, new Vector2(16, 18), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Gate_Cross, new Vector2(16, 7), EnemyBehaviourGuid: "cf2b7021eac44e3f95af07db9a7c442c"); // LeadWizard
            RoomBuilder.AddObjectToRoom(Expand_Gate_Cross, new Vector2(16, 12), EnemyBehaviourGuid: "206405acad4d4c33aac6717d184dc8d4"); // apprentice_gunjurer
            RoomBuilder.GenerateRoomLayout(Expand_Gate_Cross, "Expand_TurtleMelon_Gate_Cross_Layout");

            Expand_Passage.name = "Expand TurtleMelon Passage";
            Expand_Passage.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Passage.GUID = Guid.NewGuid().ToString();
            Expand_Passage.PreventMirroring = false;
            Expand_Passage.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Passage.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Passage.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Passage, new Vector2(12, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Passage, new Vector2(12, 21), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Passage, new Vector2(2, 2), EnemyBehaviourGuid: "4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
            RoomBuilder.AddObjectToRoom(Expand_Passage, new Vector2(20, 2), EnemyBehaviourGuid: "4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
            RoomBuilder.AddObjectToRoom(Expand_Passage, new Vector2(11, 17), EnemyBehaviourGuid: "c0260c286c8d4538a697c5bf24976ccf"); // dynamite_kin
            RoomBuilder.GenerateRoomLayout(Expand_Passage, "Expand_TurtleMelon_Passage_Layout");


            Expand_Pit_Jump.name = "Expand TurtleMelon Pit Jump";
            Expand_Pit_Jump.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Pit_Jump.GUID = Guid.NewGuid().ToString();
            Expand_Pit_Jump.PreventMirroring = false;
            Expand_Pit_Jump.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Pit_Jump.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Pit_Jump.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Pit_Jump, new Vector2(0, 2), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Pit_Jump, new Vector2(31, 22), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(Expand_Pit_Jump, new Vector2(12, 3), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Pit_Jump, new Vector2(15, 13), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // Blobulon
            RoomBuilder.AddObjectToRoom(Expand_Pit_Jump, new Vector2(16, 21), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
            RoomBuilder.GenerateRoomLayout(Expand_Pit_Jump, "Expand_TurtleMelon_Pit_Jump_Layout");


            Expand_Pit_Passage.name = "Expand TurtleMelon Pit Passage";
            Expand_Pit_Passage.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Pit_Passage.GUID = Guid.NewGuid().ToString();
            Expand_Pit_Passage.PreventMirroring = false;
            Expand_Pit_Passage.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Pit_Passage.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Pit_Passage.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Pit_Passage, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Pit_Passage, new Vector2(14, 21), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Pit_Passage, new Vector2(13, 4), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomBuilder.AddObjectToRoom(Expand_Pit_Passage, new Vector2(5, 10), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomBuilder.AddObjectToRoom(Expand_Pit_Passage, new Vector2(14, 15), EnemyBehaviourGuid: "6b7ef9e5d05b4f96b04f05ef4a0d1b18"); // rubber_kin
            RoomBuilder.GenerateRoomLayout(Expand_Pit_Passage, "Expand_TurtleMelon_Pit_Passage_Layout");


            Expand_R_Blocks.name = "Expand TurtleMelon R Blocks";
            Expand_R_Blocks.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_R_Blocks.GUID = Guid.NewGuid().ToString();
            Expand_R_Blocks.PreventMirroring = false;
            Expand_R_Blocks.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_R_Blocks.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_R_Blocks.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_R_Blocks, new Vector2(42, 27), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_R_Blocks, new Vector2(10, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_R_Blocks, new Vector2(13, 35), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_R_Blocks, new Vector2(9, 3), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_R_Blocks, new Vector2(34, 8), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_R_Blocks, new Vector2(9, 17), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_R_Blocks, new Vector2(9, 28), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.GenerateRoomLayout(Expand_R_Blocks, "Expand_TurtleMelon_R_Blocks_Layout");


            Expand_Small_Passage.name = "Expand TurtleMelon Small Passage 1";
            Expand_Small_Passage.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Small_Passage.GUID = Guid.NewGuid().ToString();
            Expand_Small_Passage.PreventMirroring = false;
            Expand_Small_Passage.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_Small_Passage.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Small_Passage.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Small_Passage, new Vector2(0, 1), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Small_Passage, new Vector2(20, 17), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(10, 0), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(11, 0), ExpandObjectDatabase.BushFlowers);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(11, 1), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(10, 6), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(10, 7), ExpandObjectDatabase.BushFlowers);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(9, 7), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(0, 16), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(0, 17), ExpandObjectDatabase.BushFlowers);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(1, 17), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(2, 8), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(1, 8), ExpandObjectDatabase.BushFlowers);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(1, 9), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(2, 1), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(3, 1), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(4, 1), ExpandObjectDatabase.BushFlowers);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(5, 1), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(8, 17), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(9, 17), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(10, 17), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(11, 17), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(11, 3), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(11, 4), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(0, 11), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(0, 12), ExpandObjectDatabase.BushFlowers);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(0, 13), ExpandObjectDatabase.Bush);
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(1, 13), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Small_Passage, new Vector2(5, 8), EnemyBehaviourGuid: "4db03291a12144d69fe940d5a01de376"); // hollowpoint
            RoomBuilder.GenerateRoomLayout(Expand_Small_Passage, "Expand_TurtleMelon_Small_Passage_1_Layout");


            Expand_Box.name = "Expand TurtleMelon Box";
            Expand_Box.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Box.GUID = Guid.NewGuid().ToString();
            Expand_Box.PreventMirroring = false;
            Expand_Box.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Box.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Box.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Box, new Vector2(0, 13), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Box, new Vector2(21, 4), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Box, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Box, new Vector2(14, 23), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Box, new Vector2(5, 4), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Box, new Vector2(12, 13), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Box, new Vector2(14, 4), EnemyBehaviourGuid: "4db03291a12144d69fe940d5a01de376"); // hollowpoint
            RoomBuilder.GenerateRoomLayout(Expand_Box, "Expand_TurtleMelon_Box_Layout");


            Expand_Steps.name = "Expand TurtleMelon Steps";
            Expand_Steps.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Steps.GUID = Guid.NewGuid().ToString();
            Expand_Steps.PreventMirroring = false;
            Expand_Steps.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Steps.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Steps.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Steps, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Steps, new Vector2(16, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Steps, new Vector2(4, 21), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Steps, new Vector2(16, 21), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Steps, new Vector2(1, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Steps, new Vector2(1, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Steps, new Vector2(18, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Steps, new Vector2(18, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Steps, new Vector2(5, 2), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Steps, new Vector2(13, 5), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Steps, new Vector2(14, 11), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Steps, new Vector2(4, 7), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // blobulon
            RoomBuilder.AddObjectToRoom(Expand_Steps, new Vector2(4, 15), EnemyBehaviourGuid: "206405acad4d4c33aac6717d184dc8d4"); // apprentice_gunjurer
            RoomBuilder.GenerateRoomLayout(Expand_Steps, "Expand_TurtleMelon_Steps_Layout");


            Expand_Apache_Hub.name = "Expand MelonTurtle Apache Hub";
            Expand_Apache_Hub.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Apache_Hub.GUID = Guid.NewGuid().ToString();
            Expand_Apache_Hub.PreventMirroring = false;
            Expand_Apache_Hub.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_Apache_Hub.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Apache_Hub.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Apache_Hub, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Apache_Hub, new Vector2(4, 51), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Apache_Hub, new Vector2(36, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Apache_Hub, new Vector2(36, 51), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Apache_Hub, new Vector2(18, 17), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_Hub, new Vector2(34, 25), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomBuilder.AddObjectToRoom(Expand_Apache_Hub, new Vector2(3, 20), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomBuilder.AddObjectToRoom(Expand_Apache_Hub, new Vector2(35, 15), EnemyBehaviourGuid: "463d16121f884984abe759de38418e48"); // chain_gunner
            RoomBuilder.GenerateRoomLayout(Expand_Apache_Hub, "Expand_MelonTurtle_Apache_Hub_Layout");


            Expand_Box_Hub.name = "Expand MelonTurtle Box Hub";
            Expand_Box_Hub.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Box_Hub.GUID = Guid.NewGuid().ToString();
            Expand_Box_Hub.PreventMirroring = false;
            Expand_Box_Hub.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_Box_Hub.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Box_Hub.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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

            RoomBuilder.AddExitToRoom(Expand_Box_Hub, new Vector2(0, 20), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Box_Hub, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Box_Hub, new Vector2(4, 41), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Box_Hub, new Vector2(41, 20), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Box_Hub, new Vector2(35, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Box_Hub, new Vector2(35, 41), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Box_Hub, new Vector2(17, 11), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddObjectToRoom(Expand_Box_Hub, new Vector2(27, 9), EnemyBehaviourGuid: "6f22935656c54ccfb89fca30ad663a64"); // blue_bookllet
            RoomBuilder.AddObjectToRoom(Expand_Box_Hub, new Vector2(3, 20), EnemyBehaviourGuid: "c0ff3744760c4a2eb0bb52ac162056e6"); // bookllet
            RoomBuilder.AddObjectToRoom(Expand_Box_Hub, new Vector2(11, 15), EnemyBehaviourGuid: "c0ff3744760c4a2eb0bb52ac162056e6"); // bookllet
            RoomBuilder.AddObjectToRoom(Expand_Box_Hub, new Vector2(34, 28), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Box_Hub, new Vector2(10, 36), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // red_shotgun_kin
            RoomBuilder.GenerateRoomLayout(Expand_Box_Hub, "Expand_MelonTurtle_Box_Hub_Layout");



            Expand_Enclose_Hub.name = "Expand MelonTurtle Enclose Hub";
            Expand_Enclose_Hub.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Enclose_Hub.GUID = Guid.NewGuid().ToString();
            Expand_Enclose_Hub.PreventMirroring = false;
            Expand_Enclose_Hub.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_Enclose_Hub.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Enclose_Hub.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_Enclose_Hub, new Vector2(5, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Enclose_Hub, new Vector2(5, 40), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Enclose_Hub, new Vector2(20, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Enclose_Hub, new Vector2(20, 40), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Enclose_Hub, new Vector2(35, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Enclose_Hub, new Vector2(35, 40), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(10, 7), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(27, 13), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(10, 31), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(30, 32), ExpandObjectDatabase.ExplodyBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(28, 9), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(23, 23), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(15, 27), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(28, 28), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(14, 12), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(27, 19), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(11, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(18, 18), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(19, 6), EnemyBehaviourGuid: "463d16121f884984abe759de38418e48"); // chain_gunner
            RoomBuilder.AddObjectToRoom(Expand_Enclose_Hub, new Vector2(19, 32), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomBuilder.GenerateRoomLayout(Expand_Enclose_Hub, "Expand_MelonTurtle_Enclose_Hub_Layout");


            Expand_Crosshairs.name = "Expand Neighborino Crosshairs";
            Expand_Crosshairs.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Crosshairs.GUID = Guid.NewGuid().ToString();
            Expand_Crosshairs.PreventMirroring = false;
            Expand_Crosshairs.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Crosshairs.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Crosshairs.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Crosshairs.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Crosshairs.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Crosshairs.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Crosshairs.pits = new List<PrototypeRoomPitEntry>();
            Expand_Crosshairs.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Crosshairs.placedObjectPositions = new List<Vector2>();
            Expand_Crosshairs.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Crosshairs.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Crosshairs.overriddenTilesets = 0;
            Expand_Crosshairs.prerequisites = new List<DungeonPrerequisite>();
            Expand_Crosshairs.InvalidInCoop = false;
            Expand_Crosshairs.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Crosshairs.preventAddedDecoLayering = false;
            Expand_Crosshairs.precludeAllTilemapDrawing = false;
            Expand_Crosshairs.drawPrecludedCeilingTiles = false;
            Expand_Crosshairs.preventBorders = false;
            Expand_Crosshairs.preventFacewallAO = false;
            Expand_Crosshairs.usesCustomAmbientLight = false;
            Expand_Crosshairs.customAmbientLight = Color.white;
            Expand_Crosshairs.ForceAllowDuplicates = false;
            Expand_Crosshairs.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Crosshairs.IsLostWoodsRoom = false;
            Expand_Crosshairs.UseCustomMusic = false;
            Expand_Crosshairs.UseCustomMusicState = false;
            Expand_Crosshairs.CustomMusicEvent = string.Empty;
            Expand_Crosshairs.UseCustomMusicSwitch = false;
            Expand_Crosshairs.CustomMusicSwitch = string.Empty;
            Expand_Crosshairs.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Crosshairs.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Crosshairs.Width = 34;
            Expand_Crosshairs.Height = 34;
            Expand_Crosshairs.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_Crosshairs, new Vector2(0, 16), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Crosshairs, new Vector2(35, 16), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Crosshairs, new Vector2(16, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Crosshairs, new Vector2(16, 35), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Crosshairs, new Vector2(7, 6), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // 
            RoomBuilder.AddObjectToRoom(Expand_Crosshairs, new Vector2(7, 22), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // 
            RoomBuilder.AddObjectToRoom(Expand_Crosshairs, new Vector2(23, 7), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // 
            RoomBuilder.AddObjectToRoom(Expand_Crosshairs, new Vector2(23, 22), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // 
            RoomBuilder.AddObjectToRoom(Expand_Crosshairs, new Vector2(15, 4), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // 
            RoomBuilder.AddObjectToRoom(Expand_Crosshairs, new Vector2(15, 24), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // 
            RoomBuilder.AddObjectToRoom(Expand_Crosshairs, new Vector2(4, 14), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // 
            RoomBuilder.AddObjectToRoom(Expand_Crosshairs, new Vector2(24, 14), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // 
            RoomBuilder.GenerateRoomLayout(Expand_Crosshairs, "Expand_Neighborino_Crosshair_Layout");

            Expand_Basic.name = "Expand Neighborino Basic";
            Expand_Basic.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Basic.GUID = Guid.NewGuid().ToString();
            Expand_Basic.PreventMirroring = false;
            Expand_Basic.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Basic.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Basic.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Basic.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Basic.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Basic.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Basic.pits = new List<PrototypeRoomPitEntry>();
            Expand_Basic.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Basic.placedObjectPositions = new List<Vector2>();
            Expand_Basic.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Basic.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Basic.overriddenTilesets = 0;
            Expand_Basic.prerequisites = new List<DungeonPrerequisite>();
            Expand_Basic.InvalidInCoop = false;
            Expand_Basic.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Basic.preventAddedDecoLayering = false;
            Expand_Basic.precludeAllTilemapDrawing = false;
            Expand_Basic.drawPrecludedCeilingTiles = false;
            Expand_Basic.preventBorders = false;
            Expand_Basic.preventFacewallAO = false;
            Expand_Basic.usesCustomAmbientLight = false;
            Expand_Basic.customAmbientLight = Color.white;
            Expand_Basic.ForceAllowDuplicates = false;
            Expand_Basic.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Basic.IsLostWoodsRoom = false;
            Expand_Basic.UseCustomMusic = false;
            Expand_Basic.UseCustomMusicState = false;
            Expand_Basic.CustomMusicEvent = string.Empty;
            Expand_Basic.UseCustomMusicSwitch = false;
            Expand_Basic.CustomMusicSwitch = string.Empty;
            Expand_Basic.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Basic.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Basic.Width = 28;
            Expand_Basic.Height = 26;
            Expand_Basic.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_Basic, new Vector2(0, 13), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Basic, new Vector2(29, 13), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Basic, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Basic, new Vector2(14, 27), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(12, 7), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(13, 7), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(14, 7), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(15, 7), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(12, 8), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(13, 8), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(14, 8), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(15, 8), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(12, 17), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(13, 17), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(14, 17), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(15, 17), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(12, 18), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(13, 18), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(14, 18), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(15, 18), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(21, 4), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(7, 21), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(7, 4), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // red_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(24, 21), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // blue_shotgun_kin
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(8, 12), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomBuilder.AddObjectToRoom(Expand_Basic, new Vector2(18, 12), EnemyBehaviourGuid: "ec8ea75b557d4e7b8ceeaacdf6f8238c"); // gun_nut
            RoomBuilder.GenerateRoomLayout(Expand_Basic, "Expand_Neighborino_Basic_Layout");

            Expand_JumpInThePit.name = "Expand Neighborino JumpInThePit";
            Expand_JumpInThePit.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_JumpInThePit.GUID = Guid.NewGuid().ToString();
            Expand_JumpInThePit.PreventMirroring = false;
            Expand_JumpInThePit.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_JumpInThePit.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_JumpInThePit.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_JumpInThePit.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_JumpInThePit.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_JumpInThePit.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_JumpInThePit.pits = new List<PrototypeRoomPitEntry>();
            Expand_JumpInThePit.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_JumpInThePit.placedObjectPositions = new List<Vector2>();
            Expand_JumpInThePit.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_JumpInThePit.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_JumpInThePit.overriddenTilesets = 0;
            Expand_JumpInThePit.prerequisites = new List<DungeonPrerequisite>();
            Expand_JumpInThePit.InvalidInCoop = false;
            Expand_JumpInThePit.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_JumpInThePit.preventAddedDecoLayering = false;
            Expand_JumpInThePit.precludeAllTilemapDrawing = false;
            Expand_JumpInThePit.drawPrecludedCeilingTiles = false;
            Expand_JumpInThePit.preventBorders = false;
            Expand_JumpInThePit.preventFacewallAO = false;
            Expand_JumpInThePit.usesCustomAmbientLight = false;
            Expand_JumpInThePit.customAmbientLight = Color.white;
            Expand_JumpInThePit.ForceAllowDuplicates = false;
            Expand_JumpInThePit.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_JumpInThePit.IsLostWoodsRoom = false;
            Expand_JumpInThePit.UseCustomMusic = false;
            Expand_JumpInThePit.UseCustomMusicState = false;
            Expand_JumpInThePit.CustomMusicEvent = string.Empty;
            Expand_JumpInThePit.UseCustomMusicSwitch = false;
            Expand_JumpInThePit.CustomMusicSwitch = string.Empty;
            Expand_JumpInThePit.overrideRoomVisualTypeForSecretRooms = false;
            Expand_JumpInThePit.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_JumpInThePit.Width = 26;
            Expand_JumpInThePit.Height = 35;
            Expand_JumpInThePit.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                            contentsBasePosition = new Vector2(18, 14),
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
                            contentsBasePosition = new Vector2(7, 14),
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
                            contentsBasePosition = new Vector2(18, 20),
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
                            contentsBasePosition = new Vector2(7, 20),
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
                        new Vector2(18, 14),
                        new Vector2(7, 14),
                        new Vector2(18, 20),
                        new Vector2(7, 20)
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
            RoomBuilder.AddExitToRoom(Expand_JumpInThePit, new Vector2(13, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_JumpInThePit, new Vector2(13, 36), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_JumpInThePit, new Vector2(5, 4), EnemyBehaviourGuid: "1a4872dafdb34fd29fe8ac90bd2cea67"); // king_bullat
            RoomBuilder.AddObjectToRoom(Expand_JumpInThePit, new Vector2(19, 28), EnemyBehaviourGuid: "1a4872dafdb34fd29fe8ac90bd2cea67"); // king_bullat
            RoomBuilder.GenerateRoomLayout(Expand_JumpInThePit, "Expand_Neighborino_JumpInThePit_Layout");

            Expand_LongSpikeTrap.name = "Expand Neighborino Long SpikeTrap";
            Expand_LongSpikeTrap.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_LongSpikeTrap.GUID = Guid.NewGuid().ToString();
            Expand_LongSpikeTrap.PreventMirroring = false;
            Expand_LongSpikeTrap.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_LongSpikeTrap.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_LongSpikeTrap.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_LongSpikeTrap.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_LongSpikeTrap.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_LongSpikeTrap.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_LongSpikeTrap.pits = new List<PrototypeRoomPitEntry>();
            Expand_LongSpikeTrap.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_LongSpikeTrap.placedObjectPositions = new List<Vector2>();
            Expand_LongSpikeTrap.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_LongSpikeTrap.roomEvents = new List<RoomEventDefinition>(0);
            Expand_LongSpikeTrap.overriddenTilesets = 0;
            Expand_LongSpikeTrap.prerequisites = new List<DungeonPrerequisite>();
            Expand_LongSpikeTrap.InvalidInCoop = false;
            Expand_LongSpikeTrap.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_LongSpikeTrap.preventAddedDecoLayering = false;
            Expand_LongSpikeTrap.precludeAllTilemapDrawing = false;
            Expand_LongSpikeTrap.drawPrecludedCeilingTiles = false;
            Expand_LongSpikeTrap.preventBorders = false;
            Expand_LongSpikeTrap.preventFacewallAO = false;
            Expand_LongSpikeTrap.usesCustomAmbientLight = false;
            Expand_LongSpikeTrap.customAmbientLight = Color.white;
            Expand_LongSpikeTrap.ForceAllowDuplicates = false;
            Expand_LongSpikeTrap.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_LongSpikeTrap.IsLostWoodsRoom = false;
            Expand_LongSpikeTrap.UseCustomMusic = false;
            Expand_LongSpikeTrap.UseCustomMusicState = false;
            Expand_LongSpikeTrap.CustomMusicEvent = string.Empty;
            Expand_LongSpikeTrap.UseCustomMusicSwitch = false;
            Expand_LongSpikeTrap.CustomMusicSwitch = string.Empty;
            Expand_LongSpikeTrap.overrideRoomVisualTypeForSecretRooms = false;
            Expand_LongSpikeTrap.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_LongSpikeTrap.Width = 15;
            Expand_LongSpikeTrap.Height = 37;
            Expand_LongSpikeTrap.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_LongSpikeTrap, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_LongSpikeTrap, new Vector2(1, 35), DungeonData.Direction.WEST);
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(5, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(7, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(9, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 8), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(9, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(7, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(5, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 12), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 16), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(5, 16), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(7, 16), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(9, 16), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 16), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 18), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(9, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(7, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(5, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 26), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 28), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(5, 28), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(7, 28), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(9, 28), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 28), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 30), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 32), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(11, 34), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(9, 34), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(7, 34), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(5, 34), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_LongSpikeTrap, new Vector2(3, 34), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.GenerateRoomLayout(Expand_LongSpikeTrap, "Expand_Neighborino_LongSpikeTrap_Layout");

            Expand_SpikeTrap.name = "Expand Neighborino SpikeTrap";
            Expand_SpikeTrap.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_SpikeTrap.GUID = Guid.NewGuid().ToString();
            Expand_SpikeTrap.PreventMirroring = false;
            Expand_SpikeTrap.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_SpikeTrap.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_SpikeTrap.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_SpikeTrap.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_SpikeTrap.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_SpikeTrap.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_SpikeTrap.pits = new List<PrototypeRoomPitEntry>();
            Expand_SpikeTrap.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_SpikeTrap.placedObjectPositions = new List<Vector2>();
            Expand_SpikeTrap.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_SpikeTrap.roomEvents = new List<RoomEventDefinition>(0);
            Expand_SpikeTrap.overriddenTilesets = 0;
            Expand_SpikeTrap.prerequisites = new List<DungeonPrerequisite>();
            Expand_SpikeTrap.InvalidInCoop = false;
            Expand_SpikeTrap.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_SpikeTrap.preventAddedDecoLayering = false;
            Expand_SpikeTrap.precludeAllTilemapDrawing = false;
            Expand_SpikeTrap.drawPrecludedCeilingTiles = false;
            Expand_SpikeTrap.preventBorders = false;
            Expand_SpikeTrap.preventFacewallAO = false;
            Expand_SpikeTrap.usesCustomAmbientLight = false;
            Expand_SpikeTrap.customAmbientLight = Color.white;
            Expand_SpikeTrap.ForceAllowDuplicates = false;
            Expand_SpikeTrap.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_SpikeTrap.IsLostWoodsRoom = false;
            Expand_SpikeTrap.UseCustomMusic = false;
            Expand_SpikeTrap.UseCustomMusicState = false;
            Expand_SpikeTrap.CustomMusicEvent = string.Empty;
            Expand_SpikeTrap.UseCustomMusicSwitch = false;
            Expand_SpikeTrap.CustomMusicSwitch = string.Empty;
            Expand_SpikeTrap.overrideRoomVisualTypeForSecretRooms = false;
            Expand_SpikeTrap.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_SpikeTrap.Width = 26;
            Expand_SpikeTrap.Height = 22;
            Expand_SpikeTrap.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_SpikeTrap, new Vector2(0, 11), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_SpikeTrap, new Vector2(27, 11), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_SpikeTrap, new Vector2(13, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_SpikeTrap, new Vector2(13, 23), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(2, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(4, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(6, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(8, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(12, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(16, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(18, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(20, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(22, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(12, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(12, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(12, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(12, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(12, 16), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpikeTrap, new Vector2(12, 18), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.GenerateRoomLayout(Expand_SpikeTrap, "Expand_Neighborino_SpikeTrap_Layout");

            Expand_ThinRoom.name = "Expand Neighborino Thin Room";
            Expand_ThinRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_ThinRoom.GUID = Guid.NewGuid().ToString();
            Expand_ThinRoom.PreventMirroring = false;
            Expand_ThinRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_ThinRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_ThinRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_ThinRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_ThinRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_ThinRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_ThinRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_ThinRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_ThinRoom.placedObjectPositions = new List<Vector2>();
            Expand_ThinRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_ThinRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_ThinRoom.overriddenTilesets = 0;
            Expand_ThinRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_ThinRoom.InvalidInCoop = false;
            Expand_ThinRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_ThinRoom.preventAddedDecoLayering = false;
            Expand_ThinRoom.precludeAllTilemapDrawing = false;
            Expand_ThinRoom.drawPrecludedCeilingTiles = false;
            Expand_ThinRoom.preventBorders = false;
            Expand_ThinRoom.preventFacewallAO = false;
            Expand_ThinRoom.usesCustomAmbientLight = false;
            Expand_ThinRoom.customAmbientLight = Color.white;
            Expand_ThinRoom.ForceAllowDuplicates = false;
            Expand_ThinRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_ThinRoom.IsLostWoodsRoom = false;
            Expand_ThinRoom.UseCustomMusic = false;
            Expand_ThinRoom.UseCustomMusicState = false;
            Expand_ThinRoom.CustomMusicEvent = string.Empty;
            Expand_ThinRoom.UseCustomMusicSwitch = false;
            Expand_ThinRoom.CustomMusicSwitch = string.Empty;
            Expand_ThinRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_ThinRoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_ThinRoom.Width = 24;
            Expand_ThinRoom.Height = 4;
            Expand_ThinRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(15, 1),
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
                            contentsBasePosition = new Vector2(4, 1),
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
                        new Vector2(15, 1),
                        new Vector2(4, 1)
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
            RoomBuilder.AddExitToRoom(Expand_ThinRoom, new Vector2(0, 2), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_ThinRoom, new Vector2(25, 2), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(Expand_ThinRoom, new Vector2(6, 1), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulin
            RoomBuilder.AddObjectToRoom(Expand_ThinRoom, new Vector2(8, 1), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulin
            RoomBuilder.AddObjectToRoom(Expand_ThinRoom, new Vector2(8, 3), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulin
            RoomBuilder.AddObjectToRoom(Expand_ThinRoom, new Vector2(10, 2), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulin
            RoomBuilder.AddObjectToRoom(Expand_ThinRoom, new Vector2(10, 1), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulin
            RoomBuilder.AddObjectToRoom(Expand_ThinRoom, new Vector2(12, 1), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulin
            RoomBuilder.AddObjectToRoom(Expand_ThinRoom, new Vector2(14, 2), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulin
            RoomBuilder.GenerateBasicRoomLayout(Expand_ThinRoom);

            Expand_SniperRoom.name = "Expand Neighborino Sniper Room";
            Expand_SniperRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_SniperRoom.GUID = Guid.NewGuid().ToString();
            Expand_SniperRoom.PreventMirroring = false;
            Expand_SniperRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_SniperRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_SniperRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_SniperRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_SniperRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_SniperRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_SniperRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_SniperRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_SniperRoom.placedObjectPositions = new List<Vector2>();
            Expand_SniperRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_SniperRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_SniperRoom.overriddenTilesets = 0;
            Expand_SniperRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_SniperRoom.InvalidInCoop = false;
            Expand_SniperRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_SniperRoom.preventAddedDecoLayering = false;
            Expand_SniperRoom.precludeAllTilemapDrawing = false;
            Expand_SniperRoom.drawPrecludedCeilingTiles = false;
            Expand_SniperRoom.preventBorders = false;
            Expand_SniperRoom.preventFacewallAO = false;
            Expand_SniperRoom.usesCustomAmbientLight = false;
            Expand_SniperRoom.customAmbientLight = Color.white;
            Expand_SniperRoom.ForceAllowDuplicates = false;
            Expand_SniperRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_SniperRoom.IsLostWoodsRoom = false;
            Expand_SniperRoom.UseCustomMusic = false;
            Expand_SniperRoom.UseCustomMusicState = false;
            Expand_SniperRoom.CustomMusicEvent = string.Empty;
            Expand_SniperRoom.UseCustomMusicSwitch = false;
            Expand_SniperRoom.CustomMusicSwitch = string.Empty;
            Expand_SniperRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_SniperRoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_SniperRoom.Width = 32;
            Expand_SniperRoom.Height = 27;
            Expand_SniperRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "31a3ea0c54a745e182e22ea54844a82d", //
                            contentsBasePosition = new Vector2(29, 14),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "31a3ea0c54a745e182e22ea54844a82d", //
                            contentsBasePosition = new Vector2(2, 14),
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
                        new Vector2(29, 14),
                        new Vector2(2, 14)
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
            RoomBuilder.AddExitToRoom(Expand_SniperRoom, new Vector2(16, 28), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_SniperRoom, new Vector2(16, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_SniperRoom, new Vector2(6, 1), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulin
            RoomBuilder.AddObjectToRoom(Expand_SniperRoom, new Vector2(8, 1), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulin
            RoomBuilder.GenerateRoomLayout(Expand_SniperRoom, "Expand_Neighborino_SniperRoom_Layout");

            Expand_TableRoom.name = "Expand Neighborino Table Room";
            Expand_TableRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_TableRoom.GUID = Guid.NewGuid().ToString();
            Expand_TableRoom.PreventMirroring = false;
            Expand_TableRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_TableRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_TableRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_TableRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_TableRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_TableRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_TableRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_TableRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_TableRoom.placedObjectPositions = new List<Vector2>();
            Expand_TableRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_TableRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_TableRoom.overriddenTilesets = 0;
            Expand_TableRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_TableRoom.InvalidInCoop = false;
            Expand_TableRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_TableRoom.preventAddedDecoLayering = false;
            Expand_TableRoom.precludeAllTilemapDrawing = false;
            Expand_TableRoom.drawPrecludedCeilingTiles = false;
            Expand_TableRoom.preventBorders = false;
            Expand_TableRoom.preventFacewallAO = false;
            Expand_TableRoom.usesCustomAmbientLight = false;
            Expand_TableRoom.customAmbientLight = Color.white;
            Expand_TableRoom.ForceAllowDuplicates = false;
            Expand_TableRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_TableRoom.IsLostWoodsRoom = false;
            Expand_TableRoom.UseCustomMusic = false;
            Expand_TableRoom.UseCustomMusicState = false;
            Expand_TableRoom.CustomMusicEvent = string.Empty;
            Expand_TableRoom.UseCustomMusicSwitch = false;
            Expand_TableRoom.CustomMusicSwitch = string.Empty;
            Expand_TableRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_TableRoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_TableRoom.Width = 16;
            Expand_TableRoom.Height = 14;
            Expand_TableRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ed37fa13e0fa4fcf8239643957c51293", // gigi
                            contentsBasePosition = new Vector2(6, 5),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ed37fa13e0fa4fcf8239643957c51293", // gigi
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
                            enemyBehaviourGuid = "ed37fa13e0fa4fcf8239643957c51293", // gigi
                            contentsBasePosition = new Vector2(6, 8),
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
                        new Vector2(6, 5),
                        new Vector2(9, 6),
                        new Vector2(6, 8)
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
            RoomBuilder.AddExitToRoom(Expand_TableRoom, new Vector2(0, 7), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_TableRoom, new Vector2(17, 7), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_TableRoom, new Vector2(8, 15), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_TableRoom, new Vector2(8, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(13, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(13, 9), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(13, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(2, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(2, 9), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(2, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(4, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(7, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(10, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(4, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(7, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(10, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom, new Vector2(7, 6), EnemyBehaviourGuid: "98ca70157c364750a60f5e0084f9d3e2"); // phase_spider
            RoomBuilder.GenerateBasicRoomLayout(Expand_TableRoom);


            Expand_GoopTroop.name = "Expand Neighborino GoopTroop";
            Expand_GoopTroop.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_GoopTroop.GUID = Guid.NewGuid().ToString();
            Expand_GoopTroop.PreventMirroring = false;
            Expand_GoopTroop.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_GoopTroop.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_GoopTroop.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_GoopTroop.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_GoopTroop.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_GoopTroop.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_GoopTroop.pits = new List<PrototypeRoomPitEntry>();
            Expand_GoopTroop.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_GoopTroop.placedObjectPositions = new List<Vector2>();
            Expand_GoopTroop.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_GoopTroop.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_GoopTroop.overriddenTilesets = 0;
            Expand_GoopTroop.prerequisites = new List<DungeonPrerequisite>();
            Expand_GoopTroop.InvalidInCoop = false;
            Expand_GoopTroop.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_GoopTroop.preventAddedDecoLayering = false;
            Expand_GoopTroop.precludeAllTilemapDrawing = false;
            Expand_GoopTroop.drawPrecludedCeilingTiles = false;
            Expand_GoopTroop.preventBorders = false;
            Expand_GoopTroop.preventFacewallAO = false;
            Expand_GoopTroop.usesCustomAmbientLight = false;
            Expand_GoopTroop.customAmbientLight = Color.white;
            Expand_GoopTroop.ForceAllowDuplicates = false;
            Expand_GoopTroop.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_GoopTroop.IsLostWoodsRoom = false;
            Expand_GoopTroop.UseCustomMusic = false;
            Expand_GoopTroop.UseCustomMusicState = false;
            Expand_GoopTroop.CustomMusicEvent = string.Empty;
            Expand_GoopTroop.UseCustomMusicSwitch = false;
            Expand_GoopTroop.CustomMusicSwitch = string.Empty;
            Expand_GoopTroop.overrideRoomVisualTypeForSecretRooms = false;
            Expand_GoopTroop.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_GoopTroop.Width = 26;
            Expand_GoopTroop.Height = 26;
            Expand_GoopTroop.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ffdc8680bdaa487f8f31995539f74265", // muzzle_wisp
                            contentsBasePosition = new Vector2(15, 15),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ffdc8680bdaa487f8f31995539f74265", // muzzle_wisp
                            contentsBasePosition = new Vector2(10, 15),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "7ec3e8146f634c559a7d58b19191cd43", // Spirat
                            contentsBasePosition = new Vector2(13, 10),
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
                        new Vector2(15, 15),
                        new Vector2(10, 15),
                        new Vector2(13, 10)
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
            RoomBuilder.AddExitToRoom(Expand_GoopTroop, new Vector2(0, 13), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_GoopTroop, new Vector2(27, 13), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_GoopTroop, new Vector2(13, 27), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_GoopTroop, new Vector2(13, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_GoopTroop, new Vector2(15, 13), EnemyBehaviourGuid: "e61cab252cfb435db9172adc96ded75f"); // poisbulon
            RoomBuilder.AddObjectToRoom(Expand_GoopTroop, new Vector2(11, 13), EnemyBehaviourGuid: "e61cab252cfb435db9172adc96ded75f"); // poisbulon
            RoomBuilder.GenerateRoomLayout(Expand_GoopTroop, "Neighborino_GoopTroop_Layout", DamageCellsType: CoreDamageTypes.Poison);

            Expand_HopScotch.name = "Expand Neighborino HopScotch";
            Expand_HopScotch.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_HopScotch.GUID = Guid.NewGuid().ToString();
            Expand_HopScotch.PreventMirroring = false;
            Expand_HopScotch.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_HopScotch.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_HopScotch.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_HopScotch.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_HopScotch.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_HopScotch.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_HopScotch.pits = new List<PrototypeRoomPitEntry>();
            Expand_HopScotch.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_HopScotch.placedObjectPositions = new List<Vector2>();
            Expand_HopScotch.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_HopScotch.roomEvents = new List<RoomEventDefinition>(0);
            Expand_HopScotch.overriddenTilesets = 0;
            Expand_HopScotch.prerequisites = new List<DungeonPrerequisite>();
            Expand_HopScotch.InvalidInCoop = false;
            Expand_HopScotch.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_HopScotch.preventAddedDecoLayering = false;
            Expand_HopScotch.precludeAllTilemapDrawing = false;
            Expand_HopScotch.drawPrecludedCeilingTiles = false;
            Expand_HopScotch.preventBorders = false;
            Expand_HopScotch.preventFacewallAO = false;
            Expand_HopScotch.usesCustomAmbientLight = false;
            Expand_HopScotch.customAmbientLight = Color.white;
            Expand_HopScotch.ForceAllowDuplicates = false;
            Expand_HopScotch.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_HopScotch.IsLostWoodsRoom = false;
            Expand_HopScotch.UseCustomMusic = false;
            Expand_HopScotch.UseCustomMusicState = false;
            Expand_HopScotch.CustomMusicEvent = string.Empty;
            Expand_HopScotch.UseCustomMusicSwitch = false;
            Expand_HopScotch.CustomMusicSwitch = string.Empty;
            Expand_HopScotch.overrideRoomVisualTypeForSecretRooms = false;
            Expand_HopScotch.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_HopScotch.Width = 30;
            Expand_HopScotch.Height = 10;
            Expand_HopScotch.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_HopScotch, new Vector2(0, 5), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_HopScotch, new Vector2(31, 5), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(Expand_HopScotch, new Vector2(5, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_HopScotch, new Vector2(8, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_HopScotch, new Vector2(11, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_HopScotch, new Vector2(14, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_HopScotch, new Vector2(16, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_HopScotch, new Vector2(19, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_HopScotch, new Vector2(22, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_HopScotch, new Vector2(24, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.GenerateRoomLayout(Expand_HopScotch, "Neighborino_Hopscoth_Layout");


            Expand_Pit.name = "Expand Neighborino Pit";
            Expand_Pit.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Pit.GUID = Guid.NewGuid().ToString();
            Expand_Pit.PreventMirroring = false;
            Expand_Pit.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Pit.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Pit.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Pit.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Pit.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Pit.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Pit.pits = new List<PrototypeRoomPitEntry>();
            Expand_Pit.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Pit.placedObjectPositions = new List<Vector2>();
            Expand_Pit.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Pit.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Pit.overriddenTilesets = 0;
            Expand_Pit.prerequisites = new List<DungeonPrerequisite>();
            Expand_Pit.InvalidInCoop = false;
            Expand_Pit.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Pit.preventAddedDecoLayering = false;
            Expand_Pit.precludeAllTilemapDrawing = false;
            Expand_Pit.drawPrecludedCeilingTiles = false;
            Expand_Pit.preventBorders = false;
            Expand_Pit.preventFacewallAO = false;
            Expand_Pit.usesCustomAmbientLight = false;
            Expand_Pit.customAmbientLight = Color.white;
            Expand_Pit.ForceAllowDuplicates = false;
            Expand_Pit.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Pit.IsLostWoodsRoom = false;
            Expand_Pit.UseCustomMusic = false;
            Expand_Pit.UseCustomMusicState = false;
            Expand_Pit.CustomMusicEvent = string.Empty;
            Expand_Pit.UseCustomMusicSwitch = false;
            Expand_Pit.CustomMusicSwitch = string.Empty;
            Expand_Pit.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Pit.overrideRoomVisualType = 5;
            Expand_Pit.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Pit.Width = 28;
            Expand_Pit.Height = 25;
            Expand_Pit.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "2feb50a6a40f4f50982e89fd276f6f15", // bullat
                            contentsBasePosition = new Vector2(20, 9),
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
                            contentsBasePosition = new Vector2(8, 20),
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
                            enemyBehaviourGuid = "2d4f8b5404614e7d8b235006acde427a", // shotgat
                            contentsBasePosition = new Vector2(20, 20),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "72d2f44431da43b8a3bae7d8a114a46d", // bullat_shark
                            contentsBasePosition = new Vector2(2, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "72d2f44431da43b8a3bae7d8a114a46d", // bullat_shark
                            contentsBasePosition = new Vector2(25, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "72d2f44431da43b8a3bae7d8a114a46d", // bullat_shark
                            contentsBasePosition = new Vector2(2, 22),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "72d2f44431da43b8a3bae7d8a114a46d", // bullat_shark
                            contentsBasePosition = new Vector2(25, 22),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(20, 9),
                        new Vector2(8, 20),
                        new Vector2(7, 8),
                        new Vector2(20, 20),
                        new Vector2(2, 2),
                        new Vector2(25, 2),
                        new Vector2(2, 22),
                        new Vector2(25, 22)
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
            RoomBuilder.AddExitToRoom(Expand_Pit, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Pit, new Vector2(14, 26), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Pit, new Vector2(12, 11), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Pit, new Vector2(14, 13), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Pit, new Vector2(7, 7), EnemyBehaviourGuid: "ed37fa13e0fa4fcf8239643957c51293"); // gigi
            RoomBuilder.AddObjectToRoom(Expand_Pit, new Vector2(20, 7), EnemyBehaviourGuid: "ed37fa13e0fa4fcf8239643957c51293"); // gigi
            RoomBuilder.AddObjectToRoom(Expand_Pit, new Vector2(7, 17), EnemyBehaviourGuid: "af84951206324e349e1f13f9b7b60c1a"); // skusket
            RoomBuilder.AddObjectToRoom(Expand_Pit, new Vector2(20, 17), EnemyBehaviourGuid: "af84951206324e349e1f13f9b7b60c1a"); // skusket
            RoomBuilder.GenerateRoomLayout(Expand_Pit, "Neighborino_Pit_Layout");

            Expand_Singer.name = "Expand Neighborino Singer";
            Expand_Singer.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Singer.GUID = Guid.NewGuid().ToString();
            Expand_Singer.PreventMirroring = false;
            Expand_Singer.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Singer.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Singer.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Singer.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Singer.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Singer.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Singer.pits = new List<PrototypeRoomPitEntry>();
            Expand_Singer.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Singer.placedObjectPositions = new List<Vector2>();
            Expand_Singer.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Singer.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Singer.overriddenTilesets = 0;
            Expand_Singer.prerequisites = new List<DungeonPrerequisite>();
            Expand_Singer.InvalidInCoop = false;
            Expand_Singer.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Singer.preventAddedDecoLayering = false;
            Expand_Singer.precludeAllTilemapDrawing = false;
            Expand_Singer.drawPrecludedCeilingTiles = false;
            Expand_Singer.preventBorders = false;
            Expand_Singer.preventFacewallAO = false;
            Expand_Singer.usesCustomAmbientLight = false;
            Expand_Singer.customAmbientLight = Color.white;
            Expand_Singer.ForceAllowDuplicates = false;
            Expand_Singer.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Singer.IsLostWoodsRoom = false;
            Expand_Singer.UseCustomMusic = false;
            Expand_Singer.UseCustomMusicState = false;
            Expand_Singer.CustomMusicEvent = string.Empty;
            Expand_Singer.UseCustomMusicSwitch = false;
            Expand_Singer.CustomMusicSwitch = string.Empty;
            Expand_Singer.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Singer.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Singer.Width = 27;
            Expand_Singer.Height = 14;
            Expand_Singer.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "8b4a938cdbc64e64822e841e482ba3d2", // jammomancer
                            contentsBasePosition = new Vector2(23, 11),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "f905765488874846b7ff257ff81d6d0c", // fungun
                            contentsBasePosition = new Vector2(13, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "f905765488874846b7ff257ff81d6d0c", // fungun
                            contentsBasePosition = new Vector2(16, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "88b6b6a93d4b4234a67844ef4728382c", //
                            contentsBasePosition = new Vector2(13, 12),
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
                        new Vector2(23, 11),
                        new Vector2(13, 2),
                        new Vector2(16, 2),
                        new Vector2(13, 12)
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
            RoomBuilder.AddExitToRoom(Expand_Singer, new Vector2(0, 3), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Singer, new Vector2(28, 3), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(Expand_Singer, new Vector2(3, 11), EnemyBehaviourGuid: "8b4a938cdbc64e64822e841e482ba3d2"); // jammomancer
            RoomBuilder.AddObjectToRoom(Expand_Singer, new Vector2(13, 2), EnemyBehaviourGuid: "f905765488874846b7ff257ff81d6d0c"); // fungun
            RoomBuilder.AddObjectToRoom(Expand_Singer, new Vector2(9, 3), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); //
            RoomBuilder.AddObjectToRoom(Expand_Singer, new Vector2(18, 3), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); //
            RoomBuilder.GenerateRoomLayout(Expand_Singer, "Neighborino_Singer_Layout");

            Expand_TableRoom2.name = "Expand Neighborino Table Room 2";
            Expand_TableRoom2.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_TableRoom2.GUID = Guid.NewGuid().ToString();
            Expand_TableRoom2.PreventMirroring = false;
            Expand_TableRoom2.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_TableRoom2.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_TableRoom2.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_TableRoom2.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_TableRoom2.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_TableRoom2.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_TableRoom2.pits = new List<PrototypeRoomPitEntry>();
            Expand_TableRoom2.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_TableRoom2.placedObjectPositions = new List<Vector2>();
            Expand_TableRoom2.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_TableRoom2.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_TableRoom2.overriddenTilesets = 0;
            Expand_TableRoom2.prerequisites = new List<DungeonPrerequisite>();
            Expand_TableRoom2.InvalidInCoop = false;
            Expand_TableRoom2.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_TableRoom2.preventAddedDecoLayering = false;
            Expand_TableRoom2.precludeAllTilemapDrawing = false;
            Expand_TableRoom2.drawPrecludedCeilingTiles = false;
            Expand_TableRoom2.preventBorders = false;
            Expand_TableRoom2.preventFacewallAO = false;
            Expand_TableRoom2.usesCustomAmbientLight = false;
            Expand_TableRoom2.customAmbientLight = Color.white;
            Expand_TableRoom2.ForceAllowDuplicates = false;
            Expand_TableRoom2.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_TableRoom2.IsLostWoodsRoom = false;
            Expand_TableRoom2.UseCustomMusic = false;
            Expand_TableRoom2.UseCustomMusicState = false;
            Expand_TableRoom2.CustomMusicEvent = string.Empty;
            Expand_TableRoom2.UseCustomMusicSwitch = false;
            Expand_TableRoom2.CustomMusicSwitch = string.Empty;
            Expand_TableRoom2.overrideRoomVisualTypeForSecretRooms = false;
            Expand_TableRoom2.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_TableRoom2.Width = 16;
            Expand_TableRoom2.Height = 14;
            Expand_TableRoom2.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "98ea2fe181ab4323ab6e9981955a9bca", // shambling_round
                            contentsBasePosition = new Vector2(7, 6),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "3cadf10c489b461f9fb8814abc1a09c1", // minelet
                            contentsBasePosition = new Vector2(6, 8),
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
                        new Vector2(7, 6),
                        new Vector2(6, 8)
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
            RoomBuilder.AddExitToRoom(Expand_TableRoom2, new Vector2(0, 7), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_TableRoom2, new Vector2(17, 7), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_TableRoom2, new Vector2(8, 15), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_TableRoom2, new Vector2(8, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(13, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(13, 9), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(13, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(2, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(2, 9), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(2, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(4, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(7, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(10, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(4, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(7, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(10, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(7, 6), EnemyBehaviourGuid: "eed5addcc15148179f300cc0d9ee7f94"); // sporge
            RoomBuilder.AddObjectToRoom(Expand_TableRoom2, new Vector2(6, 8), EnemyBehaviourGuid: "f905765488874846b7ff257ff81d6d0c"); // fungun
            RoomBuilder.GenerateBasicRoomLayout(Expand_TableRoom2);


            Expand_OilRoom.name = "Expand Neighborino Oil Room";
            Expand_OilRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_OilRoom.GUID = Guid.NewGuid().ToString();
            Expand_OilRoom.PreventMirroring = false;
            Expand_OilRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_OilRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_OilRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_OilRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_OilRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_OilRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_OilRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_OilRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_OilRoom.placedObjectPositions = new List<Vector2>();
            Expand_OilRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_OilRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_OilRoom.overriddenTilesets = 0;
            Expand_OilRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_OilRoom.InvalidInCoop = false;
            Expand_OilRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_OilRoom.preventAddedDecoLayering = false;
            Expand_OilRoom.precludeAllTilemapDrawing = false;
            Expand_OilRoom.drawPrecludedCeilingTiles = false;
            Expand_OilRoom.preventBorders = false;
            Expand_OilRoom.preventFacewallAO = false;
            Expand_OilRoom.usesCustomAmbientLight = false;
            Expand_OilRoom.customAmbientLight = Color.white;
            Expand_OilRoom.ForceAllowDuplicates = false;
            Expand_OilRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_OilRoom.IsLostWoodsRoom = false;
            Expand_OilRoom.UseCustomMusic = false;
            Expand_OilRoom.UseCustomMusicState = false;
            Expand_OilRoom.CustomMusicEvent = string.Empty;
            Expand_OilRoom.UseCustomMusicSwitch = false;
            Expand_OilRoom.CustomMusicSwitch = string.Empty;
            Expand_OilRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_OilRoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_OilRoom.Width = 23;
            Expand_OilRoom.Height = 19;
            Expand_OilRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "ffdc8680bdaa487f8f31995539f74265", // muzzle_wisp
                            contentsBasePosition = new Vector2(7, 9),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "d8a445ea4d944cc1b55a40f22821ae69", // muzzle_flare
                            contentsBasePosition = new Vector2(15, 9),
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
                        new Vector2(7, 9),
                        new Vector2(15, 9)
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
                            enemyBehaviourGuid = "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                            contentsBasePosition = new Vector2(7, 5),
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
                            contentsBasePosition = new Vector2(15, 5),
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
                            contentsBasePosition = new Vector2(7, 13),
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
                            contentsBasePosition = new Vector2(15, 13),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(7, 5),
                        new Vector2(15, 5),
                        new Vector2(7, 13),
                        new Vector2(15, 13)
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
            RoomBuilder.AddExitToRoom(Expand_OilRoom, new Vector2(0, 9), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_OilRoom, new Vector2(24, 10), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_OilRoom, new Vector2(11, 20), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_OilRoom, new Vector2(10, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(3, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(3, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(3, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(3, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(11, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(11, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(11, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(11, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(19, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(19, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(19, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(19, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.OilDrum, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(7, 9), EnemyBehaviourGuid: "9d50684ce2c044e880878e86dbada919"); // coaler
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(15, 13), EnemyBehaviourGuid: "9d50684ce2c044e880878e86dbada919"); // coaler
            RoomBuilder.AddObjectToRoom(Expand_OilRoom, new Vector2(15, 6), EnemyBehaviourGuid: "9d50684ce2c044e880878e86dbada919"); // coaler
            RoomBuilder.GenerateBasicRoomLayout(Expand_OilRoom);

            Expand_Walkway.name = "Expand Neighborino Walkway";
            Expand_Walkway.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Walkway.GUID = Guid.NewGuid().ToString();
            Expand_Walkway.PreventMirroring = false;
            Expand_Walkway.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Walkway.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Walkway.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Walkway.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Walkway.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Walkway.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Walkway.pits = new List<PrototypeRoomPitEntry>();
            Expand_Walkway.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Walkway.placedObjectPositions = new List<Vector2>();
            Expand_Walkway.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Walkway.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Walkway.overriddenTilesets = 0;
            Expand_Walkway.prerequisites = new List<DungeonPrerequisite>();
            Expand_Walkway.InvalidInCoop = false;
            Expand_Walkway.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Walkway.preventAddedDecoLayering = false;
            Expand_Walkway.precludeAllTilemapDrawing = false;
            Expand_Walkway.drawPrecludedCeilingTiles = false;
            Expand_Walkway.preventBorders = false;
            Expand_Walkway.preventFacewallAO = false;
            Expand_Walkway.usesCustomAmbientLight = false;
            Expand_Walkway.customAmbientLight = Color.white;
            Expand_Walkway.ForceAllowDuplicates = false;
            Expand_Walkway.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Walkway.IsLostWoodsRoom = false;
            Expand_Walkway.UseCustomMusic = false;
            Expand_Walkway.UseCustomMusicState = false;
            Expand_Walkway.CustomMusicEvent = string.Empty;
            Expand_Walkway.UseCustomMusicSwitch = false;
            Expand_Walkway.CustomMusicSwitch = string.Empty;
            Expand_Walkway.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Walkway.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Walkway.Width = 23;
            Expand_Walkway.Height = 27;
            Expand_Walkway.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_Walkway, new Vector2(12, 28), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Walkway, new Vector2(11, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_Walkway, new Vector2(18, 2), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Walkway, new Vector2(2, 24), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Walkway, new Vector2(8, 8), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // 
            RoomBuilder.AddObjectToRoom(Expand_Walkway, new Vector2(14, 8), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // 
            RoomBuilder.AddObjectToRoom(Expand_Walkway, new Vector2(16, 18), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // 
            RoomBuilder.AddObjectToRoom(Expand_Walkway, new Vector2(6, 18), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // 
            RoomBuilder.AddObjectToRoom(Expand_Walkway, new Vector2(14, 13), EnemyBehaviourGuid: "2752019b770f473193b08b4005dc781f"); // veteran_shotgun_kin 
            RoomBuilder.AddObjectToRoom(Expand_Walkway, new Vector2(8, 13), EnemyBehaviourGuid: "2752019b770f473193b08b4005dc781f"); // veteran_shotgun_kin
            RoomBuilder.GenerateRoomLayout(Expand_Walkway, "Neighborino_Walkway_Layout");

            Expand_SpiderMaze.name = "Expand Apache SpiderMaze";
            Expand_SpiderMaze.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_SpiderMaze.GUID = Guid.NewGuid().ToString();
            Expand_SpiderMaze.PreventMirroring = false;
            Expand_SpiderMaze.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_SpiderMaze.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_SpiderMaze.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_SpiderMaze, new Vector2(0, 13), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_SpiderMaze, new Vector2(4, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_SpiderMaze, new Vector2(0, 32), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_SpiderMaze, new Vector2(23, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_SpiderMaze, new Vector2(51, 13), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_SpiderMaze, new Vector2(9, 51), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_SpiderMaze, new Vector2(51, 29), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_SpiderMaze, new Vector2(46, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_SpiderMaze, new Vector2(36, 51), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(19, 20), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(17, 21), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(25, 21), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(20, 18), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(20, 25), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(23, 36), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(28, 36), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(25, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(41, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(2, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(15, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(20, 40), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(45, 44), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(32, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsVertical_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(9, 34), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.DoorsHorizontal_Catacombs, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(21, 21), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(24, 34), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(30, 30), EnemyBehaviourGuid: "249db525a9464e5282d02162c88e0357"); // spent
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(6, 35), EnemyBehaviourGuid: "249db525a9464e5282d02162c88e0357"); // spent
            RoomBuilder.AddObjectToRoom(Expand_SpiderMaze, new Vector2(29, 34), EnemyBehaviourGuid: "5288e86d20184fa69c91ceb642d31474"); // gummy
            RoomBuilder.GenerateRoomLayout(Expand_SpiderMaze, "Expand_Apache_SpiderMaze_Layout");

            Expand_BlobRoom.name = "Expand Neighborino Blob Room";
            Expand_BlobRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_BlobRoom.GUID = Guid.NewGuid().ToString();
            Expand_BlobRoom.PreventMirroring = false;
            Expand_BlobRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_BlobRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_BlobRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_BlobRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_BlobRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_BlobRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_BlobRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_BlobRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_BlobRoom.placedObjectPositions = new List<Vector2>();
            Expand_BlobRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_BlobRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_BlobRoom.overriddenTilesets = 0;
            Expand_BlobRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_BlobRoom.InvalidInCoop = false;
            Expand_BlobRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_BlobRoom.preventAddedDecoLayering = false;
            Expand_BlobRoom.precludeAllTilemapDrawing = false;
            Expand_BlobRoom.drawPrecludedCeilingTiles = false;
            Expand_BlobRoom.preventBorders = false;
            Expand_BlobRoom.preventFacewallAO = false;
            Expand_BlobRoom.usesCustomAmbientLight = false;
            Expand_BlobRoom.customAmbientLight = Color.white;
            Expand_BlobRoom.ForceAllowDuplicates = false;
            Expand_BlobRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_BlobRoom.IsLostWoodsRoom = false;
            Expand_BlobRoom.UseCustomMusic = false;
            Expand_BlobRoom.UseCustomMusicState = false;
            Expand_BlobRoom.CustomMusicEvent = string.Empty;
            Expand_BlobRoom.UseCustomMusicSwitch = false;
            Expand_BlobRoom.CustomMusicSwitch = string.Empty;
            Expand_BlobRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_BlobRoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_BlobRoom.overrideRoomVisualType = -1;
            Expand_BlobRoom.Width = 22;
            Expand_BlobRoom.Height = 17;
            Expand_BlobRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "e61cab252cfb435db9172adc96ded75f", // poisbulon
                            contentsBasePosition = new Vector2(17, 4),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "e61cab252cfb435db9172adc96ded75f", // poisbulon
                            contentsBasePosition = new Vector2(4, 4),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "e61cab252cfb435db9172adc96ded75f", // poisbulon
                            contentsBasePosition = new Vector2(19, 11),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "062b9b64371e46e195de17b6f10e47c8", // bloodbulon
                            contentsBasePosition = new Vector2(12, 12),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "062b9b64371e46e195de17b6f10e47c8", // bloodbulon
                            contentsBasePosition = new Vector2(7, 4),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "022d7c822bc146b58fe3b0287568aaa2", // blizzbulon
                            contentsBasePosition = new Vector2(17, 5),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "022d7c822bc146b58fe3b0287568aaa2", // blizzbulon
                            contentsBasePosition = new Vector2(8, 11),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                    },
                    placedObjectBasePositions = new List<Vector2>() {
                        new Vector2(17, 4),
                        new Vector2(4, 4),
                        new Vector2(19, 11),
                        new Vector2(12, 12),
                        new Vector2(7, 4),
                        new Vector2(17, 5),
                        new Vector2(8, 11),
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
            RoomBuilder.AddExitToRoom(Expand_BlobRoom, new Vector2(0, 7), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_BlobRoom, new Vector2(11, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_BlobRoom, new Vector2(23, 7), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_BlobRoom, new Vector2(11, 18), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_BlobRoom, new Vector2(10, 5), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // Blobulon
            RoomBuilder.AddObjectToRoom(Expand_BlobRoom, new Vector2(15, 5), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // Blobulon
            RoomBuilder.AddObjectToRoom(Expand_BlobRoom, new Vector2(6, 8), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // Blobulon
            RoomBuilder.AddObjectToRoom(Expand_BlobRoom, new Vector2(15, 8), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // Blobulon
            // RoomBuilder.AddObjectToRoom(Expand_BlobRoom, new Vector2(8, 12), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // Blobulon
            // RoomBuilder.AddObjectToRoom(Expand_BlobRoom, new Vector2(13, 12), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // Blobulon
            RoomBuilder.GenerateRoomLayout(Expand_BlobRoom, "Expand_Neighborino_BlobRoom_Layout");

            /*Expand_CubeRoom.name = "Expand Neighborino Cube Room";
            Expand_CubeRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_CubeRoom.GUID = Guid.NewGuid().ToString();
            Expand_CubeRoom.PreventMirroring = false;
            Expand_CubeRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_CubeRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_CubeRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_CubeRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_CubeRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_CubeRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_CubeRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_CubeRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_CubeRoom.placedObjectPositions = new List<Vector2>();
            Expand_CubeRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_CubeRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_CubeRoom.overriddenTilesets = 0;
            Expand_CubeRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_CubeRoom.InvalidInCoop = false;
            Expand_CubeRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_CubeRoom.preventAddedDecoLayering = false;
            Expand_CubeRoom.precludeAllTilemapDrawing = false;
            Expand_CubeRoom.drawPrecludedCeilingTiles = false;
            Expand_CubeRoom.preventBorders = false;
            Expand_CubeRoom.preventFacewallAO = false;
            Expand_CubeRoom.usesCustomAmbientLight = false;
            Expand_CubeRoom.customAmbientLight = Color.white;
            Expand_CubeRoom.ForceAllowDuplicates = false;
            Expand_CubeRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_CubeRoom.IsLostWoodsRoom = false;
            Expand_CubeRoom.UseCustomMusic = false;
            Expand_CubeRoom.UseCustomMusicState = false;
            Expand_CubeRoom.CustomMusicEvent = string.Empty;
            Expand_CubeRoom.UseCustomMusicSwitch = false;
            Expand_CubeRoom.CustomMusicSwitch = string.Empty;
            Expand_CubeRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_CubeRoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_CubeRoom.overrideRoomVisualType = -1;
            Expand_CubeRoom.Width = 17;
            Expand_CubeRoom.Height = 25;
            Expand_CubeRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_CubeRoom, new Vector2(1, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_CubeRoom, new Vector2(1, 26), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_CubeRoom, new Vector2(7, 3), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // metal_cube_guy (trap version)
            RoomBuilder.AddObjectToRoom(Expand_CubeRoom, new Vector2(11, 11), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // metal_cube_guy (trap version)
            RoomBuilder.AddObjectToRoom(Expand_CubeRoom, new Vector2(7, 20), EnemyBehaviourGuid: "ba928393c8ed47819c2c5f593100a5bc"); // metal_cube_guy (trap version)
            RoomBuilder.GenerateRoomLayout(Expand_CubeRoom, "Expand_Neighborino_CubeRoom_Layout");*/

            Expand_HellInACell.name = "Expand Neighborino Hell In a Cell";
            Expand_HellInACell.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_HellInACell.GUID = Guid.NewGuid().ToString();
            Expand_HellInACell.PreventMirroring = false;
            Expand_HellInACell.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_HellInACell.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_HellInACell.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_HellInACell.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_HellInACell.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_HellInACell.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_HellInACell.pits = new List<PrototypeRoomPitEntry>();
            Expand_HellInACell.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_HellInACell.placedObjectPositions = new List<Vector2>();
            Expand_HellInACell.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_HellInACell.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_HellInACell.overriddenTilesets = 0;
            Expand_HellInACell.prerequisites = new List<DungeonPrerequisite>();
            Expand_HellInACell.InvalidInCoop = false;
            Expand_HellInACell.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_HellInACell.preventAddedDecoLayering = false;
            Expand_HellInACell.precludeAllTilemapDrawing = false;
            Expand_HellInACell.drawPrecludedCeilingTiles = false;
            Expand_HellInACell.preventBorders = false;
            Expand_HellInACell.preventFacewallAO = false;
            Expand_HellInACell.usesCustomAmbientLight = false;
            Expand_HellInACell.customAmbientLight = Color.white;
            Expand_HellInACell.ForceAllowDuplicates = false;
            Expand_HellInACell.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_HellInACell.IsLostWoodsRoom = false;
            Expand_HellInACell.UseCustomMusic = false;
            Expand_HellInACell.UseCustomMusicState = false;
            Expand_HellInACell.CustomMusicEvent = string.Empty;
            Expand_HellInACell.UseCustomMusicSwitch = false;
            Expand_HellInACell.CustomMusicSwitch = string.Empty;
            Expand_HellInACell.overrideRoomVisualTypeForSecretRooms = false;
            Expand_HellInACell.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_HellInACell.overrideRoomVisualType = -1;
            Expand_HellInACell.Width = 23;
            Expand_HellInACell.Height = 14;
            Expand_HellInACell.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "022d7c822bc146b58fe3b0287568aaa2", // blizzbulon
                            contentsBasePosition = new Vector2(13, 6),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "022d7c822bc146b58fe3b0287568aaa2", // blizzbulon
                            contentsBasePosition = new Vector2(9, 6),
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
                        new Vector2(13, 6),
                        new Vector2(9, 6)
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
            RoomBuilder.AddExitToRoom(Expand_HellInACell, new Vector2(0, 7), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_HellInACell, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_HellInACell, new Vector2(24, 7), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_HellInACell, new Vector2(14, 15), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(3, 1), EnemyBehaviourGuid: "f155fd2759764f4a9217db29dd21b7eb"); // mountain_cube
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(8, 1), EnemyBehaviourGuid: "f155fd2759764f4a9217db29dd21b7eb"); // mountain_cube
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(18, 1), EnemyBehaviourGuid: "f155fd2759764f4a9217db29dd21b7eb"); // mountain_cube
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(3, 12), EnemyBehaviourGuid: "f155fd2759764f4a9217db29dd21b7eb"); // mountain_cube
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(8, 12), EnemyBehaviourGuid: "f155fd2759764f4a9217db29dd21b7eb"); // mountain_cube
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(18, 12), EnemyBehaviourGuid: "f155fd2759764f4a9217db29dd21b7eb"); // mountain_cube
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(1, 3), EnemyBehaviourGuid: "f155fd2759764f4a9217db29dd21b7eb"); // mountain_cube
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(1, 11), EnemyBehaviourGuid: "f155fd2759764f4a9217db29dd21b7eb"); // mountain_cube
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(20, 3), EnemyBehaviourGuid: "f155fd2759764f4a9217db29dd21b7eb"); // mountain_cube
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(20, 11), EnemyBehaviourGuid: "f155fd2759764f4a9217db29dd21b7eb"); // mountain_cube
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(11, 6), EnemyBehaviourGuid: "864ea5a6a9324efc95a0dd2407f42810"); // cubulon
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(9, 4), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulon
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(9, 7), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulon
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(13, 7), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulon
            RoomBuilder.AddObjectToRoom(Expand_HellInACell, new Vector2(16, 8), EnemyBehaviourGuid: "42be66373a3d4d89b91a35c9ff8adfec"); // blobulon
            RoomBuilder.GenerateRoomLayout(Expand_HellInACell, "Expand_Neighborino_HellInACell_Layout");

            Expand_IceIsNice.name = "Expand Neighborino Ice is Nice";
            Expand_IceIsNice.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_IceIsNice.GUID = Guid.NewGuid().ToString();
            Expand_IceIsNice.PreventMirroring = false;
            Expand_IceIsNice.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_IceIsNice.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_IceIsNice.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_IceIsNice.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_IceIsNice.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_IceIsNice.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_IceIsNice.pits = new List<PrototypeRoomPitEntry>();
            Expand_IceIsNice.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_IceIsNice.placedObjectPositions = new List<Vector2>();
            Expand_IceIsNice.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_IceIsNice.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_IceIsNice.overriddenTilesets = 0;
            Expand_IceIsNice.prerequisites = new List<DungeonPrerequisite>();
            Expand_IceIsNice.InvalidInCoop = false;
            Expand_IceIsNice.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_IceIsNice.preventAddedDecoLayering = false;
            Expand_IceIsNice.precludeAllTilemapDrawing = false;
            Expand_IceIsNice.drawPrecludedCeilingTiles = false;
            Expand_IceIsNice.preventBorders = false;
            Expand_IceIsNice.preventFacewallAO = false;
            Expand_IceIsNice.usesCustomAmbientLight = false;
            Expand_IceIsNice.customAmbientLight = Color.white;
            Expand_IceIsNice.ForceAllowDuplicates = false;
            Expand_IceIsNice.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_IceIsNice.IsLostWoodsRoom = false;
            Expand_IceIsNice.UseCustomMusic = false;
            Expand_IceIsNice.UseCustomMusicState = false;
            Expand_IceIsNice.CustomMusicEvent = string.Empty;
            Expand_IceIsNice.UseCustomMusicSwitch = false;
            Expand_IceIsNice.CustomMusicSwitch = string.Empty;
            Expand_IceIsNice.overrideRoomVisualTypeForSecretRooms = false;
            Expand_IceIsNice.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_IceIsNice.overrideRoomVisualType = -1;
            Expand_IceIsNice.Width = 22;
            Expand_IceIsNice.Height = 14;
            Expand_IceIsNice.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                            contentsBasePosition = new Vector2(15, 9),
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
                            contentsBasePosition = new Vector2(7, 4),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "3f6d6b0c4a7c4690807435c7b37c35a5", // agonizer
                            contentsBasePosition = new Vector2(7, 4),
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
                        new Vector2(15, 9),
                        new Vector2(7, 4),
                        new Vector2(11, 7)
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
            RoomBuilder.AddExitToRoom(Expand_IceIsNice, new Vector2(0, 7), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_IceIsNice, new Vector2(11, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_IceIsNice, new Vector2(23, 7), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_IceIsNice, new Vector2(11, 15), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_IceIsNice, new Vector2(15, 9), EnemyBehaviourGuid: "9b2cf2949a894599917d4d391a0b7394"); // high_gunjurer
            RoomBuilder.AddObjectToRoom(Expand_IceIsNice, new Vector2(7, 4), EnemyBehaviourGuid: "9b2cf2949a894599917d4d391a0b7394"); // high_gunjurer
            RoomBuilder.AddObjectToRoom(Expand_IceIsNice, new Vector2(15, 4), EnemyBehaviourGuid: "70216cae6c1346309d86d4a0b4603045"); // veteran_bulletkin
            RoomBuilder.AddObjectToRoom(Expand_IceIsNice, new Vector2(7, 9), EnemyBehaviourGuid: "70216cae6c1346309d86d4a0b4603045"); // veteran_bulletkin
            RoomBuilder.GenerateRoomLayout(Expand_IceIsNice, "Expand_Neighborino_IceIsNice_Layout");

            Expand_IceScotch.name = "Expand Neighborino Ice Scotch";
            Expand_IceScotch.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_IceScotch.GUID = Guid.NewGuid().ToString();
            Expand_IceScotch.PreventMirroring = false;
            Expand_IceScotch.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_IceScotch.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_IceScotch.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_IceScotch.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_IceScotch.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_IceScotch.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_IceScotch.pits = new List<PrototypeRoomPitEntry>();
            Expand_IceScotch.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_IceScotch.placedObjectPositions = new List<Vector2>();
            Expand_IceScotch.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_IceScotch.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_IceScotch.overriddenTilesets = 0;
            Expand_IceScotch.prerequisites = new List<DungeonPrerequisite>();
            Expand_IceScotch.InvalidInCoop = false;
            Expand_IceScotch.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_IceScotch.preventAddedDecoLayering = false;
            Expand_IceScotch.precludeAllTilemapDrawing = false;
            Expand_IceScotch.drawPrecludedCeilingTiles = false;
            Expand_IceScotch.preventBorders = false;
            Expand_IceScotch.preventFacewallAO = false;
            Expand_IceScotch.usesCustomAmbientLight = false;
            Expand_IceScotch.customAmbientLight = Color.white;
            Expand_IceScotch.ForceAllowDuplicates = false;
            Expand_IceScotch.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_IceScotch.IsLostWoodsRoom = false;
            Expand_IceScotch.UseCustomMusic = false;
            Expand_IceScotch.UseCustomMusicState = false;
            Expand_IceScotch.CustomMusicEvent = string.Empty;
            Expand_IceScotch.UseCustomMusicSwitch = false;
            Expand_IceScotch.CustomMusicSwitch = string.Empty;
            Expand_IceScotch.overrideRoomVisualTypeForSecretRooms = false;
            Expand_IceScotch.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_IceScotch.overrideRoomVisualType = -1;
            Expand_IceScotch.Width = 18;
            Expand_IceScotch.Height = 16;
            Expand_IceScotch.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "336190e29e8a4f75ab7486595b700d4a", // skullet
                            contentsBasePosition = new Vector2(9, 4),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "336190e29e8a4f75ab7486595b700d4a", // skullet
                            contentsBasePosition = new Vector2(9, 7),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "336190e29e8a4f75ab7486595b700d4a", // skullet
                            contentsBasePosition = new Vector2(8, 10),
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
                        new Vector2(9, 4),
                        new Vector2(9, 7),
                        new Vector2(8, 10)
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
            RoomBuilder.AddExitToRoom(Expand_IceScotch, new Vector2(0, 6), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_IceScotch, new Vector2(19, 10), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(Expand_IceScotch, new Vector2(11, 7), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_IceScotch, new Vector2(9, 6), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_IceScotch, new Vector2(7, 7), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_IceScotch, new Vector2(9, 8), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.GenerateRoomLayout(Expand_IceScotch, "Expand_Neighborino_IceScotch_Layout");

            Expand_MrPresident.name = "Expand Neighborino Mr President";
            Expand_MrPresident.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_MrPresident.GUID = Guid.NewGuid().ToString();
            Expand_MrPresident.PreventMirroring = false;
            Expand_MrPresident.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_MrPresident.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_MrPresident.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_MrPresident.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_MrPresident.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_MrPresident.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_MrPresident.pits = new List<PrototypeRoomPitEntry>();
            Expand_MrPresident.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_MrPresident.placedObjectPositions = new List<Vector2>();
            Expand_MrPresident.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_MrPresident.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_MrPresident.overriddenTilesets = 0;
            Expand_MrPresident.prerequisites = new List<DungeonPrerequisite>();
            Expand_MrPresident.InvalidInCoop = false;
            Expand_MrPresident.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_MrPresident.preventAddedDecoLayering = false;
            Expand_MrPresident.precludeAllTilemapDrawing = false;
            Expand_MrPresident.drawPrecludedCeilingTiles = false;
            Expand_MrPresident.preventBorders = false;
            Expand_MrPresident.preventFacewallAO = false;
            Expand_MrPresident.usesCustomAmbientLight = false;
            Expand_MrPresident.customAmbientLight = Color.white;
            Expand_MrPresident.ForceAllowDuplicates = false;
            Expand_MrPresident.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_MrPresident.IsLostWoodsRoom = false;
            Expand_MrPresident.UseCustomMusic = false;
            Expand_MrPresident.UseCustomMusicState = false;
            Expand_MrPresident.CustomMusicEvent = string.Empty;
            Expand_MrPresident.UseCustomMusicSwitch = false;
            Expand_MrPresident.CustomMusicSwitch = string.Empty;
            Expand_MrPresident.overrideRoomVisualTypeForSecretRooms = false;
            Expand_MrPresident.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_MrPresident.overrideRoomVisualType = -1;
            Expand_MrPresident.Width = 17;
            Expand_MrPresident.Height = 17;
            Expand_MrPresident.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "b1540990a4f1480bbcb3bea70d67f60d", // ammomancer
                            contentsBasePosition = new Vector2(8, 8),
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
                            enemyBehaviourGuid = "0239c0680f9f467dbe5c4aab7dd1eca6", // blobulon
                            contentsBasePosition = new Vector2(8, 14),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "062b9b64371e46e195de17b6f10e47c8", // bloodbulon
                            contentsBasePosition = new Vector2(15, 8),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "062b9b64371e46e195de17b6f10e47c8", // bloodbulon
                            contentsBasePosition = new Vector2(1, 8),
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
                        new Vector2(8, 8),
                        new Vector2(8, 2),
                        new Vector2(8, 14),
                        new Vector2(15, 8),
                        new Vector2(1, 8)
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
            RoomBuilder.AddExitToRoom(Expand_MrPresident, new Vector2(0, 9), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_MrPresident, new Vector2(9, 18), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_MrPresident, new Vector2(18, 8), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_MrPresident, new Vector2(8, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(6, 4), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(7, 4), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(8, 4), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(9, 4), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(10, 4), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(6, 12), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(7, 12), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(8, 12), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(9, 12), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(10, 12), ExpandObjectDatabase.WoodenBarrel);
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(8, 8), EnemyBehaviourGuid: "c50a862d19fc4d30baeba54795e8cb93"); // aged_gunsinger
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(1, 1), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // 
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(15, 1), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); // 
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(1, 15), EnemyBehaviourGuid: "128db2f0781141bcb505d8f00f9e4d47"); // 
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(15, 15), EnemyBehaviourGuid: "b54d89f9e802455cbb2b8a96a31e8259"); //
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(15, 4), EnemyBehaviourGuid: "336190e29e8a4f75ab7486595b700d4a"); // skullets
            RoomBuilder.AddObjectToRoom(Expand_MrPresident, new Vector2(1, 12), EnemyBehaviourGuid: "336190e29e8a4f75ab7486595b700d4a"); // skullets
            RoomBuilder.GenerateRoomLayout(Expand_MrPresident, "Expand_Neighborino_MrPresident_Layout");

            Expand_SawRoom.name = "Expand Neighborino Saw Room";
            Expand_SawRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_SawRoom.GUID = Guid.NewGuid().ToString();
            Expand_SawRoom.PreventMirroring = false;
            Expand_SawRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_SawRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_SawRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_SawRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_SawRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_SawRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_SawRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_SawRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_SawRoom.placedObjectPositions = new List<Vector2>();
            Expand_SawRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_SawRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_SawRoom.overriddenTilesets = 0;
            Expand_SawRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_SawRoom.InvalidInCoop = false;
            Expand_SawRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_SawRoom.preventAddedDecoLayering = false;
            Expand_SawRoom.precludeAllTilemapDrawing = false;
            Expand_SawRoom.drawPrecludedCeilingTiles = false;
            Expand_SawRoom.preventBorders = false;
            Expand_SawRoom.preventFacewallAO = false;
            Expand_SawRoom.usesCustomAmbientLight = false;
            Expand_SawRoom.customAmbientLight = Color.white;
            Expand_SawRoom.ForceAllowDuplicates = false;
            Expand_SawRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_SawRoom.IsLostWoodsRoom = false;
            Expand_SawRoom.UseCustomMusic = false;
            Expand_SawRoom.UseCustomMusicState = false;
            Expand_SawRoom.CustomMusicEvent = string.Empty;
            Expand_SawRoom.UseCustomMusicSwitch = false;
            Expand_SawRoom.CustomMusicSwitch = string.Empty;
            Expand_SawRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_SawRoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_SawRoom.overrideRoomVisualType = -1;
            Expand_SawRoom.Width = 20;
            Expand_SawRoom.Height = 26;
            Expand_SawRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "042edb1dfb614dc385d5ad1b010f2ee3", // blobuloid
                            contentsBasePosition = new Vector2(5, 12),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "042edb1dfb614dc385d5ad1b010f2ee3", // blobuloid
                            contentsBasePosition = new Vector2(14, 12),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "042edb1dfb614dc385d5ad1b010f2ee3", // blobuloid
                            contentsBasePosition = new Vector2(13, 17),
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
                        new Vector2(5, 12),
                        new Vector2(14, 12),
                        new Vector2(13, 17)
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
            RoomBuilder.AddExitToRoom(Expand_SawRoom, new Vector2(0, 13), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_SawRoom, new Vector2(21, 13), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(1, 3), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(8, 3), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(15, 3), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(1, 19), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(8, 19), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(15, 19), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(2, 8), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(9, 8), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(16, 8), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin            
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(2, 17), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(9, 17), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(16, 17), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_SawRoom, new Vector2(9, 12), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.GenerateRoomLayout(Expand_SawRoom, "Expand_Neighborino_SawRoom_Layout");






            Expand_Arena.name = "Expand Neighborino Arena";
            Expand_Arena.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Arena.GUID = Guid.NewGuid().ToString();
            Expand_Arena.PreventMirroring = false;
            Expand_Arena.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Arena.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Arena.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Arena.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Arena.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Arena.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Arena.pits = new List<PrototypeRoomPitEntry>();
            Expand_Arena.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Arena.placedObjectPositions = new List<Vector2>();
            Expand_Arena.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Arena.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Arena.overriddenTilesets = 0;
            Expand_Arena.prerequisites = new List<DungeonPrerequisite>();
            Expand_Arena.InvalidInCoop = false;
            Expand_Arena.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Arena.preventAddedDecoLayering = false;
            Expand_Arena.precludeAllTilemapDrawing = false;
            Expand_Arena.drawPrecludedCeilingTiles = false;
            Expand_Arena.preventBorders = false;
            Expand_Arena.preventFacewallAO = false;
            Expand_Arena.usesCustomAmbientLight = false;
            Expand_Arena.customAmbientLight = Color.white;
            Expand_Arena.ForceAllowDuplicates = false;
            Expand_Arena.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Arena.IsLostWoodsRoom = false;
            Expand_Arena.UseCustomMusic = false;
            Expand_Arena.UseCustomMusicState = false;
            Expand_Arena.CustomMusicEvent = string.Empty;
            Expand_Arena.UseCustomMusicSwitch = false;
            Expand_Arena.CustomMusicSwitch = string.Empty;
            Expand_Arena.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Arena.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Arena.overrideRoomVisualType = -1;
            Expand_Arena.Width = 20;
            Expand_Arena.Height = 18;
            Expand_Arena.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "128db2f0781141bcb505d8f00f9e4d47", //
                            contentsBasePosition = new Vector2(2, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "128db2f0781141bcb505d8f00f9e4d47", //
                            contentsBasePosition = new Vector2(17, 2),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "128db2f0781141bcb505d8f00f9e4d47", //
                            contentsBasePosition = new Vector2(2, 15),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "128db2f0781141bcb505d8f00f9e4d47", //
                            contentsBasePosition = new Vector2(17, 15),
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
                        new Vector2(2, 2),
                        new Vector2(17, 2),
                        new Vector2(2, 15),
                        new Vector2(17, 15)
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
                            enemyBehaviourGuid = "ec8ea75b557d4e7b8ceeaacdf6f8238c", // gun_nut
                            contentsBasePosition = new Vector2(6, 8),
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
                            contentsBasePosition = new Vector2(13, 8),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "383175a55879441d90933b5c4e60cf6f", // spectre_gun_nut
                            contentsBasePosition = new Vector2(10, 4),
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
                        new Vector2(6, 8),
                        new Vector2(13, 8),
                        new Vector2(10, 4)
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
                            enemyBehaviourGuid = "cd4a4b7f612a4ba9a720b9f97c52f38c", // lead_maiden
                            contentsBasePosition = new Vector2(9, 7),
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
                        new Vector2(9, 7)
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
            RoomBuilder.AddExitToRoom(Expand_Arena, new Vector2(0, 9), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Arena, new Vector2(21, 9), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Arena, new Vector2(10, 19), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Arena, new Vector2(10, 0), DungeonData.Direction.SOUTH);

            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(4, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(5, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(6, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(7, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(8, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(9, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(10, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(11, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(12, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(13, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(14, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(15, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(4, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(5, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(6, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(7, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(8, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(9, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(10, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(11, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(12, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(13, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(14, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(15, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 8), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 9), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 12), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(3, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 8), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 9), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 12), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(16, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(14, 5), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(14, 12), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(5, 5), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin            
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(5, 12), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(7, 8), EnemyBehaviourGuid: "88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(12, 8), EnemyBehaviourGuid: "88b6b6a93d4b4234a67844ef4728382c"); // bandana_bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(9, 5), EnemyBehaviourGuid: "70216cae6c1346309d86d4a0b4603045"); // veteran_bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Arena, new Vector2(10, 12), EnemyBehaviourGuid: "70216cae6c1346309d86d4a0b4603045"); // veteran_bullet_kin
            RoomBuilder.GenerateRoomLayout(Expand_Arena, "Expand_Neighborino_Arena_Layout");

            Expand_CaptainCrunch.name = "Expand Neighborino Captain Crunch";
            Expand_CaptainCrunch.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_CaptainCrunch.GUID = Guid.NewGuid().ToString();
            Expand_CaptainCrunch.PreventMirroring = false;
            Expand_CaptainCrunch.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_CaptainCrunch.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_CaptainCrunch.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_CaptainCrunch.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_CaptainCrunch.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_CaptainCrunch.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_CaptainCrunch.pits = new List<PrototypeRoomPitEntry>();
            Expand_CaptainCrunch.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_CaptainCrunch.placedObjectPositions = new List<Vector2>();
            Expand_CaptainCrunch.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_CaptainCrunch.roomEvents = new List<RoomEventDefinition>(0);
            Expand_CaptainCrunch.overriddenTilesets = 0;
            Expand_CaptainCrunch.prerequisites = new List<DungeonPrerequisite>();
            Expand_CaptainCrunch.InvalidInCoop = false;
            Expand_CaptainCrunch.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_CaptainCrunch.preventAddedDecoLayering = false;
            Expand_CaptainCrunch.precludeAllTilemapDrawing = false;
            Expand_CaptainCrunch.drawPrecludedCeilingTiles = false;
            Expand_CaptainCrunch.preventBorders = false;
            Expand_CaptainCrunch.preventFacewallAO = false;
            Expand_CaptainCrunch.usesCustomAmbientLight = false;
            Expand_CaptainCrunch.customAmbientLight = Color.white;
            Expand_CaptainCrunch.ForceAllowDuplicates = false;
            Expand_CaptainCrunch.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_CaptainCrunch.IsLostWoodsRoom = false;
            Expand_CaptainCrunch.UseCustomMusic = false;
            Expand_CaptainCrunch.UseCustomMusicState = false;
            Expand_CaptainCrunch.CustomMusicEvent = string.Empty;
            Expand_CaptainCrunch.UseCustomMusicSwitch = false;
            Expand_CaptainCrunch.CustomMusicSwitch = string.Empty;
            Expand_CaptainCrunch.overrideRoomVisualTypeForSecretRooms = false;
            Expand_CaptainCrunch.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_CaptainCrunch.overrideRoomVisualType = -1;
            Expand_CaptainCrunch.Width = 28;
            Expand_CaptainCrunch.Height = 28;
            Expand_CaptainCrunch.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_CaptainCrunch, new Vector2(0, 14), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_CaptainCrunch, new Vector2(29, 14), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_CaptainCrunch, new Vector2(14, 29), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_CaptainCrunch, new Vector2(14, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(13, 2), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(13, 6), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(13, 10), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(13, 15), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(13, 19), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(13, 23), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(2, 13), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(6, 13), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(10, 13), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(15, 13), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(19, 13), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(23, 13), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(13, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(14, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(13, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CaptainCrunch, new Vector2(14, 14), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.GenerateRoomLayout(Expand_CaptainCrunch, "Expand_Neighborino_CaptainCrunch_Layout");

            Expand_CorridorOfDoom.name = "Expand Neighborino Corridor of Doom";
            Expand_CorridorOfDoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_CorridorOfDoom.GUID = Guid.NewGuid().ToString();
            Expand_CorridorOfDoom.PreventMirroring = false;
            Expand_CorridorOfDoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_CorridorOfDoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_CorridorOfDoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_CorridorOfDoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_CorridorOfDoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_CorridorOfDoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_CorridorOfDoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_CorridorOfDoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_CorridorOfDoom.placedObjectPositions = new List<Vector2>();
            Expand_CorridorOfDoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_CorridorOfDoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_CorridorOfDoom.overriddenTilesets = 0;
            Expand_CorridorOfDoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_CorridorOfDoom.InvalidInCoop = false;
            Expand_CorridorOfDoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_CorridorOfDoom.preventAddedDecoLayering = false;
            Expand_CorridorOfDoom.precludeAllTilemapDrawing = false;
            Expand_CorridorOfDoom.drawPrecludedCeilingTiles = false;
            Expand_CorridorOfDoom.preventBorders = false;
            Expand_CorridorOfDoom.preventFacewallAO = false;
            Expand_CorridorOfDoom.usesCustomAmbientLight = false;
            Expand_CorridorOfDoom.customAmbientLight = Color.white;
            Expand_CorridorOfDoom.ForceAllowDuplicates = false;
            Expand_CorridorOfDoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_CorridorOfDoom.IsLostWoodsRoom = false;
            Expand_CorridorOfDoom.UseCustomMusic = false;
            Expand_CorridorOfDoom.UseCustomMusicState = false;
            Expand_CorridorOfDoom.CustomMusicEvent = string.Empty;
            Expand_CorridorOfDoom.UseCustomMusicSwitch = false;
            Expand_CorridorOfDoom.CustomMusicSwitch = string.Empty;
            Expand_CorridorOfDoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_CorridorOfDoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_CorridorOfDoom.overrideRoomVisualType = -1;
            Expand_CorridorOfDoom.Width = 34;
            Expand_CorridorOfDoom.Height = 6;
            Expand_CorridorOfDoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "1bc2a07ef87741be90c37096910843ab", // chancebulon
                            contentsBasePosition = new Vector2(16, 3),
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
                        new Vector2(16, 3)
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
            RoomBuilder.AddExitToRoom(Expand_CorridorOfDoom, new Vector2(0, 3), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_CorridorOfDoom, new Vector2(35, 3), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(15, 0), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(15, 1), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(15, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(15, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(15, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(15, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(18, 0), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(18, 1), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(18, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(18, 3), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(18, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(18, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(6, 4), EnemyBehaviourGuid: "4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(27, 4), EnemyBehaviourGuid: "4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(14, 4), EnemyBehaviourGuid: "e5cffcfabfae489da61062ea20539887"); // shroomer
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(19, 4), EnemyBehaviourGuid: "e5cffcfabfae489da61062ea20539887"); // shroomer
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(12, 3), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // blobulon
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(12, 1), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // blobulon
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(21, 2), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // blobulon
            RoomBuilder.AddObjectToRoom(Expand_CorridorOfDoom, new Vector2(22, 4), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // blobulon
            RoomBuilder.GenerateRoomLayout(Expand_CorridorOfDoom, "Expand_Neighborino_CorridorOfDoomDeath_Layout");

            Expand_FireRoom.name = "Expand Neighborino Fire Room";
            Expand_FireRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_FireRoom.GUID = Guid.NewGuid().ToString();
            Expand_FireRoom.PreventMirroring = false;
            Expand_FireRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_FireRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_FireRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_FireRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_FireRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_FireRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_FireRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_FireRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_FireRoom.placedObjectPositions = new List<Vector2>();
            Expand_FireRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_FireRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_FireRoom.overriddenTilesets = 0;
            Expand_FireRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_FireRoom.InvalidInCoop = false;
            Expand_FireRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_FireRoom.preventAddedDecoLayering = false;
            Expand_FireRoom.precludeAllTilemapDrawing = false;
            Expand_FireRoom.drawPrecludedCeilingTiles = false;
            Expand_FireRoom.preventBorders = false;
            Expand_FireRoom.preventFacewallAO = false;
            Expand_FireRoom.usesCustomAmbientLight = false;
            Expand_FireRoom.customAmbientLight = Color.white;
            Expand_FireRoom.ForceAllowDuplicates = false;
            Expand_FireRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_FireRoom.IsLostWoodsRoom = false;
            Expand_FireRoom.UseCustomMusic = false;
            Expand_FireRoom.UseCustomMusicState = false;
            Expand_FireRoom.CustomMusicEvent = string.Empty;
            Expand_FireRoom.UseCustomMusicSwitch = false;
            Expand_FireRoom.CustomMusicSwitch = string.Empty;
            Expand_FireRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_FireRoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_FireRoom.overrideRoomVisualType = -1;
            Expand_FireRoom.Width = 13;
            Expand_FireRoom.Height = 11;
            Expand_FireRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_FireRoom, new Vector2(0, 7), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_FireRoom, new Vector2(6, 12), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_FireRoom, new Vector2(14, 7), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_FireRoom, new Vector2(7, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(2, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(5, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(8, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(11, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(2, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(5, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(8, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(11, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(1, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(4, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(7, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(10, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(1, 8), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(4, 8), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(7, 8), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(10, 8), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.FlameTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(3, 3), EnemyBehaviourGuid: "4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(9, 3), EnemyBehaviourGuid: "4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(9, 7), EnemyBehaviourGuid: "4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(3, 7), EnemyBehaviourGuid: "4d37ce3d666b4ddda8039929225b7ede"); // grenade_kin
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(3, 7), EnemyBehaviourGuid: "ccf6d241dad64d989cbcaca2a8477f01"); // leadbulon
            RoomBuilder.AddObjectToRoom(Expand_FireRoom, new Vector2(9, 3), EnemyBehaviourGuid: "ccf6d241dad64d989cbcaca2a8477f01"); // leadbulon
            RoomBuilder.GenerateRoomLayout(Expand_FireRoom, "Expand_Neighborino_FireRoom_Layout");

            Expand_Pits.name = "Expand Neighborino Pits";
            Expand_Pits.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Pits.GUID = Guid.NewGuid().ToString();
            Expand_Pits.PreventMirroring = false;
            Expand_Pits.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Pits.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Pits.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Pits.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Pits.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Pits.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Pits.pits = new List<PrototypeRoomPitEntry>();
            Expand_Pits.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Pits.placedObjectPositions = new List<Vector2>();
            Expand_Pits.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Pits.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Pits.overriddenTilesets = 0;
            Expand_Pits.prerequisites = new List<DungeonPrerequisite>();
            Expand_Pits.InvalidInCoop = false;
            Expand_Pits.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Pits.preventAddedDecoLayering = false;
            Expand_Pits.precludeAllTilemapDrawing = false;
            Expand_Pits.drawPrecludedCeilingTiles = false;
            Expand_Pits.preventBorders = false;
            Expand_Pits.preventFacewallAO = false;
            Expand_Pits.usesCustomAmbientLight = false;
            Expand_Pits.customAmbientLight = Color.white;
            Expand_Pits.ForceAllowDuplicates = false;
            Expand_Pits.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Pits.IsLostWoodsRoom = false;
            Expand_Pits.UseCustomMusic = false;
            Expand_Pits.UseCustomMusicState = false;
            Expand_Pits.CustomMusicEvent = string.Empty;
            Expand_Pits.UseCustomMusicSwitch = false;
            Expand_Pits.CustomMusicSwitch = string.Empty;
            Expand_Pits.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Pits.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Pits.overrideRoomVisualType = -1;
            Expand_Pits.Width = 14;
            Expand_Pits.Height = 14;
            Expand_Pits.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "383175a55879441d90933b5c4e60cf6f", // spectre_gun_nut
                            contentsBasePosition = new Vector2(8, 8),
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
                        new Vector2(8, 8)
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
                            enemyBehaviourGuid = "1bc2a07ef87741be90c37096910843ab", // chancebulon
                            contentsBasePosition = new Vector2(5, 5),
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
                        new Vector2(5, 5)
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
            RoomBuilder.AddExitToRoom(Expand_Pits, new Vector2(0, 5), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Pits, new Vector2(5, 15), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Pits, new Vector2(15, 9), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Pits, new Vector2(9, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_Pits, new Vector2(9, 9), EnemyBehaviourGuid: "b70cbd875fea498aa7fd14b970248920"); // great_bulletshark
            RoomBuilder.GenerateRoomLayout(Expand_Pits, "Expand_Neighborino_Pits_Layout");

            Expand_SkullRoom.name = "Expand Neighborino Skull Room";
            Expand_SkullRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_SkullRoom.GUID = Guid.NewGuid().ToString();
            Expand_SkullRoom.PreventMirroring = false;
            Expand_SkullRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_SkullRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_SkullRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_SkullRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_SkullRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_SkullRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_SkullRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_SkullRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_SkullRoom.placedObjectPositions = new List<Vector2>();
            Expand_SkullRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_SkullRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_SkullRoom.overriddenTilesets = 0;
            Expand_SkullRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_SkullRoom.InvalidInCoop = false;
            Expand_SkullRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_SkullRoom.preventAddedDecoLayering = false;
            Expand_SkullRoom.precludeAllTilemapDrawing = false;
            Expand_SkullRoom.drawPrecludedCeilingTiles = false;
            Expand_SkullRoom.preventBorders = false;
            Expand_SkullRoom.preventFacewallAO = false;
            Expand_SkullRoom.usesCustomAmbientLight = false;
            Expand_SkullRoom.customAmbientLight = Color.white;
            Expand_SkullRoom.ForceAllowDuplicates = false;
            Expand_SkullRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_SkullRoom.IsLostWoodsRoom = false;
            Expand_SkullRoom.UseCustomMusic = false;
            Expand_SkullRoom.UseCustomMusicState = false;
            Expand_SkullRoom.CustomMusicEvent = string.Empty;
            Expand_SkullRoom.UseCustomMusicSwitch = false;
            Expand_SkullRoom.CustomMusicSwitch = string.Empty;
            Expand_SkullRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_SkullRoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_SkullRoom.overrideRoomVisualType = -1;
            Expand_SkullRoom.Width = 21;
            Expand_SkullRoom.Height = 20;
            Expand_SkullRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "336190e29e8a4f75ab7486595b700d4a", // skullet
                            contentsBasePosition = new Vector2(4, 8),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "336190e29e8a4f75ab7486595b700d4a", // skullet
                            contentsBasePosition = new Vector2(16, 8),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "d5a7b95774cd41f080e517bea07bf495", // revolvenant
                            contentsBasePosition = new Vector2(9, 14),
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
                        new Vector2(4, 8),
                        new Vector2(16, 8),
                        new Vector2(9, 14)
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
                            enemyBehaviourGuid = "336190e29e8a4f75ab7486595b700d4a", // skullet
                            contentsBasePosition = new Vector2(18, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "336190e29e8a4f75ab7486595b700d4a", // skullet
                            contentsBasePosition = new Vector2(2, 17),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "336190e29e8a4f75ab7486595b700d4a", // skullet
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
                            enemyBehaviourGuid = "336190e29e8a4f75ab7486595b700d4a", // skullet
                            contentsBasePosition = new Vector2(15, 8),
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
                        new Vector2(18, 17),
                        new Vector2(2, 17),
                        new Vector2(5, 8),
                        new Vector2(15, 8)
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
            RoomBuilder.AddExitToRoom(Expand_SkullRoom, new Vector2(0, 17), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_SkullRoom, new Vector2(5, 21), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_SkullRoom, new Vector2(16, 21), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_SkullRoom, new Vector2(22, 17), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(Expand_SkullRoom, new Vector2(15, 8), EnemyBehaviourGuid: "336190e29e8a4f75ab7486595b700d4a"); // skullet
            RoomBuilder.AddObjectToRoom(Expand_SkullRoom, new Vector2(5, 8), EnemyBehaviourGuid: "336190e29e8a4f75ab7486595b700d4a"); // skullet
            RoomBuilder.AddObjectToRoom(Expand_SkullRoom, new Vector2(9, 15), EnemyBehaviourGuid: "21dd14e5ca2a4a388adab5b11b69a1e1"); // shelleton
            RoomBuilder.GenerateRoomLayout(Expand_SkullRoom, "Expand_Neighborino_SkullRoom_Layout");

            Expand_TableRoomAgain.name = "Expand Neighborino Tables Again";
            Expand_TableRoomAgain.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_TableRoomAgain.GUID = Guid.NewGuid().ToString();
            Expand_TableRoomAgain.PreventMirroring = false;
            Expand_TableRoomAgain.category = PrototypeDungeonRoom.RoomCategory.HUB;
            Expand_TableRoomAgain.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_TableRoomAgain.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_TableRoomAgain.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_TableRoomAgain.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_TableRoomAgain.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_TableRoomAgain.pits = new List<PrototypeRoomPitEntry>();
            Expand_TableRoomAgain.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_TableRoomAgain.placedObjectPositions = new List<Vector2>();
            Expand_TableRoomAgain.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_TableRoomAgain.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_TableRoomAgain.overriddenTilesets = 0;
            Expand_TableRoomAgain.prerequisites = new List<DungeonPrerequisite>();
            Expand_TableRoomAgain.InvalidInCoop = false;
            Expand_TableRoomAgain.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_TableRoomAgain.preventAddedDecoLayering = false;
            Expand_TableRoomAgain.precludeAllTilemapDrawing = false;
            Expand_TableRoomAgain.drawPrecludedCeilingTiles = false;
            Expand_TableRoomAgain.preventBorders = false;
            Expand_TableRoomAgain.preventFacewallAO = false;
            Expand_TableRoomAgain.usesCustomAmbientLight = false;
            Expand_TableRoomAgain.customAmbientLight = Color.white;
            Expand_TableRoomAgain.ForceAllowDuplicates = false;
            Expand_TableRoomAgain.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_TableRoomAgain.IsLostWoodsRoom = false;
            Expand_TableRoomAgain.UseCustomMusic = false;
            Expand_TableRoomAgain.UseCustomMusicState = false;
            Expand_TableRoomAgain.CustomMusicEvent = string.Empty;
            Expand_TableRoomAgain.UseCustomMusicSwitch = false;
            Expand_TableRoomAgain.CustomMusicSwitch = string.Empty;
            Expand_TableRoomAgain.overrideRoomVisualTypeForSecretRooms = false;
            Expand_TableRoomAgain.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_TableRoomAgain.overrideRoomVisualType = -1;
            Expand_TableRoomAgain.Width = 27;
            Expand_TableRoomAgain.Height = 25;
            Expand_TableRoomAgain.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = "3f6d6b0c4a7c4690807435c7b37c35a5", // agonizer
                            contentsBasePosition = new Vector2(13, 12),
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
                        new Vector2(13, 12)
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
            RoomBuilder.AddExitToRoom(Expand_TableRoomAgain, new Vector2(0, 5), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_TableRoomAgain, new Vector2(0, 20), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_TableRoomAgain, new Vector2(28, 5), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_TableRoomAgain, new Vector2(28, 20), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_TableRoomAgain, new Vector2(6, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_TableRoomAgain, new Vector2(21, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_TableRoomAgain, new Vector2(6, 26), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_TableRoomAgain, new Vector2(21, 26), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(11, 10), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(8, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(11, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(14, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(17, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(8, 18), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(11, 18), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(14, 18), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(17, 18), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableHorizontal, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(20, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(20, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(20, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(20, 16), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(6, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(6, 10), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(6, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(6, 16), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.TableVertical, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(7, 4), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(20, 4), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(20, 21), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(7, 21), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(9, 9), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(13, 9), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(17, 9), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(9, 12), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(13, 12), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(17, 12), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(9, 15), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(13, 15), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_TableRoomAgain, new Vector2(17, 15), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.GenerateBasicRoomLayout(Expand_TableRoomAgain);

            Expand_Agony = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Agony");
            Expand_ice1 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_ice1");
            Expand_Ice2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Ice2");
            Expand_Ice3 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Ice3");
            Expand_Ice4 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Ice4");
            Expand_LargeMany = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_LargeMany");
            Expand_Roundabout = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Roundabout");
            Expand_Shells = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Shells");
            Expand_Spooky = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Spooky");
            Expand_Undead1 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Undead1");
            Expand_Undead2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Undead2");
            Expand_Undead3 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Undead3");
            Expand_Undead4 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Undead4");


            Expand_4wave = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_4wave");
            Expand_Spiralbomb = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Spiralbomb");
            Expand_Bat = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Bat");
            Expand_Batsmall = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Batsmall");
            Expand_BIRDS = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_BIRDS");
            Expand_Blobs = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Blobs");
            Expand_BoogalooFailure2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_BoogalooFailure2");
            Expand_Chess = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Chess");
            Expand_Cornerpits = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Cornerpits");
            Expand_Enclosed = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Enclosed");
            Expand_Funky = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Funky");
            Expand_Gapsniper = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Gapsniper");
            Expand_Hallway = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Hallway");

            Expand_HUB_1wave = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_HUB_1wave");
            Expand_HUB_1wave.category = PrototypeDungeonRoom.RoomCategory.HUB;
            RoomBuilder.AddObjectToRoom(Expand_HUB_1wave, new Vector2(19, 14), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());

            Expand_Islands = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Islands");
            Expand_Long = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Long");
            Expand_Mushroom = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mushroom");
            Expand_Mutant = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mutant");
            Expand_Oddshroom = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Oddshroom");
            Expand_Pitzag = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Pitzag");
            Expand_Shotgun = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Shotgun");
            Expand_Smallcentral = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Smallcentral");

            Expand_Secret_Falsechest = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Falsechest");
            Expand_Secret_Falsechest.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_Secret_Falsechest.OverrideMusicState = DungeonFloorMusicController.DungeonMusicState.CALM;

            Expand_Secret_Brother10 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Brother10", true);
            Expand_Secret_Brother9 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Brother9", true);
            Expand_Secret_Brother8 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Brother8", true);
            Expand_Secret_Brother7 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Brother7", true);
            Expand_Secret_Brother6 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Brother6", true);
            Expand_Secret_Brother5 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Brother5", true);
            Expand_Secret_Brother4 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Brother4", true);
            Expand_Secret_Brother3 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Brother3", true);
            Expand_Secret_Brother2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Brother2", true);
            Expand_Secret_Brother1 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Brother1", true);
            Expand_Secret_Witch10 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Witch10", true, true);
            Expand_Secret_Witch9 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Witch9", true, true);
            Expand_Secret_Witch8 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Witch8", true, true);
            Expand_Secret_Witch7 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Witch7", true, true);
            Expand_Secret_Witch6 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Witch6", true, true);
            Expand_Secret_Witch5 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Witch5", true, true);
            Expand_Secret_Witch4 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Witch4", true, true);
            Expand_Secret_Witch3 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Witch3", true, true);
            Expand_Secret_Witch2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Witch2", true, true);
            Expand_Secret_Witch1 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Witch1", true, true);
            Expand_Secret_Pickups = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Pickups", true, true);
            Expand_Secret_Owl10 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Owl10", true, true);
            Expand_Secret_Owl9 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Owl9", true, true);
            Expand_Secret_Owl8 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Owl8", true, true);
            Expand_Secret_Owl7 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Owl7", true, true);
            Expand_Secret_Owl6 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Owl6", true, true);
            Expand_Secret_Owl5 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Owl5", true, true);
            Expand_Secret_Owl4 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Owl4", true, true);
            Expand_Secret_Owl3 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Owl3", true, true);
            Expand_Secret_Owl2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Owl2", true, true);
            Expand_Secret_Owl1 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Owl1", true, true);
            Expand_Secret_Oldman10 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Oldman10", true, true);
            Expand_Secret_Oldman9 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Oldman9", true, true);
            Expand_Secret_Oldman8 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Oldman8", true, true);
            Expand_Secret_Oldman7 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Oldman7", true, true);
            Expand_Secret_Oldman6 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Oldman6", true, true);
            Expand_Secret_Oldman5 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Oldman5", true, true);
            Expand_Secret_Oldman4 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Oldman4", true, true);
            Expand_Secret_Oldman3 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Oldman3", true, true);
            Expand_Secret_Oldman2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Oldman2", true, true);
            Expand_Secret_Oldman1 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Secret_Oldman1", true, true);
            Secret_Expand_reto = RoomFactory.BuildFromAssetBundle(AssetBundles, "Secret_Expand_reto", true, true);
            /*Secret_Expand_logo = RoomFactory.BuildFromAssetBundle(AssetBundles, "Secret_Expand_logo", true, true);
            Secret_Expand_logo.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };*/

            Secret_Expand_Achievement_The_Password = RoomFactory.BuildFromAssetBundle(AssetBundles, "Secret_Expand_Achievement_The_Password", true, ExpandSettings.RestoreOldRooms);
            B_Expand_demon_onehand = RoomFactory.BuildFromAssetBundle(AssetBundles, "B_Expand_demon_onehand", true, ExpandSettings.RestoreOldRooms);
            B_Expand_Achievement_Rage_Mode = RoomFactory.BuildFromAssetBundle(AssetBundles, "B_Expand_Achievement_Rage_Mode", true, ExpandSettings.RestoreOldRooms);
            B_Expand_Achievement_Jammed = RoomFactory.BuildFromAssetBundle(AssetBundles, "B_Expand_Achievement_Jammed", true, ExpandSettings.RestoreOldRooms);
            PMO_Expand_Achievement_Demolition_Man = RoomFactory.BuildFromAssetBundle(AssetBundles, "PMO_Expand_Achievement_Demolition_Man", true, ExpandSettings.RestoreOldRooms);
            PMO_Expand_Achievement_Beep = RoomFactory.BuildFromAssetBundle(AssetBundles, "PMO_Expand_Achievement_Beep", true, ExpandSettings.RestoreOldRooms);
            PMHFOA_Expand_Achievement_Gun_Game = RoomFactory.BuildFromAssetBundle(AssetBundles, "PMHFOA_Expand_Achievement_Gun_Game", true, ExpandSettings.RestoreOldRooms);
            PMH_Expand_Kyle = RoomFactory.BuildFromAssetBundle(AssetBundles, "PMH_Expand_Kyle", true, ExpandSettings.RestoreOldRooms);
            PMH_Expand_Achievement_ReArmed = RoomFactory.BuildFromAssetBundle(AssetBundles, "PMH_Expand_Achievement_ReArmed", true, ExpandSettings.RestoreOldRooms);
            PMH_Expand_Achievement_Dat_Plat = RoomFactory.BuildFromAssetBundle(AssetBundles, "PMH_Expand_Achievement_Dat_Plat", true, ExpandSettings.RestoreOldRooms);
            PM_Expand_bubblebuddy = RoomFactory.BuildFromAssetBundle(AssetBundles, "PM_Expand_bubblebuddy", true, ExpandSettings.RestoreOldRooms);
            PM_Expand_Achievement_Squad_Captain = RoomFactory.BuildFromAssetBundle(AssetBundles, "PM_Expand_Achievement_Squad_Captain", true, ExpandSettings.RestoreOldRooms);
            PM_Expand_Achievement_Reverence_for_the_Dead = RoomFactory.BuildFromAssetBundle(AssetBundles, "PM_Expand_Achievement_Reverence_for_the_Dead", true, ExpandSettings.RestoreOldRooms);
            PM_Expand_Achievement_Patron = RoomFactory.BuildFromAssetBundle(AssetBundles, "PM_Expand_Achievement_Patron", true, ExpandSettings.RestoreOldRooms);
            PM_Expand_Achievement_Castle_Crasher = RoomFactory.BuildFromAssetBundle(AssetBundles, "PM_Expand_Achievement_Castle_Crasher", true, ExpandSettings.RestoreOldRooms);
            PM_Expand_Achievement_Case_Closed = RoomFactory.BuildFromAssetBundle(AssetBundles, "PM_Expand_Achievement_Case_Closed", true, ExpandSettings.RestoreOldRooms);
            P_Expand_MasterRound2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "P_Expand_MasterRound2", true, ExpandSettings.RestoreOldRooms);
            P_Expand_beholdter = RoomFactory.BuildFromAssetBundle(AssetBundles, "P_Expand_beholdter", true, ExpandSettings.RestoreOldRooms);
            O_Expand_Achievement_Sworn_Gun = RoomFactory.BuildFromAssetBundle(AssetBundles, "O_Expand_Achievement_Sworn_Gun", true, ExpandSettings.RestoreOldRooms);
            O_Expand_Achievement_Pit_Lord = RoomFactory.BuildFromAssetBundle(AssetBundles, "O_Expand_Achievement_Pit_Lord", true, ExpandSettings.RestoreOldRooms);
            MHF_Expand_Achievement_Great_Hall = RoomFactory.BuildFromAssetBundle(AssetBundles, "MHF_Expand_Achievement_Great_Hall", true, ExpandSettings.RestoreOldRooms);
            MH_Expand_TheSellCreep = RoomFactory.BuildFromAssetBundle(AssetBundles, "MH_Expand_TheSellCreep", true, ExpandSettings.RestoreOldRooms);
            MH_Expand_Achievement_Trickshot = RoomFactory.BuildFromAssetBundle(AssetBundles, "MH_Expand_Achievement_Trickshot", true, ExpandSettings.RestoreOldRooms);
            MH_Expand_Achievement_Lead_God = RoomFactory.BuildFromAssetBundle(AssetBundles, "MH_Expand_Achievement_Lead_God", true, ExpandSettings.RestoreOldRooms);
            MH_Expand_Achievement_Going_Down = RoomFactory.BuildFromAssetBundle(AssetBundles, "MH_Expand_Achievement_Going_Down", true, ExpandSettings.RestoreOldRooms);
            MH_Expand_Achievement_Challenger = RoomFactory.BuildFromAssetBundle(AssetBundles, "MH_Expand_Achievement_Challenger", true, ExpandSettings.RestoreOldRooms);
            MFA_Expand_bigshotgun = RoomFactory.BuildFromAssetBundle(AssetBundles, "MFA_Expand_bigshotgun", true, ExpandSettings.RestoreOldRooms);
            MA_Expand_Achievement_Dungeon_Diver = RoomFactory.BuildFromAssetBundle(AssetBundles, "MA_Expand_Achievement_Dungeon_Diver", true, ExpandSettings.RestoreOldRooms);
            MA_Expand_Achievement_Double_Jeopardy = RoomFactory.BuildFromAssetBundle(AssetBundles, "MA_Expand_Achievement_Double_Jeopardy", true, ExpandSettings.RestoreOldRooms);
            M_Expand_Retrashed = RoomFactory.BuildFromAssetBundle(AssetBundles, "M_Expand_Retrashed", true, ExpandSettings.RestoreOldRooms);
            M_Expand_ratkey = RoomFactory.BuildFromAssetBundle(AssetBundles, "M_Expand_ratkey", true, ExpandSettings.RestoreOldRooms);
            M_Expand_MasterRound3 = RoomFactory.BuildFromAssetBundle(AssetBundles, "M_Expand_MasterRound3", true, ExpandSettings.RestoreOldRooms);
            M_Expand_ArcaneGunpowder = RoomFactory.BuildFromAssetBundle(AssetBundles, "M_Expand_ArcaneGunpowder", true, ExpandSettings.RestoreOldRooms);
            M_Expand_AK = RoomFactory.BuildFromAssetBundle(AssetBundles, "M_Expand_AK", true, ExpandSettings.RestoreOldRooms);
            M_Expand_Achievement_Rider = RoomFactory.BuildFromAssetBundle(AssetBundles, "M_Expand_Achievement_Rider", true, ExpandSettings.RestoreOldRooms);
            M_Expand_Achievement_Resourceful = RoomFactory.BuildFromAssetBundle(AssetBundles, "M_Expand_Achievement_Resourceful", true, ExpandSettings.RestoreOldRooms);
            M_Expand_Achievement_Mine_Master = RoomFactory.BuildFromAssetBundle(AssetBundles, "M_Expand_Achievement_Mine_Master", true, ExpandSettings.RestoreOldRooms);
            KPMHFAO_Expand_Achievement_Woodsie_Lord = RoomFactory.BuildFromAssetBundle(AssetBundles, "KPMHFAO_Expand_Achievement_Woodsie_Lord", true, ExpandSettings.RestoreOldRooms);
            KPMHFAO_Expand_Achievement_Wingman = RoomFactory.BuildFromAssetBundle(AssetBundles, "KPMHFAO_Expand_Achievement_Wingman", true, ExpandSettings.RestoreOldRooms);
            KPM_Expand_Achievement_Last_Stop = RoomFactory.BuildFromAssetBundle(AssetBundles, "KPM_Expand_Achievement_Last_Stop", true, ExpandSettings.RestoreOldRooms);
            KP_Expand_hegemony = RoomFactory.BuildFromAssetBundle(AssetBundles, "KP_Expand_hegemony", true, ExpandSettings.RestoreOldRooms);
            KP_Expand_Achievement_Not_Just_A_Box = RoomFactory.BuildFromAssetBundle(AssetBundles, "KP_Expand_Achievement_Not_Just_A_Box", true, ExpandSettings.RestoreOldRooms);
            KP_Expand_Achievement_Historian = RoomFactory.BuildFromAssetBundle(AssetBundles, "KP_Expand_Achievement_Historian", true, ExpandSettings.RestoreOldRooms);
            K_Expand_Prime_Primer = RoomFactory.BuildFromAssetBundle(AssetBundles, "K_Expand_Prime_Primer", true, ExpandSettings.RestoreOldRooms);
            K_Expand_MasterRound1 = RoomFactory.BuildFromAssetBundle(AssetBundles, "K_Expand_MasterRound1", true, ExpandSettings.RestoreOldRooms);
            K_Expand_bonk = RoomFactory.BuildFromAssetBundle(AssetBundles, "K_Expand_bonk", true, ExpandSettings.RestoreOldRooms);
            K_Expand_BigBird = RoomFactory.BuildFromAssetBundle(AssetBundles, "K_Expand_BigBird", true, ExpandSettings.RestoreOldRooms);
            K_Expand_Achievement_Money_Pit = RoomFactory.BuildFromAssetBundle(AssetBundles, "K_Expand_Achievement_Money_Pit", true, ExpandSettings.RestoreOldRooms);
            K_Expand_Achievement_Hedge_Slinger = RoomFactory.BuildFromAssetBundle(AssetBundles, "K_Expand_Achievement_Hedge_Slinger", true, ExpandSettings.RestoreOldRooms);
            K_Expand_Achievement_Gungeon_Acolyte = RoomFactory.BuildFromAssetBundle(AssetBundles, "K_Expand_Achievement_Gungeon_Acolyte", true, ExpandSettings.RestoreOldRooms);
            K_Expand_Achievement_Biggest_Wallet = RoomFactory.BuildFromAssetBundle(AssetBundles, "K_Expand_Achievement_Biggest_Wallet", true, ExpandSettings.RestoreOldRooms);
            HFAB_Expand_Achievement_Forger = RoomFactory.BuildFromAssetBundle(AssetBundles, "HFAB_Expand_Achievement_Forger", true, ExpandSettings.RestoreOldRooms);
            HF_Expand_Achievement_Weird_Tale = RoomFactory.BuildFromAssetBundle(AssetBundles, "HF_Expand_Achievement_Weird_Tale", true, ExpandSettings.RestoreOldRooms);
            HF_Expand_Achievement_Cartographers_Assistant = RoomFactory.BuildFromAssetBundle(AssetBundles, "HF_Expand_Achievement_Cartographers_Assistant", true, ExpandSettings.RestoreOldRooms);
            HB_Expand_Achievement_Hollowed_Out = RoomFactory.BuildFromAssetBundle(AssetBundles, "HB_Expand_Achievement_Hollowed_Out", true, ExpandSettings.RestoreOldRooms);
            H_Expand_Planar_Lead = RoomFactory.BuildFromAssetBundle(AssetBundles, "H_Expand_Planar_Lead", true, ExpandSettings.RestoreOldRooms);
            H_Expand_MasterRound4 = RoomFactory.BuildFromAssetBundle(AssetBundles, "H_Expand_MasterRound4", true, ExpandSettings.RestoreOldRooms);
            H_Expand_Highpriest = RoomFactory.BuildFromAssetBundle(AssetBundles, "H_Expand_Highpriest", true, ExpandSettings.RestoreOldRooms);
            H_Expand_Achievement_SledgeDog = RoomFactory.BuildFromAssetBundle(AssetBundles, "H_Expand_Achievement_SledgeDog", true, ExpandSettings.RestoreOldRooms);
            H_Expand_Achievement_Exorcist = RoomFactory.BuildFromAssetBundle(AssetBundles, "H_Expand_Achievement_Exorcist", true, ExpandSettings.RestoreOldRooms);
            FB_Expand_Achievement_Time_Paradox = RoomFactory.BuildFromAssetBundle(AssetBundles, "FB_Expand_Achievement_Time_Paradox", true, ExpandSettings.RestoreOldRooms);
            FB_Expand_Achievement_Lion_Leap = RoomFactory.BuildFromAssetBundle(AssetBundles, "FB_Expand_Achievement_Lion_Leap", true, ExpandSettings.RestoreOldRooms);
            FB_Expand_Achievement_Gunsmith = RoomFactory.BuildFromAssetBundle(AssetBundles, "FB_Expand_Achievement_Gunsmith", true, ExpandSettings.RestoreOldRooms);
            FB_Expand_Achievement_Gungeon_Master = RoomFactory.BuildFromAssetBundle(AssetBundles, "FB_Expand_Achievement_Gungeon_Master", true, ExpandSettings.RestoreOldRooms);
            FB_Expand_Achievement_Day_Ruiner = RoomFactory.BuildFromAssetBundle(AssetBundles, "FB_Expand_Achievement_Day_Ruiner", true, ExpandSettings.RestoreOldRooms);
            F_Expand_Obsidian_Shell_Casing = RoomFactory.BuildFromAssetBundle(AssetBundles, "F_Expand_Obsidian_Shell_Casing", true, ExpandSettings.RestoreOldRooms);
            F_Expand_MasterRound5 = RoomFactory.BuildFromAssetBundle(AssetBundles, "F_Expand_MasterRound5", true, ExpandSettings.RestoreOldRooms);
            F_Expand_dragun = RoomFactory.BuildFromAssetBundle(AssetBundles, "F_Expand_dragun", true, ExpandSettings.RestoreOldRooms);
            F_Expand_Achievement_Terminated = RoomFactory.BuildFromAssetBundle(AssetBundles, "F_Expand_Achievement_Terminated", true, ExpandSettings.RestoreOldRooms);
            F_Expand_Achievement_Slayer = RoomFactory.BuildFromAssetBundle(AssetBundles, "F_Expand_Achievement_Slayer", true, ExpandSettings.RestoreOldRooms);
            F_Expand_Achievement_I_Knew_Someone_Would_Do_It = RoomFactory.BuildFromAssetBundle(AssetBundles, "F_Expand_Achievement_I_Knew_Someone_Would_Do_It", true, ExpandSettings.RestoreOldRooms);
            F_Expand_Achievement_Advanced_Slayer = RoomFactory.BuildFromAssetBundle(AssetBundles, "F_Expand_Achievement_Advanced_Slayer", true, ExpandSettings.RestoreOldRooms);
            Expand_Proper_Regular1 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular1", true, true);
            Expand_Proper_Regular2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular2", true, true);
            Expand_Proper_Regular3 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular3", true, true);
            Expand_Proper_Regular4 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular4", true, true);
            Expand_Proper_Regular5 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular5", true, true);
            Expand_Proper_Regular6 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular6", true, true);
            Expand_Proper_Regular7 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular7", true, true);
            Expand_Proper_Regular8 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular8", true, true);
            Expand_Proper_Regular9 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular9", true, true);
            Expand_Proper_Regular10 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular10", true, true);
            Expand_Proper_Regular11 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular11", true, true);
            Expand_Proper_Regular12 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular12", true, true);
            Expand_Proper_Regular13 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular13", true, true);
            Expand_Proper_Regular14 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular14", true, true);
            Expand_Proper_Regular15 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular15", true, true);
            Expand_Proper_Regular16 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular16", true, true);
            Expand_Proper_Regular17 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular17", true, true);
            Expand_Proper_Regular18 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular18", true, true);
            Expand_Proper_Regular19 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular19", true, true);
            Expand_Proper_Regular20 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular20", true, true);
            Expand_Proper_Regular21 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular21", true, true);
            Expand_Proper_Regular22 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular22", true, true);
            Expand_Proper_Regular23 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular23", true, true);
            Expand_Proper_Regular24 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular24", true, true);
            Expand_Proper_Regular25 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular25", true, true);
            Expand_Proper_Regular26 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular26", true, true);
            Expand_Proper_Regular27 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular27", true, true);
            Expand_Proper_Regular28 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular28", true, true);
            Expand_Proper_Regular29 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular29", true, true);
            Expand_Proper_Regular31 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular31", true, true);
            Expand_Proper_Regular32 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular32", true, true);
            Expand_Proper_Regular33 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular33", true, true);
            Expand_Proper_Regular34 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular34", true, true);
            Expand_Proper_Regular35 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular35", true, true);
            Expand_Proper_Regular36 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular36", true, true);
            Expand_Proper_Regular37 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular37", true, true);
            Expand_Proper_Regular38 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular38", true, true);
            Expand_Proper_Regular39 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular39", true, true);
            Expand_Proper_Regular40 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular40", true, true);
            Expand_Proper_Regular41 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular41", true, true);
            Expand_Proper_Regular42 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular42", true, true);
            Expand_Proper_Regular43 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular43", true, true);
            Expand_Proper_Regular44 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular44", true, true);
            Expand_Proper_Regular45 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular45", true, true);
            Expand_Proper_Regular46 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular46", true, true);
            Expand_Proper_Regular47 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular47", true, true);
            Expand_Proper_Regular48 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular48", true, true);
            Expand_Proper_Regular49 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular49", true, true);
            Expand_Proper_Regular50 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Regular50", true, true);
            Expand_Proper_Speshul_Regular30 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Proper_Speshul_Regular30", true, true);
            Expand_Mines_Regular1 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular1", true, true);
            Expand_Mines_Regular2 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular2", true, true);
            Expand_Mines_Regular3 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular3", true, true);
            Expand_Mines_Regular4 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular4", true, true);
            Expand_Mines_Regular5 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular5", true, true);
            Expand_Mines_Regular6 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular6", true, true);
            Expand_Mines_Regular7 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular7", true, true);
            Expand_Mines_Regular8 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular8", true, true);
            Expand_Mines_Regular9 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular9", true, true);
            Expand_Mines_Regular10 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular10", true, true);
            Expand_Mines_Regular11 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular11", true, true);
            Expand_Mines_Regular12 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular12", true, true);
            Expand_Mines_Regular13 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular13", true, true);
            Expand_Mines_Regular14 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular14", true, true);
            Expand_Mines_Regular15 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular15", true, true);
            Expand_Mines_Regular16 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular16", true, true);
            Expand_Mines_Regular17 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular17", true, true);
            Expand_Mines_Regular18 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular18", true, true);
            Expand_Mines_Regular19 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular19", true, true);
            Expand_Mines_Regular20 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular20", true, true);
            Expand_Mines_Regular21 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular21", true, true);
            Expand_Mines_Regular22 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular22", true, true);
            Expand_Mines_Regular23 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular23", true, true);
            Expand_Mines_Regular24 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular24", true, true);
            Expand_Mines_Regular25 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular25", true, true);
            Expand_Mines_Regular26 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular26", true, true);
            Expand_Mines_Regular27 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular27", true, true);
            Expand_Mines_Regular28 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular28", true, true);
            Expand_Mines_Regular29 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular29", true, true);
            Expand_Mines_Regular30 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular30", true, true);
            Expand_Mines_Regular31 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular31", true, true);
            Expand_Mines_Regular32 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular32", true, true);
            Expand_Mines_Regular33 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular33", true, true);
            Expand_Mines_Regular34 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular34", true, true);
            Expand_Mines_Regular35 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular35", true, true);
            Expand_Mines_Regular36 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular36", true, true);
            Expand_Mines_Regular37 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular37", true, true);
            Expand_Mines_Regular38 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular38", true, true);
            Expand_Mines_Regular39 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular39", true, true);
            Expand_Mines_Regular40 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular40", true, true);
            Expand_Mines_Regular41 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular41", true, true);
            Expand_Mines_Regular42 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular42", true, true);
            Expand_Mines_Regular43 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular43", true, true);
            Expand_Mines_Regular44 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular44", true, true);
            Expand_Mines_Regular45 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular45", true, true);
            Expand_Mines_Regular46 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular46", true, true);
            Expand_Mines_Regular47 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular47", true, true);
            Expand_Mines_Regular48 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular48", true, true);
            Expand_Mines_Regular49 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular49", true, true);
            Expand_Mines_Regular50 = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Mines_Regular50", true, true);
            Expand_Keep_Spiral = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_Keep_Spiral", true, ExpandSettings.RestoreOldRooms);

            // Trap_Expand_Turtlemelon = RoomFactory.BuildFromAssetBundle(AssetBundles, "Trap_Expand_Turtlemelon", true);

            Expand_Apache_FieldOfSaws.name = "Apache Field of Saws";
            Expand_Apache_FieldOfSaws.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Apache_FieldOfSaws.GUID = Guid.NewGuid().ToString();
            Expand_Apache_FieldOfSaws.PreventMirroring = false;
            Expand_Apache_FieldOfSaws.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Apache_FieldOfSaws.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Apache_FieldOfSaws.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Apache_FieldOfSaws.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Apache_FieldOfSaws.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Apache_FieldOfSaws.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Apache_FieldOfSaws.pits = new List<PrototypeRoomPitEntry>();
            Expand_Apache_FieldOfSaws.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Apache_FieldOfSaws.placedObjectPositions = new List<Vector2>();
            Expand_Apache_FieldOfSaws.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Apache_FieldOfSaws.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Apache_FieldOfSaws.overriddenTilesets = 0;
            Expand_Apache_FieldOfSaws.prerequisites = new List<DungeonPrerequisite>();
            Expand_Apache_FieldOfSaws.InvalidInCoop = false;
            Expand_Apache_FieldOfSaws.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Apache_FieldOfSaws.preventAddedDecoLayering = false;
            Expand_Apache_FieldOfSaws.precludeAllTilemapDrawing = false;
            Expand_Apache_FieldOfSaws.drawPrecludedCeilingTiles = false;
            Expand_Apache_FieldOfSaws.preventBorders = false;
            Expand_Apache_FieldOfSaws.preventFacewallAO = false;
            Expand_Apache_FieldOfSaws.usesCustomAmbientLight = false;
            Expand_Apache_FieldOfSaws.customAmbientLight = Color.white;
            Expand_Apache_FieldOfSaws.ForceAllowDuplicates = false;
            Expand_Apache_FieldOfSaws.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Apache_FieldOfSaws.IsLostWoodsRoom = false;
            Expand_Apache_FieldOfSaws.UseCustomMusic = false;
            Expand_Apache_FieldOfSaws.UseCustomMusicState = false;
            Expand_Apache_FieldOfSaws.CustomMusicEvent = string.Empty;
            Expand_Apache_FieldOfSaws.UseCustomMusicSwitch = false;
            Expand_Apache_FieldOfSaws.CustomMusicSwitch = string.Empty;
            Expand_Apache_FieldOfSaws.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Apache_FieldOfSaws.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Apache_FieldOfSaws.Width = 32;
            Expand_Apache_FieldOfSaws.Height = 32;
            RoomBuilder.AddExitToRoom(Expand_Apache_FieldOfSaws, new Vector2(0, 16), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Apache_FieldOfSaws, new Vector2(33, 16), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Apache_FieldOfSaws, new Vector2(16, 33), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Apache_FieldOfSaws, new Vector2(16, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(5, 5), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(11, 5), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(17, 5), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(23, 5), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(5, 11), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(11, 11), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(17, 11), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(23, 11), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(5, 17), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(11, 17), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(17, 17), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(23, 17), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(5, 23), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(11, 23), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(17, 23), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(23, 23), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXSawBladeTrap_4x4Zone, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(9, 21), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.AddObjectToRoom(Expand_Apache_FieldOfSaws, new Vector2(21, 9), EnemyBehaviourGuid: "01972dee89fc4404a5c408d50007dad5"); // bullet_kin
            RoomBuilder.GenerateRoomLayout(Expand_Apache_FieldOfSaws, "Expand_Apache_FieldOfSaws_Layout");


            Expand_Apache_TheCrushZone.name = "Apache The Crush Zone";
            Expand_Apache_TheCrushZone.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Apache_TheCrushZone.GUID = Guid.NewGuid().ToString();
            Expand_Apache_TheCrushZone.PreventMirroring = false;
            Expand_Apache_TheCrushZone.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Apache_TheCrushZone.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Apache_TheCrushZone.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Apache_TheCrushZone.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Apache_TheCrushZone.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Apache_TheCrushZone.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Apache_TheCrushZone.pits = new List<PrototypeRoomPitEntry>();
            Expand_Apache_TheCrushZone.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Apache_TheCrushZone.placedObjectPositions = new List<Vector2>();
            Expand_Apache_TheCrushZone.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Apache_TheCrushZone.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Apache_TheCrushZone.overriddenTilesets = 0;
            Expand_Apache_TheCrushZone.prerequisites = new List<DungeonPrerequisite>();
            Expand_Apache_TheCrushZone.InvalidInCoop = false;
            Expand_Apache_TheCrushZone.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Apache_TheCrushZone.preventAddedDecoLayering = false;
            Expand_Apache_TheCrushZone.precludeAllTilemapDrawing = false;
            Expand_Apache_TheCrushZone.drawPrecludedCeilingTiles = false;
            Expand_Apache_TheCrushZone.preventBorders = false;
            Expand_Apache_TheCrushZone.preventFacewallAO = false;
            Expand_Apache_TheCrushZone.usesCustomAmbientLight = false;
            Expand_Apache_TheCrushZone.customAmbientLight = Color.white;
            Expand_Apache_TheCrushZone.ForceAllowDuplicates = false;
            Expand_Apache_TheCrushZone.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Apache_TheCrushZone.IsLostWoodsRoom = false;
            Expand_Apache_TheCrushZone.UseCustomMusic = false;
            Expand_Apache_TheCrushZone.UseCustomMusicState = false;
            Expand_Apache_TheCrushZone.CustomMusicEvent = string.Empty;
            Expand_Apache_TheCrushZone.UseCustomMusicSwitch = false;
            Expand_Apache_TheCrushZone.CustomMusicSwitch = string.Empty;
            Expand_Apache_TheCrushZone.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Apache_TheCrushZone.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Apache_TheCrushZone.Width = 37;
            Expand_Apache_TheCrushZone.Height = 37;
            RoomBuilder.AddExitToRoom(Expand_Apache_TheCrushZone, new Vector2(0, 5), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Apache_TheCrushZone, new Vector2(38, 32), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Apache_TheCrushZone, new Vector2(0, 32), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Apache_TheCrushZone, new Vector2(38, 5), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Apache_TheCrushZone, new Vector2(5, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Apache_TheCrushZone, new Vector2(32, 38), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_Apache_TheCrushZone, new Vector2(32, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Apache_TheCrushZone, new Vector2(5, 38), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 8), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 17), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 26), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 8), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 17), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 26), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 8), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 17), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 26), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 8), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 17), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 26), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Horizontal.GetComponent<ForgeCrushDoorController>(), yOffset: 2);
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(8, 4), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(8, 13), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(8, 22), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(8, 31), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(17, 4), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(17, 13), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(17, 22), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(17, 31), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(26, 4), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(26, 13), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(26, 22), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(26, 31), NonEnemyBehaviour: ExpandObjectDatabase.CrushDoor_Vertical.GetComponent<ForgeCrushDoorController>());
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(2, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(2, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(33, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(33, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(2, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 31), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(2, 33), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 33), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(33, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 31), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 33), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(33, 33), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 2), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 4), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 6), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 31), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 31), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 31), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 33), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 33), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 33), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 29), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 31), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 31), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 31), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 33), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 33), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 33), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(2, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(2, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(2, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(33, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(33, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(33, 15), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(2, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(2, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(2, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(4, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(6, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(11, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(13, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(15, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(20, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(22, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(33, 20), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(33, 22), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(29, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(31, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(33, 24), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(12, 13), EnemyBehaviourGuid: "e61cab252cfb435db9172adc96ded75f"); // poisbulon
            RoomBuilder.AddObjectToRoom(Expand_Apache_TheCrushZone, new Vector2(24, 22), EnemyBehaviourGuid: "0239c0680f9f467dbe5c4aab7dd1eca6"); // blobulon
            RoomBuilder.GenerateRoomLayout(Expand_Apache_TheCrushZone, "Expand_Apache_TheCrushZone_Layout");

            Expand_Apache_SpikeAndPits.name = "Apache Spikes and Pits";
            Expand_Apache_SpikeAndPits.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Apache_SpikeAndPits.GUID = Guid.NewGuid().ToString();
            Expand_Apache_SpikeAndPits.PreventMirroring = false;
            Expand_Apache_SpikeAndPits.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Apache_SpikeAndPits.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Apache_SpikeAndPits.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Apache_SpikeAndPits.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Apache_SpikeAndPits.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Apache_SpikeAndPits.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Apache_SpikeAndPits.pits = new List<PrototypeRoomPitEntry>();
            Expand_Apache_SpikeAndPits.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Apache_SpikeAndPits.placedObjectPositions = new List<Vector2>();
            Expand_Apache_SpikeAndPits.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Apache_SpikeAndPits.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Apache_SpikeAndPits.overriddenTilesets = 0;
            Expand_Apache_SpikeAndPits.prerequisites = new List<DungeonPrerequisite>();
            Expand_Apache_SpikeAndPits.InvalidInCoop = false;
            Expand_Apache_SpikeAndPits.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Apache_SpikeAndPits.preventAddedDecoLayering = false;
            Expand_Apache_SpikeAndPits.precludeAllTilemapDrawing = false;
            Expand_Apache_SpikeAndPits.drawPrecludedCeilingTiles = false;
            Expand_Apache_SpikeAndPits.preventBorders = false;
            Expand_Apache_SpikeAndPits.preventFacewallAO = false;
            Expand_Apache_SpikeAndPits.usesCustomAmbientLight = false;
            Expand_Apache_SpikeAndPits.customAmbientLight = Color.white;
            Expand_Apache_SpikeAndPits.ForceAllowDuplicates = false;
            Expand_Apache_SpikeAndPits.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Apache_SpikeAndPits.IsLostWoodsRoom = false;
            Expand_Apache_SpikeAndPits.UseCustomMusic = false;
            Expand_Apache_SpikeAndPits.UseCustomMusicState = false;
            Expand_Apache_SpikeAndPits.CustomMusicEvent = string.Empty;
            Expand_Apache_SpikeAndPits.UseCustomMusicSwitch = false;
            Expand_Apache_SpikeAndPits.CustomMusicSwitch = string.Empty;
            Expand_Apache_SpikeAndPits.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Apache_SpikeAndPits.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Apache_SpikeAndPits.Width = 26;
            Expand_Apache_SpikeAndPits.Height = 26;
            RoomBuilder.AddExitToRoom(Expand_Apache_SpikeAndPits, new Vector2(0, 13), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Apache_SpikeAndPits, new Vector2(27, 13), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Apache_SpikeAndPits, new Vector2(13, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Apache_SpikeAndPits, new Vector2(13, 27), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(5, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(7, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(5, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(7, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(11, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(13, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(11, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(13, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(17, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(19, 5), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(17, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(19, 7), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(5, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(7, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(5, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(7, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(11, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(13, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(11, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(13, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(17, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(19, 11), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(17, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(19, 13), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(5, 17), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(7, 17), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(5, 19), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(7, 19), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(11, 17), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(13, 17), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(11, 19), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(13, 19), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(17, 17), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(19, 17), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(17, 19), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(19, 19), ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.SpikeTrap, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_Apache_SpikeAndPits, new Vector2(15, 10), EnemyBehaviourGuid: "4db03291a12144d69fe940d5a01de376"); // hollowpoint
            RoomBuilder.GenerateRoomLayout(Expand_Apache_SpikeAndPits, "Expand_Apache_SpikeAndPits_Layout");

            Expand_Apache_PitTraps.name = "Apache Pit Traps";
            Expand_Apache_PitTraps.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Apache_PitTraps.GUID = Guid.NewGuid().ToString();
            Expand_Apache_PitTraps.PreventMirroring = false;
            Expand_Apache_PitTraps.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Apache_PitTraps.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Apache_PitTraps.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP;
            Expand_Apache_PitTraps.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Apache_PitTraps.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Apache_PitTraps.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Apache_PitTraps.pits = new List<PrototypeRoomPitEntry>();
            Expand_Apache_PitTraps.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Apache_PitTraps.placedObjectPositions = new List<Vector2>();
            Expand_Apache_PitTraps.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Apache_PitTraps.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Apache_PitTraps.overriddenTilesets = 0;
            Expand_Apache_PitTraps.prerequisites = new List<DungeonPrerequisite>();
            Expand_Apache_PitTraps.InvalidInCoop = false;
            Expand_Apache_PitTraps.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Apache_PitTraps.preventAddedDecoLayering = false;
            Expand_Apache_PitTraps.precludeAllTilemapDrawing = false;
            Expand_Apache_PitTraps.drawPrecludedCeilingTiles = false;
            Expand_Apache_PitTraps.preventBorders = false;
            Expand_Apache_PitTraps.preventFacewallAO = false;
            Expand_Apache_PitTraps.usesCustomAmbientLight = false;
            Expand_Apache_PitTraps.customAmbientLight = Color.white;
            Expand_Apache_PitTraps.ForceAllowDuplicates = false;
            Expand_Apache_PitTraps.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Apache_PitTraps.IsLostWoodsRoom = false;
            Expand_Apache_PitTraps.UseCustomMusic = false;
            Expand_Apache_PitTraps.UseCustomMusicState = false;
            Expand_Apache_PitTraps.CustomMusicEvent = string.Empty;
            Expand_Apache_PitTraps.UseCustomMusicSwitch = false;
            Expand_Apache_PitTraps.CustomMusicSwitch = string.Empty;
            Expand_Apache_PitTraps.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Apache_PitTraps.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Apache_PitTraps.Width = 24;
            Expand_Apache_PitTraps.Height = 24;
            RoomBuilder.AddExitToRoom(Expand_Apache_PitTraps, new Vector2(0, 12), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_Apache_PitTraps, new Vector2(25, 12), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_Apache_PitTraps, new Vector2(12, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_Apache_PitTraps, new Vector2(12, 25), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(2, 2), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(4, 2), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 2), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(8, 2), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(10, 2), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(12, 2), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(14, 2), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(16, 2), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(18, 2), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(20, 2), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(2, 4), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(4, 4), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 4), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(8, 4), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(10, 4), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(12, 4), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(14, 4), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(16, 4), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(18, 4), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(20, 4), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(2, 6), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(4, 6), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 6), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(8, 6), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(10, 6), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(12, 6), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(14, 6), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(16, 6), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(18, 6), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(20, 6), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(2, 8), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(4, 8), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 8), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(8, 8), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(10, 8), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(12, 8), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(14, 8), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(16, 8), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(18, 8), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(20, 8), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(2, 10), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(4, 10), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 10), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(8, 10), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(10, 10), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(12, 10), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(14, 10), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(16, 10), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(18, 10), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(20, 10), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(2, 12), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(4, 12), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 12), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(8, 12), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(10, 12), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(12, 12), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(14, 12), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(16, 12), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(18, 12), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(20, 12), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(2, 14), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(4, 14), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 14), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(8, 14), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(10, 14), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(12, 14), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(14, 14), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(16, 14), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(18, 14), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(20, 14), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(2, 16), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(4, 16), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 16), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(8, 16), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(10, 16), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(12, 16), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(14, 16), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(16, 16), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(18, 16), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(20, 16), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(2, 18), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(4, 18), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 18), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(8, 18), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(10, 18), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(12, 18), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(14, 18), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(16, 18), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(18, 18), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(20, 18), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(2, 20), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(4, 20), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 20), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(8, 20), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(10, 20), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(12, 20), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(14, 20), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(16, 20), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(18, 20), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(20, 20), ExpandObjectDatabase.PitTrap);
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(11, 11), EnemyBehaviourGuid: "d9632631a18849539333a92332895ebd"); // diagonal_det
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 6), EnemyBehaviourGuid: "4db03291a12144d69fe940d5a01de376"); // hollowpoint
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(17, 17), EnemyBehaviourGuid: "4db03291a12144d69fe940d5a01de376"); // hollowpoint
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(6, 17), EnemyBehaviourGuid: "72d2f44431da43b8a3bae7d8a114a46d"); // bulletshark
            RoomBuilder.AddObjectToRoom(Expand_Apache_PitTraps, new Vector2(17, 6), EnemyBehaviourGuid: "72d2f44431da43b8a3bae7d8a114a46d"); // bulletshark
            RoomBuilder.GenerateRoomLayout(Expand_Apache_PitTraps, "Expand_Apache_PitTraps_Layout");



            Expand_Apache_RickRollChest.name = "Apache Rick Roll Chest Room";
            Expand_Apache_RickRollChest.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Apache_RickRollChest.GUID = Guid.NewGuid().ToString();
            Expand_Apache_RickRollChest.PreventMirroring = false;
            Expand_Apache_RickRollChest.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Apache_RickRollChest.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Apache_RickRollChest.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Apache_RickRollChest.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Apache_RickRollChest.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Apache_RickRollChest.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Apache_RickRollChest.pits = new List<PrototypeRoomPitEntry>();
            Expand_Apache_RickRollChest.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Apache_RickRollChest.placedObjectPositions = new List<Vector2>();
            Expand_Apache_RickRollChest.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Apache_RickRollChest.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Apache_RickRollChest.overriddenTilesets = GlobalDungeonData.ValidTilesets.FORGEGEON;
            Expand_Apache_RickRollChest.prerequisites = new List<DungeonPrerequisite>();
            Expand_Apache_RickRollChest.InvalidInCoop = false;
            Expand_Apache_RickRollChest.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Apache_RickRollChest.preventAddedDecoLayering = false;
            Expand_Apache_RickRollChest.precludeAllTilemapDrawing = false;
            Expand_Apache_RickRollChest.drawPrecludedCeilingTiles = false;
            Expand_Apache_RickRollChest.preventBorders = false;
            Expand_Apache_RickRollChest.preventFacewallAO = false;
            Expand_Apache_RickRollChest.usesCustomAmbientLight = false;
            Expand_Apache_RickRollChest.customAmbientLight = Color.white;
            Expand_Apache_RickRollChest.ForceAllowDuplicates = false;
            Expand_Apache_RickRollChest.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Apache_RickRollChest.IsLostWoodsRoom = false;
            Expand_Apache_RickRollChest.UseCustomMusic = false;
            Expand_Apache_RickRollChest.UseCustomMusicState = false;
            Expand_Apache_RickRollChest.CustomMusicEvent = string.Empty;
            Expand_Apache_RickRollChest.UseCustomMusicSwitch = false;
            Expand_Apache_RickRollChest.CustomMusicSwitch = string.Empty;
            Expand_Apache_RickRollChest.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Apache_RickRollChest.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Apache_RickRollChest.Width = 18;
            Expand_Apache_RickRollChest.Height = 13;
            Expand_Apache_RickRollChest.additionalObjectLayers = new List<PrototypeRoomObjectLayer>();
            foreach (PrototypeRoomExit exit in ExpandPrefabs.gungeon_rewardroom_1.exitData.exits) {
                Expand_Apache_RickRollChest.exitData.exits.Add(exit);
            }
            foreach (PrototypeRoomObjectLayer objectLayer in ExpandPrefabs.gungeon_rewardroom_1.additionalObjectLayers) {
                Expand_Apache_RickRollChest.additionalObjectLayers.Add(objectLayer);
            }
            foreach (PrototypePlacedObjectData objectData in ExpandPrefabs.gungeon_rewardroom_1.placedObjects) {
                Expand_Apache_RickRollChest.placedObjects.Add(objectData);
            }
            foreach (Vector2 placedObjectPosition in ExpandPrefabs.gungeon_rewardroom_1.placedObjectPositions) {
                Expand_Apache_RickRollChest.placedObjectPositions.Add(placedObjectPosition);
            }
            Expand_Apache_RickRollChest.additionalObjectLayers.Remove(Expand_Apache_RickRollChest.additionalObjectLayers[1]);
            RoomBuilder.AddObjectToRoom(Expand_Apache_RickRollChest, new Vector2(7, 9), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RickRollChestObject, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.GenerateRoomLayout(Expand_Apache_RickRollChest, "Expand_Apache_RickRollChestRoom_Layout");


            Expand_Apache_SurpriseChest.name = "Apache Suprise Chest Room";
            Expand_Apache_SurpriseChest.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_Apache_SurpriseChest.GUID = Guid.NewGuid().ToString();
            Expand_Apache_SurpriseChest.PreventMirroring = false;
            Expand_Apache_SurpriseChest.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_Apache_SurpriseChest.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_Apache_SurpriseChest.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_Apache_SurpriseChest.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_Apache_SurpriseChest.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_Apache_SurpriseChest.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_Apache_SurpriseChest.pits = new List<PrototypeRoomPitEntry>();
            Expand_Apache_SurpriseChest.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_Apache_SurpriseChest.placedObjectPositions = new List<Vector2>();
            Expand_Apache_SurpriseChest.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_Apache_SurpriseChest.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_Apache_SurpriseChest.overriddenTilesets = GlobalDungeonData.ValidTilesets.FORGEGEON;
            Expand_Apache_SurpriseChest.prerequisites = new List<DungeonPrerequisite>();
            Expand_Apache_SurpriseChest.InvalidInCoop = false;
            Expand_Apache_SurpriseChest.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_Apache_SurpriseChest.preventAddedDecoLayering = false;
            Expand_Apache_SurpriseChest.precludeAllTilemapDrawing = false;
            Expand_Apache_SurpriseChest.drawPrecludedCeilingTiles = false;
            Expand_Apache_SurpriseChest.preventBorders = false;
            Expand_Apache_SurpriseChest.preventFacewallAO = false;
            Expand_Apache_SurpriseChest.usesCustomAmbientLight = false;
            Expand_Apache_SurpriseChest.customAmbientLight = Color.white;
            Expand_Apache_SurpriseChest.ForceAllowDuplicates = false;
            Expand_Apache_SurpriseChest.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_Apache_SurpriseChest.IsLostWoodsRoom = false;
            Expand_Apache_SurpriseChest.UseCustomMusic = false;
            Expand_Apache_SurpriseChest.UseCustomMusicState = false;
            Expand_Apache_SurpriseChest.CustomMusicEvent = string.Empty;
            Expand_Apache_SurpriseChest.UseCustomMusicSwitch = false;
            Expand_Apache_SurpriseChest.CustomMusicSwitch = string.Empty;
            Expand_Apache_SurpriseChest.overrideRoomVisualTypeForSecretRooms = false;
            Expand_Apache_SurpriseChest.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_Apache_SurpriseChest.Width = 18;
            Expand_Apache_SurpriseChest.Height = 13;
            Expand_Apache_SurpriseChest.additionalObjectLayers = new List<PrototypeRoomObjectLayer>();
            foreach (PrototypeRoomExit exit in ExpandPrefabs.gungeon_rewardroom_1.exitData.exits) {
                Expand_Apache_SurpriseChest.exitData.exits.Add(exit);
            }
            foreach (PrototypeRoomObjectLayer objectLayer in ExpandPrefabs.gungeon_rewardroom_1.additionalObjectLayers) {
                Expand_Apache_SurpriseChest.additionalObjectLayers.Add(objectLayer);
            }
            foreach (PrototypePlacedObjectData objectData in ExpandPrefabs.gungeon_rewardroom_1.placedObjects) {
                Expand_Apache_SurpriseChest.placedObjects.Add(objectData);
            }
            foreach (Vector2 placedObjectPosition in ExpandPrefabs.gungeon_rewardroom_1.placedObjectPositions) {
                Expand_Apache_SurpriseChest.placedObjectPositions.Add(placedObjectPosition);
            }
            Expand_Apache_SurpriseChest.additionalObjectLayers.Remove(Expand_Apache_SurpriseChest.additionalObjectLayers[1]);
            RoomBuilder.AddObjectToRoom(Expand_Apache_SurpriseChest, new Vector2(8, 8), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.SurpriseChestObject, useExternalPrefab: true), yOffset: 8);
            RoomBuilder.GenerateRoomLayout(Expand_Apache_SurpriseChest, "Expand_Apache_SurpriseChest_Layout");


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
            SecretExitRoom2.precludeAllTilemapDrawing = true;
            SecretExitRoom2.drawPrecludedCeilingTiles = false;
            SecretExitRoom2.preventBorders = false;
            SecretExitRoom2.preventFacewallAO = false;
            SecretExitRoom2.usesCustomAmbientLight = true;
            SecretExitRoom2.customAmbientLight = new Color(0.875462f, 0.882353f, 0.810986f, 1);
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
            SecretExitRoom2.Width = 32;
            SecretExitRoom2.Height = 58;
            SecretExitRoom2.associatedMinimapIcon = ExpandPrefabs.elevator_maintenance_room.associatedMinimapIcon;
            SecretExitRoom2.overrideRoomVisualType = 0;
            SecretExitRoom2.usesProceduralDecoration = false;
            SecretExitRoom2.allowFloorDecoration = false;
            SecretExitRoom2.usesProceduralLighting = false;
            // RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(13, 9), ExpandPrefabs.ElevatorDeparture);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, Vector2.zero, ExpandPrefabs.SecretElevatorExitTileset);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(23, 6), ExpandUtility.GenerateDungeonPlacable(ExpandSecretDoorPrefabs.EXSecretDoor_Hollow_Unlocked, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(21, 4), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Door_Horizontal_West, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(0, 32), ExpandPrefabs.EXOldWestWarp);
            // RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(16, 2), ExpandPrefabs.WestLight);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(14, 4), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(15, 12), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(13, 8), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(2, 19), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(26, 19), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(18, 22), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(11, 26), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(22, 29), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(21, 34), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(27, 35), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(4, 32), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(16, 30), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(14, 32), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(15, 37), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(15, 39), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(27, 35), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(2, 57), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(3, 39), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(28, 42), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(26, 27), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(2, 24), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(8, 36), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(22, 43), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(9, 45), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(2, 46), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(6, 51), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(22, 52), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(29, 51), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(11, 56), ExpandPrefabs.Cactus_A);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(27, 35), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(19, 57), ExpandPrefabs.Cactus_B);
            RoomBuilder.AddObjectToRoom(SecretExitRoom2, new Vector2(28, 57), ExpandPrefabs.Cactus_B);
            RoomBuilder.GenerateRoomLayout(SecretExitRoom2, "Secret_Elevator_Exit_Layout");


            SecretRatEntranceRoom.name = "Secret Rat MiniElevator Room";
            SecretRatEntranceRoom.QAID = "AA" + UnityEngine.Random.Range(1000, 9999);
            SecretRatEntranceRoom.GUID = Guid.NewGuid().ToString();
            SecretRatEntranceRoom.PreventMirroring = false;
            SecretRatEntranceRoom.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            SecretRatEntranceRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            SecretRatEntranceRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            SecretRatEntranceRoom.associatedMinimapIcon = ExpandSecretDoorPrefabs.EXSecretDoorMinimapIcon;
            SecretRatEntranceRoom.usesProceduralDecoration = true;
            SecretRatEntranceRoom.usesProceduralLighting = true;
            SecretRatEntranceRoom.allowFloorDecoration = false;
            RoomBuilder.AddExitToRoom(SecretRatEntranceRoom, new Vector2(0, 8), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(SecretRatEntranceRoom, new Vector2(17, 8), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(SecretRatEntranceRoom, new Vector2(8, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(SecretRatEntranceRoom, new Vector2(2, 19), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(SecretRatEntranceRoom, new Vector2(14, 19), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(SecretRatEntranceRoom, new Vector2(6, 6), NonEnemyBehaviour: ExpandPrefabs.Teleporter_Gungeon_01.GetComponent<DungeonPlaceableBehaviour>());
            RoomBuilder.AddObjectToRoom(SecretRatEntranceRoom, new Vector2(6, 16), ExpandSecretDoorPrefabs.EXSecretDoor_Hollow);
            RoomBuilder.AddObjectToRoom(SecretRatEntranceRoom, new Vector2(7, 5), EnemyBehaviourGuid: "5861e5a077244905a8c25c2b7b4d6ebb"); // bullet kin cowboy
            RoomBuilder.AddObjectToRoom(SecretRatEntranceRoom, new Vector2(5, 11), EnemyBehaviourGuid: "ddf12a4881eb43cfba04f36dd6377abb"); // shotgun kin cowboy
            RoomBuilder.AddObjectToRoom(SecretRatEntranceRoom, new Vector2(10, 11), EnemyBehaviourGuid: "ddf12a4881eb43cfba04f36dd6377abb"); // shotgun kin cowboy
            RoomBuilder.GenerateRoomLayout(SecretRatEntranceRoom, "Secret_Rat_MiniElevator_Room_Layout");


            Expand_SecretElevatorEntranceRoom.name = "Secret MiniElevator Room";
            Expand_SecretElevatorEntranceRoom.QAID = "AA" + UnityEngine.Random.Range(1000, 9999);
            Expand_SecretElevatorEntranceRoom.GUID = Guid.NewGuid().ToString();
            Expand_SecretElevatorEntranceRoom.PreventMirroring = false;
            Expand_SecretElevatorEntranceRoom.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_SecretElevatorEntranceRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_SecretElevatorEntranceRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            Expand_SecretElevatorEntranceRoom.associatedMinimapIcon = ExpandSecretDoorPrefabs.EXSecretDoorMinimapIcon;
            RoomBuilder.AddExitToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(0, 4), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(11, 4), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(5, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(1, 9), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(9, 9), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_SecretElevatorEntranceRoom, new Vector2(3, 8), ExpandSecretDoorPrefabs.EXSecretDoor);
            RoomBuilder.GenerateRoomLayout(Expand_SecretElevatorEntranceRoom, "Secret_MiniElevator_Room_Layout");

            // This will share same layout as it's entrance version.
            Expand_SecretElevatorDestinationRoom.name = "Destination MiniElevator Room";
            Expand_SecretElevatorDestinationRoom.QAID = "AA" + UnityEngine.Random.Range(1000, 9999);
            Expand_SecretElevatorDestinationRoom.GUID = Guid.NewGuid().ToString();
            Expand_SecretElevatorDestinationRoom.PreventMirroring = false;
            Expand_SecretElevatorDestinationRoom.category = PrototypeDungeonRoom.RoomCategory.CONNECTOR;
            Expand_SecretElevatorDestinationRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_SecretElevatorDestinationRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(0, 4), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(11, 4), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(5, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(1, 9), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(9, 9), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_SecretElevatorDestinationRoom, new Vector2(3, 8), ExpandSecretDoorPrefabs.EXSecretDoor_Unlocked);
            RoomBuilder.GenerateRoomLayout(Expand_SecretElevatorDestinationRoom, "Destination_MiniElevator_Room_Layout");



            Expand_TinySecret.name = "Expand Apache Tiny Secret";
            Expand_TinySecret.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_TinySecret.GUID = Guid.NewGuid().ToString();
            Expand_TinySecret.PreventMirroring = false;
            Expand_TinySecret.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_TinySecret.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_TinySecret.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
            RoomBuilder.AddExitToRoom(Expand_TinySecret, new Vector2(0, 1), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_TinySecret, new Vector2(3, 1), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_TinySecret, new Vector2(1, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_TinySecret, new Vector2(1, 3), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_TinySecret, new Vector2(0, 0), ExpandPrefabs.TinySecretRoomJunkReward, xOffset: 1, yOffset: 10);
            RoomBuilder.AddObjectToRoom(Expand_TinySecret, new Vector2(1, 0), ExpandPrefabs.TinySecretRoomRewards, yOffset: 10);
            RoomBuilder.GenerateBasicRoomLayout(Expand_TinySecret);

            Expand_TinySecretEmpty.name = "Expand Apache Tiny Secret Empty";
            Expand_TinySecretEmpty.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_TinySecretEmpty.GUID = Guid.NewGuid().ToString();
            Expand_TinySecretEmpty.PreventMirroring = false;
            Expand_TinySecretEmpty.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_TinySecretEmpty.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_TinySecretEmpty.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_TinySecretEmpty.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_TinySecretEmpty.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_TinySecretEmpty.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_TinySecretEmpty.pits = new List<PrototypeRoomPitEntry>();
            Expand_TinySecretEmpty.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_TinySecretEmpty.placedObjectPositions = new List<Vector2>();
            Expand_TinySecretEmpty.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_TinySecretEmpty.roomEvents = new List<RoomEventDefinition>(0);
            Expand_TinySecretEmpty.overriddenTilesets = 0;
            Expand_TinySecretEmpty.prerequisites = new List<DungeonPrerequisite>();
            Expand_TinySecretEmpty.InvalidInCoop = false;
            Expand_TinySecretEmpty.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_TinySecretEmpty.preventAddedDecoLayering = false;
            Expand_TinySecretEmpty.precludeAllTilemapDrawing = false;
            Expand_TinySecretEmpty.drawPrecludedCeilingTiles = false;
            Expand_TinySecretEmpty.preventBorders = false;
            Expand_TinySecretEmpty.preventFacewallAO = false;
            Expand_TinySecretEmpty.usesCustomAmbientLight = false;
            Expand_TinySecretEmpty.customAmbientLight = Color.white;
            Expand_TinySecretEmpty.ForceAllowDuplicates = false;
            Expand_TinySecretEmpty.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_TinySecretEmpty.IsLostWoodsRoom = false;
            Expand_TinySecretEmpty.UseCustomMusic = false;
            Expand_TinySecretEmpty.UseCustomMusicState = false;
            Expand_TinySecretEmpty.CustomMusicEvent = string.Empty;
            Expand_TinySecretEmpty.UseCustomMusicSwitch = false;
            Expand_TinySecretEmpty.CustomMusicSwitch = string.Empty;
            Expand_TinySecretEmpty.overrideRoomVisualTypeForSecretRooms = false;
            Expand_TinySecretEmpty.rewardChestSpawnPosition = new IntVector2(10, 12);
            Expand_TinySecretEmpty.overrideRoomVisualType = -1;
            Expand_TinySecretEmpty.Width = 2;
            Expand_TinySecretEmpty.Height = 2;
            Expand_TinySecretEmpty.allowFloorDecoration = false;
            Expand_TinySecretEmpty.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_TinySecretEmpty, new Vector2(0, 1), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_TinySecretEmpty, new Vector2(3, 1), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_TinySecretEmpty, new Vector2(1, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_TinySecretEmpty, new Vector2(1, 3), DungeonData.Direction.NORTH);
            RoomBuilder.GenerateBasicRoomLayout(Expand_TinySecretEmpty);



            Expand_GlitchedSecret.name = "Expand Apache Corrupted Secret";
            Expand_GlitchedSecret.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_GlitchedSecret.GUID = Guid.NewGuid().ToString();
            Expand_GlitchedSecret.PreventMirroring = false;
            Expand_GlitchedSecret.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_GlitchedSecret.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_GlitchedSecret.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
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
                            placeableContents = ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.NPCMonsterManuel, useExternalPrefab: true),
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
                            placeableContents = ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.NPCVampire, useExternalPrefab: true),
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
                            placeableContents = ExpandUtility.GenerateDungeonPlacable(ExpandObjectDatabase.NPCMonsterManuel, useExternalPrefab: true, spawnChance: 0.4f),
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
                            placeableContents = ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.CorruptionBombRewardPedestal, useExternalPrefab: true),
                            contentsBasePosition = new Vector2(13, 13),
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
                        new Vector2(13, 13)
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
            RoomBuilder.AddExitToRoom(Expand_GlitchedSecret, new Vector2(0, 8), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_GlitchedSecret, new Vector2(17, 8), DungeonData.Direction.EAST);
            RoomBuilder.AddExitToRoom(Expand_GlitchedSecret, new Vector2(8, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_GlitchedSecret, new Vector2(8, 17), DungeonData.Direction.NORTH);
            RoomBuilder.AddObjectToRoom(Expand_GlitchedSecret, new Vector2(8, 8), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RoomCorruptionAmbience, useExternalPrefab: true));
            RoomBuilder.GenerateBasicRoomLayout(Expand_GlitchedSecret);


            Expand_RickRollSecret.name = "Expand Apache RickRoll Secret";
            Expand_RickRollSecret.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_RickRollSecret.GUID = Guid.NewGuid().ToString();
            Expand_RickRollSecret.PreventMirroring = false;
            Expand_RickRollSecret.category = PrototypeDungeonRoom.RoomCategory.SECRET;
            Expand_RickRollSecret.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_RickRollSecret.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_RickRollSecret.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_RickRollSecret.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_RickRollSecret.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_RickRollSecret.pits = new List<PrototypeRoomPitEntry>();
            Expand_RickRollSecret.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_RickRollSecret.placedObjectPositions = new List<Vector2>();
            Expand_RickRollSecret.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_RickRollSecret.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_RickRollSecret.overriddenTilesets = 0;
            Expand_RickRollSecret.prerequisites = new List<DungeonPrerequisite>();
            Expand_RickRollSecret.InvalidInCoop = false;
            Expand_RickRollSecret.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_RickRollSecret.preventAddedDecoLayering = false;
            Expand_RickRollSecret.precludeAllTilemapDrawing = false;
            Expand_RickRollSecret.drawPrecludedCeilingTiles = false;
            Expand_RickRollSecret.preventBorders = false;
            Expand_RickRollSecret.preventFacewallAO = false;
            Expand_RickRollSecret.usesCustomAmbientLight = false;
            Expand_RickRollSecret.customAmbientLight = Color.white;
            Expand_RickRollSecret.ForceAllowDuplicates = false;
            Expand_RickRollSecret.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_RickRollSecret.IsLostWoodsRoom = false;
            Expand_RickRollSecret.UseCustomMusic = false;
            Expand_RickRollSecret.UseCustomMusicState = false;
            Expand_RickRollSecret.CustomMusicEvent = string.Empty;
            Expand_RickRollSecret.UseCustomMusicSwitch = false;
            Expand_RickRollSecret.CustomMusicSwitch = string.Empty;
            Expand_RickRollSecret.OverrideMusicState = DungeonFloorMusicController.DungeonMusicState.CALM;
            Expand_RickRollSecret.overrideRoomVisualTypeForSecretRooms = false;
            Expand_RickRollSecret.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_RickRollSecret.overrideRoomVisualType = -1;
            Expand_RickRollSecret.Width = 16;
            Expand_RickRollSecret.Height = 16;
            Expand_RickRollSecret.allowFloorDecoration = true;
            Expand_RickRollSecret.allowWallDecoration = true;
            Expand_RickRollSecret.usesProceduralLighting = false;
            Expand_RickRollSecret.usesProceduralDecoration = true;
            Expand_RickRollSecret.additionalObjectLayers = new List<PrototypeRoomObjectLayer>(0);
            RoomBuilder.AddExitToRoom(Expand_RickRollSecret, new Vector2(0, 8), DungeonData.Direction.WEST);
            RoomBuilder.AddExitToRoom(Expand_RickRollSecret, new Vector2(8, 0), DungeonData.Direction.SOUTH);
            RoomBuilder.AddExitToRoom(Expand_RickRollSecret, new Vector2(8, 17), DungeonData.Direction.NORTH);
            RoomBuilder.AddExitToRoom(Expand_RickRollSecret, new Vector2(17, 8), DungeonData.Direction.EAST);
            RoomBuilder.AddObjectToRoom(Expand_RickRollSecret, new Vector2(6, 8), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.RickRollChestObject, useExternalPrefab: true), xOffset: 8);
            RoomBuilder.GenerateBasicRoomLayout(Expand_RickRollSecret);

            Expand_BootlegRoom = RoomFactory.BuildFromAssetBundle(AssetBundles, "Expand_BootlegRoom", true, false);
            Expand_BootlegRoom.name = "Expand Apache Bootleg";
            Expand_BootlegRoom.QAID = "FF" + UnityEngine.Random.Range(1000, 9999);
            Expand_BootlegRoom.GUID = Guid.NewGuid().ToString();
            Expand_BootlegRoom.PreventMirroring = false;
            Expand_BootlegRoom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
            Expand_BootlegRoom.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Expand_BootlegRoom.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Expand_BootlegRoom.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Expand_BootlegRoom.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Expand_BootlegRoom.exitData = new PrototypeRoomExitData() { exits = new List<PrototypeRoomExit>() };
            Expand_BootlegRoom.pits = new List<PrototypeRoomPitEntry>();
            Expand_BootlegRoom.placedObjects = new List<PrototypePlacedObjectData>();
            Expand_BootlegRoom.placedObjectPositions = new List<Vector2>();
            Expand_BootlegRoom.eventTriggerAreas = new List<PrototypeEventTriggerArea>();
            Expand_BootlegRoom.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Expand_BootlegRoom.overriddenTilesets = 0;
            Expand_BootlegRoom.prerequisites = new List<DungeonPrerequisite>();
            Expand_BootlegRoom.InvalidInCoop = false;
            Expand_BootlegRoom.cullProceduralDecorationOnWeakPlatforms = false;
            Expand_BootlegRoom.preventAddedDecoLayering = true;
            Expand_BootlegRoom.precludeAllTilemapDrawing = true;
            Expand_BootlegRoom.drawPrecludedCeilingTiles = false;
            Expand_BootlegRoom.preventBorders = false;
            Expand_BootlegRoom.preventFacewallAO = false;
            Expand_BootlegRoom.usesCustomAmbientLight = true;
            Expand_BootlegRoom.customAmbientLight = new Color(0.8f, 0.8f, 0.8f, 1);
            Expand_BootlegRoom.ForceAllowDuplicates = false;
            Expand_BootlegRoom.injectionFlags = new RuntimeInjectionFlags() { CastleFireplace = false, ShopAnnexed = false };
            Expand_BootlegRoom.IsLostWoodsRoom = false;
            Expand_BootlegRoom.UseCustomMusic = true;
            Expand_BootlegRoom.UseCustomMusicState = false;
            Expand_BootlegRoom.CustomMusicEvent = "Play_MUS_Dungeon_State_Winner";
            Expand_BootlegRoom.UseCustomMusicSwitch = true;
            Expand_BootlegRoom.CustomMusicSwitch = "Play_EX_MUS_BootlegMusic_01";
            Expand_BootlegRoom.overrideRoomVisualTypeForSecretRooms = false;
            Expand_BootlegRoom.rewardChestSpawnPosition = new IntVector2(-1, -1);
            Expand_BootlegRoom.additionalObjectLayers = new List<PrototypeRoomObjectLayer>() {
                new PrototypeRoomObjectLayer() {
                    placedObjects = new List<PrototypePlacedObjectData>() {
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = ExpandCustomEnemyDatabase.BootlegShotgunManRedGUID, // bootleg_shotgunmanred
                            contentsBasePosition = new Vector2(12, 11),
                            layer = 0,
                            xMPxOffset = 0,
                            yMPxOffset = 0,
                            fieldData = new List<PrototypePlacedObjectFieldData>(0),
                            instancePrerequisites = new DungeonPrerequisite[0],
                            linkedTriggerAreaIDs = new List<int>(0),
                            assignedPathStartNode = 0
                        },
                        new PrototypePlacedObjectData() {
                            enemyBehaviourGuid = ExpandCustomEnemyDatabase.BootlegBullatGUID, // bootleg_bullat
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
                            enemyBehaviourGuid = ExpandCustomEnemyDatabase.BootlegShotgunManBlueGUID, // bootleg_shotgunmanblue
                            contentsBasePosition = new Vector2(10, 6),
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
                        new Vector2(12, 11),
                        new Vector2(4, 6),
                        new Vector2(10, 6)
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
            Expand_BootlegRoom.Width = 20;
            Expand_BootlegRoom.Height = 14;
            Expand_BootlegRoom.usesProceduralLighting = false;
            Expand_BootlegRoom.allowFloorDecoration = false;
            Expand_BootlegRoom.allowWallDecoration = false;
            Expand_BootlegRoom.usesProceduralDecoration = false;
            RoomBuilder.AddExitToRoom(Expand_BootlegRoom, new Vector2(1, 7), DungeonData.Direction.WEST, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_BootlegRoom, new Vector2(20, 7), DungeonData.Direction.EAST, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_BootlegRoom, new Vector2(10, 0), DungeonData.Direction.SOUTH, ContainsDoor: false);
            RoomBuilder.AddExitToRoom(Expand_BootlegRoom, new Vector2(10, 14), DungeonData.Direction.NORTH, ContainsDoor: false);
            RoomBuilder.AddObjectToRoom(Expand_BootlegRoom, new Vector2(0, 0), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXBootlegRoomObject, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_BootlegRoom, new Vector2(0, 0), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.EXBootlegRoomDoorTriggers, useExternalPrefab: true));
            RoomBuilder.AddObjectToRoom(Expand_BootlegRoom, new Vector2(8, 8), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.BootlegBulletManGUID); // Bootleg BulletMan
            RoomBuilder.AddObjectToRoom(Expand_BootlegRoom, new Vector2(7, 11), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.BootlegBulletManGUID); // Bootleg BulletMan
            RoomBuilder.AddObjectToRoom(Expand_BootlegRoom, new Vector2(5, 5), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.BootlegBulletManBandanaGUID); // Bootleg BulletManBandana
            RoomBuilder.AddObjectToRoom(Expand_BootlegRoom, new Vector2(5, 8), EnemyBehaviourGuid: ExpandCustomEnemyDatabase.BootlegBullatGUID); // Bootleg Bullat


            List<PrototypeDungeonRoom> m_JungleRooms = new List<PrototypeDungeonRoom>();

            foreach (string name in Expand_Jungle_RoomList) {
                PrototypeDungeonRoom m_room = RoomFactory.BuildFromAssetBundle(AssetBundles, name, true);
                if (name == "Expand_Forest_Mixed22" && m_room != null) {
                    RoomBuilder.AddObjectToRoom(m_room, new Vector2(15, 13), ExpandUtility.GenerateDungeonPlacable(ExpandPrefabs.Jungle_BlobLostSign, useExternalPrefab: true));
                }
                m_JungleRooms.Add(m_room);
            }

            // Expand_Jungle_Rooms = ExpandUtility.BuildRoomArrayFromTextFile("Textures/RoomLayoutData/RoomFactoryRooms/Jungle/Jungle_RoomEntries.txt");
            Expand_Jungle_Rooms = m_JungleRooms.ToArray();

            foreach (PrototypeDungeonRoom room in Expand_Jungle_Rooms) {
                ExpandPrefabs.JungleRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(room, 1));
            }


            List<PrototypeDungeonRoom> m_BellyRooms = new List<PrototypeDungeonRoom>();

            foreach (string name in Expand_Belly_RoomList) {
                PrototypeDungeonRoom m_room = RoomFactory.BuildFromAssetBundle(AssetBundles, name, true);
                m_BellyRooms.Add(m_room);
            }

            Expand_Belly_Rooms = m_BellyRooms.ToArray();

            foreach (PrototypeDungeonRoom room in Expand_Belly_Rooms) { ExpandPrefabs.BellyRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(room, 1)); }
            ExpandPrefabs.BellyRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(Expand_Belly_Connector_01, 1));
            ExpandPrefabs.BellyRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(Expand_Belly_Connector_02, 1));
            ExpandPrefabs.BellyRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(Expand_Belly_Connector_03, 1));
            ExpandPrefabs.BellyRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(Expand_Belly_Connector_04, 1));
            ExpandPrefabs.BellyRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(Expand_Belly_Connector_05, 1));
            ExpandPrefabs.BellyRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(Expand_Belly_Connector_06, 1));


            WeightedRoom[] CustomTrapRooms = new WeightedRoom[] {
                GenerateWeightedRoom(ThwompCrossingVertical),
                GenerateWeightedRoom(ThwompCrossingHorizontal),
                GenerateWeightedRoom(Expand_Apache_TheCrushZone),
                GenerateWeightedRoom(Expand_Apache_SpikeAndPits),
                GenerateWeightedRoom(Expand_Apache_PitTraps),
                GenerateWeightedRoom(Expand_Apache_RainbowRoom, Weight: 0.2f)/*
                GenerateWeightedRoom(Expand_Apache_FieldOfSaws), // Added only to Sewers and Abbey now
                GenerateWeightedRoom(Trap_Expand_Turtlemelon),
                GenerateWeightedRoom(Expand_Apache_RickRollChest, Weight: 0.25f),
                GenerateWeightedRoom(Expand_Apache_SurpriseChest, Weight: 0.35f),*/
            };

            foreach (WeightedRoom room in CustomTrapRooms) {
                if (room.room != Expand_Apache_PitTraps && room.room != Expand_Apache_RainbowRoom) {
                    ExpandPrefabs.CastleRoomTable.includedRooms.elements.Add(room);
                }
                ExpandPrefabs.SewersRoomTable.includedRooms.elements.Add(room);
                if (room.room != Expand_Apache_RainbowRoom) { ExpandPrefabs.Gungeon_RoomTable.includedRooms.elements.Add(room); }
                ExpandPrefabs.AbbeyRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.CatacombsRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.ForgeRoomTable.includedRooms.elements.Add(room);
                // if (room.room != Expand_Apache_RickRollChest) { ExpandPrefabs.BulletHellRoomTable.includedRooms.elements.Add(room); }
                ExpandPrefabs.CustomRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.CustomRoomTable2.includedRooms.elements.Add(room);
                ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(room);
            }

            ExpandPrefabs.SewersRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(Expand_Apache_FieldOfSaws));
            ExpandPrefabs.AbbeyRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(Expand_Apache_FieldOfSaws));

            WeightedRoom[] CustomSecretRooms = new WeightedRoom[] {
                GenerateWeightedRoom(Expand_TinySecret, 4),
                GenerateWeightedRoom(Expand_GlitchedSecret, 0.45f),
                GenerateWeightedRoom(Expand_RickRollSecret, 0.8f),
                GenerateWeightedRoom(Expand_Secret_Falsechest, 1),
            };

            WeightedRoom[] CustomAlbernRooms = new WeightedRoom[] {
                GenerateWeightedRoom(Expand_Secret_Brother1),
                GenerateWeightedRoom(Expand_Secret_Brother2),
                GenerateWeightedRoom(Expand_Secret_Brother3),
                GenerateWeightedRoom(Expand_Secret_Brother4),
                GenerateWeightedRoom(Expand_Secret_Brother5),
                GenerateWeightedRoom(Expand_Secret_Brother6),
                GenerateWeightedRoom(Expand_Secret_Brother7),
                GenerateWeightedRoom(Expand_Secret_Brother8),
                GenerateWeightedRoom(Expand_Secret_Brother9),
                GenerateWeightedRoom(Expand_Secret_Brother10)
            };

            WeightedRoom[] CustomMiscRooms = new WeightedRoom[] { GenerateWeightedRoom(Expand_BootlegRoom) };

            foreach (WeightedRoom room in CustomMiscRooms) {
                ExpandPrefabs.CastleRoomTable.includedRooms.elements.Add(room);
                if (!ExpandSettings.IsHardModeBuild) {
                    ExpandPrefabs.SewersRoomTable.includedRooms.elements.Add(room);
                }
                ExpandPrefabs.Gungeon_RoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.AbbeyRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.MinesRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.CatacombsRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.ForgeRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.CustomRoomTable.includedRooms.elements.Add(room);
                ExpandPrefabs.CustomRoomTable2.includedRooms.elements.Add(room);
                ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(room);
            }

            WeightedRoom[] CustomCastleRooms = new WeightedRoom[] {
                // GenerateWeightedRoom(Expand_Challange1), // These removed based on feedback on TheTurtleMelon and/ore other players.
                // GenerateWeightedRoom(Expand_Cross),
                // GenerateWeightedRoom(Expand_Gate_Cross),
                // GenerateWeightedRoom(Expand_Pit_Jump),
                GenerateWeightedRoom(Expand_Explode),
                GenerateWeightedRoom(Expand_C_Hub),
                GenerateWeightedRoom(Expand_C_Gap),
                GenerateWeightedRoom(Expand_ChainGap),
                GenerateWeightedRoom(Expand_Pit_Line),
                GenerateWeightedRoom(Expand_Singer_Gap),
                GenerateWeightedRoom(Expand_Flying_Gap),
                GenerateWeightedRoom(Expand_Battle),
                GenerateWeightedRoom(Expand_Blocks),
                GenerateWeightedRoom(Expand_Blocks_Pits),
                GenerateWeightedRoom(Expand_Wall_Pit),
                GenerateWeightedRoom(Expand_Passage),                
                GenerateWeightedRoom(Expand_Pit_Passage),
                GenerateWeightedRoom(Expand_R_Blocks),
                GenerateWeightedRoom(Expand_Small_Passage),
                GenerateWeightedRoom(Expand_Box),
                GenerateWeightedRoom(Expand_Steps),
                GenerateWeightedRoom(Expand_Apache_Hub),
                GenerateWeightedRoom(Expand_Box_Hub),
                GenerateWeightedRoom(Expand_Enclose_Hub)
            };

            WeightedRoom[] CustomSewersRooms = new WeightedRoom[] {
                GenerateWeightedRoom(Expand_4wave),
                GenerateWeightedRoom(Expand_Spiralbomb),
                GenerateWeightedRoom(Expand_Bat),
                GenerateWeightedRoom(Expand_Batsmall),
                GenerateWeightedRoom(Expand_BIRDS),
                GenerateWeightedRoom(Expand_Blobs),
                GenerateWeightedRoom(Expand_BoogalooFailure2),
                GenerateWeightedRoom(Expand_Chess),
                GenerateWeightedRoom(Expand_Cornerpits),
                GenerateWeightedRoom(Expand_Enclosed),
                GenerateWeightedRoom(Expand_Funky),
                GenerateWeightedRoom(Expand_Gapsniper),
                GenerateWeightedRoom(Expand_Hallway),
                GenerateWeightedRoom(Expand_HUB_1wave),
                GenerateWeightedRoom(Expand_Islands),
                GenerateWeightedRoom(Expand_Long),
                GenerateWeightedRoom(Expand_Mushroom),
                GenerateWeightedRoom(Expand_Mutant),
                GenerateWeightedRoom(Expand_Oddshroom),
                GenerateWeightedRoom(Expand_Pitzag),
                GenerateWeightedRoom(Expand_Secret_Falsechest),
                GenerateWeightedRoom(Expand_Shotgun),
                GenerateWeightedRoom(Expand_Smallcentral)
            };

            WeightedRoom[] CustomGungeonProperRooms = new WeightedRoom[] {
                GenerateWeightedRoom(Expand_Crosshairs),
                GenerateWeightedRoom(Expand_Basic),
                GenerateWeightedRoom(Expand_JumpInThePit),
                GenerateWeightedRoom(Expand_LongSpikeTrap),
                GenerateWeightedRoom(Expand_SpikeTrap),
                GenerateWeightedRoom(Expand_ThinRoom),
                GenerateWeightedRoom(Expand_SniperRoom),
                GenerateWeightedRoom(Expand_TableRoom)
            };

            WeightedRoom[] CustomMinesRooms = new WeightedRoom[] {
                // GenerateWeightedRoom(Expand_GoopTroop),
                // Awaiting Re-balance design changes. :P
                // GenerateWeightedRoom(Expand_HopScotch),
                GenerateWeightedRoom(Expand_OilRoom),
                GenerateWeightedRoom(Expand_Pit),
                GenerateWeightedRoom(Expand_Singer),
                GenerateWeightedRoom(Expand_TableRoom2),
                GenerateWeightedRoom(Expand_Walkway)
            };

            WeightedRoom[] CustomHollowsRooms = new WeightedRoom[] {
                GenerateWeightedRoom(Expand_SpiderMaze),
                GenerateWeightedRoom(Expand_BlobRoom),
                GenerateWeightedRoom(Expand_HellInACell),
                GenerateWeightedRoom(Expand_IceIsNice),
                GenerateWeightedRoom(Expand_IceScotch),
                GenerateWeightedRoom(Expand_MrPresident),
                GenerateWeightedRoom(Expand_SawRoom),
                GenerateWeightedRoom(Expand_Agony),
                GenerateWeightedRoom(Expand_ice1),
                GenerateWeightedRoom(Expand_Ice2),
                GenerateWeightedRoom(Expand_Ice3),
                GenerateWeightedRoom(Expand_Ice4),
                GenerateWeightedRoom(Expand_LargeMany),
                GenerateWeightedRoom(Expand_Roundabout),
                GenerateWeightedRoom(Expand_Shells),
                GenerateWeightedRoom(Expand_Spooky),
                GenerateWeightedRoom(Expand_Undead1),
                GenerateWeightedRoom(Expand_Undead2),
                GenerateWeightedRoom(Expand_Undead3),
                GenerateWeightedRoom(Expand_Undead4)
            };

            WeightedRoom[] CustomForgeRooms = new WeightedRoom[] {
                GenerateWeightedRoom(Expand_Arena),
                GenerateWeightedRoom(Expand_CaptainCrunch),
                GenerateWeightedRoom(Expand_CorridorOfDoom),
                GenerateWeightedRoom(Expand_FireRoom),
                GenerateWeightedRoom(Expand_Pits),
                GenerateWeightedRoom(Expand_SkullRoom),
                GenerateWeightedRoom(Expand_TableRoomAgain)
            };
            
                        
            foreach (PrototypeDungeonRoom room in Expand_West_CanyonRooms) {
                ExpandPrefabs.WestCanyonRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(room));
            }

            foreach (PrototypeDungeonRoom room in Expand_West_TinyCanyonRooms) {
                ExpandPrefabs.WestTinyCanyonRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(room));
            }

            foreach (PrototypeDungeonRoom room in Expand_West_Interior1Rooms) {
                ExpandPrefabs.WestInterior1RoomTable.includedRooms.elements.Add(GenerateWeightedRoom(room));
            }

            foreach (WeightedRoom room in CustomCastleRooms) {
                ExpandPrefabs.CastleRoomTable.includedRooms.elements.Add(room);
                if (room.room.overrideRoomVisualType == -1) {
                    ExpandPrefabs.CustomRoomTable.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTable2.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(room);
                }
            }


            foreach (WeightedRoom room in CustomGungeonProperRooms) {
                ExpandPrefabs.Gungeon_RoomTable.includedRooms.elements.Add(room);
                if (room.room.overrideRoomVisualType == -1) {
                    ExpandPrefabs.CustomRoomTable.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTable2.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(room);
                }
            }

            if (!ExpandSettings.IsHardModeBuild) {
                foreach (WeightedRoom room in CustomSewersRooms) {
                    ExpandPrefabs.SewersRoomTable.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTable.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTable2.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(room);
                }
            }

            foreach (WeightedRoom room in CustomMinesRooms) {
                ExpandPrefabs.MinesRoomTable.includedRooms.elements.Add(room);
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

            foreach(WeightedRoom room in CustomForgeRooms) {
                ExpandPrefabs.ForgeRoomTable.includedRooms.elements.Add(room);
                if (room.room.overrideRoomVisualType == -1) {
                    ExpandPrefabs.CustomRoomTable.includedRooms.elements.Add(room);
                    ExpandPrefabs.CustomRoomTable2.includedRooms.elements.Add(room);
                    if (room.room != Expand_Pits && room.room != Expand_CorridorOfDoom) {
                        ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Add(room);
                    }
                }
            }

            foreach (WeightedRoom room in CustomSecretRooms) {
                ExpandPrefabs.SecretRoomTable.includedRooms.elements.Add(room);
            }
            foreach (WeightedRoom wRoom in CustomAlbernRooms) {
                ExpandPrefabs.AbbeyAblernRoomTable.includedRooms.elements.Add(wRoom);
            }


            for(int i = 0; i < ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Count; i++) {
                bool removeRoom = false;
                if (ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements[i].room != null && ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements[i].room.FullCellData != null) {                    
                    foreach (PrototypeDungeonRoomCellData cellData in ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements[i].room.FullCellData) {
                        if (cellData.state == CellType.PIT) { removeRoom = true; break; }
                    }
                    if (removeRoom) { ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements.Remove(ExpandPrefabs.CustomRoomTableSecretGlitchFloor.includedRooms.elements[i]); }
                }
            }

            AssetBundles = null;
        }
    }
}

