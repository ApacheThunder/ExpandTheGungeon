using System.Collections.Generic;
using UnityEngine;
using Dungeonator;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandObjectDatabase : MonoBehaviour {

        private AssetBundle sharedAssets;
        private AssetBundle sharedAssets2;
        private AssetBundle braveResources;
        
        private Dungeon convictPastDungeon;
        private Dungeon catacombsDungeon;
        private Dungeon sewersDungeon;
        private Dungeon forgeDungeon;
        private ConvictPastController pastController;
        private NightclubCrowdController crowdController;

        public GameObject YellowDrum;
        public GameObject RedDrum;
        public GameObject WaterDrum;
        public GameObject OilDrum;
        public GameObject IceBomb;
        public GameObject TableHorizontal;
        public GameObject TableVertical;
        public GameObject TableHorizontalStone;
        public GameObject TableVerticalStone;
        public GameObject NPCOldMan;
        public GameObject NPCSynergrace;
        public GameObject NPCTonic;
        public GameObject NPCCursola;
        public GameObject NPCGunMuncher;
        public GameObject NPCEvilMuncher;
        public GameObject NPCMonsterManuel;
        public GameObject NPCVampire;
        public GameObject NPCGuardLeft;
        public GameObject NPCGuardRight;
        public GameObject NPCTruthKnower;
        public GameObject NPCHeartDispenser;
        public GameObject AmygdalaNorth;
        public GameObject AmygdalaSouth;
        public GameObject AmygdalaWest;
        public GameObject AmygdalaEast;
        public GameObject SpaceFog;
        public GameObject LockedDoor;
        public GameObject LockedJailDoor;
        public GameObject SpikeTrap;
        public GameObject FlameTrap;
        public GameObject FakeTrap;
        public GameObject PlayerCorpse;
        public GameObject TimefallCorpse;
        public GameObject ThoughtBubble;
        public GameObject HangingPot;
        public GameObject DoorsVertical;
        public GameObject DoorsHorizontal;
        public GameObject BigDoorsHorizontal;
        public GameObject BigDoorsVertical;
        public GameObject RatTrapDoorIcon;
        public GameObject CultistBaldBowBackLeft;
        public GameObject CultistBaldBowBackRight;
        public GameObject CultistBaldBowBack;
        public GameObject CultistBaldBowLeft;
        public GameObject CultistHoodBowBack;
        public GameObject CultistHoodBowLeft;
        public GameObject CultistHoodBowRight;
        public GameObject ForgeHammer;
        public GameObject ChestBrownTwoItems;
        public GameObject ChestTruth;
        public GameObject ChestRat;
        public GameObject ChestMirror;
        public GameObject TallGrassStrip;
        public GameObject SellPit;
        public GameObject ConvictPastCrowdNPC_01;
        public GameObject ConvictPastCrowdNPC_02;
        public GameObject ConvictPastCrowdNPC_03;
        public GameObject ConvictPastCrowdNPC_04;
        public GameObject ConvictPastCrowdNPC_05;
        public GameObject ConvictPastCrowdNPC_06;
        public GameObject ConvictPastCrowdNPC_07;
        public GameObject ConvictPastCrowdNPC_08;
        public GameObject ConvictPastCrowdNPC_09;
        public GameObject ConvictPastCrowdNPC_10;
        public GameObject ConvictPastCrowdNPC_11;
        public GameObject ConvictPastCrowdNPC_12;
        public GameObject ConvictPastCrowdNPC_13;
        public GameObject ConvictPastCrowdNPC_14;
        public GameObject ConvictPastCrowdNPC_15;
        public GameObject ConvictPastCrowdNPC_16;
        public GameObject[] ConvictPastDancers;
        public GameObject DoorsVertical_Catacombs;
        public GameObject DoorsHorizontal_Catacombs;
        public GameObject FoldingTable;
        public GameObject WinchesterMinimapIcon;
        public GameObject CrushDoor_Horizontal;
        public GameObject CrushDoor_Vertical;
        public GameObject Mines_Cave_In;
        public GameObject Plunger;
        public GameObject GatlingGullNest;
        public GameObject BabyDragunNPC;

        // DungeonPlacables
        public DungeonPlaceable ExplodyBarrel;
        public DungeonPlaceable CoffinVertical;
        public DungeonPlaceable CoffinHorizontal;
        public DungeonPlaceable Brazier;
        public DungeonPlaceable CursedPot;
        public DungeonPlaceable Sarcophogus;
        public DungeonPlaceable GodRays;
        public DungeonPlaceable SpecialTraps;
        public DungeonPlaceable PitTrap;
        public DungeonPlaceable Bush;
        public DungeonPlaceable BushFlowers;
        public DungeonPlaceable WoodenBarrel;

        public ExpandObjectDatabase() {
            sharedAssets = ResourceManager.LoadAssetBundle("shared_auto_001");
            sharedAssets2 = ResourceManager.LoadAssetBundle("shared_auto_002");
            braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");
            convictPastDungeon = DungeonDatabase.GetOrLoadByName("finalscenario_convict");
            catacombsDungeon = DungeonDatabase.GetOrLoadByName("base_catacombs");
            sewersDungeon = DungeonDatabase.GetOrLoadByName("base_sewer");
            forgeDungeon = DungeonDatabase.GetOrLoadByName("base_forge");

            YellowDrum = sharedAssets2.LoadAsset<GameObject>("Yellow Drum");
            RedDrum = sharedAssets.LoadAsset<GameObject>("Red Drum");
            WaterDrum = sharedAssets2.LoadAsset<GameObject>("Blue Drum");
            OilDrum = sharedAssets2.LoadAsset<GameObject>("Purple Drum");
            IceBomb = sharedAssets2.LoadAsset<GameObject>("Ice Cube Bomb");
            TableHorizontal = sharedAssets.LoadAsset<GameObject>("Table_Horizontal");
            TableVertical = sharedAssets.LoadAsset<GameObject>("Table_Vertical");
            TableHorizontalStone = sharedAssets.LoadAsset<GameObject>("Table_Horizontal_Stone");
            TableVerticalStone = sharedAssets.LoadAsset<GameObject>("Table_Vertical_Stone");
            NPCOldMan = sharedAssets.LoadAsset<GameObject>("NPC_Old_Man");
            NPCSynergrace = sharedAssets.LoadAsset<GameObject>("NPC_Synergrace");
            NPCTonic = sharedAssets.LoadAsset<GameObject>("NPC_Tonic");
            NPCCursola = sharedAssets2.LoadAsset<GameObject>("NPC_Curse_Jailed");
            NPCGunMuncher = sharedAssets2.LoadAsset<GameObject>("NPC_GunberMuncher");
            NPCEvilMuncher = sharedAssets.LoadAsset<GameObject>("NPC_GunberMuncher_Evil");
            NPCMonsterManuel = sharedAssets.LoadAsset<GameObject>("NPC_Monster_Manuel");
            NPCVampire = sharedAssets2.LoadAsset<GameObject>("NPC_Vampire");
            NPCGuardLeft = sharedAssets2.LoadAsset<GameObject>("NPC_Guardian_Left");
            NPCGuardRight = sharedAssets2.LoadAsset<GameObject>("NPC_Guardian_Right");
            NPCTruthKnower = sharedAssets.LoadAsset<GameObject>("NPC_Truth_Knower");
            NPCHeartDispenser = sharedAssets2.LoadAsset<GameObject>("HeartDispenser");
            AmygdalaNorth = braveResources.LoadAsset<GameObject>("Amygdala_North");
            AmygdalaSouth = braveResources.LoadAsset<GameObject>("Amygdala_South");
            AmygdalaWest = braveResources.LoadAsset<GameObject>("Amygdala_West");
            AmygdalaEast = braveResources.LoadAsset<GameObject>("Amygdala_East");
            SpaceFog = braveResources.LoadAsset<GameObject>("Space Fog");
            LockedDoor = sharedAssets2.LoadAsset<GameObject>("SimpleLockedDoor");
            LockedJailDoor = sharedAssets2.LoadAsset<GameObject>("JailDoor");
            SpikeTrap = sharedAssets.LoadAsset<GameObject>("trap_spike_gungeon_2x2");
            FlameTrap = sharedAssets2.LoadAsset<GameObject>("trap_flame_poofy_gungeon_1x1");
            FakeTrap = sharedAssets.LoadAsset<GameObject>("trap_pit_gungeon_trigger_2x2");
            PlayerCorpse = braveResources.LoadAsset<GameObject>("PlayerCorpse");
            TimefallCorpse = braveResources.LoadAsset<GameObject>("TimefallCorpse");
            ThoughtBubble = braveResources.LoadAsset<GameObject>("ThoughtBubble");
            HangingPot = sharedAssets.LoadAsset<GameObject>("Hanging_Pot");
            DoorsVertical = sharedAssets2.LoadAsset<GameObject>("GungeonShopDoor_Vertical");
            DoorsHorizontal = sharedAssets2.LoadAsset<GameObject>("GungeonShopDoor_Horizontal");            
            DoorsHorizontal_Catacombs = catacombsDungeon.doorObjects.variantTiers[0].nonDatabasePlaceable;
            DoorsVertical_Catacombs = catacombsDungeon.doorObjects.variantTiers[1].nonDatabasePlaceable;
            BigDoorsHorizontal = sharedAssets2.LoadAsset<GameObject>("IronWoodDoor_Horizontal_Gungeon");
            BigDoorsVertical = sharedAssets2.LoadAsset<GameObject>("IronWoodDoor_Vertical_Gungeon");
            RatTrapDoorIcon = braveResources.LoadAsset<GameObject>("RatTrapdoorMinimapIcon");
            CultistBaldBowBackLeft = sharedAssets2.LoadAsset<GameObject>("CultistBaldBowBackLeft_cutout");
            CultistBaldBowBackRight = sharedAssets2.LoadAsset<GameObject>("CultistBaldBowBackRight_cutout");
            CultistBaldBowBack = sharedAssets2.LoadAsset<GameObject>("CultistBaldBowBack_cutout");
            CultistBaldBowLeft = sharedAssets2.LoadAsset<GameObject>("CultistBaldBowLeft_cutout");
            CultistHoodBowBack = sharedAssets2.LoadAsset<GameObject>("CultistHoodBowBack_cutout");
            CultistHoodBowLeft = sharedAssets2.LoadAsset<GameObject>("CultistHoodBowLeft_cutout");
            CultistHoodBowRight = sharedAssets2.LoadAsset<GameObject>("CultistHoodBowRight_cutout");
            ForgeHammer = sharedAssets.LoadAsset<GameObject>("Forge_Hammer");
            ChestBrownTwoItems = sharedAssets.LoadAsset<GameObject>("Chest_Wood_Two_Items");
            ChestTruth = sharedAssets.LoadAsset<GameObject>("TruthChest");
            ChestRat = sharedAssets.LoadAsset<GameObject>("Chest_Rat");
            ChestMirror = sharedAssets.LoadAsset<GameObject>("Shrine_Mirror");
            WinchesterMinimapIcon = sharedAssets.LoadAsset<GameObject>("minimap_winchester_icon");
            GatlingGullNest = sharedAssets.LoadAsset<GameObject>("gatlinggullnest");
            BabyDragunNPC = sharedAssets2.LoadAsset<GameObject>("BabyDragunJail");


            foreach (WeightedRoom wRoom in sewersDungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements) {
                if (wRoom.room != null && !string.IsNullOrEmpty(wRoom.room.name)) {
                    if (wRoom.room.name.ToLower().StartsWith("sewer_trash_compactor_001")) {
                        CrushDoor_Horizontal = wRoom.room.placedObjects[0].nonenemyBehaviour.gameObject;
                    }
                }
            }

            foreach (WeightedRoom wRoom in forgeDungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements) {
                if (wRoom.room != null && !string.IsNullOrEmpty(wRoom.room.name)) {
                    if (wRoom.room.name.ToLower().StartsWith("forge_normal_cubulead_03")) {
                        CrushDoor_Vertical = wRoom.room.placedObjects[0].nonenemyBehaviour.gameObject;
                    }
                }
            }

            // Dungeon Placables
            ExplodyBarrel = sharedAssets2.LoadAsset<DungeonPlaceable>("ExplodyBarrel_Maybe");
            CoffinVertical = sharedAssets2.LoadAsset<DungeonPlaceable>("Vertical Coffin");
            CoffinHorizontal = sharedAssets2.LoadAsset<DungeonPlaceable>("Horizontal Coffin");
            Brazier = sharedAssets.LoadAsset<DungeonPlaceable>("Brazier");
            CursedPot = sharedAssets.LoadAsset<DungeonPlaceable>("Curse Pot");
            Sarcophogus = sharedAssets.LoadAsset<DungeonPlaceable>("Sarcophogus");
            GodRays = sharedAssets.LoadAsset<DungeonPlaceable>("Godrays_placeable");
            SpecialTraps = braveResources.LoadAsset<DungeonPlaceable>("RobotDaveTraps");
            PitTrap = sharedAssets2.LoadAsset<DungeonPlaceable>("Pit Trap");
            Bush = sharedAssets2.LoadAsset<DungeonPlaceable>("Bush");
            BushFlowers = sharedAssets2.LoadAsset<DungeonPlaceable>("Bush Flowers");
            WoodenBarrel = sharedAssets.LoadAsset<DungeonPlaceable>("Barrel_collection");
            pastController = convictPastDungeon.PatternSettings.flows[0].AllNodes[0].overrideExactRoom.placedObjects[0].nonenemyBehaviour.gameObject.GetComponent<ConvictPastController>();
            crowdController = pastController.crowdController;

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

            Mines_Cave_In = sharedAssets2.LoadAsset<GameObject>("Mines_Cave_In");
            Plunger = Mines_Cave_In.GetComponent<HangingObjectController>().triggerObjectPrefab;


            FoldingTable = PickupObjectDatabase.GetById(644).GetComponent<FoldingTableItem>().TableToSpawn.gameObject;

            sharedAssets = null;
            sharedAssets2 = null;
            braveResources = null;
            convictPastDungeon = null;
            catacombsDungeon = null;
            sewersDungeon = null;
            forgeDungeon = null;
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

