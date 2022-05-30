using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandMain {

    public static class ExpandStaticReferenceManager {

        public static List<ExpandSecretDoorPlacable> AllSecretDoors;
        public static List<ExpandCorruptedObjectDummyComponent> AllGlitchTiles;
        public static List<ExpandForgeHammerComponent> AllFriendlyHammers;
        public static List<GameObject> AllCorruptionSoundObjects;
        public static List<BeholsterShrineController> AllBeholsterShrines;
        public static List<PathingTrapController> AllMovingTraps;
        public static List<ConveyorBelt> AllConveyorBelts;
        public static List<ExpandWesternBroController> AllWesternBros;
        public static List<ExpandTallGrassPatchSystem> AllGrasses;
        public static List<ExpandAlarmMushroomPlacable> AllAlarmMushrooms;


        static ExpandStaticReferenceManager() {
            AllSecretDoors = new List<ExpandSecretDoorPlacable>();
            AllGlitchTiles = new List<ExpandCorruptedObjectDummyComponent>();

            // Things generated during game play.
            AllFriendlyHammers = new List<ExpandForgeHammerComponent>();
            AllCorruptionSoundObjects = new List<GameObject>();
            AllConveyorBelts = new List<ConveyorBelt>();

            //Things generated during floor generation or gameplay.
            AllBeholsterShrines = new List<BeholsterShrineController>();
            AllMovingTraps = new List<PathingTrapController>();

            //West Bros List
            AllWesternBros = new List<ExpandWesternBroController>();

            // Jungle Grass Patches
            AllGrasses = new List<ExpandTallGrassPatchSystem>();

            // Alarm Mushrooms
            AllAlarmMushrooms = new List<ExpandAlarmMushroomPlacable>();
        }

        public static void PopulateLists() {
            BeholsterShrineController[] BeholsterShrines = Object.FindObjectsOfType<BeholsterShrineController>();
            FlippableCover[] Tables = Object.FindObjectsOfType<FlippableCover>();
            PathingTrapController[] MovingTraps = Object.FindObjectsOfType<PathingTrapController>();
            ConveyorBelt[] ConveyorBelts = Object.FindObjectsOfType<ConveyorBelt>();
            ExpandAlarmMushroomPlacable[] Mushrooms = Object.FindObjectsOfType<ExpandAlarmMushroomPlacable>();

            if (BeholsterShrines != null) {
                foreach (BeholsterShrineController BeholsterShrine in BeholsterShrines) { AllBeholsterShrines.Add(BeholsterShrine); }
            }
            if (MovingTraps != null) {
                foreach (PathingTrapController MovingTrap in MovingTraps) { AllMovingTraps.Add(MovingTrap); }
            }
            if (ConveyorBelts != null) {
                foreach (ConveyorBelt conveyorBelt in ConveyorBelts) { AllConveyorBelts.Add(conveyorBelt); }
            }

            if (Mushrooms != null) {
                foreach (ExpandAlarmMushroomPlacable mushroom in Mushrooms) { AllAlarmMushrooms.Add(mushroom); }
            }
        } 


        public static void ClearStaticPerLevelData() {
            if (GameManager.Instance?.Dungeon?.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.CATACOMBGEON) { AllSecretDoors.Clear(); }
            AllGlitchTiles.Clear();
            AllFriendlyHammers.Clear();
            AllCorruptionSoundObjects.Clear();
            AllBeholsterShrines.Clear();
            AllMovingTraps.Clear();
            AllConveyorBelts.Clear();
            AllWesternBros.Clear();
            AllGrasses.Clear();
            AllAlarmMushrooms.Clear();
        }

        public static void ForceClearAllStaticMemory() {
            AllSecretDoors.Clear();
            AllGlitchTiles.Clear();
            AllFriendlyHammers.Clear();
            AllCorruptionSoundObjects.Clear();
            AllBeholsterShrines.Clear();
            AllMovingTraps.Clear();
            AllConveyorBelts.Clear();
            AllWesternBros.Clear();
            AllGrasses.Clear();
            AllAlarmMushrooms.Clear();
        }
    }
}

