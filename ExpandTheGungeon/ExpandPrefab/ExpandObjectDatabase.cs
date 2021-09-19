using System.Collections.Generic;
using UnityEngine;
using Dungeonator;

namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandObjectDatabase : MonoBehaviour {
        
        public static GameObject YellowDrum;
        public static GameObject RedDrum;
        public static GameObject WaterDrum;
        public static GameObject OilDrum;
        public static GameObject IceBomb;
        public static GameObject TableHorizontal;
        public static GameObject TableVertical;
        public static GameObject TableHorizontalStone;
        public static GameObject TableVerticalStone;
        public static GameObject NPCOldMan;
        public static GameObject NPCSynergrace;
        public static GameObject NPCTonic;
        public static GameObject NPCCursola;
        public static GameObject NPCGunMuncher;
        public static GameObject NPCEvilMuncher;
        public static GameObject NPCMonsterManuel;
        public static GameObject NPCVampire;
        public static GameObject NPCGuardLeft;
        public static GameObject NPCGuardRight;
        public static GameObject NPCTruthKnower;
        public static GameObject NPCHeartDispenser;
        public static GameObject AmygdalaNorth;
        public static GameObject AmygdalaSouth;
        public static GameObject AmygdalaWest;
        public static GameObject AmygdalaEast;
        public static GameObject SpaceFog;
        public static GameObject LockedDoor;
        public static GameObject LockedJailDoor;
        public static GameObject SpikeTrap;
        public static GameObject FlameTrap;
        public static GameObject FakeTrap;
        public static GameObject PlayerCorpse;
        public static GameObject TimefallCorpse;
        public static GameObject ThoughtBubble;
        public static GameObject HangingPot;
        public static GameObject DoorsVertical;
        public static GameObject DoorsHorizontal;
        public static GameObject BigDoorsHorizontal;
        public static GameObject BigDoorsVertical;
        public static GameObject RatTrapDoorIcon;
        public static GameObject CultistBaldBowBackLeft;
        public static GameObject CultistBaldBowBackRight;
        public static GameObject CultistBaldBowBack;
        public static GameObject CultistBaldBowLeft;
        public static GameObject CultistHoodBowBack;
        public static GameObject CultistHoodBowLeft;
        public static GameObject CultistHoodBowRight;
        public static GameObject ForgeHammer;
        public static GameObject ChestBrownTwoItems;
        public static GameObject ChestTruth;
        public static GameObject ChestBlue;
        public static GameObject ChestRed;
        public static GameObject ChestBlack;
        public static GameObject ChestRat;
        public static GameObject ChestMirror;
        public static GameObject ConvictPastCrowdNPC_01;
        public static GameObject ConvictPastCrowdNPC_02;
        public static GameObject ConvictPastCrowdNPC_03;
        public static GameObject ConvictPastCrowdNPC_04;
        public static GameObject ConvictPastCrowdNPC_05;
        public static GameObject ConvictPastCrowdNPC_06;
        public static GameObject ConvictPastCrowdNPC_07;
        public static GameObject ConvictPastCrowdNPC_08;
        public static GameObject ConvictPastCrowdNPC_09;
        public static GameObject ConvictPastCrowdNPC_10;
        public static GameObject ConvictPastCrowdNPC_11;
        public static GameObject ConvictPastCrowdNPC_12;
        public static GameObject ConvictPastCrowdNPC_13;
        public static GameObject ConvictPastCrowdNPC_14;
        public static GameObject ConvictPastCrowdNPC_15;
        public static GameObject ConvictPastCrowdNPC_16;
        public static GameObject[] ConvictPastDancers;
        public static GameObject DoorsVertical_Catacombs;
        public static GameObject DoorsHorizontal_Catacombs;
        public static GameObject FoldingTable;
        public static GameObject WinchesterMinimapIcon;
        public static GameObject CrushDoor_Horizontal;
        public static GameObject CrushDoor_Vertical;
        public static GameObject Mines_Cave_In;
        public static GameObject Plunger;
        public static GameObject GatlingGullNest;
        public static GameObject BabyDragunNPC;
        public static GameObject GungeonLightStone;
        public static GameObject GungeonLightPurple;
        public static GameObject Sconce_Light;
        public static GameObject Sconce_Light_Side;
        public static GameObject DefaultTorch;
        public static GameObject DefaultTorchSide;
        public static GameObject GungeonWarpDoor;
        public static GameObject CastleWarpDoor;
        public static GameObject EndTimes;
        
        // DungeonPlacables
        public static DungeonPlaceable ExplodyBarrel;
        public static DungeonPlaceable CoffinVertical;
        public static DungeonPlaceable CoffinHorizontal;
        public static DungeonPlaceable Brazier;
        public static DungeonPlaceable CursedPot;
        public static DungeonPlaceable Sarcophogus;
        public static DungeonPlaceable GodRays;
        public static DungeonPlaceable SpecialTraps;
        public static DungeonPlaceable PitTrap;
        public static DungeonPlaceable Bush;
        public static DungeonPlaceable BushFlowers;
        public static DungeonPlaceable WoodenBarrel;
        public static DungeonPlaceable WrithingBulletman;
        public static DungeonPlaceable GungeonLockedDoors;
        public static DungeonPlaceable IronWoodDoors;


        public static void BuildDatabase() {
            // Dungeon marinePastDungeon = DungeonDatabase.GetOrLoadByName("finalscenario_soldier");
            Dungeon convictPastDungeon = DungeonDatabase.GetOrLoadByName("finalscenario_convict");
            Dungeon catacombsDungeon = DungeonDatabase.GetOrLoadByName("base_catacombs");
            Dungeon sewersDungeon = DungeonDatabase.GetOrLoadByName("base_sewer");
            Dungeon forgeDungeon = DungeonDatabase.GetOrLoadByName("base_forge");
            Dungeon gungeonDungeon = DungeonDatabase.GetOrLoadByName("base_gungeon");
            Dungeon castleDungeon = DungeonDatabase.GetOrLoadByName("base_castle");

            YellowDrum = ExpandAssets.LoadOfficialAsset<GameObject>("Yellow Drum", ExpandAssets.AssetSource.SharedAuto2);
            RedDrum = ExpandAssets.LoadOfficialAsset<GameObject>("Red Drum", ExpandAssets.AssetSource.SharedAuto1);
            WaterDrum = ExpandAssets.LoadOfficialAsset<GameObject>("Blue Drum", ExpandAssets.AssetSource.SharedAuto2);
            OilDrum = ExpandAssets.LoadOfficialAsset<GameObject>("Purple Drum", ExpandAssets.AssetSource.SharedAuto2);
            IceBomb = ExpandAssets.LoadOfficialAsset<GameObject>("Ice Cube Bomb", ExpandAssets.AssetSource.SharedAuto2);
            TableHorizontal = ExpandAssets.LoadOfficialAsset<GameObject>("Table_Horizontal", ExpandAssets.AssetSource.SharedAuto1);
            TableVertical = ExpandAssets.LoadOfficialAsset<GameObject>("Table_Vertical", ExpandAssets.AssetSource.SharedAuto1);
            TableHorizontalStone = ExpandAssets.LoadOfficialAsset<GameObject>("Table_Horizontal_Stone", ExpandAssets.AssetSource.SharedAuto1);
            TableVerticalStone = ExpandAssets.LoadOfficialAsset<GameObject>("Table_Vertical_Stone", ExpandAssets.AssetSource.SharedAuto1);
            NPCOldMan = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_Old_Man", ExpandAssets.AssetSource.SharedAuto1);
            NPCSynergrace = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_Synergrace", ExpandAssets.AssetSource.SharedAuto1);
            NPCTonic = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_Tonic", ExpandAssets.AssetSource.SharedAuto1);
            NPCCursola = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_Curse_Jailed", ExpandAssets.AssetSource.SharedAuto2);
            NPCGunMuncher = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_GunberMuncher", ExpandAssets.AssetSource.SharedAuto2);
            NPCEvilMuncher = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_GunberMuncher_Evil", ExpandAssets.AssetSource.SharedAuto1);
            NPCMonsterManuel = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_Monster_Manuel", ExpandAssets.AssetSource.SharedAuto1);
            NPCVampire = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_Vampire", ExpandAssets.AssetSource.SharedAuto2);
            NPCGuardLeft = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_Guardian_Left", ExpandAssets.AssetSource.SharedAuto2);
            NPCGuardRight = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_Guardian_Right", ExpandAssets.AssetSource.SharedAuto2);
            NPCTruthKnower = ExpandAssets.LoadOfficialAsset<GameObject>("NPC_Truth_Knower", ExpandAssets.AssetSource.SharedAuto1);
            NPCHeartDispenser = ExpandAssets.LoadOfficialAsset<GameObject>("HeartDispenser", ExpandAssets.AssetSource.SharedAuto2);
            AmygdalaNorth = ExpandAssets.LoadOfficialAsset<GameObject>("Amygdala_North", ExpandAssets.AssetSource.BraveResources);
            AmygdalaSouth = ExpandAssets.LoadOfficialAsset<GameObject>("Amygdala_South", ExpandAssets.AssetSource.BraveResources);
            AmygdalaWest = ExpandAssets.LoadOfficialAsset<GameObject>("Amygdala_West", ExpandAssets.AssetSource.BraveResources);
            AmygdalaEast = ExpandAssets.LoadOfficialAsset<GameObject>("Amygdala_East", ExpandAssets.AssetSource.BraveResources);
            SpaceFog = ExpandAssets.LoadOfficialAsset<GameObject>("Space Fog", ExpandAssets.AssetSource.BraveResources);
            LockedDoor = ExpandAssets.LoadOfficialAsset<GameObject>("SimpleLockedDoor", ExpandAssets.AssetSource.SharedAuto2);
            LockedJailDoor = ExpandAssets.LoadOfficialAsset<GameObject>("JailDoor", ExpandAssets.AssetSource.SharedAuto2);
            SpikeTrap = ExpandAssets.LoadOfficialAsset<GameObject>("trap_spike_gungeon_2x2", ExpandAssets.AssetSource.SharedAuto1);
            FlameTrap = ExpandAssets.LoadOfficialAsset<GameObject>("trap_flame_poofy_gungeon_1x1", ExpandAssets.AssetSource.SharedAuto2);
            FakeTrap = ExpandAssets.LoadOfficialAsset<GameObject>("trap_pit_gungeon_trigger_2x2", ExpandAssets.AssetSource.SharedAuto1);
            PlayerCorpse = ExpandAssets.LoadOfficialAsset<GameObject>("PlayerCorpse", ExpandAssets.AssetSource.BraveResources);
            TimefallCorpse = ExpandAssets.LoadOfficialAsset<GameObject>("TimefallCorpse", ExpandAssets.AssetSource.BraveResources);
            ThoughtBubble = ExpandAssets.LoadOfficialAsset<GameObject>("ThoughtBubble", ExpandAssets.AssetSource.BraveResources);
            HangingPot = ExpandAssets.LoadOfficialAsset<GameObject>("Hanging_Pot", ExpandAssets.AssetSource.SharedAuto1);
            DoorsVertical = ExpandAssets.LoadOfficialAsset<GameObject>("GungeonShopDoor_Vertical", ExpandAssets.AssetSource.SharedAuto2);
            DoorsHorizontal = ExpandAssets.LoadOfficialAsset<GameObject>("GungeonShopDoor_Horizontal", ExpandAssets.AssetSource.SharedAuto2);
            DoorsHorizontal_Catacombs = catacombsDungeon.doorObjects.variantTiers[0].nonDatabasePlaceable;
            DoorsVertical_Catacombs = catacombsDungeon.doorObjects.variantTiers[1].nonDatabasePlaceable;
            BigDoorsHorizontal = ExpandAssets.LoadOfficialAsset<GameObject>("IronWoodDoor_Horizontal_Gungeon", ExpandAssets.AssetSource.SharedAuto2);
            BigDoorsVertical = ExpandAssets.LoadOfficialAsset<GameObject>("IronWoodDoor_Vertical_Gungeon", ExpandAssets.AssetSource.SharedAuto2);
            RatTrapDoorIcon = ExpandAssets.LoadOfficialAsset<GameObject>("RatTrapdoorMinimapIcon", ExpandAssets.AssetSource.BraveResources);
            CultistBaldBowBackLeft = ExpandAssets.LoadOfficialAsset<GameObject>("CultistBaldBowBackLeft_cutout", ExpandAssets.AssetSource.SharedAuto2);
            CultistBaldBowBackRight = ExpandAssets.LoadOfficialAsset<GameObject>("CultistBaldBowBackRight_cutout", ExpandAssets.AssetSource.SharedAuto2);
            CultistBaldBowBack = ExpandAssets.LoadOfficialAsset<GameObject>("CultistBaldBowBack_cutout", ExpandAssets.AssetSource.SharedAuto2);
            CultistBaldBowLeft = ExpandAssets.LoadOfficialAsset<GameObject>("CultistBaldBowLeft_cutout", ExpandAssets.AssetSource.SharedAuto2);
            CultistHoodBowBack = ExpandAssets.LoadOfficialAsset<GameObject>("CultistHoodBowBack_cutout", ExpandAssets.AssetSource.SharedAuto2);
            CultistHoodBowLeft = ExpandAssets.LoadOfficialAsset<GameObject>("CultistHoodBowLeft_cutout", ExpandAssets.AssetSource.SharedAuto2);
            CultistHoodBowRight = ExpandAssets.LoadOfficialAsset<GameObject>("CultistHoodBowRight_cutout", ExpandAssets.AssetSource.SharedAuto2);
            ForgeHammer = ExpandAssets.LoadOfficialAsset<GameObject>("Forge_Hammer", ExpandAssets.AssetSource.SharedAuto1);
            ChestBrownTwoItems = ExpandAssets.LoadOfficialAsset<GameObject>("Chest_Wood_Two_Items", ExpandAssets.AssetSource.SharedAuto1);
            ChestTruth = ExpandAssets.LoadOfficialAsset<GameObject>("TruthChest", ExpandAssets.AssetSource.SharedAuto1);
            ChestBlue = ChestTruth = ExpandAssets.LoadOfficialAsset<GameObject>("Chest_Silver", ExpandAssets.AssetSource.SharedAuto1);
            ChestRed = ChestTruth = ExpandAssets.LoadOfficialAsset<GameObject>("Chest_Red", ExpandAssets.AssetSource.SharedAuto1);
            ChestBlack = ChestTruth = ExpandAssets.LoadOfficialAsset<GameObject>("Chest_Black", ExpandAssets.AssetSource.SharedAuto1);
            ChestRat = ExpandAssets.LoadOfficialAsset<GameObject>("Chest_Rat", ExpandAssets.AssetSource.SharedAuto1);
            ChestMirror = ExpandAssets.LoadOfficialAsset<GameObject>("Shrine_Mirror", ExpandAssets.AssetSource.SharedAuto1);
            WinchesterMinimapIcon = ExpandAssets.LoadOfficialAsset<GameObject>("minimap_winchester_icon", ExpandAssets.AssetSource.SharedAuto1);
            GatlingGullNest = ExpandAssets.LoadOfficialAsset<GameObject>("gatlinggullnest", ExpandAssets.AssetSource.SharedAuto1);
            BabyDragunNPC = ExpandAssets.LoadOfficialAsset<GameObject>("BabyDragunJail", ExpandAssets.AssetSource.SharedAuto2);
            GungeonLightStone = ExpandAssets.LoadOfficialAsset<GameObject>("Gungeon Light (Stone)", ExpandAssets.AssetSource.SharedAuto1);
            GungeonLightPurple = ExpandAssets.LoadOfficialAsset<GameObject>("Gungeon Light (Purple)", ExpandAssets.AssetSource.SharedAuto1);
            Sconce_Light = ExpandAssets.LoadOfficialAsset<GameObject>("Sconce_Light", ExpandAssets.AssetSource.SharedAuto1);
            Sconce_Light_Side = ExpandAssets.LoadOfficialAsset<GameObject>("Sconce_Light_Side", ExpandAssets.AssetSource.SharedAuto1);
            DefaultTorch = ExpandAssets.LoadOfficialAsset<GameObject>("DefaultTorch", ExpandAssets.AssetSource.SharedAuto1);
            DefaultTorchSide = ExpandAssets.LoadOfficialAsset<GameObject>("DefaultTorchSide", ExpandAssets.AssetSource.SharedAuto1);
            GungeonWarpDoor = gungeonDungeon.WarpWingDoorPrefab;
            CastleWarpDoor = castleDungeon.WarpWingDoorPrefab;
            EndTimes = ExpandAssets.LoadOfficialAsset<GameObject>("EndTimes", ExpandAssets.AssetSource.BraveResources);

            foreach (WeightedRoom wRoom in sewersDungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements)
            {
                if (wRoom.room != null && !string.IsNullOrEmpty(wRoom.room.name))
                {
                    if (wRoom.room.name.ToLower().StartsWith("sewer_trash_compactor_001"))
                    {
                        CrushDoor_Horizontal = wRoom.room.placedObjects[0].nonenemyBehaviour.gameObject;
                    }
                }
            }

            foreach (WeightedRoom wRoom in forgeDungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements)
            {
                if (wRoom.room != null && !string.IsNullOrEmpty(wRoom.room.name))
                {
                    if (wRoom.room.name.ToLower().StartsWith("forge_normal_cubulead_03"))
                    {
                        CrushDoor_Vertical = wRoom.room.placedObjects[0].nonenemyBehaviour.gameObject;
                    }
                }
            }

            // Dungeon Placables
            ExplodyBarrel = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("ExplodyBarrel_Maybe", ExpandAssets.AssetSource.SharedAuto2);
            CoffinVertical = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Vertical Coffin", ExpandAssets.AssetSource.SharedAuto2);
            CoffinHorizontal = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Horizontal Coffin", ExpandAssets.AssetSource.SharedAuto2);
            Brazier = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Brazier", ExpandAssets.AssetSource.SharedAuto1);
            CursedPot = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Curse Pot", ExpandAssets.AssetSource.SharedAuto1);
            Sarcophogus = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Sarcophogus", ExpandAssets.AssetSource.SharedAuto1);
            GodRays = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Godrays_placeable", ExpandAssets.AssetSource.SharedAuto1);
            SpecialTraps = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("RobotDaveTraps", ExpandAssets.AssetSource.BraveResources);
            PitTrap = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Pit Trap", ExpandAssets.AssetSource.SharedAuto2);
            Bush = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Bush", ExpandAssets.AssetSource.SharedAuto2);
            BushFlowers = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Bush Flowers", ExpandAssets.AssetSource.SharedAuto2);
            WoodenBarrel = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Barrel_collection", ExpandAssets.AssetSource.SharedAuto1);
            WrithingBulletman = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("Writhing Bulletman", ExpandAssets.AssetSource.SharedAuto2);
            GungeonLockedDoors = gungeonDungeon.lockedDoorObjects;
            IronWoodDoors = ExpandAssets.LoadOfficialAsset<DungeonPlaceable>("DoorTest", ExpandAssets.AssetSource.SharedAuto2);
            // DimensionFog = marinePastDungeon.PatternSettings.flows[0].AllNodes[0].overrideExactRoom.placedObjects[0].nonenemyBehaviour.gameObject.transform.Find("DimensionFog").gameObject;

            ConvictPastController pastController = convictPastDungeon.PatternSettings.flows[0].AllNodes[0].overrideExactRoom.placedObjects[0].nonenemyBehaviour.gameObject.GetComponent<ConvictPastController>();
            NightclubCrowdController crowdController = pastController.crowdController;

            ConvictPastCrowdNPC_01 = crowdController.Dancers[0].gameObject;
            ConvictPastCrowdNPC_02 = crowdController.Dancers[1].gameObject;
            ConvictPastCrowdNPC_03 = crowdController.Dancers[2].gameObject;
            ConvictPastCrowdNPC_04 = crowdController.Dancers[3].gameObject;
            ConvictPastCrowdNPC_05 = crowdController.Dancers[4].gameObject;
            ConvictPastCrowdNPC_06 = crowdController.Dancers[5].gameObject;
            ConvictPastCrowdNPC_07 = crowdController.Dancers[6].gameObject;
            ConvictPastCrowdNPC_08 = crowdController.Dancers[7].gameObject;
            ConvictPastCrowdNPC_09 = crowdController.Dancers[8].gameObject;
            ConvictPastCrowdNPC_10 = crowdController.Dancers[9].gameObject;
            ConvictPastCrowdNPC_11 = crowdController.Dancers[10].gameObject;
            ConvictPastCrowdNPC_12 = crowdController.Dancers[11].gameObject;
            ConvictPastCrowdNPC_13 = crowdController.Dancers[12].gameObject;
            ConvictPastCrowdNPC_14 = crowdController.Dancers[13].gameObject;
            ConvictPastCrowdNPC_15 = crowdController.Dancers[14].gameObject;
            ConvictPastCrowdNPC_16 = crowdController.Dancers[15].gameObject;

            ConvictPastDancers = new GameObject[] {
                ConvictPastCrowdNPC_01,
                ConvictPastCrowdNPC_02,
                ConvictPastCrowdNPC_03,
                ConvictPastCrowdNPC_04,
                ConvictPastCrowdNPC_05,
                ConvictPastCrowdNPC_06,
                ConvictPastCrowdNPC_07,
                ConvictPastCrowdNPC_08,
                ConvictPastCrowdNPC_09,
                ConvictPastCrowdNPC_10,
                ConvictPastCrowdNPC_11,
                ConvictPastCrowdNPC_12,
                ConvictPastCrowdNPC_13,
                ConvictPastCrowdNPC_14,
                ConvictPastCrowdNPC_15,
                ConvictPastCrowdNPC_16
            };


            Mines_Cave_In = ExpandAssets.LoadOfficialAsset<GameObject>("Mines_Cave_In", ExpandAssets.AssetSource.SharedAuto2);
            Plunger = Mines_Cave_In.GetComponent<HangingObjectController>().triggerObjectPrefab;

            FoldingTable = PickupObjectDatabase.GetById(644).GetComponent<FoldingTableItem>().TableToSpawn.gameObject;

            convictPastDungeon = null;
            catacombsDungeon = null;
            sewersDungeon = null;
            forgeDungeon = null;
            gungeonDungeon = null;
            castleDungeon = null;
        }





        public static IntVector2 GetRandomAvailableCellForPlacable(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, bool useCachedList, bool allowPlacingOverPits = false, int gridSnap = 1) {
            if (!useCachedList | validCellsCached == null) { validCellsCached = new List<IntVector2>(); }
            if (validCellsCached.Count <= 0) {
                for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                    for (int height = -1; height <= currentRoom.area.dimensions.y; height++) {
                        int X = currentRoom.area.basePosition.x + Width;
                        int Y = currentRoom.area.basePosition.y + height;
                        if (X % gridSnap == 0 && Y % gridSnap == 0) {
                            if (allowPlacingOverPits) {
                                if (!dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) &&
                                    !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) &&
                                    !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) &&
                                    !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) &&
                                    !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) &&
                                    !dungeon.data[X - 2, Y + 2].isOccupied && !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied && !dungeon.data[X + 2, Y + 2].isOccupied &&
                                    !dungeon.data[X - 2, Y + 1].isOccupied && !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied && !dungeon.data[X + 2, Y + 1].isOccupied &&
                                    !dungeon.data[X - 2, Y].isOccupied && !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied && !dungeon.data[X + 2, Y].isOccupied &&
                                    !dungeon.data[X - 2, Y - 1].isOccupied && !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied && !dungeon.data[X + 2, Y - 1].isOccupied &&
                                    !dungeon.data[X - 2, Y - 2].isOccupied && !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied && !dungeon.data[X + 2, Y - 2].isOccupied)
                                {
                                    validCellsCached.Add(new IntVector2(X, Y));
                                }
                            } else {
                                if (!dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) &&
                                    !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) &&
                                    !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) &&
                                    !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) &&
                                    !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) &&
                                    !dungeon.data[X - 2, Y + 2].isOccupied && !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied && !dungeon.data[X + 2, Y + 2].isOccupied &&
                                    !dungeon.data[X - 2, Y + 1].isOccupied && !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied && !dungeon.data[X + 2, Y + 1].isOccupied &&
                                    !dungeon.data[X - 2, Y].isOccupied && !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied && !dungeon.data[X + 2, Y].isOccupied &&
                                    !dungeon.data[X - 2, Y - 1].isOccupied && !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied && !dungeon.data[X + 2, Y - 1].isOccupied &&
                                    !dungeon.data[X - 2, Y - 2].isOccupied && !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied && !dungeon.data[X + 2, Y - 2].isOccupied &&
                                    !dungeon.data.isPit(X - 2, Y + 2) && !dungeon.data.isPit(X - 1, Y + 2) && !dungeon.data.isPit(X, Y + 2) && !dungeon.data.isPit(X + 1, Y + 2) && !dungeon.data.isPit(X + 2, Y + 2) &&
                                    !dungeon.data.isPit(X - 2, Y + 1) && !dungeon.data.isPit(X - 1, Y + 1) && !dungeon.data.isPit(X, Y + 1) && !dungeon.data.isPit(X + 1, Y + 1) && !dungeon.data.isPit(X + 2, Y + 1) &&
                                    !dungeon.data.isPit(X - 2, Y) && !dungeon.data.isPit(X - 1, Y) && !dungeon.data.isPit(X, Y) && !dungeon.data.isPit(X + 1, Y) && !dungeon.data.isPit(X + 2, Y) &&
                                    !dungeon.data.isPit(X - 2, Y - 1) && !dungeon.data.isPit(X - 1, Y - 1) && !dungeon.data.isPit(X, Y - 1) && !dungeon.data.isPit(X + 1, Y - 1) && !dungeon.data.isPit(X + 2, Y - 1) &&
                                    !dungeon.data.isPit(X - 2, Y - 2) && !dungeon.data.isPit(X - 1, Y - 2) && !dungeon.data.isPit(X, Y - 2) && !dungeon.data.isPit(X + 1, Y - 2) && !dungeon.data.isPit(X + 2, Y - 2))
                                {
                                    validCellsCached.Add(new IntVector2(X, Y));
                                }
                            }
                        }
                    }
                }
            }
            if (validCellsCached.Count > 0) {
                IntVector2 SelectedCell = BraveUtility.RandomElement(validCellsCached);
                IntVector2 RegisteredCell = (SelectedCell);
                if (useCachedList) dungeon.data[RegisteredCell].isOccupied = true;
                validCellsCached.Remove(SelectedCell);
                return (SelectedCell - currentRoom.area.basePosition);
            } else { return IntVector2.Zero; }
        }

        public static IntVector2 GetRandomAvailableCellForNPC(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, bool useCachedList) {
            if (!useCachedList | validCellsCached == null) {
                validCellsCached = new List<IntVector2>();
                validCellsCached.Clear();
            }
            if (validCellsCached.Count <= 0) {
                for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                    for (int height = -1; height <= currentRoom.area.dimensions.y; height++) {
                        int X = currentRoom.area.basePosition.x + Width;
                        int Y = currentRoom.area.basePosition.y + height;
                        if (!dungeon.data.isWall(X - 3, Y + 3) && !dungeon.data.isWall(X - 2, Y + 3) && !dungeon.data.isWall(X - 1, Y + 3) && !dungeon.data.isWall(X, Y + 3) && !dungeon.data.isWall(X + 1, Y + 3) && !dungeon.data.isWall(X + 2, Y + 3) && !dungeon.data.isWall(X + 3, Y + 3) &&
                            !dungeon.data.isWall(X - 3, Y + 2) && !dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) && !dungeon.data.isWall(X + 3, Y + 2) &&
                            !dungeon.data.isWall(X - 3, Y + 1) && !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) && !dungeon.data.isWall(X + 3, Y + 1) &&
                            !dungeon.data.isWall(X - 3, Y) && !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) && !dungeon.data.isWall(X + 3, Y) &&
                            !dungeon.data.isWall(X - 3, Y - 1) && !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) && !dungeon.data.isWall(X + 3, Y - 1) &&
                            !dungeon.data.isWall(X - 3, Y - 2) && !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) && !dungeon.data.isWall(X + 3, Y - 2) &&
                            !dungeon.data.isPit(X - 3, Y + 3) && !dungeon.data.isPit(X - 2, Y + 3) && !dungeon.data.isPit(X - 1, Y + 3) && !dungeon.data.isPit(X, Y + 3) && !dungeon.data.isPit(X + 1, Y + 3) && !dungeon.data.isPit(X + 2, Y + 3) && !dungeon.data.isPit(X + 3, Y + 3) &&
                            !dungeon.data.isPit(X - 3, Y + 2) && !dungeon.data.isPit(X - 2, Y + 2) && !dungeon.data.isPit(X - 1, Y + 2) && !dungeon.data.isPit(X, Y + 2) && !dungeon.data.isPit(X + 1, Y + 2) && !dungeon.data.isPit(X + 2, Y + 2) && !dungeon.data.isPit(X + 3, Y + 2) &&
                            !dungeon.data.isPit(X - 3, Y + 1) && !dungeon.data.isPit(X - 2, Y + 1) && !dungeon.data.isPit(X - 1, Y + 1) && !dungeon.data.isPit(X, Y + 1) && !dungeon.data.isPit(X + 1, Y + 1) && !dungeon.data.isPit(X + 2, Y + 1) && !dungeon.data.isPit(X + 3, Y + 1) &&
                            !dungeon.data.isPit(X - 3, Y) && !dungeon.data.isPit(X - 2, Y) && !dungeon.data.isPit(X - 1, Y) && !dungeon.data.isPit(X, Y) && !dungeon.data.isPit(X + 1, Y) && !dungeon.data.isPit(X + 2, Y) && !dungeon.data.isPit(X + 3, Y) &&
                            !dungeon.data.isPit(X - 3, Y - 1) && !dungeon.data.isPit(X - 2, Y - 1) && !dungeon.data.isPit(X - 1, Y - 1) && !dungeon.data.isPit(X, Y - 1) && !dungeon.data.isPit(X + 1, Y - 1) && !dungeon.data.isPit(X + 2, Y - 1) && !dungeon.data.isPit(X + 3, Y - 1) &&
                            !dungeon.data.isPit(X - 3, Y - 2) && !dungeon.data.isPit(X - 2, Y - 2) && !dungeon.data.isPit(X - 1, Y - 2) && !dungeon.data.isPit(X, Y - 2) && !dungeon.data.isPit(X + 1, Y - 2) && !dungeon.data.isPit(X + 2, Y - 2) && !dungeon.data.isPit(X + 3, Y - 2) &&
                            !dungeon.data.isPit(X - 3, Y - 3) && !dungeon.data.isPit(X - 2, Y - 3) && !dungeon.data.isPit(X - 1, Y - 3) && !dungeon.data.isPit(X, Y - 3) && !dungeon.data.isPit(X + 1, Y - 3) && !dungeon.data.isPit(X + 2, Y - 3) && !dungeon.data.isPit(X + 3, Y - 3) &&
                            !dungeon.data[X - 2, Y + 2].isOccupied && !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied && !dungeon.data[X + 2, Y + 2].isOccupied &&
                            !dungeon.data[X - 2, Y + 1].isOccupied && !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied && !dungeon.data[X + 2, Y + 1].isOccupied &&
                            !dungeon.data[X - 2, Y].isOccupied && !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied && !dungeon.data[X + 2, Y].isOccupied &&
                            !dungeon.data[X - 2, Y - 1].isOccupied && !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied && !dungeon.data[X + 2, Y - 1].isOccupied &&
                            !dungeon.data[X - 2, Y - 2].isOccupied && !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied && !dungeon.data[X + 2, Y - 2].isOccupied)
                        {
                            validCellsCached.Add(new IntVector2(X, Y));
                        }
                    }
                }
            }
            if (validCellsCached.Count > 0) {
                IntVector2 SelectedCell = BraveUtility.RandomElement(validCellsCached);
                IntVector2 RegisteredCell = (SelectedCell);
                dungeon.data[RegisteredCell].isOccupied = true;
                validCellsCached.Remove(SelectedCell);
                return (SelectedCell - currentRoom.area.basePosition);
            } else { return IntVector2.Zero; }
        }

        public static IntVector2 GetRandomAvailableCellForChest(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached) {
            if (validCellsCached == null) {
                validCellsCached = new List<IntVector2>();
                validCellsCached.Clear();
            }
            if (validCellsCached.Count <= 0) { 
                for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                    for (int height = -1; height <= currentRoom.area.dimensions.y; height++) {
                        int X = currentRoom.area.basePosition.x + Width;
                        int Y = currentRoom.area.basePosition.y + height;
                        if (!dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) &&
                            !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) &&
                            !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) &&
                            !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) &&
                            !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) &&
                            !dungeon.data.isPit(X - 1, Y + 1) && !dungeon.data.isPit(X, Y + 1) && !dungeon.data.isPit(X + 1, Y + 1) &&
                            !dungeon.data.isPit(X - 1, Y) && !dungeon.data.isPit(X, Y) && !dungeon.data.isPit(X + 1, Y) &&
                            !dungeon.data.isPit(X - 1, Y - 1) && !dungeon.data.isPit(X, Y - 1) && !dungeon.data.isPit(X + 1, Y - 1) &&
                            !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied &&
                            !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied &&
                            !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied &&
                            !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied &&
                            !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied)
                        {
                            validCellsCached.Add(new IntVector2(X, Y));
                        }
                    }
                }
            }
            if (validCellsCached.Count > 0) {
                IntVector2 SelectedCell = BraveUtility.RandomElement(validCellsCached);
                dungeon.data[SelectedCell].isOccupied = true;
                validCellsCached.Remove(SelectedCell);
                return SelectedCell;
            } else { return IntVector2.Zero; }
        }        
    }
}

