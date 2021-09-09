using System.Collections.Generic;
using UnityEngine;
using Dungeonator;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandObjectDatabase {

        private static readonly string sharedAssets = "shared_auto_001";
        private static readonly string sharedAssets2 = "shared_auto_002";
        private static readonly string braveResources = "brave_resources_001";

        public static GameObject YellowDrum { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Yellow Drum"); } }
        public static GameObject RedDrum { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Red Drum"); } }
        public static GameObject WaterDrum { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("Blue Drum"); } }
        public static GameObject OilDrum { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("Purple Drum"); } }
        public static GameObject IceBomb { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("Ice Cube Bomb"); } }
        public static GameObject TableHorizontal { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Table_Horizontal"); } }
        public static GameObject TableVertical { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Table_Vertical"); } }
        public static GameObject TableHorizontalStone { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Table_Horizontal_Stone"); } }
        public static GameObject TableVerticalStone { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Table_Vertical_Stone"); } }
        public static GameObject NPCOldMan { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("NPC_Old_Man"); } }
        public static GameObject NPCSynergrace { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("NPC_Synergrace"); } }
        public static GameObject NPCTonic { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("NPC_Tonic"); } }
        public static GameObject NPCCursola { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("NPC_Curse_Jailed"); } }
        public static GameObject NPCGunMuncher { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("NPC_GunberMuncher"); } }
        public static GameObject NPCEvilMuncher { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("NPC_GunberMuncher_Evil"); } }
        public static GameObject NPCMonsterManuel { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("NPC_Monster_Manuel"); } }
        public static GameObject NPCVampire { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("NPC_Vampire"); } }
        public static GameObject NPCGuardLeft { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("NPC_Guardian_Left"); } }
        public static GameObject NPCGuardRight { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("NPC_Guardian_Right"); } }
        public static GameObject NPCTruthKnower { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("NPC_Truth_Knower"); } }
        public static GameObject NPCHeartDispenser { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("HeartDispenser"); } }
        public static GameObject AmygdalaNorth { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<GameObject>("Amygdala_North"); } }
        public static GameObject AmygdalaSouth { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<GameObject>("Amygdala_South"); } }
        public static GameObject AmygdalaWest { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<GameObject>("Amygdala_West"); } }
        public static GameObject AmygdalaEast { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<GameObject>("Amygdala_East"); } }
        public static GameObject SpaceFog { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<GameObject>("Space Fog"); } }
        public static GameObject LockedDoor { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("SimpleLockedDoor"); } }
        public static GameObject LockedJailDoor { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("JailDoor"); } }
        public static GameObject SpikeTrap { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("trap_spike_gungeon_2x2"); } }
        public static GameObject FlameTrap { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("trap_flame_poofy_gungeon_1x1"); } }
        public static GameObject FakeTrap { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("trap_pit_gungeon_trigger_2x2"); } }
        public static GameObject PlayerCorpse { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<GameObject>("PlayerCorpse"); } }
        public static GameObject TimefallCorpse { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<GameObject>("TimefallCorpse"); } }
        public static GameObject ThoughtBubble { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<GameObject>("ThoughtBubble"); } }
        public static GameObject HangingPot { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Hanging_Pot"); } }
        public static GameObject DoorsVertical { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("GungeonShopDoor_Vertical"); } }
        public static GameObject DoorsHorizontal { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("GungeonShopDoor_Horizontal"); } }
        public static GameObject DoorsVertical_Catacombs {
            get {
                Dungeon catacombsDungeon = DungeonDatabase.GetOrLoadByName("base_catacombs");
                GameObject m_GameObject = catacombsDungeon.doorObjects.variantTiers[0].nonDatabasePlaceable;
                catacombsDungeon = null;
                return m_GameObject;
            }
        }
        public static GameObject DoorsHorizontal_Catacombs {
            get {
                Dungeon catacombsDungeon = DungeonDatabase.GetOrLoadByName("base_catacombs");
                GameObject m_GameObject = catacombsDungeon.doorObjects.variantTiers[1].nonDatabasePlaceable;
                catacombsDungeon = null;
                return m_GameObject;
            }
        }
        public static GameObject BigDoorsHorizontal { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("IronWoodDoor_Horizontal_Gungeon"); } }
        public static GameObject BigDoorsVertical { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("IronWoodDoor_Vertical_Gungeon"); } }
        public static GameObject RatTrapDoorIcon { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<GameObject>("RatTrapdoorMinimapIcon"); } }
        public static GameObject CultistBaldBowBackLeft { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("CultistBaldBowBackLeft_cutout"); } }
        public static GameObject CultistBaldBowBackRight { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("CultistBaldBowBackRight_cutout"); } }
        public static GameObject CultistBaldBowBack { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("CultistBaldBowBack_cutout"); } }
        public static GameObject CultistBaldBowLeft { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("CultistBaldBowLeft_cutout"); } }
        public static GameObject CultistHoodBowBack { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("CultistHoodBowBack_cutout"); } }
        public static GameObject CultistHoodBowLeft { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("CultistHoodBowLeft_cutout"); } }
        public static GameObject CultistHoodBowRight { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("CultistHoodBowRight_cutout"); } }
        public static GameObject ForgeHammer { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Forge_Hammer"); } }
        public static GameObject ChestBrownTwoItems { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Chest_Wood_Two_Items"); } }
        public static GameObject ChestTruth { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("TruthChest"); } }
        public static GameObject ChestBlue { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Chest_Silver"); } }
        public static GameObject ChestRed { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Chest_Red"); } }
        public static GameObject ChestBlack { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Chest_Black"); } }
        public static GameObject ChestRat { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Chest_Rat"); } }
        public static GameObject ChestMirror { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Shrine_Mirror"); } }
        public static GameObject WinchesterMinimapIcon { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("minimap_winchester_icon"); } }
        public static GameObject GatlingGullNest { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("gatlinggullnest"); } }
        public static GameObject BabyDragunNPC { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("BabyDragunJail"); } }
        public static GameObject GungeonLightStone { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Gungeon Light (Stone)"); } }
        public static GameObject GungeonLightPurple { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Gungeon Light (Purple)"); } }
        public static GameObject Sconce_Light { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Sconce_Light"); } }
        public static GameObject Sconce_Light_Side { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("Sconce_Light_Side"); } }
        public static GameObject DefaultTorch { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("DefaultTorch"); } }
        public static GameObject DefaultTorchSide { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<GameObject>("DefaultTorchSide"); } }
        public static GameObject FoldingTable { get { return PickupObjectDatabase.GetById(644).GetComponent<FoldingTableItem>().TableToSpawn.gameObject; } }
        public static GameObject EndTimes { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<GameObject>("EndTimes"); } }
        public static GameObject Mines_Cave_In { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<GameObject>("Mines_Cave_In"); } }
        public static GameObject Plunger { get { return Mines_Cave_In.GetComponent<HangingObjectController>().triggerObjectPrefab; } }
        public static GameObject CrushDoor_Horizontal {
            get {
                Dungeon sewersDungeon = DungeonDatabase.GetOrLoadByName("base_sewer");
                GameObject m_CrushDoor_Horizontal = sewersDungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements[15].room.placedObjects[0].nonenemyBehaviour.gameObject;
                sewersDungeon = null;
                return m_CrushDoor_Horizontal;
            }

        }
        public static GameObject CrushDoor_Vertical {
            get {
                Dungeon forgeDungeon = DungeonDatabase.GetOrLoadByName("base_forge");
                GameObject m_CrushDoor_Vertical = forgeDungeon.PatternSettings.flows[0].fallbackRoomTable.includedRooms.elements[2].room.placedObjects[0].nonenemyBehaviour.gameObject;
                forgeDungeon = null;
                return m_CrushDoor_Vertical;
            }
        }
        public static GameObject GungeonWarpDoor {
            get {
                Dungeon gungeonDungeon = DungeonDatabase.GetOrLoadByName("base_gungeon");
                GameObject m_GungeonWarpDoor = gungeonDungeon.WarpWingDoorPrefab;
                gungeonDungeon = null;
                return m_GungeonWarpDoor;
            }

        }
        public static GameObject CastleWarpDoor {
            get {
                Dungeon castleDungeon = DungeonDatabase.GetOrLoadByName("base_castle");
                GameObject m_CastleWarpDoor = castleDungeon.WarpWingDoorPrefab;
                castleDungeon = null;
                return m_CastleWarpDoor;
            }
        }
        public static GameObject[] ConvictPastDancers {
            get {
                Dungeon convictPastDungeon = DungeonDatabase.GetOrLoadByName("finalscenario_convict");
                ConvictPastController pastController = convictPastDungeon.PatternSettings.flows[0].AllNodes[0].overrideExactRoom.placedObjects[0].nonenemyBehaviour.gameObject.GetComponent<ConvictPastController>();
                NightclubCrowdController crowdController = pastController.crowdController;
                convictPastDungeon = null;
                return new GameObject[] {
                    crowdController.Dancers[0].gameObject,
                    crowdController.Dancers[1].gameObject,
                    crowdController.Dancers[2].gameObject,
                    crowdController.Dancers[3].gameObject,
                    crowdController.Dancers[4].gameObject,
                    crowdController.Dancers[5].gameObject,
                    crowdController.Dancers[6].gameObject,
                    crowdController.Dancers[7].gameObject,
                    crowdController.Dancers[8].gameObject,
                    crowdController.Dancers[9].gameObject,
                    crowdController.Dancers[10].gameObject,
                    crowdController.Dancers[11].gameObject,
                    crowdController.Dancers[12].gameObject,
                    crowdController.Dancers[13].gameObject,
                    crowdController.Dancers[14].gameObject,
                    crowdController.Dancers[15].gameObject
                };
            }
        }
        public static GameObject ConvictPastCrowdNPC_01 { get { return ConvictPastDancers[0]; } }
        public static GameObject ConvictPastCrowdNPC_02 { get { return ConvictPastDancers[1]; } }
        public static GameObject ConvictPastCrowdNPC_03 { get { return ConvictPastDancers[2]; } }
        public static GameObject ConvictPastCrowdNPC_04 { get { return ConvictPastDancers[3]; } }
        public static GameObject ConvictPastCrowdNPC_05 { get { return ConvictPastDancers[4]; } }
        public static GameObject ConvictPastCrowdNPC_06 { get { return ConvictPastDancers[5]; } }
        public static GameObject ConvictPastCrowdNPC_07 { get { return ConvictPastDancers[6]; } }
        public static GameObject ConvictPastCrowdNPC_08 { get { return ConvictPastDancers[7]; } }
        public static GameObject ConvictPastCrowdNPC_09 { get { return ConvictPastDancers[8]; } }
        public static GameObject ConvictPastCrowdNPC_10 { get { return ConvictPastDancers[9]; } }
        public static GameObject ConvictPastCrowdNPC_11 { get { return ConvictPastDancers[10]; } }
        public static GameObject ConvictPastCrowdNPC_12 { get { return ConvictPastDancers[11]; } }
        public static GameObject ConvictPastCrowdNPC_13 { get { return ConvictPastDancers[12]; } }
        public static GameObject ConvictPastCrowdNPC_14 { get { return ConvictPastDancers[13]; } }
        public static GameObject ConvictPastCrowdNPC_15 { get { return ConvictPastDancers[14]; } }
        public static GameObject ConvictPastCrowdNPC_16 { get { return ConvictPastDancers[15]; } }
        // public static GameObject SellPit;
        // public static GameObject TallGrassStrip;
        // public static GameObject DimensionFog;

        // DungeonPlacables
        public static DungeonPlaceable ExplodyBarrel { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<DungeonPlaceable>("ExplodyBarrel_Maybe"); } }
        public static DungeonPlaceable CoffinVertical { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<DungeonPlaceable>("Vertical Coffin"); } }
        public static DungeonPlaceable CoffinHorizontal { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<DungeonPlaceable>("Horizontal Coffin"); } }
        public static DungeonPlaceable Brazier { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<DungeonPlaceable>("Brazier"); } }
        public static DungeonPlaceable CursedPot { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<DungeonPlaceable>("Curse Pot"); } }
        public static DungeonPlaceable Sarcophogus { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<DungeonPlaceable>("Sarcophogus"); } }
        public static DungeonPlaceable GodRays { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<DungeonPlaceable>("Godrays_placeable"); } }
        public static DungeonPlaceable SpecialTraps { get { return ResourceManager.LoadAssetBundle(braveResources).LoadAsset<DungeonPlaceable>("RobotDaveTraps"); } }
        public static DungeonPlaceable PitTrap { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<DungeonPlaceable>("Pit Trap"); } }
        public static DungeonPlaceable Bush { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<DungeonPlaceable>("Bush"); } }
        public static DungeonPlaceable BushFlowers { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<DungeonPlaceable>("Bush Flowers"); } }
        public static DungeonPlaceable WoodenBarrel { get { return ResourceManager.LoadAssetBundle(sharedAssets).LoadAsset<DungeonPlaceable>("Barrel_collection"); } }
        public static DungeonPlaceable WrithingBulletman { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<DungeonPlaceable>("Writhing Bulletman"); } }
        public static DungeonPlaceable GungeonLockedDoors {
            get {
                Dungeon gungeonDungeon = DungeonDatabase.GetOrLoadByName("base_gungeon");
                DungeonPlaceable m_GungeonLockedDoors = gungeonDungeon.lockedDoorObjects;
                return m_GungeonLockedDoors;
            }
        }
        public static DungeonPlaceable IronWoodDoors { get { return ResourceManager.LoadAssetBundle(sharedAssets2).LoadAsset<DungeonPlaceable>("DoorTest"); } }
        

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

